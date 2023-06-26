using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x02000091 RID: 145
	[StructLayout(LayoutKind.Sequential)]
	internal class FileAssociationEntry
	{
		// Token: 0x0400027D RID: 637
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Extension;

		// Token: 0x0400027E RID: 638
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Description;

		// Token: 0x0400027F RID: 639
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ProgID;

		// Token: 0x04000280 RID: 640
		[MarshalAs(UnmanagedType.LPWStr)]
		public string DefaultIcon;

		// Token: 0x04000281 RID: 641
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Parameter;
	}
}
