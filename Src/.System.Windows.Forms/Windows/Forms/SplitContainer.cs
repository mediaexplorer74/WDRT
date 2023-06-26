using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Represents a control consisting of a movable bar that divides a container's display area into two resizable panels.</summary>
	// Token: 0x0200036C RID: 876
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultEvent("SplitterMoved")]
	[Docking(DockingBehavior.AutoDock)]
	[Designer("System.Windows.Forms.Design.SplitContainerDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionSplitContainer")]
	public class SplitContainer : ContainerControl, ISupportInitialize
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.SplitContainer" /> class.</summary>
		// Token: 0x060038B3 RID: 14515 RVA: 0x000FC23C File Offset: 0x000FA43C
		public SplitContainer()
		{
			this.panel1 = new SplitterPanel(this);
			this.panel2 = new SplitterPanel(this);
			this.splitterRect = default(Rectangle);
			base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			((WindowsFormsUtils.TypedControlCollection)this.Controls).AddInternal(this.panel1);
			((WindowsFormsUtils.TypedControlCollection)this.Controls).AddInternal(this.panel2);
			this.UpdateSplitter();
		}

		/// <summary>When overridden in a derived class, gets or sets a value indicating whether scroll bars automatically appear if controls are placed outside the <see cref="T:System.Windows.Forms.SplitContainer" /> client area. This property is not relevant to this class.</summary>
		/// <returns>
		///   <see langword="true" /> if scroll bars to automatically appear when controls are placed outside the <see cref="T:System.Windows.Forms.SplitContainer" /> client area; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x060038B4 RID: 14516 RVA: 0x0001180C File Offset: 0x0000FA0C
		// (set) Token: 0x060038B5 RID: 14517 RVA: 0x000EC0F6 File Offset: 0x000EA2F6
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[DefaultValue(false)]
		[SRDescription("FormAutoScrollDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool AutoScroll
		{
			get
			{
				return false;
			}
			set
			{
				base.AutoScroll = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> value.</returns>
		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x060038B6 RID: 14518 RVA: 0x000FC322 File Offset: 0x000FA522
		// (set) Token: 0x060038B7 RID: 14519 RVA: 0x000FC32A File Offset: 0x000FA52A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(typeof(Point), "0, 0")]
		public override Point AutoScrollOffset
		{
			get
			{
				return base.AutoScrollOffset;
			}
			set
			{
				base.AutoScrollOffset = value;
			}
		}

		/// <summary>Gets or sets the minimum size of the scroll bar. This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the minimum height and width of the scroll bar, in pixels.</returns>
		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x060038B8 RID: 14520 RVA: 0x00011820 File Offset: 0x0000FA20
		// (set) Token: 0x060038B9 RID: 14521 RVA: 0x00011828 File Offset: 0x0000FA28
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
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

		/// <summary>Gets or sets the size of the auto-scroll margin. This property is not relevant to this class. This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> value that represents the height and width, in pixels, of the auto-scroll margin.</returns>
		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x060038BA RID: 14522 RVA: 0x0001180F File Offset: 0x0000FA0F
		// (set) Token: 0x060038BB RID: 14523 RVA: 0x00011817 File Offset: 0x0000FA17
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
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

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> value.</returns>
		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x060038BC RID: 14524 RVA: 0x000FC333 File Offset: 0x000FA533
		// (set) Token: 0x060038BD RID: 14525 RVA: 0x000FC33B File Offset: 0x000FA53B
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("FormAutoScrollPositionDescr")]
		public new Point AutoScrollPosition
		{
			get
			{
				return base.AutoScrollPosition;
			}
			set
			{
				base.AutoScrollPosition = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.SplitContainer" /> is automatically resized to display its entire contents. This property is not relevant to this class.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.SplitContainer" /> is automatically resized; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x060038BE RID: 14526 RVA: 0x00011831 File Offset: 0x0000FA31
		// (set) Token: 0x060038BF RID: 14527 RVA: 0x00011839 File Offset: 0x0000FA39
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.SplitContainer.AutoSize" /> property changes. This property is not relevant to this class.</summary>
		// Token: 0x140002AB RID: 683
		// (add) Token: 0x060038C0 RID: 14528 RVA: 0x00011842 File Offset: 0x0000FA42
		// (remove) Token: 0x060038C1 RID: 14529 RVA: 0x0001184B File Offset: 0x0000FA4B
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

		/// <summary>Gets or sets the background image displayed in the control.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> that represents the image to display in the background of the control.</returns>
		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x060038C2 RID: 14530 RVA: 0x0001187C File Offset: 0x0000FA7C
		// (set) Token: 0x060038C3 RID: 14531 RVA: 0x00011884 File Offset: 0x0000FA84
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
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

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageLayout" /> value.</returns>
		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x060038C4 RID: 14532 RVA: 0x0001189F File Offset: 0x0000FA9F
		// (set) Token: 0x060038C5 RID: 14533 RVA: 0x000118A7 File Offset: 0x0000FAA7
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

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.BindingContext" /> for the <see cref="T:System.Windows.Forms.SplitContainer" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.BindingContext" /> for the control.</returns>
		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x060038C6 RID: 14534 RVA: 0x0002FB8D File Offset: 0x0002DD8D
		// (set) Token: 0x060038C7 RID: 14535 RVA: 0x0002FB95 File Offset: 0x0002DD95
		[Browsable(false)]
		[SRDescription("ContainerControlBindingContextDescr")]
		public override BindingContext BindingContext
		{
			get
			{
				return base.BindingContextInternal;
			}
			set
			{
				base.BindingContextInternal = value;
			}
		}

		/// <summary>Gets or sets the style of border for the <see cref="T:System.Windows.Forms.SplitContainer" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. The default is <see cref="F:System.Windows.Forms.BorderStyle.Fixed3D" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value of the property is not one of the <see cref="T:System.Windows.Forms.BorderStyle" /> values.</exception>
		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x060038C8 RID: 14536 RVA: 0x000FC344 File Offset: 0x000FA544
		// (set) Token: 0x060038C9 RID: 14537 RVA: 0x000FC34C File Offset: 0x000FA54C
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
					base.Invalidate();
					this.SetInnerMostBorder(this);
					if (this.ParentInternal != null && this.ParentInternal is SplitterPanel)
					{
						SplitContainer owner = ((SplitterPanel)this.ParentInternal).Owner;
						owner.SetInnerMostBorder(owner);
					}
				}
				switch (this.BorderStyle)
				{
				case BorderStyle.None:
					this.BORDERSIZE = 0;
					return;
				case BorderStyle.FixedSingle:
					this.BORDERSIZE = 1;
					return;
				case BorderStyle.Fixed3D:
					this.BORDERSIZE = 4;
					return;
				default:
					return;
				}
			}
		}

		/// <summary>Gets a collection of child controls. This property is not relevant to this class.</summary>
		/// <returns>An object of type <see cref="T:System.Windows.Forms.Control.ControlCollection" /> that contains the child controls.</returns>
		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x060038CA RID: 14538 RVA: 0x000EC38A File Offset: 0x000EA58A
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Control.ControlCollection Controls
		{
			get
			{
				return base.Controls;
			}
		}

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x140002AC RID: 684
		// (add) Token: 0x060038CB RID: 14539 RVA: 0x000FC3FA File Offset: 0x000FA5FA
		// (remove) Token: 0x060038CC RID: 14540 RVA: 0x000FC403 File Offset: 0x000FA603
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event ControlEventHandler ControlAdded
		{
			add
			{
				base.ControlAdded += value;
			}
			remove
			{
				base.ControlAdded -= value;
			}
		}

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x140002AD RID: 685
		// (add) Token: 0x060038CD RID: 14541 RVA: 0x000FC40C File Offset: 0x000FA60C
		// (remove) Token: 0x060038CE RID: 14542 RVA: 0x000FC415 File Offset: 0x000FA615
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event ControlEventHandler ControlRemoved
		{
			add
			{
				base.ControlRemoved += value;
			}
			remove
			{
				base.ControlRemoved -= value;
			}
		}

		/// <summary>Gets or sets which <see cref="T:System.Windows.Forms.SplitContainer" /> borders are attached to the edges of the container.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values. The default value is <see langword="None" />.</returns>
		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x060038CF RID: 14543 RVA: 0x000FC41E File Offset: 0x000FA61E
		// (set) Token: 0x060038D0 RID: 14544 RVA: 0x000FC428 File Offset: 0x000FA628
		public new DockStyle Dock
		{
			get
			{
				return base.Dock;
			}
			set
			{
				base.Dock = value;
				if (this.ParentInternal != null && this.ParentInternal is SplitterPanel)
				{
					SplitContainer owner = ((SplitterPanel)this.ParentInternal).Owner;
					owner.SetInnerMostBorder(owner);
				}
				this.ResizeSplitContainer();
			}
		}

		/// <summary>Gets the default size of the <see cref="T:System.Windows.Forms.SplitContainer" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the size of the <see cref="T:System.Windows.Forms.SplitContainer" />.</returns>
		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x060038D1 RID: 14545 RVA: 0x000FC46F File Offset: 0x000FA66F
		protected override Size DefaultSize
		{
			get
			{
				return new Size(150, 100);
			}
		}

		/// <summary>Gets or sets which <see cref="T:System.Windows.Forms.SplitContainer" /> panel remains the same size when the container is resized.</summary>
		/// <returns>One of the values of <see cref="T:System.Windows.Forms.FixedPanel" />. The default value is <see langword="None" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.FixedPanel" /> values.</exception>
		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x060038D2 RID: 14546 RVA: 0x000FC47D File Offset: 0x000FA67D
		// (set) Token: 0x060038D3 RID: 14547 RVA: 0x000FC488 File Offset: 0x000FA688
		[DefaultValue(FixedPanel.None)]
		[SRCategory("CatLayout")]
		[SRDescription("SplitContainerFixedPanelDescr")]
		public FixedPanel FixedPanel
		{
			get
			{
				return this.fixedPanel;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(FixedPanel));
				}
				if (this.fixedPanel != value)
				{
					this.fixedPanel = value;
					FixedPanel fixedPanel = this.fixedPanel;
					if (fixedPanel == FixedPanel.Panel2)
					{
						if (this.Orientation == Orientation.Vertical)
						{
							this.panelSize = base.Width - this.SplitterDistanceInternal - this.SplitterWidthInternal;
							return;
						}
						this.panelSize = base.Height - this.SplitterDistanceInternal - this.SplitterWidthInternal;
						return;
					}
					else
					{
						this.panelSize = this.SplitterDistanceInternal;
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the splitter is fixed or movable.</summary>
		/// <returns>
		///   <see langword="true" /> if the splitter is fixed; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x060038D4 RID: 14548 RVA: 0x000FC521 File Offset: 0x000FA721
		// (set) Token: 0x060038D5 RID: 14549 RVA: 0x000FC529 File Offset: 0x000FA729
		[SRCategory("CatLayout")]
		[DefaultValue(false)]
		[Localizable(true)]
		[SRDescription("SplitContainerIsSplitterFixedDescr")]
		public bool IsSplitterFixed
		{
			get
			{
				return this.splitterFixed;
			}
			set
			{
				this.splitterFixed = value;
			}
		}

		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x060038D6 RID: 14550 RVA: 0x000FC534 File Offset: 0x000FA734
		private bool IsSplitterMovable
		{
			get
			{
				if (this.Orientation == Orientation.Vertical)
				{
					return base.Width >= this.Panel1MinSize + this.SplitterWidthInternal + this.Panel2MinSize;
				}
				return base.Height >= this.Panel1MinSize + this.SplitterWidthInternal + this.Panel2MinSize;
			}
		}

		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x060038D7 RID: 14551 RVA: 0x00012E4E File Offset: 0x0001104E
		internal override bool IsContainerControl
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets or sets a value indicating the horizontal or vertical orientation of the <see cref="T:System.Windows.Forms.SplitContainer" /> panels.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.Orientation" /> values. The default is <see langword="Vertical" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.Orientation" /> values.</exception>
		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x060038D8 RID: 14552 RVA: 0x000FC589 File Offset: 0x000FA789
		// (set) Token: 0x060038D9 RID: 14553 RVA: 0x000FC594 File Offset: 0x000FA794
		[SRCategory("CatBehavior")]
		[DefaultValue(Orientation.Vertical)]
		[Localizable(true)]
		[SRDescription("SplitContainerOrientationDescr")]
		public Orientation Orientation
		{
			get
			{
				return this.orientation;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 1))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(Orientation));
				}
				if (this.orientation != value)
				{
					this.orientation = value;
					this.splitDistance = 0;
					this.SplitterDistance = this.SplitterDistanceInternal;
					this.UpdateSplitter();
				}
			}
		}

		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x060038DA RID: 14554 RVA: 0x000FC5F0 File Offset: 0x000FA7F0
		// (set) Token: 0x060038DB RID: 14555 RVA: 0x000FC5F8 File Offset: 0x000FA7F8
		private Cursor OverrideCursor
		{
			get
			{
				return this.overrideCursor;
			}
			set
			{
				if (this.overrideCursor != value)
				{
					this.overrideCursor = value;
					if (base.IsHandleCreated)
					{
						NativeMethods.POINT point = new NativeMethods.POINT();
						NativeMethods.RECT rect = default(NativeMethods.RECT);
						UnsafeNativeMethods.GetCursorPos(point);
						UnsafeNativeMethods.GetWindowRect(new HandleRef(this, base.Handle), ref rect);
						if ((rect.left <= point.x && point.x < rect.right && rect.top <= point.y && point.y < rect.bottom) || UnsafeNativeMethods.GetCapture() == base.Handle)
						{
							base.SendMessage(32, base.Handle, 1);
						}
					}
				}
			}
		}

		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x060038DC RID: 14556 RVA: 0x000FC6AB File Offset: 0x000FA8AB
		private bool CollapsedMode
		{
			get
			{
				return this.Panel1Collapsed || this.Panel2Collapsed;
			}
		}

		/// <summary>Gets the left or top panel of the <see cref="T:System.Windows.Forms.SplitContainer" />, depending on <see cref="P:System.Windows.Forms.SplitContainer.Orientation" />.</summary>
		/// <returns>If <see cref="P:System.Windows.Forms.SplitContainer.Orientation" /> is <see langword="Vertical" />, the left panel of the <see cref="T:System.Windows.Forms.SplitContainer" />. If <see cref="P:System.Windows.Forms.SplitContainer.Orientation" /> is <see langword="Horizontal" />, the top panel of the <see cref="T:System.Windows.Forms.SplitContainer" />.</returns>
		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x060038DD RID: 14557 RVA: 0x000FC6BD File Offset: 0x000FA8BD
		[SRCategory("CatAppearance")]
		[SRDescription("SplitContainerPanel1Descr")]
		[Localizable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public SplitterPanel Panel1
		{
			get
			{
				return this.panel1;
			}
		}

		// Token: 0x060038DE RID: 14558 RVA: 0x000FC6C5 File Offset: 0x000FA8C5
		private void CollapsePanel(SplitterPanel p, bool collapsing)
		{
			p.Collapsed = collapsing;
			if (collapsing)
			{
				p.Visible = false;
			}
			else
			{
				p.Visible = true;
			}
			this.UpdateSplitter();
		}

		/// <summary>Gets or sets the interior spacing, in pixels, between the edges of a <see cref="T:System.Windows.Forms.SplitterPanel" /> and its contents. This property is not relevant to this class.</summary>
		/// <returns>An object of type <see cref="T:System.Windows.Forms.Padding" /> representing the interior spacing.</returns>
		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x060038DF RID: 14559 RVA: 0x00013442 File Offset: 0x00011642
		// (set) Token: 0x060038E0 RID: 14560 RVA: 0x0001344A File Offset: 0x0001164A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Padding Padding
		{
			get
			{
				return base.Padding;
			}
			set
			{
				base.Padding = value;
			}
		}

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x140002AE RID: 686
		// (add) Token: 0x060038E1 RID: 14561 RVA: 0x00013453 File Offset: 0x00011653
		// (remove) Token: 0x060038E2 RID: 14562 RVA: 0x0001345C File Offset: 0x0001165C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler PaddingChanged
		{
			add
			{
				base.PaddingChanged += value;
			}
			remove
			{
				base.PaddingChanged -= value;
			}
		}

		/// <summary>Gets or sets a value determining whether <see cref="P:System.Windows.Forms.SplitContainer.Panel1" /> is collapsed or expanded.</summary>
		/// <returns>
		///   <see langword="true" /> if <see cref="P:System.Windows.Forms.SplitContainer.Panel1" /> is collapsed; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x060038E3 RID: 14563 RVA: 0x000FC6E7 File Offset: 0x000FA8E7
		// (set) Token: 0x060038E4 RID: 14564 RVA: 0x000FC6F4 File Offset: 0x000FA8F4
		[SRCategory("CatLayout")]
		[DefaultValue(false)]
		[SRDescription("SplitContainerPanel1CollapsedDescr")]
		public bool Panel1Collapsed
		{
			get
			{
				return this.panel1.Collapsed;
			}
			set
			{
				if (value != this.panel1.Collapsed)
				{
					if (value && this.panel2.Collapsed)
					{
						this.CollapsePanel(this.panel2, false);
					}
					this.CollapsePanel(this.panel1, value);
				}
			}
		}

		/// <summary>Gets or sets a value determining whether <see cref="P:System.Windows.Forms.SplitContainer.Panel2" /> is collapsed or expanded.</summary>
		/// <returns>
		///   <see langword="true" /> if <see cref="P:System.Windows.Forms.SplitContainer.Panel2" /> is collapsed; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x060038E5 RID: 14565 RVA: 0x000FC72E File Offset: 0x000FA92E
		// (set) Token: 0x060038E6 RID: 14566 RVA: 0x000FC73B File Offset: 0x000FA93B
		[SRCategory("CatLayout")]
		[DefaultValue(false)]
		[SRDescription("SplitContainerPanel2CollapsedDescr")]
		public bool Panel2Collapsed
		{
			get
			{
				return this.panel2.Collapsed;
			}
			set
			{
				if (value != this.panel2.Collapsed)
				{
					if (value && this.panel1.Collapsed)
					{
						this.CollapsePanel(this.panel1, false);
					}
					this.CollapsePanel(this.panel2, value);
				}
			}
		}

		/// <summary>Gets or sets the minimum distance in pixels of the splitter from the left or top edge of <see cref="P:System.Windows.Forms.SplitContainer.Panel1" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the minimum distance in pixels of the splitter from the left or top edge of <see cref="P:System.Windows.Forms.SplitContainer.Panel1" />. The default value is 25 pixels, regardless of <see cref="P:System.Windows.Forms.SplitContainer.Orientation" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is incompatible with the orientation.</exception>
		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x060038E7 RID: 14567 RVA: 0x000FC775 File Offset: 0x000FA975
		// (set) Token: 0x060038E8 RID: 14568 RVA: 0x000FC77D File Offset: 0x000FA97D
		[SRCategory("CatLayout")]
		[DefaultValue(25)]
		[Localizable(true)]
		[SRDescription("SplitContainerPanel1MinSizeDescr")]
		[RefreshProperties(RefreshProperties.All)]
		public int Panel1MinSize
		{
			get
			{
				return this.panel1MinSize;
			}
			set
			{
				this.newPanel1MinSize = value;
				if (value != this.Panel1MinSize && !this.initializing)
				{
					this.ApplyPanel1MinSize(value);
				}
			}
		}

		/// <summary>Gets the right or bottom panel of the <see cref="T:System.Windows.Forms.SplitContainer" />, depending on <see cref="P:System.Windows.Forms.SplitContainer.Orientation" />.</summary>
		/// <returns>If <see cref="P:System.Windows.Forms.SplitContainer.Orientation" /> is <see langword="Vertical" />, the right panel of the <see cref="T:System.Windows.Forms.SplitContainer" />. If <see cref="P:System.Windows.Forms.SplitContainer.Orientation" /> is <see langword="Horizontal" />, the bottom panel of the <see cref="T:System.Windows.Forms.SplitContainer" />.</returns>
		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x060038E9 RID: 14569 RVA: 0x000FC79E File Offset: 0x000FA99E
		[SRCategory("CatAppearance")]
		[SRDescription("SplitContainerPanel2Descr")]
		[Localizable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public SplitterPanel Panel2
		{
			get
			{
				return this.panel2;
			}
		}

		/// <summary>Gets or sets the minimum distance in pixels of the splitter from the right or bottom edge of <see cref="P:System.Windows.Forms.SplitContainer.Panel2" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the minimum distance in pixels of the splitter from the right or bottom edge of <see cref="P:System.Windows.Forms.SplitContainer.Panel2" />. The default value is 25 pixels, regardless of <see cref="P:System.Windows.Forms.SplitContainer.Orientation" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is incompatible with the orientation.</exception>
		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x060038EA RID: 14570 RVA: 0x000FC7A6 File Offset: 0x000FA9A6
		// (set) Token: 0x060038EB RID: 14571 RVA: 0x000FC7AE File Offset: 0x000FA9AE
		[SRCategory("CatLayout")]
		[DefaultValue(25)]
		[Localizable(true)]
		[SRDescription("SplitContainerPanel2MinSizeDescr")]
		[RefreshProperties(RefreshProperties.All)]
		public int Panel2MinSize
		{
			get
			{
				return this.panel2MinSize;
			}
			set
			{
				this.newPanel2MinSize = value;
				if (value != this.Panel2MinSize && !this.initializing)
				{
					this.ApplyPanel2MinSize(value);
				}
			}
		}

		/// <summary>Gets or sets the location of the splitter, in pixels, from the left or top edge of the <see cref="T:System.Windows.Forms.SplitContainer" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the location of the splitter, in pixels, from the left or top edge of the <see cref="T:System.Windows.Forms.SplitContainer" />. The default value is 50 pixels.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">The value is incompatible with the orientation.</exception>
		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x060038EC RID: 14572 RVA: 0x000FC7CF File Offset: 0x000FA9CF
		// (set) Token: 0x060038ED RID: 14573 RVA: 0x000FC7D8 File Offset: 0x000FA9D8
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[SettingsBindable(true)]
		[SRDescription("SplitContainerSplitterDistanceDescr")]
		[DefaultValue(50)]
		public int SplitterDistance
		{
			get
			{
				return this.splitDistance;
			}
			set
			{
				if (value != this.SplitterDistance)
				{
					if (value < 0)
					{
						throw new ArgumentOutOfRangeException("SplitterDistance", SR.GetString("InvalidLowBoundArgument", new object[]
						{
							"SplitterDistance",
							value.ToString(CultureInfo.CurrentCulture),
							"0"
						}));
					}
					try
					{
						this.setSplitterDistance = true;
						if (this.Orientation == Orientation.Vertical)
						{
							if (value < this.Panel1MinSize)
							{
								value = this.Panel1MinSize;
							}
							if (value + this.SplitterWidthInternal > base.Width - this.Panel2MinSize)
							{
								value = base.Width - this.Panel2MinSize - this.SplitterWidthInternal;
							}
							if (value < 0)
							{
								throw new InvalidOperationException(SR.GetString("SplitterDistanceNotAllowed"));
							}
							this.splitDistance = value;
							this.splitterDistance = value;
							this.panel1.WidthInternal = this.SplitterDistance;
						}
						else
						{
							if (value < this.Panel1MinSize)
							{
								value = this.Panel1MinSize;
							}
							if (value + this.SplitterWidthInternal > base.Height - this.Panel2MinSize)
							{
								value = base.Height - this.Panel2MinSize - this.SplitterWidthInternal;
							}
							if (value < 0)
							{
								throw new InvalidOperationException(SR.GetString("SplitterDistanceNotAllowed"));
							}
							this.splitDistance = value;
							this.splitterDistance = value;
							this.panel1.HeightInternal = this.SplitterDistance;
						}
						FixedPanel fixedPanel = this.fixedPanel;
						if (fixedPanel != FixedPanel.Panel1)
						{
							if (fixedPanel == FixedPanel.Panel2)
							{
								if (this.Orientation == Orientation.Vertical)
								{
									this.panelSize = base.Width - this.SplitterDistance - this.SplitterWidthInternal;
								}
								else
								{
									this.panelSize = base.Height - this.SplitterDistance - this.SplitterWidthInternal;
								}
							}
						}
						else
						{
							this.panelSize = this.SplitterDistance;
						}
						this.UpdateSplitter();
					}
					finally
					{
						this.setSplitterDistance = false;
					}
					this.OnSplitterMoved(new SplitterEventArgs(this.SplitterRectangle.X + this.SplitterRectangle.Width / 2, this.SplitterRectangle.Y + this.SplitterRectangle.Height / 2, this.SplitterRectangle.X, this.SplitterRectangle.Y));
				}
			}
		}

		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x060038EE RID: 14574 RVA: 0x000FCA14 File Offset: 0x000FAC14
		// (set) Token: 0x060038EF RID: 14575 RVA: 0x000FCA1C File Offset: 0x000FAC1C
		private int SplitterDistanceInternal
		{
			get
			{
				return this.splitterDistance;
			}
			set
			{
				this.SplitterDistance = value;
			}
		}

		/// <summary>Gets or sets a value representing the increment of splitter movement in pixels.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the increment of splitter movement in pixels. The default value is one pixel.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than one.</exception>
		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x060038F0 RID: 14576 RVA: 0x000FCA25 File Offset: 0x000FAC25
		// (set) Token: 0x060038F1 RID: 14577 RVA: 0x000FCA30 File Offset: 0x000FAC30
		[SRCategory("CatLayout")]
		[DefaultValue(1)]
		[Localizable(true)]
		[SRDescription("SplitContainerSplitterIncrementDescr")]
		public int SplitterIncrement
		{
			get
			{
				return this.splitterInc;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("SplitterIncrement", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"SplitterIncrement",
						value.ToString(CultureInfo.CurrentCulture),
						"1"
					}));
				}
				this.splitterInc = value;
			}
		}

		/// <summary>Gets the size and location of the splitter relative to the <see cref="T:System.Windows.Forms.SplitContainer" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that specifies the size and location of the splitter relative to the <see cref="T:System.Windows.Forms.SplitContainer" />.</returns>
		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x060038F2 RID: 14578 RVA: 0x000FCA84 File Offset: 0x000FAC84
		[SRCategory("CatLayout")]
		[SRDescription("SplitContainerSplitterRectangleDescr")]
		[Browsable(false)]
		public Rectangle SplitterRectangle
		{
			get
			{
				Rectangle rectangle = this.splitterRect;
				rectangle.X = this.splitterRect.X - base.Left;
				rectangle.Y = this.splitterRect.Y - base.Top;
				return rectangle;
			}
		}

		/// <summary>Gets or sets the width of the splitter in pixels.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the width of the splitter, in pixels. The default is four pixels.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than one or is incompatible with the orientation.</exception>
		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x060038F3 RID: 14579 RVA: 0x000FCACB File Offset: 0x000FACCB
		// (set) Token: 0x060038F4 RID: 14580 RVA: 0x000FCAD3 File Offset: 0x000FACD3
		[SRCategory("CatLayout")]
		[SRDescription("SplitContainerSplitterWidthDescr")]
		[Localizable(true)]
		[DefaultValue(4)]
		public int SplitterWidth
		{
			get
			{
				return this.splitterWidth;
			}
			set
			{
				this.newSplitterWidth = value;
				if (value != this.SplitterWidth && !this.initializing)
				{
					this.ApplySplitterWidth(value);
				}
			}
		}

		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x060038F5 RID: 14581 RVA: 0x000FCAF4 File Offset: 0x000FACF4
		private int SplitterWidthInternal
		{
			get
			{
				if (!this.CollapsedMode)
				{
					return this.splitterWidth;
				}
				return 0;
			}
		}

		/// <summary>Gets or sets a value indicating whether the user can give the focus to the splitter using the TAB key.</summary>
		/// <returns>
		///   <see langword="true" /> if the user can give the focus to the splitter using the TAB key; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x060038F6 RID: 14582 RVA: 0x000FCB06 File Offset: 0x000FAD06
		// (set) Token: 0x060038F7 RID: 14583 RVA: 0x000FCB0E File Offset: 0x000FAD0E
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[DispId(-516)]
		[SRDescription("ControlTabStopDescr")]
		public new bool TabStop
		{
			get
			{
				return this.tabStop;
			}
			set
			{
				if (this.TabStop != value)
				{
					this.tabStop = value;
					this.OnTabStopChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A string.</returns>
		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x060038F8 RID: 14584 RVA: 0x00013814 File Offset: 0x00011A14
		// (set) Token: 0x060038F9 RID: 14585 RVA: 0x00023FE9 File Offset: 0x000221E9
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Bindable(false)]
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

		/// <summary>Signals the object that initialization is started.</summary>
		// Token: 0x060038FA RID: 14586 RVA: 0x000FCB2B File Offset: 0x000FAD2B
		public void BeginInit()
		{
			this.initializing = true;
		}

		/// <summary>Signals the object that initialization is complete.</summary>
		// Token: 0x060038FB RID: 14587 RVA: 0x000FCB34 File Offset: 0x000FAD34
		public void EndInit()
		{
			this.initializing = false;
			if (this.newPanel1MinSize != this.panel1MinSize)
			{
				this.ApplyPanel1MinSize(this.newPanel1MinSize);
			}
			if (this.newPanel2MinSize != this.panel2MinSize)
			{
				this.ApplyPanel2MinSize(this.newPanel2MinSize);
			}
			if (this.newSplitterWidth != this.splitterWidth)
			{
				this.ApplySplitterWidth(this.newSplitterWidth);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.SplitContainer.BackgroundImage" /> property changes.</summary>
		// Token: 0x140002AF RID: 687
		// (add) Token: 0x060038FC RID: 14588 RVA: 0x0001188D File Offset: 0x0000FA8D
		// (remove) Token: 0x060038FD RID: 14589 RVA: 0x00011896 File Offset: 0x0000FA96
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.SplitContainer.BackgroundImageLayout" /> property changes. This event is not relevant to this class.</summary>
		// Token: 0x140002B0 RID: 688
		// (add) Token: 0x060038FE RID: 14590 RVA: 0x000118B0 File Offset: 0x0000FAB0
		// (remove) Token: 0x060038FF RID: 14591 RVA: 0x000118B9 File Offset: 0x0000FAB9
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

		/// <summary>Occurs when the splitter control is in the process of moving.</summary>
		// Token: 0x140002B1 RID: 689
		// (add) Token: 0x06003900 RID: 14592 RVA: 0x000FCB96 File Offset: 0x000FAD96
		// (remove) Token: 0x06003901 RID: 14593 RVA: 0x000FCBA9 File Offset: 0x000FADA9
		[SRCategory("CatBehavior")]
		[SRDescription("SplitterSplitterMovingDescr")]
		public event SplitterCancelEventHandler SplitterMoving
		{
			add
			{
				base.Events.AddHandler(SplitContainer.EVENT_MOVING, value);
			}
			remove
			{
				base.Events.RemoveHandler(SplitContainer.EVENT_MOVING, value);
			}
		}

		/// <summary>Occurs when the splitter control is moved.</summary>
		// Token: 0x140002B2 RID: 690
		// (add) Token: 0x06003902 RID: 14594 RVA: 0x000FCBBC File Offset: 0x000FADBC
		// (remove) Token: 0x06003903 RID: 14595 RVA: 0x000FCBCF File Offset: 0x000FADCF
		[SRCategory("CatBehavior")]
		[SRDescription("SplitterSplitterMovedDescr")]
		public event SplitterEventHandler SplitterMoved
		{
			add
			{
				base.Events.AddHandler(SplitContainer.EVENT_MOVED, value);
			}
			remove
			{
				base.Events.RemoveHandler(SplitContainer.EVENT_MOVED, value);
			}
		}

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x140002B3 RID: 691
		// (add) Token: 0x06003904 RID: 14596 RVA: 0x00046591 File Offset: 0x00044791
		// (remove) Token: 0x06003905 RID: 14597 RVA: 0x0004659A File Offset: 0x0004479A
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

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003906 RID: 14598 RVA: 0x00024227 File Offset: 0x00022427
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			base.Invalidate();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		// Token: 0x06003907 RID: 14599 RVA: 0x000FCBE4 File Offset: 0x000FADE4
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (this.IsSplitterMovable && !this.IsSplitterFixed)
			{
				if (e.KeyData == Keys.Escape && this.splitBegin)
				{
					this.splitBegin = false;
					this.splitBreak = true;
					return;
				}
				if (e.KeyData == Keys.Right || e.KeyData == Keys.Down || e.KeyData == Keys.Left || (e.KeyData == Keys.Up && this.splitterFocused))
				{
					if (this.splitBegin)
					{
						this.splitMove = true;
					}
					if (e.KeyData == Keys.Left || (e.KeyData == Keys.Up && this.splitterFocused))
					{
						this.splitterDistance -= this.SplitterIncrement;
						this.splitterDistance = ((this.splitterDistance < this.Panel1MinSize) ? (this.splitterDistance + this.SplitterIncrement) : Math.Max(this.splitterDistance, this.BORDERSIZE));
					}
					if (e.KeyData == Keys.Right || (e.KeyData == Keys.Down && this.splitterFocused))
					{
						this.splitterDistance += this.SplitterIncrement;
						if (this.Orientation == Orientation.Vertical)
						{
							this.splitterDistance = ((this.splitterDistance + this.SplitterWidth > base.Width - this.Panel2MinSize - this.BORDERSIZE) ? (this.splitterDistance - this.SplitterIncrement) : this.splitterDistance);
						}
						else
						{
							this.splitterDistance = ((this.splitterDistance + this.SplitterWidth > base.Height - this.Panel2MinSize - this.BORDERSIZE) ? (this.splitterDistance - this.SplitterIncrement) : this.splitterDistance);
						}
					}
					if (!this.splitBegin)
					{
						this.splitBegin = true;
					}
					if (this.splitBegin && !this.splitMove)
					{
						this.initialSplitterDistance = this.SplitterDistanceInternal;
						this.DrawSplitBar(1);
						return;
					}
					this.DrawSplitBar(2);
					Rectangle rectangle = this.CalcSplitLine(this.splitterDistance, 0);
					int x = rectangle.X;
					int y = rectangle.Y;
					SplitterCancelEventArgs splitterCancelEventArgs = new SplitterCancelEventArgs(base.Left + this.SplitterRectangle.X + this.SplitterRectangle.Width / 2, base.Top + this.SplitterRectangle.Y + this.SplitterRectangle.Height / 2, x, y);
					this.OnSplitterMoving(splitterCancelEventArgs);
					if (splitterCancelEventArgs.Cancel)
					{
						this.SplitEnd(false);
					}
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		// Token: 0x06003908 RID: 14600 RVA: 0x000FCE5C File Offset: 0x000FB05C
		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
			if (this.splitBegin && this.IsSplitterMovable && (e.KeyData == Keys.Right || e.KeyData == Keys.Down || e.KeyData == Keys.Left || (e.KeyData == Keys.Up && this.splitterFocused)))
			{
				this.DrawSplitBar(3);
				this.ApplySplitterDistance();
				this.splitBegin = false;
				this.splitMove = false;
			}
			if (this.splitBreak)
			{
				this.splitBreak = false;
				this.SplitEnd(false);
			}
			using (Graphics graphics = base.CreateGraphicsInternal())
			{
				if (this.BackgroundImage == null)
				{
					using (SolidBrush solidBrush = new SolidBrush(this.BackColor))
					{
						graphics.FillRectangle(solidBrush, this.SplitterRectangle);
					}
				}
				this.DrawFocus(graphics, this.SplitterRectangle);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
		// Token: 0x06003909 RID: 14601 RVA: 0x000FCF4C File Offset: 0x000FB14C
		protected override void OnLayout(LayoutEventArgs e)
		{
			this.SetInnerMostBorder(this);
			if (this.IsSplitterMovable && !this.setSplitterDistance)
			{
				this.ResizeSplitContainer();
			}
			base.OnLayout(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600390A RID: 14602 RVA: 0x000FCF72 File Offset: 0x000FB172
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			base.Invalidate();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x0600390B RID: 14603 RVA: 0x000FCF84 File Offset: 0x000FB184
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (!this.IsSplitterFixed && this.IsSplitterMovable)
			{
				if (this.Cursor == this.DefaultCursor && this.SplitterRectangle.Contains(e.Location))
				{
					if (this.Orientation == Orientation.Vertical)
					{
						this.OverrideCursor = Cursors.VSplit;
					}
					else
					{
						this.OverrideCursor = Cursors.HSplit;
					}
				}
				else
				{
					this.OverrideCursor = null;
				}
				if (this.splitterClick)
				{
					int num = e.X;
					int num2 = e.Y;
					this.splitterDrag = true;
					this.SplitMove(num, num2);
					if (this.Orientation == Orientation.Vertical)
					{
						num = Math.Max(Math.Min(num, base.Width - this.Panel2MinSize), this.Panel1MinSize);
						num2 = Math.Max(num2, 0);
					}
					else
					{
						num2 = Math.Max(Math.Min(num2, base.Height - this.Panel2MinSize), this.Panel1MinSize);
						num = Math.Max(num, 0);
					}
					Rectangle rectangle = this.CalcSplitLine(this.GetSplitterDistance(e.X, e.Y), 0);
					int x = rectangle.X;
					int y = rectangle.Y;
					SplitterCancelEventArgs splitterCancelEventArgs = new SplitterCancelEventArgs(num, num2, x, y);
					this.OnSplitterMoving(splitterCancelEventArgs);
					if (splitterCancelEventArgs.Cancel)
					{
						this.SplitEnd(false);
					}
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600390C RID: 14604 RVA: 0x000FD0D3 File Offset: 0x000FB2D3
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			if (!base.Enabled)
			{
				return;
			}
			this.OverrideCursor = null;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x0600390D RID: 14605 RVA: 0x000FD0EC File Offset: 0x000FB2EC
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (this.IsSplitterMovable && this.SplitterRectangle.Contains(e.Location))
			{
				if (!base.Enabled)
				{
					return;
				}
				if (e.Button == MouseButtons.Left && e.Clicks == 1 && !this.IsSplitterFixed)
				{
					this.splitterFocused = true;
					IContainerControl containerControlInternal = this.ParentInternal.GetContainerControlInternal();
					if (containerControlInternal != null)
					{
						ContainerControl containerControl = containerControlInternal as ContainerControl;
						if (containerControl == null)
						{
							containerControlInternal.ActiveControl = this;
						}
						else
						{
							containerControl.SetActiveControlInternal(this);
						}
					}
					base.SetActiveControlInternal(null);
					this.nextActiveControl = this.panel2;
					this.SplitBegin(e.X, e.Y);
					this.splitterClick = true;
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x0600390E RID: 14606 RVA: 0x000FD1A8 File Offset: 0x000FB3A8
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (!base.Enabled)
			{
				return;
			}
			if (!this.IsSplitterFixed && this.IsSplitterMovable && this.splitterClick)
			{
				base.CaptureInternal = false;
				if (this.splitterDrag)
				{
					this.CalcSplitLine(this.GetSplitterDistance(e.X, e.Y), 0);
					this.SplitEnd(true);
				}
				else
				{
					this.SplitEnd(false);
				}
				this.splitterClick = false;
				this.splitterDrag = false;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Move" /> event.</summary>
		/// <param name="e">The data for the event.</param>
		// Token: 0x0600390F RID: 14607 RVA: 0x000FD224 File Offset: 0x000FB424
		protected override void OnMove(EventArgs e)
		{
			base.OnMove(e);
			this.SetSplitterRect(this.Orientation == Orientation.Vertical);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		// Token: 0x06003910 RID: 14608 RVA: 0x000FD23C File Offset: 0x000FB43C
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (this.Focused)
			{
				this.DrawFocus(e.Graphics, this.SplitterRectangle);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.SplitContainer.SplitterMoving" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.SplitterEventArgs" /> that contains the event data.</param>
		// Token: 0x06003911 RID: 14609 RVA: 0x000FD260 File Offset: 0x000FB460
		public void OnSplitterMoving(SplitterCancelEventArgs e)
		{
			SplitterCancelEventHandler splitterCancelEventHandler = (SplitterCancelEventHandler)base.Events[SplitContainer.EVENT_MOVING];
			if (splitterCancelEventHandler != null)
			{
				splitterCancelEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.SplitContainer.SplitterMoved" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.SplitterEventArgs" /> that contains the event data.</param>
		// Token: 0x06003912 RID: 14610 RVA: 0x000FD290 File Offset: 0x000FB490
		public void OnSplitterMoved(SplitterEventArgs e)
		{
			SplitterEventHandler splitterEventHandler = (SplitterEventHandler)base.Events[SplitContainer.EVENT_MOVED];
			if (splitterEventHandler != null)
			{
				splitterEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.RightToLeftChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003913 RID: 14611 RVA: 0x000FD2BE File Offset: 0x000FB4BE
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
			this.panel1.RightToLeft = this.RightToLeft;
			this.panel2.RightToLeft = this.RightToLeft;
			this.UpdateSplitter();
		}

		// Token: 0x06003914 RID: 14612 RVA: 0x000FD2F0 File Offset: 0x000FB4F0
		private void ApplyPanel1MinSize(int value)
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("Panel1MinSize", SR.GetString("InvalidLowBoundArgument", new object[]
				{
					"Panel1MinSize",
					value.ToString(CultureInfo.CurrentCulture),
					"0"
				}));
			}
			if (this.Orientation == Orientation.Vertical)
			{
				if (base.DesignMode && base.Width != this.DefaultSize.Width && value + this.Panel2MinSize + this.SplitterWidth > base.Width)
				{
					throw new ArgumentOutOfRangeException("Panel1MinSize", SR.GetString("InvalidArgument", new object[]
					{
						"Panel1MinSize",
						value.ToString(CultureInfo.CurrentCulture)
					}));
				}
			}
			else if (this.Orientation == Orientation.Horizontal && base.DesignMode && base.Height != this.DefaultSize.Height && value + this.Panel2MinSize + this.SplitterWidth > base.Height)
			{
				throw new ArgumentOutOfRangeException("Panel1MinSize", SR.GetString("InvalidArgument", new object[]
				{
					"Panel1MinSize",
					value.ToString(CultureInfo.CurrentCulture)
				}));
			}
			this.panel1MinSize = value;
			if (value > this.SplitterDistanceInternal)
			{
				this.SplitterDistanceInternal = value;
			}
		}

		// Token: 0x06003915 RID: 14613 RVA: 0x000FD43C File Offset: 0x000FB63C
		private void ApplyPanel2MinSize(int value)
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("Panel2MinSize", SR.GetString("InvalidLowBoundArgument", new object[]
				{
					"Panel2MinSize",
					value.ToString(CultureInfo.CurrentCulture),
					"0"
				}));
			}
			if (this.Orientation == Orientation.Vertical)
			{
				if (base.DesignMode && base.Width != this.DefaultSize.Width && value + this.Panel1MinSize + this.SplitterWidth > base.Width)
				{
					throw new ArgumentOutOfRangeException("Panel2MinSize", SR.GetString("InvalidArgument", new object[]
					{
						"Panel2MinSize",
						value.ToString(CultureInfo.CurrentCulture)
					}));
				}
			}
			else if (this.Orientation == Orientation.Horizontal && base.DesignMode && base.Height != this.DefaultSize.Height && value + this.Panel1MinSize + this.SplitterWidth > base.Height)
			{
				throw new ArgumentOutOfRangeException("Panel2MinSize", SR.GetString("InvalidArgument", new object[]
				{
					"Panel2MinSize",
					value.ToString(CultureInfo.CurrentCulture)
				}));
			}
			this.panel2MinSize = value;
			if (value > this.Panel2.Width)
			{
				this.SplitterDistanceInternal = this.Panel2.Width + this.SplitterWidthInternal;
			}
		}

		// Token: 0x06003916 RID: 14614 RVA: 0x000FD59C File Offset: 0x000FB79C
		private void ApplySplitterWidth(int value)
		{
			if (value < 1)
			{
				throw new ArgumentOutOfRangeException("SplitterWidth", SR.GetString("InvalidLowBoundArgumentEx", new object[]
				{
					"SplitterWidth",
					value.ToString(CultureInfo.CurrentCulture),
					"1"
				}));
			}
			if (this.Orientation == Orientation.Vertical)
			{
				if (base.DesignMode && value + this.Panel1MinSize + this.Panel2MinSize > base.Width)
				{
					throw new ArgumentOutOfRangeException("SplitterWidth", SR.GetString("InvalidArgument", new object[]
					{
						"SplitterWidth",
						value.ToString(CultureInfo.CurrentCulture)
					}));
				}
			}
			else if (this.Orientation == Orientation.Horizontal && base.DesignMode && value + this.Panel1MinSize + this.Panel2MinSize > base.Height)
			{
				throw new ArgumentOutOfRangeException("SplitterWidth", SR.GetString("InvalidArgument", new object[]
				{
					"SplitterWidth",
					value.ToString(CultureInfo.CurrentCulture)
				}));
			}
			this.splitterWidth = value;
			this.UpdateSplitter();
		}

		// Token: 0x06003917 RID: 14615 RVA: 0x000FD6B0 File Offset: 0x000FB8B0
		private void ApplySplitterDistance()
		{
			using (new LayoutTransaction(this, this, "SplitterDistance", false))
			{
				this.SplitterDistanceInternal = this.splitterDistance;
			}
			if (this.BackColor == Color.Transparent)
			{
				base.Invalidate();
			}
			if (this.Orientation != Orientation.Vertical)
			{
				this.splitterRect.Y = base.Location.Y + this.SplitterDistanceInternal;
				return;
			}
			if (this.RightToLeft == RightToLeft.No)
			{
				this.splitterRect.X = base.Location.X + this.SplitterDistanceInternal;
				return;
			}
			this.splitterRect.X = base.Right - this.SplitterDistanceInternal - this.SplitterWidthInternal;
		}

		// Token: 0x06003918 RID: 14616 RVA: 0x000FD780 File Offset: 0x000FB980
		private Rectangle CalcSplitLine(int splitSize, int minWeight)
		{
			Rectangle rectangle = default(Rectangle);
			Orientation orientation = this.Orientation;
			if (orientation != Orientation.Horizontal)
			{
				if (orientation == Orientation.Vertical)
				{
					rectangle.Width = this.SplitterWidthInternal;
					rectangle.Height = base.Height;
					if (rectangle.Width < minWeight)
					{
						rectangle.Width = minWeight;
					}
					if (this.RightToLeft == RightToLeft.No)
					{
						rectangle.X = this.panel1.Location.X + splitSize;
					}
					else
					{
						rectangle.X = base.Width - splitSize - this.SplitterWidthInternal;
					}
				}
			}
			else
			{
				rectangle.Width = base.Width;
				rectangle.Height = this.SplitterWidthInternal;
				if (rectangle.Width < minWeight)
				{
					rectangle.Width = minWeight;
				}
				rectangle.Y = this.panel1.Location.Y + splitSize;
			}
			return rectangle;
		}

		// Token: 0x06003919 RID: 14617 RVA: 0x000FD85C File Offset: 0x000FBA5C
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
			if (mode == 3)
			{
				if (this.lastDrawSplit != -1)
				{
					this.DrawSplitHelper(this.lastDrawSplit);
				}
				this.lastDrawSplit = -1;
				return;
			}
			if (this.splitMove || this.splitBegin)
			{
				this.DrawSplitHelper(this.splitterDistance);
				this.lastDrawSplit = this.splitterDistance;
				return;
			}
			this.DrawSplitHelper(this.splitterDistance);
			this.lastDrawSplit = this.splitterDistance;
		}

		// Token: 0x0600391A RID: 14618 RVA: 0x000FD8FB File Offset: 0x000FBAFB
		private void DrawFocus(Graphics g, Rectangle r)
		{
			r.Inflate(-1, -1);
			ControlPaint.DrawFocusRectangle(g, r, this.ForeColor, this.BackColor);
		}

		// Token: 0x0600391B RID: 14619 RVA: 0x000FD91C File Offset: 0x000FBB1C
		private void DrawSplitHelper(int splitSize)
		{
			Rectangle rectangle = this.CalcSplitLine(splitSize, 3);
			IntPtr handle = base.Handle;
			IntPtr dcex = UnsafeNativeMethods.GetDCEx(new HandleRef(this, handle), NativeMethods.NullHandleRef, 1026);
			IntPtr intPtr = ControlPaint.CreateHalftoneHBRUSH();
			IntPtr intPtr2 = SafeNativeMethods.SelectObject(new HandleRef(this, dcex), new HandleRef(null, intPtr));
			SafeNativeMethods.PatBlt(new HandleRef(this, dcex), rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, 5898313);
			SafeNativeMethods.SelectObject(new HandleRef(this, dcex), new HandleRef(null, intPtr2));
			SafeNativeMethods.DeleteObject(new HandleRef(null, intPtr));
			UnsafeNativeMethods.ReleaseDC(new HandleRef(this, handle), new HandleRef(null, dcex));
		}

		// Token: 0x0600391C RID: 14620 RVA: 0x000FD9D0 File Offset: 0x000FBBD0
		private int GetSplitterDistance(int x, int y)
		{
			int num;
			if (this.Orientation == Orientation.Vertical)
			{
				num = x - this.anchor.X;
			}
			else
			{
				num = y - this.anchor.Y;
			}
			int num2 = 0;
			Orientation orientation = this.Orientation;
			if (orientation != Orientation.Horizontal)
			{
				if (orientation == Orientation.Vertical)
				{
					if (this.RightToLeft == RightToLeft.No)
					{
						num2 = Math.Max(this.panel1.Width + num, this.BORDERSIZE);
					}
					else
					{
						num2 = Math.Max(this.panel1.Width - num, this.BORDERSIZE);
					}
				}
			}
			else
			{
				num2 = Math.Max(this.panel1.Height + num, this.BORDERSIZE);
			}
			if (this.Orientation == Orientation.Vertical)
			{
				return Math.Max(Math.Min(num2, base.Width - this.Panel2MinSize), this.Panel1MinSize);
			}
			return Math.Max(Math.Min(num2, base.Height - this.Panel2MinSize), this.Panel1MinSize);
		}

		// Token: 0x0600391D RID: 14621 RVA: 0x000FDAB4 File Offset: 0x000FBCB4
		private bool ProcessArrowKey(bool forward)
		{
			Control control = this;
			if (base.ActiveControl != null)
			{
				control = base.ActiveControl.ParentInternal;
			}
			return control.SelectNextControl(base.ActiveControl, forward, false, false, true);
		}

		// Token: 0x0600391E RID: 14622 RVA: 0x000FDAE8 File Offset: 0x000FBCE8
		private void RepaintSplitterRect()
		{
			if (base.IsHandleCreated)
			{
				Graphics graphics = base.CreateGraphicsInternal();
				if (this.BackgroundImage != null)
				{
					using (TextureBrush textureBrush = new TextureBrush(this.BackgroundImage, WrapMode.Tile))
					{
						graphics.FillRectangle(textureBrush, base.ClientRectangle);
						goto IL_62;
					}
				}
				using (SolidBrush solidBrush = new SolidBrush(this.BackColor))
				{
					graphics.FillRectangle(solidBrush, this.splitterRect);
				}
				IL_62:
				graphics.Dispose();
			}
		}

		// Token: 0x0600391F RID: 14623 RVA: 0x000FDB7C File Offset: 0x000FBD7C
		private void SetSplitterRect(bool vertical)
		{
			if (vertical)
			{
				this.splitterRect.X = ((this.RightToLeft == RightToLeft.Yes) ? (base.Width - this.splitterDistance - this.SplitterWidthInternal) : (base.Location.X + this.splitterDistance));
				this.splitterRect.Y = base.Location.Y;
				this.splitterRect.Width = this.SplitterWidthInternal;
				this.splitterRect.Height = base.Height;
				return;
			}
			this.splitterRect.X = base.Location.X;
			this.splitterRect.Y = base.Location.Y + this.SplitterDistanceInternal;
			this.splitterRect.Width = base.Width;
			this.splitterRect.Height = this.SplitterWidthInternal;
		}

		// Token: 0x06003920 RID: 14624 RVA: 0x000FDC64 File Offset: 0x000FBE64
		private void ResizeSplitContainer()
		{
			if (this.splitContainerScaling)
			{
				return;
			}
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			if (base.Width == 0)
			{
				this.panel1.Size = new Size(0, this.panel1.Height);
				this.panel2.Size = new Size(0, this.panel2.Height);
			}
			else if (base.Height == 0)
			{
				this.panel1.Size = new Size(this.panel1.Width, 0);
				this.panel2.Size = new Size(this.panel2.Width, 0);
			}
			else
			{
				if (this.Orientation == Orientation.Vertical)
				{
					if (!this.CollapsedMode)
					{
						if (this.FixedPanel == FixedPanel.Panel1)
						{
							this.panel1.Size = new Size(this.panelSize, base.Height);
							this.panel2.Size = new Size(Math.Max(base.Width - this.panelSize - this.SplitterWidthInternal, this.Panel2MinSize), base.Height);
						}
						if (this.FixedPanel == FixedPanel.Panel2)
						{
							this.panel2.Size = new Size(this.panelSize, base.Height);
							this.splitterDistance = Math.Max(base.Width - this.panelSize - this.SplitterWidthInternal, this.Panel1MinSize);
							this.panel1.WidthInternal = this.splitterDistance;
							this.panel1.HeightInternal = base.Height;
						}
						if (this.FixedPanel == FixedPanel.None)
						{
							if (this.ratioWidth != 0.0)
							{
								this.splitterDistance = Math.Max((int)Math.Floor((double)base.Width / this.ratioWidth), this.Panel1MinSize);
							}
							this.panel1.WidthInternal = this.splitterDistance;
							this.panel1.HeightInternal = base.Height;
							this.panel2.Size = new Size(Math.Max(base.Width - this.splitterDistance - this.SplitterWidthInternal, this.Panel2MinSize), base.Height);
						}
						if (this.RightToLeft == RightToLeft.No)
						{
							this.panel2.Location = new Point(this.panel1.WidthInternal + this.SplitterWidthInternal, 0);
						}
						else
						{
							this.panel1.Location = new Point(base.Width - this.panel1.WidthInternal, 0);
						}
						this.RepaintSplitterRect();
						this.SetSplitterRect(true);
					}
					else if (this.Panel1Collapsed)
					{
						this.panel2.Size = base.Size;
						this.panel2.Location = new Point(0, 0);
					}
					else if (this.Panel2Collapsed)
					{
						this.panel1.Size = base.Size;
						this.panel1.Location = new Point(0, 0);
					}
				}
				else if (this.Orientation == Orientation.Horizontal)
				{
					if (!this.CollapsedMode)
					{
						if (this.FixedPanel == FixedPanel.Panel1)
						{
							this.panel1.Size = new Size(base.Width, this.panelSize);
							int num = this.panelSize + this.SplitterWidthInternal;
							this.panel2.Size = new Size(base.Width, Math.Max(base.Height - num, this.Panel2MinSize));
							this.panel2.Location = new Point(0, num);
						}
						if (this.FixedPanel == FixedPanel.Panel2)
						{
							this.panel2.Size = new Size(base.Width, this.panelSize);
							this.splitterDistance = Math.Max(base.Height - this.Panel2.Height - this.SplitterWidthInternal, this.Panel1MinSize);
							this.panel1.HeightInternal = this.splitterDistance;
							this.panel1.WidthInternal = base.Width;
							int num2 = this.splitterDistance + this.SplitterWidthInternal;
							this.panel2.Location = new Point(0, num2);
						}
						if (this.FixedPanel == FixedPanel.None)
						{
							if (this.ratioHeight != 0.0)
							{
								this.splitterDistance = Math.Max((int)Math.Floor((double)base.Height / this.ratioHeight), this.Panel1MinSize);
							}
							this.panel1.HeightInternal = this.splitterDistance;
							this.panel1.WidthInternal = base.Width;
							int num3 = this.splitterDistance + this.SplitterWidthInternal;
							this.panel2.Size = new Size(base.Width, Math.Max(base.Height - num3, this.Panel2MinSize));
							this.panel2.Location = new Point(0, num3);
						}
						this.RepaintSplitterRect();
						this.SetSplitterRect(false);
					}
					else if (this.Panel1Collapsed)
					{
						this.panel2.Size = base.Size;
						this.panel2.Location = new Point(0, 0);
					}
					else if (this.Panel2Collapsed)
					{
						this.panel1.Size = base.Size;
						this.panel1.Location = new Point(0, 0);
					}
				}
				try
				{
					this.resizeCalled = true;
					this.ApplySplitterDistance();
				}
				finally
				{
					this.resizeCalled = false;
				}
			}
			this.panel1.ResumeLayout();
			this.panel2.ResumeLayout();
		}

		/// <summary>Scales the location, size, padding and margin.</summary>
		/// <param name="factor">The factor by which the height and width of the control will be scaled.</param>
		/// <param name="specified">A <see cref="T:System.Windows.Forms.BoundsSpecified" /> value that specifies the bounds of the control to use when defining its size and position.</param>
		// Token: 0x06003921 RID: 14625 RVA: 0x000FE1C0 File Offset: 0x000FC3C0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			try
			{
				this.splitContainerScaling = true;
				base.ScaleControl(factor, specified);
				float num;
				if (this.orientation == Orientation.Vertical)
				{
					num = factor.Width;
				}
				else
				{
					num = factor.Height;
				}
				this.SplitterWidth = (int)Math.Round((double)((float)this.SplitterWidth * num));
			}
			finally
			{
				this.splitContainerScaling = false;
			}
		}

		/// <summary>Activates a child control. Optionally specifies the direction in the tab order to select the control from.</summary>
		/// <param name="directed">
		///   <see langword="true" /> to specify the direction of the control to select; otherwise, <see langword="false" />.</param>
		/// <param name="forward">
		///   <see langword="true" /> to move forward in the tab order; <see langword="false" /> to move backward in the tab order.</param>
		// Token: 0x06003922 RID: 14626 RVA: 0x000FE228 File Offset: 0x000FC428
		protected override void Select(bool directed, bool forward)
		{
			if (this.selectNextControl)
			{
				return;
			}
			if (this.Panel1.Controls.Count > 0 || this.Panel2.Controls.Count > 0 || this.TabStop)
			{
				this.SelectNextControlInContainer(this, forward, true, true, false);
				return;
			}
			try
			{
				Control control = this.ParentInternal;
				this.selectNextControl = true;
				while (control != null)
				{
					if (control.SelectNextControl(this, forward, true, true, control.ParentInternal == null))
					{
						break;
					}
					control = control.ParentInternal;
				}
			}
			finally
			{
				this.selectNextControl = false;
			}
		}

		// Token: 0x06003923 RID: 14627 RVA: 0x000FE2C8 File Offset: 0x000FC4C8
		private bool SelectNextControlInContainer(Control ctl, bool forward, bool tabStopOnly, bool nested, bool wrap)
		{
			if (!base.Contains(ctl) || (!nested && ctl.ParentInternal != this))
			{
				ctl = null;
			}
			SplitterPanel splitterPanel = null;
			for (;;)
			{
				ctl = base.GetNextControl(ctl, forward);
				SplitterPanel splitterPanel2 = ctl as SplitterPanel;
				if (splitterPanel2 != null && splitterPanel2.Visible)
				{
					if (splitterPanel != null)
					{
						goto IL_8D;
					}
					splitterPanel = splitterPanel2;
				}
				if (!forward && splitterPanel != null && ctl.ParentInternal != splitterPanel)
				{
					break;
				}
				if (ctl == null)
				{
					goto IL_8D;
				}
				if (ctl.CanSelect && ctl.TabStop)
				{
					goto Block_11;
				}
				if (ctl == null)
				{
					goto IL_8D;
				}
			}
			ctl = splitterPanel;
			goto IL_8D;
			Block_11:
			if (ctl is SplitContainer)
			{
				((SplitContainer)ctl).Select(forward, forward);
			}
			else
			{
				SplitContainer.SelectNextActiveControl(ctl, forward, tabStopOnly, nested, wrap);
			}
			return true;
			IL_8D:
			if (ctl != null && this.TabStop)
			{
				this.splitterFocused = true;
				IContainerControl containerControlInternal = this.ParentInternal.GetContainerControlInternal();
				if (containerControlInternal != null)
				{
					ContainerControl containerControl = containerControlInternal as ContainerControl;
					if (containerControl == null)
					{
						containerControlInternal.ActiveControl = this;
					}
					else
					{
						IntSecurity.ModifyFocus.Demand();
						containerControl.SetActiveControlInternal(this);
					}
				}
				base.SetActiveControlInternal(null);
				this.nextActiveControl = ctl;
				return true;
			}
			if (!this.SelectNextControlInPanel(ctl, forward, tabStopOnly, nested, wrap))
			{
				Control parentInternal = this.ParentInternal;
				if (parentInternal != null)
				{
					try
					{
						this.selectNextControl = true;
						parentInternal.SelectNextControl(this, forward, true, true, true);
					}
					finally
					{
						this.selectNextControl = false;
					}
				}
			}
			return false;
		}

		// Token: 0x06003924 RID: 14628 RVA: 0x000FE408 File Offset: 0x000FC608
		private bool SelectNextControlInPanel(Control ctl, bool forward, bool tabStopOnly, bool nested, bool wrap)
		{
			if (!base.Contains(ctl) || (!nested && ctl.ParentInternal != this))
			{
				ctl = null;
			}
			for (;;)
			{
				ctl = base.GetNextControl(ctl, forward);
				if (ctl == null || (ctl is SplitterPanel && ctl.Visible))
				{
					goto IL_73;
				}
				if (ctl.CanSelect && (!tabStopOnly || ctl.TabStop))
				{
					break;
				}
				if (ctl == null)
				{
					goto IL_73;
				}
			}
			if (ctl is SplitContainer)
			{
				((SplitContainer)ctl).Select(forward, forward);
			}
			else
			{
				SplitContainer.SelectNextActiveControl(ctl, forward, tabStopOnly, nested, wrap);
			}
			return true;
			IL_73:
			if (ctl == null || (ctl is SplitterPanel && !ctl.Visible))
			{
				this.callBaseVersion = true;
			}
			else
			{
				ctl = base.GetNextControl(ctl, forward);
				if (forward)
				{
					this.nextActiveControl = this.panel2;
				}
				else if (ctl == null || !ctl.ParentInternal.Visible)
				{
					this.callBaseVersion = true;
				}
				else
				{
					this.nextActiveControl = this.panel2;
				}
			}
			return false;
		}

		// Token: 0x06003925 RID: 14629 RVA: 0x000FE4E8 File Offset: 0x000FC6E8
		private static void SelectNextActiveControl(Control ctl, bool forward, bool tabStopOnly, bool nested, bool wrap)
		{
			ContainerControl containerControl = ctl as ContainerControl;
			if (containerControl != null)
			{
				bool flag = true;
				if (containerControl.ParentInternal != null)
				{
					IContainerControl containerControlInternal = containerControl.ParentInternal.GetContainerControlInternal();
					if (containerControlInternal != null)
					{
						containerControlInternal.ActiveControl = containerControl;
						flag = containerControlInternal.ActiveControl == containerControl;
					}
				}
				if (flag)
				{
					ctl.SelectNextControl(null, forward, tabStopOnly, nested, wrap);
					return;
				}
			}
			else
			{
				ctl.Select();
			}
		}

		// Token: 0x06003926 RID: 14630 RVA: 0x000FE540 File Offset: 0x000FC740
		private void SetInnerMostBorder(SplitContainer sc)
		{
			foreach (object obj in sc.Controls)
			{
				Control control = (Control)obj;
				bool flag = false;
				if (control is SplitterPanel)
				{
					foreach (object obj2 in control.Controls)
					{
						Control control2 = (Control)obj2;
						SplitContainer splitContainer = control2 as SplitContainer;
						if (splitContainer != null && splitContainer.Dock == DockStyle.Fill)
						{
							if (splitContainer.BorderStyle != this.BorderStyle)
							{
								break;
							}
							((SplitterPanel)control).BorderStyle = BorderStyle.None;
							this.SetInnerMostBorder(splitContainer);
							flag = true;
						}
					}
					if (!flag)
					{
						((SplitterPanel)control).BorderStyle = this.BorderStyle;
					}
				}
			}
		}

		/// <summary>Performs the work of setting the specified bounds of this control.</summary>
		/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control.</param>
		/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control.</param>
		/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control.</param>
		/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control.</param>
		/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values.</param>
		// Token: 0x06003927 RID: 14631 RVA: 0x000FE640 File Offset: 0x000FC840
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if ((specified & BoundsSpecified.Height) != BoundsSpecified.None && this.Orientation == Orientation.Horizontal && height < this.Panel1MinSize + this.SplitterWidthInternal + this.Panel2MinSize)
			{
				height = this.Panel1MinSize + this.SplitterWidthInternal + this.Panel2MinSize;
			}
			if ((specified & BoundsSpecified.Width) != BoundsSpecified.None && this.Orientation == Orientation.Vertical && width < this.Panel1MinSize + this.SplitterWidthInternal + this.Panel2MinSize)
			{
				width = this.Panel1MinSize + this.SplitterWidthInternal + this.Panel2MinSize;
			}
			base.SetBoundsCore(x, y, width, height, specified);
			this.SetSplitterRect(this.Orientation == Orientation.Vertical);
		}

		// Token: 0x06003928 RID: 14632 RVA: 0x000FE6E4 File Offset: 0x000FC8E4
		private void SplitBegin(int x, int y)
		{
			this.anchor = new Point(x, y);
			this.splitterDistance = this.GetSplitterDistance(x, y);
			this.initialSplitterDistance = this.splitterDistance;
			this.initialSplitterRectangle = this.SplitterRectangle;
			IntSecurity.UnmanagedCode.Assert();
			try
			{
				if (this.splitContainerMessageFilter == null)
				{
					this.splitContainerMessageFilter = new SplitContainer.SplitContainerMessageFilter(this);
				}
				Application.AddMessageFilter(this.splitContainerMessageFilter);
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			base.CaptureInternal = true;
			this.DrawSplitBar(1);
		}

		// Token: 0x06003929 RID: 14633 RVA: 0x000FE774 File Offset: 0x000FC974
		private void SplitMove(int x, int y)
		{
			int num = this.GetSplitterDistance(x, y);
			int num2 = num - this.initialSplitterDistance;
			int num3 = num2 % this.SplitterIncrement;
			if (this.splitterDistance != num)
			{
				if (this.Orientation == Orientation.Vertical)
				{
					if (num + this.SplitterWidthInternal <= base.Width - this.Panel2MinSize - this.BORDERSIZE)
					{
						this.splitterDistance = num - num3;
					}
				}
				else if (num + this.SplitterWidthInternal <= base.Height - this.Panel2MinSize - this.BORDERSIZE)
				{
					this.splitterDistance = num - num3;
				}
			}
			this.DrawSplitBar(2);
		}

		// Token: 0x0600392A RID: 14634 RVA: 0x000FE808 File Offset: 0x000FCA08
		private void SplitEnd(bool accept)
		{
			this.DrawSplitBar(3);
			if (this.splitContainerMessageFilter != null)
			{
				Application.RemoveMessageFilter(this.splitContainerMessageFilter);
				this.splitContainerMessageFilter = null;
			}
			if (accept)
			{
				this.ApplySplitterDistance();
			}
			else if (this.splitterDistance != this.initialSplitterDistance)
			{
				this.splitterClick = false;
				this.splitterDistance = (this.SplitterDistanceInternal = this.initialSplitterDistance);
			}
			this.anchor = Point.Empty;
		}

		// Token: 0x0600392B RID: 14635 RVA: 0x000FE878 File Offset: 0x000FCA78
		private void UpdateSplitter()
		{
			if (this.splitContainerScaling)
			{
				return;
			}
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			if (this.Orientation == Orientation.Vertical)
			{
				bool flag = this.RightToLeft == RightToLeft.Yes;
				if (!this.CollapsedMode)
				{
					this.panel1.HeightInternal = base.Height;
					this.panel1.WidthInternal = this.splitterDistance;
					this.panel2.Size = new Size(base.Width - this.splitterDistance - this.SplitterWidthInternal, base.Height);
					if (!flag)
					{
						this.panel1.Location = new Point(0, 0);
						this.panel2.Location = new Point(this.splitterDistance + this.SplitterWidthInternal, 0);
					}
					else
					{
						this.panel1.Location = new Point(base.Width - this.splitterDistance, 0);
						this.panel2.Location = new Point(0, 0);
					}
					this.RepaintSplitterRect();
					this.SetSplitterRect(true);
					if (!this.resizeCalled)
					{
						this.ratioWidth = (((double)base.Width / (double)this.panel1.Width > 0.0) ? ((double)base.Width / (double)this.panel1.Width) : this.ratioWidth);
					}
				}
				else
				{
					if (this.Panel1Collapsed)
					{
						this.panel2.Size = base.Size;
						this.panel2.Location = new Point(0, 0);
					}
					else if (this.Panel2Collapsed)
					{
						this.panel1.Size = base.Size;
						this.panel1.Location = new Point(0, 0);
					}
					if (!this.resizeCalled)
					{
						this.ratioWidth = (((double)base.Width / (double)this.splitterDistance > 0.0) ? ((double)base.Width / (double)this.splitterDistance) : this.ratioWidth);
					}
				}
			}
			else if (!this.CollapsedMode)
			{
				this.panel1.Location = new Point(0, 0);
				this.panel1.WidthInternal = base.Width;
				this.panel1.HeightInternal = this.SplitterDistanceInternal;
				int num = this.splitterDistance + this.SplitterWidthInternal;
				this.panel2.Size = new Size(base.Width, base.Height - num);
				this.panel2.Location = new Point(0, num);
				this.RepaintSplitterRect();
				this.SetSplitterRect(false);
				if (!this.resizeCalled)
				{
					this.ratioHeight = (((double)base.Height / (double)this.panel1.Height > 0.0) ? ((double)base.Height / (double)this.panel1.Height) : this.ratioHeight);
				}
			}
			else
			{
				if (this.Panel1Collapsed)
				{
					this.panel2.Size = base.Size;
					this.panel2.Location = new Point(0, 0);
				}
				else if (this.Panel2Collapsed)
				{
					this.panel1.Size = base.Size;
					this.panel1.Location = new Point(0, 0);
				}
				if (!this.resizeCalled)
				{
					this.ratioHeight = (((double)base.Height / (double)this.splitterDistance > 0.0) ? ((double)base.Height / (double)this.splitterDistance) : this.ratioHeight);
				}
			}
			this.panel1.ResumeLayout();
			this.panel2.ResumeLayout();
		}

		// Token: 0x0600392C RID: 14636 RVA: 0x000FEBFC File Offset: 0x000FCDFC
		private void WmSetCursor(ref Message m)
		{
			if (!(m.WParam == base.InternalHandle) || ((int)m.LParam & 65535) != 1)
			{
				this.DefWndProc(ref m);
				return;
			}
			if (this.OverrideCursor != null)
			{
				Cursor.CurrentInternal = this.OverrideCursor;
				return;
			}
			Cursor.CurrentInternal = this.Cursor;
		}

		// Token: 0x0600392D RID: 14637 RVA: 0x000FEC60 File Offset: 0x000FCE60
		internal override Rectangle GetToolNativeScreenRectangle()
		{
			Rectangle toolNativeScreenRectangle = base.GetToolNativeScreenRectangle();
			Rectangle splitterRectangle = this.SplitterRectangle;
			return new Rectangle(toolNativeScreenRectangle.X + splitterRectangle.X, toolNativeScreenRectangle.Y + splitterRectangle.Y, splitterRectangle.Width, splitterRectangle.Height);
		}

		// Token: 0x0600392E RID: 14638 RVA: 0x000FECAC File Offset: 0x000FCEAC
		internal override void AfterControlRemoved(Control control, Control oldParent)
		{
			base.AfterControlRemoved(control, oldParent);
			if (control is SplitContainer && control.Dock == DockStyle.Fill)
			{
				this.SetInnerMostBorder(this);
			}
		}

		/// <summary>Processes a dialog box key.</summary>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
		/// <returns>
		///   <see langword="true" /> if the key was processed by the control; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600392F RID: 14639 RVA: 0x000FECD0 File Offset: 0x000FCED0
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if ((keyData & (Keys.Control | Keys.Alt)) == Keys.None)
			{
				Keys keys = keyData & Keys.KeyCode;
				if (keys != Keys.Tab)
				{
					if (keys - Keys.Left <= 3)
					{
						if (this.splitterFocused)
						{
							return false;
						}
						if (this.ProcessArrowKey(keys == Keys.Right || keys == Keys.Down))
						{
							return true;
						}
					}
				}
				else if (this.ProcessTabKey((keyData & Keys.Shift) == Keys.None))
				{
					return true;
				}
			}
			return base.ProcessDialogKey(keyData);
		}

		/// <summary>Selects the next available control and makes it the active control.</summary>
		/// <param name="forward">
		///   <see langword="true" /> to cycle forward through the controls in the <see cref="T:System.Windows.Forms.ContainerControl" />; otherwise, <see langword="false" />.</param>
		/// <returns>
		///   <see langword="true" /> if a control is selected; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003930 RID: 14640 RVA: 0x000FED38 File Offset: 0x000FCF38
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool ProcessTabKey(bool forward)
		{
			if (!this.TabStop || this.IsSplitterFixed)
			{
				return base.ProcessTabKey(forward);
			}
			if (this.nextActiveControl != null)
			{
				base.SetActiveControlInternal(this.nextActiveControl);
				this.nextActiveControl = null;
			}
			if (this.SelectNextControlInPanel(base.ActiveControl, forward, true, true, true))
			{
				this.nextActiveControl = null;
				this.splitterFocused = false;
				return true;
			}
			if (this.callBaseVersion)
			{
				this.callBaseVersion = false;
				return base.ProcessTabKey(forward);
			}
			this.splitterFocused = true;
			IContainerControl containerControlInternal = this.ParentInternal.GetContainerControlInternal();
			if (containerControlInternal != null)
			{
				ContainerControl containerControl = containerControlInternal as ContainerControl;
				if (containerControl == null)
				{
					containerControlInternal.ActiveControl = this;
				}
				else
				{
					containerControl.SetActiveControlInternal(this);
				}
			}
			base.SetActiveControlInternal(null);
			return true;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseCaptureChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003931 RID: 14641 RVA: 0x000FEDE9 File Offset: 0x000FCFE9
		protected override void OnMouseCaptureChanged(EventArgs e)
		{
			base.OnMouseCaptureChanged(e);
			if (this.splitContainerMessageFilter != null)
			{
				Application.RemoveMessageFilter(this.splitContainerMessageFilter);
				this.splitContainerMessageFilter = null;
			}
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="msg">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x06003932 RID: 14642 RVA: 0x000FEE0C File Offset: 0x000FD00C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message msg)
		{
			int msg2 = msg.Msg;
			if (msg2 == 7)
			{
				this.splitterFocused = true;
				base.WndProc(ref msg);
				return;
			}
			if (msg2 == 8)
			{
				this.splitterFocused = false;
				base.WndProc(ref msg);
				return;
			}
			if (msg2 == 32)
			{
				this.WmSetCursor(ref msg);
				return;
			}
			base.WndProc(ref msg);
		}

		/// <summary>Creates a new instance of the control collection for the control.</summary>
		/// <returns>A new instance of <see cref="T:System.Windows.Forms.Control.ControlCollection" /> assigned to the control.</returns>
		// Token: 0x06003933 RID: 14643 RVA: 0x000FEE5A File Offset: 0x000FD05A
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override Control.ControlCollection CreateControlsInstance()
		{
			return new SplitContainer.SplitContainerTypedControlCollection(this, typeof(SplitterPanel), true);
		}

		// Token: 0x04002283 RID: 8835
		private const int DRAW_START = 1;

		// Token: 0x04002284 RID: 8836
		private const int DRAW_MOVE = 2;

		// Token: 0x04002285 RID: 8837
		private const int DRAW_END = 3;

		// Token: 0x04002286 RID: 8838
		private const int rightBorder = 5;

		// Token: 0x04002287 RID: 8839
		private const int leftBorder = 2;

		// Token: 0x04002288 RID: 8840
		private int BORDERSIZE;

		// Token: 0x04002289 RID: 8841
		private Orientation orientation = Orientation.Vertical;

		// Token: 0x0400228A RID: 8842
		private SplitterPanel panel1;

		// Token: 0x0400228B RID: 8843
		private SplitterPanel panel2;

		// Token: 0x0400228C RID: 8844
		private BorderStyle borderStyle;

		// Token: 0x0400228D RID: 8845
		private FixedPanel fixedPanel;

		// Token: 0x0400228E RID: 8846
		private int panel1MinSize = 25;

		// Token: 0x0400228F RID: 8847
		private int newPanel1MinSize = 25;

		// Token: 0x04002290 RID: 8848
		private int panel2MinSize = 25;

		// Token: 0x04002291 RID: 8849
		private int newPanel2MinSize = 25;

		// Token: 0x04002292 RID: 8850
		private bool tabStop = true;

		// Token: 0x04002293 RID: 8851
		private int panelSize;

		// Token: 0x04002294 RID: 8852
		private Rectangle splitterRect;

		// Token: 0x04002295 RID: 8853
		private int splitterInc = 1;

		// Token: 0x04002296 RID: 8854
		private bool splitterFixed;

		// Token: 0x04002297 RID: 8855
		private int splitterDistance = 50;

		// Token: 0x04002298 RID: 8856
		private int splitterWidth = 4;

		// Token: 0x04002299 RID: 8857
		private int newSplitterWidth = 4;

		// Token: 0x0400229A RID: 8858
		private int splitDistance = 50;

		// Token: 0x0400229B RID: 8859
		private int lastDrawSplit = 1;

		// Token: 0x0400229C RID: 8860
		private int initialSplitterDistance;

		// Token: 0x0400229D RID: 8861
		private Rectangle initialSplitterRectangle;

		// Token: 0x0400229E RID: 8862
		private Point anchor = Point.Empty;

		// Token: 0x0400229F RID: 8863
		private bool splitBegin;

		// Token: 0x040022A0 RID: 8864
		private bool splitMove;

		// Token: 0x040022A1 RID: 8865
		private bool splitBreak;

		// Token: 0x040022A2 RID: 8866
		private Cursor overrideCursor;

		// Token: 0x040022A3 RID: 8867
		private Control nextActiveControl;

		// Token: 0x040022A4 RID: 8868
		private bool callBaseVersion;

		// Token: 0x040022A5 RID: 8869
		private bool splitterFocused;

		// Token: 0x040022A6 RID: 8870
		private bool splitterClick;

		// Token: 0x040022A7 RID: 8871
		private bool splitterDrag;

		// Token: 0x040022A8 RID: 8872
		private double ratioWidth;

		// Token: 0x040022A9 RID: 8873
		private double ratioHeight;

		// Token: 0x040022AA RID: 8874
		private bool resizeCalled;

		// Token: 0x040022AB RID: 8875
		private bool splitContainerScaling;

		// Token: 0x040022AC RID: 8876
		private bool setSplitterDistance;

		// Token: 0x040022AD RID: 8877
		private static readonly object EVENT_MOVING = new object();

		// Token: 0x040022AE RID: 8878
		private static readonly object EVENT_MOVED = new object();

		// Token: 0x040022AF RID: 8879
		private SplitContainer.SplitContainerMessageFilter splitContainerMessageFilter;

		// Token: 0x040022B0 RID: 8880
		private bool selectNextControl;

		// Token: 0x040022B1 RID: 8881
		private bool initializing;

		// Token: 0x020007E4 RID: 2020
		private class SplitContainerMessageFilter : IMessageFilter
		{
			// Token: 0x06006DC8 RID: 28104 RVA: 0x00192395 File Offset: 0x00190595
			public SplitContainerMessageFilter(SplitContainer splitContainer)
			{
				this.owner = splitContainer;
			}

			// Token: 0x06006DC9 RID: 28105 RVA: 0x001923A4 File Offset: 0x001905A4
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			bool IMessageFilter.PreFilterMessage(ref Message m)
			{
				if (m.Msg >= 256 && m.Msg <= 264)
				{
					if ((m.Msg == 256 && (int)m.WParam == 27) || m.Msg == 260)
					{
						this.owner.splitBegin = false;
						this.owner.SplitEnd(false);
						this.owner.splitterClick = false;
						this.owner.splitterDrag = false;
					}
					return true;
				}
				return false;
			}

			// Token: 0x040042BF RID: 17087
			private SplitContainer owner;
		}

		// Token: 0x020007E5 RID: 2021
		internal class SplitContainerTypedControlCollection : WindowsFormsUtils.TypedControlCollection
		{
			// Token: 0x06006DCA RID: 28106 RVA: 0x00192427 File Offset: 0x00190627
			public SplitContainerTypedControlCollection(Control c, Type type, bool isReadOnly)
				: base(c, type, isReadOnly)
			{
				this.owner = c as SplitContainer;
			}

			// Token: 0x06006DCB RID: 28107 RVA: 0x0019243E File Offset: 0x0019063E
			public override void Remove(Control value)
			{
				if (value is SplitterPanel && !this.owner.DesignMode && this.IsReadOnly)
				{
					throw new NotSupportedException(SR.GetString("ReadonlyControlsCollection"));
				}
				base.Remove(value);
			}

			// Token: 0x06006DCC RID: 28108 RVA: 0x00192474 File Offset: 0x00190674
			internal override void SetChildIndexInternal(Control child, int newIndex)
			{
				if (child is SplitterPanel)
				{
					if (this.owner.DesignMode)
					{
						return;
					}
					if (this.IsReadOnly)
					{
						throw new NotSupportedException(SR.GetString("ReadonlyControlsCollection"));
					}
				}
				base.SetChildIndexInternal(child, newIndex);
			}

			// Token: 0x040042C0 RID: 17088
			private SplitContainer owner;
		}
	}
}
