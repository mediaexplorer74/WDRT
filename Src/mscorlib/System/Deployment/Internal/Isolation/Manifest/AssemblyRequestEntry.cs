using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006FF RID: 1791
	[StructLayout(LayoutKind.Sequential)]
	internal class AssemblyRequestEntry
	{
		// Token: 0x04002396 RID: 9110
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Name;

		// Token: 0x04002397 RID: 9111
		[MarshalAs(UnmanagedType.LPWStr)]
		public string permissionSetID;
	}
}
