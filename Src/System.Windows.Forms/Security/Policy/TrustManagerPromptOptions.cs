using System;

namespace System.Security.Policy
{
	// Token: 0x02000103 RID: 259
	[Flags]
	internal enum TrustManagerPromptOptions
	{
		// Token: 0x04000449 RID: 1097
		None = 0,
		// Token: 0x0400044A RID: 1098
		StopApp = 1,
		// Token: 0x0400044B RID: 1099
		RequiresPermissions = 2,
		// Token: 0x0400044C RID: 1100
		WillHaveFullTrust = 4,
		// Token: 0x0400044D RID: 1101
		AddsShortcut = 8,
		// Token: 0x0400044E RID: 1102
		LocalNetworkSource = 16,
		// Token: 0x0400044F RID: 1103
		LocalComputerSource = 32,
		// Token: 0x04000450 RID: 1104
		InternetSource = 64,
		// Token: 0x04000451 RID: 1105
		TrustedSitesSource = 128,
		// Token: 0x04000452 RID: 1106
		UntrustedSitesSource = 256
	}
}
