using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020003F3 RID: 1011
	internal sealed class NotifyPropertyChangedToManagedAdapter
	{
		// Token: 0x0600262D RID: 9773 RVA: 0x000B05DE File Offset: 0x000AE7DE
		private NotifyPropertyChangedToManagedAdapter()
		{
		}

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x0600262E RID: 9774 RVA: 0x000B05E8 File Offset: 0x000AE7E8
		// (remove) Token: 0x0600262F RID: 9775 RVA: 0x000B0620 File Offset: 0x000AE820
		internal event PropertyChangedEventHandler PropertyChanged
		{
			[SecurityCritical]
			add
			{
				INotifyPropertyChanged_WinRT notifyPropertyChanged_WinRT = JitHelpers.UnsafeCast<INotifyPropertyChanged_WinRT>(this);
				Func<PropertyChangedEventHandler, EventRegistrationToken> func = new Func<PropertyChangedEventHandler, EventRegistrationToken>(notifyPropertyChanged_WinRT.add_PropertyChanged);
				Action<EventRegistrationToken> action = new Action<EventRegistrationToken>(notifyPropertyChanged_WinRT.remove_PropertyChanged);
				WindowsRuntimeMarshal.AddEventHandler<PropertyChangedEventHandler>(func, action, value);
			}
			[SecurityCritical]
			remove
			{
				INotifyPropertyChanged_WinRT notifyPropertyChanged_WinRT = JitHelpers.UnsafeCast<INotifyPropertyChanged_WinRT>(this);
				Action<EventRegistrationToken> action = new Action<EventRegistrationToken>(notifyPropertyChanged_WinRT.remove_PropertyChanged);
				WindowsRuntimeMarshal.RemoveEventHandler<PropertyChangedEventHandler>(action, value);
			}
		}
	}
}
