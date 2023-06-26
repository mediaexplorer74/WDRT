using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x0200045A RID: 1114
	internal sealed class SafeCertContextHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06002967 RID: 10599 RVA: 0x000BC3B9 File Offset: 0x000BA5B9
		private SafeCertContextHandle()
			: base(true)
		{
		}

		// Token: 0x06002968 RID: 10600 RVA: 0x000BC3C2 File Offset: 0x000BA5C2
		internal SafeCertContextHandle(IntPtr handle)
			: base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06002969 RID: 10601 RVA: 0x000BC3D4 File Offset: 0x000BA5D4
		internal static SafeCertContextHandle InvalidHandle
		{
			get
			{
				SafeCertContextHandle safeCertContextHandle = new SafeCertContextHandle(IntPtr.Zero);
				GC.SuppressFinalize(safeCertContextHandle);
				return safeCertContextHandle;
			}
		}

		// Token: 0x0600296A RID: 10602
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("crypt32.dll", SetLastError = true)]
		private static extern bool CertFreeCertificateContext(IntPtr pCertContext);

		// Token: 0x0600296B RID: 10603 RVA: 0x000BC3F3 File Offset: 0x000BA5F3
		protected override bool ReleaseHandle()
		{
			return SafeCertContextHandle.CertFreeCertificateContext(this.handle);
		}
	}
}
