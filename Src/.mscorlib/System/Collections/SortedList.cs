using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections
{
	/// <summary>Represents a collection of key/value pairs that are sorted by the keys and are accessible by key and by index.</summary>
	// Token: 0x020004A2 RID: 1186
	[DebuggerTypeProxy(typeof(SortedList.SortedListDebugView))]
	[DebuggerDisplay("Count = {Count}")]
	[ComVisible(true)]
	[Serializable]
	public class SortedList : IDictionary, ICollection, IEnumerable, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.SortedList" /> class that is empty, has the default initial capacity, and is sorted according to the <see cref="T:System.IComparable" /> interface implemented by each key added to the <see cref="T:System.Collections.SortedList" /> object.</summary>
		// Token: 0x060038E7 RID: 14567 RVA: 0x000DB453 File Offset: 0x000D9653
		public SortedList()
		{
			this.Init();
		}

		// Token: 0x060038E8 RID: 14568 RVA: 0x000DB461 File Offset: 0x000D9661
		private void Init()
		{
			this.keys = SortedList.emptyArray;
			this.values = SortedList.emptyArray;
			this._size = 0;
			this.comparer = new Comparer(CultureInfo.CurrentCulture);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.SortedList" /> class that is empty, has the specified initial capacity, and is sorted according to the <see cref="T:System.IComparable" /> interface implemented by each key added to the <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <param name="initialCapacity">The initial number of elements that the <see cref="T:System.Collections.SortedList" /> object can contain.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="initialCapacity" /> is less than zero.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough available memory to create a <see cref="T:System.Collections.SortedList" /> object with the specified <paramref name="initialCapacity" />.</exception>
		// Token: 0x060038E9 RID: 14569 RVA: 0x000DB490 File Offset: 0x000D9690
		public SortedList(int initialCapacity)
		{
			if (initialCapacity < 0)
			{
				throw new ArgumentOutOfRangeException("initialCapacity", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			this.keys = new object[initialCapacity];
			this.values = new object[initialCapacity];
			this.comparer = new Comparer(CultureInfo.CurrentCulture);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.SortedList" /> class that is empty, has the default initial capacity, and is sorted according to the specified <see cref="T:System.Collections.IComparer" /> interface.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing keys.  
		///  -or-  
		///  <see langword="null" /> to use the <see cref="T:System.IComparable" /> implementation of each key.</param>
		// Token: 0x060038EA RID: 14570 RVA: 0x000DB4E4 File Offset: 0x000D96E4
		public SortedList(IComparer comparer)
			: this()
		{
			if (comparer != null)
			{
				this.comparer = comparer;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.SortedList" /> class that is empty, has the specified initial capacity, and is sorted according to the specified <see cref="T:System.Collections.IComparer" /> interface.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing keys.  
		///  -or-  
		///  <see langword="null" /> to use the <see cref="T:System.IComparable" /> implementation of each key.</param>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.SortedList" /> object can contain.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough available memory to create a <see cref="T:System.Collections.SortedList" /> object with the specified <paramref name="capacity" />.</exception>
		// Token: 0x060038EB RID: 14571 RVA: 0x000DB4F6 File Offset: 0x000D96F6
		public SortedList(IComparer comparer, int capacity)
			: this(comparer)
		{
			this.Capacity = capacity;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.SortedList" /> class that contains elements copied from the specified dictionary, has the same initial capacity as the number of elements copied, and is sorted according to the <see cref="T:System.IComparable" /> interface implemented by each key.</summary>
		/// <param name="d">The <see cref="T:System.Collections.IDictionary" /> implementation to copy to a new <see cref="T:System.Collections.SortedList" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidCastException">One or more elements in <paramref name="d" /> do not implement the <see cref="T:System.IComparable" /> interface.</exception>
		// Token: 0x060038EC RID: 14572 RVA: 0x000DB506 File Offset: 0x000D9706
		public SortedList(IDictionary d)
			: this(d, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.SortedList" /> class that contains elements copied from the specified dictionary, has the same initial capacity as the number of elements copied, and is sorted according to the specified <see cref="T:System.Collections.IComparer" /> interface.</summary>
		/// <param name="d">The <see cref="T:System.Collections.IDictionary" /> implementation to copy to a new <see cref="T:System.Collections.SortedList" /> object.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> implementation to use when comparing keys.  
		///  -or-  
		///  <see langword="null" /> to use the <see cref="T:System.IComparable" /> implementation of each key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="comparer" /> is <see langword="null" />, and one or more elements in <paramref name="d" /> do not implement the <see cref="T:System.IComparable" /> interface.</exception>
		// Token: 0x060038ED RID: 14573 RVA: 0x000DB510 File Offset: 0x000D9710
		public SortedList(IDictionary d, IComparer comparer)
			: this(comparer, (d != null) ? d.Count : 0)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d", Environment.GetResourceString("ArgumentNull_Dictionary"));
			}
			d.Keys.CopyTo(this.keys, 0);
			d.Values.CopyTo(this.values, 0);
			Array.Sort(this.keys, this.values, comparer);
			this._size = d.Count;
		}

		/// <summary>Adds an element with the specified key and value to a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="value">The value of the element to add. The value can be <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">An element with the specified <paramref name="key" /> already exists in the <see cref="T:System.Collections.SortedList" /> object.  
		///  -or-  
		///  The <see cref="T:System.Collections.SortedList" /> is set to use the <see cref="T:System.IComparable" /> interface, and <paramref name="key" /> does not implement the <see cref="T:System.IComparable" /> interface.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.SortedList" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.SortedList" /> has a fixed size.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough available memory to add the element to the <see cref="T:System.Collections.SortedList" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The comparer throws an exception.</exception>
		// Token: 0x060038EE RID: 14574 RVA: 0x000DB58C File Offset: 0x000D978C
		public virtual void Add(object key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
			}
			int num = Array.BinarySearch(this.keys, 0, this._size, key, this.comparer);
			if (num >= 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_AddingDuplicate__", new object[]
				{
					this.GetKey(num),
					key
				}));
			}
			this.Insert(~num, key, value);
		}

		/// <summary>Gets or sets the capacity of a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>The number of elements that the <see cref="T:System.Collections.SortedList" /> object can contain.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value assigned is less than the current number of elements in the <see cref="T:System.Collections.SortedList" /> object.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough memory available on the system.</exception>
		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x060038EF RID: 14575 RVA: 0x000DB5FD File Offset: 0x000D97FD
		// (set) Token: 0x060038F0 RID: 14576 RVA: 0x000DB608 File Offset: 0x000D9808
		public virtual int Capacity
		{
			get
			{
				return this.keys.Length;
			}
			set
			{
				if (value < this.Count)
				{
					throw new ArgumentOutOfRangeException("value", Environment.GetResourceString("ArgumentOutOfRange_SmallCapacity"));
				}
				if (value != this.keys.Length)
				{
					if (value > 0)
					{
						object[] array = new object[value];
						object[] array2 = new object[value];
						if (this._size > 0)
						{
							Array.Copy(this.keys, 0, array, 0, this._size);
							Array.Copy(this.values, 0, array2, 0, this._size);
						}
						this.keys = array;
						this.values = array2;
						return;
					}
					this.keys = SortedList.emptyArray;
					this.values = SortedList.emptyArray;
				}
			}
		}

		/// <summary>Gets the number of elements contained in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.SortedList" /> object.</returns>
		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x060038F1 RID: 14577 RVA: 0x000DB6A6 File Offset: 0x000D98A6
		public virtual int Count
		{
			get
			{
				return this._size;
			}
		}

		/// <summary>Gets the keys in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing the keys in the <see cref="T:System.Collections.SortedList" /> object.</returns>
		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x060038F2 RID: 14578 RVA: 0x000DB6AE File Offset: 0x000D98AE
		public virtual ICollection Keys
		{
			get
			{
				return this.GetKeyList();
			}
		}

		/// <summary>Gets the values in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing the values in the <see cref="T:System.Collections.SortedList" /> object.</returns>
		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x060038F3 RID: 14579 RVA: 0x000DB6B6 File Offset: 0x000D98B6
		public virtual ICollection Values
		{
			get
			{
				return this.GetValueList();
			}
		}

		/// <summary>Gets a value indicating whether a <see cref="T:System.Collections.SortedList" /> object is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.SortedList" /> object is read-only; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x060038F4 RID: 14580 RVA: 0x000DB6BE File Offset: 0x000D98BE
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether a <see cref="T:System.Collections.SortedList" /> object has a fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.SortedList" /> object has a fixed size; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x060038F5 RID: 14581 RVA: 0x000DB6C1 File Offset: 0x000D98C1
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to a <see cref="T:System.Collections.SortedList" /> object is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Collections.SortedList" /> object is synchronized (thread safe); otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x060038F6 RID: 14582 RVA: 0x000DB6C4 File Offset: 0x000D98C4
		public virtual bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.SortedList" /> object.</returns>
		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x060038F7 RID: 14583 RVA: 0x000DB6C7 File Offset: 0x000D98C7
		public virtual object SyncRoot
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

		/// <summary>Removes all elements from a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.SortedList" /> object is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.SortedList" /> has a fixed size.</exception>
		// Token: 0x060038F8 RID: 14584 RVA: 0x000DB6E9 File Offset: 0x000D98E9
		public virtual void Clear()
		{
			this.version++;
			Array.Clear(this.keys, 0, this._size);
			Array.Clear(this.values, 0, this._size);
			this._size = 0;
		}

		/// <summary>Creates a shallow copy of a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>A shallow copy of the <see cref="T:System.Collections.SortedList" /> object.</returns>
		// Token: 0x060038F9 RID: 14585 RVA: 0x000DB724 File Offset: 0x000D9924
		public virtual object Clone()
		{
			SortedList sortedList = new SortedList(this._size);
			Array.Copy(this.keys, 0, sortedList.keys, 0, this._size);
			Array.Copy(this.values, 0, sortedList.values, 0, this._size);
			sortedList._size = this._size;
			sortedList.version = this.version;
			sortedList.comparer = this.comparer;
			return sortedList;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.SortedList" /> object contains a specific key.</summary>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.SortedList" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.SortedList" /> object contains an element with the specified <paramref name="key" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The comparer throws an exception.</exception>
		// Token: 0x060038FA RID: 14586 RVA: 0x000DB794 File Offset: 0x000D9994
		public virtual bool Contains(object key)
		{
			return this.IndexOfKey(key) >= 0;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.SortedList" /> object contains a specific key.</summary>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.SortedList" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.SortedList" /> object contains an element with the specified <paramref name="key" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The comparer throws an exception.</exception>
		// Token: 0x060038FB RID: 14587 RVA: 0x000DB7A3 File Offset: 0x000D99A3
		public virtual bool ContainsKey(object key)
		{
			return this.IndexOfKey(key) >= 0;
		}

		/// <summary>Determines whether a <see cref="T:System.Collections.SortedList" /> object contains a specific value.</summary>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.SortedList" /> object. The value can be <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.SortedList" /> object contains an element with the specified <paramref name="value" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060038FC RID: 14588 RVA: 0x000DB7B2 File Offset: 0x000D99B2
		public virtual bool ContainsValue(object value)
		{
			return this.IndexOfValue(value) >= 0;
		}

		/// <summary>Copies <see cref="T:System.Collections.SortedList" /> elements to a one-dimensional <see cref="T:System.Array" /> object, starting at the specified index in the array.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> object that is the destination of the <see cref="T:System.Collections.DictionaryEntry" /> objects copied from <see cref="T:System.Collections.SortedList" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the source <see cref="T:System.Collections.SortedList" /> object is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.SortedList" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x060038FD RID: 14589 RVA: 0x000DB7C4 File Offset: 0x000D99C4
		public virtual void CopyTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (array.Rank != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - arrayIndex < this.Count)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_ArrayPlusOffTooSmall"));
			}
			for (int i = 0; i < this.Count; i++)
			{
				DictionaryEntry dictionaryEntry = new DictionaryEntry(this.keys[i], this.values[i]);
				array.SetValue(dictionaryEntry, i + arrayIndex);
			}
		}

		// Token: 0x060038FE RID: 14590 RVA: 0x000DB874 File Offset: 0x000D9A74
		internal virtual KeyValuePairs[] ToKeyValuePairsArray()
		{
			KeyValuePairs[] array = new KeyValuePairs[this.Count];
			for (int i = 0; i < this.Count; i++)
			{
				array[i] = new KeyValuePairs(this.keys[i], this.values[i]);
			}
			return array;
		}

		// Token: 0x060038FF RID: 14591 RVA: 0x000DB8B8 File Offset: 0x000D9AB8
		private void EnsureCapacity(int min)
		{
			int num = ((this.keys.Length == 0) ? 16 : (this.keys.Length * 2));
			if (num > 2146435071)
			{
				num = 2146435071;
			}
			if (num < min)
			{
				num = min;
			}
			this.Capacity = num;
		}

		/// <summary>Gets the value at the specified index of a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <param name="index">The zero-based index of the value to get.</param>
		/// <returns>The value at the specified index of the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes for the <see cref="T:System.Collections.SortedList" /> object.</exception>
		// Token: 0x06003900 RID: 14592 RVA: 0x000DB8F8 File Offset: 0x000D9AF8
		public virtual object GetByIndex(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			return this.values[index];
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that iterates through the <see cref="T:System.Collections.SortedList" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.SortedList" />.</returns>
		// Token: 0x06003901 RID: 14593 RVA: 0x000DB924 File Offset: 0x000D9B24
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SortedList.SortedListEnumerator(this, 0, this._size, 3);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> object that iterates through a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> object for the <see cref="T:System.Collections.SortedList" /> object.</returns>
		// Token: 0x06003902 RID: 14594 RVA: 0x000DB934 File Offset: 0x000D9B34
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return new SortedList.SortedListEnumerator(this, 0, this._size, 3);
		}

		/// <summary>Gets the key at the specified index of a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <param name="index">The zero-based index of the key to get.</param>
		/// <returns>The key at the specified index of the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes for the <see cref="T:System.Collections.SortedList" /> object.</exception>
		// Token: 0x06003903 RID: 14595 RVA: 0x000DB944 File Offset: 0x000D9B44
		public virtual object GetKey(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			return this.keys[index];
		}

		/// <summary>Gets the keys in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> object containing the keys in the <see cref="T:System.Collections.SortedList" /> object.</returns>
		// Token: 0x06003904 RID: 14596 RVA: 0x000DB970 File Offset: 0x000D9B70
		public virtual IList GetKeyList()
		{
			if (this.keyList == null)
			{
				this.keyList = new SortedList.KeyList(this);
			}
			return this.keyList;
		}

		/// <summary>Gets the values in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> object containing the values in the <see cref="T:System.Collections.SortedList" /> object.</returns>
		// Token: 0x06003905 RID: 14597 RVA: 0x000DB98C File Offset: 0x000D9B8C
		public virtual IList GetValueList()
		{
			if (this.valueList == null)
			{
				this.valueList = new SortedList.ValueList(this);
			}
			return this.valueList;
		}

		/// <summary>Gets or sets the value associated with a specific key in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <param name="key">The key associated with the value to get or set.</param>
		/// <returns>The value associated with the <paramref name="key" /> parameter in the <see cref="T:System.Collections.SortedList" /> object, if <paramref name="key" /> is found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The property is set and the <see cref="T:System.Collections.SortedList" /> object is read-only.  
		///  -or-  
		///  The property is set, <paramref name="key" /> does not exist in the collection, and the <see cref="T:System.Collections.SortedList" /> has a fixed size.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough available memory to add the element to the <see cref="T:System.Collections.SortedList" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The comparer throws an exception.</exception>
		// Token: 0x17000886 RID: 2182
		public virtual object this[object key]
		{
			get
			{
				int num = this.IndexOfKey(key);
				if (num >= 0)
				{
					return this.values[num];
				}
				return null;
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
				}
				int num = Array.BinarySearch(this.keys, 0, this._size, key, this.comparer);
				if (num >= 0)
				{
					this.values[num] = value;
					this.version++;
					return;
				}
				this.Insert(~num, key, value);
			}
		}

		/// <summary>Returns the zero-based index of the specified key in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.SortedList" /> object.</param>
		/// <returns>The zero-based index of the <paramref name="key" /> parameter, if <paramref name="key" /> is found in the <see cref="T:System.Collections.SortedList" /> object; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The comparer throws an exception.</exception>
		// Token: 0x06003908 RID: 14600 RVA: 0x000DBA34 File Offset: 0x000D9C34
		public virtual int IndexOfKey(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
			}
			int num = Array.BinarySearch(this.keys, 0, this._size, key, this.comparer);
			if (num < 0)
			{
				return -1;
			}
			return num;
		}

		/// <summary>Returns the zero-based index of the first occurrence of the specified value in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.SortedList" /> object. The value can be <see langword="null" />.</param>
		/// <returns>The zero-based index of the first occurrence of the <paramref name="value" /> parameter, if <paramref name="value" /> is found in the <see cref="T:System.Collections.SortedList" /> object; otherwise, -1.</returns>
		// Token: 0x06003909 RID: 14601 RVA: 0x000DBA7A File Offset: 0x000D9C7A
		public virtual int IndexOfValue(object value)
		{
			return Array.IndexOf<object>(this.values, value, 0, this._size);
		}

		// Token: 0x0600390A RID: 14602 RVA: 0x000DBA90 File Offset: 0x000D9C90
		private void Insert(int index, object key, object value)
		{
			if (this._size == this.keys.Length)
			{
				this.EnsureCapacity(this._size + 1);
			}
			if (index < this._size)
			{
				Array.Copy(this.keys, index, this.keys, index + 1, this._size - index);
				Array.Copy(this.values, index, this.values, index + 1, this._size - index);
			}
			this.keys[index] = key;
			this.values[index] = value;
			this._size++;
			this.version++;
		}

		/// <summary>Removes the element at the specified index of a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes for the <see cref="T:System.Collections.SortedList" /> object.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.SortedList" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.SortedList" /> has a fixed size.</exception>
		// Token: 0x0600390B RID: 14603 RVA: 0x000DBB2C File Offset: 0x000D9D2C
		public virtual void RemoveAt(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			this._size--;
			if (index < this._size)
			{
				Array.Copy(this.keys, index + 1, this.keys, index, this._size - index);
				Array.Copy(this.values, index + 1, this.values, index, this._size - index);
			}
			this.keys[this._size] = null;
			this.values[this._size] = null;
			this.version++;
		}

		/// <summary>Removes the element with the specified key from a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.SortedList" /> object is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.SortedList" /> has a fixed size.</exception>
		// Token: 0x0600390C RID: 14604 RVA: 0x000DBBD8 File Offset: 0x000D9DD8
		public virtual void Remove(object key)
		{
			int num = this.IndexOfKey(key);
			if (num >= 0)
			{
				this.RemoveAt(num);
			}
		}

		/// <summary>Replaces the value at a specific index in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <param name="index">The zero-based index at which to save <paramref name="value" />.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to save into the <see cref="T:System.Collections.SortedList" /> object. The value can be <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes for the <see cref="T:System.Collections.SortedList" /> object.</exception>
		// Token: 0x0600390D RID: 14605 RVA: 0x000DBBF8 File Offset: 0x000D9DF8
		public virtual void SetByIndex(int index, object value)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("ArgumentOutOfRange_Index"));
			}
			this.values[index] = value;
			this.version++;
		}

		/// <summary>Returns a synchronized (thread-safe) wrapper for a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <param name="list">The <see cref="T:System.Collections.SortedList" /> object to synchronize.</param>
		/// <returns>A synchronized (thread-safe) wrapper for the <see cref="T:System.Collections.SortedList" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="list" /> is <see langword="null" />.</exception>
		// Token: 0x0600390E RID: 14606 RVA: 0x000DBC33 File Offset: 0x000D9E33
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
		public static SortedList Synchronized(SortedList list)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			return new SortedList.SyncSortedList(list);
		}

		/// <summary>Sets the capacity to the actual number of elements in a <see cref="T:System.Collections.SortedList" /> object.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.SortedList" /> object is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.SortedList" /> has a fixed size.</exception>
		// Token: 0x0600390F RID: 14607 RVA: 0x000DBC49 File Offset: 0x000D9E49
		public virtual void TrimToSize()
		{
			this.Capacity = this._size;
		}

		// Token: 0x0400190C RID: 6412
		private object[] keys;

		// Token: 0x0400190D RID: 6413
		private object[] values;

		// Token: 0x0400190E RID: 6414
		private int _size;

		// Token: 0x0400190F RID: 6415
		private int version;

		// Token: 0x04001910 RID: 6416
		private IComparer comparer;

		// Token: 0x04001911 RID: 6417
		private SortedList.KeyList keyList;

		// Token: 0x04001912 RID: 6418
		private SortedList.ValueList valueList;

		// Token: 0x04001913 RID: 6419
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x04001914 RID: 6420
		private const int _defaultCapacity = 16;

		// Token: 0x04001915 RID: 6421
		private static object[] emptyArray = EmptyArray<object>.Value;

		// Token: 0x02000BB7 RID: 2999
		[Serializable]
		private class SyncSortedList : SortedList
		{
			// Token: 0x06006E38 RID: 28216 RVA: 0x0017DBCC File Offset: 0x0017BDCC
			internal SyncSortedList(SortedList list)
			{
				this._list = list;
				this._root = list.SyncRoot;
			}

			// Token: 0x170012C3 RID: 4803
			// (get) Token: 0x06006E39 RID: 28217 RVA: 0x0017DBE8 File Offset: 0x0017BDE8
			public override int Count
			{
				get
				{
					object root = this._root;
					int count;
					lock (root)
					{
						count = this._list.Count;
					}
					return count;
				}
			}

			// Token: 0x170012C4 RID: 4804
			// (get) Token: 0x06006E3A RID: 28218 RVA: 0x0017DC30 File Offset: 0x0017BE30
			public override object SyncRoot
			{
				get
				{
					return this._root;
				}
			}

			// Token: 0x170012C5 RID: 4805
			// (get) Token: 0x06006E3B RID: 28219 RVA: 0x0017DC38 File Offset: 0x0017BE38
			public override bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x170012C6 RID: 4806
			// (get) Token: 0x06006E3C RID: 28220 RVA: 0x0017DC45 File Offset: 0x0017BE45
			public override bool IsFixedSize
			{
				get
				{
					return this._list.IsFixedSize;
				}
			}

			// Token: 0x170012C7 RID: 4807
			// (get) Token: 0x06006E3D RID: 28221 RVA: 0x0017DC52 File Offset: 0x0017BE52
			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170012C8 RID: 4808
			public override object this[object key]
			{
				get
				{
					object root = this._root;
					object obj;
					lock (root)
					{
						obj = this._list[key];
					}
					return obj;
				}
				set
				{
					object root = this._root;
					lock (root)
					{
						this._list[key] = value;
					}
				}
			}

			// Token: 0x06006E40 RID: 28224 RVA: 0x0017DCE8 File Offset: 0x0017BEE8
			public override void Add(object key, object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Add(key, value);
				}
			}

			// Token: 0x170012C9 RID: 4809
			// (get) Token: 0x06006E41 RID: 28225 RVA: 0x0017DD30 File Offset: 0x0017BF30
			public override int Capacity
			{
				get
				{
					object root = this._root;
					int capacity;
					lock (root)
					{
						capacity = this._list.Capacity;
					}
					return capacity;
				}
			}

			// Token: 0x06006E42 RID: 28226 RVA: 0x0017DD78 File Offset: 0x0017BF78
			public override void Clear()
			{
				object root = this._root;
				lock (root)
				{
					this._list.Clear();
				}
			}

			// Token: 0x06006E43 RID: 28227 RVA: 0x0017DDC0 File Offset: 0x0017BFC0
			public override object Clone()
			{
				object root = this._root;
				object obj;
				lock (root)
				{
					obj = this._list.Clone();
				}
				return obj;
			}

			// Token: 0x06006E44 RID: 28228 RVA: 0x0017DE08 File Offset: 0x0017C008
			public override bool Contains(object key)
			{
				object root = this._root;
				bool flag2;
				lock (root)
				{
					flag2 = this._list.Contains(key);
				}
				return flag2;
			}

			// Token: 0x06006E45 RID: 28229 RVA: 0x0017DE50 File Offset: 0x0017C050
			public override bool ContainsKey(object key)
			{
				object root = this._root;
				bool flag2;
				lock (root)
				{
					flag2 = this._list.ContainsKey(key);
				}
				return flag2;
			}

			// Token: 0x06006E46 RID: 28230 RVA: 0x0017DE98 File Offset: 0x0017C098
			public override bool ContainsValue(object key)
			{
				object root = this._root;
				bool flag2;
				lock (root)
				{
					flag2 = this._list.ContainsValue(key);
				}
				return flag2;
			}

			// Token: 0x06006E47 RID: 28231 RVA: 0x0017DEE0 File Offset: 0x0017C0E0
			public override void CopyTo(Array array, int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.CopyTo(array, index);
				}
			}

			// Token: 0x06006E48 RID: 28232 RVA: 0x0017DF28 File Offset: 0x0017C128
			public override object GetByIndex(int index)
			{
				object root = this._root;
				object byIndex;
				lock (root)
				{
					byIndex = this._list.GetByIndex(index);
				}
				return byIndex;
			}

			// Token: 0x06006E49 RID: 28233 RVA: 0x0017DF70 File Offset: 0x0017C170
			public override IDictionaryEnumerator GetEnumerator()
			{
				object root = this._root;
				IDictionaryEnumerator enumerator;
				lock (root)
				{
					enumerator = this._list.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x06006E4A RID: 28234 RVA: 0x0017DFB8 File Offset: 0x0017C1B8
			public override object GetKey(int index)
			{
				object root = this._root;
				object key;
				lock (root)
				{
					key = this._list.GetKey(index);
				}
				return key;
			}

			// Token: 0x06006E4B RID: 28235 RVA: 0x0017E000 File Offset: 0x0017C200
			public override IList GetKeyList()
			{
				object root = this._root;
				IList keyList;
				lock (root)
				{
					keyList = this._list.GetKeyList();
				}
				return keyList;
			}

			// Token: 0x06006E4C RID: 28236 RVA: 0x0017E048 File Offset: 0x0017C248
			public override IList GetValueList()
			{
				object root = this._root;
				IList valueList;
				lock (root)
				{
					valueList = this._list.GetValueList();
				}
				return valueList;
			}

			// Token: 0x06006E4D RID: 28237 RVA: 0x0017E090 File Offset: 0x0017C290
			public override int IndexOfKey(object key)
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
				}
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.IndexOfKey(key);
				}
				return num;
			}

			// Token: 0x06006E4E RID: 28238 RVA: 0x0017E0F0 File Offset: 0x0017C2F0
			public override int IndexOfValue(object value)
			{
				object root = this._root;
				int num;
				lock (root)
				{
					num = this._list.IndexOfValue(value);
				}
				return num;
			}

			// Token: 0x06006E4F RID: 28239 RVA: 0x0017E138 File Offset: 0x0017C338
			public override void RemoveAt(int index)
			{
				object root = this._root;
				lock (root)
				{
					this._list.RemoveAt(index);
				}
			}

			// Token: 0x06006E50 RID: 28240 RVA: 0x0017E180 File Offset: 0x0017C380
			public override void Remove(object key)
			{
				object root = this._root;
				lock (root)
				{
					this._list.Remove(key);
				}
			}

			// Token: 0x06006E51 RID: 28241 RVA: 0x0017E1C8 File Offset: 0x0017C3C8
			public override void SetByIndex(int index, object value)
			{
				object root = this._root;
				lock (root)
				{
					this._list.SetByIndex(index, value);
				}
			}

			// Token: 0x06006E52 RID: 28242 RVA: 0x0017E210 File Offset: 0x0017C410
			internal override KeyValuePairs[] ToKeyValuePairsArray()
			{
				return this._list.ToKeyValuePairsArray();
			}

			// Token: 0x06006E53 RID: 28243 RVA: 0x0017E220 File Offset: 0x0017C420
			public override void TrimToSize()
			{
				object root = this._root;
				lock (root)
				{
					this._list.TrimToSize();
				}
			}

			// Token: 0x0400357D RID: 13693
			private SortedList _list;

			// Token: 0x0400357E RID: 13694
			private object _root;
		}

		// Token: 0x02000BB8 RID: 3000
		[Serializable]
		private class SortedListEnumerator : IDictionaryEnumerator, IEnumerator, ICloneable
		{
			// Token: 0x06006E54 RID: 28244 RVA: 0x0017E268 File Offset: 0x0017C468
			internal SortedListEnumerator(SortedList sortedList, int index, int count, int getObjRetType)
			{
				this.sortedList = sortedList;
				this.index = index;
				this.startIndex = index;
				this.endIndex = index + count;
				this.version = sortedList.version;
				this.getObjectRetType = getObjRetType;
				this.current = false;
			}

			// Token: 0x06006E55 RID: 28245 RVA: 0x0017E2B4 File Offset: 0x0017C4B4
			public object Clone()
			{
				return base.MemberwiseClone();
			}

			// Token: 0x170012CA RID: 4810
			// (get) Token: 0x06006E56 RID: 28246 RVA: 0x0017E2BC File Offset: 0x0017C4BC
			public virtual object Key
			{
				get
				{
					if (this.version != this.sortedList.version)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
					}
					if (!this.current)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
					}
					return this.key;
				}
			}

			// Token: 0x06006E57 RID: 28247 RVA: 0x0017E30C File Offset: 0x0017C50C
			public virtual bool MoveNext()
			{
				if (this.version != this.sortedList.version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				if (this.index < this.endIndex)
				{
					this.key = this.sortedList.keys[this.index];
					this.value = this.sortedList.values[this.index];
					this.index++;
					this.current = true;
					return true;
				}
				this.key = null;
				this.value = null;
				this.current = false;
				return false;
			}

			// Token: 0x170012CB RID: 4811
			// (get) Token: 0x06006E58 RID: 28248 RVA: 0x0017E3A8 File Offset: 0x0017C5A8
			public virtual DictionaryEntry Entry
			{
				get
				{
					if (this.version != this.sortedList.version)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
					}
					if (!this.current)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
					}
					return new DictionaryEntry(this.key, this.value);
				}
			}

			// Token: 0x170012CC RID: 4812
			// (get) Token: 0x06006E59 RID: 28249 RVA: 0x0017E404 File Offset: 0x0017C604
			public virtual object Current
			{
				get
				{
					if (!this.current)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
					}
					if (this.getObjectRetType == 1)
					{
						return this.key;
					}
					if (this.getObjectRetType == 2)
					{
						return this.value;
					}
					return new DictionaryEntry(this.key, this.value);
				}
			}

			// Token: 0x170012CD RID: 4813
			// (get) Token: 0x06006E5A RID: 28250 RVA: 0x0017E460 File Offset: 0x0017C660
			public virtual object Value
			{
				get
				{
					if (this.version != this.sortedList.version)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
					}
					if (!this.current)
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumOpCantHappen"));
					}
					return this.value;
				}
			}

			// Token: 0x06006E5B RID: 28251 RVA: 0x0017E4B0 File Offset: 0x0017C6B0
			public virtual void Reset()
			{
				if (this.version != this.sortedList.version)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumFailedVersion"));
				}
				this.index = this.startIndex;
				this.current = false;
				this.key = null;
				this.value = null;
			}

			// Token: 0x0400357F RID: 13695
			private SortedList sortedList;

			// Token: 0x04003580 RID: 13696
			private object key;

			// Token: 0x04003581 RID: 13697
			private object value;

			// Token: 0x04003582 RID: 13698
			private int index;

			// Token: 0x04003583 RID: 13699
			private int startIndex;

			// Token: 0x04003584 RID: 13700
			private int endIndex;

			// Token: 0x04003585 RID: 13701
			private int version;

			// Token: 0x04003586 RID: 13702
			private bool current;

			// Token: 0x04003587 RID: 13703
			private int getObjectRetType;

			// Token: 0x04003588 RID: 13704
			internal const int Keys = 1;

			// Token: 0x04003589 RID: 13705
			internal const int Values = 2;

			// Token: 0x0400358A RID: 13706
			internal const int DictEntry = 3;
		}

		// Token: 0x02000BB9 RID: 3001
		[Serializable]
		private class KeyList : IList, ICollection, IEnumerable
		{
			// Token: 0x06006E5C RID: 28252 RVA: 0x0017E501 File Offset: 0x0017C701
			internal KeyList(SortedList sortedList)
			{
				this.sortedList = sortedList;
			}

			// Token: 0x170012CE RID: 4814
			// (get) Token: 0x06006E5D RID: 28253 RVA: 0x0017E510 File Offset: 0x0017C710
			public virtual int Count
			{
				get
				{
					return this.sortedList._size;
				}
			}

			// Token: 0x170012CF RID: 4815
			// (get) Token: 0x06006E5E RID: 28254 RVA: 0x0017E51D File Offset: 0x0017C71D
			public virtual bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170012D0 RID: 4816
			// (get) Token: 0x06006E5F RID: 28255 RVA: 0x0017E520 File Offset: 0x0017C720
			public virtual bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170012D1 RID: 4817
			// (get) Token: 0x06006E60 RID: 28256 RVA: 0x0017E523 File Offset: 0x0017C723
			public virtual bool IsSynchronized
			{
				get
				{
					return this.sortedList.IsSynchronized;
				}
			}

			// Token: 0x170012D2 RID: 4818
			// (get) Token: 0x06006E61 RID: 28257 RVA: 0x0017E530 File Offset: 0x0017C730
			public virtual object SyncRoot
			{
				get
				{
					return this.sortedList.SyncRoot;
				}
			}

			// Token: 0x06006E62 RID: 28258 RVA: 0x0017E53D File Offset: 0x0017C73D
			public virtual int Add(object key)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
			}

			// Token: 0x06006E63 RID: 28259 RVA: 0x0017E54E File Offset: 0x0017C74E
			public virtual void Clear()
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
			}

			// Token: 0x06006E64 RID: 28260 RVA: 0x0017E55F File Offset: 0x0017C75F
			public virtual bool Contains(object key)
			{
				return this.sortedList.Contains(key);
			}

			// Token: 0x06006E65 RID: 28261 RVA: 0x0017E56D File Offset: 0x0017C76D
			public virtual void CopyTo(Array array, int arrayIndex)
			{
				if (array != null && array.Rank != 1)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
				}
				Array.Copy(this.sortedList.keys, 0, array, arrayIndex, this.sortedList.Count);
			}

			// Token: 0x06006E66 RID: 28262 RVA: 0x0017E5A9 File Offset: 0x0017C7A9
			public virtual void Insert(int index, object value)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
			}

			// Token: 0x170012D3 RID: 4819
			public virtual object this[int index]
			{
				get
				{
					return this.sortedList.GetKey(index);
				}
				set
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_KeyCollectionSet"));
				}
			}

			// Token: 0x06006E69 RID: 28265 RVA: 0x0017E5D9 File Offset: 0x0017C7D9
			public virtual IEnumerator GetEnumerator()
			{
				return new SortedList.SortedListEnumerator(this.sortedList, 0, this.sortedList.Count, 1);
			}

			// Token: 0x06006E6A RID: 28266 RVA: 0x0017E5F4 File Offset: 0x0017C7F4
			public virtual int IndexOf(object key)
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", Environment.GetResourceString("ArgumentNull_Key"));
				}
				int num = Array.BinarySearch(this.sortedList.keys, 0, this.sortedList.Count, key, this.sortedList.comparer);
				if (num >= 0)
				{
					return num;
				}
				return -1;
			}

			// Token: 0x06006E6B RID: 28267 RVA: 0x0017E649 File Offset: 0x0017C849
			public virtual void Remove(object key)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
			}

			// Token: 0x06006E6C RID: 28268 RVA: 0x0017E65A File Offset: 0x0017C85A
			public virtual void RemoveAt(int index)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
			}

			// Token: 0x0400358B RID: 13707
			private SortedList sortedList;
		}

		// Token: 0x02000BBA RID: 3002
		[Serializable]
		private class ValueList : IList, ICollection, IEnumerable
		{
			// Token: 0x06006E6D RID: 28269 RVA: 0x0017E66B File Offset: 0x0017C86B
			internal ValueList(SortedList sortedList)
			{
				this.sortedList = sortedList;
			}

			// Token: 0x170012D4 RID: 4820
			// (get) Token: 0x06006E6E RID: 28270 RVA: 0x0017E67A File Offset: 0x0017C87A
			public virtual int Count
			{
				get
				{
					return this.sortedList._size;
				}
			}

			// Token: 0x170012D5 RID: 4821
			// (get) Token: 0x06006E6F RID: 28271 RVA: 0x0017E687 File Offset: 0x0017C887
			public virtual bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170012D6 RID: 4822
			// (get) Token: 0x06006E70 RID: 28272 RVA: 0x0017E68A File Offset: 0x0017C88A
			public virtual bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170012D7 RID: 4823
			// (get) Token: 0x06006E71 RID: 28273 RVA: 0x0017E68D File Offset: 0x0017C88D
			public virtual bool IsSynchronized
			{
				get
				{
					return this.sortedList.IsSynchronized;
				}
			}

			// Token: 0x170012D8 RID: 4824
			// (get) Token: 0x06006E72 RID: 28274 RVA: 0x0017E69A File Offset: 0x0017C89A
			public virtual object SyncRoot
			{
				get
				{
					return this.sortedList.SyncRoot;
				}
			}

			// Token: 0x06006E73 RID: 28275 RVA: 0x0017E6A7 File Offset: 0x0017C8A7
			public virtual int Add(object key)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
			}

			// Token: 0x06006E74 RID: 28276 RVA: 0x0017E6B8 File Offset: 0x0017C8B8
			public virtual void Clear()
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
			}

			// Token: 0x06006E75 RID: 28277 RVA: 0x0017E6C9 File Offset: 0x0017C8C9
			public virtual bool Contains(object value)
			{
				return this.sortedList.ContainsValue(value);
			}

			// Token: 0x06006E76 RID: 28278 RVA: 0x0017E6D7 File Offset: 0x0017C8D7
			public virtual void CopyTo(Array array, int arrayIndex)
			{
				if (array != null && array.Rank != 1)
				{
					throw new ArgumentException(Environment.GetResourceString("Arg_RankMultiDimNotSupported"));
				}
				Array.Copy(this.sortedList.values, 0, array, arrayIndex, this.sortedList.Count);
			}

			// Token: 0x06006E77 RID: 28279 RVA: 0x0017E713 File Offset: 0x0017C913
			public virtual void Insert(int index, object value)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
			}

			// Token: 0x170012D9 RID: 4825
			public virtual object this[int index]
			{
				get
				{
					return this.sortedList.GetByIndex(index);
				}
				set
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
				}
			}

			// Token: 0x06006E7A RID: 28282 RVA: 0x0017E743 File Offset: 0x0017C943
			public virtual IEnumerator GetEnumerator()
			{
				return new SortedList.SortedListEnumerator(this.sortedList, 0, this.sortedList.Count, 2);
			}

			// Token: 0x06006E7B RID: 28283 RVA: 0x0017E75D File Offset: 0x0017C95D
			public virtual int IndexOf(object value)
			{
				return Array.IndexOf<object>(this.sortedList.values, value, 0, this.sortedList.Count);
			}

			// Token: 0x06006E7C RID: 28284 RVA: 0x0017E77C File Offset: 0x0017C97C
			public virtual void Remove(object value)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
			}

			// Token: 0x06006E7D RID: 28285 RVA: 0x0017E78D File Offset: 0x0017C98D
			public virtual void RemoveAt(int index)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_SortedListNestedWrite"));
			}

			// Token: 0x0400358C RID: 13708
			private SortedList sortedList;
		}

		// Token: 0x02000BBB RID: 3003
		internal class SortedListDebugView
		{
			// Token: 0x06006E7E RID: 28286 RVA: 0x0017E79E File Offset: 0x0017C99E
			public SortedListDebugView(SortedList sortedList)
			{
				if (sortedList == null)
				{
					throw new ArgumentNullException("sortedList");
				}
				this.sortedList = sortedList;
			}

			// Token: 0x170012DA RID: 4826
			// (get) Token: 0x06006E7F RID: 28287 RVA: 0x0017E7BB File Offset: 0x0017C9BB
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public KeyValuePairs[] Items
			{
				get
				{
					return this.sortedList.ToKeyValuePairsArray();
				}
			}

			// Token: 0x0400358D RID: 13709
			private SortedList sortedList;
		}
	}
}
