using System;
using Microsoft.WindowsDeviceRecoveryTool.StateMachine.BaseTypes;

namespace Microsoft.WindowsDeviceRecoveryTool.StateMachine.DefaultTypes
{
	// Token: 0x0200000B RID: 11
	public class LambdaTransition : BaseTransition
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002340 File Offset: 0x00000540
		public LambdaTransition(Func<bool> predicate, BaseState state)
			: base(state)
		{
			this.predicate = predicate;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002354 File Offset: 0x00000554
		public override bool ConditionsAreMet(object sender, TransitionEventArgs eventArgs)
		{
			return this.predicate();
		}

		// Token: 0x04000008 RID: 8
		private readonly Func<bool> predicate;
	}
}
