using System;
using System.Reflection;
using System.Security;
using Microsoft.Win32;

namespace System.Runtime.InteropServices
{
	/// <summary>Provides information about the .NET runtime installation.</summary>
	// Token: 0x020009A9 RID: 2473
	public static class RuntimeInformation
	{
		/// <summary>Returns a string that indicates the name of the .NET installation on which an app is running.</summary>
		/// <returns>The name of the .NET installation on which the app is running.</returns>
		// Token: 0x1700111B RID: 4379
		// (get) Token: 0x0600631F RID: 25375 RVA: 0x00152FE8 File Offset: 0x001511E8
		public static string FrameworkDescription
		{
			get
			{
				if (RuntimeInformation.s_frameworkDescription == null)
				{
					AssemblyFileVersionAttribute assemblyFileVersionAttribute = (AssemblyFileVersionAttribute)typeof(object).GetTypeInfo().Assembly.GetCustomAttribute(typeof(AssemblyFileVersionAttribute));
					RuntimeInformation.s_frameworkDescription = ".NET Framework " + assemblyFileVersionAttribute.Version;
				}
				return RuntimeInformation.s_frameworkDescription;
			}
		}

		/// <summary>Indicates whether the current application is running on the specified platform.</summary>
		/// <param name="osPlatform">A platform.</param>
		/// <returns>
		///   <see langword="true" /> if the current app is running on the specified platform; otherwise, <see langword="false" />.</returns>
		// Token: 0x06006320 RID: 25376 RVA: 0x0015303F File Offset: 0x0015123F
		public static bool IsOSPlatform(OSPlatform osPlatform)
		{
			return OSPlatform.Windows == osPlatform;
		}

		/// <summary>Gets a string that describes the operating system on which the app is running.</summary>
		/// <returns>The description of the operating system on which the app is running.</returns>
		// Token: 0x1700111C RID: 4380
		// (get) Token: 0x06006321 RID: 25377 RVA: 0x0015304C File Offset: 0x0015124C
		public static string OSDescription
		{
			[SecuritySafeCritical]
			get
			{
				if (RuntimeInformation.s_osDescription == null)
				{
					RuntimeInformation.s_osDescription = RuntimeInformation.RtlGetVersion();
				}
				return RuntimeInformation.s_osDescription;
			}
		}

		/// <summary>Gets the platform architecture on which the current app is running.</summary>
		/// <returns>The platform architecture on which the current app is running.</returns>
		// Token: 0x1700111D RID: 4381
		// (get) Token: 0x06006322 RID: 25378 RVA: 0x00153064 File Offset: 0x00151264
		public static Architecture OSArchitecture
		{
			[SecuritySafeCritical]
			get
			{
				object obj = RuntimeInformation.s_osLock;
				lock (obj)
				{
					if (RuntimeInformation.s_osArch == null)
					{
						Win32Native.SYSTEM_INFO system_INFO;
						Win32Native.GetNativeSystemInfo(out system_INFO);
						RuntimeInformation.s_osArch = new Architecture?(RuntimeInformation.GetArchitecture(system_INFO.wProcessorArchitecture));
					}
				}
				return RuntimeInformation.s_osArch.Value;
			}
		}

		/// <summary>Gets the process architecture of the currently running app.</summary>
		/// <returns>The process architecture of the currently running app.</returns>
		// Token: 0x1700111E RID: 4382
		// (get) Token: 0x06006323 RID: 25379 RVA: 0x001530D0 File Offset: 0x001512D0
		public static Architecture ProcessArchitecture
		{
			[SecuritySafeCritical]
			get
			{
				object obj = RuntimeInformation.s_processLock;
				lock (obj)
				{
					if (RuntimeInformation.s_processArch == null)
					{
						Win32Native.SYSTEM_INFO system_INFO = default(Win32Native.SYSTEM_INFO);
						Win32Native.GetSystemInfo(ref system_INFO);
						RuntimeInformation.s_processArch = new Architecture?(RuntimeInformation.GetArchitecture(system_INFO.wProcessorArchitecture));
					}
				}
				return RuntimeInformation.s_processArch.Value;
			}
		}

		// Token: 0x06006324 RID: 25380 RVA: 0x00153144 File Offset: 0x00151344
		private static Architecture GetArchitecture(ushort wProcessorArchitecture)
		{
			Architecture architecture = Architecture.X86;
			if (wProcessorArchitecture <= 5)
			{
				if (wProcessorArchitecture != 0)
				{
					if (wProcessorArchitecture == 5)
					{
						architecture = Architecture.Arm;
					}
				}
				else
				{
					architecture = Architecture.X86;
				}
			}
			else if (wProcessorArchitecture != 9)
			{
				if (wProcessorArchitecture == 12)
				{
					architecture = Architecture.Arm64;
				}
			}
			else
			{
				architecture = Architecture.X64;
			}
			return architecture;
		}

		// Token: 0x06006325 RID: 25381 RVA: 0x0015317C File Offset: 0x0015137C
		[SecuritySafeCritical]
		private static string RtlGetVersion()
		{
			Win32Native.RTL_OSVERSIONINFOEX rtl_OSVERSIONINFOEX = default(Win32Native.RTL_OSVERSIONINFOEX);
			rtl_OSVERSIONINFOEX.dwOSVersionInfoSize = (uint)Marshal.SizeOf<Win32Native.RTL_OSVERSIONINFOEX>(rtl_OSVERSIONINFOEX);
			if (Win32Native.RtlGetVersion(out rtl_OSVERSIONINFOEX) == 0)
			{
				return string.Format("{0} {1}.{2}.{3} {4}", new object[] { "Microsoft Windows", rtl_OSVERSIONINFOEX.dwMajorVersion, rtl_OSVERSIONINFOEX.dwMinorVersion, rtl_OSVERSIONINFOEX.dwBuildNumber, rtl_OSVERSIONINFOEX.szCSDVersion });
			}
			return "Microsoft Windows";
		}

		// Token: 0x04002CB6 RID: 11446
		private const string FrameworkName = ".NET Framework";

		// Token: 0x04002CB7 RID: 11447
		private static string s_frameworkDescription;

		// Token: 0x04002CB8 RID: 11448
		private static string s_osDescription = null;

		// Token: 0x04002CB9 RID: 11449
		private static object s_osLock = new object();

		// Token: 0x04002CBA RID: 11450
		private static object s_processLock = new object();

		// Token: 0x04002CBB RID: 11451
		private static Architecture? s_osArch = null;

		// Token: 0x04002CBC RID: 11452
		private static Architecture? s_processArch = null;
	}
}
