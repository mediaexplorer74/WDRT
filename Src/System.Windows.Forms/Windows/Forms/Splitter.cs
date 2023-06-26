using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	/// <summary>Represents a splitter control that enables the user to resize docked controls. <see cref="T:System.Windows.Forms.Splitter" /> has been replaced by <see cref="T:System.Windows.Forms.SplitContainer" /> and is provided only for compatibility with previous versions.</summary>
	// Token: 0x0200036D RID: 877
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultEvent("SplitterMoved")]
	[DefaultProperty("Dock")]
	[SRDescription("DescriptionSplitter")]
	[Designer("System.Windows.Forms.Design.SplitterDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class Splitter : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Splitter" /> class. <see cref="T:System.Windows.Forms.Splitter" /> has been replaced by <see cref="T:System.Windows.Forms.SplitContainer" />, and is provided only for compatibility with previous versions.</summary>
		// Token: 0x06003935 RID: 14645 RVA: 0x000FEE84 File Offset: 0x000FD084
		public Splitter()
		{
			base.SetStyle(ControlStyles.Selectable, false);
			this.TabStop = false;
			this.minSize = 25;
			this.minExtra = 25;
			this.Dock = DockStyle.Left;
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AnchorStyles" /> values.</returns>
		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x06003936 RID: 14646 RVA: 0x0001180C File Offset: 0x0000FA0C
		// (set) Token: 0x06003937 RID: 14647 RVA: 0x000070A6 File Offset: 0x000052A6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(AnchorStyles.None)]
		public override AnchorStyles Anchor
		{
			get
			{
				return AnchorStyles.None;
			}
			set
			{
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>
		///   <see langword="true" /> if enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x06003938 RID: 14648 RVA: 0x000B8E3D File Offset: 0x000B703D
		// (set) Token: 0x06003939 RID: 14649 RVA: 0x000B8E45 File Offset: 0x000B7045
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the default size of the control.</returns>
		// Token: 0x17000D97 RID: 3479
		// (get) Token: 0x0600393A RID: 14650 RVA: 0x000FEEF1 File Offset: 0x000FD0F1
		protected override Size DefaultSize
		{
			get
			{
				return new Size(3, 3);
			}
		}

		/// <summary>Gets or sets the default cursor for the control.</summary>
		/// <returns>An object of type <see cref="T:System.Windows.Forms.Cursor" /> representing the current default cursor.</returns>
		// Token: 0x17000D98 RID: 3480
		// (get) Token: 0x0600393B RID: 14651 RVA: 0x000FEEFC File Offset: 0x000FD0FC
		protected override Cursor DefaultCursor
		{
			get
			{
				DockStyle dock = this.Dock;
				if (dock - DockStyle.Top <= 1)
				{
					return Cursors.HSplit;
				}
				if (dock - DockStyle.Left > 1)
				{
					return base.DefaultCursor;
				}
				return Cursors.VSplit;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>The foreground color of the control.</returns>
		// Token: 0x17000D99 RID: 3481
		// (get) Token: 0x0600393C RID: 14652 RVA: 0x0001A0E7 File Offset: 0x000182E7
		// (set) Token: 0x0600393D RID: 14653 RVA: 0x00013024 File Offset: 0x00011224
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

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x140002B4 RID: 692
		// (add) Token: 0x0600393E RID: 14654 RVA: 0x0005A8EE File Offset: 0x00058AEE
		// (remove) Token: 0x0600393F RID: 14655 RVA: 0x0005A8F7 File Offset: 0x00058AF7
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

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>The background image displayed in the control.</returns>
		// Token: 0x17000D9A RID: 3482
		// (get) Token: 0x06003940 RID: 14656 RVA: 0x0001187C File Offset: 0x0000FA7C
		// (set) Token: 0x06003941 RID: 14657 RVA: 0x00011884 File Offset: 0x0000FA84
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

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x140002B5 RID: 693
		// (add) Token: 0x06003942 RID: 14658 RVA: 0x0001188D File Offset: 0x0000FA8D
		// (remove) Token: 0x06003943 RID: 14659 RVA: 0x00011896 File Offset: 0x0000FA96
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

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImageLayout" /> values.</returns>
		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x06003944 RID: 14660 RVA: 0x0001189F File Offset: 0x0000FA9F
		// (set) Token: 0x06003945 RID: 14661 RVA: 0x000118A7 File Offset: 0x0000FAA7
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

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x140002B6 RID: 694
		// (add) Token: 0x06003946 RID: 14662 RVA: 0x000118B0 File Offset: 0x0000FAB0
		// (remove) Token: 0x06003947 RID: 14663 RVA: 0x000118B9 File Offset: 0x0000FAB9
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

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>The font of the text displayed by the control.</returns>
		// Token: 0x17000D9C RID: 3484
		// (get) Token: 0x06003948 RID: 14664 RVA: 0x0001A0D6 File Offset: 0x000182D6
		// (set) Token: 0x06003949 RID: 14665 RVA: 0x0001A0DE File Offset: 0x000182DE
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;
			}
		}

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x140002B7 RID: 695
		// (add) Token: 0x0600394A RID: 14666 RVA: 0x0005A900 File Offset: 0x00058B00
		// (remove) Token: 0x0600394B RID: 14667 RVA: 0x0005A909 File Offset: 0x00058B09
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler FontChanged
		{
			add
			{
				base.FontChanged += value;
			}
			remove
			{
				base.FontChanged -= value;
			}
		}

		/// <summary>Gets or sets the style of border for the control. <see cref="P:System.Windows.Forms.Splitter.BorderStyle" /> has been replaced by <see cref="P:System.Windows.Forms.SplitContainer.BorderStyle" /> and is provided only for compatibility with previous versions.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. The default is <see langword="BorderStyle.None" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value of the property is not one of the <see cref="T:System.Windows.Forms.BorderStyle" /> values.</exception>
		// Token: 0x17000D9D RID: 3485
		// (get) Token: 0x0600394C RID: 14668 RVA: 0x000FEF30 File Offset: 0x000FD130
		// (set) Token: 0x0600394D RID: 14669 RVA: 0x000FEF38 File Offset: 0x000FD138
		[DefaultValue(BorderStyle.None)]
		[SRCategory("CatAppearance")]
		[DispId(-504)]
		[SRDescription("SplitterBorderStyleDescr")]
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
					base.UpdateStyles();
				}
			}
		}

		/// <summary>Returns the parameters needed to create the handle.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x17000D9E RID: 3486
		// (get) Token: 0x0600394E RID: 14670 RVA: 0x000FEF78 File Offset: 0x000FD178
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
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

		/// <summary>Gets the default Input Method Editor (IME) mode supported by this control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x0600394F RID: 14671 RVA: 0x00023BD7 File Offset: 0x00021DD7
		protected override ImeMode DefaultImeMode
		{
			get
			{
				return ImeMode.Disable;
			}
		}

		/// <summary>Gets or sets which <see cref="T:System.Windows.Forms.Splitter" /> borders are docked to its parent control and determines how a <see cref="T:System.Windows.Forms.Splitter" /> is resized with its parent.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values. The default is <see cref="F:System.Windows.Forms.DockStyle.Left" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Windows.Forms.Splitter.Dock" /> is not set to one of the valid <see cref="T:System.Windows.Forms.DockStyle" /> values.</exception>
		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x06003950 RID: 14672 RVA: 0x000FC41E File Offset: 0x000FA61E
		// (set) Token: 0x06003951 RID: 14673 RVA: 0x000FEFE8 File Offset: 0x000FD1E8
		[Localizable(true)]
		[DefaultValue(DockStyle.Left)]
		public override DockStyle Dock
		{
			get
			{
				return base.Dock;
			}
			set
			{
				if (value != DockStyle.Top && value != DockStyle.Bottom && value != DockStyle.Left && value != DockStyle.Right)
				{
					throw new ArgumentException(SR.GetString("SplitterInvalidDockEnum"));
				}
				int num = this.splitterThickness;
				base.Dock = value;
				DockStyle dock = this.Dock;
				if (dock - DockStyle.Top > 1)
				{
					if (dock - DockStyle.Left > 1)
					{
						return;
					}
					if (this.splitterThickness != -1)
					{
						base.Width = num;
					}
				}
				else if (this.splitterThickness != -1)
				{
					base.Height = num;
					return;
				}
			}
		}

		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x06003952 RID: 14674 RVA: 0x000FF058 File Offset: 0x000FD258
		private bool Horizontal
		{
			get
			{
				DockStyle dock = this.Dock;
				return dock == DockStyle.Left || dock == DockStyle.Right;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
		// Token: 0x17000DA2 RID: 3490
		// (get) Token: 0x06003953 RID: 14675 RVA: 0x0001A051 File Offset: 0x00018251
		// (set) Token: 0x06003954 RID: 14676 RVA: 0x0001A059 File Offset: 0x00018259
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

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x140002B8 RID: 696
		// (add) Token: 0x06003955 RID: 14677 RVA: 0x00023F70 File Offset: 0x00022170
		// (remove) Token: 0x06003956 RID: 14678 RVA: 0x00023F79 File Offset: 0x00022179
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

		/// <summary>Gets or sets the minimum distance that must remain between the splitter control and the edge of the opposite side of the container (or the closest control docked to that side). <see cref="P:System.Windows.Forms.Splitter.MinExtra" /> has been replaced by similar properties in <see cref="T:System.Windows.Forms.SplitContainer" /> and is provided only for compatibility with previous versions.</summary>
		/// <returns>The minimum distance, in pixels, between the <see cref="T:System.Windows.Forms.Splitter" /> control and the edge of the opposite side of the container (or the closest control docked to that side). The default is 25.</returns>
		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x06003957 RID: 14679 RVA: 0x000FF076 File Offset: 0x000FD276
		// (set) Token: 0x06003958 RID: 14680 RVA: 0x000FF07E File Offset: 0x000FD27E
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[DefaultValue(25)]
		[SRDescription("SplitterMinExtraDescr")]
		public int MinExtra
		{
			get
			{
				return this.minExtra;
			}
			set
			{
				if (value < 0)
				{
					value = 0;
				}
				this.minExtra = value;
			}
		}

		/// <summary>Gets or sets the minimum distance that must remain between the splitter control and the container edge that the control is docked to. <see cref="P:System.Windows.Forms.Splitter.MinSize" /> has been replaced by <see cref="P:System.Windows.Forms.SplitContainer.Panel1MinSize" /> and <see cref="P:System.Windows.Forms.SplitContainer.Panel2MinSize" /> and is provided only for compatibility with previous versions.</summary>
		/// <returns>The minimum distance, in pixels, between the <see cref="T:System.Windows.Forms.Splitter" /> control and the container edge that the control is docked to. The default is 25.</returns>
		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x06003959 RID: 14681 RVA: 0x000FF08E File Offset: 0x000FD28E
		// (set) Token: 0x0600395A RID: 14682 RVA: 0x000FF096 File Offset: 0x000FD296
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[DefaultValue(25)]
		[SRDescription("SplitterMinSizeDescr")]
		public int MinSize
		{
			get
			{
				return this.minSize;
			}
			set
			{
				if (value < 0)
				{
					value = 0;
				}
				this.minSize = value;
			}
		}

		/// <summary>Gets or sets the distance between the splitter control and the container edge that the control is docked to. <see cref="P:System.Windows.Forms.Splitter.SplitPosition" /> has been replaced by <see cref="P:System.Windows.Forms.SplitContainer.Panel1MinSize" /> and <see cref="P:System.Windows.Forms.SplitContainer.Panel2MinSize" /> and is provided only for compatibility with previous versions.</summary>
		/// <returns>The distance, in pixels, between the <see cref="T:System.Windows.Forms.Splitter" /> control and the container edge that the control is docked to. If the <see cref="T:System.Windows.Forms.Splitter" /> control is not bound to a control, the value is -1.</returns>
		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x0600395B RID: 14683 RVA: 0x000FF0A6 File Offset: 0x000FD2A6
		// (set) Token: 0x0600395C RID: 14684 RVA: 0x000FF0C4 File Offset: 0x000FD2C4
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("SplitterSplitPositionDescr")]
		public int SplitPosition
		{
			get
			{
				if (this.splitSize == -1)
				{
					this.splitSize = this.CalcSplitSize();
				}
				return this.splitSize;
			}
			set
			{
				Splitter.SplitData splitData = this.CalcSplitBounds();
				if (value > this.maxSize)
				{
					value = this.maxSize;
				}
				if (value < this.minSize)
				{
					value = this.minSize;
				}
				this.splitSize = value;
				this.DrawSplitBar(3);
				if (splitData.target == null)
				{
					this.splitSize = -1;
					return;
				}
				Rectangle bounds = splitData.target.Bounds;
				switch (this.Dock)
				{
				case DockStyle.Top:
					bounds.Height = value;
					break;
				case DockStyle.Bottom:
					bounds.Y += bounds.Height - this.splitSize;
					bounds.Height = value;
					break;
				case DockStyle.Left:
					bounds.Width = value;
					break;
				case DockStyle.Right:
					bounds.X += bounds.Width - this.splitSize;
					bounds.Width = value;
					break;
				}
				splitData.target.Bounds = bounds;
				Application.DoEvents();
				this.OnSplitterMoved(new SplitterEventArgs(base.Left, base.Top, base.Left + bounds.Width / 2, base.Top + bounds.Height / 2));
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>
		///   <see langword="true" /> if enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x0600395D RID: 14685 RVA: 0x000B2395 File Offset: 0x000B0595
		// (set) Token: 0x0600395E RID: 14686 RVA: 0x000B239D File Offset: 0x000B059D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x140002B9 RID: 697
		// (add) Token: 0x0600395F RID: 14687 RVA: 0x000B23A6 File Offset: 0x000B05A6
		// (remove) Token: 0x06003960 RID: 14688 RVA: 0x000B23AF File Offset: 0x000B05AF
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A string.</returns>
		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x06003961 RID: 14689 RVA: 0x00013814 File Offset: 0x00011A14
		// (set) Token: 0x06003962 RID: 14690 RVA: 0x00023FE9 File Offset: 0x000221E9
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

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x140002BA RID: 698
		// (add) Token: 0x06003963 RID: 14691 RVA: 0x00046591 File Offset: 0x00044791
		// (remove) Token: 0x06003964 RID: 14692 RVA: 0x0004659A File Offset: 0x0004479A
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

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x140002BB RID: 699
		// (add) Token: 0x06003965 RID: 14693 RVA: 0x000E3338 File Offset: 0x000E1538
		// (remove) Token: 0x06003966 RID: 14694 RVA: 0x000E3341 File Offset: 0x000E1541
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Enter
		{
			add
			{
				base.Enter += value;
			}
			remove
			{
				base.Enter -= value;
			}
		}

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x140002BC RID: 700
		// (add) Token: 0x06003967 RID: 14695 RVA: 0x000B9104 File Offset: 0x000B7304
		// (remove) Token: 0x06003968 RID: 14696 RVA: 0x000B910D File Offset: 0x000B730D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x140002BD RID: 701
		// (add) Token: 0x06003969 RID: 14697 RVA: 0x000B9116 File Offset: 0x000B7316
		// (remove) Token: 0x0600396A RID: 14698 RVA: 0x000B911F File Offset: 0x000B731F
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x140002BE RID: 702
		// (add) Token: 0x0600396B RID: 14699 RVA: 0x000B9128 File Offset: 0x000B7328
		// (remove) Token: 0x0600396C RID: 14700 RVA: 0x000B9131 File Offset: 0x000B7331
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x140002BF RID: 703
		// (add) Token: 0x0600396D RID: 14701 RVA: 0x000E334A File Offset: 0x000E154A
		// (remove) Token: 0x0600396E RID: 14702 RVA: 0x000E3353 File Offset: 0x000E1553
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Leave
		{
			add
			{
				base.Leave += value;
			}
			remove
			{
				base.Leave -= value;
			}
		}

		/// <summary>Occurs when the splitter control is in the process of moving. <see cref="E:System.Windows.Forms.Splitter.SplitterMoving" /> has been replaced by <see cref="E:System.Windows.Forms.SplitContainer.SplitterMoving" /> and is provided only for compatibility with previous versions.</summary>
		// Token: 0x140002C0 RID: 704
		// (add) Token: 0x0600396F RID: 14703 RVA: 0x000FF1EB File Offset: 0x000FD3EB
		// (remove) Token: 0x06003970 RID: 14704 RVA: 0x000FF1FE File Offset: 0x000FD3FE
		[SRCategory("CatBehavior")]
		[SRDescription("SplitterSplitterMovingDescr")]
		public event SplitterEventHandler SplitterMoving
		{
			add
			{
				base.Events.AddHandler(Splitter.EVENT_MOVING, value);
			}
			remove
			{
				base.Events.RemoveHandler(Splitter.EVENT_MOVING, value);
			}
		}

		/// <summary>Occurs when the splitter control is moved. <see cref="E:System.Windows.Forms.Splitter.SplitterMoved" /> has been replaced by <see cref="E:System.Windows.Forms.SplitContainer.SplitterMoved" /> and is provided only for compatibility with previous versions.</summary>
		// Token: 0x140002C1 RID: 705
		// (add) Token: 0x06003971 RID: 14705 RVA: 0x000FF211 File Offset: 0x000FD411
		// (remove) Token: 0x06003972 RID: 14706 RVA: 0x000FF224 File Offset: 0x000FD424
		[SRCategory("CatBehavior")]
		[SRDescription("SplitterSplitterMovedDescr")]
		public event SplitterEventHandler SplitterMoved
		{
			add
			{
				base.Events.AddHandler(Splitter.EVENT_MOVED, value);
			}
			remove
			{
				base.Events.RemoveHandler(Splitter.EVENT_MOVED, value);
			}
		}

		// Token: 0x06003973 RID: 14707 RVA: 0x000FF238 File Offset: 0x000FD438
		private void DrawSplitBar(int mode)
		{
			if (mode != 1 && this.lastDrawSplit != -1)
			{
				this.DrawSplitHelper(this.lastDrawSplit);
				this.lastDrawSplit = -1;
			}
			else if (mode != 1 && this.lastDrawSplit == -1)
			{
				return;
			}
			if (mode != 3)
			{
				this.DrawSplitHelper(this.splitSize);
				this.lastDrawSplit = this.splitSize;
				return;
			}
			if (this.lastDrawSplit != -1)
			{
				this.DrawSplitHelper(this.lastDrawSplit);
			}
			this.lastDrawSplit = -1;
		}

		// Token: 0x06003974 RID: 14708 RVA: 0x000FF2B0 File Offset: 0x000FD4B0
		private Rectangle CalcSplitLine(int splitSize, int minWeight)
		{
			Rectangle bounds = base.Bounds;
			Rectangle bounds2 = this.splitTarget.Bounds;
			switch (this.Dock)
			{
			case DockStyle.Top:
				if (bounds.Height < minWeight)
				{
					bounds.Height = minWeight;
				}
				bounds.Y = bounds2.Y + splitSize;
				break;
			case DockStyle.Bottom:
				if (bounds.Height < minWeight)
				{
					bounds.Height = minWeight;
				}
				bounds.Y = bounds2.Y + bounds2.Height - splitSize - bounds.Height;
				break;
			case DockStyle.Left:
				if (bounds.Width < minWeight)
				{
					bounds.Width = minWeight;
				}
				bounds.X = bounds2.X + splitSize;
				break;
			case DockStyle.Right:
				if (bounds.Width < minWeight)
				{
					bounds.Width = minWeight;
				}
				bounds.X = bounds2.X + bounds2.Width - splitSize - bounds.Width;
				break;
			}
			return bounds;
		}

		// Token: 0x06003975 RID: 14709 RVA: 0x000FF3A8 File Offset: 0x000FD5A8
		private int CalcSplitSize()
		{
			Control control = this.FindTarget();
			if (control == null)
			{
				return -1;
			}
			Rectangle bounds = control.Bounds;
			DockStyle dock = this.Dock;
			if (dock - DockStyle.Top <= 1)
			{
				return bounds.Height;
			}
			if (dock - DockStyle.Left > 1)
			{
				return -1;
			}
			return bounds.Width;
		}

		// Token: 0x06003976 RID: 14710 RVA: 0x000FF3F0 File Offset: 0x000FD5F0
		private Splitter.SplitData CalcSplitBounds()
		{
			Splitter.SplitData splitData = new Splitter.SplitData();
			Control control = this.FindTarget();
			splitData.target = control;
			if (control != null)
			{
				DockStyle dock = control.Dock;
				if (dock - DockStyle.Top > 1)
				{
					if (dock - DockStyle.Left <= 1)
					{
						this.initTargetSize = control.Bounds.Width;
					}
				}
				else
				{
					this.initTargetSize = control.Bounds.Height;
				}
				Control parentInternal = this.ParentInternal;
				Control.ControlCollection controls = parentInternal.Controls;
				int count = controls.Count;
				int num = 0;
				int num2 = 0;
				for (int i = 0; i < count; i++)
				{
					Control control2 = controls[i];
					if (control2 != control)
					{
						DockStyle dock2 = control2.Dock;
						if (dock2 - DockStyle.Top > 1)
						{
							if (dock2 - DockStyle.Left <= 1)
							{
								num += control2.Width;
							}
						}
						else
						{
							num2 += control2.Height;
						}
					}
				}
				Size clientSize = parentInternal.ClientSize;
				if (this.Horizontal)
				{
					this.maxSize = clientSize.Width - num - this.minExtra;
				}
				else
				{
					this.maxSize = clientSize.Height - num2 - this.minExtra;
				}
				splitData.dockWidth = num;
				splitData.dockHeight = num2;
			}
			return splitData;
		}

		// Token: 0x06003977 RID: 14711 RVA: 0x000FF518 File Offset: 0x000FD718
		private void DrawSplitHelper(int splitSize)
		{
			if (this.splitTarget == null)
			{
				return;
			}
			Rectangle rectangle = this.CalcSplitLine(splitSize, 3);
			IntPtr handle = this.ParentInternal.Handle;
			IntPtr dcex = UnsafeNativeMethods.GetDCEx(new HandleRef(this.ParentInternal, handle), NativeMethods.NullHandleRef, 1026);
			IntPtr intPtr = ControlPaint.CreateHalftoneHBRUSH();
			IntPtr intPtr2 = SafeNativeMethods.SelectObject(new HandleRef(this.ParentInternal, dcex), new HandleRef(null, intPtr));
			SafeNativeMethods.PatBlt(new HandleRef(this.ParentInternal, dcex), rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, 5898313);
			SafeNativeMethods.SelectObject(new HandleRef(this.ParentInternal, dcex), new HandleRef(null, intPtr2));
			SafeNativeMethods.DeleteObject(new HandleRef(null, intPtr));
			UnsafeNativeMethods.ReleaseDC(new HandleRef(this.ParentInternal, handle), new HandleRef(null, dcex));
		}

		// Token: 0x06003978 RID: 14712 RVA: 0x000FF5F4 File Offset: 0x000FD7F4
		private Control FindTarget()
		{
			Control parentInternal = this.ParentInternal;
			if (parentInternal == null)
			{
				return null;
			}
			Control.ControlCollection controls = parentInternal.Controls;
			int count = controls.Count;
			DockStyle dock = this.Dock;
			for (int i = 0; i < count; i++)
			{
				Control control = controls[i];
				if (control != this)
				{
					switch (dock)
					{
					case DockStyle.Top:
						if (control.Bottom == base.Top)
						{
							return control;
						}
						break;
					case DockStyle.Bottom:
						if (control.Top == base.Bottom)
						{
							return control;
						}
						break;
					case DockStyle.Left:
						if (control.Right == base.Left)
						{
							return control;
						}
						break;
					case DockStyle.Right:
						if (control.Left == base.Right)
						{
							return control;
						}
						break;
					}
				}
			}
			return null;
		}

		// Token: 0x06003979 RID: 14713 RVA: 0x000FF6A4 File Offset: 0x000FD8A4
		private int GetSplitSize(int x, int y)
		{
			int num;
			if (this.Horizontal)
			{
				num = x - this.anchor.X;
			}
			else
			{
				num = y - this.anchor.Y;
			}
			int num2 = 0;
			switch (this.Dock)
			{
			case DockStyle.Top:
				num2 = this.splitTarget.Height + num;
				break;
			case DockStyle.Bottom:
				num2 = this.splitTarget.Height - num;
				break;
			case DockStyle.Left:
				num2 = this.splitTarget.Width + num;
				break;
			case DockStyle.Right:
				num2 = this.splitTarget.Width - num;
				break;
			}
			return Math.Max(Math.Min(num2, this.maxSize), this.minSize);
		}

		/// <summary>This method is not relevant to this class.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		// Token: 0x0600397A RID: 14714 RVA: 0x000FF74F File Offset: 0x000FD94F
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (this.splitTarget != null && e.KeyCode == Keys.Escape)
			{
				this.SplitEnd(false);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x0600397B RID: 14715 RVA: 0x000FF771 File Offset: 0x000FD971
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (e.Button == MouseButtons.Left && e.Clicks == 1)
			{
				this.SplitBegin(e.X, e.Y);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x0600397C RID: 14716 RVA: 0x000FF7A4 File Offset: 0x000FD9A4
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (this.splitTarget != null)
			{
				int num = e.X + base.Left;
				int num2 = e.Y + base.Top;
				Rectangle rectangle = this.CalcSplitLine(this.GetSplitSize(e.X, e.Y), 0);
				int x = rectangle.X;
				int y = rectangle.Y;
				this.OnSplitterMoving(new SplitterEventArgs(num, num2, x, y));
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x0600397D RID: 14717 RVA: 0x000FF818 File Offset: 0x000FDA18
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (this.splitTarget != null)
			{
				int num = e.X + base.Left;
				int num2 = e.Y + base.Top;
				Rectangle rectangle = this.CalcSplitLine(this.GetSplitSize(e.X, e.Y), 0);
				int x = rectangle.X;
				int y = rectangle.Y;
				this.SplitEnd(true);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Splitter.SplitterMoving" /> event. <see cref="M:System.Windows.Forms.Splitter.OnSplitterMoving(System.Windows.Forms.SplitterEventArgs)" /> has been replaced by <see cref="M:System.Windows.Forms.SplitContainer.OnSplitterMoving(System.Windows.Forms.SplitterCancelEventArgs)" /> and is provided only for compatibility with previous versions.</summary>
		/// <param name="sevent">A <see cref="T:System.Windows.Forms.SplitterEventArgs" /> that contains the event data.</param>
		// Token: 0x0600397E RID: 14718 RVA: 0x000FF884 File Offset: 0x000FDA84
		protected virtual void OnSplitterMoving(SplitterEventArgs sevent)
		{
			SplitterEventHandler splitterEventHandler = (SplitterEventHandler)base.Events[Splitter.EVENT_MOVING];
			if (splitterEventHandler != null)
			{
				splitterEventHandler(this, sevent);
			}
			if (this.splitTarget != null)
			{
				this.SplitMove(sevent.SplitX, sevent.SplitY);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Splitter.SplitterMoved" /> event. <see cref="M:System.Windows.Forms.Splitter.OnSplitterMoved(System.Windows.Forms.SplitterEventArgs)" /> has been replaced by <see cref="M:System.Windows.Forms.SplitContainer.OnSplitterMoved(System.Windows.Forms.SplitterEventArgs)" /> and is provided only for compatibility with previous versions.</summary>
		/// <param name="sevent">A <see cref="T:System.Windows.Forms.SplitterEventArgs" /> that contains the event data.</param>
		// Token: 0x0600397F RID: 14719 RVA: 0x000FF8CC File Offset: 0x000FDACC
		protected virtual void OnSplitterMoved(SplitterEventArgs sevent)
		{
			SplitterEventHandler splitterEventHandler = (SplitterEventHandler)base.Events[Splitter.EVENT_MOVED];
			if (splitterEventHandler != null)
			{
				splitterEventHandler(this, sevent);
			}
			if (this.splitTarget != null)
			{
				this.SplitMove(sevent.SplitX, sevent.SplitY);
			}
		}

		/// <summary>Performs the work of setting the specified bounds of this control.</summary>
		/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control.</param>
		/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control.</param>
		/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control.</param>
		/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control.</param>
		/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values.</param>
		// Token: 0x06003980 RID: 14720 RVA: 0x000FF914 File Offset: 0x000FDB14
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if (this.Horizontal)
			{
				if (width < 1)
				{
					width = 3;
				}
				this.splitterThickness = width;
			}
			else
			{
				if (height < 1)
				{
					height = 3;
				}
				this.splitterThickness = height;
			}
			base.SetBoundsCore(x, y, width, height, specified);
		}

		// Token: 0x06003981 RID: 14721 RVA: 0x000FF94C File Offset: 0x000FDB4C
		private void SplitBegin(int x, int y)
		{
			Splitter.SplitData splitData = this.CalcSplitBounds();
			if (splitData.target != null && this.minSize < this.maxSize)
			{
				this.anchor = new Point(x, y);
				this.splitTarget = splitData.target;
				this.splitSize = this.GetSplitSize(x, y);
				IntSecurity.UnmanagedCode.Assert();
				try
				{
					if (this.splitterMessageFilter != null)
					{
						this.splitterMessageFilter = new Splitter.SplitterMessageFilter(this);
					}
					Application.AddMessageFilter(this.splitterMessageFilter);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
				base.CaptureInternal = true;
				this.DrawSplitBar(1);
			}
		}

		// Token: 0x06003982 RID: 14722 RVA: 0x000FF9EC File Offset: 0x000FDBEC
		private void SplitEnd(bool accept)
		{
			this.DrawSplitBar(3);
			this.splitTarget = null;
			base.CaptureInternal = false;
			if (this.splitterMessageFilter != null)
			{
				Application.RemoveMessageFilter(this.splitterMessageFilter);
				this.splitterMessageFilter = null;
			}
			if (accept)
			{
				this.ApplySplitPosition();
			}
			else if (this.splitSize != this.initTargetSize)
			{
				this.SplitPosition = this.initTargetSize;
			}
			this.anchor = Point.Empty;
		}

		// Token: 0x06003983 RID: 14723 RVA: 0x000FFA58 File Offset: 0x000FDC58
		private void ApplySplitPosition()
		{
			this.SplitPosition = this.splitSize;
		}

		// Token: 0x06003984 RID: 14724 RVA: 0x000FFA68 File Offset: 0x000FDC68
		private void SplitMove(int x, int y)
		{
			int num = this.GetSplitSize(x - base.Left + this.anchor.X, y - base.Top + this.anchor.Y);
			if (this.splitSize != num)
			{
				this.splitSize = num;
				this.DrawSplitBar(2);
			}
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.Splitter" /> control.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.Splitter" />.</returns>
		// Token: 0x06003985 RID: 14725 RVA: 0x000FFABC File Offset: 0x000FDCBC
		public override string ToString()
		{
			string text = base.ToString();
			return string.Concat(new string[]
			{
				text,
				", MinExtra: ",
				this.MinExtra.ToString(CultureInfo.CurrentCulture),
				", MinSize: ",
				this.MinSize.ToString(CultureInfo.CurrentCulture)
			});
		}

		// Token: 0x040022B2 RID: 8882
		private const int DRAW_START = 1;

		// Token: 0x040022B3 RID: 8883
		private const int DRAW_MOVE = 2;

		// Token: 0x040022B4 RID: 8884
		private const int DRAW_END = 3;

		// Token: 0x040022B5 RID: 8885
		private const int defaultWidth = 3;

		// Token: 0x040022B6 RID: 8886
		private BorderStyle borderStyle;

		// Token: 0x040022B7 RID: 8887
		private int minSize = 25;

		// Token: 0x040022B8 RID: 8888
		private int minExtra = 25;

		// Token: 0x040022B9 RID: 8889
		private Point anchor = Point.Empty;

		// Token: 0x040022BA RID: 8890
		private Control splitTarget;

		// Token: 0x040022BB RID: 8891
		private int splitSize = -1;

		// Token: 0x040022BC RID: 8892
		private int splitterThickness = 3;

		// Token: 0x040022BD RID: 8893
		private int initTargetSize;

		// Token: 0x040022BE RID: 8894
		private int lastDrawSplit = -1;

		// Token: 0x040022BF RID: 8895
		private int maxSize;

		// Token: 0x040022C0 RID: 8896
		private static readonly object EVENT_MOVING = new object();

		// Token: 0x040022C1 RID: 8897
		private static readonly object EVENT_MOVED = new object();

		// Token: 0x040022C2 RID: 8898
		private Splitter.SplitterMessageFilter splitterMessageFilter;

		// Token: 0x020007E6 RID: 2022
		private class SplitData
		{
			// Token: 0x040042C1 RID: 17089
			public int dockWidth = -1;

			// Token: 0x040042C2 RID: 17090
			public int dockHeight = -1;

			// Token: 0x040042C3 RID: 17091
			internal Control target;
		}

		// Token: 0x020007E7 RID: 2023
		private class SplitterMessageFilter : IMessageFilter
		{
			// Token: 0x06006DCE RID: 28110 RVA: 0x001924C2 File Offset: 0x001906C2
			public SplitterMessageFilter(Splitter splitter)
			{
				this.owner = splitter;
			}

			// Token: 0x06006DCF RID: 28111 RVA: 0x001924D4 File Offset: 0x001906D4
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public bool PreFilterMessage(ref Message m)
			{
				if (m.Msg >= 256 && m.Msg <= 264)
				{
					if (m.Msg == 256 && (int)(long)m.WParam == 27)
					{
						this.owner.SplitEnd(false);
					}
					return true;
				}
				return false;
			}

			// Token: 0x040042C4 RID: 17092
			private Splitter owner;
		}
	}
}
