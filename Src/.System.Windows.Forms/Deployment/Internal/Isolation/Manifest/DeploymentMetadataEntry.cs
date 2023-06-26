using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000BE RID: 190
	[StructLayout(LayoutKind.Sequential)]
	internal class DeploymentMetadataEntry
	{
		// Token: 0x040002F5 RID: 757
		[MarshalAs(UnmanagedType.LPWStr)]
		public string DeploymentProviderCodebase;

		// Token: 0x040002F6 RID: 758
		[MarshalAs(UnmanagedType.LPWStr)]
		public string MinimumRequiredVersion;

		// Token: 0x040002F7 RID: 759
		public ushort MaximumAge;

		// Token: 0x040002F8 RID: 760
		public byte MaximumAge_Unit;

		// Token: 0x040002F9 RID: 761
		public uint DeploymentFlags;
	}
}
