using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000B8 RID: 184
	[StructLayout(LayoutKind.Sequential)]
	internal class AssemblyRequestEntry
	{
		// Token: 0x040002E4 RID: 740
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Name;

		// Token: 0x040002E5 RID: 741
		[MarshalAs(UnmanagedType.LPWStr)]
		public string permissionSetID;
	}
}
