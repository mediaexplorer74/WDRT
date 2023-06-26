using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Data.OData
{
	// Token: 0x0200028B RID: 651
	internal static class HttpUtils
	{
		// Token: 0x060015A2 RID: 5538 RVA: 0x0004F2EC File Offset: 0x0004D4EC
		internal static IList<KeyValuePair<string, string>> ReadMimeType(string contentType, out string mediaTypeName, out string mediaTypeCharset)
		{
			if (string.IsNullOrEmpty(contentType))
			{
				throw new ODataContentTypeException(Strings.HttpUtils_ContentTypeMissing);
			}
			IList<KeyValuePair<MediaType, string>> list = HttpUtils.ReadMediaTypes(contentType);
			if (list.Count != 1)
			{
				throw new ODataContentTypeException(Strings.HttpUtils_NoOrMoreThanOneContentTypeSpecified(contentType));
			}
			MediaType key = list[0].Key;
			MediaTypeUtils.CheckMediaTypeForWildCards(key);
			mediaTypeName = key.FullTypeName;
			mediaTypeCharset = list[0].Value;
			return key.Parameters;
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x0004F35D File Offset: 0x0004D55D
		internal static string BuildContentType(MediaType mediaType, Encoding encoding)
		{
			return mediaType.ToText(encoding);
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x0004F366 File Offset: 0x0004D566
		internal static IList<KeyValuePair<MediaType, string>> MediaTypesFromString(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			return HttpUtils.ReadMediaTypes(text);
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x0004F378 File Offset: 0x0004D578
		internal static bool CompareMediaTypeNames(string mediaTypeName1, string mediaTypeName2)
		{
			return string.Equals(mediaTypeName1, mediaTypeName2, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x0004F382 File Offset: 0x0004D582
		internal static bool CompareMediaTypeParameterNames(string parameterName1, string parameterName2)
		{
			return string.Equals(parameterName1, parameterName2, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x0004F3A0 File Offset: 0x0004D5A0
		internal static Encoding EncodingFromAcceptableCharsets(string acceptableCharsets, MediaType mediaType, Encoding utf8Encoding, Encoding defaultEncoding)
		{
			Encoding encoding = null;
			if (!string.IsNullOrEmpty(acceptableCharsets))
			{
				HttpUtils.CharsetPart[] array = new List<HttpUtils.CharsetPart>(HttpUtils.AcceptCharsetParts(acceptableCharsets)).ToArray();
				KeyValuePair<int, HttpUtils.CharsetPart>[] array2 = array.StableSort((HttpUtils.CharsetPart x, HttpUtils.CharsetPart y) => y.Quality - x.Quality);
				foreach (KeyValuePair<int, HttpUtils.CharsetPart> keyValuePair in array2)
				{
					HttpUtils.CharsetPart value = keyValuePair.Value;
					if (value.Quality > 0)
					{
						if (string.Compare("utf-8", value.Charset, StringComparison.OrdinalIgnoreCase) == 0)
						{
							encoding = utf8Encoding;
							break;
						}
						encoding = HttpUtils.GetEncodingFromCharsetName(value.Charset);
						if (encoding != null)
						{
							break;
						}
					}
				}
			}
			if (encoding == null)
			{
				encoding = mediaType.SelectEncoding();
				if (encoding == null)
				{
					return defaultEncoding;
				}
			}
			return encoding;
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x0004F460 File Offset: 0x0004D660
		internal static void ReadQualityValue(string text, ref int textIndex, out int qualityValue)
		{
			char c = text[textIndex++];
			switch (c)
			{
			case '0':
				qualityValue = 0;
				break;
			case '1':
				qualityValue = 1;
				break;
			default:
				throw new ODataContentTypeException(Strings.HttpUtils_InvalidQualityValueStartChar(text, c));
			}
			if (textIndex < text.Length && text[textIndex] == '.')
			{
				textIndex++;
				int num = 1000;
				while (num > 1 && textIndex < text.Length)
				{
					char c2 = text[textIndex];
					int num2 = HttpUtils.DigitToInt32(c2);
					if (num2 < 0)
					{
						break;
					}
					textIndex++;
					num /= 10;
					qualityValue *= 10;
					qualityValue += num2;
				}
				qualityValue *= num;
				if (qualityValue > 1000)
				{
					throw new ODataContentTypeException(Strings.HttpUtils_InvalidQualityValue(qualityValue / 1000, text));
				}
			}
			else
			{
				qualityValue *= 1000;
			}
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x0004F544 File Offset: 0x0004D744
		internal static void ValidateHttpMethod(string httpMethodString)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(httpMethodString, "httpMethodString");
			if (string.CompareOrdinal(httpMethodString, "GET") != 0 && string.CompareOrdinal(httpMethodString, "DELETE") != 0 && string.CompareOrdinal(httpMethodString, "MERGE") != 0 && string.CompareOrdinal(httpMethodString, "PATCH") != 0 && string.CompareOrdinal(httpMethodString, "POST") != 0 && string.CompareOrdinal(httpMethodString, "PUT") != 0)
			{
				throw new ODataException(Strings.HttpUtils_InvalidHttpMethodString(httpMethodString));
			}
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x0004F5B6 File Offset: 0x0004D7B6
		internal static bool IsQueryMethod(string httpMethod)
		{
			return string.CompareOrdinal(httpMethod, "GET") == 0;
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x0004F5C8 File Offset: 0x0004D7C8
		internal static string GetStatusMessage(int statusCode)
		{
			if (statusCode <= 206)
			{
				switch (statusCode)
				{
				case 100:
					return "Continue";
				case 101:
					return "Switching Protocols";
				default:
					switch (statusCode)
					{
					case 200:
						return "OK";
					case 201:
						return "Created";
					case 202:
						return "Accepted";
					case 203:
						return "Non-Authoritative Information";
					case 204:
						return "No Content";
					case 205:
						return "Reset Content";
					case 206:
						return "Partial Content";
					}
					break;
				}
			}
			else
			{
				switch (statusCode)
				{
				case 300:
					return "Multiple Choices";
				case 301:
					return "Moved Permanently";
				case 302:
					return "Found";
				case 303:
					return "See Other";
				case 304:
					return "Not Modified";
				case 305:
					return "Use Proxy";
				case 306:
					break;
				case 307:
					return "Temporary Redirect";
				default:
					switch (statusCode)
					{
					case 400:
						return "Bad Request";
					case 401:
						return "Unauthorized";
					case 402:
						return "Payment Required";
					case 403:
						return "Forbidden";
					case 404:
						return "Not Found";
					case 405:
						return "Method Not Allowed";
					case 406:
						return "Not Acceptable";
					case 407:
						return "Proxy Authentication Required";
					case 408:
						return "Request Time-out";
					case 409:
						return "Conflict";
					case 410:
						return "Gone";
					case 411:
						return "Length Required";
					case 412:
						return "Precondition Failed";
					case 413:
						return "Request Entity Too Large";
					case 414:
						return "Request-URI Too Large";
					case 415:
						return "Unsupported Media Type";
					case 416:
						return "Requested range not satisfiable";
					case 417:
						return "Expectation Failed";
					default:
						switch (statusCode)
						{
						case 500:
							return "Internal Server Error";
						case 501:
							return "Not Implemented";
						case 502:
							return "Bad Gateway";
						case 503:
							return "Service Unavailable";
						case 504:
							return "Gateway Time-out";
						case 505:
							return "HTTP Version not supported";
						}
						break;
					}
					break;
				}
			}
			return "Unknown Status Code";
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x0004F7BC File Offset: 0x0004D9BC
		internal static Encoding GetEncodingFromCharsetName(string charsetName)
		{
			Encoding encoding;
			try
			{
				encoding = Encoding.GetEncoding(charsetName, new EncoderExceptionFallback(), new DecoderExceptionFallback());
			}
			catch (ArgumentException)
			{
				encoding = null;
			}
			return encoding;
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x0004F7F4 File Offset: 0x0004D9F4
		internal static string ReadTokenOrQuotedStringValue(string headerName, string headerText, ref int textIndex, out bool isQuotedString, Func<string, Exception> createException)
		{
			StringBuilder stringBuilder = new StringBuilder();
			isQuotedString = false;
			if (textIndex < headerText.Length && headerText[textIndex] == '"')
			{
				textIndex++;
				isQuotedString = true;
			}
			char c = '\0';
			while (textIndex < headerText.Length)
			{
				c = headerText[textIndex];
				if (c == '\\' || c == '"')
				{
					if (!isQuotedString)
					{
						throw createException(Strings.HttpUtils_EscapeCharWithoutQuotes(headerName, headerText, textIndex, c));
					}
					textIndex++;
					if (c == '"')
					{
						break;
					}
					if (textIndex >= headerText.Length)
					{
						throw createException(Strings.HttpUtils_EscapeCharAtEnd(headerName, headerText, textIndex, c));
					}
					c = headerText[textIndex];
				}
				else
				{
					if (!isQuotedString && !HttpUtils.IsHttpToken(c))
					{
						break;
					}
					if (isQuotedString && !HttpUtils.IsValidInQuotedHeaderValue(c))
					{
						throw createException(Strings.HttpUtils_InvalidCharacterInQuotedParameterValue(headerName, headerText, textIndex, c));
					}
				}
				stringBuilder.Append(c);
				textIndex++;
			}
			if (isQuotedString && c != '"')
			{
				throw createException(Strings.HttpUtils_ClosingQuoteNotFound(headerName, headerText, textIndex));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x0004F916 File Offset: 0x0004DB16
		internal static bool SkipWhitespace(string text, ref int textIndex)
		{
			while (textIndex < text.Length && char.IsWhiteSpace(text, textIndex))
			{
				textIndex++;
			}
			return textIndex == text.Length;
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x0004FBF0 File Offset: 0x0004DDF0
		private static IEnumerable<HttpUtils.CharsetPart> AcceptCharsetParts(string headerValue)
		{
			bool commaRequired = false;
			int headerIndex = 0;
			while (headerIndex < headerValue.Length && !HttpUtils.SkipWhitespace(headerValue, ref headerIndex))
			{
				if (headerValue[headerIndex] == ',')
				{
					commaRequired = false;
					headerIndex++;
				}
				else
				{
					if (commaRequired)
					{
						throw new ODataContentTypeException(Strings.HttpUtils_MissingSeparatorBetweenCharsets(headerValue));
					}
					int headerStart = headerIndex;
					int headerNameEnd = headerStart;
					bool endReached = HttpUtils.ReadToken(headerValue, ref headerNameEnd);
					if (headerNameEnd == headerIndex)
					{
						throw new ODataContentTypeException(Strings.HttpUtils_InvalidCharsetName(headerValue));
					}
					int qualityValue;
					int headerEnd;
					if (endReached)
					{
						qualityValue = 1000;
						headerEnd = headerNameEnd;
					}
					else
					{
						char c = headerValue[headerNameEnd];
						if (!HttpUtils.IsHttpSeparator(c))
						{
							throw new ODataContentTypeException(Strings.HttpUtils_InvalidSeparatorBetweenCharsets(headerValue));
						}
						if (c == ';')
						{
							if (HttpUtils.ReadLiteral(headerValue, headerNameEnd, ";q="))
							{
								throw new ODataContentTypeException(Strings.HttpUtils_UnexpectedEndOfQValue(headerValue));
							}
							headerEnd = headerNameEnd + 3;
							HttpUtils.ReadQualityValue(headerValue, ref headerEnd, out qualityValue);
						}
						else
						{
							qualityValue = 1000;
							headerEnd = headerNameEnd;
						}
					}
					yield return new HttpUtils.CharsetPart(headerValue.Substring(headerStart, headerNameEnd - headerStart), qualityValue);
					commaRequired = true;
					headerIndex = headerEnd;
				}
			}
			yield break;
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x0004FC10 File Offset: 0x0004DE10
		private static IList<KeyValuePair<MediaType, string>> ReadMediaTypes(string text)
		{
			List<KeyValuePair<string, string>> list = null;
			List<KeyValuePair<MediaType, string>> list2 = new List<KeyValuePair<MediaType, string>>();
			int num = 0;
			while (!HttpUtils.SkipWhitespace(text, ref num))
			{
				string text2;
				string text3;
				HttpUtils.ReadMediaTypeAndSubtype(text, ref num, out text2, out text3);
				string text4 = null;
				while (!HttpUtils.SkipWhitespace(text, ref num))
				{
					if (text[num] == ',')
					{
						num++;
						break;
					}
					if (text[num] != ';')
					{
						throw new ODataContentTypeException(Strings.HttpUtils_MediaTypeRequiresSemicolonBeforeParameter(text));
					}
					num++;
					if (HttpUtils.SkipWhitespace(text, ref num))
					{
						break;
					}
					HttpUtils.ReadMediaTypeParameter(text, ref num, ref list, ref text4);
				}
				list2.Add(new KeyValuePair<MediaType, string>(new MediaType(text2, text3, (list == null) ? null : list.ToArray()), text4));
				list = null;
			}
			return list2;
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x0004FCC4 File Offset: 0x0004DEC4
		private static void ReadMediaTypeParameter(string text, ref int textIndex, ref List<KeyValuePair<string, string>> parameters, ref string charset)
		{
			int num = textIndex;
			bool flag = HttpUtils.ReadToken(text, ref textIndex);
			string text2 = text.Substring(num, textIndex - num);
			if (text2.Length == 0)
			{
				throw new ODataContentTypeException(Strings.HttpUtils_MediaTypeMissingParameterName);
			}
			if (flag)
			{
				throw new ODataContentTypeException(Strings.HttpUtils_MediaTypeMissingParameterValue(text2));
			}
			if (text[textIndex] != '=')
			{
				throw new ODataContentTypeException(Strings.HttpUtils_MediaTypeMissingParameterValue(text2));
			}
			textIndex++;
			bool flag2;
			string text3 = HttpUtils.ReadTokenOrQuotedStringValue("Content-Type", text, ref textIndex, out flag2, (string message) => new ODataContentTypeException(message));
			if (HttpUtils.CompareMediaTypeParameterNames("charset", text2))
			{
				charset = text3;
				return;
			}
			if (parameters == null)
			{
				parameters = new List<KeyValuePair<string, string>>(1);
			}
			parameters.Add(new KeyValuePair<string, string>(text2, text3));
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x0004FD84 File Offset: 0x0004DF84
		private static void ReadMediaTypeAndSubtype(string mediaTypeName, ref int textIndex, out string type, out string subType)
		{
			int num = textIndex;
			if (HttpUtils.ReadToken(mediaTypeName, ref textIndex))
			{
				throw new ODataContentTypeException(Strings.HttpUtils_MediaTypeUnspecified(mediaTypeName));
			}
			if (mediaTypeName[textIndex] != '/')
			{
				throw new ODataContentTypeException(Strings.HttpUtils_MediaTypeRequiresSlash(mediaTypeName));
			}
			type = mediaTypeName.Substring(num, textIndex - num);
			textIndex++;
			int num2 = textIndex;
			HttpUtils.ReadToken(mediaTypeName, ref textIndex);
			if (textIndex == num2)
			{
				throw new ODataContentTypeException(Strings.HttpUtils_MediaTypeRequiresSubType(mediaTypeName));
			}
			subType = mediaTypeName.Substring(num2, textIndex - num2);
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x0004FDFD File Offset: 0x0004DFFD
		private static bool IsHttpToken(char c)
		{
			return c < '\u007f' && c > '\u001f' && !HttpUtils.IsHttpSeparator(c);
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x0004FE14 File Offset: 0x0004E014
		private static bool IsValidInQuotedHeaderValue(char c)
		{
			return (c >= ' ' || c == ' ' || c == '\t') && c != '\u007f';
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x0004FE3C File Offset: 0x0004E03C
		private static bool IsHttpSeparator(char c)
		{
			return c == '(' || c == ')' || c == '<' || c == '>' || c == '@' || c == ',' || c == ';' || c == ':' || c == '\\' || c == '"' || c == '/' || c == '[' || c == ']' || c == '?' || c == '=' || c == '{' || c == '}' || c == ' ' || c == '\t';
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x0004FEAA File Offset: 0x0004E0AA
		private static bool ReadToken(string text, ref int textIndex)
		{
			while (textIndex < text.Length && HttpUtils.IsHttpToken(text[textIndex]))
			{
				textIndex++;
			}
			return textIndex == text.Length;
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x0004FED7 File Offset: 0x0004E0D7
		private static int DigitToInt32(char c)
		{
			if (c >= '0' && c <= '9')
			{
				return (int)(c - '0');
			}
			if (HttpUtils.IsHttpElementSeparator(c))
			{
				return -1;
			}
			throw new ODataException(Strings.HttpUtils_CannotConvertCharToInt(c));
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x0004FF02 File Offset: 0x0004E102
		private static bool IsHttpElementSeparator(char c)
		{
			return c == ',' || c == ' ' || c == '\t';
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x0004FF15 File Offset: 0x0004E115
		private static bool ReadLiteral(string text, int textIndex, string literal)
		{
			if (string.Compare(text, textIndex, literal, 0, literal.Length, StringComparison.Ordinal) != 0)
			{
				throw new ODataException(Strings.HttpUtils_ExpectedLiteralNotFoundInString(literal, textIndex, text));
			}
			return textIndex + literal.Length == text.Length;
		}

		// Token: 0x0200028C RID: 652
		private struct CharsetPart
		{
			// Token: 0x060015BC RID: 5564 RVA: 0x0004FF4C File Offset: 0x0004E14C
			internal CharsetPart(string charset, int quality)
			{
				this.Charset = charset;
				this.Quality = quality;
			}

			// Token: 0x04000855 RID: 2133
			internal readonly string Charset;

			// Token: 0x04000856 RID: 2134
			internal readonly int Quality;
		}
	}
}
