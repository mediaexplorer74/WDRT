using System;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009F2 RID: 2546
	internal static class UnsafeNativeMethods
	{
		// Token: 0x060064D9 RID: 25817
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		[DllImport("api-ms-win-core-winrt-error-l1-1-1.dll", PreserveSig = false)]
		internal static extern IRestrictedErrorInfo GetRestrictedErrorInfo();

		// Token: 0x060064DA RID: 25818
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		[DllImport("api-ms-win-core-winrt-error-l1-1-1.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool RoOriginateLanguageException(int error, [MarshalAs(UnmanagedType.HString)] string message, IntPtr languageException);

		// Token: 0x060064DB RID: 25819
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		[DllImport("api-ms-win-core-winrt-error-l1-1-1.dll", PreserveSig = false)]
		internal static extern void RoReportUnhandledError(IRestrictedErrorInfo error);

		// Token: 0x060064DC RID: 25820
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		[DllImport("api-ms-win-core-winrt-string-l1-1-0.dll", CallingConvention = CallingConvention.StdCall)]
		internal unsafe static extern int WindowsCreateString([MarshalAs(UnmanagedType.LPWStr)] string sourceString, int length, [Out] IntPtr* hstring);

		// Token: 0x060064DD RID: 25821
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		[DllImport("api-ms-win-core-winrt-string-l1-1-0.dll", CallingConvention = CallingConvention.StdCall)]
		internal unsafe static extern int WindowsCreateStringReference(char* sourceString, int length, [Out] HSTRING_HEADER* hstringHeader, [Out] IntPtr* hstring);

		// Token: 0x060064DE RID: 25822
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		[DllImport("api-ms-win-core-winrt-string-l1-1-0.dll", CallingConvention = CallingConvention.StdCall)]
		internal static extern int WindowsDeleteString(IntPtr hstring);

		// Token: 0x060064DF RID: 25823
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		[DllImport("api-ms-win-core-winrt-string-l1-1-0.dll", CallingConvention = CallingConvention.StdCall)]
		internal unsafe static extern char* WindowsGetStringRawBuffer(IntPtr hstring, [Out] uint* length);
	}
}
