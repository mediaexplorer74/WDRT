using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Describes the operand type of Microsoft intermediate language (MSIL) instruction.</summary>
	// Token: 0x02000657 RID: 1623
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum OperandType
	{
		/// <summary>The operand is a 32-bit integer branch target.</summary>
		// Token: 0x04002176 RID: 8566
		[__DynamicallyInvokable]
		InlineBrTarget,
		/// <summary>The operand is a 32-bit metadata token.</summary>
		// Token: 0x04002177 RID: 8567
		[__DynamicallyInvokable]
		InlineField,
		/// <summary>The operand is a 32-bit integer.</summary>
		// Token: 0x04002178 RID: 8568
		[__DynamicallyInvokable]
		InlineI,
		/// <summary>The operand is a 64-bit integer.</summary>
		// Token: 0x04002179 RID: 8569
		[__DynamicallyInvokable]
		InlineI8,
		/// <summary>The operand is a 32-bit metadata token.</summary>
		// Token: 0x0400217A RID: 8570
		[__DynamicallyInvokable]
		InlineMethod,
		/// <summary>No operand.</summary>
		// Token: 0x0400217B RID: 8571
		[__DynamicallyInvokable]
		InlineNone,
		/// <summary>The operand is reserved and should not be used.</summary>
		// Token: 0x0400217C RID: 8572
		[Obsolete("This API has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
		InlinePhi,
		/// <summary>The operand is a 64-bit IEEE floating point number.</summary>
		// Token: 0x0400217D RID: 8573
		[__DynamicallyInvokable]
		InlineR,
		/// <summary>The operand is a 32-bit metadata signature token.</summary>
		// Token: 0x0400217E RID: 8574
		[__DynamicallyInvokable]
		InlineSig = 9,
		/// <summary>The operand is a 32-bit metadata string token.</summary>
		// Token: 0x0400217F RID: 8575
		[__DynamicallyInvokable]
		InlineString,
		/// <summary>The operand is the 32-bit integer argument to a switch instruction.</summary>
		// Token: 0x04002180 RID: 8576
		[__DynamicallyInvokable]
		InlineSwitch,
		/// <summary>The operand is a <see langword="FieldRef" />, <see langword="MethodRef" />, or <see langword="TypeRef" /> token.</summary>
		// Token: 0x04002181 RID: 8577
		[__DynamicallyInvokable]
		InlineTok,
		/// <summary>The operand is a 32-bit metadata token.</summary>
		// Token: 0x04002182 RID: 8578
		[__DynamicallyInvokable]
		InlineType,
		/// <summary>The operand is 16-bit integer containing the ordinal of a local variable or an argument.</summary>
		// Token: 0x04002183 RID: 8579
		[__DynamicallyInvokable]
		InlineVar,
		/// <summary>The operand is an 8-bit integer branch target.</summary>
		// Token: 0x04002184 RID: 8580
		[__DynamicallyInvokable]
		ShortInlineBrTarget,
		/// <summary>The operand is an 8-bit integer.</summary>
		// Token: 0x04002185 RID: 8581
		[__DynamicallyInvokable]
		ShortInlineI,
		/// <summary>The operand is a 32-bit IEEE floating point number.</summary>
		// Token: 0x04002186 RID: 8582
		[__DynamicallyInvokable]
		ShortInlineR,
		/// <summary>The operand is an 8-bit integer containing the ordinal of a local variable or an argumenta.</summary>
		// Token: 0x04002187 RID: 8583
		[__DynamicallyInvokable]
		ShortInlineVar
	}
}
