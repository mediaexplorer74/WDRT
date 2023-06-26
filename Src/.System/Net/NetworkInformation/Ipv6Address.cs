using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002D6 RID: 726
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct Ipv6Address
	{
		// Token: 0x04001A2B RID: 6699
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
		internal byte[] Goo;

		// Token: 0x04001A2C RID: 6700
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		internal byte[] Address;

		// Token: 0x04001A2D RID: 6701
		internal uint ScopeID;
	}
}
