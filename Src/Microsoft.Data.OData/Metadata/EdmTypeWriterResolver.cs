using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Metadata
{
	// Token: 0x0200014A RID: 330
	internal sealed class EdmTypeWriterResolver : EdmTypeResolver
	{
		// Token: 0x060008E1 RID: 2273 RVA: 0x0001C80F File Offset: 0x0001AA0F
		private EdmTypeWriterResolver()
		{
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0001C817 File Offset: 0x0001AA17
		internal override IEdmEntityType GetElementType(IEdmEntitySet entitySet)
		{
			if (entitySet != null)
			{
				return entitySet.ElementType;
			}
			return null;
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0001C824 File Offset: 0x0001AA24
		internal override IEdmTypeReference GetReturnType(IEdmFunctionImport functionImport)
		{
			if (functionImport != null)
			{
				return functionImport.ReturnType;
			}
			return null;
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0001C831 File Offset: 0x0001AA31
		internal override IEdmTypeReference GetReturnType(IEnumerable<IEdmFunctionImport> functionImportGroup)
		{
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.EdmTypeWriterResolver_GetReturnTypeForFunctionImportGroup));
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0001C844 File Offset: 0x0001AA44
		internal override IEdmTypeReference GetParameterType(IEdmFunctionParameter functionParameter)
		{
			if (functionParameter != null)
			{
				return functionParameter.Type;
			}
			return null;
		}

		// Token: 0x04000358 RID: 856
		internal static EdmTypeWriterResolver Instance = new EdmTypeWriterResolver();
	}
}
