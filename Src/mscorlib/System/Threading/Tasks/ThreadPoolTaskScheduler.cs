using System;
using System.Collections.Generic;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x02000578 RID: 1400
	internal sealed class ThreadPoolTaskScheduler : TaskScheduler
	{
		// Token: 0x0600423B RID: 16955 RVA: 0x000F7CFC File Offset: 0x000F5EFC
		internal ThreadPoolTaskScheduler()
		{
			int id = base.Id;
		}

		// Token: 0x0600423C RID: 16956 RVA: 0x000F7D18 File Offset: 0x000F5F18
		private static void LongRunningThreadWork(object obj)
		{
			Task task = obj as Task;
			task.ExecuteEntry(false);
		}

		// Token: 0x0600423D RID: 16957 RVA: 0x000F7D34 File Offset: 0x000F5F34
		[SecurityCritical]
		protected internal override void QueueTask(Task task)
		{
			if ((task.Options & TaskCreationOptions.LongRunning) != TaskCreationOptions.None)
			{
				new Thread(ThreadPoolTaskScheduler.s_longRunningThreadWork)
				{
					IsBackground = true
				}.Start(task);
				return;
			}
			bool flag = (task.Options & TaskCreationOptions.PreferFairness) > TaskCreationOptions.None;
			ThreadPool.UnsafeQueueCustomWorkItem(task, flag);
		}

		// Token: 0x0600423E RID: 16958 RVA: 0x000F7D78 File Offset: 0x000F5F78
		[SecurityCritical]
		protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
		{
			if (taskWasPreviouslyQueued && !ThreadPool.TryPopCustomWorkItem(task))
			{
				return false;
			}
			bool flag = false;
			try
			{
				flag = task.ExecuteEntry(false);
			}
			finally
			{
				if (taskWasPreviouslyQueued)
				{
					this.NotifyWorkItemProgress();
				}
			}
			return flag;
		}

		// Token: 0x0600423F RID: 16959 RVA: 0x000F7DBC File Offset: 0x000F5FBC
		[SecurityCritical]
		protected internal override bool TryDequeue(Task task)
		{
			return ThreadPool.TryPopCustomWorkItem(task);
		}

		// Token: 0x06004240 RID: 16960 RVA: 0x000F7DC4 File Offset: 0x000F5FC4
		[SecurityCritical]
		protected override IEnumerable<Task> GetScheduledTasks()
		{
			return this.FilterTasksFromWorkItems(ThreadPool.GetQueuedWorkItems());
		}

		// Token: 0x06004241 RID: 16961 RVA: 0x000F7DD1 File Offset: 0x000F5FD1
		private IEnumerable<Task> FilterTasksFromWorkItems(IEnumerable<IThreadPoolWorkItem> tpwItems)
		{
			foreach (IThreadPoolWorkItem threadPoolWorkItem in tpwItems)
			{
				if (threadPoolWorkItem is Task)
				{
					yield return (Task)threadPoolWorkItem;
				}
			}
			IEnumerator<IThreadPoolWorkItem> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06004242 RID: 16962 RVA: 0x000F7DE1 File Offset: 0x000F5FE1
		internal override void NotifyWorkItemProgress()
		{
			ThreadPool.NotifyWorkItemProgress();
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06004243 RID: 16963 RVA: 0x000F7DE8 File Offset: 0x000F5FE8
		internal override bool RequiresAtomicStartTransition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001B7F RID: 7039
		private static readonly ParameterizedThreadStart s_longRunningThreadWork = new ParameterizedThreadStart(ThreadPoolTaskScheduler.LongRunningThreadWork);
	}
}
