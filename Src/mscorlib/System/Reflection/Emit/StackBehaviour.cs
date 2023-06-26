using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Describes how values are pushed onto a stack or popped off a stack.</summary>
	// Token: 0x02000656 RID: 1622
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum StackBehaviour
	{
		/// <summary>No values are popped off the stack.</summary>
		// Token: 0x04002158 RID: 8536
		[__DynamicallyInvokable]
		Pop0,
		/// <summary>Pops one value off the stack.</summary>
		// Token: 0x04002159 RID: 8537
		[__DynamicallyInvokable]
		Pop1,
		/// <summary>Pops 1 value off the stack for the first operand, and 1 value of the stack for the second operand.</summary>
		// Token: 0x0400215A RID: 8538
		[__DynamicallyInvokable]
		Pop1_pop1,
		/// <summary>Pops a 32-bit integer off the stack.</summary>
		// Token: 0x0400215B RID: 8539
		[__DynamicallyInvokable]
		Popi,
		/// <summary>Pops a 32-bit integer off the stack for the first operand, and a value off the stack for the second operand.</summary>
		// Token: 0x0400215C RID: 8540
		[__DynamicallyInvokable]
		Popi_pop1,
		/// <summary>Pops a 32-bit integer off the stack for the first operand, and a 32-bit integer off the stack for the second operand.</summary>
		// Token: 0x0400215D RID: 8541
		[__DynamicallyInvokable]
		Popi_popi,
		/// <summary>Pops a 32-bit integer off the stack for the first operand, and a 64-bit integer off the stack for the second operand.</summary>
		// Token: 0x0400215E RID: 8542
		[__DynamicallyInvokable]
		Popi_popi8,
		/// <summary>Pops a 32-bit integer off the stack for the first operand, a 32-bit integer off the stack for the second operand, and a 32-bit integer off the stack for the third operand.</summary>
		// Token: 0x0400215F RID: 8543
		[__DynamicallyInvokable]
		Popi_popi_popi,
		/// <summary>Pops a 32-bit integer off the stack for the first operand, and a 32-bit floating point number off the stack for the second operand.</summary>
		// Token: 0x04002160 RID: 8544
		[__DynamicallyInvokable]
		Popi_popr4,
		/// <summary>Pops a 32-bit integer off the stack for the first operand, and a 64-bit floating point number off the stack for the second operand.</summary>
		// Token: 0x04002161 RID: 8545
		[__DynamicallyInvokable]
		Popi_popr8,
		/// <summary>Pops a reference off the stack.</summary>
		// Token: 0x04002162 RID: 8546
		[__DynamicallyInvokable]
		Popref,
		/// <summary>Pops a reference off the stack for the first operand, and a value off the stack for the second operand.</summary>
		// Token: 0x04002163 RID: 8547
		[__DynamicallyInvokable]
		Popref_pop1,
		/// <summary>Pops a reference off the stack for the first operand, and a 32-bit integer off the stack for the second operand.</summary>
		// Token: 0x04002164 RID: 8548
		[__DynamicallyInvokable]
		Popref_popi,
		/// <summary>Pops a reference off the stack for the first operand, a value off the stack for the second operand, and a value off the stack for the third operand.</summary>
		// Token: 0x04002165 RID: 8549
		[__DynamicallyInvokable]
		Popref_popi_popi,
		/// <summary>Pops a reference off the stack for the first operand, a value off the stack for the second operand, and a 64-bit integer off the stack for the third operand.</summary>
		// Token: 0x04002166 RID: 8550
		[__DynamicallyInvokable]
		Popref_popi_popi8,
		/// <summary>Pops a reference off the stack for the first operand, a value off the stack for the second operand, and a 32-bit integer off the stack for the third operand.</summary>
		// Token: 0x04002167 RID: 8551
		[__DynamicallyInvokable]
		Popref_popi_popr4,
		/// <summary>Pops a reference off the stack for the first operand, a value off the stack for the second operand, and a 64-bit floating point number off the stack for the third operand.</summary>
		// Token: 0x04002168 RID: 8552
		[__DynamicallyInvokable]
		Popref_popi_popr8,
		/// <summary>Pops a reference off the stack for the first operand, a value off the stack for the second operand, and a reference off the stack for the third operand.</summary>
		// Token: 0x04002169 RID: 8553
		[__DynamicallyInvokable]
		Popref_popi_popref,
		/// <summary>No values are pushed onto the stack.</summary>
		// Token: 0x0400216A RID: 8554
		[__DynamicallyInvokable]
		Push0,
		/// <summary>Pushes one value onto the stack.</summary>
		// Token: 0x0400216B RID: 8555
		[__DynamicallyInvokable]
		Push1,
		/// <summary>Pushes 1 value onto the stack for the first operand, and 1 value onto the stack for the second operand.</summary>
		// Token: 0x0400216C RID: 8556
		[__DynamicallyInvokable]
		Push1_push1,
		/// <summary>Pushes a 32-bit integer onto the stack.</summary>
		// Token: 0x0400216D RID: 8557
		[__DynamicallyInvokable]
		Pushi,
		/// <summary>Pushes a 64-bit integer onto the stack.</summary>
		// Token: 0x0400216E RID: 8558
		[__DynamicallyInvokable]
		Pushi8,
		/// <summary>Pushes a 32-bit floating point number onto the stack.</summary>
		// Token: 0x0400216F RID: 8559
		[__DynamicallyInvokable]
		Pushr4,
		/// <summary>Pushes a 64-bit floating point number onto the stack.</summary>
		// Token: 0x04002170 RID: 8560
		[__DynamicallyInvokable]
		Pushr8,
		/// <summary>Pushes a reference onto the stack.</summary>
		// Token: 0x04002171 RID: 8561
		[__DynamicallyInvokable]
		Pushref,
		/// <summary>Pops a variable off the stack.</summary>
		// Token: 0x04002172 RID: 8562
		[__DynamicallyInvokable]
		Varpop,
		/// <summary>Pushes a variable onto the stack.</summary>
		// Token: 0x04002173 RID: 8563
		[__DynamicallyInvokable]
		Varpush,
		/// <summary>Pops a reference off the stack for the first operand, a value off the stack for the second operand, and a 32-bit integer off the stack for the third operand.</summary>
		// Token: 0x04002174 RID: 8564
		[__DynamicallyInvokable]
		Popref_popi_pop1
	}
}
