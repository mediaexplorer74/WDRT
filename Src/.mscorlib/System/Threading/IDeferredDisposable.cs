using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x02000508 RID: 1288
	internal interface IDeferredDisposable
	{
		// Token: 0x06003CDA RID: 15578
		[SecurityCritical]
		void OnFinalRelease(bool disposed);
	}
}
