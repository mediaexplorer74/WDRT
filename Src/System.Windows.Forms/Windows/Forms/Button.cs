using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.ButtonInternal;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows button control.</summary>
	// Token: 0x02000141 RID: 321
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[SRDescription("DescriptionButton")]
	[Designer("System.Windows.Forms.Design.ButtonBaseDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class Button : ButtonBase, IButtonControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Button" /> class.</summary>
		// Token: 0x06000C4B RID: 3147 RVA: 0x00023528 File Offset: 0x00021728
		public Button()
		{
			base.SetStyle(ControlStyles.StandardClick | ControlStyles.StandardDoubleClick, false);
		}

		/// <summary>Gets or sets the mode by which the <see cref="T:System.Windows.Forms.Button" /> automatically resizes itself.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AutoSizeMode" /> values. The default value is <see cref="F:System.Windows.Forms.AutoSizeMode.GrowOnly" />.</returns>
		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000C4C RID: 3148 RVA: 0x00023551 File Offset: 0x00021751
		// (set) Token: 0x06000C4D RID: 3149 RVA: 0x0002355C File Offset: 0x0002175C
		[SRCategory("CatLayout")]
		[Browsable(true)]
		[DefaultValue(AutoSizeMode.GrowOnly)]
		[Localizable(true)]
		[SRDescription("ControlAutoSizeModeDescr")]
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
					if (this.ParentInternal != null)
					{
						if (this.ParentInternal.LayoutEngine == DefaultLayout.Instance)
						{
							this.ParentInternal.LayoutEngine.InitLayout(this, BoundsSpecified.Size);
						}
						LayoutTransaction.DoLayout(this.ParentInternal, this, PropertyNames.AutoSize);
					}
				}
			}
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x000235DD File Offset: 0x000217DD
		internal override ButtonBaseAdapter CreateFlatAdapter()
		{
			return new ButtonFlatAdapter(this);
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x000235E5 File Offset: 0x000217E5
		internal override ButtonBaseAdapter CreatePopupAdapter()
		{
			return new ButtonPopupAdapter(this);
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x000235ED File Offset: 0x000217ED
		internal override ButtonBaseAdapter CreateStandardAdapter()
		{
			return new ButtonStandardAdapter(this);
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x000235F8 File Offset: 0x000217F8
		internal override Size GetPreferredSizeCore(Size proposedConstraints)
		{
			if (base.FlatStyle != FlatStyle.System)
			{
				Size preferredSizeCore = base.GetPreferredSizeCore(proposedConstraints);
				if (this.AutoSizeMode != AutoSizeMode.GrowAndShrink)
				{
					return LayoutUtils.UnionSizes(preferredSizeCore, base.Size);
				}
				return preferredSizeCore;
			}
			else
			{
				if (this.systemSize.Width == -2147483648)
				{
					Size size = TextRenderer.MeasureText(this.Text, this.Font);
					size = this.SizeFromClientSize(size);
					size.Width += 14;
					size.Height += 9;
					this.systemSize = size;
				}
				Size size2 = this.systemSize + base.Padding.Size;
				if (this.AutoSizeMode != AutoSizeMode.GrowAndShrink)
				{
					return LayoutUtils.UnionSizes(size2, base.Size);
				}
				return size2;
			}
		}

		/// <summary>Gets a <see cref="T:System.Windows.Forms.CreateParams" /> on the base class when creating a window.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> object on the base class when creating a window.</returns>
		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000C52 RID: 3154 RVA: 0x000236B0 File Offset: 0x000218B0
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ClassName = "BUTTON";
				if (base.GetStyle(ControlStyles.UserPaint))
				{
					createParams.Style |= 11;
				}
				else
				{
					createParams.Style |= 0;
					if (base.IsDefault)
					{
						createParams.Style |= 1;
					}
				}
				return createParams;
			}
		}

		/// <summary>Gets or sets a value that is returned to the parent form when the button is clicked.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values. The default value is <see langword="None" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</exception>
		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x0002370E File Offset: 0x0002190E
		// (set) Token: 0x06000C54 RID: 3156 RVA: 0x00023716 File Offset: 0x00021916
		[SRCategory("CatBehavior")]
		[DefaultValue(DialogResult.None)]
		[SRDescription("ButtonDialogResultDescr")]
		public virtual DialogResult DialogResult
		{
			get
			{
				return this.dialogResult;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 7))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DialogResult));
				}
				this.dialogResult = value;
			}
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnMouseEnter(System.EventArgs)" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000C55 RID: 3157 RVA: 0x00023745 File Offset: 0x00021945
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnMouseLeave(System.EventArgs)" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000C56 RID: 3158 RVA: 0x0002374E File Offset: 0x0002194E
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
		}

		/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.Button" /> control.</summary>
		// Token: 0x14000062 RID: 98
		// (add) Token: 0x06000C57 RID: 3159 RVA: 0x00023757 File Offset: 0x00021957
		// (remove) Token: 0x06000C58 RID: 3160 RVA: 0x00023760 File Offset: 0x00021960
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event EventHandler DoubleClick
		{
			add
			{
				base.DoubleClick += value;
			}
			remove
			{
				base.DoubleClick -= value;
			}
		}

		/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.Button" /> control with the mouse.</summary>
		// Token: 0x14000063 RID: 99
		// (add) Token: 0x06000C59 RID: 3161 RVA: 0x00023769 File Offset: 0x00021969
		// (remove) Token: 0x06000C5A RID: 3162 RVA: 0x00023772 File Offset: 0x00021972
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event MouseEventHandler MouseDoubleClick
		{
			add
			{
				base.MouseDoubleClick += value;
			}
			remove
			{
				base.MouseDoubleClick -= value;
			}
		}

		/// <summary>Notifies the <see cref="T:System.Windows.Forms.Button" /> whether it is the default button so that it can adjust its appearance accordingly.</summary>
		/// <param name="value">
		///   <see langword="true" /> if the button is to have the appearance of the default button; otherwise, <see langword="false" />.</param>
		// Token: 0x06000C5B RID: 3163 RVA: 0x0002377B File Offset: 0x0002197B
		public virtual void NotifyDefault(bool value)
		{
			if (base.IsDefault != value)
			{
				base.IsDefault = value;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000C5C RID: 3164 RVA: 0x00023790 File Offset: 0x00021990
		protected override void OnClick(EventArgs e)
		{
			Form form = base.FindFormInternal();
			if (form != null)
			{
				form.DialogResult = this.dialogResult;
			}
			base.AccessibilityNotifyClients(AccessibleEvents.StateChange, -1);
			base.AccessibilityNotifyClients(AccessibleEvents.NameChange, -1);
			base.OnClick(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000C5D RID: 3165 RVA: 0x000237D2 File Offset: 0x000219D2
		protected override void OnFontChanged(EventArgs e)
		{
			this.systemSize = new Size(int.MinValue, int.MinValue);
			base.OnFontChanged(e);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnMouseUp(System.Windows.Forms.MouseEventArgs)" /> event.</summary>
		/// <param name="mevent">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06000C5E RID: 3166 RVA: 0x000237F0 File Offset: 0x000219F0
		protected override void OnMouseUp(MouseEventArgs mevent)
		{
			if (mevent.Button == MouseButtons.Left && base.MouseIsPressed)
			{
				bool mouseIsDown = base.MouseIsDown;
				if (base.GetStyle(ControlStyles.UserPaint))
				{
					base.ResetFlagsandPaint();
				}
				if (mouseIsDown)
				{
					Point point = base.PointToScreen(new Point(mevent.X, mevent.Y));
					if (UnsafeNativeMethods.WindowFromPoint(point.X, point.Y) == base.Handle && !base.ValidationCancelled)
					{
						if (base.GetStyle(ControlStyles.UserPaint))
						{
							this.OnClick(mevent);
						}
						this.OnMouseClick(mevent);
					}
				}
			}
			base.OnMouseUp(mevent);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.TextChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000C5F RID: 3167 RVA: 0x00023889 File Offset: 0x00021A89
		protected override void OnTextChanged(EventArgs e)
		{
			this.systemSize = new Size(int.MinValue, int.MinValue);
			base.OnTextChanged(e);
		}

		/// <summary>Provides constants for rescaling the <see cref="T:System.Windows.Forms.Button" /> control when a DPI change occurs.</summary>
		/// <param name="deviceDpiOld">The DPI value prior to the change.</param>
		/// <param name="deviceDpiNew">The DPI value after the change.</param>
		// Token: 0x06000C60 RID: 3168 RVA: 0x000238A7 File Offset: 0x00021AA7
		protected override void RescaleConstantsForDpi(int deviceDpiOld, int deviceDpiNew)
		{
			base.RescaleConstantsForDpi(deviceDpiOld, deviceDpiNew);
			if (DpiHelper.EnableDpiChangedHighDpiImprovements)
			{
				this.systemSize = new Size(int.MinValue, int.MinValue);
			}
		}

		/// <summary>Generates a <see cref="E:System.Windows.Forms.Control.Click" /> event for a button.</summary>
		// Token: 0x06000C61 RID: 3169 RVA: 0x000238D0 File Offset: 0x00021AD0
		public void PerformClick()
		{
			if (base.CanSelect)
			{
				bool flag2;
				bool flag = base.ValidateActiveControl(out flag2);
				if (!base.ValidationCancelled && (flag || flag2))
				{
					base.ResetFlagsandPaint();
					this.OnClick(EventArgs.Empty);
				}
			}
		}

		/// <summary>Processes a mnemonic character.</summary>
		/// <param name="charCode">The mnemonic character entered.</param>
		/// <returns>
		///   <see langword="true" /> if the mnemonic was processed; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000C62 RID: 3170 RVA: 0x0002390C File Offset: 0x00021B0C
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal override bool ProcessMnemonic(char charCode)
		{
			if (base.UseMnemonic && this.CanProcessMnemonic() && Control.IsMnemonic(charCode, this.Text))
			{
				this.PerformClick();
				return true;
			}
			return base.ProcessMnemonic(charCode);
		}

		/// <summary>Returns a <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any. This method should not be overridden.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any, or <see langword="null" /> if the <see cref="T:System.ComponentModel.Component" /> is unnamed.</returns>
		// Token: 0x06000C63 RID: 3171 RVA: 0x0002393C File Offset: 0x00021B3C
		public override string ToString()
		{
			string text = base.ToString();
			return text + ", Text: " + this.Text;
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x06000C64 RID: 3172 RVA: 0x00023964 File Offset: 0x00021B64
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg != 20)
			{
				if (msg == 8465)
				{
					if (NativeMethods.Util.HIWORD(m.WParam) == 0 && !base.ValidationCancelled)
					{
						this.OnClick(EventArgs.Empty);
						return;
					}
				}
				else
				{
					base.WndProc(ref m);
				}
				return;
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x0400071F RID: 1823
		private DialogResult dialogResult;

		// Token: 0x04000720 RID: 1824
		private const int InvalidDimensionValue = -2147483648;

		// Token: 0x04000721 RID: 1825
		private Size systemSize = new Size(int.MinValue, int.MinValue);
	}
}
