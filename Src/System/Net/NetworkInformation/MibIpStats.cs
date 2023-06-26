using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002C5 RID: 709
	internal struct MibIpStats
	{
		// Token: 0x040019BD RID: 6589
		internal bool forwardingEnabled;

		// Token: 0x040019BE RID: 6590
		internal uint defaultTtl;

		// Token: 0x040019BF RID: 6591
		internal uint packetsReceived;

		// Token: 0x040019C0 RID: 6592
		internal uint receivedPacketsWithHeaderErrors;

		// Token: 0x040019C1 RID: 6593
		internal uint receivedPacketsWithAddressErrors;

		// Token: 0x040019C2 RID: 6594
		internal uint packetsForwarded;

		// Token: 0x040019C3 RID: 6595
		internal uint receivedPacketsWithUnknownProtocols;

		// Token: 0x040019C4 RID: 6596
		internal uint receivedPacketsDiscarded;

		// Token: 0x040019C5 RID: 6597
		internal uint receivedPacketsDelivered;

		// Token: 0x040019C6 RID: 6598
		internal uint packetOutputRequests;

		// Token: 0x040019C7 RID: 6599
		internal uint outputPacketRoutingDiscards;

		// Token: 0x040019C8 RID: 6600
		internal uint outputPacketsDiscarded;

		// Token: 0x040019C9 RID: 6601
		internal uint outputPacketsWithNoRoute;

		// Token: 0x040019CA RID: 6602
		internal uint packetReassemblyTimeout;

		// Token: 0x040019CB RID: 6603
		internal uint packetsReassemblyRequired;

		// Token: 0x040019CC RID: 6604
		internal uint packetsReassembled;

		// Token: 0x040019CD RID: 6605
		internal uint packetsReassemblyFailed;

		// Token: 0x040019CE RID: 6606
		internal uint packetsFragmented;

		// Token: 0x040019CF RID: 6607
		internal uint packetsFragmentFailed;

		// Token: 0x040019D0 RID: 6608
		internal uint packetsFragmentCreated;

		// Token: 0x040019D1 RID: 6609
		internal uint interfaces;

		// Token: 0x040019D2 RID: 6610
		internal uint ipAddresses;

		// Token: 0x040019D3 RID: 6611
		internal uint routes;
	}
}
