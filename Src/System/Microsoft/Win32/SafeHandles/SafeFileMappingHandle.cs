using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200002D RID: 45
	[SuppressUnmanagedCodeSecurity]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	internal sealed class SafeFileMappingHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000297 RID: 663 RVA: 0x000107F5 File Offset: 0x0000E9F5
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal SafeFileMappingHandle()
			: base(true)
		{
		}

		// Token: 0x06000298 RID: 664
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		private static extern bool CloseHandle(IntPtr handle);

		// Token: 0x06000299 RID: 665 RVA: 0x000107FE File Offset: 0x0000E9FE
		protected override bool ReleaseHandle()
		{
			return SafeFileMappingHandle.CloseHandle(this.handle);
		}
	}
}
