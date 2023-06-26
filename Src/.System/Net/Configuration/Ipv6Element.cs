using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Determines whether Internet Protocol version 6 is enabled on the local computer. This class cannot be inherited.</summary>
	// Token: 0x02000336 RID: 822
	public sealed class Ipv6Element : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.Ipv6Element" /> class.</summary>
		// Token: 0x06001D61 RID: 7521 RVA: 0x0008BA90 File Offset: 0x00089C90
		public Ipv6Element()
		{
			this.properties.Add(this.enabled);
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06001D62 RID: 7522 RVA: 0x0008BAE0 File Offset: 0x00089CE0
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		/// <summary>Gets or sets a Boolean value that indicates whether Internet Protocol version 6 is enabled on the local computer.</summary>
		/// <returns>
		///   <see langword="true" /> if IPv6 is enabled; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06001D63 RID: 7523 RVA: 0x0008BAE8 File Offset: 0x00089CE8
		// (set) Token: 0x06001D64 RID: 7524 RVA: 0x0008BAFB File Offset: 0x00089CFB
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

		// Token: 0x04001C35 RID: 7221
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001C36 RID: 7222
		private readonly ConfigurationProperty enabled = new ConfigurationProperty("enabled", typeof(bool), false, ConfigurationPropertyOptions.None);
	}
}
