using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009E2 RID: 2530
	[DebuggerDisplay("Count = {Count}")]
	internal sealed class IMapViewToIReadOnlyDictionaryAdapter
	{
		// Token: 0x0600649B RID: 25755 RVA: 0x00158236 File Offset: 0x00156436
		private IMapViewToIReadOnlyDictionaryAdapter()
		{
		}

		// Token: 0x0600649C RID: 25756 RVA: 0x00158240 File Offset: 0x00156440
		[SecurityCritical]
		internal V Indexer_Get<K, V>(K key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			IMapView<K, V> mapView = JitHelpers.UnsafeCast<IMapView<K, V>>(this);
			return IMapViewToIReadOnlyDictionaryAdapter.Lookup<K, V>(mapView, key);
		}

		// Token: 0x0600649D RID: 25757 RVA: 0x00158270 File Offset: 0x00156470
		[SecurityCritical]
		internal IEnumerable<K> Keys<K, V>()
		{
			IMapView<K, V> mapView = JitHelpers.UnsafeCast<IMapView<K, V>>(this);
			IReadOnlyDictionary<K, V> readOnlyDictionary = (IReadOnlyDictionary<K, V>)mapView;
			return new ReadOnlyDictionaryKeyCollection<K, V>(readOnlyDictionary);
		}

		// Token: 0x0600649E RID: 25758 RVA: 0x00158294 File Offset: 0x00156494
		[SecurityCritical]
		internal IEnumerable<V> Values<K, V>()
		{
			IMapView<K, V> mapView = JitHelpers.UnsafeCast<IMapView<K, V>>(this);
			IReadOnlyDictionary<K, V> readOnlyDictionary = (IReadOnlyDictionary<K, V>)mapView;
			return new ReadOnlyDictionaryValueCollection<K, V>(readOnlyDictionary);
		}

		// Token: 0x0600649F RID: 25759 RVA: 0x001582B8 File Offset: 0x001564B8
		[SecurityCritical]
		internal bool ContainsKey<K, V>(K key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			IMapView<K, V> mapView = JitHelpers.UnsafeCast<IMapView<K, V>>(this);
			return mapView.HasKey(key);
		}

		// Token: 0x060064A0 RID: 25760 RVA: 0x001582E8 File Offset: 0x001564E8
		[SecurityCritical]
		internal bool TryGetValue<K, V>(K key, out V value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			IMapView<K, V> mapView = JitHelpers.UnsafeCast<IMapView<K, V>>(this);
			if (!mapView.HasKey(key))
			{
				value = default(V);
				return false;
			}
			bool flag;
			try
			{
				value = mapView.Lookup(key);
				flag = true;
			}
			catch (Exception ex)
			{
				if (-2147483637 != ex._HResult)
				{
					throw;
				}
				value = default(V);
				flag = false;
			}
			return flag;
		}

		// Token: 0x060064A1 RID: 25761 RVA: 0x00158360 File Offset: 0x00156560
		private static V Lookup<K, V>(IMapView<K, V> _this, K key)
		{
			V v;
			try
			{
				v = _this.Lookup(key);
			}
			catch (Exception ex)
			{
				if (-2147483637 == ex._HResult)
				{
					throw new KeyNotFoundException(Environment.GetResourceString("Arg_KeyNotFound"));
				}
				throw;
			}
			return v;
		}
	}
}
