using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009D5 RID: 2517
	internal sealed class MapToDictionaryAdapter
	{
		// Token: 0x06006434 RID: 25652 RVA: 0x00156D08 File Offset: 0x00154F08
		private MapToDictionaryAdapter()
		{
		}

		// Token: 0x06006435 RID: 25653 RVA: 0x00156D10 File Offset: 0x00154F10
		[SecurityCritical]
		internal V Indexer_Get<K, V>(K key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			IMap<K, V> map = JitHelpers.UnsafeCast<IMap<K, V>>(this);
			return MapToDictionaryAdapter.Lookup<K, V>(map, key);
		}

		// Token: 0x06006436 RID: 25654 RVA: 0x00156D40 File Offset: 0x00154F40
		[SecurityCritical]
		internal void Indexer_Set<K, V>(K key, V value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			IMap<K, V> map = JitHelpers.UnsafeCast<IMap<K, V>>(this);
			MapToDictionaryAdapter.Insert<K, V>(map, key, value);
		}

		// Token: 0x06006437 RID: 25655 RVA: 0x00156D70 File Offset: 0x00154F70
		[SecurityCritical]
		internal ICollection<K> Keys<K, V>()
		{
			IMap<K, V> map = JitHelpers.UnsafeCast<IMap<K, V>>(this);
			IDictionary<K, V> dictionary = (IDictionary<K, V>)map;
			return new DictionaryKeyCollection<K, V>(dictionary);
		}

		// Token: 0x06006438 RID: 25656 RVA: 0x00156D94 File Offset: 0x00154F94
		[SecurityCritical]
		internal ICollection<V> Values<K, V>()
		{
			IMap<K, V> map = JitHelpers.UnsafeCast<IMap<K, V>>(this);
			IDictionary<K, V> dictionary = (IDictionary<K, V>)map;
			return new DictionaryValueCollection<K, V>(dictionary);
		}

		// Token: 0x06006439 RID: 25657 RVA: 0x00156DB8 File Offset: 0x00154FB8
		[SecurityCritical]
		internal bool ContainsKey<K, V>(K key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			IMap<K, V> map = JitHelpers.UnsafeCast<IMap<K, V>>(this);
			return map.HasKey(key);
		}

		// Token: 0x0600643A RID: 25658 RVA: 0x00156DE8 File Offset: 0x00154FE8
		[SecurityCritical]
		internal void Add<K, V>(K key, V value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (this.ContainsKey<K, V>(key))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_AddingDuplicate"));
			}
			IMap<K, V> map = JitHelpers.UnsafeCast<IMap<K, V>>(this);
			MapToDictionaryAdapter.Insert<K, V>(map, key, value);
		}

		// Token: 0x0600643B RID: 25659 RVA: 0x00156E34 File Offset: 0x00155034
		[SecurityCritical]
		internal bool Remove<K, V>(K key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			IMap<K, V> map = JitHelpers.UnsafeCast<IMap<K, V>>(this);
			if (!map.HasKey(key))
			{
				return false;
			}
			bool flag;
			try
			{
				map.Remove(key);
				flag = true;
			}
			catch (Exception ex)
			{
				if (-2147483637 != ex._HResult)
				{
					throw;
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600643C RID: 25660 RVA: 0x00156E98 File Offset: 0x00155098
		[SecurityCritical]
		internal bool TryGetValue<K, V>(K key, out V value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			IMap<K, V> map = JitHelpers.UnsafeCast<IMap<K, V>>(this);
			if (!map.HasKey(key))
			{
				value = default(V);
				return false;
			}
			bool flag;
			try
			{
				value = MapToDictionaryAdapter.Lookup<K, V>(map, key);
				flag = true;
			}
			catch (KeyNotFoundException)
			{
				value = default(V);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600643D RID: 25661 RVA: 0x00156F00 File Offset: 0x00155100
		private static V Lookup<K, V>(IMap<K, V> _this, K key)
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

		// Token: 0x0600643E RID: 25662 RVA: 0x00156F48 File Offset: 0x00155148
		private static bool Insert<K, V>(IMap<K, V> _this, K key, V value)
		{
			return _this.Insert(key, value);
		}
	}
}
