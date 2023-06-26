using System;

namespace System.Threading
{
	// Token: 0x02000544 RID: 1348
	internal struct CancellationCallbackCoreWorkArguments
	{
		// Token: 0x06003F80 RID: 16256 RVA: 0x000ED864 File Offset: 0x000EBA64
		public CancellationCallbackCoreWorkArguments(SparselyPopulatedArrayFragment<CancellationCallbackInfo> currArrayFragment, int currArrayIndex)
		{
			this.m_currArrayFragment = currArrayFragment;
			this.m_currArrayIndex = currArrayIndex;
		}

		// Token: 0x04001AB4 RID: 6836
		internal SparselyPopulatedArrayFragment<CancellationCallbackInfo> m_currArrayFragment;

		// Token: 0x04001AB5 RID: 6837
		internal int m_currArrayIndex;
	}
}
