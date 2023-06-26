using System;
using System.Threading;

namespace System.Collections
{
	// Token: 0x02000493 RID: 1171
	[Serializable]
	internal class ListDictionaryInternal : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x1700084F RID: 2127
		public object this[object key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
				}
				for (ListDictionaryInternal.DictionaryNode next = this.head; next != null; next = next.next)
				{
					if (next.key.Equals(key))
					{
						return next.value;
					}
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
				this.version++;
				ListDictionaryInternal.DictionaryNode dictionaryNode = null;
				ListDictionaryInternal.DictionaryNode next = this.head;
				while (next != null && !next.key.Equals(key))
				{
					dictionaryNode = next;
					next = next.next;
				}
				if (next != null)
				{
					next.value = value;
					return;
				}
				ListDictionaryInternal.DictionaryNode dictionaryNode2 = new ListDictionaryInternal.DictionaryNode();
				dictionaryNode2.key = key;
				dictionaryNode2.value = value;
				if (dictionaryNode != null)
				{
					dictionaryNode.next = dictionaryNode2;
				}
				else
				{
					this.head = dictionaryNode2;
				}
				this.count++;
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06003858 RID: 14424 RVA: 0x000D9727 File Offset: 0x000D7927
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06003859 RID: 14425 RVA: 0x000D972F File Offset: 0x000D792F
		public ICollection Keys
		{
			get
			{
				return new ListDictionaryInternal.NodeKeyValueCollection(this, true);
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x0600385A RID: 14426 RVA: 0x000D9738 File Offset: 0x000D7938
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x0600385B RID: 14427 RVA: 0x000D973B File Offset: 0x000D793B
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x0600385C RID: 14428 RVA: 0x000D973E File Offset: 0x000D793E
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x0600385D RID: 14429 RVA: 0x000D9741 File Offset: 0x000D7941
		public object SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x0600385E RID: 14430 RVA: 0x000D9763 File Offset: 0x000D7963
		public ICollection Values
		{
			get
			{
				return new ListDictionaryInternal.NodeKeyValueCollection(this, false);
			}
		}

		// Token: 0x0600385F RID: 14431 RVA: 0x000D976C File Offset: 0x000D796C
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
			this.version++;
			ListDictionaryInternal.DictionaryNode dictionaryNode = null;
			ListDictionaryInternal.DictionaryNode next;
			for (next = this.head; next != null; next = next.next)
			{
				if (next.key.Equals(key))
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_AddingDuplicate__", new object[] { next.key, key }));
				}
				dictionaryNode = next;
			}
			if (next != null)
			{
				next.value = value;
				return;
			}
			ListDictionaryInternal.DictionaryNode dictionaryNode2 = new ListDictionaryInternal.DictionaryNode();
			dictionaryNode2.key = key;
			dictionaryNode2.value = value;
			if (dictionaryNode != null)
			{
				dictionaryNode.next = dictionaryNode2;
			}
			else
			{
				this.head = dictionaryNode2;
			}
			this.count++;
		}

		// Token: 0x06003860 RID: 14432 RVA: 0x000D986E File Offset: 0x000D7A6E
		public void Clear()
		{
			this.count = 0;
			this.head = null;
			this.version++;
		}

		// Token: 0x06003861 RID: 14433 RVA: 0x000D988C File Offset: 0x000D7A8C
		public bool Contains(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
			}
			for (ListDictionaryInternal.DictionaryNode next = this.head; next != null; next = next.next)
			{
				if (next.key.Equals(key))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003862 RID: 14434 RVA: 0x000D98D8 File Offset: 0x000D7AD8
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
			for (ListDictionaryInternal.DictionaryNode next = this.head; next != null; next = next.next)
			{
				array.SetValue(new DictionaryEntry(next.key, next.value), index);
				index++;
			}
		}

		// Token: 0x06003863 RID: 14435 RVA: 0x000D997F File Offset: 0x000D7B7F
		public IDictionaryEnumerator GetEnumerator()
		{
			return new ListDictionaryInternal.NodeEnumerator(this);
		}

		// Token: 0x06003864 RID: 14436 RVA: 0x000D9987 File Offset: 0x000D7B87
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ListDictionaryInternal.NodeEnumerator(this);
		}

