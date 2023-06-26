using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Microsoft.Win32;

namespace System.Windows.Forms
{
	/// <summary>Provides information about the current system environment.</summary>
	// Token: 0x02000381 RID: 897
	public class SystemInformation
	{
		// Token: 0x06003A99 RID: 15001 RVA: 0x00002843 File Offset: 0x00000A43
		private SystemInformation()
		{
		}

		/// <summary>Gets a value indicating whether the user has enabled full window drag.</summary>
		/// <returns>
		///   <see langword="true" /> if the user has enabled full window drag; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x06003A9A RID: 15002 RVA: 0x001024A0 File Offset: 0x001006A0
		public static bool DragFullWindows
		{
			get
			{
				int num = 0;
				UnsafeNativeMethods.SystemParametersInfo(38, 0, ref num, 0);
				return num != 0;
			}
		}

		/// <summary>Gets a value indicating whether the user has enabled the high-contrast mode accessibility feature.</summary>
		/// <returns>
		///   <see langword="true" /> if the user has enabled high-contrast mode; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x06003A9B RID: 15003 RVA: 0x001024C0 File Offset: 0x001006C0
		public static bool HighContrast
		{
			get
			{
				SystemInformation.EnsureSystemEvents();
				if (SystemInformation.systemEventsDirty)
				{
					NativeMethods.HIGHCONTRAST_I highcontrast_I = default(NativeMethods.HIGHCONTRAST_I);
					highcontrast_I.cbSize = Marshal.SizeOf(highcontrast_I);
					highcontrast_I.dwFlags = 0;
					highcontrast_I.lpszDefaultScheme = IntPtr.Zero;
					bool flag = UnsafeNativeMethods.SystemParametersInfo(66, highcontrast_I.cbSize, ref highcontrast_I, 0);
					if (flag)
					{
						SystemInformation.highContrast = (highcontrast_I.dwFlags & 1) != 0;
					}
					else
					{
						SystemInformation.highContrast = false;
					}
					SystemInformation.systemEventsDirty = false;
				}
				return SystemInformation.highContrast;
			}
		}

		/// <summary>Gets the number of lines to scroll when the mouse wheel is rotated.</summary>
		/// <returns>The number of lines to scroll on a mouse wheel rotation, or -1 if the "One screen at a time" mouse option is selected.</returns>
		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x06003A9C RID: 15004 RVA: 0x00102540 File Offset: 0x00100740
		public static int MouseWheelScrollLines
		{
			get
			{
				if (SystemInformation.NativeMouseWheelSupport)
				{
					int num = 0;
					UnsafeNativeMethods.SystemParametersInfo(104, 0, ref num, 0);
					return num;
				}
				IntPtr intPtr = IntPtr.Zero;
				intPtr = UnsafeNativeMethods.FindWindow("MouseZ", "Magellan MSWHEEL");
				if (intPtr != IntPtr.Zero)
				{
					int num2 = SafeNativeMethods.RegisterWindowMessage("MSH_SCROLL_LINES_MSG");
					int num3 = (int)UnsafeNativeMethods.SendMessage(new HandleRef(null, intPtr), num2, 0, 0);
					if (num3 != 0)
					{
						return num3;
					}
				}
				return 3;
			}
		}

