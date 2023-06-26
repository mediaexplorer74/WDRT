using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>Represents an instant in time, typically expressed as a date and time of day.</summary>
	// Token: 0x0200007D RID: 125
	[__DynamicallyInvokable]
	[Serializable]
	[StructLayout(LayoutKind.Auto)]
	public struct DateTime : IComparable, IFormattable, IConvertible, ISerializable, IComparable<DateTime>, IEquatable<DateTime>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to a specified number of ticks.</summary>
		/// <param name="ticks">A date and time expressed in the number of 100-nanosecond intervals that have elapsed since January 1, 0001 at 00:00:00.000 in the Gregorian calendar.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="ticks" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />.</exception>
		// Token: 0x060005E3 RID: 1507 RVA: 0x00014CC9 File Offset: 0x00012EC9
		[__DynamicallyInvokable]
		public DateTime(long ticks)
		{
			if (ticks < 0L || ticks > 3155378975999999999L)
			{
				throw new ArgumentOutOfRangeException("ticks", Environment.GetResourceString("ArgumentOutOfRange_DateTimeBadTicks"));
			}
			this.dateData = (ulong)ticks;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00014CF8 File Offset: 0x00012EF8
		private DateTime(ulong dateData)
		{
			this.dateData = dateData;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to a specified number of ticks and to Coordinated Universal Time (UTC) or local time.</summary>
		/// <param name="ticks">A date and time expressed in the number of 100-nanosecond intervals that have elapsed since January 1, 0001 at 00:00:00.000 in the Gregorian calendar.</param>
		/// <param name="kind">One of the enumeration values that indicates whether <paramref name="ticks" /> specifies a local time, Coordinated Universal Time (UTC), or neither.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="ticks" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="kind" /> is not one of the <see cref="T:System.DateTimeKind" /> values.</exception>
		// Token: 0x060005E5 RID: 1509 RVA: 0x00014D04 File Offset: 0x00012F04
		[__DynamicallyInvokable]
		public DateTime(long ticks, DateTimeKind kind)
		{
			if (ticks < 0L || ticks > 3155378975999999999L)
			{
				throw new ArgumentOutOfRangeException("ticks", Environment.GetResourceString("ArgumentOutOfRange_DateTimeBadTicks"));
			}
			if (kind < DateTimeKind.Unspecified || kind > DateTimeKind.Local)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDateTimeKind"), "kind");
			}
			this.dateData = (ulong)(ticks | ((long)kind << 62));
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00014D64 File Offset: 0x00012F64
		internal DateTime(long ticks, DateTimeKind kind, bool isAmbiguousDst)
		{
			if (ticks < 0L || ticks > 3155378975999999999L)
			{
				throw new ArgumentOutOfRangeException("ticks", Environment.GetResourceString("ArgumentOutOfRange_DateTimeBadTicks"));
			}
			this.dateData = (ulong)(ticks | (isAmbiguousDst ? (-4611686018427387904L) : long.MinValue));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to the specified year, month, and day.</summary>
		/// <param name="year">The year (1 through 9999).</param>
		/// <param name="month">The month (1 through 12).</param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />).</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than 1 or greater than 9999.  
		/// -or-  
		/// <paramref name="month" /> is less than 1 or greater than 12.  
		/// -or-  
		/// <paramref name="day" /> is less than 1 or greater than the number of days in <paramref name="month" />.</exception>
		// Token: 0x060005E7 RID: 1511 RVA: 0x00014DB6 File Offset: 0x00012FB6
		[__DynamicallyInvokable]
		public DateTime(int year, int month, int day)
		{
			this.dateData = (ulong)DateTime.DateToTicks(year, month, day);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to the specified year, month, and day for the specified calendar.</summary>
		/// <param name="year">The year (1 through the number of years in <paramref name="calendar" />).</param>
		/// <param name="month">The month (1 through the number of months in <paramref name="calendar" />).</param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />).</param>
		/// <param name="calendar">The calendar that is used to interpret <paramref name="year" />, <paramref name="month" />, and <paramref name="day" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="calendar" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is not in the range supported by <paramref name="calendar" />.  
		/// -or-  
		/// <paramref name="month" /> is less than 1 or greater than the number of months in <paramref name="calendar" />.  
		/// -or-  
		/// <paramref name="day" /> is less than 1 or greater than the number of days in <paramref name="month" />.</exception>
		// Token: 0x060005E8 RID: 1512 RVA: 0x00014DC6 File Offset: 0x00012FC6
		public DateTime(int year, int month, int day, Calendar calendar)
		{
			this = new DateTime(year, month, day, 0, 0, 0, calendar);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to the specified year, month, day, hour, minute, and second.</summary>
		/// <param name="year">The year (1 through 9999).</param>
		/// <param name="month">The month (1 through 12).</param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />).</param>
		/// <param name="hour">The hours (0 through 23).</param>
		/// <param name="minute">The minutes (0 through 59).</param>
		/// <param name="second">The seconds (0 through 59).</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than 1 or greater than 9999.  
		/// -or-  
		/// <paramref name="month" /> is less than 1 or greater than 12.  
		/// -or-  
		/// <paramref name="day" /> is less than 1 or greater than the number of days in <paramref name="month" />.  
		/// -or-  
		/// <paramref name="hour" /> is less than 0 or greater than 23.  
		/// -or-  
		/// <paramref name="minute" /> is less than 0 or greater than 59.  
		/// -or-  
		/// <paramref name="second" /> is less than 0 or greater than 59.</exception>
		// Token: 0x060005E9 RID: 1513 RVA: 0x00014DD6 File Offset: 0x00012FD6
		[__DynamicallyInvokable]
		public DateTime(int year, int month, int day, int hour, int minute, int second)
		{
			if (second == 60 && DateTime.s_isLeapSecondsSupportedSystem && DateTime.IsValidTimeWithLeapSeconds(year, month, day, hour, minute, second, DateTimeKind.Unspecified))
			{
				second = 59;
			}
			this.dateData = (ulong)(DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to the specified year, month, day, hour, minute, second, and Coordinated Universal Time (UTC) or local time.</summary>
		/// <param name="year">The year (1 through 9999).</param>
		/// <param name="month">The month (1 through 12).</param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />).</param>
		/// <param name="hour">The hours (0 through 23).</param>
		/// <param name="minute">The minutes (0 through 59).</param>
		/// <param name="second">The seconds (0 through 59).</param>
		/// <param name="kind">One of the enumeration values that indicates whether <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, <paramref name="hour" />, <paramref name="minute" /> and <paramref name="second" /> specify a local time, Coordinated Universal Time (UTC), or neither.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than 1 or greater than 9999.  
		/// -or-  
		/// <paramref name="month" /> is less than 1 or greater than 12.  
		/// -or-  
		/// <paramref name="day" /> is less than 1 or greater than the number of days in <paramref name="month" />.  
		/// -or-  
		/// <paramref name="hour" /> is less than 0 or greater than 23.  
		/// -or-  
		/// <paramref name="minute" /> is less than 0 or greater than 59.  
		/// -or-  
		/// <paramref name="second" /> is less than 0 or greater than 59.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="kind" /> is not one of the <see cref="T:System.DateTimeKind" /> values.</exception>
		// Token: 0x060005EA RID: 1514 RVA: 0x00014E14 File Offset: 0x00013014
		[__DynamicallyInvokable]
		public DateTime(int year, int month, int day, int hour, int minute, int second, DateTimeKind kind)
		{
			if (kind < DateTimeKind.Unspecified || kind > DateTimeKind.Local)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDateTimeKind"), "kind");
			}
			if (second == 60 && DateTime.s_isLeapSecondsSupportedSystem && DateTime.IsValidTimeWithLeapSeconds(year, month, day, hour, minute, second, kind))
			{
				second = 59;
			}
			long num = DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second);
			this.dateData = (ulong)(num | ((long)kind << 62));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to the specified year, month, day, hour, minute, and second for the specified calendar.</summary>
		/// <param name="year">The year (1 through the number of years in <paramref name="calendar" />).</param>
		/// <param name="month">The month (1 through the number of months in <paramref name="calendar" />).</param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />).</param>
		/// <param name="hour">The hours (0 through 23).</param>
		/// <param name="minute">The minutes (0 through 59).</param>
		/// <param name="second">The seconds (0 through 59).</param>
		/// <param name="calendar">The calendar that is used to interpret <paramref name="year" />, <paramref name="month" />, and <paramref name="day" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="calendar" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is not in the range supported by <paramref name="calendar" />.  
		/// -or-  
		/// <paramref name="month" /> is less than 1 or greater than the number of months in <paramref name="calendar" />.  
		/// -or-  
		/// <paramref name="day" /> is less than 1 or greater than the number of days in <paramref name="month" />.  
		/// -or-  
		/// <paramref name="hour" /> is less than 0 or greater than 23  
		/// -or-  
		/// <paramref name="minute" /> is less than 0 or greater than 59.  
		/// -or-  
		/// <paramref name="second" /> is less than 0 or greater than 59.</exception>
		// Token: 0x060005EB RID: 1515 RVA: 0x00014E88 File Offset: 0x00013088
		public DateTime(int year, int month, int day, int hour, int minute, int second, Calendar calendar)
		{
			if (calendar == null)
			{
				throw new ArgumentNullException("calendar");
			}
			int num = second;
			if (second == 60 && DateTime.s_isLeapSecondsSupportedSystem)
			{
				second = 59;
			}
			this.dateData = (ulong)calendar.ToDateTime(year, month, day, hour, minute, second, 0).Ticks;
			if (num == 60)
			{
				DateTime dateTime = new DateTime(this.dateData);
				if (!DateTime.IsValidTimeWithLeapSeconds(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 60, DateTimeKind.Unspecified))
				{
					throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("ArgumentOutOfRange_BadHourMinuteSecond"));
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to the specified year, month, day, hour, minute, second, and millisecond.</summary>
		/// <param name="year">The year (1 through 9999).</param>
		/// <param name="month">The month (1 through 12).</param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />).</param>
		/// <param name="hour">The hours (0 through 23).</param>
		/// <param name="minute">The minutes (0 through 59).</param>
		/// <param name="second">The seconds (0 through 59).</param>
		/// <param name="millisecond">The milliseconds (0 through 999).</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than 1 or greater than 9999.  
		/// -or-  
		/// <paramref name="month" /> is less than 1 or greater than 12.  
		/// -or-  
		/// <paramref name="day" /> is less than 1 or greater than the number of days in <paramref name="month" />.  
		/// -or-  
		/// <paramref name="hour" /> is less than 0 or greater than 23.  
		/// -or-  
		/// <paramref name="minute" /> is less than 0 or greater than 59.  
		/// -or-  
		/// <paramref name="second" /> is less than 0 or greater than 59.  
		/// -or-  
		/// <paramref name="millisecond" /> is less than 0 or greater than 999.</exception>
		// Token: 0x060005EC RID: 1516 RVA: 0x00014F28 File Offset: 0x00013128
		[__DynamicallyInvokable]
		public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
		{
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[] { 0, 999 }));
			}
			if (second == 60 && DateTime.s_isLeapSecondsSupportedSystem && DateTime.IsValidTimeWithLeapSeconds(year, month, day, hour, minute, second, DateTimeKind.Unspecified))
			{
				second = 59;
			}
			long num = DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second);
			num += (long)millisecond * 10000L;
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_DateTimeRange"));
			}
			this.dateData = (ulong)num;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to the specified year, month, day, hour, minute, second, millisecond, and Coordinated Universal Time (UTC) or local time.</summary>
		/// <param name="year">The year (1 through 9999).</param>
		/// <param name="month">The month (1 through 12).</param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />).</param>
		/// <param name="hour">The hours (0 through 23).</param>
		/// <param name="minute">The minutes (0 through 59).</param>
		/// <param name="second">The seconds (0 through 59).</param>
		/// <param name="millisecond">The milliseconds (0 through 999).</param>
		/// <param name="kind">One of the enumeration values that indicates whether <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, <paramref name="hour" />, <paramref name="minute" />, <paramref name="second" />, and <paramref name="millisecond" /> specify a local time, Coordinated Universal Time (UTC), or neither.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than 1 or greater than 9999.  
		/// -or-  
		/// <paramref name="month" /> is less than 1 or greater than 12.  
		/// -or-  
		/// <paramref name="day" /> is less than 1 or greater than the number of days in <paramref name="month" />.  
		/// -or-  
		/// <paramref name="hour" /> is less than 0 or greater than 23.  
		/// -or-  
		/// <paramref name="minute" /> is less than 0 or greater than 59.  
		/// -or-  
		/// <paramref name="second" /> is less than 0 or greater than 59.  
		/// -or-  
		/// <paramref name="millisecond" /> is less than 0 or greater than 999.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="kind" /> is not one of the <see cref="T:System.DateTimeKind" /> values.</exception>
		// Token: 0x060005ED RID: 1517 RVA: 0x00014FE0 File Offset: 0x000131E0
		[__DynamicallyInvokable]
		public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, DateTimeKind kind)
		{
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[] { 0, 999 }));
			}
			if (kind < DateTimeKind.Unspecified || kind > DateTimeKind.Local)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDateTimeKind"), "kind");
			}
			if (second == 60 && DateTime.s_isLeapSecondsSupportedSystem && DateTime.IsValidTimeWithLeapSeconds(year, month, day, hour, minute, second, kind))
			{
				second = 59;
			}
			long num = DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second);
			num += (long)millisecond * 10000L;
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_DateTimeRange"));
			}
			this.dateData = (ulong)(num | ((long)kind << 62));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to the specified year, month, day, hour, minute, second, and millisecond for the specified calendar.</summary>
		/// <param name="year">The year (1 through the number of years in <paramref name="calendar" />).</param>
		/// <param name="month">The month (1 through the number of months in <paramref name="calendar" />).</param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />).</param>
		/// <param name="hour">The hours (0 through 23).</param>
		/// <param name="minute">The minutes (0 through 59).</param>
		/// <param name="second">The seconds (0 through 59).</param>
		/// <param name="millisecond">The milliseconds (0 through 999).</param>
		/// <param name="calendar">The calendar that is used to interpret <paramref name="year" />, <paramref name="month" />, and <paramref name="day" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="calendar" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is not in the range supported by <paramref name="calendar" />.  
		/// -or-  
		/// <paramref name="month" /> is less than 1 or greater than the number of months in <paramref name="calendar" />.  
		/// -or-  
		/// <paramref name="day" /> is less than 1 or greater than the number of days in <paramref name="month" />.  
		/// -or-  
		/// <paramref name="hour" /> is less than 0 or greater than 23.  
		/// -or-  
		/// <paramref name="minute" /> is less than 0 or greater than 59.  
		/// -or-  
		/// <paramref name="second" /> is less than 0 or greater than 59.  
		/// -or-  
		/// <paramref name="millisecond" /> is less than 0 or greater than 999.</exception>
		// Token: 0x060005EE RID: 1518 RVA: 0x000150C0 File Offset: 0x000132C0
		public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar)
		{
			if (calendar == null)
			{
				throw new ArgumentNullException("calendar");
			}
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[] { 0, 999 }));
			}
			int num = second;
			if (second == 60 && DateTime.s_isLeapSecondsSupportedSystem)
			{
				second = 59;
			}
			long num2 = calendar.ToDateTime(year, month, day, hour, minute, second, 0).Ticks;
			num2 += (long)millisecond * 10000L;
			if (num2 < 0L || num2 > 3155378975999999999L)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_DateTimeRange"));
			}
			this.dateData = (ulong)num2;
			if (num == 60)
			{
				DateTime dateTime = new DateTime(this.dateData);
				if (!DateTime.IsValidTimeWithLeapSeconds(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 60, DateTimeKind.Unspecified))
				{
					throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("ArgumentOutOfRange_BadHourMinuteSecond"));
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.DateTime" /> structure to the specified year, month, day, hour, minute, second, millisecond, and Coordinated Universal Time (UTC) or local time for the specified calendar.</summary>
		/// <param name="year">The year (1 through the number of years in <paramref name="calendar" />).</param>
		/// <param name="month">The month (1 through the number of months in <paramref name="calendar" />).</param>
		/// <param name="day">The day (1 through the number of days in <paramref name="month" />).</param>
		/// <param name="hour">The hours (0 through 23).</param>
		/// <param name="minute">The minutes (0 through 59).</param>
		/// <param name="second">The seconds (0 through 59).</param>
		/// <param name="millisecond">The milliseconds (0 through 999).</param>
		/// <param name="calendar">The calendar that is used to interpret <paramref name="year" />, <paramref name="month" />, and <paramref name="day" />.</param>
		/// <param name="kind">One of the enumeration values that indicates whether <paramref name="year" />, <paramref name="month" />, <paramref name="day" />, <paramref name="hour" />, <paramref name="minute" />, <paramref name="second" />, and <paramref name="millisecond" /> specify a local time, Coordinated Universal Time (UTC), or neither.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="calendar" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is not in the range supported by <paramref name="calendar" />.  
		/// -or-  
		/// <paramref name="month" /> is less than 1 or greater than the number of months in <paramref name="calendar" />.  
		/// -or-  
		/// <paramref name="day" /> is less than 1 or greater than the number of days in <paramref name="month" />.  
		/// -or-  
		/// <paramref name="hour" /> is less than 0 or greater than 23.  
		/// -or-  
		/// <paramref name="minute" /> is less than 0 or greater than 59.  
		/// -or-  
		/// <paramref name="second" /> is less than 0 or greater than 59.  
		/// -or-  
		/// <paramref name="millisecond" /> is less than 0 or greater than 999.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="kind" /> is not one of the <see cref="T:System.DateTimeKind" /> values.</exception>
		// Token: 0x060005EF RID: 1519 RVA: 0x000151D0 File Offset: 0x000133D0
		public DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, Calendar calendar, DateTimeKind kind)
		{
			if (calendar == null)
			{
				throw new ArgumentNullException("calendar");
			}
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[] { 0, 999 }));
			}
			if (kind < DateTimeKind.Unspecified || kind > DateTimeKind.Local)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidDateTimeKind"), "kind");
			}
			int num = second;
			if (second == 60 && DateTime.s_isLeapSecondsSupportedSystem)
			{
				second = 59;
			}
			long num2 = calendar.ToDateTime(year, month, day, hour, minute, second, 0).Ticks;
			num2 += (long)millisecond * 10000L;
			if (num2 < 0L || num2 > 3155378975999999999L)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_DateTimeRange"));
			}
			this.dateData = (ulong)(num2 | ((long)kind << 62));
			if (num == 60)
			{
				DateTime dateTime = new DateTime(this.dateData);
				if (!DateTime.IsValidTimeWithLeapSeconds(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 60, kind))
				{
					throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("ArgumentOutOfRange_BadHourMinuteSecond"));
				}
			}
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00015308 File Offset: 0x00013508
		private DateTime(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			bool flag = false;
			bool flag2 = false;
			long num = 0L;
			ulong num2 = 0UL;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				if (!(name == "ticks"))
				{
					if (name == "dateData")
					{
						num2 = Convert.ToUInt64(enumerator.Value, CultureInfo.InvariantCulture);
						flag2 = true;
					}
				}
				else
				{
					num = Convert.ToInt64(enumerator.Value, CultureInfo.InvariantCulture);
					flag = true;
				}
			}
			if (flag2)
			{
				this.dateData = num2;
			}
			else
			{
				if (!flag)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_MissingDateTimeData"));
				}
				this.dateData = (ulong)num;
			}
			long internalTicks = this.InternalTicks;
			if (internalTicks < 0L || internalTicks > 3155378975999999999L)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_DateTimeTicksOutOfRange"));
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x000153E4 File Offset: 0x000135E4
		internal long InternalTicks
		{
			get
			{
				return (long)(this.dateData & 4611686018427387903UL);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x000153F6 File Offset: 0x000135F6
		private ulong InternalKind
		{
			get
			{
				return this.dateData & 13835058055282163712UL;
			}
		}

		/// <summary>Returns a new <see cref="T:System.DateTime" /> that adds the value of the specified <see cref="T:System.TimeSpan" /> to the value of this instance.</summary>
		/// <param name="value">A positive or negative time interval.</param>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the time interval represented by <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />.</exception>
		// Token: 0x060005F3 RID: 1523 RVA: 0x00015408 File Offset: 0x00013608
		[__DynamicallyInvokable]
		public DateTime Add(TimeSpan value)
		{
			return this.AddTicks(value._ticks);
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00015418 File Offset: 0x00013618
		private DateTime Add(double value, int scale)
		{
			long num = (long)(value * (double)scale + ((value >= 0.0) ? 0.5 : (-0.5)));
			if (num <= -315537897600000L || num >= 315537897600000L)
			{
				throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_AddValue"));
			}
			return this.AddTicks(num * 10000L);
		}

		/// <summary>Returns a new <see cref="T:System.DateTime" /> that adds the specified number of days to the value of this instance.</summary>
		/// <param name="value">A number of whole and fractional days. The <paramref name="value" /> parameter can be negative or positive.</param>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the number of days represented by <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />.</exception>
		// Token: 0x060005F5 RID: 1525 RVA: 0x00015487 File Offset: 0x00013687
		[__DynamicallyInvokable]
		public DateTime AddDays(double value)
		{
			return this.Add(value, 86400000);
		}

		/// <summary>Returns a new <see cref="T:System.DateTime" /> that adds the specified number of hours to the value of this instance.</summary>
		/// <param name="value">A number of whole and fractional hours. The <paramref name="value" /> parameter can be negative or positive.</param>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the number of hours represented by <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />.</exception>
		// Token: 0x060005F6 RID: 1526 RVA: 0x00015495 File Offset: 0x00013695
		[__DynamicallyInvokable]
		public DateTime AddHours(double value)
		{
			return this.Add(value, 3600000);
		}

		/// <summary>Returns a new <see cref="T:System.DateTime" /> that adds the specified number of milliseconds to the value of this instance.</summary>
		/// <param name="value">A number of whole and fractional milliseconds. The <paramref name="value" /> parameter can be negative or positive. Note that this value is rounded to the nearest integer.</param>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the number of milliseconds represented by <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />.</exception>
		// Token: 0x060005F7 RID: 1527 RVA: 0x000154A3 File Offset: 0x000136A3
		[__DynamicallyInvokable]
		public DateTime AddMilliseconds(double value)
		{
			return this.Add(value, 1);
		}

		/// <summary>Returns a new <see cref="T:System.DateTime" /> that adds the specified number of minutes to the value of this instance.</summary>
		/// <param name="value">A number of whole and fractional minutes. The <paramref name="value" /> parameter can be negative or positive.</param>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the number of minutes represented by <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />.</exception>
		// Token: 0x060005F8 RID: 1528 RVA: 0x000154AD File Offset: 0x000136AD
		[__DynamicallyInvokable]
		public DateTime AddMinutes(double value)
		{
			return this.Add(value, 60000);
		}

		/// <summary>Returns a new <see cref="T:System.DateTime" /> that adds the specified number of months to the value of this instance.</summary>
		/// <param name="months">A number of months. The <paramref name="months" /> parameter can be negative or positive.</param>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and <paramref name="months" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />.  
		///  -or-  
		///  <paramref name="months" /> is less than -120,000 or greater than 120,000.</exception>
		// Token: 0x060005F9 RID: 1529 RVA: 0x000154BC File Offset: 0x000136BC
		[__DynamicallyInvokable]
		public DateTime AddMonths(int months)
		{
			if (months < -120000 || months > 120000)
			{
				throw new ArgumentOutOfRangeException("months", Environment.GetResourceString("ArgumentOutOfRange_DateTimeBadMonths"));
			}
			int num;
			int num2;
			int num3;
			this.GetDatePart(out num, out num2, out num3);
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
			if (num < 1 || num > 9999)
			{
				throw new ArgumentOutOfRangeException("months", Environment.GetResourceString("ArgumentOutOfRange_DateArithmetic"));
			}
			int num5 = DateTime.DaysInMonth(num, num2);
			if (num3 > num5)
			{
				num3 = num5;
			}
			return new DateTime((ulong)((DateTime.DateToTicks(num, num2, num3) + this.InternalTicks % 864000000000L) | (long)this.InternalKind));
		}

		/// <summary>Returns a new <see cref="T:System.DateTime" /> that adds the specified number of seconds to the value of this instance.</summary>
		/// <param name="value">A number of whole and fractional seconds. The <paramref name="value" /> parameter can be negative or positive.</param>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the number of seconds represented by <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />.</exception>
		// Token: 0x060005FA RID: 1530 RVA: 0x0001557F File Offset: 0x0001377F
		[__DynamicallyInvokable]
		public DateTime AddSeconds(double value)
		{
			return this.Add(value, 1000);
		}

		/// <summary>Returns a new <see cref="T:System.DateTime" /> that adds the specified number of ticks to the value of this instance.</summary>
		/// <param name="value">A number of 100-nanosecond ticks. The <paramref name="value" /> parameter can be positive or negative.</param>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the time represented by <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />.</exception>
		// Token: 0x060005FB RID: 1531 RVA: 0x00015590 File Offset: 0x00013790
		[__DynamicallyInvokable]
		public DateTime AddTicks(long value)
		{
			long internalTicks = this.InternalTicks;
			if (value > 3155378975999999999L - internalTicks || value < 0L - internalTicks)
			{
				throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_DateArithmetic"));
			}
			return new DateTime((ulong)((internalTicks + value) | (long)this.InternalKind));
		}

		/// <summary>Returns a new <see cref="T:System.DateTime" /> that adds the specified number of years to the value of this instance.</summary>
		/// <param name="value">A number of years. The <paramref name="value" /> parameter can be negative or positive.</param>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the number of years represented by <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> or the resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />.</exception>
		// Token: 0x060005FC RID: 1532 RVA: 0x000155DD File Offset: 0x000137DD
		[__DynamicallyInvokable]
		public DateTime AddYears(int value)
		{
			if (value < -10000 || value > 10000)
			{
				throw new ArgumentOutOfRangeException("years", Environment.GetResourceString("ArgumentOutOfRange_DateTimeBadYears"));
			}
			return this.AddMonths(value * 12);
		}

		/// <summary>Compares two instances of <see cref="T:System.DateTime" /> and returns an integer that indicates whether the first instance is earlier than, the same as, or later than the second instance.</summary>
		/// <param name="t1">The first object to compare.</param>
		/// <param name="t2">The second object to compare.</param>
		/// <returns>A signed number indicating the relative values of <paramref name="t1" /> and <paramref name="t2" />.  
		///   Value Type  
		///
		///   Condition  
		///
		///   Less than zero  
		///
		///  <paramref name="t1" /> is earlier than <paramref name="t2" />.  
		///
		///   Zero  
		///
		///  <paramref name="t1" /> is the same as <paramref name="t2" />.  
		///
		///   Greater than zero  
		///
		///  <paramref name="t1" /> is later than <paramref name="t2" />.</returns>
		// Token: 0x060005FD RID: 1533 RVA: 0x00015610 File Offset: 0x00013810
		[__DynamicallyInvokable]
		public static int Compare(DateTime t1, DateTime t2)
		{
			long internalTicks = t1.InternalTicks;
			long internalTicks2 = t2.InternalTicks;
			if (internalTicks > internalTicks2)
			{
				return 1;
			}
			if (internalTicks < internalTicks2)
			{
				return -1;
			}
			return 0;
		}

		/// <summary>Compares the value of this instance to a specified object that contains a specified <see cref="T:System.DateTime" /> value, and returns an integer that indicates whether this instance is earlier than, the same as, or later than the specified <see cref="T:System.DateTime" /> value.</summary>
		/// <param name="value">A boxed object to compare, or <see langword="null" />.</param>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.  
		///   Value  
		///
		///   Description  
		///
		///   Less than zero  
		///
		///   This instance is earlier than <paramref name="value" />.  
		///
		///   Zero  
		///
		///   This instance is the same as <paramref name="value" />.  
		///
		///   Greater than zero  
		///
		///   This instance is later than <paramref name="value" />, or <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.DateTime" />.</exception>
		// Token: 0x060005FE RID: 1534 RVA: 0x0001563C File Offset: 0x0001383C
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is DateTime))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDateTime"));
			}
			long internalTicks = ((DateTime)value).InternalTicks;
			long internalTicks2 = this.InternalTicks;
			if (internalTicks2 > internalTicks)
			{
				return 1;
			}
			if (internalTicks2 < internalTicks)
			{
				return -1;
			}
			return 0;
		}

		/// <summary>Compares the value of this instance to a specified <see cref="T:System.DateTime" /> value and returns an integer that indicates whether this instance is earlier than, the same as, or later than the specified <see cref="T:System.DateTime" /> value.</summary>
		/// <param name="value">The object to compare to the current instance.</param>
		/// <returns>A signed number indicating the relative values of this instance and the <paramref name="value" /> parameter.  
		///   Value  
		///
		///   Description  
		///
		///   Less than zero  
		///
		///   This instance is earlier than <paramref name="value" />.  
		///
		///   Zero  
		///
		///   This instance is the same as <paramref name="value" />.  
		///
		///   Greater than zero  
		///
		///   This instance is later than <paramref name="value" />.</returns>
		// Token: 0x060005FF RID: 1535 RVA: 0x0001568C File Offset: 0x0001388C
		[__DynamicallyInvokable]
		public int CompareTo(DateTime value)
		{
			long internalTicks = value.InternalTicks;
			long internalTicks2 = this.InternalTicks;
			if (internalTicks2 > internalTicks)
			{
				return 1;
			}
			if (internalTicks2 < internalTicks)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x000156B8 File Offset: 0x000138B8
		private static long DateToTicks(int year, int month, int day)
		{
			if (year >= 1 && year <= 9999 && month >= 1 && month <= 12)
			{
				int[] array = (DateTime.IsLeapYear(year) ? DateTime.DaysToMonth366 : DateTime.DaysToMonth365);
				if (day >= 1 && day <= array[month] - array[month - 1])
				{
					int num = year - 1;
					int num2 = num * 365 + num / 4 - num / 100 + num / 400 + array[month - 1] + day - 1;
					return (long)num2 * 864000000000L;
				}
			}
			throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("ArgumentOutOfRange_BadYearMonthDay"));
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00015743 File Offset: 0x00013943
		private static long TimeToTicks(int hour, int minute, int second)
		{
			if (hour >= 0 && hour < 24 && minute >= 0 && minute < 60 && second >= 0 && second < 60)
			{
				return TimeSpan.TimeToTicks(hour, minute, second);
			}
			throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("ArgumentOutOfRange_BadHourMinuteSecond"));
		}

		/// <summary>Returns the number of days in the specified month and year.</summary>
		/// <param name="year">The year.</param>
		/// <param name="month">The month (a number ranging from 1 to 12).</param>
		/// <returns>The number of days in <paramref name="month" /> for the specified <paramref name="year" />.  
		///  For example, if <paramref name="month" /> equals 2 for February, the return value is 28 or 29 depending upon whether <paramref name="year" /> is a leap year.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="month" /> is less than 1 or greater than 12.  
		/// -or-  
		/// <paramref name="year" /> is less than 1 or greater than 9999.</exception>
		// Token: 0x06000602 RID: 1538 RVA: 0x0001577C File Offset: 0x0001397C
		[__DynamicallyInvokable]
		public static int DaysInMonth(int year, int month)
		{
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("ArgumentOutOfRange_Month"));
			}
			int[] array = (DateTime.IsLeapYear(year) ? DateTime.DaysToMonth366 : DateTime.DaysToMonth365);
			return array[month] - array[month - 1];
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x000157C8 File Offset: 0x000139C8
		internal static long DoubleDateToTicks(double value)
		{
			if (value >= 2958466.0 || value <= -657435.0)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_OleAutDateInvalid"));
			}
			long num = (long)(value * 86400000.0 + ((value >= 0.0) ? 0.5 : (-0.5)));
			if (num < 0L)
			{
				num -= num % 86400000L * 2L;
			}
			num += 59926435200000L;
			if (num < 0L || num >= 315537897600000L)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_OleAutDateScale"));
			}
			return num * 10000L;
		}

		// Token: 0x06000604 RID: 1540
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool LegacyParseMode();

		// Token: 0x06000605 RID: 1541
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool EnableAmPmParseAdjustment();

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <param name="value">The object to compare to this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is an instance of <see cref="T:System.DateTime" /> and equals the value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000606 RID: 1542 RVA: 0x00015874 File Offset: 0x00013A74
		[__DynamicallyInvokable]
		public override bool Equals(object value)
		{
			return value is DateTime && this.InternalTicks == ((DateTime)value).InternalTicks;
		}

		/// <summary>Returns a value indicating whether the value of this instance is equal to the value of the specified <see cref="T:System.DateTime" /> instance.</summary>
		/// <param name="value">The object to compare to this instance.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="value" /> parameter equals the value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000607 RID: 1543 RVA: 0x000158A1 File Offset: 0x00013AA1
		[__DynamicallyInvokable]
		public bool Equals(DateTime value)
		{
			return this.InternalTicks == value.InternalTicks;
		}

		/// <summary>Returns a value indicating whether two <see cref="T:System.DateTime" /> instances  have the same date and time value.</summary>
		/// <param name="t1">The first object to compare.</param>
		/// <param name="t2">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the two values are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000608 RID: 1544 RVA: 0x000158B2 File Offset: 0x00013AB2
		[__DynamicallyInvokable]
		public static bool Equals(DateTime t1, DateTime t2)
		{
			return t1.InternalTicks == t2.InternalTicks;
		}

		/// <summary>Deserializes a 64-bit binary value and recreates an original serialized <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="dateData">A 64-bit signed integer that encodes the <see cref="P:System.DateTime.Kind" /> property in a 2-bit field and the <see cref="P:System.DateTime.Ticks" /> property in a 62-bit field.</param>
		/// <returns>An object that is equivalent to the <see cref="T:System.DateTime" /> object that was serialized by the <see cref="M:System.DateTime.ToBinary" /> method.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dateData" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />.</exception>
		// Token: 0x06000609 RID: 1545 RVA: 0x000158C4 File Offset: 0x00013AC4
		[__DynamicallyInvokable]
		public static DateTime FromBinary(long dateData)
		{
			if ((dateData & -9223372036854775808L) == 0L)
			{
				return DateTime.FromBinaryRaw(dateData);
			}
			long num = dateData & 4611686018427387903L;
			if (num > 4611685154427387904L)
			{
				num -= 4611686018427387904L;
			}
			bool flag = false;
			long num2;
			if (num < 0L)
			{
				num2 = TimeZoneInfo.GetLocalUtcOffset(DateTime.MinValue, TimeZoneInfoOptions.NoThrowOnInvalidTime).Ticks;
			}
			else if (num > 3155378975999999999L)
			{
				num2 = TimeZoneInfo.GetLocalUtcOffset(DateTime.MaxValue, TimeZoneInfoOptions.NoThrowOnInvalidTime).Ticks;
			}
			else
			{
				DateTime dateTime = new DateTime(num, DateTimeKind.Utc);
				bool flag2 = false;
				num2 = TimeZoneInfo.GetUtcOffsetFromUtc(dateTime, TimeZoneInfo.Local, out flag2, out flag).Ticks;
			}
			num += num2;
			if (num < 0L)
			{
				num += 864000000000L;
			}
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeBadBinaryData"), "dateData");
			}
			return new DateTime(num, DateTimeKind.Local, flag);
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x000159B4 File Offset: 0x00013BB4
		internal static DateTime FromBinaryRaw(long dateData)
		{
			long num = dateData & 4611686018427387903L;
			if (num < 0L || num > 3155378975999999999L)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeBadBinaryData"), "dateData");
			}
			return new DateTime((ulong)dateData);
		}

		/// <summary>Converts the specified Windows file time to an equivalent local time.</summary>
		/// <param name="fileTime">A Windows file time expressed in ticks.</param>
		/// <returns>An object that represents the local time equivalent of the date and time represented by the <paramref name="fileTime" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="fileTime" /> is less than 0 or represents a time greater than <see cref="F:System.DateTime.MaxValue" />.</exception>
		// Token: 0x0600060B RID: 1547 RVA: 0x000159FC File Offset: 0x00013BFC
		[__DynamicallyInvokable]
		public static DateTime FromFileTime(long fileTime)
		{
			return DateTime.FromFileTimeUtc(fileTime).ToLocalTime();
		}

		/// <summary>Converts the specified Windows file time to an equivalent UTC time.</summary>
		/// <param name="fileTime">A Windows file time expressed in ticks.</param>
		/// <returns>An object that represents the UTC time equivalent of the date and time represented by the <paramref name="fileTime" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="fileTime" /> is less than 0 or represents a time greater than <see cref="F:System.DateTime.MaxValue" />.</exception>
		// Token: 0x0600060C RID: 1548 RVA: 0x00015A18 File Offset: 0x00013C18
		[__DynamicallyInvokable]
		public static DateTime FromFileTimeUtc(long fileTime)
		{
			if (fileTime < 0L || fileTime > 2650467743999999999L)
			{
				throw new ArgumentOutOfRangeException("fileTime", Environment.GetResourceString("ArgumentOutOfRange_FileTimeInvalid"));
			}
			if (DateTime.s_isLeapSecondsSupportedSystem)
			{
				return DateTime.InternalFromFileTime(fileTime);
			}
			long num = fileTime + 504911232000000000L;
			return new DateTime(num, DateTimeKind.Utc);
		}

		/// <summary>Returns a <see cref="T:System.DateTime" /> equivalent to the specified OLE Automation Date.</summary>
		/// <param name="d">An OLE Automation Date value.</param>
		/// <returns>An object that represents the same date and time as <paramref name="d" />.</returns>
		/// <exception cref="T:System.ArgumentException">The date is not a valid OLE Automation Date value.</exception>
		// Token: 0x0600060D RID: 1549 RVA: 0x00015A6C File Offset: 0x00013C6C
		[__DynamicallyInvokable]
		public static DateTime FromOADate(double d)
		{
			return new DateTime(DateTime.DoubleDateToTicks(d), DateTimeKind.Unspecified);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data needed to serialize the current <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="info">The object to populate with data.</param>
		/// <param name="context">The destination for this serialization. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x0600060E RID: 1550 RVA: 0x00015A7A File Offset: 0x00013C7A
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("ticks", this.InternalTicks);
			info.AddValue("dateData", this.dateData);
		}

		/// <summary>Indicates whether this instance of <see cref="T:System.DateTime" /> is within the daylight saving time range for the current time zone.</summary>
		/// <returns>
		///   <see langword="true" /> if the value of the <see cref="P:System.DateTime.Kind" /> property is <see cref="F:System.DateTimeKind.Local" /> or <see cref="F:System.DateTimeKind.Unspecified" /> and the value of this instance of <see cref="T:System.DateTime" /> is within the daylight saving time range for the local time zone; <see langword="false" /> if <see cref="P:System.DateTime.Kind" /> is <see cref="F:System.DateTimeKind.Utc" />.</returns>
		// Token: 0x0600060F RID: 1551 RVA: 0x00015AAC File Offset: 0x00013CAC
		[__DynamicallyInvokable]
		public bool IsDaylightSavingTime()
		{
			return this.Kind != DateTimeKind.Utc && TimeZoneInfo.Local.IsDaylightSavingTime(this, TimeZoneInfoOptions.NoThrowOnInvalidTime);
		}

		/// <summary>Creates a new <see cref="T:System.DateTime" /> object that has the same number of ticks as the specified <see cref="T:System.DateTime" />, but is designated as either local time, Coordinated Universal Time (UTC), or neither, as indicated by the specified <see cref="T:System.DateTimeKind" /> value.</summary>
		/// <param name="value">A date and time.</param>
		/// <param name="kind">One of the enumeration values that indicates whether the new object represents local time, UTC, or neither.</param>
		/// <returns>A new object that has the same number of ticks as the object represented by the <paramref name="value" /> parameter and the <see cref="T:System.DateTimeKind" /> value specified by the <paramref name="kind" /> parameter.</returns>
		// Token: 0x06000610 RID: 1552 RVA: 0x00015ACA File Offset: 0x00013CCA
		[__DynamicallyInvokable]
		public static DateTime SpecifyKind(DateTime value, DateTimeKind kind)
		{
			return new DateTime(value.InternalTicks, kind);
		}

		/// <summary>Serializes the current <see cref="T:System.DateTime" /> object to a 64-bit binary value that subsequently can be used to recreate the <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>A 64-bit signed integer that encodes the <see cref="P:System.DateTime.Kind" /> and <see cref="P:System.DateTime.Ticks" /> properties.</returns>
		// Token: 0x06000611 RID: 1553 RVA: 0x00015ADC File Offset: 0x00013CDC
		[__DynamicallyInvokable]
		public long ToBinary()
		{
			if (this.Kind == DateTimeKind.Local)
			{
				TimeSpan localUtcOffset = TimeZoneInfo.GetLocalUtcOffset(this, TimeZoneInfoOptions.NoThrowOnInvalidTime);
				long ticks = this.Ticks;
				long num = ticks - localUtcOffset.Ticks;
				if (num < 0L)
				{
					num = 4611686018427387904L + num;
				}
				return num | long.MinValue;
			}
			return (long)this.dateData;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00015B33 File Offset: 0x00013D33
		internal long ToBinaryRaw()
		{
			return (long)this.dateData;
		}

		/// <summary>Gets the date component of this instance.</summary>
		/// <returns>A new object with the same date as this instance, and the time value set to 12:00:00 midnight (00:00:00).</returns>
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x00015B3C File Offset: 0x00013D3C
		[__DynamicallyInvokable]
		public DateTime Date
		{
			[__DynamicallyInvokable]
			get
			{
				long internalTicks = this.InternalTicks;
				return new DateTime((ulong)((internalTicks - internalTicks % 864000000000L) | (long)this.InternalKind));
			}
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00015B6C File Offset: 0x00013D6C
		private int GetDatePart(int part)
		{
			long internalTicks = this.InternalTicks;
			int i = (int)(internalTicks / 864000000000L);
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
			int[] array = ((num4 == 3 && (num3 != 24 || num2 == 3)) ? DateTime.DaysToMonth366 : DateTime.DaysToMonth365);
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

		// Token: 0x06000615 RID: 1557 RVA: 0x00015C5C File Offset: 0x00013E5C
		internal void GetDatePart(out int year, out int month, out int day)
		{
			long internalTicks = this.InternalTicks;
			int i = (int)(internalTicks / 864000000000L);
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
			year = num * 400 + num2 * 100 + num3 * 4 + num4 + 1;
			i -= num4 * 365;
			int[] array = ((num4 == 3 && (num3 != 24 || num2 == 3)) ? DateTime.DaysToMonth366 : DateTime.DaysToMonth365);
			int num5 = (i >> 5) + 1;
			while (i >= array[num5])
			{
				num5++;
			}
			month = num5;
			day = i - array[num5 - 1] + 1;
		}

		/// <summary>Gets the day of the month represented by this instance.</summary>
		/// <returns>The day component, expressed as a value between 1 and 31.</returns>
		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x00015D40 File Offset: 0x00013F40
		[__DynamicallyInvokable]
		public int Day
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetDatePart(3);
			}
		}

		/// <summary>Gets the day of the week represented by this instance.</summary>
		/// <returns>An enumerated constant that indicates the day of the week of this <see cref="T:System.DateTime" /> value.</returns>
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x00015D49 File Offset: 0x00013F49
		[__DynamicallyInvokable]
		public DayOfWeek DayOfWeek
		{
			[__DynamicallyInvokable]
			get
			{
				return (DayOfWeek)((this.InternalTicks / 864000000000L + 1L) % 7L);
			}
		}

		/// <summary>Gets the day of the year represented by this instance.</summary>
		/// <returns>The day of the year, expressed as a value between 1 and 366.</returns>
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x00015D62 File Offset: 0x00013F62
		[__DynamicallyInvokable]
		public int DayOfYear
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetDatePart(1);
			}
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000619 RID: 1561 RVA: 0x00015D6C File Offset: 0x00013F6C
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			long internalTicks = this.InternalTicks;
			return (int)internalTicks ^ (int)(internalTicks >> 32);
		}

		/// <summary>Gets the hour component of the date represented by this instance.</summary>
		/// <returns>The hour component, expressed as a value between 0 and 23.</returns>
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x00015D88 File Offset: 0x00013F88
		[__DynamicallyInvokable]
		public int Hour
		{
			[__DynamicallyInvokable]
			get
			{
				return (int)(this.InternalTicks / 36000000000L % 24L);
			}
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00015D9F File Offset: 0x00013F9F
		internal bool IsAmbiguousDaylightSavingTime()
		{
			return this.InternalKind == 13835058055282163712UL;
		}

		/// <summary>Gets a value that indicates whether the time represented by this instance is based on local time, Coordinated Universal Time (UTC), or neither.</summary>
		/// <returns>One of the enumeration values that indicates what the current time represents. The default is <see cref="F:System.DateTimeKind.Unspecified" />.</returns>
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x00015DB4 File Offset: 0x00013FB4
		[__DynamicallyInvokable]
		public DateTimeKind Kind
		{
			[__DynamicallyInvokable]
			get
			{
				ulong internalKind = this.InternalKind;
				if (internalKind == 0UL)
				{
					return DateTimeKind.Unspecified;
				}
				if (internalKind != 4611686018427387904UL)
				{
					return DateTimeKind.Local;
				}
				return DateTimeKind.Utc;
			}
		}

		/// <summary>Gets the milliseconds component of the date represented by this instance.</summary>
		/// <returns>The milliseconds component, expressed as a value between 0 and 999.</returns>
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x00015DDE File Offset: 0x00013FDE
		[__DynamicallyInvokable]
		public int Millisecond
		{
			[__DynamicallyInvokable]
			get
			{
				return (int)(this.InternalTicks / 10000L % 1000L);
			}
		}

		/// <summary>Gets the minute component of the date represented by this instance.</summary>
		/// <returns>The minute component, expressed as a value between 0 and 59.</returns>
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x00015DF5 File Offset: 0x00013FF5
		[__DynamicallyInvokable]
		public int Minute
		{
			[__DynamicallyInvokable]
			get
			{
				return (int)(this.InternalTicks / 600000000L % 60L);
			}
		}

		/// <summary>Gets the month component of the date represented by this instance.</summary>
		/// <returns>The month component, expressed as a value between 1 and 12.</returns>
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x00015E09 File Offset: 0x00014009
		[__DynamicallyInvokable]
		public int Month
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetDatePart(2);
			}
		}

		/// <summary>Gets a <see cref="T:System.DateTime" /> object that is set to the current date and time on this computer, expressed as the local time.</summary>
		/// <returns>An object whose value is the current local date and time.</returns>
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x00015E14 File Offset: 0x00014014
		[__DynamicallyInvokable]
		public static DateTime Now
		{
			[__DynamicallyInvokable]
			get
			{
				DateTime utcNow = DateTime.UtcNow;
				bool flag = false;
				long ticks = TimeZoneInfo.GetDateTimeNowUtcOffsetFromUtc(utcNow, out flag).Ticks;
				long num = utcNow.Ticks + ticks;
				if (num > 3155378975999999999L)
				{
					return new DateTime(3155378975999999999L, DateTimeKind.Local);
				}
				if (num < 0L)
				{
					return new DateTime(0L, DateTimeKind.Local);
				}
				return new DateTime(num, DateTimeKind.Local, flag);
			}
		}

		/// <summary>Gets a <see cref="T:System.DateTime" /> object that is set to the current date and time on this computer, expressed as the Coordinated Universal Time (UTC).</summary>
		/// <returns>An object whose value is the current UTC date and time.</returns>
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x00015E78 File Offset: 0x00014078
		[__DynamicallyInvokable]
		public static DateTime UtcNow
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				if (DateTime.s_isLeapSecondsSupportedSystem)
				{
					DateTime.FullSystemTime fullSystemTime = default(DateTime.FullSystemTime);
					DateTime.GetSystemTimeWithLeapSecondsHandling(ref fullSystemTime);
					return DateTime.CreateDateTimeFromSystemTime(ref fullSystemTime);
				}
				long systemTimeAsFileTime = DateTime.GetSystemTimeAsFileTime();
				return new DateTime((ulong)((systemTimeAsFileTime + 504911232000000000L) | 4611686018427387904L));
			}
		}

		// Token: 0x06000622 RID: 1570
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern long GetSystemTimeAsFileTime();

		// Token: 0x06000623 RID: 1571
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool ValidateSystemTime(ref DateTime.FullSystemTime time, bool localTime);

		// Token: 0x06000624 RID: 1572
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void GetSystemTimeWithLeapSecondsHandling(ref DateTime.FullSystemTime time);

		// Token: 0x06000625 RID: 1573
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern bool IsLeapSecondsSupportedSystem();

		// Token: 0x06000626 RID: 1574
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool SystemFileTimeToSystemTime(long fileTime, ref DateTime.FullSystemTime time);

		// Token: 0x06000627 RID: 1575
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool SystemTimeToSystemFileTime(ref DateTime.FullSystemTime time, ref long fileTime);

		// Token: 0x06000628 RID: 1576 RVA: 0x00015EC6 File Offset: 0x000140C6
		[SecuritySafeCritical]
		internal static bool SystemSupportLeapSeconds()
		{
			return DateTime.IsLeapSecondsSupportedSystem();
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00015ED0 File Offset: 0x000140D0
		[SecuritySafeCritical]
		internal static DateTime InternalFromFileTime(long fileTime)
		{
			DateTime.FullSystemTime fullSystemTime = default(DateTime.FullSystemTime);
			if (DateTime.SystemFileTimeToSystemTime(fileTime, ref fullSystemTime))
			{
				fullSystemTime.hundredNanoSecond = fileTime % 10000L;
				return DateTime.CreateDateTimeFromSystemTime(ref fullSystemTime);
			}
			throw new ArgumentOutOfRangeException("fileTime", Environment.GetResourceString("ArgumentOutOfRange_DateTimeBadTicks"));
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00015F1C File Offset: 0x0001411C
		[SecuritySafeCritical]
		internal static long InternalToFileTime(long ticks)
		{
			long num = 0L;
			DateTime.FullSystemTime fullSystemTime = new DateTime.FullSystemTime(ticks);
			if (DateTime.SystemTimeToSystemFileTime(ref fullSystemTime, ref num))
			{
				return num + ticks % 10000L;
			}
			throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("ArgumentOutOfRange_FileTimeInvalid"));
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00015F5C File Offset: 0x0001415C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static DateTime CreateDateTimeFromSystemTime(ref DateTime.FullSystemTime time)
		{
			long num = DateTime.DateToTicks((int)time.wYear, (int)time.wMonth, (int)time.wDay);
			num += DateTime.TimeToTicks((int)time.wHour, (int)time.wMinute, (int)time.wSecond);
			num += (long)((ulong)time.wMillisecond * 10000UL);
			num += time.hundredNanoSecond;
			return new DateTime((ulong)(num | 4611686018427387904L));
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00015FC8 File Offset: 0x000141C8
		[SecuritySafeCritical]
		internal static bool IsValidTimeWithLeapSeconds(int year, int month, int day, int hour, int minute, int second, DateTimeKind kind)
		{
			DateTime dateTime = new DateTime(year, month, day);
			DateTime.FullSystemTime fullSystemTime = new DateTime.FullSystemTime(year, month, dateTime.DayOfWeek, day, hour, minute, second);
			if (kind == DateTimeKind.Utc)
			{
				return DateTime.ValidateSystemTime(ref fullSystemTime, false);
			}
			if (kind == DateTimeKind.Local)
			{
				return DateTime.ValidateSystemTime(ref fullSystemTime, true);
			}
			return DateTime.ValidateSystemTime(ref fullSystemTime, true) || DateTime.ValidateSystemTime(ref fullSystemTime, false);
		}

		/// <summary>Gets the seconds component of the date represented by this instance.</summary>
		/// <returns>The seconds component, expressed as a value between 0 and 59.</returns>
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x00016025 File Offset: 0x00014225
		[__DynamicallyInvokable]
		public int Second
		{
			[__DynamicallyInvokable]
			get
			{
				return (int)(this.InternalTicks / 10000000L % 60L);
			}
		}

		/// <summary>Gets the number of ticks that represent the date and time of this instance.</summary>
		/// <returns>The number of ticks that represent the date and time of this instance. The value is between <see langword="DateTime.MinValue.Ticks" /> and <see langword="DateTime.MaxValue.Ticks" />.</returns>
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x00016039 File Offset: 0x00014239
		[__DynamicallyInvokable]
		public long Ticks
		{
			[__DynamicallyInvokable]
			get
			{
				return this.InternalTicks;
			}
		}

		/// <summary>Gets the time of day for this instance.</summary>
		/// <returns>A time interval that represents the fraction of the day that has elapsed since midnight.</returns>
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x00016041 File Offset: 0x00014241
		[__DynamicallyInvokable]
		public TimeSpan TimeOfDay
		{
			[__DynamicallyInvokable]
			get
			{
				return new TimeSpan(this.InternalTicks % 864000000000L);
			}
		}

		/// <summary>Gets the current date.</summary>
		/// <returns>An object that is set to today's date, with the time component set to 00:00:00.</returns>
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x00016058 File Offset: 0x00014258
		[__DynamicallyInvokable]
		public static DateTime Today
		{
			[__DynamicallyInvokable]
			get
			{
				return DateTime.Now.Date;
			}
		}

		/// <summary>Gets the year component of the date represented by this instance.</summary>
		/// <returns>The year, between 1 and 9999.</returns>
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x00016072 File Offset: 0x00014272
		[__DynamicallyInvokable]
		public int Year
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetDatePart(0);
			}
		}

		/// <summary>Returns an indication whether the specified year is a leap year.</summary>
		/// <param name="year">A 4-digit year.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="year" /> is a leap year; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than 1 or greater than 9999.</exception>
		// Token: 0x06000632 RID: 1586 RVA: 0x0001607B File Offset: 0x0001427B
		[__DynamicallyInvokable]
		public static bool IsLeapYear(int year)
		{
			if (year < 1 || year > 9999)
			{
				throw new ArgumentOutOfRangeException("year", Environment.GetResourceString("ArgumentOutOfRange_Year"));
			}
			return year % 4 == 0 && (year % 100 != 0 || year % 400 == 0);
		}

		/// <summary>Converts the string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent by using the conventions of the current thread culture.</summary>
		/// <param name="s">A string that contains a date and time to convert. See The string to parse for more information.</param>
		/// <returns>An object that is equivalent to the date and time contained in <paramref name="s" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> does not contain a valid string representation of a date and time.</exception>
		// Token: 0x06000633 RID: 1587 RVA: 0x000160B7 File Offset: 0x000142B7
		[__DynamicallyInvokable]
		public static DateTime Parse(string s)
		{
			return DateTimeParse.Parse(s, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None);
		}

		/// <summary>Converts the string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent by using culture-specific format information.</summary>
		/// <param name="s">A string that contains a date and time to convert. See The string to parse for more information.</param>
		/// <param name="provider">An object that supplies culture-specific format information about <paramref name="s" />.  See Parsing and cultural conventions</param>
		/// <returns>An object that is equivalent to the date and time contained in <paramref name="s" /> as specified by <paramref name="provider" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> does not contain a valid string representation of a date and time.</exception>
		// Token: 0x06000634 RID: 1588 RVA: 0x000160C5 File Offset: 0x000142C5
		[__DynamicallyInvokable]
		public static DateTime Parse(string s, IFormatProvider provider)
		{
			return DateTimeParse.Parse(s, DateTimeFormatInfo.GetInstance(provider), DateTimeStyles.None);
		}

		/// <summary>Converts the string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent by using culture-specific format information and a formatting style.</summary>
		/// <param name="s">A string that contains a date and time to convert. See The string to parse for more information.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />.  See Parsing and cultural conventions</param>
		/// <param name="styles">A bitwise combination of the enumeration values that indicates the style elements that can be present in <paramref name="s" /> for the parse operation to succeed, and that defines how to interpret the parsed date in relation to the current time zone or the current date. A typical value to specify is <see cref="F:System.Globalization.DateTimeStyles.None" />.</param>
		/// <returns>An object that is equivalent to the date and time contained in <paramref name="s" />, as specified by <paramref name="provider" /> and <paramref name="styles" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> does not contain a valid string representation of a date and time.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="styles" /> contains an invalid combination of <see cref="T:System.Globalization.DateTimeStyles" /> values. For example, both <see cref="F:System.Globalization.DateTimeStyles.AssumeLocal" /> and <see cref="F:System.Globalization.DateTimeStyles.AssumeUniversal" />.</exception>
		// Token: 0x06000635 RID: 1589 RVA: 0x000160D4 File Offset: 0x000142D4
		[__DynamicallyInvokable]
		public static DateTime Parse(string s, IFormatProvider provider, DateTimeStyles styles)
		{
			DateTimeFormatInfo.ValidateStyles(styles, "styles");
			return DateTimeParse.Parse(s, DateTimeFormatInfo.GetInstance(provider), styles);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent using the specified format and culture-specific format information. The format of the string representation must match the specified format exactly.</summary>
		/// <param name="s">A string that contains a date and time to convert.</param>
		/// <param name="format">A format specifier that defines the required format of <paramref name="s" />. For more information, see the Remarks section.</param>
		/// <param name="provider">An object that supplies culture-specific format information about <paramref name="s" />.</param>
		/// <returns>An object that is equivalent to the date and time contained in <paramref name="s" />, as specified by <paramref name="format" /> and <paramref name="provider" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> or <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> or <paramref name="format" /> is an empty string.  
		/// -or-  
		/// <paramref name="s" /> does not contain a date and time that corresponds to the pattern specified in <paramref name="format" />.  
		/// -or-  
		/// The hour component and the AM/PM designator in <paramref name="s" /> do not agree.</exception>
		// Token: 0x06000636 RID: 1590 RVA: 0x000160EE File Offset: 0x000142EE
		[__DynamicallyInvokable]
		public static DateTime ParseExact(string s, string format, IFormatProvider provider)
		{
			return DateTimeParse.ParseExact(s, format, DateTimeFormatInfo.GetInstance(provider), DateTimeStyles.None);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent using the specified format, culture-specific format information, and style. The format of the string representation must match the specified format exactly or an exception is thrown.</summary>
		/// <param name="s">A string containing a date and time to convert.</param>
		/// <param name="format">A format specifier that defines the required format of <paramref name="s" />. For more information, see the Remarks section.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />.</param>
		/// <param name="style">A bitwise combination of the enumeration values that provides additional information about <paramref name="s" />, about style elements that may be present in <paramref name="s" />, or about the conversion from <paramref name="s" /> to a <see cref="T:System.DateTime" /> value. A typical value to specify is <see cref="F:System.Globalization.DateTimeStyles.None" />.</param>
		/// <returns>An object that is equivalent to the date and time contained in <paramref name="s" />, as specified by <paramref name="format" />, <paramref name="provider" />, and <paramref name="style" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> or <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> or <paramref name="format" /> is an empty string.  
		/// -or-  
		/// <paramref name="s" /> does not contain a date and time that corresponds to the pattern specified in <paramref name="format" />.  
		/// -or-  
		/// The hour component and the AM/PM designator in <paramref name="s" /> do not agree.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> contains an invalid combination of <see cref="T:System.Globalization.DateTimeStyles" /> values. For example, both <see cref="F:System.Globalization.DateTimeStyles.AssumeLocal" /> and <see cref="F:System.Globalization.DateTimeStyles.AssumeUniversal" />.</exception>
		// Token: 0x06000637 RID: 1591 RVA: 0x000160FE File Offset: 0x000142FE
		[__DynamicallyInvokable]
		public static DateTime ParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			return DateTimeParse.ParseExact(s, format, DateTimeFormatInfo.GetInstance(provider), style);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent using the specified array of formats, culture-specific format information, and style. The format of the string representation must match at least one of the specified formats exactly or an exception is thrown.</summary>
		/// <param name="s">A string that contains a date and time to convert.</param>
		/// <param name="formats">An array of allowable formats of <paramref name="s" />. For more information, see the Remarks section.</param>
		/// <param name="provider">An object that supplies culture-specific format information about <paramref name="s" />.</param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.DateTimeStyles.None" />.</param>
		/// <returns>An object that is equivalent to the date and time contained in <paramref name="s" />, as specified by <paramref name="formats" />, <paramref name="provider" />, and <paramref name="style" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> or <paramref name="formats" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is an empty string.  
		/// -or-  
		/// an element of <paramref name="formats" /> is an empty string.  
		/// -or-  
		/// <paramref name="s" /> does not contain a date and time that corresponds to any element of <paramref name="formats" />.  
		/// -or-  
		/// The hour component and the AM/PM designator in <paramref name="s" /> do not agree.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> contains an invalid combination of <see cref="T:System.Globalization.DateTimeStyles" /> values. For example, both <see cref="F:System.Globalization.DateTimeStyles.AssumeLocal" /> and <see cref="F:System.Globalization.DateTimeStyles.AssumeUniversal" />.</exception>
		// Token: 0x06000638 RID: 1592 RVA: 0x00016119 File Offset: 0x00014319
		[__DynamicallyInvokable]
		public static DateTime ParseExact(string s, string[] formats, IFormatProvider provider, DateTimeStyles style)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			return DateTimeParse.ParseExactMultiple(s, formats, DateTimeFormatInfo.GetInstance(provider), style);
		}

		/// <summary>Subtracts the specified date and time from this instance.</summary>
		/// <param name="value">The date and time value to subtract.</param>
		/// <returns>A time interval that is equal to the date and time represented by this instance minus the date and time represented by <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The result is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />.</exception>
		// Token: 0x06000639 RID: 1593 RVA: 0x00016134 File Offset: 0x00014334
		[__DynamicallyInvokable]
		public TimeSpan Subtract(DateTime value)
		{
			return new TimeSpan(this.InternalTicks - value.InternalTicks);
		}

		/// <summary>Subtracts the specified duration from this instance.</summary>
		/// <param name="value">The time interval to subtract.</param>
		/// <returns>An object that is equal to the date and time represented by this instance minus the time interval represented by <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The result is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />.</exception>
		// Token: 0x0600063A RID: 1594 RVA: 0x0001614C File Offset: 0x0001434C
		[__DynamicallyInvokable]
		public DateTime Subtract(TimeSpan value)
		{
			long internalTicks = this.InternalTicks;
			long ticks = value._ticks;
			if (internalTicks < ticks || internalTicks - 3155378975999999999L > ticks)
			{
				throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_DateArithmetic"));
			}
			return new DateTime((ulong)((internalTicks - ticks) | (long)this.InternalKind));
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x000161A0 File Offset: 0x000143A0
		private static double TicksToOADate(long value)
		{
			if (value == 0L)
			{
				return 0.0;
			}
			if (value < 864000000000L)
			{
				value += 599264352000000000L;
			}
			if (value < 31241376000000000L)
			{
				throw new OverflowException(Environment.GetResourceString("Arg_OleAutDateInvalid"));
			}
			long num = (value - 599264352000000000L) / 10000L;
			if (num < 0L)
			{
				long num2 = num % 86400000L;
				if (num2 != 0L)
				{
					num -= (86400000L + num2) * 2L;
				}
			}
			return (double)num / 86400000.0;
		}

		/// <summary>Converts the value of this instance to the equivalent OLE Automation date.</summary>
		/// <returns>A double-precision floating-point number that contains an OLE Automation date equivalent to the value of this instance.</returns>
		/// <exception cref="T:System.OverflowException">The value of this instance cannot be represented as an OLE Automation Date.</exception>
		// Token: 0x0600063C RID: 1596 RVA: 0x0001622D File Offset: 0x0001442D
		[__DynamicallyInvokable]
		public double ToOADate()
		{
			return DateTime.TicksToOADate(this.InternalTicks);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to a Windows file time.</summary>
		/// <returns>The value of the current <see cref="T:System.DateTime" /> object expressed as a Windows file time.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting file time would represent a date and time before 12:00 midnight January 1, 1601 C.E. UTC.</exception>
		// Token: 0x0600063D RID: 1597 RVA: 0x0001623C File Offset: 0x0001443C
		[__DynamicallyInvokable]
		public long ToFileTime()
		{
			return this.ToUniversalTime().ToFileTimeUtc();
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to a Windows file time.</summary>
		/// <returns>The value of the current <see cref="T:System.DateTime" /> object expressed as a Windows file time.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting file time would represent a date and time before 12:00 midnight January 1, 1601 C.E. UTC.</exception>
		// Token: 0x0600063E RID: 1598 RVA: 0x00016258 File Offset: 0x00014458
		[__DynamicallyInvokable]
		public long ToFileTimeUtc()
		{
			long num = (((this.InternalKind & 9223372036854775808UL) != 0UL) ? this.ToUniversalTime().InternalTicks : this.InternalTicks);
			if (DateTime.s_isLeapSecondsSupportedSystem)
			{
				return DateTime.InternalToFileTime(num);
			}
			num -= 504911232000000000L;
			if (num < 0L)
			{
				throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("ArgumentOutOfRange_FileTimeInvalid"));
			}
			return num;
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to local time.</summary>
		/// <returns>An object whose <see cref="P:System.DateTime.Kind" /> property is <see cref="F:System.DateTimeKind.Local" />, and whose value is the local time equivalent to the value of the current <see cref="T:System.DateTime" /> object, or <see cref="F:System.DateTime.MaxValue" /> if the converted value is too large to be represented by a <see cref="T:System.DateTime" /> object, or <see cref="F:System.DateTime.MinValue" /> if the converted value is too small to be represented as a <see cref="T:System.DateTime" /> object.</returns>
		// Token: 0x0600063F RID: 1599 RVA: 0x000162BF File Offset: 0x000144BF
		[__DynamicallyInvokable]
		public DateTime ToLocalTime()
		{
			return this.ToLocalTime(false);
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x000162C8 File Offset: 0x000144C8
		internal DateTime ToLocalTime(bool throwOnOverflow)
		{
			if (this.Kind == DateTimeKind.Local)
			{
				return this;
			}
			bool flag = false;
			bool flag2 = false;
			long ticks = TimeZoneInfo.GetUtcOffsetFromUtc(this, TimeZoneInfo.Local, out flag, out flag2).Ticks;
			long num = this.Ticks + ticks;
			if (num > 3155378975999999999L)
			{
				if (throwOnOverflow)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_ArgumentOutOfRangeException"));
				}
				return new DateTime(3155378975999999999L, DateTimeKind.Local);
			}
			else
			{
				if (num >= 0L)
				{
					return new DateTime(num, DateTimeKind.Local, flag2);
				}
				if (throwOnOverflow)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_ArgumentOutOfRangeException"));
				}
				return new DateTime(0L, DateTimeKind.Local);
			}
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to its equivalent long date string representation.</summary>
		/// <returns>A string that contains the long date string representation of the current <see cref="T:System.DateTime" /> object.</returns>
		// Token: 0x06000641 RID: 1601 RVA: 0x00016368 File Offset: 0x00014568
		public string ToLongDateString()
		{
			return DateTimeFormat.Format(this, "D", DateTimeFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to its equivalent long time string representation.</summary>
		/// <returns>A string that contains the long time string representation of the current <see cref="T:System.DateTime" /> object.</returns>
		// Token: 0x06000642 RID: 1602 RVA: 0x0001637F File Offset: 0x0001457F
		public string ToLongTimeString()
		{
			return DateTimeFormat.Format(this, "T", DateTimeFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to its equivalent short date string representation.</summary>
		/// <returns>A string that contains the short date string representation of the current <see cref="T:System.DateTime" /> object.</returns>
		// Token: 0x06000643 RID: 1603 RVA: 0x00016396 File Offset: 0x00014596
		public string ToShortDateString()
		{
			return DateTimeFormat.Format(this, "d", DateTimeFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to its equivalent short time string representation.</summary>
		/// <returns>A string that contains the short time string representation of the current <see cref="T:System.DateTime" /> object.</returns>
		// Token: 0x06000644 RID: 1604 RVA: 0x000163AD File Offset: 0x000145AD
		public string ToShortTimeString()
		{
			return DateTimeFormat.Format(this, "t", DateTimeFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to its equivalent string representation using the formatting conventions of the current culture.</summary>
		/// <returns>A string representation of the value of the current <see cref="T:System.DateTime" /> object.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The date and time is outside the range of dates supported by the calendar used by the current culture.</exception>
		// Token: 0x06000645 RID: 1605 RVA: 0x000163C4 File Offset: 0x000145C4
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return DateTimeFormat.Format(this, null, DateTimeFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to its equivalent string representation using the specified format and the formatting conventions of the current culture.</summary>
		/// <param name="format">A standard or custom date and time format string.</param>
		/// <returns>A string representation of value of the current <see cref="T:System.DateTime" /> object as specified by <paramref name="format" />.</returns>
		/// <exception cref="T:System.FormatException">The length of <paramref name="format" /> is 1, and it is not one of the format specifier characters defined for <see cref="T:System.Globalization.DateTimeFormatInfo" />.  
		///  -or-  
		///  <paramref name="format" /> does not contain a valid custom format pattern.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The date and time is outside the range of dates supported by the calendar used by the current culture.</exception>
		// Token: 0x06000646 RID: 1606 RVA: 0x000163D7 File Offset: 0x000145D7
		[__DynamicallyInvokable]
		public string ToString(string format)
		{
			return DateTimeFormat.Format(this, format, DateTimeFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to its equivalent string representation using the specified culture-specific format information.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>A string representation of value of the current <see cref="T:System.DateTime" /> object as specified by <paramref name="provider" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The date and time is outside the range of dates supported by the calendar used by <paramref name="provider" />.</exception>
		// Token: 0x06000647 RID: 1607 RVA: 0x000163EA File Offset: 0x000145EA
		[__DynamicallyInvokable]
		public string ToString(IFormatProvider provider)
		{
			return DateTimeFormat.Format(this, null, DateTimeFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to its equivalent string representation using the specified format and culture-specific format information.</summary>
		/// <param name="format">A standard or custom date and time format string.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>A string representation of value of the current <see cref="T:System.DateTime" /> object as specified by <paramref name="format" /> and <paramref name="provider" />.</returns>
		/// <exception cref="T:System.FormatException">The length of <paramref name="format" /> is 1, and it is not one of the format specifier characters defined for <see cref="T:System.Globalization.DateTimeFormatInfo" />.  
		///  -or-  
		///  <paramref name="format" /> does not contain a valid custom format pattern.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The date and time is outside the range of dates supported by the calendar used by <paramref name="provider" />.</exception>
		// Token: 0x06000648 RID: 1608 RVA: 0x000163FE File Offset: 0x000145FE
		[__DynamicallyInvokable]
		public string ToString(string format, IFormatProvider provider)
		{
			return DateTimeFormat.Format(this, format, DateTimeFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the value of the current <see cref="T:System.DateTime" /> object to Coordinated Universal Time (UTC).</summary>
		/// <returns>An object whose <see cref="P:System.DateTime.Kind" /> property is <see cref="F:System.DateTimeKind.Utc" />, and whose value is the UTC equivalent to the value of the current <see cref="T:System.DateTime" /> object, or <see cref="F:System.DateTime.MaxValue" /> if the converted value is too large to be represented by a <see cref="T:System.DateTime" /> object, or <see cref="F:System.DateTime.MinValue" /> if the converted value is too small to be represented by a <see cref="T:System.DateTime" /> object.</returns>
		// Token: 0x06000649 RID: 1609 RVA: 0x00016412 File Offset: 0x00014612
		[__DynamicallyInvokable]
		public DateTime ToUniversalTime()
		{
			return TimeZoneInfo.ConvertTimeToUtc(this, TimeZoneInfoOptions.NoThrowOnInvalidTime);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent and returns a value that indicates whether the conversion succeeded.</summary>
		/// <param name="s">A string containing a date and time to convert.</param>
		/// <param name="result">When this method returns, contains the <see cref="T:System.DateTime" /> value equivalent to the date and time contained in <paramref name="s" />, if the conversion succeeded, or <see cref="F:System.DateTime.MinValue" /> if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is <see langword="null" />, is an empty string (""), or does not contain a valid string representation of a date and time. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="s" /> parameter was converted successfully; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600064A RID: 1610 RVA: 0x00016420 File Offset: 0x00014620
		[__DynamicallyInvokable]
		public static bool TryParse(string s, out DateTime result)
		{
			return DateTimeParse.TryParse(s, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out result);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent using the specified culture-specific format information and formatting style, and returns a value that indicates whether the conversion succeeded.</summary>
		/// <param name="s">A string containing a date and time to convert.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />.</param>
		/// <param name="styles">A bitwise combination of enumeration values that defines how to interpret the parsed date in relation to the current time zone or the current date. A typical value to specify is <see cref="F:System.Globalization.DateTimeStyles.None" />.</param>
		/// <param name="result">When this method returns, contains the <see cref="T:System.DateTime" /> value equivalent to the date and time contained in <paramref name="s" />, if the conversion succeeded, or <see cref="F:System.DateTime.MinValue" /> if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is <see langword="null" />, is an empty string (""), or does not contain a valid string representation of a date and time. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="s" /> parameter was converted successfully; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="styles" /> is not a valid <see cref="T:System.Globalization.DateTimeStyles" /> value.  
		/// -or-  
		/// <paramref name="styles" /> contains an invalid combination of <see cref="T:System.Globalization.DateTimeStyles" /> values (for example, both <see cref="F:System.Globalization.DateTimeStyles.AssumeLocal" /> and <see cref="F:System.Globalization.DateTimeStyles.AssumeUniversal" />).</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="provider" /> is a neutral culture and cannot be used in a parsing operation.</exception>
		// Token: 0x0600064B RID: 1611 RVA: 0x0001642F File Offset: 0x0001462F
		[__DynamicallyInvokable]
		public static bool TryParse(string s, IFormatProvider provider, DateTimeStyles styles, out DateTime result)
		{
			DateTimeFormatInfo.ValidateStyles(styles, "styles");
			return DateTimeParse.TryParse(s, DateTimeFormatInfo.GetInstance(provider), styles, out result);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent using the specified format, culture-specific format information, and style. The format of the string representation must match the specified format exactly. The method returns a value that indicates whether the conversion succeeded.</summary>
		/// <param name="s">A string containing a date and time to convert.</param>
		/// <param name="format">The required format of <paramref name="s" />.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about <paramref name="s" />.</param>
		/// <param name="style">A bitwise combination of one or more enumeration values that indicate the permitted format of <paramref name="s" />.</param>
		/// <param name="result">When this method returns, contains the <see cref="T:System.DateTime" /> value equivalent to the date and time contained in <paramref name="s" />, if the conversion succeeded, or <see cref="F:System.DateTime.MinValue" /> if the conversion failed. The conversion fails if either the <paramref name="s" /> or <paramref name="format" /> parameter is <see langword="null" />, is an empty string, or does not contain a date and time that correspond to the pattern specified in <paramref name="format" />. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="s" /> was converted successfully; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="styles" /> is not a valid <see cref="T:System.Globalization.DateTimeStyles" /> value.  
		/// -or-  
		/// <paramref name="styles" /> contains an invalid combination of <see cref="T:System.Globalization.DateTimeStyles" /> values (for example, both <see cref="F:System.Globalization.DateTimeStyles.AssumeLocal" /> and <see cref="F:System.Globalization.DateTimeStyles.AssumeUniversal" />).</exception>
		// Token: 0x0600064C RID: 1612 RVA: 0x0001644A File Offset: 0x0001464A
		[__DynamicallyInvokable]
		public static bool TryParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style, out DateTime result)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			return DateTimeParse.TryParseExact(s, format, DateTimeFormatInfo.GetInstance(provider), style, out result);
		}

		/// <summary>Converts the specified string representation of a date and time to its <see cref="T:System.DateTime" /> equivalent using the specified array of formats, culture-specific format information, and style. The format of the string representation must match at least one of the specified formats exactly. The method returns a value that indicates whether the conversion succeeded.</summary>
		/// <param name="s">A string that contains a date and time to convert.</param>
		/// <param name="formats">An array of allowable formats of <paramref name="s" />.</param>
		/// <param name="provider">An object that supplies culture-specific format information about <paramref name="s" />.</param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.DateTimeStyles.None" />.</param>
		/// <param name="result">When this method returns, contains the <see cref="T:System.DateTime" /> value equivalent to the date and time contained in <paramref name="s" />, if the conversion succeeded, or <see cref="F:System.DateTime.MinValue" /> if the conversion failed. The conversion fails if <paramref name="s" /> or <paramref name="formats" /> is <see langword="null" />, <paramref name="s" /> or an element of <paramref name="formats" /> is an empty string, or the format of <paramref name="s" /> is not exactly as specified by at least one of the format patterns in <paramref name="formats" />. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="s" /> parameter was converted successfully; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="styles" /> is not a valid <see cref="T:System.Globalization.DateTimeStyles" /> value.  
		/// -or-  
		/// <paramref name="styles" /> contains an invalid combination of <see cref="T:System.Globalization.DateTimeStyles" /> values (for example, both <see cref="F:System.Globalization.DateTimeStyles.AssumeLocal" /> and <see cref="F:System.Globalization.DateTimeStyles.AssumeUniversal" />).</exception>
		// Token: 0x0600064D RID: 1613 RVA: 0x00016467 File Offset: 0x00014667
		[__DynamicallyInvokable]
		public static bool TryParseExact(string s, string[] formats, IFormatProvider provider, DateTimeStyles style, out DateTime result)
		{
			DateTimeFormatInfo.ValidateStyles(style, "style");
			return DateTimeParse.TryParseExactMultiple(s, formats, DateTimeFormatInfo.GetInstance(provider), style, out result);
		}

		/// <summary>Adds a specified time interval to a specified date and time, yielding a new date and time.</summary>
		/// <param name="d">The date and time value to add.</param>
		/// <param name="t">The time interval to add.</param>
		/// <returns>An object that is the sum of the values of <paramref name="d" /> and <paramref name="t" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />.</exception>
		// Token: 0x0600064E RID: 1614 RVA: 0x00016484 File Offset: 0x00014684
		[__DynamicallyInvokable]
		public static DateTime operator +(DateTime d, TimeSpan t)
		{
			long internalTicks = d.InternalTicks;
			long ticks = t._ticks;
			if (ticks > 3155378975999999999L - internalTicks || ticks < 0L - internalTicks)
			{
				throw new ArgumentOutOfRangeException("t", Environment.GetResourceString("ArgumentOutOfRange_DateArithmetic"));
			}
			return new DateTime((ulong)((internalTicks + ticks) | (long)d.InternalKind));
		}

		/// <summary>Subtracts a specified time interval from a specified date and time and returns a new date and time.</summary>
		/// <param name="d">The date and time value to subtract from.</param>
		/// <param name="t">The time interval to subtract.</param>
		/// <returns>An object whose value is the value of <paramref name="d" /> minus the value of <paramref name="t" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The resulting <see cref="T:System.DateTime" /> is less than <see cref="F:System.DateTime.MinValue" /> or greater than <see cref="F:System.DateTime.MaxValue" />.</exception>
		// Token: 0x0600064F RID: 1615 RVA: 0x000164DC File Offset: 0x000146DC
		[__DynamicallyInvokable]
		public static DateTime operator -(DateTime d, TimeSpan t)
		{
			long internalTicks = d.InternalTicks;
			long ticks = t._ticks;
			if (internalTicks < ticks || internalTicks - 3155378975999999999L > ticks)
			{
				throw new ArgumentOutOfRangeException("t", Environment.GetResourceString("ArgumentOutOfRange_DateArithmetic"));
			}
			return new DateTime((ulong)((internalTicks - ticks) | (long)d.InternalKind));
		}

		/// <summary>Subtracts a specified date and time from another specified date and time and returns a time interval.</summary>
		/// <param name="d1">The date and time value to subtract from (the minuend).</param>
		/// <param name="d2">The date and time value to subtract (the subtrahend).</param>
		/// <returns>The time interval between <paramref name="d1" /> and <paramref name="d2" />; that is, <paramref name="d1" /> minus <paramref name="d2" />.</returns>
		// Token: 0x06000650 RID: 1616 RVA: 0x0001652F File Offset: 0x0001472F
		[__DynamicallyInvokable]
		public static TimeSpan operator -(DateTime d1, DateTime d2)
		{
			return new TimeSpan(d1.InternalTicks - d2.InternalTicks);
		}

		/// <summary>Determines whether two specified instances of <see cref="T:System.DateTime" /> are equal.</summary>
		/// <param name="d1">The first object to compare.</param>
		/// <param name="d2">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="d1" /> and <paramref name="d2" /> represent the same date and time; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000651 RID: 1617 RVA: 0x00016545 File Offset: 0x00014745
		[__DynamicallyInvokable]
		public static bool operator ==(DateTime d1, DateTime d2)
		{
			return d1.InternalTicks == d2.InternalTicks;
		}

		/// <summary>Determines whether two specified instances of <see cref="T:System.DateTime" /> are not equal.</summary>
		/// <param name="d1">The first object to compare.</param>
		/// <param name="d2">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="d1" /> and <paramref name="d2" /> do not represent the same date and time; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000652 RID: 1618 RVA: 0x00016557 File Offset: 0x00014757
		[__DynamicallyInvokable]
		public static bool operator !=(DateTime d1, DateTime d2)
		{
			return d1.InternalTicks != d2.InternalTicks;
		}

		/// <summary>Determines whether one specified <see cref="T:System.DateTime" /> is earlier than another specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="t1">The first object to compare.</param>
		/// <param name="t2">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="t1" /> is earlier than <paramref name="t2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000653 RID: 1619 RVA: 0x0001656C File Offset: 0x0001476C
		[__DynamicallyInvokable]
		public static bool operator <(DateTime t1, DateTime t2)
		{
			return t1.InternalTicks < t2.InternalTicks;
		}

		/// <summary>Determines whether one specified <see cref="T:System.DateTime" /> represents a date and time that is the same as or earlier than another specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="t1">The first object to compare.</param>
		/// <param name="t2">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="t1" /> is the same as or earlier than <paramref name="t2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000654 RID: 1620 RVA: 0x0001657E File Offset: 0x0001477E
		[__DynamicallyInvokable]
		public static bool operator <=(DateTime t1, DateTime t2)
		{
			return t1.InternalTicks <= t2.InternalTicks;
		}

		/// <summary>Determines whether one specified <see cref="T:System.DateTime" /> is later than another specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="t1">The first object to compare.</param>
		/// <param name="t2">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="t1" /> is later than <paramref name="t2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000655 RID: 1621 RVA: 0x00016593 File Offset: 0x00014793
		[__DynamicallyInvokable]
		public static bool operator >(DateTime t1, DateTime t2)
		{
			return t1.InternalTicks > t2.InternalTicks;
		}

		/// <summary>Determines whether one specified <see cref="T:System.DateTime" /> represents a date and time that is the same as or later than another specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="t1">The first object to compare.</param>
		/// <param name="t2">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="t1" /> is the same as or later than <paramref name="t2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000656 RID: 1622 RVA: 0x000165A5 File Offset: 0x000147A5
		[__DynamicallyInvokable]
		public static bool operator >=(DateTime t1, DateTime t2)
		{
			return t1.InternalTicks >= t2.InternalTicks;
		}

		/// <summary>Converts the value of this instance to all the string representations supported by the standard date and time format specifiers.</summary>
		/// <returns>A string array where each element is the representation of the value of this instance formatted with one of the standard date and time format specifiers.</returns>
		// Token: 0x06000657 RID: 1623 RVA: 0x000165BA File Offset: 0x000147BA
		[__DynamicallyInvokable]
		public string[] GetDateTimeFormats()
		{
			return this.GetDateTimeFormats(CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the value of this instance to all the string representations supported by the standard date and time format specifiers and the specified culture-specific formatting information.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information about this instance.</param>
		/// <returns>A string array where each element is the representation of the value of this instance formatted with one of the standard date and time format specifiers.</returns>
		// Token: 0x06000658 RID: 1624 RVA: 0x000165C7 File Offset: 0x000147C7
		[__DynamicallyInvokable]
		public string[] GetDateTimeFormats(IFormatProvider provider)
		{
			return DateTimeFormat.GetAllDateTimes(this, DateTimeFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the value of this instance to all the string representations supported by the specified standard date and time format specifier.</summary>
		/// <param name="format">A standard date and time format string.</param>
		/// <returns>A string array where each element is the representation of the value of this instance formatted with the <paramref name="format" /> standard date and time format specifier.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is not a valid standard date and time format specifier character.</exception>
		// Token: 0x06000659 RID: 1625 RVA: 0x000165DA File Offset: 0x000147DA
		[__DynamicallyInvokable]
		public string[] GetDateTimeFormats(char format)
		{
			return this.GetDateTimeFormats(format, CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the value of this instance to all the string representations supported by the specified standard date and time format specifier and culture-specific formatting information.</summary>
		/// <param name="format">A date and time format string.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about this instance.</param>
		/// <returns>A string array where each element is the representation of the value of this instance formatted with one of the standard date and time format specifiers.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is not a valid standard date and time format specifier character.</exception>
		// Token: 0x0600065A RID: 1626 RVA: 0x000165E8 File Offset: 0x000147E8
		[__DynamicallyInvokable]
		public string[] GetDateTimeFormats(char format, IFormatProvider provider)
		{
			return DateTimeFormat.GetAllDateTimes(this, format, DateTimeFormatInfo.GetInstance(provider));
		}

		/// <summary>Returns the <see cref="T:System.TypeCode" /> for value type <see cref="T:System.DateTime" />.</summary>
		/// <returns>The enumerated constant, <see cref="F:System.TypeCode.DateTime" />.</returns>
		// Token: 0x0600065B RID: 1627 RVA: 0x000165FC File Offset: 0x000147FC
		public TypeCode GetTypeCode()
		{
			return TypeCode.DateTime;
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x0600065C RID: 1628 RVA: 0x00016600 File Offset: 0x00014800
		[__DynamicallyInvokable]
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "DateTime", "Boolean" }));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x0600065D RID: 1629 RVA: 0x00016627 File Offset: 0x00014827
		[__DynamicallyInvokable]
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "DateTime", "Char" }));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x0600065E RID: 1630 RVA: 0x0001664E File Offset: 0x0001484E
		[__DynamicallyInvokable]
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "DateTime", "SByte" }));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x0600065F RID: 1631 RVA: 0x00016675 File Offset: 0x00014875
		[__DynamicallyInvokable]
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "DateTime", "Byte" }));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000660 RID: 1632 RVA: 0x0001669C File Offset: 0x0001489C
		[__DynamicallyInvokable]
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "DateTime", "Int16" }));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000661 RID: 1633 RVA: 0x000166C3 File Offset: 0x000148C3
		[__DynamicallyInvokable]
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "DateTime", "UInt16" }));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000662 RID: 1634 RVA: 0x000166EA File Offset: 0x000148EA
		[__DynamicallyInvokable]
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "DateTime", "Int32" }));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000663 RID: 1635 RVA: 0x00016711 File Offset: 0x00014911
		[__DynamicallyInvokable]
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "DateTime", "UInt32" }));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000664 RID: 1636 RVA: 0x00016738 File Offset: 0x00014938
		[__DynamicallyInvokable]
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "DateTime", "Int64" }));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000665 RID: 1637 RVA: 0x0001675F File Offset: 0x0001495F
		[__DynamicallyInvokable]
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "DateTime", "UInt64" }));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000666 RID: 1638 RVA: 0x00016786 File Offset: 0x00014986
		[__DynamicallyInvokable]
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "DateTime", "Single" }));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000667 RID: 1639 RVA: 0x000167AD File Offset: 0x000149AD
		[__DynamicallyInvokable]
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "DateTime", "Double" }));
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>The return value for this member is not used.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06000668 RID: 1640 RVA: 0x000167D4 File Offset: 0x000149D4
		[__DynamicallyInvokable]
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("InvalidCast_FromTo", new object[] { "DateTime", "Decimal" }));
		}

		/// <summary>Returns the current <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>The current object.</returns>
		// Token: 0x06000669 RID: 1641 RVA: 0x000167FB File Offset: 0x000149FB
		[__DynamicallyInvokable]
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return this;
		}

		/// <summary>Converts the current <see cref="T:System.DateTime" /> object to an object of a specified type.</summary>
		/// <param name="type">The desired type.</param>
		/// <param name="provider">An object that implements the <see cref="T:System.IFormatProvider" /> interface. (This parameter is not used; specify <see langword="null" />.)</param>
		/// <returns>An object of the type specified by the <paramref name="type" /> parameter, with a value equivalent to the current <see cref="T:System.DateTime" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported for the <see cref="T:System.DateTime" /> type.</exception>
		// Token: 0x0600066A RID: 1642 RVA: 0x00016803 File Offset: 0x00014A03
		[__DynamicallyInvokable]
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00016818 File Offset: 0x00014A18
		internal static bool TryCreate(int year, int month, int day, int hour, int minute, int second, int millisecond, out DateTime result)
		{
			result = DateTime.MinValue;
			if (year < 1 || year > 9999 || month < 1 || month > 12)
			{
				return false;
			}
			int[] array = (DateTime.IsLeapYear(year) ? DateTime.DaysToMonth366 : DateTime.DaysToMonth365);
			if (day < 1 || day > array[month] - array[month - 1])
			{
				return false;
			}
			if (hour < 0 || hour >= 24 || minute < 0 || minute >= 60 || second < 0 || second > 60)
			{
				return false;
			}
			if (millisecond < 0 || millisecond >= 1000)
			{
				return false;
			}
			if (second == 60)
			{
				if (!DateTime.s_isLeapSecondsSupportedSystem || !DateTime.IsValidTimeWithLeapSeconds(year, month, day, hour, minute, second, DateTimeKind.Unspecified))
				{
					return false;
				}
				second = 59;
			}
			long num = DateTime.DateToTicks(year, month, day) + DateTime.TimeToTicks(hour, minute, second);
			num += (long)millisecond * 10000L;
			if (num < 0L || num > 3155378975999999999L)
			{
				return false;
			}
			result = new DateTime(num, DateTimeKind.Unspecified);
			return true;
		}

		// Token: 0x040002B8 RID: 696
		private const long TicksPerMillisecond = 10000L;

		// Token: 0x040002B9 RID: 697
		private const long TicksPerSecond = 10000000L;

		// Token: 0x040002BA RID: 698
		private const long TicksPerMinute = 600000000L;

		// Token: 0x040002BB RID: 699
		private const long TicksPerHour = 36000000000L;

		// Token: 0x040002BC RID: 700
		private const long TicksPerDay = 864000000000L;

		// Token: 0x040002BD RID: 701
		private const int MillisPerSecond = 1000;

		// Token: 0x040002BE RID: 702
		private const int MillisPerMinute = 60000;

		// Token: 0x040002BF RID: 703
		private const int MillisPerHour = 3600000;

		// Token: 0x040002C0 RID: 704
		private const int MillisPerDay = 86400000;

		// Token: 0x040002C1 RID: 705
		private const int DaysPerYear = 365;

		// Token: 0x040002C2 RID: 706
		private const int DaysPer4Years = 1461;

		// Token: 0x040002C3 RID: 707
		private const int DaysPer100Years = 36524;

		// Token: 0x040002C4 RID: 708
		private const int DaysPer400Years = 146097;

		// Token: 0x040002C5 RID: 709
		private const int DaysTo1601 = 584388;

		// Token: 0x040002C6 RID: 710
		private const int DaysTo1899 = 693593;

		// Token: 0x040002C7 RID: 711
		internal const int DaysTo1970 = 719162;

		// Token: 0x040002C8 RID: 712
		private const int DaysTo10000 = 3652059;

		// Token: 0x040002C9 RID: 713
		internal const long MinTicks = 0L;

		// Token: 0x040002CA RID: 714
		internal const long MaxTicks = 3155378975999999999L;

		// Token: 0x040002CB RID: 715
		private const long MaxMillis = 315537897600000L;

		// Token: 0x040002CC RID: 716
		private const long FileTimeOffset = 504911232000000000L;

		// Token: 0x040002CD RID: 717
		private const long DoubleDateOffset = 599264352000000000L;

		// Token: 0x040002CE RID: 718
		private const long OADateMinAsTicks = 31241376000000000L;

		// Token: 0x040002CF RID: 719
		private const double OADateMinAsDouble = -657435.0;

		// Token: 0x040002D0 RID: 720
		private const double OADateMaxAsDouble = 2958466.0;

		// Token: 0x040002D1 RID: 721
		private const int DatePartYear = 0;

		// Token: 0x040002D2 RID: 722
		private const int DatePartDayOfYear = 1;

		// Token: 0x040002D3 RID: 723
		private const int DatePartMonth = 2;

		// Token: 0x040002D4 RID: 724
		private const int DatePartDay = 3;

		// Token: 0x040002D5 RID: 725
		internal static readonly bool s_isLeapSecondsSupportedSystem = DateTime.SystemSupportLeapSeconds();

		// Token: 0x040002D6 RID: 726
		private static readonly int[] DaysToMonth365 = new int[]
		{
			0, 31, 59, 90, 120, 151, 181, 212, 243, 273,
			304, 334, 365
		};

		// Token: 0x040002D7 RID: 727
		private static readonly int[] DaysToMonth366 = new int[]
		{
			0, 31, 60, 91, 121, 152, 182, 213, 244, 274,
			305, 335, 366
		};

		/// <summary>Represents the smallest possible value of <see cref="T:System.DateTime" />. This field is read-only.</summary>
		// Token: 0x040002D8 RID: 728
		[__DynamicallyInvokable]
		public static readonly DateTime MinValue = new DateTime(0L, DateTimeKind.Unspecified);

		/// <summary>Represents the largest possible value of <see cref="T:System.DateTime" />. This field is read-only.</summary>
		// Token: 0x040002D9 RID: 729
		[__DynamicallyInvokable]
		public static readonly DateTime MaxValue = new DateTime(3155378975999999999L, DateTimeKind.Unspecified);

		// Token: 0x040002DA RID: 730
		private const ulong TicksMask = 4611686018427387903UL;

		// Token: 0x040002DB RID: 731
		private const ulong FlagsMask = 13835058055282163712UL;

		// Token: 0x040002DC RID: 732
		private const ulong LocalMask = 9223372036854775808UL;

		// Token: 0x040002DD RID: 733
		private const long TicksCeiling = 4611686018427387904L;

		// Token: 0x040002DE RID: 734
		private const ulong KindUnspecified = 0UL;

		// Token: 0x040002DF RID: 735
		private const ulong KindUtc = 4611686018427387904UL;

		// Token: 0x040002E0 RID: 736
		private const ulong KindLocal = 9223372036854775808UL;

		// Token: 0x040002E1 RID: 737
		private const ulong KindLocalAmbiguousDst = 13835058055282163712UL;

		// Token: 0x040002E2 RID: 738
		private const int KindShift = 62;

		// Token: 0x040002E3 RID: 739
		private const string TicksField = "ticks";

		// Token: 0x040002E4 RID: 740
		private const string DateDataField = "dateData";

		// Token: 0x040002E5 RID: 741
		private ulong dateData;

		// Token: 0x02000AC7 RID: 2759
		internal struct FullSystemTime
		{
			// Token: 0x060069E4 RID: 27108 RVA: 0x0016E354 File Offset: 0x0016C554
			internal FullSystemTime(int year, int month, DayOfWeek dayOfWeek, int day, int hour, int minute, int second)
			{
				this.wYear = (ushort)year;
				this.wMonth = (ushort)month;
				this.wDayOfWeek = (ushort)dayOfWeek;
				this.wDay = (ushort)day;
				this.wHour = (ushort)hour;
				this.wMinute = (ushort)minute;
				this.wSecond = (ushort)second;
				this.wMillisecond = 0;
				this.hundredNanoSecond = 0L;
			}

			// Token: 0x060069E5 RID: 27109 RVA: 0x0016E3AC File Offset: 0x0016C5AC
			internal FullSystemTime(long ticks)
			{
				DateTime dateTime = new DateTime(ticks);
				int num;
				int num2;
				int num3;
				dateTime.GetDatePart(out num, out num2, out num3);
				this.wYear = (ushort)num;
				this.wMonth = (ushort)num2;
				this.wDayOfWeek = (ushort)dateTime.DayOfWeek;
				this.wDay = (ushort)num3;
				this.wHour = (ushort)dateTime.Hour;
				this.wMinute = (ushort)dateTime.Minute;
				this.wSecond = (ushort)dateTime.Second;
				this.wMillisecond = (ushort)dateTime.Millisecond;
				this.hundredNanoSecond = 0L;
			}

			// Token: 0x040030EB RID: 12523
			internal ushort wYear;

			// Token: 0x040030EC RID: 12524
			internal ushort wMonth;

			// Token: 0x040030ED RID: 12525
			internal ushort wDayOfWeek;

			// Token: 0x040030EE RID: 12526
			internal ushort wDay;

			// Token: 0x040030EF RID: 12527
			internal ushort wHour;

			// Token: 0x040030F0 RID: 12528
			internal ushort wMinute;

			// Token: 0x040030F1 RID: 12529
			internal ushort wSecond;

			// Token: 0x040030F2 RID: 12530
			internal ushort wMillisecond;

			// Token: 0x040030F3 RID: 12531
			internal long hundredNanoSecond;
		}
	}
}
