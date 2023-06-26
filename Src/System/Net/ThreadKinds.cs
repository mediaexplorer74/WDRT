using System;

namespace System.Net
{
	// Token: 0x020001C2 RID: 450
	[Flags]
	internal enum ThreadKinds
	{
		// Token: 0x04001456 RID: 5206
		Unknown = 0,
		// Token: 0x04001457 RID: 5207
		User = 1,
		// Token: 0x04001458 RID: 5208
		System = 2,
		// Token: 0x04001459 RID: 5209
		Sync = 4,
		// Token: 0x0400145A RID: 5210
		Async = 8,
		// Token: 0x0400145B RID: 5211
		Timer = 16,
		// Token: 0x0400145C RID: 5212
		CompletionPort = 32,
		// Token: 0x0400145D RID: 5213
		Worker = 64,
		// Token: 0x0400145E RID: 5214
		Finalization = 128,
		// Token: 0x0400145F RID: 5215
		Other = 256,
		// Token: 0x04001460 RID: 5216
		OwnerMask = 3,
		// Token: 0x04001461 RID: 5217
		SyncMask = 12,
		// Token: 0x04001462 RID: 5218
		SourceMask = 496,
		// Token: 0x04001463 RID: 5219
		SafeSources = 352,
		// Token: 0x04001464 RID: 5220
		ThreadPool = 96
	}
}
