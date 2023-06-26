using System;
using System.Runtime.ConstrainedExecution;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x020001EC RID: 492
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeInternetHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060012E6 RID: 4838 RVA: 0x00063ED7 File Offset: 0x000620D7
		public SafeInternetHandle()
			: base(true)
		{
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x00063EE0 File Offset: 0x000620E0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected override bool ReleaseHandle()
		{
			return UnsafeNclNativeMethods.WinHttp.WinHttpCloseHandle(this.handle);
		}
	}
}
