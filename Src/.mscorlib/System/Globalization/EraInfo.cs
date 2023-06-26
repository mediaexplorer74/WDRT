using System;
using System.Runtime.Serialization;

namespace System.Globalization
{
	// Token: 0x020003BD RID: 957
	[Serializable]
	internal class EraInfo
	{
		// Token: 0x06002F97 RID: 12183 RVA: 0x000B7F30 File Offset: 0x000B6130
		internal EraInfo(int era, int startYear, int startMonth, int startDay, int yearOffset, int minEraYear, int maxEraYear)
		{
			this.era = era;
			this.yearOffset = yearOffset;
			this.minEraYear = minEraYear;
			this.maxEraYear = maxEraYear;
			this.ticks = new DateTime(startYear, startMonth, startDay).Ticks;
		}

		// Token: 0x06002F98 RID: 12184 RVA: 0x000B7F7C File Offset: 0x000B617C
		internal EraInfo(int era, int startYear, int startMonth, int startDay, int yearOffset, int minEraYear, int maxEraYear, string eraName, string abbrevEraName, string englishEraName)
		{
			this.era = era;
			this.yearOffset = yearOffset;
			this.minEraYear = minEraYear;
			this.maxEraYear = maxEraYear;
			this.ticks = new DateTime(startYear, startMonth, startDay).Ticks;
			this.eraName = eraName;
			this.abbrevEraName = abbrevEraName;
			this.englishEraName = englishEraName;
		}

		// Token: 0x0400143C RID: 5180
		internal int era;

		// Token: 0x0400143D RID: 5181
		internal long ticks;

		// Token: 0x0400143E RID: 5182
		internal int yearOffset;

		// Token: 0x0400143F RID: 5183
		internal int minEraYear;

		// Token: 0x04001440 RID: 5184
		internal int maxEraYear;

		// Token: 0x04001441 RID: 5185
		[OptionalField(VersionAdded = 4)]
		internal string eraName;

		// Token: 0x04001442 RID: 5186
		[OptionalField(VersionAdded = 4)]
		internal string abbrevEraName;

		// Token: 0x04001443 RID: 5187
		[OptionalField(VersionAdded = 4)]
		internal string englishEraName;
	}
}
