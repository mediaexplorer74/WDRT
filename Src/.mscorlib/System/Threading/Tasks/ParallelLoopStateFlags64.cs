using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000558 RID: 1368
	internal class ParallelLoopStateFlags64 : ParallelLoopStateFlags
	{
		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06004081 RID: 16513 RVA: 0x000F1C87 File Offset: 0x000EFE87
		internal long LowestBreakIteration
		{
			get
			{
				if (IntPtr.Size >= 8)
				{
					return this.m_lowestBreakIteration;
				}
				return Interlocked.Read(ref this.m_lowestBreakIteration);
			}
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06004082 RID: 16514 RVA: 0x000F1CA4 File Offset: 0x000EFEA4
		internal long? NullableLowestBreakIteration
		{
			get
			{
				if (this.m_lowestBreakIteration == 9223372036854775807L)
				{
					return null;
				}
				if (IntPtr.Size >= 8)
				{
					return new long?(this.m_lowestBreakIteration);
				}
				return new long?(Interlocked.Read(ref this.m_lowestBreakIteration));
			}
		}

		// Token: 0x06004083 RID: 16515 RVA: 0x000F1CF0 File Offset: 0x000EFEF0
		internal bool ShouldExitLoop(long CallerIteration)
		{
			int loopStateFlags = base.LoopStateFlags;
			return loopStateFlags != ParallelLoopStateFlags.PLS_NONE && ((loopStateFlags & (ParallelLoopStateFlags.PLS_EXCEPTIONAL | ParallelLoopStateFlags.PLS_STOPPED | ParallelLoopStateFlags.PLS_CANCELED)) != 0 || ((loopStateFlags & ParallelLoopStateFlags.PLS_BROKEN) != 0 && CallerIteration > this.LowestBreakIteration));
		}

		// Token: 0x06004084 RID: 16516 RVA: 0x000F1D3C File Offset: 0x000EFF3C
		internal bool ShouldExitLoop()
		{
			int loopStateFlags = base.LoopStateFlags;
			return loopStateFlags != ParallelLoopStateFlags.PLS_NONE && (loopStateFlags & (ParallelLoopStateFlags.PLS_EXCEPTIONAL | ParallelLoopStateFlags.PLS_CANCELED)) != 0;
		}

		// Token: 0x04001AF2 RID: 6898
		internal long m_lowestBreakIteration = long.MaxValue;
	}
}
