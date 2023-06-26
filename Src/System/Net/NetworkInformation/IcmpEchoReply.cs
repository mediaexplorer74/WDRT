using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002D5 RID: 725
	internal struct IcmpEchoReply
	{
		// Token: 0x04001A24 RID: 6692
		internal uint address;

		// Token: 0x04001A25 RID: 6693
		internal uint status;

		// Token: 0x04001A26 RID: 6694
		internal uint roundTripTime;

		// Token: 0x04001A27 RID: 6695
		internal ushort dataSize;

		// Token: 0x04001A28 RID: 6696
		internal ushort reserved;

		// Token: 0x04001A29 RID: 6697
		internal IntPtr data;

		// Token: 0x04001A2A RID: 6698
		internal IPOptions options;
	}
}
