using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a literal expression.</summary>
	// Token: 0x02000652 RID: 1618
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeSnippetExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetExpression" /> class.</summary>
		// Token: 0x06003AAC RID: 15020 RVA: 0x000F3D6C File Offset: 0x000F1F6C
		public CodeSnippetExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetExpression" /> class using the specified literal expression.</summary>
		/// <param name="value">The literal expression to represent.</param>
		// Token: 0x06003AAD RID: 15021 RVA: 0x000F3D74 File Offset: 0x000F1F74
		public CodeSnippetExpression(string value)
		{
			this.Value = value;
		}

		/// <summary>Gets or sets the literal string of code.</summary>
		/// <returns>The literal string.</returns>
		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x06003AAE RID: 15022 RVA: 0x000F3D83 File Offset: 0x000F1F83
		// (set) Token: 0x06003AAF RID: 15023 RVA: 0x000F3D99 File Offset: 0x000F1F99
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

		// Token: 0x04002C03 RID: 11267
		private string value;
	}
}
