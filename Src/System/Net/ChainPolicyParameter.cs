using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000126 RID: 294
	internal struct ChainPolicyParameter
	{
		// Token: 0x04000FF0 RID: 4080
		public uint cbSize;

		// Token: 0x04000FF1 RID: 4081
		public uint dwFlags;

		// Token: 0x04000FF2 RID: 4082
		public unsafe SSL_EXTRA_CERT_CHAIN_POLICY_PARA* pvExtraPolicyPara;

		// Token: 0x04000FF3 RID: 4083
		public static readonly uint StructSize = (uint)Marshal.SizeOf(typeof(ChainPolicyParameter));
	}
}
