using System;
using System.Runtime.ConstrainedExecution;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x020001F8 RID: 504
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeFreeCertContext : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06001316 RID: 4886 RVA: 0x00064624 File Offset: 0x00062824
		internal SafeFreeCertContext()
			: base(true)
		{
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x0006462D File Offset: 0x0006282D
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal void Set(IntPtr value)
		{
			this.handle = value;
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x00064636 File Offset: 0x00062836
		protected override bool ReleaseHandle()
		{
			UnsafeNclNativeMethods.SafeNetHandles.CertFreeCertificateContext(this.handle);
			return true;
		}

		// Token: 0x04001537 RID: 5431
		private const string CRYPT32 = "crypt32.dll";

		// Token: 0x04001538 RID: 5432
		private const string ADVAPI32 = "advapi32.dll";

		// Token: 0x04001539 RID: 5433
		private const uint CRYPT_ACQUIRE_SILENT_FLAG = 64U;
	}
}
