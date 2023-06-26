using System;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime
{
	/// <summary>Specifies the garbage collection settings for the current process.</summary>
	// Token: 0x02000715 RID: 1813
	[__DynamicallyInvokable]
	public static class GCSettings
	{
		/// <summary>Gets or sets the current latency mode for garbage collection.</summary>
		/// <returns>One of the enumeration values that specifies the latency mode.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Runtime.GCSettings.LatencyMode" /> property is being set to an invalid value.  
		///  -or-  
		///  The <see cref="P:System.Runtime.GCSettings.LatencyMode" /> property cannot be set to <see cref="F:System.Runtime.GCLatencyMode.NoGCRegion" />.</exception>
		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x06005142 RID: 20802 RVA: 0x0011FD74 File Offset: 0x0011DF74
		// (set) Token: 0x06005143 RID: 20803 RVA: 0x0011FD7B File Offset: 0x0011DF7B
		[__DynamicallyInvokable]
		public static GCLatencyMode LatencyMode
		{
			[SecuritySafeCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[__DynamicallyInvokable]
			get
			{
				return (GCLatencyMode)GC.GetGCLatencyMode();
			}
			[SecurityCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[__DynamicallyInvokable]
			[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
			set
			{
				if (value < GCLatencyMode.Batch || value > GCLatencyMode.SustainedLowLatency)
				{
					throw new ArgumentOutOfRangeException(Environment.GetResourceString("ArgumentOutOfRange_Enum"));
				}
				if (GC.SetGCLatencyMode((int)value) == 1)
				{
					throw new InvalidOperationException("The NoGCRegion mode is in progress. End it and then set a different mode.");
				}
			}
		}

		/// <summary>Gets or sets a value that indicates whether a full blocking garbage collection compacts the large object heap (LOH).</summary>
		/// <returns>One of the enumeration values that indicates whether a full blocking garbage collection compacts the LOH.</returns>
		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x06005144 RID: 20804 RVA: 0x0011FDA9 File Offset: 0x0011DFA9
		// (set) Token: 0x06005145 RID: 20805 RVA: 0x0011FDB0 File Offset: 0x0011DFB0
		[__DynamicallyInvokable]
		public static GCLargeObjectHeapCompactionMode LargeObjectHeapCompactionMode
		{
			[SecuritySafeCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[__DynamicallyInvokable]
			get
			{
				return (GCLargeObjectHeapCompactionMode)GC.GetLOHCompactionMode();
			}
			[SecurityCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[__DynamicallyInvokable]
			[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
			set
			{
				if (value < GCLargeObjectHeapCompactionMode.Default || value > GCLargeObjectHeapCompactionMode.CompactOnce)
				{
					throw new ArgumentOutOfRangeException(Environment.GetResourceString("ArgumentOutOfRange_Enum"));
				}
				GC.SetLOHCompactionMode((int)value);
			}
		}

		/// <summary>Gets a value that indicates whether server garbage collection is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if server garbage collection is enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x06005146 RID: 20806 RVA: 0x0011FDD0 File Offset: 0x0011DFD0
		[__DynamicallyInvokable]
		public static bool IsServerGC
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				return GC.IsServerGC();
			}
		}

		// Token: 0x02000C5D RID: 3165
		private enum SetLatencyModeStatus
		{
			// Token: 0x040037BB RID: 14267
			Succeeded,
			// Token: 0x040037BC RID: 14268
			NoGCInProgress
		}
	}
}
