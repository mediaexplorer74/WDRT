using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Describes the original settings of the <see cref="T:System.Runtime.InteropServices.TYPEFLAGS" /> in the COM type library from which the type was imported.</summary>
	// Token: 0x02000921 RID: 2337
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum TypeLibTypeFlags
	{
		/// <summary>A type description that describes an <see langword="Application" /> object.</summary>
		// Token: 0x04002A85 RID: 10885
		FAppObject = 1,
		/// <summary>Instances of the type can be created by <see langword="ITypeInfo::CreateInstance" />.</summary>
		// Token: 0x04002A86 RID: 10886
		FCanCreate = 2,
		/// <summary>The type is licensed.</summary>
		// Token: 0x04002A87 RID: 10887
		FLicensed = 4,
		/// <summary>The type is predefined. The client application should automatically create a single instance of the object that has this attribute. The name of the variable that points to the object is the same as the class name of the object.</summary>
		// Token: 0x04002A88 RID: 10888
		FPreDeclId = 8,
		/// <summary>The type should not be displayed to browsers.</summary>
		// Token: 0x04002A89 RID: 10889
		FHidden = 16,
		/// <summary>The type is a control from which other types will be derived, and should not be displayed to users.</summary>
		// Token: 0x04002A8A RID: 10890
		FControl = 32,
		/// <summary>The interface supplies both <see langword="IDispatch" /> and V-table binding.</summary>
		// Token: 0x04002A8B RID: 10891
		FDual = 64,
		/// <summary>The interface cannot add members at run time.</summary>
		// Token: 0x04002A8C RID: 10892
		FNonExtensible = 128,
		/// <summary>The types used in the interface are fully compatible with Automation, including vtable binding support.</summary>
		// Token: 0x04002A8D RID: 10893
		FOleAutomation = 256,
		/// <summary>This flag is intended for system-level types or types that type browsers should not display.</summary>
		// Token: 0x04002A8E RID: 10894
		FRestricted = 512,
		/// <summary>The class supports aggregation.</summary>
		// Token: 0x04002A8F RID: 10895
		FAggregatable = 1024,
		/// <summary>The object supports <see langword="IConnectionPointWithDefault" />, and has default behaviors.</summary>
		// Token: 0x04002A90 RID: 10896
		FReplaceable = 2048,
		/// <summary>Indicates that the interface derives from <see langword="IDispatch" />, either directly or indirectly.</summary>
		// Token: 0x04002A91 RID: 10897
		FDispatchable = 4096,
		/// <summary>Indicates base interfaces should be checked for name resolution before checking child interfaces. This is the reverse of the default behavior.</summary>
		// Token: 0x04002A92 RID: 10898
		FReverseBind = 8192
	}
}
