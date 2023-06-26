using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Defines identifiers for supported binary operators.</summary>
	// Token: 0x02000620 RID: 1568
	[ComVisible(true)]
	[Serializable]
	public enum CodeBinaryOperatorType
	{
		/// <summary>Addition operator.</summary>
		// Token: 0x04002B7F RID: 11135
		Add,
		/// <summary>Subtraction operator.</summary>
		// Token: 0x04002B80 RID: 11136
		Subtract,
		/// <summary>Multiplication operator.</summary>
		// Token: 0x04002B81 RID: 11137
		Multiply,
		/// <summary>Division operator.</summary>
		// Token: 0x04002B82 RID: 11138
		Divide,
		/// <summary>Modulus operator.</summary>
		// Token: 0x04002B83 RID: 11139
		Modulus,
		/// <summary>Assignment operator.</summary>
		// Token: 0x04002B84 RID: 11140
		Assign,
		/// <summary>Identity not equal operator.</summary>
		// Token: 0x04002B85 RID: 11141
		IdentityInequality,
		/// <summary>Identity equal operator.</summary>
		// Token: 0x04002B86 RID: 11142
		IdentityEquality,
		/// <summary>Value equal operator.</summary>
		// Token: 0x04002B87 RID: 11143
		ValueEquality,
		/// <summary>Bitwise or operator.</summary>
		// Token: 0x04002B88 RID: 11144
		BitwiseOr,
		/// <summary>Bitwise and operator.</summary>
		// Token: 0x04002B89 RID: 11145
		BitwiseAnd,
		/// <summary>Boolean or operator. This represents a short circuiting operator. A short circuiting operator will evaluate only as many expressions as necessary before returning a correct value.</summary>
		// Token: 0x04002B8A RID: 11146
		BooleanOr,
		/// <summary>Boolean and operator. This represents a short circuiting operator. A short circuiting operator will evaluate only as many expressions as necessary before returning a correct value.</summary>
		// Token: 0x04002B8B RID: 11147
		BooleanAnd,
		/// <summary>Less than operator.</summary>
		// Token: 0x04002B8C RID: 11148
		LessThan,
		/// <summary>Less than or equal operator.</summary>
		// Token: 0x04002B8D RID: 11149
		LessThanOrEqual,
		/// <summary>Greater than operator.</summary>
		// Token: 0x04002B8E RID: 11150
		GreaterThan,
		/// <summary>Greater than or equal operator.</summary>
		// Token: 0x04002B8F RID: 11151
		GreaterThanOrEqual
	}
}
