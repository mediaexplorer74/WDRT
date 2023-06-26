using System;
using Microsoft.WindowsDeviceRecoveryTool.StateMachine.BaseTypes;

namespace Microsoft.WindowsDeviceRecoveryTool.StateMachine.DefaultTypes
{
	// Token: 0x0200000C RID: 12
	public class StartState : BaseState
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002371 File Offset: 0x00000571
		public override void Start()
		{
			this.RaiseStateFinished(TransitionEventArgs.Empty);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002329 File Offset: 0x00000529
		public override void Stop()
		{
		}
	}
}
