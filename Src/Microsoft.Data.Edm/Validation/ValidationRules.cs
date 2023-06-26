using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.Data.Edm.Annotations;
using Microsoft.Data.Edm.Csdl;
using Microsoft.Data.Edm.Csdl.Internal.CsdlSemantics;
using Microsoft.Data.Edm.Expressions;
using Microsoft.Data.Edm.Internal;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.Edm.Library.Internal;
using Microsoft.Data.Edm.Validation.Internal;
using Microsoft.Data.Edm.Values;

namespace Microsoft.Data.Edm.Validation
{
	// Token: 0x0200023A RID: 570
	public static class ValidationRules
	{
		// Token: 0x06000C9D RID: 3229 RVA: 0x000254E4 File Offset: 0x000236E4
		private static void CheckForUnreacheableTypeError(ValidationContext context, IEdmSchemaType type, EdmLocation location)
		{
			IEdmType edmType = context.Model.FindType(type.FullName());
			if (edmType is AmbiguousTypeBinding)
			{
				context.AddError(location, EdmErrorCode.BadAmbiguousElementBinding, Strings.EdmModel_Validator_Semantic_AmbiguousType(type.FullName()));
				return;
			}
			if (!edmType.IsEquivalentTo(type))
			{
				context.AddError(location, EdmErrorCode.BadUnresolvedType, Strings.EdmModel_Validator_Semantic_InaccessibleType(type.FullName()));
			}
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x00025544 File Offset: 0x00023744
		private static void CheckForNameError(ValidationContext context, string name, EdmLocation location)
		{
			if (EdmUtil.IsNullOrWhiteSpaceInternal(name) || name.Length == 0)
			{
				context.AddError(location, EdmErrorCode.InvalidName, Strings.EdmModel_Validator_Syntactic_MissingName);
				return;
			}
			if (name.Length > 480)
			{
				context.AddError(location, EdmErrorCode.NameTooLong, Strings.EdmModel_Validator_Syntactic_EdmModel_NameIsTooLong(name));
				return;
			}
			if (!EdmUtil.IsValidUndottedName(name))
			{
				context.AddError(location, EdmErrorCode.InvalidName, Strings.EdmModel_Validator_Syntactic_EdmModel_NameIsNotAllowed(name));
			}
		}

		// Token: 0x0400059D RID: 1437
		public static readonly ValidationRule<IEdmElement> ElementDirectValueAnnotationFullNameMustBeUnique = new ValidationRule<IEdmElement>(delegate(ValidationContext context, IEdmElement item)
		{
			HashSetInternal<string> hashSetInternal = new HashSetInternal<string>();
			foreach (IEdmDirectValueAnnotation edmDirectValueAnnotation in context.Model.DirectValueAnnotationsManager.GetDirectValueAnnotations(item))
			{
				if (!hashSetInternal.Add(edmDirectValueAnnotation.NamespaceUri + ":" + edmDirectValueAnnotation.Name))
				{
					context.AddError(edmDirectValueAnnotation.Location(), EdmErrorCode.DuplicateDirectValueAnnotationFullName, Strings.EdmModel_Validator_Semantic_ElementDirectValueAnnotationFullNameMustBeUnique(edmDirectValueAnnotation.NamespaceUri, edmDirectValueAnnotation.Name));
				}
			}
		});

		// Token: 0x0400059E RID: 1438
		public static readonly ValidationRule<IEdmNamedElement> NamedElementNameMustNotBeEmptyOrWhiteSpace = new ValidationRule<IEdmNamedElement>(delegate(ValidationContext context, IEdmNamedElement item)
		{
			if (EdmUtil.IsNullOrWhiteSpaceInternal(item.Name) || item.Name.Length == 0)
			{
				context.AddError(item.Location(), EdmErrorCode.InvalidName, Strings.EdmModel_Validator_Syntactic_MissingName);
			}
		});

		// Token: 0x0400059F RID: 1439
		public static readonly ValidationRule<IEdmNamedElement> NamedElementNameIsTooLong = new ValidationRule<IEdmNamedElement>(delegate(ValidationContext context, IEdmNamedElement item)
		{
			if (!EdmUtil.IsNullOrWhiteSpaceInternal(item.Name) && item.Name.Length > 480)
			{
				context.AddError(item.Location(), EdmErrorCode.NameTooLong, Strings.EdmModel_Validator_Syntactic_EdmModel_NameIsTooLong(item.Name));
			}
		});

		// Token: 0x040005A0 RID: 1440
		public static readonly ValidationRule<IEdmNamedElement> NamedElementNameIsNotAllowed = new ValidationRule<IEdmNamedElement>(delegate(ValidationContext context, IEdmNamedElement item)
		{
			if (item is IEdmDirectValueAnnotation)
			{
				return;
			}
			if (!EdmUtil.IsNullOrWhiteSpaceInternal(item.Name) && item.Name.Length <= 480 && item.Name.Length > 0 && !EdmUtil.IsValidUndottedName(item.Name))
			{
				context.AddError(item.Location(), EdmErrorCode.InvalidName, Strings.EdmModel_Validator_Syntactic_EdmModel_NameIsNotAllowed(item.Name));
			}
		});

		// Token: 0x040005A1 RID: 1441
		public static readonly ValidationRule<IEdmSchemaElement> SchemaElementNamespaceMustNotBeEmptyOrWhiteSpace = new ValidationRule<IEdmSchemaElement>(delegate(ValidationContext context, IEdmSchemaElement item)
		{
			if (EdmUtil.IsNullOrWhiteSpaceInternal(item.Namespace) || item.Namespace.Length == 0)
			{
				context.AddError(item.Location(), EdmErrorCode.InvalidNamespaceName, Strings.EdmModel_Validator_Syntactic_MissingNamespaceName);
			}
		});

		// Token: 0x040005A2 RID: 1442
		public static readonly ValidationRule<IEdmSchemaElement> SchemaElementNamespaceIsTooLong = new ValidationRule<IEdmSchemaElement>(delegate(ValidationContext context, IEdmSchemaElement item)
		{
			if (item.Namespace.Length > 512)
			{
				context.AddError(item.Location(), EdmErrorCode.InvalidNamespaceName, Strings.EdmModel_Validator_Syntactic_EdmModel_NamespaceNameIsTooLong(item.Namespace));
			}
		});

		// Token: 0x040005A3 RID: 1443
		public static readonly ValidationRule<IEdmSchemaElement> SchemaElementNamespaceIsNotAllowed = new ValidationRule<IEdmSchemaElement>(delegate(ValidationContext context, IEdmSchemaElement item)
		{
			if (item.Namespace.Length <= 512 && item.Namespace.Length > 0 && !EdmUtil.IsNullOrWhiteSpaceInternal(item.Namespace) && !EdmUtil.IsValidDottedName(item.Namespace))
			{
				context.AddError(item.Location(), EdmErrorCode.InvalidNamespaceName, Strings.EdmModel_Validator_Syntactic_EdmModel_NamespaceNameIsNotAllowed(item.Namespace));
			}
		});

		// Token: 0x040005A4 RID: 1444
		public static readonly ValidationRule<IEdmSchemaElement> SchemaElementSystemNamespaceEncountered = new ValidationRule<IEdmSchemaElement>(delegate(ValidationContext context, IEdmSchemaElement element)
		{
			if (ValidationHelper.IsEdmSystemNamespace(element.Namespace))
			{
				context.AddError(element.Location(), EdmErrorCode.SystemNamespaceEncountered, Strings.EdmModel_Validator_Semantic_SystemNamespaceEncountered(element.Namespace));
			}
		});

		// Token: 0x040005A5 RID: 1445
		public static readonly ValidationRule<IEdmSchemaElement> SchemaElementMustNotHaveKindOfNone = new ValidationRule<IEdmSchemaElement>(delegate(ValidationContext context, IEdmSchemaElement element)
		{
			if (element.SchemaElementKind == EdmSchemaElementKind.None && !context.IsBad(element))
			{
				context.AddError(element.Location(), EdmErrorCode.SchemaElementMustNotHaveKindOfNone, Strings.EdmModel_Validator_Semantic_SchemaElementMustNotHaveKindOfNone(element.FullName()));
			}
		});

		// Token: 0x040005A6 RID: 1446
		public static readonly ValidationRule<IEdmEntityContainerElement> EntityContainerElementMustNotHaveKindOfNone = new ValidationRule<IEdmEntityContainerElement>(delegate(ValidationContext context, IEdmEntityContainerElement element)
		{
			if (element.ContainerElementKind == EdmContainerElementKind.None && !context.IsBad(element))
			{
				context.AddError(element.Location(), EdmErrorCode.EntityContainerElementMustNotHaveKindOfNone, Strings.EdmModel_Validator_Semantic_EntityContainerElementMustNotHaveKindOfNone(element.Container.FullName() + '/' + element.Name));
			}
		});

		// Token: 0x040005A7 RID: 1447
		public static readonly ValidationRule<IEdmEntityContainer> EntityContainerDuplicateEntityContainerMemberName = new ValidationRule<IEdmEntityContainer>(delegate(ValidationContext context, IEdmEntityContainer entityContainer)
		{
			HashSetInternal<string> hashSetInternal2 = new HashSetInternal<string>();
			Dictionary<string, List<IEdmFunctionImport>> dictionary = new Dictionary<string, List<IEdmFunctionImport>>();
			foreach (IEdmEntityContainerElement edmEntityContainerElement in entityContainer.Elements)
			{
				IEdmFunctionImport edmFunctionImport = edmEntityContainerElement as IEdmFunctionImport;
				if (edmFunctionImport != null)
				{
					if (hashSetInternal2.Contains(edmEntityContainerElement.Name))
					{
						context.AddError(edmEntityContainerElement.Location(), EdmErrorCode.DuplicateEntityContainerMemberName, Strings.EdmModel_Validator_Semantic_DuplicateEntityContainerMemberName(edmEntityContainerElement.Name));
					}
					List<IEdmFunctionImport> list;
					if (dictionary.TryGetValue(edmFunctionImport.Name, out list))
					{
						using (List<IEdmFunctionImport>.Enumerator enumerator3 = list.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								IEdmFunctionImport edmFunctionImport2 = enumerator3.Current;
								if (edmFunctionImport.IsFunctionSignatureEquivalentTo(edmFunctionImport2))
								{
									context.AddError(edmEntityContainerElement.Location(), EdmErrorCode.DuplicateEntityContainerMemberName, Strings.EdmModel_Validator_Semantic_DuplicateEntityContainerMemberName(edmEntityContainerElement.Name));
									break;
								}
							}
							goto IL_C9;
						}
						goto IL_C2;
					}
					goto IL_C2;
					IL_C9:
					list.Add(edmFunctionImport);
					continue;
					IL_C2:
					list = new List<IEdmFunctionImport>();
					goto IL_C9;
				}
				if (ValidationHelper.AddMemberNameToHashSet(edmEntityContainerElement, hashSetInternal2, context, EdmErrorCode.DuplicateEntityContainerMemberName, Strings.EdmModel_Validator_Semantic_DuplicateEntityContainerMemberName(edmEntityContainerElement.Name), false) && dictionary.ContainsKey(edmEntityContainerElement.Name))
				{
					context.AddError(edmEntityContainerElement.Location(), EdmErrorCode.DuplicateEntityContainerMemberName, Strings.EdmModel_Validator_Semantic_DuplicateEntityContainerMemberName(edmEntityContainerElement.Name));
				}
			}
		});

		// Token: 0x040005A8 RID: 1448
		public static readonly ValidationRule<IEdmEntitySet> EntitySetTypeHasNoKeys = new ValidationRule<IEdmEntitySet>(delegate(ValidationContext context, IEdmEntitySet entitySet)
		{
			if ((entitySet.ElementType.Key() == null || entitySet.ElementType.Key().Count<IEdmStructuralProperty>() == 0) && !context.IsBad(entitySet.ElementType))
			{
				string text = Strings.EdmModel_Validator_Semantic_EntitySetTypeHasNoKeys(entitySet.Name, entitySet.ElementType.Name);
				context.AddError(entitySet.Location(), EdmErrorCode.EntitySetTypeHasNoKeys, text);
			}
		});

