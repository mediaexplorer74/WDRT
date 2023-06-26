using System;

namespace System.Globalization
{
	/// <summary>Represents time in divisions, such as months, days, and years. Years are calculated as for the Japanese calendar, while days and months are calculated using the lunisolar calendar.</summary>
	// Token: 0x020003C4 RID: 964
	[Serializable]
	public class JapaneseLunisolarCalendar : EastAsianLunisolarCalendar
	{
		/// <summary>Gets the minimum date and time supported by the <see cref="T:System.Globalization.JapaneseLunisolarCalendar" /> class.</summary>
		/// <returns>The earliest date and time supported by the <see cref="T:System.Globalization.JapaneseLunisolarCalendar" /> class, which is equivalent to the first moment of January 28, 1960 C.E. in the Gregorian calendar.</returns>
		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06003066 RID: 12390 RVA: 0x000BB261 File Offset: 0x000B9461
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return JapaneseLunisolarCalendar.minDate;
			}
		}

		/// <summary>Gets the maximum date and time supported by the <see cref="T:System.Globalization.JapaneseLunisolarCalendar" /> class.</summary>
		/// <returns>The latest date and time supported by the <see cref="T:System.Globalization.JapaneseLunisolarCalendar" /> class, which is equivalent to the last moment of January 22, 2050 C.E. in the Gregorian calendar.</returns>
		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06003067 RID: 12391 RVA: 0x000BB268 File Offset: 0x000B9468
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return JapaneseLunisolarCalendar.maxDate;
			}
		}

		/// <summary>Gets the number of days in the year that precedes the year that is specified by the <see cref="P:System.Globalization.JapaneseLunisolarCalendar.MinSupportedDateTime" /> property.</summary>
		/// <returns>The number of days in the year that precedes the year specified by <see cref="P:System.Globalization.JapaneseLunisolarCalendar.MinSupportedDateTime" />.</returns>
		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06003068 RID: 12392 RVA: 0x000BB26F File Offset: 0x000B946F
		protected override int DaysInYearBeforeMinSupportedYear
		{
			get
			{
				return 354;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06003069 RID: 12393 RVA: 0x000BB276 File Offset: 0x000B9476
		internal override int MinCalendarYear
		{
			get
			{
				return 1960;
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x0600306A RID: 12394 RVA: 0x000BB27D File Offset: 0x000B947D
		internal override int MaxCalendarYear
		{
			get
			{
				return 2049;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x0600306B RID: 12395 RVA: 0x000BB284 File Offset: 0x000B9484
		internal override DateTime MinDate
		{
			get
			{
				return JapaneseLunisolarCalendar.minDate;
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x0600306C RID: 12396 RVA: 0x000BB28B File Offset: 0x000B948B
		internal override DateTime MaxDate
		{
			get
			{
				return JapaneseLunisolarCalendar.maxDate;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x0600306D RID: 12397 RVA: 0x000BB292 File Offset: 0x000B9492
		internal override EraInfo[] CalEraInfo
		{
			get
			{
				return JapaneseCalendar.GetEraInfo();
			}
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x000BB29C File Offset: 0x000B949C
		internal override int GetYearInfo(int LunarYear, int Index)
		{
			if (LunarYear < 1960 || LunarYear > 2049)
			{
				throw new ArgumentOutOfRangeException("year", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Range"), 1960, 2049));
			}
			return JapaneseLunisolarCalendar.yinfo[LunarYear - 1960, Index];
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x000BB2FE File Offset: 0x000B94FE
		internal override int GetYear(int year, DateTime time)
		{
			return this.helper.GetYear(year, time);
		}

		// Token: 0x06003070 RID: 12400 RVA: 0x000BB30D File Offset: 0x000B950D
		internal override int GetGregorianYear(int year, int era)
		{
			return this.helper.GetGregorianYear(year, era);
		}

		// Token: 0x06003071 RID: 12401 RVA: 0x000BB31C File Offset: 0x000B951C
		private static EraInfo[] TrimEras(EraInfo[] baseEras)
		{
			EraInfo[] array = new EraInfo[baseEras.Length];
			int num = 0;
			for (int i = 0; i < baseEras.Length; i++)
			{
				if (baseEras[i].yearOffset + baseEras[i].minEraYear < 2049)
				{
					if (baseEras[i].yearOffset + baseEras[i].maxEraYear < 1960)
					{
						break;
					}
					array[num] = baseEras[i];
					num++;
				}
			}
			if (num == 0)
			{
				return baseEras;
			}
			Array.Resize<EraInfo>(ref array, num);
			return array;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.JapaneseLunisolarCalendar" /> class.</summary>
		// Token: 0x06003072 RID: 12402 RVA: 0x000BB38A File Offset: 0x000B958A
		public JapaneseLunisolarCalendar()
		{
			this.helper = new GregorianCalendarHelper(this, JapaneseLunisolarCalendar.TrimEras(JapaneseCalendar.GetEraInfo()));
		}

		/// <summary>Retrieves the era that corresponds to the specified <see cref="T:System.DateTime" />.</summary>
		/// <param name="time">The <see cref="T:System.DateTime" /> to read.</param>
		/// <returns>An integer that represents the era specified in the <paramref name="time" /> parameter.</returns>
		// Token: 0x06003073 RID: 12403 RVA: 0x000BB3A8 File Offset: 0x000B95A8
		public override int GetEra(DateTime time)
		{
			return this.helper.GetEra(time);
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06003074 RID: 12404 RVA: 0x000BB3B6 File Offset: 0x000B95B6
		internal override int BaseCalendarID
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06003075 RID: 12405 RVA: 0x000BB3B9 File Offset: 0x000B95B9
		internal override int ID
		{
			get
			{
				return 14;
			}
		}

		/// <summary>Gets the eras that are relevant to the <see cref="T:System.Globalization.JapaneseLunisolarCalendar" /> object.</summary>
		/// <returns>An array of 32-bit signed integers that specify the relevant eras.</returns>
		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06003076 RID: 12406 RVA: 0x000BB3BD File Offset: 0x000B95BD
		public override int[] Eras
		{
			get
			{
				return this.helper.Eras;
			}
		}

		/// <summary>Specifies the current era.</summary>
		// Token: 0x040014A6 RID: 5286
		public const int JapaneseEra = 1;

		// Token: 0x040014A7 RID: 5287
		internal GregorianCalendarHelper helper;

		// Token: 0x040014A8 RID: 5288
		internal const int MIN_LUNISOLAR_YEAR = 1960;

		// Token: 0x040014A9 RID: 5289
		internal const int MAX_LUNISOLAR_YEAR = 2049;

		// Token: 0x040014AA RID: 5290
		internal const int MIN_GREGORIAN_YEAR = 1960;

		// Token: 0x040014AB RID: 5291
		internal const int MIN_GREGORIAN_MONTH = 1;

		// Token: 0x040014AC RID: 5292
		internal const int MIN_GREGORIAN_DAY = 28;

		// Token: 0x040014AD RID: 5293
		internal const int MAX_GREGORIAN_YEAR = 2050;

		// Token: 0x040014AE RID: 5294
		internal const int MAX_GREGORIAN_MONTH = 1;

		// Token: 0x040014AF RID: 5295
		internal const int MAX_GREGORIAN_DAY = 22;

		// Token: 0x040014B0 RID: 5296
		internal static DateTime minDate = new DateTime(1960, 1, 28);

		// Token: 0x040014B1 RID: 5297
		internal static DateTime maxDate = new DateTime(new DateTime(2050, 1, 22, 23, 59, 59, 999).Ticks + 9999L);

		// Token: 0x040014B2 RID: 5298
		private static readonly int[,] yinfo = new int[,]
		{
			{ 6, 1, 28, 44368 },
			{ 0, 2, 15, 43856 },
			{ 0, 2, 5, 19808 },
			{ 4, 1, 25, 42352 },
			{ 0, 2, 13, 42352 },
			{ 0, 2, 2, 21104 },
			{ 3, 1, 22, 26928 },
			{ 0, 2, 9, 55632 },
			{ 7, 1, 30, 27304 },
			{ 0, 2, 17, 22176 },
			{ 0, 2, 6, 39632 },
			{ 5, 1, 27, 19176 },
			{ 0, 2, 15, 19168 },
			{ 0, 2, 3, 42208 },
			{ 4, 1, 23, 53864 },
			{ 0, 2, 11, 53840 },
			{ 8, 1, 31, 54600 },
			{ 0, 2, 18, 46400 },
			{ 0, 2, 7, 54944 },
			{ 6, 1, 28, 38608 },
			{ 0, 2, 16, 38320 },
			{ 0, 2, 5, 18864 },
			{ 4, 1, 25, 42200 },
			{ 0, 2, 13, 42160 },
			{ 10, 2, 2, 45656 },
			{ 0, 2, 20, 27216 },
			{ 0, 2, 9, 27968 },
			{ 6, 1, 29, 46504 },
			{ 0, 2, 18, 11104 },
			{ 0, 2, 6, 38320 },
			{ 5, 1, 27, 18872 },
			{ 0, 2, 15, 18800 },
			{ 0, 2, 4, 25776 },
			{ 3, 1, 23, 27216 },
			{ 0, 2, 10, 59984 },
			{ 8, 1, 31, 27976 },
			{ 0, 2, 19, 23248 },
			{ 0, 2, 8, 11104 },
			{ 5, 1, 28, 37744 },
			{ 0, 2, 16, 37600 },
			{ 0, 2, 5, 51552 },
			{ 4, 1, 24, 58536 },
			{ 0, 2, 12, 54432 },
			{ 0, 2, 1, 55888 },
			{ 2, 1, 22, 23208 },
			{ 0, 2, 9, 22208 },
			{ 7, 1, 29, 43736 },
			{ 0, 2, 18, 9680 },
			{ 0, 2, 7, 37584 },
			{ 5, 1, 26, 51544 },
			{ 0, 2, 14, 43344 },
			{ 0, 2, 3, 46240 },
			{ 3, 1, 23, 47696 },
			{ 0, 2, 10, 46416 },
			{ 9, 1, 31, 21928 },
			{ 0, 2, 19, 19360 },
			{ 0, 2, 8, 42416 },
			{ 5, 1, 28, 21176 },
			{ 0, 2, 16, 21168 },
			{ 0, 2, 5, 43344 },
			{ 4, 1, 25, 46248 },
			{ 0, 2, 12, 27296 },
			{ 0, 2, 1, 44368 },
			{ 2, 1, 22, 21928 },
			{ 0, 2, 10, 19296 },
			{ 6, 1, 29, 42352 },
			{ 0, 2, 17, 42352 },
			{ 0, 2, 7, 21104 },
			{ 5, 1, 27, 26928 },
			{ 0, 2, 13, 55600 },
			{ 0, 2, 3, 23200 },
			{ 3, 1, 23, 43856 },
			{ 0, 2, 11, 38608 },
			{ 11, 1, 31, 19176 },
			{ 0, 2, 19, 19168 },
			{ 0, 2, 8, 42192 },
			{ 6, 1, 28, 53864 },
			{ 0, 2, 15, 53840 },
			{ 0, 2, 4, 54560 },
			{ 5, 1, 24, 55968 },
			{ 0, 2, 12, 46752 },
			{ 0, 2, 1, 38608 },
			{ 2, 1, 22, 19160 },
			{ 0, 2, 10, 18864 },
			{ 7, 1, 30, 42168 },
			{ 0, 2, 17, 42160 },
			{ 0, 2, 6, 45648 },
			{ 5, 1, 26, 46376 },
			{ 0, 2, 14, 27968 },
			{ 0, 2, 2, 44448 }
		};
	}
}
