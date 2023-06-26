using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Represents the number of 100-nanosecond intervals since January 1, 1601. This structure is a 64-bit value.</summary>
	// Token: 0x02000A2E RID: 2606
	[__DynamicallyInvokable]
	public struct FILETIME
	{
		/// <summary>Specifies the low 32 bits of the <see langword="FILETIME" />.</summary>
		// Token: 0x04002D53 RID: 11603
		[__DynamicallyInvokable]
		public int dwLowDateTime;

		/// <summary>Specifies the high 32 bits of the <see langword="FILETIME" />.</summary>
		// Token: 0x04002D54 RID: 11604
		[__DynamicallyInvokable]
		public int dwHighDateTime;
	}
}
