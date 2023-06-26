using System;
using System.Diagnostics.Tracing;
using System.Security;
using Microsoft.Win32;

namespace System.Threading.NetCore
{
	// Token: 0x02000589 RID: 1417
	internal sealed class TimerQueueTimer : IThreadPoolWorkItem
	{
		// Token: 0x060042CF RID: 17103 RVA: 0x000FA51C File Offset: 0x000F871C
		[SecuritySafeCritical]
		internal TimerQueueTimer(TimerCallback timerCallback, object state, uint dueTime, uint period, bool flowExecutionContext, ref StackCrawlMark stackMark)
		{
			this.m_timerCallback = timerCallback;
			this.m_state = state;
			this.m_dueTime = uint.MaxValue;
			this.m_period = uint.MaxValue;
			if (flowExecutionContext)
			{
				this.m_executionContext = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
			}
			this.m_associatedTimerQueue = TimerQueue.Instances[Thread.GetCurrentProcessorId() % TimerQueue.Instances.Length];
			if (dueTime != 4294967295U)
			{
				this.Change(dueTime, period);
			}
		}

		// Token: 0x060042D0 RID: 17104 RVA: 0x000FA584 File Offset: 0x000F8784
		internal bool Change(uint dueTime, uint period)
		{
			TimerQueue associatedTimerQueue = this.m_associatedTimerQueue;
			bool flag2;
			lock (associatedTimerQueue)
			{
				if (this.m_canceled)
				{
					throw new ObjectDisposedException(null, Environment.GetResourceString("ObjectDisposed_Generic"));
				}
				try
				{
				}
				finally
				{
					this.m_period = period;
					if (dueTime == 4294967295U)
					{
						this.m_associatedTimerQueue.DeleteTimer(this);
						flag2 = true;
					}
					else
					{
						if (FrameworkEventSource.IsInitialized && FrameworkEventSource.Log.IsEnabled(EventLevel.Informational, (EventKeywords)16L))
						{
							FrameworkEventSource.Log.ThreadTransferSendObj(this, 1, string.Empty, true);
						}
						flag2 = this.m_associatedTimerQueue.UpdateTimer(this, dueTime, period);
					}
				}
			}
			return flag2;
		}

		// Token: 0x060042D1 RID: 17105 RVA: 0x000FA63C File Offset: 0x000F883C
		public void Close()
		{
			TimerQueue associatedTimerQueue = this.m_associatedTimerQueue;
			lock (associatedTimerQueue)
			{
				try
				{
				}
				finally
				{
					if (!this.m_canceled)
					{
						this.m_canceled = true;
						this.m_associatedTimerQueue.DeleteTimer(this);
					}
				}
			}
		}

		// Token: 0x060042D2 RID: 17106 RVA: 0x000FA6A4 File Offset: 0x000F88A4
		public bool Close(WaitHandle toSignal)
		{
			bool flag = false;
			TimerQueue associatedTimerQueue = this.m_associatedTimerQueue;
			bool flag3;
			lock (associatedTimerQueue)
			{
				try
				{
				}
				finally
				{
					if (this.m_canceled)
					{
						flag3 = false;
					}
					else
					{
						this.m_canceled = true;
						this.m_notifyWhenNoCallbacksRunning = toSignal;
						this.m_associatedTimerQueue.DeleteTimer(this);
						flag = this.m_callbacksRunning == 0;
						flag3 = true;
					}
				}
			}
			if (flag)
			{
				this.SignalNoCallbacksRunning();
			}
			return flag3;
		}

		// Token: 0x060042D3 RID: 17107 RVA: 0x000FA730 File Offset: 0x000F8930
		[SecurityCritical]
		void IThreadPoolWorkItem.ExecuteWorkItem()
		{
			this.Fire();
		}

		// Token: 0x060042D4 RID: 17108 RVA: 0x000FA738 File Offset: 0x000F8938
		[SecurityCritical]
		void IThreadPoolWorkItem.MarkAborted(ThreadAbortException tae)
		{
		}

