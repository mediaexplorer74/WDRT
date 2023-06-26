using System;

namespace System.Data.Services.Client
{
	// Token: 0x0200011D RID: 285
	[Flags]
	public enum EntityStates
	{
		// Token: 0x0400057D RID: 1405
		Detached = 1,
		// Token: 0x0400057E RID: 1406
		Unchanged = 2,
		// Token: 0x0400057F RID: 1407
		Added = 4,
		// Token: 0x04000580 RID: 1408
		Deleted = 8,
		// Token: 0x04000581 RID: 1409
		Modified = 16
	}
}
