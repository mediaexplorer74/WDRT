using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000552 RID: 1362
	internal class RangeManager
	{
		// Token: 0x0600405A RID: 16474 RVA: 0x000F1748 File Offset: 0x000EF948
		internal RangeManager(long nFromInclusive, long nToExclusive, long nStep, int nNumExpectedWorkers)
		{
			this.m_nCurrentIndexRangeToAssign = 0;
			this.m_nStep = nStep;
			if (nNumExpectedWorkers == 1)
			{
				nNumExpectedWorkers = 2;
			}
			ulong num = (ulong)(nToExclusive - nFromInclusive);
			ulong num2 = num / (ulong)((long)nNumExpectedWorkers);
			num2 -= num2 % (ulong)nStep;
			if (num2 == 0UL)
			{
				num2 = (ulong)nStep;
			}
			int num3 = (int)(num / num2);
			if (num % num2 != 0UL)
			{
				num3++;
			}
			long num4 = (long)num2;
			this._use32BitCurrentIndex = IntPtr.Size == 4 && num4 <= 2147483647L;
			this.m_indexRanges = new IndexRange[num3];
			long num5 = nFromInclusive;
			for (int i = 0; i < num3; i++)
			{
				this.m_indexRanges[i].m_nFromInclusive = num5;
				this.m_indexRanges[i].m_nSharedCurrentIndexOffset = null;
				this.m_indexRanges[i].m_bRangeFinished = 0;
				num5 += num4;
				if (num5 < num5 - num4 || num5 > nToExclusive)
				{
					num5 = nToExclusive;
				}
				this.m_indexRanges[i].m_nToExclusive = num5;
			}
		}

		// Token: 0x0600405B RID: 16475 RVA: 0x000F1838 File Offset: 0x000EFA38
		internal RangeWorker RegisterNewWorker()
		{
			int num = (Interlocked.Increment(ref this.m_nCurrentIndexRangeToAssign) - 1) % this.m_indexRanges.Length;
			return new RangeWorker(this.m_indexRanges, num, this.m_nStep, this._use32BitCurrentIndex);
		}

		// Token: 0x04001AE2 RID: 6882
		internal readonly IndexRange[] m_indexRanges;

		// Token: 0x04001AE3 RID: 6883
		internal readonly bool _use32BitCurrentIndex;

		// Token: 0x04001AE4 RID: 6884
		internal int m_nCurrentIndexRangeToAssign;

		// Token: 0x04001AE5 RID: 6885
		internal long m_nStep;
	}
}
