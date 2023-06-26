using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002CB RID: 715
	internal struct MibTcpRow
	{
		// Token: 0x040019E9 RID: 6633
		internal TcpState state;

		// Token: 0x040019EA RID: 6634
		internal uint localAddr;

		// Token: 0x040019EB RID: 6635
		internal byte localPort1;

		// Token: 0x040019EC RID: 6636
		internal byte localPort2;

		// Token: 0x040019ED RID: 6637
		internal byte ignoreLocalPort3;

		// Token: 0x040019EE RID: 6638
		internal byte ignoreLocalPort4;

		// Token: 0x040019EF RID: 6639
		internal uint remoteAddr;

		// Token: 0x040019F0 RID: 6640
		internal byte remotePort1;

		// Token: 0x040019F1 RID: 6641
		internal byte remotePort2;

		// Token: 0x040019F2 RID: 6642
		internal byte ignoreRemotePort3;

		// Token: 0x040019F3 RID: 6643
		internal byte ignoreRemotePort4;
	}
}
