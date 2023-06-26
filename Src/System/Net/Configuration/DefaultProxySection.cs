using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for Web proxy server usage. This class cannot be inherited.</summary>
	// Token: 0x0200032F RID: 815
	public sealed class DefaultProxySection : ConfigurationSection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.DefaultProxySection" /> class.</summary>
		// Token: 0x06001D23 RID: 7459 RVA: 0x0008ACC8 File Offset: 0x00088EC8
		public DefaultProxySection()
		{
			this.properties.Add(this.bypasslist);
			this.properties.Add(this.module);
			this.properties.Add(this.proxy);
			this.properties.Add(this.enabled);
			this.properties.Add(this.useDefaultCredentials);
		}

		// Token: 0x06001D24 RID: 7460 RVA: 0x0008ADD4 File Offset: 0x00088FD4
		protected override void PostDeserialize()
		{
			if (base.EvaluationContext.IsMachineLevel)
			{
				return;
			}
			try
			{
				ExceptionHelper.WebPermissionUnrestricted.Demand();
			}
			catch (Exception ex)
			{
				throw new ConfigurationErrorsException(SR.GetString("net_config_section_permission", new object[] { "defaultProxy" }), ex);
			}
		}

		/// <summary>Gets the collection of resources that are not obtained using the Web proxy server.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.BypassElementCollection" /> that contains the addresses of resources that bypass the Web proxy server.</returns>
		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06001D25 RID: 7461 RVA: 0x0008AE2C File Offset: 0x0008902C
		[ConfigurationProperty("bypasslist")]
		public BypassElementCollection BypassList
		{
			get
			{
				return (BypassElementCollection)base[this.bypasslist];
			}
		}

		/// <summary>Gets the type information for a custom Web proxy implementation.</summary>
		/// <returns>The type information for a custom Web proxy implementation.</returns>
		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06001D26 RID: 7462 RVA: 0x0008AE3F File Offset: 0x0008903F
		[ConfigurationProperty("module")]
		public ModuleElement Module
		{
			get
			{
				return (ModuleElement)base[this.module];
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06001D27 RID: 7463 RVA: 0x0008AE52 File Offset: 0x00089052
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		/// <summary>Gets the URI that identifies the Web proxy server to use.</summary>
		/// <returns>The URI that identifies the Web proxy server.</returns>
		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06001D28 RID: 7464 RVA: 0x0008AE5A File Offset: 0x0008905A
		[ConfigurationProperty("proxy")]
		public ProxyElement Proxy
		{
			get
			{
				return (ProxyElement)base[this.proxy];
			}
		}

		/// <summary>Gets or sets whether a Web proxy is used.</summary>
		/// <returns>
		///   <see langword="true" /> if a Web proxy will be used; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06001D29 RID: 7465 RVA: 0x0008AE6D File Offset: 0x0008906D
		// (set) Token: 0x06001D2A RID: 7466 RVA: 0x0008AE80 File Offset: 0x00089080
		[ConfigurationProperty("enabled", DefaultValue = true)]
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

		/// <summary>Gets or sets whether default credentials are to be used to access a Web proxy server.</summary>
		/// <returns>
		///   <see langword="true" /> if default credentials are to be used; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06001D2B RID: 7467 RVA: 0x0008AE94 File Offset: 0x00089094
		// (set) Token: 0x06001D2C RID: 7468 RVA: 0x0008AEA7 File Offset: 0x000890A7
		[ConfigurationProperty("useDefaultCredentials", DefaultValue = false)]
		public bool UseDefaultCredentials
		{
			get
			{
				return (bool)base[this.useDefaultCredentials];
			}
			set
			{
				base[this.useDefaultCredentials] = value;
			}
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x0008AEBC File Offset: 0x000890BC
		protected override void Reset(ConfigurationElement parentElement)
		{
			DefaultProxySection defaultProxySection = new DefaultProxySection();
			defaultProxySection.InitializeDefault();
			base.Reset(defaultProxySection);
		}

		// Token: 0x04001C14 RID: 7188
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001C15 RID: 7189
		private readonly ConfigurationProperty bypasslist = new ConfigurationProperty("bypasslist", typeof(BypassElementCollection), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C16 RID: 7190
		private readonly ConfigurationProperty module = new ConfigurationProperty("module", typeof(ModuleElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C17 RID: 7191
		private readonly ConfigurationProperty proxy = new ConfigurationProperty("proxy", typeof(ProxyElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C18 RID: 7192
		private readonly ConfigurationProperty enabled = new ConfigurationProperty("enabled", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04001C19 RID: 7193
		private readonly ConfigurationProperty useDefaultCredentials = new ConfigurationProperty("useDefaultCredentials", typeof(bool), false, ConfigurationPropertyOptions.None);
	}
}
