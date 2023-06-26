using System;
using System.Text;

namespace System.Globalization
{
	// Token: 0x020003D6 RID: 982
	internal static class TimeSpanParse
	{
		// Token: 0x0600320B RID: 12811 RVA: 0x000C0CDD File Offset: 0x000BEEDD
		internal static void ValidateStyles(TimeSpanStyles style, string parameterName)
		{
			if (style != TimeSpanStyles.None && style != TimeSpanStyles.AssumeNegative)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidTimeSpanStyles"), parameterName);
			}
		}

		// Token: 0x0600320C RID: 12812 RVA: 0x000C0CF8 File Offset: 0x000BEEF8
		private static bool TryTimeToTicks(bool positive, TimeSpanParse.TimeSpanToken days, TimeSpanParse.TimeSpanToken hours, TimeSpanParse.TimeSpanToken minutes, TimeSpanParse.TimeSpanToken seconds, TimeSpanParse.TimeSpanToken fraction, out long result)
		{
			if (days.IsInvalidNumber(10675199, -1) || hours.IsInvalidNumber(23, -1) || minutes.IsInvalidNumber(59, -1) || seconds.IsInvalidNumber(59, -1) || fraction.IsInvalidNumber(9999999, 7))
			{
				result = 0L;
				return false;
			}
			long num = ((long)days.num * 3600L * 24L + (long)hours.num * 3600L + (long)minutes.num * 60L + (long)seconds.num) * 1000L;
			if (num > 922337203685477L || num < -922337203685477L)
			{
				result = 0L;
				return false;
			}
			long num2 = (long)fraction.num;
			if (num2 != 0L)
			{
				long num3 = 1000000L;
				if (fraction.zeroes > 0)
				{
					long num4 = (long)Math.Pow(10.0, (double)fraction.zeroes);
					num3 /= num4;
				}
				while (num2 < num3)
				{
					num2 *= 10L;
				}
			}
			result = num * 10000L + num2;
			if (positive && result < 0L)
			{
				result = 0L;
				return false;
			}
			return true;
		}

		// Token: 0x0600320D RID: 12813 RVA: 0x000C0E10 File Offset: 0x000BF010
		internal static TimeSpan Parse(string input, IFormatProvider formatProvider)
		{
			TimeSpanParse.TimeSpanResult timeSpanResult = default(TimeSpanParse.TimeSpanResult);
			timeSpanResult.Init(TimeSpanParse.TimeSpanThrowStyle.All);
			if (TimeSpanParse.TryParseTimeSpan(input, TimeSpanParse.TimeSpanStandardStyles.Any, formatProvider, ref timeSpanResult))
			{
				return timeSpanResult.parsedTimeSpan;
			}
			throw timeSpanResult.GetTimeSpanParseException();
		}

		// Token: 0x0600320E RID: 12814 RVA: 0x000C0E48 File Offset: 0x000BF048
		internal static bool TryParse(string input, IFormatProvider formatProvider, out TimeSpan result)
		{
			TimeSpanParse.TimeSpanResult timeSpanResult = default(TimeSpanParse.TimeSpanResult);
			timeSpanResult.Init(TimeSpanParse.TimeSpanThrowStyle.None);
			if (TimeSpanParse.TryParseTimeSpan(input, TimeSpanParse.TimeSpanStandardStyles.Any, formatProvider, ref timeSpanResult))
			{
				result = timeSpanResult.parsedTimeSpan;
				return true;
			}
			result = default(TimeSpan);
			return false;
		}

		// Token: 0x0600320F RID: 12815 RVA: 0x000C0E88 File Offset: 0x000BF088
		internal static TimeSpan ParseExact(string input, string format, IFormatProvider formatProvider, TimeSpanStyles styles)
		{
			TimeSpanParse.TimeSpanResult timeSpanResult = default(TimeSpanParse.TimeSpanResult);
			timeSpanResult.Init(TimeSpanParse.TimeSpanThrowStyle.All);
			if (TimeSpanParse.TryParseExactTimeSpan(input, format, formatProvider, styles, ref timeSpanResult))
			{
				return timeSpanResult.parsedTimeSpan;
			}
			throw timeSpanResult.GetTimeSpanParseException();
		}

		// Token: 0x06003210 RID: 12816 RVA: 0x000C0EC0 File Offset: 0x000BF0C0
		internal static bool TryParseExact(string input, string format, IFormatProvider formatProvider, TimeSpanStyles styles, out TimeSpan result)
		{
			TimeSpanParse.TimeSpanResult timeSpanResult = default(TimeSpanParse.TimeSpanResult);
			timeSpanResult.Init(TimeSpanParse.TimeSpanThrowStyle.None);
			if (TimeSpanParse.TryParseExactTimeSpan(input, format, formatProvider, styles, ref timeSpanResult))
			{
				result = timeSpanResult.parsedTimeSpan;
				return true;
			}
			result = default(TimeSpan);
			return false;
		}

		// Token: 0x06003211 RID: 12817 RVA: 0x000C0F04 File Offset: 0x000BF104
		internal static TimeSpan ParseExactMultiple(string input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles)
		{
			TimeSpanParse.TimeSpanResult timeSpanResult = default(TimeSpanParse.TimeSpanResult);
			timeSpanResult.Init(TimeSpanParse.TimeSpanThrowStyle.All);
			if (TimeSpanParse.TryParseExactMultipleTimeSpan(input, formats, formatProvider, styles, ref timeSpanResult))
			{
				return timeSpanResult.parsedTimeSpan;
			}
			throw timeSpanResult.GetTimeSpanParseException();
		}

		// Token: 0x06003212 RID: 12818 RVA: 0x000C0F3C File Offset: 0x000BF13C
		internal static bool TryParseExactMultiple(string input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles, out TimeSpan result)
		{
			TimeSpanParse.TimeSpanResult timeSpanResult = default(TimeSpanParse.TimeSpanResult);
			timeSpanResult.Init(TimeSpanParse.TimeSpanThrowStyle.None);
			if (TimeSpanParse.TryParseExactMultipleTimeSpan(input, formats, formatProvider, styles, ref timeSpanResult))
			{
				result = timeSpanResult.parsedTimeSpan;
				return true;
			}
			result = default(TimeSpan);
			return false;
		}

