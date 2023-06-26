using System;
using System.Drawing;

namespace System.Windows.Forms.ButtonInternal
{
	// Token: 0x020004BA RID: 1210
	internal class ButtonPopupAdapter : ButtonBaseAdapter
	{
		// Token: 0x06004FB4 RID: 20404 RVA: 0x0014946C File Offset: 0x0014766C
		internal ButtonPopupAdapter(ButtonBase control)
			: base(control)
		{
		}

		// Token: 0x06004FB5 RID: 20405 RVA: 0x00149E0C File Offset: 0x0014800C
		internal override void PaintUp(PaintEventArgs e, CheckState state)
		{
			ButtonBaseAdapter.ColorData colorData = base.PaintPopupRender(e.Graphics).Calculate();
			ButtonBaseAdapter.LayoutData layoutData = this.PaintPopupLayout(e, state == CheckState.Unchecked, 1).Layout();
			Graphics graphics = e.Graphics;
			Rectangle clientRectangle = base.Control.ClientRectangle;
			if (state == CheckState.Indeterminate)
			{
				Brush brush = ButtonBaseAdapter.CreateDitherBrush(colorData.highlight, colorData.buttonFace);
				try
				{
					base.PaintButtonBackground(e, clientRectangle, brush);
					goto IL_93;
				}
				finally
				{
					brush.Dispose();
					brush = null;
				}
			}
			base.Control.PaintBackground(e, clientRectangle, base.IsHighContrastHighlighted2() ? SystemColors.Highlight : base.Control.BackColor, clientRectangle.Location);
			IL_93:
			if (base.Control.IsDefault)
			{
				clientRectangle.Inflate(-1, -1);
			}
			base.PaintImage(e, layoutData);
			base.PaintField(e, layoutData, colorData, (state != CheckState.Indeterminate && base.IsHighContrastHighlighted2()) ? SystemColors.HighlightText : colorData.windowText, true);
			Color color = (colorData.options.highContrast ? colorData.windowText : ButtonBaseAdapter.GetContrastingBorderColor(colorData.buttonShadow));
			ButtonBaseAdapter.DrawDefaultBorder(graphics, clientRectangle, color, base.Control.IsDefault);
			if (state == CheckState.Unchecked)
			{
				ButtonBaseAdapter.DrawFlatBorder(graphics, clientRectangle, color);
				return;
			}
			ButtonBaseAdapter.Draw3DLiteBorder(graphics, clientRectangle, colorData, false);
		}

		// Token: 0x06004FB6 RID: 20406 RVA: 0x00149F4C File Offset: 0x0014814C
		internal override void PaintOver(PaintEventArgs e, CheckState state)
		{
			ButtonBaseAdapter.ColorData colorData = base.PaintPopupRender(e.Graphics).Calculate();
			ButtonBaseAdapter.LayoutData layoutData = this.PaintPopupLayout(e, state == CheckState.Unchecked, SystemInformation.HighContrast ? 2 : 1).Layout();
			Graphics graphics = e.Graphics;
			Rectangle clientRectangle = base.Control.ClientRectangle;
			if (state == CheckState.Indeterminate)
			{
				Brush brush = ButtonBaseAdapter.CreateDitherBrush(colorData.highlight, colorData.buttonFace);
				try
				{
					base.PaintButtonBackground(e, clientRectangle, brush);
					goto IL_9D;
				}
				finally
				{
					brush.Dispose();
					brush = null;
				}
			}
			base.Control.PaintBackground(e, clientRectangle, base.IsHighContrastHighlighted2() ? SystemColors.Highlight : base.Control.BackColor, clientRectangle.Location);
			IL_9D:
			if (base.Control.IsDefault)
			{
				clientRectangle.Inflate(-1, -1);
			}
			base.PaintImage(e, layoutData);
			base.PaintField(e, layoutData, colorData, base.IsHighContrastHighlighted2() ? SystemColors.HighlightText : colorData.windowText, true);
			ButtonBaseAdapter.DrawDefaultBorder(graphics, clientRectangle, colorData.options.highContrast ? colorData.windowText : colorData.buttonShadow, base.Control.IsDefault);
			if (SystemInformation.HighContrast)
			{
				using (Pen pen = new Pen(colorData.windowFrame))
				{
					using (Pen pen2 = new Pen(colorData.highlight))
					{
						using (Pen pen3 = new Pen(colorData.buttonShadow))
						{
							graphics.DrawLine(pen, clientRectangle.Left + 1, clientRectangle.Top + 1, clientRectangle.Right - 2, clientRectangle.Top + 1);
							graphics.DrawLine(pen, clientRectangle.Left + 1, clientRectangle.Top + 1, clientRectangle.Left + 1, clientRectangle.Bottom - 2);
							graphics.DrawLine(pen, clientRectangle.Left, clientRectangle.Bottom - 1, clientRectangle.Right, clientRectangle.Bottom - 1);
							graphics.DrawLine(pen, clientRectangle.Right - 1, clientRectangle.Top, clientRectangle.Right - 1, clientRectangle.Bottom);
							graphics.DrawLine(pen2, clientRectangle.Left, clientRectangle.Top, clientRectangle.Right, clientRectangle.Top);
							graphics.DrawLine(pen2, clientRectangle.Left, clientRectangle.Top, clientRectangle.Left, clientRectangle.Bottom);
							graphics.DrawLine(pen3, clientRectangle.Left + 1, clientRectangle.Bottom - 2, clientRectangle.Right - 2, clientRectangle.Bottom - 2);
							graphics.DrawLine(pen3, clientRectangle.Right - 2, clientRectangle.Top + 1, clientRectangle.Right - 2, clientRectangle.Bottom - 2);
						}
					}
				}
				clientRectangle.Inflate(-2, -2);
				return;
			}
			ButtonBaseAdapter.Draw3DLiteBorder(graphics, clientRectangle, colorData, true);
		}

