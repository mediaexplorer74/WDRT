using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.CodeSigning
{
	// Token: 0x02000013 RID: 19
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct BLOBHEADER
	{
		// Token: 0x040000DE RID: 222
		internal byte bType;

		// Token: 0x040000DF RID: 223
		internal byte bVersion;

		// Token: 0x040000E0 RID: 224
		internal short reserved;

		// Token: 0x040000E1 RID: 225
		internal uint aiKeyAlg;
	}
}
