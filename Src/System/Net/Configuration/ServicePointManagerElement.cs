using System;
using System.Configuration;
using System.Net.Security;

namespace System.Net.Configuration
{
	/// <summary>Represents the default settings used to create connections to a remote computer. This class cannot be inherited.</summary>
	// Token: 0x02000341 RID: 833
	public sealed class ServicePointManagerElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.ServicePointManagerElement" /> class.</summary>
		// Token: 0x06001DD3 RID: 7635 RVA: 0x0008CA18 File Offset: 0x0008AC18
		public ServicePointManagerElement()
		{
			this.properties.Add(this.checkCertificateName);
			this.properties.Add(this.checkCertificateRevocationList);
			this.properties.Add(this.dnsRefreshTimeout);
			this.properties.Add(this.enableDnsRoundRobin);
			this.properties.Add(this.encryptionPolicy);
			this.properties.Add(this.expect100Continue);
			this.properties.Add(this.useNagleAlgorithm);
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x0008CBA0 File Offset: 0x0008ADA0
		protected override void PostDeserialize()
		{
			if (base.EvaluationContext.IsMachineLevel)
			{
				return;
			}
			PropertyInformation[] array = new PropertyInformation[]
			{
				base.ElementInformation.Properties["checkCertificateName"],
				base.ElementInformation.Properties["checkCertificateRevocationList"]
			};
			foreach (PropertyInformation propertyInformation in array)
			{
				if (propertyInformation.ValueOrigin == PropertyValueOrigin.SetHere)
				{
					try
					{
						ExceptionHelper.UnmanagedPermission.Demand();
					}
					catch (Exception ex)
					{
						throw new ConfigurationErrorsException(SR.GetString("net_config_property_permission", new object[] { propertyInformation.Name }), ex);
					}
				}
			}
		}

		/// <summary>Gets or sets a Boolean value that controls checking host name information in an X509 certificate.</summary>
		/// <returns>
		///   <see langword="true" /> to specify host name checking; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06001DD5 RID: 7637 RVA: 0x0008CC50 File Offset: 0x0008AE50
		// (set) Token: 0x06001DD6 RID: 7638 RVA: 0x0008CC63 File Offset: 0x0008AE63
		[ConfigurationProperty("checkCertificateName", DefaultValue = true)]
		public bool CheckCertificateName
		{
			get
			{
				return (bool)base[this.checkCertificateName];
			}
			set
			{
				base[this.checkCertificateName] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that indicates whether the certificate is checked against the certificate authority revocation list.</summary>
		/// <returns>
		///   <see langword="true" /> if the certificate revocation list is checked; otherwise, <see langword="false" />.The default value is <see langword="false" />.</returns>
		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06001DD7 RID: 7639 RVA: 0x0008CC77 File Offset: 0x0008AE77
		// (set) Token: 0x06001DD8 RID: 7640 RVA: 0x0008CC8A File Offset: 0x0008AE8A
		[ConfigurationProperty("checkCertificateRevocationList", DefaultValue = false)]
		public bool CheckCertificateRevocationList
		{
			get
			{
				return (bool)base[this.checkCertificateRevocationList];
			}
			set
			{
				base[this.checkCertificateRevocationList] = value;
			}
		}

		/// <summary>Gets or sets the amount of time after which address information is refreshed.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that specifies when addresses are resolved using DNS.</returns>
		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06001DD9 RID: 7641 RVA: 0x0008CC9E File Offset: 0x0008AE9E
		// (set) Token: 0x06001DDA RID: 7642 RVA: 0x0008CCB1 File Offset: 0x0008AEB1
		[ConfigurationProperty("dnsRefreshTimeout", DefaultValue = 120000)]
		public int DnsRefreshTimeout
		{
			get
			{
				return (int)base[this.dnsRefreshTimeout];
			}
			set
			{
				base[this.dnsRefreshTimeout] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that controls using different IP addresses on connections to the same server.</summary>
		/// <returns>
		///   <see langword="true" /> to enable DNS round-robin behavior; otherwise, <see langword="false" />.</returns>
		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06001DDB RID: 7643 RVA: 0x0008CCC5 File Offset: 0x0008AEC5
		// (set) Token: 0x06001DDC RID: 7644 RVA: 0x0008CCD8 File Offset: 0x0008AED8
		[ConfigurationProperty("enableDnsRoundRobin", DefaultValue = false)]
		public bool EnableDnsRoundRobin
		{
			get
			{
				return (bool)base[this.enableDnsRoundRobin];
			}
			set
			{
				base[this.enableDnsRoundRobin] = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Net.Security.EncryptionPolicy" /> to use.</summary>
		/// <returns>The encryption policy to use for a <see cref="T:System.Net.ServicePointManager" /> instance.</returns>
		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06001DDD RID: 7645 RVA: 0x0008CCEC File Offset: 0x0008AEEC
		// (set) Token: 0x06001DDE RID: 7646 RVA: 0x0008CCFF File Offset: 0x0008AEFF
		[ConfigurationProperty("encryptionPolicy", DefaultValue = EncryptionPolicy.RequireEncryption)]
		public EncryptionPolicy EncryptionPolicy
		{
			get
			{
				return (EncryptionPolicy)base[this.encryptionPolicy];
			}
			set
			{
				base[this.encryptionPolicy] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that determines whether 100-Continue behavior is used.</summary>
		/// <returns>
		///   <see langword="true" /> to expect 100-Continue responses for <see langword="POST" /> requests; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06001DDF RID: 7647 RVA: 0x0008CD13 File Offset: 0x0008AF13
		// (set) Token: 0x06001DE0 RID: 7648 RVA: 0x0008CD26 File Offset: 0x0008AF26
		[ConfigurationProperty("expect100Continue", DefaultValue = true)]
		public bool Expect100Continue
		{
			get
			{
				return (bool)base[this.expect100Continue];
			}
			set
			{
				base[this.expect100Continue] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that determines whether the Nagle algorithm is used.</summary>
		/// <returns>
		///   <see langword="true" /> to use the Nagle algorithm; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06001DE1 RID: 7649 RVA: 0x0008CD3A File Offset: 0x0008AF3A
		// (set) Token: 0x06001DE2 RID: 7650 RVA: 0x0008CD4D File Offset: 0x0008AF4D
		[ConfigurationProperty("useNagleAlgorithm", DefaultValue = true)]
		public bool UseNagleAlgorithm
		{
			get
			{
				return (bool)base[this.useNagleAlgorithm];
			}
			set
			{
				base[this.useNagleAlgorithm] = value;
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06001DE3 RID: 7651 RVA: 0x0008CD61 File Offset: 0x0008AF61
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04001C76 RID: 7286
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001C77 RID: 7287
		private readonly ConfigurationProperty checkCertificateName = new ConfigurationProperty("checkCertificateName", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04001C78 RID: 7288
		private readonly ConfigurationProperty checkCertificateRevocationList = new ConfigurationProperty("checkCertificateRevocationList", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04001C79 RID: 7289
		private readonly ConfigurationProperty dnsRefreshTimeout = new ConfigurationProperty("dnsRefreshTimeout", typeof(int), 120000, null, new TimeoutValidator(true), ConfigurationPropertyOptions.None);

		// Token: 0x04001C7A RID: 7290
		private readonly ConfigurationProperty enableDnsRoundRobin = new ConfigurationProperty("enableDnsRoundRobin", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04001C7B RID: 7291
		private readonly ConfigurationProperty encryptionPolicy = new ConfigurationProperty("encryptionPolicy", typeof(EncryptionPolicy), EncryptionPolicy.RequireEncryption, ConfigurationPropertyOptions.None);

		// Token: 0x04001C7C RID: 7292
		private readonly ConfigurationProperty expect100Continue = new ConfigurationProperty("expect100Continue", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04001C7D RID: 7293
		private readonly ConfigurationProperty useNagleAlgorithm = new ConfigurationProperty("useNagleAlgorithm", typeof(bool), true, ConfigurationPropertyOptions.None);
	}
}
