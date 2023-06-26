using System;

namespace System.ComponentModel
{
	/// <summary>Defines identifiers for types of inheritance levels.</summary>
	// Token: 0x020005BD RID: 1469
	public enum InheritanceLevel
	{
		/// <summary>The object is inherited.</summary>
		// Token: 0x04002AAF RID: 10927
		Inherited = 1,
		/// <summary>The object is inherited, but has read-only access.</summary>
		// Token: 0x04002AB0 RID: 10928
		InheritedReadOnly,
		/// <summary>The object is not inherited.</summary>
		// Token: 0x04002AB1 RID: 10929
		NotInherited
	}
}
