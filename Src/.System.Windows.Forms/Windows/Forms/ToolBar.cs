using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows toolbar. Although <see cref="T:System.Windows.Forms.ToolStrip" /> replaces and adds functionality to the <see cref="T:System.Windows.Forms.ToolBar" /> control of previous versions, <see cref="T:System.Windows.Forms.ToolBar" /> is retained for both backward compatibility and future use if you choose.</summary>
	// Token: 0x020003A7 RID: 935
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultEvent("ButtonClick")]
	[Designer("System.Windows.Forms.Design.ToolBarDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("Buttons")]
	public class ToolBar : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolBar" /> class.</summary>
		// Token: 0x06003D00 RID: 15616 RVA: 0x00109308 File Offset: 0x00107508
		public ToolBar()
		{
			this.toolBarState = new BitVector32(31);
			base.SetStyle(ControlStyles.UserPaint, false);
			base.SetStyle(ControlStyles.FixedHeight, this.AutoSize);
			base.SetStyle(ControlStyles.FixedWidth, false);
			this.TabStop = false;
			this.Dock = DockStyle.Top;
			this.buttonsCollection = new ToolBar.ToolBarButtonCollection(this);
		}

		/// <summary>Gets or set the value that determines the appearance of a toolbar control and its buttons.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolBarAppearance" /> values. The default is <see langword="ToolBarAppearance.Normal" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.ToolBarAppearance" /> values.</exception>
		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x06003D01 RID: 15617 RVA: 0x00109390 File Offset: 0x00107590
		// (set) Token: 0x06003D02 RID: 15618 RVA: 0x00109398 File Offset: 0x00107598
		[SRCategory("CatBehavior")]
		[DefaultValue(ToolBarAppearance.Normal)]
		[Localizable(true)]
		[SRDescription("ToolBarAppearanceDescr")]
		public ToolBarAppearance Appearance
		{
			get
			{
				return this.appearance;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 1))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ToolBarAppearance));
				}
				if (value != this.appearance)
				{
					this.appearance = value;
					base.RecreateHandle();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the toolbar adjusts its size automatically, based on the size of the buttons and the dock style.</summary>
		/// <returns>
		///   <see langword="true" /> if the toolbar adjusts its size automatically, based on the size of the buttons and dock style; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x06003D03 RID: 15619 RVA: 0x001093D6 File Offset: 0x001075D6
		// (set) Token: 0x06003D04 RID: 15620 RVA: 0x001093E8 File Offset: 0x001075E8
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[Localizable(true)]
		[SRDescription("ToolBarAutoSizeDescr")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override bool AutoSize
		{
			get
			{
				return this.toolBarState[16];
			}
			set
			{
				if (this.AutoSize != value)
				{
					this.toolBarState[16] = value;
					if (this.Dock == DockStyle.Left || this.Dock == DockStyle.Right)
					{
						base.SetStyle(ControlStyles.FixedWidth, this.AutoSize);
						base.SetStyle(ControlStyles.FixedHeight, false);
					}
					else
					{
						base.SetStyle(ControlStyles.FixedHeight, this.AutoSize);
						base.SetStyle(ControlStyles.FixedWidth, false);
					}
					this.AdjustSize(this.Dock);
					this.OnAutoSizeChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolBar.AutoSize" /> property has changed.</summary>
		// Token: 0x140002EA RID: 746
		// (add) Token: 0x06003D05 RID: 15621 RVA: 0x00011842 File Offset: 0x0000FA42
		// (remove) Token: 0x06003D06 RID: 15622 RVA: 0x0001184B File Offset: 0x0000FA4B
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

		/// <summary>Gets or sets the background color.</summary>
		/// <returns>The background color.</returns>
		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x06003D07 RID: 15623 RVA: 0x0001A049 File Offset: 0x00018249
		// (set) Token: 0x06003D08 RID: 15624 RVA: 0x00012D84 File Offset: 0x00010F84
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolBar.BackColor" /> property changes.</summary>
		// Token: 0x140002EB RID: 747
		// (add) Token: 0x06003D09 RID: 15625 RVA: 0x00058BF2 File Offset: 0x00056DF2
		// (remove) Token: 0x06003D0A RID: 15626 RVA: 0x00058BFB File Offset: 0x00056DFB
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackColorChanged
		{
			add
			{
				base.BackColorChanged += value;
			}
			remove
			{
				base.BackColorChanged -= value;
			}
		}

		/// <summary>Gets or sets the background image.</summary>
		/// <returns>The background image.</returns>
		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x06003D0B RID: 15627 RVA: 0x0001187C File Offset: 0x0000FA7C
		// (set) Token: 0x06003D0C RID: 15628 RVA: 0x00011884 File Offset: 0x0000FA84
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolBar.BackgroundImage" /> property changes.</summary>
		// Token: 0x140002EC RID: 748
		// (add) Token: 0x06003D0D RID: 15629 RVA: 0x0001188D File Offset: 0x0000FA8D
		// (remove) Token: 0x06003D0E RID: 15630 RVA: 0x00011896 File Offset: 0x0000FA96
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

		/// <summary>Gets or sets the layout for background image.</summary>
		/// <returns>The layout for background image.</returns>
		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x06003D0F RID: 15631 RVA: 0x0001189F File Offset: 0x0000FA9F
		// (set) Token: 0x06003D10 RID: 15632 RVA: 0x000118A7 File Offset: 0x0000FAA7
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolBar.BackgroundImageLayout" /> property changes.</summary>
		// Token: 0x140002ED RID: 749
		// (add) Token: 0x06003D11 RID: 15633 RVA: 0x000118B0 File Offset: 0x0000FAB0
		// (remove) Token: 0x06003D12 RID: 15634 RVA: 0x000118B9 File Offset: 0x0000FAB9
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

		/// <summary>Gets or sets the border style of the toolbar control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. The default is <see langword="BorderStyle.None" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.BorderStyle" /> values.</exception>
		// Token: 0x17000EE0 RID: 3808
		// (get) Token: 0x06003D13 RID: 15635 RVA: 0x00109465 File Offset: 0x00107665
		// (set) Token: 0x06003D14 RID: 15636 RVA: 0x0010946D File Offset: 0x0010766D
		[SRCategory("CatAppearance")]
		[DefaultValue(BorderStyle.None)]
		[DispId(-504)]
		[SRDescription("ToolBarBorderStyleDescr")]
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

		/// <summary>Gets the collection of <see cref="T:System.Windows.Forms.ToolBarButton" /> controls assigned to the toolbar control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolBar.ToolBarButtonCollection" /> that contains a collection of <see cref="T:System.Windows.Forms.ToolBarButton" /> controls.</returns>
		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x06003D15 RID: 15637 RVA: 0x001094AB File Offset: 0x001076AB
		[SRCategory("CatBehavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Localizable(true)]
		[SRDescription("ToolBarButtonsDescr")]
		[MergableProperty(false)]
		public ToolBar.ToolBarButtonCollection Buttons
		{
			get
			{
				return this.buttonsCollection;
			}
		}

		/// <summary>Gets or sets the size of the buttons on the toolbar control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> object that represents the size of the <see cref="T:System.Windows.Forms.ToolBarButton" /> controls on the toolbar. The default size has a width of 24 pixels and a height of 22 pixels, or large enough to accommodate the <see cref="T:System.Drawing.Image" /> and text, whichever is greater.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Size.Width" /> or <see cref="P:System.Drawing.Size.Height" /> property of the <see cref="T:System.Drawing.Size" /> object is less than 0.</exception>
		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x06003D16 RID: 15638 RVA: 0x001094B4 File Offset: 0x001076B4
		// (set) Token: 0x06003D17 RID: 15639 RVA: 0x00109534 File Offset: 0x00107734
		[SRCategory("CatAppearance")]
		[RefreshProperties(RefreshProperties.All)]
		[Localizable(true)]
		[SRDescription("ToolBarButtonSizeDescr")]
		public Size ButtonSize
		{
			get
			{
				if (!this.buttonSize.IsEmpty)
				{
					return this.buttonSize;
				}
				if (base.IsHandleCreated && this.buttons != null && this.buttonCount > 0)
				{
					int num = (int)(long)base.SendMessage(1082, 0, 0);
					if (num > 0)
					{
						return new Size(NativeMethods.Util.LOWORD(num), NativeMethods.Util.HIWORD(num));
					}
				}
				if (this.TextAlign == ToolBarTextAlign.Underneath)
				{
					return new Size(39, 36);
				}
				return new Size(23, 22);
			}
			set
			{
				if (value.Width < 0 || value.Height < 0)
				{
					throw new ArgumentOutOfRangeException("ButtonSize", SR.GetString("InvalidArgument", new object[]
					{
						"ButtonSize",
						value.ToString()
					}));
				}
				if (this.buttonSize != value)
				{
					this.buttonSize = value;
					this.maxWidth = -1;
					base.RecreateHandle();
					this.AdjustSize(this.Dock);
				}
			}
		}

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x06003D18 RID: 15640 RVA: 0x001095B8 File Offset: 0x001077B8
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ClassName = "ToolbarWindow32";
				createParams.Style |= 12;
				if (!this.Divider)
				{
					createParams.Style |= 64;
				}
				if (this.Wrappable)
				{
					createParams.Style |= 512;
				}
				if (this.ShowToolTips && !base.DesignMode)
				{
					createParams.Style |= 256;
				}
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
				ToolBarAppearance toolBarAppearance = this.appearance;
				if (toolBarAppearance != ToolBarAppearance.Normal && toolBarAppearance == ToolBarAppearance.Flat)
				{
					createParams.Style |= 2048;
				}
				ToolBarTextAlign toolBarTextAlign = this.textAlign;
				if (toolBarTextAlign != ToolBarTextAlign.Underneath && toolBarTextAlign == ToolBarTextAlign.Right)
				{
					createParams.Style |= 4096;
				}
				return createParams;
			}
		}

		/// <summary>Gets the default Input Method Editor (IME) mode supported by this control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x06003D19 RID: 15641 RVA: 0x00023BD7 File Offset: 0x00021DD7
		protected override ImeMode DefaultImeMode
		{
			get
			{
				return ImeMode.Disable;
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x06003D1A RID: 15642 RVA: 0x00010FC8 File Offset: 0x0000F1C8
		protected override Size DefaultSize
		{
			get
			{
				return new Size(100, 22);
			}
		}

		/// <summary>Gets or sets a value indicating whether the toolbar displays a divider.</summary>
		/// <returns>
		///   <see langword="true" /> if the toolbar displays a divider; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x06003D1B RID: 15643 RVA: 0x001096D3 File Offset: 0x001078D3
		// (set) Token: 0x06003D1C RID: 15644 RVA: 0x001096E1 File Offset: 0x001078E1
		[SRCategory("CatAppearance")]
		[DefaultValue(true)]
		[SRDescription("ToolBarDividerDescr")]
		public bool Divider
		{
			get
			{
				return this.toolBarState[4];
			}
			set
			{
				if (this.Divider != value)
				{
					this.toolBarState[4] = value;
					base.RecreateHandle();
				}
			}
		}

		/// <summary>Gets or sets which control borders are docked to its parent control and determines how a control is resized with its parent.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values. The default is <see cref="F:System.Windows.Forms.DockStyle.None" />.</returns>
		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x06003D1D RID: 15645 RVA: 0x000FC41E File Offset: 0x000FA61E
		// (set) Token: 0x06003D1E RID: 15646 RVA: 0x00109700 File Offset: 0x00107900
		[Localizable(true)]
		[DefaultValue(DockStyle.Top)]
		public override DockStyle Dock
		{
			get
			{
				return base.Dock;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 5))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DockStyle));
				}
				if (this.Dock != value)
				{
					if (value == DockStyle.Left || value == DockStyle.Right)
					{
						base.SetStyle(ControlStyles.FixedWidth, this.AutoSize);
						base.SetStyle(ControlStyles.FixedHeight, false);
					}
					else
					{
						base.SetStyle(ControlStyles.FixedHeight, this.AutoSize);
						base.SetStyle(ControlStyles.FixedWidth, false);
					}
					this.AdjustSize(value);
					base.Dock = value;
				}
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value.</returns>
		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x06003D1F RID: 15647 RVA: 0x00012FC3 File Offset: 0x000111C3
		// (set) Token: 0x06003D20 RID: 15648 RVA: 0x00012FCB File Offset: 0x000111CB
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override bool DoubleBuffered
		{
			get
			{
				return base.DoubleBuffered;
			}
			set
			{
				base.DoubleBuffered = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether drop-down buttons on a toolbar display down arrows.</summary>
		/// <returns>
		///   <see langword="true" /> if drop-down toolbar buttons display down arrows; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x06003D21 RID: 15649 RVA: 0x00109782 File Offset: 0x00107982
		// (set) Token: 0x06003D22 RID: 15650 RVA: 0x00109790 File Offset: 0x00107990
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[SRDescription("ToolBarDropDownArrowsDescr")]
		public bool DropDownArrows
		{
			get
			{
				return this.toolBarState[2];
			}
			set
			{
				if (this.DropDownArrows != value)
				{
					this.toolBarState[2] = value;
					base.RecreateHandle();
				}
			}
		}

		/// <summary>Gets or sets the forecolor .</summary>
		/// <returns>The forecolor.</returns>
		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x06003D23 RID: 15651 RVA: 0x0001A0E7 File Offset: 0x000182E7
		// (set) Token: 0x06003D24 RID: 15652 RVA: 0x00013024 File Offset: 0x00011224
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolBar.ForeColor" /> property changes.</summary>
		// Token: 0x140002EE RID: 750
		// (add) Token: 0x06003D25 RID: 15653 RVA: 0x0005A8EE File Offset: 0x00058AEE
		// (remove) Token: 0x06003D26 RID: 15654 RVA: 0x0005A8F7 File Offset: 0x00058AF7
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ForeColorChanged
		{
			add
			{
				base.ForeColorChanged += value;
			}
			remove
			{
				base.ForeColorChanged -= value;
			}
		}

		/// <summary>Gets or sets the collection of images available to the toolbar button controls.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageList" /> that contains images available to the <see cref="T:System.Windows.Forms.ToolBarButton" /> controls. The default is <see langword="null" />.</returns>
		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x06003D27 RID: 15655 RVA: 0x001097AE File Offset: 0x001079AE
		// (set) Token: 0x06003D28 RID: 15656 RVA: 0x001097B8 File Offset: 0x001079B8
		[SRCategory("CatBehavior")]
		[DefaultValue(null)]
		[SRDescription("ToolBarImageListDescr")]
		public ImageList ImageList
		{
			get
			{
				return this.imageList;
			}
			set
			{
				if (value != this.imageList)
				{
					EventHandler eventHandler = new EventHandler(this.ImageListRecreateHandle);
					EventHandler eventHandler2 = new EventHandler(this.DetachImageList);
					if (this.imageList != null)
					{
						this.imageList.Disposed -= eventHandler2;
						this.imageList.RecreateHandle -= eventHandler;
					}
					this.imageList = value;
					if (value != null)
					{
						value.Disposed += eventHandler2;
						value.RecreateHandle += eventHandler;
					}
					if (base.IsHandleCreated)
					{
						base.RecreateHandle();
					}
				}
			}
		}

		/// <summary>Gets the size of the images in the image list assigned to the toolbar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the size of the images (in the <see cref="T:System.Windows.Forms.ImageList" />) assigned to the <see cref="T:System.Windows.Forms.ToolBar" />.</returns>
		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x06003D29 RID: 15657 RVA: 0x0010982E File Offset: 0x00107A2E
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ToolBarImageSizeDescr")]
		public Size ImageSize
		{
			get
			{
				if (this.imageList != null)
				{
					return this.imageList.ImageSize;
				}
				return new Size(0, 0);
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x06003D2A RID: 15658 RVA: 0x0001A051 File Offset: 0x00018251
		// (set) Token: 0x06003D2B RID: 15659 RVA: 0x0001A059 File Offset: 0x00018259
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new ImeMode ImeMode
		{
			get
			{
				return base.ImeMode;
			}
			set
			{
				base.ImeMode = value;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolBar.ImeMode" /> property changes.</summary>
		// Token: 0x140002EF RID: 751
		// (add) Token: 0x06003D2C RID: 15660 RVA: 0x00023F70 File Offset: 0x00022170
		// (remove) Token: 0x06003D2D RID: 15661 RVA: 0x00023F79 File Offset: 0x00022179
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ImeModeChanged
		{
			add
			{
				base.ImeModeChanged += value;
			}
			remove
			{
				base.ImeModeChanged -= value;
			}
		}

		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x06003D2E RID: 15662 RVA: 0x0010984C File Offset: 0x00107A4C
		internal int PreferredHeight
		{
			get
			{
				int num;
				if (this.buttons == null || this.buttonCount == 0 || !base.IsHandleCreated)
				{
					num = this.ButtonSize.Height;
				}
				else
				{
					NativeMethods.RECT rect = default(NativeMethods.RECT);
					int num2 = 0;
					while (num2 < this.buttons.Length && (this.buttons[num2] == null || !this.buttons[num2].Visible))
					{
						num2++;
					}
					if (num2 == this.buttons.Length)
					{
						num2 = 0;
					}
					base.SendMessage(1075, num2, ref rect);
					num = rect.bottom - rect.top;
				}
				if (this.Wrappable && base.IsHandleCreated)
				{
					num *= (int)(long)base.SendMessage(1064, 0, 0);
				}
				num = ((num > 0) ? num : 1);
				BorderStyle borderStyle = this.borderStyle;
				if (borderStyle != BorderStyle.FixedSingle)
				{
					if (borderStyle == BorderStyle.Fixed3D)
					{
						num += SystemInformation.Border3DSize.Height;
					}
				}
				else
				{
					num += SystemInformation.BorderSize.Height;
				}
				if (this.Divider)
				{
					num += 2;
				}
				return num + 4;
			}
		}

		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x06003D2F RID: 15663 RVA: 0x00109958 File Offset: 0x00107B58
		internal int PreferredWidth
		{
			get
			{
				if (this.maxWidth == -1)
				{
					if (!base.IsHandleCreated || this.buttons == null)
					{
						this.maxWidth = this.ButtonSize.Width;
					}
					else
					{
						NativeMethods.RECT rect = default(NativeMethods.RECT);
						for (int i = 0; i < this.buttonCount; i++)
						{
							base.SendMessage(1075, 0, ref rect);
							if (rect.right - rect.left > this.maxWidth)
							{
								this.maxWidth = rect.right - rect.left;
							}
						}
					}
				}
				int num = this.maxWidth;
				if (this.borderStyle != BorderStyle.None)
				{
					num += SystemInformation.BorderSize.Height * 4 + 3;
				}
				return num;
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.RightToLeft" /> value.</returns>
		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x06003D30 RID: 15664 RVA: 0x000E322B File Offset: 0x000E142B
		// (set) Token: 0x06003D31 RID: 15665 RVA: 0x000C5F21 File Offset: 0x000C4121
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override RightToLeft RightToLeft
		{
			get
			{
				return base.RightToLeft;
			}
			set
			{
				base.RightToLeft = value;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolBar.RightToLeft" /> property changes.</summary>
		// Token: 0x140002F0 RID: 752
		// (add) Token: 0x06003D32 RID: 15666 RVA: 0x000E3233 File Offset: 0x000E1433
		// (remove) Token: 0x06003D33 RID: 15667 RVA: 0x000E323C File Offset: 0x000E143C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler RightToLeftChanged
		{
			add
			{
				base.RightToLeftChanged += value;
			}
			remove
			{
				base.RightToLeftChanged -= value;
			}
		}

		/// <summary>This method is not relevant for this class.</summary>
		/// <param name="dx">The horizontal scaling factor.</param>
		/// <param name="dy">The vertical scaling factor.</param>
		// Token: 0x06003D34 RID: 15668 RVA: 0x00109A08 File Offset: 0x00107C08
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void ScaleCore(float dx, float dy)
		{
			this.currentScaleDX = dx;
			this.currentScaleDY = dy;
			base.ScaleCore(dx, dy);
			this.UpdateButtons();
		}

		/// <summary>Scales a control's location, size, padding and margin.</summary>
		/// <param name="factor">The factor by which the height and width of the control will be scaled.</param>
		/// <param name="specified">A <see cref="T:System.Windows.Forms.BoundsSpecified" /> value that specifies the bounds of the control to use when defining its size and position.</param>
		// Token: 0x06003D35 RID: 15669 RVA: 0x00109A26 File Offset: 0x00107C26
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			this.currentScaleDX = factor.Width;
			this.currentScaleDY = factor.Height;
			base.ScaleControl(factor, specified);
		}

		/// <summary>Gets or sets a value indicating whether the toolbar displays a ToolTip for each button.</summary>
		/// <returns>
		///   <see langword="true" /> if the toolbar display a ToolTip for each button; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x06003D36 RID: 15670 RVA: 0x00109A4A File Offset: 0x00107C4A
		// (set) Token: 0x06003D37 RID: 15671 RVA: 0x00109A58 File Offset: 0x00107C58
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[Localizable(true)]
		[SRDescription("ToolBarShowToolTipsDescr")]
		public bool ShowToolTips
		{
			get
			{
				return this.toolBarState[8];
			}
			set
			{
				if (this.ShowToolTips != value)
				{
					this.toolBarState[8] = value;
					base.RecreateHandle();
				}
			}
		}

		/// <summary>This property is not meaningful for this control.</summary>
		/// <returns>This property is not meaningful for this control.</returns>
		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x06003D38 RID: 15672 RVA: 0x000B2395 File Offset: 0x000B0595
		// (set) Token: 0x06003D39 RID: 15673 RVA: 0x000B239D File Offset: 0x000B059D
		[DefaultValue(false)]
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

		/// <summary>Gets or sets the text for the toolbar.</summary>
		/// <returns>The text for the toolbar.</returns>
		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x06003D3A RID: 15674 RVA: 0x00013814 File Offset: 0x00011A14
		// (set) Token: 0x06003D3B RID: 15675 RVA: 0x00023FE9 File Offset: 0x000221E9
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolBar.Text" /> property changes.</summary>
		// Token: 0x140002F1 RID: 753
		// (add) Token: 0x06003D3C RID: 15676 RVA: 0x00046591 File Offset: 0x00044791
		// (remove) Token: 0x06003D3D RID: 15677 RVA: 0x0004659A File Offset: 0x0004479A
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

		/// <summary>Gets or sets the alignment of text in relation to each image displayed on the toolbar button controls.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolBarTextAlign" /> values. The default is <see langword="ToolBarTextAlign.Underneath" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.ToolBarTextAlign" /> values.</exception>
		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x06003D3E RID: 15678 RVA: 0x00109A76 File Offset: 0x00107C76
		// (set) Token: 0x06003D3F RID: 15679 RVA: 0x00109A7E File Offset: 0x00107C7E
		[SRCategory("CatAppearance")]
		[DefaultValue(ToolBarTextAlign.Underneath)]
		[Localizable(true)]
		[SRDescription("ToolBarTextAlignDescr")]
		public ToolBarTextAlign TextAlign
		{
			get
			{
				return this.textAlign;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 1))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ToolBarTextAlign));
				}
				if (this.textAlign == value)
				{
					return;
				}
				this.textAlign = value;
				base.RecreateHandle();
			}
		}

		/// <summary>Gets or sets a value indicating whether the toolbar buttons wrap to the next line if the toolbar becomes too small to display all the buttons on the same line.</summary>
		/// <returns>
		///   <see langword="true" /> if the toolbar buttons wrap to another line if the toolbar becomes too small to display all the buttons on the same line; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x06003D40 RID: 15680 RVA: 0x00109ABD File Offset: 0x00107CBD
		// (set) Token: 0x06003D41 RID: 15681 RVA: 0x00109ACB File Offset: 0x00107CCB
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[Localizable(true)]
		[SRDescription("ToolBarWrappableDescr")]
		public bool Wrappable
		{
			get
			{
				return this.toolBarState[1];
			}
			set
			{
				if (this.Wrappable != value)
				{
					this.toolBarState[1] = value;
					base.RecreateHandle();
				}
			}
		}

		/// <summary>Occurs when a <see cref="T:System.Windows.Forms.ToolBarButton" /> on the <see cref="T:System.Windows.Forms.ToolBar" /> is clicked.</summary>
		// Token: 0x140002F2 RID: 754
		// (add) Token: 0x06003D42 RID: 15682 RVA: 0x00109AE9 File Offset: 0x00107CE9
		// (remove) Token: 0x06003D43 RID: 15683 RVA: 0x00109B02 File Offset: 0x00107D02
		[SRCategory("CatBehavior")]
		[SRDescription("ToolBarButtonClickDescr")]
		public event ToolBarButtonClickEventHandler ButtonClick
		{
			add
			{
				this.onButtonClick = (ToolBarButtonClickEventHandler)Delegate.Combine(this.onButtonClick, value);
			}
			remove
			{
				this.onButtonClick = (ToolBarButtonClickEventHandler)Delegate.Remove(this.onButtonClick, value);
			}
		}

		/// <summary>Occurs when a drop-down style <see cref="T:System.Windows.Forms.ToolBarButton" /> or its down arrow is clicked.</summary>
		// Token: 0x140002F3 RID: 755
		// (add) Token: 0x06003D44 RID: 15684 RVA: 0x00109B1B File Offset: 0x00107D1B
		// (remove) Token: 0x06003D45 RID: 15685 RVA: 0x00109B34 File Offset: 0x00107D34
		[SRCategory("CatBehavior")]
		[SRDescription("ToolBarButtonDropDownDescr")]
		public event ToolBarButtonClickEventHandler ButtonDropDown
		{
			add
			{
				this.onButtonDropDown = (ToolBarButtonClickEventHandler)Delegate.Combine(this.onButtonDropDown, value);
			}
			remove
			{
				this.onButtonDropDown = (ToolBarButtonClickEventHandler)Delegate.Remove(this.onButtonDropDown, value);
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		// Token: 0x140002F4 RID: 756
		// (add) Token: 0x06003D46 RID: 15686 RVA: 0x00013D73 File Offset: 0x00011F73
		// (remove) Token: 0x06003D47 RID: 15687 RVA: 0x00013D7C File Offset: 0x00011F7C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event PaintEventHandler Paint
		{
			add
			{
				base.Paint += value;
			}
			remove
			{
				base.Paint -= value;
			}
		}

		// Token: 0x06003D48 RID: 15688 RVA: 0x00109B50 File Offset: 0x00107D50
		private void AdjustSize(DockStyle dock)
		{
			int num = this.requestedSize;
			try
			{
				if (dock == DockStyle.Left || dock == DockStyle.Right)
				{
					base.Width = (this.AutoSize ? this.PreferredWidth : num);
				}
				else
				{
					base.Height = (this.AutoSize ? this.PreferredHeight : num);
				}
			}
			finally
			{
				this.requestedSize = num;
			}
		}

		// Token: 0x06003D49 RID: 15689 RVA: 0x00103F69 File Offset: 0x00102169
		internal void BeginUpdate()
		{
			base.BeginUpdateInternal();
		}

		/// <summary>Creates a handle for the control.</summary>
		// Token: 0x06003D4A RID: 15690 RVA: 0x00109BB8 File Offset: 0x00107DB8
		protected override void CreateHandle()
		{
			if (!base.RecreatingHandle)
			{
				IntPtr intPtr = UnsafeNativeMethods.ThemingScope.Activate();
				try
				{
					SafeNativeMethods.InitCommonControlsEx(new NativeMethods.INITCOMMONCONTROLSEX
					{
						dwICC = 4
					});
				}
				finally
				{
					UnsafeNativeMethods.ThemingScope.Deactivate(intPtr);
				}
			}
			base.CreateHandle();
		}

		// Token: 0x06003D4B RID: 15691 RVA: 0x00109C08 File Offset: 0x00107E08
		private void DetachImageList(object sender, EventArgs e)
		{
			this.ImageList = null;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ToolBar" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06003D4C RID: 15692 RVA: 0x00109C14 File Offset: 0x00107E14
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				lock (this)
				{
					bool state = base.GetState(4096);
					try
					{
						base.SetState(4096, true);
						if (this.imageList != null)
						{
							this.imageList.Disposed -= this.DetachImageList;
							this.imageList = null;
						}
						if (this.buttonsCollection != null)
						{
							ToolBarButton[] array = new ToolBarButton[this.buttonsCollection.Count];
							((ICollection)this.buttonsCollection).CopyTo(array, 0);
							this.buttonsCollection.Clear();
							foreach (ToolBarButton toolBarButton in array)
							{
								toolBarButton.Dispose();
							}
						}
					}
					finally
					{
						base.SetState(4096, state);
					}
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06003D4D RID: 15693 RVA: 0x00109D04 File Offset: 0x00107F04
		internal void EndUpdate()
		{
			base.EndUpdateInternal();
		}

		// Token: 0x06003D4E RID: 15694 RVA: 0x00109D10 File Offset: 0x00107F10
		private void ForceButtonWidths()
		{
			if (this.buttons != null && this.buttonSize.IsEmpty && base.IsHandleCreated)
			{
				this.maxWidth = -1;
				for (int i = 0; i < this.buttonCount; i++)
				{
					NativeMethods.TBBUTTONINFO tbbuttoninfo = new NativeMethods.TBBUTTONINFO
					{
						cbSize = Marshal.SizeOf(typeof(NativeMethods.TBBUTTONINFO)),
						cx = this.buttons[i].Width
					};
					if ((int)tbbuttoninfo.cx > this.maxWidth)
					{
						this.maxWidth = (int)tbbuttoninfo.cx;
					}
					tbbuttoninfo.dwMask = 64;
					UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.TB_SETBUTTONINFO, i, ref tbbuttoninfo);
				}
			}
		}

		// Token: 0x06003D4F RID: 15695 RVA: 0x00109DCA File Offset: 0x00107FCA
		private void ImageListRecreateHandle(object sender, EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				base.RecreateHandle();
			}
		}

		// Token: 0x06003D50 RID: 15696 RVA: 0x00109DDC File Offset: 0x00107FDC
		private void Insert(int index, ToolBarButton button)
		{
			button.parent = this;
			if (this.buttons == null)
			{
				this.buttons = new ToolBarButton[4];
			}
			else if (this.buttons.Length == this.buttonCount)
			{
				ToolBarButton[] array = new ToolBarButton[this.buttonCount + 4];
				Array.Copy(this.buttons, 0, array, 0, this.buttonCount);
				this.buttons = array;
			}
			if (index < this.buttonCount)
			{
				Array.Copy(this.buttons, index, this.buttons, index + 1, this.buttonCount - index);
			}
			this.buttons[index] = button;
			this.buttonCount++;
		}

		// Token: 0x06003D51 RID: 15697 RVA: 0x00109E7C File Offset: 0x0010807C
		private void InsertButton(int index, ToolBarButton value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (index < 0 || (this.buttons != null && index > this.buttonCount))
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			this.Insert(index, value);
			if (base.IsHandleCreated)
			{
				NativeMethods.TBBUTTON tbbutton = value.GetTBBUTTON(index);
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.TB_INSERTBUTTON, index, ref tbbutton);
			}
			this.UpdateButtons();
		}

		// Token: 0x06003D52 RID: 15698 RVA: 0x00109F18 File Offset: 0x00108118
		private int InternalAddButton(ToolBarButton button)
		{
			if (button == null)
			{
				throw new ArgumentNullException("button");
			}
			int num = this.buttonCount;
			this.Insert(num, button);
			return num;
		}

		// Token: 0x06003D53 RID: 15699 RVA: 0x00109F44 File Offset: 0x00108144
		internal void InternalSetButton(int index, ToolBarButton value, bool recreate, bool updateText)
		{
			this.buttons[index].parent = null;
			this.buttons[index].stringIndex = (IntPtr)(-1);
			this.buttons[index] = value;
			this.buttons[index].parent = this;
			if (base.IsHandleCreated)
			{
				NativeMethods.TBBUTTONINFO tbbuttoninfo = value.GetTBBUTTONINFO(updateText, index);
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.TB_SETBUTTONINFO, index, ref tbbuttoninfo);
				if (tbbuttoninfo.pszText != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(tbbuttoninfo.pszText);
				}
				if (recreate)
				{
					this.UpdateButtons();
					return;
				}
				base.SendMessage(1057, 0, 0);
				this.ForceButtonWidths();
				base.Invalidate();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolBar.ButtonClick" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolBarButtonClickEventArgs" /> that contains the event data.</param>
		// Token: 0x06003D54 RID: 15700 RVA: 0x00109FF6 File Offset: 0x001081F6
		protected virtual void OnButtonClick(ToolBarButtonClickEventArgs e)
		{
			if (this.onButtonClick != null)
			{
				this.onButtonClick(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolBar.ButtonDropDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolBarButtonClickEventArgs" /> that contains the event data.</param>
		// Token: 0x06003D55 RID: 15701 RVA: 0x0010A00D File Offset: 0x0010820D
		protected virtual void OnButtonDropDown(ToolBarButtonClickEventArgs e)
		{
			if (this.onButtonDropDown != null)
			{
				this.onButtonDropDown(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003D56 RID: 15702 RVA: 0x0010A024 File Offset: 0x00108224
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			base.SendMessage(1054, Marshal.SizeOf(typeof(NativeMethods.TBBUTTON)), 0);
			if (this.DropDownArrows)
			{
				base.SendMessage(1108, 0, 1);
			}
			if (this.imageList != null)
			{
				base.SendMessage(1072, 0, this.imageList.Handle);
			}
			this.RealizeButtons();
			this.BeginUpdate();
			try
			{
				Size size = base.Size;
				base.Size = new Size(size.Width + 1, size.Height);
				base.Size = size;
			}
			finally
			{
				this.EndUpdate();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003D57 RID: 15703 RVA: 0x0010A0D8 File Offset: 0x001082D8
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (this.Wrappable)
			{
				this.AdjustSize(this.Dock);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003D58 RID: 15704 RVA: 0x0010A0F5 File Offset: 0x001082F5
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			if (base.IsHandleCreated)
			{
				if (!this.buttonSize.IsEmpty)
				{
					this.SendToolbarButtonSizeMessage();
					return;
				}
				this.AdjustSize(this.Dock);
				this.ForceButtonWidths();
			}
		}

		// Token: 0x06003D59 RID: 15705 RVA: 0x0010A12C File Offset: 0x0010832C
		private void RealizeButtons()
		{
			if (this.buttons != null)
			{
				IntPtr intPtr = IntPtr.Zero;
				try
				{
					this.BeginUpdate();
					for (int i = 0; i < this.buttonCount; i++)
					{
						if (this.buttons[i].Text.Length > 0)
						{
							string text = this.buttons[i].Text + '\0'.ToString();
							this.buttons[i].stringIndex = base.SendMessage(NativeMethods.TB_ADDSTRING, 0, text);
						}
						else
						{
							this.buttons[i].stringIndex = (IntPtr)(-1);
						}
					}
					int num = Marshal.SizeOf(typeof(NativeMethods.TBBUTTON));
					int num2 = this.buttonCount;
					intPtr = Marshal.AllocHGlobal(checked(num * num2));
					for (int j = 0; j < num2; j++)
					{
						NativeMethods.TBBUTTON tbbutton = this.buttons[j].GetTBBUTTON(j);
						checked
						{
							Marshal.StructureToPtr(tbbutton, (IntPtr)((long)intPtr + unchecked((long)(checked(num * j)))), true);
							this.buttons[j].parent = this;
						}
					}
					base.SendMessage(NativeMethods.TB_ADDBUTTONS, num2, intPtr);
					base.SendMessage(1057, 0, 0);
					if (!this.buttonSize.IsEmpty)
					{
						this.SendToolbarButtonSizeMessage();
					}
					else
					{
						this.ForceButtonWidths();
					}
					this.AdjustSize(this.Dock);
				}
				finally
				{
					Marshal.FreeHGlobal(intPtr);
					this.EndUpdate();
				}
			}
		}

		// Token: 0x06003D5A RID: 15706 RVA: 0x0010A2A4 File Offset: 0x001084A4
		private void RemoveAt(int index)
		{
			this.buttons[index].parent = null;
			this.buttons[index].stringIndex = (IntPtr)(-1);
			this.buttonCount--;
			if (index < this.buttonCount)
			{
				Array.Copy(this.buttons, index + 1, this.buttons, index, this.buttonCount - index);
			}
			this.buttons[this.buttonCount] = null;
		}

		// Token: 0x06003D5B RID: 15707 RVA: 0x0010A314 File Offset: 0x00108514
		private void ResetButtonSize()
		{
			this.buttonSize = Size.Empty;
			base.RecreateHandle();
		}

		// Token: 0x06003D5C RID: 15708 RVA: 0x0010A327 File Offset: 0x00108527
		private void SendToolbarButtonSizeMessage()
		{
			base.SendMessage(1055, 0, NativeMethods.Util.MAKELPARAM((int)((float)this.buttonSize.Width * this.currentScaleDX), (int)((float)this.buttonSize.Height * this.currentScaleDY)));
		}

		/// <summary>Sets the specified bounds of the <see cref="T:System.Windows.Forms.ToolBar" /> control.</summary>
		/// <param name="x">The new <see langword="Left" /> property value of the control.</param>
		/// <param name="y">The new <see langword="Top" /> property value of the control.</param>
		/// <param name="width">The new <see langword="Width" /> property value of the control.</param>
		/// <param name="height">Not used.</param>
		/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values.</param>
		// Token: 0x06003D5D RID: 15709 RVA: 0x0010A364 File Offset: 0x00108564
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			int num = height;
			int num2 = width;
			base.SetBoundsCore(x, y, width, height, specified);
			Rectangle bounds = base.Bounds;
			if (this.Dock == DockStyle.Left || this.Dock == DockStyle.Right)
			{
				if ((specified & BoundsSpecified.Width) != BoundsSpecified.None)
				{
					this.requestedSize = width;
				}
				if (this.AutoSize)
				{
					width = this.PreferredWidth;
				}
				if (width != num2 && this.Dock == DockStyle.Right)
				{
					int num3 = num2 - width;
					x += num3;
				}
			}
			else
			{
				if ((specified & BoundsSpecified.Height) != BoundsSpecified.None)
				{
					this.requestedSize = height;
				}
				if (this.AutoSize)
				{
					height = this.PreferredHeight;
				}
				if (height != num && this.Dock == DockStyle.Bottom)
				{
					int num4 = num - height;
					y += num4;
				}
			}
			base.SetBoundsCore(x, y, width, height, specified);
		}

		// Token: 0x06003D5E RID: 15710 RVA: 0x0010A416 File Offset: 0x00108616
		private bool ShouldSerializeButtonSize()
		{
			return !this.buttonSize.IsEmpty;
		}

		// Token: 0x06003D5F RID: 15711 RVA: 0x0010A426 File Offset: 0x00108626
		internal void SetToolTip(ToolTip toolTip)
		{
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1060, new HandleRef(toolTip, toolTip.Handle), 0);
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.ToolBar" /> control.</summary>
		/// <returns>A String that represents the current <see cref="T:System.Windows.Forms.ToolBar" />.</returns>
		// Token: 0x06003D60 RID: 15712 RVA: 0x0010A44C File Offset: 0x0010864C
		public override string ToString()
		{
			string text = base.ToString();
			text = text + ", Buttons.Count: " + this.buttonCount.ToString(CultureInfo.CurrentCulture);
			if (this.buttonCount > 0)
			{
				text = text + ", Buttons[0]: " + this.buttons[0].ToString();
			}
			return text;
		}

		// Token: 0x06003D61 RID: 15713 RVA: 0x00109DCA File Offset: 0x00107FCA
		internal void UpdateButtons()
		{
			if (base.IsHandleCreated)
			{
				base.RecreateHandle();
			}
		}

		// Token: 0x06003D62 RID: 15714 RVA: 0x0010A4A0 File Offset: 0x001086A0
		private void WmNotifyDropDown(ref Message m)
		{
			NativeMethods.NMTOOLBAR nmtoolbar = (NativeMethods.NMTOOLBAR)m.GetLParam(typeof(NativeMethods.NMTOOLBAR));
			ToolBarButton toolBarButton = this.buttons[nmtoolbar.iItem];
			if (toolBarButton == null)
			{
				throw new InvalidOperationException(SR.GetString("ToolBarButtonNotFound"));
			}
			this.OnButtonDropDown(new ToolBarButtonClickEventArgs(toolBarButton));
			Menu dropDownMenu = toolBarButton.DropDownMenu;
			if (dropDownMenu != null)
			{
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				NativeMethods.TPMPARAMS tpmparams = new NativeMethods.TPMPARAMS();
				base.SendMessage(1075, nmtoolbar.iItem, ref rect);
				if (dropDownMenu.GetType().IsAssignableFrom(typeof(ContextMenu)))
				{
					((ContextMenu)dropDownMenu).Show(this, new Point(rect.left, rect.bottom));
					return;
				}
				Menu mainMenu = dropDownMenu.GetMainMenu();
				if (mainMenu != null)
				{
					mainMenu.ProcessInitMenuPopup(dropDownMenu.Handle);
				}
				UnsafeNativeMethods.MapWindowPoints(new HandleRef(nmtoolbar.hdr, nmtoolbar.hdr.hwndFrom), NativeMethods.NullHandleRef, ref rect, 2);
				tpmparams.rcExclude_left = rect.left;
				tpmparams.rcExclude_top = rect.top;
				tpmparams.rcExclude_right = rect.right;
				tpmparams.rcExclude_bottom = rect.bottom;
				SafeNativeMethods.TrackPopupMenuEx(new HandleRef(dropDownMenu, dropDownMenu.Handle), 64, rect.left, rect.bottom, new HandleRef(this, base.Handle), tpmparams);
			}
		}

		// Token: 0x06003D63 RID: 15715 RVA: 0x0010A5FC File Offset: 0x001087FC
		private void WmNotifyNeedText(ref Message m)
		{
			NativeMethods.TOOLTIPTEXT tooltiptext = (NativeMethods.TOOLTIPTEXT)m.GetLParam(typeof(NativeMethods.TOOLTIPTEXT));
			int num = (int)tooltiptext.hdr.idFrom;
			ToolBarButton toolBarButton = this.buttons[num];
			if (toolBarButton != null && toolBarButton.ToolTipText != null)
			{
				tooltiptext.lpszText = toolBarButton.ToolTipText;
			}
			else
			{
				tooltiptext.lpszText = null;
			}
			tooltiptext.hinst = IntPtr.Zero;
			if (this.RightToLeft == RightToLeft.Yes)
			{
				tooltiptext.uFlags |= 4;
			}
			Marshal.StructureToPtr(tooltiptext, m.LParam, false);
		}

		// Token: 0x06003D64 RID: 15716 RVA: 0x0010A688 File Offset: 0x00108888
		private void WmNotifyNeedTextA(ref Message m)
		{
			NativeMethods.TOOLTIPTEXTA tooltiptexta = (NativeMethods.TOOLTIPTEXTA)m.GetLParam(typeof(NativeMethods.TOOLTIPTEXTA));
			int num = (int)tooltiptexta.hdr.idFrom;
			ToolBarButton toolBarButton = this.buttons[num];
			if (toolBarButton != null && toolBarButton.ToolTipText != null)
			{
				tooltiptexta.lpszText = toolBarButton.ToolTipText;
			}
			else
			{
				tooltiptexta.lpszText = null;
			}
			tooltiptexta.hinst = IntPtr.Zero;
			if (this.RightToLeft == RightToLeft.Yes)
			{
				tooltiptexta.uFlags |= 4;
			}
			Marshal.StructureToPtr(tooltiptexta, m.LParam, false);
		}

		// Token: 0x06003D65 RID: 15717 RVA: 0x0010A714 File Offset: 0x00108914
		private void WmNotifyHotItemChange(ref Message m)
		{
			NativeMethods.NMTBHOTITEM nmtbhotitem = (NativeMethods.NMTBHOTITEM)m.GetLParam(typeof(NativeMethods.NMTBHOTITEM));
			if (16 == (nmtbhotitem.dwFlags & 16))
			{
				this.hotItem = nmtbhotitem.idNew;
				return;
			}
			if (32 == (nmtbhotitem.dwFlags & 32))
			{
				this.hotItem = -1;
				return;
			}
			if (1 == (nmtbhotitem.dwFlags & 1))
			{
				this.hotItem = nmtbhotitem.idNew;
				return;
			}
			if (2 == (nmtbhotitem.dwFlags & 2))
			{
				this.hotItem = nmtbhotitem.idNew;
				return;
			}
			if (4 == (nmtbhotitem.dwFlags & 4))
			{
				this.hotItem = nmtbhotitem.idNew;
				return;
			}
			if (8 == (nmtbhotitem.dwFlags & 8))
			{
				this.hotItem = nmtbhotitem.idNew;
				return;
			}
			if (64 == (nmtbhotitem.dwFlags & 64))
			{
				this.hotItem = nmtbhotitem.idNew;
				return;
			}
			if (128 == (nmtbhotitem.dwFlags & 128))
			{
				this.hotItem = nmtbhotitem.idNew;
				return;
			}
			if (256 == (nmtbhotitem.dwFlags & 256))
			{
				this.hotItem = nmtbhotitem.idNew;
			}
		}

		// Token: 0x06003D66 RID: 15718 RVA: 0x0010A820 File Offset: 0x00108A20
		private void WmReflectCommand(ref Message m)
		{
			int num = NativeMethods.Util.LOWORD(m.WParam);
			ToolBarButton toolBarButton = this.buttons[num];
			if (toolBarButton != null)
			{
				ToolBarButtonClickEventArgs toolBarButtonClickEventArgs = new ToolBarButtonClickEventArgs(toolBarButton);
				this.OnButtonClick(toolBarButtonClickEventArgs);
			}
			base.WndProc(ref m);
			base.ResetMouseEventArgs();
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x06003D67 RID: 15719 RVA: 0x0010A860 File Offset: 0x00108A60
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg != 78 && msg != 8270)
			{
				if (msg == 8465)
				{
					this.WmReflectCommand(ref m);
				}
			}
			else
			{
				NativeMethods.NMHDR nmhdr = (NativeMethods.NMHDR)m.GetLParam(typeof(NativeMethods.NMHDR));
				int code = nmhdr.code;
				if (code <= -706)
				{
					if (code != -713)
					{
						if (code != -710)
						{
							if (code == -706)
							{
								m.Result = (IntPtr)1;
							}
						}
						else
						{
							this.WmNotifyDropDown(ref m);
						}
					}
					else
					{
						this.WmNotifyHotItemChange(ref m);
					}
				}
				else if (code != -530)
				{
					if (code != -521)
					{
						if (code == -520)
						{
							this.WmNotifyNeedTextA(ref m);
							m.Result = (IntPtr)1;
							return;
						}
					}
					else
					{
						NativeMethods.WINDOWPLACEMENT windowplacement = default(NativeMethods.WINDOWPLACEMENT);
						int windowPlacement = UnsafeNativeMethods.GetWindowPlacement(new HandleRef(null, nmhdr.hwndFrom), ref windowplacement);
						if (windowplacement.rcNormalPosition_left == 0 && windowplacement.rcNormalPosition_top == 0 && this.hotItem != -1)
						{
							int num = 0;
							for (int i = 0; i <= this.hotItem; i++)
							{
								num += this.buttonsCollection[i].GetButtonWidth();
							}
							int num2 = windowplacement.rcNormalPosition_right - windowplacement.rcNormalPosition_left;
							int num3 = windowplacement.rcNormalPosition_bottom - windowplacement.rcNormalPosition_top;
							int num4 = base.Location.X + num + 1;
							int num5 = base.Location.Y + this.ButtonSize.Height / 2;
							NativeMethods.POINT point = new NativeMethods.POINT(num4, num5);
							UnsafeNativeMethods.ClientToScreen(new HandleRef(this, base.Handle), point);
							if (point.y < SystemInformation.WorkingArea.Y)
							{
								point.y += this.ButtonSize.Height / 2 + 1;
							}
							if (point.y + num3 > SystemInformation.WorkingArea.Height)
							{
								point.y -= this.ButtonSize.Height / 2 + num3 + 1;
							}
							if (point.x + num2 > SystemInformation.WorkingArea.Right)
							{
								point.x -= this.ButtonSize.Width + num2 + 2;
							}
							SafeNativeMethods.SetWindowPos(new HandleRef(null, nmhdr.hwndFrom), NativeMethods.NullHandleRef, point.x, point.y, 0, 0, 21);
							m.Result = (IntPtr)1;
							return;
						}
					}
				}
				else if (Marshal.SystemDefaultCharSize == 2)
				{
					this.WmNotifyNeedText(ref m);
					m.Result = (IntPtr)1;
					return;
				}
			}
			base.WndProc(ref m);
		}

		// Token: 0x040023F8 RID: 9208
		private ToolBar.ToolBarButtonCollection buttonsCollection;

		// Token: 0x040023F9 RID: 9209
		internal Size buttonSize = Size.Empty;

		// Token: 0x040023FA RID: 9210
		private int requestedSize;

		// Token: 0x040023FB RID: 9211
		internal const int DDARROW_WIDTH = 15;

		// Token: 0x040023FC RID: 9212
		private ToolBarAppearance appearance;

		// Token: 0x040023FD RID: 9213
		private BorderStyle borderStyle;

		// Token: 0x040023FE RID: 9214
		private ToolBarButton[] buttons;

		// Token: 0x040023FF RID: 9215
		private int buttonCount;

		// Token: 0x04002400 RID: 9216
		private ToolBarTextAlign textAlign;

		// Token: 0x04002401 RID: 9217
		private ImageList imageList;

		// Token: 0x04002402 RID: 9218
		private int maxWidth = -1;

		// Token: 0x04002403 RID: 9219
		private int hotItem = -1;

		// Token: 0x04002404 RID: 9220
		private float currentScaleDX = 1f;

		// Token: 0x04002405 RID: 9221
		private float currentScaleDY = 1f;

		// Token: 0x04002406 RID: 9222
		private const int TOOLBARSTATE_wrappable = 1;

		// Token: 0x04002407 RID: 9223
		private const int TOOLBARSTATE_dropDownArrows = 2;

		// Token: 0x04002408 RID: 9224
		private const int TOOLBARSTATE_divider = 4;

		// Token: 0x04002409 RID: 9225
		private const int TOOLBARSTATE_showToolTips = 8;

		// Token: 0x0400240A RID: 9226
		private const int TOOLBARSTATE_autoSize = 16;

		// Token: 0x0400240B RID: 9227
		private BitVector32 toolBarState;

		// Token: 0x0400240C RID: 9228
		private ToolBarButtonClickEventHandler onButtonClick;

		// Token: 0x0400240D RID: 9229
		private ToolBarButtonClickEventHandler onButtonDropDown;

		/// <summary>Encapsulates a collection of <see cref="T:System.Windows.Forms.ToolBarButton" /> controls for use by the <see cref="T:System.Windows.Forms.ToolBar" /> class.</summary>
		// Token: 0x020007F3 RID: 2035
		public class ToolBarButtonCollection : IList, ICollection, IEnumerable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolBar.ToolBarButtonCollection" /> class and assigns it to the specified toolbar.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.ToolBar" /> that is the parent of the collection of <see cref="T:System.Windows.Forms.ToolBarButton" /> controls.</param>
			// Token: 0x06006E57 RID: 28247 RVA: 0x0019404E File Offset: 0x0019224E
			public ToolBarButtonCollection(ToolBar owner)
			{
				this.owner = owner;
			}

			/// <summary>Gets or sets the toolbar button at the specified indexed location in the toolbar button collection.</summary>
			/// <param name="index">The indexed location of the <see cref="T:System.Windows.Forms.ToolBarButton" /> in the collection.</param>
			/// <returns>A <see cref="T:System.Windows.Forms.ToolBarButton" /> that represents the toolbar button at the specified indexed location.</returns>
			/// <exception cref="T:System.ArgumentNullException">The <paramref name="index" /> value is <see langword="null" />.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> value is less than zero.  
			///  -or-  
			///  The <paramref name="index" /> value is greater than the number of buttons in the collection, and the collection of buttons is not <see langword="null" />.</exception>
			// Token: 0x1700181C RID: 6172
			public virtual ToolBarButton this[int index]
			{
				get
				{
					if (index < 0 || (this.owner.buttons != null && index >= this.owner.buttonCount))
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					return this.owner.buttons[index];
				}
				set
				{
					if (index < 0 || (this.owner.buttons != null && index >= this.owner.buttonCount))
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					if (value == null)
					{
						throw new ArgumentNullException("value");
					}
					this.owner.InternalSetButton(index, value, true, true);
				}
			}

			/// <summary>Gets or sets the item at a specified index.</summary>
			/// <param name="index">The zero-based index of the element to get or set.</param>
			/// <returns>The element at the specified index.</returns>
			// Token: 0x1700181D RID: 6173
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					if (value is ToolBarButton)
					{
						this[index] = (ToolBarButton)value;
						return;
					}
					throw new ArgumentException(SR.GetString("ToolBarBadToolBarButton"), "value");
				}
			}

			/// <summary>Gets a <see cref="T:System.Windows.Forms.ToolBarButton" /> with the specified key from the collection.</summary>
			/// <param name="key">The name of the <see cref="T:System.Windows.Forms.ToolBarButton" /> to retrieve.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.ToolBarButton" /> whose <see cref="P:System.Windows.Forms.ToolBarButton.Name" /> property matches the specified key.</returns>
			// Token: 0x1700181E RID: 6174
			public virtual ToolBarButton this[string key]
			{
				get
				{
					if (string.IsNullOrEmpty(key))
					{
						return null;
					}
					int num = this.IndexOfKey(key);
					if (this.IsValidIndex(num))
					{
						return this[num];
					}
					return null;
				}
			}

			/// <summary>Gets the number of buttons in the toolbar button collection.</summary>
			/// <returns>The number of the <see cref="T:System.Windows.Forms.ToolBarButton" /> controls assigned to the toolbar.</returns>
			// Token: 0x1700181F RID: 6175
			// (get) Token: 0x06006E5D RID: 28253 RVA: 0x001941B1 File Offset: 0x001923B1
			[Browsable(false)]
			public int Count
			{
				get
				{
					return this.owner.buttonCount;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection of buttons.</summary>
			// Token: 0x17001820 RID: 6176
			// (get) Token: 0x06006E5E RID: 28254 RVA: 0x00006A49 File Offset: 0x00004C49
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
			/// <returns>
			///   <see langword="false" /> in all cases.</returns>
			// Token: 0x17001821 RID: 6177
			// (get) Token: 0x06006E5F RID: 28255 RVA: 0x0001180C File Offset: 0x0000FA0C
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
			/// <returns>
			///   <see langword="false" /> in all cases.</returns>
			// Token: 0x17001822 RID: 6178
			// (get) Token: 0x06006E60 RID: 28256 RVA: 0x0001180C File Offset: 0x0000FA0C
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>
			///   <see langword="true" /> if the collection is read-only; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
			// Token: 0x17001823 RID: 6179
			// (get) Token: 0x06006E61 RID: 28257 RVA: 0x0001180C File Offset: 0x0000FA0C
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			/// <summary>Adds the specified toolbar button to the end of the toolbar button collection.</summary>
			/// <param name="button">The <see cref="T:System.Windows.Forms.ToolBarButton" /> to be added after all existing buttons.</param>
			/// <returns>The zero-based index value of the <see cref="T:System.Windows.Forms.ToolBarButton" /> added to the collection.</returns>
			// Token: 0x06006E62 RID: 28258 RVA: 0x001941C0 File Offset: 0x001923C0
			public int Add(ToolBarButton button)
			{
				int num = this.owner.InternalAddButton(button);
				if (!this.suspendUpdate)
				{
					this.owner.UpdateButtons();
				}
				return num;
			}

			/// <summary>Adds a new toolbar button to the end of the toolbar button collection with the specified <see cref="P:System.Windows.Forms.ToolBarButton.Text" /> property value.</summary>
			/// <param name="text">The text to display on the new <see cref="T:System.Windows.Forms.ToolBarButton" />.</param>
			/// <returns>The zero-based index value of the <see cref="T:System.Windows.Forms.ToolBarButton" /> added to the collection.</returns>
			// Token: 0x06006E63 RID: 28259 RVA: 0x001941F0 File Offset: 0x001923F0
			public int Add(string text)
			{
				ToolBarButton toolBarButton = new ToolBarButton(text);
				return this.Add(toolBarButton);
			}

			/// <summary>Adds the specified toolbar button to the end of the toolbar button collection.</summary>
			/// <param name="button">The <see cref="T:System.Windows.Forms.ToolBarButton" /> to be added after all existing buttons.</param>
			/// <returns>The zero-based index value of the <see cref="T:System.Windows.Forms.ToolBarButton" /> added to the collection.</returns>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="button" /> is not a <see cref="T:System.Windows.Forms.ToolBarButton" />.</exception>
			// Token: 0x06006E64 RID: 28260 RVA: 0x0019420B File Offset: 0x0019240B
			int IList.Add(object button)
			{
				if (button is ToolBarButton)
				{
					return this.Add((ToolBarButton)button);
				}
				throw new ArgumentException(SR.GetString("ToolBarBadToolBarButton"), "button");
			}

			/// <summary>Adds a collection of toolbar buttons to this toolbar button collection.</summary>
			/// <param name="buttons">The collection of <see cref="T:System.Windows.Forms.ToolBarButton" /> controls to add to this <see cref="T:System.Windows.Forms.ToolBar.ToolBarButtonCollection" /> contained in an array.</param>
			// Token: 0x06006E65 RID: 28261 RVA: 0x00194238 File Offset: 0x00192438
			public void AddRange(ToolBarButton[] buttons)
			{
				if (buttons == null)
				{
					throw new ArgumentNullException("buttons");
				}
				try
				{
					this.suspendUpdate = true;
					foreach (ToolBarButton toolBarButton in buttons)
					{
						this.Add(toolBarButton);
					}
				}
				finally
				{
					this.suspendUpdate = false;
					this.owner.UpdateButtons();
				}
			}

			/// <summary>Removes all buttons from the toolbar button collection.</summary>
			// Token: 0x06006E66 RID: 28262 RVA: 0x0019429C File Offset: 0x0019249C
			public void Clear()
			{
				if (this.owner.buttons == null)
				{
					return;
				}
				for (int i = this.owner.buttonCount; i > 0; i--)
				{
					if (this.owner.IsHandleCreated)
					{
						this.owner.SendMessage(1046, i - 1, 0);
					}
					this.owner.RemoveAt(i - 1);
				}
				this.owner.buttons = null;
				this.owner.buttonCount = 0;
				if (!this.owner.Disposing)
				{
					this.owner.UpdateButtons();
				}
			}

			/// <summary>Determines if the specified toolbar button is a member of the collection.</summary>
			/// <param name="button">The <see cref="T:System.Windows.Forms.ToolBarButton" /> to locate in the collection.</param>
			/// <returns>
			///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.ToolBarButton" /> is a member of the collection; otherwise, <see langword="false" />.</returns>
			// Token: 0x06006E67 RID: 28263 RVA: 0x0019432D File Offset: 0x0019252D
			public bool Contains(ToolBarButton button)
			{
				return this.IndexOf(button) != -1;
			}

			/// <summary>Determines whether the collection contains a specific value.</summary>
			/// <param name="button">The item to locate in the collection.</param>
			/// <returns>
			///   <see langword="true" /> if the item is found in the collection; otherwise, <see langword="false" />.</returns>
			// Token: 0x06006E68 RID: 28264 RVA: 0x0019433C File Offset: 0x0019253C
			bool IList.Contains(object button)
			{
				return button is ToolBarButton && this.Contains((ToolBarButton)button);
			}

			/// <summary>Determines if a <see cref="T:System.Windows.Forms.ToolBarButton" /> with the specified key is contained in the collection.</summary>
			/// <param name="key">The name of the <see cref="T:System.Windows.Forms.ToolBarButton" /> to search for.</param>
			/// <returns>
			///   <see langword="true" /> to indicate a <see cref="T:System.Windows.Forms.ToolBarButton" /> with the specified key is found; otherwise, <see langword="false" />.</returns>
			// Token: 0x06006E69 RID: 28265 RVA: 0x00194354 File Offset: 0x00192554
			public virtual bool ContainsKey(string key)
			{
				return this.IsValidIndex(this.IndexOfKey(key));
			}

			/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
			/// <param name="dest">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="dest" /> at which copying begins.</param>
			// Token: 0x06006E6A RID: 28266 RVA: 0x00194363 File Offset: 0x00192563
			void ICollection.CopyTo(Array dest, int index)
			{
				if (this.owner.buttonCount > 0)
				{
					Array.Copy(this.owner.buttons, 0, dest, index, this.owner.buttonCount);
				}
			}

			/// <summary>Retrieves the index of the specified toolbar button in the collection.</summary>
			/// <param name="button">The <see cref="T:System.Windows.Forms.ToolBarButton" /> to locate in the collection.</param>
			/// <returns>The zero-based index of the item found in the collection; otherwise, -1.</returns>
			// Token: 0x06006E6B RID: 28267 RVA: 0x00194394 File Offset: 0x00192594
			public int IndexOf(ToolBarButton button)
			{
				for (int i = 0; i < this.Count; i++)
				{
					if (this[i] == button)
					{
						return i;
					}
				}
				return -1;
			}

			/// <summary>Determines the index of a specific item in the collection.</summary>
			/// <param name="button">The item to locate in the collection.</param>
			/// <returns>The index of <paramref name="button" /> if found in the list; otherwise, -1.</returns>
			// Token: 0x06006E6C RID: 28268 RVA: 0x001943BF File Offset: 0x001925BF
			int IList.IndexOf(object button)
			{
				if (button is ToolBarButton)
				{
					return this.IndexOf((ToolBarButton)button);
				}
				return -1;
			}

			/// <summary>Retrieves the index of the first occurrence of a <see cref="T:System.Windows.Forms.ToolBarButton" /> with the specified key.</summary>
			/// <param name="key">The name of the <see cref="T:System.Windows.Forms.ToolBarButton" /> to search for.</param>
			/// <returns>The index of the first occurrence of a <see cref="T:System.Windows.Forms.ToolBarButton" /> with the specified key, if found; otherwise, -1.</returns>
			// Token: 0x06006E6D RID: 28269 RVA: 0x001943D8 File Offset: 0x001925D8
			public virtual int IndexOfKey(string key)
			{
				if (string.IsNullOrEmpty(key))
				{
					return -1;
				}
				if (this.IsValidIndex(this.lastAccessedIndex) && WindowsFormsUtils.SafeCompareStrings(this[this.lastAccessedIndex].Name, key, true))
				{
					return this.lastAccessedIndex;
				}
				for (int i = 0; i < this.Count; i++)
				{
					if (WindowsFormsUtils.SafeCompareStrings(this[i].Name, key, true))
					{
						this.lastAccessedIndex = i;
						return i;
					}
				}
				this.lastAccessedIndex = -1;
				return -1;
			}

			/// <summary>Inserts an existing toolbar button in the toolbar button collection at the specified location.</summary>
			/// <param name="index">The indexed location within the collection to insert the toolbar button.</param>
			/// <param name="button">The <see cref="T:System.Windows.Forms.ToolBarButton" /> to insert.</param>
			// Token: 0x06006E6E RID: 28270 RVA: 0x00194455 File Offset: 0x00192655
			public void Insert(int index, ToolBarButton button)
			{
				this.owner.InsertButton(index, button);
			}

			/// <summary>Inserts an existing toolbar button in the toolbar button collection at the specified location.</summary>
			/// <param name="index">The indexed location within the collection to insert the toolbar button.</param>
			/// <param name="button">The <see cref="T:System.Windows.Forms.ToolBarButton" /> to insert.</param>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="button" /> is not a <see cref="T:System.Windows.Forms.ToolBarButton" />.</exception>
			// Token: 0x06006E6F RID: 28271 RVA: 0x00194464 File Offset: 0x00192664
			void IList.Insert(int index, object button)
			{
				if (button is ToolBarButton)
				{
					this.Insert(index, (ToolBarButton)button);
					return;
				}
				throw new ArgumentException(SR.GetString("ToolBarBadToolBarButton"), "button");
			}

			// Token: 0x06006E70 RID: 28272 RVA: 0x00194490 File Offset: 0x00192690
			private bool IsValidIndex(int index)
			{
				return index >= 0 && index < this.Count;
			}

			/// <summary>Removes a given button from the toolbar button collection.</summary>
			/// <param name="index">The indexed location of the <see cref="T:System.Windows.Forms.ToolBarButton" /> in the collection.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> value is less than 0, or it is greater than the number of buttons in the collection.</exception>
			// Token: 0x06006E71 RID: 28273 RVA: 0x001944A4 File Offset: 0x001926A4
			public void RemoveAt(int index)
			{
				int num = ((this.owner.buttons == null) ? 0 : this.owner.buttonCount);
				if (index < 0 || index >= num)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.owner.IsHandleCreated)
				{
					this.owner.SendMessage(1046, index, 0);
				}
				this.owner.RemoveAt(index);
				this.owner.UpdateButtons();
			}

			/// <summary>Removes the <see cref="T:System.Windows.Forms.ToolBarButton" /> with the specified key from the collection.</summary>
			/// <param name="key">The name of the <see cref="T:System.Windows.Forms.ToolBarButton" /> to remove from the collection.</param>
			// Token: 0x06006E72 RID: 28274 RVA: 0x00194540 File Offset: 0x00192740
			public virtual void RemoveByKey(string key)
			{
				int num = this.IndexOfKey(key);
				if (this.IsValidIndex(num))
				{
					this.RemoveAt(num);
				}
			}

			/// <summary>Removes a given button from the toolbar button collection.</summary>
			/// <param name="button">The <see cref="T:System.Windows.Forms.ToolBarButton" /> to remove from the collection.</param>
			// Token: 0x06006E73 RID: 28275 RVA: 0x00194568 File Offset: 0x00192768
			public void Remove(ToolBarButton button)
			{
				int num = this.IndexOf(button);
				if (num != -1)
				{
					this.RemoveAt(num);
				}
			}

			/// <summary>Removes the first occurrence of an item from the collection.</summary>
			/// <param name="button">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.IList" />.</param>
			// Token: 0x06006E74 RID: 28276 RVA: 0x00194588 File Offset: 0x00192788
			void IList.Remove(object button)
			{
				if (button is ToolBarButton)
				{
					this.Remove((ToolBarButton)button);
				}
			}

			/// <summary>Returns an enumerator that can be used to iterate through the toolbar button collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the tree node collection.</returns>
			// Token: 0x06006E75 RID: 28277 RVA: 0x001945A0 File Offset: 0x001927A0
			public IEnumerator GetEnumerator()
			{
				object[] buttons = this.owner.buttons;
				return new WindowsFormsUtils.ArraySubsetEnumerator(buttons, this.owner.buttonCount);
			}

			// Token: 0x040042DC RID: 17116
			private ToolBar owner;

			// Token: 0x040042DD RID: 17117
			private bool suspendUpdate;

			// Token: 0x040042DE RID: 17118
			private int lastAccessedIndex = -1;
		}
	}
}
