using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000AC RID: 172
	[StructLayout(LayoutKind.Sequential)]
	internal class WindowClassEntry
	{
		// Token: 0x040002CC RID: 716
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ClassName;

		// Token: 0x040002CD RID: 717
		[MarshalAs(UnmanagedType.LPWStr)]
		public string HostDll;

		// Token: 0x040002CE RID: 718
		public bool fVersioned;
	}
}
