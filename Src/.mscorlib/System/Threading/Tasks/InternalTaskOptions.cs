using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000562 RID: 1378
	[Flags]
	[Serializable]
	internal enum InternalTaskOptions
	{
		// Token: 0x04001B37 RID: 6967
		None = 0,
		// Token: 0x04001B38 RID: 6968
		InternalOptionsMask = 65280,
		// Token: 0x04001B39 RID: 6969
		ChildReplica = 256,
		// Token: 0x04001B3A RID: 6970
		ContinuationTask = 512,
		// Token: 0x04001B3B RID: 6971
		PromiseTask = 1024,
		// Token: 0x04001B3C RID: 6972
		SelfReplicating = 2048,
		// Token: 0x04001B3D RID: 6973
		LazyCancellation = 4096,
		// Token: 0x04001B3E RID: 6974
		QueuedByRuntime = 8192,
		// Token: 0x04001B3F RID: 6975
		DoNotDispose = 16384
	}
}
