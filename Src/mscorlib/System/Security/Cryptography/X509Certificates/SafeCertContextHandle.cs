using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002A5 RID: 677
	[SecurityCritical]
	internal sealed class SafeCertContextHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06002419 RID: 9241 RVA: 0x00083B7A File Offset: 0x00081D7A
		private SafeCertContextHandle()
			: base(true)
		{
		}

		// Token: 0x0600241A RID: 9242 RVA: 0x00083B83 File Offset: 0x00081D83
		internal SafeCertContextHandle(IntPtr handle)
			: base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x0600241B RID: 9243 RVA: 0x00083B94 File Offset: 0x00081D94
		internal static SafeCertContextHandle InvalidHandle
		{
			get
			{
				SafeCertContextHandle safeCertContextHandle = new SafeCertContextHandle(IntPtr.Zero);
				GC.SuppressFinalize(safeCertContextHandle);
				return safeCertContextHandle;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x0600241C RID: 9244 RVA: 0x00083BB3 File Offset: 0x00081DB3
		internal IntPtr pCertContext
		{
			get
			{
				if (this.handle == IntPtr.Zero)
				{
					return IntPtr.Zero;
				}
				return Marshal.ReadIntPtr(this.handle);
			}
		}

		// Token: 0x0600241D RID: 9245
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _FreePCertContext(IntPtr pCert);

		// Token: 0x0600241E RID: 9246 RVA: 0x00083BD8 File Offset: 0x00081DD8
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			SafeCertContextHandle._FreePCertContext(this.handle);
			return true;
		}
	}
}
