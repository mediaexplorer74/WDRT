using System;

namespace System.Net
{
	// Token: 0x020001B0 RID: 432
	internal enum FtpLoginState : byte
	{
		// Token: 0x040013DF RID: 5087
		NotLoggedIn,
		// Token: 0x040013E0 RID: 5088
		LoggedIn,
		// Token: 0x040013E1 RID: 5089
		LoggedInButNeedsRelogin,
		// Token: 0x040013E2 RID: 5090
		ReloginFailed
	}
}
