using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200002C RID: 44
	[SuppressUnmanagedCodeSecurity]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	internal sealed class SafeEventLogWriteHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000293 RID: 659 RVA: 0x000107DF File Offset: 0x0000E9DF
		internal SafeEventLogWriteHandle()
			: base(true)
		{
		}

		// Token: 0x06000294 RID: 660
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern SafeEventLogWriteHandle RegisterEventSource(string uncServerName, string sourceName);

		// Token: 0x06000295 RID: 661
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool DeregisterEventSource(IntPtr hEventLog);

		// Token: 0x06000296 RID: 662 RVA: 0x000107E8 File Offset: 0x0000E9E8
		protected override bool ReleaseHandle()
		{
			return SafeEventLogWriteHandle.DeregisterEventSource(this.handle);
		}
	}
}
