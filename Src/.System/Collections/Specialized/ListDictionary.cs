using System;
using System.Threading;

namespace System.Collections.Specialized
{
	/// <summary>Implements <see langword="IDictionary" /> using a singly linked list. Recommended for collections that typically include fewer than 10 items.</summary>
	// Token: 0x020003AD RID: 941
	[Serializable]
	public class ListDictionary : IDictionary, ICollection, IEnumerable
	{
		/// <summary>Creates an empty <see cref="T:System.Collections.Specialized.ListDictionary" /> using the default comparer.</summary>
		// Token: 0x0600230B RID: 8971 RVA: 0x000A651B File Offset: 0x000A471B
		public ListDictionary()
		{
		}

		/// <summary>Creates an empty <see cref="T:System.Collections.Specialized.ListDictionary" /> using the specified comparer.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> to use to determine whether two keys are equal.  
		///  -or-  
		///  <see langword="null" /> to use the default comparer, which is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />.</param>
		// Token: 0x0600230C RID: 8972 RVA: 0x000A6523 File Offset: 0x000A4723
		public ListDictionary(IComparer comparer)
		{
			this.comparer = comparer;
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <param name="key">The key whose value to get or set.</param>
		/// <returns>The value associated with the specified key. If the specified key is not found, attempting to get it returns <see langword="null" />, and attempting to set it creates a new entry using the specified key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x170008DF RID: 2271
		public object this[object key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", SR.GetString("ArgumentNull_Key"));
				}
				ListDictionary.DictionaryNode dictionaryNode = this.head;
				if (this.comparer == null)
				{
					while (dictionaryNode != null)
					{
						object key2 = dictionaryNode.key;
						if (key2 != null && key2.Equals(key))
						{
							return dictionaryNode.value;
						}
						dictionaryNode = dictionaryNode.next;
					}
				}
				else
				{
					while (dictionaryNode != null)
					{
						object key3 = dictionaryNode.key;
						if (key3 != null && this.comparer.Compare(key3, key) == 0)
						{
							return dictionaryNode.value;
						}
						dictionaryNode = dictionaryNode.next;
					}
				}
				return null;
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", SR.GetString("ArgumentNull_Key"));
				}
				this.version++;
				ListDictionary.DictionaryNode dictionaryNode = null;
				ListDictionary.DictionaryNode next;
				for (next = this.head; next != null; next = next.next)
				{
					object key2 = next.key;
					if ((this.comparer == null) ? key2.Equals(key) : (this.comparer.Compare(key2, key) == 0))
					{
						break;
					}
					dictionaryNode = next;
				}
				if (next != null)
				{
					next.value = value;
					return;
				}
				ListDictionary.DictionaryNode dictionaryNode2 = new ListDictionary.DictionaryNode();
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

