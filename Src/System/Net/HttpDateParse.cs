using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x020001B6 RID: 438
	internal static class HttpDateParse
	{
		// Token: 0x06001135 RID: 4405 RVA: 0x0005DD70 File Offset: 0x0005BF70
		private static char MAKE_UPPER(char c)
		{
			return char.ToUpper(c, CultureInfo.InvariantCulture);
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x0005DD80 File Offset: 0x0005BF80
		private static int MapDayMonthToDword(char[] lpszDay, int index)
		{
			switch (HttpDateParse.MAKE_UPPER(lpszDay[index]))
			{
			case 'A':
			{
				char c = HttpDateParse.MAKE_UPPER(lpszDay[index + 1]);
				if (c == 'P')
				{
					return 4;
				}
				if (c != 'U')
				{
					return -999;
				}
				return 8;
			}
			case 'D':
				return 12;
			case 'F':
			{
				char c2 = HttpDateParse.MAKE_UPPER(lpszDay[index + 1]);
				if (c2 == 'E')
				{
					return 2;
				}
				if (c2 == 'R')
				{
					return 5;
				}
				return -999;
			}
			case 'G':
				return -1000;
			case 'J':
			{
				char c3 = HttpDateParse.MAKE_UPPER(lpszDay[index + 1]);
				if (c3 != 'A')
				{
					if (c3 == 'U')
					{
						char c4 = HttpDateParse.MAKE_UPPER(lpszDay[index + 2]);
						if (c4 == 'L')
						{
							return 7;
						}
						if (c4 == 'N')
						{
							return 6;
						}
					}
					return -999;
				}
				return 1;
			}
			case 'M':
			{
				char c5 = HttpDateParse.MAKE_UPPER(lpszDay[index + 1]);
				if (c5 != 'A')
				{
					if (c5 == 'O')
					{
						return 1;
					}
				}
				else
				{
					char c6 = HttpDateParse.MAKE_UPPER(lpszDay[index + 2]);
					if (c6 == 'R')
					{
						return 3;
					}
					if (c6 == 'Y')
					{
						return 5;
					}
				}
				return -999;
			}
			case 'N':
				return 11;
			case 'O':
				return 10;
			case 'S':
			{
				char c7 = HttpDateParse.MAKE_UPPER(lpszDay[index + 1]);
				if (c7 == 'A')
				{
					return 6;
				}
				if (c7 == 'E')
				{
					return 9;
				}
				if (c7 != 'U')
				{
					return -999;
				}
				return 0;
			}
			case 'T':
			{
				char c8 = HttpDateParse.MAKE_UPPER(lpszDay[index + 1]);
				if (c8 == 'H')
				{
					return 4;
				}
				if (c8 == 'U')
				{
					return 2;
				}
				return -999;
			}
			case 'U':
				return -1000;
			case 'W':
				return 3;
			}
			return -999;
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x0005DF24 File Offset: 0x0005C124
		public static bool ParseHttpDate(string DateString, out DateTime dtOut)
		{
			int num = 0;
			int num2 = 0;
			int num3 = -1;
			bool flag = false;
			int[] array = new int[8];
			bool flag2 = true;
			char[] array2 = DateString.ToCharArray();
			dtOut = default(DateTime);
			while (num < DateString.Length && num2 < 8)
			{
				if (array2[num] >= '0' && array2[num] <= '9')
				{
					array[num2] = 0;
					do
					{
						array[num2] *= 10;
						array[num2] += (int)(array2[num] - '0');
						num++;
					}
					while (num < DateString.Length && array2[num] >= '0' && array2[num] <= '9');
					num2++;
				}
				else if ((array2[num] >= 'A' && array2[num] <= 'Z') || (array2[num] >= 'a' && array2[num] <= 'z'))
				{
					array[num2] = HttpDateParse.MapDayMonthToDword(array2, num);
					num3 = num2;
					if (array[num2] == -999 && (!flag || num2 != 6))
					{
						flag2 = false;
						return flag2;
					}
					if (num2 == 1)
					{
						flag = true;
					}
					do
					{
						num++;
					}
					while (num < DateString.Length && ((array2[num] >= 'A' && array2[num] <= 'Z') || (array2[num] >= 'a' && array2[num] <= 'z')));
					num2++;
				}
				else
				{
					num++;
				}
			}
			int num4 = 0;
			int num5;
			int num6;
			int num7;
			int num8;
			int num9;
			int num10;
			if (flag)
			{
				num5 = array[2];
				num6 = array[1];
				num7 = array[3];
				num8 = array[4];
				num9 = array[5];
				if (num3 != 6)
				{
					num10 = array[6];
				}
				else
				{
					num10 = array[7];
				}
			}
			else
			{
				num5 = array[1];
				num6 = array[2];
				num10 = array[3];
				num7 = array[4];
				num8 = array[5];
				num9 = array[6];
			}
			if (num10 < 100)
			{
				num10 += ((num10 < 80) ? 2000 : 1900);
			}
			if (num2 < 4 || num5 > 31 || num7 > 23 || num8 > 59 || num9 > 59)
			{
				return false;
			}
			dtOut = new DateTime(num10, num6, num5, num7, num8, num9, num4);
			if (num3 == 6)
			{
				dtOut = dtOut.ToUniversalTime();
			}
			if (num2 > 7 && array[7] != -1000)
			{
				double num11 = (double)array[7];
				dtOut.AddHours(num11);
			}
			dtOut = dtOut.ToLocalTime();
			return flag2;
		}

		// Token: 0x04001408 RID: 5128
		private const int BASE_DEC = 10;

		// Token: 0x04001409 RID: 5129
		private const int DATE_INDEX_DAY_OF_WEEK = 0;

		// Token: 0x0400140A RID: 5130
		private const int DATE_1123_INDEX_DAY = 1;

		// Token: 0x0400140B RID: 5131
		private const int DATE_1123_INDEX_MONTH = 2;

		// Token: 0x0400140C RID: 5132
		private const int DATE_1123_INDEX_YEAR = 3;

		// Token: 0x0400140D RID: 5133
		private const int DATE_1123_INDEX_HRS = 4;

		// Token: 0x0400140E RID: 5134
		private const int DATE_1123_INDEX_MINS = 5;

		// Token: 0x0400140F RID: 5135
		private const int DATE_1123_INDEX_SECS = 6;

		// Token: 0x04001410 RID: 5136
		private const int DATE_ANSI_INDEX_MONTH = 1;

		// Token: 0x04001411 RID: 5137
		private const int DATE_ANSI_INDEX_DAY = 2;

		// Token: 0x04001412 RID: 5138
		private const int DATE_ANSI_INDEX_HRS = 3;

		// Token: 0x04001413 RID: 5139
		private const int DATE_ANSI_INDEX_MINS = 4;

		// Token: 0x04001414 RID: 5140
		private const int DATE_ANSI_INDEX_SECS = 5;

		// Token: 0x04001415 RID: 5141
		private const int DATE_ANSI_INDEX_YEAR = 6;

		// Token: 0x04001416 RID: 5142
		private const int DATE_INDEX_TZ = 7;

		// Token: 0x04001417 RID: 5143
		private const int DATE_INDEX_LAST = 7;

		// Token: 0x04001418 RID: 5144
		private const int MAX_FIELD_DATE_ENTRIES = 8;

		// Token: 0x04001419 RID: 5145
		private const int DATE_TOKEN_JANUARY = 1;

		// Token: 0x0400141A RID: 5146
		private const int DATE_TOKEN_FEBRUARY = 2;

		// Token: 0x0400141B RID: 5147
		private const int DATE_TOKEN_MARCH = 3;

		// Token: 0x0400141C RID: 5148
		private const int DATE_TOKEN_APRIL = 4;

		// Token: 0x0400141D RID: 5149
		private const int DATE_TOKEN_MAY = 5;

		// Token: 0x0400141E RID: 5150
		private const int DATE_TOKEN_JUNE = 6;

		// Token: 0x0400141F RID: 5151
		private const int DATE_TOKEN_JULY = 7;

		// Token: 0x04001420 RID: 5152
		private const int DATE_TOKEN_AUGUST = 8;

		// Token: 0x04001421 RID: 5153
		private const int DATE_TOKEN_SEPTEMBER = 9;

		// Token: 0x04001422 RID: 5154
		private const int DATE_TOKEN_OCTOBER = 10;

		// Token: 0x04001423 RID: 5155
		private const int DATE_TOKEN_NOVEMBER = 11;

		// Token: 0x04001424 RID: 5156
		private const int DATE_TOKEN_DECEMBER = 12;

		// Token: 0x04001425 RID: 5157
		private const int DATE_TOKEN_LAST_MONTH = 13;

		// Token: 0x04001426 RID: 5158
		private const int DATE_TOKEN_SUNDAY = 0;

		// Token: 0x04001427 RID: 5159
		private const int DATE_TOKEN_MONDAY = 1;

		// Token: 0x04001428 RID: 5160
		private const int DATE_TOKEN_TUESDAY = 2;

		// Token: 0x04001429 RID: 5161
		private const int DATE_TOKEN_WEDNESDAY = 3;

		// Token: 0x0400142A RID: 5162
		private const int DATE_TOKEN_THURSDAY = 4;

		// Token: 0x0400142B RID: 5163
		private const int DATE_TOKEN_FRIDAY = 5;

		// Token: 0x0400142C RID: 5164
		private const int DATE_TOKEN_SATURDAY = 6;

		// Token: 0x0400142D RID: 5165
		private const int DATE_TOKEN_LAST_DAY = 7;

		// Token: 0x0400142E RID: 5166
		private const int DATE_TOKEN_GMT = -1000;

		// Token: 0x0400142F RID: 5167
		private const int DATE_TOKEN_LAST = -1000;

		// Token: 0x04001430 RID: 5168
		private const int DATE_TOKEN_ERROR = -999;
	}
}
