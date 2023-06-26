using System;

namespace System.Windows.Forms
{
	// Token: 0x02000313 RID: 787
	internal static class OsVersion
	{
		// Token: 0x0600321F RID: 12831 RVA: 0x000E183C File Offset: 0x000DFA3C
		private static NativeMethods.NtDll.RTL_OSVERSIONINFOEX InitVersion()
		{
			IntSecurity.UnmanagedCode.Assert();
			NativeMethods.NtDll.RTL_OSVERSIONINFOEX rtl_OSVERSIONINFOEX;
			NativeMethods.NtDll.RtlGetVersion(out rtl_OSVERSIONINFOEX);
			return rtl_OSVERSIONINFOEX;
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x06003220 RID: 12832 RVA: 0x000E185C File Offset: 0x000DFA5C
		public static bool IsWindows11_OrGreater
		{
			get
			{
				return OsVersion.s_versionInfo.dwMajorVersion >= 10U && OsVersion.s_versionInfo.dwBuildNumber >= 22000U;
			}
		}

		// Token: 0x04001E5D RID: 7773
		private static NativeMethods.NtDll.RTL_OSVERSIONINFOEX s_versionInfo = OsVersion.InitVersion();
	}
}
