using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000022 RID: 34
	internal struct STORE_ASSEMBLY_FILE
	{
		// Token: 0x0400010D RID: 269
		public uint Size;

		// Token: 0x0400010E RID: 270
		public uint Flags;

		// Token: 0x0400010F RID: 271
		[MarshalAs(UnmanagedType.LPWStr)]
		public string FileName;

		// Token: 0x04000110 RID: 272
		public uint FileStatusFlags;
	}
}
