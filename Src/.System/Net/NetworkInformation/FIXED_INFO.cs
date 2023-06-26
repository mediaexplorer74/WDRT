using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002BA RID: 698
	internal struct FIXED_INFO
	{
		// Token: 0x0400192A RID: 6442
		internal const int MAX_HOSTNAME_LEN = 128;

		// Token: 0x0400192B RID: 6443
		internal const int MAX_DOMAIN_NAME_LEN = 128;

		// Token: 0x0400192C RID: 6444
		internal const int MAX_SCOPE_ID_LEN = 256;

		// Token: 0x0400192D RID: 6445
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 132)]
		internal string hostName;

		// Token: 0x0400192E RID: 6446
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 132)]
		internal string domainName;

		// Token: 0x0400192F RID: 6447
		internal uint currentDnsServer;

		// Token: 0x04001930 RID: 6448
		internal IpAddrString DnsServerList;

		// Token: 0x04001931 RID: 6449
		internal NetBiosNodeType nodeType;

		// Token: 0x04001932 RID: 6450
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		internal string scopeId;

		// Token: 0x04001933 RID: 6451
		internal bool enableRouting;

		// Token: 0x04001934 RID: 6452
		internal bool enableProxy;

		// Token: 0x04001935 RID: 6453
		internal bool enableDns;
	}
}
