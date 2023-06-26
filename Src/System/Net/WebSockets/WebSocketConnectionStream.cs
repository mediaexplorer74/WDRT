using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.WebSockets
{
	// Token: 0x02000233 RID: 563
	internal class WebSocketConnectionStream : BufferedReadStream, WebSocketBase.IWebSocketStream
	{
		// Token: 0x06001508 RID: 5384 RVA: 0x0006E24C File Offset: 0x0006C44C
		public WebSocketConnectionStream(ConnectStream connectStream, string connectionGroupName)
			: base(new WebSocketConnectionStream.WebSocketConnection(connectStream.Connection), false)
		{
			this.m_ConnectStream = connectStream;
			this.m_ConnectionGroupName = connectionGroupName;
			this.m_CloseConnectStreamLock = new object();
			this.m_IsFastPathAllowed = this.m_ConnectStream.Connection.NetworkStream.GetType() == WebSocketConnectionStream.s_NetworkStreamType;
			if (WebSocketBase.LoggingEnabled)
			{
				Logging.Associate(Logging.WebSockets, this, this.m_ConnectStream.Connection);
			}
			this.ConsumeConnectStreamBuffer(connectStream);
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001509 RID: 5385 RVA: 0x0006E2CD File Offset: 0x0006C4CD
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x0600150A RID: 5386 RVA: 0x0006E2D0 File Offset: 0x0006C4D0
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x0600150B RID: 5387 RVA: 0x0006E2D3 File Offset: 0x0006C4D3
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x0600150C RID: 5388 RVA: 0x0006E2D6 File Offset: 0x0006C4D6
		public bool SupportsMultipleWrite
		{
			get
			{
				return ((WebSocketConnectionStream.WebSocketConnection)base.BaseStream).SupportsMultipleWrite;
			}
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x0006E2E8 File Offset: 0x0006C4E8
		public async Task CloseNetworkConnectionAsync(CancellationToken cancellationToken)
		{
			await Task.Yield();
			if (WebSocketBase.LoggingEnabled)
			{
				Logging.Enter(Logging.WebSockets, this, "CloseNetworkConnectionAsync", string.Empty);
			}
			CancellationTokenSource reasonableTimeoutCancellationTokenSource = null;
			CancellationTokenSource linkedCancellationTokenSource = null;
			CancellationToken cancellationToken2 = CancellationToken.None;
			CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
			int bytesRead = 0;
			try
			{
				reasonableTimeoutCancellationTokenSource = new CancellationTokenSource(1000);
				linkedCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(reasonableTimeoutCancellationTokenSource.Token, cancellationToken);
				cancellationToken2 = linkedCancellationTokenSource.Token;
				cancellationTokenRegistration = cancellationToken2.Register(WebSocketConnectionStream.s_OnCancel, this, false);
				WebSocketHelpers.ThrowIfConnectionAborted(this.m_ConnectStream.Connection, true);
				byte[] buffer = new byte[1];
				if (this.m_WebSocketConnection != null && this.m_InOpaqueMode)
				{
					bytesRead = await this.m_WebSocketConnection.ReadAsyncCore(buffer, 0, 1, cancellationToken2, true).SuppressContextFlow<int>();
				}
				else
				{
					bytesRead = await base.ReadAsync(buffer, 0, 1, cancellationToken2).SuppressContextFlow<int>();
				}
				if (bytesRead != 0)
				{
					if (WebSocketBase.LoggingEnabled)
					{
						Logging.Dump(Logging.WebSockets, this, "CloseNetworkConnectionAsync", buffer, 0, bytesRead);
					}
					throw new WebSocketException(WebSocketError.NotAWebSocket);
				}
				buffer = null;
			}
			catch (Exception ex)
			{
				if (!WebSocketConnectionStream.s_CanHandleException(ex))
				{
					throw;
				}
				cancellationToken.ThrowIfCancellationRequested();
			}
			finally
			{
				cancellationTokenRegistration.Dispose();
				if (linkedCancellationTokenSource != null)
				{
					linkedCancellationTokenSource.Dispose();
				}
				if (reasonableTimeoutCancellationTokenSource != null)
				{
					reasonableTimeoutCancellationTokenSource.Dispose();
				}
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Exit(Logging.WebSockets, this, "CloseNetworkConnectionAsync", bytesRead);
				}
			}
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x0006E334 File Offset: 0x0006C534
		public override void Close()
		{
			if (WebSocketBase.LoggingEnabled)
			{
				Logging.Enter(Logging.WebSockets, this, "Close", string.Empty);
			}
			try
			{
				object closeConnectStreamLock = this.m_CloseConnectStreamLock;
				lock (closeConnectStreamLock)
				{
					this.m_ConnectStream.Connection.ServicePoint.CloseConnectionGroup(this.m_ConnectionGroupName);
				}
				base.Close();
			}
			finally
			{
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Exit(Logging.WebSockets, this, "Close", string.Empty);
				}
			}
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x0006E3D8 File Offset: 0x0006C5D8
		public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (WebSocketBase.LoggingEnabled)
			{
				Logging.Enter(Logging.WebSockets, this, "ReadAsync", WebSocketHelpers.GetTraceMsgForParameters(offset, count, cancellationToken));
			}
			CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
			int bytesRead = 0;
			try
			{
				if (cancellationToken.CanBeCanceled)
				{
					cancellationTokenRegistration = cancellationToken.Register(WebSocketConnectionStream.s_OnCancel, this, false);
				}
				WebSocketHelpers.ThrowIfConnectionAborted(this.m_ConnectStream.Connection, true);
				int num = await base.ReadAsync(buffer, offset, count, cancellationToken).SuppressContextFlow<int>();
				bytesRead = num;
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Dump(Logging.WebSockets, this, "ReadAsync", buffer, offset, bytesRead);
				}
			}
			catch (Exception ex)
			{
				if (WebSocketConnectionStream.s_CanHandleException(ex))
				{
					cancellationToken.ThrowIfCancellationRequested();
				}
				throw;
			}
			finally
			{
				cancellationTokenRegistration.Dispose();
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Exit(Logging.WebSockets, this, "ReadAsync", bytesRead);
				}
			}
			return bytesRead;
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x0006E43C File Offset: 0x0006C63C
		public override async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			if (WebSocketBase.LoggingEnabled)
			{
				Logging.Enter(Logging.WebSockets, this, "WriteAsync", WebSocketHelpers.GetTraceMsgForParameters(offset, count, cancellationToken));
			}
			CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
			try
			{
				if (cancellationToken.CanBeCanceled)
				{
					cancellationTokenRegistration = cancellationToken.Register(WebSocketConnectionStream.s_OnCancel, this, false);
				}
				WebSocketHelpers.ThrowIfConnectionAborted(this.m_ConnectStream.Connection, false);
				await base.WriteAsync(buffer, offset, count, cancellationToken).SuppressContextFlow();
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Dump(Logging.WebSockets, this, "WriteAsync", buffer, offset, count);
				}
			}
			catch (Exception ex)
			{
				if (WebSocketConnectionStream.s_CanHandleException(ex))
				{
					cancellationToken.ThrowIfCancellationRequested();
				}
				throw;
			}
			finally
			{
				cancellationTokenRegistration.Dispose();
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Exit(Logging.WebSockets, this, "WriteAsync", string.Empty);
				}
			}
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x0006E4A0 File Offset: 0x0006C6A0
		public void SwitchToOpaqueMode(WebSocketBase webSocket)
		{
			if (this.m_InOpaqueMode)
			{
				throw new InvalidOperationException();
			}
			this.m_WebSocketConnection = base.BaseStream as WebSocketConnectionStream.WebSocketConnection;
			if (this.m_WebSocketConnection != null && this.m_IsFastPathAllowed)
			{
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Associate(Logging.WebSockets, this, this.m_WebSocketConnection);
				}
				this.m_WebSocketConnection.SwitchToOpaqueMode(webSocket);
				this.m_InOpaqueMode = true;
			}
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x0006E508 File Offset: 0x0006C708
		public async Task MultipleWriteAsync(IList<ArraySegment<byte>> sendBuffers, CancellationToken cancellationToken)
		{
			if (WebSocketBase.LoggingEnabled)
			{
				Logging.Enter(Logging.WebSockets, this, "MultipleWriteAsync", string.Empty);
			}
			CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
			try
			{
				if (cancellationToken.CanBeCanceled)
				{
					cancellationTokenRegistration = cancellationToken.Register(WebSocketConnectionStream.s_OnCancel, this, false);
				}
				WebSocketHelpers.ThrowIfConnectionAborted(this.m_ConnectStream.Connection, false);
				await ((WebSocketBase.IWebSocketStream)base.BaseStream).MultipleWriteAsync(sendBuffers, cancellationToken).SuppressContextFlow();
				if (WebSocketBase.LoggingEnabled)
				{
					foreach (ArraySegment<byte> arraySegment in sendBuffers)
					{
						Logging.Dump(Logging.WebSockets, this, "MultipleWriteAsync", arraySegment.Array, arraySegment.Offset, arraySegment.Count);
					}
				}
			}
			catch (Exception ex)
			{
				if (WebSocketConnectionStream.s_CanHandleException(ex))
				{
					cancellationToken.ThrowIfCancellationRequested();
				}
				throw;
			}
			finally
			{
				cancellationTokenRegistration.Dispose();
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Exit(Logging.WebSockets, this, "MultipleWriteAsync", string.Empty);
				}
			}
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x0006E55B File Offset: 0x0006C75B
		private static bool CanHandleException(Exception error)
		{
			return error is SocketException || error is ObjectDisposedException || error is WebException || error is IOException;
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x0006E580 File Offset: 0x0006C780
		private static void OnCancel(object state)
		{
			WebSocketConnectionStream webSocketConnectionStream = state as WebSocketConnectionStream;
			if (WebSocketBase.LoggingEnabled)
			{
				Logging.Enter(Logging.WebSockets, state, "OnCancel", string.Empty);
			}
			try
			{
				object closeConnectStreamLock = webSocketConnectionStream.m_CloseConnectStreamLock;
				lock (closeConnectStreamLock)
				{
					webSocketConnectionStream.m_ConnectStream.Connection.NetworkStream.InternalAbortSocket();
					((ICloseEx)webSocketConnectionStream.m_ConnectStream).CloseEx(CloseExState.Abort);
				}
				webSocketConnectionStream.CancelWebSocketConnection();
			}
			catch
			{
			}
			finally
			{
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Exit(Logging.WebSockets, state, "OnCancel", string.Empty);
				}
			}
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x0006E640 File Offset: 0x0006C840
		private void CancelWebSocketConnection()
		{
			if (this.m_InOpaqueMode)
			{
				WebSocketConnectionStream.WebSocketConnection webSocketConnection = (WebSocketConnectionStream.WebSocketConnection)base.BaseStream;
				WebSocketConnectionStream.s_OnCancelWebSocketConnection(webSocketConnection);
			}
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x0006E66C File Offset: 0x0006C86C
		public void Abort()
		{
			WebSocketConnectionStream.OnCancel(this);
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x0006E674 File Offset: 0x0006C874
		private void ConsumeConnectStreamBuffer(ConnectStream connectStream)
		{
			if (connectStream.Eof)
			{
				return;
			}
			byte[] array = new byte[1024];
			int num = 0;
			int num2 = array.Length;
			int num3;
			while ((num3 = connectStream.FillFromBufferedData(array, ref num, ref num2)) > 0)
			{
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Dump(Logging.WebSockets, this, "ConsumeConnectStreamBuffer", array, 0, num3);
				}
				base.Append(array, 0, num3);
				num = 0;
				num2 = array.Length;
			}
		}

		// Token: 0x0400167F RID: 5759
		private static readonly Func<Exception, bool> s_CanHandleException = new Func<Exception, bool>(WebSocketConnectionStream.CanHandleException);

		// Token: 0x04001680 RID: 5760
		private static readonly Action<object> s_OnCancel = new Action<object>(WebSocketConnectionStream.OnCancel);

		// Token: 0x04001681 RID: 5761
		private static readonly Action<object> s_OnCancelWebSocketConnection = new Action<object>(WebSocketConnectionStream.WebSocketConnection.OnCancel);

		// Token: 0x04001682 RID: 5762
		private static readonly Type s_NetworkStreamType = typeof(NetworkStream);

		// Token: 0x04001683 RID: 5763
		private readonly ConnectStream m_ConnectStream;

		// Token: 0x04001684 RID: 5764
		private readonly string m_ConnectionGroupName;

		// Token: 0x04001685 RID: 5765
		private readonly bool m_IsFastPathAllowed;

		// Token: 0x04001686 RID: 5766
		private readonly object m_CloseConnectStreamLock;

		// Token: 0x04001687 RID: 5767
		private bool m_InOpaqueMode;

		// Token: 0x04001688 RID: 5768
		private WebSocketConnectionStream.WebSocketConnection m_WebSocketConnection;

		// Token: 0x02000779 RID: 1913
		private static class Methods
		{
			// Token: 0x040032AA RID: 12970
			public const string Close = "Close";

			// Token: 0x040032AB RID: 12971
			public const string CloseNetworkConnectionAsync = "CloseNetworkConnectionAsync";

			// Token: 0x040032AC RID: 12972
			public const string OnCancel = "OnCancel";

			// Token: 0x040032AD RID: 12973
			public const string ReadAsync = "ReadAsync";

			// Token: 0x040032AE RID: 12974
			public const string WriteAsync = "WriteAsync";

			// Token: 0x040032AF RID: 12975
			public const string MultipleWriteAsync = "MultipleWriteAsync";
		}

		// Token: 0x0200077A RID: 1914
		private class WebSocketConnection : DelegatedStream, WebSocketBase.IWebSocketStream
		{
			// Token: 0x06004267 RID: 16999 RVA: 0x00114B4E File Offset: 0x00112D4E
			internal WebSocketConnection(Connection connection)
				: base(connection)
			{
				this.m_InnerStream = connection;
				this.m_InOpaqueMode = false;
				this.m_SupportsMultipleWrites = connection.NetworkStream.GetType().Assembly == WebSocketConnectionStream.s_NetworkStreamType.Assembly;
			}

			// Token: 0x17000F27 RID: 3879
			// (get) Token: 0x06004268 RID: 17000 RVA: 0x00114B8A File Offset: 0x00112D8A
			internal Socket InnerSocket
			{
				get
				{
					return this.GetInnerSocket(false);
				}
			}

			// Token: 0x17000F28 RID: 3880
			// (get) Token: 0x06004269 RID: 17001 RVA: 0x00114B93 File Offset: 0x00112D93
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000F29 RID: 3881
			// (get) Token: 0x0600426A RID: 17002 RVA: 0x00114B96 File Offset: 0x00112D96
			public override bool CanRead
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000F2A RID: 3882
			// (get) Token: 0x0600426B RID: 17003 RVA: 0x00114B99 File Offset: 0x00112D99
			public override bool CanWrite
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000F2B RID: 3883
			// (get) Token: 0x0600426C RID: 17004 RVA: 0x00114B9C File Offset: 0x00112D9C
			public bool SupportsMultipleWrite
			{
				get
				{
					return this.m_SupportsMultipleWrites;
				}
			}

			// Token: 0x0600426D RID: 17005 RVA: 0x00114BA4 File Offset: 0x00112DA4
			public Task CloseNetworkConnectionAsync(CancellationToken cancellationToken)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600426E RID: 17006 RVA: 0x00114BAC File Offset: 0x00112DAC
			public override void Close()
			{
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Enter(Logging.WebSockets, this, "Close", string.Empty);
				}
				try
				{
					base.Close();
					if (Interlocked.Increment(ref this.m_CleanedUp) == 1)
					{
						if (this.m_WriteEventArgs != null)
						{
							this.m_WriteEventArgs.Completed -= WebSocketConnectionStream.WebSocketConnection.s_OnWriteCompleted;
							this.m_WriteEventArgs.Dispose();
						}
						if (this.m_ReadEventArgs != null)
						{
							this.m_ReadEventArgs.Completed -= WebSocketConnectionStream.WebSocketConnection.s_OnReadCompleted;
							this.m_ReadEventArgs.Dispose();
						}
					}
				}
				finally
				{
					if (WebSocketBase.LoggingEnabled)
					{
						Logging.Exit(Logging.WebSockets, this, "Close", string.Empty);
					}
				}
			}

			// Token: 0x0600426F RID: 17007 RVA: 0x00114C60 File Offset: 0x00112E60
			internal Socket GetInnerSocket(bool skipStateCheck)
			{
				if (!skipStateCheck)
				{
					this.m_WebSocket.ThrowIfClosedOrAborted();
				}
				Socket internalSocket;
				try
				{
					internalSocket = this.m_InnerStream.NetworkStream.InternalSocket;
				}
				catch (ObjectDisposedException)
				{
					this.m_WebSocket.ThrowIfClosedOrAborted();
					throw;
				}
				return internalSocket;
			}

			// Token: 0x06004270 RID: 17008 RVA: 0x00114CB0 File Offset: 0x00112EB0
			private static IAsyncResult BeginMultipleWrite(IList<ArraySegment<byte>> sendBuffers, AsyncCallback callback, object asyncState)
			{
				WebSocketConnectionStream.WebSocketConnection webSocketConnection = asyncState as WebSocketConnectionStream.WebSocketConnection;
				BufferOffsetSize[] array = new BufferOffsetSize[sendBuffers.Count];
				for (int i = 0; i < sendBuffers.Count; i++)
				{
					ArraySegment<byte> arraySegment = sendBuffers[i];
					array[i] = new BufferOffsetSize(arraySegment.Array, arraySegment.Offset, arraySegment.Count, false);
				}
				WebSocketHelpers.ThrowIfConnectionAborted(webSocketConnection.m_InnerStream, false);
				return webSocketConnection.m_InnerStream.NetworkStream.BeginMultipleWrite(array, callback, asyncState);
			}

			// Token: 0x06004271 RID: 17009 RVA: 0x00114D28 File Offset: 0x00112F28
			private static void EndMultipleWrite(IAsyncResult asyncResult)
			{
				WebSocketConnectionStream.WebSocketConnection webSocketConnection = asyncResult.AsyncState as WebSocketConnectionStream.WebSocketConnection;
				WebSocketHelpers.ThrowIfConnectionAborted(webSocketConnection.m_InnerStream, false);
				webSocketConnection.m_InnerStream.NetworkStream.EndMultipleWrite(asyncResult);
			}

			// Token: 0x06004272 RID: 17010 RVA: 0x00114D60 File Offset: 0x00112F60
			public Task MultipleWriteAsync(IList<ArraySegment<byte>> sendBuffers, CancellationToken cancellationToken)
			{
				if (!this.m_InOpaqueMode)
				{
					return Task.Factory.FromAsync<IList<ArraySegment<byte>>>(WebSocketConnectionStream.WebSocketConnection.s_BeginMultipleWrite, WebSocketConnectionStream.WebSocketConnection.s_EndMultipleWrite, sendBuffers, this);
				}
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Enter(Logging.WebSockets, this, "MultipleWriteAsync", string.Empty);
				}
				bool flag = false;
				Task task;
				try
				{
					cancellationToken.ThrowIfCancellationRequested();
					WebSocketHelpers.ThrowIfConnectionAborted(this.m_InnerStream, false);
					this.m_WriteTaskCompletionSource = new TaskCompletionSource<object>();
					this.m_WriteEventArgs.SetBuffer(null, 0, 0);
					this.m_WriteEventArgs.BufferList = sendBuffers;
					flag = this.InnerSocket.SendAsync(this.m_WriteEventArgs);
					if (!flag)
					{
						if (this.m_WriteEventArgs.SocketError != SocketError.Success)
						{
							throw new SocketException(this.m_WriteEventArgs.SocketError);
						}
						task = Task.CompletedTask;
					}
					else
					{
						task = this.m_WriteTaskCompletionSource.Task;
					}
				}
				finally
				{
					if (WebSocketBase.LoggingEnabled)
					{
						Logging.Exit(Logging.WebSockets, this, "MultipleWriteAsync", flag);
					}
				}
				return task;
			}

			// Token: 0x06004273 RID: 17011 RVA: 0x00114E5C File Offset: 0x0011305C
			public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
			{
				WebSocketHelpers.ValidateBuffer(buffer, offset, count);
				if (!this.m_InOpaqueMode)
				{
					return base.WriteAsync(buffer, offset, count, cancellationToken);
				}
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Enter(Logging.WebSockets, this, "WriteAsync", WebSocketHelpers.GetTraceMsgForParameters(offset, count, cancellationToken));
				}
				bool flag = false;
				Task task;
				try
				{
					cancellationToken.ThrowIfCancellationRequested();
					WebSocketHelpers.ThrowIfConnectionAborted(this.m_InnerStream, false);
					this.m_WriteTaskCompletionSource = new TaskCompletionSource<object>();
					this.m_WriteEventArgs.BufferList = null;
					this.m_WriteEventArgs.SetBuffer(buffer, offset, count);
					flag = this.InnerSocket.SendAsync(this.m_WriteEventArgs);
					if (!flag)
					{
						if (this.m_WriteEventArgs.SocketError != SocketError.Success)
						{
							throw new SocketException(this.m_WriteEventArgs.SocketError);
						}
						task = Task.CompletedTask;
					}
					else
					{
						task = this.m_WriteTaskCompletionSource.Task;
					}
				}
				finally
				{
					if (WebSocketBase.LoggingEnabled)
					{
						Logging.Exit(Logging.WebSockets, this, "WriteAsync", flag);
					}
				}
				return task;
			}

			// Token: 0x06004274 RID: 17012 RVA: 0x00114F58 File Offset: 0x00113158
			public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
			{
				WebSocketHelpers.ValidateBuffer(buffer, offset, count);
				if (!this.m_InOpaqueMode)
				{
					return base.ReadAsync(buffer, offset, count, cancellationToken);
				}
				return this.ReadAsyncCore(buffer, offset, count, cancellationToken, false);
			}

			// Token: 0x06004275 RID: 17013 RVA: 0x00114F84 File Offset: 0x00113184
			internal Task<int> ReadAsyncCore(byte[] buffer, int offset, int count, CancellationToken cancellationToken, bool ignoreReadError)
			{
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Enter(Logging.WebSockets, this, "ReadAsyncCore", WebSocketHelpers.GetTraceMsgForParameters(offset, count, cancellationToken));
				}
				bool flag = false;
				this.m_IgnoreReadError = ignoreReadError;
				Task<int> task;
				try
				{
					cancellationToken.ThrowIfCancellationRequested();
					WebSocketHelpers.ThrowIfConnectionAborted(this.m_InnerStream, true);
					this.m_ReadTaskCompletionSource = new TaskCompletionSource<int>();
					this.m_ReadEventArgs.SetBuffer(buffer, offset, count);
					Socket socket;
					if (ignoreReadError)
					{
						socket = this.GetInnerSocket(true);
					}
					else
					{
						socket = this.InnerSocket;
					}
					flag = socket.ReceiveAsync(this.m_ReadEventArgs);
					if (!flag)
					{
						if (this.m_ReadEventArgs.SocketError != SocketError.Success)
						{
							if (!this.m_IgnoreReadError)
							{
								throw new SocketException(this.m_ReadEventArgs.SocketError);
							}
							task = Task.FromResult<int>(0);
						}
						else
						{
							task = Task.FromResult<int>(this.m_ReadEventArgs.BytesTransferred);
						}
					}
					else
					{
						task = this.m_ReadTaskCompletionSource.Task;
					}
				}
				finally
				{
					if (WebSocketBase.LoggingEnabled)
					{
						Logging.Exit(Logging.WebSockets, this, "ReadAsyncCore", flag);
					}
				}
				return task;
			}

			// Token: 0x06004276 RID: 17014 RVA: 0x0011508C File Offset: 0x0011328C
			public override Task FlushAsync(CancellationToken cancellationToken)
			{
				if (!this.m_InOpaqueMode)
				{
					return base.FlushAsync(cancellationToken);
				}
				cancellationToken.ThrowIfCancellationRequested();
				return Task.CompletedTask;
			}

			// Token: 0x06004277 RID: 17015 RVA: 0x001150AA File Offset: 0x001132AA
			public void Abort()
			{
			}

			// Token: 0x06004278 RID: 17016 RVA: 0x001150AC File Offset: 0x001132AC
			internal static void OnCancel(object state)
			{
				WebSocketConnectionStream.WebSocketConnection webSocketConnection = state as WebSocketConnectionStream.WebSocketConnection;
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Enter(Logging.WebSockets, webSocketConnection, "OnCancel", string.Empty);
				}
				try
				{
					TaskCompletionSource<int> readTaskCompletionSource = webSocketConnection.m_ReadTaskCompletionSource;
					if (readTaskCompletionSource != null)
					{
						readTaskCompletionSource.TrySetCanceled();
					}
					TaskCompletionSource<object> writeTaskCompletionSource = webSocketConnection.m_WriteTaskCompletionSource;
					if (writeTaskCompletionSource != null)
					{
						writeTaskCompletionSource.TrySetCanceled();
					}
				}
				finally
				{
					if (WebSocketBase.LoggingEnabled)
					{
						Logging.Exit(Logging.WebSockets, webSocketConnection, "OnCancel", string.Empty);
					}
				}
			}

			// Token: 0x06004279 RID: 17017 RVA: 0x00115130 File Offset: 0x00113330
			public void SwitchToOpaqueMode(WebSocketBase webSocket)
			{
				this.m_WebSocket = webSocket;
				this.m_InOpaqueMode = true;
				this.m_ReadEventArgs = new SocketAsyncEventArgs();
				this.m_ReadEventArgs.UserToken = this;
				this.m_ReadEventArgs.Completed += WebSocketConnectionStream.WebSocketConnection.s_OnReadCompleted;
				this.m_WriteEventArgs = new SocketAsyncEventArgs();
				this.m_WriteEventArgs.UserToken = this;
				this.m_WriteEventArgs.Completed += WebSocketConnectionStream.WebSocketConnection.s_OnWriteCompleted;
			}

			// Token: 0x0600427A RID: 17018 RVA: 0x00115199 File Offset: 0x00113399
			private static string GetIOCompletionTraceMsg(SocketAsyncEventArgs eventArgs)
			{
				return string.Format(CultureInfo.InvariantCulture, "LastOperation: {0}, SocketError: {1}", new object[] { eventArgs.LastOperation, eventArgs.SocketError });
			}

			// Token: 0x0600427B RID: 17019 RVA: 0x001151CC File Offset: 0x001133CC
			private static void OnWriteCompleted(object sender, SocketAsyncEventArgs eventArgs)
			{
				WebSocketConnectionStream.WebSocketConnection webSocketConnection = eventArgs.UserToken as WebSocketConnectionStream.WebSocketConnection;
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Enter(Logging.WebSockets, webSocketConnection, "OnWriteCompleted", WebSocketConnectionStream.WebSocketConnection.GetIOCompletionTraceMsg(eventArgs));
				}
				if (eventArgs.SocketError != SocketError.Success)
				{
					webSocketConnection.m_WriteTaskCompletionSource.TrySetException(new SocketException(eventArgs.SocketError));
				}
				else
				{
					webSocketConnection.m_WriteTaskCompletionSource.TrySetResult(null);
				}
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Exit(Logging.WebSockets, webSocketConnection, "OnWriteCompleted", string.Empty);
				}
			}

			// Token: 0x0600427C RID: 17020 RVA: 0x0011524C File Offset: 0x0011344C
			private static void OnReadCompleted(object sender, SocketAsyncEventArgs eventArgs)
			{
				WebSocketConnectionStream.WebSocketConnection webSocketConnection = eventArgs.UserToken as WebSocketConnectionStream.WebSocketConnection;
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Enter(Logging.WebSockets, webSocketConnection, "OnReadCompleted", WebSocketConnectionStream.WebSocketConnection.GetIOCompletionTraceMsg(eventArgs));
				}
				if (eventArgs.SocketError != SocketError.Success)
				{
					if (!webSocketConnection.m_IgnoreReadError)
					{
						webSocketConnection.m_ReadTaskCompletionSource.TrySetException(new SocketException(eventArgs.SocketError));
					}
					else
					{
						webSocketConnection.m_ReadTaskCompletionSource.TrySetResult(0);
					}
				}
				else
				{
					webSocketConnection.m_ReadTaskCompletionSource.TrySetResult(eventArgs.BytesTransferred);
				}
				if (WebSocketBase.LoggingEnabled)
				{
					Logging.Exit(Logging.WebSockets, webSocketConnection, "OnReadCompleted", string.Empty);
				}
			}

			// Token: 0x040032B0 RID: 12976
			private static readonly EventHandler<SocketAsyncEventArgs> s_OnReadCompleted = new EventHandler<SocketAsyncEventArgs>(WebSocketConnectionStream.WebSocketConnection.OnReadCompleted);

			// Token: 0x040032B1 RID: 12977
			private static readonly EventHandler<SocketAsyncEventArgs> s_OnWriteCompleted = new EventHandler<SocketAsyncEventArgs>(WebSocketConnectionStream.WebSocketConnection.OnWriteCompleted);

			// Token: 0x040032B2 RID: 12978
			private static readonly Func<IList<ArraySegment<byte>>, AsyncCallback, object, IAsyncResult> s_BeginMultipleWrite = new Func<IList<ArraySegment<byte>>, AsyncCallback, object, IAsyncResult>(WebSocketConnectionStream.WebSocketConnection.BeginMultipleWrite);

			// Token: 0x040032B3 RID: 12979
			private static readonly Action<IAsyncResult> s_EndMultipleWrite = new Action<IAsyncResult>(WebSocketConnectionStream.WebSocketConnection.EndMultipleWrite);

			// Token: 0x040032B4 RID: 12980
			private readonly Connection m_InnerStream;

			// Token: 0x040032B5 RID: 12981
			private readonly bool m_SupportsMultipleWrites;

			// Token: 0x040032B6 RID: 12982
			private bool m_InOpaqueMode;

			// Token: 0x040032B7 RID: 12983
			private WebSocketBase m_WebSocket;

			// Token: 0x040032B8 RID: 12984
			private SocketAsyncEventArgs m_WriteEventArgs;

			// Token: 0x040032B9 RID: 12985
			private SocketAsyncEventArgs m_ReadEventArgs;

			// Token: 0x040032BA RID: 12986
			private TaskCompletionSource<object> m_WriteTaskCompletionSource;

			// Token: 0x040032BB RID: 12987
			private TaskCompletionSource<int> m_ReadTaskCompletionSource;

			// Token: 0x040032BC RID: 12988
			private int m_CleanedUp;

			// Token: 0x040032BD RID: 12989
			private bool m_IgnoreReadError;

			// Token: 0x0200091C RID: 2332
			private static class Methods
			{
				// Token: 0x04003D7B RID: 15739
				public const string Close = "Close";

				// Token: 0x04003D7C RID: 15740
				public const string OnCancel = "OnCancel";

				// Token: 0x04003D7D RID: 15741
				public const string OnReadCompleted = "OnReadCompleted";

				// Token: 0x04003D7E RID: 15742
				public const string OnWriteCompleted = "OnWriteCompleted";

				// Token: 0x04003D7F RID: 15743
				public const string ReadAsyncCore = "ReadAsyncCore";

				// Token: 0x04003D80 RID: 15744
				public const string WriteAsync = "WriteAsync";

				// Token: 0x04003D81 RID: 15745
				public const string MultipleWriteAsync = "MultipleWriteAsync";
			}
		}
	}
}
