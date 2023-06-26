using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006D8 RID: 1752
	[StructLayout(LayoutKind.Sequential)]
	internal class FileAssociationEntry
	{
		// Token: 0x0400232F RID: 9007
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Extension;

		// Token: 0x04002330 RID: 9008
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Description;

		// Token: 0x04002331 RID: 9009
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ProgID;

		// Token: 0x04002332 RID: 9010
		[MarshalAs(UnmanagedType.LPWStr)]
		public string DefaultIcon;

		// Token: 0x04002333 RID: 9011
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Parameter;
	}
}
