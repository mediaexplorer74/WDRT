using System;

namespace System.Runtime
{
	/// <summary>Indicates whether the next blocking garbage collection compacts the large object heap (LOH).</summary>
	// Token: 0x02000713 RID: 1811
	[__DynamicallyInvokable]
	[Serializable]
	public enum GCLargeObjectHeapCompactionMode
	{
		/// <summary>The large object heap (LOH) is not compacted.</summary>
		// Token: 0x040023FB RID: 9211
		[__DynamicallyInvokable]
		Default = 1,
		/// <summary>The large object heap (LOH) will be compacted during the next blocking generation 2 garbage collection.</summary>
		// Token: 0x040023FC RID: 9212
		[__DynamicallyInvokable]
		CompactOnce
	}
}
