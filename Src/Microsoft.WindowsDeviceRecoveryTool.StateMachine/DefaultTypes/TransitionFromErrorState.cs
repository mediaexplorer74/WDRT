using System;
using Microsoft.WindowsDeviceRecoveryTool.StateMachine.BaseTypes;

namespace Microsoft.WindowsDeviceRecoveryTool.StateMachine.DefaultTypes
{
	// Token: 0x0200000E RID: 14
	public class TransitionFromErrorState : BaseErrorState
	{
		// Token: 0x06000034 RID: 52 RVA: 0x0000269C File Offset: 0x0000089C
		public TransitionFromErrorState(BaseState state)
		{
			base.DefaultTransition = new DefaultTransition(state);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002371 File Offset: 0x00000571
		public override void Start(Error error)
		{
			this.RaiseStateFinished(TransitionEventArgs.Empty);
		}
	}
}
