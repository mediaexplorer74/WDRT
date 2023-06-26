using System;

namespace System.Net.Mail
{
	// Token: 0x02000287 RID: 647
	internal struct LineInfo
	{
		// Token: 0x0600181B RID: 6171 RVA: 0x0007AECD File Offset: 0x000790CD
		internal LineInfo(SmtpStatusCode statusCode, string line)
		{
			this.statusCode = statusCode;
			this.line = line;
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x0600181C RID: 6172 RVA: 0x0007AEDD File Offset: 0x000790DD
		internal string Line
		{
			get
			{
				return this.line;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x0600181D RID: 6173 RVA: 0x0007AEE5 File Offset: 0x000790E5
		internal SmtpStatusCode StatusCode
		{
			get
			{
				return this.statusCode;
			}
		}

		// Token: 0x04001822 RID: 6178
		private string line;

		// Token: 0x04001823 RID: 6179
		private SmtpStatusCode statusCode;
	}
}
