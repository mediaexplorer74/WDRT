using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Metadata
{
	// Token: 0x02000148 RID: 328
	internal abstract class EdmTypeResolver
	{
		// Token: 0x060008D5 RID: 2261
		internal abstract IEdmEntityType GetElementType(IEdmEntitySet entitySet);

		// Token: 0x060008D6 RID: 2262
		internal abstract IEdmTypeReference GetReturnType(IEdmFunctionImport functionImport);

		// Token: 0x060008D7 RID: 2263
		internal abstract IEdmTypeReference GetReturnType(IEnumerable<IEdmFunctionImport> functionImportGroup);

		// Token: 0x060008D8 RID: 2264
		internal abstract IEdmTypeReference GetParameterType(IEdmFunctionParameter functionParameter);
	}
}
