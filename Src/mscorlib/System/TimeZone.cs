using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;

namespace System
{
	/// <summary>Represents a time zone.</summary>
	// Token: 0x02000144 RID: 324
	[ComVisible(true)]
	[Serializable]
	public abstract class TimeZone
	{
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06001384 RID: 4996 RVA: 0x00038F28 File Offset: 0x00037128
		private static object InternalSyncObject
		{
			get
			{
				if (TimeZone.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange<object>(ref TimeZone.s_InternalSyncObject, obj, null);
				}
				return TimeZone.s_InternalSyncObject;
			}
		}

		/// <summary>Gets the time zone of the current computer.</summary>
		/// <returns>A <see cref="T:System.TimeZone" /> object that represents the current local time zone.</returns>
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06001386 RID: 4998 RVA: 0x00038F5C File Offset: 0x0003715C
		public static TimeZone CurrentTimeZone
		{
			get
			{
				TimeZone timeZone = TimeZone.currentTimeZone;
				if (timeZone == null)
				{
					object internalSyncObject = TimeZone.InternalSyncObject;
					lock (internalSyncObject)
					{
						if (TimeZone.currentTimeZone == null)
						{
							TimeZone.currentTimeZone = new CurrentSystemTimeZone();
						}
						timeZone = TimeZone.currentTimeZone;
					}
				}
				return timeZone;
			}
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x00038FC0 File Offset: 0x000371C0
		internal static void ResetTimeZone()
		{
			if (TimeZone.currentTimeZone != null)
			{
				object internalSyncObject = TimeZone.InternalSyncObject;
				lock (internalSyncObject)
				{
					TimeZone.currentTimeZone = null;
				}
			}
		}

		/// <summary>Gets the standard time zone name.</summary>
		/// <returns>The standard time zone name.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt was made to set this property to <see langword="null" />.</exception>
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06001388 RID: 5000
		public abstract string StandardName { get; }

		/// <summary>Gets the daylight saving time zone name.</summary>
		/// <returns>The daylight saving time zone name.</returns>
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06001389 RID: 5001
		public abstract string DaylightName { get; }

		/// <summary>Returns the Coordinated Universal Time (UTC) offset for the specified local time.</summary>
		/// <param name="time">A date and time value.</param>
		/// <returns>The Coordinated Universal Time (UTC) offset from <paramref name="time" />.</returns>
		// Token: 0x0600138A RID: 5002
		public abstract TimeSpan GetUtcOffset(DateTime time);

		/// <summary>Returns the Coordinated Universal Time (UTC) that corresponds to a specified time.</summary>
		/// <param name="time">A date and time.</param>
		/// <returns>A <see cref="T:System.DateTime" /> object whose value is the Coordinated Universal Time (UTC) that corresponds to <paramref name="time" />.</returns>
		// Token: 0x0600138B RID: 5003 RVA: 0x0003900C File Offset: 0x0003720C
		public virtual DateTime ToUniversalTime(DateTime time)
		{
			if (time.Kind == DateTimeKind.Utc)
			{
				return time;
			}
			long num = time.Ticks - this.GetUtcOffset(time).Ticks;
			if (num > 3155378975999999999L)
			{
				return new DateTime(3155378975999999999L, DateTimeKind.Utc);
			}
			if (num < 0L)
			{
				return new DateTime(0L, DateTimeKind.Utc);
			}
			return new DateTime(num, DateTimeKind.Utc);
		}

		/// <summary>Returns the local time that corresponds to a specified date and time value.</summary>
		/// <param name="time">A Coordinated Universal Time (UTC) time.</param>
		/// <returns>A <see cref="T:System.DateTime" /> object whose value is the local time that corresponds to <paramref name="time" />.</returns>
		// Token: 0x0600138C RID: 5004 RVA: 0x00039070 File Offset: 0x00037270
		public virtual DateTime ToLocalTime(DateTime time)
		{
			if (time.Kind == DateTimeKind.Local)
			{
				return time;
			}
			bool flag = false;
			long utcOffsetFromUniversalTime = ((CurrentSystemTimeZone)TimeZone.CurrentTimeZone).GetUtcOffsetFromUniversalTime(time, ref flag);
			return new DateTime(time.Ticks + utcOffsetFromUniversalTime, DateTimeKind.Local, flag);
		}

		/// <summary>Returns the daylight saving time period for a particular year.</summary>
		/// <param name="year">The year that the daylight saving time period applies to.</param>
		/// <returns>A <see cref="T:System.Globalization.DaylightTime" /> object that contains the start and end date for daylight saving time in <paramref name="year" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="year" /> is less than 1 or greater than 9999.</exception>
		// Token: 0x0600138D RID: 5005
		public abstract DaylightTime GetDaylightChanges(int year);

		/// <summary>Returns a value indicating whether the specified date and time is within a daylight saving time period.</summary>
		/// <param name="time">A date and time.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="time" /> is in a daylight saving time period; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600138E RID: 5006 RVA: 0x000390AE File Offset: 0x000372AE
		public virtual bool IsDaylightSavingTime(DateTime time)
		{
			return TimeZone.IsDaylightSavingTime(time, this.GetDaylightChanges(time.Year));
		}

		/// <summary>Returns a value indicating whether the specified date and time is within the specified daylight saving time period.</summary>
		/// <param name="time">A date and time.</param>
		/// <param name="daylightTimes">A daylight saving time period.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="time" /> is in <paramref name="daylightTimes" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="daylightTimes" /> is <see langword="null" />.</exception>
		// Token: 0x0600138F RID: 5007 RVA: 0x000390C3 File Offset: 0x000372C3
		public static bool IsDaylightSavingTime(DateTime time, DaylightTime daylightTimes)
		{
			return TimeZone.CalculateUtcOffset(time, daylightTimes) != TimeSpan.Zero;
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x000390D8 File Offset: 0x000372D8
		internal static TimeSpan CalculateUtcOffset(DateTime time, DaylightTime daylightTimes)
		{
			if (daylightTimes == null)
			{
				return TimeSpan.Zero;
			}
			DateTimeKind kind = time.Kind;
			if (kind == DateTimeKind.Utc)
			{
				return TimeSpan.Zero;
			}
			DateTime dateTime = daylightTimes.Start + daylightTimes.Delta;
			DateTime end = daylightTimes.End;
			DateTime dateTime2;
			DateTime dateTime3;
			if (daylightTimes.Delta.Ticks > 0L)
			{
				dateTime2 = end - daylightTimes.Delta;
				dateTime3 = end;
			}
			else
			{
				dateTime2 = dateTime;
				dateTime3 = dateTime - daylightTimes.Delta;
			}
			bool flag = false;
			if (dateTime > end)
			{
				if (time >= dateTime || time < end)
				{
					flag = true;
				}
			}
			else if (time >= dateTime && time < end)
			{
				flag = true;
			}
			if (flag && time >= dateTime2 && time < dateTime3)
			{
				flag = time.IsAmbiguousDaylightSavingTime();
			}
			if (flag)
			{
				return daylightTimes.Delta;
			}
			return TimeSpan.Zero;
		}

		// Token: 0x040006A8 RID: 1704
		private static volatile TimeZone currentTimeZone;

		// Token: 0x040006A9 RID: 1705
		private static object s_InternalSyncObject;
	}
}
