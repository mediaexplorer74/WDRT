using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a type parameter of a generic type or method.</summary>
	// Token: 0x02000661 RID: 1633
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeTypeParameter : CodeObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeParameter" /> class.</summary>
		// Token: 0x06003B1D RID: 15133 RVA: 0x000F4778 File Offset: 0x000F2978
		public CodeTypeParameter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeParameter" /> class with the specified type parameter name.</summary>
		/// <param name="name">The name of the type parameter.</param>
		// Token: 0x06003B1E RID: 15134 RVA: 0x000F4780 File Offset: 0x000F2980
		public CodeTypeParameter(string name)
		{
			this.name = name;
		}

		/// <summary>Gets or sets the name of the type parameter.</summary>
		/// <returns>The name of the type parameter. The default is an empty string ("").</returns>
		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x06003B1F RID: 15135 RVA: 0x000F478F File Offset: 0x000F298F
		// (set) Token: 0x06003B20 RID: 15136 RVA: 0x000F47A5 File Offset: 0x000F29A5
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

		/// <summary>Gets the constraints for the type parameter.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> object that contains the constraints for the type parameter.</returns>
		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x06003B21 RID: 15137 RVA: 0x000F47AE File Offset: 0x000F29AE
		public CodeTypeReferenceCollection Constraints
		{
			get
			{
				if (this.constraints == null)
				{
					this.constraints = new CodeTypeReferenceCollection();
				}
				return this.constraints;
			}
		}

		/// <summary>Gets the custom attributes of the type parameter.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> that indicates the custom attributes of the type parameter. The default is <see langword="null" />.</returns>
		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x06003B22 RID: 15138 RVA: 0x000F47C9 File Offset: 0x000F29C9
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
		}

		/// <summary>Gets or sets a value indicating whether the type parameter has a constructor constraint.</summary>
		/// <returns>
		///   <see langword="true" /> if the type parameter has a constructor constraint; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x06003B23 RID: 15139 RVA: 0x000F47E4 File Offset: 0x000F29E4
		// (set) Token: 0x06003B24 RID: 15140 RVA: 0x000F47EC File Offset: 0x000F29EC
		public bool HasConstructorConstraint
		{
			get
			{
				return this.hasConstructorConstraint;
			}
			set
			{
				this.hasConstructorConstraint = value;
			}
		}

		// Token: 0x04002C23 RID: 11299
		private string name;

		// Token: 0x04002C24 RID: 11300
		private CodeAttributeDeclarationCollection customAttributes;

		// Token: 0x04002C25 RID: 11301
		private CodeTypeReferenceCollection constraints;

		// Token: 0x04002C26 RID: 11302
		private bool hasConstructorConstraint;
	}
}
