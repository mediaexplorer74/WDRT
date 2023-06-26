using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System.Globalization
{
	/// <summary>Supports the use of non-ASCII characters for Internet domain names. This class cannot be inherited.</summary>
	// Token: 0x020003CA RID: 970
	public sealed class IdnMapping
	{
		/// <summary>Gets or sets a value that indicates whether unassigned Unicode code points are used in operations performed by members of the current <see cref="T:System.Globalization.IdnMapping" /> object.</summary>
		/// <returns>
		///   <see langword="true" /> if unassigned code points are used in operations; otherwise, <see langword="false" />.</returns>
		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06003104 RID: 12548 RVA: 0x000BD351 File Offset: 0x000BB551
		// (set) Token: 0x06003105 RID: 12549 RVA: 0x000BD359 File Offset: 0x000BB559
		public bool AllowUnassigned
		{
			get
			{
				return this.m_bAllowUnassigned;
			}
			set
			{
				this.m_bAllowUnassigned = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether standard or relaxed naming conventions are used in operations performed by members of the current <see cref="T:System.Globalization.IdnMapping" /> object.</summary>
		/// <returns>
		///   <see langword="true" /> if standard naming conventions are used in operations; otherwise, <see langword="false" />.</returns>
		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06003106 RID: 12550 RVA: 0x000BD362 File Offset: 0x000BB562
		// (set) Token: 0x06003107 RID: 12551 RVA: 0x000BD36A File Offset: 0x000BB56A
		public bool UseStd3AsciiRules
		{
			get
			{
				return this.m_bUseStd3AsciiRules;
			}
			set
			{
				this.m_bUseStd3AsciiRules = value;
			}
		}

		/// <summary>Encodes a string of domain name labels that consist of Unicode characters to a string of displayable Unicode characters in the US-ASCII character range. The string is formatted according to the IDNA standard.</summary>
		/// <param name="unicode">The string to convert, which consists of one or more domain name labels delimited with label separators.</param>
		/// <returns>The equivalent of the string specified by the <paramref name="unicode" /> parameter, consisting of displayable Unicode characters in the US-ASCII character range (U+0020 to U+007E) and formatted according to the IDNA standard.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="unicode" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="unicode" /> is invalid based on the <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties, and the IDNA standard.</exception>
		// Token: 0x06003108 RID: 12552 RVA: 0x000BD373 File Offset: 0x000BB573
		public string GetAscii(string unicode)
		{
			return this.GetAscii(unicode, 0);
		}

		/// <summary>Encodes a substring of domain name labels that include Unicode characters outside the US-ASCII character range. The substring is converted to a string of displayable Unicode characters in the US-ASCII character range and is formatted according to the IDNA standard.</summary>
		/// <param name="unicode">The string to convert, which consists of one or more domain name labels delimited with label separators.</param>
		/// <param name="index">A zero-based offset into <paramref name="unicode" /> that specifies the start of the substring to convert. The conversion operation continues to the end of the <paramref name="unicode" /> string.</param>
		/// <returns>The equivalent of the substring specified by the <paramref name="unicode" /> and <paramref name="index" /> parameters, consisting of displayable Unicode characters in the US-ASCII character range (U+0020 to U+007E) and formatted according to the IDNA standard.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="unicode" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is greater than the length of <paramref name="unicode" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="unicode" /> is invalid based on the <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties, and the IDNA standard.</exception>
		// Token: 0x06003109 RID: 12553 RVA: 0x000BD37D File Offset: 0x000BB57D
		public string GetAscii(string unicode, int index)
		{
			if (unicode == null)
			{
				throw new ArgumentNullException("unicode");
			}
			return this.GetAscii(unicode, index, unicode.Length - index);
		}

		/// <summary>Encodes the specified number of characters in a  substring of domain name labels that include Unicode characters outside the US-ASCII character range. The substring is converted to a string of displayable Unicode characters in the US-ASCII character range and is formatted according to the IDNA standard.</summary>
		/// <param name="unicode">The string to convert, which consists of one or more domain name labels delimited with label separators.</param>
		/// <param name="index">A zero-based offset into <paramref name="unicode" /> that specifies the start of the substring.</param>
		/// <param name="count">The number of characters to convert in the substring that starts at the position specified by  <paramref name="index" /> in the <paramref name="unicode" /> string.</param>
		/// <returns>The equivalent of the substring specified by the <paramref name="unicode" />, <paramref name="index" />, and <paramref name="count" /> parameters, consisting of displayable Unicode characters in the US-ASCII character range (U+0020 to U+007E) and formatted according to the IDNA standard.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="unicode" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is greater than the length of <paramref name="unicode" />.  
		/// -or-  
		/// <paramref name="index" /> is greater than the length of <paramref name="unicode" /> minus <paramref name="count" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="unicode" /> is invalid based on the <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties, and the IDNA standard.</exception>
		// Token: 0x0600310A RID: 12554 RVA: 0x000BD3A0 File Offset: 0x000BB5A0
		public string GetAscii(string unicode, int index, int count)
		{
			if (unicode == null)
			{
				throw new ArgumentNullException("unicode");
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (index > unicode.Length)
			{
				throw new ArgumentOutOfRangeException("byteIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (index > unicode.Length - count)
			{
				throw new ArgumentOutOfRangeException("unicode", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
			}
			unicode = unicode.Substring(index, count);
			if (Environment.IsWindows8OrAbove)
			{
				return this.GetAsciiUsingOS(unicode);
			}
			if (IdnMapping.ValidateStd3AndAscii(unicode, this.UseStd3AsciiRules, true))
			{
				return unicode;
			}
			if (unicode[unicode.Length - 1] <= '\u001f')
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequence", new object[] { unicode.Length - 1 }), "unicode");
			}
			bool flag = unicode.Length > 0 && IdnMapping.IsDot(unicode[unicode.Length - 1]);
			unicode = unicode.Normalize(this.m_bAllowUnassigned ? ((NormalizationForm)13) : ((NormalizationForm)269));
			if (!flag && unicode.Length > 0 && IdnMapping.IsDot(unicode[unicode.Length - 1]))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), "unicode");
			}
			if (this.UseStd3AsciiRules)
			{
				IdnMapping.ValidateStd3AndAscii(unicode, true, false);
			}
			return IdnMapping.punycode_encode(unicode);
		}

		// Token: 0x0600310B RID: 12555 RVA: 0x000BD510 File Offset: 0x000BB710
		[SecuritySafeCritical]
		private string GetAsciiUsingOS(string unicode)
		{
			if (unicode.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), "unicode");
			}
			if (unicode[unicode.Length - 1] == '\0')
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequence", new object[] { unicode.Length - 1 }), "unicode");
			}
			uint num = (this.AllowUnassigned ? 1U : 0U) | (this.UseStd3AsciiRules ? 2U : 0U);
			int num2 = IdnMapping.IdnToAscii(num, unicode, unicode.Length, null, 0);
			if (num2 == 0)
			{
				int num3 = Marshal.GetLastWin32Error();
				if (num3 == 123)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_IdnIllegalName"), "unicode");
				}
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequenceNoIndex"), "unicode");
			}
			else
			{
				char[] array = new char[num2];
				num2 = IdnMapping.IdnToAscii(num, unicode, unicode.Length, array, num2);
				if (num2 != 0)
				{
					return new string(array, 0, num2);
				}
				int num3 = Marshal.GetLastWin32Error();
				if (num3 == 123)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_IdnIllegalName"), "unicode");
				}
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequenceNoIndex"), "unicode");
			}
		}

		/// <summary>Decodes a string of one or more domain name labels, encoded according to the IDNA standard, to a string of Unicode characters.</summary>
		/// <param name="ascii">The string to decode, which consists of one or more labels in the US-ASCII character range (U+0020 to U+007E) encoded according to the IDNA standard.</param>
		/// <returns>The Unicode equivalent of the IDNA substring specified by the <paramref name="ascii" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ascii" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ascii" /> is invalid based on the <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties, and the IDNA standard.</exception>
		// Token: 0x0600310C RID: 12556 RVA: 0x000BD62E File Offset: 0x000BB82E
		public string GetUnicode(string ascii)
		{
			return this.GetUnicode(ascii, 0);
		}

		/// <summary>Decodes a substring of one or more domain name labels, encoded according to the IDNA standard, to a string of Unicode characters.</summary>
		/// <param name="ascii">The string to decode, which consists of one or more labels in the US-ASCII character range (U+0020 to U+007E) encoded according to the IDNA standard.</param>
		/// <param name="index">A zero-based offset into <paramref name="ascii" /> that specifies the start of the substring to decode. The decoding operation continues to the end of the <paramref name="ascii" /> string.</param>
		/// <returns>The Unicode equivalent of the IDNA substring specified by the <paramref name="ascii" /> and <paramref name="index" /> parameters.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ascii" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is greater than the length of <paramref name="ascii" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ascii" /> is invalid based on the <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties, and the IDNA standard.</exception>
		// Token: 0x0600310D RID: 12557 RVA: 0x000BD638 File Offset: 0x000BB838
		public string GetUnicode(string ascii, int index)
		{
			if (ascii == null)
			{
				throw new ArgumentNullException("ascii");
			}
			return this.GetUnicode(ascii, index, ascii.Length - index);
		}

		/// <summary>Decodes a substring of a specified length that contains one or more domain name labels, encoded according to the IDNA standard, to a string of Unicode characters.</summary>
		/// <param name="ascii">The string to decode, which consists of one or more labels in the US-ASCII character range (U+0020 to U+007E) encoded according to the IDNA standard.</param>
		/// <param name="index">A zero-based offset into <paramref name="ascii" /> that specifies the start of the substring.</param>
		/// <param name="count">The number of characters to convert in the substring that starts at the position specified by <paramref name="index" /> in the <paramref name="ascii" /> string.</param>
		/// <returns>The Unicode equivalent of the IDNA substring specified by the <paramref name="ascii" />, <paramref name="index" />, and <paramref name="count" /> parameters.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ascii" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is greater than the length of <paramref name="ascii" />.  
		/// -or-  
		/// <paramref name="index" /> is greater than the length of <paramref name="ascii" /> minus <paramref name="count" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ascii" /> is invalid based on the <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties, and the IDNA standard.</exception>
		// Token: 0x0600310E RID: 12558 RVA: 0x000BD658 File Offset: 0x000BB858
		public string GetUnicode(string ascii, int index, int count)
		{
			if (ascii == null)
			{
				throw new ArgumentNullException("ascii");
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (index > ascii.Length)
			{
				throw new ArgumentOutOfRangeException("byteIndex", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			if (index > ascii.Length - count)
			{
				throw new ArgumentOutOfRangeException("ascii", Environment.GetResourceString("ArgumentOutOfRange_IndexCountBuffer"));
			}
			if (count > 0 && ascii[index + count - 1] == '\0')
			{
				throw new ArgumentException("ascii", Environment.GetResourceString("Argument_IdnBadPunycode"));
			}
			ascii = ascii.Substring(index, count);
			if (Environment.IsWindows8OrAbove)
			{
				return this.GetUnicodeUsingOS(ascii);
			}
			string text = IdnMapping.punycode_decode(ascii);
			if (!ascii.Equals(this.GetAscii(text), StringComparison.OrdinalIgnoreCase))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IdnIllegalName"), "ascii");
			}
			return text;
		}

		// Token: 0x0600310F RID: 12559 RVA: 0x000BD748 File Offset: 0x000BB948
		[SecuritySafeCritical]
		private string GetUnicodeUsingOS(string ascii)
		{
			uint num = (this.AllowUnassigned ? 1U : 0U) | (this.UseStd3AsciiRules ? 2U : 0U);
			int num2 = IdnMapping.IdnToUnicode(num, ascii, ascii.Length, null, 0);
			if (num2 == 0)
			{
				int num3 = Marshal.GetLastWin32Error();
				if (num3 == 123)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_IdnIllegalName"), "ascii");
				}
				throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadPunycode"), "ascii");
			}
			else
			{
				char[] array = new char[num2];
				num2 = IdnMapping.IdnToUnicode(num, ascii, ascii.Length, array, num2);
				if (num2 != 0)
				{
					return new string(array, 0, num2);
				}
				int num3 = Marshal.GetLastWin32Error();
				if (num3 == 123)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_IdnIllegalName"), "ascii");
				}
				throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadPunycode"), "ascii");
			}
		}

		/// <summary>Indicates whether a specified object and the current <see cref="T:System.Globalization.IdnMapping" /> object are equal.</summary>
		/// <param name="obj">The object to compare to the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the object specified by the <paramref name="obj" /> parameter is derived from <see cref="T:System.Globalization.IdnMapping" /> and its <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003110 RID: 12560 RVA: 0x000BD810 File Offset: 0x000BBA10
		public override bool Equals(object obj)
		{
			IdnMapping idnMapping = obj as IdnMapping;
			return idnMapping != null && this.m_bAllowUnassigned == idnMapping.m_bAllowUnassigned && this.m_bUseStd3AsciiRules == idnMapping.m_bUseStd3AsciiRules;
		}

		/// <summary>Returns a hash code for this <see cref="T:System.Globalization.IdnMapping" /> object.</summary>
		/// <returns>One of four 32-bit signed constants derived from the properties of an <see cref="T:System.Globalization.IdnMapping" /> object.  The return value has no special meaning and is not suitable for use in a hash code algorithm.</returns>
		// Token: 0x06003111 RID: 12561 RVA: 0x000BD847 File Offset: 0x000BBA47
		public override int GetHashCode()
		{
			return (this.m_bAllowUnassigned ? 100 : 200) + (this.m_bUseStd3AsciiRules ? 1000 : 2000);
		}

		// Token: 0x06003112 RID: 12562 RVA: 0x000BD86F File Offset: 0x000BBA6F
		private static bool IsSupplementary(int cTest)
		{
			return cTest >= 65536;
		}

		// Token: 0x06003113 RID: 12563 RVA: 0x000BD87C File Offset: 0x000BBA7C
		private static bool IsDot(char c)
		{
			return c == '.' || c == '。' || c == '．' || c == '｡';
		}

		// Token: 0x06003114 RID: 12564 RVA: 0x000BD8A0 File Offset: 0x000BBAA0
		private static bool ValidateStd3AndAscii(string unicode, bool bUseStd3, bool bCheckAscii)
		{
			if (unicode.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), "unicode");
			}
			int num = -1;
			for (int i = 0; i < unicode.Length; i++)
			{
				if (unicode[i] <= '\u001f')
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidCharSequence", new object[] { i }), "unicode");
				}
				if (bCheckAscii && unicode[i] >= '\u007f')
				{
					return false;
				}
				if (IdnMapping.IsDot(unicode[i]))
				{
					if (i == num + 1)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), "unicode");
					}
					if (i - num > 64)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), "Unicode");
					}
					if (bUseStd3 && i > 0)
					{
						IdnMapping.ValidateStd3(unicode[i - 1], true);
					}
					num = i;
				}
				else if (bUseStd3)
				{
					IdnMapping.ValidateStd3(unicode[i], i == num + 1);
				}
			}
			if (num == -1 && unicode.Length > 63)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), "unicode");
			}
			if (unicode.Length > 255 - (IdnMapping.IsDot(unicode[unicode.Length - 1]) ? 0 : 1))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadNameSize", new object[] { 255 - (IdnMapping.IsDot(unicode[unicode.Length - 1]) ? 0 : 1) }), "unicode");
			}
			if (bUseStd3 && !IdnMapping.IsDot(unicode[unicode.Length - 1]))
			{
				IdnMapping.ValidateStd3(unicode[unicode.Length - 1], true);
			}
			return true;
		}

		// Token: 0x06003115 RID: 12565 RVA: 0x000BDA50 File Offset: 0x000BBC50
		private static void ValidateStd3(char c, bool bNextToDot)
		{
			if (c <= ',' || c == '/' || (c >= ':' && c <= '@') || (c >= '[' && c <= '`') || (c >= '{' && c <= '\u007f') || (c == '-' && bNextToDot))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadStd3", new object[] { c }), "Unicode");
			}
		}

		// Token: 0x06003116 RID: 12566 RVA: 0x000BDAB2 File Offset: 0x000BBCB2
		private static bool HasUpperCaseFlag(char punychar)
		{
			return punychar >= 'A' && punychar <= 'Z';
		}

		// Token: 0x06003117 RID: 12567 RVA: 0x000BDAC3 File Offset: 0x000BBCC3
		private static bool basic(uint cp)
		{
			return cp < 128U;
		}

		// Token: 0x06003118 RID: 12568 RVA: 0x000BDAD0 File Offset: 0x000BBCD0
		private static int decode_digit(char cp)
		{
			if (cp >= '0' && cp <= '9')
			{
				return (int)(cp - '0' + '\u001a');
			}
			if (cp >= 'a' && cp <= 'z')
			{
				return (int)(cp - 'a');
			}
			if (cp >= 'A' && cp <= 'Z')
			{
				return (int)(cp - 'A');
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadPunycode"), "ascii");
		}

		// Token: 0x06003119 RID: 12569 RVA: 0x000BDB21 File Offset: 0x000BBD21
		private static char encode_digit(int d)
		{
			if (d > 25)
			{
				return (char)(d - 26 + 48);
			}
			return (char)(d + 97);
		}

		// Token: 0x0600311A RID: 12570 RVA: 0x000BDB36 File Offset: 0x000BBD36
		private static char encode_basic(char bcp)
		{
			if (IdnMapping.HasUpperCaseFlag(bcp))
			{
				bcp += ' ';
			}
			return bcp;
		}

		// Token: 0x0600311B RID: 12571 RVA: 0x000BDB48 File Offset: 0x000BBD48
		private static int adapt(int delta, int numpoints, bool firsttime)
		{
			delta = (firsttime ? (delta / 700) : (delta / 2));
			delta += delta / numpoints;
			uint num = 0U;
			while (delta > 455)
			{
				delta /= 35;
				num += 36U;
			}
			return (int)((ulong)num + (ulong)((long)(36 * delta / (delta + 38))));
		}

		// Token: 0x0600311C RID: 12572 RVA: 0x000BDB94 File Offset: 0x000BBD94
		private static string punycode_encode(string unicode)
		{
			if (unicode.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), "unicode");
			}
			StringBuilder stringBuilder = new StringBuilder(unicode.Length);
			int i = 0;
			int num = 0;
			int num2 = 0;
			while (i < unicode.Length)
			{
				i = unicode.IndexOfAny(IdnMapping.M_Dots, num);
				if (i < 0)
				{
					i = unicode.Length;
				}
				if (i == num)
				{
					if (i != unicode.Length)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), "unicode");
					}
					break;
				}
				else
				{
					stringBuilder.Append("xn--");
					bool flag = false;
					BidiCategory bidiCategory = CharUnicodeInfo.GetBidiCategory(unicode, num);
					if (bidiCategory == BidiCategory.RightToLeft || bidiCategory == BidiCategory.RightToLeftArabic)
					{
						flag = true;
						int num3 = i - 1;
						if (char.IsLowSurrogate(unicode, num3))
						{
							num3--;
						}
						bidiCategory = CharUnicodeInfo.GetBidiCategory(unicode, num3);
						if (bidiCategory != BidiCategory.RightToLeft && bidiCategory != BidiCategory.RightToLeftArabic)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadBidi"), "unicode");
						}
					}
					int j = 0;
					for (int k = num; k < i; k++)
					{
						BidiCategory bidiCategory2 = CharUnicodeInfo.GetBidiCategory(unicode, k);
						if (flag && bidiCategory2 == BidiCategory.LeftToRight)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadBidi"), "unicode");
						}
						if (!flag && (bidiCategory2 == BidiCategory.RightToLeft || bidiCategory2 == BidiCategory.RightToLeftArabic))
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadBidi"), "unicode");
						}
						if (IdnMapping.basic((uint)unicode[k]))
						{
							stringBuilder.Append(IdnMapping.encode_basic(unicode[k]));
							j++;
						}
						else if (char.IsSurrogatePair(unicode, k))
						{
							k++;
						}
					}
					int num4 = j;
					if (num4 == i - num)
					{
						stringBuilder.Remove(num2, "xn--".Length);
					}
					else
					{
						if (unicode.Length - num >= "xn--".Length && unicode.Substring(num, "xn--".Length).Equals("xn--", StringComparison.OrdinalIgnoreCase))
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadPunycode"), "unicode");
						}
						int num5 = 0;
						if (num4 > 0)
						{
							stringBuilder.Append('-');
						}
						int num6 = 128;
						int num7 = 0;
						int num8 = 72;
						while (j < i - num)
						{
							int num9 = 134217727;
							int num10;
							for (int l = num; l < i; l += (IdnMapping.IsSupplementary(num10) ? 2 : 1))
							{
								num10 = char.ConvertToUtf32(unicode, l);
								if (num10 >= num6 && num10 < num9)
								{
									num9 = num10;
								}
							}
							num7 += (num9 - num6) * (j - num5 + 1);
							num6 = num9;
							for (int l = num; l < i; l += (IdnMapping.IsSupplementary(num10) ? 2 : 1))
							{
								num10 = char.ConvertToUtf32(unicode, l);
								if (num10 < num6)
								{
									num7++;
								}
								if (num10 == num6)
								{
									int num11 = num7;
									int num12 = 36;
									for (;;)
									{
										int num13 = ((num12 <= num8) ? 1 : ((num12 >= num8 + 26) ? 26 : (num12 - num8)));
										if (num11 < num13)
										{
											break;
										}
										stringBuilder.Append(IdnMapping.encode_digit(num13 + (num11 - num13) % (36 - num13)));
										num11 = (num11 - num13) / (36 - num13);
										num12 += 36;
									}
									stringBuilder.Append(IdnMapping.encode_digit(num11));
									num8 = IdnMapping.adapt(num7, j - num5 + 1, j == num4);
									num7 = 0;
									j++;
									if (IdnMapping.IsSupplementary(num9))
									{
										j++;
										num5++;
									}
								}
							}
							num7++;
							num6++;
						}
					}
					if (stringBuilder.Length - num2 > 63)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), "unicode");
					}
					if (i != unicode.Length)
					{
						stringBuilder.Append('.');
					}
					num = i + 1;
					num2 = stringBuilder.Length;
				}
			}
			if (stringBuilder.Length > 255 - (IdnMapping.IsDot(unicode[unicode.Length - 1]) ? 0 : 1))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadNameSize", new object[] { 255 - (IdnMapping.IsDot(unicode[unicode.Length - 1]) ? 0 : 1) }), "unicode");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600311D RID: 12573 RVA: 0x000BDFA4 File Offset: 0x000BC1A4
		private static string punycode_decode(string ascii)
		{
			if (ascii.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), "ascii");
			}
			if (ascii.Length > 255 - (IdnMapping.IsDot(ascii[ascii.Length - 1]) ? 0 : 1))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadNameSize", new object[] { 255 - (IdnMapping.IsDot(ascii[ascii.Length - 1]) ? 0 : 1) }), "ascii");
			}
			StringBuilder stringBuilder = new StringBuilder(ascii.Length);
			int i = 0;
			int num = 0;
			int num2 = 0;
			while (i < ascii.Length)
			{
				i = ascii.IndexOf('.', num);
				if (i < 0 || i > ascii.Length)
				{
					i = ascii.Length;
				}
				if (i == num)
				{
					if (i != ascii.Length)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), "ascii");
					}
					break;
				}
				else
				{
					if (i - num > 63)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), "ascii");
					}
					if (ascii.Length < "xn--".Length + num || !ascii.Substring(num, "xn--".Length).Equals("xn--", StringComparison.OrdinalIgnoreCase))
					{
						stringBuilder.Append(ascii.Substring(num, i - num));
					}
					else
					{
						num += "xn--".Length;
						int num3 = ascii.LastIndexOf('-', i - 1);
						if (num3 == i - 1)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadPunycode"), "ascii");
						}
						int num4;
						if (num3 <= num)
						{
							num4 = 0;
						}
						else
						{
							num4 = num3 - num;
							for (int j = num; j < num + num4; j++)
							{
								if (ascii[j] > '\u007f')
								{
									throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadPunycode"), "ascii");
								}
								stringBuilder.Append((ascii[j] >= 'A' && ascii[j] <= 'Z') ? (ascii[j] - 'A' + 'a') : ascii[j]);
							}
						}
						int k = num + ((num4 > 0) ? (num4 + 1) : 0);
						int num5 = 128;
						int num6 = 72;
						int num7 = 0;
						int num8 = 0;
						IL_40D:
						while (k < i)
						{
							int num9 = num7;
							int num10 = 1;
							int num11 = 36;
							while (k < i)
							{
								int num12 = IdnMapping.decode_digit(ascii[k++]);
								if (num12 > (134217727 - num7) / num10)
								{
									throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadPunycode"), "ascii");
								}
								num7 += num12 * num10;
								int num13 = ((num11 <= num6) ? 1 : ((num11 >= num6 + 26) ? 26 : (num11 - num6)));
								if (num12 >= num13)
								{
									if (num10 > 134217727 / (36 - num13))
									{
										throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadPunycode"), "ascii");
									}
									num10 *= 36 - num13;
									num11 += 36;
								}
								else
								{
									num6 = IdnMapping.adapt(num7 - num9, stringBuilder.Length - num2 - num8 + 1, num9 == 0);
									if (num7 / (stringBuilder.Length - num2 - num8 + 1) > 134217727 - num5)
									{
										throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadPunycode"), "ascii");
									}
									num5 += num7 / (stringBuilder.Length - num2 - num8 + 1);
									num7 %= stringBuilder.Length - num2 - num8 + 1;
									if (num5 < 0 || num5 > 1114111 || (num5 >= 55296 && num5 <= 57343))
									{
										throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadPunycode"), "ascii");
									}
									string text = char.ConvertFromUtf32(num5);
									int num14;
									if (num8 > 0)
									{
										int l = num7;
										num14 = num2;
										while (l > 0)
										{
											if (num14 >= stringBuilder.Length)
											{
												throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadPunycode"), "ascii");
											}
											if (char.IsSurrogate(stringBuilder[num14]))
											{
												num14++;
											}
											l--;
											num14++;
										}
									}
									else
									{
										num14 = num2 + num7;
									}
									stringBuilder.Insert(num14, text);
									if (IdnMapping.IsSupplementary(num5))
									{
										num8++;
									}
									num7++;
									goto IL_40D;
								}
							}
							throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadPunycode"), "ascii");
						}
						bool flag = false;
						BidiCategory bidiCategory = CharUnicodeInfo.GetBidiCategory(stringBuilder.ToString(), num2);
						if (bidiCategory == BidiCategory.RightToLeft || bidiCategory == BidiCategory.RightToLeftArabic)
						{
							flag = true;
						}
						for (int m = num2; m < stringBuilder.Length; m++)
						{
							if (!char.IsLowSurrogate(stringBuilder.ToString(), m))
							{
								bidiCategory = CharUnicodeInfo.GetBidiCategory(stringBuilder.ToString(), m);
								if ((flag && bidiCategory == BidiCategory.LeftToRight) || (!flag && (bidiCategory == BidiCategory.RightToLeft || bidiCategory == BidiCategory.RightToLeftArabic)))
								{
									throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadBidi"), "ascii");
								}
							}
						}
						if (flag && bidiCategory != BidiCategory.RightToLeft && bidiCategory != BidiCategory.RightToLeftArabic)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadBidi"), "ascii");
						}
					}
					if (i - num > 63)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadLabelSize"), "ascii");
					}
					if (i != ascii.Length)
					{
						stringBuilder.Append('.');
					}
					num = i + 1;
					num2 = stringBuilder.Length;
				}
			}
			if (stringBuilder.Length > 255 - (IdnMapping.IsDot(stringBuilder[stringBuilder.Length - 1]) ? 0 : 1))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IdnBadNameSize", new object[] { 255 - (IdnMapping.IsDot(stringBuilder[stringBuilder.Length - 1]) ? 0 : 1) }), "ascii");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600311E RID: 12574
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern int IdnToAscii(uint dwFlags, [MarshalAs(UnmanagedType.LPWStr)] [In] string lpUnicodeCharStr, int cchUnicodeChar, [Out] char[] lpASCIICharStr, int cchASCIIChar);

		// Token: 0x0600311F RID: 12575
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern int IdnToUnicode(uint dwFlags, [MarshalAs(UnmanagedType.LPWStr)] [In] string lpASCIICharStr, int cchASCIIChar, [Out] char[] lpUnicodeCharStr, int cchUnicodeChar);

		// Token: 0x040014FE RID: 5374
		private const int M_labelLimit = 63;

		// Token: 0x040014FF RID: 5375
		private const int M_defaultNameLimit = 255;

		// Token: 0x04001500 RID: 5376
		private const string M_strAcePrefix = "xn--";

		// Token: 0x04001501 RID: 5377
		private static char[] M_Dots = new char[] { '.', '。', '．', '｡' };

		// Token: 0x04001502 RID: 5378
		private bool m_bAllowUnassigned;

		// Token: 0x04001503 RID: 5379
		private bool m_bUseStd3AsciiRules;

		// Token: 0x04001504 RID: 5380
		private const int punycodeBase = 36;

		// Token: 0x04001505 RID: 5381
		private const int tmin = 1;

		// Token: 0x04001506 RID: 5382
		private const int tmax = 26;

		// Token: 0x04001507 RID: 5383
		private const int skew = 38;

		// Token: 0x04001508 RID: 5384
		private const int damp = 700;

		// Token: 0x04001509 RID: 5385
		private const int initial_bias = 72;

		// Token: 0x0400150A RID: 5386
		private const int initial_n = 128;

		// Token: 0x0400150B RID: 5387
		private const char delimiter = '-';

		// Token: 0x0400150C RID: 5388
		private const int maxint = 134217727;

		// Token: 0x0400150D RID: 5389
		private const int IDN_ALLOW_UNASSIGNED = 1;

		// Token: 0x0400150E RID: 5390
		private const int IDN_USE_STD3_ASCII_RULES = 2;

		// Token: 0x0400150F RID: 5391
		private const int ERROR_INVALID_NAME = 123;
	}
}