		// Token: 0x06003213 RID: 12819 RVA: 0x000C0F80 File Offset: 0x000BF180
		private static bool TryParseTimeSpan(string input, TimeSpanParse.TimeSpanStandardStyles style, IFormatProvider formatProvider, ref TimeSpanParse.TimeSpanResult result)
		{
			if (input == null)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.ArgumentNull, "ArgumentNull_String", null, "input");
				return false;
			}
			input = input.Trim();
			if (input == string.Empty)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
				return false;
			}
			TimeSpanParse.TimeSpanTokenizer timeSpanTokenizer = default(TimeSpanParse.TimeSpanTokenizer);
			timeSpanTokenizer.Init(input);
			TimeSpanParse.TimeSpanRawInfo timeSpanRawInfo = default(TimeSpanParse.TimeSpanRawInfo);
			timeSpanRawInfo.Init(DateTimeFormatInfo.GetInstance(formatProvider));
			TimeSpanParse.TimeSpanToken timeSpanToken = timeSpanTokenizer.GetNextToken();
			while (timeSpanToken.ttt != TimeSpanParse.TTT.End)
			{
				if (!timeSpanRawInfo.ProcessToken(ref timeSpanToken, ref result))
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
					return false;
				}
				timeSpanToken = timeSpanTokenizer.GetNextToken();
			}
			if (!timeSpanTokenizer.EOL)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
				return false;
			}
			if (!TimeSpanParse.ProcessTerminalState(ref timeSpanRawInfo, style, ref result))
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
				return false;
			}
			return true;
		}

		// Token: 0x06003214 RID: 12820 RVA: 0x000C1054 File Offset: 0x000BF254
		private static bool ProcessTerminalState(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw.lastSeenTTT == TimeSpanParse.TTT.Num)
			{
				TimeSpanParse.TimeSpanToken timeSpanToken = default(TimeSpanParse.TimeSpanToken);
				timeSpanToken.ttt = TimeSpanParse.TTT.Sep;
				timeSpanToken.sep = string.Empty;
				if (!raw.ProcessToken(ref timeSpanToken, ref result))
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
					return false;
				}
			}
			switch (raw.NumCount)
			{
			case 1:
				return TimeSpanParse.ProcessTerminal_D(ref raw, style, ref result);
			case 2:
				return TimeSpanParse.ProcessTerminal_HM(ref raw, style, ref result);
			case 3:
				return TimeSpanParse.ProcessTerminal_HM_S_D(ref raw, style, ref result);
			case 4:
				return TimeSpanParse.ProcessTerminal_HMS_F_D(ref raw, style, ref result);
			case 5:
				return TimeSpanParse.ProcessTerminal_DHMSF(ref raw, style, ref result);
			default:
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
				return false;
			}
		}

		// Token: 0x06003215 RID: 12821 RVA: 0x000C1100 File Offset: 0x000BF300
		private static bool ProcessTerminal_DHMSF(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw.SepCount != 6 || raw.NumCount != 5)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
				return false;
			}
			bool flag = (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag2 = (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag3 = false;
			bool flag4 = false;
			if (flag)
			{
				if (raw.FullMatch(raw.PositiveInvariant))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullMatch(raw.NegativeInvariant))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			if (flag2)
			{
				if (!flag4 && raw.FullMatch(raw.PositiveLocalized))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullMatch(raw.NegativeLocalized))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			if (!flag4)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
				return false;
			}
			long num;
			if (!TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], raw.numbers[3], raw.numbers[4], out num))
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
				return false;
			}
			if (!flag3)
			{
				num = -num;
				if (num > 0L)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
					return false;
				}
			}
			result.parsedTimeSpan._ticks = num;
			return true;
		}

		// Token: 0x06003216 RID: 12822 RVA: 0x000C1228 File Offset: 0x000BF428
		private static bool ProcessTerminal_HMS_F_D(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw.SepCount != 5 || raw.NumCount != 4 || (style & TimeSpanParse.TimeSpanStandardStyles.RequireFull) != TimeSpanParse.TimeSpanStandardStyles.None)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
				return false;
			}
			bool flag = (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag2 = (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > TimeSpanParse.TimeSpanStandardStyles.None;
			long num = 0L;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			if (flag)
			{
				if (raw.FullHMSFMatch(raw.PositiveInvariant))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], raw.numbers[2], raw.numbers[3], out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullDHMSMatch(raw.PositiveInvariant))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], raw.numbers[3], TimeSpanParse.zero, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullAppCompatMatch(raw.PositiveInvariant))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, raw.numbers[3], out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullHMSFMatch(raw.NegativeInvariant))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], raw.numbers[2], raw.numbers[3], out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullDHMSMatch(raw.NegativeInvariant))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], raw.numbers[3], TimeSpanParse.zero, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullAppCompatMatch(raw.NegativeInvariant))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, raw.numbers[3], out num);
					flag5 = flag5 || !flag4;
				}
			}
			if (flag2)
			{
				if (!flag4 && raw.FullHMSFMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], raw.numbers[2], raw.numbers[3], out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullDHMSMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], raw.numbers[3], TimeSpanParse.zero, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullAppCompatMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, raw.numbers[3], out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullHMSFMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], raw.numbers[2], raw.numbers[3], out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullDHMSMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], raw.numbers[3], TimeSpanParse.zero, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullAppCompatMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, raw.numbers[3], out num);
					flag5 = flag5 || !flag4;
				}
			}
			if (flag4)
			{
				if (!flag3)
				{
					num = -num;
					if (num > 0L)
					{
						result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
						return false;
					}
				}
				result.parsedTimeSpan._ticks = num;
				return true;
			}
			if (flag5)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
				return false;
			}
			result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
			return false;
		}

		// Token: 0x06003217 RID: 12823 RVA: 0x000C1754 File Offset: 0x000BF954
		private static bool ProcessTerminal_HM_S_D(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw.SepCount != 4 || raw.NumCount != 3 || (style & TimeSpanParse.TimeSpanStandardStyles.RequireFull) != TimeSpanParse.TimeSpanStandardStyles.None)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
				return false;
			}
			bool flag = (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag2 = (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			long num = 0L;
			if (flag)
			{
				if (raw.FullHMSMatch(raw.PositiveInvariant))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullDHMMatch(raw.PositiveInvariant))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, TimeSpanParse.zero, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.PartialAppCompatMatch(raw.PositiveInvariant))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], TimeSpanParse.zero, raw.numbers[2], out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullHMSMatch(raw.NegativeInvariant))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullDHMMatch(raw.NegativeInvariant))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, TimeSpanParse.zero, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.PartialAppCompatMatch(raw.NegativeInvariant))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], TimeSpanParse.zero, raw.numbers[2], out num);
					flag5 = flag5 || !flag4;
				}
			}
			if (flag2)
			{
				if (!flag4 && raw.FullHMSMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullDHMMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, TimeSpanParse.zero, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.PartialAppCompatMatch(raw.PositiveLocalized))
				{
					flag3 = true;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], TimeSpanParse.zero, raw.numbers[2], out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullHMSMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.FullDHMMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], raw.numbers[1], raw.numbers[2], TimeSpanParse.zero, TimeSpanParse.zero, out num);
					flag5 = flag5 || !flag4;
				}
				if (!flag4 && raw.PartialAppCompatMatch(raw.NegativeLocalized))
				{
					flag3 = false;
					flag4 = TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], TimeSpanParse.zero, raw.numbers[2], out num);
					flag5 = flag5 || !flag4;
				}
			}
			if (flag4)
			{
				if (!flag3)
				{
					num = -num;
					if (num > 0L)
					{
						result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
						return false;
					}
				}
				result.parsedTimeSpan._ticks = num;
				return true;
			}
			if (flag5)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
				return false;
			}
			result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
			return false;
		}

		// Token: 0x06003218 RID: 12824 RVA: 0x000C1C0C File Offset: 0x000BFE0C
		private static bool ProcessTerminal_HM(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw.SepCount != 3 || raw.NumCount != 2 || (style & TimeSpanParse.TimeSpanStandardStyles.RequireFull) != TimeSpanParse.TimeSpanStandardStyles.None)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
				return false;
			}
			bool flag = (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag2 = (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag3 = false;
			bool flag4 = false;
			if (flag)
			{
				if (raw.FullHMMatch(raw.PositiveInvariant))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullHMMatch(raw.NegativeInvariant))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			if (flag2)
			{
				if (!flag4 && raw.FullHMMatch(raw.PositiveLocalized))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullHMMatch(raw.NegativeLocalized))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			long num = 0L;
			if (!flag4)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
				return false;
			}
			if (!TimeSpanParse.TryTimeToTicks(flag3, TimeSpanParse.zero, raw.numbers[0], raw.numbers[1], TimeSpanParse.zero, TimeSpanParse.zero, out num))
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
				return false;
			}
			if (!flag3)
			{
				num = -num;
				if (num > 0L)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
					return false;
				}
			}
			result.parsedTimeSpan._ticks = num;
			return true;
		}

		// Token: 0x06003219 RID: 12825 RVA: 0x000C1D28 File Offset: 0x000BFF28
		private static bool ProcessTerminal_D(ref TimeSpanParse.TimeSpanRawInfo raw, TimeSpanParse.TimeSpanStandardStyles style, ref TimeSpanParse.TimeSpanResult result)
		{
			if (raw.SepCount != 2 || raw.NumCount != 1 || (style & TimeSpanParse.TimeSpanStandardStyles.RequireFull) != TimeSpanParse.TimeSpanStandardStyles.None)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
				return false;
			}
			bool flag = (style & TimeSpanParse.TimeSpanStandardStyles.Invariant) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag2 = (style & TimeSpanParse.TimeSpanStandardStyles.Localized) > TimeSpanParse.TimeSpanStandardStyles.None;
			bool flag3 = false;
			bool flag4 = false;
			if (flag)
			{
				if (raw.FullDMatch(raw.PositiveInvariant))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullDMatch(raw.NegativeInvariant))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			if (flag2)
			{
				if (!flag4 && raw.FullDMatch(raw.PositiveLocalized))
				{
					flag4 = true;
					flag3 = true;
				}
				if (!flag4 && raw.FullDMatch(raw.NegativeLocalized))
				{
					flag4 = true;
					flag3 = false;
				}
			}
			long num = 0L;
			if (!flag4)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
				return false;
			}
			if (!TimeSpanParse.TryTimeToTicks(flag3, raw.numbers[0], TimeSpanParse.zero, TimeSpanParse.zero, TimeSpanParse.zero, TimeSpanParse.zero, out num))
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
				return false;
			}
			if (!flag3)
			{
				num = -num;
				if (num > 0L)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
					return false;
				}
			}
			result.parsedTimeSpan._ticks = num;
			return true;
		}

		// Token: 0x0600321A RID: 12826 RVA: 0x000C1E3C File Offset: 0x000C003C
		private static bool TryParseExactTimeSpan(string input, string format, IFormatProvider formatProvider, TimeSpanStyles styles, ref TimeSpanParse.TimeSpanResult result)
		{
			if (input == null)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.ArgumentNull, "ArgumentNull_String", null, "input");
				return false;
			}
			if (format == null)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.ArgumentNull, "ArgumentNull_String", null, "format");
				return false;
			}
			if (format.Length == 0)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadFormatSpecifier");
				return false;
			}
			if (format.Length != 1)
			{
				return TimeSpanParse.TryParseByFormat(input, format, styles, ref result);
			}
			if (format[0] == 'c' || format[0] == 't' || format[0] == 'T')
			{
				return TimeSpanParse.TryParseTimeSpanConstant(input, ref result);
			}
			TimeSpanParse.TimeSpanStandardStyles timeSpanStandardStyles;
			if (format[0] == 'g')
			{
				timeSpanStandardStyles = TimeSpanParse.TimeSpanStandardStyles.Localized;
			}
			else
			{
				if (format[0] != 'G')
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadFormatSpecifier");
					return false;
				}
				timeSpanStandardStyles = TimeSpanParse.TimeSpanStandardStyles.Localized | TimeSpanParse.TimeSpanStandardStyles.RequireFull;
			}
			return TimeSpanParse.TryParseTimeSpan(input, timeSpanStandardStyles, formatProvider, ref result);
		}

		// Token: 0x0600321B RID: 12827 RVA: 0x000C1F08 File Offset: 0x000C0108
		private static bool TryParseByFormat(string input, string format, TimeSpanStyles styles, ref TimeSpanParse.TimeSpanResult result)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			int i = 0;
			int num7 = 0;
			TimeSpanParse.TimeSpanTokenizer timeSpanTokenizer = default(TimeSpanParse.TimeSpanTokenizer);
			timeSpanTokenizer.Init(input, -1);
			while (i < format.Length)
			{
				char c = format[i];
				if (c <= 'F')
				{
					if (c <= '%')
					{
						if (c != '"')
						{
							if (c != '%')
							{
								goto IL_2C5;
							}
							int num8 = DateTimeFormat.ParseNextChar(format, i);
							if (num8 >= 0 && num8 != 37)
							{
								num7 = 1;
								goto IL_2D3;
							}
							result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_InvalidString");
							return false;
						}
					}
					else if (c != '\'')
					{
						if (c != 'F')
						{
							goto IL_2C5;
						}
						num7 = DateTimeFormat.ParseRepeatPattern(format, i, c);
						if (num7 > 7 || flag5)
						{
							result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_InvalidString");
							return false;
						}
						TimeSpanParse.ParseExactDigits(ref timeSpanTokenizer, num7, num7, out num5, out num6);
						flag5 = true;
						goto IL_2D3;
					}
					StringBuilder stringBuilder = new StringBuilder();
					if (!DateTimeParse.TryParseQuoteString(format, i, stringBuilder, out num7))
					{
						result.SetFailure(TimeSpanParse.ParseFailureKind.FormatWithParameter, "Format_BadQuote", c);
						return false;
					}
					if (!TimeSpanParse.ParseExactLiteral(ref timeSpanTokenizer, stringBuilder))
					{
						result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_InvalidString");
						return false;
					}
				}
				else if (c <= 'h')
				{
					if (c != '\\')
					{
						switch (c)
						{
						case 'd':
						{
							num7 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							int num9 = 0;
							if (num7 > 8 || flag || !TimeSpanParse.ParseExactDigits(ref timeSpanTokenizer, (num7 < 2) ? 1 : num7, (num7 < 2) ? 8 : num7, out num9, out num))
							{
								result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_InvalidString");
								return false;
							}
							flag = true;
							break;
						}
						case 'e':
						case 'g':
							goto IL_2C5;
						case 'f':
							num7 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							if (num7 > 7 || flag5 || !TimeSpanParse.ParseExactDigits(ref timeSpanTokenizer, num7, num7, out num5, out num6))
							{
								result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_InvalidString");
								return false;
							}
							flag5 = true;
							break;
						case 'h':
							num7 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							if (num7 > 2 || flag2 || !TimeSpanParse.ParseExactDigits(ref timeSpanTokenizer, num7, out num2))
							{
								result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_InvalidString");
								return false;
							}
							flag2 = true;
							break;
						default:
							goto IL_2C5;
						}
					}
					else
					{
						int num8 = DateTimeFormat.ParseNextChar(format, i);
						if (num8 < 0 || timeSpanTokenizer.NextChar != (char)num8)
						{
							result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_InvalidString");
							return false;
						}
						num7 = 2;
					}
				}
				else if (c != 'm')
				{
					if (c != 's')
					{
						goto IL_2C5;
					}
					num7 = DateTimeFormat.ParseRepeatPattern(format, i, c);
					if (num7 > 2 || flag4 || !TimeSpanParse.ParseExactDigits(ref timeSpanTokenizer, num7, out num4))
					{
						result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_InvalidString");
						return false;
					}
					flag4 = true;
				}
				else
				{
					num7 = DateTimeFormat.ParseRepeatPattern(format, i, c);
					if (num7 > 2 || flag3 || !TimeSpanParse.ParseExactDigits(ref timeSpanTokenizer, num7, out num3))
					{
						result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_InvalidString");
						return false;
					}
					flag3 = true;
				}
				IL_2D3:
				i += num7;
				continue;
				IL_2C5:
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_InvalidString");
				return false;
			}
			if (!timeSpanTokenizer.EOL)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
				return false;
			}
			long num10 = 0L;
			bool flag6 = (styles & TimeSpanStyles.AssumeNegative) == TimeSpanStyles.None;
			if (TimeSpanParse.TryTimeToTicks(flag6, new TimeSpanParse.TimeSpanToken(num), new TimeSpanParse.TimeSpanToken(num2), new TimeSpanParse.TimeSpanToken(num3), new TimeSpanParse.TimeSpanToken(num4), new TimeSpanParse.TimeSpanToken(num5, num6), out num10))
			{
				if (!flag6)
				{
					num10 = -num10;
				}
				result.parsedTimeSpan._ticks = num10;
				return true;
			}
			result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
			return false;
		}

		// Token: 0x0600321C RID: 12828 RVA: 0x000C2274 File Offset: 0x000C0474
		private static bool ParseExactDigits(ref TimeSpanParse.TimeSpanTokenizer tokenizer, int minDigitLength, out int result)
		{
			result = 0;
			int num = 0;
			int num2 = ((minDigitLength == 1) ? 2 : minDigitLength);
			return TimeSpanParse.ParseExactDigits(ref tokenizer, minDigitLength, num2, out num, out result);
		}

		// Token: 0x0600321D RID: 12829 RVA: 0x000C229C File Offset: 0x000C049C
		private static bool ParseExactDigits(ref TimeSpanParse.TimeSpanTokenizer tokenizer, int minDigitLength, int maxDigitLength, out int zeroes, out int result)
		{
			result = 0;
			zeroes = 0;
			int i;
			for (i = 0; i < maxDigitLength; i++)
			{
				char nextChar = tokenizer.NextChar;
				if (nextChar < '0' || nextChar > '9')
				{
					tokenizer.BackOne();
					break;
				}
				result = result * 10 + (int)(nextChar - '0');
				if (result == 0)
				{
					zeroes++;
				}
			}
			return i >= minDigitLength;
		}

		// Token: 0x0600321E RID: 12830 RVA: 0x000C22F8 File Offset: 0x000C04F8
		private static bool ParseExactLiteral(ref TimeSpanParse.TimeSpanTokenizer tokenizer, StringBuilder enquotedString)
		{
			for (int i = 0; i < enquotedString.Length; i++)
			{
				if (enquotedString[i] != tokenizer.NextChar)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600321F RID: 12831 RVA: 0x000C2328 File Offset: 0x000C0528
		private static bool TryParseTimeSpanConstant(string input, ref TimeSpanParse.TimeSpanResult result)
		{
			return default(TimeSpanParse.StringParser).TryParse(input, ref result);
		}

		// Token: 0x06003220 RID: 12832 RVA: 0x000C2348 File Offset: 0x000C0548
		private static bool TryParseExactMultipleTimeSpan(string input, string[] formats, IFormatProvider formatProvider, TimeSpanStyles styles, ref TimeSpanParse.TimeSpanResult result)
		{
			if (input == null)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.ArgumentNull, "ArgumentNull_String", null, "input");
				return false;
			}
			if (formats == null)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.ArgumentNull, "ArgumentNull_String", null, "formats");
				return false;
			}
			if (input.Length == 0)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
				return false;
			}
			if (formats.Length == 0)
			{
				result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadFormatSpecifier");
				return false;
			}
			for (int i = 0; i < formats.Length; i++)
			{
				if (formats[i] == null || formats[i].Length == 0)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadFormatSpecifier");
					return false;
				}
				TimeSpanParse.TimeSpanResult timeSpanResult = default(TimeSpanParse.TimeSpanResult);
				timeSpanResult.Init(TimeSpanParse.TimeSpanThrowStyle.None);
				if (TimeSpanParse.TryParseExactTimeSpan(input, formats[i], formatProvider, styles, ref timeSpanResult))
				{
					result.parsedTimeSpan = timeSpanResult.parsedTimeSpan;
					return true;
				}
			}
			result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
			return false;
		}

		// Token: 0x0400154D RID: 5453
		internal const int unlimitedDigits = -1;

		// Token: 0x0400154E RID: 5454
		internal const int maxFractionDigits = 7;

		// Token: 0x0400154F RID: 5455
		internal const int maxDays = 10675199;

		// Token: 0x04001550 RID: 5456
		internal const int maxHours = 23;

		// Token: 0x04001551 RID: 5457
		internal const int maxMinutes = 59;

		// Token: 0x04001552 RID: 5458
		internal const int maxSeconds = 59;

		// Token: 0x04001553 RID: 5459
		internal const int maxFraction = 9999999;

		// Token: 0x04001554 RID: 5460
		private static readonly TimeSpanParse.TimeSpanToken zero = new TimeSpanParse.TimeSpanToken(0);

		// Token: 0x02000B6F RID: 2927
		private enum TimeSpanThrowStyle
		{
			// Token: 0x04003480 RID: 13440
			None,
			// Token: 0x04003481 RID: 13441
			All
		}

		// Token: 0x02000B70 RID: 2928
		private enum ParseFailureKind
		{
			// Token: 0x04003483 RID: 13443
			None,
			// Token: 0x04003484 RID: 13444
			ArgumentNull,
			// Token: 0x04003485 RID: 13445
			Format,
			// Token: 0x04003486 RID: 13446
			FormatWithParameter,
			// Token: 0x04003487 RID: 13447
			Overflow
		}

		// Token: 0x02000B71 RID: 2929
		[Flags]
		private enum TimeSpanStandardStyles
		{
			// Token: 0x04003489 RID: 13449
			None = 0,
			// Token: 0x0400348A RID: 13450
			Invariant = 1,
			// Token: 0x0400348B RID: 13451
			Localized = 2,
			// Token: 0x0400348C RID: 13452
			RequireFull = 4,
			// Token: 0x0400348D RID: 13453
			Any = 3
		}

		// Token: 0x02000B72 RID: 2930
		private enum TTT
		{
			// Token: 0x0400348F RID: 13455
			None,
			// Token: 0x04003490 RID: 13456
			End,
			// Token: 0x04003491 RID: 13457
			Num,
			// Token: 0x04003492 RID: 13458
			Sep,
			// Token: 0x04003493 RID: 13459
			NumOverflow
		}

		// Token: 0x02000B73 RID: 2931
		private struct TimeSpanToken
		{
			// Token: 0x06006C4B RID: 27723 RVA: 0x00177EF0 File Offset: 0x001760F0
			public TimeSpanToken(int number)
			{
				this.ttt = TimeSpanParse.TTT.Num;
				this.num = number;
				this.zeroes = 0;
				this.sep = null;
			}

			// Token: 0x06006C4C RID: 27724 RVA: 0x00177F0E File Offset: 0x0017610E
			public TimeSpanToken(int leadingZeroes, int number)
			{
				this.ttt = TimeSpanParse.TTT.Num;
				this.num = number;
				this.zeroes = leadingZeroes;
				this.sep = null;
			}

			// Token: 0x06006C4D RID: 27725 RVA: 0x00177F2C File Offset: 0x0017612C
			public bool IsInvalidNumber(int maxValue, int maxPrecision)
			{
				return this.num > maxValue || (maxPrecision != -1 && (this.zeroes > maxPrecision || (this.num != 0 && this.zeroes != 0 && (long)this.num >= (long)maxValue / (long)Math.Pow(10.0, (double)(this.zeroes - 1)))));
			}

			// Token: 0x04003494 RID: 13460
			internal TimeSpanParse.TTT ttt;

			// Token: 0x04003495 RID: 13461
			internal int num;

			// Token: 0x04003496 RID: 13462
			internal int zeroes;

			// Token: 0x04003497 RID: 13463
			internal string sep;
		}

		// Token: 0x02000B74 RID: 2932
		private struct TimeSpanTokenizer
		{
			// Token: 0x06006C4E RID: 27726 RVA: 0x00177F8E File Offset: 0x0017618E
			internal void Init(string input)
			{
				this.Init(input, 0);
			}

			// Token: 0x06006C4F RID: 27727 RVA: 0x00177F98 File Offset: 0x00176198
			internal void Init(string input, int startPosition)
			{
				this.m_pos = startPosition;
				this.m_value = input;
			}

			// Token: 0x06006C50 RID: 27728 RVA: 0x00177FA8 File Offset: 0x001761A8
			internal TimeSpanParse.TimeSpanToken GetNextToken()
			{
				TimeSpanParse.TimeSpanToken timeSpanToken = default(TimeSpanParse.TimeSpanToken);
				char c = this.CurrentChar;
				if (c == '\0')
				{
					timeSpanToken.ttt = TimeSpanParse.TTT.End;
					return timeSpanToken;
				}
				if (c >= '0' && c <= '9')
				{
					timeSpanToken.ttt = TimeSpanParse.TTT.Num;
					timeSpanToken.num = 0;
					timeSpanToken.zeroes = 0;
					while (((long)timeSpanToken.num & (long)((ulong)(-268435456))) == 0L)
					{
						timeSpanToken.num = timeSpanToken.num * 10 + (int)c - 48;
						if (timeSpanToken.num == 0)
						{
							timeSpanToken.zeroes++;
						}
						if (timeSpanToken.num < 0)
						{
							timeSpanToken.ttt = TimeSpanParse.TTT.NumOverflow;
							return timeSpanToken;
						}
						c = this.NextChar;
						if (c < '0' || c > '9')
						{
							return timeSpanToken;
						}
					}
					timeSpanToken.ttt = TimeSpanParse.TTT.NumOverflow;
					return timeSpanToken;
				}
				timeSpanToken.ttt = TimeSpanParse.TTT.Sep;
				int pos = this.m_pos;
				int num = 0;
				while (c != '\0' && (c < '0' || '9' < c))
				{
					c = this.NextChar;
					num++;
				}
				timeSpanToken.sep = this.m_value.Substring(pos, num);
				return timeSpanToken;
			}

			// Token: 0x17001254 RID: 4692
			// (get) Token: 0x06006C51 RID: 27729 RVA: 0x001780A2 File Offset: 0x001762A2
			internal bool EOL
			{
				get
				{
					return this.m_pos >= this.m_value.Length - 1;
				}
			}

			// Token: 0x06006C52 RID: 27730 RVA: 0x001780BC File Offset: 0x001762BC
			internal void BackOne()
			{
				if (this.m_pos > 0)
				{
					this.m_pos--;
				}
			}

			// Token: 0x17001255 RID: 4693
			// (get) Token: 0x06006C53 RID: 27731 RVA: 0x001780D5 File Offset: 0x001762D5
			internal char NextChar
			{
				get
				{
					this.m_pos++;
					return this.CurrentChar;
				}
			}

			// Token: 0x17001256 RID: 4694
			// (get) Token: 0x06006C54 RID: 27732 RVA: 0x001780EB File Offset: 0x001762EB
			internal char CurrentChar
			{
				get
				{
					if (this.m_pos > -1 && this.m_pos < this.m_value.Length)
					{
						return this.m_value[this.m_pos];
					}
					return '\0';
				}
			}

			// Token: 0x04003498 RID: 13464
			private int m_pos;

			// Token: 0x04003499 RID: 13465
			private string m_value;
		}

		// Token: 0x02000B75 RID: 2933
		private struct TimeSpanRawInfo
		{
			// Token: 0x17001257 RID: 4695
			// (get) Token: 0x06006C55 RID: 27733 RVA: 0x0017811C File Offset: 0x0017631C
			internal TimeSpanFormat.FormatLiterals PositiveInvariant
			{
				get
				{
					return TimeSpanFormat.PositiveInvariantFormatLiterals;
				}
			}

			// Token: 0x17001258 RID: 4696
			// (get) Token: 0x06006C56 RID: 27734 RVA: 0x00178123 File Offset: 0x00176323
			internal TimeSpanFormat.FormatLiterals NegativeInvariant
			{
				get
				{
					return TimeSpanFormat.NegativeInvariantFormatLiterals;
				}
			}

			// Token: 0x17001259 RID: 4697
			// (get) Token: 0x06006C57 RID: 27735 RVA: 0x0017812A File Offset: 0x0017632A
			internal TimeSpanFormat.FormatLiterals PositiveLocalized
			{
				get
				{
					if (!this.m_posLocInit)
					{
						this.m_posLoc = default(TimeSpanFormat.FormatLiterals);
						this.m_posLoc.Init(this.m_fullPosPattern, false);
						this.m_posLocInit = true;
					}
					return this.m_posLoc;
				}
			}

			// Token: 0x1700125A RID: 4698
			// (get) Token: 0x06006C58 RID: 27736 RVA: 0x0017815F File Offset: 0x0017635F
			internal TimeSpanFormat.FormatLiterals NegativeLocalized
			{
				get
				{
					if (!this.m_negLocInit)
					{
						this.m_negLoc = default(TimeSpanFormat.FormatLiterals);
						this.m_negLoc.Init(this.m_fullNegPattern, false);
						this.m_negLocInit = true;
					}
					return this.m_negLoc;
				}
			}

			// Token: 0x06006C59 RID: 27737 RVA: 0x00178194 File Offset: 0x00176394
			internal bool FullAppCompatMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this.SepCount == 5 && this.NumCount == 4 && pattern.Start == this.literals[0] && pattern.DayHourSep == this.literals[1] && pattern.HourMinuteSep == this.literals[2] && pattern.AppCompatLiteral == this.literals[3] && pattern.End == this.literals[4];
			}

			// Token: 0x06006C5A RID: 27738 RVA: 0x00178220 File Offset: 0x00176420
			internal bool PartialAppCompatMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this.SepCount == 4 && this.NumCount == 3 && pattern.Start == this.literals[0] && pattern.HourMinuteSep == this.literals[1] && pattern.AppCompatLiteral == this.literals[2] && pattern.End == this.literals[3];
			}

			// Token: 0x06006C5B RID: 27739 RVA: 0x00178298 File Offset: 0x00176498
			internal bool FullMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this.SepCount == 6 && this.NumCount == 5 && pattern.Start == this.literals[0] && pattern.DayHourSep == this.literals[1] && pattern.HourMinuteSep == this.literals[2] && pattern.MinuteSecondSep == this.literals[3] && pattern.SecondFractionSep == this.literals[4] && pattern.End == this.literals[5];
			}

			// Token: 0x06006C5C RID: 27740 RVA: 0x00178341 File Offset: 0x00176541
			internal bool FullDMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this.SepCount == 2 && this.NumCount == 1 && pattern.Start == this.literals[0] && pattern.End == this.literals[1];
			}

			// Token: 0x06006C5D RID: 27741 RVA: 0x00178384 File Offset: 0x00176584
			internal bool FullHMMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this.SepCount == 3 && this.NumCount == 2 && pattern.Start == this.literals[0] && pattern.HourMinuteSep == this.literals[1] && pattern.End == this.literals[2];
			}

			// Token: 0x06006C5E RID: 27742 RVA: 0x001783E8 File Offset: 0x001765E8
			internal bool FullDHMMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this.SepCount == 4 && this.NumCount == 3 && pattern.Start == this.literals[0] && pattern.DayHourSep == this.literals[1] && pattern.HourMinuteSep == this.literals[2] && pattern.End == this.literals[3];
			}

			// Token: 0x06006C5F RID: 27743 RVA: 0x00178460 File Offset: 0x00176660
			internal bool FullHMSMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this.SepCount == 4 && this.NumCount == 3 && pattern.Start == this.literals[0] && pattern.HourMinuteSep == this.literals[1] && pattern.MinuteSecondSep == this.literals[2] && pattern.End == this.literals[3];
			}

			// Token: 0x06006C60 RID: 27744 RVA: 0x001784D8 File Offset: 0x001766D8
			internal bool FullDHMSMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this.SepCount == 5 && this.NumCount == 4 && pattern.Start == this.literals[0] && pattern.DayHourSep == this.literals[1] && pattern.HourMinuteSep == this.literals[2] && pattern.MinuteSecondSep == this.literals[3] && pattern.End == this.literals[4];
			}

			// Token: 0x06006C61 RID: 27745 RVA: 0x00178568 File Offset: 0x00176768
			internal bool FullHMSFMatch(TimeSpanFormat.FormatLiterals pattern)
			{
				return this.SepCount == 5 && this.NumCount == 4 && pattern.Start == this.literals[0] && pattern.HourMinuteSep == this.literals[1] && pattern.MinuteSecondSep == this.literals[2] && pattern.SecondFractionSep == this.literals[3] && pattern.End == this.literals[4];
			}

			// Token: 0x06006C62 RID: 27746 RVA: 0x001785F8 File Offset: 0x001767F8
			internal void Init(DateTimeFormatInfo dtfi)
			{
				this.lastSeenTTT = TimeSpanParse.TTT.None;
				this.tokenCount = 0;
				this.SepCount = 0;
				this.NumCount = 0;
				this.literals = new string[6];
				this.numbers = new TimeSpanParse.TimeSpanToken[5];
				this.m_fullPosPattern = dtfi.FullTimeSpanPositivePattern;
				this.m_fullNegPattern = dtfi.FullTimeSpanNegativePattern;
				this.m_posLocInit = false;
				this.m_negLocInit = false;
			}

			// Token: 0x06006C63 RID: 27747 RVA: 0x00178660 File Offset: 0x00176860
			internal bool ProcessToken(ref TimeSpanParse.TimeSpanToken tok, ref TimeSpanParse.TimeSpanResult result)
			{
				if (tok.ttt == TimeSpanParse.TTT.NumOverflow)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge", null);
					return false;
				}
				if (tok.ttt != TimeSpanParse.TTT.Sep && tok.ttt != TimeSpanParse.TTT.Num)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan", null);
					return false;
				}
				TimeSpanParse.TTT ttt = tok.ttt;
				if (ttt != TimeSpanParse.TTT.Num)
				{
					if (ttt == TimeSpanParse.TTT.Sep && !this.AddSep(tok.sep, ref result))
					{
						return false;
					}
				}
				else
				{
					if (this.tokenCount == 0 && !this.AddSep(string.Empty, ref result))
					{
						return false;
					}
					if (!this.AddNum(tok, ref result))
					{
						return false;
					}
				}
				this.lastSeenTTT = tok.ttt;
				return true;
			}

			// Token: 0x06006C64 RID: 27748 RVA: 0x001786FC File Offset: 0x001768FC
			private bool AddSep(string sep, ref TimeSpanParse.TimeSpanResult result)
			{
				if (this.SepCount >= 6 || this.tokenCount >= 11)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan", null);
					return false;
				}
				string[] array = this.literals;
				int sepCount = this.SepCount;
				this.SepCount = sepCount + 1;
				array[sepCount] = sep;
				this.tokenCount++;
				return true;
			}

			// Token: 0x06006C65 RID: 27749 RVA: 0x00178754 File Offset: 0x00176954
			private bool AddNum(TimeSpanParse.TimeSpanToken num, ref TimeSpanParse.TimeSpanResult result)
			{
				if (this.NumCount >= 5 || this.tokenCount >= 11)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan", null);
					return false;
				}
				TimeSpanParse.TimeSpanToken[] array = this.numbers;
				int numCount = this.NumCount;
				this.NumCount = numCount + 1;
				array[numCount] = num;
				this.tokenCount++;
				return true;
			}

			// Token: 0x0400349A RID: 13466
			internal TimeSpanParse.TTT lastSeenTTT;

			// Token: 0x0400349B RID: 13467
			internal int tokenCount;

			// Token: 0x0400349C RID: 13468
			internal int SepCount;

			// Token: 0x0400349D RID: 13469
			internal int NumCount;

			// Token: 0x0400349E RID: 13470
			internal string[] literals;

			// Token: 0x0400349F RID: 13471
			internal TimeSpanParse.TimeSpanToken[] numbers;

			// Token: 0x040034A0 RID: 13472
			private TimeSpanFormat.FormatLiterals m_posLoc;

			// Token: 0x040034A1 RID: 13473
			private TimeSpanFormat.FormatLiterals m_negLoc;

			// Token: 0x040034A2 RID: 13474
			private bool m_posLocInit;

			// Token: 0x040034A3 RID: 13475
			private bool m_negLocInit;

			// Token: 0x040034A4 RID: 13476
			private string m_fullPosPattern;

			// Token: 0x040034A5 RID: 13477
			private string m_fullNegPattern;

			// Token: 0x040034A6 RID: 13478
			private const int MaxTokens = 11;

			// Token: 0x040034A7 RID: 13479
			private const int MaxLiteralTokens = 6;

			// Token: 0x040034A8 RID: 13480
			private const int MaxNumericTokens = 5;
		}

		// Token: 0x02000B76 RID: 2934
		private struct TimeSpanResult
		{
			// Token: 0x06006C66 RID: 27750 RVA: 0x001787AF File Offset: 0x001769AF
			internal void Init(TimeSpanParse.TimeSpanThrowStyle canThrow)
			{
				this.parsedTimeSpan = default(TimeSpan);
				this.throwStyle = canThrow;
			}

			// Token: 0x06006C67 RID: 27751 RVA: 0x001787C4 File Offset: 0x001769C4
			internal void SetFailure(TimeSpanParse.ParseFailureKind failure, string failureMessageID)
			{
				this.SetFailure(failure, failureMessageID, null, null);
			}

			// Token: 0x06006C68 RID: 27752 RVA: 0x001787D0 File Offset: 0x001769D0
			internal void SetFailure(TimeSpanParse.ParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument)
			{
				this.SetFailure(failure, failureMessageID, failureMessageFormatArgument, null);
			}

			// Token: 0x06006C69 RID: 27753 RVA: 0x001787DC File Offset: 0x001769DC
			internal void SetFailure(TimeSpanParse.ParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument, string failureArgumentName)
			{
				this.m_failure = failure;
				this.m_failureMessageID = failureMessageID;
				this.m_failureMessageFormatArgument = failureMessageFormatArgument;
				this.m_failureArgumentName = failureArgumentName;
				if (this.throwStyle != TimeSpanParse.TimeSpanThrowStyle.None)
				{
					throw this.GetTimeSpanParseException();
				}
			}

			// Token: 0x06006C6A RID: 27754 RVA: 0x0017880C File Offset: 0x00176A0C
			internal Exception GetTimeSpanParseException()
			{
				switch (this.m_failure)
				{
				case TimeSpanParse.ParseFailureKind.ArgumentNull:
					return new ArgumentNullException(this.m_failureArgumentName, Environment.GetResourceString(this.m_failureMessageID));
				case TimeSpanParse.ParseFailureKind.Format:
					return new FormatException(Environment.GetResourceString(this.m_failureMessageID));
				case TimeSpanParse.ParseFailureKind.FormatWithParameter:
					return new FormatException(Environment.GetResourceString(this.m_failureMessageID, new object[] { this.m_failureMessageFormatArgument }));
				case TimeSpanParse.ParseFailureKind.Overflow:
					return new OverflowException(Environment.GetResourceString(this.m_failureMessageID));
				default:
					return new FormatException(Environment.GetResourceString("Format_InvalidString"));
				}
			}

			// Token: 0x040034A9 RID: 13481
			internal TimeSpan parsedTimeSpan;

			// Token: 0x040034AA RID: 13482
			internal TimeSpanParse.TimeSpanThrowStyle throwStyle;

			// Token: 0x040034AB RID: 13483
			internal TimeSpanParse.ParseFailureKind m_failure;

			// Token: 0x040034AC RID: 13484
			internal string m_failureMessageID;

			// Token: 0x040034AD RID: 13485
			internal object m_failureMessageFormatArgument;

			// Token: 0x040034AE RID: 13486
			internal string m_failureArgumentName;
		}

		// Token: 0x02000B77 RID: 2935
		private struct StringParser
		{
			// Token: 0x06006C6B RID: 27755 RVA: 0x001788A4 File Offset: 0x00176AA4
			internal void NextChar()
			{
				if (this.pos < this.len)
				{
					this.pos++;
				}
				this.ch = ((this.pos < this.len) ? this.str[this.pos] : '\0');
			}

			// Token: 0x06006C6C RID: 27756 RVA: 0x001788F8 File Offset: 0x00176AF8
			internal char NextNonDigit()
			{
				for (int i = this.pos; i < this.len; i++)
				{
					char c = this.str[i];
					if (c < '0' || c > '9')
					{
						return c;
					}
				}
				return '\0';
			}

			// Token: 0x06006C6D RID: 27757 RVA: 0x00178938 File Offset: 0x00176B38
			internal bool TryParse(string input, ref TimeSpanParse.TimeSpanResult result)
			{
				result.parsedTimeSpan._ticks = 0L;
				if (input == null)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.ArgumentNull, "ArgumentNull_String", null, "input");
					return false;
				}
				this.str = input;
				this.len = input.Length;
				this.pos = -1;
				this.NextChar();
				this.SkipBlanks();
				bool flag = false;
				if (this.ch == '-')
				{
					flag = true;
					this.NextChar();
				}
				long num;
				if (this.NextNonDigit() == ':')
				{
					if (!this.ParseTime(out num, ref result))
					{
						return false;
					}
				}
				else
				{
					int num2;
					if (!this.ParseInt(10675199, out num2, ref result))
					{
						return false;
					}
					num = (long)num2 * 864000000000L;
					if (this.ch == '.')
					{
						this.NextChar();
						long num3;
						if (!this.ParseTime(out num3, ref result))
						{
							return false;
						}
						num += num3;
					}
				}
				if (flag)
				{
					num = -num;
					if (num > 0L)
					{
						result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
						return false;
					}
				}
				else if (num < 0L)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
					return false;
				}
				this.SkipBlanks();
				if (this.pos < this.len)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
					return false;
				}
				result.parsedTimeSpan._ticks = num;
				return true;
			}

			// Token: 0x06006C6E RID: 27758 RVA: 0x00178A58 File Offset: 0x00176C58
			internal bool ParseInt(int max, out int i, ref TimeSpanParse.TimeSpanResult result)
			{
				i = 0;
				int num = this.pos;
				while (this.ch >= '0' && this.ch <= '9')
				{
					if (((long)i & (long)((ulong)(-268435456))) != 0L)
					{
						result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
						return false;
					}
					i = i * 10 + (int)this.ch - 48;
					if (i < 0)
					{
						result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
						return false;
					}
					this.NextChar();
				}
				if (num == this.pos)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
					return false;
				}
				if (i > max)
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Overflow, "Overflow_TimeSpanElementTooLarge");
					return false;
				}
				return true;
			}

			// Token: 0x06006C6F RID: 27759 RVA: 0x00178AF4 File Offset: 0x00176CF4
			internal bool ParseTime(out long time, ref TimeSpanParse.TimeSpanResult result)
			{
				time = 0L;
				int num;
				if (!this.ParseInt(23, out num, ref result))
				{
					return false;
				}
				time = (long)num * 36000000000L;
				if (this.ch != ':')
				{
					result.SetFailure(TimeSpanParse.ParseFailureKind.Format, "Format_BadTimeSpan");
					return false;
				}
				this.NextChar();
				if (!this.ParseInt(59, out num, ref result))
				{
					return false;
				}
				time += (long)num * 600000000L;
				if (this.ch == ':')
				{
					this.NextChar();
					if (this.ch != '.')
					{
						if (!this.ParseInt(59, out num, ref result))
						{
							return false;
						}
						time += (long)num * 10000000L;
					}
					if (this.ch == '.')
					{
						this.NextChar();
						int num2 = 10000000;
						while (num2 > 1 && this.ch >= '0' && this.ch <= '9')
						{
							num2 /= 10;
							time += (long)((int)(this.ch - '0') * num2);
							this.NextChar();
						}
					}
				}
				return true;
			}

			// Token: 0x06006C70 RID: 27760 RVA: 0x00178BE1 File Offset: 0x00176DE1
			internal void SkipBlanks()
			{
				while (this.ch == ' ' || this.ch == '\t')
				{
					this.NextChar();
				}
			}

			// Token: 0x040034AF RID: 13487
			private string str;

			// Token: 0x040034B0 RID: 13488
			private char ch;

			// Token: 0x040034B1 RID: 13489
			private int pos;

			// Token: 0x040034B2 RID: 13490
			private int len;
		}
	}
}
