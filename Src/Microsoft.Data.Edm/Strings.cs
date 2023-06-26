using System;

namespace Microsoft.Data.Edm
{
	// Token: 0x02000243 RID: 579
	internal static class Strings
	{
		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000D54 RID: 3412 RVA: 0x0002A4A3 File Offset: 0x000286A3
		internal static string EdmPrimitive_UnexpectedKind
		{
			get
			{
				return EntityRes.GetString("EdmPrimitive_UnexpectedKind");
			}
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x0002A4B0 File Offset: 0x000286B0
		internal static string Annotations_DocumentationPun(object p0)
		{
			return EntityRes.GetString("Annotations_DocumentationPun", new object[] { p0 });
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0002A4D4 File Offset: 0x000286D4
		internal static string Annotations_TypeMismatch(object p0, object p1)
		{
			return EntityRes.GetString("Annotations_TypeMismatch", new object[] { p0, p1 });
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000D57 RID: 3415 RVA: 0x0002A4FB File Offset: 0x000286FB
		internal static string Constructable_VocabularyAnnotationMustHaveTarget
		{
			get
			{
				return EntityRes.GetString("Constructable_VocabularyAnnotationMustHaveTarget");
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000D58 RID: 3416 RVA: 0x0002A507 File Offset: 0x00028707
		internal static string Constructable_EntityTypeOrCollectionOfEntityTypeExpected
		{
			get
			{
				return EntityRes.GetString("Constructable_EntityTypeOrCollectionOfEntityTypeExpected");
			}
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x0002A514 File Offset: 0x00028714
		internal static string Constructable_TargetMustBeStock(object p0)
		{
			return EntityRes.GetString("Constructable_TargetMustBeStock", new object[] { p0 });
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x0002A538 File Offset: 0x00028738
		internal static string TypeSemantics_CouldNotConvertTypeReference(object p0, object p1)
		{
			return EntityRes.GetString("TypeSemantics_CouldNotConvertTypeReference", new object[] { p0, p1 });
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000D5B RID: 3419 RVA: 0x0002A55F File Offset: 0x0002875F
		internal static string EdmModel_CannotUseElementWithTypeNone
		{
			get
			{
				return EntityRes.GetString("EdmModel_CannotUseElementWithTypeNone");
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x0002A56B File Offset: 0x0002876B
		internal static string EdmEntityContainer_CannotUseElementWithTypeNone
		{
			get
			{
				return EntityRes.GetString("EdmEntityContainer_CannotUseElementWithTypeNone");
			}
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x0002A578 File Offset: 0x00028778
		internal static string ValueWriter_NonSerializableValue(object p0)
		{
			return EntityRes.GetString("ValueWriter_NonSerializableValue", new object[] { p0 });
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000D5E RID: 3422 RVA: 0x0002A59B File Offset: 0x0002879B
		internal static string ValueHasAlreadyBeenSet
		{
			get
			{
				return EntityRes.GetString("ValueHasAlreadyBeenSet");
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x0002A5A7 File Offset: 0x000287A7
		internal static string PathSegmentMustNotContainSlash
		{
			get
			{
				return EntityRes.GetString("PathSegmentMustNotContainSlash");
			}
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x0002A5B4 File Offset: 0x000287B4
		internal static string Edm_Evaluator_NoTermTypeAnnotationOnType(object p0, object p1)
		{
			return EntityRes.GetString("Edm_Evaluator_NoTermTypeAnnotationOnType", new object[] { p0, p1 });
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x0002A5DC File Offset: 0x000287DC
		internal static string Edm_Evaluator_NoValueAnnotationOnType(object p0, object p1)
		{
			return EntityRes.GetString("Edm_Evaluator_NoValueAnnotationOnType", new object[] { p0, p1 });
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x0002A604 File Offset: 0x00028804
		internal static string Edm_Evaluator_NoValueAnnotationOnElement(object p0)
		{
			return EntityRes.GetString("Edm_Evaluator_NoValueAnnotationOnElement", new object[] { p0 });
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x0002A628 File Offset: 0x00028828
		internal static string Edm_Evaluator_UnrecognizedExpressionKind(object p0)
		{
			return EntityRes.GetString("Edm_Evaluator_UnrecognizedExpressionKind", new object[] { p0 });
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x0002A64C File Offset: 0x0002884C
		internal static string Edm_Evaluator_UnboundFunction(object p0)
		{
			return EntityRes.GetString("Edm_Evaluator_UnboundFunction", new object[] { p0 });
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x0002A670 File Offset: 0x00028870
		internal static string Edm_Evaluator_UnboundPath(object p0)
		{
			return EntityRes.GetString("Edm_Evaluator_UnboundPath", new object[] { p0 });
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x0002A694 File Offset: 0x00028894
		internal static string Edm_Evaluator_FailedTypeAssertion(object p0)
		{
			return EntityRes.GetString("Edm_Evaluator_FailedTypeAssertion", new object[] { p0 });
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x0002A6B8 File Offset: 0x000288B8
		internal static string EdmModel_Validator_Semantic_SystemNamespaceEncountered(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_SystemNamespaceEncountered", new object[] { p0 });
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x0002A6DC File Offset: 0x000288DC
		internal static string EdmModel_Validator_Semantic_EntitySetTypeHasNoKeys(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EntitySetTypeHasNoKeys", new object[] { p0, p1 });
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x0002A704 File Offset: 0x00028904
		internal static string EdmModel_Validator_Semantic_DuplicateEndName(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DuplicateEndName", new object[] { p0 });
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x0002A728 File Offset: 0x00028928
		internal static string EdmModel_Validator_Semantic_DuplicatePropertyNameSpecifiedInEntityKey(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DuplicatePropertyNameSpecifiedInEntityKey", new object[] { p0, p1 });
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x0002A750 File Offset: 0x00028950
		internal static string EdmModel_Validator_Semantic_InvalidComplexTypeAbstract(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidComplexTypeAbstract", new object[] { p0 });
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x0002A774 File Offset: 0x00028974
		internal static string EdmModel_Validator_Semantic_InvalidComplexTypePolymorphic(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidComplexTypePolymorphic", new object[] { p0 });
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x0002A798 File Offset: 0x00028998
		internal static string EdmModel_Validator_Semantic_InvalidKeyNullablePart(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidKeyNullablePart", new object[] { p0, p1 });
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x0002A7C0 File Offset: 0x000289C0
		internal static string EdmModel_Validator_Semantic_EntityKeyMustBeScalar(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EntityKeyMustBeScalar", new object[] { p0, p1 });
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x0002A7E8 File Offset: 0x000289E8
		internal static string EdmModel_Validator_Semantic_InvalidKeyKeyDefinedInBaseClass(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidKeyKeyDefinedInBaseClass", new object[] { p0, p1 });
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x0002A810 File Offset: 0x00028A10
		internal static string EdmModel_Validator_Semantic_KeyMissingOnEntityType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_KeyMissingOnEntityType", new object[] { p0 });
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x0002A834 File Offset: 0x00028A34
		internal static string EdmModel_Validator_Semantic_BadNavigationPropertyUndefinedRole(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_BadNavigationPropertyUndefinedRole", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x0002A860 File Offset: 0x00028A60
		internal static string EdmModel_Validator_Semantic_BadNavigationPropertyRolesCannotBeTheSame(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_BadNavigationPropertyRolesCannotBeTheSame", new object[] { p0 });
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x0002A884 File Offset: 0x00028A84
		internal static string EdmModel_Validator_Semantic_BadNavigationPropertyCouldNotDetermineType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_BadNavigationPropertyCouldNotDetermineType", new object[] { p0 });
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x0002A8A7 File Offset: 0x00028AA7
		internal static string EdmModel_Validator_Semantic_InvalidOperationMultipleEndsInAssociation
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidOperationMultipleEndsInAssociation");
			}
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x0002A8B4 File Offset: 0x00028AB4
		internal static string EdmModel_Validator_Semantic_EndWithManyMultiplicityCannotHaveOperationsSpecified(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EndWithManyMultiplicityCannotHaveOperationsSpecified", new object[] { p0 });
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x0002A8D8 File Offset: 0x00028AD8
		internal static string EdmModel_Validator_Semantic_EndNameAlreadyDefinedDuplicate(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EndNameAlreadyDefinedDuplicate", new object[] { p0 });
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x0002A8FC File Offset: 0x00028AFC
		internal static string EdmModel_Validator_Semantic_SameRoleReferredInReferentialConstraint(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_SameRoleReferredInReferentialConstraint", new object[] { p0 });
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x0002A920 File Offset: 0x00028B20
		internal static string EdmModel_Validator_Semantic_NavigationPropertyPrincipalEndMultiplicityUpperBoundMustBeOne(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NavigationPropertyPrincipalEndMultiplicityUpperBoundMustBeOne", new object[] { p0 });
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x0002A944 File Offset: 0x00028B44
		internal static string EdmModel_Validator_Semantic_InvalidMultiplicityOfPrincipalEndDependentPropertiesAllNonnullable(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidMultiplicityOfPrincipalEndDependentPropertiesAllNonnullable", new object[] { p0, p1 });
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x0002A96C File Offset: 0x00028B6C
		internal static string EdmModel_Validator_Semantic_InvalidMultiplicityOfPrincipalEndDependentPropertiesAllNullable(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidMultiplicityOfPrincipalEndDependentPropertiesAllNullable", new object[] { p0, p1 });
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x0002A994 File Offset: 0x00028B94
		internal static string EdmModel_Validator_Semantic_InvalidMultiplicityOfDependentEndMustBeZeroOneOrOne(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidMultiplicityOfDependentEndMustBeZeroOneOrOne", new object[] { p0 });
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x0002A9B8 File Offset: 0x00028BB8
		internal static string EdmModel_Validator_Semantic_InvalidMultiplicityOfDependentEndMustBeMany(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidMultiplicityOfDependentEndMustBeMany", new object[] { p0 });
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x0002A9DC File Offset: 0x00028BDC
		internal static string EdmModel_Validator_Semantic_InvalidToPropertyInRelationshipConstraint(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidToPropertyInRelationshipConstraint", new object[] { p0, p1 });
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x0002AA03 File Offset: 0x00028C03
		internal static string EdmModel_Validator_Semantic_MismatchNumberOfPropertiesinRelationshipConstraint
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_MismatchNumberOfPropertiesinRelationshipConstraint");
			}
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x0002AA10 File Offset: 0x00028C10
		internal static string EdmModel_Validator_Semantic_TypeMismatchRelationshipConstraint(object p0, object p1, object p2, object p3, object p4)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_TypeMismatchRelationshipConstraint", new object[] { p0, p1, p2, p3, p4 });
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x0002AA44 File Offset: 0x00028C44
		internal static string EdmModel_Validator_Semantic_InvalidPropertyInRelationshipConstraintDependentEnd(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidPropertyInRelationshipConstraintDependentEnd", new object[] { p0, p1 });
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x0002AA6C File Offset: 0x00028C6C
		internal static string EdmModel_Validator_Semantic_InvalidPropertyInRelationshipConstraintPrimaryEnd(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidPropertyInRelationshipConstraintPrimaryEnd", new object[] { p0, p1 });
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x0002AA94 File Offset: 0x00028C94
		internal static string EdmModel_Validator_Semantic_NullableComplexTypeProperty(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NullableComplexTypeProperty", new object[] { p0 });
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x0002AAB8 File Offset: 0x00028CB8
		internal static string EdmModel_Validator_Semantic_InvalidPropertyType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidPropertyType", new object[] { p0 });
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x0002AADC File Offset: 0x00028CDC
		internal static string EdmModel_Validator_Semantic_ComposableFunctionImportCannotBeSideEffecting(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ComposableFunctionImportCannotBeSideEffecting", new object[] { p0 });
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x0002AB00 File Offset: 0x00028D00
		internal static string EdmModel_Validator_Semantic_BindableFunctionImportMustHaveParameters(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_BindableFunctionImportMustHaveParameters", new object[] { p0 });
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x0002AB24 File Offset: 0x00028D24
		internal static string EdmModel_Validator_Semantic_FunctionImportWithUnsupportedReturnTypeV1(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_FunctionImportWithUnsupportedReturnTypeV1", new object[] { p0 });
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x0002AB48 File Offset: 0x00028D48
		internal static string EdmModel_Validator_Semantic_FunctionImportWithUnsupportedReturnTypeAfterV1(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_FunctionImportWithUnsupportedReturnTypeAfterV1", new object[] { p0 });
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x0002AB6C File Offset: 0x00028D6C
		internal static string EdmModel_Validator_Semantic_FunctionImportReturnEntitiesButDoesNotSpecifyEntitySet(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_FunctionImportReturnEntitiesButDoesNotSpecifyEntitySet", new object[] { p0 });
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x0002AB90 File Offset: 0x00028D90
		internal static string EdmModel_Validator_Semantic_FunctionImportEntityTypeDoesNotMatchEntitySet(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_FunctionImportEntityTypeDoesNotMatchEntitySet", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x0002ABBC File Offset: 0x00028DBC
		internal static string EdmModel_Validator_Semantic_FunctionImportEntityTypeDoesNotMatchEntitySet2(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_FunctionImportEntityTypeDoesNotMatchEntitySet2", new object[] { p0, p1 });
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x0002ABE4 File Offset: 0x00028DE4
		internal static string EdmModel_Validator_Semantic_FunctionImportEntitySetExpressionKindIsInvalid(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_FunctionImportEntitySetExpressionKindIsInvalid", new object[] { p0, p1 });
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x0002AC0C File Offset: 0x00028E0C
		internal static string EdmModel_Validator_Semantic_FunctionImportEntitySetExpressionIsInvalid(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_FunctionImportEntitySetExpressionIsInvalid", new object[] { p0 });
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x0002AC30 File Offset: 0x00028E30
		internal static string EdmModel_Validator_Semantic_FunctionImportSpecifiesEntitySetButNotEntityType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_FunctionImportSpecifiesEntitySetButNotEntityType", new object[] { p0 });
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x0002AC54 File Offset: 0x00028E54
		internal static string EdmModel_Validator_Semantic_ComposableFunctionImportMustHaveReturnType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ComposableFunctionImportMustHaveReturnType", new object[] { p0 });
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x0002AC78 File Offset: 0x00028E78
		internal static string EdmModel_Validator_Semantic_ParameterNameAlreadyDefinedDuplicate(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ParameterNameAlreadyDefinedDuplicate", new object[] { p0 });
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x0002AC9C File Offset: 0x00028E9C
		internal static string EdmModel_Validator_Semantic_DuplicateEntityContainerMemberName(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DuplicateEntityContainerMemberName", new object[] { p0 });
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x0002ACC0 File Offset: 0x00028EC0
		internal static string EdmModel_Validator_Semantic_SchemaElementNameAlreadyDefined(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_SchemaElementNameAlreadyDefined", new object[] { p0 });
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x0002ACE4 File Offset: 0x00028EE4
		internal static string EdmModel_Validator_Semantic_InvalidMemberNameMatchesTypeName(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidMemberNameMatchesTypeName", new object[] { p0 });
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x0002AD08 File Offset: 0x00028F08
		internal static string EdmModel_Validator_Semantic_PropertyNameAlreadyDefined(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_PropertyNameAlreadyDefined", new object[] { p0 });
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x0002AD2B File Offset: 0x00028F2B
		internal static string EdmModel_Validator_Semantic_BaseTypeMustHaveSameTypeKind
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_BaseTypeMustHaveSameTypeKind");
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06000D95 RID: 3477 RVA: 0x0002AD37 File Offset: 0x00028F37
		internal static string EdmModel_Validator_Semantic_RowTypeMustNotHaveBaseType
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_RowTypeMustNotHaveBaseType");
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x0002AD43 File Offset: 0x00028F43
		internal static string EdmModel_Validator_Semantic_FunctionsNotSupportedBeforeV2
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_FunctionsNotSupportedBeforeV2");
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06000D97 RID: 3479 RVA: 0x0002AD4F File Offset: 0x00028F4F
		internal static string EdmModel_Validator_Semantic_FunctionImportSideEffectingNotSupportedBeforeV3
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_FunctionImportSideEffectingNotSupportedBeforeV3");
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x0002AD5B File Offset: 0x00028F5B
		internal static string EdmModel_Validator_Semantic_FunctionImportComposableNotSupportedBeforeV3
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_FunctionImportComposableNotSupportedBeforeV3");
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000D99 RID: 3481 RVA: 0x0002AD67 File Offset: 0x00028F67
		internal static string EdmModel_Validator_Semantic_FunctionImportBindableNotSupportedBeforeV3
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_FunctionImportBindableNotSupportedBeforeV3");
			}
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x0002AD74 File Offset: 0x00028F74
		internal static string EdmModel_Validator_Semantic_KeyPropertyMustBelongToEntity(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_KeyPropertyMustBelongToEntity", new object[] { p0, p1 });
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x0002AD9C File Offset: 0x00028F9C
		internal static string EdmModel_Validator_Semantic_DependentPropertiesMustBelongToDependentEntity(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DependentPropertiesMustBelongToDependentEntity", new object[] { p0, p1 });
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x0002ADC4 File Offset: 0x00028FC4
		internal static string EdmModel_Validator_Semantic_DeclaringTypeMustBeCorrect(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DeclaringTypeMustBeCorrect", new object[] { p0 });
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x0002ADE8 File Offset: 0x00028FE8
		internal static string EdmModel_Validator_Semantic_InaccessibleType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InaccessibleType", new object[] { p0 });
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x0002AE0C File Offset: 0x0002900C
		internal static string EdmModel_Validator_Semantic_AmbiguousType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_AmbiguousType", new object[] { p0 });
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x0002AE30 File Offset: 0x00029030
		internal static string EdmModel_Validator_Semantic_InvalidNavigationPropertyType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidNavigationPropertyType", new object[] { p0 });
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x0002AE54 File Offset: 0x00029054
		internal static string EdmModel_Validator_Semantic_NavigationPropertyWithRecursiveContainmentTargetMustBeOptional(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NavigationPropertyWithRecursiveContainmentTargetMustBeOptional", new object[] { p0 });
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x0002AE78 File Offset: 0x00029078
		internal static string EdmModel_Validator_Semantic_NavigationPropertyWithRecursiveContainmentSourceMustBeFromZeroOrOne(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NavigationPropertyWithRecursiveContainmentSourceMustBeFromZeroOrOne", new object[] { p0 });
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x0002AE9C File Offset: 0x0002909C
		internal static string EdmModel_Validator_Semantic_NavigationPropertyWithNonRecursiveContainmentSourceMustBeFromOne(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NavigationPropertyWithNonRecursiveContainmentSourceMustBeFromOne", new object[] { p0 });
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x0002AEBF File Offset: 0x000290BF
		internal static string EdmModel_Validator_Semantic_NavigationPropertyContainsTargetNotSupportedBeforeV3
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_NavigationPropertyContainsTargetNotSupportedBeforeV3");
			}
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x0002AECC File Offset: 0x000290CC
		internal static string EdmModel_Validator_Semantic_OnlyInputParametersAllowedInFunctions(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_OnlyInputParametersAllowedInFunctions", new object[] { p0, p1 });
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x0002AEF4 File Offset: 0x000290F4
		internal static string EdmModel_Validator_Semantic_InvalidFunctionImportParameterMode(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidFunctionImportParameterMode", new object[] { p0, p1 });
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x0002AF1C File Offset: 0x0002911C
		internal static string EdmModel_Validator_Semantic_FunctionImportParameterIncorrectType(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_FunctionImportParameterIncorrectType", new object[] { p0, p1 });
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x0002AF43 File Offset: 0x00029143
		internal static string EdmModel_Validator_Semantic_RowTypeMustHaveProperties
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_RowTypeMustHaveProperties");
			}
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x0002AF50 File Offset: 0x00029150
		internal static string EdmModel_Validator_Semantic_ComplexTypeMustHaveProperties(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ComplexTypeMustHaveProperties", new object[] { p0 });
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0002AF74 File Offset: 0x00029174
		internal static string EdmModel_Validator_Semantic_DuplicateDependentProperty(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DuplicateDependentProperty", new object[] { p0, p1 });
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000DAA RID: 3498 RVA: 0x0002AF9B File Offset: 0x0002919B
		internal static string EdmModel_Validator_Semantic_ScaleOutOfRange
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_ScaleOutOfRange");
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x0002AFA7 File Offset: 0x000291A7
		internal static string EdmModel_Validator_Semantic_PrecisionOutOfRange
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_PrecisionOutOfRange");
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x0002AFB3 File Offset: 0x000291B3
		internal static string EdmModel_Validator_Semantic_StringMaxLengthOutOfRange
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_StringMaxLengthOutOfRange");
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000DAD RID: 3501 RVA: 0x0002AFBF File Offset: 0x000291BF
		internal static string EdmModel_Validator_Semantic_MaxLengthOutOfRange
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_MaxLengthOutOfRange");
			}
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0002AFCC File Offset: 0x000291CC
		internal static string EdmModel_Validator_Semantic_InvalidPropertyTypeConcurrencyMode(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidPropertyTypeConcurrencyMode", new object[] { p0 });
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x0002AFF0 File Offset: 0x000291F0
		internal static string EdmModel_Validator_Semantic_EntityKeyMustNotBeBinaryBeforeV2(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EntityKeyMustNotBeBinaryBeforeV2", new object[] { p0, p1 });
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x0002B017 File Offset: 0x00029217
		internal static string EdmModel_Validator_Semantic_EnumsNotSupportedBeforeV3
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_EnumsNotSupportedBeforeV3");
			}
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0002B024 File Offset: 0x00029224
		internal static string EdmModel_Validator_Semantic_EnumMemberTypeMustMatchEnumUnderlyingType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EnumMemberTypeMustMatchEnumUnderlyingType", new object[] { p0 });
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0002B048 File Offset: 0x00029248
		internal static string EdmModel_Validator_Semantic_EnumMemberNameAlreadyDefined(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EnumMemberNameAlreadyDefined", new object[] { p0 });
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06000DB3 RID: 3507 RVA: 0x0002B06B File Offset: 0x0002926B
		internal static string EdmModel_Validator_Semantic_ValueTermsNotSupportedBeforeV3
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_ValueTermsNotSupportedBeforeV3");
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06000DB4 RID: 3508 RVA: 0x0002B077 File Offset: 0x00029277
		internal static string EdmModel_Validator_Semantic_VocabularyAnnotationsNotSupportedBeforeV3
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_VocabularyAnnotationsNotSupportedBeforeV3");
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x0002B083 File Offset: 0x00029283
		internal static string EdmModel_Validator_Semantic_OpenTypesSupportedOnlyInV12AndAfterV3
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_OpenTypesSupportedOnlyInV12AndAfterV3");
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x0002B08F File Offset: 0x0002928F
		internal static string EdmModel_Validator_Semantic_OpenTypesSupportedForEntityTypesOnly
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_OpenTypesSupportedForEntityTypesOnly");
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x0002B09B File Offset: 0x0002929B
		internal static string EdmModel_Validator_Semantic_IsUnboundedCannotBeTrueWhileMaxLengthIsNotNull
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_IsUnboundedCannotBeTrueWhileMaxLengthIsNotNull");
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06000DB8 RID: 3512 RVA: 0x0002B0A7 File Offset: 0x000292A7
		internal static string EdmModel_Validator_Semantic_InvalidElementAnnotationMismatchedTerm
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidElementAnnotationMismatchedTerm");
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000DB9 RID: 3513 RVA: 0x0002B0B3 File Offset: 0x000292B3
		internal static string EdmModel_Validator_Semantic_InvalidElementAnnotationValueInvalidXml
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidElementAnnotationValueInvalidXml");
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000DBA RID: 3514 RVA: 0x0002B0BF File Offset: 0x000292BF
		internal static string EdmModel_Validator_Semantic_InvalidElementAnnotationNotIEdmStringValue
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidElementAnnotationNotIEdmStringValue");
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06000DBB RID: 3515 RVA: 0x0002B0CB File Offset: 0x000292CB
		internal static string EdmModel_Validator_Semantic_InvalidElementAnnotationNullNamespaceOrName
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_InvalidElementAnnotationNullNamespaceOrName");
			}
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x0002B0D8 File Offset: 0x000292D8
		internal static string EdmModel_Validator_Semantic_CannotAssertNullableTypeAsNonNullableType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_CannotAssertNullableTypeAsNonNullableType", new object[] { p0 });
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0002B0FC File Offset: 0x000292FC
		internal static string EdmModel_Validator_Semantic_ExpressionPrimitiveKindCannotPromoteToAssertedType(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ExpressionPrimitiveKindCannotPromoteToAssertedType", new object[] { p0, p1 });
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06000DBE RID: 3518 RVA: 0x0002B123 File Offset: 0x00029323
		internal static string EdmModel_Validator_Semantic_NullCannotBeAssertedToBeANonNullableType
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_NullCannotBeAssertedToBeANonNullableType");
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06000DBF RID: 3519 RVA: 0x0002B12F File Offset: 0x0002932F
		internal static string EdmModel_Validator_Semantic_ExpressionNotValidForTheAssertedType
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_ExpressionNotValidForTheAssertedType");
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06000DC0 RID: 3520 RVA: 0x0002B13B File Offset: 0x0002933B
		internal static string EdmModel_Validator_Semantic_CollectionExpressionNotValidForNonCollectionType
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_CollectionExpressionNotValidForNonCollectionType");
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x0002B147 File Offset: 0x00029347
		internal static string EdmModel_Validator_Semantic_PrimitiveConstantExpressionNotValidForNonPrimitiveType
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_PrimitiveConstantExpressionNotValidForNonPrimitiveType");
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x0002B153 File Offset: 0x00029353
		internal static string EdmModel_Validator_Semantic_RecordExpressionNotValidForNonStructuredType
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_RecordExpressionNotValidForNonStructuredType");
			}
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x0002B160 File Offset: 0x00029360
		internal static string EdmModel_Validator_Semantic_RecordExpressionMissingProperty(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_RecordExpressionMissingProperty", new object[] { p0 });
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x0002B184 File Offset: 0x00029384
		internal static string EdmModel_Validator_Semantic_RecordExpressionHasExtraProperties(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_RecordExpressionHasExtraProperties", new object[] { p0 });
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x0002B1A8 File Offset: 0x000293A8
		internal static string EdmModel_Validator_Semantic_DuplicateAnnotation(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DuplicateAnnotation", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x0002B1D4 File Offset: 0x000293D4
		internal static string EdmModel_Validator_Semantic_IncorrectNumberOfArguments(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_IncorrectNumberOfArguments", new object[] { p0, p1, p2 });
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x0002B1FF File Offset: 0x000293FF
		internal static string EdmModel_Validator_Semantic_StreamTypeReferencesNotSupportedBeforeV3
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_StreamTypeReferencesNotSupportedBeforeV3");
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x0002B20B File Offset: 0x0002940B
		internal static string EdmModel_Validator_Semantic_SpatialTypeReferencesNotSupportedBeforeV3
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_SpatialTypeReferencesNotSupportedBeforeV3");
			}
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x0002B218 File Offset: 0x00029418
		internal static string EdmModel_Validator_Semantic_DuplicateEntityContainerName(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DuplicateEntityContainerName", new object[] { p0 });
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x0002B23B File Offset: 0x0002943B
		internal static string EdmModel_Validator_Semantic_ExpressionPrimitiveKindNotValidForAssertedType
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_ExpressionPrimitiveKindNotValidForAssertedType");
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06000DCB RID: 3531 RVA: 0x0002B247 File Offset: 0x00029447
		internal static string EdmModel_Validator_Semantic_IntegerConstantValueOutOfRange
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_IntegerConstantValueOutOfRange");
			}
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x0002B254 File Offset: 0x00029454
		internal static string EdmModel_Validator_Semantic_StringConstantLengthOutOfRange(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_StringConstantLengthOutOfRange", new object[] { p0, p1 });
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x0002B27C File Offset: 0x0002947C
		internal static string EdmModel_Validator_Semantic_BinaryConstantLengthOutOfRange(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_BinaryConstantLengthOutOfRange", new object[] { p0, p1 });
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x0002B2A3 File Offset: 0x000294A3
		internal static string EdmModel_Validator_Semantic_TypeMustNotHaveKindOfNone
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Semantic_TypeMustNotHaveKindOfNone");
			}
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x0002B2B0 File Offset: 0x000294B0
		internal static string EdmModel_Validator_Semantic_TermMustNotHaveKindOfNone(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_TermMustNotHaveKindOfNone", new object[] { p0 });
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x0002B2D4 File Offset: 0x000294D4
		internal static string EdmModel_Validator_Semantic_SchemaElementMustNotHaveKindOfNone(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_SchemaElementMustNotHaveKindOfNone", new object[] { p0 });
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x0002B2F8 File Offset: 0x000294F8
		internal static string EdmModel_Validator_Semantic_PropertyMustNotHaveKindOfNone(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_PropertyMustNotHaveKindOfNone", new object[] { p0 });
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x0002B31C File Offset: 0x0002951C
		internal static string EdmModel_Validator_Semantic_PrimitiveTypeMustNotHaveKindOfNone(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_PrimitiveTypeMustNotHaveKindOfNone", new object[] { p0 });
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x0002B340 File Offset: 0x00029540
		internal static string EdmModel_Validator_Semantic_EntityContainerElementMustNotHaveKindOfNone(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EntityContainerElementMustNotHaveKindOfNone", new object[] { p0 });
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x0002B364 File Offset: 0x00029564
		internal static string EdmModel_Validator_Semantic_DuplicateNavigationPropertyMapping(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_DuplicateNavigationPropertyMapping", new object[] { p0, p1 });
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x0002B38C File Offset: 0x0002958C
		internal static string EdmModel_Validator_Semantic_EntitySetNavigationMappingMustBeBidirectional(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EntitySetNavigationMappingMustBeBidirectional", new object[] { p0, p1 });
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x0002B3B4 File Offset: 0x000295B4
		internal static string EdmModel_Validator_Semantic_EntitySetCanOnlyBeContainedByASingleNavigationProperty(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EntitySetCanOnlyBeContainedByASingleNavigationProperty", new object[] { p0 });
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x0002B3D8 File Offset: 0x000295D8
		internal static string EdmModel_Validator_Semantic_TypeAnnotationMissingRequiredProperty(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_TypeAnnotationMissingRequiredProperty", new object[] { p0 });
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x0002B3FC File Offset: 0x000295FC
		internal static string EdmModel_Validator_Semantic_TypeAnnotationHasExtraProperties(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_TypeAnnotationHasExtraProperties", new object[] { p0 });
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x0002B420 File Offset: 0x00029620
		internal static string EdmModel_Validator_Semantic_EnumMustHaveIntegralUnderlyingType(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EnumMustHaveIntegralUnderlyingType", new object[] { p0 });
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x0002B444 File Offset: 0x00029644
		internal static string EdmModel_Validator_Semantic_InaccessibleTerm(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InaccessibleTerm", new object[] { p0 });
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x0002B468 File Offset: 0x00029668
		internal static string EdmModel_Validator_Semantic_InaccessibleTarget(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_InaccessibleTarget", new object[] { p0 });
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x0002B48C File Offset: 0x0002968C
		internal static string EdmModel_Validator_Semantic_ElementDirectValueAnnotationFullNameMustBeUnique(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_ElementDirectValueAnnotationFullNameMustBeUnique", new object[] { p0, p1 });
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x0002B4B4 File Offset: 0x000296B4
		internal static string EdmModel_Validator_Semantic_NoEntitySetsFoundForType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NoEntitySetsFoundForType", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x0002B4E0 File Offset: 0x000296E0
		internal static string EdmModel_Validator_Semantic_CannotInferEntitySetWithMultipleSetsPerType(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_CannotInferEntitySetWithMultipleSetsPerType", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x0002B50C File Offset: 0x0002970C
		internal static string EdmModel_Validator_Semantic_EntitySetRecursiveNavigationPropertyMappingsMustPointBackToSourceEntitySet(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EntitySetRecursiveNavigationPropertyMappingsMustPointBackToSourceEntitySet", new object[] { p0, p1 });
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x0002B534 File Offset: 0x00029734
		internal static string EdmModel_Validator_Semantic_NavigationPropertyEntityMustNotIndirectlyContainItself(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_NavigationPropertyEntityMustNotIndirectlyContainItself", new object[] { p0 });
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x0002B558 File Offset: 0x00029758
		internal static string EdmModel_Validator_Semantic_PathIsNotValidForTheGivenContext(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_PathIsNotValidForTheGivenContext", new object[] { p0 });
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0002B57C File Offset: 0x0002977C
		internal static string EdmModel_Validator_Semantic_EntitySetNavigationPropertyMappingMustPointToValidTargetForProperty(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Semantic_EntitySetNavigationPropertyMappingMustPointToValidTargetForProperty", new object[] { p0, p1 });
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x0002B5A3 File Offset: 0x000297A3
		internal static string EdmModel_Validator_Syntactic_MissingName
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Syntactic_MissingName");
			}
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x0002B5B0 File Offset: 0x000297B0
		internal static string EdmModel_Validator_Syntactic_EdmModel_NameIsTooLong(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmModel_NameIsTooLong", new object[] { p0 });
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x0002B5D4 File Offset: 0x000297D4
		internal static string EdmModel_Validator_Syntactic_EdmModel_NameIsNotAllowed(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmModel_NameIsNotAllowed", new object[] { p0 });
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x0002B5F7 File Offset: 0x000297F7
		internal static string EdmModel_Validator_Syntactic_MissingNamespaceName
		{
			get
			{
				return EntityRes.GetString("EdmModel_Validator_Syntactic_MissingNamespaceName");
			}
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x0002B604 File Offset: 0x00029804
		internal static string EdmModel_Validator_Syntactic_EdmModel_NamespaceNameIsTooLong(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmModel_NamespaceNameIsTooLong", new object[] { p0 });
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x0002B628 File Offset: 0x00029828
		internal static string EdmModel_Validator_Syntactic_EdmModel_NamespaceNameIsNotAllowed(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_EdmModel_NamespaceNameIsNotAllowed", new object[] { p0 });
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x0002B64C File Offset: 0x0002984C
		internal static string EdmModel_Validator_Syntactic_PropertyMustNotBeNull(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_PropertyMustNotBeNull", new object[] { p0, p1 });
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x0002B674 File Offset: 0x00029874
		internal static string EdmModel_Validator_Syntactic_EnumPropertyValueOutOfRange(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_EnumPropertyValueOutOfRange", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x0002B6A4 File Offset: 0x000298A4
		internal static string EdmModel_Validator_Syntactic_InterfaceKindValueMismatch(object p0, object p1, object p2, object p3)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_InterfaceKindValueMismatch", new object[] { p0, p1, p2, p3 });
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x0002B6D4 File Offset: 0x000298D4
		internal static string EdmModel_Validator_Syntactic_TypeRefInterfaceTypeKindValueMismatch(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_TypeRefInterfaceTypeKindValueMismatch", new object[] { p0, p1 });
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x0002B6FC File Offset: 0x000298FC
		internal static string EdmModel_Validator_Syntactic_InterfaceKindValueUnexpected(object p0, object p1, object p2)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_InterfaceKindValueUnexpected", new object[] { p0, p1, p2 });
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x0002B728 File Offset: 0x00029928
		internal static string EdmModel_Validator_Syntactic_EnumerableMustNotHaveNullElements(object p0, object p1)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_EnumerableMustNotHaveNullElements", new object[] { p0, p1 });
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x0002B750 File Offset: 0x00029950
		internal static string EdmModel_Validator_Syntactic_NavigationPartnerInvalid(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_NavigationPartnerInvalid", new object[] { p0 });
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x0002B774 File Offset: 0x00029974
		internal static string EdmModel_Validator_Syntactic_InterfaceCriticalCycleInTypeHierarchy(object p0)
		{
			return EntityRes.GetString("EdmModel_Validator_Syntactic_InterfaceCriticalCycleInTypeHierarchy", new object[] { p0 });
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x0002B797 File Offset: 0x00029997
		internal static string Serializer_SingleFileExpected
		{
			get
			{
				return EntityRes.GetString("Serializer_SingleFileExpected");
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x0002B7A3 File Offset: 0x000299A3
		internal static string Serializer_UnknownEdmVersion
		{
			get
			{
				return EntityRes.GetString("Serializer_UnknownEdmVersion");
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x0002B7AF File Offset: 0x000299AF
		internal static string Serializer_UnknownEdmxVersion
		{
			get
			{
				return EntityRes.GetString("Serializer_UnknownEdmxVersion");
			}
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x0002B7BC File Offset: 0x000299BC
		internal static string Serializer_NonInlineFunctionImportReturnType(object p0)
		{
			return EntityRes.GetString("Serializer_NonInlineFunctionImportReturnType", new object[] { p0 });
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x0002B7E0 File Offset: 0x000299E0
		internal static string Serializer_ReferencedTypeMustHaveValidName(object p0)
		{
			return EntityRes.GetString("Serializer_ReferencedTypeMustHaveValidName", new object[] { p0 });
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x0002B804 File Offset: 0x00029A04
		internal static string Serializer_OutOfLineAnnotationTargetMustHaveValidName(object p0)
		{
			return EntityRes.GetString("Serializer_OutOfLineAnnotationTargetMustHaveValidName", new object[] { p0 });
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x0002B827 File Offset: 0x00029A27
		internal static string Serializer_NoSchemasProduced
		{
			get
			{
				return EntityRes.GetString("Serializer_NoSchemasProduced");
			}
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x0002B834 File Offset: 0x00029A34
		internal static string XmlParser_EmptyFile(object p0)
		{
			return EntityRes.GetString("XmlParser_EmptyFile", new object[] { p0 });
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x0002B857 File Offset: 0x00029A57
		internal static string XmlParser_EmptySchemaTextReader
		{
			get
			{
				return EntityRes.GetString("XmlParser_EmptySchemaTextReader");
			}
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x0002B864 File Offset: 0x00029A64
		internal static string XmlParser_MissingAttribute(object p0, object p1)
		{
			return EntityRes.GetString("XmlParser_MissingAttribute", new object[] { p0, p1 });
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x0002B88C File Offset: 0x00029A8C
		internal static string XmlParser_TextNotAllowed(object p0)
		{
			return EntityRes.GetString("XmlParser_TextNotAllowed", new object[] { p0 });
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x0002B8B0 File Offset: 0x00029AB0
		internal static string XmlParser_UnexpectedAttribute(object p0)
		{
			return EntityRes.GetString("XmlParser_UnexpectedAttribute", new object[] { p0 });
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x0002B8D4 File Offset: 0x00029AD4
		internal static string XmlParser_UnexpectedElement(object p0)
		{
			return EntityRes.GetString("XmlParser_UnexpectedElement", new object[] { p0 });
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x0002B8F8 File Offset: 0x00029AF8
		internal static string XmlParser_UnusedElement(object p0)
		{
			return EntityRes.GetString("XmlParser_UnusedElement", new object[] { p0 });
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x0002B91C File Offset: 0x00029B1C
		internal static string XmlParser_UnexpectedNodeType(object p0)
		{
			return EntityRes.GetString("XmlParser_UnexpectedNodeType", new object[] { p0 });
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x0002B940 File Offset: 0x00029B40
		internal static string XmlParser_UnexpectedRootElement(object p0, object p1)
		{
			return EntityRes.GetString("XmlParser_UnexpectedRootElement", new object[] { p0, p1 });
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x0002B968 File Offset: 0x00029B68
		internal static string XmlParser_UnexpectedRootElementWrongNamespace(object p0, object p1)
		{
			return EntityRes.GetString("XmlParser_UnexpectedRootElementWrongNamespace", new object[] { p0, p1 });
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x0002B990 File Offset: 0x00029B90
		internal static string XmlParser_UnexpectedRootElementNoNamespace(object p0)
		{
			return EntityRes.GetString("XmlParser_UnexpectedRootElementNoNamespace", new object[] { p0 });
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x0002B9B4 File Offset: 0x00029BB4
		internal static string CsdlParser_InvalidAlias(object p0)
		{
			return EntityRes.GetString("CsdlParser_InvalidAlias", new object[] { p0 });
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06000E04 RID: 3588 RVA: 0x0002B9D7 File Offset: 0x00029BD7
		internal static string CsdlParser_AssociationHasAtMostOneConstraint
		{
			get
			{
				return EntityRes.GetString("CsdlParser_AssociationHasAtMostOneConstraint");
			}
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x0002B9E4 File Offset: 0x00029BE4
		internal static string CsdlParser_InvalidDeleteAction(object p0)
		{
			return EntityRes.GetString("CsdlParser_InvalidDeleteAction", new object[] { p0 });
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06000E06 RID: 3590 RVA: 0x0002BA07 File Offset: 0x00029C07
		internal static string CsdlParser_MissingTypeAttributeOrElement
		{
			get
			{
				return EntityRes.GetString("CsdlParser_MissingTypeAttributeOrElement");
			}
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x0002BA14 File Offset: 0x00029C14
		internal static string CsdlParser_InvalidAssociationIncorrectNumberOfEnds(object p0)
		{
			return EntityRes.GetString("CsdlParser_InvalidAssociationIncorrectNumberOfEnds", new object[] { p0 });
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0002BA38 File Offset: 0x00029C38
		internal static string CsdlParser_InvalidAssociationSetIncorrectNumberOfEnds(object p0)
		{
			return EntityRes.GetString("CsdlParser_InvalidAssociationSetIncorrectNumberOfEnds", new object[] { p0 });
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x0002BA5C File Offset: 0x00029C5C
		internal static string CsdlParser_InvalidConcurrencyMode(object p0)
		{
			return EntityRes.GetString("CsdlParser_InvalidConcurrencyMode", new object[] { p0 });
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x0002BA80 File Offset: 0x00029C80
		internal static string CsdlParser_InvalidParameterMode(object p0)
		{
			return EntityRes.GetString("CsdlParser_InvalidParameterMode", new object[] { p0 });
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x0002BAA4 File Offset: 0x00029CA4
		internal static string CsdlParser_InvalidEndRoleInRelationshipConstraint(object p0, object p1)
		{
			return EntityRes.GetString("CsdlParser_InvalidEndRoleInRelationshipConstraint", new object[] { p0, p1 });
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x0002BACC File Offset: 0x00029CCC
		internal static string CsdlParser_InvalidMultiplicity(object p0)
		{
			return EntityRes.GetString("CsdlParser_InvalidMultiplicity", new object[] { p0 });
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06000E0D RID: 3597 RVA: 0x0002BAEF File Offset: 0x00029CEF
		internal static string CsdlParser_ReferentialConstraintRequiresOneDependent
		{
			get
			{
				return EntityRes.GetString("CsdlParser_ReferentialConstraintRequiresOneDependent");
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06000E0E RID: 3598 RVA: 0x0002BAFB File Offset: 0x00029CFB
		internal static string CsdlParser_ReferentialConstraintRequiresOnePrincipal
		{
			get
			{
				return EntityRes.GetString("CsdlParser_ReferentialConstraintRequiresOnePrincipal");
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06000E0F RID: 3599 RVA: 0x0002BB07 File Offset: 0x00029D07
		internal static string CsdlParser_InvalidIfExpressionIncorrectNumberOfOperands
		{
			get
			{
				return EntityRes.GetString("CsdlParser_InvalidIfExpressionIncorrectNumberOfOperands");
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x0002BB13 File Offset: 0x00029D13
		internal static string CsdlParser_InvalidIsTypeExpressionIncorrectNumberOfOperands
		{
			get
			{
				return EntityRes.GetString("CsdlParser_InvalidIsTypeExpressionIncorrectNumberOfOperands");
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06000E11 RID: 3601 RVA: 0x0002BB1F File Offset: 0x00029D1F
		internal static string CsdlParser_InvalidAssertTypeExpressionIncorrectNumberOfOperands
		{
			get
			{
				return EntityRes.GetString("CsdlParser_InvalidAssertTypeExpressionIncorrectNumberOfOperands");
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x0002BB2B File Offset: 0x00029D2B
		internal static string CsdlParser_InvalidLabeledElementExpressionIncorrectNumberOfOperands
		{
			get
			{
				return EntityRes.GetString("CsdlParser_InvalidLabeledElementExpressionIncorrectNumberOfOperands");
			}
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x0002BB38 File Offset: 0x00029D38
		internal static string CsdlParser_InvalidTypeName(object p0)
		{
			return EntityRes.GetString("CsdlParser_InvalidTypeName", new object[] { p0 });
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x0002BB5C File Offset: 0x00029D5C
		internal static string CsdlParser_InvalidQualifiedName(object p0)
		{
			return EntityRes.GetString("CsdlParser_InvalidQualifiedName", new object[] { p0 });
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06000E15 RID: 3605 RVA: 0x0002BB7F File Offset: 0x00029D7F
		internal static string CsdlParser_NoReadersProvided
		{
			get
			{
				return EntityRes.GetString("CsdlParser_NoReadersProvided");
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x0002BB8B File Offset: 0x00029D8B
		internal static string CsdlParser_NullXmlReader
		{
			get
			{
				return EntityRes.GetString("CsdlParser_NullXmlReader");
			}
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x0002BB98 File Offset: 0x00029D98
		internal static string CsdlParser_InvalidEntitySetPath(object p0)
		{
			return EntityRes.GetString("CsdlParser_InvalidEntitySetPath", new object[] { p0 });
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x0002BBBC File Offset: 0x00029DBC
		internal static string CsdlParser_InvalidEnumMemberPath(object p0)
		{
			return EntityRes.GetString("CsdlParser_InvalidEnumMemberPath", new object[] { p0 });
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06000E19 RID: 3609 RVA: 0x0002BBDF File Offset: 0x00029DDF
		internal static string CsdlSemantics_ReferentialConstraintMismatch
		{
			get
			{
				return EntityRes.GetString("CsdlSemantics_ReferentialConstraintMismatch");
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06000E1A RID: 3610 RVA: 0x0002BBEB File Offset: 0x00029DEB
		internal static string CsdlSemantics_EnumMemberValueOutOfRange
		{
			get
			{
				return EntityRes.GetString("CsdlSemantics_EnumMemberValueOutOfRange");
			}
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x0002BBF8 File Offset: 0x00029DF8
		internal static string CsdlSemantics_ImpossibleAnnotationsTarget(object p0)
		{
			return EntityRes.GetString("CsdlSemantics_ImpossibleAnnotationsTarget", new object[] { p0 });
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x0002BC1C File Offset: 0x00029E1C
		internal static string CsdlSemantics_DuplicateAlias(object p0, object p1)
		{
			return EntityRes.GetString("CsdlSemantics_DuplicateAlias", new object[] { p0, p1 });
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06000E1D RID: 3613 RVA: 0x0002BC43 File Offset: 0x00029E43
		internal static string EdmxParser_EdmxVersionMismatch
		{
			get
			{
				return EntityRes.GetString("EdmxParser_EdmxVersionMismatch");
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06000E1E RID: 3614 RVA: 0x0002BC4F File Offset: 0x00029E4F
		internal static string EdmxParser_EdmxDataServiceVersionInvalid
		{
			get
			{
				return EntityRes.GetString("EdmxParser_EdmxDataServiceVersionInvalid");
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x0002BC5B File Offset: 0x00029E5B
		internal static string EdmxParser_EdmxMaxDataServiceVersionInvalid
		{
			get
			{
				return EntityRes.GetString("EdmxParser_EdmxMaxDataServiceVersionInvalid");
			}
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0002BC68 File Offset: 0x00029E68
		internal static string EdmxParser_BodyElement(object p0)
		{
			return EntityRes.GetString("EdmxParser_BodyElement", new object[] { p0 });
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x0002BC8C File Offset: 0x00029E8C
		internal static string EdmParseException_ErrorsEncounteredInEdmx(object p0)
		{
			return EntityRes.GetString("EdmParseException_ErrorsEncounteredInEdmx", new object[] { p0 });
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x0002BCB0 File Offset: 0x00029EB0
		internal static string ValueParser_InvalidBoolean(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidBoolean", new object[] { p0 });
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x0002BCD4 File Offset: 0x00029ED4
		internal static string ValueParser_InvalidInteger(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidInteger", new object[] { p0 });
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x0002BCF8 File Offset: 0x00029EF8
		internal static string ValueParser_InvalidLong(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidLong", new object[] { p0 });
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x0002BD1C File Offset: 0x00029F1C
		internal static string ValueParser_InvalidFloatingPoint(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidFloatingPoint", new object[] { p0 });
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x0002BD40 File Offset: 0x00029F40
		internal static string ValueParser_InvalidMaxLength(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidMaxLength", new object[] { p0 });
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x0002BD64 File Offset: 0x00029F64
		internal static string ValueParser_InvalidSrid(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidSrid", new object[] { p0 });
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x0002BD88 File Offset: 0x00029F88
		internal static string ValueParser_InvalidGuid(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidGuid", new object[] { p0 });
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x0002BDAC File Offset: 0x00029FAC
		internal static string ValueParser_InvalidDecimal(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidDecimal", new object[] { p0 });
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x0002BDD0 File Offset: 0x00029FD0
		internal static string ValueParser_InvalidDateTimeOffset(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidDateTimeOffset", new object[] { p0 });
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x0002BDF4 File Offset: 0x00029FF4
		internal static string ValueParser_InvalidDateTime(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidDateTime", new object[] { p0 });
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x0002BE18 File Offset: 0x0002A018
		internal static string ValueParser_InvalidTime(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidTime", new object[] { p0 });
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x0002BE3C File Offset: 0x0002A03C
		internal static string ValueParser_InvalidBinary(object p0)
		{
			return EntityRes.GetString("ValueParser_InvalidBinary", new object[] { p0 });
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0002BE60 File Offset: 0x0002A060
		internal static string UnknownEnumVal_Multiplicity(object p0)
		{
			return EntityRes.GetString("UnknownEnumVal_Multiplicity", new object[] { p0 });
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x0002BE84 File Offset: 0x0002A084
		internal static string UnknownEnumVal_SchemaElementKind(object p0)
		{
			return EntityRes.GetString("UnknownEnumVal_SchemaElementKind", new object[] { p0 });
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x0002BEA8 File Offset: 0x0002A0A8
		internal static string UnknownEnumVal_TypeKind(object p0)
		{
			return EntityRes.GetString("UnknownEnumVal_TypeKind", new object[] { p0 });
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x0002BECC File Offset: 0x0002A0CC
		internal static string UnknownEnumVal_PrimitiveKind(object p0)
		{
			return EntityRes.GetString("UnknownEnumVal_PrimitiveKind", new object[] { p0 });
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x0002BEF0 File Offset: 0x0002A0F0
		internal static string UnknownEnumVal_ContainerElementKind(object p0)
		{
			return EntityRes.GetString("UnknownEnumVal_ContainerElementKind", new object[] { p0 });
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x0002BF14 File Offset: 0x0002A114
		internal static string UnknownEnumVal_EdmxTarget(object p0)
		{
			return EntityRes.GetString("UnknownEnumVal_EdmxTarget", new object[] { p0 });
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x0002BF38 File Offset: 0x0002A138
		internal static string UnknownEnumVal_FunctionParameterMode(object p0)
		{
			return EntityRes.GetString("UnknownEnumVal_FunctionParameterMode", new object[] { p0 });
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x0002BF5C File Offset: 0x0002A15C
		internal static string UnknownEnumVal_ConcurrencyMode(object p0)
		{
			return EntityRes.GetString("UnknownEnumVal_ConcurrencyMode", new object[] { p0 });
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0002BF80 File Offset: 0x0002A180
		internal static string UnknownEnumVal_PropertyKind(object p0)
		{
			return EntityRes.GetString("UnknownEnumVal_PropertyKind", new object[] { p0 });
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0002BFA4 File Offset: 0x0002A1A4
		internal static string UnknownEnumVal_TermKind(object p0)
		{
			return EntityRes.GetString("UnknownEnumVal_TermKind", new object[] { p0 });
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x0002BFC8 File Offset: 0x0002A1C8
		internal static string UnknownEnumVal_ExpressionKind(object p0)
		{
			return EntityRes.GetString("UnknownEnumVal_ExpressionKind", new object[] { p0 });
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x0002BFEC File Offset: 0x0002A1EC
		internal static string Bad_AmbiguousElementBinding(object p0)
		{
			return EntityRes.GetString("Bad_AmbiguousElementBinding", new object[] { p0 });
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x0002C010 File Offset: 0x0002A210
		internal static string Bad_UnresolvedType(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedType", new object[] { p0 });
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x0002C034 File Offset: 0x0002A234
		internal static string Bad_UnresolvedComplexType(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedComplexType", new object[] { p0 });
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x0002C058 File Offset: 0x0002A258
		internal static string Bad_UnresolvedEntityType(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedEntityType", new object[] { p0 });
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x0002C07C File Offset: 0x0002A27C
		internal static string Bad_UnresolvedPrimitiveType(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedPrimitiveType", new object[] { p0 });
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x0002C0A0 File Offset: 0x0002A2A0
		internal static string Bad_UnresolvedFunction(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedFunction", new object[] { p0 });
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x0002C0C4 File Offset: 0x0002A2C4
		internal static string Bad_AmbiguousFunction(object p0)
		{
			return EntityRes.GetString("Bad_AmbiguousFunction", new object[] { p0 });
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x0002C0E8 File Offset: 0x0002A2E8
		internal static string Bad_FunctionParametersDontMatch(object p0)
		{
			return EntityRes.GetString("Bad_FunctionParametersDontMatch", new object[] { p0 });
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x0002C10C File Offset: 0x0002A30C
		internal static string Bad_UnresolvedEntitySet(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedEntitySet", new object[] { p0 });
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x0002C130 File Offset: 0x0002A330
		internal static string Bad_UnresolvedEntityContainer(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedEntityContainer", new object[] { p0 });
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x0002C154 File Offset: 0x0002A354
		internal static string Bad_UnresolvedEnumType(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedEnumType", new object[] { p0 });
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x0002C178 File Offset: 0x0002A378
		internal static string Bad_UnresolvedEnumMember(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedEnumMember", new object[] { p0 });
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x0002C19C File Offset: 0x0002A39C
		internal static string Bad_UnresolvedProperty(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedProperty", new object[] { p0 });
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x0002C1C0 File Offset: 0x0002A3C0
		internal static string Bad_UnresolvedParameter(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedParameter", new object[] { p0 });
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x0002C1E4 File Offset: 0x0002A3E4
		internal static string Bad_UnresolvedLabeledElement(object p0)
		{
			return EntityRes.GetString("Bad_UnresolvedLabeledElement", new object[] { p0 });
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x0002C208 File Offset: 0x0002A408
		internal static string Bad_CyclicEntity(object p0)
		{
			return EntityRes.GetString("Bad_CyclicEntity", new object[] { p0 });
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x0002C22C File Offset: 0x0002A42C
		internal static string Bad_CyclicComplex(object p0)
		{
			return EntityRes.GetString("Bad_CyclicComplex", new object[] { p0 });
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x0002C250 File Offset: 0x0002A450
		internal static string Bad_CyclicEntityContainer(object p0)
		{
			return EntityRes.GetString("Bad_CyclicEntityContainer", new object[] { p0 });
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x0002C274 File Offset: 0x0002A474
		internal static string Bad_UncomputableAssociationEnd(object p0)
		{
			return EntityRes.GetString("Bad_UncomputableAssociationEnd", new object[] { p0 });
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06000E4C RID: 3660 RVA: 0x0002C297 File Offset: 0x0002A497
		internal static string RuleSet_DuplicateRulesExistInRuleSet
		{
			get
			{
				return EntityRes.GetString("RuleSet_DuplicateRulesExistInRuleSet");
			}
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x0002C2A4 File Offset: 0x0002A4A4
		internal static string EdmToClr_UnsupportedTypeCode(object p0)
		{
			return EntityRes.GetString("EdmToClr_UnsupportedTypeCode", new object[] { p0 });
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06000E4E RID: 3662 RVA: 0x0002C2C7 File Offset: 0x0002A4C7
		internal static string EdmToClr_StructuredValueMappedToNonClass
		{
			get
			{
				return EntityRes.GetString("EdmToClr_StructuredValueMappedToNonClass");
			}
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x0002C2D4 File Offset: 0x0002A4D4
		internal static string EdmToClr_IEnumerableOfTPropertyAlreadyHasValue(object p0, object p1)
		{
			return EntityRes.GetString("EdmToClr_IEnumerableOfTPropertyAlreadyHasValue", new object[] { p0, p1 });
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x0002C2FC File Offset: 0x0002A4FC
		internal static string EdmToClr_StructuredPropertyDuplicateValue(object p0)
		{
			return EntityRes.GetString("EdmToClr_StructuredPropertyDuplicateValue", new object[] { p0 });
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x0002C320 File Offset: 0x0002A520
		internal static string EdmToClr_CannotConvertEdmValueToClrType(object p0, object p1)
		{
			return EntityRes.GetString("EdmToClr_CannotConvertEdmValueToClrType", new object[] { p0, p1 });
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x0002C348 File Offset: 0x0002A548
		internal static string EdmToClr_CannotConvertEdmCollectionValueToClrType(object p0)
		{
			return EntityRes.GetString("EdmToClr_CannotConvertEdmCollectionValueToClrType", new object[] { p0 });
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x0002C36C File Offset: 0x0002A56C
		internal static string EdmToClr_TryCreateObjectInstanceReturnedWrongObject(object p0, object p1)
		{
			return EntityRes.GetString("EdmToClr_TryCreateObjectInstanceReturnedWrongObject", new object[] { p0, p1 });
		}
	}
}
