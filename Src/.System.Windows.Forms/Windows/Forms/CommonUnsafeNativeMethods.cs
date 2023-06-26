using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Windows.Forms
{
	// Token: 0x02000113 RID: 275
	[SuppressUnmanagedCodeSecurity]
	internal class CommonUnsafeNativeMethods
	{
		// Token: 0x06000766 RID: 1894
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr GetProcAddress(HandleRef hModule, string lpProcName);

		// Token: 0x06000767 RID: 1895
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr GetModuleHandle(string modName);

		// Token: 0x06000768 RID: 1896
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr LoadLibraryEx(string lpModuleName, IntPtr hFile, uint dwFlags);

		// Token: 0x06000769 RID: 1897
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr LoadLibrary(string libname);

		// Token: 0x0600076A RID: 1898
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool FreeLibrary(HandleRef hModule);

		// Token: 0x0600076B RID: 1899 RVA: 0x00015688 File Offset: 0x00013888
		public static IntPtr LoadLibraryFromSystemPathIfAvailable(string libraryName)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr moduleHandle = CommonUnsafeNativeMethods.GetModuleHandle("kernel32.dll");
			if (moduleHandle != IntPtr.Zero)
			{
				if (CommonUnsafeNativeMethods.GetProcAddress(new HandleRef(null, moduleHandle), "AddDllDirectory") != IntPtr.Zero)
				{
					intPtr = CommonUnsafeNativeMethods.LoadLibraryEx(libraryName, IntPtr.Zero, 2048U);
				}
				else
				{
					intPtr = CommonUnsafeNativeMethods.LoadLibrary(libraryName);
				}
			}
			return intPtr;
		}

		// Token: 0x0600076C RID: 1900
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern DpiAwarenessContext GetThreadDpiAwarenessContext();

		// Token: 0x0600076D RID: 1901
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern IntPtr GetWindowDpiAwarenessContext(IntPtr hWnd);

		// Token: 0x0600076E RID: 1902
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern CommonUnsafeNativeMethods.DPI_AWARENESS GetAwarenessFromDpiAwarenessContext(IntPtr dpiAwarenessContext);

		// Token: 0x0600076F RID: 1903
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		internal static extern DpiAwarenessContext SetThreadDpiAwarenessContext(DpiAwarenessContext dpiContext);

		// Token: 0x06000770 RID: 1904
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool AreDpiAwarenessContextsEqual(DpiAwarenessContext dpiContextA, DpiAwarenessContext dpiContextB);

		// Token: 0x06000771 RID: 1905 RVA: 0x000156EB File Offset: 0x000138EB
		public static bool TryFindDpiAwarenessContextsEqual(DpiAwarenessContext dpiContextA, DpiAwarenessContext dpiContextB)
		{
			return (dpiContextA == DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNSPECIFIED && dpiContextB == DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNSPECIFIED) || (ApiHelper.IsApiAvailable("user32.dll", "AreDpiAwarenessContextsEqual") && CommonUnsafeNativeMethods.AreDpiAwarenessContextsEqual(dpiContextA, dpiContextB));
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0001570F File Offset: 0x0001390F
		public static DpiAwarenessContext TryGetThreadDpiAwarenessContext()
		{
			if (ApiHelper.IsApiAvailable("user32.dll", "GetThreadDpiAwarenessContext"))
			{
				return CommonUnsafeNativeMethods.GetThreadDpiAwarenessContext();
			}
			return DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNSPECIFIED;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00015729 File Offset: 0x00013929
		public static DpiAwarenessContext TrySetThreadDpiAwarenessContext(DpiAwarenessContext dpiCOntext)
		{
			if (ApiHelper.IsApiAvailable("user32.dll", "SetThreadDpiAwarenessContext"))
			{
				return CommonUnsafeNativeMethods.SetThreadDpiAwarenessContext(dpiCOntext);
			}
			return DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNSPECIFIED;
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00015744 File Offset: 0x00013944
		internal static DpiAwarenessContext TryGetDpiAwarenessContextForWindow(IntPtr hWnd)
		{
			DpiAwarenessContext dpiAwarenessContext = DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNSPECIFIED;
			try
			{
				if (ApiHelper.IsApiAvailable("user32.dll", "GetWindowDpiAwarenessContext") && ApiHelper.IsApiAvailable("user32.dll", "GetAwarenessFromDpiAwarenessContext"))
				{
					IntPtr windowDpiAwarenessContext = CommonUnsafeNativeMethods.GetWindowDpiAwarenessContext(hWnd);
					CommonUnsafeNativeMethods.DPI_AWARENESS awarenessFromDpiAwarenessContext = CommonUnsafeNativeMethods.GetAwarenessFromDpiAwarenessContext(windowDpiAwarenessContext);
					dpiAwarenessContext = CommonUnsafeNativeMethods.ConvertToDpiAwarenessContext(awarenessFromDpiAwarenessContext);
				}
			}
			catch
			{
			}
			return dpiAwarenessContext;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x000157A0 File Offset: 0x000139A0
		private static DpiAwarenessContext ConvertToDpiAwarenessContext(CommonUnsafeNativeMethods.DPI_AWARENESS dpiAwareness)
		{
			switch (dpiAwareness)
			{
			case CommonUnsafeNativeMethods.DPI_AWARENESS.DPI_AWARENESS_UNAWARE:
				return DpiAwarenessContext.DPI_AWARENESS_CONTEXT_UNAWARE;
			case CommonUnsafeNativeMethods.DPI_AWARENESS.DPI_AWARENESS_SYSTEM_AWARE:
				return DpiAwarenessContext.DPI_AWARENESS_CONTEXT_SYSTEM_AWARE;
			case CommonUnsafeNativeMethods.DPI_AWARENESS.DPI_AWARENESS_PER_MONITOR_AWARE:
				return DpiAwarenessContext.DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2;
			default:
				return DpiAwarenessContext.DPI_AWARENESS_CONTEXT_SYSTEM_AWARE;
			}
		}

		// Token: 0x040004F9 RID: 1273
		internal const int LOAD_LIBRARY_SEARCH_SYSTEM32 = 2048;

		// Token: 0x020005FC RID: 1532
		internal enum DPI_AWARENESS
		{
			// Token: 0x04003895 RID: 14485
			DPI_AWARENESS_INVALID = -1,
			// Token: 0x04003896 RID: 14486
			DPI_AWARENESS_UNAWARE,
			// Token: 0x04003897 RID: 14487
			DPI_AWARENESS_SYSTEM_AWARE,
			// Token: 0x04003898 RID: 14488
			DPI_AWARENESS_PER_MONITOR_AWARE
		}
	}
}
