using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Specifies whether white space should be ignored in the base 64 transformation.</summary>
	// Token: 0x0200024E RID: 590
	[ComVisible(true)]
	[Serializable]
	public enum FromBase64TransformMode
	{
		/// <summary>White space should be ignored.</summary>
		// Token: 0x04000BEB RID: 3051
		IgnoreWhiteSpaces,
		/// <summary>White space should not be ignored.</summary>
		// Token: 0x04000BEC RID: 3052
		DoNotIgnoreWhiteSpaces
	}
}
