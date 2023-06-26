using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008D1 RID: 2257
	internal struct StringHandleOnStack
	{
		// Token: 0x06005DDB RID: 24027 RVA: 0x0014B061 File Offset: 0x00149261
		internal StringHandleOnStack(IntPtr pString)
		{
			this.m_ptr = pString;
		}

		// Token: 0x04002A3C RID: 10812
		private IntPtr m_ptr;
	}
}
