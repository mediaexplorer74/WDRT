using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Runtime.Versioning;

namespace System.Windows.Forms
{
	// Token: 0x0200010E RID: 270
	internal static class ConfigurationOptions
	{
		// Token: 0x06000737 RID: 1847 RVA: 0x00014AC8 File Offset: 0x00012CC8
		static ConfigurationOptions()
		{
			ConfigurationOptions.PopulateWinformsSection();
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00014B14 File Offset: 0x00012D14
		private static void PopulateWinformsSection()
		{
			if (ConfigurationOptions.NetFrameworkVersion.CompareTo(ConfigurationOptions.featureSupportedMinimumFrameworkVersion) >= 0)
			{
				try
				{
					ConfigurationOptions.applicationConfigOptions = ConfigurationManager.GetSection("System.Windows.Forms.ApplicationConfigurationSection") as NameValueCollection;
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x00014B5C File Offset: 0x00012D5C
		public static Version NetFrameworkVersion
		{
			get
			{
				if (ConfigurationOptions.netFrameworkVersion == null)
				{
					ConfigurationOptions.netFrameworkVersion = new Version(0, 0, 0, 0);
					try
					{
						string targetFrameworkName = AppDomain.CurrentDomain.SetupInformation.TargetFrameworkName;
						if (!string.IsNullOrEmpty(targetFrameworkName))
						{
							FrameworkName frameworkName = new FrameworkName(targetFrameworkName);
							if (string.Equals(frameworkName.Identifier, ".NETFramework"))
							{
								ConfigurationOptions.netFrameworkVersion = frameworkName.Version;
							}
						}
					}
					catch (Exception ex)
					{
					}
				}
				return ConfigurationOptions.netFrameworkVersion;
			}
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00014BDC File Offset: 0x00012DDC
		public static string GetConfigSettingValue(string settingName)
		{
			if (ConfigurationOptions.applicationConfigOptions != null && !string.IsNullOrEmpty(settingName))
			{
				return ConfigurationOptions.applicationConfigOptions.Get(settingName);
			}
			return null;
		}

		// Token: 0x040004DB RID: 1243
		private static NameValueCollection applicationConfigOptions = null;

		// Token: 0x040004DC RID: 1244
		private static Version netFrameworkVersion = null;

		// Token: 0x040004DD RID: 1245
		private static readonly Version featureSupportedMinimumFrameworkVersion = new Version(4, 7);

		// Token: 0x040004DE RID: 1246
		internal static Version OSVersion = Environment.OSVersion.Version;

		// Token: 0x040004DF RID: 1247
		internal static readonly Version RS2Version = new Version(10, 0, 14933, 0);
	}
}
