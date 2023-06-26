using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.CodeDom.Compiler
{
	// Token: 0x0200067A RID: 1658
	internal static class FileIntegrity
	{
		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x06003D1B RID: 15643 RVA: 0x000FB3A5 File Offset: 0x000F95A5
		public static bool IsEnabled
		{
			get
			{
				return FileIntegrity.s_lazyIsEnabled.Value;
			}
		}

		// Token: 0x06003D1C RID: 15644 RVA: 0x000FB3B4 File Offset: 0x000F95B4
		public static void MarkAsTrusted(SafeFileHandle safeFileHandle)
		{
			int num = Microsoft.Win32.UnsafeNativeMethods.WldpSetDynamicCodeTrust(safeFileHandle);
			Marshal.ThrowExceptionForHR(num, new IntPtr(-1));
		}

		// Token: 0x06003D1D RID: 15645 RVA: 0x000FB3D4 File Offset: 0x000F95D4
		public static bool IsTrusted(SafeFileHandle safeFileHandle)
		{
			int num = Microsoft.Win32.UnsafeNativeMethods.WldpQueryDynamicCodeTrust(safeFileHandle, IntPtr.Zero, 0U);
			if (num == -805305819)
			{
				return false;
			}
			Marshal.ThrowExceptionForHR(num, new IntPtr(-1));
			return true;
		}

		// Token: 0x04002C81 RID: 11393
		private static readonly Lazy<bool> s_lazyIsEnabled = new Lazy<bool>(delegate
		{
			Version version = Environment.OSVersion.Version;
			if (version.Major < 6 || (version.Major == 6 && version.Minor < 2))
			{
				return false;
			}
			bool flag;
			using (Microsoft.Win32.SafeHandles.SafeLibraryHandle safeLibraryHandle = Microsoft.Win32.SafeHandles.SafeLibraryHandle.LoadLibraryEx("wldp.dll", IntPtr.Zero, 2048))
			{
				if (safeLibraryHandle.IsInvalid)
				{
					flag = false;
				}
				else
				{
					IntPtr moduleHandle = Microsoft.Win32.UnsafeNativeMethods.GetModuleHandle("wldp.dll");
					if (!(moduleHandle != IntPtr.Zero) || !(IntPtr.Zero != Microsoft.Win32.UnsafeNativeMethods.GetProcAddress(moduleHandle, "WldpIsDynamicCodePolicyEnabled")) || !(IntPtr.Zero != Microsoft.Win32.UnsafeNativeMethods.GetProcAddress(moduleHandle, "WldpSetDynamicCodeTrust")) || !(IntPtr.Zero != Microsoft.Win32.UnsafeNativeMethods.GetProcAddress(moduleHandle, "WldpQueryDynamicCodeTrust")))
					{
						flag = false;
					}
					else
					{
						int num = 0;
						int num2 = Microsoft.Win32.UnsafeNativeMethods.WldpIsDynamicCodePolicyEnabled(out num);
						Marshal.ThrowExceptionForHR(num2, new IntPtr(-1));
						flag = num != 0;
					}
				}
			}
			return flag;
		});
	}
}
