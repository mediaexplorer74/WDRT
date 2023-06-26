using System;
using Microsoft.WindowsDeviceRecoveryTool.StateMachine.BaseTypes;
using Microsoft.WindowsDeviceRecoveryTool.StateMachine.DefaultTypes;

namespace Microsoft.WindowsDeviceRecoveryTool.StateMachine.Transitions
{
	// Token: 0x02000003 RID: 3
	public class PropagateStateStatusTransition : BaseTransition
	{
		// Token: 0x06000002 RID: 2 RVA: 0x0000205F File Offset: 0x0000025F
		public PropagateStateStatusTransition(string statusKey)
			: base(new EndState(statusKey))
		{
			this.statusKey = statusKey;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002078 File Offset: 0x00000278
		public override bool ConditionsAreMet(object sender, TransitionEventArgs eventArgs)
		{
			return eventArgs.Status == this.statusKey;
		}

		// Token: 0x04000001 RID: 1
		private readonly string statusKey;
	}
}
