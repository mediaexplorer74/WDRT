using System;
using System.Drawing;
using System.Windows.Forms.Internal;

namespace System.Windows.Forms.ButtonInternal
{
	// Token: 0x020004C1 RID: 1217
	internal abstract class RadioButtonBaseAdapter : CheckableControlBaseAdapter
	{
		// Token: 0x06004FF4 RID: 20468 RVA: 0x0014AA4E File Offset: 0x00148C4E
		internal RadioButtonBaseAdapter(ButtonBase control)
			: base(control)
		{
		}

		// Token: 0x17001380 RID: 4992
		// (get) Token: 0x06004FF5 RID: 20469 RVA: 0x0014BD20 File Offset: 0x00149F20
		protected new RadioButton Control
		{
			get
			{
				return (RadioButton)base.Control;
			}
		}

		// Token: 0x06004FF6 RID: 20470 RVA: 0x0014BD2D File Offset: 0x00149F2D
		protected void DrawCheckFlat(PaintEventArgs e, ButtonBaseAdapter.LayoutData layout, Color checkColor, Color checkBackground, Color checkBorder)
		{
			this.DrawCheckBackgroundFlat(e, layout.checkBounds, checkBorder, checkBackground);
			this.DrawCheckOnly(e, layout, checkColor, checkBackground, true);
		}

		// Token: 0x06004FF7 RID: 20471 RVA: 0x0014BD4C File Offset: 0x00149F4C
		protected void DrawCheckBackground3DLite(PaintEventArgs e, Rectangle bounds, Color checkColor, Color checkBackground, ButtonBaseAdapter.ColorData colors, bool disabledColors)
		{
			Graphics graphics = e.Graphics;
			Color color = checkBackground;
			if (!this.Control.Enabled && disabledColors)
			{
				color = SystemColors.Control;
			}
			using (Brush brush = new SolidBrush(color))
			{
				using (Pen pen = new Pen(colors.buttonShadow))
				{
					using (Pen pen2 = new Pen(colors.buttonFace))
					{
						using (Pen pen3 = new Pen(colors.highlight))
						{
							int num = bounds.Width;
							bounds.Width = num - 1;
							num = bounds.Height;
							bounds.Height = num - 1;
							graphics.DrawPie(pen, bounds, 136f, 88f);
							graphics.DrawPie(pen, bounds, 226f, 88f);
							graphics.DrawPie(pen3, bounds, 316f, 88f);
							graphics.DrawPie(pen3, bounds, 46f, 88f);
							bounds.Inflate(-1, -1);
							graphics.FillEllipse(brush, bounds);
							graphics.DrawEllipse(pen2, bounds);
						}
					}
				}
			}
		}

		// Token: 0x06004FF8 RID: 20472 RVA: 0x0014BE98 File Offset: 0x0014A098
		protected void DrawCheckBackgroundFlat(PaintEventArgs e, Rectangle bounds, Color borderColor, Color checkBackground)
		{
			Color color = checkBackground;
			Color color2 = borderColor;
			if (!this.Control.Enabled)
			{
				if (!SystemInformation.HighContrast || !AccessibilityImprovements.Level1)
				{
					color2 = ControlPaint.ContrastControlDark;
				}
				color = SystemColors.Control;
			}
			double dpiScaleRatio = base.GetDpiScaleRatio(e.Graphics);
			using (WindowsGraphics windowsGraphics = WindowsGraphics.FromGraphics(e.Graphics))
			{
				using (WindowsPen windowsPen = new WindowsPen(windowsGraphics.DeviceContext, color2))
				{
					using (WindowsBrush windowsBrush = new WindowsSolidBrush(windowsGraphics.DeviceContext, color))
					{
						if (dpiScaleRatio > 1.1)
						{
							int num = bounds.Width;
							bounds.Width = num - 1;
							num = bounds.Height;
							bounds.Height = num - 1;
							windowsGraphics.DrawAndFillEllipse(windowsPen, windowsBrush, bounds);
							bounds.Inflate(-1, -1);
						}
						else
						{
							RadioButtonBaseAdapter.DrawAndFillEllipse(windowsGraphics, windowsPen, windowsBrush, bounds);
						}
					}
				}
			}
		}

		// Token: 0x06004FF9 RID: 20473 RVA: 0x0014BFA8 File Offset: 0x0014A1A8
		private static void DrawAndFillEllipse(WindowsGraphics wg, WindowsPen borderPen, WindowsBrush fieldBrush, Rectangle bounds)
		{
			if (wg == null)
			{
				return;
			}
			wg.FillRectangle(fieldBrush, new Rectangle(bounds.X + 2, bounds.Y + 2, 8, 8));
			wg.FillRectangle(fieldBrush, new Rectangle(bounds.X + 4, bounds.Y + 1, 4, 10));
			wg.FillRectangle(fieldBrush, new Rectangle(bounds.X + 1, bounds.Y + 4, 10, 4));
			wg.DrawLine(borderPen, new Point(bounds.X + 4, bounds.Y), new Point(bounds.X + 8, bounds.Y));
			wg.DrawLine(borderPen, new Point(bounds.X + 4, bounds.Y + 11), new Point(bounds.X + 8, bounds.Y + 11));
			wg.DrawLine(borderPen, new Point(bounds.X + 2, bounds.Y + 1), new Point(bounds.X + 4, bounds.Y + 1));
			wg.DrawLine(borderPen, new Point(bounds.X + 8, bounds.Y + 1), new Point(bounds.X + 10, bounds.Y + 1));
			wg.DrawLine(borderPen, new Point(bounds.X + 2, bounds.Y + 10), new Point(bounds.X + 4, bounds.Y + 10));
			wg.DrawLine(borderPen, new Point(bounds.X + 8, bounds.Y + 10), new Point(bounds.X + 10, bounds.Y + 10));
			wg.DrawLine(borderPen, new Point(bounds.X, bounds.Y + 4), new Point(bounds.X, bounds.Y + 8));
			wg.DrawLine(borderPen, new Point(bounds.X + 11, bounds.Y + 4), new Point(bounds.X + 11, bounds.Y + 8));
			wg.DrawLine(borderPen, new Point(bounds.X + 1, bounds.Y + 2), new Point(bounds.X + 1, bounds.Y + 4));
			wg.DrawLine(borderPen, new Point(bounds.X + 1, bounds.Y + 8), new Point(bounds.X + 1, bounds.Y + 10));
			wg.DrawLine(borderPen, new Point(bounds.X + 10, bounds.Y + 2), new Point(bounds.X + 10, bounds.Y + 4));
			wg.DrawLine(borderPen, new Point(bounds.X + 10, bounds.Y + 8), new Point(bounds.X + 10, bounds.Y + 10));
		}

