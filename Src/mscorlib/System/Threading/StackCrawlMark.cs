using System;

namespace System.Threading
{
	// Token: 0x02000515 RID: 1301
	[Serializable]
	internal enum StackCrawlMark
	{
		// Token: 0x04001A04 RID: 6660
		LookForMe,
		// Token: 0x04001A05 RID: 6661
		LookForMyCaller,
		// Token: 0x04001A06 RID: 6662
		LookForMyCallersCaller,
		// Token: 0x04001A07 RID: 6663
		LookForThread
	}
}
