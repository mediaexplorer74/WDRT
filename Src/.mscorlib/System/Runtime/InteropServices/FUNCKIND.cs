using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.FUNCKIND" /> instead.</summary>
	// Token: 0x0200099D RID: 2461
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.FUNCKIND instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Serializable]
	public enum FUNCKIND
	{
		/// <summary>The function is accessed the same as <see cref="F:System.Runtime.InteropServices.FUNCKIND.FUNC_PUREVIRTUAL" />, except the function has an implementation.</summary>
		// Token: 0x04002C6D RID: 11373
		FUNC_VIRTUAL,
		/// <summary>The function is accessed through the virtual function table (VTBL), and takes an implicit <see langword="this" /> pointer.</summary>
		// Token: 0x04002C6E RID: 11374
		FUNC_PUREVIRTUAL,
		/// <summary>The function is accessed by <see langword="static" /> address and takes an implicit <see langword="this" /> pointer.</summary>
		// Token: 0x04002C6F RID: 11375
		FUNC_NONVIRTUAL,
		/// <summary>The function is accessed by <see langword="static" /> address and does not take an implicit <see langword="this" /> pointer.</summary>
		// Token: 0x04002C70 RID: 11376
		FUNC_STATIC,
		/// <summary>The function can be accessed only through <see langword="IDispatch" />.</summary>
		// Token: 0x04002C71 RID: 11377
		FUNC_DISPATCH
	}
}
