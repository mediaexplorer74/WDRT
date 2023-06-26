using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32;

namespace System.Net
{
	// Token: 0x02000152 RID: 338
	internal static class RegistryConfiguration
	{
		// Token: 0x06000BD9 RID: 3033 RVA: 0x000401C4 File Offset: 0x0003E3C4
		[RegistryPermission(SecurityAction.Assert, Read = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\.NETFramework")]
		public static int GlobalConfigReadInt(string configVariable, int defaultValue)
		{
			object obj = RegistryConfiguration.ReadConfig(RegistryConfiguration.GetNetFrameworkVersionedPath(), configVariable, RegistryValueKind.DWord);
			if (obj != null)
			{
				return (int)obj;
			}
			return defaultValue;
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x000401EC File Offset: 0x0003E3EC
		[RegistryPermission(SecurityAction.Assert, Read = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\.NETFramework")]
		public static string GlobalConfigReadString(string configVariable, string defaultValue)
		{
			object obj = RegistryConfiguration.ReadConfig(RegistryConfiguration.GetNetFrameworkVersionedPath(), configVariable, RegistryValueKind.String);
			if (obj != null)
			{
				return (string)obj;
			}
			return defaultValue;
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x00040214 File Offset: 0x0003E414
		[RegistryPermission(SecurityAction.Assert, Read = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\.NETFramework")]
		public static int AppConfigReadInt(string configVariable, int defaultValue)
		{
			object obj = RegistryConfiguration.ReadConfig(RegistryConfiguration.GetAppConfigPath(configVariable), RegistryConfiguration.GetAppConfigValueName(), RegistryValueKind.DWord);
			if (obj != null)
			{
				return (int)obj;
			}
			return defaultValue;
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x00040240 File Offset: 0x0003E440
		[RegistryPermission(SecurityAction.Assert, Read = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\.NETFramework")]
		public static string AppConfigReadString(string configVariable, string defaultValue)
		{
			object obj = RegistryConfiguration.ReadConfig(RegistryConfiguration.GetAppConfigPath(configVariable), RegistryConfiguration.GetAppConfigValueName(), RegistryValueKind.String);
			if (obj != null)
			{
				return (string)obj;
			}
			return defaultValue;
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x0004026C File Offset: 0x0003E46C
		private static object ReadConfig(string path, string valueName, RegistryValueKind kind)
		{
			object obj = null;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(path))
				{
					if (registryKey == null)
					{
						return obj;
					}
					try
					{
						object value = registryKey.GetValue(valueName, null);
						if (value != null && registryKey.GetValueKind(valueName) == kind)
						{
							obj = value;
						}
					}
					catch (UnauthorizedAccessException)
					{
					}
					catch (IOException)
					{
					}
				}
			}
			catch (SecurityException)
			{
			}
			catch (ObjectDisposedException)
			{
			}
			return obj;
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x00040308 File Offset: 0x0003E508
		private static string GetNetFrameworkVersionedPath()
		{
			return string.Format(CultureInfo.InvariantCulture, "SOFTWARE\\Microsoft\\.NETFramework\\v{0}", new object[] { Environment.Version.ToString(3) });
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x0004033A File Offset: 0x0003E53A
		private static string GetAppConfigPath(string valueName)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}\\{1}", new object[]
			{
				RegistryConfiguration.GetNetFrameworkVersionedPath(),
				valueName
			});
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x00040360 File Offset: 0x0003E560
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		private static string GetAppConfigValueName()
		{
			string text = "Unknown";
			Process currentProcess = Process.GetCurrentProcess();
			try
			{
				ProcessModule mainModule = currentProcess.MainModule;
				text = mainModule.FileName;
			}
			catch (NotSupportedException)
			{
			}
			catch (Win32Exception)
			{
			}
			catch (InvalidOperationException)
			{
			}
			try
			{
				text = Path.GetFullPath(text);
			}
			catch (ArgumentException)
			{
			}
			catch (SecurityException)
			{
			}
			catch (NotSupportedException)
			{
			}
			catch (PathTooLongException)
			{
			}
			return text;
		}

		// Token: 0x0400111C RID: 4380
		private const string netFrameworkPath = "SOFTWARE\\Microsoft\\.NETFramework";

		// Token: 0x0400111D RID: 4381
		private const string netFrameworkVersionedPath = "SOFTWARE\\Microsoft\\.NETFramework\\v{0}";

		// Token: 0x0400111E RID: 4382
		private const string netFrameworkFullPath = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\.NETFramework";
	}
}
