using System;

namespace System.Diagnostics
{
	/// <summary>Specifies the lifetime of a performance counter instance.</summary>
	// Token: 0x020004E1 RID: 1249
	public enum PerformanceCounterInstanceLifetime
	{
		/// <summary>Remove the performance counter instance when no counters are using the process category.</summary>
		// Token: 0x040027AD RID: 10157
		Global,
		/// <summary>Remove the performance counter instance when the process is closed.</summary>
		// Token: 0x040027AE RID: 10158
		Process
	}
}
