using System;
using System.Threading.Tasks;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x0200018A RID: 394
	internal sealed class ODataJsonLightCollectionWriter : ODataCollectionWriterCore
	{
		// Token: 0x06000B28 RID: 2856 RVA: 0x00025216 File Offset: 0x00023416
		internal ODataJsonLightCollectionWriter(ODataJsonLightOutputContext jsonLightOutputContext, IEdmTypeReference itemTypeReference)
			: base(jsonLightOutputContext, itemTypeReference)
		{
			this.jsonLightOutputContext = jsonLightOutputContext;
			this.jsonLightCollectionSerializer = new ODataJsonLightCollectionSerializer(this.jsonLightOutputContext, true);
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00025239 File Offset: 0x00023439
		internal ODataJsonLightCollectionWriter(ODataJsonLightOutputContext jsonLightOutputContext, IEdmTypeReference expectedItemType, IODataReaderWriterListener listener)
			: base(jsonLightOutputContext, expectedItemType, listener)
		{
			this.jsonLightOutputContext = jsonLightOutputContext;
			this.jsonLightCollectionSerializer = new ODataJsonLightCollectionSerializer(this.jsonLightOutputContext, false);
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0002525D File Offset: 0x0002345D
		protected override void VerifyNotDisposed()
		{
			this.jsonLightOutputContext.VerifyNotDisposed();
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0002526A File Offset: 0x0002346A
		protected override void FlushSynchronously()
		{
			this.jsonLightOutputContext.Flush();
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00025277 File Offset: 0x00023477
		protected override Task FlushAsynchronously()
		{
			return this.jsonLightOutputContext.FlushAsync();
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x00025284 File Offset: 0x00023484
		protected override void StartPayload()
		{
			this.jsonLightCollectionSerializer.WritePayloadStart();
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x00025291 File Offset: 0x00023491
		protected override void EndPayload()
		{
			this.jsonLightCollectionSerializer.WritePayloadEnd();
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0002529E File Offset: 0x0002349E
		protected override void StartCollection(ODataCollectionStart collectionStart)
		{
			this.jsonLightCollectionSerializer.WriteCollectionStart(collectionStart, base.ItemTypeReference);
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x000252B2 File Offset: 0x000234B2
		protected override void EndCollection()
		{
			this.jsonLightCollectionSerializer.WriteCollectionEnd();
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x000252C0 File Offset: 0x000234C0
		protected override void WriteCollectionItem(object item, IEdmTypeReference expectedItemType)
		{
			if (item == null)
			{
				ValidationUtils.ValidateNullCollectionItem(expectedItemType, this.jsonLightOutputContext.MessageWriterSettings.WriterBehavior);
				this.jsonLightOutputContext.JsonWriter.WriteValue(null);
				return;
			}
			ODataComplexValue odataComplexValue = item as ODataComplexValue;
			if (odataComplexValue != null)
			{
				this.jsonLightCollectionSerializer.WriteComplexValue(odataComplexValue, expectedItemType, false, false, base.DuplicatePropertyNamesChecker);
				base.DuplicatePropertyNamesChecker.Clear();
				return;
			}
			this.jsonLightCollectionSerializer.WritePrimitiveValue(item, expectedItemType);
		}

		// Token: 0x04000415 RID: 1045
		private readonly ODataJsonLightOutputContext jsonLightOutputContext;

		// Token: 0x04000416 RID: 1046
		private readonly ODataJsonLightCollectionSerializer jsonLightCollectionSerializer;
	}
}