		// Token: 0x040005A9 RID: 1449
		public static readonly ValidationRule<IEdmEntitySet> EntitySetInaccessibleEntityType = new ValidationRule<IEdmEntitySet>(delegate(ValidationContext context, IEdmEntitySet entitySet)
		{
			if (!context.IsBad(entitySet.ElementType))
			{
				ValidationRules.CheckForUnreacheableTypeError(context, entitySet.ElementType, entitySet.Location());
			}
		});

		// Token: 0x040005AA RID: 1450
		public static readonly ValidationRule<IEdmEntitySet> EntitySetCanOnlyBeContainedByASingleNavigationProperty = new ValidationRule<IEdmEntitySet>(delegate(ValidationContext context, IEdmEntitySet set)
		{
			bool flag = false;
			foreach (IEdmEntitySet edmEntitySet in set.Container.EntitySets())
			{
				foreach (IEdmNavigationTargetMapping edmNavigationTargetMapping in edmEntitySet.NavigationTargets)
				{
					IEdmNavigationProperty navigationProperty3 = edmNavigationTargetMapping.NavigationProperty;
					if (edmNavigationTargetMapping.TargetEntitySet == set && navigationProperty3.ContainsTarget)
					{
						if (flag)
						{
							context.AddError(set.Location(), EdmErrorCode.EntitySetCanOnlyBeContainedByASingleNavigationProperty, Strings.EdmModel_Validator_Semantic_EntitySetCanOnlyBeContainedByASingleNavigationProperty(set.Container.FullName() + "." + set.Name));
						}
						flag = true;
					}
				}
			}
		});

		// Token: 0x040005AB RID: 1451
		public static readonly ValidationRule<IEdmEntitySet> EntitySetNavigationMappingMustBeBidirectional = new ValidationRule<IEdmEntitySet>(delegate(ValidationContext context, IEdmEntitySet set)
		{
			foreach (IEdmNavigationTargetMapping edmNavigationTargetMapping2 in set.NavigationTargets)
			{
				IEdmNavigationProperty navigationProperty2 = edmNavigationTargetMapping2.NavigationProperty;
				IEdmEntitySet edmEntitySet2 = edmNavigationTargetMapping2.TargetEntitySet.FindNavigationTarget(navigationProperty2.Partner);
				if ((edmEntitySet2 != null || navigationProperty2.Partner.DeclaringEntityType().FindProperty(navigationProperty2.Partner.Name) == navigationProperty2.Partner) && edmEntitySet2 != set)
				{
					context.AddError(set.Location(), EdmErrorCode.EntitySetNavigationMappingMustBeBidirectional, Strings.EdmModel_Validator_Semantic_EntitySetNavigationMappingMustBeBidirectional(set.Container.FullName() + "." + set.Name, navigationProperty2.Name));
				}
			}
		});

		// Token: 0x040005AC RID: 1452
		public static readonly ValidationRule<IEdmEntitySet> EntitySetAssociationSetNameMustBeValid = new ValidationRule<IEdmEntitySet>(delegate(ValidationContext context, IEdmEntitySet set)
		{
			foreach (IEdmNavigationTargetMapping edmNavigationTargetMapping3 in set.NavigationTargets)
			{
				if (edmNavigationTargetMapping3.NavigationProperty.GetPrimary() == edmNavigationTargetMapping3.NavigationProperty)
				{
					ValidationRules.CheckForNameError(context, context.Model.GetAssociationSetName(set, edmNavigationTargetMapping3.NavigationProperty), set.Location());
				}
			}
		});

		// Token: 0x040005AD RID: 1453
		public static readonly ValidationRule<IEdmEntitySet> EntitySetNavigationPropertyMappingsMustBeUnique = new ValidationRule<IEdmEntitySet>(delegate(ValidationContext context, IEdmEntitySet set)
		{
			foreach (IEdmNavigationTargetMapping edmNavigationTargetMapping4 in set.NavigationTargets)
			{
				HashSetInternal<IEdmNavigationProperty> hashSetInternal3 = new HashSetInternal<IEdmNavigationProperty>();
				if (!hashSetInternal3.Add(edmNavigationTargetMapping4.NavigationProperty))
				{
					context.AddError(set.Location(), EdmErrorCode.DuplicateNavigationPropertyMapping, Strings.EdmModel_Validator_Semantic_DuplicateNavigationPropertyMapping(set.Container.FullName() + "." + set.Name, edmNavigationTargetMapping4.NavigationProperty.Name));
				}
			}
		});

		// Token: 0x040005AE RID: 1454
		public static readonly ValidationRule<IEdmEntitySet> EntitySetRecursiveNavigationPropertyMappingsMustPointBackToSourceEntitySet = new ValidationRule<IEdmEntitySet>(delegate(ValidationContext context, IEdmEntitySet set)
		{
			foreach (IEdmNavigationTargetMapping edmNavigationTargetMapping5 in set.NavigationTargets)
			{
				if (edmNavigationTargetMapping5.NavigationProperty.ContainsTarget && edmNavigationTargetMapping5.NavigationProperty.DeclaringType.IsOrInheritsFrom(edmNavigationTargetMapping5.NavigationProperty.ToEntityType()) && edmNavigationTargetMapping5.TargetEntitySet != set)
				{
					context.AddError(set.Location(), EdmErrorCode.EntitySetRecursiveNavigationPropertyMappingsMustPointBackToSourceEntitySet, Strings.EdmModel_Validator_Semantic_EntitySetRecursiveNavigationPropertyMappingsMustPointBackToSourceEntitySet(edmNavigationTargetMapping5.NavigationProperty, set.Name));
				}
			}
		});

		// Token: 0x040005AF RID: 1455
		public static readonly ValidationRule<IEdmEntitySet> EntitySetNavigationPropertyMappingMustPointToValidTargetForProperty = new ValidationRule<IEdmEntitySet>(delegate(ValidationContext context, IEdmEntitySet set)
		{
			foreach (IEdmNavigationTargetMapping edmNavigationTargetMapping6 in set.NavigationTargets)
			{
				if (!edmNavigationTargetMapping6.TargetEntitySet.ElementType.IsOrInheritsFrom(edmNavigationTargetMapping6.NavigationProperty.ToEntityType()) && !edmNavigationTargetMapping6.NavigationProperty.ToEntityType().IsOrInheritsFrom(edmNavigationTargetMapping6.TargetEntitySet.ElementType) && !context.IsBad(edmNavigationTargetMapping6.TargetEntitySet))
				{
					context.AddError(set.Location(), EdmErrorCode.EntitySetNavigationPropertyMappingMustPointToValidTargetForProperty, Strings.EdmModel_Validator_Semantic_EntitySetNavigationPropertyMappingMustPointToValidTargetForProperty(edmNavigationTargetMapping6.NavigationProperty.Name, edmNavigationTargetMapping6.TargetEntitySet.Name));
				}
			}
		});

		// Token: 0x040005B0 RID: 1456
		public static readonly ValidationRule<IEdmStructuredType> StructuredTypeInvalidMemberNameMatchesTypeName = new ValidationRule<IEdmStructuredType>(delegate(ValidationContext context, IEdmStructuredType structuredType)
		{
			IEdmSchemaType edmSchemaType = structuredType as IEdmSchemaType;
			if (edmSchemaType != null)
			{
				List<IEdmProperty> list2 = structuredType.Properties().ToList<IEdmProperty>();
				if (list2.Count > 0)
				{
					foreach (IEdmProperty edmProperty in list2)
					{
						if (edmProperty != null && edmProperty.Name.EqualsOrdinal(edmSchemaType.Name))
						{
							context.AddError(edmProperty.Location(), EdmErrorCode.BadProperty, Strings.EdmModel_Validator_Semantic_InvalidMemberNameMatchesTypeName(edmProperty.Name));
						}
					}
				}
			}
		});

		// Token: 0x040005B1 RID: 1457
		public static readonly ValidationRule<IEdmStructuredType> StructuredTypePropertyNameAlreadyDefined = new ValidationRule<IEdmStructuredType>(delegate(ValidationContext context, IEdmStructuredType structuredType)
		{
			HashSetInternal<string> hashSetInternal4 = new HashSetInternal<string>();
			foreach (IEdmProperty edmProperty2 in structuredType.Properties())
			{
				if (edmProperty2 != null)
				{
					ValidationHelper.AddMemberNameToHashSet(edmProperty2, hashSetInternal4, context, EdmErrorCode.AlreadyDefined, Strings.EdmModel_Validator_Semantic_PropertyNameAlreadyDefined(edmProperty2.Name), !structuredType.DeclaredProperties.Contains(edmProperty2));
				}
			}
		});

		// Token: 0x040005B2 RID: 1458
		public static readonly ValidationRule<IEdmStructuredType> StructuredTypeBaseTypeMustBeSameKindAsDerivedKind = new ValidationRule<IEdmStructuredType>(delegate(ValidationContext context, IEdmStructuredType structuredType)
		{
			if (structuredType is IEdmSchemaType && structuredType.BaseType != null && structuredType.BaseType.TypeKind != structuredType.TypeKind)
			{
				context.AddError(structuredType.Location(), (structuredType.TypeKind == EdmTypeKind.Entity) ? EdmErrorCode.EntityMustHaveEntityBaseType : EdmErrorCode.ComplexTypeMustHaveComplexBaseType, Strings.EdmModel_Validator_Semantic_BaseTypeMustHaveSameTypeKind);
			}
		});

		// Token: 0x040005B3 RID: 1459
		public static readonly ValidationRule<IEdmStructuredType> StructuredTypeInaccessibleBaseType = new ValidationRule<IEdmStructuredType>(delegate(ValidationContext context, IEdmStructuredType structuredType)
		{
			IEdmSchemaType edmSchemaType2 = structuredType.BaseType as IEdmSchemaType;
			if (edmSchemaType2 != null && !context.IsBad(edmSchemaType2))
			{
				ValidationRules.CheckForUnreacheableTypeError(context, edmSchemaType2, structuredType.Location());
			}
		});

		// Token: 0x040005B4 RID: 1460
		public static readonly ValidationRule<IEdmStructuredType> StructuredTypePropertiesDeclaringTypeMustBeCorrect = new ValidationRule<IEdmStructuredType>(delegate(ValidationContext context, IEdmStructuredType structuredType)
		{
			foreach (IEdmProperty edmProperty3 in structuredType.DeclaredProperties)
			{
				if (edmProperty3 != null && !edmProperty3.DeclaringType.Equals(structuredType))
				{
					context.AddError(edmProperty3.Location(), EdmErrorCode.DeclaringTypeMustBeCorrect, Strings.EdmModel_Validator_Semantic_DeclaringTypeMustBeCorrect(edmProperty3.Name));
				}
			}
		});

		// Token: 0x040005B5 RID: 1461
		public static readonly ValidationRule<IEdmStructuredType> OpenTypesNotSupported = new ValidationRule<IEdmStructuredType>(delegate(ValidationContext context, IEdmStructuredType structuredType)
		{
			if (structuredType.IsOpen)
			{
				context.AddError(structuredType.Location(), EdmErrorCode.OpenTypeNotSupported, Strings.EdmModel_Validator_Semantic_OpenTypesSupportedOnlyInV12AndAfterV3);
			}
		});

		// Token: 0x040005B6 RID: 1462
		public static readonly ValidationRule<IEdmStructuredType> OnlyEntityTypesCanBeOpen = new ValidationRule<IEdmStructuredType>(delegate(ValidationContext context, IEdmStructuredType structuredType)
		{
			if (structuredType.IsOpen && structuredType.TypeKind != EdmTypeKind.Entity)
			{
				context.AddError(structuredType.Location(), EdmErrorCode.OpenTypeNotSupported, Strings.EdmModel_Validator_Semantic_OpenTypesSupportedForEntityTypesOnly);
			}
		});

