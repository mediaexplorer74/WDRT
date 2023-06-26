using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a return value statement.</summary>
	// Token: 0x02000642 RID: 1602
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeMethodReturnStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodReturnStatement" /> class.</summary>
		// Token: 0x06003A2E RID: 14894 RVA: 0x000F32B2 File Offset: 0x000F14B2
		public CodeMethodReturnStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodReturnStatement" /> class using the specified expression.</summary>
		/// <param name="expression">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the return value.</param>
		// Token: 0x06003A2F RID: 14895 RVA: 0x000F32BA File Offset: 0x000F14BA
		public CodeMethodReturnStatement(CodeExpression expression)
		{
			this.Expression = expression;
		}

		/// <summary>Gets or sets the return value.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the value to return for the return statement, or <see langword="null" /> if the statement is part of a subroutine.</returns>
		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x06003A30 RID: 14896 RVA: 0x000F32C9 File Offset: 0x000F14C9
		// (set) Token: 0x06003A31 RID: 14897 RVA: 0x000F32D1 File Offset: 0x000F14D1
		public CodeExpression Expression
		{
			get
			{
				return this.expression;
			}
			set
			{
				this.expression = value;
			}
		}

		// Token: 0x04002BDD RID: 11229
		private CodeExpression expression;
	}
}
