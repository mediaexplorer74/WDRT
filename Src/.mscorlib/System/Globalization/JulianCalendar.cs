using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Represents the Julian calendar.</summary>
	// Token: 0x020003C5 RID: 965
	[ComVisible(true)]
	[Serializable]
	public class JulianCalendar : Calendar
	{
		/// <summary>Gets the earliest date and time supported by the <see cref="T:System.Globalization.JulianCalendar" /> class.</summary>
		/// <returns>The earliest date and time supported by the <see cref="T:System.Globalization.JulianCalendar" /> class, which is equivalent to the first moment of January 1, 0001 C.E. in the Gregorian calendar.</returns>
		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06003078 RID: 12408 RVA: 0x000BB434 File Offset: 0x000B9634
		[ComVisible(false)]
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return DateTime.MinValue;
			}
		}

		/// <summary>Gets the latest date and time supported by the <see cref="T:System.Globalization.JulianCalendar" /> class.</summary>
		/// <returns>The latest date and time supported by the <see cref="T:System.Globalization.JulianCalendar" /> class, which is equivalent to the last moment of December 31, 9999 C.E. in the Gregorian calendar.</returns>
		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06003079 RID: 12409 RVA: 0x000BB43B File Offset: 0x000B963B
		[ComVisible(false)]
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		/// <summary>Gets a value that indicates whether the current calendar is solar-based, lunar-based, or a combination of both.</summary>
		/// <returns>Always returns <see cref="F:System.Globalization.CalendarAlgorithmType.SolarCalendar" />.</returns>
		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x0600307A RID: 12410 RVA: 0x000BB442 File Offset: 0x000B9642
		[ComVisible(false)]
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.SolarCalendar;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.JulianCalendar" /> class.</summary>
		// Token: 0x0600307B RID: 12411 RVA: 0x000BB445 File Offset: 0x000B9645
		public JulianCalendar()
		{
			this.twoDigitYearMax = 2029;
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x0600307C RID: 12412 RVA: 0x000BB463 File Offset: 0x000B9663
		internal override int ID
		{
			get
			{
				return 13;
			}
		}

		// Token: 0x0600307D RID: 12413 RVA: 0x000BB467 File Offset: 0x000B9667
		internal static void CheckEraRange(int era)
		{
			if (era != 0 && era != JulianCalendar.JulianEra)
			{
				throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
			}
		}

		// Token: 0x0600307E RID: 12414 RVA: 0x000BB48C File Offset: 0x000B968C
		internal void CheckYearEraRange(int year, int era)
		{
			JulianCalendar.CheckEraRange(era);
			if (year <= 0 || year > this.MaxYear)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 1, this.MaxYear));
			}
		}

		// Token: 0x0600307F RID: 12415 RVA: 0x000BB4DC File Offset: 0x000B96DC
		internal static void CheckMonthRange(int month)
		{
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("ArgumentOutOfRange_Month"));
			}
		}

		// Token: 0x06003080 RID: 12416 RVA: 0x000BB4FC File Offset: 0x000B96FC
		internal static void CheckDayRange(int year, int month, int day)
		{
			if (year == 1 && month == 1 && day < 3)
			{
				throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("ArgumentOutOfRange_BadYearMonthDay"));
			}
			int[] array = ((year % 4 == 0) ? JulianCalendar.DaysToMonth366 : JulianCalendar.DaysToMonth365);
			int num = array[month] - array[month - 1];
			if (day < 1 || day > num)
			{
				throw new ArgumentOutOfRangeException("day", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 1, num));
			}
		}

		// Token: 0x06003081 RID: 12417 RVA: 0x000BB57C File Offset: 0x000B977C
		internal static int GetDatePart(long ticks, int part)
		{
			long num = ticks + 1728000000000L;
			int i = (int)(num / 864000000000L);
			int num2 = i / 1461;
			i -= num2 * 1461;
			int num3 = i / 365;
			if (num3 == 4)
			{
				num3 = 3;
			}
			if (part == 0)
			{
				return num2 * 4 + num3 + 1;
			}
			i -= num3 * 365;
			if (part == 1)
			{
				return i + 1;
			}
			int[] array = ((num3 == 3) ? JulianCalendar.DaysToMonth366 : JulianCalendar.DaysToMonth365);
			int num4 = i >> 6;
			while (i >= array[num4])
			{
				num4++;
			}
			if (part == 2)
			{
				return num4;
			}
			return i - array[num4 - 1] + 1;
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x000BB620 File Offset: 0x000B9820
		internal static long DateToTicks(int year, int month, int day)
		{
			int[] array = ((year % 4 == 0) ? JulianCalendar.DaysToMonth366 : JulianCalendar.DaysToMonth365);
			int num = year - 1;
			int num2 = num * 365 + num / 4 + array[month - 1] + day - 1;
			return (long)(num2 - 2) * 864000000000L;
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of months away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add months.</param>
		/// <param name="months">The number of months to add.</param>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of months to the specified <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="months" /> is less than -120000.  
		/// -or-  
		/// <paramref name="months" /> is greater than 120000.</exception>
		// Token: 0x06003083 RID: 12419 RVA: 0x000BB668 File Offset: 0x000B9868
		public override DateTime AddMonths(DateTime time, int months)
		{
			if (months < -120000 || months > 120000)
			{
				throw new ArgumentOutOfRangeException("months", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), -120000, 120000));
			}
			int num = JulianCalendar.GetDatePart(time.Ticks, 0);
			int num2 = JulianCalendar.GetDatePart(time.Ticks, 2);
			int num3 = JulianCalendar.GetDatePart(time.Ticks, 3);
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
			int[] array = ((num % 4 == 0 && (num % 100 != 0 || num % 400 == 0)) ? JulianCalendar.DaysToMonth366 : JulianCalendar.DaysToMonth365);
			int num5 = array[num2] - array[num2 - 1];
			if (num3 > num5)
			{
				num3 = num5;
			}
			long num6 = JulianCalendar.DateToTicks(num, num2, num3) + time.Ticks % 864000000000L;
			Calendar.CheckAddResult(num6, this.MinSupportedDateTime, this.MaxSupportedDateTime);
			return new DateTime(num6);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of years away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add years.</param>
		/// <param name="years">The number of years to add.</param>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of years to the specified <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range.</exception>
		// Token: 0x06003084 RID: 12420 RVA: 0x000BB77D File Offset: 0x000B997D
		public override DateTime AddYears(DateTime time, int years)
		{
			return this.AddMonths(time, years * 12);
		}

		/// <summary>Returns the day of the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer from 1 to 31 that represents the day of the month in <paramref name="time" />.</returns>
		// Token: 0x06003085 RID: 12421 RVA: 0x000BB78A File Offset: 0x000B998A
		public override int GetDayOfMonth(DateTime time)
		{
			return JulianCalendar.GetDatePart(time.Ticks, 3);
		}

		/// <summary>Returns the day of the week in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>A <see cref="T:System.DayOfWeek" /> value that represents the day of the week in <paramref name="time" />.</returns>
		// Token: 0x06003086 RID: 12422 RVA: 0x000BB799 File Offset: 0x000B9999
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return (DayOfWeek)(time.Ticks / 864000000000L + 1L) % (DayOfWeek)7;
		}

		/// <summary>Returns the day of the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer from 1 to 366 that represents the day of the year in <paramref name="time" />.</returns>
		// Token: 0x06003087 RID: 12423 RVA: 0x000BB7B2 File Offset: 0x000B99B2
		public override int GetDayOfYear(DateTime time)
		{
			return JulianCalendar.GetDatePart(time.Ticks, 1);
		}

		/// <summary>Returns the number of days in the specified month in the specified year in the specified era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="month">An integer from 1 to 12 that represents the month.</param>
		/// <param name="era">An integer that represents the era.</param>
		/// <returns>The number of days in the specified month in the specified year in the specified era.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="year" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="month" /> is outside the range supported by the calendar.</exception>
		// Token: 0x06003088 RID: 12424 RVA: 0x000BB7C4 File Offset: 0x000B99C4
		public override int GetDaysInMonth(int year, int month, int era)
		{
			this.CheckYearEraRange(year, era);
			JulianCalendar.CheckMonthRange(month);
			int[] array = ((year % 4 == 0) ? JulianCalendar.DaysToMonth366 : JulianCalendar.DaysToMonth365);
			return array[month] - array[month - 1];
		}

		/// <summary>Returns the number of days in the specified year in the specified era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="era">An integer that represents the era.</param>
		/// <returns>The number of days in the specified year in the specified era.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="year" /> is outside the range supported by the calendar.</exception>
		// Token: 0x06003089 RID: 12425 RVA: 0x000BB7FA File Offset: 0x000B99FA
		public override int GetDaysInYear(int year, int era)
		{
			if (!this.IsLeapYear(year, era))
			{
				return 365;
			}
			return 366;
		}

		/// <summary>Returns the era in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer that represents the era in <paramref name="time" />.</returns>
		// Token: 0x0600308A RID: 12426 RVA: 0x000BB811 File Offset: 0x000B9A11
		public override int GetEra(DateTime time)
		{
			return JulianCalendar.JulianEra;
		}

		/// <summary>Returns the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer from 1 to 12 that represents the month in <paramref name="time" />.</returns>
		// Token: 0x0600308B RID: 12427 RVA: 0x000BB818 File Offset: 0x000B9A18
		public override int GetMonth(DateTime time)
		{
			return JulianCalendar.GetDatePart(time.Ticks, 2);
		}

		/// <summary>Gets the list of eras in the <see cref="T:System.Globalization.JulianCalendar" />.</summary>
		/// <returns>An array of integers that represents the eras in the <see cref="T:System.Globalization.JulianCalendar" />.</returns>
		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x0600308C RID: 12428 RVA: 0x000BB827 File Offset: 0x000B9A27
		public override int[] Eras
		{
			get
			{
				return new int[] { JulianCalendar.JulianEra };
			}
		}

		/// <summary>Returns the number of months in the specified year in the specified era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="era">An integer that represents the era.</param>
		/// <returns>The number of months in the specified year in the specified era.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="year" /> is outside the range supported by the calendar.</exception>
		// Token: 0x0600308D RID: 12429 RVA: 0x000BB837 File Offset: 0x000B9A37
		public override int GetMonthsInYear(int year, int era)
		{
			this.CheckYearEraRange(year, era);
			return 12;
		}

		/// <summary>Returns the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer that represents the year in <paramref name="time" />.</returns>
		// Token: 0x0600308E RID: 12430 RVA: 0x000BB843 File Offset: 0x000B9A43
		public override int GetYear(DateTime time)
		{
			return JulianCalendar.GetDatePart(time.Ticks, 0);
		}

		/// <summary>Determines whether the specified date in the specified era is a leap day.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="month">An integer from 1 to 12 that represents the month.</param>
		/// <param name="day">An integer from 1 to 31 that represents the day.</param>
		/// <param name="era">An integer that represents the era.</param>
		/// <returns>
		///   <see langword="true" /> if the specified day is a leap day; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="month" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="day" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="era" /> is outside the range supported by the calendar.</exception>
		// Token: 0x0600308F RID: 12431 RVA: 0x000BB852 File Offset: 0x000B9A52
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			JulianCalendar.CheckMonthRange(month);
			if (this.IsLeapYear(year, era))
			{
				JulianCalendar.CheckDayRange(year, month, day);
				return month == 2 && day == 29;
			}
			JulianCalendar.CheckDayRange(year, month, day);
			return false;
		}

		/// <summary>Calculates the leap month for a specified year and era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="era">An integer that represents the era.</param>
		/// <returns>A positive integer that indicates the leap month in the specified year and era. Alternatively, this method returns zero if the calendar does not support a leap month, or if <paramref name="year" /> and <paramref name="era" /> do not specify a leap year.</returns>
		// Token: 0x06003090 RID: 12432 RVA: 0x000BB882 File Offset: 0x000B9A82
		[ComVisible(false)]
		public override int GetLeapMonth(int year, int era)
		{
			this.CheckYearEraRange(year, era);
			return 0;
		}

		/// <summary>Determines whether the specified month in the specified year in the specified era is a leap month.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="month">An integer from 1 to 12 that represents the month.</param>
		/// <param name="era">An integer that represents the era.</param>
		/// <returns>This method always returns <see langword="false" />, unless overridden by a derived class.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="month" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="era" /> is outside the range supported by the calendar.</exception>
		// Token: 0x06003091 RID: 12433 RVA: 0x000BB88D File Offset: 0x000B9A8D
		public override bool IsLeapMonth(int year, int month, int era)
		{
			this.CheckYearEraRange(year, era);
			JulianCalendar.CheckMonthRange(month);
			return false;
		}

		/// <summary>Determines whether the specified year in the specified era is a leap year.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="era">An integer that represents the era.</param>
		/// <returns>
		///   <see langword="true" /> if the specified year is a leap year; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="era" /> is outside the range supported by the calendar.</exception>
		// Token: 0x06003092 RID: 12434 RVA: 0x000BB89E File Offset: 0x000B9A9E
		public override bool IsLeapYear(int year, int era)
		{
			this.CheckYearEraRange(year, era);
			return year % 4 == 0;
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is set to the specified date and time in the specified era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="month">An integer from 1 to 12 that represents the month.</param>
		/// <param name="day">An integer from 1 to 31 that represents the day.</param>
		/// <param name="hour">An integer from 0 to 23 that represents the hour.</param>
		/// <param name="minute">An integer from 0 to 59 that represents the minute.</param>
		/// <param name="second">An integer from 0 to 59 that represents the second.</param>
		/// <param name="millisecond">An integer from 0 to 999 that represents the millisecond.</param>
		/// <param name="era">An integer that represents the era.</param>
		/// <returns>The <see cref="T:System.DateTime" /> that is set to the specified date and time in the current era.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="month" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="day" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="hour" /> is less than zero or greater than 23.  
		/// -or-  
		/// <paramref name="minute" /> is less than zero or greater than 59.  
		/// -or-  
		/// <paramref name="second" /> is less than zero or greater than 59.  
		/// -or-  
		/// <paramref name="millisecond" /> is less than zero or greater than 999.  
		/// -or-  
		/// <paramref name="era" /> is outside the range supported by the calendar.</exception>
		// Token: 0x06003093 RID: 12435 RVA: 0x000BB8B0 File Offset: 0x000B9AB0
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			this.CheckYearEraRange(year, era);
			JulianCalendar.CheckMonthRange(month);
			JulianCalendar.CheckDayRange(year, month, day);
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 0, 999));
			}
			if (hour >= 0 && hour < 24 && minute >= 0 && minute < 60 && second >= 0 && second < 60)
			{
				return new DateTime(JulianCalendar.DateToTicks(year, month, day) + new TimeSpan(0, hour, minute, second, millisecond).Ticks);
			}
			throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("ArgumentOutOfRange_BadHourMinuteSecond"));
		}

		/// <summary>Gets or sets the last year of a 100-year range that can be represented by a 2-digit year.</summary>
		/// <returns>The last year of a 100-year range that can be represented by a 2-digit year.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified in a set operation is less than 99.  
		///  -or-  
		///  The value specified in a set operation is greater than <see langword="MaxSupportedDateTime.Year" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">In a set operation, the current instance is read-only.</exception>
		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06003094 RID: 12436 RVA: 0x000BB967 File Offset: 0x000B9B67
		// (set) Token: 0x06003095 RID: 12437 RVA: 0x000BB970 File Offset: 0x000B9B70
		public override int TwoDigitYearMax
		{
			get
			{
				return this.twoDigitYearMax;
			}
			set
			{
				base.VerifyWritable();
				if (value < 99 || value > this.MaxYear)
				{
					throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 99, this.MaxYear));
				}
				this.twoDigitYearMax = value;
			}
		}

		/// <summary>Converts the specified year to a four-digit year by using the <see cref="P:System.Globalization.JulianCalendar.TwoDigitYearMax" /> property to determine the appropriate century.</summary>
		/// <param name="year">A two-digit or four-digit integer that represents the year to convert.</param>
		/// <returns>An integer that contains the four-digit representation of <paramref name="year" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.</exception>
		// Token: 0x06003096 RID: 12438 RVA: 0x000BB9CC File Offset: 0x000B9BCC
		public override int ToFourDigitYear(int year)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (year > this.MaxYear)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Bounds_Lower_Upper"), 1, this.MaxYear));
			}
			return base.ToFourDigitYear(year);
		}

		/// <summary>Represents the current era. This field is constant.</summary>
		// Token: 0x040014B3 RID: 5299
		public static readonly int JulianEra = 1;

		// Token: 0x040014B4 RID: 5300
		private const int DatePartYear = 0;

		// Token: 0x040014B5 RID: 5301
		private const int DatePartDayOfYear = 1;

		// Token: 0x040014B6 RID: 5302
		private const int DatePartMonth = 2;

		// Token: 0x040014B7 RID: 5303
		private const int DatePartDay = 3;

		// Token: 0x040014B8 RID: 5304
		private const int JulianDaysPerYear = 365;

		// Token: 0x040014B9 RID: 5305
		private const int JulianDaysPer4Years = 1461;

		// Token: 0x040014BA RID: 5306
		private static readonly int[] DaysToMonth365 = new int[]
		{
			0, 31, 59, 90, 120, 151, 181, 212, 243, 273,
			304, 334, 365
		};

		// Token: 0x040014BB RID: 5307
		private static readonly int[] DaysToMonth366 = new int[]
		{
			0, 31, 60, 91, 121, 152, 182, 213, 244, 274,
			305, 335, 366
		};

		// Token: 0x040014BC RID: 5308
		internal int MaxYear = 9999;
	}
}
