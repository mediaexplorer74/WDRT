using System;

namespace System.Security
{
	/// <summary>Identifies the source for the security context.</summary>
	// Token: 0x020001EA RID: 490
	public enum SecurityContextSource
	{
		/// <summary>The current application domain is the source for the security context.</summary>
		// Token: 0x04000A52 RID: 2642
		CurrentAppDomain,
		/// <summary>The current assembly is the source for the security context.</summary>
		// Token: 0x04000A53 RID: 2643
		CurrentAssembly
	}
}
