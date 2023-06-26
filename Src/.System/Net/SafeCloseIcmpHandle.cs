using System;
using System.Net.NetworkInformation;
using System.Runtime.ConstrainedExecution;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x020001EB RID: 491
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeCloseIcmpHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060012E4 RID: 4836 RVA: 0x00063EC1 File Offset: 0x000620C1
		private SafeCloseIcmpHandle()
			: base(true)
		{
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x00063ECA File Offset: 0x000620CA
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected override bool ReleaseHandle()
		{
			return UnsafeNetInfoNativeMethods.IcmpCloseHandle(this.handle);
		}
	}
}
