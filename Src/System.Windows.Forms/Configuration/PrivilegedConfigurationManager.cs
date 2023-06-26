using System;
using System.Security.Permissions;

namespace System.Configuration
{
	// Token: 0x020000F5 RID: 245
	[ConfigurationPermission(SecurityAction.Assert, Unrestricted = true)]
	internal static class PrivilegedConfigurationManager
	{
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0000BF04 File Offset: 0x0000A104
		internal static ConnectionStringSettingsCollection ConnectionStrings
		{
			get
			{
				return ConfigurationManager.ConnectionStrings;
			}
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000BF0B File Offset: 0x0000A10B
		internal static object GetSection(string sectionName)
		{
			return ConfigurationManager.GetSection(sectionName);
		}
	}
}