		// Token: 0x040005B7 RID: 1463
		public static readonly ValidationRule<IEdmEnumType> EnumTypeEnumsNotSupportedBeforeV3 = new ValidationRule<IEdmEnumType>(delegate(ValidationContext context, IEdmEnumType enumType)
		{
			context.AddError(enumType.Location(), EdmErrorCode.EnumsNotSupportedBeforeV3, Strings.EdmModel_Validator_Semantic_EnumsNotSupportedBeforeV3);
		});

		// Token: 0x040005B8 RID: 1464
		public static readonly ValidationRule<IEdmEnumType> EnumTypeEnumMemberNameAlreadyDefined = new ValidationRule<IEdmEnumType>(delegate(ValidationContext context, IEdmEnumType enumType)
		{
			HashSetInternal<string> hashSetInternal5 = new HashSetInternal<string>();
			foreach (IEdmEnumMember edmEnumMember in enumType.Members)
			{
				if (edmEnumMember != null)
				{
					ValidationHelper.AddMemberNameToHashSet(edmEnumMember, hashSetInternal5, context, EdmErrorCode.AlreadyDefined, Strings.EdmModel_Validator_Semantic_EnumMemberNameAlreadyDefined(edmEnumMember.Name), false);
				}
			}
		});

		// Token: 0x040005B9 RID: 1465
		public static readonly ValidationRule<IEdmEnumType> EnumMustHaveIntegerUnderlyingType = new ValidationRule<IEdmEnumType>(delegate(ValidationContext context, IEdmEnumType enumType)
		{
			if (!enumType.UnderlyingType.PrimitiveKind.IsIntegral() && !context.IsBad(enumType.UnderlyingType))
			{
				context.AddError(enumType.Location(), EdmErrorCode.EnumMustHaveIntegerUnderlyingType, Strings.EdmModel_Validator_Semantic_EnumMustHaveIntegralUnderlyingType(enumType.FullName()));
			}
		});

		// Token: 0x040005BA RID: 1466
		public static readonly ValidationRule<IEdmEnumMember> EnumMemberValueMustHaveSameTypeAsUnderlyingType = new ValidationRule<IEdmEnumMember>(delegate(ValidationContext context, IEdmEnumMember enumMember)
		{
			IEnumerable<EdmError> enumerable;
			if (!context.IsBad(enumMember.DeclaringType) && !context.IsBad(enumMember.DeclaringType.UnderlyingType) && !enumMember.Value.TryAssertPrimitiveAsType(enumMember.DeclaringType.UnderlyingType.GetPrimitiveTypeReference(false), out enumerable))
			{
				context.AddError(enumMember.Location(), EdmErrorCode.EnumMemberTypeMustMatchEnumUnderlyingType, Strings.EdmModel_Validator_Semantic_EnumMemberTypeMustMatchEnumUnderlyingType(enumMember.Name));
			}
		});

		// Token: 0x040005BB RID: 1467
		public static readonly ValidationRule<IEdmEntityType> EntityTypeDuplicatePropertyNameSpecifiedInEntityKey = new ValidationRule<IEdmEntityType>(delegate(ValidationContext context, IEdmEntityType entityType)
		{
			if (entityType.DeclaredKey != null)
			{
				HashSetInternal<string> hashSetInternal6 = new HashSetInternal<string>();
				foreach (IEdmStructuralProperty edmStructuralProperty in entityType.DeclaredKey)
				{
					ValidationHelper.AddMemberNameToHashSet(edmStructuralProperty, hashSetInternal6, context, EdmErrorCode.DuplicatePropertySpecifiedInEntityKey, Strings.EdmModel_Validator_Semantic_DuplicatePropertyNameSpecifiedInEntityKey(entityType.Name, edmStructuralProperty.Name), false);
				}
			}
		});

		// Token: 0x040005BC RID: 1468
		public static readonly ValidationRule<IEdmEntityType> EntityTypeInvalidKeyNullablePart = new ValidationRule<IEdmEntityType>(delegate(ValidationContext context, IEdmEntityType entityType)
		{
			if (entityType.Key() != null)
			{
				foreach (IEdmStructuralProperty edmStructuralProperty2 in entityType.Key())
				{
					if (edmStructuralProperty2.Type.IsPrimitive() && edmStructuralProperty2.Type.IsNullable)
					{
						context.AddError(edmStructuralProperty2.Location(), EdmErrorCode.InvalidKey, Strings.EdmModel_Validator_Semantic_InvalidKeyNullablePart(edmStructuralProperty2.Name, entityType.Name));
					}
				}
			}
		});

		// Token: 0x040005BD RID: 1469
		public static readonly ValidationRule<IEdmEntityType> EntityTypeEntityKeyMustBeScalar = new ValidationRule<IEdmEntityType>(delegate(ValidationContext context, IEdmEntityType entityType)
		{
			if (entityType.Key() != null)
			{
				foreach (IEdmStructuralProperty edmStructuralProperty3 in entityType.Key())
				{
					if (!edmStructuralProperty3.Type.IsPrimitive() && !context.IsBad(edmStructuralProperty3))
					{
						context.AddError(edmStructuralProperty3.Location(), EdmErrorCode.EntityKeyMustBeScalar, Strings.EdmModel_Validator_Semantic_EntityKeyMustBeScalar(edmStructuralProperty3.Name, entityType.Name));
					}
				}
			}
		});

		// Token: 0x040005BE RID: 1470
		public static readonly ValidationRule<IEdmEntityType> EntityTypeEntityKeyMustNotBeBinaryBeforeV2 = new ValidationRule<IEdmEntityType>(delegate(ValidationContext context, IEdmEntityType entityType)
		{
			if (entityType.Key() != null)
			{
				foreach (IEdmStructuralProperty edmStructuralProperty4 in entityType.Key())
				{
					if (edmStructuralProperty4.Type.IsBinary() && !context.IsBad(edmStructuralProperty4.Type.Definition))
					{
						context.AddError(edmStructuralProperty4.Location(), EdmErrorCode.EntityKeyMustNotBeBinary, Strings.EdmModel_Validator_Semantic_EntityKeyMustNotBeBinaryBeforeV2(edmStructuralProperty4.Name, entityType.Name));
					}
				}
			}
		});

		// Token: 0x040005BF RID: 1471
		public static readonly ValidationRule<IEdmEntityType> EntityTypeInvalidKeyKeyDefinedInBaseClass = new ValidationRule<IEdmEntityType>(delegate(ValidationContext context, IEdmEntityType entityType)
		{
			if (entityType.BaseType != null && entityType.DeclaredKey != null && entityType.BaseType.TypeKind == EdmTypeKind.Entity && entityType.BaseEntityType().DeclaredKey != null)
			{
				context.AddError(entityType.Location(), EdmErrorCode.InvalidKey, Strings.EdmModel_Validator_Semantic_InvalidKeyKeyDefinedInBaseClass(entityType.Name, entityType.BaseEntityType().Name));
			}
		});

		// Token: 0x040005C0 RID: 1472
		public static readonly ValidationRule<IEdmEntityType> EntityTypeKeyMissingOnEntityType = new ValidationRule<IEdmEntityType>(delegate(ValidationContext context, IEdmEntityType entityType)
		{
			if ((entityType.Key() == null || entityType.Key().Count<IEdmStructuralProperty>() == 0) && entityType.BaseType == null)
			{
				context.AddError(entityType.Location(), EdmErrorCode.KeyMissingOnEntityType, Strings.EdmModel_Validator_Semantic_KeyMissingOnEntityType(entityType.Name));
			}
		});

		// Token: 0x040005C1 RID: 1473
		public static readonly ValidationRule<IEdmEntityType> EntityTypeKeyPropertyMustBelongToEntity = new ValidationRule<IEdmEntityType>(delegate(ValidationContext context, IEdmEntityType entityType)
		{
			if (entityType.DeclaredKey != null)
			{
				foreach (IEdmStructuralProperty edmStructuralProperty5 in entityType.DeclaredKey)
				{
					if (edmStructuralProperty5.DeclaringType != entityType && !context.IsBad(edmStructuralProperty5))
					{
						context.AddError(entityType.Location(), EdmErrorCode.KeyPropertyMustBelongToEntity, Strings.EdmModel_Validator_Semantic_KeyPropertyMustBelongToEntity(edmStructuralProperty5.Name, entityType.Name));
					}
				}
			}
		});

		// Token: 0x040005C2 RID: 1474
		public static readonly ValidationRule<IEdmEntityReferenceType> EntityReferenceTypeInaccessibleEntityType = new ValidationRule<IEdmEntityReferenceType>(delegate(ValidationContext context, IEdmEntityReferenceType entityReferenceType)
		{
			if (!context.IsBad(entityReferenceType.EntityType))
			{
				ValidationRules.CheckForUnreacheableTypeError(context, entityReferenceType.EntityType, entityReferenceType.Location());
			}
		});

		// Token: 0x040005C3 RID: 1475
		public static readonly ValidationRule<IEdmType> TypeMustNotHaveKindOfNone = new ValidationRule<IEdmType>(delegate(ValidationContext context, IEdmType type)
		{
			if (type2.TypeKind == EdmTypeKind.None && !context.IsBad(type2))
			{
				context.AddError(type2.Location(), EdmErrorCode.TypeMustNotHaveKindOfNone, Strings.EdmModel_Validator_Semantic_TypeMustNotHaveKindOfNone);
			}
		});

		// Token: 0x040005C4 RID: 1476
		public static readonly ValidationRule<IEdmPrimitiveType> PrimitiveTypeMustNotHaveKindOfNone = new ValidationRule<IEdmPrimitiveType>(delegate(ValidationContext context, IEdmPrimitiveType type)
		{
			if (type2.PrimitiveKind == EdmPrimitiveTypeKind.None && !context.IsBad(type2))
			{
				context.AddError(type2.Location(), EdmErrorCode.PrimitiveTypeMustNotHaveKindOfNone, Strings.EdmModel_Validator_Semantic_PrimitiveTypeMustNotHaveKindOfNone(type2.FullName()));
			}
		});

		// Token: 0x040005C5 RID: 1477
		public static readonly ValidationRule<IEdmComplexType> ComplexTypeInvalidAbstractComplexType = new ValidationRule<IEdmComplexType>(delegate(ValidationContext context, IEdmComplexType complexType)
		{
			if (complexType.IsAbstract)
			{
				context.AddError(complexType.Location(), EdmErrorCode.InvalidAbstractComplexType, Strings.EdmModel_Validator_Semantic_InvalidComplexTypeAbstract(complexType.FullName()));
			}
		});

		// Token: 0x040005C6 RID: 1478
		public static readonly ValidationRule<IEdmComplexType> ComplexTypeInvalidPolymorphicComplexType = new ValidationRule<IEdmComplexType>(delegate(ValidationContext context, IEdmComplexType edmComplexType)
		{
			if (edmComplexType.BaseType != null)
			{
				context.AddError(edmComplexType.Location(), EdmErrorCode.InvalidPolymorphicComplexType, Strings.EdmModel_Validator_Semantic_InvalidComplexTypePolymorphic(edmComplexType.FullName()));
			}
		});

		// Token: 0x040005C7 RID: 1479
		public static readonly ValidationRule<IEdmComplexType> ComplexTypeMustContainProperties = new ValidationRule<IEdmComplexType>(delegate(ValidationContext context, IEdmComplexType complexType)
		{
			if (!complexType.Properties().Any<IEdmProperty>())
			{
				context.AddError(complexType.Location(), EdmErrorCode.ComplexTypeMustHaveProperties, Strings.EdmModel_Validator_Semantic_ComplexTypeMustHaveProperties(complexType.FullName()));
			}
		});

