using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006F6 RID: 1782
	[StructLayout(LayoutKind.Sequential)]
	internal class ResourceTableMappingEntry
	{
		// Token: 0x04002384 RID: 9092
		[MarshalAs(UnmanagedType.LPWStr)]
		public string id;

		// Token: 0x04002385 RID: 9093
		[MarshalAs(UnmanagedType.LPWStr)]
		public string FinalStringMapped;
	}
}
