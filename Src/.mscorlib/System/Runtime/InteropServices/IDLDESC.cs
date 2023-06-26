using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.IDLDESC" /> instead.</summary>
	// Token: 0x02000995 RID: 2453
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IDLDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct IDLDESC
	{
		/// <summary>Reserved; set to <see langword="null" />.</summary>
		// Token: 0x04002C4A RID: 11338
		public int dwReserved;

		/// <summary>Indicates an <see cref="T:System.Runtime.InteropServices.IDLFLAG" /> value describing the type.</summary>
		// Token: 0x04002C4B RID: 11339
		public IDLFLAG wIDLFlags;
	}
}
