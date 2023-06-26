using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x02000520 RID: 1312
	internal interface IThreadPoolWorkItem
	{
		// Token: 0x06003DFB RID: 15867
		[SecurityCritical]
		void ExecuteWorkItem();

		// Token: 0x06003DFC RID: 15868
		[SecurityCritical]
		void MarkAborted(ThreadAbortException tae);
	}
}
