using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x0200008B RID: 139
	[StructLayout(LayoutKind.Sequential)]
	internal class HashElementEntry : IDisposable
	{
		// Token: 0x0600023D RID: 573 RVA: 0x000088C8 File Offset: 0x00006AC8
		~HashElementEntry()
		{
			this.Dispose(false);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000088F8 File Offset: 0x00006AF8
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00008904 File Offset: 0x00006B04
		[SecuritySafeCritical]
		public void Dispose(bool fDisposing)
		{
			if (this.TransformMetadata != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.TransformMetadata);
				this.TransformMetadata = IntPtr.Zero;
			}
			if (this.DigestValue != IntPtr.Zero)
			{
				Marshal.FreeCoTaskMem(this.DigestValue);
				this.DigestValue = IntPtr.Zero;
			}
			if (fDisposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x0400024F RID: 591
		public uint index;

		// Token: 0x04000250 RID: 592
		public byte Transform;

		// Token: 0x04000251 RID: 593
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr TransformMetadata;

		// Token: 0x04000252 RID: 594
		public uint TransformMetadataSize;

		// Token: 0x04000253 RID: 595
		public byte DigestMethod;

		// Token: 0x04000254 RID: 596
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr DigestValue;

		// Token: 0x04000255 RID: 597
		public uint DigestValueSize;

		// Token: 0x04000256 RID: 598
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Xml;
	}
}
