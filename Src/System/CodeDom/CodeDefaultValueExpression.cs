using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a reference to a default value.</summary>
	// Token: 0x0200062B RID: 1579
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeDefaultValueExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDefaultValueExpression" /> class.</summary>
		// Token: 0x06003991 RID: 14737 RVA: 0x000F26D5 File Offset: 0x000F08D5
		public CodeDefaultValueExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDefaultValueExpression" /> class using the specified code type reference.</summary>
		/// <param name="type">A <see cref="T:System.CodeDom.CodeTypeReference" /> that specifies the reference to a value type.</param>
		// Token: 0x06003992 RID: 14738 RVA: 0x000F26DD File Offset: 0x000F08DD
		public CodeDefaultValueExpression(CodeTypeReference type)
		{
			this.type = type;
		}

		/// <summary>Gets or sets the data type reference for a default value.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> object representing a data type that has a default value.</returns>
		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x06003993 RID: 14739 RVA: 0x000F26EC File Offset: 0x000F08EC
		// (set) Token: 0x06003994 RID: 14740 RVA: 0x000F270C File Offset: 0x000F090C
		public CodeTypeReference Type
		{
			get
			{
				if (this.type == null)
				{
					this.type = new CodeTypeReference("");
				}
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x04002BA5 RID: 11173
		private CodeTypeReference type;
	}
}
