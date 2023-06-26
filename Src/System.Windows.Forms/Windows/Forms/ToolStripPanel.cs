using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Creates a container within which other controls can share horizontal or vertical space.</summary>
	// Token: 0x020003EC RID: 1004
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Designer("System.Windows.Forms.Design.ToolStripPanelDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxBitmap(typeof(ToolStripPanel), "ToolStripPanel_standalone.bmp")]
	public class ToolStripPanel : ContainerControl, IArrangedElement, IComponent, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripPanel" /> class.</summary>
		// Token: 0x060044CB RID: 17611 RVA: 0x0012140C File Offset: 0x0011F60C
		public ToolStripPanel()
		{
			if (DpiHelper.EnableToolStripHighDpiImprovements)
			{
				this.scaledRowMargin = DpiHelper.LogicalToDeviceUnits(ToolStripPanel.rowMargin, 0);
			}
			base.SuspendLayout();
			base.AutoScaleMode = AutoScaleMode.None;
			this.InitFlowLayout();
			this.AutoSize = true;
			this.MinimumSize = Size.Empty;
			this.state[ToolStripPanel.stateLocked | ToolStripPanel.stateBeginInit | ToolStripPanel.stateChangingZOrder] = false;
			this.TabStop = false;
			ToolStripManager.ToolStripPanels.Add(this);
			base.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, true);
			base.SetStyle(ControlStyles.Selectable, false);
			base.ResumeLayout(true);
		}

		// Token: 0x060044CC RID: 17612 RVA: 0x001214C6 File Offset: 0x0011F6C6
		internal ToolStripPanel(ToolStripContainer owner)
			: this()
		{
			this.owner = owner;
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>
		///   <see langword="true" /> if enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x170010E1 RID: 4321
		// (get) Token: 0x060044CD RID: 17613 RVA: 0x000B8E3D File Offset: 0x000B703D
		// (set) Token: 0x060044CE RID: 17614 RVA: 0x000B8E45 File Offset: 0x000B7045
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>
		///   <see langword="true" /> if enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x170010E2 RID: 4322
		// (get) Token: 0x060044CF RID: 17615 RVA: 0x000B0A3B File Offset: 0x000AEC3B
		// (set) Token: 0x060044D0 RID: 17616 RVA: 0x000EC0F6 File Offset: 0x000EA2F6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool AutoScroll
		{
			get
			{
				return base.AutoScroll;
			}
			set
			{
				base.AutoScroll = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>
		///   <see langword="true" /> if enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x170010E3 RID: 4323
		// (get) Token: 0x060044D1 RID: 17617 RVA: 0x0001180F File Offset: 0x0000FA0F
		// (set) Token: 0x060044D2 RID: 17618 RVA: 0x00011817 File Offset: 0x0000FA17
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

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>
		///   <see langword="true" /> if enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x170010E4 RID: 4324
		// (get) Token: 0x060044D3 RID: 17619 RVA: 0x00011820 File Offset: 0x0000FA20
		// (set) Token: 0x060044D4 RID: 17620 RVA: 0x00011828 File Offset: 0x0000FA28
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

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripPanel" /> automatically adjusts its size when the form is resized.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.ToolStripPanel" /> automatically resizes; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170010E5 RID: 4325
		// (get) Token: 0x060044D5 RID: 17621 RVA: 0x00011831 File Offset: 0x0000FA31
		// (set) Token: 0x060044D6 RID: 17622 RVA: 0x00011839 File Offset: 0x0000FA39
		[DefaultValue(true)]
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripPanel.AutoSize" /> property changes.</summary>
		// Token: 0x14000365 RID: 869
		// (add) Token: 0x060044D7 RID: 17623 RVA: 0x00011842 File Offset: 0x0000FA42
		// (remove) Token: 0x060044D8 RID: 17624 RVA: 0x0001184B File Offset: 0x0000FA4B
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

		/// <summary>Gets the internal spacing, in pixels, of the contents of a control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> that represents the internal spacing of the contents of a control.</returns>
		// Token: 0x170010E6 RID: 4326
		// (get) Token: 0x060044D9 RID: 17625 RVA: 0x00019A61 File Offset: 0x00017C61
		protected override Padding DefaultPadding
		{
			get
			{
				return Padding.Empty;
			}
		}

		/// <summary>Gets the space, in pixels, that is specified by default between controls.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> that represents the default space between controls.</returns>
		// Token: 0x170010E7 RID: 4327
		// (get) Token: 0x060044DA RID: 17626 RVA: 0x00019A61 File Offset: 0x00017C61
		protected override Padding DefaultMargin
		{
			get
			{
				return Padding.Empty;
			}
		}

		/// <summary>Gets or sets the spacing, in pixels, between the <see cref="T:System.Windows.Forms.ToolStripPanelRow" />s and the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value representing the spacing, in pixels.</returns>
		// Token: 0x170010E8 RID: 4328
		// (get) Token: 0x060044DB RID: 17627 RVA: 0x001214D5 File Offset: 0x0011F6D5
		// (set) Token: 0x060044DC RID: 17628 RVA: 0x001214DD File Offset: 0x0011F6DD
		public Padding RowMargin
		{
			get
			{
				return this.scaledRowMargin;
			}
			set
			{
				this.scaledRowMargin = value;
				LayoutTransaction.DoLayout(this, this, "RowMargin");
			}
		}

		/// <summary>Gets or sets which control borders are docked to its parent control and determines how a control is resized with its parent.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values. The default is None.</returns>
		// Token: 0x170010E9 RID: 4329
		// (get) Token: 0x060044DD RID: 17629 RVA: 0x000FC41E File Offset: 0x000FA61E
		// (set) Token: 0x060044DE RID: 17630 RVA: 0x001214F2 File Offset: 0x0011F6F2
		public override DockStyle Dock
		{
			get
			{
				return base.Dock;
			}
			set
			{
				base.Dock = value;
				if (value == DockStyle.Left || value == DockStyle.Right)
				{
					this.Orientation = Orientation.Vertical;
					return;
				}
				this.Orientation = Orientation.Horizontal;
			}
		}

		// Token: 0x170010EA RID: 4330
		// (get) Token: 0x060044DF RID: 17631 RVA: 0x00121512 File Offset: 0x0011F712
		internal Rectangle DragBounds
		{
			get
			{
				return LayoutUtils.InflateRect(base.ClientRectangle, ToolStripPanel.DragMargin);
			}
		}

		// Token: 0x170010EB RID: 4331
		// (get) Token: 0x060044E0 RID: 17632 RVA: 0x0010C201 File Offset: 0x0010A401
		internal bool IsInDesignMode
		{
			get
			{
				return base.DesignMode;
			}
		}

		/// <summary>Gets a cached instance of the control's layout engine.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Layout.LayoutEngine" /> for the control's contents.</returns>
		// Token: 0x170010EC RID: 4332
		// (get) Token: 0x060044E1 RID: 17633 RVA: 0x000AF974 File Offset: 0x000ADB74
		public override LayoutEngine LayoutEngine
		{
			get
			{
				return FlowLayout.Instance;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripPanel" /> can be moved or resized.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.ToolStripPanel" /> can be moved or resized; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170010ED RID: 4333
		// (get) Token: 0x060044E2 RID: 17634 RVA: 0x00121524 File Offset: 0x0011F724
		// (set) Token: 0x060044E3 RID: 17635 RVA: 0x00121536 File Offset: 0x0011F736
		[DefaultValue(false)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool Locked
		{
			get
			{
				return this.state[ToolStripPanel.stateLocked];
			}
			set
			{
				this.state[ToolStripPanel.stateLocked] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the horizontal or vertical orientation of the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.Orientation" /> values.</returns>
		// Token: 0x170010EE RID: 4334
		// (get) Token: 0x060044E4 RID: 17636 RVA: 0x00121549 File Offset: 0x0011F749
		// (set) Token: 0x060044E5 RID: 17637 RVA: 0x00121554 File Offset: 0x0011F754
		public Orientation Orientation
		{
			get
			{
				return this.orientation;
			}
			set
			{
				if (this.orientation != value)
				{
					this.orientation = value;
					this.scaledRowMargin = LayoutUtils.FlipPadding(this.scaledRowMargin);
					this.InitFlowLayout();
					foreach (object obj in this.RowsInternal)
					{
						ToolStripPanelRow toolStripPanelRow = (ToolStripPanelRow)obj;
						toolStripPanelRow.OnOrientationChanged();
					}
				}
			}
		}

		// Token: 0x170010EF RID: 4335
		// (get) Token: 0x060044E6 RID: 17638 RVA: 0x001215D4 File Offset: 0x0011F7D4
		private ToolStripRendererSwitcher RendererSwitcher
		{
			get
			{
				if (this.rendererSwitcher == null)
				{
					this.rendererSwitcher = new ToolStripRendererSwitcher(this);
					this.HandleRendererChanged(this, EventArgs.Empty);
					this.rendererSwitcher.RendererChanged += this.HandleRendererChanged;
				}
				return this.rendererSwitcher;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Windows.Forms.ToolStripRenderer" /> used to customize the appearance of a <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripRenderer" /> that handles painting.</returns>
		// Token: 0x170010F0 RID: 4336
		// (get) Token: 0x060044E7 RID: 17639 RVA: 0x00121613 File Offset: 0x0011F813
		// (set) Token: 0x060044E8 RID: 17640 RVA: 0x00121620 File Offset: 0x0011F820
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ToolStripRenderer Renderer
		{
			get
			{
				return this.RendererSwitcher.Renderer;
			}
			set
			{
				this.RendererSwitcher.Renderer = value;
			}
		}

		/// <summary>Gets or sets the painting styles to be applied to the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripRenderMode" /> values.</returns>
		// Token: 0x170010F1 RID: 4337
		// (get) Token: 0x060044E9 RID: 17641 RVA: 0x0012162E File Offset: 0x0011F82E
		// (set) Token: 0x060044EA RID: 17642 RVA: 0x0012163B File Offset: 0x0011F83B
		[SRDescription("ToolStripRenderModeDescr")]
		[SRCategory("CatAppearance")]
		public ToolStripRenderMode RenderMode
		{
			get
			{
				return this.RendererSwitcher.RenderMode;
			}
			set
			{
				this.RendererSwitcher.RenderMode = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripPanel.Renderer" /> property changes.</summary>
		// Token: 0x14000366 RID: 870
		// (add) Token: 0x060044EB RID: 17643 RVA: 0x00121649 File Offset: 0x0011F849
		// (remove) Token: 0x060044EC RID: 17644 RVA: 0x0012165C File Offset: 0x0011F85C
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripRendererChanged")]
		public event EventHandler RendererChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripPanel.EventRendererChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripPanel.EventRendererChanged, value);
			}
		}

		// Token: 0x170010F2 RID: 4338
		// (get) Token: 0x060044ED RID: 17645 RVA: 0x00121670 File Offset: 0x0011F870
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SRDescription("ToolStripPanelRowsDescr")]
		internal ToolStripPanel.ToolStripPanelRowCollection RowsInternal
		{
			get
			{
				ToolStripPanel.ToolStripPanelRowCollection toolStripPanelRowCollection = (ToolStripPanel.ToolStripPanelRowCollection)base.Properties.GetObject(ToolStripPanel.PropToolStripPanelRowCollection);
				if (toolStripPanelRowCollection == null)
				{
					toolStripPanelRowCollection = this.CreateToolStripPanelRowCollection();
					base.Properties.SetObject(ToolStripPanel.PropToolStripPanelRowCollection, toolStripPanelRowCollection);
				}
				return toolStripPanelRowCollection;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ToolStripPanelRow" />s in this <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" /> representing the <see cref="T:System.Windows.Forms.ToolStripPanelRow" />s in this <see cref="T:System.Windows.Forms.ToolStripPanel" />.</returns>
		// Token: 0x170010F3 RID: 4339
		// (get) Token: 0x060044EE RID: 17646 RVA: 0x001216B0 File Offset: 0x0011F8B0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ToolStripPanelRowsDescr")]
		public ToolStripPanelRow[] Rows
		{
			get
			{
				ToolStripPanelRow[] array = new ToolStripPanelRow[this.RowsInternal.Count];
				this.RowsInternal.CopyTo(array, 0);
				return array;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the tab index.</returns>
		// Token: 0x170010F4 RID: 4340
		// (get) Token: 0x060044EF RID: 17647 RVA: 0x000B2372 File Offset: 0x000B0572
		// (set) Token: 0x060044F0 RID: 17648 RVA: 0x000B237A File Offset: 0x000B057A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x14000367 RID: 871
		// (add) Token: 0x060044F1 RID: 17649 RVA: 0x000B2383 File Offset: 0x000B0583
		// (remove) Token: 0x060044F2 RID: 17650 RVA: 0x000B238C File Offset: 0x000B058C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>
		///   <see langword="true" /> if enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x170010F5 RID: 4341
		// (get) Token: 0x060044F3 RID: 17651 RVA: 0x000B2395 File Offset: 0x000B0595
		// (set) Token: 0x060044F4 RID: 17652 RVA: 0x001216DC File Offset: 0x0011F8DC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
				if (AccessibilityImprovements.Level2)
				{
					base.SetStyle(ControlStyles.Selectable, value);
				}
				base.TabStop = value;
			}
		}

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x14000368 RID: 872
		// (add) Token: 0x060044F5 RID: 17653 RVA: 0x000B23A6 File Offset: 0x000B05A6
		// (remove) Token: 0x060044F6 RID: 17654 RVA: 0x000B23AF File Offset: 0x000B05AF
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
		/// <returns>A <see cref="T:System.String" /> representing the display text.</returns>
		// Token: 0x170010F6 RID: 4342
		// (get) Token: 0x060044F7 RID: 17655 RVA: 0x00013814 File Offset: 0x00011A14
		// (set) Token: 0x060044F8 RID: 17656 RVA: 0x00023FE9 File Offset: 0x000221E9
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x14000369 RID: 873
		// (add) Token: 0x060044F9 RID: 17657 RVA: 0x00046591 File Offset: 0x00044791
		// (remove) Token: 0x060044FA RID: 17658 RVA: 0x0004659A File Offset: 0x0004479A
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

		/// <summary>Begins the initialization of a <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		// Token: 0x060044FB RID: 17659 RVA: 0x001216F8 File Offset: 0x0011F8F8
		public void BeginInit()
		{
			this.state[ToolStripPanel.stateBeginInit] = true;
		}

		/// <summary>Ends the initialization of a <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		// Token: 0x060044FC RID: 17660 RVA: 0x0012170C File Offset: 0x0011F90C
		public void EndInit()
		{
			this.state[ToolStripPanel.stateBeginInit] = false;
			this.state[ToolStripPanel.stateEndInit] = true;
			try
			{
				if (!this.state[ToolStripPanel.stateInJoin])
				{
					this.JoinControls();
				}
			}
			finally
			{
				this.state[ToolStripPanel.stateEndInit] = false;
			}
		}

		// Token: 0x060044FD RID: 17661 RVA: 0x00121778 File Offset: 0x0011F978
		private ToolStripPanel.ToolStripPanelRowCollection CreateToolStripPanelRowCollection()
		{
			return new ToolStripPanel.ToolStripPanelRowCollection(this);
		}

		/// <summary>Retrieves a collection of <see cref="T:System.Windows.Forms.ToolStripPanel" /> controls.</summary>
		/// <returns>A collection of <see cref="T:System.Windows.Forms.ToolStripPanel" /> controls.</returns>
		// Token: 0x060044FE RID: 17662 RVA: 0x00121780 File Offset: 0x0011F980
		protected override Control.ControlCollection CreateControlsInstance()
		{
			return new ToolStripPanel.ToolStripPanelControlCollection(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ToolStripPanel" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x060044FF RID: 17663 RVA: 0x00121788 File Offset: 0x0011F988
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				ToolStripManager.ToolStripPanels.Remove(this);
			}
			base.Dispose(disposing);
		}

		// Token: 0x06004500 RID: 17664 RVA: 0x0012179F File Offset: 0x0011F99F
		private void InitFlowLayout()
		{
			if (this.Orientation == Orientation.Horizontal)
			{
				FlowLayout.SetFlowDirection(this, FlowDirection.TopDown);
			}
			else
			{
				FlowLayout.SetFlowDirection(this, FlowDirection.LeftToRight);
			}
			FlowLayout.SetWrapContents(this, false);
		}

		// Token: 0x06004501 RID: 17665 RVA: 0x001217C0 File Offset: 0x0011F9C0
		private Point GetStartLocation(ToolStrip toolStripToDrag)
		{
			if (toolStripToDrag.IsCurrentlyDragging && this.Orientation == Orientation.Horizontal && toolStripToDrag.RightToLeft == RightToLeft.Yes)
			{
				return new Point(toolStripToDrag.Right, toolStripToDrag.Top);
			}
			return toolStripToDrag.Location;
		}

		// Token: 0x06004502 RID: 17666 RVA: 0x001217F3 File Offset: 0x0011F9F3
		private void HandleRendererChanged(object sender, EventArgs e)
		{
			this.OnRendererChanged(e);
		}

		/// <summary>Paints the background of the control.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		// Token: 0x06004503 RID: 17667 RVA: 0x001217FC File Offset: 0x0011F9FC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			ToolStripPanelRenderEventArgs toolStripPanelRenderEventArgs = new ToolStripPanelRenderEventArgs(e.Graphics, this);
			this.Renderer.DrawToolStripPanelBackground(toolStripPanelRenderEventArgs);
			if (!toolStripPanelRenderEventArgs.Handled)
			{
				base.OnPaintBackground(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStrip.ControlAdded" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ControlEventArgs" /> that contains the event data.</param>
		// Token: 0x06004504 RID: 17668 RVA: 0x00121834 File Offset: 0x0011FA34
		protected override void OnControlAdded(ControlEventArgs e)
		{
			base.OnControlAdded(e);
			if (!this.state[ToolStripPanel.stateBeginInit] && !this.state[ToolStripPanel.stateInJoin])
			{
				if (!this.state[ToolStripPanel.stateLayoutSuspended])
				{
					this.Join(e.Control as ToolStrip, e.Control.Location);
					return;
				}
				this.BeginInit();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStrip.ControlRemoved" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ControlEventArgs" /> that contains the event data.</param>
		// Token: 0x06004505 RID: 17669 RVA: 0x001218A4 File Offset: 0x0011FAA4
		protected override void OnControlRemoved(ControlEventArgs e)
		{
			ISupportToolStripPanel supportToolStripPanel = e.Control as ISupportToolStripPanel;
			if (supportToolStripPanel != null && supportToolStripPanel.ToolStripPanelRow != null)
			{
				supportToolStripPanel.ToolStripPanelRow.ControlsInternal.Remove(e.Control);
			}
			base.OnControlRemoved(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
		// Token: 0x06004506 RID: 17670 RVA: 0x001218E8 File Offset: 0x0011FAE8
		protected override void OnLayout(LayoutEventArgs e)
		{
			if (e.AffectedComponent != this.ParentInternal && e.AffectedComponent is Control)
			{
				ISupportToolStripPanel supportToolStripPanel = e.AffectedComponent as ISupportToolStripPanel;
				if (supportToolStripPanel != null && this.RowsInternal.Contains(supportToolStripPanel.ToolStripPanelRow))
				{
					LayoutTransaction.DoLayout(supportToolStripPanel.ToolStripPanelRow, e.AffectedComponent as IArrangedElement, e.AffectedProperty);
				}
			}
			base.OnLayout(e);
		}

		// Token: 0x06004507 RID: 17671 RVA: 0x00121955 File Offset: 0x0011FB55
		internal override void OnLayoutSuspended()
		{
			base.OnLayoutSuspended();
			this.state[ToolStripPanel.stateLayoutSuspended] = true;
		}

		// Token: 0x06004508 RID: 17672 RVA: 0x0012196E File Offset: 0x0011FB6E
		internal override void OnLayoutResuming(bool resumeLayout)
		{
			base.OnLayoutResuming(resumeLayout);
			this.state[ToolStripPanel.stateLayoutSuspended] = false;
			if (this.state[ToolStripPanel.stateBeginInit])
			{
				this.EndInit();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.RightToLeftChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06004509 RID: 17673 RVA: 0x001219A0 File Offset: 0x0011FBA0
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
			if (!this.state[ToolStripPanel.stateBeginInit])
			{
				if (base.Controls.Count > 0)
				{
					base.SuspendLayout();
					Control[] array = new Control[base.Controls.Count];
					Point[] array2 = new Point[base.Controls.Count];
					int num = 0;
					foreach (object obj in this.RowsInternal)
					{
						ToolStripPanelRow toolStripPanelRow = (ToolStripPanelRow)obj;
						foreach (object obj2 in toolStripPanelRow.ControlsInternal)
						{
							Control control = (Control)obj2;
							array[num] = control;
							array2[num] = new Point(toolStripPanelRow.Bounds.Width - control.Right, control.Top);
							num++;
						}
					}
					base.Controls.Clear();
					for (int i = 0; i < array.Length; i++)
					{
						this.Join(array[i] as ToolStrip, array2[i]);
					}
					base.ResumeLayout(true);
					return;
				}
			}
			else
			{
				this.state[ToolStripPanel.stateRightToLeftChanged] = true;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripPanel.RendererChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600450A RID: 17674 RVA: 0x00121B1C File Offset: 0x0011FD1C
		protected virtual void OnRendererChanged(EventArgs e)
		{
			this.Renderer.InitializePanel(this);
			base.Invalidate();
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripPanel.EventRendererChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnParentChanged(System.EventArgs)" /> event.</summary>
		/// <param name="e">The event data.</param>
		// Token: 0x0600450B RID: 17675 RVA: 0x00121B5C File Offset: 0x0011FD5C
		protected override void OnParentChanged(EventArgs e)
		{
			this.PerformUpdate();
			base.OnParentChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DockChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600450C RID: 17676 RVA: 0x00121B6B File Offset: 0x0011FD6B
		protected override void OnDockChanged(EventArgs e)
		{
			this.PerformUpdate();
			base.OnDockChanged(e);
		}

		// Token: 0x0600450D RID: 17677 RVA: 0x00121B7A File Offset: 0x0011FD7A
		internal void PerformUpdate()
		{
			this.PerformUpdate(false);
		}

		// Token: 0x0600450E RID: 17678 RVA: 0x00121B83 File Offset: 0x0011FD83
		internal void PerformUpdate(bool forceLayout)
		{
			if (!this.state[ToolStripPanel.stateBeginInit] && !this.state[ToolStripPanel.stateInJoin])
			{
				this.JoinControls(forceLayout);
			}
		}

		// Token: 0x0600450F RID: 17679 RVA: 0x00121BB0 File Offset: 0x0011FDB0
		private void ResetRenderMode()
		{
			this.RendererSwitcher.ResetRenderMode();
		}

		// Token: 0x06004510 RID: 17680 RVA: 0x00121BBD File Offset: 0x0011FDBD
		private bool ShouldSerializeRenderMode()
		{
			return this.RendererSwitcher.ShouldSerializeRenderMode();
		}

		// Token: 0x06004511 RID: 17681 RVA: 0x00121BCA File Offset: 0x0011FDCA
		private bool ShouldSerializeDock()
		{
			return this.owner == null && this.Dock > DockStyle.None;
		}

		// Token: 0x06004512 RID: 17682 RVA: 0x00121BDF File Offset: 0x0011FDDF
		private void JoinControls()
		{
			this.JoinControls(false);
		}

		// Token: 0x06004513 RID: 17683 RVA: 0x00121BE8 File Offset: 0x0011FDE8
		private void JoinControls(bool forceLayout)
		{
			ToolStripPanel.ToolStripPanelControlCollection toolStripPanelControlCollection = base.Controls as ToolStripPanel.ToolStripPanelControlCollection;
			if (toolStripPanelControlCollection.Count > 0)
			{
				toolStripPanelControlCollection.Sort();
				Control[] array = new Control[toolStripPanelControlCollection.Count];
				toolStripPanelControlCollection.CopyTo(array, 0);
				int i = 0;
				while (i < array.Length)
				{
					int count = this.RowsInternal.Count;
					ISupportToolStripPanel supportToolStripPanel = array[i] as ISupportToolStripPanel;
					if (supportToolStripPanel == null || supportToolStripPanel.ToolStripPanelRow == null || supportToolStripPanel.IsCurrentlyDragging)
					{
						goto IL_8B;
					}
					ToolStripPanelRow toolStripPanelRow = supportToolStripPanel.ToolStripPanelRow;
					if (!toolStripPanelRow.Bounds.Contains(array[i].Location))
					{
						goto IL_8B;
					}
					IL_117:
					i++;
					continue;
					IL_8B:
					if (array[i].AutoSize)
					{
						array[i].Size = array[i].PreferredSize;
					}
					Point location = array[i].Location;
					if (this.state[ToolStripPanel.stateRightToLeftChanged])
					{
						location = new Point(base.Width - array[i].Right, location.Y);
					}
					this.Join(array[i] as ToolStrip, array[i].Location);
					if (count < this.RowsInternal.Count || forceLayout)
					{
						this.OnLayout(new LayoutEventArgs(this, PropertyNames.Rows));
						goto IL_117;
					}
					goto IL_117;
				}
			}
			this.state[ToolStripPanel.stateRightToLeftChanged] = false;
		}

		// Token: 0x06004514 RID: 17684 RVA: 0x00121D2C File Offset: 0x0011FF2C
		private void GiveToolStripPanelFeedback(ToolStrip toolStripToDrag, Point screenLocation)
		{
			if (this.Orientation == Orientation.Horizontal && this.RightToLeft == RightToLeft.Yes)
			{
				screenLocation.Offset(-toolStripToDrag.Width, 0);
			}
			if (ToolStripPanel.CurrentFeedbackRect == null)
			{
				ToolStripPanel.CurrentFeedbackRect = new ToolStripPanel.FeedbackRectangle(toolStripToDrag.ClientRectangle);
			}
			if (!ToolStripPanel.CurrentFeedbackRect.Visible)
			{
				toolStripToDrag.SuspendCaputureMode();
				try
				{
					ToolStripPanel.CurrentFeedbackRect.Show(screenLocation);
					toolStripToDrag.CaptureInternal = true;
					return;
				}
				finally
				{
					toolStripToDrag.ResumeCaputureMode();
				}
			}
			ToolStripPanel.CurrentFeedbackRect.Move(screenLocation);
		}

		// Token: 0x06004515 RID: 17685 RVA: 0x00121DB8 File Offset: 0x0011FFB8
		internal static void ClearDragFeedback()
		{
			ToolStripPanel.FeedbackRectangle feedbackRectangle = ToolStripPanel.feedbackRect;
			ToolStripPanel.feedbackRect = null;
			if (feedbackRectangle != null)
			{
				feedbackRectangle.Dispose();
			}
		}

		// Token: 0x170010F7 RID: 4343
		// (get) Token: 0x06004516 RID: 17686 RVA: 0x00121DDA File Offset: 0x0011FFDA
		// (set) Token: 0x06004517 RID: 17687 RVA: 0x00121DE1 File Offset: 0x0011FFE1
		private static ToolStripPanel.FeedbackRectangle CurrentFeedbackRect
		{
			get
			{
				return ToolStripPanel.feedbackRect;
			}
			set
			{
				ToolStripPanel.feedbackRect = value;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Windows.Forms.ToolStrip" /> to a <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		/// <param name="toolStripToDrag">The <see cref="T:System.Windows.Forms.ToolStrip" /> to add to the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</param>
		// Token: 0x06004518 RID: 17688 RVA: 0x00121DE9 File Offset: 0x0011FFE9
		public void Join(ToolStrip toolStripToDrag)
		{
			this.Join(toolStripToDrag, Point.Empty);
		}

		/// <summary>Adds the specified <see cref="T:System.Windows.Forms.ToolStrip" /> to a <see cref="T:System.Windows.Forms.ToolStripPanel" /> in the specified row.</summary>
		/// <param name="toolStripToDrag">The <see cref="T:System.Windows.Forms.ToolStrip" /> to add to the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</param>
		/// <param name="row">An <see cref="T:System.Int32" /> representing the <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> to which the <see cref="T:System.Windows.Forms.ToolStrip" /> is added.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="row" /> parameter is less than zero (0).</exception>
		// Token: 0x06004519 RID: 17689 RVA: 0x00121DF8 File Offset: 0x0011FFF8
		public void Join(ToolStrip toolStripToDrag, int row)
		{
			if (row < 0)
			{
				throw new ArgumentOutOfRangeException("row", SR.GetString("IndexOutOfRange", new object[] { row.ToString(CultureInfo.CurrentCulture) }));
			}
			Point empty = Point.Empty;
			Rectangle rectangle = Rectangle.Empty;
			if (row >= this.RowsInternal.Count)
			{
				rectangle = this.DragBounds;
			}
			else
			{
				rectangle = this.RowsInternal[row].DragBounds;
			}
			if (this.Orientation == Orientation.Horizontal)
			{
				empty = new Point(0, rectangle.Bottom - 1);
			}
			else
			{
				empty = new Point(rectangle.Right - 1, 0);
			}
			this.Join(toolStripToDrag, empty);
		}

		/// <summary>Adds the specified <see cref="T:System.Windows.Forms.ToolStrip" /> to a <see cref="T:System.Windows.Forms.ToolStripPanel" /> at the specified coordinates.</summary>
		/// <param name="toolStripToDrag">The <see cref="T:System.Windows.Forms.ToolStrip" /> to add to the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</param>
		/// <param name="x">The horizontal client coordinate, in pixels.</param>
		/// <param name="y">The vertical client coordinate, in pixels.</param>
		// Token: 0x0600451A RID: 17690 RVA: 0x00121E9C File Offset: 0x0012009C
		public void Join(ToolStrip toolStripToDrag, int x, int y)
		{
			this.Join(toolStripToDrag, new Point(x, y));
		}

		/// <summary>Adds the specified <see cref="T:System.Windows.Forms.ToolStrip" /> to a <see cref="T:System.Windows.Forms.ToolStripPanel" /> at the specified location.</summary>
		/// <param name="toolStripToDrag">The <see cref="T:System.Windows.Forms.ToolStrip" /> to add to the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</param>
		/// <param name="location">A <see cref="T:System.Drawing.Point" /> value representing the x- and y-client coordinates, in pixels, of the new location for the <see cref="T:System.Windows.Forms.ToolStrip" />.</param>
		// Token: 0x0600451B RID: 17691 RVA: 0x00121EAC File Offset: 0x001200AC
		public void Join(ToolStrip toolStripToDrag, Point location)
		{
			if (toolStripToDrag == null)
			{
				throw new ArgumentNullException("toolStripToDrag");
			}
			if (!this.state[ToolStripPanel.stateBeginInit] && !this.state[ToolStripPanel.stateInJoin])
			{
				try
				{
					this.state[ToolStripPanel.stateInJoin] = true;
					toolStripToDrag.ParentInternal = this;
					this.MoveInsideContainer(toolStripToDrag, location);
					return;
				}
				finally
				{
					this.state[ToolStripPanel.stateInJoin] = false;
				}
			}
			base.Controls.Add(toolStripToDrag);
			toolStripToDrag.Location = location;
		}

		// Token: 0x0600451C RID: 17692 RVA: 0x00121F44 File Offset: 0x00120144
		internal void MoveControl(ToolStrip toolStripToDrag, Point screenLocation)
		{
			if (toolStripToDrag == null)
			{
				return;
			}
			Point point = base.PointToClient(screenLocation);
			if (!this.DragBounds.Contains(point))
			{
				this.MoveOutsideContainer(toolStripToDrag, screenLocation);
				return;
			}
			this.Join(toolStripToDrag, point);
		}

		// Token: 0x0600451D RID: 17693 RVA: 0x00121F84 File Offset: 0x00120184
		private void MoveInsideContainer(ToolStrip toolStripToDrag, Point clientLocation)
		{
			if (((ISupportToolStripPanel)toolStripToDrag).IsCurrentlyDragging && !this.DragBounds.Contains(clientLocation))
			{
				return;
			}
			ToolStripPanel.ClearDragFeedback();
			if (toolStripToDrag.Site != null && toolStripToDrag.Site.DesignMode && base.IsHandleCreated && (clientLocation.X < 0 || clientLocation.Y < 0))
			{
				Point point = base.PointToClient(WindowsFormsUtils.LastCursorPoint);
				if (base.ClientRectangle.Contains(point))
				{
					clientLocation = point;
				}
			}
			ToolStripPanelRow toolStripPanelRow = ((ISupportToolStripPanel)toolStripToDrag).ToolStripPanelRow;
			bool flag = false;
			if (toolStripPanelRow != null && toolStripPanelRow.Visible && toolStripPanelRow.ToolStripPanel == this)
			{
				if (toolStripToDrag.IsCurrentlyDragging)
				{
					flag = toolStripPanelRow.DragBounds.Contains(clientLocation);
				}
				else
				{
					flag = toolStripPanelRow.Bounds.Contains(clientLocation);
				}
			}
			if (flag)
			{
				((ISupportToolStripPanel)toolStripToDrag).ToolStripPanelRow.MoveControl(toolStripToDrag, this.GetStartLocation(toolStripToDrag), clientLocation);
				return;
			}
			ToolStripPanelRow toolStripPanelRow2 = this.PointToRow(clientLocation);
			if (toolStripPanelRow2 == null)
			{
				int num = this.RowsInternal.Count;
				if (this.Orientation == Orientation.Horizontal)
				{
					num = ((clientLocation.Y <= base.Padding.Left) ? 0 : num);
				}
				else
				{
					num = ((clientLocation.X <= base.Padding.Left) ? 0 : num);
				}
				ToolStripPanelRow toolStripPanelRow3 = null;
				if (this.RowsInternal.Count > 0)
				{
					if (num == 0)
					{
						toolStripPanelRow3 = this.RowsInternal[0];
					}
					else if (num > 0)
					{
						toolStripPanelRow3 = this.RowsInternal[num - 1];
					}
				}
				if (toolStripPanelRow3 != null && toolStripPanelRow3.ControlsInternal.Count == 1 && toolStripPanelRow3.ControlsInternal.Contains(toolStripToDrag))
				{
					toolStripPanelRow2 = toolStripPanelRow3;
					if (toolStripToDrag.IsInDesignMode)
					{
						Point point2 = ((this.Orientation == Orientation.Horizontal) ? new Point(clientLocation.X, toolStripPanelRow2.Bounds.Y) : new Point(toolStripPanelRow2.Bounds.X, clientLocation.Y));
						((ISupportToolStripPanel)toolStripToDrag).ToolStripPanelRow.MoveControl(toolStripToDrag, this.GetStartLocation(toolStripToDrag), point2);
					}
				}
				else
				{
					toolStripPanelRow2 = new ToolStripPanelRow(this);
					this.RowsInternal.Insert(num, toolStripPanelRow2);
				}
			}
			else if (!toolStripPanelRow2.CanMove(toolStripToDrag))
			{
				int num2 = this.RowsInternal.IndexOf(toolStripPanelRow2);
				if (toolStripPanelRow != null && toolStripPanelRow.ControlsInternal.Count == 1 && num2 > 0 && num2 - 1 == this.RowsInternal.IndexOf(toolStripPanelRow))
				{
					return;
				}
				toolStripPanelRow2 = new ToolStripPanelRow(this);
				this.RowsInternal.Insert(num2, toolStripPanelRow2);
				clientLocation.Y = toolStripPanelRow2.Bounds.Y;
			}
			bool flag2 = toolStripPanelRow != toolStripPanelRow2;
			if (!flag2 && toolStripPanelRow != null && toolStripPanelRow.ControlsInternal.Count > 1)
			{
				toolStripPanelRow.LeaveRow(toolStripToDrag);
				toolStripPanelRow = null;
				flag2 = true;
			}
			if (flag2)
			{
				if (toolStripPanelRow != null)
				{
					toolStripPanelRow.LeaveRow(toolStripToDrag);
				}
				toolStripPanelRow2.JoinRow(toolStripToDrag, clientLocation);
			}
			if (flag2 && ((ISupportToolStripPanel)toolStripToDrag).IsCurrentlyDragging)
			{
				for (int i = 0; i < this.RowsInternal.Count; i++)
				{
					LayoutTransaction.DoLayout(this.RowsInternal[i], this, PropertyNames.Rows);
				}
				if (this.RowsInternal.IndexOf(toolStripPanelRow2) > 0)
				{
					IntSecurity.AdjustCursorPosition.Assert();
					try
					{
						Point point3 = toolStripToDrag.PointToScreen(toolStripToDrag.GripRectangle.Location);
						if (this.Orientation == Orientation.Vertical)
						{
							point3.X += toolStripToDrag.GripRectangle.Width / 2;
							point3.Y = Cursor.Position.Y;
						}
						else
						{
							point3.Y += toolStripToDrag.GripRectangle.Height / 2;
							point3.X = Cursor.Position.X;
						}
						Cursor.Position = point3;
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
			}
		}

		// Token: 0x0600451E RID: 17694 RVA: 0x00122370 File Offset: 0x00120570
		private void MoveOutsideContainer(ToolStrip toolStripToDrag, Point screenLocation)
		{
			ToolStripPanel toolStripPanel = ToolStripManager.ToolStripPanelFromPoint(toolStripToDrag, screenLocation);
			if (toolStripPanel != null)
			{
				using (new LayoutTransaction(toolStripPanel, toolStripPanel, null))
				{
					toolStripPanel.MoveControl(toolStripToDrag, screenLocation);
				}
				toolStripToDrag.PerformLayout();
				return;
			}
			this.GiveToolStripPanelFeedback(toolStripToDrag, screenLocation);
		}

		/// <summary>Retrieves the <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> given a point within the <see cref="T:System.Windows.Forms.ToolStripPanel" /> client area.</summary>
		/// <param name="clientLocation">A <see cref="T:System.Drawing.Point" /> used as a reference to find the <see cref="T:System.Windows.Forms.ToolStripPanelRow" />.</param>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> that contains the <paramref name="raftingContainerPoint" />, or <see langword="null" /> if no such <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> exists.</returns>
		// Token: 0x0600451F RID: 17695 RVA: 0x001223C4 File Offset: 0x001205C4
		public ToolStripPanelRow PointToRow(Point clientLocation)
		{
			foreach (object obj in this.RowsInternal)
			{
				ToolStripPanelRow toolStripPanelRow = (ToolStripPanelRow)obj;
				Rectangle rectangle = LayoutUtils.InflateRect(toolStripPanelRow.Bounds, toolStripPanelRow.Margin);
				if (this.ParentInternal != null)
				{
					if (this.Orientation == Orientation.Horizontal && rectangle.Width == 0)
					{
						rectangle.Width = this.ParentInternal.DisplayRectangle.Width;
					}
					else if (this.Orientation == Orientation.Vertical && rectangle.Height == 0)
					{
						rectangle.Height = this.ParentInternal.DisplayRectangle.Height;
					}
				}
				if (rectangle.Contains(clientLocation))
				{
					return toolStripPanelRow;
				}
			}
			return null;
		}

		// Token: 0x06004520 RID: 17696 RVA: 0x001224A8 File Offset: 0x001206A8
		[Conditional("DEBUG")]
		private void Debug_VerifyOneToOneCellRowControlMatchup()
		{
			for (int i = 0; i < this.RowsInternal.Count; i++)
			{
				ToolStripPanelRow toolStripPanelRow = this.RowsInternal[i];
				foreach (object obj in toolStripPanelRow.Cells)
				{
					ToolStripPanelCell toolStripPanelCell = (ToolStripPanelCell)obj;
					if (toolStripPanelCell.Control != null)
					{
						ToolStripPanelRow toolStripPanelRow2 = ((ISupportToolStripPanel)toolStripPanelCell.Control).ToolStripPanelRow;
						if (toolStripPanelRow2 != toolStripPanelRow)
						{
							int num = ((toolStripPanelRow2 != null) ? this.RowsInternal.IndexOf(toolStripPanelRow2) : (-1));
						}
					}
				}
			}
		}

		// Token: 0x06004521 RID: 17697 RVA: 0x00122560 File Offset: 0x00120760
		[Conditional("DEBUG")]
		private void Debug_PrintRows()
		{
			for (int i = 0; i < this.RowsInternal.Count; i++)
			{
				for (int j = 0; j < this.RowsInternal[i].ControlsInternal.Count; j++)
				{
				}
			}
		}

		// Token: 0x06004522 RID: 17698 RVA: 0x000070A6 File Offset: 0x000052A6
		[Conditional("DEBUG")]
		private void Debug_VerifyCountRows()
		{
		}

		// Token: 0x06004523 RID: 17699 RVA: 0x001225A4 File Offset: 0x001207A4
		[Conditional("DEBUG")]
		private void Debug_VerifyNoOverlaps()
		{
			foreach (object obj in base.Controls)
			{
				Control control = (Control)obj;
				foreach (object obj2 in base.Controls)
				{
					Control control2 = (Control)obj2;
					if (control != control2)
					{
						Rectangle bounds = control.Bounds;
						bounds.Intersect(control2.Bounds);
						if (!LayoutUtils.IsZeroWidthOrHeight(bounds))
						{
							ISupportToolStripPanel supportToolStripPanel = control as ISupportToolStripPanel;
							ISupportToolStripPanel supportToolStripPanel2 = control2 as ISupportToolStripPanel;
							string text = string.Format(CultureInfo.CurrentCulture, "OVERLAP detection:\r\n{0}: {1} row {2} row bounds {3}", new object[]
							{
								(control.Name == null) ? "" : control.Name,
								control.Bounds,
								(!this.RowsInternal.Contains(supportToolStripPanel.ToolStripPanelRow)) ? "unknown" : this.RowsInternal.IndexOf(supportToolStripPanel.ToolStripPanelRow).ToString(CultureInfo.CurrentCulture),
								supportToolStripPanel.ToolStripPanelRow.Bounds
							});
							text += string.Format(CultureInfo.CurrentCulture, "\r\n{0}: {1} row {2} row bounds {3}", new object[]
							{
								(control2.Name == null) ? "" : control2.Name,
								control2.Bounds,
								(!this.RowsInternal.Contains(supportToolStripPanel2.ToolStripPanelRow)) ? "unknown" : this.RowsInternal.IndexOf(supportToolStripPanel2.ToolStripPanelRow).ToString(CultureInfo.CurrentCulture),
								supportToolStripPanel2.ToolStripPanelRow.Bounds
							});
						}
					}
				}
			}
		}

		// Token: 0x170010F8 RID: 4344
		// (get) Token: 0x06004524 RID: 17700 RVA: 0x001227C0 File Offset: 0x001209C0
		ArrangedElementCollection IArrangedElement.Children
		{
			get
			{
				return this.RowsInternal;
			}
		}

		// Token: 0x04002624 RID: 9764
		private Orientation orientation;

		// Token: 0x04002625 RID: 9765
		private static readonly Padding rowMargin = new Padding(3, 0, 0, 0);

		// Token: 0x04002626 RID: 9766
		private Padding scaledRowMargin = ToolStripPanel.rowMargin;

		// Token: 0x04002627 RID: 9767
		private ToolStripRendererSwitcher rendererSwitcher;

		// Token: 0x04002628 RID: 9768
		private Type currentRendererType = typeof(Type);

		// Token: 0x04002629 RID: 9769
		private BitVector32 state;

		// Token: 0x0400262A RID: 9770
		private ToolStripContainer owner;

		// Token: 0x0400262B RID: 9771
		internal static TraceSwitch ToolStripPanelDebug;

		// Token: 0x0400262C RID: 9772
		internal static TraceSwitch ToolStripPanelFeedbackDebug;

		// Token: 0x0400262D RID: 9773
		internal static TraceSwitch ToolStripPanelMissingRowDebug;

		// Token: 0x0400262E RID: 9774
		[ThreadStatic]
		private static Rectangle lastFeedbackRect = Rectangle.Empty;

		// Token: 0x0400262F RID: 9775
		private static readonly int PropToolStripPanelRowCollection = PropertyStore.CreateKey();

		// Token: 0x04002630 RID: 9776
		private static readonly int stateLocked = BitVector32.CreateMask();

		// Token: 0x04002631 RID: 9777
		private static readonly int stateBeginInit = BitVector32.CreateMask(ToolStripPanel.stateLocked);

		// Token: 0x04002632 RID: 9778
		private static readonly int stateChangingZOrder = BitVector32.CreateMask(ToolStripPanel.stateBeginInit);

		// Token: 0x04002633 RID: 9779
		private static readonly int stateInJoin = BitVector32.CreateMask(ToolStripPanel.stateChangingZOrder);

		// Token: 0x04002634 RID: 9780
		private static readonly int stateEndInit = BitVector32.CreateMask(ToolStripPanel.stateInJoin);

		// Token: 0x04002635 RID: 9781
		private static readonly int stateLayoutSuspended = BitVector32.CreateMask(ToolStripPanel.stateEndInit);

		// Token: 0x04002636 RID: 9782
		private static readonly int stateRightToLeftChanged = BitVector32.CreateMask(ToolStripPanel.stateLayoutSuspended);

		// Token: 0x04002637 RID: 9783
		internal static readonly Padding DragMargin = new Padding(10);

		// Token: 0x04002638 RID: 9784
		private static readonly object EventRendererChanged = new object();

		// Token: 0x04002639 RID: 9785
		[ThreadStatic]
		private static ToolStripPanel.FeedbackRectangle feedbackRect;

		// Token: 0x0200080D RID: 2061
		private class FeedbackRectangle : IDisposable
		{
			// Token: 0x06006F1B RID: 28443 RVA: 0x00197054 File Offset: 0x00195254
			public FeedbackRectangle(Rectangle bounds)
			{
				this.dropDown = new ToolStripPanel.FeedbackRectangle.FeedbackDropDown(bounds);
			}

			// Token: 0x17001851 RID: 6225
			// (get) Token: 0x06006F1C RID: 28444 RVA: 0x00197068 File Offset: 0x00195268
			// (set) Token: 0x06006F1D RID: 28445 RVA: 0x0019708C File Offset: 0x0019528C
			public bool Visible
			{
				get
				{
					return this.dropDown != null && !this.dropDown.IsDisposed && this.dropDown.Visible;
				}
				set
				{
					if (this.dropDown != null && !this.dropDown.IsDisposed)
					{
						this.dropDown.Visible = value;
					}
				}
			}

			// Token: 0x06006F1E RID: 28446 RVA: 0x001970AF File Offset: 0x001952AF
			public void Show(Point newLocation)
			{
				this.dropDown.Show(newLocation);
			}

			// Token: 0x06006F1F RID: 28447 RVA: 0x001970BD File Offset: 0x001952BD
			public void Move(Point newLocation)
			{
				this.dropDown.MoveTo(newLocation);
			}

			// Token: 0x06006F20 RID: 28448 RVA: 0x001970CB File Offset: 0x001952CB
			protected void Dispose(bool disposing)
			{
				if (disposing && this.dropDown != null)
				{
					this.Visible = false;
					this.dropDown.Dispose();
					this.dropDown = null;
				}
			}

			// Token: 0x06006F21 RID: 28449 RVA: 0x001970F1 File Offset: 0x001952F1
			public void Dispose()
			{
				this.Dispose(true);
			}

			// Token: 0x06006F22 RID: 28450 RVA: 0x001970FC File Offset: 0x001952FC
			~FeedbackRectangle()
			{
				this.Dispose(false);
			}

			// Token: 0x04004315 RID: 17173
			private ToolStripPanel.FeedbackRectangle.FeedbackDropDown dropDown;

			// Token: 0x020008CA RID: 2250
			private class FeedbackDropDown : ToolStripDropDown
			{
				// Token: 0x060072D8 RID: 29400 RVA: 0x001A3670 File Offset: 0x001A1870
				public FeedbackDropDown(Rectangle bounds)
				{
					base.SetStyle(ControlStyles.AllPaintingInWmPaint, false);
					base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
					base.SetStyle(ControlStyles.CacheText, true);
					base.AutoClose = false;
					this.AutoSize = false;
					base.DropShadowEnabled = false;
					base.Bounds = bounds;
					Rectangle rectangle = bounds;
					rectangle.Inflate(-1, -1);
					Region region = new Region(bounds);
					region.Exclude(rectangle);
					IntSecurity.ChangeWindowRegionForTopLevel.Assert();
					base.Region = region;
				}

				// Token: 0x060072D9 RID: 29401 RVA: 0x001A36F0 File Offset: 0x001A18F0
				private void ForceSynchronousPaint()
				{
					if (!base.IsDisposed && this._numPaintsServiced == 0)
					{
						try
						{
							NativeMethods.MSG msg = default(NativeMethods.MSG);
							while (UnsafeNativeMethods.PeekMessage(ref msg, new HandleRef(this, IntPtr.Zero), 15, 15, 1))
							{
								SafeNativeMethods.UpdateWindow(new HandleRef(null, msg.hwnd));
								int numPaintsServiced = this._numPaintsServiced;
								this._numPaintsServiced = numPaintsServiced + 1;
								if (numPaintsServiced > 20)
								{
									break;
								}
							}
						}
						finally
						{
							this._numPaintsServiced = 0;
						}
					}
				}

				// Token: 0x060072DA RID: 29402 RVA: 0x000070A6 File Offset: 0x000052A6
				protected override void OnPaint(PaintEventArgs e)
				{
				}

				// Token: 0x060072DB RID: 29403 RVA: 0x001A3774 File Offset: 0x001A1974
				protected override void OnPaintBackground(PaintEventArgs e)
				{
					base.Renderer.DrawToolStripBackground(new ToolStripRenderEventArgs(e.Graphics, this));
					base.Renderer.DrawToolStripBorder(new ToolStripRenderEventArgs(e.Graphics, this));
				}

				// Token: 0x060072DC RID: 29404 RVA: 0x001A37A4 File Offset: 0x001A19A4
				protected override void OnOpening(CancelEventArgs e)
				{
					base.OnOpening(e);
					e.Cancel = false;
				}

				// Token: 0x060072DD RID: 29405 RVA: 0x001A37B4 File Offset: 0x001A19B4
				public void MoveTo(Point newLocation)
				{
					base.Location = newLocation;
					this.ForceSynchronousPaint();
				}

				// Token: 0x060072DE RID: 29406 RVA: 0x001A37C3 File Offset: 0x001A19C3
				[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				protected override void WndProc(ref Message m)
				{
					if (m.Msg == 132)
					{
						m.Result = (IntPtr)(-1);
					}
					base.WndProc(ref m);
				}

				// Token: 0x04004555 RID: 17749
				private const int MAX_PAINTS_TO_SERVICE = 20;

				// Token: 0x04004556 RID: 17750
				private int _numPaintsServiced;
			}
		}

		/// <summary>Represents all the <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> objects in a <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		// Token: 0x0200080E RID: 2062
		[ListBindable(false)]
		[ComVisible(false)]
		public class ToolStripPanelRowCollection : ArrangedElementCollection, IList, ICollection, IEnumerable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" /> class in the specified <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.ToolStripPanel" /> that holds this <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</param>
			// Token: 0x06006F23 RID: 28451 RVA: 0x0019712C File Offset: 0x0019532C
			public ToolStripPanelRowCollection(ToolStripPanel owner)
			{
				this.owner = owner;
			}

			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" /> class with the specified number of rows in the specified <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.ToolStripPanel" /> that holds this <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</param>
			/// <param name="value">The number of rows in the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</param>
			// Token: 0x06006F24 RID: 28452 RVA: 0x0019713B File Offset: 0x0019533B
			public ToolStripPanelRowCollection(ToolStripPanel owner, ToolStripPanelRow[] value)
			{
				this.owner = owner;
				this.AddRange(value);
			}

			/// <summary>Gets a particular <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> within the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</summary>
			/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> within the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> of the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" /> as specified by the <paramref name="index" /> parameter.</returns>
			// Token: 0x17001852 RID: 6226
			public virtual ToolStripPanelRow this[int index]
			{
				get
				{
					return (ToolStripPanelRow)base.InnerList[index];
				}
			}

			/// <summary>Adds the specified <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> to the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> to add to the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</param>
			/// <returns>The position of the <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> in the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</returns>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="value" /> is <see langword="null" />.</exception>
			// Token: 0x06006F26 RID: 28454 RVA: 0x00197164 File Offset: 0x00195364
			public int Add(ToolStripPanelRow value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				int num = base.InnerList.Add(value);
				this.OnAdd(value, num);
				return num;
			}

			/// <summary>Adds an array of <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> objects to a <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
			/// <param name="value">An array of <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> objects.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="value" /> is <see langword="null" />.</exception>
			// Token: 0x06006F27 RID: 28455 RVA: 0x00197198 File Offset: 0x00195398
			public void AddRange(ToolStripPanelRow[] value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				ToolStripPanel toolStripPanel = this.owner;
				if (toolStripPanel != null)
				{
					toolStripPanel.SuspendLayout();
				}
				try
				{
					for (int i = 0; i < value.Length; i++)
					{
						this.Add(value[i]);
					}
				}
				finally
				{
					if (toolStripPanel != null)
					{
						toolStripPanel.ResumeLayout();
					}
				}
			}

			/// <summary>Adds the specified <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" /> to a <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" /> to add to the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="value" /> is <see langword="null" />.</exception>
			// Token: 0x06006F28 RID: 28456 RVA: 0x001971F8 File Offset: 0x001953F8
			public void AddRange(ToolStripPanel.ToolStripPanelRowCollection value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				ToolStripPanel toolStripPanel = this.owner;
				if (toolStripPanel != null)
				{
					toolStripPanel.SuspendLayout();
				}
				try
				{
					int count = value.Count;
					for (int i = 0; i < count; i++)
					{
						this.Add(value[i]);
					}
				}
				finally
				{
					if (toolStripPanel != null)
					{
						toolStripPanel.ResumeLayout();
					}
				}
			}

			/// <summary>Determines whether the specified <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> is in the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> to search for in the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</param>
			/// <returns>
			///   <see langword="true" /> if the specified <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> is in the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />; otherwise, <see langword="false" />.</returns>
			// Token: 0x06006F29 RID: 28457 RVA: 0x0011C768 File Offset: 0x0011A968
			public bool Contains(ToolStripPanelRow value)
			{
				return base.InnerList.Contains(value);
			}

			/// <summary>Removes all <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> objects from the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</summary>
			// Token: 0x06006F2A RID: 28458 RVA: 0x00197264 File Offset: 0x00195464
			public virtual void Clear()
			{
				if (this.owner != null)
				{
					this.owner.SuspendLayout();
				}
				try
				{
					while (this.Count != 0)
					{
						this.RemoveAt(this.Count - 1);
					}
				}
				finally
				{
					if (this.owner != null)
					{
						this.owner.ResumeLayout();
					}
				}
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Clear" />.</summary>
			// Token: 0x06006F2B RID: 28459 RVA: 0x001972C4 File Offset: 0x001954C4
			void IList.Clear()
			{
				this.Clear();
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
			/// <returns>
			///   <see langword="false" /> in all cases.</returns>
			// Token: 0x17001853 RID: 6227
			// (get) Token: 0x06006F2C RID: 28460 RVA: 0x0011C9DC File Offset: 0x0011ABDC
			bool IList.IsFixedSize
			{
				get
				{
					return base.InnerList.IsFixedSize;
				}
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
			/// <param name="value">The item to locate in the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</param>
			/// <returns>
			///   <see langword="true" /> if <paramref name="value" /> is a <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> found in the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />; otherwise, <see langword="false" />.</returns>
			// Token: 0x06006F2D RID: 28461 RVA: 0x0011C768 File Offset: 0x0011A968
			bool IList.Contains(object value)
			{
				return base.InnerList.Contains(value);
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
			/// <returns>
			///   <see langword="false" /> in all cases.</returns>
			// Token: 0x17001854 RID: 6228
			// (get) Token: 0x06006F2E RID: 28462 RVA: 0x0014D3E3 File Offset: 0x0014B5E3
			bool IList.IsReadOnly
			{
				get
				{
					return base.InnerList.IsReadOnly;
				}
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" />.</summary>
			/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> to remove.</param>
			// Token: 0x06006F2F RID: 28463 RVA: 0x001972CC File Offset: 0x001954CC
			void IList.RemoveAt(int index)
			{
				this.RemoveAt(index);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> to remove from the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</param>
			// Token: 0x06006F30 RID: 28464 RVA: 0x001972D5 File Offset: 0x001954D5
			void IList.Remove(object value)
			{
				this.Remove(value as ToolStripPanelRow);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
			/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
			/// <returns>The zero-based index of the item to add.</returns>
			// Token: 0x06006F31 RID: 28465 RVA: 0x001972E3 File Offset: 0x001954E3
			int IList.Add(object value)
			{
				return this.Add(value as ToolStripPanelRow);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
			/// <param name="value">The object to locate in the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</param>
			/// <returns>The index of <paramref name="value" /> if it is a <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> found in the list; otherwise, -1.</returns>
			// Token: 0x06006F32 RID: 28466 RVA: 0x001972F1 File Offset: 0x001954F1
			int IList.IndexOf(object value)
			{
				return this.IndexOf(value as ToolStripPanelRow);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
			/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
			/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> to insert into the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</param>
			// Token: 0x06006F33 RID: 28467 RVA: 0x001972FF File Offset: 0x001954FF
			void IList.Insert(int index, object value)
			{
				this.Insert(index, value as ToolStripPanelRow);
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
			/// <param name="index">The zero-based index of the element to get.</param>
			/// <returns>The <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> at the specified index.</returns>
			// Token: 0x17001855 RID: 6229
			object IList.this[int index]
			{
				get
				{
					return base.InnerList[index];
				}
				set
				{
					throw new NotSupportedException(SR.GetString("ToolStripCollectionMustInsertAndRemove"));
				}
			}

			/// <summary>Gets the index of the specified <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> in the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> to return the index of.</param>
			/// <returns>The index of the specified <see cref="T:System.Windows.Forms.ToolStripPanelRow" />.</returns>
			// Token: 0x06006F36 RID: 28470 RVA: 0x0011CACC File Offset: 0x0011ACCC
			public int IndexOf(ToolStripPanelRow value)
			{
				return base.InnerList.IndexOf(value);
			}

			/// <summary>Inserts the specified <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> at the specified location in the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</summary>
			/// <param name="index">The zero-based index at which to insert the <see cref="T:System.Windows.Forms.ToolStripPanelRow" />.</param>
			/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> to insert.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="value" /> is <see langword="null" />.</exception>
			// Token: 0x06006F37 RID: 28471 RVA: 0x0019730E File Offset: 0x0019550E
			public void Insert(int index, ToolStripPanelRow value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.InnerList.Insert(index, value);
				this.OnAdd(value, index);
			}

			// Token: 0x06006F38 RID: 28472 RVA: 0x00197333 File Offset: 0x00195533
			private void OnAdd(ToolStripPanelRow value, int index)
			{
				if (this.owner != null)
				{
					LayoutTransaction.DoLayout(this.owner, value, PropertyNames.Parent);
				}
			}

			// Token: 0x06006F39 RID: 28473 RVA: 0x000070A6 File Offset: 0x000052A6
			private void OnAfterRemove(ToolStripPanelRow row)
			{
			}

			/// <summary>Removes the specified <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> from the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> to remove.</param>
			// Token: 0x06006F3A RID: 28474 RVA: 0x0019734E File Offset: 0x0019554E
			public void Remove(ToolStripPanelRow value)
			{
				base.InnerList.Remove(value);
				this.OnAfterRemove(value);
			}

			/// <summary>Removes the <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> at the specified index from the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</summary>
			/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> to remove.</param>
			// Token: 0x06006F3B RID: 28475 RVA: 0x00197364 File Offset: 0x00195564
			public void RemoveAt(int index)
			{
				ToolStripPanelRow toolStripPanelRow = null;
				if (index < this.Count && index >= 0)
				{
					toolStripPanelRow = (ToolStripPanelRow)base.InnerList[index];
				}
				base.InnerList.RemoveAt(index);
				this.OnAfterRemove(toolStripPanelRow);
			}

			/// <summary>Copies the entire <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" /> into an existing array at a specified location within the array.</summary>
			/// <param name="array">An <see cref="T:System.Array" /> representing the array to copy the contents of the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" /> to.</param>
			/// <param name="index">The location within the destination array to copy the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" /> to.</param>
			// Token: 0x06006F3C RID: 28476 RVA: 0x0011CCA9 File Offset: 0x0011AEA9
			public void CopyTo(ToolStripPanelRow[] array, int index)
			{
				base.InnerList.CopyTo(array, index);
			}

			// Token: 0x04004316 RID: 17174
			private ToolStripPanel owner;
		}

		// Token: 0x0200080F RID: 2063
		internal class ToolStripPanelControlCollection : WindowsFormsUtils.TypedControlCollection
		{
			// Token: 0x06006F3D RID: 28477 RVA: 0x001973A5 File Offset: 0x001955A5
			public ToolStripPanelControlCollection(ToolStripPanel owner)
				: base(owner, typeof(ToolStrip))
			{
				this.owner = owner;
			}

			// Token: 0x06006F3E RID: 28478 RVA: 0x001973C0 File Offset: 0x001955C0
			internal override void AddInternal(Control value)
			{
				if (value != null)
				{
					using (new LayoutTransaction(value, value, PropertyNames.Parent))
					{
						base.AddInternal(value);
						return;
					}
				}
				base.AddInternal(value);
			}

			// Token: 0x06006F3F RID: 28479 RVA: 0x00197408 File Offset: 0x00195608
			internal void Sort()
			{
				if (this.owner.Orientation == Orientation.Horizontal)
				{
					base.InnerList.Sort(new ToolStripPanel.ToolStripPanelControlCollection.YXComparer());
					return;
				}
				base.InnerList.Sort(new ToolStripPanel.ToolStripPanelControlCollection.XYComparer());
			}

			// Token: 0x04004317 RID: 17175
			private ToolStripPanel owner;

			// Token: 0x020008CB RID: 2251
			public class XYComparer : IComparer
			{
				// Token: 0x060072E0 RID: 29408 RVA: 0x001A37E8 File Offset: 0x001A19E8
				public int Compare(object first, object second)
				{
					Control control = first as Control;
					Control control2 = second as Control;
					if (control.Bounds.X < control2.Bounds.X)
					{
						return -1;
					}
					if (control.Bounds.X != control2.Bounds.X)
					{
						return 1;
					}
					if (control.Bounds.Y < control2.Bounds.Y)
					{
						return -1;
					}
					return 1;
				}
			}

			// Token: 0x020008CC RID: 2252
			public class YXComparer : IComparer
			{
				// Token: 0x060072E2 RID: 29410 RVA: 0x001A3864 File Offset: 0x001A1A64
				public int Compare(object first, object second)
				{
					Control control = first as Control;
					Control control2 = second as Control;
					if (control.Bounds.Y < control2.Bounds.Y)
					{
						return -1;
					}
					if (control.Bounds.Y != control2.Bounds.Y)
					{
						return 1;
					}
					if (control.Bounds.X < control2.Bounds.X)
					{
						return -1;
					}
					return 1;
				}
			}
		}
	}
}
