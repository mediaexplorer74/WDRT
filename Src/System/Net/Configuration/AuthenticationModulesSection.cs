using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the configuration section for authentication modules. This class cannot be inherited.</summary>
	// Token: 0x02000326 RID: 806
	public sealed class AuthenticationModulesSection : ConfigurationSection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.AuthenticationModulesSection" /> class.</summary>
		// Token: 0x06001CE1 RID: 7393 RVA: 0x0008A4A0 File Offset: 0x000886A0
		public AuthenticationModulesSection()
		{
			this.properties.Add(this.authenticationModules);
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x0008A4DC File Offset: 0x000886DC
		protected override void PostDeserialize()
		{
			if (base.EvaluationContext.IsMachineLevel)
			{
				return;
			}
			try
			{
				ExceptionHelper.UnmanagedPermission.Demand();
			}
			catch (Exception ex)
			{
				throw new ConfigurationErrorsException(SR.GetString("net_config_section_permission", new object[] { "authenticationModules" }), ex);
			}
		}

		/// <summary>Gets the collection of authentication modules in the section.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.AuthenticationModuleElementCollection" /> that contains the registered authentication modules.</returns>
		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06001CE3 RID: 7395 RVA: 0x0008A534 File Offset: 0x00088734
		[ConfigurationProperty("", IsDefaultCollection = true)]
		public AuthenticationModuleElementCollection AuthenticationModules
		{
			get
			{
				return (AuthenticationModuleElementCollection)base[this.authenticationModules];
			}
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x0008A548 File Offset: 0x00088748
		protected override void InitializeDefault()
		{
			this.AuthenticationModules.Add(new AuthenticationModuleElement(typeof(NegotiateClient).AssemblyQualifiedName));
			this.AuthenticationModules.Add(new AuthenticationModuleElement(typeof(KerberosClient).AssemblyQualifiedName));
			this.AuthenticationModules.Add(new AuthenticationModuleElement(typeof(NtlmClient).AssemblyQualifiedName));
			this.AuthenticationModules.Add(new AuthenticationModuleElement(typeof(DigestClient).AssemblyQualifiedName));
			this.AuthenticationModules.Add(new AuthenticationModuleElement(typeof(BasicClient).AssemblyQualifiedName));
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06001CE5 RID: 7397 RVA: 0x0008A5F0 File Offset: 0x000887F0
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04001BAE RID: 7086
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001BAF RID: 7087
		private readonly ConfigurationProperty authenticationModules = new ConfigurationProperty(null, typeof(AuthenticationModuleElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
