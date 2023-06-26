using System;
using System.Internal;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Windows.Forms.Internal
{
	// Token: 0x020004E2 RID: 1250
	[SuppressUnmanagedCodeSecurity]
	internal static class IntUnsafeNativeMethods
	{
		// Token: 0x0600514A RID: 20810
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "GetDC", ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr IntGetDC(HandleRef hWnd);

		// Token: 0x0600514B RID: 20811 RVA: 0x0015309C File Offset: 0x0015129C
		public static IntPtr GetDC(HandleRef hWnd)
		{
			return System.Internal.HandleCollector.Add(IntUnsafeNativeMethods.IntGetDC(hWnd), IntSafeNativeMethods.CommonHandles.HDC);
		}

		// Token: 0x0600514C RID: 20812
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "DeleteDC", ExactSpelling = true, SetLastError = true)]
		public static extern bool IntDeleteDC(HandleRef hDC);

		// Token: 0x0600514D RID: 20813 RVA: 0x001530BC File Offset: 0x001512BC
		public static bool DeleteDC(HandleRef hDC)
		{
			System.Internal.HandleCollector.Remove((IntPtr)hDC, IntSafeNativeMethods.CommonHandles.GDI);
			return IntUnsafeNativeMethods.IntDeleteDC(hDC);
		}

		// Token: 0x0600514E RID: 20814 RVA: 0x001530E4 File Offset: 0x001512E4
		public static bool DeleteHDC(HandleRef hDC)
		{
			System.Internal.HandleCollector.Remove((IntPtr)hDC, IntSafeNativeMethods.CommonHandles.HDC);
			return IntUnsafeNativeMethods.IntDeleteDC(hDC);
		}

		// Token: 0x0600514F RID: 20815
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "ReleaseDC", ExactSpelling = true, SetLastError = true)]
		public static extern int IntReleaseDC(HandleRef hWnd, HandleRef hDC);

		// Token: 0x06005150 RID: 20816 RVA: 0x0015310A File Offset: 0x0015130A
		public static int ReleaseDC(HandleRef hWnd, HandleRef hDC)
		{
			System.Internal.HandleCollector.Remove((IntPtr)hDC, IntSafeNativeMethods.CommonHandles.HDC);
			return IntUnsafeNativeMethods.IntReleaseDC(hWnd, hDC);
		}

		// Token: 0x06005151 RID: 20817
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateDC", SetLastError = true)]
		public static extern IntPtr IntCreateDC(string lpszDriverName, string lpszDeviceName, string lpszOutput, HandleRef lpInitData);

		// Token: 0x06005152 RID: 20818 RVA: 0x00153124 File Offset: 0x00151324
		public static IntPtr CreateDC(string lpszDriverName, string lpszDeviceName, string lpszOutput, HandleRef lpInitData)
		{
			return System.Internal.HandleCollector.Add(IntUnsafeNativeMethods.IntCreateDC(lpszDriverName, lpszDeviceName, lpszOutput, lpInitData), IntSafeNativeMethods.CommonHandles.HDC);
		}

		// Token: 0x06005153 RID: 20819
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateIC", SetLastError = true)]
		public static extern IntPtr IntCreateIC(string lpszDriverName, string lpszDeviceName, string lpszOutput, HandleRef lpInitData);

		// Token: 0x06005154 RID: 20820 RVA: 0x00153148 File Offset: 0x00151348
		public static IntPtr CreateIC(string lpszDriverName, string lpszDeviceName, string lpszOutput, HandleRef lpInitData)
		{
			return System.Internal.HandleCollector.Add(IntUnsafeNativeMethods.IntCreateIC(lpszDriverName, lpszDeviceName, lpszOutput, lpInitData), IntSafeNativeMethods.CommonHandles.HDC);
		}

		// Token: 0x06005155 RID: 20821
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateCompatibleDC", ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr IntCreateCompatibleDC(HandleRef hDC);

		// Token: 0x06005156 RID: 20822 RVA: 0x0015316C File Offset: 0x0015136C
		public static IntPtr CreateCompatibleDC(HandleRef hDC)
		{
			return System.Internal.HandleCollector.Add(IntUnsafeNativeMethods.IntCreateCompatibleDC(hDC), IntSafeNativeMethods.CommonHandles.GDI);
		}

		// Token: 0x06005157 RID: 20823
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "SaveDC", ExactSpelling = true, SetLastError = true)]
		public static extern int IntSaveDC(HandleRef hDC);

		// Token: 0x06005158 RID: 20824 RVA: 0x0015318C File Offset: 0x0015138C
		public static int SaveDC(HandleRef hDC)
		{
			return IntUnsafeNativeMethods.IntSaveDC(hDC);
		}

		// Token: 0x06005159 RID: 20825
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "RestoreDC", ExactSpelling = true, SetLastError = true)]
		public static extern bool IntRestoreDC(HandleRef hDC, int nSavedDC);

		// Token: 0x0600515A RID: 20826 RVA: 0x001531A4 File Offset: 0x001513A4
		public static bool RestoreDC(HandleRef hDC, int nSavedDC)
		{
			return IntUnsafeNativeMethods.IntRestoreDC(hDC, nSavedDC);
		}

		// Token: 0x0600515B RID: 20827
		[DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr WindowFromDC(HandleRef hDC);

		// Token: 0x0600515C RID: 20828
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int GetDeviceCaps(HandleRef hDC, int nIndex);

		// Token: 0x0600515D RID: 20829
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "OffsetViewportOrgEx", ExactSpelling = true, SetLastError = true)]
		public static extern bool IntOffsetViewportOrgEx(HandleRef hDC, int nXOffset, int nYOffset, [In] [Out] IntNativeMethods.POINT point);

		// Token: 0x0600515E RID: 20830 RVA: 0x001531BC File Offset: 0x001513BC
		public static bool OffsetViewportOrgEx(HandleRef hDC, int nXOffset, int nYOffset, [In] [Out] IntNativeMethods.POINT point)
		{
			return IntUnsafeNativeMethods.IntOffsetViewportOrgEx(hDC, nXOffset, nYOffset, point);
		}

		// Token: 0x0600515F RID: 20831
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "SetGraphicsMode", ExactSpelling = true, SetLastError = true)]
		public static extern int IntSetGraphicsMode(HandleRef hDC, int iMode);

		// Token: 0x06005160 RID: 20832 RVA: 0x001531D4 File Offset: 0x001513D4
		public static int SetGraphicsMode(HandleRef hDC, int iMode)
		{
			iMode = IntUnsafeNativeMethods.IntSetGraphicsMode(hDC, iMode);
			return iMode;
		}

		// Token: 0x06005161 RID: 20833
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int GetGraphicsMode(HandleRef hDC);

		// Token: 0x06005162 RID: 20834
		[DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
		public static extern int GetROP2(HandleRef hdc);

		// Token: 0x06005163 RID: 20835
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int SetROP2(HandleRef hDC, int nDrawMode);

		// Token: 0x06005164 RID: 20836
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CombineRgn", ExactSpelling = true, SetLastError = true)]
		public static extern IntNativeMethods.RegionFlags IntCombineRgn(HandleRef hRgnDest, HandleRef hRgnSrc1, HandleRef hRgnSrc2, RegionCombineMode combineMode);

		// Token: 0x06005165 RID: 20837 RVA: 0x001531E0 File Offset: 0x001513E0
		public static IntNativeMethods.RegionFlags CombineRgn(HandleRef hRgnDest, HandleRef hRgnSrc1, HandleRef hRgnSrc2, RegionCombineMode combineMode)
		{
			if (hRgnDest.Wrapper == null || hRgnSrc1.Wrapper == null || hRgnSrc2.Wrapper == null)
			{
				return IntNativeMethods.RegionFlags.ERROR;
			}
			return IntUnsafeNativeMethods.IntCombineRgn(hRgnDest, hRgnSrc1, hRgnSrc2, combineMode);
		}

		// Token: 0x06005166 RID: 20838
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "GetClipRgn", ExactSpelling = true, SetLastError = true)]
		public static extern int IntGetClipRgn(HandleRef hDC, HandleRef hRgn);

		// Token: 0x06005167 RID: 20839 RVA: 0x00153208 File Offset: 0x00151408
		public static int GetClipRgn(HandleRef hDC, HandleRef hRgn)
		{
			return IntUnsafeNativeMethods.IntGetClipRgn(hDC, hRgn);
		}

		// Token: 0x06005168 RID: 20840
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "SelectClipRgn", ExactSpelling = true, SetLastError = true)]
		public static extern IntNativeMethods.RegionFlags IntSelectClipRgn(HandleRef hDC, HandleRef hRgn);

		// Token: 0x06005169 RID: 20841 RVA: 0x00153220 File Offset: 0x00151420
		public static IntNativeMethods.RegionFlags SelectClipRgn(HandleRef hDC, HandleRef hRgn)
		{
			return IntUnsafeNativeMethods.IntSelectClipRgn(hDC, hRgn);
		}

		// Token: 0x0600516A RID: 20842
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "GetRgnBox", ExactSpelling = true, SetLastError = true)]
		public static extern IntNativeMethods.RegionFlags IntGetRgnBox(HandleRef hRgn, [In] [Out] ref IntNativeMethods.RECT clipRect);

		// Token: 0x0600516B RID: 20843 RVA: 0x00153238 File Offset: 0x00151438
		public static IntNativeMethods.RegionFlags GetRgnBox(HandleRef hRgn, [In] [Out] ref IntNativeMethods.RECT clipRect)
		{
			return IntUnsafeNativeMethods.IntGetRgnBox(hRgn, ref clipRect);
		}

		// Token: 0x0600516C RID: 20844
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateFontIndirect", SetLastError = true)]
		public static extern IntPtr IntCreateFontIndirect([MarshalAs(UnmanagedType.AsAny)] [In] [Out] object lf);

		// Token: 0x0600516D RID: 20845 RVA: 0x00153250 File Offset: 0x00151450
		public static IntPtr CreateFontIndirect(object lf)
		{
			return System.Internal.HandleCollector.Add(IntUnsafeNativeMethods.IntCreateFontIndirect(lf), IntSafeNativeMethods.CommonHandles.GDI);
		}

		// Token: 0x0600516E RID: 20846
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "DeleteObject", ExactSpelling = true, SetLastError = true)]
		public static extern bool IntDeleteObject(HandleRef hObject);

		// Token: 0x0600516F RID: 20847 RVA: 0x00153270 File Offset: 0x00151470
		public static bool DeleteObject(HandleRef hObject)
		{
			System.Internal.HandleCollector.Remove((IntPtr)hObject, IntSafeNativeMethods.CommonHandles.GDI);
			return IntUnsafeNativeMethods.IntDeleteObject(hObject);
		}

		// Token: 0x06005170 RID: 20848
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "GetObject", SetLastError = true)]
		public static extern int IntGetObject(HandleRef hBrush, int nSize, [In] [Out] IntNativeMethods.LOGBRUSH lb);

		// Token: 0x06005171 RID: 20849 RVA: 0x00153298 File Offset: 0x00151498
		public static int GetObject(HandleRef hBrush, IntNativeMethods.LOGBRUSH lb)
		{
			return IntUnsafeNativeMethods.IntGetObject(hBrush, Marshal.SizeOf(typeof(IntNativeMethods.LOGBRUSH)), lb);
		}

		// Token: 0x06005172 RID: 20850
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "GetObject", SetLastError = true)]
		public static extern int IntGetObject(HandleRef hFont, int nSize, [In] [Out] IntNativeMethods.LOGFONT lf);

		// Token: 0x06005173 RID: 20851 RVA: 0x001532C0 File Offset: 0x001514C0
		public static int GetObject(HandleRef hFont, IntNativeMethods.LOGFONT lp)
		{
			return IntUnsafeNativeMethods.IntGetObject(hFont, Marshal.SizeOf(typeof(IntNativeMethods.LOGFONT)), lp);
		}

		// Token: 0x06005174 RID: 20852
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "SelectObject", ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr IntSelectObject(HandleRef hdc, HandleRef obj);

		// Token: 0x06005175 RID: 20853 RVA: 0x001532E8 File Offset: 0x001514E8
		public static IntPtr SelectObject(HandleRef hdc, HandleRef obj)
		{
			return IntUnsafeNativeMethods.IntSelectObject(hdc, obj);
		}

		// Token: 0x06005176 RID: 20854
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "GetCurrentObject", ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr IntGetCurrentObject(HandleRef hDC, int uObjectType);

		// Token: 0x06005177 RID: 20855 RVA: 0x00153300 File Offset: 0x00151500
		public static IntPtr GetCurrentObject(HandleRef hDC, int uObjectType)
		{
			return IntUnsafeNativeMethods.IntGetCurrentObject(hDC, uObjectType);
		}

		// Token: 0x06005178 RID: 20856
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "GetStockObject", ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr IntGetStockObject(int nIndex);

		// Token: 0x06005179 RID: 20857 RVA: 0x00153318 File Offset: 0x00151518
		public static IntPtr GetStockObject(int nIndex)
		{
			return IntUnsafeNativeMethods.IntGetStockObject(nIndex);
		}

		// Token: 0x0600517A RID: 20858
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int GetNearestColor(HandleRef hDC, int color);

		// Token: 0x0600517B RID: 20859
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int SetTextColor(HandleRef hDC, int crColor);

		// Token: 0x0600517C RID: 20860
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int GetTextAlign(HandleRef hdc);

		// Token: 0x0600517D RID: 20861
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int GetTextColor(HandleRef hDC);

		// Token: 0x0600517E RID: 20862
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int SetBkColor(HandleRef hDC, int clr);

		// Token: 0x0600517F RID: 20863
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "SetBkMode", ExactSpelling = true, SetLastError = true)]
		public static extern int IntSetBkMode(HandleRef hDC, int nBkMode);

		// Token: 0x06005180 RID: 20864 RVA: 0x00153330 File Offset: 0x00151530
		public static int SetBkMode(HandleRef hDC, int nBkMode)
		{
			return IntUnsafeNativeMethods.IntSetBkMode(hDC, nBkMode);
		}

		// Token: 0x06005181 RID: 20865
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "GetBkMode", ExactSpelling = true, SetLastError = true)]
		public static extern int IntGetBkMode(HandleRef hDC);

		// Token: 0x06005182 RID: 20866 RVA: 0x00153348 File Offset: 0x00151548
		public static int GetBkMode(HandleRef hDC)
		{
			return IntUnsafeNativeMethods.IntGetBkMode(hDC);
		}

		// Token: 0x06005183 RID: 20867
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int GetBkColor(HandleRef hDC);

		// Token: 0x06005184 RID: 20868
		[DllImport("user32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		public static extern int DrawTextW(HandleRef hDC, string lpszString, int nCount, ref IntNativeMethods.RECT lpRect, int nFormat);

		// Token: 0x06005185 RID: 20869
		[DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern int DrawTextA(HandleRef hDC, byte[] lpszString, int byteCount, ref IntNativeMethods.RECT lpRect, int nFormat);

		// Token: 0x06005186 RID: 20870 RVA: 0x00153360 File Offset: 0x00151560
		public static int DrawText(HandleRef hDC, string text, ref IntNativeMethods.RECT lpRect, int nFormat)
		{
			int num2;
			if (Marshal.SystemDefaultCharSize == 1)
			{
				lpRect.top = Math.Min(32767, lpRect.top);
				lpRect.left = Math.Min(32767, lpRect.left);
				lpRect.right = Math.Min(32767, lpRect.right);
				lpRect.bottom = Math.Min(32767, lpRect.bottom);
				int num = IntUnsafeNativeMethods.WideCharToMultiByte(0, 0, text, text.Length, null, 0, IntPtr.Zero, IntPtr.Zero);
				byte[] array = new byte[num];
				IntUnsafeNativeMethods.WideCharToMultiByte(0, 0, text, text.Length, array, array.Length, IntPtr.Zero, IntPtr.Zero);
				num = Math.Min(num, 8192);
				num2 = IntUnsafeNativeMethods.DrawTextA(hDC, array, num, ref lpRect, nFormat);
			}
			else
			{
				num2 = IntUnsafeNativeMethods.DrawTextW(hDC, text, text.Length, ref lpRect, nFormat);
			}
			return num2;
		}

		// Token: 0x06005187 RID: 20871
		[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int DrawTextExW(HandleRef hDC, string lpszString, int nCount, ref IntNativeMethods.RECT lpRect, int nFormat, [In] [Out] IntNativeMethods.DRAWTEXTPARAMS lpDTParams);

		// Token: 0x06005188 RID: 20872
		[DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
		public static extern int DrawTextExA(HandleRef hDC, byte[] lpszString, int byteCount, ref IntNativeMethods.RECT lpRect, int nFormat, [In] [Out] IntNativeMethods.DRAWTEXTPARAMS lpDTParams);

		// Token: 0x06005189 RID: 20873 RVA: 0x0015343C File Offset: 0x0015163C
		public static int DrawTextEx(HandleRef hDC, string text, ref IntNativeMethods.RECT lpRect, int nFormat, [In] [Out] IntNativeMethods.DRAWTEXTPARAMS lpDTParams)
		{
			int num2;
			if (Marshal.SystemDefaultCharSize == 1)
			{
				lpRect.top = Math.Min(32767, lpRect.top);
				lpRect.left = Math.Min(32767, lpRect.left);
				lpRect.right = Math.Min(32767, lpRect.right);
				lpRect.bottom = Math.Min(32767, lpRect.bottom);
				int num = IntUnsafeNativeMethods.WideCharToMultiByte(0, 0, text, text.Length, null, 0, IntPtr.Zero, IntPtr.Zero);
				byte[] array = new byte[num];
				IntUnsafeNativeMethods.WideCharToMultiByte(0, 0, text, text.Length, array, array.Length, IntPtr.Zero, IntPtr.Zero);
				num = Math.Min(num, 8192);
				num2 = IntUnsafeNativeMethods.DrawTextExA(hDC, array, num, ref lpRect, nFormat, lpDTParams);
			}
			else
			{
				num2 = IntUnsafeNativeMethods.DrawTextExW(hDC, text, text.Length, ref lpRect, nFormat, lpDTParams);
			}
			return num2;
		}

		// Token: 0x0600518A RID: 20874
		[DllImport("gdi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		public static extern int GetTextExtentPoint32W(HandleRef hDC, string text, int len, [In] [Out] IntNativeMethods.SIZE size);

		// Token: 0x0600518B RID: 20875
		[DllImport("gdi32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern int GetTextExtentPoint32A(HandleRef hDC, byte[] lpszString, int byteCount, [In] [Out] IntNativeMethods.SIZE size);

		// Token: 0x0600518C RID: 20876 RVA: 0x0015351C File Offset: 0x0015171C
		public static int GetTextExtentPoint32(HandleRef hDC, string text, [In] [Out] IntNativeMethods.SIZE size)
		{
			int num = text.Length;
			int num2;
			if (Marshal.SystemDefaultCharSize == 1)
			{
				num = IntUnsafeNativeMethods.WideCharToMultiByte(0, 0, text, text.Length, null, 0, IntPtr.Zero, IntPtr.Zero);
				byte[] array = new byte[num];
				IntUnsafeNativeMethods.WideCharToMultiByte(0, 0, text, text.Length, array, array.Length, IntPtr.Zero, IntPtr.Zero);
				num = Math.Min(text.Length, 8192);
				num2 = IntUnsafeNativeMethods.GetTextExtentPoint32A(hDC, array, num, size);
			}
			else
			{
				num2 = IntUnsafeNativeMethods.GetTextExtentPoint32W(hDC, text, text.Length, size);
			}
			return num2;
		}

		// Token: 0x0600518D RID: 20877
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool ExtTextOut(HandleRef hdc, int x, int y, int options, ref IntNativeMethods.RECT rect, string str, int length, int[] spacing);

		// Token: 0x0600518E RID: 20878
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "LineTo", ExactSpelling = true, SetLastError = true)]
		public static extern bool IntLineTo(HandleRef hdc, int x, int y);

		// Token: 0x0600518F RID: 20879 RVA: 0x001535A4 File Offset: 0x001517A4
		public static bool LineTo(HandleRef hdc, int x, int y)
		{
			return IntUnsafeNativeMethods.IntLineTo(hdc, x, y);
		}

		// Token: 0x06005190 RID: 20880
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "MoveToEx", ExactSpelling = true, SetLastError = true)]
		public static extern bool IntMoveToEx(HandleRef hdc, int x, int y, IntNativeMethods.POINT pt);

		// Token: 0x06005191 RID: 20881 RVA: 0x001535BC File Offset: 0x001517BC
		public static bool MoveToEx(HandleRef hdc, int x, int y, IntNativeMethods.POINT pt)
		{
			return IntUnsafeNativeMethods.IntMoveToEx(hdc, x, y, pt);
		}

		// Token: 0x06005192 RID: 20882
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "Rectangle", ExactSpelling = true, SetLastError = true)]
		public static extern bool IntRectangle(HandleRef hdc, int left, int top, int right, int bottom);

		// Token: 0x06005193 RID: 20883 RVA: 0x001535D4 File Offset: 0x001517D4
		public static bool Rectangle(HandleRef hdc, int left, int top, int right, int bottom)
		{
			return IntUnsafeNativeMethods.IntRectangle(hdc, left, top, right, bottom);
		}

		// Token: 0x06005194 RID: 20884
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "FillRect", ExactSpelling = true, SetLastError = true)]
		public static extern bool IntFillRect(HandleRef hdc, [In] ref IntNativeMethods.RECT rect, HandleRef hbrush);

		// Token: 0x06005195 RID: 20885 RVA: 0x001535F0 File Offset: 0x001517F0
		public static bool FillRect(HandleRef hDC, [In] ref IntNativeMethods.RECT rect, HandleRef hbrush)
		{
			return IntUnsafeNativeMethods.IntFillRect(hDC, ref rect, hbrush);
		}

		// Token: 0x06005196 RID: 20886
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "SetMapMode", ExactSpelling = true, SetLastError = true)]
		public static extern int IntSetMapMode(HandleRef hDC, int nMapMode);

		// Token: 0x06005197 RID: 20887 RVA: 0x00153608 File Offset: 0x00151808
		public static int SetMapMode(HandleRef hDC, int nMapMode)
		{
			return IntUnsafeNativeMethods.IntSetMapMode(hDC, nMapMode);
		}

		// Token: 0x06005198 RID: 20888
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "GetMapMode", ExactSpelling = true, SetLastError = true)]
		public static extern int IntGetMapMode(HandleRef hDC);

		// Token: 0x06005199 RID: 20889 RVA: 0x00153620 File Offset: 0x00151820
		public static int GetMapMode(HandleRef hDC)
		{
			return IntUnsafeNativeMethods.IntGetMapMode(hDC);
		}

		// Token: 0x0600519A RID: 20890
		[DllImport("gdi32.dll", EntryPoint = "GetViewportExtEx", ExactSpelling = true, SetLastError = true)]
		public static extern bool IntGetViewportExtEx(HandleRef hdc, [In] [Out] IntNativeMethods.SIZE lpSize);

		// Token: 0x0600519B RID: 20891 RVA: 0x00153638 File Offset: 0x00151838
		public static bool GetViewportExtEx(HandleRef hdc, [In] [Out] IntNativeMethods.SIZE lpSize)
		{
			return IntUnsafeNativeMethods.IntGetViewportExtEx(hdc, lpSize);
		}

		// Token: 0x0600519C RID: 20892
		[DllImport("gdi32.dll", EntryPoint = "GetViewportOrgEx", ExactSpelling = true, SetLastError = true)]
		public static extern bool IntGetViewportOrgEx(HandleRef hdc, [In] [Out] IntNativeMethods.POINT lpPoint);

		// Token: 0x0600519D RID: 20893 RVA: 0x00153650 File Offset: 0x00151850
		public static bool GetViewportOrgEx(HandleRef hdc, [In] [Out] IntNativeMethods.POINT lpPoint)
		{
			return IntUnsafeNativeMethods.IntGetViewportOrgEx(hdc, lpPoint);
		}

		// Token: 0x0600519E RID: 20894
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "SetViewportExtEx", ExactSpelling = true, SetLastError = true)]
		public static extern bool IntSetViewportExtEx(HandleRef hDC, int x, int y, [In] [Out] IntNativeMethods.SIZE size);

		// Token: 0x0600519F RID: 20895 RVA: 0x00153668 File Offset: 0x00151868
		public static bool SetViewportExtEx(HandleRef hDC, int x, int y, [In] [Out] IntNativeMethods.SIZE size)
		{
			return IntUnsafeNativeMethods.IntSetViewportExtEx(hDC, x, y, size);
		}

		// Token: 0x060051A0 RID: 20896
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "SetViewportOrgEx", ExactSpelling = true, SetLastError = true)]
		public static extern bool IntSetViewportOrgEx(HandleRef hDC, int x, int y, [In] [Out] IntNativeMethods.POINT point);

		// Token: 0x060051A1 RID: 20897 RVA: 0x00153680 File Offset: 0x00151880
		public static bool SetViewportOrgEx(HandleRef hDC, int x, int y, [In] [Out] IntNativeMethods.POINT point)
		{
			return IntUnsafeNativeMethods.IntSetViewportOrgEx(hDC, x, y, point);
		}

		// Token: 0x060051A2 RID: 20898
		[DllImport("gdi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		public static extern int GetTextMetricsW(HandleRef hDC, [In] [Out] ref IntNativeMethods.TEXTMETRIC lptm);

		// Token: 0x060051A3 RID: 20899
		[DllImport("gdi32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern int GetTextMetricsA(HandleRef hDC, [In] [Out] ref IntNativeMethods.TEXTMETRICA lptm);

		// Token: 0x060051A4 RID: 20900 RVA: 0x00153698 File Offset: 0x00151898
		public static int GetTextMetrics(HandleRef hDC, ref IntNativeMethods.TEXTMETRIC lptm)
		{
			int num;
			if (Marshal.SystemDefaultCharSize == 1)
			{
				IntNativeMethods.TEXTMETRICA textmetrica = default(IntNativeMethods.TEXTMETRICA);
				num = IntUnsafeNativeMethods.GetTextMetricsA(hDC, ref textmetrica);
				lptm.tmHeight = textmetrica.tmHeight;
				lptm.tmAscent = textmetrica.tmAscent;
				lptm.tmDescent = textmetrica.tmDescent;
				lptm.tmInternalLeading = textmetrica.tmInternalLeading;
				lptm.tmExternalLeading = textmetrica.tmExternalLeading;
				lptm.tmAveCharWidth = textmetrica.tmAveCharWidth;
				lptm.tmMaxCharWidth = textmetrica.tmMaxCharWidth;
				lptm.tmWeight = textmetrica.tmWeight;
				lptm.tmOverhang = textmetrica.tmOverhang;
				lptm.tmDigitizedAspectX = textmetrica.tmDigitizedAspectX;
				lptm.tmDigitizedAspectY = textmetrica.tmDigitizedAspectY;
				lptm.tmFirstChar = (char)textmetrica.tmFirstChar;
				lptm.tmLastChar = (char)textmetrica.tmLastChar;
				lptm.tmDefaultChar = (char)textmetrica.tmDefaultChar;
				lptm.tmBreakChar = (char)textmetrica.tmBreakChar;
				lptm.tmItalic = textmetrica.tmItalic;
				lptm.tmUnderlined = textmetrica.tmUnderlined;
				lptm.tmStruckOut = textmetrica.tmStruckOut;
				lptm.tmPitchAndFamily = textmetrica.tmPitchAndFamily;
				lptm.tmCharSet = textmetrica.tmCharSet;
			}
			else
			{
				num = IntUnsafeNativeMethods.GetTextMetricsW(hDC, ref lptm);
			}
			return num;
		}

		// Token: 0x060051A5 RID: 20901
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "BeginPath", ExactSpelling = true, SetLastError = true)]
		public static extern bool IntBeginPath(HandleRef hDC);

		// Token: 0x060051A6 RID: 20902 RVA: 0x001537BC File Offset: 0x001519BC
		public static bool BeginPath(HandleRef hDC)
		{
			return IntUnsafeNativeMethods.IntBeginPath(hDC);
		}

		// Token: 0x060051A7 RID: 20903
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "EndPath", ExactSpelling = true, SetLastError = true)]
		public static extern bool IntEndPath(HandleRef hDC);

		// Token: 0x060051A8 RID: 20904 RVA: 0x001537D4 File Offset: 0x001519D4
		public static bool EndPath(HandleRef hDC)
		{
			return IntUnsafeNativeMethods.IntEndPath(hDC);
		}

		// Token: 0x060051A9 RID: 20905
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "StrokePath", ExactSpelling = true, SetLastError = true)]
		public static extern bool IntStrokePath(HandleRef hDC);

		// Token: 0x060051AA RID: 20906 RVA: 0x001537EC File Offset: 0x001519EC
		public static bool StrokePath(HandleRef hDC)
		{
			return IntUnsafeNativeMethods.IntStrokePath(hDC);
		}

		// Token: 0x060051AB RID: 20907
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "AngleArc", ExactSpelling = true, SetLastError = true)]
		public static extern bool IntAngleArc(HandleRef hDC, int x, int y, int radius, float startAngle, float endAngle);

		// Token: 0x060051AC RID: 20908 RVA: 0x00153804 File Offset: 0x00151A04
		public static bool AngleArc(HandleRef hDC, int x, int y, int radius, float startAngle, float endAngle)
		{
			return IntUnsafeNativeMethods.IntAngleArc(hDC, x, y, radius, startAngle, endAngle);
		}

		// Token: 0x060051AD RID: 20909
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "Arc", ExactSpelling = true, SetLastError = true)]
		public static extern bool IntArc(HandleRef hDC, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nXStartArc, int nYStartArc, int nXEndArc, int nYEndArc);

		// Token: 0x060051AE RID: 20910 RVA: 0x00153820 File Offset: 0x00151A20
		public static bool Arc(HandleRef hDC, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nXStartArc, int nYStartArc, int nXEndArc, int nYEndArc)
		{
			return IntUnsafeNativeMethods.IntArc(hDC, nLeftRect, nTopRect, nRightRect, nBottomRect, nXStartArc, nYStartArc, nXEndArc, nYEndArc);
		}

		// Token: 0x060051AF RID: 20911
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int SetTextAlign(HandleRef hDC, int nMode);

		// Token: 0x060051B0 RID: 20912
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "Ellipse", ExactSpelling = true, SetLastError = true)]
		public static extern bool IntEllipse(HandleRef hDc, int x1, int y1, int x2, int y2);

		// Token: 0x060051B1 RID: 20913 RVA: 0x00153844 File Offset: 0x00151A44
		public static bool Ellipse(HandleRef hDc, int x1, int y1, int x2, int y2)
		{
			return IntUnsafeNativeMethods.IntEllipse(hDc, x1, y1, x2, y2);
		}

		// Token: 0x060051B2 RID: 20914
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int WideCharToMultiByte(int codePage, int flags, [MarshalAs(UnmanagedType.LPWStr)] string wideStr, int chars, [In] [Out] byte[] pOutBytes, int bufferBytes, IntPtr defaultChar, IntPtr pDefaultUsed);
	}
}
