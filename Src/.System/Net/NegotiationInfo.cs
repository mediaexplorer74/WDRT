using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000214 RID: 532
	internal struct NegotiationInfo
	{
		// Token: 0x040015A2 RID: 5538
		internal IntPtr PackageInfo;

		// Token: 0x040015A3 RID: 5539
		internal uint NegotiationState;

		// Token: 0x040015A4 RID: 5540
		internal static readonly int Size = Marshal.SizeOf(typeof(NegotiationInfo));

		// Token: 0x040015A5 RID: 5541
		internal static readonly int NegotiationStateOffest = (int)Marshal.OffsetOf(typeof(NegotiationInfo), "NegotiationState");
	}
}
