using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000556 RID: 1366
	internal class ParallelLoopStateFlags
	{
		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06004074 RID: 16500 RVA: 0x000F1ABC File Offset: 0x000EFCBC
		internal int LoopStateFlags
		{
			get
			{
				return this.m_LoopStateFlags;
			}
		}

		// Token: 0x06004075 RID: 16501 RVA: 0x000F1AC8 File Offset: 0x000EFCC8
		internal bool AtomicLoopStateUpdate(int newState, int illegalStates)
		{
			int num = 0;
			return this.AtomicLoopStateUpdate(newState, illegalStates, ref num);
		}

		// Token: 0x06004076 RID: 16502 RVA: 0x000F1AE4 File Offset: 0x000EFCE4
		internal bool AtomicLoopStateUpdate(int newState, int illegalStates, ref int oldState)
		{
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				oldState = this.m_LoopStateFlags;
				if ((oldState & illegalStates) != 0)
				{
					break;
				}
				if (Interlocked.CompareExchange(ref this.m_LoopStateFlags, oldState | newState, oldState) == oldState)
				{
					return true;
				}
				spinWait.SpinOnce();
			}
			return false;
		}

		// Token: 0x06004077 RID: 16503 RVA: 0x000F1B2A File Offset: 0x000EFD2A
		internal void SetExceptional()
		{
			this.AtomicLoopStateUpdate(ParallelLoopStateFlags.PLS_EXCEPTIONAL, ParallelLoopStateFlags.PLS_NONE);
		}

		// Token: 0x06004078 RID: 16504 RVA: 0x000F1B3D File Offset: 0x000EFD3D
		internal void Stop()
		{
			if (!this.AtomicLoopStateUpdate(ParallelLoopStateFlags.PLS_STOPPED, ParallelLoopStateFlags.PLS_BROKEN))
			{
				throw new InvalidOperationException(Environment.GetResourceString("ParallelState_Stop_InvalidOperationException_StopAfterBreak"));
			}
		}

		// Token: 0x06004079 RID: 16505 RVA: 0x000F1B61 File Offset: 0x000EFD61
		internal bool Cancel()
		{
			return this.AtomicLoopStateUpdate(ParallelLoopStateFlags.PLS_CANCELED, ParallelLoopStateFlags.PLS_NONE);
		}

		// Token: 0x04001AEB RID: 6891
		internal static int PLS_NONE;

		// Token: 0x04001AEC RID: 6892
		internal static int PLS_EXCEPTIONAL = 1;

		// Token: 0x04001AED RID: 6893
		internal static int PLS_BROKEN = 2;

		// Token: 0x04001AEE RID: 6894
		internal static int PLS_STOPPED = 4;

		// Token: 0x04001AEF RID: 6895
		internal static int PLS_CANCELED = 8;

		// Token: 0x04001AF0 RID: 6896
		private volatile int m_LoopStateFlags = ParallelLoopStateFlags.PLS_NONE;
	}
}
