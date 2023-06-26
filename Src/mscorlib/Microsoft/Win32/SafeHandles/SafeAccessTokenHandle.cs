using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
	/// <summary>Provides a safe handle to a Windows thread or process access token. For more information, see Access Tokens.</summary>
	// Token: 0x02000026 RID: 38
	[SecurityCritical]
	public sealed class SafeAccessTokenHandle : SafeHandle
	{
		// Token: 0x06000179 RID: 377 RVA: 0x000048DB File Offset: 0x00002ADB
		private SafeAccessTokenHandle()
			: base(IntPtr.Zero, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SafeHandles.SafeAccessTokenHandle" /> class.</summary>
		/// <param name="handle">An <see cref="T:System.IntPtr" /> object that represents the pre-existing handle to use. Using <see cref="F:System.IntPtr.Zero" /> returns an invalid handle.</param>
		// Token: 0x0600017A RID: 378 RVA: 0x000048E9 File Offset: 0x00002AE9
		public SafeAccessTokenHandle(IntPtr handle)
			: base(IntPtr.Zero, true)
		{
			base.SetHandle(handle);
		}

		/// <summary>Returns an invalid handle by instantiating a <see cref="T:Microsoft.Win32.SafeHandles.SafeAccessTokenHandle" /> object with <see cref="F:System.IntPtr.Zero" />.</summary>
		/// <returns>Returns a <see cref="T:Microsoft.Win32.SafeHandles.SafeAccessTokenHandle" /> object.</returns>
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600017B RID: 379 RVA: 0x000048FE File Offset: 0x00002AFE
		public static SafeAccessTokenHandle InvalidHandle
		{
			[SecurityCritical]
			get
			{
				return new SafeAccessTokenHandle(IntPtr.Zero);
			}
		}

		/// <summary>Gets a value that indicates whether the handle is invalid.</summary>
		/// <returns>
		///   <see langword="true" /> if the handle is not valid; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600017C RID: 380 RVA: 0x0000490A File Offset: 0x00002B0A
		public override bool IsInvalid
		{
			[SecurityCritical]
			get
			{
				return this.handle == IntPtr.Zero || this.handle == new IntPtr(-1);
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00004931 File Offset: 0x00002B31
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return Win32Native.CloseHandle(this.handle);
		}
	}
}
