using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the address information for resources that are not retrieved using a proxy server. This class cannot be inherited.</summary>
	// Token: 0x02000328 RID: 808
	public sealed class BypassElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.BypassElement" /> class.</summary>
		// Token: 0x06001CEA RID: 7402 RVA: 0x0008A7A0 File Offset: 0x000889A0
		public BypassElement()
		{
			this.properties.Add(this.address);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.BypassElement" /> class with the specified type information.</summary>
		/// <param name="address">A string that identifies the address of a resource.</param>
		// Token: 0x06001CEB RID: 7403 RVA: 0x0008A7E0 File Offset: 0x000889E0
		public BypassElement(string address)
			: this()
		{
			this.Address = address;
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06001CEC RID: 7404 RVA: 0x0008A7EF File Offset: 0x000889EF
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		/// <summary>Gets or sets the addresses of resources that bypass the proxy server.</summary>
		/// <returns>A string that identifies a resource.</returns>
		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06001CED RID: 7405 RVA: 0x0008A7F7 File Offset: 0x000889F7
		// (set) Token: 0x06001CEE RID: 7406 RVA: 0x0008A80A File Offset: 0x00088A0A
		[ConfigurationProperty("address", IsRequired = true, IsKey = true)]
		public string Address
		{
			get
			{
				return (string)base[this.address];
			}
			set
			{
				base[this.address] = value;
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001CEF RID: 7407 RVA: 0x0008A819 File Offset: 0x00088A19
		internal string Key
		{
			get
			{
				return this.Address;
			}
		}

		// Token: 0x04001BB2 RID: 7090
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001BB3 RID: 7091
		private readonly ConfigurationProperty address = new ConfigurationProperty("address", typeof(string), null, ConfigurationPropertyOptions.IsKey);
	}
}
