using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000D9 RID: 217
	[StructLayout(LayoutKind.Sequential)]
	internal class DirectoryEntry : IDisposable
	{
		// Token: 0x06000308 RID: 776 RVA: 0x00008BE0 File Offset: 0x00006DE0
		~DirectoryEntry()
		{
			this.Dispose(false);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00008C10 File Offset: 0x00006E10
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00008C19 File Offset: 0x00006E19
		[SecuritySafeCritical]
		public void Dispose(bool fDisposing)
		{
			if (this.SecurityDescriptor != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.SecurityDescriptor);
				this.SecurityDescriptor = IntPtr.Zero;
			}
			if (fDisposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x04000379 RID: 889
		public uint Flags;

		// Token: 0x0400037A RID: 890
		public uint Protection;

		// Token: 0x0400037B RID: 891
		[MarshalAs(UnmanagedType.LPWStr)]
		public string BuildFilter;

		// Token: 0x0400037C RID: 892
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr SecurityDescriptor;

		// Token: 0x0400037D RID: 893
		public uint SecurityDescriptorSize;
	}
}
