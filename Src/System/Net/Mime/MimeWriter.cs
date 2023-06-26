using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x0200024D RID: 589
	internal class MimeWriter : BaseWriter
	{
		// Token: 0x0600164E RID: 5710 RVA: 0x00073B02 File Offset: 0x00071D02
		internal MimeWriter(Stream stream, string boundary)
			: base(stream, false)
		{
			if (boundary == null)
			{
				throw new ArgumentNullException("boundary");
			}
			this.boundaryBytes = Encoding.ASCII.GetBytes(boundary);
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x00073B34 File Offset: 0x00071D34
		internal override void WriteHeaders(NameValueCollection headers, bool allowUnicode)
		{
			if (headers == null)
			{
				throw new ArgumentNullException("headers");
			}
			foreach (object obj in headers)
			{
				string text = (string)obj;
				base.WriteHeader(text, headers[text], allowUnicode);
			}
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x00073BA0 File Offset: 0x00071DA0
		internal IAsyncResult BeginClose(AsyncCallback callback, object state)
		{
			MultiAsyncResult multiAsyncResult = new MultiAsyncResult(this, callback, state);
			this.Close(multiAsyncResult);
			multiAsyncResult.CompleteSequence();
			return multiAsyncResult;
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x00073BC4 File Offset: 0x00071DC4
		internal void EndClose(IAsyncResult result)
		{
			MultiAsyncResult.End(result);
			this.stream.Close();
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x00073BD8 File Offset: 0x00071DD8
		internal override void Close()
		{
			this.Close(null);
			this.stream.Close();
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x00073BEC File Offset: 0x00071DEC
		private void Close(MultiAsyncResult multiResult)
		{
			this.bufferBuilder.Append(BaseWriter.CRLF);
			this.bufferBuilder.Append(MimeWriter.DASHDASH);
			this.bufferBuilder.Append(this.boundaryBytes);
			this.bufferBuilder.Append(MimeWriter.DASHDASH);
			this.bufferBuilder.Append(BaseWriter.CRLF);
			base.Flush(multiResult);
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x00073C51 File Offset: 0x00071E51
		protected override void OnClose(object sender, EventArgs args)
		{
			if (this.contentStream != sender)
			{
				return;
			}
			this.contentStream.Flush();
			this.contentStream = null;
			this.writeBoundary = true;
			this.isInContent = false;
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x00073C80 File Offset: 0x00071E80
		protected override void CheckBoundary()
		{
			if (this.writeBoundary)
			{
				this.bufferBuilder.Append(BaseWriter.CRLF);
				this.bufferBuilder.Append(MimeWriter.DASHDASH);
				this.bufferBuilder.Append(this.boundaryBytes);
				this.bufferBuilder.Append(BaseWriter.CRLF);
				this.writeBoundary = false;
			}
		}

		// Token: 0x04001725 RID: 5925
		private static byte[] DASHDASH = new byte[] { 45, 45 };

		// Token: 0x04001726 RID: 5926
		private byte[] boundaryBytes;

		// Token: 0x04001727 RID: 5927
		private bool writeBoundary = true;
	}
}
