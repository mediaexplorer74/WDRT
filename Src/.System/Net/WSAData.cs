using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x020001D6 RID: 470
	internal struct WSAData
	{
		// Token: 0x040014CC RID: 5324
		internal short wVersion;

		// Token: 0x040014CD RID: 5325
		internal short wHighVersion;

		// Token: 0x040014CE RID: 5326
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 257)]
		internal string szDescription;

		// Token: 0x040014CF RID: 5327
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
		internal string szSystemStatus;

		// Token: 0x040014D0 RID: 5328
		internal short iMaxSockets;

		// Token: 0x040014D1 RID: 5329
		internal short iMaxUdpDg;

		// Token: 0x040014D2 RID: 5330
		internal IntPtr lpVendorInfo;
	}
}
