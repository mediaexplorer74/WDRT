using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.FILETIME" /> instead.</summary>
	// Token: 0x02000985 RID: 2437
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.FILETIME instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	public struct FILETIME
	{
		/// <summary>Specifies the low 32 bits of the <see langword="FILETIME" />.</summary>
		// Token: 0x04002BEF RID: 11247
		public int dwLowDateTime;

		/// <summary>Specifies the high 32 bits of the <see langword="FILETIME" />.</summary>
		// Token: 0x04002BF0 RID: 11248
		public int dwHighDateTime;
	}
}
