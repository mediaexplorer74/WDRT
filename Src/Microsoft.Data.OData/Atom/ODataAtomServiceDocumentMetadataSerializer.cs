using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x020001C0 RID: 448
	internal sealed class ODataAtomServiceDocumentMetadataSerializer : ODataAtomMetadataSerializer
	{
		// Token: 0x06000DE6 RID: 3558 RVA: 0x000309C7 File Offset: 0x0002EBC7
		internal ODataAtomServiceDocumentMetadataSerializer(ODataAtomOutputContext atomOutputContext)
			: base(atomOutputContext)
		{
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x000309D0 File Offset: 0x0002EBD0
		internal void WriteWorkspaceMetadata(ODataWorkspace workspace)
		{
			AtomWorkspaceMetadata annotation = workspace.GetAnnotation<AtomWorkspaceMetadata>();
			AtomTextConstruct atomTextConstruct = null;
			if (annotation != null)
			{
				atomTextConstruct = annotation.Title;
			}
			if (atomTextConstruct == null)
			{
				atomTextConstruct = new AtomTextConstruct
				{
					Text = "Default"
				};
			}
			if (base.UseServerFormatBehavior && atomTextConstruct.Kind == AtomTextConstructKind.Text)
			{
				base.WriteElementWithTextContent("atom", "title", "http://www.w3.org/2005/Atom", atomTextConstruct.Text);
				return;
			}
			base.WriteTextConstruct("atom", "title", "http://www.w3.org/2005/Atom", atomTextConstruct);
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x00030A48 File Offset: 0x0002EC48
		internal void WriteResourceCollectionMetadata(ODataResourceCollectionInfo collection)
		{
			AtomResourceCollectionMetadata annotation = collection.GetAnnotation<AtomResourceCollectionMetadata>();
			AtomTextConstruct atomTextConstruct = null;
			if (annotation != null)
			{
				atomTextConstruct = annotation.Title;
			}
			if (collection.Name != null)
			{
				if (atomTextConstruct == null)
				{
					atomTextConstruct = new AtomTextConstruct
					{
						Text = collection.Name
					};
				}
				else if (string.CompareOrdinal(atomTextConstruct.Text, collection.Name) != 0)
				{
					throw new ODataException(Strings.ODataAtomServiceDocumentMetadataSerializer_ResourceCollectionNameAndTitleMismatch(collection.Name, atomTextConstruct.Text));
				}
			}
			if (base.UseServerFormatBehavior && atomTextConstruct.Kind == AtomTextConstructKind.Text)
			{
				base.WriteElementWithTextContent("atom", "title", "http://www.w3.org/2005/Atom", atomTextConstruct.Text);
			}
			else
			{
				base.WriteTextConstruct("atom", "title", "http://www.w3.org/2005/Atom", atomTextConstruct);
			}
			if (annotation != null)
			{
				string accept = annotation.Accept;
				if (accept != null)
				{
					base.WriteElementWithTextContent(string.Empty, "accept", "http://www.w3.org/2007/app", accept);
				}
				AtomCategoriesMetadata categories = annotation.Categories;
				if (categories != null)
				{
					base.XmlWriter.WriteStartElement(string.Empty, "categories", "http://www.w3.org/2007/app");
					Uri href = categories.Href;
					bool? @fixed = categories.Fixed;
					string scheme = categories.Scheme;
					IEnumerable<AtomCategoryMetadata> categories2 = categories.Categories;
					if (href != null)
					{
						if (@fixed != null || scheme != null || (categories2 != null && categories2.Any<AtomCategoryMetadata>()))
						{
							throw new ODataException(Strings.ODataAtomWriterMetadataUtils_CategoriesHrefWithOtherValues);
						}
						base.XmlWriter.WriteAttributeString("href", base.UriToUrlAttributeValue(href));
					}
					else
					{
						if (@fixed != null)
						{
							base.XmlWriter.WriteAttributeString("fixed", @fixed.Value ? "yes" : "no");
						}
						if (scheme != null)
						{
							base.XmlWriter.WriteAttributeString("scheme", scheme);
						}
						if (categories2 != null)
						{
							foreach (AtomCategoryMetadata atomCategoryMetadata in categories2)
							{
								base.WriteCategory("atom", atomCategoryMetadata.Term, atomCategoryMetadata.Scheme, atomCategoryMetadata.Label);
							}
						}
					}
					base.XmlWriter.WriteEndElement();
				}
			}
		}
	}
}
