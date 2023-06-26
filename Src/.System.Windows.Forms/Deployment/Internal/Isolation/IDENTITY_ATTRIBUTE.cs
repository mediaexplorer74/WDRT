using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200001E RID: 30
	internal struct IDENTITY_ATTRIBUTE
	{
		// Token: 0x040000FF RID: 255
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Namespace;

		// Token: 0x04000100 RID: 256
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Name;

		// Token: 0x04000101 RID: 257
		[MarshalAs(UnmanagedType.LPWStr)]
		public string Value;
	}
}
