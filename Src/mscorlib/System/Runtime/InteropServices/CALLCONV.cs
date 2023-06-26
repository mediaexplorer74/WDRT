using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.CALLCONV" /> instead.</summary>
	// Token: 0x0200099F RID: 2463
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.CALLCONV instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Serializable]
	public enum CALLCONV
	{
		/// <summary>Indicates that the Cdecl calling convention is used for a method.</summary>
		// Token: 0x04002C78 RID: 11384
		CC_CDECL = 1,
		/// <summary>Indicates that the Mscpascal calling convention is used for a method.</summary>
		// Token: 0x04002C79 RID: 11385
		CC_MSCPASCAL,
		/// <summary>Indicates that the Pascal calling convention is used for a method.</summary>
		// Token: 0x04002C7A RID: 11386
		CC_PASCAL = 2,
		/// <summary>Indicates that the Macpascal calling convention is used for a method.</summary>
		// Token: 0x04002C7B RID: 11387
		CC_MACPASCAL,
		/// <summary>Indicates that the Stdcall calling convention is used for a method.</summary>
		// Token: 0x04002C7C RID: 11388
		CC_STDCALL,
		/// <summary>This value is reserved for future use.</summary>
		// Token: 0x04002C7D RID: 11389
		CC_RESERVED,
		/// <summary>Indicates that the Syscall calling convention is used for a method.</summary>
		// Token: 0x04002C7E RID: 11390
		CC_SYSCALL,
		/// <summary>Indicates that the Mpwcdecl calling convention is used for a method.</summary>
		// Token: 0x04002C7F RID: 11391
		CC_MPWCDECL,
		/// <summary>Indicates that the Mpwpascal calling convention is used for a method.</summary>
		// Token: 0x04002C80 RID: 11392
		CC_MPWPASCAL,
		/// <summary>Indicates the end of the <see cref="T:System.Runtime.InteropServices.CALLCONV" /> enumeration.</summary>
		// Token: 0x04002C81 RID: 11393
		CC_MAX
	}
}
