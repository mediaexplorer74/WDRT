using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Defines the valid calling conventions for a method.</summary>
	// Token: 0x020005CF RID: 1487
	[Flags]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum CallingConventions
	{
		/// <summary>Specifies the default calling convention as determined by the common language runtime. Use this calling convention for static methods. For instance or virtual methods use <see langword="HasThis" />.</summary>
		// Token: 0x04001C4D RID: 7245
		[__DynamicallyInvokable]
		Standard = 1,
		/// <summary>Specifies the calling convention for methods with variable arguments.</summary>
		// Token: 0x04001C4E RID: 7246
		[__DynamicallyInvokable]
		VarArgs = 2,
		/// <summary>Specifies that either the <see langword="Standard" /> or the <see langword="VarArgs" /> calling convention may be used.</summary>
		// Token: 0x04001C4F RID: 7247
		[__DynamicallyInvokable]
		Any = 3,
		/// <summary>Specifies an instance or virtual method (not a static method). At run-time, the called method is passed a pointer to the target object as its first argument (the <see langword="this" /> pointer). The signature stored in metadata does not include the type of this first argument, because the method is known and its owner class can be discovered from metadata.</summary>
		// Token: 0x04001C50 RID: 7248
		[__DynamicallyInvokable]
		HasThis = 32,
		/// <summary>Specifies that the signature is a function-pointer signature, representing a call to an instance or virtual method (not a static method). If <see langword="ExplicitThis" /> is set, <see langword="HasThis" /> must also be set. The first argument passed to the called method is still a <see langword="this" /> pointer, but the type of the first argument is now unknown. Therefore, a token that describes the type (or class) of the <see langword="this" /> pointer is explicitly stored into its metadata signature.</summary>
		// Token: 0x04001C51 RID: 7249
		[__DynamicallyInvokable]
		ExplicitThis = 64
	}
}
