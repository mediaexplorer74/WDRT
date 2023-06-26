using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000C4 RID: 196
	[StructLayout(LayoutKind.Sequential)]
	internal class CompatibleFrameworksMetadataEntry
	{
		// Token: 0x0400030F RID: 783
		[MarshalAs(UnmanagedType.LPWStr)]
		public string SupportUrl;
	}
}
