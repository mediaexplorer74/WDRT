using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms.Internal;
using System.Windows.Forms.Layout;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows control that displays a frame around a group of controls with an optional caption.</summary>
	// Token: 0x0200026B RID: 619
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultEvent("Enter")]
	[DefaultProperty("Text")]
	[Designer("System.Windows.Forms.Design.GroupBoxDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionGroupBox")]
	public class GroupBox : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.GroupBox" /> class.</summary>
		// Token: 0x060027AA RID: 10154 RVA: 0x000B8DE4 File Offset: 0x000B6FE4
		public GroupBox()
		{
			base.SetState2(2048, true);
			base.SetStyle(ControlStyles.ContainerControl, true);
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, this.OwnerDraw);
			base.SetStyle(ControlStyles.Selectable, false);
			this.TabStop = false;
		}

		/// <summary>Gets or sets a value that indicates whether the control will allow drag-and-drop operations and events to be used.</summary>
		/// <returns>
		///   <see langword="true" /> to allow drag-and-drop operations and events to be used; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x060027AB RID: 10155 RVA: 0x000B8E3D File Offset: 0x000B703D
		// (set) Token: 0x060027AC RID: 10156 RVA: 0x000B8E45 File Offset: 0x000B7045
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public override bool AllowDrop
		{
			get
			{
				return base.AllowDrop;
			}
			set
			{
				base.AllowDrop = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Windows.Forms.GroupBox" /> resizes based on its contents.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.GroupBox" /> automatically resizes based on its contents; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x060027AD RID: 10157 RVA: 0x00011831 File Offset: 0x0000FA31
		// (set) Token: 0x060027AE RID: 10158 RVA: 0x00011839 File Offset: 0x0000FA39
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.GroupBox.AutoSize" /> property changes.</summary>
		// Token: 0x140001B7 RID: 439
		// (add) Token: 0x060027AF RID: 10159 RVA: 0x00011842 File Offset: 0x0000FA42
		// (remove) Token: 0x060027B0 RID: 10160 RVA: 0x0001184B File Offset: 0x0000FA4B
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnAutoSizeChangedDescr")]
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

		/// <summary>Gets or sets how the <see cref="T:System.Windows.Forms.GroupBox" /> behaves when its <see cref="P:System.Windows.Forms.Control.AutoSize" /> property is enabled.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AutoSizeMode" /> values. The default is <see cref="F:System.Windows.Forms.AutoSizeMode.GrowOnly" />.</returns>
		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x060027B1 RID: 10161 RVA: 0x00023551 File Offset: 0x00021751
		// (set) Token: 0x060027B2 RID: 10162 RVA: 0x000B8E50 File Offset: 0x000B7050
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

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x060027B3 RID: 10163 RVA: 0x000B8ED4 File Offset: 0x000B70D4
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				if (!this.OwnerDraw)
				{
					createParams.ClassName = "BUTTON";
					createParams.Style |= 7;
				}
				else
				{
					createParams.ClassName = null;
					createParams.Style &= -8;
				}
				createParams.ExStyle |= 65536;
				return createParams;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.Padding" /> structure that contains the default padding settings for a <see cref="T:System.Windows.Forms.GroupBox" /> control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> with all its edges set to three pixels.</returns>
		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x060027B4 RID: 10164 RVA: 0x000B8F34 File Offset: 0x000B7134
		protected override Padding DefaultPadding
		{
			get
			{
				return new Padding(3);
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x060027B5 RID: 10165 RVA: 0x000B8F3C File Offset: 0x000B713C
		protected override Size DefaultSize
		{
			get
			{
				return new Size(200, 100);
			}
		}

		/// <summary>Gets a rectangle that represents the dimensions of the <see cref="T:System.Windows.Forms.GroupBox" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> with the dimensions of the <see cref="T:System.Windows.Forms.GroupBox" />.</returns>
		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x060027B6 RID: 10166 RVA: 0x000B8F4C File Offset: 0x000B714C
		public override Rectangle DisplayRectangle
		{
			get
			{
				Size clientSize = base.ClientSize;
				if (this.fontHeight == -1)
				{
					this.fontHeight = this.Font.Height;
					this.cachedFont = this.Font;
				}
				else if (this.cachedFont != this.Font)
				{
					this.fontHeight = this.Font.Height;
					this.cachedFont = this.Font;
				}
				Padding padding = base.Padding;
				return new Rectangle(padding.Left, this.fontHeight + padding.Top, Math.Max(clientSize.Width - padding.Horizontal, 0), Math.Max(clientSize.Height - this.fontHeight - padding.Vertical, 0));
			}
		}

		/// <summary>Gets or sets the flat style appearance of the group box control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.FlatStyle" /> values. The default value is <see cref="F:System.Windows.Forms.FlatStyle.Standard" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.FlatStyle" /> values.</exception>
		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x060027B7 RID: 10167 RVA: 0x000B9005 File Offset: 0x000B7205
		// (set) Token: 0x060027B8 RID: 10168 RVA: 0x000B9010 File Offset: 0x000B7210
		[SRCategory("CatAppearance")]
		[DefaultValue(FlatStyle.Standard)]
		[SRDescription("ButtonFlatStyleDescr")]
		public FlatStyle FlatStyle
		{
			get
			{
				return this.flatStyle;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(FlatStyle));
				}
				if (this.flatStyle != value)
				{
					bool ownerDraw = this.OwnerDraw;
					this.flatStyle = value;
					bool flag = this.OwnerDraw != ownerDraw;
					base.SetStyle(ControlStyles.ContainerControl, true);
					base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.UserMouse | ControlStyles.SupportsTransparentBackColor, this.OwnerDraw);
					if (flag)
					{
						base.RecreateHandle();
						return;
					}
					this.Refresh();
				}
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x060027B9 RID: 10169 RVA: 0x000B9090 File Offset: 0x000B7290
		private bool OwnerDraw
		{
			get
			{
				return this.FlatStyle != FlatStyle.System;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the user can press the TAB key to give the focus to the <see cref="T:System.Windows.Forms.GroupBox" />.</summary>
		/// <returns>
		///   <see langword="true" /> to allow the user to press the TAB key to give the focus to the <see cref="T:System.Windows.Forms.GroupBox" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x060027BA RID: 10170 RVA: 0x000B2395 File Offset: 0x000B0595
		// (set) Token: 0x060027BB RID: 10171 RVA: 0x000B239D File Offset: 0x000B059D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new bool TabStop
		{
			get
			{
				return base.TabStop;
			}
			set
			{
				base.TabStop = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.GroupBox.TabStop" /> property changes.</summary>
		// Token: 0x140001B8 RID: 440
		// (add) Token: 0x060027BC RID: 10172 RVA: 0x000B23A6 File Offset: 0x000B05A6
		// (remove) Token: 0x060027BD RID: 10173 RVA: 0x000B23AF File Offset: 0x000B05AF
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event EventHandler TabStopChanged
		{
			add
			{
				base.TabStopChanged += value;
			}
			remove
			{
				base.TabStopChanged -= value;
			}
		}

		/// <summary>Gets or sets the text associated with this control.</summary>
		/// <returns>The text associated with this control.</returns>
		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x060027BE RID: 10174 RVA: 0x00013814 File Offset: 0x00011A14
		// (set) Token: 0x060027BF RID: 10175 RVA: 0x000B90A0 File Offset: 0x000B72A0
		[Localizable(true)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				bool visible = base.Visible;
				try
				{
					if (visible && base.IsHandleCreated)
					{
						base.SendMessage(11, 0, 0);
					}
					base.Text = value;
				}
				finally
				{
					if (visible && base.IsHandleCreated)
					{
						base.SendMessage(11, 1, 0);
					}
				}
				base.Invalidate(true);
			}
		}

		/// <summary>Gets or sets a value that determines whether to use the <see cref="T:System.Drawing.Graphics" /> class (GDI+) or the <see cref="T:System.Windows.Forms.TextRenderer" /> class (GDI) to render text</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Drawing.Graphics" /> class should be used to perform text rendering for compatibility with versions 1.0 and 1.1. of the .NET Framework; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x060027C0 RID: 10176 RVA: 0x00024807 File Offset: 0x00022A07
		// (set) Token: 0x060027C1 RID: 10177 RVA: 0x0002480F File Offset: 0x00022A0F
		[DefaultValue(false)]
		[SRCategory("CatBehavior")]
		[SRDescription("UseCompatibleTextRenderingDescr")]
		public bool UseCompatibleTextRendering
		{
			get
			{
				return base.UseCompatibleTextRenderingInt;
			}
			set
			{
				base.UseCompatibleTextRenderingInt = value;
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x060027C2 RID: 10178 RVA: 0x00012E4E File Offset: 0x0001104E
		internal override bool SupportsUseCompatibleTextRendering
		{
			get
			{
				return true;
			}
		}

		/// <summary>Occurs when the user clicks the <see cref="T:System.Windows.Forms.GroupBox" /> control.</summary>
		// Token: 0x140001B9 RID: 441
		// (add) Token: 0x060027C3 RID: 10179 RVA: 0x00012FD4 File Offset: 0x000111D4
		// (remove) Token: 0x060027C4 RID: 10180 RVA: 0x00012FDD File Offset: 0x000111DD
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event EventHandler Click
		{
			add
			{
				base.Click += value;
			}
			remove
			{
				base.Click -= value;
			}
		}

		/// <summary>Occurs when the user clicks the <see cref="T:System.Windows.Forms.GroupBox" /> control with the mouse.</summary>
		// Token: 0x140001BA RID: 442
		// (add) Token: 0x060027C5 RID: 10181 RVA: 0x00012FE6 File Offset: 0x000111E6
		// (remove) Token: 0x060027C6 RID: 10182 RVA: 0x00012FEF File Offset: 0x000111EF
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event MouseEventHandler MouseClick
		{
			add
			{
				base.MouseClick += value;
			}
			remove
			{
				base.MouseClick -= value;
			}
		}

		/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.GroupBox" /> control.</summary>
		// Token: 0x140001BB RID: 443
		// (add) Token: 0x060027C7 RID: 10183 RVA: 0x00023757 File Offset: 0x00021957
		// (remove) Token: 0x060027C8 RID: 10184 RVA: 0x00023760 File Offset: 0x00021960
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

		/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.GroupBox" /> control with the mouse.</summary>
		// Token: 0x140001BC RID: 444
		// (add) Token: 0x060027C9 RID: 10185 RVA: 0x00023769 File Offset: 0x00021969
		// (remove) Token: 0x060027CA RID: 10186 RVA: 0x00023772 File Offset: 0x00021972
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

		/// <summary>Occurs when the user releases a key while the <see cref="T:System.Windows.Forms.GroupBox" /> control has focus.</summary>
		// Token: 0x140001BD RID: 445
		// (add) Token: 0x060027CB RID: 10187 RVA: 0x000B9104 File Offset: 0x000B7304
		// (remove) Token: 0x060027CC RID: 10188 RVA: 0x000B910D File Offset: 0x000B730D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event KeyEventHandler KeyUp
		{
			add
			{
				base.KeyUp += value;
			}
			remove
			{
				base.KeyUp -= value;
			}
		}

		/// <summary>Occurs when the user presses a key while the <see cref="T:System.Windows.Forms.GroupBox" /> control has focus.</summary>
		// Token: 0x140001BE RID: 446
		// (add) Token: 0x060027CD RID: 10189 RVA: 0x000B9116 File Offset: 0x000B7316
		// (remove) Token: 0x060027CE RID: 10190 RVA: 0x000B911F File Offset: 0x000B731F
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event KeyEventHandler KeyDown
		{
			add
			{
				base.KeyDown += value;
			}
			remove
			{
				base.KeyDown -= value;
			}
		}

		/// <summary>Occurs when the user presses a key while the <see cref="T:System.Windows.Forms.GroupBox" /> control has focus.</summary>
		// Token: 0x140001BF RID: 447
		// (add) Token: 0x060027CF RID: 10191 RVA: 0x000B9128 File Offset: 0x000B7328
		// (remove) Token: 0x060027D0 RID: 10192 RVA: 0x000B9131 File Offset: 0x000B7331
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event KeyPressEventHandler KeyPress
		{
			add
			{
				base.KeyPress += value;
			}
			remove
			{
				base.KeyPress -= value;
			}
		}

		/// <summary>Occurs when the user presses a mouse button while the mouse pointer is over the control.</summary>
		// Token: 0x140001C0 RID: 448
		// (add) Token: 0x060027D1 RID: 10193 RVA: 0x000B913A File Offset: 0x000B733A
		// (remove) Token: 0x060027D2 RID: 10194 RVA: 0x000B9143 File Offset: 0x000B7343
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event MouseEventHandler MouseDown
		{
			add
			{
				base.MouseDown += value;
			}
			remove
			{
				base.MouseDown -= value;
			}
		}

		/// <summary>Occurs when the user releases a mouse button while the mouse pointer is over the control.</summary>
		// Token: 0x140001C1 RID: 449
		// (add) Token: 0x060027D3 RID: 10195 RVA: 0x000B914C File Offset: 0x000B734C
		// (remove) Token: 0x060027D4 RID: 10196 RVA: 0x000B9155 File Offset: 0x000B7355
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event MouseEventHandler MouseUp
		{
			add
			{
				base.MouseUp += value;
			}
			remove
			{
				base.MouseUp -= value;
			}
		}

		/// <summary>Occurs when the user moves the mouse pointer over the control.</summary>
		// Token: 0x140001C2 RID: 450
		// (add) Token: 0x060027D5 RID: 10197 RVA: 0x00011A7E File Offset: 0x0000FC7E
		// (remove) Token: 0x060027D6 RID: 10198 RVA: 0x00011A87 File Offset: 0x0000FC87
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event MouseEventHandler MouseMove
		{
			add
			{
				base.MouseMove += value;
			}
			remove
			{
				base.MouseMove -= value;
			}
		}

		/// <summary>Occurs when the mouse pointer enters the control.</summary>
		// Token: 0x140001C3 RID: 451
		// (add) Token: 0x060027D7 RID: 10199 RVA: 0x00011A48 File Offset: 0x0000FC48
		// (remove) Token: 0x060027D8 RID: 10200 RVA: 0x00011A51 File Offset: 0x0000FC51
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event EventHandler MouseEnter
		{
			add
			{
				base.MouseEnter += value;
			}
			remove
			{
				base.MouseEnter -= value;
			}
		}

		/// <summary>Occurs when the mouse pointer leaves the control.</summary>
		// Token: 0x140001C4 RID: 452
		// (add) Token: 0x060027D9 RID: 10201 RVA: 0x00011A5A File Offset: 0x0000FC5A
		// (remove) Token: 0x060027DA RID: 10202 RVA: 0x00011A63 File Offset: 0x0000FC63
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new event EventHandler MouseLeave
		{
			add
			{
				base.MouseLeave += value;
			}
			remove
			{
				base.MouseLeave -= value;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		// Token: 0x060027DB RID: 10203 RVA: 0x000B9160 File Offset: 0x000B7360
		protected override void OnPaint(PaintEventArgs e)
		{
			if (Application.RenderWithVisualStyles && base.Width >= 10 && base.Height >= 10)
			{
				GroupBoxState groupBoxState = (base.Enabled ? GroupBoxState.Normal : GroupBoxState.Disabled);
				TextFormatFlags textFormatFlags = TextFormatFlags.TextBoxControl | TextFormatFlags.WordBreak | TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.PreserveGraphicsTranslateTransform;
				if (!this.ShowKeyboardCues)
				{
					textFormatFlags |= TextFormatFlags.HidePrefix;
				}
				if (this.RightToLeft == RightToLeft.Yes)
				{
					textFormatFlags |= TextFormatFlags.Right | TextFormatFlags.RightToLeft;
				}
				if (this.ShouldSerializeForeColor() || !base.Enabled)
				{
					Color color = (base.Enabled ? this.ForeColor : TextRenderer.DisabledTextColor(this.BackColor));
					GroupBoxRenderer.DrawGroupBox(e.Graphics, new Rectangle(0, 0, base.Width, base.Height), this.Text, this.Font, color, textFormatFlags, groupBoxState);
				}
				else
				{
					GroupBoxRenderer.DrawGroupBox(e.Graphics, new Rectangle(0, 0, base.Width, base.Height), this.Text, this.Font, textFormatFlags, groupBoxState);
				}
			}
			else
			{
				this.DrawGroupBox(e);
			}
			base.OnPaint(e);
		}

		// Token: 0x060027DC RID: 10204 RVA: 0x000B925C File Offset: 0x000B745C
		private void DrawGroupBox(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			Rectangle clientRectangle = base.ClientRectangle;
			int num = 8;
			Color disabledColor = base.DisabledColor;
			Pen pen = new Pen(ControlPaint.Light(disabledColor, 1f));
			Pen pen2 = new Pen(ControlPaint.Dark(disabledColor, 0f));
			clientRectangle.X += num;
			clientRectangle.Width -= 2 * num;
			try
			{
				Size size;
				if (this.UseCompatibleTextRendering)
				{
					using (Brush brush = new SolidBrush(this.ForeColor))
					{
						using (StringFormat stringFormat = new StringFormat())
						{
							stringFormat.HotkeyPrefix = (this.ShowKeyboardCues ? HotkeyPrefix.Show : HotkeyPrefix.Hide);
							if (this.RightToLeft == RightToLeft.Yes)
							{
								stringFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
							}
							size = Size.Ceiling(graphics.MeasureString(this.Text, this.Font, clientRectangle.Width, stringFormat));
							if (base.Enabled)
							{
								graphics.DrawString(this.Text, this.Font, brush, clientRectangle, stringFormat);
								goto IL_1E6;
							}
							ControlPaint.DrawStringDisabled(graphics, this.Text, this.Font, disabledColor, clientRectangle, stringFormat);
							goto IL_1E6;
						}
					}
				}
				using (WindowsGraphics windowsGraphics = WindowsGraphics.FromGraphics(graphics))
				{
					IntTextFormatFlags intTextFormatFlags = IntTextFormatFlags.TextBoxControl | IntTextFormatFlags.WordBreak;
					if (!this.ShowKeyboardCues)
					{
						intTextFormatFlags |= IntTextFormatFlags.HidePrefix;
					}
					if (this.RightToLeft == RightToLeft.Yes)
					{
						intTextFormatFlags |= IntTextFormatFlags.RightToLeft;
						intTextFormatFlags |= IntTextFormatFlags.Right;
					}
					using (WindowsFont windowsFont = WindowsGraphicsCacheManager.GetWindowsFont(this.Font))
					{
						size = windowsGraphics.MeasureText(this.Text, windowsFont, new Size(clientRectangle.Width, int.MaxValue), intTextFormatFlags);
						if (base.Enabled)
						{
							windowsGraphics.DrawText(this.Text, windowsFont, clientRectangle, this.ForeColor, intTextFormatFlags);
						}
						else
						{
							ControlPaint.DrawStringDisabled(windowsGraphics, this.Text, this.Font, disabledColor, clientRectangle, (TextFormatFlags)intTextFormatFlags);
						}
					}
				}
				IL_1E6:
				int num2 = num;
				if (this.RightToLeft == RightToLeft.Yes)
				{
					num2 += clientRectangle.Width - size.Width;
				}
				int num3 = Math.Min(num2 + size.Width, base.Width - 6);
				int num4 = base.FontHeight / 2;
				if (SystemInformation.HighContrast && AccessibilityImprovements.Level1)
				{
					Color color;
					if (base.Enabled)
					{
						color = this.ForeColor;
					}
					else
					{
						color = SystemColors.GrayText;
					}
					bool flag = !color.IsSystemColor;
					Pen pen3 = null;
					try
					{
						if (flag)
						{
							pen3 = new Pen(color);
						}
						else
						{
							pen3 = SystemPens.FromSystemColor(color);
						}
						graphics.DrawLine(pen3, 0, num4, 0, base.Height);
						graphics.DrawLine(pen3, 0, base.Height - 1, base.Width, base.Height - 1);
						graphics.DrawLine(pen3, 0, num4, num2, num4);
						graphics.DrawLine(pen3, num3, num4, base.Width - 1, num4);
						graphics.DrawLine(pen3, base.Width - 1, num4, base.Width - 1, base.Height - 1);
						return;
					}
					finally
					{
						if (flag && pen3 != null)
						{
							pen3.Dispose();
						}
					}
				}
				graphics.DrawLine(pen, 1, num4, 1, base.Height - 1);
				graphics.DrawLine(pen2, 0, num4, 0, base.Height - 2);
				graphics.DrawLine(pen, 0, base.Height - 1, base.Width, base.Height - 1);
				graphics.DrawLine(pen2, 0, base.Height - 2, base.Width - 1, base.Height - 2);
				graphics.DrawLine(pen2, 0, num4 - 1, num2, num4 - 1);
				graphics.DrawLine(pen, 1, num4, num2, num4);
				graphics.DrawLine(pen2, num3, num4 - 1, base.Width - 2, num4 - 1);
				graphics.DrawLine(pen, num3, num4, base.Width - 1, num4);
				graphics.DrawLine(pen, base.Width - 1, num4 - 1, base.Width - 1, base.Height - 1);
				graphics.DrawLine(pen2, base.Width - 2, num4, base.Width - 2, base.Height - 2);
			}
			finally
			{
				pen.Dispose();
				pen2.Dispose();
			}
		}

		// Token: 0x060027DD RID: 10205 RVA: 0x000B9718 File Offset: 0x000B7918
		internal override Size GetPreferredSizeCore(Size proposedSize)
		{
			Size size = this.SizeFromClientSize(Size.Empty);
			Size size2 = size + new Size(0, this.fontHeight) + base.Padding.Size;
			Size preferredSize = this.LayoutEngine.GetPreferredSize(this, proposedSize - size2);
			return preferredSize + size2;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060027DE RID: 10206 RVA: 0x000B9772 File Offset: 0x000B7972
		protected override void OnFontChanged(EventArgs e)
		{
			this.fontHeight = -1;
			this.cachedFont = null;
			base.Invalidate();
			base.OnFontChanged(e);
		}

		/// <summary>Processes a mnemonic character.</summary>
		/// <param name="charCode">The character to process.</param>
		/// <returns>
		///   <see langword="true" /> if the character was processed as a mnemonic by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x060027DF RID: 10207 RVA: 0x000B9790 File Offset: 0x000B7990
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal override bool ProcessMnemonic(char charCode)
		{
			if (Control.IsMnemonic(charCode, this.Text) && this.CanProcessMnemonic())
			{
				IntSecurity.ModifyFocus.Assert();
				try
				{
					base.SelectNextControl(null, true, true, true, false);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				return true;
			}
			return false;
		}

		/// <summary>Scales the <see cref="T:System.Windows.Forms.GroupBox" /> by the specified factor and scaling instruction.</summary>
		/// <param name="factor">The <see cref="T:System.Drawing.SizeF" /> that indicates the height and width of the scaled control.</param>
		/// <param name="specified">One of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values that indicates how the control should be scaled.</param>
		// Token: 0x060027E0 RID: 10208 RVA: 0x000B97E4 File Offset: 0x000B79E4
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			if (factor.Width != 1f && factor.Height != 1f)
			{
				this.fontHeight = -1;
				this.cachedFont = null;
			}
			base.ScaleControl(factor, specified);
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x060027E1 RID: 10209 RVA: 0x00028BBB File Offset: 0x00026DBB
		internal override bool SupportsUiaProviders
		{
			get
			{
				return AccessibilityImprovements.Level3 && !base.DesignMode;
			}
		}

		/// <summary>Returns a <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any. This method should not be overridden.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any, or <see langword="null" /> if the <see cref="T:System.ComponentModel.Component" /> is unnamed.</returns>
		// Token: 0x060027E2 RID: 10210 RVA: 0x000B9818 File Offset: 0x000B7A18
		public override string ToString()
		{
			string text = base.ToString();
			return text + ", Text: " + this.Text;
		}

		// Token: 0x060027E3 RID: 10211 RVA: 0x000B9840 File Offset: 0x000B7A40
		private void WmEraseBkgnd(ref Message m)
		{
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			SafeNativeMethods.GetClientRect(new HandleRef(this, base.Handle), ref rect);
			using (Graphics graphics = Graphics.FromHdcInternal(m.WParam))
			{
				using (Brush brush = new SolidBrush(this.BackColor))
				{
					graphics.FillRectangle(brush, rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);
				}
			}
			m.Result = (IntPtr)1;
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x060027E4 RID: 10212 RVA: 0x000B98EC File Offset: 0x000B7AEC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			if (this.OwnerDraw)
			{
				base.WndProc(ref m);
				return;
			}
			int msg = m.Msg;
			if (msg != 20)
			{
				if (msg != 61)
				{
					if (msg == 792)
					{
						goto IL_29;
					}
					base.WndProc(ref m);
				}
				else
				{
					base.WndProc(ref m);
					if ((int)(long)m.LParam == -12)
					{
						m.Result = IntPtr.Zero;
						return;
					}
				}
				return;
			}
			IL_29:
			this.WmEraseBkgnd(ref m);
		}

		/// <summary>Creates a new accessibility object for the <see cref="T:System.Windows.Forms.GroupBox" />.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the <see cref="T:System.Windows.Forms.GroupBox" />.</returns>
		// Token: 0x060027E5 RID: 10213 RVA: 0x000B9954 File Offset: 0x000B7B54
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new GroupBox.GroupBoxAccessibleObject(this);
		}

		// Token: 0x04001052 RID: 4178
		private int fontHeight = -1;

		// Token: 0x04001053 RID: 4179
		private Font cachedFont;

		// Token: 0x04001054 RID: 4180
		private FlatStyle flatStyle = FlatStyle.Standard;

		// Token: 0x020006A2 RID: 1698
		[ComVisible(true)]
		internal class GroupBoxAccessibleObject : Control.ControlAccessibleObject
		{
			// Token: 0x060067C9 RID: 26569 RVA: 0x0009B733 File Offset: 0x00099933
			internal GroupBoxAccessibleObject(GroupBox owner)
				: base(owner)
			{
			}

			// Token: 0x17001689 RID: 5769
			// (get) Token: 0x060067CA RID: 26570 RVA: 0x00183020 File Offset: 0x00181220
			public override AccessibleRole Role
			{
				get
				{
					AccessibleRole accessibleRole = base.Owner.AccessibleRole;
					if (accessibleRole != AccessibleRole.Default)
					{
						return accessibleRole;
					}
					return AccessibleRole.Grouping;
				}
			}

			// Token: 0x060067CB RID: 26571 RVA: 0x0009B73C File Offset: 0x0009993C
			internal override bool IsIAccessibleExSupported()
			{
				return AccessibilityImprovements.Level3 || base.IsIAccessibleExSupported();
			}

			// Token: 0x060067CC RID: 26572 RVA: 0x00183041 File Offset: 0x00181241
			internal override bool IsPatternSupported(int patternId)
			{
				return (AccessibilityImprovements.Level3 && patternId == 10018) || base.IsPatternSupported(patternId);
			}

			// Token: 0x060067CD RID: 26573 RVA: 0x0018305C File Offset: 0x0018125C
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID != 30003)
				{
					if (propertyID != 30005)
					{
						if (propertyID == 30009)
						{
							return true;
						}
					}
					else if (AccessibilityImprovements.Level3)
					{
						return this.Name;
					}
					return base.GetPropertyValue(propertyID);
				}
				return 50026;
			}
		}
	}
}
