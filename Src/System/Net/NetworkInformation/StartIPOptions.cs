using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002DA RID: 730
	[Flags]
	internal enum StartIPOptions
	{
		// Token: 0x04001A34 RID: 6708
		Both = 3,
		// Token: 0x04001A35 RID: 6709
		None = 0,
		// Token: 0x04001A36 RID: 6710
		StartIPv4 = 1,
		// Token: 0x04001A37 RID: 6711
		StartIPv6 = 2
	}
}
