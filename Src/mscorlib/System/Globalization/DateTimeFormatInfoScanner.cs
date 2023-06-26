using System;
using System.Collections.Generic;
using System.Text;

namespace System.Globalization
{
	// Token: 0x020003B1 RID: 945
	internal class DateTimeFormatInfoScanner
	{
		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06002F48 RID: 12104 RVA: 0x000B68F0 File Offset: 0x000B4AF0
		private static Dictionary<string, string> KnownWords
		{
			get
			{
				if (DateTimeFormatInfoScanner.s_knownWords == null)
				{
					DateTimeFormatInfoScanner.s_knownWords = new Dictionary<string, string>
					{
						{
							"/",
							string.Empty
						},
						{
							"-",
							string.Empty
						},
						{
							".",
							string.Empty
						},
						{
							"年",
							string.Empty
						},
						{
							"月",
							string.Empty
						},
						{
							"日",
							string.Empty
						},
						{
							"년",
							string.Empty
						},
						{
							"월",
							string.Empty
						},
						{
							"일",
							string.Empty
						},
						{
							"시",
							string.Empty
						},
						{
							"분",
							string.Empty
						},
						{
							"초",
							string.Empty
						},
						{
							"時",
							string.Empty
						},
						{
							"时",
							string.Empty
						},
						{
							"分",
							string.Empty
						},
						{
							"秒",
							string.Empty
						}
					};
				}
				return DateTimeFormatInfoScanner.s_knownWords;
			}
		}

		// Token: 0x06002F49 RID: 12105 RVA: 0x000B6A20 File Offset: 0x000B4C20
		internal static int SkipWhiteSpacesAndNonLetter(string pattern, int currentIndex)
		{
			while (currentIndex < pattern.Length)
			{
				char c = pattern[currentIndex];
				if (c == '\\')
				{
					currentIndex++;
					if (currentIndex >= pattern.Length)
					{
						break;
					}
					c = pattern[currentIndex];
					if (c == '\'')
					{
						continue;
					}
				}
				if (char.IsLetter(c) || c == '\'' || c == '.')
				{
					break;
				}
				currentIndex++;
			}
			return currentIndex;
		}

		// Token: 0x06002F4A RID: 12106 RVA: 0x000B6A78 File Offset: 0x000B4C78
		internal void AddDateWordOrPostfix(string formatPostfix, string str)
		{
			if (str.Length > 0)
			{
				if (str.Equals("."))
				{
					this.AddIgnorableSymbols(".");
					return;
				}
				string text;
				if (!DateTimeFormatInfoScanner.KnownWords.TryGetValue(str, out text))
				{
					if (this.m_dateWords == null)
					{
						this.m_dateWords = new List<string>();
					}
					if (formatPostfix == "MMMM")
					{
						string text2 = "\ue000" + str;
						if (!this.m_dateWords.Contains(text2))
						{
							this.m_dateWords.Add(text2);
							return;
						}
					}
					else
					{
						if (!this.m_dateWords.Contains(str))
						{
							this.m_dateWords.Add(str);
						}
						if (str[str.Length - 1] == '.')
						{
							string text3 = str.Substring(0, str.Length - 1);
							if (!this.m_dateWords.Contains(text3))
							{
								this.m_dateWords.Add(text3);
							}
						}
					}
				}
			}
		}

		// Token: 0x06002F4B RID: 12107 RVA: 0x000B6B5C File Offset: 0x000B4D5C
		internal int AddDateWords(string pattern, int index, string formatPostfix)
		{
			int num = DateTimeFormatInfoScanner.SkipWhiteSpacesAndNonLetter(pattern, index);
			if (num != index && formatPostfix != null)
			{
				formatPostfix = null;
			}
			index = num;
			StringBuilder stringBuilder = new StringBuilder();
			while (index < pattern.Length)
			{
				char c = pattern[index];
				if (c == '\'')
				{
					this.AddDateWordOrPostfix(formatPostfix, stringBuilder.ToString());
					index++;
					break;
				}
				if (c == '\\')
				{
					index++;
					if (index < pattern.Length)
					{
						stringBuilder.Append(pattern[index]);
						index++;
					}
				}
				else if (char.IsWhiteSpace(c))
				{
					this.AddDateWordOrPostfix(formatPostfix, stringBuilder.ToString());
					if (formatPostfix != null)
					{
						formatPostfix = null;
					}
					stringBuilder.Length = 0;
					index++;
				}
				else
				{
					stringBuilder.Append(c);
					index++;
				}
			}
			return index;
		}

