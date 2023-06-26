using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009CA RID: 2506
	[DebuggerDisplay("Count = {Count}")]
	[Serializable]
	internal sealed class ConstantSplittableMap<TKey, TValue> : IMapView<TKey, TValue>, IIterable<IKeyValuePair<TKey, TValue>>, IEnumerable<IKeyValuePair<TKey, TValue>>, IEnumerable
	{
		// Token: 0x060063E3 RID: 25571 RVA: 0x001560BC File Offset: 0x001542BC
		internal ConstantSplittableMap(IReadOnlyDictionary<TKey, TValue> data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this.firstItemIndex = 0;
			this.lastItemIndex = data.Count - 1;
			this.items = this.CreateKeyValueArray(data.Count, data.GetEnumerator());
		}

		// Token: 0x060063E4 RID: 25572 RVA: 0x0015610C File Offset: 0x0015430C
		internal ConstantSplittableMap(IMapView<TKey, TValue> data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (2147483647U < data.Size)
			{
				Exception ex = new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingDictionaryTooLarge"));
				ex.SetErrorCode(-2147483637);
				throw ex;
			}
			int size = (int)data.Size;
			this.firstItemIndex = 0;
			this.lastItemIndex = size - 1;
			this.items = this.CreateKeyValueArray(size, data.GetEnumerator());
		}

		// Token: 0x060063E5 RID: 25573 RVA: 0x00156181 File Offset: 0x00154381
		private ConstantSplittableMap(KeyValuePair<TKey, TValue>[] items, int firstItemIndex, int lastItemIndex)
		{
			this.items = items;
			this.firstItemIndex = firstItemIndex;
			this.lastItemIndex = lastItemIndex;
		}

		// Token: 0x060063E6 RID: 25574 RVA: 0x001561A0 File Offset: 0x001543A0
		private KeyValuePair<TKey, TValue>[] CreateKeyValueArray(int count, IEnumerator<KeyValuePair<TKey, TValue>> data)
		{
			KeyValuePair<TKey, TValue>[] array = new KeyValuePair<TKey, TValue>[count];
			int num = 0;
			while (data.MoveNext())
			{
				KeyValuePair<TKey, TValue> keyValuePair = data.Current;
				array[num++] = keyValuePair;
			}
			Array.Sort<KeyValuePair<TKey, TValue>>(array, ConstantSplittableMap<TKey, TValue>.keyValuePairComparator);
			return array;
		}

		// Token: 0x060063E7 RID: 25575 RVA: 0x001561E0 File Offset: 0x001543E0
		private KeyValuePair<TKey, TValue>[] CreateKeyValueArray(int count, IEnumerator<IKeyValuePair<TKey, TValue>> data)
		{
			KeyValuePair<TKey, TValue>[] array = new KeyValuePair<TKey, TValue>[count];
			int num = 0;
			while (data.MoveNext())
			{
				IKeyValuePair<TKey, TValue> keyValuePair = data.Current;
				array[num++] = new KeyValuePair<TKey, TValue>(keyValuePair.Key, keyValuePair.Value);
			}
			Array.Sort<KeyValuePair<TKey, TValue>>(array, ConstantSplittableMap<TKey, TValue>.keyValuePairComparator);
			return array;
		}

		// Token: 0x17001140 RID: 4416
		// (get) Token: 0x060063E8 RID: 25576 RVA: 0x0015622F File Offset: 0x0015442F
		public int Count
		{
			get
			{
				return this.lastItemIndex - this.firstItemIndex + 1;
			}
		}

		// Token: 0x17001141 RID: 4417
		// (get) Token: 0x060063E9 RID: 25577 RVA: 0x00156240 File Offset: 0x00154440
		public uint Size
		{
			get
			{
				return (uint)(this.lastItemIndex - this.firstItemIndex + 1);
			}
		}

		// Token: 0x060063EA RID: 25578 RVA: 0x00156254 File Offset: 0x00154454
		public TValue Lookup(TKey key)
		{
			TValue tvalue;
			if (!this.TryGetValue(key, out tvalue))
			{
				Exception ex = new KeyNotFoundException(Environment.GetResourceString("Arg_KeyNotFound"));
				ex.SetErrorCode(-2147483637);
				throw ex;
			}
			return tvalue;
		}

		// Token: 0x060063EB RID: 25579 RVA: 0x0015628C File Offset: 0x0015448C
		public bool HasKey(TKey key)
		{
			TValue tvalue;
			return this.TryGetValue(key, out tvalue);
		}

		// Token: 0x060063EC RID: 25580 RVA: 0x001562A4 File Offset: 0x001544A4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<IKeyValuePair<TKey, TValue>>)this).GetEnumerator();
		}

		// Token: 0x060063ED RID: 25581 RVA: 0x001562AC File Offset: 0x001544AC
		public IIterator<IKeyValuePair<TKey, TValue>> First()
		{
			return new EnumeratorToIteratorAdapter<IKeyValuePair<TKey, TValue>>(this.GetEnumerator());
		}

		// Token: 0x060063EE RID: 25582 RVA: 0x001562B9 File Offset: 0x001544B9
		public IEnumerator<IKeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return new ConstantSplittableMap<TKey, TValue>.IKeyValuePairEnumerator(this.items, this.firstItemIndex, this.lastItemIndex);
		}

		// Token: 0x060063EF RID: 25583 RVA: 0x001562D8 File Offset: 0x001544D8
		public void Split(out IMapView<TKey, TValue> firstPartition, out IMapView<TKey, TValue> secondPartition)
		{
			if (this.Count < 2)
			{
				firstPartition = null;
				secondPartition = null;
				return;
			}
			int num = (int)(((long)this.firstItemIndex + (long)this.lastItemIndex) / 2L);
			firstPartition = new ConstantSplittableMap<TKey, TValue>(this.items, this.firstItemIndex, num);
			secondPartition = new ConstantSplittableMap<TKey, TValue>(this.items, num + 1, this.lastItemIndex);
		}

		// Token: 0x060063F0 RID: 25584 RVA: 0x00156334 File Offset: 0x00154534
		public bool ContainsKey(TKey key)
		{
			KeyValuePair<TKey, TValue> keyValuePair = new KeyValuePair<TKey, TValue>(key, default(TValue));
			int num = Array.BinarySearch<KeyValuePair<TKey, TValue>>(this.items, this.firstItemIndex, this.Count, keyValuePair, ConstantSplittableMap<TKey, TValue>.keyValuePairComparator);
			return num >= 0;
		}

		// Token: 0x060063F1 RID: 25585 RVA: 0x00156378 File Offset: 0x00154578
		public bool TryGetValue(TKey key, out TValue value)
		{
			KeyValuePair<TKey, TValue> keyValuePair = new KeyValuePair<TKey, TValue>(key, default(TValue));
			int num = Array.BinarySearch<KeyValuePair<TKey, TValue>>(this.items, this.firstItemIndex, this.Count, keyValuePair, ConstantSplittableMap<TKey, TValue>.keyValuePairComparator);
			if (num < 0)
			{
				value = default(TValue);
				return false;
			}
			value = this.items[num].Value;
			return true;
		}

		// Token: 0x17001142 RID: 4418
		public TValue this[TKey key]
		{
			get
			{
				return this.Lookup(key);
			}
		}

		// Token: 0x17001143 RID: 4419
		// (get) Token: 0x060063F3 RID: 25587 RVA: 0x001563E2 File Offset: 0x001545E2
		public IEnumerable<TKey> Keys
		{
			get
			{
				throw new NotImplementedException("NYI");
			}
		}

		// Token: 0x17001144 RID: 4420
		// (get) Token: 0x060063F4 RID: 25588 RVA: 0x001563EE File Offset: 0x001545EE
		public IEnumerable<TValue> Values
		{
			get
			{
				throw new NotImplementedException("NYI");
			}
		}

		// Token: 0x04002CE9 RID: 11497
		private static readonly ConstantSplittableMap<TKey, TValue>.KeyValuePairComparator keyValuePairComparator = new ConstantSplittableMap<TKey, TValue>.KeyValuePairComparator();

		// Token: 0x04002CEA RID: 11498
		private readonly KeyValuePair<TKey, TValue>[] items;

		// Token: 0x04002CEB RID: 11499
		private readonly int firstItemIndex;

		// Token: 0x04002CEC RID: 11500
		private readonly int lastItemIndex;

		// Token: 0x02000C9B RID: 3227
		private class KeyValuePairComparator : IComparer<KeyValuePair<TKey, TValue>>
		{
			// Token: 0x0600713E RID: 28990 RVA: 0x00186EB9 File Offset: 0x001850B9
			public int Compare(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
			{
				return ConstantSplittableMap<TKey, TValue>.KeyValuePairComparator.keyComparator.Compare(x.Key, y.Key);
			}

			// Token: 0x0400387A RID: 14458
			private static readonly IComparer<TKey> keyComparator = Comparer<TKey>.Default;
		}

		// Token: 0x02000C9C RID: 3228
		[Serializable]
		internal struct IKeyValuePairEnumerator : IEnumerator<IKeyValuePair<TKey, TValue>>, IDisposable, IEnumerator
		{
			// Token: 0x06007141 RID: 28993 RVA: 0x00186EE7 File Offset: 0x001850E7
			internal IKeyValuePairEnumerator(KeyValuePair<TKey, TValue>[] items, int first, int end)
			{
				this._array = items;
				this._start = first;
				this._end = end;
				this._current = this._start - 1;
			}

			// Token: 0x06007142 RID: 28994 RVA: 0x00186F0C File Offset: 0x0018510C
			public bool MoveNext()
			{
				if (this._current < this._end)
				{
					this._current++;
					return true;
				}
				return false;
			}

			// Token: 0x17001367 RID: 4967
			// (get) Token: 0x06007143 RID: 28995 RVA: 0x00186F30 File Offset: 0x00185130
			public IKeyValuePair<TKey, TValue> Current
			{
				get
				{
					if (this._current < this._start)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
					}
					if (this._current > this._end)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
					}
					return new CLRIKeyValuePairImpl<TKey, TValue>(ref this._array[this._current]);
				}
			}

			// Token: 0x17001368 RID: 4968
			// (get) Token: 0x06007144 RID: 28996 RVA: 0x00186F8F File Offset: 0x0018518F
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06007145 RID: 28997 RVA: 0x00186F97 File Offset: 0x00185197
			void IEnumerator.Reset()
			{
				this._current = this._start - 1;
			}

			// Token: 0x06007146 RID: 28998 RVA: 0x00186FA7 File Offset: 0x001851A7
			public void Dispose()
			{
			}

			// Token: 0x0400387B RID: 14459
			private KeyValuePair<TKey, TValue>[] _array;

			// Token: 0x0400387C RID: 14460
			private int _start;

			// Token: 0x0400387D RID: 14461
			private int _end;

			// Token: 0x0400387E RID: 14462
			private int _current;
		}
	}
}
