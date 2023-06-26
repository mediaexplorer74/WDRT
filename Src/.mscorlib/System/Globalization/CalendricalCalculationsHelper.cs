﻿using System;

namespace System.Globalization
{
	// Token: 0x020003C8 RID: 968
	internal class CalendricalCalculationsHelper
	{
		// Token: 0x060030CC RID: 12492 RVA: 0x000BC33E File Offset: 0x000BA53E
		private static double RadiansFromDegrees(double degree)
		{
			return degree * 3.1415926535897931 / 180.0;
		}

		// Token: 0x060030CD RID: 12493 RVA: 0x000BC355 File Offset: 0x000BA555
		private static double SinOfDegree(double degree)
		{
			return Math.Sin(CalendricalCalculationsHelper.RadiansFromDegrees(degree));
		}

		// Token: 0x060030CE RID: 12494 RVA: 0x000BC362 File Offset: 0x000BA562
		private static double CosOfDegree(double degree)
		{
			return Math.Cos(CalendricalCalculationsHelper.RadiansFromDegrees(degree));
		}

		// Token: 0x060030CF RID: 12495 RVA: 0x000BC36F File Offset: 0x000BA56F
		private static double TanOfDegree(double degree)
		{
			return Math.Tan(CalendricalCalculationsHelper.RadiansFromDegrees(degree));
		}

		// Token: 0x060030D0 RID: 12496 RVA: 0x000BC37C File Offset: 0x000BA57C
		public static double Angle(int degrees, int minutes, double seconds)
		{
			return (seconds / 60.0 + (double)minutes) / 60.0 + (double)degrees;
		}

		// Token: 0x060030D1 RID: 12497 RVA: 0x000BC399 File Offset: 0x000BA599
		private static double Obliquity(double julianCenturies)
		{
			return CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.Coefficients, julianCenturies);
		}

		// Token: 0x060030D2 RID: 12498 RVA: 0x000BC3A6 File Offset: 0x000BA5A6
		internal static long GetNumberOfDays(DateTime date)
		{
			return date.Ticks / 864000000000L;
		}

		// Token: 0x060030D3 RID: 12499 RVA: 0x000BC3BC File Offset: 0x000BA5BC
		private static int GetGregorianYear(double numberOfDays)
		{
			return new DateTime(Math.Min((long)(Math.Floor(numberOfDays) * 864000000000.0), DateTime.MaxValue.Ticks)).Year;
		}

		// Token: 0x060030D4 RID: 12500 RVA: 0x000BC3FC File Offset: 0x000BA5FC
		private static double Reminder(double divisor, double dividend)
		{
			double num = Math.Floor(divisor / dividend);
			return divisor - dividend * num;
		}

		// Token: 0x060030D5 RID: 12501 RVA: 0x000BC417 File Offset: 0x000BA617
		private static double NormalizeLongitude(double longitude)
		{
			longitude = CalendricalCalculationsHelper.Reminder(longitude, 360.0);
			if (longitude < 0.0)
			{
				longitude += 360.0;
			}
			return longitude;
		}

		// Token: 0x060030D6 RID: 12502 RVA: 0x000BC444 File Offset: 0x000BA644
		public static double AsDayFraction(double longitude)
		{
			return longitude / 360.0;
		}

		// Token: 0x060030D7 RID: 12503 RVA: 0x000BC454 File Offset: 0x000BA654
		private static double PolynomialSum(double[] coefficients, double indeterminate)
		{
			double num = coefficients[0];
			double num2 = 1.0;
			for (int i = 1; i < coefficients.Length; i++)
			{
				num2 *= indeterminate;
				num += coefficients[i] * num2;
			}
			return num;
		}

		// Token: 0x060030D8 RID: 12504 RVA: 0x000BC48C File Offset: 0x000BA68C
		private static double CenturiesFrom1900(int gregorianYear)
		{
			long numberOfDays = CalendricalCalculationsHelper.GetNumberOfDays(new DateTime(gregorianYear, 7, 1));
			return (double)(numberOfDays - CalendricalCalculationsHelper.StartOf1900Century) / 36525.0;
		}

		// Token: 0x060030D9 RID: 12505 RVA: 0x000BC4BC File Offset: 0x000BA6BC
		private static double DefaultEphemerisCorrection(int gregorianYear)
		{
			long numberOfDays = CalendricalCalculationsHelper.GetNumberOfDays(new DateTime(gregorianYear, 1, 1));
			double num = (double)(numberOfDays - CalendricalCalculationsHelper.StartOf1810);
			double num2 = 0.5 + num;
			return (Math.Pow(num2, 2.0) / 41048480.0 - 15.0) / 86400.0;
		}

