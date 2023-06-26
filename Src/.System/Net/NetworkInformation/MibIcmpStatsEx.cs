using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002C9 RID: 713
	internal struct MibIcmpStatsEx
	{
		// Token: 0x040019E5 RID: 6629
		internal uint dwMsgs;

		// Token: 0x040019E6 RID: 6630
		internal uint dwErrors;

		// Token: 0x040019E7 RID: 6631
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		internal uint[] rgdwTypeCount;
	}
}
