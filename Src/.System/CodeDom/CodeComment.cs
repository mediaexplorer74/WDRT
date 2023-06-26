using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a comment.</summary>
	// Token: 0x02000625 RID: 1573
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeComment : CodeObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeComment" /> class.</summary>
		// Token: 0x06003967 RID: 14695 RVA: 0x000F23AA File Offset: 0x000F05AA
		public CodeComment()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeComment" /> class with the specified text as contents.</summary>
		/// <param name="text">The contents of the comment.</param>
		// Token: 0x06003968 RID: 14696 RVA: 0x000F23B2 File Offset: 0x000F05B2
		public CodeComment(string text)
		{
			this.Text = text;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeComment" /> class using the specified text and documentation comment flag.</summary>
		/// <param name="text">The contents of the comment.</param>
		/// <param name="docComment">
		///   <see langword="true" /> if the comment is a documentation comment; otherwise, <see langword="false" />.</param>
		// Token: 0x06003969 RID: 14697 RVA: 0x000F23C1 File Offset: 0x000F05C1
		public CodeComment(string text, bool docComment)
		{
			this.Text = text;
			this.docComment = docComment;
		}

		/// <summary>Gets or sets a value that indicates whether the comment is a documentation comment.</summary>
		/// <returns>
		///   <see langword="true" /> if the comment is a documentation comment; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x0600396A RID: 14698 RVA: 0x000F23D7 File Offset: 0x000F05D7
		// (set) Token: 0x0600396B RID: 14699 RVA: 0x000F23DF File Offset: 0x000F05DF
		public bool DocComment
		{
			get
			{
				return this.docComment;
			}
			set
			{
				this.docComment = value;
			}
		}

		/// <summary>Gets or sets the text of the comment.</summary>
		/// <returns>A string containing the comment text.</returns>
		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x0600396C RID: 14700 RVA: 0x000F23E8 File Offset: 0x000F05E8
		// (set) Token: 0x0600396D RID: 14701 RVA: 0x000F23FE File Offset: 0x000F05FE
		public string Text
		{
			get
			{
				if (this.text != null)
				{
					return this.text;
				}
				return string.Empty;
			}
			set
			{
				this.text = value;
			}
		}

		// Token: 0x04002B98 RID: 11160
		private string text;

		// Token: 0x04002B99 RID: 11161
		private bool docComment;
	}
}
