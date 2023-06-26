using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
	/// <summary>Represents a time interval.</summary>
	// Token: 0x02000143 RID: 323
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct TimeSpan : IComparable, IComparable<TimeSpan>, IEquatable<TimeSpan>, IFormattable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.TimeSpan" /> structure to the specified number of ticks.</summary>
		/// <param name="ticks">A time period expressed in 100-nanosecond units.</param>
		// Token: 0x06001345 RID: 4933 RVA: 0x0003882A File Offset: 0x00036A2A
		[__DynamicallyInvokable]
		public TimeSpan(long ticks)
		{
			this._ticks = ticks;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TimeSpan" /> structure to a specified number of hours, minutes, and seconds.</summary>
		/// <param name="hours">Number of hours.</param>
		/// <param name="minutes">Number of minutes.</param>
		/// <param name="seconds">Number of seconds.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The parameters specify a <see cref="T:System.TimeSpan" /> value less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.</exception>
		// Token: 0x06001346 RID: 4934 RVA: 0x00038833 File Offset: 0x00036A33
		[__DynamicallyInvokable]
		public TimeSpan(int hours, int minutes, int seconds)
		{
			this._ticks = TimeSpan.TimeToTicks(hours, minutes, seconds);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TimeSpan" /> structure to a specified number of days, hours, minutes, and seconds.</summary>
		/// <param name="days">Number of days.</param>
		/// <param name="hours">Number of hours.</param>
		/// <param name="minutes">Number of minutes.</param>
		/// <param name="seconds">Number of seconds.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The parameters specify a <see cref="T:System.TimeSpan" /> value less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.</exception>
		// Token: 0x06001347 RID: 4935 RVA: 0x00038843 File Offset: 0x00036A43
		[__DynamicallyInvokable]
		public TimeSpan(int days, int hours, int minutes, int seconds)
		{
			this = new TimeSpan(days, hours, minutes, seconds, 0);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.TimeSpan" /> structure to a specified number of days, hours, minutes, seconds, and milliseconds.</summary>
		/// <param name="days">Number of days.</param>
		/// <param name="hours">Number of hours.</param>
		/// <param name="minutes">Number of minutes.</param>
		/// <param name="seconds">Number of seconds.</param>
		/// <param name="milliseconds">Number of milliseconds.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The parameters specify a <see cref="T:System.TimeSpan" /> value less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.</exception>
		// Token: 0x06001348 RID: 4936 RVA: 0x00038854 File Offset: 0x00036A54
		[__DynamicallyInvokable]
		public TimeSpan(int days, int hours, int minutes, int seconds, int milliseconds)
		{
			long num = ((long)days * 3600L * 24L + (long)hours * 3600L + (long)minutes * 60L + (long)seconds) * 1000L + (long)milliseconds;
			if (num > 922337203685477L || num < -922337203685477L)
			{
				throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("Overflow_TimeSpanTooLong"));
			}
			this._ticks = num * 10000L;
		}

		/// <summary>Gets the number of ticks that represent the value of the current <see cref="T:System.TimeSpan" /> structure.</summary>
		/// <returns>The number of ticks contained in this instance.</returns>
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06001349 RID: 4937 RVA: 0x000388C6 File Offset: 0x00036AC6
		[__DynamicallyInvokable]
		public long Ticks
		{
			[__DynamicallyInvokable]
			get
			{
				return this._ticks;
			}
		}

		/// <summary>Gets the days component of the time interval represented by the current <see cref="T:System.TimeSpan" /> structure.</summary>
		/// <returns>The day component of this instance. The return value can be positive or negative.</returns>
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x000388CE File Offset: 0x00036ACE
		[__DynamicallyInvokable]
		public int Days
		{
			[__DynamicallyInvokable]
			get
			{
				return (int)(this._ticks / 864000000000L);
			}
		}

		/// <summary>Gets the hours component of the time interval represented by the current <see cref="T:System.TimeSpan" /> structure.</summary>
		/// <returns>The hour component of the current <see cref="T:System.TimeSpan" /> structure. The return value ranges from -23 through 23.</returns>
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600134B RID: 4939 RVA: 0x000388E1 File Offset: 0x00036AE1
		[__DynamicallyInvokable]
		public int Hours
		{
			[__DynamicallyInvokable]
			get
			{
				return (int)(this._ticks / 36000000000L % 24L);
			}
		}

		/// <summary>Gets the milliseconds component of the time interval represented by the current <see cref="T:System.TimeSpan" /> structure.</summary>
		/// <returns>The millisecond component of the current <see cref="T:System.TimeSpan" /> structure. The return value ranges from -999 through 999.</returns>
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x0600134C RID: 4940 RVA: 0x000388F8 File Offset: 0x00036AF8
		[__DynamicallyInvokable]
		public int Milliseconds
		{
			[__DynamicallyInvokable]
			get
			{
				return (int)(this._ticks / 10000L % 1000L);
			}
		}

		/// <summary>Gets the minutes component of the time interval represented by the current <see cref="T:System.TimeSpan" /> structure.</summary>
		/// <returns>The minute component of the current <see cref="T:System.TimeSpan" /> structure. The return value ranges from -59 through 59.</returns>
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600134D RID: 4941 RVA: 0x0003890F File Offset: 0x00036B0F
		[__DynamicallyInvokable]
		public int Minutes
		{
			[__DynamicallyInvokable]
			get
			{
				return (int)(this._ticks / 600000000L % 60L);
			}
		}

		/// <summary>Gets the seconds component of the time interval represented by the current <see cref="T:System.TimeSpan" /> structure.</summary>
		/// <returns>The second component of the current <see cref="T:System.TimeSpan" /> structure. The return value ranges from -59 through 59.</returns>
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600134E RID: 4942 RVA: 0x00038923 File Offset: 0x00036B23
		[__DynamicallyInvokable]
		public int Seconds
		{
			[__DynamicallyInvokable]
			get
			{
				return (int)(this._ticks / 10000000L % 60L);
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.TimeSpan" /> structure expressed in whole and fractional days.</summary>
		/// <returns>The total number of days represented by this instance.</returns>
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600134F RID: 4943 RVA: 0x00038937 File Offset: 0x00036B37
		[__DynamicallyInvokable]
		public double TotalDays
		{
			[__DynamicallyInvokable]
			get
			{
				return (double)this._ticks * 1.1574074074074074E-12;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.TimeSpan" /> structure expressed in whole and fractional hours.</summary>
		/// <returns>The total number of hours represented by this instance.</returns>
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06001350 RID: 4944 RVA: 0x0003894A File Offset: 0x00036B4A
		[__DynamicallyInvokable]
		public double TotalHours
		{
			[__DynamicallyInvokable]
			get
			{
				return (double)this._ticks * 2.7777777777777777E-11;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.TimeSpan" /> structure expressed in whole and fractional milliseconds.</summary>
		/// <returns>The total number of milliseconds represented by this instance.</returns>
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06001351 RID: 4945 RVA: 0x00038960 File Offset: 0x00036B60
		[__DynamicallyInvokable]
		public double TotalMilliseconds
		{
			[__DynamicallyInvokable]
			get
			{
				double num = (double)this._ticks * 0.0001;
				if (num > 922337203685477.0)
				{
					return 922337203685477.0;
				}
				if (num < -922337203685477.0)
				{
					return -922337203685477.0;
				}
				return num;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.TimeSpan" /> structure expressed in whole and fractional minutes.</summary>
		/// <returns>The total number of minutes represented by this instance.</returns>
		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06001352 RID: 4946 RVA: 0x000389AC File Offset: 0x00036BAC
		[__DynamicallyInvokable]
		public double TotalMinutes
		{
			[__DynamicallyInvokable]
			get
			{
				return (double)this._ticks * 1.6666666666666667E-09;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.TimeSpan" /> structure expressed in whole and fractional seconds.</summary>
		/// <returns>The total number of seconds represented by this instance.</returns>
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06001353 RID: 4947 RVA: 0x000389BF File Offset: 0x00036BBF
		[__DynamicallyInvokable]
		public double TotalSeconds
		{
			[__DynamicallyInvokable]
			get
			{
				return (double)this._ticks * 1E-07;
			}
		}

		/// <summary>Returns a new <see cref="T:System.TimeSpan" /> object whose value is the sum of the specified <see cref="T:System.TimeSpan" /> object and this instance.</summary>
		/// <param name="ts">The time interval to add.</param>
		/// <returns>A new object that represents the value of this instance plus the value of <paramref name="ts" />.</returns>
		/// <exception cref="T:System.OverflowException">The resulting <see cref="T:System.TimeSpan" /> is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.</exception>
		// Token: 0x06001354 RID: 4948 RVA: 0x000389D4 File Offset: 0x00036BD4
		[__DynamicallyInvokable]
		public TimeSpan Add(TimeSpan ts)
		{
			long num = this._ticks + ts._ticks;
			if (this._ticks >> 63 == ts._ticks >> 63 && this._ticks >> 63 != num >> 63)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_TimeSpanTooLong"));
			}
			return new TimeSpan(num);
		}

		/// <summary>Compares two <see cref="T:System.TimeSpan" /> values and returns an integer that indicates whether the first value is shorter than, equal to, or longer than the second value.</summary>
		/// <param name="t1">The first time interval to compare.</param>
		/// <param name="t2">The second time interval to compare.</param>
		/// <returns>One of the following values.  
		///   Value  
		///
		///   Description  
		///
		///   -1  
		///
		///  <paramref name="t1" /> is shorter than <paramref name="t2" />.  
		///
		///   0  
		///
		///  <paramref name="t1" /> is equal to <paramref name="t2" />.  
		///
		///   1  
		///
		///  <paramref name="t1" /> is longer than <paramref name="t2" />.</returns>
		// Token: 0x06001355 RID: 4949 RVA: 0x00038A28 File Offset: 0x00036C28
		[__DynamicallyInvokable]
		public static int Compare(TimeSpan t1, TimeSpan t2)
		{
			if (t1._ticks > t2._ticks)
			{
				return 1;
			}
			if (t1._ticks < t2._ticks)
			{
				return -1;
			}
			return 0;
		}

		/// <summary>Compares this instance to a specified object and returns an integer that indicates whether this instance is shorter than, equal to, or longer than the specified object.</summary>
		/// <param name="value">An object to compare, or <see langword="null" />.</param>
		/// <returns>One of the following values.  
		///   Value  
		///
		///   Description  
		///
		///   -1  
		///
		///   This instance is shorter than <paramref name="value" />.  
		///
		///   0  
		///
		///   This instance is equal to <paramref name="value" />.  
		///
		///   1  
		///
		///   This instance is longer than <paramref name="value" />.  
		///
		///  -or-  
		///
		///  <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.TimeSpan" />.</exception>
		// Token: 0x06001356 RID: 4950 RVA: 0x00038A4C File Offset: 0x00036C4C
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is TimeSpan))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_MustBeTimeSpan"));
			}
			long ticks = ((TimeSpan)value)._ticks;
			if (this._ticks > ticks)
			{
				return 1;
			}
			if (this._ticks < ticks)
			{
				return -1;
			}
			return 0;
		}

		/// <summary>Compares this instance to a specified <see cref="T:System.TimeSpan" /> object and returns an integer that indicates whether this instance is shorter than, equal to, or longer than the <see cref="T:System.TimeSpan" /> object.</summary>
		/// <param name="value">An object to compare to this instance.</param>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.  
		///   Value  
		///
		///   Description  
		///
		///   A negative integer  
		///
		///   This instance is shorter than <paramref name="value" />.  
		///
		///   Zero  
		///
		///   This instance is equal to <paramref name="value" />.  
		///
		///   A positive integer  
		///
		///   This instance is longer than <paramref name="value" />.</returns>
		// Token: 0x06001357 RID: 4951 RVA: 0x00038A9C File Offset: 0x00036C9C
		[__DynamicallyInvokable]
		public int CompareTo(TimeSpan value)
		{
			long ticks = value._ticks;
			if (this._ticks > ticks)
			{
				return 1;
			}
			if (this._ticks < ticks)
			{
				return -1;
			}
			return 0;
		}

		/// <summary>Returns a <see cref="T:System.TimeSpan" /> that represents a specified number of days, where the specification is accurate to the nearest millisecond.</summary>
		/// <param name="value">A number of days, accurate to the nearest millisecond.</param>
		/// <returns>An object that represents <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.  
		/// -or-  
		/// <paramref name="value" /> is <see cref="F:System.Double.PositiveInfinity" />.  
		/// -or-  
		/// <paramref name="value" /> is <see cref="F:System.Double.NegativeInfinity" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is equal to <see cref="F:System.Double.NaN" />.</exception>
		// Token: 0x06001358 RID: 4952 RVA: 0x00038AC7 File Offset: 0x00036CC7
		[__DynamicallyInvokable]
		public static TimeSpan FromDays(double value)
		{
			return TimeSpan.Interval(value, 86400000);
		}

		/// <summary>Returns a new <see cref="T:System.TimeSpan" /> object whose value is the absolute value of the current <see cref="T:System.TimeSpan" /> object.</summary>
		/// <returns>A new object whose value is the absolute value of the current <see cref="T:System.TimeSpan" /> object.</returns>
		/// <exception cref="T:System.OverflowException">The value of this instance is <see cref="F:System.TimeSpan.MinValue" />.</exception>
		// Token: 0x06001359 RID: 4953 RVA: 0x00038AD4 File Offset: 0x00036CD4
		[__DynamicallyInvokable]
		public TimeSpan Duration()
		{
			if (this.Ticks == TimeSpan.MinValue.Ticks)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_Duration"));
			}
			return new TimeSpan((this._ticks >= 0L) ? this._ticks : (-this._ticks));
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <param name="value">An object to compare with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is a <see cref="T:System.TimeSpan" /> object that represents the same time interval as the current <see cref="T:System.TimeSpan" /> structure; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600135A RID: 4954 RVA: 0x00038B24 File Offset: 0x00036D24
		[__DynamicallyInvokable]
		public override bool Equals(object value)
		{
			return value is TimeSpan && this._ticks == ((TimeSpan)value)._ticks;
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified <see cref="T:System.TimeSpan" /> object.</summary>
		/// <param name="obj">An object to compare with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> represents the same time interval as this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600135B RID: 4955 RVA: 0x00038B43 File Offset: 0x00036D43
		[__DynamicallyInvokable]
		public bool Equals(TimeSpan obj)
		{
			return this._ticks == obj._ticks;
		}

		/// <summary>Returns a value that indicates whether two specified instances of <see cref="T:System.TimeSpan" /> are equal.</summary>
		/// <param name="t1">The first time interval to compare.</param>
		/// <param name="t2">The second time interval to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the values of <paramref name="t1" /> and <paramref name="t2" /> are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600135C RID: 4956 RVA: 0x00038B53 File Offset: 0x00036D53
		[__DynamicallyInvokable]
		public static bool Equals(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks == t2._ticks;
		}

		/// <summary>Returns a hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x0600135D RID: 4957 RVA: 0x00038B63 File Offset: 0x00036D63
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return (int)this._ticks ^ (int)(this._ticks >> 32);
		}

		/// <summary>Returns a <see cref="T:System.TimeSpan" /> that represents a specified number of hours, where the specification is accurate to the nearest millisecond.</summary>
		/// <param name="value">A number of hours accurate to the nearest millisecond.</param>
		/// <returns>An object that represents <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.  
		/// -or-  
		/// <paramref name="value" /> is <see cref="F:System.Double.PositiveInfinity" />.  
		/// -or-  
		/// <paramref name="value" /> is <see cref="F:System.Double.NegativeInfinity" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is equal to <see cref="F:System.Double.NaN" />.</exception>
		// Token: 0x0600135E RID: 4958 RVA: 0x00038B77 File Offset: 0x00036D77
		[__DynamicallyInvokable]
		public static TimeSpan FromHours(double value)
		{
			return TimeSpan.Interval(value, 3600000);
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x00038B84 File Offset: 0x00036D84
		private static TimeSpan Interval(double value, int scale)
		{
			if (double.IsNaN(value))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_CannotBeNaN"));
			}
			double num = value * (double)scale;
			double num2 = num + ((value >= 0.0) ? 0.5 : (-0.5));
			if (num2 > 922337203685477.0 || num2 < -922337203685477.0)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_TimeSpanTooLong"));
			}
			return new TimeSpan((long)num2 * 10000L);
		}

		/// <summary>Returns a <see cref="T:System.TimeSpan" /> that represents a specified number of milliseconds.</summary>
		/// <param name="value">A number of milliseconds.</param>
		/// <returns>An object that represents <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.  
		/// -or-  
		/// <paramref name="value" /> is <see cref="F:System.Double.PositiveInfinity" />.  
		/// -or-  
		/// <paramref name="value" /> is <see cref="F:System.Double.NegativeInfinity" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is equal to <see cref="F:System.Double.NaN" />.</exception>
		// Token: 0x06001360 RID: 4960 RVA: 0x00038C07 File Offset: 0x00036E07
		[__DynamicallyInvokable]
		public static TimeSpan FromMilliseconds(double value)
		{
			return TimeSpan.Interval(value, 1);
		}

		/// <summary>Returns a <see cref="T:System.TimeSpan" /> that represents a specified number of minutes, where the specification is accurate to the nearest millisecond.</summary>
		/// <param name="value">A number of minutes, accurate to the nearest millisecond.</param>
		/// <returns>An object that represents <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.  
		/// -or-  
		/// <paramref name="value" /> is <see cref="F:System.Double.PositiveInfinity" />.  
		/// -or-  
		/// <paramref name="value" /> is <see cref="F:System.Double.NegativeInfinity" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is equal to <see cref="F:System.Double.NaN" />.</exception>
		// Token: 0x06001361 RID: 4961 RVA: 0x00038C10 File Offset: 0x00036E10
		[__DynamicallyInvokable]
		public static TimeSpan FromMinutes(double value)
		{
			return TimeSpan.Interval(value, 60000);
		}

		/// <summary>Returns a new <see cref="T:System.TimeSpan" /> object whose value is the negated value of this instance.</summary>
		/// <returns>A new object with the same numeric value as this instance, but with the opposite sign.</returns>
		/// <exception cref="T:System.OverflowException">The negated value of this instance cannot be represented by a <see cref="T:System.TimeSpan" />; that is, the value of this instance is <see cref="F:System.TimeSpan.MinValue" />.</exception>
		// Token: 0x06001362 RID: 4962 RVA: 0x00038C20 File Offset: 0x00036E20
		[__DynamicallyInvokable]
		public TimeSpan Negate()
		{
			if (this.Ticks == TimeSpan.MinValue.Ticks)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_NegateTwosCompNum"));
			}
			return new TimeSpan(-this._ticks);
		}

		/// <summary>Returns a <see cref="T:System.TimeSpan" /> that represents a specified number of seconds, where the specification is accurate to the nearest millisecond.</summary>
		/// <param name="value">A number of seconds, accurate to the nearest millisecond.</param>
		/// <returns>An object that represents <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.  
		/// -or-  
		/// <paramref name="value" /> is <see cref="F:System.Double.PositiveInfinity" />.  
		/// -or-  
		/// <paramref name="value" /> is <see cref="F:System.Double.NegativeInfinity" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is equal to <see cref="F:System.Double.NaN" />.</exception>
		// Token: 0x06001363 RID: 4963 RVA: 0x00038C5E File Offset: 0x00036E5E
		[__DynamicallyInvokable]
		public static TimeSpan FromSeconds(double value)
		{
			return TimeSpan.Interval(value, 1000);
		}

		/// <summary>Returns a new <see cref="T:System.TimeSpan" /> object whose value is the difference between the specified <see cref="T:System.TimeSpan" /> object and this instance.</summary>
		/// <param name="ts">The time interval to be subtracted.</param>
		/// <returns>A new time interval whose value is the result of the value of this instance minus the value of <paramref name="ts" />.</returns>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.</exception>
		// Token: 0x06001364 RID: 4964 RVA: 0x00038C6C File Offset: 0x00036E6C
		[__DynamicallyInvokable]
		public TimeSpan Subtract(TimeSpan ts)
		{
			long num = this._ticks - ts._ticks;
			if (this._ticks >> 63 != ts._ticks >> 63 && this._ticks >> 63 != num >> 63)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_TimeSpanTooLong"));
			}
			return new TimeSpan(num);
		}

		/// <summary>Returns a <see cref="T:System.TimeSpan" /> that represents a specified time, where the specification is in units of ticks.</summary>
		/// <param name="value">A number of ticks that represent a time.</param>
		/// <returns>An object that represents <paramref name="value" />.</returns>
		// Token: 0x06001365 RID: 4965 RVA: 0x00038CC0 File Offset: 0x00036EC0
		[__DynamicallyInvokable]
		public static TimeSpan FromTicks(long value)
		{
			return new TimeSpan(value);
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x00038CC8 File Offset: 0x00036EC8
		internal static long TimeToTicks(int hour, int minute, int second)
		{
			long num = (long)hour * 3600L + (long)minute * 60L + (long)second;
			if (num > 922337203685L || num < -922337203685L)
			{
				throw new ArgumentOutOfRangeException(null, Environment.GetResourceString("Overflow_TimeSpanTooLong"));
			}
			return num * 10000000L;
		}

		/// <summary>Converts the string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent.</summary>
		/// <param name="s">A string that specifies the time interval to convert.</param>
		/// <returns>A time interval that corresponds to <paramref name="s" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> has an invalid format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number that is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.  
		/// -or-  
		/// At least one of the days, hours, minutes, or seconds components is outside its valid range.</exception>
		// Token: 0x06001367 RID: 4967 RVA: 0x00038D1A File Offset: 0x00036F1A
		[__DynamicallyInvokable]
		public static TimeSpan Parse(string s)
		{
			return TimeSpanParse.Parse(s, null);
		}

		/// <summary>Converts the string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent by using the specified culture-specific format information.</summary>
		/// <param name="input">A string that specifies the time interval to convert.</param>
		/// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
		/// <returns>A time interval that corresponds to <paramref name="input" />, as specified by <paramref name="formatProvider" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> has an invalid format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="input" /> represents a number that is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.  
		/// -or-  
		/// At least one of the days, hours, minutes, or seconds components in <paramref name="input" /> is outside its valid range.</exception>
		// Token: 0x06001368 RID: 4968 RVA: 0x00038D23 File Offset: 0x00036F23
		[__DynamicallyInvokable]
		public static TimeSpan Parse(string input, IFormatProvider formatProvider)
		{
			return TimeSpanParse.Parse(input, formatProvider);
		}

		/// <summary>Converts the string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent by using the specified format and culture-specific format information. The format of the string representation must match the specified format exactly.</summary>
		/// <param name="input">A string that specifies the time interval to convert.</param>
		/// <param name="format">A standard or custom format string that defines the required format of <paramref name="input" />.</param>
		/// <param name="formatProvider">An object that provides culture-specific formatting information.</param>
		/// <returns>A time interval that corresponds to <paramref name="input" />, as specified by <paramref name="format" /> and <paramref name="formatProvider" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> has an invalid format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="input" /> represents a number that is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.  
		/// -or-  
		/// At least one of the days, hours, minutes, or seconds components in <paramref name="input" /> is outside its valid range.</exception>
		// Token: 0x06001369 RID: 4969 RVA: 0x00038D2C File Offset: 0x00036F2C
		[__DynamicallyInvokable]
		public static TimeSpan ParseExact(string input, string format, IFormatProvider formatProvider)
		{
			return TimeSpanParse.ParseExact(input, format, formatProvider, TimeSpanStyles.None);
		}

		/// <summary>Converts the string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent by using the specified array of format strings and culture-specific format information. The format of the string representation must match one of the specified formats exactly.</summary>
		/// <param name="input">A string that specifies the time interval to convert.</param>
		/// <param name="formats">A array of standard or custom format strings that defines the required format of <paramref name="input" />.</param>
		/// <param name="formatProvider">An object that provides culture-specific formatting information.</param>
		/// <returns>A time interval that corresponds to <paramref name="input" />, as specified by <paramref name="formats" /> and <paramref name="formatProvider" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> has an invalid format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="input" /> represents a number that is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.  
		/// -or-  
		/// At least one of the days, hours, minutes, or seconds components in <paramref name="input" /> is outside its valid range.</exception>
		// Token: 0x0600136A RID: 4970 RVA: 0x00038D37 File Offset: 0x00036F37
		[__DynamicallyInvokable]
		public static TimeSpan ParseExact(string input, string[] formats, IFormatProvider formatProvider)
		{
			return TimeSpanParse.ParseExactMultiple(input, formats, formatProvider, TimeSpanStyles.None);
		}

		/// <summary>Converts the string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent by using the specified format, culture-specific format information, and styles. The format of the string representation must match the specified format exactly.</summary>
		/// <param name="input">A string that specifies the time interval to convert.</param>
		/// <param name="format">A standard or custom format string that defines the required format of <paramref name="input" />.</param>
		/// <param name="formatProvider">An object that provides culture-specific formatting information.</param>
		/// <param name="styles">A bitwise combination of enumeration values that defines the style elements that may be present in <paramref name="input" />.</param>
		/// <returns>A time interval that corresponds to <paramref name="input" />, as specified by <paramref name="format" />, <paramref name="formatProvider" />, and <paramref name="styles" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="styles" /> is an invalid <see cref="T:System.Globalization.TimeSpanStyles" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> has an invalid format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="input" /> represents a number that is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.  
		/// -or-  
		/// At least one of the days, hours, minutes, or seconds components in <paramref name="input" /> is outside its valid range.</exception>
		// Token: 0x0600136B RID: 4971 RVA: 0x00038D42 File Offset: 0x00036F42
		[__DynamicallyInvokable]
		public static TimeSpan ParseExact(string input, string format, IFormatProvider formatProvider, TimeSpanStyles styles)
		{
			TimeSpanParse.ValidateStyles(styles, "styles");
			return TimeSpanParse.ParseExact(input, format, formatProvider, styles);
		}

		/// <summary>Converts the string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent by using the specified formats, culture-specific format information, and styles. The format of the string representation must match one of the specified formats exactly.</summary>
		/// <param name="input">A string that specifies the time interval to convert.</param>
		/// <param name="formats">A array of standard or custom format strings that define the required format of <paramref name="input" />.</param>
		/// <param name="formatProvider">An object that provides culture-specific formatting information.</param>
		/// <param name="styles">A bitwise combination of enumeration values that defines the style elements that may be present in input.</param>
		/// <returns>A time interval that corresponds to <paramref name="input" />, as specified by <paramref name="formats" />, <paramref name="formatProvider" />, and <paramref name="styles" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="styles" /> is an invalid <see cref="T:System.Globalization.TimeSpanStyles" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="input" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="input" /> has an invalid format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="input" /> represents a number that is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.  
		/// -or-  
		/// At least one of the days, hours, minutes, or seconds components in <paramref name="input" /> is outside its valid range.</exception>
		// Token: 0x0600136C RID: 4972 RVA: 0x00038D58 File Offset: 0x00036F58
		[__DynamicallyInvokable]
		public static TimeSpan ParseExact(string input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles)
		{
			TimeSpanParse.ValidateStyles(styles, "styles");
			return TimeSpanParse.ParseExactMultiple(input, formats, formatProvider, styles);
		}

		/// <summary>Converts the string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent and returns a value that indicates whether the conversion succeeded.</summary>
		/// <param name="s">A string that specifies the time interval to convert.</param>
		/// <param name="result">When this method returns, contains an object that represents the time interval specified by <paramref name="s" />, or <see cref="F:System.TimeSpan.Zero" /> if the conversion failed. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="s" /> was converted successfully; otherwise, <see langword="false" />. This operation returns <see langword="false" /> if the <paramref name="s" /> parameter is <see langword="null" /> or <see cref="F:System.String.Empty" />, has an invalid format, represents a time interval that is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />, or has at least one days, hours, minutes, or seconds component outside its valid range.</returns>
		// Token: 0x0600136D RID: 4973 RVA: 0x00038D6E File Offset: 0x00036F6E
		[__DynamicallyInvokable]
		public static bool TryParse(string s, out TimeSpan result)
		{
			return TimeSpanParse.TryParse(s, null, out result);
		}

		/// <summary>Converts the string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent by using the specified culture-specific formatting information, and returns a value that indicates whether the conversion succeeded.</summary>
		/// <param name="input">A string that specifies the time interval to convert.</param>
		/// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
		/// <param name="result">When this method returns, contains an object that represents the time interval specified by <paramref name="input" />, or <see cref="F:System.TimeSpan.Zero" /> if the conversion failed. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> was converted successfully; otherwise, <see langword="false" />. This operation returns <see langword="false" /> if the <paramref name="input" /> parameter is <see langword="null" /> or <see cref="F:System.String.Empty" />, has an invalid format, represents a time interval that is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />, or has at least one days, hours, minutes, or seconds component outside its valid range.</returns>
		// Token: 0x0600136E RID: 4974 RVA: 0x00038D78 File Offset: 0x00036F78
		[__DynamicallyInvokable]
		public static bool TryParse(string input, IFormatProvider formatProvider, out TimeSpan result)
		{
			return TimeSpanParse.TryParse(input, formatProvider, out result);
		}

		/// <summary>Converts the string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent by using the specified format and culture-specific format information, and returns a value that indicates whether the conversion succeeded. The format of the string representation must match the specified format exactly.</summary>
		/// <param name="input">A string that specifies the time interval to convert.</param>
		/// <param name="format">A standard or custom format string that defines the required format of <paramref name="input" />.</param>
		/// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
		/// <param name="result">When this method returns, contains an object that represents the time interval specified by <paramref name="input" />, or <see cref="F:System.TimeSpan.Zero" /> if the conversion failed. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> was converted successfully; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600136F RID: 4975 RVA: 0x00038D82 File Offset: 0x00036F82
		[__DynamicallyInvokable]
		public static bool TryParseExact(string input, string format, IFormatProvider formatProvider, out TimeSpan result)
		{
			return TimeSpanParse.TryParseExact(input, format, formatProvider, TimeSpanStyles.None, out result);
		}

		/// <summary>Converts the specified string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent by using the specified formats and culture-specific format information, and returns a value that indicates whether the conversion succeeded. The format of the string representation must match one of the specified formats exactly.</summary>
		/// <param name="input">A string that specifies the time interval to convert.</param>
		/// <param name="formats">A array of standard or custom format strings that define the acceptable formats of <paramref name="input" />.</param>
		/// <param name="formatProvider">An object that provides culture-specific formatting information.</param>
		/// <param name="result">When this method returns, contains an object that represents the time interval specified by <paramref name="input" />, or <see cref="F:System.TimeSpan.Zero" /> if the conversion failed. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> was converted successfully; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001370 RID: 4976 RVA: 0x00038D8E File Offset: 0x00036F8E
		[__DynamicallyInvokable]
		public static bool TryParseExact(string input, string[] formats, IFormatProvider formatProvider, out TimeSpan result)
		{
			return TimeSpanParse.TryParseExactMultiple(input, formats, formatProvider, TimeSpanStyles.None, out result);
		}

		/// <summary>Converts the string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent by using the specified format, culture-specific format information, and styles, and returns a value that indicates whether the conversion succeeded. The format of the string representation must match the specified format exactly.</summary>
		/// <param name="input">A string that specifies the time interval to convert.</param>
		/// <param name="format">A standard or custom format string that defines the required format of <paramref name="input" />.</param>
		/// <param name="formatProvider">An object that provides culture-specific formatting information.</param>
		/// <param name="styles">One or more enumeration values that indicate the style of <paramref name="input" />.</param>
		/// <param name="result">When this method returns, contains an object that represents the time interval specified by <paramref name="input" />, or <see cref="F:System.TimeSpan.Zero" /> if the conversion failed. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> was converted successfully; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001371 RID: 4977 RVA: 0x00038D9A File Offset: 0x00036F9A
		[__DynamicallyInvokable]
		public static bool TryParseExact(string input, string format, IFormatProvider formatProvider, TimeSpanStyles styles, out TimeSpan result)
		{
			TimeSpanParse.ValidateStyles(styles, "styles");
			return TimeSpanParse.TryParseExact(input, format, formatProvider, styles, out result);
		}

		/// <summary>Converts the specified string representation of a time interval to its <see cref="T:System.TimeSpan" /> equivalent by using the specified formats, culture-specific format information, and styles, and returns a value that indicates whether the conversion succeeded. The format of the string representation must match one of the specified formats exactly.</summary>
		/// <param name="input">A string that specifies the time interval to convert.</param>
		/// <param name="formats">A array of standard or custom format strings that define the acceptable formats of <paramref name="input" />.</param>
		/// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
		/// <param name="styles">One or more enumeration values that indicate the style of <paramref name="input" />.</param>
		/// <param name="result">When this method returns, contains an object that represents the time interval specified by <paramref name="input" />, or <see cref="F:System.TimeSpan.Zero" /> if the conversion failed. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="input" /> was converted successfully; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001372 RID: 4978 RVA: 0x00038DB2 File Offset: 0x00036FB2
		[__DynamicallyInvokable]
		public static bool TryParseExact(string input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles, out TimeSpan result)
		{
			TimeSpanParse.ValidateStyles(styles, "styles");
			return TimeSpanParse.TryParseExactMultiple(input, formats, formatProvider, styles, out result);
		}

		/// <summary>Converts the value of the current <see cref="T:System.TimeSpan" /> object to its equivalent string representation.</summary>
		/// <returns>The string representation of the current <see cref="T:System.TimeSpan" /> value.</returns>
		// Token: 0x06001373 RID: 4979 RVA: 0x00038DCA File Offset: 0x00036FCA
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return TimeSpanFormat.Format(this, null, null);
		}

		/// <summary>Converts the value of the current <see cref="T:System.TimeSpan" /> object to its equivalent string representation by using the specified format.</summary>
		/// <param name="format">A standard or custom <see cref="T:System.TimeSpan" /> format string.</param>
		/// <returns>The string representation of the current <see cref="T:System.TimeSpan" /> value in the format specified by the <paramref name="format" /> parameter.</returns>
		/// <exception cref="T:System.FormatException">The <paramref name="format" /> parameter is not recognized or is not supported.</exception>
		// Token: 0x06001374 RID: 4980 RVA: 0x00038DD9 File Offset: 0x00036FD9
		[__DynamicallyInvokable]
		public string ToString(string format)
		{
			return TimeSpanFormat.Format(this, format, null);
		}

		/// <summary>Converts the value of the current <see cref="T:System.TimeSpan" /> object to its equivalent string representation by using the specified format and culture-specific formatting information.</summary>
		/// <param name="format">A standard or custom <see cref="T:System.TimeSpan" /> format string.</param>
		/// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the current <see cref="T:System.TimeSpan" /> value, as specified by <paramref name="format" /> and <paramref name="formatProvider" />.</returns>
		/// <exception cref="T:System.FormatException">The <paramref name="format" /> parameter is not recognized or is not supported.</exception>
		// Token: 0x06001375 RID: 4981 RVA: 0x00038DE8 File Offset: 0x00036FE8
		[__DynamicallyInvokable]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			if (TimeSpan.LegacyMode)
			{
				return TimeSpanFormat.Format(this, null, null);
			}
			return TimeSpanFormat.Format(this, format, formatProvider);
		}

		/// <summary>Returns a <see cref="T:System.TimeSpan" /> whose value is the negated value of the specified instance.</summary>
		/// <param name="t">The time interval to be negated.</param>
		/// <returns>An object that has the same numeric value as this instance, but the opposite sign.</returns>
		/// <exception cref="T:System.OverflowException">The negated value of this instance cannot be represented by a <see cref="T:System.TimeSpan" />; that is, the value of this instance is <see cref="F:System.TimeSpan.MinValue" />.</exception>
		// Token: 0x06001376 RID: 4982 RVA: 0x00038E0C File Offset: 0x0003700C
		[__DynamicallyInvokable]
		public static TimeSpan operator -(TimeSpan t)
		{
			if (t._ticks == TimeSpan.MinValue._ticks)
			{
				throw new OverflowException(Environment.GetResourceString("Overflow_NegateTwosCompNum"));
			}
			return new TimeSpan(-t._ticks);
		}

		/// <summary>Subtracts a specified <see cref="T:System.TimeSpan" /> from another specified <see cref="T:System.TimeSpan" />.</summary>
		/// <param name="t1">The minuend.</param>
		/// <param name="t2">The subtrahend.</param>
		/// <returns>An object whose value is the result of the value of <paramref name="t1" /> minus the value of <paramref name="t2" />.</returns>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.</exception>
		// Token: 0x06001377 RID: 4983 RVA: 0x00038E3C File Offset: 0x0003703C
		[__DynamicallyInvokable]
		public static TimeSpan operator -(TimeSpan t1, TimeSpan t2)
		{
			return t1.Subtract(t2);
		}

		/// <summary>Returns the specified instance of <see cref="T:System.TimeSpan" />.</summary>
		/// <param name="t">The time interval to return.</param>
		/// <returns>The time interval specified by <paramref name="t" />.</returns>
		// Token: 0x06001378 RID: 4984 RVA: 0x00038E46 File Offset: 0x00037046
		[__DynamicallyInvokable]
		public static TimeSpan operator +(TimeSpan t)
		{
			return t;
		}

		/// <summary>Adds two specified <see cref="T:System.TimeSpan" /> instances.</summary>
		/// <param name="t1">The first time interval to add.</param>
		/// <param name="t2">The second time interval to add.</param>
		/// <returns>An object whose value is the sum of the values of <paramref name="t1" /> and <paramref name="t2" />.</returns>
		/// <exception cref="T:System.OverflowException">The resulting <see cref="T:System.TimeSpan" /> is less than <see cref="F:System.TimeSpan.MinValue" /> or greater than <see cref="F:System.TimeSpan.MaxValue" />.</exception>
		// Token: 0x06001379 RID: 4985 RVA: 0x00038E49 File Offset: 0x00037049
		[__DynamicallyInvokable]
		public static TimeSpan operator +(TimeSpan t1, TimeSpan t2)
		{
			return t1.Add(t2);
		}

		/// <summary>Indicates whether two <see cref="T:System.TimeSpan" /> instances are equal.</summary>
		/// <param name="t1">The first time interval to compare.</param>
		/// <param name="t2">The second time interval to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the values of <paramref name="t1" /> and <paramref name="t2" /> are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600137A RID: 4986 RVA: 0x00038E53 File Offset: 0x00037053
		[__DynamicallyInvokable]
		public static bool operator ==(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks == t2._ticks;
		}

		/// <summary>Indicates whether two <see cref="T:System.TimeSpan" /> instances are not equal.</summary>
		/// <param name="t1">The first time interval to compare.</param>
		/// <param name="t2">The second time interval to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the values of <paramref name="t1" /> and <paramref name="t2" /> are not equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600137B RID: 4987 RVA: 0x00038E63 File Offset: 0x00037063
		[__DynamicallyInvokable]
		public static bool operator !=(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks != t2._ticks;
		}

		/// <summary>Indicates whether a specified <see cref="T:System.TimeSpan" /> is less than another specified <see cref="T:System.TimeSpan" />.</summary>
		/// <param name="t1">The first time interval to compare.</param>
		/// <param name="t2">The second time interval to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the value of <paramref name="t1" /> is less than the value of <paramref name="t2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600137C RID: 4988 RVA: 0x00038E76 File Offset: 0x00037076
		[__DynamicallyInvokable]
		public static bool operator <(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks < t2._ticks;
		}

		/// <summary>Indicates whether a specified <see cref="T:System.TimeSpan" /> is less than or equal to another specified <see cref="T:System.TimeSpan" />.</summary>
		/// <param name="t1">The first time interval to compare.</param>
		/// <param name="t2">The second time interval to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the value of <paramref name="t1" /> is less than or equal to the value of <paramref name="t2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600137D RID: 4989 RVA: 0x00038E86 File Offset: 0x00037086
		[__DynamicallyInvokable]
		public static bool operator <=(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks <= t2._ticks;
		}

		/// <summary>Indicates whether a specified <see cref="T:System.TimeSpan" /> is greater than another specified <see cref="T:System.TimeSpan" />.</summary>
		/// <param name="t1">The first time interval to compare.</param>
		/// <param name="t2">The second time interval to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the value of <paramref name="t1" /> is greater than the value of <paramref name="t2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600137E RID: 4990 RVA: 0x00038E99 File Offset: 0x00037099
		[__DynamicallyInvokable]
		public static bool operator >(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks > t2._ticks;
		}

		/// <summary>Indicates whether a specified <see cref="T:System.TimeSpan" /> is greater than or equal to another specified <see cref="T:System.TimeSpan" />.</summary>
		/// <param name="t1">The first time interval to compare.</param>
		/// <param name="t2">The second time interval to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the value of <paramref name="t1" /> is greater than or equal to the value of <paramref name="t2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600137F RID: 4991 RVA: 0x00038EA9 File Offset: 0x000370A9
		[__DynamicallyInvokable]
		public static bool operator >=(TimeSpan t1, TimeSpan t2)
		{
			return t1._ticks >= t2._ticks;
		}

		// Token: 0x06001380 RID: 4992
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool LegacyFormatMode();

		// Token: 0x06001381 RID: 4993 RVA: 0x00038EBC File Offset: 0x000370BC
		[SecuritySafeCritical]
		private static bool GetLegacyFormatMode()
		{
			return TimeSpan.LegacyFormatMode() || CompatibilitySwitches.IsNetFx40TimeSpanLegacyFormatMode;
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06001382 RID: 4994 RVA: 0x00038ECC File Offset: 0x000370CC
		private static bool LegacyMode
		{
			get
			{
				if (!TimeSpan._legacyConfigChecked)
				{
					TimeSpan._legacyMode = TimeSpan.GetLegacyFormatMode();
					TimeSpan._legacyConfigChecked = true;
				}
				return TimeSpan._legacyMode;
			}
		}

		/// <summary>Represents the number of ticks in 1 millisecond. This field is constant.</summary>
		// Token: 0x0400068F RID: 1679
		[__DynamicallyInvokable]
		public const long TicksPerMillisecond = 10000L;

		// Token: 0x04000690 RID: 1680
		private const double MillisecondsPerTick = 0.0001;

		/// <summary>Represents the number of ticks in 1 second.</summary>
		// Token: 0x04000691 RID: 1681
		[__DynamicallyInvokable]
		public const long TicksPerSecond = 10000000L;

		// Token: 0x04000692 RID: 1682
		private const double SecondsPerTick = 1E-07;

		/// <summary>Represents the number of ticks in 1 minute. This field is constant.</summary>
		// Token: 0x04000693 RID: 1683
		[__DynamicallyInvokable]
		public const long TicksPerMinute = 600000000L;

		// Token: 0x04000694 RID: 1684
		private const double MinutesPerTick = 1.6666666666666667E-09;

		/// <summary>Represents the number of ticks in 1 hour. This field is constant.</summary>
		// Token: 0x04000695 RID: 1685
		[__DynamicallyInvokable]
		public const long TicksPerHour = 36000000000L;

		// Token: 0x04000696 RID: 1686
		private const double HoursPerTick = 2.7777777777777777E-11;

		/// <summary>Represents the number of ticks in 1 day. This field is constant.</summary>
		// Token: 0x04000697 RID: 1687
		[__DynamicallyInvokable]
		public const long TicksPerDay = 864000000000L;

		// Token: 0x04000698 RID: 1688
		private const double DaysPerTick = 1.1574074074074074E-12;

		// Token: 0x04000699 RID: 1689
		private const int MillisPerSecond = 1000;

		// Token: 0x0400069A RID: 1690
		private const int MillisPerMinute = 60000;

		// Token: 0x0400069B RID: 1691
		private const int MillisPerHour = 3600000;

		// Token: 0x0400069C RID: 1692
		private const int MillisPerDay = 86400000;

		// Token: 0x0400069D RID: 1693
		internal const long MaxSeconds = 922337203685L;

		// Token: 0x0400069E RID: 1694
		internal const long MinSeconds = -922337203685L;

		// Token: 0x0400069F RID: 1695
		internal const long MaxMilliSeconds = 922337203685477L;

		// Token: 0x040006A0 RID: 1696
		internal const long MinMilliSeconds = -922337203685477L;

		// Token: 0x040006A1 RID: 1697
		internal const long TicksPerTenthSecond = 1000000L;

		/// <summary>Represents the zero <see cref="T:System.TimeSpan" /> value. This field is read-only.</summary>
		// Token: 0x040006A2 RID: 1698
		[__DynamicallyInvokable]
		public static readonly TimeSpan Zero = new TimeSpan(0L);

		/// <summary>Represents the maximum <see cref="T:System.TimeSpan" /> value. This field is read-only.</summary>
		// Token: 0x040006A3 RID: 1699
		[__DynamicallyInvokable]
		public static readonly TimeSpan MaxValue = new TimeSpan(long.MaxValue);

		/// <summary>Represents the minimum <see cref="T:System.TimeSpan" /> value. This field is read-only.</summary>
		// Token: 0x040006A4 RID: 1700
		[__DynamicallyInvokable]
		public static readonly TimeSpan MinValue = new TimeSpan(long.MinValue);

		// Token: 0x040006A5 RID: 1701
		internal long _ticks;

		// Token: 0x040006A6 RID: 1702
		private static volatile bool _legacyConfigChecked;

		// Token: 0x040006A7 RID: 1703
		private static volatile bool _legacyMode;
	}
}
