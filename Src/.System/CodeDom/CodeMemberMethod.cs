using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.CodeDom
{
	/// <summary>Represents a declaration for a method of a type.</summary>
	// Token: 0x0200063E RID: 1598
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeMemberMethod : CodeTypeMember
	{
		/// <summary>An event that will be raised the first time the <see cref="P:System.CodeDom.CodeMemberMethod.Parameters" /> collection is accessed.</summary>
		// Token: 0x1400006A RID: 106
		// (add) Token: 0x06003A03 RID: 14851 RVA: 0x000F2DE8 File Offset: 0x000F0FE8
		// (remove) Token: 0x06003A04 RID: 14852 RVA: 0x000F2E20 File Offset: 0x000F1020
		public event EventHandler PopulateParameters;

		/// <summary>An event that will be raised the first time the <see cref="P:System.CodeDom.CodeMemberMethod.Statements" /> collection is accessed.</summary>
		// Token: 0x1400006B RID: 107
		// (add) Token: 0x06003A05 RID: 14853 RVA: 0x000F2E58 File Offset: 0x000F1058
		// (remove) Token: 0x06003A06 RID: 14854 RVA: 0x000F2E90 File Offset: 0x000F1090
		public event EventHandler PopulateStatements;

		/// <summary>An event that will be raised the first time the <see cref="P:System.CodeDom.CodeMemberMethod.ImplementationTypes" /> collection is accessed.</summary>
		// Token: 0x1400006C RID: 108
		// (add) Token: 0x06003A07 RID: 14855 RVA: 0x000F2EC8 File Offset: 0x000F10C8
		// (remove) Token: 0x06003A08 RID: 14856 RVA: 0x000F2F00 File Offset: 0x000F1100
		public event EventHandler PopulateImplementationTypes;

		/// <summary>Gets or sets the data type of the return value of the method.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the value returned by the method.</returns>
		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x06003A09 RID: 14857 RVA: 0x000F2F35 File Offset: 0x000F1135
		// (set) Token: 0x06003A0A RID: 14858 RVA: 0x000F2F5F File Offset: 0x000F115F
		public CodeTypeReference ReturnType
		{
			get
			{
				if (this.returnType == null)
				{
					this.returnType = new CodeTypeReference(typeof(void).FullName);
				}
				return this.returnType;
			}
			set
			{
				this.returnType = value;
			}
		}

		/// <summary>Gets the statements within the method.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> that indicates the statements within the method.</returns>
		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x06003A0B RID: 14859 RVA: 0x000F2F68 File Offset: 0x000F1168
		public CodeStatementCollection Statements
		{
			get
			{
				if ((this.populated & 2) == 0)
				{
					this.populated |= 2;
					if (this.PopulateStatements != null)
					{
						this.PopulateStatements(this, EventArgs.Empty);
					}
				}
				return this.statements;
			}
		}

		/// <summary>Gets the parameter declarations for the method.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> that indicates the method parameters.</returns>
		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x06003A0C RID: 14860 RVA: 0x000F2FA1 File Offset: 0x000F11A1
		public CodeParameterDeclarationExpressionCollection Parameters
		{
			get
			{
				if ((this.populated & 1) == 0)
				{
					this.populated |= 1;
					if (this.PopulateParameters != null)
					{
						this.PopulateParameters(this, EventArgs.Empty);
					}
				}
				return this.parameters;
			}
		}

		/// <summary>Gets or sets the data type of the interface this method, if private, implements a method of, if any.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the interface with the method that the private method whose declaration is represented by this <see cref="T:System.CodeDom.CodeMemberMethod" /> implements.</returns>
		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x06003A0D RID: 14861 RVA: 0x000F2FDA File Offset: 0x000F11DA
		// (set) Token: 0x06003A0E RID: 14862 RVA: 0x000F2FE2 File Offset: 0x000F11E2
		public CodeTypeReference PrivateImplementationType
		{
			get
			{
				return this.privateImplements;
			}
			set
			{
				this.privateImplements = value;
			}
		}

		/// <summary>Gets the data types of the interfaces implemented by this method, unless it is a private method implementation, which is indicated by the <see cref="P:System.CodeDom.CodeMemberMethod.PrivateImplementationType" /> property.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> that indicates the interfaces implemented by this method.</returns>
		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x06003A0F RID: 14863 RVA: 0x000F2FEC File Offset: 0x000F11EC
		public CodeTypeReferenceCollection ImplementationTypes
		{
			get
			{
				if (this.implementationTypes == null)
				{
					this.implementationTypes = new CodeTypeReferenceCollection();
				}
				if ((this.populated & 4) == 0)
				{
					this.populated |= 4;
					if (this.PopulateImplementationTypes != null)
					{
						this.PopulateImplementationTypes(this, EventArgs.Empty);
					}
				}
				return this.implementationTypes;
			}
		}

		/// <summary>Gets the custom attributes of the return type of the method.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> that indicates the custom attributes.</returns>
		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x06003A10 RID: 14864 RVA: 0x000F3043 File Offset: 0x000F1243
		public CodeAttributeDeclarationCollection ReturnTypeCustomAttributes
		{
			get
			{
				if (this.returnAttributes == null)
				{
					this.returnAttributes = new CodeAttributeDeclarationCollection();
				}
				return this.returnAttributes;
			}
		}

		/// <summary>Gets the type parameters for the current generic method.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeParameterCollection" /> that contains the type parameters for the generic method.</returns>
		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x06003A11 RID: 14865 RVA: 0x000F305E File Offset: 0x000F125E
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

		// Token: 0x04002BC2 RID: 11202
		private CodeParameterDeclarationExpressionCollection parameters = new CodeParameterDeclarationExpressionCollection();

		// Token: 0x04002BC3 RID: 11203
		private CodeStatementCollection statements = new CodeStatementCollection();

		// Token: 0x04002BC4 RID: 11204
		private CodeTypeReference returnType;

		// Token: 0x04002BC5 RID: 11205
		private CodeTypeReference privateImplements;

		// Token: 0x04002BC6 RID: 11206
		private CodeTypeReferenceCollection implementationTypes;

		// Token: 0x04002BC7 RID: 11207
		private CodeAttributeDeclarationCollection returnAttributes;

		// Token: 0x04002BC8 RID: 11208
		[OptionalField]
		private CodeTypeParameterCollection typeParameters;

		// Token: 0x04002BC9 RID: 11209
		private int populated;

		// Token: 0x04002BCA RID: 11210
		private const int ParametersCollection = 1;

		// Token: 0x04002BCB RID: 11211
		private const int StatementsCollection = 2;

		// Token: 0x04002BCC RID: 11212
		private const int ImplTypesCollection = 4;
	}
}
