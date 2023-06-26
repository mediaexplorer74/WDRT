using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x0200028E RID: 654
	[SecurityCritical]
	internal sealed class SafeHashHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600233A RID: 9018 RVA: 0x0007FCBC File Offset: 0x0007DEBC
		private SafeHashHandle()
			: base(true)
		{
			base.SetHandle(IntPtr.Zero);
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x0007FCD0 File Offset: 0x0007DED0
		private SafeHashHandle(IntPtr handle)
			: base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x0600233C RID: 9020 RVA: 0x0007FCE0 File Offset: 0x0007DEE0
		internal static SafeHashHandle InvalidHandle
		{
			get
			{
				return new SafeHashHandle();
			}
		}

		// Token: 0x0600233D RID: 9021
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void FreeHash(IntPtr pHashContext);

		// Token: 0x0600233E RID: 9022 RVA: 0x0007FCE7 File Offset: 0x0007DEE7
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			SafeHashHandle.FreeHash(this.handle);
			return true;
		}
	}
}
