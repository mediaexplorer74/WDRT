using System;
using System.Configuration;
using System.Net.Mail;

namespace System.Net.Configuration
{
	/// <summary>Represents the network element in the SMTP configuration file. This class cannot be inherited.</summary>
	// Token: 0x02000344 RID: 836
	public sealed class SmtpNetworkElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.SmtpNetworkElement" /> class.</summary>
		// Token: 0x06001DF6 RID: 7670 RVA: 0x0008D018 File Offset: 0x0008B218
		public SmtpNetworkElement()
		{
			this.properties.Add(this.defaultCredentials);
			this.properties.Add(this.host);
			this.properties.Add(this.clientDomain);
			this.properties.Add(this.password);
			this.properties.Add(this.port);
			this.properties.Add(this.userName);
			this.properties.Add(this.targetName);
			this.properties.Add(this.enableSsl);
		}

		// Token: 0x06001DF7 RID: 7671 RVA: 0x0008D1BC File Offset: 0x0008B3BC
		protected override void PostDeserialize()
		{
			if (base.EvaluationContext.IsMachineLevel)
			{
				return;
			}
			PropertyInformation propertyInformation = base.ElementInformation.Properties["port"];
			if (propertyInformation.ValueOrigin == PropertyValueOrigin.SetHere && (int)propertyInformation.Value != (int)propertyInformation.DefaultValue)
			{
				try
				{
					new SmtpPermission(SmtpAccess.ConnectToUnrestrictedPort).Demand();
				}
				catch (Exception ex)
				{
					throw new ConfigurationErrorsException(SR.GetString("net_config_property_permission", new object[] { propertyInformation.Name }), ex);
				}
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06001DF8 RID: 7672 RVA: 0x0008D24C File Offset: 0x0008B44C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		/// <summary>Determines whether or not default user credentials are used to access an SMTP server. The default value is <see langword="false" />.</summary>
		/// <returns>
		///   <see langword="true" /> indicates that default user credentials will be used to access the SMTP server; otherwise, <see langword="false" />.</returns>
		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06001DF9 RID: 7673 RVA: 0x0008D254 File Offset: 0x0008B454
		// (set) Token: 0x06001DFA RID: 7674 RVA: 0x0008D267 File Offset: 0x0008B467
		[ConfigurationProperty("defaultCredentials", DefaultValue = false)]
		public bool DefaultCredentials
		{
			get
			{
				return (bool)base[this.defaultCredentials];
			}
			set
			{
				base[this.defaultCredentials] = value;
			}
		}

		/// <summary>Gets or sets the name of the SMTP server.</summary>
		/// <returns>A string that represents the name of the SMTP server to connect to.</returns>
		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06001DFB RID: 7675 RVA: 0x0008D27B File Offset: 0x0008B47B
		// (set) Token: 0x06001DFC RID: 7676 RVA: 0x0008D28E File Offset: 0x0008B48E
		[ConfigurationProperty("host")]
		public string Host
		{
			get
			{
				return (string)base[this.host];
			}
			set
			{
				base[this.host] = value;
			}
		}

		/// <summary>Gets or sets the Service Provider Name (SPN) to use for authentication when using extended protection to connect to an SMTP mail server.</summary>
		/// <returns>A string that represents the SPN to use for authentication when using extended protection to connect to an SMTP mail server.</returns>
		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06001DFD RID: 7677 RVA: 0x0008D29D File Offset: 0x0008B49D
		// (set) Token: 0x06001DFE RID: 7678 RVA: 0x0008D2B0 File Offset: 0x0008B4B0
		[ConfigurationProperty("targetName")]
		public string TargetName
		{
			get
			{
				return (string)base[this.targetName];
			}
			set
			{
				base[this.targetName] = value;
			}
		}

		/// <summary>Gets or sets the client domain name used in the initial SMTP protocol request to connect to an SMTP mail server.</summary>
		/// <returns>A string that represents the client domain name used in the initial SMTP protocol request to connect to an SMTP mail server.</returns>
		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06001DFF RID: 7679 RVA: 0x0008D2BF File Offset: 0x0008B4BF
		// (set) Token: 0x06001E00 RID: 7680 RVA: 0x0008D2D2 File Offset: 0x0008B4D2
		[ConfigurationProperty("clientDomain")]
		public string ClientDomain
		{
			get
			{
				return (string)base[this.clientDomain];
			}
			set
			{
				base[this.clientDomain] = value;
			}
		}

		/// <summary>Gets or sets the user password to use to connect to an SMTP mail server.</summary>
		/// <returns>A string that represents the password to use to connect to an SMTP mail server.</returns>
		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06001E01 RID: 7681 RVA: 0x0008D2E1 File Offset: 0x0008B4E1
		// (set) Token: 0x06001E02 RID: 7682 RVA: 0x0008D2F4 File Offset: 0x0008B4F4
		[ConfigurationProperty("password")]
		public string Password
		{
			get
			{
				return (string)base[this.password];
			}
			set
			{
				base[this.password] = value;
			}
		}

		/// <summary>Gets or sets the port that SMTP clients use to connect to an SMTP mail server. The default value is 25.</summary>
		/// <returns>A string that represents the port to connect to an SMTP mail server.</returns>
		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06001E03 RID: 7683 RVA: 0x0008D303 File Offset: 0x0008B503
		// (set) Token: 0x06001E04 RID: 7684 RVA: 0x0008D316 File Offset: 0x0008B516
		[ConfigurationProperty("port", DefaultValue = 25)]
		public int Port
		{
			get
			{
				return (int)base[this.port];
			}
			set
			{
				base[this.port] = value;
			}
		}

		/// <summary>Gets or sets the user name to connect to an SMTP mail server.</summary>
		/// <returns>A string that represents the user name to connect to an SMTP mail server.</returns>
		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06001E05 RID: 7685 RVA: 0x0008D32A File Offset: 0x0008B52A
		// (set) Token: 0x06001E06 RID: 7686 RVA: 0x0008D33D File Offset: 0x0008B53D
		[ConfigurationProperty("userName")]
		public string UserName
		{
			get
			{
				return (string)base[this.userName];
			}
			set
			{
				base[this.userName] = value;
			}
		}

		/// <summary>Gets or sets whether SSL is used to access an SMTP mail server. The default value is <see langword="false" />.</summary>
		/// <returns>
		///   <see langword="true" /> indicates that SSL will be used to access the SMTP mail server; otherwise, <see langword="false" />.</returns>
		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06001E07 RID: 7687 RVA: 0x0008D34C File Offset: 0x0008B54C
		// (set) Token: 0x06001E08 RID: 7688 RVA: 0x0008D35F File Offset: 0x0008B55F
		[ConfigurationProperty("enableSsl", DefaultValue = false)]
		public bool EnableSsl
		{
			get
			{
				return (bool)base[this.enableSsl];
			}
			set
			{
				base[this.enableSsl] = value;
			}
		}

		// Token: 0x04001C8A RID: 7306
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001C8B RID: 7307
		private readonly ConfigurationProperty defaultCredentials = new ConfigurationProperty("defaultCredentials", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04001C8C RID: 7308
		private readonly ConfigurationProperty host = new ConfigurationProperty("host", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C8D RID: 7309
		private readonly ConfigurationProperty clientDomain = new ConfigurationProperty("clientDomain", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C8E RID: 7310
		private readonly ConfigurationProperty password = new ConfigurationProperty("password", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C8F RID: 7311
		private readonly ConfigurationProperty port = new ConfigurationProperty("port", typeof(int), 25, null, new IntegerValidator(1, 65535), ConfigurationPropertyOptions.None);

		// Token: 0x04001C90 RID: 7312
		private readonly ConfigurationProperty userName = new ConfigurationProperty("userName", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C91 RID: 7313
		private readonly ConfigurationProperty targetName = new ConfigurationProperty("targetName", typeof(string), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C92 RID: 7314
		private readonly ConfigurationProperty enableSsl = new ConfigurationProperty("enableSsl", typeof(bool), false, ConfigurationPropertyOptions.None);
	}
}
