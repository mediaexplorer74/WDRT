using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x02000097 RID: 151
	[StructLayout(LayoutKind.Sequential)]
	internal class SubcategoryMembershipEntry
	{
		// Token: 0x0400028D RID: 653
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Subcategory;

		// Token: 0x0400028E RID: 654
		public ISection CategoryMembershipData;
	}
}
