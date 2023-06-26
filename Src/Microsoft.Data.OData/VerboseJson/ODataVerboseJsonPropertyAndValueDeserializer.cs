using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Json;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x020001B9 RID: 441
	internal class ODataVerboseJsonPropertyAndValueDeserializer : ODataVerboseJsonDeserializer
	{
		// Token: 0x06000DB7 RID: 3511 RVA: 0x0002FA59 File Offset: 0x0002DC59
		internal ODataVerboseJsonPropertyAndValueDeserializer(ODataVerboseJsonInputContext verboseJsonInputContext)
			: base(verboseJsonInputContext)
		{
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x0002FA64 File Offset: 0x0002DC64
		internal ODataProperty ReadTopLevelProperty(IEdmStructuralProperty expectedProperty, IEdmTypeReference expectedPropertyTypeReference)
		{
			if (!base.Model.IsUserModel())
			{
				throw new ODataException(Strings.ODataJsonPropertyAndValueDeserializer_TopLevelPropertyWithoutMetadata);
			}
			base.ReadPayloadStart(false);
			string expectedPropertyName = ReaderUtils.GetExpectedPropertyName(expectedProperty);
			object obj = null;
			string text;
			if (this.ShouldReadTopLevelPropertyValueWithoutPropertyWrapper(expectedPropertyTypeReference))
			{
				text = expectedPropertyName ?? string.Empty;
				obj = this.ReadNonEntityValue(expectedPropertyTypeReference, null, null, true, text);
			}
			else
			{
				base.JsonReader.ReadStartObject();
				bool flag = false;
				string text2 = null;
				while (base.JsonReader.NodeType == JsonNodeType.Property)
				{
					text = base.JsonReader.ReadPropertyName();
					if (!ValidationUtils.IsValidPropertyName(text))
					{
						base.JsonReader.SkipValue();
					}
					else
					{
						if (flag)
						{
							throw new ODataException(Strings.ODataJsonPropertyAndValueDeserializer_InvalidTopLevelPropertyPayload);
						}
						flag = true;
						text2 = text;
						obj = this.ReadNonEntityValue(expectedPropertyTypeReference, null, null, true, text);
					}
				}
				if (!flag)
				{
					throw new ODataException(Strings.ODataJsonPropertyAndValueDeserializer_InvalidTopLevelPropertyPayload);
				}
				ReaderValidationUtils.ValidateExpectedPropertyName(expectedPropertyName, text2);
				text = text2;
				base.JsonReader.Read();
			}
			base.ReadPayloadEnd(false);
			return new ODataProperty
			{
				Name = text,
				Value = obj
			};
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x0002FB68 File Offset: 0x0002DD68
		internal string FindTypeNameInPayload()
		{
			if (base.JsonReader.NodeType == JsonNodeType.PrimitiveValue)
			{
				return null;
			}
			base.JsonReader.StartBuffering();
			base.JsonReader.ReadStartObject();
			string text = null;
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text2 = base.JsonReader.ReadPropertyName();
				if (string.CompareOrdinal(text2, "__metadata") == 0)
				{
					text = this.ReadTypeNameFromMetadataPropertyValue();
					break;
				}
				base.JsonReader.SkipValue();
			}
			base.JsonReader.StopBuffering();
			return text;
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0002FBE8 File Offset: 0x0002DDE8
		internal object ReadNonEntityValue(IEdmTypeReference expectedValueTypeReference, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, CollectionWithoutExpectedTypeValidator collectionValidator, bool validateNullValue, string propertyName)
		{
			return this.ReadNonEntityValueImplementation(expectedValueTypeReference, duplicatePropertyNamesChecker, collectionValidator, validateNullValue, propertyName);
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x0002FC04 File Offset: 0x0002DE04
		internal string ReadTypeNameFromMetadataPropertyValue()
		{
			string text = null;
			if (base.JsonReader.NodeType != JsonNodeType.StartObject)
			{
				throw new ODataException(Strings.ODataJsonPropertyAndValueDeserializer_MetadataPropertyMustHaveObjectValue(base.JsonReader.NodeType));
			}
			base.JsonReader.ReadStartObject();
			ODataVerboseJsonReaderUtils.MetadataPropertyBitMask metadataPropertyBitMask = ODataVerboseJsonReaderUtils.MetadataPropertyBitMask.None;
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text2 = base.JsonReader.ReadPropertyName();
				if (string.CompareOrdinal("type", text2) == 0)
				{
					ODataVerboseJsonReaderUtils.VerifyMetadataPropertyNotFound(ref metadataPropertyBitMask, ODataVerboseJsonReaderUtils.MetadataPropertyBitMask.Type, "type");
					object obj = base.JsonReader.ReadPrimitiveValue();
					text = obj as string;
					if (text == null)
					{
						throw new ODataException(Strings.ODataJsonPropertyAndValueDeserializer_InvalidTypeName(obj));
					}
				}
				else
				{
					base.JsonReader.SkipValue();
				}
			}
			base.JsonReader.ReadEndObject();
			return text;
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x0002FCB8 File Offset: 0x0002DEB8
		internal object ReadPrimitiveValue(IEdmPrimitiveTypeReference expectedValueTypeReference, bool validateNullValue, string propertyName)
		{
			object obj;
			if (expectedValueTypeReference != null && expectedValueTypeReference.IsSpatial())
			{
				obj = ODataJsonReaderCoreUtils.ReadSpatialValue(base.JsonReader, false, base.VerboseJsonInputContext, expectedValueTypeReference, validateNullValue, this.recursionDepth, propertyName);
			}
			else
			{
				obj = base.JsonReader.ReadPrimitiveValue();
				if (expectedValueTypeReference != null && !base.MessageReaderSettings.DisablePrimitiveTypeConversion)
				{
					obj = ODataVerboseJsonReaderUtils.ConvertValue(obj, expectedValueTypeReference, base.MessageReaderSettings, base.Version, validateNullValue, propertyName);
				}
			}
			return obj;
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0002FD24 File Offset: 0x0002DF24
		private ODataCollectionValue ReadCollectionValueImplementation(IEdmCollectionTypeReference collectionValueTypeReference, string payloadTypeName, SerializationTypeNameAnnotation serializationTypeNameAnnotation)
		{
			ODataVersionChecker.CheckCollectionValue(base.Version);
			this.IncreaseRecursionDepth();
			base.JsonReader.ReadStartObject();
			ODataCollectionValue odataCollectionValue = new ODataCollectionValue();
			odataCollectionValue.TypeName = ((collectionValueTypeReference != null) ? collectionValueTypeReference.ODataFullName() : payloadTypeName);
			if (serializationTypeNameAnnotation != null)
			{
				odataCollectionValue.SetAnnotation<SerializationTypeNameAnnotation>(serializationTypeNameAnnotation);
			}
			List<object> list = null;
			bool flag = false;
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text = base.JsonReader.ReadPropertyName();
				if (string.CompareOrdinal("__metadata", text) == 0)
				{
					if (flag)
					{
						throw new ODataException(Strings.ODataJsonPropertyAndValueDeserializer_MultiplePropertiesInCollectionWrapper("__metadata"));
					}
					flag = true;
					base.JsonReader.SkipValue();
				}
				else if (string.CompareOrdinal("results", text) == 0)
				{
					if (list != null)
					{
						throw new ODataException(Strings.ODataJsonPropertyAndValueDeserializer_MultiplePropertiesInCollectionWrapper("results"));
					}
					list = new List<object>();
					base.JsonReader.ReadStartArray();
					DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
					IEdmTypeReference edmTypeReference = null;
					if (collectionValueTypeReference != null)
					{
						edmTypeReference = collectionValueTypeReference.CollectionDefinition().ElementType;
					}
					CollectionWithoutExpectedTypeValidator collectionWithoutExpectedTypeValidator = null;
					while (base.JsonReader.NodeType != JsonNodeType.EndArray)
					{
						object obj = this.ReadNonEntityValueImplementation(edmTypeReference, duplicatePropertyNamesChecker, collectionWithoutExpectedTypeValidator, true, null);
						ValidationUtils.ValidateCollectionItem(obj, false);
						list.Add(obj);
					}
					base.JsonReader.ReadEndArray();
				}
				else
				{
					base.JsonReader.SkipValue();
				}
			}
			base.JsonReader.ReadEndObject();
			if (list == null)
			{
				throw new ODataException(Strings.ODataJsonPropertyAndValueDeserializer_CollectionWithoutResults);
			}
			odataCollectionValue.Items = new ReadOnlyEnumerable(list);
			this.DecreaseRecursionDepth();
			return odataCollectionValue;
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x0002FE8C File Offset: 0x0002E08C
		private ODataComplexValue ReadComplexValueImplementation(IEdmComplexTypeReference complexValueTypeReference, string payloadTypeName, SerializationTypeNameAnnotation serializationTypeNameAnnotation, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			this.IncreaseRecursionDepth();
			base.JsonReader.ReadStartObject();
			ODataComplexValue odataComplexValue = new ODataComplexValue();
			odataComplexValue.TypeName = ((complexValueTypeReference != null) ? complexValueTypeReference.ODataFullName() : payloadTypeName);
			if (serializationTypeNameAnnotation != null)
			{
				odataComplexValue.SetAnnotation<SerializationTypeNameAnnotation>(serializationTypeNameAnnotation);
			}
			if (duplicatePropertyNamesChecker == null)
			{
				duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
			}
			else
			{
				duplicatePropertyNamesChecker.Clear();
			}
			List<ODataProperty> list = new List<ODataProperty>();
			bool flag = false;
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				string text = base.JsonReader.ReadPropertyName();
				if (string.CompareOrdinal("__metadata", text) == 0)
				{
					if (flag)
					{
						throw new ODataException(Strings.ODataJsonPropertyAndValueDeserializer_MultipleMetadataPropertiesInComplexValue);
					}
					flag = true;
					base.JsonReader.SkipValue();
				}
				else if (!ValidationUtils.IsValidPropertyName(text))
				{
					base.JsonReader.SkipValue();
				}
				else
				{
					ODataProperty odataProperty = new ODataProperty();
					odataProperty.Name = text;
					IEdmProperty edmProperty = null;
					bool flag2 = false;
					if (complexValueTypeReference != null)
					{
						edmProperty = ReaderValidationUtils.ValidateValuePropertyDefined(text, complexValueTypeReference.ComplexDefinition(), base.MessageReaderSettings, out flag2);
					}
					if (flag2)
					{
						base.JsonReader.SkipValue();
					}
					else
					{
						ODataNullValueBehaviorKind odataNullValueBehaviorKind = ((base.ReadingResponse || edmProperty == null) ? ODataNullValueBehaviorKind.Default : base.Model.NullValueReadBehaviorKind(edmProperty));
						object obj = this.ReadNonEntityValueImplementation((edmProperty == null) ? null : edmProperty.Type, null, null, odataNullValueBehaviorKind == ODataNullValueBehaviorKind.Default, text);
						if (odataNullValueBehaviorKind != ODataNullValueBehaviorKind.IgnoreValue || obj != null)
						{
							duplicatePropertyNamesChecker.CheckForDuplicatePropertyNames(odataProperty);
							odataProperty.Value = obj;
							list.Add(odataProperty);
						}
					}
				}
			}
			base.JsonReader.ReadEndObject();
			odataComplexValue.Properties = new ReadOnlyEnumerable<ODataProperty>(list);
			this.DecreaseRecursionDepth();
			return odataComplexValue;
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x0003000C File Offset: 0x0002E20C
		private object ReadNonEntityValueImplementation(IEdmTypeReference expectedTypeReference, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, CollectionWithoutExpectedTypeValidator collectionValidator, bool validateNullValue, string propertyName)
		{
			JsonNodeType nodeType = base.JsonReader.NodeType;
			if (nodeType == JsonNodeType.StartArray)
			{
				throw new ODataException(Strings.ODataJsonPropertyAndValueDeserializer_CannotReadPropertyValue(nodeType));
			}
			object obj;
			if (ODataJsonReaderCoreUtils.TryReadNullValue(base.JsonReader, base.VerboseJsonInputContext, expectedTypeReference, validateNullValue, propertyName))
			{
				obj = null;
			}
			else
			{
				string text = this.FindTypeNameInPayload();
				EdmTypeKind edmTypeKind;
				SerializationTypeNameAnnotation serializationTypeNameAnnotation;
				IEdmTypeReference edmTypeReference = ReaderValidationUtils.ResolvePayloadTypeNameAndComputeTargetType(EdmTypeKind.None, null, expectedTypeReference, text, base.Model, base.MessageReaderSettings, base.Version, new Func<EdmTypeKind>(this.GetNonEntityValueKind), out edmTypeKind, out serializationTypeNameAnnotation);
				switch (edmTypeKind)
				{
				case EdmTypeKind.Primitive:
				{
					IEdmPrimitiveTypeReference edmPrimitiveTypeReference = ((edmTypeReference == null) ? null : edmTypeReference.AsPrimitive());
					if (text != null && !edmPrimitiveTypeReference.IsSpatial())
					{
						throw new ODataException(Strings.ODataJsonPropertyAndValueDeserializer_InvalidPrimitiveTypeName(text));
					}
					obj = this.ReadPrimitiveValue(edmPrimitiveTypeReference, validateNullValue, propertyName);
					goto IL_110;
				}
				case EdmTypeKind.Complex:
					obj = this.ReadComplexValueImplementation((edmTypeReference == null) ? null : edmTypeReference.AsComplex(), text, serializationTypeNameAnnotation, duplicatePropertyNamesChecker);
					goto IL_110;
				case EdmTypeKind.Collection:
				{
					IEdmCollectionTypeReference edmCollectionTypeReference = ValidationUtils.ValidateCollectionType(edmTypeReference);
					obj = this.ReadCollectionValueImplementation(edmCollectionTypeReference, text, serializationTypeNameAnnotation);
					goto IL_110;
				}
				}
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataVerboseJsonPropertyAndValueDeserializer_ReadPropertyValue));
				IL_110:
				if (collectionValidator != null)
				{
					string payloadTypeName = ODataVerboseJsonReaderUtils.GetPayloadTypeName(obj);
					collectionValidator.ValidateCollectionItem(payloadTypeName, edmTypeKind);
				}
			}
			return obj;
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x00030140 File Offset: 0x0002E340
		private EdmTypeKind GetNonEntityValueKind()
		{
			JsonNodeType nodeType = base.JsonReader.NodeType;
			if (nodeType == JsonNodeType.PrimitiveValue)
			{
				return EdmTypeKind.Primitive;
			}
			base.JsonReader.StartBuffering();
			EdmTypeKind edmTypeKind;
			try
			{
				base.JsonReader.ReadNext();
				while (base.JsonReader.NodeType == JsonNodeType.Property)
				{
					string text = base.JsonReader.ReadPropertyName();
					if (string.Equals("results", text, StringComparison.Ordinal))
					{
						if (base.JsonReader.NodeType == JsonNodeType.StartArray && base.Version >= ODataVersion.V3)
						{
							return EdmTypeKind.Collection;
						}
						return EdmTypeKind.Complex;
					}
					else
					{
						base.JsonReader.SkipValue();
					}
				}
				edmTypeKind = EdmTypeKind.Complex;
			}
			finally
			{
				base.JsonReader.StopBuffering();
			}
			return edmTypeKind;
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x000301EC File Offset: 0x0002E3EC
		private bool ShouldReadTopLevelPropertyValueWithoutPropertyWrapper(IEdmTypeReference expectedPropertyTypeReference)
		{
			if (base.UseServerFormatBehavior && expectedPropertyTypeReference == null)
			{
				base.JsonReader.StartBuffering();
				try
				{
					base.JsonReader.ReadStartObject();
					if (base.JsonReader.NodeType == JsonNodeType.EndObject)
					{
						return false;
					}
					string text = base.JsonReader.ReadPropertyName();
					base.JsonReader.SkipValue();
					if (base.JsonReader.NodeType != JsonNodeType.EndObject)
					{
						return true;
					}
					if (string.CompareOrdinal(text, "__metadata") == 0)
					{
						return true;
					}
				}
				finally
				{
					base.JsonReader.StopBuffering();
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x00030288 File Offset: 0x0002E488
		private void IncreaseRecursionDepth()
		{
			ValidationUtils.IncreaseAndValidateRecursionDepth(ref this.recursionDepth, base.MessageReaderSettings.MessageQuotas.MaxNestingDepth);
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x000302A5 File Offset: 0x0002E4A5
		private void DecreaseRecursionDepth()
		{
			this.recursionDepth--;
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x000302B5 File Offset: 0x0002E4B5
		[Conditional("DEBUG")]
		private void AssertRecursionDepthIsZero()
		{
		}

		// Token: 0x04000495 RID: 1173
		private int recursionDepth;
	}
}
