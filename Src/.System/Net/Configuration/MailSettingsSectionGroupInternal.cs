using System;

namespace System.Net.Configuration
{
	// Token: 0x02000338 RID: 824
	internal sealed class MailSettingsSectionGroupInternal
	{
		// Token: 0x06001D67 RID: 7527 RVA: 0x0008BB2E File Offset: 0x00089D2E
		internal MailSettingsSectionGroupInternal()
		{
			this.smtp = SmtpSectionInternal.GetSection();
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06001D68 RID: 7528 RVA: 0x0008BB41 File Offset: 0x00089D41
		internal SmtpSectionInternal Smtp
		{
			get
			{
				return this.smtp;
			}
		}

		// Token: 0x06001D69 RID: 7529 RVA: 0x0008BB49 File Offset: 0x00089D49
		internal static MailSettingsSectionGroupInternal GetSection()
		{
			return new MailSettingsSectionGroupInternal();
		}

		// Token: 0x04001C37 RID: 7223
		private SmtpSectionInternal smtp;
	}
}
