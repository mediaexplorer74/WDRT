using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;

namespace System.Net.Configuration
{
	/// <summary>Represents the WebUtility element in the configuration file.</summary>
	// Token: 0x0200034E RID: 846
	public sealed class WebUtilityElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.WebUtilityElement" /> class.</summary>
		// Token: 0x06001E45 RID: 7749 RVA: 0x0008DC38 File Offset: 0x0008BE38
		public WebUtilityElement()
		{
			this.properties.Add(this.unicodeDecodingConformance);
			this.properties.Add(this.unicodeEncodingConformance);
		}

		/// <summary>Gets the default Unicode decoding conformance behavior used for an <see cref="T:System.Net.WebUtility" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Net.Configuration.UnicodeDecodingConformance" />.  
		///  The default Unicode decoding behavior.</returns>
		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06001E46 RID: 7750 RVA: 0x0008DCC6 File Offset: 0x0008BEC6
		// (set) Token: 0x06001E47 RID: 7751 RVA: 0x0008DCD9 File Offset: 0x0008BED9
		[ConfigurationProperty("unicodeDecodingConformance", DefaultValue = UnicodeDecodingConformance.Auto)]
		public UnicodeDecodingConformance UnicodeDecodingConformance
		{
			get
			{
				return (UnicodeDecodingConformance)base[this.unicodeDecodingConformance];
			}
			set
			{
				base[this.unicodeDecodingConformance] = value;
			}
		}

		/// <summary>Gets the default Unicode encoding conformance behavior used for an <see cref="T:System.Net.WebUtility" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Net.Configuration.UnicodeEncodingConformance" />.  
		///  The default Unicode encoding behavior.</returns>
		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06001E48 RID: 7752 RVA: 0x0008DCED File Offset: 0x0008BEED
		// (set) Token: 0x06001E49 RID: 7753 RVA: 0x0008DD00 File Offset: 0x0008BF00
		[ConfigurationProperty("unicodeEncodingConformance", DefaultValue = UnicodeEncodingConformance.Auto)]
		public UnicodeEncodingConformance UnicodeEncodingConformance
		{
			get
			{
				return (UnicodeEncodingConformance)base[this.unicodeEncodingConformance];
			}
			set
			{
				base[this.unicodeEncodingConformance] = value;
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06001E4A RID: 7754 RVA: 0x0008DD14 File Offset: 0x0008BF14
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04001CAA RID: 7338
		private readonly ConfigurationProperty unicodeDecodingConformance = new ConfigurationProperty("unicodeDecodingConformance", typeof(UnicodeDecodingConformance), UnicodeDecodingConformance.Auto, new WebUtilityElement.EnumTypeConverter<UnicodeDecodingConformance>(), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001CAB RID: 7339
		private readonly ConfigurationProperty unicodeEncodingConformance = new ConfigurationProperty("unicodeEncodingConformance", typeof(UnicodeEncodingConformance), UnicodeEncodingConformance.Auto, new WebUtilityElement.EnumTypeConverter<UnicodeEncodingConformance>(), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001CAC RID: 7340
		private readonly ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x020007C9 RID: 1993
		private class EnumTypeConverter<TEnum> : TypeConverter where TEnum : struct
		{
			// Token: 0x06004378 RID: 17272 RVA: 0x0011C451 File Offset: 0x0011A651
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
			}

			// Token: 0x06004379 RID: 17273 RVA: 0x0011C470 File Offset: 0x0011A670
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				string text = value as string;
				TEnum tenum;
				if (text != null && Enum.TryParse<TEnum>(text, true, out tenum))
				{
					return tenum;
				}
				return base.ConvertFrom(context, culture, value);
			}
		}
	}
}