		// Token: 0x06002F4C RID: 12108 RVA: 0x000B6C12 File Offset: 0x000B4E12
		internal static int ScanRepeatChar(string pattern, char ch, int index, out int count)
		{
			count = 1;
			while (++index < pattern.Length && pattern[index] == ch)
			{
				count++;
			}
			return index;
		}

		// Token: 0x06002F4D RID: 12109 RVA: 0x000B6C38 File Offset: 0x000B4E38
		internal void AddIgnorableSymbols(string text)
		{
			if (this.m_dateWords == null)
			{
				this.m_dateWords = new List<string>();
			}
			string text2 = "\ue001" + text;
			if (!this.m_dateWords.Contains(text2))
			{
				this.m_dateWords.Add(text2);
			}
		}

		// Token: 0x06002F4E RID: 12110 RVA: 0x000B6C80 File Offset: 0x000B4E80
		internal void ScanDateWord(string pattern)
		{
			this.m_ymdFlags = DateTimeFormatInfoScanner.FoundDatePattern.None;
			for (int i = 0; i < pattern.Length; i++)
			{
				char c = pattern[i];
				if (c <= 'M')
				{
					if (c == '\'')
					{
						i = this.AddDateWords(pattern, i + 1, null);
						continue;
					}
					if (c == '.')
					{
						if (this.m_ymdFlags == DateTimeFormatInfoScanner.FoundDatePattern.FoundYMDPatternFlag)
						{
							this.AddIgnorableSymbols(".");
							this.m_ymdFlags = DateTimeFormatInfoScanner.FoundDatePattern.None;
						}
						i++;
						continue;
					}
					if (c == 'M')
					{
						int num;
						i = DateTimeFormatInfoScanner.ScanRepeatChar(pattern, 'M', i, out num);
						if (num >= 4 && i < pattern.Length && pattern[i] == '\'')
						{
							i = this.AddDateWords(pattern, i + 1, "MMMM");
						}
						this.m_ymdFlags |= DateTimeFormatInfoScanner.FoundDatePattern.FoundMonthPatternFlag;
						continue;
					}
				}
				else
				{
					if (c == '\\')
					{
						i += 2;
						continue;
					}
					if (c != 'd')
					{
						if (c == 'y')
						{
							int num;
							i = DateTimeFormatInfoScanner.ScanRepeatChar(pattern, 'y', i, out num);
							this.m_ymdFlags |= DateTimeFormatInfoScanner.FoundDatePattern.FoundYearPatternFlag;
							continue;
						}
					}
					else
					{
						int num;
						i = DateTimeFormatInfoScanner.ScanRepeatChar(pattern, 'd', i, out num);
						if (num <= 2)
						{
							this.m_ymdFlags |= DateTimeFormatInfoScanner.FoundDatePattern.FoundDayPatternFlag;
							continue;
						}
						continue;
					}
				}
				if (this.m_ymdFlags == DateTimeFormatInfoScanner.FoundDatePattern.FoundYMDPatternFlag && !char.IsWhiteSpace(c))
				{
					this.m_ymdFlags = DateTimeFormatInfoScanner.FoundDatePattern.None;
				}
			}
		}

