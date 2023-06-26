using System;
using Microsoft.WindowsDeviceRecoveryTool.StateMachine.BaseTypes;

namespace Microsoft.WindowsDeviceRecoveryTool.StateMachine.DefaultTypes
{
	// Token: 0x0200000F RID: 15
	public class TransitionToErrorState : BaseState
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000026B3 File Offset: 0x000008B3
		public TransitionToErrorState(BaseErrorState errorState, Exception exception)
		{
			this.exception = exception;
			base.AddErrorTransition(new ErrorTransition(errorState), exception);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000026D2 File Offset: 0x000008D2
		public override void Start()
		{
			this.RaiseStateErrored(new Error(this.exception));
		}

		// Token: 0x0400000B RID: 11
		private readonly Exception exception;
	}
}
