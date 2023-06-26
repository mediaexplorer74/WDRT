using System;

namespace System.Net.Configuration
{
	// Token: 0x02000345 RID: 837
	internal sealed class SmtpNetworkElementInternal
	{
		// Token: 0x06001E09 RID: 7689 RVA: 0x0008D374 File Offset: 0x0008B574
		internal SmtpNetworkElementInternal(SmtpNetworkElement element)
		{
			this.host = element.Host;
			this.port = element.Port;
			this.targetname = element.TargetName;
			this.clientDomain = element.ClientDomain;
			this.enableSsl = element.EnableSsl;
			if (element.DefaultCredentials)
			{
				this.credential = (NetworkCredential)CredentialCache.DefaultCredentials;
				return;
			}
			if (element.UserName != null && element.UserName.Length > 0)
			{
				this.credential = new NetworkCredential(element.UserName, element.Password);
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06001E0A RID: 7690 RVA: 0x0008D409 File Offset: 0x0008B609
		internal NetworkCredential Credential
		{
			get
			{
				return this.credential;
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06001E0B RID: 7691 RVA: 0x0008D411 File Offset: 0x0008B611
		internal string Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06001E0C RID: 7692 RVA: 0x0008D419 File Offset: 0x0008B619
		internal string ClientDomain
		{
			get
			{
				return this.clientDomain;
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06001E0D RID: 7693 RVA: 0x0008D421 File Offset: 0x0008B621
		internal int Port
		{
			get
			{
				return this.port;
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06001E0E RID: 7694 RVA: 0x0008D429 File Offset: 0x0008B629
		internal string TargetName
		{
			get
			{
				return this.targetname;
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06001E0F RID: 7695 RVA: 0x0008D431 File Offset: 0x0008B631
		internal bool EnableSsl
		{
			get
			{
				return this.enableSsl;
			}
		}

		// Token: 0x04001C93 RID: 7315
		private string targetname;

		// Token: 0x04001C94 RID: 7316
		private string host;

		// Token: 0x04001C95 RID: 7317
		private string clientDomain;

		// Token: 0x04001C96 RID: 7318
		private int port;

		// Token: 0x04001C97 RID: 7319
		private NetworkCredential credential;

		// Token: 0x04001C98 RID: 7320
		private bool enableSsl;
	}
}
