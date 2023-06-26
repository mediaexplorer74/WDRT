using System;
using System.Collections.Generic;
using System.Xml;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x0200021E RID: 542
	internal sealed class ODataAtomServiceDocumentDeserializer : ODataAtomDeserializer
	{
		// Token: 0x060010E8 RID: 4328 RVA: 0x0003F60C File Offset: 0x0003D80C
		internal ODataAtomServiceDocumentDeserializer(ODataAtomInputContext atomInputContext)
			: base(atomInputContext)
		{
			XmlNameTable nameTable = base.XmlReader.NameTable;
			this.AtomPublishingServiceElementName = nameTable.Add("service");
			this.AtomPublishingWorkspaceElementName = nameTable.Add("workspace");
			this.AtomPublishingCollectionElementName = nameTable.Add("collection");
			this.AtomPublishingAcceptElementName = nameTable.Add("accept");
			this.AtomPublishingCategoriesElementName = nameTable.Add("categories");
			this.AtomHRefAttributeName = nameTable.Add("href");
			this.AtomPublishingNamespace = nameTable.Add("http://www.w3.org/2007/app");
			this.AtomNamespace = nameTable.Add("http://www.w3.org/2005/Atom");
			this.AtomTitleElementName = nameTable.Add("title");
			this.EmptyNamespace = nameTable.Add(string.Empty);
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x060010E9 RID: 4329 RVA: 0x0003F6D6 File Offset: 0x0003D8D6
		private ODataAtomServiceDocumentMetadataDeserializer ServiceDocumentMetadataDeserializer
		{
			get
			{
				if (this.serviceDocumentMetadataDeserializer == null)
				{
					this.serviceDocumentMetadataDeserializer = new ODataAtomServiceDocumentMetadataDeserializer(base.AtomInputContext);
				}
				return this.serviceDocumentMetadataDeserializer;
			}
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x0003F6F8 File Offset: 0x0003D8F8
		internal ODataWorkspace ReadServiceDocument()
		{
			base.ReadPayloadStart();
			if (!base.XmlReader.NamespaceEquals(this.AtomPublishingNamespace) || !base.XmlReader.LocalNameEquals(this.AtomPublishingServiceElementName))
			{
				throw new ODataException(Strings.ODataAtomServiceDocumentDeserializer_ServiceDocumentRootElementWrongNameOrNamespace(base.XmlReader.LocalName, base.XmlReader.NamespaceURI));
			}
			ODataWorkspace odataWorkspace = null;
			if (!base.XmlReader.IsEmptyElement)
			{
				base.XmlReader.Read();
				odataWorkspace = this.ReadWorkspace();
			}
			if (odataWorkspace == null)
			{
				throw new ODataException(Strings.ODataAtomServiceDocumentDeserializer_MissingWorkspaceElement);
			}
			this.SkipToElementInAtomPublishingNamespace();
			if (base.XmlReader.NodeType != XmlNodeType.Element)
			{
				base.XmlReader.Read();
				base.ReadPayloadEnd();
				return odataWorkspace;
			}
			if (base.XmlReader.LocalNameEquals(this.AtomPublishingWorkspaceElementName))
			{
				throw new ODataException(Strings.ODataAtomServiceDocumentDeserializer_MultipleWorkspaceElementsFound);
			}
			throw new ODataException(Strings.ODataAtomServiceDocumentDeserializer_UnexpectedElementInServiceDocument(base.XmlReader.LocalName));
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x0003F7E0 File Offset: 0x0003D9E0
		private ODataWorkspace ReadWorkspace()
		{
			bool enableAtomMetadataReading = base.AtomInputContext.MessageReaderSettings.EnableAtomMetadataReading;
			this.SkipToElementInAtomPublishingNamespace();
			if (base.XmlReader.NodeType == XmlNodeType.EndElement)
			{
				return null;
			}
			if (!base.XmlReader.LocalNameEquals(this.AtomPublishingWorkspaceElementName))
			{
				throw new ODataException(Strings.ODataAtomServiceDocumentDeserializer_UnexpectedElementInServiceDocument(base.XmlReader.LocalName));
			}
			List<ODataResourceCollectionInfo> list = new List<ODataResourceCollectionInfo>();
			AtomWorkspaceMetadata atomWorkspaceMetadata = null;
			if (enableAtomMetadataReading)
			{
				atomWorkspaceMetadata = new AtomWorkspaceMetadata();
			}
			if (!base.XmlReader.IsEmptyElement)
			{
				base.XmlReader.ReadStartElement();
				for (;;)
				{
					base.XmlReader.SkipInsignificantNodes();
					XmlNodeType nodeType = base.XmlReader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType != XmlNodeType.EndElement)
						{
							base.XmlReader.Skip();
						}
					}
					else if (base.XmlReader.NamespaceEquals(this.AtomPublishingNamespace))
					{
						if (!base.XmlReader.LocalNameEquals(this.AtomPublishingCollectionElementName))
						{
							break;
						}
						ODataResourceCollectionInfo odataResourceCollectionInfo = this.ReadCollectionElement();
						list.Add(odataResourceCollectionInfo);
					}
					else if (enableAtomMetadataReading && base.XmlReader.NamespaceEquals(this.AtomNamespace))
					{
						if (base.XmlReader.LocalNameEquals(this.AtomTitleElementName))
						{
							this.ServiceDocumentMetadataDeserializer.ReadTitleElementInWorkspace(atomWorkspaceMetadata);
						}
						else
						{
							base.XmlReader.Skip();
						}
					}
					else
					{
						base.XmlReader.Skip();
					}
					if (base.XmlReader.NodeType == XmlNodeType.EndElement)
					{
						goto IL_162;
					}
				}
				throw new ODataException(Strings.ODataAtomServiceDocumentDeserializer_UnexpectedElementInWorkspace(base.XmlReader.LocalName));
			}
			IL_162:
			base.XmlReader.Read();
			ODataWorkspace odataWorkspace = new ODataWorkspace
			{
				Collections = new ReadOnlyEnumerable<ODataResourceCollectionInfo>(list)
			};
			if (enableAtomMetadataReading)
			{
				odataWorkspace.SetAnnotation<AtomWorkspaceMetadata>(atomWorkspaceMetadata);
			}
			return odataWorkspace;
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x0003F980 File Offset: 0x0003DB80
		private ODataResourceCollectionInfo ReadCollectionElement()
		{
			ODataResourceCollectionInfo odataResourceCollectionInfo = new ODataResourceCollectionInfo();
			string attribute = base.XmlReader.GetAttribute(this.AtomHRefAttributeName, this.EmptyNamespace);
			ValidationUtils.ValidateResourceCollectionInfoUrl(attribute);
			odataResourceCollectionInfo.Url = base.ProcessUriFromPayload(attribute, base.XmlReader.XmlBaseUri);
			bool enableAtomMetadataReading = base.MessageReaderSettings.EnableAtomMetadataReading;
			AtomResourceCollectionMetadata atomResourceCollectionMetadata = null;
			if (enableAtomMetadataReading)
			{
				atomResourceCollectionMetadata = new AtomResourceCollectionMetadata();
			}
			if (!base.XmlReader.IsEmptyElement)
			{
				base.XmlReader.ReadStartElement();
				for (;;)
				{
					XmlNodeType nodeType = base.XmlReader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType != XmlNodeType.EndElement)
						{
							base.XmlReader.Skip();
						}
					}
					else if (base.XmlReader.NamespaceEquals(this.AtomPublishingNamespace))
					{
						if (base.XmlReader.LocalNameEquals(this.AtomPublishingCategoriesElementName))
						{
							if (enableAtomMetadataReading)
							{
								this.ServiceDocumentMetadataDeserializer.ReadCategoriesElementInCollection(atomResourceCollectionMetadata);
							}
							else
							{
								base.XmlReader.Skip();
							}
						}
						else
						{
							if (!base.XmlReader.LocalNameEquals(this.AtomPublishingAcceptElementName))
							{
								break;
							}
							if (enableAtomMetadataReading)
							{
								this.ServiceDocumentMetadataDeserializer.ReadAcceptElementInCollection(atomResourceCollectionMetadata);
							}
							else
							{
								base.XmlReader.Skip();
							}
						}
					}
					else if (base.XmlReader.NamespaceEquals(this.AtomNamespace))
					{
						if (base.XmlReader.LocalNameEquals(this.AtomTitleElementName))
						{
							this.ServiceDocumentMetadataDeserializer.ReadTitleElementInCollection(atomResourceCollectionMetadata, odataResourceCollectionInfo);
						}
						else
						{
							base.XmlReader.Skip();
						}
					}
					else
					{
						base.XmlReader.Skip();
					}
					if (base.XmlReader.NodeType == XmlNodeType.EndElement)
					{
						goto IL_18B;
					}
				}
				throw new ODataException(Strings.ODataAtomServiceDocumentDeserializer_UnexpectedElementInResourceCollection(base.XmlReader.LocalName));
			}
			IL_18B:
			base.XmlReader.Read();
			if (enableAtomMetadataReading)
			{
				odataResourceCollectionInfo.SetAnnotation<AtomResourceCollectionMetadata>(atomResourceCollectionMetadata);
			}
			return odataResourceCollectionInfo;
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x0003FB30 File Offset: 0x0003DD30
		private void SkipToElementInAtomPublishingNamespace()
		{
			for (;;)
			{
				XmlNodeType nodeType = base.XmlReader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						return;
					}
					base.XmlReader.Skip();
				}
				else
				{
					if (base.XmlReader.NamespaceEquals(this.AtomPublishingNamespace))
					{
						break;
					}
					base.XmlReader.Skip();
				}
			}
		}

		// Token: 0x0400062B RID: 1579
		private readonly string AtomPublishingServiceElementName;

		// Token: 0x0400062C RID: 1580
		private readonly string AtomPublishingWorkspaceElementName;

		// Token: 0x0400062D RID: 1581
		private readonly string AtomHRefAttributeName;

		// Token: 0x0400062E RID: 1582
		private readonly string AtomPublishingCollectionElementName;

		// Token: 0x0400062F RID: 1583
		private readonly string AtomPublishingCategoriesElementName;

		// Token: 0x04000630 RID: 1584
		private readonly string AtomPublishingAcceptElementName;

		// Token: 0x04000631 RID: 1585
		private readonly string AtomPublishingNamespace;

		// Token: 0x04000632 RID: 1586
		private readonly string AtomNamespace;

		// Token: 0x04000633 RID: 1587
		private readonly string AtomTitleElementName;

		// Token: 0x04000634 RID: 1588
		private readonly string EmptyNamespace;

		// Token: 0x04000635 RID: 1589
		private ODataAtomServiceDocumentMetadataDeserializer serviceDocumentMetadataDeserializer;
	}
}
