using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Describes the types of the Microsoft intermediate language (MSIL) instructions.</summary>
	// Token: 0x02000655 RID: 1621
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum OpCodeType
	{
		/// <summary>This enumerator value is reserved and should not be used.</summary>
		// Token: 0x04002151 RID: 8529
		[Obsolete("This API has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
		Annotation,
		/// <summary>These are Microsoft intermediate language (MSIL) instructions that are used as a synonym for other MSIL instructions. For example, <see langword="ldarg.0" /> represents the <see langword="ldarg" /> instruction with an argument of 0.</summary>
		// Token: 0x04002152 RID: 8530
		[__DynamicallyInvokable]
		Macro,
		/// <summary>Describes a reserved Microsoft intermediate language (MSIL) instruction.</summary>
		// Token: 0x04002153 RID: 8531
		[__DynamicallyInvokable]
		Nternal,
		/// <summary>Describes a Microsoft intermediate language (MSIL) instruction that applies to objects.</summary>
		// Token: 0x04002154 RID: 8532
		[__DynamicallyInvokable]
		Objmodel,
		/// <summary>Describes a prefix instruction that modifies the behavior of the following instruction.</summary>
		// Token: 0x04002155 RID: 8533
		[__DynamicallyInvokable]
		Prefix,
		/// <summary>Describes a built-in instruction.</summary>
		// Token: 0x04002156 RID: 8534
		[__DynamicallyInvokable]
		Primitive
	}
}
