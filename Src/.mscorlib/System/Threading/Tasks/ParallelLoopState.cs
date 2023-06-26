using System;
using System.Diagnostics;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
	/// <summary>Enables iterations of parallel loops to interact with other iterations. An instance of this class is provided by the <see cref="T:System.Threading.Tasks.Parallel" /> class to each loop; you can not create instances in your code.</summary>
	// Token: 0x02000553 RID: 1363
	[DebuggerDisplay("ShouldExitCurrentIteration = {ShouldExitCurrentIteration}")]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class ParallelLoopState
	{
		// Token: 0x0600405C RID: 16476 RVA: 0x000F1874 File Offset: 0x000EFA74
		internal ParallelLoopState(ParallelLoopStateFlags fbase)
		{
			this.m_flagsBase = fbase;
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x0600405D RID: 16477 RVA: 0x000F1883 File Offset: 0x000EFA83
		internal virtual bool InternalShouldExitCurrentIteration
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("ParallelState_NotSupportedException_UnsupportedMethod"));
			}
		}

		/// <summary>Gets whether the current iteration of the loop should exit based on requests made by this or other iterations.</summary>
		/// <returns>
		///   <see langword="true" /> if the current iteration should exit; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x0600405E RID: 16478 RVA: 0x000F1894 File Offset: 0x000EFA94
		[__DynamicallyInvokable]
		public bool ShouldExitCurrentIteration
		{
			[__DynamicallyInvokable]
			get
			{
				return this.InternalShouldExitCurrentIteration;
			}
		}

		/// <summary>Gets whether any iteration of the loop has called the <see cref="M:System.Threading.Tasks.ParallelLoopState.Stop" /> method.</summary>
		/// <returns>
		///   <see langword="true" /> if any iteration has stopped the loop by calling the <see cref="M:System.Threading.Tasks.ParallelLoopState.Stop" /> method; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x0600405F RID: 16479 RVA: 0x000F189C File Offset: 0x000EFA9C
		[__DynamicallyInvokable]
		public bool IsStopped
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.m_flagsBase.LoopStateFlags & ParallelLoopStateFlags.PLS_STOPPED) != 0;
			}
		}

		/// <summary>Gets whether any iteration of the loop has thrown an exception that went unhandled by that iteration.</summary>
		/// <returns>
		///   <see langword="true" /> if an unhandled exception was thrown; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06004060 RID: 16480 RVA: 0x000F18B2 File Offset: 0x000EFAB2
		[__DynamicallyInvokable]
		public bool IsExceptional
		{
			[__DynamicallyInvokable]
			get
			{
				return (this.m_flagsBase.LoopStateFlags & ParallelLoopStateFlags.PLS_EXCEPTIONAL) != 0;
			}
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06004061 RID: 16481 RVA: 0x000F18C8 File Offset: 0x000EFAC8
		internal virtual long? InternalLowestBreakIteration
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("ParallelState_NotSupportedException_UnsupportedMethod"));
			}
		}

		/// <summary>Gets the lowest iteration of the loop from which <see cref="M:System.Threading.Tasks.ParallelLoopState.Break" /> was called.</summary>
		/// <returns>The lowest iteration from which <see cref="M:System.Threading.Tasks.ParallelLoopState.Break" /> was called. In the case of a <see cref="M:System.Threading.Tasks.Parallel.ForEach``1(System.Collections.Concurrent.Partitioner{``0},System.Action{``0})" /> loop, the value is based on an internally-generated index.</returns>
		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06004062 RID: 16482 RVA: 0x000F18D9 File Offset: 0x000EFAD9
		[__DynamicallyInvokable]
		public long? LowestBreakIteration
		{
			[__DynamicallyInvokable]
			get
			{
				return this.InternalLowestBreakIteration;
			}
		}

		/// <summary>Communicates that the <see cref="T:System.Threading.Tasks.Parallel" /> loop should cease execution at the system's earliest convenience.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Threading.Tasks.ParallelLoopState.Break" /> method was called previously. <see cref="M:System.Threading.Tasks.ParallelLoopState.Break" /> and <see cref="M:System.Threading.Tasks.ParallelLoopState.Stop" /> may not be used in combination by iterations of the same loop.</exception>
		// Token: 0x06004063 RID: 16483 RVA: 0x000F18E1 File Offset: 0x000EFAE1
		[__DynamicallyInvokable]
		public void Stop()
		{
			this.m_flagsBase.Stop();
		}

		// Token: 0x06004064 RID: 16484 RVA: 0x000F18EE File Offset: 0x000EFAEE
		internal virtual void InternalBreak()
		{
			throw new NotSupportedException(Environment.GetResourceString("ParallelState_NotSupportedException_UnsupportedMethod"));
		}

		/// <summary>Communicates that the <see cref="T:System.Threading.Tasks.Parallel" /> loop should cease execution of iterations beyond the current iteration at the system's earliest convenience.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Threading.Tasks.ParallelLoopState.Stop" /> method was previously called. <see cref="M:System.Threading.Tasks.ParallelLoopState.Break" /> and <see cref="M:System.Threading.Tasks.ParallelLoopState.Stop" /> may not be used in combination by iterations of the same loop.</exception>
		// Token: 0x06004065 RID: 16485 RVA: 0x000F18FF File Offset: 0x000EFAFF
		[__DynamicallyInvokable]
		public void Break()
		{
			this.InternalBreak();
		}

		// Token: 0x06004066 RID: 16486 RVA: 0x000F1908 File Offset: 0x000EFB08
		internal static void Break(int iteration, ParallelLoopStateFlags32 pflags)
		{
			int pls_NONE = ParallelLoopStateFlags.PLS_NONE;
			if (pflags.AtomicLoopStateUpdate(ParallelLoopStateFlags.PLS_BROKEN, ParallelLoopStateFlags.PLS_STOPPED | ParallelLoopStateFlags.PLS_EXCEPTIONAL | ParallelLoopStateFlags.PLS_CANCELED, ref pls_NONE))
			{
				int num = pflags.m_lowestBreakIteration;
				if (iteration < num)
				{
					SpinWait spinWait = default(SpinWait);
					while (Interlocked.CompareExchange(ref pflags.m_lowestBreakIteration, iteration, num) != num)
					{
						spinWait.SpinOnce();
						num = pflags.m_lowestBreakIteration;
						if (iteration > num)
						{
							break;
						}
					}
				}
				return;
			}
			if ((pls_NONE & ParallelLoopStateFlags.PLS_STOPPED) != 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("ParallelState_Break_InvalidOperationException_BreakAfterStop"));
			}
		}

		// Token: 0x06004067 RID: 16487 RVA: 0x000F1990 File Offset: 0x000EFB90
		internal static void Break(long iteration, ParallelLoopStateFlags64 pflags)
		{
			int pls_NONE = ParallelLoopStateFlags.PLS_NONE;
			if (pflags.AtomicLoopStateUpdate(ParallelLoopStateFlags.PLS_BROKEN, ParallelLoopStateFlags.PLS_STOPPED | ParallelLoopStateFlags.PLS_EXCEPTIONAL | ParallelLoopStateFlags.PLS_CANCELED, ref pls_NONE))
			{
				long num = pflags.LowestBreakIteration;
				if (iteration < num)
				{
					SpinWait spinWait = default(SpinWait);
					while (Interlocked.CompareExchange(ref pflags.m_lowestBreakIteration, iteration, num) != num)
					{
						spinWait.SpinOnce();
						num = pflags.LowestBreakIteration;
						if (iteration > num)
						{
							break;
						}
					}
				}
				return;
			}
			if ((pls_NONE & ParallelLoopStateFlags.PLS_STOPPED) != 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("ParallelState_Break_InvalidOperationException_BreakAfterStop"));
			}
		}

		// Token: 0x04001AE6 RID: 6886
		private ParallelLoopStateFlags m_flagsBase;
	}
}
