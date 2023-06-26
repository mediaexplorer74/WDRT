using System;

namespace System
{
	/// <summary>Specifies the behavior for a forced garbage collection.</summary>
	// Token: 0x020000E6 RID: 230
	[__DynamicallyInvokable]
	[Serializable]
	public enum GCCollectionMode
	{
		/// <summary>The default setting for this enumeration, which is currently <see cref="F:System.GCCollectionMode.Forced" />.</summary>
		// Token: 0x0400057E RID: 1406
		[__DynamicallyInvokable]
		Default,
		/// <summary>Forces the garbage collection to occur immediately.</summary>
		// Token: 0x0400057F RID: 1407
		[__DynamicallyInvokable]
		Forced,
		/// <summary>Allows the garbage collector to determine whether the current time is optimal to reclaim objects.</summary>
		// Token: 0x04000580 RID: 1408
		[__DynamicallyInvokable]
		Optimized
	}
}
