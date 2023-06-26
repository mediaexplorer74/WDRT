using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000560 RID: 1376
	internal class ParallelForReplicaTask : Task
	{
		// Token: 0x0600416A RID: 16746 RVA: 0x000F55F8 File Offset: 0x000F37F8
		internal ParallelForReplicaTask(Action<object> taskReplicaDelegate, object stateObject, Task parentTask, TaskScheduler taskScheduler, TaskCreationOptions creationOptionsForReplica, InternalTaskOptions internalOptionsForReplica)
			: base(taskReplicaDelegate, stateObject, parentTask, default(CancellationToken), creationOptionsForReplica, internalOptionsForReplica, taskScheduler)
		{
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x0600416B RID: 16747 RVA: 0x000F561D File Offset: 0x000F381D
		// (set) Token: 0x0600416C RID: 16748 RVA: 0x000F5625 File Offset: 0x000F3825
		internal override object SavedStateForNextReplica
		{
			get
			{
				return this.m_stateForNextReplica;
			}
			set
			{
				this.m_stateForNextReplica = value;
			}
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x0600416D RID: 16749 RVA: 0x000F562E File Offset: 0x000F382E
		// (set) Token: 0x0600416E RID: 16750 RVA: 0x000F5636 File Offset: 0x000F3836
		internal override object SavedStateFromPreviousReplica
		{
			get
			{
				return this.m_stateFromPreviousReplica;
			}
			set
			{
				this.m_stateFromPreviousReplica = value;
			}
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x0600416F RID: 16751 RVA: 0x000F563F File Offset: 0x000F383F
		// (set) Token: 0x06004170 RID: 16752 RVA: 0x000F5647 File Offset: 0x000F3847
		internal override Task HandedOverChildReplica
		{
			get
			{
				return this.m_handedOverChildReplica;
			}
			set
			{
				this.m_handedOverChildReplica = value;
			}
		}

		// Token: 0x04001B2B RID: 6955
		internal object m_stateForNextReplica;

		// Token: 0x04001B2C RID: 6956
		internal object m_stateFromPreviousReplica;

		// Token: 0x04001B2D RID: 6957
		internal Task m_handedOverChildReplica;
	}
}
