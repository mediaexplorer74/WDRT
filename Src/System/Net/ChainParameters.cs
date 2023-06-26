using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x0200012B RID: 299
	internal struct ChainParameters
	{
		// Token: 0x04001003 RID: 4099
		public uint cbSize;

		// Token: 0x04001004 RID: 4100
		public CertUsageMatch RequestedUsage;

		// Token: 0x04001005 RID: 4101
		public CertUsageMatch RequestedIssuancePolicy;

		// Token: 0x04001006 RID: 4102
		public uint UrlRetrievalTimeout;

		// Token: 0x04001007 RID: 4103
		public int BoolCheckRevocationFreshnessTime;

		// Token: 0x04001008 RID: 4104
		public uint RevocationFreshnessTime;

		// Token: 0x04001009 RID: 4105
		public static readonly uint StructSize = (uint)Marshal.SizeOf(typeof(ChainParameters));
	}
}
