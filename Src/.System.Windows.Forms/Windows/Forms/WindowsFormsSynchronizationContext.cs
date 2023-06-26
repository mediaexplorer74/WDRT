using System;
using System.ComponentModel;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Windows.Forms
{
	/// <summary>Provides a synchronization context for the Windows Forms application model.</summary>
	// Token: 0x02000442 RID: 1090
	public sealed class WindowsFormsSynchronizationContext : SynchronizationContext, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.WindowsFormsSynchronizationContext" /> class.</summary>
		// Token: 0x06004BB4 RID: 19380 RVA: 0x0013AD18 File Offset: 0x00138F18
		public WindowsFormsSynchronizationContext()
		{
			this.DestinationThread = Thread.CurrentThread;
			Application.ThreadContext threadContext = Application.ThreadContext.FromCurrent();
			if (threadContext != null)
			{
				this.controlToSendTo = threadContext.MarshalingControl;
			}
		}

		// Token: 0x06004BB5 RID: 19381 RVA: 0x0013AD4B File Offset: 0x00138F4B
		private WindowsFormsSynchronizationContext(Control marshalingControl, Thread destinationThread)
		{
			this.controlToSendTo = marshalingControl;
			this.DestinationThread = destinationThread;
		}

		// Token: 0x1700126F RID: 4719
		// (get) Token: 0x06004BB6 RID: 19382 RVA: 0x0013AD61 File Offset: 0x00138F61
		// (set) Token: 0x06004BB7 RID: 19383 RVA: 0x0013AD8A File Offset: 0x00138F8A
		private Thread DestinationThread
		{
			get
			{
				if (this.destinationThreadRef != null && this.destinationThreadRef.IsAlive)
				{
					return this.destinationThreadRef.Target as Thread;
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this.destinationThreadRef = new WeakReference(value);
				}
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Windows.Forms.WindowsFormsSynchronizationContext" />.</summary>
		// Token: 0x06004BB8 RID: 19384 RVA: 0x0013AD9B File Offset: 0x00138F9B
		public void Dispose()
		{
			if (this.controlToSendTo != null)
			{
				if (!this.controlToSendTo.IsDisposed)
				{
					this.controlToSendTo.Dispose();
				}
				this.controlToSendTo = null;
			}
		}

		/// <summary>Dispatches a synchronous message to a synchronization context</summary>
		/// <param name="d">The <see cref="T:System.Threading.SendOrPostCallback" /> delegate to call.</param>
		/// <param name="state">The object passed to the delegate.</param>
		/// <exception cref="T:System.ComponentModel.InvalidAsynchronousStateException">The destination thread no longer exists.</exception>
		// Token: 0x06004BB9 RID: 19385 RVA: 0x0013ADC4 File Offset: 0x00138FC4
		public override void Send(SendOrPostCallback d, object state)
		{
			Thread destinationThread = this.DestinationThread;
			if (destinationThread == null || !destinationThread.IsAlive)
			{
				throw new InvalidAsynchronousStateException(SR.GetString("ThreadNoLongerValid"));
			}
			if (this.controlToSendTo != null)
			{
				this.controlToSendTo.Invoke(d, new object[] { state });
			}
		}

		/// <summary>Dispatches an asynchronous message to a synchronization context.</summary>
		/// <param name="d">The <see cref="T:System.Threading.SendOrPostCallback" /> delegate to call.</param>
		/// <param name="state">The object passed to the delegate.</param>
		// Token: 0x06004BBA RID: 19386 RVA: 0x0013AE12 File Offset: 0x00139012
		public override void Post(SendOrPostCallback d, object state)
		{
			if (this.controlToSendTo != null)
			{
				this.controlToSendTo.BeginInvoke(d, new object[] { state });
			}
		}

		/// <summary>Copies the synchronization context.</summary>
		/// <returns>A copy of the synchronization context.</returns>
		// Token: 0x06004BBB RID: 19387 RVA: 0x0013AE33 File Offset: 0x00139033
		public override SynchronizationContext CreateCopy()
		{
			return new WindowsFormsSynchronizationContext(this.controlToSendTo, this.DestinationThread);
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.WindowsFormsSynchronizationContext" /> is installed when a control is created.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.WindowsFormsSynchronizationContext" /> is installed; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17001270 RID: 4720
		// (get) Token: 0x06004BBC RID: 19388 RVA: 0x0013AE46 File Offset: 0x00139046
		// (set) Token: 0x06004BBD RID: 19389 RVA: 0x0013AE50 File Offset: 0x00139050
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static bool AutoInstall
		{
			get
			{
				return !WindowsFormsSynchronizationContext.dontAutoInstall;
			}
			set
			{
				WindowsFormsSynchronizationContext.dontAutoInstall = !value;
			}
		}

		// Token: 0x06004BBE RID: 19390 RVA: 0x0013AE5C File Offset: 0x0013905C
		internal static void InstallIfNeeded()
		{
			if (!WindowsFormsSynchronizationContext.AutoInstall || WindowsFormsSynchronizationContext.inSyncContextInstallation)
			{
				return;
			}
			if (SynchronizationContext.Current == null)
			{
				WindowsFormsSynchronizationContext.previousSyncContext = null;
			}
			if (WindowsFormsSynchronizationContext.previousSyncContext != null)
			{
				return;
			}
			WindowsFormsSynchronizationContext.inSyncContextInstallation = true;
			try
			{
				SynchronizationContext synchronizationContext = AsyncOperationManager.SynchronizationContext;
				if (synchronizationContext == null || synchronizationContext.GetType() == typeof(SynchronizationContext))
				{
					WindowsFormsSynchronizationContext.previousSyncContext = synchronizationContext;
					new PermissionSet(PermissionState.Unrestricted).Assert();
					try
					{
						AsyncOperationManager.SynchronizationContext = new WindowsFormsSynchronizationContext();
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
			}
			finally
			{
				WindowsFormsSynchronizationContext.inSyncContextInstallation = false;
			}
		}

		/// <summary>Uninstalls the currently installed <see cref="T:System.Windows.Forms.WindowsFormsSynchronizationContext" /> and replaces it with the previously installed context.</summary>
		// Token: 0x06004BBF RID: 19391 RVA: 0x0013AEFC File Offset: 0x001390FC
		public static void Uninstall()
		{
			WindowsFormsSynchronizationContext.Uninstall(true);
		}

		// Token: 0x06004BC0 RID: 19392 RVA: 0x0013AF04 File Offset: 0x00139104
		internal static void Uninstall(bool turnOffAutoInstall)
		{
			if (WindowsFormsSynchronizationContext.AutoInstall)
			{
				WindowsFormsSynchronizationContext windowsFormsSynchronizationContext = AsyncOperationManager.SynchronizationContext as WindowsFormsSynchronizationContext;
				if (windowsFormsSynchronizationContext != null)
				{
					try
					{
						new PermissionSet(PermissionState.Unrestricted).Assert();
						if (WindowsFormsSynchronizationContext.previousSyncContext == null)
						{
							AsyncOperationManager.SynchronizationContext = new SynchronizationContext();
						}
						else
						{
							AsyncOperationManager.SynchronizationContext = WindowsFormsSynchronizationContext.previousSyncContext;
						}
					}
					finally
					{
						WindowsFormsSynchronizationContext.previousSyncContext = null;
						CodeAccessPermission.RevertAssert();
					}
				}
			}
			if (turnOffAutoInstall)
			{
				WindowsFormsSynchronizationContext.AutoInstall = false;
			}
		}

		// Token: 0x04002833 RID: 10291
		private Control controlToSendTo;

		// Token: 0x04002834 RID: 10292
		private WeakReference destinationThreadRef;

		// Token: 0x04002835 RID: 10293
		[ThreadStatic]
		private static bool dontAutoInstall;

		// Token: 0x04002836 RID: 10294
		[ThreadStatic]
		private static bool inSyncContextInstallation;

		// Token: 0x04002837 RID: 10295
		[ThreadStatic]
		private static SynchronizationContext previousSyncContext;
	}
}
