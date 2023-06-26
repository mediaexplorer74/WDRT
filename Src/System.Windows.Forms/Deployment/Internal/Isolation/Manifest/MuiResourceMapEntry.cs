using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x02000088 RID: 136
	[StructLayout(LayoutKind.Sequential)]
	internal class MuiResourceMapEntry : IDisposable
	{
		// Token: 0x06000236 RID: 566 RVA: 0x00008824 File Offset: 0x00006A24
		~MuiResourceMapEntry()
		{
			this.Dispose(false);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00008854 File Offset: 0x00006A54
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00008860 File Offset: 0x00006A60
		[SecuritySafeCritical]
		public void Dispose(bool fDisposing)
		{
			if (this.ResourceTypeIdInt != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.ResourceTypeIdInt);
				this.ResourceTypeIdInt = IntPtr.Zero;
			}
			if (this.ResourceTypeIdString != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.ResourceTypeIdString);
				this.ResourceTypeIdString = IntPtr.Zero;
			}
			if (fDisposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x04000246 RID: 582
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr ResourceTypeIdInt;

		// Token: 0x04000247 RID: 583
		public uint ResourceTypeIdIntSize;

		// Token: 0x04000248 RID: 584
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr ResourceTypeIdString;

		// Token: 0x04000249 RID: 585
		public uint ResourceTypeIdStringSize;
	}
}
