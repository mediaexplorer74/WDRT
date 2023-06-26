using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace System.Windows.Forms
{
	// Token: 0x020003C3 RID: 963
	internal class ToolStripHighContrastRenderer : ToolStripSystemRenderer
	{
		// Token: 0x0600415C RID: 16732 RVA: 0x001177EB File Offset: 0x001159EB
		public ToolStripHighContrastRenderer(bool systemRenderMode)
		{
			this.options[ToolStripHighContrastRenderer.optionsDottedBorder | ToolStripHighContrastRenderer.optionsDottedGrip | ToolStripHighContrastRenderer.optionsFillWhenSelected] = !systemRenderMode;
		}

		// Token: 0x17000FF0 RID: 4080
		// (get) Token: 0x0600415D RID: 16733 RVA: 0x00117813 File Offset: 0x00115A13
		public bool DottedBorder
		{
			get
			{
				return this.options[ToolStripHighContrastRenderer.optionsDottedBorder];
			}
		}

		// Token: 0x17000FF1 RID: 4081
		// (get) Token: 0x0600415E RID: 16734 RVA: 0x00117825 File Offset: 0x00115A25
		public bool DottedGrip
		{
			get
			{
				return this.options[ToolStripHighContrastRenderer.optionsDottedGrip];
			}
		}

		// Token: 0x17000FF2 RID: 4082
		// (get) Token: 0x0600415F RID: 16735 RVA: 0x00117837 File Offset: 0x00115A37
		public bool FillWhenSelected
		{
			get
			{
				return this.options[ToolStripHighContrastRenderer.optionsFillWhenSelected];
			}
		}

		// Token: 0x17000FF3 RID: 4083
		// (get) Token: 0x06004160 RID: 16736 RVA: 0x00015C90 File Offset: 0x00013E90
		internal override ToolStripRenderer RendererOverride
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004161 RID: 16737 RVA: 0x00117849 File Offset: 0x00115A49
		protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
		{
			base.OnRenderArrow(e);
		}

		// Token: 0x06004162 RID: 16738 RVA: 0x00117854 File Offset: 0x00115A54
		protected override void OnRenderGrip(ToolStripGripRenderEventArgs e)
		{
			if (this.DottedGrip)
			{
				Graphics graphics = e.Graphics;
				Rectangle gripBounds = e.GripBounds;
				ToolStrip toolStrip = e.ToolStrip;
				int num = ((toolStrip.Orientation == Orientation.Horizontal) ? gripBounds.Height : gripBounds.Width);
				int num2 = ((toolStrip.Orientation == Orientation.Horizontal) ? gripBounds.Width : gripBounds.Height);
				int num3 = (num - 8) / 4;
				if (num3 > 0)
				{
					Rectangle[] array = new Rectangle[num3];
					int num4 = 4;
					int num5 = num2 / 2;
					for (int i = 0; i < num3; i++)
					{
						array[i] = ((toolStrip.Orientation == Orientation.Horizontal) ? new Rectangle(num5, num4, 2, 2) : new Rectangle(num4, num5, 2, 2));
						num4 += 4;
					}
					graphics.FillRectangles(SystemBrushes.ControlLight, array);
					return;
				}
			}
			else
			{
				base.OnRenderGrip(e);
			}
		}

		// Token: 0x06004163 RID: 16739 RVA: 0x00117928 File Offset: 0x00115B28
		protected override void OnRenderDropDownButtonBackground(ToolStripItemRenderEventArgs e)
		{
			if (this.FillWhenSelected)
			{
				this.RenderItemInternalFilled(e, false);
				return;
			}
			base.OnRenderDropDownButtonBackground(e);
			if (e.Item.Pressed)
			{
				e.Graphics.DrawRectangle(SystemPens.ButtonHighlight, new Rectangle(0, 0, e.Item.Width - 1, e.Item.Height - 1));
			}
		}

		// Token: 0x06004164 RID: 16740 RVA: 0x0011798C File Offset: 0x00115B8C
		protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
		{
			if (AccessibilityImprovements.Level1)
			{
				Color color = Color.FromArgb(255, 4, 2, 4);
				ColorMap[] array = new ColorMap[]
				{
					new ColorMap()
				};
				array[0].OldColor = color;
				array[0].NewColor = (((e.Item.Selected || e.Item.Pressed) && e.Item.Enabled) ? SystemColors.HighlightText : SystemColors.MenuText);
				ImageAttributes imageAttributes = e.ImageAttributes ?? new ImageAttributes();
				imageAttributes.SetRemapTable(array, ColorAdjustType.Bitmap);
				e.ImageAttributes = imageAttributes;
			}
			base.OnRenderItemCheck(e);
		}

		// Token: 0x06004165 RID: 16741 RVA: 0x000070A6 File Offset: 0x000052A6
		protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
		{
		}

		// Token: 0x06004166 RID: 16742 RVA: 0x00117A2A File Offset: 0x00115C2A
		protected override void OnRenderItemBackground(ToolStripItemRenderEventArgs e)
		{
			base.OnRenderItemBackground(e);
		}

		// Token: 0x06004167 RID: 16743 RVA: 0x00117A34 File Offset: 0x00115C34
		protected override void OnRenderSplitButtonBackground(ToolStripItemRenderEventArgs e)
		{
			ToolStripSplitButton toolStripSplitButton = e.Item as ToolStripSplitButton;
			Rectangle rectangle = new Rectangle(Point.Empty, e.Item.Size);
			Graphics graphics = e.Graphics;
			if (toolStripSplitButton != null)
			{
				Rectangle dropDownButtonBounds = toolStripSplitButton.DropDownButtonBounds;
				if (toolStripSplitButton.Pressed)
				{
					graphics.DrawRectangle(SystemPens.ButtonHighlight, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
				}
				else if (toolStripSplitButton.Selected)
				{
					graphics.FillRectangle(SystemBrushes.Highlight, rectangle);
					graphics.DrawRectangle(SystemPens.ButtonHighlight, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
					graphics.DrawRectangle(SystemPens.ButtonHighlight, dropDownButtonBounds);
				}
				Color color = ((AccessibilityImprovements.Level2 && toolStripSplitButton.Selected && !toolStripSplitButton.Pressed) ? SystemColors.HighlightText : SystemColors.ControlText);
				base.DrawArrow(new ToolStripArrowRenderEventArgs(graphics, toolStripSplitButton, dropDownButtonBounds, color, ArrowDirection.Down));
			}
		}

		// Token: 0x06004168 RID: 16744 RVA: 0x00117B2F File Offset: 0x00115D2F
		protected override void OnRenderStatusStripSizingGrip(ToolStripRenderEventArgs e)
		{
			base.OnRenderStatusStripSizingGrip(e);
		}

		// Token: 0x06004169 RID: 16745 RVA: 0x00117B38 File Offset: 0x00115D38
		protected override void OnRenderLabelBackground(ToolStripItemRenderEventArgs e)
		{
			if (this.FillWhenSelected)
			{
				this.RenderItemInternalFilled(e);
				return;
			}
			base.OnRenderLabelBackground(e);
		}

		// Token: 0x0600416A RID: 16746 RVA: 0x00117B54 File Offset: 0x00115D54
		protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
		{
			base.OnRenderMenuItemBackground(e);
			if (!e.Item.IsOnDropDown && e.Item.Pressed)
			{
				e.Graphics.DrawRectangle(SystemPens.ButtonHighlight, 0, 0, e.Item.Width - 1, e.Item.Height - 1);
			}
		}

		// Token: 0x0600416B RID: 16747 RVA: 0x00117BB0 File Offset: 0x00115DB0
		protected override void OnRenderOverflowButtonBackground(ToolStripItemRenderEventArgs e)
		{
			if (this.FillWhenSelected)
			{
				this.RenderItemInternalFilled(e, false);
				ToolStripItem item = e.Item;
				Graphics graphics = e.Graphics;
				Color color = (item.Enabled ? SystemColors.ControlText : SystemColors.ControlDark);
				base.DrawArrow(new ToolStripArrowRenderEventArgs(graphics, item, new Rectangle(Point.Empty, item.Size), color, ArrowDirection.Down));
				return;
			}
			base.OnRenderOverflowButtonBackground(e);
		}

		// Token: 0x0600416C RID: 16748 RVA: 0x00117C18 File Offset: 0x00115E18
		protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
		{
			if (AccessibilityImprovements.Level2 && e.Item.Selected && (!e.Item.Pressed || e.Item is ToolStripButton))
			{
				e.DefaultTextColor = SystemColors.HighlightText;
			}
			else if (e.TextColor != SystemColors.HighlightText && e.TextColor != SystemColors.ControlText)
			{
				if (e.Item.Selected || e.Item.Pressed)
				{
					e.DefaultTextColor = SystemColors.HighlightText;
				}
				else
				{
					e.DefaultTextColor = SystemColors.ControlText;
				}
			}
			if (AccessibilityImprovements.Level1 && typeof(ToolStripButton).IsAssignableFrom(e.Item.GetType()) && ((ToolStripButton)e.Item).DisplayStyle != ToolStripItemDisplayStyle.Image && ((ToolStripButton)e.Item).Checked)
			{
				e.TextColor = SystemColors.HighlightText;
			}
			base.OnRenderItemText(e);
		}

		// Token: 0x0600416D RID: 16749 RVA: 0x000070A6 File Offset: 0x000052A6
		protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
		{
		}

		// Token: 0x0600416E RID: 16750 RVA: 0x00117D10 File Offset: 0x00115F10
		protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
		{
			Rectangle rectangle = new Rectangle(Point.Empty, e.ToolStrip.Size);
			Graphics graphics = e.Graphics;
			if (e.ToolStrip is ToolStripDropDown)
			{
				graphics.DrawRectangle(SystemPens.ButtonHighlight, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
				if (!(e.ToolStrip is ToolStripOverflow))
				{
					graphics.FillRectangle(SystemBrushes.Control, e.ConnectedArea);
					return;
				}
			}
			else if (!(e.ToolStrip is MenuStrip))
			{
				if (e.ToolStrip is StatusStrip)
				{
					graphics.DrawRectangle(SystemPens.ButtonShadow, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
					return;
				}
				this.RenderToolStripBackgroundInternal(e);
			}
		}

		// Token: 0x0600416F RID: 16751 RVA: 0x00117DE0 File Offset: 0x00115FE0
		private void RenderToolStripBackgroundInternal(ToolStripRenderEventArgs e)
		{
			Rectangle rectangle = new Rectangle(Point.Empty, e.ToolStrip.Size);
			Graphics graphics = e.Graphics;
			if (this.DottedBorder)
			{
				using (Pen pen = new Pen(SystemColors.ButtonShadow))
				{
					pen.DashStyle = DashStyle.Dot;
					bool flag = (rectangle.Width & 1) == 1;
					bool flag2 = (rectangle.Height & 1) == 1;
					int num = 2;
					graphics.DrawLine(pen, rectangle.X + num, rectangle.Y, rectangle.Width - 1, rectangle.Y);
					graphics.DrawLine(pen, rectangle.X + num, rectangle.Height - 1, rectangle.Width - 1, rectangle.Height - 1);
					graphics.DrawLine(pen, rectangle.X, rectangle.Y + num, rectangle.X, rectangle.Height - 1);
					graphics.DrawLine(pen, rectangle.Width - 1, rectangle.Y + num, rectangle.Width - 1, rectangle.Height - 1);
					graphics.FillRectangle(SystemBrushes.ButtonShadow, new Rectangle(1, 1, 1, 1));
					if (flag)
					{
						graphics.FillRectangle(SystemBrushes.ButtonShadow, new Rectangle(rectangle.Width - 2, 1, 1, 1));
					}
					if (flag2)
					{
						graphics.FillRectangle(SystemBrushes.ButtonShadow, new Rectangle(1, rectangle.Height - 2, 1, 1));
					}
					if (flag2 && flag)
					{
						graphics.FillRectangle(SystemBrushes.ButtonShadow, new Rectangle(rectangle.Width - 2, rectangle.Height - 2, 1, 1));
					}
					return;
				}
			}
			rectangle.Width--;
			rectangle.Height--;
			graphics.DrawRectangle(SystemPens.ButtonShadow, rectangle);
		}

		// Token: 0x06004170 RID: 16752 RVA: 0x00117FC0 File Offset: 0x001161C0
		protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
		{
			Pen buttonShadow = SystemPens.ButtonShadow;
			Graphics graphics = e.Graphics;
			Rectangle rectangle = new Rectangle(Point.Empty, e.Item.Size);
			if (e.Vertical)
			{
				if (rectangle.Height >= 8)
				{
					rectangle.Inflate(0, -4);
				}
				int num = rectangle.Width / 2;
				graphics.DrawLine(buttonShadow, num, rectangle.Top, num, rectangle.Bottom - 1);
				return;
			}
			if (rectangle.Width >= 4)
			{
				rectangle.Inflate(-2, 0);
			}
			int num2 = rectangle.Height / 2;
			graphics.DrawLine(buttonShadow, rectangle.Left, num2, rectangle.Right - 1, num2);
		}

		// Token: 0x06004171 RID: 16753 RVA: 0x0011806C File Offset: 0x0011626C
		internal static bool IsHighContrastWhiteOnBlack()
		{
			return SystemColors.Control.ToArgb() == Color.Black.ToArgb();
		}

		// Token: 0x06004172 RID: 16754 RVA: 0x00118098 File Offset: 0x00116298
		protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
		{
			Image image = e.Image;
			if (image != null)
			{
				if (Image.GetPixelFormatSize(image.PixelFormat) > 16)
				{
					base.OnRenderItemImage(e);
					return;
				}
				Graphics graphics = e.Graphics;
				ToolStripItem item = e.Item;
				Rectangle imageRectangle = e.ImageRectangle;
				using (ImageAttributes imageAttributes = new ImageAttributes())
				{
					if (ToolStripHighContrastRenderer.IsHighContrastWhiteOnBlack() && (!this.FillWhenSelected || (!e.Item.Pressed && !e.Item.Selected)))
					{
						ColorMap colorMap = new ColorMap();
						ColorMap colorMap2 = new ColorMap();
						ColorMap colorMap3 = new ColorMap();
						colorMap.OldColor = Color.Black;
						colorMap.NewColor = Color.White;
						colorMap2.OldColor = Color.White;
						colorMap2.NewColor = Color.Black;
						colorMap3.OldColor = Color.FromArgb(0, 0, 128);
						colorMap3.NewColor = Color.White;
						imageAttributes.SetRemapTable(new ColorMap[] { colorMap, colorMap2, colorMap3 }, ColorAdjustType.Bitmap);
					}
					if (item.ImageScaling == ToolStripItemImageScaling.None)
					{
						graphics.DrawImage(image, imageRectangle, 0, 0, imageRectangle.Width, imageRectangle.Height, GraphicsUnit.Pixel, imageAttributes);
					}
					else
					{
						graphics.DrawImage(image, imageRectangle, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);
					}
				}
			}
		}

		// Token: 0x06004173 RID: 16755 RVA: 0x001181F4 File Offset: 0x001163F4
		protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
		{
			if (!this.FillWhenSelected)
			{
				base.OnRenderButtonBackground(e);
				return;
			}
			ToolStripButton toolStripButton = e.Item as ToolStripButton;
			if (toolStripButton == null || !toolStripButton.Checked)
			{
				this.RenderItemInternalFilled(e);
				return;
			}
			Graphics graphics = e.Graphics;
			Rectangle rectangle = new Rectangle(Point.Empty, e.Item.Size);
			if (toolStripButton.CheckState == CheckState.Checked || AccessibilityImprovements.Level5)
			{
				graphics.FillRectangle(SystemBrushes.Highlight, rectangle);
			}
			if (toolStripButton.Selected && AccessibilityImprovements.Level1)
			{
				graphics.DrawRectangle(SystemPens.Highlight, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
				return;
			}
			graphics.DrawRectangle(SystemPens.ControlLight, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
		}

		// Token: 0x06004174 RID: 16756 RVA: 0x001182D9 File Offset: 0x001164D9
		private void RenderItemInternalFilled(ToolStripItemRenderEventArgs e)
		{
			this.RenderItemInternalFilled(e, true);
		}

		// Token: 0x06004175 RID: 16757 RVA: 0x001182E4 File Offset: 0x001164E4
		private void RenderItemInternalFilled(ToolStripItemRenderEventArgs e, bool pressFill)
		{
			Graphics graphics = e.Graphics;
			Rectangle rectangle = new Rectangle(Point.Empty, e.Item.Size);
			if (!e.Item.Pressed)
			{
				if (e.Item.Selected)
				{
					graphics.FillRectangle(SystemBrushes.Highlight, rectangle);
					graphics.DrawRectangle(SystemPens.ControlLight, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
				}
				return;
			}
			if (pressFill)
			{
				graphics.FillRectangle(SystemBrushes.Highlight, rectangle);
				return;
			}
			graphics.DrawRectangle(SystemPens.ControlLight, rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
		}

		// Token: 0x0400250A RID: 9482
		private const int GRIP_PADDING = 4;

		// Token: 0x0400250B RID: 9483
		private BitVector32 options;

		// Token: 0x0400250C RID: 9484
		private static readonly int optionsDottedBorder = BitVector32.CreateMask();

		// Token: 0x0400250D RID: 9485
		private static readonly int optionsDottedGrip = BitVector32.CreateMask(ToolStripHighContrastRenderer.optionsDottedBorder);

		// Token: 0x0400250E RID: 9486
		private static readonly int optionsFillWhenSelected = BitVector32.CreateMask(ToolStripHighContrastRenderer.optionsDottedGrip);
	}
}
