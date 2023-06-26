using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net.Configuration;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Text;

namespace System.Net
{
	/// <summary>Provides methods for encoding and decoding URLs when processing Web requests.</summary>
	// Token: 0x0200018C RID: 396
	[global::__DynamicallyInvokable]
	public static class WebUtility
	{
		/// <summary>Converts a string to an HTML-encoded string.</summary>
		/// <param name="value">The string to encode.</param>
		/// <returns>An encoded string.</returns>
		// Token: 0x06000F27 RID: 3879 RVA: 0x0004E6D0 File Offset: 0x0004C8D0
		[global::__DynamicallyInvokable]
		public static string HtmlEncode(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}
			int num = WebUtility.IndexOfHtmlEncodingChars(value, 0);
			if (num == -1)
			{
				return value;
			}
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			WebUtility.HtmlEncode(value, stringWriter);
			return stringWriter.ToString();
		}

		/// <summary>Converts a string into an HTML-encoded string, and returns the output as a <see cref="T:System.IO.TextWriter" /> stream of output.</summary>
		/// <param name="value">The string to encode.</param>
		/// <param name="output">A <see cref="T:System.IO.TextWriter" /> output stream.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="output" /> parameter cannot be <see langword="null" /> if the <paramref name="value" /> parameter is not <see langword="null" />.</exception>
		// Token: 0x06000F28 RID: 3880 RVA: 0x0004E710 File Offset: 0x0004C910
		public unsafe static void HtmlEncode(string value, TextWriter output)
		{
			if (value == null)
			{
				return;
			}
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			int num = WebUtility.IndexOfHtmlEncodingChars(value, 0);
			if (num == -1)
			{
				output.Write(value);
				return;
			}
			UnicodeEncodingConformance htmlEncodeConformance = WebUtility.HtmlEncodeConformance;
			int i = value.Length - num;
			fixed (string text = value)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* ptr2 = ptr;
				while (num-- > 0)
				{
					output.Write(*(ptr2++));
				}
				while (i > 0)
				{
					char c = *ptr2;
					if (c <= '>')
					{
						if (c <= '&')
						{
							if (c == '"')
							{
								output.Write("&quot;");
								goto IL_172;
							}
							if (c == '&')
							{
								output.Write("&amp;");
								goto IL_172;
							}
						}
						else
						{
							if (c == '\'')
							{
								output.Write("&#39;");
								goto IL_172;
							}
							if (c == '<')
							{
								output.Write("&lt;");
								goto IL_172;
							}
							if (c == '>')
							{
								output.Write("&gt;");
								goto IL_172;
							}
						}
						output.Write(c);
					}
					else
					{
						int num2 = -1;
						if (c >= '\u00a0' && c < 'Ā')
						{
							num2 = (int)c;
						}
						else if (htmlEncodeConformance == UnicodeEncodingConformance.Strict && char.IsSurrogate(c))
						{
							int nextUnicodeScalarValueFromUtf16Surrogate = WebUtility.GetNextUnicodeScalarValueFromUtf16Surrogate(ref ptr2, ref i);
							if (nextUnicodeScalarValueFromUtf16Surrogate >= 65536)
							{
								num2 = nextUnicodeScalarValueFromUtf16Surrogate;
							}
							else
							{
								c = (char)nextUnicodeScalarValueFromUtf16Surrogate;
							}
						}
						if (num2 >= 0)
						{
							output.Write("&#");
							output.Write(num2.ToString(NumberFormatInfo.InvariantInfo));
							output.Write(';');
						}
						else
						{
							output.Write(c);
						}
					}
					IL_172:
					i--;
					ptr2++;
				}
			}
		}

		/// <summary>Converts a string that has been HTML-encoded for HTTP transmission into a decoded string.</summary>
		/// <param name="value">The string to decode.</param>
		/// <returns>A decoded string.</returns>
		// Token: 0x06000F29 RID: 3881 RVA: 0x0004E8A4 File Offset: 0x0004CAA4
		[global::__DynamicallyInvokable]
		public static string HtmlDecode(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}
			if (!WebUtility.StringRequiresHtmlDecoding(value))
			{
				return value;
			}
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			WebUtility.HtmlDecode(value, stringWriter);
			return stringWriter.ToString();
		}

		/// <summary>Converts a string that has been HTML-encoded into a decoded string, and sends the decoded string to a <see cref="T:System.IO.TextWriter" /> output stream.</summary>
		/// <param name="value">The string to decode.</param>
		/// <param name="output">A <see cref="T:System.IO.TextWriter" /> stream of output.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="output" /> parameter cannot be <see langword="null" /> if the <paramref name="value" /> parameter is not <see langword="null" />.</exception>
		// Token: 0x06000F2A RID: 3882 RVA: 0x0004E8E0 File Offset: 0x0004CAE0
		public static void HtmlDecode(string value, TextWriter output)
		{
			if (value == null)
			{
				return;
			}
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (!WebUtility.StringRequiresHtmlDecoding(value))
			{
				output.Write(value);
				return;
			}
			UnicodeDecodingConformance htmlDecodeConformance = WebUtility.HtmlDecodeConformance;
			int length = value.Length;
			int i = 0;
			while (i < length)
			{
				char c = value[i];
				if (c != '&')
				{
					goto IL_1BA;
				}
				int num = value.IndexOfAny(WebUtility._htmlEntityEndingChars, i + 1);
				if (num <= 0 || value[num] != ';')
				{
					goto IL_1BA;
				}
				string text = value.Substring(i + 1, num - i - 1);
				if (text.Length > 1 && text[0] == '#')
				{
					uint num2;
					bool flag;
					if (text[1] == 'x' || text[1] == 'X')
					{
						flag = uint.TryParse(text.Substring(2), NumberStyles.AllowHexSpecifier, NumberFormatInfo.InvariantInfo, out num2);
					}
					else
					{
						flag = uint.TryParse(text.Substring(1), NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out num2);
					}
					if (flag)
					{
						switch (htmlDecodeConformance)
						{
						case UnicodeDecodingConformance.Strict:
							flag = num2 < 55296U || (57343U < num2 && num2 <= 1114111U);
							break;
						case UnicodeDecodingConformance.Compat:
							flag = 0U < num2 && num2 <= 65535U;
							break;
						case UnicodeDecodingConformance.Loose:
							flag = num2 <= 1114111U;
							break;
						default:
							flag = false;
							break;
						}
					}
					if (!flag)
					{
						goto IL_1BA;
					}
					if (num2 <= 65535U)
					{
						output.Write((char)num2);
					}
					else
					{
						char c2;
						char c3;
						WebUtility.ConvertSmpToUtf16(num2, out c2, out c3);
						output.Write(c2);
						output.Write(c3);
					}
					i = num;
				}
				else
				{
					i = num;
					char c4 = WebUtility.HtmlEntities.Lookup(text);
					if (c4 != '\0')
					{
						c = c4;
						goto IL_1BA;
					}
					output.Write('&');
					output.Write(text);
					output.Write(';');
				}
				IL_1C1:
				i++;
				continue;
				IL_1BA:
				output.Write(c);
				goto IL_1C1;
			}
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x0004EABC File Offset: 0x0004CCBC
		private unsafe static int IndexOfHtmlEncodingChars(string s, int startPos)
		{
			UnicodeEncodingConformance htmlEncodeConformance = WebUtility.HtmlEncodeConformance;
			int i = s.Length - startPos;
			fixed (string text = s)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* ptr2 = ptr + startPos;
				while (i > 0)
				{
					char c = *ptr2;
					if (c <= '>')
					{
						if (c <= '&')
						{
							if (c != '"' && c != '&')
							{
								goto IL_95;
							}
						}
						else if (c != '\'' && c != '<' && c != '>')
						{
							goto IL_95;
						}
						return s.Length - i;
					}
					if (c >= '\u00a0' && c < 'Ā')
					{
						return s.Length - i;
					}
					if (htmlEncodeConformance == UnicodeEncodingConformance.Strict && char.IsSurrogate(c))
					{
						return s.Length - i;
					}
					IL_95:
					ptr2++;
					i--;
				}
			}
			return -1;
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000F2C RID: 3884 RVA: 0x0004EB70 File Offset: 0x0004CD70
		private static UnicodeDecodingConformance HtmlDecodeConformance
		{
			get
			{
				if (WebUtility._htmlDecodeConformance != UnicodeDecodingConformance.Auto)
				{
					return WebUtility._htmlDecodeConformance;
				}
				UnicodeDecodingConformance unicodeDecodingConformance = (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5 ? UnicodeDecodingConformance.Strict : UnicodeDecodingConformance.Compat);
				UnicodeDecodingConformance unicodeDecodingConformance2 = unicodeDecodingConformance;
				try
				{
					unicodeDecodingConformance2 = SettingsSectionInternal.Section.WebUtilityUnicodeDecodingConformance;
					if (unicodeDecodingConformance2 <= UnicodeDecodingConformance.Auto || unicodeDecodingConformance2 > UnicodeDecodingConformance.Loose)
					{
						unicodeDecodingConformance2 = unicodeDecodingConformance;
					}
				}
				catch (ConfigurationException)
				{
					unicodeDecodingConformance2 = unicodeDecodingConformance;
				}
				catch
				{
					return unicodeDecodingConformance;
				}
				WebUtility._htmlDecodeConformance = unicodeDecodingConformance2;
				return WebUtility._htmlDecodeConformance;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000F2D RID: 3885 RVA: 0x0004EBEC File Offset: 0x0004CDEC
		private static UnicodeEncodingConformance HtmlEncodeConformance
		{
			get
			{
				if (WebUtility._htmlEncodeConformance != UnicodeEncodingConformance.Auto)
				{
					return WebUtility._htmlEncodeConformance;
				}
				UnicodeEncodingConformance unicodeEncodingConformance = (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5 ? UnicodeEncodingConformance.Strict : UnicodeEncodingConformance.Compat);
				UnicodeEncodingConformance unicodeEncodingConformance2 = unicodeEncodingConformance;
				try
				{
					unicodeEncodingConformance2 = SettingsSectionInternal.Section.WebUtilityUnicodeEncodingConformance;
					if (unicodeEncodingConformance2 <= UnicodeEncodingConformance.Auto || unicodeEncodingConformance2 > UnicodeEncodingConformance.Compat)
					{
						unicodeEncodingConformance2 = unicodeEncodingConformance;
					}
				}
				catch (ConfigurationException)
				{
					unicodeEncodingConformance2 = unicodeEncodingConformance;
				}
				catch
				{
					return unicodeEncodingConformance;
				}
				WebUtility._htmlEncodeConformance = unicodeEncodingConformance2;
				return WebUtility._htmlEncodeConformance;
			}
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x0004EC68 File Offset: 0x0004CE68
		private static byte[] UrlEncode(byte[] bytes, int offset, int count, bool alwaysCreateNewReturnValue)
		{
			byte[] array = WebUtility.UrlEncode(bytes, offset, count);
			if (!alwaysCreateNewReturnValue || array == null || array != bytes)
			{
				return array;
			}
			return (byte[])array.Clone();
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x0004EC98 File Offset: 0x0004CE98
		private static byte[] UrlEncode(byte[] bytes, int offset, int count)
		{
			if (!WebUtility.ValidateUrlEncodingParameters(bytes, offset, count))
			{
				return null;
			}
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < count; i++)
			{
				char c = (char)bytes[offset + i];
				if (c == ' ')
				{
					num++;
				}
				else if (!WebUtility.IsUrlSafeChar(c))
				{
					num2++;
				}
			}
			if (num != 0 || num2 != 0)
			{
				byte[] array = new byte[count + num2 * 2];
				int num3 = 0;
				for (int j = 0; j < count; j++)
				{
					byte b = bytes[offset + j];
					char c2 = (char)b;
					if (WebUtility.IsUrlSafeChar(c2))
					{
						array[num3++] = b;
					}
					else if (c2 == ' ')
					{
						array[num3++] = 43;
					}
					else
					{
						array[num3++] = 37;
						array[num3++] = (byte)WebUtility.IntToHex((b >> 4) & 15);
						array[num3++] = (byte)WebUtility.IntToHex((int)(b & 15));
					}
				}
				return array;
			}
			if (offset == 0 && bytes.Length == count)
			{
				return bytes;
			}
			byte[] array2 = new byte[count];
			Buffer.BlockCopy(bytes, offset, array2, 0, count);
			return array2;
		}

		/// <summary>Converts a text string into a URL-encoded string.</summary>
		/// <param name="value">The text to URL-encode.</param>
		/// <returns>A URL-encoded string.</returns>
		// Token: 0x06000F30 RID: 3888 RVA: 0x0004ED8C File Offset: 0x0004CF8C
		[global::__DynamicallyInvokable]
		public static string UrlEncode(string value)
		{
			if (value == null)
			{
				return null;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(value);
			return Encoding.UTF8.GetString(WebUtility.UrlEncode(bytes, 0, bytes.Length, false));
		}

		/// <summary>Converts a byte array into a URL-encoded byte array.</summary>
		/// <param name="value">The <see cref="T:System.Byte" /> array to URL-encode.</param>
		/// <param name="offset">The offset, in bytes, from the start of the <see cref="T:System.Byte" /> array to encode.</param>
		/// <param name="count">The count, in bytes, to encode from the <see cref="T:System.Byte" /> array.</param>
		/// <returns>An encoded <see cref="T:System.Byte" /> array.</returns>
		// Token: 0x06000F31 RID: 3889 RVA: 0x0004EDBF File Offset: 0x0004CFBF
		[global::__DynamicallyInvokable]
		public static byte[] UrlEncodeToBytes(byte[] value, int offset, int count)
		{
			return WebUtility.UrlEncode(value, offset, count, true);
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x0004EDCC File Offset: 0x0004CFCC
		private static string UrlDecodeInternal(string value, Encoding encoding)
		{
			if (value == null)
			{
				return null;
			}
			int length = value.Length;
			WebUtility.UrlDecoder urlDecoder = new WebUtility.UrlDecoder(length, encoding);
			int i = 0;
			while (i < length)
			{
				char c = value[i];
				if (c == '+')
				{
					c = ' ';
					goto IL_77;
				}
				if (c != '%' || i >= length - 2)
				{
					goto IL_77;
				}
				int num = WebUtility.HexToInt(value[i + 1]);
				int num2 = WebUtility.HexToInt(value[i + 2]);
				if (num < 0 || num2 < 0)
				{
					goto IL_77;
				}
				byte b = (byte)((num << 4) | num2);
				i += 2;
				urlDecoder.AddByte(b);
				IL_91:
				i++;
				continue;
				IL_77:
				if ((c & 'ﾀ') == '\0')
				{
					urlDecoder.AddByte((byte)c);
					goto IL_91;
				}
				urlDecoder.AddChar(c);
				goto IL_91;
			}
			return urlDecoder.GetString();
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x0004EE7C File Offset: 0x0004D07C
		private static byte[] UrlDecodeInternal(byte[] bytes, int offset, int count)
		{
			if (!WebUtility.ValidateUrlEncodingParameters(bytes, offset, count))
			{
				return null;
			}
			int num = 0;
			byte[] array = new byte[count];
			for (int i = 0; i < count; i++)
			{
				int num2 = offset + i;
				byte b = bytes[num2];
				if (b == 43)
				{
					b = 32;
				}
				else if (b == 37 && i < count - 2)
				{
					int num3 = WebUtility.HexToInt((char)bytes[num2 + 1]);
					int num4 = WebUtility.HexToInt((char)bytes[num2 + 2]);
					if (num3 >= 0 && num4 >= 0)
					{
						b = (byte)((num3 << 4) | num4);
						i += 2;
					}
				}
				array[num++] = b;
			}
			if (num < array.Length)
			{
				byte[] array2 = new byte[num];
				Array.Copy(array, array2, num);
				array = array2;
			}
			return array;
		}

		/// <summary>Converts a string that has been encoded for transmission in a URL into a decoded string.</summary>
		/// <param name="encodedValue">A URL-encoded string to decode.</param>
		/// <returns>A decoded string.</returns>
		// Token: 0x06000F34 RID: 3892 RVA: 0x0004EF1F File Offset: 0x0004D11F
		[global::__DynamicallyInvokable]
		public static string UrlDecode(string encodedValue)
		{
			if (encodedValue == null)
			{
				return null;
			}
			return WebUtility.UrlDecodeInternal(encodedValue, Encoding.UTF8);
		}

		/// <summary>Converts an encoded byte array that has been encoded for transmission in a URL into a decoded byte array.</summary>
		/// <param name="encodedValue">A URL-encoded <see cref="T:System.Byte" /> array to decode.</param>
		/// <param name="offset">The offset, in bytes, from the start of the <see cref="T:System.Byte" /> array to decode.</param>
		/// <param name="count">The count, in bytes, to decode from the <see cref="T:System.Byte" /> array.</param>
		/// <returns>A decoded <see cref="T:System.Byte" /> array.</returns>
		// Token: 0x06000F35 RID: 3893 RVA: 0x0004EF31 File Offset: 0x0004D131
		[global::__DynamicallyInvokable]
		public static byte[] UrlDecodeToBytes(byte[] encodedValue, int offset, int count)
		{
			return WebUtility.UrlDecodeInternal(encodedValue, offset, count);
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x0004EF3C File Offset: 0x0004D13C
		private static void ConvertSmpToUtf16(uint smpChar, out char leadingSurrogate, out char trailingSurrogate)
		{
			int num = (int)(smpChar - 65536U);
			leadingSurrogate = (char)(num / 1024 + 55296);
			trailingSurrogate = (char)(num % 1024 + 56320);
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x0004EF74 File Offset: 0x0004D174
		private unsafe static int GetNextUnicodeScalarValueFromUtf16Surrogate(ref char* pch, ref int charsRemaining)
		{
			if (charsRemaining <= 1)
			{
				return 65533;
			}
			char c = (char)(*pch);
			char c2 = (char)(*(pch + 2));
			if (char.IsSurrogatePair(c, c2))
			{
				pch += 2;
				charsRemaining--;
				return (int)((c - '\ud800') * 'Ѐ' + (c2 - '\udc00')) + 65536;
			}
			return 65533;
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x0004EFCC File Offset: 0x0004D1CC
		private static int HexToInt(char h)
		{
			if (h >= '0' && h <= '9')
			{
				return (int)(h - '0');
			}
			if (h >= 'a' && h <= 'f')
			{
				return (int)(h - 'a' + '\n');
			}
			if (h < 'A' || h > 'F')
			{
				return -1;
			}
			return (int)(h - 'A' + '\n');
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x0004F002 File Offset: 0x0004D202
		private static char IntToHex(int n)
		{
			if (n <= 9)
			{
				return (char)(n + 48);
			}
			return (char)(n - 10 + 65);
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x0004F018 File Offset: 0x0004D218
		private static bool IsUrlSafeChar(char ch)
		{
			if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || (ch >= '0' && ch <= '9'))
			{
				return true;
			}
			if (ch != '!')
			{
				switch (ch)
				{
				case '(':
				case ')':
				case '*':
				case '-':
				case '.':
					return true;
				case '+':
				case ',':
					break;
				default:
					if (ch == '_')
					{
						return true;
					}
					break;
				}
				return false;
			}
			return true;
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x0004F078 File Offset: 0x0004D278
		private static bool ValidateUrlEncodingParameters(byte[] bytes, int offset, int count)
		{
			if (bytes == null && count == 0)
			{
				return false;
			}
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (offset < 0 || offset > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || offset + count > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			return true;
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x0004F0C8 File Offset: 0x0004D2C8
		private static bool StringRequiresHtmlDecoding(string s)
		{
			if (WebUtility.HtmlDecodeConformance == UnicodeDecodingConformance.Compat)
			{
				return s.IndexOf('&') >= 0;
			}
			foreach (char c in s)
			{
				if (c == '&' || char.IsSurrogate(c))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400128E RID: 4750
		private const char HIGH_SURROGATE_START = '\ud800';

		// Token: 0x0400128F RID: 4751
		private const char LOW_SURROGATE_START = '\udc00';

		// Token: 0x04001290 RID: 4752
		private const char LOW_SURROGATE_END = '\udfff';

		// Token: 0x04001291 RID: 4753
		private const int UNICODE_PLANE00_END = 65535;

		// Token: 0x04001292 RID: 4754
		private const int UNICODE_PLANE01_START = 65536;

		// Token: 0x04001293 RID: 4755
		private const int UNICODE_PLANE16_END = 1114111;

		// Token: 0x04001294 RID: 4756
		private const int UnicodeReplacementChar = 65533;

		// Token: 0x04001295 RID: 4757
		private static readonly char[] _htmlEntityEndingChars = new char[] { ';', '&' };

		// Token: 0x04001296 RID: 4758
		private static volatile UnicodeDecodingConformance _htmlDecodeConformance = UnicodeDecodingConformance.Auto;

		// Token: 0x04001297 RID: 4759
		private static volatile UnicodeEncodingConformance _htmlEncodeConformance = UnicodeEncodingConformance.Auto;

		// Token: 0x0200073C RID: 1852
		private class UrlDecoder
		{
			// Token: 0x060041A1 RID: 16801 RVA: 0x0011042C File Offset: 0x0010E62C
			private void FlushBytes()
			{
				if (this._numBytes > 0)
				{
					this._numChars += this._encoding.GetChars(this._byteBuffer, 0, this._numBytes, this._charBuffer, this._numChars);
					this._numBytes = 0;
				}
			}

			// Token: 0x060041A2 RID: 16802 RVA: 0x0011047A File Offset: 0x0010E67A
			internal UrlDecoder(int bufferSize, Encoding encoding)
			{
				this._bufferSize = bufferSize;
				this._encoding = encoding;
				this._charBuffer = new char[bufferSize];
			}

			// Token: 0x060041A3 RID: 16803 RVA: 0x0011049C File Offset: 0x0010E69C
			internal void AddChar(char ch)
			{
				if (this._numBytes > 0)
				{
					this.FlushBytes();
				}
				char[] charBuffer = this._charBuffer;
				int numChars = this._numChars;
				this._numChars = numChars + 1;
				charBuffer[numChars] = ch;
			}

			// Token: 0x060041A4 RID: 16804 RVA: 0x001104D4 File Offset: 0x0010E6D4
			internal void AddByte(byte b)
			{
				if (this._byteBuffer == null)
				{
					this._byteBuffer = new byte[this._bufferSize];
				}
				byte[] byteBuffer = this._byteBuffer;
				int numBytes = this._numBytes;
				this._numBytes = numBytes + 1;
				byteBuffer[numBytes] = b;
			}

			// Token: 0x060041A5 RID: 16805 RVA: 0x00110513 File Offset: 0x0010E713
			internal string GetString()
			{
				if (this._numBytes > 0)
				{
					this.FlushBytes();
				}
				if (this._numChars > 0)
				{
					return new string(this._charBuffer, 0, this._numChars);
				}
				return string.Empty;
			}

			// Token: 0x0400319A RID: 12698
			private int _bufferSize;

			// Token: 0x0400319B RID: 12699
			private int _numChars;

			// Token: 0x0400319C RID: 12700
			private char[] _charBuffer;

			// Token: 0x0400319D RID: 12701
			private int _numBytes;

			// Token: 0x0400319E RID: 12702
			private byte[] _byteBuffer;

			// Token: 0x0400319F RID: 12703
			private Encoding _encoding;
		}

		// Token: 0x0200073D RID: 1853
		private static class HtmlEntities
		{
			// Token: 0x060041A6 RID: 16806 RVA: 0x00110548 File Offset: 0x0010E748
			private static Dictionary<string, char> GenerateLookupTable()
			{
				Dictionary<string, char> dictionary = new Dictionary<string, char>(StringComparer.Ordinal);
				foreach (string text in WebUtility.HtmlEntities._entitiesList)
				{
					dictionary.Add(text.Substring(2), text[0]);
				}
				return dictionary;
			}

			// Token: 0x060041A7 RID: 16807 RVA: 0x00110590 File Offset: 0x0010E790
			public static char Lookup(string entity)
			{
				char c;
				WebUtility.HtmlEntities._lookupTable.TryGetValue(entity, out c);
				return c;
			}

			// Token: 0x040031A0 RID: 12704
			private static string[] _entitiesList = new string[]
			{
				"\"-quot", "&-amp", "'-apos", "<-lt", ">-gt", "\u00a0-nbsp", "¡-iexcl", "¢-cent", "£-pound", "¤-curren",
				"¥-yen", "¦-brvbar", "§-sect", "\u00a8-uml", "©-copy", "ª-ordf", "«-laquo", "¬-not", "­-shy", "®-reg",
				"\u00af-macr", "°-deg", "±-plusmn", "²-sup2", "³-sup3", "\u00b4-acute", "µ-micro", "¶-para", "·-middot", "\u00b8-cedil",
				"¹-sup1", "º-ordm", "»-raquo", "¼-frac14", "½-frac12", "¾-frac34", "¿-iquest", "À-Agrave", "Á-Aacute", "Â-Acirc",
				"Ã-Atilde", "Ä-Auml", "Å-Aring", "Æ-AElig", "Ç-Ccedil", "È-Egrave", "É-Eacute", "Ê-Ecirc", "Ë-Euml", "Ì-Igrave",
				"Í-Iacute", "Î-Icirc", "Ï-Iuml", "Ð-ETH", "Ñ-Ntilde", "Ò-Ograve", "Ó-Oacute", "Ô-Ocirc", "Õ-Otilde", "Ö-Ouml",
				"×-times", "Ø-Oslash", "Ù-Ugrave", "Ú-Uacute", "Û-Ucirc", "Ü-Uuml", "Ý-Yacute", "Þ-THORN", "ß-szlig", "à-agrave",
				"á-aacute", "â-acirc", "ã-atilde", "ä-auml", "å-aring", "æ-aelig", "ç-ccedil", "è-egrave", "é-eacute", "ê-ecirc",
				"ë-euml", "ì-igrave", "í-iacute", "î-icirc", "ï-iuml", "ð-eth", "ñ-ntilde", "ò-ograve", "ó-oacute", "ô-ocirc",
				"õ-otilde", "ö-ouml", "÷-divide", "ø-oslash", "ù-ugrave", "ú-uacute", "û-ucirc", "ü-uuml", "ý-yacute", "þ-thorn",
				"ÿ-yuml", "Œ-OElig", "œ-oelig", "Š-Scaron", "š-scaron", "Ÿ-Yuml", "ƒ-fnof", "ˆ-circ", "\u02dc-tilde", "Α-Alpha",
				"Β-Beta", "Γ-Gamma", "Δ-Delta", "Ε-Epsilon", "Ζ-Zeta", "Η-Eta", "Θ-Theta", "Ι-Iota", "Κ-Kappa", "Λ-Lambda",
				"Μ-Mu", "Ν-Nu", "Ξ-Xi", "Ο-Omicron", "Π-Pi", "Ρ-Rho", "Σ-Sigma", "Τ-Tau", "Υ-Upsilon", "Φ-Phi",
				"Χ-Chi", "Ψ-Psi", "Ω-Omega", "α-alpha", "β-beta", "γ-gamma", "δ-delta", "ε-epsilon", "ζ-zeta", "η-eta",
				"θ-theta", "ι-iota", "κ-kappa", "λ-lambda", "μ-mu", "ν-nu", "ξ-xi", "ο-omicron", "π-pi", "ρ-rho",
				"ς-sigmaf", "σ-sigma", "τ-tau", "υ-upsilon", "φ-phi", "χ-chi", "ψ-psi", "ω-omega", "ϑ-thetasym", "ϒ-upsih",
				"ϖ-piv", "\u2002-ensp", "\u2003-emsp", "\u2009-thinsp", "\u200c-zwnj", "\u200d-zwj", "\u200e-lrm", "\u200f-rlm", "–-ndash", "—-mdash",
				"‘-lsquo", "’-rsquo", "‚-sbquo", "“-ldquo", "”-rdquo", "„-bdquo", "†-dagger", "‡-Dagger", "•-bull", "…-hellip",
				"‰-permil", "′-prime", "″-Prime", "‹-lsaquo", "›-rsaquo", "‾-oline", "⁄-frasl", "€-euro", "ℑ-image", "℘-weierp",
				"ℜ-real", "™-trade", "ℵ-alefsym", "←-larr", "↑-uarr", "→-rarr", "↓-darr", "↔-harr", "↵-crarr", "⇐-lArr",
				"⇑-uArr", "⇒-rArr", "⇓-dArr", "⇔-hArr", "∀-forall", "∂-part", "∃-exist", "∅-empty", "∇-nabla", "∈-isin",
				"∉-notin", "∋-ni", "∏-prod", "∑-sum", "−-minus", "∗-lowast", "√-radic", "∝-prop", "∞-infin", "∠-ang",
				"∧-and", "∨-or", "∩-cap", "∪-cup", "∫-int", "∴-there4", "∼-sim", "≅-cong", "≈-asymp", "≠-ne",
				"≡-equiv", "≤-le", "≥-ge", "⊂-sub", "⊃-sup", "⊄-nsub", "⊆-sube", "⊇-supe", "⊕-oplus", "⊗-otimes",
				"⊥-perp", "⋅-sdot", "⌈-lceil", "⌉-rceil", "⌊-lfloor", "⌋-rfloor", "〈-lang", "〉-rang", "◊-loz", "♠-spades",
				"♣-clubs", "♥-hearts", "♦-diams"
			};

			// Token: 0x040031A1 RID: 12705
			private static Dictionary<string, char> _lookupTable = WebUtility.HtmlEntities.GenerateLookupTable();
		}
	}
}
