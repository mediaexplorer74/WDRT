using System;

namespace Microsoft.Data.Edm.Library
{
	// Token: 0x020001D2 RID: 466
	public class EdmFunction : EdmFunctionBase, IEdmFunction, IEdmFunctionBase, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000B11 RID: 2833 RVA: 0x00020746 File Offset: 0x0001E946
		public EdmFunction(string namespaceName, string name, IEdmTypeReference returnType)
			: this(namespaceName, name, returnType, null)
		{
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x00020752 File Offset: 0x0001E952
		public EdmFunction(string namespaceName, string name, IEdmTypeReference returnType, string definingExpression)
			: base(name, returnType)
		{
			EdmUtil.CheckArgumentNull<string>(namespaceName, "namespaceName");
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(returnType, "returnType");
			this.namespaceName = namespaceName;
			this.definingExpression = definingExpression;
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x00020783 File Offset: 0x0001E983
		public string DefiningExpression
		{
			get
			{
				return this.definingExpression;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x0002078B File Offset: 0x0001E98B
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.Function;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x0002078E File Offset: 0x0001E98E
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x04000533 RID: 1331
		private readonly string namespaceName;

		// Token: 0x04000534 RID: 1332
		private readonly string definingExpression;
	}
}
