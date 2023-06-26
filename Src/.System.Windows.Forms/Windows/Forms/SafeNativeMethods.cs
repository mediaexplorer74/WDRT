using System;
using System.Drawing;
using System.Internal;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Text;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	// Token: 0x02000350 RID: 848
	[SuppressUnmanagedCodeSecurity]
	internal static class SafeNativeMethods
	{
		// Token: 0x060036AF RID: 13999
		[DllImport("shlwapi.dll")]
		public static extern int SHAutoComplete(HandleRef hwndEdit, int flags);

		// Token: 0x060036B0 RID: 14000
		[DllImport("user32.dll")]
		public static extern int OemKeyScan(short wAsciiVal);

		// Token: 0x060036B1 RID: 14001
		[DllImport("gdi32.dll")]
		public static extern int GetSystemPaletteEntries(HandleRef hdc, int iStartIndex, int nEntries, byte[] lppe);

		// Token: 0x060036B2 RID: 14002
		[DllImport("gdi32.dll")]
		public static extern int GetDIBits(HandleRef hdc, HandleRef hbm, int uStartScan, int cScanLines, byte[] lpvBits, ref NativeMethods.BITMAPINFO_FLAT bmi, int uUsage);

		// Token: 0x060036B3 RID: 14003
		[DllImport("gdi32.dll")]
		public static extern int StretchDIBits(HandleRef hdc, int XDest, int YDest, int nDestWidth, int nDestHeight, int XSrc, int YSrc, int nSrcWidth, int nSrcHeight, byte[] lpBits, ref NativeMethods.BITMAPINFO_FLAT lpBitsInfo, int iUsage, int dwRop);

		// Token: 0x060036B4 RID: 14004
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateCompatibleBitmap", ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr IntCreateCompatibleBitmap(HandleRef hDC, int width, int height);

		// Token: 0x060036B5 RID: 14005 RVA: 0x000F7523 File Offset: 0x000F5723
		public static IntPtr CreateCompatibleBitmap(HandleRef hDC, int width, int height)
		{
			return System.Internal.HandleCollector.Add(SafeNativeMethods.IntCreateCompatibleBitmap(hDC, width, height), NativeMethods.CommonHandles.GDI);
		}

		// Token: 0x060036B6 RID: 14006
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool GetScrollInfo(HandleRef hWnd, int fnBar, [In] [Out] NativeMethods.SCROLLINFO si);

		// Token: 0x060036B7 RID: 14007
		[DllImport("ole32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool IsAccelerator(HandleRef hAccel, int cAccelEntries, [In] ref NativeMethods.MSG lpMsg, short[] lpwCmd);

		// Token: 0x060036B8 RID: 14008
		[DllImport("comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool ChooseFont([In] [Out] NativeMethods.CHOOSEFONT cf);

		// Token: 0x060036B9 RID: 14009
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int GetBitmapBits(HandleRef hbmp, int cbBuffer, byte[] lpvBits);

		// Token: 0x060036BA RID: 14010
		[DllImport("comdlg32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int CommDlgExtendedError();

		// Token: 0x060036BB RID: 14011
		[DllImport("oleaut32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern void SysFreeString(HandleRef bstr);

		// Token: 0x060036BC RID: 14012
		[DllImport("oleaut32.dll", PreserveSig = false)]
		public static extern void OleCreatePropertyFrame(HandleRef hwndOwner, int x, int y, [MarshalAs(UnmanagedType.LPWStr)] string caption, int objects, [MarshalAs(UnmanagedType.Interface)] ref object pobjs, int pages, HandleRef pClsid, int locale, int reserved1, IntPtr reserved2);

		// Token: 0x060036BD RID: 14013
		[DllImport("oleaut32.dll", PreserveSig = false)]
		public static extern void OleCreatePropertyFrame(HandleRef hwndOwner, int x, int y, [MarshalAs(UnmanagedType.LPWStr)] string caption, int objects, [MarshalAs(UnmanagedType.Interface)] ref object pobjs, int pages, Guid[] pClsid, int locale, int reserved1, IntPtr reserved2);

		// Token: 0x060036BE RID: 14014
		[DllImport("oleaut32.dll", PreserveSig = false)]
		public static extern void OleCreatePropertyFrame(HandleRef hwndOwner, int x, int y, [MarshalAs(UnmanagedType.LPWStr)] string caption, int objects, HandleRef lplpobjs, int pages, HandleRef pClsid, int locale, int reserved1, IntPtr reserved2);

		// Token: 0x060036BF RID: 14015
		[DllImport("hhctrl.ocx", CharSet = CharSet.Auto)]
		public static extern int HtmlHelp(HandleRef hwndCaller, [MarshalAs(UnmanagedType.LPTStr)] string pszFile, int uCommand, int dwData);

		// Token: 0x060036C0 RID: 14016
		[DllImport("hhctrl.ocx", CharSet = CharSet.Auto)]
		public static extern int HtmlHelp(HandleRef hwndCaller, [MarshalAs(UnmanagedType.LPTStr)] string pszFile, int uCommand, string dwData);

		// Token: 0x060036C1 RID: 14017
		[DllImport("hhctrl.ocx", CharSet = CharSet.Auto)]
		public static extern int HtmlHelp(HandleRef hwndCaller, [MarshalAs(UnmanagedType.LPTStr)] string pszFile, int uCommand, [MarshalAs(UnmanagedType.LPStruct)] NativeMethods.HH_POPUP dwData);

		// Token: 0x060036C2 RID: 14018
		[DllImport("hhctrl.ocx", CharSet = CharSet.Auto)]
		public static extern int HtmlHelp(HandleRef hwndCaller, [MarshalAs(UnmanagedType.LPTStr)] string pszFile, int uCommand, [MarshalAs(UnmanagedType.LPStruct)] NativeMethods.HH_FTS_QUERY dwData);

		// Token: 0x060036C3 RID: 14019
		[DllImport("hhctrl.ocx", CharSet = CharSet.Auto)]
		public static extern int HtmlHelp(HandleRef hwndCaller, [MarshalAs(UnmanagedType.LPTStr)] string pszFile, int uCommand, [MarshalAs(UnmanagedType.LPStruct)] NativeMethods.HH_AKLINK dwData);

		// Token: 0x060036C4 RID: 14020
		[DllImport("oleaut32.dll")]
		public static extern void VariantInit(HandleRef pObject);

		// Token: 0x060036C5 RID: 14021
		[DllImport("oleaut32.dll", PreserveSig = false)]
		public static extern void VariantClear(HandleRef pObject);

		// Token: 0x060036C6 RID: 14022
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern bool LineTo(HandleRef hdc, int x, int y);

		// Token: 0x060036C7 RID: 14023
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern bool MoveToEx(HandleRef hdc, int x, int y, NativeMethods.POINT pt);

		// Token: 0x060036C8 RID: 14024
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern bool Rectangle(HandleRef hdc, int left, int top, int right, int bottom);

		// Token: 0x060036C9 RID: 14025
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern bool PatBlt(HandleRef hdc, int left, int top, int width, int height, int rop);

		// Token: 0x060036CA RID: 14026
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, EntryPoint = "GetThreadLocale")]
		public static extern int GetThreadLCID();

		// Token: 0x060036CB RID: 14027
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int GetMessagePos();

		// Token: 0x060036CC RID: 14028
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int RegisterClipboardFormat(string format);

		// Token: 0x060036CD RID: 14029
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int GetClipboardFormatName(int format, StringBuilder lpString, int cchMax);

		// Token: 0x060036CE RID: 14030
		[DllImport("comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool ChooseColor([In] [Out] NativeMethods.CHOOSECOLOR cc);

		// Token: 0x060036CF RID: 14031
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int RegisterWindowMessage(string msg);

		// Token: 0x060036D0 RID: 14032
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "DeleteObject", ExactSpelling = true, SetLastError = true)]
		public static extern bool ExternalDeleteObject(HandleRef hObject);

		// Token: 0x060036D1 RID: 14033
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "DeleteObject", ExactSpelling = true, SetLastError = true)]
		internal static extern bool IntDeleteObject(HandleRef hObject);

		// Token: 0x060036D2 RID: 14034 RVA: 0x000F7537 File Offset: 0x000F5737
		public static bool DeleteObject(HandleRef hObject)
		{
			System.Internal.HandleCollector.Remove((IntPtr)hObject, NativeMethods.CommonHandles.GDI);
			return SafeNativeMethods.IntDeleteObject(hObject);
		}

		// Token: 0x060036D3 RID: 14035
		[DllImport("oleaut32.dll", EntryPoint = "OleCreateFontIndirect", ExactSpelling = true, PreserveSig = false)]
		public static extern SafeNativeMethods.IFontDisp OleCreateIFontDispIndirect(NativeMethods.FONTDESC fd, ref Guid iid);

		// Token: 0x060036D4 RID: 14036
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateSolidBrush", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr IntCreateSolidBrush(int crColor);

		// Token: 0x060036D5 RID: 14037 RVA: 0x000F7550 File Offset: 0x000F5750
		public static IntPtr CreateSolidBrush(int crColor)
		{
			return System.Internal.HandleCollector.Add(SafeNativeMethods.IntCreateSolidBrush(crColor), NativeMethods.CommonHandles.GDI);
		}

		// Token: 0x060036D6 RID: 14038
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern bool SetWindowExtEx(HandleRef hDC, int x, int y, [In] [Out] NativeMethods.SIZE size);

		// Token: 0x060036D7 RID: 14039
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		public static extern int FormatMessage(int dwFlags, HandleRef lpSource, int dwMessageId, int dwLanguageId, StringBuilder lpBuffer, int nSize, HandleRef arguments);

		// Token: 0x060036D8 RID: 14040
		[DllImport("comctl32.dll")]
		public static extern void InitCommonControls();

		// Token: 0x060036D9 RID: 14041
		[DllImport("comctl32.dll")]
		public static extern bool InitCommonControlsEx(NativeMethods.INITCOMMONCONTROLSEX icc);

		// Token: 0x060036DA RID: 14042
		[DllImport("comctl32.dll")]
		public static extern IntPtr ImageList_Create(int cx, int cy, int flags, int cInitial, int cGrow);

		// Token: 0x060036DB RID: 14043
		[DllImport("comctl32.dll")]
		public static extern bool ImageList_Destroy(HandleRef himl);

		// Token: 0x060036DC RID: 14044
		[DllImport("comctl32.dll", EntryPoint = "ImageList_Destroy")]
		public static extern bool ImageList_Destroy_Native(HandleRef himl);

		// Token: 0x060036DD RID: 14045
		[DllImport("comctl32.dll")]
		public static extern int ImageList_GetImageCount(HandleRef himl);

		// Token: 0x060036DE RID: 14046
		[DllImport("comctl32.dll")]
		public static extern int ImageList_Add(HandleRef himl, HandleRef hbmImage, HandleRef hbmMask);

		// Token: 0x060036DF RID: 14047
		[DllImport("comctl32.dll")]
		public static extern int ImageList_ReplaceIcon(HandleRef himl, int index, HandleRef hicon);

		// Token: 0x060036E0 RID: 14048
		[DllImport("comctl32.dll")]
		public static extern int ImageList_SetBkColor(HandleRef himl, int clrBk);

		// Token: 0x060036E1 RID: 14049
		[DllImport("comctl32.dll")]
		public static extern bool ImageList_Draw(HandleRef himl, int i, HandleRef hdcDst, int x, int y, int fStyle);

		// Token: 0x060036E2 RID: 14050
		[DllImport("comctl32.dll")]
		public static extern bool ImageList_Replace(HandleRef himl, int i, HandleRef hbmImage, HandleRef hbmMask);

		// Token: 0x060036E3 RID: 14051
		[DllImport("comctl32.dll")]
		public static extern bool ImageList_DrawEx(HandleRef himl, int i, HandleRef hdcDst, int x, int y, int dx, int dy, int rgbBk, int rgbFg, int fStyle);

		// Token: 0x060036E4 RID: 14052
		[DllImport("comctl32.dll")]
		public static extern bool ImageList_GetIconSize(HandleRef himl, out int x, out int y);

		// Token: 0x060036E5 RID: 14053
		[DllImport("comctl32.dll")]
		public static extern IntPtr ImageList_Duplicate(HandleRef himl);

		// Token: 0x060036E6 RID: 14054
		[DllImport("comctl32.dll")]
		public static extern bool ImageList_Remove(HandleRef himl, int i);

		// Token: 0x060036E7 RID: 14055
		[DllImport("comctl32.dll")]
		public static extern bool ImageList_GetImageInfo(HandleRef himl, int i, NativeMethods.IMAGEINFO pImageInfo);

		// Token: 0x060036E8 RID: 14056
		[DllImport("comctl32.dll")]
		public static extern IntPtr ImageList_Read(UnsafeNativeMethods.IStream pstm);

		// Token: 0x060036E9 RID: 14057
		[DllImport("comctl32.dll")]
		public static extern bool ImageList_Write(HandleRef himl, UnsafeNativeMethods.IStream pstm);

		// Token: 0x060036EA RID: 14058
		[DllImport("comctl32.dll")]
		public static extern int ImageList_WriteEx(HandleRef himl, int dwFlags, UnsafeNativeMethods.IStream pstm);

		// Token: 0x060036EB RID: 14059
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool TrackPopupMenuEx(HandleRef hmenu, int fuFlags, int x, int y, HandleRef hwnd, NativeMethods.TPMPARAMS tpm);

		// Token: 0x060036EC RID: 14060
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetKeyboardLayout(int dwLayout);

		// Token: 0x060036ED RID: 14061
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr ActivateKeyboardLayout(HandleRef hkl, int uFlags);

		// Token: 0x060036EE RID: 14062
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int GetKeyboardLayoutList(int size, [MarshalAs(UnmanagedType.LPArray)] [Out] IntPtr[] hkls);

		// Token: 0x060036EF RID: 14063
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref NativeMethods.DEVMODE lpDevMode);

		// Token: 0x060036F0 RID: 14064
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool GetMonitorInfo(HandleRef hmonitor, [In] [Out] NativeMethods.MONITORINFOEX info);

		// Token: 0x060036F1 RID: 14065
		[DllImport("user32.dll", ExactSpelling = true)]
		public static extern IntPtr MonitorFromPoint(NativeMethods.POINTSTRUCT pt, int flags);

		// Token: 0x060036F2 RID: 14066
		[DllImport("user32.dll", ExactSpelling = true)]
		public static extern IntPtr MonitorFromRect(ref NativeMethods.RECT rect, int flags);

		// Token: 0x060036F3 RID: 14067
		[DllImport("user32.dll", ExactSpelling = true)]
		public static extern IntPtr MonitorFromWindow(HandleRef handle, int flags);

		// Token: 0x060036F4 RID: 14068
		[DllImport("user32.dll", ExactSpelling = true)]
		public static extern bool EnumDisplayMonitors(HandleRef hdc, NativeMethods.COMRECT rcClip, NativeMethods.MonitorEnumProc lpfnEnum, IntPtr dwData);

		// Token: 0x060036F5 RID: 14069
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateHalftonePalette", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr IntCreateHalftonePalette(HandleRef hdc);

		// Token: 0x060036F6 RID: 14070 RVA: 0x000F7562 File Offset: 0x000F5762
		public static IntPtr CreateHalftonePalette(HandleRef hdc)
		{
			return System.Internal.HandleCollector.Add(SafeNativeMethods.IntCreateHalftonePalette(hdc), NativeMethods.CommonHandles.GDI);
		}

		// Token: 0x060036F7 RID: 14071
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int GetPaletteEntries(HandleRef hpal, int iStartIndex, int nEntries, int[] lppe);

		// Token: 0x060036F8 RID: 14072
		[DllImport("gdi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		public static extern int GetTextMetricsW(HandleRef hDC, [In] [Out] ref NativeMethods.TEXTMETRIC lptm);

		// Token: 0x060036F9 RID: 14073
		[DllImport("gdi32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern int GetTextMetricsA(HandleRef hDC, [In] [Out] ref NativeMethods.TEXTMETRICA lptm);

		// Token: 0x060036FA RID: 14074 RVA: 0x000F7574 File Offset: 0x000F5774
		public static int GetTextMetrics(HandleRef hDC, ref NativeMethods.TEXTMETRIC lptm)
		{
			if (Marshal.SystemDefaultCharSize == 1)
			{
				NativeMethods.TEXTMETRICA textmetrica = default(NativeMethods.TEXTMETRICA);
				int textMetricsA = SafeNativeMethods.GetTextMetricsA(hDC, ref textmetrica);
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
				return textMetricsA;
			}
			return SafeNativeMethods.GetTextMetricsW(hDC, ref lptm);
		}

		// Token: 0x060036FB RID: 14075
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateDIBSection", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr IntCreateDIBSection(HandleRef hdc, HandleRef pbmi, int iUsage, byte[] ppvBits, IntPtr hSection, int dwOffset);

		// Token: 0x060036FC RID: 14076 RVA: 0x000F7696 File Offset: 0x000F5896
		public static IntPtr CreateDIBSection(HandleRef hdc, HandleRef pbmi, int iUsage, byte[] ppvBits, IntPtr hSection, int dwOffset)
		{
			return System.Internal.HandleCollector.Add(SafeNativeMethods.IntCreateDIBSection(hdc, pbmi, iUsage, ppvBits, hSection, dwOffset), NativeMethods.CommonHandles.GDI);
		}

		// Token: 0x060036FD RID: 14077
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateBitmap", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr IntCreateBitmap(int nWidth, int nHeight, int nPlanes, int nBitsPerPixel, IntPtr lpvBits);

		// Token: 0x060036FE RID: 14078 RVA: 0x000F76AF File Offset: 0x000F58AF
		public static IntPtr CreateBitmap(int nWidth, int nHeight, int nPlanes, int nBitsPerPixel, IntPtr lpvBits)
		{
			return System.Internal.HandleCollector.Add(SafeNativeMethods.IntCreateBitmap(nWidth, nHeight, nPlanes, nBitsPerPixel, lpvBits), NativeMethods.CommonHandles.GDI);
		}

		// Token: 0x060036FF RID: 14079
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateBitmap", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr IntCreateBitmapShort(int nWidth, int nHeight, int nPlanes, int nBitsPerPixel, short[] lpvBits);

		// Token: 0x06003700 RID: 14080 RVA: 0x000F76C6 File Offset: 0x000F58C6
		public static IntPtr CreateBitmap(int nWidth, int nHeight, int nPlanes, int nBitsPerPixel, short[] lpvBits)
		{
			return System.Internal.HandleCollector.Add(SafeNativeMethods.IntCreateBitmapShort(nWidth, nHeight, nPlanes, nBitsPerPixel, lpvBits), NativeMethods.CommonHandles.GDI);
		}

		// Token: 0x06003701 RID: 14081
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateBitmap", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr IntCreateBitmapByte(int nWidth, int nHeight, int nPlanes, int nBitsPerPixel, byte[] lpvBits);

		// Token: 0x06003702 RID: 14082 RVA: 0x000F76DD File Offset: 0x000F58DD
		public static IntPtr CreateBitmap(int nWidth, int nHeight, int nPlanes, int nBitsPerPixel, byte[] lpvBits)
		{
			return System.Internal.HandleCollector.Add(SafeNativeMethods.IntCreateBitmapByte(nWidth, nHeight, nPlanes, nBitsPerPixel, lpvBits), NativeMethods.CommonHandles.GDI);
		}

		// Token: 0x06003703 RID: 14083
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreatePatternBrush", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr IntCreatePatternBrush(HandleRef hbmp);

		// Token: 0x06003704 RID: 14084 RVA: 0x000F76F4 File Offset: 0x000F58F4
		public static IntPtr CreatePatternBrush(HandleRef hbmp)
		{
			return System.Internal.HandleCollector.Add(SafeNativeMethods.IntCreatePatternBrush(hbmp), NativeMethods.CommonHandles.GDI);
		}

		// Token: 0x06003705 RID: 14085
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateBrushIndirect", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr IntCreateBrushIndirect(NativeMethods.LOGBRUSH lb);

		// Token: 0x06003706 RID: 14086 RVA: 0x000F7706 File Offset: 0x000F5906
		public static IntPtr CreateBrushIndirect(NativeMethods.LOGBRUSH lb)
		{
			return System.Internal.HandleCollector.Add(SafeNativeMethods.IntCreateBrushIndirect(lb), NativeMethods.CommonHandles.GDI);
		}

		// Token: 0x06003707 RID: 14087
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreatePen", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr IntCreatePen(int nStyle, int nWidth, int crColor);

		// Token: 0x06003708 RID: 14088 RVA: 0x000F7718 File Offset: 0x000F5918
		public static IntPtr CreatePen(int nStyle, int nWidth, int crColor)
		{
			return System.Internal.HandleCollector.Add(SafeNativeMethods.IntCreatePen(nStyle, nWidth, crColor), NativeMethods.CommonHandles.GDI);
		}

		// Token: 0x06003709 RID: 14089
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern bool SetViewportExtEx(HandleRef hDC, int x, int y, NativeMethods.SIZE size);

		// Token: 0x0600370A RID: 14090
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr LoadCursor(HandleRef hInst, int iconId);

		// Token: 0x0600370B RID: 14091
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool GetClipCursor([In] [Out] ref NativeMethods.RECT lpRect);

		// Token: 0x0600370C RID: 14092
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetCursor();

		// Token: 0x0600370D RID: 14093
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool GetIconInfo(HandleRef hIcon, [In] [Out] NativeMethods.ICONINFO info);

		// Token: 0x0600370E RID: 14094
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int IntersectClipRect(HandleRef hDC, int x1, int y1, int x2, int y2);

		// Token: 0x0600370F RID: 14095
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "CopyImage", ExactSpelling = true)]
		private static extern IntPtr IntCopyImage(HandleRef hImage, int uType, int cxDesired, int cyDesired, int fuFlags);

		// Token: 0x06003710 RID: 14096 RVA: 0x000F772C File Offset: 0x000F592C
		public static IntPtr CopyImage(HandleRef hImage, int uType, int cxDesired, int cyDesired, int fuFlags)
		{
			return System.Internal.HandleCollector.Add(SafeNativeMethods.IntCopyImage(hImage, uType, cxDesired, cyDesired, fuFlags), NativeMethods.CommonHandles.GDI);
		}

		// Token: 0x06003711 RID: 14097 RVA: 0x000F7743 File Offset: 0x000F5943
		public static IntPtr CopyImageAsCursor(HandleRef hImage, int uType, int cxDesired, int cyDesired, int fuFlags)
		{
			return System.Internal.HandleCollector.Add(SafeNativeMethods.IntCopyImage(hImage, uType, cxDesired, cyDesired, fuFlags), NativeMethods.CommonHandles.Cursor);
		}

		// Token: 0x06003712 RID: 14098
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool AdjustWindowRectEx(ref NativeMethods.RECT lpRect, int dwStyle, bool bMenu, int dwExStyle);

		// Token: 0x06003713 RID: 14099
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool AdjustWindowRectExForDpi(ref NativeMethods.RECT lpRect, int dwStyle, bool bMenu, int dwExStyle, uint dpi);

		// Token: 0x06003714 RID: 14100
		[DllImport("ole32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int DoDragDrop(IDataObject dataObject, UnsafeNativeMethods.IOleDropSource dropSource, int allowedEffects, int[] finalEffect);

		// Token: 0x06003715 RID: 14101
		[DllImport("user32.dll", ExactSpelling = true)]
		public static extern int GetSysColor(int nIndex);

		// Token: 0x06003716 RID: 14102
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetSysColorBrush(int nIndex);

		// Token: 0x06003717 RID: 14103
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool EnableWindow(HandleRef hWnd, bool enable);

		// Token: 0x06003718 RID: 14104
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool GetClientRect(HandleRef hWnd, [In] [Out] ref NativeMethods.RECT rect);

		// Token: 0x06003719 RID: 14105
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int GetDoubleClickTime();

		// Token: 0x0600371A RID: 14106
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int GetUpdateRgn(HandleRef hwnd, HandleRef hrgn, bool fErase);

		// Token: 0x0600371B RID: 14107
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool ValidateRect(HandleRef hWnd, [In] [Out] ref NativeMethods.RECT rect);

		// Token: 0x0600371C RID: 14108
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int FillRect(HandleRef hdc, [In] ref NativeMethods.RECT rect, HandleRef hbrush);

		// Token: 0x0600371D RID: 14109
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int GetTextColor(HandleRef hDC);

		// Token: 0x0600371E RID: 14110
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int GetBkColor(HandleRef hDC);

		// Token: 0x0600371F RID: 14111
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int SetTextColor(HandleRef hDC, int crColor);

		// Token: 0x06003720 RID: 14112
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int SetBkColor(HandleRef hDC, int clr);

		// Token: 0x06003721 RID: 14113
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr SelectPalette(HandleRef hdc, HandleRef hpal, int bForceBackground);

		// Token: 0x06003722 RID: 14114
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern bool SetViewportOrgEx(HandleRef hDC, int x, int y, [In] [Out] NativeMethods.POINT point);

		// Token: 0x06003723 RID: 14115
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateRectRgn", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr IntCreateRectRgn(int x1, int y1, int x2, int y2);

		// Token: 0x06003724 RID: 14116 RVA: 0x000F775A File Offset: 0x000F595A
		public static IntPtr CreateRectRgn(int x1, int y1, int x2, int y2)
		{
			return System.Internal.HandleCollector.Add(SafeNativeMethods.IntCreateRectRgn(x1, y1, x2, y2), NativeMethods.CommonHandles.GDI);
		}

		// Token: 0x06003725 RID: 14117
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int CombineRgn(HandleRef hRgn, HandleRef hRgn1, HandleRef hRgn2, int nCombineMode);

		// Token: 0x06003726 RID: 14118
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int RealizePalette(HandleRef hDC);

		// Token: 0x06003727 RID: 14119
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern bool LPtoDP(HandleRef hDC, [In] [Out] ref NativeMethods.RECT lpRect, int nCount);

		// Token: 0x06003728 RID: 14120
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern bool SetWindowOrgEx(HandleRef hDC, int x, int y, [In] [Out] NativeMethods.POINT point);

		// Token: 0x06003729 RID: 14121
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern bool GetViewportOrgEx(HandleRef hDC, [In] [Out] NativeMethods.POINT point);

		// Token: 0x0600372A RID: 14122
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int SetMapMode(HandleRef hDC, int nMapMode);

		// Token: 0x0600372B RID: 14123
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool IsWindowEnabled(HandleRef hWnd);

		// Token: 0x0600372C RID: 14124
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool IsWindowVisible(HandleRef hWnd);

		// Token: 0x0600372D RID: 14125
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool ReleaseCapture();

		// Token: 0x0600372E RID: 14126
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int GetCurrentThreadId();

		// Token: 0x0600372F RID: 14127
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool EnumWindows(SafeNativeMethods.EnumThreadWindowsCallback callback, IntPtr extraData);

		// Token: 0x06003730 RID: 14128
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int GetWindowThreadProcessId(HandleRef hWnd, out int lpdwProcessId);

		// Token: 0x06003731 RID: 14129
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetExitCodeThread(HandleRef hWnd, out int lpdwExitCode);

		// Token: 0x06003732 RID: 14130
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool ShowWindow(HandleRef hWnd, int nCmdShow);

		// Token: 0x06003733 RID: 14131
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool SetWindowPos(HandleRef hWnd, HandleRef hWndInsertAfter, int x, int y, int cx, int cy, int flags);

		// Token: 0x06003734 RID: 14132
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int GetWindowTextLength(HandleRef hWnd);

		// Token: 0x06003735 RID: 14133
		[CLSCompliant(false)]
		[DllImport("comctl32.dll", ExactSpelling = true)]
		private static extern bool _TrackMouseEvent(NativeMethods.TRACKMOUSEEVENT tme);

		// Token: 0x06003736 RID: 14134 RVA: 0x000F776F File Offset: 0x000F596F
		public static bool TrackMouseEvent(NativeMethods.TRACKMOUSEEVENT tme)
		{
			return SafeNativeMethods._TrackMouseEvent(tme);
		}

		// Token: 0x06003737 RID: 14135
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool RedrawWindow(HandleRef hwnd, ref NativeMethods.RECT rcUpdate, HandleRef hrgnUpdate, int flags);

		// Token: 0x06003738 RID: 14136
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool RedrawWindow(HandleRef hwnd, NativeMethods.COMRECT rcUpdate, HandleRef hrgnUpdate, int flags);

		// Token: 0x06003739 RID: 14137
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool InvalidateRect(HandleRef hWnd, ref NativeMethods.RECT rect, bool erase);

		// Token: 0x0600373A RID: 14138
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool InvalidateRect(HandleRef hWnd, NativeMethods.COMRECT rect, bool erase);

		// Token: 0x0600373B RID: 14139
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool InvalidateRgn(HandleRef hWnd, HandleRef hrgn, bool erase);

		// Token: 0x0600373C RID: 14140
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool UpdateWindow(HandleRef hWnd);

		// Token: 0x0600373D RID: 14141
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int GetCurrentProcessId();

		// Token: 0x0600373E RID: 14142
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int ScrollWindowEx(HandleRef hWnd, int nXAmount, int nYAmount, NativeMethods.COMRECT rectScrollRegion, ref NativeMethods.RECT rectClip, HandleRef hrgnUpdate, ref NativeMethods.RECT prcUpdate, int flags);

		// Token: 0x0600373F RID: 14143
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int GetThreadLocale();

		// Token: 0x06003740 RID: 14144
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool MessageBeep(int type);

		// Token: 0x06003741 RID: 14145
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool DrawMenuBar(HandleRef hWnd);

		// Token: 0x06003742 RID: 14146
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool IsChild(HandleRef parent, HandleRef child);

		// Token: 0x06003743 RID: 14147
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr SetTimer(HandleRef hWnd, int nIDEvent, int uElapse, IntPtr lpTimerFunc);

		// Token: 0x06003744 RID: 14148
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool KillTimer(HandleRef hwnd, int idEvent);

		// Token: 0x06003745 RID: 14149
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int MessageBox(HandleRef hWnd, string text, string caption, int type);

		// Token: 0x06003746 RID: 14150
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr SelectObject(HandleRef hDC, HandleRef hObject);

		// Token: 0x06003747 RID: 14151
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern int GetTickCount();

		// Token: 0x06003748 RID: 14152
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool ScrollWindow(HandleRef hWnd, int nXAmount, int nYAmount, ref NativeMethods.RECT rectScrollRegion, ref NativeMethods.RECT rectClip);

		// Token: 0x06003749 RID: 14153
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetCurrentProcess();

		// Token: 0x0600374A RID: 14154
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetCurrentThread();

		// Token: 0x0600374B RID: 14155
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool SetThreadLocale(int Locale);

		// Token: 0x0600374C RID: 14156
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool IsWindowUnicode(HandleRef hWnd);

		// Token: 0x0600374D RID: 14157
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool DrawEdge(HandleRef hDC, ref NativeMethods.RECT rect, int edge, int flags);

		// Token: 0x0600374E RID: 14158
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool DrawFrameControl(HandleRef hDC, ref NativeMethods.RECT rect, int type, int state);

		// Token: 0x0600374F RID: 14159
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int GetClipRgn(HandleRef hDC, HandleRef hRgn);

		// Token: 0x06003750 RID: 14160
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int GetRgnBox(HandleRef hRegion, ref NativeMethods.RECT clipRect);

		// Token: 0x06003751 RID: 14161
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int SelectClipRgn(HandleRef hDC, HandleRef hRgn);

		// Token: 0x06003752 RID: 14162
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int SetROP2(HandleRef hDC, int nDrawMode);

		// Token: 0x06003753 RID: 14163
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool DrawIcon(HandleRef hDC, int x, int y, HandleRef hIcon);

		// Token: 0x06003754 RID: 14164
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool DrawIconEx(HandleRef hDC, int x, int y, HandleRef hIcon, int width, int height, int iStepIfAniCursor, HandleRef hBrushFlickerFree, int diFlags);

		// Token: 0x06003755 RID: 14165
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int SetBkMode(HandleRef hDC, int nBkMode);

		// Token: 0x06003756 RID: 14166
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern bool BitBlt(HandleRef hDC, int x, int y, int nWidth, int nHeight, HandleRef hSrcDC, int xSrc, int ySrc, int dwRop);

		// Token: 0x06003757 RID: 14167
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool ShowCaret(HandleRef hWnd);

		// Token: 0x06003758 RID: 14168
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern bool HideCaret(HandleRef hWnd);

		// Token: 0x06003759 RID: 14169
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern uint GetCaretBlinkTime();

		// Token: 0x0600375A RID: 14170
		[DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
		public static extern bool IsAppThemed();

		// Token: 0x0600375B RID: 14171
		[DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
		public static extern int GetThemeAppProperties();

		// Token: 0x0600375C RID: 14172
		[DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
		public static extern void SetThemeAppProperties(int Flags);

		// Token: 0x0600375D RID: 14173
		[DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr OpenThemeData(HandleRef hwnd, [MarshalAs(UnmanagedType.LPWStr)] string pszClassList);

		// Token: 0x0600375E RID: 14174
		[DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
		public static extern int CloseThemeData(HandleRef hTheme);

		// Token: 0x0600375F RID: 14175
		[DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
		public static extern int GetCurrentThemeName(StringBuilder pszThemeFileName, int dwMaxNameChars, StringBuilder pszColorBuff, int dwMaxColorChars, StringBuilder pszSizeBuff, int cchMaxSizeChars);

		// Token: 0x06003760 RID: 14176
		[DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
		public static extern bool IsThemePartDefined(HandleRef hTheme, int iPartId, int iStateId);

		// Token: 0x06003761 RID: 14177
		[DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
		public static extern int DrawThemeBackground(HandleRef hTheme, HandleRef hdc, int partId, int stateId, [In] NativeMethods.COMRECT pRect, [In] NativeMethods.COMRECT pClipRect);

		// Token: 0x06003762 RID: 14178
		[DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
		public static extern int DrawThemeEdge(HandleRef hTheme, HandleRef hdc, int iPartId, int iStateId, [In] NativeMethods.COMRECT pDestRect, int uEdge, int uFlags, [Out] NativeMethods.COMRECT pContentRect);

		// Token: 0x06003763 RID: 14179
		[DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
		public static extern int DrawThemeParentBackground(HandleRef hwnd, HandleRef hdc, [In] NativeMethods.COMRECT prc);

		// Token: 0x06003764 RID: 14180
		[DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
		public static extern int DrawThemeText(HandleRef hTheme, HandleRef hdc, int iPartId, int iStateId, [MarshalAs(UnmanagedType.LPWStr)] string pszText, int iCharCount, int dwTextFlags, int dwTextFlags2, [In] NativeMethods.COMRECT pRect);

		// Token: 0x06003765 RID: 14181
		[DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
		public static extern int GetThemeBackgroundContentRect(HandleRef hTheme, HandleRef hdc, int iPartId, int iStateId, [In] NativeMethods.COMRECT pBoundingRect, [Out] NativeMethods.COMRECT pContentRect);

		// Token: 0x06003766 RID: 14182
		[DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
		public static extern int GetThemeBackgroundExtent(HandleRef hTheme, HandleRef hdc, int iPartId, int iStateId, [In] NativeMethods.COMRECT pContentRect, [Out] NativeMethods.COMRECT pExtentRect);

		// Token: 0x06003767 RID: 14183
		[DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
		public static extern int GetThemeBackgroundRegion(HandleRef hTheme, HandleRef hdc, int iPartId, int iStateId, [In] NativeMethods.COMRECT pRect, ref IntPtr pRegion);

		// Token: 0x06003768 RID: 14184
		[DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
		public static extern int GetThemeBool(HandleRef hTheme, int iPartId, int iStateId, int iPropId, ref bool pfVal);

		// Token: 0x06003769 RID: 14185
		[DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
		public static extern int GetThemeColor(HandleRef hTheme, int iPartId, int iStateId, int iPropId, ref int pColor);

		// Token: 0x0600376A RID: 14186
		[DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
		public static extern int GetThemeEnumValue(HandleRef hTheme, int iPartId, int iStateId, int iPropId, ref int piVal);

		// Token: 0x0600376B RID: 14187
		[DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
		public static extern int GetThemeFilename(HandleRef hTheme, int iPartId, int iStateId, int iPropId, StringBuilder pszThemeFilename, int cchMaxBuffChars);

		// Token: 0x0600376C RID: 14188
		[DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
		public static extern int GetThemeFont(HandleRef hTheme, HandleRef hdc, int iPartId, int iStateId, int iPropId, NativeMethods.LOGFONT pFont);

		// Token: 0x0600376D RID: 14189
		[DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
		public static extern int GetThemeInt(HandleRef hTheme, int iPartId, int iStateId, int iPropId, ref int piVal);

		// Token: 0x0600376E RID: 14190
		[DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
		public static extern int GetThemePartSize(HandleRef hTheme, HandleRef hdc, int iPartId, int iStateId, [In] NativeMethods.COMRECT prc, ThemeSizeType eSize, [Out] NativeMethods.SIZE psz);

		// Token: 0x0600376F RID: 14191
		[DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
		public static extern int GetThemePosition(HandleRef hTheme, int iPartId, int iStateId, int iPropId, [Out] NativeMethods.POINT pPoint);

		// Token: 0x06003770 RID: 14192
		[DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
		public static extern int GetThemeMargins(HandleRef hTheme, HandleRef hDC, int iPartId, int iStateId, int iPropId, ref NativeMethods.MARGINS margins);

		// Token: 0x06003771 RID: 14193
		[DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
		public static extern int GetThemeString(HandleRef hTheme, int iPartId, int iStateId, int iPropId, StringBuilder pszBuff, int cchMaxBuffChars);

		// Token: 0x06003772 RID: 14194
		[DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
		public static extern int GetThemeDocumentationProperty([MarshalAs(UnmanagedType.LPWStr)] string pszThemeName, [MarshalAs(UnmanagedType.LPWStr)] string pszPropertyName, StringBuilder pszValueBuff, int cchMaxValChars);

		// Token: 0x06003773 RID: 14195
		[DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
		public static extern int GetThemeTextExtent(HandleRef hTheme, HandleRef hdc, int iPartId, int iStateId, [MarshalAs(UnmanagedType.LPWStr)] string pszText, int iCharCount, int dwTextFlags, [In] NativeMethods.COMRECT pBoundingRect, [Out] NativeMethods.COMRECT pExtentRect);

		// Token: 0x06003774 RID: 14196
		[DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
		public static extern int GetThemeTextMetrics(HandleRef hTheme, HandleRef hdc, int iPartId, int iStateId, ref TextMetrics ptm);

		// Token: 0x06003775 RID: 14197
		[DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
		public static extern int HitTestThemeBackground(HandleRef hTheme, HandleRef hdc, int iPartId, int iStateId, int dwOptions, [In] NativeMethods.COMRECT pRect, HandleRef hrgn, [In] NativeMethods.POINTSTRUCT ptTest, ref int pwHitTestCode);

		// Token: 0x06003776 RID: 14198
		[DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
		public static extern bool IsThemeBackgroundPartiallyTransparent(HandleRef hTheme, int iPartId, int iStateId);

		// Token: 0x06003777 RID: 14199
		[DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
		public static extern bool GetThemeSysBool(HandleRef hTheme, int iBoolId);

		// Token: 0x06003778 RID: 14200
		[DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
		public static extern int GetThemeSysInt(HandleRef hTheme, int iIntId, ref int piValue);

		// Token: 0x06003779 RID: 14201
		[DllImport("user32.dll")]
		public static extern IntPtr OpenInputDesktop(int dwFlags, [MarshalAs(UnmanagedType.Bool)] bool fInherit, int dwDesiredAccess);

		// Token: 0x0600377A RID: 14202
		[DllImport("user32.dll")]
		public static extern bool CloseDesktop(IntPtr hDesktop);

		// Token: 0x0600377B RID: 14203
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool SetProcessDPIAware();

		// Token: 0x0600377C RID: 14204
		[DllImport("SHCore.dll", SetLastError = true)]
		public static extern int SetProcessDpiAwareness(NativeMethods.PROCESS_DPI_AWARENESS awareness);

		// Token: 0x0600377D RID: 14205
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool SetProcessDpiAwarenessContext(int dpiFlag);

		// Token: 0x0600377E RID: 14206
		[DllImport("user32.dll")]
		public static extern int GetThreadDpiAwarenessContext();

		// Token: 0x0600377F RID: 14207
		[DllImport("user32.dll")]
		public static extern bool AreDpiAwarenessContextsEqual(int dpiContextA, int dpiContextB);

		// Token: 0x06003780 RID: 14208 RVA: 0x000F7778 File Offset: 0x000F5978
		public static int RGBToCOLORREF(int rgbValue)
		{
			int num = (rgbValue & 255) << 16;
			rgbValue &= 16776960;
			rgbValue |= (rgbValue >> 16) & 255;
			rgbValue &= 65535;
			rgbValue |= num;
			return rgbValue;
		}

		// Token: 0x06003781 RID: 14209 RVA: 0x000F77B8 File Offset: 0x000F59B8
		public static Color ColorFromCOLORREF(int colorref)
		{
			int num = colorref & 255;
			int num2 = (colorref >> 8) & 255;
			int num3 = (colorref >> 16) & 255;
			return Color.FromArgb(num, num2, num3);
		}

		// Token: 0x06003782 RID: 14210 RVA: 0x000F77EA File Offset: 0x000F59EA
		public static int ColorToCOLORREF(Color color)
		{
			return (int)color.R | ((int)color.G << 8) | ((int)color.B << 16);
		}

		// Token: 0x020007DA RID: 2010
		// (Invoke) Token: 0x06006D93 RID: 28051
		internal delegate bool EnumThreadWindowsCallback(IntPtr hWnd, IntPtr lParam);

		// Token: 0x020007DB RID: 2011
		[Guid("BEF6E003-A874-101A-8BBA-00AA00300CAB")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[ComImport]
		public interface IFontDisp
		{
			// Token: 0x170017F6 RID: 6134
			// (get) Token: 0x06006D96 RID: 28054
			// (set) Token: 0x06006D97 RID: 28055
			string Name { get; set; }

			// Token: 0x170017F7 RID: 6135
			// (get) Token: 0x06006D98 RID: 28056
			// (set) Token: 0x06006D99 RID: 28057
			long Size { get; set; }

			// Token: 0x170017F8 RID: 6136
			// (get) Token: 0x06006D9A RID: 28058
			// (set) Token: 0x06006D9B RID: 28059
			bool Bold { get; set; }

			// Token: 0x170017F9 RID: 6137
			// (get) Token: 0x06006D9C RID: 28060
			// (set) Token: 0x06006D9D RID: 28061
			bool Italic { get; set; }

			// Token: 0x170017FA RID: 6138
			// (get) Token: 0x06006D9E RID: 28062
			// (set) Token: 0x06006D9F RID: 28063
			bool Underline { get; set; }

			// Token: 0x170017FB RID: 6139
			// (get) Token: 0x06006DA0 RID: 28064
			// (set) Token: 0x06006DA1 RID: 28065
			bool Strikethrough { get; set; }

			// Token: 0x170017FC RID: 6140
			// (get) Token: 0x06006DA2 RID: 28066
			// (set) Token: 0x06006DA3 RID: 28067
			short Weight { get; set; }

			// Token: 0x170017FD RID: 6141
			// (get) Token: 0x06006DA4 RID: 28068
			// (set) Token: 0x06006DA5 RID: 28069
			short Charset { get; set; }
		}
	}
}
