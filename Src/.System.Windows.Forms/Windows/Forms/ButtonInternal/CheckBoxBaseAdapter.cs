using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.Internal;

namespace System.Windows.Forms.ButtonInternal
{
	// Token: 0x020004BD RID: 1213
	internal abstract class CheckBoxBaseAdapter : CheckableControlBaseAdapter
	{
		// Token: 0x06004FCE RID: 20430 RVA: 0x0014AA4E File Offset: 0x00148C4E
		internal CheckBoxBaseAdapter(ButtonBase control)
			: base(control)
		{
		}

		// Token: 0x1700137D RID: 4989
		// (get) Token: 0x06004FCF RID: 20431 RVA: 0x0014AA57 File Offset: 0x00148C57
		protected new CheckBox Control
		{
			get
			{
				return (CheckBox)base.Control;
			}
		}

		// Token: 0x06004FD0 RID: 20432 RVA: 0x0014AA64 File Offset: 0x00148C64
		protected void DrawCheckFlat(PaintEventArgs e, ButtonBaseAdapter.LayoutData layout, Color checkColor, Color checkBackground, Color checkBorder, ButtonBaseAdapter.ColorData colors)
		{
			Rectangle checkBounds = layout.checkBounds;
			if (!layout.options.everettButtonCompat)
			{
				int num = checkBounds.Width;
				checkBounds.Width = num - 1;
				num = checkBounds.Height;
				checkBounds.Height = num - 1;
			}
			using (WindowsGraphics windowsGraphics = WindowsGraphics.FromGraphics(e.Graphics))
			{
				using (WindowsPen windowsPen = new WindowsPen(windowsGraphics.DeviceContext, checkBorder))
				{
					windowsGraphics.DrawRectangle(windowsPen, checkBounds);
				}
				if (layout.options.everettButtonCompat)
				{
					int num = checkBounds.Width;
					checkBounds.Width = num - 1;
					num = checkBounds.Height;
					checkBounds.Height = num - 1;
				}
				checkBounds.Inflate(-1, -1);
			}
			if (this.Control.CheckState == CheckState.Indeterminate)
			{
				int num = checkBounds.Width;
				checkBounds.Width = num + 1;
				num = checkBounds.Height;
				checkBounds.Height = num + 1;
				ButtonBaseAdapter.DrawDitheredFill(e.Graphics, colors.buttonFace, checkBackground, checkBounds);
			}
			else
			{
				using (WindowsGraphics windowsGraphics2 = WindowsGraphics.FromGraphics(e.Graphics))
				{
					using (WindowsBrush windowsBrush = new WindowsSolidBrush(windowsGraphics2.DeviceContext, checkBackground))
					{
						int num = checkBounds.Width;
						checkBounds.Width = num + 1;
						num = checkBounds.Height;
						checkBounds.Height = num + 1;
						windowsGraphics2.FillRectangle(windowsBrush, checkBounds);
					}
				}
			}
			this.DrawCheckOnly(e, layout, colors, checkColor, checkBackground);
		}

		// Token: 0x06004FD1 RID: 20433 RVA: 0x0014AC04 File Offset: 0x00148E04
		internal static void DrawCheckBackground(bool controlEnabled, CheckState controlCheckState, Graphics g, Rectangle bounds, Color checkColor, Color checkBackground, bool disabledColors, ButtonBaseAdapter.ColorData colors)
		{
			using (WindowsGraphics windowsGraphics = WindowsGraphics.FromGraphics(g))
			{
				WindowsBrush windowsBrush;
				if (!controlEnabled && disabledColors)
				{
					windowsBrush = new WindowsSolidBrush(windowsGraphics.DeviceContext, SystemColors.Control);
				}
				else if (controlCheckState == CheckState.Indeterminate && checkBackground == SystemColors.Window && disabledColors)
				{
					Color color = (SystemInformation.HighContrast ? SystemColors.ControlDark : SystemColors.Control);
					byte b = (color.R + SystemColors.Window.R) / 2;
					byte b2 = (color.G + SystemColors.Window.G) / 2;
					byte b3 = (color.B + SystemColors.Window.B) / 2;
					windowsBrush = new WindowsSolidBrush(windowsGraphics.DeviceContext, Color.FromArgb((int)b, (int)b2, (int)b3));
				}
				else
				{
					windowsBrush = new WindowsSolidBrush(windowsGraphics.DeviceContext, checkBackground);
				}
				try
				{
					windowsGraphics.FillRectangle(windowsBrush, bounds);
				}
				finally
				{
					if (windowsBrush != null)
					{
						windowsBrush.Dispose();
					}
				}
			}
		}

