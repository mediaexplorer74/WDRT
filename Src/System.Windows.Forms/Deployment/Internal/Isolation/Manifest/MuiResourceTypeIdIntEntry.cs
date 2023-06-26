using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x02000085 RID: 133
	[StructLayout(LayoutKind.Sequential)]
	internal class MuiResourceTypeIdIntEntry : IDisposable
	{
		// Token: 0x0600022F RID: 559 RVA: 0x00008780 File Offset: 0x00006980
		~MuiResourceTypeIdIntEntry()
		{
			this.Dispose(false);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000087B0 File Offset: 0x000069B0
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x000087BC File Offset: 0x000069BC
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

		// Token: 0x0400023D RID: 573
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr StringIds;

		// Token: 0x0400023E RID: 574
		public uint StringIdsSize;

		// Token: 0x0400023F RID: 575
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr IntegerIds;

		// Token: 0x04000240 RID: 576
		public uint IntegerIdsSize;
	}
}
