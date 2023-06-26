using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x02000082 RID: 130
	[StructLayout(LayoutKind.Sequential)]
	internal class MuiResourceTypeIdStringEntry : IDisposable
	{
		// Token: 0x06000228 RID: 552 RVA: 0x000086DC File Offset: 0x000068DC
		~MuiResourceTypeIdStringEntry()
		{
			this.Dispose(false);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000870C File Offset: 0x0000690C
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00008718 File Offset: 0x00006918
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

		// Token: 0x04000234 RID: 564
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr StringIds;

		// Token: 0x04000235 RID: 565
		public uint StringIdsSize;

		// Token: 0x04000236 RID: 566
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr IntegerIds;

		// Token: 0x04000237 RID: 567
		public uint IntegerIdsSize;
	}
}
