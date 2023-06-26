using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a statement using a literal code fragment.</summary>
	// Token: 0x02000653 RID: 1619
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeSnippetStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetStatement" /> class.</summary>
		// Token: 0x06003AB0 RID: 15024 RVA: 0x000F3DA2 File Offset: 0x000F1FA2
		public CodeSnippetStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetStatement" /> class using the specified code fragment.</summary>
		/// <param name="value">The literal code fragment of the statement to represent.</param>
		// Token: 0x06003AB1 RID: 15025 RVA: 0x000F3DAA File Offset: 0x000F1FAA
		public CodeSnippetStatement(string value)
		{
			this.Value = value;
		}

		/// <summary>Gets or sets the literal code fragment statement.</summary>
		/// <returns>The literal code fragment statement.</returns>
		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x06003AB2 RID: 15026 RVA: 0x000F3DB9 File Offset: 0x000F1FB9
		// (set) Token: 0x06003AB3 RID: 15027 RVA: 0x000F3DCF File Offset: 0x000F1FCF
		public string Value
		{
			get
			{
				if (this.value != null)
				{
					return this.value;
				}
				return string.Empty;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x04002C04 RID: 11268
		private string value;
	}
}
