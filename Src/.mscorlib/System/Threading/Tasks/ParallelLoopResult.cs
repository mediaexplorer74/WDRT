using System;

namespace System.Threading.Tasks
{
	/// <summary>Provides completion status on the execution of a <see cref="T:System.Threading.Tasks.Parallel" /> loop.</summary>
	// Token: 0x02000559 RID: 1369
	[__DynamicallyInvokable]
	public struct ParallelLoopResult
	{
		/// <summary>Gets whether the loop ran to completion, such that all iterations of the loop were executed and the loop didn't receive a request to end prematurely.</summary>
		/// <returns>true if the loop ran to completion; otherwise false;</returns>
		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06004086 RID: 16518 RVA: 0x000F1D81 File Offset: 0x000EFF81
		[__DynamicallyInvokable]
		public bool IsCompleted
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_completed;
			}
		}

		/// <summary>Gets the index of the lowest iteration from which <see cref="M:System.Threading.Tasks.ParallelLoopState.Break" /> was called.</summary>
		/// <returns>Returns an integer that represents the lowest iteration from which the Break statement was called.</returns>
		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06004087 RID: 16519 RVA: 0x000F1D89 File Offset: 0x000EFF89
		[__DynamicallyInvokable]
		public long? LowestBreakIteration
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_lowestBreakIteration;
			}
		}

		// Token: 0x04001AF3 RID: 6899
		internal bool m_completed;

		// Token: 0x04001AF4 RID: 6900
		internal long? m_lowestBreakIteration;
	}
}
