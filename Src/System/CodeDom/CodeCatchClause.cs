using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a <see langword="catch" /> exception block of a <see langword="try/catch" /> statement.</summary>
	// Token: 0x02000622 RID: 1570
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeCatchClause
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCatchClause" /> class.</summary>
		// Token: 0x06003949 RID: 14665 RVA: 0x000F217E File Offset: 0x000F037E
		public CodeCatchClause()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCatchClause" /> class using the specified local variable name for the exception.</summary>
		/// <param name="localName">The name of the local variable declared in the catch clause for the exception. This is optional.</param>
		// Token: 0x0600394A RID: 14666 RVA: 0x000F2186 File Offset: 0x000F0386
		public CodeCatchClause(string localName)
		{
			this.localName = localName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCatchClause" /> class using the specified local variable name for the exception and exception type.</summary>
		/// <param name="localName">The name of the local variable declared in the catch clause for the exception. This is optional.</param>
		/// <param name="catchExceptionType">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the type of exception to catch.</param>
		// Token: 0x0600394B RID: 14667 RVA: 0x000F2195 File Offset: 0x000F0395
		public CodeCatchClause(string localName, CodeTypeReference catchExceptionType)
		{
			this.localName = localName;
			this.catchExceptionType = catchExceptionType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCatchClause" /> class using the specified local variable name for the exception, exception type and statement collection.</summary>
		/// <param name="localName">The name of the local variable declared in the catch clause for the exception. This is optional.</param>
		/// <param name="catchExceptionType">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the type of exception to catch.</param>
		/// <param name="statements">An array of <see cref="T:System.CodeDom.CodeStatement" /> objects that represent the contents of the catch block.</param>
		// Token: 0x0600394C RID: 14668 RVA: 0x000F21AB File Offset: 0x000F03AB
		public CodeCatchClause(string localName, CodeTypeReference catchExceptionType, params CodeStatement[] statements)
		{
			this.localName = localName;
			this.catchExceptionType = catchExceptionType;
			this.Statements.AddRange(statements);
		}

		/// <summary>Gets or sets the variable name of the exception that the <see langword="catch" /> clause handles.</summary>
		/// <returns>The name for the exception variable that the <see langword="catch" /> clause handles.</returns>
		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x0600394D RID: 14669 RVA: 0x000F21CD File Offset: 0x000F03CD
		// (set) Token: 0x0600394E RID: 14670 RVA: 0x000F21E3 File Offset: 0x000F03E3
		public string LocalName
		{
			get
			{
				if (this.localName != null)
				{
					return this.localName;
				}
				return string.Empty;
			}
			set
			{
				this.localName = value;
			}
		}

		/// <summary>Gets or sets the type of the exception to handle with the catch block.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the type of the exception to handle.</returns>
		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x0600394F RID: 14671 RVA: 0x000F21EC File Offset: 0x000F03EC
		// (set) Token: 0x06003950 RID: 14672 RVA: 0x000F2211 File Offset: 0x000F0411
		public CodeTypeReference CatchExceptionType
		{
			get
			{
				if (this.catchExceptionType == null)
				{
					this.catchExceptionType = new CodeTypeReference(typeof(Exception));
				}
				return this.catchExceptionType;
			}
			set
			{
				this.catchExceptionType = value;
			}
		}

		/// <summary>Gets the statements within the catch block.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> containing the statements within the catch block.</returns>
		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x06003951 RID: 14673 RVA: 0x000F221A File Offset: 0x000F041A
		public CodeStatementCollection Statements
		{
			get
			{
				if (this.statements == null)
				{
					this.statements = new CodeStatementCollection();
				}
				return this.statements;
			}
		}

		// Token: 0x04002B92 RID: 11154
		private CodeStatementCollection statements;

		// Token: 0x04002B93 RID: 11155
		private CodeTypeReference catchExceptionType;

		// Token: 0x04002B94 RID: 11156
		private string localName;
	}
}
