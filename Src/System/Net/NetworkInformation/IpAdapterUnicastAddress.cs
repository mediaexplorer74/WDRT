using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002BD RID: 701
	internal struct IpAdapterUnicastAddress
	{
		// Token: 0x0400193C RID: 6460
		internal uint length;

		// Token: 0x0400193D RID: 6461
		internal AdapterAddressFlags flags;

		// Token: 0x0400193E RID: 6462
		internal IntPtr next;

		// Token: 0x0400193F RID: 6463
		internal IpSocketAddress address;

		// Token: 0x04001940 RID: 6464
		internal PrefixOrigin prefixOrigin;

		// Token: 0x04001941 RID: 6465
		internal SuffixOrigin suffixOrigin;

		// Token: 0x04001942 RID: 6466
		internal DuplicateAddressDetectionState dadState;

		// Token: 0x04001943 RID: 6467
		internal uint validLifetime;

		// Token: 0x04001944 RID: 6468
		internal uint preferredLifetime;

		// Token: 0x04001945 RID: 6469
		internal uint leaseLifetime;

		// Token: 0x04001946 RID: 6470
		internal byte prefixLength;
	}
}
