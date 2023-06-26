using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x020001F6 RID: 502
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeFreeCertChainList : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600130D RID: 4877 RVA: 0x0006439C File Offset: 0x0006259C
		internal SafeFreeCertChainList()
			: base(true)
		{
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x000643A8 File Offset: 0x000625A8
		public override string ToString()
		{
			return "0x" + base.DangerousGetHandle().ToString("x");
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x000643D2 File Offset: 0x000625D2
		protected override bool ReleaseHandle()
		{
			UnsafeNclNativeMethods.SafeNetHandles.CertFreeCertificateChainList(this.handle);
			return true;
		}
	}
}
