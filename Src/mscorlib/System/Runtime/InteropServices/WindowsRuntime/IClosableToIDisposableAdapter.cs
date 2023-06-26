using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009FA RID: 2554
	[SecurityCritical]
	internal sealed class IClosableToIDisposableAdapter
	{
		// Token: 0x06006506 RID: 25862 RVA: 0x0015943A File Offset: 0x0015763A
		private IClosableToIDisposableAdapter()
		{
		}

		// Token: 0x06006507 RID: 25863 RVA: 0x00159444 File Offset: 0x00157644
		[SecurityCritical]
		private void Dispose()
		{
			IClosable closable = JitHelpers.UnsafeCast<IClosable>(this);
			closable.Close();
		}
	}
}
