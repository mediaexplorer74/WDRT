using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000557 RID: 1367
	internal class ParallelLoopStateFlags32 : ParallelLoopStateFlags
	{
		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x0600407C RID: 16508 RVA: 0x000F1BA2 File Offset: 0x000EFDA2
		internal int LowestBreakIteration
		{
			get
			{
				return this.m_lowestBreakIteration;
			}
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x0600407D RID: 16509 RVA: 0x000F1BAC File Offset: 0x000EFDAC
		internal long? NullableLowestBreakIteration
		{
			get
			{
				if (this.m_lowestBreakIteration == 2147483647)
				{
					return null;
				}
				long num = (long)this.m_lowestBreakIteration;
				if (IntPtr.Size >= 8)
				{
					return new long?(num);
				}
				return new long?(Interlocked.Read(ref num));
			}
		}

		// Token: 0x0600407E RID: 16510 RVA: 0x000F1BF8 File Offset: 0x000EFDF8
		internal bool ShouldExitLoop(int CallerIteration)
		{
			int loopStateFlags = base.LoopStateFlags;
			return loopStateFlags != ParallelLoopStateFlags.PLS_NONE && ((loopStateFlags & (ParallelLoopStateFlags.PLS_EXCEPTIONAL | ParallelLoopStateFlags.PLS_STOPPED | ParallelLoopStateFlags.PLS_CANCELED)) != 0 || ((loopStateFlags & ParallelLoopStateFlags.PLS_BROKEN) != 0 && CallerIteration > this.LowestBreakIteration));
		}

		// Token: 0x0600407F RID: 16511 RVA: 0x000F1C44 File Offset: 0x000EFE44
		internal bool ShouldExitLoop()
		{
			int loopStateFlags = base.LoopStateFlags;
			return loopStateFlags != ParallelLoopStateFlags.PLS_NONE && (loopStateFlags & (ParallelLoopStateFlags.PLS_EXCEPTIONAL | ParallelLoopStateFlags.PLS_CANCELED)) != 0;
		}

		// Token: 0x04001AF1 RID: 6897
		internal volatile int m_lowestBreakIteration = int.MaxValue;
	}
}
