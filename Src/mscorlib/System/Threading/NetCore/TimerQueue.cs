using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32;

namespace System.Threading.NetCore
{
	// Token: 0x02000588 RID: 1416
	internal class TimerQueue
	{
		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x060042BE RID: 17086 RVA: 0x000F9D64 File Offset: 0x000F7F64
		public static TimerQueue[] Instances { get; } = TimerQueue.CreateTimerQueues();

		// Token: 0x060042BF RID: 17087 RVA: 0x000F9D6B File Offset: 0x000F7F6B
		private TimerQueue(int id)
		{
			this.m_id = id;
		}

		// Token: 0x060042C0 RID: 17088 RVA: 0x000F9D8C File Offset: 0x000F7F8C
		private static TimerQueue[] CreateTimerQueues()
		{
			TimerQueue[] array = new TimerQueue[Environment.ProcessorCount];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new TimerQueue(i);
			}
			return array;
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x060042C1 RID: 17089 RVA: 0x000F9DBC File Offset: 0x000F7FBC
		private static long TickCount64
		{
			[SecuritySafeCritical]
			get
			{
				if (!Environment.IsWindows8OrAbove)
				{
					return Environment.TickCount64;
				}
				ulong num;
				if (!Win32Native.QueryUnbiasedInterruptTime(out num))
				{
					throw Marshal.GetExceptionForHR(Marshal.GetLastWin32Error());
				}
				return (long)(num / 10000UL);
			}
		}

		// Token: 0x060042C2 RID: 17090 RVA: 0x000F9DF4 File Offset: 0x000F7FF4
		[SecuritySafeCritical]
		private bool EnsureAppDomainTimerFiresBy(uint requestedDuration)
		{
			uint num = Math.Min(requestedDuration, 268435455U);
			if (this.m_isAppDomainTimerScheduled)
			{
				long num2 = TimerQueue.TickCount64 - this.m_currentAppDomainTimerStartTicks;
				if (num2 >= (long)((ulong)this.m_currentAppDomainTimerDuration))
				{
					return true;
				}
				uint num3 = this.m_currentAppDomainTimerDuration - (uint)num2;
				if (num >= num3)
				{
					return true;
				}
			}
			if (this.m_pauseTicks != 0L)
			{
				return true;
			}
			if (this.m_appDomainTimer == null || this.m_appDomainTimer.IsInvalid)
			{
				this.m_appDomainTimer = TimerQueue.CreateAppDomainTimer(num, this.m_id);
				if (!this.m_appDomainTimer.IsInvalid)
				{
					this.m_isAppDomainTimerScheduled = true;
					this.m_currentAppDomainTimerStartTicks = TimerQueue.TickCount64;
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
					this.m_currentAppDomainTimerStartTicks = TimerQueue.TickCount64;
					this.m_currentAppDomainTimerDuration = num;
					return true;
				}
				return false;
			}
		}

		// Token: 0x060042C3 RID: 17091 RVA: 0x000F9EC3 File Offset: 0x000F80C3
		[SecuritySafeCritical]
		internal static void AppDomainTimerCallback(int id)
		{
			TimerQueue.Instances[id].FireNextTimers();
		}

		// Token: 0x060042C4 RID: 17092 RVA: 0x000F9ED4 File Offset: 0x000F80D4
		[SecurityCritical]
		internal static void PauseAll()
		{
			foreach (TimerQueue timerQueue in TimerQueue.Instances)
			{
				timerQueue.Pause();
			}
		}

		// Token: 0x060042C5 RID: 17093 RVA: 0x000F9F00 File Offset: 0x000F8100
		[SecurityCritical]
		internal static void ResumeAll()
		{
			foreach (TimerQueue timerQueue in TimerQueue.Instances)
			{
				timerQueue.Resume();
			}
		}

