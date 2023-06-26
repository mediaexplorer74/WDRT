using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200002F RID: 47
	[SuppressUnmanagedCodeSecurity]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	internal sealed class SafeLibraryHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600029E RID: 670 RVA: 0x00010821 File Offset: 0x0000EA21
		internal SafeLibraryHandle()
			: base(true)
		{
		}

		// Token: 0x0600029F RID: 671
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern SafeLibraryHandle LoadLibraryEx(string libFilename, IntPtr reserved, int flags);

		// Token: 0x060002A0 RID: 672
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		private static extern bool FreeLibrary(IntPtr hModule);

		// Token: 0x060002A1 RID: 673 RVA: 0x0001082A File Offset: 0x0000EA2A
		protected override bool ReleaseHandle()
		{
			return SafeLibraryHandle.FreeLibrary(this.handle);
		}
	}
}
