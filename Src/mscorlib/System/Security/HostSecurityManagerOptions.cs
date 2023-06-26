using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	/// <summary>Specifies the security policy components to be used by the host security manager.</summary>
	// Token: 0x020001D8 RID: 472
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum HostSecurityManagerOptions
	{
		/// <summary>Use none of the security policy components.</summary>
		// Token: 0x040009FD RID: 2557
		None = 0,
		/// <summary>Use the application domain evidence.</summary>
		// Token: 0x040009FE RID: 2558
		HostAppDomainEvidence = 1,
		/// <summary>Use the policy level specified in the <see cref="P:System.Security.HostSecurityManager.DomainPolicy" /> property.</summary>
		// Token: 0x040009FF RID: 2559
		[Obsolete("AppDomain policy levels are obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
		HostPolicyLevel = 2,
		/// <summary>Use the assembly evidence.</summary>
		// Token: 0x04000A00 RID: 2560
		HostAssemblyEvidence = 4,
		/// <summary>Route calls to the <see cref="M:System.Security.Policy.ApplicationSecurityManager.DetermineApplicationTrust(System.ActivationContext,System.Security.Policy.TrustManagerContext)" /> method to the <see cref="M:System.Security.HostSecurityManager.DetermineApplicationTrust(System.Security.Policy.Evidence,System.Security.Policy.Evidence,System.Security.Policy.TrustManagerContext)" /> method first.</summary>
		// Token: 0x04000A01 RID: 2561
		HostDetermineApplicationTrust = 8,
		/// <summary>Use the <see cref="M:System.Security.HostSecurityManager.ResolvePolicy(System.Security.Policy.Evidence)" /> method to resolve the application evidence.</summary>
		// Token: 0x04000A02 RID: 2562
		HostResolvePolicy = 16,
		/// <summary>Use all security policy components.</summary>
		// Token: 0x04000A03 RID: 2563
		AllFlags = 31
	}
}
