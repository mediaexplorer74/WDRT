using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Specifies the preferred default binding for a dependent assembly.</summary>
	// Token: 0x020008C1 RID: 2241
	[Serializable]
	public enum LoadHint
	{
		/// <summary>No preference specified.</summary>
		// Token: 0x04002A35 RID: 10805
		Default,
		/// <summary>The dependency is always loaded.</summary>
		// Token: 0x04002A36 RID: 10806
		Always,
		/// <summary>The dependency is sometimes loaded.</summary>
		// Token: 0x04002A37 RID: 10807
		Sometimes
	}
}
