using System;

namespace System.Reflection.Emit
{
	// Token: 0x02000626 RID: 1574
	[Flags]
	internal enum DynamicAssemblyFlags
	{
		// Token: 0x04001E4B RID: 7755
		None = 0,
		// Token: 0x04001E4C RID: 7756
		AllCritical = 1,
		// Token: 0x04001E4D RID: 7757
		Aptca = 2,
		// Token: 0x04001E4E RID: 7758
		Critical = 4,
		// Token: 0x04001E4F RID: 7759
		Transparent = 8,
		// Token: 0x04001E50 RID: 7760
		TreatAsSafe = 16
	}
}
