using System;

namespace System.Security.Util
{
	// Token: 0x02000377 RID: 887
	[Flags]
	[Serializable]
	internal enum QuickCacheEntryType
	{
		// Token: 0x040011C5 RID: 4549
		FullTrustZoneMyComputer = 16777216,
		// Token: 0x040011C6 RID: 4550
		FullTrustZoneIntranet = 33554432,
		// Token: 0x040011C7 RID: 4551
		FullTrustZoneInternet = 67108864,
		// Token: 0x040011C8 RID: 4552
		FullTrustZoneTrusted = 134217728,
		// Token: 0x040011C9 RID: 4553
		FullTrustZoneUntrusted = 268435456,
		// Token: 0x040011CA RID: 4554
		FullTrustAll = 536870912
	}
}
