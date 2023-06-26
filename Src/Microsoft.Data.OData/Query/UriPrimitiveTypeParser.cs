using System;
using System.Globalization;
using System.Spatial;
using System.Text;
using System.Xml;
using Microsoft.Data.Edm;
using Microsoft.Data.Edm.Library;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x020000D9 RID: 217
	internal static class UriPrimitiveTypeParser
	{
		// Token: 0x06000544 RID: 1348 RVA: 0x00011F09 File Offset: 0x00010109
		internal static bool IsCharHexDigit(char c)
		{
			return (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F');
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00011F30 File Offset: 0x00010130
		internal static bool TryUriStringToPrimitive(string text, IEdmTypeReference targetType, out object targetValue)
		{
			if (targetType.IsNullable && text == "null")
			{
				targetValue = null;
				return true;
			}
			IEdmPrimitiveTypeReference edmPrimitiveTypeReference = targetType.AsPrimitiveOrNull();
			if (edmPrimitiveTypeReference == null)
			{
				targetValue = null;
				return false;
			}
			EdmPrimitiveTypeKind edmPrimitiveTypeKind = edmPrimitiveTypeReference.PrimitiveKind();
			byte[] array;
			bool flag = UriPrimitiveTypeParser.TryUriStringToByteArray(text, out array);
			if (edmPrimitiveTypeKind == EdmPrimitiveTypeKind.Binary)
			{
				targetValue = array;
				return flag;
			}
			if (flag)
			{
				string @string = Encoding.UTF8.GetString(array, 0, array.Length);
				return UriPrimitiveTypeParser.TryUriStringToPrimitive(@string, targetType, out targetValue);
			}
			if (edmPrimitiveTypeKind == EdmPrimitiveTypeKind.Guid)
			{
				Guid guid;
				bool flag2 = UriPrimitiveTypeParser.TryUriStringToGuid(text, out guid);
				targetValue = guid;
				return flag2;
			}
			if (edmPrimitiveTypeKind == EdmPrimitiveTypeKind.DateTime)
			{
				DateTime dateTime;
				bool flag3 = UriPrimitiveTypeParser.TryUriStringToDateTime(text, out dateTime);
				targetValue = dateTime;
				return flag3;
			}
			if (edmPrimitiveTypeKind == EdmPrimitiveTypeKind.DateTimeOffset)
			{
				DateTimeOffset dateTimeOffset;
				bool flag4 = UriPrimitiveTypeParser.TryUriStringToDateTimeOffset(text, out dateTimeOffset);
				targetValue = dateTimeOffset;
				return flag4;
			}
			if (edmPrimitiveTypeKind == EdmPrimitiveTypeKind.Time)
			{
				TimeSpan timeSpan;
				bool flag5 = UriPrimitiveTypeParser.TryUriStringToTime(text, out timeSpan);
				targetValue = timeSpan;
				return flag5;
			}
			if (edmPrimitiveTypeKind == EdmPrimitiveTypeKind.Geography)
			{
				Geography geography;
				bool flag6 = UriPrimitiveTypeParser.TryUriStringToGeography(text, out geography);
				targetValue = geography;
				return flag6;
			}
			if (edmPrimitiveTypeKind == EdmPrimitiveTypeKind.Geometry)
			{
				Geometry geometry;
				bool flag7 = UriPrimitiveTypeParser.TryUriStringToGeometry(text, out geometry);
				targetValue = geometry;
				return flag7;
			}
			bool flag8 = edmPrimitiveTypeKind == EdmPrimitiveTypeKind.String;
			if (flag8 != UriPrimitiveTypeParser.IsUriValueQuoted(text))
			{
				targetValue = null;
				return false;
			}
			if (flag8)
			{
				text = UriPrimitiveTypeParser.RemoveQuotes(text);
			}
			bool flag9;
			try
			{
				switch (edmPrimitiveTypeKind)
				{
				case EdmPrimitiveTypeKind.Boolean:
					targetValue = XmlConvert.ToBoolean(text);
					goto IL_2B3;
				case EdmPrimitiveTypeKind.Byte:
					targetValue = XmlConvert.ToByte(text);
					goto IL_2B3;
				case EdmPrimitiveTypeKind.Decimal:
					if (UriPrimitiveTypeParser.TryRemoveLiteralSuffix("M", ref text))
					{
						try
						{
							targetValue = XmlConvert.ToDecimal(text);
							goto IL_2B3;
						}
						catch (FormatException)
						{
							decimal num;
							if (decimal.TryParse(text, NumberStyles.Float, NumberFormatInfo.InvariantInfo, out num))
							{
								targetValue = num;
								goto IL_2B3;
							}
							targetValue = 0m;
							return false;
						}
					}
					targetValue = 0m;
					return false;
				case EdmPrimitiveTypeKind.Double:
					UriPrimitiveTypeParser.TryRemoveLiteralSuffix("D", ref text);
					targetValue = XmlConvert.ToDouble(text);
					goto IL_2B3;
				case EdmPrimitiveTypeKind.Int16:
					targetValue = XmlConvert.ToInt16(text);
					goto IL_2B3;
				case EdmPrimitiveTypeKind.Int32:
					targetValue = XmlConvert.ToInt32(text);
					goto IL_2B3;
				case EdmPrimitiveTypeKind.Int64:
					if (UriPrimitiveTypeParser.TryRemoveLiteralSuffix("L", ref text))
					{
						targetValue = XmlConvert.ToInt64(text);
						goto IL_2B3;
					}
					targetValue = 0L;
					return false;
				case EdmPrimitiveTypeKind.SByte:
					targetValue = XmlConvert.ToSByte(text);
					goto IL_2B3;
				case EdmPrimitiveTypeKind.Single:
					if (UriPrimitiveTypeParser.TryRemoveLiteralSuffix("f", ref text))
					{
						targetValue = XmlConvert.ToSingle(text);
						goto IL_2B3;
					}
					targetValue = 0f;
					return false;
				case EdmPrimitiveTypeKind.String:
					targetValue = text;
					goto IL_2B3;
				}
				throw new ODataException(Strings.General_InternalError(InternalErrorCodes.UriPrimitiveTypeParser_TryUriStringToPrimitive));
				IL_2B3:
				flag9 = true;
			}
			catch (FormatException)
			{
				targetValue = null;
				flag9 = false;
			}
			catch (OverflowException)
			{
				targetValue = null;
				flag9 = false;
			}
			return flag9;
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00012258 File Offset: 0x00010458
		internal static bool TryUriStringToNonNegativeInteger(string text, out int nonNegativeInteger)
		{
			object obj;
			if (!UriPrimitiveTypeParser.TryUriStringToPrimitive(text, EdmCoreModel.Instance.GetInt32(false), out obj))
			{
				nonNegativeInteger = -1;
				return false;
			}
			nonNegativeInteger = (int)obj;
			return nonNegativeInteger >= 0;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0001228F File Offset: 0x0001048F
		internal static bool TryRemoveSuffix(string suffix, ref string text)
		{
			return UriPrimitiveTypeParser.TryRemoveLiteralSuffix(suffix, ref text);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00012298 File Offset: 0x00010498
		internal static bool TryRemovePrefix(string prefix, ref string text)
		{
			return UriPrimitiveTypeParser.TryRemoveLiteralPrefix(prefix, ref text);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x000122A4 File Offset: 0x000104A4
		internal static bool TryRemoveQuotes(ref string text)
		{
			if (text.Length < 2)
			{
				return false;
			}
			char c = text[0];
			if (c != '\'' || text[text.Length - 1] != c)
			{
				return false;
			}
			string text2 = text.Substring(1, text.Length - 2);
			int num = 0;
			for (;;)
			{
				int num2 = text2.IndexOf(c, num);
				if (num2 < 0)
				{
					goto IL_76;
				}
				text2 = text2.Remove(num2, 1);
				if (text2.Length < num2 + 1 || text2[num2] != c)
				{
					break;
				}
				num = num2 + 1;
			}
			return false;
			IL_76:
			text = text2;
			return true;
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0001232C File Offset: 0x0001052C
		private static bool TryUriStringToByteArray(string text, out byte[] targetValue)
		{
			if (!UriPrimitiveTypeParser.TryRemoveLiteralPrefix("binary", ref text) && !UriPrimitiveTypeParser.TryRemoveLiteralPrefix("X", ref text))
			{
				targetValue = null;
				return false;
			}
			if (!UriPrimitiveTypeParser.TryRemoveQuotes(ref text))
			{
				targetValue = null;
				return false;
			}
			if (text.Length % 2 != 0)
			{
				targetValue = null;
				return false;
			}
			byte[] array = new byte[text.Length / 2];
			int i = 0;
			int num = 0;
			while (i < array.Length)
			{
				char c = text[num];
				char c2 = text[num + 1];
				if (!UriPrimitiveTypeParser.IsCharHexDigit(c) || !UriPrimitiveTypeParser.IsCharHexDigit(c2))
				{
					targetValue = null;
					return false;
				}
				array[i] = (byte)(UriPrimitiveTypeParser.HexCharToNibble(c) << 4) + UriPrimitiveTypeParser.HexCharToNibble(c2);
				num += 2;
				i++;
			}
			targetValue = array;
			return true;
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x000123DC File Offset: 0x000105DC
		private static bool TryUriStringToGuid(string text, out Guid targetValue)
		{
			if (!UriPrimitiveTypeParser.TryRemoveLiteralPrefix("guid", ref text))
			{
				targetValue = default(Guid);
				return false;
			}
			if (!UriPrimitiveTypeParser.TryRemoveQuotes(ref text))
			{
				targetValue = default(Guid);
				return false;
			}
			bool flag;
			try
			{
				targetValue = XmlConvert.ToGuid(text);
				flag = true;
			}
			catch (FormatException)
			{
				targetValue = default(Guid);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00012440 File Offset: 0x00010640
		private static bool TryUriStringToDateTime(string text, out DateTime targetValue)
		{
			targetValue = default(DateTime);
			if (!UriPrimitiveTypeParser.TryRemoveLiteralPrefix("datetime", ref text))
			{
				return false;
			}
			if (!UriPrimitiveTypeParser.TryRemoveQuotes(ref text))
			{
				return false;
			}
			bool flag;
			try
			{
				targetValue = PlatformHelper.ConvertStringToDateTime(text);
				flag = true;
			}
			catch (FormatException)
			{
				flag = false;
			}
			catch (ArgumentOutOfRangeException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x000124A8 File Offset: 0x000106A8
		private static bool TryUriStringToDateTimeOffset(string text, out DateTimeOffset targetValue)
		{
			targetValue = default(DateTimeOffset);
			if (!UriPrimitiveTypeParser.TryRemoveLiteralPrefix("datetimeoffset", ref text))
			{
				return false;
			}
			if (!UriPrimitiveTypeParser.TryRemoveQuotes(ref text))
			{
				return false;
			}
			bool flag;
			try
			{
				targetValue = PlatformHelper.ConvertStringToDateTimeOffset(text);
				flag = true;
			}
			catch (FormatException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00012500 File Offset: 0x00010700
		private static bool TryUriStringToTime(string text, out TimeSpan targetValue)
		{
			if (!UriPrimitiveTypeParser.TryRemoveLiteralPrefix("time", ref text))
			{
				targetValue = default(TimeSpan);
				return false;
			}
			if (!UriPrimitiveTypeParser.TryRemoveQuotes(ref text))
			{
				targetValue = default(TimeSpan);
				return false;
			}
			bool flag;
			try
			{
				targetValue = XmlConvert.ToTimeSpan(text);
				flag = true;
			}
			catch (FormatException)
			{
				targetValue = default(TimeSpan);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00012564 File Offset: 0x00010764
		private static bool TryUriStringToGeography(string text, out Geography targetValue)
		{
			if (!UriPrimitiveTypeParser.TryRemoveLiteralPrefix("geography", ref text))
			{
				targetValue = null;
				return false;
			}
			if (!UriPrimitiveTypeParser.TryRemoveQuotes(ref text))
			{
				targetValue = null;
				return false;
			}
			bool flag;
			try
			{
				targetValue = LiteralUtils.ParseGeography(text);
				flag = true;
			}
			catch (ParseErrorException)
			{
				targetValue = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x000125B8 File Offset: 0x000107B8
		private static bool TryUriStringToGeometry(string text, out Geometry targetValue)
		{
			if (!UriPrimitiveTypeParser.TryRemoveLiteralPrefix("geometry", ref text))
			{
				targetValue = null;
				return false;
			}
			if (!UriPrimitiveTypeParser.TryRemoveQuotes(ref text))
			{
				targetValue = null;
				return false;
			}
			bool flag;
			try
			{
				targetValue = LiteralUtils.ParseGeometry(text);
				flag = true;
			}
			catch (ParseErrorException)
			{
				targetValue = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0001260C File Offset: 0x0001080C
		private static bool TryRemoveLiteralSuffix(string suffix, ref string text)
		{
			text = text.Trim(UriPrimitiveTypeParser.WhitespaceChars);
			if (text.Length <= suffix.Length || !text.EndsWith(suffix, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			text = text.Substring(0, text.Length - suffix.Length);
			return true;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0001265C File Offset: 0x0001085C
		private static bool TryRemoveLiteralPrefix(string prefix, ref string text)
		{
			if (text.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
			{
				text = text.Remove(0, prefix.Length);
				return true;
			}
			return false;
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0001267C File Offset: 0x0001087C
		private static bool IsUriValueQuoted(string text)
		{
			if (text.Length < 2 || text[0] != '\'' || text[text.Length - 1] != '\'')
			{
				return false;
			}
			int num;
			for (int i = 1; i < text.Length - 1; i = num + 2)
			{
				num = text.IndexOf('\'', i, text.Length - i - 1);
				if (num == -1)
				{
					break;
				}
				if (num == text.Length - 2 || text[num + 1] != '\'')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x000126F8 File Offset: 0x000108F8
		private static string RemoveQuotes(string text)
		{
			char c = text[0];
			string text2 = text.Substring(1, text.Length - 2);
			int num = 0;
			for (;;)
			{
				int num2 = text2.IndexOf(c, num);
				if (num2 < 0)
				{
					break;
				}
				text2 = text2.Remove(num2, 1);
				num = num2 + 1;
			}
			return text2;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0001273C File Offset: 0x0001093C
		private static byte HexCharToNibble(char c)
		{
			switch (c)
			{
			case '0':
				return 0;
			case '1':
				return 1;
			case '2':
				return 2;
			case '3':
				return 3;
			case '4':
				return 4;
			case '5':
				return 5;
			case '6':
				return 6;
			case '7':
				return 7;
			case '8':
				return 8;
			case '9':
				return 9;
			case ':':
			case ';':
			case '<':
			case '=':
			case '>':
			case '?':
			case '@':
				goto IL_B1;
			case 'A':
				break;
			case 'B':
				return 11;
			case 'C':
				return 12;
			case 'D':
				return 13;
			case 'E':
				return 14;
			case 'F':
				return 15;
			default:
				switch (c)
				{
				case 'a':
					break;
				case 'b':
					return 11;
				case 'c':
					return 12;
				case 'd':
					return 13;
				case 'e':
					return 14;
				case 'f':
					return 15;
				default:
					goto IL_B1;
				}
				break;
			}
			return 10;
			IL_B1:
			throw new ODataException(Strings.General_InternalError(InternalErrorCodes.UriPrimitiveTypeParser_HexCharToNibble));
		}

		// Token: 0x04000229 RID: 553
		private static char[] WhitespaceChars = new char[] { ' ', '\t', '\n', '\r' };
	}
}
