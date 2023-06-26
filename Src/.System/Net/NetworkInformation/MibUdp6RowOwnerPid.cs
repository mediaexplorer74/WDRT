using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002D3 RID: 723
	internal struct MibUdp6RowOwnerPid
	{
		// Token: 0x04001A18 RID: 6680
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		internal byte[] localAddr;

		// Token: 0x04001A19 RID: 6681
		internal uint localScopeId;

		// Token: 0x04001A1A RID: 6682
		internal byte localPort1;

		// Token: 0x04001A1B RID: 6683
		internal byte localPort2;

		// Token: 0x04001A1C RID: 6684
		internal byte ignoreLocalPort3;

		// Token: 0x04001A1D RID: 6685
		internal byte ignoreLocalPort4;

		// Token: 0x04001A1E RID: 6686
		internal uint owningPid;
	}
}
