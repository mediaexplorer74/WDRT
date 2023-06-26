using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200021C RID: 540
	internal static class TimerThread
	{
		// Token: 0x060013D5 RID: 5077 RVA: 0x0006923C File Offset: 0x0006743C
		static TimerThread()
		{
			AppDomain.CurrentDomain.DomainUnload += TimerThread.OnDomainUnload;
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x000692B4 File Offset: 0x000674B4
		internal static TimerThread.Queue CreateQueue(int durationMilliseconds)
		{
			if (durationMilliseconds == -1)
			{
				return new TimerThread.InfiniteTimerQueue();
			}
			if (durationMilliseconds < 0)
			{
				throw new ArgumentOutOfRangeException("durationMilliseconds");
			}
			LinkedList<WeakReference> linkedList = TimerThread.s_NewQueues;
			TimerThread.TimerQueue timerQueue;
			lock (linkedList)
			{
				timerQueue = new TimerThread.TimerQueue(durationMilliseconds);
				WeakReference weakReference = new WeakReference(timerQueue);
				TimerThread.s_NewQueues.AddLast(weakReference);
			}
			return timerQueue;
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x00069324 File Offset: 0x00067524
		internal static TimerThread.Queue GetOrCreateQueue(int durationMilliseconds)
		{
			if (durationMilliseconds == -1)
			{
				return new TimerThread.InfiniteTimerQueue();
			}
			if (durationMilliseconds < 0)
			{
				throw new ArgumentOutOfRangeException("durationMilliseconds");
			}
			WeakReference weakReference = (WeakReference)TimerThread.s_QueuesCache[durationMilliseconds];
			TimerThread.TimerQueue timerQueue;
			if (weakReference == null || (timerQueue = (TimerThread.TimerQueue)weakReference.Target) == null)
			{
				LinkedList<WeakReference> linkedList = TimerThread.s_NewQueues;
				lock (linkedList)
				{
					weakReference = (WeakReference)TimerThread.s_QueuesCache[durationMilliseconds];
					if (weakReference == null || (timerQueue = (TimerThread.TimerQueue)weakReference.Target) == null)
					{
						timerQueue = new TimerThread.TimerQueue(durationMilliseconds);
						weakReference = new WeakReference(timerQueue);
						TimerThread.s_NewQueues.AddLast(weakReference);
						TimerThread.s_QueuesCache[durationMilliseconds] = weakReference;
						if (++TimerThread.s_CacheScanIteration % 32 == 0)
						{
							List<int> list = new List<int>();
							foreach (object obj in TimerThread.s_QueuesCache)
							{
								DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
								if (((WeakReference)dictionaryEntry.Value).Target == null)
								{
									list.Add((int)dictionaryEntry.Key);
								}
							}
							for (int i = 0; i < list.Count; i++)
							{
								TimerThread.s_QueuesCache.Remove(list[i]);
							}
						}
					}
				}
			}
			return timerQueue;
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x000694C8 File Offset: 0x000676C8
		private static void Prod()
		{
			TimerThread.s_ThreadReadyEvent.Set();
			if (Interlocked.CompareExchange(ref TimerThread.s_ThreadState, 1, 0) == 0)
			{
				new Thread(new ThreadStart(TimerThread.ThreadProc)).Start();
			}
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x00069508 File Offset: 0x00067708
		private static void ThreadProc()
		{
			Thread.CurrentThread.IsBackground = true;
			LinkedList<WeakReference> linkedList = TimerThread.s_Queues;
			lock (linkedList)
			{
				if (Interlocked.CompareExchange(ref TimerThread.s_ThreadState, 1, 1) == 1)
				{
					bool flag2 = true;
					while (flag2)
					{
						try
						{
							TimerThread.s_ThreadReadyEvent.Reset();
							for (;;)
							{
								if (TimerThread.s_NewQueues.Count > 0)
								{
									LinkedList<WeakReference> linkedList2 = TimerThread.s_NewQueues;
									lock (linkedList2)
									{
										for (LinkedListNode<WeakReference> linkedListNode = TimerThread.s_NewQueues.First; linkedListNode != null; linkedListNode = TimerThread.s_NewQueues.First)
										{
											TimerThread.s_NewQueues.Remove(linkedListNode);
											TimerThread.s_Queues.AddLast(linkedListNode);
										}
									}
								}
								int tickCount = Environment.TickCount;
								int num = 0;
								bool flag4 = false;
								LinkedListNode<WeakReference> linkedListNode2 = TimerThread.s_Queues.First;
								while (linkedListNode2 != null)
								{
									TimerThread.TimerQueue timerQueue = (TimerThread.TimerQueue)linkedListNode2.Value.Target;
									if (timerQueue == null)
									{
										LinkedListNode<WeakReference> next = linkedListNode2.Next;
										TimerThread.s_Queues.Remove(linkedListNode2);
										linkedListNode2 = next;
									}
									else
									{
										int num2;
										if (timerQueue.Fire(out num2) && (!flag4 || TimerThread.IsTickBetween(tickCount, num, num2)))
										{
											num = num2;
											flag4 = true;
										}
										linkedListNode2 = linkedListNode2.Next;
									}
								}
								int tickCount2 = Environment.TickCount;
								int num3 = (int)(flag4 ? (TimerThread.IsTickBetween(tickCount, num, tickCount2) ? (Math.Min((uint)(num - tickCount2), 2147483632U) + 15U) : 0U) : 30000U);
								int num4 = WaitHandle.WaitAny(TimerThread.s_ThreadEvents, num3, false);
								if (num4 == 0)
								{
									break;
								}
								if (num4 == 258 && !flag4)
								{
									Interlocked.CompareExchange(ref TimerThread.s_ThreadState, 0, 1);
									if (!TimerThread.s_ThreadReadyEvent.WaitOne(0, false) || Interlocked.CompareExchange(ref TimerThread.s_ThreadState, 1, 0) != 0)
									{
										goto IL_1AC;
									}
								}
							}
							flag2 = false;
							continue;
							IL_1AC:
							flag2 = false;
						}
						catch (Exception ex)
						{
							if (NclUtilities.IsFatal(ex))
							{
								throw;
							}
							if (Logging.On)
							{
								Logging.PrintError(Logging.Web, "TimerThread#" + Thread.CurrentThread.ManagedThreadId.ToString(NumberFormatInfo.InvariantInfo) + "::ThreadProc() - Exception:" + ex.ToString());
							}
							Thread.Sleep(1000);
						}
					}
				}
			}
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x0006977C File Offset: 0x0006797C
		private static void StopTimerThread()
		{
			Interlocked.Exchange(ref TimerThread.s_ThreadState, 2);
			TimerThread.s_ThreadShutdownEvent.Set();
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x00069795 File Offset: 0x00067995
		private static bool IsTickBetween(int start, int end, int comparand)
		{
			return start <= comparand == end <= comparand != start <= end;
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x000697B4 File Offset: 0x000679B4
		private static void OnDomainUnload(object sender, EventArgs e)
		{
			try
			{
				TimerThread.StopTimerThread();
			}
			catch
			{
			}
		}

		// Token: 0x040015D6 RID: 5590
		private const int c_ThreadIdleTimeoutMilliseconds = 30000;

		// Token: 0x040015D7 RID: 5591
		private const int c_CacheScanPerIterations = 32;

		// Token: 0x040015D8 RID: 5592
		private const int c_TickCountResolution = 15;

		// Token: 0x040015D9 RID: 5593
		private static LinkedList<WeakReference> s_Queues = new LinkedList<WeakReference>();

		// Token: 0x040015DA RID: 5594
		private static LinkedList<WeakReference> s_NewQueues = new LinkedList<WeakReference>();

		// Token: 0x040015DB RID: 5595
		private static int s_ThreadState = 0;

		// Token: 0x040015DC RID: 5596
		private static AutoResetEvent s_ThreadReadyEvent = new AutoResetEvent(false);

		// Token: 0x040015DD RID: 5597
		private static ManualResetEvent s_ThreadShutdownEvent = new ManualResetEvent(false);

		// Token: 0x040015DE RID: 5598
		private static WaitHandle[] s_ThreadEvents = new WaitHandle[]
		{
			TimerThread.s_ThreadShutdownEvent,
			TimerThread.s_ThreadReadyEvent
		};

		// Token: 0x040015DF RID: 5599
		private static int s_CacheScanIteration;

		// Token: 0x040015E0 RID: 5600
		private static Hashtable s_QueuesCache = new Hashtable();

		// Token: 0x0200075C RID: 1884
		internal abstract class Queue
		{
			// Token: 0x060041F9 RID: 16889 RVA: 0x00111C98 File Offset: 0x0010FE98
			internal Queue(int durationMilliseconds)
			{
				this.m_DurationMilliseconds = durationMilliseconds;
			}

			// Token: 0x17000F16 RID: 3862
			// (get) Token: 0x060041FA RID: 16890 RVA: 0x00111CA7 File Offset: 0x0010FEA7
			internal int Duration
			{
				get
				{
					return this.m_DurationMilliseconds;
				}
			}

			// Token: 0x060041FB RID: 16891 RVA: 0x00111CAF File Offset: 0x0010FEAF
			internal TimerThread.Timer CreateTimer()
			{
				return this.CreateTimer(null, null);
			}

			// Token: 0x060041FC RID: 16892
			internal abstract TimerThread.Timer CreateTimer(TimerThread.Callback callback, object context);

			// Token: 0x04003215 RID: 12821
			private readonly int m_DurationMilliseconds;
		}

		// Token: 0x0200075D RID: 1885
		internal abstract class Timer : IDisposable
		{
			// Token: 0x060041FD RID: 16893 RVA: 0x00111CB9 File Offset: 0x0010FEB9
			internal Timer(int durationMilliseconds)
			{
				this.m_DurationMilliseconds = durationMilliseconds;
				this.m_StartTimeMilliseconds = Environment.TickCount;
			}

			// Token: 0x17000F17 RID: 3863
			// (get) Token: 0x060041FE RID: 16894 RVA: 0x00111CD3 File Offset: 0x0010FED3
			internal int Duration
			{
				get
				{
					return this.m_DurationMilliseconds;
				}
			}

			// Token: 0x17000F18 RID: 3864
			// (get) Token: 0x060041FF RID: 16895 RVA: 0x00111CDB File Offset: 0x0010FEDB
			internal int StartTime
			{
				get
				{
					return this.m_StartTimeMilliseconds;
				}
			}

			// Token: 0x17000F19 RID: 3865
			// (get) Token: 0x06004200 RID: 16896 RVA: 0x00111CE3 File Offset: 0x0010FEE3
			internal int Expiration
			{
				get
				{
					return this.m_StartTimeMilliseconds + this.m_DurationMilliseconds;
				}
			}

			// Token: 0x17000F1A RID: 3866
			// (get) Token: 0x06004201 RID: 16897 RVA: 0x00111CF4 File Offset: 0x0010FEF4
			internal int TimeRemaining
			{
				get
				{
					if (this.HasExpired)
					{
						return 0;
					}
					if (this.Duration == -1)
					{
						return -1;
					}
					int tickCount = Environment.TickCount;
					int num = (int)(TimerThread.IsTickBetween(this.StartTime, this.Expiration, tickCount) ? Math.Min((uint)(this.Expiration - tickCount), 2147483647U) : 0U);
					if (num >= 2)
					{
						return num;
					}
					return num + 1;
				}
			}

			// Token: 0x06004202 RID: 16898
			internal abstract bool Cancel();

			// Token: 0x17000F1B RID: 3867
			// (get) Token: 0x06004203 RID: 16899
			internal abstract bool HasExpired { get; }

			// Token: 0x06004204 RID: 16900 RVA: 0x00111D4F File Offset: 0x0010FF4F
			public void Dispose()
			{
				this.Cancel();
			}

			// Token: 0x04003216 RID: 12822
			private readonly int m_StartTimeMilliseconds;

			// Token: 0x04003217 RID: 12823
			private readonly int m_DurationMilliseconds;
		}

		// Token: 0x0200075E RID: 1886
		// (Invoke) Token: 0x06004206 RID: 16902
		internal delegate void Callback(TimerThread.Timer timer, int timeNoticed, object context);

		// Token: 0x0200075F RID: 1887
		private enum TimerThreadState
		{
			// Token: 0x04003219 RID: 12825
			Idle,
			// Token: 0x0400321A RID: 12826
			Running,
			// Token: 0x0400321B RID: 12827
			Stopped
		}

		// Token: 0x02000760 RID: 1888
		private class TimerQueue : TimerThread.Queue
		{
			// Token: 0x06004209 RID: 16905 RVA: 0x00111D58 File Offset: 0x0010FF58
			internal TimerQueue(int durationMilliseconds)
				: base(durationMilliseconds)
			{
				this.m_Timers = new TimerThread.TimerNode();
				this.m_Timers.Next = this.m_Timers;
				this.m_Timers.Prev = this.m_Timers;
			}

			// Token: 0x0600420A RID: 16906 RVA: 0x00111D90 File Offset: 0x0010FF90
			internal override TimerThread.Timer CreateTimer(TimerThread.Callback callback, object context)
			{
				TimerThread.TimerNode timerNode = new TimerThread.TimerNode(callback, context, base.Duration, this.m_Timers);
				bool flag = false;
				TimerThread.TimerNode timers = this.m_Timers;
				lock (timers)
				{
					if (this.m_Timers.Next == this.m_Timers)
					{
						if (this.m_ThisHandle == IntPtr.Zero)
						{
							this.m_ThisHandle = (IntPtr)GCHandle.Alloc(this);
						}
						flag = true;
					}
					timerNode.Next = this.m_Timers;
					timerNode.Prev = this.m_Timers.Prev;
					this.m_Timers.Prev.Next = timerNode;
					this.m_Timers.Prev = timerNode;
				}
				if (flag)
				{
					TimerThread.Prod();
				}
				return timerNode;
			}

			// Token: 0x0600420B RID: 16907 RVA: 0x00111E5C File Offset: 0x0011005C
			internal bool Fire(out int nextExpiration)
			{
				TimerThread.TimerNode timerNode;
				do
				{
					timerNode = this.m_Timers.Next;
					if (timerNode == this.m_Timers)
					{
						TimerThread.TimerNode timers = this.m_Timers;
						lock (timers)
						{
							timerNode = this.m_Timers.Next;
							if (timerNode == this.m_Timers)
							{
								if (this.m_ThisHandle != IntPtr.Zero)
								{
									((GCHandle)this.m_ThisHandle).Free();
									this.m_ThisHandle = IntPtr.Zero;
								}
								nextExpiration = 0;
								return false;
							}
						}
					}
				}
				while (timerNode.Fire());
				nextExpiration = timerNode.Expiration;
				return true;
			}

			// Token: 0x0400321C RID: 12828
			private IntPtr m_ThisHandle;

			// Token: 0x0400321D RID: 12829
			private readonly TimerThread.TimerNode m_Timers;
		}

		// Token: 0x02000761 RID: 1889
		private class InfiniteTimerQueue : TimerThread.Queue
		{
			// Token: 0x0600420C RID: 16908 RVA: 0x00111F10 File Offset: 0x00110110
			internal InfiniteTimerQueue()
				: base(-1)
			{
			}

			// Token: 0x0600420D RID: 16909 RVA: 0x00111F19 File Offset: 0x00110119
			internal override TimerThread.Timer CreateTimer(TimerThread.Callback callback, object context)
			{
				return new TimerThread.InfiniteTimer();
			}
		}

		// Token: 0x02000762 RID: 1890
		private class TimerNode : TimerThread.Timer
		{
			// Token: 0x0600420E RID: 16910 RVA: 0x00111F20 File Offset: 0x00110120
			internal TimerNode(TimerThread.Callback callback, object context, int durationMilliseconds, object queueLock)
				: base(durationMilliseconds)
			{
				if (callback != null)
				{
					this.m_Callback = callback;
					this.m_Context = context;
				}
				this.m_TimerState = TimerThread.TimerNode.TimerState.Ready;
				this.m_QueueLock = queueLock;
			}

			// Token: 0x0600420F RID: 16911 RVA: 0x00111F49 File Offset: 0x00110149
			internal TimerNode()
				: base(0)
			{
				this.m_TimerState = TimerThread.TimerNode.TimerState.Sentinel;
			}

			// Token: 0x17000F1C RID: 3868
			// (get) Token: 0x06004210 RID: 16912 RVA: 0x00111F59 File Offset: 0x00110159
			internal override bool HasExpired
			{
				get
				{
					return this.m_TimerState == TimerThread.TimerNode.TimerState.Fired;
				}
			}

			// Token: 0x17000F1D RID: 3869
			// (get) Token: 0x06004211 RID: 16913 RVA: 0x00111F64 File Offset: 0x00110164
			// (set) Token: 0x06004212 RID: 16914 RVA: 0x00111F6C File Offset: 0x0011016C
			internal TimerThread.TimerNode Next
			{
				get
				{
					return this.next;
				}
				set
				{
					this.next = value;
				}
			}

			// Token: 0x17000F1E RID: 3870
			// (get) Token: 0x06004213 RID: 16915 RVA: 0x00111F75 File Offset: 0x00110175
			// (set) Token: 0x06004214 RID: 16916 RVA: 0x00111F7D File Offset: 0x0011017D
			internal TimerThread.TimerNode Prev
			{
				get
				{
					return this.prev;
				}
				set
				{
					this.prev = value;
				}
			}

			// Token: 0x06004215 RID: 16917 RVA: 0x00111F88 File Offset: 0x00110188
			internal override bool Cancel()
			{
				if (this.m_TimerState == TimerThread.TimerNode.TimerState.Ready)
				{
					object queueLock = this.m_QueueLock;
					lock (queueLock)
					{
						if (this.m_TimerState == TimerThread.TimerNode.TimerState.Ready)
						{
							this.Next.Prev = this.Prev;
							this.Prev.Next = this.Next;
							this.Next = null;
							this.Prev = null;
							this.m_Callback = null;
							this.m_Context = null;
							this.m_TimerState = TimerThread.TimerNode.TimerState.Cancelled;
							return true;
						}
					}
					return false;
				}
				return false;
			}

			// Token: 0x06004216 RID: 16918 RVA: 0x00112020 File Offset: 0x00110220
			internal bool Fire()
			{
				if (this.m_TimerState != TimerThread.TimerNode.TimerState.Ready)
				{
					return true;
				}
				int tickCount = Environment.TickCount;
				if (TimerThread.IsTickBetween(base.StartTime, base.Expiration, tickCount))
				{
					return false;
				}
				bool flag = false;
				object queueLock = this.m_QueueLock;
				lock (queueLock)
				{
					if (this.m_TimerState == TimerThread.TimerNode.TimerState.Ready)
					{
						this.m_TimerState = TimerThread.TimerNode.TimerState.Fired;
						this.Next.Prev = this.Prev;
						this.Prev.Next = this.Next;
						this.Next = null;
						this.Prev = null;
						flag = this.m_Callback != null;
					}
				}
				if (flag)
				{
					try
					{
						TimerThread.Callback callback = this.m_Callback;
						object context = this.m_Context;
						this.m_Callback = null;
						this.m_Context = null;
						callback(this, tickCount, context);
					}
					catch (Exception ex)
					{
						if (NclUtilities.IsFatal(ex))
						{
							throw;
						}
						if (Logging.On)
						{
							Logging.PrintError(Logging.Web, "TimerThreadTimer#" + base.StartTime.ToString(NumberFormatInfo.InvariantInfo) + "::Fire() - " + SR.GetString("net_log_exception_in_callback", new object[] { ex }));
						}
					}
				}
				return true;
			}

			// Token: 0x0400321E RID: 12830
			private TimerThread.TimerNode.TimerState m_TimerState;

			// Token: 0x0400321F RID: 12831
			private TimerThread.Callback m_Callback;

			// Token: 0x04003220 RID: 12832
			private object m_Context;

			// Token: 0x04003221 RID: 12833
			private object m_QueueLock;

			// Token: 0x04003222 RID: 12834
			private TimerThread.TimerNode next;

			// Token: 0x04003223 RID: 12835
			private TimerThread.TimerNode prev;

			// Token: 0x02000915 RID: 2325
			private enum TimerState
			{
				// Token: 0x04003D5C RID: 15708
				Ready,
				// Token: 0x04003D5D RID: 15709
				Fired,
				// Token: 0x04003D5E RID: 15710
				Cancelled,
				// Token: 0x04003D5F RID: 15711
				Sentinel
			}
		}

		// Token: 0x02000763 RID: 1891
		private class InfiniteTimer : TimerThread.Timer
		{
			// Token: 0x06004217 RID: 16919 RVA: 0x00112164 File Offset: 0x00110364
			internal InfiniteTimer()
				: base(-1)
			{
			}

			// Token: 0x17000F1F RID: 3871
			// (get) Token: 0x06004218 RID: 16920 RVA: 0x0011216D File Offset: 0x0011036D
			internal override bool HasExpired
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06004219 RID: 16921 RVA: 0x00112170 File Offset: 0x00110370
			internal override bool Cancel()
			{
				return Interlocked.Exchange(ref this.cancelled, 1) == 0;
			}

			// Token: 0x04003224 RID: 12836
			private int cancelled;
		}
	}
}
