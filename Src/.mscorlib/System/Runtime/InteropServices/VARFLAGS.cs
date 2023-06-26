using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.VARFLAGS" /> instead.</summary>
	// Token: 0x020009A1 RID: 2465
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.VARFLAGS instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Flags]
	[Serializable]
	public enum VARFLAGS : short
	{
		/// <summary>Assignment to the variable should not be allowed.</summary>
		// Token: 0x04002C91 RID: 11409
		VARFLAG_FREADONLY = 1,
		/// <summary>The variable returns an object that is a source of events.</summary>
		// Token: 0x04002C92 RID: 11410
		VARFLAG_FSOURCE = 2,
		/// <summary>The variable supports data binding.</summary>
		// Token: 0x04002C93 RID: 11411
		VARFLAG_FBINDABLE = 4,
		/// <summary>When set, any attempt to directly change the property results in a call to <see langword="IPropertyNotifySink::OnRequestEdit" />. The implementation of <see langword="OnRequestEdit" /> determines if the change is accepted.</summary>
		// Token: 0x04002C94 RID: 11412
		VARFLAG_FREQUESTEDIT = 8,
		/// <summary>The variable is displayed to the user as bindable. <see cref="F:System.Runtime.InteropServices.VARFLAGS.VARFLAG_FBINDABLE" /> must also be set.</summary>
		// Token: 0x04002C95 RID: 11413
		VARFLAG_FDISPLAYBIND = 16,
		/// <summary>The variable is the single property that best represents the object. Only one variable in type information can have this attribute.</summary>
		// Token: 0x04002C96 RID: 11414
		VARFLAG_FDEFAULTBIND = 32,
		/// <summary>The variable should not be displayed to the user in a browser, although it exists and is bindable.</summary>
		// Token: 0x04002C97 RID: 11415
		VARFLAG_FHIDDEN = 64,
		/// <summary>The variable should not be accessible from macro languages. This flag is intended for system-level variables or variables that you do not want type browsers to display.</summary>
		// Token: 0x04002C98 RID: 11416
		VARFLAG_FRESTRICTED = 128,
		/// <summary>Permits an optimization in which the compiler looks for a member named "xyz" on the type of "abc". If such a member is found and is flagged as an accessor function for an element of the default collection, then a call is generated to that member function. Permitted on members in dispinterfaces and interfaces; not permitted on modules.</summary>
		// Token: 0x04002C99 RID: 11417
		VARFLAG_FDEFAULTCOLLELEM = 256,
		/// <summary>The variable is the default display in the user interface.</summary>
		// Token: 0x04002C9A RID: 11418
		VARFLAG_FUIDEFAULT = 512,
		/// <summary>The variable appears in an object browser, but not in a properties browser.</summary>
		// Token: 0x04002C9B RID: 11419
		VARFLAG_FNONBROWSABLE = 1024,
		/// <summary>Tags the interface as having default behaviors.</summary>
		// Token: 0x04002C9C RID: 11420
		VARFLAG_FREPLACEABLE = 2048,
		/// <summary>The variable is mapped as individual bindable properties.</summary>
		// Token: 0x04002C9D RID: 11421
		VARFLAG_FIMMEDIATEBIND = 4096
	}
}