		// Token: 0x060030DA RID: 12506 RVA: 0x000BC519 File Offset: 0x000BA719
		private static double EphemerisCorrection1988to2019(int gregorianYear)
		{
			return (double)(gregorianYear - 1933) / 86400.0;
		}

		// Token: 0x060030DB RID: 12507 RVA: 0x000BC530 File Offset: 0x000BA730
		private static double EphemerisCorrection1900to1987(int gregorianYear)
		{
			double num = CalendricalCalculationsHelper.CenturiesFrom1900(gregorianYear);
			return CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.Coefficients1900to1987, num);
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x000BC550 File Offset: 0x000BA750
		private static double EphemerisCorrection1800to1899(int gregorianYear)
		{
			double num = CalendricalCalculationsHelper.CenturiesFrom1900(gregorianYear);
			return CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.Coefficients1800to1899, num);
		}

		// Token: 0x060030DD RID: 12509 RVA: 0x000BC570 File Offset: 0x000BA770
		private static double EphemerisCorrection1700to1799(int gregorianYear)
		{
			double num = (double)(gregorianYear - 1700);
			return CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.Coefficients1700to1799, num) / 86400.0;
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x000BC59C File Offset: 0x000BA79C
		private static double EphemerisCorrection1620to1699(int gregorianYear)
		{
			double num = (double)(gregorianYear - 1600);
			return CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.Coefficients1620to1699, num) / 86400.0;
		}

		// Token: 0x060030DF RID: 12511 RVA: 0x000BC5C8 File Offset: 0x000BA7C8
		private static double EphemerisCorrection(double time)
		{
			int gregorianYear = CalendricalCalculationsHelper.GetGregorianYear(time);
			CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap[] ephemerisCorrectionTable = CalendricalCalculationsHelper.EphemerisCorrectionTable;
			int i = 0;
			while (i < ephemerisCorrectionTable.Length)
			{
				CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap ephemerisCorrectionAlgorithmMap = ephemerisCorrectionTable[i];
				if (ephemerisCorrectionAlgorithmMap._lowestYear <= gregorianYear)
				{
					switch (ephemerisCorrectionAlgorithmMap._algorithm)
					{
					case CalendricalCalculationsHelper.CorrectionAlgorithm.Default:
						return CalendricalCalculationsHelper.DefaultEphemerisCorrection(gregorianYear);
					case CalendricalCalculationsHelper.CorrectionAlgorithm.Year1988to2019:
						return CalendricalCalculationsHelper.EphemerisCorrection1988to2019(gregorianYear);
					case CalendricalCalculationsHelper.CorrectionAlgorithm.Year1900to1987:
						return CalendricalCalculationsHelper.EphemerisCorrection1900to1987(gregorianYear);
					case CalendricalCalculationsHelper.CorrectionAlgorithm.Year1800to1899:
						return CalendricalCalculationsHelper.EphemerisCorrection1800to1899(gregorianYear);
					case CalendricalCalculationsHelper.CorrectionAlgorithm.Year1700to1799:
						return CalendricalCalculationsHelper.EphemerisCorrection1700to1799(gregorianYear);
					case CalendricalCalculationsHelper.CorrectionAlgorithm.Year1620to1699:
						return CalendricalCalculationsHelper.EphemerisCorrection1620to1699(gregorianYear);
					default:
						goto IL_7F;
					}
				}
				else
				{
					i++;
				}
			}
			IL_7F:
			return CalendricalCalculationsHelper.DefaultEphemerisCorrection(gregorianYear);
		}

		// Token: 0x060030E0 RID: 12512 RVA: 0x000BC65C File Offset: 0x000BA85C
		public static double JulianCenturies(double moment)
		{
			double num = moment + CalendricalCalculationsHelper.EphemerisCorrection(moment);
			return (num - 730120.5) / 36525.0;
		}

		// Token: 0x060030E1 RID: 12513 RVA: 0x000BC687 File Offset: 0x000BA887
		private static bool IsNegative(double value)
		{
			return Math.Sign(value) == -1;
		}

		// Token: 0x060030E2 RID: 12514 RVA: 0x000BC692 File Offset: 0x000BA892
		private static double CopySign(double value, double sign)
		{
			if (CalendricalCalculationsHelper.IsNegative(value) != CalendricalCalculationsHelper.IsNegative(sign))
			{
				return -value;
			}
			return value;
		}

		// Token: 0x060030E3 RID: 12515 RVA: 0x000BC6A8 File Offset: 0x000BA8A8
		private static double EquationOfTime(double time)
		{
			double num = CalendricalCalculationsHelper.JulianCenturies(time);
			double num2 = CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.LambdaCoefficients, num);
			double num3 = CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.AnomalyCoefficients, num);
			double num4 = CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.EccentricityCoefficients, num);
			double num5 = CalendricalCalculationsHelper.Obliquity(num);
			double num6 = CalendricalCalculationsHelper.TanOfDegree(num5 / 2.0);
			double num7 = num6 * num6;
			double num8 = num7 * CalendricalCalculationsHelper.SinOfDegree(2.0 * num2) - 2.0 * num4 * CalendricalCalculationsHelper.SinOfDegree(num3) + 4.0 * num4 * num7 * CalendricalCalculationsHelper.SinOfDegree(num3) * CalendricalCalculationsHelper.CosOfDegree(2.0 * num2) - 0.5 * Math.Pow(num7, 2.0) * CalendricalCalculationsHelper.SinOfDegree(4.0 * num2) - 1.25 * Math.Pow(num4, 2.0) * CalendricalCalculationsHelper.SinOfDegree(2.0 * num3);
			double num9 = 6.2831853071795862;
			double num10 = num8 / num9;
			return CalendricalCalculationsHelper.CopySign(Math.Min(Math.Abs(num10), 0.5), num10);
		}

		// Token: 0x060030E4 RID: 12516 RVA: 0x000BC7D8 File Offset: 0x000BA9D8
		private static double AsLocalTime(double apparentMidday, double longitude)
		{
			double num = apparentMidday - CalendricalCalculationsHelper.AsDayFraction(longitude);
			return apparentMidday - CalendricalCalculationsHelper.EquationOfTime(num);
		}

		// Token: 0x060030E5 RID: 12517 RVA: 0x000BC7F6 File Offset: 0x000BA9F6
		public static double Midday(double date, double longitude)
		{
			return CalendricalCalculationsHelper.AsLocalTime(date + 0.5, longitude) - CalendricalCalculationsHelper.AsDayFraction(longitude);
		}

		// Token: 0x060030E6 RID: 12518 RVA: 0x000BC810 File Offset: 0x000BAA10
		private static double InitLongitude(double longitude)
		{
			return CalendricalCalculationsHelper.NormalizeLongitude(longitude + 180.0) - 180.0;
		}

		// Token: 0x060030E7 RID: 12519 RVA: 0x000BC82C File Offset: 0x000BAA2C
		public static double MiddayAtPersianObservationSite(double date)
		{
			return CalendricalCalculationsHelper.Midday(date, CalendricalCalculationsHelper.InitLongitude(52.5));
		}

		// Token: 0x060030E8 RID: 12520 RVA: 0x000BC842 File Offset: 0x000BAA42
		private static double PeriodicTerm(double julianCenturies, int x, double y, double z)
		{
			return (double)x * CalendricalCalculationsHelper.SinOfDegree(y + z * julianCenturies);
		}

		// Token: 0x060030E9 RID: 12521 RVA: 0x000BC854 File Offset: 0x000BAA54
		private static double SumLongSequenceOfPeriodicTerms(double julianCenturies)
		{
			double num = 0.0;
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 403406, 270.54861, 0.9287892);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 195207, 340.19128, 35999.1376958);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 119433, 63.91854, 35999.4089666);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 112392, 331.2622, 35998.7287385);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 3891, 317.843, 71998.20261);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 2819, 86.631, 71998.4403);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 1721, 240.052, 36000.35726);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 660, 310.26, 71997.4812);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 350, 247.23, 32964.4678);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 334, 260.87, -19.441);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 314, 297.82, 445267.1117);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 268, 343.14, 45036.884);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 242, 166.79, 3.1008);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 234, 81.53, 22518.4434);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 158, 3.5, -19.9739);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 132, 132.75, 65928.9345);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 129, 182.95, 9038.0293);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 114, 162.03, 3034.7684);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 99, 29.8, 33718.148);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 93, 266.4, 3034.448);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 86, 249.2, -2280.773);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 78, 157.6, 29929.992);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 72, 257.8, 31556.493);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 68, 185.1, 149.588);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 64, 69.9, 9037.75);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 46, 8.0, 107997.405);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 38, 197.1, -4444.176);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 37, 250.4, 151.771);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 32, 65.3, 67555.316);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 29, 162.7, 31556.08);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 28, 341.5, -4561.54);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 27, 291.6, 107996.706);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 27, 98.5, 1221.655);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 25, 146.7, 62894.167);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 24, 110.0, 31437.369);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 21, 5.2, 14578.298);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 21, 342.6, -31931.757);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 20, 230.9, 34777.243);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 18, 256.1, 1221.999);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 17, 45.3, 62894.511);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 14, 242.9, -4442.039);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 13, 115.2, 107997.909);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 13, 151.8, 119.066);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 13, 285.3, 16859.071);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 12, 53.3, -4.578);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 10, 126.6, 26895.292);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 10, 205.7, -39.127);
			num += CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 10, 85.9, 12297.536);
			return num + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 10, 146.1, 90073.778);
		}

		// Token: 0x060030EA RID: 12522 RVA: 0x000BCE2C File Offset: 0x000BB02C
		private static double Aberration(double julianCenturies)
		{
			return 9.74E-05 * CalendricalCalculationsHelper.CosOfDegree(177.63 + 35999.01848 * julianCenturies) - 0.005575;
		}

		// Token: 0x060030EB RID: 12523 RVA: 0x000BCE5C File Offset: 0x000BB05C
		private static double Nutation(double julianCenturies)
		{
			double num = CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.CoefficientsA, julianCenturies);
			double num2 = CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.CoefficientsB, julianCenturies);
			return -0.004778 * CalendricalCalculationsHelper.SinOfDegree(num) - 0.0003667 * CalendricalCalculationsHelper.SinOfDegree(num2);
		}

		// Token: 0x060030EC RID: 12524 RVA: 0x000BCEA4 File Offset: 0x000BB0A4
		public static double Compute(double time)
		{
			double num = CalendricalCalculationsHelper.JulianCenturies(time);
			double num2 = 282.7771834 + 36000.76953744 * num + 5.7295779513082322E-06 * CalendricalCalculationsHelper.SumLongSequenceOfPeriodicTerms(num);
			double num3 = num2 + CalendricalCalculationsHelper.Aberration(num) + CalendricalCalculationsHelper.Nutation(num);
			return CalendricalCalculationsHelper.InitLongitude(num3);
		}

		// Token: 0x060030ED RID: 12525 RVA: 0x000BCEF5 File Offset: 0x000BB0F5
		public static double AsSeason(double longitude)
		{
			if (longitude >= 0.0)
			{
				return longitude;
			}
			return longitude + 360.0;
		}

		// Token: 0x060030EE RID: 12526 RVA: 0x000BCF10 File Offset: 0x000BB110
		private static double EstimatePrior(double longitude, double time)
		{
			double num = time - 1.0145616361111112 * CalendricalCalculationsHelper.AsSeason(CalendricalCalculationsHelper.InitLongitude(CalendricalCalculationsHelper.Compute(time) - longitude));
			double num2 = CalendricalCalculationsHelper.InitLongitude(CalendricalCalculationsHelper.Compute(num) - longitude);
			return Math.Min(time, num - 1.0145616361111112 * num2);
		}

		// Token: 0x060030EF RID: 12527 RVA: 0x000BCF60 File Offset: 0x000BB160
		internal static long PersianNewYearOnOrBefore(long numberOfDays)
		{
			double num = (double)numberOfDays;
			double num2 = CalendricalCalculationsHelper.EstimatePrior(0.0, CalendricalCalculationsHelper.MiddayAtPersianObservationSite(num));
			long num3 = (long)Math.Floor(num2) - 1L;
			long num4 = num3 + 3L;
			long num5;
			for (num5 = num3; num5 != num4; num5 += 1L)
			{
				double num6 = CalendricalCalculationsHelper.MiddayAtPersianObservationSite((double)num5);
				double num7 = CalendricalCalculationsHelper.Compute(num6);
				if (0.0 <= num7 && num7 <= 2.0)
				{
					break;
				}
			}
			return num5 - 1L;
		}

		// Token: 0x040014D8 RID: 5336
		private const double FullCircleOfArc = 360.0;

		// Token: 0x040014D9 RID: 5337
		private const int HalfCircleOfArc = 180;

		// Token: 0x040014DA RID: 5338
		private const double TwelveHours = 0.5;

		// Token: 0x040014DB RID: 5339
		private const double Noon2000Jan01 = 730120.5;

		// Token: 0x040014DC RID: 5340
		internal const double MeanTropicalYearInDays = 365.242189;

		// Token: 0x040014DD RID: 5341
		private const double MeanSpeedOfSun = 1.0145616361111112;

		// Token: 0x040014DE RID: 5342
		private const double LongitudeSpring = 0.0;

		// Token: 0x040014DF RID: 5343
		private const double TwoDegreesAfterSpring = 2.0;

		// Token: 0x040014E0 RID: 5344
		private const int SecondsPerDay = 86400;

		// Token: 0x040014E1 RID: 5345
		private const int DaysInUniformLengthCentury = 36525;

		// Token: 0x040014E2 RID: 5346
		private const int SecondsPerMinute = 60;

		// Token: 0x040014E3 RID: 5347
		private const int MinutesPerDegree = 60;

		// Token: 0x040014E4 RID: 5348
		private static long StartOf1810 = CalendricalCalculationsHelper.GetNumberOfDays(new DateTime(1810, 1, 1));

		// Token: 0x040014E5 RID: 5349
		private static long StartOf1900Century = CalendricalCalculationsHelper.GetNumberOfDays(new DateTime(1900, 1, 1));

		// Token: 0x040014E6 RID: 5350
		private static double[] Coefficients1900to1987 = new double[] { -2E-05, 0.000297, 0.025184, -0.181133, 0.55304, -0.861938, 0.677066, -0.212591 };

		// Token: 0x040014E7 RID: 5351
		private static double[] Coefficients1800to1899 = new double[]
		{
			-9E-06, 0.003844, 0.083563, 0.865736, 4.867575, 15.845535, 31.332267, 38.291999, 28.316289, 11.636204,
			2.043794
		};

		// Token: 0x040014E8 RID: 5352
		private static double[] Coefficients1700to1799 = new double[] { 8.118780842, -0.005092142, 0.003336121, -2.66484E-05 };

		// Token: 0x040014E9 RID: 5353
		private static double[] Coefficients1620to1699 = new double[] { 196.58333, -4.0675, 0.0219167 };

		// Token: 0x040014EA RID: 5354
		private static double[] LambdaCoefficients = new double[] { 280.46645, 36000.76983, 0.0003032 };

		// Token: 0x040014EB RID: 5355
		private static double[] AnomalyCoefficients = new double[] { 357.5291, 35999.0503, -0.0001559, -4.8E-07 };

		// Token: 0x040014EC RID: 5356
		private static double[] EccentricityCoefficients = new double[] { 0.016708617, -4.2037E-05, -1.236E-07 };

		// Token: 0x040014ED RID: 5357
		private static double[] Coefficients = new double[]
		{
			CalendricalCalculationsHelper.Angle(23, 26, 21.448),
			CalendricalCalculationsHelper.Angle(0, 0, -46.815),
			CalendricalCalculationsHelper.Angle(0, 0, -0.00059),
			CalendricalCalculationsHelper.Angle(0, 0, 0.001813)
		};

		// Token: 0x040014EE RID: 5358
		private static double[] CoefficientsA = new double[] { 124.9, -1934.134, 0.002063 };

		// Token: 0x040014EF RID: 5359
		private static double[] CoefficientsB = new double[] { 201.11, 72001.5377, 0.00057 };

		// Token: 0x040014F0 RID: 5360
		private static CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap[] EphemerisCorrectionTable = new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap[]
		{
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(2020, CalendricalCalculationsHelper.CorrectionAlgorithm.Default),
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(1988, CalendricalCalculationsHelper.CorrectionAlgorithm.Year1988to2019),
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(1900, CalendricalCalculationsHelper.CorrectionAlgorithm.Year1900to1987),
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(1800, CalendricalCalculationsHelper.CorrectionAlgorithm.Year1800to1899),
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(1700, CalendricalCalculationsHelper.CorrectionAlgorithm.Year1700to1799),
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(1620, CalendricalCalculationsHelper.CorrectionAlgorithm.Year1620to1699),
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(int.MinValue, CalendricalCalculationsHelper.CorrectionAlgorithm.Default)
		};

		// Token: 0x02000B6A RID: 2922
		private enum CorrectionAlgorithm
		{
			// Token: 0x04003468 RID: 13416
			Default,
			// Token: 0x04003469 RID: 13417
			Year1988to2019,
			// Token: 0x0400346A RID: 13418
			Year1900to1987,
			// Token: 0x0400346B RID: 13419
			Year1800to1899,
			// Token: 0x0400346C RID: 13420
			Year1700to1799,
			// Token: 0x0400346D RID: 13421
			Year1620to1699
		}

		// Token: 0x02000B6B RID: 2923
		private struct EphemerisCorrectionAlgorithmMap
		{
			// Token: 0x06006C42 RID: 27714 RVA: 0x00177B51 File Offset: 0x00175D51
			public EphemerisCorrectionAlgorithmMap(int year, CalendricalCalculationsHelper.CorrectionAlgorithm algorithm)
			{
				this._lowestYear = year;
				this._algorithm = algorithm;
			}

			// Token: 0x0400346E RID: 13422
			internal int _lowestYear;

			// Token: 0x0400346F RID: 13423
			internal CalendricalCalculationsHelper.CorrectionAlgorithm _algorithm;
		}
	}
}
