using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the type information for a custom <see cref="T:System.Net.IWebProxy" /> module. This class cannot be inherited.</summary>
	// Token: 0x02000339 RID: 825
	public sealed class ModuleElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.ModuleElement" /> class.</summary>
		// Token: 0x06001D6A RID: 7530 RVA: 0x0008BB50 File Offset: 0x00089D50
		public ModuleElement()
		{
			this.properties.Add(this.type);
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06001D6B RID: 7531 RVA: 0x0008BB90 File Offset: 0x00089D90
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		/// <summary>Gets or sets the type and assembly information for the current instance.</summary>
		/// <returns>A string that identifies a type that implements the <see cref="T:System.Net.IWebProxy" /> interface or <see langword="null" /> if no value has been specified.</returns>
		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06001D6C RID: 7532 RVA: 0x0008BB98 File Offset: 0x00089D98
		// (set) Token: 0x06001D6D RID: 7533 RVA: 0x0008BBAB File Offset: 0x00089DAB
		[ConfigurationProperty("type")]
		public string Type
		{
			get
			{
				return (string)base[this.type];
			}
			set
			{
				base[this.type] = value;
			}
		}

		// Token: 0x04001C38 RID: 7224
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001C39 RID: 7225
		private readonly ConfigurationProperty type = new ConfigurationProperty("type", typeof(string), null, ConfigurationPropertyOptions.None);
	}
}
