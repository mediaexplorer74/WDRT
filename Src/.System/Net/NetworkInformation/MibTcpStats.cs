using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002C4 RID: 708
	internal struct MibTcpStats
	{
		// Token: 0x040019AE RID: 6574
		internal uint reTransmissionAlgorithm;

		// Token: 0x040019AF RID: 6575
		internal uint minimumRetransmissionTimeOut;

		// Token: 0x040019B0 RID: 6576
		internal uint maximumRetransmissionTimeOut;

		// Token: 0x040019B1 RID: 6577
		internal uint maximumConnections;

		// Token: 0x040019B2 RID: 6578
		internal uint activeOpens;

		// Token: 0x040019B3 RID: 6579
		internal uint passiveOpens;

		// Token: 0x040019B4 RID: 6580
		internal uint failedConnectionAttempts;

		// Token: 0x040019B5 RID: 6581
		internal uint resetConnections;

		// Token: 0x040019B6 RID: 6582
		internal uint currentConnections;

		// Token: 0x040019B7 RID: 6583
		internal uint segmentsReceived;

		// Token: 0x040019B8 RID: 6584
		internal uint segmentsSent;

		// Token: 0x040019B9 RID: 6585
		internal uint segmentsResent;

		// Token: 0x040019BA RID: 6586
		internal uint errorsReceived;

		// Token: 0x040019BB RID: 6587
		internal uint segmentsSentWithReset;

		// Token: 0x040019BC RID: 6588
		internal uint cumulativeConnections;
	}
}