		/// <summary>Gets the number of key/value pairs contained in the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <returns>The number of key/value pairs contained in the <see cref="T:System.Collections.Specialized.ListDictionary" />.</returns>
		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x0600230F RID: 8975 RVA: 0x000A6670 File Offset: 0x000A4870
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the keys in the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys in the <see cref="T:System.Collections.Specialized.ListDictionary" />.</returns>
		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06002310 RID: 8976 RVA: 0x000A6678 File Offset: 0x000A4878
		public ICollection Keys
		{
			get
			{
				return new ListDictionary.NodeKeyValueCollection(this, true);
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.ListDictionary" /> is read-only.</summary>
		/// <returns>This property always returns <see langword="false" />.</returns>
		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06002311 RID: 8977 RVA: 0x000A6681 File Offset: 0x000A4881
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.ListDictionary" /> has a fixed size.</summary>
		/// <returns>This property always returns <see langword="false" />.</returns>
		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06002312 RID: 8978 RVA: 0x000A6684 File Offset: 0x000A4884
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.ListDictionary" /> is synchronized (thread safe).</summary>
		/// <returns>This property always returns <see langword="false" />.</returns>
		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06002313 RID: 8979 RVA: 0x000A6687 File Offset: 0x000A4887
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.ListDictionary" />.</returns>
		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06002314 RID: 8980 RVA: 0x000A668A File Offset: 0x000A488A
		public object SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.Specialized.ListDictionary" />.</returns>
		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06002315 RID: 8981 RVA: 0x000A66AC File Offset: 0x000A48AC
		public ICollection Values
		{
			get
			{
				return new ListDictionary.NodeKeyValueCollection(this, false);
			}
		}

		/// <summary>Adds an entry with the specified key and value into the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <param name="key">The key of the entry to add.</param>
		/// <param name="value">The value of the entry to add. The value can be <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">An entry with the same key already exists in the <see cref="T:System.Collections.Specialized.ListDictionary" />.</exception>
		// Token: 0x06002316 RID: 8982 RVA: 0x000A66B8 File Offset: 0x000A48B8
		public void Add(object key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", SR.GetString("ArgumentNull_Key"));
			}
			this.version++;
			ListDictionary.DictionaryNode dictionaryNode = null;
			for (ListDictionary.DictionaryNode next = this.head; next != null; next = next.next)
			{
				object key2 = next.key;
				if ((this.comparer == null) ? key2.Equals(key) : (this.comparer.Compare(key2, key) == 0))
				{
					throw new ArgumentException(SR.GetString("Argument_AddingDuplicate"));
				}
				dictionaryNode = next;
			}
			ListDictionary.DictionaryNode dictionaryNode2 = new ListDictionary.DictionaryNode();
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

		/// <summary>Removes all entries from the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		// Token: 0x06002317 RID: 8983 RVA: 0x000A6771 File Offset: 0x000A4971
		public void Clear()
		{
			this.count = 0;
			this.head = null;
			this.version++;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Specialized.ListDictionary" /> contains a specific key.</summary>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Specialized.ListDictionary" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Specialized.ListDictionary" /> contains an entry with the specified key; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x06002318 RID: 8984 RVA: 0x000A6790 File Offset: 0x000A4990
		public bool Contains(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", SR.GetString("ArgumentNull_Key"));
			}
			for (ListDictionary.DictionaryNode next = this.head; next != null; next = next.next)
			{
				object key2 = next.key;
				if ((this.comparer == null) ? key2.Equals(key) : (this.comparer.Compare(key2, key) == 0))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Copies the <see cref="T:System.Collections.Specialized.ListDictionary" /> entries to a one-dimensional <see cref="T:System.Array" /> instance at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the <see cref="T:System.Collections.DictionaryEntry" /> objects copied from <see cref="T:System.Collections.Specialized.ListDictionary" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the source <see cref="T:System.Collections.Specialized.ListDictionary" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.Specialized.ListDictionary" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06002319 RID: 8985 RVA: 0x000A67F8 File Offset: 0x000A49F8
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - index < this.count)
			{
				throw new ArgumentException(SR.GetString("Arg_InsufficientSpace"));
			}
			for (ListDictionary.DictionaryNode next = this.head; next != null; next = next.next)
			{
				array.SetValue(new DictionaryEntry(next.key, next.value), index);
				index++;
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> that iterates through the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.Specialized.ListDictionary" />.</returns>
		// Token: 0x0600231A RID: 8986 RVA: 0x000A6881 File Offset: 0x000A4A81
		public IDictionaryEnumerator GetEnumerator()
		{
			return new ListDictionary.NodeEnumerator(this);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that iterates through the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.Specialized.ListDictionary" />.</returns>
		// Token: 0x0600231B RID: 8987 RVA: 0x000A6889 File Offset: 0x000A4A89
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ListDictionary.NodeEnumerator(this);
		}

		/// <summary>Removes the entry with the specified key from the <see cref="T:System.Collections.Specialized.ListDictionary" />.</summary>
		/// <param name="key">The key of the entry to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x0600231C RID: 8988 RVA: 0x000A6894 File Offset: 0x000A4A94
		public void Remove(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", SR.GetString("ArgumentNull_Key"));
			}
			this.version++;
			ListDictionary.DictionaryNode dictionaryNode = null;
			ListDictionary.DictionaryNode next;
			for (next = this.head; next != null; next = next.next)
			{
				object key2 = next.key;
				if ((this.comparer == null) ? key2.Equals(key) : (this.comparer.Compare(key2, key) == 0))
				{
					break;
				}
				dictionaryNode = next;
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

		// Token: 0x04001FA7 RID: 8103
		private ListDictionary.DictionaryNode head;

		// Token: 0x04001FA8 RID: 8104
		private int version;

		// Token: 0x04001FA9 RID: 8105
		private int count;

		// Token: 0x04001FAA RID: 8106
		private IComparer comparer;

		// Token: 0x04001FAB RID: 8107
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x020007E7 RID: 2023
		private class NodeEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x060043C5 RID: 17349 RVA: 0x0011CF5B File Offset: 0x0011B15B
			public NodeEnumerator(ListDictionary list)
			{
				this.list = list;
				this.version = list.version;
				this.start = true;
				this.current = null;
			}

			// Token: 0x17000F53 RID: 3923
			// (get) Token: 0x060043C6 RID: 17350 RVA: 0x0011CF84 File Offset: 0x0011B184
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x17000F54 RID: 3924
			// (get) Token: 0x060043C7 RID: 17351 RVA: 0x0011CF91 File Offset: 0x0011B191
			public DictionaryEntry Entry
			{
				get
				{
					if (this.current == null)
					{
						throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumOpCantHappen"));
					}
					return new DictionaryEntry(this.current.key, this.current.value);
				}
			}

			// Token: 0x17000F55 RID: 3925
			// (get) Token: 0x060043C8 RID: 17352 RVA: 0x0011CFC6 File Offset: 0x0011B1C6
			public object Key
			{
				get
				{
					if (this.current == null)
					{
						throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumOpCantHappen"));
					}
					return this.current.key;
				}
			}

			// Token: 0x17000F56 RID: 3926
			// (get) Token: 0x060043C9 RID: 17353 RVA: 0x0011CFEB File Offset: 0x0011B1EB
			public object Value
			{
				get
				{
					if (this.current == null)
					{
						throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumOpCantHappen"));
					}
					return this.current.value;
				}
			}

			// Token: 0x060043CA RID: 17354 RVA: 0x0011D010 File Offset: 0x0011B210
			public bool MoveNext()
			{
				if (this.version != this.list.version)
				{
					throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumFailedVersion"));
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

			// Token: 0x060043CB RID: 17355 RVA: 0x0011D084 File Offset: 0x0011B284
			public void Reset()
			{
				if (this.version != this.list.version)
				{
					throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumFailedVersion"));
				}
				this.start = true;
				this.current = null;
			}

			// Token: 0x040034D9 RID: 13529
			private ListDictionary list;

			// Token: 0x040034DA RID: 13530
			private ListDictionary.DictionaryNode current;

			// Token: 0x040034DB RID: 13531
			private int version;

			// Token: 0x040034DC RID: 13532
			private bool start;
		}

		// Token: 0x020007E8 RID: 2024
		private class NodeKeyValueCollection : ICollection, IEnumerable
		{
			// Token: 0x060043CC RID: 17356 RVA: 0x0011D0B7 File Offset: 0x0011B2B7
			public NodeKeyValueCollection(ListDictionary list, bool isKeys)
			{
				this.list = list;
				this.isKeys = isKeys;
			}

			// Token: 0x060043CD RID: 17357 RVA: 0x0011D0D0 File Offset: 0x0011B2D0
			void ICollection.CopyTo(Array array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				for (ListDictionary.DictionaryNode dictionaryNode = this.list.head; dictionaryNode != null; dictionaryNode = dictionaryNode.next)
				{
					array.SetValue(this.isKeys ? dictionaryNode.key : dictionaryNode.value, index);
					index++;
				}
			}

			// Token: 0x17000F57 RID: 3927
			// (get) Token: 0x060043CE RID: 17358 RVA: 0x0011D140 File Offset: 0x0011B340
			int ICollection.Count
			{
				get
				{
					int num = 0;
					for (ListDictionary.DictionaryNode dictionaryNode = this.list.head; dictionaryNode != null; dictionaryNode = dictionaryNode.next)
					{
						num++;
					}
					return num;
				}
			}

			// Token: 0x17000F58 RID: 3928
			// (get) Token: 0x060043CF RID: 17359 RVA: 0x0011D16C File Offset: 0x0011B36C
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000F59 RID: 3929
			// (get) Token: 0x060043D0 RID: 17360 RVA: 0x0011D16F File Offset: 0x0011B36F
			object ICollection.SyncRoot
			{
				get
				{
					return this.list.SyncRoot;
				}
			}

			// Token: 0x060043D1 RID: 17361 RVA: 0x0011D17C File Offset: 0x0011B37C
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new ListDictionary.NodeKeyValueCollection.NodeKeyValueEnumerator(this.list, this.isKeys);
			}

			// Token: 0x040034DD RID: 13533
			private ListDictionary list;

			// Token: 0x040034DE RID: 13534
			private bool isKeys;

			// Token: 0x02000922 RID: 2338
			private class NodeKeyValueEnumerator : IEnumerator
			{
				// Token: 0x06004650 RID: 18000 RVA: 0x001258F9 File Offset: 0x00123AF9
				public NodeKeyValueEnumerator(ListDictionary list, bool isKeys)
				{
					this.list = list;
					this.isKeys = isKeys;
					this.version = list.version;
					this.start = true;
					this.current = null;
				}

				// Token: 0x17000FDD RID: 4061
				// (get) Token: 0x06004651 RID: 18001 RVA: 0x00125929 File Offset: 0x00123B29
				public object Current
				{
					get
					{
						if (this.current == null)
						{
							throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumOpCantHappen"));
						}
						if (!this.isKeys)
						{
							return this.current.value;
						}
						return this.current.key;
					}
				}

				// Token: 0x06004652 RID: 18002 RVA: 0x00125964 File Offset: 0x00123B64
				public bool MoveNext()
				{
					if (this.version != this.list.version)
					{
						throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumFailedVersion"));
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

				// Token: 0x06004653 RID: 18003 RVA: 0x001259D8 File Offset: 0x00123BD8
				public void Reset()
				{
					if (this.version != this.list.version)
					{
						throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumFailedVersion"));
					}
					this.start = true;
					this.current = null;
				}

				// Token: 0x04003D99 RID: 15769
				private ListDictionary list;

				// Token: 0x04003D9A RID: 15770
				private ListDictionary.DictionaryNode current;

				// Token: 0x04003D9B RID: 15771
				private int version;

				// Token: 0x04003D9C RID: 15772
				private bool isKeys;

				// Token: 0x04003D9D RID: 15773
				private bool start;
			}
		}

		// Token: 0x020007E9 RID: 2025
		[Serializable]
		private class DictionaryNode
		{
			// Token: 0x040034DF RID: 13535
			public object key;

			// Token: 0x040034E0 RID: 13536
			public object value;

			// Token: 0x040034E1 RID: 13537
			public ListDictionary.DictionaryNode next;
		}
	}
}
