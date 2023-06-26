using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020000BB RID: 187
	[StructLayout(LayoutKind.Sequential)]
	internal class DescriptionMetadataEntry
	{
		// Token: 0x040002E8 RID: 744
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Publisher;

		// Token: 0x040002E9 RID: 745
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Product;

		// Token: 0x040002EA RID: 746
		[MarshalAs(UnmanagedType.LPWStr)]
		public string SupportUrl;

		// Token: 0x040002EB RID: 747
		[MarshalAs(UnmanagedType.LPWStr)]
		public string IconFile;

		// Token: 0x040002EC RID: 748
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ErrorReportUrl;

		// Token: 0x040002ED RID: 749
		[MarshalAs(UnmanagedType.LPWStr)]
		public string SuiteName;
	}
}
