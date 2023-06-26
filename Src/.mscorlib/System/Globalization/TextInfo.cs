using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;

namespace System.Globalization
{
	/// <summary>Defines text properties and behaviors, such as casing, that are specific to a writing system.</summary>
	// Token: 0x020003D2 RID: 978
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class TextInfo : ICloneable, IDeserializationCallback
	{
		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x060031B8 RID: 12728 RVA: 0x000BFC4D File Offset: 0x000BDE4D
		internal static TextInfo Invariant
		{
			get
			{
				if (TextInfo.s_Invariant == null)
				{
					TextInfo.s_Invariant = new TextInfo(CultureData.Invariant);
				}
				return TextInfo.s_Invariant;
			}
		}

		// Token: 0x060031B9 RID: 12729 RVA: 0x000BFC70 File Offset: 0x000BDE70
		internal TextInfo(CultureData cultureData)
		{
			this.m_cultureData = cultureData;
			this.m_cultureName = this.m_cultureData.CultureName;
			this.m_textInfoName = this.m_cultureData.STEXTINFO;
			IntPtr intPtr;
			this.m_dataHandle = CompareInfo.InternalInitSortHandle(this.m_textInfoName, out intPtr);
			this.m_handleOrigin = intPtr;
		}

		// Token: 0x060031BA RID: 12730 RVA: 0x000BFCC6 File Offset: 0x000BDEC6
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this.m_cultureData = null;
			this.m_cultureName = null;
		}

		// Token: 0x060031BB RID: 12731 RVA: 0x000BFCD8 File Offset: 0x000BDED8
		private void OnDeserialized()
		{
			if (this.m_cultureData == null)
			{
				if (this.m_cultureName == null)
				{
					if (this.customCultureName != null)
					{
						this.m_cultureName = this.customCultureName;
					}
					else if (this.m_win32LangID == 0)
					{
						this.m_cultureName = "ar-SA";
					}
					else
					{
						this.m_cultureName = CultureInfo.GetCultureInfo(this.m_win32LangID).m_cultureData.CultureName;
					}
				}
				this.m_cultureData = CultureInfo.GetCultureInfo(this.m_cultureName).m_cultureData;
				this.m_textInfoName = this.m_cultureData.STEXTINFO;
				IntPtr intPtr;
				this.m_dataHandle = CompareInfo.InternalInitSortHandle(this.m_textInfoName, out intPtr);
				this.m_handleOrigin = intPtr;
			}
		}

