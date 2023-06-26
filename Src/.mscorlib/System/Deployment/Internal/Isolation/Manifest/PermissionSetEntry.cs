using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006FC RID: 1788
	[StructLayout(LayoutKind.Sequential)]
	internal class PermissionSetEntry
	{
		// Token: 0x04002392 RID: 9106
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Id;

		// Token: 0x04002393 RID: 9107
		[MarshalAs(UnmanagedType.LPWStr)]
		public string XmlSegment;
	}
}
