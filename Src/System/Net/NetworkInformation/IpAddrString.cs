using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002B9 RID: 697
	internal struct IpAddrString
	{
		// Token: 0x04001926 RID: 6438
		internal IntPtr Next;

		// Token: 0x04001927 RID: 6439
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
		internal string IpAddress;

		// Token: 0x04001928 RID: 6440
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
		internal string IpMask;

		// Token: 0x04001929 RID: 6441
		internal uint Context;
	}
}
