using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002CD RID: 717
	internal struct MibTcp6RowOwnerPid
	{
		// Token: 0x040019F5 RID: 6645
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		internal byte[] localAddr;

		// Token: 0x040019F6 RID: 6646
		internal uint localScopeId;

		// Token: 0x040019F7 RID: 6647
		internal byte localPort1;

		// Token: 0x040019F8 RID: 6648
		internal byte localPort2;

		// Token: 0x040019F9 RID: 6649
		internal byte ignoreLocalPort3;

		// Token: 0x040019FA RID: 6650
		internal byte ignoreLocalPort4;

		// Token: 0x040019FB RID: 6651
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		internal byte[] remoteAddr;

		// Token: 0x040019FC RID: 6652
		internal uint remoteScopeId;

		// Token: 0x040019FD RID: 6653
		internal byte remotePort1;

		// Token: 0x040019FE RID: 6654
		internal byte remotePort2;

		// Token: 0x040019FF RID: 6655
		internal byte ignoreRemotePort3;

		// Token: 0x04001A00 RID: 6656
		internal byte ignoreRemotePort4;

		// Token: 0x04001A01 RID: 6657
		internal TcpState state;

		// Token: 0x04001A02 RID: 6658
		internal uint owningPid;
	}
}
