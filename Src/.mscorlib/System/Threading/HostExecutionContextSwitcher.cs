using System;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004F8 RID: 1272
	internal class HostExecutionContextSwitcher
	{
		// Token: 0x06003C53 RID: 15443 RVA: 0x000E56DC File Offset: 0x000E38DC
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void Undo(object switcherObject)
		{
			if (switcherObject == null)
			{
				return;
			}
			HostExecutionContextManager currentHostExecutionContextManager = HostExecutionContextManager.GetCurrentHostExecutionContextManager();
			if (currentHostExecutionContextManager != null)
			{
				currentHostExecutionContextManager.Revert(switcherObject);
			}
		}

		// Token: 0x040019A0 RID: 6560
		internal ExecutionContext executionContext;

		// Token: 0x040019A1 RID: 6561
		internal HostExecutionContext previousHostContext;

		// Token: 0x040019A2 RID: 6562
		internal HostExecutionContext currentHostContext;
	}
}
