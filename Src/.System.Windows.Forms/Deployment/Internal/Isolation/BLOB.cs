using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200001D RID: 29
	internal struct BLOB : IDisposable
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x000069F2 File Offset: 0x00004BF2
		[SecuritySafeCritical]
		public void Dispose()
		{
			if (this.BlobData != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.BlobData);
				this.BlobData = IntPtr.Zero;
			}
		}

		// Token: 0x040000FD RID: 253
		[MarshalAs(UnmanagedType.U4)]
		public uint Size;

		// Token: 0x040000FE RID: 254
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr BlobData;
	}
}
