using System;
using System.Runtime.CompilerServices;
using System.Security;
using System.Windows.Input;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020003F6 RID: 1014
	[SecurityCritical]
	internal sealed class ICommandToWinRTAdapter
	{
		// Token: 0x0600263A RID: 9786 RVA: 0x000B07A3 File Offset: 0x000AE9A3
		private ICommandToWinRTAdapter()
		{
		}

		// Token: 0x0600263B RID: 9787 RVA: 0x000B07AC File Offset: 0x000AE9AC
		private EventRegistrationToken add_CanExecuteChanged(EventHandler<object> value)
		{
			ICommand command = JitHelpers.UnsafeCast<ICommand>(this);
			EventRegistrationTokenTable<EventHandler> orCreateValue = ICommandToWinRTAdapter.m_weakTable.GetOrCreateValue(command);
			EventHandler eventHandler = ICommandAdapterHelpers.CreateWrapperHandler(value);
			EventRegistrationToken eventRegistrationToken = orCreateValue.AddEventHandler(eventHandler);
			command.CanExecuteChanged += eventHandler;
			return eventRegistrationToken;
		}

		// Token: 0x0600263C RID: 9788 RVA: 0x000B07E4 File Offset: 0x000AE9E4
		private void remove_CanExecuteChanged(EventRegistrationToken token)
		{
			ICommand command = JitHelpers.UnsafeCast<ICommand>(this);
			EventRegistrationTokenTable<EventHandler> orCreateValue = ICommandToWinRTAdapter.m_weakTable.GetOrCreateValue(command);
			EventHandler eventHandler = orCreateValue.ExtractHandler(token);
			if (eventHandler != null)
			{
				command.CanExecuteChanged -= eventHandler;
			}
		}

		// Token: 0x0600263D RID: 9789 RVA: 0x000B0818 File Offset: 0x000AEA18
		private bool CanExecute(object parameter)
		{
			ICommand command = JitHelpers.UnsafeCast<ICommand>(this);
			return command.CanExecute(parameter);
		}

		// Token: 0x0600263E RID: 9790 RVA: 0x000B0834 File Offset: 0x000AEA34
		private void Execute(object parameter)
		{
			ICommand command = JitHelpers.UnsafeCast<ICommand>(this);
			command.Execute(parameter);
		}

		// Token: 0x04002096 RID: 8342
		private static ConditionalWeakTable<ICommand, EventRegistrationTokenTable<EventHandler>> m_weakTable = new ConditionalWeakTable<ICommand, EventRegistrationTokenTable<EventHandler>>();
	}
}
