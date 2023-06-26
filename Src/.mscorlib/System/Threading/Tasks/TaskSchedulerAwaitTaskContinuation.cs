using System;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x0200056F RID: 1391
	internal sealed class TaskSchedulerAwaitTaskContinuation : AwaitTaskContinuation
	{
		// Token: 0x06004192 RID: 16786 RVA: 0x000F5FC1 File Offset: 0x000F41C1
		[SecurityCritical]
		internal TaskSchedulerAwaitTaskContinuation(TaskScheduler scheduler, Action action, bool flowExecutionContext, ref StackCrawlMark stackMark)
			: base(action, flowExecutionContext, ref stackMark)
		{
			this.m_scheduler = scheduler;
		}

		// Token: 0x06004193 RID: 16787 RVA: 0x000F5FD4 File Offset: 0x000F41D4
		internal sealed override void Run(Task ignored, bool canInlineContinuationTask)
		{
			if (this.m_scheduler == TaskScheduler.Default)
			{
				base.Run(ignored, canInlineContinuationTask);
				return;
			}
			bool flag = canInlineContinuationTask && (TaskScheduler.InternalCurrent == this.m_scheduler || Thread.CurrentThread.IsThreadPoolThread);
			Task task = base.CreateTask(delegate(object state)
			{
				try
				{
					((Action)state)();
				}
				catch (Exception ex)
				{
					AwaitTaskContinuation.ThrowAsyncIfNecessary(ex);
				}
			}, this.m_action, this.m_scheduler);
			if (flag)
			{
				TaskContinuation.InlineIfPossibleOrElseQueue(task, false);
				return;
			}
			try
			{
				task.ScheduleAndStart(false);
			}
			catch (TaskSchedulerException)
			{
			}
		}

		// Token: 0x04001B64 RID: 7012
		private readonly TaskScheduler m_scheduler;
	}
}
