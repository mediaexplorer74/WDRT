using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Describes how an instruction alters the flow of control.</summary>
	// Token: 0x02000658 RID: 1624
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum FlowControl
	{
		/// <summary>Branch instruction.</summary>
		// Token: 0x04002189 RID: 8585
		[__DynamicallyInvokable]
		Branch,
		/// <summary>Break instruction.</summary>
		// Token: 0x0400218A RID: 8586
		[__DynamicallyInvokable]
		Break,
		/// <summary>Call instruction.</summary>
		// Token: 0x0400218B RID: 8587
		[__DynamicallyInvokable]
		Call,
		/// <summary>Conditional branch instruction.</summary>
		// Token: 0x0400218C RID: 8588
		[__DynamicallyInvokable]
		Cond_Branch,
		/// <summary>Provides information about a subsequent instruction. For example, the <see langword="Unaligned" /> instruction of <see langword="Reflection.Emit.Opcodes" /> has <see langword="FlowControl.Meta" /> and specifies that the subsequent pointer instruction might be unaligned.</summary>
		// Token: 0x0400218D RID: 8589
		[__DynamicallyInvokable]
		Meta,
		/// <summary>Normal flow of control.</summary>
		// Token: 0x0400218E RID: 8590
		[__DynamicallyInvokable]
		Next,
		/// <summary>This enumerator value is reserved and should not be used.</summary>
		// Token: 0x0400218F RID: 8591
		[Obsolete("This API has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
		Phi,
		/// <summary>Return instruction.</summary>
		// Token: 0x04002190 RID: 8592
		[__DynamicallyInvokable]
		Return,
		/// <summary>Exception throw instruction.</summary>
		// Token: 0x04002191 RID: 8593
		[__DynamicallyInvokable]
		Throw
	}
}
