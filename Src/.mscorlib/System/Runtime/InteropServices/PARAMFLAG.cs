using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.PARAMFLAG" /> instead.</summary>
	// Token: 0x02000996 RID: 2454
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.PARAMFLAG instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Flags]
	[Serializable]
	public enum PARAMFLAG : short
	{
		/// <summary>Whether the parameter passes or receives information is unspecified.</summary>
		// Token: 0x04002C4D RID: 11341
		PARAMFLAG_NONE = 0,
		/// <summary>The parameter passes information from the caller to the callee.</summary>
		// Token: 0x04002C4E RID: 11342
		PARAMFLAG_FIN = 1,
		/// <summary>The parameter returns information from the callee to the caller.</summary>
		// Token: 0x04002C4F RID: 11343
		PARAMFLAG_FOUT = 2,
		/// <summary>The parameter is the local identifier of a client application.</summary>
		// Token: 0x04002C50 RID: 11344
		PARAMFLAG_FLCID = 4,
		/// <summary>The parameter is the return value of the member.</summary>
		// Token: 0x04002C51 RID: 11345
		PARAMFLAG_FRETVAL = 8,
		/// <summary>The parameter is optional.</summary>
		// Token: 0x04002C52 RID: 11346
		PARAMFLAG_FOPT = 16,
		/// <summary>The parameter has default behaviors defined.</summary>
		// Token: 0x04002C53 RID: 11347
		PARAMFLAG_FHASDEFAULT = 32,
		/// <summary>The parameter has custom data.</summary>
		// Token: 0x04002C54 RID: 11348
		PARAMFLAG_FHASCUSTDATA = 64
	}
}
