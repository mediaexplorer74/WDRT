using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the maximum number of connections to a remote computer. This class cannot be inherited.</summary>
	// Token: 0x0200032B RID: 811
	public sealed class ConnectionManagementElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.ConnectionManagementElement" /> class.</summary>
		// Token: 0x06001D07 RID: 7431 RVA: 0x0008A988 File Offset: 0x00088B88
		public ConnectionManagementElement()
		{
			this.properties.Add(this.address);
			this.properties.Add(this.maxconnection);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.ConnectionManagementElement" /> class with the specified address and connection limit information.</summary>
		/// <param name="address">A string that identifies the address of a remote computer.</param>
		/// <param name="maxConnection">An integer that identifies the maximum number of connections allowed to <paramref name="address" /> from the local computer.</param>
		// Token: 0x06001D08 RID: 7432 RVA: 0x0008AA05 File Offset: 0x00088C05
		public ConnectionManagementElement(string address, int maxConnection)
			: this()
		{
			this.Address = address;
			this.MaxConnection = maxConnection;
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06001D09 RID: 7433 RVA: 0x0008AA1B File Offset: 0x00088C1B
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		/// <summary>Gets or sets the address for remote computers.</summary>
		/// <returns>A string that contains a regular expression describing an IP address or DNS name.</returns>
		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06001D0A RID: 7434 RVA: 0x0008AA23 File Offset: 0x00088C23
		// (set) Token: 0x06001D0B RID: 7435 RVA: 0x0008AA36 File Offset: 0x00088C36
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

		/// <summary>Gets or sets the maximum number of connections that can be made to a remote computer.</summary>
		/// <returns>An integer that specifies the maximum number of connections.</returns>
		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06001D0C RID: 7436 RVA: 0x0008AA45 File Offset: 0x00088C45
		// (set) Token: 0x06001D0D RID: 7437 RVA: 0x0008AA58 File Offset: 0x00088C58
		[ConfigurationProperty("maxconnection", IsRequired = true, DefaultValue = 1)]
		public int MaxConnection
		{
			get
			{
				return (int)base[this.maxconnection];
			}
			set
			{
				base[this.maxconnection] = value;
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06001D0E RID: 7438 RVA: 0x0008AA6C File Offset: 0x00088C6C
		internal string Key
		{
			get
			{
				return this.Address;
			}
		}

		// Token: 0x04001C0D RID: 7181
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001C0E RID: 7182
		private readonly ConfigurationProperty address = new ConfigurationProperty("address", typeof(string), null, ConfigurationPropertyOptions.IsKey);

		// Token: 0x04001C0F RID: 7183
		private readonly ConfigurationProperty maxconnection = new ConfigurationProperty("maxconnection", typeof(int), 1, ConfigurationPropertyOptions.None);
	}
}
