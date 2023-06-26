using System;

namespace System.Configuration
{
	/// <summary>Represents the Uri section within a configuration file.</summary>
	// Token: 0x02000075 RID: 117
	public sealed class UriSection : ConfigurationSection
	{
		// Token: 0x060004AF RID: 1199 RVA: 0x0001FA04 File Offset: 0x0001DC04
		static UriSection()
		{
			UriSection.properties.Add(UriSection.idn);
			UriSection.properties.Add(UriSection.iriParsing);
			UriSection.properties.Add(UriSection.schemeSettings);
		}

		/// <summary>Gets an <see cref="T:System.Configuration.IdnElement" /> object that contains the configuration setting for International Domain Name (IDN) processing in the <see cref="T:System.Uri" /> class.</summary>
		/// <returns>The configuration setting for International Domain Name (IDN) processing in the <see cref="T:System.Uri" /> class.</returns>
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x0001FA99 File Offset: 0x0001DC99
		[ConfigurationProperty("idn")]
		public IdnElement Idn
		{
			get
			{
				return (IdnElement)base[UriSection.idn];
			}
		}

		/// <summary>Gets an <see cref="T:System.Configuration.IriParsingElement" /> object that contains the configuration setting for International Resource Identifiers (IRI) parsing in the <see cref="T:System.Uri" /> class.</summary>
		/// <returns>The configuration setting for International Resource Identifiers (IRI) parsing in the <see cref="T:System.Uri" /> class.</returns>
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x0001FAAB File Offset: 0x0001DCAB
		[ConfigurationProperty("iriParsing")]
		public IriParsingElement IriParsing
		{
			get
			{
				return (IriParsingElement)base[UriSection.iriParsing];
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.SchemeSettingElementCollection" /> object that contains the configuration settings for scheme parsing in the <see cref="T:System.Uri" /> class.</summary>
		/// <returns>The configuration settings for scheme parsing in the <see cref="T:System.Uri" /> class</returns>
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0001FABD File Offset: 0x0001DCBD
		[ConfigurationProperty("schemeSettings")]
		public SchemeSettingElementCollection SchemeSettings
		{
			get
			{
				return (SchemeSettingElementCollection)base[UriSection.schemeSettings];
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x0001FACF File Offset: 0x0001DCCF
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return UriSection.properties;
			}
		}

		// Token: 0x04000BED RID: 3053
		private static readonly ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04000BEE RID: 3054
		private static readonly ConfigurationProperty idn = new ConfigurationProperty("idn", typeof(IdnElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04000BEF RID: 3055
		private static readonly ConfigurationProperty iriParsing = new ConfigurationProperty("iriParsing", typeof(IriParsingElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04000BF0 RID: 3056
		private static readonly ConfigurationProperty schemeSettings = new ConfigurationProperty("schemeSettings", typeof(SchemeSettingElementCollection), null, ConfigurationPropertyOptions.None);
	}
}
