using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009D6 RID: 2518
	internal sealed class MapToCollectionAdapter
	{
		// Token: 0x0600643F RID: 25663 RVA: 0x00156F5F File Offset: 0x0015515F
		private MapToCollectionAdapter()
		{
		}

		// Token: 0x06006440 RID: 25664 RVA: 0x00156F68 File Offset: 0x00155168
		[SecurityCritical]
		internal int Count<K, V>()
		{
			object obj = JitHelpers.UnsafeCast<object>(this);
			IMap<K, V> map = obj as IMap<K, V>;
			if (map != null)
			{
				uint size = map.Size;
				if (2147483647U < size)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingDictionaryTooLarge"));
				}
				return (int)size;
			}
			else
			{
				IVector<KeyValuePair<K, V>> vector = JitHelpers.UnsafeCast<IVector<KeyValuePair<K, V>>>(this);
				uint size2 = vector.Size;
				if (2147483647U < size2)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
				}
				return (int)size2;
			}
		}

		// Token: 0x06006441 RID: 25665 RVA: 0x00156FD1 File Offset: 0x001551D1
		[SecurityCritical]
		internal bool IsReadOnly<K, V>()
		{
			return false;
		}

		// Token: 0x06006442 RID: 25666 RVA: 0x00156FD4 File Offset: 0x001551D4
		[SecurityCritical]
		internal void Add<K, V>(KeyValuePair<K, V> item)
		{
			object obj = JitHelpers.UnsafeCast<object>(this);
			IDictionary<K, V> dictionary = obj as IDictionary<K, V>;
			if (dictionary != null)
			{
				dictionary.Add(item.Key, item.Value);
				return;
			}
			IVector<KeyValuePair<K, V>> vector = JitHelpers.UnsafeCast<IVector<KeyValuePair<K, V>>>(this);
			vector.Append(item);
		}

		// Token: 0x06006443 RID: 25667 RVA: 0x00157018 File Offset: 0x00155218
		[SecurityCritical]
		internal void Clear<K, V>()
		{
			object obj = JitHelpers.UnsafeCast<object>(this);
			IMap<K, V> map = obj as IMap<K, V>;
			if (map != null)
			{
				map.Clear();
				return;
			}
			IVector<KeyValuePair<K, V>> vector = JitHelpers.UnsafeCast<IVector<KeyValuePair<K, V>>>(this);
			vector.Clear();
		}

		// Token: 0x06006444 RID: 25668 RVA: 0x0015704C File Offset: 0x0015524C
		[SecurityCritical]
		internal bool Contains<K, V>(KeyValuePair<K, V> item)
		{
			object obj = JitHelpers.UnsafeCast<object>(this);
			IDictionary<K, V> dictionary = obj as IDictionary<K, V>;
			if (dictionary != null)
			{
				V v;
				return dictionary.TryGetValue(item.Key, out v) && EqualityComparer<V>.Default.Equals(v, item.Value);
			}
			IVector<KeyValuePair<K, V>> vector = JitHelpers.UnsafeCast<IVector<KeyValuePair<K, V>>>(this);
			uint num;
			return vector.IndexOf(item, out num);
		}

		// Token: 0x06006445 RID: 25669 RVA: 0x001570A4 File Offset: 0x001552A4
		[SecurityCritical]
		internal void CopyTo<K, V>(KeyValuePair<K, V>[] array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex");
			}
			if (array.Length <= arrayIndex && this.Count<K, V>() > 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IndexOutOfArrayBounds"));
			}
			if (array.Length - arrayIndex < this.Count<K, V>())
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InsufficientSpaceToCopyCollection"));
			}
			IIterable<KeyValuePair<K, V>> iterable = JitHelpers.UnsafeCast<IIterable<KeyValuePair<K, V>>>(this);
			foreach (KeyValuePair<K, V> keyValuePair in iterable)
			{
				array[arrayIndex++] = keyValuePair;
			}
		}

		// Token: 0x06006446 RID: 25670 RVA: 0x00157154 File Offset: 0x00155354
		[SecurityCritical]
		internal bool Remove<K, V>(KeyValuePair<K, V> item)
		{
			object obj = JitHelpers.UnsafeCast<object>(this);
			IDictionary<K, V> dictionary = obj as IDictionary<K, V>;
			if (dictionary != null)
			{
				return dictionary.Remove(item.Key);
			}
			IVector<KeyValuePair<K, V>> vector = JitHelpers.UnsafeCast<IVector<KeyValuePair<K, V>>>(this);
			uint num;
			if (!vector.IndexOf(item, out num))
			{
				return false;
			}
			if (2147483647U < num)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
			}
			VectorToListAdapter.RemoveAtHelper<KeyValuePair<K, V>>(vector, num);
			return true;
		}
	}
}
