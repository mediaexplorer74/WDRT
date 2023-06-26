using System;
using Microsoft.WindowsDeviceRecoveryTool.StateMachine.BaseTypes;
using Microsoft.WindowsDeviceRecoveryTool.StateMachine.DefaultTypes;

namespace Microsoft.WindowsDeviceRecoveryTool.StateMachine.Transitions
{
	// Token: 0x02000002 RID: 2
	public class PropagateErrorTransition : ErrorTransition
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public PropagateErrorTransition()
			: base(new ErrorEndState())
		{
		}
	}
}
