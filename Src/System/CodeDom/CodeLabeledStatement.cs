using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a labeled statement or a stand-alone label.</summary>
	// Token: 0x0200063A RID: 1594
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeLabeledStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeLabeledStatement" /> class.</summary>
		// Token: 0x060039E8 RID: 14824 RVA: 0x000F2C52 File Offset: 0x000F0E52
		public CodeLabeledStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeLabeledStatement" /> class using the specified label name.</summary>
		/// <param name="label">The name of the label.</param>
		// Token: 0x060039E9 RID: 14825 RVA: 0x000F2C5A File Offset: 0x000F0E5A
		public CodeLabeledStatement(string label)
		{
			this.label = label;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeLabeledStatement" /> class using the specified label name and statement.</summary>
		/// <param name="label">The name of the label.</param>
		/// <param name="statement">The <see cref="T:System.CodeDom.CodeStatement" /> to associate with the label.</param>
		// Token: 0x060039EA RID: 14826 RVA: 0x000F2C69 File Offset: 0x000F0E69
		public CodeLabeledStatement(string label, CodeStatement statement)
		{
			this.label = label;
			this.statement = statement;
		}

		/// <summary>Gets or sets the name of the label.</summary>
		/// <returns>The name of the label.</returns>
		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x060039EB RID: 14827 RVA: 0x000F2C7F File Offset: 0x000F0E7F
		// (set) Token: 0x060039EC RID: 14828 RVA: 0x000F2C95 File Offset: 0x000F0E95
		public string Label
		{
			get
			{
				if (this.label != null)
				{
					return this.label;
				}
				return string.Empty;
			}
			set
			{
				this.label = value;
			}
		}

		/// <summary>Gets or sets the optional associated statement.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatement" /> that indicates the statement associated with the label.</returns>
		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x060039ED RID: 14829 RVA: 0x000F2C9E File Offset: 0x000F0E9E
		// (set) Token: 0x060039EE RID: 14830 RVA: 0x000F2CA6 File Offset: 0x000F0EA6
		public CodeStatement Statement
		{
			get
			{
				return this.statement;
			}
			set
			{
				this.statement = value;
			}
		}

		// Token: 0x04002BB9 RID: 11193
		private string label;

		// Token: 0x04002BBA RID: 11194
		private CodeStatement statement;
	}
}
