using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002F3 RID: 755
	internal enum IcmpV6StatType
	{
		// Token: 0x04001A88 RID: 6792
		DestinationUnreachable = 1,
		// Token: 0x04001A89 RID: 6793
		PacketTooBig,
		// Token: 0x04001A8A RID: 6794
		TimeExceeded,
		// Token: 0x04001A8B RID: 6795
		ParameterProblem,
		// Token: 0x04001A8C RID: 6796
		EchoRequest = 128,
		// Token: 0x04001A8D RID: 6797
		EchoReply,
		// Token: 0x04001A8E RID: 6798
		MembershipQuery,
		// Token: 0x04001A8F RID: 6799
		MembershipReport,
		// Token: 0x04001A90 RID: 6800
		MembershipReduction,
		// Token: 0x04001A91 RID: 6801
		RouterSolicit,
		// Token: 0x04001A92 RID: 6802
		RouterAdvertisement,
		// Token: 0x04001A93 RID: 6803
		NeighborSolict,
		// Token: 0x04001A94 RID: 6804
		NeighborAdvertisement,
		// Token: 0x04001A95 RID: 6805
		Redirect
	}
}
