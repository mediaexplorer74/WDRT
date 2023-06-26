using System;

namespace System.Diagnostics
{
	/// <summary>Specifies trace data options to be written to the trace output.</summary>
	// Token: 0x020004B4 RID: 1204
	[Flags]
	public enum TraceOptions
	{
		/// <summary>Do not write any elements.</summary>
		// Token: 0x040026E0 RID: 9952
		None = 0,
		/// <summary>Write the logical operation stack, which is represented by the return value of the <see cref="P:System.Diagnostics.CorrelationManager.LogicalOperationStack" /> property.</summary>
		// Token: 0x040026E1 RID: 9953
		LogicalOperationStack = 1,
		/// <summary>Write the date and time.</summary>
		// Token: 0x040026E2 RID: 9954
		DateTime = 2,
		/// <summary>Write the timestamp, which is represented by the return value of the <see cref="M:System.Diagnostics.Stopwatch.GetTimestamp" /> method.</summary>
		// Token: 0x040026E3 RID: 9955
		Timestamp = 4,
		/// <summary>Write the process identity, which is represented by the return value of the <see cref="P:System.Diagnostics.Process.Id" /> property.</summary>
		// Token: 0x040026E4 RID: 9956
		ProcessId = 8,
		/// <summary>Write the thread identity, which is represented by the return value of the <see cref="P:System.Threading.Thread.ManagedThreadId" /> property for the current thread.</summary>
		// Token: 0x040026E5 RID: 9957
		ThreadId = 16,
		/// <summary>Write the call stack, which is represented by the return value of the <see cref="P:System.Environment.StackTrace" /> property.</summary>
		// Token: 0x040026E6 RID: 9958
		Callstack = 32
	}
}
