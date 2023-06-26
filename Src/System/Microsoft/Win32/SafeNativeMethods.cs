using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Win32
{
	// Token: 0x02000015 RID: 21
	[SuppressUnmanagedCodeSecurity]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	internal static class SafeNativeMethods
	{
		// Token: 0x0600019F RID: 415
		[DllImport("gdi32.dll", CharSet = CharSet.Auto)]
		public static extern bool GetTextMetrics(IntPtr hDC, [In] [Out] NativeMethods.TEXTMETRIC tm);

		// Token: 0x060001A0 RID: 416
		[DllImport("gdi32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr GetStockObject(int nIndex);

		// Token: 0x060001A1 RID: 417
		[DllImport("kernel32.dll", BestFitMapping = true, CharSet = CharSet.Auto)]
		public static extern void OutputDebugString(string message);

		// Token: 0x060001A2 RID: 418
		[DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "MessageBoxW", ExactSpelling = true)]
		private static extern int MessageBoxSystem(IntPtr hWnd, string text, string caption, int type);

		// Token: 0x060001A3 RID: 419 RVA: 0x0000D458 File Offset: 0x0000B658
		[SecurityCritical]
		public static int MessageBox(IntPtr hWnd, string text, string caption, int type)
		{
			int num;
			try
			{
				num = SafeNativeMethods.MessageBoxSystem(hWnd, text, caption, type);
			}
			catch (DllNotFoundException)
			{
				num = 0;
			}
			catch (EntryPointNotFoundException)
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x060001A4 RID: 420
		[DllImport("kernel32.dll", BestFitMapping = true, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int FormatMessage(int dwFlags, IntPtr lpSource_mustBeNull, uint dwMessageId, int dwLanguageId, StringBuilder lpBuffer, int nSize, IntPtr[] arguments);

		// Token: 0x060001A5 RID: 421
		[DllImport("kernel32.dll", BestFitMapping = true, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int FormatMessage(int dwFlags, SafeLibraryHandle lpSource, uint dwMessageId, int dwLanguageId, StringBuilder lpBuffer, int nSize, IntPtr[] arguments);

		// Token: 0x060001A6 RID: 422
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern bool CloseHandle(IntPtr handle);

		// Token: 0x060001A7 RID: 423
		[DllImport("kernel32.dll")]
		public static extern bool QueryPerformanceCounter(out long value);

		// Token: 0x060001A8 RID: 424
		[DllImport("kernel32.dll")]
		public static extern bool QueryPerformanceFrequency(out long value);

		// Token: 0x060001A9 RID: 425
		[DllImport("user32.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
		public static extern int RegisterWindowMessage(string msg);

		// Token: 0x060001AA RID: 426
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern IntPtr LoadLibrary(string libFilename);

		// Token: 0x060001AB RID: 427
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern bool FreeLibrary(HandleRef hModule);

		// Token: 0x060001AC RID: 428
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
		public static extern bool GetComputerName(StringBuilder lpBuffer, int[] nSize);

		// Token: 0x060001AD RID: 429 RVA: 0x0000D498 File Offset: 0x0000B698
		public unsafe static int InterlockedCompareExchange(IntPtr pDestination, int exchange, int compare)
		{
			return Interlocked.CompareExchange(ref *(int*)pDestination.ToPointer(), exchange, compare);
		}

		// Token: 0x060001AE RID: 430
		[DllImport("perfcounter.dll", CharSet = CharSet.Auto)]
		public static extern int FormatFromRawValue(uint dwCounterType, uint dwFormat, ref long pTimeBase, NativeMethods.PDH_RAW_COUNTER pRawValue1, NativeMethods.PDH_RAW_COUNTER pRawValue2, NativeMethods.PDH_FMT_COUNTERVALUE pFmtValue);

		// Token: 0x060001AF RID: 431
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool IsWow64Process(Microsoft.Win32.SafeHandles.SafeProcessHandle hProcess, ref bool Wow64Process);

		// Token: 0x060001B0 RID: 432
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern SafeWaitHandle CreateSemaphore(NativeMethods.SECURITY_ATTRIBUTES lpSecurityAttributes, int initialCount, int maximumCount, string name);

		// Token: 0x060001B1 RID: 433
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern SafeWaitHandle OpenSemaphore(int desiredAccess, bool inheritHandle, string name);

		// Token: 0x060001B2 RID: 434
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool ReleaseSemaphore(SafeWaitHandle handle, int releaseCount, out int previousCount);

		// Token: 0x040002F6 RID: 758
		public const int MB_RIGHT = 524288;

		// Token: 0x040002F7 RID: 759
		public const int MB_RTLREADING = 1048576;

		// Token: 0x040002F8 RID: 760
		public const int FORMAT_MESSAGE_ALLOCATE_BUFFER = 256;

		// Token: 0x040002F9 RID: 761
		public const int FORMAT_MESSAGE_IGNORE_INSERTS = 512;

		// Token: 0x040002FA RID: 762
		public const int FORMAT_MESSAGE_FROM_STRING = 1024;

		// Token: 0x040002FB RID: 763
		public const int FORMAT_MESSAGE_FROM_SYSTEM = 4096;

		// Token: 0x040002FC RID: 764
		public const int FORMAT_MESSAGE_ARGUMENT_ARRAY = 8192;

		// Token: 0x040002FD RID: 765
		public const int ERROR_INSUFFICIENT_BUFFER = 122;

		// Token: 0x040002FE RID: 766
		public const int FORMAT_MESSAGE_MAX_WIDTH_MASK = 255;

		// Token: 0x040002FF RID: 767
		public const int FORMAT_MESSAGE_FROM_HMODULE = 2048;

		// Token: 0x020006D0 RID: 1744
		[StructLayout(LayoutKind.Sequential)]
		internal class PROCESS_INFORMATION
		{
			// Token: 0x04002FA5 RID: 12197
			public IntPtr hProcess = IntPtr.Zero;

			// Token: 0x04002FA6 RID: 12198
			public IntPtr hThread = IntPtr.Zero;

			// Token: 0x04002FA7 RID: 12199
			public int dwProcessId;

			// Token: 0x04002FA8 RID: 12200
			public int dwThreadId;
		}
	}
}
