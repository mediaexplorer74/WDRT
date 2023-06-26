using System;
using System.Windows;

namespace Microsoft.Xaml.Behaviors.Core
{
	// Token: 0x0200004C RID: 76
	public sealed class NavigateBackAction : PrototypingActionBase
	{
		// Token: 0x060002BF RID: 703 RVA: 0x0000BFBE File Offset: 0x0000A1BE
		protected override void Invoke(object parameter)
		{
			InteractionContext.GoBack();
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000BFC5 File Offset: 0x0000A1C5
		protected override Freezable CreateInstanceCore()
		{
			return new NavigateBackAction();
		}
	}
}
