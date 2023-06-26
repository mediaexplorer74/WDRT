using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Data.Edm.Library;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x020001FA RID: 506
	internal sealed class ODataAtomEntityReferenceLinkDeserializer : ODataAtomDeserializer
	{
		// Token: 0x06000F71 RID: 3953 RVA: 0x000377E0 File Offset: 0x000359E0
		internal ODataAtomEntityReferenceLinkDeserializer(ODataAtomInputContext atomInputContext)
			: base(atomInputContext)
		{
			XmlNameTable nameTable = base.XmlReader.NameTable;
			this.ODataLinksElementName = nameTable.Add("links");
			this.ODataCountElementName = nameTable.Add("count");
			this.ODataNextElementName = nameTable.Add("next");
			this.ODataUriElementName = nameTable.Add("uri");
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x00037844 File Offset: 0x00035A44
		internal ODataEntityReferenceLinks ReadEntityReferenceLinks()
		{
			base.ReadPayloadStart();
			if (!base.XmlReader.NamespaceEquals(base.XmlReader.ODataNamespace) || !base.XmlReader.LocalNameEquals(this.ODataLinksElementName))
			{
				throw new ODataException(Strings.ODataAtomEntityReferenceLinkDeserializer_InvalidEntityReferenceLinksStartElement(base.XmlReader.LocalName, base.XmlReader.NamespaceURI));
			}
			ODataEntityReferenceLinks odataEntityReferenceLinks = this.ReadLinksElement();
			base.ReadPayloadEnd();
			return odataEntityReferenceLinks;
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x000378B4 File Offset: 0x00035AB4
		internal ODataEntityReferenceLink ReadEntityReferenceLink()
		{
			base.ReadPayloadStart();
			if ((!base.XmlReader.NamespaceEquals(base.XmlReader.ODataNamespace) && !base.XmlReader.NamespaceEquals(base.XmlReader.ODataMetadataNamespace)) || !base.XmlReader.LocalNameEquals(this.ODataUriElementName))
			{
				throw new ODataException(Strings.ODataAtomEntityReferenceLinkDeserializer_InvalidEntityReferenceLinkStartElement(base.XmlReader.LocalName, base.XmlReader.NamespaceURI));
			}
			ODataEntityReferenceLink odataEntityReferenceLink = this.ReadUriElement();
			base.ReadPayloadEnd();
			return odataEntityReferenceLink;
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x00037939 File Offset: 0x00035B39
		private static void VerifyEntityReferenceLinksElementNotFound(ref ODataAtomEntityReferenceLinkDeserializer.DuplicateEntityReferenceLinksElementBitMask elementsFoundBitField, ODataAtomEntityReferenceLinkDeserializer.DuplicateEntityReferenceLinksElementBitMask elementFoundBitMask, string elementNamespace, string elementName)
		{
			if ((elementsFoundBitField & elementFoundBitMask) == elementFoundBitMask)
			{
				throw new ODataException(Strings.ODataAtomEntityReferenceLinkDeserializer_MultipleEntityReferenceLinksElementsWithSameName(elementNamespace, elementName));
			}
			elementsFoundBitField |= elementFoundBitMask;
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x00037958 File Offset: 0x00035B58
		private ODataEntityReferenceLinks ReadLinksElement()
		{
			ODataEntityReferenceLinks odataEntityReferenceLinks = new ODataEntityReferenceLinks();
			List<ODataEntityReferenceLink> list = new List<ODataEntityReferenceLink>();
			ODataAtomEntityReferenceLinkDeserializer.DuplicateEntityReferenceLinksElementBitMask duplicateEntityReferenceLinksElementBitMask = ODataAtomEntityReferenceLinkDeserializer.DuplicateEntityReferenceLinksElementBitMask.None;
			if (!base.XmlReader.IsEmptyElement)
			{
				base.XmlReader.Read();
				for (;;)
				{
					XmlNodeType nodeType = base.XmlReader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType != XmlNodeType.EndElement)
						{
							goto IL_16F;
						}
					}
					else if (base.XmlReader.NamespaceEquals(base.XmlReader.ODataMetadataNamespace) && base.XmlReader.LocalNameEquals(this.ODataCountElementName) && base.Version >= ODataVersion.V2)
					{
						ODataAtomEntityReferenceLinkDeserializer.VerifyEntityReferenceLinksElementNotFound(ref duplicateEntityReferenceLinksElementBitMask, ODataAtomEntityReferenceLinkDeserializer.DuplicateEntityReferenceLinksElementBitMask.Count, base.XmlReader.ODataMetadataNamespace, "count");
						long num = (long)AtomValueUtils.ReadPrimitiveValue(base.XmlReader, EdmCoreModel.Instance.GetInt64(false));
						odataEntityReferenceLinks.Count = new long?(num);
						base.XmlReader.Read();
					}
					else
					{
						if (!base.XmlReader.NamespaceEquals(base.XmlReader.ODataNamespace))
						{
							goto IL_16F;
						}
						if (base.XmlReader.LocalNameEquals(this.ODataUriElementName))
						{
							ODataEntityReferenceLink odataEntityReferenceLink = this.ReadUriElement();
							list.Add(odataEntityReferenceLink);
						}
						else
						{
							if (!base.XmlReader.LocalNameEquals(this.ODataNextElementName) || base.Version < ODataVersion.V2)
							{
								goto IL_16F;
							}
							ODataAtomEntityReferenceLinkDeserializer.VerifyEntityReferenceLinksElementNotFound(ref duplicateEntityReferenceLinksElementBitMask, ODataAtomEntityReferenceLinkDeserializer.DuplicateEntityReferenceLinksElementBitMask.NextLink, base.XmlReader.ODataNamespace, "next");
							Uri xmlBaseUri = base.XmlReader.XmlBaseUri;
							string text = base.XmlReader.ReadElementValue();
							odataEntityReferenceLinks.NextPageLink = base.ProcessUriFromPayload(text, xmlBaseUri);
						}
					}
					IL_17A:
					if (base.XmlReader.NodeType == XmlNodeType.EndElement)
					{
						break;
					}
					continue;
					IL_16F:
					base.XmlReader.Skip();
					goto IL_17A;
				}
			}
			base.XmlReader.Read();
			odataEntityReferenceLinks.Links = new ReadOnlyEnumerable<ODataEntityReferenceLink>(list);
			return odataEntityReferenceLinks;
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x00037B0C File Offset: 0x00035D0C
		private ODataEntityReferenceLink ReadUriElement()
		{
			ODataEntityReferenceLink odataEntityReferenceLink = new ODataEntityReferenceLink();
			Uri xmlBaseUri = base.XmlReader.XmlBaseUri;
			string text = base.XmlReader.ReadElementValue();
			Uri uri = base.ProcessUriFromPayload(text, xmlBaseUri);
			odataEntityReferenceLink.Url = uri;
			ReaderValidationUtils.ValidateEntityReferenceLink(odataEntityReferenceLink);
			return odataEntityReferenceLink;
		}

		// Token: 0x04000575 RID: 1397
		private readonly string ODataLinksElementName;

		// Token: 0x04000576 RID: 1398
		private readonly string ODataCountElementName;

		// Token: 0x04000577 RID: 1399
		private readonly string ODataNextElementName;

		// Token: 0x04000578 RID: 1400
		private readonly string ODataUriElementName;

		// Token: 0x020001FB RID: 507
		[Flags]
		private enum DuplicateEntityReferenceLinksElementBitMask
		{
			// Token: 0x0400057A RID: 1402
			None = 0,
			// Token: 0x0400057B RID: 1403
			Count = 1,
			// Token: 0x0400057C RID: 1404
			NextLink = 2
		}
	}
}
