using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.MailSettingsSectionGroup" /> class.</summary>
	// Token: 0x02000337 RID: 823
	public sealed class MailSettingsSectionGroup : ConfigurationSectionGroup
	{
		/// <summary>Gets the SMTP settings for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.SmtpSection" /> object that contains configuration information for the local computer.</returns>
		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06001D66 RID: 7526 RVA: 0x0008BB17 File Offset: 0x00089D17
		public SmtpSection Smtp
		{
			get
			{
				return (SmtpSection)base.Sections["smtp"];
			}
		}
	}
}
