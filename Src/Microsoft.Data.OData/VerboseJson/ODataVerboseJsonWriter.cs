using System;
using System.Threading.Tasks;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x02000294 RID: 660
	internal sealed class ODataVerboseJsonWriter : ODataWriterCore
	{
		// Token: 0x06001640 RID: 5696 RVA: 0x0005166B File Offset: 0x0004F86B
		internal ODataVerboseJsonWriter(ODataVerboseJsonOutputContext jsonOutputContext, IEdmEntitySet entitySet, IEdmEntityType entityType, bool writingFeed)
			: base(jsonOutputContext, entitySet, entityType, writingFeed)
		{
			this.verboseJsonOutputContext = jsonOutputContext;
			this.verboseJsonEntryAndFeedSerializer = new ODataVerboseJsonEntryAndFeedSerializer(this.verboseJsonOutputContext);
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001641 RID: 5697 RVA: 0x00051690 File Offset: 0x0004F890
		private ODataVerboseJsonWriter.VerboseJsonFeedScope CurrentFeedScope
		{
			get
			{
				return base.CurrentScope as ODataVerboseJsonWriter.VerboseJsonFeedScope;
			}
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x000516AA File Offset: 0x0004F8AA
		protected override void VerifyNotDisposed()
		{
			this.verboseJsonOutputContext.VerifyNotDisposed();
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x000516B7 File Offset: 0x0004F8B7
		protected override void FlushSynchronously()
		{
			this.verboseJsonOutputContext.Flush();
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x000516C4 File Offset: 0x0004F8C4
		protected override Task FlushAsynchronously()
		{
			return this.verboseJsonOutputContext.FlushAsync();
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x000516D1 File Offset: 0x0004F8D1
		protected override void StartPayload()
		{
			this.verboseJsonEntryAndFeedSerializer.WritePayloadStart();
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x000516DE File Offset: 0x0004F8DE
		protected override void EndPayload()
		{
			this.verboseJsonEntryAndFeedSerializer.WritePayloadEnd();
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x000516EC File Offset: 0x0004F8EC
		protected override void StartEntry(ODataEntry entry)
		{
			if (entry == null)
			{
				this.verboseJsonOutputContext.JsonWriter.WriteValue(null);
				return;
			}
			this.verboseJsonOutputContext.JsonWriter.StartObjectScope();
			ProjectedPropertiesAnnotation projectedPropertiesAnnotation = ODataWriterCore.GetProjectedPropertiesAnnotation(base.CurrentScope);
			this.verboseJsonEntryAndFeedSerializer.WriteEntryMetadata(entry, projectedPropertiesAnnotation, base.EntryEntityType, base.DuplicatePropertyNamesChecker);
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x00051744 File Offset: 0x0004F944
		protected override void EndEntry(ODataEntry entry)
		{
			if (entry == null)
			{
				return;
			}
			ProjectedPropertiesAnnotation projectedPropertiesAnnotation = ODataWriterCore.GetProjectedPropertiesAnnotation(base.CurrentScope);
			this.verboseJsonEntryAndFeedSerializer.WriteProperties(base.EntryEntityType, entry.Properties, false, base.DuplicatePropertyNamesChecker, projectedPropertiesAnnotation);
			this.verboseJsonOutputContext.JsonWriter.EndObjectScope();
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x00051790 File Offset: 0x0004F990
		protected override void StartFeed(ODataFeed feed)
		{
			if (base.ParentNavigationLink == null || this.verboseJsonOutputContext.WritingResponse)
			{
				if (this.verboseJsonOutputContext.Version >= ODataVersion.V2 && this.verboseJsonOutputContext.WritingResponse)
				{
					this.verboseJsonOutputContext.JsonWriter.StartObjectScope();
					this.WriteFeedCount(feed);
					this.verboseJsonOutputContext.JsonWriter.WriteDataArrayName();
				}
				this.verboseJsonOutputContext.JsonWriter.StartArrayScope();
			}
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x00051804 File Offset: 0x0004FA04
		protected override void EndFeed(ODataFeed feed)
		{
			if (base.ParentNavigationLink == null || this.verboseJsonOutputContext.WritingResponse)
			{
				this.verboseJsonOutputContext.JsonWriter.EndArrayScope();
				Uri nextPageLink = feed.NextPageLink;
				if (this.verboseJsonOutputContext.Version >= ODataVersion.V2 && this.verboseJsonOutputContext.WritingResponse)
				{
					this.WriteFeedCount(feed);
					if (nextPageLink != null)
					{
						this.verboseJsonOutputContext.JsonWriter.WriteName("__next");
						this.verboseJsonOutputContext.JsonWriter.WriteValue(this.verboseJsonEntryAndFeedSerializer.UriToAbsoluteUriString(nextPageLink));
					}
					this.verboseJsonOutputContext.JsonWriter.EndObjectScope();
				}
			}
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x000518AC File Offset: 0x0004FAAC
		protected override void WriteDeferredNavigationLink(ODataNavigationLink navigationLink)
		{
			WriterValidationUtils.ValidateNavigationLinkUrlPresent(navigationLink);
			this.verboseJsonOutputContext.JsonWriter.WriteName(navigationLink.Name);
			this.verboseJsonOutputContext.JsonWriter.StartObjectScope();
			this.verboseJsonOutputContext.JsonWriter.WriteName("__deferred");
			this.verboseJsonOutputContext.JsonWriter.StartObjectScope();
			this.verboseJsonOutputContext.JsonWriter.WriteName("uri");
			this.verboseJsonOutputContext.JsonWriter.WriteValue(this.verboseJsonEntryAndFeedSerializer.UriToAbsoluteUriString(navigationLink.Url));
			this.verboseJsonOutputContext.JsonWriter.EndObjectScope();
			this.verboseJsonOutputContext.JsonWriter.EndObjectScope();
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x00051960 File Offset: 0x0004FB60
		protected override void StartNavigationLinkWithContent(ODataNavigationLink navigationLink)
		{
			this.verboseJsonOutputContext.JsonWriter.WriteName(navigationLink.Name);
			if (this.verboseJsonOutputContext.WritingResponse)
			{
				return;
			}
			WriterValidationUtils.ValidateNavigationLinkHasCardinality(navigationLink);
			if (navigationLink.IsCollection.Value)
			{
				this.verboseJsonOutputContext.JsonWriter.StartArrayScope();
			}
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x000519B8 File Offset: 0x0004FBB8
		protected override void EndNavigationLinkWithContent(ODataNavigationLink navigationLink)
		{
			if (this.verboseJsonOutputContext.WritingResponse)
			{
				return;
			}
			if (navigationLink.IsCollection.Value)
			{
				this.verboseJsonOutputContext.JsonWriter.EndArrayScope();
			}
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x000519F4 File Offset: 0x0004FBF4
		protected override void WriteEntityReferenceInNavigationLinkContent(ODataNavigationLink parentNavigationLink, ODataEntityReferenceLink entityReferenceLink)
		{
			this.verboseJsonOutputContext.JsonWriter.StartObjectScope();
			this.verboseJsonOutputContext.JsonWriter.WriteName("__metadata");
			this.verboseJsonOutputContext.JsonWriter.StartObjectScope();
			this.verboseJsonOutputContext.JsonWriter.WriteName("uri");
			this.verboseJsonOutputContext.JsonWriter.WriteValue(this.verboseJsonEntryAndFeedSerializer.UriToAbsoluteUriString(entityReferenceLink.Url));
			this.verboseJsonOutputContext.JsonWriter.EndObjectScope();
			this.verboseJsonOutputContext.JsonWriter.EndObjectScope();
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x00051A8C File Offset: 0x0004FC8C
		protected override ODataWriterCore.FeedScope CreateFeedScope(ODataFeed feed, IEdmEntitySet entitySet, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties)
		{
			return new ODataVerboseJsonWriter.VerboseJsonFeedScope(feed, entitySet, entityType, skipWriting, selectedProperties);
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x00051A9A File Offset: 0x0004FC9A
		protected override ODataWriterCore.EntryScope CreateEntryScope(ODataEntry entry, IEdmEntitySet entitySet, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties)
		{
			return new ODataWriterCore.EntryScope(entry, base.GetEntrySerializationInfo(entry), entitySet, entityType, skipWriting, this.verboseJsonOutputContext.WritingResponse, this.verboseJsonOutputContext.MessageWriterSettings.WriterBehavior, selectedProperties);
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x00051ACC File Offset: 0x0004FCCC
		private void WriteFeedCount(ODataFeed feed)
		{
			long? count = feed.Count;
			if (count != null && !this.CurrentFeedScope.CountWritten)
			{
				this.verboseJsonOutputContext.JsonWriter.WriteName("__count");
				this.verboseJsonOutputContext.JsonWriter.WriteValue(count.Value);
				this.CurrentFeedScope.CountWritten = true;
			}
		}

		// Token: 0x040008CB RID: 2251
		private readonly ODataVerboseJsonOutputContext verboseJsonOutputContext;

		// Token: 0x040008CC RID: 2252
		private readonly ODataVerboseJsonEntryAndFeedSerializer verboseJsonEntryAndFeedSerializer;

		// Token: 0x02000295 RID: 661
		private sealed class VerboseJsonFeedScope : ODataWriterCore.FeedScope
		{
			// Token: 0x06001652 RID: 5714 RVA: 0x00051B2E File Offset: 0x0004FD2E
			internal VerboseJsonFeedScope(ODataFeed feed, IEdmEntitySet entitySet, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties)
				: base(feed, entitySet, entityType, skipWriting, selectedProperties)
			{
			}

			// Token: 0x1700047A RID: 1146
			// (get) Token: 0x06001653 RID: 5715 RVA: 0x00051B3D File Offset: 0x0004FD3D
			// (set) Token: 0x06001654 RID: 5716 RVA: 0x00051B45 File Offset: 0x0004FD45
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

			// Token: 0x040008CD RID: 2253
			private bool countWritten;
		}
	}
}
