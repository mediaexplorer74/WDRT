using System;
using System.Diagnostics.Tracing;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Threading
{
	// Token: 0x02000519 RID: 1305
	internal sealed class ThreadPoolWorkQueue
	{
		// Token: 0x06003DD8 RID: 15832 RVA: 0x000E85EC File Offset: 0x000E67EC
		public ThreadPoolWorkQueue()
		{
			this.queueTail = (this.queueHead = new ThreadPoolWorkQueue.QueueSegment());
			this.loggingEnabled = FrameworkEventSource.Log.IsEnabled(EventLevel.Verbose, (EventKeywords)18L);
		}

		// Token: 0x06003DD9 RID: 15833 RVA: 0x000E862B File Offset: 0x000E682B
		[SecurityCritical]
		public ThreadPoolWorkQueueThreadLocals EnsureCurrentThreadHasQueue()
		{
			if (ThreadPoolWorkQueueThreadLocals.threadLocals == null)
			{
				ThreadPoolWorkQueueThreadLocals.threadLocals = new ThreadPoolWorkQueueThreadLocals(this);
			}
			return ThreadPoolWorkQueueThreadLocals.threadLocals;
		}

		// Token: 0x06003DDA RID: 15834 RVA: 0x000E8644 File Offset: 0x000E6844
		[SecurityCritical]
		internal void EnsureThreadRequested()
		{
			int num;
			for (int i = this.numOutstandingThreadRequests; i < ThreadPoolGlobals.processorCount; i = num)
			{
				num = Interlocked.CompareExchange(ref this.numOutstandingThreadRequests, i + 1, i);
				if (num == i)
				{
					ThreadPool.RequestWorkerThread();
					return;
				}
			}
		}

		// Token: 0x06003DDB RID: 15835 RVA: 0x000E8684 File Offset: 0x000E6884
		[SecurityCritical]
		internal void MarkThreadRequestSatisfied()
		{
			int num;
			for (int i = this.numOutstandingThreadRequests; i > 0; i = num)
			{
				num = Interlocked.CompareExchange(ref this.numOutstandingThreadRequests, i - 1, i);
				if (num == i)
				{
					break;
				}
			}
		}

		// Token: 0x06003DDC RID: 15836 RVA: 0x000E86B8 File Offset: 0x000E68B8
		[SecurityCritical]
		public void Enqueue(IThreadPoolWorkItem callback, bool forceGlobal)
		{
			ThreadPoolWorkQueueThreadLocals threadPoolWorkQueueThreadLocals = null;
			if (!forceGlobal)
			{
				threadPoolWorkQueueThreadLocals = ThreadPoolWorkQueueThreadLocals.threadLocals;
			}
			if (this.loggingEnabled)
			{
				FrameworkEventSource.Log.ThreadPoolEnqueueWorkObject(callback);
			}
			if (threadPoolWorkQueueThreadLocals != null)
			{
				threadPoolWorkQueueThreadLocals.workStealingQueue.LocalPush(callback);
			}
			else
			{
				ThreadPoolWorkQueue.QueueSegment queueSegment = this.queueHead;
				while (!queueSegment.TryEnqueue(callback))
				{
					Interlocked.CompareExchange<ThreadPoolWorkQueue.QueueSegment>(ref queueSegment.Next, new ThreadPoolWorkQueue.QueueSegment(), null);
					while (queueSegment.Next != null)
					{
						Interlocked.CompareExchange<ThreadPoolWorkQueue.QueueSegment>(ref this.queueHead, queueSegment.Next, queueSegment);
						queueSegment = this.queueHead;
					}
				}
			}
			this.EnsureThreadRequested();
		}

		// Token: 0x06003DDD RID: 15837 RVA: 0x000E874C File Offset: 0x000E694C
		[SecurityCritical]
		internal bool LocalFindAndPop(IThreadPoolWorkItem callback)
		{
			ThreadPoolWorkQueueThreadLocals threadLocals = ThreadPoolWorkQueueThreadLocals.threadLocals;
			return threadLocals != null && threadLocals.workStealingQueue.LocalFindAndPop(callback);
		}

		// Token: 0x06003DDE RID: 15838 RVA: 0x000E8770 File Offset: 0x000E6970
		[SecurityCritical]
		public void Dequeue(ThreadPoolWorkQueueThreadLocals tl, out IThreadPoolWorkItem callback, out bool missedSteal)
		{
			callback = null;
			missedSteal = false;
			ThreadPoolWorkQueue.WorkStealingQueue workStealingQueue = tl.workStealingQueue;
			workStealingQueue.LocalPop(out callback);
			if (callback == null)
			{
				ThreadPoolWorkQueue.QueueSegment queueSegment = this.queueTail;
				while (!queueSegment.TryDequeue(out callback) && queueSegment.Next != null && queueSegment.IsUsedUp())
				{
					Interlocked.CompareExchange<ThreadPoolWorkQueue.QueueSegment>(ref this.queueTail, queueSegment.Next, queueSegment);
					queueSegment = this.queueTail;
				}
			}
			if (callback == null)
			{
				ThreadPoolWorkQueue.WorkStealingQueue[] array = ThreadPoolWorkQueue.allThreadQueues.Current;
				int num = tl.random.Next(array.Length);
				for (int i = array.Length; i > 0; i--)
				{
					ThreadPoolWorkQueue.WorkStealingQueue workStealingQueue2 = Volatile.Read<ThreadPoolWorkQueue.WorkStealingQueue>(ref array[num % array.Length]);
					if (workStealingQueue2 != null && workStealingQueue2 != workStealingQueue && workStealingQueue2.TrySteal(out callback, ref missedSteal))
					{
						break;
					}
					num++;
				}
			}
		}

		// Token: 0x06003DDF RID: 15839 RVA: 0x000E8834 File Offset: 0x000E6A34
		[SecurityCritical]
		internal static bool Dispatch()
		{
			ThreadPoolWorkQueue workQueue = ThreadPoolGlobals.workQueue;
			int tickCount = Environment.TickCount;
			workQueue.MarkThreadRequestSatisfied();
			workQueue.loggingEnabled = FrameworkEventSource.Log.IsEnabled(EventLevel.Verbose, (EventKeywords)18L);
			bool flag = true;
			IThreadPoolWorkItem threadPoolWorkItem = null;
			try
			{
				ThreadPoolWorkQueueThreadLocals threadPoolWorkQueueThreadLocals = workQueue.EnsureCurrentThreadHasQueue();
				while ((long)(Environment.TickCount - tickCount) < (long)((ulong)ThreadPoolGlobals.tpQuantum))
				{
					try
					{
					}
					finally
					{
						bool flag2 = false;
						workQueue.Dequeue(threadPoolWorkQueueThreadLocals, out threadPoolWorkItem, out flag2);
						if (threadPoolWorkItem == null)
						{
							flag = flag2;
						}
						else
						{
							workQueue.EnsureThreadRequested();
						}
					}
					if (threadPoolWorkItem == null)
					{
						return true;
					}
					if (workQueue.loggingEnabled)
					{
						FrameworkEventSource.Log.ThreadPoolDequeueWorkObject(threadPoolWorkItem);
					}
					if (ThreadPoolGlobals.enableWorkerTracking)
					{
						bool flag3 = false;
						try
						{
							try
							{
							}
							finally
							{
								ThreadPool.ReportThreadStatus(true);
								flag3 = true;
							}
							threadPoolWorkItem.ExecuteWorkItem();
							threadPoolWorkItem = null;
							goto IL_A6;
						}
						finally
						{
							if (flag3)
							{
								ThreadPool.ReportThreadStatus(false);
							}
						}
						goto IL_9E;
					}
					goto IL_9E;
					IL_A6:
					if (!ThreadPool.NotifyWorkItemComplete())
					{
						return false;
					}
					continue;
					IL_9E:
					threadPoolWorkItem.ExecuteWorkItem();
					threadPoolWorkItem = null;
					goto IL_A6;
				}
				return true;
			}
			catch (ThreadAbortException ex)
			{
				if (threadPoolWorkItem != null)
				{
					threadPoolWorkItem.MarkAborted(ex);
				}
				flag = false;
			}
			finally
			{
				if (flag)
				{
					workQueue.EnsureThreadRequested();
				}
			}
			return true;
		}

		// Token: 0x04001A0E RID: 6670
		internal volatile ThreadPoolWorkQueue.QueueSegment queueHead;

		// Token: 0x04001A0F RID: 6671
		internal volatile ThreadPoolWorkQueue.QueueSegment queueTail;

		// Token: 0x04001A10 RID: 6672
		internal bool loggingEnabled;

		// Token: 0x04001A11 RID: 6673
		internal static ThreadPoolWorkQueue.SparseArray<ThreadPoolWorkQueue.WorkStealingQueue> allThreadQueues = new ThreadPoolWorkQueue.SparseArray<ThreadPoolWorkQueue.WorkStealingQueue>(16);

		// Token: 0x04001A12 RID: 6674
		private volatile int numOutstandingThreadRequests;

		// Token: 0x02000BEF RID: 3055
		internal class SparseArray<T> where T : class
		{
			// Token: 0x06006F81 RID: 28545 RVA: 0x0018148A File Offset: 0x0017F68A
			internal SparseArray(int initialSize)
			{
				this.m_array = new T[initialSize];
			}

			// Token: 0x17001322 RID: 4898
			// (get) Token: 0x06006F82 RID: 28546 RVA: 0x001814A0 File Offset: 0x0017F6A0
			internal T[] Current
			{
				get
				{
					return this.m_array;
				}
			}

			// Token: 0x06006F83 RID: 28547 RVA: 0x001814AC File Offset: 0x0017F6AC
			internal int Add(T e)
			{
				for (;;)
				{
					T[] array = this.m_array;
					T[] array2 = array;
					lock (array2)
					{
						for (int i = 0; i < array.Length; i++)
						{
							if (array[i] == null)
							{
								Volatile.Write<T>(ref array[i], e);
								return i;
							}
							if (i == array.Length - 1 && array == this.m_array)
							{
								T[] array3 = new T[array.Length * 2];
								Array.Copy(array, array3, i + 1);
								array3[i + 1] = e;
								this.m_array = array3;
								return i + 1;
							}
						}
						continue;
					}
					break;
				}
				int num;
				return num;
			}

			// Token: 0x06006F84 RID: 28548 RVA: 0x00181564 File Offset: 0x0017F764
			internal void Remove(T e)
			{
				T[] array = this.m_array;
				T[] array2 = array;
				lock (array2)
				{
					for (int i = 0; i < this.m_array.Length; i++)
					{
						if (this.m_array[i] == e)
						{
							Volatile.Write<T>(ref this.m_array[i], default(T));
							break;
						}
					}
				}
			}

			// Token: 0x04003621 RID: 13857
			private volatile T[] m_array;
		}

		// Token: 0x02000BF0 RID: 3056
		internal class WorkStealingQueue
		{
			// Token: 0x06006F85 RID: 28549 RVA: 0x001815F4 File Offset: 0x0017F7F4
			public void LocalPush(IThreadPoolWorkItem obj)
			{
				int num = this.m_tailIndex;
				if (num == 2147483647)
				{
					bool flag = false;
					try
					{
						this.m_foreignLock.Enter(ref flag);
						if (this.m_tailIndex == 2147483647)
						{
							this.m_headIndex &= this.m_mask;
							num = (this.m_tailIndex &= this.m_mask);
						}
					}
					finally
					{
						if (flag)
						{
							this.m_foreignLock.Exit(true);
						}
					}
				}
				if (num < this.m_headIndex + this.m_mask)
				{
					Volatile.Write<IThreadPoolWorkItem>(ref this.m_array[num & this.m_mask], obj);
					this.m_tailIndex = num + 1;
					return;
				}
				bool flag2 = false;
				try
				{
					this.m_foreignLock.Enter(ref flag2);
					int headIndex = this.m_headIndex;
					int num2 = this.m_tailIndex - this.m_headIndex;
					if (num2 >= this.m_mask)
					{
						IThreadPoolWorkItem[] array = new IThreadPoolWorkItem[this.m_array.Length << 1];
						for (int i = 0; i < this.m_array.Length; i++)
						{
							array[i] = this.m_array[(i + headIndex) & this.m_mask];
						}
						this.m_array = array;
						this.m_headIndex = 0;
						num = (this.m_tailIndex = num2);
						this.m_mask = (this.m_mask << 1) | 1;
					}
					Volatile.Write<IThreadPoolWorkItem>(ref this.m_array[num & this.m_mask], obj);
					this.m_tailIndex = num + 1;
				}
				finally
				{
					if (flag2)
					{
						this.m_foreignLock.Exit(false);
					}
				}
			}

			// Token: 0x06006F86 RID: 28550 RVA: 0x001817BC File Offset: 0x0017F9BC
			public bool LocalFindAndPop(IThreadPoolWorkItem obj)
			{
				if (this.m_array[(this.m_tailIndex - 1) & this.m_mask] == obj)
				{
					IThreadPoolWorkItem threadPoolWorkItem;
					return this.LocalPop(out threadPoolWorkItem);
				}
				for (int i = this.m_tailIndex - 2; i >= this.m_headIndex; i--)
				{
					if (this.m_array[i & this.m_mask] == obj)
					{
						bool flag = false;
						try
						{
							this.m_foreignLock.Enter(ref flag);
							if (this.m_array[i & this.m_mask] == null)
							{
								return false;
							}
							Volatile.Write<IThreadPoolWorkItem>(ref this.m_array[i & this.m_mask], null);
							if (i == this.m_tailIndex)
							{
								this.m_tailIndex--;
							}
							else if (i == this.m_headIndex)
							{
								this.m_headIndex++;
							}
							return true;
						}
						finally
						{
							if (flag)
							{
								this.m_foreignLock.Exit(false);
							}
						}
					}
				}
				return false;
			}

			// Token: 0x06006F87 RID: 28551 RVA: 0x001818DC File Offset: 0x0017FADC
			public bool LocalPop(out IThreadPoolWorkItem obj)
			{
				int num3;
				for (;;)
				{
					int num = this.m_tailIndex;
					if (this.m_headIndex >= num)
					{
						break;
					}
					num--;
					Interlocked.Exchange(ref this.m_tailIndex, num);
					if (this.m_headIndex > num)
					{
						bool flag = false;
						bool flag2;
						try
						{
							this.m_foreignLock.Enter(ref flag);
							if (this.m_headIndex <= num)
							{
								int num2 = num & this.m_mask;
								obj = Volatile.Read<IThreadPoolWorkItem>(ref this.m_array[num2]);
								if (obj == null)
								{
									continue;
								}
								this.m_array[num2] = null;
								flag2 = true;
							}
							else
							{
								this.m_tailIndex = num + 1;
								obj = null;
								flag2 = false;
							}
						}
						finally
						{
							if (flag)
							{
								this.m_foreignLock.Exit(false);
							}
						}
						return flag2;
					}
					num3 = num & this.m_mask;
					obj = Volatile.Read<IThreadPoolWorkItem>(ref this.m_array[num3]);
					if (obj != null)
					{
						goto Block_2;
					}
				}
				obj = null;
				return false;
				Block_2:
				this.m_array[num3] = null;
				return true;
			}

			// Token: 0x06006F88 RID: 28552 RVA: 0x001819D8 File Offset: 0x0017FBD8
			public bool TrySteal(out IThreadPoolWorkItem obj, ref bool missedSteal)
			{
				return this.TrySteal(out obj, ref missedSteal, 0);
			}

			// Token: 0x06006F89 RID: 28553 RVA: 0x001819E4 File Offset: 0x0017FBE4
			private bool TrySteal(out IThreadPoolWorkItem obj, ref bool missedSteal, int millisecondsTimeout)
			{
				obj = null;
				while (this.m_headIndex < this.m_tailIndex)
				{
					bool flag = false;
					try
					{
						this.m_foreignLock.TryEnter(millisecondsTimeout, ref flag);
						if (flag)
						{
							int headIndex = this.m_headIndex;
							Interlocked.Exchange(ref this.m_headIndex, headIndex + 1);
							if (headIndex < this.m_tailIndex)
							{
								int num = headIndex & this.m_mask;
								obj = Volatile.Read<IThreadPoolWorkItem>(ref this.m_array[num]);
								if (obj == null)
								{
									continue;
								}
								this.m_array[num] = null;
								return true;
							}
							else
							{
								this.m_headIndex = headIndex;
								obj = null;
								missedSteal = true;
							}
						}
						else
						{
							missedSteal = true;
						}
					}
					finally
					{
						if (flag)
						{
							this.m_foreignLock.Exit(false);
						}
					}
					return false;
				}
				return false;
			}

			// Token: 0x04003622 RID: 13858
			private const int INITIAL_SIZE = 32;

			// Token: 0x04003623 RID: 13859
			internal volatile IThreadPoolWorkItem[] m_array = new IThreadPoolWorkItem[32];

			// Token: 0x04003624 RID: 13860
			private volatile int m_mask = 31;

			// Token: 0x04003625 RID: 13861
			private const int START_INDEX = 0;

			// Token: 0x04003626 RID: 13862
			private volatile int m_headIndex;

			// Token: 0x04003627 RID: 13863
			private volatile int m_tailIndex;

			// Token: 0x04003628 RID: 13864
			private SpinLock m_foreignLock = new SpinLock(false);
		}

		// Token: 0x02000BF1 RID: 3057
		internal class QueueSegment
		{
			// Token: 0x06006F8B RID: 28555 RVA: 0x00181ADC File Offset: 0x0017FCDC
			private void GetIndexes(out int upper, out int lower)
			{
				int num = this.indexes;
				upper = (num >> 16) & 65535;
				lower = num & 65535;
			}

			// Token: 0x06006F8C RID: 28556 RVA: 0x00181B08 File Offset: 0x0017FD08
			private bool CompareExchangeIndexes(ref int prevUpper, int newUpper, ref int prevLower, int newLower)
			{
				int num = (prevUpper << 16) | (prevLower & 65535);
				int num2 = (newUpper << 16) | (newLower & 65535);
				int num3 = Interlocked.CompareExchange(ref this.indexes, num2, num);
				prevUpper = (num3 >> 16) & 65535;
				prevLower = num3 & 65535;
				return num3 == num;
			}

			// Token: 0x06006F8D RID: 28557 RVA: 0x00181B59 File Offset: 0x0017FD59
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			public QueueSegment()
			{
				this.nodes = new IThreadPoolWorkItem[256];
			}

			// Token: 0x06006F8E RID: 28558 RVA: 0x00181B74 File Offset: 0x0017FD74
			public bool IsUsedUp()
			{
				int num;
				int num2;
				this.GetIndexes(out num, out num2);
				return num == this.nodes.Length && num2 == this.nodes.Length;
			}

			// Token: 0x06006F8F RID: 28559 RVA: 0x00181BA4 File Offset: 0x0017FDA4
			public bool TryEnqueue(IThreadPoolWorkItem node)
			{
				int num;
				int num2;
				this.GetIndexes(out num, out num2);
				while (num != this.nodes.Length)
				{
					if (this.CompareExchangeIndexes(ref num, num + 1, ref num2, num2))
					{
						Volatile.Write<IThreadPoolWorkItem>(ref this.nodes[num], node);
						return true;
					}
				}
				return false;
			}

			// Token: 0x06006F90 RID: 28560 RVA: 0x00181BEC File Offset: 0x0017FDEC
			public bool TryDequeue(out IThreadPoolWorkItem node)
			{
				int num;
				int num2;
				this.GetIndexes(out num, out num2);
				while (num2 != num)
				{
					if (this.CompareExchangeIndexes(ref num, num, ref num2, num2 + 1))
					{
						SpinWait spinWait = default(SpinWait);
						for (;;)
						{
							IThreadPoolWorkItem threadPoolWorkItem;
							node = (threadPoolWorkItem = Volatile.Read<IThreadPoolWorkItem>(ref this.nodes[num2]));
							if (threadPoolWorkItem != null)
							{
								break;
							}
							spinWait.SpinOnce();
						}
						this.nodes[num2] = null;
						return true;
					}
				}
				node = null;
				return false;
			}

			// Token: 0x04003629 RID: 13865
			internal readonly IThreadPoolWorkItem[] nodes;

			// Token: 0x0400362A RID: 13866
			private const int QueueSegmentLength = 256;

			// Token: 0x0400362B RID: 13867
			private volatile int indexes;

			// Token: 0x0400362C RID: 13868
			public volatile ThreadPoolWorkQueue.QueueSegment Next;

			// Token: 0x0400362D RID: 13869
			private const int SixteenBits = 65535;
		}
	}
}
