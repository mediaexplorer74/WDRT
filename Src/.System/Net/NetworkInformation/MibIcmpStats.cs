using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002C7 RID: 711
	internal struct MibIcmpStats
	{
		// Token: 0x040019D6 RID: 6614
		internal uint messages;

		// Token: 0x040019D7 RID: 6615
		internal uint errors;

		// Token: 0x040019D8 RID: 6616
		internal uint destinationUnreachables;

		// Token: 0x040019D9 RID: 6617
		internal uint timeExceeds;

		// Token: 0x040019DA RID: 6618
		internal uint parameterProblems;

		// Token: 0x040019DB RID: 6619
		internal uint sourceQuenches;

		// Token: 0x040019DC RID: 6620
		internal uint redirects;

		// Token: 0x040019DD RID: 6621
		internal uint echoRequests;

		// Token: 0x040019DE RID: 6622
		internal uint echoReplies;

		// Token: 0x040019DF RID: 6623
		internal uint timestampRequests;

		// Token: 0x040019E0 RID: 6624
		internal uint timestampReplies;

		// Token: 0x040019E1 RID: 6625
		internal uint addressMaskRequests;

		// Token: 0x040019E2 RID: 6626
		internal uint addressMaskReplies;
	}
}
