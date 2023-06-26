using System;
using System.Collections.Specialized;
using System.Security;
using System.StubHelpers;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020003EF RID: 1007
	internal static class NotifyCollectionChangedEventArgsMarshaler
	{
		// Token: 0x06002622 RID: 9762 RVA: 0x000B0431 File Offset: 0x000AE631
		[SecurityCritical]
		internal static IntPtr ConvertToNative(NotifyCollectionChangedEventArgs managedArgs)
		{
			if (managedArgs == null)
			{
				return IntPtr.Zero;
			}
			return EventArgsMarshaler.CreateNativeNCCEventArgsInstance((int)managedArgs.Action, managedArgs.NewItems, managedArgs.OldItems, managedArgs.NewStartingIndex, managedArgs.OldStartingIndex);
		}

		// Token: 0x06002623 RID: 9763 RVA: 0x000B0460 File Offset: 0x000AE660
		[SecurityCritical]
		internal static NotifyCollectionChangedEventArgs ConvertToManaged(IntPtr nativeArgsIP)
		{
			if (nativeArgsIP == IntPtr.Zero)
			{
				return null;
			}
			object obj = InterfaceMarshaler.ConvertToManagedWithoutUnboxing(nativeArgsIP);
			INotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs = (INotifyCollectionChangedEventArgs)obj;
			return new NotifyCollectionChangedEventArgs(notifyCollectionChangedEventArgs.Action, notifyCollectionChangedEventArgs.NewItems, notifyCollectionChangedEventArgs.OldItems, notifyCollectionChangedEventArgs.NewStartingIndex, notifyCollectionChangedEventArgs.OldStartingIndex);
		}
	}
}
