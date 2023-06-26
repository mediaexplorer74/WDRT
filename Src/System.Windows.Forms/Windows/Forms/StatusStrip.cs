using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows status bar control.</summary>
	// Token: 0x0200037C RID: 892
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[SRDescription("DescriptionStatusStrip")]
	public class StatusStrip : ToolStrip
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.StatusStrip" /> class.</summary>
		// Token: 0x06003A53 RID: 14931 RVA: 0x0010143C File Offset: 0x000FF63C
		public StatusStrip()
		{
			base.SuspendLayout();
			this.CanOverflow = false;
			this.LayoutStyle = ToolStripLayoutStyle.Table;
			base.RenderMode = ToolStripRenderMode.System;
			this.GripStyle = ToolStripGripStyle.Hidden;
			base.SetStyle(ControlStyles.ResizeRedraw, true);
			this.Stretch = true;
			this.state[StatusStrip.stateSizingGrip] = true;
			base.ResumeLayout(true);
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.StatusStrip" /> supports overflow functionality.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.StatusStrip" /> supports overflow functionality; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x06003A54 RID: 14932 RVA: 0x000D8D08 File Offset: 0x000D6F08
		// (set) Token: 0x06003A55 RID: 14933 RVA: 0x000D8D10 File Offset: 0x000D6F10
		[DefaultValue(false)]
		[SRDescription("ToolStripCanOverflowDescr")]
		[SRCategory("CatLayout")]
		[Browsable(false)]
		public new bool CanOverflow
		{
			get
			{
				return base.CanOverflow;
			}
			set
			{
				base.CanOverflow = value;
			}
		}

		/// <summary>Gets a value indicating whether ToolTips are shown for the <see cref="T:System.Windows.Forms.StatusStrip" /> by default.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x06003A56 RID: 14934 RVA: 0x0001180C File Offset: 0x0000FA0C
		protected override bool DefaultShowItemToolTips
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the size, in pixels, of the <see cref="T:System.Windows.Forms.StatusStrip" /> when it is first created.</summary>
		/// <returns>A <see cref="M:System.Drawing.Point.#ctor(System.Drawing.Size)" /> constructor representing the size of the <see cref="T:System.Windows.Forms.StatusStrip" />, in pixels.</returns>
		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x06003A57 RID: 14935 RVA: 0x00101499 File Offset: 0x000FF699
		protected override Size DefaultSize
		{
			get
			{
				return new Size(200, 22);
			}
		}

		/// <summary>Gets the spacing, in pixels, between the left, right, top, and bottom edges of the <see cref="T:System.Windows.Forms.StatusStrip" /> from the edges of the form.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> that represents the spacing. The default is <c>{Left=6, Top=2, Right=0, Bottom=2}</c>.</returns>
		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x06003A58 RID: 14936 RVA: 0x001014A8 File Offset: 0x000FF6A8
		protected override Padding DefaultPadding
		{
			get
			{
				if (base.Orientation != Orientation.Horizontal)
				{
					return new Padding(1, 3, 1, this.DefaultSize.Height);
				}
				if (this.RightToLeft == RightToLeft.No)
				{
					return new Padding(1, 0, 14, 0);
				}
				return new Padding(14, 0, 1, 0);
			}
		}

		/// <summary>Gets which borders of the <see cref="T:System.Windows.Forms.StatusStrip" /> are docked to the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values. The default is <see cref="F:System.Windows.Forms.DockStyle.Bottom" />.</returns>
		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x06003A59 RID: 14937 RVA: 0x00016041 File Offset: 0x00014241
		protected override DockStyle DefaultDock
		{
			get
			{
				return DockStyle.Bottom;
			}
		}

		/// <summary>Gets or sets which <see cref="T:System.Windows.Forms.StatusStrip" /> borders are docked to its parent control and determines how a <see cref="T:System.Windows.Forms.StatusStrip" /> is resized with its parent.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values. The default is <see cref="F:System.Windows.Forms.DockStyle.Bottom" />.</returns>
		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x06003A5A RID: 14938 RVA: 0x001014F1 File Offset: 0x000FF6F1
		// (set) Token: 0x06003A5B RID: 14939 RVA: 0x001014F9 File Offset: 0x000FF6F9
		[DefaultValue(DockStyle.Bottom)]
		public override DockStyle Dock
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

		/// <summary>Gets or sets the visibility of the grip used to reposition the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripGripStyle" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripGripStyle.Hidden" />.</returns>
		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x06003A5C RID: 14940 RVA: 0x000D8DD1 File Offset: 0x000D6FD1
		// (set) Token: 0x06003A5D RID: 14941 RVA: 0x000D8DD9 File Offset: 0x000D6FD9
		[DefaultValue(ToolStripGripStyle.Hidden)]
		public new ToolStripGripStyle GripStyle
		{
			get
			{
				return base.GripStyle;
			}
			set
			{
				base.GripStyle = value;
			}
		}

		/// <summary>Gets or sets a value indicating how the <see cref="T:System.Windows.Forms.StatusStrip" /> lays out the items collection.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripLayoutStyle" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.Table" />.</returns>
		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x06003A5E RID: 14942 RVA: 0x00101502 File Offset: 0x000FF702
		// (set) Token: 0x06003A5F RID: 14943 RVA: 0x0010150A File Offset: 0x000FF70A
		[DefaultValue(ToolStripLayoutStyle.Table)]
		public new ToolStripLayoutStyle LayoutStyle
		{
			get
			{
				return base.LayoutStyle;
			}
			set
			{
				base.LayoutStyle = value;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value.</returns>
		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x06003A60 RID: 14944 RVA: 0x00013442 File Offset: 0x00011642
		// (set) Token: 0x06003A61 RID: 14945 RVA: 0x0001344A File Offset: 0x0001164A
		[Browsable(false)]
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

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x140002D0 RID: 720
		// (add) Token: 0x06003A62 RID: 14946 RVA: 0x00013453 File Offset: 0x00011653
		// (remove) Token: 0x06003A63 RID: 14947 RVA: 0x0001345C File Offset: 0x0001165C
		[Browsable(false)]
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

		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x06003A64 RID: 14948 RVA: 0x00101513 File Offset: 0x000FF713
		private Control RTLGrip
		{
			get
			{
				if (this.rtlLayoutGrip == null)
				{
					this.rtlLayoutGrip = new StatusStrip.RightToLeftLayoutGrip();
				}
				return this.rtlLayoutGrip;
			}
		}

		/// <summary>Gets or sets a value indicating whether ToolTips are shown for the <see cref="T:System.Windows.Forms.StatusStrip" />.</summary>
		/// <returns>
		///   <see langword="true" /> if ToolTips are shown for the <see cref="T:System.Windows.Forms.StatusStrip" />; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x06003A65 RID: 14949 RVA: 0x000D8E2E File Offset: 0x000D702E
		// (set) Token: 0x06003A66 RID: 14950 RVA: 0x000D8E36 File Offset: 0x000D7036
		[DefaultValue(false)]
		[SRDescription("ToolStripShowItemToolTipsDescr")]
		[SRCategory("CatBehavior")]
		public new bool ShowItemToolTips
		{
			get
			{
				return base.ShowItemToolTips;
			}
			set
			{
				base.ShowItemToolTips = value;
			}
		}

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x06003A67 RID: 14951 RVA: 0x00101530 File Offset: 0x000FF730
		private bool ShowSizingGrip
		{
			get
			{
				if (this.SizingGrip && base.IsHandleCreated)
				{
					if (base.DesignMode)
					{
						return true;
					}
					HandleRef rootHWnd = WindowsFormsUtils.GetRootHWnd(this);
					if (rootHWnd.Handle != IntPtr.Zero)
					{
						return !UnsafeNativeMethods.IsZoomed(rootHWnd);
					}
				}
				return false;
			}
		}

		/// <summary>Gets or sets a value indicating whether a sizing handle (grip) is displayed in the lower-right corner of the control.</summary>
		/// <returns>
		///   <see langword="true" /> if a grip is displayed; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x06003A68 RID: 14952 RVA: 0x0010157C File Offset: 0x000FF77C
		// (set) Token: 0x06003A69 RID: 14953 RVA: 0x0010158E File Offset: 0x000FF78E
		[SRCategory("CatAppearance")]
		[DefaultValue(true)]
		[SRDescription("StatusStripSizingGripDescr")]
		public bool SizingGrip
		{
			get
			{
				return this.state[StatusStrip.stateSizingGrip];
			}
			set
			{
				if (value != this.state[StatusStrip.stateSizingGrip])
				{
					this.state[StatusStrip.stateSizingGrip] = value;
					this.EnsureRightToLeftGrip();
					base.Invalidate(true);
				}
			}
		}

		/// <summary>Gets the boundaries of the sizing handle (grip) for a <see cref="T:System.Windows.Forms.StatusStrip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> representing the grip boundaries.</returns>
		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x06003A6A RID: 14954 RVA: 0x001015C4 File Offset: 0x000FF7C4
		[Browsable(false)]
		public Rectangle SizeGripBounds
		{
			get
			{
				if (!this.SizingGrip)
				{
					return Rectangle.Empty;
				}
				Size size = base.Size;
				int num = Math.Min(this.DefaultSize.Height, size.Height);
				if (this.RightToLeft == RightToLeft.Yes)
				{
					return new Rectangle(0, size.Height - num, 12, num);
				}
				return new Rectangle(size.Width - 12, size.Height - num, 12, num);
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.StatusStrip" /> stretches from end to end in its container.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.StatusStrip" /> stretches from end to end in its <see cref="T:System.Windows.Forms.ToolStripContainer" />; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x06003A6B RID: 14955 RVA: 0x000D8E3F File Offset: 0x000D703F
		// (set) Token: 0x06003A6C RID: 14956 RVA: 0x000D8E47 File Offset: 0x000D7047
		[DefaultValue(true)]
		[SRCategory("CatLayout")]
		[SRDescription("ToolStripStretchDescr")]
		public new bool Stretch
		{
			get
			{
				return base.Stretch;
			}
			set
			{
				base.Stretch = value;
			}
		}

		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x06003A6D RID: 14957 RVA: 0x00101638 File Offset: 0x000FF838
		private TableLayoutSettings TableLayoutSettings
		{
			get
			{
				return base.LayoutSettings as TableLayoutSettings;
			}
		}

		/// <summary>Creates a new accessibility object for the control.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the control.</returns>
		// Token: 0x06003A6E RID: 14958 RVA: 0x00101645 File Offset: 0x000FF845
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new StatusStrip.StatusStripAccessibleObject(this);
		}

		/// <summary>Creates a default <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> with the specified text, image, and event handler on a new <see cref="T:System.Windows.Forms.StatusStrip" /> instance.</summary>
		/// <param name="text">The text to use for the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />. If the <paramref name="text" /> parameter is a hyphen (-), this method creates a <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</param>
		/// <param name="onClick">An event handler that raises the <see cref="E:System.Windows.Forms.Control.Click" /> event when the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> is clicked.</param>
		/// <returns>A <see cref="M:System.Windows.Forms.ToolStripStatusLabel.#ctor(System.String,System.Drawing.Image,System.EventHandler)" />, or a <see cref="T:System.Windows.Forms.ToolStripSeparator" /> if the <paramref name="text" /> parameter is a hyphen (-).</returns>
		// Token: 0x06003A6F RID: 14959 RVA: 0x0010164D File Offset: 0x000FF84D
		protected internal override ToolStripItem CreateDefaultItem(string text, Image image, EventHandler onClick)
		{
			return new ToolStripStatusLabel(text, image, onClick);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.StatusStrip" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06003A70 RID: 14960 RVA: 0x00101657 File Offset: 0x000FF857
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.rtlLayoutGrip != null)
			{
				this.rtlLayoutGrip.Dispose();
				this.rtlLayoutGrip = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06003A71 RID: 14961 RVA: 0x00101680 File Offset: 0x000FF880
		private void EnsureRightToLeftGrip()
		{
			if (this.SizingGrip && this.RightToLeft == RightToLeft.Yes)
			{
				this.RTLGrip.Bounds = this.SizeGripBounds;
				if (!base.Controls.Contains(this.RTLGrip))
				{
					WindowsFormsUtils.ReadOnlyControlCollection readOnlyControlCollection = base.Controls as WindowsFormsUtils.ReadOnlyControlCollection;
					if (readOnlyControlCollection != null)
					{
						readOnlyControlCollection.AddInternal(this.RTLGrip);
						return;
					}
				}
			}
			else if (this.rtlLayoutGrip != null && base.Controls.Contains(this.rtlLayoutGrip))
			{
				WindowsFormsUtils.ReadOnlyControlCollection readOnlyControlCollection2 = base.Controls as WindowsFormsUtils.ReadOnlyControlCollection;
				if (readOnlyControlCollection2 != null)
				{
					readOnlyControlCollection2.RemoveInternal(this.rtlLayoutGrip);
				}
				this.rtlLayoutGrip.Dispose();
				this.rtlLayoutGrip = null;
			}
		}

		// Token: 0x06003A72 RID: 14962 RVA: 0x00101728 File Offset: 0x000FF928
		internal override Size GetPreferredSizeCore(Size proposedSize)
		{
			if (this.LayoutStyle != ToolStripLayoutStyle.Table)
			{
				return base.GetPreferredSizeCore(proposedSize);
			}
			if (proposedSize.Width == 1)
			{
				proposedSize.Width = int.MaxValue;
			}
			if (proposedSize.Height == 1)
			{
				proposedSize.Height = int.MaxValue;
			}
			if (base.Orientation == Orientation.Horizontal)
			{
				return ToolStrip.GetPreferredSizeHorizontal(this, proposedSize) + this.Padding.Size;
			}
			return ToolStrip.GetPreferredSizeVertical(this, proposedSize) + this.Padding.Size;
		}

		/// <summary>Paints the background of the control.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains information about the <see cref="T:System.Windows.Forms.StatusStrip" /> to paint.</param>
		// Token: 0x06003A73 RID: 14963 RVA: 0x001017AF File Offset: 0x000FF9AF
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);
			if (this.ShowSizingGrip)
			{
				base.Renderer.DrawStatusStripSizingGrip(new ToolStripRenderEventArgs(e.Graphics, this));
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
		/// <param name="levent">The event data.</param>
		// Token: 0x06003A74 RID: 14964 RVA: 0x001017D8 File Offset: 0x000FF9D8
		protected override void OnLayout(LayoutEventArgs levent)
		{
			this.state[StatusStrip.stateCalledSpringTableLayout] = false;
			bool flag = false;
			ToolStripItem toolStripItem = levent.AffectedComponent as ToolStripItem;
			int count = this.DisplayedItems.Count;
			if (toolStripItem != null)
			{
				flag = this.DisplayedItems.Contains(toolStripItem);
			}
			if (this.LayoutStyle == ToolStripLayoutStyle.Table)
			{
				this.OnSpringTableLayoutCore();
			}
			base.OnLayout(levent);
			if ((count != this.DisplayedItems.Count || (toolStripItem != null && flag != this.DisplayedItems.Contains(toolStripItem))) && this.LayoutStyle == ToolStripLayoutStyle.Table)
			{
				this.OnSpringTableLayoutCore();
				base.OnLayout(levent);
			}
			this.EnsureRightToLeftGrip();
		}

		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x06003A75 RID: 14965 RVA: 0x00028BBB File Offset: 0x00026DBB
		internal override bool SupportsUiaProviders
		{
			get
			{
				return AccessibilityImprovements.Level3 && !base.DesignMode;
			}
		}

		/// <summary>Resets the collection of displayed and overflow items after a layout is done.</summary>
		// Token: 0x06003A76 RID: 14966 RVA: 0x00101874 File Offset: 0x000FFA74
		protected override void SetDisplayedItems()
		{
			if (this.state[StatusStrip.stateCalledSpringTableLayout])
			{
				bool flag = base.Orientation == Orientation.Horizontal && this.RightToLeft == RightToLeft.Yes;
				Point location = this.DisplayRectangle.Location;
				location.X += base.ClientSize.Width + 1;
				location.Y += base.ClientSize.Height + 1;
				bool flag2 = false;
				Rectangle rectangle = Rectangle.Empty;
				ToolStripItem toolStripItem = null;
				for (int i = 0; i < this.Items.Count; i++)
				{
					ToolStripItem toolStripItem2 = this.Items[i];
					if (flag2 || ((IArrangedElement)toolStripItem2).ParticipatesInLayout)
					{
						if (flag2 || (this.SizingGrip && toolStripItem2.Bounds.IntersectsWith(this.SizeGripBounds)))
						{
							base.SetItemLocation(toolStripItem2, location);
							toolStripItem2.SetPlacement(ToolStripItemPlacement.None);
						}
					}
					else if (toolStripItem != null && rectangle.IntersectsWith(toolStripItem2.Bounds))
					{
						base.SetItemLocation(toolStripItem2, location);
						toolStripItem2.SetPlacement(ToolStripItemPlacement.None);
					}
					else if (toolStripItem2.Bounds.Width == 1)
					{
						ToolStripStatusLabel toolStripStatusLabel = toolStripItem2 as ToolStripStatusLabel;
						if (toolStripStatusLabel != null && toolStripStatusLabel.Spring)
						{
							base.SetItemLocation(toolStripItem2, location);
							toolStripItem2.SetPlacement(ToolStripItemPlacement.None);
						}
					}
					if (toolStripItem2.Bounds.Location != location)
					{
						toolStripItem = toolStripItem2;
						rectangle = toolStripItem.Bounds;
					}
					else if (((IArrangedElement)toolStripItem2).ParticipatesInLayout)
					{
						flag2 = true;
					}
				}
			}
			base.SetDisplayedItems();
		}

		// Token: 0x06003A77 RID: 14967 RVA: 0x00101A11 File Offset: 0x000FFC11
		internal override void ResetRenderMode()
		{
			base.RenderMode = ToolStripRenderMode.System;
		}

		// Token: 0x06003A78 RID: 14968 RVA: 0x00101A1A File Offset: 0x000FFC1A
		internal override bool ShouldSerializeRenderMode()
		{
			return base.RenderMode != ToolStripRenderMode.System && base.RenderMode > ToolStripRenderMode.Custom;
		}

		/// <summary>Provides custom table layout for a <see cref="T:System.Windows.Forms.StatusStrip" />.</summary>
		// Token: 0x06003A79 RID: 14969 RVA: 0x00101A30 File Offset: 0x000FFC30
		protected virtual void OnSpringTableLayoutCore()
		{
			if (this.LayoutStyle == ToolStripLayoutStyle.Table)
			{
				this.state[StatusStrip.stateCalledSpringTableLayout] = true;
				base.SuspendLayout();
				if (this.lastOrientation != base.Orientation)
				{
					TableLayoutSettings tableLayoutSettings = this.TableLayoutSettings;
					tableLayoutSettings.RowCount = 0;
					tableLayoutSettings.ColumnCount = 0;
					tableLayoutSettings.ColumnStyles.Clear();
					tableLayoutSettings.RowStyles.Clear();
				}
				this.lastOrientation = base.Orientation;
				if (base.Orientation == Orientation.Horizontal)
				{
					this.TableLayoutSettings.GrowStyle = TableLayoutPanelGrowStyle.AddColumns;
					int count = this.TableLayoutSettings.ColumnStyles.Count;
					for (int i = 0; i < this.DisplayedItems.Count; i++)
					{
						if (i >= count)
						{
							this.TableLayoutSettings.ColumnStyles.Add(new ColumnStyle());
						}
						ToolStripStatusLabel toolStripStatusLabel = this.DisplayedItems[i] as ToolStripStatusLabel;
						bool flag = toolStripStatusLabel != null && toolStripStatusLabel.Spring;
						this.DisplayedItems[i].Anchor = (flag ? (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right) : (AnchorStyles.Top | AnchorStyles.Bottom));
						ColumnStyle columnStyle = this.TableLayoutSettings.ColumnStyles[i];
						columnStyle.Width = 100f;
						columnStyle.SizeType = (flag ? SizeType.Percent : SizeType.AutoSize);
					}
					if (this.TableLayoutSettings.RowStyles.Count > 1 || this.TableLayoutSettings.RowStyles.Count == 0)
					{
						this.TableLayoutSettings.RowStyles.Clear();
						this.TableLayoutSettings.RowStyles.Add(new RowStyle());
					}
					this.TableLayoutSettings.RowCount = 1;
					this.TableLayoutSettings.RowStyles[0].SizeType = SizeType.Absolute;
					this.TableLayoutSettings.RowStyles[0].Height = (float)Math.Max(0, this.DisplayRectangle.Height);
					this.TableLayoutSettings.ColumnCount = this.DisplayedItems.Count + 1;
					for (int j = this.DisplayedItems.Count; j < this.TableLayoutSettings.ColumnStyles.Count; j++)
					{
						this.TableLayoutSettings.ColumnStyles[j].SizeType = SizeType.AutoSize;
					}
				}
				else
				{
					this.TableLayoutSettings.GrowStyle = TableLayoutPanelGrowStyle.AddRows;
					int count2 = this.TableLayoutSettings.RowStyles.Count;
					for (int k = 0; k < this.DisplayedItems.Count; k++)
					{
						if (k >= count2)
						{
							this.TableLayoutSettings.RowStyles.Add(new RowStyle());
						}
						ToolStripStatusLabel toolStripStatusLabel2 = this.DisplayedItems[k] as ToolStripStatusLabel;
						bool flag2 = toolStripStatusLabel2 != null && toolStripStatusLabel2.Spring;
						this.DisplayedItems[k].Anchor = (flag2 ? (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right) : (AnchorStyles.Left | AnchorStyles.Right));
						RowStyle rowStyle = this.TableLayoutSettings.RowStyles[k];
						rowStyle.Height = 100f;
						rowStyle.SizeType = (flag2 ? SizeType.Percent : SizeType.AutoSize);
					}
					this.TableLayoutSettings.ColumnCount = 1;
					if (this.TableLayoutSettings.ColumnStyles.Count > 1 || this.TableLayoutSettings.ColumnStyles.Count == 0)
					{
						this.TableLayoutSettings.ColumnStyles.Clear();
						this.TableLayoutSettings.ColumnStyles.Add(new ColumnStyle());
					}
					this.TableLayoutSettings.ColumnCount = 1;
					this.TableLayoutSettings.ColumnStyles[0].SizeType = SizeType.Absolute;
					this.TableLayoutSettings.ColumnStyles[0].Width = (float)Math.Max(0, this.DisplayRectangle.Width);
					this.TableLayoutSettings.RowCount = this.DisplayedItems.Count + 1;
					for (int l = this.DisplayedItems.Count; l < this.TableLayoutSettings.RowStyles.Count; l++)
					{
						this.TableLayoutSettings.RowStyles[l].SizeType = SizeType.AutoSize;
					}
				}
				base.ResumeLayout(false);
			}
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x06003A7A RID: 14970 RVA: 0x00101E34 File Offset: 0x00100034
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 132 && this.SizingGrip)
			{
				Rectangle sizeGripBounds = this.SizeGripBounds;
				int num = NativeMethods.Util.LOWORD(m.LParam);
				int num2 = NativeMethods.Util.HIWORD(m.LParam);
				if (sizeGripBounds.Contains(base.PointToClient(new Point(num, num2))))
				{
					HandleRef rootHWnd = WindowsFormsUtils.GetRootHWnd(this);
					if (rootHWnd.Handle != IntPtr.Zero && !UnsafeNativeMethods.IsZoomed(rootHWnd))
					{
						NativeMethods.RECT rect = default(NativeMethods.RECT);
						UnsafeNativeMethods.GetClientRect(rootHWnd, ref rect);
						NativeMethods.POINT point;
						if (this.RightToLeft == RightToLeft.Yes)
						{
							point = new NativeMethods.POINT(this.SizeGripBounds.Left, this.SizeGripBounds.Bottom);
						}
						else
						{
							point = new NativeMethods.POINT(this.SizeGripBounds.Right, this.SizeGripBounds.Bottom);
						}
						UnsafeNativeMethods.MapWindowPoints(new HandleRef(this, base.Handle), rootHWnd, point, 1);
						int num3 = Math.Abs(rect.bottom - point.y);
						int num4 = Math.Abs(rect.right - point.x);
						if (this.RightToLeft != RightToLeft.Yes && num4 + num3 < 2)
						{
							m.Result = (IntPtr)17;
							return;
						}
					}
				}
			}
			base.WndProc(ref m);
		}

		// Token: 0x040022FD RID: 8957
		private const AnchorStyles AllAnchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

		// Token: 0x040022FE RID: 8958
		private const AnchorStyles HorizontalAnchor = AnchorStyles.Left | AnchorStyles.Right;

		// Token: 0x040022FF RID: 8959
		private const AnchorStyles VerticalAnchor = AnchorStyles.Top | AnchorStyles.Bottom;

		// Token: 0x04002300 RID: 8960
		private BitVector32 state;

		// Token: 0x04002301 RID: 8961
		private static readonly int stateSizingGrip = BitVector32.CreateMask();

		// Token: 0x04002302 RID: 8962
		private static readonly int stateCalledSpringTableLayout = BitVector32.CreateMask(StatusStrip.stateSizingGrip);

		// Token: 0x04002303 RID: 8963
		private const int gripWidth = 12;

		// Token: 0x04002304 RID: 8964
		private StatusStrip.RightToLeftLayoutGrip rtlLayoutGrip;

		// Token: 0x04002305 RID: 8965
		private Orientation lastOrientation;

		// Token: 0x020007EA RID: 2026
		private class RightToLeftLayoutGrip : Control
		{
			// Token: 0x06006E00 RID: 28160 RVA: 0x00193001 File Offset: 0x00191201
			public RightToLeftLayoutGrip()
			{
				base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
				this.BackColor = Color.Transparent;
			}

			// Token: 0x1700180E RID: 6158
			// (get) Token: 0x06006E01 RID: 28161 RVA: 0x00193020 File Offset: 0x00191220
			protected override CreateParams CreateParams
			{
				[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					CreateParams createParams = base.CreateParams;
					createParams.ExStyle |= 4194304;
					return createParams;
				}
			}

			// Token: 0x06006E02 RID: 28162 RVA: 0x00193048 File Offset: 0x00191248
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			protected override void WndProc(ref Message m)
			{
				if (m.Msg == 132)
				{
					int num = NativeMethods.Util.LOWORD(m.LParam);
					int num2 = NativeMethods.Util.HIWORD(m.LParam);
					if (base.ClientRectangle.Contains(base.PointToClient(new Point(num, num2))))
					{
						m.Result = (IntPtr)16;
						return;
					}
				}
				base.WndProc(ref m);
			}
		}

		// Token: 0x020007EB RID: 2027
		[ComVisible(true)]
		internal class StatusStripAccessibleObject : ToolStrip.ToolStripAccessibleObject
		{
			// Token: 0x06006E03 RID: 28163 RVA: 0x0018BC34 File Offset: 0x00189E34
			public StatusStripAccessibleObject(StatusStrip owner)
				: base(owner)
			{
			}

			// Token: 0x1700180F RID: 6159
			// (get) Token: 0x06006E04 RID: 28164 RVA: 0x001930AC File Offset: 0x001912AC
			public override AccessibleRole Role
			{
				get
				{
					AccessibleRole accessibleRole = base.Owner.AccessibleRole;
					if (accessibleRole != AccessibleRole.Default)
					{
						return accessibleRole;
					}
					return AccessibleRole.StatusBar;
				}
			}

			// Token: 0x06006E05 RID: 28165 RVA: 0x001930CD File Offset: 0x001912CD
			internal override object GetPropertyValue(int propertyID)
			{
				if (AccessibilityImprovements.Level3 && propertyID == 30003)
				{
					return 50017;
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x06006E06 RID: 28166 RVA: 0x001930F0 File Offset: 0x001912F0
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				StatusStrip statusStrip = base.Owner as StatusStrip;
				if (statusStrip == null || statusStrip.Items.Count == 0)
				{
					if (base.Owner.ToolStripControlHost != null && (direction == UnsafeNativeMethods.NavigateDirection.Parent || direction == UnsafeNativeMethods.NavigateDirection.PreviousSibling || direction == UnsafeNativeMethods.NavigateDirection.NextSibling))
					{
						return base.Owner.ToolStripControlHost.AccessibilityObject.FragmentNavigate(direction);
					}
					return null;
				}
				else
				{
					if (direction == UnsafeNativeMethods.NavigateDirection.FirstChild)
					{
						for (int i = 0; i < this.GetChildCount(); i++)
						{
							AccessibleObject child = this.GetChild(i);
							if (child != null && !(child is Control.ControlAccessibleObject))
							{
								return child;
							}
						}
						return null;
					}
					if (direction != UnsafeNativeMethods.NavigateDirection.LastChild)
					{
						return base.FragmentNavigate(direction);
					}
					for (int j = this.GetChildCount() - 1; j >= 0; j--)
					{
						AccessibleObject child2 = this.GetChild(j);
						if (child2 != null && !(child2 is Control.ControlAccessibleObject))
						{
							return child2;
						}
					}
					return null;
				}
			}

			// Token: 0x06006E07 RID: 28167 RVA: 0x00178D9E File Offset: 0x00176F9E
			internal override UnsafeNativeMethods.IRawElementProviderFragment ElementProviderFromPoint(double x, double y)
			{
				return this.HitTest((int)x, (int)y);
			}

			// Token: 0x06006E08 RID: 28168 RVA: 0x000F1520 File Offset: 0x000EF720
			internal override UnsafeNativeMethods.IRawElementProviderFragment GetFocus()
			{
				return this.GetFocused();
			}
		}
	}
}
