using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Evaluation;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000110 RID: 272
	internal sealed class JsonMinimalMetadataLevel : JsonLightMetadataLevel
	{
		// Token: 0x06000753 RID: 1875 RVA: 0x00019014 File Offset: 0x00017214
		internal override JsonLightTypeNameOracle GetTypeNameOracle(bool autoComputePayloadMetadataInJson)
		{
			return new JsonMinimalMetadataTypeNameOracle();
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0001901B File Offset: 0x0001721B
		internal override bool ShouldWriteODataMetadataUri()
		{
			return true;
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0001901E File Offset: 0x0001721E
		internal override ODataEntityMetadataBuilder CreateEntityMetadataBuilder(ODataEntry entry, IODataFeedAndEntryTypeContext typeContext, ODataFeedAndEntrySerializationInfo serializationInfo, IEdmEntityType actualEntityType, SelectedPropertiesNode selectedProperties, bool isResponse, bool? keyAsSegment)
		{
			return null;
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00019021 File Offset: 0x00017221
		internal override void InjectMetadataBuilder(ODataEntry entry, ODataEntityMetadataBuilder builder)
		{
		}
	}
}
