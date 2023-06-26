using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Globalization
{
	/// <summary>Represents time in divisions, such as weeks, months, and years.</summary>
	// Token: 0x020003A0 RID: 928
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class Calendar : ICloneable
	{
		/// <summary>Gets the earliest date and time supported by this <see cref="T:System.Globalization.Calendar" /> object.</summary>
		/// <returns>The earliest date and time supported by this calendar. The default is <see cref="F:System.DateTime.MinValue" />.</returns>
		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06002DB8 RID: 11704 RVA: 0x000B02EB File Offset: 0x000AE4EB
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public virtual DateTime MinSupportedDateTime
		{
			[__DynamicallyInvokable]
			get
			{
				return DateTime.MinValue;
			}
		}

		/// <summary>Gets the latest date and time supported by this <see cref="T:System.Globalization.Calendar" /> object.</summary>
		/// <returns>The latest date and time supported by this calendar. The default is <see cref="F:System.DateTime.MaxValue" />.</returns>
		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06002DB9 RID: 11705 RVA: 0x000B02F2 File Offset: 0x000AE4F2
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public virtual DateTime MaxSupportedDateTime
		{
			[__DynamicallyInvokable]
			get
			{
				return DateTime.MaxValue;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.Calendar" /> class.</summary>
		// Token: 0x06002DBA RID: 11706 RVA: 0x000B02F9 File Offset: 0x000AE4F9
		[__DynamicallyInvokable]
		protected Calendar()
		{
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06002DBB RID: 11707 RVA: 0x000B030F File Offset: 0x000AE50F
		internal virtual int ID
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06002DBC RID: 11708 RVA: 0x000B0312 File Offset: 0x000AE512
		internal virtual int BaseCalendarID
		{
			get
			{
				return this.ID;
			}
		}

		/// <summary>Gets a value indicating whether the current calendar is solar-based, lunar-based, or a combination of both.</summary>
		/// <returns>One of the <see cref="T:System.Globalization.CalendarAlgorithmType" /> values.</returns>
		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06002DBD RID: 11709 RVA: 0x000B031A File Offset: 0x000AE51A
		[ComVisible(false)]
		public virtual CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.Unknown;
			}
		}

		/// <summary>Gets a value indicating whether this <see cref="T:System.Globalization.Calendar" /> object is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if this <see cref="T:System.Globalization.Calendar" /> object is read-only; otherwise, <see langword="false" />.</returns>
		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06002DBE RID: 11710 RVA: 0x000B031D File Offset: 0x000AE51D
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

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Globalization.Calendar" /> object.</summary>
		/// <returns>A new instance of <see cref="T:System.Object" /> that is the memberwise clone of the current <see cref="T:System.Globalization.Calendar" /> object.</returns>
		// Token: 0x06002DBF RID: 11711 RVA: 0x000B0328 File Offset: 0x000AE528
		[ComVisible(false)]
		public virtual object Clone()
		{
			object obj = base.MemberwiseClone();
			((Calendar)obj).SetReadOnlyState(false);
			return obj;
		}

		/// <summary>Returns a read-only version of the specified <see cref="T:System.Globalization.Calendar" /> object.</summary>
		/// <param name="calendar">A <see cref="T:System.Globalization.Calendar" /> object.</param>
		/// <returns>The <see cref="T:System.Globalization.Calendar" /> object specified by the <paramref name="calendar" /> parameter, if <paramref name="calendar" /> is read-only.  
		///  -or-  
		///  A read-only memberwise clone of the <see cref="T:System.Globalization.Calendar" /> object specified by <paramref name="calendar" />, if <paramref name="calendar" /> is not read-only.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="calendar" /> is <see langword="null" />.</exception>
		// Token: 0x06002DC0 RID: 11712 RVA: 0x000B034C File Offset: 0x000AE54C
		[ComVisible(false)]
		public static Calendar ReadOnly(Calendar calendar)
		{
			if (calendar == null)
			{
				throw new ArgumentNullException("calendar");
			}
			if (calendar.IsReadOnly)
			{
				return calendar;
			}
			Calendar calendar2 = (Calendar)calendar.MemberwiseClone();
			calendar2.SetReadOnlyState(true);
			return calendar2;
		}

		// Token: 0x06002DC1 RID: 11713 RVA: 0x000B0385 File Offset: 0x000AE585
		internal void VerifyWritable()
		{
			if (this.m_isReadOnly)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
			}
		}

		// Token: 0x06002DC2 RID: 11714 RVA: 0x000B039F File Offset: 0x000AE59F
		internal void SetReadOnlyState(bool readOnly)
		{
			this.m_isReadOnly = readOnly;
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06002DC3 RID: 11715 RVA: 0x000B03A8 File Offset: 0x000AE5A8
		internal virtual int CurrentEraValue
		{
			get
			{
				if (this.m_currentEraValue == -1)
				{
					this.m_currentEraValue = CalendarData.GetCalendarData(this.BaseCalendarID).iCurrentEra;
				}
				return this.m_currentEraValue;
			}
		}

		// Token: 0x06002DC4 RID: 11716 RVA: 0x000B03CF File Offset: 0x000AE5CF
		internal static void CheckAddResult(long ticks, DateTime minValue, DateTime maxValue)
		{
			if (ticks < minValue.Ticks || ticks > maxValue.Ticks)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("Argument_ResultCalendarRange"), minValue, maxValue));
			}
		}

		// Token: 0x06002DC5 RID: 11717 RVA: 0x000B040C File Offset: 0x000AE60C
		internal DateTime Add(DateTime time, double value, int scale)
		{
			double num = value * (double)scale + ((value >= 0.0) ? 0.5 : (-0.5));
			if (num <= -315537897600000.0 || num >= 315537897600000.0)
			{
				throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_AddValue"));
			}
			long num2 = (long)num;
			long num3 = time.Ticks + num2 * 10000L;
			Calendar.CheckAddResult(num3, this.MinSupportedDateTime, this.MaxSupportedDateTime);
			return new DateTime(num3);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of milliseconds away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to add milliseconds to.</param>
		/// <param name="milliseconds">The number of milliseconds to add.</param>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of milliseconds to the specified <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range of this calendar.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="milliseconds" /> is outside the supported range of the <see cref="T:System.DateTime" /> return value.</exception>
		// Token: 0x06002DC6 RID: 11718 RVA: 0x000B0498 File Offset: 0x000AE698
		[__DynamicallyInvokable]
		public virtual DateTime AddMilliseconds(DateTime time, double milliseconds)
		{
			return this.Add(time, milliseconds, 1);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of days away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add days.</param>
		/// <param name="days">The number of days to add.</param>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of days to the specified <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range of this calendar.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="days" /> is outside the supported range of the <see cref="T:System.DateTime" /> return value.</exception>
		// Token: 0x06002DC7 RID: 11719 RVA: 0x000B04A3 File Offset: 0x000AE6A3
		[__DynamicallyInvokable]
		public virtual DateTime AddDays(DateTime time, int days)
		{
			return this.Add(time, (double)days, 86400000);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of hours away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add hours.</param>
		/// <param name="hours">The number of hours to add.</param>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of hours to the specified <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range of this calendar.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="hours" /> is outside the supported range of the <see cref="T:System.DateTime" /> return value.</exception>
		// Token: 0x06002DC8 RID: 11720 RVA: 0x000B04B3 File Offset: 0x000AE6B3
		[__DynamicallyInvokable]
		public virtual DateTime AddHours(DateTime time, int hours)
		{
			return this.Add(time, (double)hours, 3600000);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of minutes away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add minutes.</param>
		/// <param name="minutes">The number of minutes to add.</param>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of minutes to the specified <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range of this calendar.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="minutes" /> is outside the supported range of the <see cref="T:System.DateTime" /> return value.</exception>
		// Token: 0x06002DC9 RID: 11721 RVA: 0x000B04C3 File Offset: 0x000AE6C3
		[__DynamicallyInvokable]
		public virtual DateTime AddMinutes(DateTime time, int minutes)
		{
			return this.Add(time, (double)minutes, 60000);
		}

		/// <summary>When overridden in a derived class, returns a <see cref="T:System.DateTime" /> that is the specified number of months away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add months.</param>
		/// <param name="months">The number of months to add.</param>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of months to the specified <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range of this calendar.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="months" /> is outside the supported range of the <see cref="T:System.DateTime" /> return value.</exception>
		// Token: 0x06002DCA RID: 11722
		[__DynamicallyInvokable]
		public abstract DateTime AddMonths(DateTime time, int months);

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of seconds away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add seconds.</param>
		/// <param name="seconds">The number of seconds to add.</param>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of seconds to the specified <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range of this calendar.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="seconds" /> is outside the supported range of the <see cref="T:System.DateTime" /> return value.</exception>
		// Token: 0x06002DCB RID: 11723 RVA: 0x000B04D3 File Offset: 0x000AE6D3
		[__DynamicallyInvokable]
		public virtual DateTime AddSeconds(DateTime time, int seconds)
		{
			return this.Add(time, (double)seconds, 1000);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is the specified number of weeks away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add weeks.</param>
		/// <param name="weeks">The number of weeks to add.</param>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of weeks to the specified <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range of this calendar.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="weeks" /> is outside the supported range of the <see cref="T:System.DateTime" /> return value.</exception>
		// Token: 0x06002DCC RID: 11724 RVA: 0x000B04E3 File Offset: 0x000AE6E3
		[__DynamicallyInvokable]
		public virtual DateTime AddWeeks(DateTime time, int weeks)
		{
			return this.AddDays(time, weeks * 7);
		}

		/// <summary>When overridden in a derived class, returns a <see cref="T:System.DateTime" /> that is the specified number of years away from the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to which to add years.</param>
		/// <param name="years">The number of years to add.</param>
		/// <returns>The <see cref="T:System.DateTime" /> that results from adding the specified number of years to the specified <see cref="T:System.DateTime" />.</returns>
		/// <exception cref="T:System.ArgumentException">The resulting <see cref="T:System.DateTime" /> is outside the supported range of this calendar.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="years" /> is outside the supported range of the <see cref="T:System.DateTime" /> return value.</exception>
		// Token: 0x06002DCD RID: 11725
		[__DynamicallyInvokable]
		public abstract DateTime AddYears(DateTime time, int years);

		/// <summary>When overridden in a derived class, returns the day of the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>A positive integer that represents the day of the month in the <paramref name="time" /> parameter.</returns>
		// Token: 0x06002DCE RID: 11726
		[__DynamicallyInvokable]
		public abstract int GetDayOfMonth(DateTime time);

		/// <summary>When overridden in a derived class, returns the day of the week in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>A <see cref="T:System.DayOfWeek" /> value that represents the day of the week in the <paramref name="time" /> parameter.</returns>
		// Token: 0x06002DCF RID: 11727
		[__DynamicallyInvokable]
		public abstract DayOfWeek GetDayOfWeek(DateTime time);

		/// <summary>When overridden in a derived class, returns the day of the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>A positive integer that represents the day of the year in the <paramref name="time" /> parameter.</returns>
		// Token: 0x06002DD0 RID: 11728
		[__DynamicallyInvokable]
		public abstract int GetDayOfYear(DateTime time);

		/// <summary>Returns the number of days in the specified month and year of the current era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="month">A positive integer that represents the month.</param>
		/// <returns>The number of days in the specified month in the specified year in the current era.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="month" /> is outside the range supported by the calendar.</exception>
		// Token: 0x06002DD1 RID: 11729 RVA: 0x000B04EF File Offset: 0x000AE6EF
		[__DynamicallyInvokable]
		public virtual int GetDaysInMonth(int year, int month)
		{
			return this.GetDaysInMonth(year, month, 0);
		}

		/// <summary>When overridden in a derived class, returns the number of days in the specified month, year, and era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="month">A positive integer that represents the month.</param>
		/// <param name="era">An integer that represents the era.</param>
		/// <returns>The number of days in the specified month in the specified year in the specified era.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="month" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="era" /> is outside the range supported by the calendar.</exception>
		// Token: 0x06002DD2 RID: 11730
		[__DynamicallyInvokable]
		public abstract int GetDaysInMonth(int year, int month, int era);

		/// <summary>Returns the number of days in the specified year of the current era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <returns>The number of days in the specified year in the current era.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.</exception>
		// Token: 0x06002DD3 RID: 11731 RVA: 0x000B04FA File Offset: 0x000AE6FA
		[__DynamicallyInvokable]
		public virtual int GetDaysInYear(int year)
		{
			return this.GetDaysInYear(year, 0);
		}

		/// <summary>When overridden in a derived class, returns the number of days in the specified year and era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="era">An integer that represents the era.</param>
		/// <returns>The number of days in the specified year in the specified era.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="era" /> is outside the range supported by the calendar.</exception>
		// Token: 0x06002DD4 RID: 11732
		[__DynamicallyInvokable]
		public abstract int GetDaysInYear(int year, int era);

		/// <summary>When overridden in a derived class, returns the era in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer that represents the era in <paramref name="time" />.</returns>
		// Token: 0x06002DD5 RID: 11733
		[__DynamicallyInvokable]
		public abstract int GetEra(DateTime time);

		/// <summary>When overridden in a derived class, gets the list of eras in the current calendar.</summary>
		/// <returns>An array of integers that represents the eras in the current calendar.</returns>
		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06002DD6 RID: 11734
		[__DynamicallyInvokable]
		public abstract int[] Eras
		{
			[__DynamicallyInvokable]
			get;
		}

		/// <summary>Returns the hours value in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer from 0 to 23 that represents the hour in <paramref name="time" />.</returns>
		// Token: 0x06002DD7 RID: 11735 RVA: 0x000B0504 File Offset: 0x000AE704
		[__DynamicallyInvokable]
		public virtual int GetHour(DateTime time)
		{
			return (int)(time.Ticks / 36000000000L % 24L);
		}

		/// <summary>Returns the milliseconds value in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>A double-precision floating-point number from 0 to 999 that represents the milliseconds in the <paramref name="time" /> parameter.</returns>
		// Token: 0x06002DD8 RID: 11736 RVA: 0x000B051C File Offset: 0x000AE71C
		[__DynamicallyInvokable]
		public virtual double GetMilliseconds(DateTime time)
		{
			return (double)(time.Ticks / 10000L % 1000L);
		}

		/// <summary>Returns the minutes value in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer from 0 to 59 that represents the minutes in <paramref name="time" />.</returns>
		// Token: 0x06002DD9 RID: 11737 RVA: 0x000B0534 File Offset: 0x000AE734
		[__DynamicallyInvokable]
		public virtual int GetMinute(DateTime time)
		{
			return (int)(time.Ticks / 600000000L % 60L);
		}

		/// <summary>When overridden in a derived class, returns the month in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>A positive integer that represents the month in <paramref name="time" />.</returns>
		// Token: 0x06002DDA RID: 11738
		[__DynamicallyInvokable]
		public abstract int GetMonth(DateTime time);

		/// <summary>Returns the number of months in the specified year in the current era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <returns>The number of months in the specified year in the current era.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.</exception>
		// Token: 0x06002DDB RID: 11739 RVA: 0x000B0549 File Offset: 0x000AE749
		[__DynamicallyInvokable]
		public virtual int GetMonthsInYear(int year)
		{
			return this.GetMonthsInYear(year, 0);
		}

		/// <summary>When overridden in a derived class, returns the number of months in the specified year in the specified era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="era">An integer that represents the era.</param>
		/// <returns>The number of months in the specified year in the specified era.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="era" /> is outside the range supported by the calendar.</exception>
		// Token: 0x06002DDC RID: 11740
		[__DynamicallyInvokable]
		public abstract int GetMonthsInYear(int year, int era);

		/// <summary>Returns the seconds value in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer from 0 to 59 that represents the seconds in <paramref name="time" />.</returns>
		// Token: 0x06002DDD RID: 11741 RVA: 0x000B0553 File Offset: 0x000AE753
		[__DynamicallyInvokable]
		public virtual int GetSecond(DateTime time)
		{
			return (int)(time.Ticks / 10000000L % 60L);
		}

		// Token: 0x06002DDE RID: 11742 RVA: 0x000B0568 File Offset: 0x000AE768
		internal int GetFirstDayWeekOfYear(DateTime time, int firstDayOfWeek)
		{
			int num = this.GetDayOfYear(time) - 1;
			int num2 = this.GetDayOfWeek(time) - (DayOfWeek)(num % 7);
			int num3 = (num2 - firstDayOfWeek + 14) % 7;
			return (num + num3) / 7 + 1;
		}

		// Token: 0x06002DDF RID: 11743 RVA: 0x000B059C File Offset: 0x000AE79C
		private int GetWeekOfYearFullDays(DateTime time, int firstDayOfWeek, int fullDays)
		{
			int num = this.GetDayOfYear(time) - 1;
			int num2 = this.GetDayOfWeek(time) - (DayOfWeek)(num % 7);
			int num3 = (firstDayOfWeek - num2 + 14) % 7;
			if (num3 != 0 && num3 >= fullDays)
			{
				num3 -= 7;
			}
			int num4 = num - num3;
			if (num4 >= 0)
			{
				return num4 / 7 + 1;
			}
			if (time <= this.MinSupportedDateTime.AddDays((double)num))
			{
				return this.GetWeekOfYearOfMinSupportedDateTime(firstDayOfWeek, fullDays);
			}
			return this.GetWeekOfYearFullDays(time.AddDays((double)(-(double)(num + 1))), firstDayOfWeek, fullDays);
		}

		// Token: 0x06002DE0 RID: 11744 RVA: 0x000B0618 File Offset: 0x000AE818
		private int GetWeekOfYearOfMinSupportedDateTime(int firstDayOfWeek, int minimumDaysInFirstWeek)
		{
			int num = this.GetDayOfYear(this.MinSupportedDateTime) - 1;
			int num2 = this.GetDayOfWeek(this.MinSupportedDateTime) - (DayOfWeek)(num % 7);
			int num3 = (firstDayOfWeek + 7 - num2) % 7;
			if (num3 == 0 || num3 >= minimumDaysInFirstWeek)
			{
				return 1;
			}
			int num4 = this.DaysInYearBeforeMinSupportedYear - 1;
			int num5 = num2 - 1 - num4 % 7;
			int num6 = (firstDayOfWeek - num5 + 14) % 7;
			int num7 = num4 - num6;
			if (num6 >= minimumDaysInFirstWeek)
			{
				num7 += 7;
			}
			return num7 / 7 + 1;
		}

		/// <summary>Gets the number of days in the year that precedes the year that is specified by the <see cref="P:System.Globalization.Calendar.MinSupportedDateTime" /> property.</summary>
		/// <returns>The number of days in the year that precedes the year specified by <see cref="P:System.Globalization.Calendar.MinSupportedDateTime" />.</returns>
		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06002DE1 RID: 11745 RVA: 0x000B068A File Offset: 0x000AE88A
		protected virtual int DaysInYearBeforeMinSupportedYear
		{
			get
			{
				return 365;
			}
		}

		/// <summary>Returns the week of the year that includes the date in the specified <see cref="T:System.DateTime" /> value.</summary>
		/// <param name="time">A date and time value.</param>
		/// <param name="rule">An enumeration value that defines a calendar week.</param>
		/// <param name="firstDayOfWeek">An enumeration value that represents the first day of the week.</param>
		/// <returns>A positive integer that represents the week of the year that includes the date in the <paramref name="time" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="time" /> is earlier than <see cref="P:System.Globalization.Calendar.MinSupportedDateTime" /> or later than <see cref="P:System.Globalization.Calendar.MaxSupportedDateTime" />.  
		/// -or-  
		/// <paramref name="firstDayOfWeek" /> is not a valid <see cref="T:System.DayOfWeek" /> value.  
		/// -or-  
		/// <paramref name="rule" /> is not a valid <see cref="T:System.Globalization.CalendarWeekRule" /> value.</exception>
		// Token: 0x06002DE2 RID: 11746 RVA: 0x000B0694 File Offset: 0x000AE894
		[__DynamicallyInvokable]
		public virtual int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
		{
			if (firstDayOfWeek < DayOfWeek.Sunday || firstDayOfWeek > DayOfWeek.Saturday)
			{
				throw new ArgumentOutOfRangeException("firstDayOfWeek", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[]
				{
					DayOfWeek.Sunday,
					DayOfWeek.Saturday
				}));
			}
			switch (rule)
			{
			case CalendarWeekRule.FirstDay:
				return this.GetFirstDayWeekOfYear(time, (int)firstDayOfWeek);
			case CalendarWeekRule.FirstFullWeek:
				return this.GetWeekOfYearFullDays(time, (int)firstDayOfWeek, 7);
			case CalendarWeekRule.FirstFourDayWeek:
				return this.GetWeekOfYearFullDays(time, (int)firstDayOfWeek, 4);
			default:
				throw new ArgumentOutOfRangeException("rule", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[]
				{
					CalendarWeekRule.FirstDay,
					CalendarWeekRule.FirstFourDayWeek
				}));
			}
		}

		/// <summary>When overridden in a derived class, returns the year in the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer that represents the year in <paramref name="time" />.</returns>
		// Token: 0x06002DE3 RID: 11747
		[__DynamicallyInvokable]
		public abstract int GetYear(DateTime time);

		/// <summary>Determines whether the specified date in the current era is a leap day.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="month">A positive integer that represents the month.</param>
		/// <param name="day">A positive integer that represents the day.</param>
		/// <returns>
		///   <see langword="true" /> if the specified day is a leap day; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="month" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="day" /> is outside the range supported by the calendar.</exception>
		// Token: 0x06002DE4 RID: 11748 RVA: 0x000B0733 File Offset: 0x000AE933
		[__DynamicallyInvokable]
		public virtual bool IsLeapDay(int year, int month, int day)
		{
			return this.IsLeapDay(year, month, day, 0);
		}

		/// <summary>When overridden in a derived class, determines whether the specified date in the specified era is a leap day.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="month">A positive integer that represents the month.</param>
		/// <param name="day">A positive integer that represents the day.</param>
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
		// Token: 0x06002DE5 RID: 11749
		[__DynamicallyInvokable]
		public abstract bool IsLeapDay(int year, int month, int day, int era);

		/// <summary>Determines whether the specified month in the specified year in the current era is a leap month.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="month">A positive integer that represents the month.</param>
		/// <returns>
		///   <see langword="true" /> if the specified month is a leap month; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="month" /> is outside the range supported by the calendar.</exception>
		// Token: 0x06002DE6 RID: 11750 RVA: 0x000B073F File Offset: 0x000AE93F
		[__DynamicallyInvokable]
		public virtual bool IsLeapMonth(int year, int month)
		{
			return this.IsLeapMonth(year, month, 0);
		}

		/// <summary>When overridden in a derived class, determines whether the specified month in the specified year in the specified era is a leap month.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="month">A positive integer that represents the month.</param>
		/// <param name="era">An integer that represents the era.</param>
		/// <returns>
		///   <see langword="true" /> if the specified month is a leap month; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="month" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="era" /> is outside the range supported by the calendar.</exception>
		// Token: 0x06002DE7 RID: 11751
		[__DynamicallyInvokable]
		public abstract bool IsLeapMonth(int year, int month, int era);

		/// <summary>Calculates the leap month for a specified year.</summary>
		/// <param name="year">A year.</param>
		/// <returns>A positive integer that indicates the leap month in the specified year.  
		///  -or-  
		///  Zero if this calendar does not support a leap month or if the <paramref name="year" /> parameter does not represent a leap year.</returns>
		// Token: 0x06002DE8 RID: 11752 RVA: 0x000B074A File Offset: 0x000AE94A
		[ComVisible(false)]
		public virtual int GetLeapMonth(int year)
		{
			return this.GetLeapMonth(year, 0);
		}

		/// <summary>Calculates the leap month for a specified year and era.</summary>
		/// <param name="year">A year.</param>
		/// <param name="era">An era.</param>
		/// <returns>A positive integer that indicates the leap month in the specified year and era.  
		///  -or-  
		///  Zero if this calendar does not support a leap month or if the <paramref name="year" /> and <paramref name="era" /> parameters do not specify a leap year.</returns>
		// Token: 0x06002DE9 RID: 11753 RVA: 0x000B0754 File Offset: 0x000AE954
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public virtual int GetLeapMonth(int year, int era)
		{
			if (!this.IsLeapYear(year, era))
			{
				return 0;
			}
			int monthsInYear = this.GetMonthsInYear(year, era);
			for (int i = 1; i <= monthsInYear; i++)
			{
				if (this.IsLeapMonth(year, i, era))
				{
					return i;
				}
			}
			return 0;
		}

		/// <summary>Determines whether the specified year in the current era is a leap year.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <returns>
		///   <see langword="true" /> if the specified year is a leap year; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.</exception>
		// Token: 0x06002DEA RID: 11754 RVA: 0x000B0790 File Offset: 0x000AE990
		[__DynamicallyInvokable]
		public virtual bool IsLeapYear(int year)
		{
			return this.IsLeapYear(year, 0);
		}

		/// <summary>When overridden in a derived class, determines whether the specified year in the specified era is a leap year.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="era">An integer that represents the era.</param>
		/// <returns>
		///   <see langword="true" /> if the specified year is a leap year; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.  
		/// -or-  
		/// <paramref name="era" /> is outside the range supported by the calendar.</exception>
		// Token: 0x06002DEB RID: 11755
		[__DynamicallyInvokable]
		public abstract bool IsLeapYear(int year, int era);

		/// <summary>Returns a <see cref="T:System.DateTime" /> that is set to the specified date and time in the current era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="month">A positive integer that represents the month.</param>
		/// <param name="day">A positive integer that represents the day.</param>
		/// <param name="hour">An integer from 0 to 23 that represents the hour.</param>
		/// <param name="minute">An integer from 0 to 59 that represents the minute.</param>
		/// <param name="second">An integer from 0 to 59 that represents the second.</param>
		/// <param name="millisecond">An integer from 0 to 999 that represents the millisecond.</param>
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
		/// <paramref name="millisecond" /> is less than zero or greater than 999.</exception>
		// Token: 0x06002DEC RID: 11756 RVA: 0x000B079C File Offset: 0x000AE99C
		[__DynamicallyInvokable]
		public virtual DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
		{
			return this.ToDateTime(year, month, day, hour, minute, second, millisecond, 0);
		}

		/// <summary>When overridden in a derived class, returns a <see cref="T:System.DateTime" /> that is set to the specified date and time in the specified era.</summary>
		/// <param name="year">An integer that represents the year.</param>
		/// <param name="month">A positive integer that represents the month.</param>
		/// <param name="day">A positive integer that represents the day.</param>
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
		// Token: 0x06002DED RID: 11757
		[__DynamicallyInvokable]
		public abstract DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era);

		// Token: 0x06002DEE RID: 11758 RVA: 0x000B07BC File Offset: 0x000AE9BC
		internal virtual bool TryToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era, out DateTime result)
		{
			result = DateTime.MinValue;
			bool flag;
			try
			{
				result = this.ToDateTime(year, month, day, hour, minute, second, millisecond, era);
				flag = true;
			}
			catch (ArgumentException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06002DEF RID: 11759 RVA: 0x000B080C File Offset: 0x000AEA0C
		internal virtual bool IsValidYear(int year, int era)
		{
			return year >= this.GetYear(this.MinSupportedDateTime) && year <= this.GetYear(this.MaxSupportedDateTime);
		}

		// Token: 0x06002DF0 RID: 11760 RVA: 0x000B0831 File Offset: 0x000AEA31
		internal virtual bool IsValidMonth(int year, int month, int era)
		{
			return this.IsValidYear(year, era) && month >= 1 && month <= this.GetMonthsInYear(year, era);
		}

		// Token: 0x06002DF1 RID: 11761 RVA: 0x000B0851 File Offset: 0x000AEA51
		internal virtual bool IsValidDay(int year, int month, int day, int era)
		{
			return this.IsValidMonth(year, month, era) && day >= 1 && day <= this.GetDaysInMonth(year, month, era);
		}

		/// <summary>Gets or sets the last year of a 100-year range that can be represented by a 2-digit year.</summary>
		/// <returns>The last year of a 100-year range that can be represented by a 2-digit year.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Globalization.Calendar" /> object is read-only.</exception>
		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06002DF2 RID: 11762 RVA: 0x000B0875 File Offset: 0x000AEA75
		// (set) Token: 0x06002DF3 RID: 11763 RVA: 0x000B087D File Offset: 0x000AEA7D
		[__DynamicallyInvokable]
		public virtual int TwoDigitYearMax
		{
			[__DynamicallyInvokable]
			get
			{
				return this.twoDigitYearMax;
			}
			[__DynamicallyInvokable]
			set
			{
				this.VerifyWritable();
				this.twoDigitYearMax = value;
			}
		}

		/// <summary>Converts the specified year to a four-digit year by using the <see cref="P:System.Globalization.Calendar.TwoDigitYearMax" /> property to determine the appropriate century.</summary>
		/// <param name="year">A two-digit or four-digit integer that represents the year to convert.</param>
		/// <returns>An integer that contains the four-digit representation of <paramref name="year" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is outside the range supported by the calendar.</exception>
		// Token: 0x06002DF4 RID: 11764 RVA: 0x000B088C File Offset: 0x000AEA8C
		[__DynamicallyInvokable]
		public virtual int ToFourDigitYear(int year)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (year < 100)
			{
				return (this.TwoDigitYearMax / 100 - ((year > this.TwoDigitYearMax % 100) ? 1 : 0)) * 100 + year;
			}
			return year;
		}

		// Token: 0x06002DF5 RID: 11765 RVA: 0x000B08D8 File Offset: 0x000AEAD8
		internal static long TimeToTicks(int hour, int minute, int second, int millisecond)
		{
			if (hour < 0 || hour >= 24 || minute < 0 || minute >= 60 || second < 0 || second >= 60)
			{
				throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("ArgumentOutOfRange_BadHourMinuteSecond"));
			}
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 0, 999));
			}
			return TimeSpan.TimeToTicks(hour, minute, second) + (long)millisecond * 10000L;
		}

		// Token: 0x06002DF6 RID: 11766 RVA: 0x000B0960 File Offset: 0x000AEB60
		[SecuritySafeCritical]
		internal static int GetSystemTwoDigitYearSetting(int CalID, int defaultYearValue)
		{
			int num = CalendarData.nativeGetTwoDigitYearMax(CalID);
			if (num < 0)
			{
				num = defaultYearValue;
			}
			return num;
		}

		// Token: 0x040012BF RID: 4799
		internal const long TicksPerMillisecond = 10000L;

		// Token: 0x040012C0 RID: 4800
		internal const long TicksPerSecond = 10000000L;

		// Token: 0x040012C1 RID: 4801
		internal const long TicksPerMinute = 600000000L;

		// Token: 0x040012C2 RID: 4802
		internal const long TicksPerHour = 36000000000L;

		// Token: 0x040012C3 RID: 4803
		internal const long TicksPerDay = 864000000000L;

		// Token: 0x040012C4 RID: 4804
		internal const int MillisPerSecond = 1000;

		// Token: 0x040012C5 RID: 4805
		internal const int MillisPerMinute = 60000;

		// Token: 0x040012C6 RID: 4806
		internal const int MillisPerHour = 3600000;

		// Token: 0x040012C7 RID: 4807
		internal const int MillisPerDay = 86400000;

		// Token: 0x040012C8 RID: 4808
		internal const int DaysPerYear = 365;

		// Token: 0x040012C9 RID: 4809
		internal const int DaysPer4Years = 1461;

		// Token: 0x040012CA RID: 4810
		internal const int DaysPer100Years = 36524;

		// Token: 0x040012CB RID: 4811
		internal const int DaysPer400Years = 146097;

		// Token: 0x040012CC RID: 4812
		internal const int DaysTo10000 = 3652059;

		// Token: 0x040012CD RID: 4813
		internal const long MaxMillis = 315537897600000L;

		// Token: 0x040012CE RID: 4814
		internal const int CAL_GREGORIAN = 1;

		// Token: 0x040012CF RID: 4815
		internal const int CAL_GREGORIAN_US = 2;

		// Token: 0x040012D0 RID: 4816
		internal const int CAL_JAPAN = 3;

		// Token: 0x040012D1 RID: 4817
		internal const int CAL_TAIWAN = 4;

		// Token: 0x040012D2 RID: 4818
		internal const int CAL_KOREA = 5;

		// Token: 0x040012D3 RID: 4819
		internal const int CAL_HIJRI = 6;

		// Token: 0x040012D4 RID: 4820
		internal const int CAL_THAI = 7;

		// Token: 0x040012D5 RID: 4821
		internal const int CAL_HEBREW = 8;

		// Token: 0x040012D6 RID: 4822
		internal const int CAL_GREGORIAN_ME_FRENCH = 9;

		// Token: 0x040012D7 RID: 4823
		internal const int CAL_GREGORIAN_ARABIC = 10;

		// Token: 0x040012D8 RID: 4824
		internal const int CAL_GREGORIAN_XLIT_ENGLISH = 11;

		// Token: 0x040012D9 RID: 4825
		internal const int CAL_GREGORIAN_XLIT_FRENCH = 12;

		// Token: 0x040012DA RID: 4826
		internal const int CAL_JULIAN = 13;

		// Token: 0x040012DB RID: 4827
		internal const int CAL_JAPANESELUNISOLAR = 14;

		// Token: 0x040012DC RID: 4828
		internal const int CAL_CHINESELUNISOLAR = 15;

		// Token: 0x040012DD RID: 4829
		internal const int CAL_SAKA = 16;

		// Token: 0x040012DE RID: 4830
		internal const int CAL_LUNAR_ETO_CHN = 17;

		// Token: 0x040012DF RID: 4831
		internal const int CAL_LUNAR_ETO_KOR = 18;

		// Token: 0x040012E0 RID: 4832
		internal const int CAL_LUNAR_ETO_ROKUYOU = 19;

		// Token: 0x040012E1 RID: 4833
		internal const int CAL_KOREANLUNISOLAR = 20;

		// Token: 0x040012E2 RID: 4834
		internal const int CAL_TAIWANLUNISOLAR = 21;

		// Token: 0x040012E3 RID: 4835
		internal const int CAL_PERSIAN = 22;

		// Token: 0x040012E4 RID: 4836
		internal const int CAL_UMALQURA = 23;

		// Token: 0x040012E5 RID: 4837
		internal int m_currentEraValue = -1;

		// Token: 0x040012E6 RID: 4838
		[OptionalField(VersionAdded = 2)]
		private bool m_isReadOnly;

		/// <summary>Represents the current era of the current calendar. The value of this field is 0.</summary>
		// Token: 0x040012E7 RID: 4839
		[__DynamicallyInvokable]
		public const int CurrentEra = 0;

		// Token: 0x040012E8 RID: 4840
		internal int twoDigitYearMax = -1;
	}
}
