using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies type attributes.</summary>
	// Token: 0x02000622 RID: 1570
	[Flags]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum TypeAttributes
	{
		/// <summary>Specifies type visibility information.</summary>
		// Token: 0x04001E29 RID: 7721
		[__DynamicallyInvokable]
		VisibilityMask = 7,
		/// <summary>Specifies that the class is not public.</summary>
		// Token: 0x04001E2A RID: 7722
		[__DynamicallyInvokable]
		NotPublic = 0,
		/// <summary>Specifies that the class is public.</summary>
		// Token: 0x04001E2B RID: 7723
		[__DynamicallyInvokable]
		Public = 1,
		/// <summary>Specifies that the class is nested with public visibility.</summary>
		// Token: 0x04001E2C RID: 7724
		[__DynamicallyInvokable]
		NestedPublic = 2,
		/// <summary>Specifies that the class is nested with private visibility.</summary>
		// Token: 0x04001E2D RID: 7725
		[__DynamicallyInvokable]
		NestedPrivate = 3,
		/// <summary>Specifies that the class is nested with family visibility, and is thus accessible only by methods within its own type and any derived types.</summary>
		// Token: 0x04001E2E RID: 7726
		[__DynamicallyInvokable]
		NestedFamily = 4,
		/// <summary>Specifies that the class is nested with assembly visibility, and is thus accessible only by methods within its assembly.</summary>
		// Token: 0x04001E2F RID: 7727
		[__DynamicallyInvokable]
		NestedAssembly = 5,
		/// <summary>Specifies that the class is nested with assembly and family visibility, and is thus accessible only by methods lying in the intersection of its family and assembly.</summary>
		// Token: 0x04001E30 RID: 7728
		[__DynamicallyInvokable]
		NestedFamANDAssem = 6,
		/// <summary>Specifies that the class is nested with family or assembly visibility, and is thus accessible only by methods lying in the union of its family and assembly.</summary>
		// Token: 0x04001E31 RID: 7729
		[__DynamicallyInvokable]
		NestedFamORAssem = 7,
		/// <summary>Specifies class layout information.</summary>
		// Token: 0x04001E32 RID: 7730
		[__DynamicallyInvokable]
		LayoutMask = 24,
		/// <summary>Specifies that class fields are automatically laid out by the common language runtime.</summary>
		// Token: 0x04001E33 RID: 7731
		[__DynamicallyInvokable]
		AutoLayout = 0,
		/// <summary>Specifies that class fields are laid out sequentially, in the order that the fields were emitted to the metadata.</summary>
		// Token: 0x04001E34 RID: 7732
		[__DynamicallyInvokable]
		SequentialLayout = 8,
		/// <summary>Specifies that class fields are laid out at the specified offsets.</summary>
		// Token: 0x04001E35 RID: 7733
		[__DynamicallyInvokable]
		ExplicitLayout = 16,
		/// <summary>Specifies class semantics information; the current class is contextful (else agile).</summary>
		// Token: 0x04001E36 RID: 7734
		[__DynamicallyInvokable]
		ClassSemanticsMask = 32,
		/// <summary>Specifies that the type is a class.</summary>
		// Token: 0x04001E37 RID: 7735
		[__DynamicallyInvokable]
		Class = 0,
		/// <summary>Specifies that the type is an interface.</summary>
		// Token: 0x04001E38 RID: 7736
		[__DynamicallyInvokable]
		Interface = 32,
		/// <summary>Specifies that the type is abstract.</summary>
		// Token: 0x04001E39 RID: 7737
		[__DynamicallyInvokable]
		Abstract = 128,
		/// <summary>Specifies that the class is concrete and cannot be extended.</summary>
		// Token: 0x04001E3A RID: 7738
		[__DynamicallyInvokable]
		Sealed = 256,
		/// <summary>Specifies that the class is special in a way denoted by the name.</summary>
		// Token: 0x04001E3B RID: 7739
		[__DynamicallyInvokable]
		SpecialName = 1024,
		/// <summary>Specifies that the class or interface is imported from another module.</summary>
		// Token: 0x04001E3C RID: 7740
		[__DynamicallyInvokable]
		Import = 4096,
		/// <summary>Specifies that the class can be serialized.</summary>
		// Token: 0x04001E3D RID: 7741
		[__DynamicallyInvokable]
		Serializable = 8192,
		/// <summary>Specifies a Windows Runtime type.</summary>
		// Token: 0x04001E3E RID: 7742
		[ComVisible(false)]
		[__DynamicallyInvokable]
		WindowsRuntime = 16384,
		/// <summary>Used to retrieve string information for native interoperability.</summary>
		// Token: 0x04001E3F RID: 7743
		[__DynamicallyInvokable]
		StringFormatMask = 196608,
		/// <summary>LPTSTR is interpreted as ANSI.</summary>
		// Token: 0x04001E40 RID: 7744
		[__DynamicallyInvokable]
		AnsiClass = 0,
		/// <summary>LPTSTR is interpreted as UNICODE.</summary>
		// Token: 0x04001E41 RID: 7745
		[__DynamicallyInvokable]
		UnicodeClass = 65536,
		/// <summary>LPTSTR is interpreted automatically.</summary>
		// Token: 0x04001E42 RID: 7746
		[__DynamicallyInvokable]
		AutoClass = 131072,
		/// <summary>LPSTR is interpreted by some implementation-specific means, which includes the possibility of throwing a <see cref="T:System.NotSupportedException" />. Not used in the Microsoft implementation of the .NET Framework.</summary>
		// Token: 0x04001E43 RID: 7747
		[__DynamicallyInvokable]
		CustomFormatClass = 196608,
		/// <summary>Used to retrieve non-standard encoding information for native interop. The meaning of the values of these 2 bits is unspecified. Not used in the Microsoft implementation of the .NET Framework.</summary>
		// Token: 0x04001E44 RID: 7748
		[__DynamicallyInvokable]
		CustomFormatMask = 12582912,
		/// <summary>Specifies that calling static methods of the type does not force the system to initialize the type.</summary>
		// Token: 0x04001E45 RID: 7749
		[__DynamicallyInvokable]
		BeforeFieldInit = 1048576,
		/// <summary>Attributes reserved for runtime use.</summary>
		// Token: 0x04001E46 RID: 7750
		ReservedMask = 264192,
		/// <summary>Runtime should check name encoding.</summary>
		// Token: 0x04001E47 RID: 7751
		[__DynamicallyInvokable]
		RTSpecialName = 2048,
		/// <summary>Type has security associate with it.</summary>
		// Token: 0x04001E48 RID: 7752
		[__DynamicallyInvokable]
		HasSecurity = 262144
	}
}
