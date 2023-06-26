using System;
using System.Drawing;
using System.Windows.Forms.ButtonInternal;

namespace System.Windows.Forms.PropertyGridInternal
{
	// Token: 0x020004FE RID: 1278
	internal class DropDownButtonAdapter : ButtonStandardAdapter
	{
		// Token: 0x060053BE RID: 21438 RVA: 0x0015EAF5 File Offset: 0x0015CCF5
		internal DropDownButtonAdapter(ButtonBase control)
			: base(control)
		{
		}

		// Token: 0x060053BF RID: 21439 RVA: 0x0015EB00 File Offset: 0x0015CD00
		private void DDB_Draw3DBorder(Graphics g, Rectangle r, bool raised)
		{
			if (base.Control.BackColor != SystemColors.Control && SystemInformation.HighContrast)
			{
				if (raised)
				{
					Color color = ControlPaint.LightLight(base.Control.BackColor);
					ControlPaint.DrawBorder(g, r, color, 1, ButtonBorderStyle.Outset, color, 1, ButtonBorderStyle.Outset, color, 2, ButtonBorderStyle.Inset, color, 2, ButtonBorderStyle.Inset);
					return;
				}
				ControlPaint.DrawBorder(g, r, ControlPaint.Dark(base.Control.BackColor), ButtonBorderStyle.Solid);
				return;
			}
			else
			{
				if (raised)
				{
					Color color2 = ControlPaint.Light(base.Control.BackColor);
					ControlPaint.DrawBorder(g, r, color2, 1, ButtonBorderStyle.Solid, color2, 1, ButtonBorderStyle.Solid, base.Control.BackColor, 2, ButtonBorderStyle.Outset, base.Control.BackColor, 2, ButtonBorderStyle.Outset);
					Rectangle rectangle = r;
					rectangle.Offset(1, 1);
					rectangle.Width -= 3;
					rectangle.Height -= 3;
					color2 = ControlPaint.LightLight(base.Control.BackColor);
					ControlPaint.DrawBorder(g, rectangle, color2, 1, ButtonBorderStyle.Solid, color2, 1, ButtonBorderStyle.Solid, color2, 1, ButtonBorderStyle.None, color2, 1, ButtonBorderStyle.None);
					return;
				}
				ControlPaint.DrawBorder(g, r, ControlPaint.Dark(base.Control.BackColor), ButtonBorderStyle.Solid);
				return;
			}
		}

		// Token: 0x060053C0 RID: 21440 RVA: 0x0015EC10 File Offset: 0x0015CE10
		internal override void PaintUp(PaintEventArgs pevent, CheckState state)
		{
			base.PaintUp(pevent, state);
			if (!Application.RenderWithVisualStyles)
			{
				this.DDB_Draw3DBorder(pevent.Graphics, base.Control.ClientRectangle, true);
				return;
			}
			Color window = SystemColors.Window;
			Rectangle clientRectangle = base.Control.ClientRectangle;
			clientRectangle.Inflate(0, -1);
			ControlPaint.DrawBorder(pevent.Graphics, clientRectangle, window, 1, ButtonBorderStyle.None, window, 1, ButtonBorderStyle.None, window, 1, ButtonBorderStyle.Solid, window, 1, ButtonBorderStyle.None);
		}

		// Token: 0x060053C1 RID: 21441 RVA: 0x0015EC78 File Offset: 0x0015CE78
		internal override void DrawImageCore(Graphics graphics, Image image, Rectangle imageBounds, Point imageStart, ButtonBaseAdapter.LayoutData layout)
		{
			if (AccessibilityImprovements.Level3 && base.IsFilledWithHighlightColor && (base.Control.MouseIsOver || base.Control.Focused))
			{
				ControlPaint.DrawImageReplaceColor(graphics, image, imageBounds, Color.Black, SystemColors.HighlightText);
				return;
			}
			ControlPaint.DrawImageReplaceColor(graphics, image, imageBounds, Color.Black, base.Control.ForeColor);
		}
	}
}
