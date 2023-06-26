using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Specifies how the code type reference is to be resolved.</summary>
	// Token: 0x02000663 RID: 1635
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum CodeTypeReferenceOptions
	{
		/// <summary>Resolve the type from the root namespace.</summary>
		// Token: 0x04002C28 RID: 11304
		GlobalReference = 1,
		/// <summary>Resolve the type from the type parameter.</summary>
		// Token: 0x04002C29 RID: 11305
		GenericTypeParameter = 2
	}
}
