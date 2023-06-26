using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x0200029E RID: 670
	internal sealed class ODataAtomWriter : ODataWriterCore
	{
		// Token: 0x0600169B RID: 5787 RVA: 0x0005269C File Offset: 0x0005089C
		internal ODataAtomWriter(ODataAtomOutputContext atomOutputContext, IEdmEntitySet entitySet, IEdmEntityType entityType, bool writingFeed)
			: base(atomOutputContext, entitySet, entityType, writingFeed)
		{
			this.atomOutputContext = atomOutputContext;
			if (this.atomOutputContext.MessageWriterSettings.AtomStartEntryXmlCustomizationCallback != null)
			{
				this.atomOutputContext.InitializeWriterCustomization();
			}
			this.atomEntryAndFeedSerializer = new ODataAtomEntryAndFeedSerializer(this.atomOutputContext);
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x0600169C RID: 5788 RVA: 0x000526FC File Offset: 0x000508FC
		private ODataAtomWriter.AtomEntryScope CurrentEntryScope
		{
			get
			{
				return base.CurrentScope as ODataAtomWriter.AtomEntryScope;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x0600169D RID: 5789 RVA: 0x00052718 File Offset: 0x00050918
		private ODataAtomWriter.AtomFeedScope CurrentFeedScope
		{
			get
			{
				return base.CurrentScope as ODataAtomWriter.AtomFeedScope;
			}
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x00052732 File Offset: 0x00050932
		protected override void VerifyNotDisposed()
		{
			this.atomOutputContext.VerifyNotDisposed();
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x0005273F File Offset: 0x0005093F
		protected override void FlushSynchronously()
		{
			this.atomOutputContext.Flush();
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x0005274C File Offset: 0x0005094C
		protected override Task FlushAsynchronously()
		{
			return this.atomOutputContext.FlushAsync();
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x00052759 File Offset: 0x00050959
		protected override void StartPayload()
		{
			this.atomEntryAndFeedSerializer.WritePayloadStart();
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x00052766 File Offset: 0x00050966
		protected override void EndPayload()
		{
			this.atomEntryAndFeedSerializer.WritePayloadEnd();
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x00052774 File Offset: 0x00050974
		protected override void StartEntry(ODataEntry entry)
		{
			this.CheckAndWriteParentNavigationLinkStartForInlineElement();
			if (entry == null)
			{
				return;
			}
			this.StartEntryXmlCustomization(entry);
			this.atomOutputContext.XmlWriter.WriteStartElement("", "entry", "http://www.w3.org/2005/Atom");
			if (base.IsTopLevel)
			{
				this.atomEntryAndFeedSerializer.WriteBaseUriAndDefaultNamespaceAttributes();
			}
			string etag = entry.ETag;
			if (etag != null)
			{
				ODataAtomWriterUtils.WriteETag(this.atomOutputContext.XmlWriter, etag);
			}
			ODataAtomWriter.AtomEntryScope currentEntryScope = this.CurrentEntryScope;
			AtomEntryMetadata atomEntryMetadata = entry.Atom();
			string id = entry.Id;
			if (id != null)
			{
				this.atomEntryAndFeedSerializer.WriteEntryId(id);
				currentEntryScope.SetWrittenElement(ODataAtomWriter.AtomElement.Id);
			}
			string entryTypeNameForWriting = this.atomOutputContext.TypeNameOracle.GetEntryTypeNameForWriting(entry);
			this.atomEntryAndFeedSerializer.WriteEntryTypeName(entryTypeNameForWriting, atomEntryMetadata);
			Uri editLink = entry.EditLink;
			if (editLink != null)
			{
				this.atomEntryAndFeedSerializer.WriteEntryEditLink(editLink, atomEntryMetadata);
				currentEntryScope.SetWrittenElement(ODataAtomWriter.AtomElement.EditLink);
			}
			Uri readLink = entry.ReadLink;
			if (readLink != null)
			{
				this.atomEntryAndFeedSerializer.WriteEntryReadLink(readLink, atomEntryMetadata);
				currentEntryScope.SetWrittenElement(ODataAtomWriter.AtomElement.ReadLink);
			}
			this.WriteInstanceAnnotations(entry.InstanceAnnotations, currentEntryScope.InstanceAnnotationWriteTracker);
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x0005288C File Offset: 0x00050A8C
		protected override void EndEntry(ODataEntry entry)
		{
			if (entry == null)
			{
				this.CheckAndWriteParentNavigationLinkEndForInlineElement();
				return;
			}
			IEdmEntityType entryEntityType = base.EntryEntityType;
			EntryPropertiesValueCache entryPropertiesValueCache = new EntryPropertiesValueCache(entry);
			ODataEntityPropertyMappingCache odataEntityPropertyMappingCache = this.atomOutputContext.Model.EnsureEpmCache(entryEntityType, int.MaxValue);
			if (odataEntityPropertyMappingCache != null)
			{
				EpmWriterUtils.CacheEpmProperties(entryPropertiesValueCache, odataEntityPropertyMappingCache.EpmSourceTree);
			}
			ODataAtomWriter.AtomEntryScope currentEntryScope = this.CurrentEntryScope;
			ProjectedPropertiesAnnotation projectedPropertiesAnnotation = ODataWriterCore.GetProjectedPropertiesAnnotation(currentEntryScope);
			AtomEntryMetadata atomEntryMetadata = entry.Atom();
			if (!currentEntryScope.IsElementWritten(ODataAtomWriter.AtomElement.Id))
			{
				this.atomEntryAndFeedSerializer.WriteEntryId(entry.Id);
			}
			Uri editLink = entry.EditLink;
			if (editLink != null && !currentEntryScope.IsElementWritten(ODataAtomWriter.AtomElement.EditLink))
			{
				this.atomEntryAndFeedSerializer.WriteEntryEditLink(editLink, atomEntryMetadata);
			}
			Uri readLink = entry.ReadLink;
			if (readLink != null && !currentEntryScope.IsElementWritten(ODataAtomWriter.AtomElement.ReadLink))
			{
				this.atomEntryAndFeedSerializer.WriteEntryReadLink(readLink, atomEntryMetadata);
			}
			AtomEntryMetadata atomEntryMetadata2 = null;
			if (odataEntityPropertyMappingCache != null)
			{
				ODataVersionChecker.CheckEntityPropertyMapping(this.atomOutputContext.Version, entryEntityType, this.atomOutputContext.Model);
				atomEntryMetadata2 = EpmSyndicationWriter.WriteEntryEpm(odataEntityPropertyMappingCache.EpmTargetTree, entryPropertiesValueCache, entryEntityType.ToTypeReference().AsEntity(), this.atomOutputContext);
			}
			this.atomEntryAndFeedSerializer.WriteEntryMetadata(atomEntryMetadata, atomEntryMetadata2, this.updatedTime);
			IEnumerable<ODataProperty> entryStreamProperties = entryPropertiesValueCache.EntryStreamProperties;
			if (entryStreamProperties != null)
			{
				foreach (ODataProperty odataProperty in entryStreamProperties)
				{
					this.atomEntryAndFeedSerializer.WriteStreamProperty(odataProperty, entryEntityType, base.DuplicatePropertyNamesChecker, projectedPropertiesAnnotation);
				}
			}
			IEnumerable<ODataAssociationLink> associationLinks = entry.AssociationLinks;
			if (associationLinks != null)
			{
				foreach (ODataAssociationLink odataAssociationLink in associationLinks)
				{
					this.atomEntryAndFeedSerializer.WriteAssociationLink(odataAssociationLink, entryEntityType, base.DuplicatePropertyNamesChecker, projectedPropertiesAnnotation);
				}
			}
			IEnumerable<ODataAction> actions = entry.Actions;
			if (actions != null)
			{
				foreach (ODataAction odataAction in actions)
				{
					ValidationUtils.ValidateOperationNotNull(odataAction, true);
					this.atomEntryAndFeedSerializer.WriteOperation(odataAction);
				}
			}
			IEnumerable<ODataFunction> functions = entry.Functions;
			if (functions != null)
			{
				foreach (ODataFunction odataFunction in functions)
				{
					ValidationUtils.ValidateOperationNotNull(odataFunction, false);
					this.atomEntryAndFeedSerializer.WriteOperation(odataFunction);
				}
			}
			this.WriteEntryContent(entry, entryEntityType, entryPropertiesValueCache, (odataEntityPropertyMappingCache == null) ? null : odataEntityPropertyMappingCache.EpmSourceTree.Root, projectedPropertiesAnnotation);
			if (odataEntityPropertyMappingCache != null)
			{
				EpmCustomWriter.WriteEntryEpm(this.atomOutputContext.XmlWriter, odataEntityPropertyMappingCache.EpmTargetTree, entryPropertiesValueCache, entryEntityType.ToTypeReference().AsEntity(), this.atomOutputContext);
			}
			this.WriteInstanceAnnotations(entry.InstanceAnnotations, currentEntryScope.InstanceAnnotationWriteTracker);
			this.atomOutputContext.XmlWriter.WriteEndElement();
			this.EndEntryXmlCustomization(entry);
			this.CheckAndWriteParentNavigationLinkEndForInlineElement();
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x00052B90 File Offset: 0x00050D90
		protected override void StartFeed(ODataFeed feed)
		{
			if (string.IsNullOrEmpty(feed.Id))
			{
				throw new ODataException(Strings.ODataAtomWriter_FeedsMustHaveNonEmptyId);
			}
			this.CheckAndWriteParentNavigationLinkStartForInlineElement();
			this.atomOutputContext.XmlWriter.WriteStartElement("", "feed", "http://www.w3.org/2005/Atom");
			if (base.IsTopLevel)
			{
				this.atomEntryAndFeedSerializer.WriteBaseUriAndDefaultNamespaceAttributes();
				if (feed.Count != null)
				{
					this.atomEntryAndFeedSerializer.WriteCount(feed.Count.Value, false);
				}
			}
			bool flag;
			this.atomEntryAndFeedSerializer.WriteFeedMetadata(feed, this.updatedTime, out flag);
			this.CurrentFeedScope.AuthorWritten = flag;
			this.WriteFeedInstanceAnnotations(feed, this.CurrentFeedScope);
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x00052C44 File Offset: 0x00050E44
		protected override void EndFeed(ODataFeed feed)
		{
			ODataAtomWriter.AtomFeedScope currentFeedScope = this.CurrentFeedScope;
			if (!currentFeedScope.AuthorWritten && currentFeedScope.EntryCount == 0)
			{
				this.atomEntryAndFeedSerializer.WriteFeedDefaultAuthor();
			}
			this.WriteFeedInstanceAnnotations(feed, currentFeedScope);
			this.atomEntryAndFeedSerializer.WriteFeedNextPageLink(feed);
			if (base.IsTopLevel)
			{
				if (this.atomOutputContext.WritingResponse)
				{
					this.atomEntryAndFeedSerializer.WriteFeedDeltaLink(feed);
				}
			}
			else
			{
				base.ValidateNoDeltaLinkForExpandedFeed(feed);
			}
			this.atomOutputContext.XmlWriter.WriteEndElement();
			this.CheckAndWriteParentNavigationLinkEndForInlineElement();
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x00052CC7 File Offset: 0x00050EC7
		protected override void WriteDeferredNavigationLink(ODataNavigationLink navigationLink)
		{
			this.WriteNavigationLinkStart(navigationLink, null);
			this.WriteNavigationLinkEnd();
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x00052CD7 File Offset: 0x00050ED7
		protected override void StartNavigationLinkWithContent(ODataNavigationLink navigationLink)
		{
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x00052CD9 File Offset: 0x00050ED9
		protected override void EndNavigationLinkWithContent(ODataNavigationLink navigationLink)
		{
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x00052CDB File Offset: 0x00050EDB
		protected override void WriteEntityReferenceInNavigationLinkContent(ODataNavigationLink parentNavigationLink, ODataEntityReferenceLink entityReferenceLink)
		{
			this.WriteNavigationLinkStart(parentNavigationLink, entityReferenceLink.Url);
			this.WriteNavigationLinkEnd();
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x00052CF0 File Offset: 0x00050EF0
		protected override ODataWriterCore.FeedScope CreateFeedScope(ODataFeed feed, IEdmEntitySet entitySet, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties)
		{
			return new ODataAtomWriter.AtomFeedScope(feed, entitySet, entityType, skipWriting, selectedProperties);
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x00052CFE File Offset: 0x00050EFE
		protected override ODataWriterCore.EntryScope CreateEntryScope(ODataEntry entry, IEdmEntitySet entitySet, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties)
		{
			return new ODataAtomWriter.AtomEntryScope(entry, base.GetEntrySerializationInfo(entry), entitySet, entityType, skipWriting, this.atomOutputContext.WritingResponse, this.atomOutputContext.MessageWriterSettings.WriterBehavior, selectedProperties);
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x00052D38 File Offset: 0x00050F38
		private void WriteInstanceAnnotations(IEnumerable<ODataInstanceAnnotation> instanceAnnotations, InstanceAnnotationWriteTracker tracker)
		{
			IEnumerable<AtomInstanceAnnotation> enumerable = instanceAnnotations.Select((ODataInstanceAnnotation instanceAnnotation) => AtomInstanceAnnotation.CreateFrom(instanceAnnotation, null));
			this.atomEntryAndFeedSerializer.WriteInstanceAnnotations(enumerable, tracker);
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x00052D76 File Offset: 0x00050F76
		private void WriteFeedInstanceAnnotations(ODataFeed feed, ODataAtomWriter.AtomFeedScope currentFeedScope)
		{
			if (base.IsTopLevel)
			{
				this.WriteInstanceAnnotations(feed.InstanceAnnotations, currentFeedScope.InstanceAnnotationWriteTracker);
				return;
			}
			if (feed.InstanceAnnotations.Count > 0)
			{
				throw new ODataException(Strings.ODataJsonLightWriter_InstanceAnnotationNotSupportedOnExpandedFeed);
			}
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x00052DAC File Offset: 0x00050FAC
		private void WriteEntryContent(ODataEntry entry, IEdmEntityType entryType, EntryPropertiesValueCache propertiesValueCache, EpmSourcePathSegment rootSourcePathSegment, ProjectedPropertiesAnnotation projectedProperties)
		{
			ODataStreamReferenceValue mediaResource = entry.MediaResource;
			if (mediaResource == null)
			{
				this.atomOutputContext.XmlWriter.WriteStartElement("", "content", "http://www.w3.org/2005/Atom");
				this.atomOutputContext.XmlWriter.WriteAttributeString("type", "application/xml");
				this.atomEntryAndFeedSerializer.WriteProperties(entryType, propertiesValueCache.EntryProperties, false, new Action(this.atomEntryAndFeedSerializer.WriteEntryPropertiesStart), new Action(this.atomEntryAndFeedSerializer.WriteEntryPropertiesEnd), base.DuplicatePropertyNamesChecker, propertiesValueCache, rootSourcePathSegment, projectedProperties);
				this.atomOutputContext.XmlWriter.WriteEndElement();
				return;
			}
			WriterValidationUtils.ValidateStreamReferenceValue(mediaResource, true);
			this.atomEntryAndFeedSerializer.WriteEntryMediaEditLink(mediaResource);
			if (mediaResource.ReadLink != null)
			{
				this.atomOutputContext.XmlWriter.WriteStartElement("", "content", "http://www.w3.org/2005/Atom");
				this.atomOutputContext.XmlWriter.WriteAttributeString("type", mediaResource.ContentType);
				this.atomOutputContext.XmlWriter.WriteAttributeString("src", this.atomEntryAndFeedSerializer.UriToUrlAttributeValue(mediaResource.ReadLink));
				this.atomOutputContext.XmlWriter.WriteEndElement();
			}
			this.atomEntryAndFeedSerializer.WriteProperties(entryType, propertiesValueCache.EntryProperties, false, new Action(this.atomEntryAndFeedSerializer.WriteEntryPropertiesStart), new Action(this.atomEntryAndFeedSerializer.WriteEntryPropertiesEnd), base.DuplicatePropertyNamesChecker, propertiesValueCache, rootSourcePathSegment, projectedProperties);
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x00052F24 File Offset: 0x00051124
		private void CheckAndWriteParentNavigationLinkStartForInlineElement()
		{
			ODataNavigationLink parentNavigationLink = base.ParentNavigationLink;
			if (parentNavigationLink != null)
			{
				this.WriteNavigationLinkStart(parentNavigationLink, null);
				this.atomOutputContext.XmlWriter.WriteStartElement("m", "inline", "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata");
			}
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x00052F64 File Offset: 0x00051164
		private void CheckAndWriteParentNavigationLinkEndForInlineElement()
		{
			ODataNavigationLink parentNavigationLink = base.ParentNavigationLink;
			if (parentNavigationLink != null)
			{
				this.atomOutputContext.XmlWriter.WriteEndElement();
				this.WriteNavigationLinkEnd();
			}
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x00052F91 File Offset: 0x00051191
		private void WriteNavigationLinkStart(ODataNavigationLink navigationLink, Uri navigationLinkUrlOverride)
		{
			WriterValidationUtils.ValidateNavigationLinkHasCardinality(navigationLink);
			WriterValidationUtils.ValidateNavigationLinkUrlPresent(navigationLink);
			this.atomEntryAndFeedSerializer.WriteNavigationLinkStart(navigationLink, navigationLinkUrlOverride);
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x00052FAC File Offset: 0x000511AC
		private void WriteNavigationLinkEnd()
		{
			this.atomOutputContext.XmlWriter.WriteEndElement();
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x00052FC0 File Offset: 0x000511C0
		private void StartEntryXmlCustomization(ODataEntry entry)
		{
			if (this.atomOutputContext.MessageWriterSettings.AtomStartEntryXmlCustomizationCallback != null)
			{
				XmlWriter xmlWriter = this.atomOutputContext.MessageWriterSettings.AtomStartEntryXmlCustomizationCallback(entry, this.atomOutputContext.XmlWriter);
				if (xmlWriter != null)
				{
					if (object.ReferenceEquals(this.atomOutputContext.XmlWriter, xmlWriter))
					{
						throw new ODataException(Strings.ODataAtomWriter_StartEntryXmlCustomizationCallbackReturnedSameInstance);
					}
				}
				else
				{
					xmlWriter = this.atomOutputContext.XmlWriter;
				}
				this.atomOutputContext.PushCustomWriter(xmlWriter);
			}
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x0005303C File Offset: 0x0005123C
		private void EndEntryXmlCustomization(ODataEntry entry)
		{
			if (this.atomOutputContext.MessageWriterSettings.AtomStartEntryXmlCustomizationCallback != null)
			{
				XmlWriter xmlWriter = this.atomOutputContext.PopCustomWriter();
				if (!object.ReferenceEquals(this.atomOutputContext.XmlWriter, xmlWriter))
				{
					this.atomOutputContext.MessageWriterSettings.AtomEndEntryXmlCustomizationCallback(entry, xmlWriter, this.atomOutputContext.XmlWriter);
				}
			}
		}

		// Token: 0x04000929 RID: 2345
		private readonly string updatedTime = ODataAtomConvert.ToAtomString(DateTimeOffset.UtcNow);

		// Token: 0x0400092A RID: 2346
		private readonly ODataAtomOutputContext atomOutputContext;

		// Token: 0x0400092B RID: 2347
		private readonly ODataAtomEntryAndFeedSerializer atomEntryAndFeedSerializer;

		// Token: 0x0200029F RID: 671
		private enum AtomElement
		{
			// Token: 0x0400092E RID: 2350
			Id = 1,
			// Token: 0x0400092F RID: 2351
			ReadLink,
			// Token: 0x04000930 RID: 2352
			EditLink = 4
		}

		// Token: 0x020002A0 RID: 672
		private sealed class AtomFeedScope : ODataWriterCore.FeedScope
		{
			// Token: 0x060016B7 RID: 5815 RVA: 0x0005309C File Offset: 0x0005129C
			internal AtomFeedScope(ODataFeed feed, IEdmEntitySet entitySet, IEdmEntityType entityType, bool skipWriting, SelectedPropertiesNode selectedProperties)
				: base(feed, entitySet, entityType, skipWriting, selectedProperties)
			{
			}

			// Token: 0x17000490 RID: 1168
			// (get) Token: 0x060016B8 RID: 5816 RVA: 0x000530AB File Offset: 0x000512AB
			// (set) Token: 0x060016B9 RID: 5817 RVA: 0x000530B3 File Offset: 0x000512B3
			internal bool AuthorWritten
			{
				get
				{
					return this.authorWritten;
				}
				set
				{
					this.authorWritten = value;
				}
			}

			// Token: 0x04000931 RID: 2353
			private bool authorWritten;
		}

		// Token: 0x020002A1 RID: 673
		private sealed class AtomEntryScope : ODataWriterCore.EntryScope
		{
			// Token: 0x060016BA RID: 5818 RVA: 0x000530BC File Offset: 0x000512BC
			internal AtomEntryScope(ODataEntry entry, ODataFeedAndEntrySerializationInfo serializationInfo, IEdmEntitySet entitySet, IEdmEntityType entityType, bool skipWriting, bool writingResponse, ODataWriterBehavior writerBehavior, SelectedPropertiesNode selectedProperties)
				: base(entry, serializationInfo, entitySet, entityType, skipWriting, writingResponse, writerBehavior, selectedProperties)
			{
			}

			// Token: 0x060016BB RID: 5819 RVA: 0x000530DC File Offset: 0x000512DC
			internal void SetWrittenElement(ODataAtomWriter.AtomElement atomElement)
			{
				this.alreadyWrittenElements |= (int)atomElement;
			}

			// Token: 0x060016BC RID: 5820 RVA: 0x000530EC File Offset: 0x000512EC
			internal bool IsElementWritten(ODataAtomWriter.AtomElement atomElement)
			{
				return (this.alreadyWrittenElements & (int)atomElement) == (int)atomElement;
			}

			// Token: 0x04000932 RID: 2354
			private int alreadyWrittenElements;
		}
	}
}
