using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Net.Mime
{
	// Token: 0x02000251 RID: 593
	internal class SmtpDateTime
	{
		// Token: 0x0600167D RID: 5757 RVA: 0x00074A0C File Offset: 0x00072C0C
		internal static IDictionary<string, TimeSpan> InitializeShortHandLookups()
		{
			return new Dictionary<string, TimeSpan>
			{
				{
					"UT",
					TimeSpan.Zero
				},
				{
					"GMT",
					TimeSpan.Zero
				},
				{
					"EDT",
					new TimeSpan(-4, 0, 0)
				},
				{
					"EST",
					new TimeSpan(-5, 0, 0)
				},
				{
					"CDT",
					new TimeSpan(-5, 0, 0)
				},
				{
					"CST",
					new TimeSpan(-6, 0, 0)
				},
				{
					"MDT",
					new TimeSpan(-6, 0, 0)
				},
				{
					"MST",
					new TimeSpan(-7, 0, 0)
				},
				{
					"PDT",
					new TimeSpan(-7, 0, 0)
				},
				{
					"PST",
					new TimeSpan(-8, 0, 0)
				}
			};
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x00074AE0 File Offset: 0x00072CE0
		internal SmtpDateTime(DateTime value)
		{
			this.date = value;
			switch (value.Kind)
			{
			case DateTimeKind.Unspecified:
				this.unknownTimeZone = true;
				return;
			case DateTimeKind.Utc:
				this.timeZone = TimeSpan.Zero;
				return;
			case DateTimeKind.Local:
			{
				TimeSpan utcOffset = TimeZoneInfo.Local.GetUtcOffset(value);
				this.timeZone = this.ValidateAndGetSanitizedTimeSpan(utcOffset);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x00074B44 File Offset: 0x00072D44
		internal SmtpDateTime(string value)
		{
			string text;
			this.date = this.ParseValue(value, out text);
			if (!this.TryParseTimeZoneString(text, out this.timeZone))
			{
				this.unknownTimeZone = true;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001680 RID: 5760 RVA: 0x00074B7C File Offset: 0x00072D7C
		internal DateTime Date
		{
			get
			{
				if (this.unknownTimeZone)
				{
					return DateTime.SpecifyKind(this.date, DateTimeKind.Unspecified);
				}
				DateTimeOffset dateTimeOffset = new DateTimeOffset(this.date, this.timeZone);
				return dateTimeOffset.LocalDateTime;
			}
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x00074BB8 File Offset: 0x00072DB8
		public override string ToString()
		{
			if (this.unknownTimeZone)
			{
				return string.Format("{0} {1}", this.FormatDate(this.date), "-0000");
			}
			return string.Format("{0} {1}", this.FormatDate(this.date), this.TimeSpanToOffset(this.timeZone));
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x00074C0C File Offset: 0x00072E0C
		internal void ValidateAndGetTimeZoneOffsetValues(string offset, out bool positive, out int hours, out int minutes)
		{
			if (offset.Length != 5)
			{
				throw new FormatException(SR.GetString("MailDateInvalidFormat"));
			}
			positive = offset.StartsWith("+");
			if (!int.TryParse(offset.Substring(1, 2), NumberStyles.None, CultureInfo.InvariantCulture, out hours))
			{
				throw new FormatException(SR.GetString("MailDateInvalidFormat"));
			}
			if (!int.TryParse(offset.Substring(3, 2), NumberStyles.None, CultureInfo.InvariantCulture, out minutes))
			{
				throw new FormatException(SR.GetString("MailDateInvalidFormat"));
			}
			if (minutes > 59)
			{
				throw new FormatException(SR.GetString("MailDateInvalidFormat"));
			}
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x00074CA4 File Offset: 0x00072EA4
		internal void ValidateTimeZoneShortHandValue(string value)
		{
			for (int i = 0; i < value.Length; i++)
			{
				if (!char.IsLetter(value, i))
				{
					throw new FormatException(SR.GetString("MailHeaderFieldInvalidCharacter"));
				}
			}
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x00074CDC File Offset: 0x00072EDC
		internal string FormatDate(DateTime value)
		{
			return value.ToString("ddd, dd MMM yyyy HH:mm:ss", CultureInfo.InvariantCulture);
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x00074CFC File Offset: 0x00072EFC
		internal DateTime ParseValue(string data, out string timeZone)
		{
			if (string.IsNullOrEmpty(data))
			{
				throw new FormatException(SR.GetString("MailDateInvalidFormat"));
			}
			int num = data.IndexOf(':');
			if (num == -1)
			{
				throw new FormatException(SR.GetString("MailHeaderFieldInvalidCharacter"));
			}
			int num2 = data.IndexOfAny(SmtpDateTime.allowedWhiteSpaceChars, num);
			if (num2 == -1)
			{
				throw new FormatException(SR.GetString("MailHeaderFieldInvalidCharacter"));
			}
			string text = data.Substring(0, num2).Trim();
			DateTime dateTime;
			if (!DateTime.TryParseExact(text, SmtpDateTime.validDateTimeFormats, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out dateTime))
			{
				throw new FormatException(SR.GetString("MailDateInvalidFormat"));
			}
			string text2 = data.Substring(num2).Trim();
			int num3 = text2.IndexOfAny(SmtpDateTime.allowedWhiteSpaceChars);
			if (num3 != -1)
			{
				text2 = text2.Substring(0, num3);
			}
			if (string.IsNullOrEmpty(text2))
			{
				throw new FormatException(SR.GetString("MailDateInvalidFormat"));
			}
			timeZone = text2;
			return dateTime;
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x00074DE0 File Offset: 0x00072FE0
		internal bool TryParseTimeZoneString(string timeZoneString, out TimeSpan timeZone)
		{
			timeZone = TimeSpan.Zero;
			if (timeZoneString == "-0000")
			{
				return false;
			}
			if (timeZoneString[0] == '+' || timeZoneString[0] == '-')
			{
				bool flag;
				int num;
				int num2;
				this.ValidateAndGetTimeZoneOffsetValues(timeZoneString, out flag, out num, out num2);
				if (!flag)
				{
					if (num != 0)
					{
						num *= -1;
					}
					else if (num2 != 0)
					{
						num2 *= -1;
					}
				}
				timeZone = new TimeSpan(num, num2, 0);
				return true;
			}
			this.ValidateTimeZoneShortHandValue(timeZoneString);
			if (SmtpDateTime.timeZoneOffsetLookup.ContainsKey(timeZoneString))
			{
				timeZone = SmtpDateTime.timeZoneOffsetLookup[timeZoneString];
				return true;
			}
			return false;
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x00074E78 File Offset: 0x00073078
		internal TimeSpan ValidateAndGetSanitizedTimeSpan(TimeSpan span)
		{
			TimeSpan timeSpan = new TimeSpan(span.Days, span.Hours, span.Minutes, 0, 0);
			if (Math.Abs(timeSpan.Ticks) > SmtpDateTime.timeSpanMaxTicks)
			{
				throw new FormatException(SR.GetString("MailDateInvalidFormat"));
			}
			return timeSpan;
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x00074EC8 File Offset: 0x000730C8
		internal string TimeSpanToOffset(TimeSpan span)
		{
			if (span.Ticks == 0L)
			{
				return "+0000";
			}
			uint num = (uint)Math.Abs(Math.Floor(span.TotalHours));
			uint num2 = (uint)Math.Abs(span.Minutes);
			string text = ((span.Ticks > 0L) ? "+" : "-");
			if (num < 10U)
			{
				text += "0";
			}
			text += num.ToString();
			if (num2 < 10U)
			{
				text += "0";
			}
			return text + num2.ToString();
		}

		// Token: 0x04001739 RID: 5945
		internal const string unknownTimeZoneDefaultOffset = "-0000";

		// Token: 0x0400173A RID: 5946
		internal const string utcDefaultTimeZoneOffset = "+0000";

		// Token: 0x0400173B RID: 5947
		internal const int offsetLength = 5;

		// Token: 0x0400173C RID: 5948
		internal const int maxMinuteValue = 59;

		// Token: 0x0400173D RID: 5949
		internal const string dateFormatWithDayOfWeek = "ddd, dd MMM yyyy HH:mm:ss";

		// Token: 0x0400173E RID: 5950
		internal const string dateFormatWithoutDayOfWeek = "dd MMM yyyy HH:mm:ss";

		// Token: 0x0400173F RID: 5951
		internal const string dateFormatWithDayOfWeekAndNoSeconds = "ddd, dd MMM yyyy HH:mm";

		// Token: 0x04001740 RID: 5952
		internal const string dateFormatWithoutDayOfWeekAndNoSeconds = "dd MMM yyyy HH:mm";

		// Token: 0x04001741 RID: 5953
		internal static readonly string[] validDateTimeFormats = new string[] { "ddd, dd MMM yyyy HH:mm:ss", "dd MMM yyyy HH:mm:ss", "ddd, dd MMM yyyy HH:mm", "dd MMM yyyy HH:mm" };

		// Token: 0x04001742 RID: 5954
		internal static readonly char[] allowedWhiteSpaceChars = new char[] { ' ', '\t' };

		// Token: 0x04001743 RID: 5955
		internal static readonly IDictionary<string, TimeSpan> timeZoneOffsetLookup = SmtpDateTime.InitializeShortHandLookups();

		// Token: 0x04001744 RID: 5956
		internal static readonly long timeSpanMaxTicks = 3599400000000L;

		// Token: 0x04001745 RID: 5957
		internal static readonly int offsetMaxValue = 9959;

		// Token: 0x04001746 RID: 5958
		private readonly DateTime date;

		// Token: 0x04001747 RID: 5959
		private readonly TimeSpan timeZone;

		// Token: 0x04001748 RID: 5960
		private readonly bool unknownTimeZone;
	}
}
