using System;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using Microsoft.Win32;

namespace System.Net
{
	// Token: 0x02000119 RID: 281
	internal static class ComNetOS
	{
		// Token: 0x06000B1A RID: 2842 RVA: 0x0003D45C File Offset: 0x0003B65C
		[EnvironmentPermission(SecurityAction.Assert, Unrestricted = true)]
		static ComNetOS()
		{
			OperatingSystem osversion = Environment.OSVersion;
			try
			{
				ComNetOS.IsAspNetServer = Thread.GetDomain().GetData(".appDomain") != null;
			}
			catch
			{
			}
			ComNetOS.IsWin7orLater = osversion.Version >= new Version(6, 1);
			ComNetOS.IsWin7Sp1orLater = osversion.Version >= new Version(6, 1, 7601);
			ComNetOS.IsWin8orLater = osversion.Version >= new Version(6, 2);
			ComNetOS.InstallationType = ComNetOS.GetWindowsInstallType();
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, SR.GetString("net_osinstalltype", new object[] { ComNetOS.InstallationType }));
			}
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0003D520 File Offset: 0x0003B720
		[RegistryPermission(SecurityAction.Assert, Read = "HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows NT\\CurrentVersion")]
		private static WindowsInstallationType GetWindowsInstallType()
		{
			WindowsInstallationType windowsInstallationType;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion"))
				{
					string text = registryKey.GetValue("InstallationType") as string;
					if (string.IsNullOrEmpty(text))
					{
						if (Logging.On)
						{
							Logging.PrintWarning(Logging.Web, SR.GetString("net_empty_osinstalltype", new object[] { "Software\\Microsoft\\Windows NT\\CurrentVersion\\InstallationType" }));
						}
						windowsInstallationType = WindowsInstallationType.Unknown;
					}
					else if (string.Compare(text, "Client", StringComparison.OrdinalIgnoreCase) == 0)
					{
						windowsInstallationType = WindowsInstallationType.Client;
					}
					else if (string.Compare(text, "Server", StringComparison.OrdinalIgnoreCase) == 0)
					{
						windowsInstallationType = WindowsInstallationType.Server;
					}
					else if (string.Compare(text, "Server Core", StringComparison.OrdinalIgnoreCase) == 0)
					{
						windowsInstallationType = WindowsInstallationType.ServerCore;
					}
					else if (string.Compare(text, "Embedded", StringComparison.OrdinalIgnoreCase) == 0)
					{
						windowsInstallationType = WindowsInstallationType.Embedded;
					}
					else
					{
						if (Logging.On)
						{
							Logging.PrintError(Logging.Web, SR.GetString("net_unknown_osinstalltype", new object[] { text }));
						}
						windowsInstallationType = WindowsInstallationType.Unknown;
					}
				}
			}
			catch (UnauthorizedAccessException ex)
			{
				if (Logging.On)
				{
					Logging.PrintWarning(Logging.Web, SR.GetString("net_cant_determine_osinstalltype", new object[] { "Software\\Microsoft\\Windows NT\\CurrentVersion", ex.Message }));
				}
				windowsInstallationType = WindowsInstallationType.Unknown;
			}
			catch (SecurityException ex2)
			{
				if (Logging.On)
				{
					Logging.PrintWarning(Logging.Web, SR.GetString("net_cant_determine_osinstalltype", new object[] { "Software\\Microsoft\\Windows NT\\CurrentVersion", ex2.Message }));
				}
				windowsInstallationType = WindowsInstallationType.Unknown;
			}
			return windowsInstallationType;
		}

		// Token: 0x04000F64 RID: 3940
		private const string OSInstallTypeRegKey = "Software\\Microsoft\\Windows NT\\CurrentVersion";

		// Token: 0x04000F65 RID: 3941
		private const string OSInstallTypeRegKeyPath = "HKEY_LOCAL_MACHINE\\Software\\Microsoft\\Windows NT\\CurrentVersion";

		// Token: 0x04000F66 RID: 3942
		private const string OSInstallTypeRegName = "InstallationType";

		// Token: 0x04000F67 RID: 3943
		private const string InstallTypeStringClient = "Client";

		// Token: 0x04000F68 RID: 3944
		private const string InstallTypeStringServer = "Server";

		// Token: 0x04000F69 RID: 3945
		private const string InstallTypeStringServerCore = "Server Core";

		// Token: 0x04000F6A RID: 3946
		private const string InstallTypeStringEmbedded = "Embedded";

		// Token: 0x04000F6B RID: 3947
		internal static readonly bool IsAspNetServer;

		// Token: 0x04000F6C RID: 3948
		internal static readonly bool IsWin7orLater;

		// Token: 0x04000F6D RID: 3949
		internal static readonly bool IsWin7Sp1orLater;

		// Token: 0x04000F6E RID: 3950
		internal static readonly bool IsWin8orLater;

		// Token: 0x04000F6F RID: 3951
		internal static readonly WindowsInstallationType InstallationType;
	}
}
