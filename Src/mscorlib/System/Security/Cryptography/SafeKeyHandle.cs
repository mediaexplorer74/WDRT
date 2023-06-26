using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x0200028D RID: 653
	[SecurityCritical]
	internal sealed class SafeKeyHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06002335 RID: 9013 RVA: 0x0007FC83 File Offset: 0x0007DE83
		private SafeKeyHandle()
			: base(true)
		{
			base.SetHandle(IntPtr.Zero);
		}

		// Token: 0x06002336 RID: 9014 RVA: 0x0007FC97 File Offset: 0x0007DE97
		private SafeKeyHandle(IntPtr handle)
			: base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06002337 RID: 9015 RVA: 0x0007FCA7 File Offset: 0x0007DEA7
		internal static SafeKeyHandle InvalidHandle
		{
			get
			{
				return new SafeKeyHandle();
			}
		}

		// Token: 0x06002338 RID: 9016
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void FreeKey(IntPtr pKeyCotext);

		// Token: 0x06002339 RID: 9017 RVA: 0x0007FCAE File Offset: 0x0007DEAE
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			SafeKeyHandle.FreeKey(this.handle);
			return true;
		}
	}
}
