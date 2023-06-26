using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.WindowsDeviceRecoveryTool.Common.Tracing;

namespace Microsoft.WindowsDeviceRecoveryTool.Model
{
	// Token: 0x02000015 RID: 21
	public class RegionAndLanguage
	{
		// Token: 0x06000147 RID: 327
		[DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
		private static extern int GetUserGeoID(RegionAndLanguage.GeoClass geoClass);

		// Token: 0x06000148 RID: 328
		[DllImport("kernel32.dll")]
		private static extern int GetUserDefaultLCID();

		// Token: 0x06000149 RID: 329
		[DllImport("kernel32.dll")]
		private static extern int GetGeoInfo(int geoid, int geoType, StringBuilder lpGeoData, int cchData, int langid);

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00004D4C File Offset: 0x00002F4C
		public static string CurrentLocation
		{
			get
			{
				return RegionAndLanguage.currentLocation;
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00004D64 File Offset: 0x00002F64
		private static string GetMachineCurrentLocation()
		{
			RegionInfo regionInfo = RegionAndLanguage.SafelyGetRegionInfo(CultureInfo.CurrentCulture.LCID);
			string text = string.Empty;
			string text2 = string.Empty;
			try
			{
				int userGeoID = RegionAndLanguage.GetUserGeoID(RegionAndLanguage.GeoClass.Nation);
				int userDefaultLCID = RegionAndLanguage.GetUserDefaultLCID();
				StringBuilder stringBuilder = new StringBuilder(100);
				RegionAndLanguage.GetGeoInfo(userGeoID, 8, stringBuilder, stringBuilder.Capacity, userDefaultLCID);
				Tracer<RegionAndLanguage>.WriteInformation("GetGEoInfo returned location: {0}", new object[] { stringBuilder });
				Collection<Location> collection = RegionAndLanguage.CreateLocationList();
				foreach (Location location in collection)
				{
					bool flag = location.CountryEnglishName == stringBuilder.ToString() && location.GeoId == userGeoID;
					if (flag)
					{
						text2 = location.IetfLanguageTag;
					}
				}
				bool flag2 = !string.IsNullOrWhiteSpace(text2);
				if (flag2)
				{
					regionInfo = RegionAndLanguage.SafelyGetRegionInfo(text2);
				}
				else
				{
					Tracer<RegionAndLanguage>.WriteInformation("Culture not found for: {0}. Trying read region info from location id: {1}", new object[] { stringBuilder, userDefaultLCID });
					regionInfo = RegionAndLanguage.SafelyGetRegionInfo(userDefaultLCID);
				}
				bool flag3 = regionInfo != null;
				if (flag3)
				{
					text = regionInfo.TwoLetterISORegionName;
				}
				else
				{
					text2 = ((!string.IsNullOrWhiteSpace(text2)) ? text2 : CultureInfo.CurrentCulture.IetfLanguageTag);
					Tracer<RegionAndLanguage>.WriteInformation("Region not found. Extracting country code from language tag: {0}", new object[] { text2 });
					text = RegionAndLanguage.ExtractCountryCodeFromLanguageTag(text2);
				}
			}
			catch (Exception ex)
			{
				Tracer<RegionAndLanguage>.WriteWarning(ex, "Could not read culture location info: {0}", new object[] { text2 });
			}
			return text;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00004F20 File Offset: 0x00003120
		private static RegionInfo SafelyGetRegionInfo(int lcid)
		{
			try
			{
				return new RegionInfo(lcid);
			}
			catch (Exception ex)
			{
				Tracer<RegionAndLanguage>.WriteWarning(ex, "Could not read region info: {0}", new object[] { lcid });
			}
			return null;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00004F70 File Offset: 0x00003170
		private static RegionInfo SafelyGetRegionInfo(string languageTag)
		{
			try
			{
				return new RegionInfo(languageTag);
			}
			catch (Exception ex)
			{
				Tracer<RegionAndLanguage>.WriteWarning(ex, "Could not read region info: {0}", new object[] { languageTag });
			}
			return null;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00004FB8 File Offset: 0x000031B8
		private static string ExtractCountryCodeFromLanguageTag(string ietfFlag)
		{
			Regex regex = new Regex(".*-([A-Za-z][A-Za-z])");
			Match match = regex.Match(ietfFlag);
			bool success = match.Success;
			string text;
			if (success)
			{
				text = match.Groups[1].Value;
			}
			else
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00005004 File Offset: 0x00003204
		private static Collection<Location> CreateLocationList()
		{
			Collection<Location> collection = new Collection<Location>();
			CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("en-GB", false, "", "", ""));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("ru-RU", false, "", "", ""));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("en-CA", false, "", "", ""));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("es-CL", false, "", "", ""));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("es-GT", false, "", "", ""));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("en-PH", false, "", "", ""));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("af-ZA", false, "", "", ""));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("es-EC", false, "", "", ""));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("ms-MY", false, "", "", ""));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("ko-KR", false, "한국", "", ""));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("th-TH", false, "ประเทศไทย", "", ""));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("ur-PK", true, "پاکستان", "", ""));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("tk-TM", false, "Туркменистан", "", ""));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("cs-CZ", false, "Česko", "", ""));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("sk-SK", false, "Slovensko", "", ""));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("ar-SY", true, "سورية", "", ""));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("si-LK", false, "ශ\u0dcaර\u0dd3 ල\u0d82ක\u0dcf", "", ""));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("dv-MV", true, "ދ\u07a8ވ\u07acހ\u07a8", "", ""));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("es-BO", false, "Bolivia", "", ""));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("ga-IE", false, "Ireland", "", ""));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("ms-BN", true, "بروني دارالسلام", "", ""));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("ar-DZ", true, "الجزائر", "", ""));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("ar-MA", true, "المغرب", "", ""));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("en-TT", false, "Trinidad and Tobago", "", ""));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("sr-Latn-ME", false, "Црна Гора", "", "sr-ME"));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("zh-HK", false, "香港", "Hong Kong", ""));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("es-PE", false, "Perú", "Peru", ""));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("zh-MO", false, "澳門", "Macau", ""));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("fr-MC", false, "Monaco", "Monaco", ""));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("es-VE", false, "Venezuela", "Venezuela", ""));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("sr-Latn-CS", false, "Srbija i Crna Gora", "Serbia and Montenegro", "sr-CS"));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("bo-CN", false, "中国", "China", ""));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("lo-LA", false, "ສາທາລະນະລ\u0eb1ດ ປະຊາທ\u0eb4ປະໄຕ ປະຊາຊ\u0ebbນລາວ", "Laos", ""));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("mk-MK", false, "", "Macedonia", ""));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("uz-Latn-UZ", false, "Ўзбекистон", "", "uz-UZ"));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("sr-Latn-RS", false, "", "", "sr-RS"));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("tg-Cyrl-TJ", false, "", "", "tg-TJ"));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("az-Latn-AZ", false, "", "", "az-AZ"));
			collection.Add(RegionAndLanguage.CreateCountryLocationFix("ha-Latn-NG", false, "", "", "ha-NG"));
			for (;;)
			{
				int num;
				if ((num = collection.ToList<Location>().FindIndex((Location x) => x == null)) == -1)
				{
					break;
				}
				collection.RemoveAt(num);
			}
			CultureInfo[] array = cultures;
			for (int i = 0; i < array.Length; i++)
			{
				CultureInfo cul = array[i];
				try
				{
					CultureInfo cultureInfo = new CultureInfo(cul.Name, false);
					RegionInfo country = new RegionInfo(cultureInfo.LCID);
					bool flag = collection.Count((Location p) => p.IetfLanguageTag.Substring(p.IetfLanguageTag.LastIndexOf('-') + 1) == cul.IetfLanguageTag.Substring(cul.IetfLanguageTag.LastIndexOf('-') + 1) || p.GeoId == country.GeoId) == 0;
					if (flag)
					{
						collection.Add(new Location(country.EnglishName, cul.IetfLanguageTag, country.GeoId));
					}
				}
				catch (Exception ex)
				{
				}
			}
			return collection;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000564C File Offset: 0x0000384C
		private static Location CreateCountryLocationFix(string ietfLanguageTag, bool isRightToLeft, string nativeName = "", string englishName = "", string customIetfLanguageTag = "")
		{
			Location location;
			try
			{
				CultureInfo cultureInfo = new CultureInfo(ietfLanguageTag);
				RegionInfo regionInfo = new RegionInfo(new CultureInfo(cultureInfo.Name, false).LCID);
				nativeName = ((nativeName == string.Empty) ? regionInfo.NativeName : nativeName);
				englishName = ((englishName == string.Empty) ? regionInfo.EnglishName : englishName);
				ietfLanguageTag = ((customIetfLanguageTag == string.Empty) ? cultureInfo.IetfLanguageTag : customIetfLanguageTag);
				location = new Location(englishName, ietfLanguageTag, regionInfo.GeoId);
			}
			catch (Exception ex)
			{
				location = null;
			}
			return location;
		}

		// Token: 0x04000070 RID: 112
		private const int GEO_FRIENDLYNAME = 8;

		// Token: 0x04000071 RID: 113
		private static readonly string currentLocation = RegionAndLanguage.GetMachineCurrentLocation();

		// Token: 0x02000053 RID: 83
		private enum GeoClass
		{
			// Token: 0x0400023D RID: 573
			Nation = 16,
			// Token: 0x0400023E RID: 574
			Region = 14
		}
	}
}
