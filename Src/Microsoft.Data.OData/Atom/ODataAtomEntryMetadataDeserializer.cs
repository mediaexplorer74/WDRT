using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x0200021D RID: 541
	internal sealed class ODataAtomEntryMetadataDeserializer : ODataAtomEpmDeserializer
	{
		// Token: 0x060010DC RID: 4316 RVA: 0x0003EE90 File Offset: 0x0003D090
		internal ODataAtomEntryMetadataDeserializer(ODataAtomInputContext atomInputContext)
			: base(atomInputContext)
		{
			XmlNameTable nameTable = base.XmlReader.NameTable;
			this.EmptyNamespace = nameTable.Add(string.Empty);
			this.AtomNamespace = nameTable.Add("http://www.w3.org/2005/Atom");
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x060010DD RID: 4317 RVA: 0x0003EED4 File Offset: 0x0003D0D4
		private ODataAtomFeedMetadataDeserializer SourceMetadataDeserializer
		{
			get
			{
				ODataAtomFeedMetadataDeserializer odataAtomFeedMetadataDeserializer;
				if ((odataAtomFeedMetadataDeserializer = this.sourceMetadataDeserializer) == null)
				{
					odataAtomFeedMetadataDeserializer = (this.sourceMetadataDeserializer = new ODataAtomFeedMetadataDeserializer(base.AtomInputContext, true));
				}
				return odataAtomFeedMetadataDeserializer;
			}
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x0003EF00 File Offset: 0x0003D100
		internal void ReadAtomElementInEntryContent(IODataAtomReaderEntryState entryState)
		{
			ODataEntityPropertyMappingCache cachedEpm = entryState.CachedEpm;
			EpmTargetPathSegment epmTargetPathSegment = null;
			if (cachedEpm != null)
			{
				epmTargetPathSegment = cachedEpm.EpmTargetTree.SyndicationRoot;
			}
			EpmTargetPathSegment epmTargetPathSegment2;
			string localName;
			if (base.ShouldReadElement(epmTargetPathSegment, base.XmlReader.LocalName, out epmTargetPathSegment2) && (localName = base.XmlReader.LocalName) != null)
			{
				if (<PrivateImplementationDetails>{E8251738-41A8-451C-AA83-3F2B33F0CD5E}.$$method0x600104b-1 == null)
				{
					<PrivateImplementationDetails>{E8251738-41A8-451C-AA83-3F2B33F0CD5E}.$$method0x600104b-1 = new Dictionary<string, int>(8)
					{
						{ "author", 0 },
						{ "contributor", 1 },
						{ "updated", 2 },
						{ "published", 3 },
						{ "rights", 4 },
						{ "source", 5 },
						{ "summary", 6 },
						{ "title", 7 }
					};
				}
				int num;
				if (<PrivateImplementationDetails>{E8251738-41A8-451C-AA83-3F2B33F0CD5E}.$$method0x600104b-1.TryGetValue(localName, out num))
				{
					switch (num)
					{
					case 0:
						this.ReadAuthorElement(entryState, epmTargetPathSegment2);
						return;
					case 1:
						this.ReadContributorElement(entryState, epmTargetPathSegment2);
						return;
					case 2:
					{
						AtomEntryMetadata atomEntryMetadata = entryState.AtomEntryMetadata;
						if (base.UseClientFormatBehavior)
						{
							if (this.ShouldReadSingletonElement(atomEntryMetadata.UpdatedString != null))
							{
								atomEntryMetadata.UpdatedString = base.ReadAtomDateConstructAsString();
								return;
							}
						}
						else if (this.ShouldReadSingletonElement(atomEntryMetadata.Updated != null))
						{
							atomEntryMetadata.Updated = base.ReadAtomDateConstruct();
							return;
						}
						break;
					}
					case 3:
					{
						AtomEntryMetadata atomEntryMetadata2 = entryState.AtomEntryMetadata;
						if (base.UseClientFormatBehavior)
						{
							if (this.ShouldReadSingletonElement(atomEntryMetadata2.PublishedString != null))
							{
								atomEntryMetadata2.PublishedString = base.ReadAtomDateConstructAsString();
								return;
							}
						}
						else if (this.ShouldReadSingletonElement(atomEntryMetadata2.Published != null))
						{
							atomEntryMetadata2.Published = base.ReadAtomDateConstruct();
							return;
						}
						break;
					}
					case 4:
						if (this.ShouldReadSingletonElement(entryState.AtomEntryMetadata.Rights != null))
						{
							entryState.AtomEntryMetadata.Rights = base.ReadAtomTextConstruct();
							return;
						}
						break;
					case 5:
						if (this.ShouldReadSingletonElement(entryState.AtomEntryMetadata.Source != null))
						{
							entryState.AtomEntryMetadata.Source = this.ReadAtomSourceInEntryContent();
							return;
						}
						break;
					case 6:
						if (this.ShouldReadSingletonElement(entryState.AtomEntryMetadata.Summary != null))
						{
							entryState.AtomEntryMetadata.Summary = base.ReadAtomTextConstruct();
							return;
						}
						break;
					case 7:
						if (this.ShouldReadSingletonElement(entryState.AtomEntryMetadata.Title != null))
						{
							entryState.AtomEntryMetadata.Title = base.ReadAtomTextConstruct();
							return;
						}
						break;
					}
				}
			}
			base.XmlReader.Skip();
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x0003F190 File Offset: 0x0003D390
		internal AtomLinkMetadata ReadAtomLinkElementInEntryContent(string relation, string hrefStringValue)
		{
			AtomLinkMetadata atomLinkMetadata = null;
			if (base.ReadAtomMetadata)
			{
				atomLinkMetadata = new AtomLinkMetadata();
				atomLinkMetadata.Relation = relation;
				if (base.ReadAtomMetadata)
				{
					atomLinkMetadata.Href = ((hrefStringValue == null) ? null : base.ProcessUriFromPayload(hrefStringValue, base.XmlReader.XmlBaseUri));
				}
				while (base.XmlReader.MoveToNextAttribute())
				{
					string localName;
					if (base.XmlReader.NamespaceEquals(this.EmptyNamespace) && (localName = base.XmlReader.LocalName) != null)
					{
						if (!(localName == "type"))
						{
							if (!(localName == "hreflang"))
							{
								if (!(localName == "title"))
								{
									if (localName == "length")
									{
										if (base.ReadAtomMetadata)
										{
											string value = base.XmlReader.Value;
											int num;
											if (!int.TryParse(value, NumberStyles.Any, NumberFormatInfo.InvariantInfo, out num))
											{
												throw new ODataException(Strings.EpmSyndicationWriter_InvalidLinkLengthValue(value));
											}
											atomLinkMetadata.Length = new int?(num);
										}
									}
								}
								else
								{
									atomLinkMetadata.Title = base.XmlReader.Value;
								}
							}
							else
							{
								atomLinkMetadata.HrefLang = base.XmlReader.Value;
							}
						}
						else
						{
							atomLinkMetadata.MediaType = base.XmlReader.Value;
						}
					}
				}
			}
			base.XmlReader.MoveToElement();
			return atomLinkMetadata;
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x0003F2F0 File Offset: 0x0003D4F0
		internal void ReadAtomCategoryElementInEntryContent(IODataAtomReaderEntryState entryState)
		{
			ODataEntityPropertyMappingCache cachedEpm = entryState.CachedEpm;
			EpmTargetPathSegment epmTargetPathSegment = null;
			if (cachedEpm != null)
			{
				epmTargetPathSegment = cachedEpm.EpmTargetTree.SyndicationRoot;
			}
			bool flag;
			if (epmTargetPathSegment != null)
			{
				flag = epmTargetPathSegment.SubSegments.Any((EpmTargetPathSegment segment) => string.CompareOrdinal(segment.SegmentName, "category") == 0);
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			if (base.ReadAtomMetadata || flag2)
			{
				AtomCategoryMetadata atomCategoryMetadata = this.ReadAtomCategoryElement();
				entryState.AtomEntryMetadata.AddCategory(atomCategoryMetadata);
				return;
			}
			base.XmlReader.Skip();
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x0003F370 File Offset: 0x0003D570
		internal AtomCategoryMetadata ReadAtomCategoryElement()
		{
			AtomCategoryMetadata atomCategoryMetadata = new AtomCategoryMetadata();
			while (base.XmlReader.MoveToNextAttribute())
			{
				string localName2;
				if (base.XmlReader.NamespaceEquals(this.EmptyNamespace))
				{
					string localName;
					if ((localName = base.XmlReader.LocalName) != null)
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
								atomCategoryMetadata.Term = atomCategoryMetadata.Term ?? base.XmlReader.Value;
							}
						}
						else
						{
							atomCategoryMetadata.Scheme = atomCategoryMetadata.Scheme ?? base.XmlReader.Value;
						}
					}
				}
				else if (base.UseClientFormatBehavior && base.XmlReader.NamespaceEquals(this.AtomNamespace) && (localName2 = base.XmlReader.LocalName) != null)
				{
					if (!(localName2 == "scheme"))
					{
						if (localName2 == "term")
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
			base.XmlReader.Skip();
			return atomCategoryMetadata;
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x0003F4B4 File Offset: 0x0003D6B4
		internal AtomFeedMetadata ReadAtomSourceInEntryContent()
		{
			AtomFeedMetadata atomFeedMetadata = AtomMetadataReaderUtils.CreateNewAtomFeedMetadata();
			if (base.XmlReader.IsEmptyElement)
			{
				base.XmlReader.Read();
				return atomFeedMetadata;
			}
			base.XmlReader.Read();
			while (base.XmlReader.NodeType != XmlNodeType.EndElement)
			{
				if (base.XmlReader.NodeType != XmlNodeType.Element)
				{
					base.XmlReader.Skip();
				}
				else if (base.XmlReader.NamespaceEquals(this.AtomNamespace))
				{
					this.SourceMetadataDeserializer.ReadAtomElementAsFeedMetadata(atomFeedMetadata);
				}
				else
				{
					base.XmlReader.Skip();
				}
			}
			base.XmlReader.Read();
			return atomFeedMetadata;
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x0003F553 File Offset: 0x0003D753
		private void ReadAuthorElement(IODataAtomReaderEntryState entryState, EpmTargetPathSegment epmTargetPathSegment)
		{
			if (this.ShouldReadCollectionElement(entryState.AtomEntryMetadata.Authors.Any<AtomPersonMetadata>()))
			{
				entryState.AtomEntryMetadata.AddAuthor(base.ReadAtomPersonConstruct(epmTargetPathSegment));
				return;
			}
			base.XmlReader.Skip();
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x0003F58B File Offset: 0x0003D78B
		private void ReadContributorElement(IODataAtomReaderEntryState entryState, EpmTargetPathSegment epmTargetPathSegment)
		{
			if (this.ShouldReadCollectionElement(entryState.AtomEntryMetadata.Contributors.Any<AtomPersonMetadata>()))
			{
				entryState.AtomEntryMetadata.AddContributor(base.ReadAtomPersonConstruct(epmTargetPathSegment));
				return;
			}
			base.XmlReader.Skip();
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x0003F5C3 File Offset: 0x0003D7C3
		private bool ShouldReadCollectionElement(bool someAlreadyExist)
		{
			return base.ReadAtomMetadata || !someAlreadyExist;
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x0003F5D3 File Offset: 0x0003D7D3
		private bool ShouldReadSingletonElement(bool alreadyExists)
		{
			if (!alreadyExists)
			{
				return true;
			}
			if (base.ReadAtomMetadata || base.AtomInputContext.UseDefaultFormatBehavior)
			{
				throw new ODataException(Strings.ODataAtomMetadataDeserializer_MultipleSingletonMetadataElements(base.XmlReader.LocalName, "entry"));
			}
			return false;
		}

		// Token: 0x04000627 RID: 1575
		private readonly string EmptyNamespace;

		// Token: 0x04000628 RID: 1576
		private readonly string AtomNamespace;

		// Token: 0x04000629 RID: 1577
		private ODataAtomFeedMetadataDeserializer sourceMetadataDeserializer;
	}
}
