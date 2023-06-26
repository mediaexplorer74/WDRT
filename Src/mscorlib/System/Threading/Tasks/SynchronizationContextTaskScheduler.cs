using System;
using System.Collections.Generic;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x02000576 RID: 1398
	internal sealed class SynchronizationContextTaskScheduler : TaskScheduler
	{
		// Token: 0x06004230 RID: 16944 RVA: 0x000F7C40 File Offset: 0x000F5E40
		internal SynchronizationContextTaskScheduler()
		{
			SynchronizationContext synchronizationContext = SynchronizationContext.Current;
			if (synchronizationContext == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("TaskScheduler_FromCurrentSynchronizationContext_NoCurrent"));
			}
			this.m_synchronizationContext = synchronizationContext;
		}

		// Token: 0x06004231 RID: 16945 RVA: 0x000F7C73 File Offset: 0x000F5E73
		[SecurityCritical]
		protected internal override void QueueTask(Task task)
		{
			this.m_synchronizationContext.Post(SynchronizationContextTaskScheduler.s_postCallback, task);
		}

		// Token: 0x06004232 RID: 16946 RVA: 0x000F7C86 File Offset: 0x000F5E86
		[SecurityCritical]
		protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
		{
			return SynchronizationContext.Current == this.m_synchronizationContext && base.TryExecuteTask(task);
		}

		// Token: 0x06004233 RID: 16947 RVA: 0x000F7C9E File Offset: 0x000F5E9E
		[SecurityCritical]
		protected override IEnumerable<Task> GetScheduledTasks()
		{
			return null;
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06004234 RID: 16948 RVA: 0x000F7CA1 File Offset: 0x000F5EA1
		public override int MaximumConcurrencyLevel
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06004235 RID: 16949 RVA: 0x000F7CA4 File Offset: 0x000F5EA4
		private static void PostCallback(object obj)
		{
			Task task = (Task)obj;
			task.ExecuteEntry(true);
		}

		// Token: 0x04001B7B RID: 7035
		private SynchronizationContext m_synchronizationContext;

		// Token: 0x04001B7C RID: 7036
		private static SendOrPostCallback s_postCallback = new SendOrPostCallback(SynchronizationContextTaskScheduler.PostCallback);
	}
}
