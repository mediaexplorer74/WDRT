using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006E4 RID: 1764
	[StructLayout(LayoutKind.Sequential)]
	internal class COMServerEntry
	{
		// Token: 0x04002347 RID: 9031
		public Guid Clsid;

		// Token: 0x04002348 RID: 9032
		public uint Flags;

		// Token: 0x04002349 RID: 9033
		public Guid ConfiguredGuid;

		// Token: 0x0400234A RID: 9034
		public Guid ImplementedClsid;

		// Token: 0x0400234B RID: 9035
		public Guid TypeLibrary;

		// Token: 0x0400234C RID: 9036
		public uint ThreadingModel;

		// Token: 0x0400234D RID: 9037
		[MarshalAs(UnmanagedType.LPWStr)]
		public string RuntimeVersion;

		// Token: 0x0400234E RID: 9038
		[MarshalAs(UnmanagedType.LPWStr)]
		public string HostFile;
	}
}
