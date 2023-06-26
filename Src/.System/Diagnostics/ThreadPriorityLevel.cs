using System;

namespace System.Diagnostics
{
	/// <summary>Specifies the priority level of a thread.</summary>
	// Token: 0x02000507 RID: 1287
	public enum ThreadPriorityLevel
	{
		/// <summary>Specifies idle priority. This is the lowest possible priority value of all threads, independent of the value of the associated <see cref="T:System.Diagnostics.ProcessPriorityClass" />.</summary>
		// Token: 0x040028CE RID: 10446
		Idle = -15,
		/// <summary>Specifies lowest priority. This is two steps below the normal priority for the associated <see cref="T:System.Diagnostics.ProcessPriorityClass" />.</summary>
		// Token: 0x040028CF RID: 10447
		Lowest = -2,
		/// <summary>Specifies one step below the normal priority for the associated <see cref="T:System.Diagnostics.ProcessPriorityClass" />.</summary>
		// Token: 0x040028D0 RID: 10448
		BelowNormal,
		/// <summary>Specifies normal priority for the associated <see cref="T:System.Diagnostics.ProcessPriorityClass" />.</summary>
		// Token: 0x040028D1 RID: 10449
		Normal,
		/// <summary>Specifies one step above the normal priority for the associated <see cref="T:System.Diagnostics.ProcessPriorityClass" />.</summary>
		// Token: 0x040028D2 RID: 10450
		AboveNormal,
		/// <summary>Specifies highest priority. This is two steps above the normal priority for the associated <see cref="T:System.Diagnostics.ProcessPriorityClass" />.</summary>
		// Token: 0x040028D3 RID: 10451
		Highest,
		/// <summary>Specifies time-critical priority. This is the highest priority of all threads, independent of the value of the associated <see cref="T:System.Diagnostics.ProcessPriorityClass" />.</summary>
		// Token: 0x040028D4 RID: 10452
		TimeCritical = 15
	}
}
