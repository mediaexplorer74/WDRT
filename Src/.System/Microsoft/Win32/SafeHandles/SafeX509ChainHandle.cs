using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
	/// <summary>Provides a wrapper class that represents the handle of an X.509 chain object. For more information, see <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" />.</summary>
	// Token: 0x02000035 RID: 53
	[SecurityCritical]
	public sealed class SafeX509ChainHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060002B9 RID: 697 RVA: 0x00010912 File Offset: 0x0000EB12
		private SafeX509ChainHandle()
			: base(true)
		{
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0001091B File Offset: 0x0000EB1B
		internal SafeX509ChainHandle(IntPtr handle)
			: base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0001092C File Offset: 0x0000EB2C
		internal static SafeX509ChainHandle InvalidHandle
		{
			get
			{
				SafeX509ChainHandle safeX509ChainHandle = new SafeX509ChainHandle(IntPtr.Zero);
				GC.SuppressFinalize(safeX509ChainHandle);
				return safeX509ChainHandle;
			}
		}

		// Token: 0x060002BC RID: 700
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("crypt32.dll", SetLastError = true)]
		private static extern void CertFreeCertificateChain(IntPtr handle);

		// Token: 0x060002BD RID: 701 RVA: 0x0001094B File Offset: 0x0000EB4B
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			SafeX509ChainHandle.CertFreeCertificateChain(this.handle);
			return true;
		}
	}
}
