using System;
using System.Threading.Tasks;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000183 RID: 387
	internal sealed class ODataJsonLightCollectionReader : ODataCollectionReaderCoreAsync
	{
		// Token: 0x06000AE1 RID: 2785 RVA: 0x0002470B File Offset: 0x0002290B
		internal ODataJsonLightCollectionReader(ODataJsonLightInputContext jsonLightInputContext, IEdmTypeReference expectedItemTypeReference, IODataReaderWriterListener listener)
			: base(jsonLightInputContext, expectedItemTypeReference, listener)
		{
			this.jsonLightInputContext = jsonLightInputContext;
			this.jsonLightCollectionDeserializer = new ODataJsonLightCollectionDeserializer(jsonLightInputContext);
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0002472C File Offset: 0x0002292C
		protected override bool ReadAtStartImplementation()
		{
			DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = this.jsonLightInputContext.CreateDuplicatePropertyNamesChecker();
			this.jsonLightCollectionDeserializer.ReadPayloadStart(ODataPayloadKind.Collection, duplicatePropertyNamesChecker, base.IsReadingNestedPayload, false);
			return this.ReadAtStartImplementationSynchronously(duplicatePropertyNamesChecker);
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0002477C File Offset: 0x0002297C
		protected override Task<bool> ReadAtStartImplementationAsync()
		{
			DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = this.jsonLightInputContext.CreateDuplicatePropertyNamesChecker();
			return this.jsonLightCollectionDeserializer.ReadPayloadStartAsync(ODataPayloadKind.Collection, duplicatePropertyNamesChecker, base.IsReadingNestedPayload, false).FollowOnSuccessWith((Task t) => this.ReadAtStartImplementationSynchronously(duplicatePropertyNamesChecker));
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x000247D1 File Offset: 0x000229D1
		protected override bool ReadAtCollectionStartImplementation()
		{
			return this.ReadAtCollectionStartImplementationSynchronously();
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x000247D9 File Offset: 0x000229D9
		protected override Task<bool> ReadAtCollectionStartImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtCollectionStartImplementationSynchronously));
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x000247EC File Offset: 0x000229EC
		protected override bool ReadAtValueImplementation()
		{
			return this.ReadAtValueImplementationSynchronously();
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x000247F4 File Offset: 0x000229F4
		protected override Task<bool> ReadAtValueImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtValueImplementationSynchronously));
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x00024807 File Offset: 0x00022A07
		protected override bool ReadAtCollectionEndImplementation()
		{
			return this.ReadAtCollectionEndImplementationSynchronously();
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0002480F File Offset: 0x00022A0F
		protected override Task<bool> ReadAtCollectionEndImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtCollectionEndImplementationSynchronously));
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x00024824 File Offset: 0x00022A24
		private bool ReadAtStartImplementationSynchronously(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			base.ExpectedItemTypeReference = ReaderValidationUtils.ValidateCollectionMetadataUriAndGetPayloadItemTypeReference(this.jsonLightCollectionDeserializer.MetadataUriParseResult, base.ExpectedItemTypeReference);
			IEdmTypeReference edmTypeReference;
			ODataCollectionStart odataCollectionStart = this.jsonLightCollectionDeserializer.ReadCollectionStart(duplicatePropertyNamesChecker, base.IsReadingNestedPayload, base.ExpectedItemTypeReference, out edmTypeReference);
			if (edmTypeReference != null)
			{
				base.ExpectedItemTypeReference = edmTypeReference;
			}
			this.jsonLightCollectionDeserializer.JsonReader.ReadStartArray();
			base.EnterScope(ODataCollectionReaderState.CollectionStart, odataCollectionStart);
			return true;
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0002488C File Offset: 0x00022A8C
		private bool ReadAtCollectionStartImplementationSynchronously()
		{
			if (this.jsonLightCollectionDeserializer.JsonReader.NodeType == JsonNodeType.EndArray)
			{
				base.ReplaceScope(ODataCollectionReaderState.CollectionEnd, this.Item);
			}
			else
			{
				object obj = this.jsonLightCollectionDeserializer.ReadCollectionItem(base.ExpectedItemTypeReference, base.CollectionValidator);
				base.EnterScope(ODataCollectionReaderState.Value, obj);
			}
			return true;
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x000248DC File Offset: 0x00022ADC
		private bool ReadAtValueImplementationSynchronously()
		{
			if (this.jsonLightCollectionDeserializer.JsonReader.NodeType == JsonNodeType.EndArray)
			{
				base.PopScope(ODataCollectionReaderState.Value);
				base.ReplaceScope(ODataCollectionReaderState.CollectionEnd, this.Item);
			}
			else
			{
				object obj = this.jsonLightCollectionDeserializer.ReadCollectionItem(base.ExpectedItemTypeReference, base.CollectionValidator);
				base.ReplaceScope(ODataCollectionReaderState.Value, obj);
			}
			return true;
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x00024933 File Offset: 0x00022B33
		private bool ReadAtCollectionEndImplementationSynchronously()
		{
			base.PopScope(ODataCollectionReaderState.CollectionEnd);
			this.jsonLightCollectionDeserializer.ReadCollectionEnd(base.IsReadingNestedPayload);
			this.jsonLightCollectionDeserializer.ReadPayloadEnd(base.IsReadingNestedPayload);
			base.ReplaceScope(ODataCollectionReaderState.Completed, null);
			return false;
		}

		// Token: 0x04000403 RID: 1027
		private readonly ODataJsonLightInputContext jsonLightInputContext;

		// Token: 0x04000404 RID: 1028
		private readonly ODataJsonLightCollectionDeserializer jsonLightCollectionDeserializer;
	}
}
