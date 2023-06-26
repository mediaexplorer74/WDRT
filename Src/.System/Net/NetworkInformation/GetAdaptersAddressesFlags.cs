using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002B8 RID: 696
	[Flags]
	internal enum GetAdaptersAddressesFlags
	{
		// Token: 0x0400191B RID: 6427
		SkipUnicast = 1,
		// Token: 0x0400191C RID: 6428
		SkipAnycast = 2,
		// Token: 0x0400191D RID: 6429
		SkipMulticast = 4,
		// Token: 0x0400191E RID: 6430
		SkipDnsServer = 8,
		// Token: 0x0400191F RID: 6431
		IncludePrefix = 16,
		// Token: 0x04001920 RID: 6432
		SkipFriendlyName = 32,
		// Token: 0x04001921 RID: 6433
		IncludeWins = 64,
		// Token: 0x04001922 RID: 6434
		IncludeGateways = 128,
		// Token: 0x04001923 RID: 6435
		IncludeAllInterfaces = 256,
		// Token: 0x04001924 RID: 6436
		IncludeAllCompartments = 512,
		// Token: 0x04001925 RID: 6437
		IncludeTunnelBindingOrder = 1024
	}
}
