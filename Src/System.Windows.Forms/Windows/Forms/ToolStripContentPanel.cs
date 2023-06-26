using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents the center panel of a <see cref="T:System.Windows.Forms.ToolStripContainer" /> control.</summary>
	// Token: 0x020003EB RID: 1003
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Designer("System.Windows.Forms.Design.ToolStripContentPanelDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultEvent("Load")]
	[Docking(DockingBehavior.Never)]
	[InitializationEvent("Load")]
	[ToolboxItem(false)]
	public class ToolStripContentPanel : Panel
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripContentPanel" /> class.</summary>
		// Token: 0x0600448F RID: 17551 RVA: 0x001211B0 File Offset: 0x0011F3B0
		public ToolStripContentPanel()
		{
			base.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>The mode by which the content panel automatically resizes itself.</returns>
		// Token: 0x170010CF RID: 4303
		// (get) Token: 0x06004490 RID: 17552 RVA: 0x00012E4E File Offset: 0x0001104E
		// (set) Token: 0x06004491 RID: 17553 RVA: 0x000070A6 File Offset: 0x000052A6
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

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>The edges of the container to which a control is bound and determines how a control is resized with its parent.</returns>
		// Token: 0x170010D0 RID: 4304
		// (get) Token: 0x06004492 RID: 17554 RVA: 0x000FFC2C File Offset: 0x000FDE2C
		// (set) Token: 0x06004493 RID: 17555 RVA: 0x000FFC34 File Offset: 0x000FDE34
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override AnchorStyles Anchor
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

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>
		///   <see langword="true" /> to enable automatic scrolling; otherwise, <see langword="false" />.</returns>
		// Token: 0x170010D1 RID: 4305
		// (get) Token: 0x06004494 RID: 17556 RVA: 0x000B0A3B File Offset: 0x000AEC3B
		// (set) Token: 0x06004495 RID: 17557 RVA: 0x000EC0F6 File Offset: 0x000EA2F6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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
		/// <returns>The distance between any child controls and the edges of the scrollable parent control.</returns>
		// Token: 0x170010D2 RID: 4306
		// (get) Token: 0x06004496 RID: 17558 RVA: 0x0001180F File Offset: 0x0000FA0F
		// (set) Token: 0x06004497 RID: 17559 RVA: 0x00011817 File Offset: 0x0000FA17
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
		/// <returns>The minimum size of the auto-scroll.</returns>
		// Token: 0x170010D3 RID: 4307
		// (get) Token: 0x06004498 RID: 17560 RVA: 0x00011820 File Offset: 0x0000FA20
		// (set) Token: 0x06004499 RID: 17561 RVA: 0x00011828 File Offset: 0x0000FA28
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

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>
		///   <see langword="true" /> to enable automatic sizing; otherwise, <see langword="false" />.</returns>
		// Token: 0x170010D4 RID: 4308
		// (get) Token: 0x0600449A RID: 17562 RVA: 0x000FFC09 File Offset: 0x000FDE09
		// (set) Token: 0x0600449B RID: 17563 RVA: 0x000FFC11 File Offset: 0x000FDE11
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

		/// <summary>Overridden to ensure that the background color of the <see cref="T:System.Windows.Forms.ToolStripContainer" /> reflects the background color of the <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> structure representing the background color of the <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</returns>
		// Token: 0x170010D5 RID: 4309
		// (get) Token: 0x0600449C RID: 17564 RVA: 0x0001A049 File Offset: 0x00018249
		// (set) Token: 0x0600449D RID: 17565 RVA: 0x001211C4 File Offset: 0x0011F3C4
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				if (this.ParentInternal is ToolStripContainer && value == Color.Transparent)
				{
					this.ParentInternal.BackColor = Color.Transparent;
				}
				base.BackColor = value;
			}
		}

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x1400035D RID: 861
		// (add) Token: 0x0600449E RID: 17566 RVA: 0x000FFC1A File Offset: 0x000FDE1A
		// (remove) Token: 0x0600449F RID: 17567 RVA: 0x000FFC23 File Offset: 0x000FDE23
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>
		///   <see langword="true" /> if the control causes validation; otherwise, <see langword="false" />.</returns>
		// Token: 0x170010D6 RID: 4310
		// (get) Token: 0x060044A0 RID: 17568 RVA: 0x000E28D7 File Offset: 0x000E0AD7
		// (set) Token: 0x060044A1 RID: 17569 RVA: 0x000E28DF File Offset: 0x000E0ADF
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

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x1400035E RID: 862
		// (add) Token: 0x060044A2 RID: 17570 RVA: 0x000E28E8 File Offset: 0x000E0AE8
		// (remove) Token: 0x060044A3 RID: 17571 RVA: 0x000E28F1 File Offset: 0x000E0AF1
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

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values.</returns>
		// Token: 0x170010D7 RID: 4311
		// (get) Token: 0x060044A4 RID: 17572 RVA: 0x000FC41E File Offset: 0x000FA61E
		// (set) Token: 0x060044A5 RID: 17573 RVA: 0x000FFC4E File Offset: 0x000FDE4E
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x1400035F RID: 863
		// (add) Token: 0x060044A6 RID: 17574 RVA: 0x000FFD50 File Offset: 0x000FDF50
		// (remove) Token: 0x060044A7 RID: 17575 RVA: 0x000FFD59 File Offset: 0x000FDF59
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		/// <summary>Occurs when the content panel loads.</summary>
		// Token: 0x14000360 RID: 864
		// (add) Token: 0x060044A8 RID: 17576 RVA: 0x001211F7 File Offset: 0x0011F3F7
		// (remove) Token: 0x060044A9 RID: 17577 RVA: 0x0012120A File Offset: 0x0011F40A
		[SRCategory("CatBehavior")]
		[SRDescription("ToolStripContentPanelOnLoadDescr")]
		public event EventHandler Load
		{
			add
			{
				base.Events.AddHandler(ToolStripContentPanel.EventLoad, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripContentPanel.EventLoad, value);
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>The coordinates of the upper-left corner of the control relative to the upper-left corner of its container.</returns>
		// Token: 0x170010D8 RID: 4312
		// (get) Token: 0x060044AA RID: 17578 RVA: 0x000B15D1 File Offset: 0x000AF7D1
		// (set) Token: 0x060044AB RID: 17579 RVA: 0x000B15D9 File Offset: 0x000AF7D9
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x14000361 RID: 865
		// (add) Token: 0x060044AC RID: 17580 RVA: 0x000FFD62 File Offset: 0x000FDF62
		// (remove) Token: 0x060044AD RID: 17581 RVA: 0x000FFD6B File Offset: 0x000FDF6B
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>The size that is the lower limit that GetPreferredSize can specify.</returns>
		// Token: 0x170010D9 RID: 4313
		// (get) Token: 0x060044AE RID: 17582 RVA: 0x00011A2B File Offset: 0x0000FC2B
		// (set) Token: 0x060044AF RID: 17583 RVA: 0x000FFC96 File Offset: 0x000FDE96
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Size MinimumSize
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

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>The size that is the upper limit that GetPreferredSize can specify.</returns>
		// Token: 0x170010DA RID: 4314
		// (get) Token: 0x060044B0 RID: 17584 RVA: 0x00011A0E File Offset: 0x0000FC0E
		// (set) Token: 0x060044B1 RID: 17585 RVA: 0x000FFC9F File Offset: 0x000FDE9F
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Size MaximumSize
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

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>The name of the control.</returns>
		// Token: 0x170010DB RID: 4315
		// (get) Token: 0x060044B2 RID: 17586 RVA: 0x000FFCA8 File Offset: 0x000FDEA8
		// (set) Token: 0x060044B3 RID: 17587 RVA: 0x000FFCB0 File Offset: 0x000FDEB0
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

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>The tab order of the control within its container.</returns>
		// Token: 0x170010DC RID: 4316
		// (get) Token: 0x060044B4 RID: 17588 RVA: 0x000B2372 File Offset: 0x000B0572
		// (set) Token: 0x060044B5 RID: 17589 RVA: 0x000B237A File Offset: 0x000B057A
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
		// Token: 0x14000362 RID: 866
		// (add) Token: 0x060044B6 RID: 17590 RVA: 0x000B2383 File Offset: 0x000B0583
		// (remove) Token: 0x060044B7 RID: 17591 RVA: 0x000B238C File Offset: 0x000B058C
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
		///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.ToolStripContentPanel" /> can be tabbed to; otherwise, <see langword="false" />.</returns>
		// Token: 0x170010DD RID: 4317
		// (get) Token: 0x060044B8 RID: 17592 RVA: 0x000FFCE8 File Offset: 0x000FDEE8
		// (set) Token: 0x060044B9 RID: 17593 RVA: 0x000FFCF0 File Offset: 0x000FDEF0
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
				base.TabStop = value;
			}
		}

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x14000363 RID: 867
		// (add) Token: 0x060044BA RID: 17594 RVA: 0x000B23A6 File Offset: 0x000B05A6
		// (remove) Token: 0x060044BB RID: 17595 RVA: 0x000B23AF File Offset: 0x000B05AF
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

		// Token: 0x170010DE RID: 4318
		// (get) Token: 0x060044BC RID: 17596 RVA: 0x0012121D File Offset: 0x0011F41D
		private ToolStripRendererSwitcher RendererSwitcher
		{
			get
			{
				if (this.rendererSwitcher == null)
				{
					this.rendererSwitcher = new ToolStripRendererSwitcher(this, ToolStripRenderMode.System);
					this.HandleRendererChanged(this, EventArgs.Empty);
					this.rendererSwitcher.RendererChanged += this.HandleRendererChanged;
				}
				return this.rendererSwitcher;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Windows.Forms.ToolStripRenderer" /> used to customize the appearance of a <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripRenderer" /> that handles painting.</returns>
		// Token: 0x170010DF RID: 4319
		// (get) Token: 0x060044BD RID: 17597 RVA: 0x0012125D File Offset: 0x0011F45D
		// (set) Token: 0x060044BE RID: 17598 RVA: 0x0012126A File Offset: 0x0011F46A
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

		/// <summary>Gets or sets the painting styles to be applied to the <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripRenderMode" /> values.</returns>
		// Token: 0x170010E0 RID: 4320
		// (get) Token: 0x060044BF RID: 17599 RVA: 0x00121278 File Offset: 0x0011F478
		// (set) Token: 0x060044C0 RID: 17600 RVA: 0x00121285 File Offset: 0x0011F485
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripContentPanel.Renderer" /> property changes.</summary>
		// Token: 0x14000364 RID: 868
		// (add) Token: 0x060044C1 RID: 17601 RVA: 0x00121293 File Offset: 0x0011F493
		// (remove) Token: 0x060044C2 RID: 17602 RVA: 0x001212A6 File Offset: 0x0011F4A6
		[SRCategory("CatAppearance")]
		[SRDescription("ToolStripRendererChanged")]
		public event EventHandler RendererChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripContentPanel.EventRendererChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripContentPanel.EventRendererChanged, value);
			}
		}

		// Token: 0x060044C3 RID: 17603 RVA: 0x001212B9 File Offset: 0x0011F4B9
		private void HandleRendererChanged(object sender, EventArgs e)
		{
			this.OnRendererChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060044C4 RID: 17604 RVA: 0x001212C2 File Offset: 0x0011F4C2
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (!base.RecreatingHandle)
			{
				this.OnLoad(EventArgs.Empty);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Form.Load" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060044C5 RID: 17605 RVA: 0x001212E0 File Offset: 0x0011F4E0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnLoad(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripContentPanel.EventLoad];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Renders the <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		// Token: 0x060044C6 RID: 17606 RVA: 0x00121310 File Offset: 0x0011F510
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			ToolStripContentPanelRenderEventArgs toolStripContentPanelRenderEventArgs = new ToolStripContentPanelRenderEventArgs(e.Graphics, this);
			this.Renderer.DrawToolStripContentPanelBackground(toolStripContentPanelRenderEventArgs);
			if (!toolStripContentPanelRenderEventArgs.Handled)
			{
				base.OnPaintBackground(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripContentPanel.RendererChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060044C7 RID: 17607 RVA: 0x00121348 File Offset: 0x0011F548
		protected virtual void OnRendererChanged(EventArgs e)
		{
			if (this.Renderer is ToolStripProfessionalRenderer)
			{
				this.state[ToolStripContentPanel.stateLastDoubleBuffer] = this.DoubleBuffered;
				base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			}
			else
			{
				this.DoubleBuffered = this.state[ToolStripContentPanel.stateLastDoubleBuffer];
			}
			this.Renderer.InitializeContentPanel(this);
			base.Invalidate();
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripContentPanel.EventRendererChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060044C8 RID: 17608 RVA: 0x001213CF File Offset: 0x0011F5CF
		private void ResetRenderMode()
		{
			this.RendererSwitcher.ResetRenderMode();
		}

		// Token: 0x060044C9 RID: 17609 RVA: 0x001213DC File Offset: 0x0011F5DC
		private bool ShouldSerializeRenderMode()
		{
			return this.RendererSwitcher.ShouldSerializeRenderMode();
		}

		// Token: 0x0400261F RID: 9759
		private ToolStripRendererSwitcher rendererSwitcher;

		// Token: 0x04002620 RID: 9760
		private BitVector32 state;

		// Token: 0x04002621 RID: 9761
		private static readonly int stateLastDoubleBuffer = BitVector32.CreateMask();

		// Token: 0x04002622 RID: 9762
		private static readonly object EventRendererChanged = new object();

		// Token: 0x04002623 RID: 9763
		private static readonly object EventLoad = new object();
	}
}
