using System;
using System.Internal;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Windows.Forms.Internal
{
	// Token: 0x020004DF RID: 1247
	[SuppressUnmanagedCodeSecurity]
	internal static class IntSafeNativeMethods
	{
		// Token: 0x06005140 RID: 20800
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateSolidBrush", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr IntCreateSolidBrush(int crColor);

		// Token: 0x06005141 RID: 20801 RVA: 0x00153010 File Offset: 0x00151210
		public static IntPtr CreateSolidBrush(int crColor)
		{
			return System.Internal.HandleCollector.Add(IntSafeNativeMethods.IntCreateSolidBrush(crColor), IntSafeNativeMethods.CommonHandles.GDI);
		}

		// Token: 0x06005142 RID: 20802
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreatePen", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr IntCreatePen(int fnStyle, int nWidth, int crColor);

		// Token: 0x06005143 RID: 20803 RVA: 0x00153030 File Offset: 0x00151230
		public static IntPtr CreatePen(int fnStyle, int nWidth, int crColor)
		{
			return System.Internal.HandleCollector.Add(IntSafeNativeMethods.IntCreatePen(fnStyle, nWidth, crColor), IntSafeNativeMethods.CommonHandles.GDI);
		}

		// Token: 0x06005144 RID: 20804
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "ExtCreatePen", ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr IntExtCreatePen(int fnStyle, int dwWidth, IntNativeMethods.LOGBRUSH lplb, int dwStyleCount, [MarshalAs(UnmanagedType.LPArray)] int[] lpStyle);

		// Token: 0x06005145 RID: 20805 RVA: 0x00153054 File Offset: 0x00151254
		public static IntPtr ExtCreatePen(int fnStyle, int dwWidth, IntNativeMethods.LOGBRUSH lplb, int dwStyleCount, int[] lpStyle)
		{
			return System.Internal.HandleCollector.Add(IntSafeNativeMethods.IntExtCreatePen(fnStyle, dwWidth, lplb, dwStyleCount, lpStyle), IntSafeNativeMethods.CommonHandles.GDI);
		}

		// Token: 0x06005146 RID: 20806
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, EntryPoint = "CreateRectRgn", ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr IntCreateRectRgn(int x1, int y1, int x2, int y2);

		// Token: 0x06005147 RID: 20807 RVA: 0x00153078 File Offset: 0x00151278
		public static IntPtr CreateRectRgn(int x1, int y1, int x2, int y2)
		{
			return System.Internal.HandleCollector.Add(IntSafeNativeMethods.IntCreateRectRgn(x1, y1, x2, y2), IntSafeNativeMethods.CommonHandles.GDI);
		}

		// Token: 0x06005148 RID: 20808
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetUserDefaultLCID();

		// Token: 0x06005149 RID: 20809
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern bool GdiFlush();

		// Token: 0x0200087D RID: 2173
		public sealed class CommonHandles
		{
			// Token: 0x04004473 RID: 17523
			public static readonly int EMF = System.Internal.HandleCollector.RegisterType("EnhancedMetaFile", 20, 500);

			// Token: 0x04004474 RID: 17524
			public static readonly int GDI = System.Internal.HandleCollector.RegisterType("GDI", 90, 50);

			// Token: 0x04004475 RID: 17525
			public static readonly int HDC = System.Internal.HandleCollector.RegisterType("HDC", 100, 2);
		}
	}
}
