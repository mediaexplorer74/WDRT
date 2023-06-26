using System;

namespace System.Diagnostics
{
	/// <summary>Indicates the priority that the system associates with a process. This value, together with the priority value of each thread of the process, determines each thread's base priority level.</summary>
	// Token: 0x020004FD RID: 1277
	public enum ProcessPriorityClass
	{
		/// <summary>Specifies that the process has no special scheduling needs.</summary>
		// Token: 0x0400287E RID: 10366
		Normal = 32,
		/// <summary>Specifies that the threads of this process run only when the system is idle, such as a screen saver. The threads of the process are preempted by the threads of any process running in a higher priority class.</summary>
		// Token: 0x0400287F RID: 10367
		Idle = 64,
		/// <summary>Specifies that the process performs time-critical tasks that must be executed immediately, such as the <see langword="Task List" /> dialog, which must respond quickly when called by the user, regardless of the load on the operating system. The threads of the process preempt the threads of normal or idle priority class processes.</summary>
		// Token: 0x04002880 RID: 10368
		High = 128,
		/// <summary>Specifies that the process has the highest possible priority.</summary>
		// Token: 0x04002881 RID: 10369
		RealTime = 256,
		/// <summary>Specifies that the process has priority above <see langword="Idle" /> but below <see langword="Normal" />.</summary>
		// Token: 0x04002882 RID: 10370
		BelowNormal = 16384,
		/// <summary>Specifies that the process has priority above <see langword="Normal" /> but below <see cref="F:System.Diagnostics.ProcessPriorityClass.High" />.</summary>
		// Token: 0x04002883 RID: 10371
		AboveNormal = 32768
	}
}
