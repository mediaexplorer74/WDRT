using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Contains information about how to transfer a structure element, parameter, or function return value between processes.</summary>
	// Token: 0x02000A40 RID: 2624
	[__DynamicallyInvokable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct PARAMDESC
	{
		/// <summary>Represents a pointer to a value that is being passed between processes.</summary>
		// Token: 0x04002DB9 RID: 11705
		public IntPtr lpVarValue;

		/// <summary>Represents bitmask values that describe the structure element, parameter, or return value.</summary>
		// Token: 0x04002DBA RID: 11706
		[__DynamicallyInvokable]
		public PARAMFLAG wParamFlags;
	}
}
