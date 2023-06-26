using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x020001D2 RID: 466
	internal struct IPMulticastRequest
	{
		// Token: 0x040014C1 RID: 5313
		internal int MulticastAddress;

		// Token: 0x040014C2 RID: 5314
		internal int InterfaceAddress;

		// Token: 0x040014C3 RID: 5315
		internal static readonly int Size = Marshal.SizeOf(typeof(IPMulticastRequest));
	}
}