		// Token: 0x06004FB7 RID: 20407 RVA: 0x0014A278 File Offset: 0x00148478
		internal override void PaintDown(PaintEventArgs e, CheckState state)
		{
			ButtonBaseAdapter.ColorData colorData = base.PaintPopupRender(e.Graphics).Calculate();
			ButtonBaseAdapter.LayoutData layoutData = this.PaintPopupLayout(e, false, SystemInformation.HighContrast ? 2 : 1).Layout();
			Graphics graphics = e.Graphics;
			Rectangle clientRectangle = base.Control.ClientRectangle;
			base.PaintButtonBackground(e, clientRectangle, null);
			if (base.Control.IsDefault)
			{
				clientRectangle.Inflate(-1, -1);
			}
			clientRectangle.Inflate(-1, -1);
			base.PaintImage(e, layoutData);
			base.PaintField(e, layoutData, colorData, colorData.windowText, true);
			clientRectangle.Inflate(1, 1);
			ButtonBaseAdapter.DrawDefaultBorder(graphics, clientRectangle, colorData.options.highContrast ? colorData.windowText : colorData.windowFrame, base.Control.IsDefault);
			ControlPaint.DrawBorder(graphics, clientRectangle, colorData.options.highContrast ? colorData.windowText : ButtonBaseAdapter.GetContrastingBorderColor(colorData.buttonShadow), ButtonBorderStyle.Solid);
		}

		// Token: 0x06004FB8 RID: 20408 RVA: 0x0014A364 File Offset: 0x00148564
		protected override ButtonBaseAdapter.LayoutOptions Layout(PaintEventArgs e)
		{
			return this.PaintPopupLayout(e, false, 0);
		}

		// Token: 0x06004FB9 RID: 20409 RVA: 0x0014A37C File Offset: 0x0014857C
		internal static ButtonBaseAdapter.LayoutOptions PaintPopupLayout(Graphics g, bool up, int paintedBorder, Rectangle clientRectangle, Padding padding, bool isDefault, Font font, string text, bool enabled, ContentAlignment textAlign, RightToLeft rtl)
		{
			ButtonBaseAdapter.LayoutOptions layoutOptions = ButtonBaseAdapter.CommonLayout(clientRectangle, padding, isDefault, font, text, enabled, textAlign, rtl);
			layoutOptions.borderSize = paintedBorder;
			layoutOptions.paddingSize = 2 - paintedBorder;
			layoutOptions.hintTextUp = false;
			layoutOptions.textOffset = !up;
			layoutOptions.shadowedText = SystemInformation.HighContrast;
			return layoutOptions;
		}

		// Token: 0x06004FBA RID: 20410 RVA: 0x0014A3CC File Offset: 0x001485CC
		private ButtonBaseAdapter.LayoutOptions PaintPopupLayout(PaintEventArgs e, bool up, int paintedBorder)
		{
			ButtonBaseAdapter.LayoutOptions layoutOptions = this.CommonLayout();
			layoutOptions.borderSize = paintedBorder;
			layoutOptions.paddingSize = 2 - paintedBorder;
			layoutOptions.hintTextUp = false;
			layoutOptions.textOffset = !up;
			layoutOptions.shadowedText = SystemInformation.HighContrast;
			return layoutOptions;
		}
	}
}
