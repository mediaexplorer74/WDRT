using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Json;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000165 RID: 357
	internal class ODataJsonLightPropertyAndValueDeserializer : ODataJsonLightDeserializer
	{
		// Token: 0x060009CF RID: 2511 RVA: 0x0001F18D File Offset: 0x0001D38D
		internal ODataJsonLightPropertyAndValueDeserializer(ODataJsonLightInputContext jsonLightInputContext)
			: base(jsonLightInputContext)
		{
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0001F198 File Offset: 0x0001D398
		internal ODataProperty ReadTopLevelProperty(IEdmTypeReference expectedPropertyTypeReference)
		{
			DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
			base.ReadPayloadStart(ODataPayloadKind.Property, duplicatePropertyNamesChecker, false, false);
			ODataProperty odataProperty = this.ReadTopLevelPropertyImplementation(expectedPropertyTypeReference, duplicatePropertyNamesChecker);
			base.ReadPayloadEnd(false);
			return odataProperty;
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0001F204 File Offset: 0x0001D404
		internal Task<ODataProperty> ReadTopLevelPropertyAsync(IEdmTypeReference expectedPropertyTypeReference)
		{
			DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
			return base.ReadPayloadStartAsync(ODataPayloadKind.Property, duplicatePropertyNamesChecker, false, false).FollowOnSuccessWith(delegate(Task t)
			{
				ODataProperty odataProperty = this.ReadTopLevelPropertyImplementation(expectedPropertyTypeReference, duplicatePropertyNamesChecker);
				this.ReadPayloadEnd(false);
				return odataProperty;
			});
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0001F254 File Offset: 0x0001D454
		internal object ReadNonEntityValue(string payloadTypeName, IEdmTypeReference expectedValueTypeReference, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, CollectionWithoutExpectedTypeValidator collectionValidator, bool validateNullValue, bool isTopLevelPropertyValue, bool insideComplexValue, string propertyName)
		{
			return this.ReadNonEntityValue(payloadTypeName, expectedValueTypeReference, duplicatePropertyNamesChecker, collectionValidator, validateNullValue, isTopLevelPropertyValue, insideComplexValue, propertyName, false);
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0001F278 File Offset: 0x0001D478
		internal object ReadNonEntityValue(string payloadTypeName, IEdmTypeReference expectedValueTypeReference, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, CollectionWithoutExpectedTypeValidator collectionValidator, bool validateNullValue, bool isTopLevelPropertyValue, bool insideComplexValue, string propertyName, bool readRawValueEvenIfNoTypeFound)
		{
			return this.ReadNonEntityValueImplementation(payloadTypeName, expectedValueTypeReference, duplicatePropertyNamesChecker, collectionValidator, validateNullValue, isTopLevelPropertyValue, insideComplexValue, propertyName, readRawValueEvenIfNoTypeFound);
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0001F29C File Offset: 0x0001D49C
		internal static string ValidateDataPropertyTypeNameAnnotation(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, string propertyName)
		{
			Dictionary<string, object> odataPropertyAnnotations = duplicatePropertyNamesChecker.GetODataPropertyAnnotations(propertyName);
			string text = null;
			if (odataPropertyAnnotations != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in odataPropertyAnnotations)
				{
					if (string.CompareOrdinal(keyValuePair.Key, "odata.type") != 0)
					{
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedDataPropertyAnnotation(propertyName, keyValuePair.Key));
					}
					text = (string)keyValuePair.Value;
				}
			}
			return text;
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0001F324 File Offset: 0x0001D524
		protected bool TryReadODataTypeAnnotationValue(string annotationName, out string value)
		{
			if (string.CompareOrdinal(annotationName, "odata.type") == 0)
			{
				value = this.ReadODataTypeAnnotationValue();
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0001F344 File Offset: 0x0001D544
		protected string ReadODataTypeAnnotationValue()
		{
			string text = base.JsonReader.ReadStringValue();
			if (text == null)
			{
				throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_InvalidTypeName(text));
			}
			return text;
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0001F370 File Offset: 0x0001D570
		protected object ReadTypePropertyAnnotationValue(string propertyAnnotationName)
		{
			string text;
			if (this.TryReadODataTypeAnnotationValue(propertyAnnotationName, out text))
			{
				return text;
			}
			throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedAnnotationProperties(propertyAnnotationName));
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0001F398 File Offset: 0x0001D598
		protected static bool IsKnownValueTypeForNonOpenEntityOrComplex(JsonNodeType jsonReaderNodeType, object jsonReaderValue, string payloadTypeName, IEdmTypeReference payloadTypeReference)
		{
			if (string.IsNullOrEmpty(payloadTypeName))
			{
				bool flag = jsonReaderNodeType == JsonNodeType.PrimitiveValue && jsonReaderValue == null;
				bool flag2 = jsonReaderNodeType == JsonNodeType.PrimitiveValue && jsonReaderValue is bool;
				return flag || flag2;
			}
			return payloadTypeReference != null;
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0001F3D8 File Offset: 0x0001D5D8
		protected static bool IsKnownValueTypeForOpenEntityOrComplex(JsonNodeType jsonReaderNodeType, object jsonReaderValue, string payloadTypeName, IEdmTypeReference payloadTypeReference)
		{
			if (string.IsNullOrEmpty(payloadTypeName))
			{
				return jsonReaderNodeType == JsonNodeType.PrimitiveValue;
			}
			return payloadTypeReference != null;
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0001F3F0 File Offset: 0x0001D5F0
		protected string TryReadOrPeekPayloadType(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, string propertyName, bool insideComplexValue)
		{
			string text = ODataJsonLightPropertyAndValueDeserializer.ValidateDataPropertyTypeNameAnnotation(duplicatePropertyNamesChecker, propertyName);
			bool flag = base.JsonReader.NodeType == JsonNodeType.StartObject;
			if (string.IsNullOrEmpty(text) && flag)
			{
				try
				{
					base.JsonReader.StartBuffering();
					duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
					string text2;
					bool flag2 = this.TryReadPayloadTypeFromObject(duplicatePropertyNamesChecker, insideComplexValue, out text2);
					if (flag2)
					{
						text = text2;
					}
				}
				finally
				{
					base.JsonReader.StopBuffering();
				}
			}
			return text;
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0001F464 File Offset: 0x0001D664
		protected object InnerReadNonOpenUndeclaredProperty(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, string propertyName, bool isTopLevelPropertyValue)
		{
			bool flag = false;
			string text = ODataJsonLightPropertyAndValueDeserializer.ValidateDataPropertyTypeNameAnnotation(duplicatePropertyNamesChecker, propertyName);
			string text2 = this.TryReadOrPeekPayloadType(duplicatePropertyNamesChecker, propertyName, flag);
			EdmTypeKind edmTypeKind;
			IEdmType edmType = ReaderValidationUtils.ResolvePayloadTypeName(base.Model, null, text2, EdmTypeKind.Complex, base.MessageReaderSettings.ReaderBehavior, base.Version, out edmTypeKind);
			IEdmTypeReference edmTypeReference = null;
			if (!string.IsNullOrEmpty(text2) && edmType != null)
			{
				EdmTypeKind edmTypeKind2;
				SerializationTypeNameAnnotation serializationTypeNameAnnotation;
				edmTypeReference = ReaderValidationUtils.ResolvePayloadTypeNameAndComputeTargetType(EdmTypeKind.None, null, null, text2, base.Model, base.MessageReaderSettings, base.Version, new Func<EdmTypeKind>(this.GetNonEntityValueKind), out edmTypeKind2, out serializationTypeNameAnnotation);
			}
			bool flag2 = ODataJsonLightPropertyAndValueDeserializer.IsKnownValueTypeForNonOpenEntityOrComplex(base.JsonReader.NodeType, base.JsonReader.Value, text2, edmTypeReference);
			object obj;
			if (flag2)
			{
				bool flag3 = true;
				if (ODataJsonReaderCoreUtils.TryReadNullValue(base.JsonReader, base.JsonLightInputContext, edmTypeReference, flag3, propertyName))
				{
					if (isTopLevelPropertyValue)
					{
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_TopLevelPropertyWithPrimitiveNullValue("odata.null", "true"));
					}
					obj = null;
				}
				else
				{
					ODataJsonLightPropertyAndValueDeserializer.ValidateDataPropertyTypeNameAnnotation(duplicatePropertyNamesChecker, propertyName);
					obj = this.ReadNonEntityValueImplementation(text, edmTypeReference, null, null, false, isTopLevelPropertyValue, flag, propertyName);
				}
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				base.JsonReader.SkipValue(stringBuilder);
				ODataUntypedValue odataUntypedValue = new ODataUntypedValue
				{
					RawJson = stringBuilder.ToString()
				};
				obj = odataUntypedValue;
			}
			return obj;
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0001F594 File Offset: 0x0001D794
		protected static bool TryAttachRawAnnotationSetToPropertyValue(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, ODataProperty property)
		{
			if (duplicatePropertyNamesChecker != null)
			{
				ODataJsonLightRawAnnotationSet propertyRawAnnotationSet = duplicatePropertyNamesChecker.AnnotationCollector.GetPropertyRawAnnotationSet(property.Name);
				if (propertyRawAnnotationSet != null)
				{
					ODataUntypedValue odataUntypedValue = property.Value as ODataUntypedValue;
					ODataAnnotatable odataAnnotatable = odataUntypedValue ?? property.ODataValue;
					if (odataAnnotatable != null)
					{
						odataAnnotatable.SetAnnotation<ODataJsonLightRawAnnotationSet>(propertyRawAnnotationSet);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0001F5E0 File Offset: 0x0001D7E0
		protected EdmTypeKind GetNonEntityValueKind()
		{
			JsonNodeType nodeType = base.JsonReader.NodeType;
			if (nodeType == JsonNodeType.StartArray)
			{
				return EdmTypeKind.Collection;
			}
			if (nodeType == JsonNodeType.PrimitiveValue)
			{
				return EdmTypeKind.Primitive;
			}
			return EdmTypeKind.Complex;
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0001F608 File Offset: 0x0001D808
		private bool TryReadODataTypeAnnotation(out string payloadTypeName)
		{
			payloadTypeName = null;
			bool flag = false;
			string propertyName = base.JsonReader.GetPropertyName();
			if (string.CompareOrdinal(propertyName, "odata.type") == 0)
			{
				base.JsonReader.ReadNext();
				payloadTypeName = this.ReadODataTypeAnnotationValue();
				flag = true;
			}
			return flag;
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0001F7A4 File Offset: 0x0001D9A4
		private ODataProperty ReadTopLevelPropertyImplementation(IEdmTypeReference expectedPropertyTypeReference, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			expectedPropertyTypeReference = this.UpdateExpectedTypeBasedOnMetadataUri(expectedPropertyTypeReference);
			object propertyValue = ODataJsonLightPropertyAndValueDeserializer.missingPropertyValue;
			if (this.IsTopLevelNullValue())
			{
				ReaderValidationUtils.ValidateNullValue(base.Model, expectedPropertyTypeReference, base.MessageReaderSettings, true, base.Version, null);
				this.ValidateNoPropertyInNullPayload(duplicatePropertyNamesChecker);
				propertyValue = null;
			}
			else
			{
				string payloadTypeName = null;
				if (this.ReadingComplexProperty(duplicatePropertyNamesChecker, expectedPropertyTypeReference, out payloadTypeName))
				{
					propertyValue = this.ReadNonEntityValue(payloadTypeName, expectedPropertyTypeReference, duplicatePropertyNamesChecker, null, true, true, true, null);
				}
				else
				{
					bool isReordering = base.JsonReader is ReorderingJsonReader;
					Func<string, object> func = delegate(string annotationName)
					{
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedODataPropertyAnnotation(annotationName));
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
								if (string.CompareOrdinal("value", propertyName) == 0)
								{
									propertyValue = this.ReadNonEntityValue(payloadTypeName, expectedPropertyTypeReference, null, null, true, true, false, propertyName);
									return;
								}
								throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_InvalidTopLevelPropertyName(propertyName, "value"));
							case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue:
								throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_TopLevelPropertyAnnotationWithoutProperty(propertyName));
							case ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation:
								if (string.CompareOrdinal("odata.type", propertyName) != 0)
								{
									throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedAnnotationProperties(propertyName));
								}
								if (isReordering)
								{
									this.JsonReader.SkipValue();
									return;
								}
								if (!object.ReferenceEquals(ODataJsonLightPropertyAndValueDeserializer.missingPropertyValue, propertyValue))
								{
									throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_TypePropertyAfterValueProperty("odata.type", "value"));
								}
								payloadTypeName = this.ReadODataTypeAnnotationValue();
								return;
							case ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation:
								this.JsonReader.SkipValue();
								return;
							case ODataJsonLightDeserializer.PropertyParsingResult.MetadataReferenceProperty:
								throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedMetadataReferenceProperty(propertyName));
							default:
								return;
							}
						});
					}
					if (object.ReferenceEquals(ODataJsonLightPropertyAndValueDeserializer.missingPropertyValue, propertyValue))
					{
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_InvalidTopLevelPropertyPayload);
					}
				}
			}
			ODataProperty odataProperty = new ODataProperty
			{
				Name = null,
				Value = propertyValue
			};
			base.JsonReader.Read();
			return odataProperty;
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0001F92C File Offset: 0x0001DB2C
		private IEdmTypeReference UpdateExpectedTypeBasedOnMetadataUri(IEdmTypeReference expectedPropertyTypeReference)
		{
			if (base.MetadataUriParseResult == null || base.MetadataUriParseResult.EdmType == null)
			{
				return expectedPropertyTypeReference;
			}
			IEdmType edmType = base.MetadataUriParseResult.EdmType;
			if (expectedPropertyTypeReference != null && !expectedPropertyTypeReference.Definition.IsAssignableFrom(edmType))
			{
				throw new ODataException(Strings.ReaderValidationUtils_TypeInMetadataUriDoesNotMatchExpectedType(UriUtilsCommon.UriToString(base.MetadataUriParseResult.MetadataUri), edmType.ODataFullName(), expectedPropertyTypeReference.ODataFullName()));
			}
			bool flag = true;
			if (expectedPropertyTypeReference != null)
			{
				flag = expectedPropertyTypeReference.IsNullable;
			}
			return edmType.ToTypeReference(flag);
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0001F9A8 File Offset: 0x0001DBA8
		private ODataCollectionValue ReadCollectionValue(IEdmCollectionTypeReference collectionValueTypeReference, string payloadTypeName, SerializationTypeNameAnnotation serializationTypeNameAnnotation)
		{
			ODataVersionChecker.CheckCollectionValue(base.Version);
			this.IncreaseRecursionDepth();
			base.JsonReader.ReadStartArray();
			ODataCollectionValue odataCollectionValue = new ODataCollectionValue();
			odataCollectionValue.TypeName = ((collectionValueTypeReference != null) ? collectionValueTypeReference.ODataFullName() : payloadTypeName);
			if (serializationTypeNameAnnotation != null)
			{
				odataCollectionValue.SetAnnotation<SerializationTypeNameAnnotation>(serializationTypeNameAnnotation);
			}
			if (collectionValueTypeReference != null)
			{
				odataCollectionValue.SetAnnotation<ODataTypeAnnotation>(new ODataTypeAnnotation(collectionValueTypeReference));
			}
			List<object> list = new List<object>();
			DuplicatePropertyNamesChecker duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
			IEdmTypeReference edmTypeReference = null;
			if (collectionValueTypeReference != null)
			{
				edmTypeReference = collectionValueTypeReference.CollectionDefinition().ElementType;
			}
			CollectionWithoutExpectedTypeValidator collectionWithoutExpectedTypeValidator = null;
			while (base.JsonReader.NodeType != JsonNodeType.EndArray)
			{
				object obj = this.ReadNonEntityValueImplementation(null, edmTypeReference, duplicatePropertyNamesChecker, collectionWithoutExpectedTypeValidator, true, false, false, null);
				ValidationUtils.ValidateCollectionItem(obj, false);
				list.Add(obj);
			}
			base.JsonReader.ReadEndArray();
			odataCollectionValue.Items = new ReadOnlyEnumerable(list);
			this.DecreaseRecursionDepth();
			return odataCollectionValue;
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0001FA74 File Offset: 0x0001DC74
		private object ReadPrimitiveValue(bool insideJsonObjectValue, IEdmPrimitiveTypeReference expectedValueTypeReference, bool validateNullValue, string propertyName)
		{
			object obj;
			if (expectedValueTypeReference != null && expectedValueTypeReference.IsSpatial())
			{
				obj = ODataJsonReaderCoreUtils.ReadSpatialValue(base.JsonReader, insideJsonObjectValue, base.JsonLightInputContext, expectedValueTypeReference, validateNullValue, this.recursionDepth, propertyName);
			}
			else
			{
				if (insideJsonObjectValue)
				{
					throw new ODataException(Strings.JsonReaderExtensions_UnexpectedNodeDetected(JsonNodeType.PrimitiveValue, JsonNodeType.StartObject));
				}
				obj = base.JsonReader.ReadPrimitiveValue();
				if (expectedValueTypeReference != null)
				{
					obj = ODataJsonLightReaderUtils.ConvertValue(obj, expectedValueTypeReference, base.MessageReaderSettings, base.Version, validateNullValue, propertyName);
				}
			}
			return obj;
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0001FD08 File Offset: 0x0001DF08
		private ODataComplexValue ReadComplexValue(IEdmComplexTypeReference complexValueTypeReference, string payloadTypeName, SerializationTypeNameAnnotation serializationTypeNameAnnotation, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			this.IncreaseRecursionDepth();
			ODataComplexValue odataComplexValue = new ODataComplexValue();
			odataComplexValue.TypeName = ((complexValueTypeReference != null) ? complexValueTypeReference.ODataFullName() : payloadTypeName);
			if (serializationTypeNameAnnotation != null)
			{
				odataComplexValue.SetAnnotation<SerializationTypeNameAnnotation>(serializationTypeNameAnnotation);
			}
			if (complexValueTypeReference != null)
			{
				odataComplexValue.SetAnnotation<ODataTypeAnnotation>(new ODataTypeAnnotation(complexValueTypeReference));
			}
			List<ODataProperty> properties = new List<ODataProperty>();
			while (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				base.ProcessProperty(duplicatePropertyNamesChecker, new Func<string, object>(this.ReadTypePropertyAnnotationValue), delegate(ODataJsonLightDeserializer.PropertyParsingResult propertyParsingResult, string propertyName)
				{
					switch (propertyParsingResult)
					{
					case ODataJsonLightDeserializer.PropertyParsingResult.EndOfObject:
						break;
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithValue:
					{
						ODataProperty odataProperty = new ODataProperty();
						odataProperty.Name = propertyName;
						IEdmProperty edmProperty = null;
						bool flag = false;
						if (complexValueTypeReference != null)
						{
							edmProperty = ReaderValidationUtils.ValidateValuePropertyDefined(propertyName, complexValueTypeReference.ComplexDefinition(), this.MessageReaderSettings, out flag);
						}
						if (flag)
						{
							this.JsonReader.SkipValue();
							return;
						}
						ODataNullValueBehaviorKind odataNullValueBehaviorKind = ((this.ReadingResponse || edmProperty == null) ? ODataNullValueBehaviorKind.Default : this.Model.NullValueReadBehaviorKind(edmProperty));
						IEdmProperty edmProperty2 = ((complexValueTypeReference == null) ? null : complexValueTypeReference.FindProperty(propertyName));
						object obj = null;
						bool flag2 = true;
						if (edmProperty2 == null)
						{
							if (!this.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.IgnoreUndeclaredValueProperty) && !this.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.SupportUndeclaredValueProperty))
							{
								IEdmStructuredType edmStructuredType = complexValueTypeReference.Definition as IEdmStructuredType;
								throw new ODataException(Strings.ValidationUtils_PropertyDoesNotExistOnType(propertyName, (edmStructuredType != null) ? edmStructuredType.ODataFullName() : null));
							}
							if (this.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.SupportUndeclaredValueProperty))
							{
								bool flag3 = false;
								obj = this.InnerReadNonOpenUndeclaredProperty(duplicatePropertyNamesChecker, propertyName, flag3);
							}
							else
							{
								this.JsonReader.SkipValue();
								flag2 = false;
							}
						}
						else
						{
							obj = this.ReadNonEntityValueImplementation(ODataJsonLightPropertyAndValueDeserializer.ValidateDataPropertyTypeNameAnnotation(duplicatePropertyNamesChecker, propertyName), (edmProperty == null) ? null : edmProperty.Type, null, null, odataNullValueBehaviorKind == ODataNullValueBehaviorKind.Default, false, false, propertyName);
						}
						if (flag2 && (odataNullValueBehaviorKind != ODataNullValueBehaviorKind.IgnoreValue || obj != null))
						{
							duplicatePropertyNamesChecker.CheckForDuplicatePropertyNames(odataProperty);
							odataProperty.Value = obj;
							ODataJsonLightPropertyAndValueDeserializer.TryAttachRawAnnotationSetToPropertyValue(duplicatePropertyNamesChecker, odataProperty);
							properties.Add(odataProperty);
							return;
						}
						break;
					}
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue:
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_ComplexValuePropertyAnnotationWithoutProperty(propertyName));
					case ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation:
						if (string.CompareOrdinal("odata.type", propertyName) == 0)
						{
							throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_ComplexTypeAnnotationNotFirst);
						}
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedAnnotationProperties(propertyName));
					case ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation:
						this.JsonReader.SkipValue();
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.MetadataReferenceProperty:
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedMetadataReferenceProperty(propertyName));
					default:
						return;
					}
				});
			}
			base.JsonReader.ReadEndObject();
			odataComplexValue.Properties = new ReadOnlyEnumerable<ODataProperty>(properties);
			this.DecreaseRecursionDepth();
			return odataComplexValue;
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0001FDE8 File Offset: 0x0001DFE8
		private object ReadNonEntityValueImplementation(string payloadTypeName, IEdmTypeReference expectedTypeReference, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, CollectionWithoutExpectedTypeValidator collectionValidator, bool validateNullValue, bool isTopLevelPropertyValue, bool insideComplexValue, string propertyName)
		{
			return this.ReadNonEntityValueImplementation(payloadTypeName, expectedTypeReference, duplicatePropertyNamesChecker, collectionValidator, validateNullValue, isTopLevelPropertyValue, insideComplexValue, propertyName, false);
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0001FE0C File Offset: 0x0001E00C
		private object ReadNonEntityValueImplementation(string payloadTypeName, IEdmTypeReference expectedTypeReference, DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, CollectionWithoutExpectedTypeValidator collectionValidator, bool validateNullValue, bool isTopLevelPropertyValue, bool insideComplexValue, string propertyName, bool readRawValueEvenIfNoTypeFound)
		{
			bool flag = base.JsonReader.NodeType == JsonNodeType.StartObject;
			bool flag2;
			if (duplicatePropertyNamesChecker != null && propertyName != null)
			{
				string text = ODataJsonLightPropertyAndValueDeserializer.ValidateDataPropertyTypeNameAnnotation(duplicatePropertyNamesChecker, propertyName);
				flag2 = !insideComplexValue && text != null;
			}
			else
			{
				flag2 = !insideComplexValue && payloadTypeName != null;
			}
			bool flag3 = false;
			if (flag || insideComplexValue)
			{
				if (duplicatePropertyNamesChecker == null)
				{
					duplicatePropertyNamesChecker = base.CreateDuplicatePropertyNamesChecker();
				}
				else
				{
					duplicatePropertyNamesChecker.Clear();
				}
				if (!insideComplexValue)
				{
					string text2;
					flag3 = this.TryReadPayloadTypeFromObject(duplicatePropertyNamesChecker, insideComplexValue, out text2);
					if (flag3)
					{
						payloadTypeName = text2;
					}
				}
			}
			if (string.IsNullOrEmpty(payloadTypeName) && expectedTypeReference == null && readRawValueEvenIfNoTypeFound)
			{
				ODataJsonLightGeneralDeserializer odataJsonLightGeneralDeserializer = new ODataJsonLightGeneralDeserializer(base.JsonLightInputContext);
				return odataJsonLightGeneralDeserializer.ReadValue();
			}
			EdmTypeKind edmTypeKind;
			SerializationTypeNameAnnotation serializationTypeNameAnnotation;
			IEdmTypeReference edmTypeReference = ReaderValidationUtils.ResolvePayloadTypeNameAndComputeTargetType(EdmTypeKind.None, null, expectedTypeReference, payloadTypeName, base.Model, base.MessageReaderSettings, base.Version, new Func<EdmTypeKind>(this.GetNonEntityValueKind), out edmTypeKind, out serializationTypeNameAnnotation);
			object obj;
			if (ODataJsonReaderCoreUtils.TryReadNullValue(base.JsonReader, base.JsonLightInputContext, edmTypeReference, validateNullValue, propertyName))
			{
				if (isTopLevelPropertyValue)
				{
					throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_TopLevelPropertyWithPrimitiveNullValue("odata.null", "true"));
				}
				obj = null;
			}
			else
			{
				if (!base.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.IgnoreUndeclaredValueProperty) && !base.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.SupportUndeclaredValueProperty) && edmTypeReference == null && edmTypeKind != EdmTypeKind.Primitive)
				{
					throw new ODataException(Strings.ReaderValidationUtils_ValueWithoutType);
				}
				switch (edmTypeKind)
				{
				case EdmTypeKind.Primitive:
				{
					IEdmPrimitiveTypeReference edmPrimitiveTypeReference = ((edmTypeReference == null) ? null : edmTypeReference.AsPrimitive());
					if (flag3)
					{
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_ODataTypeAnnotationInPrimitiveValue("odata.type"));
					}
					obj = this.ReadPrimitiveValue(flag, edmPrimitiveTypeReference, validateNullValue, propertyName);
					goto IL_25B;
				}
				case EdmTypeKind.Complex:
					if (!base.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.IgnoreUndeclaredValueProperty))
					{
						base.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.SupportUndeclaredValueProperty);
					}
					if (flag2)
					{
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_ComplexValueWithPropertyTypeAnnotation("odata.type"));
					}
					if (!flag && !insideComplexValue)
					{
						base.JsonReader.ReadStartObject();
					}
					if (base.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.IgnoreUndeclaredValueProperty) || base.MessageReaderSettings.ContainUndeclaredPropertyBehavior(ODataUndeclaredPropertyBehaviorKinds.SupportUndeclaredValueProperty))
					{
						obj = this.ReadComplexValue((edmTypeReference == null) ? null : edmTypeReference.AsComplex(), payloadTypeName, serializationTypeNameAnnotation, duplicatePropertyNamesChecker);
						goto IL_25B;
					}
					obj = this.ReadComplexValue(edmTypeReference.AsComplex(), payloadTypeName, serializationTypeNameAnnotation, duplicatePropertyNamesChecker);
					goto IL_25B;
				case EdmTypeKind.Collection:
				{
					IEdmCollectionTypeReference edmCollectionTypeReference = ValidationUtils.ValidateCollectionType(edmTypeReference);
					if (flag)
					{
						throw new ODataException(Strings.JsonReaderExtensions_UnexpectedNodeDetected(JsonNodeType.StartArray, JsonNodeType.StartObject));
					}
					obj = this.ReadCollectionValue(edmCollectionTypeReference, payloadTypeName, serializationTypeNameAnnotation);
					goto IL_25B;
				}
				}
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataJsonLightPropertyAndValueDeserializer_ReadPropertyValue));
				IL_25B:
				if (collectionValidator != null)
				{
					string payloadTypeName2 = ODataJsonLightReaderUtils.GetPayloadTypeName(obj);
					collectionValidator.ValidateCollectionItem(payloadTypeName2, edmTypeKind);
				}
			}
			return obj;
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x00020090 File Offset: 0x0001E290
		private bool TryReadPayloadTypeFromObject(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, bool insideComplexValue, out string payloadTypeName)
		{
			bool flag = false;
			payloadTypeName = null;
			if (!insideComplexValue)
			{
				base.JsonReader.ReadStartObject();
			}
			if (base.JsonReader.NodeType == JsonNodeType.Property)
			{
				flag = this.TryReadODataTypeAnnotation(out payloadTypeName);
				if (flag)
				{
					duplicatePropertyNamesChecker.MarkPropertyAsProcessed("odata.type");
				}
			}
			return flag;
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x000200D8 File Offset: 0x0001E2D8
		private bool ReadingComplexProperty(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker, IEdmTypeReference expectedPropertyTypeReference, out string payloadTypeName)
		{
			payloadTypeName = null;
			bool flag = false;
			if (expectedPropertyTypeReference != null)
			{
				flag = expectedPropertyTypeReference.IsComplex();
			}
			if (base.JsonReader.NodeType == JsonNodeType.Property && this.TryReadODataTypeAnnotation(out payloadTypeName))
			{
				duplicatePropertyNamesChecker.MarkPropertyAsProcessed("odata.type");
				IEdmType edmType = null;
				if (expectedPropertyTypeReference != null)
				{
					edmType = expectedPropertyTypeReference.Definition;
				}
				EdmTypeKind edmTypeKind = EdmTypeKind.None;
				IEdmType edmType2 = MetadataUtils.ResolveTypeNameForRead(base.Model, edmType, payloadTypeName, base.MessageReaderSettings.ReaderBehavior, base.MessageReaderSettings.MaxProtocolVersion, out edmTypeKind);
				if (edmType2 != null)
				{
					flag = edmType2.IsODataComplexTypeKind();
				}
			}
			return flag;
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x00020158 File Offset: 0x0001E358
		private bool IsTopLevelNullValue()
		{
			bool flag = base.MetadataUriParseResult != null && base.MetadataUriParseResult.IsNullProperty;
			bool flag2 = base.JsonReader.NodeType == JsonNodeType.Property && string.CompareOrdinal("odata.null", base.JsonReader.GetPropertyName()) == 0;
			if (flag2)
			{
				base.JsonReader.ReadNext();
				object obj = base.JsonReader.ReadPrimitiveValue();
				if (!(obj is bool) || !(bool)obj)
				{
					throw new ODataException(Strings.ODataJsonLightReaderUtils_InvalidValueForODataNullAnnotation("odata.null", "true"));
				}
			}
			return flag || flag2;
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x00020268 File Offset: 0x0001E468
		private void ValidateNoPropertyInNullPayload(DuplicatePropertyNamesChecker duplicatePropertyNamesChecker)
		{
			Func<string, object> func = delegate(string annotationName)
			{
				throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedODataPropertyAnnotation(annotationName));
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
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_NoPropertyAndAnnotationAllowedInNullPayload(propertyName));
					case ODataJsonLightDeserializer.PropertyParsingResult.PropertyWithoutValue:
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_TopLevelPropertyAnnotationWithoutProperty(propertyName));
					case ODataJsonLightDeserializer.PropertyParsingResult.ODataInstanceAnnotation:
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedAnnotationProperties(propertyName));
					case ODataJsonLightDeserializer.PropertyParsingResult.CustomInstanceAnnotation:
						base.JsonReader.SkipValue();
						return;
					case ODataJsonLightDeserializer.PropertyParsingResult.MetadataReferenceProperty:
						throw new ODataException(Strings.ODataJsonLightPropertyAndValueDeserializer_UnexpectedMetadataReferenceProperty(propertyName));
					default:
						return;
					}
				});
			}
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x000202BE File Offset: 0x0001E4BE
		private void IncreaseRecursionDepth()
		{
			ValidationUtils.IncreaseAndValidateRecursionDepth(ref this.recursionDepth, base.MessageReaderSettings.MessageQuotas.MaxNestingDepth);
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x000202DB File Offset: 0x0001E4DB
		private void DecreaseRecursionDepth()
		{
			this.recursionDepth--;
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x000202EB File Offset: 0x0001E4EB
		[Conditional("DEBUG")]
		private void AssertRecursionDepthIsZero()
		{
		}

		// Token: 0x0400039F RID: 927
		private static readonly object missingPropertyValue = new object();

		// Token: 0x040003A0 RID: 928
		private int recursionDepth;
	}
}
