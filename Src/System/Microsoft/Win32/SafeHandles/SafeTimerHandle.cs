using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000032 RID: 50
	[SuppressUnmanagedCodeSecurity]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	internal sealed class SafeTimerHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060002AE RID: 686 RVA: 0x000108B7 File Offset: 0x0000EAB7
		internal SafeTimerHandle()
			: base(true)
		{
		}

		// Token: 0x060002AF RID: 687
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		private static extern bool CloseHandle(IntPtr handle);

		// Token: 0x060002B0 RID: 688 RVA: 0x000108C0 File Offset: 0x0000EAC0
		protected override bool ReleaseHandle()
		{
			return SafeTimerHandle.CloseHandle(this.handle);
		}
	}
}
