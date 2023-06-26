using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000E5 RID: 229
	[StructLayout(LayoutKind.Sequential)]
	internal class CompatibleFrameworkEntry
	{
		// Token: 0x040003A3 RID: 931
		public uint index;

		// Token: 0x040003A4 RID: 932
		[MarshalAs(UnmanagedType.LPWStr)]
		public string TargetVersion;

		// Token: 0x040003A5 RID: 933
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Profile;

		// Token: 0x040003A6 RID: 934
		[MarshalAs(UnmanagedType.LPWStr)]
		public string SupportedRuntime;
	}
}
