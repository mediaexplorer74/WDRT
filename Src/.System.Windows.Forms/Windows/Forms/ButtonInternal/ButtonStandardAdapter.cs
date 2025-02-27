﻿using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms.ButtonInternal
{
	// Token: 0x020004BB RID: 1211
	internal class ButtonStandardAdapter : ButtonBaseAdapter
	{
		// Token: 0x06004FBB RID: 20411 RVA: 0x0014946C File Offset: 0x0014766C
		internal ButtonStandardAdapter(ButtonBase control)
			: base(control)
		{
		}

		// Token: 0x1700137A RID: 4986
		// (get) Token: 0x06004FBC RID: 20412 RVA: 0x0014A40D File Offset: 0x0014860D
		// (set) Token: 0x06004FBD RID: 20413 RVA: 0x0014A415 File Offset: 0x00148615
		private protected bool IsFilledWithHighlightColor { protected get; private set; }

		// Token: 0x06004FBE RID: 20414 RVA: 0x0014A420 File Offset: 0x00148620
		private PushButtonState DetermineState(bool up)
		{
			PushButtonState pushButtonState = PushButtonState.Normal;
			if (!up)
			{
				pushButtonState = PushButtonState.Pressed;
			}
			else if (base.Control.MouseIsOver)
			{
				pushButtonState = PushButtonState.Hot;
			}
			else if (!base.Control.Enabled)
			{
				pushButtonState = PushButtonState.Disabled;
			}
			else if (base.Control.Focused || base.Control.IsDefault)
			{
				pushButtonState = PushButtonState.Default;
			}
			return pushButtonState;
		}

		// Token: 0x06004FBF RID: 20415 RVA: 0x0014A475 File Offset: 0x00148675
		internal override void PaintUp(PaintEventArgs e, CheckState state)
		{
			this.PaintWorker(e, true, state);
		}

		// Token: 0x06004FC0 RID: 20416 RVA: 0x0014A480 File Offset: 0x00148680
		internal override void PaintDown(PaintEventArgs e, CheckState state)
		{
			this.PaintWorker(e, false, state);
		}

		// Token: 0x06004FC1 RID: 20417 RVA: 0x0014A48B File Offset: 0x0014868B
		internal override void PaintOver(PaintEventArgs e, CheckState state)
		{
			this.PaintUp(e, state);
		}

		// Token: 0x06004FC2 RID: 20418 RVA: 0x0014A498 File Offset: 0x00148698
		private void PaintThemedButtonBackground(PaintEventArgs e, Rectangle bounds, bool up)
		{
			PushButtonState pushButtonState = this.DetermineState(up);
			if (ButtonRenderer.IsBackgroundPartiallyTransparent(pushButtonState))
			{
				ButtonRenderer.DrawParentBackground(e.Graphics, bounds, base.Control);
			}
			if (!DpiHelper.EnableDpiChangedHighDpiImprovements)
			{
				ButtonRenderer.DrawButton(e.Graphics, base.Control.ClientRectangle, false, pushButtonState);
			}
			else
			{
				ButtonRenderer.DrawButtonForHandle(e.Graphics, base.Control.ClientRectangle, false, pushButtonState, base.Control.HandleInternal);
			}
			bounds.Inflate(-ButtonBaseAdapter.buttonBorderSize, -ButtonBaseAdapter.buttonBorderSize);
			if (!base.Control.UseVisualStyleBackColor)
			{
				bool flag = false;
				bool flag2 = up && base.IsHighContrastHighlighted();
				Color color = (flag2 ? SystemColors.Highlight : base.Control.BackColor);
				if (color.A == 255 && e.HDC != IntPtr.Zero && DisplayInformation.BitsPerPixel > 8)
				{
					NativeMethods.RECT rect = new NativeMethods.RECT(bounds.X, bounds.Y, bounds.Right, bounds.Bottom);
					SafeNativeMethods.FillRect(new HandleRef(e, e.HDC), ref rect, new HandleRef(this, flag2 ? SafeNativeMethods.GetSysColorBrush(ColorTranslator.ToOle(color) & 255) : base.Control.BackColorBrush));
					flag = true;
				}
				if (!flag && color.A > 0)
				{
					if (color.A == 255)
					{
						color = e.Graphics.GetNearestColor(color);
					}
					using (Brush brush = new SolidBrush(color))
					{
						e.Graphics.FillRectangle(brush, bounds);
						this.IsFilledWithHighlightColor = color.ToArgb() == SystemColors.Highlight.ToArgb();
					}
				}
			}
			if (base.Control.BackgroundImage != null && !DisplayInformation.HighContrast)
			{
				ControlPaint.DrawBackgroundImage(e.Graphics, base.Control.BackgroundImage, Color.Transparent, base.Control.BackgroundImageLayout, base.Control.ClientRectangle, bounds, base.Control.DisplayRectangle.Location, base.Control.RightToLeft);
			}
		}

		// Token: 0x06004FC3 RID: 20419 RVA: 0x0014A6BC File Offset: 0x001488BC
		private void PaintWorker(PaintEventArgs e, bool up, CheckState state)
		{
			up = up && state == CheckState.Unchecked;
			this.IsFilledWithHighlightColor = false;
			ButtonBaseAdapter.ColorData colorData = base.PaintRender(e.Graphics).Calculate();
			ButtonBaseAdapter.LayoutData layoutData;
			if (Application.RenderWithVisualStyles)
			{
				layoutData = this.PaintLayout(e, true).Layout();
			}
			else
			{
				layoutData = this.PaintLayout(e, up).Layout();
			}
			Graphics graphics = e.Graphics;
			Button button = base.Control as Button;
			if (Application.RenderWithVisualStyles)
			{
				this.PaintThemedButtonBackground(e, base.Control.ClientRectangle, up);
			}
			else
			{
				Brush brush = null;
				if (state == CheckState.Indeterminate)
				{
					brush = ButtonBaseAdapter.CreateDitherBrush(colorData.highlight, colorData.buttonFace);
				}
				try
				{
					Rectangle clientRectangle = base.Control.ClientRectangle;
					if (up)
					{
						clientRectangle.Inflate(-2, -2);
					}
					else
					{
						clientRectangle.Inflate(-1, -1);
					}
					base.PaintButtonBackground(e, clientRectangle, brush);
				}
				finally
				{
					if (brush != null)
					{
						brush.Dispose();
						brush = null;
					}
				}
			}
			base.PaintImage(e, layoutData);
			if (Application.RenderWithVisualStyles)
			{
				layoutData.focus.Inflate(1, 1);
			}
			if (up & base.IsHighContrastHighlighted2())
			{
				Color highlightText = SystemColors.HighlightText;
				base.PaintField(e, layoutData, colorData, highlightText, false);
				if (base.Control.Focused && base.Control.ShowFocusCues)
				{
					ControlPaint.DrawHighContrastFocusRectangle(graphics, layoutData.focus, highlightText);
				}
			}
			else if (up & base.IsHighContrastHighlighted())
			{
				base.PaintField(e, layoutData, colorData, SystemColors.HighlightText, true);
			}
			else
			{
				base.PaintField(e, layoutData, colorData, colorData.windowText, true);
			}
			if (!Application.RenderWithVisualStyles)
			{
				Rectangle clientRectangle2 = base.Control.ClientRectangle;
				if (base.Control.IsDefault)
				{
					clientRectangle2.Inflate(-1, -1);
				}
				ButtonBaseAdapter.DrawDefaultBorder(graphics, clientRectangle2, colorData.windowFrame, base.Control.IsDefault);
				if (up)
				{
					base.Draw3DBorder(graphics, clientRectangle2, colorData, up);
					return;
				}
				ControlPaint.DrawBorder(graphics, clientRectangle2, colorData.buttonShadow, ButtonBorderStyle.Solid);
			}
		}

		// Token: 0x06004FC4 RID: 20420 RVA: 0x0014A8A0 File Offset: 0x00148AA0
		protected override ButtonBaseAdapter.LayoutOptions Layout(PaintEventArgs e)
		{
			return this.PaintLayout(e, false);
		}

		// Token: 0x06004FC5 RID: 20421 RVA: 0x0014A8B8 File Offset: 0x00148AB8
		private ButtonBaseAdapter.LayoutOptions PaintLayout(PaintEventArgs e, bool up)
		{
			ButtonBaseAdapter.LayoutOptions layoutOptions = this.CommonLayout();
			layoutOptions.textOffset = !up;
			layoutOptions.everettButtonCompat = !Application.RenderWithVisualStyles;
			return layoutOptions;
		}

		// Token: 0x04003464 RID: 13412
		private const int borderWidth = 2;
	}
}
