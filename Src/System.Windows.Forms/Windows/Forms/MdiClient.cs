using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	/// <summary>Represents the container for multiple-document interface (MDI) child forms. This class cannot be inherited.</summary>
	// Token: 0x020002E9 RID: 745
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	public sealed class MdiClient : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MdiClient" /> class.</summary>
		// Token: 0x06002F63 RID: 12131 RVA: 0x000D5920 File Offset: 0x000D3B20
		public MdiClient()
		{
			base.SetStyle(ControlStyles.Selectable, false);
			this.BackColor = SystemColors.AppWorkspace;
			this.Dock = DockStyle.Fill;
		}

		/// <summary>Gets or sets the background image displayed in the <see cref="T:System.Windows.Forms.MdiClient" /> control.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> that represents the image to display in the background of the control.</returns>
		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06002F64 RID: 12132 RVA: 0x000D5954 File Offset: 0x000D3B54
		// (set) Token: 0x06002F65 RID: 12133 RVA: 0x00011884 File Offset: 0x0000FA84
		[Localizable(true)]
		public override Image BackgroundImage
		{
			get
			{
				Image image = base.BackgroundImage;
				if (image == null && this.ParentInternal != null)
				{
					image = this.ParentInternal.BackgroundImage;
				}
				return image;
			}
			set
			{
				base.BackgroundImage = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageLayout" /> value.</returns>
		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x06002F66 RID: 12134 RVA: 0x000D5980 File Offset: 0x000D3B80
		// (set) Token: 0x06002F67 RID: 12135 RVA: 0x000118A7 File Offset: 0x0000FAA7
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				Image backgroundImage = this.BackgroundImage;
				if (backgroundImage != null && this.ParentInternal != null)
				{
					ImageLayout backgroundImageLayout = base.BackgroundImageLayout;
					if (backgroundImageLayout != this.ParentInternal.BackgroundImageLayout)
					{
						return this.ParentInternal.BackgroundImageLayout;
					}
				}
				return base.BackgroundImageLayout;
			}
			set
			{
				base.BackgroundImageLayout = value;
			}
		}

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06002F68 RID: 12136 RVA: 0x000D59C8 File Offset: 0x000D3BC8
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ClassName = "MDICLIENT";
				createParams.Style |= 3145728;
				createParams.ExStyle |= 512;
				createParams.Param = new NativeMethods.CLIENTCREATESTRUCT(IntPtr.Zero, 1);
				ISite site = ((this.ParentInternal == null) ? null : this.ParentInternal.Site);
				if (site != null && site.DesignMode)
				{
					createParams.Style |= 134217728;
					base.SetState(4, false);
				}
				if (this.RightToLeft == RightToLeft.Yes && this.ParentInternal != null && this.ParentInternal.IsMirrored)
				{
					createParams.ExStyle |= 5242880;
					createParams.ExStyle &= -28673;
				}
				return createParams;
			}
		}

		/// <summary>Gets the child multiple-document interface (MDI) forms of the <see cref="T:System.Windows.Forms.MdiClient" /> control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Form" /> array that contains the child MDI forms of the <see cref="T:System.Windows.Forms.MdiClient" />.</returns>
		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06002F69 RID: 12137 RVA: 0x000D5A9C File Offset: 0x000D3C9C
		public Form[] MdiChildren
		{
			get
			{
				Form[] array = new Form[this.children.Count];
				this.children.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x06002F6A RID: 12138 RVA: 0x000D5AC8 File Offset: 0x000D3CC8
		protected override Control.ControlCollection CreateControlsInstance()
		{
			return new MdiClient.ControlCollection(this);
		}

		/// <summary>Arranges the multiple-document interface (MDI) child forms within the MDI parent form.</summary>
		/// <param name="value">One of the <see cref="T:System.Windows.Forms.MdiLayout" /> values that defines the layout of MDI child forms.</param>
		// Token: 0x06002F6B RID: 12139 RVA: 0x000D5AD0 File Offset: 0x000D3CD0
		public void LayoutMdi(MdiLayout value)
		{
			if (base.Handle == IntPtr.Zero)
			{
				return;
			}
			switch (value)
			{
			case MdiLayout.Cascade:
				base.SendMessage(551, 0, 0);
				return;
			case MdiLayout.TileHorizontal:
				base.SendMessage(550, 1, 0);
				return;
			case MdiLayout.TileVertical:
				base.SendMessage(550, 0, 0);
				return;
			case MdiLayout.ArrangeIcons:
				base.SendMessage(552, 0, 0);
				return;
			default:
				return;
			}
		}

		// Token: 0x06002F6C RID: 12140 RVA: 0x000D5B44 File Offset: 0x000D3D44
		protected override void OnResize(EventArgs e)
		{
			ISite site = ((this.ParentInternal == null) ? null : this.ParentInternal.Site);
			if (site != null && site.DesignMode && base.Handle != IntPtr.Zero)
			{
				this.SetWindowRgn();
			}
			base.OnResize(e);
		}

		// Token: 0x06002F6D RID: 12141 RVA: 0x000D5B94 File Offset: 0x000D3D94
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void ScaleCore(float dx, float dy)
		{
			base.SuspendLayout();
			try
			{
				Rectangle bounds = base.Bounds;
				int num = (int)Math.Round((double)((float)bounds.X * dx));
				int num2 = (int)Math.Round((double)((float)bounds.Y * dy));
				int num3 = (int)Math.Round((double)((float)(bounds.X + bounds.Width) * dx - (float)num));
				int num4 = (int)Math.Round((double)((float)(bounds.Y + bounds.Height) * dy - (float)num2));
				base.SetBounds(num, num2, num3, num4, BoundsSpecified.All);
			}
			finally
			{
				base.ResumeLayout();
			}
		}

		// Token: 0x06002F6E RID: 12142 RVA: 0x000D5C34 File Offset: 0x000D3E34
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			specified &= ~(BoundsSpecified.X | BoundsSpecified.Y);
			base.ScaleControl(factor, specified);
		}

		// Token: 0x06002F6F RID: 12143 RVA: 0x000D5C44 File Offset: 0x000D3E44
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			ISite site = ((this.ParentInternal == null) ? null : this.ParentInternal.Site);
			if (base.IsHandleCreated && (site == null || !site.DesignMode))
			{
				Rectangle bounds = base.Bounds;
				base.SetBoundsCore(x, y, width, height, specified);
				Rectangle bounds2 = base.Bounds;
				int num = bounds.Height - bounds2.Height;
				if (num != 0)
				{
					NativeMethods.WINDOWPLACEMENT windowplacement = default(NativeMethods.WINDOWPLACEMENT);
					windowplacement.length = Marshal.SizeOf(typeof(NativeMethods.WINDOWPLACEMENT));
					for (int i = 0; i < base.Controls.Count; i++)
					{
						Control control = base.Controls[i];
						if (control != null && control is Form)
						{
							Form form = (Form)control;
							if (form.CanRecreateHandle() && form.WindowState == FormWindowState.Minimized)
							{
								UnsafeNativeMethods.GetWindowPlacement(new HandleRef(form, form.Handle), ref windowplacement);
								windowplacement.ptMinPosition_y -= num;
								if (windowplacement.ptMinPosition_y == -1)
								{
									if (num < 0)
									{
										windowplacement.ptMinPosition_y = 0;
									}
									else
									{
										windowplacement.ptMinPosition_y = -2;
									}
								}
								windowplacement.flags = 1;
								UnsafeNativeMethods.SetWindowPlacement(new HandleRef(form, form.Handle), ref windowplacement);
								windowplacement.flags = 0;
							}
						}
					}
					return;
				}
			}
			else
			{
				base.SetBoundsCore(x, y, width, height, specified);
			}
		}

		// Token: 0x06002F70 RID: 12144 RVA: 0x000D5DA8 File Offset: 0x000D3FA8
		private void SetWindowRgn()
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			CreateParams createParams = this.CreateParams;
			base.AdjustWindowRectEx(ref rect, createParams.Style, false, createParams.ExStyle);
			Rectangle bounds = base.Bounds;
			intPtr = SafeNativeMethods.CreateRectRgn(0, 0, bounds.Width, bounds.Height);
			try
			{
				intPtr2 = SafeNativeMethods.CreateRectRgn(-rect.left, -rect.top, bounds.Width - rect.right, bounds.Height - rect.bottom);
				try
				{
					if (intPtr == IntPtr.Zero || intPtr2 == IntPtr.Zero)
					{
						throw new InvalidOperationException(SR.GetString("ErrorSettingWindowRegion"));
					}
					if (SafeNativeMethods.CombineRgn(new HandleRef(null, intPtr), new HandleRef(null, intPtr), new HandleRef(null, intPtr2), 4) == 0)
					{
						throw new InvalidOperationException(SR.GetString("ErrorSettingWindowRegion"));
					}
					if (UnsafeNativeMethods.SetWindowRgn(new HandleRef(this, base.Handle), new HandleRef(null, intPtr), true) == 0)
					{
						throw new InvalidOperationException(SR.GetString("ErrorSettingWindowRegion"));
					}
					intPtr = IntPtr.Zero;
				}
				finally
				{
					if (intPtr2 != IntPtr.Zero)
					{
						SafeNativeMethods.DeleteObject(new HandleRef(null, intPtr2));
					}
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					SafeNativeMethods.DeleteObject(new HandleRef(null, intPtr));
				}
			}
		}

		// Token: 0x06002F71 RID: 12145 RVA: 0x000D5F10 File Offset: 0x000D4110
		internal override bool ShouldSerializeBackColor()
		{
			return this.BackColor != SystemColors.AppWorkspace;
		}

		// Token: 0x06002F72 RID: 12146 RVA: 0x0001180C File Offset: 0x0000FA0C
		private bool ShouldSerializeLocation()
		{
			return false;
		}

		// Token: 0x06002F73 RID: 12147 RVA: 0x0001180C File Offset: 0x0000FA0C
		internal override bool ShouldSerializeSize()
		{
			return false;
		}

		// Token: 0x06002F74 RID: 12148 RVA: 0x000D5F24 File Offset: 0x000D4124
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg != 1)
			{
				if (msg == 7)
				{
					base.InvokeGotFocus(this.ParentInternal, EventArgs.Empty);
					Form form = null;
					if (this.ParentInternal is Form)
					{
						form = ((Form)this.ParentInternal).ActiveMdiChildInternal;
					}
					if (form == null && this.MdiChildren.Length != 0 && this.MdiChildren[0].IsMdiChildFocusable)
					{
						form = this.MdiChildren[0];
					}
					if (form != null && form.Visible)
					{
						form.Active = true;
					}
					base.WmImeSetFocus();
					this.DefWndProc(ref m);
					base.InvokeGotFocus(this, EventArgs.Empty);
					return;
				}
				if (msg == 8)
				{
					base.InvokeLostFocus(this.ParentInternal, EventArgs.Empty);
				}
			}
			else if (this.ParentInternal != null && this.ParentInternal.Site != null && this.ParentInternal.Site.DesignMode && base.Handle != IntPtr.Zero)
			{
				this.SetWindowRgn();
			}
			base.WndProc(ref m);
		}

		// Token: 0x06002F75 RID: 12149 RVA: 0x000D6035 File Offset: 0x000D4235
		internal override void OnInvokedSetScrollPosition(object sender, EventArgs e)
		{
			Application.Idle += this.OnIdle;
		}

		// Token: 0x06002F76 RID: 12150 RVA: 0x000D6048 File Offset: 0x000D4248
		private void OnIdle(object sender, EventArgs e)
		{
			Application.Idle -= this.OnIdle;
			base.OnInvokedSetScrollPosition(sender, e);
		}

		// Token: 0x0400138A RID: 5002
		private ArrayList children = new ArrayList();

		/// <summary>Contains a collection of <see cref="T:System.Windows.Forms.MdiClient" /> controls.</summary>
		// Token: 0x020006D0 RID: 1744
		[ComVisible(false)]
		public new class ControlCollection : Control.ControlCollection
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MdiClient.ControlCollection" /> class, specifying the owner of the collection.</summary>
			/// <param name="owner">The owner of the collection.</param>
			// Token: 0x06006AAC RID: 27308 RVA: 0x0018ADA3 File Offset: 0x00188FA3
			public ControlCollection(MdiClient owner)
				: base(owner)
			{
				this.owner = owner;
			}

			/// <summary>Adds a control to the multiple-document interface (MDI) Container.</summary>
			/// <param name="value">MDI Child Form to add.</param>
			// Token: 0x06006AAD RID: 27309 RVA: 0x0018ADB4 File Offset: 0x00188FB4
			public override void Add(Control value)
			{
				if (value == null)
				{
					return;
				}
				if (!(value is Form) || !((Form)value).IsMdiChild)
				{
					throw new ArgumentException(SR.GetString("MDIChildAddToNonMDIParent"), "value");
				}
				if (this.owner.CreateThreadId != value.CreateThreadId)
				{
					throw new ArgumentException(SR.GetString("AddDifferentThreads"), "value");
				}
				this.owner.children.Add((Form)value);
				base.Add(value);
			}

			/// <summary>Removes a child control.</summary>
			/// <param name="value">MDI Child Form to remove.</param>
			// Token: 0x06006AAE RID: 27310 RVA: 0x0018AE35 File Offset: 0x00189035
			public override void Remove(Control value)
			{
				this.owner.children.Remove(value);
				base.Remove(value);
			}

			// Token: 0x04003B43 RID: 15171
			private MdiClient owner;
		}
	}
}
