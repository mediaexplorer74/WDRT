using System;
using System.Globalization;
using System.IO;
using System.Xml;

namespace Microsoft.Data.OData.Json
{
	// Token: 0x020002AA RID: 682
	internal static class JsonValueUtils
	{
		// Token: 0x060016F8 RID: 5880 RVA: 0x000537C3 File Offset: 0x000519C3
		internal static void WriteValue(TextWriter writer, bool value)
		{
			writer.Write(value ? "true" : "false");
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x000537DA File Offset: 0x000519DA
		internal static void WriteValue(TextWriter writer, int value)
		{
			writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x000537EE File Offset: 0x000519EE
		internal static void WriteValue(TextWriter writer, float value)
		{
			if (float.IsInfinity(value) || float.IsNaN(value))
			{
				JsonValueUtils.WriteQuoted(writer, value.ToString(null, CultureInfo.InvariantCulture));
				return;
			}
			writer.Write(XmlConvert.ToString(value));
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x00053820 File Offset: 0x00051A20
		internal static void WriteValue(TextWriter writer, short value)
		{
			writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x00053834 File Offset: 0x00051A34
		internal static void WriteValue(TextWriter writer, long value)
		{
			JsonValueUtils.WriteQuoted(writer, value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x00053848 File Offset: 0x00051A48
		internal static void WriteValue(TextWriter writer, double value, bool mustIncludeDecimalPoint)
		{
			if (JsonSharedUtils.IsDoubleValueSerializedAsString(value))
			{
				JsonValueUtils.WriteQuoted(writer, value.ToString(null, CultureInfo.InvariantCulture));
				return;
			}
			string text = XmlConvert.ToString(value);
			writer.Write(text);
			if (mustIncludeDecimalPoint && text.IndexOfAny(JsonValueUtils.DoubleIndicatingCharacters) < 0)
			{
				writer.Write(".0");
			}
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x0005389B File Offset: 0x00051A9B
		internal static void WriteValue(TextWriter writer, Guid value)
		{
			JsonValueUtils.WriteQuoted(writer, value.ToString());
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x000538B0 File Offset: 0x00051AB0
		internal static void WriteValue(TextWriter writer, decimal value)
		{
			JsonValueUtils.WriteQuoted(writer, value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x000538C4 File Offset: 0x00051AC4
		internal static void WriteValue(TextWriter writer, DateTime value, ODataJsonDateTimeFormat dateTimeFormat)
		{
			switch (dateTimeFormat)
			{
			case ODataJsonDateTimeFormat.ODataDateTime:
			{
				value = JsonValueUtils.GetUniversalDate(value);
				string text = string.Format(CultureInfo.InvariantCulture, "\\/Date({0})\\/", new object[] { JsonValueUtils.DateTimeTicksToJsonTicks(value.Ticks) });
				JsonValueUtils.WriteQuoted(writer, text);
				return;
			}
			case ODataJsonDateTimeFormat.ISO8601DateTime:
			{
				string text2 = PlatformHelper.ConvertDateTimeToString(value);
				JsonValueUtils.WriteQuoted(writer, text2);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x0005392C File Offset: 0x00051B2C
		internal static void WriteValue(TextWriter writer, DateTimeOffset value, ODataJsonDateTimeFormat dateTimeFormat)
		{
			int num = (int)value.Offset.TotalMinutes;
			switch (dateTimeFormat)
			{
			case ODataJsonDateTimeFormat.ODataDateTime:
			{
				string text = string.Format(CultureInfo.InvariantCulture, "\\/Date({0}{1}{2:D4})\\/", new object[]
				{
					JsonValueUtils.DateTimeTicksToJsonTicks(value.Ticks),
					(num >= 0) ? "+" : string.Empty,
					num
				});
				JsonValueUtils.WriteQuoted(writer, text);
				return;
			}
			case ODataJsonDateTimeFormat.ISO8601DateTime:
			{
				string text2 = XmlConvert.ToString(value);
				JsonValueUtils.WriteQuoted(writer, text2);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x000539C0 File Offset: 0x00051BC0
		internal static void WriteValue(TextWriter writer, TimeSpan value)
		{
			JsonValueUtils.WriteQuoted(writer, XmlConvert.ToString(value));
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x000539CE File Offset: 0x00051BCE
		internal static void WriteValue(TextWriter writer, byte value)
		{
			writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x000539E2 File Offset: 0x00051BE2
		internal static void WriteValue(TextWriter writer, sbyte value)
		{
			writer.Write(value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x000539F6 File Offset: 0x00051BF6
		internal static void WriteValue(TextWriter writer, string value)
		{
			if (value == null)
			{
				writer.Write("null");
				return;
			}
			JsonValueUtils.WriteEscapedJsonString(writer, value);
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x00053A10 File Offset: 0x00051C10
		internal static void WriteEscapedJsonString(TextWriter writer, string inputString)
		{
			writer.Write('"');
			int num = 0;
			int length = inputString.Length;
			int i = 0;
			int num2;
			while (i < length)
			{
				char c = inputString[i];
				char c2 = c;
				string text;
				switch (c2)
				{
				case '\b':
					text = "\\b";
					goto IL_A9;
				case '\t':
					text = "\\t";
					goto IL_A9;
				case '\n':
					text = "\\n";
					goto IL_A9;
				case '\v':
					goto IL_93;
				case '\f':
					text = "\\f";
					goto IL_A9;
				case '\r':
					text = "\\r";
					goto IL_A9;
				default:
					if (c2 == '"')
					{
						text = "\\\"";
						goto IL_A9;
					}
					if (c2 != '\\')
					{
						goto IL_93;
					}
					text = "\\\\";
					goto IL_A9;
				}
				IL_CB:
				i++;
				continue;
				IL_93:
				if (c >= ' ' && c <= '\u007f')
				{
					goto IL_CB;
				}
				text = JsonValueUtils.SpecialCharToEscapedStringMap[(int)c];
				IL_A9:
				num2 = i - num;
				if (num2 > 0)
				{
					writer.Write(inputString.Substring(num, num2));
				}
				writer.Write(text);
				num = i + 1;
				goto IL_CB;
			}
			num2 = length - num;
			if (num2 > 0)
			{
				writer.Write(inputString.Substring(num, num2));
			}
			writer.Write('"');
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x00053B11 File Offset: 0x00051D11
		internal static long JsonTicksToDateTimeTicks(long ticks)
		{
			return ticks * 10000L + JsonValueUtils.JsonDateTimeMinTimeTicks;
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x00053B21 File Offset: 0x00051D21
		private static void WriteQuoted(TextWriter writer, string text)
		{
			writer.Write('"');
			writer.Write(text);
			writer.Write('"');
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x00053B3A File Offset: 0x00051D3A
		private static long DateTimeTicksToJsonTicks(long ticks)
		{
			return (ticks - JsonValueUtils.JsonDateTimeMinTimeTicks) / 10000L;
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x00053B4C File Offset: 0x00051D4C
		private static DateTime GetUniversalDate(DateTime value)
		{
			switch (value.Kind)
			{
			case DateTimeKind.Unspecified:
				value = new DateTime(value.Ticks, DateTimeKind.Utc);
				break;
			case DateTimeKind.Local:
				value = value.ToUniversalTime();
				break;
			}
			return value;
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x00053B90 File Offset: 0x00051D90
		private static string[] CreateSpecialCharToEscapedStringMap()
		{
			string[] array = new string[65536];
			for (int i = 0; i <= 65535; i++)
			{
				array[i] = string.Format(CultureInfo.InvariantCulture, "\\u{0:x4}", new object[] { i });
			}
			return array;
		}

		// Token: 0x0400097E RID: 2430
		private static readonly long JsonDateTimeMinTimeTicks = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;

		// Token: 0x0400097F RID: 2431
		private static readonly char[] DoubleIndicatingCharacters = new char[] { '.', 'e', 'E' };

		// Token: 0x04000980 RID: 2432
		private static readonly string[] SpecialCharToEscapedStringMap = JsonValueUtils.CreateSpecialCharToEscapedStringMap();
	}
}
