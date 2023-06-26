using System;

namespace Microsoft.Data.OData
{
	// Token: 0x020001DC RID: 476
	public enum ODataBatchReaderState
	{
		// Token: 0x04000519 RID: 1305
		Initial,
		// Token: 0x0400051A RID: 1306
		Operation,
		// Token: 0x0400051B RID: 1307
		ChangesetStart,
		// Token: 0x0400051C RID: 1308
		ChangesetEnd,
		// Token: 0x0400051D RID: 1309
		Completed,
		// Token: 0x0400051E RID: 1310
		Exception
	}
}
