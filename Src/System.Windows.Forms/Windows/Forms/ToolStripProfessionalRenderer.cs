using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Handles the painting functionality for <see cref="T:System.Windows.Forms.ToolStrip" /> objects, applying a custom palette and a streamlined style.</summary>
	// Token: 0x020003F4 RID: 1012
	public class ToolStripProfessionalRenderer : ToolStripRenderer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripProfessionalRenderer" /> class.</summary>
		// Token: 0x06004589 RID: 17801 RVA: 0x00123CB4 File Offset: 0x00121EB4
		public ToolStripProfessionalRenderer()
		{
		}

		// Token: 0x0600458A RID: 17802 RVA: 0x00123D0C File Offset: 0x00121F0C
		internal ToolStripProfessionalRenderer(bool isDefault)
			: base(isDefault)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripProfessionalRenderer" /> class.</summary>
		/// <param name="professionalColorTable">A <see cref="T:System.Windows.Forms.ProfessionalColorTable" /> to be used for painting.</param>
		// Token: 0x0600458B RID: 17803 RVA: 0x00123D64 File Offset: 0x00121F64
		public ToolStripProfessionalRenderer(ProfessionalColorTable professionalColorTable)
		{
			this.professionalColorTable = professionalColorTable;
		}

		/// <summary>Gets the color palette used for painting.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ProfessionalColorTable" /> used for painting.</returns>
		// Token: 0x17001121 RID: 4385
		// (get) Token: 0x0600458C RID: 17804 RVA: 0x00123DC1 File Offset: 0x00121FC1
		public ProfessionalColorTable ColorTable
		{
			get
			{
				if (this.professionalColorTable == null)
				{
					return ProfessionalColors.ColorTable;
				}
				return this.professionalColorTable;
			}
		}

		// Token: 0x17001122 RID: 4386
		// (get) Token: 0x0600458D RID: 17805 RVA: 0x00123DD7 File Offset: 0x00121FD7
		internal override ToolStripRenderer RendererOverride
		{
			get
			{
				if (DisplayInformation.HighContrast)
				{
					return this.HighContrastRenderer;
				}
				if (DisplayInformation.LowResolution)
				{
					return this.LowResolutionRenderer;
				}
				return null;
			}
		}

		// Token: 0x17001123 RID: 4387
		// (get) Token: 0x0600458E RID: 17806 RVA: 0x00123DF6 File Offset: 0x00121FF6
		internal ToolStripRenderer HighContrastRenderer
		{
			get
			{
				if (this.toolStripHighContrastRenderer == null)
				{
					this.toolStripHighContrastRenderer = new ToolStripHighContrastRenderer(false);
				}
				return this.toolStripHighContrastRenderer;
			}
		}

		// Token: 0x17001124 RID: 4388
		// (get) Token: 0x0600458F RID: 17807 RVA: 0x00123E12 File Offset: 0x00122012
		internal ToolStripRenderer LowResolutionRenderer
		{
			get
			{
				if (this.toolStripLowResolutionRenderer == null)
				{
					this.toolStripLowResolutionRenderer = new ToolStripProfessionalLowResolutionRenderer();
				}
				return this.toolStripLowResolutionRenderer;
			}
		}

		/// <summary>Gets or sets a value indicating whether edges of controls have a rounded rather than a square or sharp appearance.</summary>
		/// <returns>
		///   <see langword="true" /> to round off control edges; otherwise, <see langword="false" />.</returns>
		// Token: 0x17001125 RID: 4389
		// (get) Token: 0x06004590 RID: 17808 RVA: 0x00123E2D File Offset: 0x0012202D
		// (set) Token: 0x06004591 RID: 17809 RVA: 0x00123E35 File Offset: 0x00122035
		public bool RoundedEdges
		{
			get
			{
				return this.roundedEdges;
			}
			set
			{
				this.roundedEdges = value;
			}
		}

		// Token: 0x17001126 RID: 4390
		// (get) Token: 0x06004592 RID: 17810 RVA: 0x00123E3E File Offset: 0x0012203E
		private bool UseSystemColors
		{
			get
			{
				return this.ColorTable.UseSystemColors || !ToolStripManager.VisualStylesEnabled;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderToolStripBackground" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06004593 RID: 17811 RVA: 0x00123E58 File Offset: 0x00122058
		protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
		{
			if (this.RendererOverride != null)
			{
				base.OnRenderToolStripBackground(e);
				return;
			}
			ToolStrip toolStrip = e.ToolStrip;
			if (!base.ShouldPaintBackground(toolStrip))
			{
				return;
			}
			if (toolStrip is ToolStripDropDown)
			{
				this.RenderToolStripDropDownBackground(e);
				return;
			}
			if (toolStrip is MenuStrip)
			{
				this.RenderMenuStripBackground(e);
				return;
			}
			if (toolStrip is StatusStrip)
			{
				this.RenderStatusStripBackground(e);
				return;
			}
			this.RenderToolStripBackgroundInternal(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderOverflowButtonBackground" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06004594 RID: 17812 RVA: 0x00123EC0 File Offset: 0x001220C0
		protected override void OnRenderOverflowButtonBackground(ToolStripItemRenderEventArgs e)
		{
			this.ScaleObjectSizesIfNeeded(e.ToolStrip.DeviceDpi);
			if (this.RendererOverride != null)
			{
				base.OnRenderOverflowButtonBackground(e);
				return;
			}
			ToolStripItem item = e.Item;
			Graphics graphics = e.Graphics;
			bool flag = item.RightToLeft == RightToLeft.Yes;
			this.RenderOverflowBackground(e, flag);
			bool flag2 = e.ToolStrip.Orientation == Orientation.Horizontal;
			Rectangle empty = Rectangle.Empty;
			if (flag)
			{
				empty = new Rectangle(0, item.Height - this.overflowArrowOffsetY, this.overflowArrowWidth, this.overflowArrowHeight);
			}
			else
			{
				empty = new Rectangle(item.Width - this.overflowButtonWidth, item.Height - this.overflowArrowOffsetY, this.overflowArrowWidth, this.overflowArrowHeight);
			}
			ArrowDirection arrowDirection = (flag2 ? ArrowDirection.Down : ArrowDirection.Right);
			int num = ((flag && flag2) ? (-1) : 1);
			empty.Offset(num, 1);
			this.RenderArrowInternal(graphics, empty, arrowDirection, SystemBrushes.ButtonHighlight);
			empty.Offset(-1 * num, -1);
			Point point = this.RenderArrowInternal(graphics, empty, arrowDirection, SystemBrushes.ControlText);
			if (flag2)
			{
				num = (flag ? (-2) : 0);
				graphics.DrawLine(SystemPens.ControlText, point.X - ToolStripRenderer.Offset2X, empty.Y - ToolStripRenderer.Offset2Y, point.X + ToolStripRenderer.Offset2X, empty.Y - ToolStripRenderer.Offset2Y);
				graphics.DrawLine(SystemPens.ButtonHighlight, point.X - ToolStripRenderer.Offset2X + 1 + num, empty.Y - ToolStripRenderer.Offset2Y + 1, point.X + ToolStripRenderer.Offset2X + 1 + num, empty.Y - ToolStripRenderer.Offset2Y + 1);
				return;
			}
			graphics.DrawLine(SystemPens.ControlText, empty.X, point.Y - ToolStripRenderer.Offset2Y, empty.X, point.Y + ToolStripRenderer.Offset2Y);
			graphics.DrawLine(SystemPens.ButtonHighlight, empty.X + 1, point.Y - ToolStripRenderer.Offset2Y + 1, empty.X + 1, point.Y + ToolStripRenderer.Offset2Y + 1);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderDropDownButtonBackground" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06004595 RID: 17813 RVA: 0x001240D4 File Offset: 0x001222D4
		protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
		{
			if (this.RendererOverride != null)
			{
				base.OnRenderDropDownButtonBackground(e);
				return;
			}
			ToolStripDropDownItem toolStripDropDownItem = e.Item as ToolStripDropDownItem;
			if (toolStripDropDownItem != null && toolStripDropDownItem.Pressed && toolStripDropDownItem.HasDropDownItems)
			{
				Rectangle rectangle = new Rectangle(Point.Empty, toolStripDropDownItem.Size);
				this.RenderPressedGradient(e.Graphics, rectangle);
				return;
			}
			this.RenderItemInternal(e, true);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderSeparator" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripSeparatorRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06004596 RID: 17814 RVA: 0x00124138 File Offset: 0x00122338
		protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
		{
			if (this.RendererOverride != null)
			{
				base.OnRenderSeparator(e);
				return;
			}
			this.RenderSeparatorInternal(e.Graphics, e.Item, new Rectangle(Point.Empty, e.Item.Size), e.Vertical);
		}

		/// <summary>Raises the OnRenderSplitButtonBackground event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06004597 RID: 17815 RVA: 0x00124178 File Offset: 0x00122378
		protected override void OnRenderSplitButtonBackground(ToolStripItemRenderEventArgs e)
		{
			if (this.RendererOverride != null)
			{
				base.OnRenderSplitButtonBackground(e);
				return;
			}
			ToolStripSplitButton toolStripSplitButton = e.Item as ToolStripSplitButton;
			Graphics graphics = e.Graphics;
			if (toolStripSplitButton != null)
			{
				Rectangle rectangle = new Rectangle(Point.Empty, toolStripSplitButton.Size);
				if (toolStripSplitButton.BackgroundImage != null)
				{
					Rectangle rectangle2 = (toolStripSplitButton.Selected ? toolStripSplitButton.ContentRectangle : rectangle);
					ControlPaint.DrawBackgroundImage(graphics, toolStripSplitButton.BackgroundImage, toolStripSplitButton.BackColor, toolStripSplitButton.BackgroundImageLayout, rectangle, rectangle2);
				}
				bool flag = toolStripSplitButton.Pressed || toolStripSplitButton.ButtonPressed || toolStripSplitButton.Selected || toolStripSplitButton.ButtonSelected;
				if (flag)
				{
					this.RenderItemInternal(e, true);
				}
				if (toolStripSplitButton.ButtonPressed)
				{
					Rectangle rectangle3 = toolStripSplitButton.ButtonBounds;
					Padding padding = ((toolStripSplitButton.RightToLeft == RightToLeft.Yes) ? new Padding(0, 1, 1, 1) : new Padding(1, 1, 0, 1));
					rectangle3 = LayoutUtils.DeflateRect(rectangle3, padding);
					this.RenderPressedButtonFill(graphics, rectangle3);
				}
				else if (toolStripSplitButton.Pressed)
				{
					this.RenderPressedGradient(e.Graphics, rectangle);
				}
				Rectangle dropDownButtonBounds = toolStripSplitButton.DropDownButtonBounds;
				if (flag && !toolStripSplitButton.Pressed)
				{
					using (Brush brush = new SolidBrush(this.ColorTable.ButtonSelectedBorder))
					{
						graphics.FillRectangle(brush, toolStripSplitButton.SplitterBounds);
					}
				}
				base.DrawArrow(new ToolStripArrowRenderEventArgs(graphics, toolStripSplitButton, dropDownButtonBounds, SystemColors.ControlText, ArrowDirection.Down));
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderToolStripStatusLabelBackground" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06004598 RID: 17816 RVA: 0x001242E4 File Offset: 0x001224E4
		protected override void OnRenderToolStripStatusLabelBackground(ToolStripItemRenderEventArgs e)
		{
			if (this.RendererOverride != null)
			{
				base.OnRenderToolStripStatusLabelBackground(e);
				return;
			}
			ToolStripProfessionalRenderer.RenderLabelInternal(e);
			ToolStripStatusLabel toolStripStatusLabel = e.Item as ToolStripStatusLabel;
			ControlPaint.DrawBorder3D(e.Graphics, new Rectangle(0, 0, toolStripStatusLabel.Width, toolStripStatusLabel.Height), toolStripStatusLabel.BorderStyle, (Border3DSide)toolStripStatusLabel.BorderSides);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderLabelBackground" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06004599 RID: 17817 RVA: 0x0012433D File Offset: 0x0012253D
		protected override void OnRenderLabelBackground(ToolStripItemRenderEventArgs e)
		{
			if (this.RendererOverride != null)
			{
				base.OnRenderLabelBackground(e);
				return;
			}
			ToolStripProfessionalRenderer.RenderLabelInternal(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderButtonBackground" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x0600459A RID: 17818 RVA: 0x00124358 File Offset: 0x00122558
		protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
		{
			if (this.RendererOverride != null)
			{
				base.OnRenderButtonBackground(e);
				return;
			}
			ToolStripButton toolStripButton = e.Item as ToolStripButton;
			Graphics graphics = e.Graphics;
			Rectangle rectangle = new Rectangle(Point.Empty, toolStripButton.Size);
			if (toolStripButton.CheckState == CheckState.Unchecked)
			{
				this.RenderItemInternal(e, true);
				return;
			}
			Rectangle rectangle2 = (toolStripButton.Selected ? toolStripButton.ContentRectangle : rectangle);
			if (toolStripButton.BackgroundImage != null)
			{
				ControlPaint.DrawBackgroundImage(graphics, toolStripButton.BackgroundImage, toolStripButton.BackColor, toolStripButton.BackgroundImageLayout, rectangle, rectangle2);
			}
			if (this.UseSystemColors)
			{
				if (toolStripButton.Selected)
				{
					this.RenderPressedButtonFill(graphics, rectangle);
				}
				else
				{
					this.RenderCheckedButtonFill(graphics, rectangle);
				}
				using (Pen pen = new Pen(this.ColorTable.ButtonSelectedBorder))
				{
					graphics.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
					return;
				}
			}
			if (toolStripButton.Selected)
			{
				this.RenderPressedButtonFill(graphics, rectangle);
			}
			else
			{
				this.RenderCheckedButtonFill(graphics, rectangle);
			}
			using (Pen pen2 = new Pen(this.ColorTable.ButtonSelectedBorder))
			{
				graphics.DrawRectangle(pen2, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderToolStripBorder" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x0600459B RID: 17819 RVA: 0x001244C8 File Offset: 0x001226C8
		protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
		{
			if (this.RendererOverride != null)
			{
				base.OnRenderToolStripBorder(e);
				return;
			}
			ToolStrip toolStrip = e.ToolStrip;
			Graphics graphics = e.Graphics;
			if (toolStrip is ToolStripDropDown)
			{
				this.RenderToolStripDropDownBorder(e);
				return;
			}
			if (!(toolStrip is MenuStrip))
			{
				if (toolStrip is StatusStrip)
				{
					this.RenderStatusStripBorder(e);
					return;
				}
				Rectangle rectangle = new Rectangle(Point.Empty, toolStrip.Size);
				using (Pen pen = new Pen(this.ColorTable.ToolStripBorder))
				{
					if (toolStrip.Orientation == Orientation.Horizontal)
					{
						graphics.DrawLine(pen, rectangle.Left, rectangle.Height - 1, rectangle.Right, rectangle.Height - 1);
						if (this.RoundedEdges)
						{
							graphics.DrawLine(pen, rectangle.Width - 2, rectangle.Height - 2, rectangle.Width - 1, rectangle.Height - 3);
						}
					}
					else
					{
						graphics.DrawLine(pen, rectangle.Width - 1, 0, rectangle.Width - 1, rectangle.Height - 1);
						if (this.RoundedEdges)
						{
							graphics.DrawLine(pen, rectangle.Width - 2, rectangle.Height - 2, rectangle.Width - 1, rectangle.Height - 3);
						}
					}
				}
				if (this.RoundedEdges)
				{
					if (toolStrip.OverflowButton.Visible)
					{
						this.RenderOverflowButtonEffectsOverBorder(e);
						return;
					}
					Rectangle empty = Rectangle.Empty;
					if (toolStrip.Orientation == Orientation.Horizontal)
					{
						empty = new Rectangle(rectangle.Width - 1, 3, 1, rectangle.Height - 3);
					}
					else
					{
						empty = new Rectangle(3, rectangle.Height - 1, rectangle.Width - 3, rectangle.Height - 1);
					}
					this.ScaleObjectSizesIfNeeded(toolStrip.DeviceDpi);
					this.FillWithDoubleGradient(this.ColorTable.OverflowButtonGradientBegin, this.ColorTable.OverflowButtonGradientMiddle, this.ColorTable.OverflowButtonGradientEnd, e.Graphics, empty, this.iconWellGradientWidth, this.iconWellGradientWidth, LinearGradientMode.Vertical, false);
					this.RenderToolStripCurve(e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderGrip" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripGripRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x0600459C RID: 17820 RVA: 0x001246DC File Offset: 0x001228DC
		protected override void OnRenderGrip(ToolStripGripRenderEventArgs e)
		{
			if (this.RendererOverride != null)
			{
				base.OnRenderGrip(e);
				return;
			}
			this.ScaleObjectSizesIfNeeded(e.ToolStrip.DeviceDpi);
			Graphics graphics = e.Graphics;
			Rectangle gripBounds = e.GripBounds;
			ToolStrip toolStrip = e.ToolStrip;
			bool flag = e.ToolStrip.RightToLeft == RightToLeft.Yes;
			int num = ((toolStrip.Orientation == Orientation.Horizontal) ? gripBounds.Height : gripBounds.Width);
			int num2 = ((toolStrip.Orientation == Orientation.Horizontal) ? gripBounds.Width : gripBounds.Height);
			int num3 = (num - this.gripPadding * 2) / 4;
			if (num3 > 0)
			{
				int num4 = ((toolStrip is MenuStrip) ? 2 : 0);
				Rectangle[] array = new Rectangle[num3];
				int num5 = this.gripPadding + 1 + num4;
				int num6 = num2 / 2;
				for (int i = 0; i < num3; i++)
				{
					array[i] = ((toolStrip.Orientation == Orientation.Horizontal) ? new Rectangle(num6, num5, 2, 2) : new Rectangle(num5, num6, 2, 2));
					num5 += 4;
				}
				int num7 = (flag ? 1 : (-1));
				if (flag)
				{
					for (int j = 0; j < num3; j++)
					{
						array[j].Offset(-num7, 0);
					}
				}
				using (Brush brush = new SolidBrush(this.ColorTable.GripLight))
				{
					graphics.FillRectangles(brush, array);
				}
				for (int k = 0; k < num3; k++)
				{
					array[k].Offset(num7, -1);
				}
				using (Brush brush2 = new SolidBrush(this.ColorTable.GripDark))
				{
					graphics.FillRectangles(brush2, array);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderMenuItemBackground" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x0600459D RID: 17821 RVA: 0x001248A4 File Offset: 0x00122AA4
		protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
		{
			if (this.RendererOverride != null)
			{
				base.OnRenderMenuItemBackground(e);
				return;
			}
			ToolStripItem item = e.Item;
			Graphics graphics = e.Graphics;
			Rectangle rectangle = new Rectangle(Point.Empty, item.Size);
			if (rectangle.Width == 0 || rectangle.Height == 0)
			{
				return;
			}
			if (item is MdiControlStrip.SystemMenuItem)
			{
				return;
			}
			if (item.IsOnDropDown)
			{
				this.ScaleObjectSizesIfNeeded(item.DeviceDpi);
				rectangle = LayoutUtils.DeflateRect(rectangle, this.scaledDropDownMenuItemPaintPadding);
				if (item.Selected)
				{
					Color color = this.ColorTable.MenuItemBorder;
					if (item.Enabled)
					{
						if (this.UseSystemColors)
						{
							color = SystemColors.Highlight;
							this.RenderSelectedButtonFill(graphics, rectangle);
						}
						else
						{
							using (Brush brush = new SolidBrush(this.ColorTable.MenuItemSelected))
							{
								graphics.FillRectangle(brush, rectangle);
							}
						}
					}
					using (Pen pen = new Pen(color))
					{
						graphics.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
						return;
					}
				}
				Rectangle rectangle2 = rectangle;
				if (item.BackgroundImage != null)
				{
					ControlPaint.DrawBackgroundImage(graphics, item.BackgroundImage, item.BackColor, item.BackgroundImageLayout, rectangle, rectangle2);
					return;
				}
				if (item.Owner == null || !(item.BackColor != item.Owner.BackColor))
				{
					return;
				}
				using (Brush brush2 = new SolidBrush(item.BackColor))
				{
					graphics.FillRectangle(brush2, rectangle2);
					return;
				}
			}
			if (item.Pressed)
			{
				this.RenderPressedGradient(graphics, rectangle);
				return;
			}
			if (item.Selected)
			{
				Color color2 = this.ColorTable.MenuItemBorder;
				if (item.Enabled)
				{
					if (this.UseSystemColors)
					{
						color2 = SystemColors.Highlight;
						this.RenderSelectedButtonFill(graphics, rectangle);
					}
					else
					{
						using (Brush brush3 = new LinearGradientBrush(rectangle, this.ColorTable.MenuItemSelectedGradientBegin, this.ColorTable.MenuItemSelectedGradientEnd, LinearGradientMode.Vertical))
						{
							graphics.FillRectangle(brush3, rectangle);
						}
					}
				}
				using (Pen pen2 = new Pen(color2))
				{
					graphics.DrawRectangle(pen2, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
					return;
				}
			}
			Rectangle rectangle3 = rectangle;
			if (item.BackgroundImage != null)
			{
				ControlPaint.DrawBackgroundImage(graphics, item.BackgroundImage, item.BackColor, item.BackgroundImageLayout, rectangle, rectangle3);
				return;
			}
			if (item.Owner != null && item.BackColor != item.Owner.BackColor)
			{
				using (Brush brush4 = new SolidBrush(item.BackColor))
				{
					graphics.FillRectangle(brush4, rectangle3);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderArrow" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripArrowRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x0600459E RID: 17822 RVA: 0x00124BB0 File Offset: 0x00122DB0
		protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
		{
			if (this.RendererOverride != null)
			{
				base.OnRenderArrow(e);
				return;
			}
			ToolStripItem item = e.Item;
			if (item is ToolStripDropDownItem)
			{
				e.DefaultArrowColor = (item.Enabled ? SystemColors.ControlText : SystemColors.ControlDark);
			}
			base.OnRenderArrow(e);
		}

		/// <summary>Draws the item background.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x0600459F RID: 17823 RVA: 0x00124C00 File Offset: 0x00122E00
		protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
		{
			if (this.RendererOverride != null)
			{
				base.OnRenderImageMargin(e);
				return;
			}
			this.ScaleObjectSizesIfNeeded(e.ToolStrip.DeviceDpi);
			Graphics graphics = e.Graphics;
			Rectangle affectedBounds = e.AffectedBounds;
			affectedBounds.Y += 2;
			affectedBounds.Height -= 4;
			RightToLeft rightToLeft = e.ToolStrip.RightToLeft;
			Color color = ((rightToLeft == RightToLeft.No) ? this.ColorTable.ImageMarginGradientBegin : this.ColorTable.ImageMarginGradientEnd);
			Color color2 = ((rightToLeft == RightToLeft.No) ? this.ColorTable.ImageMarginGradientEnd : this.ColorTable.ImageMarginGradientBegin);
			this.FillWithDoubleGradient(color, this.ColorTable.ImageMarginGradientMiddle, color2, e.Graphics, affectedBounds, this.iconWellGradientWidth, this.iconWellGradientWidth, LinearGradientMode.Horizontal, e.ToolStrip.RightToLeft == RightToLeft.Yes);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderItemText" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemTextRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x060045A0 RID: 17824 RVA: 0x00124CD8 File Offset: 0x00122ED8
		protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
		{
			if (this.RendererOverride != null)
			{
				base.OnRenderItemText(e);
				return;
			}
			if (e.Item is ToolStripMenuItem && (e.Item.Selected || e.Item.Pressed))
			{
				e.DefaultTextColor = e.Item.ForeColor;
			}
			base.OnRenderItemText(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderItemCheck" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemImageRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x060045A1 RID: 17825 RVA: 0x00124D34 File Offset: 0x00122F34
		protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
		{
			if (this.RendererOverride != null)
			{
				base.OnRenderItemCheck(e);
				return;
			}
			this.RenderCheckBackground(e);
			base.OnRenderItemCheck(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderItemImage" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemImageRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x060045A2 RID: 17826 RVA: 0x00124D54 File Offset: 0x00122F54
		protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
		{
			if (this.RendererOverride != null)
			{
				base.OnRenderItemImage(e);
				return;
			}
			Rectangle imageRectangle = e.ImageRectangle;
			Image image = e.Image;
			if (e.Item is ToolStripMenuItem)
			{
				ToolStripMenuItem toolStripMenuItem = e.Item as ToolStripMenuItem;
				if (toolStripMenuItem.CheckState != CheckState.Unchecked)
				{
					ToolStripDropDownMenu toolStripDropDownMenu = toolStripMenuItem.ParentInternal as ToolStripDropDownMenu;
					if (toolStripDropDownMenu != null && !toolStripDropDownMenu.ShowCheckMargin && toolStripDropDownMenu.ShowImageMargin)
					{
						this.RenderCheckBackground(e);
					}
				}
			}
			if (imageRectangle != Rectangle.Empty && image != null)
			{
				if (!e.Item.Enabled)
				{
					base.OnRenderItemImage(e);
					return;
				}
				if (e.Item.ImageScaling == ToolStripItemImageScaling.None)
				{
					e.Graphics.DrawImage(image, imageRectangle, new Rectangle(Point.Empty, imageRectangle.Size), GraphicsUnit.Pixel);
					return;
				}
				e.Graphics.DrawImage(image, imageRectangle);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderToolStripPanelBackground" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripPanelRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x060045A3 RID: 17827 RVA: 0x00124E28 File Offset: 0x00123028
		protected override void OnRenderToolStripPanelBackground(ToolStripPanelRenderEventArgs e)
		{
			if (this.RendererOverride != null)
			{
				base.OnRenderToolStripPanelBackground(e);
				return;
			}
			ToolStripPanel toolStripPanel = e.ToolStripPanel;
			if (!base.ShouldPaintBackground(toolStripPanel))
			{
				return;
			}
			e.Handled = true;
			this.RenderBackgroundGradient(e.Graphics, toolStripPanel, this.ColorTable.ToolStripPanelGradientBegin, this.ColorTable.ToolStripPanelGradientEnd);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderToolStripContentPanelBackground" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripContentPanelRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x060045A4 RID: 17828 RVA: 0x00124E80 File Offset: 0x00123080
		protected override void OnRenderToolStripContentPanelBackground(ToolStripContentPanelRenderEventArgs e)
		{
			if (this.RendererOverride != null)
			{
				base.OnRenderToolStripContentPanelBackground(e);
				return;
			}
			ToolStripContentPanel toolStripContentPanel = e.ToolStripContentPanel;
			if (!base.ShouldPaintBackground(toolStripContentPanel))
			{
				return;
			}
			if (SystemInformation.InLockedTerminalSession())
			{
				return;
			}
			e.Handled = true;
			e.Graphics.Clear(this.ColorTable.ToolStripContentPanelGradientEnd);
		}

		// Token: 0x060045A5 RID: 17829 RVA: 0x00124ED4 File Offset: 0x001230D4
		internal override Region GetTransparentRegion(ToolStrip toolStrip)
		{
			if (toolStrip is ToolStripDropDown || toolStrip is MenuStrip || toolStrip is StatusStrip)
			{
				return null;
			}
			if (!this.RoundedEdges)
			{
				return null;
			}
			Rectangle rectangle = new Rectangle(Point.Empty, toolStrip.Size);
			if (toolStrip.ParentInternal != null)
			{
				Point empty = Point.Empty;
				Point point = new Point(rectangle.Width - 1, 0);
				Point point2 = new Point(0, rectangle.Height - 1);
				Point point3 = new Point(rectangle.Width - 1, rectangle.Height - 1);
				Rectangle rectangle2 = new Rectangle(empty, ToolStripProfessionalRenderer.onePix);
				Rectangle rectangle3 = new Rectangle(point2, new Size(2, 1));
				Rectangle rectangle4 = new Rectangle(point2.X, point2.Y - 1, 1, 2);
				Rectangle rectangle5 = new Rectangle(point3.X - 1, point3.Y, 2, 1);
				Rectangle rectangle6 = new Rectangle(point3.X, point3.Y - 1, 1, 2);
				Rectangle rectangle7;
				Rectangle rectangle8;
				if (toolStrip.OverflowButton.Visible)
				{
					rectangle7 = new Rectangle(point.X - 1, point.Y, 1, 1);
					rectangle8 = new Rectangle(point.X, point.Y, 1, 2);
				}
				else
				{
					rectangle7 = new Rectangle(point.X - 2, point.Y, 2, 1);
					rectangle8 = new Rectangle(point.X, point.Y, 1, 3);
				}
				Region region = new Region(rectangle2);
				region.Union(rectangle2);
				region.Union(rectangle3);
				region.Union(rectangle4);
				region.Union(rectangle5);
				region.Union(rectangle6);
				region.Union(rectangle7);
				region.Union(rectangle8);
				return region;
			}
			return null;
		}

		// Token: 0x060045A6 RID: 17830 RVA: 0x00125088 File Offset: 0x00123288
		private void RenderOverflowButtonEffectsOverBorder(ToolStripRenderEventArgs e)
		{
			ToolStrip toolStrip = e.ToolStrip;
			ToolStripItem overflowButton = toolStrip.OverflowButton;
			if (!overflowButton.Visible)
			{
				return;
			}
			Graphics graphics = e.Graphics;
			Color color;
			Color color2;
			if (overflowButton.Pressed)
			{
				color = this.ColorTable.ButtonPressedGradientBegin;
				color2 = color;
			}
			else if (overflowButton.Selected)
			{
				color = this.ColorTable.ButtonSelectedGradientMiddle;
				color2 = color;
			}
			else
			{
				color = this.ColorTable.ToolStripBorder;
				color2 = this.ColorTable.ToolStripGradientMiddle;
			}
			using (Brush brush = new SolidBrush(color))
			{
				graphics.FillRectangle(brush, toolStrip.Width - 1, toolStrip.Height - 2, 1, 1);
				graphics.FillRectangle(brush, toolStrip.Width - 2, toolStrip.Height - 1, 1, 1);
			}
			using (Brush brush2 = new SolidBrush(color2))
			{
				graphics.FillRectangle(brush2, toolStrip.Width - 2, 0, 1, 1);
				graphics.FillRectangle(brush2, toolStrip.Width - 1, 1, 1, 1);
			}
		}

		// Token: 0x060045A7 RID: 17831 RVA: 0x001251A4 File Offset: 0x001233A4
		private void FillWithDoubleGradient(Color beginColor, Color middleColor, Color endColor, Graphics g, Rectangle bounds, int firstGradientWidth, int secondGradientWidth, LinearGradientMode mode, bool flipHorizontal)
		{
			if (bounds.Width == 0 || bounds.Height == 0)
			{
				return;
			}
			Rectangle rectangle = bounds;
			Rectangle rectangle2 = bounds;
			bool flag;
			if (mode == LinearGradientMode.Horizontal)
			{
				if (flipHorizontal)
				{
					Color color = endColor;
					endColor = beginColor;
					beginColor = color;
				}
				rectangle2.Width = firstGradientWidth;
				rectangle.Width = secondGradientWidth + 1;
				rectangle.X = bounds.Right - rectangle.Width;
				flag = bounds.Width > firstGradientWidth + secondGradientWidth;
			}
			else
			{
				rectangle2.Height = firstGradientWidth;
				rectangle.Height = secondGradientWidth + 1;
				rectangle.Y = bounds.Bottom - rectangle.Height;
				flag = bounds.Height > firstGradientWidth + secondGradientWidth;
			}
			if (flag)
			{
				using (Brush brush = new SolidBrush(middleColor))
				{
					g.FillRectangle(brush, bounds);
				}
				using (Brush brush2 = new LinearGradientBrush(rectangle2, beginColor, middleColor, mode))
				{
					g.FillRectangle(brush2, rectangle2);
				}
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rectangle, middleColor, endColor, mode))
				{
					if (mode == LinearGradientMode.Horizontal)
					{
						rectangle.X++;
						rectangle.Width--;
					}
					else
					{
						rectangle.Y++;
						rectangle.Height--;
					}
					g.FillRectangle(linearGradientBrush, rectangle);
					return;
				}
			}
			using (Brush brush3 = new LinearGradientBrush(bounds, beginColor, endColor, mode))
			{
				g.FillRectangle(brush3, bounds);
			}
		}

		// Token: 0x060045A8 RID: 17832 RVA: 0x0012535C File Offset: 0x0012355C
		private void RenderStatusStripBorder(ToolStripRenderEventArgs e)
		{
			e.Graphics.DrawLine(SystemPens.ButtonHighlight, 0, 0, e.ToolStrip.Width, 0);
		}

		// Token: 0x060045A9 RID: 17833 RVA: 0x0012537C File Offset: 0x0012357C
		private void RenderStatusStripBackground(ToolStripRenderEventArgs e)
		{
			StatusStrip statusStrip = e.ToolStrip as StatusStrip;
			this.RenderBackgroundGradient(e.Graphics, statusStrip, this.ColorTable.StatusStripGradientBegin, this.ColorTable.StatusStripGradientEnd, statusStrip.Orientation);
		}

		// Token: 0x060045AA RID: 17834 RVA: 0x001253C0 File Offset: 0x001235C0
		private void RenderCheckBackground(ToolStripItemImageRenderEventArgs e)
		{
			Rectangle rectangle = (DpiHelper.IsScalingRequired ? new Rectangle(e.ImageRectangle.Left - 2, (e.Item.Height - e.ImageRectangle.Height) / 2 - 1, e.ImageRectangle.Width + 4, e.ImageRectangle.Height + 2) : new Rectangle(e.ImageRectangle.Left - 2, 1, e.ImageRectangle.Width + 4, e.Item.Height - 2));
			Graphics graphics = e.Graphics;
			if (!this.UseSystemColors)
			{
				Color color = (e.Item.Selected ? this.ColorTable.CheckSelectedBackground : this.ColorTable.CheckBackground);
				color = (e.Item.Pressed ? this.ColorTable.CheckPressedBackground : color);
				using (Brush brush = new SolidBrush(color))
				{
					graphics.FillRectangle(brush, rectangle);
				}
				using (Pen pen = new Pen(this.ColorTable.ButtonSelectedBorder))
				{
					graphics.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
					return;
				}
			}
			if (e.Item.Pressed)
			{
				this.RenderPressedButtonFill(graphics, rectangle);
			}
			else
			{
				this.RenderSelectedButtonFill(graphics, rectangle);
			}
			graphics.DrawRectangle(SystemPens.Highlight, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
		}

		// Token: 0x060045AB RID: 17835 RVA: 0x00125580 File Offset: 0x00123780
		private void RenderPressedGradient(Graphics g, Rectangle bounds)
		{
			if (bounds.Width == 0 || bounds.Height == 0)
			{
				return;
			}
			using (Brush brush = new LinearGradientBrush(bounds, this.ColorTable.MenuItemPressedGradientBegin, this.ColorTable.MenuItemPressedGradientEnd, LinearGradientMode.Vertical))
			{
				g.FillRectangle(brush, bounds);
			}
			using (Pen pen = new Pen(this.ColorTable.MenuBorder))
			{
				g.DrawRectangle(pen, bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
			}
		}

		// Token: 0x060045AC RID: 17836 RVA: 0x00125634 File Offset: 0x00123834
		private void RenderMenuStripBackground(ToolStripRenderEventArgs e)
		{
			this.RenderBackgroundGradient(e.Graphics, e.ToolStrip, this.ColorTable.MenuStripGradientBegin, this.ColorTable.MenuStripGradientEnd, e.ToolStrip.Orientation);
		}

		// Token: 0x060045AD RID: 17837 RVA: 0x0012566C File Offset: 0x0012386C
		private static void RenderLabelInternal(ToolStripItemRenderEventArgs e)
		{
			Graphics graphics = e.Graphics;
			ToolStripItem item = e.Item;
			Rectangle rectangle = new Rectangle(Point.Empty, item.Size);
			Rectangle rectangle2 = (item.Selected ? item.ContentRectangle : rectangle);
			if (item.BackgroundImage != null)
			{
				ControlPaint.DrawBackgroundImage(graphics, item.BackgroundImage, item.BackColor, item.BackgroundImageLayout, rectangle, rectangle2);
			}
		}

		// Token: 0x060045AE RID: 17838 RVA: 0x001256CD File Offset: 0x001238CD
		private void RenderBackgroundGradient(Graphics g, Control control, Color beginColor, Color endColor)
		{
			this.RenderBackgroundGradient(g, control, beginColor, endColor, Orientation.Horizontal);
		}

		// Token: 0x060045AF RID: 17839 RVA: 0x001256DC File Offset: 0x001238DC
		private void RenderBackgroundGradient(Graphics g, Control control, Color beginColor, Color endColor, Orientation orientation)
		{
			if (control.RightToLeft == RightToLeft.Yes)
			{
				Color color = beginColor;
				beginColor = endColor;
				endColor = color;
			}
			if (orientation == Orientation.Horizontal)
			{
				Control parentInternal = control.ParentInternal;
				if (parentInternal != null)
				{
					Rectangle rectangle = new Rectangle(Point.Empty, parentInternal.Size);
					if (LayoutUtils.IsZeroWidthOrHeight(rectangle))
					{
						return;
					}
					using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rectangle, beginColor, endColor, LinearGradientMode.Horizontal))
					{
						linearGradientBrush.TranslateTransform((float)(parentInternal.Width - control.Location.X), (float)(parentInternal.Height - control.Location.Y));
						g.FillRectangle(linearGradientBrush, new Rectangle(Point.Empty, control.Size));
						return;
					}
				}
				Rectangle rectangle2 = new Rectangle(Point.Empty, control.Size);
				if (LayoutUtils.IsZeroWidthOrHeight(rectangle2))
				{
					return;
				}
				using (LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(rectangle2, beginColor, endColor, LinearGradientMode.Horizontal))
				{
					g.FillRectangle(linearGradientBrush2, rectangle2);
					return;
				}
			}
			using (Brush brush = new SolidBrush(beginColor))
			{
				g.FillRectangle(brush, new Rectangle(Point.Empty, control.Size));
			}
		}

		// Token: 0x060045B0 RID: 17840 RVA: 0x00125824 File Offset: 0x00123A24
		private void RenderToolStripBackgroundInternal(ToolStripRenderEventArgs e)
		{
			this.ScaleObjectSizesIfNeeded(e.ToolStrip.DeviceDpi);
			ToolStrip toolStrip = e.ToolStrip;
			Graphics graphics = e.Graphics;
			Rectangle rectangle = new Rectangle(Point.Empty, e.ToolStrip.Size);
			LinearGradientMode linearGradientMode = ((toolStrip.Orientation == Orientation.Horizontal) ? LinearGradientMode.Vertical : LinearGradientMode.Horizontal);
			this.FillWithDoubleGradient(this.ColorTable.ToolStripGradientBegin, this.ColorTable.ToolStripGradientMiddle, this.ColorTable.ToolStripGradientEnd, e.Graphics, rectangle, this.iconWellGradientWidth, this.iconWellGradientWidth, linearGradientMode, false);
		}

		// Token: 0x060045B1 RID: 17841 RVA: 0x001258B0 File Offset: 0x00123AB0
		private void RenderToolStripDropDownBackground(ToolStripRenderEventArgs e)
		{
			ToolStrip toolStrip = e.ToolStrip;
			Rectangle rectangle = new Rectangle(Point.Empty, e.ToolStrip.Size);
			using (Brush brush = new SolidBrush(this.ColorTable.ToolStripDropDownBackground))
			{
				e.Graphics.FillRectangle(brush, rectangle);
			}
		}

		// Token: 0x060045B2 RID: 17842 RVA: 0x00125918 File Offset: 0x00123B18
		private void RenderToolStripDropDownBorder(ToolStripRenderEventArgs e)
		{
			ToolStripDropDown toolStripDropDown = e.ToolStrip as ToolStripDropDown;
			Graphics graphics = e.Graphics;
			if (toolStripDropDown != null)
			{
				Rectangle rectangle = new Rectangle(Point.Empty, toolStripDropDown.Size);
				using (Pen pen = new Pen(this.ColorTable.MenuBorder))
				{
					graphics.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
				}
				if (!(toolStripDropDown is ToolStripOverflow))
				{
					using (Brush brush = new SolidBrush(this.ColorTable.ToolStripDropDownBackground))
					{
						graphics.FillRectangle(brush, e.ConnectedArea);
					}
				}
			}
		}

		// Token: 0x060045B3 RID: 17843 RVA: 0x001259E8 File Offset: 0x00123BE8
		private void RenderOverflowBackground(ToolStripItemRenderEventArgs e, bool rightToLeft)
		{
			this.ScaleObjectSizesIfNeeded(e.Item.DeviceDpi);
			Graphics graphics = e.Graphics;
			ToolStripOverflowButton toolStripOverflowButton = e.Item as ToolStripOverflowButton;
			Rectangle rectangle = new Rectangle(Point.Empty, e.Item.Size);
			Rectangle rectangle2 = rectangle;
			bool flag = this.RoundedEdges && !(toolStripOverflowButton.GetCurrentParent() is MenuStrip);
			bool flag2 = e.ToolStrip.Orientation == Orientation.Horizontal;
			if (flag2)
			{
				rectangle.X += rectangle.Width - this.overflowButtonWidth + 1;
				rectangle.Width = this.overflowButtonWidth;
				if (rightToLeft)
				{
					rectangle = LayoutUtils.RTLTranslate(rectangle, rectangle2);
				}
			}
			else
			{
				rectangle.Y = rectangle.Height - this.overflowButtonWidth + 1;
				rectangle.Height = this.overflowButtonWidth;
			}
			Color color;
			Color color2;
			Color color3;
			Color color4;
			Color color5;
			if (toolStripOverflowButton.Pressed)
			{
				color = this.ColorTable.ButtonPressedGradientBegin;
				color2 = this.ColorTable.ButtonPressedGradientMiddle;
				color3 = this.ColorTable.ButtonPressedGradientEnd;
				color4 = this.ColorTable.ButtonPressedGradientBegin;
				color5 = color4;
			}
			else if (toolStripOverflowButton.Selected)
			{
				color = this.ColorTable.ButtonSelectedGradientBegin;
				color2 = this.ColorTable.ButtonSelectedGradientMiddle;
				color3 = this.ColorTable.ButtonSelectedGradientEnd;
				color4 = this.ColorTable.ButtonSelectedGradientMiddle;
				color5 = color4;
			}
			else
			{
				color = this.ColorTable.OverflowButtonGradientBegin;
				color2 = this.ColorTable.OverflowButtonGradientMiddle;
				color3 = this.ColorTable.OverflowButtonGradientEnd;
				color4 = this.ColorTable.ToolStripBorder;
				color5 = (flag2 ? this.ColorTable.ToolStripGradientMiddle : this.ColorTable.ToolStripGradientEnd);
			}
			if (flag)
			{
				using (Pen pen = new Pen(color4))
				{
					Point point = new Point(rectangle.Left - 1, rectangle.Height - 2);
					Point point2 = new Point(rectangle.Left, rectangle.Height - 2);
					if (rightToLeft)
					{
						point.X = rectangle.Right + 1;
						point2.X = rectangle.Right;
					}
					graphics.DrawLine(pen, point, point2);
				}
			}
			LinearGradientMode linearGradientMode = (flag2 ? LinearGradientMode.Vertical : LinearGradientMode.Horizontal);
			this.FillWithDoubleGradient(color, color2, color3, graphics, rectangle, this.iconWellGradientWidth, this.iconWellGradientWidth, linearGradientMode, false);
			if (flag)
			{
				using (Brush brush = new SolidBrush(color5))
				{
					if (flag2)
					{
						Point point3 = new Point(rectangle.X - 2, 0);
						Point point4 = new Point(rectangle.X - 1, 1);
						if (rightToLeft)
						{
							point3.X = rectangle.Right + 1;
							point4.X = rectangle.Right;
						}
						graphics.FillRectangle(brush, point3.X, point3.Y, 1, 1);
						graphics.FillRectangle(brush, point4.X, point4.Y, 1, 1);
					}
					else
					{
						graphics.FillRectangle(brush, rectangle.Width - 3, rectangle.Top - 1, 1, 1);
						graphics.FillRectangle(brush, rectangle.Width - 2, rectangle.Top - 2, 1, 1);
					}
				}
				using (Brush brush2 = new SolidBrush(color))
				{
					if (flag2)
					{
						Rectangle rectangle3 = new Rectangle(rectangle.X - 1, 0, 1, 1);
						if (rightToLeft)
						{
							rectangle3.X = rectangle.Right;
						}
						graphics.FillRectangle(brush2, rectangle3);
					}
					else
					{
						graphics.FillRectangle(brush2, rectangle.X, rectangle.Top - 1, 1, 1);
					}
				}
			}
		}

		// Token: 0x060045B4 RID: 17844 RVA: 0x00125D94 File Offset: 0x00123F94
		private void RenderToolStripCurve(ToolStripRenderEventArgs e)
		{
			Rectangle rectangle = new Rectangle(Point.Empty, e.ToolStrip.Size);
			ToolStrip toolStrip = e.ToolStrip;
			Rectangle displayRectangle = toolStrip.DisplayRectangle;
			Graphics graphics = e.Graphics;
			Point empty = Point.Empty;
			Point point = new Point(rectangle.Width - 1, 0);
			Point point2 = new Point(0, rectangle.Height - 1);
			using (Brush brush = new SolidBrush(this.ColorTable.ToolStripGradientMiddle))
			{
				Rectangle rectangle2 = new Rectangle(empty, ToolStripProfessionalRenderer.onePix);
				rectangle2.X++;
				Rectangle rectangle3 = new Rectangle(empty, ToolStripProfessionalRenderer.onePix);
				rectangle3.Y++;
				Rectangle rectangle4 = new Rectangle(point, ToolStripProfessionalRenderer.onePix);
				rectangle4.X -= 2;
				Rectangle rectangle5 = rectangle4;
				rectangle5.Y++;
				rectangle5.X++;
				Rectangle[] array = new Rectangle[] { rectangle2, rectangle3, rectangle4, rectangle5 };
				for (int i = 0; i < array.Length; i++)
				{
					if (displayRectangle.IntersectsWith(array[i]))
					{
						array[i] = Rectangle.Empty;
					}
				}
				graphics.FillRectangles(brush, array);
			}
			using (Brush brush2 = new SolidBrush(this.ColorTable.ToolStripGradientEnd))
			{
				Point point3 = point2;
				point3.Offset(1, -1);
				if (!displayRectangle.Contains(point3))
				{
					graphics.FillRectangle(brush2, new Rectangle(point3, ToolStripProfessionalRenderer.onePix));
				}
				Rectangle rectangle6 = new Rectangle(point2.X, point2.Y - 2, 1, 1);
				if (!displayRectangle.IntersectsWith(rectangle6))
				{
					graphics.FillRectangle(brush2, rectangle6);
				}
			}
		}

		// Token: 0x060045B5 RID: 17845 RVA: 0x00125F90 File Offset: 0x00124190
		private void RenderSelectedButtonFill(Graphics g, Rectangle bounds)
		{
			if (bounds.Width == 0 || bounds.Height == 0)
			{
				return;
			}
			if (!this.UseSystemColors)
			{
				using (Brush brush = new LinearGradientBrush(bounds, this.ColorTable.ButtonSelectedGradientBegin, this.ColorTable.ButtonSelectedGradientEnd, LinearGradientMode.Vertical))
				{
					g.FillRectangle(brush, bounds);
					return;
				}
			}
			Color buttonSelectedHighlight = this.ColorTable.ButtonSelectedHighlight;
			using (Brush brush2 = new SolidBrush(buttonSelectedHighlight))
			{
				g.FillRectangle(brush2, bounds);
			}
		}

		// Token: 0x060045B6 RID: 17846 RVA: 0x00126030 File Offset: 0x00124230
		private void RenderCheckedButtonFill(Graphics g, Rectangle bounds)
		{
			if (bounds.Width == 0 || bounds.Height == 0)
			{
				return;
			}
			if (!this.UseSystemColors)
			{
				using (Brush brush = new LinearGradientBrush(bounds, this.ColorTable.ButtonCheckedGradientBegin, this.ColorTable.ButtonCheckedGradientEnd, LinearGradientMode.Vertical))
				{
					g.FillRectangle(brush, bounds);
					return;
				}
			}
			Color buttonCheckedHighlight = this.ColorTable.ButtonCheckedHighlight;
			using (Brush brush2 = new SolidBrush(buttonCheckedHighlight))
			{
				g.FillRectangle(brush2, bounds);
			}
		}

		// Token: 0x060045B7 RID: 17847 RVA: 0x001260D0 File Offset: 0x001242D0
		private void RenderSeparatorInternal(Graphics g, ToolStripItem item, Rectangle bounds, bool vertical)
		{
			Color separatorDark = this.ColorTable.SeparatorDark;
			Color separatorLight = this.ColorTable.SeparatorLight;
			Pen pen = new Pen(separatorDark);
			Pen pen2 = new Pen(separatorLight);
			bool flag = true;
			bool flag2 = true;
			bool flag3 = item is ToolStripSeparator;
			bool flag4 = false;
			if (flag3)
			{
				if (vertical)
				{
					if (!item.IsOnDropDown)
					{
						bounds.Y += 3;
						bounds.Height = Math.Max(0, bounds.Height - 6);
					}
				}
				else
				{
					ToolStripDropDownMenu toolStripDropDownMenu = item.GetCurrentParent() as ToolStripDropDownMenu;
					if (toolStripDropDownMenu != null)
					{
						if (toolStripDropDownMenu.RightToLeft == RightToLeft.No)
						{
							bounds.X += toolStripDropDownMenu.Padding.Left - 2;
							bounds.Width = toolStripDropDownMenu.Width - bounds.X;
						}
						else
						{
							bounds.X += 2;
							bounds.Width = toolStripDropDownMenu.Width - bounds.X - toolStripDropDownMenu.Padding.Right;
						}
					}
					else
					{
						flag4 = true;
					}
				}
			}
			try
			{
				if (vertical)
				{
					if (bounds.Height >= 4)
					{
						bounds.Inflate(0, -2);
					}
					bool flag5 = item.RightToLeft == RightToLeft.Yes;
					Pen pen3 = (flag5 ? pen2 : pen);
					Pen pen4 = (flag5 ? pen : pen2);
					int num = bounds.Width / 2;
					g.DrawLine(pen3, num, bounds.Top, num, bounds.Bottom - 1);
					num++;
					g.DrawLine(pen4, num, bounds.Top + 1, num, bounds.Bottom);
				}
				else
				{
					if (flag4 && bounds.Width >= 4)
					{
						bounds.Inflate(-2, 0);
					}
					int num2 = bounds.Height / 2;
					g.DrawLine(pen, bounds.Left, num2, bounds.Right - 1, num2);
					if (!flag3 || flag4)
					{
						num2++;
						g.DrawLine(pen2, bounds.Left + 1, num2, bounds.Right - 1, num2);
					}
				}
			}
			finally
			{
				if (flag && pen != null)
				{
					pen.Dispose();
				}
				if (flag2 && pen2 != null)
				{
					pen2.Dispose();
				}
			}
		}

		// Token: 0x060045B8 RID: 17848 RVA: 0x00126308 File Offset: 0x00124508
		private void RenderPressedButtonFill(Graphics g, Rectangle bounds)
		{
			if (bounds.Width == 0 || bounds.Height == 0)
			{
				return;
			}
			if (!this.UseSystemColors)
			{
				using (Brush brush = new LinearGradientBrush(bounds, this.ColorTable.ButtonPressedGradientBegin, this.ColorTable.ButtonPressedGradientEnd, LinearGradientMode.Vertical))
				{
					g.FillRectangle(brush, bounds);
					return;
				}
			}
			Color buttonPressedHighlight = this.ColorTable.ButtonPressedHighlight;
			using (Brush brush2 = new SolidBrush(buttonPressedHighlight))
			{
				g.FillRectangle(brush2, bounds);
			}
		}

		// Token: 0x060045B9 RID: 17849 RVA: 0x001263A8 File Offset: 0x001245A8
		private void RenderItemInternal(ToolStripItemRenderEventArgs e, bool useHotBorder)
		{
			Graphics graphics = e.Graphics;
			ToolStripItem item = e.Item;
			Rectangle rectangle = new Rectangle(Point.Empty, item.Size);
			bool flag = false;
			Rectangle rectangle2 = (item.Selected ? item.ContentRectangle : rectangle);
			if (item.BackgroundImage != null)
			{
				ControlPaint.DrawBackgroundImage(graphics, item.BackgroundImage, item.BackColor, item.BackgroundImageLayout, rectangle, rectangle2);
			}
			if (item.Pressed)
			{
				this.RenderPressedButtonFill(graphics, rectangle);
				flag = useHotBorder;
			}
			else if (item.Selected)
			{
				this.RenderSelectedButtonFill(graphics, rectangle);
				flag = useHotBorder;
			}
			else if (item.Owner != null && item.BackColor != item.Owner.BackColor)
			{
				using (Brush brush = new SolidBrush(item.BackColor))
				{
					graphics.FillRectangle(brush, rectangle);
				}
			}
			if (flag)
			{
				using (Pen pen = new Pen(this.ColorTable.ButtonSelectedBorder))
				{
					graphics.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
				}
			}
		}

		// Token: 0x060045BA RID: 17850 RVA: 0x001264E0 File Offset: 0x001246E0
		private void ScaleObjectSizesIfNeeded(int currentDeviceDpi)
		{
			if (DpiHelper.EnableToolStripPerMonitorV2HighDpiImprovements && this.previousDeviceDpi != currentDeviceDpi)
			{
				ToolStripRenderer.ScaleArrowOffsetsIfNeeded(currentDeviceDpi);
				this.overflowButtonWidth = DpiHelper.LogicalToDeviceUnits(12, currentDeviceDpi);
				this.overflowArrowWidth = DpiHelper.LogicalToDeviceUnits(9, currentDeviceDpi);
				this.overflowArrowHeight = DpiHelper.LogicalToDeviceUnits(5, currentDeviceDpi);
				this.overflowArrowOffsetY = DpiHelper.LogicalToDeviceUnits(8, currentDeviceDpi);
				this.gripPadding = DpiHelper.LogicalToDeviceUnits(4, currentDeviceDpi);
				this.iconWellGradientWidth = DpiHelper.LogicalToDeviceUnits(12, currentDeviceDpi);
				int num = DpiHelper.LogicalToDeviceUnits(1, currentDeviceDpi);
				this.scaledDropDownMenuItemPaintPadding = new Padding(num + 1, 0, num, 0);
				this.previousDeviceDpi = currentDeviceDpi;
				this.isScalingInitialized = true;
				return;
			}
			if (this.isScalingInitialized)
			{
				return;
			}
			if (DpiHelper.IsScalingRequired)
			{
				ToolStripRenderer.ScaleArrowOffsetsIfNeeded();
				this.overflowButtonWidth = DpiHelper.LogicalToDeviceUnitsX(12);
				this.overflowArrowWidth = DpiHelper.LogicalToDeviceUnitsX(9);
				this.overflowArrowHeight = DpiHelper.LogicalToDeviceUnitsY(5);
				this.overflowArrowOffsetY = DpiHelper.LogicalToDeviceUnitsY(8);
				if (DpiHelper.EnableToolStripHighDpiImprovements)
				{
					this.gripPadding = DpiHelper.LogicalToDeviceUnitsY(4);
					this.iconWellGradientWidth = DpiHelper.LogicalToDeviceUnitsX(12);
					int num2 = DpiHelper.LogicalToDeviceUnitsX(1);
					this.scaledDropDownMenuItemPaintPadding = new Padding(num2 + 1, 0, num2, 0);
				}
			}
			this.isScalingInitialized = true;
		}

		// Token: 0x060045BB RID: 17851 RVA: 0x00126608 File Offset: 0x00124808
		private Point RenderArrowInternal(Graphics g, Rectangle dropDownRect, ArrowDirection direction, Brush brush)
		{
			Point point = new Point(dropDownRect.Left + dropDownRect.Width / 2, dropDownRect.Top + dropDownRect.Height / 2);
			point.X += dropDownRect.Width % 2;
			Point[] array;
			if (direction <= ArrowDirection.Up)
			{
				if (direction == ArrowDirection.Left)
				{
					array = new Point[]
					{
						new Point(point.X + ToolStripRenderer.Offset2X, point.Y - ToolStripRenderer.Offset2Y - 1),
						new Point(point.X + ToolStripRenderer.Offset2X, point.Y + ToolStripRenderer.Offset2Y + 1),
						new Point(point.X - 1, point.Y)
					};
					goto IL_236;
				}
				if (direction == ArrowDirection.Up)
				{
					array = new Point[]
					{
						new Point(point.X - ToolStripRenderer.Offset2X, point.Y + 1),
						new Point(point.X + ToolStripRenderer.Offset2X + 1, point.Y + 1),
						new Point(point.X, point.Y - ToolStripRenderer.Offset2Y)
					};
					goto IL_236;
				}
			}
			else
			{
				if (direction == ArrowDirection.Right)
				{
					array = new Point[]
					{
						new Point(point.X - ToolStripRenderer.Offset2X, point.Y - ToolStripRenderer.Offset2Y - 1),
						new Point(point.X - ToolStripRenderer.Offset2X, point.Y + ToolStripRenderer.Offset2Y + 1),
						new Point(point.X + 1, point.Y)
					};
					goto IL_236;
				}
				if (direction != ArrowDirection.Down)
				{
				}
			}
			array = new Point[]
			{
				new Point(point.X - ToolStripRenderer.Offset2X, point.Y - 1),
				new Point(point.X + ToolStripRenderer.Offset2X + 1, point.Y - 1),
				new Point(point.X, point.Y + ToolStripRenderer.Offset2Y)
			};
			IL_236:
			g.FillPolygon(brush, array);
			return point;
		}

		// Token: 0x0400265C RID: 9820
		private const int GRIP_PADDING = 4;

		// Token: 0x0400265D RID: 9821
		private int gripPadding = 4;

		// Token: 0x0400265E RID: 9822
		private const int ICON_WELL_GRADIENT_WIDTH = 12;

		// Token: 0x0400265F RID: 9823
		private int iconWellGradientWidth = 12;

		// Token: 0x04002660 RID: 9824
		private static readonly Size onePix = new Size(1, 1);

		// Token: 0x04002661 RID: 9825
		private bool isScalingInitialized;

		// Token: 0x04002662 RID: 9826
		private const int OVERFLOW_BUTTON_WIDTH = 12;

		// Token: 0x04002663 RID: 9827
		private const int OVERFLOW_ARROW_WIDTH = 9;

		// Token: 0x04002664 RID: 9828
		private const int OVERFLOW_ARROW_HEIGHT = 5;

		// Token: 0x04002665 RID: 9829
		private const int OVERFLOW_ARROW_OFFSETY = 8;

		// Token: 0x04002666 RID: 9830
		private int overflowButtonWidth = 12;

		// Token: 0x04002667 RID: 9831
		private int overflowArrowWidth = 9;

		// Token: 0x04002668 RID: 9832
		private int overflowArrowHeight = 5;

		// Token: 0x04002669 RID: 9833
		private int overflowArrowOffsetY = 8;

		// Token: 0x0400266A RID: 9834
		private const int DROP_DOWN_MENU_ITEM_PAINT_PADDING_SIZE = 1;

		// Token: 0x0400266B RID: 9835
		private Padding scaledDropDownMenuItemPaintPadding = new Padding(2, 0, 1, 0);

		// Token: 0x0400266C RID: 9836
		private ProfessionalColorTable professionalColorTable;

		// Token: 0x0400266D RID: 9837
		private bool roundedEdges = true;

		// Token: 0x0400266E RID: 9838
		private ToolStripRenderer toolStripHighContrastRenderer;

		// Token: 0x0400266F RID: 9839
		private ToolStripRenderer toolStripLowResolutionRenderer;
	}
}
