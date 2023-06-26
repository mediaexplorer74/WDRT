using System;
using System.Deployment.Internal.Isolation.Manifest;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Policy;

namespace System.Runtime.Hosting
{
	/// <summary>Provides the base class for the activation of manifest-based assemblies.</summary>
	// Token: 0x02000A54 RID: 2644
	[ComVisible(true)]
	public class ApplicationActivator
	{
		/// <summary>Creates an instance of the application to be activated, using the specified activation context.</summary>
		/// <param name="activationContext">An <see cref="T:System.ActivationContext" /> that identifies the application to activate.</param>
		/// <returns>An <see cref="T:System.Runtime.Remoting.ObjectHandle" /> that is a wrapper for the return value of the application execution. The return value must be unwrapped to access the real object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="activationContext" /> is <see langword="null" />.</exception>
		// Token: 0x060066DC RID: 26332 RVA: 0x0015B630 File Offset: 0x00159830
		public virtual ObjectHandle CreateInstance(ActivationContext activationContext)
		{
			return this.CreateInstance(activationContext, null);
		}

		/// <summary>Creates an instance of the application to be activated, using the specified activation context  and custom activation data.</summary>
		/// <param name="activationContext">An <see cref="T:System.ActivationContext" /> that identifies the application to activate.</param>
		/// <param name="activationCustomData">Custom activation data.</param>
		/// <returns>An <see cref="T:System.Runtime.Remoting.ObjectHandle" /> that is a wrapper for the return value of the application execution. The return value must be unwrapped to access the real object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="activationContext" /> is <see langword="null" />.</exception>
		// Token: 0x060066DD RID: 26333 RVA: 0x0015B63C File Offset: 0x0015983C
		[SecuritySafeCritical]
		public virtual ObjectHandle CreateInstance(ActivationContext activationContext, string[] activationCustomData)
		{
			if (activationContext == null)
			{
				throw new ArgumentNullException("activationContext");
			}
			if (CmsUtils.CompareIdentities(AppDomain.CurrentDomain.ActivationContext, activationContext))
			{
				ManifestRunner manifestRunner = new ManifestRunner(AppDomain.CurrentDomain, activationContext);
				return new ObjectHandle(manifestRunner.ExecuteAsAssembly());
			}
			AppDomainSetup appDomainSetup = new AppDomainSetup(new ActivationArguments(activationContext, activationCustomData));
			AppDomainSetup setupInformation = AppDomain.CurrentDomain.SetupInformation;
			appDomainSetup.AppDomainManagerType = setupInformation.AppDomainManagerType;
			appDomainSetup.AppDomainManagerAssembly = setupInformation.AppDomainManagerAssembly;
			return ApplicationActivator.CreateInstanceHelper(appDomainSetup);
		}

		/// <summary>Creates an instance of an application using the specified <see cref="T:System.AppDomainSetup" /> object.</summary>
		/// <param name="adSetup">An <see cref="T:System.AppDomainSetup" /> object whose <see cref="P:System.AppDomainSetup.ActivationArguments" /> property identifies the application to activate.</param>
		/// <returns>An <see cref="T:System.Runtime.Remoting.ObjectHandle" /> that is a wrapper for the return value of the application execution. The return value must be unwrapped to access the real object.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.AppDomainSetup.ActivationArguments" /> property of <paramref name="adSetup" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Policy.PolicyException">The application instance failed to execute because the policy settings on the current application domain do not provide permission for this application to run.</exception>
		// Token: 0x060066DE RID: 26334 RVA: 0x0015B6BC File Offset: 0x001598BC
		[SecuritySafeCritical]
		protected static ObjectHandle CreateInstanceHelper(AppDomainSetup adSetup)
		{
			if (adSetup.ActivationArguments == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MissingActivationArguments"));
			}
			adSetup.ActivationArguments.ActivateInstance = true;
			Evidence evidence = AppDomain.CurrentDomain.Evidence;
			Evidence evidence2 = CmsUtils.MergeApplicationEvidence(null, adSetup.ActivationArguments.ApplicationIdentity, adSetup.ActivationArguments.ActivationContext, adSetup.ActivationArguments.ActivationData);
			HostSecurityManager hostSecurityManager = AppDomain.CurrentDomain.HostSecurityManager;
			ApplicationTrust applicationTrust = hostSecurityManager.DetermineApplicationTrust(evidence2, evidence, new TrustManagerContext());
			if (applicationTrust == null || !applicationTrust.IsApplicationTrustedToRun)
			{
				throw new PolicyException(Environment.GetResourceString("Policy_NoExecutionPermission"), -2146233320, null);
			}
			ObjRef objRef = AppDomain.nCreateInstance(adSetup.ActivationArguments.ApplicationIdentity.FullName, adSetup, evidence2, (evidence2 == null) ? AppDomain.CurrentDomain.InternalEvidence : null, AppDomain.CurrentDomain.GetSecurityDescriptor());
			if (objRef == null)
			{
				return null;
			}
			return RemotingServices.Unmarshal(objRef) as ObjectHandle;
		}
	}
}
