using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000677 RID: 1655
	internal struct STORE_ASSEMBLY_FILE
	{
		// Token: 0x040021F7 RID: 8695
		public uint Size;

		// Token: 0x040021F8 RID: 8696
		public uint Flags;

		// Token: 0x040021F9 RID: 8697
		[MarshalAs(UnmanagedType.LPWStr)]
		public string FileName;

		// Token: 0x040021FA RID: 8698
		public uint FileStatusFlags;
	}
}
