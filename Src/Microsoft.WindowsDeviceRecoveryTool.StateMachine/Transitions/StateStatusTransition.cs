using System;
using Microsoft.WindowsDeviceRecoveryTool.StateMachine.BaseTypes;

namespace Microsoft.WindowsDeviceRecoveryTool.StateMachine.Transitions
{
	// Token: 0x02000004 RID: 4
	public class StateStatusTransition : BaseTransition
	{
		// Token: 0x06000004 RID: 4 RVA: 0x0000209B File Offset: 0x0000029B
		public StateStatusTransition(BaseState next, string statusKey)
			: base(next)
		{
			this.statusKey = statusKey;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020B0 File Offset: 0x000002B0
		public override bool ConditionsAreMet(object sender, TransitionEventArgs eventArgs)
		{
			return eventArgs.Status == this.statusKey;
		}

		// Token: 0x04000002 RID: 2
		private readonly string statusKey;
	}
}
