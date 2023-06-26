using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x020001DA RID: 474
	internal struct IPv6MulticastRequest
	{
		// Token: 0x040014E6 RID: 5350
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		internal byte[] MulticastAddress;

		// Token: 0x040014E7 RID: 5351
		internal int InterfaceIndex;

		// Token: 0x040014E8 RID: 5352
		internal static readonly int Size = Marshal.SizeOf(typeof(IPv6MulticastRequest));
	}
}
