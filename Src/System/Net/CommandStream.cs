using System;
using System.IO;
using System.Text;

namespace System.Net
{
	// Token: 0x02000198 RID: 408
	internal class CommandStream : PooledStream
	{
		// Token: 0x06000FCC RID: 4044 RVA: 0x00052C73 File Offset: 0x00050E73
		internal CommandStream(ConnectionPool connectionPool, TimeSpan lifetime, bool checkLifetime)
			: base(connectionPool, lifetime, checkLifetime)
		{
			this.m_Decoder = this.m_Encoding.GetDecoder();
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x00052CA8 File Offset: 0x00050EA8
		internal virtual void Abort(Exception e)
		{
			lock (this)
			{
				if (this.m_Aborted)
				{
					return;
				}
				this.m_Aborted = true;
				base.CanBePooled = false;
			}
			try
			{
				base.Close(0);
			}
			finally
			{
				if (e != null)
				{
					this.InvokeRequestCallback(e);
				}
				else
				{
					this.InvokeRequestCallback(null);
				}
			}
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x00052D20 File Offset: 0x00050F20
		protected override void Dispose(bool disposing)
		{
			this.InvokeRequestCallback(null);
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x00052D2C File Offset: 0x00050F2C
		protected void InvokeRequestCallback(object obj)
		{
			WebRequest request = this.m_Request;
			if (request != null)
			{
				request.RequestCallback(obj);
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000FD0 RID: 4048 RVA: 0x00052D4A File Offset: 0x00050F4A
		internal bool RecoverableFailure
		{
			get
			{
				return this.m_RecoverableFailure;
			}
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x00052D52 File Offset: 0x00050F52
		protected void MarkAsRecoverableFailure()
		{
			if (this.m_Index <= 1)
			{
				this.m_RecoverableFailure = true;
			}
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x00052D64 File Offset: 0x00050F64
		internal Stream SubmitRequest(WebRequest request, bool async, bool readInitalResponseOnConnect)
		{
			this.ClearState();
			base.UpdateLifetime();
			CommandStream.PipelineEntry[] array = this.BuildCommandsList(request);
			this.InitCommandPipeline(request, array, async);
			if (readInitalResponseOnConnect && base.JustConnected)
			{
				this.m_DoSend = false;
				this.m_Index = -1;
			}
			return this.ContinueCommandPipeline();
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x00052DAD File Offset: 0x00050FAD
		protected virtual void ClearState()
		{
			this.InitCommandPipeline(null, null, false);
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x00052DB8 File Offset: 0x00050FB8
		protected virtual CommandStream.PipelineEntry[] BuildCommandsList(WebRequest request)
		{
			return null;
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x00052DBB File Offset: 0x00050FBB
		protected Exception GenerateException(WebExceptionStatus status, Exception innerException)
		{
			return new WebException(NetRes.GetWebStatusString("net_connclosed", status), innerException, status, null);
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x00052DD0 File Offset: 0x00050FD0
		protected Exception GenerateException(FtpStatusCode code, string statusDescription, Exception innerException)
		{
			return new WebException(SR.GetString("net_servererror", new object[] { NetRes.GetWebStatusCodeString(code, statusDescription) }), innerException, WebExceptionStatus.ProtocolError, null);
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x00052DF4 File Offset: 0x00050FF4
		protected void InitCommandPipeline(WebRequest request, CommandStream.PipelineEntry[] commands, bool async)
		{
			this.m_Commands = commands;
			this.m_Index = 0;
			this.m_Request = request;
			this.m_Aborted = false;
			this.m_DoRead = true;
			this.m_DoSend = true;
			this.m_CurrentResponseDescription = null;
			this.m_Async = async;
			this.m_RecoverableFailure = false;
			this.m_AbortReason = string.Empty;
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x00052E4C File Offset: 0x0005104C
		internal void CheckContinuePipeline()
		{
			if (this.m_Async)
			{
				return;
			}
			try
			{
				this.ContinueCommandPipeline();
			}
			catch (Exception ex)
			{
				this.Abort(ex);
			}
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x00052E88 File Offset: 0x00051088
		protected Stream ContinueCommandPipeline()
		{
			bool async = this.m_Async;
			while (this.m_Index < this.m_Commands.Length)
			{
				if (this.m_DoSend)
				{
					if (this.m_Index < 0)
					{
						throw new InternalException();
					}
					byte[] bytes = this.Encoding.GetBytes(this.m_Commands[this.m_Index].Command);
					if (Logging.On)
					{
						string text = this.m_Commands[this.m_Index].Command.Substring(0, this.m_Commands[this.m_Index].Command.Length - 2);
						if (this.m_Commands[this.m_Index].HasFlag(CommandStream.PipelineEntryFlags.DontLogParameter))
						{
							int num = text.IndexOf(' ');
							if (num != -1)
							{
								text = text.Substring(0, num) + " ********";
							}
						}
						Logging.PrintInfo(Logging.Web, this, SR.GetString("net_log_sending_command", new object[] { text }));
					}
					try
					{
						if (async)
						{
							this.BeginWrite(bytes, 0, bytes.Length, CommandStream.m_WriteCallbackDelegate, this);
						}
						else
						{
							this.Write(bytes, 0, bytes.Length);
						}
					}
					catch (IOException)
					{
						this.MarkAsRecoverableFailure();
						throw;
					}
					catch
					{
						throw;
					}
					if (async)
					{
						return null;
					}
				}
				Stream stream = null;
				bool flag = this.PostSendCommandProcessing(ref stream);
				if (flag)
				{
					return stream;
				}
			}
			lock (this)
			{
				this.Close();
			}
			return null;
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x00053018 File Offset: 0x00051218
		private bool PostSendCommandProcessing(ref Stream stream)
		{
			if (this.m_DoRead)
			{
				bool async = this.m_Async;
				int index = this.m_Index;
				CommandStream.PipelineEntry[] commands = this.m_Commands;
				try
				{
					ResponseDescription responseDescription = this.ReceiveCommandResponse();
					if (async)
					{
						return true;
					}
					this.m_CurrentResponseDescription = responseDescription;
				}
				catch
				{
					if (index < 0 || index >= commands.Length || commands[index].Command != "QUIT\r\n")
					{
						throw;
					}
				}
			}
			return this.PostReadCommandProcessing(ref stream);
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x00053098 File Offset: 0x00051298
		private bool PostReadCommandProcessing(ref Stream stream)
		{
			if (this.m_Index >= this.m_Commands.Length)
			{
				return false;
			}
			this.m_DoSend = false;
			this.m_DoRead = false;
			CommandStream.PipelineEntry pipelineEntry;
			if (this.m_Index == -1)
			{
				pipelineEntry = null;
			}
			else
			{
				pipelineEntry = this.m_Commands[this.m_Index];
			}
			CommandStream.PipelineInstruction pipelineInstruction;
			if (this.m_CurrentResponseDescription == null && pipelineEntry.Command == "QUIT\r\n")
			{
				pipelineInstruction = CommandStream.PipelineInstruction.Advance;
			}
			else
			{
				pipelineInstruction = this.PipelineCallback(pipelineEntry, this.m_CurrentResponseDescription, false, ref stream);
			}
			if (pipelineInstruction == CommandStream.PipelineInstruction.Abort)
			{
				Exception ex;
				if (this.m_AbortReason != string.Empty)
				{
					ex = new WebException(this.m_AbortReason);
				}
				else
				{
					ex = this.GenerateException(WebExceptionStatus.ServerProtocolViolation, null);
				}
				this.Abort(ex);
				throw ex;
			}
			if (pipelineInstruction == CommandStream.PipelineInstruction.Advance)
			{
				this.m_CurrentResponseDescription = null;
				this.m_DoSend = true;
				this.m_DoRead = true;
				this.m_Index++;
			}
			else
			{
				if (pipelineInstruction == CommandStream.PipelineInstruction.Pause)
				{
					return true;
				}
				if (pipelineInstruction == CommandStream.PipelineInstruction.GiveStream)
				{
					this.m_CurrentResponseDescription = null;
					this.m_DoRead = true;
					if (this.m_Async)
					{
						this.ContinueCommandPipeline();
						this.InvokeRequestCallback(stream);
					}
					return true;
				}
				if (pipelineInstruction == CommandStream.PipelineInstruction.Reread)
				{
					this.m_CurrentResponseDescription = null;
					this.m_DoRead = true;
				}
			}
			return false;
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x000531B1 File Offset: 0x000513B1
		protected virtual CommandStream.PipelineInstruction PipelineCallback(CommandStream.PipelineEntry entry, ResponseDescription response, bool timeout, ref Stream stream)
		{
			return CommandStream.PipelineInstruction.Abort;
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x000531B4 File Offset: 0x000513B4
		private static void ReadCallback(IAsyncResult asyncResult)
		{
			ReceiveState receiveState = (ReceiveState)asyncResult.AsyncState;
			try
			{
				Stream connection = receiveState.Connection;
				int num = 0;
				try
				{
					num = connection.EndRead(asyncResult);
					if (num == 0)
					{
						receiveState.Connection.CloseSocket();
					}
				}
				catch (IOException)
				{
					receiveState.Connection.MarkAsRecoverableFailure();
					throw;
				}
				catch
				{
					throw;
				}
				receiveState.Connection.ReceiveCommandResponseCallback(receiveState, num);
			}
			catch (Exception ex)
			{
				receiveState.Connection.Abort(ex);
			}
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x00053248 File Offset: 0x00051448
		private static void WriteCallback(IAsyncResult asyncResult)
		{
			CommandStream commandStream = (CommandStream)asyncResult.AsyncState;
			try
			{
				try
				{
					commandStream.EndWrite(asyncResult);
				}
				catch (IOException)
				{
					commandStream.MarkAsRecoverableFailure();
					throw;
				}
				catch
				{
					throw;
				}
				Stream stream = null;
				if (!commandStream.PostSendCommandProcessing(ref stream))
				{
					commandStream.ContinueCommandPipeline();
				}
			}
			catch (Exception ex)
			{
				commandStream.Abort(ex);
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000FDF RID: 4063 RVA: 0x000532C0 File Offset: 0x000514C0
		// (set) Token: 0x06000FE0 RID: 4064 RVA: 0x000532C8 File Offset: 0x000514C8
		protected Encoding Encoding
		{
			get
			{
				return this.m_Encoding;
			}
			set
			{
				this.m_Encoding = value;
				this.m_Decoder = this.m_Encoding.GetDecoder();
			}
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x000532E2 File Offset: 0x000514E2
		protected virtual bool CheckValid(ResponseDescription response, ref int validThrough, ref int completeLength)
		{
			return false;
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x000532E8 File Offset: 0x000514E8
		private ResponseDescription ReceiveCommandResponse()
		{
			ReceiveState receiveState = new ReceiveState(this);
			try
			{
				if (this.m_Buffer.Length > 0)
				{
					this.ReceiveCommandResponseCallback(receiveState, -1);
				}
				else
				{
					try
					{
						if (this.m_Async)
						{
							this.BeginRead(receiveState.Buffer, 0, receiveState.Buffer.Length, CommandStream.m_ReadCallbackDelegate, receiveState);
							return null;
						}
						int num = this.Read(receiveState.Buffer, 0, receiveState.Buffer.Length);
						if (num == 0)
						{
							base.CloseSocket();
						}
						this.ReceiveCommandResponseCallback(receiveState, num);
					}
					catch (IOException)
					{
						this.MarkAsRecoverableFailure();
						throw;
					}
					catch
					{
						throw;
					}
				}
			}
			catch (Exception ex)
			{
				if (ex is WebException)
				{
					throw;
				}
				throw this.GenerateException(WebExceptionStatus.ReceiveFailure, ex);
			}
			return receiveState.Resp;
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x000533B8 File Offset: 0x000515B8
		private void ReceiveCommandResponseCallback(ReceiveState state, int bytesRead)
		{
			int num = -1;
			for (;;)
			{
				int validThrough = state.ValidThrough;
				if (this.m_Buffer.Length > 0)
				{
					state.Resp.StatusBuffer.Append(this.m_Buffer);
					this.m_Buffer = string.Empty;
					if (!this.CheckValid(state.Resp, ref validThrough, ref num))
					{
						break;
					}
				}
				else
				{
					if (bytesRead <= 0)
					{
						goto Block_3;
					}
					char[] array = new char[this.m_Decoder.GetCharCount(state.Buffer, 0, bytesRead)];
					int chars = this.m_Decoder.GetChars(state.Buffer, 0, bytesRead, array, 0, false);
					string text = new string(array, 0, chars);
					state.Resp.StatusBuffer.Append(text);
					if (!this.CheckValid(state.Resp, ref validThrough, ref num))
					{
						goto Block_4;
					}
					if (num >= 0)
					{
						int num2 = state.Resp.StatusBuffer.Length - num;
						if (num2 > 0)
						{
							this.m_Buffer = text.Substring(text.Length - num2, num2);
						}
					}
				}
				if (num < 0)
				{
					state.ValidThrough = validThrough;
					try
					{
						if (this.m_Async)
						{
							this.BeginRead(state.Buffer, 0, state.Buffer.Length, CommandStream.m_ReadCallbackDelegate, state);
							return;
						}
						bytesRead = this.Read(state.Buffer, 0, state.Buffer.Length);
						if (bytesRead == 0)
						{
							base.CloseSocket();
						}
						continue;
					}
					catch (IOException)
					{
						this.MarkAsRecoverableFailure();
						throw;
					}
					catch
					{
						throw;
					}
					goto IL_16C;
				}
				goto IL_16C;
			}
			throw this.GenerateException(WebExceptionStatus.ServerProtocolViolation, null);
			Block_3:
			throw this.GenerateException(WebExceptionStatus.ServerProtocolViolation, null);
			Block_4:
			throw this.GenerateException(WebExceptionStatus.ServerProtocolViolation, null);
			IL_16C:
			string text2 = state.Resp.StatusBuffer.ToString();
			state.Resp.StatusDescription = text2.Substring(0, num);
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, this, SR.GetString("net_log_received_response", new object[] { text2.Substring(0, num - 2) }));
			}
			if (this.m_Async)
			{
				if (state.Resp != null)
				{
					this.m_CurrentResponseDescription = state.Resp;
				}
				Stream stream = null;
				if (this.PostReadCommandProcessing(ref stream))
				{
					return;
				}
				this.ContinueCommandPipeline();
			}
		}

		// Token: 0x040012E3 RID: 4835
		private static readonly AsyncCallback m_WriteCallbackDelegate = new AsyncCallback(CommandStream.WriteCallback);

		// Token: 0x040012E4 RID: 4836
		private static readonly AsyncCallback m_ReadCallbackDelegate = new AsyncCallback(CommandStream.ReadCallback);

		// Token: 0x040012E5 RID: 4837
		private bool m_RecoverableFailure;

		// Token: 0x040012E6 RID: 4838
		protected WebRequest m_Request;

		// Token: 0x040012E7 RID: 4839
		protected bool m_Async;

		// Token: 0x040012E8 RID: 4840
		private bool m_Aborted;

		// Token: 0x040012E9 RID: 4841
		protected CommandStream.PipelineEntry[] m_Commands;

		// Token: 0x040012EA RID: 4842
		protected int m_Index;

		// Token: 0x040012EB RID: 4843
		private bool m_DoRead;

		// Token: 0x040012EC RID: 4844
		private bool m_DoSend;

		// Token: 0x040012ED RID: 4845
		private ResponseDescription m_CurrentResponseDescription;

		// Token: 0x040012EE RID: 4846
		protected string m_AbortReason;

		// Token: 0x040012EF RID: 4847
		private const int _WaitingForPipeline = 1;

		// Token: 0x040012F0 RID: 4848
		private const int _CompletedPipeline = 2;

		// Token: 0x040012F1 RID: 4849
		private string m_Buffer = string.Empty;

		// Token: 0x040012F2 RID: 4850
		private Encoding m_Encoding = Encoding.UTF8;

		// Token: 0x040012F3 RID: 4851
		private Decoder m_Decoder;

		// Token: 0x02000743 RID: 1859
		internal enum PipelineInstruction
		{
			// Token: 0x040031BB RID: 12731
			Abort,
			// Token: 0x040031BC RID: 12732
			Advance,
			// Token: 0x040031BD RID: 12733
			Pause,
			// Token: 0x040031BE RID: 12734
			Reread,
			// Token: 0x040031BF RID: 12735
			GiveStream
		}

		// Token: 0x02000744 RID: 1860
		[Flags]
		internal enum PipelineEntryFlags
		{
			// Token: 0x040031C1 RID: 12737
			UserCommand = 1,
			// Token: 0x040031C2 RID: 12738
			GiveDataStream = 2,
			// Token: 0x040031C3 RID: 12739
			CreateDataConnection = 4,
			// Token: 0x040031C4 RID: 12740
			DontLogParameter = 8
		}

		// Token: 0x02000745 RID: 1861
		internal class PipelineEntry
		{
			// Token: 0x060041C4 RID: 16836 RVA: 0x001114C9 File Offset: 0x0010F6C9
			internal PipelineEntry(string command)
			{
				this.Command = command;
			}

			// Token: 0x060041C5 RID: 16837 RVA: 0x001114D8 File Offset: 0x0010F6D8
			internal PipelineEntry(string command, CommandStream.PipelineEntryFlags flags)
			{
				this.Command = command;
				this.Flags = flags;
			}

			// Token: 0x060041C6 RID: 16838 RVA: 0x001114EE File Offset: 0x0010F6EE
			internal bool HasFlag(CommandStream.PipelineEntryFlags flags)
			{
				return (this.Flags & flags) > (CommandStream.PipelineEntryFlags)0;
			}

			// Token: 0x040031C5 RID: 12741
			internal string Command;

			// Token: 0x040031C6 RID: 12742
			internal CommandStream.PipelineEntryFlags Flags;
		}
	}
}