		/// <summary>Gets the dimensions, in pixels, of the current video mode of the primary display.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the dimensions, in pixels, of the current video mode of the primary display.</returns>
		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x06003A9D RID: 15005 RVA: 0x001025AD File Offset: 0x001007AD
		public static Size PrimaryMonitorSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(0), UnsafeNativeMethods.GetSystemMetrics(1));
			}
		}

		/// <summary>Gets the default width, in pixels, of the vertical scroll bar.</summary>
		/// <returns>The default width, in pixels, of the vertical scroll bar.</returns>
		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x06003A9E RID: 15006 RVA: 0x001025C0 File Offset: 0x001007C0
		public static int VerticalScrollBarWidth
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(2);
			}
		}

		/// <summary>Gets the default height, in pixels, of the vertical scroll bar for a given DPI value.</summary>
		/// <param name="dpi">The DPI value for the current display device.</param>
		/// <returns>The default height, in pixels, of the vertical scroll bar.</returns>
		// Token: 0x06003A9F RID: 15007 RVA: 0x001025C8 File Offset: 0x001007C8
		public static int GetVerticalScrollBarWidthForDpi(int dpi)
		{
			if (DpiHelper.EnableDpiChangedMessageHandling)
			{
				return UnsafeNativeMethods.TryGetSystemMetricsForDpi(2, (uint)dpi);
			}
			return UnsafeNativeMethods.GetSystemMetrics(2);
		}

		/// <summary>Gets the default height, in pixels, of the horizontal scroll bar.</summary>
		/// <returns>The default height, in pixels, of the horizontal scroll bar.</returns>
		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x06003AA0 RID: 15008 RVA: 0x001025DF File Offset: 0x001007DF
		public static int HorizontalScrollBarHeight
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(3);
			}
		}

		/// <summary>Gets the default height, in pixels, of the horizontal scroll bar for a given DPI value.</summary>
		/// <param name="dpi">The DPI value for the current display device.</param>
		/// <returns>The default height, in pixels, of the horizontal scroll bar.</returns>
		// Token: 0x06003AA1 RID: 15009 RVA: 0x001025E7 File Offset: 0x001007E7
		public static int GetHorizontalScrollBarHeightForDpi(int dpi)
		{
			if (DpiHelper.EnableDpiChangedMessageHandling)
			{
				return UnsafeNativeMethods.TryGetSystemMetricsForDpi(3, (uint)dpi);
			}
			return UnsafeNativeMethods.GetSystemMetrics(3);
		}

		/// <summary>Gets the height, in pixels, of the standard title bar area of a window.</summary>
		/// <returns>The height, in pixels, of the standard title bar area of a window.</returns>
		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x06003AA2 RID: 15010 RVA: 0x001025FE File Offset: 0x001007FE
		public static int CaptionHeight
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(4);
			}
		}

		/// <summary>Gets the thickness, in pixels, of a flat-style window or system control border.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the width, in pixels, of a vertical border, and the height, in pixels, of a horizontal border.</returns>
		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x06003AA3 RID: 15011 RVA: 0x00102606 File Offset: 0x00100806
		public static Size BorderSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(5), UnsafeNativeMethods.GetSystemMetrics(6));
			}
		}

		/// <summary>Gets the thickness, in pixels, of a flat-style window or system control border for a given DPI value.</summary>
		/// <param name="dpi">The DPI value for the current display device.</param>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the width, in pixels, of a vertical border, and the height, in pixels, of a horizontal border.</returns>
		// Token: 0x06003AA4 RID: 15012 RVA: 0x00102619 File Offset: 0x00100819
		public static Size GetBorderSizeForDpi(int dpi)
		{
			if (DpiHelper.EnableDpiChangedMessageHandling)
			{
				return new Size(UnsafeNativeMethods.TryGetSystemMetricsForDpi(5, (uint)dpi), UnsafeNativeMethods.TryGetSystemMetricsForDpi(6, (uint)dpi));
			}
			return SystemInformation.BorderSize;
		}

		/// <summary>Gets the thickness, in pixels, of the frame border of a window that has a caption and is not resizable.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the thickness, in pixels, of a fixed sized window border.</returns>
		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x06003AA5 RID: 15013 RVA: 0x0010263B File Offset: 0x0010083B
		public static Size FixedFrameBorderSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(7), UnsafeNativeMethods.GetSystemMetrics(8));
			}
		}

		/// <summary>Gets the height, in pixels, of the scroll box in a vertical scroll bar.</summary>
		/// <returns>The height, in pixels, of the scroll box in a vertical scroll bar.</returns>
		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x06003AA6 RID: 15014 RVA: 0x0010264E File Offset: 0x0010084E
		public static int VerticalScrollBarThumbHeight
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(9);
			}
		}

		/// <summary>Gets the width, in pixels, of the scroll box in a horizontal scroll bar.</summary>
		/// <returns>The width, in pixels, of the scroll box in a horizontal scroll bar.</returns>
		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x06003AA7 RID: 15015 RVA: 0x00102657 File Offset: 0x00100857
		public static int HorizontalScrollBarThumbWidth
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(10);
			}
		}

		/// <summary>Gets the dimensions, in pixels, of the Windows default program icon size.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the default dimensions, in pixels, for a program icon.</returns>
		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x06003AA8 RID: 15016 RVA: 0x00102660 File Offset: 0x00100860
		public static Size IconSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(11), UnsafeNativeMethods.GetSystemMetrics(12));
			}
		}

		/// <summary>Gets the maximum size, in pixels, that a cursor can occupy.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the maximum dimensions of a cursor in pixels.</returns>
		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x06003AA9 RID: 15017 RVA: 0x00102675 File Offset: 0x00100875
		public static Size CursorSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(13), UnsafeNativeMethods.GetSystemMetrics(14));
			}
		}

		/// <summary>Gets the font used to display text on menus.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> used to display text on menus.</returns>
		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x06003AAA RID: 15018 RVA: 0x0010268A File Offset: 0x0010088A
		public static Font MenuFont
		{
			get
			{
				return SystemInformation.GetMenuFontHelper(0U, false);
			}
		}

		/// <summary>Gets the font used to display text on menus for use in changing the DPI for a given display device.</summary>
		/// <param name="dpi">The DPI value for the current display device.</param>
		/// <returns>The <see cref="T:System.Drawing.Font" /> used to display text on menus.</returns>
		// Token: 0x06003AAB RID: 15019 RVA: 0x00102693 File Offset: 0x00100893
		public static Font GetMenuFontForDpi(int dpi)
		{
			return SystemInformation.GetMenuFontHelper((uint)dpi, DpiHelper.EnableDpiChangedMessageHandling);
		}

		// Token: 0x06003AAC RID: 15020 RVA: 0x001026A0 File Offset: 0x001008A0
		private static Font GetMenuFontHelper(uint dpi, bool useDpi)
		{
			Font font = null;
			NativeMethods.NONCLIENTMETRICS nonclientmetrics = new NativeMethods.NONCLIENTMETRICS();
			bool flag;
			if (useDpi)
			{
				flag = UnsafeNativeMethods.TrySystemParametersInfoForDpi(41, nonclientmetrics.cbSize, nonclientmetrics, 0, dpi);
			}
			else
			{
				flag = UnsafeNativeMethods.SystemParametersInfo(41, nonclientmetrics.cbSize, nonclientmetrics, 0);
			}
			if (flag && nonclientmetrics.lfMenuFont != null)
			{
				IntSecurity.ObjectFromWin32Handle.Assert();
				try
				{
					font = Font.FromLogFont(nonclientmetrics.lfMenuFont);
				}
				catch
				{
					font = Control.DefaultFont;
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
			return font;
		}

		/// <summary>Gets the height, in pixels, of one line of a menu.</summary>
		/// <returns>The height, in pixels, of one line of a menu.</returns>
		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x06003AAD RID: 15021 RVA: 0x0010272C File Offset: 0x0010092C
		public static int MenuHeight
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(15);
			}
		}

		/// <summary>Gets the current system power status.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.PowerStatus" /> that indicates the current system power status.</returns>
		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x06003AAE RID: 15022 RVA: 0x00102735 File Offset: 0x00100935
		public static PowerStatus PowerStatus
		{
			get
			{
				if (SystemInformation.powerStatus == null)
				{
					SystemInformation.powerStatus = new PowerStatus();
				}
				return SystemInformation.powerStatus;
			}
		}

		/// <summary>Gets the size, in pixels, of the working area of the screen.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the size, in pixels, of the working area of the screen.</returns>
		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x06003AAF RID: 15023 RVA: 0x00102750 File Offset: 0x00100950
		public static Rectangle WorkingArea
		{
			get
			{
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				UnsafeNativeMethods.SystemParametersInfo(48, 0, ref rect, 0);
				return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
			}
		}

		/// <summary>Gets the height, in pixels, of the Kanji window at the bottom of the screen for double-byte character set (DBCS) versions of Windows.</summary>
		/// <returns>The height, in pixels, of the Kanji window.</returns>
		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x06003AB0 RID: 15024 RVA: 0x0010278E File Offset: 0x0010098E
		public static int KanjiWindowHeight
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(18);
			}
		}

		/// <summary>Gets a value indicating whether a pointing device is installed.</summary>
		/// <returns>
		///   <see langword="true" /> if a mouse is installed; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x06003AB1 RID: 15025 RVA: 0x00102797 File Offset: 0x00100997
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static bool MousePresent
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(19) != 0;
			}
		}

		/// <summary>Gets the height, in pixels, of the arrow bitmap on the vertical scroll bar.</summary>
		/// <returns>The height, in pixels, of the arrow bitmap on the vertical scroll bar.</returns>
		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x06003AB2 RID: 15026 RVA: 0x001027A3 File Offset: 0x001009A3
		public static int VerticalScrollBarArrowHeight
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(20);
			}
		}

		/// <summary>Gets the height of the vertical scroll bar arrow bitmap in pixels.</summary>
		/// <param name="dpi">An arbitrary DPI value used to scale the vertical scroll bar arrow bitmap.</param>
		/// <returns>The height of the vertical scroll bar arrow bitmap in pixels.</returns>
		// Token: 0x06003AB3 RID: 15027 RVA: 0x001027AC File Offset: 0x001009AC
		public static int VerticalScrollBarArrowHeightForDpi(int dpi)
		{
			return UnsafeNativeMethods.TryGetSystemMetricsForDpi(21, (uint)dpi);
		}

		/// <summary>Gets the width, in pixels, of the arrow bitmap on the horizontal scroll bar.</summary>
		/// <returns>The width, in pixels, of the arrow bitmap on the horizontal scroll bar.</returns>
		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x06003AB4 RID: 15028 RVA: 0x001027B6 File Offset: 0x001009B6
		public static int HorizontalScrollBarArrowWidth
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(21);
			}
		}

		/// <summary>Gets the width of the horizontal scroll bar arrow bitmap in pixels.</summary>
		/// <param name="dpi">The DPI value for the current display device.</param>
		/// <returns>The default width, in pixels, of the horizontal scroll bar arrow.</returns>
		// Token: 0x06003AB5 RID: 15029 RVA: 0x001027BF File Offset: 0x001009BF
		public static int GetHorizontalScrollBarArrowWidthForDpi(int dpi)
		{
			if (DpiHelper.EnableDpiChangedMessageHandling)
			{
				return UnsafeNativeMethods.TryGetSystemMetricsForDpi(21, (uint)dpi);
			}
			return UnsafeNativeMethods.GetSystemMetrics(21);
		}

		/// <summary>Gets a value indicating whether the debug version of USER.EXE is installed.</summary>
		/// <returns>
		///   <see langword="true" /> if the debugging version of USER.EXE is installed; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x06003AB6 RID: 15030 RVA: 0x001027D8 File Offset: 0x001009D8
		public static bool DebugOS
		{
			get
			{
				IntSecurity.SensitiveSystemInformation.Demand();
				return UnsafeNativeMethods.GetSystemMetrics(22) != 0;
			}
		}

		/// <summary>Gets a value indicating whether the functions of the left and right mouse buttons have been swapped.</summary>
		/// <returns>
		///   <see langword="true" /> if the functions of the left and right mouse buttons are swapped; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x06003AB7 RID: 15031 RVA: 0x001027EE File Offset: 0x001009EE
		public static bool MouseButtonsSwapped
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(23) != 0;
			}
		}

		/// <summary>Gets the minimum width and height for a window, in pixels.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the minimum allowable dimensions of a window, in pixels.</returns>
		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x06003AB8 RID: 15032 RVA: 0x001027FA File Offset: 0x001009FA
		public static Size MinimumWindowSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(28), UnsafeNativeMethods.GetSystemMetrics(29));
			}
		}

		/// <summary>Gets the standard size, in pixels, of a button in a window's title bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the standard dimensions, in pixels, of a button in a window's title bar.</returns>
		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x06003AB9 RID: 15033 RVA: 0x0010280F File Offset: 0x00100A0F
		public static Size CaptionButtonSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(30), UnsafeNativeMethods.GetSystemMetrics(31));
			}
		}

		/// <summary>Gets the thickness, in pixels, of the resizing border that is drawn around the perimeter of a window that is being drag resized.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the thickness, in pixels, of the width of a vertical resizing border and the height of a horizontal resizing border.</returns>
		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x06003ABA RID: 15034 RVA: 0x00102824 File Offset: 0x00100A24
		public static Size FrameBorderSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(32), UnsafeNativeMethods.GetSystemMetrics(33));
			}
		}

		/// <summary>Gets the default minimum dimensions, in pixels, that a window may occupy during a drag resize.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the default minimum width and height of a window during resize, in pixels.</returns>
		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x06003ABB RID: 15035 RVA: 0x00102839 File Offset: 0x00100A39
		public static Size MinWindowTrackSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(34), UnsafeNativeMethods.GetSystemMetrics(35));
			}
		}

		/// <summary>Gets the dimensions, in pixels, of the area within which the user must click twice for the operating system to consider the two clicks a double-click.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the dimensions, in pixels, of the area within which the user must click twice for the operating system to consider the two clicks a double-click.</returns>
		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x06003ABC RID: 15036 RVA: 0x0010284E File Offset: 0x00100A4E
		public static Size DoubleClickSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(36), UnsafeNativeMethods.GetSystemMetrics(37));
			}
		}

		/// <summary>Gets the maximum number of milliseconds that can elapse between a first click and a second click for the OS to consider the mouse action a double-click.</summary>
		/// <returns>The maximum amount of time, in milliseconds, that can elapse between a first click and a second click for the OS to consider the mouse action a double-click.</returns>
		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x06003ABD RID: 15037 RVA: 0x00102863 File Offset: 0x00100A63
		public static int DoubleClickTime
		{
			get
			{
				return SafeNativeMethods.GetDoubleClickTime();
			}
		}

		/// <summary>Gets the size, in pixels, of the grid square used to arrange icons in a large-icon view.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the dimensions, in pixels, of the grid square used to arrange icons in a large-icon view.</returns>
		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x06003ABE RID: 15038 RVA: 0x0010286A File Offset: 0x00100A6A
		public static Size IconSpacingSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(38), UnsafeNativeMethods.GetSystemMetrics(39));
			}
		}

		/// <summary>Gets a value indicating whether drop-down menus are right-aligned with the corresponding menu-bar item.</summary>
		/// <returns>
		///   <see langword="true" /> if drop-down menus are right-aligned with the corresponding menu-bar item; <see langword="false" /> if the menus are left-aligned.</returns>
		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x06003ABF RID: 15039 RVA: 0x0010287F File Offset: 0x00100A7F
		public static bool RightAlignedMenus
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(40) != 0;
			}
		}

		/// <summary>Gets a value indicating whether the Microsoft Windows for Pen Computing extensions are installed.</summary>
		/// <returns>
		///   <see langword="true" /> if the Windows for Pen Computing extensions are installed; <see langword="false" /> if Windows for Pen Computing extensions are not installed.</returns>
		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x06003AC0 RID: 15040 RVA: 0x0010288B File Offset: 0x00100A8B
		public static bool PenWindows
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(41) != 0;
			}
		}

		/// <summary>Gets a value indicating whether the operating system is capable of handling double-byte character set (DBCS) characters.</summary>
		/// <returns>
		///   <see langword="true" /> if the operating system supports DBCS; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x06003AC1 RID: 15041 RVA: 0x00102897 File Offset: 0x00100A97
		public static bool DbcsEnabled
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(42) != 0;
			}
		}

		/// <summary>Gets the number of buttons on the mouse.</summary>
		/// <returns>The number of buttons on the mouse, or zero if no mouse is installed.</returns>
		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x06003AC2 RID: 15042 RVA: 0x001028A3 File Offset: 0x00100AA3
		public static int MouseButtons
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(43);
			}
		}

		/// <summary>Gets a value indicating whether a Security Manager is present on this operating system.</summary>
		/// <returns>
		///   <see langword="true" /> if a Security Manager is present; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x06003AC3 RID: 15043 RVA: 0x001028AC File Offset: 0x00100AAC
		public static bool Secure
		{
			get
			{
				IntSecurity.SensitiveSystemInformation.Demand();
				return UnsafeNativeMethods.GetSystemMetrics(44) != 0;
			}
		}

		/// <summary>Gets the thickness, in pixels, of a three-dimensional (3-D) style window or system control border.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the width, in pixels, of a 3-D style vertical border, and the height, in pixels, of a 3-D style horizontal border.</returns>
		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x06003AC4 RID: 15044 RVA: 0x001028C2 File Offset: 0x00100AC2
		public static Size Border3DSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(45), UnsafeNativeMethods.GetSystemMetrics(46));
			}
		}

		/// <summary>Gets the dimensions, in pixels, of the area each minimized window is allocated when arranged.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the area each minimized window is allocated when arranged.</returns>
		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x06003AC5 RID: 15045 RVA: 0x001028D7 File Offset: 0x00100AD7
		public static Size MinimizedWindowSpacingSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(47), UnsafeNativeMethods.GetSystemMetrics(48));
			}
		}

		/// <summary>Gets the dimensions, in pixels, of a small icon.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the dimensions, in pixels, of a small icon.</returns>
		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x06003AC6 RID: 15046 RVA: 0x001028EC File Offset: 0x00100AEC
		public static Size SmallIconSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(49), UnsafeNativeMethods.GetSystemMetrics(50));
			}
		}

		/// <summary>Gets the height, in pixels, of a tool window caption.</summary>
		/// <returns>The height, in pixels, of a tool window caption in pixels.</returns>
		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x06003AC7 RID: 15047 RVA: 0x00102901 File Offset: 0x00100B01
		public static int ToolWindowCaptionHeight
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(51);
			}
		}

		/// <summary>Gets the dimensions, in pixels, of small caption buttons.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the dimensions, in pixels, of small caption buttons.</returns>
		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x06003AC8 RID: 15048 RVA: 0x0010290A File Offset: 0x00100B0A
		public static Size ToolWindowCaptionButtonSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(52), UnsafeNativeMethods.GetSystemMetrics(53));
			}
		}

		/// <summary>Gets the default dimensions, in pixels, of menu-bar buttons.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the default dimensions, in pixels, of menu-bar buttons.</returns>
		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x06003AC9 RID: 15049 RVA: 0x0010291F File Offset: 0x00100B1F
		public static Size MenuButtonSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(54), UnsafeNativeMethods.GetSystemMetrics(55));
			}
		}

		/// <summary>Gets an <see cref="T:System.Windows.Forms.ArrangeStartingPosition" /> value that indicates the starting position from which the operating system arranges minimized windows.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ArrangeStartingPosition" /> values that indicates the starting position from which the operating system arranges minimized windows.</returns>
		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x06003ACA RID: 15050 RVA: 0x00102934 File Offset: 0x00100B34
		public static ArrangeStartingPosition ArrangeStartingPosition
		{
			get
			{
				ArrangeStartingPosition arrangeStartingPosition = ArrangeStartingPosition.BottomRight | ArrangeStartingPosition.Hide | ArrangeStartingPosition.TopLeft;
				int systemMetrics = UnsafeNativeMethods.GetSystemMetrics(56);
				return arrangeStartingPosition & (ArrangeStartingPosition)systemMetrics;
			}
		}

		/// <summary>Gets a value that indicates the direction in which the operating system arranges minimized windows.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ArrangeDirection" /> values that indicates the direction in which the operating system arranges minimized windows.</returns>
		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x06003ACB RID: 15051 RVA: 0x00102950 File Offset: 0x00100B50
		public static ArrangeDirection ArrangeDirection
		{
			get
			{
				ArrangeDirection arrangeDirection = ArrangeDirection.Down;
				int systemMetrics = UnsafeNativeMethods.GetSystemMetrics(56);
				return arrangeDirection & (ArrangeDirection)systemMetrics;
			}
		}

		/// <summary>Gets the dimensions, in pixels, of a normal minimized window.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the dimensions, in pixels, of a normal minimized window.</returns>
		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x06003ACC RID: 15052 RVA: 0x0010296A File Offset: 0x00100B6A
		public static Size MinimizedWindowSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(57), UnsafeNativeMethods.GetSystemMetrics(58));
			}
		}

		/// <summary>Gets the default maximum dimensions, in pixels, of a window that has a caption and sizing borders.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the maximum dimensions, in pixels, to which a window can be sized.</returns>
		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x06003ACD RID: 15053 RVA: 0x0010297F File Offset: 0x00100B7F
		public static Size MaxWindowTrackSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(59), UnsafeNativeMethods.GetSystemMetrics(60));
			}
		}

		/// <summary>Gets the default dimensions, in pixels, of a maximized window on the primary display.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the dimensions, in pixels, of a maximized window on the primary display.</returns>
		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x06003ACE RID: 15054 RVA: 0x00102994 File Offset: 0x00100B94
		public static Size PrimaryMonitorMaximizedWindowSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(61), UnsafeNativeMethods.GetSystemMetrics(62));
			}
		}

		/// <summary>Gets a value indicating whether a network connection is present.</summary>
		/// <returns>
		///   <see langword="true" /> if a network connection is present; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x06003ACF RID: 15055 RVA: 0x001029A9 File Offset: 0x00100BA9
		public static bool Network
		{
			get
			{
				return (UnsafeNativeMethods.GetSystemMetrics(63) & 1) != 0;
			}
		}

		/// <summary>Gets a value indicating whether the calling process is associated with a Terminal Services client session.</summary>
		/// <returns>
		///   <see langword="true" /> if the calling process is associated with a Terminal Services client session; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x06003AD0 RID: 15056 RVA: 0x001029B7 File Offset: 0x00100BB7
		public static bool TerminalServerSession
		{
			get
			{
				return (UnsafeNativeMethods.GetSystemMetrics(4096) & 1) != 0;
			}
		}

		/// <summary>Gets a <see cref="T:System.Windows.Forms.BootMode" /> value that indicates the boot mode the system was started in.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BootMode" /> values that indicates the boot mode the system was started in.</returns>
		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x06003AD1 RID: 15057 RVA: 0x001029C8 File Offset: 0x00100BC8
		public static BootMode BootMode
		{
			get
			{
				IntSecurity.SensitiveSystemInformation.Demand();
				return (BootMode)UnsafeNativeMethods.GetSystemMetrics(67);
			}
		}

		/// <summary>Gets the width and height of a rectangle centered on the point the mouse button was pressed, within which a drag operation will not begin.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the area of a rectangle, in pixels, centered on the point the mouse button was pressed, within which a drag operation will not begin.</returns>
		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x06003AD2 RID: 15058 RVA: 0x001029DB File Offset: 0x00100BDB
		public static Size DragSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(68), UnsafeNativeMethods.GetSystemMetrics(69));
			}
		}

		/// <summary>Gets a value indicating whether the user prefers that an application present information in visual form in situations when it would present the information in audible form.</summary>
		/// <returns>
		///   <see langword="true" /> if the application should visually show information about audible output; <see langword="false" /> if the application does not need to provide extra visual cues for audio events.</returns>
		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x06003AD3 RID: 15059 RVA: 0x001029F0 File Offset: 0x00100BF0
		public static bool ShowSounds
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(70) != 0;
			}
		}

		/// <summary>Gets the dimensions, in pixels, of the default size of a menu check mark area.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the default size, in pixels, of a menu check mark area.</returns>
		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x06003AD4 RID: 15060 RVA: 0x001029FC File Offset: 0x00100BFC
		public static Size MenuCheckSize
		{
			get
			{
				return new Size(UnsafeNativeMethods.GetSystemMetrics(71), UnsafeNativeMethods.GetSystemMetrics(72));
			}
		}

		/// <summary>Gets a value indicating whether the operating system is enabled for the Hebrew and Arabic languages.</summary>
		/// <returns>
		///   <see langword="true" /> if the operating system is enabled for Hebrew or Arabic; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x06003AD5 RID: 15061 RVA: 0x00102A11 File Offset: 0x00100C11
		public static bool MidEastEnabled
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(74) != 0;
			}
		}

		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x06003AD6 RID: 15062 RVA: 0x00102A1D File Offset: 0x00100C1D
		private static bool MultiMonitorSupport
		{
			get
			{
				if (!SystemInformation.checkMultiMonitorSupport)
				{
					SystemInformation.multiMonitorSupport = UnsafeNativeMethods.GetSystemMetrics(80) != 0;
					SystemInformation.checkMultiMonitorSupport = true;
				}
				return SystemInformation.multiMonitorSupport;
			}
		}

		/// <summary>Gets a value indicating whether the operating system natively supports a mouse wheel.</summary>
		/// <returns>
		///   <see langword="true" /> if the operating system natively supports a mouse wheel; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x06003AD7 RID: 15063 RVA: 0x00102A40 File Offset: 0x00100C40
		public static bool NativeMouseWheelSupport
		{
			get
			{
				if (!SystemInformation.checkNativeMouseWheelSupport)
				{
					SystemInformation.nativeMouseWheelSupport = UnsafeNativeMethods.GetSystemMetrics(75) != 0;
					SystemInformation.checkNativeMouseWheelSupport = true;
				}
				return SystemInformation.nativeMouseWheelSupport;
			}
		}

		/// <summary>Gets a value indicating whether a mouse with a mouse wheel is installed.</summary>
		/// <returns>
		///   <see langword="true" /> if a mouse with a mouse wheel is installed; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x06003AD8 RID: 15064 RVA: 0x00102A64 File Offset: 0x00100C64
		public static bool MouseWheelPresent
		{
			get
			{
				bool flag = false;
				if (!SystemInformation.NativeMouseWheelSupport)
				{
					IntPtr intPtr = IntPtr.Zero;
					intPtr = UnsafeNativeMethods.FindWindow("MouseZ", "Magellan MSWHEEL");
					if (intPtr != IntPtr.Zero)
					{
						flag = true;
					}
				}
				else
				{
					flag = UnsafeNativeMethods.GetSystemMetrics(75) != 0;
				}
				return flag;
			}
		}

		/// <summary>Gets the bounds of the virtual screen.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that specifies the bounding rectangle of the entire virtual screen.</returns>
		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x06003AD9 RID: 15065 RVA: 0x00102AB0 File Offset: 0x00100CB0
		public static Rectangle VirtualScreen
		{
			get
			{
				if (SystemInformation.MultiMonitorSupport)
				{
					return new Rectangle(UnsafeNativeMethods.GetSystemMetrics(76), UnsafeNativeMethods.GetSystemMetrics(77), UnsafeNativeMethods.GetSystemMetrics(78), UnsafeNativeMethods.GetSystemMetrics(79));
				}
				Size primaryMonitorSize = SystemInformation.PrimaryMonitorSize;
				return new Rectangle(0, 0, primaryMonitorSize.Width, primaryMonitorSize.Height);
			}
		}

		/// <summary>Gets the number of display monitors on the desktop.</summary>
		/// <returns>The number of monitors that make up the desktop.</returns>
		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x06003ADA RID: 15066 RVA: 0x00102B01 File Offset: 0x00100D01
		public static int MonitorCount
		{
			get
			{
				if (SystemInformation.MultiMonitorSupport)
				{
					return UnsafeNativeMethods.GetSystemMetrics(80);
				}
				return 1;
			}
		}

		/// <summary>Gets a value indicating whether all the display monitors are using the same pixel color format.</summary>
		/// <returns>
		///   <see langword="true" /> if all monitors are using the same pixel color format; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x06003ADB RID: 15067 RVA: 0x00102B13 File Offset: 0x00100D13
		public static bool MonitorsSameDisplayFormat
		{
			get
			{
				return !SystemInformation.MultiMonitorSupport || UnsafeNativeMethods.GetSystemMetrics(81) != 0;
			}
		}

		/// <summary>Gets the NetBIOS computer name of the local computer.</summary>
		/// <returns>The name of this computer.</returns>
		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x06003ADC RID: 15068 RVA: 0x00102B28 File Offset: 0x00100D28
		public static string ComputerName
		{
			get
			{
				IntSecurity.SensitiveSystemInformation.Demand();
				StringBuilder stringBuilder = new StringBuilder(256);
				UnsafeNativeMethods.GetComputerName(stringBuilder, new int[] { stringBuilder.Capacity });
				return stringBuilder.ToString();
			}
		}

		/// <summary>Gets the name of the domain the user belongs to.</summary>
		/// <returns>The name of the user domain. If a local user account exists with the same name as the user name, this property gets the computer name.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operating system does not support this feature.</exception>
		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x06003ADD RID: 15069 RVA: 0x00102B66 File Offset: 0x00100D66
		public static string UserDomainName
		{
			get
			{
				return Environment.UserDomainName;
			}
		}

		/// <summary>Gets a value indicating whether the current process is running in user-interactive mode.</summary>
		/// <returns>
		///   <see langword="true" /> if the current process is running in user-interactive mode; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x06003ADE RID: 15070 RVA: 0x00102B70 File Offset: 0x00100D70
		public static bool UserInteractive
		{
			get
			{
				if (Environment.OSVersion.Platform == PlatformID.Win32NT)
				{
					IntPtr intPtr = IntPtr.Zero;
					intPtr = UnsafeNativeMethods.GetProcessWindowStation();
					if (intPtr != IntPtr.Zero && SystemInformation.processWinStation != intPtr)
					{
						SystemInformation.isUserInteractive = true;
						int num = 0;
						NativeMethods.USEROBJECTFLAGS userobjectflags = new NativeMethods.USEROBJECTFLAGS();
						if (UnsafeNativeMethods.GetUserObjectInformation(new HandleRef(null, intPtr), 1, userobjectflags, Marshal.SizeOf(userobjectflags), ref num) && (userobjectflags.dwFlags & 1) == 0)
						{
							SystemInformation.isUserInteractive = false;
						}
						SystemInformation.processWinStation = intPtr;
					}
				}
				else
				{
					SystemInformation.isUserInteractive = true;
				}
				return SystemInformation.isUserInteractive;
			}
		}

		/// <summary>Gets the user name associated with the current thread.</summary>
		/// <returns>The user name of the user associated with the current thread.</returns>
		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x06003ADF RID: 15071 RVA: 0x00102BFC File Offset: 0x00100DFC
		public static string UserName
		{
			get
			{
				IntSecurity.SensitiveSystemInformation.Demand();
				StringBuilder stringBuilder = new StringBuilder(256);
				UnsafeNativeMethods.GetUserName(stringBuilder, new int[] { stringBuilder.Capacity });
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06003AE0 RID: 15072 RVA: 0x00102C3A File Offset: 0x00100E3A
		private static void EnsureSystemEvents()
		{
			if (!SystemInformation.systemEventsAttached)
			{
				SystemEvents.UserPreferenceChanged += SystemInformation.OnUserPreferenceChanged;
				SystemInformation.systemEventsAttached = true;
			}
		}

		// Token: 0x06003AE1 RID: 15073 RVA: 0x00102C5A File Offset: 0x00100E5A
		private static void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs pref)
		{
			SystemInformation.systemEventsDirty = true;
		}

		/// <summary>Gets a value indicating whether the drop shadow effect is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if the drop shadow effect is enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x06003AE2 RID: 15074 RVA: 0x00102C64 File Offset: 0x00100E64
		public static bool IsDropShadowEnabled
		{
			get
			{
				if (OSFeature.Feature.OnXp)
				{
					int num = 0;
					UnsafeNativeMethods.SystemParametersInfo(4132, 0, ref num, 0);
					return num != 0;
				}
				return false;
			}
		}

		/// <summary>Gets a value indicating whether native user menus have a flat menu appearance.</summary>
		/// <returns>This property is not used and always returns <see langword="false" />.</returns>
		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x06003AE3 RID: 15075 RVA: 0x00102C94 File Offset: 0x00100E94
		public static bool IsFlatMenuEnabled
		{
			get
			{
				if (OSFeature.Feature.OnXp)
				{
					int num = 0;
					UnsafeNativeMethods.SystemParametersInfo(4130, 0, ref num, 0);
					return num != 0;
				}
				return false;
			}
		}

		/// <summary>Gets a value indicating whether font smoothing is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if the font smoothing feature is enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x06003AE4 RID: 15076 RVA: 0x00102CC4 File Offset: 0x00100EC4
		public static bool IsFontSmoothingEnabled
		{
			get
			{
				int num = 0;
				UnsafeNativeMethods.SystemParametersInfo(74, 0, ref num, 0);
				return num != 0;
			}
		}

		/// <summary>Gets the font smoothing contrast value used in ClearType smoothing.</summary>
		/// <returns>The ClearType font smoothing contrast value.</returns>
		/// <exception cref="T:System.NotSupportedException">The operating system does not support this feature.</exception>
		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x06003AE5 RID: 15077 RVA: 0x00102CE4 File Offset: 0x00100EE4
		public static int FontSmoothingContrast
		{
			get
			{
				if (OSFeature.Feature.OnXp)
				{
					int num = 0;
					UnsafeNativeMethods.SystemParametersInfo(8204, 0, ref num, 0);
					return num;
				}
				throw new NotSupportedException(SR.GetString("SystemInformationFeatureNotSupported"));
			}
		}

		/// <summary>Gets the current type of font smoothing.</summary>
		/// <returns>A value that indicates the current type of font smoothing.</returns>
		/// <exception cref="T:System.NotSupportedException">The operating system does not support this feature.</exception>
		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x06003AE6 RID: 15078 RVA: 0x00102D20 File Offset: 0x00100F20
		public static int FontSmoothingType
		{
			get
			{
				if (OSFeature.Feature.OnXp)
				{
					int num = 0;
					UnsafeNativeMethods.SystemParametersInfo(8202, 0, ref num, 0);
					return num;
				}
				throw new NotSupportedException(SR.GetString("SystemInformationFeatureNotSupported"));
			}
		}

		/// <summary>Gets the width, in pixels, of an icon arrangement cell in large icon view.</summary>
		/// <returns>The width, in pixels, of an icon arrangement cell in large icon view.</returns>
		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x06003AE7 RID: 15079 RVA: 0x00102D5C File Offset: 0x00100F5C
		public static int IconHorizontalSpacing
		{
			get
			{
				int num = 0;
				UnsafeNativeMethods.SystemParametersInfo(13, 0, ref num, 0);
				return num;
			}
		}

		/// <summary>Gets the height, in pixels, of an icon arrangement cell in large icon view.</summary>
		/// <returns>The height, in pixels, of an icon arrangement cell in large icon view.</returns>
		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x06003AE8 RID: 15080 RVA: 0x00102D78 File Offset: 0x00100F78
		public static int IconVerticalSpacing
		{
			get
			{
				int num = 0;
				UnsafeNativeMethods.SystemParametersInfo(24, 0, ref num, 0);
				return num;
			}
		}

		/// <summary>Gets a value indicating whether icon-title wrapping is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if the icon-title wrapping feature is enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x06003AE9 RID: 15081 RVA: 0x00102D94 File Offset: 0x00100F94
		public static bool IsIconTitleWrappingEnabled
		{
			get
			{
				int num = 0;
				UnsafeNativeMethods.SystemParametersInfo(25, 0, ref num, 0);
				return num != 0;
			}
		}

		/// <summary>Gets a value indicating whether menu access keys are always underlined.</summary>
		/// <returns>
		///   <see langword="true" /> if menu access keys are always underlined; <see langword="false" /> if they are underlined only when the menu is activated or receives focus.</returns>
		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x06003AEA RID: 15082 RVA: 0x00102DB4 File Offset: 0x00100FB4
		public static bool MenuAccessKeysUnderlined
		{
			get
			{
				int num = 0;
				UnsafeNativeMethods.SystemParametersInfo(4106, 0, ref num, 0);
				return num != 0;
			}
		}

		/// <summary>Gets the keyboard repeat-delay setting.</summary>
		/// <returns>The keyboard repeat-delay setting, from 0 (approximately 250 millisecond delay) through 3 (approximately 1 second delay).</returns>
		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x06003AEB RID: 15083 RVA: 0x00102DD8 File Offset: 0x00100FD8
		public static int KeyboardDelay
		{
			get
			{
				int num = 0;
				UnsafeNativeMethods.SystemParametersInfo(22, 0, ref num, 0);
				return num;
			}
		}

		/// <summary>Gets a value indicating whether the user relies on the keyboard instead of the mouse, and prefers applications to display keyboard interfaces that would otherwise be hidden.</summary>
		/// <returns>
		///   <see langword="true" /> if keyboard preferred mode is enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x06003AEC RID: 15084 RVA: 0x00102DF4 File Offset: 0x00100FF4
		public static bool IsKeyboardPreferred
		{
			get
			{
				int num = 0;
				UnsafeNativeMethods.SystemParametersInfo(68, 0, ref num, 0);
				return num != 0;
			}
		}

		/// <summary>Gets the keyboard repeat-speed setting.</summary>
		/// <returns>The keyboard repeat-speed setting, from 0 (approximately 2.5 repetitions per second) through 31 (approximately 30 repetitions per second).</returns>
		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x06003AED RID: 15085 RVA: 0x00102E14 File Offset: 0x00101014
		public static int KeyboardSpeed
		{
			get
			{
				int num = 0;
				UnsafeNativeMethods.SystemParametersInfo(10, 0, ref num, 0);
				return num;
			}
		}

		/// <summary>Gets the dimensions, in pixels, of the rectangle within which the mouse pointer has to stay for the mouse hover time before a mouse hover message is generated.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the dimensions, in pixels, of the rectangle within which the mouse pointer has to stay for the mouse hover time before a mouse hover message is generated.</returns>
		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x06003AEE RID: 15086 RVA: 0x00102E30 File Offset: 0x00101030
		public static Size MouseHoverSize
		{
			get
			{
				int num = 0;
				int num2 = 0;
				UnsafeNativeMethods.SystemParametersInfo(100, 0, ref num, 0);
				UnsafeNativeMethods.SystemParametersInfo(98, 0, ref num2, 0);
				return new Size(num2, num);
			}
		}

		/// <summary>Gets the time, in milliseconds, that the mouse pointer has to stay in the hover rectangle before a mouse hover message is generated.</summary>
		/// <returns>The time, in milliseconds, that the mouse pointer has to stay in the hover rectangle before a mouse hover message is generated.</returns>
		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x06003AEF RID: 15087 RVA: 0x00102E60 File Offset: 0x00101060
		public static int MouseHoverTime
		{
			get
			{
				int num = 0;
				UnsafeNativeMethods.SystemParametersInfo(102, 0, ref num, 0);
				return num;
			}
		}

		/// <summary>Gets the current mouse speed.</summary>
		/// <returns>A mouse speed value between 1 (slowest) and 20 (fastest).</returns>
		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x06003AF0 RID: 15088 RVA: 0x00102E7C File Offset: 0x0010107C
		public static int MouseSpeed
		{
			get
			{
				int num = 0;
				UnsafeNativeMethods.SystemParametersInfo(112, 0, ref num, 0);
				return num;
			}
		}

		/// <summary>Gets a value indicating whether the snap-to-default-button feature is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if the snap-to-default-button feature is enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x06003AF1 RID: 15089 RVA: 0x00102E98 File Offset: 0x00101098
		public static bool IsSnapToDefaultEnabled
		{
			get
			{
				int num = 0;
				UnsafeNativeMethods.SystemParametersInfo(95, 0, ref num, 0);
				return num != 0;
			}
		}

		/// <summary>Gets the side of pop-up menus that are aligned to the corresponding menu-bar item.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.LeftRightAlignment" /> that indicates whether pop-up menus are left-aligned or right-aligned, relative to the corresponding menu-bar item.</returns>
		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x06003AF2 RID: 15090 RVA: 0x00102EB8 File Offset: 0x001010B8
		public static LeftRightAlignment PopupMenuAlignment
		{
			get
			{
				bool flag = false;
				UnsafeNativeMethods.SystemParametersInfo(27, 0, ref flag, 0);
				if (flag)
				{
					return LeftRightAlignment.Left;
				}
				return LeftRightAlignment.Right;
			}
		}

		/// <summary>Gets a value indicating whether menu fade animation is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if fade animation is enabled; <see langword="false" /> if it is disabled.</returns>
		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x06003AF3 RID: 15091 RVA: 0x00102EDC File Offset: 0x001010DC
		public static bool IsMenuFadeEnabled
		{
			get
			{
				if (OSFeature.Feature.OnXp || OSFeature.Feature.OnWin2k)
				{
					int num = 0;
					UnsafeNativeMethods.SystemParametersInfo(4114, 0, ref num, 0);
					return num != 0;
				}
				return false;
			}
		}

		/// <summary>Gets the time, in milliseconds, that the system waits before displaying a cascaded shortcut menu when the mouse cursor is over a submenu item.</summary>
		/// <returns>The time, in milliseconds, that the system waits before displaying a cascaded shortcut menu when the mouse cursor is over a submenu item.</returns>
		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x06003AF4 RID: 15092 RVA: 0x00102F18 File Offset: 0x00101118
		public static int MenuShowDelay
		{
			get
			{
				int num = 0;
				UnsafeNativeMethods.SystemParametersInfo(106, 0, ref num, 0);
				return num;
			}
		}

		/// <summary>Gets a value indicating whether the slide-open effect for combo boxes is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if the slide-open effect for combo boxes is enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x06003AF5 RID: 15093 RVA: 0x00102F34 File Offset: 0x00101134
		public static bool IsComboBoxAnimationEnabled
		{
			get
			{
				int num = 0;
				UnsafeNativeMethods.SystemParametersInfo(4100, 0, ref num, 0);
				return num != 0;
			}
		}

		/// <summary>Gets a value indicating whether the gradient effect for window title bars is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if the gradient effect for window title bars is enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x06003AF6 RID: 15094 RVA: 0x00102F58 File Offset: 0x00101158
		public static bool IsTitleBarGradientEnabled
		{
			get
			{
				int num = 0;
				UnsafeNativeMethods.SystemParametersInfo(4104, 0, ref num, 0);
				return num != 0;
			}
		}

		/// <summary>Gets a value indicating whether hot tracking of user-interface elements, such as menu names on menu bars, is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if hot tracking of user-interface elements is enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x06003AF7 RID: 15095 RVA: 0x00102F7C File Offset: 0x0010117C
		public static bool IsHotTrackingEnabled
		{
			get
			{
				int num = 0;
				UnsafeNativeMethods.SystemParametersInfo(4110, 0, ref num, 0);
				return num != 0;
			}
		}

		/// <summary>Gets a value indicating whether the smooth-scrolling effect for list boxes is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if smooth-scrolling is enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x06003AF8 RID: 15096 RVA: 0x00102FA0 File Offset: 0x001011A0
		public static bool IsListBoxSmoothScrollingEnabled
		{
			get
			{
				int num = 0;
				UnsafeNativeMethods.SystemParametersInfo(4102, 0, ref num, 0);
				return num != 0;
			}
		}

		/// <summary>Gets a value indicating whether menu fade or slide animation features are enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if menu fade or slide animation is enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x06003AF9 RID: 15097 RVA: 0x00102FC4 File Offset: 0x001011C4
		public static bool IsMenuAnimationEnabled
		{
			get
			{
				int num = 0;
				UnsafeNativeMethods.SystemParametersInfo(4098, 0, ref num, 0);
				return num != 0;
			}
		}

		/// <summary>Gets a value indicating whether the selection fade effect is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if the selection fade effect is enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x06003AFA RID: 15098 RVA: 0x00102FE8 File Offset: 0x001011E8
		public static bool IsSelectionFadeEnabled
		{
			get
			{
				if (OSFeature.Feature.OnXp || OSFeature.Feature.OnWin2k)
				{
					int num = 0;
					UnsafeNativeMethods.SystemParametersInfo(4116, 0, ref num, 0);
					return num != 0;
				}
				return false;
			}
		}

		/// <summary>Gets a value indicating whether <see cref="T:System.Windows.Forms.ToolTip" /> animation is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if <see cref="T:System.Windows.Forms.ToolTip" /> animation is enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x06003AFB RID: 15099 RVA: 0x00103024 File Offset: 0x00101224
		public static bool IsToolTipAnimationEnabled
		{
			get
			{
				if (OSFeature.Feature.OnXp || OSFeature.Feature.OnWin2k)
				{
					int num = 0;
					UnsafeNativeMethods.SystemParametersInfo(4118, 0, ref num, 0);
					return num != 0;
				}
				return false;
			}
		}

		/// <summary>Gets a value indicating whether user interface (UI) effects are enabled or disabled.</summary>
		/// <returns>
		///   <see langword="true" /> if UI effects are enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x06003AFC RID: 15100 RVA: 0x00103060 File Offset: 0x00101260
		public static bool UIEffectsEnabled
		{
			get
			{
				if (OSFeature.Feature.OnXp || OSFeature.Feature.OnWin2k)
				{
					int num = 0;
					UnsafeNativeMethods.SystemParametersInfo(4158, 0, ref num, 0);
					return num != 0;
				}
				return false;
			}
		}

		/// <summary>Gets a value indicating whether active window tracking is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if active window tracking is enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x06003AFD RID: 15101 RVA: 0x0010309C File Offset: 0x0010129C
		public static bool IsActiveWindowTrackingEnabled
		{
			get
			{
				int num = 0;
				UnsafeNativeMethods.SystemParametersInfo(4096, 0, ref num, 0);
				return num != 0;
			}
		}

		/// <summary>Gets the active window tracking delay.</summary>
		/// <returns>The active window tracking delay, in milliseconds.</returns>
		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x06003AFE RID: 15102 RVA: 0x001030C0 File Offset: 0x001012C0
		public static int ActiveWindowTrackingDelay
		{
			get
			{
				int num = 0;
				UnsafeNativeMethods.SystemParametersInfo(8194, 0, ref num, 0);
				return num;
			}
		}

		/// <summary>Gets a value indicating whether window minimize and restore animation is enabled.</summary>
		/// <returns>
		///   <see langword="true" /> if window minimize and restore animation is enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x06003AFF RID: 15103 RVA: 0x001030E0 File Offset: 0x001012E0
		public static bool IsMinimizeRestoreAnimationEnabled
		{
			get
			{
				int num = 0;
				UnsafeNativeMethods.SystemParametersInfo(72, 0, ref num, 0);
				return num != 0;
			}
		}

		/// <summary>Gets the border multiplier factor that is used when determining the thickness of a window's sizing border.</summary>
		/// <returns>The multiplier used to determine the thickness of a window's sizing border.</returns>
		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x06003B00 RID: 15104 RVA: 0x00103100 File Offset: 0x00101300
		public static int BorderMultiplierFactor
		{
			get
			{
				int num = 0;
				UnsafeNativeMethods.SystemParametersInfo(5, 0, ref num, 0);
				return num;
			}
		}

		/// <summary>Gets the caret blink time.</summary>
		/// <returns>The caret blink time.</returns>
		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x06003B01 RID: 15105 RVA: 0x0010311B File Offset: 0x0010131B
		public static int CaretBlinkTime
		{
			get
			{
				return (int)SafeNativeMethods.GetCaretBlinkTime();
			}
		}

		/// <summary>Gets the width, in pixels, of the caret in edit controls.</summary>
		/// <returns>The width, in pixels, of the caret in edit controls.</returns>
		/// <exception cref="T:System.NotSupportedException">The operating system does not support this feature.</exception>
		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x06003B02 RID: 15106 RVA: 0x00103124 File Offset: 0x00101324
		public static int CaretWidth
		{
			get
			{
				if (OSFeature.Feature.OnXp || OSFeature.Feature.OnWin2k)
				{
					int num = 0;
					UnsafeNativeMethods.SystemParametersInfo(8198, 0, ref num, 0);
					return num;
				}
				throw new NotSupportedException(SR.GetString("SystemInformationFeatureNotSupported"));
			}
		}

		/// <summary>Gets the amount of the delta value of a single mouse wheel rotation increment.</summary>
		/// <returns>The amount of the delta value of a single mouse wheel rotation increment.</returns>
		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x06003B03 RID: 15107 RVA: 0x0010316B File Offset: 0x0010136B
		public static int MouseWheelScrollDelta
		{
			get
			{
				return 120;
			}
		}

		/// <summary>Gets the thickness, in pixels, of the top and bottom edges of the system focus rectangle.</summary>
		/// <returns>The thickness, in pixels, of the top and bottom edges of the system focus rectangle.</returns>
		/// <exception cref="T:System.NotSupportedException">The operating system does not support this feature.</exception>
		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x06003B04 RID: 15108 RVA: 0x0010316F File Offset: 0x0010136F
		public static int VerticalFocusThickness
		{
			get
			{
				if (OSFeature.Feature.OnXp)
				{
					return UnsafeNativeMethods.GetSystemMetrics(84);
				}
				throw new NotSupportedException(SR.GetString("SystemInformationFeatureNotSupported"));
			}
		}

		/// <summary>Gets the thickness of the left and right edges of the system focus rectangle, in pixels.</summary>
		/// <returns>The thickness of the left and right edges of the system focus rectangle, in pixels.</returns>
		/// <exception cref="T:System.NotSupportedException">The operating system does not support this feature.</exception>
		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x06003B05 RID: 15109 RVA: 0x00103194 File Offset: 0x00101394
		public static int HorizontalFocusThickness
		{
			get
			{
				if (OSFeature.Feature.OnXp)
				{
					return UnsafeNativeMethods.GetSystemMetrics(83);
				}
				throw new NotSupportedException(SR.GetString("SystemInformationFeatureNotSupported"));
			}
		}

		/// <summary>Gets the thickness, in pixels, of the top and bottom edges of the sizing border around the perimeter of a window being resized.</summary>
		/// <returns>The height, in pixels, of the top and bottom edges of the sizing border around the perimeter of a window being resized, in pixels.</returns>
		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x06003B06 RID: 15110 RVA: 0x001031B9 File Offset: 0x001013B9
		public static int VerticalResizeBorderThickness
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(33);
			}
		}

		/// <summary>Gets the thickness of the left and right edges of the sizing border around the perimeter of a window being resized, in pixels.</summary>
		/// <returns>The width of the left and right edges of the sizing border around the perimeter of a window being resized, in pixels.</returns>
		// Token: 0x17000E61 RID: 3681
		// (get) Token: 0x06003B07 RID: 15111 RVA: 0x001031C2 File Offset: 0x001013C2
		public static int HorizontalResizeBorderThickness
		{
			get
			{
				return UnsafeNativeMethods.GetSystemMetrics(32);
			}
		}

		/// <summary>Gets the orientation of the screen.</summary>
		/// <returns>The orientation of the screen, in degrees.</returns>
		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x06003B08 RID: 15112 RVA: 0x001031CC File Offset: 0x001013CC
		public static ScreenOrientation ScreenOrientation
		{
			get
			{
				ScreenOrientation screenOrientation = ScreenOrientation.Angle0;
				NativeMethods.DEVMODE devmode = default(NativeMethods.DEVMODE);
				devmode.dmSize = (short)Marshal.SizeOf(typeof(NativeMethods.DEVMODE));
				devmode.dmDriverExtra = 0;
				try
				{
					SafeNativeMethods.EnumDisplaySettings(null, -1, ref devmode);
					if ((devmode.dmFields & 128) > 0)
					{
						screenOrientation = devmode.dmDisplayOrientation;
					}
				}
				catch
				{
				}
				return screenOrientation;
			}
		}

		/// <summary>Gets the width, in pixels, of the sizing border drawn around the perimeter of a window being resized.</summary>
		/// <returns>The width, in pixels, of the window sizing border drawn around the perimeter of a window being resized.</returns>
		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x06003B09 RID: 15113 RVA: 0x00103238 File Offset: 0x00101438
		public static int SizingBorderWidth
		{
			get
			{
				NativeMethods.NONCLIENTMETRICS nonclientmetrics = new NativeMethods.NONCLIENTMETRICS();
				bool flag = UnsafeNativeMethods.SystemParametersInfo(41, nonclientmetrics.cbSize, nonclientmetrics, 0);
				if (flag && nonclientmetrics.iBorderWidth > 0)
				{
					return nonclientmetrics.iBorderWidth;
				}
				return 0;
			}
		}

		/// <summary>Gets the width, in pixels, of small caption buttons, and the height, in pixels, of small captions.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the width, in pixels, of small caption buttons, and the height, in pixels, of small captions.</returns>
		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x06003B0A RID: 15114 RVA: 0x00103270 File Offset: 0x00101470
		public static Size SmallCaptionButtonSize
		{
			get
			{
				NativeMethods.NONCLIENTMETRICS nonclientmetrics = new NativeMethods.NONCLIENTMETRICS();
				bool flag = UnsafeNativeMethods.SystemParametersInfo(41, nonclientmetrics.cbSize, nonclientmetrics, 0);
				if (flag && nonclientmetrics.iSmCaptionHeight > 0 && nonclientmetrics.iSmCaptionWidth > 0)
				{
					return new Size(nonclientmetrics.iSmCaptionWidth, nonclientmetrics.iSmCaptionHeight);
				}
				return Size.Empty;
			}
		}

		/// <summary>Gets the default width, in pixels, for menu-bar buttons and the height, in pixels, of a menu bar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that indicates the default width for menu-bar buttons, in pixels, and the height of a menu bar, in pixels.</returns>
		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x06003B0B RID: 15115 RVA: 0x001032C0 File Offset: 0x001014C0
		public static Size MenuBarButtonSize
		{
			get
			{
				NativeMethods.NONCLIENTMETRICS nonclientmetrics = new NativeMethods.NONCLIENTMETRICS();
				bool flag = UnsafeNativeMethods.SystemParametersInfo(41, nonclientmetrics.cbSize, nonclientmetrics, 0);
				if (flag && nonclientmetrics.iMenuHeight > 0 && nonclientmetrics.iMenuWidth > 0)
				{
					return new Size(nonclientmetrics.iMenuWidth, nonclientmetrics.iMenuHeight);
				}
				return Size.Empty;
			}
		}

		// Token: 0x06003B0C RID: 15116 RVA: 0x00103310 File Offset: 0x00101510
		internal static bool InLockedTerminalSession()
		{
			bool flag = false;
			if (SystemInformation.TerminalServerSession)
			{
				IntPtr intPtr = SafeNativeMethods.OpenInputDesktop(0, false, 256);
				if (intPtr == IntPtr.Zero)
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					flag = lastWin32Error == 5;
				}
				if (intPtr != IntPtr.Zero)
				{
					SafeNativeMethods.CloseDesktop(intPtr);
				}
			}
			return flag;
		}

		// Token: 0x04002320 RID: 8992
		private static bool checkMultiMonitorSupport = false;

		// Token: 0x04002321 RID: 8993
		private static bool multiMonitorSupport = false;

		// Token: 0x04002322 RID: 8994
		private static bool checkNativeMouseWheelSupport = false;

		// Token: 0x04002323 RID: 8995
		private static bool nativeMouseWheelSupport = true;

		// Token: 0x04002324 RID: 8996
		private static bool highContrast = false;

		// Token: 0x04002325 RID: 8997
		private static bool systemEventsAttached = false;

		// Token: 0x04002326 RID: 8998
		private static bool systemEventsDirty = true;

		// Token: 0x04002327 RID: 8999
		private static IntPtr processWinStation = IntPtr.Zero;

		// Token: 0x04002328 RID: 9000
		private static bool isUserInteractive = false;

		// Token: 0x04002329 RID: 9001
		private static PowerStatus powerStatus = null;

		// Token: 0x0400232A RID: 9002
		private const int DefaultMouseWheelScrollLines = 3;
	}
}
