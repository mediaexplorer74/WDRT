using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006E7 RID: 1767
	[StructLayout(LayoutKind.Sequential)]
	internal class ProgIdRedirectionEntry
	{
		// Token: 0x04002357 RID: 9047
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ProgId;

		// Token: 0x04002358 RID: 9048
		public Guid RedirectedGuid;
	}
}
