using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Threading;

namespace System.Diagnostics
{
	// Token: 0x020004BE RID: 1214
	internal class AsyncStreamReader : IDisposable
	{
		// Token: 0x06002D4C RID: 11596 RVA: 0x000CBF04 File Offset: 0x000CA104
		internal AsyncStreamReader(Process process, Stream stream, UserCallBack callback, Encoding encoding)
			: this(process, stream, callback, encoding, 1024)
		{
		}

		// Token: 0x06002D4D RID: 11597 RVA: 0x000CBF16 File Offset: 0x000CA116
		internal AsyncStreamReader(Process process, Stream stream, UserCallBack callback, Encoding encoding, int bufferSize)
		{
			this.Init(process, stream, callback, encoding, bufferSize);
			this.messageQueue = new Queue();
		}

		// Token: 0x06002D4E RID: 11598 RVA: 0x000CBF38 File Offset: 0x000CA138
		private void Init(Process process, Stream stream, UserCallBack callback, Encoding encoding, int bufferSize)
		{
			this.process = process;
			this.stream = stream;
			this.encoding = encoding;
			this.userCallBack = callback;
			this.decoder = encoding.GetDecoder();
			if (bufferSize < 128)
			{
				bufferSize = 128;
			}
			this.byteBuffer = new byte[bufferSize];
			this._maxCharsPerBuffer = encoding.GetMaxCharCount(bufferSize);
			this.charBuffer = new char[this._maxCharsPerBuffer];
			this.cancelOperation = false;
			this.eofEvent = new ManualResetEvent(false);
			this.sb = null;
			this.bLastCarriageReturn = false;
		}

