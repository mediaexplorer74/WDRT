using System;
using System.ComponentModel;
using System.Security;
using System.StubHelpers;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020003F0 RID: 1008
	internal static class PropertyChangedEventArgsMarshaler
	{
		// Token: 0x06002624 RID: 9764 RVA: 0x000B04AD File Offset: 0x000AE6AD
		[SecurityCritical]
		internal static IntPtr ConvertToNative(PropertyChangedEventArgs managedArgs)
		{
			if (managedArgs == null)
			{
				return IntPtr.Zero;
			}
			return EventArgsMarshaler.CreateNativePCEventArgsInstance(managedArgs.PropertyName);
		}

		// Token: 0x06002625 RID: 9765 RVA: 0x000B04C4 File Offset: 0x000AE6C4
		[SecurityCritical]
		internal static PropertyChangedEventArgs ConvertToManaged(IntPtr nativeArgsIP)
		{
			if (nativeArgsIP == IntPtr.Zero)
			{
				return null;
			}
			object obj = InterfaceMarshaler.ConvertToManagedWithoutUnboxing(nativeArgsIP);
			IPropertyChangedEventArgs propertyChangedEventArgs = (IPropertyChangedEventArgs)obj;
			return new PropertyChangedEventArgs(propertyChangedEventArgs.PropertyName);
		}
	}
}
