using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Evaluation;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x0200019C RID: 412
	internal sealed class ODataJsonLightReader : ODataReaderCoreAsync
	{
		// Token: 0x06000C76 RID: 3190 RVA: 0x0002B169 File Offset: 0x00029369
		internal ODataJsonLightReader(ODataJsonLightInputContext jsonLightInputContext, IEdmEntitySet entitySet, IEdmEntityType expectedEntityType, bool readingFeed, IODataReaderWriterListener listener)
			: base(jsonLightInputContext, readingFeed, listener)
		{
			this.jsonLightInputContext = jsonLightInputContext;
			this.jsonLightEntryAndFeedDeserializer = new ODataJsonLightEntryAndFeedDeserializer(jsonLightInputContext);
			this.topLevelScope = new ODataJsonLightReader.JsonLightTopLevelScope(entitySet, expectedEntityType);
			base.EnterScope(this.topLevelScope);
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x0002B1A2 File Offset: 0x000293A2
		private IODataJsonLightReaderEntryState CurrentEntryState
		{
			get
			{
				return (IODataJsonLightReaderEntryState)base.CurrentScope;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x0002B1AF File Offset: 0x000293AF
		private ODataJsonLightReader.JsonLightFeedScope CurrentJsonLightFeedScope
		{
			get
			{
				return (ODataJsonLightReader.JsonLightFeedScope)base.CurrentScope;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000C79 RID: 3193 RVA: 0x0002B1BC File Offset: 0x000293BC
		private ODataJsonLightReader.JsonLightNavigationLinkScope CurrentJsonLightNavigationLinkScope
		{
			get
			{
				return (ODataJsonLightReader.JsonLightNavigationLinkScope)base.CurrentScope;
			}
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x0002B1CC File Offset: 0x000293CC
		protected override bool ReadAtStartImplementation()
		{
			DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = this.jsonLightInputContext.CreateDuplicatePropertyNamesChecker();
			ODataPayloadKind odataPayloadKind = (base.ReadingFeed ? ODataPayloadKind.Feed : ODataPayloadKind.Entry);
			this.jsonLightEntryAndFeedDeserializer.ReadPayloadStart(odataPayloadKind, duplicatePropertyNamesChecker, base.IsReadingNestedPayload, false);
			return this.ReadAtStartImplementationSynchronously(duplicatePropertyNamesChecker);
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x0002B228 File Offset: 0x00029428
		protected override Task<bool> ReadAtStartImplementationAsync()
		{
			DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = this.jsonLightInputContext.CreateDuplicatePropertyNamesChecker();
			ODataPayloadKind odataPayloadKind = (base.ReadingFeed ? ODataPayloadKind.Feed : ODataPayloadKind.Entry);
			return this.jsonLightEntryAndFeedDeserializer.ReadPayloadStartAsync(odataPayloadKind, duplicatePropertyNamesChecker, base.IsReadingNestedPayload, false).FollowOnSuccessWith((Task t) => this.ReadAtStartImplementationSynchronously(duplicatePropertyNamesChecker));
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x0002B28A File Offset: 0x0002948A
		protected override bool ReadAtFeedStartImplementation()
		{
			return this.ReadAtFeedStartImplementationSynchronously();
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x0002B292 File Offset: 0x00029492
		protected override Task<bool> ReadAtFeedStartImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtFeedStartImplementationSynchronously));
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x0002B2A5 File Offset: 0x000294A5
		protected override bool ReadAtFeedEndImplementation()
		{
			return this.ReadAtFeedEndImplementationSynchronously();
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x0002B2AD File Offset: 0x000294AD
		protected override Task<bool> ReadAtFeedEndImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtFeedEndImplementationSynchronously));
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x0002B2C0 File Offset: 0x000294C0
		protected override bool ReadAtEntryStartImplementation()
		{
			return this.ReadAtEntryStartImplementationSynchronously();
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x0002B2C8 File Offset: 0x000294C8
		protected override Task<bool> ReadAtEntryStartImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtEntryStartImplementationSynchronously));
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x0002B2DB File Offset: 0x000294DB
		protected override bool ReadAtEntryEndImplementation()
		{
			return this.ReadAtEntryEndImplementationSynchronously();
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x0002B2E3 File Offset: 0x000294E3
		protected override Task<bool> ReadAtEntryEndImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtEntryEndImplementationSynchronously));
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x0002B2F6 File Offset: 0x000294F6
		protected override bool ReadAtNavigationLinkStartImplementation()
		{
			return this.ReadAtNavigationLinkStartImplementationSynchronously();
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x0002B2FE File Offset: 0x000294FE
		protected override Task<bool> ReadAtNavigationLinkStartImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtNavigationLinkStartImplementationSynchronously));
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x0002B311 File Offset: 0x00029511
		protected override bool ReadAtNavigationLinkEndImplementation()
		{
			return this.ReadAtNavigationLinkEndImplementationSynchronously();
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x0002B319 File Offset: 0x00029519
		protected override Task<bool> ReadAtNavigationLinkEndImplementationAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtNavigationLinkEndImplementationSynchronously));
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x0002B32C File Offset: 0x0002952C
		protected override bool ReadAtEntityReferenceLink()
		{
			return this.ReadAtEntityReferenceLinkSynchronously();
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x0002B334 File Offset: 0x00029534
		protected override Task<bool> ReadAtEntityReferenceLinkAsync()
		{
			return TaskUtils.GetTaskForSynchronousOperation<bool>(new Func<bool>(this.ReadAtEntityReferenceLinkSynchronously));
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x0002B348 File Offset: 0x00029548
		private bool ReadAtStartImplementationSynchronously(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			if (this.jsonLightInputContext.ReadingResponse)
			{
				ReaderValidationUtils.ValidateFeedOrEntryMetadataUri(this.jsonLightEntryAndFeedDeserializer.MetadataUriParseResult, base.CurrentScope);
			}
			string text = ((this.jsonLightEntryAndFeedDeserializer.MetadataUriParseResult == null) ? null : this.jsonLightEntryAndFeedDeserializer.MetadataUriParseResult.SelectQueryOption);
			SelectedPropertiesNode selectedPropertiesNode = SelectedPropertiesNode.Create(text);
			if (base.ReadingFeed)
			{
				ODataFeed odataFeed = new ODataFeed();
				this.topLevelScope.DuplicatePropertyNamesChecker = duplicatePropertyNamesChecker;
				bool flag = this.jsonLightInputContext.JsonReader is ReorderingJsonReader;
				this.jsonLightEntryAndFeedDeserializer.ReadTopLevelFeedAnnotations(odataFeed, duplicatePropertyNamesChecker, true, flag);
				this.ReadFeedStart(odataFeed, selectedPropertiesNode);
				return true;
			}
			this.ReadEntryStart(duplicatePropertyNamesChecker, selectedPropertiesNode);
			return true;
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x0002B3F0 File Offset: 0x000295F0
		private bool ReadAtFeedStartImplementationSynchronously()
		{
			JsonNodeType nodeType = this.jsonLightEntryAndFeedDeserializer.JsonReader.NodeType;
			if (nodeType != JsonNodeType.StartObject)
			{
				if (nodeType != JsonNodeType.EndArray)
				{
					throw new ODataException(Strings.ODataJsonReader_CannotReadEntriesOfFeed(this.jsonLightEntryAndFeedDeserializer.JsonReader.NodeType));
				}
				this.ReadFeedEnd();
			}
			else
			{
				this.ReadEntryStart(null, this.CurrentJsonLightFeedScope.SelectedProperties);
			}
			return true;
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x0002B458 File Offset: 0x00029658
		private bool ReadAtFeedEndImplementationSynchronously()
		{
			bool isTopLevel = base.IsTopLevel;
			base.PopScope(ODataReaderState.FeedEnd);
			if (isTopLevel)
			{
				this.jsonLightEntryAndFeedDeserializer.JsonReader.Read();
				this.jsonLightEntryAndFeedDeserializer.ReadPayloadEnd(base.IsReadingNestedPayload);
				this.ReplaceScope(ODataReaderState.Completed);
				return false;
			}
			this.ReadExpandedNavigationLinkEnd(true);
			return true;
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x0002B4AC File Offset: 0x000296AC
		private bool ReadAtEntryStartImplementationSynchronously()
		{
			if (base.CurrentEntry == null)
			{
				this.EndEntry();
			}
			else if (this.jsonLightInputContext.UseServerApiBehavior)
			{
				ODataJsonLightReaderNavigationLinkInfo odataJsonLightReaderNavigationLinkInfo = this.jsonLightEntryAndFeedDeserializer.ReadEntryContent(this.CurrentEntryState);
				if (odataJsonLightReaderNavigationLinkInfo != null)
				{
					this.StartNavigationLink(odataJsonLightReaderNavigationLinkInfo);
				}
				else
				{
					this.EndEntry();
				}
			}
			else if (this.CurrentEntryState.FirstNavigationLinkInfo != null)
			{
				this.StartNavigationLink(this.CurrentEntryState.FirstNavigationLinkInfo);
			}
			else
			{
				this.EndEntry();
			}
			return true;
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x0002B524 File Offset: 0x00029724
		private bool ReadAtEntryEndImplementationSynchronously()
		{
			bool isTopLevel = base.IsTopLevel;
			bool isExpandedLinkContent = base.IsExpandedLinkContent;
			base.PopScope(ODataReaderState.EntryEnd);
			this.jsonLightEntryAndFeedDeserializer.JsonReader.Read();
			JsonNodeType nodeType = this.jsonLightEntryAndFeedDeserializer.JsonReader.NodeType;
			bool flag = true;
			if (isTopLevel)
			{
				this.jsonLightEntryAndFeedDeserializer.ReadPayloadEnd(base.IsReadingNestedPayload);
				this.ReplaceScope(ODataReaderState.Completed);
				flag = false;
			}
			else if (isExpandedLinkContent)
			{
				this.ReadExpandedNavigationLinkEnd(false);
			}
			else
			{
				JsonNodeType jsonNodeType = nodeType;
				if (jsonNodeType != JsonNodeType.StartObject)
				{
					if (jsonNodeType != JsonNodeType.EndArray)
					{
						throw new ODataException(Strings.ODataJsonReader_CannotReadEntriesOfFeed(this.jsonLightEntryAndFeedDeserializer.JsonReader.NodeType));
					}
					this.ReadFeedEnd();
				}
				else
				{
					this.ReadEntryStart(null, this.CurrentJsonLightFeedScope.SelectedProperties);
				}
			}
			return flag;
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x0002B5E4 File Offset: 0x000297E4
		private bool ReadAtNavigationLinkStartImplementationSynchronously()
		{
			ODataNavigationLink currentNavigationLink = base.CurrentNavigationLink;
			IODataJsonLightReaderEntryState iodataJsonLightReaderEntryState = (IODataJsonLightReaderEntryState)base.LinkParentEntityScope;
			if (this.jsonLightInputContext.ReadingResponse)
			{
				if (iodataJsonLightReaderEntryState.ProcessingMissingProjectedNavigationLinks)
				{
					this.ReplaceScope(ODataReaderState.NavigationLinkEnd);
				}
				else if (!this.jsonLightEntryAndFeedDeserializer.JsonReader.IsOnValueNode())
				{
					ReaderUtils.CheckForDuplicateNavigationLinkNameAndSetAssociationLink(iodataJsonLightReaderEntryState.DuplicatePropertyNamesChecker, currentNavigationLink, false, currentNavigationLink.IsCollection);
					iodataJsonLightReaderEntryState.NavigationPropertiesRead.Add(currentNavigationLink.Name);
					this.ReplaceScope(ODataReaderState.NavigationLinkEnd);
				}
				else if (!currentNavigationLink.IsCollection.Value)
				{
					ReaderUtils.CheckForDuplicateNavigationLinkNameAndSetAssociationLink(iodataJsonLightReaderEntryState.DuplicatePropertyNamesChecker, currentNavigationLink, true, new bool?(false));
					this.ReadExpandedEntryStart(currentNavigationLink);
				}
				else
				{
					ReaderUtils.CheckForDuplicateNavigationLinkNameAndSetAssociationLink(iodataJsonLightReaderEntryState.DuplicatePropertyNamesChecker, currentNavigationLink, true, new bool?(true));
					ODataJsonLightReaderNavigationLinkInfo navigationLinkInfo = this.CurrentJsonLightNavigationLinkScope.NavigationLinkInfo;
					ODataJsonLightReader.JsonLightEntryScope jsonLightEntryScope = (ODataJsonLightReader.JsonLightEntryScope)base.LinkParentEntityScope;
					SelectedPropertiesNode selectedProperties = jsonLightEntryScope.SelectedProperties;
					this.ReadFeedStart(navigationLinkInfo.ExpandedFeed, selectedProperties.GetSelectedPropertiesForNavigationProperty(jsonLightEntryScope.EntityType, currentNavigationLink.Name));
				}
			}
			else
			{
				ODataJsonLightReaderNavigationLinkInfo navigationLinkInfo2 = this.CurrentJsonLightNavigationLinkScope.NavigationLinkInfo;
				ReaderUtils.CheckForDuplicateNavigationLinkNameAndSetAssociationLink(iodataJsonLightReaderEntryState.DuplicatePropertyNamesChecker, currentNavigationLink, navigationLinkInfo2.IsExpanded, currentNavigationLink.IsCollection);
				this.ReadNextNavigationLinkContentItemInRequest();
			}
			return true;
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x0002B71C File Offset: 0x0002991C
		private bool ReadAtNavigationLinkEndImplementationSynchronously()
		{
			base.PopScope(ODataReaderState.NavigationLinkEnd);
			IODataJsonLightReaderEntryState currentEntryState = this.CurrentEntryState;
			ODataJsonLightReaderNavigationLinkInfo odataJsonLightReaderNavigationLinkInfo;
			if (this.jsonLightInputContext.ReadingResponse && currentEntryState.ProcessingMissingProjectedNavigationLinks)
			{
				odataJsonLightReaderNavigationLinkInfo = currentEntryState.Entry.MetadataBuilder.GetNextUnprocessedNavigationLink();
			}
			else
			{
				odataJsonLightReaderNavigationLinkInfo = this.jsonLightEntryAndFeedDeserializer.ReadEntryContent(currentEntryState);
			}
			if (odataJsonLightReaderNavigationLinkInfo == null)
			{
				this.EndEntry();
			}
			else
			{
				this.StartNavigationLink(odataJsonLightReaderNavigationLinkInfo);
			}
			return true;
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x0002B781 File Offset: 0x00029981
		private bool ReadAtEntityReferenceLinkSynchronously()
		{
			base.PopScope(ODataReaderState.EntityReferenceLink);
			this.ReadNextNavigationLinkContentItemInRequest();
			return true;
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x0002B791 File Offset: 0x00029991
		private void ReadFeedStart(ODataFeed feed, SelectedPropertiesNode selectedProperties)
		{
			this.jsonLightEntryAndFeedDeserializer.ReadFeedContentStart();
			base.EnterScope(new ODataJsonLightReader.JsonLightFeedScope(feed, base.CurrentEntitySet, base.CurrentEntityType, selectedProperties));
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x0002B7B8 File Offset: 0x000299B8
		private void ReadFeedEnd()
		{
			this.jsonLightEntryAndFeedDeserializer.ReadFeedContentEnd();
			ODataJsonLightReaderNavigationLinkInfo odataJsonLightReaderNavigationLinkInfo = null;
			ODataJsonLightReader.JsonLightNavigationLinkScope jsonLightNavigationLinkScope = (ODataJsonLightReader.JsonLightNavigationLinkScope)base.ExpandedLinkContentParentScope;
			if (jsonLightNavigationLinkScope != null)
			{
				odataJsonLightReaderNavigationLinkInfo = jsonLightNavigationLinkScope.NavigationLinkInfo;
			}
			this.jsonLightEntryAndFeedDeserializer.ReadNextLinkAnnotationAtFeedEnd(base.CurrentFeed, odataJsonLightReaderNavigationLinkInfo, this.topLevelScope.DuplicatePropertyNamesChecker);
			this.ReplaceScope(ODataReaderState.FeedEnd);
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x0002B80C File Offset: 0x00029A0C
		private void ReadExpandedEntryStart(ODataNavigationLink navigationLink)
		{
			if (this.jsonLightEntryAndFeedDeserializer.JsonReader.NodeType == JsonNodeType.PrimitiveValue)
			{
				base.EnterScope(new ODataJsonLightReader.JsonLightEntryScope(ODataReaderState.EntryStart, null, base.CurrentEntitySet, base.CurrentEntityType, null, null));
				return;
			}
			ODataJsonLightReader.JsonLightEntryScope jsonLightEntryScope = (ODataJsonLightReader.JsonLightEntryScope)base.LinkParentEntityScope;
			SelectedPropertiesNode selectedProperties = jsonLightEntryScope.SelectedProperties;
			this.ReadEntryStart(null, selectedProperties.GetSelectedPropertiesForNavigationProperty(jsonLightEntryScope.EntityType, navigationLink.Name));
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x0002B874 File Offset: 0x00029A74
		private void ReadEntryStart(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, SelectedPropertiesNode selectedProperties)
		{
			if (this.jsonLightEntryAndFeedDeserializer.JsonReader.NodeType == JsonNodeType.StartObject)
			{
				this.jsonLightEntryAndFeedDeserializer.JsonReader.Read();
			}
			this.StartEntry(duplicatePropertyNamesChecker, selectedProperties);
			if (this.jsonLightInputContext.JsonReader.NodeType == JsonNodeType.Property)
			{
				this.jsonLightEntryAndFeedDeserializer.ApplyAnnotationGroupIfPresent(this.CurrentEntryState);
			}
			this.jsonLightEntryAndFeedDeserializer.ReadEntryTypeName(this.CurrentEntryState);
			base.ApplyEntityTypeNameFromPayload(base.CurrentEntry.TypeName);
			if (base.CurrentFeedValidator != null)
			{
				base.CurrentFeedValidator.ValidateEntry(base.CurrentEntityType);
			}
			if (base.CurrentEntityType != null)
			{
				base.CurrentEntry.SetAnnotation<ODataTypeAnnotation>(new ODataTypeAnnotation(base.CurrentEntitySet, base.CurrentEntityType));
			}
			if (this.jsonLightInputContext.UseServerApiBehavior)
			{
				this.CurrentEntryState.FirstNavigationLinkInfo = null;
				return;
			}
			this.CurrentEntryState.FirstNavigationLinkInfo = this.jsonLightEntryAndFeedDeserializer.ReadEntryContent(this.CurrentEntryState);
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x0002B968 File Offset: 0x00029B68
		private void ReadExpandedNavigationLinkEnd(bool isCollection)
		{
			base.CurrentNavigationLink.IsCollection = new bool?(isCollection);
			IODataJsonLightReaderEntryState iodataJsonLightReaderEntryState = (IODataJsonLightReaderEntryState)base.LinkParentEntityScope;
			iodataJsonLightReaderEntryState.NavigationPropertiesRead.Add(base.CurrentNavigationLink.Name);
			this.ReplaceScope(ODataReaderState.NavigationLinkEnd);
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x0002B9B0 File Offset: 0x00029BB0
		private void ReadNextNavigationLinkContentItemInRequest()
		{
			ODataJsonLightReaderNavigationLinkInfo navigationLinkInfo = this.CurrentJsonLightNavigationLinkScope.NavigationLinkInfo;
			if (navigationLinkInfo.HasEntityReferenceLink)
			{
				base.EnterScope(new ODataReaderCore.Scope(ODataReaderState.EntityReferenceLink, navigationLinkInfo.ReportEntityReferenceLink(), null, null));
				return;
			}
			if (!navigationLinkInfo.IsExpanded)
			{
				this.ReplaceScope(ODataReaderState.NavigationLinkEnd);
				return;
			}
			if (navigationLinkInfo.NavigationLink.IsCollection == true)
			{
				SelectedPropertiesNode selectedPropertiesNode = SelectedPropertiesNode.Create(null);
				this.ReadFeedStart(new ODataFeed(), selectedPropertiesNode);
				return;
			}
			this.ReadExpandedEntryStart(navigationLinkInfo.NavigationLink);
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x0002BA37 File Offset: 0x00029C37
		private void StartEntry(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, SelectedPropertiesNode selectedProperties)
		{
			base.EnterScope(new ODataJsonLightReader.JsonLightEntryScope(ODataReaderState.EntryStart, ReaderUtils.CreateNewEntry(), base.CurrentEntitySet, base.CurrentEntityType, duplicatePropertyNamesChecker ?? this.jsonLightInputContext.CreateDuplicatePropertyNamesChecker(), selectedProperties));
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x0002BA68 File Offset: 0x00029C68
		private void StartNavigationLink(ODataJsonLightReaderNavigationLinkInfo navigationLinkInfo)
		{
			ODataNavigationLink navigationLink = navigationLinkInfo.NavigationLink;
			IEdmNavigationProperty navigationProperty = navigationLinkInfo.NavigationProperty;
			IEdmEntityType edmEntityType = null;
			if (navigationProperty != null)
			{
				IEdmTypeReference type = navigationProperty.Type;
				edmEntityType = (type.IsCollection() ? type.AsCollection().ElementType().AsEntity()
					.EntityDefinition() : type.AsEntity().EntityDefinition());
			}
			if (this.jsonLightInputContext.ReadingResponse)
			{
				ODataAssociationLink odataAssociationLink = new ODataAssociationLink
				{
					Name = navigationLink.Name
				};
				if (navigationLink.AssociationLinkUrl != null)
				{
					odataAssociationLink.Url = navigationLink.AssociationLinkUrl;
				}
				base.CurrentEntry.AddAssociationLink(odataAssociationLink);
				ODataEntityMetadataBuilder entityMetadataBuilderForReader = this.jsonLightEntryAndFeedDeserializer.MetadataContext.GetEntityMetadataBuilderForReader(this.CurrentEntryState);
				navigationLink.SetMetadataBuilder(entityMetadataBuilderForReader);
				odataAssociationLink.SetMetadataBuilder(entityMetadataBuilderForReader);
			}
			IEdmEntitySet edmEntitySet = ((navigationProperty == null) ? null : base.CurrentEntitySet.FindNavigationTarget(navigationProperty));
			base.EnterScope(new ODataJsonLightReader.JsonLightNavigationLinkScope(navigationLinkInfo, edmEntitySet, edmEntityType));
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x0002BB53 File Offset: 0x00029D53
		private void ReplaceScope(ODataReaderState state)
		{
			base.ReplaceScope(new ODataReaderCore.Scope(state, this.Item, base.CurrentEntitySet, base.CurrentEntityType));
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x0002BB74 File Offset: 0x00029D74
		private void EndEntry()
		{
			IODataJsonLightReaderEntryState currentEntryState = this.CurrentEntryState;
			if (currentEntryState.DuplicatePropertyNamesChecker != null)
			{
				foreach (string text in currentEntryState.DuplicatePropertyNamesChecker.GetAllUnprocessedProperties())
				{
					currentEntryState.AnyPropertyFound = true;
					ODataJsonLightReaderNavigationLinkInfo odataJsonLightReaderNavigationLinkInfo = this.jsonLightEntryAndFeedDeserializer.ReadEntryPropertyWithoutValue(currentEntryState, text);
					currentEntryState.DuplicatePropertyNamesChecker.MarkPropertyAsProcessed(text);
					if (odataJsonLightReaderNavigationLinkInfo != null)
					{
						this.StartNavigationLink(odataJsonLightReaderNavigationLinkInfo);
						return;
					}
				}
			}
			if (base.CurrentEntry != null)
			{
				ODataEntityMetadataBuilder entityMetadataBuilderForReader = this.jsonLightEntryAndFeedDeserializer.MetadataContext.GetEntityMetadataBuilderForReader(this.CurrentEntryState);
				if (entityMetadataBuilderForReader != base.CurrentEntry.MetadataBuilder)
				{
					foreach (string text2 in this.CurrentEntryState.NavigationPropertiesRead)
					{
						entityMetadataBuilderForReader.MarkNavigationLinkProcessed(text2);
					}
					base.CurrentEntry.MetadataBuilder = entityMetadataBuilderForReader;
				}
			}
			this.jsonLightEntryAndFeedDeserializer.ValidateEntryMetadata(currentEntryState);
			if (this.jsonLightInputContext.ReadingResponse && base.CurrentEntry != null)
			{
				ODataJsonLightReaderNavigationLinkInfo nextUnprocessedNavigationLink = base.CurrentEntry.MetadataBuilder.GetNextUnprocessedNavigationLink();
				if (nextUnprocessedNavigationLink != null)
				{
					this.CurrentEntryState.ProcessingMissingProjectedNavigationLinks = true;
					this.StartNavigationLink(nextUnprocessedNavigationLink);
					return;
				}
			}
			base.EndEntry(new ODataJsonLightReader.JsonLightEntryScope(ODataReaderState.EntryEnd, (ODataEntry)this.Item, base.CurrentEntitySet, base.CurrentEntityType, this.CurrentEntryState.DuplicatePropertyNamesChecker, this.CurrentEntryState.SelectedProperties));
		}

		// Token: 0x04000442 RID: 1090
		private readonly ODataJsonLightInputContext jsonLightInputContext;

		// Token: 0x04000443 RID: 1091
		private readonly ODataJsonLightEntryAndFeedDeserializer jsonLightEntryAndFeedDeserializer;

		// Token: 0x04000444 RID: 1092
		private readonly ODataJsonLightReader.JsonLightTopLevelScope topLevelScope;

		// Token: 0x0200019D RID: 413
		private sealed class JsonLightTopLevelScope : ODataReaderCore.Scope
		{
			// Token: 0x06000C9C RID: 3228 RVA: 0x0002BD0C File Offset: 0x00029F0C
			internal JsonLightTopLevelScope(IEdmEntitySet entitySet, IEdmEntityType expectedEntityType)
				: base(ODataReaderState.Start, null, entitySet, expectedEntityType)
			{
			}

			// Token: 0x170002B5 RID: 693
			// (get) Token: 0x06000C9D RID: 3229 RVA: 0x0002BD18 File Offset: 0x00029F18
			// (set) Token: 0x06000C9E RID: 3230 RVA: 0x0002BD20 File Offset: 0x00029F20
			public DuplicatePropertyNamesChecker DuplicatePropertyNamesChecker { get; set; }
		}

		// Token: 0x0200019E RID: 414
		private sealed class JsonLightEntryScope : ODataReaderCore.Scope, IODataJsonLightReaderEntryState
		{
			// Token: 0x06000C9F RID: 3231 RVA: 0x0002BD29 File Offset: 0x00029F29
			internal JsonLightEntryScope(ODataReaderState readerState, ODataEntry entry, IEdmEntitySet entitySet, IEdmEntityType expectedEntityType, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, SelectedPropertiesNode selectedProperties)
				: base(readerState, entry, entitySet, expectedEntityType)
			{
				this.DuplicatePropertyNamesChecker = duplicatePropertyNamesChecker;
				this.SelectedProperties = selectedProperties;
			}

			// Token: 0x170002B6 RID: 694
			// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x0002BD46 File Offset: 0x00029F46
			// (set) Token: 0x06000CA1 RID: 3233 RVA: 0x0002BD4E File Offset: 0x00029F4E
			public ODataEntityMetadataBuilder MetadataBuilder { get; set; }

			// Token: 0x170002B7 RID: 695
			// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x0002BD57 File Offset: 0x00029F57
			// (set) Token: 0x06000CA3 RID: 3235 RVA: 0x0002BD5F File Offset: 0x00029F5F
			public bool AnyPropertyFound { get; set; }

			// Token: 0x170002B8 RID: 696
			// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x0002BD68 File Offset: 0x00029F68
			// (set) Token: 0x06000CA5 RID: 3237 RVA: 0x0002BD70 File Offset: 0x00029F70
			public ODataJsonLightReaderNavigationLinkInfo FirstNavigationLinkInfo { get; set; }

			// Token: 0x170002B9 RID: 697
			// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x0002BD79 File Offset: 0x00029F79
			// (set) Token: 0x06000CA7 RID: 3239 RVA: 0x0002BD81 File Offset: 0x00029F81
			public DuplicatePropertyNamesChecker DuplicatePropertyNamesChecker { get; private set; }

			// Token: 0x170002BA RID: 698
			// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x0002BD8A File Offset: 0x00029F8A
			// (set) Token: 0x06000CA9 RID: 3241 RVA: 0x0002BD92 File Offset: 0x00029F92
			public SelectedPropertiesNode SelectedProperties { get; private set; }

			// Token: 0x170002BB RID: 699
			// (get) Token: 0x06000CAA RID: 3242 RVA: 0x0002BD9C File Offset: 0x00029F9C
			public List<string> NavigationPropertiesRead
			{
				get
				{
					List<string> list;
					if ((list = this.navigationPropertiesRead) == null)
					{
						list = (this.navigationPropertiesRead = new List<string>());
					}
					return list;
				}
			}

			// Token: 0x170002BC RID: 700
			// (get) Token: 0x06000CAB RID: 3243 RVA: 0x0002BDC1 File Offset: 0x00029FC1
			// (set) Token: 0x06000CAC RID: 3244 RVA: 0x0002BDC9 File Offset: 0x00029FC9
			public bool ProcessingMissingProjectedNavigationLinks { get; set; }

			// Token: 0x170002BD RID: 701
			// (get) Token: 0x06000CAD RID: 3245 RVA: 0x0002BDD2 File Offset: 0x00029FD2
			ODataEntry IODataJsonLightReaderEntryState.Entry
			{
				get
				{
					return (ODataEntry)base.Item;
				}
			}

			// Token: 0x170002BE RID: 702
			// (get) Token: 0x06000CAE RID: 3246 RVA: 0x0002BDDF File Offset: 0x00029FDF
			IEdmEntityType IODataJsonLightReaderEntryState.EntityType
			{
				get
				{
					return base.EntityType;
				}
			}

			// Token: 0x04000446 RID: 1094
			private List<string> navigationPropertiesRead;
		}

		// Token: 0x0200019F RID: 415
		private sealed class JsonLightFeedScope : ODataReaderCore.Scope
		{
			// Token: 0x06000CAF RID: 3247 RVA: 0x0002BDE7 File Offset: 0x00029FE7
			internal JsonLightFeedScope(ODataFeed feed, IEdmEntitySet entitySet, IEdmEntityType expectedEntityType, SelectedPropertiesNode selectedProperties)
				: base(ODataReaderState.FeedStart, feed, entitySet, expectedEntityType)
			{
				this.SelectedProperties = selectedProperties;
			}

			// Token: 0x170002BF RID: 703
			// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x0002BDFB File Offset: 0x00029FFB
			// (set) Token: 0x06000CB1 RID: 3249 RVA: 0x0002BE03 File Offset: 0x0002A003
			public SelectedPropertiesNode SelectedProperties { get; private set; }
		}

		// Token: 0x020001A0 RID: 416
		private sealed class JsonLightNavigationLinkScope : ODataReaderCore.Scope
		{
			// Token: 0x06000CB2 RID: 3250 RVA: 0x0002BE0C File Offset: 0x0002A00C
			internal JsonLightNavigationLinkScope(ODataJsonLightReaderNavigationLinkInfo navigationLinkInfo, IEdmEntitySet entitySet, IEdmEntityType expectedEntityType)
				: base(ODataReaderState.NavigationLinkStart, navigationLinkInfo.NavigationLink, entitySet, expectedEntityType)
			{
				this.NavigationLinkInfo = navigationLinkInfo;
			}

			// Token: 0x170002C0 RID: 704
			// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x0002BE24 File Offset: 0x0002A024
			// (set) Token: 0x06000CB4 RID: 3252 RVA: 0x0002BE2C File Offset: 0x0002A02C
			public ODataJsonLightReaderNavigationLinkInfo NavigationLinkInfo { get; private set; }
		}
	}
}
