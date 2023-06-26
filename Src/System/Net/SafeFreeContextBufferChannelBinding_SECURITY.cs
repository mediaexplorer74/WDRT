using System;
using System.Security;

namespace System.Net
{
	// Token: 0x02000204 RID: 516
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeFreeContextBufferChannelBinding_SECURITY : SafeFreeContextBufferChannelBinding
	{
		// Token: 0x06001355 RID: 4949 RVA: 0x00065E7C File Offset: 0x0006407C
		protected override bool ReleaseHandle()
		{
			return UnsafeNclNativeMethods.SafeNetHandles_SECURITY.FreeContextBuffer(this.handle) == 0;
		}
	}
}
