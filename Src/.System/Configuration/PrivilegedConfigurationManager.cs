using System;
using System.Security.Permissions;

namespace System.Configuration
{
	// Token: 0x020000BC RID: 188
	[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
	internal static class PrivilegedConfigurationManager
	{
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x00024244 File Offset: 0x00022444
		internal static ConnectionStringSettingsCollection ConnectionStrings
		{
			get
			{
				return ConfigurationManager.ConnectionStrings;
			}
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0002424B File Offset: 0x0002244B
		internal static object GetSection(string sectionName)
		{
			return ConfigurationManager.GetSection(sectionName);
		}
	}
}
