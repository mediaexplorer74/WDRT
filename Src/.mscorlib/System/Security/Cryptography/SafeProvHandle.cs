using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x0200028C RID: 652
	[SecurityCritical]
	internal sealed class SafeProvHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06002330 RID: 9008 RVA: 0x0007FC4A File Offset: 0x0007DE4A
		private SafeProvHandle()
			: base(true)
		{
			base.SetHandle(IntPtr.Zero);
		}

		// Token: 0x06002331 RID: 9009 RVA: 0x0007FC5E File Offset: 0x0007DE5E
		private SafeProvHandle(IntPtr handle)
			: base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06002332 RID: 9010 RVA: 0x0007FC6E File Offset: 0x0007DE6E
		internal static SafeProvHandle InvalidHandle
		{
			get
			{
				return new SafeProvHandle();
			}
		}

		// Token: 0x06002333 RID: 9011
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void FreeCsp(IntPtr pProviderContext);

		// Token: 0x06002334 RID: 9012 RVA: 0x0007FC75 File Offset: 0x0007DE75
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			SafeProvHandle.FreeCsp(this.handle);
			return true;
		}
	}
}
