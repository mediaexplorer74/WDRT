using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004FA RID: 1274
	[SecurityCritical]
	internal class IUnknownSafeHandle : SafeHandle
	{
		// Token: 0x06003C5C RID: 15452 RVA: 0x000E577E File Offset: 0x000E397E
		public IUnknownSafeHandle()
			: base(IntPtr.Zero, true)
		{
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x06003C5D RID: 15453 RVA: 0x000E578C File Offset: 0x000E398C
		public override bool IsInvalid
		{
			[SecurityCritical]
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x06003C5E RID: 15454 RVA: 0x000E579E File Offset: 0x000E399E
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			HostExecutionContextManager.ReleaseHostSecurityContext(this.handle);
			return true;
		}

		// Token: 0x06003C5F RID: 15455 RVA: 0x000E57B0 File Offset: 0x000E39B0
		internal object Clone()
		{
			IUnknownSafeHandle unknownSafeHandle = new IUnknownSafeHandle();
			if (!this.IsInvalid)
			{
				HostExecutionContextManager.CloneHostSecurityContext(this, unknownSafeHandle);
			}
			return unknownSafeHandle;
		}
	}
}