		// Token: 0x06004FD2 RID: 20434 RVA: 0x0014AD14 File Offset: 0x00148F14
		protected void DrawCheckBackground(PaintEventArgs e, Rectangle bounds, Color checkColor, Color checkBackground, bool disabledColors, ButtonBaseAdapter.ColorData colors)
		{
			if (this.Control.CheckState == CheckState.Indeterminate)
			{
				ButtonBaseAdapter.DrawDitheredFill(e.Graphics, colors.buttonFace, checkBackground, bounds);
				return;
			}
			CheckBoxBaseAdapter.DrawCheckBackground(this.Control.Enabled, this.Control.CheckState, e.Graphics, bounds, checkColor, checkBackground, disabledColors, colors);
		}

		// Token: 0x06004FD3 RID: 20435 RVA: 0x0014AD70 File Offset: 0x00148F70
		protected void DrawCheckOnly(PaintEventArgs e, ButtonBaseAdapter.LayoutData layout, ButtonBaseAdapter.ColorData colors, Color checkColor, Color checkBackground)
		{
			CheckBoxBaseAdapter.DrawCheckOnly(11, this.Control.Checked, this.Control.Enabled, this.Control.CheckState, e.Graphics, layout, colors, checkColor, checkBackground);
		}

		// Token: 0x06004FD4 RID: 20436 RVA: 0x0014ADB4 File Offset: 0x00148FB4
		internal static void DrawCheckOnly(int checkSize, bool controlChecked, bool controlEnabled, CheckState controlCheckState, Graphics g, ButtonBaseAdapter.LayoutData layout, ButtonBaseAdapter.ColorData colors, Color checkColor, Color checkBackground)
		{
			if (controlChecked)
			{
				if (!controlEnabled)
				{
					checkColor = colors.buttonShadow;
				}
				else if (controlCheckState == CheckState.Indeterminate)
				{
					checkColor = (SystemInformation.HighContrast ? colors.highlight : colors.buttonShadow);
				}
				Rectangle checkBounds = layout.checkBounds;
				int num;
				if (checkBounds.Width == checkSize)
				{
					num = checkBounds.Width;
					checkBounds.Width = num + 1;
					num = checkBounds.Height;
					checkBounds.Height = num + 1;
				}
				num = checkBounds.Width;
				checkBounds.Width = num + 1;
				num = checkBounds.Height;
				checkBounds.Height = num + 1;
				Bitmap bitmap;
				if (controlCheckState == CheckState.Checked)
				{
					bitmap = CheckBoxBaseAdapter.GetCheckBoxImage(checkColor, checkBounds, ref CheckBoxBaseAdapter.checkImageCheckedBackColor, ref CheckBoxBaseAdapter.checkImageChecked);
				}
				else
				{
					bitmap = CheckBoxBaseAdapter.GetCheckBoxImage(checkColor, checkBounds, ref CheckBoxBaseAdapter.checkImageIndeterminateBackColor, ref CheckBoxBaseAdapter.checkImageIndeterminate);
				}
				if (layout.options.everettButtonCompat)
				{
					checkBounds.Y--;
				}
				else
				{
					checkBounds.Y -= 2;
				}
				ControlPaint.DrawImageColorized(g, bitmap, checkBounds, checkColor);
			}
		}

		// Token: 0x06004FD5 RID: 20437 RVA: 0x0014AEB0 File Offset: 0x001490B0
		internal static Rectangle DrawPopupBorder(Graphics g, Rectangle r, ButtonBaseAdapter.ColorData colors)
		{
			using (WindowsGraphics windowsGraphics = WindowsGraphics.FromGraphics(g))
			{
				using (WindowsPen windowsPen = new WindowsPen(windowsGraphics.DeviceContext, colors.highlight))
				{
					using (WindowsPen windowsPen2 = new WindowsPen(windowsGraphics.DeviceContext, colors.buttonShadow))
					{
						using (WindowsPen windowsPen3 = new WindowsPen(windowsGraphics.DeviceContext, colors.buttonFace))
						{
							windowsGraphics.DrawLine(windowsPen, r.Right - 1, r.Top, r.Right - 1, r.Bottom);
							windowsGraphics.DrawLine(windowsPen, r.Left, r.Bottom - 1, r.Right, r.Bottom - 1);
							windowsGraphics.DrawLine(windowsPen2, r.Left, r.Top, r.Left, r.Bottom);
							windowsGraphics.DrawLine(windowsPen2, r.Left, r.Top, r.Right - 1, r.Top);
							windowsGraphics.DrawLine(windowsPen3, r.Right - 2, r.Top + 1, r.Right - 2, r.Bottom - 1);
							windowsGraphics.DrawLine(windowsPen3, r.Left + 1, r.Bottom - 2, r.Right - 1, r.Bottom - 2);
						}
					}
				}
			}
			r.Inflate(-1, -1);
			return r;
		}

