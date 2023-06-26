using System;
using System.Windows;

namespace Microsoft.Xaml.Behaviors.Core
{
	// Token: 0x0200004D RID: 77
	public sealed class NavigateForwardAction : PrototypingActionBase
	{
		// Token: 0x060002C2 RID: 706 RVA: 0x0000BFD4 File Offset: 0x0000A1D4
		protected override void Invoke(object parameter)
		{
			InteractionContext.GoForward();
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000BFDB File Offset: 0x0000A1DB
		protected override Freezable CreateInstanceCore()
		{
			return new NavigateForwardAction();
		}
	}
}