		// Token: 0x040005C8 RID: 1480
		public static readonly ValidationRule<IEdmRowType> RowTypeBaseTypeMustBeNull = new ValidationRule<IEdmRowType>(delegate(ValidationContext context, IEdmRowType rowType)
		{
			if (rowType.BaseType != null)
			{
				context.AddError(rowType.Location(), EdmErrorCode.RowTypeMustNotHaveBaseType, Strings.EdmModel_Validator_Semantic_RowTypeMustNotHaveBaseType);
			}
		});

		// Token: 0x040005C9 RID: 1481
		public static readonly ValidationRule<IEdmRowType> RowTypeMustContainProperties = new ValidationRule<IEdmRowType>(delegate(ValidationContext context, IEdmRowType rowType)
		{
			if (!rowType.Properties().Any<IEdmProperty>())
			{
				context.AddError(rowType.Location(), EdmErrorCode.RowTypeMustHaveProperties, Strings.EdmModel_Validator_Semantic_RowTypeMustHaveProperties);
			}
		});

		// Token: 0x040005CA RID: 1482
		public static readonly ValidationRule<IEdmStructuralProperty> StructuralPropertyNullableComplexType = new ValidationRule<IEdmStructuralProperty>(delegate(ValidationContext context, IEdmStructuralProperty property)
		{
			if (property.Type.IsComplex() && property.Type.IsNullable)
			{
				context.AddError(property.Location(), EdmErrorCode.NullableComplexTypeProperty, Strings.EdmModel_Validator_Semantic_NullableComplexTypeProperty(property.Name));
			}
		});

		// Token: 0x040005CB RID: 1483
		public static readonly ValidationRule<IEdmStructuralProperty> StructuralPropertyInvalidPropertyType = new ValidationRule<IEdmStructuralProperty>(delegate(ValidationContext context, IEdmStructuralProperty property)
		{
			if (property.DeclaringType.TypeKind != EdmTypeKind.Row)
			{
				IEdmType edmType;
				if (property.Type.IsCollection())
				{
					edmType = property.Type.AsCollection().ElementType().Definition;
				}
				else
				{
					edmType = property.Type.Definition;
				}
				if (edmType.TypeKind != EdmTypeKind.Primitive && edmType.TypeKind != EdmTypeKind.Enum && edmType.TypeKind != EdmTypeKind.Complex && !context.IsBad(edmType))
				{
					context.AddError(property.Location(), EdmErrorCode.InvalidPropertyType, Strings.EdmModel_Validator_Semantic_InvalidPropertyType(property.Type.TypeKind().ToString()));
				}
			}
		});

		// Token: 0x040005CC RID: 1484
		public static readonly ValidationRule<IEdmStructuralProperty> StructuralPropertyInvalidPropertyTypeConcurrencyMode = new ValidationRule<IEdmStructuralProperty>(delegate(ValidationContext context, IEdmStructuralProperty property)
		{
			if (property.ConcurrencyMode == EdmConcurrencyMode.Fixed && !property.Type.IsPrimitive() && !context.IsBad(property.Type.Definition))
			{
				context.AddError(property.Location(), EdmErrorCode.InvalidPropertyType, Strings.EdmModel_Validator_Semantic_InvalidPropertyTypeConcurrencyMode(property.Type.IsCollection() ? "Collection" : property.Type.TypeKind().ToString()));
			}
		});

