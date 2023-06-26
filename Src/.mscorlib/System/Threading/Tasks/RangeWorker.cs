using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading.Tasks
{
	// Token: 0x02000551 RID: 1361
	[StructLayout(LayoutKind.Auto)]
	internal struct RangeWorker
	{
		// Token: 0x06004057 RID: 16471 RVA: 0x000F153C File Offset: 0x000EF73C
		internal RangeWorker(IndexRange[] ranges, int nInitialRange, long nStep, bool use32BitCurrentIndex)
		{
			this.m_indexRanges = ranges;
			this.m_nCurrentIndexRange = nInitialRange;
			this._use32BitCurrentIndex = use32BitCurrentIndex;
			this.m_nStep = nStep;
			this.m_nIncrementValue = nStep;
			this.m_nMaxIncrementValue = 16L * nStep;
		}

		// Token: 0x06004058 RID: 16472 RVA: 0x000F1570 File Offset: 0x000EF770
		[SecuritySafeCritical]
		internal unsafe bool FindNewWork(out long nFromInclusiveLocal, out long nToExclusiveLocal)
		{
			int num = this.m_indexRanges.Length;
			IndexRange indexRange;
			long num2;
			for (;;)
			{
				indexRange = this.m_indexRanges[this.m_nCurrentIndexRange];
				if (indexRange.m_bRangeFinished == 0)
				{
					if (this.m_indexRanges[this.m_nCurrentIndexRange].m_nSharedCurrentIndexOffset == null)
					{
						Interlocked.CompareExchange<Shared<long>>(ref this.m_indexRanges[this.m_nCurrentIndexRange].m_nSharedCurrentIndexOffset, new Shared<long>(0L), null);
					}
					if (IntPtr.Size == 4 && this._use32BitCurrentIndex)
					{
						fixed (long* ptr = &this.m_indexRanges[this.m_nCurrentIndexRange].m_nSharedCurrentIndexOffset.Value)
						{
							long* ptr2 = ptr;
							num2 = (long)Interlocked.Add(ref *(int*)ptr2, (int)this.m_nIncrementValue) - this.m_nIncrementValue;
						}
					}
					else
					{
						num2 = Interlocked.Add(ref this.m_indexRanges[this.m_nCurrentIndexRange].m_nSharedCurrentIndexOffset.Value, this.m_nIncrementValue) - this.m_nIncrementValue;
					}
					if (indexRange.m_nToExclusive - indexRange.m_nFromInclusive > num2)
					{
						break;
					}
					Interlocked.Exchange(ref this.m_indexRanges[this.m_nCurrentIndexRange].m_bRangeFinished, 1);
				}
				this.m_nCurrentIndexRange = (this.m_nCurrentIndexRange + 1) % this.m_indexRanges.Length;
				num--;
				if (num <= 0)
				{
					goto Block_9;
				}
			}
			nFromInclusiveLocal = indexRange.m_nFromInclusive + num2;
			nToExclusiveLocal = nFromInclusiveLocal + this.m_nIncrementValue;
			if (nToExclusiveLocal > indexRange.m_nToExclusive || nToExclusiveLocal < indexRange.m_nFromInclusive)
			{
				nToExclusiveLocal = indexRange.m_nToExclusive;
			}
			if (this.m_nIncrementValue < this.m_nMaxIncrementValue)
			{
				this.m_nIncrementValue *= 2L;
				if (this.m_nIncrementValue > this.m_nMaxIncrementValue)
				{
					this.m_nIncrementValue = this.m_nMaxIncrementValue;
				}
			}
			return true;
			Block_9:
			nFromInclusiveLocal = 0L;
			nToExclusiveLocal = 0L;
			return false;
		}

		// Token: 0x06004059 RID: 16473 RVA: 0x000F1724 File Offset: 0x000EF924
		internal bool FindNewWork32(out int nFromInclusiveLocal32, out int nToExclusiveLocal32)
		{
			long num;
			long num2;
			bool flag = this.FindNewWork(out num, out num2);
			nFromInclusiveLocal32 = (int)num;
			nToExclusiveLocal32 = (int)num2;
			return flag;
		}

		// Token: 0x04001ADC RID: 6876
		internal readonly IndexRange[] m_indexRanges;

		// Token: 0x04001ADD RID: 6877
		internal int m_nCurrentIndexRange;

		// Token: 0x04001ADE RID: 6878
		internal long m_nStep;

		// Token: 0x04001ADF RID: 6879
		internal long m_nIncrementValue;

		// Token: 0x04001AE0 RID: 6880
		internal readonly long m_nMaxIncrementValue;

		// Token: 0x04001AE1 RID: 6881
		internal readonly bool _use32BitCurrentIndex;
	}
}
