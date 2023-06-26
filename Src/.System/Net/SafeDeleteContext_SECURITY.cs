using System;
using System.Security;

namespace System.Net
{
	// Token: 0x020001FE RID: 510
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeDeleteContext_SECURITY : SafeDeleteContext
	{
		// Token: 0x06001332 RID: 4914 RVA: 0x00065975 File Offset: 0x00063B75
		internal SafeDeleteContext_SECURITY()
		{
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x0006597D File Offset: 0x00063B7D
		protected override bool ReleaseHandle()
		{
			if (this._EffectiveCredential != null)
			{
				this._EffectiveCredential.DangerousRelease();
			}
			return UnsafeNclNativeMethods.SafeNetHandles_SECURITY.DeleteSecurityContext(ref this._handle) == 0;
		}
	}
}
