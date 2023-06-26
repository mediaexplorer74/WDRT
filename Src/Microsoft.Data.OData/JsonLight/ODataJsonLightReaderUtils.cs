using System;
using System.Globalization;
using System.Xml;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x020001A1 RID: 417
	internal static class ODataJsonLightReaderUtils
	{
		// Token: 0x06000CB5 RID: 3253 RVA: 0x0002BE38 File Offset: 0x0002A038
		internal static object ConvertValue(object value, IEdmPrimitiveTypeReference primitiveTypeReference, ODataMessageReaderSettings messageReaderSettings, ODataVersion version, bool validateNullValue, string propertyName)
		{
			if (value == null)
			{
				ReaderValidationUtils.ValidateNullValue(EdmCoreModel.Instance, primitiveTypeReference, messageReaderSettings, validateNullValue, version, propertyName);
				return null;
			}
			try
			{
				Type primitiveClrType = EdmLibraryExtensions.GetPrimitiveClrType(primitiveTypeReference.PrimitiveDefinition(), false);
				string text = value as string;
				if (text != null)
				{
					return ODataJsonLightReaderUtils.ConvertStringValue(text, primitiveClrType);
				}
				if (value is int)
				{
					return ODataJsonLightReaderUtils.ConvertInt32Value((int)value, primitiveClrType, primitiveTypeReference);
				}
				if (value is double)
				{
					double num = (double)value;
					if (primitiveClrType == typeof(float))
					{
						return Convert.ToSingle(num);
					}
					if (primitiveClrType != typeof(double))
					{
						throw new ODataException(Strings.ODataJsonReaderUtils_CannotConvertDouble(primitiveTypeReference.ODataFullName()));
					}
				}
				else if (value is bool)
				{
					if (primitiveClrType != typeof(bool))
					{
						throw new ODataException(Strings.ODataJsonReaderUtils_CannotConvertBoolean(primitiveTypeReference.ODataFullName()));
					}
				}
				else if (value is DateTime)
				{
					if (primitiveClrType != typeof(DateTime))
					{
						throw new ODataException(Strings.ODataJsonReaderUtils_CannotConvertDateTime(primitiveTypeReference.ODataFullName()));
					}
				}
				else if (value is DateTimeOffset && primitiveClrType != typeof(DateTimeOffset))
				{
					throw new ODataException(Strings.ODataJsonReaderUtils_CannotConvertDateTimeOffset(primitiveTypeReference.ODataFullName()));
				}
			}
			catch (Exception ex)
			{
				if (!ExceptionUtils.IsCatchableExceptionType(ex))
				{
					throw;
				}
				throw ReaderValidationUtils.GetPrimitiveTypeConversionException(primitiveTypeReference, ex);
			}
			return value;
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x0002BFAC File Offset: 0x0002A1AC
		internal static void EnsureInstance<T>(ref T instance) where T : class, new()
		{
			if (instance == null)
			{
				instance = new T();
			}
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x0002BFC6 File Offset: 0x0002A1C6
		internal static bool IsODataAnnotationName(string propertyName)
		{
			return propertyName.StartsWith("odata.", StringComparison.Ordinal);
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x0002BFD4 File Offset: 0x0002A1D4
		internal static bool IsAnnotationProperty(string propertyName)
		{
			return propertyName.IndexOf('.') >= 0;
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x0002BFE4 File Offset: 0x0002A1E4
		internal static void ValidateAnnotationStringValue(string propertyValue, string annotationName)
		{
			if (propertyValue == null)
			{
				throw new ODataException(Strings.ODataJsonLightReaderUtils_AnnotationWithNullValue(annotationName));
			}
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x0002BFF8 File Offset: 0x0002A1F8
		internal static string GetPayloadTypeName(object payloadItem)
		{
			if (payloadItem == null)
			{
				return null;
			}
			TypeCode typeCode = PlatformHelper.GetTypeCode(payloadItem.GetType());
			TypeCode typeCode2 = typeCode;
			if (typeCode2 == TypeCode.Boolean)
			{
				return "Edm.Boolean";
			}
			if (typeCode2 == TypeCode.Int32)
			{
				return "Edm.Int32";
			}
			switch (typeCode2)
			{
			case TypeCode.Double:
				return "Edm.Double";
			case TypeCode.DateTime:
				return "Edm.DateTime";
			case TypeCode.String:
				return "Edm.String";
			}
			ODataComplexValue odataComplexValue = payloadItem as ODataComplexValue;
			if (odataComplexValue != null)
			{
				return odataComplexValue.TypeName;
			}
			ODataCollectionValue odataCollectionValue = payloadItem as ODataCollectionValue;
			if (odataCollectionValue != null)
			{
				return odataCollectionValue.TypeName;
			}
			ODataEntry odataEntry = payloadItem as ODataEntry;
			if (odataEntry != null)
			{
				return odataEntry.TypeName;
			}
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataJsonLightReader_ReadEntryStart));
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x0002C0A8 File Offset: 0x0002A2A8
		private static object ConvertStringValue(string stringValue, Type targetType)
		{
			if (targetType == typeof(byte[]))
			{
				return Convert.FromBase64String(stringValue);
			}
			if (targetType == typeof(Guid))
			{
				return new Guid(stringValue);
			}
			if (targetType == typeof(TimeSpan))
			{
				return XmlConvert.ToTimeSpan(stringValue);
			}
			if (targetType == typeof(DateTime))
			{
				return PlatformHelper.ConvertStringToDateTime(stringValue);
			}
			if (targetType == typeof(DateTimeOffset))
			{
				return PlatformHelper.ConvertStringToDateTimeOffset(stringValue);
			}
			return Convert.ChangeType(stringValue, targetType, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x0002C154 File Offset: 0x0002A354
		private static object ConvertInt32Value(int intValue, Type targetType, IEdmPrimitiveTypeReference primitiveTypeReference)
		{
			if (targetType == typeof(short))
			{
				return Convert.ToInt16(intValue);
			}
			if (targetType == typeof(byte))
			{
				return Convert.ToByte(intValue);
			}
			if (targetType == typeof(sbyte))
			{
				return Convert.ToSByte(intValue);
			}
			if (targetType == typeof(float))
			{
				return Convert.ToSingle(intValue);
			}
			if (targetType == typeof(double))
			{
				return Convert.ToDouble(intValue);
			}
			if (targetType == typeof(decimal) || targetType == typeof(long))
			{
				throw new ODataException(Strings.ODataJsonReaderUtils_CannotConvertInt64OrDecimal);
			}
			if (targetType != typeof(int))
			{
				throw new ODataException(Strings.ODataJsonReaderUtils_CannotConvertInt32(primitiveTypeReference.ODataFullName()));
			}
			return intValue;
		}
	}
}
