using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.CodeDom.Compiler
{
	/// <summary>Represents a compiler error or warning.</summary>
	// Token: 0x02000674 RID: 1652
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[Serializable]
	public class CompilerError
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerError" /> class.</summary>
		// Token: 0x06003CB2 RID: 15538 RVA: 0x000FA3EF File Offset: 0x000F85EF
		public CompilerError()
		{
			this.line = 0;
			this.column = 0;
			this.errorNumber = string.Empty;
			this.errorText = string.Empty;
			this.fileName = string.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerError" /> class using the specified file name, line, column, error number, and error text.</summary>
		/// <param name="fileName">The file name of the file that the compiler was compiling when it encountered the error.</param>
		/// <param name="line">The line of the source of the error.</param>
		/// <param name="column">The column of the source of the error.</param>
		/// <param name="errorNumber">The error number of the error.</param>
		/// <param name="errorText">The error message text.</param>
		// Token: 0x06003CB3 RID: 15539 RVA: 0x000FA426 File Offset: 0x000F8626
		public CompilerError(string fileName, int line, int column, string errorNumber, string errorText)
		{
			this.line = line;
			this.column = column;
			this.errorNumber = errorNumber;
			this.errorText = errorText;
			this.fileName = fileName;
		}

		/// <summary>Gets or sets the line number where the source of the error occurs.</summary>
		/// <returns>The line number of the source file where the compiler encountered the error.</returns>
		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x06003CB4 RID: 15540 RVA: 0x000FA453 File Offset: 0x000F8653
		// (set) Token: 0x06003CB5 RID: 15541 RVA: 0x000FA45B File Offset: 0x000F865B
		public int Line
		{
			get
			{
				return this.line;
			}
			set
			{
				this.line = value;
			}
		}

		/// <summary>Gets or sets the column number where the source of the error occurs.</summary>
		/// <returns>The column number of the source file where the compiler encountered the error.</returns>
		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x06003CB6 RID: 15542 RVA: 0x000FA464 File Offset: 0x000F8664
		// (set) Token: 0x06003CB7 RID: 15543 RVA: 0x000FA46C File Offset: 0x000F866C
		public int Column
		{
			get
			{
				return this.column;
			}
			set
			{
				this.column = value;
			}
		}

		/// <summary>Gets or sets the error number.</summary>
		/// <returns>The error number as a string.</returns>
		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x06003CB8 RID: 15544 RVA: 0x000FA475 File Offset: 0x000F8675
		// (set) Token: 0x06003CB9 RID: 15545 RVA: 0x000FA47D File Offset: 0x000F867D
		public string ErrorNumber
		{
			get
			{
				return this.errorNumber;
			}
			set
			{
				this.errorNumber = value;
			}
		}

		/// <summary>Gets or sets the text of the error message.</summary>
		/// <returns>The text of the error message.</returns>
		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x06003CBA RID: 15546 RVA: 0x000FA486 File Offset: 0x000F8686
		// (set) Token: 0x06003CBB RID: 15547 RVA: 0x000FA48E File Offset: 0x000F868E
		public string ErrorText
		{
			get
			{
				return this.errorText;
			}
			set
			{
				this.errorText = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the error is a warning.</summary>
		/// <returns>
		///   <see langword="true" /> if the error is a warning; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x06003CBC RID: 15548 RVA: 0x000FA497 File Offset: 0x000F8697
		// (set) Token: 0x06003CBD RID: 15549 RVA: 0x000FA49F File Offset: 0x000F869F
		public bool IsWarning
		{
			get
			{
				return this.warning;
			}
			set
			{
				this.warning = value;
			}
		}

		/// <summary>Gets or sets the file name of the source file that contains the code which caused the error.</summary>
		/// <returns>The file name of the source file that contains the code which caused the error.</returns>
		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x06003CBE RID: 15550 RVA: 0x000FA4A8 File Offset: 0x000F86A8
		// (set) Token: 0x06003CBF RID: 15551 RVA: 0x000FA4B0 File Offset: 0x000F86B0
		public string FileName
		{
			get
			{
				return this.fileName;
			}
			set
			{
				this.fileName = value;
			}
		}

		/// <summary>Provides an implementation of Object's <see cref="M:System.Object.ToString" /> method.</summary>
		/// <returns>A string representation of the compiler error.</returns>
		// Token: 0x06003CC0 RID: 15552 RVA: 0x000FA4BC File Offset: 0x000F86BC
		public override string ToString()
		{
			if (this.FileName.Length > 0)
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}({1},{2}) : {3} {4}: {5}", new object[]
				{
					this.FileName,
					this.Line,
					this.Column,
					this.IsWarning ? "warning" : "error",
					this.ErrorNumber,
					this.ErrorText
				});
			}
			return string.Format(CultureInfo.InvariantCulture, "{0} {1}: {2}", new object[]
			{
				this.IsWarning ? "warning" : "error",
				this.ErrorNumber,
				this.ErrorText
			});
		}

		// Token: 0x04002C5A RID: 11354
		private int line;

		// Token: 0x04002C5B RID: 11355
		private int column;

		// Token: 0x04002C5C RID: 11356
		private string errorNumber;

		// Token: 0x04002C5D RID: 11357
		private bool warning;

		// Token: 0x04002C5E RID: 11358
		private string errorText;

		// Token: 0x04002C5F RID: 11359
		private string fileName;
	}
}
