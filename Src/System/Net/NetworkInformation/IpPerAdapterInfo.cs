using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002C1 RID: 705
	internal struct IpPerAdapterInfo
	{
		// Token: 0x04001979 RID: 6521
		internal bool autoconfigEnabled;

		// Token: 0x0400197A RID: 6522
		internal bool autoconfigActive;

		// Token: 0x0400197B RID: 6523
		internal IntPtr currentDnsServer;

		// Token: 0x0400197C RID: 6524
		internal IpAddrString dnsServerList;
	}
}
