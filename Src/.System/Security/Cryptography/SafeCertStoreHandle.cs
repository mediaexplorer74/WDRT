using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x0200045B RID: 1115
	internal sealed class SafeCertStoreHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600296C RID: 10604 RVA: 0x000BC400 File Offset: 0x000BA600
		private SafeCertStoreHandle()
			: base(true)
		{
		}

		// Token: 0x0600296D RID: 10605 RVA: 0x000BC409 File Offset: 0x000BA609
		internal SafeCertStoreHandle(IntPtr handle)
			: base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x0600296E RID: 10606 RVA: 0x000BC41C File Offset: 0x000BA61C
		internal static SafeCertStoreHandle InvalidHandle
		{
			get
			{
				SafeCertStoreHandle safeCertStoreHandle = new SafeCertStoreHandle(IntPtr.Zero);
				GC.SuppressFinalize(safeCertStoreHandle);
				return safeCertStoreHandle;
			}
		}

		// Token: 0x0600296F RID: 10607
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("crypt32.dll", SetLastError = true)]
		private static extern bool CertCloseStore(IntPtr hCertStore, uint dwFlags);

		// Token: 0x06002970 RID: 10608 RVA: 0x000BC43B File Offset: 0x000BA63B
		protected override bool ReleaseHandle()
		{
			return SafeCertStoreHandle.CertCloseStore(this.handle, 0U);
		}
	}
}
