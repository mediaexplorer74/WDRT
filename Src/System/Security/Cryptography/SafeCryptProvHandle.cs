using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x02000459 RID: 1113
	internal sealed class SafeCryptProvHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06002962 RID: 10594 RVA: 0x000BC372 File Offset: 0x000BA572
		private SafeCryptProvHandle()
			: base(true)
		{
		}

		// Token: 0x06002963 RID: 10595 RVA: 0x000BC37B File Offset: 0x000BA57B
		internal SafeCryptProvHandle(IntPtr handle)
			: base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06002964 RID: 10596 RVA: 0x000BC38C File Offset: 0x000BA58C
		internal static SafeCryptProvHandle InvalidHandle
		{
			get
			{
				SafeCryptProvHandle safeCryptProvHandle = new SafeCryptProvHandle(IntPtr.Zero);
				GC.SuppressFinalize(safeCryptProvHandle);
				return safeCryptProvHandle;
			}
		}

		// Token: 0x06002965 RID: 10597
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern bool CryptReleaseContext(IntPtr hCryptProv, uint dwFlags);

		// Token: 0x06002966 RID: 10598 RVA: 0x000BC3AB File Offset: 0x000BA5AB
		protected override bool ReleaseHandle()
		{
			return SafeCryptProvHandle.CryptReleaseContext(this.handle, 0U);
		}
	}
}
