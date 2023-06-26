using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Windows.Forms
{
	// Token: 0x020002EA RID: 746
	internal class MdiControlStrip : MenuStrip
	{
		// Token: 0x06002F77 RID: 12151 RVA: 0x000D6064 File Offset: 0x000D4264
		public MdiControlStrip(IWin32Window target)
		{
			IntPtr systemMenu = UnsafeNativeMethods.GetSystemMenu(new HandleRef(this, Control.GetSafeHandle(target)), false);
			this.target = target;
			this.minimize = new MdiControlStrip.ControlBoxMenuItem(systemMenu, 61472, target);
			this.close = new MdiControlStrip.ControlBoxMenuItem(systemMenu, 61536, target);
			this.restore = new MdiControlStrip.ControlBoxMenuItem(systemMenu, 61728, target);
			this.system = new MdiControlStrip.SystemMenuItem();
			Control control = target as Control;
			if (control != null)
			{
				control.HandleCreated += this.OnTargetWindowHandleRecreated;
				control.Disposed += this.OnTargetWindowDisposed;
			}
			this.Items.AddRange(new ToolStripItem[] { this.minimize, this.restore, this.close, this.system });
			base.SuspendLayout();
			foreach (object obj in this.Items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				toolStripItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
				toolStripItem.MergeIndex = 0;
				toolStripItem.MergeAction = MergeAction.Insert;
				toolStripItem.Overflow = ToolStripItemOverflow.Never;
				toolStripItem.Alignment = ToolStripItemAlignment.Right;
				toolStripItem.Padding = Padding.Empty;
				toolStripItem.ImageScaling = ToolStripItemImageScaling.SizeToFit;
			}
			this.system.Image = this.GetTargetWindowIcon();
			this.system.Alignment = ToolStripItemAlignment.Left;
			this.system.DropDownOpening += this.OnSystemMenuDropDownOpening;
			this.system.ImageScaling = ToolStripItemImageScaling.None;
			this.system.DoubleClickEnabled = true;
			this.system.DoubleClick += this.OnSystemMenuDoubleClick;
			this.system.Padding = Padding.Empty;
			this.system.ShortcutKeys = Keys.LButton | Keys.MButton | Keys.Back | Keys.ShiftKey | Keys.Space | Keys.F17 | Keys.Alt;
			base.ResumeLayout(false);
		}

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x06002F78 RID: 12152 RVA: 0x000D6244 File Offset: 0x000D4444
		public ToolStripMenuItem Close
		{
			get
			{
				return this.close;
			}
		}

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x06002F79 RID: 12153 RVA: 0x000D624C File Offset: 0x000D444C
		// (set) Token: 0x06002F7A RID: 12154 RVA: 0x000D6254 File Offset: 0x000D4454
		internal MenuStrip MergedMenu
		{
			get
			{
				return this.mergedMenu;
			}
			set
			{
				this.mergedMenu = value;
			}
		}

		// Token: 0x06002F7B RID: 12155 RVA: 0x000D6260 File Offset: 0x000D4460
		private Image GetTargetWindowIcon()
		{
			Image image = null;
			IntPtr intPtr = UnsafeNativeMethods.SendMessage(new HandleRef(this, Control.GetSafeHandle(this.target)), 127, 0, 0);
			IntSecurity.ObjectFromWin32Handle.Assert();
			try
			{
				Icon icon = ((intPtr != IntPtr.Zero) ? Icon.FromHandle(intPtr) : Form.DefaultIcon);
				Icon icon2 = new Icon(icon, SystemInformation.SmallIconSize);
				image = icon2.ToBitmap();
				icon2.Dispose();
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return image;
		}

		// Token: 0x06002F7C RID: 12156 RVA: 0x000D62E4 File Offset: 0x000D44E4
		protected internal override void OnItemAdded(ToolStripItemEventArgs e)
		{
			base.OnItemAdded(e);
		}

		// Token: 0x06002F7D RID: 12157 RVA: 0x000D62ED File Offset: 0x000D44ED
		private void OnTargetWindowDisposed(object sender, EventArgs e)
		{
			this.UnhookTarget();
			this.target = null;
		}

		// Token: 0x06002F7E RID: 12158 RVA: 0x000D62FC File Offset: 0x000D44FC
		private void OnTargetWindowHandleRecreated(object sender, EventArgs e)
		{
			this.system.SetNativeTargetWindow(this.target);
			this.minimize.SetNativeTargetWindow(this.target);
			this.close.SetNativeTargetWindow(this.target);
			this.restore.SetNativeTargetWindow(this.target);
			IntPtr systemMenu = UnsafeNativeMethods.GetSystemMenu(new HandleRef(this, Control.GetSafeHandle(this.target)), false);
			this.system.SetNativeTargetMenu(systemMenu);
			this.minimize.SetNativeTargetMenu(systemMenu);
			this.close.SetNativeTargetMenu(systemMenu);
			this.restore.SetNativeTargetMenu(systemMenu);
			if (this.system.HasDropDownItems)
			{
				this.system.DropDown.Items.Clear();
				this.system.DropDown.Dispose();
			}
			this.system.Image = this.GetTargetWindowIcon();
		}

		// Token: 0x06002F7F RID: 12159 RVA: 0x000D63D8 File Offset: 0x000D45D8
		private void OnSystemMenuDropDownOpening(object sender, EventArgs e)
		{
			if (!this.system.HasDropDownItems && this.target != null)
			{
				this.system.DropDown = ToolStripDropDownMenu.FromHMenu(UnsafeNativeMethods.GetSystemMenu(new HandleRef(this, Control.GetSafeHandle(this.target)), false), this.target);
				return;
			}
			if (this.MergedMenu == null)
			{
				this.system.DropDown.Dispose();
			}
		}

		// Token: 0x06002F80 RID: 12160 RVA: 0x000D6440 File Offset: 0x000D4640
		private void OnSystemMenuDoubleClick(object sender, EventArgs e)
		{
			this.Close.PerformClick();
		}

		// Token: 0x06002F81 RID: 12161 RVA: 0x000D644D File Offset: 0x000D464D
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.UnhookTarget();
				this.target = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06002F82 RID: 12162 RVA: 0x000D6468 File Offset: 0x000D4668
		private void UnhookTarget()
		{
			if (this.target != null)
			{
				Control control = this.target as Control;
				if (control != null)
				{
					control.HandleCreated -= this.OnTargetWindowHandleRecreated;
					control.Disposed -= this.OnTargetWindowDisposed;
				}
				this.target = null;
			}
		}

		// Token: 0x0400138B RID: 5003
		private ToolStripMenuItem system;

		// Token: 0x0400138C RID: 5004
		private ToolStripMenuItem close;

		// Token: 0x0400138D RID: 5005
		private ToolStripMenuItem minimize;

		// Token: 0x0400138E RID: 5006
		private ToolStripMenuItem restore;

		// Token: 0x0400138F RID: 5007
		private MenuStrip mergedMenu;

		// Token: 0x04001390 RID: 5008
		private IWin32Window target;

		// Token: 0x020006D1 RID: 1745
		internal class ControlBoxMenuItem : ToolStripMenuItem
		{
			// Token: 0x06006AAF RID: 27311 RVA: 0x0018AE4F File Offset: 0x0018904F
			internal ControlBoxMenuItem(IntPtr hMenu, int nativeMenuCommandId, IWin32Window targetWindow)
				: base(hMenu, nativeMenuCommandId, targetWindow)
			{
			}

			// Token: 0x17001729 RID: 5929
			// (get) Token: 0x06006AB0 RID: 27312 RVA: 0x0001180C File Offset: 0x0000FA0C
			internal override bool CanKeyboardSelect
			{
				get
				{
					return false;
				}
			}
		}

		// Token: 0x020006D2 RID: 1746
		internal class SystemMenuItem : ToolStripMenuItem
		{
			// Token: 0x06006AB1 RID: 27313 RVA: 0x0018AE5A File Offset: 0x0018905A
			public SystemMenuItem()
			{
				if (AccessibilityImprovements.Level1)
				{
					base.AccessibleName = SR.GetString("MDIChildSystemMenuItemAccessibleName");
				}
			}

			// Token: 0x06006AB2 RID: 27314 RVA: 0x0018AE79 File Offset: 0x00189079
			protected internal override bool ProcessCmdKey(ref Message m, Keys keyData)
			{
				if (base.Visible && base.ShortcutKeys == keyData)
				{
					base.ShowDropDown();
					base.DropDown.SelectNextToolStripItem(null, true);
					return true;
				}
				return base.ProcessCmdKey(ref m, keyData);
			}

			// Token: 0x06006AB3 RID: 27315 RVA: 0x0018AEAA File Offset: 0x001890AA
			protected override void OnOwnerChanged(EventArgs e)
			{
				if (this.HasDropDownItems && base.DropDown.Visible)
				{
					base.HideDropDown();
				}
				base.OnOwnerChanged(e);
			}
		}
	}
}
