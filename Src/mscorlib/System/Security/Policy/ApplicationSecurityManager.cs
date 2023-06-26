using System;
using System.Deployment.Internal.Isolation.Manifest;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Security.Policy
{
	/// <summary>Manages trust decisions for manifest-activated applications.</summary>
	// Token: 0x02000342 RID: 834
	[ComVisible(true)]
	public static class ApplicationSecurityManager
	{
		/// <summary>Determines whether the user approves the specified application to execute with the requested permission set.</summary>
		/// <param name="activationContext">An <see cref="T:System.ActivationContext" /> identifying the activation context for the application.</param>
		/// <param name="context">A <see cref="T:System.Security.Policy.TrustManagerContext" /> identifying the trust manager context for the application.</param>
		/// <returns>
		///   <see langword="true" /> to execute the specified application; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="activationContext" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060029A4 RID: 10660 RVA: 0x0009AE64 File Offset: 0x00099064
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
		public static bool DetermineApplicationTrust(ActivationContext activationContext, TrustManagerContext context)
		{
			if (activationContext == null)
			{
				throw new ArgumentNullException("activationContext");
			}
			AppDomainManager domainManager = AppDomain.CurrentDomain.DomainManager;
			ApplicationTrust applicationTrust;
			if (domainManager != null)
			{
				HostSecurityManager hostSecurityManager = domainManager.HostSecurityManager;
				if (hostSecurityManager != null && (hostSecurityManager.Flags & HostSecurityManagerOptions.HostDetermineApplicationTrust) == HostSecurityManagerOptions.HostDetermineApplicationTrust)
				{
					applicationTrust = hostSecurityManager.DetermineApplicationTrust(CmsUtils.MergeApplicationEvidence(null, activationContext.Identity, activationContext, null), null, context);
					return applicationTrust != null && applicationTrust.IsApplicationTrustedToRun;
				}
			}
			applicationTrust = ApplicationSecurityManager.DetermineApplicationTrustInternal(activationContext, context);
			return applicationTrust != null && applicationTrust.IsApplicationTrustedToRun;
		}

		/// <summary>Gets an application trust collection that contains the cached trust decisions for the user.</summary>
		/// <returns>An <see cref="T:System.Security.Policy.ApplicationTrustCollection" /> that contains the cached trust decisions for the user.</returns>
		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x060029A5 RID: 10661 RVA: 0x0009AEDA File Offset: 0x000990DA
		public static ApplicationTrustCollection UserApplicationTrusts
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)]
			get
			{
				return new ApplicationTrustCollection(true);
			}
		}

		/// <summary>Gets the current application trust manager.</summary>
		/// <returns>An <see cref="T:System.Security.Policy.IApplicationTrustManager" /> that represents the current trust manager.</returns>
		/// <exception cref="T:System.Security.Policy.PolicyException">The policy on this application does not have a trust manager.</exception>
		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x060029A6 RID: 10662 RVA: 0x0009AEE2 File Offset: 0x000990E2
		public static IApplicationTrustManager ApplicationTrustManager
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)]
			get
			{
				if (ApplicationSecurityManager.m_appTrustManager == null)
				{
					ApplicationSecurityManager.m_appTrustManager = ApplicationSecurityManager.DecodeAppTrustManager();
					if (ApplicationSecurityManager.m_appTrustManager == null)
					{
						throw new PolicyException(Environment.GetResourceString("Policy_NoTrustManager"));
					}
				}
				return ApplicationSecurityManager.m_appTrustManager;
			}
		}

		// Token: 0x060029A7 RID: 10663 RVA: 0x0009AF1C File Offset: 0x0009911C
		[SecurityCritical]
		internal static ApplicationTrust DetermineApplicationTrustInternal(ActivationContext activationContext, TrustManagerContext context)
		{
			ApplicationTrustCollection applicationTrustCollection = new ApplicationTrustCollection(true);
			ApplicationTrust applicationTrust;
			if (context == null || !context.IgnorePersistedDecision)
			{
				applicationTrust = applicationTrustCollection[activationContext.Identity.FullName];
				if (applicationTrust != null)
				{
					return applicationTrust;
				}
			}
			applicationTrust = ApplicationSecurityManager.ApplicationTrustManager.DetermineApplicationTrust(activationContext, context);
			if (applicationTrust == null)
			{
				applicationTrust = new ApplicationTrust(activationContext.Identity);
			}
			applicationTrust.ApplicationIdentity = activationContext.Identity;
			if (applicationTrust.Persist)
			{
				applicationTrustCollection.Add(applicationTrust);
			}
			return applicationTrust;
		}

		// Token: 0x060029A8 RID: 10664 RVA: 0x0009AF90 File Offset: 0x00099190
		[SecurityCritical]
		private static IApplicationTrustManager DecodeAppTrustManager()
		{
			if (File.InternalExists(ApplicationSecurityManager.s_machineConfigFile))
			{
				string text;
				using (FileStream fileStream = new FileStream(ApplicationSecurityManager.s_machineConfigFile, FileMode.Open, FileAccess.Read))
				{
					text = new StreamReader(fileStream).ReadToEnd();
				}
				SecurityElement securityElement = SecurityElement.FromString(text);
				SecurityElement securityElement2 = securityElement.SearchForChildByTag("mscorlib");
				if (securityElement2 != null)
				{
					SecurityElement securityElement3 = securityElement2.SearchForChildByTag("security");
					if (securityElement3 != null)
					{
						SecurityElement securityElement4 = securityElement3.SearchForChildByTag("policy");
						if (securityElement4 != null)
						{
							SecurityElement securityElement5 = securityElement4.SearchForChildByTag("ApplicationSecurityManager");
							if (securityElement5 != null)
							{
								SecurityElement securityElement6 = securityElement5.SearchForChildByTag("IApplicationTrustManager");
								if (securityElement6 != null)
								{
									IApplicationTrustManager applicationTrustManager = ApplicationSecurityManager.DecodeAppTrustManagerFromElement(securityElement6);
									if (applicationTrustManager != null)
									{
										return applicationTrustManager;
									}
								}
							}
						}
					}
				}
			}
			return ApplicationSecurityManager.DecodeAppTrustManagerFromElement(ApplicationSecurityManager.CreateDefaultApplicationTrustManagerElement());
		}

		// Token: 0x060029A9 RID: 10665 RVA: 0x0009B05C File Offset: 0x0009925C
		[SecurityCritical]
		private static SecurityElement CreateDefaultApplicationTrustManagerElement()
		{
			SecurityElement securityElement = new SecurityElement("IApplicationTrustManager");
			SecurityElement securityElement2 = securityElement;
			string text = "class";
			string text2 = "System.Security.Policy.TrustManager, System.Windows.Forms, Version=";
			Version version = ((RuntimeAssembly)Assembly.GetExecutingAssembly()).GetVersion();
			securityElement2.AddAttribute(text, text2 + ((version != null) ? version.ToString() : null) + ", Culture=neutral, PublicKeyToken=b77a5c561934e089");
			securityElement.AddAttribute("version", "1");
			return securityElement;
		}

		// Token: 0x060029AA RID: 10666 RVA: 0x0009B0BC File Offset: 0x000992BC
		[SecurityCritical]
		private static IApplicationTrustManager DecodeAppTrustManagerFromElement(SecurityElement elTrustManager)
		{
			new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Assert();
			string text = elTrustManager.Attribute("class");
			Type type = Type.GetType(text, false, false);
			if (type == null)
			{
				return null;
			}
			IApplicationTrustManager applicationTrustManager = Activator.CreateInstance(type) as IApplicationTrustManager;
			if (applicationTrustManager != null)
			{
				applicationTrustManager.FromXml(elTrustManager);
			}
			return applicationTrustManager;
		}

		// Token: 0x04001117 RID: 4375
		private static volatile IApplicationTrustManager m_appTrustManager = null;

		// Token: 0x04001118 RID: 4376
		private static string s_machineConfigFile = Config.MachineDirectory + "applicationtrust.config";
	}
}
