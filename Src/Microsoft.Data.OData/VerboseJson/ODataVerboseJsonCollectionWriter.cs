using System;
using System.Threading.Tasks;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x02000274 RID: 628
	internal sealed class ODataVerboseJsonCollectionWriter : ODataCollectionWriterCore
	{
		// Token: 0x060014DA RID: 5338 RVA: 0x0004DFA0 File Offset: 0x0004C1A0
		internal ODataVerboseJsonCollectionWriter(ODataVerboseJsonOutputContext verboseJsonOutputContext, IEdmTypeReference itemTypeReference)
			: base(verboseJsonOutputContext, itemTypeReference)
		{
			this.verboseJsonOutputContext = verboseJsonOutputContext;
			this.verboseJsonCollectionSerializer = new ODataVerboseJsonCollectionSerializer(this.verboseJsonOutputContext);
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x0004DFC2 File Offset: 0x0004C1C2
		internal ODataVerboseJsonCollectionWriter(ODataVerboseJsonOutputContext verboseJsonOutputContext, IEdmTypeReference expectedItemType, IODataReaderWriterListener listener)
			: base(verboseJsonOutputContext, expectedItemType, listener)
		{
			this.verboseJsonOutputContext = verboseJsonOutputContext;
			this.verboseJsonCollectionSerializer = new ODataVerboseJsonCollectionSerializer(this.verboseJsonOutputContext);
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x0004DFE5 File Offset: 0x0004C1E5
		protected override void VerifyNotDisposed()
		{
			this.verboseJsonOutputContext.VerifyNotDisposed();
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x0004DFF2 File Offset: 0x0004C1F2
		protected override void FlushSynchronously()
		{
			this.verboseJsonOutputContext.Flush();
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x0004DFFF File Offset: 0x0004C1FF
		protected override Task FlushAsynchronously()
		{
			return this.verboseJsonOutputContext.FlushAsync();
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x0004E00C File Offset: 0x0004C20C
		protected override void StartPayload()
		{
			this.verboseJsonCollectionSerializer.WritePayloadStart();
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x0004E019 File Offset: 0x0004C219
		protected override void EndPayload()
		{
			this.verboseJsonCollectionSerializer.WritePayloadEnd();
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x0004E026 File Offset: 0x0004C226
		protected override void StartCollection(ODataCollectionStart collectionStart)
		{
			this.verboseJsonCollectionSerializer.WriteCollectionStart();
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x0004E033 File Offset: 0x0004C233
		protected override void EndCollection()
		{
			this.verboseJsonCollectionSerializer.WriteCollectionEnd();
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x0004E040 File Offset: 0x0004C240
		protected override void WriteCollectionItem(object item, IEdmTypeReference expectedItemType)
		{
			if (item == null)
			{
				ValidationUtils.ValidateNullCollectionItem(expectedItemType, this.verboseJsonOutputContext.MessageWriterSettings.WriterBehavior);
				this.verboseJsonOutputContext.JsonWriter.WriteValue(null);
				return;
			}
			ODataComplexValue odataComplexValue = item as ODataComplexValue;
			if (odataComplexValue != null)
			{
				this.verboseJsonCollectionSerializer.WriteComplexValue(odataComplexValue, expectedItemType, false, base.DuplicatePropertyNamesChecker, base.CollectionValidator);
				base.DuplicatePropertyNamesChecker.Clear();
				return;
			}
			this.verboseJsonCollectionSerializer.WritePrimitiveValue(item, base.CollectionValidator, expectedItemType);
		}

		// Token: 0x0400075C RID: 1884
		private readonly ODataVerboseJsonOutputContext verboseJsonOutputContext;

		// Token: 0x0400075D RID: 1885
		private readonly ODataVerboseJsonCollectionSerializer verboseJsonCollectionSerializer;
	}
}
