using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.TYPEKIND" /> instead.</summary>
	// Token: 0x0200098F RID: 2447
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.TYPEKIND instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Serializable]
	public enum TYPEKIND
	{
		/// <summary>A set of enumerators.</summary>
		// Token: 0x04002C07 RID: 11271
		TKIND_ENUM,
		/// <summary>A structure with no methods.</summary>
		// Token: 0x04002C08 RID: 11272
		TKIND_RECORD,
		/// <summary>A module that can only have static functions and data (for example, a DLL).</summary>
		// Token: 0x04002C09 RID: 11273
		TKIND_MODULE,
		/// <summary>A type that has virtual functions, all of which are pure.</summary>
		// Token: 0x04002C0A RID: 11274
		TKIND_INTERFACE,
		/// <summary>A set of methods and properties that are accessible through <see langword="IDispatch::Invoke" />. By default, dual interfaces return <see langword="TKIND_DISPATCH" />.</summary>
		// Token: 0x04002C0B RID: 11275
		TKIND_DISPATCH,
		/// <summary>A set of implemented components interfaces.</summary>
		// Token: 0x04002C0C RID: 11276
		TKIND_COCLASS,
		/// <summary>A type that is an alias for another type.</summary>
		// Token: 0x04002C0D RID: 11277
		TKIND_ALIAS,
		/// <summary>A union of all members that have an offset of zero.</summary>
		// Token: 0x04002C0E RID: 11278
		TKIND_UNION,
		/// <summary>End of enumeration marker.</summary>
		// Token: 0x04002C0F RID: 11279
		TKIND_MAX
	}
}
