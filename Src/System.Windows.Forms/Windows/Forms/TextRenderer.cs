using System;
using System.Drawing;
using System.Windows.Forms.Internal;

namespace System.Windows.Forms
{
	/// <summary>Provides methods used to measure and render text. This class cannot be inherited.</summary>
	// Token: 0x0200044D RID: 1101
	public sealed class TextRenderer
	{
		// Token: 0x06004D3F RID: 19775 RVA: 0x00002843 File Offset: 0x00000A43
		private TextRenderer()
		{
		}

		/// <summary>Draws the specified text at the specified location using the specified device context, font, and color.</summary>
		/// <param name="dc">The device context in which to draw the text.</param>
		/// <param name="text">The text to draw.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the drawn text.</param>
		/// <param name="pt">The <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of the drawn text.</param>
		/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> to apply to the drawn text.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is <see langword="null" />.</exception>
		// Token: 0x06004D40 RID: 19776 RVA: 0x0013F088 File Offset: 0x0013D288
		public static void DrawText(IDeviceContext dc, string text, Font font, Point pt, Color foreColor)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			WindowsFontQuality windowsFontQuality = WindowsFont.WindowsFontQualityFromTextRenderingHint(dc as Graphics);
			IntPtr hdc = dc.GetHdc();
			try
			{
				using (WindowsGraphics windowsGraphics = WindowsGraphics.FromHdc(hdc))
				{
					using (WindowsFont windowsFont = WindowsGraphicsCacheManager.GetWindowsFont(font, windowsFontQuality))
					{
						windowsGraphics.DrawText(text, windowsFont, pt, foreColor);
					}
				}
			}
			finally
			{
				dc.ReleaseHdc();
			}
		}

