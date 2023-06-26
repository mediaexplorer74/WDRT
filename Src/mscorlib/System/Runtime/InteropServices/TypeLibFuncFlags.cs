using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Describes the original settings of the <see langword="FUNCFLAGS" /> in the COM type library from where this method was imported.</summary>
	// Token: 0x02000922 RID: 2338
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum TypeLibFuncFlags
	{
		/// <summary>This flag is intended for system-level functions or functions that type browsers should not display.</summary>
		// Token: 0x04002A94 RID: 10900
		FRestricted = 1,
		/// <summary>The function returns an object that is a source of events.</summary>
		// Token: 0x04002A95 RID: 10901
		FSource = 2,
		/// <summary>The function that supports data binding.</summary>
		// Token: 0x04002A96 RID: 10902
		FBindable = 4,
		/// <summary>When set, any call to a method that sets the property results first in a call to <see langword="IPropertyNotifySink::OnRequestEdit" />.</summary>
		// Token: 0x04002A97 RID: 10903
		FRequestEdit = 8,
		/// <summary>The function that is displayed to the user as bindable. <see cref="F:System.Runtime.InteropServices.TypeLibFuncFlags.FBindable" /> must also be set.</summary>
		// Token: 0x04002A98 RID: 10904
		FDisplayBind = 16,
		/// <summary>The function that best represents the object. Only one function in a type information can have this attribute.</summary>
		// Token: 0x04002A99 RID: 10905
		FDefaultBind = 32,
		/// <summary>The function should not be displayed to the user, although it exists and is bindable.</summary>
		// Token: 0x04002A9A RID: 10906
		FHidden = 64,
		/// <summary>The function supports <see langword="GetLastError" />.</summary>
		// Token: 0x04002A9B RID: 10907
		FUsesGetLastError = 128,
		/// <summary>Permits an optimization in which the compiler looks for a member named "xyz" on the type "abc". If such a member is found and is flagged as an accessor function for an element of the default collection, then a call is generated to that member function.</summary>
		// Token: 0x04002A9C RID: 10908
		FDefaultCollelem = 256,
		/// <summary>The type information member is the default member for display in the user interface.</summary>
		// Token: 0x04002A9D RID: 10909
		FUiDefault = 512,
		/// <summary>The property appears in an object browser, but not in a properties browser.</summary>
		// Token: 0x04002A9E RID: 10910
		FNonBrowsable = 1024,
		/// <summary>Tags the interface as having default behaviors.</summary>
		// Token: 0x04002A9F RID: 10911
		FReplaceable = 2048,
		/// <summary>The function is mapped as individual bindable properties.</summary>
		// Token: 0x04002AA0 RID: 10912
		FImmediateBind = 4096
	}
}
