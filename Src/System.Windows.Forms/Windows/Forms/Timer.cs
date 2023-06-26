using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Implements a timer that raises an event at user-defined intervals. This timer is optimized for use in Windows Forms applications and must be used in a window.</summary>
	// Token: 0x020003A6 RID: 934
	[DefaultProperty("Interval")]
	[DefaultEvent("Tick")]
	[ToolboxItemFilter("System.Windows.Forms")]
	[SRDescription("DescriptionTimer")]
	public class Timer : Component
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Timer" /> class.</summary>
		// Token: 0x06003CF1 RID: 15601 RVA: 0x00109080 File Offset: 0x00107280
		public Timer()
		{
			this.interval = 100;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Timer" /> class together with the specified container.</summary>
		/// <param name="container">An <see cref="T:System.ComponentModel.IContainer" /> that represents the container for the timer.</param>
		// Token: 0x06003CF2 RID: 15602 RVA: 0x0010909B File Offset: 0x0010729B
		public Timer(IContainer container)
			: this()
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			container.Add(this);
		}

		/// <summary>Gets or sets an arbitrary string representing some type of user state.</summary>
		/// <returns>An arbitrary string representing some type of user state.</returns>
		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x06003CF3 RID: 15603 RVA: 0x001090B8 File Offset: 0x001072B8
		// (set) Token: 0x06003CF4 RID: 15604 RVA: 0x001090C0 File Offset: 0x001072C0
		[SRCategory("CatData")]
		[Localizable(false)]
		[Bindable(true)]
		[SRDescription("ControlTagDescr")]
		[DefaultValue(null)]
		[TypeConverter(typeof(StringConverter))]
		public object Tag
		{
			get
			{
				return this.userData;
			}
			set
			{
				this.userData = value;
			}
		}

		/// <summary>Occurs when the specified timer interval has elapsed and the timer is enabled.</summary>
		// Token: 0x140002E9 RID: 745
		// (add) Token: 0x06003CF5 RID: 15605 RVA: 0x001090C9 File Offset: 0x001072C9
		// (remove) Token: 0x06003CF6 RID: 15606 RVA: 0x001090E2 File Offset: 0x001072E2
		[SRCategory("CatBehavior")]
		[SRDescription("TimerTimerDescr")]
		public event EventHandler Tick
		{
			add
			{
				this.onTimer = (EventHandler)Delegate.Combine(this.onTimer, value);
			}
			remove
			{
				this.onTimer = (EventHandler)Delegate.Remove(this.onTimer, value);
			}
		}

		/// <summary>Disposes of the resources, other than memory, used by the timer.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources. <see langword="false" /> to release only the unmanaged resources.</param>
		// Token: 0x06003CF7 RID: 15607 RVA: 0x001090FB File Offset: 0x001072FB
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.timerWindow != null)
				{
					this.timerWindow.StopTimer();
				}
				this.Enabled = false;
			}
			this.timerWindow = null;
			base.Dispose(disposing);
		}

		/// <summary>Gets or sets whether the timer is running.</summary>
		/// <returns>
		///   <see langword="true" /> if the timer is currently enabled; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x06003CF8 RID: 15608 RVA: 0x00109128 File Offset: 0x00107328
		// (set) Token: 0x06003CF9 RID: 15609 RVA: 0x00109144 File Offset: 0x00107344
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("TimerEnabledDescr")]
		public virtual bool Enabled
		{
			get
			{
				if (this.timerWindow == null)
				{
					return this.enabled;
				}
				return this.timerWindow.IsTimerRunning;
			}
			set
			{
				object obj = this.syncObj;
				lock (obj)
				{
					if (this.enabled != value)
					{
						this.enabled = value;
						if (!base.DesignMode)
						{
							if (value)
							{
								if (this.timerWindow == null)
								{
									this.timerWindow = new Timer.TimerNativeWindow(this);
								}
								this.timerRoot = GCHandle.Alloc(this);
								this.timerWindow.StartTimer(this.interval);
							}
							else
							{
								if (this.timerWindow != null)
								{
									this.timerWindow.StopTimer();
								}
								if (this.timerRoot.IsAllocated)
								{
									this.timerRoot.Free();
								}
							}
						}
					}
				}
			}
		}

		/// <summary>Gets or sets the time, in milliseconds, before the <see cref="E:System.Windows.Forms.Timer.Tick" /> event is raised relative to the last occurrence of the <see cref="E:System.Windows.Forms.Timer.Tick" /> event.</summary>
		/// <returns>An <see cref="T:System.Int32" /> specifying the number of milliseconds before the <see cref="E:System.Windows.Forms.Timer.Tick" /> event is raised relative to the last occurrence of the <see cref="E:System.Windows.Forms.Timer.Tick" /> event. The value cannot be less than one.</returns>
		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x06003CFA RID: 15610 RVA: 0x001091F8 File Offset: 0x001073F8
		// (set) Token: 0x06003CFB RID: 15611 RVA: 0x00109200 File Offset: 0x00107400
		[SRCategory("CatBehavior")]
		[DefaultValue(100)]
		[SRDescription("TimerIntervalDescr")]
		public int Interval
		{
			get
			{
				return this.interval;
			}
			set
			{
				object obj = this.syncObj;
				lock (obj)
				{
					if (value < 1)
					{
						throw new ArgumentOutOfRangeException("Interval", SR.GetString("TimerInvalidInterval", new object[]
						{
							value,
							0.ToString(CultureInfo.CurrentCulture)
						}));
					}
					if (this.interval != value)
					{
						this.interval = value;
						if (this.Enabled && !base.DesignMode && this.timerWindow != null)
						{
							this.timerWindow.RestartTimer(value);
						}
					}
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Timer.Tick" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. This is always <see cref="F:System.EventArgs.Empty" />.</param>
		// Token: 0x06003CFC RID: 15612 RVA: 0x001092A8 File Offset: 0x001074A8
		protected virtual void OnTick(EventArgs e)
		{
			if (this.onTimer != null)
			{
				this.onTimer(this, e);
			}
		}

		/// <summary>Starts the timer.</summary>
		// Token: 0x06003CFD RID: 15613 RVA: 0x001092BF File Offset: 0x001074BF
		public void Start()
		{
			this.Enabled = true;
		}

		/// <summary>Stops the timer.</summary>
		// Token: 0x06003CFE RID: 15614 RVA: 0x001092C8 File Offset: 0x001074C8
		public void Stop()
		{
			this.Enabled = false;
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.Timer" />.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.Timer" />.</returns>
		// Token: 0x06003CFF RID: 15615 RVA: 0x001092D4 File Offset: 0x001074D4
		public override string ToString()
		{
			string text = base.ToString();
			return text + ", Interval: " + this.Interval.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x040023F1 RID: 9201
		private int interval;

		// Token: 0x040023F2 RID: 9202
		private bool enabled;

		// Token: 0x040023F3 RID: 9203
		internal EventHandler onTimer;

		// Token: 0x040023F4 RID: 9204
		private GCHandle timerRoot;

		// Token: 0x040023F5 RID: 9205
		private Timer.TimerNativeWindow timerWindow;

		// Token: 0x040023F6 RID: 9206
		private object userData;

		// Token: 0x040023F7 RID: 9207
		private object syncObj = new object();

		// Token: 0x020007F2 RID: 2034
		private class TimerNativeWindow : NativeWindow
		{
			// Token: 0x06006E49 RID: 28233 RVA: 0x00193D61 File Offset: 0x00191F61
			internal TimerNativeWindow(Timer owner)
			{
				this._owner = owner;
			}

			// Token: 0x06006E4A RID: 28234 RVA: 0x00193D70 File Offset: 0x00191F70
			~TimerNativeWindow()
			{
				this.StopTimer();
			}

			// Token: 0x1700181B RID: 6171
			// (get) Token: 0x06006E4B RID: 28235 RVA: 0x00193D9C File Offset: 0x00191F9C
			public bool IsTimerRunning
			{
				get
				{
					return this._timerID != 0 && base.Handle != IntPtr.Zero;
				}
			}

			// Token: 0x06006E4C RID: 28236 RVA: 0x00193DB8 File Offset: 0x00191FB8
			private bool EnsureHandle()
			{
				if (base.Handle == IntPtr.Zero)
				{
					CreateParams createParams = new CreateParams();
					createParams.Style = 0;
					createParams.ExStyle = 0;
					createParams.ClassStyle = 0;
					createParams.Caption = base.GetType().Name;
					if (Environment.OSVersion.Platform == PlatformID.Win32NT)
					{
						createParams.Parent = (IntPtr)NativeMethods.HWND_MESSAGE;
					}
					this.CreateHandle(createParams);
				}
				return base.Handle != IntPtr.Zero;
			}

			// Token: 0x06006E4D RID: 28237 RVA: 0x00193E38 File Offset: 0x00192038
			private bool GetInvokeRequired(IntPtr hWnd)
			{
				if (hWnd != IntPtr.Zero)
				{
					int num;
					int windowThreadProcessId = SafeNativeMethods.GetWindowThreadProcessId(new HandleRef(this, hWnd), out num);
					int currentThreadId = SafeNativeMethods.GetCurrentThreadId();
					return windowThreadProcessId != currentThreadId;
				}
				return false;
			}

			// Token: 0x06006E4E RID: 28238 RVA: 0x00193E70 File Offset: 0x00192070
			public void RestartTimer(int newInterval)
			{
				this.StopTimer(false, IntPtr.Zero);
				this.StartTimer(newInterval);
			}

			// Token: 0x06006E4F RID: 28239 RVA: 0x00193E88 File Offset: 0x00192088
			public void StartTimer(int interval)
			{
				if (this._timerID == 0 && !this._stoppingTimer && this.EnsureHandle())
				{
					this._timerID = (int)SafeNativeMethods.SetTimer(new HandleRef(this, base.Handle), Timer.TimerNativeWindow.TimerID++, interval, IntPtr.Zero);
				}
			}

			// Token: 0x06006E50 RID: 28240 RVA: 0x00193EDC File Offset: 0x001920DC
			public void StopTimer()
			{
				this.StopTimer(true, IntPtr.Zero);
			}

			// Token: 0x06006E51 RID: 28241 RVA: 0x00193EEC File Offset: 0x001920EC
			public void StopTimer(bool destroyHwnd, IntPtr hWnd)
			{
				if (hWnd == IntPtr.Zero)
				{
					hWnd = base.Handle;
				}
				if (this.GetInvokeRequired(hWnd))
				{
					UnsafeNativeMethods.PostMessage(new HandleRef(this, hWnd), 16, 0, 0);
					return;
				}
				lock (this)
				{
					if (!this._stoppingTimer && !(hWnd == IntPtr.Zero) && UnsafeNativeMethods.IsWindow(new HandleRef(this, hWnd)))
					{
						if (this._timerID != 0)
						{
							try
							{
								this._stoppingTimer = true;
								SafeNativeMethods.KillTimer(new HandleRef(this, hWnd), this._timerID);
							}
							finally
							{
								this._timerID = 0;
								this._stoppingTimer = false;
							}
						}
						if (destroyHwnd)
						{
							base.DestroyHandle();
						}
					}
				}
			}

			// Token: 0x06006E52 RID: 28242 RVA: 0x00193FC0 File Offset: 0x001921C0
			public override void DestroyHandle()
			{
				this.StopTimer(false, IntPtr.Zero);
				base.DestroyHandle();
			}

			// Token: 0x06006E53 RID: 28243 RVA: 0x0003B8FD File Offset: 0x00039AFD
			protected override void OnThreadException(Exception e)
			{
				Application.OnThreadException(e);
			}

			// Token: 0x06006E54 RID: 28244 RVA: 0x00193FD4 File Offset: 0x001921D4
			public override void ReleaseHandle()
			{
				this.StopTimer(false, IntPtr.Zero);
				base.ReleaseHandle();
			}

			// Token: 0x06006E55 RID: 28245 RVA: 0x00193FE8 File Offset: 0x001921E8
			protected override void WndProc(ref Message m)
			{
				if (m.Msg == 275)
				{
					if ((int)(long)m.WParam == this._timerID)
					{
						this._owner.OnTick(EventArgs.Empty);
						return;
					}
				}
				else if (m.Msg == 16)
				{
					this.StopTimer(true, m.HWnd);
					return;
				}
				base.WndProc(ref m);
			}

			// Token: 0x040042D8 RID: 17112
			private Timer _owner;

			// Token: 0x040042D9 RID: 17113
			private int _timerID;

			// Token: 0x040042DA RID: 17114
			private static int TimerID = 1;

			// Token: 0x040042DB RID: 17115
			private bool _stoppingTimer;
		}
	}
}
