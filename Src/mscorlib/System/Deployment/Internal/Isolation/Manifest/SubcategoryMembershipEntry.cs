using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x020006DE RID: 1758
	[StructLayout(LayoutKind.Sequential)]
	internal class SubcategoryMembershipEntry
	{
		// Token: 0x0400233F RID: 9023
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Subcategory;

		// Token: 0x04002340 RID: 9024
		public ISection CategoryMembershipData;
	}
}
