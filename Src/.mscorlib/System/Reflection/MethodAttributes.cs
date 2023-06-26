using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies flags for method attributes. These flags are defined in the corhdr.h file.</summary>
	// Token: 0x02000602 RID: 1538
	[Flags]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum MethodAttributes
	{
		/// <summary>Retrieves accessibility information.</summary>
		// Token: 0x04001D70 RID: 7536
		[__DynamicallyInvokable]
		MemberAccessMask = 7,
		/// <summary>Indicates that the member cannot be referenced.</summary>
		// Token: 0x04001D71 RID: 7537
		[__DynamicallyInvokable]
		PrivateScope = 0,
		/// <summary>Indicates that the method is accessible only to the current class.</summary>
		// Token: 0x04001D72 RID: 7538
		[__DynamicallyInvokable]
		Private = 1,
		/// <summary>Indicates that the method is accessible to members of this type and its derived types that are in this assembly only.</summary>
		// Token: 0x04001D73 RID: 7539
		[__DynamicallyInvokable]
		FamANDAssem = 2,
		/// <summary>Indicates that the method is accessible to any class of this assembly.</summary>
		// Token: 0x04001D74 RID: 7540
		[__DynamicallyInvokable]
		Assembly = 3,
		/// <summary>Indicates that the method is accessible only to members of this class and its derived classes.</summary>
		// Token: 0x04001D75 RID: 7541
		[__DynamicallyInvokable]
		Family = 4,
		/// <summary>Indicates that the method is accessible to derived classes anywhere, as well as to any class in the assembly.</summary>
		// Token: 0x04001D76 RID: 7542
		[__DynamicallyInvokable]
		FamORAssem = 5,
		/// <summary>Indicates that the method is accessible to any object for which this object is in scope.</summary>
		// Token: 0x04001D77 RID: 7543
		[__DynamicallyInvokable]
		Public = 6,
		/// <summary>Indicates that the method is defined on the type; otherwise, it is defined per instance.</summary>
		// Token: 0x04001D78 RID: 7544
		[__DynamicallyInvokable]
		Static = 16,
		/// <summary>Indicates that the method cannot be overridden.</summary>
		// Token: 0x04001D79 RID: 7545
		[__DynamicallyInvokable]
		Final = 32,
		/// <summary>Indicates that the method is virtual.</summary>
		// Token: 0x04001D7A RID: 7546
		[__DynamicallyInvokable]
		Virtual = 64,
		/// <summary>Indicates that the method hides by name and signature; otherwise, by name only.</summary>
		// Token: 0x04001D7B RID: 7547
		[__DynamicallyInvokable]
		HideBySig = 128,
		/// <summary>Indicates that the method can only be overridden when it is also accessible.</summary>
		// Token: 0x04001D7C RID: 7548
		[__DynamicallyInvokable]
		CheckAccessOnOverride = 512,
		/// <summary>Retrieves vtable attributes.</summary>
		// Token: 0x04001D7D RID: 7549
		[__DynamicallyInvokable]
		VtableLayoutMask = 256,
		/// <summary>Indicates that the method will reuse an existing slot in the vtable. This is the default behavior.</summary>
		// Token: 0x04001D7E RID: 7550
		[__DynamicallyInvokable]
		ReuseSlot = 0,
		/// <summary>Indicates that the method always gets a new slot in the vtable.</summary>
		// Token: 0x04001D7F RID: 7551
		[__DynamicallyInvokable]
		NewSlot = 256,
		/// <summary>Indicates that the class does not provide an implementation of this method.</summary>
		// Token: 0x04001D80 RID: 7552
		[__DynamicallyInvokable]
		Abstract = 1024,
		/// <summary>Indicates that the method is special. The name describes how this method is special.</summary>
		// Token: 0x04001D81 RID: 7553
		[__DynamicallyInvokable]
		SpecialName = 2048,
		/// <summary>Indicates that the method implementation is forwarded through PInvoke (Platform Invocation Services).</summary>
		// Token: 0x04001D82 RID: 7554
		[__DynamicallyInvokable]
		PinvokeImpl = 8192,
		/// <summary>Indicates that the managed method is exported by thunk to unmanaged code.</summary>
		// Token: 0x04001D83 RID: 7555
		[__DynamicallyInvokable]
		UnmanagedExport = 8,
		/// <summary>Indicates that the common language runtime checks the name encoding.</summary>
		// Token: 0x04001D84 RID: 7556
		[__DynamicallyInvokable]
		RTSpecialName = 4096,
		/// <summary>Indicates a reserved flag for runtime use only.</summary>
		// Token: 0x04001D85 RID: 7557
		ReservedMask = 53248,
		/// <summary>Indicates that the method has security associated with it. Reserved flag for runtime use only.</summary>
		// Token: 0x04001D86 RID: 7558
		[__DynamicallyInvokable]
		HasSecurity = 16384,
		/// <summary>Indicates that the method calls another method containing security code. Reserved flag for runtime use only.</summary>
		// Token: 0x04001D87 RID: 7559
		[__DynamicallyInvokable]
		RequireSecObject = 32768
	}
}
