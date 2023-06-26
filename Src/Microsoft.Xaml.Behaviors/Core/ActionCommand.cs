using System;
using System.Windows.Input;

namespace Microsoft.Xaml.Behaviors.Core
{
	// Token: 0x02000038 RID: 56
	public sealed class ActionCommand : ICommand
	{
		// Token: 0x060001F3 RID: 499 RVA: 0x000083CA File Offset: 0x000065CA
		public ActionCommand(Action action)
		{
			this.action = action;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000083D9 File Offset: 0x000065D9
		public ActionCommand(Action<object> objectAction)
		{
			this.objectAction = objectAction;
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060001F5 RID: 501 RVA: 0x000083E8 File Offset: 0x000065E8
		// (remove) Token: 0x060001F6 RID: 502 RVA: 0x00008420 File Offset: 0x00006620
		private event EventHandler CanExecuteChanged;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060001F7 RID: 503 RVA: 0x00008455 File Offset: 0x00006655
		// (remove) Token: 0x060001F8 RID: 504 RVA: 0x0000845E File Offset: 0x0000665E
		event EventHandler ICommand.CanExecuteChanged
		{
			add
			{
				this.CanExecuteChanged += value;
			}
			remove
			{
				this.CanExecuteChanged -= value;
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00008467 File Offset: 0x00006667
		bool ICommand.CanExecute(object parameter)
		{
			return true;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000846A File Offset: 0x0000666A
		public void Execute(object parameter)
		{
			if (this.objectAction != null)
			{
				this.objectAction(parameter);
				return;
			}
			this.action();
		}

		// Token: 0x040000A9 RID: 169
		private Action action;

		// Token: 0x040000AA RID: 170
		private Action<object> objectAction;
	}
}
