using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006ED RID: 1773
	[StructLayout(LayoutKind.Sequential)]
	internal class AssemblyReferenceDependentAssemblyEntry : IDisposable
	{
		// Token: 0x060050CA RID: 20682 RVA: 0x0011F4D8 File Offset: 0x0011D6D8
		~AssemblyReferenceDependentAssemblyEntry()
		{
			this.Dispose(false);
		}

		// Token: 0x060050CB RID: 20683 RVA: 0x0011F508 File Offset: 0x0011D708
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060050CC RID: 20684 RVA: 0x0011F511 File Offset: 0x0011D711
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

		// Token: 0x04002361 RID: 9057
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Group;

		// Token: 0x04002362 RID: 9058
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Codebase;

		// Token: 0x04002363 RID: 9059
		public ulong Size;

		// Token: 0x04002364 RID: 9060
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr HashValue;

		// Token: 0x04002365 RID: 9061
		public uint HashValueSize;

		// Token: 0x04002366 RID: 9062
		public uint HashAlgorithm;

		// Token: 0x04002367 RID: 9063
		public uint Flags;

		// Token: 0x04002368 RID: 9064
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ResourceFallbackCulture;

		// Token: 0x04002369 RID: 9065
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Description;

		// Token: 0x0400236A RID: 9066
		[MarshalAs(UnmanagedType.LPWStr)]
		public string SupportUrl;

		// Token: 0x0400236B RID: 9067
		public ISection HashElements;
	}
}
