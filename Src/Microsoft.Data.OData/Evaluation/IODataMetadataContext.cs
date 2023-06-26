using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.JsonLight;

namespace Microsoft.Data.OData.Evaluation
{
	// Token: 0x02000108 RID: 264
	internal interface IODataMetadataContext
	{
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000729 RID: 1833
		IEdmModel Model { get; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600072A RID: 1834
		Uri ServiceBaseUri { get; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600072B RID: 1835
		Uri MetadataDocumentUri { get; }

		// Token: 0x0600072C RID: 1836
		ODataEntityMetadataBuilder GetEntityMetadataBuilderForReader(IODataJsonLightReaderEntryState entryState);

		// Token: 0x0600072D RID: 1837
		IEdmFunctionImport[] GetAlwaysBindableOperationsForType(IEdmType bindingType);

		// Token: 0x0600072E RID: 1838
		bool OperationsBoundToEntityTypeMustBeContainerQualified(IEdmEntityType entityType);
	}
}
