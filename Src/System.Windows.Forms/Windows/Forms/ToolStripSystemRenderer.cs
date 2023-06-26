using System;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Handles the painting functionality for <see cref="T:System.Windows.Forms.ToolStrip" /> objects, using system colors and a flat visual style.</summary>
	// Token: 0x02000406 RID: 1030
	public class ToolStripSystemRenderer : ToolStripRenderer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripSystemRenderer" /> class.</summary>
		// Token: 0x06004745 RID: 18245 RVA: 0x0012B01E File Offset: 0x0012921E
		public ToolStripSystemRenderer()
		{
		}

		// Token: 0x06004746 RID: 18246 RVA: 0x0012B026 File Offset: 0x00129226
		internal ToolStripSystemRenderer(bool isDefault)
			: base(isDefault)
		{
		}

		// Token: 0x17001182 RID: 4482
		// (get) Token: 0x06004747 RID: 18247 RVA: 0x0012B02F File Offset: 0x0012922F
		internal override ToolStripRenderer RendererOverride
		{
			get
			{
				if (DisplayInformation.HighContrast)
				{
					return this.HighContrastRenderer;
				}
				return null;
			}
		}

		// Token: 0x17001183 RID: 4483
		// (get) Token: 0x06004748 RID: 18248 RVA: 0x0012B040 File Offset: 0x00129240
		internal ToolStripRenderer HighContrastRenderer
		{
			get
			{
				if (this.toolStripHighContrastRenderer == null)
				{
					this.toolStripHighContrastRenderer = new ToolStripHighContrastRenderer(!AccessibilityImprovements.Level5);
				}
				return this.toolStripHighContrastRenderer;
			}
		}

		// Token: 0x17001184 RID: 4484
		// (get) Token: 0x06004749 RID: 18249 RVA: 0x0012B063 File Offset: 0x00129263
		private static VisualStyleRenderer VisualStyleRenderer
		{
			get
			{
				if (Application.RenderWithVisualStyles)
				{
					if (ToolStripSystemRenderer.renderer == null && VisualStyleRenderer.IsElementDefined(VisualStyleElement.ToolBar.Button.Normal))
					{
						ToolStripSystemRenderer.renderer = new VisualStyleRenderer(VisualStyleElement.ToolBar.Button.Normal);
					}
				}
				else
				{
					ToolStripSystemRenderer.renderer = null;
				}
				return ToolStripSystemRenderer.renderer;
			}
		}

		// Token: 0x0600474A RID: 18250 RVA: 0x0012B09C File Offset: 0x0012929C
		private static void FillBackground(Graphics g, Rectangle bounds, Color backColor)
		{
			if (backColor.IsSystemColor)
			{
				g.FillRectangle(SystemBrushes.FromSystemColor(backColor), bounds);
				return;
			}
			using (Brush brush = new SolidBrush(backColor))
			{
				g.FillRectangle(brush, bounds);
			}
		}

		// Token: 0x0600474B RID: 18251 RVA: 0x0012B0EC File Offset: 0x001292EC
		private static bool GetPen(Color color, ref Pen pen)
		{
			if (color.IsSystemColor)
			{
				pen = SystemPens.FromSystemColor(color);
				return false;
			}
			pen = new Pen(color);
			return true;
		}

		// Token: 0x0600474C RID: 18252 RVA: 0x0012B10A File Offset: 0x0012930A
		private static int GetItemState(ToolStripItem item)
		{
			return (int)ToolStripSystemRenderer.GetToolBarState(item);
		}

		// Token: 0x0600474D RID: 18253 RVA: 0x0012B112 File Offset: 0x00129312
		private static int GetSplitButtonDropDownItemState(ToolStripSplitButton item)
		{
			return (int)ToolStripSystemRenderer.GetSplitButtonToolBarState(item, true);
		}

		// Token: 0x0600474E RID: 18254 RVA: 0x0012B11B File Offset: 0x0012931B
		private static int GetSplitButtonItemState(ToolStripSplitButton item)
		{
			return (int)ToolStripSystemRenderer.GetSplitButtonToolBarState(item, false);
		}

		// Token: 0x0600474F RID: 18255 RVA: 0x0012B124 File Offset: 0x00129324
		private static ToolBarState GetSplitButtonToolBarState(ToolStripSplitButton button, bool dropDownButton)
		{
			ToolBarState toolBarState = ToolBarState.Normal;
			if (button != null)
			{
				if (!button.Enabled)
				{
					toolBarState = ToolBarState.Disabled;
				}
				else if (dropDownButton)
				{
					if (button.DropDownButtonPressed || button.ButtonPressed)
					{
						toolBarState = ToolBarState.Pressed;
					}
					else if (button.DropDownButtonSelected || button.ButtonSelected)
					{
						toolBarState = ToolBarState.Hot;
					}
				}
				else if (button.ButtonPressed)
				{
					toolBarState = ToolBarState.Pressed;
				}
				else if (button.ButtonSelected)
				{
					toolBarState = ToolBarState.Hot;
				}
			}
			return toolBarState;
		}

		// Token: 0x06004750 RID: 18256 RVA: 0x0012B184 File Offset: 0x00129384
		private static ToolBarState GetToolBarState(ToolStripItem item)
		{
			ToolBarState toolBarState = ToolBarState.Normal;
			if (item != null)
			{
				if (!item.Enabled)
				{
					toolBarState = ToolBarState.Disabled;
				}
				if (item is ToolStripButton && ((ToolStripButton)item).Checked)
				{
					if (((ToolStripButton)item).Selected && AccessibilityImprovements.Level1)
					{
						toolBarState = ToolBarState.Hot;
					}
					else
					{
						toolBarState = ToolBarState.Checked;
					}
				}
				else if (item.Pressed)
				{
					toolBarState = ToolBarState.Pressed;
				}
				else if (item.Selected)
				{
					toolBarState = ToolBarState.Hot;
				}
			}
			return toolBarState;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderToolStripBackground" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06004751 RID: 18257 RVA: 0x0012B1E8 File Offset: 0x001293E8
		protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
		{
			ToolStrip toolStrip = e.ToolStrip;
			Graphics graphics = e.Graphics;
			Rectangle affectedBounds = e.AffectedBounds;
			if (!base.ShouldPaintBackground(toolStrip))
			{
				return;
			}
			if (toolStrip is StatusStrip)
			{
				ToolStripSystemRenderer.RenderStatusStripBackground(e);
				return;
			}
			if (DisplayInformation.HighContrast)
			{
				ToolStripSystemRenderer.FillBackground(graphics, affectedBounds, SystemColors.ButtonFace);
				return;
			}
			if (DisplayInformation.LowResolution)
			{
				ToolStripSystemRenderer.FillBackground(graphics, affectedBounds, (toolStrip is ToolStripDropDown) ? SystemColors.ControlLight : e.BackColor);
				return;
			}
			if (toolStrip.IsDropDown)
			{
				ToolStripSystemRenderer.FillBackground(graphics, affectedBounds, (!ToolStripManager.VisualStylesEnabled) ? e.BackColor : SystemColors.Menu);
				return;
			}
			if (toolStrip is MenuStrip)
			{
				ToolStripSystemRenderer.FillBackground(graphics, affectedBounds, (!ToolStripManager.VisualStylesEnabled) ? e.BackColor : SystemColors.MenuBar);
				return;
			}
			if (ToolStripManager.VisualStylesEnabled && VisualStyleRenderer.IsElementDefined(VisualStyleElement.Rebar.Band.Normal))
			{
				VisualStyleRenderer visualStyleRenderer = ToolStripSystemRenderer.VisualStyleRenderer;
				visualStyleRenderer.SetParameters(VisualStyleElement.ToolBar.Bar.Normal);
				visualStyleRenderer.DrawBackground(graphics, affectedBounds);
				return;
			}
			ToolStripSystemRenderer.FillBackground(graphics, affectedBounds, (!ToolStripManager.VisualStylesEnabled) ? e.BackColor : SystemColors.MenuBar);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderToolStripBorder" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06004752 RID: 18258 RVA: 0x0012B2EC File Offset: 0x001294EC
		protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
		{
			ToolStrip toolStrip = e.ToolStrip;
			Graphics graphics = e.Graphics;
			Rectangle clientRectangle = e.ToolStrip.ClientRectangle;
			if (toolStrip is StatusStrip)
			{
				this.RenderStatusStripBorder(e);
				return;
			}
			if (toolStrip is ToolStripDropDown)
			{
				ToolStripDropDown toolStripDropDown = toolStrip as ToolStripDropDown;
				if (toolStripDropDown.DropShadowEnabled && ToolStripManager.VisualStylesEnabled)
				{
					clientRectangle.Width--;
					clientRectangle.Height--;
					e.Graphics.DrawRectangle(new Pen(SystemColors.ControlDark), clientRectangle);
					return;
				}
				ControlPaint.DrawBorder3D(e.Graphics, clientRectangle, Border3DStyle.Raised);
				return;
			}
			else
			{
				if (ToolStripManager.VisualStylesEnabled)
				{
					e.Graphics.DrawLine(SystemPens.ButtonHighlight, 0, clientRectangle.Bottom - 1, clientRectangle.Width, clientRectangle.Bottom - 1);
					e.Graphics.DrawLine(SystemPens.InactiveBorder, 0, clientRectangle.Bottom - 2, clientRectangle.Width, clientRectangle.Bottom - 2);
					return;
				}
				e.Graphics.DrawLine(SystemPens.ButtonHighlight, 0, clientRectangle.Bottom - 1, clientRectangle.Width, clientRectangle.Bottom - 1);
				e.Graphics.DrawLine(SystemPens.ButtonShadow, 0, clientRectangle.Bottom - 2, clientRectangle.Width, clientRectangle.Bottom - 2);
				return;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderGrip" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripGripRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06004753 RID: 18259 RVA: 0x0012B434 File Offset: 0x00129634
		protected override void OnRenderGrip(ToolStripGripRenderEventArgs e)
		{
			Graphics graphics = e.Graphics;
			Rectangle rectangle = new Rectangle(Point.Empty, e.GripBounds.Size);
			bool flag = e.GripDisplayStyle == ToolStripGripDisplayStyle.Vertical;
			if (ToolStripManager.VisualStylesEnabled && VisualStyleRenderer.IsElementDefined(VisualStyleElement.Rebar.Gripper.Normal))
			{
				VisualStyleRenderer visualStyleRenderer = ToolStripSystemRenderer.VisualStyleRenderer;
				if (flag)
				{
					visualStyleRenderer.SetParameters(VisualStyleElement.Rebar.Gripper.Normal);
					rectangle.Height = (rectangle.Height - 2) / 4 * 4;
					rectangle.Y = Math.Max(0, (e.GripBounds.Height - rectangle.Height - 2) / 2);
				}
				else
				{
					visualStyleRenderer.SetParameters(VisualStyleElement.Rebar.GripperVertical.Normal);
				}
				visualStyleRenderer.DrawBackground(graphics, rectangle);
				return;
			}
			Color backColor = e.ToolStrip.BackColor;
			ToolStripSystemRenderer.FillBackground(graphics, rectangle, backColor);
			if (flag)
			{
				if (rectangle.Height >= 4)
				{
					rectangle.Inflate(0, -2);
				}
				rectangle.Width = 3;
			}
			else
			{
				if (rectangle.Width >= 4)
				{
					rectangle.Inflate(-2, 0);
				}
				rectangle.Height = 3;
			}
			this.RenderSmall3DBorderInternal(graphics, rectangle, ToolBarState.Hot, e.ToolStrip.RightToLeft == RightToLeft.Yes);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.ToolStripSystemRenderer.OnRenderItemBackground(System.Windows.Forms.ToolStripItemRenderEventArgs)" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06004754 RID: 18260 RVA: 0x000070A6 File Offset: 0x000052A6
		protected override void OnRenderItemBackground(ToolStripItemRenderEventArgs e)
		{
		}

		/// <summary>Draws the item background.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06004755 RID: 18261 RVA: 0x000070A6 File Offset: 0x000052A6
		protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderButtonBackground" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06004756 RID: 18262 RVA: 0x0012B552 File Offset: 0x00129752
		protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
		{
			if (AccessibilityImprovements.Level5 && this.RendererOverride != null)
			{
				base.OnRenderButtonBackground(e);
				return;
			}
			this.RenderItemInternal(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderDropDownButtonBackground" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06004757 RID: 18263 RVA: 0x0012B572 File Offset: 0x00129772
		protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
		{
			if (AccessibilityImprovements.Level5 && this.RendererOverride != null)
			{
				base.OnRenderDropDownButtonBackground(e);
				return;
			}
			this.RenderItemInternal(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderOverflowButtonBackground" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06004758 RID: 18264 RVA: 0x0012B594 File Offset: 0x00129794
		protected override void OnRenderOverflowButtonBackground(ToolStripItemRenderEventArgs e)
		{
			ToolStripItem item = e.Item;
			Graphics graphics = e.Graphics;
			if (ToolStripManager.VisualStylesEnabled && VisualStyleRenderer.IsElementDefined(VisualStyleElement.Rebar.Chevron.Normal))
			{
				VisualStyleElement normal = VisualStyleElement.Rebar.Chevron.Normal;
				VisualStyleRenderer visualStyleRenderer = ToolStripSystemRenderer.VisualStyleRenderer;
				visualStyleRenderer.SetParameters(normal.ClassName, normal.Part, ToolStripSystemRenderer.GetItemState(item));
				visualStyleRenderer.DrawBackground(graphics, new Rectangle(Point.Empty, item.Size));
				return;
			}
			this.RenderItemInternal(e);
			Color color = (item.Enabled ? SystemColors.ControlText : SystemColors.ControlDark);
			base.DrawArrow(new ToolStripArrowRenderEventArgs(graphics, item, new Rectangle(Point.Empty, item.Size), color, ArrowDirection.Down));
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderLabelBackground" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x06004759 RID: 18265 RVA: 0x0012B63C File Offset: 0x0012983C
		protected override void OnRenderLabelBackground(ToolStripItemRenderEventArgs e)
		{
			ToolStripSystemRenderer.RenderLabelInternal(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderMenuItemBackground" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x0600475A RID: 18266 RVA: 0x0012B644 File Offset: 0x00129844
		protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
		{
			ToolStripMenuItem toolStripMenuItem = e.Item as ToolStripMenuItem;
			Graphics graphics = e.Graphics;
			if (toolStripMenuItem is MdiControlStrip.SystemMenuItem)
			{
				return;
			}
			if (toolStripMenuItem != null)
			{
				Rectangle rectangle = new Rectangle(Point.Empty, toolStripMenuItem.Size);
				if (toolStripMenuItem.IsTopLevel && !ToolStripManager.VisualStylesEnabled)
				{
					if (toolStripMenuItem.BackgroundImage != null)
					{
						ControlPaint.DrawBackgroundImage(graphics, toolStripMenuItem.BackgroundImage, toolStripMenuItem.BackColor, toolStripMenuItem.BackgroundImageLayout, toolStripMenuItem.ContentRectangle, toolStripMenuItem.ContentRectangle);
					}
					else if (toolStripMenuItem.RawBackColor != Color.Empty)
					{
						ToolStripSystemRenderer.FillBackground(graphics, toolStripMenuItem.ContentRectangle, toolStripMenuItem.BackColor);
					}
					ToolBarState toolBarState = ToolStripSystemRenderer.GetToolBarState(toolStripMenuItem);
					this.RenderSmall3DBorderInternal(graphics, rectangle, toolBarState, toolStripMenuItem.RightToLeft == RightToLeft.Yes);
					return;
				}
				Rectangle rectangle2 = new Rectangle(Point.Empty, toolStripMenuItem.Size);
				if (toolStripMenuItem.IsOnDropDown)
				{
					rectangle2.X += 2;
					rectangle2.Width -= 3;
				}
				if (toolStripMenuItem.Selected || toolStripMenuItem.Pressed)
				{
					if (!AccessibilityImprovements.Level1 || toolStripMenuItem.Enabled)
					{
						graphics.FillRectangle(SystemBrushes.Highlight, rectangle2);
					}
					if (!AccessibilityImprovements.Level1)
					{
						return;
					}
					Color color = (ToolStripManager.VisualStylesEnabled ? SystemColors.Highlight : ProfessionalColors.MenuItemBorder);
					using (Pen pen = new Pen(color))
					{
						graphics.DrawRectangle(pen, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
						return;
					}
				}
				if (toolStripMenuItem.BackgroundImage != null)
				{
					ControlPaint.DrawBackgroundImage(graphics, toolStripMenuItem.BackgroundImage, toolStripMenuItem.BackColor, toolStripMenuItem.BackgroundImageLayout, toolStripMenuItem.ContentRectangle, rectangle2);
					return;
				}
				if (!ToolStripManager.VisualStylesEnabled && toolStripMenuItem.RawBackColor != Color.Empty)
				{
					ToolStripSystemRenderer.FillBackground(graphics, rectangle2, toolStripMenuItem.BackColor);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderSeparator" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripSeparatorRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x0600475B RID: 18267 RVA: 0x0012B824 File Offset: 0x00129A24
		protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
		{
			this.RenderSeparatorInternal(e.Graphics, e.Item, new Rectangle(Point.Empty, e.Item.Size), e.Vertical);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderToolStripStatusLabelBackground" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x0600475C RID: 18268 RVA: 0x0012B854 File Offset: 0x00129A54
		protected override void OnRenderToolStripStatusLabelBackground(ToolStripItemRenderEventArgs e)
		{
			ToolStripSystemRenderer.RenderLabelInternal(e);
			ToolStripStatusLabel toolStripStatusLabel = e.Item as ToolStripStatusLabel;
			ControlPaint.DrawBorder3D(e.Graphics, new Rectangle(0, 0, toolStripStatusLabel.Width - 1, toolStripStatusLabel.Height - 1), toolStripStatusLabel.BorderStyle, (Border3DSide)toolStripStatusLabel.BorderSides);
		}

		/// <summary>Raises the OnRenderSplitButtonBackground event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> that contains the event data.</param>
		// Token: 0x0600475D RID: 18269 RVA: 0x0012B8A4 File Offset: 0x00129AA4
		protected override void OnRenderSplitButtonBackground(ToolStripItemRenderEventArgs e)
		{
			if (AccessibilityImprovements.Level5 && this.RendererOverride != null)
			{
				base.OnRenderSplitButtonBackground(e);
				return;
			}
			ToolStripSplitButton toolStripSplitButton = e.Item as ToolStripSplitButton;
			Graphics graphics = e.Graphics;
			bool flag = toolStripSplitButton.RightToLeft == RightToLeft.Yes;
			Color color = (toolStripSplitButton.Enabled ? SystemColors.ControlText : SystemColors.ControlDark);
			VisualStyleElement visualStyleElement = (flag ? VisualStyleElement.ToolBar.SplitButton.Normal : VisualStyleElement.ToolBar.SplitButtonDropDown.Normal);
			VisualStyleElement visualStyleElement2 = (flag ? VisualStyleElement.ToolBar.DropDownButton.Normal : VisualStyleElement.ToolBar.SplitButton.Normal);
			Rectangle rectangle = new Rectangle(Point.Empty, toolStripSplitButton.Size);
			if (ToolStripManager.VisualStylesEnabled && VisualStyleRenderer.IsElementDefined(visualStyleElement) && VisualStyleRenderer.IsElementDefined(visualStyleElement2))
			{
				VisualStyleRenderer visualStyleRenderer = ToolStripSystemRenderer.VisualStyleRenderer;
				visualStyleRenderer.SetParameters(visualStyleElement2.ClassName, visualStyleElement2.Part, ToolStripSystemRenderer.GetSplitButtonItemState(toolStripSplitButton));
				Rectangle buttonBounds = toolStripSplitButton.ButtonBounds;
				if (flag)
				{
					buttonBounds.Inflate(2, 0);
				}
				visualStyleRenderer.DrawBackground(graphics, buttonBounds);
				visualStyleRenderer.SetParameters(visualStyleElement.ClassName, visualStyleElement.Part, ToolStripSystemRenderer.GetSplitButtonDropDownItemState(toolStripSplitButton));
				visualStyleRenderer.DrawBackground(graphics, toolStripSplitButton.DropDownButtonBounds);
				Rectangle contentRectangle = toolStripSplitButton.ContentRectangle;
				if (toolStripSplitButton.BackgroundImage != null)
				{
					ControlPaint.DrawBackgroundImage(graphics, toolStripSplitButton.BackgroundImage, toolStripSplitButton.BackColor, toolStripSplitButton.BackgroundImageLayout, contentRectangle, contentRectangle);
				}
				this.RenderSeparatorInternal(graphics, toolStripSplitButton, toolStripSplitButton.SplitterBounds, true);
				if (flag || toolStripSplitButton.BackgroundImage != null)
				{
					base.DrawArrow(new ToolStripArrowRenderEventArgs(graphics, toolStripSplitButton, toolStripSplitButton.DropDownButtonBounds, color, ArrowDirection.Down));
					return;
				}
			}
			else
			{
				Rectangle buttonBounds2 = toolStripSplitButton.ButtonBounds;
				if (toolStripSplitButton.BackgroundImage != null)
				{
					Rectangle rectangle2 = (toolStripSplitButton.Selected ? toolStripSplitButton.ContentRectangle : rectangle);
					if (toolStripSplitButton.BackgroundImage != null)
					{
						ControlPaint.DrawBackgroundImage(graphics, toolStripSplitButton.BackgroundImage, toolStripSplitButton.BackColor, toolStripSplitButton.BackgroundImageLayout, rectangle, rectangle2);
					}
				}
				else
				{
					ToolStripSystemRenderer.FillBackground(graphics, buttonBounds2, toolStripSplitButton.BackColor);
				}
				ToolBarState toolBarState = ToolStripSystemRenderer.GetSplitButtonToolBarState(toolStripSplitButton, false);
				this.RenderSmall3DBorderInternal(graphics, buttonBounds2, toolBarState, flag);
				Rectangle dropDownButtonBounds = toolStripSplitButton.DropDownButtonBounds;
				if (toolStripSplitButton.BackgroundImage == null)
				{
					ToolStripSystemRenderer.FillBackground(graphics, dropDownButtonBounds, toolStripSplitButton.BackColor);
				}
				toolBarState = ToolStripSystemRenderer.GetSplitButtonToolBarState(toolStripSplitButton, true);
				if (toolBarState == ToolBarState.Pressed || toolBarState == ToolBarState.Hot)
				{
					this.RenderSmall3DBorderInternal(graphics, dropDownButtonBounds, toolBarState, flag);
				}
				base.DrawArrow(new ToolStripArrowRenderEventArgs(graphics, toolStripSplitButton, dropDownButtonBounds, color, ArrowDirection.Down));
			}
		}

		// Token: 0x0600475E RID: 18270 RVA: 0x0012BAD8 File Offset: 0x00129CD8
		private void RenderItemInternal(ToolStripItemRenderEventArgs e)
		{
			ToolStripItem item = e.Item;
			Graphics graphics = e.Graphics;
			ToolBarState toolBarState = ToolStripSystemRenderer.GetToolBarState(item);
			VisualStyleElement normal = VisualStyleElement.ToolBar.Button.Normal;
			if (ToolStripManager.VisualStylesEnabled && VisualStyleRenderer.IsElementDefined(normal))
			{
				VisualStyleRenderer visualStyleRenderer = ToolStripSystemRenderer.VisualStyleRenderer;
				visualStyleRenderer.SetParameters(normal.ClassName, normal.Part, (int)toolBarState);
				visualStyleRenderer.DrawBackground(graphics, new Rectangle(Point.Empty, item.Size));
				if (AccessibilityImprovements.Level5 && !SystemInformation.HighContrast && (toolBarState == ToolBarState.Hot || toolBarState == ToolBarState.Pressed || toolBarState == ToolBarState.Checked))
				{
					Rectangle clientBounds = item.ClientBounds;
					clientBounds.Height--;
					ControlPaint.DrawBorderSimple(graphics, clientBounds, SystemColors.Highlight, ButtonBorderStyle.Solid);
				}
			}
			else
			{
				this.RenderSmall3DBorderInternal(graphics, new Rectangle(Point.Empty, item.Size), toolBarState, item.RightToLeft == RightToLeft.Yes);
			}
			Rectangle contentRectangle = item.ContentRectangle;
			if (item.BackgroundImage != null)
			{
				ControlPaint.DrawBackgroundImage(graphics, item.BackgroundImage, item.BackColor, item.BackgroundImageLayout, contentRectangle, contentRectangle);
				return;
			}
			ToolStrip currentParent = item.GetCurrentParent();
			if (currentParent != null && toolBarState != ToolBarState.Checked && item.BackColor != currentParent.BackColor)
			{
				ToolStripSystemRenderer.FillBackground(graphics, contentRectangle, item.BackColor);
			}
		}

		// Token: 0x0600475F RID: 18271 RVA: 0x0012BC04 File Offset: 0x00129E04
		private void RenderSeparatorInternal(Graphics g, ToolStripItem item, Rectangle bounds, bool vertical)
		{
			VisualStyleElement visualStyleElement = (vertical ? VisualStyleElement.ToolBar.SeparatorHorizontal.Normal : VisualStyleElement.ToolBar.SeparatorVertical.Normal);
			if (ToolStripManager.VisualStylesEnabled && VisualStyleRenderer.IsElementDefined(visualStyleElement))
			{
				VisualStyleRenderer visualStyleRenderer = ToolStripSystemRenderer.VisualStyleRenderer;
				visualStyleRenderer.SetParameters(visualStyleElement.ClassName, visualStyleElement.Part, ToolStripSystemRenderer.GetItemState(item));
				visualStyleRenderer.DrawBackground(g, bounds);
				return;
			}
			Color foreColor = item.ForeColor;
			Color backColor = item.BackColor;
			Pen controlDark = SystemPens.ControlDark;
			bool pen = ToolStripSystemRenderer.GetPen(foreColor, ref controlDark);
			try
			{
				if (vertical)
				{
					if (bounds.Height >= 4)
					{
						bounds.Inflate(0, -2);
					}
					bool flag = item.RightToLeft == RightToLeft.Yes;
					Pen pen2 = (flag ? SystemPens.ButtonHighlight : controlDark);
					Pen pen3 = (flag ? controlDark : SystemPens.ButtonHighlight);
					int num = bounds.Width / 2;
					g.DrawLine(pen2, num, bounds.Top, num, bounds.Bottom);
					num++;
					g.DrawLine(pen3, num, bounds.Top, num, bounds.Bottom);
				}
				else
				{
					if (bounds.Width >= 4)
					{
						bounds.Inflate(-2, 0);
					}
					int num2 = bounds.Height / 2;
					g.DrawLine(controlDark, bounds.Left, num2, bounds.Right, num2);
					num2++;
					g.DrawLine(SystemPens.ButtonHighlight, bounds.Left, num2, bounds.Right, num2);
				}
			}
			finally
			{
				if (pen && controlDark != null)
				{
					controlDark.Dispose();
				}
			}
		}

		// Token: 0x06004760 RID: 18272 RVA: 0x0012BD80 File Offset: 0x00129F80
		private void RenderSmall3DBorderInternal(Graphics g, Rectangle bounds, ToolBarState state, bool rightToLeft)
		{
			if (state == ToolBarState.Hot || state == ToolBarState.Pressed || state == ToolBarState.Checked)
			{
				Pen pen = ((state == ToolBarState.Hot) ? SystemPens.ButtonHighlight : SystemPens.ButtonShadow);
				Pen pen2 = ((state == ToolBarState.Hot) ? SystemPens.ButtonShadow : SystemPens.ButtonHighlight);
				Pen pen3 = (rightToLeft ? pen2 : pen);
				Pen pen4 = (rightToLeft ? pen : pen2);
				g.DrawLine(pen, bounds.Left, bounds.Top, bounds.Right - 1, bounds.Top);
				g.DrawLine(pen3, bounds.Left, bounds.Top, bounds.Left, bounds.Bottom - 1);
				g.DrawLine(pen4, bounds.Right - 1, bounds.Top, bounds.Right - 1, bounds.Bottom - 1);
				g.DrawLine(pen2, bounds.Left, bounds.Bottom - 1, bounds.Right - 1, bounds.Bottom - 1);
			}
		}

		// Token: 0x06004761 RID: 18273 RVA: 0x0012BE6C File Offset: 0x0012A06C
		private void RenderStatusStripBorder(ToolStripRenderEventArgs e)
		{
			if (!Application.RenderWithVisualStyles)
			{
				e.Graphics.DrawLine(SystemPens.ButtonHighlight, 0, 0, e.ToolStrip.Width, 0);
			}
		}

		// Token: 0x06004762 RID: 18274 RVA: 0x0012BE94 File Offset: 0x0012A094
		private static void RenderStatusStripBackground(ToolStripRenderEventArgs e)
		{
			if (Application.RenderWithVisualStyles)
			{
				VisualStyleRenderer visualStyleRenderer = ToolStripSystemRenderer.VisualStyleRenderer;
				visualStyleRenderer.SetParameters(VisualStyleElement.Status.Bar.Normal);
				visualStyleRenderer.DrawBackground(e.Graphics, new Rectangle(0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1));
				return;
			}
			if (!SystemInformation.InLockedTerminalSession())
			{
				e.Graphics.Clear(e.BackColor);
			}
		}

		// Token: 0x06004763 RID: 18275 RVA: 0x0012BF00 File Offset: 0x0012A100
		private static void RenderLabelInternal(ToolStripItemRenderEventArgs e)
		{
			ToolStripItem item = e.Item;
			Graphics graphics = e.Graphics;
			Rectangle contentRectangle = item.ContentRectangle;
			if (item.BackgroundImage != null)
			{
				ControlPaint.DrawBackgroundImage(graphics, item.BackgroundImage, item.BackColor, item.BackgroundImageLayout, contentRectangle, contentRectangle);
				return;
			}
			VisualStyleRenderer visualStyleRenderer = ToolStripSystemRenderer.VisualStyleRenderer;
			if (visualStyleRenderer == null || item.BackColor != SystemColors.Control)
			{
				ToolStripSystemRenderer.FillBackground(graphics, contentRectangle, item.BackColor);
			}
		}

		// Token: 0x040026D7 RID: 9943
		[ThreadStatic]
		private static VisualStyleRenderer renderer;

		// Token: 0x040026D8 RID: 9944
		private ToolStripRenderer toolStripHighContrastRenderer;
	}
}
