using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002A6 RID: 678
	[SecurityCritical]
	internal sealed class SafeCertStoreHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600241F RID: 9247 RVA: 0x00083BE6 File Offset: 0x00081DE6
		private SafeCertStoreHandle()
			: base(true)
		{
		}

		// Token: 0x06002420 RID: 9248 RVA: 0x00083BEF File Offset: 0x00081DEF
		internal SafeCertStoreHandle(IntPtr handle)
			: base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06002421 RID: 9249 RVA: 0x00083C00 File Offset: 0x00081E00
		internal static SafeCertStoreHandle InvalidHandle
		{
			get
			{
				SafeCertStoreHandle safeCertStoreHandle = new SafeCertStoreHandle(IntPtr.Zero);
				GC.SuppressFinalize(safeCertStoreHandle);
				return safeCertStoreHandle;
			}
		}

		// Token: 0x06002422 RID: 9250
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _FreeCertStoreContext(IntPtr hCertStore);

		// Token: 0x06002423 RID: 9251 RVA: 0x00083C1F File Offset: 0x00081E1F
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			SafeCertStoreHandle._FreeCertStoreContext(this.handle);
			return true;
		}
	}
}
