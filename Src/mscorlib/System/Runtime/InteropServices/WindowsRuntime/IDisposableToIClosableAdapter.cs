using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009F9 RID: 2553
	internal sealed class IDisposableToIClosableAdapter
	{
		// Token: 0x06006504 RID: 25860 RVA: 0x00159417 File Offset: 0x00157617
		private IDisposableToIClosableAdapter()
		{
		}

		// Token: 0x06006505 RID: 25861 RVA: 0x00159420 File Offset: 0x00157620
		[SecurityCritical]
		public void Close()
		{
			IDisposable disposable = JitHelpers.UnsafeCast<IDisposable>(this);
			disposable.Dispose();
		}
	}
}
