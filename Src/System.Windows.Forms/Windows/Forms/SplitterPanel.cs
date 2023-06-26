using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Creates a panel that is associated with a <see cref="T:System.Windows.Forms.SplitContainer" />.</summary>
	// Token: 0x02000372 RID: 882
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Docking(DockingBehavior.Never)]
	[Designer("System.Windows.Forms.Design.SplitterPanelDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxItem(false)]
	public sealed class SplitterPanel : Panel
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.SplitterPanel" /> class with its specified <see cref="T:System.Windows.Forms.SplitContainer" />.</summary>
		/// <param name="owner">The <see cref="T:System.Windows.Forms.SplitContainer" /> that contains the <see cref="T:System.Windows.Forms.SplitterPanel" />.</param>
		// Token: 0x0600399D RID: 14749 RVA: 0x000FFBE0 File Offset: 0x000FDDE0
		public SplitterPanel(SplitContainer owner)
		{
			this.owner = owner;
			base.SetStyle(ControlStyles.ResizeRedraw, true);
		}

		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x0600399E RID: 14750 RVA: 0x000FFBF8 File Offset: 0x000FDDF8
		// (set) Token: 0x0600399F RID: 14751 RVA: 0x000FFC00 File Offset: 0x000FDE00
		internal bool Collapsed
		{
			get
			{
				return this.collapsed;
			}
			set
			{
				this.collapsed = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.SplitterPanel" /> is automatically resized to display its entire contents. This property is not relevant to this class.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.SplitterPanel" /> is automatically resized; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x060039A0 RID: 14752 RVA: 0x000FFC09 File Offset: 0x000FDE09
		// (set) Token: 0x060039A1 RID: 14753 RVA: 0x000FFC11 File Offset: 0x000FDE11
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new bool AutoSize
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

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x140002C2 RID: 706
		// (add) Token: 0x060039A2 RID: 14754 RVA: 0x000FFC1A File Offset: 0x000FDE1A
		// (remove) Token: 0x060039A3 RID: 14755 RVA: 0x000FFC23 File Offset: 0x000FDE23
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		/// <summary>Enables the <see cref="T:System.Windows.Forms.SplitterPanel" /> to shrink when <see cref="P:System.Windows.Forms.SplitterPanel.AutoSize" /> is <see langword="true" />. This property is not relevant to this class.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AutoSizeMode" /> values. The default is <see cref="F:System.Windows.Forms.AutoSizeMode.GrowOnly" />.</returns>
		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x060039A4 RID: 14756 RVA: 0x00012E4E File Offset: 0x0001104E
		// (set) Token: 0x060039A5 RID: 14757 RVA: 0x000070A6 File Offset: 0x000052A6
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[Localizable(false)]
		public override AutoSizeMode AutoSizeMode
		{
			get
			{
				return AutoSizeMode.GrowOnly;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets how a <see cref="T:System.Windows.Forms.SplitterPanel" /> attaches to the edges of the <see cref="T:System.Windows.Forms.SplitContainer" />. This property is not relevant to this class.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AnchorStyles" /> values.</returns>
		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x060039A6 RID: 14758 RVA: 0x000FFC2C File Offset: 0x000FDE2C
		// (set) Token: 0x060039A7 RID: 14759 RVA: 0x000FFC34 File Offset: 0x000FDE34
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new AnchorStyles Anchor
		{
			get
			{
				return base.Anchor;
			}
			set
			{
				base.Anchor = value;
			}
		}

		/// <summary>Gets or sets the border style for the <see cref="T:System.Windows.Forms.SplitterPanel" />. This property is not relevant to this class.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values.</returns>
		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x060039A8 RID: 14760 RVA: 0x000FFC3D File Offset: 0x000FDE3D
		// (set) Token: 0x060039A9 RID: 14761 RVA: 0x000FFC45 File Offset: 0x000FDE45
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new BorderStyle BorderStyle
		{
			get
			{
				return base.BorderStyle;
			}
			set
			{
				base.BorderStyle = value;
			}
		}

		/// <summary>Gets or sets which edge of the <see cref="T:System.Windows.Forms.SplitContainer" /> that the <see cref="T:System.Windows.Forms.SplitterPanel" /> is docked to. This property is not relevant to this class.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values.</returns>
		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x060039AA RID: 14762 RVA: 0x000FC41E File Offset: 0x000FA61E
		// (set) Token: 0x060039AB RID: 14763 RVA: 0x000FFC4E File Offset: 0x000FDE4E
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new DockStyle Dock
		{
			get
			{
				return base.Dock;
			}
			set
			{
				base.Dock = value;
			}
		}

		/// <summary>Gets the internal spacing between the <see cref="T:System.Windows.Forms.SplitterPanel" /> and its edges. This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ScrollableControl.DockPaddingEdges" /> that represents the padding for all the edges of a docked control.</returns>
		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x060039AC RID: 14764 RVA: 0x000119C6 File Offset: 0x0000FBC6
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new ScrollableControl.DockPaddingEdges DockPadding
		{
			get
			{
				return base.DockPadding;
			}
		}

		/// <summary>Gets or sets the height of the <see cref="T:System.Windows.Forms.SplitterPanel" />.</summary>
		/// <returns>The height of the <see cref="T:System.Windows.Forms.SplitterPanel" />, in pixels.</returns>
		/// <exception cref="T:System.NotSupportedException">The height cannot be set.</exception>
		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x060039AD RID: 14765 RVA: 0x000FFC57 File Offset: 0x000FDE57
		// (set) Token: 0x060039AE RID: 14766 RVA: 0x000FFC69 File Offset: 0x000FDE69
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlHeightDescr")]
		public new int Height
		{
			get
			{
				if (this.Collapsed)
				{
					return 0;
				}
				return base.Height;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("SplitContainerPanelHeight"));
			}
		}

		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x060039AF RID: 14767 RVA: 0x000FFC7A File Offset: 0x000FDE7A
		// (set) Token: 0x060039B0 RID: 14768 RVA: 0x000FFC82 File Offset: 0x000FDE82
		internal int HeightInternal
		{
			get
			{
				return base.Height;
			}
			set
			{
				base.Height = value;
			}
		}

		/// <summary>Gets or sets the coordinates of the upper-left corner of the <see cref="T:System.Windows.Forms.SplitterPanel" /> relative to the upper-left corner of its <see cref="T:System.Windows.Forms.SplitContainer" />. This property is not relevant to this class.</summary>
		/// <returns>The <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of the <see cref="T:System.Windows.Forms.SplitterPanel" /> relative to the upper-left corner of its <see cref="T:System.Windows.Forms.SplitContainer" />.</returns>
		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x060039B1 RID: 14769 RVA: 0x000B15D1 File Offset: 0x000AF7D1
		// (set) Token: 0x060039B2 RID: 14770 RVA: 0x000B15D9 File Offset: 0x000AF7D9
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new Point Location
		{
			get
			{
				return base.Location;
			}
			set
			{
				base.Location = value;
			}
		}

		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x060039B3 RID: 14771 RVA: 0x000FFC8B File Offset: 0x000FDE8B
		protected override Padding DefaultMargin
		{
			get
			{
				return new Padding(0, 0, 0, 0);
			}
		}

		/// <summary>Gets or sets the size that is the lower limit that <see cref="M:System.Windows.Forms.Control.GetPreferredSize(System.Drawing.Size)" /> can specify. This property is not relevant to this class.</summary>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		/// <exception cref="T:System.NotSupportedException">The width cannot be set.</exception>
		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x060039B4 RID: 14772 RVA: 0x00011A2B File Offset: 0x0000FC2B
		// (set) Token: 0x060039B5 RID: 14773 RVA: 0x000FFC96 File Offset: 0x000FDE96
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new Size MinimumSize
		{
			get
			{
				return base.MinimumSize;
			}
			set
			{
				base.MinimumSize = value;
			}
		}

		/// <summary>Gets or sets the size that is the upper limit that <see cref="M:System.Windows.Forms.Control.GetPreferredSize(System.Drawing.Size)" /> can specify. This property is not relevant to this class.</summary>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x060039B6 RID: 14774 RVA: 0x00011A0E File Offset: 0x0000FC0E
		// (set) Token: 0x060039B7 RID: 14775 RVA: 0x000FFC9F File Offset: 0x000FDE9F
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new Size MaximumSize
		{
			get
			{
				return base.MaximumSize;
			}
			set
			{
				base.MaximumSize = value;
			}
		}

		/// <summary>The name of this <see cref="T:System.Windows.Forms.SplitterPanel" />. This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the name of this <see cref="T:System.Windows.Forms.SplitterPanel" />.</returns>
		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x060039B8 RID: 14776 RVA: 0x000FFCA8 File Offset: 0x000FDEA8
		// (set) Token: 0x060039B9 RID: 14777 RVA: 0x000FFCB0 File Offset: 0x000FDEB0
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x060039BA RID: 14778 RVA: 0x000FFCB9 File Offset: 0x000FDEB9
		internal SplitContainer Owner
		{
			get
			{
				return this.owner;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.SplitContainer" /> that contains this <see cref="T:System.Windows.Forms.SplitterPanel" />. This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Control" /> representing the <see cref="T:System.Windows.Forms.SplitContainer" /> that contains this <see cref="T:System.Windows.Forms.SplitterPanel" />.</returns>
		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x060039BB RID: 14779 RVA: 0x000FFCC1 File Offset: 0x000FDEC1
		// (set) Token: 0x060039BC RID: 14780 RVA: 0x000FFCC9 File Offset: 0x000FDEC9
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new Control Parent
		{
			get
			{
				return base.Parent;
			}
			set
			{
				base.Parent = value;
			}
		}

		/// <summary>Gets or sets the height and width of the <see cref="T:System.Windows.Forms.SplitterPanel" />. This property is not relevant to this class.</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" /> that represents the height and width of the <see cref="T:System.Windows.Forms.SplitterPanel" /> in pixels.</returns>
		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x060039BD RID: 14781 RVA: 0x000FFCD2 File Offset: 0x000FDED2
		// (set) Token: 0x060039BE RID: 14782 RVA: 0x000B22B7 File Offset: 0x000B04B7
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new Size Size
		{
			get
			{
				if (this.Collapsed)
				{
					return Size.Empty;
				}
				return base.Size;
			}
			set
			{
				base.Size = value;
			}
		}

		/// <summary>Gets or sets the tab order of the <see cref="T:System.Windows.Forms.SplitterPanel" /> within its <see cref="T:System.Windows.Forms.SplitContainer" />. This property is not relevant to this class.</summary>
		/// <returns>The index value of the <see cref="T:System.Windows.Forms.SplitterPanel" /> within the set of other <see cref="T:System.Windows.Forms.SplitterPanel" /> objects within its <see cref="T:System.Windows.Forms.SplitContainer" /> that are included in the tab order.</returns>
		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x060039BF RID: 14783 RVA: 0x000B2372 File Offset: 0x000B0572
		// (set) Token: 0x060039C0 RID: 14784 RVA: 0x000B237A File Offset: 0x000B057A
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new int TabIndex
		{
			get
			{
				return base.TabIndex;
			}
			set
			{
				base.TabIndex = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the user can give the focus to this <see cref="T:System.Windows.Forms.SplitterPanel" /> using the TAB key. This property is not relevant to this class.</summary>
		/// <returns>
		///   <see langword="true" /> if the user can give the focus to this <see cref="T:System.Windows.Forms.SplitterPanel" /> using the TAB key; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x060039C1 RID: 14785 RVA: 0x000FFCE8 File Offset: 0x000FDEE8
		// (set) Token: 0x060039C2 RID: 14786 RVA: 0x000FFCF0 File Offset: 0x000FDEF0
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
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

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.SplitterPanel" /> is displayed. This property is not relevant to this class.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.SplitterPanel" /> is displayed; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x060039C3 RID: 14787 RVA: 0x000FFCF9 File Offset: 0x000FDEF9
		// (set) Token: 0x060039C4 RID: 14788 RVA: 0x000FFD01 File Offset: 0x000FDF01
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = value;
			}
		}

		/// <summary>Gets or sets the width of the <see cref="T:System.Windows.Forms.SplitterPanel" />.</summary>
		/// <returns>The width of the <see cref="T:System.Windows.Forms.SplitterPanel" /> in pixels.</returns>
		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x060039C5 RID: 14789 RVA: 0x000FFD0A File Offset: 0x000FDF0A
		// (set) Token: 0x060039C6 RID: 14790 RVA: 0x000FFD1C File Offset: 0x000FDF1C
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ControlWidthDescr")]
		public new int Width
		{
			get
			{
				if (this.Collapsed)
				{
					return 0;
				}
				return base.Width;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("SplitContainerPanelWidth"));
			}
		}

		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x060039C7 RID: 14791 RVA: 0x000FFD2D File Offset: 0x000FDF2D
		// (set) Token: 0x060039C8 RID: 14792 RVA: 0x000FFD35 File Offset: 0x000FDF35
		internal int WidthInternal
		{
			get
			{
				return base.Width;
			}
			set
			{
				base.Width = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.SplitterPanel.Visible" /> property changes. This event is not relevant to this class.</summary>
		// Token: 0x140002C3 RID: 707
		// (add) Token: 0x060039C9 RID: 14793 RVA: 0x000FFD3E File Offset: 0x000FDF3E
		// (remove) Token: 0x060039CA RID: 14794 RVA: 0x000FFD47 File Offset: 0x000FDF47
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new event EventHandler VisibleChanged
		{
			add
			{
				base.VisibleChanged += value;
			}
			remove
			{
				base.VisibleChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.SplitterPanel.Dock" /> property changes. This event is not relevant to this class.</summary>
		// Token: 0x140002C4 RID: 708
		// (add) Token: 0x060039CB RID: 14795 RVA: 0x000FFD50 File Offset: 0x000FDF50
		// (remove) Token: 0x060039CC RID: 14796 RVA: 0x000FFD59 File Offset: 0x000FDF59
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new event EventHandler DockChanged
		{
			add
			{
				base.DockChanged += value;
			}
			remove
			{
				base.DockChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.SplitterPanel.Location" /> property changes. This event is not relevant to this class.</summary>
		// Token: 0x140002C5 RID: 709
		// (add) Token: 0x060039CD RID: 14797 RVA: 0x000FFD62 File Offset: 0x000FDF62
		// (remove) Token: 0x060039CE RID: 14798 RVA: 0x000FFD6B File Offset: 0x000FDF6B
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new event EventHandler LocationChanged
		{
			add
			{
				base.LocationChanged += value;
			}
			remove
			{
				base.LocationChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.SplitterPanel.TabIndex" /> property changes. This event is not relevant to this class.</summary>
		// Token: 0x140002C6 RID: 710
		// (add) Token: 0x060039CF RID: 14799 RVA: 0x000B2383 File Offset: 0x000B0583
		// (remove) Token: 0x060039D0 RID: 14800 RVA: 0x000B238C File Offset: 0x000B058C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new event EventHandler TabIndexChanged
		{
			add
			{
				base.TabIndexChanged += value;
			}
			remove
			{
				base.TabIndexChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.SplitterPanel.TabStop" /> property changes. This event is not relevant to this class.</summary>
		// Token: 0x140002C7 RID: 711
		// (add) Token: 0x060039D1 RID: 14801 RVA: 0x000B23A6 File Offset: 0x000B05A6
		// (remove) Token: 0x060039D2 RID: 14802 RVA: 0x000B23AF File Offset: 0x000B05AF
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
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

		// Token: 0x040022CB RID: 8907
		private SplitContainer owner;

		// Token: 0x040022CC RID: 8908
		private bool collapsed;
	}
}
