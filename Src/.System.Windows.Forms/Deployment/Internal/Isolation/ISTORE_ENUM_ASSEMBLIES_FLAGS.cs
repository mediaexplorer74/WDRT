﻿using System;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200004D RID: 77
	[Flags]
	internal enum ISTORE_ENUM_ASSEMBLIES_FLAGS
	{
		// Token: 0x04000145 RID: 325
		ISTORE_ENUM_ASSEMBLIES_FLAG_LIMIT_TO_VISIBLE_ONLY = 1,
		// Token: 0x04000146 RID: 326
		ISTORE_ENUM_ASSEMBLIES_FLAG_MATCH_SERVICING = 2,
		// Token: 0x04000147 RID: 327
		ISTORE_ENUM_ASSEMBLIES_FLAG_FORCE_LIBRARY_SEMANTICS = 4
	}
}
