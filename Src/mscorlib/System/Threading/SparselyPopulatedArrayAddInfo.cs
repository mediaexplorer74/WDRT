using System;

namespace System.Threading
{
	// Token: 0x02000547 RID: 1351
	internal struct SparselyPopulatedArrayAddInfo<T> where T : class
	{
		// Token: 0x06003F87 RID: 16263 RVA: 0x000EDAB6 File Offset: 0x000EBCB6
		internal SparselyPopulatedArrayAddInfo(SparselyPopulatedArrayFragment<T> source, int index)
		{
			this.m_source = source;
			this.m_index = index;
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06003F88 RID: 16264 RVA: 0x000EDAC6 File Offset: 0x000EBCC6
		internal SparselyPopulatedArrayFragment<T> Source
		{
			get
			{
				return this.m_source;
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06003F89 RID: 16265 RVA: 0x000EDACE File Offset: 0x000EBCCE
		internal int Index
		{
			get
			{
				return this.m_index;
			}
		}

		// Token: 0x04001ABE RID: 6846
		private SparselyPopulatedArrayFragment<T> m_source;

		// Token: 0x04001ABF RID: 6847
		private int m_index;
	}
}
