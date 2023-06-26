using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200055E RID: 1374
	internal class SystemThreadingTasks_TaskDebugView
	{
		// Token: 0x06004160 RID: 16736 RVA: 0x000F54F8 File Offset: 0x000F36F8
		public SystemThreadingTasks_TaskDebugView(Task task)
		{
			this.m_task = task;
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06004161 RID: 16737 RVA: 0x000F5507 File Offset: 0x000F3707
		public object AsyncState
		{
			get
			{
				return this.m_task.AsyncState;
			}
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x06004162 RID: 16738 RVA: 0x000F5514 File Offset: 0x000F3714
		public TaskCreationOptions CreationOptions
		{
			get
			{
				return this.m_task.CreationOptions;
			}
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06004163 RID: 16739 RVA: 0x000F5521 File Offset: 0x000F3721
		public Exception Exception
		{
			get
			{
				return this.m_task.Exception;
			}
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06004164 RID: 16740 RVA: 0x000F552E File Offset: 0x000F372E
		public int Id
		{
			get
			{
				return this.m_task.Id;
			}
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06004165 RID: 16741 RVA: 0x000F553C File Offset: 0x000F373C
		public bool CancellationPending
		{
			get
			{
				return this.m_task.Status == TaskStatus.WaitingToRun && this.m_task.CancellationToken.IsCancellationRequested;
			}
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06004166 RID: 16742 RVA: 0x000F556C File Offset: 0x000F376C
		public TaskStatus Status
		{
			get
			{
				return this.m_task.Status;
			}
		}

		// Token: 0x04001B29 RID: 6953
		private Task m_task;
	}
}
