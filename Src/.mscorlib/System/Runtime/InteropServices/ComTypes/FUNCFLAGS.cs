using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Identifies the constants that define the properties of a function.</summary>
	// Token: 0x02000A4A RID: 2634
	[Flags]
	[__DynamicallyInvokable]
	[Serializable]
	public enum FUNCFLAGS : short
	{
		/// <summary>The function should not be accessible from macro languages. This flag is intended for system-level functions or functions that type browsers should not display.</summary>
		// Token: 0x04002DEE RID: 11758
		[__DynamicallyInvokable]
		FUNCFLAG_FRESTRICTED = 1,
		/// <summary>The function returns an object that is a source of events.</summary>
		// Token: 0x04002DEF RID: 11759
		[__DynamicallyInvokable]
		FUNCFLAG_FSOURCE = 2,
		/// <summary>The function that supports data binding.</summary>
		// Token: 0x04002DF0 RID: 11760
		[__DynamicallyInvokable]
		FUNCFLAG_FBINDABLE = 4,
		/// <summary>When set, any call to a method that sets the property results first in a call to <see langword="IPropertyNotifySink::OnRequestEdit" />. The implementation of <see langword="OnRequestEdit" /> determines if the call is allowed to set the property.</summary>
		// Token: 0x04002DF1 RID: 11761
		[__DynamicallyInvokable]
		FUNCFLAG_FREQUESTEDIT = 8,
		/// <summary>The function that is displayed to the user as bindable. <see cref="F:System.Runtime.InteropServices.FUNCFLAGS.FUNCFLAG_FBINDABLE" /> must also be set.</summary>
		// Token: 0x04002DF2 RID: 11762
		[__DynamicallyInvokable]
		FUNCFLAG_FDISPLAYBIND = 16,
		/// <summary>The function that best represents the object. Only one function in a type can have this attribute.</summary>
		// Token: 0x04002DF3 RID: 11763
		[__DynamicallyInvokable]
		FUNCFLAG_FDEFAULTBIND = 32,
		/// <summary>The function should not be displayed to the user, although it exists and is bindable.</summary>
		// Token: 0x04002DF4 RID: 11764
		[__DynamicallyInvokable]
		FUNCFLAG_FHIDDEN = 64,
		/// <summary>The function supports <see langword="GetLastError" />. If an error occurs during the function, the caller can call <see langword="GetLastError" /> to retrieve the error code.</summary>
		// Token: 0x04002DF5 RID: 11765
		[__DynamicallyInvokable]
		FUNCFLAG_FUSESGETLASTERROR = 128,
		/// <summary>Permits an optimization in which the compiler looks for a member named "xyz" on the type of "abc". If such a member is found, and is flagged as an accessor function for an element of the default collection, a call is generated to that member function. Permitted on members in dispinterfaces and interfaces; not permitted on modules.</summary>
		// Token: 0x04002DF6 RID: 11766
		[__DynamicallyInvokable]
		FUNCFLAG_FDEFAULTCOLLELEM = 256,
		/// <summary>The type information member is the default member for display in the user interface.</summary>
		// Token: 0x04002DF7 RID: 11767
		[__DynamicallyInvokable]
		FUNCFLAG_FUIDEFAULT = 512,
		/// <summary>The property appears in an object browser, but not in a properties browser.</summary>
		// Token: 0x04002DF8 RID: 11768
		[__DynamicallyInvokable]
		FUNCFLAG_FNONBROWSABLE = 1024,
		/// <summary>Tags the interface as having default behaviors.</summary>
		// Token: 0x04002DF9 RID: 11769
		[__DynamicallyInvokable]
		FUNCFLAG_FREPLACEABLE = 2048,
		/// <summary>Mapped as individual bindable properties.</summary>
		// Token: 0x04002DFA RID: 11770
		[__DynamicallyInvokable]
		FUNCFLAG_FIMMEDIATEBIND = 4096
	}
}
