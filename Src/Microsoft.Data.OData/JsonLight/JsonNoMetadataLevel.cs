using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Evaluation;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000112 RID: 274
	internal sealed class JsonNoMetadataLevel : JsonLightMetadataLevel
	{
		// Token: 0x0600075B RID: 1883 RVA: 0x000190CF File Offset: 0x000172CF
		internal override JsonLightTypeNameOracle GetTypeNameOracle(bool autoComputePayloadMetadataInJson)
		{
			if (autoComputePayloadMetadataInJson)
			{
				return new JsonNoMetadataTypeNameOracle();
			}
			return new JsonMinimalMetadataTypeNameOracle();
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x000190DF File Offset: 0x000172DF
		internal override bool ShouldWriteODataMetadataUri()
		{
			return false;
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x000190E2 File Offset: 0x000172E2
		internal override ODataEntityMetadataBuilder CreateEntityMetadataBuilder(ODataEntry entry, IODataFeedAndEntryTypeContext typeContext, ODataFeedAndEntrySerializationInfo serializationInfo, IEdmEntityType actualEntityType, SelectedPropertiesNode selectedProperties, bool isResponse, bool? keyAsSegment)
		{
			return ODataEntityMetadataBuilder.Null;
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x000190E9 File Offset: 0x000172E9
		internal override void InjectMetadataBuilder(ODataEntry entry, ODataEntityMetadataBuilder builder)
		{
			entry.MetadataBuilder = builder;
		}
	}
}
