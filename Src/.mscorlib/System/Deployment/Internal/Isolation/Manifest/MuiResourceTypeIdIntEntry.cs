using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006CC RID: 1740
	[StructLayout(LayoutKind.Sequential)]
	internal class MuiResourceTypeIdIntEntry : IDisposable
	{
		// Token: 0x06005077 RID: 20599 RVA: 0x0011F1FC File Offset: 0x0011D3FC
		~MuiResourceTypeIdIntEntry()
		{
			this.Dispose(false);
		}

		// Token: 0x06005078 RID: 20600 RVA: 0x0011F22C File Offset: 0x0011D42C
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06005079 RID: 20601 RVA: 0x0011F238 File Offset: 0x0011D438
		[SecuritySafeCritical]
		public void Dispose(bool fDisposing)
		{
			if (this.StringIds != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.StringIds);
				this.StringIds = IntPtr.Zero;
			}
			if (this.IntegerIds != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.IntegerIds);
				this.IntegerIds = IntPtr.Zero;
			}
			if (fDisposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x040022EF RID: 8943
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr StringIds;

		// Token: 0x040022F0 RID: 8944
		public uint StringIdsSize;

		// Token: 0x040022F1 RID: 8945
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr IntegerIds;

		// Token: 0x040022F2 RID: 8946
		public uint IntegerIdsSize;
	}
}
