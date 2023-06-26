using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x020001E2 RID: 482
	internal sealed class ODataAtomFeedMetadataDeserializer : ODataAtomMetadataDeserializer
	{
		// Token: 0x06000EE9 RID: 3817 RVA: 0x00034FAC File Offset: 0x000331AC
		internal ODataAtomFeedMetadataDeserializer(ODataAtomInputContext atomInputContext, bool inSourceElement)
			: base(atomInputContext)
		{
			XmlNameTable nameTable = base.XmlReader.NameTable;
			this.EmptyNamespace = nameTable.Add(string.Empty);
			this.InSourceElement = inSourceElement;
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000EEA RID: 3818 RVA: 0x00034FE4 File Offset: 0x000331E4
		// (set) Token: 0x06000EEB RID: 3819 RVA: 0x00034FEC File Offset: 0x000331EC
		private bool InSourceElement { get; set; }

		// Token: 0x06000EEC RID: 3820 RVA: 0x00034FF8 File Offset: 0x000331F8
		internal void ReadAtomElementAsFeedMetadata(AtomFeedMetadata atomFeedMetadata)
		{
			string localName;
			switch (localName = base.XmlReader.LocalName)
			{
			case "author":
				this.ReadAuthorElement(atomFeedMetadata);
				return;
			case "category":
				this.ReadCategoryElement(atomFeedMetadata);
				return;
			case "contributor":
				this.ReadContributorElement(atomFeedMetadata);
				return;
			case "generator":
				this.ReadGeneratorElement(atomFeedMetadata);
				return;
			case "icon":
				this.ReadIconElement(atomFeedMetadata);
				return;
			case "id":
				if (this.InSourceElement)
				{
					this.ReadIdElementAsSourceId(atomFeedMetadata);
					return;
				}
				base.XmlReader.Skip();
				return;
			case "link":
				this.ReadLinkElementIntoLinksCollection(atomFeedMetadata);
				return;
			case "logo":
				this.ReadLogoElement(atomFeedMetadata);
				return;
			case "rights":
				this.ReadRightsElement(atomFeedMetadata);
				return;
			case "subtitle":
				this.ReadSubtitleElement(atomFeedMetadata);
				return;
			case "title":
				this.ReadTitleElement(atomFeedMetadata);
				return;
			case "updated":
				this.ReadUpdatedElement(atomFeedMetadata);
				return;
			}
			base.XmlReader.Skip();
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x00035190 File Offset: 0x00033390
		internal AtomLinkMetadata ReadAtomLinkElementInFeed(string relation, string hrefStringValue)
		{
			AtomLinkMetadata atomLinkMetadata = new AtomLinkMetadata
			{
				Relation = relation,
				Href = ((hrefStringValue == null) ? null : base.ProcessUriFromPayload(hrefStringValue, base.XmlReader.XmlBaseUri))
			};
			while (base.XmlReader.MoveToNextAttribute())
			{
				string localName;
				if (base.XmlReader.NamespaceEquals(this.EmptyNamespace) && (localName = base.XmlReader.LocalName) != null)
				{
					if (<PrivateImplementationDetails>{E8251738-41A8-451C-AA83-3F2B33F0CD5E}.$$method0x6000e61-1 == null)
					{
						<PrivateImplementationDetails>{E8251738-41A8-451C-AA83-3F2B33F0CD5E}.$$method0x6000e61-1 = new Dictionary<string, int>(6)
						{
							{ "type", 0 },
							{ "hreflang", 1 },
							{ "title", 2 },
							{ "length", 3 },
							{ "rel", 4 },
							{ "href", 5 }
						};
					}
					int num;
					if (<PrivateImplementationDetails>{E8251738-41A8-451C-AA83-3F2B33F0CD5E}.$$method0x6000e61-1.TryGetValue(localName, out num))
					{
						switch (num)
						{
						case 0:
							atomLinkMetadata.MediaType = base.XmlReader.Value;
							break;
						case 1:
							atomLinkMetadata.HrefLang = base.XmlReader.Value;
							break;
						case 2:
							atomLinkMetadata.Title = base.XmlReader.Value;
							break;
						case 3:
						{
							string value = base.XmlReader.Value;
							int num2;
							if (!int.TryParse(value, NumberStyles.Any, NumberFormatInfo.InvariantInfo, out num2))
							{
								throw new ODataException(Strings.EpmSyndicationWriter_InvalidLinkLengthValue(value));
							}
							atomLinkMetadata.Length = new int?(num2);
							break;
						}
						case 4:
							if (atomLinkMetadata.Relation == null)
							{
								atomLinkMetadata.Relation = base.XmlReader.Value;
							}
							break;
						case 5:
							if (atomLinkMetadata.Href == null)
							{
								atomLinkMetadata.Href = base.ProcessUriFromPayload(base.XmlReader.Value, base.XmlReader.XmlBaseUri);
							}
							break;
						}
					}
				}
			}
			base.XmlReader.Skip();
			return atomLinkMetadata;
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x00035372 File Offset: 0x00033572
		private void ReadAuthorElement(AtomFeedMetadata atomFeedMetadata)
		{
			atomFeedMetadata.AddAuthor(base.ReadAtomPersonConstruct(null));
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x00035384 File Offset: 0x00033584
		private void ReadCategoryElement(AtomFeedMetadata atomFeedMetadata)
		{
			AtomCategoryMetadata atomCategoryMetadata = new AtomCategoryMetadata();
			while (base.XmlReader.MoveToNextAttribute())
			{
				string localName;
				if (base.XmlReader.NamespaceEquals(this.EmptyNamespace) && (localName = base.XmlReader.LocalName) != null)
				{
					if (!(localName == "scheme"))
					{
						if (!(localName == "term"))
						{
							if (localName == "label")
							{
								atomCategoryMetadata.Label = base.XmlReader.Value;
							}
						}
						else
						{
							atomCategoryMetadata.Term = base.XmlReader.Value;
						}
					}
					else
					{
						atomCategoryMetadata.Scheme = base.XmlReader.Value;
					}
				}
			}
			atomFeedMetadata.AddCategory(atomCategoryMetadata);
			base.XmlReader.Skip();
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x00035440 File Offset: 0x00033640
		private void ReadContributorElement(AtomFeedMetadata atomFeedMetadata)
		{
			atomFeedMetadata.AddContributor(base.ReadAtomPersonConstruct(null));
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x00035450 File Offset: 0x00033650
		private void ReadGeneratorElement(AtomFeedMetadata atomFeedMetadata)
		{
			this.VerifyNotPreviouslyDefined(atomFeedMetadata.Generator);
			AtomGeneratorMetadata atomGeneratorMetadata = new AtomGeneratorMetadata();
			while (base.XmlReader.MoveToNextAttribute())
			{
				string localName;
				if (base.XmlReader.NamespaceEquals(this.EmptyNamespace) && (localName = base.XmlReader.LocalName) != null)
				{
					if (!(localName == "uri"))
					{
						if (localName == "version")
						{
							atomGeneratorMetadata.Version = base.XmlReader.Value;
						}
					}
					else
					{
						atomGeneratorMetadata.Uri = base.ProcessUriFromPayload(base.XmlReader.Value, base.XmlReader.XmlBaseUri);
					}
				}
			}
			base.XmlReader.MoveToElement();
			if (base.XmlReader.IsEmptyElement)
			{
				base.XmlReader.Skip();
			}
			else
			{
				atomGeneratorMetadata.Name = base.XmlReader.ReadElementValue();
			}
			atomFeedMetadata.Generator = atomGeneratorMetadata;
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x0003552F File Offset: 0x0003372F
		private void ReadIconElement(AtomFeedMetadata atomFeedMetadata)
		{
			this.VerifyNotPreviouslyDefined(atomFeedMetadata.Icon);
			atomFeedMetadata.Icon = this.ReadUriValuedElement();
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x00035549 File Offset: 0x00033749
		private void ReadIdElementAsSourceId(AtomFeedMetadata atomFeedMetadata)
		{
			this.VerifyNotPreviouslyDefined(atomFeedMetadata.SourceId);
			atomFeedMetadata.SourceId = base.XmlReader.ReadElementValue();
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x00035568 File Offset: 0x00033768
		private void ReadLinkElementIntoLinksCollection(AtomFeedMetadata atomFeedMetadata)
		{
			AtomLinkMetadata atomLinkMetadata = this.ReadAtomLinkElementInFeed(null, null);
			atomFeedMetadata.AddLink(atomLinkMetadata);
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x00035585 File Offset: 0x00033785
		private void ReadLogoElement(AtomFeedMetadata atomFeedMetadata)
		{
			this.VerifyNotPreviouslyDefined(atomFeedMetadata.Logo);
			atomFeedMetadata.Logo = this.ReadUriValuedElement();
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x0003559F File Offset: 0x0003379F
		private void ReadRightsElement(AtomFeedMetadata atomFeedMetadata)
		{
			this.VerifyNotPreviouslyDefined(atomFeedMetadata.Rights);
			atomFeedMetadata.Rights = base.ReadAtomTextConstruct();
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x000355B9 File Offset: 0x000337B9
		private void ReadSubtitleElement(AtomFeedMetadata atomFeedMetadata)
		{
			this.VerifyNotPreviouslyDefined(atomFeedMetadata.Subtitle);
			atomFeedMetadata.Subtitle = base.ReadAtomTextConstruct();
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x000355D3 File Offset: 0x000337D3
		private void ReadTitleElement(AtomFeedMetadata atomFeedMetadata)
		{
			this.VerifyNotPreviouslyDefined(atomFeedMetadata.Title);
			atomFeedMetadata.Title = base.ReadAtomTextConstruct();
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x000355ED File Offset: 0x000337ED
		private void ReadUpdatedElement(AtomFeedMetadata atomFeedMetadata)
		{
			this.VerifyNotPreviouslyDefined(atomFeedMetadata.Updated);
			atomFeedMetadata.Updated = base.ReadAtomDateConstruct();
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x0003560C File Offset: 0x0003380C
		private Uri ReadUriValuedElement()
		{
			string text = base.XmlReader.ReadElementValue();
			return base.ProcessUriFromPayload(text, base.XmlReader.XmlBaseUri);
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x00035638 File Offset: 0x00033838
		private void VerifyNotPreviouslyDefined(object metadataValue)
		{
			if (metadataValue != null)
			{
				string text = (this.InSourceElement ? "source" : "feed");
				throw new ODataException(Strings.ODataAtomMetadataDeserializer_MultipleSingletonMetadataElements(base.XmlReader.LocalName, text));
			}
		}

		// Token: 0x04000524 RID: 1316
		private readonly string EmptyNamespace;
	}
}
