using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
	/// <summary>Provides a managed wrapper for a process handle.</summary>
	// Token: 0x02000031 RID: 49
	[SuppressUnmanagedCodeSecurity]
	public sealed class SafeProcessHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060002A7 RID: 679 RVA: 0x00010867 File Offset: 0x0000EA67
		internal SafeProcessHandle()
			: base(true)
		{
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00010870 File Offset: 0x0000EA70
		internal SafeProcessHandle(IntPtr handle)
			: base(true)
		{
			base.SetHandle(handle);
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SafeHandles.SafeProcessHandle" /> class from the specified handle, indicating whether to release the handle during the finalization phase.</summary>
		/// <param name="existingHandle">The handle to be wrapped.</param>
		/// <param name="ownsHandle">
		///   <see langword="true" /> to reliably let <see cref="T:Microsoft.Win32.SafeHandles.SafeProcessHandle" /> release the handle during the finalization phase; otherwise, <see langword="false" />.</param>
		// Token: 0x060002A9 RID: 681 RVA: 0x00010880 File Offset: 0x0000EA80
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		public SafeProcessHandle(IntPtr existingHandle, bool ownsHandle)
			: base(ownsHandle)
		{
			base.SetHandle(existingHandle);
		}

		// Token: 0x060002AA RID: 682
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		internal static extern SafeProcessHandle OpenProcess(int access, bool inherit, int processId);

		// Token: 0x060002AB RID: 683 RVA: 0x00010890 File Offset: 0x0000EA90
		internal void InitialSetHandle(IntPtr h)
		{
			this.handle = h;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00010899 File Offset: 0x0000EA99
		protected override bool ReleaseHandle()
		{
			return SafeNativeMethods.CloseHandle(this.handle);
		}

		// Token: 0x04000395 RID: 917
		internal static SafeProcessHandle InvalidHandle = new SafeProcessHandle(IntPtr.Zero);
	}
}
