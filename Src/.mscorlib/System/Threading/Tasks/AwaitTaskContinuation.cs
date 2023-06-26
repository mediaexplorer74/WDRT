using System;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x02000570 RID: 1392
	internal class AwaitTaskContinuation : TaskContinuation, IThreadPoolWorkItem
	{
		// Token: 0x06004194 RID: 16788 RVA: 0x000F6074 File Offset: 0x000F4274
		[SecurityCritical]
		internal AwaitTaskContinuation(Action action, bool flowExecutionContext, ref StackCrawlMark stackMark)
		{
			this.m_action = action;
			if (flowExecutionContext)
			{
				this.m_capturedContext = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
			}
		}

		// Token: 0x06004195 RID: 16789 RVA: 0x000F6093 File Offset: 0x000F4293
		[SecurityCritical]
		internal AwaitTaskContinuation(Action action, bool flowExecutionContext)
		{
			this.m_action = action;
			if (flowExecutionContext)
			{
				this.m_capturedContext = ExecutionContext.FastCapture();
			}
		}

		// Token: 0x06004196 RID: 16790 RVA: 0x000F60B0 File Offset: 0x000F42B0
		protected Task CreateTask(Action<object> action, object state, TaskScheduler scheduler)
		{
			return new Task(action, state, null, default(CancellationToken), TaskCreationOptions.None, InternalTaskOptions.QueuedByRuntime, scheduler)
			{
				CapturedContext = this.m_capturedContext
			};
		}

		// Token: 0x06004197 RID: 16791 RVA: 0x000F60E4 File Offset: 0x000F42E4
		[SecuritySafeCritical]
		internal override void Run(Task task, bool canInlineContinuationTask)
		{
			if (canInlineContinuationTask && AwaitTaskContinuation.IsValidLocationForInlining)
			{
				this.RunCallback(AwaitTaskContinuation.GetInvokeActionCallback(), this.m_action, ref Task.t_currentTask);
				return;
			}
			TplEtwProvider log = TplEtwProvider.Log;
			if (log.IsEnabled())
			{
				this.m_continuationId = Task.NewId();
				log.AwaitTaskContinuationScheduled((task.ExecutingTaskScheduler ?? TaskScheduler.Default).Id, task.Id, this.m_continuationId);
			}
			ThreadPool.UnsafeQueueCustomWorkItem(this, false);
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06004198 RID: 16792 RVA: 0x000F6158 File Offset: 0x000F4358
		internal static bool IsValidLocationForInlining
		{
			get
			{
				SynchronizationContext currentNoFlow = SynchronizationContext.CurrentNoFlow;
				if (currentNoFlow != null && currentNoFlow.GetType() != typeof(SynchronizationContext))
				{
					return false;
				}
				TaskScheduler internalCurrent = TaskScheduler.InternalCurrent;
				return internalCurrent == null || internalCurrent == TaskScheduler.Default;
			}
		}

		// Token: 0x06004199 RID: 16793 RVA: 0x000F619C File Offset: 0x000F439C
		[SecurityCritical]
		private void ExecuteWorkItemHelper()
		{
			TplEtwProvider log = TplEtwProvider.Log;
			Guid empty = Guid.Empty;
			if (log.TasksSetActivityIds && this.m_continuationId != 0)
			{
				Guid guid = TplEtwProvider.CreateGuidForTaskID(this.m_continuationId);
				EventSource.SetCurrentThreadActivityId(guid, out empty);
			}
			try
			{
				if (this.m_capturedContext == null)
				{
					this.m_action();
				}
				else
				{
					try
					{
						ExecutionContext.Run(this.m_capturedContext, AwaitTaskContinuation.GetInvokeActionCallback(), this.m_action, true);
					}
					finally
					{
						this.m_capturedContext.Dispose();
					}
				}
			}
			finally
			{
				if (log.TasksSetActivityIds && this.m_continuationId != 0)
				{
					EventSource.SetCurrentThreadActivityId(empty);
				}
			}
		}

		// Token: 0x0600419A RID: 16794 RVA: 0x000F6248 File Offset: 0x000F4448
		[SecurityCritical]
		void IThreadPoolWorkItem.ExecuteWorkItem()
		{
			if (this.m_capturedContext == null && !TplEtwProvider.Log.IsEnabled())
			{
				this.m_action();
				return;
			}
			this.ExecuteWorkItemHelper();
		}

		// Token: 0x0600419B RID: 16795 RVA: 0x000F6270 File Offset: 0x000F4470
		[SecurityCritical]
		void IThreadPoolWorkItem.MarkAborted(ThreadAbortException tae)
		{
		}

		// Token: 0x0600419C RID: 16796 RVA: 0x000F6272 File Offset: 0x000F4472
		[SecurityCritical]
		private static void InvokeAction(object state)
		{
			((Action)state)();
		}

		// Token: 0x0600419D RID: 16797 RVA: 0x000F6280 File Offset: 0x000F4480
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected static ContextCallback GetInvokeActionCallback()
		{
			ContextCallback contextCallback = AwaitTaskContinuation.s_invokeActionCallback;
			if (contextCallback == null)
			{
				contextCallback = (AwaitTaskContinuation.s_invokeActionCallback = new ContextCallback(AwaitTaskContinuation.InvokeAction));
			}
			return contextCallback;
		}

		// Token: 0x0600419E RID: 16798 RVA: 0x000F62AC File Offset: 0x000F44AC
		[SecurityCritical]
		protected void RunCallback(ContextCallback callback, object state, ref Task currentTask)
		{
			Task task = currentTask;
			try
			{
				if (task != null)
				{
					currentTask = null;
				}
				if (this.m_capturedContext == null)
				{
					callback(state);
				}
				else
				{
					ExecutionContext.Run(this.m_capturedContext, callback, state, true);
				}
			}
			catch (Exception ex)
			{
				AwaitTaskContinuation.ThrowAsyncIfNecessary(ex);
			}
			finally
			{
				if (task != null)
				{
					currentTask = task;
				}
				if (this.m_capturedContext != null)
				{
					this.m_capturedContext.Dispose();
				}
			}
		}

		// Token: 0x0600419F RID: 16799 RVA: 0x000F6324 File Offset: 0x000F4524
		[SecurityCritical]
		internal static void RunOrScheduleAction(Action action, bool allowInlining, ref Task currentTask)
		{
			if (!allowInlining || !AwaitTaskContinuation.IsValidLocationForInlining)
			{
				AwaitTaskContinuation.UnsafeScheduleAction(action, currentTask);
				return;
			}
			Task task = currentTask;
			try
			{
				if (task != null)
				{
					currentTask = null;
				}
				action();
			}
			catch (Exception ex)
			{
				AwaitTaskContinuation.ThrowAsyncIfNecessary(ex);
			}
			finally
			{
				if (task != null)
				{
					currentTask = task;
				}
			}
		}

		// Token: 0x060041A0 RID: 16800 RVA: 0x000F6384 File Offset: 0x000F4584
		[SecurityCritical]
		internal static void UnsafeScheduleAction(Action action, Task task)
		{
			AwaitTaskContinuation awaitTaskContinuation = new AwaitTaskContinuation(action, false);
			TplEtwProvider log = TplEtwProvider.Log;
			if (log.IsEnabled() && task != null)
			{
				awaitTaskContinuation.m_continuationId = Task.NewId();
				log.AwaitTaskContinuationScheduled((task.ExecutingTaskScheduler ?? TaskScheduler.Default).Id, task.Id, awaitTaskContinuation.m_continuationId);
			}
			ThreadPool.UnsafeQueueCustomWorkItem(awaitTaskContinuation, false);
		}

		// Token: 0x060041A1 RID: 16801 RVA: 0x000F63E4 File Offset: 0x000F45E4
		protected static void ThrowAsyncIfNecessary(Exception exc)
		{
			if (!(exc is ThreadAbortException) && !(exc is AppDomainUnloadedException) && !WindowsRuntimeMarshal.ReportUnhandledError(exc))
			{
				ExceptionDispatchInfo exceptionDispatchInfo = ExceptionDispatchInfo.Capture(exc);
				ThreadPool.QueueUserWorkItem(delegate(object s)
				{
					((ExceptionDispatchInfo)s).Throw();
				}, exceptionDispatchInfo);
			}
		}

		// Token: 0x060041A2 RID: 16802 RVA: 0x000F6436 File Offset: 0x000F4636
		internal override Delegate[] GetDelegateContinuationsForDebugger()
		{
			return new Delegate[] { AsyncMethodBuilderCore.TryGetStateMachineForDebugger(this.m_action) };
		}

		// Token: 0x04001B65 RID: 7013
		private readonly ExecutionContext m_capturedContext;

		// Token: 0x04001B66 RID: 7014
		protected readonly Action m_action;

		// Token: 0x04001B67 RID: 7015
		protected int m_continuationId;

		// Token: 0x04001B68 RID: 7016
		[SecurityCritical]
		private static ContextCallback s_invokeActionCallback;
	}
}
