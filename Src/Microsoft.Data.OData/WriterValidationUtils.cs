using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x02000249 RID: 585
	internal static class WriterValidationUtils
	{
		// Token: 0x060012B7 RID: 4791 RVA: 0x000467FC File Offset: 0x000449FC
		internal static void ValidateMessageWriterSettings(ODataMessageWriterSettings messageWriterSettings, bool writingResponse)
		{
			if (messageWriterSettings.BaseUri != null && !messageWriterSettings.BaseUri.IsAbsoluteUri)
			{
				throw new ODataException(Strings.WriterValidationUtils_MessageWriterSettingsBaseUriMustBeNullOrAbsolute(UriUtilsCommon.UriToString(messageWriterSettings.BaseUri)));
			}
			if (messageWriterSettings.HasJsonPaddingFunction() && !writingResponse)
			{
				throw new ODataException(Strings.WriterValidationUtils_MessageWriterSettingsJsonPaddingOnRequestMessage);
			}
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x00046850 File Offset: 0x00044A50
		internal static void ValidatePropertyNotNull(ODataProperty property)
		{
			if (property == null)
			{
				throw new ODataException(Strings.WriterValidationUtils_PropertyMustNotBeNull);
			}
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x00046860 File Offset: 0x00044A60
		internal static void ValidatePropertyName(string propertyName)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				throw new ODataException(Strings.WriterValidationUtils_PropertiesMustHaveNonEmptyName);
			}
			ValidationUtils.ValidatePropertyName(propertyName);
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x0004687C File Offset: 0x00044A7C
		internal static IEdmProperty ValidatePropertyDefined(string propertyName, IEdmStructuredType owningStructuredType, ODataUndeclaredPropertyBehaviorKinds undeclaredPropertyBehaviorKinds)
		{
			if (owningStructuredType == null)
			{
				return null;
			}
			bool flag = !undeclaredPropertyBehaviorKinds.HasFlag(ODataUndeclaredPropertyBehaviorKinds.IgnoreUndeclaredValueProperty) && !undeclaredPropertyBehaviorKinds.HasFlag(ODataUndeclaredPropertyBehaviorKinds.SupportUndeclaredValueProperty);
			IEdmProperty edmProperty = owningStructuredType.FindProperty(propertyName);
			if (flag && !owningStructuredType.IsOpen && edmProperty == null)
			{
				throw new ODataException(Strings.ValidationUtils_PropertyDoesNotExistOnType(propertyName, owningStructuredType.ODataFullName()));
			}
			return edmProperty;
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x000468E4 File Offset: 0x00044AE4
		internal static IEdmNavigationProperty ValidateNavigationPropertyDefined(string propertyName, IEdmEntityType owningEntityType, ODataUndeclaredPropertyBehaviorKinds undeclaredPropertyBehaviorKinds)
		{
			if (owningEntityType == null)
			{
				return null;
			}
			IEdmProperty edmProperty = WriterValidationUtils.ValidatePropertyDefined(propertyName, owningEntityType, undeclaredPropertyBehaviorKinds);
			if (edmProperty == null)
			{
				bool flag = !undeclaredPropertyBehaviorKinds.HasFlag(ODataUndeclaredPropertyBehaviorKinds.IgnoreUndeclaredValueProperty) && !undeclaredPropertyBehaviorKinds.HasFlag(ODataUndeclaredPropertyBehaviorKinds.SupportUndeclaredValueProperty);
				throw new ODataException(Strings.ValidationUtils_OpenNavigationProperty(propertyName, owningEntityType.ODataFullName()));
			}
			if (edmProperty.PropertyKind != EdmPropertyKind.Navigation)
			{
				throw new ODataException(Strings.ValidationUtils_NavigationPropertyExpected(propertyName, owningEntityType.ODataFullName(), edmProperty.PropertyKind.ToString()));
			}
			return (IEdmNavigationProperty)edmProperty;
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x00046972 File Offset: 0x00044B72
		internal static void ValidateEntryInExpandedLink(IEdmEntityType entryEntityType, IEdmEntityType parentNavigationPropertyType)
		{
			if (parentNavigationPropertyType == null)
			{
				return;
			}
			if (!parentNavigationPropertyType.IsAssignableFrom(entryEntityType))
			{
				throw new ODataException(Strings.WriterValidationUtils_EntryTypeInExpandedLinkNotCompatibleWithNavigationPropertyType(entryEntityType.ODataFullName(), parentNavigationPropertyType.ODataFullName()));
			}
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x00046998 File Offset: 0x00044B98
		internal static void ValidateAssociationLink(ODataAssociationLink associationLink, ODataVersion version, bool writingResponse)
		{
			ODataVersionChecker.CheckAssociationLinks(version);
			if (!writingResponse)
			{
				throw new ODataException(Strings.WriterValidationUtils_AssociationLinkInRequest(associationLink.Name));
			}
			ValidationUtils.ValidateAssociationLink(associationLink);
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x000469BA File Offset: 0x00044BBA
		internal static void ValidateCanWriteOperation(ODataOperation operation, bool writingResponse)
		{
			if (!writingResponse)
			{
				throw new ODataException(Strings.WriterValidationUtils_OperationInRequest(operation.Metadata));
			}
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x000469D0 File Offset: 0x00044BD0
		internal static void ValidateFeedAtEnd(ODataFeed feed, bool writingRequest, ODataVersion version)
		{
			if (feed.NextPageLink != null)
			{
				if (writingRequest)
				{
					throw new ODataException(Strings.WriterValidationUtils_NextPageLinkInRequest);
				}
				ODataVersionChecker.CheckNextLink(version);
			}
			if (feed.DeltaLink != null)
			{
				ODataVersionChecker.CheckDeltaLink(version);
			}
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x00046A08 File Offset: 0x00044C08
		internal static void ValidateEntryAtStart(ODataEntry entry)
		{
			WriterValidationUtils.ValidateEntryId(entry.Id);
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x00046A15 File Offset: 0x00044C15
		internal static void ValidateEntryAtEnd(ODataEntry entry)
		{
			WriterValidationUtils.ValidateEntryId(entry.Id);
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x00046A24 File Offset: 0x00044C24
		internal static void ValidateStreamReferenceValue(ODataStreamReferenceValue streamReference, bool isDefaultStream)
		{
			if (streamReference.ContentType != null && streamReference.ContentType.Length == 0)
			{
				throw new ODataException(Strings.WriterValidationUtils_StreamReferenceValueEmptyContentType);
			}
			if (isDefaultStream && streamReference.ReadLink == null && streamReference.ContentType != null)
			{
				throw new ODataException(Strings.WriterValidationUtils_DefaultStreamWithContentTypeWithoutReadLink);
			}
			if (isDefaultStream && streamReference.ReadLink != null && streamReference.ContentType == null)
			{
				throw new ODataException(Strings.WriterValidationUtils_DefaultStreamWithReadLinkWithoutContentType);
			}
			if (streamReference.EditLink == null && streamReference.ReadLink == null && !isDefaultStream)
			{
				throw new ODataException(Strings.WriterValidationUtils_StreamReferenceValueMustHaveEditLinkOrReadLink);
			}
			if (streamReference.EditLink == null && streamReference.ETag != null)
			{
				throw new ODataException(Strings.WriterValidationUtils_StreamReferenceValueMustHaveEditLinkToHaveETag);
			}
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x00046AE4 File Offset: 0x00044CE4
		internal static void ValidateStreamReferenceProperty(ODataProperty streamProperty, IEdmProperty edmProperty, ODataVersion version, bool writingResponse)
		{
			ODataVersionChecker.CheckStreamReferenceProperty(version);
			ValidationUtils.ValidateStreamReferenceProperty(streamProperty, edmProperty);
			if (!writingResponse)
			{
				throw new ODataException(Strings.WriterValidationUtils_StreamPropertyInRequest(streamProperty.Name));
			}
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x00046B07 File Offset: 0x00044D07
		internal static void ValidateEntityReferenceLinkNotNull(ODataEntityReferenceLink entityReferenceLink)
		{
			if (entityReferenceLink == null)
			{
				throw new ODataException(Strings.WriterValidationUtils_EntityReferenceLinksLinkMustNotBeNull);
			}
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x00046B17 File Offset: 0x00044D17
		internal static void ValidateEntityReferenceLink(ODataEntityReferenceLink entityReferenceLink)
		{
			if (entityReferenceLink.Url == null)
			{
				throw new ODataException(Strings.WriterValidationUtils_EntityReferenceLinkUrlMustNotBeNull);
			}
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x00046B34 File Offset: 0x00044D34
		internal static IEdmNavigationProperty ValidateNavigationLink(ODataNavigationLink navigationLink, IEdmEntityType declaringEntityType, ODataPayloadKind? expandedPayloadKind, ODataUndeclaredPropertyBehaviorKinds undeclaredPropertyBehaviorKinds)
		{
			if (string.IsNullOrEmpty(navigationLink.Name))
			{
				throw new ODataException(Strings.ValidationUtils_LinkMustSpecifyName);
			}
			bool flag = expandedPayloadKind == ODataPayloadKind.EntityReferenceLink;
			bool flag2 = expandedPayloadKind == ODataPayloadKind.Feed;
			Func<object, string> func = null;
			if (!flag && navigationLink.IsCollection != null && expandedPayloadKind != null && flag2 != navigationLink.IsCollection.Value)
			{
				func = ((expandedPayloadKind.Value == ODataPayloadKind.Feed) ? new Func<object, string>(Strings.WriterValidationUtils_ExpandedLinkIsCollectionFalseWithFeedContent) : new Func<object, string>(Strings.WriterValidationUtils_ExpandedLinkIsCollectionTrueWithEntryContent));
			}
			IEdmNavigationProperty edmNavigationProperty = null;
			if (func == null && declaringEntityType != null)
			{
				edmNavigationProperty = WriterValidationUtils.ValidateNavigationPropertyDefined(navigationLink.Name, declaringEntityType, undeclaredPropertyBehaviorKinds);
				bool flag3 = edmNavigationProperty.Type.TypeKind() == EdmTypeKind.Collection;
				if (navigationLink.IsCollection != null && flag3 != navigationLink.IsCollection && (!(navigationLink.IsCollection == false) || !flag))
				{
					func = (flag3 ? new Func<object, string>(Strings.WriterValidationUtils_ExpandedLinkIsCollectionFalseWithFeedMetadata) : new Func<object, string>(Strings.WriterValidationUtils_ExpandedLinkIsCollectionTrueWithEntryMetadata));
				}
				if (!flag && expandedPayloadKind != null && flag3 != flag2)
				{
					func = (flag3 ? new Func<object, string>(Strings.WriterValidationUtils_ExpandedLinkWithEntryPayloadAndFeedMetadata) : new Func<object, string>(Strings.WriterValidationUtils_ExpandedLinkWithFeedPayloadAndEntryMetadata));
				}
			}
			if (func != null)
			{
				string text = ((navigationLink.Url == null) ? "null" : UriUtilsCommon.UriToString(navigationLink.Url));
				throw new ODataException(func(text));
			}
			return edmNavigationProperty;
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x00046CE3 File Offset: 0x00044EE3
		internal static void ValidateNavigationLinkUrlPresent(ODataNavigationLink navigationLink)
		{
			if (navigationLink.Url == null)
			{
				throw new ODataException(Strings.WriterValidationUtils_NavigationLinkMustSpecifyUrl(navigationLink.Name));
			}
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x00046D04 File Offset: 0x00044F04
		internal static void ValidateNavigationLinkHasCardinality(ODataNavigationLink navigationLink)
		{
			if (navigationLink.IsCollection == null)
			{
				throw new ODataException(Strings.WriterValidationUtils_NavigationLinkMustSpecifyIsCollection(navigationLink.Name));
			}
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x00046D34 File Offset: 0x00044F34
		internal static void ValidateNullPropertyValue(IEdmTypeReference expectedPropertyTypeReference, string propertyName, ODataWriterBehavior writerBehavior, IEdmModel model)
		{
			if (expectedPropertyTypeReference != null)
			{
				if (expectedPropertyTypeReference.IsNonEntityCollectionType())
				{
					throw new ODataException(Strings.WriterValidationUtils_CollectionPropertiesMustNotHaveNullValue(propertyName));
				}
				if (expectedPropertyTypeReference.IsODataPrimitiveTypeKind())
				{
					if (!expectedPropertyTypeReference.IsNullable && !writerBehavior.AllowNullValuesForNonNullablePrimitiveTypes)
					{
						throw new ODataException(Strings.WriterValidationUtils_NonNullablePropertiesMustNotHaveNullValue(propertyName, expectedPropertyTypeReference.ODataFullName()));
					}
				}
				else
				{
					if (expectedPropertyTypeReference.IsStream())
					{
						throw new ODataException(Strings.WriterValidationUtils_StreamPropertiesMustNotHaveNullValue(propertyName));
					}
					if (expectedPropertyTypeReference.IsODataComplexTypeKind() && ValidationUtils.ShouldValidateComplexPropertyNullValue(model))
					{
						IEdmComplexTypeReference edmComplexTypeReference = expectedPropertyTypeReference.AsComplex();
						if (!edmComplexTypeReference.IsNullable)
						{
							throw new ODataException(Strings.WriterValidationUtils_NonNullablePropertiesMustNotHaveNullValue(propertyName, expectedPropertyTypeReference.ODataFullName()));
						}
					}
				}
			}
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x00046DCA File Offset: 0x00044FCA
		private static void ValidateEntryId(string id)
		{
			if (id != null && id.Length == 0)
			{
				throw new ODataException(Strings.WriterValidationUtils_EntriesMustHaveNonEmptyId);
			}
		}
	}
}
