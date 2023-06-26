using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a statement consisting of a single comment.</summary>
	// Token: 0x02000626 RID: 1574
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeCommentStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCommentStatement" /> class.</summary>
		// Token: 0x0600396E RID: 14702 RVA: 0x000F2407 File Offset: 0x000F0607
		public CodeCommentStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCommentStatement" /> class using the specified comment.</summary>
		/// <param name="comment">A <see cref="T:System.CodeDom.CodeComment" /> that indicates the comment.</param>
		// Token: 0x0600396F RID: 14703 RVA: 0x000F240F File Offset: 0x000F060F
		public CodeCommentStatement(CodeComment comment)
		{
			this.comment = comment;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCommentStatement" /> class using the specified text as contents.</summary>
		/// <param name="text">The contents of the comment.</param>
		// Token: 0x06003970 RID: 14704 RVA: 0x000F241E File Offset: 0x000F061E
		public CodeCommentStatement(string text)
		{
			this.comment = new CodeComment(text);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCommentStatement" /> class using the specified text and documentation comment flag.</summary>
		/// <param name="text">The contents of the comment.</param>
		/// <param name="docComment">
		///   <see langword="true" /> if the comment is a documentation comment; otherwise, <see langword="false" />.</param>
		// Token: 0x06003971 RID: 14705 RVA: 0x000F2432 File Offset: 0x000F0632
		public CodeCommentStatement(string text, bool docComment)
		{
			this.comment = new CodeComment(text, docComment);
		}

		/// <summary>Gets or sets the contents of the comment.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeComment" /> that indicates the comment.</returns>
		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x06003972 RID: 14706 RVA: 0x000F2447 File Offset: 0x000F0647
		// (set) Token: 0x06003973 RID: 14707 RVA: 0x000F244F File Offset: 0x000F064F
		public CodeComment Comment
		{
			get
			{
				return this.comment;
			}
			set
			{
				this.comment = value;
			}
		}

		// Token: 0x04002B9A RID: 11162
		private CodeComment comment;
	}
}
