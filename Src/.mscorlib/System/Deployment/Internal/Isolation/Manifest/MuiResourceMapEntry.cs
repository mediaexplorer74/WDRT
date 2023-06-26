using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006CF RID: 1743
	[StructLayout(LayoutKind.Sequential)]
	internal class MuiResourceMapEntry : IDisposable
	{
		// Token: 0x0600507E RID: 20606 RVA: 0x0011F2A8 File Offset: 0x0011D4A8
		~MuiResourceMapEntry()
		{
			this.Dispose(false);
		}

		// Token: 0x0600507F RID: 20607 RVA: 0x0011F2D8 File Offset: 0x0011D4D8
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06005080 RID: 20608 RVA: 0x0011F2E4 File Offset: 0x0011D4E4
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

		// Token: 0x040022F8 RID: 8952
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr ResourceTypeIdInt;

		// Token: 0x040022F9 RID: 8953
		public uint ResourceTypeIdIntSize;

		// Token: 0x040022FA RID: 8954
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr ResourceTypeIdString;

		// Token: 0x040022FB RID: 8955
		public uint ResourceTypeIdStringSize;
	}
}
