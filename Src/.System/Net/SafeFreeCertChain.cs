using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x020001F5 RID: 501
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeFreeCertChain : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06001309 RID: 4873 RVA: 0x00064341 File Offset: 0x00062541
		internal SafeFreeCertChain(IntPtr handle)
			: base(false)
		{
			base.SetHandle(handle);
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x00064351 File Offset: 0x00062551
		internal SafeFreeCertChain(IntPtr handle, bool ownsHandle)
			: base(ownsHandle)
		{
			base.SetHandle(handle);
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x00064364 File Offset: 0x00062564
		public override string ToString()
		{
			return "0x" + base.DangerousGetHandle().ToString("x");
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x0006438E File Offset: 0x0006258E
		protected override bool ReleaseHandle()
		{
			UnsafeNclNativeMethods.SafeNetHandles.CertFreeCertificateChain(this.handle);
			return true;
		}
	}
}
