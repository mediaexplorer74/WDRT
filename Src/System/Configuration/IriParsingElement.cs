using System;

namespace System.Configuration
{
	/// <summary>Provides the configuration setting for International Resource Identifier (IRI) processing in the <see cref="T:System.Uri" /> class.</summary>
	// Token: 0x02000077 RID: 119
	public sealed class IriParsingElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.IriParsingElement" /> class.</summary>
		// Token: 0x060004C0 RID: 1216 RVA: 0x0001FE14 File Offset: 0x0001E014
		public IriParsingElement()
		{
			this.properties.Add(this.enabled);
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x0001FE64 File Offset: 0x0001E064
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		/// <summary>Gets or sets the value of the <see cref="T:System.Configuration.IriParsingElement" /> configuration setting.</summary>
		/// <returns>A Boolean that indicates if International Resource Identifier (IRI) processing is enabled.</returns>
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x0001FE6C File Offset: 0x0001E06C
		// (set) Token: 0x060004C3 RID: 1219 RVA: 0x0001FE7F File Offset: 0x0001E07F
		[ConfigurationProperty("enabled", DefaultValue = false)]
		public bool Enabled
		{
			get
			{
				return (bool)base[this.enabled];
			}
			set
			{
				base[this.enabled] = value;
			}
		}

		// Token: 0x04000BF5 RID: 3061
		internal const bool EnabledDefaultValue = false;

		// Token: 0x04000BF6 RID: 3062
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04000BF7 RID: 3063
		private readonly ConfigurationProperty enabled = new ConfigurationProperty("enabled", typeof(bool), false, ConfigurationPropertyOptions.None);
	}
}
