using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace System.Windows.Forms
{
	// Token: 0x020003F5 RID: 1013
	internal class ToolStripProfessionalLowResolutionRenderer : ToolStripProfessionalRenderer
	{
		// Token: 0x17001127 RID: 4391
		// (get) Token: 0x060045BE RID: 17854 RVA: 0x00015C90 File Offset: 0x00013E90
		internal override ToolStripRenderer RendererOverride
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060045BF RID: 17855 RVA: 0x0012686B File Offset: 0x00124A6B
		protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
		{
			if (e.ToolStrip is ToolStripDropDown)
			{
				base.OnRenderToolStripBackground(e);
			}
		}

		// Token: 0x060045C0 RID: 17856 RVA: 0x00126881 File Offset: 0x00124A81
		protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
		{
			if (e.ToolStrip is MenuStrip)
			{
				return;
			}
			if (e.ToolStrip is StatusStrip)
			{
				return;
			}
			if (e.ToolStrip is ToolStripDropDown)
			{
				base.OnRenderToolStripBorder(e);
				return;
			}
			this.RenderToolStripBorderInternal(e);
		}

		// Token: 0x060045C1 RID: 17857 RVA: 0x001268BC File Offset: 0x00124ABC
		private void RenderToolStripBorderInternal(ToolStripRenderEventArgs e)
		{
			Rectangle rectangle = new Rectangle(Point.Empty, e.ToolStrip.Size);
			Graphics graphics = e.Graphics;
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
			}
		}
	}
}
