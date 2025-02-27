﻿using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000020 RID: 32
	internal struct STORE_ASSEMBLY
	{
		// Token: 0x04000106 RID: 262
		public uint Status;

		// Token: 0x04000107 RID: 263
		public IDefinitionIdentity DefinitionIdentity;

		// Token: 0x04000108 RID: 264
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ManifestPath;

		// Token: 0x04000109 RID: 265
		public ulong AssemblySize;

		// Token: 0x0400010A RID: 266
		public ulong ChangeId;
	}
}
