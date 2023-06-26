using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Provides an empty control that can be used to create other controls.</summary>
	// Token: 0x0200042A RID: 1066
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Designer("System.Windows.Forms.Design.UserControlDocumentDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(IRootDesigner))]
	[Designer("System.Windows.Forms.Design.ControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DesignerCategory("UserControl")]
	[DefaultEvent("Load")]
	public class UserControl : ContainerControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.UserControl" /> class.</summary>
		// Token: 0x060049F1 RID: 18929 RVA: 0x00137266 File Offset: 0x00135466
		public UserControl()
		{
			base.SetScrollState(1, false);
			base.SetState(2, true);
			base.SetState(524288, false);
			base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>
		///   <see langword="true" /> if enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17001222 RID: 4642
		// (get) Token: 0x060049F2 RID: 18930 RVA: 0x00011831 File Offset: 0x0000FA31
		// (set) Token: 0x060049F3 RID: 18931 RVA: 0x00011839 File Offset: 0x0000FA39
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override bool AutoSize
		{
			get
			{
				return base.AutoSize;
			}
			set
			{
				base.AutoSize = value;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.UserControl.AutoSize" /> property changes.</summary>
		// Token: 0x140003B5 RID: 949
		// (add) Token: 0x060049F4 RID: 18932 RVA: 0x00011842 File Offset: 0x0000FA42
		// (remove) Token: 0x060049F5 RID: 18933 RVA: 0x0001184B File Offset: 0x0000FA4B
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public new event EventHandler AutoSizeChanged
		{
			add
			{
				base.AutoSizeChanged += value;
			}
			remove
			{
				base.AutoSizeChanged -= value;
			}
		}

		/// <summary>Gets or sets how the control will resize itself.</summary>
		/// <returns>A value from the <see cref="T:System.Windows.Forms.AutoSizeMode" /> enumeration. The default is <see cref="F:System.Windows.Forms.AutoSizeMode.GrowOnly" />.</returns>
		// Token: 0x17001223 RID: 4643
		// (get) Token: 0x060049F6 RID: 18934 RVA: 0x00023551 File Offset: 0x00021751
		// (set) Token: 0x060049F7 RID: 18935 RVA: 0x00137298 File Offset: 0x00135498
		[SRDescription("ControlAutoSizeModeDescr")]
		[SRCategory("CatLayout")]
		[Browsable(true)]
		[DefaultValue(AutoSizeMode.GrowOnly)]
		[Localizable(true)]
		public AutoSizeMode AutoSizeMode
		{
			get
			{
				return base.GetAutoSizeMode();
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 1))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(AutoSizeMode));
				}
				if (base.GetAutoSizeMode() != value)
				{
					base.SetAutoSizeMode(value);
					Control control = ((base.DesignMode || this.ParentInternal == null) ? this : this.ParentInternal);
					if (control != null)
					{
						if (control.LayoutEngine == DefaultLayout.Instance)
						{
							control.LayoutEngine.InitLayout(this, BoundsSpecified.Size);
						}
						LayoutTransaction.DoLayout(control, this, PropertyNames.AutoSize);
					}
				}
			}
		}

		/// <summary>Gets or sets how the control performs validation when the user changes focus to another control.</summary>
		/// <returns>A member of the <see cref="T:System.Windows.Forms.AutoValidate" /> enumeration. The default value for <see cref="T:System.Windows.Forms.UserControl" /> is <see cref="F:System.Windows.Forms.AutoValidate.EnablePreventFocusChange" />.</returns>
		// Token: 0x17001224 RID: 4644
		// (get) Token: 0x060049F8 RID: 18936 RVA: 0x000B0B5F File Offset: 0x000AED5F
		// (set) Token: 0x060049F9 RID: 18937 RVA: 0x000B0B67 File Offset: 0x000AED67
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public override AutoValidate AutoValidate
		{
			get
			{
				return base.AutoValidate;
			}
			set
			{
				base.AutoValidate = value;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.UserControl.AutoValidate" /> property changes.</summary>
		// Token: 0x140003B6 RID: 950
		// (add) Token: 0x060049FA RID: 18938 RVA: 0x000B0B70 File Offset: 0x000AED70
		// (remove) Token: 0x060049FB RID: 18939 RVA: 0x000B0B79 File Offset: 0x000AED79
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public new event EventHandler AutoValidateChanged
		{
			add
			{
				base.AutoValidateChanged += value;
			}
			remove
			{
				base.AutoValidateChanged -= value;
			}
		}

		/// <summary>Gets or sets the border style of the user control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. The default is <see cref="F:System.Windows.Forms.BorderStyle.Fixed3D" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.BorderStyle" /> values.</exception>
		// Token: 0x17001225 RID: 4645
		// (get) Token: 0x060049FC RID: 18940 RVA: 0x0013731F File Offset: 0x0013551F
		// (set) Token: 0x060049FD RID: 18941 RVA: 0x00137327 File Offset: 0x00135527
		[SRCategory("CatAppearance")]
		[DefaultValue(BorderStyle.None)]
		[SRDescription("UserControlBorderStyleDescr")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public BorderStyle BorderStyle
		{
			get
			{
				return this.borderStyle;
			}
			set
			{
				if (this.borderStyle != value)
				{
					if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(BorderStyle));
					}
					this.borderStyle = value;
					base.UpdateStyles();
				}
			}
		}

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x17001226 RID: 4646
		// (get) Token: 0x060049FE RID: 18942 RVA: 0x00137368 File Offset: 0x00135568
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ExStyle |= 65536;
				createParams.ExStyle &= -513;
				createParams.Style &= -8388609;
				BorderStyle borderStyle = this.borderStyle;
				if (borderStyle != BorderStyle.FixedSingle)
				{
					if (borderStyle == BorderStyle.Fixed3D)
					{
						createParams.ExStyle |= 512;
					}
				}
				else
				{
					createParams.Style |= 8388608;
				}
				return createParams;
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x17001227 RID: 4647
		// (get) Token: 0x060049FF RID: 18943 RVA: 0x001373E8 File Offset: 0x001355E8
		protected override Size DefaultSize
		{
			get
			{
				return new Size(150, 150);
			}
		}

		/// <summary>Occurs before the control becomes visible for the first time.</summary>
		// Token: 0x140003B7 RID: 951
		// (add) Token: 0x06004A00 RID: 18944 RVA: 0x001373F9 File Offset: 0x001355F9
		// (remove) Token: 0x06004A01 RID: 18945 RVA: 0x0013740C File Offset: 0x0013560C
		[SRCategory("CatBehavior")]
		[SRDescription("UserControlOnLoadDescr")]
		public event EventHandler Load
		{
			add
			{
				base.Events.AddHandler(UserControl.EVENT_LOAD, value);
			}
			remove
			{
				base.Events.RemoveHandler(UserControl.EVENT_LOAD, value);
			}
		}

		/// <summary>Gets or sets the text associated with this control.</summary>
		/// <returns>The text associated with this control.</returns>
		// Token: 0x17001228 RID: 4648
		// (get) Token: 0x06004A02 RID: 18946 RVA: 0x00013814 File Offset: 0x00011A14
		// (set) Token: 0x06004A03 RID: 18947 RVA: 0x00023FE9 File Offset: 0x000221E9
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Bindable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.TextChanged" /> event.</summary>
		// Token: 0x140003B8 RID: 952
		// (add) Token: 0x06004A04 RID: 18948 RVA: 0x00046591 File Offset: 0x00044791
		// (remove) Token: 0x06004A05 RID: 18949 RVA: 0x0004659A File Offset: 0x0004479A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TextChanged
		{
			add
			{
				base.TextChanged += value;
			}
			remove
			{
				base.TextChanged -= value;
			}
		}

		/// <summary>Causes all of the child controls within a control that support validation to validate their data.</summary>
		/// <returns>
		///   <see langword="true" /> if all of the children validated successfully; otherwise, <see langword="false" />. If called from the <see cref="E:System.Windows.Forms.Control.Validating" /> or <see cref="E:System.Windows.Forms.Control.Validated" /> event handlers, this method will always return <see langword="false" />.</returns>
		// Token: 0x06004A06 RID: 18950 RVA: 0x000B71D3 File Offset: 0x000B53D3
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public override bool ValidateChildren()
		{
			return base.ValidateChildren();
		}

		/// <summary>Causes all of the child controls within a control that support validation to validate their data.</summary>
		/// <param name="validationConstraints">Places restrictions on which controls have their <see cref="E:System.Windows.Forms.Control.Validating" /> event raised.</param>
		/// <returns>
		///   <see langword="true" /> if all of the children validated successfully; otherwise, <see langword="false" />. If called from the <see cref="E:System.Windows.Forms.Control.Validating" /> or <see cref="E:System.Windows.Forms.Control.Validated" /> event handlers, this method will always return <see langword="false" />.</returns>
		// Token: 0x06004A07 RID: 18951 RVA: 0x000B71DB File Offset: 0x000B53DB
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public override bool ValidateChildren(ValidationConstraints validationConstraints)
		{
			return base.ValidateChildren(validationConstraints);
		}

		// Token: 0x06004A08 RID: 18952 RVA: 0x00137420 File Offset: 0x00135620
		private bool FocusInside()
		{
			if (!base.IsHandleCreated)
			{
				return false;
			}
			IntPtr focus = UnsafeNativeMethods.GetFocus();
			if (focus == IntPtr.Zero)
			{
				return false;
			}
			IntPtr handle = base.Handle;
			return handle == focus || SafeNativeMethods.IsChild(new HandleRef(this, handle), new HandleRef(null, focus));
		}

		/// <summary>Raises the <see langword="CreateControl" /> event.</summary>
		// Token: 0x06004A09 RID: 18953 RVA: 0x00137474 File Offset: 0x00135674
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			this.OnLoad(EventArgs.Empty);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.UserControl.Load" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004A0A RID: 18954 RVA: 0x00137488 File Offset: 0x00135688
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnLoad(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[UserControl.EVENT_LOAD];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004A0B RID: 18955 RVA: 0x001374B6 File Offset: 0x001356B6
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (this.BackgroundImage != null)
			{
				base.Invalidate();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06004A0C RID: 18956 RVA: 0x001374CD File Offset: 0x001356CD
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (!this.FocusInside())
			{
				this.FocusInternal();
			}
			base.OnMouseDown(e);
		}

		// Token: 0x06004A0D RID: 18957 RVA: 0x001374E8 File Offset: 0x001356E8
		private void WmSetFocus(ref Message m)
		{
			if (!base.HostedInWin32DialogManager)
			{
				IntSecurity.ModifyFocus.Assert();
				try
				{
					if (base.ActiveControl == null)
					{
						base.SelectNextControl(null, true, true, true, false);
					}
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
			if (!base.ValidationCancelled)
			{
				base.WndProc(ref m);
			}
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x06004A0E RID: 18958 RVA: 0x00137544 File Offset: 0x00135744
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg == 7)
			{
				this.WmSetFocus(ref m);
				return;
			}
			base.WndProc(ref m);
		}

		// Token: 0x040027BA RID: 10170
		private static readonly object EVENT_LOAD = new object();

		// Token: 0x040027BB RID: 10171
		private BorderStyle borderStyle;
	}
}
