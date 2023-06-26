using System;
using System.Collections.Generic;
using System.Configuration;

namespace System.Security.Authentication.ExtendedProtection.Configuration
{
	/// <summary>The <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ExtendedProtectionPolicyElement" /> class represents a configuration element for an <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" />.</summary>
	// Token: 0x0200044A RID: 1098
	public sealed class ExtendedProtectionPolicyElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ExtendedProtectionPolicyElement" /> class.</summary>
		// Token: 0x06002893 RID: 10387 RVA: 0x000BA4C8 File Offset: 0x000B86C8
		public ExtendedProtectionPolicyElement()
		{
			this.properties.Add(this.policyEnforcement);
			this.properties.Add(this.protectionScenario);
			this.properties.Add(this.customServiceNames);
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06002894 RID: 10388 RVA: 0x000BA57B File Offset: 0x000B877B
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		/// <summary>Gets or sets the policy enforcement value for this configuration policy element.</summary>
		/// <returns>One of the enumeration values that indicates when the extended protection policy should be enforced.</returns>
		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06002895 RID: 10389 RVA: 0x000BA583 File Offset: 0x000B8783
		// (set) Token: 0x06002896 RID: 10390 RVA: 0x000BA596 File Offset: 0x000B8796
		[ConfigurationProperty("policyEnforcement")]
		public PolicyEnforcement PolicyEnforcement
		{
			get
			{
				return (PolicyEnforcement)base[this.policyEnforcement];
			}
			set
			{
				base[this.policyEnforcement] = value;
			}
		}

		/// <summary>Gets or sets the kind of protection enforced by the extended protection policy for this configuration policy element.</summary>
		/// <returns>A <see cref="T:System.Security.Authentication.ExtendedProtection.ProtectionScenario" /> value that indicates the kind of protection enforced by the policy.</returns>
		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06002897 RID: 10391 RVA: 0x000BA5AA File Offset: 0x000B87AA
		// (set) Token: 0x06002898 RID: 10392 RVA: 0x000BA5BD File Offset: 0x000B87BD
		[ConfigurationProperty("protectionScenario", DefaultValue = ProtectionScenario.TransportSelected)]
		public ProtectionScenario ProtectionScenario
		{
			get
			{
				return (ProtectionScenario)base[this.protectionScenario];
			}
			set
			{
				base[this.protectionScenario] = value;
			}
		}

		/// <summary>Gets or sets the custom Service Provider Name (SPN) list used to match against a client's SPN for this configuration policy element.</summary>
		/// <returns>A collection that includes the custom SPN list used to match against a client's SPN.</returns>
		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06002899 RID: 10393 RVA: 0x000BA5D1 File Offset: 0x000B87D1
		[ConfigurationProperty("customServiceNames")]
		public ServiceNameElementCollection CustomServiceNames
		{
			get
			{
				return (ServiceNameElementCollection)base[this.customServiceNames];
			}
		}

		/// <summary>The <see cref="M:System.Security.Authentication.ExtendedProtection.Configuration.ExtendedProtectionPolicyElement.BuildPolicy" /> method builds a new <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> instance based on the properties set on the <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ExtendedProtectionPolicyElement" /> class.</summary>
		/// <returns>A new <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> instance that represents the extended protection policy created.</returns>
		// Token: 0x0600289A RID: 10394 RVA: 0x000BA5E4 File Offset: 0x000B87E4
		public ExtendedProtectionPolicy BuildPolicy()
		{
			if (this.PolicyEnforcement == PolicyEnforcement.Never)
			{
				return new ExtendedProtectionPolicy(PolicyEnforcement.Never);
			}
			ServiceNameCollection serviceNameCollection = null;
			ServiceNameElementCollection serviceNameElementCollection = this.CustomServiceNames;
			if (serviceNameElementCollection != null && serviceNameElementCollection.Count > 0)
			{
				List<string> list = new List<string>(serviceNameElementCollection.Count);
				foreach (object obj in serviceNameElementCollection)
				{
					ServiceNameElement serviceNameElement = (ServiceNameElement)obj;
					list.Add(serviceNameElement.Name);
				}
				serviceNameCollection = new ServiceNameCollection(list);
			}
			return new ExtendedProtectionPolicy(this.PolicyEnforcement, this.ProtectionScenario, serviceNameCollection);
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x0600289B RID: 10395 RVA: 0x000BA68C File Offset: 0x000B888C
		private static PolicyEnforcement DefaultPolicyEnforcement
		{
			get
			{
				return PolicyEnforcement.Never;
			}
		}

		// Token: 0x04002261 RID: 8801
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002262 RID: 8802
		private readonly ConfigurationProperty policyEnforcement = new ConfigurationProperty("policyEnforcement", typeof(PolicyEnforcement), ExtendedProtectionPolicyElement.DefaultPolicyEnforcement, ConfigurationPropertyOptions.None);

		// Token: 0x04002263 RID: 8803
		private readonly ConfigurationProperty protectionScenario = new ConfigurationProperty("protectionScenario", typeof(ProtectionScenario), ProtectionScenario.TransportSelected, ConfigurationPropertyOptions.None);

		// Token: 0x04002264 RID: 8804
		private readonly ConfigurationProperty customServiceNames = new ConfigurationProperty("customServiceNames", typeof(ServiceNameElementCollection), null, ConfigurationPropertyOptions.None);
	}
}
