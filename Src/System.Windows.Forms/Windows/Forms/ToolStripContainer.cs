using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides panels on each side of the form and a central panel that can hold one or more controls.</summary>
	// Token: 0x020003EA RID: 1002
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Designer("System.Windows.Forms.Design.ToolStripContainerDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("ToolStripContainerDesc")]
	public class ToolStripContainer : ContainerControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripContainer" /> class.</summary>
		// Token: 0x06004458 RID: 17496 RVA: 0x00120E9C File Offset: 0x0011F09C
		public ToolStripContainer()
		{
			base.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
			base.SuspendLayout();
			try
			{
				this.topPanel = new ToolStripPanel(this);
				this.bottomPanel = new ToolStripPanel(this);
				this.leftPanel = new ToolStripPanel(this);
				this.rightPanel = new ToolStripPanel(this);
				this.contentPanel = new ToolStripContentPanel();
				this.contentPanel.Dock = DockStyle.Fill;
				this.topPanel.Dock = DockStyle.Top;
				this.bottomPanel.Dock = DockStyle.Bottom;
				this.rightPanel.Dock = DockStyle.Right;
				this.leftPanel.Dock = DockStyle.Left;
				ToolStripContainer.ToolStripContainerTypedControlCollection toolStripContainerTypedControlCollection = this.Controls as ToolStripContainer.ToolStripContainerTypedControlCollection;
				if (toolStripContainerTypedControlCollection != null)
				{
					toolStripContainerTypedControlCollection.AddInternal(this.contentPanel);
					toolStripContainerTypedControlCollection.AddInternal(this.leftPanel);
					toolStripContainerTypedControlCollection.AddInternal(this.rightPanel);
					toolStripContainerTypedControlCollection.AddInternal(this.topPanel);
					toolStripContainerTypedControlCollection.AddInternal(this.bottomPanel);
				}
			}
			finally
			{
				base.ResumeLayout(true);
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>
		///   <see langword="true" /> to enable automatic scrolling; otherwise, <see langword="false" />.</returns>
		// Token: 0x170010BA RID: 4282
		// (get) Token: 0x06004459 RID: 17497 RVA: 0x000B0A3B File Offset: 0x000AEC3B
		// (set) Token: 0x0600445A RID: 17498 RVA: 0x000EC0F6 File Offset: 0x000EA2F6
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

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> value.</returns>
		// Token: 0x170010BB RID: 4283
		// (get) Token: 0x0600445B RID: 17499 RVA: 0x0001180F File Offset: 0x0000FA0F
		// (set) Token: 0x0600445C RID: 17500 RVA: 0x00011817 File Offset: 0x0000FA17
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

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> value.</returns>
		// Token: 0x170010BC RID: 4284
		// (get) Token: 0x0600445D RID: 17501 RVA: 0x00011820 File Offset: 0x0000FA20
		// (set) Token: 0x0600445E RID: 17502 RVA: 0x00011828 File Offset: 0x0000FA28
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

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> value.</returns>
		// Token: 0x170010BD RID: 4285
		// (get) Token: 0x0600445F RID: 17503 RVA: 0x0001A049 File Offset: 0x00018249
		// (set) Token: 0x06004460 RID: 17504 RVA: 0x00012D84 File Offset: 0x00010F84
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Color BackColor
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

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x14000356 RID: 854
		// (add) Token: 0x06004461 RID: 17505 RVA: 0x00058BF2 File Offset: 0x00056DF2
		// (remove) Token: 0x06004462 RID: 17506 RVA: 0x00058BFB File Offset: 0x00056DFB
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>TTThe background image displayed in the control.</returns>
		// Token: 0x170010BE RID: 4286
		// (get) Token: 0x06004463 RID: 17507 RVA: 0x0001187C File Offset: 0x0000FA7C
		// (set) Token: 0x06004464 RID: 17508 RVA: 0x00011884 File Offset: 0x0000FA84
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Image BackgroundImage
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

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x14000357 RID: 855
		// (add) Token: 0x06004465 RID: 17509 RVA: 0x0001188D File Offset: 0x0000FA8D
		// (remove) Token: 0x06004466 RID: 17510 RVA: 0x00011896 File Offset: 0x0000FA96
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>The background image layout as defined in the ImageLayout enumeration.</returns>
		// Token: 0x170010BF RID: 4287
		// (get) Token: 0x06004467 RID: 17511 RVA: 0x0001189F File Offset: 0x0000FA9F
		// (set) Token: 0x06004468 RID: 17512 RVA: 0x000118A7 File Offset: 0x0000FAA7
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

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x14000358 RID: 856
		// (add) Token: 0x06004469 RID: 17513 RVA: 0x000118B0 File Offset: 0x0000FAB0
		// (remove) Token: 0x0600446A RID: 17514 RVA: 0x000118B0 File Offset: 0x0000FAB0
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new event EventHandler BackgroundImageLayoutChanged
		{
			add
			{
				base.BackgroundImageLayoutChanged += value;
			}
			remove
			{
				base.BackgroundImageLayoutChanged += value;
			}
		}

		/// <summary>Gets the bottom panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripPanel" /> representing the bottom panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</returns>
		// Token: 0x170010C0 RID: 4288
		// (get) Token: 0x0600446B RID: 17515 RVA: 0x00120FA0 File Offset: 0x0011F1A0
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripContainerBottomToolStripPanelDescr")]
		[Localizable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ToolStripPanel BottomToolStripPanel
		{
			get
			{
				return this.bottomPanel;
			}
		}

		/// <summary>Gets or sets a value indicating whether the bottom panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" /> is visible.</summary>
		/// <returns>
		///   <see langword="true" /> if the bottom panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" /> is visible; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170010C1 RID: 4289
		// (get) Token: 0x0600446C RID: 17516 RVA: 0x00120FA8 File Offset: 0x0011F1A8
		// (set) Token: 0x0600446D RID: 17517 RVA: 0x00120FB5 File Offset: 0x0011F1B5
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripContainerBottomToolStripPanelVisibleDescr")]
		[DefaultValue(true)]
		public bool BottomToolStripPanelVisible
		{
			get
			{
				return this.BottomToolStripPanel.Visible;
			}
			set
			{
				this.BottomToolStripPanel.Visible = value;
			}
		}

		/// <summary>Gets the center panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripContentPanel" /> representing the center panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</returns>
		// Token: 0x170010C2 RID: 4290
		// (get) Token: 0x0600446E RID: 17518 RVA: 0x00120FC3 File Offset: 0x0011F1C3
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripContainerContentPanelDescr")]
		[Localizable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ToolStripContentPanel ContentPanel
		{
			get
			{
				return this.contentPanel;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>
		///   <see langword="true" /> if the control causes validation; otherwise, <see langword="false" />.</returns>
		// Token: 0x170010C3 RID: 4291
		// (get) Token: 0x0600446F RID: 17519 RVA: 0x000E28D7 File Offset: 0x000E0AD7
		// (set) Token: 0x06004470 RID: 17520 RVA: 0x000E28DF File Offset: 0x000E0ADF
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool CausesValidation
		{
			get
			{
				return base.CausesValidation;
			}
			set
			{
				base.CausesValidation = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripContainer.CausesValidation" /> property changes.</summary>
		// Token: 0x14000359 RID: 857
		// (add) Token: 0x06004471 RID: 17521 RVA: 0x000E28E8 File Offset: 0x000E0AE8
		// (remove) Token: 0x06004472 RID: 17522 RVA: 0x000E28F1 File Offset: 0x000E0AF1
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler CausesValidationChanged
		{
			add
			{
				base.CausesValidationChanged += value;
			}
			remove
			{
				base.CausesValidationChanged -= value;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>The ContextMenuStrip associated with this control.</returns>
		// Token: 0x170010C4 RID: 4292
		// (get) Token: 0x06004473 RID: 17523 RVA: 0x00011936 File Offset: 0x0000FB36
		// (set) Token: 0x06004474 RID: 17524 RVA: 0x00112AB6 File Offset: 0x00110CB6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ContextMenuStrip ContextMenuStrip
		{
			get
			{
				return base.ContextMenuStrip;
			}
			set
			{
				base.ContextMenuStrip = value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripContainer.ContextMenuStrip" /> property changes.</summary>
		// Token: 0x1400035A RID: 858
		// (add) Token: 0x06004475 RID: 17525 RVA: 0x00112ABF File Offset: 0x00110CBF
		// (remove) Token: 0x06004476 RID: 17526 RVA: 0x00112AC8 File Offset: 0x00110CC8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ContextMenuStripChanged
		{
			add
			{
				base.ContextMenuStripChanged += value;
			}
			remove
			{
				base.ContextMenuStripChanged -= value;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>The cursor that is displayed when the mouse pointer is over the control.</returns>
		// Token: 0x170010C5 RID: 4293
		// (get) Token: 0x06004477 RID: 17527 RVA: 0x0001A0A0 File Offset: 0x000182A0
		// (set) Token: 0x06004478 RID: 17528 RVA: 0x0001A0A8 File Offset: 0x000182A8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Cursor Cursor
		{
			get
			{
				return base.Cursor;
			}
			set
			{
				base.Cursor = value;
			}
		}

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x1400035B RID: 859
		// (add) Token: 0x06004479 RID: 17529 RVA: 0x0004620F File Offset: 0x0004440F
		// (remove) Token: 0x0600447A RID: 17530 RVA: 0x00046218 File Offset: 0x00044418
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new event EventHandler CursorChanged
		{
			add
			{
				base.CursorChanged += value;
			}
			remove
			{
				base.CursorChanged -= value;
			}
		}

		/// <summary>Gets the default size of the <see cref="T:System.Windows.Forms.ToolStripContainer" />, in pixels.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> representing the horizontal and vertical dimensions of the <see cref="T:System.Windows.Forms.ToolStripContainer" />, in pixels.</returns>
		// Token: 0x170010C6 RID: 4294
		// (get) Token: 0x0600447B RID: 17531 RVA: 0x00120FCB File Offset: 0x0011F1CB
		protected override Size DefaultSize
		{
			get
			{
				return new Size(150, 175);
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>The foreground color of the control.</returns>
		// Token: 0x170010C7 RID: 4295
		// (get) Token: 0x0600447C RID: 17532 RVA: 0x0001A0E7 File Offset: 0x000182E7
		// (set) Token: 0x0600447D RID: 17533 RVA: 0x00013024 File Offset: 0x00011224
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Color ForeColor
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

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x1400035C RID: 860
		// (add) Token: 0x0600447E RID: 17534 RVA: 0x0005A8EE File Offset: 0x00058AEE
		// (remove) Token: 0x0600447F RID: 17535 RVA: 0x0005A8F7 File Offset: 0x00058AF7
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		/// <summary>Gets the left panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripPanel" /> representing the left panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</returns>
		// Token: 0x170010C8 RID: 4296
		// (get) Token: 0x06004480 RID: 17536 RVA: 0x00120FDC File Offset: 0x0011F1DC
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripContainerLeftToolStripPanelDescr")]
		[Localizable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ToolStripPanel LeftToolStripPanel
		{
			get
			{
				return this.leftPanel;
			}
		}

		/// <summary>Gets or sets a value indicating whether the left panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" /> is visible.</summary>
		/// <returns>
		///   <see langword="true" /> if the left panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" /> is visible; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170010C9 RID: 4297
		// (get) Token: 0x06004481 RID: 17537 RVA: 0x00120FE4 File Offset: 0x0011F1E4
		// (set) Token: 0x06004482 RID: 17538 RVA: 0x00120FF1 File Offset: 0x0011F1F1
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripContainerLeftToolStripPanelVisibleDescr")]
		[DefaultValue(true)]
		public bool LeftToolStripPanelVisible
		{
			get
			{
				return this.LeftToolStripPanel.Visible;
			}
			set
			{
				this.LeftToolStripPanel.Visible = value;
			}
		}

		/// <summary>Gets the right panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripPanel" /> representing the right panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</returns>
		// Token: 0x170010CA RID: 4298
		// (get) Token: 0x06004483 RID: 17539 RVA: 0x00120FFF File Offset: 0x0011F1FF
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripContainerRightToolStripPanelDescr")]
		[Localizable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ToolStripPanel RightToolStripPanel
		{
			get
			{
				return this.rightPanel;
			}
		}

		/// <summary>Gets or sets a value indicating whether the right panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" /> is visible.</summary>
		/// <returns>
		///   <see langword="true" /> if the right panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" /> is visible; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170010CB RID: 4299
		// (get) Token: 0x06004484 RID: 17540 RVA: 0x00121007 File Offset: 0x0011F207
		// (set) Token: 0x06004485 RID: 17541 RVA: 0x00121014 File Offset: 0x0011F214
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripContainerRightToolStripPanelVisibleDescr")]
		[DefaultValue(true)]
		public bool RightToolStripPanelVisible
		{
			get
			{
				return this.RightToolStripPanel.Visible;
			}
			set
			{
				this.RightToolStripPanel.Visible = value;
			}
		}

		/// <summary>Gets the top panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripPanel" /> representing the top panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</returns>
		// Token: 0x170010CC RID: 4300
		// (get) Token: 0x06004486 RID: 17542 RVA: 0x00121022 File Offset: 0x0011F222
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripContainerTopToolStripPanelDescr")]
		[Localizable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ToolStripPanel TopToolStripPanel
		{
			get
			{
				return this.topPanel;
			}
		}

		/// <summary>Gets or sets a value indicating whether the top panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" /> is visible.</summary>
		/// <returns>
		///   <see langword="true" /> if the top panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" /> is visible; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170010CD RID: 4301
		// (get) Token: 0x06004487 RID: 17543 RVA: 0x0012102A File Offset: 0x0011F22A
		// (set) Token: 0x06004488 RID: 17544 RVA: 0x00121037 File Offset: 0x0011F237
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripContainerTopToolStripPanelVisibleDescr")]
		[DefaultValue(true)]
		public bool TopToolStripPanelVisible
		{
			get
			{
				return this.TopToolStripPanel.Visible;
			}
			set
			{
				this.TopToolStripPanel.Visible = value;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>The collection of controls contained within the control.</returns>
		// Token: 0x170010CE RID: 4302
		// (get) Token: 0x06004489 RID: 17545 RVA: 0x000EC38A File Offset: 0x000EA58A
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Control.ControlCollection Controls
		{
			get
			{
				return base.Controls;
			}
		}

		/// <summary>Creates and returns a <see cref="T:System.Windows.Forms.ToolStripContainer" /> collection.</summary>
		/// <returns>A read-only <see cref="T:System.Windows.Forms.ToolStripContainer" /> collection.</returns>
		// Token: 0x0600448A RID: 17546 RVA: 0x00121045 File Offset: 0x0011F245
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override Control.ControlCollection CreateControlsInstance()
		{
			return new ToolStripContainer.ToolStripContainerTypedControlCollection(this, true);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.RightToLeftChanged" /> event.</summary>
		/// <param name="e">The event data.</param>
		// Token: 0x0600448B RID: 17547 RVA: 0x00121050 File Offset: 0x0011F250
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
			RightToLeft rightToLeft = this.RightToLeft;
			if (rightToLeft == RightToLeft.Yes)
			{
				this.RightToolStripPanel.Dock = DockStyle.Left;
				this.LeftToolStripPanel.Dock = DockStyle.Right;
				return;
			}
			this.RightToolStripPanel.Dock = DockStyle.Right;
			this.LeftToolStripPanel.Dock = DockStyle.Left;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600448C RID: 17548 RVA: 0x001210A0 File Offset: 0x0011F2A0
		protected override void OnSizeChanged(EventArgs e)
		{
			foreach (object obj in this.Controls)
			{
				Control control = (Control)obj;
				control.SuspendLayout();
			}
			base.OnSizeChanged(e);
			foreach (object obj2 in this.Controls)
			{
				Control control2 = (Control)obj2;
				control2.ResumeLayout();
			}
		}

		// Token: 0x0600448D RID: 17549 RVA: 0x00121148 File Offset: 0x0011F348
		internal override void RecreateHandleCore()
		{
			if (base.IsHandleCreated)
			{
				foreach (object obj in this.Controls)
				{
					Control control = (Control)obj;
					control.CreateControl(true);
				}
			}
			base.RecreateHandleCore();
		}

		// Token: 0x0600448E RID: 17550 RVA: 0x0001180C File Offset: 0x0000FA0C
		internal override bool AllowsKeyboardToolTip()
		{
			return false;
		}

		// Token: 0x0400261A RID: 9754
		private ToolStripPanel topPanel;

		// Token: 0x0400261B RID: 9755
		private ToolStripPanel bottomPanel;

		// Token: 0x0400261C RID: 9756
		private ToolStripPanel leftPanel;

		// Token: 0x0400261D RID: 9757
		private ToolStripPanel rightPanel;

		// Token: 0x0400261E RID: 9758
		private ToolStripContentPanel contentPanel;

		// Token: 0x0200080C RID: 2060
		internal class ToolStripContainerTypedControlCollection : WindowsFormsUtils.ReadOnlyControlCollection
		{
			// Token: 0x06006F17 RID: 28439 RVA: 0x00196EF4 File Offset: 0x001950F4
			public ToolStripContainerTypedControlCollection(Control c, bool isReadOnly)
				: base(c, isReadOnly)
			{
				this.owner = c as ToolStripContainer;
			}

			// Token: 0x06006F18 RID: 28440 RVA: 0x00196F2C File Offset: 0x0019512C
			public override void Add(Control value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this.IsReadOnly)
				{
					throw new NotSupportedException(SR.GetString("ToolStripContainerUseContentPanel"));
				}
				Type type = value.GetType();
				if (!this.contentPanelType.IsAssignableFrom(type) && !this.panelType.IsAssignableFrom(type))
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, SR.GetString("TypedControlCollectionShouldBeOfTypes", new object[]
					{
						this.contentPanelType.Name,
						this.panelType.Name
					}), new object[0]), value.GetType().Name);
				}
				base.Add(value);
			}

			// Token: 0x06006F19 RID: 28441 RVA: 0x00196FD6 File Offset: 0x001951D6
			public override void Remove(Control value)
			{
				if ((value is ToolStripPanel || value is ToolStripContentPanel) && !this.owner.DesignMode && this.IsReadOnly)
				{
					throw new NotSupportedException(SR.GetString("ReadonlyControlsCollection"));
				}
				base.Remove(value);
			}

			// Token: 0x06006F1A RID: 28442 RVA: 0x00197014 File Offset: 0x00195214
			internal override void SetChildIndexInternal(Control child, int newIndex)
			{
				if (child is ToolStripPanel || child is ToolStripContentPanel)
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

			// Token: 0x04004312 RID: 17170
			private ToolStripContainer owner;

			// Token: 0x04004313 RID: 17171
			private Type contentPanelType = typeof(ToolStripContentPanel);

			// Token: 0x04004314 RID: 17172
			private Type panelType = typeof(ToolStripPanel);
		}
	}
}
