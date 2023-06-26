using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006F3 RID: 1779
	[StructLayout(LayoutKind.Sequential)]
	internal class WindowClassEntry
	{
		// Token: 0x0400237E RID: 9086
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ClassName;

		// Token: 0x0400237F RID: 9087
		[MarshalAs(UnmanagedType.LPWStr)]
		public string HostDll;

		// Token: 0x04002380 RID: 9088
		public bool fVersioned;
	}
}
