using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006C9 RID: 1737
	[StructLayout(LayoutKind.Sequential)]
	internal class MuiResourceTypeIdStringEntry : IDisposable
	{
		// Token: 0x06005070 RID: 20592 RVA: 0x0011F150 File Offset: 0x0011D350
		~MuiResourceTypeIdStringEntry()
		{
			this.Dispose(false);
		}

		// Token: 0x06005071 RID: 20593 RVA: 0x0011F180 File Offset: 0x0011D380
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06005072 RID: 20594 RVA: 0x0011F18C File Offset: 0x0011D38C
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

		// Token: 0x040022E6 RID: 8934
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr StringIds;

		// Token: 0x040022E7 RID: 8935
		public uint StringIdsSize;

		// Token: 0x040022E8 RID: 8936
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr IntegerIds;

		// Token: 0x040022E9 RID: 8937
		public uint IntegerIdsSize;
	}
}
