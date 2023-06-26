using System;
using System.Globalization;
using System.IO;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32;

namespace System.Net
{
	// Token: 0x02000106 RID: 262
	internal static class HttpSysSettings
	{
		// Token: 0x060009A9 RID: 2473 RVA: 0x00035F7F File Offset: 0x0003417F
		static HttpSysSettings()
		{
			HttpSysSettings.ReadHttpSysRegistrySettings();
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x00035F96 File Offset: 0x00034196
		public static bool EnableNonUtf8
		{
			get
			{
				return HttpSysSettings.enableNonUtf8;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x00035F9F File Offset: 0x0003419F
		public static bool FavorUtf8
		{
			get
			{
				return HttpSysSettings.favorUtf8;
			}
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00035FA8 File Offset: 0x000341A8
		[RegistryPermission(SecurityAction.Assert, Read = "HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Services\\HTTP\\Parameters")]
		private static void ReadHttpSysRegistrySettings()
		{
			try
			{
				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Services\\HTTP\\Parameters");
				if (registryKey == null)
				{
					HttpSysSettings.LogWarning("ReadHttpSysRegistrySettings", "net_log_listener_httpsys_registry_null", new object[] { "System\\CurrentControlSet\\Services\\HTTP\\Parameters" });
				}
				else
				{
					using (registryKey)
					{
						HttpSysSettings.enableNonUtf8 = HttpSysSettings.ReadRegistryValue(registryKey, "EnableNonUtf8", true);
						HttpSysSettings.favorUtf8 = HttpSysSettings.ReadRegistryValue(registryKey, "FavorUtf8", true);
					}
				}
			}
			catch (SecurityException ex)
			{
				HttpSysSettings.LogRegistryException("ReadHttpSysRegistrySettings", ex);
			}
			catch (ObjectDisposedException ex2)
			{
				HttpSysSettings.LogRegistryException("ReadHttpSysRegistrySettings", ex2);
			}
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00036064 File Offset: 0x00034264
		private static bool ReadRegistryValue(RegistryKey key, string valueName, bool defaultValue)
		{
			try
			{
				if (key.GetValueKind(valueName) == RegistryValueKind.DWord)
				{
					return Convert.ToBoolean(key.GetValue(valueName), CultureInfo.InvariantCulture);
				}
			}
			catch (UnauthorizedAccessException ex)
			{
				HttpSysSettings.LogRegistryException("ReadRegistryValue", ex);
			}
			catch (IOException ex2)
			{
				HttpSysSettings.LogRegistryException("ReadRegistryValue", ex2);
			}
			catch (SecurityException ex3)
			{
				HttpSysSettings.LogRegistryException("ReadRegistryValue", ex3);
			}
			catch (ObjectDisposedException ex4)
			{
				HttpSysSettings.LogRegistryException("ReadRegistryValue", ex4);
			}
			return defaultValue;
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x00036104 File Offset: 0x00034304
		private static void LogRegistryException(string methodName, Exception e)
		{
			HttpSysSettings.LogWarning(methodName, "net_log_listener_httpsys_registry_error", new object[] { "System\\CurrentControlSet\\Services\\HTTP\\Parameters", e });
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00036123 File Offset: 0x00034323
		private static void LogWarning(string methodName, string message, params object[] args)
		{
			if (Logging.On)
			{
				Logging.PrintWarning(Logging.HttpListener, typeof(HttpSysSettings), methodName, SR.GetString(message, args));
			}
		}

		// Token: 0x04000EC1 RID: 3777
		private const string httpSysParametersKey = "System\\CurrentControlSet\\Services\\HTTP\\Parameters";

		// Token: 0x04000EC2 RID: 3778
		private const bool enableNonUtf8Default = true;

		// Token: 0x04000EC3 RID: 3779
		private const bool favorUtf8Default = true;

		// Token: 0x04000EC4 RID: 3780
		private const string enableNonUtf8Name = "EnableNonUtf8";

		// Token: 0x04000EC5 RID: 3781
		private const string favorUtf8Name = "FavorUtf8";

		// Token: 0x04000EC6 RID: 3782
		private static volatile bool enableNonUtf8 = true;

		// Token: 0x04000EC7 RID: 3783
		private static volatile bool favorUtf8 = true;
	}
}
