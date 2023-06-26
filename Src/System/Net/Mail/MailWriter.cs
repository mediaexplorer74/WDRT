using System;
using System.Collections.Specialized;
using System.IO;
using System.Net.Mime;

namespace System.Net.Mail
{
	// Token: 0x02000271 RID: 625
	internal class MailWriter : BaseWriter
	{
		// Token: 0x0600177B RID: 6011 RVA: 0x00077D18 File Offset: 0x00075F18
		internal MailWriter(Stream stream)
			: base(stream, true)
		{
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x00077D24 File Offset: 0x00075F24
		internal override void WriteHeaders(NameValueCollection headers, bool allowUnicode)
		{
			if (headers == null)
			{
				throw new ArgumentNullException("headers");
			}
			foreach (object obj in headers)
			{
				string text = (string)obj;
				string[] values = headers.GetValues(text);
				foreach (string text2 in values)
				{
					base.WriteHeader(text, text2, allowUnicode);
				}
			}
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x00077DB0 File Offset: 0x00075FB0
		internal override void Close()
		{
			this.bufferBuilder.Append(BaseWriter.CRLF);
			base.Flush(null);
			this.stream.Close();
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x00077DD4 File Offset: 0x00075FD4
		protected override void OnClose(object sender, EventArgs args)
		{
			this.contentStream.Flush();
			this.contentStream = null;
		}
	}
}
