using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x0200045C RID: 1116
	internal sealed class SafeCryptMsgHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06002971 RID: 10609 RVA: 0x000BC449 File Offset: 0x000BA649
		private SafeCryptMsgHandle()
			: base(true)
		{
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x000BC452 File Offset: 0x000BA652
		internal SafeCryptMsgHandle(IntPtr handle)
			: base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06002973 RID: 10611 RVA: 0x000BC464 File Offset: 0x000BA664
		internal static SafeCryptMsgHandle InvalidHandle
		{
			get
			{
				SafeCryptMsgHandle safeCryptMsgHandle = new SafeCryptMsgHandle(IntPtr.Zero);
				GC.SuppressFinalize(safeCryptMsgHandle);
				return safeCryptMsgHandle;
			}
		}

		// Token: 0x06002974 RID: 10612
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("crypt32.dll", SetLastError = true)]
		private static extern bool CryptMsgClose(IntPtr handle);

		// Token: 0x06002975 RID: 10613 RVA: 0x000BC483 File Offset: 0x000BA683
		protected override bool ReleaseHandle()
		{
			return SafeCryptMsgHandle.CryptMsgClose(this.handle);
		}
	}
}
