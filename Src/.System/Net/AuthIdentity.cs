using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x020001D0 RID: 464
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	internal struct AuthIdentity
	{
		// Token: 0x06001261 RID: 4705 RVA: 0x000624F0 File Offset: 0x000606F0
		internal AuthIdentity(string userName, string password, string domain)
		{
			this.UserName = userName;
			this.UserNameLength = ((userName == null) ? 0 : userName.Length);
			this.Password = password;
			this.PasswordLength = ((password == null) ? 0 : password.Length);
			this.Domain = domain;
			this.DomainLength = ((domain == null) ? 0 : domain.Length);
			this.Flags = 2;
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x0006254F File Offset: 0x0006074F
		public override string ToString()
		{
			return ValidationHelper.ToString(this.Domain) + "\\" + ValidationHelper.ToString(this.UserName);
		}

		// Token: 0x040014B7 RID: 5303
		internal string UserName;

		// Token: 0x040014B8 RID: 5304
		internal int UserNameLength;

		// Token: 0x040014B9 RID: 5305
		internal string Domain;

		// Token: 0x040014BA RID: 5306
		internal int DomainLength;

		// Token: 0x040014BB RID: 5307
		internal string Password;

		// Token: 0x040014BC RID: 5308
		internal int PasswordLength;

		// Token: 0x040014BD RID: 5309
		internal int Flags;
	}
}
