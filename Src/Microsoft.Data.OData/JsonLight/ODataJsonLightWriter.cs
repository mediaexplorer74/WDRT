﻿using System;
using System.Threading.Tasks;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Evaluation;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x020001AC RID: 428
	internal sealed class ODataJsonLightWriter : ODataWriterCore
	{
		// Token: 0x06000D3E RID: 3390 RVA: 0x0002DF34 File Offset: 0x0002C134
		internal ODataJsonLightWriter(ODataJsonLightOutputContext jsonLightOutputContext, IEdmEntitySet entitySet, IEdmEntityType entityType, bool writingFeed)
			: base(jsonLightOutputContext, entitySet, entityType, writingFeed)
		{
			this.jsonLightOutputContext = jsonLightOutputContext;
			this.jsonLightEntryAndFeedSerializer = new ODataJsonLightEntryAndFeedSerializer(this.jsonLightOutputContext);
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000D3F RID: 3391 RVA: 0x0002DF5C File Offset: 0x0002C15C
		private ODataJsonLightWriter.JsonLightEntryScope CurrentEntryScope
		{
			get
			{
				return base.CurrentScope as ODataJsonLightWriter.JsonLightEntryScope;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x0002DF78 File Offset: 0x0002C178
		private ODataJsonLightWriter.JsonLightFeedScope CurrentFeedScope
		{
			get
			{
				return base.CurrentScope as ODataJsonLightWriter.JsonLightFeedScope;
			}
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x0002DF92 File Offset: 0x0002C192
		protected override void VerifyNotDisposed()
		{
			this.jsonLightOutputContext.VerifyNotDisposed();
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x0002DF9F File Offset: 0x0002C19F
		protected override void FlushSynchronously()
		{
			this.jsonLightOutputContext.Flush();
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x0002DFAC File Offset: 0x0002C1AC
		protected override Task FlushAsynchronously()
		{
			return this.jsonLightOutputContext.FlushAsync();
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x0002DFB9 File Offset: 0x0002C1B9
		protected override void StartPayload()
		{
			this.jsonLightEntryAndFeedSerializer.WritePayloadStart();
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x0002DFC6 File Offset: 0x0002C1C6
		protected override void EndPayload()
		{
			this.jsonLightEntryAndFeedSerializer.WritePayloadEnd();
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x0002DFD4 File Offset: 0x0002C1D4
		protected override void PrepareEntryForWriteStart(ODataEntry entry, ODataFeedAndEntryTypeContext typeContext, SelectedPropertiesNode selectedProperties)
		{
			if (this.jsonLightOutputContext.MessageWriterSettings.AutoComputePayloadMetadataInJson)
			{
				ODataWriterCore.EntryScope entryScope = (ODataWriterCore.EntryScope)base.CurrentScope;
				ODataEntityMetadataBuilder odataEntityMetadataBuilder = this.jsonLightOutputContext.MetadataLevel.CreateEntityMetadataBuilder(entry, typeContext, entryScope.SerializationInfo, entryScope.EntityType, selectedProperties, this.jsonLightOutputContext.WritingResponse, this.jsonLightOutputContext.MessageWriterSettings.AutoGeneratedUrlsShouldPutKeyValueInDedicatedSegment);
				this.jsonLightOutputContext.MetadataLevel.InjectMetadataBuilder(entry, odataEntityMetadataBuilder);
			}
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0002E04C File Offset: 0x0002C24C
		protected override void ValidateEntryMediaResource(ODataEntry entry, IEdmEntityType entityType)
		{
			if (this.jsonLightOutputContext.MessageWriterSettings.AutoComputePayloadMetadataInJson && this.jsonLightOutputContext.MetadataLevel is JsonNoMetadataLevel)
			{
				return;
			}
			base.ValidateEntryMediaResource(entry, entityType);
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x0002E07C File Offset: 0x0002C27C
		protected override void StartEntry(ODataEntry entry)
		{
			ODataNavigationLink parentNavigationLink = base.ParentNavigationLink;
			if (parentNavigationLink != null)
			{
				this.jsonLightOutputContext.JsonWriter.WriteName(parentNavigationLink.Name);
			}
			if (entry == null)
			{
				this.jsonLightOutputContext.JsonWriter.WriteValue(null);
				return;
			}
			this.jsonLightOutputContext.JsonWriter.StartObjectScope();
			ODataJsonLightWriter.JsonLightEntryScope currentEntryScope = this.CurrentEntryScope;
			if (base.IsTopLevel)
			{
				this.jsonLightEntryAndFeedSerializer.TryWriteEntryMetadataUri(currentEntryScope.GetOrCreateTypeContext(this.jsonLightOutputContext.Model, this.jsonLightOutputContext.WritingResponse));
			}
			this.jsonLightEntryAndFeedSerializer.WriteAnnotationGroup(entry);
			this.jsonLightEntryAndFeedSerializer.WriteEntryStartMetadataProperties(currentEntryScope);
			this.jsonLightEntryAndFeedSerializer.WriteEntryMetadataProperties(currentEntryScope);
			this.jsonLightEntryAndFeedSerializer.InstanceAnnotationWriter.WriteInstanceAnnotations(entry.InstanceAnnotations, currentEntryScope.InstanceAnnotationWriteTracker);
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x0002E144 File Offset: 0x0002C344
		protected override void EndEntry(ODataEntry entry)
		{
			if (entry == null)
			{
				return;
			}
			ODataJsonLightWriter.JsonLightEntryScope currentEntryScope = this.CurrentEntryScope;
			ProjectedPropertiesAnnotation projectedPropertiesAnnotation = ODataWriterCore.GetProjectedPropertiesAnnotation(currentEntryScope);
			this.jsonLightEntryAndFeedSerializer.WriteEntryMetadataProperties(currentEntryScope);
			this.jsonLightEntryAndFeedSerializer.InstanceAnnotationWriter.WriteInstanceAnnotations(entry.InstanceAnnotations, currentEntryScope.InstanceAnnotationWriteTracker);
			this.jsonLightEntryAndFeedSerializer.WriteEntryEndMetadataProperties(currentEntryScope, currentEntryScope.DuplicatePropertyNamesChecker);
			this.jsonLightEntryAndFeedSerializer.WriteProperties(base.EntryEntityType, entry.Properties, false, base.DuplicatePropertyNamesChecker, projectedPropertiesAnnotation);
			this.jsonLightOutputContext.JsonWriter.EndObjectScope();
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x0002E1CC File Offset: 0x0002C3CC
		protected override void StartFeed(ODataFeed feed)
		{
			IJsonWriter jsonWriter = this.jsonLightOutputContext.JsonWriter;
			if (base.ParentNavigationLink == null)
			{
				jsonWriter.StartObjectScope();
				this.jsonLightEntryAndFeedSerializer.TryWriteFeedMetadataUri(this.CurrentFeedScope.GetOrCreateTypeContext(this.jsonLightOutputContext.Model, this.jsonLightOutputContext.WritingResponse));
				if (this.jsonLightOutputContext.WritingResponse)
				{
					this.WriteFeedCount(feed, null);
					this.WriteFeedNextLink(feed, null);
					this.WriteFeedDeltaLink(feed);
				}
				this.jsonLightEntryAndFeedSerializer.InstanceAnnotationWriter.WriteInstanceAnnotations(feed.InstanceAnnotations, this.CurrentFeedScope.InstanceAnnotationWriteTracker);
				jsonWriter.WriteValuePropertyName();
				jsonWriter.StartArrayScope();
				return;
			}
			string name = base.ParentNavigationLink.Name;
			base.ValidateNoDeltaLinkForExpandedFeed(feed);
			this.ValidateNoCustomInstanceAnnotationsForExpandedFeed(feed);
			if (this.jsonLightOutputContext.WritingResponse)
			{
				this.WriteFeedCount(feed, name);
				this.WriteFeedNextLink(feed, name);
				jsonWriter.WriteName(name);
				jsonWriter.StartArrayScope();
				return;
			}
			ODataJsonLightWriter.JsonLightNavigationLinkScope jsonLightNavigationLinkScope = (ODataJsonLightWriter.JsonLightNavigationLinkScope)base.ParentNavigationLinkScope;
			if (!jsonLightNavigationLinkScope.FeedWritten)
			{
				if (jsonLightNavigationLinkScope.EntityReferenceLinkWritten)
				{
					jsonWriter.EndArrayScope();
				}
				jsonWriter.WriteName(name);
				jsonWriter.StartArrayScope();
				jsonLightNavigationLinkScope.FeedWritten = true;
			}
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x0002E2F0 File Offset: 0x0002C4F0
		protected override void EndFeed(ODataFeed feed)
		{
			bool flag = base.ParentNavigationLink == null;
			if (flag)
			{
				this.jsonLightOutputContext.JsonWriter.EndArrayScope();
				this.jsonLightEntryAndFeedSerializer.InstanceAnnotationWriter.WriteInstanceAnnotations(feed.InstanceAnnotations, this.CurrentFeedScope.InstanceAnnotationWriteTracker);
				if (this.jsonLightOutputContext.WritingResponse)
				{
					this.WriteFeedCount(feed, null);
					this.WriteFeedNextLink(feed, null);
					this.WriteFeedDeltaLink(feed);
				}
				this.jsonLightOutputContext.JsonWriter.EndObjectScope();
				return;
			}
			string name = base.ParentNavigationLink.Name;
			base.ValidateNoDeltaLinkForExpandedFeed(feed);
			this.ValidateNoCustomInstanceAnnotationsForExpandedFeed(feed);
			if (this.jsonLightOutputContext.WritingResponse)
			{
				this.jsonLightOutputContext.JsonWriter.EndArrayScope();
				this.WriteFeedCount(feed, name);
				this.WriteFeedNextLink(feed, name);
			}
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x0002E3B7 File Offset: 0x0002C5B7
		protected override void WriteDeferredNavigationLink(ODataNavigationLink navigationLink)
		{
			this.jsonLightEntryAndFeedSerializer.WriteNavigationLinkMetadata(navigationLink, base.DuplicatePropertyNamesChecker);
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x0002E3CB File Offset: 0x0002C5CB
		protected override void StartNavigationLinkWithContent(ODataNavigationLink navigationLink)
		{
			if (this.jsonLightOutputContext.WritingResponse)
			{
				this.jsonLightEntryAndFeedSerializer.WriteNavigationLinkMetadata(navigationLink, base.DuplicatePropertyNamesChecker);
				return;
			}
			WriterValidationUtils.ValidateNavigationLinkHasCardinality(navigationLink);
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x0002E3F4 File Offset: 0x0002C5F4
		protected override void EndNavigationLinkWithContent(ODataNavigationLink navigationLink)
		{
			if (!this.jsonLightOutputContext.WritingResponse)
			{
				ODataJsonLightWriter.JsonLightNavigationLinkScope jsonLightNavigationLinkScope = (ODataJsonLightWriter.JsonLightNavigationLinkScope)base.CurrentScope;
				if (jsonLightNavigationLinkScope.EntityReferenceLinkWritten && !jsonLightNavigationLinkScope.FeedWritten && navigationLink.IsCollection.Value)
				{
					this.jsonLightOutputContext.JsonWriter.EndArrayScope();
				}
				if (jsonLightNavigationLinkScope.FeedWritten)
				{
					this.jsonLightOutputContext.JsonWriter.EndArrayScope();
				}
			}
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x0002E464 File Offset: 0x0002C664
		protected override void WriteEntityReferenceInNavigationLinkContent(ODataNavigationLink parentNavigationLink, ODataEntityReferenceLink entityReferenceLink)
		{
			ODataJsonLightWriter.JsonLightNavigationLinkScope jsonLightNavigationLinkScope = (ODataJsonLightWriter.JsonLightNavigationLinkScope)base.CurrentScope;
			if (jsonLightNavigationLinkScope.FeedWritten)
			{
				throw new ODataException(Strings.ODataJsonLightWriter_EntityReferenceLinkAfterFeedInRequest);
			}
			if (!jsonLightNavigationLinkScope.EntityReferenceLinkWritten)
			{
				this.jsonLightOutputContext.JsonWriter.WritePropertyAnnotationName(parentNavigationLink.Name, "odata.bind");
				if (parentNavigationLink.IsCollection.Value)
				{
					this.jsonLightOutputContext.JsonWriter.StartArrayScope();
				}
				jsonLightNavigationLinkScope.EntityReferenceLinkWritten = true;
			}
			this.jsonLightOutputContext.JsonWriter.WriteValue(this.jsonLightEntryAndFeedSerializer.UriToString(entityReferenceLink.Url));
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x0002E4FB File Offset: 0x0002C6FB
		protected override ODataWriterCore.FeedScope CreateFeedScope(ODataFeed feed, IEdmEntitySet entitySet, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties)
		{
			return new ODataJsonLightWriter.JsonLightFeedScope(feed, entitySet, entityType, skipWriting, selectedProperties);
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0002E509 File Offset: 0x0002C709
		protected override ODataWriterCore.EntryScope CreateEntryScope(ODataEntry entry, IEdmEntitySet entitySet, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties)
		{
			return new ODataJsonLightWriter.JsonLightEntryScope(entry, base.GetEntrySerializationInfo(entry), entitySet, entityType, skipWriting, this.jsonLightOutputContext.WritingResponse, this.jsonLightOutputContext.MessageWriterSettings.WriterBehavior, selectedProperties);
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x0002E539 File Offset: 0x0002C739
		protected override ODataWriterCore.NavigationLinkScope CreateNavigationLinkScope(ODataWriterCore.WriterState writerState, ODataNavigationLink navLink, IEdmEntitySet entitySet, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties)
		{
			return new ODataJsonLightWriter.JsonLightNavigationLinkScope(writerState, navLink, entitySet, entityType, skipWriting, selectedProperties);
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x0002E54C File Offset: 0x0002C74C
		private void WriteFeedCount(ODataFeed feed, string propertyName)
		{
			long? count = feed.Count;
			if (count != null && !this.CurrentFeedScope.CountWritten)
			{
				if (propertyName == null)
				{
					this.jsonLightOutputContext.JsonWriter.WriteName("odata.count");
				}
				else
				{
					this.jsonLightOutputContext.JsonWriter.WritePropertyAnnotationName(propertyName, "odata.count");
				}
				this.jsonLightOutputContext.JsonWriter.WriteValue(count.Value);
				this.CurrentFeedScope.CountWritten = true;
			}
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x0002E5CC File Offset: 0x0002C7CC
		private void WriteFeedNextLink(ODataFeed feed, string propertyName)
		{
			Uri nextPageLink = feed.NextPageLink;
			if (nextPageLink != null && !this.CurrentFeedScope.NextPageLinkWritten)
			{
				if (propertyName == null)
				{
					this.jsonLightOutputContext.JsonWriter.WriteName("odata.nextLink");
				}
				else
				{
					this.jsonLightOutputContext.JsonWriter.WritePropertyAnnotationName(propertyName, "odata.nextLink");
				}
				this.jsonLightOutputContext.JsonWriter.WriteValue(this.jsonLightEntryAndFeedSerializer.UriToString(nextPageLink));
				this.CurrentFeedScope.NextPageLinkWritten = true;
			}
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x0002E650 File Offset: 0x0002C850
		private void WriteFeedDeltaLink(ODataFeed feed)
		{
			Uri deltaLink = feed.DeltaLink;
			if (deltaLink != null && !this.CurrentFeedScope.DeltaLinkWritten)
			{
				this.jsonLightOutputContext.JsonWriter.WriteName("odata.deltaLink");
				this.jsonLightOutputContext.JsonWriter.WriteValue(this.jsonLightEntryAndFeedSerializer.UriToString(deltaLink));
				this.CurrentFeedScope.DeltaLinkWritten = true;
			}
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0002E6B7 File Offset: 0x0002C8B7
		private void ValidateNoCustomInstanceAnnotationsForExpandedFeed(ODataFeed feed)
		{
			if (feed.InstanceAnnotations.Count > 0)
			{
				throw new ODataException(Strings.ODataJsonLightWriter_InstanceAnnotationNotSupportedOnExpandedFeed);
			}
		}

		// Token: 0x04000470 RID: 1136
		private readonly ODataJsonLightOutputContext jsonLightOutputContext;

		// Token: 0x04000471 RID: 1137
		private readonly ODataJsonLightEntryAndFeedSerializer jsonLightEntryAndFeedSerializer;

		// Token: 0x020001AD RID: 429
		private sealed class JsonLightFeedScope : ODataWriterCore.FeedScope
		{
			// Token: 0x06000D57 RID: 3415 RVA: 0x0002E6D2 File Offset: 0x0002C8D2
			internal JsonLightFeedScope(ODataFeed feed, IEdmEntitySet entitySet, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties)
				: base(feed, entitySet, entityType, skipWriting, selectedProperties)
			{
			}

			// Token: 0x170002DF RID: 735
			// (get) Token: 0x06000D58 RID: 3416 RVA: 0x0002E6E1 File Offset: 0x0002C8E1
			// (set) Token: 0x06000D59 RID: 3417 RVA: 0x0002E6E9 File Offset: 0x0002C8E9
			internal bool CountWritten
			{
				get
				{
					return this.countWritten;
				}
				set
				{
					this.countWritten = value;
				}
			}

			// Token: 0x170002E0 RID: 736
			// (get) Token: 0x06000D5A RID: 3418 RVA: 0x0002E6F2 File Offset: 0x0002C8F2
			// (set) Token: 0x06000D5B RID: 3419 RVA: 0x0002E6FA File Offset: 0x0002C8FA
			internal bool NextPageLinkWritten
			{
				get
				{
					return this.nextLinkWritten;
				}
				set
				{
					this.nextLinkWritten = value;
				}
			}

			// Token: 0x170002E1 RID: 737
			// (get) Token: 0x06000D5C RID: 3420 RVA: 0x0002E703 File Offset: 0x0002C903
			// (set) Token: 0x06000D5D RID: 3421 RVA: 0x0002E70B File Offset: 0x0002C90B
			internal bool DeltaLinkWritten
			{
				get
				{
					return this.deltaLinkWritten;
				}
				set
				{
					this.deltaLinkWritten = value;
				}
			}

			// Token: 0x04000472 RID: 1138
			private bool countWritten;

			// Token: 0x04000473 RID: 1139
			private bool nextLinkWritten;

			// Token: 0x04000474 RID: 1140
			private bool deltaLinkWritten;
		}

		// Token: 0x020001AE RID: 430
		private sealed class JsonLightEntryScope : ODataWriterCore.EntryScope, IODataJsonLightWriterEntryState
		{
			// Token: 0x06000D5E RID: 3422 RVA: 0x0002E714 File Offset: 0x0002C914
			internal JsonLightEntryScope(ODataEntry entry, ODataFeedAndEntrySerializationInfo serializationInfo, IEdmEntitySet entitySet, IEdmEntityType entityType, bool skipWriting, bool writingResponse, ODataWriterBehavior writerBehavior, SelectedPropertiesNode selectedProperties)
				: base(entry, serializationInfo, entitySet, entityType, skipWriting, writingResponse, writerBehavior, selectedProperties)
			{
			}

			// Token: 0x170002E2 RID: 738
			// (get) Token: 0x06000D5F RID: 3423 RVA: 0x0002E734 File Offset: 0x0002C934
			public ODataEntry Entry
			{
				get
				{
					return (ODataEntry)base.Item;
				}
			}

			// Token: 0x170002E3 RID: 739
			// (get) Token: 0x06000D60 RID: 3424 RVA: 0x0002E741 File Offset: 0x0002C941
			// (set) Token: 0x06000D61 RID: 3425 RVA: 0x0002E74A File Offset: 0x0002C94A
			public bool EditLinkWritten
			{
				get
				{
					return this.IsMetadataPropertyWritten(ODataJsonLightWriter.JsonLightEntryScope.JsonLightEntryMetadataProperty.EditLink);
				}
				set
				{
					this.SetWrittenMetadataProperty(ODataJsonLightWriter.JsonLightEntryScope.JsonLightEntryMetadataProperty.EditLink);
				}
			}

			// Token: 0x170002E4 RID: 740
			// (get) Token: 0x06000D62 RID: 3426 RVA: 0x0002E753 File Offset: 0x0002C953
			// (set) Token: 0x06000D63 RID: 3427 RVA: 0x0002E75C File Offset: 0x0002C95C
			public bool ReadLinkWritten
			{
				get
				{
					return this.IsMetadataPropertyWritten(ODataJsonLightWriter.JsonLightEntryScope.JsonLightEntryMetadataProperty.ReadLink);
				}
				set
				{
					this.SetWrittenMetadataProperty(ODataJsonLightWriter.JsonLightEntryScope.JsonLightEntryMetadataProperty.ReadLink);
				}
			}

			// Token: 0x170002E5 RID: 741
			// (get) Token: 0x06000D64 RID: 3428 RVA: 0x0002E765 File Offset: 0x0002C965
			// (set) Token: 0x06000D65 RID: 3429 RVA: 0x0002E76E File Offset: 0x0002C96E
			public bool MediaEditLinkWritten
			{
				get
				{
					return this.IsMetadataPropertyWritten(ODataJsonLightWriter.JsonLightEntryScope.JsonLightEntryMetadataProperty.MediaEditLink);
				}
				set
				{
					this.SetWrittenMetadataProperty(ODataJsonLightWriter.JsonLightEntryScope.JsonLightEntryMetadataProperty.MediaEditLink);
				}
			}

			// Token: 0x170002E6 RID: 742
			// (get) Token: 0x06000D66 RID: 3430 RVA: 0x0002E777 File Offset: 0x0002C977
			// (set) Token: 0x06000D67 RID: 3431 RVA: 0x0002E780 File Offset: 0x0002C980
			public bool MediaReadLinkWritten
			{
				get
				{
					return this.IsMetadataPropertyWritten(ODataJsonLightWriter.JsonLightEntryScope.JsonLightEntryMetadataProperty.MediaReadLink);
				}
				set
				{
					this.SetWrittenMetadataProperty(ODataJsonLightWriter.JsonLightEntryScope.JsonLightEntryMetadataProperty.MediaReadLink);
				}
			}

			// Token: 0x170002E7 RID: 743
			// (get) Token: 0x06000D68 RID: 3432 RVA: 0x0002E789 File Offset: 0x0002C989
			// (set) Token: 0x06000D69 RID: 3433 RVA: 0x0002E793 File Offset: 0x0002C993
			public bool MediaContentTypeWritten
			{
				get
				{
					return this.IsMetadataPropertyWritten(ODataJsonLightWriter.JsonLightEntryScope.JsonLightEntryMetadataProperty.MediaContentType);
				}
				set
				{
					this.SetWrittenMetadataProperty(ODataJsonLightWriter.JsonLightEntryScope.JsonLightEntryMetadataProperty.MediaContentType);
				}
			}

			// Token: 0x170002E8 RID: 744
			// (get) Token: 0x06000D6A RID: 3434 RVA: 0x0002E79D File Offset: 0x0002C99D
			// (set) Token: 0x06000D6B RID: 3435 RVA: 0x0002E7A7 File Offset: 0x0002C9A7
			public bool MediaETagWritten
			{
				get
				{
					return this.IsMetadataPropertyWritten(ODataJsonLightWriter.JsonLightEntryScope.JsonLightEntryMetadataProperty.MediaETag);
				}
				set
				{
					this.SetWrittenMetadataProperty(ODataJsonLightWriter.JsonLightEntryScope.JsonLightEntryMetadataProperty.MediaETag);
				}
			}

			// Token: 0x06000D6C RID: 3436 RVA: 0x0002E7B1 File Offset: 0x0002C9B1
			private void SetWrittenMetadataProperty(ODataJsonLightWriter.JsonLightEntryScope.JsonLightEntryMetadataProperty jsonLightMetadataProperty)
			{
				this.alreadyWrittenMetadataProperties |= (int)jsonLightMetadataProperty;
			}

			// Token: 0x06000D6D RID: 3437 RVA: 0x0002E7C1 File Offset: 0x0002C9C1
			private bool IsMetadataPropertyWritten(ODataJsonLightWriter.JsonLightEntryScope.JsonLightEntryMetadataProperty jsonLightMetadataProperty)
			{
				return (this.alreadyWrittenMetadataProperties & (int)jsonLightMetadataProperty) == (int)jsonLightMetadataProperty;
			}

			// Token: 0x04000475 RID: 1141
			private int alreadyWrittenMetadataProperties;

			// Token: 0x020001AF RID: 431
			[Flags]
			private enum JsonLightEntryMetadataProperty
			{
				// Token: 0x04000477 RID: 1143
				EditLink = 1,
				// Token: 0x04000478 RID: 1144
				ReadLink = 2,
				// Token: 0x04000479 RID: 1145
				MediaEditLink = 4,
				// Token: 0x0400047A RID: 1146
				MediaReadLink = 8,
				// Token: 0x0400047B RID: 1147
				MediaContentType = 16,
				// Token: 0x0400047C RID: 1148
				MediaETag = 32
			}
		}

		// Token: 0x020001B0 RID: 432
		private sealed class JsonLightNavigationLinkScope : ODataWriterCore.NavigationLinkScope
		{
			// Token: 0x06000D6E RID: 3438 RVA: 0x0002E7CE File Offset: 0x0002C9CE
			internal JsonLightNavigationLinkScope(ODataWriterCore.WriterState writerState, ODataNavigationLink navLink, IEdmEntitySet entitySet, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties)
				: base(writerState, navLink, entitySet, entityType, skipWriting, selectedProperties)
			{
			}

			// Token: 0x170002E9 RID: 745
			// (get) Token: 0x06000D6F RID: 3439 RVA: 0x0002E7DF File Offset: 0x0002C9DF
			// (set) Token: 0x06000D70 RID: 3440 RVA: 0x0002E7E7 File Offset: 0x0002C9E7
			internal bool EntityReferenceLinkWritten
			{
				get
				{
					return this.entityReferenceLinkWritten;
				}
				set
				{
					this.entityReferenceLinkWritten = value;
				}
			}

			// Token: 0x170002EA RID: 746
			// (get) Token: 0x06000D71 RID: 3441 RVA: 0x0002E7F0 File Offset: 0x0002C9F0
			// (set) Token: 0x06000D72 RID: 3442 RVA: 0x0002E7F8 File Offset: 0x0002C9F8
			internal bool FeedWritten
			{
				get
				{
					return this.feedWritten;
				}
				set
				{
					this.feedWritten = value;
				}
			}

			// Token: 0x06000D73 RID: 3443 RVA: 0x0002E804 File Offset: 0x0002CA04
			internal override ODataWriterCore.NavigationLinkScope Clone(ODataWriterCore.WriterState newWriterState)
			{
				return new ODataJsonLightWriter.JsonLightNavigationLinkScope(newWriterState, (ODataNavigationLink)base.Item, base.EntitySet, base.EntityType, base.SkipWriting, base.SelectedProperties)
				{
					EntityReferenceLinkWritten = this.entityReferenceLinkWritten,
					FeedWritten = this.feedWritten
				};
			}

			// Token: 0x0400047D RID: 1149
			private bool entityReferenceLinkWritten;

			// Token: 0x0400047E RID: 1150
			private bool feedWritten;
		}
	}
}
