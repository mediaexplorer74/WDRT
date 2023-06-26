using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a <see langword="for" /> statement, or a loop through a block of statements, using a test expression as a condition for continuing to loop.</summary>
	// Token: 0x02000639 RID: 1593
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeIterationStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeIterationStatement" /> class.</summary>
		// Token: 0x060039DF RID: 14815 RVA: 0x000F2BCF File Offset: 0x000F0DCF
		public CodeIterationStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeIterationStatement" /> class using the specified parameters.</summary>
		/// <param name="initStatement">A <see cref="T:System.CodeDom.CodeStatement" /> containing the loop initialization statement.</param>
		/// <param name="testExpression">A <see cref="T:System.CodeDom.CodeExpression" /> containing the expression to test for exit condition.</param>
		/// <param name="incrementStatement">A <see cref="T:System.CodeDom.CodeStatement" /> containing the per-cycle increment statement.</param>
		/// <param name="statements">An array of type <see cref="T:System.CodeDom.CodeStatement" /> containing the statements within the loop.</param>
		// Token: 0x060039E0 RID: 14816 RVA: 0x000F2BE2 File Offset: 0x000F0DE2
		public CodeIterationStatement(CodeStatement initStatement, CodeExpression testExpression, CodeStatement incrementStatement, params CodeStatement[] statements)
		{
			this.InitStatement = initStatement;
			this.TestExpression = testExpression;
			this.IncrementStatement = incrementStatement;
			this.Statements.AddRange(statements);
		}

		/// <summary>Gets or sets the loop initialization statement.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatement" /> that indicates the loop initialization statement.</returns>
		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x060039E1 RID: 14817 RVA: 0x000F2C17 File Offset: 0x000F0E17
		// (set) Token: 0x060039E2 RID: 14818 RVA: 0x000F2C1F File Offset: 0x000F0E1F
		public CodeStatement InitStatement
		{
			get
			{
				return this.initStatement;
			}
			set
			{
				this.initStatement = value;
			}
		}

		/// <summary>Gets or sets the expression to test as the condition that continues the loop.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the expression to test.</returns>
		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x060039E3 RID: 14819 RVA: 0x000F2C28 File Offset: 0x000F0E28
		// (set) Token: 0x060039E4 RID: 14820 RVA: 0x000F2C30 File Offset: 0x000F0E30
		public CodeExpression TestExpression
		{
			get
			{
				return this.testExpression;
			}
			set
			{
				this.testExpression = value;
			}
		}

		/// <summary>Gets or sets the statement that is called after each loop cycle.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatement" /> that indicates the per cycle increment statement.</returns>
		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x060039E5 RID: 14821 RVA: 0x000F2C39 File Offset: 0x000F0E39
		// (set) Token: 0x060039E6 RID: 14822 RVA: 0x000F2C41 File Offset: 0x000F0E41
		public CodeStatement IncrementStatement
		{
			get
			{
				return this.incrementStatement;
			}
			set
			{
				this.incrementStatement = value;
			}
		}

		/// <summary>Gets the collection of statements to be executed within the loop.</summary>
		/// <returns>An array of type <see cref="T:System.CodeDom.CodeStatement" /> that indicates the statements within the loop.</returns>
		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x060039E7 RID: 14823 RVA: 0x000F2C4A File Offset: 0x000F0E4A
		public CodeStatementCollection Statements
		{
			get
			{
				return this.statements;
			}
		}

		// Token: 0x04002BB5 RID: 11189
		private CodeStatement initStatement;

		// Token: 0x04002BB6 RID: 11190
		private CodeExpression testExpression;

		// Token: 0x04002BB7 RID: 11191
		private CodeStatement incrementStatement;

		// Token: 0x04002BB8 RID: 11192
		private CodeStatementCollection statements = new CodeStatementCollection();
	}
}
