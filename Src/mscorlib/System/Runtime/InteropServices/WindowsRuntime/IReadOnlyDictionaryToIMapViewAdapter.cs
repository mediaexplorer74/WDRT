using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009E9 RID: 2537
	[DebuggerDisplay("Size = {Size}")]
	internal sealed class IReadOnlyDictionaryToIMapViewAdapter
	{
		// Token: 0x060064BB RID: 25787 RVA: 0x001585D0 File Offset: 0x001567D0
		private IReadOnlyDictionaryToIMapViewAdapter()
		{
		}

		// Token: 0x060064BC RID: 25788 RVA: 0x001585D8 File Offset: 0x001567D8
		[SecurityCritical]
		internal V Lookup<K, V>(K key)
		{
			IReadOnlyDictionary<K, V> readOnlyDictionary = JitHelpers.UnsafeCast<IReadOnlyDictionary<K, V>>(this);
			V v;
			if (!readOnlyDictionary.TryGetValue(key, out v))
			{
				Exception ex = new KeyNotFoundException(Environment.GetResourceString("Arg_KeyNotFound"));
				ex.SetErrorCode(-2147483637);
				throw ex;
			}
			return v;
		}

		// Token: 0x060064BD RID: 25789 RVA: 0x00158618 File Offset: 0x00156818
		[SecurityCritical]
		internal uint Size<K, V>()
		{
			IReadOnlyDictionary<K, V> readOnlyDictionary = JitHelpers.UnsafeCast<IReadOnlyDictionary<K, V>>(this);
			return (uint)readOnlyDictionary.Count;
		}

		// Token: 0x060064BE RID: 25790 RVA: 0x00158634 File Offset: 0x00156834
		[SecurityCritical]
		internal bool HasKey<K, V>(K key)
		{
			IReadOnlyDictionary<K, V> readOnlyDictionary = JitHelpers.UnsafeCast<IReadOnlyDictionary<K, V>>(this);
			return readOnlyDictionary.ContainsKey(key);
		}

		// Token: 0x060064BF RID: 25791 RVA: 0x00158650 File Offset: 0x00156850
		[SecurityCritical]
		internal void Split<K, V>(out IMapView<K, V> first, out IMapView<K, V> second)
		{
			IReadOnlyDictionary<K, V> readOnlyDictionary = JitHelpers.UnsafeCast<IReadOnlyDictionary<K, V>>(this);
			if (readOnlyDictionary.Count < 2)
			{
				first = null;
				second = null;
				return;
			}
			ConstantSplittableMap<K, V> constantSplittableMap = readOnlyDictionary as ConstantSplittableMap<K, V>;
			if (constantSplittableMap == null)
			{
				constantSplittableMap = new ConstantSplittableMap<K, V>(readOnlyDictionary);
			}
			constantSplittableMap.Split(out first, out second);
		}
	}
}
