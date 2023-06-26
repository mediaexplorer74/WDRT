using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Timers
{
	/// <summary>Generates an event after a set interval, with an option to generate recurring events.</summary>
	// Token: 0x0200006D RID: 109
	[DefaultProperty("Interval")]
	[DefaultEvent("Elapsed")]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class Timer : Component, ISupportInitialize
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Timers.Timer" /> class, and sets all the properties to their initial values.</summary>
		// Token: 0x06000473 RID: 1139 RVA: 0x0001EF28 File Offset: 0x0001D128
		public Timer()
		{
			this.interval = 100.0;
			this.enabled = false;
			this.autoReset = true;
			this.initializing = false;
			this.delayedEnable = false;
			this.callback = new TimerCallback(this.MyTimerCallback);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Timers.Timer" /> class, and sets the <see cref="P:System.Timers.Timer.Interval" /> property to the specified number of milliseconds.</summary>
		/// <param name="interval">The time, in milliseconds, between events. The value must be greater than zero and less than or equal to <see cref="F:System.Int32.MaxValue" />.</param>
		/// <exception cref="T:System.ArgumentException">The value of the <paramref name="interval" /> parameter is less than or equal to zero, or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06000474 RID: 1140 RVA: 0x0001EF78 File Offset: 0x0001D178
		public Timer(double interval)
			: this()
		{
			if (interval <= 0.0)
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[] { "interval", interval }));
			}
			double num = Math.Ceiling(interval);
			if (num > 2147483647.0 || num <= 0.0)
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[] { "interval", interval }));
			}
			this.interval = (double)((int)num);
		}

		/// <summary>Gets or sets a Boolean indicating whether the <see cref="T:System.Timers.Timer" /> should raise the <see cref="E:System.Timers.Timer.Elapsed" /> event only once (<see langword="false" />) or repeatedly (<see langword="true" />).</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Timers.Timer" /> should raise the <see cref="E:System.Timers.Timer.Elapsed" /> event each time the interval elapses; <see langword="false" /> if it should raise the <see cref="E:System.Timers.Timer.Elapsed" /> event only once, after the first time the interval elapses. The default is <see langword="true" />.</returns>
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x0001F00D File Offset: 0x0001D20D
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x0001F015 File Offset: 0x0001D215
		[Category("Behavior")]
		[TimersDescription("TimerAutoReset")]
		[DefaultValue(true)]
		public bool AutoReset
		{
			get
			{
				return this.autoReset;
			}
			set
			{
				if (base.DesignMode)
				{
					this.autoReset = value;
					return;
				}
				if (this.autoReset != value)
				{
					this.autoReset = value;
					if (this.timer != null)
					{
						this.UpdateTimer();
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Timers.Timer" /> should raise the <see cref="E:System.Timers.Timer.Elapsed" /> event.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Timers.Timer" /> should raise the <see cref="E:System.Timers.Timer.Elapsed" /> event; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This property cannot be set because the timer has been disposed.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Timers.Timer.Interval" /> property was set to a value greater than <see cref="F:System.Int32.MaxValue" /> before the timer was enabled.</exception>
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0001F045 File Offset: 0x0001D245
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x0001F050 File Offset: 0x0001D250
		[Category("Behavior")]
		[TimersDescription("TimerEnabled")]
		[DefaultValue(false)]
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				if (base.DesignMode)
				{
					this.delayedEnable = value;
					this.enabled = value;
					return;
				}
				if (this.initializing)
				{
					this.delayedEnable = value;
					return;
				}
				if (this.enabled != value)
				{
					if (!value)
					{
						if (this.timer != null)
						{
							this.cookie = null;
							this.timer.Dispose();
							this.timer = null;
						}
						this.enabled = value;
						return;
					}
					this.enabled = value;
					if (this.timer == null)
					{
						if (this.disposed)
						{
							throw new ObjectDisposedException(base.GetType().Name);
						}
						int num = (int)Math.Ceiling(this.interval);
						this.cookie = new object();
						this.timer = new Timer(this.callback, this.cookie, num, this.autoReset ? num : (-1));
						return;
					}
					else
					{
						this.UpdateTimer();
					}
				}
			}
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0001F128 File Offset: 0x0001D328
		private void UpdateTimer()
		{
			int num = (int)Math.Ceiling(this.interval);
			this.timer.Change(num, this.autoReset ? num : (-1));
		}

		/// <summary>Gets or sets the interval, expressed in milliseconds, at which to raise the <see cref="E:System.Timers.Timer.Elapsed" /> event.</summary>
		/// <returns>The time, in milliseconds, between <see cref="E:System.Timers.Timer.Elapsed" /> events. The value must be greater than zero, and less than or equal to <see cref="F:System.Int32.MaxValue" />. The default is 100 milliseconds.</returns>
		/// <exception cref="T:System.ArgumentException">The interval is less than or equal to zero.  
		///  -or-  
		///  The interval is greater than <see cref="F:System.Int32.MaxValue" />, and the timer is currently enabled. (If the timer is not currently enabled, no exception is thrown until it becomes enabled.)</exception>
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x0001F15B File Offset: 0x0001D35B
		// (set) Token: 0x0600047B RID: 1147 RVA: 0x0001F164 File Offset: 0x0001D364
		[Category("Behavior")]
		[TimersDescription("TimerInterval")]
		[DefaultValue(100.0)]
		[SettingsBindable(true)]
		public double Interval
		{
			get
			{
				return this.interval;
			}
			set
			{
				if (value <= 0.0)
				{
					throw new ArgumentException(SR.GetString("TimerInvalidInterval", new object[] { value, 0 }));
				}
				this.interval = value;
				if (this.timer != null)
				{
					this.UpdateTimer();
				}
			}
		}

		/// <summary>Occurs when the interval elapses.</summary>
		// Token: 0x1400000F RID: 15
		// (add) Token: 0x0600047C RID: 1148 RVA: 0x0001F1BA File Offset: 0x0001D3BA
		// (remove) Token: 0x0600047D RID: 1149 RVA: 0x0001F1D3 File Offset: 0x0001D3D3
		[Category("Behavior")]
		[TimersDescription("TimerIntervalElapsed")]
		public event ElapsedEventHandler Elapsed
		{
			add
			{
				this.onIntervalElapsed = (ElapsedEventHandler)Delegate.Combine(this.onIntervalElapsed, value);
			}
			remove
			{
				this.onIntervalElapsed = (ElapsedEventHandler)Delegate.Remove(this.onIntervalElapsed, value);
			}
		}

		/// <summary>Gets or sets the site that binds the <see cref="T:System.Timers.Timer" /> to its container in design mode.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ISite" /> interface representing the site that binds the <see cref="T:System.Timers.Timer" /> object to its container.</returns>
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x0001F204 File Offset: 0x0001D404
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x0001F1EC File Offset: 0x0001D3EC
		public override ISite Site
		{
			get
			{
				return base.Site;
			}
			set
			{
				base.Site = value;
				if (base.DesignMode)
				{
					this.enabled = true;
				}
			}
		}

		/// <summary>Gets or sets the object used to marshal event-handler calls that are issued when an interval has elapsed.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISynchronizeInvoke" /> representing the object used to marshal the event-handler calls that are issued when an interval has elapsed. The default is <see langword="null" />.</returns>
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x0001F20C File Offset: 0x0001D40C
		// (set) Token: 0x06000481 RID: 1153 RVA: 0x0001F266 File Offset: 0x0001D466
		[Browsable(false)]
		[DefaultValue(null)]
		[TimersDescription("TimerSynchronizingObject")]
		public ISynchronizeInvoke SynchronizingObject
		{
			get
			{
				if (this.synchronizingObject == null && base.DesignMode)
				{
					IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
					if (designerHost != null)
					{
						object rootComponent = designerHost.RootComponent;
						if (rootComponent != null && rootComponent is ISynchronizeInvoke)
						{
							this.synchronizingObject = (ISynchronizeInvoke)rootComponent;
						}
					}
				}
				return this.synchronizingObject;
			}
			set
			{
				this.synchronizingObject = value;
			}
		}

		/// <summary>Begins the run-time initialization of a <see cref="T:System.Timers.Timer" /> that is used on a form or by another component.</summary>
		// Token: 0x06000482 RID: 1154 RVA: 0x0001F26F File Offset: 0x0001D46F
		public void BeginInit()
		{
			this.Close();
			this.initializing = true;
		}

		/// <summary>Releases the resources used by the <see cref="T:System.Timers.Timer" />.</summary>
		// Token: 0x06000483 RID: 1155 RVA: 0x0001F27E File Offset: 0x0001D47E
		public void Close()
		{
			this.initializing = false;
			this.delayedEnable = false;
			this.enabled = false;
			if (this.timer != null)
			{
				this.timer.Dispose();
				this.timer = null;
			}
		}

		/// <summary>Releases all resources used by the current <see cref="T:System.Timers.Timer" />.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06000484 RID: 1156 RVA: 0x0001F2AF File Offset: 0x0001D4AF
		protected override void Dispose(bool disposing)
		{
			this.Close();
			this.disposed = true;
			base.Dispose(disposing);
		}

		/// <summary>Ends the run-time initialization of a <see cref="T:System.Timers.Timer" /> that is used on a form or by another component.</summary>
		// Token: 0x06000485 RID: 1157 RVA: 0x0001F2C5 File Offset: 0x0001D4C5
		public void EndInit()
		{
			this.initializing = false;
			this.Enabled = this.delayedEnable;
		}

		/// <summary>Starts raising the <see cref="E:System.Timers.Timer.Elapsed" /> event by setting <see cref="P:System.Timers.Timer.Enabled" /> to <see langword="true" />.</summary>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="T:System.Timers.Timer" /> is created with an interval equal to or greater than <see cref="F:System.Int32.MaxValue" /> + 1, or set to an interval less than zero.</exception>
		// Token: 0x06000486 RID: 1158 RVA: 0x0001F2DA File Offset: 0x0001D4DA
		public void Start()
		{
			this.Enabled = true;
		}

		/// <summary>Stops raising the <see cref="E:System.Timers.Timer.Elapsed" /> event by setting <see cref="P:System.Timers.Timer.Enabled" /> to <see langword="false" />.</summary>
		// Token: 0x06000487 RID: 1159 RVA: 0x0001F2E3 File Offset: 0x0001D4E3
		public void Stop()
		{
			this.Enabled = false;
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0001F2EC File Offset: 0x0001D4EC
		private void MyTimerCallback(object state)
		{
			if (state != this.cookie)
			{
				return;
			}
			if (!this.autoReset)
			{
				this.enabled = false;
			}
			Timer.FILE_TIME file_TIME = default(Timer.FILE_TIME);
			Timer.GetSystemTimeAsFileTime(ref file_TIME);
			ElapsedEventArgs elapsedEventArgs = new ElapsedEventArgs(file_TIME.ftTimeLow, file_TIME.ftTimeHigh);
			try
			{
				ElapsedEventHandler elapsedEventHandler = this.onIntervalElapsed;
				if (elapsedEventHandler != null)
				{
					if (this.SynchronizingObject != null && this.SynchronizingObject.InvokeRequired)
					{
						this.SynchronizingObject.BeginInvoke(elapsedEventHandler, new object[] { this, elapsedEventArgs });
					}
					else
					{
						elapsedEventHandler(this, elapsedEventArgs);
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000489 RID: 1161
		[SuppressUnmanagedCodeSecurity]
		[DllImport("kernel32.dll")]
		internal static extern void GetSystemTimeAsFileTime(ref Timer.FILE_TIME lpSystemTimeAsFileTime);

		// Token: 0x04000BCB RID: 3019
		private double interval;

		// Token: 0x04000BCC RID: 3020
		private bool enabled;

		// Token: 0x04000BCD RID: 3021
		private bool initializing;

		// Token: 0x04000BCE RID: 3022
		private bool delayedEnable;

		// Token: 0x04000BCF RID: 3023
		private ElapsedEventHandler onIntervalElapsed;

		// Token: 0x04000BD0 RID: 3024
		private bool autoReset;

		// Token: 0x04000BD1 RID: 3025
		private ISynchronizeInvoke synchronizingObject;

		// Token: 0x04000BD2 RID: 3026
		private bool disposed;

		// Token: 0x04000BD3 RID: 3027
		private Timer timer;

		// Token: 0x04000BD4 RID: 3028
		private TimerCallback callback;

		// Token: 0x04000BD5 RID: 3029
		private object cookie;

		// Token: 0x020006E5 RID: 1765
		internal struct FILE_TIME
		{
			// Token: 0x0400304F RID: 12367
			internal int ftTimeLow;

			// Token: 0x04003050 RID: 12368
			internal int ftTimeHigh;
		}
	}
}
