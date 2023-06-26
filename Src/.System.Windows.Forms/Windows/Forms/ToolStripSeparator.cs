using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design;

namespace System.Windows.Forms
{
	/// <summary>Represents a line used to group items of a <see cref="T:System.Windows.Forms.ToolStrip" /> or the drop-down items of a <see cref="T:System.Windows.Forms.MenuStrip" /> or <see cref="T:System.Windows.Forms.ContextMenuStrip" /> or other <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control.</summary>
	// Token: 0x020003FD RID: 1021
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.ContextMenuStrip)]
	public class ToolStripSeparator : ToolStripItem
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripSeparator" /> class.</summary>
		// Token: 0x06004685 RID: 18053 RVA: 0x00128979 File Offset: 0x00126B79
		public ToolStripSeparator()
		{
			this.ForeColor = SystemColors.ControlDark;
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>
		///   <see langword="true" /> if enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17001146 RID: 4422
		// (get) Token: 0x06004686 RID: 18054 RVA: 0x00110E48 File Offset: 0x0010F048
		// (set) Token: 0x06004687 RID: 18055 RVA: 0x00110E50 File Offset: 0x0010F050
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool AutoToolTip
		{
			get
			{
				return base.AutoToolTip;
			}
			set
			{
				base.AutoToolTip = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>The image to display in the background of the separator.</returns>
		// Token: 0x17001147 RID: 4423
		// (get) Token: 0x06004688 RID: 18056 RVA: 0x0012898C File Offset: 0x00126B8C
		// (set) Token: 0x06004689 RID: 18057 RVA: 0x00128994 File Offset: 0x00126B94
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

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImageLayout" /> values.</returns>
		// Token: 0x17001148 RID: 4424
		// (get) Token: 0x0600468A RID: 18058 RVA: 0x0012899D File Offset: 0x00126B9D
		// (set) Token: 0x0600468B RID: 18059 RVA: 0x001289A5 File Offset: 0x00126BA5
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

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripSeparator" /> can be selected.</summary>
		/// <returns>
		///   <see langword="true" /> if the component using the <see cref="T:System.Windows.Forms.ToolStripSeparator" /> is in design mode; otherwise, <see langword="false" />.</returns>
		// Token: 0x17001149 RID: 4425
		// (get) Token: 0x0600468C RID: 18060 RVA: 0x0010C201 File Offset: 0x0010A401
		public override bool CanSelect
		{
			get
			{
				return base.DesignMode;
			}
		}

		/// <summary>Gets the default size of the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" /> of the <see cref="T:System.Windows.Forms.ToolStripSeparator" />, measured in pixels. The default is 100 pixels horizontally.</returns>
		// Token: 0x1700114A RID: 4426
		// (get) Token: 0x0600468D RID: 18061 RVA: 0x001289AE File Offset: 0x00126BAE
		protected override Size DefaultSize
		{
			get
			{
				return new Size(6, 6);
			}
		}

		/// <summary>Gets the spacing between the <see cref="T:System.Windows.Forms.ToolStripSeparator" /> and an adjacent item.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value representing the spacing.</returns>
		// Token: 0x1700114B RID: 4427
		// (get) Token: 0x0600468E RID: 18062 RVA: 0x00019A61 File Offset: 0x00017C61
		protected internal override Padding DefaultMargin
		{
			get
			{
				return Padding.Empty;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>
		///   <see langword="true" /> if enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700114C RID: 4428
		// (get) Token: 0x0600468F RID: 18063 RVA: 0x001119E9 File Offset: 0x0010FBE9
		// (set) Token: 0x06004690 RID: 18064 RVA: 0x001119F1 File Offset: 0x0010FBF1
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool DoubleClickEnabled
		{
			get
			{
				return base.DoubleClickEnabled;
			}
			set
			{
				base.DoubleClickEnabled = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>
		///   <see langword="true" /> if enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700114D RID: 4429
		// (get) Token: 0x06004691 RID: 18065 RVA: 0x001289B7 File Offset: 0x00126BB7
		// (set) Token: 0x06004692 RID: 18066 RVA: 0x0011F341 File Offset: 0x0011D541
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x14000388 RID: 904
		// (add) Token: 0x06004693 RID: 18067 RVA: 0x001289BF File Offset: 0x00126BBF
		// (remove) Token: 0x06004694 RID: 18068 RVA: 0x001289C8 File Offset: 0x00126BC8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler EnabledChanged
		{
			add
			{
				base.EnabledChanged += value;
			}
			remove
			{
				base.EnabledChanged -= value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripItemDisplayStyle" /> values.</returns>
		// Token: 0x1700114E RID: 4430
		// (get) Token: 0x06004695 RID: 18069 RVA: 0x001119B2 File Offset: 0x0010FBB2
		// (set) Token: 0x06004696 RID: 18070 RVA: 0x001119BA File Offset: 0x0010FBBA
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ToolStripItemDisplayStyle DisplayStyle
		{
			get
			{
				return base.DisplayStyle;
			}
			set
			{
				base.DisplayStyle = value;
			}
		}

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x14000389 RID: 905
		// (add) Token: 0x06004697 RID: 18071 RVA: 0x001289D1 File Offset: 0x00126BD1
		// (remove) Token: 0x06004698 RID: 18072 RVA: 0x001289DA File Offset: 0x00126BDA
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler DisplayStyleChanged
		{
			add
			{
				base.DisplayStyleChanged += value;
			}
			remove
			{
				base.DisplayStyleChanged -= value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</returns>
		// Token: 0x1700114F RID: 4431
		// (get) Token: 0x06004699 RID: 18073 RVA: 0x001289E3 File Offset: 0x00126BE3
		// (set) Token: 0x0600469A RID: 18074 RVA: 0x001289EB File Offset: 0x00126BEB
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values.</returns>
		// Token: 0x17001150 RID: 4432
		// (get) Token: 0x0600469B RID: 18075 RVA: 0x00111AD7 File Offset: 0x0010FCD7
		// (set) Token: 0x0600469C RID: 18076 RVA: 0x00111ADF File Offset: 0x0010FCDF
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ContentAlignment ImageAlign
		{
			get
			{
				return base.ImageAlign;
			}
			set
			{
				base.ImageAlign = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>The image to be displayed.</returns>
		// Token: 0x17001151 RID: 4433
		// (get) Token: 0x0600469D RID: 18077 RVA: 0x00111AA4 File Offset: 0x0010FCA4
		// (set) Token: 0x0600469E RID: 18078 RVA: 0x00111AAC File Offset: 0x0010FCAC
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Image Image
		{
			get
			{
				return base.Image;
			}
			set
			{
				base.Image = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>The index of the image that is displayed.</returns>
		// Token: 0x17001152 RID: 4434
		// (get) Token: 0x0600469F RID: 18079 RVA: 0x001289F4 File Offset: 0x00126BF4
		// (set) Token: 0x060046A0 RID: 18080 RVA: 0x001289FC File Offset: 0x00126BFC
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new int ImageIndex
		{
			get
			{
				return base.ImageIndex;
			}
			set
			{
				base.ImageIndex = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>The key for the image that is displayed for the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</returns>
		// Token: 0x17001153 RID: 4435
		// (get) Token: 0x060046A1 RID: 18081 RVA: 0x00128A05 File Offset: 0x00126C05
		// (set) Token: 0x060046A2 RID: 18082 RVA: 0x00128A0D File Offset: 0x00126C0D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new string ImageKey
		{
			get
			{
				return base.ImageKey;
			}
			set
			{
				base.ImageKey = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Color" /> values.</returns>
		// Token: 0x17001154 RID: 4436
		// (get) Token: 0x060046A3 RID: 18083 RVA: 0x00111AC6 File Offset: 0x0010FCC6
		// (set) Token: 0x060046A4 RID: 18084 RVA: 0x00111ACE File Offset: 0x0010FCCE
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Color ImageTransparentColor
		{
			get
			{
				return base.ImageTransparentColor;
			}
			set
			{
				base.ImageTransparentColor = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripItemImageScaling" /> value.</returns>
		// Token: 0x17001155 RID: 4437
		// (get) Token: 0x060046A5 RID: 18085 RVA: 0x00111AB5 File Offset: 0x0010FCB5
		// (set) Token: 0x060046A6 RID: 18086 RVA: 0x00111ABD File Offset: 0x0010FCBD
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ToolStripItemImageScaling ImageScaling
		{
			get
			{
				return base.ImageScaling;
			}
			set
			{
				base.ImageScaling = value;
			}
		}

		// Token: 0x17001156 RID: 4438
		// (get) Token: 0x060046A7 RID: 18087 RVA: 0x00128A18 File Offset: 0x00126C18
		private bool IsVertical
		{
			get
			{
				ToolStrip toolStrip = base.ParentInternal;
				if (toolStrip == null)
				{
					toolStrip = base.Owner;
				}
				ToolStripDropDownMenu toolStripDropDownMenu = toolStrip as ToolStripDropDownMenu;
				if (toolStripDropDownMenu != null)
				{
					return false;
				}
				switch (toolStrip.LayoutStyle)
				{
				case ToolStripLayoutStyle.VerticalStackWithOverflow:
					return false;
				}
				return true;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the item's text.</returns>
		// Token: 0x17001157 RID: 4439
		// (get) Token: 0x060046A8 RID: 18088 RVA: 0x00128A66 File Offset: 0x00126C66
		// (set) Token: 0x060046A9 RID: 18089 RVA: 0x00128A6E File Offset: 0x00126C6E
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

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x1400038A RID: 906
		// (add) Token: 0x060046AA RID: 18090 RVA: 0x00126D18 File Offset: 0x00124F18
		// (remove) Token: 0x060046AB RID: 18091 RVA: 0x00126D21 File Offset: 0x00124F21
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

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> value.</returns>
		// Token: 0x17001158 RID: 4440
		// (get) Token: 0x060046AC RID: 18092 RVA: 0x00111CF4 File Offset: 0x0010FEF4
		// (set) Token: 0x060046AD RID: 18093 RVA: 0x00111CFC File Offset: 0x0010FEFC
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ContentAlignment TextAlign
		{
			get
			{
				return base.TextAlign;
			}
			set
			{
				base.TextAlign = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripTextDirection" /> values.</returns>
		// Token: 0x17001159 RID: 4441
		// (get) Token: 0x060046AE RID: 18094 RVA: 0x00111D05 File Offset: 0x0010FF05
		// (set) Token: 0x060046AF RID: 18095 RVA: 0x00111D0D File Offset: 0x0010FF0D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(ToolStripTextDirection.Horizontal)]
		public override ToolStripTextDirection TextDirection
		{
			get
			{
				return base.TextDirection;
			}
			set
			{
				base.TextDirection = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.TextImageRelation" /> values.</returns>
		// Token: 0x1700115A RID: 4442
		// (get) Token: 0x060046B0 RID: 18096 RVA: 0x00111D16 File Offset: 0x0010FF16
		// (set) Token: 0x060046B1 RID: 18097 RVA: 0x00111D1E File Offset: 0x0010FF1E
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new TextImageRelation TextImageRelation
		{
			get
			{
				return base.TextImageRelation;
			}
			set
			{
				base.TextImageRelation = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A string representing the ToolTip text.</returns>
		// Token: 0x1700115B RID: 4443
		// (get) Token: 0x060046B2 RID: 18098 RVA: 0x0011BD95 File Offset: 0x00119F95
		// (set) Token: 0x060046B3 RID: 18099 RVA: 0x00128A77 File Offset: 0x00126C77
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new string ToolTipText
		{
			get
			{
				return base.ToolTipText;
			}
			set
			{
				base.ToolTipText = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>
		///   <see langword="true" /> if enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700115C RID: 4444
		// (get) Token: 0x060046B4 RID: 18100 RVA: 0x00111BD8 File Offset: 0x0010FDD8
		// (set) Token: 0x060046B5 RID: 18101 RVA: 0x00111BE0 File Offset: 0x0010FDE0
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool RightToLeftAutoMirrorImage
		{
			get
			{
				return base.RightToLeftAutoMirrorImage;
			}
			set
			{
				base.RightToLeftAutoMirrorImage = value;
			}
		}

		/// <summary>Creates a new accessibility object for the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</returns>
		// Token: 0x060046B6 RID: 18102 RVA: 0x00128A80 File Offset: 0x00126C80
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new ToolStripSeparator.ToolStripSeparatorAccessibleObject(this);
		}

		/// <summary>Retrieves the size of a rectangular area into which a <see cref="T:System.Windows.Forms.ToolStripSeparator" /> can be fitted.</summary>
		/// <param name="constrainingSize">A <see cref="T:System.Drawing.Size" /> representing the height and width of the <see cref="T:System.Windows.Forms.ToolStripSeparator" />, in pixels.</param>
		/// <returns>A <see cref="T:System.Drawing.Size" /> representing the height and width of the <see cref="T:System.Windows.Forms.ToolStripSeparator" />, in pixels.</returns>
		// Token: 0x060046B7 RID: 18103 RVA: 0x00128A88 File Offset: 0x00126C88
		public override Size GetPreferredSize(Size constrainingSize)
		{
			ToolStrip toolStrip = base.ParentInternal;
			if (toolStrip == null)
			{
				toolStrip = base.Owner;
			}
			if (toolStrip == null)
			{
				return new Size(6, 6);
			}
			ToolStripDropDownMenu toolStripDropDownMenu = toolStrip as ToolStripDropDownMenu;
			if (toolStripDropDownMenu != null)
			{
				return new Size(toolStrip.Width - (toolStrip.Padding.Horizontal - toolStripDropDownMenu.ImageMargin.Width), 6);
			}
			if (toolStrip.LayoutStyle != ToolStripLayoutStyle.HorizontalStackWithOverflow || toolStrip.LayoutStyle != ToolStripLayoutStyle.VerticalStackWithOverflow)
			{
				constrainingSize.Width = 23;
				constrainingSize.Height = 23;
			}
			if (this.IsVertical)
			{
				return new Size(6, constrainingSize.Height);
			}
			return new Size(constrainingSize.Width, 6);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060046B8 RID: 18104 RVA: 0x00128B2D File Offset: 0x00126D2D
		protected override void OnPaint(PaintEventArgs e)
		{
			if (base.Owner != null && base.ParentInternal != null)
			{
				base.Renderer.DrawSeparator(new ToolStripSeparatorRenderEventArgs(e.Graphics, this, this.IsVertical));
			}
		}

		/// <summary>This method is not relevant to this class.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060046B9 RID: 18105 RVA: 0x00128B5C File Offset: 0x00126D5C
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void OnFontChanged(EventArgs e)
		{
			base.RaiseEvent(ToolStripItem.EventFontChanged, e);
		}

		// Token: 0x060046BA RID: 18106 RVA: 0x00128B6A File Offset: 0x00126D6A
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal override bool ShouldSerializeForeColor()
		{
			return this.ForeColor != SystemColors.ControlDark;
		}

		/// <summary>Sets the size and location of the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> specifying the size and location of the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</param>
		// Token: 0x060046BB RID: 18107 RVA: 0x00128B7C File Offset: 0x00126D7C
		protected internal override void SetBounds(Rectangle rect)
		{
			ToolStripDropDownMenu toolStripDropDownMenu = base.Owner as ToolStripDropDownMenu;
			if (toolStripDropDownMenu != null && toolStripDropDownMenu != null)
			{
				rect.X = 2;
				rect.Width = toolStripDropDownMenu.Width - 4;
			}
			base.SetBounds(rect);
		}

		// Token: 0x040026AC RID: 9900
		private const int WINBAR_SEPARATORTHICKNESS = 6;

		// Token: 0x040026AD RID: 9901
		private const int WINBAR_SEPARATORHEIGHT = 23;

		// Token: 0x02000818 RID: 2072
		[ComVisible(true)]
		internal class ToolStripSeparatorAccessibleObject : ToolStripItem.ToolStripItemAccessibleObject
		{
			// Token: 0x06006F98 RID: 28568 RVA: 0x001996E7 File Offset: 0x001978E7
			public ToolStripSeparatorAccessibleObject(ToolStripSeparator ownerItem)
				: base(ownerItem)
			{
				this.ownerItem = ownerItem;
			}

			// Token: 0x17001869 RID: 6249
			// (get) Token: 0x06006F99 RID: 28569 RVA: 0x001996F8 File Offset: 0x001978F8
			public override AccessibleRole Role
			{
				get
				{
					AccessibleRole accessibleRole = this.ownerItem.AccessibleRole;
					if (accessibleRole != AccessibleRole.Default)
					{
						return accessibleRole;
					}
					return AccessibleRole.Separator;
				}
			}

			// Token: 0x06006F9A RID: 28570 RVA: 0x00199719 File Offset: 0x00197919
			internal override object GetPropertyValue(int propertyID)
			{
				if (AccessibilityImprovements.Level3 && propertyID == 30003)
				{
					return 50038;
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x04004322 RID: 17186
			private ToolStripSeparator ownerItem;
		}
	}
}
