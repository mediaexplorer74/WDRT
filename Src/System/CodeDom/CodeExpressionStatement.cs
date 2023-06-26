using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a statement that consists of a single expression.</summary>
	// Token: 0x02000635 RID: 1589
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeExpressionStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeExpressionStatement" /> class.</summary>
		// Token: 0x060039CC RID: 14796 RVA: 0x000F2AC4 File Offset: 0x000F0CC4
		public CodeExpressionStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeExpressionStatement" /> class by using the specified expression.</summary>
		/// <param name="expression">A <see cref="T:System.CodeDom.CodeExpression" /> for the statement.</param>
		// Token: 0x060039CD RID: 14797 RVA: 0x000F2ACC File Offset: 0x000F0CCC
		public CodeExpressionStatement(CodeExpression expression)
		{
			this.expression = expression;
		}

		/// <summary>Gets or sets the expression for the statement.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the expression for the statement.</returns>
		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x060039CE RID: 14798 RVA: 0x000F2ADB File Offset: 0x000F0CDB
		// (set) Token: 0x060039CF RID: 14799 RVA: 0x000F2AE3 File Offset: 0x000F0CE3
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

		// Token: 0x04002BAF RID: 11183
		private CodeExpression expression;
	}
}
