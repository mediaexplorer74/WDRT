using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a literal code fragment that can be compiled.</summary>
	// Token: 0x02000651 RID: 1617
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeSnippetCompileUnit : CodeCompileUnit
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetCompileUnit" /> class.</summary>
		// Token: 0x06003AA6 RID: 15014 RVA: 0x000F3D25 File Offset: 0x000F1F25
		public CodeSnippetCompileUnit()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetCompileUnit" /> class.</summary>
		/// <param name="value">The literal code fragment to represent.</param>
		// Token: 0x06003AA7 RID: 15015 RVA: 0x000F3D2D File Offset: 0x000F1F2D
		public CodeSnippetCompileUnit(string value)
		{
			this.Value = value;
		}

		/// <summary>Gets or sets the literal code fragment to represent.</summary>
		/// <returns>The literal code fragment.</returns>
		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x06003AA8 RID: 15016 RVA: 0x000F3D3C File Offset: 0x000F1F3C
		// (set) Token: 0x06003AA9 RID: 15017 RVA: 0x000F3D52 File Offset: 0x000F1F52
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

		/// <summary>Gets or sets the line and file information about where the code is located in a source code document.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeLinePragma" /> that indicates the position of the code fragment.</returns>
		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x06003AAA RID: 15018 RVA: 0x000F3D5B File Offset: 0x000F1F5B
		// (set) Token: 0x06003AAB RID: 15019 RVA: 0x000F3D63 File Offset: 0x000F1F63
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

		// Token: 0x04002C01 RID: 11265
		private string value;

		// Token: 0x04002C02 RID: 11266
		private CodeLinePragma linePragma;
	}
}
