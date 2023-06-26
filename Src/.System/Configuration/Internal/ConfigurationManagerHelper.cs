using System;
using System.Net.Configuration;

namespace System.Configuration.Internal
{
	// Token: 0x020000BD RID: 189
	internal sealed class ConfigurationManagerHelper : IConfigurationManagerHelper
	{
		// Token: 0x06000642 RID: 1602 RVA: 0x00024253 File Offset: 0x00022453
		private ConfigurationManagerHelper()
		{
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0002425B File Offset: 0x0002245B
		void IConfigurationManagerHelper.EnsureNetConfigLoaded()
		{
			SettingsSection.EnsureConfigLoaded();
		}
	}
}
