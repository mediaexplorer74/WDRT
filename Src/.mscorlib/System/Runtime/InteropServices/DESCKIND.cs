using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.DESCKIND" /> instead.</summary>
	// Token: 0x0200098C RID: 2444
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.DESCKIND instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Serializable]
	public enum DESCKIND
	{
		/// <summary>Indicates that no match was found.</summary>
		// Token: 0x04002BFD RID: 11261
		DESCKIND_NONE,
		/// <summary>Indicates that a <see cref="T:System.Runtime.InteropServices.FUNCDESC" /> was returned.</summary>
		// Token: 0x04002BFE RID: 11262
		DESCKIND_FUNCDESC,
		/// <summary>Indicates that a <see langword="VARDESC" /> was returned.</summary>
		// Token: 0x04002BFF RID: 11263
		DESCKIND_VARDESC,
		/// <summary>Indicates that a <see langword="TYPECOMP" /> was returned.</summary>
		// Token: 0x04002C00 RID: 11264
		DESCKIND_TYPECOMP,
		/// <summary>Indicates that an <see langword="IMPLICITAPPOBJ" /> was returned.</summary>
		// Token: 0x04002C01 RID: 11265
		DESCKIND_IMPLICITAPPOBJ,
		/// <summary>Indicates an end of enumeration marker.</summary>
		// Token: 0x04002C02 RID: 11266
		DESCKIND_MAX
	}
}
