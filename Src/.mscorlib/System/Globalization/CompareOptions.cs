﻿using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Defines the string comparison options to use with <see cref="T:System.Globalization.CompareInfo" />.</summary>
	// Token: 0x020003A5 RID: 933
	[Flags]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum CompareOptions
	{
		/// <summary>Indicates the default option settings for string comparisons.</summary>
		// Token: 0x04001316 RID: 4886
		[__DynamicallyInvokable]
		None = 0,
		/// <summary>Indicates that the string comparison must ignore case.</summary>
		// Token: 0x04001317 RID: 4887
		[__DynamicallyInvokable]
		IgnoreCase = 1,
		/// <summary>Indicates that the string comparison must ignore nonspacing combining characters, such as diacritics. The Unicode Standard defines combining characters as characters that are combined with base characters to produce a new character. Nonspacing combining characters do not occupy a spacing position by themselves when rendered.</summary>
		// Token: 0x04001318 RID: 4888
		[__DynamicallyInvokable]
		IgnoreNonSpace = 2,
		/// <summary>Indicates that the string comparison must ignore symbols, such as white-space characters, punctuation, currency symbols, the percent sign, mathematical symbols, the ampersand, and so on.</summary>
		// Token: 0x04001319 RID: 4889
		[__DynamicallyInvokable]
		IgnoreSymbols = 4,
		/// <summary>Indicates that the string comparison must ignore the Kana type. Kana type refers to Japanese hiragana and katakana characters, which represent phonetic sounds in the Japanese language. Hiragana is used for native Japanese expressions and words, while katakana is used for words borrowed from other languages, such as "computer" or "Internet". A phonetic sound can be expressed in both hiragana and katakana. If this value is selected, the hiragana character for one sound is considered equal to the katakana character for the same sound.</summary>
		// Token: 0x0400131A RID: 4890
		[__DynamicallyInvokable]
		IgnoreKanaType = 8,
		/// <summary>Indicates that the string comparison must ignore the character width. For example, Japanese katakana characters can be written as full-width or half-width. If this value is selected, the katakana characters written as full-width are considered equal to the same characters written as half-width.</summary>
		// Token: 0x0400131B RID: 4891
		[__DynamicallyInvokable]
		IgnoreWidth = 16,
		/// <summary>String comparison must ignore case, then perform an ordinal comparison. This technique is equivalent to converting the string to uppercase using the invariant culture and then performing an ordinal comparison on the result.</summary>
		// Token: 0x0400131C RID: 4892
		[__DynamicallyInvokable]
		OrdinalIgnoreCase = 268435456,
		/// <summary>Indicates that the string comparison must use the string sort algorithm. In a string sort, the hyphen and the apostrophe, as well as other nonalphanumeric symbols, come before alphanumeric characters.</summary>
		// Token: 0x0400131D RID: 4893
		[__DynamicallyInvokable]
		StringSort = 536870912,
		/// <summary>Indicates that the string comparison must use successive Unicode UTF-16 encoded values of the string (code unit by code unit comparison), leading to a fast comparison but one that is culture-insensitive. A string starting with a code unit XXXX16 comes before a string starting with YYYY16, if XXXX16 is less than YYYY16. This value cannot be combined with other <see cref="T:System.Globalization.CompareOptions" /> values and must be used alone.</summary>
		// Token: 0x0400131E RID: 4894
		[__DynamicallyInvokable]
		Ordinal = 1073741824
	}
}
