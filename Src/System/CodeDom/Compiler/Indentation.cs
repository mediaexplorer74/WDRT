using System;
using System.Text;

namespace System.CodeDom.Compiler
{
	// Token: 0x02000681 RID: 1665
	internal class Indentation
	{
		// Token: 0x06003D5E RID: 15710 RVA: 0x000FB807 File Offset: 0x000F9A07
		internal Indentation(IndentedTextWriter writer, int indent)
		{
			this.writer = writer;
			this.indent = indent;
			this.s = null;
		}

		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x06003D5F RID: 15711 RVA: 0x000FB824 File Offset: 0x000F9A24
		internal string IndentationString
		{
			get
			{
				if (this.s == null)
				{
					string tabString = this.writer.TabString;
					StringBuilder stringBuilder = new StringBuilder(this.indent * tabString.Length);
					for (int i = 0; i < this.indent; i++)
					{
						stringBuilder.Append(tabString);
					}
					this.s = stringBuilder.ToString();
				}
				return this.s;
			}
		}

		// Token: 0x04002CA4 RID: 11428
		private IndentedTextWriter writer;

		// Token: 0x04002CA5 RID: 11429
		private int indent;

		// Token: 0x04002CA6 RID: 11430
		private string s;
	}
}
