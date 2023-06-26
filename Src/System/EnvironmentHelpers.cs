using System;
using System.ComponentModel;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using Microsoft.Win32;

namespace System
{
	// Token: 0x0200005F RID: 95
	internal static class EnvironmentHelpers
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0001D93C File Offset: 0x0001BB3C
		public static bool IsAppContainerProcess
		{
			get
			{
				if (!EnvironmentHelpers.s_IsAppContainerProcessInitalized)
				{
					if (Environment.OSVersion.Platform != PlatformID.Win32NT)
					{
						EnvironmentHelpers.s_IsAppContainerProcess = false;
					}
					else if (Environment.OSVersion.Version.Major < 6 || (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor <= 1))
					{
						EnvironmentHelpers.s_IsAppContainerProcess = false;
					}
					else
					{
						EnvironmentHelpers.s_IsAppContainerProcess = EnvironmentHelpers.HasAppContainerToken();
					}
					EnvironmentHelpers.s_IsAppContainerProcessInitalized = true;
				}
				return EnvironmentHelpers.s_IsAppContainerProcess;
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0001D9C4 File Offset: 0x0001BBC4
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode | SecurityPermissionFlag.ControlPrincipal)]
		private unsafe static bool HasAppContainerToken()
		{
			int* ptr = stackalloc int[(UIntPtr)4];
			uint num = 0U;
			using (WindowsIdentity current = WindowsIdentity.GetCurrent(TokenAccessLevels.Query))
			{
				if (!Microsoft.Win32.UnsafeNativeMethods.GetTokenInformation(current.Token, 29U, new IntPtr((void*)ptr), 4U, out num))
				{
					throw new Win32Exception();
				}
			}
			return *ptr != 0;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0001DA20 File Offset: 0x0001BC20
		internal static bool IsWindowsVistaOrAbove()
		{
			OperatingSystem osversion = Environment.OSVersion;
			return osversion.Platform == PlatformID.Win32NT && osversion.Version.Major >= 6;
		}

		// Token: 0x04000519 RID: 1305
		private static volatile bool s_IsAppContainerProcess;

		// Token: 0x0400051A RID: 1306
		private static volatile bool s_IsAppContainerProcessInitalized;
	}
}
