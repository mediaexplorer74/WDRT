using System;
using System.Collections;
using System.IO;
using System.Text;

namespace System.Net.Mail
{
	// Token: 0x02000294 RID: 660
	internal class SmtpReplyReaderFactory
	{
		// Token: 0x06001885 RID: 6277 RVA: 0x0007C922 File Offset: 0x0007AB22
		internal SmtpReplyReaderFactory(Stream stream)
		{
			this.bufferedStream = new BufferedReadStream(stream);
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001886 RID: 6278 RVA: 0x0007C936 File Offset: 0x0007AB36
		internal SmtpReplyReader CurrentReader
		{
			get
			{
				return this.currentReader;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001887 RID: 6279 RVA: 0x0007C93E File Offset: 0x0007AB3E
		internal SmtpStatusCode StatusCode
		{
			get
			{
				return this.statusCode;
			}
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x0007C948 File Offset: 0x0007AB48
		internal IAsyncResult BeginReadLines(SmtpReplyReader caller, AsyncCallback callback, object state)
		{
			SmtpReplyReaderFactory.ReadLinesAsyncResult readLinesAsyncResult = new SmtpReplyReaderFactory.ReadLinesAsyncResult(this, callback, state);
			readLinesAsyncResult.Read(caller);
			return readLinesAsyncResult;
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x0007C968 File Offset: 0x0007AB68
		internal IAsyncResult BeginReadLine(SmtpReplyReader caller, AsyncCallback callback, object state)
		{
			SmtpReplyReaderFactory.ReadLinesAsyncResult readLinesAsyncResult = new SmtpReplyReaderFactory.ReadLinesAsyncResult(this, callback, state, true);
			readLinesAsyncResult.Read(caller);
			return readLinesAsyncResult;
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x0007C988 File Offset: 0x0007AB88
		internal void Close(SmtpReplyReader caller)
		{
			if (this.currentReader == caller)
			{
				if (this.readState != SmtpReplyReaderFactory.ReadState.Done)
				{
					if (this.byteBuffer == null)
					{
						this.byteBuffer = new byte[256];
					}
					while (this.Read(caller, this.byteBuffer, 0, this.byteBuffer.Length) != 0)
					{
					}
				}
				this.currentReader = null;
			}
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x0007C9DE File Offset: 0x0007ABDE
		internal LineInfo[] EndReadLines(IAsyncResult result)
		{
			return SmtpReplyReaderFactory.ReadLinesAsyncResult.End(result);
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x0007C9E8 File Offset: 0x0007ABE8
		internal LineInfo EndReadLine(IAsyncResult result)
		{
			LineInfo[] array = SmtpReplyReaderFactory.ReadLinesAsyncResult.End(result);
			if (array != null && array.Length != 0)
			{
				return array[0];
			}
			return default(LineInfo);
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x0007CA14 File Offset: 0x0007AC14
		internal SmtpReplyReader GetNextReplyReader()
		{
			if (this.currentReader != null)
			{
				this.currentReader.Close();
			}
			this.readState = SmtpReplyReaderFactory.ReadState.Status0;
			this.currentReader = new SmtpReplyReader(this);
			return this.currentReader;
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x0007CA44 File Offset: 0x0007AC44
		private unsafe int ProcessRead(byte[] buffer, int offset, int read, bool readLine)
		{
			if (read == 0)
			{
				throw new IOException(SR.GetString("net_io_readfailure", new object[] { "net_io_connectionclosed" }));
			}
			byte* ptr;
			if (buffer == null || buffer.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &buffer[0];
			}
			byte* ptr2 = ptr + offset;
			byte* ptr3 = ptr2;
			byte* ptr4 = ptr3 + read;
			switch (this.readState)
			{
			case SmtpReplyReaderFactory.ReadState.Status0:
				goto IL_7C;
			case SmtpReplyReaderFactory.ReadState.Status1:
				goto IL_C1;
			case SmtpReplyReaderFactory.ReadState.Status2:
				goto IL_10D;
			case SmtpReplyReaderFactory.ReadState.ContinueFlag:
				goto IL_156;
			case SmtpReplyReaderFactory.ReadState.ContinueCR:
				break;
			case SmtpReplyReaderFactory.ReadState.ContinueLF:
				goto IL_1A9;
			case SmtpReplyReaderFactory.ReadState.LastCR:
				goto IL_1F1;
			case SmtpReplyReaderFactory.ReadState.LastLF:
				goto IL_1FF;
			case SmtpReplyReaderFactory.ReadState.Done:
				goto IL_227;
			default:
				goto IL_23A;
			}
			IL_198:
			while (ptr3 < ptr4)
			{
				if (*(ptr3++) == 13)
				{
					goto IL_1A9;
				}
			}
			this.readState = SmtpReplyReaderFactory.ReadState.ContinueCR;
			goto IL_23A;
			IL_1F1:
			while (ptr3 < ptr4)
			{
				if (*(ptr3++) == 13)
				{
					goto IL_1FF;
				}
			}
			this.readState = SmtpReplyReaderFactory.ReadState.LastCR;
			goto IL_23A;
			IL_7C:
			if (ptr3 >= ptr4)
			{
				this.readState = SmtpReplyReaderFactory.ReadState.Status0;
				goto IL_23A;
			}
			byte b = *(ptr3++);
			if (b < 48 && b > 57)
			{
				throw new FormatException(SR.GetString("SmtpInvalidResponse"));
			}
			this.statusCode = (SmtpStatusCode)(100 * (b - 48));
			IL_C1:
			if (ptr3 >= ptr4)
			{
				this.readState = SmtpReplyReaderFactory.ReadState.Status1;
				goto IL_23A;
			}
			byte b2 = *(ptr3++);
			if (b2 < 48 && b2 > 57)
			{
				throw new FormatException(SR.GetString("SmtpInvalidResponse"));
			}
			this.statusCode += (int)(10 * (b2 - 48));
			IL_10D:
			if (ptr3 >= ptr4)
			{
				this.readState = SmtpReplyReaderFactory.ReadState.Status2;
				goto IL_23A;
			}
			byte b3 = *(ptr3++);
			if (b3 < 48 && b3 > 57)
			{
				throw new FormatException(SR.GetString("SmtpInvalidResponse"));
			}
			this.statusCode += (int)(b3 - 48);
			IL_156:
			if (ptr3 >= ptr4)
			{
				this.readState = SmtpReplyReaderFactory.ReadState.ContinueFlag;
				goto IL_23A;
			}
			byte b4 = *(ptr3++);
			if (b4 == 32)
			{
				goto IL_1F1;
			}
			if (b4 != 45)
			{
				throw new FormatException(SR.GetString("SmtpInvalidResponse"));
			}
			goto IL_198;
			IL_1A9:
			if (ptr3 >= ptr4)
			{
				this.readState = SmtpReplyReaderFactory.ReadState.ContinueLF;
				goto IL_23A;
			}
			if (*(ptr3++) != 10)
			{
				throw new FormatException(SR.GetString("SmtpInvalidResponse"));
			}
			if (readLine)
			{
				this.readState = SmtpReplyReaderFactory.ReadState.Status0;
				return (int)((long)(ptr3 - ptr2));
			}
			goto IL_7C;
			IL_1FF:
			if (ptr3 >= ptr4)
			{
				this.readState = SmtpReplyReaderFactory.ReadState.LastLF;
				goto IL_23A;
			}
			if (*(ptr3++) != 10)
			{
				throw new FormatException(SR.GetString("SmtpInvalidResponse"));
			}
			IL_227:
			int num = (int)((long)(ptr3 - ptr2));
			this.readState = SmtpReplyReaderFactory.ReadState.Done;
			return num;
			IL_23A:
			return (int)((long)(ptr3 - ptr2));
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x0007CC94 File Offset: 0x0007AE94
		internal int Read(SmtpReplyReader caller, byte[] buffer, int offset, int count)
		{
			if (count == 0 || this.currentReader != caller || this.readState == SmtpReplyReaderFactory.ReadState.Done)
			{
				return 0;
			}
			int num = this.bufferedStream.Read(buffer, offset, count);
			int num2 = this.ProcessRead(buffer, offset, num, false);
			if (num2 < num)
			{
				this.bufferedStream.Push(buffer, offset + num2, num - num2);
			}
			return num2;
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x0007CCEC File Offset: 0x0007AEEC
		internal LineInfo ReadLine(SmtpReplyReader caller)
		{
			LineInfo[] array = this.ReadLines(caller, true);
			if (array != null && array.Length != 0)
			{
				return array[0];
			}
			return default(LineInfo);
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x0007CD1A File Offset: 0x0007AF1A
		internal LineInfo[] ReadLines(SmtpReplyReader caller)
		{
			return this.ReadLines(caller, false);
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x0007CD24 File Offset: 0x0007AF24
		internal LineInfo[] ReadLines(SmtpReplyReader caller, bool oneLine)
		{
			if (caller != this.currentReader || this.readState == SmtpReplyReaderFactory.ReadState.Done)
			{
				return new LineInfo[0];
			}
			if (this.byteBuffer == null)
			{
				this.byteBuffer = new byte[256];
			}
			StringBuilder stringBuilder = new StringBuilder();
			ArrayList arrayList = new ArrayList();
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			for (;;)
			{
				if (num2 == num3)
				{
					num3 = this.bufferedStream.Read(this.byteBuffer, 0, this.byteBuffer.Length);
					num2 = 0;
				}
				int num4 = this.ProcessRead(this.byteBuffer, num2, num3 - num2, true);
				if (num < 4)
				{
					int num5 = Math.Min(4 - num, num4);
					num += num5;
					num2 += num5;
					num4 -= num5;
					if (num4 == 0)
					{
						continue;
					}
				}
				stringBuilder.Append(Encoding.UTF8.GetString(this.byteBuffer, num2, num4));
				num2 += num4;
				if (this.readState == SmtpReplyReaderFactory.ReadState.Status0)
				{
					num = 0;
					arrayList.Add(new LineInfo(this.statusCode, stringBuilder.ToString(0, stringBuilder.Length - 2)));
					if (oneLine)
					{
						break;
					}
					stringBuilder = new StringBuilder();
				}
				else if (this.readState == SmtpReplyReaderFactory.ReadState.Done)
				{
					goto Block_7;
				}
			}
			this.bufferedStream.Push(this.byteBuffer, num2, num3 - num2);
			return (LineInfo[])arrayList.ToArray(typeof(LineInfo));
			Block_7:
			arrayList.Add(new LineInfo(this.statusCode, stringBuilder.ToString(0, stringBuilder.Length - 2)));
			this.bufferedStream.Push(this.byteBuffer, num2, num3 - num2);
			return (LineInfo[])arrayList.ToArray(typeof(LineInfo));
		}

		// Token: 0x04001852 RID: 6226
		private BufferedReadStream bufferedStream;

		// Token: 0x04001853 RID: 6227
		private byte[] byteBuffer;

		// Token: 0x04001854 RID: 6228
		private SmtpReplyReader currentReader;

		// Token: 0x04001855 RID: 6229
		private const int DefaultBufferSize = 256;

		// Token: 0x04001856 RID: 6230
		private SmtpReplyReaderFactory.ReadState readState;

		// Token: 0x04001857 RID: 6231
		private SmtpStatusCode statusCode;

		// Token: 0x020007A1 RID: 1953
		private enum ReadState
		{
			// Token: 0x040033A7 RID: 13223
			Status0,
			// Token: 0x040033A8 RID: 13224
			Status1,
			// Token: 0x040033A9 RID: 13225
			Status2,
			// Token: 0x040033AA RID: 13226
			ContinueFlag,
			// Token: 0x040033AB RID: 13227
			ContinueCR,
			// Token: 0x040033AC RID: 13228
			ContinueLF,
			// Token: 0x040033AD RID: 13229
			LastCR,
			// Token: 0x040033AE RID: 13230
			LastLF,
			// Token: 0x040033AF RID: 13231
			Done
		}

		// Token: 0x020007A2 RID: 1954
		private class ReadLinesAsyncResult : LazyAsyncResult
		{
			// Token: 0x060042E5 RID: 17125 RVA: 0x001184BF File Offset: 0x001166BF
			internal ReadLinesAsyncResult(SmtpReplyReaderFactory parent, AsyncCallback callback, object state)
				: base(null, state, callback)
			{
				this.parent = parent;
			}

			// Token: 0x060042E6 RID: 17126 RVA: 0x001184D1 File Offset: 0x001166D1
			internal ReadLinesAsyncResult(SmtpReplyReaderFactory parent, AsyncCallback callback, object state, bool oneLine)
				: base(null, state, callback)
			{
				this.oneLine = oneLine;
				this.parent = parent;
			}

			// Token: 0x060042E7 RID: 17127 RVA: 0x001184EC File Offset: 0x001166EC
			internal void Read(SmtpReplyReader caller)
			{
				if (this.parent.currentReader != caller || this.parent.readState == SmtpReplyReaderFactory.ReadState.Done)
				{
					base.InvokeCallback();
					return;
				}
				if (this.parent.byteBuffer == null)
				{
					this.parent.byteBuffer = new byte[256];
				}
				this.builder = new StringBuilder();
				this.lines = new ArrayList();
				this.Read();
			}

			// Token: 0x060042E8 RID: 17128 RVA: 0x0011855C File Offset: 0x0011675C
			internal static LineInfo[] End(IAsyncResult result)
			{
				SmtpReplyReaderFactory.ReadLinesAsyncResult readLinesAsyncResult = (SmtpReplyReaderFactory.ReadLinesAsyncResult)result;
				readLinesAsyncResult.InternalWaitForCompletion();
				return (LineInfo[])readLinesAsyncResult.lines.ToArray(typeof(LineInfo));
			}

			// Token: 0x060042E9 RID: 17129 RVA: 0x00118594 File Offset: 0x00116794
			private void Read()
			{
				for (;;)
				{
					IAsyncResult asyncResult = this.parent.bufferedStream.BeginRead(this.parent.byteBuffer, 0, this.parent.byteBuffer.Length, SmtpReplyReaderFactory.ReadLinesAsyncResult.readCallback, this);
					if (!asyncResult.CompletedSynchronously)
					{
						break;
					}
					this.read = this.parent.bufferedStream.EndRead(asyncResult);
					if (!this.ProcessRead())
					{
						return;
					}
				}
			}

			// Token: 0x060042EA RID: 17130 RVA: 0x001185FC File Offset: 0x001167FC
			private static void ReadCallback(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					Exception ex = null;
					SmtpReplyReaderFactory.ReadLinesAsyncResult readLinesAsyncResult = (SmtpReplyReaderFactory.ReadLinesAsyncResult)result.AsyncState;
					try
					{
						readLinesAsyncResult.read = readLinesAsyncResult.parent.bufferedStream.EndRead(result);
						if (readLinesAsyncResult.ProcessRead())
						{
							readLinesAsyncResult.Read();
						}
					}
					catch (Exception ex2)
					{
						ex = ex2;
					}
					if (ex != null)
					{
						readLinesAsyncResult.InvokeCallback(ex);
					}
				}
			}

			// Token: 0x060042EB RID: 17131 RVA: 0x00118668 File Offset: 0x00116868
			private bool ProcessRead()
			{
				if (this.read == 0)
				{
					throw new IOException(SR.GetString("net_io_readfailure", new object[] { "net_io_connectionclosed" }));
				}
				int num = 0;
				while (num != this.read)
				{
					int num2 = this.parent.ProcessRead(this.parent.byteBuffer, num, this.read - num, true);
					if (this.statusRead < 4)
					{
						int num3 = Math.Min(4 - this.statusRead, num2);
						this.statusRead += num3;
						num += num3;
						num2 -= num3;
						if (num2 == 0)
						{
							continue;
						}
					}
					this.builder.Append(Encoding.UTF8.GetString(this.parent.byteBuffer, num, num2));
					num += num2;
					if (this.parent.readState == SmtpReplyReaderFactory.ReadState.Status0)
					{
						this.lines.Add(new LineInfo(this.parent.statusCode, this.builder.ToString(0, this.builder.Length - 2)));
						this.builder = new StringBuilder();
						this.statusRead = 0;
						if (this.oneLine)
						{
							this.parent.bufferedStream.Push(this.parent.byteBuffer, num, this.read - num);
							base.InvokeCallback();
							return false;
						}
					}
					else if (this.parent.readState == SmtpReplyReaderFactory.ReadState.Done)
					{
						this.lines.Add(new LineInfo(this.parent.statusCode, this.builder.ToString(0, this.builder.Length - 2)));
						this.parent.bufferedStream.Push(this.parent.byteBuffer, num, this.read - num);
						base.InvokeCallback();
						return false;
					}
				}
				return true;
			}

			// Token: 0x040033B0 RID: 13232
			private StringBuilder builder;

			// Token: 0x040033B1 RID: 13233
			private ArrayList lines;

			// Token: 0x040033B2 RID: 13234
			private SmtpReplyReaderFactory parent;

			// Token: 0x040033B3 RID: 13235
			private static AsyncCallback readCallback = new AsyncCallback(SmtpReplyReaderFactory.ReadLinesAsyncResult.ReadCallback);

			// Token: 0x040033B4 RID: 13236
			private int read;

			// Token: 0x040033B5 RID: 13237
			private int statusRead;

			// Token: 0x040033B6 RID: 13238
			private bool oneLine;
		}
	}
}
