using System;
using System.Linq;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x020000F4 RID: 244
	internal sealed class ODataAtomEntryAndFeedSerializer : ODataAtomPropertyAndValueSerializer
	{
		// Token: 0x0600062C RID: 1580 RVA: 0x0001611E File Offset: 0x0001431E
		internal ODataAtomEntryAndFeedSerializer(ODataAtomOutputContext atomOutputContext)
			: base(atomOutputContext)
		{
			this.atomEntryMetadataSerializer = new ODataAtomEntryMetadataSerializer(atomOutputContext);
			this.atomFeedMetadataSerializer = new ODataAtomFeedMetadataSerializer(atomOutputContext);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0001613F File Offset: 0x0001433F
		internal void WriteEntryPropertiesStart()
		{
			base.XmlWriter.WriteStartElement("m", "properties", "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata");
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0001615B File Offset: 0x0001435B
		internal void WriteEntryPropertiesEnd()
		{
			base.XmlWriter.WriteEndElement();
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00016168 File Offset: 0x00014368
		internal void WriteEntryTypeName(string typeName, AtomEntryMetadata entryMetadata)
		{
			if (typeName != null)
			{
				AtomCategoryMetadata atomCategoryMetadata = ODataAtomWriterMetadataUtils.MergeCategoryMetadata((entryMetadata == null) ? null : entryMetadata.CategoryWithTypeName, typeName, base.MessageWriterSettings.WriterBehavior.ODataTypeScheme);
				this.atomEntryMetadataSerializer.WriteCategory(atomCategoryMetadata);
			}
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x000161A7 File Offset: 0x000143A7
		internal void WriteEntryMetadata(AtomEntryMetadata entryMetadata, AtomEntryMetadata epmEntryMetadata, string updatedTime)
		{
			this.atomEntryMetadataSerializer.WriteEntryMetadata(entryMetadata, epmEntryMetadata, updatedTime);
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x000161B7 File Offset: 0x000143B7
		internal void WriteEntryId(string entryId)
		{
			base.WriteElementWithTextContent("", "id", "http://www.w3.org/2005/Atom", entryId);
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x000161D0 File Offset: 0x000143D0
		internal void WriteEntryReadLink(Uri readLink, AtomEntryMetadata entryMetadata)
		{
			AtomLinkMetadata atomLinkMetadata = ((entryMetadata == null) ? null : entryMetadata.SelfLink);
			this.WriteReadOrEditLink(readLink, atomLinkMetadata, "self");
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x000161F8 File Offset: 0x000143F8
		internal void WriteEntryEditLink(Uri editLink, AtomEntryMetadata entryMetadata)
		{
			AtomLinkMetadata atomLinkMetadata = ((entryMetadata == null) ? null : entryMetadata.EditLink);
			this.WriteReadOrEditLink(editLink, atomLinkMetadata, "edit");
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00016220 File Offset: 0x00014420
		internal void WriteEntryMediaEditLink(ODataStreamReferenceValue mediaResource)
		{
			Uri editLink = mediaResource.EditLink;
			if (editLink != null)
			{
				AtomStreamReferenceMetadata annotation = mediaResource.GetAnnotation<AtomStreamReferenceMetadata>();
				AtomLinkMetadata atomLinkMetadata = ((annotation == null) ? null : annotation.EditLink);
				AtomLinkMetadata atomLinkMetadata2 = ODataAtomWriterMetadataUtils.MergeLinkMetadata(atomLinkMetadata, "edit-media", editLink, null, null);
				this.atomEntryMetadataSerializer.WriteAtomLink(atomLinkMetadata2, mediaResource.ETag);
			}
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00016274 File Offset: 0x00014474
		internal void WriteAssociationLink(ODataAssociationLink associationLink, IEdmEntityType owningType, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, ProjectedPropertiesAnnotation projectedProperties)
		{
			ValidationUtils.ValidateAssociationLinkNotNull(associationLink);
			string name = associationLink.Name;
			if (projectedProperties.ShouldSkipProperty(name))
			{
				return;
			}
			base.ValidateAssociationLink(associationLink, owningType);
			duplicatePropertyNamesChecker.CheckForDuplicateAssociationLinkNames(associationLink);
			AtomLinkMetadata annotation = associationLink.GetAnnotation<AtomLinkMetadata>();
			string text = AtomUtils.ComputeODataAssociationLinkRelation(associationLink);
			AtomLinkMetadata atomLinkMetadata = ODataAtomWriterMetadataUtils.MergeLinkMetadata(annotation, text, associationLink.Url, name, "application/xml");
			this.atomEntryMetadataSerializer.WriteAtomLink(atomLinkMetadata, null);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x000162D8 File Offset: 0x000144D8
		internal void WriteNavigationLinkStart(ODataNavigationLink navigationLink, Uri navigationLinkUrlOverride)
		{
			base.XmlWriter.WriteStartElement("", "link", "http://www.w3.org/2005/Atom");
			string text = AtomUtils.ComputeODataNavigationLinkRelation(navigationLink);
			string text2 = AtomUtils.ComputeODataNavigationLinkType(navigationLink);
			string name = navigationLink.Name;
			Uri uri = navigationLinkUrlOverride ?? navigationLink.Url;
			AtomLinkMetadata annotation = navigationLink.GetAnnotation<AtomLinkMetadata>();
			AtomLinkMetadata atomLinkMetadata = ODataAtomWriterMetadataUtils.MergeLinkMetadata(annotation, text, uri, name, text2);
			this.atomEntryMetadataSerializer.WriteAtomLinkAttributes(atomLinkMetadata, null);
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00016344 File Offset: 0x00014544
		internal void WriteFeedMetadata(ODataFeed feed, string updatedTime, out bool authorWritten)
		{
			AtomFeedMetadata annotation = feed.GetAnnotation<AtomFeedMetadata>();
			if (annotation == null)
			{
				base.WriteElementWithTextContent("", "id", "http://www.w3.org/2005/Atom", feed.Id);
				base.WriteEmptyElement("", "title", "http://www.w3.org/2005/Atom");
				base.WriteElementWithTextContent("", "updated", "http://www.w3.org/2005/Atom", updatedTime);
				authorWritten = false;
				return;
			}
			this.atomFeedMetadataSerializer.WriteFeedMetadata(annotation, feed, updatedTime, out authorWritten);
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x000163B4 File Offset: 0x000145B4
		internal void WriteFeedDefaultAuthor()
		{
			this.atomFeedMetadataSerializer.WriteEmptyAuthor();
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x000163D0 File Offset: 0x000145D0
		internal void WriteFeedNextPageLink(ODataFeed feed)
		{
			Uri nextPageLink = feed.NextPageLink;
			if (nextPageLink != null)
			{
				this.WriteFeedLink(feed, "next", nextPageLink, delegate(AtomFeedMetadata feedMetadata)
				{
					if (feedMetadata != null)
					{
						return feedMetadata.NextPageLink;
					}
					return null;
				});
			}
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00016458 File Offset: 0x00014658
		internal void WriteFeedDeltaLink(ODataFeed feed)
		{
			Uri deltaLink = feed.DeltaLink;
			if (deltaLink != null)
			{
				this.WriteFeedLink(feed, "http://docs.oasis-open.org/odata/ns/delta", deltaLink, delegate(AtomFeedMetadata feedMetadata)
				{
					if (feedMetadata != null)
					{
						return feedMetadata.Links.FirstOrDefault((AtomLinkMetadata link) => link.Relation == "http://docs.oasis-open.org/odata/ns/delta");
					}
					return null;
				});
			}
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x000164A0 File Offset: 0x000146A0
		internal void WriteFeedLink(ODataFeed feed, string relation, Uri href, Func<AtomFeedMetadata, AtomLinkMetadata> getLinkMetadata)
		{
			AtomFeedMetadata annotation = feed.GetAnnotation<AtomFeedMetadata>();
			AtomLinkMetadata atomLinkMetadata = ODataAtomWriterMetadataUtils.MergeLinkMetadata(getLinkMetadata(annotation), relation, href, null, null);
			this.atomFeedMetadataSerializer.WriteAtomLink(atomLinkMetadata, null);
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x000164D4 File Offset: 0x000146D4
		internal void WriteStreamProperty(ODataProperty streamProperty, IEdmEntityType owningType, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, ProjectedPropertiesAnnotation projectedProperties)
		{
			WriterValidationUtils.ValidatePropertyNotNull(streamProperty);
			string name = streamProperty.Name;
			if (projectedProperties.ShouldSkipProperty(name))
			{
				return;
			}
			WriterValidationUtils.ValidatePropertyName(name);
			duplicatePropertyNamesChecker.CheckForDuplicatePropertyNames(streamProperty);
			IEdmProperty edmProperty = WriterValidationUtils.ValidatePropertyDefined(streamProperty.Name, owningType, base.MessageWriterSettings.UndeclaredPropertyBehaviorKinds);
			WriterValidationUtils.ValidateStreamReferenceProperty(streamProperty, edmProperty, base.Version, base.WritingResponse);
			ODataStreamReferenceValue odataStreamReferenceValue = (ODataStreamReferenceValue)streamProperty.Value;
			WriterValidationUtils.ValidateStreamReferenceValue(odataStreamReferenceValue, false);
			if (owningType != null && owningType.IsOpen && edmProperty == null)
			{
				ValidationUtils.ValidateOpenPropertyValue(streamProperty.Name, odataStreamReferenceValue, base.MessageWriterSettings.UndeclaredPropertyBehaviorKinds);
			}
			AtomStreamReferenceMetadata annotation = odataStreamReferenceValue.GetAnnotation<AtomStreamReferenceMetadata>();
			string contentType = odataStreamReferenceValue.ContentType;
			string name2 = streamProperty.Name;
			Uri readLink = odataStreamReferenceValue.ReadLink;
			if (readLink != null)
			{
				string text = AtomUtils.ComputeStreamPropertyRelation(streamProperty, false);
				AtomLinkMetadata atomLinkMetadata = ((annotation == null) ? null : annotation.SelfLink);
				AtomLinkMetadata atomLinkMetadata2 = ODataAtomWriterMetadataUtils.MergeLinkMetadata(atomLinkMetadata, text, readLink, name2, contentType);
				this.atomEntryMetadataSerializer.WriteAtomLink(atomLinkMetadata2, null);
			}
			Uri editLink = odataStreamReferenceValue.EditLink;
			if (editLink != null)
			{
				string text2 = AtomUtils.ComputeStreamPropertyRelation(streamProperty, true);
				AtomLinkMetadata atomLinkMetadata3 = ((annotation == null) ? null : annotation.EditLink);
				AtomLinkMetadata atomLinkMetadata4 = ODataAtomWriterMetadataUtils.MergeLinkMetadata(atomLinkMetadata3, text2, editLink, name2, contentType);
				this.atomEntryMetadataSerializer.WriteAtomLink(atomLinkMetadata4, odataStreamReferenceValue.ETag);
			}
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00016618 File Offset: 0x00014818
		internal void WriteOperation(ODataOperation operation)
		{
			WriterValidationUtils.ValidateCanWriteOperation(operation, base.WritingResponse);
			ValidationUtils.ValidateOperationMetadataNotNull(operation);
			ValidationUtils.ValidateOperationTargetNotNull(operation);
			string text;
			if (operation is ODataAction)
			{
				text = "action";
			}
			else
			{
				text = "function";
			}
			base.XmlWriter.WriteStartElement("m", text, "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata");
			string text2 = base.UriToUrlAttributeValue(operation.Metadata, false);
			base.XmlWriter.WriteAttributeString("metadata", text2);
			if (operation.Title != null)
			{
				base.XmlWriter.WriteAttributeString("title", operation.Title);
			}
			string text3 = base.UriToUrlAttributeValue(operation.Target);
			base.XmlWriter.WriteAttributeString("target", text3);
			base.XmlWriter.WriteEndElement();
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x000166D0 File Offset: 0x000148D0
		private void WriteReadOrEditLink(Uri link, AtomLinkMetadata linkMetadata, string linkRelation)
		{
			if (link != null)
			{
				AtomLinkMetadata atomLinkMetadata = ODataAtomWriterMetadataUtils.MergeLinkMetadata(linkMetadata, linkRelation, link, null, null);
				this.atomEntryMetadataSerializer.WriteAtomLink(atomLinkMetadata, null);
			}
		}

		// Token: 0x0400027E RID: 638
		private readonly ODataAtomEntryMetadataSerializer atomEntryMetadataSerializer;

		// Token: 0x0400027F RID: 639
		private readonly ODataAtomFeedMetadataSerializer atomFeedMetadataSerializer;
	}
}