		// Token: 0x06004FFA RID: 20474 RVA: 0x0014C29F File Offset: 0x0014A49F
		private static int GetScaledNumber(int n, double scale)
		{
			return (int)((double)n * scale);
		}

		// Token: 0x06004FFB RID: 20475 RVA: 0x0014C2A8 File Offset: 0x0014A4A8
		protected void DrawCheckOnly(PaintEventArgs e, ButtonBaseAdapter.LayoutData layout, Color checkColor, Color checkBackground, bool disabledColors)
		{
			if (this.Control.Checked)
			{
				if (!this.Control.Enabled && disabledColors)
				{
					checkColor = SystemColors.ControlDark;
				}
				double dpiScaleRatio = base.GetDpiScaleRatio(e.Graphics);
				using (WindowsGraphics windowsGraphics = WindowsGraphics.FromGraphics(e.Graphics))
				{
					using (WindowsBrush windowsBrush = new WindowsSolidBrush(windowsGraphics.DeviceContext, checkColor))
					{
						int num = 5;
						Rectangle rectangle = new Rectangle(layout.checkBounds.X + RadioButtonBaseAdapter.GetScaledNumber(num, dpiScaleRatio), layout.checkBounds.Y + RadioButtonBaseAdapter.GetScaledNumber(num - 1, dpiScaleRatio), RadioButtonBaseAdapter.GetScaledNumber(2, dpiScaleRatio), RadioButtonBaseAdapter.GetScaledNumber(4, dpiScaleRatio));
						windowsGraphics.FillRectangle(windowsBrush, rectangle);
						Rectangle rectangle2 = new Rectangle(layout.checkBounds.X + RadioButtonBaseAdapter.GetScaledNumber(num - 1, dpiScaleRatio), layout.checkBounds.Y + RadioButtonBaseAdapter.GetScaledNumber(num, dpiScaleRatio), RadioButtonBaseAdapter.GetScaledNumber(4, dpiScaleRatio), RadioButtonBaseAdapter.GetScaledNumber(2, dpiScaleRatio));
						windowsGraphics.FillRectangle(windowsBrush, rectangle2);
					}
				}
			}
		}

		// Token: 0x06004FFC RID: 20476 RVA: 0x0014C3C8 File Offset: 0x0014A5C8
		protected ButtonState GetState()
		{
			ButtonState buttonState = ButtonState.Normal;
			if (this.Control.Checked)
			{
				buttonState |= ButtonState.Checked;
			}
			else
			{
				buttonState |= ButtonState.Normal;
			}
			if (!this.Control.Enabled)
			{
				buttonState |= ButtonState.Inactive;
			}
			if (this.Control.MouseIsDown)
			{
				buttonState |= ButtonState.Pushed;
			}
			return buttonState;
		}

		// Token: 0x06004FFD RID: 20477 RVA: 0x0014C420 File Offset: 0x0014A620
		protected void DrawCheckBox(PaintEventArgs e, ButtonBaseAdapter.LayoutData layout)
		{
			Graphics graphics = e.Graphics;
			Rectangle checkBounds = layout.checkBounds;
			if (!Application.RenderWithVisualStyles)
			{
				int x = checkBounds.X;
				checkBounds.X = x - 1;
			}
			ButtonState state = this.GetState();
			if (Application.RenderWithVisualStyles)
			{
				RadioButtonRenderer.DrawRadioButton(graphics, new Point(checkBounds.Left, checkBounds.Top), RadioButtonRenderer.ConvertFromButtonState(state, this.Control.MouseIsOver), this.Control.HandleInternal);
				return;
			}
			ControlPaint.DrawRadioButton(graphics, checkBounds, state);
		}

		// Token: 0x06004FFE RID: 20478 RVA: 0x0014C49F File Offset: 0x0014A69F
		protected void AdjustFocusRectangle(ButtonBaseAdapter.LayoutData layout)
		{
			if (AccessibilityImprovements.Level2 && string.IsNullOrEmpty(this.Control.Text))
			{
				layout.focus = (this.Control.AutoSize ? layout.checkBounds : layout.field);
			}
		}

		// Token: 0x06004FFF RID: 20479 RVA: 0x0014C4DC File Offset: 0x0014A6DC
		internal override ButtonBaseAdapter.LayoutOptions CommonLayout()
		{
			ButtonBaseAdapter.LayoutOptions layoutOptions = base.CommonLayout();
			layoutOptions.checkAlign = this.Control.CheckAlign;
			return layoutOptions;
		}
	}
}
