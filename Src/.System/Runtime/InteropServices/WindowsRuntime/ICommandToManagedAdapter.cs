using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020003F5 RID: 1013
	[SecurityCritical]
	internal sealed class ICommandToManagedAdapter
	{
		// Token: 0x06002634 RID: 9780 RVA: 0x000B06C2 File Offset: 0x000AE8C2
		private ICommandToManagedAdapter()
		{
		}

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x06002635 RID: 9781 RVA: 0x000B06CC File Offset: 0x000AE8CC
		// (remove) Token: 0x06002636 RID: 9782 RVA: 0x000B071C File Offset: 0x000AE91C
		private event EventHandler CanExecuteChanged
		{
			add
			{
				ICommand_WinRT command_WinRT = JitHelpers.UnsafeCast<ICommand_WinRT>(this);
				Func<EventHandler<object>, EventRegistrationToken> func = new Func<EventHandler<object>, EventRegistrationToken>(command_WinRT.add_CanExecuteChanged);
				Action<EventRegistrationToken> action = new Action<EventRegistrationToken>(command_WinRT.remove_CanExecuteChanged);
				EventHandler<object> value2 = ICommandToManagedAdapter.m_weakTable.GetValue(value, new ConditionalWeakTable<EventHandler, EventHandler<object>>.CreateValueCallback(ICommandAdapterHelpers.CreateWrapperHandler));
				WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(func, action, value2);
			}
			remove
			{
				ICommand_WinRT command_WinRT = JitHelpers.UnsafeCast<ICommand_WinRT>(this);
				Action<EventRegistrationToken> action = new Action<EventRegistrationToken>(command_WinRT.remove_CanExecuteChanged);
				EventHandler<object> valueFromEquivalentKey = ICommandAdapterHelpers.GetValueFromEquivalentKey(ICommandToManagedAdapter.m_weakTable, value, new ConditionalWeakTable<EventHandler, EventHandler<object>>.CreateValueCallback(ICommandAdapterHelpers.CreateWrapperHandler));
				WindowsRuntimeMarshal.RemoveEventHandler<EventHandler<object>>(action, valueFromEquivalentKey);
			}
		}

		// Token: 0x06002637 RID: 9783 RVA: 0x000B0760 File Offset: 0x000AE960
		private bool CanExecute(object parameter)
		{
			ICommand_WinRT command_WinRT = JitHelpers.UnsafeCast<ICommand_WinRT>(this);
			return command_WinRT.CanExecute(parameter);
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x000B077C File Offset: 0x000AE97C
		private void Execute(object parameter)
		{
			ICommand_WinRT command_WinRT = JitHelpers.UnsafeCast<ICommand_WinRT>(this);
			command_WinRT.Execute(parameter);
		}

		// Token: 0x04002095 RID: 8341
		private static ConditionalWeakTable<EventHandler, EventHandler<object>> m_weakTable = new ConditionalWeakTable<EventHandler, EventHandler<object>>();
	}
}
