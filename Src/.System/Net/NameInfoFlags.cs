using System;

namespace System.Net
{
	// Token: 0x020001D9 RID: 473
	[Flags]
	internal enum NameInfoFlags
	{
		// Token: 0x040014E1 RID: 5345
		NI_NOFQDN = 1,
		// Token: 0x040014E2 RID: 5346
		NI_NUMERICHOST = 2,
		// Token: 0x040014E3 RID: 5347
		NI_NAMEREQD = 4,
		// Token: 0x040014E4 RID: 5348
		NI_NUMERICSERV = 8,
		// Token: 0x040014E5 RID: 5349
		NI_DGRAM = 16
	}
}
