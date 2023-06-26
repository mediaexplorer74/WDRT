using System;
using System.Globalization;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Csdl;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x02000258 RID: 600
	internal static class ValidationUtils
	{
		// Token: 0x060013B5 RID: 5045 RVA: 0x0004A464 File Offset: 0x00048664
		internal static void ValidateOpenPropertyValue(string propertyName, object value, ODataUndeclaredPropertyBehaviorKinds undeclaredPropertyBehaviorKinds)
		{
			bool flag = !undeclaredPropertyBehaviorKinds.HasFlag(ODataUndeclaredPropertyBehaviorKinds.IgnoreUndeclaredValueProperty) && !undeclaredPropertyBehaviorKinds.HasFlag(ODataUndeclaredPropertyBehaviorKinds.SupportUndeclaredValueProperty);
			if (flag && value is ODataCollectionValue)
			{
				throw new ODataException(Strings.ValidationUtils_OpenCollectionProperty(propertyName));
			}
			if (value is ODataStreamReferenceValue)
			{
				throw new ODataException(Strings.ValidationUtils_OpenStreamProperty(propertyName));
			}
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x0004A4C7 File Offset: 0x000486C7
		internal static void ValidateValueTypeKind(EdmTypeKind typeKind, string typeName)
		{
			if (typeKind != EdmTypeKind.Primitive && typeKind != EdmTypeKind.Complex && typeKind != EdmTypeKind.Collection)
			{
				throw new ODataException(Strings.ValidationUtils_IncorrectValueTypeKind(typeName, typeKind.ToString()));
			}
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x0004A4EC File Offset: 0x000486EC
		internal static string ValidateCollectionTypeName(string collectionTypeName)
		{
			string collectionItemTypeName = EdmLibraryExtensions.GetCollectionItemTypeName(collectionTypeName);
			if (collectionItemTypeName == null)
			{
				throw new ODataException(Strings.ValidationUtils_InvalidCollectionTypeName(collectionTypeName));
			}
			return collectionItemTypeName;
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x0004A510 File Offset: 0x00048710
		internal static void ValidateEntityTypeIsAssignable(IEdmEntityTypeReference expectedEntityTypeReference, IEdmEntityTypeReference payloadEntityTypeReference)
		{
			if (!expectedEntityTypeReference.EntityDefinition().IsAssignableFrom(payloadEntityTypeReference.EntityDefinition()))
			{
				throw new ODataException(Strings.ValidationUtils_EntryTypeNotAssignableToExpectedType(payloadEntityTypeReference.ODataFullName(), expectedEntityTypeReference.ODataFullName()));
			}
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x0004A53C File Offset: 0x0004873C
		internal static IEdmCollectionTypeReference ValidateCollectionType(IEdmTypeReference typeReference)
		{
			IEdmCollectionTypeReference edmCollectionTypeReference = typeReference.AsCollectionOrNull();
			if (edmCollectionTypeReference != null && !typeReference.IsNonEntityCollectionType())
			{
				throw new ODataException(Strings.ValidationUtils_InvalidCollectionTypeReference(typeReference.TypeKind()));
			}
			return edmCollectionTypeReference;
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x0004A572 File Offset: 0x00048772
		internal static void ValidateCollectionItem(object item, bool isStreamable)
		{
			if (!isStreamable && item == null)
			{
				throw new ODataException(Strings.ValidationUtils_NonStreamingCollectionElementsMustNotBeNull);
			}
			if (item is ODataCollectionValue)
			{
				throw new ODataException(Strings.ValidationUtils_NestedCollectionsAreNotSupported);
			}
			if (item is ODataStreamReferenceValue)
			{
				throw new ODataException(Strings.ValidationUtils_StreamReferenceValuesNotSupportedInCollections);
			}
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x0004A5AB File Offset: 0x000487AB
		internal static void ValidateNullCollectionItem(IEdmTypeReference expectedItemType, ODataWriterBehavior writerBehavior)
		{
			if (expectedItemType != null && expectedItemType.IsODataPrimitiveTypeKind() && !expectedItemType.IsNullable && !writerBehavior.AllowNullValuesForNonNullablePrimitiveTypes)
			{
				throw new ODataException(Strings.ValidationUtils_NullCollectionItemForNonNullableType(expectedItemType.ODataFullName()));
			}
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x0004A5D9 File Offset: 0x000487D9
		internal static void ValidateStreamReferenceProperty(ODataProperty streamProperty, IEdmProperty edmProperty)
		{
			if (edmProperty != null && !edmProperty.Type.IsStream())
			{
				throw new ODataException(Strings.ValidationUtils_MismatchPropertyKindForStreamProperty(streamProperty.Name));
			}
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x0004A5FC File Offset: 0x000487FC
		internal static void ValidateAssociationLinkNotNull(ODataAssociationLink associationLink)
		{
			if (associationLink == null)
			{
				throw new ODataException(Strings.ValidationUtils_EnumerableContainsANullItem("ODataEntry.AssociationLinks"));
			}
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x0004A611 File Offset: 0x00048811
		internal static void ValidateAssociationLinkName(string associationLinkName)
		{
			if (string.IsNullOrEmpty(associationLinkName))
			{
				throw new ODataException(Strings.ValidationUtils_AssociationLinkMustSpecifyName);
			}
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x0004A626 File Offset: 0x00048826
		internal static void ValidateAssociationLink(ODataAssociationLink associationLink)
		{
			ValidationUtils.ValidateAssociationLinkName(associationLink.Name);
			if (associationLink.Url == null)
			{
				throw new ODataException(Strings.ValidationUtils_AssociationLinkMustSpecifyUrl);
			}
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x0004A64C File Offset: 0x0004884C
		internal static void IncreaseAndValidateRecursionDepth(ref int recursionDepth, int maxDepth)
		{
			recursionDepth++;
			if (recursionDepth > maxDepth)
			{
				throw new ODataException(Strings.ValidationUtils_RecursionDepthLimitReached(maxDepth));
			}
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x0004A66C File Offset: 0x0004886C
		internal static void ValidateOperationNotNull(ODataOperation operation, bool isAction)
		{
			if (operation == null)
			{
				string text = (isAction ? "ODataEntry.Actions" : "ODataEntry.Functions");
				throw new ODataException(Strings.ValidationUtils_EnumerableContainsANullItem(text));
			}
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x0004A698 File Offset: 0x00048898
		internal static void ValidateOperationMetadataNotNull(ODataOperation operation)
		{
			if (operation.Metadata == null)
			{
				throw new ODataException(Strings.ValidationUtils_ActionsAndFunctionsMustSpecifyMetadata(operation.GetType().Name));
			}
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x0004A6BE File Offset: 0x000488BE
		internal static void ValidateOperationTargetNotNull(ODataOperation operation)
		{
			if (operation.Target == null)
			{
				throw new ODataException(Strings.ValidationUtils_ActionsAndFunctionsMustSpecifyTarget(operation.GetType().Name));
			}
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x0004A6E4 File Offset: 0x000488E4
		internal static void ValidateEntryMetadataResource(ODataEntry entry, IEdmEntityType entityType, IEdmModel model, bool validateMediaResource)
		{
			if (entityType != null && validateMediaResource)
			{
				bool flag = model.HasDefaultStream(entityType);
				if (entry.MediaResource == null)
				{
					if (flag)
					{
						throw new ODataException(Strings.ValidationUtils_EntryWithoutMediaResourceAndMLEType(entityType.ODataFullName()));
					}
				}
				else if (!flag)
				{
					throw new ODataException(Strings.ValidationUtils_EntryWithMediaResourceAndNonMLEType(entityType.ODataFullName()));
				}
			}
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x0004A730 File Offset: 0x00048930
		internal static void ValidateIsExpectedPrimitiveType(object value, IEdmTypeReference expectedTypeReference)
		{
			Type type = value.GetType();
			IEdmPrimitiveTypeReference primitiveTypeReference = EdmLibraryExtensions.GetPrimitiveTypeReference(type);
			ValidationUtils.ValidateIsExpectedPrimitiveType(value, primitiveTypeReference, expectedTypeReference);
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x0004A753 File Offset: 0x00048953
		internal static void ValidateIsExpectedPrimitiveType(object value, IEdmPrimitiveTypeReference valuePrimitiveTypeReference, IEdmTypeReference expectedTypeReference)
		{
			if (valuePrimitiveTypeReference == null)
			{
				throw new ODataException(Strings.ValidationUtils_UnsupportedPrimitiveType(value.GetType().FullName));
			}
			if (!expectedTypeReference.IsODataPrimitiveTypeKind())
			{
				throw new ODataException(Strings.ValidationUtils_NonPrimitiveTypeForPrimitiveValue(expectedTypeReference.ODataFullName()));
			}
			ValidationUtils.ValidateMetadataPrimitiveType(expectedTypeReference, valuePrimitiveTypeReference);
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x0004A790 File Offset: 0x00048990
		internal static void ValidateMetadataPrimitiveType(IEdmTypeReference expectedTypeReference, IEdmTypeReference typeReferenceFromValue)
		{
			IEdmPrimitiveType edmPrimitiveType = (IEdmPrimitiveType)expectedTypeReference.Definition;
			IEdmPrimitiveType edmPrimitiveType2 = (IEdmPrimitiveType)typeReferenceFromValue.Definition;
			bool flag = expectedTypeReference.IsNullable == typeReferenceFromValue.IsNullable || (expectedTypeReference.IsNullable && !typeReferenceFromValue.IsNullable) || !typeReferenceFromValue.IsODataValueType();
			bool flag2 = edmPrimitiveType.IsAssignableFrom(edmPrimitiveType2);
			if (!flag || !flag2)
			{
				throw new ODataException(Strings.ValidationUtils_IncompatiblePrimitiveItemType(typeReferenceFromValue.ODataFullName(), typeReferenceFromValue.IsNullable, expectedTypeReference.ODataFullName(), expectedTypeReference.IsNullable));
			}
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x0004A81B File Offset: 0x00048A1B
		internal static void ValidateResourceCollectionInfo(ODataResourceCollectionInfo collectionInfo)
		{
			if (collectionInfo == null)
			{
				throw new ODataException(Strings.ValidationUtils_WorkspaceCollectionsMustNotContainNullItem);
			}
			if (collectionInfo.Url == null)
			{
				throw new ODataException(Strings.ValidationUtils_ResourceCollectionMustSpecifyUrl);
			}
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x0004A844 File Offset: 0x00048A44
		internal static void ValidateResourceCollectionInfoUrl(string collectionInfoUrl)
		{
			if (collectionInfoUrl == null)
			{
				throw new ODataException(Strings.ValidationUtils_ResourceCollectionUrlMustNotBeNull);
			}
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x0004A854 File Offset: 0x00048A54
		internal static void ValidateTypeKind(EdmTypeKind actualTypeKind, EdmTypeKind expectedTypeKind, string typeName)
		{
			if (actualTypeKind == expectedTypeKind)
			{
				return;
			}
			if (typeName == null)
			{
				throw new ODataException(Strings.ValidationUtils_IncorrectTypeKindNoTypeName(actualTypeKind.ToString(), expectedTypeKind.ToString()));
			}
			throw new ODataException(Strings.ValidationUtils_IncorrectTypeKind(typeName, expectedTypeKind.ToString(), actualTypeKind.ToString()));
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x0004A8AB File Offset: 0x00048AAB
		internal static void ValidateBoundaryString(string boundary)
		{
			if (boundary == null || boundary.Length == 0 || boundary.Length > 70)
			{
				throw new ODataException(Strings.ValidationUtils_InvalidBatchBoundaryDelimiterLength(boundary, 70));
			}
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x0004A8D8 File Offset: 0x00048AD8
		internal static bool ShouldValidateComplexPropertyNullValue(IEdmModel model)
		{
			Version edmVersion = model.GetEdmVersion();
			Version dataServiceVersion = model.GetDataServiceVersion();
			return !(edmVersion != null) || !(dataServiceVersion != null) || !(edmVersion < ODataVersion.V3.ToDataServiceVersion());
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x0004A916 File Offset: 0x00048B16
		internal static bool IsValidPropertyName(string propertyName)
		{
			return propertyName.IndexOfAny(ValidationUtils.InvalidCharactersInPropertyNames) < 0;
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x0004A958 File Offset: 0x00048B58
		internal static void ValidatePropertyName(string propertyName)
		{
			if (!ValidationUtils.IsValidPropertyName(propertyName))
			{
				string text = string.Join(", ", ValidationUtils.InvalidCharactersInPropertyNames.Select((char c) => string.Format(CultureInfo.InvariantCulture, "'{0}'", new object[] { c })).ToArray<string>());
				throw new ODataException(Strings.ValidationUtils_PropertiesMustNotContainReservedChars(propertyName, text));
			}
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x0004A9B4 File Offset: 0x00048BB4
		internal static int ValidateTotalEntityPropertyMappingCount(ODataEntityPropertyMappingCache baseCache, ODataEntityPropertyMappingCollection mappings, int maxMappingCount)
		{
			int num = ((baseCache == null) ? 0 : baseCache.TotalMappingCount);
			int num2 = ((mappings == null) ? 0 : mappings.Count);
			int num3 = num + num2;
			if (num3 > maxMappingCount)
			{
				throw new ODataException(Strings.ValidationUtils_MaxNumberOfEntityPropertyMappingsExceeded(num3, maxMappingCount));
			}
			return num3;
		}

		// Token: 0x04000708 RID: 1800
		private const int MaxBoundaryLength = 70;

		// Token: 0x04000709 RID: 1801
		internal static readonly char[] InvalidCharactersInPropertyNames = new char[] { ':', '.', '@' };
	}
}
