using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.Diagnostics;
using System.Linq;
using System.Spatial;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x020000F1 RID: 241
	internal class ODataAtomPropertyAndValueSerializer : ODataAtomSerializer
	{
		// Token: 0x0600060F RID: 1551 RVA: 0x000153F0 File Offset: 0x000135F0
		internal ODataAtomPropertyAndValueSerializer(ODataAtomOutputContext atomOutputContext)
			: base(atomOutputContext)
		{
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x000153FC File Offset: 0x000135FC
		internal void WriteTopLevelProperty(ODataProperty property)
		{
			base.WritePayloadStart();
			this.WriteProperty(property, null, true, false, null, null, null, base.CreateDuplicatePropertyNamesChecker(), null);
			base.WritePayloadEnd();
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0001542C File Offset: 0x0001362C
		internal void WriteInstanceAnnotations(IEnumerable<AtomInstanceAnnotation> instanceAnnotations, InstanceAnnotationWriteTracker tracker)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.Ordinal);
			foreach (AtomInstanceAnnotation atomInstanceAnnotation in instanceAnnotations)
			{
				if (!hashSet.Add(atomInstanceAnnotation.TermName))
				{
					throw new ODataException(Strings.JsonLightInstanceAnnotationWriter_DuplicateAnnotationNameInCollection(atomInstanceAnnotation.TermName));
				}
				if (!tracker.IsAnnotationWritten(atomInstanceAnnotation.TermName))
				{
					this.WriteInstanceAnnotation(atomInstanceAnnotation);
					tracker.MarkAnnotationWritten(atomInstanceAnnotation.TermName);
				}
			}
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x000154BC File Offset: 0x000136BC
		internal void WriteInstanceAnnotation(AtomInstanceAnnotation instanceAnnotation)
		{
			if (base.MessageWriterSettings.ShouldSkipAnnotation(instanceAnnotation.TermName))
			{
				return;
			}
			IEdmTypeReference edmTypeReference = MetadataUtils.LookupTypeOfValueTerm(instanceAnnotation.TermName, base.Model);
			this.WriteInstanceAnnotationStart(instanceAnnotation);
			ODataPrimitiveValue odataPrimitiveValue = instanceAnnotation.Value as ODataPrimitiveValue;
			if (odataPrimitiveValue != null)
			{
				this.WritePrimitiveInstanceAnnotationValue(odataPrimitiveValue, edmTypeReference);
			}
			else
			{
				ODataComplexValue odataComplexValue = instanceAnnotation.Value as ODataComplexValue;
				if (odataComplexValue != null)
				{
					this.WriteComplexValue(odataComplexValue, edmTypeReference, false, false, null, null, base.CreateDuplicatePropertyNamesChecker(), null, null, null, null);
				}
				else
				{
					ODataCollectionValue odataCollectionValue = instanceAnnotation.Value as ODataCollectionValue;
					if (odataCollectionValue != null)
					{
						this.WriteCollectionValue(odataCollectionValue, edmTypeReference, false, false);
					}
					else
					{
						if (edmTypeReference != null && !edmTypeReference.IsNullable)
						{
							throw new ODataException(Strings.ODataAtomPropertyAndValueSerializer_NullValueNotAllowedForInstanceAnnotation(instanceAnnotation.TermName, edmTypeReference.ODataFullName()));
						}
						this.WriteNullAttribute();
					}
				}
			}
			this.WriteInstanceAnnotationEnd();
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00015580 File Offset: 0x00013780
		internal bool WriteProperties(IEdmStructuredType owningType, IEnumerable<ODataProperty> cachedProperties, bool isWritingCollection, Action beforePropertiesAction, Action afterPropertiesAction, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, EpmValueCache epmValueCache, EpmSourcePathSegment epmSourcePathSegment, ProjectedPropertiesAnnotation projectedProperties)
		{
			if (cachedProperties == null)
			{
				return false;
			}
			bool flag = false;
			foreach (ODataProperty odataProperty in cachedProperties)
			{
				flag |= this.WriteProperty(odataProperty, owningType, false, isWritingCollection, flag ? null : beforePropertiesAction, epmValueCache, epmSourcePathSegment, duplicatePropertyNamesChecker, projectedProperties);
			}
			if (afterPropertiesAction != null && flag)
			{
				afterPropertiesAction();
			}
			return flag;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x000155F4 File Offset: 0x000137F4
		internal void WritePrimitiveValue(object value, CollectionWithoutExpectedTypeValidator collectionValidator, IEdmTypeReference expectedTypeReference, SerializationTypeNameAnnotation typeNameAnnotation)
		{
			IEdmPrimitiveTypeReference primitiveTypeReference = EdmLibraryExtensions.GetPrimitiveTypeReference(value.GetType());
			if (primitiveTypeReference == null)
			{
				throw new ODataException(Strings.ValidationUtils_UnsupportedPrimitiveType(value.GetType().FullName));
			}
			if (collectionValidator != null)
			{
				collectionValidator.ValidateCollectionItem(primitiveTypeReference.FullName(), EdmTypeKind.Primitive);
			}
			if (expectedTypeReference != null)
			{
				ValidationUtils.ValidateIsExpectedPrimitiveType(value, primitiveTypeReference, expectedTypeReference);
			}
			string text;
			string valueTypeNameForWriting = base.AtomOutputContext.TypeNameOracle.GetValueTypeNameForWriting(value, primitiveTypeReference, typeNameAnnotation, collectionValidator, out text);
			if (valueTypeNameForWriting != null && valueTypeNameForWriting != "Edm.String")
			{
				this.WritePropertyTypeAttribute(valueTypeNameForWriting);
			}
			AtomValueUtils.WritePrimitiveValue(base.XmlWriter, value);
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x000156B0 File Offset: 0x000138B0
		internal bool WriteComplexValue(ODataComplexValue complexValue, IEdmTypeReference metadataTypeReference, bool isOpenPropertyType, bool isWritingCollection, Action beforeValueAction, Action afterValueAction, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, CollectionWithoutExpectedTypeValidator collectionValidator, EpmValueCache epmValueCache, EpmSourcePathSegment epmSourcePathSegment, ProjectedPropertiesAnnotation projectedProperties)
		{
			string typeName = complexValue.TypeName;
			if (collectionValidator != null)
			{
				collectionValidator.ValidateCollectionItem(typeName, EdmTypeKind.Complex);
			}
			this.IncreaseRecursionDepth();
			IEdmComplexTypeReference edmComplexTypeReference = TypeNameOracle.ResolveAndValidateTypeNameForValue(base.Model, metadataTypeReference, complexValue, isOpenPropertyType).AsComplexOrNull();
			string text;
			typeName = base.AtomOutputContext.TypeNameOracle.GetValueTypeNameForWriting(complexValue, edmComplexTypeReference, complexValue.GetAnnotation<SerializationTypeNameAnnotation>(), collectionValidator, out text);
			Action action = beforeValueAction;
			if (typeName != null)
			{
				if (beforeValueAction != null)
				{
					action = delegate
					{
						beforeValueAction();
						this.WritePropertyTypeAttribute(typeName);
					};
				}
				else
				{
					this.WritePropertyTypeAttribute(typeName);
				}
			}
			if (base.MessageWriterSettings.WriterBehavior != null && base.MessageWriterSettings.WriterBehavior.UseV1ProviderBehavior && !object.ReferenceEquals(projectedProperties, ProjectedPropertiesAnnotation.EmptyProjectedPropertiesInstance))
			{
				IEdmComplexType edmComplexType = (IEdmComplexType)edmComplexTypeReference.Definition;
				if (base.Model.EpmCachedKeepPrimitiveInContent(edmComplexType) == null)
				{
					List<string> list = null;
					foreach (IEdmProperty edmProperty in from p in edmComplexType.Properties()
						where p.Type.IsODataPrimitiveTypeKind()
						select p)
					{
						EntityPropertyMappingAttribute entityPropertyMapping = EpmWriterUtils.GetEntityPropertyMapping(epmSourcePathSegment, edmProperty.Name);
						if (entityPropertyMapping != null && entityPropertyMapping.KeepInContent)
						{
							if (list == null)
							{
								list = new List<string>();
							}
							list.Add(edmProperty.Name);
						}
					}
					base.Model.SetAnnotationValue(edmComplexType, new CachedPrimitiveKeepInContentAnnotation(list));
				}
			}
			bool flag = this.WriteProperties((edmComplexTypeReference == null) ? null : edmComplexTypeReference.ComplexDefinition(), EpmValueCache.GetComplexValueProperties(epmValueCache, complexValue, true), isWritingCollection, action, afterValueAction, duplicatePropertyNamesChecker, epmValueCache, epmSourcePathSegment, projectedProperties);
			this.DecreaseRecursionDepth();
			return flag;
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x000158AC File Offset: 0x00013AAC
		[Conditional("DEBUG")]
		internal void AssertRecursionDepthIsZero()
		{
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x000158B0 File Offset: 0x00013AB0
		private void WriteCollectionValue(ODataCollectionValue collectionValue, IEdmTypeReference propertyTypeReference, bool isOpenPropertyType, bool isWritingCollection)
		{
			this.IncreaseRecursionDepth();
			IEdmCollectionTypeReference edmCollectionTypeReference = (IEdmCollectionTypeReference)TypeNameOracle.ResolveAndValidateTypeNameForValue(base.Model, propertyTypeReference, collectionValue, isOpenPropertyType);
			string text;
			string valueTypeNameForWriting = base.AtomOutputContext.TypeNameOracle.GetValueTypeNameForWriting(collectionValue, edmCollectionTypeReference, collectionValue.GetAnnotation<SerializationTypeNameAnnotation>(), null, out text);
			if (valueTypeNameForWriting != null)
			{
				this.WritePropertyTypeAttribute(valueTypeNameForWriting);
			}
			IEdmTypeReference edmTypeReference = ((edmCollectionTypeReference == null) ? null : edmCollectionTypeReference.ElementType());
			CollectionWithoutExpectedTypeValidator collectionWithoutExpectedTypeValidator = new CollectionWithoutExpectedTypeValidator(text);
			IEnumerable items = collectionValue.Items;
			if (items != null)
			{
				DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = null;
				foreach (object obj in items)
				{
					ValidationUtils.ValidateCollectionItem(obj, false);
					base.XmlWriter.WriteStartElement("d", "element", base.MessageWriterSettings.WriterBehavior.ODataNamespace);
					ODataComplexValue odataComplexValue = obj as ODataComplexValue;
					if (odataComplexValue != null)
					{
						if (duplicatePropertyNamesChecker == null)
						{
							duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
						}
						this.WriteComplexValue(odataComplexValue, edmTypeReference, false, isWritingCollection, null, null, duplicatePropertyNamesChecker, collectionWithoutExpectedTypeValidator, null, null, null);
						duplicatePropertyNamesChecker.Clear();
					}
					else
					{
						this.WritePrimitiveValue(obj, collectionWithoutExpectedTypeValidator, edmTypeReference, null);
					}
					base.XmlWriter.WriteEndElement();
				}
			}
			this.DecreaseRecursionDepth();
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x000159F0 File Offset: 0x00013BF0
		private void WritePrimitiveInstanceAnnotationValue(ODataPrimitiveValue primitiveValue, IEdmTypeReference expectedTypeReference)
		{
			object value = primitiveValue.Value;
			IEdmPrimitiveTypeReference primitiveTypeReference = EdmLibraryExtensions.GetPrimitiveTypeReference(value.GetType());
			string text = AtomInstanceAnnotation.LookupAttributeValueNotationNameByEdmTypeKind(primitiveTypeReference.PrimitiveKind());
			if (text != null)
			{
				if (expectedTypeReference != null)
				{
					ValidationUtils.ValidateIsExpectedPrimitiveType(primitiveValue.Value, primitiveTypeReference, expectedTypeReference);
				}
				base.XmlWriter.WriteAttributeString(text, AtomValueUtils.ConvertPrimitiveToString(value));
				return;
			}
			this.WritePrimitiveValue(value, null, expectedTypeReference, primitiveValue.GetAnnotation<SerializationTypeNameAnnotation>());
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00015A54 File Offset: 0x00013C54
		private bool WriteProperty(ODataProperty property, IEdmStructuredType owningType, bool isTopLevel, bool isWritingCollection, Action beforePropertyAction, EpmValueCache epmValueCache, EpmSourcePathSegment epmParentSourcePathSegment, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, ProjectedPropertiesAnnotation projectedProperties)
		{
			WriterValidationUtils.ValidatePropertyNotNull(property);
			object value = property.Value;
			string name = property.Name;
			EpmSourcePathSegment propertySourcePathSegment = EpmWriterUtils.GetPropertySourcePathSegment(epmParentSourcePathSegment, name);
			ODataComplexValue odataComplexValue = value as ODataComplexValue;
			ProjectedPropertiesAnnotation projectedPropertiesAnnotation = null;
			if (!this.ShouldWritePropertyInContent(owningType, projectedProperties, name, value, propertySourcePathSegment))
			{
				if (propertySourcePathSegment == null || odataComplexValue == null)
				{
					return false;
				}
				projectedPropertiesAnnotation = ProjectedPropertiesAnnotation.EmptyProjectedPropertiesInstance;
			}
			WriterValidationUtils.ValidatePropertyName(name);
			duplicatePropertyNamesChecker.CheckForDuplicatePropertyNames(property);
			IEdmProperty edmProperty = WriterValidationUtils.ValidatePropertyDefined(name, owningType, base.MessageWriterSettings.UndeclaredPropertyBehaviorKinds);
			IEdmTypeReference edmTypeReference = ((edmProperty == null) ? null : edmProperty.Type);
			if (value is ODataStreamReferenceValue)
			{
				throw new ODataException(Strings.ODataWriter_StreamPropertiesMustBePropertiesOfODataEntry(name));
			}
			if ((edmTypeReference != null && edmTypeReference.IsSpatial()) || (edmTypeReference == null && value is ISpatial))
			{
				ODataVersionChecker.CheckSpatialValue(base.Version);
			}
			if (value == null)
			{
				this.WriteNullPropertyValue(edmTypeReference, name, isTopLevel, isWritingCollection, beforePropertyAction);
				return true;
			}
			bool flag = owningType != null && owningType.IsOpen && edmTypeReference == null;
			if (flag)
			{
				ValidationUtils.ValidateOpenPropertyValue(name, value, base.MessageWriterSettings.UndeclaredPropertyBehaviorKinds);
			}
			if (odataComplexValue != null)
			{
				return this.WriteComplexValueProperty(odataComplexValue, name, isTopLevel, isWritingCollection, beforePropertyAction, epmValueCache, edmTypeReference, flag, propertySourcePathSegment, projectedPropertiesAnnotation);
			}
			ODataCollectionValue odataCollectionValue = value as ODataCollectionValue;
			if (odataCollectionValue != null)
			{
				this.WriteCollectionValueProperty(odataCollectionValue, name, isTopLevel, isWritingCollection, beforePropertyAction, edmTypeReference, flag);
				return true;
			}
			this.WritePropertyStart(beforePropertyAction, name, isWritingCollection, isTopLevel);
			SerializationTypeNameAnnotation annotation = property.ODataValue.GetAnnotation<SerializationTypeNameAnnotation>();
			this.WritePrimitiveValue(value, null, edmTypeReference, annotation);
			this.WritePropertyEnd();
			return true;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00015BE0 File Offset: 0x00013DE0
		private bool WriteComplexValueProperty(ODataComplexValue complexValue, string propertyName, bool isTopLevel, bool isWritingCollection, Action beforeValueAction, EpmValueCache epmValueCache, IEdmTypeReference propertyTypeReference, bool isOpenPropertyType, EpmSourcePathSegment epmSourcePathSegment, ProjectedPropertiesAnnotation complexValueProjectedProperties)
		{
			DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
			if (isTopLevel)
			{
				this.WritePropertyStart(beforeValueAction, propertyName, isWritingCollection, true);
				this.WriteComplexValue(complexValue, propertyTypeReference, isOpenPropertyType, isWritingCollection, null, null, duplicatePropertyNamesChecker, null, epmValueCache, epmSourcePathSegment, null);
				this.WritePropertyEnd();
				return true;
			}
			return this.WriteComplexValue(complexValue, propertyTypeReference, isOpenPropertyType, isWritingCollection, delegate
			{
				this.WritePropertyStart(beforeValueAction, propertyName, isWritingCollection, false);
			}, new Action(this.WritePropertyEnd), duplicatePropertyNamesChecker, null, epmValueCache, epmSourcePathSegment, complexValueProjectedProperties);
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00015C88 File Offset: 0x00013E88
		private void WriteCollectionValueProperty(ODataCollectionValue collectionValue, string propertyName, bool isTopLevel, bool isWritingTopLevelCollection, Action beforePropertyAction, IEdmTypeReference propertyTypeReference, bool isOpenPropertyType)
		{
			ODataVersionChecker.CheckCollectionValueProperties(base.Version, propertyName);
			this.WritePropertyStart(beforePropertyAction, propertyName, isWritingTopLevelCollection, isTopLevel);
			this.WriteCollectionValue(collectionValue, propertyTypeReference, isOpenPropertyType, isWritingTopLevelCollection);
			this.WritePropertyEnd();
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00015CB8 File Offset: 0x00013EB8
		private bool ShouldWritePropertyInContent(IEdmStructuredType owningType, ProjectedPropertiesAnnotation projectedProperties, string propertyName, object propertyValue, EpmSourcePathSegment epmSourcePathSegment)
		{
			bool flag = !projectedProperties.ShouldSkipProperty(propertyName);
			bool flag2 = base.MessageWriterSettings.WriterBehavior != null && base.MessageWriterSettings.WriterBehavior.UseV1ProviderBehavior;
			if (flag2 && owningType != null && owningType.IsODataComplexTypeKind())
			{
				IEdmComplexType edmComplexType = (IEdmComplexType)owningType;
				CachedPrimitiveKeepInContentAnnotation cachedPrimitiveKeepInContentAnnotation = base.Model.EpmCachedKeepPrimitiveInContent(edmComplexType);
				if (cachedPrimitiveKeepInContentAnnotation != null && cachedPrimitiveKeepInContentAnnotation.IsKeptInContent(propertyName))
				{
					return flag;
				}
			}
			if (propertyValue == null && epmSourcePathSegment != null)
			{
				return true;
			}
			EntityPropertyMappingAttribute entityPropertyMapping = EpmWriterUtils.GetEntityPropertyMapping(epmSourcePathSegment);
			if (entityPropertyMapping == null)
			{
				return flag;
			}
			string text = propertyValue as string;
			if (text != null && text.Length == 0)
			{
				switch (entityPropertyMapping.TargetSyndicationItem)
				{
				case SyndicationItemProperty.AuthorEmail:
				case SyndicationItemProperty.AuthorUri:
				case SyndicationItemProperty.ContributorEmail:
				case SyndicationItemProperty.ContributorUri:
					return true;
				}
			}
			return entityPropertyMapping.KeepInContent && flag;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00015D8C File Offset: 0x00013F8C
		private void WriteNullPropertyValue(IEdmTypeReference propertyTypeReference, string propertyName, bool isTopLevel, bool isWritingCollection, Action beforePropertyAction)
		{
			WriterValidationUtils.ValidateNullPropertyValue(propertyTypeReference, propertyName, base.MessageWriterSettings.WriterBehavior, base.Model);
			this.WritePropertyStart(beforePropertyAction, propertyName, isWritingCollection, isTopLevel);
			if (propertyTypeReference != null && !base.UseDefaultFormatBehavior)
			{
				string text = propertyTypeReference.ODataFullName();
				if (text != "Edm.String" && (propertyTypeReference.IsODataPrimitiveTypeKind() || base.UseServerFormatBehavior))
				{
					this.WritePropertyTypeAttribute(text);
				}
			}
			this.WriteNullAttribute();
			this.WritePropertyEnd();
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00015E00 File Offset: 0x00014000
		private void WritePropertyStart(Action beforePropertyCallback, string propertyName, bool isWritingCollection, bool isTopLevel)
		{
			if (beforePropertyCallback != null)
			{
				beforePropertyCallback();
			}
			base.XmlWriter.WriteStartElement(isWritingCollection ? string.Empty : "d", propertyName, base.MessageWriterSettings.WriterBehavior.ODataNamespace);
			if (isTopLevel)
			{
				ODataAtomSerializer.DefaultNamespaceFlags defaultNamespaceFlags = ODataAtomSerializer.DefaultNamespaceFlags.ODataMetadata | ODataAtomSerializer.DefaultNamespaceFlags.GeoRss | ODataAtomSerializer.DefaultNamespaceFlags.Gml;
				if (!base.MessageWriterSettings.AlwaysUseDefaultXmlNamespaceForRootElement)
				{
					defaultNamespaceFlags |= ODataAtomSerializer.DefaultNamespaceFlags.OData;
				}
				base.WriteDefaultNamespaceAttributes(defaultNamespaceFlags);
			}
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00015E60 File Offset: 0x00014060
		private void WritePropertyEnd()
		{
			base.XmlWriter.WriteEndElement();
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00015E70 File Offset: 0x00014070
		private void WriteInstanceAnnotationStart(AtomInstanceAnnotation instanceAnnotation)
		{
			base.XmlWriter.WriteStartElement("annotation", "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata");
			base.XmlWriter.WriteAttributeString("term", string.Empty, instanceAnnotation.TermName);
			if (!string.IsNullOrEmpty(instanceAnnotation.Target))
			{
				base.XmlWriter.WriteAttributeString("target", string.Empty, instanceAnnotation.Target);
			}
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00015ED5 File Offset: 0x000140D5
		private void WriteInstanceAnnotationEnd()
		{
			base.XmlWriter.WriteEndElement();
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00015EE2 File Offset: 0x000140E2
		private void WritePropertyTypeAttribute(string typeName)
		{
			base.XmlWriter.WriteAttributeString("m", "type", "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", typeName);
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00015EFF File Offset: 0x000140FF
		private void WriteNullAttribute()
		{
			base.XmlWriter.WriteAttributeString("m", "null", "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", "true");
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00015F20 File Offset: 0x00014120
		private void IncreaseRecursionDepth()
		{
			ValidationUtils.IncreaseAndValidateRecursionDepth(ref this.recursionDepth, base.MessageWriterSettings.MessageQuotas.MaxNestingDepth);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00015F3D File Offset: 0x0001413D
		private void DecreaseRecursionDepth()
		{
			this.recursionDepth--;
		}

		// Token: 0x0400027C RID: 636
		private int recursionDepth;
	}
}
