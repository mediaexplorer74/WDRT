using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace System
{
	/// <summary>Represents a character as a UTF-16 code unit.</summary>
	// Token: 0x020000B6 RID: 182
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct Char : IComparable, IConvertible, IComparable<char>, IEquatable<char>
	{
		// Token: 0x06000A87 RID: 2695 RVA: 0x00021865 File Offset: 0x0001FA65
		private static bool IsLatin1(char ch)
		{
			return ch <= 'ÿ';
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00021872 File Offset: 0x0001FA72
		private static bool IsAscii(char ch)
		{
			return ch <= '\u007f';
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0002187C File Offset: 0x0001FA7C
		private static UnicodeCategory GetLatin1UnicodeCategory(char ch)
		{
			return (UnicodeCategory)char.categoryForLatin1[(int)ch];
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000A8A RID: 2698 RVA: 0x00021885 File Offset: 0x0001FA85
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return (int)(this | ((int)this << 16));
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An object to compare with this instance or <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is an instance of <see cref="T:System.Char" /> and equals the value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A8B RID: 2699 RVA: 0x0002188F File Offset: 0x0001FA8F
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return obj is char && this == (char)obj;
		}

		/// <summary>Returns a value that indicates whether this instance is equal to the specified <see cref="T:System.Char" /> object.</summary>
		/// <param name="obj">An object to compare to this instance.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="obj" /> parameter equals the value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A8C RID: 2700 RVA: 0x000218A5 File Offset: 0x0001FAA5
		[NonVersionable]
		[__DynamicallyInvokable]
		public bool Equals(char obj)
		{
			return this == obj;
		}

		/// <summary>Compares this instance to a specified object and indicates whether this instance precedes, follows, or appears in the same position in the sort order as the specified <see cref="T:System.Object" />.</summary>
		/// <param name="value">An object to compare this instance to, or <see langword="null" />.</param>
		/// <returns>A signed number indicating the position of this instance in the sort order in relation to the <paramref name="value" /> parameter.  
		///   Return Value  
		///
		///   Description  
		///
		///   Less than zero  
		///
		///   This instance precedes <paramref name="value" />.  
		///
		///   Zero  
		///
		///   This instance has the same position in the sort order as <paramref name="value" />.  
		///
		///   Greater than zero  
		///
		///   This instance follows <paramref name="value" />.  
		///
		///  -or-  
		///
		///  <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Char" /> object.</exception>
		// Token: 0x06000A8D RID: 2701 RVA: 0x000218AC File Offset: 0x0001FAAC
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is char))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeChar"));
			}
			return (int)(this - (char)value);
		}

		/// <summary>Compares this instance to a specified <see cref="T:System.Char" /> object and indicates whether this instance precedes, follows, or appears in the same position in the sort order as the specified <see cref="T:System.Char" /> object.</summary>
		/// <param name="value">A <see cref="T:System.Char" /> object to compare.</param>
		/// <returns>A signed number indicating the position of this instance in the sort order in relation to the <paramref name="value" /> parameter.  
		///   Return Value  
		///
		///   Description  
		///
		///   Less than zero  
		///
		///   This instance precedes <paramref name="value" />.  
		///
		///   Zero  
		///
		///   This instance has the same position in the sort order as <paramref name="value" />.  
		///
		///   Greater than zero  
		///
		///   This instance follows <paramref name="value" />.</returns>
		// Token: 0x06000A8E RID: 2702 RVA: 0x000218D4 File Offset: 0x0001FAD4
		[__DynamicallyInvokable]
		public int CompareTo(char value)
		{
			return (int)(this - value);
		}

		/// <summary>Converts the value of this instance to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of this instance.</returns>
		// Token: 0x06000A8F RID: 2703 RVA: 0x000218DA File Offset: 0x0001FADA
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return char.ToString(this);
		}

		/// <summary>Converts the value of this instance to its equivalent string representation using the specified culture-specific format information.</summary>
		/// <param name="provider">(Reserved) An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="provider" />.</returns>
		// Token: 0x06000A90 RID: 2704 RVA: 0x000218E3 File Offset: 0x0001FAE3
		public string ToString(IFormatProvider provider)
		{
			return char.ToString(this);
		}

		/// <summary>Converts the specified Unicode character to its equivalent string representation.</summary>
		/// <param name="c">The Unicode character to convert.</param>
		/// <returns>The string representation of the value of <paramref name="c" />.</returns>
		// Token: 0x06000A91 RID: 2705 RVA: 0x000218EC File Offset: 0x0001FAEC
		[__DynamicallyInvokable]
		public static string ToString(char c)
		{
			return new string(c, 1);
		}

		/// <summary>Converts the value of the specified string to its equivalent Unicode character.</summary>
		/// <param name="s">A string that contains a single character, or <see langword="null" />.</param>
		/// <returns>A Unicode character equivalent to the sole character in <paramref name="s" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">The length of <paramref name="s" /> is not 1.</exception>
		// Token: 0x06000A92 RID: 2706 RVA: 0x000218F5 File Offset: 0x0001FAF5
		[__DynamicallyInvokable]
		public static char Parse(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (s.Length != 1)
			{
				throw new FormatException(Environment.GetResourceString("Format_NeedSingleChar"));
			}
			return s[0];
		}

		/// <summary>Converts the value of the specified string to its equivalent Unicode character. A return code indicates whether the conversion succeeded or failed.</summary>
		/// <param name="s">A string that contains a single character, or <see langword="null" />.</param>
		/// <param name="result">When this method returns, contains a Unicode character equivalent to the sole character in <paramref name="s" />, if the conversion succeeded, or an undefined value if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is <see langword="null" /> or the length of <paramref name="s" /> is not 1. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="s" /> parameter was converted successfully; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A93 RID: 2707 RVA: 0x00021925 File Offset: 0x0001FB25
		[__DynamicallyInvokable]
		public static bool TryParse(string s, out char result)
		{
			result = '\0';
			if (s == null)
			{
				return false;
			}
			if (s.Length != 1)
			{
				return false;
			}
			result = s[0];
			return true;
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as a decimal digit.</summary>
		/// <param name="c">The Unicode character to evaluate.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="c" /> is a decimal digit; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A94 RID: 2708 RVA: 0x00021944 File Offset: 0x0001FB44
		[__DynamicallyInvokable]
		public static bool IsDigit(char c)
		{
			if (char.IsLatin1(c))
			{
				return c >= '0' && c <= '9';
			}
			return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.DecimalDigitNumber;
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x00021967 File Offset: 0x0001FB67
		internal static bool CheckLetter(UnicodeCategory uc)
		{
			return uc <= UnicodeCategory.OtherLetter;
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as a Unicode letter.</summary>
		/// <param name="c">The Unicode character to evaluate.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="c" /> is a letter; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A96 RID: 2710 RVA: 0x00021970 File Offset: 0x0001FB70
		[__DynamicallyInvokable]
		public static bool IsLetter(char c)
		{
			if (!char.IsLatin1(c))
			{
				return char.CheckLetter(CharUnicodeInfo.GetUnicodeCategory(c));
			}
			if (char.IsAscii(c))
			{
				c |= ' ';
				return c >= 'a' && c <= 'z';
			}
			return char.CheckLetter(char.GetLatin1UnicodeCategory(c));
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x000219B0 File Offset: 0x0001FBB0
		private static bool IsWhiteSpaceLatin1(char c)
		{
			return c == ' ' || (c >= '\t' && c <= '\r') || c == '\u00a0' || c == '\u0085';
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as white space.</summary>
		/// <param name="c">The Unicode character to evaluate.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="c" /> is white space; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A98 RID: 2712 RVA: 0x000219D4 File Offset: 0x0001FBD4
		[__DynamicallyInvokable]
		public static bool IsWhiteSpace(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.IsWhiteSpaceLatin1(c);
			}
			return CharUnicodeInfo.IsWhiteSpace(c);
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as an uppercase letter.</summary>
		/// <param name="c">The Unicode character to evaluate.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="c" /> is an uppercase letter; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A99 RID: 2713 RVA: 0x000219EB File Offset: 0x0001FBEB
		[__DynamicallyInvokable]
		public static bool IsUpper(char c)
		{
			if (!char.IsLatin1(c))
			{
				return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.UppercaseLetter;
			}
			if (char.IsAscii(c))
			{
				return c >= 'A' && c <= 'Z';
			}
			return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.UppercaseLetter;
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as a lowercase letter.</summary>
		/// <param name="c">The Unicode character to evaluate.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="c" /> is a lowercase letter; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A9A RID: 2714 RVA: 0x00021A20 File Offset: 0x0001FC20
		[__DynamicallyInvokable]
		public static bool IsLower(char c)
		{
			if (!char.IsLatin1(c))
			{
				return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.LowercaseLetter;
			}
			if (char.IsAscii(c))
			{
				return c >= 'a' && c <= 'z';
			}
			return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.LowercaseLetter;
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00021A55 File Offset: 0x0001FC55
		internal static bool CheckPunctuation(UnicodeCategory uc)
		{
			return uc - UnicodeCategory.ConnectorPunctuation <= 6;
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as a punctuation mark.</summary>
		/// <param name="c">The Unicode character to evaluate.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="c" /> is a punctuation mark; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A9C RID: 2716 RVA: 0x00021A61 File Offset: 0x0001FC61
		[__DynamicallyInvokable]
		public static bool IsPunctuation(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.CheckPunctuation(char.GetLatin1UnicodeCategory(c));
			}
			return char.CheckPunctuation(CharUnicodeInfo.GetUnicodeCategory(c));
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x00021A82 File Offset: 0x0001FC82
		internal static bool CheckLetterOrDigit(UnicodeCategory uc)
		{
			return uc <= UnicodeCategory.OtherLetter || uc == UnicodeCategory.DecimalDigitNumber;
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as a letter or a decimal digit.</summary>
		/// <param name="c">The Unicode character to evaluate.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="c" /> is a letter or a decimal digit; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000A9E RID: 2718 RVA: 0x00021A8F File Offset: 0x0001FC8F
		[__DynamicallyInvokable]
		public static bool IsLetterOrDigit(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.CheckLetterOrDigit(char.GetLatin1UnicodeCategory(c));
			}
			return char.CheckLetterOrDigit(CharUnicodeInfo.GetUnicodeCategory(c));
		}

		/// <summary>Converts the value of a specified Unicode character to its uppercase equivalent using specified culture-specific formatting information.</summary>
		/// <param name="c">The Unicode character to convert.</param>
		/// <param name="culture">An object that supplies culture-specific casing rules.</param>
		/// <returns>The uppercase equivalent of <paramref name="c" />, modified according to <paramref name="culture" />, or the unchanged value of <paramref name="c" /> if <paramref name="c" /> is already uppercase, has no uppercase equivalent, or is not alphabetic.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is <see langword="null" />.</exception>
		// Token: 0x06000A9F RID: 2719 RVA: 0x00021AB0 File Offset: 0x0001FCB0
		[__DynamicallyInvokable]
		public static char ToUpper(char c, CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return culture.TextInfo.ToUpper(c);
		}

		/// <summary>Converts the value of a Unicode character to its uppercase equivalent.</summary>
		/// <param name="c">The Unicode character to convert.</param>
		/// <returns>The uppercase equivalent of <paramref name="c" />, or the unchanged value of <paramref name="c" /> if <paramref name="c" /> is already uppercase, has no uppercase equivalent, or is not alphabetic.</returns>
		// Token: 0x06000AA0 RID: 2720 RVA: 0x00021ACC File Offset: 0x0001FCCC
		[__DynamicallyInvokable]
		public static char ToUpper(char c)
		{
			return char.ToUpper(c, CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the value of a Unicode character to its uppercase equivalent using the casing rules of the invariant culture.</summary>
		/// <param name="c">The Unicode character to convert.</param>
		/// <returns>The uppercase equivalent of the <paramref name="c" /> parameter, or the unchanged value of <paramref name="c" />, if <paramref name="c" /> is already uppercase or not alphabetic.</returns>
		// Token: 0x06000AA1 RID: 2721 RVA: 0x00021AD9 File Offset: 0x0001FCD9
		[__DynamicallyInvokable]
		public static char ToUpperInvariant(char c)
		{
			return char.ToUpper(c, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts the value of a specified Unicode character to its lowercase equivalent using specified culture-specific formatting information.</summary>
		/// <param name="c">The Unicode character to convert.</param>
		/// <param name="culture">An object that supplies culture-specific casing rules.</param>
		/// <returns>The lowercase equivalent of <paramref name="c" />, modified according to <paramref name="culture" />, or the unchanged value of <paramref name="c" />, if <paramref name="c" /> is already lowercase or not alphabetic.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is <see langword="null" />.</exception>
		// Token: 0x06000AA2 RID: 2722 RVA: 0x00021AE6 File Offset: 0x0001FCE6
		[__DynamicallyInvokable]
		public static char ToLower(char c, CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return culture.TextInfo.ToLower(c);
		}

		/// <summary>Converts the value of a Unicode character to its lowercase equivalent.</summary>
		/// <param name="c">The Unicode character to convert.</param>
		/// <returns>The lowercase equivalent of <paramref name="c" />, or the unchanged value of <paramref name="c" />, if <paramref name="c" /> is already lowercase or not alphabetic.</returns>
		// Token: 0x06000AA3 RID: 2723 RVA: 0x00021B02 File Offset: 0x0001FD02
		[__DynamicallyInvokable]
		public static char ToLower(char c)
		{
			return char.ToLower(c, CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the value of a Unicode character to its lowercase equivalent using the casing rules of the invariant culture.</summary>
		/// <param name="c">The Unicode character to convert.</param>
		/// <returns>The lowercase equivalent of the <paramref name="c" /> parameter, or the unchanged value of <paramref name="c" />, if <paramref name="c" /> is already lowercase or not alphabetic.</returns>
		// Token: 0x06000AA4 RID: 2724 RVA: 0x00021B0F File Offset: 0x0001FD0F
		[__DynamicallyInvokable]
		public static char ToLowerInvariant(char c)
		{
			return char.ToLower(c, CultureInfo.InvariantCulture);
		}

		/// <summary>Returns the <see cref="T:System.TypeCode" /> for value type <see cref="T:System.Char" />.</summary>
		/// <returns>The enumerated constant, <see cref="F:System.TypeCode.Char" />.</returns>
		// Token: 0x06000AA5 RID: 2725 RVA: 0x00021B1C File Offset: 0x0001FD1C
		public TypeCode GetTypeCode()
		{
			return TypeCode.Char;
		}

		/// <summary>Note This conversion is not supported. Attempting to do so throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x06000AA6 RID: 2726 RVA: 0x00021B1F File Offset: 0x0001FD1F
		[__DynamicallyInvokable]
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "Char", "Boolean" }));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToChar(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current <see cref="T:System.Char" /> object unchanged.</returns>
		// Token: 0x06000AA7 RID: 2727 RVA: 0x00021B46 File Offset: 0x0001FD46
		[__DynamicallyInvokable]
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return this;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSByte(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
		// Token: 0x06000AA8 RID: 2728 RVA: 0x00021B4A File Offset: 0x0001FD4A
		[__DynamicallyInvokable]
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToByte(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
		// Token: 0x06000AA9 RID: 2729 RVA: 0x00021B53 File Offset: 0x0001FD53
		[__DynamicallyInvokable]
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt16(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
		// Token: 0x06000AAA RID: 2730 RVA: 0x00021B5C File Offset: 0x0001FD5C
		[__DynamicallyInvokable]
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt16(System.IFormatProvider)" />.</summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> object. (Specify <see langword="null" /> because the <paramref name="provider" /> parameter is ignored.)</param>
		/// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
		// Token: 0x06000AAB RID: 2731 RVA: 0x00021B65 File Offset: 0x0001FD65
		[__DynamicallyInvokable]
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
		// Token: 0x06000AAC RID: 2732 RVA: 0x00021B6E File Offset: 0x0001FD6E
		[__DynamicallyInvokable]
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt32(System.IFormatProvider)" />.</summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> object. (Specify <see langword="null" /> because the <paramref name="provider" /> parameter is ignored.)</param>
		/// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
		// Token: 0x06000AAD RID: 2733 RVA: 0x00021B77 File Offset: 0x0001FD77
		[__DynamicallyInvokable]
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
		// Token: 0x06000AAE RID: 2734 RVA: 0x00021B80 File Offset: 0x0001FD80
		[__DynamicallyInvokable]
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt64(System.IFormatProvider)" />.</summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> object. (Specify <see langword="null" /> because the <paramref name="provider" /> parameter is ignored.)</param>
		/// <returns>The converted value of the current <see cref="T:System.Char" /> object.</returns>
		// Token: 0x06000AAF RID: 2735 RVA: 0x00021B89 File Offset: 0x0001FD89
		[__DynamicallyInvokable]
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		/// <summary>Note This conversion is not supported. Attempting to do so throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x06000AB0 RID: 2736 RVA: 0x00021B92 File Offset: 0x0001FD92
		[__DynamicallyInvokable]
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "Char", "Single" }));
		}

		/// <summary>Note This conversion is not supported. Attempting to do so throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x06000AB1 RID: 2737 RVA: 0x00021BB9 File Offset: 0x0001FDB9
		[__DynamicallyInvokable]
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "Char", "Double" }));
		}

		/// <summary>Note This conversion is not supported. Attempting to do so throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x06000AB2 RID: 2738 RVA: 0x00021BE0 File Offset: 0x0001FDE0
		[__DynamicallyInvokable]
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "Char", "Decimal" }));
		}

		/// <summary>Note This conversion is not supported. Attempting to do so throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x06000AB3 RID: 2739 RVA: 0x00021C07 File Offset: 0x0001FE07
		[__DynamicallyInvokable]
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "Char", "DateTime" }));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToType(System.Type,System.IFormatProvider)" />.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> object.</param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> object.</param>
		/// <returns>An object of the specified type.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The value of the current <see cref="T:System.Char" /> object cannot be converted to the type specified by the <paramref name="type" /> parameter.</exception>
		// Token: 0x06000AB4 RID: 2740 RVA: 0x00021C2E File Offset: 0x0001FE2E
		[__DynamicallyInvokable]
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as a control character.</summary>
		/// <param name="c">The Unicode character to evaluate.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="c" /> is a control character; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000AB5 RID: 2741 RVA: 0x00021C3E File Offset: 0x0001FE3E
		[__DynamicallyInvokable]
		public static bool IsControl(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.Control;
			}
			return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.Control;
		}

		/// <summary>Indicates whether the character at the specified position in a specified string is categorized as a control character.</summary>
		/// <param name="s">A string.</param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
		/// <returns>
		///   <see langword="true" /> if the character at position <paramref name="index" /> in <paramref name="s" /> is a control character; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
		// Token: 0x06000AB6 RID: 2742 RVA: 0x00021C60 File Offset: 0x0001FE60
		[__DynamicallyInvokable]
		public static bool IsControl(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (char.IsLatin1(c))
			{
				return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.Control;
			}
			return CharUnicodeInfo.GetUnicodeCategory(s, index) == UnicodeCategory.Control;
		}

		/// <summary>Indicates whether the character at the specified position in a specified string is categorized as a decimal digit.</summary>
		/// <param name="s">A string.</param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
		/// <returns>
		///   <see langword="true" /> if the character at position <paramref name="index" /> in <paramref name="s" /> is a decimal digit; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
		// Token: 0x06000AB7 RID: 2743 RVA: 0x00021CB8 File Offset: 0x0001FEB8
		[__DynamicallyInvokable]
		public static bool IsDigit(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (char.IsLatin1(c))
			{
				return c >= '0' && c <= '9';
			}
			return CharUnicodeInfo.GetUnicodeCategory(s, index) == UnicodeCategory.DecimalDigitNumber;
		}

		/// <summary>Indicates whether the character at the specified position in a specified string is categorized as a Unicode letter.</summary>
		/// <param name="s">A string.</param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
		/// <returns>
		///   <see langword="true" /> if the character at position <paramref name="index" /> in <paramref name="s" /> is a letter; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
		// Token: 0x06000AB8 RID: 2744 RVA: 0x00021D14 File Offset: 0x0001FF14
		[__DynamicallyInvokable]
		public static bool IsLetter(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (!char.IsLatin1(c))
			{
				return char.CheckLetter(CharUnicodeInfo.GetUnicodeCategory(s, index));
			}
			if (char.IsAscii(c))
			{
				c |= ' ';
				return c >= 'a' && c <= 'z';
			}
			return char.CheckLetter(char.GetLatin1UnicodeCategory(c));
		}

		/// <summary>Indicates whether the character at the specified position in a specified string is categorized as a letter or a decimal digit.</summary>
		/// <param name="s">A string.</param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
		/// <returns>
		///   <see langword="true" /> if the character at position <paramref name="index" /> in <paramref name="s" /> is a letter or a decimal digit; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
		// Token: 0x06000AB9 RID: 2745 RVA: 0x00021D8C File Offset: 0x0001FF8C
		[__DynamicallyInvokable]
		public static bool IsLetterOrDigit(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (char.IsLatin1(c))
			{
				return char.CheckLetterOrDigit(char.GetLatin1UnicodeCategory(c));
			}
			return char.CheckLetterOrDigit(CharUnicodeInfo.GetUnicodeCategory(s, index));
		}

		/// <summary>Indicates whether the character at the specified position in a specified string is categorized as a lowercase letter.</summary>
		/// <param name="s">A string.</param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
		/// <returns>
		///   <see langword="true" /> if the character at position <paramref name="index" /> in <paramref name="s" /> is a lowercase letter; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
		// Token: 0x06000ABA RID: 2746 RVA: 0x00021DE4 File Offset: 0x0001FFE4
		[__DynamicallyInvokable]
		public static bool IsLower(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (!char.IsLatin1(c))
			{
				return CharUnicodeInfo.GetUnicodeCategory(s, index) == UnicodeCategory.LowercaseLetter;
			}
			if (char.IsAscii(c))
			{
				return c >= 'a' && c <= 'z';
			}
			return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.LowercaseLetter;
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00021E4F File Offset: 0x0002004F
		internal static bool CheckNumber(UnicodeCategory uc)
		{
			return uc - UnicodeCategory.DecimalDigitNumber <= 2;
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as a number.</summary>
		/// <param name="c">The Unicode character to evaluate.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="c" /> is a number; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000ABC RID: 2748 RVA: 0x00021E5A File Offset: 0x0002005A
		[__DynamicallyInvokable]
		public static bool IsNumber(char c)
		{
			if (!char.IsLatin1(c))
			{
				return char.CheckNumber(CharUnicodeInfo.GetUnicodeCategory(c));
			}
			if (char.IsAscii(c))
			{
				return c >= '0' && c <= '9';
			}
			return char.CheckNumber(char.GetLatin1UnicodeCategory(c));
		}

		/// <summary>Indicates whether the character at the specified position in a specified string is categorized as a number.</summary>
		/// <param name="s">A string.</param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
		/// <returns>
		///   <see langword="true" /> if the character at position <paramref name="index" /> in <paramref name="s" /> is a number; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
		// Token: 0x06000ABD RID: 2749 RVA: 0x00021E94 File Offset: 0x00020094
		[__DynamicallyInvokable]
		public static bool IsNumber(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (!char.IsLatin1(c))
			{
				return char.CheckNumber(CharUnicodeInfo.GetUnicodeCategory(s, index));
			}
			if (char.IsAscii(c))
			{
				return c >= '0' && c <= '9';
			}
			return char.CheckNumber(char.GetLatin1UnicodeCategory(c));
		}

		/// <summary>Indicates whether the character at the specified position in a specified string is categorized as a punctuation mark.</summary>
		/// <param name="s">A string.</param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
		/// <returns>
		///   <see langword="true" /> if the character at position <paramref name="index" /> in <paramref name="s" /> is a punctuation mark; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
		// Token: 0x06000ABE RID: 2750 RVA: 0x00021F04 File Offset: 0x00020104
		[__DynamicallyInvokable]
		public static bool IsPunctuation(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (char.IsLatin1(c))
			{
				return char.CheckPunctuation(char.GetLatin1UnicodeCategory(c));
			}
			return char.CheckPunctuation(CharUnicodeInfo.GetUnicodeCategory(s, index));
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x00021F5B File Offset: 0x0002015B
		internal static bool CheckSeparator(UnicodeCategory uc)
		{
			return uc - UnicodeCategory.SpaceSeparator <= 2;
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00021F67 File Offset: 0x00020167
		private static bool IsSeparatorLatin1(char c)
		{
			return c == ' ' || c == '\u00a0';
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as a separator character.</summary>
		/// <param name="c">The Unicode character to evaluate.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="c" /> is a separator character; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000AC1 RID: 2753 RVA: 0x00021F78 File Offset: 0x00020178
		[__DynamicallyInvokable]
		public static bool IsSeparator(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.IsSeparatorLatin1(c);
			}
			return char.CheckSeparator(CharUnicodeInfo.GetUnicodeCategory(c));
		}

		/// <summary>Indicates whether the character at the specified position in a specified string is categorized as a separator character.</summary>
		/// <param name="s">A string.</param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
		/// <returns>
		///   <see langword="true" /> if the character at position <paramref name="index" /> in <paramref name="s" /> is a separator character; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
		// Token: 0x06000AC2 RID: 2754 RVA: 0x00021F94 File Offset: 0x00020194
		[__DynamicallyInvokable]
		public static bool IsSeparator(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (char.IsLatin1(c))
			{
				return char.IsSeparatorLatin1(c);
			}
			return char.CheckSeparator(CharUnicodeInfo.GetUnicodeCategory(s, index));
		}

		/// <summary>Indicates whether the specified character has a surrogate code unit.</summary>
		/// <param name="c">The Unicode character to evaluate.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="c" /> is either a high surrogate or a low surrogate; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000AC3 RID: 2755 RVA: 0x00021FE6 File Offset: 0x000201E6
		[__DynamicallyInvokable]
		public static bool IsSurrogate(char c)
		{
			return c >= '\ud800' && c <= '\udfff';
		}

		/// <summary>Indicates whether the character at the specified position in a specified string has a surrogate code unit.</summary>
		/// <param name="s">A string.</param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
		/// <returns>
		///   <see langword="true" /> if the character at position <paramref name="index" /> in <paramref name="s" /> is a either a high surrogate or a low surrogate; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
		// Token: 0x06000AC4 RID: 2756 RVA: 0x00021FFD File Offset: 0x000201FD
		[__DynamicallyInvokable]
		public static bool IsSurrogate(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return char.IsSurrogate(s[index]);
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0002202D File Offset: 0x0002022D
		internal static bool CheckSymbol(UnicodeCategory uc)
		{
			return uc - UnicodeCategory.MathSymbol <= 3;
		}

		/// <summary>Indicates whether the specified Unicode character is categorized as a symbol character.</summary>
		/// <param name="c">The Unicode character to evaluate.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="c" /> is a symbol character; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000AC6 RID: 2758 RVA: 0x00022039 File Offset: 0x00020239
		[__DynamicallyInvokable]
		public static bool IsSymbol(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.CheckSymbol(char.GetLatin1UnicodeCategory(c));
			}
			return char.CheckSymbol(CharUnicodeInfo.GetUnicodeCategory(c));
		}

		/// <summary>Indicates whether the character at the specified position in a specified string is categorized as a symbol character.</summary>
		/// <param name="s">A string.</param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
		/// <returns>
		///   <see langword="true" /> if the character at position <paramref name="index" /> in <paramref name="s" /> is a symbol character; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
		// Token: 0x06000AC7 RID: 2759 RVA: 0x0002205C File Offset: 0x0002025C
		[__DynamicallyInvokable]
		public static bool IsSymbol(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (char.IsLatin1(s[index]))
			{
				return char.CheckSymbol(char.GetLatin1UnicodeCategory(s[index]));
			}
			return char.CheckSymbol(CharUnicodeInfo.GetUnicodeCategory(s, index));
		}

		/// <summary>Indicates whether the character at the specified position in a specified string is categorized as an uppercase letter.</summary>
		/// <param name="s">A string.</param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
		/// <returns>
		///   <see langword="true" /> if the character at position <paramref name="index" /> in <paramref name="s" /> is an uppercase letter; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
		// Token: 0x06000AC8 RID: 2760 RVA: 0x000220B8 File Offset: 0x000202B8
		[__DynamicallyInvokable]
		public static bool IsUpper(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (!char.IsLatin1(c))
			{
				return CharUnicodeInfo.GetUnicodeCategory(s, index) == UnicodeCategory.UppercaseLetter;
			}
			if (char.IsAscii(c))
			{
				return c >= 'A' && c <= 'Z';
			}
			return char.GetLatin1UnicodeCategory(c) == UnicodeCategory.UppercaseLetter;
		}

		/// <summary>Indicates whether the character at the specified position in a specified string is categorized as white space.</summary>
		/// <param name="s">A string.</param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
		/// <returns>
		///   <see langword="true" /> if the character at position <paramref name="index" /> in <paramref name="s" /> is white space; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
		// Token: 0x06000AC9 RID: 2761 RVA: 0x00022124 File Offset: 0x00020324
		[__DynamicallyInvokable]
		public static bool IsWhiteSpace(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (char.IsLatin1(s[index]))
			{
				return char.IsWhiteSpaceLatin1(s[index]);
			}
			return CharUnicodeInfo.IsWhiteSpace(s, index);
		}

		/// <summary>Categorizes a specified Unicode character into a group identified by one of the <see cref="T:System.Globalization.UnicodeCategory" /> values.</summary>
		/// <param name="c">The Unicode character to categorize.</param>
		/// <returns>A <see cref="T:System.Globalization.UnicodeCategory" /> value that identifies the group that contains <paramref name="c" />.</returns>
		// Token: 0x06000ACA RID: 2762 RVA: 0x00022175 File Offset: 0x00020375
		public static UnicodeCategory GetUnicodeCategory(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.GetLatin1UnicodeCategory(c);
			}
			return CharUnicodeInfo.InternalGetUnicodeCategory((int)c);
		}

		/// <summary>Categorizes the character at the specified position in a specified string into a group identified by one of the <see cref="T:System.Globalization.UnicodeCategory" /> values.</summary>
		/// <param name="s">A <see cref="T:System.String" />.</param>
		/// <param name="index">The character position in <paramref name="s" />.</param>
		/// <returns>A <see cref="T:System.Globalization.UnicodeCategory" /> enumerated constant that identifies the group that contains the character at position <paramref name="index" /> in <paramref name="s" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
		// Token: 0x06000ACB RID: 2763 RVA: 0x0002218C File Offset: 0x0002038C
		public static UnicodeCategory GetUnicodeCategory(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (char.IsLatin1(s[index]))
			{
				return char.GetLatin1UnicodeCategory(s[index]);
			}
			return CharUnicodeInfo.InternalGetUnicodeCategory(s, index);
		}

		/// <summary>Converts the specified numeric Unicode character to a double-precision floating point number.</summary>
		/// <param name="c">The Unicode character to convert.</param>
		/// <returns>The numeric value of <paramref name="c" /> if that character represents a number; otherwise, -1.0.</returns>
		// Token: 0x06000ACC RID: 2764 RVA: 0x000221DD File Offset: 0x000203DD
		[__DynamicallyInvokable]
		public static double GetNumericValue(char c)
		{
			return CharUnicodeInfo.GetNumericValue(c);
		}

		/// <summary>Converts the numeric Unicode character at the specified position in a specified string to a double-precision floating point number.</summary>
		/// <param name="s">A <see cref="T:System.String" />.</param>
		/// <param name="index">The character position in <paramref name="s" />.</param>
		/// <returns>The numeric value of the character at position <paramref name="index" /> in <paramref name="s" /> if that character represents a number; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero or greater than the last position in <paramref name="s" />.</exception>
		// Token: 0x06000ACD RID: 2765 RVA: 0x000221E5 File Offset: 0x000203E5
		[__DynamicallyInvokable]
		public static double GetNumericValue(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return CharUnicodeInfo.GetNumericValue(s, index);
		}

		/// <summary>Indicates whether the specified <see cref="T:System.Char" /> object is a high surrogate.</summary>
		/// <param name="c">The Unicode character to evaluate.</param>
		/// <returns>
		///   <see langword="true" /> if the numeric value of the <paramref name="c" /> parameter ranges from U+D800 through U+DBFF; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000ACE RID: 2766 RVA: 0x00022210 File Offset: 0x00020410
		[__DynamicallyInvokable]
		public static bool IsHighSurrogate(char c)
		{
			return c >= '\ud800' && c <= '\udbff';
		}

		/// <summary>Indicates whether the <see cref="T:System.Char" /> object at the specified position in a string is a high surrogate.</summary>
		/// <param name="s">A string.</param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
		/// <returns>
		///   <see langword="true" /> if the numeric value of the specified character in the <paramref name="s" /> parameter ranges from U+D800 through U+DBFF; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a position within <paramref name="s" />.</exception>
		// Token: 0x06000ACF RID: 2767 RVA: 0x00022227 File Offset: 0x00020427
		[__DynamicallyInvokable]
		public static bool IsHighSurrogate(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return char.IsHighSurrogate(s[index]);
		}

		/// <summary>Indicates whether the specified <see cref="T:System.Char" /> object is a low surrogate.</summary>
		/// <param name="c">The character to evaluate.</param>
		/// <returns>
		///   <see langword="true" /> if the numeric value of the <paramref name="c" /> parameter ranges from U+DC00 through U+DFFF; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000AD0 RID: 2768 RVA: 0x0002225B File Offset: 0x0002045B
		[__DynamicallyInvokable]
		public static bool IsLowSurrogate(char c)
		{
			return c >= '\udc00' && c <= '\udfff';
		}

		/// <summary>Indicates whether the <see cref="T:System.Char" /> object at the specified position in a string is a low surrogate.</summary>
		/// <param name="s">A string.</param>
		/// <param name="index">The position of the character to evaluate in <paramref name="s" />.</param>
		/// <returns>
		///   <see langword="true" /> if the numeric value of the specified character in the <paramref name="s" /> parameter ranges from U+DC00 through U+DFFF; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a position within <paramref name="s" />.</exception>
		// Token: 0x06000AD1 RID: 2769 RVA: 0x00022272 File Offset: 0x00020472
		[__DynamicallyInvokable]
		public static bool IsLowSurrogate(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return char.IsLowSurrogate(s[index]);
		}

		/// <summary>Indicates whether two adjacent <see cref="T:System.Char" /> objects at a specified position in a string form a surrogate pair.</summary>
		/// <param name="s">A string.</param>
		/// <param name="index">The starting position of the pair of characters to evaluate within <paramref name="s" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="s" /> parameter includes adjacent characters at positions <paramref name="index" /> and <paramref name="index" /> + 1, and the numeric value of the character at position <paramref name="index" /> ranges from U+D800 through U+DBFF, and the numeric value of the character at position <paramref name="index" />+1 ranges from U+DC00 through U+DFFF; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a position within <paramref name="s" />.</exception>
		// Token: 0x06000AD2 RID: 2770 RVA: 0x000222A8 File Offset: 0x000204A8
		[__DynamicallyInvokable]
		public static bool IsSurrogatePair(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return index + 1 < s.Length && char.IsSurrogatePair(s[index], s[index + 1]);
		}

		/// <summary>Indicates whether the two specified <see cref="T:System.Char" /> objects form a surrogate pair.</summary>
		/// <param name="highSurrogate">The character to evaluate as the high surrogate of a surrogate pair.</param>
		/// <param name="lowSurrogate">The character to evaluate as the low surrogate of a surrogate pair.</param>
		/// <returns>
		///   <see langword="true" /> if the numeric value of the <paramref name="highSurrogate" /> parameter ranges from U+D800 through U+DBFF, and the numeric value of the <paramref name="lowSurrogate" /> parameter ranges from U+DC00 through U+DFFF; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000AD3 RID: 2771 RVA: 0x000222FD File Offset: 0x000204FD
		[__DynamicallyInvokable]
		public static bool IsSurrogatePair(char highSurrogate, char lowSurrogate)
		{
			return highSurrogate >= '\ud800' && highSurrogate <= '\udbff' && lowSurrogate >= '\udc00' && lowSurrogate <= '\udfff';
		}

		/// <summary>Converts the specified Unicode code point into a UTF-16 encoded string.</summary>
		/// <param name="utf32">A 21-bit Unicode code point.</param>
		/// <returns>A string consisting of one <see cref="T:System.Char" /> object or a surrogate pair of <see cref="T:System.Char" /> objects equivalent to the code point specified by the <paramref name="utf32" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="utf32" /> is not a valid 21-bit Unicode code point ranging from U+0 through U+10FFFF, excluding the surrogate pair range from U+D800 through U+DFFF.</exception>
		// Token: 0x06000AD4 RID: 2772 RVA: 0x00022328 File Offset: 0x00020528
		[__DynamicallyInvokable]
		public static string ConvertFromUtf32(int utf32)
		{
			if (utf32 < 0 || utf32 > 1114111 || (utf32 >= 55296 && utf32 <= 57343))
			{
				throw new ArgumentOutOfRangeException("utf32", Environment.GetResourceString("ArgumentOutOfRange_InvalidUTF32"));
			}
			if (utf32 < 65536)
			{
				return char.ToString((char)utf32);
			}
			utf32 -= 65536;
			return new string(new char[]
			{
				(char)(utf32 / 1024 + 55296),
				(char)(utf32 % 1024 + 56320)
			});
		}

		/// <summary>Converts the value of a UTF-16 encoded surrogate pair into a Unicode code point.</summary>
		/// <param name="highSurrogate">A high surrogate code unit (that is, a code unit ranging from U+D800 through U+DBFF).</param>
		/// <param name="lowSurrogate">A low surrogate code unit (that is, a code unit ranging from U+DC00 through U+DFFF).</param>
		/// <returns>The 21-bit Unicode code point represented by the <paramref name="highSurrogate" /> and <paramref name="lowSurrogate" /> parameters.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="highSurrogate" /> is not in the range U+D800 through U+DBFF, or <paramref name="lowSurrogate" /> is not in the range U+DC00 through U+DFFF.</exception>
		// Token: 0x06000AD5 RID: 2773 RVA: 0x000223B0 File Offset: 0x000205B0
		[__DynamicallyInvokable]
		public static int ConvertToUtf32(char highSurrogate, char lowSurrogate)
		{
			if (!char.IsHighSurrogate(highSurrogate))
			{
				throw new ArgumentOutOfRangeException("highSurrogate", Environment.GetResourceString("ArgumentOutOfRange_InvalidHighSurrogate"));
			}
			if (!char.IsLowSurrogate(lowSurrogate))
			{
				throw new ArgumentOutOfRangeException("lowSurrogate", Environment.GetResourceString("ArgumentOutOfRange_InvalidLowSurrogate"));
			}
			return (int)((highSurrogate - '\ud800') * 'Ѐ' + (lowSurrogate - '\udc00')) + 65536;
		}

		/// <summary>Converts the value of a UTF-16 encoded character or surrogate pair at a specified position in a string into a Unicode code point.</summary>
		/// <param name="s">A string that contains a character or surrogate pair.</param>
		/// <param name="index">The index position of the character or surrogate pair in <paramref name="s" />.</param>
		/// <returns>The 21-bit Unicode code point represented by the character or surrogate pair at the position in the <paramref name="s" /> parameter specified by the <paramref name="index" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is not a position within <paramref name="s" />.</exception>
		/// <exception cref="T:System.ArgumentException">The specified index position contains a surrogate pair, and either the first character in the pair is not a valid high surrogate or the second character in the pair is not a valid low surrogate.</exception>
		// Token: 0x06000AD6 RID: 2774 RVA: 0x00022414 File Offset: 0x00020614
		[__DynamicallyInvokable]
		public static int ConvertToUtf32(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			int num = (int)(s[index] - '\ud800');
			if (num < 0 || num > 2047)
			{
				return (int)s[index];
			}
			if (num > 1023)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidLowSurrogate", new object[] { index }), "s");
			}
			if (index >= s.Length - 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidHighSurrogate", new object[] { index }), "s");
			}
			int num2 = (int)(s[index + 1] - '\udc00');
			if (num2 >= 0 && num2 <= 1023)
			{
				return num * 1024 + num2 + 65536;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidHighSurrogate", new object[] { index }), "s");
		}

		// Token: 0x040003F7 RID: 1015
		internal char m_value;

		/// <summary>Represents the largest possible value of a <see cref="T:System.Char" />. This field is constant.</summary>
		// Token: 0x040003F8 RID: 1016
		[__DynamicallyInvokable]
		public const char MaxValue = '\uffff';

		/// <summary>Represents the smallest possible value of a <see cref="T:System.Char" />. This field is constant.</summary>
		// Token: 0x040003F9 RID: 1017
		[__DynamicallyInvokable]
		public const char MinValue = '\0';

		// Token: 0x040003FA RID: 1018
		private static readonly byte[] categoryForLatin1 = new byte[]
		{
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 11, 24, 24, 24, 26, 24, 24, 24,
			20, 21, 24, 25, 24, 19, 24, 24, 8, 8,
			8, 8, 8, 8, 8, 8, 8, 8, 24, 24,
			25, 25, 25, 24, 24, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 20, 24, 21, 27, 18, 27, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 20, 25, 21, 25, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			14, 14, 14, 14, 14, 14, 14, 14, 14, 14,
			11, 24, 26, 26, 26, 26, 28, 28, 27, 28,
			1, 22, 25, 19, 28, 27, 28, 25, 10, 10,
			27, 1, 28, 24, 27, 10, 1, 23, 10, 10,
			10, 24, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 25, 0, 0, 0, 0,
			0, 0, 0, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 1, 25, 1, 1,
			1, 1, 1, 1, 1, 1
		};

		// Token: 0x040003FB RID: 1019
		internal const int UNICODE_PLANE00_END = 65535;

		// Token: 0x040003FC RID: 1020
		internal const int UNICODE_PLANE01_START = 65536;

		// Token: 0x040003FD RID: 1021
		internal const int UNICODE_PLANE16_END = 1114111;

		// Token: 0x040003FE RID: 1022
		internal const int HIGH_SURROGATE_START = 55296;

		// Token: 0x040003FF RID: 1023
		internal const int LOW_SURROGATE_END = 57343;
	}
}
