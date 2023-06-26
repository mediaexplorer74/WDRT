using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008D3 RID: 2259
	internal struct StackCrawlMarkHandle
	{
		// Token: 0x06005DDD RID: 24029 RVA: 0x0014B073 File Offset: 0x00149273
		internal StackCrawlMarkHandle(IntPtr stackMark)
		{
			this.m_ptr = stackMark;
		}

		// Token: 0x04002A3E RID: 10814
		private IntPtr m_ptr;
	}
}
