using System;
using System.Runtime.InteropServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000550 RID: 1360
	[StructLayout(LayoutKind.Auto)]
	internal struct IndexRange
	{
		// Token: 0x04001AD8 RID: 6872
		internal long m_nFromInclusive;

		// Token: 0x04001AD9 RID: 6873
		internal long m_nToExclusive;

		// Token: 0x04001ADA RID: 6874
		internal volatile Shared<long> m_nSharedCurrentIndexOffset;

		// Token: 0x04001ADB RID: 6875
		internal int m_bRangeFinished;
	}
}
