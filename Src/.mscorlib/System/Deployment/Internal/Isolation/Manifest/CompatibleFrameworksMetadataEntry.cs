using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x0200070B RID: 1803
	[StructLayout(LayoutKind.Sequential)]
	internal class CompatibleFrameworksMetadataEntry
	{
		// Token: 0x040023C1 RID: 9153
		[MarshalAs(UnmanagedType.LPWStr)]
		public string SupportUrl;
	}
}