		// Token: 0x060042C6 RID: 17094 RVA: 0x000F9F2C File Offset: 0x000F812C
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
					this.m_pauseTicks = TimerQueue.TickCount64;
				}
			}
		}

		// Token: 0x060042C7 RID: 17095 RVA: 0x000F9F9C File Offset: 0x000F819C
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
					long pauseTicks = this.m_pauseTicks;
					this.m_pauseTicks = 0L;
					long tickCount = TimerQueue.TickCount64;
					long num = tickCount - pauseTicks;
					bool flag2 = false;
					uint num2 = uint.MaxValue;
					TimerQueueTimer timerQueueTimer = this.m_shortTimers;
					for (int i = 0; i < 2; i++)
					{
						while (timerQueueTimer != null)
						{
							TimerQueueTimer next = timerQueueTimer.m_next;
							long num3;
							if (num <= tickCount - timerQueueTimer.m_startTicks)
							{
								num3 = pauseTicks - timerQueueTimer.m_startTicks;
							}
							else
							{
								num3 = tickCount - timerQueueTimer.m_startTicks;
							}
							timerQueueTimer.m_dueTime = (((ulong)timerQueueTimer.m_dueTime > (ulong)num3) ? (timerQueueTimer.m_dueTime - (uint)num3) : 0U);
							timerQueueTimer.m_startTicks = tickCount;
							if (timerQueueTimer.m_dueTime < num2)
							{
								flag2 = true;
								num2 = timerQueueTimer.m_dueTime;
							}
							if (!timerQueueTimer.m_short && timerQueueTimer.m_dueTime <= 333U)
							{
								this.MoveTimerToCorrectList(timerQueueTimer, true);
							}
							timerQueueTimer = next;
						}
						if (i == 0)
						{
							long num4 = this.m_currentAbsoluteThreshold - tickCount;
							if (num4 > 0L)
							{
								if (this.m_shortTimers == null && this.m_longTimers != null)
								{
									num2 = (uint)num4 + 1U;
									flag2 = true;
									break;
								}
								break;
							}
							else
							{
								timerQueueTimer = this.m_longTimers;
								this.m_currentAbsoluteThreshold = tickCount + 333L;
							}
						}
					}
					if (flag2)
					{
						this.EnsureAppDomainTimerFiresBy(num2);
					}
				}
			}
		}

		// Token: 0x060042C8 RID: 17096 RVA: 0x000FA120 File Offset: 0x000F8320
		[SecuritySafeCritical]
		private void FireNextTimers()
		{
			TimerQueueTimer timerQueueTimer = null;
			List<TimerQueueTimer> list = TimerQueue.t_timersToQueueToFire;
			if (list == null)
			{
				list = (TimerQueue.t_timersToQueueToFire = new List<TimerQueueTimer>());
			}
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
					long tickCount = TimerQueue.TickCount64;
					TimerQueueTimer timerQueueTimer2 = this.m_shortTimers;
					for (int i = 0; i < 2; i++)
					{
						while (timerQueueTimer2 != null)
						{
							TimerQueueTimer next = timerQueueTimer2.m_next;
							long num2 = tickCount - timerQueueTimer2.m_startTicks;
							long num3 = (long)((ulong)timerQueueTimer2.m_dueTime - (ulong)num2);
							if (num3 <= 0L)
							{
								if (timerQueueTimer2.m_period != 4294967295U)
								{
									timerQueueTimer2.m_startTicks = tickCount;
									long num4 = num2 - (long)((ulong)timerQueueTimer2.m_dueTime);
									timerQueueTimer2.m_dueTime = ((num4 < (long)((ulong)timerQueueTimer2.m_period)) ? (timerQueueTimer2.m_period - (uint)num4) : 1U);
									if (timerQueueTimer2.m_dueTime < num)
									{
										flag2 = true;
										num = timerQueueTimer2.m_dueTime;
									}
									bool flag3 = tickCount + (long)((ulong)timerQueueTimer2.m_dueTime) - this.m_currentAbsoluteThreshold <= 0L;
									if (timerQueueTimer2.m_short != flag3)
									{
										this.MoveTimerToCorrectList(timerQueueTimer2, flag3);
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
									list.Add(timerQueueTimer2);
								}
							}
							else
							{
								if (num3 < (long)((ulong)num))
								{
									flag2 = true;
									num = (uint)num3;
								}
								if (!timerQueueTimer2.m_short && num3 <= 333L)
								{
									this.MoveTimerToCorrectList(timerQueueTimer2, true);
								}
							}
							timerQueueTimer2 = next;
						}
						if (i == 0)
						{
							long num5 = this.m_currentAbsoluteThreshold - tickCount;
							if (num5 > 0L)
							{
								if (this.m_shortTimers == null && this.m_longTimers != null)
								{
									num = (uint)num5 + 1U;
									flag2 = true;
									break;
								}
								break;
							}
							else
							{
								timerQueueTimer2 = this.m_longTimers;
								this.m_currentAbsoluteThreshold = tickCount + 333L;
							}
						}
					}
					if (flag2)
					{
						this.EnsureAppDomainTimerFiresBy(num);
					}
				}
			}
			if (list.Count != 0)
			{
				foreach (TimerQueueTimer timerQueueTimer3 in list)
				{
					ThreadPool.UnsafeQueueCustomWorkItem(timerQueueTimer3, true);
				}
				list.Clear();
			}
			if (timerQueueTimer != null)
			{
				timerQueueTimer.Fire();
			}
		}

		// Token: 0x060042C9 RID: 17097 RVA: 0x000FA384 File Offset: 0x000F8584
		public bool UpdateTimer(TimerQueueTimer timer, uint dueTime, uint period)
		{
			long tickCount = TimerQueue.TickCount64;
			long num = tickCount + (long)((ulong)dueTime);
			bool flag = this.m_currentAbsoluteThreshold - num >= 0L;
			if (timer.m_dueTime == 4294967295U)
			{
				timer.m_short = flag;
				this.LinkTimer(timer);
			}
			else if (timer.m_short != flag)
			{
				this.UnlinkTimer(timer);
				timer.m_short = flag;
				this.LinkTimer(timer);
			}
			timer.m_dueTime = dueTime;
			timer.m_period = ((period == 0U) ? uint.MaxValue : period);
			timer.m_startTicks = tickCount;
			return this.EnsureAppDomainTimerFiresBy(dueTime);
		}

		// Token: 0x060042CA RID: 17098 RVA: 0x000FA405 File Offset: 0x000F8605
		public void MoveTimerToCorrectList(TimerQueueTimer timer, bool shortList)
		{
			this.UnlinkTimer(timer);
			timer.m_short = shortList;
			this.LinkTimer(timer);
		}

		// Token: 0x060042CB RID: 17099 RVA: 0x000FA41C File Offset: 0x000F861C
		private void LinkTimer(TimerQueueTimer timer)
		{
			timer.m_next = (timer.m_short ? this.m_shortTimers : this.m_longTimers);
			if (timer.m_next != null)
			{
				timer.m_next.m_prev = timer;
			}
			timer.m_prev = null;
			if (timer.m_short)
			{
				this.m_shortTimers = timer;
				return;
			}
			this.m_longTimers = timer;
		}

		// Token: 0x060042CC RID: 17100 RVA: 0x000FA478 File Offset: 0x000F8678
		private void UnlinkTimer(TimerQueueTimer timer)
		{
			TimerQueueTimer timerQueueTimer = timer.m_next;
			if (timerQueueTimer != null)
			{
				timerQueueTimer.m_prev = timer.m_prev;
			}
			if (this.m_shortTimers == timer)
			{
				this.m_shortTimers = timerQueueTimer;
			}
			else if (this.m_longTimers == timer)
			{
				this.m_longTimers = timerQueueTimer;
			}
			timerQueueTimer = timer.m_prev;
			if (timerQueueTimer != null)
			{
				timerQueueTimer.m_next = timer.m_next;
			}
		}

		// Token: 0x060042CD RID: 17101 RVA: 0x000FA4D3 File Offset: 0x000F86D3
		public void DeleteTimer(TimerQueueTimer timer)
		{
			if (timer.m_dueTime != 4294967295U)
			{
				this.UnlinkTimer(timer);
				timer.m_prev = null;
				timer.m_next = null;
				timer.m_dueTime = uint.MaxValue;
				timer.m_period = uint.MaxValue;
				timer.m_startTicks = 0L;
				timer.m_short = false;
			}
		}

		// Token: 0x04001BC5 RID: 7109
		private readonly int m_id;

		// Token: 0x04001BC6 RID: 7110
		[SecurityCritical]
		private TimerQueue.AppDomainTimerSafeHandle m_appDomainTimer;

		// Token: 0x04001BC7 RID: 7111
		private bool m_isAppDomainTimerScheduled;

		// Token: 0x04001BC8 RID: 7112
		private long m_currentAppDomainTimerStartTicks;

		// Token: 0x04001BC9 RID: 7113
		private uint m_currentAppDomainTimerDuration;

		// Token: 0x04001BCA RID: 7114
		private TimerQueueTimer m_shortTimers;

		// Token: 0x04001BCB RID: 7115
		private TimerQueueTimer m_longTimers;

		// Token: 0x04001BCC RID: 7116
		private long m_currentAbsoluteThreshold = TimerQueue.TickCount64 + 333L;

		// Token: 0x04001BCD RID: 7117
		private const int ShortTimersThresholdMilliseconds = 333;

		// Token: 0x04001BCE RID: 7118
		private long m_pauseTicks;

		// Token: 0x04001BCF RID: 7119
		[ThreadStatic]
		private static List<TimerQueueTimer> t_timersToQueueToFire;
	}
}
