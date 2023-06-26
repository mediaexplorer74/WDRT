using System;

namespace System.Net.Mail
{
	// Token: 0x02000293 RID: 659
	internal class SmtpReplyReader
	{
		// Token: 0x0600187D RID: 6269 RVA: 0x0007C8AD File Offset: 0x0007AAAD
		internal SmtpReplyReader(SmtpReplyReaderFactory reader)
		{
			this.reader = reader;
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x0007C8BC File Offset: 0x0007AABC
		internal IAsyncResult BeginReadLines(AsyncCallback callback, object state)
		{
			return this.reader.BeginReadLines(this, callback, state);
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x0007C8CC File Offset: 0x0007AACC
		internal IAsyncResult BeginReadLine(AsyncCallback callback, object state)
		{
			return this.reader.BeginReadLine(this, callback, state);
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x0007C8DC File Offset: 0x0007AADC
		public void Close()
		{
			this.reader.Close(this);
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x0007C8EA File Offset: 0x0007AAEA
		internal LineInfo[] EndReadLines(IAsyncResult result)
		{
			return this.reader.EndReadLines(result);
		}

		// Token: 0x06001882 RID: 6274 RVA: 0x0007C8F8 File Offset: 0x0007AAF8
		internal LineInfo EndReadLine(IAsyncResult result)
		{
			return this.reader.EndReadLine(result);
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x0007C906 File Offset: 0x0007AB06
		internal LineInfo[] ReadLines()
		{
			return this.reader.ReadLines(this);
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x0007C914 File Offset: 0x0007AB14
		internal LineInfo ReadLine()
		{
			return this.reader.ReadLine(this);
		}

		// Token: 0x04001851 RID: 6225
		private SmtpReplyReaderFactory reader;
	}
}
