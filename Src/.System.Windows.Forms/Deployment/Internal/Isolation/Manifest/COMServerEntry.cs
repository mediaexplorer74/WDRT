using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x0200009D RID: 157
	[StructLayout(LayoutKind.Sequential)]
	internal class COMServerEntry
	{
		// Token: 0x04000295 RID: 661
		public Guid Clsid;

		// Token: 0x04000296 RID: 662
		public uint Flags;

		// Token: 0x04000297 RID: 663
		public Guid ConfiguredGuid;

		// Token: 0x04000298 RID: 664
		public Guid ImplementedClsid;

		// Token: 0x04000299 RID: 665
		public Guid TypeLibrary;

		// Token: 0x0400029A RID: 666
		public uint ThreadingModel;

		// Token: 0x0400029B RID: 667
		[MarshalAs(UnmanagedType.LPWStr)]
		public string RuntimeVersion;

		// Token: 0x0400029C RID: 668
		[MarshalAs(UnmanagedType.LPWStr)]
		public string HostFile;
	}
}
