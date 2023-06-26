using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents an expression that creates a new instance of a type.</summary>
	// Token: 0x02000648 RID: 1608
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeObjectCreateExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeObjectCreateExpression" /> class.</summary>
		// Token: 0x06003A6E RID: 14958 RVA: 0x000F3935 File Offset: 0x000F1B35
		public CodeObjectCreateExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeObjectCreateExpression" /> class using the specified type and parameters.</summary>
		/// <param name="createType">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the object to create.</param>
		/// <param name="parameters">An array of <see cref="T:System.CodeDom.CodeExpression" /> objects that indicates the parameters to use to create the object.</param>
		// Token: 0x06003A6F RID: 14959 RVA: 0x000F3948 File Offset: 0x000F1B48
		public CodeObjectCreateExpression(CodeTypeReference createType, params CodeExpression[] parameters)
		{
			this.CreateType = createType;
			this.Parameters.AddRange(parameters);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeObjectCreateExpression" /> class using the specified type and parameters.</summary>
		/// <param name="createType">The name of the data type of object to create.</param>
		/// <param name="parameters">An array of <see cref="T:System.CodeDom.CodeExpression" /> objects that indicates the parameters to use to create the object.</param>
		// Token: 0x06003A70 RID: 14960 RVA: 0x000F396E File Offset: 0x000F1B6E
		public CodeObjectCreateExpression(string createType, params CodeExpression[] parameters)
		{
			this.CreateType = new CodeTypeReference(createType);
			this.Parameters.AddRange(parameters);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeObjectCreateExpression" /> class using the specified type and parameters.</summary>
		/// <param name="createType">The data type of the object to create.</param>
		/// <param name="parameters">An array of <see cref="T:System.CodeDom.CodeExpression" /> objects that indicates the parameters to use to create the object.</param>
		// Token: 0x06003A71 RID: 14961 RVA: 0x000F3999 File Offset: 0x000F1B99
		public CodeObjectCreateExpression(Type createType, params CodeExpression[] parameters)
		{
			this.CreateType = new CodeTypeReference(createType);
			this.Parameters.AddRange(parameters);
		}

		/// <summary>Gets or sets the data type of the object to create.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> to the data type of the object to create.</returns>
		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x06003A72 RID: 14962 RVA: 0x000F39C4 File Offset: 0x000F1BC4
		// (set) Token: 0x06003A73 RID: 14963 RVA: 0x000F39E4 File Offset: 0x000F1BE4
		public CodeTypeReference CreateType
		{
			get
			{
				if (this.createType == null)
				{
					this.createType = new CodeTypeReference("");
				}
				return this.createType;
			}
			set
			{
				this.createType = value;
			}
		}

		/// <summary>Gets or sets the parameters to use in creating the object.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that indicates the parameters to use when creating the object.</returns>
		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x06003A74 RID: 14964 RVA: 0x000F39ED File Offset: 0x000F1BED
		public CodeExpressionCollection Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x04002BEF RID: 11247
		private CodeTypeReference createType;

		// Token: 0x04002BF0 RID: 11248
		private CodeExpressionCollection parameters = new CodeExpressionCollection();
	}
}
