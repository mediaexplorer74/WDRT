using System;
using System.Security;

namespace System.Net
{
	// Token: 0x020001FC RID: 508
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeFreeCredential_SECURITY : SafeFreeCredentials
	{
		// Token: 0x06001327 RID: 4903 RVA: 0x00064A11 File Offset: 0x00062C11
		protected override bool ReleaseHandle()
		{
			return UnsafeNclNativeMethods.SafeNetHandles_SECURITY.FreeCredentialsHandle(ref this._handle) == 0;
		}
	}
}
