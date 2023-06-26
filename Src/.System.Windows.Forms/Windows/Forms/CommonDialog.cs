using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	/// <summary>Specifies the base class used for displaying dialog boxes on the screen.</summary>
	// Token: 0x02000162 RID: 354
	[ToolboxItemFilter("System.Windows.Forms")]
	public abstract class CommonDialog : Component
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.CommonDialog" /> class.</summary>
		// Token: 0x06000EB6 RID: 3766 RVA: 0x0002C0DA File Offset: 0x0002A2DA
		public CommonDialog()
		{
		}

		/// <summary>Gets or sets an object that contains data about the control.</summary>
		/// <returns>The object that contains data about the <see cref="T:System.Windows.Forms.CommonDialog" />.</returns>
		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x0002C0E2 File Offset: 0x0002A2E2
		// (set) Token: 0x06000EB8 RID: 3768 RVA: 0x0002C0EA File Offset: 0x0002A2EA
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

		/// <summary>Occurs when the user clicks the Help button on a common dialog box.</summary>
		// Token: 0x14000080 RID: 128
		// (add) Token: 0x06000EB9 RID: 3769 RVA: 0x0002C0F3 File Offset: 0x0002A2F3
		// (remove) Token: 0x06000EBA RID: 3770 RVA: 0x0002C106 File Offset: 0x0002A306
		[SRDescription("CommonDialogHelpRequested")]
		public event EventHandler HelpRequest
		{
			add
			{
				base.Events.AddHandler(CommonDialog.EventHelpRequest, value);
			}
			remove
			{
				base.Events.RemoveHandler(CommonDialog.EventHelpRequest, value);
			}
		}

		/// <summary>Defines the common dialog box hook procedure that is overridden to add specific functionality to a common dialog box.</summary>
		/// <param name="hWnd">The handle to the dialog box window.</param>
		/// <param name="msg">The message being received.</param>
		/// <param name="wparam">Additional information about the message.</param>
		/// <param name="lparam">Additional information about the message.</param>
		/// <returns>A zero value if the default dialog box procedure processes the message; a nonzero value if the default dialog box procedure ignores the message.</returns>
		// Token: 0x06000EBB RID: 3771 RVA: 0x0002C11C File Offset: 0x0002A31C
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected virtual IntPtr HookProc(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
		{
			if (msg == 272)
			{
				CommonDialog.MoveToScreenCenter(hWnd);
				this.defaultControlHwnd = wparam;
				UnsafeNativeMethods.SetFocus(new HandleRef(null, wparam));
			}
			else if (msg == 7)
			{
				UnsafeNativeMethods.PostMessage(new HandleRef(null, hWnd), 1105, 0, 0);
			}
			else if (msg == 1105)
			{
				UnsafeNativeMethods.SetFocus(new HandleRef(this, this.defaultControlHwnd));
			}
			return IntPtr.Zero;
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x0002C188 File Offset: 0x0002A388
		internal static void MoveToScreenCenter(IntPtr hWnd)
		{
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			UnsafeNativeMethods.GetWindowRect(new HandleRef(null, hWnd), ref rect);
			Rectangle workingArea = Screen.GetWorkingArea(Control.MousePosition);
			int num = workingArea.X + (workingArea.Width - rect.right + rect.left) / 2;
			int num2 = workingArea.Y + (workingArea.Height - rect.bottom + rect.top) / 3;
			SafeNativeMethods.SetWindowPos(new HandleRef(null, hWnd), NativeMethods.NullHandleRef, num, num2, 0, 0, 21);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.CommonDialog.HelpRequest" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Windows.Forms.HelpEventArgs" /> that provides the event data.</param>
		// Token: 0x06000EBD RID: 3773 RVA: 0x0002C210 File Offset: 0x0002A410
		protected virtual void OnHelpRequest(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[CommonDialog.EventHelpRequest];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Defines the owner window procedure that is overridden to add specific functionality to a common dialog box.</summary>
		/// <param name="hWnd">The window handle of the message to send.</param>
		/// <param name="msg">The Win32 message to send.</param>
		/// <param name="wparam">The <paramref name="wparam" /> to send with the message.</param>
		/// <param name="lparam">The <paramref name="lparam" /> to send with the message.</param>
		/// <returns>The result of the message processing, which is dependent on the message sent.</returns>
		// Token: 0x06000EBE RID: 3774 RVA: 0x0002C240 File Offset: 0x0002A440
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected virtual IntPtr OwnerWndProc(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
		{
			if (msg == CommonDialog.helpMsg)
			{
				if (NativeWindow.WndProcShouldBeDebuggable)
				{
					this.OnHelpRequest(EventArgs.Empty);
				}
				else
				{
					try
					{
						this.OnHelpRequest(EventArgs.Empty);
					}
					catch (Exception ex)
					{
						Application.OnThreadException(ex);
					}
				}
				return IntPtr.Zero;
			}
			return UnsafeNativeMethods.CallWindowProc(this.defOwnerWndProc, hWnd, msg, wparam, lparam);
		}

		/// <summary>When overridden in a derived class, resets the properties of a common dialog box to their default values.</summary>
		// Token: 0x06000EBF RID: 3775
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public abstract void Reset();

		/// <summary>When overridden in a derived class, specifies a common dialog box.</summary>
		/// <param name="hwndOwner">A value that represents the window handle of the owner window for the common dialog box.</param>
		/// <returns>
		///   <see langword="true" /> if the dialog box was successfully run; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000EC0 RID: 3776
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected abstract bool RunDialog(IntPtr hwndOwner);

		/// <summary>Runs a common dialog box with a default owner.</summary>
		/// <returns>
		///   <see cref="F:System.Windows.Forms.DialogResult.OK" /> if the user clicks OK in the dialog box; otherwise, <see cref="F:System.Windows.Forms.DialogResult.Cancel" />.</returns>
		// Token: 0x06000EC1 RID: 3777 RVA: 0x0002C2A8 File Offset: 0x0002A4A8
		public DialogResult ShowDialog()
		{
			return this.ShowDialog(null);
		}

		/// <summary>Runs a common dialog box with the specified owner.</summary>
		/// <param name="owner">Any object that implements <see cref="T:System.Windows.Forms.IWin32Window" /> that represents the top-level window that will own the modal dialog box.</param>
		/// <returns>
		///   <see cref="F:System.Windows.Forms.DialogResult.OK" /> if the user clicks OK in the dialog box; otherwise, <see cref="F:System.Windows.Forms.DialogResult.Cancel" />.</returns>
		// Token: 0x06000EC2 RID: 3778 RVA: 0x0002C2B4 File Offset: 0x0002A4B4
		public DialogResult ShowDialog(IWin32Window owner)
		{
			IntSecurity.SafeSubWindows.Demand();
			if (!SystemInformation.UserInteractive)
			{
				throw new InvalidOperationException(SR.GetString("CantShowModalOnNonInteractive"));
			}
			NativeWindow nativeWindow = null;
			IntPtr intPtr = IntPtr.Zero;
			DialogResult dialogResult = DialogResult.Cancel;
			try
			{
				if (owner != null)
				{
					intPtr = Control.GetSafeHandle(owner);
				}
				if (intPtr == IntPtr.Zero)
				{
					intPtr = UnsafeNativeMethods.GetActiveWindow();
				}
				if (intPtr == IntPtr.Zero)
				{
					nativeWindow = new NativeWindow();
					nativeWindow.CreateHandle(new CreateParams());
					intPtr = nativeWindow.Handle;
				}
				if (CommonDialog.helpMsg == 0)
				{
					CommonDialog.helpMsg = SafeNativeMethods.RegisterWindowMessage("commdlg_help");
				}
				NativeMethods.WndProc wndProc = new NativeMethods.WndProc(this.OwnerWndProc);
				this.hookedWndProc = Marshal.GetFunctionPointerForDelegate(wndProc);
				IntPtr intPtr2 = IntPtr.Zero;
				try
				{
					this.defOwnerWndProc = UnsafeNativeMethods.SetWindowLong(new HandleRef(this, intPtr), -4, wndProc);
					if (Application.UseVisualStyles)
					{
						intPtr2 = UnsafeNativeMethods.ThemingScope.Activate();
					}
					Application.BeginModalMessageLoop();
					try
					{
						dialogResult = (this.RunDialog(intPtr) ? DialogResult.OK : DialogResult.Cancel);
					}
					finally
					{
						Application.EndModalMessageLoop();
					}
				}
				finally
				{
					IntPtr windowLong = UnsafeNativeMethods.GetWindowLong(new HandleRef(this, intPtr), -4);
					if (IntPtr.Zero != this.defOwnerWndProc || windowLong != this.hookedWndProc)
					{
						UnsafeNativeMethods.SetWindowLong(new HandleRef(this, intPtr), -4, new HandleRef(this, this.defOwnerWndProc));
					}
					UnsafeNativeMethods.ThemingScope.Deactivate(intPtr2);
					this.defOwnerWndProc = IntPtr.Zero;
					this.hookedWndProc = IntPtr.Zero;
					GC.KeepAlive(wndProc);
				}
			}
			finally
			{
				if (nativeWindow != null)
				{
					nativeWindow.DestroyHandle();
				}
			}
			return dialogResult;
		}

		// Token: 0x040007EA RID: 2026
		private static readonly object EventHelpRequest = new object();

		// Token: 0x040007EB RID: 2027
		private const int CDM_SETDEFAULTFOCUS = 1105;

		// Token: 0x040007EC RID: 2028
		private static int helpMsg;

		// Token: 0x040007ED RID: 2029
		private IntPtr defOwnerWndProc;

		// Token: 0x040007EE RID: 2030
		private IntPtr hookedWndProc;

		// Token: 0x040007EF RID: 2031
		private IntPtr defaultControlHwnd;

		// Token: 0x040007F0 RID: 2032
		private object userData;
	}
}
