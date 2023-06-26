using System;
using System.Runtime.CompilerServices;
using System.Threading.NetCore;

namespace System.Threading
{
	// Token: 0x0200052E RID: 1326
	internal sealed class TimerHolder
	{
		// Token: 0x06003E64 RID: 15972 RVA: 0x000E9CE5 File Offset: 0x000E7EE5
		public TimerHolder(object timer)
		{
			this.m_timer = timer;
		}

		// Token: 0x06003E65 RID: 15973 RVA: 0x000E9CF4 File Offset: 0x000E7EF4
		~TimerHolder()
		{
			if (!Environment.HasShutdownStarted && !AppDomain.CurrentDomain.IsFinalizingForUnload())
			{
				if (Timer.UseNetCoreTimer)
				{
					this.NetCoreTimer.Close();
				}
				else
				{
					this.NetFxTimer.Close();
				}
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06003E66 RID: 15974 RVA: 0x000E9D50 File Offset: 0x000E7F50
		private TimerQueueTimer NetFxTimer
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (TimerQueueTimer)this.m_timer;
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06003E67 RID: 15975 RVA: 0x000E9D5D File Offset: 0x000E7F5D
		private TimerQueueTimer NetCoreTimer
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (TimerQueueTimer)this.m_timer;
			}
		}

		// Token: 0x06003E68 RID: 15976 RVA: 0x000E9D6A File Offset: 0x000E7F6A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Change(uint dueTime, uint period)
		{
			if (!Timer.UseNetCoreTimer)
			{
				return this.NetFxTimer.Change(dueTime, period);
			}
			return this.NetCoreTimer.Change(dueTime, period);
		}

		// Token: 0x06003E69 RID: 15977 RVA: 0x000E9D8E File Offset: 0x000E7F8E
		public void Close()
		{
			if (Timer.UseNetCoreTimer)
			{
				this.NetCoreTimer.Close();
			}
			else
			{
				this.NetFxTimer.Close();
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003E6A RID: 15978 RVA: 0x000E9DB8 File Offset: 0x000E7FB8
		public bool Close(WaitHandle notifyObject)
		{
			bool flag = (Timer.UseNetCoreTimer ? this.NetCoreTimer.Close(notifyObject) : this.NetFxTimer.Close(notifyObject));
			GC.SuppressFinalize(this);
			return flag;
		}

		// Token: 0x04001A4D RID: 6733
		private object m_timer;
	}
}
