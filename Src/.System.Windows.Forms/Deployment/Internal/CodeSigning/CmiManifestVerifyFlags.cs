using System;

namespace System.Deployment.Internal.CodeSigning
{
	// Token: 0x0200000E RID: 14
	[Flags]
	internal enum CmiManifestVerifyFlags
	{
		// Token: 0x040000C0 RID: 192
		None = 0,
		// Token: 0x040000C1 RID: 193
		RevocationNoCheck = 1,
		// Token: 0x040000C2 RID: 194
		RevocationCheckEndCertOnly = 2,
		// Token: 0x040000C3 RID: 195
		RevocationCheckEntireChain = 4,
		// Token: 0x040000C4 RID: 196
		UrlCacheOnlyRetrieval = 8,
		// Token: 0x040000C5 RID: 197
		LifetimeSigning = 16,
		// Token: 0x040000C6 RID: 198
		TrustMicrosoftRootOnly = 32,
		// Token: 0x040000C7 RID: 199
		StrongNameOnly = 65536
	}
}
