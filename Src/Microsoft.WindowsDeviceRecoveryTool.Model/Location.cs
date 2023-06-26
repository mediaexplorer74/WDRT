using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Model
{
	// Token: 0x02000016 RID: 22
	internal class Location
	{
		// Token: 0x06000153 RID: 339 RVA: 0x000056F8 File Offset: 0x000038F8
		public Location(string countryEnglishName, string ietfLanguageTag, int geoId)
		{
			this.CountryEnglishName = countryEnglishName;
			this.IetfLanguageTag = ietfLanguageTag;
			this.GeoId = geoId;
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000154 RID: 340 RVA: 0x0000571A File Offset: 0x0000391A
		// (set) Token: 0x06000155 RID: 341 RVA: 0x00005722 File Offset: 0x00003922
		public string CountryEnglishName { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000156 RID: 342 RVA: 0x0000572B File Offset: 0x0000392B
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00005733 File Offset: 0x00003933
		public string IetfLanguageTag { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000158 RID: 344 RVA: 0x0000573C File Offset: 0x0000393C
		// (set) Token: 0x06000159 RID: 345 RVA: 0x00005744 File Offset: 0x00003944
		public int GeoId { get; set; }
	}
}
