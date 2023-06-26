using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x0200055F RID: 1375
	internal class ParallelForReplicatingTask : Task
	{
		// Token: 0x06004167 RID: 16743 RVA: 0x000F557C File Offset: 0x000F377C
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal ParallelForReplicatingTask(ParallelOptions parallelOptions, Action action, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions)
			: base(action, null, Task.InternalCurrent, default(CancellationToken), creationOptions, internalOptions | InternalTaskOptions.SelfReplicating, null)
		{
			this.m_replicationDownCount = parallelOptions.EffectiveMaxConcurrencyLevel;
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			base.PossiblyCaptureContext(ref stackCrawlMark);
		}

		// Token: 0x06004168 RID: 16744 RVA: 0x000F55BF File Offset: 0x000F37BF
		internal override bool ShouldReplicate()
		{
			if (this.m_replicationDownCount == -1)
			{
				return true;
			}
			if (this.m_replicationDownCount > 0)
			{
				this.m_replicationDownCount--;
				return true;
			}
			return false;
		}

		// Token: 0x06004169 RID: 16745 RVA: 0x000F55E6 File Offset: 0x000F37E6
		internal override Task CreateReplicaTask(Action<object> taskReplicaDelegate, object stateObject, Task parentTask, TaskScheduler taskScheduler, TaskCreationOptions creationOptionsForReplica, InternalTaskOptions internalOptionsForReplica)
		{
			return new ParallelForReplicaTask(taskReplicaDelegate, stateObject, parentTask, taskScheduler, creationOptionsForReplica, internalOptionsForReplica);
		}

		// Token: 0x04001B2A RID: 6954
		private int m_replicationDownCount;
	}
}
