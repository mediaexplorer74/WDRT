using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm.Library.Internal;
using Microsoft.Data.Edm.Validation;

namespace Microsoft.Data.Edm.Csdl.Internal.CsdlSemantics
{
	// Token: 0x02000188 RID: 392
	internal class UnresolvedFunction : BadElement, IEdmFunction, IEdmFunctionBase, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement, IUnresolvedElement
	{
		// Token: 0x060008A2 RID: 2210 RVA: 0x000180FC File Offset: 0x000162FC
		public UnresolvedFunction(string qualifiedName, string errorMessage, EdmLocation location)
			: base(new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedFunction, errorMessage)
			})
		{
			qualifiedName = qualifiedName ?? string.Empty;
			EdmUtil.TryGetNamespaceNameFromQualifiedName(qualifiedName, out this.namespaceName, out this.name);
			this.returnType = new BadTypeReference(new BadType(base.Errors), true);
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x0001815C File Offset: 0x0001635C
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.Function;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x0001815F File Offset: 0x0001635F
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x00018167 File Offset: 0x00016367
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x0001816F File Offset: 0x0001636F
		public string DefiningExpression
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x00018172 File Offset: 0x00016372
		public IEdmTypeReference ReturnType
		{
			get
			{
				return this.returnType;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x0001817A File Offset: 0x0001637A
		public IEnumerable<IEdmFunctionParameter> Parameters
		{
			get
			{
				return Enumerable.Empty<IEdmFunctionParameter>();
			}
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00018181 File Offset: 0x00016381
		public IEdmFunctionParameter FindParameter(string name)
		{
			return null;
		}

		// Token: 0x04000446 RID: 1094
		private readonly string namespaceName;

		// Token: 0x04000447 RID: 1095
		private readonly string name;

		// Token: 0x04000448 RID: 1096
		private readonly IEdmTypeReference returnType;
	}
}
