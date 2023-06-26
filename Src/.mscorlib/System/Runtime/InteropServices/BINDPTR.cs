using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.BINDPTR" /> instead.</summary>
	// Token: 0x0200098D RID: 2445
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.BINDPTR instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
	public struct BINDPTR
	{
		/// <summary>Represents a pointer to a <see cref="T:System.Runtime.InteropServices.FUNCDESC" /> structure.</summary>
		// Token: 0x04002C03 RID: 11267
		[FieldOffset(0)]
		public IntPtr lpfuncdesc;

		/// <summary>Represents a pointer to a <see cref="T:System.Runtime.InteropServices.VARDESC" /> structure.</summary>
		// Token: 0x04002C04 RID: 11268
		[FieldOffset(0)]
		public IntPtr lpvardesc;

		/// <summary>Represents a pointer to a <see cref="F:System.Runtime.InteropServices.BINDPTR.lptcomp" /> interface.</summary>
		// Token: 0x04002C05 RID: 11269
		[FieldOffset(0)]
		public IntPtr lptcomp;
	}
}