		// Token: 0x06003865 RID: 14437 RVA: 0x000D9990 File Offset: 0x000D7B90
		public void Remove(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
			}
			this.version++;
			ListDictionaryInternal.DictionaryNode dictionaryNode = null;
			ListDictionaryInternal.DictionaryNode next = this.head;
			while (next != null && !next.key.Equals(key))
			{
				dictionaryNode = next;
				next = next.next;
			}
			if (next == null)
			{
				return;
			}
			if (next == this.head)
			{
				this.head = next.next;
			}
			else
			{
				dictionaryNode.next = next.next;
			}
			this.count--;
		}

		// Token: 0x040018E5 RID: 6373
		private ListDictionaryInternal.DictionaryNode head;

		// Token: 0x040018E6 RID: 6374
		private int version;

		// Token: 0x040018E7 RID: 6375
		private int count;

		// Token: 0x040018E8 RID: 6376
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x02000BAD RID: 2989
		private class NodeEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06006DF5 RID: 28149 RVA: 0x0017D108 File Offset: 0x0017B308
			public NodeEnumerator(ListDictionaryInternal list)
			{
				this.list = list;
				this.version = list.version;
				this.start = true;
				this.current = null;
			}

			// Token: 0x170012A5 RID: 4773
			// (get) Token: 0x06006DF6 RID: 28150 RVA: 0x0017D131 File Offset: 0x0017B331
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x170012A6 RID: 4774
			// (get) Token: 0x06006DF7 RID: 28151 RVA: 0x0017D13E File Offset: 0x0017B33E
			public DictionaryEntry Entry
			{
				get
				{
					if (this.current == null)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
					}
					return new DictionaryEntry(this.current.key, this.current.value);
				}
			}

			// Token: 0x170012A7 RID: 4775
			// (get) Token: 0x06006DF8 RID: 28152 RVA: 0x0017D173 File Offset: 0x0017B373
			public object Key
			{
				get
				{
					if (this.current == null)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
					}
					return this.current.key;
				}
			}

			// Token: 0x170012A8 RID: 4776
			// (get) Token: 0x06006DF9 RID: 28153 RVA: 0x0017D198 File Offset: 0x0017B398
			public object Value
			{
				get
				{
					if (this.current == null)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
					}
					return this.current.value;
				}
			}

			// Token: 0x06006DFA RID: 28154 RVA: 0x0017D1C0 File Offset: 0x0017B3C0
			public bool MoveNext()
			{
				if (this.version != this.list.version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				if (this.start)
				{
					this.current = this.list.head;
					this.start = false;
				}
				else if (this.current != null)
				{
					this.current = this.current.next;
				}
				return this.current != null;
			}

			// Token: 0x06006DFB RID: 28155 RVA: 0x0017D234 File Offset: 0x0017B434
			public void Reset()
			{
				if (this.version != this.list.version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				this.start = true;
				this.current = null;
			}

			// Token: 0x04003563 RID: 13667
			private ListDictionaryInternal list;

			// Token: 0x04003564 RID: 13668
			private ListDictionaryInternal.DictionaryNode current;

			// Token: 0x04003565 RID: 13669
			private int version;

			// Token: 0x04003566 RID: 13670
			private bool start;
		}

		// Token: 0x02000BAE RID: 2990
		private class NodeKeyValueCollection : ICollection, IEnumerable
		{
			// Token: 0x06006DFC RID: 28156 RVA: 0x0017D267 File Offset: 0x0017B467
			public NodeKeyValueCollection(ListDictionaryInternal list, bool isKeys)
			{
				this.list = list;
				this.isKeys = isKeys;
			}

			// Token: 0x06006DFD RID: 28157 RVA: 0x0017D280 File Offset: 0x0017B480
			void ICollection.CopyTo(Array array, int index)
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
				if (array.Length - index < this.list.Count)
				{
					throw new ArgumentException(Environment.GetResourceString("ArgumentOutOfRange_Index"), "index");
				}
				for (ListDictionaryInternal.DictionaryNode dictionaryNode = this.list.head; dictionaryNode != null; dictionaryNode = dictionaryNode.next)
				{
					array.SetValue(this.isKeys ? dictionaryNode.key : dictionaryNode.value, index);
					index++;
				}
			}

			// Token: 0x170012A9 RID: 4777
			// (get) Token: 0x06006DFE RID: 28158 RVA: 0x0017D334 File Offset: 0x0017B534
			int ICollection.Count
			{
				get
				{
					int num = 0;
					for (ListDictionaryInternal.DictionaryNode dictionaryNode = this.list.head; dictionaryNode != null; dictionaryNode = dictionaryNode.next)
					{
						num++;
					}
					return num;
				}
			}

			// Token: 0x170012AA RID: 4778
			// (get) Token: 0x06006DFF RID: 28159 RVA: 0x0017D360 File Offset: 0x0017B560
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170012AB RID: 4779
			// (get) Token: 0x06006E00 RID: 28160 RVA: 0x0017D363 File Offset: 0x0017B563
			object ICollection.SyncRoot
			{
				get
				{
					return this.list.SyncRoot;
				}
			}

			// Token: 0x06006E01 RID: 28161 RVA: 0x0017D370 File Offset: 0x0017B570
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new ListDictionaryInternal.NodeKeyValueCollection.NodeKeyValueEnumerator(this.list, this.isKeys);
			}

			// Token: 0x04003567 RID: 13671
			private ListDictionaryInternal list;

			// Token: 0x04003568 RID: 13672
			private bool isKeys;

			// Token: 0x02000CFF RID: 3327
			private class NodeKeyValueEnumerator : IEnumerator
			{
				// Token: 0x0600720A RID: 29194 RVA: 0x0018A28B File Offset: 0x0018848B
				public NodeKeyValueEnumerator(ListDictionaryInternal list, bool isKeys)
				{
					this.list = list;
					this.isKeys = isKeys;
					this.version = list.version;
					this.start = true;
					this.current = null;
				}

				// Token: 0x17001385 RID: 4997
				// (get) Token: 0x0600720B RID: 29195 RVA: 0x0018A2BB File Offset: 0x001884BB
				public object Current
				{
					get
					{
						if (this.current == null)
						{
							throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
						}
						if (!this.isKeys)
						{
							return this.current.value;
						}
						return this.current.key;
					}
				}

				// Token: 0x0600720C RID: 29196 RVA: 0x0018A2F4 File Offset: 0x001884F4
				public bool MoveNext()
				{
					if (this.version != this.list.version)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
					}
					if (this.start)
					{
						this.current = this.list.head;
						this.start = false;
					}
					else if (this.current != null)
					{
						this.current = this.current.next;
					}
					return this.current != null;
				}

				// Token: 0x0600720D RID: 29197 RVA: 0x0018A368 File Offset: 0x00188568
				public void Reset()
				{
					if (this.version != this.list.version)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
					}
					this.start = true;
					this.current = null;
				}

				// Token: 0x04003938 RID: 14648
				private ListDictionaryInternal list;

				// Token: 0x04003939 RID: 14649
				private ListDictionaryInternal.DictionaryNode current;

				// Token: 0x0400393A RID: 14650
				private int version;

				// Token: 0x0400393B RID: 14651
				private bool isKeys;

				// Token: 0x0400393C RID: 14652
				private bool start;
			}
		}

		// Token: 0x02000BAF RID: 2991
		[Serializable]
		private class DictionaryNode
		{
			// Token: 0x04003569 RID: 13673
			public object key;

			// Token: 0x0400356A RID: 13674
			public object value;

			// Token: 0x0400356B RID: 13675
			public ListDictionaryInternal.DictionaryNode next;
		}
	}
}