		/// <summary>Draws the specified text at the specified location, using the specified device context, font, color, and back color.</summary>
		/// <param name="dc">The device context in which to draw the text.</param>
		/// <param name="text">The text to draw.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the drawn text.</param>
		/// <param name="pt">The <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of the drawn text.</param>
		/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> to apply to the drawn text.</param>
		/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> to apply to the background area of the drawn text.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is <see langword="null" />.</exception>
		// Token: 0x06004D41 RID: 19777 RVA: 0x0013F118 File Offset: 0x0013D318
		public static void DrawText(IDeviceContext dc, string text, Font font, Point pt, Color foreColor, Color backColor)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			WindowsFontQuality windowsFontQuality = WindowsFont.WindowsFontQualityFromTextRenderingHint(dc as Graphics);
			IntPtr hdc = dc.GetHdc();
			try
			{
				using (WindowsGraphics windowsGraphics = WindowsGraphics.FromHdc(hdc))
				{
					using (WindowsFont windowsFont = WindowsGraphicsCacheManager.GetWindowsFont(font, windowsFontQuality))
					{
						windowsGraphics.DrawText(text, windowsFont, pt, foreColor, backColor);
					}
				}
			}
			finally
			{
				dc.ReleaseHdc();
			}
		}

		/// <summary>Draws the specified text at the specified location using the specified device context, font, color, and formatting instructions.</summary>
		/// <param name="dc">The device context in which to draw the text.</param>
		/// <param name="text">The text to draw.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the drawn text.</param>
		/// <param name="pt">The <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of the drawn text.</param>
		/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> to apply to the drawn text.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is <see langword="null" />.</exception>
		// Token: 0x06004D42 RID: 19778 RVA: 0x0013F1A8 File Offset: 0x0013D3A8
		public static void DrawText(IDeviceContext dc, string text, Font font, Point pt, Color foreColor, TextFormatFlags flags)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			WindowsFontQuality windowsFontQuality = WindowsFont.WindowsFontQualityFromTextRenderingHint(dc as Graphics);
			using (WindowsGraphicsWrapper windowsGraphicsWrapper = new WindowsGraphicsWrapper(dc, flags))
			{
				using (WindowsFont windowsFont = WindowsGraphicsCacheManager.GetWindowsFont(font, windowsFontQuality))
				{
					windowsGraphicsWrapper.WindowsGraphics.DrawText(text, windowsFont, pt, foreColor, TextRenderer.GetIntTextFormatFlags(flags));
				}
			}
		}

		/// <summary>Draws the specified text at the specified location using the specified device context, font, color, back color, and formatting instructions</summary>
		/// <param name="dc">The device context in which to draw the text.</param>
		/// <param name="text">The text to draw.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the drawn text.</param>
		/// <param name="pt">The <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of the drawn text.</param>
		/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> to apply to the text.</param>
		/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> to apply to the background area of the drawn text.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is <see langword="null" />.</exception>
		// Token: 0x06004D43 RID: 19779 RVA: 0x0013F22C File Offset: 0x0013D42C
		public static void DrawText(IDeviceContext dc, string text, Font font, Point pt, Color foreColor, Color backColor, TextFormatFlags flags)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			WindowsFontQuality windowsFontQuality = WindowsFont.WindowsFontQualityFromTextRenderingHint(dc as Graphics);
			using (WindowsGraphicsWrapper windowsGraphicsWrapper = new WindowsGraphicsWrapper(dc, flags))
			{
				using (WindowsFont windowsFont = WindowsGraphicsCacheManager.GetWindowsFont(font, windowsFontQuality))
				{
					windowsGraphicsWrapper.WindowsGraphics.DrawText(text, windowsFont, pt, foreColor, backColor, TextRenderer.GetIntTextFormatFlags(flags));
				}
			}
		}

		/// <summary>Draws the specified text within the specified bounds, using the specified device context, font, and color.</summary>
		/// <param name="dc">The device context in which to draw the text.</param>
		/// <param name="text">The text to draw.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the drawn text.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the text.</param>
		/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> to apply to the drawn text.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is <see langword="null" />.</exception>
		// Token: 0x06004D44 RID: 19780 RVA: 0x0013F2B0 File Offset: 0x0013D4B0
		public static void DrawText(IDeviceContext dc, string text, Font font, Rectangle bounds, Color foreColor)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			WindowsFontQuality windowsFontQuality = WindowsFont.WindowsFontQualityFromTextRenderingHint(dc as Graphics);
			IntPtr hdc = dc.GetHdc();
			try
			{
				using (WindowsGraphics windowsGraphics = WindowsGraphics.FromHdc(hdc))
				{
					using (WindowsFont windowsFont = WindowsGraphicsCacheManager.GetWindowsFont(font, windowsFontQuality))
					{
						windowsGraphics.DrawText(text, windowsFont, bounds, foreColor);
					}
				}
			}
			finally
			{
				dc.ReleaseHdc();
			}
		}

		/// <summary>Draws the specified text within the specified bounds using the specified device context, font, color, and back color.</summary>
		/// <param name="dc">The device context in which to draw the text.</param>
		/// <param name="text">The text to draw.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the drawn text.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the text.</param>
		/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> to apply to the drawn text.</param>
		/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> to apply to the area represented by <paramref name="bounds" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is <see langword="null" />.</exception>
		// Token: 0x06004D45 RID: 19781 RVA: 0x0013F340 File Offset: 0x0013D540
		public static void DrawText(IDeviceContext dc, string text, Font font, Rectangle bounds, Color foreColor, Color backColor)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			WindowsFontQuality windowsFontQuality = WindowsFont.WindowsFontQualityFromTextRenderingHint(dc as Graphics);
			IntPtr hdc = dc.GetHdc();
			try
			{
				using (WindowsGraphics windowsGraphics = WindowsGraphics.FromHdc(hdc))
				{
					using (WindowsFont windowsFont = WindowsGraphicsCacheManager.GetWindowsFont(font, windowsFontQuality))
					{
						windowsGraphics.DrawText(text, windowsFont, bounds, foreColor, backColor);
					}
				}
			}
			finally
			{
				dc.ReleaseHdc();
			}
		}

		/// <summary>Draws the specified text within the specified bounds using the specified device context, font, color, and formatting instructions.</summary>
		/// <param name="dc">The device context in which to draw the text.</param>
		/// <param name="text">The text to draw.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the drawn text.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the text.</param>
		/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> to apply to the drawn text.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is <see langword="null" />.</exception>
		// Token: 0x06004D46 RID: 19782 RVA: 0x0013F3D0 File Offset: 0x0013D5D0
		public static void DrawText(IDeviceContext dc, string text, Font font, Rectangle bounds, Color foreColor, TextFormatFlags flags)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			WindowsFontQuality windowsFontQuality = WindowsFont.WindowsFontQualityFromTextRenderingHint(dc as Graphics);
			using (WindowsGraphicsWrapper windowsGraphicsWrapper = new WindowsGraphicsWrapper(dc, flags))
			{
				using (WindowsFont windowsFont = WindowsGraphicsCacheManager.GetWindowsFont(font, windowsFontQuality))
				{
					windowsGraphicsWrapper.WindowsGraphics.DrawText(text, windowsFont, bounds, foreColor, TextRenderer.GetIntTextFormatFlags(flags));
				}
			}
		}

		/// <summary>Draws the specified text within the specified bounds using the specified device context, font, color, back color, and formatting instructions.</summary>
		/// <param name="dc">The device context in which to draw the text.</param>
		/// <param name="text">The text to draw.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the drawn text.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the text.</param>
		/// <param name="foreColor">The <see cref="T:System.Drawing.Color" /> to apply to the text.</param>
		/// <param name="backColor">The <see cref="T:System.Drawing.Color" /> to apply to the area represented by <paramref name="bounds" />.</param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is <see langword="null" />.</exception>
		// Token: 0x06004D47 RID: 19783 RVA: 0x0013F454 File Offset: 0x0013D654
		public static void DrawText(IDeviceContext dc, string text, Font font, Rectangle bounds, Color foreColor, Color backColor, TextFormatFlags flags)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			WindowsFontQuality windowsFontQuality = WindowsFont.WindowsFontQualityFromTextRenderingHint(dc as Graphics);
			using (WindowsGraphicsWrapper windowsGraphicsWrapper = new WindowsGraphicsWrapper(dc, flags))
			{
				using (WindowsFont windowsFont = WindowsGraphicsCacheManager.GetWindowsFont(font, windowsFontQuality))
				{
					windowsGraphicsWrapper.WindowsGraphics.DrawText(text, windowsFont, bounds, foreColor, backColor, TextRenderer.GetIntTextFormatFlags(flags));
				}
			}
		}

		// Token: 0x06004D48 RID: 19784 RVA: 0x0013F4D8 File Offset: 0x0013D6D8
		private static IntTextFormatFlags GetIntTextFormatFlags(TextFormatFlags flags)
		{
			if (((ulong)flags & 18446744073692774400UL) == 0UL)
			{
				return (IntTextFormatFlags)flags;
			}
			return (IntTextFormatFlags)(flags & (TextFormatFlags)16777215);
		}

		/// <summary>Provides the size, in pixels, of the specified text when drawn with the specified font.</summary>
		/// <param name="text">The text to measure.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the measured text.</param>
		/// <returns>The <see cref="T:System.Drawing.Size" />, in pixels, of <paramref name="text" /> drawn on a single line with the specified <paramref name="font" />. You can manipulate how the text is drawn by using one of the <see cref="M:System.Windows.Forms.TextRenderer.DrawText(System.Drawing.IDeviceContext,System.String,System.Drawing.Font,System.Drawing.Rectangle,System.Drawing.Color,System.Windows.Forms.TextFormatFlags)" /> overloads that takes a <see cref="T:System.Windows.Forms.TextFormatFlags" /> parameter. For example, the default behavior of the <see cref="T:System.Windows.Forms.TextRenderer" /> is to add padding to the bounding rectangle of the drawn text to accommodate overhanging glyphs. If you need to draw a line of text without these extra spaces you should use the versions of <see cref="M:System.Windows.Forms.TextRenderer.DrawText(System.Drawing.IDeviceContext,System.String,System.Drawing.Font,System.Drawing.Point,System.Drawing.Color)" /> and <see cref="M:System.Windows.Forms.TextRenderer.MeasureText(System.Drawing.IDeviceContext,System.String,System.Drawing.Font)" /> that take a <see cref="T:System.Drawing.Size" /> and <see cref="T:System.Windows.Forms.TextFormatFlags" /> parameter. For an example, see <see cref="M:System.Windows.Forms.TextRenderer.MeasureText(System.Drawing.IDeviceContext,System.String,System.Drawing.Font,System.Drawing.Size,System.Windows.Forms.TextFormatFlags)" />.</returns>
		// Token: 0x06004D49 RID: 19785 RVA: 0x0013F4FC File Offset: 0x0013D6FC
		public static Size MeasureText(string text, Font font)
		{
			if (string.IsNullOrEmpty(text))
			{
				return Size.Empty;
			}
			Size size;
			using (WindowsFont windowsFont = WindowsGraphicsCacheManager.GetWindowsFont(font))
			{
				size = WindowsGraphicsCacheManager.MeasurementGraphics.MeasureText(text, windowsFont);
			}
			return size;
		}

		/// <summary>Provides the size, in pixels, of the specified text when drawn with the specified font, using the specified size to create an initial bounding rectangle.</summary>
		/// <param name="text">The text to measure.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the measured text.</param>
		/// <param name="proposedSize">The <see cref="T:System.Drawing.Size" /> of the initial bounding rectangle.</param>
		/// <returns>The <see cref="T:System.Drawing.Size" />, in pixels, of <paramref name="text" /> drawn with the specified <paramref name="font" />.</returns>
		// Token: 0x06004D4A RID: 19786 RVA: 0x0013F548 File Offset: 0x0013D748
		public static Size MeasureText(string text, Font font, Size proposedSize)
		{
			if (string.IsNullOrEmpty(text))
			{
				return Size.Empty;
			}
			Size size;
			using (WindowsGraphicsCacheManager.GetWindowsFont(font))
			{
				size = WindowsGraphicsCacheManager.MeasurementGraphics.MeasureText(text, WindowsGraphicsCacheManager.GetWindowsFont(font), proposedSize);
			}
			return size;
		}

		/// <summary>Provides the size, in pixels, of the specified text when drawn with the specified font and formatting instructions, using the specified size to create the initial bounding rectangle for the text.</summary>
		/// <param name="text">The text to measure.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the measured text.</param>
		/// <param name="proposedSize">The <see cref="T:System.Drawing.Size" /> of the initial bounding rectangle.</param>
		/// <param name="flags">The formatting instructions to apply to the measured text.</param>
		/// <returns>The <see cref="T:System.Drawing.Size" />, in pixels, of <paramref name="text" /> drawn with the specified <paramref name="font" /> and format.</returns>
		// Token: 0x06004D4B RID: 19787 RVA: 0x0013F59C File Offset: 0x0013D79C
		public static Size MeasureText(string text, Font font, Size proposedSize, TextFormatFlags flags)
		{
			if (string.IsNullOrEmpty(text))
			{
				return Size.Empty;
			}
			Size size;
			using (WindowsFont windowsFont = WindowsGraphicsCacheManager.GetWindowsFont(font))
			{
				size = WindowsGraphicsCacheManager.MeasurementGraphics.MeasureText(text, windowsFont, proposedSize, TextRenderer.GetIntTextFormatFlags(flags));
			}
			return size;
		}

		/// <summary>Provides the size, in pixels, of the specified text drawn with the specified font in the specified device context.</summary>
		/// <param name="dc">The device context in which to measure the text.</param>
		/// <param name="text">The text to measure.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the measured text.</param>
		/// <returns>The <see cref="T:System.Drawing.Size" />, in pixels, of <paramref name="text" /> drawn in a single line with the specified <paramref name="font" /> in the specified device context.</returns>
		// Token: 0x06004D4C RID: 19788 RVA: 0x0013F5F0 File Offset: 0x0013D7F0
		public static Size MeasureText(IDeviceContext dc, string text, Font font)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			if (string.IsNullOrEmpty(text))
			{
				return Size.Empty;
			}
			WindowsFontQuality windowsFontQuality = WindowsFont.WindowsFontQualityFromTextRenderingHint(dc as Graphics);
			IntPtr hdc = dc.GetHdc();
			Size size;
			try
			{
				using (WindowsGraphics windowsGraphics = WindowsGraphics.FromHdc(hdc))
				{
					using (WindowsFont windowsFont = WindowsGraphicsCacheManager.GetWindowsFont(font, windowsFontQuality))
					{
						size = windowsGraphics.MeasureText(text, windowsFont);
					}
				}
			}
			finally
			{
				dc.ReleaseHdc();
			}
			return size;
		}

		/// <summary>Provides the size, in pixels, of the specified text when drawn with the specified font in the specified device context, using the specified size to create an initial bounding rectangle for the text.</summary>
		/// <param name="dc">The device context in which to measure the text.</param>
		/// <param name="text">The text to measure.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the measured text.</param>
		/// <param name="proposedSize">The <see cref="T:System.Drawing.Size" /> of the initial bounding rectangle.</param>
		/// <returns>The <see cref="T:System.Drawing.Size" />, in pixels, of <paramref name="text" /> drawn with the specified <paramref name="font" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is <see langword="null" />.</exception>
		// Token: 0x06004D4D RID: 19789 RVA: 0x0013F68C File Offset: 0x0013D88C
		public static Size MeasureText(IDeviceContext dc, string text, Font font, Size proposedSize)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			if (string.IsNullOrEmpty(text))
			{
				return Size.Empty;
			}
			WindowsFontQuality windowsFontQuality = WindowsFont.WindowsFontQualityFromTextRenderingHint(dc as Graphics);
			IntPtr hdc = dc.GetHdc();
			Size size;
			try
			{
				using (WindowsGraphics windowsGraphics = WindowsGraphics.FromHdc(hdc))
				{
					using (WindowsFont windowsFont = WindowsGraphicsCacheManager.GetWindowsFont(font, windowsFontQuality))
					{
						size = windowsGraphics.MeasureText(text, windowsFont, proposedSize);
					}
				}
			}
			finally
			{
				dc.ReleaseHdc();
			}
			return size;
		}

		/// <summary>Provides the size, in pixels, of the specified text when drawn with the specified device context, font, and formatting instructions, using the specified size to create the initial bounding rectangle for the text.</summary>
		/// <param name="dc">The device context in which to measure the text.</param>
		/// <param name="text">The text to measure.</param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> to apply to the measured text.</param>
		/// <param name="proposedSize">The <see cref="T:System.Drawing.Size" /> of the initial bounding rectangle.</param>
		/// <param name="flags">The formatting instructions to apply to the measured text.</param>
		/// <returns>The <see cref="T:System.Drawing.Size" />, in pixels, of <paramref name="text" /> drawn with the specified <paramref name="font" /> and format.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dc" /> is <see langword="null" />.</exception>
		// Token: 0x06004D4E RID: 19790 RVA: 0x0013F72C File Offset: 0x0013D92C
		public static Size MeasureText(IDeviceContext dc, string text, Font font, Size proposedSize, TextFormatFlags flags)
		{
			if (dc == null)
			{
				throw new ArgumentNullException("dc");
			}
			if (string.IsNullOrEmpty(text))
			{
				return Size.Empty;
			}
			WindowsFontQuality windowsFontQuality = WindowsFont.WindowsFontQualityFromTextRenderingHint(dc as Graphics);
			Size size;
			using (WindowsGraphicsWrapper windowsGraphicsWrapper = new WindowsGraphicsWrapper(dc, flags))
			{
				using (WindowsFont windowsFont = WindowsGraphicsCacheManager.GetWindowsFont(font, windowsFontQuality))
				{
					size = windowsGraphicsWrapper.WindowsGraphics.MeasureText(text, windowsFont, proposedSize, TextRenderer.GetIntTextFormatFlags(flags));
				}
			}
			return size;
		}

		// Token: 0x06004D4F RID: 19791 RVA: 0x0013F7BC File Offset: 0x0013D9BC
		internal static Color DisabledTextColor(Color backColor)
		{
			if (SystemInformation.HighContrast && AccessibilityImprovements.Level1)
			{
				return SystemColors.GrayText;
			}
			Color color = SystemColors.ControlDark;
			if (ControlPaint.IsDarker(backColor, SystemColors.Control))
			{
				color = ControlPaint.Dark(backColor);
			}
			return color;
		}
	}
}
