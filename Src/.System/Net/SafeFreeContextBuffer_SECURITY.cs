using System;
using System.Security;

namespace System.Net
{
	// Token: 0x020001F0 RID: 496
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeFreeContextBuffer_SECURITY : SafeFreeContextBuffer
	{
		// Token: 0x060012F2 RID: 4850 RVA: 0x000640D8 File Offset: 0x000622D8
		internal SafeFreeContextBuffer_SECURITY()
		{
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x000640E0 File Offset: 0x000622E0
		protected override bool ReleaseHandle()
		{
			return UnsafeNclNativeMethods.SafeNetHandles_SECURITY.FreeContextBuffer(this.handle) == 0;
		}
	}
}
