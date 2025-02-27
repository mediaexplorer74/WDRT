﻿using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x020000F3 RID: 243
	internal sealed class ODataAtomEntityReferenceLinkSerializer : ODataAtomSerializer
	{
		// Token: 0x06000628 RID: 1576 RVA: 0x00015F56 File Offset: 0x00014156
		internal ODataAtomEntityReferenceLinkSerializer(ODataAtomOutputContext atomOutputContext)
			: base(atomOutputContext)
		{
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00015F5F File Offset: 0x0001415F
		internal void WriteEntityReferenceLink(ODataEntityReferenceLink entityReferenceLink)
		{
			base.WritePayloadStart();
			this.WriteEntityReferenceLink(entityReferenceLink, true);
			base.WritePayloadEnd();
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00015F78 File Offset: 0x00014178
		internal void WriteEntityReferenceLinks(ODataEntityReferenceLinks entityReferenceLinks)
		{
			base.WritePayloadStart();
			base.XmlWriter.WriteStartElement(string.Empty, "links", base.MessageWriterSettings.WriterBehavior.ODataNamespace);
			base.XmlWriter.WriteAttributeString("xmlns", base.MessageWriterSettings.WriterBehavior.ODataNamespace);
			if (entityReferenceLinks.Count != null)
			{
				base.WriteCount(entityReferenceLinks.Count.Value, true);
			}
			IEnumerable<ODataEntityReferenceLink> links = entityReferenceLinks.Links;
			if (links != null)
			{
				foreach (ODataEntityReferenceLink odataEntityReferenceLink in links)
				{
					WriterValidationUtils.ValidateEntityReferenceLinkNotNull(odataEntityReferenceLink);
					this.WriteEntityReferenceLink(odataEntityReferenceLink, false);
				}
			}
			if (entityReferenceLinks.NextPageLink != null)
			{
				string text = base.UriToUrlAttributeValue(entityReferenceLinks.NextPageLink);
				base.XmlWriter.WriteElementString(string.Empty, "next", base.MessageWriterSettings.WriterBehavior.ODataNamespace, text);
			}
			base.XmlWriter.WriteEndElement();
			base.WritePayloadEnd();
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0001609C File Offset: 0x0001429C
		private void WriteEntityReferenceLink(ODataEntityReferenceLink entityReferenceLink, bool isTopLevel)
		{
			WriterValidationUtils.ValidateEntityReferenceLink(entityReferenceLink);
			string text = ((base.UseClientFormatBehavior && isTopLevel) ? "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata" : base.MessageWriterSettings.WriterBehavior.ODataNamespace);
			base.XmlWriter.WriteStartElement(string.Empty, "uri", text);
			if (isTopLevel)
			{
				base.XmlWriter.WriteAttributeString("xmlns", text);
			}
			base.XmlWriter.WriteString(base.UriToUrlAttributeValue(entityReferenceLink.Url));
			base.XmlWriter.WriteEndElement();
		}
	}
}