		// Token: 0x06002D4F RID: 11599 RVA: 0x000CBFCD File Offset: 0x000CA1CD
		public virtual void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x06002D50 RID: 11600 RVA: 0x000CBFD6 File Offset: 0x000CA1D6
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002D51 RID: 11601 RVA: 0x000CBFE8 File Offset: 0x000CA1E8
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.stream != null)
			{
				this.stream.Close();
			}
			if (this.stream != null)
			{
				this.stream = null;
				this.encoding = null;
				this.decoder = null;
				this.byteBuffer = null;
				this.charBuffer = null;
			}
			if (this.eofEvent != null)
			{
				this.eofEvent.Close();
				this.eofEvent = null;
			}
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x06002D52 RID: 11602 RVA: 0x000CC050 File Offset: 0x000CA250
		public virtual Encoding CurrentEncoding
		{
			get
			{
				return this.encoding;
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x06002D53 RID: 11603 RVA: 0x000CC058 File Offset: 0x000CA258
		public virtual Stream BaseStream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x000CC060 File Offset: 0x000CA260
		internal void BeginReadLine()
		{
			if (this.cancelOperation)
			{
				this.cancelOperation = false;
			}
			if (this.sb == null)
			{
				this.sb = new StringBuilder(1024);
				this.stream.BeginRead(this.byteBuffer, 0, this.byteBuffer.Length, new AsyncCallback(this.ReadBuffer), null);
				return;
			}
			this.FlushMessageQueue();
		}

		// Token: 0x06002D55 RID: 11605 RVA: 0x000CC0C3 File Offset: 0x000CA2C3
		internal void CancelOperation()
		{
			this.cancelOperation = true;
		}

		// Token: 0x06002D56 RID: 11606 RVA: 0x000CC0CC File Offset: 0x000CA2CC
		private void ReadBuffer(IAsyncResult ar)
		{
			int num;
			try
			{
				num = this.stream.EndRead(ar);
			}
			catch (IOException)
			{
				num = 0;
			}
			catch (OperationCanceledException)
			{
				num = 0;
			}
			if (num == 0)
			{
				Queue queue = this.messageQueue;
				lock (queue)
				{
					if (this.sb.Length != 0)
					{
						this.messageQueue.Enqueue(this.sb.ToString());
						this.sb.Length = 0;
					}
					this.messageQueue.Enqueue(null);
				}
				try
				{
					this.FlushMessageQueue();
					return;
				}
				finally
				{
					this.eofEvent.Set();
				}
			}
			int chars = this.decoder.GetChars(this.byteBuffer, 0, num, this.charBuffer, 0);
			this.sb.Append(this.charBuffer, 0, chars);
			this.GetLinesFromStringBuilder();
			this.stream.BeginRead(this.byteBuffer, 0, this.byteBuffer.Length, new AsyncCallback(this.ReadBuffer), null);
		}

		// Token: 0x06002D57 RID: 11607 RVA: 0x000CC1F4 File Offset: 0x000CA3F4
		private void GetLinesFromStringBuilder()
		{
			int i = this.currentLinePos;
			int num = 0;
			int length = this.sb.Length;
			if (this.bLastCarriageReturn && length > 0 && this.sb[0] == '\n')
			{
				i = 1;
				num = 1;
				this.bLastCarriageReturn = false;
			}
			while (i < length)
			{
				char c = this.sb[i];
				if (c == '\r' || c == '\n')
				{
					string text = this.sb.ToString(num, i - num);
					num = i + 1;
					if (c == '\r' && num < length && this.sb[num] == '\n')
					{
						num++;
						i++;
					}
					Queue queue = this.messageQueue;
					lock (queue)
					{
						this.messageQueue.Enqueue(text);
					}
				}
				i++;
			}
			if (length > 0 && this.sb[length - 1] == '\r')
			{
				this.bLastCarriageReturn = true;
			}
			if (num < length)
			{
				if (num == 0)
				{
					this.currentLinePos = i;
				}
				else
				{
					this.sb.Remove(0, num);
					this.currentLinePos = 0;
				}
			}
			else
			{
				this.sb.Length = 0;
				this.currentLinePos = 0;
			}
			this.FlushMessageQueue();
		}

		// Token: 0x06002D58 RID: 11608 RVA: 0x000CC340 File Offset: 0x000CA540
		private void FlushMessageQueue()
		{
			while (this.messageQueue.Count > 0)
			{
				Queue queue = this.messageQueue;
				lock (queue)
				{
					if (this.messageQueue.Count > 0)
					{
						string text = (string)this.messageQueue.Dequeue();
						if (!this.cancelOperation)
						{
							this.userCallBack(text);
						}
					}
					continue;
				}
				break;
			}
		}

		// Token: 0x06002D59 RID: 11609 RVA: 0x000CC3BC File Offset: 0x000CA5BC
		internal void WaitUtilEOF()
		{
			if (this.eofEvent != null)
			{
				this.eofEvent.WaitOne();
				this.eofEvent.Close();
				this.eofEvent = null;
			}
		}

		// Token: 0x040026FE RID: 9982
		internal const int DefaultBufferSize = 1024;

		// Token: 0x040026FF RID: 9983
		private const int MinBufferSize = 128;

		// Token: 0x04002700 RID: 9984
		private Stream stream;

		// Token: 0x04002701 RID: 9985
		private Encoding encoding;

		// Token: 0x04002702 RID: 9986
		private Decoder decoder;

		// Token: 0x04002703 RID: 9987
		private byte[] byteBuffer;

		// Token: 0x04002704 RID: 9988
		private char[] charBuffer;

		// Token: 0x04002705 RID: 9989
		private int _maxCharsPerBuffer;

		// Token: 0x04002706 RID: 9990
		private Process process;

		// Token: 0x04002707 RID: 9991
		private UserCallBack userCallBack;

		// Token: 0x04002708 RID: 9992
		private bool cancelOperation;

		// Token: 0x04002709 RID: 9993
		private ManualResetEvent eofEvent;

		// Token: 0x0400270A RID: 9994
		private Queue messageQueue;

		// Token: 0x0400270B RID: 9995
		private StringBuilder sb;

		// Token: 0x0400270C RID: 9996
		private bool bLastCarriageReturn;

		// Token: 0x0400270D RID: 9997
		private int currentLinePos;
	}
}
