using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006D5 RID: 1749
	[StructLayout(LayoutKind.Sequential)]
	internal class FileEntry : IDisposable
	{
		// Token: 0x06005090 RID: 20624 RVA: 0x0011F400 File Offset: 0x0011D600
		~FileEntry()
		{
			this.Dispose(false);
		}

		// Token: 0x06005091 RID: 20625 RVA: 0x0011F430 File Offset: 0x0011D630
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06005092 RID: 20626 RVA: 0x0011F43C File Offset: 0x0011D63C
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
				if (this.MuiMapping != null)
				{
					this.MuiMapping.Dispose(true);
					this.MuiMapping = null;
				}
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x04002311 RID: 8977
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Name;

		// Token: 0x04002312 RID: 8978
		public uint HashAlgorithm;

		// Token: 0x04002313 RID: 8979
		[MarshalAs(UnmanagedType.LPWStr)]
		public string LoadFrom;

		// Token: 0x04002314 RID: 8980
		[MarshalAs(UnmanagedType.LPWStr)]
		public string SourcePath;

		// Token: 0x04002315 RID: 8981
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ImportPath;

		// Token: 0x04002316 RID: 8982
		[MarshalAs(UnmanagedType.LPWStr)]
		public string SourceName;

		// Token: 0x04002317 RID: 8983
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Location;

		// Token: 0x04002318 RID: 8984
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr HashValue;

		// Token: 0x04002319 RID: 8985
		public uint HashValueSize;

		// Token: 0x0400231A RID: 8986
		public ulong Size;

		// Token: 0x0400231B RID: 8987
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Group;

		// Token: 0x0400231C RID: 8988
		public uint Flags;

		// Token: 0x0400231D RID: 8989
		public MuiResourceMapEntry MuiMapping;

		// Token: 0x0400231E RID: 8990
		public uint WritableType;

		// Token: 0x0400231F RID: 8991
		public ISection HashElements;
	}
}
