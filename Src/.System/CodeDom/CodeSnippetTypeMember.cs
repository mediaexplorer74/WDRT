using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a member of a type using a literal code fragment.</summary>
	// Token: 0x02000654 RID: 1620
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeSnippetTypeMember : CodeTypeMember
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetTypeMember" /> class.</summary>
		// Token: 0x06003AB4 RID: 15028 RVA: 0x000F3DD8 File Offset: 0x000F1FD8
		public CodeSnippetTypeMember()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetTypeMember" /> class using the specified text.</summary>
		/// <param name="text">The literal code fragment for the type member.</param>
		// Token: 0x06003AB5 RID: 15029 RVA: 0x000F3DE0 File Offset: 0x000F1FE0
		public CodeSnippetTypeMember(string text)
		{
			this.Text = text;
		}

		/// <summary>Gets or sets the literal code fragment for the type member.</summary>
		/// <returns>The literal code fragment for the type member.</returns>
		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x06003AB6 RID: 15030 RVA: 0x000F3DEF File Offset: 0x000F1FEF
		// (set) Token: 0x06003AB7 RID: 15031 RVA: 0x000F3E05 File Offset: 0x000F2005
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

		// Token: 0x04002C05 RID: 11269
		private string text;
	}
}
