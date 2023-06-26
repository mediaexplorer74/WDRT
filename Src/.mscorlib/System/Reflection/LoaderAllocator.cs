using System;

namespace System.Reflection
{
	// Token: 0x020005F0 RID: 1520
	internal sealed class LoaderAllocator
	{
		// Token: 0x0600468B RID: 18059 RVA: 0x00103EB8 File Offset: 0x001020B8
		private LoaderAllocator()
		{
			this.m_slots = new object[5];
			this.m_scout = new LoaderAllocatorScout();
		}

		// Token: 0x04001CDC RID: 7388
		private LoaderAllocatorScout m_scout;

		// Token: 0x04001CDD RID: 7389
		private object[] m_slots;

		// Token: 0x04001CDE RID: 7390
		internal CerHashtable<RuntimeMethodInfo, RuntimeMethodInfo> m_methodInstantiations;

		// Token: 0x04001CDF RID: 7391
		private int m_slotsUsed;
	}
}
