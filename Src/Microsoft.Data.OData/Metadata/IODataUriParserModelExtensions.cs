using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Metadata
{
	// Token: 0x02000131 RID: 305
	public interface IODataUriParserModelExtensions
	{
		// Token: 0x060007FF RID: 2047
		IEnumerable<IEdmFunctionImport> FindFunctionImportsByBindingParameterTypeHierarchy(IEdmType bindingType, string functionImportName);

		// Token: 0x06000800 RID: 2048
		IEdmEntitySet FindEntitySetFromContainerQualifiedName(string containerQualifiedEntitySetName);

		// Token: 0x06000801 RID: 2049
		IEdmFunctionImport FindServiceOperation(string serviceOperationName);

		// Token: 0x06000802 RID: 2050
		IEdmFunctionImport FindFunctionImportByBindingParameterType(IEdmType bindingType, string functionImportName, IEnumerable<string> nonBindingParameterNamesFromUri);
	}
}
