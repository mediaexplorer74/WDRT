using System;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x0200056C RID: 1388
	internal abstract class TaskContinuation
	{
		// Token: 0x06004185 RID: 16773
		internal abstract void Run(Task completedTask, bool bCanInlineContinuationTask);

		// Token: 0x06004186 RID: 16774 RVA: 0x000F5C60 File Offset: 0x000F3E60
		[SecuritySafeCritical]
		protected static void InlineIfPossibleOrElseQueue(Task task, bool needsProtection)
		{
			if (needsProtection)
			{
				if (!task.MarkStarted())
				{
					return;
				}
			}
			else
			{
				task.m_stateFlags |= 65536;
			}
			try
			{
				if (!task.m_taskScheduler.TryRunInline(task, false))
				{
					task.m_taskScheduler.InternalQueueTask(task);
				}
			}
			catch (Exception ex)
			{
				if (!(ex is ThreadAbortException) || (task.m_stateFlags & 134217728) == 0)
				{
					TaskSchedulerException ex2 = new TaskSchedulerException(ex);
					task.AddException(ex2);
					task.Finish(false);
				}
			}
		}

		// Token: 0x06004187 RID: 16775
		internal abstract Delegate[] GetDelegateContinuationsForDebugger();
	}
}
