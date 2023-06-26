using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006DB RID: 1755
	[StructLayout(LayoutKind.Sequential)]
	internal class CategoryMembershipDataEntry
	{
		// Token: 0x04002339 RID: 9017
		public uint index;

		// Token: 0x0400233A RID: 9018
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Xml;

		// Token: 0x0400233B RID: 9019
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Description;
	}
}
