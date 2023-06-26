using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000554 RID: 1364
	internal class ParallelLoopState32 : ParallelLoopState
	{
		// Token: 0x06004068 RID: 16488 RVA: 0x000F1A14 File Offset: 0x000EFC14
		internal ParallelLoopState32(ParallelLoopStateFlags32 sharedParallelStateFlags)
			: base(sharedParallelStateFlags)
		{
			this.m_sharedParallelStateFlags = sharedParallelStateFlags;
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06004069 RID: 16489 RVA: 0x000F1A24 File Offset: 0x000EFC24
		// (set) Token: 0x0600406A RID: 16490 RVA: 0x000F1A2C File Offset: 0x000EFC2C
		internal int CurrentIteration
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

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x0600406B RID: 16491 RVA: 0x000F1A35 File Offset: 0x000EFC35
		internal override bool InternalShouldExitCurrentIteration
		{
			get
			{
				return this.m_sharedParallelStateFlags.ShouldExitLoop(this.CurrentIteration);
			}
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x0600406C RID: 16492 RVA: 0x000F1A48 File Offset: 0x000EFC48
		internal override long? InternalLowestBreakIteration
		{
			get
			{
				return this.m_sharedParallelStateFlags.NullableLowestBreakIteration;
			}
		}

		// Token: 0x0600406D RID: 16493 RVA: 0x000F1A55 File Offset: 0x000EFC55
		internal override void InternalBreak()
		{
			ParallelLoopState.Break(this.CurrentIteration, this.m_sharedParallelStateFlags);
		}

		// Token: 0x04001AE7 RID: 6887
		private ParallelLoopStateFlags32 m_sharedParallelStateFlags;

		// Token: 0x04001AE8 RID: 6888
		private int m_currentIteration;
	}
}
