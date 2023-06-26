using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData;

namespace System.Data.Services.Client.Materialization
{
	// Token: 0x0200003A RID: 58
	internal class ODataReaderEntityMaterializer : ODataEntityMaterializer
	{
		// Token: 0x060001E7 RID: 487 RVA: 0x0000A8FC File Offset: 0x00008AFC
		public ODataReaderEntityMaterializer(ODataMessageReader odataMessageReader, ODataReaderWrapper reader, IODataMaterializerContext materializerContext, EntityTrackingAdapter entityTrackingAdapter, QueryComponents queryComponents, Type expectedType, ProjectionPlan materializeEntryPlan)
			: base(materializerContext, entityTrackingAdapter, queryComponents, expectedType, materializeEntryPlan)
		{
			this.messageReader = odataMessageReader;
			this.feedEntryAdapter = new FeedAndEntryMaterializerAdapter(odataMessageReader, reader, materializerContext.Model, entityTrackingAdapter.MergeOption);
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000A92E File Offset: 0x00008B2E
		internal override ODataFeed CurrentFeed
		{
			get
			{
				return this.feedEntryAdapter.CurrentFeed;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000A93B File Offset: 0x00008B3B
		internal override ODataEntry CurrentEntry
		{
			get
			{
				return this.feedEntryAdapter.CurrentEntry;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000A948 File Offset: 0x00008B48
		internal override bool IsEndOfStream
		{
			get
			{
				return this.IsDisposed || this.feedEntryAdapter.IsEndOfStream;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000A95F File Offset: 0x00008B5F
		internal override long CountValue
		{
			get
			{
				return this.feedEntryAdapter.GetCountValue(!this.IsDisposed);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000A975 File Offset: 0x00008B75
		internal override bool IsCountable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000A978 File Offset: 0x00008B78
		protected override bool IsDisposed
		{
			get
			{
				return this.messageReader == null;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001EE RID: 494 RVA: 0x0000A983 File Offset: 0x00008B83
		protected override ODataFormat Format
		{
			get
			{
				return ODataUtils.GetReadFormat(this.messageReader);
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000A990 File Offset: 0x00008B90
		internal static MaterializerEntry ParseSingleEntityPayload(IODataResponseMessage message, ResponseInfo responseInfo, Type expectedType)
		{
			ODataPayloadKind odataPayloadKind = ODataPayloadKind.Entry;
			MaterializerEntry entry;
			using (ODataMessageReader odataMessageReader = ODataMaterializer.CreateODataMessageReader(message, responseInfo, ref odataPayloadKind))
			{
				IEdmType edmType = responseInfo.TypeResolver.ResolveExpectedTypeForReading(expectedType);
				ODataReaderWrapper odataReaderWrapper = ODataReaderWrapper.Create(odataMessageReader, odataPayloadKind, edmType, responseInfo.ResponsePipeline);
				FeedAndEntryMaterializerAdapter feedAndEntryMaterializerAdapter = new FeedAndEntryMaterializerAdapter(odataMessageReader, odataReaderWrapper, responseInfo.Model, responseInfo.MergeOption);
				ODataEntry odataEntry = null;
				bool flag = false;
				while (feedAndEntryMaterializerAdapter.Read())
				{
					flag |= feedAndEntryMaterializerAdapter.CurrentFeed != null;
					if (feedAndEntryMaterializerAdapter.CurrentEntry != null)
					{
						if (odataEntry != null)
						{
							throw new InvalidOperationException(Strings.AtomParser_SingleEntry_MultipleFound);
						}
						odataEntry = feedAndEntryMaterializerAdapter.CurrentEntry;
					}
				}
				if (odataEntry == null)
				{
					if (flag)
					{
						throw new InvalidOperationException(Strings.AtomParser_SingleEntry_NoneFound);
					}
					throw new InvalidOperationException(Strings.AtomParser_SingleEntry_ExpectedFeedOrEntry);
				}
				else
				{
					entry = MaterializerEntry.GetEntry(odataEntry);
				}
			}
			return entry;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000AA64 File Offset: 0x00008C64
		protected override void OnDispose()
		{
			if (this.messageReader != null)
			{
				this.messageReader.Dispose();
				this.messageReader = null;
			}
			this.feedEntryAdapter.Dispose();
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000AA8B File Offset: 0x00008C8B
		protected override bool ReadNextFeedOrEntry()
		{
			return this.feedEntryAdapter.Read();
		}

		// Token: 0x04000210 RID: 528
		private FeedAndEntryMaterializerAdapter feedEntryAdapter;

		// Token: 0x04000211 RID: 529
		private ODataMessageReader messageReader;
	}
}
