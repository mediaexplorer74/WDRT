using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000117 RID: 279
	internal struct InterlockedGate
	{
		// Token: 0x06000B0F RID: 2831 RVA: 0x0003D0B8 File Offset: 0x0003B2B8
		internal void Reset()
		{
			this.m_State = 0;
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0003D0C4 File Offset: 0x0003B2C4
		internal bool Trigger(bool exclusive)
		{
			int num = Interlocked.CompareExchange(ref this.m_State, 2, 0);
			if (exclusive && (num == 1 || num == 2))
			{
				throw new InternalException();
			}
			return num == 0;
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0003D0F4 File Offset: 0x0003B2F4
		internal bool StartTriggering(bool exclusive)
		{
			int num = Interlocked.CompareExchange(ref this.m_State, 1, 0);
			if (exclusive && (num == 1 || num == 2))
			{
				throw new InternalException();
			}
			return num == 0;
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0003D124 File Offset: 0x0003B324
		internal void FinishTriggering()
		{
			int num = Interlocked.CompareExchange(ref this.m_State, 2, 1);
			if (num != 1)
			{
				throw new InternalException();
			}
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0003D14C File Offset: 0x0003B34C
		internal bool StartSignaling(bool exclusive)
		{
			int num = Interlocked.CompareExchange(ref this.m_State, 3, 2);
			if (exclusive && (num == 3 || num == 4))
			{
				throw new InternalException();
			}
			return num == 2;
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0003D17C File Offset: 0x0003B37C
		internal void FinishSignaling()
		{
			int num = Interlocked.CompareExchange(ref this.m_State, 4, 3);
			if (num != 3)
			{
				throw new InternalException();
			}
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0003D1A4 File Offset: 0x0003B3A4
		internal bool Complete()
		{
			int num = Interlocked.CompareExchange(ref this.m_State, 5, 4);
			return num == 4;
		}

		// Token: 0x04000F5A RID: 3930
		private int m_State;

		// Token: 0x04000F5B RID: 3931
		internal const int Open = 0;

		// Token: 0x04000F5C RID: 3932
		internal const int Triggering = 1;

		// Token: 0x04000F5D RID: 3933
		internal const int Triggered = 2;

		// Token: 0x04000F5E RID: 3934
		internal const int Signaling = 3;

		// Token: 0x04000F5F RID: 3935
		internal const int Signaled = 4;

		// Token: 0x04000F60 RID: 3936
		internal const int Completed = 5;
	}
}
