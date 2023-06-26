using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000675 RID: 1653
	internal struct STORE_ASSEMBLY
	{
		// Token: 0x040021F0 RID: 8688
		public uint Status;

		// Token: 0x040021F1 RID: 8689
		public IDefinitionIdentity DefinitionIdentity;

		// Token: 0x040021F2 RID: 8690
		[MarshalAs(UnmanagedType.LPWStr)]
		public string ManifestPath;

		// Token: 0x040021F3 RID: 8691
		public ulong AssemblySize;

		// Token: 0x040021F4 RID: 8692
		public ulong ChangeId;
	}
}
