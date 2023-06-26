using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Json;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x0200020C RID: 524
	internal sealed class ODataVerboseJsonReader : ODataReaderCore
	{
		// Token: 0x06000FF9 RID: 4089 RVA: 0x0003A6A4 File Offset: 0x000388A4
		internal ODataVerboseJsonReader(ODataVerboseJsonInputContext verboseJsonInputContext, IEdmEntitySet entitySet, IEdmEntityType expectedEntityType, bool readingFeed, IODataReaderWriterListener listener)
			: base(verboseJsonInputContext, readingFeed, listener)
		{
			this.verboseJsonInputContext = verboseJsonInputContext;
			this.verboseJsonEntryAndFeedDeserializer = new ODataVerboseJsonEntryAndFeedDeserializer(verboseJsonInputContext);
			if (!this.verboseJsonInputContext.Model.IsUserModel())
			{
				throw new ODataException(Strings.ODataJsonReader_ParsingWithoutMetadata);
			}
			base.EnterScope(new ODataReaderCore.Scope(ODataReaderState.Start, null, entitySet, expectedEntityType));
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000FFA RID: 4090 RVA: 0x0003A6FB File Offset: 0x000388FB
		private IODataVerboseJsonReaderEntryState CurrentEntryState
		{
			get
			{
				return (IODataVerboseJsonReaderEntryState)base.CurrentScope;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000FFB RID: 4091 RVA: 0x0003A708 File Offset: 0x00038908
		private ODataVerboseJsonReader.JsonScope CurrentJsonScope
		{
			get
			{
				return (ODataVerboseJsonReader.JsonScope)base.CurrentScope;
			}
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x0003A715 File Offset: 0x00038915
		protected override bool ReadAtStartImplementation()
		{
			this.verboseJsonEntryAndFeedDeserializer.ReadPayloadStart(base.IsReadingNestedPayload);
			if (base.ReadingFeed)
			{
				this.ReadFeedStart(false);
				return true;
			}
			this.ReadEntryStart();
			return true;
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x0003A740 File Offset: 0x00038940
		protected override bool ReadAtFeedStartImplementation()
		{
			JsonNodeType nodeType = this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType;
			if (nodeType != JsonNodeType.StartObject)
			{
				if (nodeType != JsonNodeType.EndArray)
				{
					throw new ODataException(Strings.ODataJsonReader_CannotReadEntriesOfFeed(this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType));
				}
				this.verboseJsonEntryAndFeedDeserializer.ReadFeedEnd(base.CurrentFeed, this.CurrentJsonScope.FeedHasResultsWrapper, base.IsExpandedLinkContent);
				this.ReplaceScope(ODataReaderState.FeedEnd);
			}
			else
			{
				this.ReadEntryStart();
			}
			return true;
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x0003A7BC File Offset: 0x000389BC
		protected override bool ReadAtFeedEndImplementation()
		{
			bool isTopLevel = base.IsTopLevel;
			base.PopScope(ODataReaderState.FeedEnd);
			bool flag;
			if (isTopLevel)
			{
				this.verboseJsonEntryAndFeedDeserializer.JsonReader.Read();
				this.verboseJsonEntryAndFeedDeserializer.ReadPayloadEnd(base.IsReadingNestedPayload);
				this.ReplaceScope(ODataReaderState.Completed);
				flag = false;
			}
			else
			{
				if (this.verboseJsonInputContext.ReadingResponse)
				{
					this.verboseJsonEntryAndFeedDeserializer.JsonReader.Read();
					this.ReadExpandedNavigationLinkEnd(true);
				}
				else
				{
					this.ReadExpandedCollectionNavigationLinkContentInRequest();
				}
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x0003A838 File Offset: 0x00038A38
		protected override bool ReadAtEntryStartImplementation()
		{
			if (base.CurrentEntry == null)
			{
				this.EndEntry();
			}
			else if (this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType == JsonNodeType.EndObject)
			{
				this.EndEntry();
			}
			else if (this.verboseJsonInputContext.UseServerApiBehavior)
			{
				IEdmNavigationProperty edmNavigationProperty;
				ODataNavigationLink odataNavigationLink = this.verboseJsonEntryAndFeedDeserializer.ReadEntryContent(this.CurrentEntryState, out edmNavigationProperty);
				if (odataNavigationLink != null)
				{
					this.StartNavigationLink(odataNavigationLink, edmNavigationProperty);
				}
				else
				{
					this.EndEntry();
				}
			}
			else
			{
				this.StartNavigationLink(this.CurrentEntryState.FirstNavigationLink, this.CurrentEntryState.FirstNavigationProperty);
			}
			return true;
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x0003A8C4 File Offset: 0x00038AC4
		protected override bool ReadAtEntryEndImplementation()
		{
			bool isTopLevel = base.IsTopLevel;
			bool isExpandedLinkContent = base.IsExpandedLinkContent;
			base.PopScope(ODataReaderState.EntryEnd);
			this.verboseJsonEntryAndFeedDeserializer.JsonReader.Read();
			JsonNodeType nodeType = this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType;
			bool flag = true;
			if (isTopLevel)
			{
				this.verboseJsonEntryAndFeedDeserializer.ReadPayloadEnd(base.IsReadingNestedPayload);
				this.ReplaceScope(ODataReaderState.Completed);
				flag = false;
			}
			else if (isExpandedLinkContent)
			{
				this.ReadExpandedNavigationLinkEnd(false);
			}
			else if (this.CurrentJsonScope.FeedInExpandedNavigationLinkInRequest)
			{
				this.ReadExpandedCollectionNavigationLinkContentInRequest();
			}
			else
			{
				JsonNodeType jsonNodeType = nodeType;
				if (jsonNodeType != JsonNodeType.StartObject)
				{
					if (jsonNodeType != JsonNodeType.EndArray)
					{
						throw new ODataException(Strings.ODataJsonReader_CannotReadEntriesOfFeed(this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType));
					}
					this.verboseJsonEntryAndFeedDeserializer.ReadFeedEnd(base.CurrentFeed, this.CurrentJsonScope.FeedHasResultsWrapper, base.IsExpandedLinkContent);
					this.ReplaceScope(ODataReaderState.FeedEnd);
				}
				else
				{
					this.ReadEntryStart();
				}
			}
			return flag;
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x0003A9B4 File Offset: 0x00038BB4
		protected override bool ReadAtNavigationLinkStartImplementation()
		{
			ODataNavigationLink currentNavigationLink = base.CurrentNavigationLink;
			IODataVerboseJsonReaderEntryState iodataVerboseJsonReaderEntryState = (IODataVerboseJsonReaderEntryState)base.LinkParentEntityScope;
			if (this.verboseJsonInputContext.ReadingResponse && this.verboseJsonEntryAndFeedDeserializer.IsDeferredLink(true))
			{
				ReaderUtils.CheckForDuplicateNavigationLinkNameAndSetAssociationLink(iodataVerboseJsonReaderEntryState.DuplicatePropertyNamesChecker, currentNavigationLink, false, currentNavigationLink.IsCollection);
				this.verboseJsonEntryAndFeedDeserializer.ReadDeferredNavigationLink(currentNavigationLink);
				this.ReplaceScope(ODataReaderState.NavigationLinkEnd);
			}
			else if (!currentNavigationLink.IsCollection.Value)
			{
				if (!this.verboseJsonInputContext.ReadingResponse && this.verboseJsonEntryAndFeedDeserializer.IsEntityReferenceLink())
				{
					ReaderUtils.CheckForDuplicateNavigationLinkNameAndSetAssociationLink(iodataVerboseJsonReaderEntryState.DuplicatePropertyNamesChecker, currentNavigationLink, false, new bool?(false));
					ODataEntityReferenceLink odataEntityReferenceLink = this.verboseJsonEntryAndFeedDeserializer.ReadEntityReferenceLink();
					this.EnterScope(ODataReaderState.EntityReferenceLink, odataEntityReferenceLink, null);
				}
				else
				{
					ReaderUtils.CheckForDuplicateNavigationLinkNameAndSetAssociationLink(iodataVerboseJsonReaderEntryState.DuplicatePropertyNamesChecker, currentNavigationLink, true, new bool?(false));
					if (this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType == JsonNodeType.PrimitiveValue)
					{
						this.EnterScope(ODataReaderState.EntryStart, null, base.CurrentEntityType);
					}
					else
					{
						this.ReadEntryStart();
					}
				}
			}
			else
			{
				ReaderUtils.CheckForDuplicateNavigationLinkNameAndSetAssociationLink(iodataVerboseJsonReaderEntryState.DuplicatePropertyNamesChecker, currentNavigationLink, true, new bool?(true));
				if (this.verboseJsonInputContext.ReadingResponse)
				{
					this.ReadFeedStart(true);
				}
				else
				{
					if (this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType != JsonNodeType.StartObject && this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType != JsonNodeType.StartArray)
					{
						throw new ODataException(Strings.ODataJsonReader_CannotReadFeedStart(this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType));
					}
					bool flag = this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType == JsonNodeType.StartObject;
					this.verboseJsonEntryAndFeedDeserializer.ReadFeedStart(new ODataFeed(), flag, true);
					this.CurrentJsonScope.FeedHasResultsWrapper = flag;
					this.ReadExpandedCollectionNavigationLinkContentInRequest();
				}
			}
			return true;
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x0003AB64 File Offset: 0x00038D64
		protected override bool ReadAtNavigationLinkEndImplementation()
		{
			base.PopScope(ODataReaderState.NavigationLinkEnd);
			IEdmNavigationProperty edmNavigationProperty;
			ODataNavigationLink odataNavigationLink = this.verboseJsonEntryAndFeedDeserializer.ReadEntryContent(this.CurrentEntryState, out edmNavigationProperty);
			if (odataNavigationLink == null)
			{
				this.EndEntry();
			}
			else
			{
				this.StartNavigationLink(odataNavigationLink, edmNavigationProperty);
			}
			return true;
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x0003ABA0 File Offset: 0x00038DA0
		protected override bool ReadAtEntityReferenceLink()
		{
			base.PopScope(ODataReaderState.EntityReferenceLink);
			if (base.CurrentNavigationLink.IsCollection == true)
			{
				this.ReadExpandedCollectionNavigationLinkContentInRequest();
			}
			else
			{
				this.ReplaceScope(ODataReaderState.NavigationLinkEnd);
			}
			return true;
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x0003ABE8 File Offset: 0x00038DE8
		private void ReadFeedStart(bool isExpandedLinkContent)
		{
			ODataFeed odataFeed = new ODataFeed();
			if (this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType != JsonNodeType.StartObject && this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType != JsonNodeType.StartArray)
			{
				throw new ODataException(Strings.ODataJsonReader_CannotReadFeedStart(this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType));
			}
			bool flag = this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType == JsonNodeType.StartObject;
			this.verboseJsonEntryAndFeedDeserializer.ReadFeedStart(odataFeed, flag, isExpandedLinkContent);
			this.EnterScope(ODataReaderState.FeedStart, odataFeed, base.CurrentEntityType);
			this.CurrentJsonScope.FeedHasResultsWrapper = flag;
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x0003AC80 File Offset: 0x00038E80
		private void ReadExpandedCollectionNavigationLinkContentInRequest()
		{
			if (this.verboseJsonEntryAndFeedDeserializer.IsEntityReferenceLink())
			{
				if (this.State == ODataReaderState.FeedStart)
				{
					this.ReplaceScope(ODataReaderState.FeedEnd);
					return;
				}
				this.CurrentJsonScope.ExpandedNavigationLinkInRequestHasContent = true;
				ODataEntityReferenceLink odataEntityReferenceLink = this.verboseJsonEntryAndFeedDeserializer.ReadEntityReferenceLink();
				this.EnterScope(ODataReaderState.EntityReferenceLink, odataEntityReferenceLink, null);
				return;
			}
			else if (this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType == JsonNodeType.EndArray || this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType == JsonNodeType.EndObject)
			{
				if (this.State == ODataReaderState.FeedStart)
				{
					this.verboseJsonEntryAndFeedDeserializer.ReadFeedEnd(base.CurrentFeed, this.CurrentJsonScope.FeedHasResultsWrapper, true);
					this.ReplaceScope(ODataReaderState.FeedEnd);
					return;
				}
				if (!this.CurrentJsonScope.ExpandedNavigationLinkInRequestHasContent)
				{
					this.CurrentJsonScope.ExpandedNavigationLinkInRequestHasContent = true;
					this.EnterScope(ODataReaderState.FeedStart, new ODataFeed(), base.CurrentEntityType);
					this.CurrentJsonScope.FeedInExpandedNavigationLinkInRequest = true;
					return;
				}
				if (this.CurrentJsonScope.FeedHasResultsWrapper)
				{
					ODataFeed odataFeed = new ODataFeed();
					this.verboseJsonEntryAndFeedDeserializer.ReadFeedEnd(odataFeed, true, true);
				}
				this.verboseJsonEntryAndFeedDeserializer.JsonReader.Read();
				this.ReadExpandedNavigationLinkEnd(true);
				return;
			}
			else
			{
				if (this.State != ODataReaderState.FeedStart)
				{
					this.CurrentJsonScope.ExpandedNavigationLinkInRequestHasContent = true;
					this.EnterScope(ODataReaderState.FeedStart, new ODataFeed(), base.CurrentEntityType);
					this.CurrentJsonScope.FeedInExpandedNavigationLinkInRequest = true;
					return;
				}
				if (this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType != JsonNodeType.StartObject)
				{
					throw new ODataException(Strings.ODataJsonReader_CannotReadEntriesOfFeed(this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType));
				}
				this.ReadEntryStart();
				return;
			}
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x0003AE04 File Offset: 0x00039004
		private void ReadEntryStart()
		{
			this.verboseJsonEntryAndFeedDeserializer.ReadEntryStart();
			this.StartEntry();
			this.ReadEntryMetadata();
			if (this.verboseJsonInputContext.UseServerApiBehavior)
			{
				this.CurrentEntryState.FirstNavigationLink = null;
				this.CurrentEntryState.FirstNavigationProperty = null;
				return;
			}
			IEdmNavigationProperty edmNavigationProperty;
			this.CurrentEntryState.FirstNavigationLink = this.verboseJsonEntryAndFeedDeserializer.ReadEntryContent(this.CurrentEntryState, out edmNavigationProperty);
			this.CurrentEntryState.FirstNavigationProperty = edmNavigationProperty;
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x0003AE78 File Offset: 0x00039078
		private void ReadEntryMetadata()
		{
			this.verboseJsonEntryAndFeedDeserializer.JsonReader.StartBuffering();
			bool flag = false;
			while (this.verboseJsonEntryAndFeedDeserializer.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text = this.verboseJsonEntryAndFeedDeserializer.JsonReader.ReadPropertyName();
				if (string.CompareOrdinal(text, "__metadata") == 0)
				{
					flag = true;
					break;
				}
				this.verboseJsonEntryAndFeedDeserializer.JsonReader.SkipValue();
			}
			string text2 = null;
			object obj = null;
			if (flag)
			{
				obj = this.verboseJsonEntryAndFeedDeserializer.JsonReader.BookmarkCurrentPosition();
				text2 = this.verboseJsonEntryAndFeedDeserializer.ReadTypeNameFromMetadataPropertyValue();
			}
			base.ApplyEntityTypeNameFromPayload(text2);
			if (base.CurrentFeedValidator != null)
			{
				base.CurrentFeedValidator.ValidateEntry(base.CurrentEntityType);
			}
			if (flag)
			{
				this.verboseJsonEntryAndFeedDeserializer.JsonReader.MoveToBookmark(obj);
				this.verboseJsonEntryAndFeedDeserializer.ReadEntryMetadataPropertyValue(this.CurrentEntryState);
			}
			this.verboseJsonEntryAndFeedDeserializer.JsonReader.StopBuffering();
			this.verboseJsonEntryAndFeedDeserializer.ValidateEntryMetadata(this.CurrentEntryState);
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x0003AF68 File Offset: 0x00039168
		private void ReadExpandedNavigationLinkEnd(bool isCollection)
		{
			base.CurrentNavigationLink.IsCollection = new bool?(isCollection);
			this.ReplaceScope(ODataReaderState.NavigationLinkEnd);
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x0003AF82 File Offset: 0x00039182
		private void StartEntry()
		{
			this.EnterScope(ODataReaderState.EntryStart, ReaderUtils.CreateNewEntry(), base.CurrentEntityType);
			this.CurrentJsonScope.DuplicatePropertyNamesChecker = this.verboseJsonInputContext.CreateDuplicatePropertyNamesChecker();
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x0003AFAC File Offset: 0x000391AC
		private void StartNavigationLink(ODataNavigationLink navigationLink, IEdmNavigationProperty navigationProperty)
		{
			IEdmEntityType edmEntityType = null;
			if (navigationProperty != null)
			{
				IEdmTypeReference type = navigationProperty.Type;
				edmEntityType = (type.IsCollection() ? type.AsCollection().ElementType().AsEntity()
					.EntityDefinition() : type.AsEntity().EntityDefinition());
			}
			this.EnterScope(ODataReaderState.NavigationLinkStart, navigationLink, edmEntityType);
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x0003AFF9 File Offset: 0x000391F9
		private void EnterScope(ODataReaderState state, ODataItem item, IEdmEntityType expectedEntityType)
		{
			base.EnterScope(new ODataVerboseJsonReader.JsonScope(state, item, expectedEntityType));
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x0003B009 File Offset: 0x00039209
		private void ReplaceScope(ODataReaderState state)
		{
			base.ReplaceScope(new ODataVerboseJsonReader.JsonScope(state, this.Item, base.CurrentEntityType));
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x0003B023 File Offset: 0x00039223
		private void EndEntry()
		{
			base.EndEntry(new ODataVerboseJsonReader.JsonScope(ODataReaderState.EntryEnd, this.Item, base.CurrentEntityType));
		}

		// Token: 0x040005E6 RID: 1510
		private readonly ODataVerboseJsonInputContext verboseJsonInputContext;

		// Token: 0x040005E7 RID: 1511
		private readonly ODataVerboseJsonEntryAndFeedDeserializer verboseJsonEntryAndFeedDeserializer;

		// Token: 0x0200020E RID: 526
		private sealed class JsonScope : ODataReaderCore.Scope, IODataVerboseJsonReaderEntryState
		{
			// Token: 0x06001017 RID: 4119 RVA: 0x0003B03D File Offset: 0x0003923D
			internal JsonScope(ODataReaderState state, ODataItem item, IEdmEntityType expectedEntityType)
				: base(state, item, null, expectedEntityType)
			{
			}

			// Token: 0x1700035C RID: 860
			// (get) Token: 0x06001018 RID: 4120 RVA: 0x0003B049 File Offset: 0x00039249
			// (set) Token: 0x06001019 RID: 4121 RVA: 0x0003B051 File Offset: 0x00039251
			public bool MetadataPropertyFound { get; set; }

			// Token: 0x1700035D RID: 861
			// (get) Token: 0x0600101A RID: 4122 RVA: 0x0003B05A File Offset: 0x0003925A
			// (set) Token: 0x0600101B RID: 4123 RVA: 0x0003B062 File Offset: 0x00039262
			public ODataNavigationLink FirstNavigationLink { get; set; }

			// Token: 0x1700035E RID: 862
			// (get) Token: 0x0600101C RID: 4124 RVA: 0x0003B06B File Offset: 0x0003926B
			// (set) Token: 0x0600101D RID: 4125 RVA: 0x0003B073 File Offset: 0x00039273
			public IEdmNavigationProperty FirstNavigationProperty { get; set; }

			// Token: 0x1700035F RID: 863
			// (get) Token: 0x0600101E RID: 4126 RVA: 0x0003B07C File Offset: 0x0003927C
			// (set) Token: 0x0600101F RID: 4127 RVA: 0x0003B084 File Offset: 0x00039284
			public DuplicatePropertyNamesChecker DuplicatePropertyNamesChecker { get; set; }

			// Token: 0x17000360 RID: 864
			// (get) Token: 0x06001020 RID: 4128 RVA: 0x0003B08D File Offset: 0x0003928D
			// (set) Token: 0x06001021 RID: 4129 RVA: 0x0003B095 File Offset: 0x00039295
			public bool FeedInExpandedNavigationLinkInRequest { get; set; }

			// Token: 0x17000361 RID: 865
			// (get) Token: 0x06001022 RID: 4130 RVA: 0x0003B09E File Offset: 0x0003929E
			// (set) Token: 0x06001023 RID: 4131 RVA: 0x0003B0A6 File Offset: 0x000392A6
			public bool FeedHasResultsWrapper { get; set; }

			// Token: 0x17000362 RID: 866
			// (get) Token: 0x06001024 RID: 4132 RVA: 0x0003B0AF File Offset: 0x000392AF
			// (set) Token: 0x06001025 RID: 4133 RVA: 0x0003B0B7 File Offset: 0x000392B7
			public bool ExpandedNavigationLinkInRequestHasContent { get; set; }

			// Token: 0x17000363 RID: 867
			// (get) Token: 0x06001026 RID: 4134 RVA: 0x0003B0C0 File Offset: 0x000392C0
			ODataEntry IODataVerboseJsonReaderEntryState.Entry
			{
				get
				{
					return (ODataEntry)base.Item;
				}
			}

			// Token: 0x17000364 RID: 868
			// (get) Token: 0x06001027 RID: 4135 RVA: 0x0003B0CD File Offset: 0x000392CD
			IEdmEntityType IODataVerboseJsonReaderEntryState.EntityType
			{
				get
				{
					return base.EntityType;
				}
			}
		}
	}
}
