using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Controls the layout of an object when exported to unmanaged code.</summary>
	// Token: 0x0200094C RID: 2380
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum LayoutKind
	{
		/// <summary>The members of the object are laid out sequentially, in the order in which they appear when exported to unmanaged memory. The members are laid out according to the packing specified in <see cref="F:System.Runtime.InteropServices.StructLayoutAttribute.Pack" />, and can be noncontiguous.</summary>
		// Token: 0x04002B5D RID: 11101
		[__DynamicallyInvokable]
		Sequential,
		/// <summary>The precise position of each member of an object in unmanaged memory is explicitly controlled, subject to the setting of the <see cref="F:System.Runtime.InteropServices.StructLayoutAttribute.Pack" /> field. Each member must use the <see cref="T:System.Runtime.InteropServices.FieldOffsetAttribute" /> to indicate the position of that field within the type.</summary>
		// Token: 0x04002B5E RID: 11102
		[__DynamicallyInvokable]
		Explicit = 2,
		/// <summary>The runtime automatically chooses an appropriate layout for the members of an object in unmanaged memory. Objects defined with this enumeration member cannot be exposed outside of managed code. Attempting to do so generates an exception.</summary>
		// Token: 0x04002B5F RID: 11103
		[__DynamicallyInvokable]
		Auto
	}
}
