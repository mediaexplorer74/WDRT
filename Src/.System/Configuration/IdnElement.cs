using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Configuration
{
	/// <summary>Provides the configuration setting for International Domain Name (IDN) processing in the <see cref="T:System.Uri" /> class.</summary>
	// Token: 0x02000078 RID: 120
	public sealed class IdnElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.IdnElement" /> class.</summary>
		// Token: 0x060004C4 RID: 1220 RVA: 0x0001FE94 File Offset: 0x0001E094
		public IdnElement()
		{
			this.properties.Add(this.enabled);
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0001FEEA File Offset: 0x0001E0EA
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		/// <summary>Gets or sets the value of the <see cref="T:System.Configuration.IdnElement" /> configuration setting.</summary>
		/// <returns>A <see cref="T:System.UriIdnScope" /> that contains the current configuration setting for IDN processing.</returns>
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x0001FEF2 File Offset: 0x0001E0F2
		// (set) Token: 0x060004C7 RID: 1223 RVA: 0x0001FF05 File Offset: 0x0001E105
		[ConfigurationProperty("enabled", DefaultValue = UriIdnScope.None)]
		public UriIdnScope Enabled
		{
			get
			{
				return (UriIdnScope)base[this.enabled];
			}
			set
			{
				base[this.enabled] = value;
			}
		}

		// Token: 0x04000BF8 RID: 3064
		internal const UriIdnScope EnabledDefaultValue = UriIdnScope.None;

		// Token: 0x04000BF9 RID: 3065
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04000BFA RID: 3066
		private readonly ConfigurationProperty enabled = new ConfigurationProperty("enabled", typeof(UriIdnScope), UriIdnScope.None, new IdnElement.UriIdnScopeTypeConverter(), null, ConfigurationPropertyOptions.None);

		// Token: 0x020006E6 RID: 1766
		private class UriIdnScopeTypeConverter : TypeConverter
		{
			// Token: 0x06004024 RID: 16420 RVA: 0x0010D384 File Offset: 0x0010B584
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
			}

			// Token: 0x06004025 RID: 16421 RVA: 0x0010D3A4 File Offset: 0x0010B5A4
			public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
			{
				string text = value as string;
				if (text != null)
				{
					text = text.ToLower(CultureInfo.InvariantCulture);
					if (text == "all")
					{
						return UriIdnScope.All;
					}
					if (text == "none")
					{
						return UriIdnScope.None;
					}
					if (text == "allexceptintranet")
					{
						return UriIdnScope.AllExceptIntranet;
					}
				}
				return base.ConvertFrom(context, culture, value);
			}
		}
	}
}
