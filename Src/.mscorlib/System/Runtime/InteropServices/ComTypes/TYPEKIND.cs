using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Specifies various types of data and functions.</summary>
	// Token: 0x02000A38 RID: 2616
	[__DynamicallyInvokable]
	[Serializable]
	public enum TYPEKIND
	{
		/// <summary>A set of enumerators.</summary>
		// Token: 0x04002D6B RID: 11627
		[__DynamicallyInvokable]
		TKIND_ENUM,
		/// <summary>A structure with no methods.</summary>
		// Token: 0x04002D6C RID: 11628
		[__DynamicallyInvokable]
		TKIND_RECORD,
		/// <summary>A module that can have only static functions and data (for example, a DLL).</summary>
		// Token: 0x04002D6D RID: 11629
		[__DynamicallyInvokable]
		TKIND_MODULE,
		/// <summary>A type that has virtual functions, all of which are pure.</summary>
		// Token: 0x04002D6E RID: 11630
		[__DynamicallyInvokable]
		TKIND_INTERFACE,
		/// <summary>A set of methods and properties that are accessible through <see langword="IDispatch::Invoke" />. By default, dual interfaces return <see langword="TKIND_DISPATCH" />.</summary>
		// Token: 0x04002D6F RID: 11631
		[__DynamicallyInvokable]
		TKIND_DISPATCH,
		/// <summary>A set of implemented components interfaces.</summary>
		// Token: 0x04002D70 RID: 11632
		[__DynamicallyInvokable]
		TKIND_COCLASS,
		/// <summary>A type that is an alias for another type.</summary>
		// Token: 0x04002D71 RID: 11633
		[__DynamicallyInvokable]
		TKIND_ALIAS,
		/// <summary>A union of all members that have an offset of zero.</summary>
		// Token: 0x04002D72 RID: 11634
		[__DynamicallyInvokable]
		TKIND_UNION,
		/// <summary>End-of-enumeration marker.</summary>
		// Token: 0x04002D73 RID: 11635
		[__DynamicallyInvokable]
		TKIND_MAX
	}
}
