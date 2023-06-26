using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.FUNCFLAGS" /> instead.</summary>
	// Token: 0x020009A0 RID: 2464
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.FUNCFLAGS instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Flags]
	[Serializable]
	public enum FUNCFLAGS : short
	{
		/// <summary>The function should not be accessible from macro languages. This flag is intended for system-level functions or functions that type browsers should not display.</summary>
		// Token: 0x04002C83 RID: 11395
		FUNCFLAG_FRESTRICTED = 1,
		/// <summary>The function returns an object that is a source of events.</summary>
		// Token: 0x04002C84 RID: 11396
		FUNCFLAG_FSOURCE = 2,
		/// <summary>The function that supports data binding.</summary>
		// Token: 0x04002C85 RID: 11397
		FUNCFLAG_FBINDABLE = 4,
		/// <summary>When set, any call to a method that sets the property results first in a call to <see langword="IPropertyNotifySink::OnRequestEdit" />. The implementation of <see langword="OnRequestEdit" /> determines if the call is allowed to set the property.</summary>
		// Token: 0x04002C86 RID: 11398
		FUNCFLAG_FREQUESTEDIT = 8,
		/// <summary>The function that is displayed to the user as bindable. <see cref="F:System.Runtime.InteropServices.FUNCFLAGS.FUNCFLAG_FBINDABLE" /> must also be set.</summary>
		// Token: 0x04002C87 RID: 11399
		FUNCFLAG_FDISPLAYBIND = 16,
		/// <summary>The function that best represents the object. Only one function in a type information can have this attribute.</summary>
		// Token: 0x04002C88 RID: 11400
		FUNCFLAG_FDEFAULTBIND = 32,
		/// <summary>The function should not be displayed to the user, although it exists and is bindable.</summary>
		// Token: 0x04002C89 RID: 11401
		FUNCFLAG_FHIDDEN = 64,
		/// <summary>The function supports <see langword="GetLastError" />. If an error occurs during the function, the caller can call <see langword="GetLastError" /> to retrieve the error code.</summary>
		// Token: 0x04002C8A RID: 11402
		FUNCFLAG_FUSESGETLASTERROR = 128,
		/// <summary>Permits an optimization in which the compiler looks for a member named "xyz" on the type of "abc". If such a member is found, and is flagged as an accessor function for an element of the default collection, a call is generated to that member function. Permitted on members in dispinterfaces and interfaces; not permitted on modules.</summary>
		// Token: 0x04002C8B RID: 11403
		FUNCFLAG_FDEFAULTCOLLELEM = 256,
		/// <summary>The type information member is the default member for display in the user interface.</summary>
		// Token: 0x04002C8C RID: 11404
		FUNCFLAG_FUIDEFAULT = 512,
		/// <summary>The property appears in an object browser, but not in a properties browser.</summary>
		// Token: 0x04002C8D RID: 11405
		FUNCFLAG_FNONBROWSABLE = 1024,
		/// <summary>Tags the interface as having default behaviors.</summary>
		// Token: 0x04002C8E RID: 11406
		FUNCFLAG_FREPLACEABLE = 2048,
		/// <summary>Mapped as individual bindable properties.</summary>
		// Token: 0x04002C8F RID: 11407
		FUNCFLAG_FIMMEDIATEBIND = 4096
	}
}
