using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Gets the section group information for the networking namespaces. This class cannot be inherited.</summary>
	// Token: 0x0200033A RID: 826
	public sealed class NetSectionGroup : ConfigurationSectionGroup
	{
		/// <summary>Gets the configuration section containing the authentication modules registered for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.AuthenticationModulesSection" /> object.</returns>
		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06001D6F RID: 7535 RVA: 0x0008BBC2 File Offset: 0x00089DC2
		[ConfigurationProperty("authenticationModules")]
		public AuthenticationModulesSection AuthenticationModules
		{
			get
			{
				return (AuthenticationModulesSection)base.Sections["authenticationModules"];
			}
		}

		/// <summary>Gets the configuration section containing the connection management settings for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.ConnectionManagementSection" /> object.</returns>
		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06001D70 RID: 7536 RVA: 0x0008BBD9 File Offset: 0x00089DD9
		[ConfigurationProperty("connectionManagement")]
		public ConnectionManagementSection ConnectionManagement
		{
			get
			{
				return (ConnectionManagementSection)base.Sections["connectionManagement"];
			}
		}

		/// <summary>Gets the configuration section containing the default Web proxy server settings for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.DefaultProxySection" /> object.</returns>
		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06001D71 RID: 7537 RVA: 0x0008BBF0 File Offset: 0x00089DF0
		[ConfigurationProperty("defaultProxy")]
		public DefaultProxySection DefaultProxy
		{
			get
			{
				return (DefaultProxySection)base.Sections["defaultProxy"];
			}
		}

		/// <summary>Gets the configuration section containing the SMTP client email settings for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.MailSettingsSectionGroup" /> object.</returns>
		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06001D72 RID: 7538 RVA: 0x0008BC07 File Offset: 0x00089E07
		public MailSettingsSectionGroup MailSettings
		{
			get
			{
				return (MailSettingsSectionGroup)base.SectionGroups["mailSettings"];
			}
		}

		/// <summary>Gets the <see langword="System.Net" /> configuration section group from the specified configuration file.</summary>
		/// <param name="config">A <see cref="T:System.Configuration.Configuration" /> that represents a configuration file.</param>
		/// <returns>A <see cref="T:System.Net.Configuration.NetSectionGroup" /> that represents the <see langword="System.Net" /> settings in <paramref name="config" />.</returns>
		// Token: 0x06001D73 RID: 7539 RVA: 0x0008BC1E File Offset: 0x00089E1E
		public static NetSectionGroup GetSectionGroup(Configuration config)
		{
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			return config.GetSectionGroup("system.net") as NetSectionGroup;
		}

		/// <summary>Gets the configuration section containing the cache configuration settings for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.RequestCachingSection" /> object.</returns>
		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06001D74 RID: 7540 RVA: 0x0008BC3E File Offset: 0x00089E3E
		[ConfigurationProperty("requestCaching")]
		public RequestCachingSection RequestCaching
		{
			get
			{
				return (RequestCachingSection)base.Sections["requestCaching"];
			}
		}

		/// <summary>Gets the configuration section containing the network settings for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.SettingsSection" /> object.</returns>
		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06001D75 RID: 7541 RVA: 0x0008BC55 File Offset: 0x00089E55
		[ConfigurationProperty("settings")]
		public SettingsSection Settings
		{
			get
			{
				return (SettingsSection)base.Sections["settings"];
			}
		}

		/// <summary>Gets the configuration section containing the modules registered for use with the <see cref="T:System.Net.WebRequest" /> class.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.WebRequestModulesSection" /> object.</returns>
		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06001D76 RID: 7542 RVA: 0x0008BC6C File Offset: 0x00089E6C
		[ConfigurationProperty("webRequestModules")]
		public WebRequestModulesSection WebRequestModules
		{
			get
			{
				return (WebRequestModulesSection)base.Sections["webRequestModules"];
			}
		}
	}
}
