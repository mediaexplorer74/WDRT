using System;
using System.Runtime.Serialization;

namespace System.Globalization
{
	// Token: 0x020003BE RID: 958
	[Serializable]
	internal class GregorianCalendarHelper
	{
		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06002F99 RID: 12185 RVA: 0x000B7FDD File Offset: 0x000B61DD
		internal int MaxYear
		{
			get
			{
				return this.m_maxYear;
			}
		}

		// Token: 0x06002F9A RID: 12186 RVA: 0x000B7FE8 File Offset: 0x000B61E8
		internal GregorianCalendarHelper(Calendar cal, EraInfo[] eraInfo)
		{
			this.m_Cal = cal;
			this.m_EraInfo = eraInfo;
			this.m_minDate = this.m_Cal.MinSupportedDateTime;
			this.m_maxYear = this.m_EraInfo[0].maxEraYear;
			this.m_minYear = this.m_EraInfo[0].minEraYear;
		}

		// Token: 0x06002F9B RID: 12187 RVA: 0x000B804C File Offset: 0x000B624C
		private int GetYearOffset(int year, int era, bool throwOnError)
		{
			if (year < 0)
			{
				if (throwOnError)
				{
					throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				return -1;
			}
			else
			{
				if (era == 0)
				{
					era = this.m_Cal.CurrentEraValue;
				}
				int i = 0;
				while (i < this.m_EraInfo.Length)
				{
					if (era == this.m_EraInfo[i].era)
					{
						if (year >= this.m_EraInfo[i].minEraYear)
						{
							if (year <= this.m_EraInfo[i].maxEraYear)
							{
								return this.m_EraInfo[i].yearOffset;
							}
							if (!AppContextSwitches.EnforceJapaneseEraYearRanges)
							{
								int num = year - this.m_EraInfo[i].maxEraYear;
								for (int j = i - 1; j >= 0; j--)
								{
									if (num <= this.m_EraInfo[j].maxEraYear)
									{
										return this.m_EraInfo[i].yearOffset;
									}
									num -= this.m_EraInfo[j].maxEraYear;
								}
							}
						}
						if (throwOnError)
						{
							throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), this.m_EraInfo[i].minEraYear, this.m_EraInfo[i].maxEraYear));
						}
						break;
					}
					else
					{
						i++;
					}
				}
				if (throwOnError)
				{
					throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
				}
				return -1;
			}
		}

		// Token: 0x06002F9C RID: 12188 RVA: 0x000B8193 File Offset: 0x000B6393
		internal int GetGregorianYear(int year, int era)
		{
			return this.GetYearOffset(year, era, true) + year;
		}

		// Token: 0x06002F9D RID: 12189 RVA: 0x000B81A0 File Offset: 0x000B63A0
		internal bool IsValidYear(int year, int era)
		{
			return this.GetYearOffset(year, era, false) >= 0;
		}

		// Token: 0x06002F9E RID: 12190 RVA: 0x000B81B4 File Offset: 0x000B63B4
		internal virtual int GetDatePart(long ticks, int part)
		{
			this.CheckTicksRange(ticks);
			int i = (int)(ticks / 864000000000L);
			int num = i / 146097;
			i -= num * 146097;
			int num2 = i / 36524;
			if (num2 == 4)
			{
				num2 = 3;
			}
			i -= num2 * 36524;
			int num3 = i / 1461;
			i -= num3 * 1461;
			int num4 = i / 365;
			if (num4 == 4)
			{
				num4 = 3;
			}
			if (part == 0)
			{
				return num * 400 + num2 * 100 + num3 * 4 + num4 + 1;
			}
			i -= num4 * 365;
			if (part == 1)
			{
				return i + 1;
			}
			int[] array = ((num4 == 3 && (num3 != 24 || num2 == 3)) ? GregorianCalendarHelper.DaysToMonth366 : GregorianCalendarHelper.DaysToMonth365);
			int num5 = i >> 6;
			while (i >= array[num5])
			{
				num5++;
			}
			if (part == 2)
			{
				return num5;
			}
			return i - array[num5 - 1] + 1;
		}

		// Token: 0x06002F9F RID: 12191 RVA: 0x000B82A0 File Offset: 0x000B64A0
		internal static long GetAbsoluteDate(int year, int month, int day)
		{
			if (year >= 1 && year <= 9999 && month >= 1 && month <= 12)
			{
				int[] array = ((year % 4 == 0 && (year % 100 != 0 || year % 400 == 0)) ? GregorianCalendarHelper.DaysToMonth366 : GregorianCalendarHelper.DaysToMonth365);
				if (day >= 1 && day <= array[month] - array[month - 1])
				{
					int num = year - 1;
					int num2 = num * 365 + num / 4 - num / 100 + num / 400 + array[month - 1] + day - 1;
					return (long)num2;
				}
			}
			throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("ArgumentOutOfRange_BadYearMonthDay"));
		}

		// Token: 0x06002FA0 RID: 12192 RVA: 0x000B832D File Offset: 0x000B652D
		internal static long DateToTicks(int year, int month, int day)
		{
			return GregorianCalendarHelper.GetAbsoluteDate(year, month, day) * 864000000000L;
		}

		// Token: 0x06002FA1 RID: 12193 RVA: 0x000B8344 File Offset: 0x000B6544
		internal static long TimeToTicks(int hour, int minute, int second, int millisecond)
		{
			if (hour < 0 || hour >= 24 || minute < 0 || minute >= 60 || second < 0 || second >= 60)
			{
				throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("ArgumentOutOfRange_BadHourMinuteSecond"));
			}
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 0, 999));
			}
			return TimeSpan.TimeToTicks(hour, minute, second) + (long)millisecond * 10000L;
		}

		// Token: 0x06002FA2 RID: 12194 RVA: 0x000B83CC File Offset: 0x000B65CC
		internal void CheckTicksRange(long ticks)
		{
			if (ticks < this.m_Cal.MinSupportedDateTime.Ticks || ticks > this.m_Cal.MaxSupportedDateTime.Ticks)
			{
				throw new ArgumentOutOfRangeException("time", string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("ArgumentOutOfRange_CalendarRange"), this.m_Cal.MinSupportedDateTime, this.m_Cal.MaxSupportedDateTime));
			}
		}

		// Token: 0x06002FA3 RID: 12195 RVA: 0x000B8444 File Offset: 0x000B6644
		public DateTime AddMonths(DateTime time, int months)
		{
			if (months < -120000 || months > 120000)
			{
				throw new ArgumentOutOfRangeException("months", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), -120000, 120000));
			}
			this.CheckTicksRange(time.Ticks);
			int num = this.GetDatePart(time.Ticks, 0);
			int num2 = this.GetDatePart(time.Ticks, 2);
			int num3 = this.GetDatePart(time.Ticks, 3);
			int num4 = num2 - 1 + months;
			if (num4 >= 0)
			{
				num2 = num4 % 12 + 1;
				num += num4 / 12;
			}
			else
			{
				num2 = 12 + (num4 + 1) % 12;
				num += (num4 - 11) / 12;
			}
			int[] array = ((num % 4 == 0 && (num % 100 != 0 || num % 400 == 0)) ? GregorianCalendarHelper.DaysToMonth366 : GregorianCalendarHelper.DaysToMonth365);
			int num5 = array[num2] - array[num2 - 1];
			if (num3 > num5)
			{
				num3 = num5;
			}
			long num6 = GregorianCalendarHelper.DateToTicks(num, num2, num3) + time.Ticks % 864000000000L;
			Calendar.CheckAddResult(num6, this.m_Cal.MinSupportedDateTime, this.m_Cal.MaxSupportedDateTime);
			return new DateTime(num6);
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x000B8573 File Offset: 0x000B6773
		public DateTime AddYears(DateTime time, int years)
		{
			return this.AddMonths(time, years * 12);
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x000B8580 File Offset: 0x000B6780
		public int GetDayOfMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 3);
		}

		// Token: 0x06002FA6 RID: 12198 RVA: 0x000B8590 File Offset: 0x000B6790
		public DayOfWeek GetDayOfWeek(DateTime time)
		{
			this.CheckTicksRange(time.Ticks);
			return (DayOfWeek)((time.Ticks / 864000000000L + 1L) % 7L);
		}

		// Token: 0x06002FA7 RID: 12199 RVA: 0x000B85B7 File Offset: 0x000B67B7
		public int GetDayOfYear(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 1);
		}

		// Token: 0x06002FA8 RID: 12200 RVA: 0x000B85C8 File Offset: 0x000B67C8
		public int GetDaysInMonth(int year, int month, int era)
		{
			year = this.GetGregorianYear(year, era);
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("ArgumentOutOfRange_Month"));
			}
			int[] array = ((year % 4 == 0 && (year % 100 != 0 || year % 400 == 0)) ? GregorianCalendarHelper.DaysToMonth366 : GregorianCalendarHelper.DaysToMonth365);
			return array[month] - array[month - 1];
		}

		// Token: 0x06002FA9 RID: 12201 RVA: 0x000B8627 File Offset: 0x000B6827
		public int GetDaysInYear(int year, int era)
		{
			year = this.GetGregorianYear(year, era);
			if (year % 4 != 0 || (year % 100 == 0 && year % 400 != 0))
			{
				return 365;
			}
			return 366;
		}

		// Token: 0x06002FAA RID: 12202 RVA: 0x000B8654 File Offset: 0x000B6854
		public int GetEra(DateTime time)
		{
			long ticks = time.Ticks;
			for (int i = 0; i < this.m_EraInfo.Length; i++)
			{
				if (ticks >= this.m_EraInfo[i].ticks)
				{
					return this.m_EraInfo[i].era;
				}
			}
			throw new ArgumentOutOfRangeException(Environment.GetResourceString("ArgumentOutOfRange_Era"));
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06002FAB RID: 12203 RVA: 0x000B86AC File Offset: 0x000B68AC
		public int[] Eras
		{
			get
			{
				if (this.m_eras == null)
				{
					this.m_eras = new int[this.m_EraInfo.Length];
					for (int i = 0; i < this.m_EraInfo.Length; i++)
					{
						this.m_eras[i] = this.m_EraInfo[i].era;
					}
				}
				return (int[])this.m_eras.Clone();
			}
		}

		// Token: 0x06002FAC RID: 12204 RVA: 0x000B870C File Offset: 0x000B690C
		public int GetMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 2);
		}

		// Token: 0x06002FAD RID: 12205 RVA: 0x000B871C File Offset: 0x000B691C
		public int GetMonthsInYear(int year, int era)
		{
			year = this.GetGregorianYear(year, era);
			return 12;
		}

		// Token: 0x06002FAE RID: 12206 RVA: 0x000B872C File Offset: 0x000B692C
		public int GetYear(DateTime time)
		{
			long ticks = time.Ticks;
			int datePart = this.GetDatePart(ticks, 0);
			for (int i = 0; i < this.m_EraInfo.Length; i++)
			{
				if (ticks >= this.m_EraInfo[i].ticks)
				{
					return datePart - this.m_EraInfo[i].yearOffset;
				}
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_NoEra"));
		}

		// Token: 0x06002FAF RID: 12207 RVA: 0x000B878C File Offset: 0x000B698C
		public int GetYear(int year, DateTime time)
		{
			long ticks = time.Ticks;
			for (int i = 0; i < this.m_EraInfo.Length; i++)
			{
				if (ticks >= this.m_EraInfo[i].ticks)
				{
					return year - this.m_EraInfo[i].yearOffset;
				}
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_NoEra"));
		}

		// Token: 0x06002FB0 RID: 12208 RVA: 0x000B87E4 File Offset: 0x000B69E4
		public bool IsLeapDay(int year, int month, int day, int era)
		{
			if (day < 1 || day > this.GetDaysInMonth(year, month, era))
			{
				throw new ArgumentOutOfRangeException("day", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 1, this.GetDaysInMonth(year, month, era)));
			}
			return this.IsLeapYear(year, era) && (month == 2 && day == 29);
		}

		// Token: 0x06002FB1 RID: 12209 RVA: 0x000B884F File Offset: 0x000B6A4F
		public int GetLeapMonth(int year, int era)
		{
			year = this.GetGregorianYear(year, era);
			return 0;
		}

		// Token: 0x06002FB2 RID: 12210 RVA: 0x000B885C File Offset: 0x000B6A5C
		public bool IsLeapMonth(int year, int month, int era)
		{
			year = this.GetGregorianYear(year, era);
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 1, 12));
			}
			return false;
		}

		// Token: 0x06002FB3 RID: 12211 RVA: 0x000B88A9 File Offset: 0x000B6AA9
		public bool IsLeapYear(int year, int era)
		{
			year = this.GetGregorianYear(year, era);
			return year % 4 == 0 && (year % 100 != 0 || year % 400 == 0);
		}

		// Token: 0x06002FB4 RID: 12212 RVA: 0x000B88D0 File Offset: 0x000B6AD0
		public DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			year = this.GetGregorianYear(year, era);
			long num = GregorianCalendarHelper.DateToTicks(year, month, day) + GregorianCalendarHelper.TimeToTicks(hour, minute, second, millisecond);
			this.CheckTicksRange(num);
			return new DateTime(num);
		}

		// Token: 0x06002FB5 RID: 12213 RVA: 0x000B890C File Offset: 0x000B6B0C
		public virtual int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
		{
			this.CheckTicksRange(time.Ticks);
			return GregorianCalendar.GetDefaultInstance().GetWeekOfYear(time, rule, firstDayOfWeek);
		}

		// Token: 0x06002FB6 RID: 12214 RVA: 0x000B8928 File Offset: 0x000B6B28
		public int ToFourDigitYear(int year, int twoDigitYearMax)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
			}
			if (year < 100)
			{
				int num = year % 100;
				return (twoDigitYearMax / 100 - ((num > twoDigitYearMax % 100) ? 1 : 0)) * 100 + num;
			}
			if (year < this.m_minYear || year > this.m_maxYear)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), this.m_minYear, this.m_maxYear));
			}
			return year;
		}

		// Token: 0x04001444 RID: 5188
		internal const long TicksPerMillisecond = 10000L;

		// Token: 0x04001445 RID: 5189
		internal const long TicksPerSecond = 10000000L;

		// Token: 0x04001446 RID: 5190
		internal const long TicksPerMinute = 600000000L;

		// Token: 0x04001447 RID: 5191
		internal const long TicksPerHour = 36000000000L;

		// Token: 0x04001448 RID: 5192
		internal const long TicksPerDay = 864000000000L;

		// Token: 0x04001449 RID: 5193
		internal const int MillisPerSecond = 1000;

		// Token: 0x0400144A RID: 5194
		internal const int MillisPerMinute = 60000;

		// Token: 0x0400144B RID: 5195
		internal const int MillisPerHour = 3600000;

		// Token: 0x0400144C RID: 5196
		internal const int MillisPerDay = 86400000;

		// Token: 0x0400144D RID: 5197
		internal const int DaysPerYear = 365;

		// Token: 0x0400144E RID: 5198
		internal const int DaysPer4Years = 1461;

		// Token: 0x0400144F RID: 5199
		internal const int DaysPer100Years = 36524;

		// Token: 0x04001450 RID: 5200
		internal const int DaysPer400Years = 146097;

		// Token: 0x04001451 RID: 5201
		internal const int DaysTo10000 = 3652059;

		// Token: 0x04001452 RID: 5202
		internal const long MaxMillis = 315537897600000L;

		// Token: 0x04001453 RID: 5203
		internal const int DatePartYear = 0;

		// Token: 0x04001454 RID: 5204
		internal const int DatePartDayOfYear = 1;

		// Token: 0x04001455 RID: 5205
		internal const int DatePartMonth = 2;

		// Token: 0x04001456 RID: 5206
		internal const int DatePartDay = 3;

		// Token: 0x04001457 RID: 5207
		internal static readonly int[] DaysToMonth365 = new int[]
		{
			0, 31, 59, 90, 120, 151, 181, 212, 243, 273,
			304, 334, 365
		};

		// Token: 0x04001458 RID: 5208
		internal static readonly int[] DaysToMonth366 = new int[]
		{
			0, 31, 60, 91, 121, 152, 182, 213, 244, 274,
			305, 335, 366
		};

		// Token: 0x04001459 RID: 5209
		[OptionalField(VersionAdded = 1)]
		internal int m_maxYear = 9999;

		// Token: 0x0400145A RID: 5210
		[OptionalField(VersionAdded = 1)]
		internal int m_minYear;

		// Token: 0x0400145B RID: 5211
		internal Calendar m_Cal;

		// Token: 0x0400145C RID: 5212
		[OptionalField(VersionAdded = 1)]
		internal EraInfo[] m_EraInfo;

		// Token: 0x0400145D RID: 5213
		[OptionalField(VersionAdded = 1)]
		internal int[] m_eras;

		// Token: 0x0400145E RID: 5214
		[OptionalField(VersionAdded = 1)]
		internal DateTime m_minDate;
	}
}
