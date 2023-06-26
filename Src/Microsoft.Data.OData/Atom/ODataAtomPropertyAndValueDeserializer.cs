using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x020000EC RID: 236
	internal class ODataAtomPropertyAndValueDeserializer : ODataAtomDeserializer
	{
		// Token: 0x060005E2 RID: 1506 RVA: 0x000145BC File Offset: 0x000127BC
		internal ODataAtomPropertyAndValueDeserializer(ODataAtomInputContext atomInputContext)
			: base(atomInputContext)
		{
			XmlNameTable nameTable = base.XmlReader.NameTable;
			this.EmptyNamespace = nameTable.Add(string.Empty);
			this.ODataNullAttributeName = nameTable.Add("null");
			this.ODataCollectionItemElementName = nameTable.Add("element");
			this.AtomTypeAttributeName = nameTable.Add("type");
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00014620 File Offset: 0x00012820
		internal ODataProperty ReadTopLevelProperty(IEdmStructuralProperty expectedProperty, IEdmTypeReference expectedPropertyTypeReference)
		{
			base.ReadPayloadStart();
			if (!base.UseServerFormatBehavior && !base.XmlReader.NamespaceEquals(base.XmlReader.ODataNamespace))
			{
				throw new ODataException(Strings.ODataAtomPropertyAndValueDeserializer_TopLevelPropertyElementWrongNamespace(base.XmlReader.NamespaceURI, base.XmlReader.ODataNamespace));
			}
			string expectedPropertyName = ReaderUtils.GetExpectedPropertyName(expectedProperty);
			ODataProperty odataProperty = this.ReadProperty(expectedPropertyName, expectedPropertyTypeReference, ODataNullValueBehaviorKind.Default, false);
			base.ReadPayloadEnd();
			return odataProperty;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00014690 File Offset: 0x00012890
		internal object ReadNonEntityValue(IEdmTypeReference expectedValueTypeReference, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, CollectionWithoutExpectedTypeValidator collectionValidator, bool validateNullValue, bool epmPresent)
		{
			return this.ReadNonEntityValueImplementation(expectedValueTypeReference, duplicatePropertyNamesChecker, collectionValidator, validateNullValue, epmPresent, null);
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x000146B0 File Offset: 0x000128B0
		protected EdmTypeKind GetNonEntityValueKind()
		{
			if (base.XmlReader.IsEmptyElement)
			{
				return EdmTypeKind.Primitive;
			}
			base.XmlReader.StartBuffering();
			EdmTypeKind edmTypeKind;
			try
			{
				base.XmlReader.Read();
				bool flag = false;
				for (;;)
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
						if (base.XmlReader.NamespaceEquals(base.XmlReader.ODataNamespace))
						{
							if (!base.XmlReader.LocalNameEquals(this.ODataCollectionItemElementName) || base.Version < ODataVersion.V3)
							{
								break;
							}
							flag = true;
						}
						base.XmlReader.Skip();
					}
					if (base.XmlReader.NodeType == XmlNodeType.EndElement)
					{
						goto Block_9;
					}
				}
				return EdmTypeKind.Complex;
				Block_9:
				edmTypeKind = (flag ? EdmTypeKind.Collection : EdmTypeKind.Primitive);
			}
			finally
			{
				base.XmlReader.StopBuffering();
			}
			return edmTypeKind;
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00014788 File Offset: 0x00012988
		protected void ReadNonEntityValueAttributes(out string typeName, out bool isNull)
		{
			typeName = null;
			isNull = false;
			while (base.XmlReader.MoveToNextAttribute())
			{
				if (base.XmlReader.NamespaceEquals(base.XmlReader.ODataMetadataNamespace))
				{
					if (base.XmlReader.LocalNameEquals(this.AtomTypeAttributeName))
					{
						typeName = base.XmlReader.Value;
					}
					else if (base.XmlReader.LocalNameEquals(this.ODataNullAttributeName))
					{
						isNull = ODataAtomReaderUtils.ReadMetadataNullAttributeValue(base.XmlReader.Value);
						break;
					}
				}
				else if (base.UseClientFormatBehavior && base.XmlReader.NamespaceEquals(this.EmptyNamespace) && base.XmlReader.LocalNameEquals(this.AtomTypeAttributeName))
				{
					typeName = typeName ?? base.XmlReader.Value;
				}
			}
			base.XmlReader.MoveToElement();
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0001485E File Offset: 0x00012A5E
		protected void ReadProperties(IEdmStructuredType structuredType, ReadOnlyEnumerable<ODataProperty> properties, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, bool epmPresent)
		{
			this.ReadPropertiesImplementation(structuredType, properties, duplicatePropertyNamesChecker, epmPresent);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001486C File Offset: 0x00012A6C
		private object ReadNonEntityValueImplementation(IEdmTypeReference expectedTypeReference, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, CollectionWithoutExpectedTypeValidator collectionValidator, bool validateNullValue, bool epmPresent, string propertyName)
		{
			string itemTypeNameFromCollection;
			bool flag;
			this.ReadNonEntityValueAttributes(out itemTypeNameFromCollection, out flag);
			if (!flag)
			{
				bool flag2 = false;
				if (collectionValidator != null && itemTypeNameFromCollection == null)
				{
					itemTypeNameFromCollection = collectionValidator.ItemTypeNameFromCollection;
					EdmTypeKind itemTypeKindFromCollection = collectionValidator.ItemTypeKindFromCollection;
					flag2 = itemTypeKindFromCollection != EdmTypeKind.None;
				}
				EdmTypeKind edmTypeKind;
				SerializationTypeNameAnnotation serializationTypeNameAnnotation;
				IEdmTypeReference edmTypeReference = ReaderValidationUtils.ResolvePayloadTypeNameAndComputeTargetType(EdmTypeKind.None, ODataAtomPropertyAndValueDeserializer.edmStringType, expectedTypeReference, itemTypeNameFromCollection, base.Model, base.MessageReaderSettings, base.Version, new Func<EdmTypeKind>(this.GetNonEntityValueKind), out edmTypeKind, out serializationTypeNameAnnotation);
				if (flag2)
				{
					serializationTypeNameAnnotation = new SerializationTypeNameAnnotation
					{
						TypeName = null
					};
				}
				if (collectionValidator != null)
				{
					collectionValidator.ValidateCollectionItem(itemTypeNameFromCollection, edmTypeKind);
				}
				switch (edmTypeKind)
				{
				case EdmTypeKind.Primitive:
					return this.ReadPrimitiveValue(edmTypeReference.AsPrimitive());
				case EdmTypeKind.Complex:
					return this.ReadComplexValue((edmTypeReference == null) ? null : edmTypeReference.AsComplex(), itemTypeNameFromCollection, serializationTypeNameAnnotation, duplicatePropertyNamesChecker, epmPresent);
				case EdmTypeKind.Collection:
				{
					IEdmCollectionTypeReference edmCollectionTypeReference = ValidationUtils.ValidateCollectionType(edmTypeReference);
					return this.ReadCollectionValue(edmCollectionTypeReference, itemTypeNameFromCollection, serializationTypeNameAnnotation);
				}
				}
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataAtomPropertyAndValueDeserializer_ReadNonEntityValue));
			}
			return this.ReadNullValue(expectedTypeReference, validateNullValue, propertyName);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00014985 File Offset: 0x00012B85
		private object ReadNullValue(IEdmTypeReference expectedTypeReference, bool validateNullValue, string propertyName)
		{
			base.XmlReader.SkipElementContent();
			ReaderValidationUtils.ValidateNullValue(base.Model, expectedTypeReference, base.MessageReaderSettings, validateNullValue, base.Version, propertyName);
			return null;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x000149B0 File Offset: 0x00012BB0
		private void ReadPropertiesImplementation(IEdmStructuredType structuredType, ReadOnlyEnumerable<ODataProperty> properties, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, bool epmPresent)
		{
			if (!base.XmlReader.IsEmptyElement)
			{
				base.XmlReader.ReadStartElement();
				IEdmProperty edmProperty;
				for (;;)
				{
					XmlNodeType nodeType = base.XmlReader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType != XmlNodeType.EndElement)
						{
							base.XmlReader.Skip();
						}
					}
					else if (base.XmlReader.NamespaceEquals(base.XmlReader.ODataNamespace))
					{
						edmProperty = null;
						bool flag = false;
						bool flag2 = false;
						if (structuredType != null)
						{
							edmProperty = ReaderValidationUtils.ValidateValuePropertyDefined(base.XmlReader.LocalName, structuredType, base.MessageReaderSettings, out flag2);
							if (edmProperty != null && edmProperty.PropertyKind == EdmPropertyKind.Navigation)
							{
								break;
							}
							flag = edmProperty == null;
						}
						if (flag2)
						{
							base.XmlReader.Skip();
						}
						else
						{
							ODataNullValueBehaviorKind odataNullValueBehaviorKind = ((base.ReadingResponse || edmProperty == null) ? ODataNullValueBehaviorKind.Default : base.Model.NullValueReadBehaviorKind(edmProperty));
							ODataProperty odataProperty = this.ReadProperty((edmProperty == null) ? null : edmProperty.Name, (edmProperty == null) ? null : edmProperty.Type, odataNullValueBehaviorKind, epmPresent);
							if (odataProperty != null)
							{
								if (flag)
								{
									ValidationUtils.ValidateOpenPropertyValue(odataProperty.Name, odataProperty.Value, base.MessageReaderSettings.UndeclaredPropertyBehaviorKinds);
								}
								duplicatePropertyNamesChecker.CheckForDuplicatePropertyNames(odataProperty);
								properties.AddToSourceList(odataProperty);
							}
						}
					}
					else
					{
						base.XmlReader.Skip();
					}
					if (base.XmlReader.NodeType == XmlNodeType.EndElement)
					{
						return;
					}
				}
				throw new ODataException(Strings.ODataAtomPropertyAndValueDeserializer_NavigationPropertyInProperties(edmProperty.Name, structuredType));
			}
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00014B0C File Offset: 0x00012D0C
		private ODataProperty ReadProperty(string expectedPropertyName, IEdmTypeReference expectedPropertyTypeReference, ODataNullValueBehaviorKind nullValueReadBehaviorKind, bool epmPresent)
		{
			ODataProperty odataProperty = new ODataProperty();
			string localName = base.XmlReader.LocalName;
			ValidationUtils.ValidatePropertyName(localName);
			ReaderValidationUtils.ValidateExpectedPropertyName(expectedPropertyName, localName);
			odataProperty.Name = localName;
			object obj = this.ReadNonEntityValueImplementation(expectedPropertyTypeReference, null, null, nullValueReadBehaviorKind == ODataNullValueBehaviorKind.Default, epmPresent, localName);
			if (nullValueReadBehaviorKind == ODataNullValueBehaviorKind.IgnoreValue && obj == null)
			{
				odataProperty = null;
			}
			else
			{
				odataProperty.Value = obj;
			}
			base.XmlReader.Read();
			return odataProperty;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00014B70 File Offset: 0x00012D70
		private object ReadPrimitiveValue(IEdmPrimitiveTypeReference actualValueTypeReference)
		{
			return AtomValueUtils.ReadPrimitiveValue(base.XmlReader, actualValueTypeReference);
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00014B8C File Offset: 0x00012D8C
		private ODataComplexValue ReadComplexValue(IEdmComplexTypeReference complexTypeReference, string payloadTypeName, SerializationTypeNameAnnotation serializationTypeNameAnnotation, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, bool epmPresent)
		{
			this.IncreaseRecursionDepth();
			ODataComplexValue odataComplexValue = new ODataComplexValue();
			IEdmComplexType edmComplexType = ((complexTypeReference == null) ? null : ((IEdmComplexType)complexTypeReference.Definition));
			odataComplexValue.TypeName = ((edmComplexType == null) ? payloadTypeName : edmComplexType.ODataFullName());
			if (serializationTypeNameAnnotation != null)
			{
				odataComplexValue.SetAnnotation<SerializationTypeNameAnnotation>(serializationTypeNameAnnotation);
			}
			base.XmlReader.MoveToElement();
			if (duplicatePropertyNamesChecker == null)
			{
				duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
			}
			else
			{
				duplicatePropertyNamesChecker.Clear();
			}
			ReadOnlyEnumerable<ODataProperty> readOnlyEnumerable = new ReadOnlyEnumerable<ODataProperty>();
			this.ReadPropertiesImplementation(edmComplexType, readOnlyEnumerable, duplicatePropertyNamesChecker, epmPresent);
			odataComplexValue.Properties = readOnlyEnumerable;
			this.DecreaseRecursionDepth();
			return odataComplexValue;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00014C14 File Offset: 0x00012E14
		private ODataCollectionValue ReadCollectionValue(IEdmCollectionTypeReference collectionTypeReference, string payloadTypeName, SerializationTypeNameAnnotation serializationTypeNameAnnotation)
		{
			this.IncreaseRecursionDepth();
			ODataCollectionValue odataCollectionValue = new ODataCollectionValue();
			odataCollectionValue.TypeName = ((collectionTypeReference == null) ? payloadTypeName : collectionTypeReference.ODataFullName());
			if (serializationTypeNameAnnotation != null)
			{
				odataCollectionValue.SetAnnotation<SerializationTypeNameAnnotation>(serializationTypeNameAnnotation);
			}
			base.XmlReader.MoveToElement();
			List<object> list = new List<object>();
			if (!base.XmlReader.IsEmptyElement)
			{
				base.XmlReader.ReadStartElement();
				IEdmTypeReference edmTypeReference = ((collectionTypeReference == null) ? null : collectionTypeReference.ElementType());
				DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
				CollectionWithoutExpectedTypeValidator collectionWithoutExpectedTypeValidator = null;
				if (collectionTypeReference == null)
				{
					string text = ((payloadTypeName == null) ? null : EdmLibraryExtensions.GetCollectionItemTypeName(payloadTypeName));
					collectionWithoutExpectedTypeValidator = new CollectionWithoutExpectedTypeValidator(text);
				}
				for (;;)
				{
					XmlNodeType nodeType = base.XmlReader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType != XmlNodeType.EndElement)
						{
							base.XmlReader.Skip();
						}
					}
					else if (base.XmlReader.NamespaceEquals(base.XmlReader.ODataNamespace))
					{
						if (!base.XmlReader.LocalNameEquals(this.ODataCollectionItemElementName))
						{
							break;
						}
						object obj = this.ReadNonEntityValueImplementation(edmTypeReference, duplicatePropertyNamesChecker, collectionWithoutExpectedTypeValidator, true, false, null);
						base.XmlReader.Read();
						ValidationUtils.ValidateCollectionItem(obj, false);
						list.Add(obj);
					}
					else
					{
						base.XmlReader.Skip();
					}
					if (base.XmlReader.NodeType == XmlNodeType.EndElement)
					{
						goto IL_149;
					}
				}
				throw new ODataException(Strings.ODataAtomPropertyAndValueDeserializer_InvalidCollectionElement(base.XmlReader.LocalName, base.XmlReader.ODataNamespace));
			}
			IL_149:
			odataCollectionValue.Items = new ReadOnlyEnumerable(list);
			this.DecreaseRecursionDepth();
			return odataCollectionValue;
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00014D7D File Offset: 0x00012F7D
		private void IncreaseRecursionDepth()
		{
			ValidationUtils.IncreaseAndValidateRecursionDepth(ref this.recursionDepth, base.MessageReaderSettings.MessageQuotas.MaxNestingDepth);
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00014D9A File Offset: 0x00012F9A
		private void DecreaseRecursionDepth()
		{
			this.recursionDepth--;
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00014DAA File Offset: 0x00012FAA
		[Conditional("DEBUG")]
		private void AssertRecursionDepthIsZero()
		{
		}

		// Token: 0x0400026B RID: 619
		protected readonly string EmptyNamespace;

		// Token: 0x0400026C RID: 620
		protected readonly string ODataNullAttributeName;

		// Token: 0x0400026D RID: 621
		protected readonly string ODataCollectionItemElementName;

		// Token: 0x0400026E RID: 622
		protected readonly string AtomTypeAttributeName;

		// Token: 0x0400026F RID: 623
		private static readonly IEdmType edmStringType = EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.String);

		// Token: 0x04000270 RID: 624
		private int recursionDepth;
	}
}
