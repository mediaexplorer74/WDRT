using System;

namespace Microsoft.WindowsDeviceRecoveryTool.Styles.Assets
{
	// Token: 0x0200001A RID: 26
	public class Location
	{
		// Token: 0x06000089 RID: 137 RVA: 0x000038A5 File Offset: 0x00001AA5
		public Location(string countryNativeName, string countryEnglishName, string ietfLanguageTag, int geoId)
		{
			this.CountryNativeName = countryNativeName;
			this.CountryEnglishName = countryEnglishName;
			this.IetfLanguageTag = ietfLanguageTag;
			this.GeoId = geoId;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000038D0 File Offset: 0x00001AD0
		// (set) Token: 0x0600008B RID: 139 RVA: 0x0000391C File Offset: 0x00001B1C
		public string CountryNativeName
		{
			get
			{
				bool flag = this.countryNativeName == this.CountryEnglishName;
				string text;
				if (flag)
				{
					text = this.countryNativeName;
				}
				else
				{
					text = this.countryNativeName + " (" + this.CountryEnglishName + ")";
				}
				return text;
			}
			private set
			{
				this.countryNativeName = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00003926 File Offset: 0x00001B26
		// (set) Token: 0x0600008D RID: 141 RVA: 0x0000392E File Offset: 0x00001B2E
		public string CountryEnglishName { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003937 File Offset: 0x00001B37
		// (set) Token: 0x0600008F RID: 143 RVA: 0x0000393F File Offset: 0x00001B3F
		public string IetfLanguageTag { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003948 File Offset: 0x00001B48
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00003950 File Offset: 0x00001B50
		public int GeoId { get; private set; }

		// Token: 0x04000020 RID: 32
		private string countryNativeName;
	}
}
