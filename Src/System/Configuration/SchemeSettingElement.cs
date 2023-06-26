using System;

namespace System.Configuration
{
	/// <summary>Represents an element in a <see cref="T:System.Configuration.SchemeSettingElementCollection" /> class.</summary>
	// Token: 0x02000070 RID: 112
	public sealed class SchemeSettingElement : ConfigurationElement
	{
		// Token: 0x0600048C RID: 1164 RVA: 0x0001F3C0 File Offset: 0x0001D5C0
		static SchemeSettingElement()
		{
			SchemeSettingElement.properties.Add(SchemeSettingElement.name);
			SchemeSettingElement.properties.Add(SchemeSettingElement.genericUriParserOptions);
		}

		/// <summary>Gets the value of the Name entry from a <see cref="T:System.Configuration.SchemeSettingElement" /> instance.</summary>
		/// <returns>The protocol used by this schema setting.</returns>
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x0001F430 File Offset: 0x0001D630
		[ConfigurationProperty("name", DefaultValue = null, IsRequired = true, IsKey = true)]
		public string Name
		{
			get
			{
				return (string)base[SchemeSettingElement.name];
			}
		}

		/// <summary>Gets the value of the GenericUriParserOptions entry from a <see cref="T:System.Configuration.SchemeSettingElement" /> instance.</summary>
		/// <returns>The value of GenericUriParserOptions entry.</returns>
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x0001F442 File Offset: 0x0001D642
		[ConfigurationProperty("genericUriParserOptions", DefaultValue = ConfigurationPropertyOptions.None, IsRequired = true)]
		public GenericUriParserOptions GenericUriParserOptions
		{
			get
			{
				return (GenericUriParserOptions)base[SchemeSettingElement.genericUriParserOptions];
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x0001F454 File Offset: 0x0001D654
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SchemeSettingElement.properties;
			}
		}

		// Token: 0x04000BDE RID: 3038
		private static readonly ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04000BDF RID: 3039
		private static readonly ConfigurationProperty name = new ConfigurationProperty("name", typeof(string), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04000BE0 RID: 3040
		private static readonly ConfigurationProperty genericUriParserOptions = new ConfigurationProperty("genericUriParserOptions", typeof(GenericUriParserOptions), GenericUriParserOptions.Default, ConfigurationPropertyOptions.IsRequired);
	}
}
