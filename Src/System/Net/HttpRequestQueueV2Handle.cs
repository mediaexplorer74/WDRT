using System;
using System.Security;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.Net
{
	// Token: 0x020001E9 RID: 489
	[SuppressUnmanagedCodeSecurity]
	internal sealed class HttpRequestQueueV2Handle : CriticalHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060012DE RID: 4830 RVA: 0x00063E3E File Offset: 0x0006203E
		private HttpRequestQueueV2Handle()
		{
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x00063E46 File Offset: 0x00062046
		internal IntPtr DangerousGetHandle()
		{
			return this.handle;
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x00063E4E File Offset: 0x0006204E
		protected override bool ReleaseHandle()
		{
			return this.IsInvalid || Interlocked.Increment(ref this.disposed) != 1 || UnsafeNclNativeMethods.SafeNetHandles.HttpCloseRequestQueue(this.handle) == 0U;
		}

		// Token: 0x0400151F RID: 5407
		private int disposed;
	}
}
