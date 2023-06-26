using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a <see langword="typeof" /> expression, an expression that returns a <see cref="T:System.Type" /> for a specified type name.</summary>
	// Token: 0x02000660 RID: 1632
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeTypeOfExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeOfExpression" /> class.</summary>
		// Token: 0x06003B17 RID: 15127 RVA: 0x000F4710 File Offset: 0x000F2910
		public CodeTypeOfExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeOfExpression" /> class.</summary>
		/// <param name="type">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type for the <see langword="typeof" /> expression.</param>
		// Token: 0x06003B18 RID: 15128 RVA: 0x000F4718 File Offset: 0x000F2918
		public CodeTypeOfExpression(CodeTypeReference type)
		{
			this.Type = type;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeOfExpression" /> class using the specified type.</summary>
		/// <param name="type">The name of the data type for the <see langword="typeof" /> expression.</param>
		// Token: 0x06003B19 RID: 15129 RVA: 0x000F4727 File Offset: 0x000F2927
		public CodeTypeOfExpression(string type)
		{
			this.Type = new CodeTypeReference(type);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeOfExpression" /> class using the specified type.</summary>
		/// <param name="type">The data type of the data type of the <see langword="typeof" /> expression.</param>
		// Token: 0x06003B1A RID: 15130 RVA: 0x000F473B File Offset: 0x000F293B
		public CodeTypeOfExpression(Type type)
		{
			this.Type = new CodeTypeReference(type);
		}

		/// <summary>Gets or sets the data type referenced by the <see langword="typeof" /> expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type referenced by the <see langword="typeof" /> expression. This property will never return <see langword="null" />, and defaults to the <see cref="T:System.Void" /> type.</returns>
		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x06003B1B RID: 15131 RVA: 0x000F474F File Offset: 0x000F294F
		// (set) Token: 0x06003B1C RID: 15132 RVA: 0x000F476F File Offset: 0x000F296F
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

		// Token: 0x04002C22 RID: 11298
		private CodeTypeReference type;
	}
}
