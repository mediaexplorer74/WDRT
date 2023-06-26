using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002BE RID: 702
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeBCryptAlgorithmHandle : SafeHandle
	{
		// Token: 0x06002527 RID: 9511
		[SecurityCritical]
		[DllImport("bcrypt.dll")]
		private static extern int BCryptCloseAlgorithmProvider([In] IntPtr hAlgorithm, [In] uint dwFlags);

		// Token: 0x06002528 RID: 9512 RVA: 0x00088662 File Offset: 0x00086862
		[SecurityCritical]
		public SafeBCryptAlgorithmHandle()
			: base(IntPtr.Zero, true)
		{
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06002529 RID: 9513 RVA: 0x00088670 File Offset: 0x00086870
		public override bool IsInvalid
		{
			[SecurityCritical]
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x00088684 File Offset: 0x00086884
		[SecurityCritical]
		protected sealed override bool ReleaseHandle()
		{
			int num = SafeBCryptAlgorithmHandle.BCryptCloseAlgorithmProvider(this.handle, 0U);
			return num == 0;
		}
	}
}
