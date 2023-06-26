using System;

namespace System.Net.Mail
{
	// Token: 0x02000296 RID: 662
	internal enum SupportedAuth
	{
		// Token: 0x04001873 RID: 6259
		None,
		// Token: 0x04001874 RID: 6260
		Login,
		// Token: 0x04001875 RID: 6261
		NTLM,
		// Token: 0x04001876 RID: 6262
		GSSAPI = 4,
		// Token: 0x04001877 RID: 6263
		WDigest = 8
	}
}
