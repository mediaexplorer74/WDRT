using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000672 RID: 1650
	internal struct BLOB : IDisposable
	{
		// Token: 0x06004F46 RID: 20294 RVA: 0x0011DB27 File Offset: 0x0011BD27
		[SecuritySafeCritical]
		public void Dispose()
		{
			if (this.BlobData != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.BlobData);
				this.BlobData = IntPtr.Zero;
			}
		}

		// Token: 0x040021E7 RID: 8679
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x040021E8 RID: 8680
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr BlobData;
	}
}
