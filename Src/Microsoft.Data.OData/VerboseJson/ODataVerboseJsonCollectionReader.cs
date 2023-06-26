using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Json;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x0200023C RID: 572
	internal sealed class ODataVerboseJsonCollectionReader : ODataCollectionReaderCore
	{
		// Token: 0x06001255 RID: 4693 RVA: 0x00044FF4 File Offset: 0x000431F4
		internal ODataVerboseJsonCollectionReader(ODataVerboseJsonInputContext verboseJsonInputContext, IEdmTypeReference expectedItemTypeReference, IODataReaderWriterListener listener)
			: base(verboseJsonInputContext, expectedItemTypeReference, listener)
		{
			this.verboseJsonInputContext = verboseJsonInputContext;
			this.verboseJsonCollectionDeserializer = new ODataVerboseJsonCollectionDeserializer(verboseJsonInputContext);
			if (!verboseJsonInputContext.Model.IsUserModel())
			{
				throw new ODataException(Strings.ODataJsonCollectionReader_ParsingWithoutMetadata);
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06001256 RID: 4694 RVA: 0x0004502A File Offset: 0x0004322A
		private bool IsResultsWrapperExpected
		{
			get
			{
				return this.verboseJsonInputContext.Version >= ODataVersion.V2 && this.verboseJsonInputContext.ReadingResponse;
			}
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x00045048 File Offset: 0x00043248
		protected override bool ReadAtStartImplementation()
		{
			this.verboseJsonCollectionDeserializer.ReadPayloadStart(base.IsReadingNestedPayload);
			if (this.IsResultsWrapperExpected && this.verboseJsonCollectionDeserializer.JsonReader.NodeType != JsonNodeType.StartObject)
			{
				throw new ODataException(Strings.ODataJsonCollectionReader_CannotReadWrappedCollectionStart(this.verboseJsonCollectionDeserializer.JsonReader.NodeType));
			}
			if (!this.IsResultsWrapperExpected && this.verboseJsonCollectionDeserializer.JsonReader.NodeType != JsonNodeType.StartArray)
			{
				throw new ODataException(Strings.ODataJsonCollectionReader_CannotReadCollectionStart(this.verboseJsonCollectionDeserializer.JsonReader.NodeType));
			}
			ODataCollectionStart odataCollectionStart = this.verboseJsonCollectionDeserializer.ReadCollectionStart(this.IsResultsWrapperExpected);
			this.verboseJsonCollectionDeserializer.JsonReader.ReadStartArray();
			base.EnterScope(ODataCollectionReaderState.CollectionStart, odataCollectionStart);
			return true;
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x00045108 File Offset: 0x00043308
		protected override bool ReadAtCollectionStartImplementation()
		{
			if (this.verboseJsonCollectionDeserializer.JsonReader.NodeType == JsonNodeType.EndArray)
			{
				base.ReplaceScope(ODataCollectionReaderState.CollectionEnd, this.Item);
			}
			else
			{
				object obj = this.verboseJsonCollectionDeserializer.ReadCollectionItem(base.ExpectedItemTypeReference, base.CollectionValidator);
				base.EnterScope(ODataCollectionReaderState.Value, obj);
			}
			return true;
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x00045158 File Offset: 0x00043358
		protected override bool ReadAtValueImplementation()
		{
			if (this.verboseJsonCollectionDeserializer.JsonReader.NodeType == JsonNodeType.EndArray)
			{
				base.PopScope(ODataCollectionReaderState.Value);
				base.ReplaceScope(ODataCollectionReaderState.CollectionEnd, this.Item);
			}
			else
			{
				object obj = this.verboseJsonCollectionDeserializer.ReadCollectionItem(base.ExpectedItemTypeReference, base.CollectionValidator);
				base.ReplaceScope(ODataCollectionReaderState.Value, obj);
			}
			return true;
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x000451AF File Offset: 0x000433AF
		protected override bool ReadAtCollectionEndImplementation()
		{
			base.PopScope(ODataCollectionReaderState.CollectionEnd);
			this.verboseJsonCollectionDeserializer.ReadCollectionEnd(this.IsResultsWrapperExpected);
			this.verboseJsonCollectionDeserializer.ReadPayloadEnd(base.IsReadingNestedPayload);
			base.ReplaceScope(ODataCollectionReaderState.Completed, null);
			return false;
		}

		// Token: 0x0400069B RID: 1691
		private readonly ODataVerboseJsonInputContext verboseJsonInputContext;

		// Token: 0x0400069C RID: 1692
		private readonly ODataVerboseJsonCollectionDeserializer verboseJsonCollectionDeserializer;
	}
}
