using System;

namespace System.Security
{
	/// <summary>Specifies the default partial-trust visibility for code that is marked with the <see cref="T:System.Security.AllowPartiallyTrustedCallersAttribute" /> (APTCA) attribute.</summary>
	// Token: 0x020001C5 RID: 453
	public enum PartialTrustVisibilityLevel
	{
		/// <summary>The assembly can always be called by partial-trust code.</summary>
		// Token: 0x040009C0 RID: 2496
		VisibleToAllHosts,
		/// <summary>The assembly has been audited for partial trust, but it is not visible to partial-trust code in all hosts. To make the assembly visible to partial-trust code, add it to the <see cref="P:System.AppDomainSetup.PartialTrustVisibleAssemblies" /> property.</summary>
		// Token: 0x040009C1 RID: 2497
		NotVisibleByDefault
	}
}
