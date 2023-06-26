using System;
using System.Configuration.Internal;

namespace System.Configuration
{
	// Token: 0x02000082 RID: 130
	internal static class ConfigurationManagerInternalFactory
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x000213EF File Offset: 0x0001F5EF
		internal static IConfigurationManagerInternal Instance
		{
			get
			{
				if (ConfigurationManagerInternalFactory.s_instance == null)
				{
					ConfigurationManagerInternalFactory.s_instance = (IConfigurationManagerInternal)TypeUtil.CreateInstanceWithReflectionPermission("System.Configuration.Internal.ConfigurationManagerInternal, System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
				}
				return ConfigurationManagerInternalFactory.s_instance;
			}
		}

		// Token: 0x04000C17 RID: 3095
		private const string ConfigurationManagerInternalTypeString = "System.Configuration.Internal.ConfigurationManagerInternal, System.Configuration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

		// Token: 0x04000C18 RID: 3096
		private static volatile IConfigurationManagerInternal s_instance;
	}
}
