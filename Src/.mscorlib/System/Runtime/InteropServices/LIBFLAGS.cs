using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.LIBFLAGS" /> instead.</summary>
	// Token: 0x020009A4 RID: 2468
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.LIBFLAGS instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Flags]
	[Serializable]
	public enum LIBFLAGS : short
	{
		/// <summary>The type library is restricted, and should not be displayed to users.</summary>
		// Token: 0x04002CA3 RID: 11427
		LIBFLAG_FRESTRICTED = 1,
		/// <summary>The type library describes controls, and should not be displayed in type browsers intended for nonvisual objects.</summary>
		// Token: 0x04002CA4 RID: 11428
		LIBFLAG_FCONTROL = 2,
		/// <summary>The type library should not be displayed to users, although its use is not restricted. Should be used by controls. Hosts should create a new type library that wraps the control with extended properties.</summary>
		// Token: 0x04002CA5 RID: 11429
		LIBFLAG_FHIDDEN = 4,
		/// <summary>The type library exists in a persisted form on disk.</summary>
		// Token: 0x04002CA6 RID: 11430
		LIBFLAG_FHASDISKIMAGE = 8
	}
}
