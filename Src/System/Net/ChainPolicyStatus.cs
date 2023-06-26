using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000128 RID: 296
	internal struct ChainPolicyStatus
	{
		// Token: 0x04000FF9 RID: 4089
		public uint cbSize;

		// Token: 0x04000FFA RID: 4090
		public uint dwError;

		// Token: 0x04000FFB RID: 4091
		public uint lChainIndex;

		// Token: 0x04000FFC RID: 4092
		public uint lElementIndex;

		// Token: 0x04000FFD RID: 4093
		public unsafe void* pvExtraPolicyStatus;

		// Token: 0x04000FFE RID: 4094
		public static readonly uint StructSize = (uint)Marshal.SizeOf(typeof(ChainPolicyStatus));
	}
}
