using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Identifies the type description being bound to.</summary>
	// Token: 0x02000A35 RID: 2613
	[__DynamicallyInvokable]
	[Serializable]
	public enum DESCKIND
	{
		/// <summary>Indicates that no match was found.</summary>
		// Token: 0x04002D61 RID: 11617
		[__DynamicallyInvokable]
		DESCKIND_NONE,
		/// <summary>Indicates that a <see cref="T:System.Runtime.InteropServices.FUNCDESC" /> structure was returned.</summary>
		// Token: 0x04002D62 RID: 11618
		[__DynamicallyInvokable]
		DESCKIND_FUNCDESC,
		/// <summary>Indicates that a <see langword="VARDESC" /> was returned.</summary>
		// Token: 0x04002D63 RID: 11619
		[__DynamicallyInvokable]
		DESCKIND_VARDESC,
		/// <summary>Indicates that a <see langword="TYPECOMP" /> was returned.</summary>
		// Token: 0x04002D64 RID: 11620
		[__DynamicallyInvokable]
		DESCKIND_TYPECOMP,
		/// <summary>Indicates that an <see langword="IMPLICITAPPOBJ" /> was returned.</summary>
		// Token: 0x04002D65 RID: 11621
		[__DynamicallyInvokable]
		DESCKIND_IMPLICITAPPOBJ,
		/// <summary>Indicates an end-of-enumeration marker.</summary>
		// Token: 0x04002D66 RID: 11622
		[__DynamicallyInvokable]
		DESCKIND_MAX
	}
}
