using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x020006AE RID: 1710
	internal struct IStore_BindingResult
	{
		// Token: 0x04002274 RID: 8820
		[MarshalAs(UnmanagedType.U4)]
		public uint Flags;

		// Token: 0x04002275 RID: 8821
		[MarshalAs(UnmanagedType.U4)]
		public uint Disposition;

		// Token: 0x04002276 RID: 8822
		public IStore_BindingResult_BoundVersion Component;

		// Token: 0x04002277 RID: 8823
		public Guid CacheCoherencyGuid;

		// Token: 0x04002278 RID: 8824
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr Reserved;
	}
}
