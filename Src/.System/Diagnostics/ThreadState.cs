using System;

namespace System.Diagnostics
{
	/// <summary>Specifies the current execution state of the thread.</summary>
	// Token: 0x02000508 RID: 1288
	public enum ThreadState
	{
		/// <summary>A state that indicates the thread has been initialized, but has not yet started.</summary>
		// Token: 0x040028D6 RID: 10454
		Initialized,
		/// <summary>A state that indicates the thread is waiting to use a processor because no processor is free. The thread is prepared to run on the next available processor.</summary>
		// Token: 0x040028D7 RID: 10455
		Ready,
		/// <summary>A state that indicates the thread is currently using a processor.</summary>
		// Token: 0x040028D8 RID: 10456
		Running,
		/// <summary>A state that indicates the thread is about to use a processor. Only one thread can be in this state at a time.</summary>
		// Token: 0x040028D9 RID: 10457
		Standby,
		/// <summary>A state that indicates the thread has finished executing and has exited.</summary>
		// Token: 0x040028DA RID: 10458
		Terminated,
		/// <summary>A state that indicates the thread is not ready to use the processor because it is waiting for a peripheral operation to complete or a resource to become free. When the thread is ready, it will be rescheduled.</summary>
		// Token: 0x040028DB RID: 10459
		Wait,
		/// <summary>A state that indicates the thread is waiting for a resource, other than the processor, before it can execute. For example, it might be waiting for its execution stack to be paged in from disk.</summary>
		// Token: 0x040028DC RID: 10460
		Transition,
		/// <summary>The state of the thread is unknown.</summary>
		// Token: 0x040028DD RID: 10461
		Unknown
	}
}
