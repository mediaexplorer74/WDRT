using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation.Manifest
{
	// Token: 0x02000094 RID: 148
	[StructLayout(LayoutKind.Sequential)]
	internal class CategoryMembershipDataEntry
	{
		// Token: 0x04000287 RID: 647
		public uint index;

		// Token: 0x04000288 RID: 648
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Xml;

		// Token: 0x04000289 RID: 649
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Description;
	}
}
