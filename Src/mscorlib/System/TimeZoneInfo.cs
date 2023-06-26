﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32;

namespace System
{
	/// <summary>Represents any time zone in the world.</summary>
	// Token: 0x02000146 RID: 326
	[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public sealed class TimeZoneInfo : IEquatable<TimeZoneInfo>, ISerializable, IDeserializationCallback
	{
		/// <summary>Gets the time zone identifier.</summary>
		/// <returns>The time zone identifier.</returns>
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06001391 RID: 5009 RVA: 0x000391B6 File Offset: 0x000373B6
		[__DynamicallyInvokable]
		public string Id
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_id;
			}
		}

		/// <summary>Gets the general display name that represents the time zone.</summary>
		/// <returns>The time zone's general display name.</returns>
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06001392 RID: 5010 RVA: 0x000391BE File Offset: 0x000373BE
		[__DynamicallyInvokable]
		public string DisplayName
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_displayName != null)
				{
					return this.m_displayName;
				}
				return string.Empty;
			}
		}

		/// <summary>Gets the display name for the time zone's standard time.</summary>
		/// <returns>The display name of the time zone's standard time.</returns>
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06001393 RID: 5011 RVA: 0x000391D4 File Offset: 0x000373D4
		[__DynamicallyInvokable]
		public string StandardName
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_standardDisplayName != null)
				{
					return this.m_standardDisplayName;
				}
				return string.Empty;
			}
		}

		/// <summary>Gets the display name for the current time zone's daylight saving time.</summary>
		/// <returns>The display name for the time zone's daylight saving time.</returns>
		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06001394 RID: 5012 RVA: 0x000391EA File Offset: 0x000373EA
		[__DynamicallyInvokable]
		public string DaylightName
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_daylightDisplayName != null)
				{
					return this.m_daylightDisplayName;
				}
				return string.Empty;
			}
		}

		/// <summary>Gets the time difference between the current time zone's standard time and Coordinated Universal Time (UTC).</summary>
		/// <returns>An object that indicates the time difference between the current time zone's standard time and Coordinated Universal Time (UTC).</returns>
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06001395 RID: 5013 RVA: 0x00039200 File Offset: 0x00037400
		[__DynamicallyInvokable]
		public TimeSpan BaseUtcOffset
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_baseUtcOffset;
			}
		}

		/// <summary>Gets a value indicating whether the time zone has any daylight saving time rules.</summary>
		/// <returns>
		///   <see langword="true" /> if the time zone supports daylight saving time; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06001396 RID: 5014 RVA: 0x00039208 File Offset: 0x00037408
		[__DynamicallyInvokable]
		public bool SupportsDaylightSavingTime
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_supportsDaylightSavingTime;
			}
		}

		/// <summary>Retrieves an array of <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> objects that apply to the current <see cref="T:System.TimeZoneInfo" /> object.</summary>
		/// <returns>An array of objects for this time zone.</returns>
		/// <exception cref="T:System.OutOfMemoryException">The system does not have enough memory to make an in-memory copy of the adjustment rules.</exception>
		// Token: 0x06001397 RID: 5015 RVA: 0x00039210 File Offset: 0x00037410
		public TimeZoneInfo.AdjustmentRule[] GetAdjustmentRules()
		{
			if (this.m_adjustmentRules == null)
			{
				return new TimeZoneInfo.AdjustmentRule[0];
			}
			return (TimeZoneInfo.AdjustmentRule[])this.m_adjustmentRules.Clone();
		}

		/// <summary>Returns information about the possible dates and times that an ambiguous date and time can be mapped to.</summary>
		/// <param name="dateTimeOffset">A date and time.</param>
		/// <returns>An array of objects that represents possible Coordinated Universal Time (UTC) offsets that a particular date and time can be mapped to.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dateTimeOffset" /> is not an ambiguous time.</exception>
		// Token: 0x06001398 RID: 5016 RVA: 0x00039234 File Offset: 0x00037434
		[__DynamicallyInvokable]
		public TimeSpan[] GetAmbiguousTimeOffsets(DateTimeOffset dateTimeOffset)
		{
			if (!this.SupportsDaylightSavingTime)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeOffsetIsNotAmbiguous"), "dateTimeOffset");
			}
			DateTime dateTime = TimeZoneInfo.ConvertTime(dateTimeOffset, this).DateTime;
			bool flag = false;
			TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = this.GetAdjustmentRuleForTime(dateTime);
			if (adjustmentRuleForTime != null && adjustmentRuleForTime.HasDaylightSaving)
			{
				DaylightTimeStruct daylightTime = TimeZoneInfo.GetDaylightTime(dateTime.Year, adjustmentRuleForTime);
				flag = TimeZoneInfo.GetIsAmbiguousTime(dateTime, adjustmentRuleForTime, daylightTime);
			}
			if (!flag)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeOffsetIsNotAmbiguous"), "dateTimeOffset");
			}
			TimeSpan[] array = new TimeSpan[2];
			TimeSpan timeSpan = this.m_baseUtcOffset + adjustmentRuleForTime.BaseUtcOffsetDelta;
			if (adjustmentRuleForTime.DaylightDelta > TimeSpan.Zero)
			{
				array[0] = timeSpan;
				array[1] = timeSpan + adjustmentRuleForTime.DaylightDelta;
			}
			else
			{
				array[0] = timeSpan + adjustmentRuleForTime.DaylightDelta;
				array[1] = timeSpan;
			}
			return array;
		}

		/// <summary>Returns information about the possible dates and times that an ambiguous date and time can be mapped to.</summary>
		/// <param name="dateTime">A date and time.</param>
		/// <returns>An array of objects that represents possible Coordinated Universal Time (UTC) offsets that a particular date and time can be mapped to.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dateTime" /> is not an ambiguous time.</exception>
		// Token: 0x06001399 RID: 5017 RVA: 0x00039320 File Offset: 0x00037520
		[__DynamicallyInvokable]
		public TimeSpan[] GetAmbiguousTimeOffsets(DateTime dateTime)
		{
			if (!this.SupportsDaylightSavingTime)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeIsNotAmbiguous"), "dateTime");
			}
			DateTime dateTime2;
			if (dateTime.Kind == DateTimeKind.Local)
			{
				TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
				dateTime2 = TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, this, TimeZoneInfoOptions.None, cachedData);
			}
			else if (dateTime.Kind == DateTimeKind.Utc)
			{
				TimeZoneInfo.CachedData cachedData2 = TimeZoneInfo.s_cachedData;
				dateTime2 = TimeZoneInfo.ConvertTime(dateTime, cachedData2.Utc, this, TimeZoneInfoOptions.None, cachedData2);
			}
			else
			{
				dateTime2 = dateTime;
			}
			bool flag = false;
			TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = this.GetAdjustmentRuleForTime(dateTime2);
			if (adjustmentRuleForTime != null && adjustmentRuleForTime.HasDaylightSaving)
			{
				DaylightTimeStruct daylightTime = TimeZoneInfo.GetDaylightTime(dateTime2.Year, adjustmentRuleForTime);
				flag = TimeZoneInfo.GetIsAmbiguousTime(dateTime2, adjustmentRuleForTime, daylightTime);
			}
			if (!flag)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeIsNotAmbiguous"), "dateTime");
			}
			TimeSpan[] array = new TimeSpan[2];
			TimeSpan timeSpan = this.m_baseUtcOffset + adjustmentRuleForTime.BaseUtcOffsetDelta;
			if (adjustmentRuleForTime.DaylightDelta > TimeSpan.Zero)
			{
				array[0] = timeSpan;
				array[1] = timeSpan + adjustmentRuleForTime.DaylightDelta;
			}
			else
			{
				array[0] = timeSpan + adjustmentRuleForTime.DaylightDelta;
				array[1] = timeSpan;
			}
			return array;
		}

		/// <summary>Calculates the offset or difference between the time in this time zone and Coordinated Universal Time (UTC) for a particular date and time.</summary>
		/// <param name="dateTimeOffset">The date and time to determine the offset for.</param>
		/// <returns>An object that indicates the time difference between Coordinated Universal Time (UTC) and the current time zone.</returns>
		// Token: 0x0600139A RID: 5018 RVA: 0x00039445 File Offset: 0x00037645
		[__DynamicallyInvokable]
		public TimeSpan GetUtcOffset(DateTimeOffset dateTimeOffset)
		{
			return TimeZoneInfo.GetUtcOffsetFromUtc(dateTimeOffset.UtcDateTime, this);
		}

		/// <summary>Calculates the offset or difference between the time in this time zone and Coordinated Universal Time (UTC) for a particular date and time.</summary>
		/// <param name="dateTime">The date and time to determine the offset for.</param>
		/// <returns>An object that indicates the time difference between the two time zones.</returns>
		// Token: 0x0600139B RID: 5019 RVA: 0x00039454 File Offset: 0x00037654
		[__DynamicallyInvokable]
		public TimeSpan GetUtcOffset(DateTime dateTime)
		{
			return this.GetUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x00039464 File Offset: 0x00037664
		internal static TimeSpan GetLocalUtcOffset(DateTime dateTime, TimeZoneInfoOptions flags)
		{
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			return cachedData.Local.GetUtcOffset(dateTime, flags, cachedData);
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x00039485 File Offset: 0x00037685
		internal TimeSpan GetUtcOffset(DateTime dateTime, TimeZoneInfoOptions flags)
		{
			return this.GetUtcOffset(dateTime, flags, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x00039494 File Offset: 0x00037694
		private TimeSpan GetUtcOffset(DateTime dateTime, TimeZoneInfoOptions flags, TimeZoneInfo.CachedData cachedData)
		{
			if (dateTime.Kind == DateTimeKind.Local)
			{
				if (cachedData.GetCorrespondingKind(this) != DateTimeKind.Local)
				{
					DateTime dateTime2 = TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, cachedData.Utc, flags);
					return TimeZoneInfo.GetUtcOffsetFromUtc(dateTime2, this);
				}
			}
			else if (dateTime.Kind == DateTimeKind.Utc)
			{
				if (cachedData.GetCorrespondingKind(this) == DateTimeKind.Utc)
				{
					return this.m_baseUtcOffset;
				}
				return TimeZoneInfo.GetUtcOffsetFromUtc(dateTime, this);
			}
			return TimeZoneInfo.GetUtcOffset(dateTime, this, flags);
		}

		/// <summary>Determines whether a particular date and time in a particular time zone is ambiguous and can be mapped to two or more Coordinated Universal Time (UTC) times.</summary>
		/// <param name="dateTimeOffset">A date and time.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="dateTimeOffset" /> parameter is ambiguous in the current time zone; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600139F RID: 5023 RVA: 0x000394FC File Offset: 0x000376FC
		[__DynamicallyInvokable]
		public bool IsAmbiguousTime(DateTimeOffset dateTimeOffset)
		{
			return this.m_supportsDaylightSavingTime && this.IsAmbiguousTime(TimeZoneInfo.ConvertTime(dateTimeOffset, this).DateTime);
		}

		/// <summary>Determines whether a particular date and time in a particular time zone is ambiguous and can be mapped to two or more Coordinated Universal Time (UTC) times.</summary>
		/// <param name="dateTime">A date and time value.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="dateTime" /> parameter is ambiguous; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.DateTime.Kind" /> property of the <paramref name="dateTime" /> value is <see cref="F:System.DateTimeKind.Local" /> and <paramref name="dateTime" /> is an invalid time.</exception>
		// Token: 0x060013A0 RID: 5024 RVA: 0x00039528 File Offset: 0x00037728
		[__DynamicallyInvokable]
		public bool IsAmbiguousTime(DateTime dateTime)
		{
			return this.IsAmbiguousTime(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime);
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x00039534 File Offset: 0x00037734
		internal bool IsAmbiguousTime(DateTime dateTime, TimeZoneInfoOptions flags)
		{
			if (!this.m_supportsDaylightSavingTime)
			{
				return false;
			}
			DateTime dateTime2;
			if (dateTime.Kind == DateTimeKind.Local)
			{
				TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
				dateTime2 = TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, this, flags, cachedData);
			}
			else if (dateTime.Kind == DateTimeKind.Utc)
			{
				TimeZoneInfo.CachedData cachedData2 = TimeZoneInfo.s_cachedData;
				dateTime2 = TimeZoneInfo.ConvertTime(dateTime, cachedData2.Utc, this, flags, cachedData2);
			}
			else
			{
				dateTime2 = dateTime;
			}
			TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = this.GetAdjustmentRuleForTime(dateTime2);
			if (adjustmentRuleForTime != null && adjustmentRuleForTime.HasDaylightSaving)
			{
				DaylightTimeStruct daylightTime = TimeZoneInfo.GetDaylightTime(dateTime2.Year, adjustmentRuleForTime);
				return TimeZoneInfo.GetIsAmbiguousTime(dateTime2, adjustmentRuleForTime, daylightTime);
			}
			return false;
		}

		/// <summary>Indicates whether a specified date and time falls in the range of daylight saving time for the time zone of the current <see cref="T:System.TimeZoneInfo" /> object.</summary>
		/// <param name="dateTimeOffset">A date and time value.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="dateTimeOffset" /> parameter is a daylight saving time; otherwise, <see langword="false" />.</returns>
		// Token: 0x060013A2 RID: 5026 RVA: 0x000395C0 File Offset: 0x000377C0
		[__DynamicallyInvokable]
		public bool IsDaylightSavingTime(DateTimeOffset dateTimeOffset)
		{
			bool flag;
			TimeZoneInfo.GetUtcOffsetFromUtc(dateTimeOffset.UtcDateTime, this, out flag);
			return flag;
		}

		/// <summary>Indicates whether a specified date and time falls in the range of daylight saving time for the time zone of the current <see cref="T:System.TimeZoneInfo" /> object.</summary>
		/// <param name="dateTime">A date and time value.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="dateTime" /> parameter is a daylight saving time; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.DateTime.Kind" /> property of the <paramref name="dateTime" /> value is <see cref="F:System.DateTimeKind.Local" /> and <paramref name="dateTime" /> is an invalid time.</exception>
		// Token: 0x060013A3 RID: 5027 RVA: 0x000395DE File Offset: 0x000377DE
		[__DynamicallyInvokable]
		public bool IsDaylightSavingTime(DateTime dateTime)
		{
			return this.IsDaylightSavingTime(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x000395ED File Offset: 0x000377ED
		internal bool IsDaylightSavingTime(DateTime dateTime, TimeZoneInfoOptions flags)
		{
			return this.IsDaylightSavingTime(dateTime, flags, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x000395FC File Offset: 0x000377FC
		private bool IsDaylightSavingTime(DateTime dateTime, TimeZoneInfoOptions flags, TimeZoneInfo.CachedData cachedData)
		{
			if (!this.m_supportsDaylightSavingTime || this.m_adjustmentRules == null)
			{
				return false;
			}
			DateTime dateTime2;
			if (dateTime.Kind == DateTimeKind.Local)
			{
				dateTime2 = TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, this, flags, cachedData);
			}
			else if (dateTime.Kind == DateTimeKind.Utc)
			{
				if (cachedData.GetCorrespondingKind(this) == DateTimeKind.Utc)
				{
					return false;
				}
				bool flag;
				TimeZoneInfo.GetUtcOffsetFromUtc(dateTime, this, out flag);
				return flag;
			}
			else
			{
				dateTime2 = dateTime;
			}
			TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = this.GetAdjustmentRuleForTime(dateTime2);
			if (adjustmentRuleForTime != null && adjustmentRuleForTime.HasDaylightSaving)
			{
				DaylightTimeStruct daylightTime = TimeZoneInfo.GetDaylightTime(dateTime2.Year, adjustmentRuleForTime);
				return TimeZoneInfo.GetIsDaylightSavings(dateTime2, adjustmentRuleForTime, daylightTime, flags);
			}
			return false;
		}

		/// <summary>Indicates whether a particular date and time is invalid.</summary>
		/// <param name="dateTime">A date and time value.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="dateTime" /> is invalid; otherwise, <see langword="false" />.</returns>
		// Token: 0x060013A6 RID: 5030 RVA: 0x00039688 File Offset: 0x00037888
		[__DynamicallyInvokable]
		public bool IsInvalidTime(DateTime dateTime)
		{
			bool flag = false;
			if (dateTime.Kind == DateTimeKind.Unspecified || (dateTime.Kind == DateTimeKind.Local && TimeZoneInfo.s_cachedData.GetCorrespondingKind(this) == DateTimeKind.Local))
			{
				TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = this.GetAdjustmentRuleForTime(dateTime);
				if (adjustmentRuleForTime != null && adjustmentRuleForTime.HasDaylightSaving)
				{
					DaylightTimeStruct daylightTime = TimeZoneInfo.GetDaylightTime(dateTime.Year, adjustmentRuleForTime);
					flag = TimeZoneInfo.GetIsInvalidTime(dateTime, adjustmentRuleForTime, daylightTime);
				}
				else
				{
					flag = false;
				}
			}
			return flag;
		}

		/// <summary>Clears cached time zone data.</summary>
		// Token: 0x060013A7 RID: 5031 RVA: 0x000396E7 File Offset: 0x000378E7
		public static void ClearCachedData()
		{
			TimeZoneInfo.s_cachedData = new TimeZoneInfo.CachedData();
		}

		/// <summary>Converts a time to the time in another time zone based on the time zone's identifier.</summary>
		/// <param name="dateTimeOffset">The date and time to convert.</param>
		/// <param name="destinationTimeZoneId">The identifier of the destination time zone.</param>
		/// <returns>The date and time in the destination time zone.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationTimeZoneId" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidTimeZoneException">The time zone identifier was found but the registry data is corrupted.</exception>
		/// <exception cref="T:System.Security.SecurityException">The process does not have the permissions required to read from the registry key that contains the time zone information.</exception>
		/// <exception cref="T:System.TimeZoneNotFoundException">The <paramref name="destinationTimeZoneId" /> identifier was not found on the local system.</exception>
		// Token: 0x060013A8 RID: 5032 RVA: 0x000396F3 File Offset: 0x000378F3
		public static DateTimeOffset ConvertTimeBySystemTimeZoneId(DateTimeOffset dateTimeOffset, string destinationTimeZoneId)
		{
			return TimeZoneInfo.ConvertTime(dateTimeOffset, TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId));
		}

		/// <summary>Converts a time to the time in another time zone based on the time zone's identifier.</summary>
		/// <param name="dateTime">The date and time to convert.</param>
		/// <param name="destinationTimeZoneId">The identifier of the destination time zone.</param>
		/// <returns>The date and time in the destination time zone.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationTimeZoneId" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidTimeZoneException">The time zone identifier was found, but the registry data is corrupted.</exception>
		/// <exception cref="T:System.Security.SecurityException">The process does not have the permissions required to read from the registry key that contains the time zone information.</exception>
		/// <exception cref="T:System.TimeZoneNotFoundException">The <paramref name="destinationTimeZoneId" /> identifier was not found on the local system.</exception>
		// Token: 0x060013A9 RID: 5033 RVA: 0x00039701 File Offset: 0x00037901
		public static DateTime ConvertTimeBySystemTimeZoneId(DateTime dateTime, string destinationTimeZoneId)
		{
			return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId));
		}

		/// <summary>Converts a time from one time zone to another based on time zone identifiers.</summary>
		/// <param name="dateTime">The date and time to convert.</param>
		/// <param name="sourceTimeZoneId">The identifier of the source time zone.</param>
		/// <param name="destinationTimeZoneId">The identifier of the destination time zone.</param>
		/// <returns>The date and time in the destination time zone that corresponds to the <paramref name="dateTime" /> parameter in the source time zone.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.DateTime.Kind" /> property of the <paramref name="dateTime" /> parameter does not correspond to the source time zone.  
		///  -or-  
		///  <paramref name="dateTime" /> is an invalid time in the source time zone.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceTimeZoneId" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="destinationTimeZoneId" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidTimeZoneException">The time zone identifiers were found, but the registry data is corrupted.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to read from the registry keys that hold time zone data.</exception>
		/// <exception cref="T:System.TimeZoneNotFoundException">The <paramref name="sourceTimeZoneId" /> identifier was not found on the local system.  
		///  -or-  
		///  The <paramref name="destinationTimeZoneId" /> identifier was not found on the local system.</exception>
		// Token: 0x060013AA RID: 5034 RVA: 0x00039710 File Offset: 0x00037910
		public static DateTime ConvertTimeBySystemTimeZoneId(DateTime dateTime, string sourceTimeZoneId, string destinationTimeZoneId)
		{
			if (dateTime.Kind == DateTimeKind.Local && string.Compare(sourceTimeZoneId, TimeZoneInfo.Local.Id, StringComparison.OrdinalIgnoreCase) == 0)
			{
				TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
				return TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId), TimeZoneInfoOptions.None, cachedData);
			}
			if (dateTime.Kind == DateTimeKind.Utc && string.Compare(sourceTimeZoneId, TimeZoneInfo.Utc.Id, StringComparison.OrdinalIgnoreCase) == 0)
			{
				TimeZoneInfo.CachedData cachedData2 = TimeZoneInfo.s_cachedData;
				return TimeZoneInfo.ConvertTime(dateTime, cachedData2.Utc, TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId), TimeZoneInfoOptions.None, cachedData2);
			}
			return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.FindSystemTimeZoneById(sourceTimeZoneId), TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId));
		}

		/// <summary>Converts a time to the time in a particular time zone.</summary>
		/// <param name="dateTimeOffset">The date and time to convert.</param>
		/// <param name="destinationTimeZone">The time zone to convert <paramref name="dateTime" /> to.</param>
		/// <returns>The date and time in the destination time zone.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="destinationTimeZone" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060013AB RID: 5035 RVA: 0x000397A0 File Offset: 0x000379A0
		[__DynamicallyInvokable]
		public static DateTimeOffset ConvertTime(DateTimeOffset dateTimeOffset, TimeZoneInfo destinationTimeZone)
		{
			if (destinationTimeZone == null)
			{
				throw new ArgumentNullException("destinationTimeZone");
			}
			DateTime utcDateTime = dateTimeOffset.UtcDateTime;
			TimeSpan utcOffsetFromUtc = TimeZoneInfo.GetUtcOffsetFromUtc(utcDateTime, destinationTimeZone);
			long num = utcDateTime.Ticks + utcOffsetFromUtc.Ticks;
			if (num > DateTimeOffset.MaxValue.Ticks)
			{
				return DateTimeOffset.MaxValue;
			}
			if (num < DateTimeOffset.MinValue.Ticks)
			{
				return DateTimeOffset.MinValue;
			}
			return new DateTimeOffset(num, utcOffsetFromUtc);
		}

		/// <summary>Converts a time to the time in a particular time zone.</summary>
		/// <param name="dateTime">The date and time to convert.</param>
		/// <param name="destinationTimeZone">The time zone to convert <paramref name="dateTime" /> to.</param>
		/// <returns>The date and time in the destination time zone.</returns>
		/// <exception cref="T:System.ArgumentException">The value of the <paramref name="dateTime" /> parameter represents an invalid time.</exception>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="destinationTimeZone" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060013AC RID: 5036 RVA: 0x00039810 File Offset: 0x00037A10
		[__DynamicallyInvokable]
		public static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo destinationTimeZone)
		{
			if (destinationTimeZone == null)
			{
				throw new ArgumentNullException("destinationTimeZone");
			}
			if (dateTime.Ticks == 0L)
			{
				TimeZoneInfo.ClearCachedData();
			}
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				return TimeZoneInfo.ConvertTime(dateTime, cachedData.Utc, destinationTimeZone, TimeZoneInfoOptions.None, cachedData);
			}
			return TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, destinationTimeZone, TimeZoneInfoOptions.None, cachedData);
		}

		/// <summary>Converts a time from one time zone to another.</summary>
		/// <param name="dateTime">The date and time to convert.</param>
		/// <param name="sourceTimeZone">The time zone of <paramref name="dateTime" />.</param>
		/// <param name="destinationTimeZone">The time zone to convert <paramref name="dateTime" /> to.</param>
		/// <returns>The date and time in the destination time zone that corresponds to the <paramref name="dateTime" /> parameter in the source time zone.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.DateTime.Kind" /> property of the <paramref name="dateTime" /> parameter is <see cref="F:System.DateTimeKind.Local" />, but the <paramref name="sourceTimeZone" /> parameter does not equal <see cref="F:System.DateTimeKind.Local" />.  
		///  -or-  
		///  The <see cref="P:System.DateTime.Kind" /> property of the <paramref name="dateTime" /> parameter is <see cref="F:System.DateTimeKind.Utc" />, but the <paramref name="sourceTimeZone" /> parameter does not equal <see cref="P:System.TimeZoneInfo.Utc" />.  
		///  -or-  
		///  The <paramref name="dateTime" /> parameter is an invalid time (that is, it represents a time that does not exist because of a time zone's adjustment rules).</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="sourceTimeZone" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="destinationTimeZone" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060013AD RID: 5037 RVA: 0x00039868 File Offset: 0x00037A68
		[__DynamicallyInvokable]
		public static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone)
		{
			return TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, destinationTimeZone, TimeZoneInfoOptions.None, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x00039878 File Offset: 0x00037A78
		internal static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone, TimeZoneInfoOptions flags)
		{
			return TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, destinationTimeZone, flags, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x00039888 File Offset: 0x00037A88
		private static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone, TimeZoneInfoOptions flags, TimeZoneInfo.CachedData cachedData)
		{
			if (sourceTimeZone == null)
			{
				throw new ArgumentNullException("sourceTimeZone");
			}
			if (destinationTimeZone == null)
			{
				throw new ArgumentNullException("destinationTimeZone");
			}
			DateTimeKind correspondingKind = cachedData.GetCorrespondingKind(sourceTimeZone);
			if ((flags & TimeZoneInfoOptions.NoThrowOnInvalidTime) == (TimeZoneInfoOptions)0 && dateTime.Kind != DateTimeKind.Unspecified && dateTime.Kind != correspondingKind)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ConvertMismatch"), "sourceTimeZone");
			}
			TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = sourceTimeZone.GetAdjustmentRuleForTime(dateTime);
			TimeSpan timeSpan = sourceTimeZone.BaseUtcOffset;
			if (adjustmentRuleForTime != null)
			{
				timeSpan += adjustmentRuleForTime.BaseUtcOffsetDelta;
				if (adjustmentRuleForTime.HasDaylightSaving)
				{
					DaylightTimeStruct daylightTime = TimeZoneInfo.GetDaylightTime(dateTime.Year, adjustmentRuleForTime);
					if ((flags & TimeZoneInfoOptions.NoThrowOnInvalidTime) == (TimeZoneInfoOptions)0 && TimeZoneInfo.GetIsInvalidTime(dateTime, adjustmentRuleForTime, daylightTime))
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeIsInvalid"), "dateTime");
					}
					bool isDaylightSavings = TimeZoneInfo.GetIsDaylightSavings(dateTime, adjustmentRuleForTime, daylightTime, flags);
					timeSpan += (isDaylightSavings ? adjustmentRuleForTime.DaylightDelta : TimeSpan.Zero);
				}
			}
			DateTimeKind correspondingKind2 = cachedData.GetCorrespondingKind(destinationTimeZone);
			if (dateTime.Kind != DateTimeKind.Unspecified && correspondingKind != DateTimeKind.Unspecified && correspondingKind == correspondingKind2)
			{
				return dateTime;
			}
			long num = dateTime.Ticks - timeSpan.Ticks;
			bool flag = false;
			DateTime dateTime2 = TimeZoneInfo.ConvertUtcToTimeZone(num, destinationTimeZone, out flag);
			if (correspondingKind2 == DateTimeKind.Local)
			{
				return new DateTime(dateTime2.Ticks, DateTimeKind.Local, flag);
			}
			return new DateTime(dateTime2.Ticks, correspondingKind2);
		}

		/// <summary>Converts a Coordinated Universal Time (UTC) to the time in a specified time zone.</summary>
		/// <param name="dateTime">The Coordinated Universal Time (UTC).</param>
		/// <param name="destinationTimeZone">The time zone to convert <paramref name="dateTime" /> to.</param>
		/// <returns>The date and time in the destination time zone. Its <see cref="P:System.DateTime.Kind" /> property is <see cref="F:System.DateTimeKind.Utc" /> if <paramref name="destinationTimeZone" /> is <see cref="P:System.TimeZoneInfo.Utc" />; otherwise, its <see cref="P:System.DateTime.Kind" /> property is <see cref="F:System.DateTimeKind.Unspecified" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.DateTime.Kind" /> property of <paramref name="dateTime" /> is <see cref="F:System.DateTimeKind.Local" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationTimeZone" /> is <see langword="null" />.</exception>
		// Token: 0x060013B0 RID: 5040 RVA: 0x000399C8 File Offset: 0x00037BC8
		public static DateTime ConvertTimeFromUtc(DateTime dateTime, TimeZoneInfo destinationTimeZone)
		{
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			return TimeZoneInfo.ConvertTime(dateTime, cachedData.Utc, destinationTimeZone, TimeZoneInfoOptions.None, cachedData);
		}

		/// <summary>Converts the specified date and time to Coordinated Universal Time (UTC).</summary>
		/// <param name="dateTime">The date and time to convert.</param>
		/// <returns>The Coordinated Universal Time (UTC) that corresponds to the <paramref name="dateTime" /> parameter. The <see cref="T:System.DateTime" /> value's <see cref="P:System.DateTime.Kind" /> property is always set to <see cref="F:System.DateTimeKind.Utc" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see langword="TimeZoneInfo.Local.IsInvalidDateTime(" />
		///   <paramref name="dateTime" />
		///   <see langword=")" /> returns <see langword="true" />.</exception>
		// Token: 0x060013B1 RID: 5041 RVA: 0x000399EC File Offset: 0x00037BEC
		public static DateTime ConvertTimeToUtc(DateTime dateTime)
		{
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				return dateTime;
			}
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			return TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, cachedData.Utc, TimeZoneInfoOptions.None, cachedData);
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x00039A20 File Offset: 0x00037C20
		internal static DateTime ConvertTimeToUtc(DateTime dateTime, TimeZoneInfoOptions flags)
		{
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				return dateTime;
			}
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			return TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, cachedData.Utc, flags, cachedData);
		}

		/// <summary>Converts the time in a specified time zone to Coordinated Universal Time (UTC).</summary>
		/// <param name="dateTime">The date and time to convert.</param>
		/// <param name="sourceTimeZone">The time zone of <paramref name="dateTime" />.</param>
		/// <returns>The Coordinated Universal Time (UTC) that corresponds to the <paramref name="dateTime" /> parameter. The <see cref="T:System.DateTime" /> object's <see cref="P:System.DateTime.Kind" /> property is always set to <see cref="F:System.DateTimeKind.Utc" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dateTime" />.<see langword="Kind" /> is <see cref="F:System.DateTimeKind.Utc" /> and <paramref name="sourceTimeZone" /> does not equal <see cref="P:System.TimeZoneInfo.Utc" />.  
		/// -or-  
		/// <paramref name="dateTime" />.<see langword="Kind" /> is <see cref="F:System.DateTimeKind.Local" /> and <paramref name="sourceTimeZone" /> does not equal <see cref="P:System.TimeZoneInfo.Local" />.  
		/// -or-  
		/// <paramref name="sourceTimeZone" /><see langword=".IsInvalidDateTime(" /><paramref name="dateTime" /><see langword=")" /> returns <see langword="true" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceTimeZone" /> is <see langword="null" />.</exception>
		// Token: 0x060013B3 RID: 5043 RVA: 0x00039A54 File Offset: 0x00037C54
		public static DateTime ConvertTimeToUtc(DateTime dateTime, TimeZoneInfo sourceTimeZone)
		{
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			return TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, cachedData.Utc, TimeZoneInfoOptions.None, cachedData);
		}

		/// <summary>Determines whether the current <see cref="T:System.TimeZoneInfo" /> object and another <see cref="T:System.TimeZoneInfo" /> object are equal.</summary>
		/// <param name="other">A second object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the two <see cref="T:System.TimeZoneInfo" /> objects are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x060013B4 RID: 5044 RVA: 0x00039A76 File Offset: 0x00037C76
		[__DynamicallyInvokable]
		public bool Equals(TimeZoneInfo other)
		{
			return other != null && string.Compare(this.m_id, other.m_id, StringComparison.OrdinalIgnoreCase) == 0 && this.HasSameRules(other);
		}

		/// <summary>Determines whether the current <see cref="T:System.TimeZoneInfo" /> object and another object are equal.</summary>
		/// <param name="obj">A second object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.TimeZoneInfo" /> object that is equal to the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x060013B5 RID: 5045 RVA: 0x00039A98 File Offset: 0x00037C98
		public override bool Equals(object obj)
		{
			TimeZoneInfo timeZoneInfo = obj as TimeZoneInfo;
			return timeZoneInfo != null && this.Equals(timeZoneInfo);
		}

		/// <summary>Deserializes a string to re-create an original serialized <see cref="T:System.TimeZoneInfo" /> object.</summary>
		/// <param name="source">The string representation of the serialized <see cref="T:System.TimeZoneInfo" /> object.</param>
		/// <returns>The original serialized object.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> parameter is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> parameter is a null string.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The source parameter cannot be deserialized back into a <see cref="T:System.TimeZoneInfo" /> object.</exception>
		// Token: 0x060013B6 RID: 5046 RVA: 0x00039AB8 File Offset: 0x00037CB8
		public static TimeZoneInfo FromSerializedString(string source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (source.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSerializedString", new object[] { source }), "source");
			}
			return TimeZoneInfo.StringSerializer.GetDeserializedTimeZoneInfo(source);
		}

		/// <summary>Serves as a hash function for hashing algorithms and data structures such as hash tables.</summary>
		/// <returns>A 32-bit signed integer that serves as the hash code for this <see cref="T:System.TimeZoneInfo" /> object.</returns>
		// Token: 0x060013B7 RID: 5047 RVA: 0x00039AF5 File Offset: 0x00037CF5
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.m_id.ToUpper(CultureInfo.InvariantCulture).GetHashCode();
		}

		/// <summary>Returns a sorted collection of all the time zones about which information is available on the local system.</summary>
		/// <returns>A read-only collection of <see cref="T:System.TimeZoneInfo" /> objects.</returns>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to store all time zone information.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to read from the registry keys that contain time zone information.</exception>
		// Token: 0x060013B8 RID: 5048 RVA: 0x00039B0C File Offset: 0x00037D0C
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static ReadOnlyCollection<TimeZoneInfo> GetSystemTimeZones()
		{
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			TimeZoneInfo.CachedData cachedData2 = cachedData;
			lock (cachedData2)
			{
				if (cachedData.m_readOnlySystemTimeZones == null)
				{
					PermissionSet permissionSet = new PermissionSet(PermissionState.None);
					permissionSet.AddPermission(new RegistryPermission(RegistryPermissionAccess.Read, "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones"));
					permissionSet.Assert();
					using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones", RegistryKeyPermissionCheck.Default, RegistryRights.ExecuteKey))
					{
						if (registryKey != null)
						{
							foreach (string text in registryKey.GetSubKeyNames())
							{
								TimeZoneInfo timeZoneInfo;
								Exception ex;
								TimeZoneInfo.TryGetTimeZone(text, false, out timeZoneInfo, out ex, cachedData);
							}
						}
						cachedData.m_allSystemTimeZonesRead = true;
					}
					List<TimeZoneInfo> list;
					if (cachedData.m_systemTimeZones != null)
					{
						list = new List<TimeZoneInfo>(cachedData.m_systemTimeZones.Values);
					}
					else
					{
						list = new List<TimeZoneInfo>();
					}
					list.Sort(new TimeZoneInfo.TimeZoneInfoComparer());
					cachedData.m_readOnlySystemTimeZones = new ReadOnlyCollection<TimeZoneInfo>(list);
				}
			}
			return cachedData.m_readOnlySystemTimeZones;
		}

		/// <summary>Indicates whether the current object and another <see cref="T:System.TimeZoneInfo" /> object have the same adjustment rules.</summary>
		/// <param name="other">A second object to compare with the current <see cref="T:System.TimeZoneInfo" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the two time zones have identical adjustment rules and an identical base offset; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="other" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060013B9 RID: 5049 RVA: 0x00039C20 File Offset: 0x00037E20
		public bool HasSameRules(TimeZoneInfo other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.m_baseUtcOffset != other.m_baseUtcOffset || this.m_supportsDaylightSavingTime != other.m_supportsDaylightSavingTime)
			{
				return false;
			}
			TimeZoneInfo.AdjustmentRule[] adjustmentRules = this.m_adjustmentRules;
			TimeZoneInfo.AdjustmentRule[] adjustmentRules2 = other.m_adjustmentRules;
			bool flag = (adjustmentRules == null && adjustmentRules2 == null) || (adjustmentRules != null && adjustmentRules2 != null);
			if (!flag)
			{
				return false;
			}
			if (adjustmentRules != null)
			{
				if (adjustmentRules.Length != adjustmentRules2.Length)
				{
					return false;
				}
				for (int i = 0; i < adjustmentRules.Length; i++)
				{
					if (!adjustmentRules[i].Equals(adjustmentRules2[i]))
					{
						return false;
					}
				}
			}
			return flag;
		}

		/// <summary>Gets a <see cref="T:System.TimeZoneInfo" /> object that represents the local time zone.</summary>
		/// <returns>An object that represents the local time zone.</returns>
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060013BA RID: 5050 RVA: 0x00039CB0 File Offset: 0x00037EB0
		[__DynamicallyInvokable]
		public static TimeZoneInfo Local
		{
			[__DynamicallyInvokable]
			get
			{
				return TimeZoneInfo.s_cachedData.Local;
			}
		}

		/// <summary>Converts the current <see cref="T:System.TimeZoneInfo" /> object to a serialized string.</summary>
		/// <returns>A string that represents the current <see cref="T:System.TimeZoneInfo" /> object.</returns>
		// Token: 0x060013BB RID: 5051 RVA: 0x00039CBC File Offset: 0x00037EBC
		public string ToSerializedString()
		{
			return TimeZoneInfo.StringSerializer.GetSerializedString(this);
		}

		/// <summary>Returns the current <see cref="T:System.TimeZoneInfo" /> object's display name.</summary>
		/// <returns>The value of the <see cref="P:System.TimeZoneInfo.DisplayName" /> property of the current <see cref="T:System.TimeZoneInfo" /> object.</returns>
		// Token: 0x060013BC RID: 5052 RVA: 0x00039CC4 File Offset: 0x00037EC4
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this.DisplayName;
		}

		/// <summary>Gets a <see cref="T:System.TimeZoneInfo" /> object that represents the Coordinated Universal Time (UTC) zone.</summary>
		/// <returns>An object that represents the Coordinated Universal Time (UTC) zone.</returns>
		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060013BD RID: 5053 RVA: 0x00039CCC File Offset: 0x00037ECC
		[__DynamicallyInvokable]
		public static TimeZoneInfo Utc
		{
			[__DynamicallyInvokable]
			get
			{
				return TimeZoneInfo.s_cachedData.Utc;
			}
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x00039CD8 File Offset: 0x00037ED8
		[SecurityCritical]
		private TimeZoneInfo(Win32Native.TimeZoneInformation zone, bool dstDisabled)
		{
			if (string.IsNullOrEmpty(zone.StandardName))
			{
				this.m_id = "Local";
			}
			else
			{
				this.m_id = zone.StandardName;
			}
			this.m_baseUtcOffset = new TimeSpan(0, -zone.Bias, 0);
			if (!dstDisabled)
			{
				Win32Native.RegistryTimeZoneInformation registryTimeZoneInformation = new Win32Native.RegistryTimeZoneInformation(zone);
				TimeZoneInfo.AdjustmentRule adjustmentRule = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(registryTimeZoneInformation, DateTime.MinValue.Date, DateTime.MaxValue.Date, zone.Bias);
				if (adjustmentRule != null)
				{
					this.m_adjustmentRules = new TimeZoneInfo.AdjustmentRule[1];
					this.m_adjustmentRules[0] = adjustmentRule;
				}
			}
			TimeZoneInfo.ValidateTimeZoneInfo(this.m_id, this.m_baseUtcOffset, this.m_adjustmentRules, out this.m_supportsDaylightSavingTime);
			this.m_displayName = zone.StandardName;
			this.m_standardDisplayName = zone.StandardName;
			this.m_daylightDisplayName = zone.DaylightName;
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x00039DB0 File Offset: 0x00037FB0
		private TimeZoneInfo(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, TimeZoneInfo.AdjustmentRule[] adjustmentRules, bool disableDaylightSavingTime)
		{
			bool flag;
			TimeZoneInfo.ValidateTimeZoneInfo(id, baseUtcOffset, adjustmentRules, out flag);
			if (!disableDaylightSavingTime && adjustmentRules != null && adjustmentRules.Length != 0)
			{
				this.m_adjustmentRules = (TimeZoneInfo.AdjustmentRule[])adjustmentRules.Clone();
			}
			this.m_id = id;
			this.m_baseUtcOffset = baseUtcOffset;
			this.m_displayName = displayName;
			this.m_standardDisplayName = standardDisplayName;
			this.m_daylightDisplayName = (disableDaylightSavingTime ? null : daylightDisplayName);
			this.m_supportsDaylightSavingTime = flag && !disableDaylightSavingTime;
		}

		/// <summary>Creates a custom time zone with a specified identifier, an offset from Coordinated Universal Time (UTC), a display name, and a standard time display name.</summary>
		/// <param name="id">The time zone's identifier.</param>
		/// <param name="baseUtcOffset">An object that represents the time difference between this time zone and Coordinated Universal Time (UTC).</param>
		/// <param name="displayName">The display name of the new time zone.</param>
		/// <param name="standardDisplayName">The name of the new time zone's standard time.</param>
		/// <returns>The new time zone.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="id" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="id" /> parameter is an empty string ("").  
		///  -or-  
		///  The <paramref name="baseUtcOffset" /> parameter does not represent a whole number of minutes.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="baseUtcOffset" /> parameter is greater than 14 hours or less than -14 hours.</exception>
		// Token: 0x060013C0 RID: 5056 RVA: 0x00039E2A File Offset: 0x0003802A
		public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName)
		{
			return new TimeZoneInfo(id, baseUtcOffset, displayName, standardDisplayName, standardDisplayName, null, false);
		}

		/// <summary>Creates a custom time zone with a specified identifier, an offset from Coordinated Universal Time (UTC), a display name, a standard time name, a daylight saving time name, and daylight saving time rules.</summary>
		/// <param name="id">The time zone's identifier.</param>
		/// <param name="baseUtcOffset">An object that represents the time difference between this time zone and Coordinated Universal Time (UTC).</param>
		/// <param name="displayName">The display name of the new time zone.</param>
		/// <param name="standardDisplayName">The new time zone's standard time name.</param>
		/// <param name="daylightDisplayName">The daylight saving time name of the new time zone.</param>
		/// <param name="adjustmentRules">An array that augments the base UTC offset for a particular period.</param>
		/// <returns>A <see cref="T:System.TimeZoneInfo" /> object that represents the new time zone.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="id" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="id" /> parameter is an empty string ("").  
		///  -or-  
		///  The <paramref name="baseUtcOffset" /> parameter does not represent a whole number of minutes.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="baseUtcOffset" /> parameter is greater than 14 hours or less than -14 hours.</exception>
		/// <exception cref="T:System.InvalidTimeZoneException">The adjustment rules specified in the <paramref name="adjustmentRules" /> parameter overlap.  
		///  -or-  
		///  The adjustment rules specified in the <paramref name="adjustmentRules" /> parameter are not in chronological order.  
		///  -or-  
		///  One or more elements in <paramref name="adjustmentRules" /> are <see langword="null" />.  
		///  -or-  
		///  A date can have multiple adjustment rules applied to it.  
		///  -or-  
		///  The sum of the <paramref name="baseUtcOffset" /> parameter and the <see cref="P:System.TimeZoneInfo.AdjustmentRule.DaylightDelta" /> value of one or more objects in the <paramref name="adjustmentRules" /> array is greater than 14 hours or less than -14 hours.</exception>
		// Token: 0x060013C1 RID: 5057 RVA: 0x00039E38 File Offset: 0x00038038
		public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, TimeZoneInfo.AdjustmentRule[] adjustmentRules)
		{
			return new TimeZoneInfo(id, baseUtcOffset, displayName, standardDisplayName, daylightDisplayName, adjustmentRules, false);
		}

		/// <summary>Creates a custom time zone with a specified identifier, an offset from Coordinated Universal Time (UTC), a display name, a standard time name, a daylight saving time name, daylight saving time rules, and a value that indicates whether the returned object reflects daylight saving time information.</summary>
		/// <param name="id">The time zone's identifier.</param>
		/// <param name="baseUtcOffset">A <see cref="T:System.TimeSpan" /> object that represents the time difference between this time zone and Coordinated Universal Time (UTC).</param>
		/// <param name="displayName">The display name of the new time zone.</param>
		/// <param name="standardDisplayName">The standard time name of the new time zone.</param>
		/// <param name="daylightDisplayName">The daylight saving time name of the new time zone.</param>
		/// <param name="adjustmentRules">An array of <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> objects that augment the base UTC offset for a particular period.</param>
		/// <param name="disableDaylightSavingTime">
		///   <see langword="true" /> to discard any daylight saving time-related information present in <paramref name="adjustmentRules" /> with the new object; otherwise, <see langword="false" />.</param>
		/// <returns>The new time zone. If the <paramref name="disableDaylightSavingTime" /> parameter is <see langword="true" />, the returned object has no daylight saving time data.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="id" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="id" /> parameter is an empty string ("").  
		///  -or-  
		///  The <paramref name="baseUtcOffset" /> parameter does not represent a whole number of minutes.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="baseUtcOffset" /> parameter is greater than 14 hours or less than -14 hours.</exception>
		/// <exception cref="T:System.InvalidTimeZoneException">The adjustment rules specified in the <paramref name="adjustmentRules" /> parameter overlap.  
		///  -or-  
		///  The adjustment rules specified in the <paramref name="adjustmentRules" /> parameter are not in chronological order.  
		///  -or-  
		///  One or more elements in <paramref name="adjustmentRules" /> are <see langword="null" />.  
		///  -or-  
		///  A date can have multiple adjustment rules applied to it.  
		///  -or-  
		///  The sum of the <paramref name="baseUtcOffset" /> parameter and the <see cref="P:System.TimeZoneInfo.AdjustmentRule.DaylightDelta" /> value of one or more objects in the <paramref name="adjustmentRules" /> array is greater than 14 hours or less than -14 hours.</exception>
		// Token: 0x060013C2 RID: 5058 RVA: 0x00039E48 File Offset: 0x00038048
		public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, TimeZoneInfo.AdjustmentRule[] adjustmentRules, bool disableDaylightSavingTime)
		{
			return new TimeZoneInfo(id, baseUtcOffset, displayName, standardDisplayName, daylightDisplayName, adjustmentRules, disableDaylightSavingTime);
		}

		/// <summary>Runs when the deserialization of an object has been completed.</summary>
		/// <param name="sender">The object that initiated the callback. The functionality for this parameter is not currently implemented.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.TimeZoneInfo" /> object contains invalid or corrupted data.</exception>
		// Token: 0x060013C3 RID: 5059 RVA: 0x00039E5C File Offset: 0x0003805C
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			try
			{
				bool flag;
				TimeZoneInfo.ValidateTimeZoneInfo(this.m_id, this.m_baseUtcOffset, this.m_adjustmentRules, out flag);
				if (flag != this.m_supportsDaylightSavingTime)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_CorruptField", new object[] { "SupportsDaylightSavingTime" }));
				}
			}
			catch (ArgumentException ex)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), ex);
			}
			catch (InvalidTimeZoneException ex2)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), ex2);
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data needed to serialize the current <see cref="T:System.TimeZoneInfo" /> object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to populate with data.</param>
		/// <param name="context">The destination for this serialization (see <see cref="T:System.Runtime.Serialization.StreamingContext" />).</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060013C4 RID: 5060 RVA: 0x00039EEC File Offset: 0x000380EC
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("Id", this.m_id);
			info.AddValue("DisplayName", this.m_displayName);
			info.AddValue("StandardName", this.m_standardDisplayName);
			info.AddValue("DaylightName", this.m_daylightDisplayName);
			info.AddValue("BaseUtcOffset", this.m_baseUtcOffset);
			info.AddValue("AdjustmentRules", this.m_adjustmentRules);
			info.AddValue("SupportsDaylightSavingTime", this.m_supportsDaylightSavingTime);
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x00039F84 File Offset: 0x00038184
		private TimeZoneInfo(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.m_id = (string)info.GetValue("Id", typeof(string));
			this.m_displayName = (string)info.GetValue("DisplayName", typeof(string));
			this.m_standardDisplayName = (string)info.GetValue("StandardName", typeof(string));
			this.m_daylightDisplayName = (string)info.GetValue("DaylightName", typeof(string));
			this.m_baseUtcOffset = (TimeSpan)info.GetValue("BaseUtcOffset", typeof(TimeSpan));
			this.m_adjustmentRules = (TimeZoneInfo.AdjustmentRule[])info.GetValue("AdjustmentRules", typeof(TimeZoneInfo.AdjustmentRule[]));
			this.m_supportsDaylightSavingTime = (bool)info.GetValue("SupportsDaylightSavingTime", typeof(bool));
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x0003A088 File Offset: 0x00038288
		private TimeZoneInfo.AdjustmentRule GetAdjustmentRuleForTime(DateTime dateTime)
		{
			if (this.m_adjustmentRules == null || this.m_adjustmentRules.Length == 0)
			{
				return null;
			}
			DateTime date = dateTime.Date;
			for (int i = 0; i < this.m_adjustmentRules.Length; i++)
			{
				if (this.m_adjustmentRules[i].DateStart <= date && this.m_adjustmentRules[i].DateEnd >= date)
				{
					return this.m_adjustmentRules[i];
				}
			}
			return null;
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x0003A0F8 File Offset: 0x000382F8
		[SecurityCritical]
		private static bool CheckDaylightSavingTimeNotSupported(Win32Native.TimeZoneInformation timeZone)
		{
			return timeZone.DaylightDate.Year == timeZone.StandardDate.Year && timeZone.DaylightDate.Month == timeZone.StandardDate.Month && timeZone.DaylightDate.DayOfWeek == timeZone.StandardDate.DayOfWeek && timeZone.DaylightDate.Day == timeZone.StandardDate.Day && timeZone.DaylightDate.Hour == timeZone.StandardDate.Hour && timeZone.DaylightDate.Minute == timeZone.StandardDate.Minute && timeZone.DaylightDate.Second == timeZone.StandardDate.Second && timeZone.DaylightDate.Milliseconds == timeZone.StandardDate.Milliseconds;
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x0003A1D0 File Offset: 0x000383D0
		private static DateTime ConvertUtcToTimeZone(long ticks, TimeZoneInfo destinationTimeZone, out bool isAmbiguousLocalDst)
		{
			DateTime dateTime;
			if (ticks > DateTime.MaxValue.Ticks)
			{
				dateTime = DateTime.MaxValue;
			}
			else if (ticks < DateTime.MinValue.Ticks)
			{
				dateTime = DateTime.MinValue;
			}
			else
			{
				dateTime = new DateTime(ticks);
			}
			ticks += TimeZoneInfo.GetUtcOffsetFromUtc(dateTime, destinationTimeZone, out isAmbiguousLocalDst).Ticks;
			DateTime dateTime2;
			if (ticks > DateTime.MaxValue.Ticks)
			{
				dateTime2 = DateTime.MaxValue;
			}
			else if (ticks < DateTime.MinValue.Ticks)
			{
				dateTime2 = DateTime.MinValue;
			}
			else
			{
				dateTime2 = new DateTime(ticks);
			}
			return dateTime2;
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x0003A264 File Offset: 0x00038464
		[SecurityCritical]
		private static TimeZoneInfo.AdjustmentRule CreateAdjustmentRuleFromTimeZoneInformation(Win32Native.RegistryTimeZoneInformation timeZoneInformation, DateTime startDate, DateTime endDate, int defaultBaseUtcOffset)
		{
			if (timeZoneInformation.StandardDate.Month == 0)
			{
				if (timeZoneInformation.Bias == defaultBaseUtcOffset)
				{
					return null;
				}
				return TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(startDate, endDate, TimeSpan.Zero, TimeZoneInfo.TransitionTime.CreateFixedDateRule(DateTime.MinValue, 1, 1), TimeZoneInfo.TransitionTime.CreateFixedDateRule(DateTime.MinValue.AddMilliseconds(1.0), 1, 1), new TimeSpan(0, defaultBaseUtcOffset - timeZoneInformation.Bias, 0));
			}
			else
			{
				TimeZoneInfo.TransitionTime transitionTime;
				if (!TimeZoneInfo.TransitionTimeFromTimeZoneInformation(timeZoneInformation, out transitionTime, true))
				{
					return null;
				}
				TimeZoneInfo.TransitionTime transitionTime2;
				if (!TimeZoneInfo.TransitionTimeFromTimeZoneInformation(timeZoneInformation, out transitionTime2, false))
				{
					return null;
				}
				if (transitionTime.Equals(transitionTime2))
				{
					return null;
				}
				return TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(startDate, endDate, new TimeSpan(0, -timeZoneInformation.DaylightBias, 0), transitionTime, transitionTime2, new TimeSpan(0, defaultBaseUtcOffset - timeZoneInformation.Bias, 0));
			}
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x0003A324 File Offset: 0x00038524
		[SecuritySafeCritical]
		private static string FindIdFromTimeZoneInformation(Win32Native.TimeZoneInformation timeZone, out bool dstDisabled)
		{
			dstDisabled = false;
			try
			{
				PermissionSet permissionSet = new PermissionSet(PermissionState.None);
				permissionSet.AddPermission(new RegistryPermission(RegistryPermissionAccess.Read, "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones"));
				permissionSet.Assert();
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones", RegistryKeyPermissionCheck.Default, RegistryRights.ExecuteKey))
				{
					if (registryKey == null)
					{
						return null;
					}
					foreach (string text in registryKey.GetSubKeyNames())
					{
						if (TimeZoneInfo.TryCompareTimeZoneInformationToRegistry(timeZone, text, out dstDisabled))
						{
							return text;
						}
					}
				}
			}
			finally
			{
				PermissionSet.RevertAssert();
			}
			return null;
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x0003A3D0 File Offset: 0x000385D0
		private static DaylightTimeStruct GetDaylightTime(int year, TimeZoneInfo.AdjustmentRule rule)
		{
			TimeSpan daylightDelta = rule.DaylightDelta;
			DateTime dateTime = TimeZoneInfo.TransitionTimeToDateTime(year, rule.DaylightTransitionStart);
			DateTime dateTime2 = TimeZoneInfo.TransitionTimeToDateTime(year, rule.DaylightTransitionEnd);
			return new DaylightTimeStruct(dateTime, dateTime2, daylightDelta);
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x0003A408 File Offset: 0x00038608
		private static bool GetIsDaylightSavings(DateTime time, TimeZoneInfo.AdjustmentRule rule, DaylightTimeStruct daylightTime, TimeZoneInfoOptions flags)
		{
			if (rule == null)
			{
				return false;
			}
			DateTime dateTime;
			DateTime dateTime2;
			if (time.Kind == DateTimeKind.Local)
			{
				dateTime = (rule.IsStartDateMarkerForBeginningOfYear() ? new DateTime(daylightTime.Start.Year, 1, 1, 0, 0, 0) : (daylightTime.Start + daylightTime.Delta));
				dateTime2 = (rule.IsEndDateMarkerForEndOfYear() ? new DateTime(daylightTime.End.Year + 1, 1, 1, 0, 0, 0).AddTicks(-1L) : daylightTime.End);
			}
			else
			{
				bool flag = rule.DaylightDelta > TimeSpan.Zero;
				dateTime = (rule.IsStartDateMarkerForBeginningOfYear() ? new DateTime(daylightTime.Start.Year, 1, 1, 0, 0, 0) : (daylightTime.Start + (flag ? rule.DaylightDelta : TimeSpan.Zero)));
				dateTime2 = (rule.IsEndDateMarkerForEndOfYear() ? new DateTime(daylightTime.End.Year + 1, 1, 1, 0, 0, 0).AddTicks(-1L) : (daylightTime.End + (flag ? (-rule.DaylightDelta) : TimeSpan.Zero)));
			}
			bool flag2 = TimeZoneInfo.CheckIsDst(dateTime, time, dateTime2, false);
			if (flag2 && time.Kind == DateTimeKind.Local && TimeZoneInfo.GetIsAmbiguousTime(time, rule, daylightTime))
			{
				flag2 = time.IsAmbiguousDaylightSavingTime();
			}
			return flag2;
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x0003A568 File Offset: 0x00038768
		private static bool GetIsDaylightSavingsFromUtc(DateTime time, int Year, TimeSpan utc, TimeZoneInfo.AdjustmentRule rule, out bool isAmbiguousLocalDst, TimeZoneInfo zone)
		{
			isAmbiguousLocalDst = false;
			if (rule == null)
			{
				return false;
			}
			TimeSpan timeSpan = utc + rule.BaseUtcOffsetDelta;
			DaylightTimeStruct daylightTime = TimeZoneInfo.GetDaylightTime(Year, rule);
			bool flag = false;
			DateTime dateTime;
			if (rule.IsStartDateMarkerForBeginningOfYear() && daylightTime.Start.Year > DateTime.MinValue.Year)
			{
				TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = zone.GetAdjustmentRuleForTime(new DateTime(daylightTime.Start.Year - 1, 12, 31));
				if (adjustmentRuleForTime != null && adjustmentRuleForTime.IsEndDateMarkerForEndOfYear())
				{
					dateTime = TimeZoneInfo.GetDaylightTime(daylightTime.Start.Year - 1, adjustmentRuleForTime).Start - utc - adjustmentRuleForTime.BaseUtcOffsetDelta;
					flag = true;
				}
				else
				{
					dateTime = new DateTime(daylightTime.Start.Year, 1, 1, 0, 0, 0) - timeSpan;
				}
			}
			else
			{
				dateTime = daylightTime.Start - timeSpan;
			}
			DateTime dateTime2;
			if (rule.IsEndDateMarkerForEndOfYear() && daylightTime.End.Year < DateTime.MaxValue.Year)
			{
				TimeZoneInfo.AdjustmentRule adjustmentRuleForTime2 = zone.GetAdjustmentRuleForTime(new DateTime(daylightTime.End.Year + 1, 1, 1));
				if (adjustmentRuleForTime2 != null && adjustmentRuleForTime2.IsStartDateMarkerForBeginningOfYear())
				{
					if (adjustmentRuleForTime2.IsEndDateMarkerForEndOfYear())
					{
						dateTime2 = new DateTime(daylightTime.End.Year + 1, 12, 31) - utc - adjustmentRuleForTime2.BaseUtcOffsetDelta - adjustmentRuleForTime2.DaylightDelta;
					}
					else
					{
						dateTime2 = TimeZoneInfo.GetDaylightTime(daylightTime.End.Year + 1, adjustmentRuleForTime2).End - utc - adjustmentRuleForTime2.BaseUtcOffsetDelta - adjustmentRuleForTime2.DaylightDelta;
					}
					flag = true;
				}
				else
				{
					dateTime2 = new DateTime(daylightTime.End.Year + 1, 1, 1, 0, 0, 0).AddTicks(-1L) - timeSpan - rule.DaylightDelta;
				}
			}
			else
			{
				dateTime2 = daylightTime.End - timeSpan - rule.DaylightDelta;
			}
			DateTime dateTime3;
			DateTime dateTime4;
			if (daylightTime.Delta.Ticks > 0L)
			{
				dateTime3 = dateTime2 - daylightTime.Delta;
				dateTime4 = dateTime2;
			}
			else
			{
				dateTime3 = dateTime;
				dateTime4 = dateTime - daylightTime.Delta;
			}
			bool flag2 = TimeZoneInfo.CheckIsDst(dateTime, time, dateTime2, flag);
			if (flag2)
			{
				isAmbiguousLocalDst = time >= dateTime3 && time < dateTime4;
				if (!isAmbiguousLocalDst && dateTime3.Year != dateTime4.Year)
				{
					try
					{
						DateTime dateTime5 = dateTime3.AddYears(1);
						DateTime dateTime6 = dateTime4.AddYears(1);
						isAmbiguousLocalDst = time >= dateTime3 && time < dateTime4;
					}
					catch (ArgumentOutOfRangeException)
					{
					}
					if (!isAmbiguousLocalDst)
					{
						try
						{
							DateTime dateTime5 = dateTime3.AddYears(-1);
							DateTime dateTime6 = dateTime4.AddYears(-1);
							isAmbiguousLocalDst = time >= dateTime3 && time < dateTime4;
						}
						catch (ArgumentOutOfRangeException)
						{
						}
					}
				}
			}
			return flag2;
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x0003A8A4 File Offset: 0x00038AA4
		private static bool CheckIsDst(DateTime startTime, DateTime time, DateTime endTime, bool ignoreYearAdjustment)
		{
			if (!ignoreYearAdjustment)
			{
				int year = startTime.Year;
				int year2 = endTime.Year;
				if (year != year2)
				{
					endTime = endTime.AddYears(year - year2);
				}
				int year3 = time.Year;
				if (year != year3)
				{
					time = time.AddYears(year - year3);
				}
			}
			bool flag;
			if (startTime > endTime)
			{
				flag = time < endTime || time >= startTime;
			}
			else
			{
				flag = time >= startTime && time < endTime;
			}
			return flag;
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x0003A920 File Offset: 0x00038B20
		private static bool GetIsAmbiguousTime(DateTime time, TimeZoneInfo.AdjustmentRule rule, DaylightTimeStruct daylightTime)
		{
			bool flag = false;
			if (rule == null || rule.DaylightDelta == TimeSpan.Zero)
			{
				return flag;
			}
			DateTime dateTime;
			DateTime dateTime2;
			if (rule.DaylightDelta > TimeSpan.Zero)
			{
				if (rule.IsEndDateMarkerForEndOfYear())
				{
					return false;
				}
				dateTime = daylightTime.End;
				dateTime2 = daylightTime.End - rule.DaylightDelta;
			}
			else
			{
				if (rule.IsStartDateMarkerForBeginningOfYear())
				{
					return false;
				}
				dateTime = daylightTime.Start;
				dateTime2 = daylightTime.Start + rule.DaylightDelta;
			}
			flag = time >= dateTime2 && time < dateTime;
			if (!flag && dateTime.Year != dateTime2.Year)
			{
				try
				{
					DateTime dateTime3 = dateTime.AddYears(1);
					DateTime dateTime4 = dateTime2.AddYears(1);
					flag = time >= dateTime4 && time < dateTime3;
				}
				catch (ArgumentOutOfRangeException)
				{
				}
				if (!flag)
				{
					try
					{
						DateTime dateTime3 = dateTime.AddYears(-1);
						DateTime dateTime4 = dateTime2.AddYears(-1);
						flag = time >= dateTime4 && time < dateTime3;
					}
					catch (ArgumentOutOfRangeException)
					{
					}
				}
			}
			return flag;
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x0003AA48 File Offset: 0x00038C48
		private static bool GetIsInvalidTime(DateTime time, TimeZoneInfo.AdjustmentRule rule, DaylightTimeStruct daylightTime)
		{
			bool flag = false;
			if (rule == null || rule.DaylightDelta == TimeSpan.Zero)
			{
				return flag;
			}
			DateTime dateTime;
			DateTime dateTime2;
			if (rule.DaylightDelta < TimeSpan.Zero)
			{
				if (rule.IsEndDateMarkerForEndOfYear())
				{
					return false;
				}
				dateTime = daylightTime.End;
				dateTime2 = daylightTime.End - rule.DaylightDelta;
			}
			else
			{
				if (rule.IsStartDateMarkerForBeginningOfYear())
				{
					return false;
				}
				dateTime = daylightTime.Start;
				dateTime2 = daylightTime.Start + rule.DaylightDelta;
			}
			flag = time >= dateTime && time < dateTime2;
			if (!flag && dateTime.Year != dateTime2.Year)
			{
				try
				{
					DateTime dateTime3 = dateTime.AddYears(1);
					DateTime dateTime4 = dateTime2.AddYears(1);
					flag = time >= dateTime3 && time < dateTime4;
				}
				catch (ArgumentOutOfRangeException)
				{
				}
				if (!flag)
				{
					try
					{
						DateTime dateTime3 = dateTime.AddYears(-1);
						DateTime dateTime4 = dateTime2.AddYears(-1);
						flag = time >= dateTime3 && time < dateTime4;
					}
					catch (ArgumentOutOfRangeException)
					{
					}
				}
			}
			return flag;
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x0003AB70 File Offset: 0x00038D70
		[SecuritySafeCritical]
		private static TimeZoneInfo GetLocalTimeZone(TimeZoneInfo.CachedData cachedData)
		{
			Win32Native.DynamicTimeZoneInformation dynamicTimeZoneInformation = default(Win32Native.DynamicTimeZoneInformation);
			long num = (long)UnsafeNativeMethods.GetDynamicTimeZoneInformation(out dynamicTimeZoneInformation);
			if (num == -1L)
			{
				return TimeZoneInfo.CreateCustomTimeZone("Local", TimeSpan.Zero, "Local", "Local");
			}
			Win32Native.TimeZoneInformation timeZoneInformation = new Win32Native.TimeZoneInformation(dynamicTimeZoneInformation);
			bool dynamicDaylightTimeDisabled = dynamicTimeZoneInformation.DynamicDaylightTimeDisabled;
			TimeZoneInfo timeZoneInfo;
			Exception ex;
			if (!string.IsNullOrEmpty(dynamicTimeZoneInformation.TimeZoneKeyName) && TimeZoneInfo.TryGetTimeZone(dynamicTimeZoneInformation.TimeZoneKeyName, dynamicDaylightTimeDisabled, out timeZoneInfo, out ex, cachedData) == TimeZoneInfo.TimeZoneInfoResult.Success)
			{
				return timeZoneInfo;
			}
			string text = TimeZoneInfo.FindIdFromTimeZoneInformation(timeZoneInformation, out dynamicDaylightTimeDisabled);
			TimeZoneInfo timeZoneInfo2;
			Exception ex2;
			if (text != null && TimeZoneInfo.TryGetTimeZone(text, dynamicDaylightTimeDisabled, out timeZoneInfo2, out ex2, cachedData) == TimeZoneInfo.TimeZoneInfoResult.Success)
			{
				return timeZoneInfo2;
			}
			return TimeZoneInfo.GetLocalTimeZoneFromWin32Data(timeZoneInformation, dynamicDaylightTimeDisabled);
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x0003AC0C File Offset: 0x00038E0C
		[SecurityCritical]
		private static TimeZoneInfo GetLocalTimeZoneFromWin32Data(Win32Native.TimeZoneInformation timeZoneInformation, bool dstDisabled)
		{
			try
			{
				return new TimeZoneInfo(timeZoneInformation, dstDisabled);
			}
			catch (ArgumentException)
			{
			}
			catch (InvalidTimeZoneException)
			{
			}
			if (!dstDisabled)
			{
				try
				{
					return new TimeZoneInfo(timeZoneInformation, true);
				}
				catch (ArgumentException)
				{
				}
				catch (InvalidTimeZoneException)
				{
				}
			}
			return TimeZoneInfo.CreateCustomTimeZone("Local", TimeSpan.Zero, "Local", "Local");
		}

		/// <summary>Instantiates a new <see cref="T:System.TimeZoneInfo" /> object based on its identifier.</summary>
		/// <param name="id">The time zone identifier, which corresponds to the <see cref="P:System.TimeZoneInfo.Id" /> property.</param>
		/// <returns>An object whose identifier is the value of the <paramref name="id" /> parameter.</returns>
		/// <exception cref="T:System.OutOfMemoryException">The system does not have enough memory to hold information about the time zone.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="id" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.TimeZoneNotFoundException">The time zone identifier specified by <paramref name="id" /> was not found. This means that a time zone identifier whose name matches <paramref name="id" /> does not exist, or that the identifier exists but does not contain any time zone data.</exception>
		/// <exception cref="T:System.Security.SecurityException">The process does not have the permissions required to read from the registry key that contains the time zone information.</exception>
		/// <exception cref="T:System.InvalidTimeZoneException">The time zone identifier was found, but the registry data is corrupted.</exception>
		// Token: 0x060013D3 RID: 5075 RVA: 0x0003AC8C File Offset: 0x00038E8C
		[__DynamicallyInvokable]
		public static TimeZoneInfo FindSystemTimeZoneById(string id)
		{
			if (string.Compare(id, "UTC", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return TimeZoneInfo.Utc;
			}
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (id.Length == 0 || id.Length > 255 || id.Contains("\0"))
			{
				throw new TimeZoneNotFoundException(Environment.GetResourceString("TimeZoneNotFound_MissingRegistryData", new object[] { id }));
			}
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			TimeZoneInfo.CachedData cachedData2 = cachedData;
			TimeZoneInfo timeZoneInfo;
			Exception ex;
			TimeZoneInfo.TimeZoneInfoResult timeZoneInfoResult;
			lock (cachedData2)
			{
				timeZoneInfoResult = TimeZoneInfo.TryGetTimeZone(id, false, out timeZoneInfo, out ex, cachedData);
			}
			if (timeZoneInfoResult == TimeZoneInfo.TimeZoneInfoResult.Success)
			{
				return timeZoneInfo;
			}
			if (timeZoneInfoResult == TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException)
			{
				throw new InvalidTimeZoneException(Environment.GetResourceString("InvalidTimeZone_InvalidRegistryData", new object[] { id }), ex);
			}
			if (timeZoneInfoResult == TimeZoneInfo.TimeZoneInfoResult.SecurityException)
			{
				throw new SecurityException(Environment.GetResourceString("Security_CannotReadRegistryData", new object[] { id }), ex);
			}
			throw new TimeZoneNotFoundException(Environment.GetResourceString("TimeZoneNotFound_MissingRegistryData", new object[] { id }), ex);
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x0003AD94 File Offset: 0x00038F94
		private static TimeSpan GetUtcOffset(DateTime time, TimeZoneInfo zone, TimeZoneInfoOptions flags)
		{
			TimeSpan timeSpan = zone.BaseUtcOffset;
			TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = zone.GetAdjustmentRuleForTime(time);
			if (adjustmentRuleForTime != null)
			{
				timeSpan += adjustmentRuleForTime.BaseUtcOffsetDelta;
				if (adjustmentRuleForTime.HasDaylightSaving)
				{
					DaylightTimeStruct daylightTime = TimeZoneInfo.GetDaylightTime(time.Year, adjustmentRuleForTime);
					bool isDaylightSavings = TimeZoneInfo.GetIsDaylightSavings(time, adjustmentRuleForTime, daylightTime, flags);
					timeSpan += (isDaylightSavings ? adjustmentRuleForTime.DaylightDelta : TimeSpan.Zero);
				}
			}
			return timeSpan;
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x0003ADF8 File Offset: 0x00038FF8
		private static TimeSpan GetUtcOffsetFromUtc(DateTime time, TimeZoneInfo zone)
		{
			bool flag;
			return TimeZoneInfo.GetUtcOffsetFromUtc(time, zone, out flag);
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x0003AE10 File Offset: 0x00039010
		private static TimeSpan GetUtcOffsetFromUtc(DateTime time, TimeZoneInfo zone, out bool isDaylightSavings)
		{
			bool flag;
			return TimeZoneInfo.GetUtcOffsetFromUtc(time, zone, out isDaylightSavings, out flag);
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x0003AE28 File Offset: 0x00039028
		internal static TimeSpan GetDateTimeNowUtcOffsetFromUtc(DateTime time, out bool isAmbiguousLocalDst)
		{
			isAmbiguousLocalDst = false;
			int year = time.Year;
			TimeZoneInfo.OffsetAndRule oneYearLocalFromUtc = TimeZoneInfo.s_cachedData.GetOneYearLocalFromUtc(year);
			TimeSpan timeSpan = oneYearLocalFromUtc.offset;
			if (oneYearLocalFromUtc.rule != null)
			{
				timeSpan += oneYearLocalFromUtc.rule.BaseUtcOffsetDelta;
				if (oneYearLocalFromUtc.rule.HasDaylightSaving)
				{
					bool isDaylightSavingsFromUtc = TimeZoneInfo.GetIsDaylightSavingsFromUtc(time, year, oneYearLocalFromUtc.offset, oneYearLocalFromUtc.rule, out isAmbiguousLocalDst, TimeZoneInfo.Local);
					timeSpan += (isDaylightSavingsFromUtc ? oneYearLocalFromUtc.rule.DaylightDelta : TimeSpan.Zero);
				}
			}
			return timeSpan;
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x0003AEB4 File Offset: 0x000390B4
		internal static TimeSpan GetUtcOffsetFromUtc(DateTime time, TimeZoneInfo zone, out bool isDaylightSavings, out bool isAmbiguousLocalDst)
		{
			isDaylightSavings = false;
			isAmbiguousLocalDst = false;
			TimeSpan timeSpan = zone.BaseUtcOffset;
			TimeZoneInfo.AdjustmentRule adjustmentRule;
			int num;
			if (time > TimeZoneInfo.s_maxDateOnly)
			{
				adjustmentRule = zone.GetAdjustmentRuleForTime(DateTime.MaxValue);
				num = 9999;
			}
			else if (time < TimeZoneInfo.s_minDateOnly)
			{
				adjustmentRule = zone.GetAdjustmentRuleForTime(DateTime.MinValue);
				num = 1;
			}
			else
			{
				DateTime dateTime = time + timeSpan;
				num = dateTime.Year;
				adjustmentRule = zone.GetAdjustmentRuleForTime(dateTime);
			}
			if (adjustmentRule != null)
			{
				timeSpan += adjustmentRule.BaseUtcOffsetDelta;
				if (adjustmentRule.HasDaylightSaving)
				{
					isDaylightSavings = TimeZoneInfo.GetIsDaylightSavingsFromUtc(time, num, zone.m_baseUtcOffset, adjustmentRule, out isAmbiguousLocalDst, zone);
					timeSpan += (isDaylightSavings ? adjustmentRule.DaylightDelta : TimeSpan.Zero);
				}
			}
			return timeSpan;
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x0003AF68 File Offset: 0x00039168
		[SecurityCritical]
		private static bool TransitionTimeFromTimeZoneInformation(Win32Native.RegistryTimeZoneInformation timeZoneInformation, out TimeZoneInfo.TransitionTime transitionTime, bool readStartDate)
		{
			if (timeZoneInformation.StandardDate.Month == 0)
			{
				transitionTime = default(TimeZoneInfo.TransitionTime);
				return false;
			}
			if (readStartDate)
			{
				if (timeZoneInformation.DaylightDate.Year == 0)
				{
					transitionTime = TimeZoneInfo.TransitionTime.CreateFloatingDateRule(new DateTime(1, 1, 1, (int)timeZoneInformation.DaylightDate.Hour, (int)timeZoneInformation.DaylightDate.Minute, (int)timeZoneInformation.DaylightDate.Second, (int)timeZoneInformation.DaylightDate.Milliseconds), (int)timeZoneInformation.DaylightDate.Month, (int)timeZoneInformation.DaylightDate.Day, (DayOfWeek)timeZoneInformation.DaylightDate.DayOfWeek);
				}
				else
				{
					transitionTime = TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1, (int)timeZoneInformation.DaylightDate.Hour, (int)timeZoneInformation.DaylightDate.Minute, (int)timeZoneInformation.DaylightDate.Second, (int)timeZoneInformation.DaylightDate.Milliseconds), (int)timeZoneInformation.DaylightDate.Month, (int)timeZoneInformation.DaylightDate.Day);
				}
			}
			else if (timeZoneInformation.StandardDate.Year == 0)
			{
				transitionTime = TimeZoneInfo.TransitionTime.CreateFloatingDateRule(new DateTime(1, 1, 1, (int)timeZoneInformation.StandardDate.Hour, (int)timeZoneInformation.StandardDate.Minute, (int)timeZoneInformation.StandardDate.Second, (int)timeZoneInformation.StandardDate.Milliseconds), (int)timeZoneInformation.StandardDate.Month, (int)timeZoneInformation.StandardDate.Day, (DayOfWeek)timeZoneInformation.StandardDate.DayOfWeek);
			}
			else
			{
				transitionTime = TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1, (int)timeZoneInformation.StandardDate.Hour, (int)timeZoneInformation.StandardDate.Minute, (int)timeZoneInformation.StandardDate.Second, (int)timeZoneInformation.StandardDate.Milliseconds), (int)timeZoneInformation.StandardDate.Month, (int)timeZoneInformation.StandardDate.Day);
			}
			return true;
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x0003B128 File Offset: 0x00039328
		private static DateTime TransitionTimeToDateTime(int year, TimeZoneInfo.TransitionTime transitionTime)
		{
			DateTime timeOfDay = transitionTime.TimeOfDay;
			DateTime dateTime;
			if (transitionTime.IsFixedDateRule)
			{
				int num = DateTime.DaysInMonth(year, transitionTime.Month);
				dateTime = new DateTime(year, transitionTime.Month, (num < transitionTime.Day) ? num : transitionTime.Day, timeOfDay.Hour, timeOfDay.Minute, timeOfDay.Second, timeOfDay.Millisecond);
			}
			else if (transitionTime.Week <= 4)
			{
				dateTime = new DateTime(year, transitionTime.Month, 1, timeOfDay.Hour, timeOfDay.Minute, timeOfDay.Second, timeOfDay.Millisecond);
				int dayOfWeek = (int)dateTime.DayOfWeek;
				int num2 = transitionTime.DayOfWeek - (DayOfWeek)dayOfWeek;
				if (num2 < 0)
				{
					num2 += 7;
				}
				num2 += 7 * (transitionTime.Week - 1);
				if (num2 > 0)
				{
					dateTime = dateTime.AddDays((double)num2);
				}
			}
			else
			{
				int num3 = DateTime.DaysInMonth(year, transitionTime.Month);
				dateTime = new DateTime(year, transitionTime.Month, num3, timeOfDay.Hour, timeOfDay.Minute, timeOfDay.Second, timeOfDay.Millisecond);
				int dayOfWeek2 = (int)dateTime.DayOfWeek;
				int num4 = dayOfWeek2 - (int)transitionTime.DayOfWeek;
				if (num4 < 0)
				{
					num4 += 7;
				}
				if (num4 > 0)
				{
					dateTime = dateTime.AddDays((double)(-(double)num4));
				}
			}
			return dateTime;
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x0003B27C File Offset: 0x0003947C
		[SecurityCritical]
		private static bool TryCreateAdjustmentRules(string id, Win32Native.RegistryTimeZoneInformation defaultTimeZoneInformation, out TimeZoneInfo.AdjustmentRule[] rules, out Exception e, int defaultBaseUtcOffset)
		{
			e = null;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(string.Format(CultureInfo.InvariantCulture, "{0}\\{1}\\Dynamic DST", "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones", id), RegistryKeyPermissionCheck.Default, RegistryRights.ExecuteKey))
				{
					if (registryKey == null)
					{
						TimeZoneInfo.AdjustmentRule adjustmentRule = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(defaultTimeZoneInformation, DateTime.MinValue.Date, DateTime.MaxValue.Date, defaultBaseUtcOffset);
						if (adjustmentRule == null)
						{
							rules = null;
						}
						else
						{
							rules = new TimeZoneInfo.AdjustmentRule[1];
							rules[0] = adjustmentRule;
						}
						return true;
					}
					int num = (int)registryKey.GetValue("FirstEntry", -1, RegistryValueOptions.None);
					int num2 = (int)registryKey.GetValue("LastEntry", -1, RegistryValueOptions.None);
					if (num == -1 || num2 == -1 || num > num2)
					{
						rules = null;
						return false;
					}
					byte[] array = registryKey.GetValue(num.ToString(CultureInfo.InvariantCulture), null, RegistryValueOptions.None) as byte[];
					if (array == null || array.Length != 44)
					{
						rules = null;
						return false;
					}
					Win32Native.RegistryTimeZoneInformation registryTimeZoneInformation = new Win32Native.RegistryTimeZoneInformation(array);
					if (num == num2)
					{
						TimeZoneInfo.AdjustmentRule adjustmentRule2 = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(registryTimeZoneInformation, DateTime.MinValue.Date, DateTime.MaxValue.Date, defaultBaseUtcOffset);
						if (adjustmentRule2 == null)
						{
							rules = null;
						}
						else
						{
							rules = new TimeZoneInfo.AdjustmentRule[1];
							rules[0] = adjustmentRule2;
						}
						return true;
					}
					List<TimeZoneInfo.AdjustmentRule> list = new List<TimeZoneInfo.AdjustmentRule>(1);
					TimeZoneInfo.AdjustmentRule adjustmentRule3 = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(registryTimeZoneInformation, DateTime.MinValue.Date, new DateTime(num, 12, 31), defaultBaseUtcOffset);
					if (adjustmentRule3 != null)
					{
						list.Add(adjustmentRule3);
					}
					for (int i = num + 1; i < num2; i++)
					{
						array = registryKey.GetValue(i.ToString(CultureInfo.InvariantCulture), null, RegistryValueOptions.None) as byte[];
						if (array == null || array.Length != 44)
						{
							rules = null;
							return false;
						}
						registryTimeZoneInformation = new Win32Native.RegistryTimeZoneInformation(array);
						TimeZoneInfo.AdjustmentRule adjustmentRule4 = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(registryTimeZoneInformation, new DateTime(i, 1, 1), new DateTime(i, 12, 31), defaultBaseUtcOffset);
						if (adjustmentRule4 != null)
						{
							list.Add(adjustmentRule4);
						}
					}
					array = registryKey.GetValue(num2.ToString(CultureInfo.InvariantCulture), null, RegistryValueOptions.None) as byte[];
					registryTimeZoneInformation = new Win32Native.RegistryTimeZoneInformation(array);
					if (array == null || array.Length != 44)
					{
						rules = null;
						return false;
					}
					TimeZoneInfo.AdjustmentRule adjustmentRule5 = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(registryTimeZoneInformation, new DateTime(num2, 1, 1), DateTime.MaxValue.Date, defaultBaseUtcOffset);
					if (adjustmentRule5 != null)
					{
						list.Add(adjustmentRule5);
					}
					rules = list.ToArray();
					if (rules != null && rules.Length == 0)
					{
						rules = null;
					}
				}
			}
			catch (InvalidCastException ex)
			{
				rules = null;
				e = ex;
				return false;
			}
			catch (ArgumentOutOfRangeException ex2)
			{
				rules = null;
				e = ex2;
				return false;
			}
			catch (ArgumentException ex3)
			{
				rules = null;
				e = ex3;
				return false;
			}
			return true;
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x0003B590 File Offset: 0x00039790
		[SecurityCritical]
		private static bool TryCompareStandardDate(Win32Native.TimeZoneInformation timeZone, Win32Native.RegistryTimeZoneInformation registryTimeZoneInfo)
		{
			return timeZone.Bias == registryTimeZoneInfo.Bias && timeZone.StandardBias == registryTimeZoneInfo.StandardBias && timeZone.StandardDate.Year == registryTimeZoneInfo.StandardDate.Year && timeZone.StandardDate.Month == registryTimeZoneInfo.StandardDate.Month && timeZone.StandardDate.DayOfWeek == registryTimeZoneInfo.StandardDate.DayOfWeek && timeZone.StandardDate.Day == registryTimeZoneInfo.StandardDate.Day && timeZone.StandardDate.Hour == registryTimeZoneInfo.StandardDate.Hour && timeZone.StandardDate.Minute == registryTimeZoneInfo.StandardDate.Minute && timeZone.StandardDate.Second == registryTimeZoneInfo.StandardDate.Second && timeZone.StandardDate.Milliseconds == registryTimeZoneInfo.StandardDate.Milliseconds;
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x0003B688 File Offset: 0x00039888
		[SecuritySafeCritical]
		private static bool TryCompareTimeZoneInformationToRegistry(Win32Native.TimeZoneInformation timeZone, string id, out bool dstDisabled)
		{
			dstDisabled = false;
			bool flag;
			try
			{
				PermissionSet permissionSet = new PermissionSet(PermissionState.None);
				permissionSet.AddPermission(new RegistryPermission(RegistryPermissionAccess.Read, "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones"));
				permissionSet.Assert();
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(string.Format(CultureInfo.InvariantCulture, "{0}\\{1}", "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones", id), RegistryKeyPermissionCheck.Default, RegistryRights.ExecuteKey))
				{
					if (registryKey == null)
					{
						flag = false;
					}
					else
					{
						byte[] array = (byte[])registryKey.GetValue("TZI", null, RegistryValueOptions.None);
						if (array == null || array.Length != 44)
						{
							flag = false;
						}
						else
						{
							Win32Native.RegistryTimeZoneInformation registryTimeZoneInformation = new Win32Native.RegistryTimeZoneInformation(array);
							if (!TimeZoneInfo.TryCompareStandardDate(timeZone, registryTimeZoneInformation))
							{
								flag = false;
							}
							else
							{
								bool flag2 = dstDisabled || TimeZoneInfo.CheckDaylightSavingTimeNotSupported(timeZone) || (timeZone.DaylightBias == registryTimeZoneInformation.DaylightBias && timeZone.DaylightDate.Year == registryTimeZoneInformation.DaylightDate.Year && timeZone.DaylightDate.Month == registryTimeZoneInformation.DaylightDate.Month && timeZone.DaylightDate.DayOfWeek == registryTimeZoneInformation.DaylightDate.DayOfWeek && timeZone.DaylightDate.Day == registryTimeZoneInformation.DaylightDate.Day && timeZone.DaylightDate.Hour == registryTimeZoneInformation.DaylightDate.Hour && timeZone.DaylightDate.Minute == registryTimeZoneInformation.DaylightDate.Minute && timeZone.DaylightDate.Second == registryTimeZoneInformation.DaylightDate.Second && timeZone.DaylightDate.Milliseconds == registryTimeZoneInformation.DaylightDate.Milliseconds);
								if (flag2)
								{
									string text = registryKey.GetValue("Std", string.Empty, RegistryValueOptions.None) as string;
									flag2 = string.Compare(text, timeZone.StandardName, StringComparison.Ordinal) == 0;
								}
								flag = flag2;
							}
						}
					}
				}
			}
			finally
			{
				PermissionSet.RevertAssert();
			}
			return flag;
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x0003B898 File Offset: 0x00039A98
		[SecuritySafeCritical]
		[FileIOPermission(SecurityAction.Assert, AllLocalFiles = FileIOPermissionAccess.PathDiscovery)]
		private static string TryGetLocalizedNameByMuiNativeResource(string resource)
		{
			if (string.IsNullOrEmpty(resource))
			{
				return string.Empty;
			}
			string[] array = resource.Split(new char[] { ',' }, StringSplitOptions.None);
			if (array.Length != 2)
			{
				return string.Empty;
			}
			string text = Environment.UnsafeGetFolderPath(Environment.SpecialFolder.System);
			string text2 = array[0].TrimStart(new char[] { '@' });
			string text3;
			try
			{
				text3 = Path.Combine(text, text2);
			}
			catch (ArgumentException)
			{
				return string.Empty;
			}
			int num;
			if (!int.TryParse(array[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
			{
				return string.Empty;
			}
			num = -num;
			string text4;
			try
			{
				StringBuilder stringBuilder = StringBuilderCache.Acquire(260);
				stringBuilder.Length = 260;
				int num2 = 260;
				int num3 = 0;
				long num4 = 0L;
				if (!UnsafeNativeMethods.GetFileMUIPath(16, text3, null, ref num3, stringBuilder, ref num2, ref num4))
				{
					StringBuilderCache.Release(stringBuilder);
					text4 = string.Empty;
				}
				else
				{
					text4 = TimeZoneInfo.TryGetLocalizedNameByNativeResource(StringBuilderCache.GetStringAndRelease(stringBuilder), num);
				}
			}
			catch (EntryPointNotFoundException)
			{
				text4 = string.Empty;
			}
			return text4;
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x0003B9A8 File Offset: 0x00039BA8
		[SecurityCritical]
		private static string TryGetLocalizedNameByNativeResource(string filePath, int resource)
		{
			using (SafeLibraryHandle safeLibraryHandle = UnsafeNativeMethods.LoadLibraryEx(filePath, IntPtr.Zero, 2))
			{
				if (!safeLibraryHandle.IsInvalid)
				{
					StringBuilder stringBuilder = StringBuilderCache.Acquire(500);
					stringBuilder.Length = 500;
					int num = UnsafeNativeMethods.LoadString(safeLibraryHandle, resource, stringBuilder, stringBuilder.Length);
					if (num != 0)
					{
						return StringBuilderCache.GetStringAndRelease(stringBuilder);
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x0003BA20 File Offset: 0x00039C20
		private static bool TryGetLocalizedNamesByRegistryKey(RegistryKey key, out string displayName, out string standardName, out string daylightName)
		{
			displayName = string.Empty;
			standardName = string.Empty;
			daylightName = string.Empty;
			string text = key.GetValue("MUI_Display", string.Empty, RegistryValueOptions.None) as string;
			string text2 = key.GetValue("MUI_Std", string.Empty, RegistryValueOptions.None) as string;
			string text3 = key.GetValue("MUI_Dlt", string.Empty, RegistryValueOptions.None) as string;
			if (!string.IsNullOrEmpty(text))
			{
				displayName = TimeZoneInfo.TryGetLocalizedNameByMuiNativeResource(text);
			}
			if (!string.IsNullOrEmpty(text2))
			{
				standardName = TimeZoneInfo.TryGetLocalizedNameByMuiNativeResource(text2);
			}
			if (!string.IsNullOrEmpty(text3))
			{
				daylightName = TimeZoneInfo.TryGetLocalizedNameByMuiNativeResource(text3);
			}
			if (string.IsNullOrEmpty(displayName))
			{
				displayName = key.GetValue("Display", string.Empty, RegistryValueOptions.None) as string;
			}
			if (string.IsNullOrEmpty(standardName))
			{
				standardName = key.GetValue("Std", string.Empty, RegistryValueOptions.None) as string;
			}
			if (string.IsNullOrEmpty(daylightName))
			{
				daylightName = key.GetValue("Dlt", string.Empty, RegistryValueOptions.None) as string;
			}
			return true;
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x0003BB1C File Offset: 0x00039D1C
		[SecuritySafeCritical]
		private static TimeZoneInfo.TimeZoneInfoResult TryGetTimeZoneByRegistryKey(string id, out TimeZoneInfo value, out Exception e)
		{
			e = null;
			TimeZoneInfo.TimeZoneInfoResult timeZoneInfoResult;
			try
			{
				PermissionSet permissionSet = new PermissionSet(PermissionState.None);
				permissionSet.AddPermission(new RegistryPermission(RegistryPermissionAccess.Read, "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones"));
				permissionSet.Assert();
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(string.Format(CultureInfo.InvariantCulture, "{0}\\{1}", "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones", id), RegistryKeyPermissionCheck.Default, RegistryRights.ExecuteKey))
				{
					if (registryKey == null)
					{
						value = null;
						timeZoneInfoResult = TimeZoneInfo.TimeZoneInfoResult.TimeZoneNotFoundException;
					}
					else
					{
						byte[] array = registryKey.GetValue("TZI", null, RegistryValueOptions.None) as byte[];
						if (array == null || array.Length != 44)
						{
							value = null;
							timeZoneInfoResult = TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException;
						}
						else
						{
							Win32Native.RegistryTimeZoneInformation registryTimeZoneInformation = new Win32Native.RegistryTimeZoneInformation(array);
							TimeZoneInfo.AdjustmentRule[] array2;
							string text;
							string text2;
							string text3;
							if (!TimeZoneInfo.TryCreateAdjustmentRules(id, registryTimeZoneInformation, out array2, out e, registryTimeZoneInformation.Bias))
							{
								value = null;
								timeZoneInfoResult = TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException;
							}
							else if (!TimeZoneInfo.TryGetLocalizedNamesByRegistryKey(registryKey, out text, out text2, out text3))
							{
								value = null;
								timeZoneInfoResult = TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException;
							}
							else
							{
								try
								{
									value = new TimeZoneInfo(id, new TimeSpan(0, -registryTimeZoneInformation.Bias, 0), text, text2, text3, array2, false);
									timeZoneInfoResult = TimeZoneInfo.TimeZoneInfoResult.Success;
								}
								catch (ArgumentException ex)
								{
									value = null;
									e = ex;
									timeZoneInfoResult = TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException;
								}
								catch (InvalidTimeZoneException ex2)
								{
									value = null;
									e = ex2;
									timeZoneInfoResult = TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException;
								}
							}
						}
					}
				}
			}
			finally
			{
				PermissionSet.RevertAssert();
			}
			return timeZoneInfoResult;
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x0003BC68 File Offset: 0x00039E68
		private static TimeZoneInfo.TimeZoneInfoResult TryGetTimeZone(string id, bool dstDisabled, out TimeZoneInfo value, out Exception e, TimeZoneInfo.CachedData cachedData)
		{
			TimeZoneInfo.TimeZoneInfoResult timeZoneInfoResult = TimeZoneInfo.TimeZoneInfoResult.Success;
			e = null;
			TimeZoneInfo timeZoneInfo = null;
			if (cachedData.m_systemTimeZones != null && cachedData.m_systemTimeZones.TryGetValue(id, out timeZoneInfo))
			{
				if (dstDisabled && timeZoneInfo.m_supportsDaylightSavingTime)
				{
					value = TimeZoneInfo.CreateCustomTimeZone(timeZoneInfo.m_id, timeZoneInfo.m_baseUtcOffset, timeZoneInfo.m_displayName, timeZoneInfo.m_standardDisplayName);
				}
				else
				{
					value = new TimeZoneInfo(timeZoneInfo.m_id, timeZoneInfo.m_baseUtcOffset, timeZoneInfo.m_displayName, timeZoneInfo.m_standardDisplayName, timeZoneInfo.m_daylightDisplayName, timeZoneInfo.m_adjustmentRules, false);
				}
				return timeZoneInfoResult;
			}
			if (!cachedData.m_allSystemTimeZonesRead)
			{
				timeZoneInfoResult = TimeZoneInfo.TryGetTimeZoneByRegistryKey(id, out timeZoneInfo, out e);
				if (timeZoneInfoResult == TimeZoneInfo.TimeZoneInfoResult.Success)
				{
					if (cachedData.m_systemTimeZones == null)
					{
						cachedData.m_systemTimeZones = new Dictionary<string, TimeZoneInfo>();
					}
					cachedData.m_systemTimeZones.Add(id, timeZoneInfo);
					if (dstDisabled && timeZoneInfo.m_supportsDaylightSavingTime)
					{
						value = TimeZoneInfo.CreateCustomTimeZone(timeZoneInfo.m_id, timeZoneInfo.m_baseUtcOffset, timeZoneInfo.m_displayName, timeZoneInfo.m_standardDisplayName);
					}
					else
					{
						value = new TimeZoneInfo(timeZoneInfo.m_id, timeZoneInfo.m_baseUtcOffset, timeZoneInfo.m_displayName, timeZoneInfo.m_standardDisplayName, timeZoneInfo.m_daylightDisplayName, timeZoneInfo.m_adjustmentRules, false);
					}
				}
				else
				{
					value = null;
				}
			}
			else
			{
				timeZoneInfoResult = TimeZoneInfo.TimeZoneInfoResult.TimeZoneNotFoundException;
				value = null;
			}
			return timeZoneInfoResult;
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x0003BD91 File Offset: 0x00039F91
		internal static bool UtcOffsetOutOfRange(TimeSpan offset)
		{
			return offset.TotalHours < -14.0 || offset.TotalHours > 14.0;
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x0003BDBC File Offset: 0x00039FBC
		private static void ValidateTimeZoneInfo(string id, TimeSpan baseUtcOffset, TimeZoneInfo.AdjustmentRule[] adjustmentRules, out bool adjustmentRulesSupportDst)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (id.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidId", new object[] { id }), "id");
			}
			if (TimeZoneInfo.UtcOffsetOutOfRange(baseUtcOffset))
			{
				throw new ArgumentOutOfRangeException("baseUtcOffset", Environment.GetResourceString("ArgumentOutOfRange_UtcOffset"));
			}
			if (baseUtcOffset.Ticks % 600000000L != 0L)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_TimeSpanHasSeconds"), "baseUtcOffset");
			}
			adjustmentRulesSupportDst = false;
			if (adjustmentRules != null && adjustmentRules.Length != 0)
			{
				adjustmentRulesSupportDst = true;
				TimeZoneInfo.AdjustmentRule adjustmentRule = null;
				for (int i = 0; i < adjustmentRules.Length; i++)
				{
					TimeZoneInfo.AdjustmentRule adjustmentRule2 = adjustmentRule;
					adjustmentRule = adjustmentRules[i];
					if (adjustmentRule == null)
					{
						throw new InvalidTimeZoneException(Environment.GetResourceString("Argument_AdjustmentRulesNoNulls"));
					}
					if (TimeZoneInfo.UtcOffsetOutOfRange(baseUtcOffset + adjustmentRule.DaylightDelta))
					{
						throw new InvalidTimeZoneException(Environment.GetResourceString("ArgumentOutOfRange_UtcOffsetAndDaylightDelta"));
					}
					if (adjustmentRule2 != null && adjustmentRule.DateStart <= adjustmentRule2.DateEnd)
					{
						throw new InvalidTimeZoneException(Environment.GetResourceString("Argument_AdjustmentRulesOutOfOrder"));
					}
				}
			}
		}

		// Token: 0x040006AD RID: 1709
		private string m_id;

		// Token: 0x040006AE RID: 1710
		private string m_displayName;

		// Token: 0x040006AF RID: 1711
		private string m_standardDisplayName;

		// Token: 0x040006B0 RID: 1712
		private string m_daylightDisplayName;

		// Token: 0x040006B1 RID: 1713
		private TimeSpan m_baseUtcOffset;

		// Token: 0x040006B2 RID: 1714
		private bool m_supportsDaylightSavingTime;

		// Token: 0x040006B3 RID: 1715
		private TimeZoneInfo.AdjustmentRule[] m_adjustmentRules;

		// Token: 0x040006B4 RID: 1716
		private const string c_timeZonesRegistryHive = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones";

		// Token: 0x040006B5 RID: 1717
		private const string c_timeZonesRegistryHivePermissionList = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones";

		// Token: 0x040006B6 RID: 1718
		private const string c_displayValue = "Display";

		// Token: 0x040006B7 RID: 1719
		private const string c_daylightValue = "Dlt";

		// Token: 0x040006B8 RID: 1720
		private const string c_standardValue = "Std";

		// Token: 0x040006B9 RID: 1721
		private const string c_muiDisplayValue = "MUI_Display";

		// Token: 0x040006BA RID: 1722
		private const string c_muiDaylightValue = "MUI_Dlt";

		// Token: 0x040006BB RID: 1723
		private const string c_muiStandardValue = "MUI_Std";

		// Token: 0x040006BC RID: 1724
		private const string c_timeZoneInfoValue = "TZI";

		// Token: 0x040006BD RID: 1725
		private const string c_firstEntryValue = "FirstEntry";

		// Token: 0x040006BE RID: 1726
		private const string c_lastEntryValue = "LastEntry";

		// Token: 0x040006BF RID: 1727
		private const string c_utcId = "UTC";

		// Token: 0x040006C0 RID: 1728
		private const string c_localId = "Local";

		// Token: 0x040006C1 RID: 1729
		private const int c_maxKeyLength = 255;

		// Token: 0x040006C2 RID: 1730
		private const int c_regByteLength = 44;

		// Token: 0x040006C3 RID: 1731
		private const long c_ticksPerMillisecond = 10000L;

		// Token: 0x040006C4 RID: 1732
		private const long c_ticksPerSecond = 10000000L;

		// Token: 0x040006C5 RID: 1733
		private const long c_ticksPerMinute = 600000000L;

		// Token: 0x040006C6 RID: 1734
		private const long c_ticksPerHour = 36000000000L;

		// Token: 0x040006C7 RID: 1735
		private const long c_ticksPerDay = 864000000000L;

		// Token: 0x040006C8 RID: 1736
		private const long c_ticksPerDayRange = 863999990000L;

		// Token: 0x040006C9 RID: 1737
		private static TimeZoneInfo.CachedData s_cachedData = new TimeZoneInfo.CachedData();

		// Token: 0x040006CA RID: 1738
		private static DateTime s_maxDateOnly = new DateTime(9999, 12, 31);

		// Token: 0x040006CB RID: 1739
		private static DateTime s_minDateOnly = new DateTime(1, 1, 2);

		// Token: 0x02000AFB RID: 2811
		private enum TimeZoneInfoResult
		{
			// Token: 0x0400320B RID: 12811
			Success,
			// Token: 0x0400320C RID: 12812
			TimeZoneNotFoundException,
			// Token: 0x0400320D RID: 12813
			InvalidTimeZoneException,
			// Token: 0x0400320E RID: 12814
			SecurityException
		}

		// Token: 0x02000AFC RID: 2812
		private class CachedData
		{
			// Token: 0x06006A5E RID: 27230 RVA: 0x0016F574 File Offset: 0x0016D774
			private TimeZoneInfo CreateLocal()
			{
				TimeZoneInfo timeZoneInfo2;
				lock (this)
				{
					TimeZoneInfo timeZoneInfo = this.m_localTimeZone;
					if (timeZoneInfo == null)
					{
						timeZoneInfo = TimeZoneInfo.GetLocalTimeZone(this);
						timeZoneInfo = new TimeZoneInfo(timeZoneInfo.m_id, timeZoneInfo.m_baseUtcOffset, timeZoneInfo.m_displayName, timeZoneInfo.m_standardDisplayName, timeZoneInfo.m_daylightDisplayName, timeZoneInfo.m_adjustmentRules, false);
						this.m_localTimeZone = timeZoneInfo;
					}
					timeZoneInfo2 = timeZoneInfo;
				}
				return timeZoneInfo2;
			}

			// Token: 0x170011F8 RID: 4600
			// (get) Token: 0x06006A5F RID: 27231 RVA: 0x0016F5F4 File Offset: 0x0016D7F4
			public TimeZoneInfo Local
			{
				get
				{
					TimeZoneInfo timeZoneInfo = this.m_localTimeZone;
					if (timeZoneInfo == null)
					{
						timeZoneInfo = this.CreateLocal();
					}
					return timeZoneInfo;
				}
			}

			// Token: 0x06006A60 RID: 27232 RVA: 0x0016F618 File Offset: 0x0016D818
			private TimeZoneInfo CreateUtc()
			{
				TimeZoneInfo timeZoneInfo2;
				lock (this)
				{
					TimeZoneInfo timeZoneInfo = this.m_utcTimeZone;
					if (timeZoneInfo == null)
					{
						timeZoneInfo = TimeZoneInfo.CreateCustomTimeZone("UTC", TimeSpan.Zero, "UTC", "UTC");
						this.m_utcTimeZone = timeZoneInfo;
					}
					timeZoneInfo2 = timeZoneInfo;
				}
				return timeZoneInfo2;
			}

			// Token: 0x170011F9 RID: 4601
			// (get) Token: 0x06006A61 RID: 27233 RVA: 0x0016F680 File Offset: 0x0016D880
			public TimeZoneInfo Utc
			{
				get
				{
					TimeZoneInfo timeZoneInfo = this.m_utcTimeZone;
					if (timeZoneInfo == null)
					{
						timeZoneInfo = this.CreateUtc();
					}
					return timeZoneInfo;
				}
			}

			// Token: 0x06006A62 RID: 27234 RVA: 0x0016F6A4 File Offset: 0x0016D8A4
			public DateTimeKind GetCorrespondingKind(TimeZoneInfo timeZone)
			{
				DateTimeKind dateTimeKind;
				if (timeZone == this.m_utcTimeZone)
				{
					dateTimeKind = DateTimeKind.Utc;
				}
				else if (timeZone == this.m_localTimeZone)
				{
					dateTimeKind = DateTimeKind.Local;
				}
				else
				{
					dateTimeKind = DateTimeKind.Unspecified;
				}
				return dateTimeKind;
			}

			// Token: 0x06006A63 RID: 27235 RVA: 0x0016F6D4 File Offset: 0x0016D8D4
			[SecuritySafeCritical]
			private static TimeZoneInfo GetCurrentOneYearLocal()
			{
				Win32Native.TimeZoneInformation timeZoneInformation = default(Win32Native.TimeZoneInformation);
				long num = (long)UnsafeNativeMethods.GetTimeZoneInformation(out timeZoneInformation);
				TimeZoneInfo timeZoneInfo;
				if (num == -1L)
				{
					timeZoneInfo = TimeZoneInfo.CreateCustomTimeZone("Local", TimeSpan.Zero, "Local", "Local");
				}
				else
				{
					timeZoneInfo = TimeZoneInfo.GetLocalTimeZoneFromWin32Data(timeZoneInformation, false);
				}
				return timeZoneInfo;
			}

			// Token: 0x06006A64 RID: 27236 RVA: 0x0016F71C File Offset: 0x0016D91C
			public TimeZoneInfo.OffsetAndRule GetOneYearLocalFromUtc(int year)
			{
				TimeZoneInfo.OffsetAndRule offsetAndRule = this.m_oneYearLocalFromUtc;
				if (offsetAndRule == null || offsetAndRule.year != year)
				{
					TimeZoneInfo currentOneYearLocal = TimeZoneInfo.CachedData.GetCurrentOneYearLocal();
					TimeZoneInfo.AdjustmentRule adjustmentRule = ((currentOneYearLocal.m_adjustmentRules == null) ? null : currentOneYearLocal.m_adjustmentRules[0]);
					offsetAndRule = new TimeZoneInfo.OffsetAndRule(year, currentOneYearLocal.BaseUtcOffset, adjustmentRule);
					this.m_oneYearLocalFromUtc = offsetAndRule;
				}
				return offsetAndRule;
			}

			// Token: 0x0400320F RID: 12815
			private volatile TimeZoneInfo m_localTimeZone;

			// Token: 0x04003210 RID: 12816
			private volatile TimeZoneInfo m_utcTimeZone;

			// Token: 0x04003211 RID: 12817
			public Dictionary<string, TimeZoneInfo> m_systemTimeZones;

			// Token: 0x04003212 RID: 12818
			public ReadOnlyCollection<TimeZoneInfo> m_readOnlySystemTimeZones;

			// Token: 0x04003213 RID: 12819
			public bool m_allSystemTimeZonesRead;

			// Token: 0x04003214 RID: 12820
			private volatile TimeZoneInfo.OffsetAndRule m_oneYearLocalFromUtc;
		}

		// Token: 0x02000AFD RID: 2813
		private class OffsetAndRule
		{
			// Token: 0x06006A66 RID: 27238 RVA: 0x0016F778 File Offset: 0x0016D978
			public OffsetAndRule(int year, TimeSpan offset, TimeZoneInfo.AdjustmentRule rule)
			{
				this.year = year;
				this.offset = offset;
				this.rule = rule;
			}

			// Token: 0x04003215 RID: 12821
			public int year;

			// Token: 0x04003216 RID: 12822
			public TimeSpan offset;

			// Token: 0x04003217 RID: 12823
			public TimeZoneInfo.AdjustmentRule rule;
		}

		/// <summary>Provides information about a time zone adjustment, such as the transition to and from daylight saving time.</summary>
		// Token: 0x02000AFE RID: 2814
		[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
		[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
		[Serializable]
		public sealed class AdjustmentRule : IEquatable<TimeZoneInfo.AdjustmentRule>, ISerializable, IDeserializationCallback
		{
			/// <summary>Gets the date when the adjustment rule takes effect.</summary>
			/// <returns>A <see cref="T:System.DateTime" /> value that indicates when the adjustment rule takes effect.</returns>
			// Token: 0x170011FA RID: 4602
			// (get) Token: 0x06006A67 RID: 27239 RVA: 0x0016F795 File Offset: 0x0016D995
			public DateTime DateStart
			{
				get
				{
					return this.m_dateStart;
				}
			}

			/// <summary>Gets the date when the adjustment rule ceases to be in effect.</summary>
			/// <returns>A <see cref="T:System.DateTime" /> value that indicates the end date of the adjustment rule.</returns>
			// Token: 0x170011FB RID: 4603
			// (get) Token: 0x06006A68 RID: 27240 RVA: 0x0016F79D File Offset: 0x0016D99D
			public DateTime DateEnd
			{
				get
				{
					return this.m_dateEnd;
				}
			}

			/// <summary>Gets the amount of time that is required to form the time zone's daylight saving time. This amount of time is added to the time zone's offset from Coordinated Universal Time (UTC).</summary>
			/// <returns>A <see cref="T:System.TimeSpan" /> object that indicates the amount of time to add to the standard time changes as a result of the adjustment rule.</returns>
			// Token: 0x170011FC RID: 4604
			// (get) Token: 0x06006A69 RID: 27241 RVA: 0x0016F7A5 File Offset: 0x0016D9A5
			public TimeSpan DaylightDelta
			{
				get
				{
					return this.m_daylightDelta;
				}
			}

			/// <summary>Gets information about the annual transition from standard time to daylight saving time.</summary>
			/// <returns>A <see cref="T:System.TimeZoneInfo.TransitionTime" /> object that defines the annual transition from a time zone's standard time to daylight saving time.</returns>
			// Token: 0x170011FD RID: 4605
			// (get) Token: 0x06006A6A RID: 27242 RVA: 0x0016F7AD File Offset: 0x0016D9AD
			public TimeZoneInfo.TransitionTime DaylightTransitionStart
			{
				get
				{
					return this.m_daylightTransitionStart;
				}
			}

			/// <summary>Gets information about the annual transition from daylight saving time back to standard time.</summary>
			/// <returns>A <see cref="T:System.TimeZoneInfo.TransitionTime" /> object that defines the annual transition from daylight saving time back to the time zone's standard time.</returns>
			// Token: 0x170011FE RID: 4606
			// (get) Token: 0x06006A6B RID: 27243 RVA: 0x0016F7B5 File Offset: 0x0016D9B5
			public TimeZoneInfo.TransitionTime DaylightTransitionEnd
			{
				get
				{
					return this.m_daylightTransitionEnd;
				}
			}

			// Token: 0x170011FF RID: 4607
			// (get) Token: 0x06006A6C RID: 27244 RVA: 0x0016F7BD File Offset: 0x0016D9BD
			internal TimeSpan BaseUtcOffsetDelta
			{
				get
				{
					return this.m_baseUtcOffsetDelta;
				}
			}

			// Token: 0x17001200 RID: 4608
			// (get) Token: 0x06006A6D RID: 27245 RVA: 0x0016F7C8 File Offset: 0x0016D9C8
			internal bool HasDaylightSaving
			{
				get
				{
					return this.DaylightDelta != TimeSpan.Zero || this.DaylightTransitionStart.TimeOfDay != DateTime.MinValue || this.DaylightTransitionEnd.TimeOfDay != DateTime.MinValue.AddMilliseconds(1.0);
				}
			}

			/// <summary>Determines whether the current <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> object is equal to a second <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> object.</summary>
			/// <param name="other">The object to compare with the current object.</param>
			/// <returns>
			///   <see langword="true" /> if both <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> objects have equal values; otherwise, <see langword="false" />.</returns>
			// Token: 0x06006A6E RID: 27246 RVA: 0x0016F82C File Offset: 0x0016DA2C
			public bool Equals(TimeZoneInfo.AdjustmentRule other)
			{
				bool flag = other != null && this.m_dateStart == other.m_dateStart && this.m_dateEnd == other.m_dateEnd && this.m_daylightDelta == other.m_daylightDelta && this.m_baseUtcOffsetDelta == other.m_baseUtcOffsetDelta;
				return flag && this.m_daylightTransitionEnd.Equals(other.m_daylightTransitionEnd) && this.m_daylightTransitionStart.Equals(other.m_daylightTransitionStart);
			}

			/// <summary>Serves as a hash function for hashing algorithms and data structures such as hash tables.</summary>
			/// <returns>A 32-bit signed integer that serves as the hash code for the current <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> object.</returns>
			// Token: 0x06006A6F RID: 27247 RVA: 0x0016F8B6 File Offset: 0x0016DAB6
			public override int GetHashCode()
			{
				return this.m_dateStart.GetHashCode();
			}

			// Token: 0x06006A70 RID: 27248 RVA: 0x0016F8C3 File Offset: 0x0016DAC3
			private AdjustmentRule()
			{
			}

			/// <summary>Creates a new adjustment rule for a particular time zone.</summary>
			/// <param name="dateStart">The effective date of the adjustment rule. If the value of the <paramref name="dateStart" /> parameter is <see langword="DateTime.MinValue.Date" />, this is the first adjustment rule in effect for a time zone.</param>
			/// <param name="dateEnd">The last date that the adjustment rule is in force. If the value of the <paramref name="dateEnd" /> parameter is <see langword="DateTime.MaxValue.Date" />, the adjustment rule has no end date.</param>
			/// <param name="daylightDelta">The time change that results from the adjustment. This value is added to the time zone's <see cref="P:System.TimeZoneInfo.BaseUtcOffset" /> property to obtain the correct daylight offset from Coordinated Universal Time (UTC). This value can range from -14 to 14.</param>
			/// <param name="daylightTransitionStart">An object that defines the start of daylight saving time.</param>
			/// <param name="daylightTransitionEnd">An object that defines the end of daylight saving time.</param>
			/// <returns>An object that represents the new adjustment rule.</returns>
			/// <exception cref="T:System.ArgumentException">The <see cref="P:System.DateTime.Kind" /> property of the <paramref name="dateStart" /> or <paramref name="dateEnd" /> parameter does not equal <see cref="F:System.DateTimeKind.Unspecified" />.  
			///  -or-  
			///  The <paramref name="daylightTransitionStart" /> parameter is equal to the <paramref name="daylightTransitionEnd" /> parameter.  
			///  -or-  
			///  The <paramref name="dateStart" /> or <paramref name="dateEnd" /> parameter includes a time of day value.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="dateEnd" /> is earlier than <paramref name="dateStart" />.  
			/// -or-  
			/// <paramref name="daylightDelta" /> is less than -14 or greater than 14.  
			/// -or-  
			/// The <see cref="P:System.TimeSpan.Milliseconds" /> property of the <paramref name="daylightDelta" /> parameter is not equal to 0.  
			/// -or-  
			/// The <see cref="P:System.TimeSpan.Ticks" /> property of the <paramref name="daylightDelta" /> parameter does not equal a whole number of seconds.</exception>
			// Token: 0x06006A71 RID: 27249 RVA: 0x0016F8CC File Offset: 0x0016DACC
			public static TimeZoneInfo.AdjustmentRule CreateAdjustmentRule(DateTime dateStart, DateTime dateEnd, TimeSpan daylightDelta, TimeZoneInfo.TransitionTime daylightTransitionStart, TimeZoneInfo.TransitionTime daylightTransitionEnd)
			{
				TimeZoneInfo.AdjustmentRule.ValidateAdjustmentRule(dateStart, dateEnd, daylightDelta, daylightTransitionStart, daylightTransitionEnd);
				return new TimeZoneInfo.AdjustmentRule
				{
					m_dateStart = dateStart,
					m_dateEnd = dateEnd,
					m_daylightDelta = daylightDelta,
					m_daylightTransitionStart = daylightTransitionStart,
					m_daylightTransitionEnd = daylightTransitionEnd,
					m_baseUtcOffsetDelta = TimeSpan.Zero
				};
			}

			// Token: 0x06006A72 RID: 27250 RVA: 0x0016F91C File Offset: 0x0016DB1C
			internal static TimeZoneInfo.AdjustmentRule CreateAdjustmentRule(DateTime dateStart, DateTime dateEnd, TimeSpan daylightDelta, TimeZoneInfo.TransitionTime daylightTransitionStart, TimeZoneInfo.TransitionTime daylightTransitionEnd, TimeSpan baseUtcOffsetDelta)
			{
				TimeZoneInfo.AdjustmentRule adjustmentRule = TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(dateStart, dateEnd, daylightDelta, daylightTransitionStart, daylightTransitionEnd);
				adjustmentRule.m_baseUtcOffsetDelta = baseUtcOffsetDelta;
				return adjustmentRule;
			}

			// Token: 0x06006A73 RID: 27251 RVA: 0x0016F940 File Offset: 0x0016DB40
			internal bool IsStartDateMarkerForBeginningOfYear()
			{
				return this.DaylightTransitionStart.Month == 1 && this.DaylightTransitionStart.Day == 1 && this.DaylightTransitionStart.TimeOfDay.Hour == 0 && this.DaylightTransitionStart.TimeOfDay.Minute == 0 && this.DaylightTransitionStart.TimeOfDay.Second == 0 && this.m_dateStart.Year == this.m_dateEnd.Year;
			}

			// Token: 0x06006A74 RID: 27252 RVA: 0x0016F9D4 File Offset: 0x0016DBD4
			internal bool IsEndDateMarkerForEndOfYear()
			{
				return this.DaylightTransitionEnd.Month == 1 && this.DaylightTransitionEnd.Day == 1 && this.DaylightTransitionEnd.TimeOfDay.Hour == 0 && this.DaylightTransitionEnd.TimeOfDay.Minute == 0 && this.DaylightTransitionEnd.TimeOfDay.Second == 0 && this.m_dateStart.Year == this.m_dateEnd.Year;
			}

			// Token: 0x06006A75 RID: 27253 RVA: 0x0016FA68 File Offset: 0x0016DC68
			private static void ValidateAdjustmentRule(DateTime dateStart, DateTime dateEnd, TimeSpan daylightDelta, TimeZoneInfo.TransitionTime daylightTransitionStart, TimeZoneInfo.TransitionTime daylightTransitionEnd)
			{
				if (dateStart.Kind != DateTimeKind.Unspecified)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeKindMustBeUnspecified"), "dateStart");
				}
				if (dateEnd.Kind != DateTimeKind.Unspecified)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeKindMustBeUnspecified"), "dateEnd");
				}
				if (daylightTransitionStart.Equals(daylightTransitionEnd))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_TransitionTimesAreIdentical"), "daylightTransitionEnd");
				}
				if (dateStart > dateEnd)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_OutOfOrderDateTimes"), "dateStart");
				}
				if (TimeZoneInfo.UtcOffsetOutOfRange(daylightDelta))
				{
					throw new ArgumentOutOfRangeException("daylightDelta", daylightDelta, Environment.GetResourceString("ArgumentOutOfRange_UtcOffset"));
				}
				if (daylightDelta.Ticks % 600000000L != 0L)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_TimeSpanHasSeconds"), "daylightDelta");
				}
				if (dateStart.TimeOfDay != TimeSpan.Zero)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeHasTimeOfDay"), "dateStart");
				}
				if (dateEnd.TimeOfDay != TimeSpan.Zero)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeHasTimeOfDay"), "dateEnd");
				}
			}

			/// <summary>Runs when the deserialization of a <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> object is completed.</summary>
			/// <param name="sender">The object that initiated the callback. The functionality for this parameter is not currently implemented.</param>
			// Token: 0x06006A76 RID: 27254 RVA: 0x0016FB88 File Offset: 0x0016DD88
			void IDeserializationCallback.OnDeserialization(object sender)
			{
				try
				{
					TimeZoneInfo.AdjustmentRule.ValidateAdjustmentRule(this.m_dateStart, this.m_dateEnd, this.m_daylightDelta, this.m_daylightTransitionStart, this.m_daylightTransitionEnd);
				}
				catch (ArgumentException ex)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), ex);
				}
			}

			/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data that is required to serialize this object.</summary>
			/// <param name="info">The object to populate with data.</param>
			/// <param name="context">The destination for this serialization (see <see cref="T:System.Runtime.Serialization.StreamingContext" />).</param>
			// Token: 0x06006A77 RID: 27255 RVA: 0x0016FBDC File Offset: 0x0016DDDC
			[SecurityCritical]
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				info.AddValue("DateStart", this.m_dateStart);
				info.AddValue("DateEnd", this.m_dateEnd);
				info.AddValue("DaylightDelta", this.m_daylightDelta);
				info.AddValue("DaylightTransitionStart", this.m_daylightTransitionStart);
				info.AddValue("DaylightTransitionEnd", this.m_daylightTransitionEnd);
				info.AddValue("BaseUtcOffsetDelta", this.m_baseUtcOffsetDelta);
			}

			// Token: 0x06006A78 RID: 27256 RVA: 0x0016FC74 File Offset: 0x0016DE74
			private AdjustmentRule(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this.m_dateStart = (DateTime)info.GetValue("DateStart", typeof(DateTime));
				this.m_dateEnd = (DateTime)info.GetValue("DateEnd", typeof(DateTime));
				this.m_daylightDelta = (TimeSpan)info.GetValue("DaylightDelta", typeof(TimeSpan));
				this.m_daylightTransitionStart = (TimeZoneInfo.TransitionTime)info.GetValue("DaylightTransitionStart", typeof(TimeZoneInfo.TransitionTime));
				this.m_daylightTransitionEnd = (TimeZoneInfo.TransitionTime)info.GetValue("DaylightTransitionEnd", typeof(TimeZoneInfo.TransitionTime));
				object valueNoThrow = info.GetValueNoThrow("BaseUtcOffsetDelta", typeof(TimeSpan));
				if (valueNoThrow != null)
				{
					this.m_baseUtcOffsetDelta = (TimeSpan)valueNoThrow;
				}
			}

			// Token: 0x04003218 RID: 12824
			private DateTime m_dateStart;

			// Token: 0x04003219 RID: 12825
			private DateTime m_dateEnd;

			// Token: 0x0400321A RID: 12826
			private TimeSpan m_daylightDelta;

			// Token: 0x0400321B RID: 12827
			private TimeZoneInfo.TransitionTime m_daylightTransitionStart;

			// Token: 0x0400321C RID: 12828
			private TimeZoneInfo.TransitionTime m_daylightTransitionEnd;

			// Token: 0x0400321D RID: 12829
			private TimeSpan m_baseUtcOffsetDelta;
		}

		/// <summary>Provides information about a specific time change, such as the change from daylight saving time to standard time or vice versa, in a particular time zone.</summary>
		// Token: 0x02000AFF RID: 2815
		[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
		[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
		[Serializable]
		public struct TransitionTime : IEquatable<TimeZoneInfo.TransitionTime>, ISerializable, IDeserializationCallback
		{
			/// <summary>Gets the hour, minute, and second at which the time change occurs.</summary>
			/// <returns>The time of day at which the time change occurs.</returns>
			// Token: 0x17001201 RID: 4609
			// (get) Token: 0x06006A79 RID: 27257 RVA: 0x0016FD5A File Offset: 0x0016DF5A
			public DateTime TimeOfDay
			{
				get
				{
					return this.m_timeOfDay;
				}
			}

			/// <summary>Gets the month in which the time change occurs.</summary>
			/// <returns>The month in which the time change occurs.</returns>
			// Token: 0x17001202 RID: 4610
			// (get) Token: 0x06006A7A RID: 27258 RVA: 0x0016FD62 File Offset: 0x0016DF62
			public int Month
			{
				get
				{
					return (int)this.m_month;
				}
			}

			/// <summary>Gets the week of the month in which a time change occurs.</summary>
			/// <returns>The week of the month in which the time change occurs.</returns>
			// Token: 0x17001203 RID: 4611
			// (get) Token: 0x06006A7B RID: 27259 RVA: 0x0016FD6A File Offset: 0x0016DF6A
			public int Week
			{
				get
				{
					return (int)this.m_week;
				}
			}

			/// <summary>Gets the day on which the time change occurs.</summary>
			/// <returns>The day on which the time change occurs.</returns>
			// Token: 0x17001204 RID: 4612
			// (get) Token: 0x06006A7C RID: 27260 RVA: 0x0016FD72 File Offset: 0x0016DF72
			public int Day
			{
				get
				{
					return (int)this.m_day;
				}
			}

			/// <summary>Gets the day of the week on which the time change occurs.</summary>
			/// <returns>The day of the week on which the time change occurs.</returns>
			// Token: 0x17001205 RID: 4613
			// (get) Token: 0x06006A7D RID: 27261 RVA: 0x0016FD7A File Offset: 0x0016DF7A
			public DayOfWeek DayOfWeek
			{
				get
				{
					return this.m_dayOfWeek;
				}
			}

			/// <summary>Gets a value indicating whether the time change occurs at a fixed date and time (such as November 1) or a floating date and time (such as the last Sunday of October).</summary>
			/// <returns>
			///   <see langword="true" /> if the time change rule is fixed-date; <see langword="false" /> if the time change rule is floating-date.</returns>
			// Token: 0x17001206 RID: 4614
			// (get) Token: 0x06006A7E RID: 27262 RVA: 0x0016FD82 File Offset: 0x0016DF82
			public bool IsFixedDateRule
			{
				get
				{
					return this.m_isFixedDateRule;
				}
			}

			/// <summary>Determines whether an object has identical values to the current <see cref="T:System.TimeZoneInfo.TransitionTime" /> object.</summary>
			/// <param name="obj">An object to compare with the current <see cref="T:System.TimeZoneInfo.TransitionTime" /> object.</param>
			/// <returns>
			///   <see langword="true" /> if the two objects are equal; otherwise, <see langword="false" />.</returns>
			// Token: 0x06006A7F RID: 27263 RVA: 0x0016FD8A File Offset: 0x0016DF8A
			public override bool Equals(object obj)
			{
				return obj is TimeZoneInfo.TransitionTime && this.Equals((TimeZoneInfo.TransitionTime)obj);
			}

			/// <summary>Determines whether two specified <see cref="T:System.TimeZoneInfo.TransitionTime" /> objects are equal.</summary>
			/// <param name="t1">The first object to compare.</param>
			/// <param name="t2">The second object to compare.</param>
			/// <returns>
			///   <see langword="true" /> if <paramref name="t1" /> and <paramref name="t2" /> have identical values; otherwise, <see langword="false" />.</returns>
			// Token: 0x06006A80 RID: 27264 RVA: 0x0016FDA2 File Offset: 0x0016DFA2
			public static bool operator ==(TimeZoneInfo.TransitionTime t1, TimeZoneInfo.TransitionTime t2)
			{
				return t1.Equals(t2);
			}

			/// <summary>Determines whether two specified <see cref="T:System.TimeZoneInfo.TransitionTime" /> objects are not equal.</summary>
			/// <param name="t1">The first object to compare.</param>
			/// <param name="t2">The second object to compare.</param>
			/// <returns>
			///   <see langword="true" /> if <paramref name="t1" /> and <paramref name="t2" /> have any different member values; otherwise, <see langword="false" />.</returns>
			// Token: 0x06006A81 RID: 27265 RVA: 0x0016FDAC File Offset: 0x0016DFAC
			public static bool operator !=(TimeZoneInfo.TransitionTime t1, TimeZoneInfo.TransitionTime t2)
			{
				return !t1.Equals(t2);
			}

			/// <summary>Determines whether the current <see cref="T:System.TimeZoneInfo.TransitionTime" /> object has identical values to a second <see cref="T:System.TimeZoneInfo.TransitionTime" /> object.</summary>
			/// <param name="other">An object to compare to the current instance.</param>
			/// <returns>
			///   <see langword="true" /> if the two objects have identical property values; otherwise, <see langword="false" />.</returns>
			// Token: 0x06006A82 RID: 27266 RVA: 0x0016FDBC File Offset: 0x0016DFBC
			public bool Equals(TimeZoneInfo.TransitionTime other)
			{
				bool flag = this.m_isFixedDateRule == other.m_isFixedDateRule && this.m_timeOfDay == other.m_timeOfDay && this.m_month == other.m_month;
				if (flag)
				{
					if (other.m_isFixedDateRule)
					{
						flag = this.m_day == other.m_day;
					}
					else
					{
						flag = this.m_week == other.m_week && this.m_dayOfWeek == other.m_dayOfWeek;
					}
				}
				return flag;
			}

			/// <summary>Serves as a hash function for hashing algorithms and data structures such as hash tables.</summary>
			/// <returns>A 32-bit signed integer that serves as the hash code for this <see cref="T:System.TimeZoneInfo.TransitionTime" /> object.</returns>
			// Token: 0x06006A83 RID: 27267 RVA: 0x0016FE39 File Offset: 0x0016E039
			public override int GetHashCode()
			{
				return (int)this.m_month ^ ((int)this.m_week << 8);
			}

			/// <summary>Defines a time change that uses a fixed-date rule (that is, a time change that occurs on a specific day of a specific month).</summary>
			/// <param name="timeOfDay">The time at which the time change occurs. This parameter corresponds to the <see cref="P:System.TimeZoneInfo.TransitionTime.TimeOfDay" /> property.</param>
			/// <param name="month">The month in which the time change occurs. This parameter corresponds to the <see cref="P:System.TimeZoneInfo.TransitionTime.Month" /> property.</param>
			/// <param name="day">The day of the month on which the time change occurs. This parameter corresponds to the <see cref="P:System.TimeZoneInfo.TransitionTime.Day" /> property.</param>
			/// <returns>Data about the time change.</returns>
			/// <exception cref="T:System.ArgumentException">The <paramref name="timeOfDay" /> parameter has a non-default date component.  
			///  -or-  
			///  The <paramref name="timeOfDay" /> parameter's <see cref="P:System.DateTime.Kind" /> property is not <see cref="F:System.DateTimeKind.Unspecified" />.  
			///  -or-  
			///  The <paramref name="timeOfDay" /> parameter does not represent a whole number of milliseconds.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="month" /> parameter is less than 1 or greater than 12.  
			///  -or-  
			///  The <paramref name="day" /> parameter is less than 1 or greater than 31.</exception>
			// Token: 0x06006A84 RID: 27268 RVA: 0x0016FE4A File Offset: 0x0016E04A
			public static TimeZoneInfo.TransitionTime CreateFixedDateRule(DateTime timeOfDay, int month, int day)
			{
				return TimeZoneInfo.TransitionTime.CreateTransitionTime(timeOfDay, month, 1, day, DayOfWeek.Sunday, true);
			}

			/// <summary>Defines a time change that uses a floating-date rule (that is, a time change that occurs on a specific day of a specific week of a specific month).</summary>
			/// <param name="timeOfDay">The time at which the time change occurs. This parameter corresponds to the <see cref="P:System.TimeZoneInfo.TransitionTime.TimeOfDay" /> property.</param>
			/// <param name="month">The month in which the time change occurs. This parameter corresponds to the <see cref="P:System.TimeZoneInfo.TransitionTime.Month" /> property.</param>
			/// <param name="week">The week of the month in which the time change occurs. Its value can range from 1 to 5, with 5 representing the last week of the month. This parameter corresponds to the <see cref="P:System.TimeZoneInfo.TransitionTime.Week" /> property.</param>
			/// <param name="dayOfWeek">The day of the week on which the time change occurs. This parameter corresponds to the <see cref="P:System.TimeZoneInfo.TransitionTime.DayOfWeek" /> property.</param>
			/// <returns>Data about the time change.</returns>
			/// <exception cref="T:System.ArgumentException">The <paramref name="timeOfDay" /> parameter has a non-default date component.  
			///  -or-  
			///  The <paramref name="timeOfDay" /> parameter does not represent a whole number of milliseconds.  
			///  -or-  
			///  The <paramref name="timeOfDay" /> parameter's <see cref="P:System.DateTime.Kind" /> property is not <see cref="F:System.DateTimeKind.Unspecified" />.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="month" /> is less than 1 or greater than 12.  
			/// -or-  
			/// <paramref name="week" /> is less than 1 or greater than 5.  
			/// -or-  
			/// The <paramref name="dayOfWeek" /> parameter is not a member of the <see cref="T:System.DayOfWeek" /> enumeration.</exception>
			// Token: 0x06006A85 RID: 27269 RVA: 0x0016FE57 File Offset: 0x0016E057
			public static TimeZoneInfo.TransitionTime CreateFloatingDateRule(DateTime timeOfDay, int month, int week, DayOfWeek dayOfWeek)
			{
				return TimeZoneInfo.TransitionTime.CreateTransitionTime(timeOfDay, month, week, 1, dayOfWeek, false);
			}

			// Token: 0x06006A86 RID: 27270 RVA: 0x0016FE64 File Offset: 0x0016E064
			private static TimeZoneInfo.TransitionTime CreateTransitionTime(DateTime timeOfDay, int month, int week, int day, DayOfWeek dayOfWeek, bool isFixedDateRule)
			{
				TimeZoneInfo.TransitionTime.ValidateTransitionTime(timeOfDay, month, week, day, dayOfWeek);
				return new TimeZoneInfo.TransitionTime
				{
					m_isFixedDateRule = isFixedDateRule,
					m_timeOfDay = timeOfDay,
					m_dayOfWeek = dayOfWeek,
					m_day = (byte)day,
					m_week = (byte)week,
					m_month = (byte)month
				};
			}

			// Token: 0x06006A87 RID: 27271 RVA: 0x0016FEBC File Offset: 0x0016E0BC
			private static void ValidateTransitionTime(DateTime timeOfDay, int month, int week, int day, DayOfWeek dayOfWeek)
			{
				if (timeOfDay.Kind != DateTimeKind.Unspecified)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeKindMustBeUnspecified"), "timeOfDay");
				}
				if (month < 1 || month > 12)
				{
					throw new ArgumentOutOfRangeException("month", Environment.GetResourceString("ArgumentOutOfRange_MonthParam"));
				}
				if (day < 1 || day > 31)
				{
					throw new ArgumentOutOfRangeException("day", Environment.GetResourceString("ArgumentOutOfRange_DayParam"));
				}
				if (week < 1 || week > 5)
				{
					throw new ArgumentOutOfRangeException("week", Environment.GetResourceString("ArgumentOutOfRange_Week"));
				}
				if (dayOfWeek < DayOfWeek.Sunday || dayOfWeek > DayOfWeek.Saturday)
				{
					throw new ArgumentOutOfRangeException("dayOfWeek", Environment.GetResourceString("ArgumentOutOfRange_DayOfWeek"));
				}
				int num;
				int num2;
				int num3;
				timeOfDay.GetDatePart(out num, out num2, out num3);
				if (num != 1 || num2 != 1 || num3 != 1 || timeOfDay.Ticks % 10000L != 0L)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_DateTimeHasTicks"), "timeOfDay");
				}
			}

			/// <summary>Runs when the deserialization of an object has been completed.</summary>
			/// <param name="sender">The object that initiated the callback. The functionality for this parameter is not currently implemented.</param>
			// Token: 0x06006A88 RID: 27272 RVA: 0x0016FFA0 File Offset: 0x0016E1A0
			void IDeserializationCallback.OnDeserialization(object sender)
			{
				try
				{
					TimeZoneInfo.TransitionTime.ValidateTransitionTime(this.m_timeOfDay, (int)this.m_month, (int)this.m_week, (int)this.m_day, this.m_dayOfWeek);
				}
				catch (ArgumentException ex)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), ex);
				}
			}

			/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data that is required to serialize this object.</summary>
			/// <param name="info">The object to populate with data.</param>
			/// <param name="context">The destination for this serialization (see <see cref="T:System.Runtime.Serialization.StreamingContext" />).</param>
			// Token: 0x06006A89 RID: 27273 RVA: 0x0016FFF4 File Offset: 0x0016E1F4
			[SecurityCritical]
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				info.AddValue("TimeOfDay", this.m_timeOfDay);
				info.AddValue("Month", this.m_month);
				info.AddValue("Week", this.m_week);
				info.AddValue("Day", this.m_day);
				info.AddValue("DayOfWeek", this.m_dayOfWeek);
				info.AddValue("IsFixedDateRule", this.m_isFixedDateRule);
			}

			// Token: 0x06006A8A RID: 27274 RVA: 0x0017007C File Offset: 0x0016E27C
			private TransitionTime(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this.m_timeOfDay = (DateTime)info.GetValue("TimeOfDay", typeof(DateTime));
				this.m_month = (byte)info.GetValue("Month", typeof(byte));
				this.m_week = (byte)info.GetValue("Week", typeof(byte));
				this.m_day = (byte)info.GetValue("Day", typeof(byte));
				this.m_dayOfWeek = (DayOfWeek)info.GetValue("DayOfWeek", typeof(DayOfWeek));
				this.m_isFixedDateRule = (bool)info.GetValue("IsFixedDateRule", typeof(bool));
			}

			// Token: 0x0400321E RID: 12830
			private DateTime m_timeOfDay;

			// Token: 0x0400321F RID: 12831
			private byte m_month;

			// Token: 0x04003220 RID: 12832
			private byte m_week;

			// Token: 0x04003221 RID: 12833
			private byte m_day;

			// Token: 0x04003222 RID: 12834
			private DayOfWeek m_dayOfWeek;

			// Token: 0x04003223 RID: 12835
			private bool m_isFixedDateRule;
		}

		// Token: 0x02000B00 RID: 2816
		private sealed class StringSerializer
		{
			// Token: 0x06006A8B RID: 27275 RVA: 0x00170158 File Offset: 0x0016E358
			public static string GetSerializedString(TimeZoneInfo zone)
			{
				StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
				stringBuilder.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(zone.Id));
				stringBuilder.Append(';');
				stringBuilder.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(zone.BaseUtcOffset.TotalMinutes.ToString(CultureInfo.InvariantCulture)));
				stringBuilder.Append(';');
				stringBuilder.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(zone.DisplayName));
				stringBuilder.Append(';');
				stringBuilder.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(zone.StandardName));
				stringBuilder.Append(';');
				stringBuilder.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(zone.DaylightName));
				stringBuilder.Append(';');
				TimeZoneInfo.AdjustmentRule[] adjustmentRules = zone.GetAdjustmentRules();
				if (adjustmentRules != null && adjustmentRules.Length != 0)
				{
					foreach (TimeZoneInfo.AdjustmentRule adjustmentRule in adjustmentRules)
					{
						stringBuilder.Append('[');
						stringBuilder.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(adjustmentRule.DateStart.ToString("MM:dd:yyyy", DateTimeFormatInfo.InvariantInfo)));
						stringBuilder.Append(';');
						stringBuilder.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(adjustmentRule.DateEnd.ToString("MM:dd:yyyy", DateTimeFormatInfo.InvariantInfo)));
						stringBuilder.Append(';');
						stringBuilder.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(adjustmentRule.DaylightDelta.TotalMinutes.ToString(CultureInfo.InvariantCulture)));
						stringBuilder.Append(';');
						TimeZoneInfo.StringSerializer.SerializeTransitionTime(adjustmentRule.DaylightTransitionStart, stringBuilder);
						stringBuilder.Append(';');
						TimeZoneInfo.StringSerializer.SerializeTransitionTime(adjustmentRule.DaylightTransitionEnd, stringBuilder);
						stringBuilder.Append(';');
						if (adjustmentRule.BaseUtcOffsetDelta != TimeSpan.Zero)
						{
							stringBuilder.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(adjustmentRule.BaseUtcOffsetDelta.TotalMinutes.ToString(CultureInfo.InvariantCulture)));
							stringBuilder.Append(';');
						}
						stringBuilder.Append(']');
					}
				}
				stringBuilder.Append(';');
				return StringBuilderCache.GetStringAndRelease(stringBuilder);
			}

			// Token: 0x06006A8C RID: 27276 RVA: 0x0017035C File Offset: 0x0016E55C
			public static TimeZoneInfo GetDeserializedTimeZoneInfo(string source)
			{
				TimeZoneInfo.StringSerializer stringSerializer = new TimeZoneInfo.StringSerializer(source);
				string nextStringValue = stringSerializer.GetNextStringValue(false);
				TimeSpan nextTimeSpanValue = stringSerializer.GetNextTimeSpanValue(false);
				string nextStringValue2 = stringSerializer.GetNextStringValue(false);
				string nextStringValue3 = stringSerializer.GetNextStringValue(false);
				string nextStringValue4 = stringSerializer.GetNextStringValue(false);
				TimeZoneInfo.AdjustmentRule[] nextAdjustmentRuleArrayValue = stringSerializer.GetNextAdjustmentRuleArrayValue(false);
				TimeZoneInfo timeZoneInfo;
				try
				{
					timeZoneInfo = TimeZoneInfo.CreateCustomTimeZone(nextStringValue, nextTimeSpanValue, nextStringValue2, nextStringValue3, nextStringValue4, nextAdjustmentRuleArrayValue);
				}
				catch (ArgumentException ex)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), ex);
				}
				catch (InvalidTimeZoneException ex2)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), ex2);
				}
				return timeZoneInfo;
			}

			// Token: 0x06006A8D RID: 27277 RVA: 0x001703FC File Offset: 0x0016E5FC
			private StringSerializer(string str)
			{
				this.m_serializedText = str;
				this.m_state = TimeZoneInfo.StringSerializer.State.StartOfToken;
			}

			// Token: 0x06006A8E RID: 27278 RVA: 0x00170414 File Offset: 0x0016E614
			private static string SerializeSubstitute(string text)
			{
				text = text.Replace("\\", "\\\\");
				text = text.Replace("[", "\\[");
				text = text.Replace("]", "\\]");
				return text.Replace(";", "\\;");
			}

			// Token: 0x06006A8F RID: 27279 RVA: 0x00170468 File Offset: 0x0016E668
			private static void SerializeTransitionTime(TimeZoneInfo.TransitionTime time, StringBuilder serializedText)
			{
				serializedText.Append('[');
				serializedText.Append((time.IsFixedDateRule ? 1 : 0).ToString(CultureInfo.InvariantCulture));
				serializedText.Append(';');
				if (time.IsFixedDateRule)
				{
					serializedText.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(time.TimeOfDay.ToString("HH:mm:ss.FFF", DateTimeFormatInfo.InvariantInfo)));
					serializedText.Append(';');
					serializedText.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(time.Month.ToString(CultureInfo.InvariantCulture)));
					serializedText.Append(';');
					serializedText.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(time.Day.ToString(CultureInfo.InvariantCulture)));
					serializedText.Append(';');
				}
				else
				{
					serializedText.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(time.TimeOfDay.ToString("HH:mm:ss.FFF", DateTimeFormatInfo.InvariantInfo)));
					serializedText.Append(';');
					serializedText.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(time.Month.ToString(CultureInfo.InvariantCulture)));
					serializedText.Append(';');
					serializedText.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(time.Week.ToString(CultureInfo.InvariantCulture)));
					serializedText.Append(';');
					serializedText.Append(TimeZoneInfo.StringSerializer.SerializeSubstitute(((int)time.DayOfWeek).ToString(CultureInfo.InvariantCulture)));
					serializedText.Append(';');
				}
				serializedText.Append(']');
			}

			// Token: 0x06006A90 RID: 27280 RVA: 0x001705EB File Offset: 0x0016E7EB
			private static void VerifyIsEscapableCharacter(char c)
			{
				if (c != '\\' && c != ';' && c != '[' && c != ']')
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidEscapeSequence", new object[] { c }));
				}
			}

			// Token: 0x06006A91 RID: 27281 RVA: 0x00170620 File Offset: 0x0016E820
			private void SkipVersionNextDataFields(int depth)
			{
				if (this.m_currentTokenStartIndex < 0 || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
				}
				TimeZoneInfo.StringSerializer.State state = TimeZoneInfo.StringSerializer.State.NotEscaped;
				for (int i = this.m_currentTokenStartIndex; i < this.m_serializedText.Length; i++)
				{
					if (state == TimeZoneInfo.StringSerializer.State.Escaped)
					{
						TimeZoneInfo.StringSerializer.VerifyIsEscapableCharacter(this.m_serializedText[i]);
						state = TimeZoneInfo.StringSerializer.State.NotEscaped;
					}
					else if (state == TimeZoneInfo.StringSerializer.State.NotEscaped)
					{
						char c = this.m_serializedText[i];
						if (c == '\0')
						{
							throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
						}
						switch (c)
						{
						case '[':
							depth++;
							break;
						case '\\':
							state = TimeZoneInfo.StringSerializer.State.Escaped;
							break;
						case ']':
							depth--;
							if (depth == 0)
							{
								this.m_currentTokenStartIndex = i + 1;
								if (this.m_currentTokenStartIndex >= this.m_serializedText.Length)
								{
									this.m_state = TimeZoneInfo.StringSerializer.State.EndOfLine;
									return;
								}
								this.m_state = TimeZoneInfo.StringSerializer.State.StartOfToken;
								return;
							}
							break;
						}
					}
				}
				throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
			}

			// Token: 0x06006A92 RID: 27282 RVA: 0x00170720 File Offset: 0x0016E920
			private string GetNextStringValue(bool canEndWithoutSeparator)
			{
				if (this.m_state == TimeZoneInfo.StringSerializer.State.EndOfLine)
				{
					if (canEndWithoutSeparator)
					{
						return null;
					}
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
				}
				else
				{
					if (this.m_currentTokenStartIndex < 0 || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
					{
						throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
					}
					TimeZoneInfo.StringSerializer.State state = TimeZoneInfo.StringSerializer.State.NotEscaped;
					StringBuilder stringBuilder = StringBuilderCache.Acquire(64);
					for (int i = this.m_currentTokenStartIndex; i < this.m_serializedText.Length; i++)
					{
						if (state == TimeZoneInfo.StringSerializer.State.Escaped)
						{
							TimeZoneInfo.StringSerializer.VerifyIsEscapableCharacter(this.m_serializedText[i]);
							stringBuilder.Append(this.m_serializedText[i]);
							state = TimeZoneInfo.StringSerializer.State.NotEscaped;
						}
						else if (state == TimeZoneInfo.StringSerializer.State.NotEscaped)
						{
							char c = this.m_serializedText[i];
							if (c == '\0')
							{
								throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
							}
							if (c == ';')
							{
								this.m_currentTokenStartIndex = i + 1;
								if (this.m_currentTokenStartIndex >= this.m_serializedText.Length)
								{
									this.m_state = TimeZoneInfo.StringSerializer.State.EndOfLine;
								}
								else
								{
									this.m_state = TimeZoneInfo.StringSerializer.State.StartOfToken;
								}
								return StringBuilderCache.GetStringAndRelease(stringBuilder);
							}
							switch (c)
							{
							case '[':
								throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
							case '\\':
								state = TimeZoneInfo.StringSerializer.State.Escaped;
								break;
							case ']':
								if (canEndWithoutSeparator)
								{
									this.m_currentTokenStartIndex = i;
									this.m_state = TimeZoneInfo.StringSerializer.State.StartOfToken;
									return stringBuilder.ToString();
								}
								throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
							default:
								stringBuilder.Append(this.m_serializedText[i]);
								break;
							}
						}
					}
					if (state == TimeZoneInfo.StringSerializer.State.Escaped)
					{
						throw new SerializationException(Environment.GetResourceString("Serialization_InvalidEscapeSequence", new object[] { string.Empty }));
					}
					if (!canEndWithoutSeparator)
					{
						throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
					}
					this.m_currentTokenStartIndex = this.m_serializedText.Length;
					this.m_state = TimeZoneInfo.StringSerializer.State.EndOfLine;
					return StringBuilderCache.GetStringAndRelease(stringBuilder);
				}
			}

			// Token: 0x06006A93 RID: 27283 RVA: 0x001708F0 File Offset: 0x0016EAF0
			private DateTime GetNextDateTimeValue(bool canEndWithoutSeparator, string format)
			{
				string nextStringValue = this.GetNextStringValue(canEndWithoutSeparator);
				DateTime dateTime;
				if (!DateTime.TryParseExact(nextStringValue, format, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out dateTime))
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
				}
				return dateTime;
			}

			// Token: 0x06006A94 RID: 27284 RVA: 0x00170928 File Offset: 0x0016EB28
			private TimeSpan GetNextTimeSpanValue(bool canEndWithoutSeparator)
			{
				int nextInt32Value = this.GetNextInt32Value(canEndWithoutSeparator);
				TimeSpan timeSpan;
				try
				{
					timeSpan = new TimeSpan(0, nextInt32Value, 0);
				}
				catch (ArgumentOutOfRangeException ex)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), ex);
				}
				return timeSpan;
			}

			// Token: 0x06006A95 RID: 27285 RVA: 0x0017096C File Offset: 0x0016EB6C
			private int GetNextInt32Value(bool canEndWithoutSeparator)
			{
				string nextStringValue = this.GetNextStringValue(canEndWithoutSeparator);
				int num;
				if (!int.TryParse(nextStringValue, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out num))
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
				}
				return num;
			}

			// Token: 0x06006A96 RID: 27286 RVA: 0x001709A4 File Offset: 0x0016EBA4
			private TimeZoneInfo.AdjustmentRule[] GetNextAdjustmentRuleArrayValue(bool canEndWithoutSeparator)
			{
				List<TimeZoneInfo.AdjustmentRule> list = new List<TimeZoneInfo.AdjustmentRule>(1);
				int num = 0;
				for (TimeZoneInfo.AdjustmentRule adjustmentRule = this.GetNextAdjustmentRuleValue(true); adjustmentRule != null; adjustmentRule = this.GetNextAdjustmentRuleValue(true))
				{
					list.Add(adjustmentRule);
					num++;
				}
				if (!canEndWithoutSeparator)
				{
					if (this.m_state == TimeZoneInfo.StringSerializer.State.EndOfLine)
					{
						throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
					}
					if (this.m_currentTokenStartIndex < 0 || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
					{
						throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
					}
				}
				if (num == 0)
				{
					return null;
				}
				return list.ToArray();
			}

			// Token: 0x06006A97 RID: 27287 RVA: 0x00170A30 File Offset: 0x0016EC30
			private TimeZoneInfo.AdjustmentRule GetNextAdjustmentRuleValue(bool canEndWithoutSeparator)
			{
				if (this.m_state == TimeZoneInfo.StringSerializer.State.EndOfLine)
				{
					if (canEndWithoutSeparator)
					{
						return null;
					}
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
				}
				else
				{
					if (this.m_currentTokenStartIndex < 0 || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
					{
						throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
					}
					if (this.m_serializedText[this.m_currentTokenStartIndex] == ';')
					{
						return null;
					}
					if (this.m_serializedText[this.m_currentTokenStartIndex] != '[')
					{
						throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
					}
					this.m_currentTokenStartIndex++;
					DateTime nextDateTimeValue = this.GetNextDateTimeValue(false, "MM:dd:yyyy");
					DateTime nextDateTimeValue2 = this.GetNextDateTimeValue(false, "MM:dd:yyyy");
					TimeSpan nextTimeSpanValue = this.GetNextTimeSpanValue(false);
					TimeZoneInfo.TransitionTime nextTransitionTimeValue = this.GetNextTransitionTimeValue(false);
					TimeZoneInfo.TransitionTime nextTransitionTimeValue2 = this.GetNextTransitionTimeValue(false);
					TimeSpan timeSpan = TimeSpan.Zero;
					if (this.m_state == TimeZoneInfo.StringSerializer.State.EndOfLine || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
					{
						throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
					}
					if ((this.m_serializedText[this.m_currentTokenStartIndex] >= '0' && this.m_serializedText[this.m_currentTokenStartIndex] <= '9') || this.m_serializedText[this.m_currentTokenStartIndex] == '-' || this.m_serializedText[this.m_currentTokenStartIndex] == '+')
					{
						timeSpan = this.GetNextTimeSpanValue(false);
					}
					if (this.m_state == TimeZoneInfo.StringSerializer.State.EndOfLine || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
					{
						throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
					}
					if (this.m_serializedText[this.m_currentTokenStartIndex] != ']')
					{
						this.SkipVersionNextDataFields(1);
					}
					else
					{
						this.m_currentTokenStartIndex++;
					}
					TimeZoneInfo.AdjustmentRule adjustmentRule;
					try
					{
						adjustmentRule = TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(nextDateTimeValue, nextDateTimeValue2, nextTimeSpanValue, nextTransitionTimeValue, nextTransitionTimeValue2, timeSpan);
					}
					catch (ArgumentException ex)
					{
						throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), ex);
					}
					if (this.m_currentTokenStartIndex >= this.m_serializedText.Length)
					{
						this.m_state = TimeZoneInfo.StringSerializer.State.EndOfLine;
					}
					else
					{
						this.m_state = TimeZoneInfo.StringSerializer.State.StartOfToken;
					}
					return adjustmentRule;
				}
			}

			// Token: 0x06006A98 RID: 27288 RVA: 0x00170C48 File Offset: 0x0016EE48
			private TimeZoneInfo.TransitionTime GetNextTransitionTimeValue(bool canEndWithoutSeparator)
			{
				if (this.m_state == TimeZoneInfo.StringSerializer.State.EndOfLine || (this.m_currentTokenStartIndex < this.m_serializedText.Length && this.m_serializedText[this.m_currentTokenStartIndex] == ']'))
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
				}
				if (this.m_currentTokenStartIndex < 0 || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
				}
				if (this.m_serializedText[this.m_currentTokenStartIndex] != '[')
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
				}
				this.m_currentTokenStartIndex++;
				int nextInt32Value = this.GetNextInt32Value(false);
				if (nextInt32Value != 0 && nextInt32Value != 1)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
				}
				DateTime nextDateTimeValue = this.GetNextDateTimeValue(false, "HH:mm:ss.FFF");
				nextDateTimeValue = new DateTime(1, 1, 1, nextDateTimeValue.Hour, nextDateTimeValue.Minute, nextDateTimeValue.Second, nextDateTimeValue.Millisecond);
				int nextInt32Value2 = this.GetNextInt32Value(false);
				TimeZoneInfo.TransitionTime transitionTime;
				if (nextInt32Value == 1)
				{
					int nextInt32Value3 = this.GetNextInt32Value(false);
					try
					{
						transitionTime = TimeZoneInfo.TransitionTime.CreateFixedDateRule(nextDateTimeValue, nextInt32Value2, nextInt32Value3);
						goto IL_15B;
					}
					catch (ArgumentException ex)
					{
						throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), ex);
					}
				}
				int nextInt32Value4 = this.GetNextInt32Value(false);
				int nextInt32Value5 = this.GetNextInt32Value(false);
				try
				{
					transitionTime = TimeZoneInfo.TransitionTime.CreateFloatingDateRule(nextDateTimeValue, nextInt32Value2, nextInt32Value4, (DayOfWeek)nextInt32Value5);
				}
				catch (ArgumentException ex2)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"), ex2);
				}
				IL_15B:
				if (this.m_state == TimeZoneInfo.StringSerializer.State.EndOfLine || this.m_currentTokenStartIndex >= this.m_serializedText.Length)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
				}
				if (this.m_serializedText[this.m_currentTokenStartIndex] != ']')
				{
					this.SkipVersionNextDataFields(1);
				}
				else
				{
					this.m_currentTokenStartIndex++;
				}
				bool flag = false;
				if (this.m_currentTokenStartIndex < this.m_serializedText.Length && this.m_serializedText[this.m_currentTokenStartIndex] == ';')
				{
					this.m_currentTokenStartIndex++;
					flag = true;
				}
				if (!flag && !canEndWithoutSeparator)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_InvalidData"));
				}
				if (this.m_currentTokenStartIndex >= this.m_serializedText.Length)
				{
					this.m_state = TimeZoneInfo.StringSerializer.State.EndOfLine;
				}
				else
				{
					this.m_state = TimeZoneInfo.StringSerializer.State.StartOfToken;
				}
				return transitionTime;
			}

			// Token: 0x04003224 RID: 12836
			private string m_serializedText;

			// Token: 0x04003225 RID: 12837
			private int m_currentTokenStartIndex;

			// Token: 0x04003226 RID: 12838
			private TimeZoneInfo.StringSerializer.State m_state;

			// Token: 0x04003227 RID: 12839
			private const int initialCapacityForString = 64;

			// Token: 0x04003228 RID: 12840
			private const char esc = '\\';

			// Token: 0x04003229 RID: 12841
			private const char sep = ';';

			// Token: 0x0400322A RID: 12842
			private const char lhs = '[';

			// Token: 0x0400322B RID: 12843
			private const char rhs = ']';

			// Token: 0x0400322C RID: 12844
			private const string escString = "\\";

			// Token: 0x0400322D RID: 12845
			private const string sepString = ";";

			// Token: 0x0400322E RID: 12846
			private const string lhsString = "[";

			// Token: 0x0400322F RID: 12847
			private const string rhsString = "]";

			// Token: 0x04003230 RID: 12848
			private const string escapedEsc = "\\\\";

			// Token: 0x04003231 RID: 12849
			private const string escapedSep = "\\;";

			// Token: 0x04003232 RID: 12850
			private const string escapedLhs = "\\[";

			// Token: 0x04003233 RID: 12851
			private const string escapedRhs = "\\]";

			// Token: 0x04003234 RID: 12852
			private const string dateTimeFormat = "MM:dd:yyyy";

			// Token: 0x04003235 RID: 12853
			private const string timeOfDayFormat = "HH:mm:ss.FFF";

			// Token: 0x02000CF9 RID: 3321
			private enum State
			{
				// Token: 0x04003922 RID: 14626
				Escaped,
				// Token: 0x04003923 RID: 14627
				NotEscaped,
				// Token: 0x04003924 RID: 14628
				StartOfToken,
				// Token: 0x04003925 RID: 14629
				EndOfLine
			}
		}

		// Token: 0x02000B01 RID: 2817
		private class TimeZoneInfoComparer : IComparer<TimeZoneInfo>
		{
			// Token: 0x06006A99 RID: 27289 RVA: 0x00170E9C File Offset: 0x0016F09C
			int IComparer<TimeZoneInfo>.Compare(TimeZoneInfo x, TimeZoneInfo y)
			{
				int num = x.BaseUtcOffset.CompareTo(y.BaseUtcOffset);
				if (num != 0)
				{
					return num;
				}
				return string.Compare(x.DisplayName, y.DisplayName, StringComparison.Ordinal);
			}
		}
	}
}
