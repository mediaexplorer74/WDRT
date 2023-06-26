using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200005E RID: 94
	internal struct IStore_BindingResult_BoundVersion
	{
		// Token: 0x04000190 RID: 400
		[MarshalAs(UnmanagedType.U2)]
		public ushort Revision;

		// Token: 0x04000191 RID: 401
		[MarshalAs(UnmanagedType.U2)]
		public ushort Build;

		// Token: 0x04000192 RID: 402
		[MarshalAs(UnmanagedType.U2)]
		public ushort Minor;

		// Token: 0x04000193 RID: 403
		[MarshalAs(UnmanagedType.U2)]
		public ushort Major;
	}
}
