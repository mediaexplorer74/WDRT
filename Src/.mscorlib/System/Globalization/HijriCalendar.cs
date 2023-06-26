using System;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32;

namespace System.Globalization
{
	/// <summary>Represents the Hijri calendar.</summary>
	// Token: 0x020003C0 RID: 960
	[ComVisible(true)]
	[Serializable]
	public class HijriCalendar : Calendar
	{
		/// <summary>Gets the earliest date and time supported by this calendar.</summary>
		/// <returns>The earliest date and time supported by the <see cref="T:System.Globalization.HijriCalendar" /> type, which is equivalent to the first moment of July 18, 622 C.E. in the Gregorian calendar.</returns>
		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06002FDD RID: 12253 RVA: 0x000B9494 File Offset: 0x000B7694
		[ComVisible(false)]
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return HijriCalendar.calendarMinValue;
			}
		}

		/// <summary>Gets the latest date and time supported by this calendar.</summary>
		/// <returns>The latest date and time supported by the <see cref="T:System.Globalization.HijriCalendar" /> type, which is equivalent to the last moment of December 31, 9999 C.E. in the Gregorian calendar.</returns>
		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06002FDE RID: 12254 RVA: 0x000B949B File Offset: 0x000B769B
		[ComVisible(false)]
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return HijriCalendar.calendarMaxValue;
			}
		}

		/// <summary>Gets a value that indicates whether the current calendar is solar-based, lunar-based, or a combination of both.</summary>
		/// <returns>Always returns <see cref="F:System.Globalization.CalendarAlgorithmType.LunarCalendar" />.</returns>
		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06002FDF RID: 12255 RVA: 0x000B94A2 File Offset: 0x000B76A2
		[ComVisible(false)]
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.LunarCalendar;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06002FE1 RID: 12257 RVA: 0x000B94B8 File Offset: 0x000B76B8
		internal override int ID
		{
			get
			{
				return 6;
			}
		}

		/// <summary>Gets the number of days in the year that precedes the year that is specified by the <see cref="P:System.Globalization.HijriCalendar.MinSupportedDateTime" /> property.</summary>
		/// <returns>The number of days in the year that precedes the year specified by <see cref="P:System.Globalization.HijriCalendar.MinSupportedDateTime" />.</returns>
		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06002FE2 RID: 12258 RVA: 0x000B94BB File Offset: 0x000B76BB
		protected override int DaysInYearBeforeMinSupportedYear
		{
			get
			{
				return 354;
			}
		}

		// Token: 0x06002FE3 RID: 12259 RVA: 0x000B94C2 File Offset: 0x000B76C2
		private long GetAbsoluteDateHijri(int y, int m, int d)
		{
			return this.DaysUpToHijriYear(y) + (long)HijriCalendar.HijriMonthDays[m - 1] + (long)d - 1L - (long)this.HijriAdjustment;
		}

		// Token: 0x06002FE4 RID: 12260 RVA: 0x000B94E4 File Offset: 0x000B76E4
		private long DaysUpToHijriYear(int HijriYear)
		{
			int num = (HijriYear - 1) / 30 * 30;
			int i = HijriYear - num - 1;
			long num2 = (long)num * 10631L / 30L + 227013L;
			while (i > 0)
			{
				num2 += (long)(354 + (this.IsLeapYear(i, 0) ? 1 : 0));
				i--;
			}
			return num2;
		}

		/// <summary>Gets or sets the number of days to add or subtract from the calendar to accommodate the variances in the start and the end of Ramadan and to accommodate the date difference between countries/regions.</summary>
		/// <returns>An integer from -2 to 2 that represents the number of days to add or subtract from the calendar.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to an invalid value.</exception>
		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06002FE5 RID: 12261 RVA: 0x000B9539 File Offset: 0x000B7739
		// (set) Token: 0x06002FE6 RID: 12262 RVA: 0x000B955C File Offset: 0x000B775C
		public int HijriAdjustment
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_HijriAdvance == -2147483648)
				{
					this.m_HijriAdvance = HijriCalendar.GetAdvanceHijriDate();
				}
				return this.m_HijriAdvance;
			}
			set
			{
				if (value < -2 || value > 2)
				{
					throw new ArgumentOutOfRangeException("HijriAdjustment", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Bounds_Lower_Upper"), -2, 2));
				}
				base.VerifyWritable();
				this.m_HijriAdvance = value;
			}
		}

		// Token: 0x06002FE7 RID: 12263 RVA: 0x000B95AC File Offset: 0x000B77AC
		[SecurityCritical]
		private static int GetAdvanceHijriDate()
		{
			int num = 0;
			RegistryKey registryKey = null;
			try
			{
				registryKey = Registry.CurrentUser.InternalOpenSubKey("Control Panel\\International", false);
			}
			catch (ObjectDisposedException)
			{
				return 0;
			}
			catch (ArgumentException)
			{
				return 0;
			}
			if (registryKey != null)
			{
				try
				{
					object obj = registryKey.InternalGetValue("AddHijriDate", null, false, false);
					if (obj == null)
					{
						return 0;
					}
					string text = obj.ToString();
					if (string.Compare(text, 0, "AddHijriDate", 0, "AddHijriDate".Length, StringComparison.OrdinalIgnoreCase) == 0)
					{
						if (text.Length == "AddHijriDate".Length)
						{
							num = -1;
						}
						else
						{
							text = text.Substring("AddHijriDate".Length);
							try
							{
								int num2 = int.Parse(text.ToString(), CultureInfo.InvariantCulture);
								if (num2 >= -2 && num2 <= 2)
								{
									num = num2;
								}
							}
							catch (ArgumentException)
							{
							}
							catch (FormatException)
							{
							}
							catch (OverflowException)
							{
							}
						}
					}
				}
				finally
				{
					registryKey.Close();
				}
			}
			return num;
		}

		// Token: 0x06002FE8 RID: 12264 RVA: 0x000B96D0 File Offset: 0x000B78D0
		internal static void CheckTicksRange(long ticks)
		{
			if (ticks < HijriCalendar.calendarMinValue.Ticks || ticks > HijriCalendar.calendarMaxValue.Ticks)
			{
				throw new ArgumentOutOfRangeException("time", string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("ArgumentOutOfRange_CalendarRange"), HijriCalendar.calendarMinValue, HijriCalendar.calendarMaxValue));
			}
		}

		// Token: 0x06002FE9 RID: 12265 RVA: 0x000B9730 File Offset: 0x000B7930
		internal static void CheckEraRange(int era)
		{
			if (era != 0 && era != HijriCalendar.HijriEra)
			{
				throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
			}
		}

		// Token: 0x06002FEA RID: 12266 RVA: 0x000B9754 File Offset: 0x000B7954
		internal static void CheckYearRange(int year, int era)
		{
			HijriCalendar.CheckEraRange(era);
			if (year < 1 || year > 9666)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 1, 9666));
			}
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x000B97A4 File Offset: 0x000B79A4
		internal static void CheckYearMonthRange(int year, int month, int era)
		{
			HijriCalendar.CheckYearRange(year, era);
			if (year == 9666 && month > 4)
			{
				throw new ArgumentOutOfRangeException("month", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 1, 4));
			}
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("ArgumentOutOfRange_Month"));
			}
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x000B9810 File Offset: 0x000B7A10
		internal virtual int GetDatePart(long ticks, int part)
		{
			HijriCalendar.CheckTicksRange(ticks);
			long num = ticks / 864000000000L + 1L;
			num += (long)this.HijriAdjustment;
			int num2 = (int)((num - 227013L) * 30L / 10631L) + 1;
			long num3 = this.DaysUpToHijriYear(num2);
			long num4 = (long)this.GetDaysInYear(num2, 0);
			if (num < num3)
			{
				num3 -= num4;
				num2--;
			}
			else if (num == num3)
			{
				num2--;
				num3 -= (long)this.GetDaysInYear(num2, 0);
			}
			else if (num > num3 + num4)
			{
				num3 += num4;
				num2++;
			}
			if (part == 0)
			{
				return num2;
			}
			int num5 = 1;
			num -= num3;
			if (part == 1)
			{
				return (int)num;
			}
			while (num5 <= 12 && num > (long)HijriCalendar.HijriMonthDays[num5 - 1])
			{
				num5++;
			}
			num5--;
			if (part == 2)
			{
				return num5;
			}
			int num6 = (int)(num - (long)HijriCalendar.HijriMonthDays[num5 - 1]);
			if (part == 3)
			{
				return num6;
			}
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DateTimeParsing"));
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of months away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to add months to.</param>
		/// <param name="months">The number of months to add.</param>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of months to the specified <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="months" /> is less than -120000.  
		/// -or-  
		/// <paramref name="months" /> is greater than 120000.</exception>
		// Token: 0x06002FED RID: 12269 RVA: 0x000B98FC File Offset: 0x000B7AFC
		public override DateTime AddMonths(DateTime time, int months)
		{
			if (months < -120000 || months > 120000)
			{
				throw new ArgumentOutOfRangeException("months", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), -120000, 120000));
			}
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
			int daysInMonth = this.GetDaysInMonth(num, num2);
			if (num3 > daysInMonth)
			{
				num3 = daysInMonth;
			}
			long num5 = this.GetAbsoluteDateHijri(num, num2, num3) * 864000000000L + time.Ticks % 864000000000L;
			Calendar.CheckAddResult(num5, this.MinSupportedDateTime, this.MaxSupportedDateTime);
			return new DateTime(num5);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of years away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to add years to.</param>
		/// <param name="years">The number of years to add.</param>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of years to the specified <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range.</exception>
		// Token: 0x06002FEE RID: 12270 RVA: 0x000B99FA File Offset: 0x000B7BFA
		public override DateTime AddYears(DateTime time, int years)
		{
			return this.AddMonths(time, years * 12);
		}

		/// <summary>Returns the day of the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer from 1 to 30 that represents the day of the month in the specified <see cref="T:System.DateTime" />.</returns>
		// Token: 0x06002FEF RID: 12271 RVA: 0x000B9A07 File Offset: 0x000B7C07
		public override int GetDayOfMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 3);
		}

		/// <summary>Returns the day of the week in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>A <see cref="T:System.DayOfWeek" /> value that represents the day of the week in the specified <see cref="T:System.DateTime" />.</returns>
		// Token: 0x06002FF0 RID: 12272 RVA: 0x000B9A17 File Offset: 0x000B7C17
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return (DayOfWeek)(time.Ticks / 864000000000L + 1L) % (DayOfWeek)7;
		}

		/// <summary>Returns the day of the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer from 1 to 355 that represents the day of the year in the specified <see cref="T:System.DateTime" />.</returns>
		// Token: 0x06002FF1 RID: 12273 RVA: 0x000B9A30 File Offset: 0x000B7C30
		public override int GetDayOfYear(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 1);
		}

		/// <summary>Returns the number of days in the specified month of the specified year and era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="month">An integer from 1 to 12 that represents the month.</param>
		/// <param name="era">An integer that represents the era.</param>
		/// <returns>The number of days in the specified month in the specified year in the specified era.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> is outside the range supported by this calendar.  
		/// -or-  
		/// <paramref name="year" /> is outside the range supported by this calendar.  
		/// -or-  
		/// <paramref name="month" /> is outside the range supported by this calendar.</exception>
		// Token: 0x06002FF2 RID: 12274 RVA: 0x000B9A40 File Offset: 0x000B7C40
		public override int GetDaysInMonth(int year, int month, int era)
		{
			HijriCalendar.CheckYearMonthRange(year, month, era);
			if (month == 12)
			{
				if (!this.IsLeapYear(year, 0))
				{
					return 29;
				}
				return 30;
			}
			else
			{
				if (month % 2 != 1)
				{
					return 29;
				}
				return 30;
			}
		}

		/// <summary>Returns the number of days in the specified year and era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="era">An integer that represents the era.</param>
		/// <returns>The number of days in the specified year and era. The number of days is 354 in a common year or 355 in a leap year.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by this calendar.</exception>
		// Token: 0x06002FF3 RID: 12275 RVA: 0x000B9A6A File Offset: 0x000B7C6A
		public override int GetDaysInYear(int year, int era)
		{
			HijriCalendar.CheckYearRange(year, era);
			if (!this.IsLeapYear(year, 0))
			{
				return 354;
			}
			return 355;
		}

		/// <summary>Returns the era in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer that represents the era in the specified <see cref="T:System.DateTime" />.</returns>
		// Token: 0x06002FF4 RID: 12276 RVA: 0x000B9A88 File Offset: 0x000B7C88
		public override int GetEra(DateTime time)
		{
			HijriCalendar.CheckTicksRange(time.Ticks);
			return HijriCalendar.HijriEra;
		}

		/// <summary>Gets the list of eras in the <see cref="T:System.Globalization.HijriCalendar" />.</summary>
		/// <returns>An array of integers that represents the eras in the <see cref="T:System.Globalization.HijriCalendar" />.</returns>
		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06002FF5 RID: 12277 RVA: 0x000B9A9B File Offset: 0x000B7C9B
		public override int[] Eras
		{
			get
			{
				return new int[] { HijriCalendar.HijriEra };
			}
		}

		/// <summary>Returns the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer from 1 to 12 that represents the month in the specified <see cref="T:System.DateTime" />.</returns>
		// Token: 0x06002FF6 RID: 12278 RVA: 0x000B9AAB File Offset: 0x000B7CAB
		public override int GetMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 2);
		}

		/// <summary>Returns the number of months in the specified year and era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="era">An integer that represents the era.</param>
		/// <returns>The number of months in the specified year and era.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> is outside the range supported by this calendar.  
		/// -or-  
		/// <paramref name="year" /> is outside the range supported by this calendar.</exception>
		// Token: 0x06002FF7 RID: 12279 RVA: 0x000B9ABB File Offset: 0x000B7CBB
		public override int GetMonthsInYear(int year, int era)
		{
			HijriCalendar.CheckYearRange(year, era);
			return 12;
		}

		/// <summary>Returns the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer that represents the year in the specified <see cref="T:System.DateTime" />.</returns>
		// Token: 0x06002FF8 RID: 12280 RVA: 0x000B9AC6 File Offset: 0x000B7CC6
		public override int GetYear(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 0);
		}

		/// <summary>Determines whether the specified date is a leap day.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="month">An integer from 1 to 12 that represents the month.</param>
		/// <param name="day">An integer from 1 to 30 that represents the day.</param>
		/// <param name="era">An integer that represents the era.</param>
		/// <returns>
		///   <see langword="true" /> if the specified day is a leap day; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> is outside the range supported by this calendar.  
		/// -or-  
		/// <paramref name="year" /> is outside the range supported by this calendar.  
		/// -or-  
		/// <paramref name="month" /> is outside the range supported by this calendar.  
		/// -or-  
		/// <paramref name="day" /> is outside the range supported by this calendar.</exception>
		// Token: 0x06002FF9 RID: 12281 RVA: 0x000B9AD8 File Offset: 0x000B7CD8
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			int daysInMonth = this.GetDaysInMonth(year, month, era);
			if (day < 1 || day > daysInMonth)
			{
				throw new ArgumentOutOfRangeException("day", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Day"), daysInMonth, month));
			}
			return this.IsLeapYear(year, era) && month == 12 && day == 30;
		}

		/// <summary>Calculates the leap month for a specified year and era.</summary>
		/// <param name="year">A year.</param>
		/// <param name="era">An era. Specify <see cref="F:System.Globalization.Calendar.CurrentEra" /> or <see cref="F:System.Globalization.HijriCalendar.HijriEra" />.</param>
		/// <returns>Always 0 because the <see cref="T:System.Globalization.HijriCalendar" /> type does not support the notion of a leap month.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than the Hijri calendar year 1 or greater than the year 9666.  
		/// -or-  
		/// <paramref name="era" /> is not <see cref="F:System.Globalization.Calendar.CurrentEra" /> or <see cref="F:System.Globalization.HijriCalendar.HijriEra" />.</exception>
		// Token: 0x06002FFA RID: 12282 RVA: 0x000B9B3A File Offset: 0x000B7D3A
		[ComVisible(false)]
		public override int GetLeapMonth(int year, int era)
		{
			HijriCalendar.CheckYearRange(year, era);
			return 0;
		}

		/// <summary>Determines whether the specified month in the specified year and era is a leap month.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="month">An integer from 1 to 12 that represents the month.</param>
		/// <param name="era">An integer that represents the era.</param>
		/// <returns>This method always returns <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> is outside the range supported by this calendar.  
		/// -or-  
		/// <paramref name="year" /> is outside the range supported by this calendar.  
		/// -or-  
		/// <paramref name="month" /> is outside the range supported by this calendar.</exception>
		// Token: 0x06002FFB RID: 12283 RVA: 0x000B9B44 File Offset: 0x000B7D44
		public override bool IsLeapMonth(int year, int month, int era)
		{
			HijriCalendar.CheckYearMonthRange(year, month, era);
			return false;
		}

		/// <summary>Determines whether the specified year in the specified era is a leap year.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="era">An integer that represents the era.</param>
		/// <returns>
		///   <see langword="true" /> if the specified year is a leap year; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> is outside the range supported by this calendar.  
		/// -or-  
		/// <paramref name="year" /> is outside the range supported by this calendar.</exception>
		// Token: 0x06002FFC RID: 12284 RVA: 0x000B9B4F File Offset: 0x000B7D4F
		public override bool IsLeapYear(int year, int era)
		{
			HijriCalendar.CheckYearRange(year, era);
			return (year * 11 + 14) % 30 < 11;
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is set to the specified date, time, and era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="month">An integer from 1 to 12 that represents the month.</param>
		/// <param name="day">An integer from 1 to 30 that represents the day.</param>
		/// <param name="hour">An integer from 0 to 23 that represents the hour.</param>
		/// <param name="minute">An integer from 0 to 59 that represents the minute.</param>
		/// <param name="second">An integer from 0 to 59 that represents the second.</param>
		/// <param name="millisecond">An integer from 0 to 999 that represents the millisecond.</param>
		/// <param name="era">An integer that represents the era.</param>
		/// <returns>The <see cref="T:System.DateTime" /> that is set to the specified date and time in the current era.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> is outside the range supported by this calendar.  
		/// -or-  
		/// <paramref name="year" /> is outside the range supported by this calendar.  
		/// -or-  
		/// <paramref name="month" /> is outside the range supported by this calendar.  
		/// -or-  
		/// <paramref name="day" /> is outside the range supported by this calendar.  
		/// -or-  
		/// <paramref name="hour" /> is less than zero or greater than 23.  
		/// -or-  
		/// <paramref name="minute" /> is less than zero or greater than 59.  
		/// -or-  
		/// <paramref name="second" /> is less than zero or greater than 59.  
		/// -or-  
		/// <paramref name="millisecond" /> is less than zero or greater than 999.</exception>
		// Token: 0x06002FFD RID: 12285 RVA: 0x000B9B68 File Offset: 0x000B7D68
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			int daysInMonth = this.GetDaysInMonth(year, month, era);
			if (day < 1 || day > daysInMonth)
			{
				throw new ArgumentOutOfRangeException("day", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Day"), daysInMonth, month));
			}
			long absoluteDateHijri = this.GetAbsoluteDateHijri(year, month, day);
			if (absoluteDateHijri >= 0L)
			{
				return new DateTime(absoluteDateHijri * 864000000000L + Calendar.TimeToTicks(hour, minute, second, millisecond));
			}
			throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("ArgumentOutOfRange_BadYearMonthDay"));
		}

		/// <summary>Gets or sets the last year of a 100-year range that can be represented by a 2-digit year.</summary>
		/// <returns>The last year of a 100-year range that can be represented by a 2-digit year.</returns>
		/// <exception cref="T:System.InvalidOperationException">This calendar is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value in a set operation is less than 100 or greater than 9666.</exception>
		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06002FFE RID: 12286 RVA: 0x000B9BF1 File Offset: 0x000B7DF1
		// (set) Token: 0x06002FFF RID: 12287 RVA: 0x000B9C18 File Offset: 0x000B7E18
		public override int TwoDigitYearMax
		{
			get
			{
				if (this.twoDigitYearMax == -1)
				{
					this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 1451);
				}
				return this.twoDigitYearMax;
			}
			set
			{
				base.VerifyWritable();
				if (value < 99 || value > 9666)
				{
					throw new ArgumentOutOfRangeException("value", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 99, 9666));
				}
				this.twoDigitYearMax = value;
			}
		}

		/// <summary>Converts the specified year to a four-digit year by using the <see cref="P:System.Globalization.HijriCalendar.TwoDigitYearMax" /> property to determine the appropriate century.</summary>
		/// <param name="year">A two-digit or four-digit integer that represents the year to convert.</param>
		/// <returns>An integer that contains the four-digit representation of <paramref name="year" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by this calendar.</exception>
		// Token: 0x06003000 RID: 12288 RVA: 0x000B9C70 File Offset: 0x000B7E70
		public override int ToFourDigitYear(int year)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (year < 100)
			{
				return base.ToFourDigitYear(year);
			}
			if (year > 9666)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 1, 9666));
			}
			return year;
		}

		/// <summary>Represents the current era. This field is constant.</summary>
		// Token: 0x04001470 RID: 5232
		public static readonly int HijriEra = 1;

		// Token: 0x04001471 RID: 5233
		internal const int DatePartYear = 0;

		// Token: 0x04001472 RID: 5234
		internal const int DatePartDayOfYear = 1;

		// Token: 0x04001473 RID: 5235
		internal const int DatePartMonth = 2;

		// Token: 0x04001474 RID: 5236
		internal const int DatePartDay = 3;

		// Token: 0x04001475 RID: 5237
		internal const int MinAdvancedHijri = -2;

		// Token: 0x04001476 RID: 5238
		internal const int MaxAdvancedHijri = 2;

		// Token: 0x04001477 RID: 5239
		internal static readonly int[] HijriMonthDays = new int[]
		{
			0, 30, 59, 89, 118, 148, 177, 207, 236, 266,
			295, 325, 355
		};

		// Token: 0x04001478 RID: 5240
		private const string InternationalRegKey = "Control Panel\\International";

		// Token: 0x04001479 RID: 5241
		private const string HijriAdvanceRegKeyEntry = "AddHijriDate";

		// Token: 0x0400147A RID: 5242
		private int m_HijriAdvance = int.MinValue;

		// Token: 0x0400147B RID: 5243
		internal const int MaxCalendarYear = 9666;

		// Token: 0x0400147C RID: 5244
		internal const int MaxCalendarMonth = 4;

		// Token: 0x0400147D RID: 5245
		internal const int MaxCalendarDay = 3;

		// Token: 0x0400147E RID: 5246
		internal static readonly DateTime calendarMinValue = new DateTime(622, 7, 18);

		// Token: 0x0400147F RID: 5247
		internal static readonly DateTime calendarMaxValue = DateTime.MaxValue;

		// Token: 0x04001480 RID: 5248
		private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 1451;
	}
}
