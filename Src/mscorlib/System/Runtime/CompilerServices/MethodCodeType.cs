using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	/// <summary>Defines how a method is implemented.</summary>
	// Token: 0x020008BC RID: 2236
	[ComVisible(true)]
	[Serializable]
	public enum MethodCodeType
	{
		/// <summary>Specifies that the method implementation is in Microsoft intermediate language (MSIL).</summary>
		// Token: 0x04002A2D RID: 10797
		IL,
		/// <summary>Specifies that the method is implemented in native code.</summary>
		// Token: 0x04002A2E RID: 10798
		Native,
		/// <summary>Specifies that the method implementation is in optimized intermediate language (OPTIL).</summary>
		// Token: 0x04002A2F RID: 10799
		OPTIL,
		/// <summary>Specifies that the method implementation is provided by the runtime.</summary>
		// Token: 0x04002A30 RID: 10800
		Runtime
	}
}
