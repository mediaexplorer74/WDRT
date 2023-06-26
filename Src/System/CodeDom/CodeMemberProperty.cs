using System;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Represents a declaration for a property of a type.</summary>
	// Token: 0x0200063F RID: 1599
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Serializable]
	public class CodeMemberProperty : CodeTypeMember
	{
		/// <summary>Gets or sets the data type of the interface, if any, this property, if private, implements.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the interface, if any, the property, if private, implements.</returns>
		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x06003A13 RID: 14867 RVA: 0x000F3097 File Offset: 0x000F1297
		// (set) Token: 0x06003A14 RID: 14868 RVA: 0x000F309F File Offset: 0x000F129F
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

		/// <summary>Gets the data types of any interfaces that the property implements.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> that indicates the data types the property implements.</returns>
		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x06003A15 RID: 14869 RVA: 0x000F30A8 File Offset: 0x000F12A8
		public CodeTypeReferenceCollection ImplementationTypes
		{
			get
			{
				if (this.implementationTypes == null)
				{
					this.implementationTypes = new CodeTypeReferenceCollection();
				}
				return this.implementationTypes;
			}
		}

		/// <summary>Gets or sets the data type of the property.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the property.</returns>
		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x06003A16 RID: 14870 RVA: 0x000F30C3 File Offset: 0x000F12C3
		// (set) Token: 0x06003A17 RID: 14871 RVA: 0x000F30E3 File Offset: 0x000F12E3
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

		/// <summary>Gets or sets a value indicating whether the property has a <see langword="get" /> method accessor.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see langword="Count" /> property of the <see cref="P:System.CodeDom.CodeMemberProperty.GetStatements" /> collection is non-zero, or if the value of this property has been set to <see langword="true" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x06003A18 RID: 14872 RVA: 0x000F30EC File Offset: 0x000F12EC
		// (set) Token: 0x06003A19 RID: 14873 RVA: 0x000F3106 File Offset: 0x000F1306
		public bool HasGet
		{
			get
			{
				return this.hasGet || this.getStatements.Count > 0;
			}
			set
			{
				this.hasGet = value;
				if (!value)
				{
					this.getStatements.Clear();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the property has a <see langword="set" /> method accessor.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="P:System.Collections.CollectionBase.Count" /> property of the <see cref="P:System.CodeDom.CodeMemberProperty.SetStatements" /> collection is non-zero; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x06003A1A RID: 14874 RVA: 0x000F311D File Offset: 0x000F131D
		// (set) Token: 0x06003A1B RID: 14875 RVA: 0x000F3137 File Offset: 0x000F1337
		public bool HasSet
		{
			get
			{
				return this.hasSet || this.setStatements.Count > 0;
			}
			set
			{
				this.hasSet = value;
				if (!value)
				{
					this.setStatements.Clear();
				}
			}
		}

		/// <summary>Gets the collection of <see langword="get" /> statements for the property.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> that contains the <see langword="get" /> statements for the member property.</returns>
		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x06003A1C RID: 14876 RVA: 0x000F314E File Offset: 0x000F134E
		public CodeStatementCollection GetStatements
		{
			get
			{
				return this.getStatements;
			}
		}

		/// <summary>Gets the collection of <see langword="set" /> statements for the property.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> that contains the <see langword="set" /> statements for the member property.</returns>
		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x06003A1D RID: 14877 RVA: 0x000F3156 File Offset: 0x000F1356
		public CodeStatementCollection SetStatements
		{
			get
			{
				return this.setStatements;
			}
		}

		/// <summary>Gets the collection of declaration expressions for the property.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> that indicates the declaration expressions for the property.</returns>
		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x06003A1E RID: 14878 RVA: 0x000F315E File Offset: 0x000F135E
		public CodeParameterDeclarationExpressionCollection Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x04002BD0 RID: 11216
		private CodeTypeReference type;

		// Token: 0x04002BD1 RID: 11217
		private CodeParameterDeclarationExpressionCollection parameters = new CodeParameterDeclarationExpressionCollection();

		// Token: 0x04002BD2 RID: 11218
		private bool hasGet;

		// Token: 0x04002BD3 RID: 11219
		private bool hasSet;

		// Token: 0x04002BD4 RID: 11220
		private CodeStatementCollection getStatements = new CodeStatementCollection();

		// Token: 0x04002BD5 RID: 11221
		private CodeStatementCollection setStatements = new CodeStatementCollection();

		// Token: 0x04002BD6 RID: 11222
		private CodeTypeReference privateImplements;

		// Token: 0x04002BD7 RID: 11223
		private CodeTypeReferenceCollection implementationTypes;
	}
}
