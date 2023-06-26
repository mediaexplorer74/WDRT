using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Contains a pointer to a bound-to <see cref="T:System.Runtime.InteropServices.FUNCDESC" /> structure, <see cref="T:System.Runtime.InteropServices.VARDESC" /> structure, or an <see langword="ITypeComp" /> interface.</summary>
	// Token: 0x02000A36 RID: 2614
	[__DynamicallyInvokable]
	[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
	public struct BINDPTR
	{
		/// <summary>Represents a pointer to a <see cref="T:System.Runtime.InteropServices.FUNCDESC" /> structure.</summary>
		// Token: 0x04002D67 RID: 11623
		[FieldOffset(0)]
		public IntPtr lpfuncdesc;

		/// <summary>Represents a pointer to a <see cref="T:System.Runtime.InteropServices.VARDESC" /> structure.</summary>
		// Token: 0x04002D68 RID: 11624
		[FieldOffset(0)]
		public IntPtr lpvardesc;

		/// <summary>Represents a pointer to an <see cref="T:System.Runtime.InteropServices.ComTypes.ITypeComp" /> interface.</summary>
		// Token: 0x04002D69 RID: 11625
		[FieldOffset(0)]
		public IntPtr lptcomp;
	}
}
