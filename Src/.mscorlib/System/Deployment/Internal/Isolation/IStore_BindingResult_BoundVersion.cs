using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x020006AD RID: 1709
	internal struct IStore_BindingResult_BoundVersion
	{
		// Token: 0x04002270 RID: 8816
		[MarshalAs(UnmanagedType.U2)]
		public ushort Revision;

		// Token: 0x04002271 RID: 8817
		[MarshalAs(UnmanagedType.U2)]
		public ushort Build;

		// Token: 0x04002272 RID: 8818
		[MarshalAs(UnmanagedType.U2)]
		public ushort Minor;

		// Token: 0x04002273 RID: 8819
		[MarshalAs(UnmanagedType.U2)]
		public ushort Major;
	}
}
