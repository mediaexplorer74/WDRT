using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a variable declaration.</summary>
	// Token: 0x02000667 RID: 1639
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeVariableDeclarationStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableDeclarationStatement" /> class.</summary>
		// Token: 0x06003B60 RID: 15200 RVA: 0x000F5154 File Offset: 0x000F3354
		public CodeVariableDeclarationStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableDeclarationStatement" /> class using the specified type and name.</summary>
		/// <param name="type">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the variable.</param>
		/// <param name="name">The name of the variable.</param>
		// Token: 0x06003B61 RID: 15201 RVA: 0x000F515C File Offset: 0x000F335C
		public CodeVariableDeclarationStatement(CodeTypeReference type, string name)
		{
			this.Type = type;
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableDeclarationStatement" /> class using the specified data type name and variable name.</summary>
		/// <param name="type">The name of the data type of the variable.</param>
		/// <param name="name">The name of the variable.</param>
		// Token: 0x06003B62 RID: 15202 RVA: 0x000F5172 File Offset: 0x000F3372
		public CodeVariableDeclarationStatement(string type, string name)
		{
			this.Type = new CodeTypeReference(type);
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableDeclarationStatement" /> class using the specified data type and variable name.</summary>
		/// <param name="type">The data type for the variable.</param>
		/// <param name="name">The name of the variable.</param>
		// Token: 0x06003B63 RID: 15203 RVA: 0x000F518D File Offset: 0x000F338D
		public CodeVariableDeclarationStatement(Type type, string name)
		{
			this.Type = new CodeTypeReference(type);
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableDeclarationStatement" /> class using the specified data type, variable name, and initialization expression.</summary>
		/// <param name="type">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the type of the variable.</param>
		/// <param name="name">The name of the variable.</param>
		/// <param name="initExpression">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the initialization expression for the variable.</param>
		// Token: 0x06003B64 RID: 15204 RVA: 0x000F51A8 File Offset: 0x000F33A8
		public CodeVariableDeclarationStatement(CodeTypeReference type, string name, CodeExpression initExpression)
		{
			this.Type = type;
			this.Name = name;
			this.InitExpression = initExpression;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableDeclarationStatement" /> class using the specified data type, variable name, and initialization expression.</summary>
		/// <param name="type">The name of the data type of the variable.</param>
		/// <param name="name">The name of the variable.</param>
		/// <param name="initExpression">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the initialization expression for the variable.</param>
		// Token: 0x06003B65 RID: 15205 RVA: 0x000F51C5 File Offset: 0x000F33C5
		public CodeVariableDeclarationStatement(string type, string name, CodeExpression initExpression)
		{
			this.Type = new CodeTypeReference(type);
			this.Name = name;
			this.InitExpression = initExpression;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableDeclarationStatement" /> class using the specified data type, variable name, and initialization expression.</summary>
		/// <param name="type">The data type of the variable.</param>
		/// <param name="name">The name of the variable.</param>
		/// <param name="initExpression">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the initialization expression for the variable.</param>
		// Token: 0x06003B66 RID: 15206 RVA: 0x000F51E7 File Offset: 0x000F33E7
		public CodeVariableDeclarationStatement(Type type, string name, CodeExpression initExpression)
		{
			this.Type = new CodeTypeReference(type);
			this.Name = name;
			this.InitExpression = initExpression;
		}

		/// <summary>Gets or sets the initialization expression for the variable.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the initialization expression for the variable.</returns>
		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x06003B67 RID: 15207 RVA: 0x000F5209 File Offset: 0x000F3409
		// (set) Token: 0x06003B68 RID: 15208 RVA: 0x000F5211 File Offset: 0x000F3411
		public CodeExpression InitExpression
		{
			get
			{
				return this.initExpression;
			}
			set
			{
				this.initExpression = value;
			}
		}

		/// <summary>Gets or sets the name of the variable.</summary>
		/// <returns>The name of the variable.</returns>
		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x06003B69 RID: 15209 RVA: 0x000F521A File Offset: 0x000F341A
		// (set) Token: 0x06003B6A RID: 15210 RVA: 0x000F5230 File Offset: 0x000F3430
		public string Name
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return string.Empty;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets the data type of the variable.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the variable.</returns>
		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x06003B6B RID: 15211 RVA: 0x000F5239 File Offset: 0x000F3439
		// (set) Token: 0x06003B6C RID: 15212 RVA: 0x000F5259 File Offset: 0x000F3459
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

		// Token: 0x04002C32 RID: 11314
		private CodeTypeReference type;

		// Token: 0x04002C33 RID: 11315
		private string name;

		// Token: 0x04002C34 RID: 11316
		private CodeExpression initExpression;
	}
}
