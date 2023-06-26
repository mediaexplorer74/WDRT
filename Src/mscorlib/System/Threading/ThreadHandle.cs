using System;

namespace System.Threading
{
	// Token: 0x02000513 RID: 1299
	internal struct ThreadHandle
	{
		// Token: 0x06003D39 RID: 15673 RVA: 0x000E79D4 File Offset: 0x000E5BD4
		internal ThreadHandle(IntPtr pThread)
		{
			this.m_ptr = pThread;
		}

		// Token: 0x040019EF RID: 6639
		private IntPtr m_ptr;
	}
}
