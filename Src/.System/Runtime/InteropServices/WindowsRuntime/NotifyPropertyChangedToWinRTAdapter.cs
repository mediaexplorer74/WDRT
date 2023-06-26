using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020003F4 RID: 1012
	internal sealed class NotifyPropertyChangedToWinRTAdapter
	{
		// Token: 0x06002630 RID: 9776 RVA: 0x000B0649 File Offset: 0x000AE849
		private NotifyPropertyChangedToWinRTAdapter()
		{
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x000B0654 File Offset: 0x000AE854
		[SecurityCritical]
		internal EventRegistrationToken add_PropertyChanged(PropertyChangedEventHandler value)
		{
			INotifyPropertyChanged notifyPropertyChanged = JitHelpers.UnsafeCast<INotifyPropertyChanged>(this);
			EventRegistrationTokenTable<PropertyChangedEventHandler> orCreateValue = NotifyPropertyChangedToWinRTAdapter.m_weakTable.GetOrCreateValue(notifyPropertyChanged);
			EventRegistrationToken eventRegistrationToken = orCreateValue.AddEventHandler(value);
			notifyPropertyChanged.PropertyChanged += value;
			return eventRegistrationToken;
		}

		// Token: 0x06002632 RID: 9778 RVA: 0x000B0684 File Offset: 0x000AE884
		[SecurityCritical]
		internal void remove_PropertyChanged(EventRegistrationToken token)
		{
			INotifyPropertyChanged notifyPropertyChanged = JitHelpers.UnsafeCast<INotifyPropertyChanged>(this);
			EventRegistrationTokenTable<PropertyChangedEventHandler> orCreateValue = NotifyPropertyChangedToWinRTAdapter.m_weakTable.GetOrCreateValue(notifyPropertyChanged);
			PropertyChangedEventHandler propertyChangedEventHandler = orCreateValue.ExtractHandler(token);
			if (propertyChangedEventHandler != null)
			{
				notifyPropertyChanged.PropertyChanged -= propertyChangedEventHandler;
			}
		}

		// Token: 0x04002094 RID: 8340
		private static ConditionalWeakTable<INotifyPropertyChanged, EventRegistrationTokenTable<PropertyChangedEventHandler>> m_weakTable = new ConditionalWeakTable<INotifyPropertyChanged, EventRegistrationTokenTable<PropertyChangedEventHandler>>();
	}
}
