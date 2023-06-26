using System;

namespace System.Runtime
{
	/// <summary>Adjusts the time that the garbage collector intrudes in your application.</summary>
	// Token: 0x02000714 RID: 1812
	[__DynamicallyInvokable]
	[Serializable]
	public enum GCLatencyMode
	{
		/// <summary>Disables garbage collection concurrency and reclaims objects in a batch call. This is the most intrusive mode.</summary>
		// Token: 0x040023FE RID: 9214
		[__DynamicallyInvokable]
		Batch,
		/// <summary>Enables garbage collection concurrency and reclaims objects while the application is running. This is the default mode for garbage collection on a workstation and is less intrusive than <see cref="F:System.Runtime.GCLatencyMode.Batch" />. It balances responsiveness with throughput.</summary>
		// Token: 0x040023FF RID: 9215
		[__DynamicallyInvokable]
		Interactive,
		/// <summary>Enables garbage collection that is more conservative in reclaiming objects. Full collections occur only if the system is under memory pressure, whereas generation 0 and generation 1 collections might occur more frequently</summary>
		// Token: 0x04002400 RID: 9216
		[__DynamicallyInvokable]
		LowLatency,
		/// <summary>Enables garbage collection that tries to minimize latency over an extended period. The collector tries to perform only generation 0, generation 1, and concurrent generation 2 collections. Full blocking collections may still occur if the system is under memory pressure.</summary>
		// Token: 0x04002401 RID: 9217
		[__DynamicallyInvokable]
		SustainedLowLatency,
		/// <summary>Indicates that garbage collection is suspended while the app is executing a critical path.  
		///  <see cref="F:System.Runtime.GCLatencyMode.NoGCRegion" /> is a read-only value; that is, you cannot assign the <see cref="F:System.Runtime.GCLatencyMode.NoGCRegion" /> value to the <see cref="P:System.Runtime.GCSettings.LatencyMode" /> property. You specify the no GC region latency mode by calling the <see cref="Overload:System.GC.TryStartNoGCRegion" /> method and terminate it by calling the <see cref="M:System.GC.EndNoGCRegion" /> method.</summary>
		// Token: 0x04002402 RID: 9218
		NoGCRegion
	}
}
