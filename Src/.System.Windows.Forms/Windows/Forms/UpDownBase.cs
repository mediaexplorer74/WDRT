using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.VisualStyles;
using Microsoft.Win32;

namespace System.Windows.Forms
{
	/// <summary>Implements the basic functionality required by a spin box (also known as an up-down control).</summary>
	// Token: 0x0200010A RID: 266
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Designer("System.Windows.Forms.Design.UpDownBaseDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public abstract class UpDownBase : ContainerControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.UpDownBase" /> class.</summary>
		// Token: 0x060004DF RID: 1247 RVA: 0x00011698 File Offset: 0x0000F898
		public UpDownBase()
		{
			if (DpiHelper.IsScalingRequired)
			{
				this.defaultButtonsWidth = base.LogicalToDeviceUnits(16);
			}
			this.upDownButtons = new UpDownBase.UpDownButtons(this);
			this.upDownEdit = new UpDownBase.UpDownEdit(this);
			this.upDownEdit.BorderStyle = BorderStyle.None;
			this.upDownEdit.AutoSize = false;
			this.upDownEdit.KeyDown += this.OnTextBoxKeyDown;
			this.upDownEdit.KeyPress += this.OnTextBoxKeyPress;
			this.upDownEdit.TextChanged += this.OnTextBoxTextChanged;
			this.upDownEdit.LostFocus += this.OnTextBoxLostFocus;
			this.upDownEdit.Resize += this.OnTextBoxResize;
			this.upDownButtons.TabStop = false;
			this.upDownButtons.Size = new Size(this.defaultButtonsWidth, this.PreferredHeight);
			this.upDownButtons.UpDown += this.OnUpDown;
			base.Controls.AddRange(new Control[] { this.upDownButtons, this.upDownEdit });
			base.SetStyle(ControlStyles.Opaque | ControlStyles.ResizeRedraw | ControlStyles.FixedHeight, true);
			base.SetStyle(ControlStyles.StandardClick, false);
			base.SetStyle(ControlStyles.UseTextForAccessibility, false);
		}

		/// <summary>Gets a value indicating whether the container will allow the user to scroll to any controls placed outside of its visible boundaries.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x0001180C File Offset: 0x0000FA0C
		// (set) Token: 0x060004E1 RID: 1249 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AutoScroll
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the size of the auto-scroll margin.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the height and width, in pixels, of the auto-scroll margin.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Size.Height" /> or <see cref="P:System.Drawing.Size.Width" /> is less than 0.</exception>
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x0001180F File Offset: 0x0000FA0F
		// (set) Token: 0x060004E3 RID: 1251 RVA: 0x00011817 File Offset: 0x0000FA17
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Size AutoScrollMargin
		{
			get
			{
				return base.AutoScrollMargin;
			}
			set
			{
				base.AutoScrollMargin = value;
			}
		}

		/// <summary>Gets or sets the minimum size of the auto-scroll area.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the minimum height and width, in pixels, of the scroll bars.</returns>
		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060004E4 RID: 1252 RVA: 0x00011820 File Offset: 0x0000FA20
		// (set) Token: 0x060004E5 RID: 1253 RVA: 0x00011828 File Offset: 0x0000FA28
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Size AutoScrollMinSize
		{
			get
			{
				return base.AutoScrollMinSize;
			}
			set
			{
				base.AutoScrollMinSize = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the control should automatically resize based on its contents.</summary>
		/// <returns>
		///   <see langword="true" /> to indicate the control should automatically resize based on its contents; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x00011831 File Offset: 0x0000FA31
		// (set) Token: 0x060004E7 RID: 1255 RVA: 0x00011839 File Offset: 0x0000FA39
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.UpDownBase.AutoSize" /> property changes.</summary>
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060004E8 RID: 1256 RVA: 0x00011842 File Offset: 0x0000FA42
		// (remove) Token: 0x060004E9 RID: 1257 RVA: 0x0001184B File Offset: 0x0000FA4B
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

		/// <summary>Gets or sets the background color for the text box portion of the spin box (also known as an up-down control).</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the text box portion of the spin box.</returns>
		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x00011854 File Offset: 0x0000FA54
		// (set) Token: 0x060004EB RID: 1259 RVA: 0x00011861 File Offset: 0x0000FA61
		public override Color BackColor
		{
			get
			{
				return this.upDownEdit.BackColor;
			}
			set
			{
				base.BackColor = value;
				this.upDownEdit.BackColor = value;
				base.Invalidate();
			}
		}

		/// <summary>Gets or sets the background image for the <see cref="T:System.Windows.Forms.UpDownBase" />.</summary>
		/// <returns>The background image for the <see cref="T:System.Windows.Forms.UpDownBase" />.</returns>
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x0001187C File Offset: 0x0000FA7C
		// (set) Token: 0x060004ED RID: 1261 RVA: 0x00011884 File Offset: 0x0000FA84
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
			set
			{
				base.BackgroundImage = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.UpDownBase.BackgroundImage" /> property changes.</summary>
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060004EE RID: 1262 RVA: 0x0001188D File Offset: 0x0000FA8D
		// (remove) Token: 0x060004EF RID: 1263 RVA: 0x00011896 File Offset: 0x0000FA96
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageChanged
		{
			add
			{
				base.BackgroundImageChanged += value;
			}
			remove
			{
				base.BackgroundImageChanged -= value;
			}
		}

		/// <summary>Gets or sets the layout of the <see cref="P:System.Windows.Forms.UpDownBase.BackgroundImage" /> of the <see cref="T:System.Windows.Forms.UpDownBase" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImageLayout" /> values.</returns>
		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x0001189F File Offset: 0x0000FA9F
		// (set) Token: 0x060004F1 RID: 1265 RVA: 0x000118A7 File Offset: 0x0000FAA7
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				return base.BackgroundImageLayout;
			}
			set
			{
				base.BackgroundImageLayout = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.UpDownBase.BackgroundImageLayout" /> property changes.</summary>
		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060004F2 RID: 1266 RVA: 0x000118B0 File Offset: 0x0000FAB0
		// (remove) Token: 0x060004F3 RID: 1267 RVA: 0x000118B9 File Offset: 0x0000FAB9
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageLayoutChanged
		{
			add
			{
				base.BackgroundImageLayoutChanged += value;
			}
			remove
			{
				base.BackgroundImageLayoutChanged -= value;
			}
		}

		/// <summary>Gets or sets the border style for the spin box (also known as an up-down control).</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. The default value is <see cref="F:System.Windows.Forms.BorderStyle.Fixed3D" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.BorderStyle" /> values.</exception>
		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x000118C2 File Offset: 0x0000FAC2
		// (set) Token: 0x060004F5 RID: 1269 RVA: 0x000118CA File Offset: 0x0000FACA
		[SRCategory("CatAppearance")]
		[DefaultValue(BorderStyle.Fixed3D)]
		[DispId(-504)]
		[SRDescription("UpDownBaseBorderStyleDescr")]
		public BorderStyle BorderStyle
		{
			get
			{
				return this.borderStyle;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(BorderStyle));
				}
				if (this.borderStyle != value)
				{
					this.borderStyle = value;
					base.RecreateHandle();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the text property is being changed internally by its parent class.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Windows.Forms.UpDownBase.Text" /> property is being changed internally by the <see cref="T:System.Windows.Forms.UpDownBase" /> class; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x00011908 File Offset: 0x0000FB08
		// (set) Token: 0x060004F7 RID: 1271 RVA: 0x00011910 File Offset: 0x0000FB10
		protected bool ChangingText
		{
			get
			{
				return this.changingText;
			}
			set
			{
				this.changingText = value;
			}
		}

		/// <summary>Gets or sets the shortcut menu associated with the spin box (also known as an up-down control).</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ContextMenu" /> associated with the spin box.</returns>
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x00011919 File Offset: 0x0000FB19
		// (set) Token: 0x060004F9 RID: 1273 RVA: 0x00011921 File Offset: 0x0000FB21
		public override ContextMenu ContextMenu
		{
			get
			{
				return base.ContextMenu;
			}
			set
			{
				base.ContextMenu = value;
				this.upDownEdit.ContextMenu = value;
			}
		}

		/// <summary>Gets or sets the shortcut menu for the spin box (also known as an up-down control).</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> associated with the control.</returns>
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x00011936 File Offset: 0x0000FB36
		// (set) Token: 0x060004FB RID: 1275 RVA: 0x0001193E File Offset: 0x0000FB3E
		public override ContextMenuStrip ContextMenuStrip
		{
			get
			{
				return base.ContextMenuStrip;
			}
			set
			{
				base.ContextMenuStrip = value;
				this.upDownEdit.ContextMenuStrip = value;
			}
		}

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>The creation parameters.</returns>
		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x00011954 File Offset: 0x0000FB54
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.Style &= -8388609;
				if (!Application.RenderWithVisualStyles)
				{
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
				}
				return createParams;
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x000119B7 File Offset: 0x0000FBB7
		protected override Size DefaultSize
		{
			get
			{
				return new Size(120, this.PreferredHeight);
			}
		}

		/// <summary>Gets the dock padding settings for all edges of the <see cref="T:System.Windows.Forms.UpDownBase" /> control.</summary>
		/// <returns>The dock paddings settings for this control.</returns>
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x000119C6 File Offset: 0x0000FBC6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ScrollableControl.DockPaddingEdges DockPadding
		{
			get
			{
				return base.DockPadding;
			}
		}

		/// <summary>Gets a value indicating whether the control has input focus.</summary>
		/// <returns>
		///   <see langword="true" /> if the control has focus; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x000119CE File Offset: 0x0000FBCE
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlFocusedDescr")]
		public override bool Focused
		{
			get
			{
				return this.upDownEdit.Focused;
			}
		}

		/// <summary>Gets or sets the foreground color of the spin box (also known as an up-down control).</summary>
		/// <returns>The foreground <see cref="T:System.Drawing.Color" /> of the spin box.</returns>
		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x000119DB File Offset: 0x0000FBDB
		// (set) Token: 0x06000501 RID: 1281 RVA: 0x000119E8 File Offset: 0x0000FBE8
		public override Color ForeColor
		{
			get
			{
				return this.upDownEdit.ForeColor;
			}
			set
			{
				base.ForeColor = value;
				this.upDownEdit.ForeColor = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the user can use the UP ARROW and DOWN ARROW keys to select values.</summary>
		/// <returns>
		///   <see langword="true" /> if the control allows the use of the UP ARROW and DOWN ARROW keys to select values; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x000119FD File Offset: 0x0000FBFD
		// (set) Token: 0x06000503 RID: 1283 RVA: 0x00011A05 File Offset: 0x0000FC05
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("UpDownBaseInterceptArrowKeysDescr")]
		public bool InterceptArrowKeys
		{
			get
			{
				return this.interceptArrowKeys;
			}
			set
			{
				this.interceptArrowKeys = value;
			}
		}

		/// <summary>Gets or sets the maximum size of the spin box (also known as an up-down control).</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" />, which is the maximum size of the spin box.</returns>
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x00011A0E File Offset: 0x0000FC0E
		// (set) Token: 0x06000505 RID: 1285 RVA: 0x00011A16 File Offset: 0x0000FC16
		public override Size MaximumSize
		{
			get
			{
				return base.MaximumSize;
			}
			set
			{
				base.MaximumSize = new Size(value.Width, 0);
			}
		}

		/// <summary>Gets or sets the minimum size of the spin box (also known as an up-down control).</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" />, which is the minimum size of the spin box.</returns>
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x00011A2B File Offset: 0x0000FC2B
		// (set) Token: 0x06000507 RID: 1287 RVA: 0x00011A33 File Offset: 0x0000FC33
		public override Size MinimumSize
		{
			get
			{
				return base.MinimumSize;
			}
			set
			{
				base.MinimumSize = new Size(value.Width, 0);
			}
		}

		/// <summary>Occurs when the mouse pointer enters the <see cref="T:System.Windows.Forms.UpDownBase" /> control.</summary>
		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000508 RID: 1288 RVA: 0x00011A48 File Offset: 0x0000FC48
		// (remove) Token: 0x06000509 RID: 1289 RVA: 0x00011A51 File Offset: 0x0000FC51
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		/// <summary>Occurs when the mouse pointer leaves the <see cref="T:System.Windows.Forms.UpDownBase" /> control.</summary>
		// Token: 0x1400000E RID: 14
		// (add) Token: 0x0600050A RID: 1290 RVA: 0x00011A5A File Offset: 0x0000FC5A
		// (remove) Token: 0x0600050B RID: 1291 RVA: 0x00011A63 File Offset: 0x0000FC63
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		/// <summary>Occurs when the mouse pointer rests on the <see cref="T:System.Windows.Forms.UpDownBase" /> control.</summary>
		// Token: 0x1400000F RID: 15
		// (add) Token: 0x0600050C RID: 1292 RVA: 0x00011A6C File Offset: 0x0000FC6C
		// (remove) Token: 0x0600050D RID: 1293 RVA: 0x00011A75 File Offset: 0x0000FC75
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler MouseHover
		{
			add
			{
				base.MouseHover += value;
			}
			remove
			{
				base.MouseHover -= value;
			}
		}

		/// <summary>Occurs when the user moves the mouse pointer over the <see cref="T:System.Windows.Forms.UpDownBase" /> control.</summary>
		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600050E RID: 1294 RVA: 0x00011A7E File Offset: 0x0000FC7E
		// (remove) Token: 0x0600050F RID: 1295 RVA: 0x00011A87 File Offset: 0x0000FC87
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		/// <summary>Gets the height of the spin box (also known as an up-down control).</summary>
		/// <returns>The height, in pixels, of the spin box.</returns>
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x00011A90 File Offset: 0x0000FC90
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("UpDownBasePreferredHeightDescr")]
		public int PreferredHeight
		{
			get
			{
				int num = base.FontHeight;
				if (this.borderStyle != BorderStyle.None)
				{
					num += SystemInformation.BorderSize.Height * 4 + 3;
				}
				else
				{
					num += 3;
				}
				return num;
			}
		}

		/// <summary>Gets or sets a value indicating whether the text can be changed by the use of the up or down buttons only.</summary>
		/// <returns>
		///   <see langword="true" /> if the text can be changed by the use of the up or down buttons only; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x00011AC7 File Offset: 0x0000FCC7
		// (set) Token: 0x06000512 RID: 1298 RVA: 0x00011AD4 File Offset: 0x0000FCD4
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("UpDownBaseReadOnlyDescr")]
		public bool ReadOnly
		{
			get
			{
				return this.upDownEdit.ReadOnly;
			}
			set
			{
				this.upDownEdit.ReadOnly = value;
			}
		}

		/// <summary>Gets or sets the text displayed in the spin box (also known as an up-down control).</summary>
		/// <returns>The string value displayed in the spin box.</returns>
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x00011AE2 File Offset: 0x0000FCE2
		// (set) Token: 0x06000514 RID: 1300 RVA: 0x00011AEF File Offset: 0x0000FCEF
		[Localizable(true)]
		public override string Text
		{
			get
			{
				return this.upDownEdit.Text;
			}
			set
			{
				this.upDownEdit.Text = value;
				this.ChangingText = false;
				if (this.UserEdit)
				{
					this.ValidateEditText();
				}
			}
		}

		/// <summary>Gets or sets the alignment of the text in the spin box (also known as an up-down control).</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values. The default value is <see cref="F:System.Windows.Forms.HorizontalAlignment.Left" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values.</exception>
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x00011B12 File Offset: 0x0000FD12
		// (set) Token: 0x06000516 RID: 1302 RVA: 0x00011B1F File Offset: 0x0000FD1F
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[DefaultValue(HorizontalAlignment.Left)]
		[SRDescription("UpDownBaseTextAlignDescr")]
		public HorizontalAlignment TextAlign
		{
			get
			{
				return this.upDownEdit.TextAlign;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(HorizontalAlignment));
				}
				this.upDownEdit.TextAlign = value;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x00011B53 File Offset: 0x0000FD53
		internal TextBox TextBox
		{
			get
			{
				return this.upDownEdit;
			}
		}

		/// <summary>Gets or sets the alignment of the up and down buttons on the spin box (also known as an up-down control).</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.LeftRightAlignment" /> values. The default value is <see cref="F:System.Windows.Forms.LeftRightAlignment.Right" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.LeftRightAlignment" /> values.</exception>
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x00011B5B File Offset: 0x0000FD5B
		// (set) Token: 0x06000519 RID: 1305 RVA: 0x00011B64 File Offset: 0x0000FD64
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[DefaultValue(LeftRightAlignment.Right)]
		[SRDescription("UpDownBaseAlignmentDescr")]
		public LeftRightAlignment UpDownAlign
		{
			get
			{
				return this.upDownAlign;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 1))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(LeftRightAlignment));
				}
				if (this.upDownAlign != value)
				{
					this.upDownAlign = value;
					this.PositionControls();
					base.Invalidate();
				}
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x00011BB3 File Offset: 0x0000FDB3
		internal UpDownBase.UpDownButtons UpDownButtonsInternal
		{
			get
			{
				return this.upDownButtons;
			}
		}

		/// <summary>Gets or sets a value indicating whether a value has been entered by the user.</summary>
		/// <returns>
		///   <see langword="true" /> if the user has changed the <see cref="P:System.Windows.Forms.UpDownBase.Text" /> property; otherwise, <see langword="false" />.</returns>
		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x00011BBB File Offset: 0x0000FDBB
		// (set) Token: 0x0600051C RID: 1308 RVA: 0x00011BC3 File Offset: 0x0000FDC3
		protected bool UserEdit
		{
			get
			{
				return this.userEdit;
			}
			set
			{
				this.userEdit = value;
			}
		}

		/// <summary>When overridden in a derived class, handles the clicking of the down button on the spin box (also known as an up-down control).</summary>
		// Token: 0x0600051D RID: 1309
		public abstract void DownButton();

		// Token: 0x0600051E RID: 1310 RVA: 0x00011BCC File Offset: 0x0000FDCC
		internal override Rectangle ApplyBoundsConstraints(int suggestedX, int suggestedY, int proposedWidth, int proposedHeight)
		{
			return base.ApplyBoundsConstraints(suggestedX, suggestedY, proposedWidth, this.PreferredHeight);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00011BDD File Offset: 0x0000FDDD
		internal string GetAccessibleName(string baseName)
		{
			if (baseName == null)
			{
				if (AccessibilityImprovements.Level5)
				{
					return string.Empty;
				}
				if (AccessibilityImprovements.Level3)
				{
					return SR.GetString("SpinnerAccessibleName");
				}
				if (AccessibilityImprovements.Level1)
				{
					return base.GetType().Name;
				}
			}
			return baseName;
		}

		/// <summary>Provides constants for rescaling the control when a DPI change occurs.</summary>
		/// <param name="deviceDpiOld">The DPI value prior to the change.</param>
		/// <param name="deviceDpiNew">The DPI value after the change.</param>
		// Token: 0x06000520 RID: 1312 RVA: 0x00011C15 File Offset: 0x0000FE15
		protected override void RescaleConstantsForDpi(int deviceDpiOld, int deviceDpiNew)
		{
			base.RescaleConstantsForDpi(deviceDpiOld, deviceDpiNew);
			this.defaultButtonsWidth = base.LogicalToDeviceUnits(16);
			this.upDownButtons.Width = this.defaultButtonsWidth;
		}

		/// <summary>When overridden in a derived class, raises the Changed event.</summary>
		/// <param name="source">The source of the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000521 RID: 1313 RVA: 0x000070A6 File Offset: 0x000052A6
		protected virtual void OnChanged(object source, EventArgs e)
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000522 RID: 1314 RVA: 0x00011C3E File Offset: 0x0000FE3E
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			this.PositionControls();
			SystemEvents.UserPreferenceChanged += this.UserPreferenceChanged;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000523 RID: 1315 RVA: 0x00011C5E File Offset: 0x0000FE5E
		protected override void OnHandleDestroyed(EventArgs e)
		{
			SystemEvents.UserPreferenceChanged -= this.UserPreferenceChanged;
			base.OnHandleDestroyed(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		// Token: 0x06000524 RID: 1316 RVA: 0x00011C78 File Offset: 0x0000FE78
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Rectangle bounds = this.upDownEdit.Bounds;
			if (Application.RenderWithVisualStyles)
			{
				if (this.borderStyle == BorderStyle.None)
				{
					goto IL_249;
				}
				Rectangle clientRectangle = base.ClientRectangle;
				Rectangle clipRectangle = e.ClipRectangle;
				VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer(VisualStyleElement.TextBox.TextEdit.Normal);
				int num = 1;
				Rectangle rectangle = new Rectangle(clientRectangle.Left, clientRectangle.Top, num, clientRectangle.Height);
				Rectangle rectangle2 = new Rectangle(clientRectangle.Left, clientRectangle.Top, clientRectangle.Width, num);
				Rectangle rectangle3 = new Rectangle(clientRectangle.Right - num, clientRectangle.Top, num, clientRectangle.Height);
				Rectangle rectangle4 = new Rectangle(clientRectangle.Left, clientRectangle.Bottom - num, clientRectangle.Width, num);
				rectangle.Intersect(clipRectangle);
				rectangle2.Intersect(clipRectangle);
				rectangle3.Intersect(clipRectangle);
				rectangle4.Intersect(clipRectangle);
				visualStyleRenderer.DrawBackground(e.Graphics, clientRectangle, rectangle, base.HandleInternal);
				visualStyleRenderer.DrawBackground(e.Graphics, clientRectangle, rectangle2, base.HandleInternal);
				visualStyleRenderer.DrawBackground(e.Graphics, clientRectangle, rectangle3, base.HandleInternal);
				visualStyleRenderer.DrawBackground(e.Graphics, clientRectangle, rectangle4, base.HandleInternal);
				using (Pen pen = new Pen(this.BackColor))
				{
					Rectangle rectangle5 = bounds;
					int num2 = rectangle5.X;
					rectangle5.X = num2 - 1;
					num2 = rectangle5.Y;
					rectangle5.Y = num2 - 1;
					num2 = rectangle5.Width;
					rectangle5.Width = num2 + 1;
					num2 = rectangle5.Height;
					rectangle5.Height = num2 + 1;
					e.Graphics.DrawRectangle(pen, rectangle5);
					goto IL_249;
				}
			}
			using (Pen pen2 = new Pen(this.BackColor, (float)(base.Enabled ? 2 : 1)))
			{
				Rectangle rectangle6 = bounds;
				rectangle6.Inflate(1, 1);
				if (!base.Enabled)
				{
					int num2 = rectangle6.X;
					rectangle6.X = num2 - 1;
					num2 = rectangle6.Y;
					rectangle6.Y = num2 - 1;
					num2 = rectangle6.Width;
					rectangle6.Width = num2 + 1;
					num2 = rectangle6.Height;
					rectangle6.Height = num2 + 1;
				}
				e.Graphics.DrawRectangle(pen2, rectangle6);
			}
			IL_249:
			if (!base.Enabled && this.BorderStyle != BorderStyle.None && !this.upDownEdit.ShouldSerializeBackColor())
			{
				bounds.Inflate(1, 1);
				ControlPaint.DrawBorder(e.Graphics, bounds, SystemColors.Control, ButtonBorderStyle.Solid);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.</summary>
		/// <param name="source">The source of the event.</param>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		// Token: 0x06000525 RID: 1317 RVA: 0x00011F24 File Offset: 0x00010124
		protected virtual void OnTextBoxKeyDown(object source, KeyEventArgs e)
		{
			this.OnKeyDown(e);
			if (this.interceptArrowKeys)
			{
				if (e.KeyData == Keys.Up)
				{
					this.UpButton();
					e.Handled = true;
				}
				else if (e.KeyData == Keys.Down)
				{
					this.DownButton();
					e.Handled = true;
				}
			}
			if (e.KeyCode == Keys.Return && this.UserEdit)
			{
				this.ValidateEditText();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event.</summary>
		/// <param name="source">The source of the event.</param>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data.</param>
		// Token: 0x06000526 RID: 1318 RVA: 0x00011F88 File Offset: 0x00010188
		protected virtual void OnTextBoxKeyPress(object source, KeyPressEventArgs e)
		{
			this.OnKeyPress(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event.</summary>
		/// <param name="source">The source of the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000527 RID: 1319 RVA: 0x00011F91 File Offset: 0x00010191
		protected virtual void OnTextBoxLostFocus(object source, EventArgs e)
		{
			if (this.UserEdit)
			{
				this.ValidateEditText();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.</summary>
		/// <param name="source">The source of the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000528 RID: 1320 RVA: 0x00011FA1 File Offset: 0x000101A1
		protected virtual void OnTextBoxResize(object source, EventArgs e)
		{
			base.Height = this.PreferredHeight;
			this.PositionControls();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.TextChanged" /> event.</summary>
		/// <param name="source">The source of the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000529 RID: 1321 RVA: 0x00011FB5 File Offset: 0x000101B5
		protected virtual void OnTextBoxTextChanged(object source, EventArgs e)
		{
			if (this.changingText)
			{
				this.ChangingText = false;
			}
			else
			{
				this.UserEdit = true;
			}
			this.OnTextChanged(e);
			this.OnChanged(source, new EventArgs());
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x000070A6 File Offset: 0x000052A6
		internal virtual void OnStartTimer()
		{
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x000070A6 File Offset: 0x000052A6
		internal virtual void OnStopTimer()
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x0600052C RID: 1324 RVA: 0x00011FE2 File Offset: 0x000101E2
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (e.Clicks == 2 && e.Button == MouseButtons.Left)
			{
				this.doubleClickFired = true;
			}
			base.OnMouseDown(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.</summary>
		/// <param name="mevent">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x0600052D RID: 1325 RVA: 0x00012008 File Offset: 0x00010208
		protected override void OnMouseUp(MouseEventArgs mevent)
		{
			if (mevent.Button == MouseButtons.Left)
			{
				Point point = base.PointToScreen(new Point(mevent.X, mevent.Y));
				if (UnsafeNativeMethods.WindowFromPoint(point.X, point.Y) == base.Handle && !base.ValidationCancelled)
				{
					if (!this.doubleClickFired)
					{
						this.OnClick(mevent);
						this.OnMouseClick(mevent);
					}
					else
					{
						this.doubleClickFired = false;
						this.OnDoubleClick(mevent);
						this.OnMouseDoubleClick(mevent);
					}
				}
				this.doubleClickFired = false;
			}
			base.OnMouseUp(mevent);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseWheel" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x0600052E RID: 1326 RVA: 0x000120A0 File Offset: 0x000102A0
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);
			HandledMouseEventArgs handledMouseEventArgs = e as HandledMouseEventArgs;
			if (handledMouseEventArgs != null)
			{
				if (handledMouseEventArgs.Handled)
				{
					return;
				}
				handledMouseEventArgs.Handled = true;
			}
			if ((Control.ModifierKeys & (Keys.Shift | Keys.Alt)) != Keys.None || Control.MouseButtons != MouseButtons.None)
			{
				return;
			}
			int num = SystemInformation.MouseWheelScrollLines;
			if (num == 0)
			{
				return;
			}
			this.wheelDelta += e.Delta;
			float num2 = (float)this.wheelDelta / 120f;
			if (num == -1)
			{
				num = 1;
			}
			int num3 = (int)((float)num * num2);
			if (num3 != 0)
			{
				if (num3 > 0)
				{
					for (int i = num3; i > 0; i--)
					{
						this.UpButton();
					}
					this.wheelDelta -= (int)((float)num3 * (120f / (float)num));
					return;
				}
				for (int i = -num3; i > 0; i--)
				{
					this.DownButton();
				}
				this.wheelDelta -= (int)((float)num3 * (120f / (float)num));
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
		// Token: 0x0600052F RID: 1327 RVA: 0x00012181 File Offset: 0x00010381
		protected override void OnLayout(LayoutEventArgs e)
		{
			this.PositionControls();
			base.OnLayout(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000530 RID: 1328 RVA: 0x00012190 File Offset: 0x00010390
		protected override void OnFontChanged(EventArgs e)
		{
			base.FontHeight = -1;
			base.Height = this.PreferredHeight;
			this.PositionControls();
			base.OnFontChanged(e);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x000121B2 File Offset: 0x000103B2
		private void OnUpDown(object source, UpDownEventArgs e)
		{
			if (e.ButtonID == 1)
			{
				this.UpButton();
				return;
			}
			if (e.ButtonID == 2)
			{
				this.DownButton();
			}
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x000121D4 File Offset: 0x000103D4
		private void PositionControls()
		{
			Rectangle rectangle = Rectangle.Empty;
			Rectangle empty = Rectangle.Empty;
			Rectangle rectangle2 = new Rectangle(Point.Empty, base.ClientSize);
			int width = rectangle2.Width;
			bool renderWithVisualStyles = Application.RenderWithVisualStyles;
			BorderStyle borderStyle = this.BorderStyle;
			int num = ((borderStyle == BorderStyle.None) ? 0 : 2);
			rectangle2.Inflate(-num, -num);
			if (this.upDownEdit != null)
			{
				rectangle = rectangle2;
				rectangle.Size = new Size(rectangle2.Width - this.defaultButtonsWidth, rectangle2.Height);
			}
			if (this.upDownButtons != null)
			{
				int num2 = (renderWithVisualStyles ? 1 : 2);
				if (borderStyle == BorderStyle.None)
				{
					num2 = 0;
				}
				empty = new Rectangle(rectangle2.Right - this.defaultButtonsWidth + num2, rectangle2.Top - num2, this.defaultButtonsWidth, rectangle2.Height + num2 * 2);
			}
			LeftRightAlignment leftRightAlignment = this.UpDownAlign;
			if (base.RtlTranslateLeftRight(leftRightAlignment) == LeftRightAlignment.Left)
			{
				empty.X = width - empty.Right;
				rectangle.X = width - rectangle.Right;
			}
			if (this.upDownEdit != null)
			{
				this.upDownEdit.Bounds = rectangle;
			}
			if (this.upDownButtons != null)
			{
				this.upDownButtons.Bounds = empty;
				this.upDownButtons.Invalidate();
			}
		}

		/// <summary>Selects a range of text in the spin box (also known as an up-down control) specifying the starting position and number of characters to select.</summary>
		/// <param name="start">The position of the first character to be selected.</param>
		/// <param name="length">The total number of characters to be selected.</param>
		// Token: 0x06000533 RID: 1331 RVA: 0x0001230E File Offset: 0x0001050E
		public void Select(int start, int length)
		{
			this.upDownEdit.Select(start, length);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00012320 File Offset: 0x00010520
		private MouseEventArgs TranslateMouseEvent(Control child, MouseEventArgs e)
		{
			if (child != null && base.IsHandleCreated)
			{
				NativeMethods.POINT point = new NativeMethods.POINT(e.X, e.Y);
				UnsafeNativeMethods.MapWindowPoints(new HandleRef(child, child.Handle), new HandleRef(this, base.Handle), point, 1);
				return new MouseEventArgs(e.Button, e.Clicks, point.x, point.y, e.Delta);
			}
			return e;
		}

		/// <summary>When overridden in a derived class, handles the clicking of the up button on the spin box (also known as an up-down control).</summary>
		// Token: 0x06000535 RID: 1333
		public abstract void UpButton();

		/// <summary>When overridden in a derived class, updates the text displayed in the spin box (also known as an up-down control).</summary>
		// Token: 0x06000536 RID: 1334
		protected abstract void UpdateEditText();

		// Token: 0x06000537 RID: 1335 RVA: 0x0001238F File Offset: 0x0001058F
		private void UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs pref)
		{
			if (pref.Category == UserPreferenceCategory.Locale)
			{
				this.UpdateEditText();
			}
		}

		/// <summary>When overridden in a derived class, validates the text displayed in the spin box (also known as an up-down control).</summary>
		// Token: 0x06000538 RID: 1336 RVA: 0x000070A6 File Offset: 0x000052A6
		protected virtual void ValidateEditText()
		{
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x06000539 RID: 1337 RVA: 0x000123A4 File Offset: 0x000105A4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg != 7)
			{
				if (msg != 8)
				{
					base.WndProc(ref m);
					return;
				}
				this.DefWndProc(ref m);
				return;
			}
			else
			{
				if (base.HostedInWin32DialogManager)
				{
					if (this.TextBox.CanFocus)
					{
						UnsafeNativeMethods.SetFocus(new HandleRef(this.TextBox, this.TextBox.Handle));
					}
					base.WndProc(ref m);
					return;
				}
				if (base.ActiveControl == null)
				{
					base.SetActiveControlInternal(this.TextBox);
					return;
				}
				base.FocusActiveControlInternal();
				return;
			}
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00012426 File Offset: 0x00010626
		internal void SetToolTip(ToolTip toolTip, string caption)
		{
			toolTip.SetToolTip(this.upDownEdit, caption);
			toolTip.SetToolTip(this.upDownButtons, caption);
		}

		// Token: 0x040004A8 RID: 1192
		private const int DefaultWheelScrollLinesPerPage = 1;

		// Token: 0x040004A9 RID: 1193
		private const int DefaultButtonsWidth = 16;

		// Token: 0x040004AA RID: 1194
		private const int DefaultControlWidth = 120;

		// Token: 0x040004AB RID: 1195
		private const int ThemedBorderWidth = 1;

		// Token: 0x040004AC RID: 1196
		private const BorderStyle DefaultBorderStyle = BorderStyle.Fixed3D;

		// Token: 0x040004AD RID: 1197
		private static readonly bool DefaultInterceptArrowKeys = true;

		// Token: 0x040004AE RID: 1198
		private const LeftRightAlignment DefaultUpDownAlign = LeftRightAlignment.Right;

		// Token: 0x040004AF RID: 1199
		private const int DefaultTimerInterval = 500;

		// Token: 0x040004B0 RID: 1200
		internal UpDownBase.UpDownEdit upDownEdit;

		// Token: 0x040004B1 RID: 1201
		internal UpDownBase.UpDownButtons upDownButtons;

		// Token: 0x040004B2 RID: 1202
		private bool interceptArrowKeys = UpDownBase.DefaultInterceptArrowKeys;

		// Token: 0x040004B3 RID: 1203
		private LeftRightAlignment upDownAlign = LeftRightAlignment.Right;

		// Token: 0x040004B4 RID: 1204
		private bool userEdit;

		// Token: 0x040004B5 RID: 1205
		private BorderStyle borderStyle = BorderStyle.Fixed3D;

		// Token: 0x040004B6 RID: 1206
		private int wheelDelta;

		// Token: 0x040004B7 RID: 1207
		private bool changingText;

		// Token: 0x040004B8 RID: 1208
		private bool doubleClickFired;

		// Token: 0x040004B9 RID: 1209
		internal int defaultButtonsWidth = 16;

		// Token: 0x02000556 RID: 1366
		internal class UpDownEdit : TextBox
		{
			// Token: 0x060055A7 RID: 21927 RVA: 0x00166EEA File Offset: 0x001650EA
			internal UpDownEdit(UpDownBase parent)
			{
				base.SetStyle(ControlStyles.FixedWidth | ControlStyles.FixedHeight, true);
				base.SetStyle(ControlStyles.Selectable, false);
				this.parent = parent;
			}

			// Token: 0x17001490 RID: 5264
			// (get) Token: 0x060055A8 RID: 21928 RVA: 0x00166F0E File Offset: 0x0016510E
			// (set) Token: 0x060055A9 RID: 21929 RVA: 0x00166F18 File Offset: 0x00165118
			public override string Text
			{
				get
				{
					return base.Text;
				}
				set
				{
					bool flag = value != base.Text;
					base.Text = value;
					if (flag && AccessibilityImprovements.Level1)
					{
						base.AccessibilityNotifyClients(AccessibleEvents.NameChange, -1);
					}
				}
			}

			// Token: 0x060055AA RID: 21930 RVA: 0x00166F4F File Offset: 0x0016514F
			protected override AccessibleObject CreateAccessibilityInstance()
			{
				if (AccessibilityImprovements.Level5)
				{
					return new UpDownBase.UpDownEdit.UpDownEditAccessibleObjectLevel5(this, this.parent);
				}
				return new UpDownBase.UpDownEdit.UpDownEditAccessibleObject(this, this.parent);
			}

			// Token: 0x060055AB RID: 21931 RVA: 0x00166F71 File Offset: 0x00165171
			protected override void OnMouseDown(MouseEventArgs e)
			{
				if (e.Clicks == 2 && e.Button == MouseButtons.Left)
				{
					this.doubleClickFired = true;
				}
				this.parent.OnMouseDown(this.parent.TranslateMouseEvent(this, e));
			}

			// Token: 0x060055AC RID: 21932 RVA: 0x00166FA8 File Offset: 0x001651A8
			protected override void OnMouseUp(MouseEventArgs e)
			{
				Point point = new Point(e.X, e.Y);
				point = base.PointToScreen(point);
				MouseEventArgs mouseEventArgs = this.parent.TranslateMouseEvent(this, e);
				if (e.Button == MouseButtons.Left)
				{
					if (!this.parent.ValidationCancelled && UnsafeNativeMethods.WindowFromPoint(point.X, point.Y) == base.Handle)
					{
						if (!this.doubleClickFired)
						{
							this.parent.OnClick(mouseEventArgs);
							this.parent.OnMouseClick(mouseEventArgs);
						}
						else
						{
							this.doubleClickFired = false;
							this.parent.OnDoubleClick(mouseEventArgs);
							this.parent.OnMouseDoubleClick(mouseEventArgs);
						}
					}
					this.doubleClickFired = false;
				}
				this.parent.OnMouseUp(mouseEventArgs);
			}

			// Token: 0x060055AD RID: 21933 RVA: 0x0016706C File Offset: 0x0016526C
			internal override void WmContextMenu(ref Message m)
			{
				if (this.ContextMenu == null && this.ContextMenuStrip != null)
				{
					base.WmContextMenu(ref m, this.parent);
					return;
				}
				base.WmContextMenu(ref m, this);
			}

			// Token: 0x060055AE RID: 21934 RVA: 0x00167094 File Offset: 0x00165294
			protected override void OnKeyUp(KeyEventArgs e)
			{
				this.parent.OnKeyUp(e);
			}

			// Token: 0x060055AF RID: 21935 RVA: 0x001670A2 File Offset: 0x001652A2
			protected override void OnGotFocus(EventArgs e)
			{
				this.parent.SetActiveControlInternal(this);
				this.parent.InvokeGotFocus(this.parent, e);
			}

			// Token: 0x060055B0 RID: 21936 RVA: 0x001670C2 File Offset: 0x001652C2
			protected override void OnLostFocus(EventArgs e)
			{
				this.parent.InvokeLostFocus(this.parent, e);
			}

			// Token: 0x04003824 RID: 14372
			private UpDownBase parent;

			// Token: 0x04003825 RID: 14373
			private bool doubleClickFired;

			// Token: 0x020008A4 RID: 2212
			internal class UpDownEditAccessibleObjectLevel5 : TextBoxBase.TextBoxBaseAccessibleObject
			{
				// Token: 0x06007224 RID: 29220 RVA: 0x001A2337 File Offset: 0x001A0537
				public UpDownEditAccessibleObjectLevel5(UpDownBase.UpDownEdit owner, UpDownBase parent)
					: base(owner)
				{
					this._parent = parent;
				}

				// Token: 0x17001912 RID: 6418
				// (get) Token: 0x06007225 RID: 29221 RVA: 0x001A2347 File Offset: 0x001A0547
				// (set) Token: 0x06007226 RID: 29222 RVA: 0x001A2359 File Offset: 0x001A0559
				public override string Name
				{
					get
					{
						return this._parent.AccessibilityObject.Name;
					}
					set
					{
						this._parent.AccessibilityObject.Name = value;
					}
				}

				// Token: 0x17001913 RID: 6419
				// (get) Token: 0x06007227 RID: 29223 RVA: 0x001A236C File Offset: 0x001A056C
				public override string KeyboardShortcut
				{
					get
					{
						return this._parent.AccessibilityObject.KeyboardShortcut;
					}
				}

				// Token: 0x040044D6 RID: 17622
				private readonly UpDownBase _parent;
			}

			// Token: 0x020008A5 RID: 2213
			internal class UpDownEditAccessibleObject : Control.ControlAccessibleObject
			{
				// Token: 0x06007228 RID: 29224 RVA: 0x001A237E File Offset: 0x001A057E
				public UpDownEditAccessibleObject(UpDownBase.UpDownEdit owner, UpDownBase parent)
					: base(owner)
				{
					this.parent = parent;
				}

				// Token: 0x17001914 RID: 6420
				// (get) Token: 0x06007229 RID: 29225 RVA: 0x001A238E File Offset: 0x001A058E
				// (set) Token: 0x0600722A RID: 29226 RVA: 0x001A23A0 File Offset: 0x001A05A0
				public override string Name
				{
					get
					{
						return this.parent.AccessibilityObject.Name;
					}
					set
					{
						this.parent.AccessibilityObject.Name = value;
					}
				}

				// Token: 0x17001915 RID: 6421
				// (get) Token: 0x0600722B RID: 29227 RVA: 0x001A23B3 File Offset: 0x001A05B3
				public override string KeyboardShortcut
				{
					get
					{
						return this.parent.AccessibilityObject.KeyboardShortcut;
					}
				}

				// Token: 0x040044D7 RID: 17623
				private UpDownBase parent;
			}
		}

		// Token: 0x02000557 RID: 1367
		internal class UpDownButtons : Control
		{
			// Token: 0x060055B1 RID: 21937 RVA: 0x001670D6 File Offset: 0x001652D6
			internal UpDownButtons(UpDownBase parent)
			{
				base.SetStyle(ControlStyles.Opaque | ControlStyles.FixedWidth | ControlStyles.FixedHeight, true);
				base.SetStyle(ControlStyles.Selectable, false);
				this.parent = parent;
			}

			// Token: 0x14000417 RID: 1047
			// (add) Token: 0x060055B2 RID: 21938 RVA: 0x001670FA File Offset: 0x001652FA
			// (remove) Token: 0x060055B3 RID: 21939 RVA: 0x00167113 File Offset: 0x00165313
			public event UpDownEventHandler UpDown
			{
				add
				{
					this.upDownEventHandler = (UpDownEventHandler)Delegate.Combine(this.upDownEventHandler, value);
				}
				remove
				{
					this.upDownEventHandler = (UpDownEventHandler)Delegate.Remove(this.upDownEventHandler, value);
				}
			}

			// Token: 0x060055B4 RID: 21940 RVA: 0x0016712C File Offset: 0x0016532C
			private void BeginButtonPress(MouseEventArgs e)
			{
				int num = base.Size.Height / 2;
				if (e.Y < num)
				{
					this.pushed = (this.captured = UpDownBase.ButtonID.Up);
					base.Invalidate();
				}
				else
				{
					this.pushed = (this.captured = UpDownBase.ButtonID.Down);
					base.Invalidate();
				}
				base.CaptureInternal = true;
				this.OnUpDown(new UpDownEventArgs((int)this.pushed));
				this.StartTimer();
			}

			// Token: 0x060055B5 RID: 21941 RVA: 0x0016719F File Offset: 0x0016539F
			protected override AccessibleObject CreateAccessibilityInstance()
			{
				return new UpDownBase.UpDownButtons.UpDownButtonsAccessibleObject(this);
			}

			// Token: 0x060055B6 RID: 21942 RVA: 0x001671A7 File Offset: 0x001653A7
			private void EndButtonPress()
			{
				this.pushed = UpDownBase.ButtonID.None;
				this.captured = UpDownBase.ButtonID.None;
				this.StopTimer();
				base.CaptureInternal = false;
				base.Invalidate();
			}

			// Token: 0x060055B7 RID: 21943 RVA: 0x001671CC File Offset: 0x001653CC
			protected override void OnMouseDown(MouseEventArgs e)
			{
				this.parent.FocusInternal();
				if (!this.parent.ValidationCancelled && e.Button == MouseButtons.Left)
				{
					this.BeginButtonPress(e);
				}
				if (e.Clicks == 2 && e.Button == MouseButtons.Left)
				{
					this.doubleClickFired = true;
				}
				this.parent.OnMouseDown(this.parent.TranslateMouseEvent(this, e));
			}

			// Token: 0x060055B8 RID: 21944 RVA: 0x0016723C File Offset: 0x0016543C
			protected override void OnMouseMove(MouseEventArgs e)
			{
				if (base.Capture)
				{
					Rectangle clientRectangle = base.ClientRectangle;
					clientRectangle.Height /= 2;
					if (this.captured == UpDownBase.ButtonID.Down)
					{
						clientRectangle.Y += clientRectangle.Height;
					}
					if (clientRectangle.Contains(e.X, e.Y))
					{
						if (this.pushed != this.captured)
						{
							this.StartTimer();
							this.pushed = this.captured;
							base.Invalidate();
						}
					}
					else if (this.pushed != UpDownBase.ButtonID.None)
					{
						this.StopTimer();
						this.pushed = UpDownBase.ButtonID.None;
						base.Invalidate();
					}
				}
				Rectangle clientRectangle2 = base.ClientRectangle;
				Rectangle clientRectangle3 = base.ClientRectangle;
				clientRectangle2.Height /= 2;
				clientRectangle3.Y += clientRectangle3.Height / 2;
				if (clientRectangle2.Contains(e.X, e.Y))
				{
					this.mouseOver = UpDownBase.ButtonID.Up;
					base.Invalidate();
				}
				else if (clientRectangle3.Contains(e.X, e.Y))
				{
					this.mouseOver = UpDownBase.ButtonID.Down;
					base.Invalidate();
				}
				this.parent.OnMouseMove(this.parent.TranslateMouseEvent(this, e));
			}

			// Token: 0x060055B9 RID: 21945 RVA: 0x00167374 File Offset: 0x00165574
			protected override void OnMouseUp(MouseEventArgs e)
			{
				if (!this.parent.ValidationCancelled && e.Button == MouseButtons.Left)
				{
					this.EndButtonPress();
				}
				Point point = new Point(e.X, e.Y);
				point = base.PointToScreen(point);
				MouseEventArgs mouseEventArgs = this.parent.TranslateMouseEvent(this, e);
				if (e.Button == MouseButtons.Left)
				{
					if (!this.parent.ValidationCancelled && UnsafeNativeMethods.WindowFromPoint(point.X, point.Y) == base.Handle)
					{
						if (!this.doubleClickFired)
						{
							this.parent.OnClick(mouseEventArgs);
						}
						else
						{
							this.doubleClickFired = false;
							this.parent.OnDoubleClick(mouseEventArgs);
							this.parent.OnMouseDoubleClick(mouseEventArgs);
						}
					}
					this.doubleClickFired = false;
				}
				this.parent.OnMouseUp(mouseEventArgs);
			}

			// Token: 0x060055BA RID: 21946 RVA: 0x0016744C File Offset: 0x0016564C
			protected override void OnMouseLeave(EventArgs e)
			{
				this.mouseOver = UpDownBase.ButtonID.None;
				base.Invalidate();
				this.parent.OnMouseLeave(e);
			}

			// Token: 0x060055BB RID: 21947 RVA: 0x00167468 File Offset: 0x00165668
			protected override void OnPaint(PaintEventArgs e)
			{
				int num = base.ClientSize.Height / 2;
				if (Application.RenderWithVisualStyles)
				{
					VisualStyleRenderer visualStyleRenderer = new VisualStyleRenderer((this.mouseOver == UpDownBase.ButtonID.Up) ? VisualStyleElement.Spin.Up.Hot : VisualStyleElement.Spin.Up.Normal);
					if (!base.Enabled)
					{
						visualStyleRenderer.SetParameters(VisualStyleElement.Spin.Up.Disabled);
					}
					else if (this.pushed == UpDownBase.ButtonID.Up)
					{
						visualStyleRenderer.SetParameters(VisualStyleElement.Spin.Up.Pressed);
					}
					visualStyleRenderer.DrawBackground(e.Graphics, new Rectangle(0, 0, this.parent.defaultButtonsWidth, num), base.HandleInternal);
					if (!base.Enabled)
					{
						visualStyleRenderer.SetParameters(VisualStyleElement.Spin.Down.Disabled);
					}
					else if (this.pushed == UpDownBase.ButtonID.Down)
					{
						visualStyleRenderer.SetParameters(VisualStyleElement.Spin.Down.Pressed);
					}
					else
					{
						visualStyleRenderer.SetParameters((this.mouseOver == UpDownBase.ButtonID.Down) ? VisualStyleElement.Spin.Down.Hot : VisualStyleElement.Spin.Down.Normal);
					}
					visualStyleRenderer.DrawBackground(e.Graphics, new Rectangle(0, num, this.parent.defaultButtonsWidth, num), base.HandleInternal);
				}
				else
				{
					ControlPaint.DrawScrollButton(e.Graphics, new Rectangle(0, 0, this.parent.defaultButtonsWidth, num), ScrollButton.Up, (this.pushed == UpDownBase.ButtonID.Up) ? ButtonState.Pushed : (base.Enabled ? ButtonState.Normal : ButtonState.Inactive));
					ControlPaint.DrawScrollButton(e.Graphics, new Rectangle(0, num, this.parent.defaultButtonsWidth, num), ScrollButton.Down, (this.pushed == UpDownBase.ButtonID.Down) ? ButtonState.Pushed : (base.Enabled ? ButtonState.Normal : ButtonState.Inactive));
				}
				if (num != (base.ClientSize.Height + 1) / 2)
				{
					using (Pen pen = new Pen(this.parent.BackColor))
					{
						Rectangle clientRectangle = base.ClientRectangle;
						e.Graphics.DrawLine(pen, clientRectangle.Left, clientRectangle.Bottom - 1, clientRectangle.Right - 1, clientRectangle.Bottom - 1);
					}
				}
				base.OnPaint(e);
			}

			// Token: 0x060055BC RID: 21948 RVA: 0x00167664 File Offset: 0x00165864
			protected virtual void OnUpDown(UpDownEventArgs upevent)
			{
				if (this.upDownEventHandler != null)
				{
					this.upDownEventHandler(this, upevent);
				}
			}

			// Token: 0x060055BD RID: 21949 RVA: 0x0016767C File Offset: 0x0016587C
			protected void StartTimer()
			{
				this.parent.OnStartTimer();
				if (this.timer == null)
				{
					this.timer = new Timer();
					this.timer.Tick += this.TimerHandler;
				}
				this.timerInterval = 500;
				this.timer.Interval = this.timerInterval;
				this.timer.Start();
			}

			// Token: 0x060055BE RID: 21950 RVA: 0x001676E5 File Offset: 0x001658E5
			protected void StopTimer()
			{
				if (this.timer != null)
				{
					this.timer.Stop();
					this.timer.Dispose();
					this.timer = null;
				}
				this.parent.OnStopTimer();
			}

			// Token: 0x060055BF RID: 21951 RVA: 0x00167718 File Offset: 0x00165918
			private void TimerHandler(object source, EventArgs args)
			{
				if (!base.Capture)
				{
					this.EndButtonPress();
					return;
				}
				this.OnUpDown(new UpDownEventArgs((int)this.pushed));
				if (this.timer != null)
				{
					this.timerInterval *= 7;
					this.timerInterval /= 10;
					if (this.timerInterval < 1)
					{
						this.timerInterval = 1;
					}
					this.timer.Interval = this.timerInterval;
				}
			}

			// Token: 0x04003826 RID: 14374
			private UpDownBase parent;

			// Token: 0x04003827 RID: 14375
			private UpDownBase.ButtonID pushed;

			// Token: 0x04003828 RID: 14376
			private UpDownBase.ButtonID captured;

			// Token: 0x04003829 RID: 14377
			private UpDownBase.ButtonID mouseOver;

			// Token: 0x0400382A RID: 14378
			private UpDownEventHandler upDownEventHandler;

			// Token: 0x0400382B RID: 14379
			private Timer timer;

			// Token: 0x0400382C RID: 14380
			private int timerInterval;

			// Token: 0x0400382D RID: 14381
			private bool doubleClickFired;

			// Token: 0x020008A6 RID: 2214
			internal class UpDownButtonsAccessibleObject : Control.ControlAccessibleObject
			{
				// Token: 0x0600722C RID: 29228 RVA: 0x0009B733 File Offset: 0x00099933
				public UpDownButtonsAccessibleObject(UpDownBase.UpDownButtons owner)
					: base(owner)
				{
				}

				// Token: 0x17001916 RID: 6422
				// (get) Token: 0x0600722D RID: 29229 RVA: 0x001A23C8 File Offset: 0x001A05C8
				// (set) Token: 0x0600722E RID: 29230 RVA: 0x00010E62 File Offset: 0x0000F062
				public override string Name
				{
					get
					{
						string name = base.Name;
						if (name != null && name.Length != 0)
						{
							return name;
						}
						if (AccessibilityImprovements.Level3)
						{
							return base.Owner.ParentInternal.GetType().Name;
						}
						return SR.GetString("SpinnerAccessibleName");
					}
					set
					{
						base.Name = value;
					}
				}

				// Token: 0x17001917 RID: 6423
				// (get) Token: 0x0600722F RID: 29231 RVA: 0x001A2410 File Offset: 0x001A0610
				public override AccessibleRole Role
				{
					get
					{
						AccessibleRole accessibleRole = base.Owner.AccessibleRole;
						if (accessibleRole != AccessibleRole.Default)
						{
							return accessibleRole;
						}
						return AccessibleRole.SpinButton;
					}
				}

				// Token: 0x17001918 RID: 6424
				// (get) Token: 0x06007230 RID: 29232 RVA: 0x001A2431 File Offset: 0x001A0631
				private UpDownBase.UpDownButtons.UpDownButtonsAccessibleObject.DirectionButtonAccessibleObject UpButton
				{
					get
					{
						if (this.upButton == null)
						{
							this.upButton = new UpDownBase.UpDownButtons.UpDownButtonsAccessibleObject.DirectionButtonAccessibleObject(this, true);
						}
						return this.upButton;
					}
				}

				// Token: 0x17001919 RID: 6425
				// (get) Token: 0x06007231 RID: 29233 RVA: 0x001A244E File Offset: 0x001A064E
				private UpDownBase.UpDownButtons.UpDownButtonsAccessibleObject.DirectionButtonAccessibleObject DownButton
				{
					get
					{
						if (this.downButton == null)
						{
							this.downButton = new UpDownBase.UpDownButtons.UpDownButtonsAccessibleObject.DirectionButtonAccessibleObject(this, false);
						}
						return this.downButton;
					}
				}

				// Token: 0x06007232 RID: 29234 RVA: 0x001A246B File Offset: 0x001A066B
				public override AccessibleObject GetChild(int index)
				{
					if (index == 0)
					{
						return this.UpButton;
					}
					if (index == 1)
					{
						return this.DownButton;
					}
					return null;
				}

				// Token: 0x06007233 RID: 29235 RVA: 0x00016041 File Offset: 0x00014241
				public override int GetChildCount()
				{
					return 2;
				}

				// Token: 0x040044D8 RID: 17624
				private UpDownBase.UpDownButtons.UpDownButtonsAccessibleObject.DirectionButtonAccessibleObject upButton;

				// Token: 0x040044D9 RID: 17625
				private UpDownBase.UpDownButtons.UpDownButtonsAccessibleObject.DirectionButtonAccessibleObject downButton;

				// Token: 0x02000981 RID: 2433
				internal class DirectionButtonAccessibleObject : AccessibleObject
				{
					// Token: 0x06007568 RID: 30056 RVA: 0x001A8088 File Offset: 0x001A6288
					public DirectionButtonAccessibleObject(UpDownBase.UpDownButtons.UpDownButtonsAccessibleObject parent, bool up)
					{
						this.parent = parent;
						this.up = up;
					}

					// Token: 0x17001AFE RID: 6910
					// (get) Token: 0x06007569 RID: 30057 RVA: 0x001A80A0 File Offset: 0x001A62A0
					public override Rectangle Bounds
					{
						get
						{
							Rectangle bounds = ((UpDownBase.UpDownButtons)this.parent.Owner).Bounds;
							bounds.Height /= 2;
							if (!this.up)
							{
								bounds.Y += bounds.Height;
							}
							return ((UpDownBase.UpDownButtons)this.parent.Owner).ParentInternal.RectangleToScreen(bounds);
						}
					}

					// Token: 0x17001AFF RID: 6911
					// (get) Token: 0x0600756A RID: 30058 RVA: 0x001A810A File Offset: 0x001A630A
					// (set) Token: 0x0600756B RID: 30059 RVA: 0x000070A6 File Offset: 0x000052A6
					public override string Name
					{
						get
						{
							if (this.up)
							{
								return SR.GetString("UpDownBaseUpButtonAccName");
							}
							return SR.GetString("UpDownBaseDownButtonAccName");
						}
						set
						{
						}
					}

					// Token: 0x17001B00 RID: 6912
					// (get) Token: 0x0600756C RID: 30060 RVA: 0x001A8129 File Offset: 0x001A6329
					public override AccessibleObject Parent
					{
						[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
						get
						{
							return this.parent;
						}
					}

					// Token: 0x17001B01 RID: 6913
					// (get) Token: 0x0600756D RID: 30061 RVA: 0x0015EE45 File Offset: 0x0015D045
					public override AccessibleRole Role
					{
						get
						{
							return AccessibleRole.PushButton;
						}
					}

					// Token: 0x040047D4 RID: 18388
					private bool up;

					// Token: 0x040047D5 RID: 18389
					private UpDownBase.UpDownButtons.UpDownButtonsAccessibleObject parent;
				}
			}
		}

		// Token: 0x02000558 RID: 1368
		internal enum ButtonID
		{
			// Token: 0x0400382F RID: 14383
			None,
			// Token: 0x04003830 RID: 14384
			Up,
			// Token: 0x04003831 RID: 14385
			Down
		}
	}
}
