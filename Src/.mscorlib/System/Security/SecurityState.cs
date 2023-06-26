using System;
using System.Security.Permissions;

namespace System.Security
{
	/// <summary>Provides a base class for requesting the security status of an action from the <see cref="T:System.AppDomainManager" /> object.</summary>
	// Token: 0x020001F0 RID: 496
	[SecurityCritical]
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public abstract class SecurityState
	{
		/// <summary>Gets a value that indicates whether the state for this implementation of the <see cref="T:System.Security.SecurityState" /> class is available on the current host.</summary>
		/// <returns>
		///   <see langword="true" /> if the state is available; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001E03 RID: 7683 RVA: 0x00068C78 File Offset: 0x00066E78
		[SecurityCritical]
		public bool IsStateAvailable()
		{
			AppDomainManager currentAppDomainManager = AppDomainManager.CurrentAppDomainManager;
			return currentAppDomainManager != null && currentAppDomainManager.CheckSecuritySettings(this);
		}

		/// <summary>When overridden in a derived class, ensures that the state that is represented by <see cref="T:System.Security.SecurityState" /> is available on the host.</summary>
		// Token: 0x06001E04 RID: 7684
		public abstract void EnsureState();
	}
}
