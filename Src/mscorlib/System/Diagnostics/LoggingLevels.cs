using System;

namespace System.Diagnostics
{
	// Token: 0x020003F3 RID: 1011
	[Serializable]
	internal enum LoggingLevels
	{
		// Token: 0x040016C4 RID: 5828
		TraceLevel0,
		// Token: 0x040016C5 RID: 5829
		TraceLevel1,
		// Token: 0x040016C6 RID: 5830
		TraceLevel2,
		// Token: 0x040016C7 RID: 5831
		TraceLevel3,
		// Token: 0x040016C8 RID: 5832
		TraceLevel4,
		// Token: 0x040016C9 RID: 5833
		StatusLevel0 = 20,
		// Token: 0x040016CA RID: 5834
		StatusLevel1,
		// Token: 0x040016CB RID: 5835
		StatusLevel2,
		// Token: 0x040016CC RID: 5836
		StatusLevel3,
		// Token: 0x040016CD RID: 5837
		StatusLevel4,
		// Token: 0x040016CE RID: 5838
		WarningLevel = 40,
		// Token: 0x040016CF RID: 5839
		ErrorLevel = 50,
		// Token: 0x040016D0 RID: 5840
		PanicLevel = 100
	}
}
