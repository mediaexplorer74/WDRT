using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Defines the properties and attributes of a type description.</summary>
	// Token: 0x02000A39 RID: 2617
	[Flags]
	[__DynamicallyInvokable]
	[Serializable]
	public enum TYPEFLAGS : short
	{
		/// <summary>A type description that describes an <see langword="Application" /> object.</summary>
		// Token: 0x04002D75 RID: 11637
		[__DynamicallyInvokable]
		TYPEFLAG_FAPPOBJECT = 1,
		/// <summary>Instances of the type can be created by <see langword="ITypeInfo::CreateInstance" />.</summary>
		// Token: 0x04002D76 RID: 11638
		[__DynamicallyInvokable]
		TYPEFLAG_FCANCREATE = 2,
		/// <summary>The type is licensed.</summary>
		// Token: 0x04002D77 RID: 11639
		[__DynamicallyInvokable]
		TYPEFLAG_FLICENSED = 4,
		/// <summary>The type is predefined. The client application should automatically create a single instance of the object that has this attribute. The name of the variable that points to the object is the same as the class name of the object.</summary>
		// Token: 0x04002D78 RID: 11640
		[__DynamicallyInvokable]
		TYPEFLAG_FPREDECLID = 8,
		/// <summary>The type should not be displayed to browsers.</summary>
		// Token: 0x04002D79 RID: 11641
		[__DynamicallyInvokable]
		TYPEFLAG_FHIDDEN = 16,
		/// <summary>The type is a control from which other types will be derived and should not be displayed to users.</summary>
		// Token: 0x04002D7A RID: 11642
		[__DynamicallyInvokable]
		TYPEFLAG_FCONTROL = 32,
		/// <summary>The interface supplies both <see langword="IDispatch" /> and VTBL binding.</summary>
		// Token: 0x04002D7B RID: 11643
		[__DynamicallyInvokable]
		TYPEFLAG_FDUAL = 64,
		/// <summary>The interface cannot add members at run time.</summary>
		// Token: 0x04002D7C RID: 11644
		[__DynamicallyInvokable]
		TYPEFLAG_FNONEXTENSIBLE = 128,
		/// <summary>The types used in the interface are fully compatible with Automation, including VTBL binding support. Setting dual on an interface sets both this flag and the  <see cref="F:System.Runtime.InteropServices.TYPEFLAGS.TYPEFLAG_FDUAL" />. This flag is not allowed on dispinterfaces.</summary>
		// Token: 0x04002D7D RID: 11645
		[__DynamicallyInvokable]
		TYPEFLAG_FOLEAUTOMATION = 256,
		/// <summary>Should not be accessible from macro languages. This flag is intended for system-level types or types that type browsers should not display.</summary>
		// Token: 0x04002D7E RID: 11646
		[__DynamicallyInvokable]
		TYPEFLAG_FRESTRICTED = 512,
		/// <summary>The class supports aggregation.</summary>
		// Token: 0x04002D7F RID: 11647
		[__DynamicallyInvokable]
		TYPEFLAG_FAGGREGATABLE = 1024,
		/// <summary>The object supports <see langword="IConnectionPointWithDefault" />, and has default behaviors.</summary>
		// Token: 0x04002D80 RID: 11648
		[__DynamicallyInvokable]
		TYPEFLAG_FREPLACEABLE = 2048,
		/// <summary>Indicates that the interface derives from <see langword="IDispatch" />, either directly or indirectly. This flag is computed; there is no Object Description Language for the flag.</summary>
		// Token: 0x04002D81 RID: 11649
		[__DynamicallyInvokable]
		TYPEFLAG_FDISPATCHABLE = 4096,
		/// <summary>Indicates base interfaces should be checked for name resolution before checking children, which is the reverse of the default behavior.</summary>
		// Token: 0x04002D82 RID: 11650
		[__DynamicallyInvokable]
		TYPEFLAG_FREVERSEBIND = 8192,
		/// <summary>Indicates that the interface will be using a proxy/stub dynamic link library. This flag specifies that the type library proxy should not be unregistered when the type library is unregistered.</summary>
		// Token: 0x04002D83 RID: 11651
		[__DynamicallyInvokable]
		TYPEFLAG_FPROXY = 16384
	}
}
