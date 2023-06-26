using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;

namespace System.StubHelpers
{
	// Token: 0x020005A6 RID: 1446
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class KeyValuePairMarshaler
	{
		// Token: 0x06004342 RID: 17218 RVA: 0x000FBA60 File Offset: 0x000F9C60
		[SecurityCritical]
		internal static IntPtr ConvertToNative<K, V>([In] ref KeyValuePair<K, V> pair)
		{
			IKeyValuePair<K, V> keyValuePair = new CLRIKeyValuePairImpl<K, V>(ref pair);
			return Marshal.GetComInterfaceForObject(keyValuePair, typeof(IKeyValuePair<K, V>));
		}

		// Token: 0x06004343 RID: 17219 RVA: 0x000FBA84 File Offset: 0x000F9C84
		[SecurityCritical]
		internal static KeyValuePair<K, V> ConvertToManaged<K, V>(IntPtr pInsp)
		{
			object obj = InterfaceMarshaler.ConvertToManagedWithoutUnboxing(pInsp);
			IKeyValuePair<K, V> keyValuePair = (IKeyValuePair<K, V>)obj;
			return new KeyValuePair<K, V>(keyValuePair.Key, keyValuePair.Value);
		}

		// Token: 0x06004344 RID: 17220 RVA: 0x000FBAB0 File Offset: 0x000F9CB0
		[SecurityCritical]
		internal static object ConvertToManagedBox<K, V>(IntPtr pInsp)
		{
			return KeyValuePairMarshaler.ConvertToManaged<K, V>(pInsp);
		}
	}
}
