using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000148 RID: 328
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct hostent
	{
		// Token: 0x040010DE RID: 4318
		public IntPtr h_name;

		// Token: 0x040010DF RID: 4319
		public IntPtr h_aliases;

		// Token: 0x040010E0 RID: 4320
		public short h_addrtype;

		// Token: 0x040010E1 RID: 4321
		public short h_length;

		// Token: 0x040010E2 RID: 4322
		public IntPtr h_addr_list;
	}
}
