using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Globalization
{
	/// <summary>Contains information about the country/region.</summary>
	// Token: 0x020003CD RID: 973
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public class RegionInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.RegionInfo" /> class based on the country/region or specific culture, specified by name.</summary>
		/// <param name="name">A string that contains a two-letter code defined in ISO 3166 for country/region.  
		///  -or-  
		///  A string that contains the culture name for a specific culture, custom culture, or Windows-only culture. If the culture name is not in RFC 4646 format, your application should specify the entire culture name instead of just the country/region.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not a valid country/region name or specific culture name.</exception>
		// Token: 0x06003160 RID: 12640 RVA: 0x000BEE54 File Offset: 0x000BD054
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public RegionInfo(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NoRegionInvariantCulture"));
			}
			this.m_cultureData = CultureData.GetCultureDataForRegion(name, true);
			if (this.m_cultureData == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidCultureName"), name), "name");
			}
			if (this.m_cultureData.IsNeutralCulture)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidNeutralRegionName", new object[] { name }), "name");
			}
			this.SetName(name);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.RegionInfo" /> class based on the country/region associated with the specified culture identifier.</summary>
		/// <param name="culture">A culture identifier.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="culture" /> specifies either an invariant, custom, or neutral culture.</exception>
		// Token: 0x06003161 RID: 12641 RVA: 0x000BEEF8 File Offset: 0x000BD0F8
		[SecuritySafeCritical]
		public RegionInfo(int culture)
		{
			if (culture == 127)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NoRegionInvariantCulture"));
			}
			if (culture == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_CultureIsNeutral", new object[] { culture }), "culture");
			}
			if (culture == 3072)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_CustomCultureCannotBePassedByNumber", new object[] { culture }), "culture");
			}
			this.m_cultureData = CultureData.GetCultureData(culture, true);
			this.m_name = this.m_cultureData.SREGIONNAME;
			if (this.m_cultureData.IsNeutralCulture)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_CultureIsNeutral", new object[] { culture }), "culture");
			}
			this.m_cultureId = culture;
		}

		// Token: 0x06003162 RID: 12642 RVA: 0x000BEFC9 File Offset: 0x000BD1C9
		[SecuritySafeCritical]
		internal RegionInfo(CultureData cultureData)
		{
			this.m_cultureData = cultureData;
			this.m_name = this.m_cultureData.SREGIONNAME;
		}

		// Token: 0x06003163 RID: 12643 RVA: 0x000BEFE9 File Offset: 0x000BD1E9
		[SecurityCritical]
		private void SetName(string name)
		{
			this.m_name = (name.Equals(this.m_cultureData.SREGIONNAME, StringComparison.OrdinalIgnoreCase) ? this.m_cultureData.SREGIONNAME : this.m_cultureData.CultureName);
		}

		// Token: 0x06003164 RID: 12644 RVA: 0x000BF020 File Offset: 0x000BD220
		[SecurityCritical]
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			if (this.m_name == null)
			{
				this.m_cultureId = RegionInfo.IdFromEverettRegionInfoDataItem[this.m_dataItem];
			}
			if (this.m_cultureId == 0)
			{
				this.m_cultureData = CultureData.GetCultureDataForRegion(this.m_name, true);
			}
			else
			{
				this.m_cultureData = CultureData.GetCultureData(this.m_cultureId, true);
			}
			if (this.m_cultureData == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidCultureName"), this.m_name), "m_name");
			}
			if (this.m_cultureId == 0)
			{
				this.SetName(this.m_name);
				return;
			}
			this.m_name = this.m_cultureData.SREGIONNAME;
		}

		// Token: 0x06003165 RID: 12645 RVA: 0x000BF0C8 File Offset: 0x000BD2C8
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
		}

		/// <summary>Gets the <see cref="T:System.Globalization.RegionInfo" /> that represents the country/region used by the current thread.</summary>
		/// <returns>The <see cref="T:System.Globalization.RegionInfo" /> that represents the country/region used by the current thread.</returns>
		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06003166 RID: 12646 RVA: 0x000BF0CC File Offset: 0x000BD2CC
		[__DynamicallyInvokable]
		public static RegionInfo CurrentRegion
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				RegionInfo regionInfo = RegionInfo.s_currentRegionInfo;
				if (regionInfo == null)
				{
					regionInfo = new RegionInfo(CultureInfo.CurrentCulture.m_cultureData);
					regionInfo.m_name = regionInfo.m_cultureData.SREGIONNAME;
					RegionInfo.s_currentRegionInfo = regionInfo;
				}
				return regionInfo;
			}
		}

		/// <summary>Gets the name or ISO 3166 two-letter country/region code for the current <see cref="T:System.Globalization.RegionInfo" /> object.</summary>
		/// <returns>The value specified by the <paramref name="name" /> parameter of the <see cref="M:System.Globalization.RegionInfo.#ctor(System.String)" /> constructor. The return value is in uppercase.  
		///  -or-  
		///  The two-letter code defined in ISO 3166 for the country/region specified by the <paramref name="culture" /> parameter of the <see cref="M:System.Globalization.RegionInfo.#ctor(System.Int32)" /> constructor. The return value is in uppercase.</returns>
		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06003167 RID: 12647 RVA: 0x000BF10E File Offset: 0x000BD30E
		[__DynamicallyInvokable]
		public virtual string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_name;
			}
		}

		/// <summary>Gets the full name of the country/region in English.</summary>
		/// <returns>The full name of the country/region in English.</returns>
		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06003168 RID: 12648 RVA: 0x000BF116 File Offset: 0x000BD316
		[__DynamicallyInvokable]
		public virtual string EnglishName
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				return this.m_cultureData.SENGCOUNTRY;
			}
		}

		/// <summary>Gets the full name of the country/region in the language of the localized version of .NET Framework.</summary>
		/// <returns>The full name of the country/region in the language of the localized version of .NET Framework.</returns>
		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06003169 RID: 12649 RVA: 0x000BF123 File Offset: 0x000BD323
		[__DynamicallyInvokable]
		public virtual string DisplayName
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				return this.m_cultureData.SLOCALIZEDCOUNTRY;
			}
		}

		/// <summary>Gets the name of a country/region formatted in the native language of the country/region.</summary>
		/// <returns>The native name of the country/region formatted in the language associated with the ISO 3166 country/region code.</returns>
		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x0600316A RID: 12650 RVA: 0x000BF130 File Offset: 0x000BD330
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public virtual string NativeName
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				return this.m_cultureData.SNATIVECOUNTRY;
			}
		}

		/// <summary>Gets the two-letter code defined in ISO 3166 for the country/region.</summary>
		/// <returns>The two-letter code defined in ISO 3166 for the country/region.</returns>
		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x0600316B RID: 12651 RVA: 0x000BF13D File Offset: 0x000BD33D
		[__DynamicallyInvokable]
		public virtual string TwoLetterISORegionName
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				return this.m_cultureData.SISO3166CTRYNAME;
			}
		}

		/// <summary>Gets the three-letter code defined in ISO 3166 for the country/region.</summary>
		/// <returns>The three-letter code defined in ISO 3166 for the country/region.</returns>
		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x0600316C RID: 12652 RVA: 0x000BF14A File Offset: 0x000BD34A
		public virtual string ThreeLetterISORegionName
		{
			[SecuritySafeCritical]
			get
			{
				return this.m_cultureData.SISO3166CTRYNAME2;
			}
		}

		/// <summary>Gets the three-letter code assigned by Windows to the country/region represented by this <see cref="T:System.Globalization.RegionInfo" />.</summary>
		/// <returns>The three-letter code assigned by Windows to the country/region represented by this <see cref="T:System.Globalization.RegionInfo" />.</returns>
		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x0600316D RID: 12653 RVA: 0x000BF157 File Offset: 0x000BD357
		public virtual string ThreeLetterWindowsRegionName
		{
			[SecuritySafeCritical]
			get
			{
				return this.m_cultureData.SABBREVCTRYNAME;
			}
		}

		/// <summary>Gets a value indicating whether the country/region uses the metric system for measurements.</summary>
		/// <returns>
		///   <see langword="true" /> if the country/region uses the metric system for measurements; otherwise, <see langword="false" />.</returns>
		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x0600316E RID: 12654 RVA: 0x000BF164 File Offset: 0x000BD364
		[__DynamicallyInvokable]
		public virtual bool IsMetric
		{
			[__DynamicallyInvokable]
			get
			{
				int imeasure = this.m_cultureData.IMEASURE;
				return imeasure == 0;
			}
		}

		/// <summary>Gets a unique identification number for a geographical region, country, city, or location.</summary>
		/// <returns>A 32-bit signed number that uniquely identifies a geographical location.</returns>
		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x0600316F RID: 12655 RVA: 0x000BF181 File Offset: 0x000BD381
		[ComVisible(false)]
		public virtual int GeoId
		{
			get
			{
				return this.m_cultureData.IGEOID;
			}
		}

		/// <summary>Gets the name, in English, of the currency used in the country/region.</summary>
		/// <returns>The name, in English, of the currency used in the country/region.</returns>
		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06003170 RID: 12656 RVA: 0x000BF18E File Offset: 0x000BD38E
		[ComVisible(false)]
		public virtual string CurrencyEnglishName
		{
			[SecuritySafeCritical]
			get
			{
				return this.m_cultureData.SENGLISHCURRENCY;
			}
		}

		/// <summary>Gets the name of the currency used in the country/region, formatted in the native language of the country/region.</summary>
		/// <returns>The native name of the currency used in the country/region, formatted in the language associated with the ISO 3166 country/region code.</returns>
		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06003171 RID: 12657 RVA: 0x000BF19B File Offset: 0x000BD39B
		[ComVisible(false)]
		public virtual string CurrencyNativeName
		{
			[SecuritySafeCritical]
			get
			{
				return this.m_cultureData.SNATIVECURRENCY;
			}
		}

		/// <summary>Gets the currency symbol associated with the country/region.</summary>
		/// <returns>The currency symbol associated with the country/region.</returns>
		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06003172 RID: 12658 RVA: 0x000BF1A8 File Offset: 0x000BD3A8
		[__DynamicallyInvokable]
		public virtual string CurrencySymbol
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				return this.m_cultureData.SCURRENCY;
			}
		}

		/// <summary>Gets the three-character ISO 4217 currency symbol associated with the country/region.</summary>
		/// <returns>The three-character ISO 4217 currency symbol associated with the country/region.</returns>
		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06003173 RID: 12659 RVA: 0x000BF1B5 File Offset: 0x000BD3B5
		[__DynamicallyInvokable]
		public virtual string ISOCurrencySymbol
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				return this.m_cultureData.SINTLSYMBOL;
			}
		}

		/// <summary>Determines whether the specified object is the same instance as the current <see cref="T:System.Globalization.RegionInfo" />.</summary>
		/// <param name="value">The object to compare with the current <see cref="T:System.Globalization.RegionInfo" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="value" /> parameter is a <see cref="T:System.Globalization.RegionInfo" /> object and its <see cref="P:System.Globalization.RegionInfo.Name" /> property is the same as the <see cref="P:System.Globalization.RegionInfo.Name" /> property of the current <see cref="T:System.Globalization.RegionInfo" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003174 RID: 12660 RVA: 0x000BF1C4 File Offset: 0x000BD3C4
		[__DynamicallyInvokable]
		public override bool Equals(object value)
		{
			RegionInfo regionInfo = value as RegionInfo;
			return regionInfo != null && this.Name.Equals(regionInfo.Name);
		}

		/// <summary>Serves as a hash function for the current <see cref="T:System.Globalization.RegionInfo" />, suitable for hashing algorithms and data structures, such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Globalization.RegionInfo" />.</returns>
		// Token: 0x06003175 RID: 12661 RVA: 0x000BF1EE File Offset: 0x000BD3EE
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		/// <summary>Returns a string containing the culture name or ISO 3166 two-letter country/region codes specified for the current <see cref="T:System.Globalization.RegionInfo" />.</summary>
		/// <returns>A string containing the culture name or ISO 3166 two-letter country/region codes defined for the current <see cref="T:System.Globalization.RegionInfo" />.</returns>
		// Token: 0x06003176 RID: 12662 RVA: 0x000BF1FB File Offset: 0x000BD3FB
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x0400151B RID: 5403
		internal string m_name;

		// Token: 0x0400151C RID: 5404
		[NonSerialized]
		internal CultureData m_cultureData;

		// Token: 0x0400151D RID: 5405
		internal static volatile RegionInfo s_currentRegionInfo;

		// Token: 0x0400151E RID: 5406
		[OptionalField(VersionAdded = 2)]
		private int m_cultureId;

		// Token: 0x0400151F RID: 5407
		[OptionalField(VersionAdded = 2)]
		internal int m_dataItem;

		// Token: 0x04001520 RID: 5408
		private static readonly int[] IdFromEverettRegionInfoDataItem = new int[]
		{
			14337, 1052, 1067, 11274, 3079, 3081, 1068, 2060, 1026, 15361,
			2110, 16394, 1046, 1059, 10249, 3084, 9225, 2055, 13322, 2052,
			9226, 5130, 1029, 1031, 1030, 7178, 5121, 12298, 1061, 3073,
			1027, 1035, 1080, 1036, 2057, 1079, 1032, 4106, 3076, 18442,
			1050, 1038, 1057, 6153, 1037, 1081, 2049, 1065, 1039, 1040,
			8201, 11265, 1041, 1089, 1088, 1042, 13313, 1087, 12289, 5127,
			1063, 4103, 1062, 4097, 6145, 6156, 1071, 1104, 5124, 1125,
			2058, 1086, 19466, 1043, 1044, 5129, 8193, 6154, 10250, 13321,
			1056, 1045, 20490, 2070, 15370, 16385, 1048, 1049, 1025, 1053,
			4100, 1060, 1051, 2074, 17418, 1114, 1054, 7169, 1055, 11273,
			1028, 1058, 1033, 14346, 1091, 8202, 1066, 9217, 1078, 12297
		};
	}
}
