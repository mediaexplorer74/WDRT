using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for connection management. This class cannot be inherited.</summary>
	// Token: 0x0200032D RID: 813
	public sealed class ConnectionManagementSection : ConfigurationSection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.ConnectionManagementSection" /> class.</summary>
		// Token: 0x06001D1C RID: 7452 RVA: 0x0008AB35 File Offset: 0x00088D35
		public ConnectionManagementSection()
		{
			this.properties.Add(this.connectionManagement);
		}

		/// <summary>Gets the collection of connection management objects in the section.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.ConnectionManagementElementCollection" /> that contains the connection management information for the local computer.</returns>
		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06001D1D RID: 7453 RVA: 0x0008AB71 File Offset: 0x00088D71
		[ConfigurationProperty("", IsDefaultCollection = true)]
		public ConnectionManagementElementCollection ConnectionManagement
		{
			get
			{
				return (ConnectionManagementElementCollection)base[this.connectionManagement];
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06001D1E RID: 7454 RVA: 0x0008AB84 File Offset: 0x00088D84
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04001C10 RID: 7184
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001C11 RID: 7185
		private readonly ConfigurationProperty connectionManagement = new ConfigurationProperty(null, typeof(ConnectionManagementElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
