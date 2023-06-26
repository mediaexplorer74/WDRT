using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a parameter declaration for a method, property, or constructor.</summary>
	// Token: 0x02000649 RID: 1609
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeParameterDeclarationExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> class.</summary>
		// Token: 0x06003A75 RID: 14965 RVA: 0x000F39F5 File Offset: 0x000F1BF5
		public CodeParameterDeclarationExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> class using the specified parameter type and name.</summary>
		/// <param name="type">An object that indicates the type of the parameter to declare.</param>
		/// <param name="name">The name of the parameter to declare.</param>
		// Token: 0x06003A76 RID: 14966 RVA: 0x000F39FD File Offset: 0x000F1BFD
		public CodeParameterDeclarationExpression(CodeTypeReference type, string name)
		{
			this.Type = type;
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> class using the specified parameter type and name.</summary>
		/// <param name="type">The type of the parameter to declare.</param>
		/// <param name="name">The name of the parameter to declare.</param>
		// Token: 0x06003A77 RID: 14967 RVA: 0x000F3A13 File Offset: 0x000F1C13
		public CodeParameterDeclarationExpression(string type, string name)
		{
			this.Type = new CodeTypeReference(type);
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> class using the specified parameter type and name.</summary>
		/// <param name="type">The type of the parameter to declare.</param>
		/// <param name="name">The name of the parameter to declare.</param>
		// Token: 0x06003A78 RID: 14968 RVA: 0x000F3A2E File Offset: 0x000F1C2E
		public CodeParameterDeclarationExpression(Type type, string name)
		{
			this.Type = new CodeTypeReference(type);
			this.Name = name;
		}

		/// <summary>Gets or sets the custom attributes for the parameter declaration.</summary>
		/// <returns>An object that indicates the custom attributes.</returns>
		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x06003A79 RID: 14969 RVA: 0x000F3A49 File Offset: 0x000F1C49
		// (set) Token: 0x06003A7A RID: 14970 RVA: 0x000F3A64 File Offset: 0x000F1C64
		public CodeAttributeDeclarationCollection CustomAttributes
		{
			get
			{
				if (this.customAttributes == null)
				{
					this.customAttributes = new CodeAttributeDeclarationCollection();
				}
				return this.customAttributes;
			}
			set
			{
				this.customAttributes = value;
			}
		}

		/// <summary>Gets or sets the direction of the field.</summary>
		/// <returns>An object that indicates the direction of the field.</returns>
		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x06003A7B RID: 14971 RVA: 0x000F3A6D File Offset: 0x000F1C6D
		// (set) Token: 0x06003A7C RID: 14972 RVA: 0x000F3A75 File Offset: 0x000F1C75
		public FieldDirection Direction
		{
			get
			{
				return this.dir;
			}
			set
			{
				this.dir = value;
			}
		}

		/// <summary>Gets or sets the type of the parameter.</summary>
		/// <returns>The type of the parameter.</returns>
		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x06003A7D RID: 14973 RVA: 0x000F3A7E File Offset: 0x000F1C7E
		// (set) Token: 0x06003A7E RID: 14974 RVA: 0x000F3A9E File Offset: 0x000F1C9E
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

		/// <summary>Gets or sets the name of the parameter.</summary>
		/// <returns>The name of the parameter.</returns>
		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x06003A7F RID: 14975 RVA: 0x000F3AA7 File Offset: 0x000F1CA7
		// (set) Token: 0x06003A80 RID: 14976 RVA: 0x000F3ABD File Offset: 0x000F1CBD
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

		// Token: 0x04002BF1 RID: 11249
		private CodeTypeReference type;

		// Token: 0x04002BF2 RID: 11250
		private string name;

		// Token: 0x04002BF3 RID: 11251
		private CodeAttributeDeclarationCollection customAttributes;

		// Token: 0x04002BF4 RID: 11252
		private FieldDirection dir;
	}
}
