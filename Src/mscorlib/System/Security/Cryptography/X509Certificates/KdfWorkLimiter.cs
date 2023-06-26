using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002B9 RID: 697
	internal static class KdfWorkLimiter
	{
		// Token: 0x06002510 RID: 9488 RVA: 0x00087380 File Offset: 0x00085580
		internal static void SetIterationLimit(ulong workLimit)
		{
			KdfWorkLimiter.t_State = new KdfWorkLimiter.State
			{
				RemainingAllowedWork = workLimit
			};
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x000873A0 File Offset: 0x000855A0
		internal static bool WasWorkLimitExceeded()
		{
			return KdfWorkLimiter.t_State.WorkLimitWasExceeded;
		}

		// Token: 0x06002512 RID: 9490 RVA: 0x000873AC File Offset: 0x000855AC
		internal static void ResetIterationLimit()
		{
			KdfWorkLimiter.t_State = null;
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x000873B4 File Offset: 0x000855B4
		internal static void RecordIterations(int workCount)
		{
			KdfWorkLimiter.RecordIterations((long)workCount);
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x000873C0 File Offset: 0x000855C0
		internal static void RecordIterations(long workCount)
		{
			KdfWorkLimiter.State state = KdfWorkLimiter.t_State;
			bool flag = false;
			checked
			{
				try
				{
					if (!state.WorkLimitWasExceeded)
					{
						state.RemainingAllowedWork -= (ulong)workCount;
						flag = true;
					}
				}
				finally
				{
					if (!flag)
					{
						state.RemainingAllowedWork = 0UL;
						state.WorkLimitWasExceeded = true;
						throw new CryptographicException();
					}
				}
			}
		}

		// Token: 0x04000DD5 RID: 3541
		[ThreadStatic]
		private static KdfWorkLimiter.State t_State;

		// Token: 0x02000B4A RID: 2890
		private sealed class State
		{
			// Token: 0x040033DA RID: 13274
			internal ulong RemainingAllowedWork;

			// Token: 0x040033DB RID: 13275
			internal bool WorkLimitWasExceeded;
		}
	}
}
