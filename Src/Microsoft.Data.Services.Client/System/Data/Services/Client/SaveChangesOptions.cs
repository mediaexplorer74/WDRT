using System;

namespace System.Data.Services.Client
{
	// Token: 0x0200011E RID: 286
	[Flags]
	public enum SaveChangesOptions
	{
		// Token: 0x04000583 RID: 1411
		None = 0,
		// Token: 0x04000584 RID: 1412
		Batch = 1,
		// Token: 0x04000585 RID: 1413
		ContinueOnError = 2,
		// Token: 0x04000586 RID: 1414
		ReplaceOnUpdate = 4,
		// Token: 0x04000587 RID: 1415
		PatchOnUpdate = 8,
		// Token: 0x04000588 RID: 1416
		BatchWithIndependentOperations = 16
	}
}
