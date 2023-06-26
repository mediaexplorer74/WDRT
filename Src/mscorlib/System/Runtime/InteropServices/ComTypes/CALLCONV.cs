using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Identifies the calling convention used by a method described in a METHODDATA structure.</summary>
	// Token: 0x02000A49 RID: 2633
	[__DynamicallyInvokable]
	[Serializable]
	public enum CALLCONV
	{
		/// <summary>Indicates that the C declaration (CDECL) calling convention is used for a method.</summary>
		// Token: 0x04002DE3 RID: 11747
		[__DynamicallyInvokable]
		CC_CDECL = 1,
		/// <summary>Indicates that the MSC Pascal (MSCPASCAL) calling convention is used for a method.</summary>
		// Token: 0x04002DE4 RID: 11748
		[__DynamicallyInvokable]
		CC_MSCPASCAL,
		/// <summary>Indicates that the Pascal calling convention is used for a method.</summary>
		// Token: 0x04002DE5 RID: 11749
		[__DynamicallyInvokable]
		CC_PASCAL = 2,
		/// <summary>Indicates that the Macintosh Pascal (MACPASCAL) calling convention is used for a method.</summary>
		// Token: 0x04002DE6 RID: 11750
		[__DynamicallyInvokable]
		CC_MACPASCAL,
		/// <summary>Indicates that the standard calling convention (STDCALL) is used for a method.</summary>
		// Token: 0x04002DE7 RID: 11751
		[__DynamicallyInvokable]
		CC_STDCALL,
		/// <summary>This value is reserved for future use.</summary>
		// Token: 0x04002DE8 RID: 11752
		[__DynamicallyInvokable]
		CC_RESERVED,
		/// <summary>Indicates that the standard SYSCALL calling convention is used for a method.</summary>
		// Token: 0x04002DE9 RID: 11753
		[__DynamicallyInvokable]
		CC_SYSCALL,
		/// <summary>Indicates that the Macintosh Programmers' Workbench (MPW) CDECL calling convention is used for a method.</summary>
		// Token: 0x04002DEA RID: 11754
		[__DynamicallyInvokable]
		CC_MPWCDECL,
		/// <summary>Indicates that the Macintosh Programmers' Workbench (MPW) PASCAL calling convention is used for a method.</summary>
		// Token: 0x04002DEB RID: 11755
		[__DynamicallyInvokable]
		CC_MPWPASCAL,
		/// <summary>Indicates the end of the <see cref="T:System.Runtime.InteropServices.ComTypes.CALLCONV" /> enumeration.</summary>
		// Token: 0x04002DEC RID: 11756
		[__DynamicallyInvokable]
		CC_MAX
	}
}
