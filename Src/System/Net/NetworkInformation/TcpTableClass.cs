using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002CE RID: 718
	internal enum TcpTableClass
	{
		// Token: 0x04001A04 RID: 6660
		TcpTableBasicListener,
		// Token: 0x04001A05 RID: 6661
		TcpTableBasicConnections,
		// Token: 0x04001A06 RID: 6662
		TcpTableBasicAll,
		// Token: 0x04001A07 RID: 6663
		TcpTableOwnerPidListener,
		// Token: 0x04001A08 RID: 6664
		TcpTableOwnerPidConnections,
		// Token: 0x04001A09 RID: 6665
		TcpTableOwnerPidAll,
		// Token: 0x04001A0A RID: 6666
		TcpTableOwnerModuleListener,
		// Token: 0x04001A0B RID: 6667
		TcpTableOwnerModuleConnections,
		// Token: 0x04001A0C RID: 6668
		TcpTableOwnerModuleAll
	}
}
