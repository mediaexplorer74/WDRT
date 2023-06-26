using System;

namespace System.Globalization
{
	/// <summary>Represents the Persian calendar.</summary>
	// Token: 0x020003C7 RID: 967
	[Serializable]
	public class PersianCalendar : Calendar
	{
		/// <summary>Gets the earliest date and time supported by the <see cref="T:System.Globalization.PersianCalendar" /> class.</summary>
		/// <returns>The earliest date and time supported by the <see cref="T:System.Globalization.PersianCalendar" /> class.</returns>
		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x060030A9 RID: 12457 RVA: 0x000BBC07 File Offset: 0x000B9E07
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return PersianCalendar.minDate;
			}
		}

		/// <summary>Gets the latest date and time supported by the <see cref="T:System.Globalization.PersianCalendar" /> class.</summary>
		/// <returns>The latest date and time supported by the <see cref="T:System.Globalization.PersianCalendar" /> class.</returns>
		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x060030AA RID: 12458 RVA: 0x000BBC0E File Offset: 0x000B9E0E
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return PersianCalendar.maxDate;
			}
		}

		/// <summary>Gets a value indicating whether the current calendar is solar-based, lunar-based, or lunisolar-based.</summary>
		/// <returns>Always returns <see cref="F:System.Globalization.CalendarAlgorithmType.SolarCalendar" />.</returns>
		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x060030AB RID: 12459 RVA: 0x000BBC15 File Offset: 0x000B9E15
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.SolarCalendar;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x060030AD RID: 12461 RVA: 0x000BBC20 File Offset: 0x000B9E20
		internal override int BaseCalendarID
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x060030AE RID: 12462 RVA: 0x000BBC23 File Offset: 0x000B9E23
		internal override int ID
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x060030AF RID: 12463 RVA: 0x000BBC28 File Offset: 0x000B9E28
		private long GetAbsoluteDatePersian(int year, int month, int day)
		{
			if (year >= 1 && year <= 9378 && month >= 1 && month <= 12)
			{
				int num = PersianCalendar.DaysInPreviousMonths(month) + day - 1;
				int num2 = (int)(365.242189 * (double)(year - 1));
				long num3 = CalendricalCalculationsHelper.PersianNewYearOnOrBefore(PersianCalendar.PersianEpoch + (long)num2 + 180L);
				return num3 + (long)num;
			}
			throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("ArgumentOutOfRange_BadYearMonthDay"));
		}

		// Token: 0x060030B0 RID: 12464 RVA: 0x000BBC94 File Offset: 0x000B9E94
		internal static void CheckTicksRange(long ticks)
		{
			if (ticks < PersianCalendar.minDate.Ticks || ticks > PersianCalendar.maxDate.Ticks)
			{
				throw new ArgumentOutOfRangeException("time", string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("ArgumentOutOfRange_CalendarRange"), PersianCalendar.minDate, PersianCalendar.maxDate));
			}
		}

		// Token: 0x060030B1 RID: 12465 RVA: 0x000BBCEE File Offset: 0x000B9EEE
		internal static void CheckEraRange(int era)
		{
			if (era != 0 && era != PersianCalendar.PersianEra)
			{
				throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
			}
		}

		// Token: 0x060030B2 RID: 12466 RVA: 0x000BBD10 File Offset: 0x000B9F10
		internal static void CheckYearRange(int year, int era)
		{
			PersianCalendar.CheckEraRange(era);
			if (year < 1 || year > 9378)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 1, 9378));
			}
		}

		// Token: 0x060030B3 RID: 12467 RVA: 0x000BBD60 File Offset: 0x000B9F60
		internal static void CheckYearMonthRange(int year, int month, int era)
		{
			PersianCalendar.CheckYearRange(year, era);
			if (year == 9378 && month > 10)
			{
				throw new ArgumentOutOfRangeException("month", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 1, 10));
			}
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("ArgumentOutOfRange_Month"));
			}
		}

		// Token: 0x060030B4 RID: 12468 RVA: 0x000BBDCC File Offset: 0x000B9FCC
		private static int MonthFromOrdinalDay(int ordinalDay)
		{
			int num = 0;
			while (ordinalDay > PersianCalendar.DaysToMonth[num])
			{
				num++;
			}
			return num;
		}

		// Token: 0x060030B5 RID: 12469 RVA: 0x000BBDEC File Offset: 0x000B9FEC
		private static int DaysInPreviousMonths(int month)
		{
			month--;
			return PersianCalendar.DaysToMonth[month];
		}

		// Token: 0x060030B6 RID: 12470 RVA: 0x000BBDFC File Offset: 0x000B9FFC
		internal int GetDatePart(long ticks, int part)
		{
			PersianCalendar.CheckTicksRange(ticks);
			long num = ticks / 864000000000L + 1L;
			long num2 = CalendricalCalculationsHelper.PersianNewYearOnOrBefore(num);
			int num3 = (int)Math.Floor((double)(num2 - PersianCalendar.PersianEpoch) / 365.242189 + 0.5) + 1;
			if (part == 0)
			{
				return num3;
			}
			int num4 = (int)(num - CalendricalCalculationsHelper.GetNumberOfDays(this.ToDateTime(num3, 1, 1, 0, 0, 0, 0, 1)));
			if (part == 1)
			{
				return num4;
			}
			int num5 = PersianCalendar.MonthFromOrdinalDay(num4);
			if (part == 2)
			{
				return num5;
			}
			int num6 = num4 - PersianCalendar.DaysInPreviousMonths(num5);
			if (part == 3)
			{
				return num6;
			}
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DateTimeParsing"));
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> object that is offset the specified number of months from the specified <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add months.</param>
		/// <param name="months">The positive or negative number of months to add.</param>
		/// <returns>A <see cref="T:System.DateTime" /> object that represents the date yielded by adding the number of months specified by the <paramref name="months" /> parameter to the date specified by the <paramref name="time" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="months" /> is less than -120,000 or greater than 120,000.</exception>
		// Token: 0x060030B7 RID: 12471 RVA: 0x000BBE9C File Offset: 0x000BA09C
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
			long num5 = this.GetAbsoluteDatePersian(num, num2, num3) * 864000000000L + time.Ticks % 864000000000L;
			Calendar.CheckAddResult(num5, this.MinSupportedDateTime, this.MaxSupportedDateTime);
			return new DateTime(num5);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> object that is offset the specified number of years from the specified <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add years.</param>
		/// <param name="years">The positive or negative number of years to add.</param>
		/// <returns>The <see cref="T:System.DateTime" /> object that results from adding the specified number of years to the specified <see cref="T:System.DateTime" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="years" /> is less than -10,000 or greater than 10,000.</exception>
		// Token: 0x060030B8 RID: 12472 RVA: 0x000BBF9A File Offset: 0x000BA19A
		public override DateTime AddYears(DateTime time, int years)
		{
			return this.AddMonths(time, years * 12);
		}

		/// <summary>Returns the day of the month in the specified <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer from 1 through 31 that represents the day of the month in the specified <see cref="T:System.DateTime" /> object.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="time" /> parameter represents a date less than <see cref="P:System.Globalization.PersianCalendar.MinSupportedDateTime" /> or greater than <see cref="P:System.Globalization.PersianCalendar.MaxSupportedDateTime" />.</exception>
		// Token: 0x060030B9 RID: 12473 RVA: 0x000BBFA7 File Offset: 0x000BA1A7
		public override int GetDayOfMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 3);
		}

		/// <summary>Returns the day of the week in the specified <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>A <see cref="T:System.DayOfWeek" /> value that represents the day of the week in the specified <see cref="T:System.DateTime" /> object.</returns>
		// Token: 0x060030BA RID: 12474 RVA: 0x000BBFB7 File Offset: 0x000BA1B7
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return (DayOfWeek)(time.Ticks / 864000000000L + 1L) % (DayOfWeek)7;
		}

		/// <summary>Returns the day of the year in the specified <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer from 1 through 366 that represents the day of the year in the specified <see cref="T:System.DateTime" /> object.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="time" /> parameter represents a date less than <see cref="P:System.Globalization.PersianCalendar.MinSupportedDateTime" /> or greater than <see cref="P:System.Globalization.PersianCalendar.MaxSupportedDateTime" />.</exception>
		// Token: 0x060030BB RID: 12475 RVA: 0x000BBFD0 File Offset: 0x000BA1D0
		public override int GetDayOfYear(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 1);
		}

		/// <summary>Returns the number of days in the specified month of the specified year and era.</summary>
		/// <param name="year">An integer from 1 through 9378 that represents the year.</param>
		/// <param name="month">An integer that represents the month, and ranges from 1 through 12 if <paramref name="year" /> is not 9378, or 1 through 10 if <paramref name="year" /> is 9378.</param>
		/// <param name="era">An integer from 0 through 1 that represents the era.</param>
		/// <returns>The number of days in the specified month of the specified year and era.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, or <paramref name="era" /> is outside the range supported by this calendar.</exception>
		// Token: 0x060030BC RID: 12476 RVA: 0x000BBFE0 File Offset: 0x000BA1E0
		public override int GetDaysInMonth(int year, int month, int era)
		{
			PersianCalendar.CheckYearMonthRange(year, month, era);
			if (month == 10 && year == 9378)
			{
				return 13;
			}
			int num = PersianCalendar.DaysToMonth[month] - PersianCalendar.DaysToMonth[month - 1];
			if (month == 12 && !this.IsLeapYear(year))
			{
				num--;
			}
			return num;
		}

		/// <summary>Returns the number of days in the specified year of the specified era.</summary>
		/// <param name="year">An integer from 1 through 9378 that represents the year.</param>
		/// <param name="era">An integer from 0 through 1 that represents the era.</param>
		/// <returns>The number of days in the specified year and era. The number of days is 365 in a common year or 366 in a leap year.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by this calendar.</exception>
		// Token: 0x060030BD RID: 12477 RVA: 0x000BC02A File Offset: 0x000BA22A
		public override int GetDaysInYear(int year, int era)
		{
			PersianCalendar.CheckYearRange(year, era);
			if (year == 9378)
			{
				return PersianCalendar.DaysToMonth[9] + 13;
			}
			if (!this.IsLeapYear(year, 0))
			{
				return 365;
			}
			return 366;
		}

		/// <summary>Returns the era in the specified <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>Always returns <see cref="F:System.Globalization.PersianCalendar.PersianEra" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="time" /> parameter represents a date less than <see cref="P:System.Globalization.PersianCalendar.MinSupportedDateTime" /> or greater than <see cref="P:System.Globalization.PersianCalendar.MaxSupportedDateTime" />.</exception>
		// Token: 0x060030BE RID: 12478 RVA: 0x000BC05C File Offset: 0x000BA25C
		public override int GetEra(DateTime time)
		{
			PersianCalendar.CheckTicksRange(time.Ticks);
			return PersianCalendar.PersianEra;
		}

		/// <summary>Gets the list of eras in a <see cref="T:System.Globalization.PersianCalendar" /> object.</summary>
		/// <returns>An array of integers that represents the eras in a <see cref="T:System.Globalization.PersianCalendar" /> object. The array consists of a single element having a value of <see cref="F:System.Globalization.PersianCalendar.PersianEra" />.</returns>
		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x060030BF RID: 12479 RVA: 0x000BC06F File Offset: 0x000BA26F
		public override int[] Eras
		{
			get
			{
				return new int[] { PersianCalendar.PersianEra };
			}
		}

		/// <summary>Returns the month in the specified <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer from 1 through 12 that represents the month in the specified <see cref="T:System.DateTime" /> object.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="time" /> parameter represents a date less than <see cref="P:System.Globalization.PersianCalendar.MinSupportedDateTime" /> or greater than <see cref="P:System.Globalization.PersianCalendar.MaxSupportedDateTime" />.</exception>
		// Token: 0x060030C0 RID: 12480 RVA: 0x000BC07F File Offset: 0x000BA27F
		public override int GetMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 2);
		}

		/// <summary>Returns the number of months in the specified year of the specified era.</summary>
		/// <param name="year">An integer from 1 through 9378 that represents the year.</param>
		/// <param name="era">An integer from 0 through 1 that represents the era.</param>
		/// <returns>Returns 10 if the <paramref name="year" /> parameter is 9378; otherwise, always returns 12.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by this calendar.</exception>
		// Token: 0x060030C1 RID: 12481 RVA: 0x000BC08F File Offset: 0x000BA28F
		public override int GetMonthsInYear(int year, int era)
		{
			PersianCalendar.CheckYearRange(year, era);
			if (year == 9378)
			{
				return 10;
			}
			return 12;
		}

		/// <summary>Returns the year in the specified <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer from 1 through 9378 that represents the year in the specified <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="time" /> parameter represents a date less than <see cref="P:System.Globalization.PersianCalendar.MinSupportedDateTime" /> or greater than <see cref="P:System.Globalization.PersianCalendar.MaxSupportedDateTime" />.</exception>
		// Token: 0x060030C2 RID: 12482 RVA: 0x000BC0A5 File Offset: 0x000BA2A5
		public override int GetYear(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 0);
		}

		/// <summary>Determines whether the specified date is a leap day.</summary>
		/// <param name="year">An integer from 1 through 9378 that represents the year.</param>
		/// <param name="month">An integer that represents the month and ranges from 1 through 12 if <paramref name="year" /> is not 9378, or 1 through 10 if <paramref name="year" /> is 9378.</param>
		/// <param name="day">An integer from 1 through 31 that represents the day.</param>
		/// <param name="era">An integer from 0 through 1 that represents the era.</param>
		/// <returns>
		///   <see langword="true" /> if the specified day is a leap day; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, or <paramref name="era" /> is outside the range supported by this calendar.</exception>
		// Token: 0x060030C3 RID: 12483 RVA: 0x000BC0B8 File Offset: 0x000BA2B8
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			int daysInMonth = this.GetDaysInMonth(year, month, era);
			if (day < 1 || day > daysInMonth)
			{
				throw new ArgumentOutOfRangeException("day", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Day"), daysInMonth, month));
			}
			return this.IsLeapYear(year, era) && month == 12 && day == 30;
		}

		/// <summary>Returns the leap month for a specified year and era.</summary>
		/// <param name="year">An integer from 1 through 9378 that represents the year to convert.</param>
		/// <param name="era">An integer from 0 through 1 that represents the era.</param>
		/// <returns>The return value is always 0.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by this calendar.</exception>
		// Token: 0x060030C4 RID: 12484 RVA: 0x000BC11A File Offset: 0x000BA31A
		public override int GetLeapMonth(int year, int era)
		{
			PersianCalendar.CheckYearRange(year, era);
			return 0;
		}

		/// <summary>Determines whether the specified month in the specified year and era is a leap month.</summary>
		/// <param name="year">An integer from 1 through 9378 that represents the year.</param>
		/// <param name="month">An integer that represents the month and ranges from 1 through 12 if <paramref name="year" /> is not 9378, or 1 through 10 if <paramref name="year" /> is 9378.</param>
		/// <param name="era">An integer from 0 through 1 that represents the era.</param>
		/// <returns>Always returns <see langword="false" /> because the <see cref="T:System.Globalization.PersianCalendar" /> class does not support the notion of a leap month.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, or <paramref name="era" /> is outside the range supported by this calendar.</exception>
		// Token: 0x060030C5 RID: 12485 RVA: 0x000BC124 File Offset: 0x000BA324
		public override bool IsLeapMonth(int year, int month, int era)
		{
			PersianCalendar.CheckYearMonthRange(year, month, era);
			return false;
		}

		/// <summary>Determines whether the specified year in the specified era is a leap year.</summary>
		/// <param name="year">An integer from 1 through 9378 that represents the year.</param>
		/// <param name="era">An integer from 0 through 1 that represents the era.</param>
		/// <returns>
		///   <see langword="true" /> if the specified year is a leap year; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by this calendar.</exception>
		// Token: 0x060030C6 RID: 12486 RVA: 0x000BC12F File Offset: 0x000BA32F
		public override bool IsLeapYear(int year, int era)
		{
			PersianCalendar.CheckYearRange(year, era);
			return year != 9378 && this.GetAbsoluteDatePersian(year + 1, 1, 1) - this.GetAbsoluteDatePersian(year, 1, 1) == 366L;
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> object that is set to the specified date, time, and era.</summary>
		/// <param name="year">An integer from 1 through 9378 that represents the year.</param>
		/// <param name="month">An integer from 1 through 12 that represents the month.</param>
		/// <param name="day">An integer from 1 through 31 that represents the day.</param>
		/// <param name="hour">An integer from 0 through 23 that represents the hour.</param>
		/// <param name="minute">An integer from 0 through 59 that represents the minute.</param>
		/// <param name="second">An integer from 0 through 59 that represents the second.</param>
		/// <param name="millisecond">An integer from 0 through 999 that represents the millisecond.</param>
		/// <param name="era">An integer from 0 through 1 that represents the era.</param>
		/// <returns>A <see cref="T:System.DateTime" /> object that is set to the specified date and time in the current era.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, <paramref name="hour" />, <paramref name="minute" />, <paramref name="second" />, <paramref name="millisecond" />, or <paramref name="era" /> is outside the range supported by this calendar.</exception>
		// Token: 0x060030C7 RID: 12487 RVA: 0x000BC160 File Offset: 0x000BA360
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			int daysInMonth = this.GetDaysInMonth(year, month, era);
			if (day < 1 || day > daysInMonth)
			{
				throw new ArgumentOutOfRangeException("day", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Day"), daysInMonth, month));
			}
			long absoluteDatePersian = this.GetAbsoluteDatePersian(year, month, day);
			if (absoluteDatePersian >= 0L)
			{
				return new DateTime(absoluteDatePersian * 864000000000L + Calendar.TimeToTicks(hour, minute, second, millisecond));
			}
			throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("ArgumentOutOfRange_BadYearMonthDay"));
		}

		/// <summary>Gets or sets the last year of a 100-year range that can be represented by a 2-digit year.</summary>
		/// <returns>The last year of a 100-year range that can be represented by a 2-digit year.</returns>
		/// <exception cref="T:System.InvalidOperationException">This calendar is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value in a set operation is less than 100 or greater than 9378.</exception>
		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x060030C8 RID: 12488 RVA: 0x000BC1E9 File Offset: 0x000BA3E9
		// (set) Token: 0x060030C9 RID: 12489 RVA: 0x000BC210 File Offset: 0x000BA410
		public override int TwoDigitYearMax
		{
			get
			{
				if (this.twoDigitYearMax == -1)
				{
					this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 1410);
				}
				return this.twoDigitYearMax;
			}
			set
			{
				base.VerifyWritable();
				if (value < 99 || value > 9378)
				{
					throw new ArgumentOutOfRangeException("value", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 99, 9378));
				}
				this.twoDigitYearMax = value;
			}
		}

		/// <summary>Converts the specified year to a four-digit year representation.</summary>
		/// <param name="year">An integer from 1 through 9378 that represents the year to convert.</param>
		/// <returns>An integer that contains the four-digit representation of <paramref name="year" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than 0 or greater than 9378.</exception>
		// Token: 0x060030CA RID: 12490 RVA: 0x000BC268 File Offset: 0x000BA468
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
			if (year > 9378)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 1, 9378));
			}
			return year;
		}

		/// <summary>Represents the current era. This field is constant.</summary>
		// Token: 0x040014C9 RID: 5321
		public static readonly int PersianEra = 1;

		// Token: 0x040014CA RID: 5322
		internal static long PersianEpoch = new DateTime(622, 3, 22).Ticks / 864000000000L;

		// Token: 0x040014CB RID: 5323
		private const int ApproximateHalfYear = 180;

		// Token: 0x040014CC RID: 5324
		internal const int DatePartYear = 0;

		// Token: 0x040014CD RID: 5325
		internal const int DatePartDayOfYear = 1;

		// Token: 0x040014CE RID: 5326
		internal const int DatePartMonth = 2;

		// Token: 0x040014CF RID: 5327
		internal const int DatePartDay = 3;

		// Token: 0x040014D0 RID: 5328
		internal const int MonthsPerYear = 12;

		// Token: 0x040014D1 RID: 5329
		internal static int[] DaysToMonth = new int[]
		{
			0, 31, 62, 93, 124, 155, 186, 216, 246, 276,
			306, 336, 366
		};

		// Token: 0x040014D2 RID: 5330
		internal const int MaxCalendarYear = 9378;

		// Token: 0x040014D3 RID: 5331
		internal const int MaxCalendarMonth = 10;

		// Token: 0x040014D4 RID: 5332
		internal const int MaxCalendarDay = 13;

		// Token: 0x040014D5 RID: 5333
		internal static DateTime minDate = new DateTime(622, 3, 22);

		// Token: 0x040014D6 RID: 5334
		internal static DateTime maxDate = DateTime.MaxValue;

		// Token: 0x040014D7 RID: 5335
		private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 1410;
	}
}
