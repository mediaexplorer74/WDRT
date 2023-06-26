using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000B5 RID: 181
	[StructLayout(LayoutKind.Sequential)]
	internal class PermissionSetEntry
	{
		// Token: 0x040002E0 RID: 736
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Id;

		// Token: 0x040002E1 RID: 737
		[MarshalAs(UnmanagedType.LPWStr)]
		public string XmlSegment;
	}
}
