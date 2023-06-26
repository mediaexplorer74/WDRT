using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.NetCore;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Threading
{
	// Token: 0x0200052C RID: 1324
	internal class TimerQueue
	{
		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06003E4C RID: 15948 RVA: 0x000E943A File Offset: 0x000E763A
		public static TimerQueue Instance
		{
			get
			{
				return TimerQueue.s_queue;
			}
		}

		// Token: 0x06003E4D RID: 15949 RVA: 0x000E9441 File Offset: 0x000E7641
		private TimerQueue()
		{
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06003E4E RID: 15950 RVA: 0x000E944C File Offset: 0x000E764C
		private static int TickCount
		{
			[SecuritySafeCritical]
			get
			{
				if (!Environment.IsWindows8OrAbove)
				{
					return Environment.TickCount;
				}
				ulong num;
				if (!Win32Native.QueryUnbiasedInterruptTime(out num))
				{
					throw Marshal.GetExceptionForHR(Marshal.GetLastWin32Error());
				}
				return (int)((uint)(num / 10000UL));
			}
		}

		// Token: 0x06003E4F RID: 15951 RVA: 0x000E9488 File Offset: 0x000E7688
		[SecuritySafeCritical]
		private bool EnsureAppDomainTimerFiresBy(uint requestedDuration)
		{
			uint num = Math.Min(requestedDuration, 268435455U);
			if (this.m_isAppDomainTimerScheduled)
			{
				uint num2 = (uint)(TimerQueue.TickCount - this.m_currentAppDomainTimerStartTicks);
				if (num2 >= this.m_currentAppDomainTimerDuration)
				{
					return true;
				}
				uint num3 = this.m_currentAppDomainTimerDuration - num2;
				if (num >= num3)
				{
					return true;
				}
			}
			if (this.m_pauseTicks != 0)
			{
				return true;
			}
			if (this.m_appDomainTimer == null || this.m_appDomainTimer.IsInvalid)
			{
				this.m_appDomainTimer = TimerQueue.CreateAppDomainTimer(num, 0);
				if (!this.m_appDomainTimer.IsInvalid)
				{
					this.m_isAppDomainTimerScheduled = true;
					this.m_currentAppDomainTimerStartTicks = TimerQueue.TickCount;
					this.m_currentAppDomainTimerDuration = num;
					return true;
				}
				return false;
			}
			else
			{
				if (TimerQueue.ChangeAppDomainTimer(this.m_appDomainTimer, num))
				{
					this.m_isAppDomainTimerScheduled = true;
					this.m_currentAppDomainTimerStartTicks = TimerQueue.TickCount;
					this.m_currentAppDomainTimerDuration = num;
					return true;
				}
				return false;
			}
		}

		// Token: 0x06003E50 RID: 15952 RVA: 0x000E9552 File Offset: 0x000E7752
		[SecuritySafeCritical]
		internal static void AppDomainTimerCallback(int id)
		{
			if (Timer.UseNetCoreTimer)
			{
				TimerQueue.AppDomainTimerCallback(id);
				return;
			}
			TimerQueue.Instance.FireNextTimers();
		}

		// Token: 0x06003E51 RID: 15953
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern TimerQueue.AppDomainTimerSafeHandle CreateAppDomainTimer(uint dueTime, int id);

		// Token: 0x06003E52 RID: 15954
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern bool ChangeAppDomainTimer(TimerQueue.AppDomainTimerSafeHandle handle, uint dueTime);

		// Token: 0x06003E53 RID: 15955
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern bool DeleteAppDomainTimer(IntPtr handle);

		// Token: 0x06003E54 RID: 15956 RVA: 0x000E956C File Offset: 0x000E776C
		[SecurityCritical]
		internal void Pause()
		{
			lock (this)
			{
				if (this.m_appDomainTimer != null && !this.m_appDomainTimer.IsInvalid)
				{
					this.m_appDomainTimer.Dispose();
					this.m_appDomainTimer = null;
					this.m_isAppDomainTimerScheduled = false;
					this.m_pauseTicks = TimerQueue.TickCount;
				}
			}
		}

		// Token: 0x06003E55 RID: 15957 RVA: 0x000E95DC File Offset: 0x000E77DC
		[SecurityCritical]
		internal void Resume()
		{
			lock (this)
			{
				try
				{
				}
				finally
				{
					int pauseTicks = this.m_pauseTicks;
					this.m_pauseTicks = 0;
					int tickCount = TimerQueue.TickCount;
					int num = tickCount - pauseTicks;
					bool flag2 = false;
					uint num2 = uint.MaxValue;
					for (TimerQueueTimer timerQueueTimer = this.m_timers; timerQueueTimer != null; timerQueueTimer = timerQueueTimer.m_next)
					{
						uint num3;
						if (timerQueueTimer.m_startTicks <= pauseTicks)
						{
							num3 = (uint)(pauseTicks - timerQueueTimer.m_startTicks);
						}
						else
						{
							num3 = (uint)(tickCount - timerQueueTimer.m_startTicks);
						}
						timerQueueTimer.m_dueTime = ((timerQueueTimer.m_dueTime > num3) ? (timerQueueTimer.m_dueTime - num3) : 0U);
						timerQueueTimer.m_startTicks = tickCount;
						if (timerQueueTimer.m_dueTime < num2)
						{
							flag2 = true;
							num2 = timerQueueTimer.m_dueTime;
						}
					}
					if (flag2)
					{
						this.EnsureAppDomainTimerFiresBy(num2);
					}
				}
			}
		}

		// Token: 0x06003E56 RID: 15958 RVA: 0x000E96C8 File Offset: 0x000E78C8
		private void FireNextTimers()
		{
			TimerQueueTimer timerQueueTimer = null;
			lock (this)
			{
				try
				{
				}
				finally
				{
					this.m_isAppDomainTimerScheduled = false;
					bool flag2 = false;
					uint num = uint.MaxValue;
					int tickCount = TimerQueue.TickCount;
					TimerQueueTimer timerQueueTimer2 = this.m_timers;
					while (timerQueueTimer2 != null)
					{
						uint num2 = (uint)(tickCount - timerQueueTimer2.m_startTicks);
						if (num2 >= timerQueueTimer2.m_dueTime)
						{
							TimerQueueTimer next = timerQueueTimer2.m_next;
							if (timerQueueTimer2.m_period != 4294967295U)
							{
								timerQueueTimer2.m_startTicks = tickCount;
								timerQueueTimer2.m_dueTime = timerQueueTimer2.m_period;
								if (timerQueueTimer2.m_dueTime < num)
								{
									flag2 = true;
									num = timerQueueTimer2.m_dueTime;
								}
							}
							else
							{
								this.DeleteTimer(timerQueueTimer2);
							}
							if (timerQueueTimer == null)
							{
								timerQueueTimer = timerQueueTimer2;
							}
							else
							{
								TimerQueue.QueueTimerCompletion(timerQueueTimer2);
							}
							timerQueueTimer2 = next;
						}
						else
						{
							uint num3 = timerQueueTimer2.m_dueTime - num2;
							if (num3 < num)
							{
								flag2 = true;
								num = num3;
							}
							timerQueueTimer2 = timerQueueTimer2.m_next;
						}
					}
					if (flag2)
					{
						this.EnsureAppDomainTimerFiresBy(num);
					}
				}
			}
			if (timerQueueTimer != null)
			{
				timerQueueTimer.Fire();
			}
		}

		// Token: 0x06003E57 RID: 15959 RVA: 0x000E97E4 File Offset: 0x000E79E4
		[SecuritySafeCritical]
		private static void QueueTimerCompletion(TimerQueueTimer timer)
		{
			WaitCallback waitCallback = TimerQueue.s_fireQueuedTimerCompletion;
			if (waitCallback == null)
			{
				waitCallback = (TimerQueue.s_fireQueuedTimerCompletion = new WaitCallback(TimerQueue.FireQueuedTimerCompletion));
			}
			ThreadPool.UnsafeQueueUserWorkItem(waitCallback, timer);
		}

		// Token: 0x06003E58 RID: 15960 RVA: 0x000E9815 File Offset: 0x000E7A15
		private static void FireQueuedTimerCompletion(object state)
		{
			((TimerQueueTimer)state).Fire();
		}

		// Token: 0x06003E59 RID: 15961 RVA: 0x000E9824 File Offset: 0x000E7A24
		public bool UpdateTimer(TimerQueueTimer timer, uint dueTime, uint period)
		{
			if (timer.m_dueTime == 4294967295U)
			{
				timer.m_next = this.m_timers;
				timer.m_prev = null;
				if (timer.m_next != null)
				{
					timer.m_next.m_prev = timer;
				}
				this.m_timers = timer;
			}
			timer.m_dueTime = dueTime;
			timer.m_period = ((period == 0U) ? uint.MaxValue : period);
			timer.m_startTicks = TimerQueue.TickCount;
			return this.EnsureAppDomainTimerFiresBy(dueTime);
		}

		// Token: 0x06003E5A RID: 15962 RVA: 0x000E9890 File Offset: 0x000E7A90
		public void DeleteTimer(TimerQueueTimer timer)
		{
			if (timer.m_dueTime != 4294967295U)
			{
				if (timer.m_next != null)
				{
					timer.m_next.m_prev = timer.m_prev;
				}
				if (timer.m_prev != null)
				{
					timer.m_prev.m_next = timer.m_next;
				}
				if (this.m_timers == timer)
				{
					this.m_timers = timer.m_next;
				}
				timer.m_dueTime = uint.MaxValue;
				timer.m_period = uint.MaxValue;
				timer.m_startTicks = 0;
				timer.m_prev = null;
				timer.m_next = null;
			}
		}

		// Token: 0x04001A39 RID: 6713
		private static TimerQueue s_queue = new TimerQueue();

		// Token: 0x04001A3A RID: 6714
		[SecurityCritical]
		private TimerQueue.AppDomainTimerSafeHandle m_appDomainTimer;

		// Token: 0x04001A3B RID: 6715
		private bool m_isAppDomainTimerScheduled;

		// Token: 0x04001A3C RID: 6716
		private int m_currentAppDomainTimerStartTicks;

		// Token: 0x04001A3D RID: 6717
		private uint m_currentAppDomainTimerDuration;

		// Token: 0x04001A3E RID: 6718
		private TimerQueueTimer m_timers;

		// Token: 0x04001A3F RID: 6719
		private volatile int m_pauseTicks;

		// Token: 0x04001A40 RID: 6720
		private static WaitCallback s_fireQueuedTimerCompletion;

		// Token: 0x02000BF3 RID: 3059
		[SecurityCritical]
		internal class AppDomainTimerSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
		{
			// Token: 0x06006F99 RID: 28569 RVA: 0x00181E83 File Offset: 0x00180083
			public AppDomainTimerSafeHandle()
				: base(true)
			{
			}

			// Token: 0x06006F9A RID: 28570 RVA: 0x00181E8C File Offset: 0x0018008C
			[SecurityCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			protected override bool ReleaseHandle()
			{
				return TimerQueue.DeleteAppDomainTimer(this.handle);
			}
		}
	}
}
