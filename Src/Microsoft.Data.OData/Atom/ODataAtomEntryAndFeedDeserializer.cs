using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x0200021C RID: 540
	internal sealed class ODataAtomEntryAndFeedDeserializer : ODataAtomPropertyAndValueDeserializer
	{
		// Token: 0x060010B6 RID: 4278 RVA: 0x0003D138 File Offset: 0x0003B338
		internal ODataAtomEntryAndFeedDeserializer(ODataAtomInputContext atomInputContext)
			: base(atomInputContext)
		{
			XmlNameTable nameTable = base.XmlReader.NameTable;
			this.AtomNamespace = nameTable.Add("http://www.w3.org/2005/Atom");
			this.AtomEntryElementName = nameTable.Add("entry");
			this.AtomCategoryElementName = nameTable.Add("category");
			this.AtomCategoryTermAttributeName = nameTable.Add("term");
			this.AtomCategorySchemeAttributeName = nameTable.Add("scheme");
			this.AtomContentElementName = nameTable.Add("content");
			this.AtomLinkElementName = nameTable.Add("link");
			this.AtomPropertiesElementName = nameTable.Add("properties");
			this.AtomFeedElementName = nameTable.Add("feed");
			this.AtomIdElementName = nameTable.Add("id");
			this.AtomLinkRelationAttributeName = nameTable.Add("rel");
			this.AtomLinkHrefAttributeName = nameTable.Add("href");
			this.MediaLinkEntryContentSourceAttributeName = nameTable.Add("src");
			this.ODataETagAttributeName = nameTable.Add("etag");
			this.ODataCountElementName = nameTable.Add("count");
			this.ODataInlineElementName = nameTable.Add("inline");
			this.ODataActionElementName = nameTable.Add("action");
			this.ODataFunctionElementName = nameTable.Add("function");
			this.ODataOperationMetadataAttribute = nameTable.Add("metadata");
			this.ODataOperationTitleAttribute = nameTable.Add("title");
			this.ODataOperationTargetAttribute = nameTable.Add("target");
			this.atomAnnotationReader = new ODataAtomAnnotationReader(base.AtomInputContext, this);
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x060010B7 RID: 4279 RVA: 0x0003D2D0 File Offset: 0x0003B4D0
		private ODataAtomEntryMetadataDeserializer EntryMetadataDeserializer
		{
			get
			{
				ODataAtomEntryMetadataDeserializer odataAtomEntryMetadataDeserializer;
				if ((odataAtomEntryMetadataDeserializer = this.entryMetadataDeserializer) == null)
				{
					odataAtomEntryMetadataDeserializer = (this.entryMetadataDeserializer = new ODataAtomEntryMetadataDeserializer(base.AtomInputContext));
				}
				return odataAtomEntryMetadataDeserializer;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x060010B8 RID: 4280 RVA: 0x0003D2FC File Offset: 0x0003B4FC
		private ODataAtomFeedMetadataDeserializer FeedMetadataDeserializer
		{
			get
			{
				ODataAtomFeedMetadataDeserializer odataAtomFeedMetadataDeserializer;
				if ((odataAtomFeedMetadataDeserializer = this.feedMetadataDeserializer) == null)
				{
					odataAtomFeedMetadataDeserializer = (this.feedMetadataDeserializer = new ODataAtomFeedMetadataDeserializer(base.AtomInputContext, false));
				}
				return odataAtomFeedMetadataDeserializer;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x060010B9 RID: 4281 RVA: 0x0003D328 File Offset: 0x0003B528
		private bool ReadAtomMetadata
		{
			get
			{
				return base.AtomInputContext.MessageReaderSettings.EnableAtomMetadataReading;
			}
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x0003D33C File Offset: 0x0003B53C
		internal static void EnsureMediaResource(IODataAtomReaderEntryState entryState, bool validateMLEPresence)
		{
			if (validateMLEPresence)
			{
				entryState.MediaLinkEntry = new bool?(true);
			}
			ODataEntry entry = entryState.Entry;
			if (entry.MediaResource == null)
			{
				entry.MediaResource = new ODataStreamReferenceValue();
			}
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x0003D374 File Offset: 0x0003B574
		internal void VerifyEntryStart()
		{
			if (base.XmlReader.NodeType != XmlNodeType.Element)
			{
				throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_ElementExpected(base.XmlReader.NodeType));
			}
			if (!base.XmlReader.NamespaceEquals(this.AtomNamespace) || !base.XmlReader.LocalNameEquals(this.AtomEntryElementName))
			{
				throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_EntryElementWrongName(base.XmlReader.LocalName, base.XmlReader.NamespaceURI));
			}
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x0003D3F4 File Offset: 0x0003B5F4
		internal void ReadEntryStart(ODataEntry entry)
		{
			this.VerifyEntryStart();
			while (base.XmlReader.MoveToNextAttribute())
			{
				if (base.XmlReader.NamespaceEquals(base.XmlReader.ODataMetadataNamespace) && base.XmlReader.LocalNameEquals(this.ODataETagAttributeName))
				{
					entry.ETag = base.XmlReader.Value;
					break;
				}
			}
			base.XmlReader.MoveToElement();
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x0003D460 File Offset: 0x0003B660
		internal ODataAtomReaderNavigationLinkDescriptor ReadEntryContent(IODataAtomReaderEntryState entryState)
		{
			ODataAtomReaderNavigationLinkDescriptor odataAtomReaderNavigationLinkDescriptor = null;
			while (base.XmlReader.NodeType != XmlNodeType.EndElement)
			{
				if (base.XmlReader.NodeType != XmlNodeType.Element)
				{
					base.XmlReader.Skip();
				}
				else if (base.XmlReader.NamespaceEquals(this.AtomNamespace))
				{
					odataAtomReaderNavigationLinkDescriptor = this.ReadAtomElementInEntry(entryState);
					if (odataAtomReaderNavigationLinkDescriptor != null)
					{
						entryState.DuplicatePropertyNamesChecker.CheckForDuplicatePropertyNamesOnNavigationLinkStart(odataAtomReaderNavigationLinkDescriptor.NavigationLink);
						break;
					}
				}
				else if (base.XmlReader.NamespaceEquals(base.XmlReader.ODataMetadataNamespace))
				{
					if (base.XmlReader.LocalNameEquals(this.AtomPropertiesElementName))
					{
						this.ValidateDuplicateElement(entryState.HasProperties && base.AtomInputContext.UseDefaultFormatBehavior);
						ODataAtomEntryAndFeedDeserializer.EnsureMediaResource(entryState, true);
						base.ReadProperties(entryState.EntityType, entryState.Entry.Properties.ToReadOnlyEnumerable("Properties"), entryState.DuplicatePropertyNamesChecker, entryState.CachedEpm != null);
						base.XmlReader.Read();
						entryState.HasProperties = true;
					}
					else if (base.MessageReaderSettings.MaxProtocolVersion < ODataVersion.V3 || !base.ReadingResponse || !this.TryReadOperation(entryState))
					{
						AtomInstanceAnnotation atomInstanceAnnotation;
						if (this.atomAnnotationReader.TryReadAnnotation(out atomInstanceAnnotation))
						{
							if (!atomInstanceAnnotation.IsTargetingCurrentElement)
							{
								throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_AnnotationWithNonDotTarget(atomInstanceAnnotation.Target, atomInstanceAnnotation.TermName));
							}
							entryState.Entry.InstanceAnnotations.Add(new ODataInstanceAnnotation(atomInstanceAnnotation.TermName, atomInstanceAnnotation.Value));
						}
						else if (!this.EntryMetadataDeserializer.TryReadExtensionElementInEntryContent(entryState))
						{
							base.XmlReader.Skip();
						}
					}
				}
				else if (!this.EntryMetadataDeserializer.TryReadExtensionElementInEntryContent(entryState))
				{
					base.XmlReader.Skip();
				}
			}
			return odataAtomReaderNavigationLinkDescriptor;
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x0003D61F File Offset: 0x0003B81F
		internal void ReadEntryEnd()
		{
			base.XmlReader.Read();
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x0003D630 File Offset: 0x0003B830
		internal void ReadFeedStart()
		{
			if (!base.XmlReader.NamespaceEquals(this.AtomNamespace) || !base.XmlReader.LocalNameEquals(this.AtomFeedElementName))
			{
				throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_FeedElementWrongName(base.XmlReader.LocalName, base.XmlReader.NamespaceURI));
			}
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x0003D684 File Offset: 0x0003B884
		internal bool ReadFeedContent(IODataAtomReaderFeedState feedState, bool isExpandedLinkContent)
		{
			bool flag = false;
			while (base.XmlReader.NodeType != XmlNodeType.EndElement)
			{
				if (base.XmlReader.NodeType != XmlNodeType.Element)
				{
					base.XmlReader.Skip();
				}
				else if (base.XmlReader.NamespaceEquals(this.AtomNamespace))
				{
					if (this.ReadAtomElementInFeed(feedState, isExpandedLinkContent))
					{
						flag = true;
						break;
					}
				}
				else if (base.XmlReader.NamespaceEquals(base.XmlReader.ODataMetadataNamespace))
				{
					AtomInstanceAnnotation atomInstanceAnnotation;
					if (base.ReadingResponse && base.Version >= ODataVersion.V2 && !isExpandedLinkContent && base.XmlReader.LocalNameEquals(this.ODataCountElementName))
					{
						this.ValidateDuplicateElement(feedState.HasCount);
						long num = (long)AtomValueUtils.ReadPrimitiveValue(base.XmlReader, EdmCoreModel.Instance.GetInt64(true));
						feedState.Feed.Count = new long?(num);
						base.XmlReader.Read();
						feedState.HasCount = true;
					}
					else if (this.atomAnnotationReader.TryReadAnnotation(out atomInstanceAnnotation))
					{
						if (isExpandedLinkContent)
						{
							throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_EncounteredAnnotationInNestedFeed);
						}
						if (!atomInstanceAnnotation.IsTargetingCurrentElement)
						{
							throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_AnnotationWithNonDotTarget(atomInstanceAnnotation.Target, atomInstanceAnnotation.TermName));
						}
						feedState.Feed.InstanceAnnotations.Add(new ODataInstanceAnnotation(atomInstanceAnnotation.TermName, atomInstanceAnnotation.Value));
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
			}
			return flag;
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x0003D7F7 File Offset: 0x0003B9F7
		internal void ReadFeedEnd()
		{
			base.XmlReader.Read();
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x0003D805 File Offset: 0x0003BA05
		internal ODataAtomDeserializerExpandedNavigationLinkContent ReadNavigationLinkContentBeforeExpansion()
		{
			if (!this.ReadNavigationLinkContent())
			{
				return ODataAtomDeserializerExpandedNavigationLinkContent.None;
			}
			if (base.XmlReader.IsEmptyElement)
			{
				return ODataAtomDeserializerExpandedNavigationLinkContent.Empty;
			}
			base.XmlReader.Read();
			return this.ReadInlineElementContent();
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x0003D834 File Offset: 0x0003BA34
		internal bool IsReaderOnInlineEndElement()
		{
			return base.XmlReader.LocalNameEquals(this.ODataInlineElementName) && base.XmlReader.NamespaceEquals(base.XmlReader.ODataMetadataNamespace) && ((base.XmlReader.NodeType == XmlNodeType.Element && base.XmlReader.IsEmptyElement) || base.XmlReader.NodeType == XmlNodeType.EndElement);
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x0003D89C File Offset: 0x0003BA9C
		internal void SkipNavigationLinkContentOnExpansion()
		{
			do
			{
				base.XmlReader.Skip();
			}
			while (base.XmlReader.NodeType != XmlNodeType.EndElement || !base.XmlReader.LocalNameEquals(this.AtomLinkElementName) || !base.XmlReader.NamespaceEquals(this.AtomNamespace));
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x0003D8EC File Offset: 0x0003BAEC
		internal void ReadNavigationLinkContentAfterExpansion(bool emptyInline)
		{
			if (!emptyInline)
			{
				ODataAtomDeserializerExpandedNavigationLinkContent odataAtomDeserializerExpandedNavigationLinkContent = this.ReadInlineElementContent();
				if (odataAtomDeserializerExpandedNavigationLinkContent != ODataAtomDeserializerExpandedNavigationLinkContent.Empty)
				{
					throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_MultipleExpansionsInInline(odataAtomDeserializerExpandedNavigationLinkContent.ToString()));
				}
			}
			base.XmlReader.Read();
			if (this.ReadNavigationLinkContent())
			{
				throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_MultipleInlineElementsInLink);
			}
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x0003D93C File Offset: 0x0003BB3C
		internal void ReadNavigationLinkEnd()
		{
			base.XmlReader.Read();
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x0003D94C File Offset: 0x0003BB4C
		internal string FindTypeName()
		{
			base.XmlReader.MoveToElement();
			base.XmlReader.StartBuffering();
			try
			{
				if (!base.XmlReader.IsEmptyElement)
				{
					base.XmlReader.Read();
					string text;
					for (;;)
					{
						XmlNodeType nodeType = base.XmlReader.NodeType;
						if (nodeType != XmlNodeType.Element)
						{
							if (nodeType == XmlNodeType.EndElement)
							{
								goto IL_13D;
							}
							base.XmlReader.Skip();
						}
						else
						{
							if (base.XmlReader.NamespaceEquals(this.AtomNamespace) && base.XmlReader.LocalNameEquals(this.AtomCategoryElementName))
							{
								text = null;
								bool flag = false;
								while (base.XmlReader.MoveToNextAttribute())
								{
									bool flag2 = base.XmlReader.NamespaceEquals(this.EmptyNamespace);
									if (flag2 || (base.UseClientFormatBehavior && base.XmlReader.NamespaceEquals(this.AtomNamespace)))
									{
										if (base.XmlReader.LocalNameEquals(this.AtomCategorySchemeAttributeName))
										{
											if (string.CompareOrdinal(base.XmlReader.Value, base.MessageReaderSettings.ReaderBehavior.ODataTypeScheme) == 0)
											{
												flag = true;
											}
										}
										else if (base.XmlReader.LocalNameEquals(this.AtomCategoryTermAttributeName) && (text == null || !flag2))
										{
											text = base.XmlReader.Value;
										}
									}
								}
								if (flag)
								{
									break;
								}
							}
							base.XmlReader.Skip();
						}
					}
					return text;
					IL_13D:
					return null;
				}
			}
			finally
			{
				base.XmlReader.StopBuffering();
			}
			return null;
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x0003DAF8 File Offset: 0x0003BCF8
		private ODataStreamReferenceValue GetNewOrExistingStreamPropertyValue(IODataAtomReaderEntryState entryState, string streamPropertyName)
		{
			ReadOnlyEnumerable<ODataProperty> readOnlyEnumerable = entryState.Entry.Properties.ToReadOnlyEnumerable("Properties");
			ODataProperty odataProperty = readOnlyEnumerable.FirstOrDefault((ODataProperty p) => string.CompareOrdinal(p.Name, streamPropertyName) == 0);
			ODataStreamReferenceValue odataStreamReferenceValue;
			if (odataProperty == null)
			{
				IEdmProperty edmProperty = ReaderValidationUtils.ValidateLinkPropertyDefined(streamPropertyName, entryState.EntityType, base.MessageReaderSettings);
				odataStreamReferenceValue = new ODataStreamReferenceValue();
				odataProperty = new ODataProperty
				{
					Name = streamPropertyName,
					Value = odataStreamReferenceValue
				};
				ReaderValidationUtils.ValidateStreamReferenceProperty(odataProperty, entryState.EntityType, edmProperty, base.MessageReaderSettings);
				entryState.DuplicatePropertyNamesChecker.CheckForDuplicatePropertyNames(odataProperty);
				readOnlyEnumerable.AddToSourceList(odataProperty);
			}
			else
			{
				odataStreamReferenceValue = odataProperty.Value as ODataStreamReferenceValue;
				if (odataStreamReferenceValue == null)
				{
					throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_StreamPropertyDuplicatePropertyName(streamPropertyName));
				}
			}
			return odataStreamReferenceValue;
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x0003DBCA File Offset: 0x0003BDCA
		private void ValidateDuplicateElement(bool duplicateElementFound)
		{
			if (duplicateElementFound)
			{
				throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_DuplicateElements(base.XmlReader.NamespaceURI, base.XmlReader.LocalName));
			}
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x0003DBF0 File Offset: 0x0003BDF0
		private ODataAtomReaderNavigationLinkDescriptor ReadAtomElementInEntry(IODataAtomReaderEntryState entryState)
		{
			if (base.XmlReader.LocalNameEquals(this.AtomContentElementName))
			{
				this.ReadAtomContentElement(entryState);
			}
			else if (base.XmlReader.LocalNameEquals(this.AtomIdElementName))
			{
				this.ReadAtomIdElementInEntry(entryState);
			}
			else if (base.XmlReader.LocalNameEquals(this.AtomCategoryElementName))
			{
				string attribute = base.XmlReader.GetAttribute(this.AtomCategorySchemeAttributeName, this.EmptyNamespace);
				if (attribute != null && string.CompareOrdinal(attribute, base.MessageReaderSettings.ReaderBehavior.ODataTypeScheme) == 0)
				{
					this.ValidateDuplicateElement(entryState.HasTypeNameCategory && base.AtomInputContext.UseDefaultFormatBehavior);
					if (this.ReadAtomMetadata)
					{
						entryState.AtomEntryMetadata.CategoryWithTypeName = this.EntryMetadataDeserializer.ReadAtomCategoryElement();
					}
					else
					{
						base.XmlReader.Skip();
					}
					entryState.HasTypeNameCategory = true;
				}
				else if (entryState.CachedEpm != null || this.ReadAtomMetadata)
				{
					this.EntryMetadataDeserializer.ReadAtomCategoryElementInEntryContent(entryState);
				}
				else
				{
					base.XmlReader.Skip();
				}
			}
			else
			{
				if (base.XmlReader.LocalNameEquals(this.AtomLinkElementName))
				{
					return this.ReadAtomLinkElementInEntry(entryState);
				}
				if (entryState.CachedEpm != null || this.ReadAtomMetadata)
				{
					this.EntryMetadataDeserializer.ReadAtomElementInEntryContent(entryState);
				}
				else
				{
					base.XmlReader.Skip();
				}
			}
			return null;
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x0003DD44 File Offset: 0x0003BF44
		private void ReadAtomContentElement(IODataAtomReaderEntryState entryState)
		{
			this.ValidateDuplicateElement(entryState.HasContent && base.AtomInputContext.UseDefaultFormatBehavior);
			if (base.AtomInputContext.UseClientFormatBehavior)
			{
				entryState.HasProperties = false;
			}
			string text;
			string text2;
			this.ReadAtomContentAttributes(out text, out text2);
			if (text2 != null)
			{
				ODataEntry entry = entryState.Entry;
				ODataAtomEntryAndFeedDeserializer.EnsureMediaResource(entryState, true);
				if (!base.AtomInputContext.UseServerFormatBehavior)
				{
					entry.MediaResource.ReadLink = base.ProcessUriFromPayload(text2, base.XmlReader.XmlBaseUri);
				}
				entry.MediaResource.ContentType = text;
				if (!base.XmlReader.TryReadEmptyElement())
				{
					throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_ContentWithSourceLinkIsNotEmpty);
				}
			}
			else
			{
				bool flag = string.IsNullOrEmpty(text);
				if (flag && base.AtomInputContext.UseClientFormatBehavior)
				{
					base.XmlReader.SkipElementContent();
				}
				string text3 = text;
				if (!flag)
				{
					text3 = this.VerifyAtomContentMediaType(text);
				}
				entryState.MediaLinkEntry = new bool?(false);
				base.XmlReader.MoveToElement();
				if (!base.XmlReader.IsEmptyElement && base.XmlReader.NodeType != XmlNodeType.EndElement)
				{
					if (string.IsNullOrEmpty(text3))
					{
						base.XmlReader.ReadElementContentValue();
					}
					else
					{
						base.XmlReader.ReadStartElement();
						while (base.XmlReader.NodeType != XmlNodeType.EndElement)
						{
							XmlNodeType nodeType = base.XmlReader.NodeType;
							if (nodeType != XmlNodeType.Element)
							{
								if (nodeType != XmlNodeType.EndElement)
								{
									base.XmlReader.Skip();
								}
							}
							else
							{
								if (base.XmlReader.NamespaceEquals(base.XmlReader.ODataMetadataNamespace))
								{
									if (!base.XmlReader.LocalNameEquals(this.AtomPropertiesElementName))
									{
										throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_ContentWithInvalidNode(base.XmlReader.LocalName));
									}
									this.ValidateDuplicateElement(entryState.HasProperties && base.AtomInputContext.UseDefaultFormatBehavior);
									if (base.UseClientFormatBehavior && entryState.HasProperties)
									{
										base.XmlReader.SkipElementContent();
									}
									else
									{
										base.ReadProperties(entryState.EntityType, entryState.Entry.Properties.ToReadOnlyEnumerable("Properties"), entryState.DuplicatePropertyNamesChecker, entryState.CachedEpm != null);
									}
									entryState.HasProperties = true;
								}
								else
								{
									base.XmlReader.SkipElementContent();
								}
								base.XmlReader.Read();
							}
						}
					}
				}
			}
			base.XmlReader.Read();
			entryState.HasContent = true;
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x0003DFA4 File Offset: 0x0003C1A4
		private void ReadAtomContentAttributes(out string contentType, out string contentSource)
		{
			contentType = null;
			contentSource = null;
			while (base.XmlReader.MoveToNextAttribute())
			{
				bool flag = base.XmlReader.NamespaceEquals(this.EmptyNamespace);
				if (flag || (base.UseClientFormatBehavior && base.XmlReader.NamespaceEquals(this.AtomNamespace)))
				{
					if (base.XmlReader.LocalNameEquals(this.AtomTypeAttributeName))
					{
						if (!flag || contentType == null)
						{
							contentType = base.XmlReader.Value;
						}
					}
					else if (base.XmlReader.LocalNameEquals(this.MediaLinkEntryContentSourceAttributeName) && (!flag || contentSource == null))
					{
						contentSource = base.XmlReader.Value;
					}
				}
			}
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x0003E04C File Offset: 0x0003C24C
		private void ReadAtomIdElementInEntry(IODataAtomReaderEntryState entryState)
		{
			this.ValidateDuplicateElement(entryState.HasId && base.AtomInputContext.UseDefaultFormatBehavior);
			string text = base.XmlReader.ReadElementValue();
			if (!base.AtomInputContext.UseClientFormatBehavior || !entryState.HasId)
			{
				entryState.Entry.Id = ((text != null && text.Length == 0) ? null : text);
			}
			entryState.HasId = true;
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x0003E0B8 File Offset: 0x0003C2B8
		private ODataAtomReaderNavigationLinkDescriptor ReadAtomLinkElementInEntry(IODataAtomReaderEntryState entryState)
		{
			string text;
			string text2;
			this.ReadAtomLinkRelationAndHRef(out text, out text2);
			if (text != null)
			{
				bool flag = false;
				if (!base.AtomInputContext.UseServerFormatBehavior && this.TryReadAtomStandardRelationLinkInEntry(entryState, text, text2))
				{
					return null;
				}
				string text3 = AtomUtils.UnescapeAtomLinkRelationAttribute(text);
				if (text3 != null)
				{
					if (!base.AtomInputContext.UseServerFormatBehavior)
					{
						string nameFromAtomLinkRelationAttribute = AtomUtils.GetNameFromAtomLinkRelationAttribute(text3, "http://www.iana.org/assignments/relation/");
						if (nameFromAtomLinkRelationAttribute != null && this.TryReadAtomStandardRelationLinkInEntry(entryState, nameFromAtomLinkRelationAttribute, text2))
						{
							return null;
						}
					}
					ODataAtomReaderNavigationLinkDescriptor odataAtomReaderNavigationLinkDescriptor = this.TryReadNavigationLinkInEntry(entryState, text3, text2);
					if (odataAtomReaderNavigationLinkDescriptor != null)
					{
						return odataAtomReaderNavigationLinkDescriptor;
					}
					if (this.TryReadStreamPropertyLinkInEntry(entryState, text3, text2, out flag))
					{
						return null;
					}
					if (!flag && this.TryReadAssociationLinkInEntry(entryState, text3, text2))
					{
						return null;
					}
				}
			}
			if (entryState.CachedEpm != null || this.ReadAtomMetadata)
			{
				AtomLinkMetadata atomLinkMetadata = this.EntryMetadataDeserializer.ReadAtomLinkElementInEntryContent(text, text2);
				if (atomLinkMetadata != null)
				{
					entryState.AtomEntryMetadata.AddLink(atomLinkMetadata);
				}
			}
			base.XmlReader.Skip();
			return null;
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x0003E194 File Offset: 0x0003C394
		private bool TryReadAtomStandardRelationLinkInEntry(IODataAtomReaderEntryState entryState, string linkRelation, string linkHRef)
		{
			if (string.CompareOrdinal(linkRelation, "edit") == 0)
			{
				if (entryState.HasEditLink && base.AtomInputContext.UseDefaultFormatBehavior)
				{
					throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_MultipleLinksInEntry("edit"));
				}
				if (linkHRef != null && (!base.AtomInputContext.UseClientFormatBehavior || !entryState.HasEditLink))
				{
					entryState.Entry.EditLink = base.ProcessUriFromPayload(linkHRef, base.XmlReader.XmlBaseUri);
				}
				if (this.ReadAtomMetadata)
				{
					entryState.AtomEntryMetadata.EditLink = this.EntryMetadataDeserializer.ReadAtomLinkElementInEntryContent(linkRelation, linkHRef);
				}
				entryState.HasEditLink = true;
				base.XmlReader.Skip();
				return true;
			}
			else if (string.CompareOrdinal(linkRelation, "self") == 0)
			{
				if (entryState.HasReadLink && base.AtomInputContext.UseDefaultFormatBehavior)
				{
					throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_MultipleLinksInEntry("self"));
				}
				if (linkHRef != null && (!base.AtomInputContext.UseClientFormatBehavior || !entryState.HasReadLink))
				{
					entryState.Entry.ReadLink = base.ProcessUriFromPayload(linkHRef, base.XmlReader.XmlBaseUri);
				}
				if (this.ReadAtomMetadata)
				{
					entryState.AtomEntryMetadata.SelfLink = this.EntryMetadataDeserializer.ReadAtomLinkElementInEntryContent(linkRelation, linkHRef);
				}
				entryState.HasReadLink = true;
				base.XmlReader.Skip();
				return true;
			}
			else
			{
				if (string.CompareOrdinal(linkRelation, "edit-media") != 0)
				{
					return false;
				}
				if (entryState.HasEditMediaLink && base.AtomInputContext.UseDefaultFormatBehavior)
				{
					throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_MultipleLinksInEntry("edit-media"));
				}
				if (!base.AtomInputContext.UseClientFormatBehavior || !entryState.HasEditMediaLink)
				{
					ODataAtomEntryAndFeedDeserializer.EnsureMediaResource(entryState, !base.UseClientFormatBehavior);
					ODataEntry entry = entryState.Entry;
					if (linkHRef != null)
					{
						entry.MediaResource.EditLink = base.ProcessUriFromPayload(linkHRef, base.XmlReader.XmlBaseUri);
					}
					string attribute = base.XmlReader.GetAttribute(this.ODataETagAttributeName, base.XmlReader.ODataMetadataNamespace);
					if (attribute != null)
					{
						entry.MediaResource.ETag = attribute;
					}
					if (this.ReadAtomMetadata)
					{
						AtomLinkMetadata atomLinkMetadata = this.EntryMetadataDeserializer.ReadAtomLinkElementInEntryContent(linkRelation, linkHRef);
						entry.MediaResource.SetAnnotation<AtomStreamReferenceMetadata>(new AtomStreamReferenceMetadata
						{
							EditLink = atomLinkMetadata
						});
					}
				}
				entryState.HasEditMediaLink = true;
				base.XmlReader.Skip();
				return true;
			}
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x0003E3D0 File Offset: 0x0003C5D0
		private ODataAtomReaderNavigationLinkDescriptor TryReadNavigationLinkInEntry(IODataAtomReaderEntryState entryState, string linkRelation, string linkHRef)
		{
			string nameFromAtomLinkRelationAttribute = AtomUtils.GetNameFromAtomLinkRelationAttribute(linkRelation, "http://schemas.microsoft.com/ado/2007/08/dataservices/related/");
			if (string.IsNullOrEmpty(nameFromAtomLinkRelationAttribute))
			{
				return null;
			}
			IEdmNavigationProperty edmNavigationProperty = ReaderValidationUtils.ValidateNavigationPropertyDefined(nameFromAtomLinkRelationAttribute, entryState.EntityType, base.MessageReaderSettings);
			ODataNavigationLink odataNavigationLink = new ODataNavigationLink
			{
				Name = nameFromAtomLinkRelationAttribute
			};
			string attribute = base.XmlReader.GetAttribute(this.AtomTypeAttributeName, this.EmptyNamespace);
			if (!string.IsNullOrEmpty(attribute))
			{
				bool flag;
				bool flag2;
				if (!AtomUtils.IsExactNavigationLinkTypeMatch(attribute, out flag, out flag2))
				{
					string text;
					string text2;
					IList<KeyValuePair<string, string>> list = HttpUtils.ReadMimeType(attribute, out text, out text2);
					if (!HttpUtils.CompareMediaTypeNames(text, "application/atom+xml"))
					{
						return null;
					}
					string text3 = null;
					if (list != null)
					{
						for (int i = 0; i < list.Count; i++)
						{
							KeyValuePair<string, string> keyValuePair = list[i];
							if (HttpUtils.CompareMediaTypeParameterNames("type", keyValuePair.Key))
							{
								text3 = keyValuePair.Value;
								break;
							}
						}
					}
					if (text3 != null)
					{
						if (string.Compare(text3, "entry", StringComparison.OrdinalIgnoreCase) == 0)
						{
							flag = true;
						}
						else if (string.Compare(text3, "feed", StringComparison.OrdinalIgnoreCase) == 0)
						{
							flag2 = true;
						}
					}
				}
				if (flag)
				{
					if (!base.UseClientFormatBehavior)
					{
						odataNavigationLink.IsCollection = new bool?(false);
					}
				}
				else if (flag2)
				{
					odataNavigationLink.IsCollection = new bool?(true);
				}
			}
			if (linkHRef != null)
			{
				odataNavigationLink.Url = base.ProcessUriFromPayload(linkHRef, base.XmlReader.XmlBaseUri);
			}
			base.XmlReader.MoveToElement();
			AtomLinkMetadata atomLinkMetadata = this.EntryMetadataDeserializer.ReadAtomLinkElementInEntryContent(linkRelation, linkHRef);
			if (atomLinkMetadata != null)
			{
				odataNavigationLink.SetAnnotation<AtomLinkMetadata>(atomLinkMetadata);
			}
			return new ODataAtomReaderNavigationLinkDescriptor(odataNavigationLink, edmNavigationProperty);
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x0003E550 File Offset: 0x0003C750
		private bool TryReadStreamPropertyLinkInEntry(IODataAtomReaderEntryState entryState, string linkRelation, string linkHRef, out bool isStreamPropertyLink)
		{
			string text = AtomUtils.GetNameFromAtomLinkRelationAttribute(linkRelation, "http://schemas.microsoft.com/ado/2007/08/dataservices/edit-media/");
			if (text != null)
			{
				isStreamPropertyLink = true;
				return this.ReadStreamPropertyLinkInEntry(entryState, text, linkRelation, linkHRef, true);
			}
			text = AtomUtils.GetNameFromAtomLinkRelationAttribute(linkRelation, "http://schemas.microsoft.com/ado/2007/08/dataservices/mediaresource/");
			if (text != null)
			{
				isStreamPropertyLink = true;
				return this.ReadStreamPropertyLinkInEntry(entryState, text, linkRelation, linkHRef, false);
			}
			isStreamPropertyLink = false;
			return false;
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x0003E5A0 File Offset: 0x0003C7A0
		private bool ReadStreamPropertyLinkInEntry(IODataAtomReaderEntryState entryState, string streamPropertyName, string linkRelation, string linkHRef, bool editLink)
		{
			if (!base.ReadingResponse || base.Version < ODataVersion.V3)
			{
				return false;
			}
			if (streamPropertyName.Length == 0)
			{
				throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_StreamPropertyWithEmptyName);
			}
			ODataStreamReferenceValue newOrExistingStreamPropertyValue = this.GetNewOrExistingStreamPropertyValue(entryState, streamPropertyName);
			AtomStreamReferenceMetadata atomStreamReferenceMetadata = null;
			if (this.ReadAtomMetadata)
			{
				atomStreamReferenceMetadata = newOrExistingStreamPropertyValue.GetAnnotation<AtomStreamReferenceMetadata>();
				if (atomStreamReferenceMetadata == null)
				{
					atomStreamReferenceMetadata = new AtomStreamReferenceMetadata();
					newOrExistingStreamPropertyValue.SetAnnotation<AtomStreamReferenceMetadata>(atomStreamReferenceMetadata);
				}
			}
			Uri uri = ((linkHRef == null) ? null : base.ProcessUriFromPayload(linkHRef, base.XmlReader.XmlBaseUri));
			if (editLink)
			{
				if (newOrExistingStreamPropertyValue.EditLink != null)
				{
					throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_StreamPropertyWithMultipleEditLinks(streamPropertyName));
				}
				newOrExistingStreamPropertyValue.EditLink = uri;
				if (this.ReadAtomMetadata)
				{
					atomStreamReferenceMetadata.EditLink = this.EntryMetadataDeserializer.ReadAtomLinkElementInEntryContent(linkRelation, linkHRef);
				}
			}
			else
			{
				if (newOrExistingStreamPropertyValue.ReadLink != null)
				{
					throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_StreamPropertyWithMultipleReadLinks(streamPropertyName));
				}
				newOrExistingStreamPropertyValue.ReadLink = uri;
				if (this.ReadAtomMetadata)
				{
					atomStreamReferenceMetadata.SelfLink = this.EntryMetadataDeserializer.ReadAtomLinkElementInEntryContent(linkRelation, linkHRef);
				}
			}
			string attribute = base.XmlReader.GetAttribute(this.AtomTypeAttributeName, this.EmptyNamespace);
			if (attribute != null && newOrExistingStreamPropertyValue.ContentType != null && !HttpUtils.CompareMediaTypeNames(attribute, newOrExistingStreamPropertyValue.ContentType))
			{
				throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_StreamPropertyWithMultipleContentTypes(streamPropertyName));
			}
			newOrExistingStreamPropertyValue.ContentType = attribute;
			if (editLink)
			{
				string attribute2 = base.XmlReader.GetAttribute(this.ODataETagAttributeName, base.XmlReader.ODataMetadataNamespace);
				newOrExistingStreamPropertyValue.ETag = attribute2;
			}
			base.XmlReader.Skip();
			return true;
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x0003E714 File Offset: 0x0003C914
		private bool TryReadAssociationLinkInEntry(IODataAtomReaderEntryState entryState, string linkRelation, string linkHRef)
		{
			string nameFromAtomLinkRelationAttribute = AtomUtils.GetNameFromAtomLinkRelationAttribute(linkRelation, "http://schemas.microsoft.com/ado/2007/08/dataservices/relatedlinks/");
			if (string.IsNullOrEmpty(nameFromAtomLinkRelationAttribute) || !base.ReadingResponse || base.MessageReaderSettings.MaxProtocolVersion < ODataVersion.V3)
			{
				return false;
			}
			ReaderValidationUtils.ValidateNavigationPropertyDefined(nameFromAtomLinkRelationAttribute, entryState.EntityType, base.MessageReaderSettings);
			string attribute = base.XmlReader.GetAttribute(this.AtomTypeAttributeName, this.EmptyNamespace);
			if (attribute != null && !HttpUtils.CompareMediaTypeNames(attribute, "application/xml"))
			{
				throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_InvalidTypeAttributeOnAssociationLink(nameFromAtomLinkRelationAttribute));
			}
			ODataAssociationLink odataAssociationLink = new ODataAssociationLink
			{
				Name = nameFromAtomLinkRelationAttribute
			};
			if (linkHRef != null)
			{
				odataAssociationLink.Url = base.ProcessUriFromPayload(linkHRef, base.XmlReader.XmlBaseUri);
			}
			ReaderUtils.CheckForDuplicateAssociationLinkAndUpdateNavigationLink(entryState.DuplicatePropertyNamesChecker, odataAssociationLink);
			entryState.Entry.AddAssociationLink(odataAssociationLink);
			AtomLinkMetadata atomLinkMetadata = this.EntryMetadataDeserializer.ReadAtomLinkElementInEntryContent(linkRelation, linkHRef);
			if (atomLinkMetadata != null)
			{
				odataAssociationLink.SetAnnotation<AtomLinkMetadata>(atomLinkMetadata);
			}
			base.XmlReader.Skip();
			return true;
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x0003E800 File Offset: 0x0003CA00
		private bool TryReadOperation(IODataAtomReaderEntryState entryState)
		{
			bool flag = false;
			if (base.XmlReader.LocalNameEquals(this.ODataActionElementName))
			{
				flag = true;
			}
			else if (!base.XmlReader.LocalNameEquals(this.ODataFunctionElementName))
			{
				return false;
			}
			ODataOperation odataOperation;
			if (flag)
			{
				odataOperation = new ODataAction();
				entryState.Entry.AddAction((ODataAction)odataOperation);
			}
			else
			{
				odataOperation = new ODataFunction();
				entryState.Entry.AddFunction((ODataFunction)odataOperation);
			}
			string localName = base.XmlReader.LocalName;
			while (base.XmlReader.MoveToNextAttribute())
			{
				if (base.XmlReader.NamespaceEquals(this.EmptyNamespace))
				{
					string value = base.XmlReader.Value;
					if (base.XmlReader.LocalNameEquals(this.ODataOperationMetadataAttribute))
					{
						odataOperation.Metadata = base.ProcessUriFromPayload(value, base.XmlReader.XmlBaseUri, false);
					}
					else if (base.XmlReader.LocalNameEquals(this.ODataOperationTargetAttribute))
					{
						odataOperation.Target = base.ProcessUriFromPayload(value, base.XmlReader.XmlBaseUri);
					}
					else if (base.XmlReader.LocalNameEquals(this.ODataOperationTitleAttribute))
					{
						odataOperation.Title = base.XmlReader.Value;
					}
				}
			}
			if (odataOperation.Metadata == null)
			{
				throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_OperationMissingMetadataAttribute(localName));
			}
			if (odataOperation.Target == null)
			{
				throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_OperationMissingTargetAttribute(localName));
			}
			base.XmlReader.Skip();
			return true;
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x0003E970 File Offset: 0x0003CB70
		private bool ReadAtomElementInFeed(IODataAtomReaderFeedState feedState, bool isExpandedLinkContent)
		{
			if (base.XmlReader.LocalNameEquals(this.AtomEntryElementName))
			{
				return true;
			}
			if (base.XmlReader.LocalNameEquals(this.AtomLinkElementName))
			{
				string text;
				string text2;
				this.ReadAtomLinkRelationAndHRef(out text, out text2);
				if (text != null)
				{
					if (this.ReadAtomStandardRelationLinkInFeed(feedState, text, text2, isExpandedLinkContent))
					{
						return false;
					}
					string text3 = AtomUtils.UnescapeAtomLinkRelationAttribute(text);
					if (text3 != null)
					{
						string nameFromAtomLinkRelationAttribute = AtomUtils.GetNameFromAtomLinkRelationAttribute(text, "http://www.iana.org/assignments/relation/");
						if (nameFromAtomLinkRelationAttribute != null && this.ReadAtomStandardRelationLinkInFeed(feedState, nameFromAtomLinkRelationAttribute, text2, isExpandedLinkContent))
						{
							return false;
						}
					}
				}
				if (this.ReadAtomMetadata)
				{
					AtomLinkMetadata atomLinkMetadata = this.FeedMetadataDeserializer.ReadAtomLinkElementInFeed(text, text2);
					feedState.AtomFeedMetadata.AddLink(atomLinkMetadata);
				}
				else
				{
					base.XmlReader.Skip();
				}
			}
			else if (base.XmlReader.LocalNameEquals(this.AtomIdElementName))
			{
				string text4 = base.XmlReader.ReadElementValue();
				feedState.Feed.Id = text4;
			}
			else if (this.ReadAtomMetadata)
			{
				this.FeedMetadataDeserializer.ReadAtomElementAsFeedMetadata(feedState.AtomFeedMetadata);
			}
			else
			{
				base.XmlReader.Skip();
			}
			return false;
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x0003EAB4 File Offset: 0x0003CCB4
		private bool ReadAtomStandardRelationLinkInFeed(IODataAtomReaderFeedState feedState, string linkRelation, string linkHRef, bool isExpandedLinkContent)
		{
			if (string.CompareOrdinal(linkRelation, "next") == 0)
			{
				if (!base.ReadingResponse || base.Version < ODataVersion.V2)
				{
					return false;
				}
				if (feedState.HasNextPageLink)
				{
					throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_MultipleLinksInFeed("next"));
				}
				if (linkHRef != null)
				{
					feedState.Feed.NextPageLink = base.ProcessUriFromPayload(linkHRef, base.XmlReader.XmlBaseUri);
				}
				feedState.HasNextPageLink = true;
				this.ReadLinkMetadataIfRequired(linkRelation, linkHRef, delegate(AtomLinkMetadata linkMetadata)
				{
					feedState.AtomFeedMetadata.NextPageLink = linkMetadata;
				});
				return true;
			}
			else if (string.CompareOrdinal(linkRelation, "self") == 0)
			{
				if (feedState.HasReadLink && base.AtomInputContext.UseDefaultFormatBehavior)
				{
					throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_MultipleLinksInFeed("self"));
				}
				this.ReadLinkMetadataIfRequired(linkRelation, linkHRef, delegate(AtomLinkMetadata linkMetadata)
				{
					feedState.AtomFeedMetadata.SelfLink = linkMetadata;
				});
				feedState.HasReadLink = true;
				return true;
			}
			else
			{
				if (string.CompareOrdinal(linkRelation, "http://docs.oasis-open.org/odata/ns/delta") != 0)
				{
					return false;
				}
				if (!base.ReadingResponse || base.Version < ODataVersion.V3)
				{
					return false;
				}
				if (feedState.HasDeltaLink)
				{
					throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_MultipleLinksInFeed("http://docs.oasis-open.org/odata/ns/delta"));
				}
				if (isExpandedLinkContent)
				{
					throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_EncounteredDeltaLinkInNestedFeed);
				}
				if (linkHRef != null)
				{
					feedState.Feed.DeltaLink = base.ProcessUriFromPayload(linkHRef, base.XmlReader.XmlBaseUri);
				}
				this.ReadLinkMetadataIfRequired(linkRelation, linkHRef, delegate(AtomLinkMetadata linkMetadata)
				{
					feedState.AtomFeedMetadata.AddLink(linkMetadata);
				});
				feedState.HasDeltaLink = true;
				return true;
			}
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x0003EC58 File Offset: 0x0003CE58
		private void ReadLinkMetadataIfRequired(string linkRelation, string linkHRef, Action<AtomLinkMetadata> setFeedLink)
		{
			if (this.ReadAtomMetadata)
			{
				AtomLinkMetadata atomLinkMetadata = this.FeedMetadataDeserializer.ReadAtomLinkElementInFeed(linkRelation, linkHRef);
				setFeedLink(atomLinkMetadata);
				return;
			}
			base.XmlReader.Skip();
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x0003EC90 File Offset: 0x0003CE90
		private void ReadAtomLinkRelationAndHRef(out string linkRelation, out string linkHRef)
		{
			linkRelation = null;
			linkHRef = null;
			while (base.XmlReader.MoveToNextAttribute())
			{
				if (base.XmlReader.NamespaceEquals(this.EmptyNamespace))
				{
					if (base.XmlReader.LocalNameEquals(this.AtomLinkRelationAttributeName))
					{
						linkRelation = base.XmlReader.Value;
						if (linkHRef != null)
						{
							break;
						}
					}
					else if (base.XmlReader.LocalNameEquals(this.AtomLinkHrefAttributeName))
					{
						linkHRef = base.XmlReader.Value;
						if (linkRelation != null)
						{
							break;
						}
					}
				}
			}
			base.XmlReader.MoveToElement();
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x0003ED1C File Offset: 0x0003CF1C
		private bool ReadNavigationLinkContent()
		{
			for (;;)
			{
				XmlNodeType nodeType = base.XmlReader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						break;
					}
					base.XmlReader.Skip();
				}
				else
				{
					if (base.XmlReader.LocalNameEquals(this.ODataInlineElementName) && base.XmlReader.NamespaceEquals(base.XmlReader.ODataMetadataNamespace))
					{
						return true;
					}
					base.XmlReader.Skip();
				}
			}
			return false;
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x0003ED88 File Offset: 0x0003CF88
		private ODataAtomDeserializerExpandedNavigationLinkContent ReadInlineElementContent()
		{
			for (;;)
			{
				XmlNodeType nodeType = base.XmlReader.NodeType;
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.EndElement)
					{
						break;
					}
					base.XmlReader.Skip();
				}
				else
				{
					if (base.XmlReader.NamespaceEquals(this.AtomNamespace))
					{
						goto Block_2;
					}
					base.XmlReader.Skip();
				}
			}
			return ODataAtomDeserializerExpandedNavigationLinkContent.Empty;
			Block_2:
			if (base.XmlReader.LocalNameEquals(this.AtomEntryElementName))
			{
				return ODataAtomDeserializerExpandedNavigationLinkContent.Entry;
			}
			if (base.XmlReader.LocalNameEquals(this.AtomFeedElementName))
			{
				return ODataAtomDeserializerExpandedNavigationLinkContent.Feed;
			}
			throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_UnknownElementInInline(base.XmlReader.LocalName));
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x0003EE1C File Offset: 0x0003D01C
		private string VerifyAtomContentMediaType(string contentType)
		{
			if (!HttpUtils.CompareMediaTypeNames("application/xml", contentType) && !HttpUtils.CompareMediaTypeNames("application/atom+xml", contentType))
			{
				string text;
				string text2;
				HttpUtils.ReadMimeType(contentType, out text, out text2);
				if (!HttpUtils.CompareMediaTypeNames(text, "application/xml") && !HttpUtils.CompareMediaTypeNames(text, "application/atom+xml"))
				{
					if (!base.AtomInputContext.UseClientFormatBehavior)
					{
						throw new ODataException(Strings.ODataAtomEntryAndFeedDeserializer_ContentWithWrongType(text));
					}
					base.XmlReader.SkipElementContent();
				}
			}
			return contentType;
		}

		// Token: 0x0400060F RID: 1551
		private readonly string AtomNamespace;

		// Token: 0x04000610 RID: 1552
		private readonly string AtomEntryElementName;

		// Token: 0x04000611 RID: 1553
		private readonly string AtomCategoryElementName;

		// Token: 0x04000612 RID: 1554
		private readonly string AtomCategoryTermAttributeName;

		// Token: 0x04000613 RID: 1555
		private readonly string AtomCategorySchemeAttributeName;

		// Token: 0x04000614 RID: 1556
		private readonly string AtomContentElementName;

		// Token: 0x04000615 RID: 1557
		private readonly string AtomLinkElementName;

		// Token: 0x04000616 RID: 1558
		private readonly string AtomPropertiesElementName;

		// Token: 0x04000617 RID: 1559
		private readonly string AtomFeedElementName;

		// Token: 0x04000618 RID: 1560
		private readonly string AtomIdElementName;

		// Token: 0x04000619 RID: 1561
		private readonly string AtomLinkRelationAttributeName;

		// Token: 0x0400061A RID: 1562
		private readonly string AtomLinkHrefAttributeName;

		// Token: 0x0400061B RID: 1563
		private readonly string MediaLinkEntryContentSourceAttributeName;

		// Token: 0x0400061C RID: 1564
		private readonly string ODataETagAttributeName;

		// Token: 0x0400061D RID: 1565
		private readonly string ODataCountElementName;

		// Token: 0x0400061E RID: 1566
		private readonly string ODataInlineElementName;

		// Token: 0x0400061F RID: 1567
		private readonly string ODataActionElementName;

		// Token: 0x04000620 RID: 1568
		private readonly string ODataFunctionElementName;

		// Token: 0x04000621 RID: 1569
		private readonly string ODataOperationMetadataAttribute;

		// Token: 0x04000622 RID: 1570
		private readonly string ODataOperationTitleAttribute;

		// Token: 0x04000623 RID: 1571
		private readonly string ODataOperationTargetAttribute;

		// Token: 0x04000624 RID: 1572
		private readonly ODataAtomAnnotationReader atomAnnotationReader;

		// Token: 0x04000625 RID: 1573
		private ODataAtomEntryMetadataDeserializer entryMetadataDeserializer;

		// Token: 0x04000626 RID: 1574
		private ODataAtomFeedMetadataDeserializer feedMetadataDeserializer;
	}
}
