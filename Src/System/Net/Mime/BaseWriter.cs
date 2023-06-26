using System;
using System.Collections.Specialized;
using System.IO;
using System.Net.Mail;

namespace System.Net.Mime
{
	// Token: 0x0200023E RID: 574
	internal abstract class BaseWriter
	{
		// Token: 0x060015AF RID: 5551 RVA: 0x000706AC File Offset: 0x0006E8AC
		protected BaseWriter(Stream stream, bool shouldEncodeLeadingDots)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.stream = stream;
			this.shouldEncodeLeadingDots = shouldEncodeLeadingDots;
			this.onCloseHandler = new EventHandler(this.OnClose);
			this.bufferBuilder = new BufferBuilder();
			this.lineLength = BaseWriter.DefaultLineLength;
		}

		// Token: 0x060015B0 RID: 5552
		internal abstract void WriteHeaders(NameValueCollection headers, bool allowUnicode);

		// Token: 0x060015B1 RID: 5553 RVA: 0x00070704 File Offset: 0x0006E904
		internal void WriteHeader(string name, string value, bool allowUnicode)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.isInContent)
			{
				throw new InvalidOperationException(SR.GetString("MailWriterIsInContent"));
			}
			this.CheckBoundary();
			this.bufferBuilder.Append(name);
			this.bufferBuilder.Append(": ");
			this.WriteAndFold(value, name.Length + 2, allowUnicode);
			this.bufferBuilder.Append(BaseWriter.CRLF);
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x00070788 File Offset: 0x0006E988
		private void WriteAndFold(string value, int charsAlreadyOnLine, bool allowUnicode)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < value.Length; i++)
			{
				if (MailBnfHelper.IsFWSAt(value, i))
				{
					i += 2;
					this.bufferBuilder.Append(value, num2, i - num2, allowUnicode);
					num2 = i;
					num = i;
					charsAlreadyOnLine = 0;
				}
				else if (i - num2 > this.lineLength - charsAlreadyOnLine && num != num2)
				{
					this.bufferBuilder.Append(value, num2, num - num2, allowUnicode);
					this.bufferBuilder.Append(BaseWriter.CRLF);
					num2 = num;
					charsAlreadyOnLine = 0;
				}
				else if (value[i] == MailBnfHelper.Space || value[i] == MailBnfHelper.Tab)
				{
					num = i;
				}
			}
			if (value.Length - num2 > 0)
			{
				this.bufferBuilder.Append(value, num2, value.Length - num2, allowUnicode);
			}
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x0007084F File Offset: 0x0006EA4F
		internal Stream GetContentStream()
		{
			return this.GetContentStream(null);
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x00070858 File Offset: 0x0006EA58
		private Stream GetContentStream(MultiAsyncResult multiResult)
		{
			if (this.isInContent)
			{
				throw new InvalidOperationException(SR.GetString("MailWriterIsInContent"));
			}
			this.isInContent = true;
			this.CheckBoundary();
			this.bufferBuilder.Append(BaseWriter.CRLF);
			this.Flush(multiResult);
			Stream stream = new EightBitStream(this.stream, this.shouldEncodeLeadingDots);
			ClosableStream closableStream = new ClosableStream(stream, this.onCloseHandler);
			this.contentStream = closableStream;
			return closableStream;
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x000708C8 File Offset: 0x0006EAC8
		internal IAsyncResult BeginGetContentStream(AsyncCallback callback, object state)
		{
			MultiAsyncResult multiAsyncResult = new MultiAsyncResult(this, callback, state);
			Stream stream = this.GetContentStream(multiAsyncResult);
			if (!(multiAsyncResult.Result is Exception))
			{
				multiAsyncResult.Result = stream;
			}
			multiAsyncResult.CompleteSequence();
			return multiAsyncResult;
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x00070904 File Offset: 0x0006EB04
		internal Stream EndGetContentStream(IAsyncResult result)
		{
			object obj = MultiAsyncResult.End(result);
			if (obj is Exception)
			{
				throw (Exception)obj;
			}
			return (Stream)obj;
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x00070930 File Offset: 0x0006EB30
		protected void Flush(MultiAsyncResult multiResult)
		{
			if (this.bufferBuilder.Length > 0)
			{
				if (multiResult != null)
				{
					multiResult.Enter();
					IAsyncResult asyncResult = this.stream.BeginWrite(this.bufferBuilder.GetBuffer(), 0, this.bufferBuilder.Length, BaseWriter.onWrite, multiResult);
					if (asyncResult.CompletedSynchronously)
					{
						this.stream.EndWrite(asyncResult);
						multiResult.Leave();
					}
				}
				else
				{
					this.stream.Write(this.bufferBuilder.GetBuffer(), 0, this.bufferBuilder.Length);
				}
				this.bufferBuilder.Reset();
			}
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x000709C8 File Offset: 0x0006EBC8
		protected static void OnWrite(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				MultiAsyncResult multiAsyncResult = (MultiAsyncResult)result.AsyncState;
				BaseWriter baseWriter = (BaseWriter)multiAsyncResult.Context;
				try
				{
					baseWriter.stream.EndWrite(result);
					multiAsyncResult.Leave();
				}
				catch (Exception ex)
				{
					multiAsyncResult.Leave(ex);
				}
			}
		}

		// Token: 0x060015B9 RID: 5561
		internal abstract void Close();

		// Token: 0x060015BA RID: 5562
		protected abstract void OnClose(object sender, EventArgs args);

		// Token: 0x060015BB RID: 5563 RVA: 0x00070A24 File Offset: 0x0006EC24
		protected virtual void CheckBoundary()
		{
		}

		// Token: 0x040016CB RID: 5835
		private static int DefaultLineLength = 76;

		// Token: 0x040016CC RID: 5836
		private static AsyncCallback onWrite = new AsyncCallback(BaseWriter.OnWrite);

		// Token: 0x040016CD RID: 5837
		protected static byte[] CRLF = new byte[] { 13, 10 };

		// Token: 0x040016CE RID: 5838
		protected BufferBuilder bufferBuilder;

		// Token: 0x040016CF RID: 5839
		protected Stream contentStream;

		// Token: 0x040016D0 RID: 5840
		protected bool isInContent;

		// Token: 0x040016D1 RID: 5841
		protected Stream stream;

		// Token: 0x040016D2 RID: 5842
		private int lineLength;

		// Token: 0x040016D3 RID: 5843
		private EventHandler onCloseHandler;

		// Token: 0x040016D4 RID: 5844
		private bool shouldEncodeLeadingDots;
	}
}