		// Token: 0x040005CD RID: 1485
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyInvalidOperationMultipleEndsInAssociation = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty navigationProperty)
		{
			if (navigationProperty.OnDelete != EdmOnDeleteAction.None && navigationProperty.Partner.OnDelete != EdmOnDeleteAction.None)
			{
				context.AddError(navigationProperty.Location(), EdmErrorCode.InvalidAction, Strings.EdmModel_Validator_Semantic_InvalidOperationMultipleEndsInAssociation);
			}
		});

		// Token: 0x040005CE RID: 1486
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyCorrectType = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty property)
		{
			bool flag2 = false;
			if (property.ToEntityType() != property.Partner.DeclaringEntityType())
			{
				flag2 = true;
			}
			else
			{
				switch (property.Partner.Multiplicity())
				{
				case EdmMultiplicity.ZeroOrOne:
					if (property.Type.IsCollection() || !property.Type.IsNullable)
					{
						flag2 = true;
					}
					break;
				case EdmMultiplicity.One:
					if (property.Type.IsCollection() || property.Type.IsNullable)
					{
						flag2 = true;
					}
					break;
				case EdmMultiplicity.Many:
					if (!property.Type.IsCollection())
					{
						flag2 = true;
					}
					break;
				}
			}
			if (flag2)
			{
				context.AddError(property.Location(), EdmErrorCode.InvalidNavigationPropertyType, Strings.EdmModel_Validator_Semantic_InvalidNavigationPropertyType(property.Name));
			}
		});

		// Token: 0x040005CF RID: 1487
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyDuplicateDependentProperty = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty navigationProperty)
		{
			IEnumerable<IEdmStructuralProperty> dependentProperties = navigationProperty.DependentProperties;
			if (dependentProperties != null)
			{
				HashSetInternal<string> hashSetInternal7 = new HashSetInternal<string>();
				foreach (IEdmStructuralProperty edmStructuralProperty6 in navigationProperty.DependentProperties)
				{
					if (edmStructuralProperty6 != null)
					{
						ValidationHelper.AddMemberNameToHashSet(edmStructuralProperty6, hashSetInternal7, context, EdmErrorCode.DuplicateDependentProperty, Strings.EdmModel_Validator_Semantic_DuplicateDependentProperty(edmStructuralProperty6.Name, navigationProperty.Name), false);
					}
				}
			}
		});

		// Token: 0x040005D0 RID: 1488
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyPrincipalEndMultiplicity = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty navigationProperty)
		{
			IEnumerable<IEdmStructuralProperty> dependentProperties2 = navigationProperty.DependentProperties;
			if (dependentProperties2 != null)
			{
				if (ValidationHelper.AllPropertiesAreNullable(dependentProperties2))
				{
					if (navigationProperty.Partner.Multiplicity() != EdmMultiplicity.ZeroOrOne)
					{
						context.AddError(navigationProperty.Partner.Location(), EdmErrorCode.InvalidMultiplicityOfPrincipalEnd, Strings.EdmModel_Validator_Semantic_InvalidMultiplicityOfPrincipalEndDependentPropertiesAllNullable(navigationProperty.Partner.Name, navigationProperty.Name));
						return;
					}
				}
				else if (!ValidationHelper.HasNullableProperty(dependentProperties2))
				{
					if (navigationProperty.Partner.Multiplicity() != EdmMultiplicity.One)
					{
						context.AddError(navigationProperty.Partner.Location(), EdmErrorCode.InvalidMultiplicityOfPrincipalEnd, Strings.EdmModel_Validator_Semantic_InvalidMultiplicityOfPrincipalEndDependentPropertiesAllNonnullable(navigationProperty.Partner.Name, navigationProperty.Name));
						return;
					}
				}
				else if (navigationProperty.Partner.Multiplicity() != EdmMultiplicity.One && navigationProperty.Partner.Multiplicity() != EdmMultiplicity.ZeroOrOne)
				{
					context.AddError(navigationProperty.Partner.Location(), EdmErrorCode.InvalidMultiplicityOfPrincipalEnd, Strings.EdmModel_Validator_Semantic_NavigationPropertyPrincipalEndMultiplicityUpperBoundMustBeOne(navigationProperty.Name));
				}
			}
		});

		// Token: 0x040005D1 RID: 1489
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyDependentEndMultiplicity = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty navigationProperty)
		{
			IEnumerable<IEdmStructuralProperty> dependentProperties3 = navigationProperty.DependentProperties;
			if (dependentProperties3 != null)
			{
				if (ValidationHelper.PropertySetsAreEquivalent(navigationProperty.DeclaringEntityType().Key(), dependentProperties3))
				{
					if (navigationProperty.Multiplicity() != EdmMultiplicity.ZeroOrOne && navigationProperty.Multiplicity() != EdmMultiplicity.One)
					{
						context.AddError(navigationProperty.Location(), EdmErrorCode.InvalidMultiplicityOfDependentEnd, Strings.EdmModel_Validator_Semantic_InvalidMultiplicityOfDependentEndMustBeZeroOneOrOne(navigationProperty.Name));
						return;
					}
				}
				else if (navigationProperty.Multiplicity() != EdmMultiplicity.Many)
				{
					context.AddError(navigationProperty.Location(), EdmErrorCode.InvalidMultiplicityOfDependentEnd, Strings.EdmModel_Validator_Semantic_InvalidMultiplicityOfDependentEndMustBeMany(navigationProperty.Name));
				}
			}
		});

		// Token: 0x040005D2 RID: 1490
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyDependentPropertiesMustBelongToDependentEntity = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty navigationProperty)
		{
			IEnumerable<IEdmStructuralProperty> dependentProperties4 = navigationProperty.DependentProperties;
			if (dependentProperties4 != null)
			{
				IEdmEntityType edmEntityType = navigationProperty.DeclaringEntityType();
				foreach (IEdmStructuralProperty edmStructuralProperty7 in dependentProperties4)
				{
					if (!context.IsBad(edmStructuralProperty7))
					{
						IEdmProperty edmProperty4 = edmEntityType.FindProperty(edmStructuralProperty7.Name);
						if (edmProperty4 != edmStructuralProperty7)
						{
							context.AddError(navigationProperty.Location(), EdmErrorCode.DependentPropertiesMustBelongToDependentEntity, Strings.EdmModel_Validator_Semantic_DependentPropertiesMustBelongToDependentEntity(edmStructuralProperty7.Name, edmEntityType.Name));
						}
					}
				}
			}
		});

		// Token: 0x040005D3 RID: 1491
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyInvalidToPropertyInRelationshipConstraintBeforeV2 = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty navigationProperty)
		{
			IEnumerable<IEdmStructuralProperty> dependentProperties5 = navigationProperty.DependentProperties;
			if (dependentProperties5 != null && !ValidationHelper.PropertySetIsSubset(navigationProperty.DeclaringEntityType().Key(), dependentProperties5))
			{
				string text2 = Strings.EdmModel_Validator_Semantic_InvalidToPropertyInRelationshipConstraint(navigationProperty.Name, navigationProperty.DeclaringEntityType().FullName());
				context.AddError(navigationProperty.Location(), EdmErrorCode.InvalidPropertyInRelationshipConstraint, text2);
			}
		});

		// Token: 0x040005D4 RID: 1492
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyEndWithManyMultiplicityCannotHaveOperationsSpecified = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty end)
		{
			if (end.Multiplicity() == EdmMultiplicity.Many && end.OnDelete != EdmOnDeleteAction.None)
			{
				string text3 = Strings.EdmModel_Validator_Semantic_EndWithManyMultiplicityCannotHaveOperationsSpecified(end.Name);
				context.AddError(end.Location(), EdmErrorCode.EndWithManyMultiplicityCannotHaveOperationsSpecified, text3);
			}
		});

		// Token: 0x040005D5 RID: 1493
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyContainsTargetNotSupportedBeforeV3 = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty property)
		{
			if (property.ContainsTarget)
			{
				context.AddError(property.Location(), EdmErrorCode.NavigationPropertyContainsTargetNotSupportedBeforeV3, Strings.EdmModel_Validator_Semantic_NavigationPropertyContainsTargetNotSupportedBeforeV3);
			}
		});

		// Token: 0x040005D6 RID: 1494
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyWithRecursiveContainmentTargetMustBeOptional = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty property)
		{
			if (property.ContainsTarget && property.DeclaringType.IsOrInheritsFrom(property.ToEntityType()) && !property.Type.IsCollection() && !property.Type.IsNullable)
			{
				context.AddError(property.Location(), EdmErrorCode.NavigationPropertyWithRecursiveContainmentTargetMustBeOptional, Strings.EdmModel_Validator_Semantic_NavigationPropertyWithRecursiveContainmentTargetMustBeOptional(property.Name));
			}
		});

		// Token: 0x040005D7 RID: 1495
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyWithRecursiveContainmentSourceMustBeFromZeroOrOne = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty property)
		{
			if (property.ContainsTarget && property.DeclaringType.IsOrInheritsFrom(property.ToEntityType()) && property.Multiplicity() != EdmMultiplicity.ZeroOrOne)
			{
				context.AddError(property.Location(), EdmErrorCode.NavigationPropertyWithRecursiveContainmentSourceMustBeFromZeroOrOne, Strings.EdmModel_Validator_Semantic_NavigationPropertyWithRecursiveContainmentSourceMustBeFromZeroOrOne(property.Name));
			}
		});

		// Token: 0x040005D8 RID: 1496
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyWithNonRecursiveContainmentSourceMustBeFromOne = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty property)
		{
			if (property.ContainsTarget && !property.DeclaringType.IsOrInheritsFrom(property.ToEntityType()) && property.Multiplicity() != EdmMultiplicity.One)
			{
				context.AddError(property.Location(), EdmErrorCode.NavigationPropertyWithNonRecursiveContainmentSourceMustBeFromOne, Strings.EdmModel_Validator_Semantic_NavigationPropertyWithNonRecursiveContainmentSourceMustBeFromOne(property.Name));
			}
		});

		// Token: 0x040005D9 RID: 1497
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyEntityMustNotIndirectlyContainItself = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty property)
		{
			if (property.ContainsTarget && !property.DeclaringType.IsOrInheritsFrom(property.ToEntityType()) && ValidationHelper.TypeIndirectlyContainsTarget(property.ToEntityType(), property.DeclaringEntityType(), new HashSetInternal<IEdmEntityType>(), context.Model))
			{
				context.AddError(property.Location(), EdmErrorCode.NavigationPropertyEntityMustNotIndirectlyContainItself, Strings.EdmModel_Validator_Semantic_NavigationPropertyEntityMustNotIndirectlyContainItself(property.Name));
			}
		});

		// Token: 0x040005DA RID: 1498
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyTypeMismatchRelationshipConstraint = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty navigationProperty)
		{
			IEnumerable<IEdmStructuralProperty> dependentProperties6 = navigationProperty.DependentProperties;
			if (dependentProperties6 != null)
			{
				int num = dependentProperties6.Count<IEdmStructuralProperty>();
				IEdmEntityType edmEntityType2 = navigationProperty.Partner.DeclaringEntityType();
				IEnumerable<IEdmStructuralProperty> enumerable2 = edmEntityType2.Key();
				if (num == enumerable2.Count<IEdmStructuralProperty>())
				{
					for (int i = 0; i < num; i++)
					{
						if (!navigationProperty.DependentProperties.ElementAtOrDefault(i).Type.Definition.IsEquivalentTo(enumerable2.ElementAtOrDefault(i).Type.Definition))
						{
							string text4 = Strings.EdmModel_Validator_Semantic_TypeMismatchRelationshipConstraint(navigationProperty.DependentProperties.ToList<IEdmStructuralProperty>()[i].Name, navigationProperty.DeclaringEntityType().FullName(), enumerable2.ToList<IEdmStructuralProperty>()[i].Name, edmEntityType2.Name, "Fred");
							context.AddError(navigationProperty.Location(), EdmErrorCode.TypeMismatchRelationshipConstraint, text4);
						}
					}
				}
			}
		});

		// Token: 0x040005DB RID: 1499
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyAssociationNameIsValid = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty property)
		{
			if (property.IsPrincipal)
			{
				ValidationRules.CheckForNameError(context, context.Model.GetAssociationName(property), property.Location());
			}
		});

		// Token: 0x040005DC RID: 1500
		public static readonly ValidationRule<IEdmNavigationProperty> NavigationPropertyAssociationEndNameIsValid = new ValidationRule<IEdmNavigationProperty>(delegate(ValidationContext context, IEdmNavigationProperty property)
		{
			ValidationRules.CheckForNameError(context, context.Model.GetAssociationEndName(property), property.Location());
		});

		// Token: 0x040005DD RID: 1501
		public static readonly ValidationRule<IEdmProperty> PropertyMustNotHaveKindOfNone = new ValidationRule<IEdmProperty>(delegate(ValidationContext context, IEdmProperty property)
		{
			if (property.PropertyKind == EdmPropertyKind.None && !context.IsBad(property))
			{
				context.AddError(property.Location(), EdmErrorCode.PropertyMustNotHaveKindOfNone, Strings.EdmModel_Validator_Semantic_PropertyMustNotHaveKindOfNone(property.Name));
			}
		});

		// Token: 0x040005DE RID: 1502
		public static readonly ValidationRule<IEdmFunction> FunctionsNotSupportedBeforeV2 = new ValidationRule<IEdmFunction>(delegate(ValidationContext context, IEdmFunction function)
		{
			context.AddError(function.Location(), EdmErrorCode.FunctionsNotSupportedBeforeV2, Strings.EdmModel_Validator_Semantic_FunctionsNotSupportedBeforeV2);
		});

		// Token: 0x040005DF RID: 1503
		public static readonly ValidationRule<IEdmFunction> FunctionOnlyInputParametersAllowedInFunctions = new ValidationRule<IEdmFunction>(delegate(ValidationContext context, IEdmFunction function)
		{
			foreach (IEdmFunctionParameter edmFunctionParameter in function.Parameters)
			{
				if (edmFunctionParameter.Mode != EdmFunctionParameterMode.In)
				{
					context.AddError(edmFunctionParameter.Location(), EdmErrorCode.OnlyInputParametersAllowedInFunctions, Strings.EdmModel_Validator_Semantic_OnlyInputParametersAllowedInFunctions(edmFunctionParameter.Name, function.Name));
				}
			}
		});

		// Token: 0x040005E0 RID: 1504
		public static readonly ValidationRule<IEdmFunctionImport> FunctionImportUnsupportedReturnTypeV1 = new ValidationRule<IEdmFunctionImport>(delegate(ValidationContext context, IEdmFunctionImport functionImport)
		{
			if (functionImport.ReturnType != null)
			{
				bool flag3;
				if (functionImport.ReturnType.IsCollection())
				{
					IEdmTypeReference edmTypeReference = functionImport.ReturnType.AsCollection().ElementType();
					flag3 = !edmTypeReference.IsPrimitive() && !edmTypeReference.IsEntity() && !context.IsBad(edmTypeReference.Definition);
				}
				else
				{
					flag3 = true;
				}
				if (flag3 && !context.IsBad(functionImport.ReturnType.Definition))
				{
					context.AddError(functionImport.Location(), EdmErrorCode.FunctionImportUnsupportedReturnType, Strings.EdmModel_Validator_Semantic_FunctionImportWithUnsupportedReturnTypeV1(functionImport.Name));
				}
			}
		});

		// Token: 0x040005E1 RID: 1505
		public static readonly ValidationRule<IEdmFunctionImport> FunctionImportUnsupportedReturnTypeAfterV1 = new ValidationRule<IEdmFunctionImport>(delegate(ValidationContext context, IEdmFunctionImport functionImport)
		{
			if (functionImport.ReturnType != null)
			{
				IEdmTypeReference edmTypeReference2 = (functionImport.ReturnType.IsCollection() ? functionImport.ReturnType.AsCollection().ElementType() : functionImport.ReturnType);
				if (!edmTypeReference2.IsPrimitive() && !edmTypeReference2.IsEntity() && !edmTypeReference2.IsComplex() && !edmTypeReference2.IsEnum() && !context.IsBad(edmTypeReference2.Definition))
				{
					context.AddError(functionImport.Location(), EdmErrorCode.FunctionImportUnsupportedReturnType, Strings.EdmModel_Validator_Semantic_FunctionImportWithUnsupportedReturnTypeAfterV1(functionImport.Name));
				}
			}
		});

		// Token: 0x040005E2 RID: 1506
		public static readonly ValidationRule<IEdmFunctionImport> FunctionImportReturnEntitiesButDoesNotSpecifyEntitySet = new ValidationRule<IEdmFunctionImport>(delegate(ValidationContext context, IEdmFunctionImport functionImport)
		{
			if (functionImport.ReturnType != null && functionImport.EntitySet == null)
			{
				IEdmTypeReference edmTypeReference3 = (functionImport.ReturnType.IsCollection() ? functionImport.ReturnType.AsCollection().ElementType() : functionImport.ReturnType);
				if (edmTypeReference3.IsEntity() && !context.IsBad(edmTypeReference3.Definition))
				{
					context.AddError(functionImport.Location(), EdmErrorCode.FunctionImportReturnsEntitiesButDoesNotSpecifyEntitySet, Strings.EdmModel_Validator_Semantic_FunctionImportReturnEntitiesButDoesNotSpecifyEntitySet(functionImport.Name));
				}
			}
		});

		// Token: 0x040005E3 RID: 1507
		public static readonly ValidationRule<IEdmFunctionImport> FunctionImportEntitySetExpressionIsInvalid = new ValidationRule<IEdmFunctionImport>(delegate(ValidationContext context, IEdmFunctionImport functionImport)
		{
			if (functionImport.EntitySet != null)
			{
				if (functionImport.EntitySet.ExpressionKind != EdmExpressionKind.EntitySetReference && functionImport.EntitySet.ExpressionKind != EdmExpressionKind.Path)
				{
					context.AddError(functionImport.Location(), EdmErrorCode.FunctionImportEntitySetExpressionIsInvalid, Strings.EdmModel_Validator_Semantic_FunctionImportEntitySetExpressionKindIsInvalid(functionImport.Name, functionImport.EntitySet.ExpressionKind));
					return;
				}
				IEdmEntitySet edmEntitySet3;
				IEdmFunctionParameter edmFunctionParameter2;
				IEnumerable<IEdmNavigationProperty> enumerable3;
				if (!functionImport.TryGetStaticEntitySet(out edmEntitySet3) && !functionImport.TryGetRelativeEntitySetPath(context.Model, out edmFunctionParameter2, out enumerable3))
				{
					context.AddError(functionImport.Location(), EdmErrorCode.FunctionImportEntitySetExpressionIsInvalid, Strings.EdmModel_Validator_Semantic_FunctionImportEntitySetExpressionIsInvalid(functionImport.Name));
				}
			}
		});

		// Token: 0x040005E4 RID: 1508
		public static readonly ValidationRule<IEdmFunctionImport> FunctionImportEntityTypeDoesNotMatchEntitySet = new ValidationRule<IEdmFunctionImport>(delegate(ValidationContext context, IEdmFunctionImport functionImport)
		{
			if (functionImport.EntitySet != null && functionImport.ReturnType != null)
			{
				IEdmTypeReference edmTypeReference4 = (functionImport.ReturnType.IsCollection() ? functionImport.ReturnType.AsCollection().ElementType() : functionImport.ReturnType);
				if (edmTypeReference4.IsEntity())
				{
					IEdmEntityType edmEntityType3 = edmTypeReference4.AsEntity().EntityDefinition();
					IEdmEntitySet edmEntitySet4;
					IEdmFunctionParameter edmFunctionParameter3;
					IEnumerable<IEdmNavigationProperty> enumerable4;
					if (functionImport.TryGetStaticEntitySet(out edmEntitySet4))
					{
						string text5 = Strings.EdmModel_Validator_Semantic_FunctionImportEntityTypeDoesNotMatchEntitySet(functionImport.Name, edmEntityType3.FullName(), edmEntitySet4.Name);
						IEdmEntityType elementType = edmEntitySet4.ElementType;
						if (!edmEntityType3.IsOrInheritsFrom(elementType) && !context.IsBad(edmEntityType3) && !context.IsBad(edmEntitySet4) && !context.IsBad(elementType))
						{
							context.AddError(functionImport.Location(), EdmErrorCode.FunctionImportEntityTypeDoesNotMatchEntitySet, text5);
							return;
						}
					}
					else if (functionImport.TryGetRelativeEntitySetPath(context.Model, out edmFunctionParameter3, out enumerable4))
					{
						List<IEdmNavigationProperty> list3 = enumerable4.ToList<IEdmNavigationProperty>();
						IEdmTypeReference edmTypeReference5 = ((list3.Count == 0) ? edmFunctionParameter3.Type : enumerable4.Last<IEdmNavigationProperty>().Type);
						IEdmTypeReference edmTypeReference6 = (edmTypeReference5.IsCollection() ? edmTypeReference5.AsCollection().ElementType() : edmTypeReference5);
						if (!edmEntityType3.IsOrInheritsFrom(edmTypeReference6.Definition) && !context.IsBad(edmEntityType3) && !context.IsBad(edmTypeReference6.Definition))
						{
							context.AddError(functionImport.Location(), EdmErrorCode.FunctionImportEntityTypeDoesNotMatchEntitySet, Strings.EdmModel_Validator_Semantic_FunctionImportEntityTypeDoesNotMatchEntitySet2(functionImport.Name, edmTypeReference4.FullName()));
							return;
						}
					}
				}
				else if (!context.IsBad(edmTypeReference4.Definition))
				{
					context.AddError(functionImport.Location(), EdmErrorCode.FunctionImportSpecifiesEntitySetButDoesNotReturnEntityType, Strings.EdmModel_Validator_Semantic_FunctionImportSpecifiesEntitySetButNotEntityType(functionImport.Name));
				}
			}
		});

		// Token: 0x040005E5 RID: 1509
		public static readonly ValidationRule<IEdmFunctionImport> ComposableFunctionImportMustHaveReturnType = new ValidationRule<IEdmFunctionImport>(delegate(ValidationContext context, IEdmFunctionImport functionImport)
		{
			if (functionImport.IsComposable && functionImport.ReturnType == null)
			{
				context.AddError(functionImport.Location(), EdmErrorCode.ComposableFunctionImportMustHaveReturnType, Strings.EdmModel_Validator_Semantic_ComposableFunctionImportMustHaveReturnType(functionImport.Name));
			}
		});

		// Token: 0x040005E6 RID: 1510
		public static readonly ValidationRule<IEdmFunctionImport> FunctionImportParametersIncorrectTypeBeforeV3 = new ValidationRule<IEdmFunctionImport>(delegate(ValidationContext context, IEdmFunctionImport functionImport)
		{
			foreach (IEdmFunctionParameter edmFunctionParameter4 in functionImport.Parameters)
			{
				IEdmTypeReference type2 = edmFunctionParameter4.Type;
				if (!type2.IsPrimitive() && !type2.IsComplex() && !context.IsBad(type2.Definition))
				{
					context.AddError(edmFunctionParameter4.Location(), EdmErrorCode.FunctionImportParameterIncorrectType, Strings.EdmModel_Validator_Semantic_FunctionImportParameterIncorrectType(type2.FullName(), edmFunctionParameter4.Name));
				}
			}
		});

		// Token: 0x040005E7 RID: 1511
		public static readonly ValidationRule<IEdmFunctionImport> FunctionImportIsSideEffectingNotSupportedBeforeV3 = new ValidationRule<IEdmFunctionImport>(delegate(ValidationContext context, IEdmFunctionImport functionImport)
		{
			if (!functionImport.IsSideEffecting)
			{
				context.AddError(functionImport.Location(), EdmErrorCode.FunctionImportSideEffectingNotSupportedBeforeV3, Strings.EdmModel_Validator_Semantic_FunctionImportSideEffectingNotSupportedBeforeV3);
			}
		});

		// Token: 0x040005E8 RID: 1512
		public static readonly ValidationRule<IEdmFunctionImport> FunctionImportIsComposableNotSupportedBeforeV3 = new ValidationRule<IEdmFunctionImport>(delegate(ValidationContext context, IEdmFunctionImport functionImport)
		{
			if (functionImport.IsComposable)
			{
				context.AddError(functionImport.Location(), EdmErrorCode.FunctionImportComposableNotSupportedBeforeV3, Strings.EdmModel_Validator_Semantic_FunctionImportComposableNotSupportedBeforeV3);
			}
		});

		// Token: 0x040005E9 RID: 1513
		public static readonly ValidationRule<IEdmFunctionImport> FunctionImportIsBindableNotSupportedBeforeV3 = new ValidationRule<IEdmFunctionImport>(delegate(ValidationContext context, IEdmFunctionImport functionImport)
		{
			if (functionImport.IsBindable)
			{
				context.AddError(functionImport.Location(), EdmErrorCode.FunctionImportBindableNotSupportedBeforeV3, Strings.EdmModel_Validator_Semantic_FunctionImportBindableNotSupportedBeforeV3);
			}
		});

		// Token: 0x040005EA RID: 1514
		public static readonly ValidationRule<IEdmFunctionImport> FunctionImportComposableFunctionImportCannotBeSideEffecting = new ValidationRule<IEdmFunctionImport>(delegate(ValidationContext context, IEdmFunctionImport functionImport)
		{
			if (functionImport.IsComposable && functionImport.IsSideEffecting)
			{
				context.AddError(functionImport.Location(), EdmErrorCode.ComposableFunctionImportCannotBeSideEffecting, Strings.EdmModel_Validator_Semantic_ComposableFunctionImportCannotBeSideEffecting(functionImport.Name));
			}
		});

		// Token: 0x040005EB RID: 1515
		public static readonly ValidationRule<IEdmFunctionImport> FunctionImportBindableFunctionImportMustHaveParameters = new ValidationRule<IEdmFunctionImport>(delegate(ValidationContext context, IEdmFunctionImport functionImport)
		{
			if (functionImport.IsBindable && functionImport.Parameters.Count<IEdmFunctionParameter>() == 0)
			{
				context.AddError(functionImport.Location(), EdmErrorCode.BindableFunctionImportMustHaveParameters, Strings.EdmModel_Validator_Semantic_BindableFunctionImportMustHaveParameters(functionImport.Name));
			}
		});

		// Token: 0x040005EC RID: 1516
		public static readonly ValidationRule<IEdmFunctionImport> FunctionImportParametersCannotHaveModeOfNone = new ValidationRule<IEdmFunctionImport>(delegate(ValidationContext context, IEdmFunctionImport function)
		{
			foreach (IEdmFunctionParameter edmFunctionParameter5 in function.Parameters)
			{
				if (edmFunctionParameter5.Mode == EdmFunctionParameterMode.None && !context.IsBad(function))
				{
					context.AddError(edmFunctionParameter5.Location(), EdmErrorCode.InvalidFunctionImportParameterMode, Strings.EdmModel_Validator_Semantic_InvalidFunctionImportParameterMode(edmFunctionParameter5.Name, function.Name));
				}
			}
		});

		// Token: 0x040005ED RID: 1517
		public static readonly ValidationRule<IEdmFunctionBase> FunctionBaseParameterNameAlreadyDefinedDuplicate = new ValidationRule<IEdmFunctionBase>(delegate(ValidationContext context, IEdmFunctionBase edmFunction)
		{
			HashSetInternal<string> hashSetInternal8 = new HashSetInternal<string>();
			if (edmFunction.Parameters != null)
			{
				foreach (IEdmFunctionParameter edmFunctionParameter6 in edmFunction.Parameters)
				{
					ValidationHelper.AddMemberNameToHashSet(edmFunctionParameter6, hashSetInternal8, context, EdmErrorCode.AlreadyDefined, Strings.EdmModel_Validator_Semantic_ParameterNameAlreadyDefinedDuplicate(edmFunctionParameter6.Name), false);
				}
			}
		});

		// Token: 0x040005EE RID: 1518
		public static readonly ValidationRule<IEdmTypeReference> TypeReferenceInaccessibleSchemaType = new ValidationRule<IEdmTypeReference>(delegate(ValidationContext context, IEdmTypeReference typeReference)
		{
			IEdmSchemaType edmSchemaType3 = typeReference.Definition as IEdmSchemaType;
			if (edmSchemaType3 != null && !context.IsBad(edmSchemaType3))
			{
				ValidationRules.CheckForUnreacheableTypeError(context, edmSchemaType3, typeReference.Location());
			}
		});

		// Token: 0x040005EF RID: 1519
		public static readonly ValidationRule<IEdmPrimitiveTypeReference> StreamTypeReferencesNotSupportedBeforeV3 = new ValidationRule<IEdmPrimitiveTypeReference>(delegate(ValidationContext context, IEdmPrimitiveTypeReference type)
		{
			if (type.IsStream())
			{
				context.AddError(type.Location(), EdmErrorCode.StreamTypeReferencesNotSupportedBeforeV3, Strings.EdmModel_Validator_Semantic_StreamTypeReferencesNotSupportedBeforeV3);
			}
		});

		// Token: 0x040005F0 RID: 1520
		public static readonly ValidationRule<IEdmPrimitiveTypeReference> SpatialTypeReferencesNotSupportedBeforeV3 = new ValidationRule<IEdmPrimitiveTypeReference>(delegate(ValidationContext context, IEdmPrimitiveTypeReference type)
		{
			if (type.IsSpatial())
			{
				context.AddError(type.Location(), EdmErrorCode.SpatialTypeReferencesNotSupportedBeforeV3, Strings.EdmModel_Validator_Semantic_SpatialTypeReferencesNotSupportedBeforeV3);
			}
		});

		// Token: 0x040005F1 RID: 1521
		public static readonly ValidationRule<IEdmDecimalTypeReference> DecimalTypeReferenceScaleOutOfRange = new ValidationRule<IEdmDecimalTypeReference>(delegate(ValidationContext context, IEdmDecimalTypeReference type)
		{
			if (type.Scale > type.Precision || type.Scale < 0)
			{
				context.AddError(type.Location(), EdmErrorCode.ScaleOutOfRange, Strings.EdmModel_Validator_Semantic_ScaleOutOfRange);
			}
		});

		// Token: 0x040005F2 RID: 1522
		public static readonly ValidationRule<IEdmDecimalTypeReference> DecimalTypeReferencePrecisionOutOfRange = new ValidationRule<IEdmDecimalTypeReference>(delegate(ValidationContext context, IEdmDecimalTypeReference type)
		{
			if (type.Precision > 2147483647 || type.Precision < 0)
			{
				context.AddError(type.Location(), EdmErrorCode.PrecisionOutOfRange, Strings.EdmModel_Validator_Semantic_PrecisionOutOfRange);
			}
		});

		// Token: 0x040005F3 RID: 1523
		public static readonly ValidationRule<IEdmStringTypeReference> StringTypeReferenceStringMaxLengthNegative = new ValidationRule<IEdmStringTypeReference>(delegate(ValidationContext context, IEdmStringTypeReference type)
		{
			if (type.MaxLength < 0)
			{
				context.AddError(type.Location(), EdmErrorCode.MaxLengthOutOfRange, Strings.EdmModel_Validator_Semantic_StringMaxLengthOutOfRange);
			}
		});

		// Token: 0x040005F4 RID: 1524
		public static readonly ValidationRule<IEdmStringTypeReference> StringTypeReferenceStringUnboundedNotValidForMaxLength = new ValidationRule<IEdmStringTypeReference>(delegate(ValidationContext context, IEdmStringTypeReference type)
		{
			if (type.MaxLength != null && type.IsUnbounded)
			{
				context.AddError(type.Location(), EdmErrorCode.IsUnboundedCannotBeTrueWhileMaxLengthIsNotNull, Strings.EdmModel_Validator_Semantic_IsUnboundedCannotBeTrueWhileMaxLengthIsNotNull);
			}
		});

		// Token: 0x040005F5 RID: 1525
		public static readonly ValidationRule<IEdmBinaryTypeReference> BinaryTypeReferenceBinaryMaxLengthNegative = new ValidationRule<IEdmBinaryTypeReference>(delegate(ValidationContext context, IEdmBinaryTypeReference type)
		{
			if (type.MaxLength < 0)
			{
				context.AddError(type.Location(), EdmErrorCode.MaxLengthOutOfRange, Strings.EdmModel_Validator_Semantic_MaxLengthOutOfRange);
			}
		});

		// Token: 0x040005F6 RID: 1526
		public static readonly ValidationRule<IEdmBinaryTypeReference> BinaryTypeReferenceBinaryUnboundedNotValidForMaxLength = new ValidationRule<IEdmBinaryTypeReference>(delegate(ValidationContext context, IEdmBinaryTypeReference type)
		{
			if (type.MaxLength != null && type.IsUnbounded)
			{
				context.AddError(type.Location(), EdmErrorCode.IsUnboundedCannotBeTrueWhileMaxLengthIsNotNull, Strings.EdmModel_Validator_Semantic_IsUnboundedCannotBeTrueWhileMaxLengthIsNotNull);
			}
		});

		// Token: 0x040005F7 RID: 1527
		public static readonly ValidationRule<IEdmTemporalTypeReference> TemporalTypeReferencePrecisionOutOfRange = new ValidationRule<IEdmTemporalTypeReference>(delegate(ValidationContext context, IEdmTemporalTypeReference type)
		{
			if (type.Precision > 2147483647 || type.Precision < 0)
			{
				context.AddError(type.Location(), EdmErrorCode.PrecisionOutOfRange, Strings.EdmModel_Validator_Semantic_PrecisionOutOfRange);
			}
		});

		// Token: 0x040005F8 RID: 1528
		public static readonly ValidationRule<IEdmModel> ModelDuplicateSchemaElementNameBeforeV3 = new ValidationRule<IEdmModel>(delegate(ValidationContext context, IEdmModel model)
		{
			HashSetInternal<string> hashSetInternal9 = new HashSetInternal<string>();
			Dictionary<string, List<IEdmFunction>> dictionary2 = new Dictionary<string, List<IEdmFunction>>();
			foreach (IEdmSchemaElement edmSchemaElement in model.SchemaElements)
			{
				bool flag4 = false;
				string text6 = edmSchemaElement.FullName();
				if (edmSchemaElement.SchemaElementKind != EdmSchemaElementKind.EntityContainer)
				{
					IEdmFunction function = edmSchemaElement as IEdmFunction;
					if (function != null)
					{
						if (hashSetInternal9.Contains(text6))
						{
							flag4 = true;
						}
						else
						{
							List<IEdmFunction> list4;
							if (dictionary2.TryGetValue(text6, out list4))
							{
								if (list4.Any((IEdmFunction existingFunction) => function.IsFunctionSignatureEquivalentTo(existingFunction)))
								{
									flag4 = true;
								}
							}
							else
							{
								list4 = new List<IEdmFunction>();
								dictionary2[text6] = list4;
							}
							list4.Add(function);
						}
						if (!flag4)
						{
							flag4 = model.FunctionOrNameExistsInReferencedModel(function, text6, false);
						}
					}
					else
					{
						flag4 = !hashSetInternal9.Add(text6) || dictionary2.ContainsKey(text6) || model.ItemExistsInReferencedModel(text6, false);
					}
					if (flag4)
					{
						context.AddError(edmSchemaElement.Location(), EdmErrorCode.AlreadyDefined, Strings.EdmModel_Validator_Semantic_SchemaElementNameAlreadyDefined(text6));
					}
				}
			}
		});

		// Token: 0x040005F9 RID: 1529
		public static readonly ValidationRule<IEdmModel> ModelDuplicateSchemaElementName = new ValidationRule<IEdmModel>(delegate(ValidationContext context, IEdmModel model)
		{
			HashSetInternal<string> hashSetInternal10 = new HashSetInternal<string>();
			Dictionary<string, List<IEdmFunction>> dictionary3 = new Dictionary<string, List<IEdmFunction>>();
			foreach (IEdmSchemaElement edmSchemaElement2 in model.SchemaElements)
			{
				bool flag5 = false;
				string text7 = edmSchemaElement2.FullName();
				IEdmFunction function = edmSchemaElement2 as IEdmFunction;
				if (function != null)
				{
					if (hashSetInternal10.Contains(text7))
					{
						flag5 = true;
					}
					else
					{
						List<IEdmFunction> list5;
						if (dictionary3.TryGetValue(text7, out list5))
						{
							if (list5.Any((IEdmFunction existingFunction) => function.IsFunctionSignatureEquivalentTo(existingFunction)))
							{
								flag5 = true;
							}
						}
						else
						{
							list5 = new List<IEdmFunction>();
							dictionary3[text7] = list5;
						}
						list5.Add(function);
					}
					if (!flag5)
					{
						flag5 = model.FunctionOrNameExistsInReferencedModel(function, text7, true);
					}
				}
				else
				{
					flag5 = !hashSetInternal10.Add(text7) || dictionary3.ContainsKey(text7) || model.ItemExistsInReferencedModel(text7, true);
				}
				if (flag5)
				{
					context.AddError(edmSchemaElement2.Location(), EdmErrorCode.AlreadyDefined, Strings.EdmModel_Validator_Semantic_SchemaElementNameAlreadyDefined(text7));
				}
			}
		});

		// Token: 0x040005FA RID: 1530
		public static readonly ValidationRule<IEdmModel> ModelDuplicateEntityContainerName = new ValidationRule<IEdmModel>(delegate(ValidationContext context, IEdmModel model)
		{
			HashSetInternal<string> hashSetInternal11 = new HashSetInternal<string>();
			foreach (IEdmEntityContainer edmEntityContainer in model.EntityContainers())
			{
				ValidationHelper.AddMemberNameToHashSet(edmEntityContainer, hashSetInternal11, context, EdmErrorCode.DuplicateEntityContainerName, Strings.EdmModel_Validator_Semantic_DuplicateEntityContainerName(edmEntityContainer.Name), false);
			}
		});

		// Token: 0x040005FB RID: 1531
		public static readonly ValidationRule<IEdmDirectValueAnnotation> ImmediateValueAnnotationElementAnnotationIsValid = new ValidationRule<IEdmDirectValueAnnotation>(delegate(ValidationContext context, IEdmDirectValueAnnotation annotation)
		{
			IEdmStringValue edmStringValue = annotation.Value as IEdmStringValue;
			if (edmStringValue != null && edmStringValue.IsSerializedAsElement(context.Model) && (EdmUtil.IsNullOrWhiteSpaceInternal(annotation.NamespaceUri) || EdmUtil.IsNullOrWhiteSpaceInternal(annotation.Name)))
			{
				context.AddError(annotation.Location(), EdmErrorCode.InvalidElementAnnotation, Strings.EdmModel_Validator_Semantic_InvalidElementAnnotationMismatchedTerm);
			}
		});

		// Token: 0x040005FC RID: 1532
		public static readonly ValidationRule<IEdmDirectValueAnnotation> ImmediateValueAnnotationElementAnnotationHasNameAndNamespace = new ValidationRule<IEdmDirectValueAnnotation>(delegate(ValidationContext context, IEdmDirectValueAnnotation annotation)
		{
			IEdmStringValue edmStringValue2 = annotation.Value as IEdmStringValue;
			EdmError edmError;
			if (edmStringValue2 != null && edmStringValue2.IsSerializedAsElement(context.Model) && !ValidationHelper.ValidateValueCanBeWrittenAsXmlElementAnnotation(edmStringValue2, annotation.NamespaceUri, annotation.Name, out edmError))
			{
				context.AddError(edmError);
			}
		});

		// Token: 0x040005FD RID: 1533
		public static readonly ValidationRule<IEdmDirectValueAnnotation> DirectValueAnnotationHasXmlSerializableName = new ValidationRule<IEdmDirectValueAnnotation>(delegate(ValidationContext context, IEdmDirectValueAnnotation annotation)
		{
			string name = annotation.Name;
			if (!EdmUtil.IsNullOrWhiteSpaceInternal(name) && name.Length <= 480 && name.Length > 0)
			{
				try
				{
					XmlConvert.VerifyNCName(annotation.Name);
				}
				catch (Exception)
				{
					IEdmValue edmValue = annotation.Value as IEdmValue;
					EdmLocation edmLocation = ((edmValue == null) ? null : edmValue.Location());
					context.AddError(new EdmError(edmLocation, EdmErrorCode.InvalidName, Strings.EdmModel_Validator_Syntactic_EdmModel_NameIsNotAllowed(annotation.Name)));
				}
			}
		});

		// Token: 0x040005FE RID: 1534
		public static readonly ValidationRule<IEdmVocabularyAnnotation> VocabularyAnnotationsNotSupportedBeforeV3 = new ValidationRule<IEdmVocabularyAnnotation>(delegate(ValidationContext context, IEdmVocabularyAnnotation vocabularyAnnotation)
		{
			context.AddError(vocabularyAnnotation.Location(), EdmErrorCode.VocabularyAnnotationsNotSupportedBeforeV3, Strings.EdmModel_Validator_Semantic_VocabularyAnnotationsNotSupportedBeforeV3);
		});

		// Token: 0x040005FF RID: 1535
		public static readonly ValidationRule<IEdmVocabularyAnnotation> VocabularyAnnotationInaccessibleTarget = new ValidationRule<IEdmVocabularyAnnotation>(delegate(ValidationContext context, IEdmVocabularyAnnotation annotation)
		{
			IEdmVocabularyAnnotatable target = annotation.Target;
			bool flag6 = false;
			IEdmEntityContainer edmEntityContainer2 = target as IEdmEntityContainer;
			if (edmEntityContainer2 != null)
			{
				flag6 = context.Model.FindEntityContainer(edmEntityContainer2.FullName()) != null;
			}
			else
			{
				IEdmEntitySet edmEntitySet5 = target as IEdmEntitySet;
				if (edmEntitySet5 != null)
				{
					IEdmEntityContainer container = edmEntitySet5.Container;
					if (container != null)
					{
						flag6 = container.FindEntitySet(edmEntitySet5.Name) != null;
					}
				}
				else
				{
					IEdmSchemaType edmSchemaType4 = target as IEdmSchemaType;
					if (edmSchemaType4 != null)
					{
						flag6 = context.Model.FindType(edmSchemaType4.FullName()) != null;
					}
					else
					{
						IEdmTerm edmTerm = target as IEdmTerm;
						if (edmTerm != null)
						{
							flag6 = context.Model.FindValueTerm(edmTerm.FullName()) != null;
						}
						else
						{
							IEdmFunction edmFunction = target as IEdmFunction;
							if (edmFunction != null)
							{
								flag6 = context.Model.FindFunctions(edmFunction.FullName()).Any<IEdmFunction>();
							}
							else
							{
								IEdmFunctionImport edmFunctionImport3 = target as IEdmFunctionImport;
								if (edmFunctionImport3 != null)
								{
									flag6 = edmFunctionImport3.Container.FindFunctionImports(edmFunctionImport3.Name).Any<IEdmFunctionImport>();
								}
								else
								{
									IEdmProperty edmProperty5 = target as IEdmProperty;
									if (edmProperty5 != null)
									{
										string text8 = EdmUtil.FullyQualifiedName(edmProperty5.DeclaringType as IEdmSchemaType);
										IEdmStructuredType edmStructuredType = context.Model.FindType(text8) as IEdmStructuredType;
										if (edmStructuredType != null)
										{
											flag6 = edmStructuredType.FindProperty(edmProperty5.Name) != null;
										}
									}
									else
									{
										IEdmFunctionParameter edmFunctionParameter7 = target as IEdmFunctionParameter;
										if (edmFunctionParameter7 != null)
										{
											IEdmFunction edmFunction2 = edmFunctionParameter7.DeclaringFunction as IEdmFunction;
											if (edmFunction2 != null)
											{
												using (IEnumerator<IEdmFunction> enumerator29 = context.Model.FindFunctions(edmFunction2.FullName()).GetEnumerator())
												{
													while (enumerator29.MoveNext())
													{
														IEdmFunction edmFunction3 = enumerator29.Current;
														if (edmFunction3.FindParameter(edmFunctionParameter7.Name) != null)
														{
															flag6 = true;
															break;
														}
													}
													goto IL_235;
												}
											}
											IEdmFunctionImport edmFunctionImport4 = edmFunctionParameter7.DeclaringFunction as IEdmFunctionImport;
											if (edmFunctionImport4 == null)
											{
												goto IL_235;
											}
											IEdmEntityContainer container2 = edmFunctionImport4.Container;
											using (IEnumerator<IEdmFunctionImport> enumerator30 = container2.FindFunctionImports(edmFunctionImport4.Name).GetEnumerator())
											{
												while (enumerator30.MoveNext())
												{
													IEdmFunctionImport edmFunctionImport5 = enumerator30.Current;
													if (edmFunctionImport5.FindParameter(edmFunctionParameter7.Name) != null)
													{
														flag6 = true;
														break;
													}
												}
												goto IL_235;
											}
										}
										flag6 = true;
									}
								}
							}
						}
					}
				}
			}
			IL_235:
			if (!flag6)
			{
				context.AddError(annotation.Location(), EdmErrorCode.BadUnresolvedTarget, Strings.EdmModel_Validator_Semantic_InaccessibleTarget(EdmUtil.FullyQualifiedName(target)));
			}
		});

		// Token: 0x04000600 RID: 1536
		public static readonly ValidationRule<IEdmValueAnnotation> ValueAnnotationAssertCorrectExpressionType = new ValidationRule<IEdmValueAnnotation>(delegate(ValidationContext context, IEdmValueAnnotation annotation)
		{
			IEnumerable<EdmError> enumerable5;
			if (!annotation.Value.TryAssertType(((IEdmValueTerm)annotation.Term).Type, out enumerable5))
			{
				foreach (EdmError edmError2 in enumerable5)
				{
					context.AddError(edmError2);
				}
			}
		});

		// Token: 0x04000601 RID: 1537
		public static readonly ValidationRule<IEdmValueAnnotation> ValueAnnotationInaccessibleTerm = new ValidationRule<IEdmValueAnnotation>(delegate(ValidationContext context, IEdmValueAnnotation annotation)
		{
			IEdmTerm term3 = annotation.Term;
			if (!(term3 is IUnresolvedElement) && context.Model.FindValueTerm(term3.FullName()) == null)
			{
				context.AddError(annotation.Location(), EdmErrorCode.BadUnresolvedTerm, Strings.EdmModel_Validator_Semantic_InaccessibleTerm(annotation.Term.FullName()));
			}
		});

		// Token: 0x04000602 RID: 1538
		public static readonly ValidationRule<IEdmTypeAnnotation> TypeAnnotationInaccessibleTerm = new ValidationRule<IEdmTypeAnnotation>(delegate(ValidationContext context, IEdmTypeAnnotation annotation)
		{
			IEdmTerm term2 = annotation.Term;
			if (!(term2 is IUnresolvedElement) && !(context.Model.FindType(term2.FullName()) is IEdmStructuredType))
			{
				context.AddError(annotation.Location(), EdmErrorCode.BadUnresolvedTerm, Strings.EdmModel_Validator_Semantic_InaccessibleTerm(annotation.Term.FullName()));
			}
		});

		// Token: 0x04000603 RID: 1539
		public static readonly ValidationRule<IEdmTypeAnnotation> TypeAnnotationAssertMatchesTermType = new ValidationRule<IEdmTypeAnnotation>(delegate(ValidationContext context, IEdmTypeAnnotation annotation)
		{
			IEdmStructuredType edmStructuredType2 = (IEdmStructuredType)annotation.Term;
			HashSetInternal<IEdmProperty> hashSetInternal12 = new HashSetInternal<IEdmProperty>();
			foreach (IEdmProperty edmProperty6 in edmStructuredType2.Properties())
			{
				if (annotation.FindPropertyBinding(edmProperty6) == null)
				{
					context.AddError(new EdmError(annotation.Location(), EdmErrorCode.TypeAnnotationMissingRequiredProperty, Strings.EdmModel_Validator_Semantic_TypeAnnotationMissingRequiredProperty(edmProperty6.Name)));
				}
				else
				{
					hashSetInternal12.Add(edmProperty6);
				}
			}
			if (!edmStructuredType2.IsOpen)
			{
				foreach (IEdmPropertyValueBinding edmPropertyValueBinding in annotation.PropertyValueBindings)
				{
					if (!hashSetInternal12.Contains(edmPropertyValueBinding.BoundProperty) && !context.IsBad(edmPropertyValueBinding))
					{
						context.AddError(new EdmError(edmPropertyValueBinding.Location(), EdmErrorCode.TypeAnnotationHasExtraProperties, Strings.EdmModel_Validator_Semantic_TypeAnnotationHasExtraProperties(edmPropertyValueBinding.BoundProperty.Name)));
					}
				}
			}
		});

		// Token: 0x04000604 RID: 1540
		public static readonly ValidationRule<IEdmPropertyValueBinding> PropertyValueBindingValueIsCorrectType = new ValidationRule<IEdmPropertyValueBinding>(delegate(ValidationContext context, IEdmPropertyValueBinding binding)
		{
			IEnumerable<EdmError> enumerable6;
			if (!binding.Value.TryAssertType(binding.BoundProperty.Type, out enumerable6) && !context.IsBad(binding) && !context.IsBad(binding.BoundProperty))
			{
				foreach (EdmError edmError3 in enumerable6)
				{
					context.AddError(edmError3);
				}
			}
		});

		// Token: 0x04000605 RID: 1541
		public static readonly ValidationRule<IEdmValueTerm> ValueTermsNotSupportedBeforeV3 = new ValidationRule<IEdmValueTerm>(delegate(ValidationContext context, IEdmValueTerm valueTerm)
		{
			context.AddError(valueTerm.Location(), EdmErrorCode.ValueTermsNotSupportedBeforeV3, Strings.EdmModel_Validator_Semantic_ValueTermsNotSupportedBeforeV3);
		});

		// Token: 0x04000606 RID: 1542
		public static readonly ValidationRule<IEdmTerm> TermMustNotHaveKindOfNone = new ValidationRule<IEdmTerm>(delegate(ValidationContext context, IEdmTerm term)
		{
			if (term.TermKind == EdmTermKind.None && !context.IsBad(term))
			{
				context.AddError(term.Location(), EdmErrorCode.TermMustNotHaveKindOfNone, Strings.EdmModel_Validator_Semantic_TermMustNotHaveKindOfNone(term.FullName()));
			}
		});

		// Token: 0x04000607 RID: 1543
		public static readonly ValidationRule<IEdmIfExpression> IfExpressionAssertCorrectTestType = new ValidationRule<IEdmIfExpression>(delegate(ValidationContext context, IEdmIfExpression expression)
		{
			IEnumerable<EdmError> enumerable7;
			if (!expression.TestExpression.TryAssertType(EdmCoreModel.Instance.GetBoolean(false), out enumerable7))
			{
				foreach (EdmError edmError4 in enumerable7)
				{
					context.AddError(edmError4);
				}
			}
		});

		// Token: 0x04000608 RID: 1544
		public static readonly ValidationRule<IEdmCollectionExpression> CollectionExpressionAllElementsCorrectType = new ValidationRule<IEdmCollectionExpression>(delegate(ValidationContext context, IEdmCollectionExpression expression)
		{
			if (expression.DeclaredType != null && !context.IsBad(expression) && !context.IsBad(expression.DeclaredType))
			{
				IEnumerable<EdmError> enumerable8;
				expression.TryAssertCollectionAsType(expression.DeclaredType, null, false, out enumerable8);
				foreach (EdmError edmError5 in enumerable8)
				{
					context.AddError(edmError5);
				}
			}
		});

		// Token: 0x04000609 RID: 1545
		public static readonly ValidationRule<IEdmRecordExpression> RecordExpressionPropertiesMatchType = new ValidationRule<IEdmRecordExpression>(delegate(ValidationContext context, IEdmRecordExpression expression)
		{
			if (expression.DeclaredType != null && !context.IsBad(expression) && !context.IsBad(expression.DeclaredType))
			{
				IEnumerable<EdmError> enumerable9;
				expression.TryAssertRecordAsType(expression.DeclaredType, null, false, out enumerable9);
				foreach (EdmError edmError6 in enumerable9)
				{
					context.AddError(edmError6);
				}
			}
		});

		// Token: 0x0400060A RID: 1546
		public static readonly ValidationRule<IEdmApplyExpression> FunctionApplicationExpressionParametersMatchAppliedFunction = new ValidationRule<IEdmApplyExpression>(delegate(ValidationContext context, IEdmApplyExpression expression)
		{
			IEdmFunctionReferenceExpression edmFunctionReferenceExpression = expression.AppliedFunction as IEdmFunctionReferenceExpression;
			if (edmFunctionReferenceExpression.ReferencedFunction != null && !context.IsBad(edmFunctionReferenceExpression.ReferencedFunction))
			{
				if (edmFunctionReferenceExpression.ReferencedFunction.Parameters.Count<IEdmFunctionParameter>() != expression.Arguments.Count<IEdmExpression>())
				{
					context.AddError(new EdmError(expression.Location(), EdmErrorCode.IncorrectNumberOfArguments, Strings.EdmModel_Validator_Semantic_IncorrectNumberOfArguments(expression.Arguments.Count<IEdmExpression>(), edmFunctionReferenceExpression.ReferencedFunction.FullName(), edmFunctionReferenceExpression.ReferencedFunction.Parameters.Count<IEdmFunctionParameter>())));
				}
				IEnumerator<IEdmExpression> enumerator38 = expression.Arguments.GetEnumerator();
				foreach (IEdmFunctionParameter edmFunctionParameter8 in edmFunctionReferenceExpression.ReferencedFunction.Parameters)
				{
					enumerator38.MoveNext();
					IEnumerable<EdmError> enumerable10;
					if (!enumerator38.Current.TryAssertType(edmFunctionParameter8.Type, out enumerable10))
					{
						foreach (EdmError edmError7 in enumerable10)
						{
							context.AddError(edmError7);
						}
					}
				}
			}
		});

		// Token: 0x0400060B RID: 1547
		public static readonly ValidationRule<IEdmVocabularyAnnotatable> VocabularyAnnotatableNoDuplicateAnnotations = new ValidationRule<IEdmVocabularyAnnotatable>(delegate(ValidationContext context, IEdmVocabularyAnnotatable annotatable)
		{
			HashSetInternal<string> hashSetInternal13 = new HashSetInternal<string>();
			foreach (IEdmVocabularyAnnotation edmVocabularyAnnotation in annotatable.VocabularyAnnotations(context.Model))
			{
				if (!hashSetInternal13.Add(edmVocabularyAnnotation.Term.FullName() + ":" + edmVocabularyAnnotation.Qualifier))
				{
					context.AddError(new EdmError(edmVocabularyAnnotation.Location(), EdmErrorCode.DuplicateAnnotation, Strings.EdmModel_Validator_Semantic_DuplicateAnnotation(EdmUtil.FullyQualifiedName(annotatable), edmVocabularyAnnotation.Term.FullName(), edmVocabularyAnnotation.Qualifier)));
				}
			}
		});

		// Token: 0x0400060C RID: 1548
		public static readonly ValidationRule<IEdmPrimitiveValue> PrimitiveValueValidForType = new ValidationRule<IEdmPrimitiveValue>(delegate(ValidationContext context, IEdmPrimitiveValue value)
		{
			if (value.Type != null && !context.IsBad(value) && !context.IsBad(value.Type))
			{
				IEnumerable<EdmError> enumerable11;
				value.TryAssertPrimitiveAsType(value.Type, out enumerable11);
				foreach (EdmError edmError8 in enumerable11)
				{
					context.AddError(edmError8);
				}
			}
		});
	}
}
