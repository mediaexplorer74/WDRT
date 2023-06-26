using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;
using System.Threading;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert <see cref="T:System.Globalization.CultureInfo" /> objects to and from various other representations.</summary>
	// Token: 0x02000530 RID: 1328
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class CultureInfoConverter : TypeConverter
	{
		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x06003224 RID: 12836 RVA: 0x000E0C4F File Offset: 0x000DEE4F
		private string DefaultCultureString
		{
			get
			{
				return SR.GetString("CultureInfoConverterDefaultCultureString");
			}
		}

		/// <summary>Retrieves the name of the specified culture.</summary>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that specifies the culture to get the name for.</param>
		/// <returns>The name of the specified culture.</returns>
		// Token: 0x06003225 RID: 12837 RVA: 0x000E0C5B File Offset: 0x000DEE5B
		protected virtual string GetCultureName(CultureInfo culture)
		{
			return culture.Name;
		}

		/// <summary>Gets a value indicating whether this converter can convert an object in the given source type to a <see cref="T:System.Globalization.CultureInfo" /> using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you wish to convert from.</param>
		/// <returns>
		///   <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003226 RID: 12838 RVA: 0x000E0C63 File Offset: 0x000DEE63
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Gets a value indicating whether this converter can convert an object to the given destination type using the context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you wish to convert to.</param>
		/// <returns>
		///   <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003227 RID: 12839 RVA: 0x000E0C81 File Offset: 0x000DEE81
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converts the specified value object to a <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that specifies the culture to which to convert.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> specifies a culture that is not valid.</exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x06003228 RID: 12840 RVA: 0x000E0CA0 File Offset: 0x000DEEA0
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (!(value is string))
			{
				return base.ConvertFrom(context, culture, value);
			}
			string text = (string)value;
			if (this.GetCultureName(CultureInfo.InvariantCulture).Equals(""))
			{
				text = CultureInfoConverter.CultureInfoMapper.GetCultureInfoName((string)value);
			}
			CultureInfo cultureInfo = null;
			CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
			if (culture != null && culture.Equals(CultureInfo.InvariantCulture))
			{
				Thread.CurrentThread.CurrentUICulture = culture;
			}
			try
			{
				if (text == null || text.Length == 0 || string.Compare(text, this.DefaultCultureString, StringComparison.Ordinal) == 0)
				{
					cultureInfo = CultureInfo.InvariantCulture;
				}
				if (cultureInfo == null)
				{
					ICollection standardValues = this.GetStandardValues(context);
					foreach (object obj in standardValues)
					{
						CultureInfo cultureInfo2 = (CultureInfo)obj;
						if (cultureInfo2 != null && string.Compare(this.GetCultureName(cultureInfo2), text, StringComparison.Ordinal) == 0)
						{
							cultureInfo = cultureInfo2;
							break;
						}
					}
				}
				if (cultureInfo == null)
				{
					try
					{
						cultureInfo = new CultureInfo(text);
					}
					catch
					{
					}
				}
				if (cultureInfo == null)
				{
					text = text.ToLower(CultureInfo.CurrentCulture);
					foreach (object obj2 in this.values)
					{
						CultureInfo cultureInfo3 = (CultureInfo)obj2;
						if (cultureInfo3 != null && this.GetCultureName(cultureInfo3).ToLower(CultureInfo.CurrentCulture).StartsWith(text))
						{
							cultureInfo = cultureInfo3;
							break;
						}
					}
				}
			}
			finally
			{
				Thread.CurrentThread.CurrentUICulture = currentUICulture;
			}
			if (cultureInfo == null)
			{
				throw new ArgumentException(SR.GetString("CultureInfoConverterInvalidCulture", new object[] { (string)value }));
			}
			return cultureInfo;
		}

		/// <summary>Converts the given value object to the specified destination type.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that specifies the culture to which to convert.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the value to.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
		// Token: 0x06003229 RID: 12841 RVA: 0x000E0E30 File Offset: 0x000DF030
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string))
			{
				CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
				if (culture != null && culture.Equals(CultureInfo.InvariantCulture))
				{
					Thread.CurrentThread.CurrentUICulture = culture;
				}
				string text;
				try
				{
					if (value == null || value == CultureInfo.InvariantCulture)
					{
						text = this.DefaultCultureString;
					}
					else
					{
						text = this.GetCultureName((CultureInfo)value);
					}
				}
				finally
				{
					Thread.CurrentThread.CurrentUICulture = currentUICulture;
				}
				return text;
			}
			if (destinationType == typeof(InstanceDescriptor) && value is CultureInfo)
			{
				CultureInfo cultureInfo = (CultureInfo)value;
				ConstructorInfo constructor = typeof(CultureInfo).GetConstructor(new Type[] { typeof(string) });
				if (constructor != null)
				{
					return new InstanceDescriptor(constructor, new object[] { cultureInfo.Name });
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>Gets a collection of standard values for a <see cref="T:System.Globalization.CultureInfo" /> object using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> containing a standard set of valid values, or <see langword="null" /> if the data type does not support a standard set of values.</returns>
		// Token: 0x0600322A RID: 12842 RVA: 0x000E0F3C File Offset: 0x000DF13C
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (this.values == null)
			{
				CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.NeutralCultures | CultureTypes.SpecificCultures);
				int num = Array.IndexOf<CultureInfo>(cultures, CultureInfo.InvariantCulture);
				CultureInfo[] array;
				if (num != -1)
				{
					cultures[num] = null;
					array = new CultureInfo[cultures.Length];
				}
				else
				{
					array = new CultureInfo[cultures.Length + 1];
				}
				Array.Copy(cultures, array, cultures.Length);
				Array.Sort(array, new CultureInfoConverter.CultureComparer(this));
				if (array[0] == null)
				{
					array[0] = CultureInfo.InvariantCulture;
				}
				this.values = new TypeConverter.StandardValuesCollection(array);
			}
			return this.values;
		}

		/// <summary>Gets a value indicating whether the list of standard values returned from <see cref="M:System.ComponentModel.CultureInfoConverter.GetStandardValues(System.ComponentModel.ITypeDescriptorContext)" /> is an exhaustive list.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <returns>
		///   <see langword="false" /> because the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> returned from <see cref="M:System.ComponentModel.CultureInfoConverter.GetStandardValues(System.ComponentModel.ITypeDescriptorContext)" /> is not an exhaustive list of possible values (that is, other values are possible). This method never returns <see langword="true" />.</returns>
		// Token: 0x0600322B RID: 12843 RVA: 0x000E0FB7 File Offset: 0x000DF1B7
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		/// <summary>Gets a value indicating whether this object supports a standard set of values that can be picked from a list using the specified context.</summary>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <returns>
		///   <see langword="true" /> because <see cref="M:System.ComponentModel.CultureInfoConverter.GetStandardValues(System.ComponentModel.ITypeDescriptorContext)" /> should be called to find a common set of values the object supports. This method never returns <see langword="false" />.</returns>
		// Token: 0x0600322C RID: 12844 RVA: 0x000E0FBA File Offset: 0x000DF1BA
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x04002951 RID: 10577
		private TypeConverter.StandardValuesCollection values;

		// Token: 0x0200088F RID: 2191
		private class CultureComparer : IComparer
		{
			// Token: 0x0600456B RID: 17771 RVA: 0x00121176 File Offset: 0x0011F376
			public CultureComparer(CultureInfoConverter cultureConverter)
			{
				this.converter = cultureConverter;
			}

			// Token: 0x0600456C RID: 17772 RVA: 0x00121188 File Offset: 0x0011F388
			public int Compare(object item1, object item2)
			{
				if (item1 == null)
				{
					if (item2 == null)
					{
						return 0;
					}
					return -1;
				}
				else
				{
					if (item2 == null)
					{
						return 1;
					}
					string cultureName = this.converter.GetCultureName((CultureInfo)item1);
					string cultureName2 = this.converter.GetCultureName((CultureInfo)item2);
					CompareInfo compareInfo = CultureInfo.CurrentCulture.CompareInfo;
					return compareInfo.Compare(cultureName, cultureName2, CompareOptions.StringSort);
				}
			}

			// Token: 0x040037A7 RID: 14247
			private CultureInfoConverter converter;
		}

		// Token: 0x02000890 RID: 2192
		private static class CultureInfoMapper
		{
			// Token: 0x0600456D RID: 17773 RVA: 0x001211E0 File Offset: 0x0011F3E0
			public static string GetCultureInfoName(string cultureInfoDisplayName)
			{
				if (CultureInfoConverter.CultureInfoMapper.cultureInfoNameMap == null)
				{
					CultureInfoConverter.CultureInfoMapper.InitializeCultureInfoMap();
				}
				if (CultureInfoConverter.CultureInfoMapper.cultureInfoNameMap.ContainsKey(cultureInfoDisplayName))
				{
					return CultureInfoConverter.CultureInfoMapper.cultureInfoNameMap[cultureInfoDisplayName];
				}
				return cultureInfoDisplayName;
			}

			// Token: 0x0600456E RID: 17774 RVA: 0x00121210 File Offset: 0x0011F410
			private static void InitializeCultureInfoMap()
			{
				CultureInfoConverter.CultureInfoMapper.cultureInfoNameMap = new Dictionary<string, string>
				{
					{ "Afrikaans", "af" },
					{ "Afrikaans (South Africa)", "af-ZA" },
					{ "Albanian", "sq" },
					{ "Albanian (Albania)", "sq-AL" },
					{ "Alsatian (France)", "gsw-FR" },
					{ "Amharic (Ethiopia)", "am-ET" },
					{ "Arabic", "ar" },
					{ "Arabic (Algeria)", "ar-DZ" },
					{ "Arabic (Bahrain)", "ar-BH" },
					{ "Arabic (Egypt)", "ar-EG" },
					{ "Arabic (Iraq)", "ar-IQ" },
					{ "Arabic (Jordan)", "ar-JO" },
					{ "Arabic (Kuwait)", "ar-KW" },
					{ "Arabic (Lebanon)", "ar-LB" },
					{ "Arabic (Libya)", "ar-LY" },
					{ "Arabic (Morocco)", "ar-MA" },
					{ "Arabic (Oman)", "ar-OM" },
					{ "Arabic (Qatar)", "ar-QA" },
					{ "Arabic (Saudi Arabia)", "ar-SA" },
					{ "Arabic (Syria)", "ar-SY" },
					{ "Arabic (Tunisia)", "ar-TN" },
					{ "Arabic (U.A.E.)", "ar-AE" },
					{ "Arabic (Yemen)", "ar-YE" },
					{ "Armenian", "hy" },
					{ "Armenian (Armenia)", "hy-AM" },
					{ "Assamese (India)", "as-IN" },
					{ "Azeri", "az" },
					{ "Azeri (Cyrillic, Azerbaijan)", "az-Cyrl-AZ" },
					{ "Azeri (Latin, Azerbaijan)", "az-Latn-AZ" },
					{ "Bashkir (Russia)", "ba-RU" },
					{ "Basque", "eu" },
					{ "Basque (Basque)", "eu-ES" },
					{ "Belarusian", "be" },
					{ "Belarusian (Belarus)", "be-BY" },
					{ "Bengali (Bangladesh)", "bn-BD" },
					{ "Bengali (India)", "bn-IN" },
					{ "Bosnian (Cyrillic, Bosnia and Herzegovina)", "bs-Cyrl-BA" },
					{ "Bosnian (Latin, Bosnia and Herzegovina)", "bs-Latn-BA" },
					{ "Breton (France)", "br-FR" },
					{ "Bulgarian", "bg" },
					{ "Bulgarian (Bulgaria)", "bg-BG" },
					{ "Catalan", "ca" },
					{ "Catalan (Catalan)", "ca-ES" },
					{ "Chinese (Hong Kong S.A.R.)", "zh-HK" },
					{ "Chinese (Macao S.A.R.)", "zh-MO" },
					{ "Chinese (People's Republic of China)", "zh-CN" },
					{ "Chinese (Simplified)", "zh-CHS" },
					{ "Chinese (Singapore)", "zh-SG" },
					{ "Chinese (Taiwan)", "zh-TW" },
					{ "Chinese (Traditional)", "zh-CHT" },
					{ "Corsican (France)", "co-FR" },
					{ "Croatian", "hr" },
					{ "Croatian (Croatia)", "hr-HR" },
					{ "Croatian (Latin, Bosnia and Herzegovina)", "hr-BA" },
					{ "Czech", "cs" },
					{ "Czech (Czech Republic)", "cs-CZ" },
					{ "Danish", "da" },
					{ "Danish (Denmark)", "da-DK" },
					{ "Dari (Afghanistan)", "prs-AF" },
					{ "Divehi", "dv" },
					{ "Divehi (Maldives)", "dv-MV" },
					{ "Dutch", "nl" },
					{ "Dutch (Belgium)", "nl-BE" },
					{ "Dutch (Netherlands)", "nl-NL" },
					{ "English", "en" },
					{ "English (Australia)", "en-AU" },
					{ "English (Belize)", "en-BZ" },
					{ "English (Canada)", "en-CA" },
					{ "English (Caribbean)", "en-029" },
					{ "English (India)", "en-IN" },
					{ "English (Ireland)", "en-IE" },
					{ "English (Jamaica)", "en-JM" },
					{ "English (Malaysia)", "en-MY" },
					{ "English (New Zealand)", "en-NZ" },
					{ "English (Republic of the Philippines)", "en-PH" },
					{ "English (Singapore)", "en-SG" },
					{ "English (South Africa)", "en-ZA" },
					{ "English (Trinidad and Tobago)", "en-TT" },
					{ "English (United Kingdom)", "en-GB" },
					{ "English (United States)", "en-US" },
					{ "English (Zimbabwe)", "en-ZW" },
					{ "Estonian", "et" },
					{ "Estonian (Estonia)", "et-EE" },
					{ "Faroese", "fo" },
					{ "Faroese (Faroe Islands)", "fo-FO" },
					{ "Filipino (Philippines)", "fil-PH" },
					{ "Finnish", "fi" },
					{ "Finnish (Finland)", "fi-FI" },
					{ "French", "fr" },
					{ "French (Belgium)", "fr-BE" },
					{ "French (Canada)", "fr-CA" },
					{ "French (France)", "fr-FR" },
					{ "French (Luxembourg)", "fr-LU" },
					{ "French (Principality of Monaco)", "fr-MC" },
					{ "French (Switzerland)", "fr-CH" },
					{ "Frisian (Netherlands)", "fy-NL" },
					{ "Galician", "gl" },
					{ "Galician (Galician)", "gl-ES" },
					{ "Georgian", "ka" },
					{ "Georgian (Georgia)", "ka-GE" },
					{ "German", "de" },
					{ "German (Austria)", "de-AT" },
					{ "German (Germany)", "de-DE" },
					{ "German (Liechtenstein)", "de-LI" },
					{ "German (Luxembourg)", "de-LU" },
					{ "German (Switzerland)", "de-CH" },
					{ "Greek", "el" },
					{ "Greek (Greece)", "el-GR" },
					{ "Greenlandic (Greenland)", "kl-GL" },
					{ "Gujarati", "gu" },
					{ "Gujarati (India)", "gu-IN" },
					{ "Hausa (Latin, Nigeria)", "ha-Latn-NG" },
					{ "Hebrew", "he" },
					{ "Hebrew (Israel)", "he-IL" },
					{ "Hindi", "hi" },
					{ "Hindi (India)", "hi-IN" },
					{ "Hungarian", "hu" },
					{ "Hungarian (Hungary)", "hu-HU" },
					{ "Icelandic", "is" },
					{ "Icelandic (Iceland)", "is-IS" },
					{ "Igbo (Nigeria)", "ig-NG" },
					{ "Indonesian", "id" },
					{ "Indonesian (Indonesia)", "id-ID" },
					{ "Inuktitut (Latin, Canada)", "iu-Latn-CA" },
					{ "Inuktitut (Syllabics, Canada)", "iu-Cans-CA" },
					{ "Invariant Language (Invariant Country)", "" },
					{ "Irish (Ireland)", "ga-IE" },
					{ "isiXhosa (South Africa)", "xh-ZA" },
					{ "isiZulu (South Africa)", "zu-ZA" },
					{ "Italian", "it" },
					{ "Italian (Italy)", "it-IT" },
					{ "Italian (Switzerland)", "it-CH" },
					{ "Japanese", "ja" },
					{ "Japanese (Japan)", "ja-JP" },
					{ "K'iche (Guatemala)", "qut-GT" },
					{ "Kannada", "kn" },
					{ "Kannada (India)", "kn-IN" },
					{ "Kazakh", "kk" },
					{ "Kazakh (Kazakhstan)", "kk-KZ" },
					{ "Khmer (Cambodia)", "km-KH" },
					{ "Kinyarwanda (Rwanda)", "rw-RW" },
					{ "Kiswahili", "sw" },
					{ "Kiswahili (Kenya)", "sw-KE" },
					{ "Konkani", "kok" },
					{ "Konkani (India)", "kok-IN" },
					{ "Korean", "ko" },
					{ "Korean (Korea)", "ko-KR" },
					{ "Kyrgyz", "ky" },
					{ "Kyrgyz (Kyrgyzstan)", "ky-KG" },
					{ "Lao (Lao P.D.R.)", "lo-LA" },
					{ "Latvian", "lv" },
					{ "Latvian (Latvia)", "lv-LV" },
					{ "Lithuanian", "lt" },
					{ "Lithuanian (Lithuania)", "lt-LT" },
					{ "Lower Sorbian (Germany)", "dsb-DE" },
					{ "Luxembourgish (Luxembourg)", "lb-LU" },
					{ "Macedonian", "mk" },
					{ "Macedonian (Former Yugoslav Republic of Macedonia)", "mk-MK" },
					{ "Malay", "ms" },
					{ "Malay (Brunei Darussalam)", "ms-BN" },
					{ "Malay (Malaysia)", "ms-MY" },
					{ "Malayalam (India)", "ml-IN" },
					{ "Maltese (Malta)", "mt-MT" },
					{ "Maori (New Zealand)", "mi-NZ" },
					{ "Mapudungun (Chile)", "arn-CL" },
					{ "Marathi", "mr" },
					{ "Marathi (India)", "mr-IN" },
					{ "Mohawk (Mohawk)", "moh-CA" },
					{ "Mongolian", "mn" },
					{ "Mongolian (Cyrillic, Mongolia)", "mn-MN" },
					{ "Mongolian (Traditional Mongolian, PRC)", "mn-Mong-CN" },
					{ "Nepali (Nepal)", "ne-NP" },
					{ "Norwegian", "no" },
					{ "Norwegian, Bokmål (Norway)", "nb-NO" },
					{ "Norwegian, Nynorsk (Norway)", "nn-NO" },
					{ "Occitan (France)", "oc-FR" },
					{ "Oriya (India)", "or-IN" },
					{ "Pashto (Afghanistan)", "ps-AF" },
					{ "Persian", "fa" },
					{ "Persian (Iran)", "fa-IR" },
					{ "Polish", "pl" },
					{ "Polish (Poland)", "pl-PL" },
					{ "Portuguese", "pt" },
					{ "Portuguese (Brazil)", "pt-BR" },
					{ "Portuguese (Portugal)", "pt-PT" },
					{ "Punjabi", "pa" },
					{ "Punjabi (India)", "pa-IN" },
					{ "Quechua (Bolivia)", "quz-BO" },
					{ "Quechua (Ecuador)", "quz-EC" },
					{ "Quechua (Peru)", "quz-PE" },
					{ "Romanian", "ro" },
					{ "Romanian (Romania)", "ro-RO" },
					{ "Romansh (Switzerland)", "rm-CH" },
					{ "Russian", "ru" },
					{ "Russian (Russia)", "ru-RU" },
					{ "Sami, Inari (Finland)", "smn-FI" },
					{ "Sami, Lule (Norway)", "smj-NO" },
					{ "Sami, Lule (Sweden)", "smj-SE" },
					{ "Sami, Northern (Finland)", "se-FI" },
					{ "Sami, Northern (Norway)", "se-NO" },
					{ "Sami, Northern (Sweden)", "se-SE" },
					{ "Sami, Skolt (Finland)", "sms-FI" },
					{ "Sami, Southern (Norway)", "sma-NO" },
					{ "Sami, Southern (Sweden)", "sma-SE" },
					{ "Sanskrit", "sa" },
					{ "Sanskrit (India)", "sa-IN" },
					{ "Serbian", "sr" },
					{ "Serbian (Cyrillic, Bosnia and Herzegovina)", "sr-Cyrl-BA" },
					{ "Serbian (Cyrillic, Serbia)", "sr-Cyrl-CS" },
					{ "Serbian (Latin, Bosnia and Herzegovina)", "sr-Latn-BA" },
					{ "Serbian (Latin, Serbia)", "sr-Latn-CS" },
					{ "Sesotho sa Leboa (South Africa)", "nso-ZA" },
					{ "Setswana (South Africa)", "tn-ZA" },
					{ "Sinhala (Sri Lanka)", "si-LK" },
					{ "Slovak", "sk" },
					{ "Slovak (Slovakia)", "sk-SK" },
					{ "Slovenian", "sl" },
					{ "Slovenian (Slovenia)", "sl-SI" },
					{ "Spanish", "es" },
					{ "Spanish (Argentina)", "es-AR" },
					{ "Spanish (Bolivia)", "es-BO" },
					{ "Spanish (Chile)", "es-CL" },
					{ "Spanish (Colombia)", "es-CO" },
					{ "Spanish (Costa Rica)", "es-CR" },
					{ "Spanish (Dominican Republic)", "es-DO" },
					{ "Spanish (Ecuador)", "es-EC" },
					{ "Spanish (El Salvador)", "es-SV" },
					{ "Spanish (Guatemala)", "es-GT" },
					{ "Spanish (Honduras)", "es-HN" },
					{ "Spanish (Mexico)", "es-MX" },
					{ "Spanish (Nicaragua)", "es-NI" },
					{ "Spanish (Panama)", "es-PA" },
					{ "Spanish (Paraguay)", "es-PY" },
					{ "Spanish (Peru)", "es-PE" },
					{ "Spanish (Puerto Rico)", "es-PR" },
					{ "Spanish (Spain)", "es-ES" },
					{ "Spanish (United States)", "es-US" },
					{ "Spanish (Uruguay)", "es-UY" },
					{ "Spanish (Venezuela)", "es-VE" },
					{ "Swedish", "sv" },
					{ "Swedish (Finland)", "sv-FI" },
					{ "Swedish (Sweden)", "sv-SE" },
					{ "Syriac", "syr" },
					{ "Syriac (Syria)", "syr-SY" },
					{ "Tajik (Cyrillic, Tajikistan)", "tg-Cyrl-TJ" },
					{ "Tamazight (Latin, Algeria)", "tzm-Latn-DZ" },
					{ "Tamil", "ta" },
					{ "Tamil (India)", "ta-IN" },
					{ "Tatar", "tt" },
					{ "Tatar (Russia)", "tt-RU" },
					{ "Telugu", "te" },
					{ "Telugu (India)", "te-IN" },
					{ "Thai", "th" },
					{ "Thai (Thailand)", "th-TH" },
					{ "Tibetan (PRC)", "bo-CN" },
					{ "Turkish", "tr" },
					{ "Turkish (Turkey)", "tr-TR" },
					{ "Turkmen (Turkmenistan)", "tk-TM" },
					{ "Uighur (PRC)", "ug-CN" },
					{ "Ukrainian", "uk" },
					{ "Ukrainian (Ukraine)", "uk-UA" },
					{ "Upper Sorbian (Germany)", "hsb-DE" },
					{ "Urdu", "ur" },
					{ "Urdu (Islamic Republic of Pakistan)", "ur-PK" },
					{ "Uzbek", "uz" },
					{ "Uzbek (Cyrillic, Uzbekistan)", "uz-Cyrl-UZ" },
					{ "Uzbek (Latin, Uzbekistan)", "uz-Latn-UZ" },
					{ "Vietnamese", "vi" },
					{ "Vietnamese (Vietnam)", "vi-VN" },
					{ "Welsh (United Kingdom)", "cy-GB" },
					{ "Wolof (Senegal)", "wo-SN" },
					{ "Yakut (Russia)", "sah-RU" },
					{ "Yi (PRC)", "ii-CN" },
					{ "Yoruba (Nigeria)", "yo-NG" }
				};
			}

			// Token: 0x040037A8 RID: 14248
			private static volatile Dictionary<string, string> cultureInfoNameMap;
		}
	}
}
