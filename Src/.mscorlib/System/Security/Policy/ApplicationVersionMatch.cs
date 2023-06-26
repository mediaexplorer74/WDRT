using System;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	/// <summary>Specifies how to match versions when locating application trusts in a collection.</summary>
	// Token: 0x02000343 RID: 835
	[ComVisible(true)]
	public enum ApplicationVersionMatch
	{
		/// <summary>Match on the exact version.</summary>
		// Token: 0x0400111A RID: 4378
		MatchExactVersion,
		/// <summary>Match on all versions.</summary>
		// Token: 0x0400111B RID: 4379
		MatchAllVersions
	}
}
