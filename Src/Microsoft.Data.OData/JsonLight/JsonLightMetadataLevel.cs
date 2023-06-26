using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Evaluation;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x0200010C RID: 268
	internal abstract class JsonLightMetadataLevel
	{
		// Token: 0x06000741 RID: 1857 RVA: 0x00018D4C File Offset: 0x00016F4C
		internal static JsonLightMetadataLevel Create(MediaType mediaType, Uri metadataDocumentUri, IEdmModel model, bool writingResponse)
		{
			if (writingResponse && mediaType.Parameters != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in mediaType.Parameters)
				{
					if (HttpUtils.CompareMediaTypeParameterNames(keyValuePair.Key, "odata"))
					{
						if (string.Compare(keyValuePair.Value, "minimalmetadata", StringComparison.OrdinalIgnoreCase) == 0)
						{
							return new JsonMinimalMetadataLevel();
						}
						if (string.Compare(keyValuePair.Value, "fullmetadata", StringComparison.OrdinalIgnoreCase) == 0)
						{
							return new JsonFullMetadataLevel(metadataDocumentUri, model);
						}
						if (string.Compare(keyValuePair.Value, "nometadata", StringComparison.OrdinalIgnoreCase) == 0)
						{
							return new JsonNoMetadataLevel();
						}
					}
				}
			}
			return new JsonMinimalMetadataLevel();
		}

		// Token: 0x06000742 RID: 1858
		internal abstract JsonLightTypeNameOracle GetTypeNameOracle(bool autoComputePayloadMetadataInJson);

		// Token: 0x06000743 RID: 1859
		internal abstract bool ShouldWriteODataMetadataUri();

		// Token: 0x06000744 RID: 1860
		internal abstract ODataEntityMetadataBuilder CreateEntityMetadataBuilder(ODataEntry entry, IODataFeedAndEntryTypeContext typeContext, ODataFeedAndEntrySerializationInfo serializationInfo, IEdmEntityType actualEntityType, SelectedPropertiesNode selectedProperties, bool isResponse, bool? keyAsSegment);

		// Token: 0x06000745 RID: 1861
		internal abstract void InjectMetadataBuilder(ODataEntry entry, ODataEntityMetadataBuilder builder);
	}
}
