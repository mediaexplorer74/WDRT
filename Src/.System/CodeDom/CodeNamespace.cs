using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.CodeDom
{
	/// <summary>Represents a namespace declaration.</summary>
	// Token: 0x02000643 RID: 1603
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeNamespace : CodeObject
	{
		/// <summary>An event that will be raised the first time the <see cref="P:System.CodeDom.CodeNamespace.Comments" /> collection is accessed.</summary>
		// Token: 0x1400006D RID: 109
		// (add) Token: 0x06003A32 RID: 14898 RVA: 0x000F32DC File Offset: 0x000F14DC
		// (remove) Token: 0x06003A33 RID: 14899 RVA: 0x000F3314 File Offset: 0x000F1514
		public event EventHandler PopulateComments;

		/// <summary>An event that will be raised the first time the <see cref="P:System.CodeDom.CodeNamespace.Imports" /> collection is accessed.</summary>
		// Token: 0x1400006E RID: 110
		// (add) Token: 0x06003A34 RID: 14900 RVA: 0x000F334C File Offset: 0x000F154C
		// (remove) Token: 0x06003A35 RID: 14901 RVA: 0x000F3384 File Offset: 0x000F1584
		public event EventHandler PopulateImports;

		/// <summary>An event that will be raised the first time the <see cref="P:System.CodeDom.CodeNamespace.Types" /> collection is accessed.</summary>
		// Token: 0x1400006F RID: 111
		// (add) Token: 0x06003A36 RID: 14902 RVA: 0x000F33BC File Offset: 0x000F15BC
		// (remove) Token: 0x06003A37 RID: 14903 RVA: 0x000F33F4 File Offset: 0x000F15F4
		public event EventHandler PopulateTypes;

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeNamespace" /> class.</summary>
		// Token: 0x06003A38 RID: 14904 RVA: 0x000F3429 File Offset: 0x000F1629
		public CodeNamespace()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeNamespace" /> class using the specified name.</summary>
		/// <param name="name">The name of the namespace being declared.</param>
		// Token: 0x06003A39 RID: 14905 RVA: 0x000F345D File Offset: 0x000F165D
		public CodeNamespace(string name)
		{
			this.Name = name;
		}

		// Token: 0x06003A3A RID: 14906 RVA: 0x000F3498 File Offset: 0x000F1698
		private CodeNamespace(SerializationInfo info, StreamingContext context)
		{
		}

		/// <summary>Gets the collection of types that the namespace contains.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeDeclarationCollection" /> that indicates the types contained in the namespace.</returns>
		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x06003A3B RID: 14907 RVA: 0x000F34CC File Offset: 0x000F16CC
		public CodeTypeDeclarationCollection Types
		{
			get
			{
				if ((this.populated & 4) == 0)
				{
					this.populated |= 4;
					if (this.PopulateTypes != null)
					{
						this.PopulateTypes(this, EventArgs.Empty);
					}
				}
				return this.classes;
			}
		}

		/// <summary>Gets the collection of namespace import directives used by the namespace.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeNamespaceImportCollection" /> that indicates the namespace import directives used by the namespace.</returns>
		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x06003A3C RID: 14908 RVA: 0x000F3505 File Offset: 0x000F1705
		public CodeNamespaceImportCollection Imports
		{
			get
			{
				if ((this.populated & 1) == 0)
				{
					this.populated |= 1;
					if (this.PopulateImports != null)
					{
						this.PopulateImports(this, EventArgs.Empty);
					}
				}
				return this.imports;
			}
		}

		/// <summary>Gets or sets the name of the namespace.</summary>
		/// <returns>The name of the namespace.</returns>
		// Token: 0x17000DFE RID: 3582
		// (get) Token: 0x06003A3D RID: 14909 RVA: 0x000F353E File Offset: 0x000F173E
		// (set) Token: 0x06003A3E RID: 14910 RVA: 0x000F3554 File Offset: 0x000F1754
		public string Name
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return string.Empty;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets the comments for the namespace.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeCommentStatementCollection" /> that indicates the comments for the namespace.</returns>
		// Token: 0x17000DFF RID: 3583
		// (get) Token: 0x06003A3F RID: 14911 RVA: 0x000F355D File Offset: 0x000F175D
		public CodeCommentStatementCollection Comments
		{
			get
			{
				if ((this.populated & 2) == 0)
				{
					this.populated |= 2;
					if (this.PopulateComments != null)
					{
						this.PopulateComments(this, EventArgs.Empty);
					}
				}
				return this.comments;
			}
		}

		// Token: 0x04002BDE RID: 11230
		private string name;

		// Token: 0x04002BDF RID: 11231
		private CodeNamespaceImportCollection imports = new CodeNamespaceImportCollection();

		// Token: 0x04002BE0 RID: 11232
		private CodeCommentStatementCollection comments = new CodeCommentStatementCollection();

		// Token: 0x04002BE1 RID: 11233
		private CodeTypeDeclarationCollection classes = new CodeTypeDeclarationCollection();

		// Token: 0x04002BE2 RID: 11234
		private CodeNamespaceCollection namespaces = new CodeNamespaceCollection();

		// Token: 0x04002BE3 RID: 11235
		private int populated;

		// Token: 0x04002BE4 RID: 11236
		private const int ImportsCollection = 1;

		// Token: 0x04002BE5 RID: 11237
		private const int CommentsCollection = 2;

		// Token: 0x04002BE6 RID: 11238
		private const int TypesCollection = 4;
	}
}
