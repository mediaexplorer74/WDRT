using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a <see langword="try" /> block with any number of <see langword="catch" /> clauses and, optionally, a <see langword="finally" /> block.</summary>
	// Token: 0x02000659 RID: 1625
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeTryCatchFinallyStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTryCatchFinallyStatement" /> class.</summary>
		// Token: 0x06003AD0 RID: 15056 RVA: 0x000F3FAC File Offset: 0x000F21AC
		public CodeTryCatchFinallyStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTryCatchFinallyStatement" /> class using the specified statements for try and catch clauses.</summary>
		/// <param name="tryStatements">An array of <see cref="T:System.CodeDom.CodeStatement" /> objects that indicate the statements to try.</param>
		/// <param name="catchClauses">An array of <see cref="T:System.CodeDom.CodeCatchClause" /> objects that indicate the clauses to catch.</param>
		// Token: 0x06003AD1 RID: 15057 RVA: 0x000F3FD8 File Offset: 0x000F21D8
		public CodeTryCatchFinallyStatement(CodeStatement[] tryStatements, CodeCatchClause[] catchClauses)
		{
			this.TryStatements.AddRange(tryStatements);
			this.CatchClauses.AddRange(catchClauses);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTryCatchFinallyStatement" /> class using the specified statements for try, catch clauses, and finally statements.</summary>
		/// <param name="tryStatements">An array of <see cref="T:System.CodeDom.CodeStatement" /> objects that indicate the statements to try.</param>
		/// <param name="catchClauses">An array of <see cref="T:System.CodeDom.CodeCatchClause" /> objects that indicate the clauses to catch.</param>
		/// <param name="finallyStatements">An array of <see cref="T:System.CodeDom.CodeStatement" /> objects that indicate the finally statements to use.</param>
		// Token: 0x06003AD2 RID: 15058 RVA: 0x000F4024 File Offset: 0x000F2224
		public CodeTryCatchFinallyStatement(CodeStatement[] tryStatements, CodeCatchClause[] catchClauses, CodeStatement[] finallyStatements)
		{
			this.TryStatements.AddRange(tryStatements);
			this.CatchClauses.AddRange(catchClauses);
			this.FinallyStatements.AddRange(finallyStatements);
		}

		/// <summary>Gets the statements to try.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> that indicates the statements to try.</returns>
		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x06003AD3 RID: 15059 RVA: 0x000F407C File Offset: 0x000F227C
		public CodeStatementCollection TryStatements
		{
			get
			{
				return this.tryStatments;
			}
		}

		/// <summary>Gets the catch clauses to use.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeCatchClauseCollection" /> that indicates the catch clauses to use.</returns>
		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x06003AD4 RID: 15060 RVA: 0x000F4084 File Offset: 0x000F2284
		public CodeCatchClauseCollection CatchClauses
		{
			get
			{
				return this.catchClauses;
			}
		}

		/// <summary>Gets the finally statements to use.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> that indicates the finally statements.</returns>
		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x06003AD5 RID: 15061 RVA: 0x000F408C File Offset: 0x000F228C
		public CodeStatementCollection FinallyStatements
		{
			get
			{
				return this.finallyStatments;
			}
		}

		// Token: 0x04002C0A RID: 11274
		private CodeStatementCollection tryStatments = new CodeStatementCollection();

		// Token: 0x04002C0B RID: 11275
		private CodeStatementCollection finallyStatments = new CodeStatementCollection();

		// Token: 0x04002C0C RID: 11276
		private CodeCatchClauseCollection catchClauses = new CodeCatchClauseCollection();
	}
}
