using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Specifies the culture, case, and sort rules to be used by certain overloads of the <see cref="M:System.String.Compare(System.String,System.String)" /> and <see cref="M:System.String.Equals(System.Object)" /> methods.</summary>
	// Token: 0x0200007B RID: 123
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum StringComparison
	{
		/// <summary>Compare strings using culture-sensitive sort rules and the current culture.</summary>
		// Token: 0x0400029D RID: 669
		[__DynamicallyInvokable]
		CurrentCulture,
		/// <summary>Compare strings using culture-sensitive sort rules, the current culture, and ignoring the case of the strings being compared.</summary>
		// Token: 0x0400029E RID: 670
		[__DynamicallyInvokable]
		CurrentCultureIgnoreCase,
		/// <summary>Compare strings using culture-sensitive sort rules and the invariant culture.</summary>
		// Token: 0x0400029F RID: 671
		InvariantCulture,
		/// <summary>Compare strings using culture-sensitive sort rules, the invariant culture, and ignoring the case of the strings being compared.</summary>
		// Token: 0x040002A0 RID: 672
		InvariantCultureIgnoreCase,
		/// <summary>Compare strings using ordinal (binary) sort rules.</summary>
		// Token: 0x040002A1 RID: 673
		[__DynamicallyInvokable]
		Ordinal,
		/// <summary>Compare strings using ordinal (binary) sort rules and ignoring the case of the strings being compared.</summary>
		// Token: 0x040002A2 RID: 674
		[__DynamicallyInvokable]
		OrdinalIgnoreCase
	}
}
