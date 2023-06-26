using System;

namespace System.Collections
{
	// Token: 0x02000494 RID: 1172
	[Serializable]
	internal sealed class EmptyReadOnlyDictionaryInternal : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06003867 RID: 14439 RVA: 0x000D9A25 File Offset: 0x000D7C25
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new EmptyReadOnlyDictionaryInternal.NodeEnumerator();
		}

		// Token: 0x06003868 RID: 14440 RVA: 0x000D9A2C File Offset: 0x000D7C2C
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - index < this.Count)
			{
				throw new ArgumentException(Environment.GetResourceString("ArgumentOutOfRange_Index"), "index");
			}
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06003869 RID: 14441 RVA: 0x000D9A9E File Offset: 0x000D7C9E
		public int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x0600386A RID: 14442 RVA: 0x000D9AA1 File Offset: 0x000D7CA1
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x0600386B RID: 14443 RVA: 0x000D9AA4 File Offset: 0x000D7CA4
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700085A RID: 2138
		public object this[object key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
				}
				return null;
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
				}
				if (!key.GetType().IsSerializable)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_NotSerializable"), "key");
				}
				if (value != null && !value.GetType().IsSerializable)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_NotSerializable"), "value");
				}
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x0600386E RID: 14446 RVA: 0x000D9B3F File Offset: 0x000D7D3F
		public ICollection Keys
		{
			get
			{
				return EmptyArray<object>.Value;
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x0600386F RID: 14447 RVA: 0x000D9B46 File Offset: 0x000D7D46
		public ICollection Values
		{
			get
			{
				return EmptyArray<object>.Value;
			}
		}

		// Token: 0x06003870 RID: 14448 RVA: 0x000D9B4D File Offset: 0x000D7D4D
		public bool Contains(object key)
		{
			return false;
		}

		// Token: 0x06003871 RID: 14449 RVA: 0x000D9B50 File Offset: 0x000D7D50
		public void Add(object key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
			}
			if (!key.GetType().IsSerializable)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NotSerializable"), "key");
			}
			if (value != null && !value.GetType().IsSerializable)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NotSerializable"), "value");
			}
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
		}

		// Token: 0x06003872 RID: 14450 RVA: 0x000D9BCB File Offset: 0x000D7DCB
		public void Clear()
		{
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06003873 RID: 14451 RVA: 0x000D9BDC File Offset: 0x000D7DDC
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06003874 RID: 14452 RVA: 0x000D9BDF File Offset: 0x000D7DDF
		public bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003875 RID: 14453 RVA: 0x000D9BE2 File Offset: 0x000D7DE2
		public IDictionaryEnumerator GetEnumerator()
		{
			return new EmptyReadOnlyDictionaryInternal.NodeEnumerator();
		}

		// Token: 0x06003876 RID: 14454 RVA: 0x000D9BE9 File Offset: 0x000D7DE9
		public void Remove(object key)
		{
			throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
		}

		// Token: 0x02000BB0 RID: 2992
		private sealed class NodeEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06006E04 RID: 28164 RVA: 0x0017D393 File Offset: 0x0017B593
			public bool MoveNext()
			{
				return false;
			}

			// Token: 0x170012AC RID: 4780
			// (get) Token: 0x06006E05 RID: 28165 RVA: 0x0017D396 File Offset: 0x0017B596
			public object Current
			{
				get
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
				}
			}

			// Token: 0x06006E06 RID: 28166 RVA: 0x0017D3A7 File Offset: 0x0017B5A7
			public void Reset()
			{
			}

			// Token: 0x170012AD RID: 4781
			// (get) Token: 0x06006E07 RID: 28167 RVA: 0x0017D3A9 File Offset: 0x0017B5A9
			public object Key
			{
				get
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
				}
			}

			// Token: 0x170012AE RID: 4782
			// (get) Token: 0x06006E08 RID: 28168 RVA: 0x0017D3BA File Offset: 0x0017B5BA
			public object Value
			{
				get
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
				}
			}

			// Token: 0x170012AF RID: 4783
			// (get) Token: 0x06006E09 RID: 28169 RVA: 0x0017D3CB File Offset: 0x0017B5CB
			public DictionaryEntry Entry
			{
				get
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
				}
			}
		}
	}
}
