using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000673 RID: 1651
	internal struct IDENTITY_ATTRIBUTE
	{
		// Token: 0x040021E9 RID: 8681
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Namespace;

		// Token: 0x040021EA RID: 8682
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Name;

		// Token: 0x040021EB RID: 8683
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Value;
	}
}
