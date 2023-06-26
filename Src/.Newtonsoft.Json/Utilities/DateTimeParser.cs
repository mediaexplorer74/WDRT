using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200004B RID: 75
	[NullableContext(1)]
	[Nullable(0)]
	internal struct DateTimeParser
	{
		// Token: 0x0600047D RID: 1149 RVA: 0x000122B6 File Offset: 0x000104B6
		public bool Parse(char[] text, int startIndex, int length)
		{
			this._text = text;
			this._end = startIndex + length;
			return this.ParseDate(startIndex) && this.ParseChar(DateTimeParser.Lzyyyy_MM_dd + startIndex, 'T') && this.ParseTimeAndZoneAndWhitespace(DateTimeParser.Lzyyyy_MM_ddT + startIndex);
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x000122F4 File Offset: 0x000104F4
		private bool ParseDate(int start)
		{
			return this.Parse4Digit(start, out this.Year) && 1 <= this.Year && this.ParseChar(start + DateTimeParser.Lzyyyy, '-') && this.Parse2Digit(start + DateTimeParser.Lzyyyy_, out this.Month) && 1 <= this.Month && this.Month <= 12 && this.ParseChar(start + DateTimeParser.Lzyyyy_MM, '-') && this.Parse2Digit(start + DateTimeParser.Lzyyyy_MM_, out this.Day) && 1 <= this.Day && this.Day <= DateTime.DaysInMonth(this.Year, this.Month);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x000123A5 File Offset: 0x000105A5
		private bool ParseTimeAndZoneAndWhitespace(int start)
		{
			return this.ParseTime(ref start) && this.ParseZone(start);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x000123BC File Offset: 0x000105BC
		private bool ParseTime(ref int start)
		{
			if (!this.Parse2Digit(start, out this.Hour) || this.Hour > 24 || !this.ParseChar(start + DateTimeParser.LzHH, ':') || !this.Parse2Digit(start + DateTimeParser.LzHH_, out this.Minute) || this.Minute >= 60 || !this.ParseChar(start + DateTimeParser.LzHH_mm, ':') || !this.Parse2Digit(start + DateTimeParser.LzHH_mm_, out this.Second) || this.Second >= 60 || (this.Hour == 24 && (this.Minute != 0 || this.Second != 0)))
			{
				return false;
			}
			start += DateTimeParser.LzHH_mm_ss;
			if (this.ParseChar(start, '.'))
			{
				this.Fraction = 0;
				int num = 0;
				for (;;)
				{
					int num2 = start + 1;
					start = num2;
					if (num2 >= this._end || num >= 7)
					{
						break;
					}
					int num3 = (int)(this._text[start] - '0');
					if (num3 < 0 || num3 > 9)
					{
						break;
					}
					this.Fraction = this.Fraction * 10 + num3;
					num++;
				}
				if (num < 7)
				{
					if (num == 0)
					{
						return false;
					}
					this.Fraction *= DateTimeParser.Power10[7 - num];
				}
				if (this.Hour == 24 && this.Fraction != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x000124FC File Offset: 0x000106FC
		private bool ParseZone(int start)
		{
			if (start < this._end)
			{
				char c = this._text[start];
				if (c == 'Z' || c == 'z')
				{
					this.Zone = ParserTimeZone.Utc;
					start++;
				}
				else
				{
					if (start + 2 < this._end && this.Parse2Digit(start + DateTimeParser.Lz_, out this.ZoneHour) && this.ZoneHour <= 99)
					{
						if (c != '+')
						{
							if (c == '-')
							{
								this.Zone = ParserTimeZone.LocalWestOfUtc;
								start += DateTimeParser.Lz_zz;
							}
						}
						else
						{
							this.Zone = ParserTimeZone.LocalEastOfUtc;
							start += DateTimeParser.Lz_zz;
						}
					}
					if (start < this._end)
					{
						if (this.ParseChar(start, ':'))
						{
							start++;
							if (start + 1 < this._end && this.Parse2Digit(start, out this.ZoneMinute) && this.ZoneMinute <= 99)
							{
								start += 2;
							}
						}
						else if (start + 1 < this._end && this.Parse2Digit(start, out this.ZoneMinute) && this.ZoneMinute <= 99)
						{
							start += 2;
						}
					}
				}
			}
			return start == this._end;
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00012608 File Offset: 0x00010808
		private bool Parse4Digit(int start, out int num)
		{
			if (start + 3 < this._end)
			{
				int num2 = (int)(this._text[start] - '0');
				int num3 = (int)(this._text[start + 1] - '0');
				int num4 = (int)(this._text[start + 2] - '0');
				int num5 = (int)(this._text[start + 3] - '0');
				if (0 <= num2 && num2 < 10 && 0 <= num3 && num3 < 10 && 0 <= num4 && num4 < 10 && 0 <= num5 && num5 < 10)
				{
					num = ((num2 * 10 + num3) * 10 + num4) * 10 + num5;
					return true;
				}
			}
			num = 0;
			return false;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00012694 File Offset: 0x00010894
		private bool Parse2Digit(int start, out int num)
		{
			if (start + 1 < this._end)
			{
				int num2 = (int)(this._text[start] - '0');
				int num3 = (int)(this._text[start + 1] - '0');
				if (0 <= num2 && num2 < 10 && 0 <= num3 && num3 < 10)
				{
					num = num2 * 10 + num3;
					return true;
				}
			}
			num = 0;
			return false;
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x000126E6 File Offset: 0x000108E6
		private bool ParseChar(int start, char ch)
		{
			return start < this._end && this._text[start] == ch;
		}

		// Token: 0x04000195 RID: 405
		public int Year;

		// Token: 0x04000196 RID: 406
		public int Month;

		// Token: 0x04000197 RID: 407
		public int Day;

		// Token: 0x04000198 RID: 408
		public int Hour;

		// Token: 0x04000199 RID: 409
		public int Minute;

		// Token: 0x0400019A RID: 410
		public int Second;

		// Token: 0x0400019B RID: 411
		public int Fraction;

		// Token: 0x0400019C RID: 412
		public int ZoneHour;

		// Token: 0x0400019D RID: 413
		public int ZoneMinute;

		// Token: 0x0400019E RID: 414
		public ParserTimeZone Zone;

		// Token: 0x0400019F RID: 415
		private char[] _text;

		// Token: 0x040001A0 RID: 416
		private int _end;

		// Token: 0x040001A1 RID: 417
		private static readonly int[] Power10 = new int[] { -1, 10, 100, 1000, 10000, 100000, 1000000 };

		// Token: 0x040001A2 RID: 418
		private static readonly int Lzyyyy = "yyyy".Length;

		// Token: 0x040001A3 RID: 419
		private static readonly int Lzyyyy_ = "yyyy-".Length;

		// Token: 0x040001A4 RID: 420
		private static readonly int Lzyyyy_MM = "yyyy-MM".Length;

		// Token: 0x040001A5 RID: 421
		private static readonly int Lzyyyy_MM_ = "yyyy-MM-".Length;

		// Token: 0x040001A6 RID: 422
		private static readonly int Lzyyyy_MM_dd = "yyyy-MM-dd".Length;

		// Token: 0x040001A7 RID: 423
		private static readonly int Lzyyyy_MM_ddT = "yyyy-MM-ddT".Length;

		// Token: 0x040001A8 RID: 424
		private static readonly int LzHH = "HH".Length;

		// Token: 0x040001A9 RID: 425
		private static readonly int LzHH_ = "HH:".Length;

		// Token: 0x040001AA RID: 426
		private static readonly int LzHH_mm = "HH:mm".Length;

		// Token: 0x040001AB RID: 427
		private static readonly int LzHH_mm_ = "HH:mm:".Length;

		// Token: 0x040001AC RID: 428
		private static readonly int LzHH_mm_ss = "HH:mm:ss".Length;

		// Token: 0x040001AD RID: 429
		private static readonly int Lz_ = "-".Length;

		// Token: 0x040001AE RID: 430
		private static readonly int Lz_zz = "-zz".Length;

		// Token: 0x040001AF RID: 431
		private const short MaxFractionDigits = 7;
	}
}
