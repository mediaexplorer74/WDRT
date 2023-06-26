using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Evaluation;
using Microsoft.Data.OData.Json;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x0200018D RID: 397
	internal sealed class ODataJsonLightEntryAndFeedDeserializer : ODataJsonLightPropertyAndValueDeserializer
	{
		// Token: 0x06000B46 RID: 2886 RVA: 0x00025C3E File Offset: 0x00023E3E
		internal ODataJsonLightEntryAndFeedDeserializer(ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
			this.annotationGroupDeserializer = new JsonLightAnnotationGroupDeserializer(jsonLightInputContext);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00025C54 File Offset: 0x00023E54
		internal void ReadFeedContentStart()
		{
			if (base.JsonReader.NodeType != JsonNodeType.StartArray)
			{
				throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_CannotReadFeedContentStart(base.JsonReader.NodeType));
			}
			base.JsonReader.ReadStartArray();
			if (base.JsonReader.NodeType != JsonNodeType.EndArray && base.JsonReader.NodeType != JsonNodeType.StartObject)
			{
				throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_InvalidNodeTypeForItemsInFeed(base.JsonReader.NodeType));
			}
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x00025CCC File Offset: 0x00023ECC
		internal void ReadFeedContentEnd()
		{
			base.JsonReader.ReadEndArray();
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00025CDC File Offset: 0x00023EDC
		internal void ReadEntryTypeName(IODataJsonLightReaderEntryState entryState)
		{
			if (base.JsonReader.NodeType == JsonNodeType.Property && string.CompareOrdinal("odata.type", base.JsonReader.GetPropertyName()) == 0)
			{
				if (!string.IsNullOrEmpty(entryState.Entry.TypeName))
				{
					throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_EntryTypeAlreadySpecified);
				}
				base.JsonReader.Read();
				entryState.Entry.TypeName = base.ReadODataTypeAnnotationValue();
			}
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00025E14 File Offset: 0x00024014
		internal ODataJsonLightReaderNavigationLinkInfo ReadEntryContent(IODataJsonLightReaderEntryState entryState)
		{
			ODataJsonLightReaderNavigationLinkInfo navigationLinkInfo = null;
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				base.ProcessProperty(entryState.DuplicatePropertyNamesChecker, new Func<string, object>(this.ReadEntryPropertyAnnotationValue), delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult, string propertyName)
				{
					switch (propertyParsingResult)
					{
					case ODataJsonLightDeserializer.PropertyParsingResult.EndOfObject:
						break;
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithValue:
						entryState.AnyPropertyFound = true;
						navigationLinkInfo = this.ReadEntryPropertyWithValue(entryState, propertyName);
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue:
						entryState.AnyPropertyFound = true;
						navigationLinkInfo = this.ReadEntryPropertyWithoutValue(entryState, propertyName);
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation:
					case ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation:
					{
						object obj = this.ReadEntryInstanceAnnotation(propertyName, entryState.AnyPropertyFound, true, entryState.DuplicatePropertyNamesChecker);
						this.ApplyEntryInstanceAnnotation(entryState, propertyName, obj);
						return;
					}
					case ODataJsonLightDeserializer.PropertyParsingResult.MetadataReferenceProperty:
						this.ReadMetadataReferencePropertyValue(entryState, propertyName);
						break;
					default:
						return;
					}
				});
				if (navigationLinkInfo != null)
				{
					break;
				}
			}
			return navigationLinkInfo;
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x00025E8C File Offset: 0x0002408C
		internal void ValidateEntryMetadata(IODataJsonLightReaderEntryState entryState)
		{
			ODataEntry entry = entryState.Entry;
			if (entry != null)
			{
				IEdmEntityType entityType = entryState.EntityType;
				if (!base.ReadingResponse && base.Model.HasDefaultStream(entityType) && entry.MediaResource == null)
				{
					ODataStreamReferenceValue mediaResource = entry.MediaResource;
					ODataJsonLightReaderUtils.EnsureInstance<ODataStreamReferenceValue>(ref mediaResource);
					this.SetEntryMediaResource(entryState, mediaResource);
				}
				bool flag = base.UseDefaultFormatBehavior || base.UseServerFormatBehavior;
				ValidationUtils.ValidateEntryMetadataResource(entry, entityType, base.Model, flag);
			}
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x00026030 File Offset: 0x00024230
		internal void ReadTopLevelFeedAnnotations(ODataFeed feed, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, bool forFeedStart, bool readAllFeedProperties)
		{
			bool buffering = false;
			try
			{
				while (base.JsonReader.NodeType == JsonNodeType.Property)
				{
					bool foundValueProperty = false;
					if (!forFeedStart && readAllFeedProperties)
					{
						duplicatePropertyNamesChecker = new DuplicatePropertyNamesChecker(true, base.JsonLightInputContext.ReadingResponse);
					}
					base.ProcessProperty(duplicatePropertyNamesChecker, new Func<string, object>(base.ReadTypePropertyAnnotationValue), delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParseResult, string propertyName)
					{
						switch (propertyParseResult)
						{
						case ODataJsonLightDeserializer.PropertyParsingResult.EndOfObject:
							return;
						case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithValue:
							if (string.CompareOrdinal("value", propertyName) != 0)
							{
								throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_InvalidPropertyInTopLevelFeed(propertyName, "value"));
							}
							if (readAllFeedProperties)
							{
								this.JsonReader.StartBuffering();
								buffering = true;
								this.JsonReader.SkipValue();
								return;
							}
							foundValueProperty = true;
							return;
						case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue:
							throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_InvalidPropertyAnnotationInTopLevelFeed(propertyName));
						case ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation:
						case ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation:
							if (forFeedStart || !readAllFeedProperties)
							{
								this.ReadAndApplyFeedInstanceAnnotationValue(propertyName, feed, duplicatePropertyNamesChecker);
								return;
							}
							this.JsonReader.SkipValue();
							return;
						case ODataJsonLightDeserializer.PropertyParsingResult.MetadataReferenceProperty:
							throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedMetadataReferenceProperty(propertyName));
						default:
							throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataJsonLightEntryAndFeedDeserializer_ReadTopLevelFeedAnnotations));
						}
					});
					if (foundValueProperty)
					{
						return;
					}
				}
			}
			finally
			{
				if (buffering)
				{
					base.JsonReader.StopBuffering();
				}
			}
			if (forFeedStart && !readAllFeedProperties)
			{
				throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_ExpectedFeedPropertyNotFound("value"));
			}
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x00026130 File Offset: 0x00024330
		internal object ReadEntryPropertyAnnotationValue(string propertyAnnotationName)
		{
			string text;
			if (base.TryReadODataTypeAnnotationValue(propertyAnnotationName, out text))
			{
				return text;
			}
			switch (propertyAnnotationName)
			{
			case "odata.navigationLinkUrl":
			case "odata.associationLinkUrl":
			case "odata.nextLink":
			case "odata.mediaEditLink":
			case "odata.mediaReadLink":
				return base.ReadAndValidateAnnotationStringValueAsUri(propertyAnnotationName);
			case "odata.mediaETag":
			case "odata.mediaContentType":
				return base.ReadAndValidateAnnotationStringValue(propertyAnnotationName);
			case "odata.bind":
			{
				if (base.JsonReader.NodeType != JsonNodeType.StartArray)
				{
					return new ODataEntityReferenceLink
					{
						Url = base.ReadAndValidateAnnotationStringValueAsUri("odata.bind")
					};
				}
				LinkedList<ODataEntityReferenceLink> linkedList = new LinkedList<ODataEntityReferenceLink>();
				base.JsonReader.Read();
				while (base.JsonReader.NodeType != JsonNodeType.EndArray)
				{
					linkedList.AddLast(new ODataEntityReferenceLink
					{
						Url = base.ReadAndValidateAnnotationStringValueAsUri("odata.bind")
					});
				}
				base.JsonReader.Read();
				if (linkedList.Count == 0)
				{
					throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_EmptyBindArray("odata.bind"));
				}
				return linkedList;
			}
			}
			throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedAnnotationProperties(propertyAnnotationName));
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x000262D0 File Offset: 0x000244D0
		internal void ApplyAnnotationGroupIfPresent(IODataJsonLightReaderEntryState entryState)
		{
			ODataJsonLightAnnotationGroup odataJsonLightAnnotationGroup = this.annotationGroupDeserializer.ReadAnnotationGroup(new Func<string, object>(this.ReadEntryPropertyAnnotationValue), (string annotationName, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker) => this.ReadEntryInstanceAnnotation(annotationName, false, false, duplicatePropertyNamesChecker));
			this.ApplyAnnotationGroup(entryState, odataJsonLightAnnotationGroup);
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0002630C File Offset: 0x0002450C
		internal object ReadEntryInstanceAnnotation(string annotationName, bool anyPropertyFound, bool typeAnnotationFound, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			switch (annotationName)
			{
			case "odata.type":
				if (!typeAnnotationFound)
				{
					return base.ReadODataTypeAnnotationValue();
				}
				throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_EntryTypeAnnotationNotFirst);
			case "odata.id":
				if (anyPropertyFound)
				{
					throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_EntryInstanceAnnotationPrecededByProperty(annotationName));
				}
				return base.ReadAndValidateAnnotationStringValue(annotationName);
			case "odata.etag":
				if (anyPropertyFound)
				{
					throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_EntryInstanceAnnotationPrecededByProperty(annotationName));
				}
				return base.ReadAndValidateAnnotationStringValue(annotationName);
			case "odata.editLink":
			case "odata.readLink":
			case "odata.mediaEditLink":
			case "odata.mediaReadLink":
				return base.ReadAndValidateAnnotationStringValueAsUri(annotationName);
			case "odata.mediaContentType":
			case "odata.mediaETag":
				return base.ReadAndValidateAnnotationStringValue(annotationName);
			case "odata.annotationGroup":
			case "odata.annotationGroupReference":
				throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_EncounteredAnnotationGroupInUnexpectedPosition);
			}
			ODataAnnotationNames.ValidateIsCustomAnnotationName(annotationName);
			return this.ReadCustomInstanceAnnotationValue(duplicatePropertyNamesChecker, annotationName);
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00026478 File Offset: 0x00024678
		internal void ApplyEntryInstanceAnnotation(IODataJsonLightReaderEntryState entryState, string annotationName, object annotationValue)
		{
			ODataEntry entry = entryState.Entry;
			ODataStreamReferenceValue mediaResource = entry.MediaResource;
			switch (annotationName)
			{
			case "odata.type":
				entry.TypeName = (string)annotationValue;
				goto IL_1C1;
			case "odata.id":
				entry.Id = (string)annotationValue;
				goto IL_1C1;
			case "odata.etag":
				entry.ETag = (string)annotationValue;
				goto IL_1C1;
			case "odata.editLink":
				entry.EditLink = (Uri)annotationValue;
				goto IL_1C1;
			case "odata.readLink":
				entry.ReadLink = (Uri)annotationValue;
				goto IL_1C1;
			case "odata.mediaEditLink":
				ODataJsonLightReaderUtils.EnsureInstance<ODataStreamReferenceValue>(ref mediaResource);
				mediaResource.EditLink = (Uri)annotationValue;
				goto IL_1C1;
			case "odata.mediaReadLink":
				ODataJsonLightReaderUtils.EnsureInstance<ODataStreamReferenceValue>(ref mediaResource);
				mediaResource.ReadLink = (Uri)annotationValue;
				goto IL_1C1;
			case "odata.mediaContentType":
				ODataJsonLightReaderUtils.EnsureInstance<ODataStreamReferenceValue>(ref mediaResource);
				mediaResource.ContentType = (string)annotationValue;
				goto IL_1C1;
			case "odata.mediaETag":
				ODataJsonLightReaderUtils.EnsureInstance<ODataStreamReferenceValue>(ref mediaResource);
				mediaResource.ETag = (string)annotationValue;
				goto IL_1C1;
			case "odata.annotationGroup":
			case "odata.annotationGroupReference":
				goto IL_1C1;
			}
			ODataAnnotationNames.ValidateIsCustomAnnotationName(annotationName);
			entry.InstanceAnnotations.Add(new ODataInstanceAnnotation(annotationName, annotationValue.ToODataValue()));
			IL_1C1:
			if (mediaResource != null && entry.MediaResource == null)
			{
				this.SetEntryMediaResource(entryState, mediaResource);
			}
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0002665C File Offset: 0x0002485C
		internal object ReadCustomInstanceAnnotationValue(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, string name)
		{
			Dictionary<string, object> odataPropertyAnnotations = duplicatePropertyNamesChecker.GetODataPropertyAnnotations(name);
			string text = null;
			object obj;
			if (odataPropertyAnnotations != null && odataPropertyAnnotations.TryGetValue("odata.type", out obj))
			{
				text = (string)obj;
			}
			IEdmTypeReference edmTypeReference = MetadataUtils.LookupTypeOfValueTerm(name, base.Model);
			return base.ReadNonEntityValue(text, edmTypeReference, null, null, false, false, false, name);
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x000266AC File Offset: 0x000248AC
		internal void ReadAndApplyFeedInstanceAnnotationValue(string annotationName, ODataFeed feed, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			if (annotationName != null)
			{
				if (annotationName == "odata.count")
				{
					feed.Count = new long?(base.ReadAndValidateAnnotationStringValueAsLong("odata.count"));
					return;
				}
				if (annotationName == "odata.nextLink")
				{
					feed.NextPageLink = base.ReadAndValidateAnnotationStringValueAsUri("odata.nextLink");
					return;
				}
				if (annotationName == "odata.deltaLink")
				{
					feed.DeltaLink = base.ReadAndValidateAnnotationStringValueAsUri("odata.deltaLink");
					return;
				}
			}
			ODataAnnotationNames.ValidateIsCustomAnnotationName(annotationName);
			object obj = this.ReadCustomInstanceAnnotationValue(duplicatePropertyNamesChecker, annotationName);
			feed.InstanceAnnotations.Add(new ODataInstanceAnnotation(annotationName, obj.ToODataValue()));
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x00026748 File Offset: 0x00024948
		internal ODataJsonLightReaderNavigationLinkInfo ReadEntryPropertyWithoutValue(IODataJsonLightReaderEntryState entryState, string propertyName)
		{
			ODataJsonLightReaderNavigationLinkInfo odataJsonLightReaderNavigationLinkInfo = null;
			IEdmEntityType entityType = entryState.EntityType;
			IEdmProperty edmProperty = ReaderValidationUtils.FindDefinedProperty(propertyName, entityType);
			if (edmProperty != null)
			{
				IEdmNavigationProperty edmNavigationProperty = edmProperty as IEdmNavigationProperty;
				if (edmNavigationProperty != null)
				{
					if (base.ReadingResponse)
					{
						odataJsonLightReaderNavigationLinkInfo = ODataJsonLightEntryAndFeedDeserializer.ReadDeferredNavigationLink(entryState, propertyName, edmNavigationProperty);
					}
					else
					{
						odataJsonLightReaderNavigationLinkInfo = (edmNavigationProperty.Type.IsCollection() ? ODataJsonLightEntryAndFeedDeserializer.ReadEntityReferenceLinksForCollectionNavigationLinkInRequest(entryState, edmNavigationProperty, false) : ODataJsonLightEntryAndFeedDeserializer.ReadEntityReferenceLinkForSingletonNavigationLinkInRequest(entryState, edmNavigationProperty, false));
						if (!odataJsonLightReaderNavigationLinkInfo.HasEntityReferenceLink)
						{
							throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_NavigationPropertyWithoutValueAndEntityReferenceLink(propertyName, "odata.bind"));
						}
					}
					entryState.DuplicatePropertyNamesChecker.CheckForDuplicatePropertyNamesOnNavigationLinkStart(odataJsonLightReaderNavigationLinkInfo.NavigationLink);
				}
				else
				{
					IEdmTypeReference type = edmProperty.Type;
					if (!type.IsStream())
					{
						throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_PropertyWithoutValueWithWrongType(propertyName, type.ODataFullName()));
					}
					ODataStreamReferenceValue odataStreamReferenceValue = this.ReadStreamPropertyValue(entryState, propertyName);
					ODataJsonLightEntryAndFeedDeserializer.AddEntryProperty(entryState, edmProperty.Name, odataStreamReferenceValue);
				}
			}
			else
			{
				odataJsonLightReaderNavigationLinkInfo = this.ReadUndeclaredProperty(entryState, propertyName, false);
			}
			return odataJsonLightReaderNavigationLinkInfo;
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x00026820 File Offset: 0x00024A20
		internal void ReadNextLinkAnnotationAtFeedEnd(ODataFeed feed, ODataJsonLightReaderNavigationLinkInfo expandedNavigationLinkInfo, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			if (expandedNavigationLinkInfo != null)
			{
				this.ReadExpandedFeedAnnotationsAtFeedEnd(feed, expandedNavigationLinkInfo);
				return;
			}
			bool flag = base.JsonReader is ReorderingJsonReader;
			this.ReadTopLevelFeedAnnotations(feed, duplicatePropertyNamesChecker, false, flag);
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x00026854 File Offset: 0x00024A54
		private static ODataJsonLightReaderNavigationLinkInfo ReadDeferredNavigationLink(IODataJsonLightReaderEntryState entryState, string navigationPropertyName, IEdmNavigationProperty navigationProperty)
		{
			ODataNavigationLink odataNavigationLink = new ODataNavigationLink
			{
				Name = navigationPropertyName,
				IsCollection = ((navigationProperty == null) ? null : new bool?(navigationProperty.Type.IsCollection()))
			};
			Dictionary<string, object> odataPropertyAnnotations = entryState.DuplicatePropertyNamesChecker.GetODataPropertyAnnotations(odataNavigationLink.Name);
			if (odataPropertyAnnotations != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in odataPropertyAnnotations)
				{
					string key;
					if ((key = keyValuePair.Key) != null)
					{
						if (key == "odata.navigationLinkUrl")
						{
							odataNavigationLink.Url = (Uri)keyValuePair.Value;
							continue;
						}
						if (key == "odata.associationLinkUrl")
						{
							odataNavigationLink.AssociationLinkUrl = (Uri)keyValuePair.Value;
							continue;
						}
					}
					throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_UnexpectedDeferredLinkPropertyAnnotation(odataNavigationLink.Name, keyValuePair.Key));
				}
			}
			return ODataJsonLightReaderNavigationLinkInfo.CreateDeferredLinkInfo(odataNavigationLink, navigationProperty);
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0002695C File Offset: 0x00024B5C
		private static ODataJsonLightReaderNavigationLinkInfo ReadExpandedEntryNavigationLink(IODataJsonLightReaderEntryState entryState, IEdmNavigationProperty navigationProperty)
		{
			ODataNavigationLink odataNavigationLink = new ODataNavigationLink
			{
				Name = navigationProperty.Name,
				IsCollection = new bool?(false)
			};
			Dictionary<string, object> odataPropertyAnnotations = entryState.DuplicatePropertyNamesChecker.GetODataPropertyAnnotations(odataNavigationLink.Name);
			if (odataPropertyAnnotations != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in odataPropertyAnnotations)
				{
					string key;
					if ((key = keyValuePair.Key) != null)
					{
						if (key == "odata.navigationLinkUrl")
						{
							odataNavigationLink.Url = (Uri)keyValuePair.Value;
							continue;
						}
						if (key == "odata.associationLinkUrl")
						{
							odataNavigationLink.AssociationLinkUrl = (Uri)keyValuePair.Value;
							continue;
						}
					}
					throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_UnexpectedExpandedSingletonNavigationLinkPropertyAnnotation(odataNavigationLink.Name, keyValuePair.Key));
				}
			}
			return ODataJsonLightReaderNavigationLinkInfo.CreateExpandedEntryLinkInfo(odataNavigationLink, navigationProperty);
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x00026A50 File Offset: 0x00024C50
		private static ODataJsonLightReaderNavigationLinkInfo ReadExpandedFeedNavigationLink(IODataJsonLightReaderEntryState entryState, IEdmNavigationProperty navigationProperty)
		{
			ODataNavigationLink odataNavigationLink = new ODataNavigationLink
			{
				Name = navigationProperty.Name,
				IsCollection = new bool?(true)
			};
			ODataFeed odataFeed = new ODataFeed();
			Dictionary<string, object> odataPropertyAnnotations = entryState.DuplicatePropertyNamesChecker.GetODataPropertyAnnotations(odataNavigationLink.Name);
			if (odataPropertyAnnotations != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in odataPropertyAnnotations)
				{
					string key;
					if ((key = keyValuePair.Key) != null)
					{
						if (key == "odata.navigationLinkUrl")
						{
							odataNavigationLink.Url = (Uri)keyValuePair.Value;
							continue;
						}
						if (key == "odata.associationLinkUrl")
						{
							odataNavigationLink.AssociationLinkUrl = (Uri)keyValuePair.Value;
							continue;
						}
						if (key == "odata.nextLink")
						{
							odataFeed.NextPageLink = (Uri)keyValuePair.Value;
							continue;
						}
						if (!(key == "odata.deltaLink"))
						{
						}
					}
					throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_UnexpectedExpandedCollectionNavigationLinkPropertyAnnotation(odataNavigationLink.Name, keyValuePair.Key));
				}
			}
			return ODataJsonLightReaderNavigationLinkInfo.CreateExpandedFeedLinkInfo(odataNavigationLink, navigationProperty, odataFeed);
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x00026B84 File Offset: 0x00024D84
		private static ODataJsonLightReaderNavigationLinkInfo ReadEntityReferenceLinkForSingletonNavigationLinkInRequest(IODataJsonLightReaderEntryState entryState, IEdmNavigationProperty navigationProperty, bool isExpanded)
		{
			ODataNavigationLink odataNavigationLink = new ODataNavigationLink
			{
				Name = navigationProperty.Name,
				IsCollection = new bool?(false)
			};
			Dictionary<string, object> odataPropertyAnnotations = entryState.DuplicatePropertyNamesChecker.GetODataPropertyAnnotations(odataNavigationLink.Name);
			ODataEntityReferenceLink odataEntityReferenceLink = null;
			if (odataPropertyAnnotations != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in odataPropertyAnnotations)
				{
					string key;
					if ((key = keyValuePair.Key) == null || !(key == "odata.bind"))
					{
						throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_UnexpectedNavigationLinkInRequestPropertyAnnotation(odataNavigationLink.Name, keyValuePair.Key, "odata.bind"));
					}
					LinkedList<ODataEntityReferenceLink> linkedList = keyValuePair.Value as LinkedList<ODataEntityReferenceLink>;
					if (linkedList != null)
					{
						throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_ArrayValueForSingletonBindPropertyAnnotation(odataNavigationLink.Name, "odata.bind"));
					}
					if (isExpanded)
					{
						throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_SingletonNavigationPropertyWithBindingAndValue(odataNavigationLink.Name, "odata.bind"));
					}
					odataEntityReferenceLink = (ODataEntityReferenceLink)keyValuePair.Value;
				}
			}
			return ODataJsonLightReaderNavigationLinkInfo.CreateSingletonEntityReferenceLinkInfo(odataNavigationLink, navigationProperty, odataEntityReferenceLink, isExpanded);
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x00026CA0 File Offset: 0x00024EA0
		private static ODataJsonLightReaderNavigationLinkInfo ReadEntityReferenceLinksForCollectionNavigationLinkInRequest(IODataJsonLightReaderEntryState entryState, IEdmNavigationProperty navigationProperty, bool isExpanded)
		{
			ODataNavigationLink odataNavigationLink = new ODataNavigationLink
			{
				Name = navigationProperty.Name,
				IsCollection = new bool?(true)
			};
			Dictionary<string, object> odataPropertyAnnotations = entryState.DuplicatePropertyNamesChecker.GetODataPropertyAnnotations(odataNavigationLink.Name);
			LinkedList<ODataEntityReferenceLink> linkedList = null;
			if (odataPropertyAnnotations != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in odataPropertyAnnotations)
				{
					string key;
					if ((key = keyValuePair.Key) == null || !(key == "odata.bind"))
					{
						throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_UnexpectedNavigationLinkInRequestPropertyAnnotation(odataNavigationLink.Name, keyValuePair.Key, "odata.bind"));
					}
					ODataEntityReferenceLink odataEntityReferenceLink = keyValuePair.Value as ODataEntityReferenceLink;
					if (odataEntityReferenceLink != null)
					{
						throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_StringValueForCollectionBindPropertyAnnotation(odataNavigationLink.Name, "odata.bind"));
					}
					linkedList = (LinkedList<ODataEntityReferenceLink>)keyValuePair.Value;
				}
			}
			return ODataJsonLightReaderNavigationLinkInfo.CreateCollectionEntityReferenceLinksInfo(odataNavigationLink, navigationProperty, linkedList, isExpanded);
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x00026DA0 File Offset: 0x00024FA0
		private static ODataProperty AddEntryProperty(IODataJsonLightReaderEntryState entryState, string propertyName, object propertyValue)
		{
			ODataProperty odataProperty = new ODataProperty
			{
				Name = propertyName,
				Value = propertyValue
			};
			entryState.DuplicatePropertyNamesChecker.CheckForDuplicatePropertyNames(odataProperty);
			ODataEntry entry = entryState.Entry;
			entry.Properties = entry.Properties.ConcatToReadOnlyEnumerable("Properties", odataProperty);
			return odataProperty;
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x00026DF0 File Offset: 0x00024FF0
		private void ReadExpandedFeedAnnotationsAtFeedEnd(ODataFeed feed, ODataJsonLightReaderNavigationLinkInfo expandedNavigationLinkInfo)
		{
			string text;
			string text2;
			while (base.JsonReader.NodeType == JsonNodeType.Property && ODataJsonLightDeserializer.TryParsePropertyAnnotation(base.JsonReader.GetPropertyName(), out text, out text2) && string.CompareOrdinal(text, expandedNavigationLinkInfo.NavigationLink.Name) == 0)
			{
				if (!base.ReadingResponse)
				{
					throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedPropertyAnnotation(text, text2));
				}
				base.JsonReader.Read();
				string text3;
				if ((text3 = text2) != null)
				{
					if (!(text3 == "odata.nextLink"))
					{
						if (!(text3 == "odata.count") && !(text3 == "odata.deltaLink"))
						{
						}
					}
					else
					{
						if (feed.NextPageLink != null)
						{
							throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_DuplicateExpandedFeedAnnotation("odata.nextLink", expandedNavigationLinkInfo.NavigationLink.Name));
						}
						feed.NextPageLink = base.ReadAndValidateAnnotationStringValueAsUri("odata.nextLink");
						continue;
					}
				}
				throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_UnexpectedPropertyAnnotationAfterExpandedFeed(text2, expandedNavigationLinkInfo.NavigationLink.Name));
			}
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x00026EE0 File Offset: 0x000250E0
		private void ApplyAnnotationGroup(IODataJsonLightReaderEntryState entryState, ODataJsonLightAnnotationGroup annotationGroup)
		{
			if (annotationGroup != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in annotationGroup.Annotations)
				{
					string key = keyValuePair.Key;
					object value = keyValuePair.Value;
					string text;
					string text2;
					if (ODataJsonLightDeserializer.TryParsePropertyAnnotation(key, out text, out text2))
					{
						entryState.DuplicatePropertyNamesChecker.AddODataPropertyAnnotation(text, text2, value);
					}
					else
					{
						this.ApplyEntryInstanceAnnotation(entryState, key, value);
					}
				}
			}
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x00026F64 File Offset: 0x00025164
		private void SetEntryMediaResource(IODataJsonLightReaderEntryState entryState, ODataStreamReferenceValue mediaResource)
		{
			ODataEntry entry = entryState.Entry;
			ODataEntityMetadataBuilder entityMetadataBuilderForReader = base.MetadataContext.GetEntityMetadataBuilderForReader(entryState);
			mediaResource.SetMetadataBuilder(entityMetadataBuilderForReader, null);
			entry.MediaResource = mediaResource;
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x00026F94 File Offset: 0x00025194
		private ODataJsonLightReaderNavigationLinkInfo ReadEntryPropertyWithValue(IODataJsonLightReaderEntryState entryState, string propertyName)
		{
			ODataJsonLightReaderNavigationLinkInfo odataJsonLightReaderNavigationLinkInfo = null;
			IEdmEntityType entityType = entryState.EntityType;
			IEdmProperty edmProperty = ReaderValidationUtils.FindDefinedProperty(propertyName, entityType);
			if (edmProperty != null)
			{
				IEdmNavigationProperty edmNavigationProperty = edmProperty as IEdmNavigationProperty;
				if (edmNavigationProperty != null)
				{
					bool flag = edmNavigationProperty.Type.IsCollection();
					this.ValidateExpandedNavigationLinkPropertyValue(new bool?(flag));
					if (flag)
					{
						odataJsonLightReaderNavigationLinkInfo = (base.ReadingResponse ? ODataJsonLightEntryAndFeedDeserializer.ReadExpandedFeedNavigationLink(entryState, edmNavigationProperty) : ODataJsonLightEntryAndFeedDeserializer.ReadEntityReferenceLinksForCollectionNavigationLinkInRequest(entryState, edmNavigationProperty, true));
					}
					else
					{
						odataJsonLightReaderNavigationLinkInfo = (base.ReadingResponse ? ODataJsonLightEntryAndFeedDeserializer.ReadExpandedEntryNavigationLink(entryState, edmNavigationProperty) : ODataJsonLightEntryAndFeedDeserializer.ReadEntityReferenceLinkForSingletonNavigationLinkInRequest(entryState, edmNavigationProperty, true));
					}
					entryState.DuplicatePropertyNamesChecker.CheckForDuplicatePropertyNamesOnNavigationLinkStart(odataJsonLightReaderNavigationLinkInfo.NavigationLink);
				}
				else
				{
					IEdmTypeReference type = edmProperty.Type;
					if (type.IsStream())
					{
						throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_StreamPropertyWithValue(propertyName));
					}
					this.ReadEntryDataProperty(entryState, edmProperty, ODataJsonLightPropertyAndValueDeserializer.ValidateDataPropertyTypeNameAnnotation(entryState.DuplicatePropertyNamesChecker, propertyName));
				}
			}
			else
			{
				odataJsonLightReaderNavigationLinkInfo = this.ReadUndeclaredProperty(entryState, propertyName, true);
			}
			return odataJsonLightReaderNavigationLinkInfo;
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x00027068 File Offset: 0x00025268
		private void ReadEntryDataProperty(IODataJsonLightReaderEntryState entryState, IEdmProperty edmProperty, string propertyTypeName)
		{
			ODataNullValueBehaviorKind odataNullValueBehaviorKind = (base.ReadingResponse ? ODataNullValueBehaviorKind.Default : base.Model.NullValueReadBehaviorKind(edmProperty));
			object obj = base.ReadNonEntityValue(propertyTypeName, edmProperty.Type, null, null, odataNullValueBehaviorKind == ODataNullValueBehaviorKind.Default, false, false, edmProperty.Name);
			if (odataNullValueBehaviorKind != ODataNullValueBehaviorKind.IgnoreValue || obj != null)
			{
				ODataJsonLightEntryAndFeedDeserializer.AddEntryProperty(entryState, edmProperty.Name, obj);
			}
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x000270C0 File Offset: 0x000252C0
		private void InnerReadOpenUndeclaredProperty(IODataJsonLightReaderEntryState entryState, IEdmStructuredType owningStructuredType, string propertyName, bool propertyWithValue)
		{
			if (!propertyWithValue)
			{
				throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_OpenPropertyWithoutValue(propertyName));
			}
			bool flag = false;
			string text = ODataJsonLightPropertyAndValueDeserializer.ValidateDataPropertyTypeNameAnnotation(entryState.DuplicatePropertyNamesChecker, propertyName);
			string text2 = base.TryReadOrPeekPayloadType(entryState.DuplicatePropertyNamesChecker, propertyName, flag);
			EdmTypeKind edmTypeKind;
			IEdmType edmType = ReaderValidationUtils.ResolvePayloadTypeName(base.Model, null, text2, EdmTypeKind.Complex, base.MessageReaderSettings.ReaderBehavior, base.Version, out edmTypeKind);
			IEdmTypeReference edmTypeReference = null;
			if (!string.IsNullOrEmpty(text2) && edmType != null)
			{
				EdmTypeKind edmTypeKind2;
				SerializationTypeNameAnnotation serializationTypeNameAnnotation;
				edmTypeReference = ReaderValidationUtils.ResolvePayloadTypeNameAndComputeTargetType(EdmTypeKind.None, null, null, text2, base.Model, base.MessageReaderSettings, base.Version, new Func<EdmTypeKind>(base.GetNonEntityValueKind), out edmTypeKind2, out serializationTypeNameAnnotation);
			}
			object obj = null;
			bool flag2 = ODataJsonLightPropertyAndValueDeserializer.IsKnownValueTypeForOpenEntityOrComplex(base.JsonReader.NodeType, base.JsonReader.Value, text2, edmTypeReference);
			bool flag5;
			if (flag2)
			{
				bool flag3 = true;
				if (ODataJsonReaderCoreUtils.TryReadNullValue(base.JsonReader, base.JsonLightInputContext, edmTypeReference, flag3, propertyName))
				{
					bool flag4 = false;
					if (flag4)
					{
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_TopLevelPropertyWithPrimitiveNullValue("odata.null", "true"));
					}
					obj = null;
				}
				else
				{
					obj = base.ReadNonEntityValue(text, null, null, null, true, false, false, propertyName);
				}
				flag5 = true;
			}
			else if (!base.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.IgnoreUndeclaredValueProperty) && !base.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.SupportUndeclaredValueProperty))
			{
				if (!string.IsNullOrEmpty(text2) && edmTypeReference == null)
				{
					throw new ODataException(Strings.ValidationUtils_UnrecognizedTypeName(text2));
				}
				if (string.IsNullOrEmpty(text2) && (base.JsonReader.NodeType == JsonNodeType.StartObject || base.JsonReader.NodeType == JsonNodeType.StartArray))
				{
					throw new ODataException(Strings.ReaderValidationUtils_ValueWithoutType);
				}
				throw new ODataException(Strings.ValidationUtils_PropertyDoesNotExistOnType(propertyName, owningStructuredType.ODataFullName()));
			}
			else if (base.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.SupportUndeclaredValueProperty))
			{
				StringBuilder stringBuilder = new StringBuilder();
				base.JsonReader.SkipValue(stringBuilder);
				obj = new ODataUntypedValue
				{
					RawJson = stringBuilder.ToString()
				};
				flag5 = true;
			}
			else
			{
				base.JsonReader.SkipValue();
				flag5 = false;
			}
			if (flag5)
			{
				ValidationUtils.ValidateOpenPropertyValue(propertyName, obj, base.MessageReaderSettings.UndeclaredPropertyBehaviorKinds);
				ODataProperty odataProperty = ODataJsonLightEntryAndFeedDeserializer.AddEntryProperty(entryState, propertyName, obj);
				bool flag6 = obj is ODataUntypedValue;
				if (flag6)
				{
					ODataJsonLightPropertyAndValueDeserializer.TryAttachRawAnnotationSetToPropertyValue(entryState.DuplicatePropertyNamesChecker, odataProperty);
				}
			}
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x000272DC File Offset: 0x000254DC
		private ODataJsonLightReaderNavigationLinkInfo ReadUndeclaredProperty(IODataJsonLightReaderEntryState entryState, string propertyName, bool propertyWithValue)
		{
			if (!base.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.ReportUndeclaredLinkProperty) && entryState.EntityType.IsOpen)
			{
				this.InnerReadOpenUndeclaredProperty(entryState, entryState.EntityType, propertyName, propertyWithValue);
				return null;
			}
			Dictionary<string, object> odataPropertyAnnotations = entryState.DuplicatePropertyNamesChecker.GetODataPropertyAnnotations(propertyName);
			if (odataPropertyAnnotations != null)
			{
				object obj;
				if (odataPropertyAnnotations.TryGetValue("odata.navigationLinkUrl", out obj) || odataPropertyAnnotations.TryGetValue("odata.associationLinkUrl", out obj))
				{
					if (!base.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.ReportUndeclaredLinkProperty))
					{
						throw new ODataException(Strings.ValidationUtils_PropertyDoesNotExistOnType(propertyName, entryState.EntityType.ODataFullName()));
					}
					ODataJsonLightReaderNavigationLinkInfo odataJsonLightReaderNavigationLinkInfo = ODataJsonLightEntryAndFeedDeserializer.ReadDeferredNavigationLink(entryState, propertyName, null);
					entryState.DuplicatePropertyNamesChecker.CheckForDuplicatePropertyNamesOnNavigationLinkStart(odataJsonLightReaderNavigationLinkInfo.NavigationLink);
					if (propertyWithValue)
					{
						if (!base.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.IgnoreUndeclaredValueProperty) && !base.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.SupportUndeclaredValueProperty))
						{
							throw new ODataException(Strings.ValidationUtils_PropertyDoesNotExistOnType(propertyName, entryState.EntityType.ODataFullName()));
						}
						this.ValidateExpandedNavigationLinkPropertyValue(null);
						base.JsonReader.SkipValue();
					}
					return odataJsonLightReaderNavigationLinkInfo;
				}
				else if (odataPropertyAnnotations.TryGetValue("odata.mediaEditLink", out obj) || odataPropertyAnnotations.TryGetValue("odata.mediaReadLink", out obj) || odataPropertyAnnotations.TryGetValue("odata.mediaContentType", out obj) || odataPropertyAnnotations.TryGetValue("odata.mediaETag", out obj))
				{
					if (!base.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.ReportUndeclaredLinkProperty))
					{
						throw new ODataException(Strings.ValidationUtils_PropertyDoesNotExistOnType(propertyName, entryState.EntityType.ODataFullName()));
					}
					if (propertyWithValue)
					{
						throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_StreamPropertyWithValue(propertyName));
					}
					ODataStreamReferenceValue odataStreamReferenceValue = this.ReadStreamPropertyValue(entryState, propertyName);
					ODataJsonLightEntryAndFeedDeserializer.AddEntryProperty(entryState, propertyName, odataStreamReferenceValue);
					return null;
				}
			}
			if (entryState.EntityType.IsOpen)
			{
				this.InnerReadOpenUndeclaredProperty(entryState, entryState.EntityType, propertyName, propertyWithValue);
				return null;
			}
			if (!propertyWithValue)
			{
				throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_PropertyWithoutValueWithUnknownType(propertyName));
			}
			if (!base.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.IgnoreUndeclaredValueProperty) && !base.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.SupportUndeclaredValueProperty))
			{
				throw new ODataException(Strings.ValidationUtils_PropertyDoesNotExistOnType(propertyName, entryState.EntityType.ODataFullName()));
			}
			ODataJsonLightPropertyAndValueDeserializer.ValidateDataPropertyTypeNameAnnotation(entryState.DuplicatePropertyNamesChecker, propertyName);
			if (base.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.SupportUndeclaredValueProperty))
			{
				bool flag = false;
				object obj2 = base.InnerReadNonOpenUndeclaredProperty(entryState.DuplicatePropertyNamesChecker, propertyName, flag);
				ODataProperty odataProperty = ODataJsonLightEntryAndFeedDeserializer.AddEntryProperty(entryState, propertyName, obj2);
				ODataJsonLightPropertyAndValueDeserializer.TryAttachRawAnnotationSetToPropertyValue(entryState.DuplicatePropertyNamesChecker, odataProperty);
			}
			else
			{
				base.JsonReader.SkipValue();
			}
			return null;
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x00027518 File Offset: 0x00025718
		private ODataStreamReferenceValue ReadStreamPropertyValue(IODataJsonLightReaderEntryState entryState, string streamPropertyName)
		{
			if (!base.ReadingResponse)
			{
				throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_StreamPropertyInRequest);
			}
			ODataVersionChecker.CheckStreamReferenceProperty(base.Version);
			ODataStreamReferenceValue odataStreamReferenceValue = new ODataStreamReferenceValue();
			Dictionary<string, object> odataPropertyAnnotations = entryState.DuplicatePropertyNamesChecker.GetODataPropertyAnnotations(streamPropertyName);
			if (odataPropertyAnnotations != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in odataPropertyAnnotations)
				{
					string key;
					if ((key = keyValuePair.Key) != null)
					{
						if (key == "odata.mediaEditLink")
						{
							odataStreamReferenceValue.EditLink = (Uri)keyValuePair.Value;
							continue;
						}
						if (key == "odata.mediaReadLink")
						{
							odataStreamReferenceValue.ReadLink = (Uri)keyValuePair.Value;
							continue;
						}
						if (key == "odata.mediaETag")
						{
							odataStreamReferenceValue.ETag = (string)keyValuePair.Value;
							continue;
						}
						if (key == "odata.mediaContentType")
						{
							odataStreamReferenceValue.ContentType = (string)keyValuePair.Value;
							continue;
						}
					}
					throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_UnexpectedStreamPropertyAnnotation(streamPropertyName, keyValuePair.Key));
				}
			}
			ODataEntityMetadataBuilder entityMetadataBuilderForReader = base.MetadataContext.GetEntityMetadataBuilderForReader(entryState);
			odataStreamReferenceValue.SetMetadataBuilder(entityMetadataBuilderForReader, streamPropertyName);
			return odataStreamReferenceValue;
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x00027660 File Offset: 0x00025860
		private void ReadSingleOperationValue(IODataJsonOperationsDeserializerContext readerContext, IODataJsonLightReaderEntryState entryState, string metadataReferencePropertyName, bool insideArray)
		{
			if (readerContext.JsonReader.NodeType != JsonNodeType.StartObject)
			{
				throw new ODataException(Strings.ODataJsonOperationsDeserializerUtils_OperationsPropertyMustHaveObjectValue(metadataReferencePropertyName, readerContext.JsonReader.NodeType));
			}
			readerContext.JsonReader.ReadStartObject();
			ODataOperation odataOperation = this.CreateODataOperationAndAddToEntry(readerContext, entryState, metadataReferencePropertyName);
			if (odataOperation == null)
			{
				while (readerContext.JsonReader.NodeType == JsonNodeType.Property)
				{
					readerContext.JsonReader.ReadPropertyName();
					readerContext.JsonReader.SkipValue();
				}
				readerContext.JsonReader.ReadEndObject();
				return;
			}
			while (readerContext.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text = readerContext.JsonReader.ReadPropertyName();
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "title"))
					{
						if (text2 == "target")
						{
							if (odataOperation.Target != null)
							{
								throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_MultipleOptionalPropertiesInOperation(text, metadataReferencePropertyName));
							}
							string text3 = readerContext.JsonReader.ReadStringValue("target");
							ODataJsonLightValidationUtils.ValidateOperationPropertyValueIsNotNull(text3, text, metadataReferencePropertyName);
							odataOperation.Target = readerContext.ProcessUriFromPayload(text3);
							continue;
						}
					}
					else
					{
						if (odataOperation.Title != null)
						{
							throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_MultipleOptionalPropertiesInOperation(text, metadataReferencePropertyName));
						}
						string text4 = readerContext.JsonReader.ReadStringValue("title");
						ODataJsonLightValidationUtils.ValidateOperationPropertyValueIsNotNull(text4, text, metadataReferencePropertyName);
						odataOperation.Title = text4;
						continue;
					}
				}
				readerContext.JsonReader.SkipValue();
			}
			if (odataOperation.Target == null && insideArray)
			{
				throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_OperationMissingTargetProperty(metadataReferencePropertyName));
			}
			readerContext.JsonReader.ReadEndObject();
			this.SetMetadataBuilder(entryState, odataOperation);
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x000277E0 File Offset: 0x000259E0
		private void SetMetadataBuilder(IODataJsonLightReaderEntryState entryState, ODataOperation operation)
		{
			ODataEntityMetadataBuilder entityMetadataBuilderForReader = base.MetadataContext.GetEntityMetadataBuilderForReader(entryState);
			operation.SetMetadataBuilder(entityMetadataBuilderForReader, base.MetadataUriParseResult.MetadataDocumentUri);
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0002780C File Offset: 0x00025A0C
		private ODataOperation CreateODataOperationAndAddToEntry(IODataJsonOperationsDeserializerContext readerContext, IODataJsonLightReaderEntryState entryState, string metadataReferencePropertyName)
		{
			string uriFragmentFromMetadataReferencePropertyName = ODataJsonLightUtils.GetUriFragmentFromMetadataReferencePropertyName(base.MetadataUriParseResult.MetadataDocumentUri, metadataReferencePropertyName);
			IEnumerable<IEdmFunctionImport> enumerable = base.JsonLightInputContext.Model.ResolveFunctionImports(uriFragmentFromMetadataReferencePropertyName);
			IEdmFunctionImport edmFunctionImport = null;
			bool flag = false;
			foreach (IEdmFunctionImport edmFunctionImport2 in enumerable)
			{
				string text;
				if (base.JsonLightInputContext.Model.TryGetODataAnnotation(edmFunctionImport2, "HttpMethod", out text))
				{
					flag = true;
				}
				else if (edmFunctionImport == null)
				{
					edmFunctionImport = edmFunctionImport2;
				}
			}
			if (edmFunctionImport != null)
			{
				bool flag2;
				ODataOperation odataOperation = ODataJsonLightUtils.CreateODataOperation(base.MetadataUriParseResult.MetadataDocumentUri, metadataReferencePropertyName, edmFunctionImport, out flag2);
				if (flag2)
				{
					readerContext.AddActionToEntry((ODataAction)odataOperation);
				}
				else
				{
					readerContext.AddFunctionToEntry((ODataFunction)odataOperation);
				}
				return odataOperation;
			}
			if (flag)
			{
				throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_FunctionImportIsNotActionOrFunction(uriFragmentFromMetadataReferencePropertyName));
			}
			return null;
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x000278EC File Offset: 0x00025AEC
		private void ReadMetadataReferencePropertyValue(IODataJsonLightReaderEntryState entryState, string metadataReferencePropertyName)
		{
			this.ValidateCanReadMetadataReferenceProperty();
			ODataJsonLightValidationUtils.ValidateMetadataReferencePropertyName(base.MetadataUriParseResult.MetadataDocumentUri, metadataReferencePropertyName);
			IODataJsonOperationsDeserializerContext iodataJsonOperationsDeserializerContext = new ODataJsonLightEntryAndFeedDeserializer.OperationsDeserializerContext(entryState.Entry, this);
			bool flag = false;
			if (iodataJsonOperationsDeserializerContext.JsonReader.NodeType == JsonNodeType.StartArray)
			{
				iodataJsonOperationsDeserializerContext.JsonReader.ReadStartArray();
				flag = true;
			}
			do
			{
				this.ReadSingleOperationValue(iodataJsonOperationsDeserializerContext, entryState, metadataReferencePropertyName, flag);
			}
			while (flag && iodataJsonOperationsDeserializerContext.JsonReader.NodeType != JsonNodeType.EndArray);
			if (flag)
			{
				iodataJsonOperationsDeserializerContext.JsonReader.ReadEndArray();
			}
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x00027963 File Offset: 0x00025B63
		private void ValidateCanReadMetadataReferenceProperty()
		{
			if (!base.ReadingResponse)
			{
				throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_MetadataReferencePropertyInRequest);
			}
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x00027978 File Offset: 0x00025B78
		private void ValidateExpandedNavigationLinkPropertyValue(bool? isCollection)
		{
			JsonNodeType nodeType = base.JsonReader.NodeType;
			if (nodeType == JsonNodeType.StartArray)
			{
				if (isCollection == false)
				{
					throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_CannotReadSingletonNavigationPropertyValue(nodeType));
				}
			}
			else
			{
				if ((nodeType != JsonNodeType.PrimitiveValue || base.JsonReader.Value != null) && nodeType != JsonNodeType.StartObject)
				{
					throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_CannotReadNavigationPropertyValue);
				}
				if (isCollection == true)
				{
					throw new ODataException(Strings.ODataJsonLightEntryAndFeedDeserializer_CannotReadCollectionNavigationPropertyValue(nodeType));
				}
			}
		}

		// Token: 0x0400041A RID: 1050
		private readonly JsonLightAnnotationGroupDeserializer annotationGroupDeserializer;

		// Token: 0x0200018E RID: 398
		private sealed class OperationsDeserializerContext : IODataJsonOperationsDeserializerContext
		{
			// Token: 0x06000B6A RID: 2922 RVA: 0x00027A05 File Offset: 0x00025C05
			public OperationsDeserializerContext(ODataEntry entry, ODataJsonLightEntryAndFeedDeserializer jsonLightEntryAndFeedDeserializer)
			{
				this.entry = entry;
				this.jsonLightEntryAndFeedDeserializer = jsonLightEntryAndFeedDeserializer;
			}

			// Token: 0x1700029C RID: 668
			// (get) Token: 0x06000B6B RID: 2923 RVA: 0x00027A1B File Offset: 0x00025C1B
			public JsonReader JsonReader
			{
				get
				{
					return this.jsonLightEntryAndFeedDeserializer.JsonReader;
				}
			}

			// Token: 0x06000B6C RID: 2924 RVA: 0x00027A28 File Offset: 0x00025C28
			public Uri ProcessUriFromPayload(string uriFromPayload)
			{
				return this.jsonLightEntryAndFeedDeserializer.ProcessUriFromPayload(uriFromPayload);
			}

			// Token: 0x06000B6D RID: 2925 RVA: 0x00027A36 File Offset: 0x00025C36
			public void AddActionToEntry(ODataAction action)
			{
				this.entry.AddAction(action);
			}

			// Token: 0x06000B6E RID: 2926 RVA: 0x00027A44 File Offset: 0x00025C44
			public void AddFunctionToEntry(ODataFunction function)
			{
				this.entry.AddFunction(function);
			}

			// Token: 0x0400041B RID: 1051
			private ODataEntry entry;

			// Token: 0x0400041C RID: 1052
			private ODataJsonLightEntryAndFeedDeserializer jsonLightEntryAndFeedDeserializer;
		}
	}
}
