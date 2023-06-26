using System;

namespace System.Collections.Concurrent
{
	// Token: 0x020004AE RID: 1198
	internal struct VolatileBool
	{
		// Token: 0x060039A5 RID: 14757 RVA: 0x000DDFA7 File Offset: 0x000DC1A7
		public VolatileBool(bool value)
		{
			this.m_value = value;
		}

		// Token: 0x04001933 RID: 6451
		public volatile bool m_value;
	}
}
