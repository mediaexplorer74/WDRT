using System;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020003F1 RID: 1009
	internal sealed class NotifyCollectionChangedToManagedAdapter
	{
		// Token: 0x06002626 RID: 9766 RVA: 0x000B04F9 File Offset: 0x000AE6F9
		private NotifyCollectionChangedToManagedAdapter()
		{
		}

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x06002627 RID: 9767 RVA: 0x000B0504 File Offset: 0x000AE704
		// (remove) Token: 0x06002628 RID: 9768 RVA: 0x000B053C File Offset: 0x000AE73C
		internal event NotifyCollectionChangedEventHandler CollectionChanged
		{
			[SecurityCritical]
			add
			{
				INotifyCollectionChanged_WinRT notifyCollectionChanged_WinRT = JitHelpers.UnsafeCast<INotifyCollectionChanged_WinRT>(this);
				Func<NotifyCollectionChangedEventHandler, EventRegistrationToken> func = new Func<NotifyCollectionChangedEventHandler, EventRegistrationToken>(notifyCollectionChanged_WinRT.add_CollectionChanged);
				Action<EventRegistrationToken> action = new Action<EventRegistrationToken>(notifyCollectionChanged_WinRT.remove_CollectionChanged);
				WindowsRuntimeMarshal.AddEventHandler<NotifyCollectionChangedEventHandler>(func, action, value);
			}
			[SecurityCritical]
			remove
			{
				INotifyCollectionChanged_WinRT notifyCollectionChanged_WinRT = JitHelpers.UnsafeCast<INotifyCollectionChanged_WinRT>(this);
				Action<EventRegistrationToken> action = new Action<EventRegistrationToken>(notifyCollectionChanged_WinRT.remove_CollectionChanged);
				WindowsRuntimeMarshal.RemoveEventHandler<NotifyCollectionChangedEventHandler>(action, value);
			}
		}
	}
}
