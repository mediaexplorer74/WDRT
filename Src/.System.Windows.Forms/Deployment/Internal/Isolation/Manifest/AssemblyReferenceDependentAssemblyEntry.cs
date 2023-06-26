using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000A6 RID: 166
	[StructLayout(LayoutKind.Sequential)]
	internal class AssemblyReferenceDependentAssemblyEntry : IDisposable
	{
		// Token: 0x06000282 RID: 642 RVA: 0x00008A04 File Offset: 0x00006C04
		~AssemblyReferenceDependentAssemblyEntry()
		{
			this.Dispose(false);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00008A34 File Offset: 0x00006C34
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00008A3D File Offset: 0x00006C3D
		[SecuritySafeCritical]
		public void Dispose(bool fDisposing)
		{
			if (this.HashValue != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.HashValue);
				this.HashValue = IntPtr.Zero;
			}
			if (fDisposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x040002AF RID: 687
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Group;

		// Token: 0x040002B0 RID: 688
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Codebase;

		// Token: 0x040002B1 RID: 689
		public ulong Size;

		// Token: 0x040002B2 RID: 690
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr HashValue;

		// Token: 0x040002B3 RID: 691
		public uint HashValueSize;

		// Token: 0x040002B4 RID: 692
		public uint HashAlgorithm;

		// Token: 0x040002B5 RID: 693
		public uint Flags;

		// Token: 0x040002B6 RID: 694
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ResourceFallbackCulture;

		// Token: 0x040002B7 RID: 695
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Description;

		// Token: 0x040002B8 RID: 696
		[MarshalAs(UnmanagedType.LPWStr)]
		public string SupportUrl;

		// Token: 0x040002B9 RID: 697
		public ISection HashElements;
	}
}