		// Token: 0x06002F4F RID: 12111 RVA: 0x000B6DB8 File Offset: 0x000B4FB8
		internal string[] GetDateWordsOfDTFI(DateTimeFormatInfo dtfi)
		{
			string[] array = dtfi.GetAllDateTimePatterns('D');
			for (int i = 0; i < array.Length; i++)
			{
				this.ScanDateWord(array[i]);
			}
			array = dtfi.GetAllDateTimePatterns('d');
			for (int i = 0; i < array.Length; i++)
			{
				this.ScanDateWord(array[i]);
			}
			array = dtfi.GetAllDateTimePatterns('y');
			for (int i = 0; i < array.Length; i++)
			{
				this.ScanDateWord(array[i]);
			}
			this.ScanDateWord(dtfi.MonthDayPattern);
			array = dtfi.GetAllDateTimePatterns('T');
			for (int i = 0; i < array.Length; i++)
			{
				this.ScanDateWord(array[i]);
			}
			array = dtfi.GetAllDateTimePatterns('t');
			for (int i = 0; i < array.Length; i++)
			{
				this.ScanDateWord(array[i]);
			}
			string[] array2 = null;
			if (this.m_dateWords != null && this.m_dateWords.Count > 0)
			{
				array2 = new string[this.m_dateWords.Count];
				for (int i = 0; i < this.m_dateWords.Count; i++)
				{
					array2[i] = this.m_dateWords[i];
				}
			}
			return array2;
		}

		// Token: 0x06002F50 RID: 12112 RVA: 0x000B6EC0 File Offset: 0x000B50C0
		internal static FORMATFLAGS GetFormatFlagGenitiveMonth(string[] monthNames, string[] genitveMonthNames, string[] abbrevMonthNames, string[] genetiveAbbrevMonthNames)
		{
			if (DateTimeFormatInfoScanner.EqualStringArrays(monthNames, genitveMonthNames) && DateTimeFormatInfoScanner.EqualStringArrays(abbrevMonthNames, genetiveAbbrevMonthNames))
			{
				return FORMATFLAGS.None;
			}
			return FORMATFLAGS.UseGenitiveMonth;
		}

		// Token: 0x06002F51 RID: 12113 RVA: 0x000B6ED8 File Offset: 0x000B50D8
		internal static FORMATFLAGS GetFormatFlagUseSpaceInMonthNames(string[] monthNames, string[] genitveMonthNames, string[] abbrevMonthNames, string[] genetiveAbbrevMonthNames)
		{
			FORMATFLAGS formatflags = FORMATFLAGS.None;
			formatflags |= ((DateTimeFormatInfoScanner.ArrayElementsBeginWithDigit(monthNames) || DateTimeFormatInfoScanner.ArrayElementsBeginWithDigit(genitveMonthNames) || DateTimeFormatInfoScanner.ArrayElementsBeginWithDigit(abbrevMonthNames) || DateTimeFormatInfoScanner.ArrayElementsBeginWithDigit(genetiveAbbrevMonthNames)) ? FORMATFLAGS.UseDigitPrefixInTokens : FORMATFLAGS.None);
			return formatflags | ((DateTimeFormatInfoScanner.ArrayElementsHaveSpace(monthNames) || DateTimeFormatInfoScanner.ArrayElementsHaveSpace(genitveMonthNames) || DateTimeFormatInfoScanner.ArrayElementsHaveSpace(abbrevMonthNames) || DateTimeFormatInfoScanner.ArrayElementsHaveSpace(genetiveAbbrevMonthNames)) ? FORMATFLAGS.UseSpacesInMonthNames : FORMATFLAGS.None);
		}

		// Token: 0x06002F52 RID: 12114 RVA: 0x000B6F37 File Offset: 0x000B5137
		internal static FORMATFLAGS GetFormatFlagUseSpaceInDayNames(string[] dayNames, string[] abbrevDayNames)
		{
			if (!DateTimeFormatInfoScanner.ArrayElementsHaveSpace(dayNames) && !DateTimeFormatInfoScanner.ArrayElementsHaveSpace(abbrevDayNames))
			{
				return FORMATFLAGS.None;
			}
			return FORMATFLAGS.UseSpacesInDayNames;
		}

		// Token: 0x06002F53 RID: 12115 RVA: 0x000B6F4D File Offset: 0x000B514D
		internal static FORMATFLAGS GetFormatFlagUseHebrewCalendar(int calID)
		{
			if (calID != 8)
			{
				return FORMATFLAGS.None;
			}
			return (FORMATFLAGS)10;
		}

