using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002D7 RID: 727
	internal struct Icmp6EchoReply
	{
		// Token: 0x04001A2E RID: 6702
		internal Ipv6Address Address;

		// Token: 0x04001A2F RID: 6703
		internal uint Status;

		// Token: 0x04001A30 RID: 6704
		internal uint RoundTripTime;

		// Token: 0x04001A31 RID: 6705
		internal IntPtr data;
	}
}