		// Token: 0x060031BC RID: 12732 RVA: 0x000BFD7F File Offset: 0x000BDF7F
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			this.OnDeserialized();
		}

		// Token: 0x060031BD RID: 12733 RVA: 0x000BFD87 File Offset: 0x000BDF87
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			this.m_useUserOverride = false;
			this.customCultureName = this.m_cultureName;
			this.m_win32LangID = CultureInfo.GetCultureInfo(this.m_cultureName).LCID;
		}

		// Token: 0x060031BE RID: 12734 RVA: 0x000BFDB2 File Offset: 0x000BDFB2
		internal static int GetHashCodeOrdinalIgnoreCase(string s)
		{
			return TextInfo.GetHashCodeOrdinalIgnoreCase(s, false, 0L);
		}

		// Token: 0x060031BF RID: 12735 RVA: 0x000BFDBD File Offset: 0x000BDFBD
		internal static int GetHashCodeOrdinalIgnoreCase(string s, bool forceRandomizedHashing, long additionalEntropy)
		{
			return TextInfo.Invariant.GetCaseInsensitiveHashCode(s, forceRandomizedHashing, additionalEntropy);
		}

		// Token: 0x060031C0 RID: 12736 RVA: 0x000BFDCC File Offset: 0x000BDFCC
		[SecuritySafeCritical]
		internal static bool TryFastFindStringOrdinalIgnoreCase(int searchFlags, string source, int startIndex, string value, int count, ref int foundIndex)
		{
			return TextInfo.InternalTryFindStringOrdinalIgnoreCase(searchFlags, source, count, startIndex, value, value.Length, ref foundIndex);
		}

		// Token: 0x060031C1 RID: 12737 RVA: 0x000BFDE1 File Offset: 0x000BDFE1
		[SecuritySafeCritical]
		internal static int CompareOrdinalIgnoreCase(string str1, string str2)
		{
			return TextInfo.InternalCompareStringOrdinalIgnoreCase(str1, 0, str2, 0, str1.Length, str2.Length);
		}

		// Token: 0x060031C2 RID: 12738 RVA: 0x000BFDF8 File Offset: 0x000BDFF8
		[SecuritySafeCritical]
		internal static int CompareOrdinalIgnoreCaseEx(string strA, int indexA, string strB, int indexB, int lengthA, int lengthB)
		{
			return TextInfo.InternalCompareStringOrdinalIgnoreCase(strA, indexA, strB, indexB, lengthA, lengthB);
		}

		// Token: 0x060031C3 RID: 12739 RVA: 0x000BFE08 File Offset: 0x000BE008
		internal static int IndexOfStringOrdinalIgnoreCase(string source, string value, int startIndex, int count)
		{
			if (source.Length == 0 && value.Length == 0)
			{
				return 0;
			}
			int num = -1;
			if (TextInfo.TryFastFindStringOrdinalIgnoreCase(4194304, source, startIndex, value, count, ref num))
			{
				return num;
			}
			int num2 = startIndex + count;
			int num3 = num2 - value.Length;
			while (startIndex <= num3)
			{
				if (TextInfo.CompareOrdinalIgnoreCaseEx(source, startIndex, value, 0, value.Length, value.Length) == 0)
				{
					return startIndex;
				}
				startIndex++;
			}
			return -1;
		}

		// Token: 0x060031C4 RID: 12740 RVA: 0x000BFE70 File Offset: 0x000BE070
		internal static int LastIndexOfStringOrdinalIgnoreCase(string source, string value, int startIndex, int count)
		{
			if (value.Length == 0)
			{
				return startIndex;
			}
			int num = -1;
			if (TextInfo.TryFastFindStringOrdinalIgnoreCase(8388608, source, startIndex, value, count, ref num))
			{
				return num;
			}
			int num2 = startIndex - count + 1;
			if (value.Length > 0)
			{
				startIndex -= value.Length - 1;
			}
			while (startIndex >= num2)
			{
				if (TextInfo.CompareOrdinalIgnoreCaseEx(source, startIndex, value, 0, value.Length, value.Length) == 0)
				{
					return startIndex;
				}
				startIndex--;
			}
			return -1;
		}

		/// <summary>Gets the American National Standards Institute (ANSI) code page used by the writing system represented by the current <see cref="T:System.Globalization.TextInfo" />.</summary>
		/// <returns>The ANSI code page used by the writing system represented by the current <see cref="T:System.Globalization.TextInfo" />.</returns>
		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x060031C5 RID: 12741 RVA: 0x000BFEDD File Offset: 0x000BE0DD
		public virtual int ANSICodePage
		{
			get
			{
				return this.m_cultureData.IDEFAULTANSICODEPAGE;
			}
		}

		/// <summary>Gets the original equipment manufacturer (OEM) code page used by the writing system represented by the current <see cref="T:System.Globalization.TextInfo" />.</summary>
		/// <returns>The OEM code page used by the writing system represented by the current <see cref="T:System.Globalization.TextInfo" />.</returns>
		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x060031C6 RID: 12742 RVA: 0x000BFEEA File Offset: 0x000BE0EA
		public virtual int OEMCodePage
		{
			get
			{
				return this.m_cultureData.IDEFAULTOEMCODEPAGE;
			}
		}

		/// <summary>Gets the Macintosh code page used by the writing system represented by the current <see cref="T:System.Globalization.TextInfo" />.</summary>
		/// <returns>The Macintosh code page used by the writing system represented by the current <see cref="T:System.Globalization.TextInfo" />.</returns>
		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x060031C7 RID: 12743 RVA: 0x000BFEF7 File Offset: 0x000BE0F7
		public virtual int MacCodePage
		{
			get
			{
				return this.m_cultureData.IDEFAULTMACCODEPAGE;
			}
		}

		/// <summary>Gets the Extended Binary Coded Decimal Interchange Code (EBCDIC) code page used by the writing system represented by the current <see cref="T:System.Globalization.TextInfo" />.</summary>
		/// <returns>The EBCDIC code page used by the writing system represented by the current <see cref="T:System.Globalization.TextInfo" />.</returns>
		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x060031C8 RID: 12744 RVA: 0x000BFF04 File Offset: 0x000BE104
		public virtual int EBCDICCodePage
		{
			get
			{
				return this.m_cultureData.IDEFAULTEBCDICCODEPAGE;
			}
		}

		/// <summary>Gets the culture identifier for the culture associated with the current <see cref="T:System.Globalization.TextInfo" /> object.</summary>
		/// <returns>A number that identifies the culture from which the current <see cref="T:System.Globalization.TextInfo" /> object was created.</returns>
		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x060031C9 RID: 12745 RVA: 0x000BFF11 File Offset: 0x000BE111
		[ComVisible(false)]
		public int LCID
		{
			get
			{
				return CultureInfo.GetCultureInfo(this.m_textInfoName).LCID;
			}
		}

		/// <summary>Gets the name of the culture associated with the current <see cref="T:System.Globalization.TextInfo" /> object.</summary>
		/// <returns>The name of a culture.</returns>
		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x060031CA RID: 12746 RVA: 0x000BFF23 File Offset: 0x000BE123
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public string CultureName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_textInfoName;
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Globalization.TextInfo" /> object is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Globalization.TextInfo" /> object is read-only; otherwise, <see langword="false" />.</returns>
		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x060031CB RID: 12747 RVA: 0x000BFF2B File Offset: 0x000BE12B
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public bool IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_isReadOnly;
			}
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Globalization.TextInfo" /> object.</summary>
		/// <returns>A new instance of <see cref="T:System.Object" /> that is the memberwise clone of the current <see cref="T:System.Globalization.TextInfo" /> object.</returns>
		// Token: 0x060031CC RID: 12748 RVA: 0x000BFF34 File Offset: 0x000BE134
		[ComVisible(false)]
		public virtual object Clone()
		{
			object obj = base.MemberwiseClone();
			((TextInfo)obj).SetReadOnlyState(false);
			return obj;
		}

		/// <summary>Returns a read-only version of the specified <see cref="T:System.Globalization.TextInfo" /> object.</summary>
		/// <param name="textInfo">A <see cref="T:System.Globalization.TextInfo" /> object.</param>
		/// <returns>The <see cref="T:System.Globalization.TextInfo" /> object specified by the <paramref name="textInfo" /> parameter, if <paramref name="textInfo" /> is read-only.  
		///  -or-  
		///  A read-only memberwise clone of the <see cref="T:System.Globalization.TextInfo" /> object specified by <paramref name="textInfo" />, if <paramref name="textInfo" /> is not read-only.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="textInfo" /> is null.</exception>
		// Token: 0x060031CD RID: 12749 RVA: 0x000BFF58 File Offset: 0x000BE158
		[ComVisible(false)]
		public static TextInfo ReadOnly(TextInfo textInfo)
		{
			if (textInfo == null)
			{
				throw new ArgumentNullException("textInfo");
			}
			if (textInfo.IsReadOnly)
			{
				return textInfo;
			}
			TextInfo textInfo2 = (TextInfo)textInfo.MemberwiseClone();
			textInfo2.SetReadOnlyState(true);
			return textInfo2;
		}

		// Token: 0x060031CE RID: 12750 RVA: 0x000BFF91 File Offset: 0x000BE191
		private void VerifyWritable()
		{
			if (this.m_isReadOnly)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
			}
		}

		// Token: 0x060031CF RID: 12751 RVA: 0x000BFFAB File Offset: 0x000BE1AB
		internal void SetReadOnlyState(bool readOnly)
		{
			this.m_isReadOnly = readOnly;
		}

		/// <summary>Gets or sets the string that separates items in a list.</summary>
		/// <returns>The string that separates items in a list.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value in a set operation is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">In a set operation, the current <see cref="T:System.Globalization.TextInfo" /> object is read-only.</exception>
		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x060031D0 RID: 12752 RVA: 0x000BFFB4 File Offset: 0x000BE1B4
		// (set) Token: 0x060031D1 RID: 12753 RVA: 0x000BFFD5 File Offset: 0x000BE1D5
		[__DynamicallyInvokable]
		public virtual string ListSeparator
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				if (this.m_listSeparator == null)
				{
					this.m_listSeparator = this.m_cultureData.SLIST;
				}
				return this.m_listSeparator;
			}
			[ComVisible(false)]
			[__DynamicallyInvokable]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value", Environment.GetResourceString("ArgumentNull_String"));
				}
				this.VerifyWritable();
				this.m_listSeparator = value;
			}
		}

		/// <summary>Converts the specified character to lowercase.</summary>
		/// <param name="c">The character to convert to lowercase.</param>
		/// <returns>The specified character converted to lowercase.</returns>
		// Token: 0x060031D2 RID: 12754 RVA: 0x000BFFFC File Offset: 0x000BE1FC
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public virtual char ToLower(char c)
		{
			if (TextInfo.IsAscii(c) && this.IsAsciiCasingSameAsInvariant)
			{
				return TextInfo.ToLowerAsciiInvariant(c);
			}
			return TextInfo.InternalChangeCaseChar(this.m_dataHandle, this.m_handleOrigin, this.m_textInfoName, c, false);
		}

		/// <summary>Converts the specified string to lowercase.</summary>
		/// <param name="str">The string to convert to lowercase.</param>
		/// <returns>The specified string converted to lowercase.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null.</exception>
		// Token: 0x060031D3 RID: 12755 RVA: 0x000C002E File Offset: 0x000BE22E
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public virtual string ToLower(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return TextInfo.InternalChangeCaseString(this.m_dataHandle, this.m_handleOrigin, this.m_textInfoName, str, false);
		}

		// Token: 0x060031D4 RID: 12756 RVA: 0x000C0057 File Offset: 0x000BE257
		private static char ToLowerAsciiInvariant(char c)
		{
			if ('A' <= c && c <= 'Z')
			{
				c |= ' ';
			}
			return c;
		}

		/// <summary>Converts the specified character to uppercase.</summary>
		/// <param name="c">The character to convert to uppercase.</param>
		/// <returns>The specified character converted to uppercase.</returns>
		// Token: 0x060031D5 RID: 12757 RVA: 0x000C006B File Offset: 0x000BE26B
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public virtual char ToUpper(char c)
		{
			if (TextInfo.IsAscii(c) && this.IsAsciiCasingSameAsInvariant)
			{
				return TextInfo.ToUpperAsciiInvariant(c);
			}
			return TextInfo.InternalChangeCaseChar(this.m_dataHandle, this.m_handleOrigin, this.m_textInfoName, c, true);
		}

		/// <summary>Converts the specified string to uppercase.</summary>
		/// <param name="str">The string to convert to uppercase.</param>
		/// <returns>The specified string converted to uppercase.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null.</exception>
		// Token: 0x060031D6 RID: 12758 RVA: 0x000C009D File Offset: 0x000BE29D
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public virtual string ToUpper(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return TextInfo.InternalChangeCaseString(this.m_dataHandle, this.m_handleOrigin, this.m_textInfoName, str, true);
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x000C00C6 File Offset: 0x000BE2C6
		private static char ToUpperAsciiInvariant(char c)
		{
			if ('a' <= c && c <= 'z')
			{
				c = (char)((int)c & -33);
			}
			return c;
		}

		// Token: 0x060031D8 RID: 12760 RVA: 0x000C00DA File Offset: 0x000BE2DA
		private static bool IsAscii(char c)
		{
			return c < '\u0080';
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x060031D9 RID: 12761 RVA: 0x000C00E4 File Offset: 0x000BE2E4
		private bool IsAsciiCasingSameAsInvariant
		{
			get
			{
				if (this.m_IsAsciiCasingSameAsInvariant == TextInfo.Tristate.NotInitialized)
				{
					this.m_IsAsciiCasingSameAsInvariant = ((CultureInfo.GetCultureInfo(this.m_textInfoName).CompareInfo.Compare("abcdefghijklmnopqrstuvwxyz", "ABCDEFGHIJKLMNOPQRSTUVWXYZ", CompareOptions.IgnoreCase) == 0) ? TextInfo.Tristate.True : TextInfo.Tristate.False);
				}
				return this.m_IsAsciiCasingSameAsInvariant == TextInfo.Tristate.True;
			}
		}

		/// <summary>Determines whether the specified object represents the same writing system as the current <see cref="T:System.Globalization.TextInfo" /> object.</summary>
		/// <param name="obj">The object to compare with the current <see cref="T:System.Globalization.TextInfo" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> represents the same writing system as the current <see cref="T:System.Globalization.TextInfo" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060031DA RID: 12762 RVA: 0x000C0124 File Offset: 0x000BE324
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			TextInfo textInfo = obj as TextInfo;
			return textInfo != null && this.CultureName.Equals(textInfo.CultureName);
		}

		/// <summary>Serves as a hash function for the current <see cref="T:System.Globalization.TextInfo" />, suitable for hashing algorithms and data structures, such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Globalization.TextInfo" />.</returns>
		// Token: 0x060031DB RID: 12763 RVA: 0x000C014E File Offset: 0x000BE34E
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.CultureName.GetHashCode();
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Globalization.TextInfo" />.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Globalization.TextInfo" />.</returns>
		// Token: 0x060031DC RID: 12764 RVA: 0x000C015B File Offset: 0x000BE35B
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return "TextInfo - " + this.m_cultureData.CultureName;
		}

		/// <summary>Converts the specified string to title case (except for words that are entirely in uppercase, which are considered to be acronyms).</summary>
		/// <param name="str">The string to convert to title case.</param>
		/// <returns>The specified string converted to title case.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is <see langword="null" />.</exception>
		// Token: 0x060031DD RID: 12765 RVA: 0x000C0174 File Offset: 0x000BE374
		public string ToTitleCase(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (str.Length == 0)
			{
				return str;
			}
			StringBuilder stringBuilder = new StringBuilder();
			string text = null;
			for (int i = 0; i < str.Length; i++)
			{
				int num;
				UnicodeCategory unicodeCategory = CharUnicodeInfo.InternalGetUnicodeCategory(str, i, out num);
				if (char.CheckLetter(unicodeCategory))
				{
					i = this.AddTitlecaseLetter(ref stringBuilder, ref str, i, num) + 1;
					int num2 = i;
					bool flag = unicodeCategory == UnicodeCategory.LowercaseLetter;
					while (i < str.Length)
					{
						unicodeCategory = CharUnicodeInfo.InternalGetUnicodeCategory(str, i, out num);
						if (TextInfo.IsLetterCategory(unicodeCategory))
						{
							if (unicodeCategory == UnicodeCategory.LowercaseLetter)
							{
								flag = true;
							}
							i += num;
						}
						else if (str[i] == '\'')
						{
							i++;
							if (flag)
							{
								if (text == null)
								{
									text = this.ToLower(str);
								}
								stringBuilder.Append(text, num2, i - num2);
							}
							else
							{
								stringBuilder.Append(str, num2, i - num2);
							}
							num2 = i;
							flag = true;
						}
						else
						{
							if (TextInfo.IsWordSeparator(unicodeCategory))
							{
								break;
							}
							i += num;
						}
					}
					int num3 = i - num2;
					if (num3 > 0)
					{
						if (flag)
						{
							if (text == null)
							{
								text = this.ToLower(str);
							}
							stringBuilder.Append(text, num2, num3);
						}
						else
						{
							stringBuilder.Append(str, num2, num3);
						}
					}
					if (i < str.Length)
					{
						i = TextInfo.AddNonLetter(ref stringBuilder, ref str, i, num);
					}
				}
				else
				{
					i = TextInfo.AddNonLetter(ref stringBuilder, ref str, i, num);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060031DE RID: 12766 RVA: 0x000C02C1 File Offset: 0x000BE4C1
		private static int AddNonLetter(ref StringBuilder result, ref string input, int inputIndex, int charLen)
		{
			if (charLen == 2)
			{
				result.Append(input[inputIndex++]);
				result.Append(input[inputIndex]);
			}
			else
			{
				result.Append(input[inputIndex]);
			}
			return inputIndex;
		}

		// Token: 0x060031DF RID: 12767 RVA: 0x000C0300 File Offset: 0x000BE500
		private int AddTitlecaseLetter(ref StringBuilder result, ref string input, int inputIndex, int charLen)
		{
			if (charLen == 2)
			{
				result.Append(this.ToUpper(input.Substring(inputIndex, charLen)));
				inputIndex++;
			}
			else
			{
				char c = input[inputIndex];
				switch (c)
				{
				case 'Ǆ':
				case 'ǅ':
				case 'ǆ':
					result.Append('ǅ');
					break;
				case 'Ǉ':
				case 'ǈ':
				case 'ǉ':
					result.Append('ǈ');
					break;
				case 'Ǌ':
				case 'ǋ':
				case 'ǌ':
					result.Append('ǋ');
					break;
				default:
					switch (c)
					{
					case 'Ǳ':
					case 'ǲ':
					case 'ǳ':
						result.Append('ǲ');
						break;
					default:
						result.Append(this.ToUpper(input[inputIndex]));
						break;
					}
					break;
				}
			}
			return inputIndex;
		}

		// Token: 0x060031E0 RID: 12768 RVA: 0x000C03DA File Offset: 0x000BE5DA
		private static bool IsWordSeparator(UnicodeCategory category)
		{
			return (536672256 & (1 << (int)category)) != 0;
		}

		// Token: 0x060031E1 RID: 12769 RVA: 0x000C03EB File Offset: 0x000BE5EB
		private static bool IsLetterCategory(UnicodeCategory uc)
		{
			return uc == UnicodeCategory.UppercaseLetter || uc == UnicodeCategory.LowercaseLetter || uc == UnicodeCategory.TitlecaseLetter || uc == UnicodeCategory.ModifierLetter || uc == UnicodeCategory.OtherLetter;
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Globalization.TextInfo" /> object represents a writing system where text flows from right to left.</summary>
		/// <returns>
		///   <see langword="true" /> if text flows from right to left; otherwise, <see langword="false" />.</returns>
		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x060031E2 RID: 12770 RVA: 0x000C0402 File Offset: 0x000BE602
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public bool IsRightToLeft
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_cultureData.IsRightToLeft;
			}
		}

		/// <summary>Raises the deserialization event when deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		// Token: 0x060031E3 RID: 12771 RVA: 0x000C040F File Offset: 0x000BE60F
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this.OnDeserialized();
		}

		// Token: 0x060031E4 RID: 12772 RVA: 0x000C0417 File Offset: 0x000BE617
		[SecuritySafeCritical]
		internal int GetCaseInsensitiveHashCode(string str)
		{
			return this.GetCaseInsensitiveHashCode(str, false, 0L);
		}

		// Token: 0x060031E5 RID: 12773 RVA: 0x000C0423 File Offset: 0x000BE623
		[SecuritySafeCritical]
		internal int GetCaseInsensitiveHashCode(string str, bool forceRandomizedHashing, long additionalEntropy)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return TextInfo.InternalGetCaseInsHash(this.m_dataHandle, this.m_handleOrigin, this.m_textInfoName, str, forceRandomizedHashing, additionalEntropy);
		}

		// Token: 0x060031E6 RID: 12774
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern char InternalChangeCaseChar(IntPtr handle, IntPtr handleOrigin, string localeName, char ch, bool isToUpper);

		// Token: 0x060031E7 RID: 12775
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string InternalChangeCaseString(IntPtr handle, IntPtr handleOrigin, string localeName, string str, bool isToUpper);

		// Token: 0x060031E8 RID: 12776
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int InternalGetCaseInsHash(IntPtr handle, IntPtr handleOrigin, string localeName, string str, bool forceRandomizedHashing, long additionalEntropy);

		// Token: 0x060031E9 RID: 12777
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int InternalCompareStringOrdinalIgnoreCase(string string1, int index1, string string2, int index2, int length1, int length2);

		// Token: 0x060031EA RID: 12778
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool InternalTryFindStringOrdinalIgnoreCase(int searchFlags, string source, int sourceCount, int startIndex, string target, int targetCount, ref int foundIndex);

		// Token: 0x04001536 RID: 5430
		[OptionalField(VersionAdded = 2)]
		private string m_listSeparator;

		// Token: 0x04001537 RID: 5431
		[OptionalField(VersionAdded = 2)]
		private bool m_isReadOnly;

		// Token: 0x04001538 RID: 5432
		[OptionalField(VersionAdded = 3)]
		private string m_cultureName;

		// Token: 0x04001539 RID: 5433
		[NonSerialized]
		private CultureData m_cultureData;

		// Token: 0x0400153A RID: 5434
		[NonSerialized]
		private string m_textInfoName;

		// Token: 0x0400153B RID: 5435
		[NonSerialized]
		private IntPtr m_dataHandle;

		// Token: 0x0400153C RID: 5436
		[NonSerialized]
		private IntPtr m_handleOrigin;

		// Token: 0x0400153D RID: 5437
		[NonSerialized]
		private TextInfo.Tristate m_IsAsciiCasingSameAsInvariant;

		// Token: 0x0400153E RID: 5438
		internal static volatile TextInfo s_Invariant;

		// Token: 0x0400153F RID: 5439
		[OptionalField(VersionAdded = 2)]
		private string customCultureName;

		// Token: 0x04001540 RID: 5440
		[OptionalField(VersionAdded = 1)]
		internal int m_nDataItem;

		// Token: 0x04001541 RID: 5441
		[OptionalField(VersionAdded = 1)]
		internal bool m_useUserOverride;

		// Token: 0x04001542 RID: 5442
		[OptionalField(VersionAdded = 1)]
		internal int m_win32LangID;

		// Token: 0x04001543 RID: 5443
		private const int wordSeparatorMask = 536672256;

		// Token: 0x02000B6C RID: 2924
		private enum Tristate : byte
		{
			// Token: 0x04003471 RID: 13425
			NotInitialized,
			// Token: 0x04003472 RID: 13426
			True,
			// Token: 0x04003473 RID: 13427
			False
		}
	}
}
