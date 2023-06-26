using System;
using System.Globalization;
using System.IO;
using System.Spatial;
using System.Text;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Evaluation;
using Microsoft.Data.OData.JsonLight;
using Microsoft.Data.OData.Metadata;
using Microsoft.Data.OData.VerboseJson;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x020001E9 RID: 489
	internal static class ODataUriConversionUtils
	{
		// Token: 0x06000F14 RID: 3860 RVA: 0x00035D34 File Offset: 0x00033F34
		internal static string ConvertToUriPrimitiveLiteral(object value, ODataVersion version)
		{
			ExceptionUtils.CheckArgumentNotNull<object>(value, "value");
			if (value is ISpatial)
			{
				ODataVersionChecker.CheckSpatialValue(version);
			}
			return LiteralFormatter.ForConstantsWithoutEncoding.Format(value);
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x00035D5C File Offset: 0x00033F5C
		internal static object ConvertFromComplexOrCollectionValue(string value, ODataVersion version, IEdmModel model, IEdmTypeReference typeReference)
		{
			ODataMessageReaderSettings odataMessageReaderSettings = new ODataMessageReaderSettings();
			if (model.IsUserModel())
			{
				try
				{
					using (StringReader stringReader = new StringReader(value))
					{
						using (ODataJsonLightInputContext odataJsonLightInputContext = new ODataJsonLightInputContext(ODataFormat.Json, stringReader, new MediaType("application", "json"), odataMessageReaderSettings, version, false, true, model, null, null))
						{
							ODataJsonLightPropertyAndValueDeserializer odataJsonLightPropertyAndValueDeserializer = new ODataJsonLightPropertyAndValueDeserializer(odataJsonLightInputContext);
							odataJsonLightPropertyAndValueDeserializer.JsonReader.Read();
							object obj = odataJsonLightPropertyAndValueDeserializer.ReadNonEntityValue(null, typeReference, null, null, true, false, false, null);
							odataJsonLightPropertyAndValueDeserializer.ReadPayloadEnd(false);
							return obj;
						}
					}
				}
				catch (ODataException)
				{
				}
			}
			object obj3;
			using (StringReader stringReader2 = new StringReader(value))
			{
				using (ODataVerboseJsonInputContext odataVerboseJsonInputContext = new ODataVerboseJsonInputContext(ODataFormat.VerboseJson, stringReader2, odataMessageReaderSettings, version, false, true, model, null))
				{
					ODataVerboseJsonPropertyAndValueDeserializer odataVerboseJsonPropertyAndValueDeserializer = new ODataVerboseJsonPropertyAndValueDeserializer(odataVerboseJsonInputContext);
					odataVerboseJsonPropertyAndValueDeserializer.ReadPayloadStart(false);
					object obj2 = odataVerboseJsonPropertyAndValueDeserializer.ReadNonEntityValue(typeReference, null, null, true, null);
					odataVerboseJsonPropertyAndValueDeserializer.ReadPayloadEnd(false);
					obj3 = obj2;
				}
			}
			return obj3;
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x00035E8C File Offset: 0x0003408C
		internal static object VerifyAndCoerceUriPrimitiveLiteral(object primitiveValue, IEdmModel model, IEdmTypeReference expectedTypeReference, ODataVersion version)
		{
			ExceptionUtils.CheckArgumentNotNull<object>(primitiveValue, "primitiveValue");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ExceptionUtils.CheckArgumentNotNull<IEdmTypeReference>(expectedTypeReference, "expectedTypeReference");
			ODataUriNullValue odataUriNullValue = primitiveValue as ODataUriNullValue;
			if (odataUriNullValue != null)
			{
				if (!expectedTypeReference.IsNullable)
				{
					throw new ODataException(Strings.ODataUriUtils_ConvertFromUriLiteralNullOnNonNullableType(expectedTypeReference.ODataFullName()));
				}
				if (odataUriNullValue.TypeName == null)
				{
					odataUriNullValue.TypeName = expectedTypeReference.ODataFullName();
					return odataUriNullValue;
				}
				IEdmType edmType = TypeNameOracle.ResolveAndValidateTypeName(model, odataUriNullValue.TypeName, expectedTypeReference.Definition.TypeKind);
				if (edmType.IsSpatial())
				{
					ODataVersionChecker.CheckSpatialValue(version);
				}
				if (TypePromotionUtils.CanConvertTo(edmType.ToTypeReference(), expectedTypeReference))
				{
					odataUriNullValue.TypeName = expectedTypeReference.ODataFullName();
					return odataUriNullValue;
				}
				throw new ODataException(Strings.ODataUriUtils_ConvertFromUriLiteralNullTypeVerificationFailure(expectedTypeReference.ODataFullName(), odataUriNullValue.TypeName));
			}
			else
			{
				IEdmPrimitiveTypeReference edmPrimitiveTypeReference = expectedTypeReference.AsPrimitiveOrNull();
				if (edmPrimitiveTypeReference == null)
				{
					throw new ODataException(Strings.ODataUriUtils_ConvertFromUriLiteralTypeVerificationFailure(expectedTypeReference.ODataFullName(), primitiveValue));
				}
				object obj = ODataUriConversionUtils.CoerceNumericType(primitiveValue, edmPrimitiveTypeReference.PrimitiveDefinition());
				if (obj != null)
				{
					return obj;
				}
				Type type = primitiveValue.GetType();
				Type nonNullableType = TypeUtils.GetNonNullableType(EdmLibraryExtensions.GetPrimitiveClrType(edmPrimitiveTypeReference));
				if (nonNullableType.IsAssignableFrom(type))
				{
					return primitiveValue;
				}
				throw new ODataException(Strings.ODataUriUtils_ConvertFromUriLiteralTypeVerificationFailure(edmPrimitiveTypeReference.ODataFullName(), primitiveValue));
			}
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x00035FE8 File Offset: 0x000341E8
		internal static string ConvertToUriComplexLiteral(ODataComplexValue complexValue, IEdmModel model, ODataVersion version, ODataFormat format)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataComplexValue>(complexValue, "complexValue");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			StringBuilder stringBuilder = new StringBuilder();
			using (TextWriter textWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture))
			{
				ODataMessageWriterSettings odataMessageWriterSettings = new ODataMessageWriterSettings
				{
					Version = new ODataVersion?(version),
					Indent = false
				};
				if (format == ODataFormat.VerboseJson)
				{
					ODataUriConversionUtils.WriteJsonVerboseLiteral(model, odataMessageWriterSettings, textWriter, delegate(ODataVerboseJsonPropertyAndValueSerializer serializer)
					{
						serializer.WriteComplexValue(complexValue, null, true, serializer.CreateDuplicatePropertyNamesChecker(), null);
					});
				}
				else
				{
					if (format != ODataFormat.Json)
					{
						throw new ArgumentException(Strings.ODataUriUtils_ConvertToUriLiteralUnsupportedFormat(format.ToString()));
					}
					ODataUriConversionUtils.WriteJsonLightLiteral(model, odataMessageWriterSettings, textWriter, delegate(ODataJsonLightValueSerializer serializer)
					{
						serializer.WriteComplexValue(complexValue, null, false, true, serializer.CreateDuplicatePropertyNamesChecker());
					});
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x000360D4 File Offset: 0x000342D4
		internal static string ConvertToUriNullValue(ODataUriNullValue nullValue)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataUriNullValue>(nullValue, "nullValue");
			if (nullValue.TypeName != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("null");
				stringBuilder.Append("'");
				stringBuilder.Append(nullValue.TypeName);
				stringBuilder.Append("'");
				return stringBuilder.ToString();
			}
			return "null";
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x00036164 File Offset: 0x00034364
		internal static string ConvertToUriCollectionLiteral(ODataCollectionValue collectionValue, IEdmModel model, ODataVersion version, ODataFormat format)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataCollectionValue>(collectionValue, "collectionValue");
			ExceptionUtils.CheckArgumentNotNull<IEdmModel>(model, "model");
			ODataVersionChecker.CheckCollectionValue(version);
			StringBuilder stringBuilder = new StringBuilder();
			using (TextWriter textWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture))
			{
				ODataMessageWriterSettings odataMessageWriterSettings = new ODataMessageWriterSettings
				{
					Version = new ODataVersion?(version),
					Indent = false
				};
				if (format == ODataFormat.VerboseJson)
				{
					ODataUriConversionUtils.WriteJsonVerboseLiteral(model, odataMessageWriterSettings, textWriter, delegate(ODataVerboseJsonPropertyAndValueSerializer serializer)
					{
						serializer.WriteCollectionValue(collectionValue, null, false);
					});
				}
				else
				{
					if (format != ODataFormat.Json)
					{
						throw new ArgumentException(Strings.ODataUriUtils_ConvertToUriLiteralUnsupportedFormat(format.ToString()));
					}
					ODataUriConversionUtils.WriteJsonLightLiteral(model, odataMessageWriterSettings, textWriter, delegate(ODataJsonLightValueSerializer serializer)
					{
						serializer.WriteCollectionValue(collectionValue, null, false, true, false);
					});
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x00036258 File Offset: 0x00034458
		private static void WriteJsonVerboseLiteral(IEdmModel model, ODataMessageWriterSettings messageWriterSettings, TextWriter textWriter, Action<ODataVerboseJsonPropertyAndValueSerializer> writeValue)
		{
			using (ODataVerboseJsonOutputContext odataVerboseJsonOutputContext = new ODataVerboseJsonOutputContext(ODataFormat.VerboseJson, textWriter, messageWriterSettings, model))
			{
				ODataVerboseJsonPropertyAndValueSerializer odataVerboseJsonPropertyAndValueSerializer = new ODataVerboseJsonPropertyAndValueSerializer(odataVerboseJsonOutputContext);
				writeValue(odataVerboseJsonPropertyAndValueSerializer);
			}
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x000362A0 File Offset: 0x000344A0
		private static void WriteJsonLightLiteral(IEdmModel model, ODataMessageWriterSettings messageWriterSettings, TextWriter textWriter, Action<ODataJsonLightValueSerializer> writeValue)
		{
			using (ODataJsonLightOutputContext odataJsonLightOutputContext = new ODataJsonLightOutputContext(ODataFormat.Json, textWriter, messageWriterSettings, model))
			{
				ODataJsonLightValueSerializer odataJsonLightValueSerializer = new ODataJsonLightValueSerializer(odataJsonLightOutputContext);
				writeValue(odataJsonLightValueSerializer);
			}
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x000362E8 File Offset: 0x000344E8
		private static object CoerceNumericType(object primitiveValue, IEdmPrimitiveType targetEdmType)
		{
			ExceptionUtils.CheckArgumentNotNull<object>(primitiveValue, "primitiveValue");
			ExceptionUtils.CheckArgumentNotNull<IEdmPrimitiveType>(targetEdmType, "targetEdmType");
			Type type = primitiveValue.GetType();
			TypeCode typeCode = PlatformHelper.GetTypeCode(type);
			EdmPrimitiveTypeKind primitiveKind = targetEdmType.PrimitiveKind;
			switch (typeCode)
			{
			case TypeCode.SByte:
				switch (primitiveKind)
				{
				case EdmPrimitiveTypeKind.Decimal:
					return Convert.ToDecimal((sbyte)primitiveValue);
				case EdmPrimitiveTypeKind.Double:
					return Convert.ToDouble((sbyte)primitiveValue);
				case EdmPrimitiveTypeKind.Int16:
					return Convert.ToInt16((sbyte)primitiveValue);
				case EdmPrimitiveTypeKind.Int32:
					return Convert.ToInt32((sbyte)primitiveValue);
				case EdmPrimitiveTypeKind.Int64:
					return Convert.ToInt64((sbyte)primitiveValue);
				case EdmPrimitiveTypeKind.SByte:
					return primitiveValue;
				case EdmPrimitiveTypeKind.Single:
					return Convert.ToSingle((sbyte)primitiveValue);
				}
				break;
			case TypeCode.Byte:
				switch (primitiveKind)
				{
				case EdmPrimitiveTypeKind.Byte:
					return primitiveValue;
				case EdmPrimitiveTypeKind.Decimal:
					return Convert.ToDecimal((byte)primitiveValue);
				case EdmPrimitiveTypeKind.Double:
					return Convert.ToDouble((byte)primitiveValue);
				case EdmPrimitiveTypeKind.Int16:
					return Convert.ToInt16((byte)primitiveValue);
				case EdmPrimitiveTypeKind.Int32:
					return Convert.ToInt32((byte)primitiveValue);
				case EdmPrimitiveTypeKind.Int64:
					return Convert.ToInt64((byte)primitiveValue);
				case EdmPrimitiveTypeKind.Single:
					return Convert.ToSingle((byte)primitiveValue);
				}
				break;
			case TypeCode.Int16:
				switch (primitiveKind)
				{
				case EdmPrimitiveTypeKind.Decimal:
					return Convert.ToDecimal((short)primitiveValue);
				case EdmPrimitiveTypeKind.Double:
					return Convert.ToDouble((short)primitiveValue);
				case EdmPrimitiveTypeKind.Int16:
					return primitiveValue;
				case EdmPrimitiveTypeKind.Int32:
					return Convert.ToInt32((short)primitiveValue);
				case EdmPrimitiveTypeKind.Int64:
					return Convert.ToInt64((short)primitiveValue);
				case EdmPrimitiveTypeKind.Single:
					return Convert.ToSingle((short)primitiveValue);
				}
				break;
			case TypeCode.Int32:
				switch (primitiveKind)
				{
				case EdmPrimitiveTypeKind.Decimal:
					return Convert.ToDecimal((int)primitiveValue);
				case EdmPrimitiveTypeKind.Double:
					return Convert.ToDouble((int)primitiveValue);
				case EdmPrimitiveTypeKind.Int32:
					return primitiveValue;
				case EdmPrimitiveTypeKind.Int64:
					return Convert.ToInt64((int)primitiveValue);
				case EdmPrimitiveTypeKind.Single:
					return Convert.ToSingle((int)primitiveValue);
				}
				break;
			case TypeCode.Int64:
			{
				EdmPrimitiveTypeKind edmPrimitiveTypeKind = primitiveKind;
				switch (edmPrimitiveTypeKind)
				{
				case EdmPrimitiveTypeKind.Decimal:
					return Convert.ToDecimal((long)primitiveValue);
				case EdmPrimitiveTypeKind.Double:
					return Convert.ToDouble((long)primitiveValue);
				default:
					switch (edmPrimitiveTypeKind)
					{
					case EdmPrimitiveTypeKind.Int64:
						return primitiveValue;
					case EdmPrimitiveTypeKind.Single:
						return Convert.ToSingle((long)primitiveValue);
					}
					break;
				}
				break;
			}
			case TypeCode.Single:
			{
				EdmPrimitiveTypeKind edmPrimitiveTypeKind2 = primitiveKind;
				if (edmPrimitiveTypeKind2 == EdmPrimitiveTypeKind.Double)
				{
					return Convert.ToDouble((float)primitiveValue);
				}
				if (edmPrimitiveTypeKind2 == EdmPrimitiveTypeKind.Single)
				{
					return primitiveValue;
				}
				break;
			}
			case TypeCode.Double:
			{
				EdmPrimitiveTypeKind edmPrimitiveTypeKind3 = primitiveKind;
				if (edmPrimitiveTypeKind3 == EdmPrimitiveTypeKind.Double)
				{
					return primitiveValue;
				}
				break;
			}
			case TypeCode.Decimal:
			{
				EdmPrimitiveTypeKind edmPrimitiveTypeKind4 = primitiveKind;
				if (edmPrimitiveTypeKind4 == EdmPrimitiveTypeKind.Decimal)
				{
					return primitiveValue;
				}
				break;
			}
			}
			return null;
		}
	}
}
