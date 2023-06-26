using System;
using System.Diagnostics.Tracing;
using System.Security;
using Microsoft.Win32;

namespace System.Threading
{
	// Token: 0x0200052D RID: 1325
	internal sealed class TimerQueueTimer
	{
		// Token: 0x06003E5C RID: 15964 RVA: 0x000E991C File Offset: 0x000E7B1C
		[SecurityCritical]
		internal TimerQueueTimer(TimerCallback timerCallback, object state, uint dueTime, uint period, ref StackCrawlMark stackMark)
		{
			this.m_timerCallback = timerCallback;
			this.m_state = state;
			this.m_dueTime = uint.MaxValue;
			this.m_period = uint.MaxValue;
			if (!ExecutionContext.IsFlowSuppressed())
			{
				this.m_executionContext = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
			}
			if (dueTime != 4294967295U)
			{
				this.Change(dueTime, period);
			}
		}

		// Token: 0x06003E5D RID: 15965 RVA: 0x000E9970 File Offset: 0x000E7B70
		internal bool Change(uint dueTime, uint period)
		{
			TimerQueue instance = TimerQueue.Instance;
			bool flag2;
			lock (instance)
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
						TimerQueue.Instance.DeleteTimer(this);
						flag2 = true;
					}
					else
					{
						if (FrameworkEventSource.IsInitialized && FrameworkEventSource.Log.IsEnabled(EventLevel.Informational, (EventKeywords)16L))
						{
							FrameworkEventSource.Log.ThreadTransferSendObj(this, 1, string.Empty, true);
						}
						flag2 = TimerQueue.Instance.UpdateTimer(this, dueTime, period);
					}
				}
			}
			return flag2;
		}

		// Token: 0x06003E5E RID: 15966 RVA: 0x000E9A24 File Offset: 0x000E7C24
		public void Close()
		{
			TimerQueue instance = TimerQueue.Instance;
			lock (instance)
			{
				try
				{
				}
				finally
				{
					if (!this.m_canceled)
					{
						this.m_canceled = true;
						TimerQueue.Instance.DeleteTimer(this);
					}
				}
			}
		}

		// Token: 0x06003E5F RID: 15967 RVA: 0x000E9A88 File Offset: 0x000E7C88
		public bool Close(WaitHandle toSignal)
		{
			bool flag = false;
			TimerQueue instance = TimerQueue.Instance;
			bool flag3;
			lock (instance)
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
						TimerQueue.Instance.DeleteTimer(this);
						if (this.m_callbacksRunning == 0)
						{
							flag = true;
						}
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

		// Token: 0x06003E60 RID: 15968 RVA: 0x000E9B14 File Offset: 0x000E7D14
		internal void Fire()
		{
			bool flag = false;
			TimerQueue instance = TimerQueue.Instance;
			lock (instance)
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
			TimerQueue instance2 = TimerQueue.Instance;
			lock (instance2)
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

		// Token: 0x06003E61 RID: 15969 RVA: 0x000E9BF4 File Offset: 0x000E7DF4
		[SecuritySafeCritical]
		internal void SignalNoCallbacksRunning()
		{
			Win32Native.SetEvent(this.m_notifyWhenNoCallbacksRunning.SafeWaitHandle);
		}

		// Token: 0x06003E62 RID: 15970 RVA: 0x000E9C0C File Offset: 0x000E7E0C
		[SecuritySafeCritical]
		internal void CallCallback()
		{
			if (FrameworkEventSource.IsInitialized && FrameworkEventSource.Log.IsEnabled(EventLevel.Informational, (EventKeywords)16L))
			{
				FrameworkEventSource.Log.ThreadTransferReceiveObj(this, 1, string.Empty);
			}
			if (this.m_executionContext == null)
			{
				this.m_timerCallback(this.m_state);
				return;
			}
			using (ExecutionContext executionContext = (this.m_executionContext.IsPreAllocatedDefault ? this.m_executionContext : this.m_executionContext.CreateCopy()))
			{
				ContextCallback contextCallback = TimerQueueTimer.s_callCallbackInContext;
				if (contextCallback == null)
				{
					contextCallback = (TimerQueueTimer.s_callCallbackInContext = new ContextCallback(TimerQueueTimer.CallCallbackInContext));
				}
				ExecutionContext.Run(executionContext, contextCallback, this, true);
			}
		}

		// Token: 0x06003E63 RID: 15971 RVA: 0x000E9CC0 File Offset: 0x000E7EC0
		[SecurityCritical]
		private static void CallCallbackInContext(object state)
		{
			TimerQueueTimer timerQueueTimer = (TimerQueueTimer)state;
			timerQueueTimer.m_timerCallback(timerQueueTimer.m_state);
		}

		// Token: 0x04001A41 RID: 6721
		internal TimerQueueTimer m_next;

		// Token: 0x04001A42 RID: 6722
		internal TimerQueueTimer m_prev;

		// Token: 0x04001A43 RID: 6723
		internal int m_startTicks;

		// Token: 0x04001A44 RID: 6724
		internal uint m_dueTime;

		// Token: 0x04001A45 RID: 6725
		internal uint m_period;

		// Token: 0x04001A46 RID: 6726
		private readonly TimerCallback m_timerCallback;

		// Token: 0x04001A47 RID: 6727
		private readonly object m_state;

		// Token: 0x04001A48 RID: 6728
		private readonly ExecutionContext m_executionContext;

		// Token: 0x04001A49 RID: 6729
		private int m_callbacksRunning;

		// Token: 0x04001A4A RID: 6730
		private volatile bool m_canceled;

		// Token: 0x04001A4B RID: 6731
		private volatile WaitHandle m_notifyWhenNoCallbacksRunning;

		// Token: 0x04001A4C RID: 6732
		[SecurityCritical]
		private static ContextCallback s_callCallbackInContext;
	}
}
