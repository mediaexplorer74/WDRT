using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.StubHelpers
{
	// Token: 0x020005A8 RID: 1448
	[SecurityCritical]
	internal sealed class CleanupWorkListElement
	{
		// Token: 0x06004345 RID: 17221 RVA: 0x000FBABD File Offset: 0x000F9CBD
		public CleanupWorkListElement(SafeHandle handle)
		{
			this.m_handle = handle;
		}

		// Token: 0x04001BF3 RID: 7155
		public SafeHandle m_handle;

		// Token: 0x04001BF4 RID: 7156
		public bool m_owned;
	}
}
