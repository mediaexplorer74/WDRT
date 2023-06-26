using System;
using System.Deployment.Internal.Isolation.Manifest;
using System.Reflection;
using System.Runtime.Hosting;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Policy;

namespace System.Security
{
	/// <summary>Allows the control and customization of security behavior for application domains.</summary>
	// Token: 0x020001D9 RID: 473
	[SecurityCritical]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[Serializable]
	public class HostSecurityManager
	{
		/// <summary>Gets the flag representing the security policy components of concern to the host.</summary>
		/// <returns>One of the enumeration values that specifies security policy components. The default is <see cref="F:System.Security.HostSecurityManagerOptions.AllFlags" />.</returns>
		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06001C9C RID: 7324 RVA: 0x0006209C File Offset: 0x0006029C
		public virtual HostSecurityManagerOptions Flags
		{
			get
			{
				return HostSecurityManagerOptions.AllFlags;
			}
		}

		/// <summary>When overridden in a derived class, gets the security policy for the current application domain.</summary>
		/// <returns>The security policy for the current application domain. The default is <see langword="null" />.</returns>
		/// <exception cref="T:System.NotSupportedException">This method uses code access security (CAS) policy, which is obsolete in the .NET Framework 4. To enable CAS policy for compatibility with earlier versions of the .NET Framework, use the &lt;legacyCasPolicy&gt; element.</exception>
		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06001C9D RID: 7325 RVA: 0x000620A0 File Offset: 0x000602A0
		[Obsolete("AppDomain policy levels are obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		public virtual PolicyLevel DomainPolicy
		{
			get
			{
				if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_RequiresCasPolicyExplicit"));
				}
				return null;
			}
		}

		/// <summary>Provides the application domain evidence for an assembly being loaded.</summary>
		/// <param name="inputEvidence">Additional evidence to add to the <see cref="T:System.AppDomain" /> evidence.</param>
		/// <returns>The evidence to be used for the <see cref="T:System.AppDomain" />.</returns>
		// Token: 0x06001C9E RID: 7326 RVA: 0x000620BF File Offset: 0x000602BF
		public virtual Evidence ProvideAppDomainEvidence(Evidence inputEvidence)
		{
			return inputEvidence;
		}

		/// <summary>Provides the assembly evidence for an assembly being loaded.</summary>
		/// <param name="loadedAssembly">The loaded assembly.</param>
		/// <param name="inputEvidence">Additional evidence to add to the assembly evidence.</param>
		/// <returns>The evidence to be used for the assembly.</returns>
		// Token: 0x06001C9F RID: 7327 RVA: 0x000620C2 File Offset: 0x000602C2
		public virtual Evidence ProvideAssemblyEvidence(Assembly loadedAssembly, Evidence inputEvidence)
		{
			return inputEvidence;
		}

		/// <summary>Determines whether an application should be executed.</summary>
		/// <param name="applicationEvidence">The evidence for the application to be activated.</param>
		/// <param name="activatorEvidence">Optionally, the evidence for the activating application domain.</param>
		/// <param name="context">The trust context.</param>
		/// <returns>An object that contains trust information about the application.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="applicationEvidence" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">An <see cref="T:System.Runtime.Hosting.ActivationArguments" /> object could not be found in the application evidence.  
		///  -or-  
		///  The <see cref="P:System.Runtime.Hosting.ActivationArguments.ActivationContext" /> property in the activation arguments is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Security.Policy.ApplicationTrust" /> grant set does not contain the minimum request set specified by the <see cref="T:System.ActivationContext" />.</exception>
		// Token: 0x06001CA0 RID: 7328 RVA: 0x000620C8 File Offset: 0x000602C8
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
		public virtual ApplicationTrust DetermineApplicationTrust(Evidence applicationEvidence, Evidence activatorEvidence, TrustManagerContext context)
		{
			if (applicationEvidence == null)
			{
				throw new ArgumentNullException("applicationEvidence");
			}
			ActivationArguments hostEvidence = applicationEvidence.GetHostEvidence<ActivationArguments>();
			if (hostEvidence == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Policy_MissingActivationContextInAppEvidence"));
			}
			ActivationContext activationContext = hostEvidence.ActivationContext;
			if (activationContext == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Policy_MissingActivationContextInAppEvidence"));
			}
			ApplicationTrust applicationTrust = applicationEvidence.GetHostEvidence<ApplicationTrust>();
			if (applicationTrust != null && !CmsUtils.CompareIdentities(applicationTrust.ApplicationIdentity, hostEvidence.ApplicationIdentity, ApplicationVersionMatch.MatchExactVersion))
			{
				applicationTrust = null;
			}
			if (applicationTrust == null)
			{
				if (AppDomain.CurrentDomain.ApplicationTrust != null && CmsUtils.CompareIdentities(AppDomain.CurrentDomain.ApplicationTrust.ApplicationIdentity, hostEvidence.ApplicationIdentity, ApplicationVersionMatch.MatchExactVersion))
				{
					applicationTrust = AppDomain.CurrentDomain.ApplicationTrust;
				}
				else
				{
					applicationTrust = ApplicationSecurityManager.DetermineApplicationTrustInternal(activationContext, context);
				}
			}
			ApplicationSecurityInfo applicationSecurityInfo = new ApplicationSecurityInfo(activationContext);
			if (applicationTrust != null && applicationTrust.IsApplicationTrustedToRun && !applicationSecurityInfo.DefaultRequestSet.IsSubsetOf(applicationTrust.DefaultGrantSet.PermissionSet))
			{
				throw new InvalidOperationException(Environment.GetResourceString("Policy_AppTrustMustGrantAppRequest"));
			}
			return applicationTrust;
		}

		/// <summary>Determines what permissions to grant to code based on the specified evidence.</summary>
		/// <param name="evidence">The evidence set used to evaluate policy.</param>
		/// <returns>The permission set that can be granted by the security system.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="evidence" /> is <see langword="null" />.</exception>
		// Token: 0x06001CA1 RID: 7329 RVA: 0x000621B4 File Offset: 0x000603B4
		public virtual PermissionSet ResolvePolicy(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			if (evidence.GetHostEvidence<GacInstalled>() != null)
			{
				return new PermissionSet(PermissionState.Unrestricted);
			}
			if (AppDomain.CurrentDomain.IsHomogenous)
			{
				return AppDomain.CurrentDomain.GetHomogenousGrantSet(evidence);
			}
			if (!AppDomain.CurrentDomain.IsLegacyCasPolicyEnabled)
			{
				return new PermissionSet(PermissionState.Unrestricted);
			}
			return SecurityManager.PolicyManager.CodeGroupResolve(evidence, false);
		}

		/// <summary>Determines which evidence types the host can supply for the application domain, if requested.</summary>
		/// <returns>An array of evidence types.</returns>
		// Token: 0x06001CA2 RID: 7330 RVA: 0x00062215 File Offset: 0x00060415
		public virtual Type[] GetHostSuppliedAppDomainEvidenceTypes()
		{
			return null;
		}

		/// <summary>Determines which evidence types the host can supply for the assembly, if requested.</summary>
		/// <param name="assembly">The target assembly.</param>
		/// <returns>An array of evidence types.</returns>
		// Token: 0x06001CA3 RID: 7331 RVA: 0x00062218 File Offset: 0x00060418
		public virtual Type[] GetHostSuppliedAssemblyEvidenceTypes(Assembly assembly)
		{
			return null;
		}

		/// <summary>Requests a specific evidence type for the application domain.</summary>
		/// <param name="evidenceType">The evidence type.</param>
		/// <returns>The requested application domain evidence.</returns>
		// Token: 0x06001CA4 RID: 7332 RVA: 0x0006221B File Offset: 0x0006041B
		public virtual EvidenceBase GenerateAppDomainEvidence(Type evidenceType)
		{
			return null;
		}

		/// <summary>Requests a specific evidence type for the assembly.</summary>
		/// <param name="evidenceType">The evidence type.</param>
		/// <param name="assembly">The target assembly.</param>
		/// <returns>The requested assembly evidence.</returns>
		// Token: 0x06001CA5 RID: 7333 RVA: 0x0006221E File Offset: 0x0006041E
		public virtual EvidenceBase GenerateAssemblyEvidence(Type evidenceType, Assembly assembly)
		{
			return null;
		}
	}
}
