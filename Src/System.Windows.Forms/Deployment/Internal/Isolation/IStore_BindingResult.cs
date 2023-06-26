using System;
using System.Runtime.InteropServices;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200005F RID: 95
	internal struct IStore_BindingResult
	{
		// Token: 0x04000194 RID: 404
		[MarshalAs(UnmanagedType.U4)]
		public uint Flags;

		// Token: 0x04000195 RID: 405
		[MarshalAs(UnmanagedType.U4)]
		public uint Disposition;

		// Token: 0x04000196 RID: 406
		public IStore_BindingResult_BoundVersion Component;

		// Token: 0x04000197 RID: 407
		public Guid CacheCoherencyGuid;

		// Token: 0x04000198 RID: 408
		[MarshalAs(UnmanagedType.SysInt)]
		public IntPtr Reserved;
	}
}
