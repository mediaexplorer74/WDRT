using System;
using System.Text;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.JsonLight;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x02000244 RID: 580
	internal static class ReaderValidationUtils
	{
		// Token: 0x06001289 RID: 4745 RVA: 0x00045C00 File Offset: 0x00043E00
		internal static void ValidateMessageReaderSettings(ODataMessageReaderSettings messageReaderSettings, bool readingResponse)
		{
			if (messageReaderSettings.BaseUri != null && !messageReaderSettings.BaseUri.IsAbsoluteUri)
			{
				throw new ODataException(Strings.ReaderValidationUtils_MessageReaderSettingsBaseUriMustBeNullOrAbsolute(UriUtilsCommon.UriToString(messageReaderSettings.BaseUri)));
			}
			if (!readingResponse && messageReaderSettings.UndeclaredPropertyBehaviorKinds != ODataUndeclaredPropertyBehaviorKinds.None)
			{
				throw new ODataException(Strings.ReaderValidationUtils_UndeclaredPropertyBehaviorKindSpecifiedOnRequest);
			}
			if (!string.IsNullOrEmpty(messageReaderSettings.ReaderBehavior.ODataTypeScheme) && !string.Equals(messageReaderSettings.ReaderBehavior.ODataTypeScheme, "http://schemas.microsoft.com/ado/2007/08/dataservices/scheme"))
			{
				ODataVersionChecker.CheckCustomTypeScheme(messageReaderSettings.MaxProtocolVersion);
			}
			if (!string.IsNullOrEmpty(messageReaderSettings.ReaderBehavior.ODataNamespace) && !string.Equals(messageReaderSettings.ReaderBehavior.ODataNamespace, "http://schemas.microsoft.com/ado/2007/08/dataservices"))
			{
				ODataVersionChecker.CheckCustomDataNamespace(messageReaderSettings.MaxProtocolVersion);
			}
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x00045CBC File Offset: 0x00043EBC
		internal static void ValidateEntityReferenceLink(ODataEntityReferenceLink link)
		{
			if (link.Url == null)
			{
				throw new ODataException(Strings.ReaderValidationUtils_EntityReferenceLinkMissingUri);
			}
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x00045CD7 File Offset: 0x00043ED7
		internal static void ValidateStreamReferenceProperty(ODataProperty streamProperty, IEdmStructuredType structuredType, IEdmProperty streamEdmProperty, ODataMessageReaderSettings messageReaderSettings)
		{
			ValidationUtils.ValidateStreamReferenceProperty(streamProperty, streamEdmProperty);
			if (structuredType != null && structuredType.IsOpen && streamEdmProperty == null && !messageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.ReportUndeclaredLinkProperty))
			{
				ValidationUtils.ValidateOpenPropertyValue(streamProperty.Name, streamProperty.Value, messageReaderSettings.UndeclaredPropertyBehaviorKinds);
			}
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x00045D0E File Offset: 0x00043F0E
		internal static void ValidateNullValue(IEdmModel model, IEdmTypeReference expectedTypeReference, ODataMessageReaderSettings messageReaderSettings, bool validateNullValue, ODataVersion version, string propertyName)
		{
			if (expectedTypeReference != null)
			{
				ReaderValidationUtils.ValidateTypeSupported(expectedTypeReference, version);
				if (!messageReaderSettings.DisablePrimitiveTypeConversion || expectedTypeReference.TypeKind() != EdmTypeKind.Primitive)
				{
					ReaderValidationUtils.ValidateNullValueAllowed(expectedTypeReference, validateNullValue, model, propertyName);
				}
			}
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x00045D36 File Offset: 0x00043F36
		internal static void ValidateEntry(ODataEntry entry)
		{
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x00045D38 File Offset: 0x00043F38
		internal static IEdmProperty FindDefinedProperty(string propertyName, IEdmStructuredType owningStructuredType)
		{
			if (owningStructuredType == null)
			{
				return null;
			}
			return owningStructuredType.FindProperty(propertyName);
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x00045D54 File Offset: 0x00043F54
		internal static IEdmProperty ValidateValuePropertyDefined(string propertyName, IEdmStructuredType owningStructuredType, ODataMessageReaderSettings messageReaderSettings, out bool ignoreProperty)
		{
			ignoreProperty = false;
			if (owningStructuredType == null)
			{
				return null;
			}
			IEdmProperty edmProperty = ReaderValidationUtils.FindDefinedProperty(propertyName, owningStructuredType);
			if (edmProperty == null && !owningStructuredType.IsOpen)
			{
				if (messageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.IgnoreUndeclaredValueProperty))
				{
					ignoreProperty = true;
				}
				else if (!messageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.SupportUndeclaredValueProperty))
				{
					throw new ODataException(Strings.ValidationUtils_PropertyDoesNotExistOnType(propertyName, owningStructuredType.ODataFullName()));
				}
			}
			return edmProperty;
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x00045DA6 File Offset: 0x00043FA6
		internal static void ValidateExpectedPropertyName(string expectedPropertyName, string payloadPropertyName)
		{
			if (expectedPropertyName != null && string.CompareOrdinal(expectedPropertyName, payloadPropertyName) != 0)
			{
				throw new ODataException(Strings.ReaderValidationUtils_NonMatchingPropertyNames(payloadPropertyName, expectedPropertyName));
			}
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x00045DC4 File Offset: 0x00043FC4
		internal static IEdmProperty ValidateLinkPropertyDefined(string propertyName, IEdmStructuredType owningStructuredType, ODataMessageReaderSettings messageReaderSettings)
		{
			if (owningStructuredType == null)
			{
				return null;
			}
			IEdmProperty edmProperty = ReaderValidationUtils.FindDefinedProperty(propertyName, owningStructuredType);
			if (edmProperty == null && !owningStructuredType.IsOpen && !messageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.ReportUndeclaredLinkProperty))
			{
				throw new ODataException(Strings.ValidationUtils_PropertyDoesNotExistOnType(propertyName, owningStructuredType.ODataFullName()));
			}
			return edmProperty;
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x00045E08 File Offset: 0x00044008
		internal static IEdmNavigationProperty ValidateNavigationPropertyDefined(string propertyName, IEdmEntityType owningEntityType, ODataMessageReaderSettings messageReaderSettings)
		{
			if (owningEntityType == null)
			{
				return null;
			}
			IEdmProperty edmProperty = ReaderValidationUtils.ValidateLinkPropertyDefined(propertyName, owningEntityType, messageReaderSettings);
			if (edmProperty == null)
			{
				if (owningEntityType.IsOpen && !messageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.ReportUndeclaredLinkProperty))
				{
					throw new ODataException(Strings.ValidationUtils_OpenNavigationProperty(propertyName, owningEntityType.ODataFullName()));
				}
			}
			else if (edmProperty.PropertyKind != EdmPropertyKind.Navigation)
			{
				throw new ODataException(Strings.ValidationUtils_NavigationPropertyExpected(propertyName, owningEntityType.ODataFullName(), edmProperty.PropertyKind.ToString()));
			}
			return (IEdmNavigationProperty)edmProperty;
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x00045E7A File Offset: 0x0004407A
		internal static ODataException GetPrimitiveTypeConversionException(IEdmPrimitiveTypeReference targetTypeReference, Exception innerException)
		{
			return new ODataException(Strings.ReaderValidationUtils_CannotConvertPrimitiveValue(targetTypeReference.ODataFullName()), innerException);
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x00045E90 File Offset: 0x00044090
		internal static IEdmType ResolvePayloadTypeName(IEdmModel model, IEdmTypeReference expectedTypeReference, string payloadTypeName, EdmTypeKind expectedTypeKind, ODataReaderBehavior readerBehavior, ODataVersion version, out EdmTypeKind payloadTypeKind)
		{
			if (payloadTypeName == null)
			{
				payloadTypeKind = EdmTypeKind.None;
				return null;
			}
			if (payloadTypeName.Length == 0)
			{
				payloadTypeKind = expectedTypeKind;
				return null;
			}
			IEdmType edmType = MetadataUtils.ResolveTypeNameForRead(model, (expectedTypeReference == null) ? null : expectedTypeReference.Definition, payloadTypeName, readerBehavior, version, out payloadTypeKind);
			if (payloadTypeKind == EdmTypeKind.None)
			{
				payloadTypeKind = expectedTypeKind;
			}
			return edmType;
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x00045ED8 File Offset: 0x000440D8
		internal static IEdmTypeReference ResolvePayloadTypeNameAndComputeTargetType(EdmTypeKind expectedTypeKind, IEdmType defaultPrimitivePayloadType, IEdmTypeReference expectedTypeReference, string payloadTypeName, IEdmModel model, ODataMessageReaderSettings messageReaderSettings, ODataVersion version, Func<EdmTypeKind> typeKindPeekedFromPayloadFunc, out EdmTypeKind targetTypeKind, out SerializationTypeNameAnnotation serializationTypeNameAnnotation)
		{
			serializationTypeNameAnnotation = null;
			EdmTypeKind edmTypeKind;
			IEdmType edmType = ReaderValidationUtils.ResolvePayloadTypeName(model, expectedTypeReference, payloadTypeName, EdmTypeKind.Complex, messageReaderSettings.ReaderBehavior, version, out edmTypeKind);
			targetTypeKind = ReaderValidationUtils.ComputeTargetTypeKind(expectedTypeReference, expectedTypeKind == EdmTypeKind.Entity, payloadTypeName, edmTypeKind, messageReaderSettings, typeKindPeekedFromPayloadFunc);
			IEdmTypeReference edmTypeReference;
			if (targetTypeKind == EdmTypeKind.Primitive)
			{
				edmTypeReference = ReaderValidationUtils.ResolveAndValidatePrimitiveTargetType(expectedTypeReference, edmTypeKind, edmType, payloadTypeName, defaultPrimitivePayloadType, model, messageReaderSettings, version);
			}
			else
			{
				edmTypeReference = ReaderValidationUtils.ResolveAndValidateNonPrimitiveTargetType(targetTypeKind, expectedTypeReference, edmTypeKind, edmType, payloadTypeName, model, messageReaderSettings, version);
				if (edmTypeReference != null)
				{
					serializationTypeNameAnnotation = ReaderValidationUtils.CreateSerializationTypeNameAnnotation(payloadTypeName, edmTypeReference);
				}
			}
			if (expectedTypeKind != EdmTypeKind.None && edmTypeReference != null)
			{
				ValidationUtils.ValidateTypeKind(targetTypeKind, expectedTypeKind, payloadTypeName);
			}
			return edmTypeReference;
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x00045F5C File Offset: 0x0004415C
		internal static IEdmTypeReference ResolveAndValidatePrimitiveTargetType(IEdmTypeReference expectedTypeReference, EdmTypeKind payloadTypeKind, IEdmType payloadType, string payloadTypeName, IEdmType defaultPayloadType, IEdmModel model, ODataMessageReaderSettings messageReaderSettings, ODataVersion version)
		{
			bool flag = messageReaderSettings.ReaderBehavior.TypeResolver != null && payloadType != null;
			if (expectedTypeReference != null && !flag)
			{
				ReaderValidationUtils.ValidateTypeSupported(expectedTypeReference, version);
			}
			if (payloadTypeKind != EdmTypeKind.None && (messageReaderSettings.DisablePrimitiveTypeConversion || !messageReaderSettings.DisableStrictMetadataValidation))
			{
				ValidationUtils.ValidateTypeKind(payloadTypeKind, EdmTypeKind.Primitive, payloadTypeName);
			}
			if (!model.IsUserModel())
			{
				return MetadataUtils.GetNullablePayloadTypeReference(payloadType ?? defaultPayloadType);
			}
			if (expectedTypeReference == null || flag || messageReaderSettings.DisablePrimitiveTypeConversion)
			{
				return MetadataUtils.GetNullablePayloadTypeReference(payloadType ?? defaultPayloadType);
			}
			if (messageReaderSettings.DisableStrictMetadataValidation)
			{
				return expectedTypeReference;
			}
			if (payloadType != null && !MetadataUtilsCommon.CanConvertPrimitiveTypeTo((IEdmPrimitiveType)payloadType, (IEdmPrimitiveType)expectedTypeReference.Definition))
			{
				throw new ODataException(Strings.ValidationUtils_IncompatibleType(payloadTypeName, expectedTypeReference.ODataFullName()));
			}
			return expectedTypeReference;
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x00046018 File Offset: 0x00044218
		internal static IEdmTypeReference ResolveAndValidateNonPrimitiveTargetType(EdmTypeKind expectedTypeKind, IEdmTypeReference expectedTypeReference, EdmTypeKind payloadTypeKind, IEdmType payloadType, string payloadTypeName, IEdmModel model, ODataMessageReaderSettings messageReaderSettings, ODataVersion version)
		{
			bool flag = messageReaderSettings.ReaderBehavior.TypeResolver != null && payloadType != null;
			if (!flag)
			{
				ReaderValidationUtils.ValidateTypeSupported(expectedTypeReference, version);
				if (model.IsUserModel() && (expectedTypeReference == null || !messageReaderSettings.DisableStrictMetadataValidation))
				{
					ReaderValidationUtils.VerifyPayloadTypeDefined(payloadTypeName, payloadType);
				}
			}
			else
			{
				ReaderValidationUtils.ValidateTypeSupported((payloadType == null) ? null : payloadType.ToTypeReference(true), version);
			}
			if (payloadTypeKind != EdmTypeKind.None && (!messageReaderSettings.DisableStrictMetadataValidation || expectedTypeReference == null))
			{
				ValidationUtils.ValidateTypeKind(payloadTypeKind, expectedTypeKind, payloadTypeName);
			}
			if (!model.IsUserModel())
			{
				return null;
			}
			if (expectedTypeReference == null || flag)
			{
				return ReaderValidationUtils.ResolveAndValidateTargetTypeWithNoExpectedType(expectedTypeKind, payloadType, messageReaderSettings.UndeclaredPropertyBehaviorKinds);
			}
			if (messageReaderSettings.DisableStrictMetadataValidation)
			{
				return ReaderValidationUtils.ResolveAndValidateTargetTypeStrictValidationDisabled(expectedTypeKind, expectedTypeReference, payloadType);
			}
			return ReaderValidationUtils.ResolveAndValidateTargetTypeStrictValidationEnabled(expectedTypeKind, expectedTypeReference, payloadType);
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x000460CE File Offset: 0x000442CE
		internal static void ValidateEncodingSupportedInBatch(Encoding encoding)
		{
			if (!encoding.IsSingleByte && Encoding.UTF8.CodePage != encoding.CodePage)
			{
				throw new ODataException(Strings.ODataBatchReaderStream_MultiByteEncodingsNotSupported(encoding.WebName));
			}
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x000460FB File Offset: 0x000442FB
		internal static void ValidateTypeSupported(IEdmTypeReference typeReference, ODataVersion version)
		{
			if (typeReference != null)
			{
				if (typeReference.IsNonEntityCollectionType())
				{
					ODataVersionChecker.CheckCollectionValue(version);
					return;
				}
				if (typeReference.IsSpatial())
				{
					ODataVersionChecker.CheckSpatialValue(version);
				}
			}
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x00046120 File Offset: 0x00044320
		internal static void ValidateFeedOrEntryMetadataUri(ODataJsonLightMetadataUriParseResult metadataUriParseResult, ODataReaderCore.Scope scope)
		{
			if (scope.EntitySet == null)
			{
				scope.EntitySet = metadataUriParseResult.EntitySet;
			}
			else if (string.CompareOrdinal(scope.EntitySet.FullName(), metadataUriParseResult.EntitySet.FullName()) != 0)
			{
				throw new ODataException(Strings.ReaderValidationUtils_MetadataUriValidationInvalidExpectedEntitySet(UriUtilsCommon.UriToString(metadataUriParseResult.MetadataUri), metadataUriParseResult.EntitySet.FullName(), scope.EntitySet.FullName()));
			}
			IEdmEntityType edmEntityType = (IEdmEntityType)metadataUriParseResult.EdmType;
			if (scope.EntityType == null)
			{
				scope.EntityType = edmEntityType;
				return;
			}
			if (scope.EntityType.IsAssignableFrom(edmEntityType))
			{
				scope.EntityType = edmEntityType;
				return;
			}
			if (!edmEntityType.IsAssignableFrom(scope.EntityType))
			{
				throw new ODataException(Strings.ReaderValidationUtils_MetadataUriValidationInvalidExpectedEntityType(UriUtilsCommon.UriToString(metadataUriParseResult.MetadataUri), metadataUriParseResult.EdmType.ODataFullName(), scope.EntityType.FullName()));
			}
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x000461F8 File Offset: 0x000443F8
		internal static void ValidateEntityReferenceLinkMetadataUri(ODataJsonLightMetadataUriParseResult metadataUriParseResult, IEdmNavigationProperty navigationProperty)
		{
			if (navigationProperty == null)
			{
				return;
			}
			IEdmNavigationProperty navigationProperty2 = metadataUriParseResult.NavigationProperty;
			if (string.CompareOrdinal(navigationProperty.Name, navigationProperty2.Name) != 0)
			{
				throw new ODataException(Strings.ReaderValidationUtils_MetadataUriValidationNonMatchingPropertyNames(UriUtilsCommon.UriToString(metadataUriParseResult.MetadataUri), navigationProperty2.Name, navigationProperty2.DeclaringEntityType().FullName(), navigationProperty.Name));
			}
			if (!navigationProperty.DeclaringType.IsEquivalentTo(navigationProperty2.DeclaringType))
			{
				throw new ODataException(Strings.ReaderValidationUtils_MetadataUriValidationNonMatchingDeclaringTypes(UriUtilsCommon.UriToString(metadataUriParseResult.MetadataUri), navigationProperty2.Name, navigationProperty2.DeclaringEntityType().FullName(), navigationProperty.DeclaringEntityType().FullName()));
			}
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x00046298 File Offset: 0x00044498
		internal static IEdmTypeReference ValidateCollectionMetadataUriAndGetPayloadItemTypeReference(ODataJsonLightMetadataUriParseResult metadataUriParseResult, IEdmTypeReference expectedItemTypeReference)
		{
			if (metadataUriParseResult == null)
			{
				return expectedItemTypeReference;
			}
			IEdmCollectionType edmCollectionType = (IEdmCollectionType)metadataUriParseResult.EdmType;
			if (expectedItemTypeReference != null && !expectedItemTypeReference.IsAssignableFrom(edmCollectionType.ElementType))
			{
				throw new ODataException(Strings.ReaderValidationUtils_MetadataUriDoesNotReferTypeAssignableToExpectedType(UriUtilsCommon.UriToString(metadataUriParseResult.MetadataUri), edmCollectionType.ElementType.ODataFullName(), expectedItemTypeReference.ODataFullName()));
			}
			return edmCollectionType.ElementType;
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x000462F4 File Offset: 0x000444F4
		internal static void ValidateOperationProperty(object propertyValue, string propertyName, string metadata, string operationsHeader)
		{
			if (propertyValue == null)
			{
				throw new ODataException(Strings.ODataJsonOperationsDeserializerUtils_OperationPropertyCannotBeNull(propertyName, metadata, operationsHeader));
			}
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x00046308 File Offset: 0x00044508
		private static IEdmTypeReference ResolveAndValidateTargetTypeWithNoExpectedType(EdmTypeKind expectedTypeKind, IEdmType payloadType, ODataUndeclaredPropertyBehaviorKinds undeclaredPropertyBehaviorKinds)
		{
			if (payloadType != null)
			{
				return payloadType.ToTypeReference(true);
			}
			if (expectedTypeKind == EdmTypeKind.Entity)
			{
				throw new ODataException(Strings.ReaderValidationUtils_EntryWithoutType);
			}
			if (undeclaredPropertyBehaviorKinds.HasFlag(ODataUndeclaredPropertyBehaviorKinds.IgnoreUndeclaredValueProperty) || undeclaredPropertyBehaviorKinds.HasFlag(ODataUndeclaredPropertyBehaviorKinds.SupportUndeclaredValueProperty))
			{
				return null;
			}
			throw new ODataException(Strings.ReaderValidationUtils_ValueWithoutType);
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x0004636C File Offset: 0x0004456C
		private static IEdmTypeReference ResolveAndValidateTargetTypeStrictValidationDisabled(EdmTypeKind expectedTypeKind, IEdmTypeReference expectedTypeReference, IEdmType payloadType)
		{
			switch (expectedTypeKind)
			{
			case EdmTypeKind.Entity:
				if (payloadType != null && expectedTypeKind == payloadType.TypeKind && expectedTypeReference.AsEntity().EntityDefinition().IsAssignableFrom((IEdmEntityType)payloadType))
				{
					return payloadType.ToTypeReference(true);
				}
				return expectedTypeReference;
			case EdmTypeKind.Complex:
				if (payloadType != null && expectedTypeKind == payloadType.TypeKind)
				{
					ReaderValidationUtils.VerifyComplexType(expectedTypeReference, payloadType, false);
					return expectedTypeReference;
				}
				return expectedTypeReference;
			case EdmTypeKind.Collection:
				if (payloadType != null && expectedTypeKind == payloadType.TypeKind)
				{
					ReaderValidationUtils.VerifyCollectionComplexItemType(expectedTypeReference, payloadType);
					return expectedTypeReference;
				}
				return expectedTypeReference;
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ReaderValidationUtils_ResolveAndValidateTypeName_Strict_TypeKind));
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x00046404 File Offset: 0x00044604
		private static IEdmTypeReference ResolveAndValidateTargetTypeStrictValidationEnabled(EdmTypeKind expectedTypeKind, IEdmTypeReference expectedTypeReference, IEdmType payloadType)
		{
			switch (expectedTypeKind)
			{
			case EdmTypeKind.Entity:
				if (payloadType != null)
				{
					IEdmTypeReference edmTypeReference = payloadType.ToTypeReference(true);
					ValidationUtils.ValidateEntityTypeIsAssignable((IEdmEntityTypeReference)expectedTypeReference, (IEdmEntityTypeReference)edmTypeReference);
					return edmTypeReference;
				}
				return expectedTypeReference;
			case EdmTypeKind.Complex:
				if (payloadType != null)
				{
					ReaderValidationUtils.VerifyComplexType(expectedTypeReference, payloadType, true);
					return expectedTypeReference;
				}
				return expectedTypeReference;
			case EdmTypeKind.Collection:
				if (payloadType != null && string.CompareOrdinal(payloadType.ODataFullName(), expectedTypeReference.ODataFullName()) != 0)
				{
					ReaderValidationUtils.VerifyCollectionComplexItemType(expectedTypeReference, payloadType);
					throw new ODataException(Strings.ValidationUtils_IncompatibleType(payloadType.ODataFullName(), expectedTypeReference.ODataFullName()));
				}
				return expectedTypeReference;
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ReaderValidationUtils_ResolveAndValidateTypeName_Strict_TypeKind));
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x0004649F File Offset: 0x0004469F
		private static void VerifyPayloadTypeDefined(string payloadTypeName, IEdmType payloadType)
		{
			if (payloadTypeName != null && payloadType == null)
			{
				throw new ODataException(Strings.ValidationUtils_UnrecognizedTypeName(payloadTypeName));
			}
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x000464B4 File Offset: 0x000446B4
		private static void VerifyComplexType(IEdmTypeReference expectedTypeReference, IEdmType payloadType, bool failIfNotRelated)
		{
			IEdmStructuredType edmStructuredType = expectedTypeReference.AsStructured().StructuredDefinition();
			IEdmStructuredType edmStructuredType2 = (IEdmStructuredType)payloadType;
			if (!edmStructuredType.IsEquivalentTo(edmStructuredType2))
			{
				if (edmStructuredType.IsAssignableFrom(edmStructuredType2))
				{
					throw new ODataException(Strings.ReaderValidationUtils_DerivedComplexTypesAreNotAllowed(edmStructuredType.ODataFullName(), edmStructuredType2.ODataFullName()));
				}
				if (failIfNotRelated)
				{
					throw new ODataException(Strings.ValidationUtils_IncompatibleType(edmStructuredType2.ODataFullName(), edmStructuredType.ODataFullName()));
				}
			}
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x00046518 File Offset: 0x00044718
		private static void VerifyCollectionComplexItemType(IEdmTypeReference expectedTypeReference, IEdmType payloadType)
		{
			IEdmCollectionTypeReference edmCollectionTypeReference = ValidationUtils.ValidateCollectionType(expectedTypeReference);
			IEdmTypeReference collectionItemType = edmCollectionTypeReference.GetCollectionItemType();
			if (collectionItemType != null && collectionItemType.IsODataComplexTypeKind())
			{
				IEdmCollectionTypeReference edmCollectionTypeReference2 = ValidationUtils.ValidateCollectionType(payloadType.ToTypeReference());
				IEdmTypeReference collectionItemType2 = edmCollectionTypeReference2.GetCollectionItemType();
				if (collectionItemType2 != null && collectionItemType2.IsODataComplexTypeKind())
				{
					ReaderValidationUtils.VerifyComplexType(collectionItemType, collectionItemType2.Definition, false);
				}
			}
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x0004656C File Offset: 0x0004476C
		private static SerializationTypeNameAnnotation CreateSerializationTypeNameAnnotation(string payloadTypeName, IEdmTypeReference targetTypeReference)
		{
			if (payloadTypeName != null && string.CompareOrdinal(payloadTypeName, targetTypeReference.ODataFullName()) != 0)
			{
				return new SerializationTypeNameAnnotation
				{
					TypeName = payloadTypeName
				};
			}
			if (payloadTypeName == null)
			{
				return new SerializationTypeNameAnnotation
				{
					TypeName = null
				};
			}
			return null;
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x000465AC File Offset: 0x000447AC
		private static EdmTypeKind ComputeTargetTypeKind(IEdmTypeReference expectedTypeReference, bool forEntityValue, string payloadTypeName, EdmTypeKind payloadTypeKind, ODataMessageReaderSettings messageReaderSettings, Func<EdmTypeKind> typeKindFromPayloadFunc)
		{
			bool flag = messageReaderSettings.ReaderBehavior.TypeResolver != null && payloadTypeKind != EdmTypeKind.None;
			EdmTypeKind edmTypeKind = EdmTypeKind.None;
			if (!flag)
			{
				edmTypeKind = ReaderValidationUtils.GetExpectedTypeKind(expectedTypeReference, messageReaderSettings);
			}
			EdmTypeKind edmTypeKind2;
			if (edmTypeKind != EdmTypeKind.None)
			{
				edmTypeKind2 = edmTypeKind;
			}
			else if (payloadTypeKind != EdmTypeKind.None)
			{
				if (!forEntityValue)
				{
					ValidationUtils.ValidateValueTypeKind(payloadTypeKind, payloadTypeName);
				}
				edmTypeKind2 = payloadTypeKind;
			}
			else
			{
				edmTypeKind2 = typeKindFromPayloadFunc();
			}
			if (ReaderValidationUtils.ShouldValidatePayloadTypeKind(messageReaderSettings, expectedTypeReference, payloadTypeKind))
			{
				ValidationUtils.ValidateTypeKind(edmTypeKind2, expectedTypeReference.TypeKind(), payloadTypeName);
			}
			return edmTypeKind2;
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x0004661C File Offset: 0x0004481C
		private static EdmTypeKind GetExpectedTypeKind(IEdmTypeReference expectedTypeReference, ODataMessageReaderSettings messageReaderSettings)
		{
			IEdmType definition;
			if (expectedTypeReference == null || (definition = expectedTypeReference.Definition) == null)
			{
				return EdmTypeKind.None;
			}
			EdmTypeKind typeKind = definition.TypeKind;
			if (messageReaderSettings.DisablePrimitiveTypeConversion && typeKind == EdmTypeKind.Primitive && !definition.IsStream())
			{
				return EdmTypeKind.None;
			}
			return typeKind;
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x00046658 File Offset: 0x00044858
		private static bool ShouldValidatePayloadTypeKind(ODataMessageReaderSettings messageReaderSettings, IEdmTypeReference expectedValueTypeReference, EdmTypeKind payloadTypeKind)
		{
			bool flag = messageReaderSettings.ReaderBehavior.TypeResolver != null && payloadTypeKind != EdmTypeKind.None;
			return expectedValueTypeReference != null && (!messageReaderSettings.DisableStrictMetadataValidation || flag || (expectedValueTypeReference.IsODataPrimitiveTypeKind() && messageReaderSettings.DisablePrimitiveTypeConversion));
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x000466A0 File Offset: 0x000448A0
		private static void ValidateNullValueAllowed(IEdmTypeReference expectedValueTypeReference, bool validateNullValue, IEdmModel model, string propertyName)
		{
			if (validateNullValue && expectedValueTypeReference != null)
			{
				if (expectedValueTypeReference.IsODataPrimitiveTypeKind())
				{
					if (!expectedValueTypeReference.IsNullable)
					{
						ReaderValidationUtils.ThrowNullValueForNonNullableTypeException(expectedValueTypeReference, propertyName);
						return;
					}
				}
				else
				{
					if (expectedValueTypeReference.IsNonEntityCollectionType())
					{
						ReaderValidationUtils.ThrowNullValueForNonNullableTypeException(expectedValueTypeReference, propertyName);
						return;
					}
					if (expectedValueTypeReference.IsODataComplexTypeKind() && ValidationUtils.ShouldValidateComplexPropertyNullValue(model))
					{
						IEdmComplexTypeReference edmComplexTypeReference = expectedValueTypeReference.AsComplex();
						if (!edmComplexTypeReference.IsNullable)
						{
							ReaderValidationUtils.ThrowNullValueForNonNullableTypeException(expectedValueTypeReference, propertyName);
						}
					}
				}
			}
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x00046701 File Offset: 0x00044901
		private static void ThrowNullValueForNonNullableTypeException(IEdmTypeReference expectedValueTypeReference, string propertyName)
		{
			if (string.IsNullOrEmpty(propertyName))
			{
				throw new ODataException(Strings.ReaderValidationUtils_NullValueForNonNullableType(expectedValueTypeReference.ODataFullName()));
			}
			throw new ODataException(Strings.ReaderValidationUtils_NullNamedValueForNonNullableType(propertyName, expectedValueTypeReference.ODataFullName()));
		}
	}
}
