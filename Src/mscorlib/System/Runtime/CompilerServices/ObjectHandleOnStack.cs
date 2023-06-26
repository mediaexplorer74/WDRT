using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008D2 RID: 2258
	internal struct ObjectHandleOnStack
	{
		// Token: 0x06005DDC RID: 24028 RVA: 0x0014B06A File Offset: 0x0014926A
		internal ObjectHandleOnStack(IntPtr pObject)
		{
			this.m_ptr = pObject;
		}

		// Token: 0x04002A3D RID: 10813
		private IntPtr m_ptr;
	}
}