		// Token: 0x060042D5 RID: 17109 RVA: 0x000FA73C File Offset: 0x000F893C
		internal void Fire()
		{
			bool flag = false;
			TimerQueue associatedTimerQueue = this.m_associatedTimerQueue;
			lock (associatedTimerQueue)
			{
				try
				{
				}
				finally
				{
					flag = this.m_canceled;
					if (!flag)
					{
						this.m_callbacksRunning++;
					}
				}
			}
			if (flag)
			{
				return;
			}
			this.CallCallback();
			bool flag3 = false;
			TimerQueue associatedTimerQueue2 = this.m_associatedTimerQueue;
			lock (associatedTimerQueue2)
			{
				try
				{
				}
				finally
				{
					this.m_callbacksRunning--;
					if (this.m_canceled && this.m_callbacksRunning == 0 && this.m_notifyWhenNoCallbacksRunning != null)
					{
						flag3 = true;
					}
				}
			}
			if (flag3)
			{
				this.SignalNoCallbacksRunning();
			}
		}

		// Token: 0x060042D6 RID: 17110 RVA: 0x000FA81C File Offset: 0x000F8A1C
		[SecuritySafeCritical]
		internal void SignalNoCallbacksRunning()
		{
			Win32Native.SetEvent(this.m_notifyWhenNoCallbacksRunning.SafeWaitHandle);
		}

		// Token: 0x060042D7 RID: 17111 RVA: 0x000FA834 File Offset: 0x000F8A34
		[SecuritySafeCritical]
		internal void CallCallback()
		{
			if (FrameworkEventSource.IsInitialized && FrameworkEventSource.Log.IsEnabled(EventLevel.Informational, (EventKeywords)16L))
			{
				FrameworkEventSource.Log.ThreadTransferReceiveObj(this, 1, string.Empty);
			}
			ExecutionContext executionContext = this.m_executionContext;
			if (executionContext == null)
			{
				this.m_timerCallback(this.m_state);
				return;
			}
			ExecutionContext executionContext2;
			executionContext = (executionContext2 = (executionContext.IsPreAllocatedDefault ? executionContext : executionContext.CreateCopy()));
			try
			{
				if (TimerQueueTimer.s_callCallbackInContext == null)
				{
					TimerQueueTimer.s_callCallbackInContext = new ContextCallback(TimerQueueTimer.CallCallbackInContext);
				}
				ExecutionContext.Run(executionContext, TimerQueueTimer.s_callCallbackInContext, this, true);
			}
			finally
			{
				if (executionContext2 != null)
				{
					((IDisposable)executionContext2).Dispose();
				}
			}
		}

		// Token: 0x060042D8 RID: 17112 RVA: 0x000FA8E0 File Offset: 0x000F8AE0
		[SecurityCritical]
		private static void CallCallbackInContext(object state)
		{
			TimerQueueTimer timerQueueTimer = (TimerQueueTimer)state;
			timerQueueTimer.m_timerCallback(timerQueueTimer.m_state);
		}

		// Token: 0x04001BD0 RID: 7120
		private readonly TimerQueue m_associatedTimerQueue;

		// Token: 0x04001BD1 RID: 7121
		internal TimerQueueTimer m_next;

		// Token: 0x04001BD2 RID: 7122
		internal TimerQueueTimer m_prev;

		// Token: 0x04001BD3 RID: 7123
		internal bool m_short;

		// Token: 0x04001BD4 RID: 7124
		internal long m_startTicks;

		// Token: 0x04001BD5 RID: 7125
		internal uint m_dueTime;

		// Token: 0x04001BD6 RID: 7126
		internal uint m_period;

		// Token: 0x04001BD7 RID: 7127
		private readonly TimerCallback m_timerCallback;

		// Token: 0x04001BD8 RID: 7128
		private readonly object m_state;

		// Token: 0x04001BD9 RID: 7129
		private readonly ExecutionContext m_executionContext;

		// Token: 0x04001BDA RID: 7130
		private int m_callbacksRunning;

		// Token: 0x04001BDB RID: 7131
		private volatile bool m_canceled;

		// Token: 0x04001BDC RID: 7132
		private volatile WaitHandle m_notifyWhenNoCallbacksRunning;

		// Token: 0x04001BDD RID: 7133
		[SecurityCritical]
		private static ContextCallback s_callCallbackInContext;
	}
}
