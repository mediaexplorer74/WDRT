using System;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020003F2 RID: 1010
	internal sealed class NotifyCollectionChangedToWinRTAdapter
	{
		// Token: 0x06002629 RID: 9769 RVA: 0x000B0565 File Offset: 0x000AE765
		private NotifyCollectionChangedToWinRTAdapter()
		{
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x000B0570 File Offset: 0x000AE770
		[SecurityCritical]
		internal EventRegistrationToken add_CollectionChanged(NotifyCollectionChangedEventHandler value)
		{
			INotifyCollectionChanged notifyCollectionChanged = JitHelpers.UnsafeCast<INotifyCollectionChanged>(this);
			EventRegistrationTokenTable<NotifyCollectionChangedEventHandler> orCreateValue = NotifyCollectionChangedToWinRTAdapter.m_weakTable.GetOrCreateValue(notifyCollectionChanged);
			EventRegistrationToken eventRegistrationToken = orCreateValue.AddEventHandler(value);
			notifyCollectionChanged.CollectionChanged += value;
			return eventRegistrationToken;
		}

		// Token: 0x0600262B RID: 9771 RVA: 0x000B05A0 File Offset: 0x000AE7A0
		[SecurityCritical]
		internal void remove_CollectionChanged(EventRegistrationToken token)
		{
			INotifyCollectionChanged notifyCollectionChanged = JitHelpers.UnsafeCast<INotifyCollectionChanged>(this);
			EventRegistrationTokenTable<NotifyCollectionChangedEventHandler> orCreateValue = NotifyCollectionChangedToWinRTAdapter.m_weakTable.GetOrCreateValue(notifyCollectionChanged);
			NotifyCollectionChangedEventHandler notifyCollectionChangedEventHandler = orCreateValue.ExtractHandler(token);
			if (notifyCollectionChangedEventHandler != null)
			{
				notifyCollectionChanged.CollectionChanged -= notifyCollectionChangedEventHandler;
			}
		}

		// Token: 0x04002093 RID: 8339
		private static ConditionalWeakTable<INotifyCollectionChanged, EventRegistrationTokenTable<NotifyCollectionChangedEventHandler>> m_weakTable = new ConditionalWeakTable<INotifyCollectionChanged, EventRegistrationTokenTable<NotifyCollectionChangedEventHandler>>();
	}
}
