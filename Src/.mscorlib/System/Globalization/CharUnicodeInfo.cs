using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Globalization
{
	/// <summary>Retrieves information about a Unicode character. This class cannot be inherited.</summary>
	// Token: 0x020003A4 RID: 932
	[__DynamicallyInvokable]
	public static class CharUnicodeInfo
	{
		// Token: 0x06002E03 RID: 11779 RVA: 0x000B135C File Offset: 0x000AF55C
		[SecuritySafeCritical]
		private unsafe static bool InitTable()
		{
			byte* globalizationResourceBytePtr = GlobalizationAssembly.GetGlobalizationResourceBytePtr(typeof(CharUnicodeInfo).Assembly, "charinfo.nlp");
			CharUnicodeInfo.UnicodeDataHeader* ptr = (CharUnicodeInfo.UnicodeDataHeader*)globalizationResourceBytePtr;
			CharUnicodeInfo.s_pCategoryLevel1Index = (ushort*)(globalizationResourceBytePtr + ptr->OffsetToCategoriesIndex);
			CharUnicodeInfo.s_pCategoriesValue = globalizationResourceBytePtr + ptr->OffsetToCategoriesValue;
			CharUnicodeInfo.s_pNumericLevel1Index = (ushort*)(globalizationResourceBytePtr + ptr->OffsetToNumbericIndex);
			CharUnicodeInfo.s_pNumericValues = globalizationResourceBytePtr + ptr->OffsetToNumbericValue;
			CharUnicodeInfo.s_pDigitValues = (CharUnicodeInfo.DigitValues*)(globalizationResourceBytePtr + ptr->OffsetToDigitValue);
			return true;
		}

		// Token: 0x06002E04 RID: 11780 RVA: 0x000B13CC File Offset: 0x000AF5CC
		internal static int InternalConvertToUtf32(string s, int index)
		{
			if (index < s.Length - 1)
			{
				int num = (int)(s[index] - '\ud800');
				if (num >= 0 && num <= 1023)
				{
					int num2 = (int)(s[index + 1] - '\udc00');
					if (num2 >= 0 && num2 <= 1023)
					{
						return num * 1024 + num2 + 65536;
					}
				}
			}
			return (int)s[index];
		}

		// Token: 0x06002E05 RID: 11781 RVA: 0x000B1434 File Offset: 0x000AF634
		internal static int InternalConvertToUtf32(string s, int index, out int charLength)
		{
			charLength = 1;
			if (index < s.Length - 1)
			{
				int num = (int)(s[index] - '\ud800');
				if (num >= 0 && num <= 1023)
				{
					int num2 = (int)(s[index + 1] - '\udc00');
					if (num2 >= 0 && num2 <= 1023)
					{
						charLength++;
						return num * 1024 + num2 + 65536;
					}
				}
			}
			return (int)s[index];
		}

		// Token: 0x06002E06 RID: 11782 RVA: 0x000B14A4 File Offset: 0x000AF6A4
		internal static bool IsWhiteSpace(string s, int index)
		{
			UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(s, index);
			return unicodeCategory - UnicodeCategory.SpaceSeparator <= 2;
		}

		// Token: 0x06002E07 RID: 11783 RVA: 0x000B14C4 File Offset: 0x000AF6C4
		internal static bool IsWhiteSpace(char c)
		{
			UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
			return unicodeCategory - UnicodeCategory.SpaceSeparator <= 2;
		}

		// Token: 0x06002E08 RID: 11784 RVA: 0x000B14E4 File Offset: 0x000AF6E4
		[SecuritySafeCritical]
		internal unsafe static double InternalGetNumericValue(int ch)
		{
			ushort num = CharUnicodeInfo.s_pNumericLevel1Index[ch >> 8];
			num = CharUnicodeInfo.s_pNumericLevel1Index[(int)num + ((ch >> 4) & 15)];
			byte* ptr = (byte*)(CharUnicodeInfo.s_pNumericLevel1Index + num);
			byte* ptr2 = CharUnicodeInfo.s_pNumericValues + ptr[ch & 15] * 8;
			if (ptr2 % 8L != null)
			{
				double num2;
				byte* ptr3 = (byte*)(&num2);
				Buffer.Memcpy(ptr3, ptr2, 8);
				return num2;
			}
			return *(double*)(CharUnicodeInfo.s_pNumericValues + (IntPtr)ptr[ch & 15] * 8);
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x000B1558 File Offset: 0x000AF758
		[SecuritySafeCritical]
		internal unsafe static CharUnicodeInfo.DigitValues* InternalGetDigitValues(int ch)
		{
			ushort num = CharUnicodeInfo.s_pNumericLevel1Index[ch >> 8];
			num = CharUnicodeInfo.s_pNumericLevel1Index[(int)num + ((ch >> 4) & 15)];
			byte* ptr = (byte*)(CharUnicodeInfo.s_pNumericLevel1Index + num);
			return CharUnicodeInfo.s_pDigitValues + ptr[ch & 15];
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x000B15A8 File Offset: 0x000AF7A8
		[SecuritySafeCritical]
		internal unsafe static sbyte InternalGetDecimalDigitValue(int ch)
		{
			return CharUnicodeInfo.InternalGetDigitValues(ch)->decimalDigit;
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x000B15B5 File Offset: 0x000AF7B5
		[SecuritySafeCritical]
		internal unsafe static sbyte InternalGetDigitValue(int ch)
		{
			return CharUnicodeInfo.InternalGetDigitValues(ch)->digit;
		}

		/// <summary>Gets the numeric value associated with the specified character.</summary>
		/// <param name="ch">The Unicode character for which to get the numeric value.</param>
		/// <returns>The numeric value associated with the specified character.  
		///  -or-  
		///  -1, if the specified character is not a numeric character.</returns>
		// Token: 0x06002E0C RID: 11788 RVA: 0x000B15C2 File Offset: 0x000AF7C2
		[__DynamicallyInvokable]
		public static double GetNumericValue(char ch)
		{
			return CharUnicodeInfo.InternalGetNumericValue((int)ch);
		}

		/// <summary>Gets the numeric value associated with the character at the specified index of the specified string.</summary>
		/// <param name="s">The <see cref="T:System.String" /> containing the Unicode character for which to get the numeric value.</param>
		/// <param name="index">The index of the Unicode character for which to get the numeric value.</param>
		/// <returns>The numeric value associated with the character at the specified index of the specified string.  
		///  -or-  
		///  -1, if the character at the specified index of the specified string is not a numeric character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes in <paramref name="s" />.</exception>
		// Token: 0x06002E0D RID: 11789 RVA: 0x000B15CA File Offset: 0x000AF7CA
		[__DynamicallyInvokable]
		public static double GetNumericValue(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			return CharUnicodeInfo.InternalGetNumericValue(CharUnicodeInfo.InternalConvertToUtf32(s, index));
		}

		/// <summary>Gets the decimal digit value of the specified numeric character.</summary>
		/// <param name="ch">The Unicode character for which to get the decimal digit value.</param>
		/// <returns>The decimal digit value of the specified numeric character.  
		///  -or-  
		///  -1, if the specified character is not a decimal digit.</returns>
		// Token: 0x06002E0E RID: 11790 RVA: 0x000B1608 File Offset: 0x000AF808
		public static int GetDecimalDigitValue(char ch)
		{
			return (int)CharUnicodeInfo.InternalGetDecimalDigitValue((int)ch);
		}

		/// <summary>Gets the decimal digit value of the numeric character at the specified index of the specified string.</summary>
		/// <param name="s">The <see cref="T:System.String" /> containing the Unicode character for which to get the decimal digit value.</param>
		/// <param name="index">The index of the Unicode character for which to get the decimal digit value.</param>
		/// <returns>The decimal digit value of the numeric character at the specified index of the specified string.  
		///  -or-  
		///  -1, if the character at the specified index of the specified string is not a decimal digit.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes in <paramref name="s" />.</exception>
		// Token: 0x06002E0F RID: 11791 RVA: 0x000B1610 File Offset: 0x000AF810
		public static int GetDecimalDigitValue(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			return (int)CharUnicodeInfo.InternalGetDecimalDigitValue(CharUnicodeInfo.InternalConvertToUtf32(s, index));
		}

		/// <summary>Gets the digit value of the specified numeric character.</summary>
		/// <param name="ch">The Unicode character for which to get the digit value.</param>
		/// <returns>The digit value of the specified numeric character.  
		///  -or-  
		///  -1, if the specified character is not a digit.</returns>
		// Token: 0x06002E10 RID: 11792 RVA: 0x000B164E File Offset: 0x000AF84E
		public static int GetDigitValue(char ch)
		{
			return (int)CharUnicodeInfo.InternalGetDigitValue((int)ch);
		}

		/// <summary>Gets the digit value of the numeric character at the specified index of the specified string.</summary>
		/// <param name="s">The <see cref="T:System.String" /> containing the Unicode character for which to get the digit value.</param>
		/// <param name="index">The index of the Unicode character for which to get the digit value.</param>
		/// <returns>The digit value of the numeric character at the specified index of the specified string.  
		///  -or-  
		///  -1, if the character at the specified index of the specified string is not a digit.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes in <paramref name="s" />.</exception>
		// Token: 0x06002E11 RID: 11793 RVA: 0x000B1656 File Offset: 0x000AF856
		public static int GetDigitValue(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			return (int)CharUnicodeInfo.InternalGetDigitValue(CharUnicodeInfo.InternalConvertToUtf32(s, index));
		}

		/// <summary>Gets the Unicode category of the specified character.</summary>
		/// <param name="ch">The Unicode character for which to get the Unicode category.</param>
		/// <returns>A <see cref="T:System.Globalization.UnicodeCategory" /> value indicating the category of the specified character.</returns>
		// Token: 0x06002E12 RID: 11794 RVA: 0x000B1694 File Offset: 0x000AF894
		[__DynamicallyInvokable]
		public static UnicodeCategory GetUnicodeCategory(char ch)
		{
			return CharUnicodeInfo.InternalGetUnicodeCategory((int)ch);
		}

		/// <summary>Gets the Unicode category of the character at the specified index of the specified string.</summary>
		/// <param name="s">The <see cref="T:System.String" /> containing the Unicode character for which to get the Unicode category.</param>
		/// <param name="index">The index of the Unicode character for which to get the Unicode category.</param>
		/// <returns>A <see cref="T:System.Globalization.UnicodeCategory" /> value indicating the category of the character at the specified index of the specified string.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes in <paramref name="s" />.</exception>
		// Token: 0x06002E13 RID: 11795 RVA: 0x000B169C File Offset: 0x000AF89C
		[__DynamicallyInvokable]
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
			return CharUnicodeInfo.InternalGetUnicodeCategory(s, index);
		}

		// Token: 0x06002E14 RID: 11796 RVA: 0x000B16C7 File Offset: 0x000AF8C7
		internal static UnicodeCategory InternalGetUnicodeCategory(int ch)
		{
			return (UnicodeCategory)CharUnicodeInfo.InternalGetCategoryValue(ch, 0);
		}

		// Token: 0x06002E15 RID: 11797 RVA: 0x000B16D0 File Offset: 0x000AF8D0
		[SecuritySafeCritical]
		internal unsafe static byte InternalGetCategoryValue(int ch, int offset)
		{
			ushort num = CharUnicodeInfo.s_pCategoryLevel1Index[ch >> 8];
			num = CharUnicodeInfo.s_pCategoryLevel1Index[(int)num + ((ch >> 4) & 15)];
			byte* ptr = (byte*)(CharUnicodeInfo.s_pCategoryLevel1Index + num);
			byte b = ptr[ch & 15];
			return CharUnicodeInfo.s_pCategoriesValue[(int)(b * 2) + offset];
		}

		// Token: 0x06002E16 RID: 11798 RVA: 0x000B1720 File Offset: 0x000AF920
		internal static BidiCategory GetBidiCategory(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return (BidiCategory)CharUnicodeInfo.InternalGetCategoryValue(CharUnicodeInfo.InternalConvertToUtf32(s, index), 1);
		}

		// Token: 0x06002E17 RID: 11799 RVA: 0x000B1751 File Offset: 0x000AF951
		internal static UnicodeCategory InternalGetUnicodeCategory(string value, int index)
		{
			return CharUnicodeInfo.InternalGetUnicodeCategory(CharUnicodeInfo.InternalConvertToUtf32(value, index));
		}

		// Token: 0x06002E18 RID: 11800 RVA: 0x000B175F File Offset: 0x000AF95F
		internal static UnicodeCategory InternalGetUnicodeCategory(string str, int index, out int charLength)
		{
			return CharUnicodeInfo.InternalGetUnicodeCategory(CharUnicodeInfo.InternalConvertToUtf32(str, index, out charLength));
		}

		// Token: 0x06002E19 RID: 11801 RVA: 0x000B176E File Offset: 0x000AF96E
		internal static bool IsCombiningCategory(UnicodeCategory uc)
		{
			return uc == UnicodeCategory.NonSpacingMark || uc == UnicodeCategory.SpacingCombiningMark || uc == UnicodeCategory.EnclosingMark;
		}

		// Token: 0x04001307 RID: 4871
		internal const char HIGH_SURROGATE_START = '\ud800';

		// Token: 0x04001308 RID: 4872
		internal const char HIGH_SURROGATE_END = '\udbff';

		// Token: 0x04001309 RID: 4873
		internal const char LOW_SURROGATE_START = '\udc00';

		// Token: 0x0400130A RID: 4874
		internal const char LOW_SURROGATE_END = '\udfff';

		// Token: 0x0400130B RID: 4875
		internal const int UNICODE_CATEGORY_OFFSET = 0;

		// Token: 0x0400130C RID: 4876
		internal const int BIDI_CATEGORY_OFFSET = 1;

		// Token: 0x0400130D RID: 4877
		private static bool s_initialized = CharUnicodeInfo.InitTable();

		// Token: 0x0400130E RID: 4878
		[SecurityCritical]
		private unsafe static ushort* s_pCategoryLevel1Index;

		// Token: 0x0400130F RID: 4879
		[SecurityCritical]
		private unsafe static byte* s_pCategoriesValue;

		// Token: 0x04001310 RID: 4880
		[SecurityCritical]
		private unsafe static ushort* s_pNumericLevel1Index;

		// Token: 0x04001311 RID: 4881
		[SecurityCritical]
		private unsafe static byte* s_pNumericValues;

		// Token: 0x04001312 RID: 4882
		[SecurityCritical]
		private unsafe static CharUnicodeInfo.DigitValues* s_pDigitValues;

		// Token: 0x04001313 RID: 4883
		internal const string UNICODE_INFO_FILE_NAME = "charinfo.nlp";

		// Token: 0x04001314 RID: 4884
		internal const int UNICODE_PLANE01_START = 65536;

		// Token: 0x02000B65 RID: 2917
		[StructLayout(LayoutKind.Explicit)]
		internal struct UnicodeDataHeader
		{
			// Token: 0x04003453 RID: 13395
			[FieldOffset(0)]
			internal char TableName;

			// Token: 0x04003454 RID: 13396
			[FieldOffset(32)]
			internal ushort version;

			// Token: 0x04003455 RID: 13397
			[FieldOffset(40)]
			internal uint OffsetToCategoriesIndex;

			// Token: 0x04003456 RID: 13398
			[FieldOffset(44)]
			internal uint OffsetToCategoriesValue;

			// Token: 0x04003457 RID: 13399
			[FieldOffset(48)]
			internal uint OffsetToNumbericIndex;

			// Token: 0x04003458 RID: 13400
			[FieldOffset(52)]
			internal uint OffsetToDigitValue;

			// Token: 0x04003459 RID: 13401
			[FieldOffset(56)]
			internal uint OffsetToNumbericValue;
		}

		// Token: 0x02000B66 RID: 2918
		[StructLayout(LayoutKind.Sequential, Pack = 2)]
		internal struct DigitValues
		{
			// Token: 0x0400345A RID: 13402
			internal sbyte decimalDigit;

			// Token: 0x0400345B RID: 13403
			internal sbyte digit;
		}
	}
}
