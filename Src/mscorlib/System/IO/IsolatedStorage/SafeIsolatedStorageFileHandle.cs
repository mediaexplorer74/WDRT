using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.IO.IsolatedStorage
{
	// Token: 0x020001B7 RID: 439
	[SecurityCritical]
	internal sealed class SafeIsolatedStorageFileHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06001BC1 RID: 7105
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void Close(IntPtr file);

		// Token: 0x06001BC2 RID: 7106 RVA: 0x0005F670 File Offset: 0x0005D870
		private SafeIsolatedStorageFileHandle()
			: base(true)
		{
			base.SetHandle(IntPtr.Zero);
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x0005F684 File Offset: 0x0005D884
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			SafeIsolatedStorageFileHandle.Close(this.handle);
			return true;
		}
	}
}
