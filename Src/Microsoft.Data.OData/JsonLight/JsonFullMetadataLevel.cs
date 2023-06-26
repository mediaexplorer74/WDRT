using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Evaluation;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x0200010D RID: 269
	internal sealed class JsonFullMetadataLevel : JsonLightMetadataLevel
	{
		// Token: 0x06000747 RID: 1863 RVA: 0x00018E1C File Offset: 0x0001701C
		internal JsonFullMetadataLevel(Uri metadataDocumentUri, IEdmModel model)
		{
			this.metadataDocumentUri = metadataDocumentUri;
			this.model = model;
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x00018E32 File Offset: 0x00017032
		private Uri NonNullMetadataDocumentUri
		{
			get
			{
				if (this.metadataDocumentUri == null)
				{
					throw new ODataException(Strings.ODataJsonLightOutputContext_MetadataDocumentUriMissing);
				}
				return this.metadataDocumentUri;
			}
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00018E53 File Offset: 0x00017053
		internal override JsonLightTypeNameOracle GetTypeNameOracle(bool autoComputePayloadMetadataInJson)
		{
			if (autoComputePayloadMetadataInJson)
			{
				return new JsonFullMetadataTypeNameOracle();
			}
			return new JsonMinimalMetadataTypeNameOracle();
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00018E63 File Offset: 0x00017063
		internal override bool ShouldWriteODataMetadataUri()
		{
			return true;
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00018E68 File Offset: 0x00017068
		internal override ODataEntityMetadataBuilder CreateEntityMetadataBuilder(ODataEntry entry, IODataFeedAndEntryTypeContext typeContext, ODataFeedAndEntrySerializationInfo serializationInfo, IEdmEntityType actualEntityType, SelectedPropertiesNode selectedProperties, bool isResponse, bool? keyAsSegment)
		{
			IODataMetadataContext iodataMetadataContext = new ODataMetadataContext(isResponse, this.model, this.NonNullMetadataDocumentUri);
			UrlConvention urlConvention = UrlConvention.ForUserSettingAndTypeContext(keyAsSegment, typeContext);
			ODataConventionalUriBuilder odataConventionalUriBuilder = new ODataConventionalUriBuilder(iodataMetadataContext.ServiceBaseUri, urlConvention);
			IODataEntryMetadataContext iodataEntryMetadataContext = ODataEntryMetadataContext.Create(entry, typeContext, serializationInfo, actualEntityType, iodataMetadataContext, selectedProperties);
			return new ODataConventionalEntityMetadataBuilder(iodataEntryMetadataContext, iodataMetadataContext, odataConventionalUriBuilder);
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x00018EB8 File Offset: 0x000170B8
		internal override void InjectMetadataBuilder(ODataEntry entry, ODataEntityMetadataBuilder builder)
		{
			entry.MetadataBuilder = builder;
			ODataStreamReferenceValue nonComputedMediaResource = entry.NonComputedMediaResource;
			if (nonComputedMediaResource != null)
			{
				nonComputedMediaResource.SetMetadataBuilder(builder, null);
			}
			if (entry.NonComputedProperties != null)
			{
				foreach (ODataProperty odataProperty in entry.NonComputedProperties)
				{
					ODataStreamReferenceValue odataStreamReferenceValue = odataProperty.ODataValue as ODataStreamReferenceValue;
					if (odataStreamReferenceValue != null)
					{
						odataStreamReferenceValue.SetMetadataBuilder(builder, odataProperty.Name);
					}
				}
			}
			IEnumerable<ODataOperation> enumerable = ODataUtilsInternal.ConcatEnumerables<ODataOperation>(entry.NonComputedActions, entry.NonComputedFunctions);
			if (enumerable != null)
			{
				foreach (ODataOperation odataOperation in enumerable)
				{
					odataOperation.SetMetadataBuilder(builder, this.NonNullMetadataDocumentUri);
				}
			}
		}

		// Token: 0x040002C3 RID: 707
		private readonly IEdmModel model;

		// Token: 0x040002C4 RID: 708
		private readonly Uri metadataDocumentUri;
	}
}
