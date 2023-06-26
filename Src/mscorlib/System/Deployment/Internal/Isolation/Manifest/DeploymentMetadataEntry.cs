using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x02000705 RID: 1797
	[StructLayout(LayoutKind.Sequential)]
	internal class DeploymentMetadataEntry
	{
		// Token: 0x040023A7 RID: 9127
		[MarshalAs(UnmanagedType.LPWStr)]
		public string DeploymentProviderCodebase;

		// Token: 0x040023A8 RID: 9128
		[MarshalAs(UnmanagedType.LPWStr)]
		public string MinimumRequiredVersion;

		// Token: 0x040023A9 RID: 9129
		public ushort MaximumAge;

		// Token: 0x040023AA RID: 9130
		public byte MaximumAge_Unit;

		// Token: 0x040023AB RID: 9131
		public uint DeploymentFlags;
	}
}
