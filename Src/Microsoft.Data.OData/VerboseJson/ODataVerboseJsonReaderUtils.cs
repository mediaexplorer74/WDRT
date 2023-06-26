using System;
using System.Globalization;
using System.Xml;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.VerboseJson
{
	// Token: 0x02000207 RID: 519
	internal static class ODataVerboseJsonReaderUtils
	{
		// Token: 0x06000FE8 RID: 4072 RVA: 0x0003A0DF File Offset: 0x000382DF
		internal static ODataVerboseJsonReaderUtils.FeedPropertyKind DetermineFeedPropertyKind(string propertyName)
		{
			if (string.CompareOrdinal("__count", propertyName) == 0)
			{
				return ODataVerboseJsonReaderUtils.FeedPropertyKind.Count;
			}
			if (string.CompareOrdinal("__next", propertyName) == 0)
			{
				return ODataVerboseJsonReaderUtils.FeedPropertyKind.NextPageLink;
			}
			if (string.CompareOrdinal("results", propertyName) == 0)
			{
				return ODataVerboseJsonReaderUtils.FeedPropertyKind.Results;
			}
			return ODataVerboseJsonReaderUtils.FeedPropertyKind.Unsupported;
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x0003A110 File Offset: 0x00038310
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
				ODataReaderBehavior readerBehavior = messageReaderSettings.ReaderBehavior;
				string text = value as string;
				if (text != null)
				{
					return ODataVerboseJsonReaderUtils.ConvertStringValue(text, primitiveClrType, version);
				}
				if (value is int)
				{
					return ODataVerboseJsonReaderUtils.ConvertInt32Value((int)value, primitiveClrType, primitiveTypeReference, readerBehavior != null && readerBehavior.UseV1ProviderBehavior);
				}
				if (value is double)
				{
					double num = (double)value;
					if (primitiveClrType == typeof(float))
					{
						return Convert.ToSingle(num);
					}
					if (!ODataVerboseJsonReaderUtils.IsV1PrimitiveType(primitiveClrType) || (primitiveClrType != typeof(double) && (readerBehavior == null || !readerBehavior.UseV1ProviderBehavior)))
					{
						throw new ODataException(Strings.ODataJsonReaderUtils_CannotConvertDouble(primitiveTypeReference.ODataFullName()));
					}
				}
				else if (value is bool)
				{
					if (primitiveClrType != typeof(bool) && (readerBehavior == null || readerBehavior.FormatBehaviorKind != ODataBehaviorKind.WcfDataServicesServer))
					{
						throw new ODataException(Strings.ODataJsonReaderUtils_CannotConvertBoolean(primitiveTypeReference.ODataFullName()));
					}
				}
				else
				{
					if (value is DateTime)
					{
						return ODataVerboseJsonReaderUtils.ConvertDateTimeValue((DateTime)value, primitiveClrType, primitiveTypeReference, readerBehavior);
					}
					if (value is DateTimeOffset && primitiveClrType != typeof(DateTimeOffset))
					{
						throw new ODataException(Strings.ODataJsonReaderUtils_CannotConvertDateTimeOffset(primitiveTypeReference.ODataFullName()));
					}
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

		// Token: 0x06000FEA RID: 4074 RVA: 0x0003A2AC File Offset: 0x000384AC
		internal static void EnsureInstance<T>(ref T instance) where T : class, new()
		{
			if (instance == null)
			{
				instance = new T();
			}
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x0003A2C6 File Offset: 0x000384C6
		internal static bool ErrorPropertyNotFound(ref ODataVerboseJsonReaderUtils.ErrorPropertyBitMask propertiesFoundBitField, ODataVerboseJsonReaderUtils.ErrorPropertyBitMask propertyFoundBitMask)
		{
			if ((propertiesFoundBitField & propertyFoundBitMask) == propertyFoundBitMask)
			{
				return false;
			}
			propertiesFoundBitField |= propertyFoundBitMask;
			return true;
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x0003A2D8 File Offset: 0x000384D8
		internal static void ValidateMetadataStringProperty(string propertyValue, string propertyName)
		{
			if (propertyValue == null)
			{
				throw new ODataException(Strings.ODataJsonReaderUtils_MetadataPropertyWithNullValue(propertyName));
			}
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x0003A2E9 File Offset: 0x000384E9
		internal static void VerifyMetadataPropertyNotFound(ref ODataVerboseJsonReaderUtils.MetadataPropertyBitMask propertiesFoundBitField, ODataVerboseJsonReaderUtils.MetadataPropertyBitMask propertyFoundBitMask, string propertyName)
		{
			if ((propertiesFoundBitField & propertyFoundBitMask) != ODataVerboseJsonReaderUtils.MetadataPropertyBitMask.None)
			{
				throw new ODataException(Strings.ODataJsonReaderUtils_MultipleMetadataPropertiesWithSameName(propertyName));
			}
			propertiesFoundBitField |= propertyFoundBitMask;
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x0003A303 File Offset: 0x00038503
		internal static void ValidateEntityReferenceLinksStringProperty(string propertyValue, string propertyName)
		{
			if (propertyValue == null)
			{
				throw new ODataException(Strings.ODataJsonReaderUtils_EntityReferenceLinksPropertyWithNullValue(propertyName));
			}
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0003A314 File Offset: 0x00038514
		internal static void ValidateCountPropertyInEntityReferenceLinks(long? propertyValue)
		{
			if (propertyValue == null)
			{
				throw new ODataException(Strings.ODataJsonReaderUtils_EntityReferenceLinksInlineCountWithNullValue("__count"));
			}
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x0003A32F File Offset: 0x0003852F
		internal static void VerifyEntityReferenceLinksWrapperPropertyNotFound(ref ODataVerboseJsonReaderUtils.EntityReferenceLinksWrapperPropertyBitMask propertiesFoundBitField, ODataVerboseJsonReaderUtils.EntityReferenceLinksWrapperPropertyBitMask propertyFoundBitMask, string propertyName)
		{
			if ((propertiesFoundBitField & propertyFoundBitMask) == propertyFoundBitMask)
			{
				throw new ODataException(Strings.ODataJsonReaderUtils_MultipleEntityReferenceLinksWrapperPropertiesWithSameName(propertyName));
			}
			propertiesFoundBitField |= propertyFoundBitMask;
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x0003A34A File Offset: 0x0003854A
		internal static void VerifyErrorPropertyNotFound(ref ODataVerboseJsonReaderUtils.ErrorPropertyBitMask propertiesFoundBitField, ODataVerboseJsonReaderUtils.ErrorPropertyBitMask propertyFoundBitMask, string propertyName)
		{
			if (!ODataVerboseJsonReaderUtils.ErrorPropertyNotFound(ref propertiesFoundBitField, propertyFoundBitMask))
			{
				throw new ODataException(Strings.ODataJsonReaderUtils_MultipleErrorPropertiesWithSameName(propertyName));
			}
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x0003A361 File Offset: 0x00038561
		internal static void ValidateMediaResourceStringProperty(string propertyValue, string propertyName)
		{
			if (propertyValue == null)
			{
				throw new ODataException(Strings.ODataJsonReaderUtils_MediaResourcePropertyWithNullValue(propertyName));
			}
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x0003A372 File Offset: 0x00038572
		internal static void ValidateFeedProperty(object propertyValue, string propertyName)
		{
			if (propertyValue == null)
			{
				throw new ODataException(Strings.ODataJsonReaderUtils_FeedPropertyWithNullValue(propertyName));
			}
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x0003A384 File Offset: 0x00038584
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
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataVerboseJsonReader_ReadEntryStart));
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x0003A434 File Offset: 0x00038634
		private static object ConvertStringValue(string stringValue, Type targetType, ODataVersion version)
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
			if (targetType == typeof(DateTimeOffset))
			{
				return PlatformHelper.ConvertStringToDateTimeOffset(stringValue);
			}
			if (targetType == typeof(DateTime) && version >= ODataVersion.V3)
			{
				try
				{
					return PlatformHelper.ConvertStringToDateTime(stringValue);
				}
				catch (FormatException)
				{
				}
			}
			return Convert.ChangeType(stringValue, targetType, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x0003A4FC File Offset: 0x000386FC
		private static object ConvertInt32Value(int intValue, Type targetType, IEdmPrimitiveTypeReference primitiveTypeReference, bool usesV1ProviderBehavior)
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
			if (!ODataVerboseJsonReaderUtils.IsV1PrimitiveType(targetType) || (targetType != typeof(int) && !usesV1ProviderBehavior))
			{
				throw new ODataException(Strings.ODataJsonReaderUtils_CannotConvertInt32(primitiveTypeReference.ODataFullName()));
			}
			return intValue;
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0003A604 File Offset: 0x00038804
		private static object ConvertDateTimeValue(DateTime datetimeValue, Type targetType, IEdmPrimitiveTypeReference primitiveTypeReference, ODataReaderBehavior readerBehavior)
		{
			if (targetType == typeof(DateTimeOffset) && (datetimeValue.Kind == DateTimeKind.Local || datetimeValue.Kind == DateTimeKind.Utc))
			{
				return new DateTimeOffset(datetimeValue);
			}
			if (targetType != typeof(DateTime) && (readerBehavior == null || readerBehavior.FormatBehaviorKind != ODataBehaviorKind.WcfDataServicesServer))
			{
				throw new ODataException(Strings.ODataJsonReaderUtils_CannotConvertDateTime(primitiveTypeReference.ODataFullName()));
			}
			return datetimeValue;
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x0003A678 File Offset: 0x00038878
		private static bool IsV1PrimitiveType(Type targetType)
		{
			return !(targetType == typeof(DateTimeOffset)) && !(targetType == typeof(TimeSpan));
		}

		// Token: 0x02000208 RID: 520
		internal enum FeedPropertyKind
		{
			// Token: 0x040005C6 RID: 1478
			Unsupported,
			// Token: 0x040005C7 RID: 1479
			Count,
			// Token: 0x040005C8 RID: 1480
			Results,
			// Token: 0x040005C9 RID: 1481
			NextPageLink
		}

		// Token: 0x02000209 RID: 521
		[Flags]
		internal enum EntityReferenceLinksWrapperPropertyBitMask
		{
			// Token: 0x040005CB RID: 1483
			None = 0,
			// Token: 0x040005CC RID: 1484
			Count = 1,
			// Token: 0x040005CD RID: 1485
			Results = 2,
			// Token: 0x040005CE RID: 1486
			NextPageLink = 4
		}

		// Token: 0x0200020A RID: 522
		[Flags]
		internal enum ErrorPropertyBitMask
		{
			// Token: 0x040005D0 RID: 1488
			None = 0,
			// Token: 0x040005D1 RID: 1489
			Error = 1,
			// Token: 0x040005D2 RID: 1490
			Code = 2,
			// Token: 0x040005D3 RID: 1491
			Message = 4,
			// Token: 0x040005D4 RID: 1492
			MessageLanguage = 8,
			// Token: 0x040005D5 RID: 1493
			MessageValue = 16,
			// Token: 0x040005D6 RID: 1494
			InnerError = 32,
			// Token: 0x040005D7 RID: 1495
			TypeName = 64,
			// Token: 0x040005D8 RID: 1496
			StackTrace = 128
		}

		// Token: 0x0200020B RID: 523
		[Flags]
		internal enum MetadataPropertyBitMask
		{
			// Token: 0x040005DA RID: 1498
			None = 0,
			// Token: 0x040005DB RID: 1499
			Uri = 1,
			// Token: 0x040005DC RID: 1500
			Type = 2,
			// Token: 0x040005DD RID: 1501
			ETag = 4,
			// Token: 0x040005DE RID: 1502
			MediaUri = 8,
			// Token: 0x040005DF RID: 1503
			EditMedia = 16,
			// Token: 0x040005E0 RID: 1504
			ContentType = 32,
			// Token: 0x040005E1 RID: 1505
			MediaETag = 64,
			// Token: 0x040005E2 RID: 1506
			Properties = 128,
			// Token: 0x040005E3 RID: 1507
			Id = 256,
			// Token: 0x040005E4 RID: 1508
			Actions = 512,
			// Token: 0x040005E5 RID: 1509
			Functions = 1024
		}
	}
}
