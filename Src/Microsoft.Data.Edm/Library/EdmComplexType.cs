using System;

namespace Microsoft.Data.Edm.Library
{
	// Token: 0x020001CB RID: 459
	public class EdmComplexType : EdmStructuredType, IEdmComplexType, IEdmStructuredType, IEdmSchemaType, IEdmType, IEdmTerm, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000AD8 RID: 2776 RVA: 0x00020030 File Offset: 0x0001E230
		public EdmComplexType(string namespaceName, string name)
			: this(namespaceName, name, null, false)
		{
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0002003C File Offset: 0x0001E23C
		public EdmComplexType(string namespaceName, string name, IEdmComplexType baseType, bool isAbstract)
			: base(isAbstract, false, baseType)
		{
			EdmUtil.CheckArgumentNull<string>(namespaceName, "namespaceName");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			this.namespaceName = namespaceName;
			this.name = name;
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x0002006E File Offset: 0x0001E26E
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x00020071 File Offset: 0x0001E271
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x00020079 File Offset: 0x0001E279
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x00020081 File Offset: 0x0001E281
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Complex;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x00020084 File Offset: 0x0001E284
		public EdmTermKind TermKind
		{
			get
			{
				return EdmTermKind.Type;
			}
		}

		// Token: 0x0400051E RID: 1310
		private readonly string namespaceName;

		// Token: 0x0400051F RID: 1311
		private readonly string name;
	}
}
