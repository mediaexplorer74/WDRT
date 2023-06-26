using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009D9 RID: 2521
	internal sealed class DictionaryToMapAdapter
	{
		// Token: 0x06006458 RID: 25688 RVA: 0x0015757F File Offset: 0x0015577F
		private DictionaryToMapAdapter()
		{
		}

		// Token: 0x06006459 RID: 25689 RVA: 0x00157588 File Offset: 0x00155788
		[SecurityCritical]
		internal V Lookup<K, V>(K key)
		{
			IDictionary<K, V> dictionary = JitHelpers.UnsafeCast<IDictionary<K, V>>(this);
			V v;
			if (!dictionary.TryGetValue(key, out v))
			{
				Exception ex = new KeyNotFoundException(Environment.GetResourceString("Arg_KeyNotFound"));
				ex.SetErrorCode(-2147483637);
				throw ex;
			}
			return v;
		}

		// Token: 0x0600645A RID: 25690 RVA: 0x001575C8 File Offset: 0x001557C8
		[SecurityCritical]
		internal uint Size<K, V>()
		{
			IDictionary<K, V> dictionary = JitHelpers.UnsafeCast<IDictionary<K, V>>(this);
			return (uint)dictionary.Count;
		}

		// Token: 0x0600645B RID: 25691 RVA: 0x001575E4 File Offset: 0x001557E4
		[SecurityCritical]
		internal bool HasKey<K, V>(K key)
		{
			IDictionary<K, V> dictionary = JitHelpers.UnsafeCast<IDictionary<K, V>>(this);
			return dictionary.ContainsKey(key);
		}

		// Token: 0x0600645C RID: 25692 RVA: 0x00157600 File Offset: 0x00155800
		[SecurityCritical]
		internal IReadOnlyDictionary<K, V> GetView<K, V>()
		{
			IDictionary<K, V> dictionary = JitHelpers.UnsafeCast<IDictionary<K, V>>(this);
			IReadOnlyDictionary<K, V> readOnlyDictionary = dictionary as IReadOnlyDictionary<K, V>;
			if (readOnlyDictionary == null)
			{
				readOnlyDictionary = new ReadOnlyDictionary<K, V>(dictionary);
			}
			return readOnlyDictionary;
		}

		// Token: 0x0600645D RID: 25693 RVA: 0x00157628 File Offset: 0x00155828
		[SecurityCritical]
		internal bool Insert<K, V>(K key, V value)
		{
			IDictionary<K, V> dictionary = JitHelpers.UnsafeCast<IDictionary<K, V>>(this);
			bool flag = dictionary.ContainsKey(key);
			dictionary[key] = value;
			return flag;
		}

		// Token: 0x0600645E RID: 25694 RVA: 0x00157650 File Offset: 0x00155850
		[SecurityCritical]
		internal void Remove<K, V>(K key)
		{
			IDictionary<K, V> dictionary = JitHelpers.UnsafeCast<IDictionary<K, V>>(this);
			if (!dictionary.Remove(key))
			{
				Exception ex = new KeyNotFoundException(Environment.GetResourceString("Arg_KeyNotFound"));
				ex.SetErrorCode(-2147483637);
				throw ex;
			}
		}

		// Token: 0x0600645F RID: 25695 RVA: 0x0015768C File Offset: 0x0015588C
		[SecurityCritical]
		internal void Clear<K, V>()
		{
			IDictionary<K, V> dictionary = JitHelpers.UnsafeCast<IDictionary<K, V>>(this);
			dictionary.Clear();
		}
	}
}