		// Token: 0x06004FD6 RID: 20438 RVA: 0x0014B080 File Offset: 0x00149280
		protected ButtonState GetState()
		{
			ButtonState buttonState = ButtonState.Normal;
			if (this.Control.CheckState == CheckState.Unchecked)
			{
				buttonState |= ButtonState.Normal;
			}
			else
			{
				buttonState |= ButtonState.Checked;
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

		// Token: 0x06004FD7 RID: 20439 RVA: 0x0014B0D8 File Offset: 0x001492D8
		protected void DrawCheckBox(PaintEventArgs e, ButtonBaseAdapter.LayoutData layout)
		{
			Graphics graphics = e.Graphics;
			ButtonState state = this.GetState();
			if (this.Control.CheckState == CheckState.Indeterminate)
			{
				if (Application.RenderWithVisualStyles)
				{
					CheckBoxRenderer.DrawCheckBox(graphics, new Point(layout.checkBounds.Left, layout.checkBounds.Top), CheckBoxRenderer.ConvertFromButtonState(state, true, this.Control.MouseIsOver), this.Control.HandleInternal);
					return;
				}
				ControlPaint.DrawMixedCheckBox(graphics, layout.checkBounds, state);
				return;
			}
			else
			{
				if (Application.RenderWithVisualStyles)
				{
					CheckBoxRenderer.DrawCheckBox(graphics, new Point(layout.checkBounds.Left, layout.checkBounds.Top), CheckBoxRenderer.ConvertFromButtonState(state, false, this.Control.MouseIsOver), this.Control.HandleInternal);
					return;
				}
				ControlPaint.DrawCheckBox(graphics, layout.checkBounds, state);
				return;
			}
		}

		// Token: 0x06004FD8 RID: 20440 RVA: 0x0014B1A8 File Offset: 0x001493A8
		private static Bitmap GetCheckBoxImage(Color checkColor, Rectangle fullSize, ref Color cacheCheckColor, ref Bitmap cacheCheckImage)
		{
			if (cacheCheckImage != null && cacheCheckColor.Equals(checkColor) && cacheCheckImage.Width == fullSize.Width && cacheCheckImage.Height == fullSize.Height)
			{
				return cacheCheckImage;
			}
			if (cacheCheckImage != null)
			{
				cacheCheckImage.Dispose();
				cacheCheckImage = null;
			}
			NativeMethods.RECT rect = NativeMethods.RECT.FromXYWH(0, 0, fullSize.Width, fullSize.Height);
			Bitmap bitmap = new Bitmap(fullSize.Width, fullSize.Height);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.Clear(Color.Transparent);
			IntPtr hdc = graphics.GetHdc();
			try
			{
				SafeNativeMethods.DrawFrameControl(new HandleRef(graphics, hdc), ref rect, 2, 1);
			}
			finally
			{
				graphics.ReleaseHdcInternal(hdc);
				graphics.Dispose();
			}
			bitmap.MakeTransparent();
			cacheCheckImage = bitmap;
			cacheCheckColor = checkColor;
			return cacheCheckImage;
		}

		// Token: 0x06004FD9 RID: 20441 RVA: 0x0014B284 File Offset: 0x00149484
		protected void AdjustFocusRectangle(ButtonBaseAdapter.LayoutData layout)
		{
			if (AccessibilityImprovements.Level2 && string.IsNullOrEmpty(this.Control.Text))
			{
				layout.focus = (this.Control.AutoSize ? Rectangle.Inflate(layout.checkBounds, -2, -2) : layout.field);
			}
		}

		// Token: 0x06004FDA RID: 20442 RVA: 0x0014B2D4 File Offset: 0x001494D4
		internal override ButtonBaseAdapter.LayoutOptions CommonLayout()
		{
			ButtonBaseAdapter.LayoutOptions layoutOptions = base.CommonLayout();
			layoutOptions.checkAlign = this.Control.CheckAlign;
			layoutOptions.textOffset = false;
			layoutOptions.shadowedText = !this.Control.Enabled;
			layoutOptions.layoutRTL = RightToLeft.Yes == this.Control.RightToLeft;
			return layoutOptions;
		}

		// Token: 0x04003468 RID: 13416
		protected const int flatCheckSize = 11;

		// Token: 0x04003469 RID: 13417
		[ThreadStatic]
		private static Bitmap checkImageChecked = null;

		// Token: 0x0400346A RID: 13418
		[ThreadStatic]
		private static Color checkImageCheckedBackColor = Color.Empty;

		// Token: 0x0400346B RID: 13419
		[ThreadStatic]
		private static Bitmap checkImageIndeterminate = null;

		// Token: 0x0400346C RID: 13420
		[ThreadStatic]
		private static Color checkImageIndeterminateBackColor = Color.Empty;
	}
}