		// Token: 0x06002F54 RID: 12116 RVA: 0x000B6F58 File Offset: 0x000B5158
		private static bool EqualStringArrays(string[] array1, string[] array2)
		{
			if (array1 == array2)
			{
				return true;
			}
			if (array1.Length != array2.Length)
			{
				return false;
			}
			for (int i = 0; i < array1.Length; i++)
			{
				if (!array1[i].Equals(array2[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002F55 RID: 12117 RVA: 0x000B6F94 File Offset: 0x000B5194
		private static bool ArrayElementsHaveSpace(string[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				for (int j = 0; j < array[i].Length; j++)
				{
					if (char.IsWhiteSpace(array[i][j]))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002F56 RID: 12118 RVA: 0x000B6FD8 File Offset: 0x000B51D8
		private static bool ArrayElementsBeginWithDigit(string[] array)
		{
			int i = 0;
			while (i < array.Length)
			{
				if (array[i].Length > 0 && array[i][0] >= '0' && array[i][0] <= '9')
				{
					int num = 1;
					while (num < array[i].Length && array[i][num] >= '0' && array[i][num] <= '9')
					{
						num++;
					}
					if (num == array[i].Length)
					{
						return false;
					}
					if (num == array[i].Length - 1)
					{
						char c = array[i][num];
						if (c == '月' || c == '월')
						{
							return false;
						}
					}
					return num != array[i].Length - 4 || array[i][num] != '\'' || array[i][num + 1] != ' ' || array[i][num + 2] != '月' || array[i][num + 3] != '\'';
				}
				else
				{
					i++;
				}
			}
			return false;
		}

		// Token: 0x040013FB RID: 5115
		internal const char MonthPostfixChar = '\ue000';

		// Token: 0x040013FC RID: 5116
		internal const char IgnorableSymbolChar = '\ue001';

		// Token: 0x040013FD RID: 5117
		internal const string CJKYearSuff = "年";

		// Token: 0x040013FE RID: 5118
		internal const string CJKMonthSuff = "月";

		// Token: 0x040013FF RID: 5119
		internal const string CJKDaySuff = "日";

		// Token: 0x04001400 RID: 5120
		internal const string KoreanYearSuff = "년";

		// Token: 0x04001401 RID: 5121
		internal const string KoreanMonthSuff = "월";

		// Token: 0x04001402 RID: 5122
		internal const string KoreanDaySuff = "일";

		// Token: 0x04001403 RID: 5123
		internal const string KoreanHourSuff = "시";

		// Token: 0x04001404 RID: 5124
		internal const string KoreanMinuteSuff = "분";

		// Token: 0x04001405 RID: 5125
		internal const string KoreanSecondSuff = "초";

		// Token: 0x04001406 RID: 5126
		internal const string CJKHourSuff = "時";

		// Token: 0x04001407 RID: 5127
		internal const string ChineseHourSuff = "时";

		// Token: 0x04001408 RID: 5128
		internal const string CJKMinuteSuff = "分";

		// Token: 0x04001409 RID: 5129
		internal const string CJKSecondSuff = "秒";

		// Token: 0x0400140A RID: 5130
		internal List<string> m_dateWords = new List<string>();

		// Token: 0x0400140B RID: 5131
		private static volatile Dictionary<string, string> s_knownWords;

		// Token: 0x0400140C RID: 5132
		private DateTimeFormatInfoScanner.FoundDatePattern m_ymdFlags;

		// Token: 0x02000B67 RID: 2919
		private enum FoundDatePattern
		{
			// Token: 0x0400345D RID: 13405
			None,
			// Token: 0x0400345E RID: 13406
			FoundYearPatternFlag,
			// Token: 0x0400345F RID: 13407
			FoundMonthPatternFlag,
			// Token: 0x04003460 RID: 13408
			FoundDayPatternFlag = 4,
			// Token: 0x04003461 RID: 13409
			FoundYMDPatternFlag = 7
		}
	}
}
