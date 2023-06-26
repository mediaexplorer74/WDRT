using System;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x0200056E RID: 1390
	internal sealed class SynchronizationContextAwaitTaskContinuation : AwaitTaskContinuation
	{
		// Token: 0x0600418C RID: 16780 RVA: 0x000F5E54 File Offset: 0x000F4054
		[SecurityCritical]
		internal SynchronizationContextAwaitTaskContinuation(SynchronizationContext context, Action action, bool flowExecutionContext, ref StackCrawlMark stackMark)
			: base(action, flowExecutionContext, ref stackMark)
		{
			this.m_syncContext = context;
		}

		// Token: 0x0600418D RID: 16781 RVA: 0x000F5E68 File Offset: 0x000F4068
		[SecuritySafeCritical]
		internal sealed override void Run(Task task, bool canInlineContinuationTask)
		{
			if (canInlineContinuationTask && this.m_syncContext == SynchronizationContext.CurrentNoFlow)
			{
				base.RunCallback(AwaitTaskContinuation.GetInvokeActionCallback(), this.m_action, ref Task.t_currentTask);
				return;
			}
			TplEtwProvider log = TplEtwProvider.Log;
			if (log.IsEnabled())
			{
				this.m_continuationId = Task.NewId();
				log.AwaitTaskContinuationScheduled((task.ExecutingTaskScheduler ?? TaskScheduler.Default).Id, task.Id, this.m_continuationId);
			}
			base.RunCallback(SynchronizationContextAwaitTaskContinuation.GetPostActionCallback(), this, ref Task.t_currentTask);
		}

		// Token: 0x0600418E RID: 16782 RVA: 0x000F5EEC File Offset: 0x000F40EC
		[SecurityCritical]
		private static void PostAction(object state)
		{
			SynchronizationContextAwaitTaskContinuation synchronizationContextAwaitTaskContinuation = (SynchronizationContextAwaitTaskContinuation)state;
			TplEtwProvider log = TplEtwProvider.Log;
			if (log.TasksSetActivityIds && synchronizationContextAwaitTaskContinuation.m_continuationId != 0)
			{
				synchronizationContextAwaitTaskContinuation.m_syncContext.Post(SynchronizationContextAwaitTaskContinuation.s_postCallback, SynchronizationContextAwaitTaskContinuation.GetActionLogDelegate(synchronizationContextAwaitTaskContinuation.m_continuationId, synchronizationContextAwaitTaskContinuation.m_action));
				return;
			}
			synchronizationContextAwaitTaskContinuation.m_syncContext.Post(SynchronizationContextAwaitTaskContinuation.s_postCallback, synchronizationContextAwaitTaskContinuation.m_action);
		}

		// Token: 0x0600418F RID: 16783 RVA: 0x000F5F50 File Offset: 0x000F4150
		private static Action GetActionLogDelegate(int continuationId, Action action)
		{
			return delegate
			{
				Guid guid = TplEtwProvider.CreateGuidForTaskID(continuationId);
				Guid guid2;
				EventSource.SetCurrentThreadActivityId(guid, out guid2);
				try
				{
					action();
				}
				finally
				{
					EventSource.SetCurrentThreadActivityId(guid2);
				}
			};
		}

		// Token: 0x06004190 RID: 16784 RVA: 0x000F5F80 File Offset: 0x000F4180
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static ContextCallback GetPostActionCallback()
		{
			ContextCallback contextCallback = SynchronizationContextAwaitTaskContinuation.s_postActionCallback;
			if (contextCallback == null)
			{
				contextCallback = (SynchronizationContextAwaitTaskContinuation.s_postActionCallback = new ContextCallback(SynchronizationContextAwaitTaskContinuation.PostAction));
			}
			return contextCallback;
		}

		// Token: 0x04001B61 RID: 7009
		private static readonly SendOrPostCallback s_postCallback = delegate(object state)
		{
			((Action)state)();
		};

		// Token: 0x04001B62 RID: 7010
		[SecurityCritical]
		private static ContextCallback s_postActionCallback;

		// Token: 0x04001B63 RID: 7011
		private readonly SynchronizationContext m_syncContext;
	}
}
