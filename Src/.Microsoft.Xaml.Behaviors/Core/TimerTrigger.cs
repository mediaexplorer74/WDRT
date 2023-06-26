using System;
using System.Windows;
using System.Windows.Threading;

namespace Microsoft.Xaml.Behaviors.Core
{
	// Token: 0x02000053 RID: 83
	public class TimerTrigger : EventTrigger
	{
		// Token: 0x060002E4 RID: 740 RVA: 0x0000C531 File Offset: 0x0000A731
		public TimerTrigger()
			: this(new TimerTrigger.DispatcherTickTimer())
		{
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000C53E File Offset: 0x0000A73E
		internal TimerTrigger(ITickTimer timer)
		{
			this.timer = timer;
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000C54D File Offset: 0x0000A74D
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x0000C55F File Offset: 0x0000A75F
		public double MillisecondsPerTick
		{
			get
			{
				return (double)base.GetValue(TimerTrigger.MillisecondsPerTickProperty);
			}
			set
			{
				base.SetValue(TimerTrigger.MillisecondsPerTickProperty, value);
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000C572 File Offset: 0x0000A772
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x0000C584 File Offset: 0x0000A784
		public int TotalTicks
		{
			get
			{
				return (int)base.GetValue(TimerTrigger.TotalTicksProperty);
			}
			set
			{
				base.SetValue(TimerTrigger.TotalTicksProperty, value);
			}
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000C597 File Offset: 0x0000A797
		protected override void OnEvent(EventArgs eventArgs)
		{
			this.StopTimer();
			this.eventArgs = eventArgs;
			this.tickCount = 0;
			this.StartTimer();
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000C5B3 File Offset: 0x0000A7B3
		protected override void OnDetaching()
		{
			this.StopTimer();
			base.OnDetaching();
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000C5C4 File Offset: 0x0000A7C4
		internal void StartTimer()
		{
			if (this.timer != null)
			{
				this.timer.Interval = TimeSpan.FromMilliseconds(this.MillisecondsPerTick);
				this.timer.Tick += this.OnTimerTick;
				this.timer.Start();
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000C611 File Offset: 0x0000A811
		internal void StopTimer()
		{
			if (this.timer != null)
			{
				this.timer.Stop();
				this.timer.Tick -= this.OnTimerTick;
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000C640 File Offset: 0x0000A840
		private void OnTimerTick(object sender, EventArgs e)
		{
			if (this.TotalTicks > 0)
			{
				int num = this.tickCount + 1;
				this.tickCount = num;
				if (num >= this.TotalTicks)
				{
					this.StopTimer();
				}
			}
			base.InvokeActions(this.eventArgs);
		}

		// Token: 0x040000E6 RID: 230
		public static readonly DependencyProperty MillisecondsPerTickProperty = DependencyProperty.Register("MillisecondsPerTick", typeof(double), typeof(TimerTrigger), new FrameworkPropertyMetadata(1000.0));

		// Token: 0x040000E7 RID: 231
		public static readonly DependencyProperty TotalTicksProperty = DependencyProperty.Register("TotalTicks", typeof(int), typeof(TimerTrigger), new FrameworkPropertyMetadata(-1));

		// Token: 0x040000E8 RID: 232
		private ITickTimer timer;

		// Token: 0x040000E9 RID: 233
		private EventArgs eventArgs;

		// Token: 0x040000EA RID: 234
		private int tickCount;

		// Token: 0x02000066 RID: 102
		internal class DispatcherTickTimer : ITickTimer
		{
			// Token: 0x0600035C RID: 860 RVA: 0x0000CEC4 File Offset: 0x0000B0C4
			public DispatcherTickTimer()
			{
				this.dispatcherTimer = new DispatcherTimer();
			}

			// Token: 0x1400000A RID: 10
			// (add) Token: 0x0600035D RID: 861 RVA: 0x0000CED7 File Offset: 0x0000B0D7
			// (remove) Token: 0x0600035E RID: 862 RVA: 0x0000CEE5 File Offset: 0x0000B0E5
			public event EventHandler Tick
			{
				add
				{
					this.dispatcherTimer.Tick += value;
				}
				remove
				{
					this.dispatcherTimer.Tick -= value;
				}
			}

			// Token: 0x170000DC RID: 220
			// (get) Token: 0x0600035F RID: 863 RVA: 0x0000CEF3 File Offset: 0x0000B0F3
			// (set) Token: 0x06000360 RID: 864 RVA: 0x0000CF00 File Offset: 0x0000B100
			public TimeSpan Interval
			{
				get
				{
					return this.dispatcherTimer.Interval;
				}
				set
				{
					this.dispatcherTimer.Interval = value;
				}
			}

			// Token: 0x06000361 RID: 865 RVA: 0x0000CF0E File Offset: 0x0000B10E
			public void Start()
			{
				this.dispatcherTimer.Start();
			}

			// Token: 0x06000362 RID: 866 RVA: 0x0000CF1B File Offset: 0x0000B11B
			public void Stop()
			{
				this.dispatcherTimer.Stop();
			}

			// Token: 0x04000127 RID: 295
			private DispatcherTimer dispatcherTimer;
		}
	}
}
