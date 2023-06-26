using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Defines identifiers used to indicate the direction of parameter and argument declarations.</summary>
	// Token: 0x02000669 RID: 1641
	[ComVisible(true)]
	[Serializable]
	public enum FieldDirection
	{
		/// <summary>An incoming field.</summary>
		// Token: 0x04002C37 RID: 11319
		In,
		/// <summary>An outgoing field.</summary>
		// Token: 0x04002C38 RID: 11320
		Out,
		/// <summary>A field by reference.</summary>
		// Token: 0x04002C39 RID: 11321
		Ref
	}
}
