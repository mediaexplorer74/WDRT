using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Contains information needed for transferring a structure element, parameter, or function return value between processes.</summary>
	// Token: 0x02000A3E RID: 2622
	[__DynamicallyInvokable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct IDLDESC
	{
		/// <summary>Reserved; set to <see langword="null" />.</summary>
		// Token: 0x04002DAE RID: 11694
		public IntPtr dwReserved;

		/// <summary>Indicates an <see cref="T:System.Runtime.InteropServices.IDLFLAG" /> value describing the type.</summary>
		// Token: 0x04002DAF RID: 11695
		[__DynamicallyInvokable]
		public IDLFLAG wIDLFlags;
	}
}
