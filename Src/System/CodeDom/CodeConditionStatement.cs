using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a conditional branch statement, typically represented as an <see langword="if" /> statement.</summary>
	// Token: 0x02000629 RID: 1577
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeConditionStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeConditionStatement" /> class.</summary>
		// Token: 0x06003987 RID: 14727 RVA: 0x000F25EF File Offset: 0x000F07EF
		public CodeConditionStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeConditionStatement" /> class using the specified condition and statements.</summary>
		/// <param name="condition">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the expression to evaluate.</param>
		/// <param name="trueStatements">An array of type <see cref="T:System.CodeDom.CodeStatement" /> containing the statements to execute if the condition is <see langword="true" />.</param>
		// Token: 0x06003988 RID: 14728 RVA: 0x000F260D File Offset: 0x000F080D
		public CodeConditionStatement(CodeExpression condition, params CodeStatement[] trueStatements)
		{
			this.Condition = condition;
			this.TrueStatements.AddRange(trueStatements);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeConditionStatement" /> class using the specified condition and statements.</summary>
		/// <param name="condition">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the condition to evaluate.</param>
		/// <param name="trueStatements">An array of type <see cref="T:System.CodeDom.CodeStatement" /> containing the statements to execute if the condition is <see langword="true" />.</param>
		/// <param name="falseStatements">An array of type <see cref="T:System.CodeDom.CodeStatement" /> containing the statements to execute if the condition is <see langword="false" />.</param>
		// Token: 0x06003989 RID: 14729 RVA: 0x000F263E File Offset: 0x000F083E
		public CodeConditionStatement(CodeExpression condition, CodeStatement[] trueStatements, CodeStatement[] falseStatements)
		{
			this.Condition = condition;
			this.TrueStatements.AddRange(trueStatements);
			this.FalseStatements.AddRange(falseStatements);
		}

		/// <summary>Gets or sets the expression to evaluate <see langword="true" /> or <see langword="false" />.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> to evaluate <see langword="true" /> or <see langword="false" />.</returns>
		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x0600398A RID: 14730 RVA: 0x000F267B File Offset: 0x000F087B
		// (set) Token: 0x0600398B RID: 14731 RVA: 0x000F2683 File Offset: 0x000F0883
		public CodeExpression Condition
		{
			get
			{
				return this.condition;
			}
			set
			{
				this.condition = value;
			}
		}

		/// <summary>Gets the collection of statements to execute if the conditional expression evaluates to <see langword="true" />.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> containing the statements to execute if the conditional expression evaluates to <see langword="true" />.</returns>
		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x0600398C RID: 14732 RVA: 0x000F268C File Offset: 0x000F088C
		public CodeStatementCollection TrueStatements
		{
			get
			{
				return this.trueStatments;
			}
		}

		/// <summary>Gets the collection of statements to execute if the conditional expression evaluates to <see langword="false" />.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> containing the statements to execute if the conditional expression evaluates to <see langword="false" />.</returns>
		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x0600398D RID: 14733 RVA: 0x000F2694 File Offset: 0x000F0894
		public CodeStatementCollection FalseStatements
		{
			get
			{
				return this.falseStatments;
			}
		}

		// Token: 0x04002BA0 RID: 11168
		private CodeExpression condition;

		// Token: 0x04002BA1 RID: 11169
		private CodeStatementCollection trueStatments = new CodeStatementCollection();

		// Token: 0x04002BA2 RID: 11170
		private CodeStatementCollection falseStatments = new CodeStatementCollection();
	}
}
