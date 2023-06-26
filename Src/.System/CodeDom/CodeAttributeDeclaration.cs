using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.CodeDom
{
	/// <summary>Represents an attribute declaration.</summary>
	// Token: 0x0200061C RID: 1564
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeAttributeDeclaration
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> class.</summary>
		// Token: 0x06003922 RID: 14626 RVA: 0x000F1EAC File Offset: 0x000F00AC
		public CodeAttributeDeclaration()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> class using the specified name.</summary>
		/// <param name="name">The name of the attribute.</param>
		// Token: 0x06003923 RID: 14627 RVA: 0x000F1EBF File Offset: 0x000F00BF
		public CodeAttributeDeclaration(string name)
		{
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> class using the specified name and arguments.</summary>
		/// <param name="name">The name of the attribute.</param>
		/// <param name="arguments">An array of type <see cref="T:System.CodeDom.CodeAttributeArgument" /> that contains the arguments for the attribute.</param>
		// Token: 0x06003924 RID: 14628 RVA: 0x000F1ED9 File Offset: 0x000F00D9
		public CodeAttributeDeclaration(string name, params CodeAttributeArgument[] arguments)
		{
			this.Name = name;
			this.Arguments.AddRange(arguments);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> class using the specified code type reference.</summary>
		/// <param name="attributeType">The <see cref="T:System.CodeDom.CodeTypeReference" /> that identifies the attribute.</param>
		// Token: 0x06003925 RID: 14629 RVA: 0x000F1EFF File Offset: 0x000F00FF
		public CodeAttributeDeclaration(CodeTypeReference attributeType)
			: this(attributeType, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> class using the specified code type reference and arguments.</summary>
		/// <param name="attributeType">The <see cref="T:System.CodeDom.CodeTypeReference" /> that identifies the attribute.</param>
		/// <param name="arguments">An array of type <see cref="T:System.CodeDom.CodeAttributeArgument" /> that contains the arguments for the attribute.</param>
		// Token: 0x06003926 RID: 14630 RVA: 0x000F1F09 File Offset: 0x000F0109
		public CodeAttributeDeclaration(CodeTypeReference attributeType, params CodeAttributeArgument[] arguments)
		{
			this.attributeType = attributeType;
			if (attributeType != null)
			{
				this.name = attributeType.BaseType;
			}
			if (arguments != null)
			{
				this.Arguments.AddRange(arguments);
			}
		}

		/// <summary>Gets or sets the name of the attribute being declared.</summary>
		/// <returns>The name of the attribute.</returns>
		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x06003927 RID: 14631 RVA: 0x000F1F41 File Offset: 0x000F0141
		// (set) Token: 0x06003928 RID: 14632 RVA: 0x000F1F57 File Offset: 0x000F0157
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
				this.attributeType = new CodeTypeReference(this.name);
			}
		}

		/// <summary>Gets the arguments for the attribute.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeAttributeArgumentCollection" /> that contains the arguments for the attribute.</returns>
		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x06003929 RID: 14633 RVA: 0x000F1F71 File Offset: 0x000F0171
		public CodeAttributeArgumentCollection Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		/// <summary>Gets the code type reference for the code attribute declaration.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that identifies the <see cref="T:System.CodeDom.CodeAttributeDeclaration" />.</returns>
		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x0600392A RID: 14634 RVA: 0x000F1F79 File Offset: 0x000F0179
		public CodeTypeReference AttributeType
		{
			get
			{
				return this.attributeType;
			}
		}

		// Token: 0x04002B78 RID: 11128
		private string name;

		// Token: 0x04002B79 RID: 11129
		private CodeAttributeArgumentCollection arguments = new CodeAttributeArgumentCollection();

		// Token: 0x04002B7A RID: 11130
		[OptionalField]
		private CodeTypeReference attributeType;
	}
}
