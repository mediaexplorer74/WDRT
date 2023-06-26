using System;
using Microsoft.WindowsDeviceRecoveryTool.StateMachine.BaseTypes;
using Microsoft.WindowsDeviceRecoveryTool.StateMachine.Transitions;

namespace Microsoft.WindowsDeviceRecoveryTool.States.BaseStates
{
	// Token: 0x02000075 RID: 117
	public class PreviousStateTransition : StateStatusTransition
	{
		// Token: 0x060003FB RID: 1019 RVA: 0x000158CD File Offset: 0x00013ACD
		public PreviousStateTransition(BaseState next, string stateName)
			: base(next, stateName)
		{
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x000158D9 File Offset: 0x00013AD9
		public void SetNextState(BaseState state)
		{
			this.Next = state;
		}
	}
}
