using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x0200051A RID: 1306
	internal sealed class ThreadPoolWorkQueueThreadLocals
	{
		// Token: 0x06003DE1 RID: 15841 RVA: 0x000E897A File Offset: 0x000E6B7A
		public ThreadPoolWorkQueueThreadLocals(ThreadPoolWorkQueue tpq)
		{
			this.workQueue = tpq;
			this.workStealingQueue = new ThreadPoolWorkQueue.WorkStealingQueue();
			ThreadPoolWorkQueue.allThreadQueues.Add(this.workStealingQueue);
		}

		// Token: 0x06003DE2 RID: 15842 RVA: 0x000E89BC File Offset: 0x000E6BBC
		[SecurityCritical]
		private void CleanUp()
		{
			if (this.workStealingQueue != null)
			{
				if (this.workQueue != null)
				{
					bool flag = false;
					while (!flag)
					{
						try
						{
						}
						finally
						{
							IThreadPoolWorkItem threadPoolWorkItem = null;
							if (this.workStealingQueue.LocalPop(out threadPoolWorkItem))
							{
								this.workQueue.Enqueue(threadPoolWorkItem, true);
							}
							else
							{
								flag = true;
							}
						}
					}
				}
				ThreadPoolWorkQueue.allThreadQueues.Remove(this.workStealingQueue);
			}
		}

		// Token: 0x06003DE3 RID: 15843 RVA: 0x000E8A28 File Offset: 0x000E6C28
		[SecuritySafeCritical]
		~ThreadPoolWorkQueueThreadLocals()
		{
			if (!Environment.HasShutdownStarted && !AppDomain.CurrentDomain.IsFinalizingForUnload())
			{
				this.CleanUp();
			}
		}

		// Token: 0x04001A13 RID: 6675
		[ThreadStatic]
		[SecurityCritical]
		public static ThreadPoolWorkQueueThreadLocals threadLocals;

		// Token: 0x04001A14 RID: 6676
		public readonly ThreadPoolWorkQueue workQueue;

		// Token: 0x04001A15 RID: 6677
		public readonly ThreadPoolWorkQueue.WorkStealingQueue workStealingQueue;

		// Token: 0x04001A16 RID: 6678
		public readonly Random random = new Random(Thread.CurrentThread.ManagedThreadId);
	}
}
