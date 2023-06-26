using System;
using Microsoft.WindowsDeviceRecoveryTool.StateMachine.BaseTypes;

namespace Microsoft.WindowsDeviceRecoveryTool.StateMachine.DefaultTypes
{
	// Token: 0x0200000A RID: 10
	public class ErrorEndState : BaseErrorState
	{
		// Token: 0x0600001D RID: 29 RVA: 0x0000232C File Offset: 0x0000052C
		public override void Start(Error error)
		{
			this.RaiseStateErrored(error);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002329 File Offset: 0x00000529
		public override void Stop()
		{
		}
	}
}
