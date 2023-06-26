using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x02000458 RID: 1112
	internal sealed class SafeLocalAllocHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600295D RID: 10589 RVA: 0x000BC321 File Offset: 0x000BA521
		private SafeLocalAllocHandle()
			: base(true)
		{
		}

		// Token: 0x0600295E RID: 10590 RVA: 0x000BC32A File Offset: 0x000BA52A
		internal SafeLocalAllocHandle(IntPtr handle)
			: base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x0600295F RID: 10591 RVA: 0x000BC33C File Offset: 0x000BA53C
		internal static SafeLocalAllocHandle InvalidHandle
		{
			get
			{
				SafeLocalAllocHandle safeLocalAllocHandle = new SafeLocalAllocHandle(IntPtr.Zero);
				GC.SuppressFinalize(safeLocalAllocHandle);
				return safeLocalAllocHandle;
			}
		}

		// Token: 0x06002960 RID: 10592
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr LocalFree(IntPtr handle);

		// Token: 0x06002961 RID: 10593 RVA: 0x000BC35B File Offset: 0x000BA55B
		protected override bool ReleaseHandle()
		{
			return SafeLocalAllocHandle.LocalFree(this.handle) == IntPtr.Zero;
		}
	}
}
