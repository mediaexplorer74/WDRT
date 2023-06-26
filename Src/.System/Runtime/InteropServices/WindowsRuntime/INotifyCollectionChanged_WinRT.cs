using System;
using System.Collections.Specialized;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020003EA RID: 1002
	[Guid("28b167d5-1a31-465b-9b25-d5c3ae686c40")]
	[ComImport]
	internal interface INotifyCollectionChanged_WinRT
	{
		// Token: 0x06002612 RID: 9746
		EventRegistrationToken add_CollectionChanged(NotifyCollectionChangedEventHandler value);

		// Token: 0x06002613 RID: 9747
		void remove_CollectionChanged(EventRegistrationToken token);
	}
}
