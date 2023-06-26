using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Represents the Hebrew calendar.</summary>
	// Token: 0x020003BF RID: 959
	[ComVisible(true)]
	[Serializable]
	public class HebrewCalendar : Calendar
	{
		/// <summary>Gets the earliest date and time supported by the <see cref="T:System.Globalization.HebrewCalendar" /> type.</summary>
		/// <returns>The earliest date and time supported by the <see cref="T:System.Globalization.HebrewCalendar" /> type, which is equivalent to the first moment of January, 1, 1583 C.E. in the Gregorian calendar.</returns>
		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06002FB8 RID: 12216 RVA: 0x000B89E6 File Offset: 0x000B6BE6
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return HebrewCalendar.calendarMinValue;
			}
		}

		/// <summary>Gets the latest date and time supported by the <see cref="T:System.Globalization.HebrewCalendar" /> type.</summary>
		/// <returns>The latest date and time supported by the <see cref="T:System.Globalization.HebrewCalendar" /> type, which is equivalent to the last moment of September, 29, 2239 C.E. in the Gregorian calendar.</returns>
		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06002FB9 RID: 12217 RVA: 0x000B89ED File Offset: 0x000B6BED
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return HebrewCalendar.calendarMaxValue;
			}
		}

		/// <summary>Gets a value that indicates whether the current calendar is solar-based, lunar-based, or a combination of both.</summary>
		/// <returns>Always returns <see cref="F:System.Globalization.CalendarAlgorithmType.LunisolarCalendar" />.</returns>
		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06002FBA RID: 12218 RVA: 0x000B89F4 File Offset: 0x000B6BF4
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.LunisolarCalendar;
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06002FBC RID: 12220 RVA: 0x000B89FF File Offset: 0x000B6BFF
		internal override int ID
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x06002FBD RID: 12221 RVA: 0x000B8A04 File Offset: 0x000B6C04
		private static void CheckHebrewYearValue(int y, int era, string varName)
		{
			HebrewCalendar.CheckEraRange(era);
			if (y > 5999 || y < 5343)
			{
				throw new ArgumentOutOfRangeException(varName, string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 5343, 5999));
			}
		}

		// Token: 0x06002FBE RID: 12222 RVA: 0x000B8A58 File Offset: 0x000B6C58
		private void CheckHebrewMonthValue(int year, int month, int era)
		{
			int monthsInYear = this.GetMonthsInYear(year, era);
			if (month < 1 || month > monthsInYear)
			{
				throw new ArgumentOutOfRangeException("month", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 1, monthsInYear));
			}
		}

		// Token: 0x06002FBF RID: 12223 RVA: 0x000B8AA4 File Offset: 0x000B6CA4
		private void CheckHebrewDayValue(int year, int month, int day, int era)
		{
			int daysInMonth = this.GetDaysInMonth(year, month, era);
			if (day < 1 || day > daysInMonth)
			{
				throw new ArgumentOutOfRangeException("day", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 1, daysInMonth));
			}
		}

		// Token: 0x06002FC0 RID: 12224 RVA: 0x000B8AEF File Offset: 0x000B6CEF
		internal static void CheckEraRange(int era)
		{
			if (era != 0 && era != HebrewCalendar.HebrewEra)
			{
				throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
			}
		}

		// Token: 0x06002FC1 RID: 12225 RVA: 0x000B8B14 File Offset: 0x000B6D14
		private static void CheckTicksRange(long ticks)
		{
			if (ticks < HebrewCalendar.calendarMinValue.Ticks || ticks > HebrewCalendar.calendarMaxValue.Ticks)
			{
				throw new ArgumentOutOfRangeException("time", string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("ArgumentOutOfRange_CalendarRange"), HebrewCalendar.calendarMinValue, HebrewCalendar.calendarMaxValue));
			}
		}

		// Token: 0x06002FC2 RID: 12226 RVA: 0x000B8B74 File Offset: 0x000B6D74
		internal static int GetResult(HebrewCalendar.__DateBuffer result, int part)
		{
			switch (part)
			{
			case 0:
				return result.year;
			case 2:
				return result.month;
			case 3:
				return result.day;
			}
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DateTimeParsing"));
		}

		// Token: 0x06002FC3 RID: 12227 RVA: 0x000B8BB4 File Offset: 0x000B6DB4
		internal static int GetLunarMonthDay(int gregorianYear, HebrewCalendar.__DateBuffer lunarDate)
		{
			int num = gregorianYear - 1583;
			if (num < 0 || num > 656)
			{
				throw new ArgumentOutOfRangeException("gregorianYear");
			}
			num *= 2;
			lunarDate.day = HebrewCalendar.HebrewTable[num];
			int num2 = HebrewCalendar.HebrewTable[num + 1];
			int day = lunarDate.day;
			if (day != 0)
			{
				switch (day)
				{
				case 30:
					lunarDate.month = 3;
					break;
				case 31:
					lunarDate.month = 5;
					lunarDate.day = 2;
					break;
				case 32:
					lunarDate.month = 5;
					lunarDate.day = 3;
					break;
				case 33:
					lunarDate.month = 3;
					lunarDate.day = 29;
					break;
				default:
					lunarDate.month = 4;
					break;
				}
			}
			else
			{
				lunarDate.month = 5;
				lunarDate.day = 1;
			}
			return num2;
		}

		// Token: 0x06002FC4 RID: 12228 RVA: 0x000B8C74 File Offset: 0x000B6E74
		internal virtual int GetDatePart(long ticks, int part)
		{
			HebrewCalendar.CheckTicksRange(ticks);
			DateTime dateTime = new DateTime(ticks);
			int num;
			int num2;
			int num3;
			dateTime.GetDatePart(out num, out num2, out num3);
			HebrewCalendar.__DateBuffer _DateBuffer = new HebrewCalendar.__DateBuffer();
			_DateBuffer.year = num + 3760;
			int num4 = HebrewCalendar.GetLunarMonthDay(num, _DateBuffer);
			HebrewCalendar.__DateBuffer _DateBuffer2 = new HebrewCalendar.__DateBuffer();
			_DateBuffer2.year = _DateBuffer.year;
			_DateBuffer2.month = _DateBuffer.month;
			_DateBuffer2.day = _DateBuffer.day;
			long absoluteDate = GregorianCalendar.GetAbsoluteDate(num, num2, num3);
			if (num2 == 1 && num3 == 1)
			{
				return HebrewCalendar.GetResult(_DateBuffer2, part);
			}
			long num5 = absoluteDate - GregorianCalendar.GetAbsoluteDate(num, 1, 1);
			if (num5 + (long)_DateBuffer.day <= (long)HebrewCalendar.LunarMonthLen[num4, _DateBuffer.month])
			{
				_DateBuffer2.day += (int)num5;
				return HebrewCalendar.GetResult(_DateBuffer2, part);
			}
			_DateBuffer2.month++;
			_DateBuffer2.day = 1;
			num5 -= (long)(HebrewCalendar.LunarMonthLen[num4, _DateBuffer.month] - _DateBuffer.day);
			if (num5 > 1L)
			{
				while (num5 > (long)HebrewCalendar.LunarMonthLen[num4, _DateBuffer2.month])
				{
					long num6 = num5;
					int[,] lunarMonthLen = HebrewCalendar.LunarMonthLen;
					int num7 = num4;
					HebrewCalendar.__DateBuffer _DateBuffer3 = _DateBuffer2;
					int month = _DateBuffer3.month;
					_DateBuffer3.month = month + 1;
					num5 = num6 - (long)lunarMonthLen[num7, month];
					if (_DateBuffer2.month > 13 || HebrewCalendar.LunarMonthLen[num4, _DateBuffer2.month] == 0)
					{
						_DateBuffer2.year++;
						num4 = HebrewCalendar.HebrewTable[(num + 1 - 1583) * 2 + 1];
						_DateBuffer2.month = 1;
					}
				}
				_DateBuffer2.day += (int)(num5 - 1L);
			}
			return HebrewCalendar.GetResult(_DateBuffer2, part);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of months away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add <paramref name="months" />.</param>
		/// <param name="months">The number of months to add.</param>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of months to the specified <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="months" /> is less than -120,000 or greater than 120,000.</exception>
		// Token: 0x06002FC5 RID: 12229 RVA: 0x000B8E38 File Offset: 0x000B7038
		public override DateTime AddMonths(DateTime time, int months)
		{
			DateTime dateTime;
			try
			{
				int num = this.GetDatePart(time.Ticks, 0);
				int datePart = this.GetDatePart(time.Ticks, 2);
				int num2 = this.GetDatePart(time.Ticks, 3);
				int i;
				if (months >= 0)
				{
					int num3;
					for (i = datePart + months; i > (num3 = this.GetMonthsInYear(num, 0)); i -= num3)
					{
						num++;
					}
				}
				else if ((i = datePart + months) <= 0)
				{
					months = -months;
					months -= datePart;
					num--;
					int num3;
					while (months > (num3 = this.GetMonthsInYear(num, 0)))
					{
						num--;
						months -= num3;
					}
					num3 = this.GetMonthsInYear(num, 0);
					i = num3 - months;
				}
				int daysInMonth = this.GetDaysInMonth(num, i);
				if (num2 > daysInMonth)
				{
					num2 = daysInMonth;
				}
				dateTime = this.ToDateTime(num, i, num2, 0, 0, 0, 0);
				dateTime = new DateTime(dateTime.Ticks + time.Ticks % 864000000000L);
			}
			catch (ArgumentException)
			{
				throw new ArgumentOutOfRangeException("months", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_AddValue"), Array.Empty<object>()));
			}
			return dateTime;
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of years away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add <paramref name="years" />.</param>
		/// <param name="years">The number of years to add.</param>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of years to the specified <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range.</exception>
		// Token: 0x06002FC6 RID: 12230 RVA: 0x000B8F50 File Offset: 0x000B7150
		public override DateTime AddYears(DateTime time, int years)
		{
			int num = this.GetDatePart(time.Ticks, 0);
			int num2 = this.GetDatePart(time.Ticks, 2);
			int num3 = this.GetDatePart(time.Ticks, 3);
			num += years;
			HebrewCalendar.CheckHebrewYearValue(num, 0, "years");
			int monthsInYear = this.GetMonthsInYear(num, 0);
			if (num2 > monthsInYear)
			{
				num2 = monthsInYear;
			}
			int daysInMonth = this.GetDaysInMonth(num, num2);
			if (num3 > daysInMonth)
			{
				num3 = daysInMonth;
			}
			long num4 = this.ToDateTime(num, num2, num3, 0, 0, 0, 0).Ticks + time.Ticks % 864000000000L;
			Calendar.CheckAddResult(num4, this.MinSupportedDateTime, this.MaxSupportedDateTime);
			return new DateTime(num4);
		}

		/// <summary>Returns the day of the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer from 1 to 30 that represents the day of the month in the specified <see cref="T:System.DateTime" />.</returns>
		// Token: 0x06002FC7 RID: 12231 RVA: 0x000B8FFF File Offset: 0x000B71FF
		public override int GetDayOfMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 3);
		}

		/// <summary>Returns the day of the week in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>A <see cref="T:System.DayOfWeek" /> value that represents the day of the week in the specified <see cref="T:System.DateTime" />.</returns>
		// Token: 0x06002FC8 RID: 12232 RVA: 0x000B900F File Offset: 0x000B720F
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return (DayOfWeek)(time.Ticks / 864000000000L + 1L) % (DayOfWeek)7;
		}

		// Token: 0x06002FC9 RID: 12233 RVA: 0x000B9028 File Offset: 0x000B7228
		internal static int GetHebrewYearType(int year, int era)
		{
			HebrewCalendar.CheckHebrewYearValue(year, era, "year");
			return HebrewCalendar.HebrewTable[(year - 3760 - 1583) * 2 + 1];
		}

		/// <summary>Returns the day of the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer from 1 to 385 that represents the day of the year in the specified <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> is earlier than September 17, 1583 in the Gregorian calendar, or greater than <see cref="P:System.Globalization.HebrewCalendar.MaxSupportedDateTime" />.</exception>
		// Token: 0x06002FCA RID: 12234 RVA: 0x000B9050 File Offset: 0x000B7250
		public override int GetDayOfYear(DateTime time)
		{
			int year = this.GetYear(time);
			DateTime dateTime;
			if (year == 5343)
			{
				dateTime = new DateTime(1582, 9, 27);
			}
			else
			{
				dateTime = this.ToDateTime(year, 1, 1, 0, 0, 0, 0, 0);
			}
			return (int)((time.Ticks - dateTime.Ticks) / 864000000000L) + 1;
		}

		/// <summary>Returns the number of days in the specified month in the specified year in the specified era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="month">An integer from 1 to 13 that represents the month.</param>
		/// <param name="era">An integer that represents the era. Specify either <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> or <see langword="Calendar.Eras[Calendar.CurrentEra]" />.</param>
		/// <returns>The number of days in the specified month in the specified year in the specified era.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, or <paramref name="era" /> is outside the range supported by the current <see cref="T:System.Globalization.HebrewCalendar" /> object.</exception>
		// Token: 0x06002FCB RID: 12235 RVA: 0x000B90AC File Offset: 0x000B72AC
		public override int GetDaysInMonth(int year, int month, int era)
		{
			HebrewCalendar.CheckEraRange(era);
			int hebrewYearType = HebrewCalendar.GetHebrewYearType(year, era);
			this.CheckHebrewMonthValue(year, month, era);
			int num = HebrewCalendar.LunarMonthLen[hebrewYearType, month];
			if (num == 0)
			{
				throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("ArgumentOutOfRange_Month"));
			}
			return num;
		}

		/// <summary>Returns the number of days in the specified year in the specified era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="era">An integer that represents the era. Specify either <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> or <see langword="HebrewCalendar.Eras[Calendar.CurrentEra]" />.</param>
		/// <returns>The number of days in the specified year in the specified era.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by the current <see cref="T:System.Globalization.HebrewCalendar" /> object.</exception>
		// Token: 0x06002FCC RID: 12236 RVA: 0x000B90F8 File Offset: 0x000B72F8
		public override int GetDaysInYear(int year, int era)
		{
			HebrewCalendar.CheckEraRange(era);
			int hebrewYearType = HebrewCalendar.GetHebrewYearType(year, era);
			if (hebrewYearType < 4)
			{
				return 352 + hebrewYearType;
			}
			return 382 + (hebrewYearType - 3);
		}

		/// <summary>Returns the era in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer that represents the era in the specified <see cref="T:System.DateTime" />. The return value is always <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" />.</returns>
		// Token: 0x06002FCD RID: 12237 RVA: 0x000B9128 File Offset: 0x000B7328
		public override int GetEra(DateTime time)
		{
			return HebrewCalendar.HebrewEra;
		}

		/// <summary>Gets the list of eras in the <see cref="T:System.Globalization.HebrewCalendar" />.</summary>
		/// <returns>An array of integers that represents the eras in the <see cref="T:System.Globalization.HebrewCalendar" /> type. The return value is always an array containing one element equal to <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" />.</returns>
		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06002FCE RID: 12238 RVA: 0x000B912F File Offset: 0x000B732F
		public override int[] Eras
		{
			get
			{
				return new int[] { HebrewCalendar.HebrewEra };
			}
		}

		/// <summary>Returns the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer from 1 to 13 that represents the month in the specified <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> is less than <see cref="P:System.Globalization.HebrewCalendar.MinSupportedDateTime" /> or greater than <see cref="P:System.Globalization.HebrewCalendar.MaxSupportedDateTime" />.</exception>
		// Token: 0x06002FCF RID: 12239 RVA: 0x000B913F File Offset: 0x000B733F
		public override int GetMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 2);
		}

		/// <summary>Returns the number of months in the specified year in the specified era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="era">An integer that represents the era. Specify either <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> or <see langword="HebrewCalendar.Eras[Calendar.CurrentEra]" />.</param>
		/// <returns>The number of months in the specified year in the specified era. The return value is either 12 in a common year, or 13 in a leap year.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by the current <see cref="T:System.Globalization.HebrewCalendar" /> object.</exception>
		// Token: 0x06002FD0 RID: 12240 RVA: 0x000B914F File Offset: 0x000B734F
		public override int GetMonthsInYear(int year, int era)
		{
			if (!this.IsLeapYear(year, era))
			{
				return 12;
			}
			return 13;
		}

		/// <summary>Returns the year in the specified <see cref="T:System.DateTime" /> value.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer that represents the year in the specified <see cref="T:System.DateTime" /> value.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> is outside the range supported by the current <see cref="T:System.Globalization.HebrewCalendar" /> object.</exception>
		// Token: 0x06002FD1 RID: 12241 RVA: 0x000B9160 File Offset: 0x000B7360
		public override int GetYear(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 0);
		}

		/// <summary>Determines whether the specified date in the specified era is a leap day.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="month">An integer from 1 to 13 that represents the month.</param>
		/// <param name="day">An integer from 1 to 30 that represents the day.</param>
		/// <param name="era">An integer that represents the era. Specify either <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> or <see langword="HebrewCalendar.Eras[Calendar.CurrentEra]" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified day is a leap day; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, or <paramref name="era" /> is outside the range supported by this calendar.</exception>
		// Token: 0x06002FD2 RID: 12242 RVA: 0x000B9170 File Offset: 0x000B7370
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			if (this.IsLeapMonth(year, month, era))
			{
				this.CheckHebrewDayValue(year, month, day, era);
				return true;
			}
			if (this.IsLeapYear(year, 0) && month == 6 && day == 30)
			{
				return true;
			}
			this.CheckHebrewDayValue(year, month, day, era);
			return false;
		}

		/// <summary>Calculates the leap month for a specified year and era.</summary>
		/// <param name="year">A year.</param>
		/// <param name="era">An era. Specify either <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> or <see langword="HebrewCalendar.Eras[Calendar.CurrentEra]" />.</param>
		/// <returns>A positive integer that indicates the leap month in the specified year and era. The return value is 7 if the <paramref name="year" /> and <paramref name="era" /> parameters specify a leap year, or 0 if the year is not a leap year.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="era" /> is not <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> or <see langword="HebrewCalendar.Eras[Calendar.CurrentEra]" />.  
		/// -or-  
		/// <paramref name="year" /> is less than the Hebrew calendar year 5343 or greater than the Hebrew calendar year 5999.</exception>
		// Token: 0x06002FD3 RID: 12243 RVA: 0x000B91AC File Offset: 0x000B73AC
		public override int GetLeapMonth(int year, int era)
		{
			if (this.IsLeapYear(year, era))
			{
				return 7;
			}
			return 0;
		}

		/// <summary>Determines whether the specified month in the specified year in the specified era is a leap month.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="month">An integer from 1 to 13 that represents the month.</param>
		/// <param name="era">An integer that represents the era. Specify either <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> or <see langword="HebrewCalendar.Eras[Calendar.CurrentEra]" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified month is a leap month; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, or <paramref name="era" /> is outside the range supported by this calendar.</exception>
		// Token: 0x06002FD4 RID: 12244 RVA: 0x000B91BC File Offset: 0x000B73BC
		public override bool IsLeapMonth(int year, int month, int era)
		{
			bool flag = this.IsLeapYear(year, era);
			this.CheckHebrewMonthValue(year, month, era);
			return flag && month == 7;
		}

		/// <summary>Determines whether the specified year in the specified era is a leap year.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="era">An integer that represents the era. Specify either <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> or <see langword="HebrewCalendar.Eras[Calendar.CurrentEra]" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified year is a leap year; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by this calendar.</exception>
		// Token: 0x06002FD5 RID: 12245 RVA: 0x000B91E5 File Offset: 0x000B73E5
		public override bool IsLeapYear(int year, int era)
		{
			HebrewCalendar.CheckHebrewYearValue(year, era, "year");
			return (7L * (long)year + 1L) % 19L < 7L;
		}

		// Token: 0x06002FD6 RID: 12246 RVA: 0x000B9204 File Offset: 0x000B7404
		private static int GetDayDifference(int lunarYearType, int month1, int day1, int month2, int day2)
		{
			if (month1 == month2)
			{
				return day1 - day2;
			}
			bool flag = month1 > month2;
			if (flag)
			{
				int num = month1;
				int num2 = day1;
				month1 = month2;
				day1 = day2;
				month2 = num;
				day2 = num2;
			}
			int num3 = HebrewCalendar.LunarMonthLen[lunarYearType, month1] - day1;
			month1++;
			while (month1 < month2)
			{
				num3 += HebrewCalendar.LunarMonthLen[lunarYearType, month1++];
			}
			num3 += day2;
			if (!flag)
			{
				return -num3;
			}
			return num3;
		}

		// Token: 0x06002FD7 RID: 12247 RVA: 0x000B9270 File Offset: 0x000B7470
		private static DateTime HebrewToGregorian(int hebrewYear, int hebrewMonth, int hebrewDay, int hour, int minute, int second, int millisecond)
		{
			int num = hebrewYear - 3760;
			HebrewCalendar.__DateBuffer _DateBuffer = new HebrewCalendar.__DateBuffer();
			int lunarMonthDay = HebrewCalendar.GetLunarMonthDay(num, _DateBuffer);
			if (hebrewMonth == _DateBuffer.month && hebrewDay == _DateBuffer.day)
			{
				return new DateTime(num, 1, 1, hour, minute, second, millisecond);
			}
			int dayDifference = HebrewCalendar.GetDayDifference(lunarMonthDay, hebrewMonth, hebrewDay, _DateBuffer.month, _DateBuffer.day);
			DateTime dateTime = new DateTime(num, 1, 1);
			return new DateTime(dateTime.Ticks + (long)dayDifference * 864000000000L + Calendar.TimeToTicks(hour, minute, second, millisecond));
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is set to the specified date and time in the specified era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="month">An integer from 1 to 13 that represents the month.</param>
		/// <param name="day">An integer from 1 to 30 that represents the day.</param>
		/// <param name="hour">An integer from 0 to 23 that represents the hour.</param>
		/// <param name="minute">An integer from 0 to 59 that represents the minute.</param>
		/// <param name="second">An integer from 0 to 59 that represents the second.</param>
		/// <param name="millisecond">An integer from 0 to 999 that represents the millisecond.</param>
		/// <param name="era">An integer that represents the era. Specify either <see cref="F:System.Globalization.HebrewCalendar.HebrewEra" /> or <see langword="HebrewCalendar.Eras[Calendar.CurrentEra]" />.</param>
		/// <returns>The <see cref="T:System.DateTime" /> that is set to the specified date and time in the current era.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, <paramref name="day" /> or <paramref name="era" /> is outside the range supported by the current <see cref="T:System.Globalization.HebrewCalendar" /> object.  
		/// -or-  
		/// <paramref name="hour" /> is less than 0 or greater than 23.  
		/// -or-  
		/// <paramref name="minute" /> is less than 0 or greater than 59.  
		/// -or-  
		/// <paramref name="second" /> is less than 0 or greater than 59.  
		/// -or-  
		/// <paramref name="millisecond" /> is less than 0 or greater than 999.</exception>
		// Token: 0x06002FD8 RID: 12248 RVA: 0x000B92FC File Offset: 0x000B74FC
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			HebrewCalendar.CheckHebrewYearValue(year, era, "year");
			this.CheckHebrewMonthValue(year, month, era);
			this.CheckHebrewDayValue(year, month, day, era);
			DateTime dateTime = HebrewCalendar.HebrewToGregorian(year, month, day, hour, minute, second, millisecond);
			HebrewCalendar.CheckTicksRange(dateTime.Ticks);
			return dateTime;
		}

		/// <summary>Gets or sets the last year of a 100-year range that can be represented by a 2-digit year.</summary>
		/// <returns>The last year of a 100-year range that can be represented by a 2-digit year.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Globalization.HebrewCalendar" /> object is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">In a set operation, the Hebrew calendar year value is less than 5343 but is not 99, or the year value is greater than 5999.</exception>
		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06002FD9 RID: 12249 RVA: 0x000B9349 File Offset: 0x000B7549
		// (set) Token: 0x06002FDA RID: 12250 RVA: 0x000B9370 File Offset: 0x000B7570
		public override int TwoDigitYearMax
		{
			get
			{
				if (this.twoDigitYearMax == -1)
				{
					this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 5790);
				}
				return this.twoDigitYearMax;
			}
			set
			{
				base.VerifyWritable();
				if (value != 99)
				{
					HebrewCalendar.CheckHebrewYearValue(value, HebrewCalendar.HebrewEra, "value");
				}
				this.twoDigitYearMax = value;
			}
		}

		/// <summary>Converts the specified year to a 4-digit year by using the <see cref="P:System.Globalization.HebrewCalendar.TwoDigitYearMax" /> property to determine the appropriate century.</summary>
		/// <param name="year">A 2-digit year from 0 through 99, or a 4-digit Hebrew calendar year from 5343 through 5999.</param>
		/// <returns>If the <paramref name="year" /> parameter is a 2-digit year, the return value is the corresponding 4-digit year. If the <paramref name="year" /> parameter is a 4-digit year, the return value is the unchanged <paramref name="year" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than 0.  
		/// -or-  
		/// <paramref name="year" /> is less than <see cref="P:System.Globalization.HebrewCalendar.MinSupportedDateTime" /> or greater than <see cref="P:System.Globalization.HebrewCalendar.MaxSupportedDateTime" />.</exception>
		// Token: 0x06002FDB RID: 12251 RVA: 0x000B9394 File Offset: 0x000B7594
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
			if (year > 5999 || year < 5343)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 5343, 5999));
			}
			return year;
		}

		/// <summary>Represents the current era. This field is constant.</summary>
		// Token: 0x0400145F RID: 5215
		public static readonly int HebrewEra = 1;

		// Token: 0x04001460 RID: 5216
		internal const int DatePartYear = 0;

		// Token: 0x04001461 RID: 5217
		internal const int DatePartDayOfYear = 1;

		// Token: 0x04001462 RID: 5218
		internal const int DatePartMonth = 2;

		// Token: 0x04001463 RID: 5219
		internal const int DatePartDay = 3;

		// Token: 0x04001464 RID: 5220
		internal const int DatePartDayOfWeek = 4;

		// Token: 0x04001465 RID: 5221
		private const int HebrewYearOf1AD = 3760;

		// Token: 0x04001466 RID: 5222
		private const int FirstGregorianTableYear = 1583;

		// Token: 0x04001467 RID: 5223
		private const int LastGregorianTableYear = 2239;

		// Token: 0x04001468 RID: 5224
		private const int TABLESIZE = 656;

		// Token: 0x04001469 RID: 5225
		private const int MinHebrewYear = 5343;

		// Token: 0x0400146A RID: 5226
		private const int MaxHebrewYear = 5999;

		// Token: 0x0400146B RID: 5227
		private static readonly int[] HebrewTable = new int[]
		{
			7, 3, 17, 3, 0, 4, 11, 2, 21, 6,
			1, 3, 13, 2, 25, 4, 5, 3, 16, 2,
			27, 6, 9, 1, 20, 2, 0, 6, 11, 3,
			23, 4, 4, 2, 14, 3, 27, 4, 8, 2,
			18, 3, 28, 6, 11, 1, 22, 5, 2, 3,
			12, 3, 25, 4, 6, 2, 16, 3, 26, 6,
			8, 2, 20, 1, 0, 6, 11, 2, 24, 4,
			4, 3, 15, 2, 25, 6, 8, 1, 19, 2,
			29, 6, 9, 3, 22, 4, 3, 2, 13, 3,
			25, 4, 6, 3, 17, 2, 27, 6, 7, 3,
			19, 2, 31, 4, 11, 3, 23, 4, 5, 2,
			15, 3, 25, 6, 6, 2, 19, 1, 29, 6,
			10, 2, 22, 4, 3, 3, 14, 2, 24, 6,
			6, 1, 17, 3, 28, 5, 8, 3, 20, 1,
			32, 5, 12, 3, 22, 6, 4, 1, 16, 2,
			26, 6, 6, 3, 17, 2, 0, 4, 10, 3,
			22, 4, 3, 2, 14, 3, 24, 6, 5, 2,
			17, 1, 28, 6, 9, 2, 19, 3, 31, 4,
			13, 2, 23, 6, 3, 3, 15, 1, 27, 5,
			7, 3, 17, 3, 29, 4, 11, 2, 21, 6,
			3, 1, 14, 2, 25, 6, 5, 3, 16, 2,
			28, 4, 9, 3, 20, 2, 0, 6, 12, 1,
			23, 6, 4, 2, 14, 3, 26, 4, 8, 2,
			18, 3, 0, 4, 10, 3, 21, 5, 1, 3,
			13, 1, 24, 5, 5, 3, 15, 3, 27, 4,
			8, 2, 19, 3, 29, 6, 10, 2, 22, 4,
			3, 3, 14, 2, 26, 4, 6, 3, 18, 2,
			28, 6, 10, 1, 20, 6, 2, 2, 12, 3,
			24, 4, 5, 2, 16, 3, 28, 4, 8, 3,
			19, 2, 0, 6, 12, 1, 23, 5, 3, 3,
			14, 3, 26, 4, 7, 2, 17, 3, 28, 6,
			9, 2, 21, 4, 1, 3, 13, 2, 25, 4,
			5, 3, 16, 2, 27, 6, 9, 1, 19, 3,
			0, 5, 11, 3, 23, 4, 4, 2, 14, 3,
			25, 6, 7, 1, 18, 2, 28, 6, 9, 3,
			21, 4, 2, 2, 12, 3, 25, 4, 6, 2,
			16, 3, 26, 6, 8, 2, 20, 1, 0, 6,
			11, 2, 22, 6, 4, 1, 15, 2, 25, 6,
			6, 3, 18, 1, 29, 5, 9, 3, 22, 4,
			2, 3, 13, 2, 23, 6, 4, 3, 15, 2,
			27, 4, 7, 3, 19, 2, 31, 4, 11, 3,
			21, 6, 3, 2, 15, 1, 25, 6, 6, 2,
			17, 3, 29, 4, 10, 2, 20, 6, 3, 1,
			13, 3, 24, 5, 4, 3, 16, 1, 27, 5,
			7, 3, 17, 3, 0, 4, 11, 2, 21, 6,
			1, 3, 13, 2, 25, 4, 5, 3, 16, 2,
			29, 4, 9, 3, 19, 6, 30, 2, 13, 1,
			23, 6, 4, 2, 14, 3, 27, 4, 8, 2,
			18, 3, 0, 4, 11, 3, 22, 5, 2, 3,
			14, 1, 26, 5, 6, 3, 16, 3, 28, 4,
			10, 2, 20, 6, 30, 3, 11, 2, 24, 4,
			4, 3, 15, 2, 25, 6, 8, 1, 19, 2,
			29, 6, 9, 3, 22, 4, 3, 2, 13, 3,
			25, 4, 7, 2, 17, 3, 27, 6, 9, 1,
			21, 5, 1, 3, 11, 3, 23, 4, 5, 2,
			15, 3, 25, 6, 6, 2, 19, 1, 29, 6,
			10, 2, 22, 4, 3, 3, 14, 2, 24, 6,
			6, 1, 18, 2, 28, 6, 8, 3, 20, 4,
			2, 2, 12, 3, 24, 4, 4, 3, 16, 2,
			26, 6, 6, 3, 17, 2, 0, 4, 10, 3,
			22, 4, 3, 2, 14, 3, 24, 6, 5, 2,
			17, 1, 28, 6, 9, 2, 21, 4, 1, 3,
			13, 2, 23, 6, 5, 1, 15, 3, 27, 5,
			7, 3, 19, 1, 0, 5, 10, 3, 22, 4,
			2, 3, 13, 2, 24, 6, 4, 3, 15, 2,
			27, 4, 8, 3, 20, 4, 1, 2, 11, 3,
			22, 6, 3, 2, 15, 1, 25, 6, 7, 2,
			17, 3, 29, 4, 10, 2, 21, 6, 1, 3,
			13, 1, 24, 5, 5, 3, 15, 3, 27, 4,
			8, 2, 19, 6, 1, 1, 12, 2, 22, 6,
			3, 3, 14, 2, 26, 4, 6, 3, 18, 2,
			28, 6, 10, 1, 20, 6, 2, 2, 12, 3,
			24, 4, 5, 2, 16, 3, 28, 4, 9, 2,
			19, 6, 30, 3, 12, 1, 23, 5, 3, 3,
			14, 3, 26, 4, 7, 2, 17, 3, 28, 6,
			9, 2, 21, 4, 1, 3, 13, 2, 25, 4,
			5, 3, 16, 2, 27, 6, 9, 1, 19, 6,
			30, 2, 11, 3, 23, 4, 4, 2, 14, 3,
			27, 4, 7, 3, 18, 2, 28, 6, 11, 1,
			22, 5, 2, 3, 12, 3, 25, 4, 6, 2,
			16, 3, 26, 6, 8, 2, 20, 4, 30, 3,
			11, 2, 24, 4, 4, 3, 15, 2, 25, 6,
			8, 1, 18, 3, 29, 5, 9, 3, 22, 4,
			3, 2, 13, 3, 23, 6, 6, 1, 17, 2,
			27, 6, 7, 3, 20, 4, 1, 2, 11, 3,
			23, 4, 5, 2, 15, 3, 25, 6, 6, 2,
			19, 1, 29, 6, 10, 2, 20, 6, 3, 1,
			14, 2, 24, 6, 4, 3, 17, 1, 28, 5,
			8, 3, 20, 4, 1, 3, 12, 2, 22, 6,
			2, 3, 14, 2, 26, 4, 6, 3, 17, 2,
			0, 4, 10, 3, 20, 6, 1, 2, 14, 1,
			24, 6, 5, 2, 15, 3, 28, 4, 9, 2,
			19, 6, 1, 1, 12, 3, 23, 5, 3, 3,
			15, 1, 27, 5, 7, 3, 17, 3, 29, 4,
			11, 2, 21, 6, 1, 3, 12, 2, 25, 4,
			5, 3, 16, 2, 28, 4, 9, 3, 19, 6,
			30, 2, 12, 1, 23, 6, 4, 2, 14, 3,
			26, 4, 8, 2, 18, 3, 0, 4, 10, 3,
			22, 5, 2, 3, 14, 1, 25, 5, 6, 3,
			16, 3, 28, 4, 9, 2, 20, 6, 30, 3,
			11, 2, 23, 4, 4, 3, 15, 2, 27, 4,
			7, 3, 19, 2, 29, 6, 11, 1, 21, 6,
			3, 2, 13, 3, 25, 4, 6, 2, 17, 3,
			27, 6, 9, 1, 20, 5, 30, 3, 10, 3,
			22, 4, 3, 2, 14, 3, 24, 6, 5, 2,
			17, 1, 28, 6, 9, 2, 21, 4, 1, 3,
			13, 2, 23, 6, 5, 1, 16, 2, 27, 6,
			7, 3, 19, 4, 30, 2, 11, 3, 23, 4,
			3, 3, 14, 2, 25, 6, 5, 3, 16, 2,
			28, 4, 9, 3, 21, 4, 2, 2, 12, 3,
			23, 6, 4, 2, 16, 1, 26, 6, 8, 2,
			20, 4, 30, 3, 11, 2, 22, 6, 4, 1,
			14, 3, 25, 5, 6, 3, 18, 1, 29, 5,
			9, 3, 22, 4, 2, 3, 13, 2, 23, 6,
			4, 3, 15, 2, 27, 4, 7, 3, 20, 4,
			1, 2, 11, 3, 21, 6, 3, 2, 15, 1,
			25, 6, 6, 2, 17, 3, 29, 4, 10, 2,
			20, 6, 3, 1, 13, 3, 24, 5, 4, 3,
			17, 1, 28, 5, 8, 3, 18, 6, 1, 1,
			12, 2, 22, 6, 2, 3, 14, 2, 26, 4,
			6, 3, 17, 2, 28, 6, 10, 1, 20, 6,
			1, 2, 12, 3, 24, 4, 5, 2, 15, 3,
			28, 4, 9, 2, 19, 6, 33, 3, 12, 1,
			23, 5, 3, 3, 13, 3, 25, 4, 6, 2,
			16, 3, 26, 6, 8, 2, 20, 4, 30, 3,
			11, 2, 24, 4, 4, 3, 15, 2, 25, 6,
			8, 1, 18, 6, 33, 2, 9, 3, 22, 4,
			3, 2, 13, 3, 25, 4, 6, 3, 17, 2,
			27, 6, 9, 1, 21, 5, 1, 3, 11, 3,
			23, 4, 5, 2, 15, 3, 25, 6, 6, 2,
			19, 4, 33, 3, 10, 2, 22, 4, 3, 3,
			14, 2, 24, 6, 6, 1
		};

		// Token: 0x0400146C RID: 5228
		private static readonly int[,] LunarMonthLen = new int[,]
		{
			{
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0
			},
			{
				0, 30, 29, 29, 29, 30, 29, 30, 29, 30,
				29, 30, 29, 0
			},
			{
				0, 30, 29, 30, 29, 30, 29, 30, 29, 30,
				29, 30, 29, 0
			},
			{
				0, 30, 30, 30, 29, 30, 29, 30, 29, 30,
				29, 30, 29, 0
			},
			{
				0, 30, 29, 29, 29, 30, 30, 29, 30, 29,
				30, 29, 30, 29
			},
			{
				0, 30, 29, 30, 29, 30, 30, 29, 30, 29,
				30, 29, 30, 29
			},
			{
				0, 30, 30, 30, 29, 30, 30, 29, 30, 29,
				30, 29, 30, 29
			}
		};

		// Token: 0x0400146D RID: 5229
		internal static readonly DateTime calendarMinValue = new DateTime(1583, 1, 1);

		// Token: 0x0400146E RID: 5230
		internal static readonly DateTime calendarMaxValue = new DateTime(new DateTime(2239, 9, 29, 23, 59, 59, 999).Ticks + 9999L);

		// Token: 0x0400146F RID: 5231
		private const int DEFAULT_TWO_DIGIT_YEAR_MAX = 5790;

		// Token: 0x02000B68 RID: 2920
		internal class __DateBuffer
		{
			// Token: 0x04003462 RID: 13410
			internal int year;

			// Token: 0x04003463 RID: 13411
			internal int month;

			// Token: 0x04003464 RID: 13412
			internal int day;
		}
	}
}
