using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200020A RID: 522
	internal sealed class Semaphore : WaitHandle
	{
		// Token: 0x06001376 RID: 4982 RVA: 0x00066630 File Offset: 0x00064830
		internal Semaphore(int initialCount, int maxCount)
		{
			lock (this)
			{
				this.Handle = UnsafeNclNativeMethods.CreateSemaphore(IntPtr.Zero, initialCount, maxCount, IntPtr.Zero);
			}
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x00066684 File Offset: 0x00064884
		internal bool ReleaseSemaphore()
		{
			return UnsafeNclNativeMethods.ReleaseSemaphore(this.Handle, 1, IntPtr.Zero);
		}
	}
}
