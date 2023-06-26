using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Json;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x0200018B RID: 395
	internal sealed class ODataJsonLightEntityReferenceLinkDeserializer : ODataJsonLightDeserializer
	{
		// Token: 0x06000B32 RID: 2866 RVA: 0x00025330 File Offset: 0x00023530
		internal ODataJsonLightEntityReferenceLinkDeserializer(ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0002533C File Offset: 0x0002353C
		internal ODataEntityReferenceLinks ReadEntityReferenceLinks(IEdmNavigationProperty navigationProperty)
		{
			DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
			base.ReadPayloadStart(ODataPayloadKind.EntityReferenceLinks, duplicatePropertyNamesChecker, false, false);
			ODataEntityReferenceLinks odataEntityReferenceLinks = this.ReadEntityReferenceLinksImplementation(navigationProperty, duplicatePropertyNamesChecker);
			base.ReadPayloadEnd(false);
			return odataEntityReferenceLinks;
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x000253A8 File Offset: 0x000235A8
		internal Task<ODataEntityReferenceLinks> ReadEntityReferenceLinksAsync(IEdmNavigationProperty navigationProperty)
		{
			DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
			return base.ReadPayloadStartAsync(ODataPayloadKind.EntityReferenceLinks, duplicatePropertyNamesChecker, false, false).FollowOnSuccessWith(delegate(Task t)
			{
				ODataEntityReferenceLinks odataEntityReferenceLinks = this.ReadEntityReferenceLinksImplementation(navigationProperty, duplicatePropertyNamesChecker);
				this.ReadPayloadEnd(false);
				return odataEntityReferenceLinks;
			});
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x000253F8 File Offset: 0x000235F8
		internal ODataEntityReferenceLink ReadEntityReferenceLink(IEdmNavigationProperty navigationProperty)
		{
			DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
			base.ReadPayloadStart(ODataPayloadKind.EntityReferenceLink, duplicatePropertyNamesChecker, false, false);
			ODataEntityReferenceLink odataEntityReferenceLink = this.ReadEntityReferenceLinkImplementation(navigationProperty, duplicatePropertyNamesChecker);
			base.ReadPayloadEnd(false);
			return odataEntityReferenceLink;
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00025464 File Offset: 0x00023664
		internal Task<ODataEntityReferenceLink> ReadEntityReferenceLinkAsync(IEdmNavigationProperty navigationProperty)
		{
			DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
			return base.ReadPayloadStartAsync(ODataPayloadKind.EntityReferenceLink, duplicatePropertyNamesChecker, false, false).FollowOnSuccessWith(delegate(Task t)
			{
				ODataEntityReferenceLink odataEntityReferenceLink = this.ReadEntityReferenceLinkImplementation(navigationProperty, duplicatePropertyNamesChecker);
				this.ReadPayloadEnd(false);
				return odataEntityReferenceLink;
			});
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x000254B4 File Offset: 0x000236B4
		private ODataEntityReferenceLinks ReadEntityReferenceLinksImplementation(IEdmNavigationProperty navigationProperty, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			ODataEntityReferenceLinks odataEntityReferenceLinks = new ODataEntityReferenceLinks();
			if (base.JsonLightInputContext.ReadingResponse)
			{
				ReaderValidationUtils.ValidateEntityReferenceLinkMetadataUri(base.MetadataUriParseResult, navigationProperty);
			}
			this.ReadEntityReferenceLinksAnnotations(odataEntityReferenceLinks, duplicatePropertyNamesChecker, true);
			base.JsonReader.ReadStartArray();
			List<ODataEntityReferenceLink> list = new List<ODataEntityReferenceLink>();
			DuplicatePropertyNamesChecker duplicatePropertyNamesChecker2 = base.JsonLightInputContext.CreateDuplicatePropertyNamesChecker();
			while (base.JsonReader.NodeType != JsonNodeType.EndArray)
			{
				ODataEntityReferenceLink odataEntityReferenceLink = this.ReadSingleEntityReferenceLink(duplicatePropertyNamesChecker2, false);
				list.Add(odataEntityReferenceLink);
				duplicatePropertyNamesChecker2.Clear();
			}
			base.JsonReader.ReadEndArray();
			this.ReadEntityReferenceLinksAnnotations(odataEntityReferenceLinks, duplicatePropertyNamesChecker, false);
			base.JsonReader.ReadEndObject();
			odataEntityReferenceLinks.Links = new ReadOnlyEnumerable<ODataEntityReferenceLink>(list);
			return odataEntityReferenceLinks;
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x00025558 File Offset: 0x00023758
		private ODataEntityReferenceLink ReadEntityReferenceLinkImplementation(IEdmNavigationProperty navigationProperty, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			if (base.JsonLightInputContext.ReadingResponse)
			{
				ReaderValidationUtils.ValidateEntityReferenceLinkMetadataUri(base.MetadataUriParseResult, navigationProperty);
			}
			return this.ReadSingleEntityReferenceLink(duplicatePropertyNamesChecker, true);
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x00025690 File Offset: 0x00023890
		private void ReadEntityReferenceLinksAnnotations(ODataEntityReferenceLinks links, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, bool forLinksStart)
		{
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				Func<string, object> func = delegate(string annotationName)
				{
					throw new ODataException(Strings.ODataJsonLightEntityReferenceLinkDeserializer_PropertyAnnotationForEntityReferenceLinks);
				};
				bool foundValueProperty = false;
				base.ProcessProperty(duplicatePropertyNamesChecker, func, delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParseResult, string propertyName)
				{
					switch (propertyParseResult)
					{
					case ODataJsonLightDeserializer.PropertyParsingResult.EndOfObject:
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithValue:
						if (string.CompareOrdinal("value", propertyName) != 0)
						{
							throw new ODataException(Strings.ODataJsonLightEntityReferenceLinkDeserializer_InvalidEntityReferenceLinksPropertyFound(propertyName, "value"));
						}
						foundValueProperty = true;
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue:
						throw new ODataException(Strings.ODataJsonLightEntityReferenceLinkDeserializer_InvalidPropertyAnnotationInEntityReferenceLinks(propertyName));
					case ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation:
						if (string.CompareOrdinal("odata.nextLink", propertyName) == 0)
						{
							this.ReadEntityReferenceLinksNextLinkAnnotationValue(links);
							return;
						}
						if (string.CompareOrdinal("odata.count", propertyName) == 0)
						{
							this.ReadEntityReferenceCountAnnotationValue(links);
							return;
						}
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedAnnotationProperties(propertyName));
					case ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation:
						this.JsonReader.SkipValue();
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.MetadataReferenceProperty:
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedMetadataReferenceProperty(propertyName));
					default:
						throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataJsonLightEntityReferenceLinkDeserializer_ReadEntityReferenceLinksAnnotations));
					}
				});
				if (foundValueProperty)
				{
					return;
				}
			}
			if (forLinksStart)
			{
				throw new ODataException(Strings.ODataJsonLightEntityReferenceLinkDeserializer_ExpectedEntityReferenceLinksPropertyNotFound("value"));
			}
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00025724 File Offset: 0x00023924
		private void ReadEntityReferenceLinksNextLinkAnnotationValue(ODataEntityReferenceLinks links)
		{
			Uri uri = base.ReadAndValidateAnnotationStringValueAsUri("odata.nextLink");
			links.NextPageLink = uri;
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00025744 File Offset: 0x00023944
		private void ReadEntityReferenceCountAnnotationValue(ODataEntityReferenceLinks links)
		{
			links.Count = new long?(base.ReadAndValidateAnnotationStringValueAsLong("odata.count"));
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x00025880 File Offset: 0x00023A80
		private ODataEntityReferenceLink ReadSingleEntityReferenceLink(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, bool topLevel)
		{
			ODataJsonLightEntityReferenceLinkDeserializer.<>c__DisplayClass12 CS$<>8__locals1 = new ODataJsonLightEntityReferenceLinkDeserializer.<>c__DisplayClass12();
			CS$<>8__locals1.<>4__this = this;
			if (!topLevel)
			{
				if (base.JsonReader.NodeType != JsonNodeType.StartObject)
				{
					throw new ODataException(Strings.ODataJsonLightEntityReferenceLinkDeserializer_EntityReferenceLinkMustBeObjectValue(base.JsonReader.NodeType));
				}
				base.JsonReader.ReadStartObject();
			}
			ODataJsonLightEntityReferenceLinkDeserializer.<>c__DisplayClass12 CS$<>8__locals2 = CS$<>8__locals1;
			ODataEntityReferenceLink[] array = new ODataEntityReferenceLink[1];
			CS$<>8__locals2.entityReferenceLink = array;
			Func<string, object> func = delegate(string annotationName)
			{
				throw new ODataException(Strings.ODataJsonLightEntityReferenceLinkDeserializer_PropertyAnnotationForEntityReferenceLink(annotationName));
			};
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				base.ProcessProperty(duplicatePropertyNamesChecker, func, delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult, string propertyName)
				{
					switch (propertyParsingResult)
					{
					case ODataJsonLightDeserializer.PropertyParsingResult.EndOfObject:
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithValue:
					{
						if (string.CompareOrdinal("url", propertyName) != 0)
						{
							throw new ODataException(Strings.ODataJsonLightEntityReferenceLinkDeserializer_InvalidPropertyInEntityReferenceLink(propertyName, "url"));
						}
						if (CS$<>8__locals1.entityReferenceLink[0] != null)
						{
							throw new ODataException(Strings.ODataJsonLightEntityReferenceLinkDeserializer_MultipleUriPropertiesInEntityReferenceLink("url"));
						}
						string text = CS$<>8__locals1.<>4__this.JsonReader.ReadStringValue("url");
						if (text == null)
						{
							throw new ODataException(Strings.ODataJsonLightEntityReferenceLinkDeserializer_EntityReferenceLinkUrlCannotBeNull("url"));
						}
						CS$<>8__locals1.entityReferenceLink[0] = new ODataEntityReferenceLink
						{
							Url = CS$<>8__locals1.<>4__this.ProcessUriFromPayload(text)
						};
						ReaderValidationUtils.ValidateEntityReferenceLink(CS$<>8__locals1.entityReferenceLink[0]);
						return;
					}
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue:
						throw new ODataException(Strings.ODataJsonLightEntityReferenceLinkDeserializer_InvalidAnnotationInEntityReferenceLink(propertyName));
					case ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation:
						throw new ODataException(Strings.ODataJsonLightEntityReferenceLinkDeserializer_InvalidAnnotationInEntityReferenceLink(propertyName));
					case ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation:
						CS$<>8__locals1.<>4__this.JsonReader.SkipValue();
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.MetadataReferenceProperty:
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedMetadataReferenceProperty(propertyName));
					default:
						throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataJsonLightEntityReferenceLinkDeserializer_ReadSingleEntityReferenceLink));
					}
				});
			}
			if (CS$<>8__locals1.entityReferenceLink[0] == null)
			{
				throw new ODataException(Strings.ODataJsonLightEntityReferenceLinkDeserializer_MissingEntityReferenceLinkProperty("url"));
			}
			base.JsonReader.ReadEndObject();
			return CS$<>8__locals1.entityReferenceLink[0];
		}
	}
}
