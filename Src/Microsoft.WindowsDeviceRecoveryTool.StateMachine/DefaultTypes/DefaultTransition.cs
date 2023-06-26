using System;
using Microsoft.WindowsDeviceRecoveryTool.StateMachine.BaseTypes;

namespace Microsoft.WindowsDeviceRecoveryTool.StateMachine.DefaultTypes
{
	// Token: 0x02000007 RID: 7
	public class DefaultTransition : BaseTransition
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002100 File Offset: 0x00000300
		public DefaultTransition(BaseState state)
			: base(state)
		{
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000210C File Offset: 0x0000030C
		public override bool ConditionsAreMet(object sender, TransitionEventArgs eventArgs)
		{
			return true;
		}
	}
}
