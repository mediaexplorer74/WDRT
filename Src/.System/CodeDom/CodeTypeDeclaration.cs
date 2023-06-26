using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.CodeDom
{
	/// <summary>Represents a type declaration for a class, structure, interface, or enumeration.</summary>
	// Token: 0x0200065B RID: 1627
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeTypeDeclaration : CodeTypeMember
	{
		/// <summary>Occurs when the <see cref="P:System.CodeDom.CodeTypeDeclaration.BaseTypes" /> collection is accessed for the first time.</summary>
		// Token: 0x14000070 RID: 112
		// (add) Token: 0x06003AD7 RID: 15063 RVA: 0x000F40A8 File Offset: 0x000F22A8
		// (remove) Token: 0x06003AD8 RID: 15064 RVA: 0x000F40E0 File Offset: 0x000F22E0
		public event EventHandler PopulateBaseTypes;

		/// <summary>Occurs when the <see cref="P:System.CodeDom.CodeTypeDeclaration.Members" /> collection is accessed for the first time.</summary>
		// Token: 0x14000071 RID: 113
		// (add) Token: 0x06003AD9 RID: 15065 RVA: 0x000F4118 File Offset: 0x000F2318
		// (remove) Token: 0x06003ADA RID: 15066 RVA: 0x000F4150 File Offset: 0x000F2350
		public event EventHandler PopulateMembers;

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeDeclaration" /> class.</summary>
		// Token: 0x06003ADB RID: 15067 RVA: 0x000F4185 File Offset: 0x000F2385
		public CodeTypeDeclaration()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeDeclaration" /> class with the specified name.</summary>
		/// <param name="name">The name for the new type.</param>
		// Token: 0x06003ADC RID: 15068 RVA: 0x000F41AA File Offset: 0x000F23AA
		public CodeTypeDeclaration(string name)
		{
			base.Name = name;
		}

		/// <summary>Gets or sets the attributes of the type.</summary>
		/// <returns>A <see cref="T:System.Reflection.TypeAttributes" /> object that indicates the attributes of the type.</returns>
		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x06003ADD RID: 15069 RVA: 0x000F41D6 File Offset: 0x000F23D6
		// (set) Token: 0x06003ADE RID: 15070 RVA: 0x000F41DE File Offset: 0x000F23DE
		public TypeAttributes TypeAttributes
		{
			get
			{
				return this.attributes;
			}
			set
			{
				this.attributes = value;
			}
		}

		/// <summary>Gets the base types of the type.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> object that indicates the base types of the type.</returns>
		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x06003ADF RID: 15071 RVA: 0x000F41E7 File Offset: 0x000F23E7
		public CodeTypeReferenceCollection BaseTypes
		{
			get
			{
				if ((this.populated & 1) == 0)
				{
					this.populated |= 1;
					if (this.PopulateBaseTypes != null)
					{
						this.PopulateBaseTypes(this, EventArgs.Empty);
					}
				}
				return this.baseTypes;
			}
		}

		/// <summary>Gets or sets a value indicating whether the type is a class or reference type.</summary>
		/// <returns>
		///   <see langword="true" /> if the type is a class or reference type; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x06003AE0 RID: 15072 RVA: 0x000F4220 File Offset: 0x000F2420
		// (set) Token: 0x06003AE1 RID: 15073 RVA: 0x000F4240 File Offset: 0x000F2440
		public bool IsClass
		{
			get
			{
				return (this.attributes & TypeAttributes.ClassSemanticsMask) == TypeAttributes.NotPublic && !this.isEnum && !this.isStruct;
			}
			set
			{
				if (value)
				{
					this.attributes &= ~TypeAttributes.ClassSemanticsMask;
					this.attributes |= TypeAttributes.NotPublic;
					this.isStruct = false;
					this.isEnum = false;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the type is a value type (struct).</summary>
		/// <returns>
		///   <see langword="true" /> if the type is a value type; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x06003AE2 RID: 15074 RVA: 0x000F4270 File Offset: 0x000F2470
		// (set) Token: 0x06003AE3 RID: 15075 RVA: 0x000F4278 File Offset: 0x000F2478
		public bool IsStruct
		{
			get
			{
				return this.isStruct;
			}
			set
			{
				if (value)
				{
					this.attributes &= ~TypeAttributes.ClassSemanticsMask;
					this.isStruct = true;
					this.isEnum = false;
					return;
				}
				this.isStruct = false;
			}
		}

		/// <summary>Gets or sets a value indicating whether the type is an enumeration.</summary>
		/// <returns>
		///   <see langword="true" /> if the type is an enumeration; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x06003AE4 RID: 15076 RVA: 0x000F42A2 File Offset: 0x000F24A2
		// (set) Token: 0x06003AE5 RID: 15077 RVA: 0x000F42AA File Offset: 0x000F24AA
		public bool IsEnum
		{
			get
			{
				return this.isEnum;
			}
			set
			{
				if (value)
				{
					this.attributes &= ~TypeAttributes.ClassSemanticsMask;
					this.isStruct = false;
					this.isEnum = true;
					return;
				}
				this.isEnum = false;
			}
		}

		/// <summary>Gets or sets a value indicating whether the type is an interface.</summary>
		/// <returns>
		///   <see langword="true" /> if the type is an interface; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x06003AE6 RID: 15078 RVA: 0x000F42D4 File Offset: 0x000F24D4
		// (set) Token: 0x06003AE7 RID: 15079 RVA: 0x000F42E4 File Offset: 0x000F24E4
		public bool IsInterface
		{
			get
			{
				return (this.attributes & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask;
			}
			set
			{
				if (value)
				{
					this.attributes &= ~TypeAttributes.ClassSemanticsMask;
					this.attributes |= TypeAttributes.ClassSemanticsMask;
					this.isStruct = false;
					this.isEnum = false;
					return;
				}
				this.attributes &= ~TypeAttributes.ClassSemanticsMask;
			}
		}

		/// <summary>Gets or sets a value indicating whether the type declaration is complete or partial.</summary>
		/// <returns>
		///   <see langword="true" /> if the class or structure declaration is a partial representation of the implementation; <see langword="false" /> if the declaration is a complete implementation of the class or structure. The default is <see langword="false" />.</returns>
		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x06003AE8 RID: 15080 RVA: 0x000F4330 File Offset: 0x000F2530
		// (set) Token: 0x06003AE9 RID: 15081 RVA: 0x000F4338 File Offset: 0x000F2538
		public bool IsPartial
		{
			get
			{
				return this.isPartial;
			}
			set
			{
				this.isPartial = value;
			}
		}

		/// <summary>Gets the collection of class members for the represented type.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeMemberCollection" /> object that indicates the class members.</returns>
		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x06003AEA RID: 15082 RVA: 0x000F4341 File Offset: 0x000F2541
		public CodeTypeMemberCollection Members
		{
			get
			{
				if ((this.populated & 2) == 0)
				{
					this.populated |= 2;
					if (this.PopulateMembers != null)
					{
						this.PopulateMembers(this, EventArgs.Empty);
					}
				}
				return this.members;
			}
		}

		/// <summary>Gets the type parameters for the type declaration.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeParameterCollection" /> that contains the type parameters for the type declaration.</returns>
		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x06003AEB RID: 15083 RVA: 0x000F437A File Offset: 0x000F257A
		[ComVisible(false)]
		public CodeTypeParameterCollection TypeParameters
		{
			get
			{
				if (this.typeParameters == null)
				{
					this.typeParameters = new CodeTypeParameterCollection();
				}
				return this.typeParameters;
			}
		}

		// Token: 0x04002C0D RID: 11277
		private TypeAttributes attributes = TypeAttributes.Public;

		// Token: 0x04002C0E RID: 11278
		private CodeTypeReferenceCollection baseTypes = new CodeTypeReferenceCollection();

		// Token: 0x04002C0F RID: 11279
		private CodeTypeMemberCollection members = new CodeTypeMemberCollection();

		// Token: 0x04002C10 RID: 11280
		private bool isEnum;

		// Token: 0x04002C11 RID: 11281
		private bool isStruct;

		// Token: 0x04002C12 RID: 11282
		private int populated;

		// Token: 0x04002C13 RID: 11283
		private const int BaseTypesCollection = 1;

		// Token: 0x04002C14 RID: 11284
		private const int MembersCollection = 2;

		// Token: 0x04002C15 RID: 11285
		[OptionalField]
		private CodeTypeParameterCollection typeParameters;

		// Token: 0x04002C16 RID: 11286
		[OptionalField]
		private bool isPartial;
	}
}
