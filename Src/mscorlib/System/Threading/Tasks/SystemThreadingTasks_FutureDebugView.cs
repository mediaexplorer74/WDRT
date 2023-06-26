using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200054B RID: 1355
	internal class SystemThreadingTasks_FutureDebugView<TResult>
	{
		// Token: 0x06003FE1 RID: 16353 RVA: 0x000EE797 File Offset: 0x000EC997
		public SystemThreadingTasks_FutureDebugView(Task<TResult> task)
		{
			this.m_task = task;
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06003FE2 RID: 16354 RVA: 0x000EE7A8 File Offset: 0x000EC9A8
		public TResult Result
		{
			get
			{
				if (this.m_task.Status != TaskStatus.RanToCompletion)
				{
					return default(TResult);
				}
				return this.m_task.Result;
			}
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06003FE3 RID: 16355 RVA: 0x000EE7D8 File Offset: 0x000EC9D8
		public object AsyncState
		{
			get
			{
				return this.m_task.AsyncState;
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06003FE4 RID: 16356 RVA: 0x000EE7E5 File Offset: 0x000EC9E5
		public TaskCreationOptions CreationOptions
		{
			get
			{
				return this.m_task.CreationOptions;
			}
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06003FE5 RID: 16357 RVA: 0x000EE7F2 File Offset: 0x000EC9F2
		public Exception Exception
		{
			get
			{
				return this.m_task.Exception;
			}
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06003FE6 RID: 16358 RVA: 0x000EE7FF File Offset: 0x000EC9FF
		public int Id
		{
			get
			{
				return this.m_task.Id;
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06003FE7 RID: 16359 RVA: 0x000EE80C File Offset: 0x000ECA0C
		public bool CancellationPending
		{
			get
			{
				return this.m_task.Status == TaskStatus.WaitingToRun && this.m_task.CancellationToken.IsCancellationRequested;
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06003FE8 RID: 16360 RVA: 0x000EE83C File Offset: 0x000ECA3C
		public TaskStatus Status
		{
			get
			{
				return this.m_task.Status;
			}
		}

		// Token: 0x04001AC9 RID: 6857
		private Task<TResult> m_task;
	}
}
