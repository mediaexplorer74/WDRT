using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002B5 RID: 693
	[Flags]
	internal enum AdapterFlags
	{
		// Token: 0x04001906 RID: 6406
		DnsEnabled = 1,
		// Token: 0x04001907 RID: 6407
		RegisterAdapterSuffix = 2,
		// Token: 0x04001908 RID: 6408
		DhcpEnabled = 4,
		// Token: 0x04001909 RID: 6409
		ReceiveOnly = 8,
		// Token: 0x0400190A RID: 6410
		NoMulticast = 16,
		// Token: 0x0400190B RID: 6411
		Ipv6OtherStatefulConfig = 32,
		// Token: 0x0400190C RID: 6412
		NetBiosOverTcp = 64,
		// Token: 0x0400190D RID: 6413
		IPv4Enabled = 128,
		// Token: 0x0400190E RID: 6414
		IPv6Enabled = 256,
		// Token: 0x0400190F RID: 6415
		IPv6ManagedAddressConfigurationSupported = 512
	}
}
