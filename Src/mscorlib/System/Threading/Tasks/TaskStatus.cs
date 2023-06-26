using System;

namespace System.Threading.Tasks
{
	/// <summary>Represents the current stage in the lifecycle of a <see cref="T:System.Threading.Tasks.Task" />.</summary>
	// Token: 0x0200055B RID: 1371
	[__DynamicallyInvokable]
	public enum TaskStatus
	{
		/// <summary>The task has been initialized but has not yet been scheduled.</summary>
		// Token: 0x04001AF7 RID: 6903
		[__DynamicallyInvokable]
		Created,
		/// <summary>The task is waiting to be activated and scheduled internally by the .NET Framework infrastructure.</summary>
		// Token: 0x04001AF8 RID: 6904
		[__DynamicallyInvokable]
		WaitingForActivation,
		/// <summary>The task has been scheduled for execution but has not yet begun executing.</summary>
		// Token: 0x04001AF9 RID: 6905
		[__DynamicallyInvokable]
		WaitingToRun,
		/// <summary>The task is running but has not yet completed.</summary>
		// Token: 0x04001AFA RID: 6906
		[__DynamicallyInvokable]
		Running,
		/// <summary>The task has finished executing and is implicitly waiting for attached child tasks to complete.</summary>
		// Token: 0x04001AFB RID: 6907
		[__DynamicallyInvokable]
		WaitingForChildrenToComplete,
		/// <summary>The task completed execution successfully.</summary>
		// Token: 0x04001AFC RID: 6908
		[__DynamicallyInvokable]
		RanToCompletion,
		/// <summary>The task acknowledged cancellation by throwing an OperationCanceledException with its own CancellationToken while the token was in signaled state, or the task's CancellationToken was already signaled before the task started executing. For more information, see Task Cancellation.</summary>
		// Token: 0x04001AFD RID: 6909
		[__DynamicallyInvokable]
		Canceled,
		/// <summary>The task completed due to an unhandled exception.</summary>
		// Token: 0x04001AFE RID: 6910
		[__DynamicallyInvokable]
		Faulted
	}
}
