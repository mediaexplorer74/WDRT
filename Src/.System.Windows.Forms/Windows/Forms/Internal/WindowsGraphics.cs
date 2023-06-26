using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.Internal
{
	// Token: 0x020004E7 RID: 1255
	internal sealed class WindowsGraphics : MarshalByRefObject, IDisposable, IDeviceContext
	{
		// Token: 0x060051E4 RID: 20964 RVA: 0x00154194 File Offset: 0x00152394
		public WindowsGraphics(DeviceContext dc)
		{
			this.dc = dc;
			this.dc.SaveHdc();
		}

		// Token: 0x060051E5 RID: 20965 RVA: 0x001541B0 File Offset: 0x001523B0
		public static WindowsGraphics CreateMeasurementWindowsGraphics()
		{
			DeviceContext deviceContext = DeviceContext.FromCompatibleDC(IntPtr.Zero);
			return new WindowsGraphics(deviceContext)
			{
				disposeDc = true
			};
		}

		// Token: 0x060051E6 RID: 20966 RVA: 0x001541D8 File Offset: 0x001523D8
		public static WindowsGraphics CreateMeasurementWindowsGraphics(IntPtr screenDC)
		{
			DeviceContext deviceContext = DeviceContext.FromCompatibleDC(screenDC);
			return new WindowsGraphics(deviceContext)
			{
				disposeDc = true
			};
		}

		// Token: 0x060051E7 RID: 20967 RVA: 0x001541FC File Offset: 0x001523FC
		public static WindowsGraphics FromHwnd(IntPtr hWnd)
		{
			DeviceContext deviceContext = DeviceContext.FromHwnd(hWnd);
			return new WindowsGraphics(deviceContext)
			{
				disposeDc = true
			};
		}

		// Token: 0x060051E8 RID: 20968 RVA: 0x00154220 File Offset: 0x00152420
		public static WindowsGraphics FromHdc(IntPtr hDc)
		{
			DeviceContext deviceContext = DeviceContext.FromHdc(hDc);
			return new WindowsGraphics(deviceContext)
			{
				disposeDc = true
			};
		}

		// Token: 0x060051E9 RID: 20969 RVA: 0x00154244 File Offset: 0x00152444
		public static WindowsGraphics FromGraphics(Graphics g)
		{
			ApplyGraphicsProperties applyGraphicsProperties = ApplyGraphicsProperties.All;
			return WindowsGraphics.FromGraphics(g, applyGraphicsProperties);
		}

		// Token: 0x060051EA RID: 20970 RVA: 0x0015425C File Offset: 0x0015245C
		public static WindowsGraphics FromGraphics(Graphics g, ApplyGraphicsProperties properties)
		{
			WindowsRegion windowsRegion = null;
			float[] array = null;
			Region region = null;
			Matrix matrix = null;
			if ((properties & ApplyGraphicsProperties.TranslateTransform) != ApplyGraphicsProperties.None || (properties & ApplyGraphicsProperties.Clipping) != ApplyGraphicsProperties.None)
			{
				object[] array2 = g.GetContextInfo() as object[];
				if (array2 != null && array2.Length == 2)
				{
					region = array2[0] as Region;
					matrix = array2[1] as Matrix;
				}
				if (matrix != null)
				{
					if ((properties & ApplyGraphicsProperties.TranslateTransform) != ApplyGraphicsProperties.None)
					{
						array = matrix.Elements;
					}
					matrix.Dispose();
				}
				if (region != null)
				{
					if ((properties & ApplyGraphicsProperties.Clipping) != ApplyGraphicsProperties.None && !region.IsInfinite(g))
					{
						windowsRegion = WindowsRegion.FromRegion(region, g);
					}
					region.Dispose();
				}
			}
			WindowsGraphics windowsGraphics = WindowsGraphics.FromHdc(g.GetHdc());
			windowsGraphics.graphics = g;
			if (windowsRegion != null)
			{
				using (windowsRegion)
				{
					windowsGraphics.DeviceContext.IntersectClip(windowsRegion);
				}
			}
			if (array != null)
			{
				windowsGraphics.DeviceContext.TranslateTransform((int)array[4], (int)array[5]);
			}
			return windowsGraphics;
		}

		// Token: 0x060051EB RID: 20971 RVA: 0x0015433C File Offset: 0x0015253C
		~WindowsGraphics()
		{
			this.Dispose(false);
		}

		// Token: 0x170013B2 RID: 5042
		// (get) Token: 0x060051EC RID: 20972 RVA: 0x0015436C File Offset: 0x0015256C
		public DeviceContext DeviceContext
		{
			get
			{
				return this.dc;
			}
		}

		// Token: 0x060051ED RID: 20973 RVA: 0x00154374 File Offset: 0x00152574
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060051EE RID: 20974 RVA: 0x00154384 File Offset: 0x00152584
		internal void Dispose(bool disposing)
		{
			if (this.dc != null)
			{
				try
				{
					this.dc.RestoreHdc();
					if (this.disposeDc)
					{
						this.dc.Dispose(disposing);
					}
					if (this.graphics != null)
					{
						this.graphics.ReleaseHdcInternal(this.dc.Hdc);
						this.graphics = null;
					}
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
				}
				finally
				{
					this.dc = null;
				}
			}
		}

		// Token: 0x060051EF RID: 20975 RVA: 0x00154410 File Offset: 0x00152610
		public IntPtr GetHdc()
		{
			return this.dc.Hdc;
		}

		// Token: 0x060051F0 RID: 20976 RVA: 0x0015441D File Offset: 0x0015261D
		public void ReleaseHdc()
		{
			this.dc.Dispose();
		}

		// Token: 0x170013B3 RID: 5043
		// (get) Token: 0x060051F1 RID: 20977 RVA: 0x0015442A File Offset: 0x0015262A
		// (set) Token: 0x060051F2 RID: 20978 RVA: 0x00154432 File Offset: 0x00152632
		public TextPaddingOptions TextPadding
		{
			get
			{
				return this.paddingFlags;
			}
			set
			{
				if (this.paddingFlags != value)
				{
					this.paddingFlags = value;
				}
			}
		}

		// Token: 0x060051F3 RID: 20979 RVA: 0x00154444 File Offset: 0x00152644
		public void DrawPie(WindowsPen pen, Rectangle bounds, float startAngle, float sweepAngle)
		{
			HandleRef handleRef = new HandleRef(this.dc, this.dc.Hdc);
			if (pen != null)
			{
				IntUnsafeNativeMethods.SelectObject(handleRef, new HandleRef(pen, pen.HPen));
			}
			int num = Math.Min(bounds.Width, bounds.Height);
			Point point = new Point(bounds.X + num / 2, bounds.Y + num / 2);
			int num2 = num / 2;
			IntUnsafeNativeMethods.BeginPath(handleRef);
			IntUnsafeNativeMethods.MoveToEx(handleRef, point.X, point.Y, null);
			IntUnsafeNativeMethods.AngleArc(handleRef, point.X, point.Y, num2, startAngle, sweepAngle);
			IntUnsafeNativeMethods.LineTo(handleRef, point.X, point.Y);
			IntUnsafeNativeMethods.EndPath(handleRef);
			IntUnsafeNativeMethods.StrokePath(handleRef);
		}

		// Token: 0x060051F4 RID: 20980 RVA: 0x00154510 File Offset: 0x00152710
		private void DrawEllipse(WindowsPen pen, WindowsBrush brush, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect)
		{
			HandleRef handleRef = new HandleRef(this.dc, this.dc.Hdc);
			if (pen != null)
			{
				IntUnsafeNativeMethods.SelectObject(handleRef, new HandleRef(pen, pen.HPen));
			}
			if (brush != null)
			{
				IntUnsafeNativeMethods.SelectObject(handleRef, new HandleRef(brush, brush.HBrush));
			}
			IntUnsafeNativeMethods.Ellipse(handleRef, nLeftRect, nTopRect, nRightRect, nBottomRect);
		}

		// Token: 0x060051F5 RID: 20981 RVA: 0x0015456F File Offset: 0x0015276F
		public void DrawAndFillEllipse(WindowsPen pen, WindowsBrush brush, Rectangle bounds)
		{
			this.DrawEllipse(pen, brush, bounds.Left, bounds.Top, bounds.Right, bounds.Bottom);
		}

		// Token: 0x060051F6 RID: 20982 RVA: 0x00154595 File Offset: 0x00152795
		public void DrawText(string text, WindowsFont font, Point pt, Color foreColor)
		{
			this.DrawText(text, font, pt, foreColor, Color.Empty, IntTextFormatFlags.Default);
		}

		// Token: 0x060051F7 RID: 20983 RVA: 0x001545A8 File Offset: 0x001527A8
		public void DrawText(string text, WindowsFont font, Point pt, Color foreColor, Color backColor)
		{
			this.DrawText(text, font, pt, foreColor, backColor, IntTextFormatFlags.Default);
		}

		// Token: 0x060051F8 RID: 20984 RVA: 0x001545B8 File Offset: 0x001527B8
		public void DrawText(string text, WindowsFont font, Point pt, Color foreColor, IntTextFormatFlags flags)
		{
			this.DrawText(text, font, pt, foreColor, Color.Empty, flags);
		}

		// Token: 0x060051F9 RID: 20985 RVA: 0x001545CC File Offset: 0x001527CC
		public void DrawText(string text, WindowsFont font, Point pt, Color foreColor, Color backColor, IntTextFormatFlags flags)
		{
			Rectangle rectangle = new Rectangle(pt.X, pt.Y, int.MaxValue, int.MaxValue);
			this.DrawText(text, font, rectangle, foreColor, backColor, flags);
		}

		// Token: 0x060051FA RID: 20986 RVA: 0x00154607 File Offset: 0x00152807
		public void DrawText(string text, WindowsFont font, Rectangle bounds, Color foreColor)
		{
			this.DrawText(text, font, bounds, foreColor, Color.Empty);
		}

		// Token: 0x060051FB RID: 20987 RVA: 0x00154619 File Offset: 0x00152819
		public void DrawText(string text, WindowsFont font, Rectangle bounds, Color foreColor, Color backColor)
		{
			this.DrawText(text, font, bounds, foreColor, backColor, IntTextFormatFlags.HorizontalCenter | IntTextFormatFlags.VerticalCenter);
		}

		// Token: 0x060051FC RID: 20988 RVA: 0x00154629 File Offset: 0x00152829
		public void DrawText(string text, WindowsFont font, Rectangle bounds, Color color, IntTextFormatFlags flags)
		{
			this.DrawText(text, font, bounds, color, Color.Empty, flags);
		}

		// Token: 0x060051FD RID: 20989 RVA: 0x00154640 File Offset: 0x00152840
		public void DrawText(string text, WindowsFont font, Rectangle bounds, Color foreColor, Color backColor, IntTextFormatFlags flags)
		{
			if (string.IsNullOrEmpty(text) || foreColor == Color.Transparent)
			{
				return;
			}
			HandleRef handleRef = new HandleRef(this.dc, this.dc.Hdc);
			if (this.dc.TextAlignment != DeviceContextTextAlignment.Top)
			{
				this.dc.SetTextAlignment(DeviceContextTextAlignment.Top);
			}
			if (!foreColor.IsEmpty && foreColor != this.dc.TextColor)
			{
				this.dc.SetTextColor(foreColor);
			}
			if (font != null)
			{
				this.dc.SelectFont(font);
			}
			DeviceContextBackgroundMode deviceContextBackgroundMode = ((backColor.IsEmpty || backColor == Color.Transparent) ? DeviceContextBackgroundMode.Transparent : DeviceContextBackgroundMode.Opaque);
			if (this.dc.BackgroundMode != deviceContextBackgroundMode)
			{
				this.dc.SetBackgroundMode(deviceContextBackgroundMode);
			}
			if (deviceContextBackgroundMode != DeviceContextBackgroundMode.Transparent && backColor != this.dc.BackgroundColor)
			{
				this.dc.SetBackgroundColor(backColor);
			}
			IntNativeMethods.DRAWTEXTPARAMS textMargins = this.GetTextMargins(font);
			bounds = WindowsGraphics.AdjustForVerticalAlignment(handleRef, text, bounds, flags, textMargins);
			if (bounds.Width == WindowsGraphics.MaxSize.Width)
			{
				bounds.Width -= bounds.X;
			}
			if (bounds.Height == WindowsGraphics.MaxSize.Height)
			{
				bounds.Height -= bounds.Y;
			}
			IntNativeMethods.RECT rect = new IntNativeMethods.RECT(bounds);
			IntUnsafeNativeMethods.DrawTextEx(handleRef, text, ref rect, (int)flags, textMargins);
		}

		// Token: 0x060051FE RID: 20990 RVA: 0x001547B4 File Offset: 0x001529B4
		public Color GetNearestColor(Color color)
		{
			HandleRef handleRef = new HandleRef(null, this.dc.Hdc);
			int nearestColor = IntUnsafeNativeMethods.GetNearestColor(handleRef, ColorTranslator.ToWin32(color));
			return ColorTranslator.FromWin32(nearestColor);
		}

		// Token: 0x060051FF RID: 20991 RVA: 0x001547E8 File Offset: 0x001529E8
		public float GetOverhangPadding(WindowsFont font)
		{
			WindowsFont windowsFont = font;
			if (windowsFont == null)
			{
				windowsFont = this.dc.Font;
			}
			float num = (float)windowsFont.Height / 6f;
			if (windowsFont != font)
			{
				windowsFont.Dispose();
			}
			return num;
		}

		// Token: 0x06005200 RID: 20992 RVA: 0x00154820 File Offset: 0x00152A20
		public IntNativeMethods.DRAWTEXTPARAMS GetTextMargins(WindowsFont font)
		{
			int num = 0;
			int num2 = 0;
			switch (this.TextPadding)
			{
			case TextPaddingOptions.GlyphOverhangPadding:
			{
				float num3 = this.GetOverhangPadding(font);
				num = (int)Math.Ceiling((double)num3);
				num2 = (int)Math.Ceiling((double)(num3 * 1.5f));
				break;
			}
			case TextPaddingOptions.LeftAndRightPadding:
			{
				float num3 = this.GetOverhangPadding(font);
				num = (int)Math.Ceiling((double)(2f * num3));
				num2 = (int)Math.Ceiling((double)(num3 * 2.5f));
				break;
			}
			}
			return new IntNativeMethods.DRAWTEXTPARAMS(num, num2);
		}

		// Token: 0x06005201 RID: 20993 RVA: 0x001548A4 File Offset: 0x00152AA4
		public Size GetTextExtent(string text, WindowsFont font)
		{
			if (string.IsNullOrEmpty(text))
			{
				return Size.Empty;
			}
			IntNativeMethods.SIZE size = new IntNativeMethods.SIZE();
			HandleRef handleRef = new HandleRef(null, this.dc.Hdc);
			if (font != null)
			{
				this.dc.SelectFont(font);
			}
			IntUnsafeNativeMethods.GetTextExtentPoint32(handleRef, text, size);
			if (font != null && !MeasurementDCInfo.IsMeasurementDC(this.dc))
			{
				this.dc.ResetFont();
			}
			return new Size(size.cx, size.cy);
		}

		// Token: 0x06005202 RID: 20994 RVA: 0x0015491D File Offset: 0x00152B1D
		public Size MeasureText(string text, WindowsFont font)
		{
			return this.MeasureText(text, font, WindowsGraphics.MaxSize, IntTextFormatFlags.Default);
		}

		// Token: 0x06005203 RID: 20995 RVA: 0x0015492D File Offset: 0x00152B2D
		public Size MeasureText(string text, WindowsFont font, Size proposedSize)
		{
			return this.MeasureText(text, font, proposedSize, IntTextFormatFlags.Default);
		}

		// Token: 0x06005204 RID: 20996 RVA: 0x0015493C File Offset: 0x00152B3C
		public Size MeasureText(string text, WindowsFont font, Size proposedSize, IntTextFormatFlags flags)
		{
			if (string.IsNullOrEmpty(text))
			{
				return Size.Empty;
			}
			IntNativeMethods.DRAWTEXTPARAMS drawtextparams = null;
			if (MeasurementDCInfo.IsMeasurementDC(this.DeviceContext))
			{
				drawtextparams = MeasurementDCInfo.GetTextMargins(this, font);
			}
			if (drawtextparams == null)
			{
				drawtextparams = this.GetTextMargins(font);
			}
			int num = 1 + drawtextparams.iLeftMargin + drawtextparams.iRightMargin;
			if (proposedSize.Width <= num)
			{
				proposedSize.Width = num;
			}
			if (proposedSize.Height <= 0)
			{
				proposedSize.Height = 1;
			}
			IntNativeMethods.RECT rect = IntNativeMethods.RECT.FromXYWH(0, 0, proposedSize.Width, proposedSize.Height);
			HandleRef handleRef = new HandleRef(null, this.dc.Hdc);
			if (font != null)
			{
				this.dc.SelectFont(font);
			}
			if (proposedSize.Height >= WindowsGraphics.MaxSize.Height && (flags & IntTextFormatFlags.SingleLine) != IntTextFormatFlags.Default)
			{
				flags &= ~(IntTextFormatFlags.Bottom | IntTextFormatFlags.VerticalCenter);
			}
			if (proposedSize.Width == WindowsGraphics.MaxSize.Width)
			{
				flags &= ~IntTextFormatFlags.WordBreak;
			}
			flags |= IntTextFormatFlags.CalculateRectangle;
			IntUnsafeNativeMethods.DrawTextEx(handleRef, text, ref rect, (int)flags, drawtextparams);
			return rect.Size;
		}

		// Token: 0x06005205 RID: 20997 RVA: 0x00154A48 File Offset: 0x00152C48
		public static Rectangle AdjustForVerticalAlignment(HandleRef hdc, string text, Rectangle bounds, IntTextFormatFlags flags, IntNativeMethods.DRAWTEXTPARAMS dtparams)
		{
			if (((flags & IntTextFormatFlags.Bottom) == IntTextFormatFlags.Default && (flags & IntTextFormatFlags.VerticalCenter) == IntTextFormatFlags.Default) || (flags & IntTextFormatFlags.SingleLine) != IntTextFormatFlags.Default || (flags & IntTextFormatFlags.CalculateRectangle) != IntTextFormatFlags.Default)
			{
				return bounds;
			}
			IntNativeMethods.RECT rect = new IntNativeMethods.RECT(bounds);
			flags |= IntTextFormatFlags.CalculateRectangle;
			int num = IntUnsafeNativeMethods.DrawTextEx(hdc, text, ref rect, (int)flags, dtparams);
			if (num > bounds.Height)
			{
				return bounds;
			}
			Rectangle rectangle = bounds;
			if ((flags & IntTextFormatFlags.VerticalCenter) != IntTextFormatFlags.Default)
			{
				rectangle.Y = rectangle.Top + rectangle.Height / 2 - num / 2;
			}
			else
			{
				rectangle.Y = rectangle.Bottom - num;
			}
			return rectangle;
		}

		// Token: 0x06005206 RID: 20998 RVA: 0x00154AD8 File Offset: 0x00152CD8
		public void DrawRectangle(WindowsPen pen, Rectangle rect)
		{
			this.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
		}

		// Token: 0x06005207 RID: 20999 RVA: 0x00154B00 File Offset: 0x00152D00
		public void DrawRectangle(WindowsPen pen, int x, int y, int width, int height)
		{
			HandleRef handleRef = new HandleRef(this.dc, this.dc.Hdc);
			if (pen != null)
			{
				this.dc.SelectObject(pen.HPen, GdiObjectType.Pen);
			}
			DeviceContextBinaryRasterOperationFlags deviceContextBinaryRasterOperationFlags = this.dc.BinaryRasterOperation;
			if (deviceContextBinaryRasterOperationFlags != DeviceContextBinaryRasterOperationFlags.CopyPen)
			{
				deviceContextBinaryRasterOperationFlags = this.dc.SetRasterOperation(DeviceContextBinaryRasterOperationFlags.CopyPen);
			}
			IntUnsafeNativeMethods.SelectObject(handleRef, new HandleRef(null, IntUnsafeNativeMethods.GetStockObject(5)));
			IntUnsafeNativeMethods.Rectangle(handleRef, x, y, x + width, y + height);
			if (deviceContextBinaryRasterOperationFlags != DeviceContextBinaryRasterOperationFlags.CopyPen)
			{
				this.dc.SetRasterOperation(deviceContextBinaryRasterOperationFlags);
			}
		}

		// Token: 0x06005208 RID: 21000 RVA: 0x00154B90 File Offset: 0x00152D90
		public void FillRectangle(WindowsBrush brush, Rectangle rect)
		{
			this.FillRectangle(brush, rect.X, rect.Y, rect.Width, rect.Height);
		}

		// Token: 0x06005209 RID: 21001 RVA: 0x00154BB8 File Offset: 0x00152DB8
		public void FillRectangle(WindowsBrush brush, int x, int y, int width, int height)
		{
			HandleRef handleRef = new HandleRef(this.dc, this.dc.Hdc);
			IntPtr hbrush = brush.HBrush;
			IntNativeMethods.RECT rect = new IntNativeMethods.RECT(x, y, x + width, y + height);
			IntUnsafeNativeMethods.FillRect(handleRef, ref rect, new HandleRef(brush, hbrush));
		}

		// Token: 0x0600520A RID: 21002 RVA: 0x00154C05 File Offset: 0x00152E05
		public void DrawLine(WindowsPen pen, Point p1, Point p2)
		{
			this.DrawLine(pen, p1.X, p1.Y, p2.X, p2.Y);
		}

		// Token: 0x0600520B RID: 21003 RVA: 0x00154C2C File Offset: 0x00152E2C
		public void DrawLine(WindowsPen pen, int x1, int y1, int x2, int y2)
		{
			HandleRef handleRef = new HandleRef(this.dc, this.dc.Hdc);
			DeviceContextBinaryRasterOperationFlags deviceContextBinaryRasterOperationFlags = this.dc.BinaryRasterOperation;
			DeviceContextBackgroundMode deviceContextBackgroundMode = this.dc.BackgroundMode;
			if (deviceContextBinaryRasterOperationFlags != DeviceContextBinaryRasterOperationFlags.CopyPen)
			{
				deviceContextBinaryRasterOperationFlags = this.dc.SetRasterOperation(DeviceContextBinaryRasterOperationFlags.CopyPen);
			}
			if (deviceContextBackgroundMode != DeviceContextBackgroundMode.Transparent)
			{
				deviceContextBackgroundMode = this.dc.SetBackgroundMode(DeviceContextBackgroundMode.Transparent);
			}
			if (pen != null)
			{
				this.dc.SelectObject(pen.HPen, GdiObjectType.Pen);
			}
			IntNativeMethods.POINT point = new IntNativeMethods.POINT();
			IntUnsafeNativeMethods.MoveToEx(handleRef, x1, y1, point);
			IntUnsafeNativeMethods.LineTo(handleRef, x2, y2);
			if (deviceContextBackgroundMode != DeviceContextBackgroundMode.Transparent)
			{
				this.dc.SetBackgroundMode(deviceContextBackgroundMode);
			}
			if (deviceContextBinaryRasterOperationFlags != DeviceContextBinaryRasterOperationFlags.CopyPen)
			{
				this.dc.SetRasterOperation(deviceContextBinaryRasterOperationFlags);
			}
			IntUnsafeNativeMethods.MoveToEx(handleRef, point.x, point.y, null);
		}

		// Token: 0x0600520C RID: 21004 RVA: 0x00154CF8 File Offset: 0x00152EF8
		public IntNativeMethods.TEXTMETRIC GetTextMetrics()
		{
			IntNativeMethods.TEXTMETRIC textmetric = default(IntNativeMethods.TEXTMETRIC);
			HandleRef handleRef = new HandleRef(this.dc, this.dc.Hdc);
			DeviceContextMapMode deviceContextMapMode = this.dc.MapMode;
			bool flag = deviceContextMapMode != DeviceContextMapMode.Text;
			if (flag)
			{
				this.dc.SaveHdc();
			}
			try
			{
				if (flag)
				{
					deviceContextMapMode = this.dc.SetMapMode(DeviceContextMapMode.Text);
				}
				IntUnsafeNativeMethods.GetTextMetrics(handleRef, ref textmetric);
			}
			finally
			{
				if (flag)
				{
					this.dc.RestoreHdc();
				}
			}
			return textmetric;
		}

		// Token: 0x040035E7 RID: 13799
		private DeviceContext dc;

		// Token: 0x040035E8 RID: 13800
		private bool disposeDc;

		// Token: 0x040035E9 RID: 13801
		private Graphics graphics;

		// Token: 0x040035EA RID: 13802
		public const int GdiUnsupportedFlagMask = -16777216;

		// Token: 0x040035EB RID: 13803
		public static readonly Size MaxSize = new Size(int.MaxValue, int.MaxValue);

		// Token: 0x040035EC RID: 13804
		private const float ItalicPaddingFactor = 0.5f;

		// Token: 0x040035ED RID: 13805
		private TextPaddingOptions paddingFlags;
	}
}
