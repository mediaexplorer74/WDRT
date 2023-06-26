using System;
using System.Xml;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x020000EF RID: 239
	internal class ODataAtomSerializer : ODataSerializer
	{
		// Token: 0x06000602 RID: 1538 RVA: 0x00015162 File Offset: 0x00013362
		internal ODataAtomSerializer(ODataAtomOutputContext atomOutputContext)
			: base(atomOutputContext)
		{
			this.atomOutputContext = atomOutputContext;
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x00015172 File Offset: 0x00013372
		internal XmlWriter XmlWriter
		{
			get
			{
				return this.atomOutputContext.XmlWriter;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x0001517F File Offset: 0x0001337F
		protected ODataAtomOutputContext AtomOutputContext
		{
			get
			{
				return this.atomOutputContext;
			}
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00015187 File Offset: 0x00013387
		internal string UriToUrlAttributeValue(Uri uri)
		{
			return this.UriToUrlAttributeValue(uri, true);
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00015194 File Offset: 0x00013394
		internal string UriToUrlAttributeValue(Uri uri, bool failOnRelativeUriWithoutBaseUri)
		{
			if (base.UrlResolver != null)
			{
				Uri uri2 = base.UrlResolver.ResolveUrl(base.MessageWriterSettings.BaseUri, uri);
				if (uri2 != null)
				{
					return UriUtilsCommon.UriToString(uri2);
				}
			}
			if (!uri.IsAbsoluteUri)
			{
				if (base.MessageWriterSettings.BaseUri == null && failOnRelativeUriWithoutBaseUri)
				{
					throw new ODataException(Strings.ODataWriter_RelativeUriUsedWithoutBaseUriSpecified(UriUtilsCommon.UriToString(uri)));
				}
				uri = UriUtils.EnsureEscapedRelativeUri(uri);
			}
			return UriUtilsCommon.UriToString(uri);
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0001520E File Offset: 0x0001340E
		internal void WritePayloadStart()
		{
			this.XmlWriter.WriteStartDocument();
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0001521B File Offset: 0x0001341B
		internal void WritePayloadEnd()
		{
			this.XmlWriter.WriteEndDocument();
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00015228 File Offset: 0x00013428
		internal void WriteTopLevelError(ODataError error, bool includeDebugInformation)
		{
			this.WritePayloadStart();
			ODataAtomWriterUtils.WriteError(this.XmlWriter, error, includeDebugInformation, base.MessageWriterSettings.MessageQuotas.MaxNestingDepth);
			this.WritePayloadEnd();
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00015254 File Offset: 0x00013454
		internal void WriteDefaultNamespaceAttributes(ODataAtomSerializer.DefaultNamespaceFlags flags)
		{
			if ((flags & ODataAtomSerializer.DefaultNamespaceFlags.Atom) == ODataAtomSerializer.DefaultNamespaceFlags.Atom)
			{
				this.XmlWriter.WriteAttributeString("xmlns", "http://www.w3.org/2000/xmlns/", "http://www.w3.org/2005/Atom");
			}
			if ((flags & ODataAtomSerializer.DefaultNamespaceFlags.OData) == ODataAtomSerializer.DefaultNamespaceFlags.OData)
			{
				this.XmlWriter.WriteAttributeString("d", "http://www.w3.org/2000/xmlns/", base.MessageWriterSettings.WriterBehavior.ODataNamespace);
			}
			if ((flags & ODataAtomSerializer.DefaultNamespaceFlags.ODataMetadata) == ODataAtomSerializer.DefaultNamespaceFlags.ODataMetadata)
			{
				this.XmlWriter.WriteAttributeString("m", "http://www.w3.org/2000/xmlns/", "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata");
			}
			if (base.MessageWriterSettings.Version.Value >= ODataVersion.V3)
			{
				if ((flags & ODataAtomSerializer.DefaultNamespaceFlags.GeoRss) == ODataAtomSerializer.DefaultNamespaceFlags.GeoRss)
				{
					this.XmlWriter.WriteAttributeString("georss", "http://www.w3.org/2000/xmlns/", "http://www.georss.org/georss");
				}
				if ((flags & ODataAtomSerializer.DefaultNamespaceFlags.Gml) == ODataAtomSerializer.DefaultNamespaceFlags.Gml)
				{
					this.XmlWriter.WriteAttributeString("gml", "http://www.w3.org/2000/xmlns/", "http://www.opengis.net/gml");
				}
			}
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00015324 File Offset: 0x00013524
		internal void WriteCount(long count, bool includeNamespaceDeclaration)
		{
			this.XmlWriter.WriteStartElement("m", "count", "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata");
			if (includeNamespaceDeclaration)
			{
				this.WriteDefaultNamespaceAttributes(ODataAtomSerializer.DefaultNamespaceFlags.ODataMetadata);
			}
			this.XmlWriter.WriteValue(count);
			this.XmlWriter.WriteEndElement();
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00015364 File Offset: 0x00013564
		internal void WriteBaseUriAndDefaultNamespaceAttributes()
		{
			Uri baseUri = base.MessageWriterSettings.BaseUri;
			if (baseUri != null)
			{
				this.XmlWriter.WriteAttributeString("base", "http://www.w3.org/XML/1998/namespace", baseUri.AbsoluteUri);
			}
			this.WriteDefaultNamespaceAttributes(ODataAtomSerializer.DefaultNamespaceFlags.All);
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x000153A9 File Offset: 0x000135A9
		internal void WriteElementWithTextContent(string prefix, string localName, string ns, string textContent)
		{
			this.XmlWriter.WriteStartElement(prefix, localName, ns);
			if (textContent != null)
			{
				ODataAtomWriterUtils.WriteString(this.XmlWriter, textContent);
			}
			this.XmlWriter.WriteEndElement();
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x000153D5 File Offset: 0x000135D5
		internal void WriteEmptyElement(string prefix, string localName, string ns)
		{
			this.XmlWriter.WriteStartElement(prefix, localName, ns);
			this.XmlWriter.WriteEndElement();
		}

		// Token: 0x04000273 RID: 627
		private ODataAtomOutputContext atomOutputContext;

		// Token: 0x020000F0 RID: 240
		[Flags]
		internal enum DefaultNamespaceFlags
		{
			// Token: 0x04000275 RID: 629
			None = 0,
			// Token: 0x04000276 RID: 630
			OData = 1,
			// Token: 0x04000277 RID: 631
			ODataMetadata = 2,
			// Token: 0x04000278 RID: 632
			Atom = 4,
			// Token: 0x04000279 RID: 633
			GeoRss = 8,
			// Token: 0x0400027A RID: 634
			Gml = 16,
			// Token: 0x0400027B RID: 635
			All = 31
		}
	}
}
