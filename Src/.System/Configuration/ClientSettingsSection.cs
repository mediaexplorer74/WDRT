using System;

namespace System.Configuration
{
	/// <summary>Represents a group of user-scoped application settings in a configuration file.</summary>
	// Token: 0x020000B7 RID: 183
	public sealed class ClientSettingsSection : ConfigurationSection
	{
		// Token: 0x06000615 RID: 1557 RVA: 0x00023DA2 File Offset: 0x00021FA2
		static ClientSettingsSection()
		{
			ClientSettingsSection._properties.Add(ClientSettingsSection._propSettings);
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x00023DDC File Offset: 0x00021FDC
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ClientSettingsSection._properties;
			}
		}

		/// <summary>Gets the collection of client settings for the section.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingElementCollection" /> containing all the client settings found in the current configuration section.</returns>
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x00023DE3 File Offset: 0x00021FE3
		[ConfigurationProperty("", IsDefaultCollection = true)]
		public SettingElementCollection Settings
		{
			get
			{
				return (SettingElementCollection)base[ClientSettingsSection._propSettings];
			}
		}

		// Token: 0x04000C5D RID: 3165
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x04000C5E RID: 3166
		private static readonly ConfigurationProperty _propSettings = new ConfigurationProperty(null, typeof(SettingElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
