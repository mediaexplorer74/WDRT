using System;
using System.Configuration;

namespace System.Security.Authentication.ExtendedProtection.Configuration
{
	/// <summary>The <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> class represents a configuration element for a service name used in a <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" />.</summary>
	// Token: 0x0200044C RID: 1100
	public sealed class ServiceNameElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> class.</summary>
		// Token: 0x060028A9 RID: 10409 RVA: 0x000BA750 File Offset: 0x000B8950
		public ServiceNameElement()
		{
			this.properties.Add(this.name);
		}

		/// <summary>Gets or sets the Service Provider Name (SPN) for this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the representation of SPN for this <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElement" /> instance.</returns>
		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x060028AA RID: 10410 RVA: 0x000BA790 File Offset: 0x000B8990
		// (set) Token: 0x060028AB RID: 10411 RVA: 0x000BA7A3 File Offset: 0x000B89A3
		[ConfigurationProperty("name")]
		public string Name
		{
			get
			{
				return (string)base[this.name];
			}
			set
			{
				base[this.name] = value;
			}
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x060028AC RID: 10412 RVA: 0x000BA7B2 File Offset: 0x000B89B2
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x060028AD RID: 10413 RVA: 0x000BA7BA File Offset: 0x000B89BA
		internal string Key
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x04002265 RID: 8805
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002266 RID: 8806
		private readonly ConfigurationProperty name = new ConfigurationProperty("name", typeof(string), null, ConfigurationPropertyOptions.IsRequired);
	}
}
