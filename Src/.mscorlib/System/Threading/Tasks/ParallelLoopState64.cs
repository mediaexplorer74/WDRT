using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000555 RID: 1365
	internal class ParallelLoopState64 : ParallelLoopState
	{
		// Token: 0x0600406E RID: 16494 RVA: 0x000F1A68 File Offset: 0x000EFC68
		internal ParallelLoopState64(ParallelLoopStateFlags64 sharedParallelStateFlags)
			: base(sharedParallelStateFlags)
		{
			this.m_sharedParallelStateFlags = sharedParallelStateFlags;
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x0600406F RID: 16495 RVA: 0x000F1A78 File Offset: 0x000EFC78
		// (set) Token: 0x06004070 RID: 16496 RVA: 0x000F1A80 File Offset: 0x000EFC80
		internal long CurrentIteration
		{
			get
			{
				return this.m_currentIteration;
			}
			set
			{
				this.m_currentIteration = value;
			}
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06004071 RID: 16497 RVA: 0x000F1A89 File Offset: 0x000EFC89
		internal override bool InternalShouldExitCurrentIteration
		{
			get
			{
				return this.m_sharedParallelStateFlags.ShouldExitLoop(this.CurrentIteration);
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06004072 RID: 16498 RVA: 0x000F1A9C File Offset: 0x000EFC9C
		internal override long? InternalLowestBreakIteration
		{
			get
			{
				return this.m_sharedParallelStateFlags.NullableLowestBreakIteration;
			}
		}

		// Token: 0x06004073 RID: 16499 RVA: 0x000F1AA9 File Offset: 0x000EFCA9
		internal override void InternalBreak()
		{
			ParallelLoopState.Break(this.CurrentIteration, this.m_sharedParallelStateFlags);
		}

		// Token: 0x04001AE9 RID: 6889
		private ParallelLoopStateFlags64 m_sharedParallelStateFlags;

		// Token: 0x04001AEA RID: 6890
		private long m_currentIteration;
	}
}
