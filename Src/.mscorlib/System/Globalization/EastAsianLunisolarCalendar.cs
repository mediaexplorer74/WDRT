using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Represents a calendar that divides time into months, days, years, and eras, and has dates that are based on cycles of the sun and the moon.</summary>
	// Token: 0x020003C3 RID: 963
	[ComVisible(true)]
	[Serializable]
	public abstract class EastAsianLunisolarCalendar : Calendar
	{
		/// <summary>Gets a value indicating whether the current calendar is solar-based, lunar-based, or a combination of both.</summary>
		/// <returns>Always returns <see cref="F:System.Globalization.CalendarAlgorithmType.LunisolarCalendar" />.</returns>
		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06003039 RID: 12345 RVA: 0x000BA6AF File Offset: 0x000B88AF
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.LunisolarCalendar;
			}
		}

		/// <summary>Calculates the year in the sexagenary (60-year) cycle that corresponds to the specified date.</summary>
		/// <param name="time">A <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>A number from 1 through 60 in the sexagenary cycle that corresponds to the <paramref name="date" /> parameter.</returns>
		// Token: 0x0600303A RID: 12346 RVA: 0x000BA6B4 File Offset: 0x000B88B4
		public virtual int GetSexagenaryYear(DateTime time)
		{
			this.CheckTicksRange(time.Ticks);
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			this.TimeToLunar(time, ref num, ref num2, ref num3);
			return (num - 4) % 60 + 1;
		}

		/// <summary>Calculates the celestial stem of the specified year in the sexagenary (60-year) cycle.</summary>
		/// <param name="sexagenaryYear">An integer from 1 through 60 that represents a year in the sexagenary cycle.</param>
		/// <returns>A number from 1 through 10.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="sexagenaryYear" /> is less than 1 or greater than 60.</exception>
		// Token: 0x0600303B RID: 12347 RVA: 0x000BA6EC File Offset: 0x000B88EC
		public int GetCelestialStem(int sexagenaryYear)
		{
			if (sexagenaryYear < 1 || sexagenaryYear > 60)
			{
				throw new ArgumentOutOfRangeException("sexagenaryYear", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[] { 1, 60 }));
			}
			return (sexagenaryYear - 1) % 10 + 1;
		}

		/// <summary>Calculates the terrestrial branch of the specified year in the sexagenary (60-year) cycle.</summary>
		/// <param name="sexagenaryYear">An integer from 1 through 60 that represents a year in the sexagenary cycle.</param>
		/// <returns>An integer from 1 through 12.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="sexagenaryYear" /> is less than 1 or greater than 60.</exception>
		// Token: 0x0600303C RID: 12348 RVA: 0x000BA738 File Offset: 0x000B8938
		public int GetTerrestrialBranch(int sexagenaryYear)
		{
			if (sexagenaryYear < 1 || sexagenaryYear > 60)
			{
				throw new ArgumentOutOfRangeException("sexagenaryYear", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[] { 1, 60 }));
			}
			return (sexagenaryYear - 1) % 12 + 1;
		}

		// Token: 0x0600303D RID: 12349
		internal abstract int GetYearInfo(int LunarYear, int Index);

		// Token: 0x0600303E RID: 12350
		internal abstract int GetYear(int year, DateTime time);

		// Token: 0x0600303F RID: 12351
		internal abstract int GetGregorianYear(int year, int era);

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06003040 RID: 12352
		internal abstract int MinCalendarYear { get; }

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06003041 RID: 12353
		internal abstract int MaxCalendarYear { get; }

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06003042 RID: 12354
		internal abstract EraInfo[] CalEraInfo { get; }

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06003043 RID: 12355
		internal abstract DateTime MinDate { get; }

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06003044 RID: 12356
		internal abstract DateTime MaxDate { get; }

		// Token: 0x06003045 RID: 12357 RVA: 0x000BA784 File Offset: 0x000B8984
		internal int MinEraCalendarYear(int era)
		{
			EraInfo[] calEraInfo = this.CalEraInfo;
			if (calEraInfo == null)
			{
				return this.MinCalendarYear;
			}
			if (era == 0)
			{
				era = this.CurrentEraValue;
			}
			if (era == this.GetEra(this.MinDate))
			{
				return this.GetYear(this.MinCalendarYear, this.MinDate);
			}
			for (int i = 0; i < calEraInfo.Length; i++)
			{
				if (era == calEraInfo[i].era)
				{
					return calEraInfo[i].minEraYear;
				}
			}
			throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
		}

		// Token: 0x06003046 RID: 12358 RVA: 0x000BA808 File Offset: 0x000B8A08
		internal int MaxEraCalendarYear(int era)
		{
			EraInfo[] calEraInfo = this.CalEraInfo;
			if (calEraInfo == null)
			{
				return this.MaxCalendarYear;
			}
			if (era == 0)
			{
				era = this.CurrentEraValue;
			}
			if (era == this.GetEra(this.MaxDate))
			{
				return this.GetYear(this.MaxCalendarYear, this.MaxDate);
			}
			for (int i = 0; i < calEraInfo.Length; i++)
			{
				if (era == calEraInfo[i].era)
				{
					return calEraInfo[i].maxEraYear;
				}
			}
			throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
		}

		// Token: 0x06003047 RID: 12359 RVA: 0x000BA889 File Offset: 0x000B8A89
		internal EastAsianLunisolarCalendar()
		{
		}

		// Token: 0x06003048 RID: 12360 RVA: 0x000BA894 File Offset: 0x000B8A94
		internal void CheckTicksRange(long ticks)
		{
			if (ticks < this.MinSupportedDateTime.Ticks || ticks > this.MaxSupportedDateTime.Ticks)
			{
				throw new ArgumentOutOfRangeException("time", string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("ArgumentOutOfRange_CalendarRange"), this.MinSupportedDateTime, this.MaxSupportedDateTime));
			}
		}

		// Token: 0x06003049 RID: 12361 RVA: 0x000BA8F8 File Offset: 0x000B8AF8
		internal void CheckEraRange(int era)
		{
			if (era == 0)
			{
				era = this.CurrentEraValue;
			}
			if (era < this.GetEra(this.MinDate) || era > this.GetEra(this.MaxDate))
			{
				throw new ArgumentOutOfRangeException("era", Environment.GetResourceString("ArgumentOutOfRange_InvalidEraValue"));
			}
		}

		// Token: 0x0600304A RID: 12362 RVA: 0x000BA938 File Offset: 0x000B8B38
		internal int CheckYearRange(int year, int era)
		{
			this.CheckEraRange(era);
			year = this.GetGregorianYear(year, era);
			if (year < this.MinCalendarYear || year > this.MaxCalendarYear)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[]
				{
					this.MinEraCalendarYear(era),
					this.MaxEraCalendarYear(era)
				}));
			}
			return year;
		}

		// Token: 0x0600304B RID: 12363 RVA: 0x000BA9A4 File Offset: 0x000B8BA4
		internal int CheckYearMonthRange(int year, int month, int era)
		{
			year = this.CheckYearRange(year, era);
			if (month == 13 && this.GetYearInfo(year, 0) == 0)
			{
				throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("ArgumentOutOfRange_Month"));
			}
			if (month < 1 || month > 13)
			{
				throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("ArgumentOutOfRange_Month"));
			}
			return year;
		}

		// Token: 0x0600304C RID: 12364 RVA: 0x000BAA00 File Offset: 0x000B8C00
		internal int InternalGetDaysInMonth(int year, int month)
		{
			int num = 32768;
			num >>= month - 1;
			int num2;
			if ((this.GetYearInfo(year, 3) & num) == 0)
			{
				num2 = 29;
			}
			else
			{
				num2 = 30;
			}
			return num2;
		}

		/// <summary>Calculates the number of days in the specified month of the specified year and era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="month">An integer from 1 through 12 in a common year, or 1 through 13 in a leap year, that represents the month.</param>
		/// <param name="era">An integer that represents the era.</param>
		/// <returns>The number of days in the specified month of the specified year and era.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, or <paramref name="era" /> is outside the range supported by this calendar.</exception>
		// Token: 0x0600304D RID: 12365 RVA: 0x000BAA31 File Offset: 0x000B8C31
		public override int GetDaysInMonth(int year, int month, int era)
		{
			year = this.CheckYearMonthRange(year, month, era);
			return this.InternalGetDaysInMonth(year, month);
		}

		// Token: 0x0600304E RID: 12366 RVA: 0x000BAA46 File Offset: 0x000B8C46
		private static int GregorianIsLeapYear(int y)
		{
			if (y % 4 != 0)
			{
				return 0;
			}
			if (y % 100 != 0)
			{
				return 1;
			}
			if (y % 400 == 0)
			{
				return 1;
			}
			return 0;
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is set to the specified date, time, and era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="month">An integer from 1 through 13 that represents the month.</param>
		/// <param name="day">An integer from 1 through 31 that represents the day.</param>
		/// <param name="hour">An integer from 0 through 23 that represents the hour.</param>
		/// <param name="minute">An integer from 0 through 59 that represents the minute.</param>
		/// <param name="second">An integer from 0 through 59 that represents the second.</param>
		/// <param name="millisecond">An integer from 0 through 999 that represents the millisecond.</param>
		/// <param name="era">An integer that represents the era.</param>
		/// <returns>A <see cref="T:System.DateTime" /> that is set to the specified date, time, and era.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, <paramref name="hour" />, <paramref name="minute" />, <paramref name="second" />, <paramref name="millisecond" />, or <paramref name="era" /> is outside the range supported by this calendar.</exception>
		// Token: 0x0600304F RID: 12367 RVA: 0x000BAA64 File Offset: 0x000B8C64
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			year = this.CheckYearMonthRange(year, month, era);
			int num = this.InternalGetDaysInMonth(year, month);
			if (day < 1 || day > num)
			{
				throw new ArgumentOutOfRangeException("day", Environment.GetResourceString("ArgumentOutOfRange_Day", new object[] { num, month }));
			}
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			if (this.LunarToGregorian(year, month, day, ref num2, ref num3, ref num4))
			{
				return new DateTime(num2, num3, num4, hour, minute, second, millisecond);
			}
			throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("ArgumentOutOfRange_BadYearMonthDay"));
		}

		// Token: 0x06003050 RID: 12368 RVA: 0x000BAAF4 File Offset: 0x000B8CF4
		internal void GregorianToLunar(int nSYear, int nSMonth, int nSDate, ref int nLYear, ref int nLMonth, ref int nLDate)
		{
			int num = EastAsianLunisolarCalendar.GregorianIsLeapYear(nSYear);
			int num2 = ((num == 1) ? EastAsianLunisolarCalendar.DaysToMonth366[nSMonth - 1] : EastAsianLunisolarCalendar.DaysToMonth365[nSMonth - 1]);
			num2 += nSDate;
			int i = num2;
			nLYear = nSYear;
			int num3;
			int num4;
			if (nLYear == this.MaxCalendarYear + 1)
			{
				nLYear--;
				i += ((EastAsianLunisolarCalendar.GregorianIsLeapYear(nLYear) == 1) ? 366 : 365);
				num3 = this.GetYearInfo(nLYear, 1);
				num4 = this.GetYearInfo(nLYear, 2);
			}
			else
			{
				num3 = this.GetYearInfo(nLYear, 1);
				num4 = this.GetYearInfo(nLYear, 2);
				if (nSMonth < num3 || (nSMonth == num3 && nSDate < num4))
				{
					nLYear--;
					i += ((EastAsianLunisolarCalendar.GregorianIsLeapYear(nLYear) == 1) ? 366 : 365);
					num3 = this.GetYearInfo(nLYear, 1);
					num4 = this.GetYearInfo(nLYear, 2);
				}
			}
			i -= EastAsianLunisolarCalendar.DaysToMonth365[num3 - 1];
			i -= num4 - 1;
			int num5 = 32768;
			int yearInfo = this.GetYearInfo(nLYear, 3);
			int num6 = (((yearInfo & num5) != 0) ? 30 : 29);
			nLMonth = 1;
			while (i > num6)
			{
				i -= num6;
				nLMonth++;
				num5 >>= 1;
				num6 = (((yearInfo & num5) != 0) ? 30 : 29);
			}
			nLDate = i;
		}

		// Token: 0x06003051 RID: 12369 RVA: 0x000BAC3C File Offset: 0x000B8E3C
		internal bool LunarToGregorian(int nLYear, int nLMonth, int nLDate, ref int nSolarYear, ref int nSolarMonth, ref int nSolarDay)
		{
			if (nLDate < 1 || nLDate > 30)
			{
				return false;
			}
			int num = nLDate - 1;
			for (int i = 1; i < nLMonth; i++)
			{
				num += this.InternalGetDaysInMonth(nLYear, i);
			}
			int yearInfo = this.GetYearInfo(nLYear, 1);
			int yearInfo2 = this.GetYearInfo(nLYear, 2);
			int num2 = EastAsianLunisolarCalendar.GregorianIsLeapYear(nLYear);
			int[] array = ((num2 == 1) ? EastAsianLunisolarCalendar.DaysToMonth366 : EastAsianLunisolarCalendar.DaysToMonth365);
			nSolarDay = yearInfo2;
			if (yearInfo > 1)
			{
				nSolarDay += array[yearInfo - 1];
			}
			nSolarDay += num;
			if (nSolarDay > num2 + 365)
			{
				nSolarYear = nLYear + 1;
				nSolarDay -= num2 + 365;
			}
			else
			{
				nSolarYear = nLYear;
			}
			nSolarMonth = 1;
			while (nSolarMonth < 12 && array[nSolarMonth] < nSolarDay)
			{
				nSolarMonth++;
			}
			nSolarDay -= array[nSolarMonth - 1];
			return true;
		}

		// Token: 0x06003052 RID: 12370 RVA: 0x000BAD14 File Offset: 0x000B8F14
		internal DateTime LunarToTime(DateTime time, int year, int month, int day)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			this.LunarToGregorian(year, month, day, ref num, ref num2, ref num3);
			return GregorianCalendar.GetDefaultInstance().ToDateTime(num, num2, num3, time.Hour, time.Minute, time.Second, time.Millisecond);
		}

		// Token: 0x06003053 RID: 12371 RVA: 0x000BAD64 File Offset: 0x000B8F64
		internal void TimeToLunar(DateTime time, ref int year, ref int month, ref int day)
		{
			Calendar defaultInstance = GregorianCalendar.GetDefaultInstance();
			int year2 = defaultInstance.GetYear(time);
			int month2 = defaultInstance.GetMonth(time);
			int dayOfMonth = defaultInstance.GetDayOfMonth(time);
			this.GregorianToLunar(year2, month2, dayOfMonth, ref year, ref month, ref day);
		}

		/// <summary>Calculates the date that is the specified number of months away from the specified date.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add <paramref name="months" />.</param>
		/// <param name="months">The number of months to add.</param>
		/// <returns>A new <see cref="T:System.DateTime" /> that results from adding the specified number of months to the <paramref name="time" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentException">The result is outside the supported range of a <see cref="T:System.DateTime" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="months" /> is less than -120000 or greater than 120000.  
		/// -or-  
		/// <paramref name="time" /> is less than <see cref="P:System.Globalization.Calendar.MinSupportedDateTime" /> or greater than <see cref="P:System.Globalization.Calendar.MaxSupportedDateTime" />.</exception>
		// Token: 0x06003054 RID: 12372 RVA: 0x000BADA4 File Offset: 0x000B8FA4
		public override DateTime AddMonths(DateTime time, int months)
		{
			if (months < -120000 || months > 120000)
			{
				throw new ArgumentOutOfRangeException("months", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[] { -120000, 120000 }));
			}
			this.CheckTicksRange(time.Ticks);
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			this.TimeToLunar(time, ref num, ref num2, ref num3);
			int i = num2 + months;
			if (i > 0)
			{
				int num4 = (this.InternalIsLeapYear(num) ? 13 : 12);
				while (i - num4 > 0)
				{
					i -= num4;
					num++;
					num4 = (this.InternalIsLeapYear(num) ? 13 : 12);
				}
				num2 = i;
			}
			else
			{
				while (i <= 0)
				{
					int num5 = (this.InternalIsLeapYear(num - 1) ? 13 : 12);
					i += num5;
					num--;
				}
				num2 = i;
			}
			int num6 = this.InternalGetDaysInMonth(num, num2);
			if (num3 > num6)
			{
				num3 = num6;
			}
			DateTime dateTime = this.LunarToTime(time, num, num2, num3);
			Calendar.CheckAddResult(dateTime.Ticks, this.MinSupportedDateTime, this.MaxSupportedDateTime);
			return dateTime;
		}

		/// <summary>Calculates the date that is the specified number of years away from the specified date.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add <paramref name="years" />.</param>
		/// <param name="years">The number of years to add.</param>
		/// <returns>A new <see cref="T:System.DateTime" /> that results from adding the specified number of years to the <paramref name="time" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentException">The result is outside the supported range of a <see cref="T:System.DateTime" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> is less than <see cref="P:System.Globalization.Calendar.MinSupportedDateTime" /> or greater than <see cref="P:System.Globalization.Calendar.MaxSupportedDateTime" />.</exception>
		// Token: 0x06003055 RID: 12373 RVA: 0x000BAEB0 File Offset: 0x000B90B0
		public override DateTime AddYears(DateTime time, int years)
		{
			this.CheckTicksRange(time.Ticks);
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			this.TimeToLunar(time, ref num, ref num2, ref num3);
			num += years;
			if (num2 == 13 && !this.InternalIsLeapYear(num))
			{
				num2 = 12;
				num3 = this.InternalGetDaysInMonth(num, num2);
			}
			int num4 = this.InternalGetDaysInMonth(num, num2);
			if (num3 > num4)
			{
				num3 = num4;
			}
			DateTime dateTime = this.LunarToTime(time, num, num2, num3);
			Calendar.CheckAddResult(dateTime.Ticks, this.MinSupportedDateTime, this.MaxSupportedDateTime);
			return dateTime;
		}

		/// <summary>Calculates the day of the year in the specified date.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer from 1 through 354 in a common year, or 1 through 384 in a leap year, that represents the day of the year specified in the <paramref name="time" /> parameter.</returns>
		// Token: 0x06003056 RID: 12374 RVA: 0x000BAF30 File Offset: 0x000B9130
		public override int GetDayOfYear(DateTime time)
		{
			this.CheckTicksRange(time.Ticks);
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			this.TimeToLunar(time, ref num, ref num2, ref num3);
			for (int i = 1; i < num2; i++)
			{
				num3 += this.InternalGetDaysInMonth(num, i);
			}
			return num3;
		}

		/// <summary>Calculates the day of the month in the specified date.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer from 1 through 31 that represents the day of the month specified in the <paramref name="time" /> parameter.</returns>
		// Token: 0x06003057 RID: 12375 RVA: 0x000BAF78 File Offset: 0x000B9178
		public override int GetDayOfMonth(DateTime time)
		{
			this.CheckTicksRange(time.Ticks);
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			this.TimeToLunar(time, ref num, ref num2, ref num3);
			return num3;
		}

		/// <summary>Calculates the number of days in the specified year and era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="era">An integer that represents the era.</param>
		/// <returns>The number of days in the specified year and era.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by this calendar.</exception>
		// Token: 0x06003058 RID: 12376 RVA: 0x000BAFA8 File Offset: 0x000B91A8
		public override int GetDaysInYear(int year, int era)
		{
			year = this.CheckYearRange(year, era);
			int num = 0;
			int num2 = (this.InternalIsLeapYear(year) ? 13 : 12);
			while (num2 != 0)
			{
				num += this.InternalGetDaysInMonth(year, num2--);
			}
			return num;
		}

		/// <summary>Returns the month in the specified date.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer from 1 to 13 that represents the month specified in the <paramref name="time" /> parameter.</returns>
		// Token: 0x06003059 RID: 12377 RVA: 0x000BAFE8 File Offset: 0x000B91E8
		public override int GetMonth(DateTime time)
		{
			this.CheckTicksRange(time.Ticks);
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			this.TimeToLunar(time, ref num, ref num2, ref num3);
			return num2;
		}

		/// <summary>Returns the year in the specified date.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer that represents the year in the specified <see cref="T:System.DateTime" />.</returns>
		// Token: 0x0600305A RID: 12378 RVA: 0x000BB018 File Offset: 0x000B9218
		public override int GetYear(DateTime time)
		{
			this.CheckTicksRange(time.Ticks);
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			this.TimeToLunar(time, ref num, ref num2, ref num3);
			return this.GetYear(num, time);
		}

		/// <summary>Calculates the day of the week in the specified date.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>One of the <see cref="T:System.DayOfWeek" /> values that represents the day of the week specified in the <paramref name="time" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> is less than <see cref="P:System.Globalization.Calendar.MinSupportedDateTime" /> or greater than <see cref="P:System.Globalization.Calendar.MaxSupportedDateTime" />.</exception>
		// Token: 0x0600305B RID: 12379 RVA: 0x000BB04D File Offset: 0x000B924D
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			this.CheckTicksRange(time.Ticks);
			return (DayOfWeek)(time.Ticks / 864000000000L + 1L) % (DayOfWeek)7;
		}

		/// <summary>Calculates the number of months in the specified year and era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="era">An integer that represents the era.</param>
		/// <returns>The number of months in the specified year in the specified era. The return value is 12 months in a common year or 13 months in a leap year.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by this calendar.</exception>
		// Token: 0x0600305C RID: 12380 RVA: 0x000BB073 File Offset: 0x000B9273
		public override int GetMonthsInYear(int year, int era)
		{
			year = this.CheckYearRange(year, era);
			if (!this.InternalIsLeapYear(year))
			{
				return 12;
			}
			return 13;
		}

		/// <summary>Determines whether the specified date in the specified era is a leap day.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="month">An integer from 1 through 13 that represents the month.</param>
		/// <param name="day">An integer from 1 through 31 that represents the day.</param>
		/// <param name="era">An integer that represents the era.</param>
		/// <returns>
		///   <see langword="true" /> if the specified day is a leap day; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, or <paramref name="era" /> is outside the range supported by this calendar.</exception>
		// Token: 0x0600305D RID: 12381 RVA: 0x000BB090 File Offset: 0x000B9290
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			year = this.CheckYearMonthRange(year, month, era);
			int num = this.InternalGetDaysInMonth(year, month);
			if (day < 1 || day > num)
			{
				throw new ArgumentOutOfRangeException("day", Environment.GetResourceString("ArgumentOutOfRange_Day", new object[] { num, month }));
			}
			int yearInfo = this.GetYearInfo(year, 0);
			return yearInfo != 0 && month == yearInfo + 1;
		}

		/// <summary>Determines whether the specified month in the specified year and era is a leap month.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="month">An integer from 1 through 13 that represents the month.</param>
		/// <param name="era">An integer that represents the era.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="month" /> parameter is a leap month; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" />, <paramref name="month" />, or <paramref name="era" /> is outside the range supported by this calendar.</exception>
		// Token: 0x0600305E RID: 12382 RVA: 0x000BB0FC File Offset: 0x000B92FC
		public override bool IsLeapMonth(int year, int month, int era)
		{
			year = this.CheckYearMonthRange(year, month, era);
			int yearInfo = this.GetYearInfo(year, 0);
			return yearInfo != 0 && month == yearInfo + 1;
		}

		/// <summary>Calculates the leap month for the specified year and era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="era">An integer that represents the era.</param>
		/// <returns>A positive integer from 1 through 13 that indicates the leap month in the specified year and era.  
		///  -or-  
		///  Zero if this calendar does not support a leap month, or if the <paramref name="year" /> and <paramref name="era" /> parameters do not specify a leap year.</returns>
		// Token: 0x0600305F RID: 12383 RVA: 0x000BB128 File Offset: 0x000B9328
		public override int GetLeapMonth(int year, int era)
		{
			year = this.CheckYearRange(year, era);
			int yearInfo = this.GetYearInfo(year, 0);
			if (yearInfo > 0)
			{
				return yearInfo + 1;
			}
			return 0;
		}

		// Token: 0x06003060 RID: 12384 RVA: 0x000BB151 File Offset: 0x000B9351
		internal bool InternalIsLeapYear(int year)
		{
			return this.GetYearInfo(year, 0) != 0;
		}

		/// <summary>Determines whether the specified year in the specified era is a leap year.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="era">An integer that represents the era.</param>
		/// <returns>
		///   <see langword="true" /> if the specified year is a leap year; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> or <paramref name="era" /> is outside the range supported by this calendar.</exception>
		// Token: 0x06003061 RID: 12385 RVA: 0x000BB15E File Offset: 0x000B935E
		public override bool IsLeapYear(int year, int era)
		{
			year = this.CheckYearRange(year, era);
			return this.InternalIsLeapYear(year);
		}

		/// <summary>Gets or sets the last year of a 100-year range that can be represented by a 2-digit year.</summary>
		/// <returns>The last year of a 100-year range that can be represented by a 2-digit year.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Globalization.EastAsianLunisolarCalendar" /> is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value in a set operation is less than 99 or greater than the maximum supported year in the current calendar.</exception>
		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06003062 RID: 12386 RVA: 0x000BB171 File Offset: 0x000B9371
		// (set) Token: 0x06003063 RID: 12387 RVA: 0x000BB1A8 File Offset: 0x000B93A8
		public override int TwoDigitYearMax
		{
			get
			{
				if (this.twoDigitYearMax == -1)
				{
					this.twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.BaseCalendarID, this.GetYear(new DateTime(2029, 1, 1)));
				}
				return this.twoDigitYearMax;
			}
			set
			{
				base.VerifyWritable();
				if (value < 99 || value > this.MaxCalendarYear)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[] { 99, this.MaxCalendarYear }));
				}
				this.twoDigitYearMax = value;
			}
		}

		/// <summary>Converts the specified year to a four-digit year.</summary>
		/// <param name="year">A two-digit or four-digit integer that represents the year to convert.</param>
		/// <returns>An integer that contains the four-digit representation of the <paramref name="year" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by this calendar.</exception>
		// Token: 0x06003064 RID: 12388 RVA: 0x000BB203 File Offset: 0x000B9403
		public override int ToFourDigitYear(int year)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			year = base.ToFourDigitYear(year);
			this.CheckYearRange(year, 0);
			return year;
		}

		// Token: 0x04001499 RID: 5273
		internal const int LeapMonth = 0;

		// Token: 0x0400149A RID: 5274
		internal const int Jan1Month = 1;

		// Token: 0x0400149B RID: 5275
		internal const int Jan1Date = 2;

		// Token: 0x0400149C RID: 5276
		internal const int nDaysPerMonth = 3;

		// Token: 0x0400149D RID: 5277
		internal static readonly int[] DaysToMonth365 = new int[]
		{
			0, 31, 59, 90, 120, 151, 181, 212, 243, 273,
			304, 334
		};

		// Token: 0x0400149E RID: 5278
		internal static readonly int[] DaysToMonth366 = new int[]
		{
			0, 31, 60, 91, 121, 152, 182, 213, 244, 274,
			305, 335
		};

		// Token: 0x0400149F RID: 5279
		internal const int DatePartYear = 0;

		// Token: 0x040014A0 RID: 5280
		internal const int DatePartDayOfYear = 1;

		// Token: 0x040014A1 RID: 5281
		internal const int DatePartMonth = 2;

		// Token: 0x040014A2 RID: 5282
		internal const int DatePartDay = 3;

		// Token: 0x040014A3 RID: 5283
		internal const int MaxCalendarMonth = 13;

		// Token: 0x040014A4 RID: 5284
		internal const int MaxCalendarDay = 30;

		// Token: 0x040014A5 RID: 5285
		private const int DEFAULT_GREGORIAN_TWO_DIGIT_YEAR_MAX = 2029;
	}
}
