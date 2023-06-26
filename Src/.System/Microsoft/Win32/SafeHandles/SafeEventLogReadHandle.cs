using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200002B RID: 43
	[SuppressUnmanagedCodeSecurity]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	internal sealed class SafeEventLogReadHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600028F RID: 655 RVA: 0x000107C9 File Offset: 0x0000E9C9
		internal SafeEventLogReadHandle()
			: base(true)
		{
		}

		// Token: 0x06000290 RID: 656
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern SafeEventLogReadHandle OpenEventLog(string UNCServerName, string sourceName);

		// Token: 0x06000291 RID: 657
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool CloseEventLog(IntPtr hEventLog);

		// Token: 0x06000292 RID: 658 RVA: 0x000107D2 File Offset: 0x0000E9D2
		protected override bool ReleaseHandle()
		{
			return SafeEventLogReadHandle.CloseEventLog(this.handle);
		}
	}
}
