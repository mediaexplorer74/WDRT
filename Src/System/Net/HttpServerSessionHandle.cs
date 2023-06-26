using System;
using System.Security;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x020001EA RID: 490
	[SuppressUnmanagedCodeSecurity]
	internal sealed class HttpServerSessionHandle : CriticalHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060012E1 RID: 4833 RVA: 0x00063E76 File Offset: 0x00062076
		internal HttpServerSessionHandle(ulong id)
		{
			this.serverSessionId = id;
			base.SetHandle(new IntPtr(1));
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x00063E91 File Offset: 0x00062091
		internal ulong DangerousGetServerSessionId()
		{
			return this.serverSessionId;
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x00063E99 File Offset: 0x00062099
		protected override bool ReleaseHandle()
		{
			return this.IsInvalid || Interlocked.Increment(ref this.disposed) != 1 || UnsafeNclNativeMethods.HttpApi.HttpCloseServerSession(this.serverSessionId) == 0U;
		}

		// Token: 0x04001520 RID: 5408
		private int disposed;

		// Token: 0x04001521 RID: 5409
		private ulong serverSessionId;
	}
}
