using System;

namespace Microsoft.Data.Edm.Validation
{
	// Token: 0x0200023E RID: 574
	public enum EdmErrorCode
	{
		// Token: 0x04000688 RID: 1672
		InvalidErrorCodeValue,
		// Token: 0x04000689 RID: 1673
		StreamTypeReferencesNotSupportedBeforeV3,
		// Token: 0x0400068A RID: 1674
		SpatialTypeReferencesNotSupportedBeforeV3 = 3,
		// Token: 0x0400068B RID: 1675
		XmlError = 5,
		// Token: 0x0400068C RID: 1676
		UnexpectedXmlNodeType = 8,
		// Token: 0x0400068D RID: 1677
		UnexpectedXmlAttribute,
		// Token: 0x0400068E RID: 1678
		UnexpectedXmlElement,
		// Token: 0x0400068F RID: 1679
		TextNotAllowed,
		// Token: 0x04000690 RID: 1680
		EmptyFile,
		// Token: 0x04000691 RID: 1681
		MissingAttribute = 15,
		// Token: 0x04000692 RID: 1682
		InvalidName = 17,
		// Token: 0x04000693 RID: 1683
		MissingType,
		// Token: 0x04000694 RID: 1684
		AlreadyDefined,
		// Token: 0x04000695 RID: 1685
		InvalidVersionNumber = 25,
		// Token: 0x04000696 RID: 1686
		InvalidBoolean = 27,
		// Token: 0x04000697 RID: 1687
		BadProperty = 42,
		// Token: 0x04000698 RID: 1688
		InvalidPropertyType = 44,
		// Token: 0x04000699 RID: 1689
		PrecisionOutOfRange = 51,
		// Token: 0x0400069A RID: 1690
		ScaleOutOfRange,
		// Token: 0x0400069B RID: 1691
		NameTooLong = 60,
		// Token: 0x0400069C RID: 1692
		InvalidAssociation = 62,
		// Token: 0x0400069D RID: 1693
		BadNavigationProperty = 74,
		// Token: 0x0400069E RID: 1694
		InvalidKey,
		// Token: 0x0400069F RID: 1695
		InterfaceCriticalPropertyValueMustNotBeNull,
		// Token: 0x040006A0 RID: 1696
		InterfaceCriticalKindValueMismatch,
		// Token: 0x040006A1 RID: 1697
		InterfaceCriticalKindValueUnexpected,
		// Token: 0x040006A2 RID: 1698
		InterfaceCriticalEnumerableMustNotHaveNullElements,
		// Token: 0x040006A3 RID: 1699
		InterfaceCriticalEnumPropertyValueOutOfRange,
		// Token: 0x040006A4 RID: 1700
		InterfaceCriticalNavigationPartnerInvalid,
		// Token: 0x040006A5 RID: 1701
		InterfaceCriticalCycleInTypeHierarchy,
		// Token: 0x040006A6 RID: 1702
		InvalidMultiplicity = 92,
		// Token: 0x040006A7 RID: 1703
		InvalidAction = 96,
		// Token: 0x040006A8 RID: 1704
		InvalidOnDelete,
		// Token: 0x040006A9 RID: 1705
		BadUnresolvedComplexType,
		// Token: 0x040006AA RID: 1706
		InvalidEndEntitySet = 100,
		// Token: 0x040006AB RID: 1707
		FunctionImportEntitySetExpressionIsInvalid = 103,
		// Token: 0x040006AC RID: 1708
		EntitySetNavigationPropertyMappingMustPointToValidTargetForProperty = 109,
		// Token: 0x040006AD RID: 1709
		InvalidRoleInRelationshipConstraint,
		// Token: 0x040006AE RID: 1710
		InvalidPropertyInRelationshipConstraint,
		// Token: 0x040006AF RID: 1711
		TypeMismatchRelationshipConstraint,
		// Token: 0x040006B0 RID: 1712
		InvalidMultiplicityOfPrincipalEnd,
		// Token: 0x040006B1 RID: 1713
		MismatchNumberOfPropertiesInRelationshipConstraint,
		// Token: 0x040006B2 RID: 1714
		InvalidMultiplicityOfDependentEnd = 116,
		// Token: 0x040006B3 RID: 1715
		OpenTypeNotSupported,
		// Token: 0x040006B4 RID: 1716
		VocabularyAnnotationsNotSupportedBeforeV3,
		// Token: 0x040006B5 RID: 1717
		SameRoleReferredInReferentialConstraint,
		// Token: 0x040006B6 RID: 1718
		EntityKeyMustBeScalar = 128,
		// Token: 0x040006B7 RID: 1719
		EntityKeyMustNotBeBinary,
		// Token: 0x040006B8 RID: 1720
		EndWithManyMultiplicityCannotHaveOperationsSpecified = 132,
		// Token: 0x040006B9 RID: 1721
		EntitySetTypeHasNoKeys,
		// Token: 0x040006BA RID: 1722
		InvalidConcurrencyMode = 144,
		// Token: 0x040006BB RID: 1723
		ConcurrencyRedefinedOnSubtypeOfEntitySetType,
		// Token: 0x040006BC RID: 1724
		FunctionImportUnsupportedReturnType,
		// Token: 0x040006BD RID: 1725
		ComposableFunctionImportCannotBeSideEffecting,
		// Token: 0x040006BE RID: 1726
		FunctionImportReturnsEntitiesButDoesNotSpecifyEntitySet,
		// Token: 0x040006BF RID: 1727
		FunctionImportEntityTypeDoesNotMatchEntitySet,
		// Token: 0x040006C0 RID: 1728
		FunctionImportSpecifiesEntitySetButDoesNotReturnEntityType,
		// Token: 0x040006C1 RID: 1729
		ComposableFunctionImportMustHaveReturnType = 152,
		// Token: 0x040006C2 RID: 1730
		SimilarRelationshipEnd,
		// Token: 0x040006C3 RID: 1731
		DuplicatePropertySpecifiedInEntityKey,
		// Token: 0x040006C4 RID: 1732
		NullableComplexTypeProperty = 157,
		// Token: 0x040006C5 RID: 1733
		KeyMissingOnEntityType = 159,
		// Token: 0x040006C6 RID: 1734
		SystemNamespaceEncountered = 161,
		// Token: 0x040006C7 RID: 1735
		InvalidNamespaceName = 163,
		// Token: 0x040006C8 RID: 1736
		EnumMemberValueOutOfRange = 206,
		// Token: 0x040006C9 RID: 1737
		DuplicateEntityContainerMemberName = 218,
		// Token: 0x040006CA RID: 1738
		InvalidAbstractComplexType = 220,
		// Token: 0x040006CB RID: 1739
		InvalidPolymorphicComplexType,
		// Token: 0x040006CC RID: 1740
		NavigationPropertyEntityMustNotIndirectlyContainItself,
		// Token: 0x040006CD RID: 1741
		EntitySetRecursiveNavigationPropertyMappingsMustPointBackToSourceEntitySet,
		// Token: 0x040006CE RID: 1742
		BadAmbiguousElementBinding,
		// Token: 0x040006CF RID: 1743
		BadUnresolvedType,
		// Token: 0x040006D0 RID: 1744
		BadUnresolvedPrimitiveType,
		// Token: 0x040006D1 RID: 1745
		BadCyclicComplex,
		// Token: 0x040006D2 RID: 1746
		BadCyclicEntityContainer,
		// Token: 0x040006D3 RID: 1747
		BadCyclicEntity,
		// Token: 0x040006D4 RID: 1748
		TypeSemanticsCouldNotConvertTypeReference,
		// Token: 0x040006D5 RID: 1749
		ConstructibleEntitySetTypeInvalidFromEntityTypeRemoval,
		// Token: 0x040006D6 RID: 1750
		BadUnresolvedEntityContainer,
		// Token: 0x040006D7 RID: 1751
		BadUnresolvedEntitySet,
		// Token: 0x040006D8 RID: 1752
		BadUnresolvedProperty,
		// Token: 0x040006D9 RID: 1753
		BadNonComputableAssociationEnd,
		// Token: 0x040006DA RID: 1754
		NavigationPropertyTypeInvalidBecauseOfBadAssociation,
		// Token: 0x040006DB RID: 1755
		EntityMustHaveEntityBaseType,
		// Token: 0x040006DC RID: 1756
		ComplexTypeMustHaveComplexBaseType,
		// Token: 0x040006DD RID: 1757
		BadUnresolvedFunction,
		// Token: 0x040006DE RID: 1758
		RowTypeMustNotHaveBaseType,
		// Token: 0x040006DF RID: 1759
		AssociationSetEndRoleMustBelongToSetElementType,
		// Token: 0x040006E0 RID: 1760
		KeyPropertyMustBelongToEntity,
		// Token: 0x040006E1 RID: 1761
		ReferentialConstraintPrincipalEndMustBelongToAssociation,
		// Token: 0x040006E2 RID: 1762
		DependentPropertiesMustBelongToDependentEntity,
		// Token: 0x040006E3 RID: 1763
		DeclaringTypeMustBeCorrect,
		// Token: 0x040006E4 RID: 1764
		FunctionsNotSupportedBeforeV2 = 256,
		// Token: 0x040006E5 RID: 1765
		ValueTermsNotSupportedBeforeV3,
		// Token: 0x040006E6 RID: 1766
		InvalidNavigationPropertyType,
		// Token: 0x040006E7 RID: 1767
		UnderlyingTypeIsBadBecauseEnumTypeIsBad = 261,
		// Token: 0x040006E8 RID: 1768
		InvalidAssociationSetEndSetWrongType,
		// Token: 0x040006E9 RID: 1769
		OnlyInputParametersAllowedInFunctions,
		// Token: 0x040006EA RID: 1770
		ComplexTypeMustHaveProperties,
		// Token: 0x040006EB RID: 1771
		FunctionImportParameterIncorrectType,
		// Token: 0x040006EC RID: 1772
		RowTypeMustHaveProperties,
		// Token: 0x040006ED RID: 1773
		DuplicateDependentProperty,
		// Token: 0x040006EE RID: 1774
		BindableFunctionImportMustHaveParameters,
		// Token: 0x040006EF RID: 1775
		FunctionImportSideEffectingNotSupportedBeforeV3,
		// Token: 0x040006F0 RID: 1776
		FunctionImportComposableNotSupportedBeforeV3,
		// Token: 0x040006F1 RID: 1777
		FunctionImportBindableNotSupportedBeforeV3,
		// Token: 0x040006F2 RID: 1778
		MaxLengthOutOfRange,
		// Token: 0x040006F3 RID: 1779
		PathExpressionHasNoEntityContext = 274,
		// Token: 0x040006F4 RID: 1780
		InvalidSrid,
		// Token: 0x040006F5 RID: 1781
		InvalidMaxLength,
		// Token: 0x040006F6 RID: 1782
		InvalidLong,
		// Token: 0x040006F7 RID: 1783
		InvalidInteger,
		// Token: 0x040006F8 RID: 1784
		InvalidAssociationSet,
		// Token: 0x040006F9 RID: 1785
		InvalidParameterMode,
		// Token: 0x040006FA RID: 1786
		BadUnresolvedEntityType,
		// Token: 0x040006FB RID: 1787
		InvalidValue,
		// Token: 0x040006FC RID: 1788
		InvalidBinary,
		// Token: 0x040006FD RID: 1789
		InvalidFloatingPoint,
		// Token: 0x040006FE RID: 1790
		InvalidDateTime,
		// Token: 0x040006FF RID: 1791
		InvalidDateTimeOffset,
		// Token: 0x04000700 RID: 1792
		InvalidDecimal,
		// Token: 0x04000701 RID: 1793
		InvalidGuid,
		// Token: 0x04000702 RID: 1794
		InvalidTypeKindNone,
		// Token: 0x04000703 RID: 1795
		InvalidIfExpressionIncorrectNumberOfOperands,
		// Token: 0x04000704 RID: 1796
		EnumsNotSupportedBeforeV3,
		// Token: 0x04000705 RID: 1797
		EnumMemberTypeMustMatchEnumUnderlyingType,
		// Token: 0x04000706 RID: 1798
		InvalidIsTypeExpressionIncorrectNumberOfOperands,
		// Token: 0x04000707 RID: 1799
		InvalidTypeName,
		// Token: 0x04000708 RID: 1800
		InvalidQualifiedName,
		// Token: 0x04000709 RID: 1801
		NoReadersProvided,
		// Token: 0x0400070A RID: 1802
		NullXmlReader,
		// Token: 0x0400070B RID: 1803
		IsUnboundedCannotBeTrueWhileMaxLengthIsNotNull,
		// Token: 0x0400070C RID: 1804
		InvalidElementAnnotation,
		// Token: 0x0400070D RID: 1805
		InvalidLabeledElementExpressionIncorrectNumberOfOperands,
		// Token: 0x0400070E RID: 1806
		BadUnresolvedLabeledElement,
		// Token: 0x0400070F RID: 1807
		BadUnresolvedEnumMember,
		// Token: 0x04000710 RID: 1808
		InvalidAssertTypeExpressionIncorrectNumberOfOperands,
		// Token: 0x04000711 RID: 1809
		BadUnresolvedParameter,
		// Token: 0x04000712 RID: 1810
		NavigationPropertyWithRecursiveContainmentTargetMustBeOptional,
		// Token: 0x04000713 RID: 1811
		NavigationPropertyWithRecursiveContainmentSourceMustBeFromZeroOrOne,
		// Token: 0x04000714 RID: 1812
		NavigationPropertyWithNonRecursiveContainmentSourceMustBeFromOne,
		// Token: 0x04000715 RID: 1813
		NavigationPropertyContainsTargetNotSupportedBeforeV3,
		// Token: 0x04000716 RID: 1814
		ImpossibleAnnotationsTarget,
		// Token: 0x04000717 RID: 1815
		CannotAssertNullableTypeAsNonNullableType,
		// Token: 0x04000718 RID: 1816
		CannotAssertPrimitiveExpressionAsNonPrimitiveType,
		// Token: 0x04000719 RID: 1817
		ExpressionPrimitiveKindNotValidForAssertedType,
		// Token: 0x0400071A RID: 1818
		NullCannotBeAssertedToBeANonNullableType,
		// Token: 0x0400071B RID: 1819
		ExpressionNotValidForTheAssertedType,
		// Token: 0x0400071C RID: 1820
		CollectionExpressionNotValidForNonCollectionType,
		// Token: 0x0400071D RID: 1821
		RecordExpressionNotValidForNonStructuredType,
		// Token: 0x0400071E RID: 1822
		RecordExpressionMissingRequiredProperty,
		// Token: 0x0400071F RID: 1823
		RecordExpressionHasExtraProperties,
		// Token: 0x04000720 RID: 1824
		DuplicateAnnotation,
		// Token: 0x04000721 RID: 1825
		IncorrectNumberOfArguments,
		// Token: 0x04000722 RID: 1826
		DuplicateAlias,
		// Token: 0x04000723 RID: 1827
		ReferencedTypeMustHaveValidName,
		// Token: 0x04000724 RID: 1828
		SingleFileExpected,
		// Token: 0x04000725 RID: 1829
		UnknownEdmxVersion,
		// Token: 0x04000726 RID: 1830
		UnknownEdmVersion,
		// Token: 0x04000727 RID: 1831
		NoSchemasProduced,
		// Token: 0x04000728 RID: 1832
		DuplicateEntityContainerName,
		// Token: 0x04000729 RID: 1833
		ContainerElementContainerNameIncorrect,
		// Token: 0x0400072A RID: 1834
		PrimitiveConstantExpressionNotValidForNonPrimitiveType,
		// Token: 0x0400072B RID: 1835
		IntegerConstantValueOutOfRange,
		// Token: 0x0400072C RID: 1836
		StringConstantLengthOutOfRange,
		// Token: 0x0400072D RID: 1837
		BinaryConstantLengthOutOfRange,
		// Token: 0x0400072E RID: 1838
		InvalidFunctionImportParameterMode,
		// Token: 0x0400072F RID: 1839
		TypeMustNotHaveKindOfNone,
		// Token: 0x04000730 RID: 1840
		PrimitiveTypeMustNotHaveKindOfNone,
		// Token: 0x04000731 RID: 1841
		PropertyMustNotHaveKindOfNone,
		// Token: 0x04000732 RID: 1842
		TermMustNotHaveKindOfNone,
		// Token: 0x04000733 RID: 1843
		SchemaElementMustNotHaveKindOfNone,
		// Token: 0x04000734 RID: 1844
		EntityContainerElementMustNotHaveKindOfNone,
		// Token: 0x04000735 RID: 1845
		BinaryValueCannotHaveEmptyValue,
		// Token: 0x04000736 RID: 1846
		EntitySetCanOnlyBeContainedByASingleNavigationProperty,
		// Token: 0x04000737 RID: 1847
		InconsistentNavigationPropertyPartner,
		// Token: 0x04000738 RID: 1848
		EntitySetCanOnlyHaveSingleNavigationPropertyWithContainment,
		// Token: 0x04000739 RID: 1849
		EntitySetNavigationMappingMustBeBidirectional,
		// Token: 0x0400073A RID: 1850
		DuplicateNavigationPropertyMapping,
		// Token: 0x0400073B RID: 1851
		AllNavigationPropertiesMustBeMapped,
		// Token: 0x0400073C RID: 1852
		TypeAnnotationMissingRequiredProperty,
		// Token: 0x0400073D RID: 1853
		TypeAnnotationHasExtraProperties,
		// Token: 0x0400073E RID: 1854
		InvalidTime,
		// Token: 0x0400073F RID: 1855
		InvalidPrimitiveValue,
		// Token: 0x04000740 RID: 1856
		EnumMustHaveIntegerUnderlyingType,
		// Token: 0x04000741 RID: 1857
		BadUnresolvedTerm,
		// Token: 0x04000742 RID: 1858
		BadPrincipalPropertiesInReferentialConstraint,
		// Token: 0x04000743 RID: 1859
		DuplicateDirectValueAnnotationFullName,
		// Token: 0x04000744 RID: 1860
		NoEntitySetsFoundForType,
		// Token: 0x04000745 RID: 1861
		CannotInferEntitySetWithMultipleSetsPerType,
		// Token: 0x04000746 RID: 1862
		InvalidEntitySetPath,
		// Token: 0x04000747 RID: 1863
		InvalidEnumMemberPath,
		// Token: 0x04000748 RID: 1864
		QualifierMustBeSimpleName,
		// Token: 0x04000749 RID: 1865
		BadUnresolvedEnumType,
		// Token: 0x0400074A RID: 1866
		BadUnresolvedTarget,
		// Token: 0x0400074B RID: 1867
		PathIsNotValidForTheGivenContext
	}
}
