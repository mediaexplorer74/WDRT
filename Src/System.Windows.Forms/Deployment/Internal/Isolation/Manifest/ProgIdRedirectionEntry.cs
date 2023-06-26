using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000A0 RID: 160
	[StructLayout(LayoutKind.Sequential)]
	internal class ProgIdRedirectionEntry
	{
		// Token: 0x040002A5 RID: 677
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ProgId;

		// Token: 0x040002A6 RID: 678
		public Guid RedirectedGuid;
	}
}
