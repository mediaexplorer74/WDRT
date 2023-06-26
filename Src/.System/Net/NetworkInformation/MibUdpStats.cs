using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002C3 RID: 707
	internal struct MibUdpStats
	{
		// Token: 0x040019A9 RID: 6569
		internal uint datagramsReceived;

		// Token: 0x040019AA RID: 6570
		internal uint incomingDatagramsDiscarded;

		// Token: 0x040019AB RID: 6571
		internal uint incomingDatagramsWithErrors;

		// Token: 0x040019AC RID: 6572
		internal uint datagramsSent;

		// Token: 0x040019AD RID: 6573
		internal uint udpListeners;
	}
}
