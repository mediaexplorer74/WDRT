using System;

namespace System.Runtime.Remoting.Proxies
{
	// Token: 0x020007FE RID: 2046
	internal struct MessageData
	{
		// Token: 0x04002840 RID: 10304
		internal IntPtr pFrame;

		// Token: 0x04002841 RID: 10305
		internal IntPtr pMethodDesc;

		// Token: 0x04002842 RID: 10306
		internal IntPtr pDelegateMD;

		// Token: 0x04002843 RID: 10307
		internal IntPtr pSig;

		// Token: 0x04002844 RID: 10308
		internal IntPtr thGoverningType;

		// Token: 0x04002845 RID: 10309
		internal int iFlags;
	}
}
