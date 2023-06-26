using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000034 RID: 52
	[SuppressUnmanagedCodeSecurity]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	internal sealed class SafeUserTokenHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060002B4 RID: 692 RVA: 0x000108EC File Offset: 0x0000EAEC
		internal SafeUserTokenHandle()
			: base(true)
		{
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x000108F5 File Offset: 0x0000EAF5
		internal SafeUserTokenHandle(IntPtr existingHandle, bool ownsHandle)
			: base(ownsHandle)
		{
			base.SetHandle(existingHandle);
		}

		// Token: 0x060002B6 RID: 694
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool DuplicateTokenEx(SafeHandle hToken, int access, NativeMethods.SECURITY_ATTRIBUTES tokenAttributes, int impersonationLevel, int tokenType, out SafeUserTokenHandle hNewToken);

		// Token: 0x060002B7 RID: 695
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		private static extern bool CloseHandle(IntPtr handle);

		// Token: 0x060002B8 RID: 696 RVA: 0x00010905 File Offset: 0x0000EB05
		protected override bool ReleaseHandle()
		{
			return SafeUserTokenHandle.CloseHandle(this.handle);
		}
	}
}
