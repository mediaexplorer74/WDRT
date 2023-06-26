using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000030 RID: 48
	[SuppressUnmanagedCodeSecurity]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	internal sealed class SafeLocalMemHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x00010837 File Offset: 0x0000EA37
		internal SafeLocalMemHandle()
			: base(true)
		{
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00010840 File Offset: 0x0000EA40
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		internal SafeLocalMemHandle(IntPtr existingHandle, bool ownsHandle)
			: base(ownsHandle)
		{
			base.SetHandle(existingHandle);
		}

		// Token: 0x060002A4 RID: 676
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern bool ConvertStringSecurityDescriptorToSecurityDescriptor(string StringSecurityDescriptor, int StringSDRevision, out SafeLocalMemHandle pSecurityDescriptor, IntPtr SecurityDescriptorSize);

		// Token: 0x060002A5 RID: 677
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll")]
		private static extern IntPtr LocalFree(IntPtr hMem);

		// Token: 0x060002A6 RID: 678 RVA: 0x00010850 File Offset: 0x0000EA50
		protected override bool ReleaseHandle()
		{
			return SafeLocalMemHandle.LocalFree(this.handle) == IntPtr.Zero;
		}
	}
}
