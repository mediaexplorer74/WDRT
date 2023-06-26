using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a namespace import directive that indicates a namespace to use.</summary>
	// Token: 0x02000645 RID: 1605
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeNamespaceImport : CodeObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeNamespaceImport" /> class.</summary>
		// Token: 0x06003A4D RID: 14925 RVA: 0x000F36A4 File Offset: 0x000F18A4
		public CodeNamespaceImport()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeNamespaceImport" /> class using the specified namespace to import.</summary>
		/// <param name="nameSpace">The name of the namespace to import.</param>
		// Token: 0x06003A4E RID: 14926 RVA: 0x000F36AC File Offset: 0x000F18AC
		public CodeNamespaceImport(string nameSpace)
		{
			this.Namespace = nameSpace;
		}

		/// <summary>Gets or sets the line and file the statement occurs on.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeLinePragma" /> that indicates the context of the statement.</returns>
		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x06003A4F RID: 14927 RVA: 0x000F36BB File Offset: 0x000F18BB
		// (set) Token: 0x06003A50 RID: 14928 RVA: 0x000F36C3 File Offset: 0x000F18C3
		public CodeLinePragma LinePragma
		{
			get
			{
				return this.linePragma;
			}
			set
			{
				this.linePragma = value;
			}
		}

		/// <summary>Gets or sets the namespace to import.</summary>
		/// <returns>The name of the namespace to import.</returns>
		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x06003A51 RID: 14929 RVA: 0x000F36CC File Offset: 0x000F18CC
		// (set) Token: 0x06003A52 RID: 14930 RVA: 0x000F36E2 File Offset: 0x000F18E2
		public string Namespace
		{
			get
			{
				if (this.nameSpace != null)
				{
					return this.nameSpace;
				}
				return string.Empty;
			}
			set
			{
				this.nameSpace = value;
			}
		}

		// Token: 0x04002BEA RID: 11242
		private string nameSpace;

		// Token: 0x04002BEB RID: 11243
		private CodeLinePragma linePragma;
	}
}
