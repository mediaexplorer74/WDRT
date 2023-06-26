using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006D2 RID: 1746
	[StructLayout(LayoutKind.Sequential)]
	internal class HashElementEntry : IDisposable
	{
		// Token: 0x06005085 RID: 20613 RVA: 0x0011F354 File Offset: 0x0011D554
		~HashElementEntry()
		{
			this.Dispose(false);
		}

		// Token: 0x06005086 RID: 20614 RVA: 0x0011F384 File Offset: 0x0011D584
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06005087 RID: 20615 RVA: 0x0011F390 File Offset: 0x0011D590
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

		// Token: 0x04002301 RID: 8961
		public uint index;

		// Token: 0x04002302 RID: 8962
		public byte Transform;

		// Token: 0x04002303 RID: 8963
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr TransformMetadata;

		// Token: 0x04002304 RID: 8964
		public uint TransformMetadataSize;

		// Token: 0x04002305 RID: 8965
		public byte DigestMethod;

		// Token: 0x04002306 RID: 8966
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr DigestValue;

		// Token: 0x04002307 RID: 8967
		public uint DigestValueSize;

		// Token: 0x04002308 RID: 8968
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Xml;
	}
}
