using System;
using System.Xml;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Atom;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x0200029B RID: 667
	internal static class AtomValueUtils
	{
		// Token: 0x0600168D RID: 5773 RVA: 0x00052064 File Offset: 0x00050264
		internal static void WritePrimitiveValue(XmlWriter writer, object value)
		{
			if (!PrimitiveConverter.Instance.TryWriteAtom(value, writer))
			{
				string text = AtomValueUtils.ConvertPrimitiveToString(value);
				ODataAtomWriterUtils.WriteString(writer, text);
			}
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x00052090 File Offset: 0x00050290
		internal static string ConvertPrimitiveToString(object value)
		{
			string text;
			if (!AtomValueUtils.TryConvertPrimitiveToString(value, out text))
			{
				throw new ODataException(Strings.AtomValueUtils_CannotConvertValueToAtomPrimitive(value.GetType().FullName));
			}
			return text;
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x000520C0 File Offset: 0x000502C0
		internal static object ReadPrimitiveValue(XmlReader reader, IEdmPrimitiveTypeReference primitiveTypeReference)
		{
			object obj;
			if (!PrimitiveConverter.Instance.TryTokenizeFromXml(reader, EdmLibraryExtensions.GetPrimitiveClrType(primitiveTypeReference), out obj))
			{
				string text = reader.ReadElementContentValue();
				return AtomValueUtils.ConvertStringToPrimitive(text, primitiveTypeReference);
			}
			return obj;
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x000520F4 File Offset: 0x000502F4
		internal static string ToString(AtomTextConstructKind textConstructKind)
		{
			switch (textConstructKind)
			{
			case AtomTextConstructKind.Text:
				return "text";
			case AtomTextConstructKind.Html:
				return "html";
			case AtomTextConstructKind.Xhtml:
				return "xhtml";
			default:
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.ODataAtomConvert_ToString));
			}
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x0005213C File Offset: 0x0005033C
		internal static bool TryConvertPrimitiveToString(object value, out string result)
		{
			result = null;
			switch (PlatformHelper.GetTypeCode(value.GetType()))
			{
			case TypeCode.Boolean:
				result = ODataAtomConvert.ToString((bool)value);
				return true;
			case TypeCode.SByte:
				result = ((sbyte)value).ToString();
				return true;
			case TypeCode.Byte:
				result = ODataAtomConvert.ToString((byte)value);
				return true;
			case TypeCode.Int16:
				result = ((short)value).ToString();
				return true;
			case TypeCode.Int32:
				result = ((int)value).ToString();
				return true;
			case TypeCode.Int64:
				result = ((long)value).ToString();
				return true;
			case TypeCode.Single:
				result = ((float)value).ToString();
				return true;
			case TypeCode.Double:
				result = ((double)value).ToString();
				return true;
			case TypeCode.Decimal:
				result = ODataAtomConvert.ToString((decimal)value);
				return true;
			case TypeCode.DateTime:
				result = ((DateTime)value).ToString();
				return true;
			case TypeCode.String:
				result = (string)value;
				return true;
			}
			byte[] array = value as byte[];
			if (array != null)
			{
				result = array.ToString();
			}
			else if (value is DateTimeOffset)
			{
				result = ODataAtomConvert.ToString((DateTimeOffset)value);
			}
			else if (value is Guid)
			{
				result = ((Guid)value).ToString();
			}
			else
			{
				if (!(value is TimeSpan))
				{
					return false;
				}
				result = ((TimeSpan)value).ToString();
			}
			return true;
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x000522BC File Offset: 0x000504BC
		internal static object ConvertStringToPrimitive(string text, IEdmPrimitiveTypeReference targetTypeReference)
		{
			try
			{
				switch (targetTypeReference.PrimitiveKind())
				{
				case EdmPrimitiveTypeKind.Binary:
					return Convert.FromBase64String(text);
				case EdmPrimitiveTypeKind.Boolean:
					return AtomValueUtils.ConvertXmlBooleanValue(text);
				case EdmPrimitiveTypeKind.Byte:
					return XmlConvert.ToByte(text);
				case EdmPrimitiveTypeKind.DateTime:
					return PlatformHelper.ConvertStringToDateTime(text);
				case EdmPrimitiveTypeKind.DateTimeOffset:
					return PlatformHelper.ConvertStringToDateTimeOffset(text);
				case EdmPrimitiveTypeKind.Decimal:
					return XmlConvert.ToDecimal(text);
				case EdmPrimitiveTypeKind.Double:
					return XmlConvert.ToDouble(text);
				case EdmPrimitiveTypeKind.Guid:
					return new Guid(text);
				case EdmPrimitiveTypeKind.Int16:
					return XmlConvert.ToInt16(text);
				case EdmPrimitiveTypeKind.Int32:
					return XmlConvert.ToInt32(text);
				case EdmPrimitiveTypeKind.Int64:
					return XmlConvert.ToInt64(text);
				case EdmPrimitiveTypeKind.SByte:
					return XmlConvert.ToSByte(text);
				case EdmPrimitiveTypeKind.Single:
					return XmlConvert.ToSingle(text);
				case EdmPrimitiveTypeKind.String:
					return text;
				case EdmPrimitiveTypeKind.Time:
					return XmlConvert.ToTimeSpan(text);
				}
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.AtomValueUtils_ConvertStringToPrimitive));
			}
			catch (Exception ex)
			{
				if (!ExceptionUtils.IsCatchableExceptionType(ex))
				{
					throw;
				}
				throw ReaderValidationUtils.GetPrimitiveTypeConversionException(targetTypeReference, ex);
			}
			object obj;
			return obj;
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x0005247C File Offset: 0x0005067C
		private static bool ConvertXmlBooleanValue(string text)
		{
			text = text.Trim(AtomValueUtils.XmlWhitespaceChars);
			string text2;
			switch (text2 = text)
			{
			case "true":
			case "True":
			case "1":
				return true;
			case "false":
			case "False":
			case "0":
				return false;
			}
			return XmlConvert.ToBoolean(text);
		}

		// Token: 0x04000907 RID: 2311
		private static readonly char[] XmlWhitespaceChars = new char[] { ' ', '\t', '\n', '\r' };
	}
}
