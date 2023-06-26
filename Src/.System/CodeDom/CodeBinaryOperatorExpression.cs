using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents an expression that consists of a binary operation between two expressions.</summary>
	// Token: 0x0200061F RID: 1567
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeBinaryOperatorExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeBinaryOperatorExpression" /> class.</summary>
		// Token: 0x06003939 RID: 14649 RVA: 0x000F2098 File Offset: 0x000F0298
		public CodeBinaryOperatorExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeBinaryOperatorExpression" /> class using the specified parameters.</summary>
		/// <param name="left">The <see cref="T:System.CodeDom.CodeExpression" /> on the left of the operator.</param>
		/// <param name="op">A <see cref="T:System.CodeDom.CodeBinaryOperatorType" /> indicating the type of operator.</param>
		/// <param name="right">The <see cref="T:System.CodeDom.CodeExpression" /> on the right of the operator.</param>
		// Token: 0x0600393A RID: 14650 RVA: 0x000F20A0 File Offset: 0x000F02A0
		public CodeBinaryOperatorExpression(CodeExpression left, CodeBinaryOperatorType op, CodeExpression right)
		{
			this.Right = right;
			this.Operator = op;
			this.Left = left;
		}

		/// <summary>Gets or sets the code expression on the right of the operator.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the right operand.</returns>
		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x0600393B RID: 14651 RVA: 0x000F20BD File Offset: 0x000F02BD
		// (set) Token: 0x0600393C RID: 14652 RVA: 0x000F20C5 File Offset: 0x000F02C5
		public CodeExpression Right
		{
			get
			{
				return this.right;
			}
			set
			{
				this.right = value;
			}
		}

		/// <summary>Gets or sets the code expression on the left of the operator.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the left operand.</returns>
		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x0600393D RID: 14653 RVA: 0x000F20CE File Offset: 0x000F02CE
		// (set) Token: 0x0600393E RID: 14654 RVA: 0x000F20D6 File Offset: 0x000F02D6
		public CodeExpression Left
		{
			get
			{
				return this.left;
			}
			set
			{
				this.left = value;
			}
		}

		/// <summary>Gets or sets the operator in the binary operator expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeBinaryOperatorType" /> that indicates the type of operator in the expression.</returns>
		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x0600393F RID: 14655 RVA: 0x000F20DF File Offset: 0x000F02DF
		// (set) Token: 0x06003940 RID: 14656 RVA: 0x000F20E7 File Offset: 0x000F02E7
		public CodeBinaryOperatorType Operator
		{
			get
			{
				return this.op;
			}
			set
			{
				this.op = value;
			}
		}

		// Token: 0x04002B7B RID: 11131
		private CodeBinaryOperatorType op;

		// Token: 0x04002B7C RID: 11132
		private CodeExpression left;

		// Token: 0x04002B7D RID: 11133
		private CodeExpression right;
	}
}
