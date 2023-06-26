using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Collections.Generic
{
	/// <summary>Represents a collection of key/value pairs that are sorted by key based on the associated <see cref="T:System.Collections.Generic.IComparer`1" /> implementation.</summary>
	/// <typeparam name="TKey">The type of keys in the collection.</typeparam>
	/// <typeparam name="TValue">The type of values in the collection.</typeparam>
	// Token: 0x020003C5 RID: 965
	[DebuggerTypeProxy(typeof(System_DictionaryDebugView<, >))]
	[DebuggerDisplay("Count = {Count}")]
	[ComVisible(false)]
	[global::__DynamicallyInvokable]
	[Serializable]
	public class SortedList<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedList`2" /> class that is empty, has the default initial capacity, and uses the default <see cref="T:System.Collections.Generic.IComparer`1" />.</summary>
		// Token: 0x06002453 RID: 9299 RVA: 0x000AA0A0 File Offset: 0x000A82A0
		[global::__DynamicallyInvokable]
		public SortedList()
		{
			this.keys = SortedList<TKey, TValue>.emptyKeys;
			this.values = SortedList<TKey, TValue>.emptyValues;
			this._size = 0;
			this.comparer = Comparer<TKey>.Default;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedList`2" /> class that is empty, has the specified initial capacity, and uses the default <see cref="T:System.Collections.Generic.IComparer`1" />.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Generic.SortedList`2" /> can contain.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x06002454 RID: 9300 RVA: 0x000AA0D0 File Offset: 0x000A82D0
		[global::__DynamicallyInvokable]
		public SortedList(int capacity)
		{
			if (capacity < 0)
			{
				System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument.capacity, System.ExceptionResource.ArgumentOutOfRange_NeedNonNegNumRequired);
			}
			this.keys = new TKey[capacity];
			this.values = new TValue[capacity];
			this.comparer = Comparer<TKey>.Default;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedList`2" /> class that is empty, has the default initial capacity, and uses the specified <see cref="T:System.Collections.Generic.IComparer`1" />.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to use when comparing keys.  
		///  -or-  
		///  <see langword="null" /> to use the default <see cref="T:System.Collections.Generic.Comparer`1" /> for the type of the key.</param>
		// Token: 0x06002455 RID: 9301 RVA: 0x000AA107 File Offset: 0x000A8307
		[global::__DynamicallyInvokable]
		public SortedList(IComparer<TKey> comparer)
			: this()
		{
			if (comparer != null)
			{
				this.comparer = comparer;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedList`2" /> class that is empty, has the specified initial capacity, and uses the specified <see cref="T:System.Collections.Generic.IComparer`1" />.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Generic.SortedList`2" /> can contain.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to use when comparing keys.  
		///  -or-  
		///  <see langword="null" /> to use the default <see cref="T:System.Collections.Generic.Comparer`1" /> for the type of the key.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x06002456 RID: 9302 RVA: 0x000AA119 File Offset: 0x000A8319
		[global::__DynamicallyInvokable]
		public SortedList(int capacity, IComparer<TKey> comparer)
			: this(comparer)
		{
			this.Capacity = capacity;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedList`2" /> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IDictionary`2" />, has sufficient capacity to accommodate the number of elements copied, and uses the default <see cref="T:System.Collections.Generic.IComparer`1" />.</summary>
		/// <param name="dictionary">The <see cref="T:System.Collections.Generic.IDictionary`2" /> whose elements are copied to the new <see cref="T:System.Collections.Generic.SortedList`2" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dictionary" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dictionary" /> contains one or more duplicate keys.</exception>
		// Token: 0x06002457 RID: 9303 RVA: 0x000AA129 File Offset: 0x000A8329
		[global::__DynamicallyInvokable]
		public SortedList(IDictionary<TKey, TValue> dictionary)
			: this(dictionary, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedList`2" /> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IDictionary`2" />, has sufficient capacity to accommodate the number of elements copied, and uses the specified <see cref="T:System.Collections.Generic.IComparer`1" />.</summary>
		/// <param name="dictionary">The <see cref="T:System.Collections.Generic.IDictionary`2" /> whose elements are copied to the new <see cref="T:System.Collections.Generic.SortedList`2" />.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to use when comparing keys.  
		///  -or-  
		///  <see langword="null" /> to use the default <see cref="T:System.Collections.Generic.Comparer`1" /> for the type of the key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dictionary" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dictionary" /> contains one or more duplicate keys.</exception>
		// Token: 0x06002458 RID: 9304 RVA: 0x000AA134 File Offset: 0x000A8334
		[global::__DynamicallyInvokable]
		public SortedList(IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer)
			: this((dictionary != null) ? dictionary.Count : 0, comparer)
		{
			if (dictionary == null)
			{
				System.ThrowHelper.ThrowArgumentNullException(System.ExceptionArgument.dictionary);
			}
			dictionary.Keys.CopyTo(this.keys, 0);
			dictionary.Values.CopyTo(this.values, 0);
			Array.Sort<TKey, TValue>(this.keys, this.values, comparer);
			this._size = dictionary.Count;
		}

		/// <summary>Adds an element with the specified key and value into the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="value">The value of the element to add. The value can be <see langword="null" /> for reference types.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.Generic.SortedList`2" />.</exception>
		// Token: 0x06002459 RID: 9305 RVA: 0x000AA1A0 File Offset: 0x000A83A0
		[global::__DynamicallyInvokable]
		public void Add(TKey key, TValue value)
		{
			if (key == null)
			{
				System.ThrowHelper.ThrowArgumentNullException(System.ExceptionArgument.key);
			}
			int num = Array.BinarySearch<TKey>(this.keys, 0, this._size, key, this.comparer);
			if (num >= 0)
			{
				System.ThrowHelper.ThrowArgumentException(System.ExceptionResource.Argument_AddingDuplicate);
			}
			this.Insert(~num, key, value);
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x000AA1E9 File Offset: 0x000A83E9
		[global::__DynamicallyInvokable]
		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
		{
			this.Add(keyValuePair.Key, keyValuePair.Value);
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x000AA200 File Offset: 0x000A8400
		[global::__DynamicallyInvokable]
		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
		{
			int num = this.IndexOfKey(keyValuePair.Key);
			return num >= 0 && EqualityComparer<TValue>.Default.Equals(this.values[num], keyValuePair.Value);
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x000AA244 File Offset: 0x000A8444
		[global::__DynamicallyInvokable]
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
		{
			int num = this.IndexOfKey(keyValuePair.Key);
			if (num >= 0 && EqualityComparer<TValue>.Default.Equals(this.values[num], keyValuePair.Value))
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		/// <summary>Gets or sets the number of elements that the <see cref="T:System.Collections.Generic.SortedList`2" /> can contain.</summary>
		/// <returns>The number of elements that the <see cref="T:System.Collections.Generic.SortedList`2" /> can contain.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Collections.Generic.SortedList`2.Capacity" /> is set to a value that is less than <see cref="P:System.Collections.Generic.SortedList`2.Count" />.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough memory available on the system.</exception>
		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x0600245D RID: 9309 RVA: 0x000AA28C File Offset: 0x000A848C
		// (set) Token: 0x0600245E RID: 9310 RVA: 0x000AA298 File Offset: 0x000A8498
		[global::__DynamicallyInvokable]
		public int Capacity
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.keys.Length;
			}
			[global::__DynamicallyInvokable]
			set
			{
				if (value != this.keys.Length)
				{
					if (value < this._size)
					{
						System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument.value, System.ExceptionResource.ArgumentOutOfRange_SmallCapacity);
					}
					if (value > 0)
					{
						TKey[] array = new TKey[value];
						TValue[] array2 = new TValue[value];
						if (this._size > 0)
						{
							Array.Copy(this.keys, 0, array, 0, this._size);
							Array.Copy(this.values, 0, array2, 0, this._size);
						}
						this.keys = array;
						this.values = array2;
						return;
					}
					this.keys = SortedList<TKey, TValue>.emptyKeys;
					this.values = SortedList<TKey, TValue>.emptyValues;
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.Collections.Generic.IComparer`1" /> for the sorted list.</summary>
		/// <returns>The <see cref="T:System.IComparable`1" /> for the current <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x0600245F RID: 9311 RVA: 0x000AA32A File Offset: 0x000A852A
		[global::__DynamicallyInvokable]
		public IComparer<TKey> Comparer
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.comparer;
			}
		}

		/// <summary>Adds an element with the provided key and value to the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <param name="key">The <see cref="T:System.Object" /> to use as the key of the element to add.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to use as the value of the element to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.IDictionary" />.  
		/// -or-  
		/// <paramref name="value" /> is of a type that is not assignable to the value type <paramref name="TValue" /> of the <see cref="T:System.Collections.IDictionary" />.  
		/// -or-  
		/// An element with the same key already exists in the <see cref="T:System.Collections.IDictionary" />.</exception>
		// Token: 0x06002460 RID: 9312 RVA: 0x000AA334 File Offset: 0x000A8534
		[global::__DynamicallyInvokable]
		void IDictionary.Add(object key, object value)
		{
			if (key == null)
			{
				System.ThrowHelper.ThrowArgumentNullException(System.ExceptionArgument.key);
			}
			System.ThrowHelper.IfNullAndNullsAreIllegalThenThrow<TValue>(value, System.ExceptionArgument.value);
			try
			{
				TKey tkey = (TKey)((object)key);
				try
				{
					this.Add(tkey, (TValue)((object)value));
				}
				catch (InvalidCastException)
				{
					System.ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof(TValue));
				}
			}
			catch (InvalidCastException)
			{
				System.ThrowHelper.ThrowWrongKeyTypeArgumentException(key, typeof(TKey));
			}
		}

		/// <summary>Gets the number of key/value pairs contained in the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <returns>The number of key/value pairs contained in the <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06002461 RID: 9313 RVA: 0x000AA3AC File Offset: 0x000A85AC
		[global::__DynamicallyInvokable]
		public int Count
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this._size;
			}
		}

		/// <summary>Gets a collection containing the keys in the <see cref="T:System.Collections.Generic.SortedList`2" />, in sorted order.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.IList`1" /> containing the keys in the <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06002462 RID: 9314 RVA: 0x000AA3B4 File Offset: 0x000A85B4
		[global::__DynamicallyInvokable]
		public IList<TKey> Keys
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.GetKeyListHelper();
			}
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06002463 RID: 9315 RVA: 0x000AA3BC File Offset: 0x000A85BC
		[global::__DynamicallyInvokable]
		ICollection<TKey> IDictionary<TKey, TValue>.Keys
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.GetKeyListHelper();
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06002464 RID: 9316 RVA: 0x000AA3C4 File Offset: 0x000A85C4
		[global::__DynamicallyInvokable]
		ICollection IDictionary.Keys
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.GetKeyListHelper();
			}
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06002465 RID: 9317 RVA: 0x000AA3CC File Offset: 0x000A85CC
		[global::__DynamicallyInvokable]
		IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.GetKeyListHelper();
			}
		}

		/// <summary>Gets a collection containing the values in the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.IList`1" /> containing the values in the <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06002466 RID: 9318 RVA: 0x000AA3D4 File Offset: 0x000A85D4
		[global::__DynamicallyInvokable]
		public IList<TValue> Values
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.GetValueListHelper();
			}
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06002467 RID: 9319 RVA: 0x000AA3DC File Offset: 0x000A85DC
		[global::__DynamicallyInvokable]
		ICollection<TValue> IDictionary<TKey, TValue>.Values
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.GetValueListHelper();
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06002468 RID: 9320 RVA: 0x000AA3E4 File Offset: 0x000A85E4
		[global::__DynamicallyInvokable]
		ICollection IDictionary.Values
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.GetValueListHelper();
			}
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06002469 RID: 9321 RVA: 0x000AA3EC File Offset: 0x000A85EC
		[global::__DynamicallyInvokable]
		IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.GetValueListHelper();
			}
		}

		// Token: 0x0600246A RID: 9322 RVA: 0x000AA3F4 File Offset: 0x000A85F4
		private SortedList<TKey, TValue>.KeyList GetKeyListHelper()
		{
			if (this.keyList == null)
			{
				this.keyList = new SortedList<TKey, TValue>.KeyList(this);
			}
			return this.keyList;
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x000AA410 File Offset: 0x000A8610
		private SortedList<TKey, TValue>.ValueList GetValueListHelper()
		{
			if (this.valueList == null)
			{
				this.valueList = new SortedList<TKey, TValue>.ValueList(this);
			}
			return this.valueList;
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x0600246C RID: 9324 RVA: 0x000AA42C File Offset: 0x000A862C
		[global::__DynamicallyInvokable]
		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
		{
			[global::__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IDictionary" /> is read-only; otherwise, <see langword="false" />.  In the default implementation of <see cref="T:System.Collections.Generic.SortedList`2" />, this property always returns <see langword="false" />.</returns>
		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x0600246D RID: 9325 RVA: 0x000AA42F File Offset: 0x000A862F
		[global::__DynamicallyInvokable]
		bool IDictionary.IsReadOnly
		{
			[global::__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> has a fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IDictionary" /> has a fixed size; otherwise, <see langword="false" />.  In the default implementation of <see cref="T:System.Collections.Generic.SortedList`2" />, this property always returns <see langword="false" />.</returns>
		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x0600246E RID: 9326 RVA: 0x000AA432 File Offset: 0x000A8632
		[global::__DynamicallyInvokable]
		bool IDictionary.IsFixedSize
		{
			[global::__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, <see langword="false" />.  In the default implementation of <see cref="T:System.Collections.Generic.SortedList`2" />, this property always returns <see langword="false" />.</returns>
		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x0600246F RID: 9327 RVA: 0x000AA435 File Offset: 0x000A8635
		[global::__DynamicallyInvokable]
		bool ICollection.IsSynchronized
		{
			[global::__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  In the default implementation of <see cref="T:System.Collections.Generic.SortedList`2" />, this property always returns the current instance.</returns>
		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06002470 RID: 9328 RVA: 0x000AA438 File Offset: 0x000A8638
		[global::__DynamicallyInvokable]
		object ICollection.SyncRoot
		{
			[global::__DynamicallyInvokable]
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		/// <summary>Removes all elements from the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		// Token: 0x06002471 RID: 9329 RVA: 0x000AA45A File Offset: 0x000A865A
		[global::__DynamicallyInvokable]
		public void Clear()
		{
			this.version++;
			Array.Clear(this.keys, 0, this._size);
			Array.Clear(this.values, 0, this._size);
			this._size = 0;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IDictionary" /> contains an element with the specified key.</summary>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.IDictionary" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IDictionary" /> contains an element with the key; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x06002472 RID: 9330 RVA: 0x000AA495 File Offset: 0x000A8695
		[global::__DynamicallyInvokable]
		bool IDictionary.Contains(object key)
		{
			return SortedList<TKey, TValue>.IsCompatibleKey(key) && this.ContainsKey((TKey)((object)key));
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Generic.SortedList`2" /> contains a specific key.</summary>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.SortedList`2" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Generic.SortedList`2" /> contains an element with the specified key; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x06002473 RID: 9331 RVA: 0x000AA4AD File Offset: 0x000A86AD
		[global::__DynamicallyInvokable]
		public bool ContainsKey(TKey key)
		{
			return this.IndexOfKey(key) >= 0;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Generic.SortedList`2" /> contains a specific value.</summary>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.Generic.SortedList`2" />. The value can be <see langword="null" /> for reference types.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Generic.SortedList`2" /> contains an element with the specified value; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002474 RID: 9332 RVA: 0x000AA4BC File Offset: 0x000A86BC
		[global::__DynamicallyInvokable]
		public bool ContainsValue(TValue value)
		{
			return this.IndexOfValue(value) >= 0;
		}

		// Token: 0x06002475 RID: 9333 RVA: 0x000AA4CC File Offset: 0x000A86CC
		[global::__DynamicallyInvokable]
		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			if (array == null)
			{
				System.ThrowHelper.ThrowArgumentNullException(System.ExceptionArgument.array);
			}
			if (arrayIndex < 0 || arrayIndex > array.Length)
			{
				System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument.arrayIndex, System.ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (array.Length - arrayIndex < this.Count)
			{
				System.ThrowHelper.ThrowArgumentException(System.ExceptionResource.Arg_ArrayPlusOffTooSmall);
			}
			for (int i = 0; i < this.Count; i++)
			{
				KeyValuePair<TKey, TValue> keyValuePair = new KeyValuePair<TKey, TValue>(this.keys[i], this.values[i]);
				array[arrayIndex + i] = keyValuePair;
			}
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="arrayIndex" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// <paramref name="array" /> does not have zero-based indexing.  
		/// -or-  
		/// The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.  
		/// -or-  
		/// The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06002476 RID: 9334 RVA: 0x000AA544 File Offset: 0x000A8744
		[global::__DynamicallyInvokable]
		void ICollection.CopyTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				System.ThrowHelper.ThrowArgumentNullException(System.ExceptionArgument.array);
			}
			if (array.Rank != 1)
			{
				System.ThrowHelper.ThrowArgumentException(System.ExceptionResource.Arg_RankMultiDimNotSupported);
			}
			if (array.GetLowerBound(0) != 0)
			{
				System.ThrowHelper.ThrowArgumentException(System.ExceptionResource.Arg_NonZeroLowerBound);
			}
			if (arrayIndex < 0 || arrayIndex > array.Length)
			{
				System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument.arrayIndex, System.ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (array.Length - arrayIndex < this.Count)
			{
				System.ThrowHelper.ThrowArgumentException(System.ExceptionResource.Arg_ArrayPlusOffTooSmall);
			}
			KeyValuePair<TKey, TValue>[] array2 = array as KeyValuePair<TKey, TValue>[];
			if (array2 != null)
			{
				for (int i = 0; i < this.Count; i++)
				{
					array2[i + arrayIndex] = new KeyValuePair<TKey, TValue>(this.keys[i], this.values[i]);
				}
				return;
			}
			object[] array3 = array as object[];
			if (array3 == null)
			{
				System.ThrowHelper.ThrowArgumentException(System.ExceptionResource.Argument_InvalidArrayType);
			}
			try
			{
				for (int j = 0; j < this.Count; j++)
				{
					array3[j + arrayIndex] = new KeyValuePair<TKey, TValue>(this.keys[j], this.values[j]);
				}
			}
			catch (ArrayTypeMismatchException)
			{
				System.ThrowHelper.ThrowArgumentException(System.ExceptionResource.Argument_InvalidArrayType);
			}
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x000AA64C File Offset: 0x000A884C
		private void EnsureCapacity(int min)
		{
			int num = ((this.keys.Length == 0) ? 4 : (this.keys.Length * 2));
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

		// Token: 0x06002478 RID: 9336 RVA: 0x000AA68B File Offset: 0x000A888B
		private TValue GetByIndex(int index)
		{
			if (index < 0 || index >= this._size)
			{
				System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument.index, System.ExceptionResource.ArgumentOutOfRange_Index);
			}
			return this.values[index];
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerator`1" /> of type <see cref="T:System.Collections.Generic.KeyValuePair`2" /> for the <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		// Token: 0x06002479 RID: 9337 RVA: 0x000AA6AF File Offset: 0x000A88AF
		[global::__DynamicallyInvokable]
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return new SortedList<TKey, TValue>.Enumerator(this, 1);
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x000AA6BD File Offset: 0x000A88BD
		[global::__DynamicallyInvokable]
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
		{
			return new SortedList<TKey, TValue>.Enumerator(this, 1);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x0600247B RID: 9339 RVA: 0x000AA6CB File Offset: 0x000A88CB
		[global::__DynamicallyInvokable]
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new SortedList<TKey, TValue>.Enumerator(this, 2);
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x0600247C RID: 9340 RVA: 0x000AA6D9 File Offset: 0x000A88D9
		[global::__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SortedList<TKey, TValue>.Enumerator(this, 1);
		}

		// Token: 0x0600247D RID: 9341 RVA: 0x000AA6E7 File Offset: 0x000A88E7
		private TKey GetKey(int index)
		{
			if (index < 0 || index >= this._size)
			{
				System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument.index, System.ExceptionResource.ArgumentOutOfRange_Index);
			}
			return this.keys[index];
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <param name="key">The key whose value to get or set.</param>
		/// <returns>The value associated with the specified key. If the specified key is not found, a get operation throws a <see cref="T:System.Collections.Generic.KeyNotFoundException" /> and a set operation creates a new element using the specified key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The property is retrieved and <paramref name="key" /> does not exist in the collection.</exception>
		// Token: 0x17000937 RID: 2359
		[global::__DynamicallyInvokable]
		public TValue this[TKey key]
		{
			[global::__DynamicallyInvokable]
			get
			{
				int num = this.IndexOfKey(key);
				if (num >= 0)
				{
					return this.values[num];
				}
				System.ThrowHelper.ThrowKeyNotFoundException();
				return default(TValue);
			}
			[global::__DynamicallyInvokable]
			set
			{
				if (key == null)
				{
					System.ThrowHelper.ThrowArgumentNullException(System.ExceptionArgument.key);
				}
				int num = Array.BinarySearch<TKey>(this.keys, 0, this._size, key, this.comparer);
				if (num >= 0)
				{
					this.values[num] = value;
					this.version++;
					return;
				}
				this.Insert(~num, key, value);
			}
		}

		/// <summary>Gets or sets the element with the specified key.</summary>
		/// <param name="key">The key of the element to get or set.</param>
		/// <returns>The element with the specified key, or <see langword="null" /> if <paramref name="key" /> is not in the dictionary or <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">A value is being assigned, and <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.Generic.SortedList`2" />.  
		///  -or-  
		///  A value is being assigned, and <paramref name="value" /> is of a type that is not assignable to the value type <paramref name="TValue" /> of the <see cref="T:System.Collections.Generic.SortedList`2" />.</exception>
		// Token: 0x17000938 RID: 2360
		[global::__DynamicallyInvokable]
		object IDictionary.this[object key]
		{
			[global::__DynamicallyInvokable]
			get
			{
				if (SortedList<TKey, TValue>.IsCompatibleKey(key))
				{
					int num = this.IndexOfKey((TKey)((object)key));
					if (num >= 0)
					{
						return this.values[num];
					}
				}
				return null;
			}
			[global::__DynamicallyInvokable]
			set
			{
				if (!SortedList<TKey, TValue>.IsCompatibleKey(key))
				{
					System.ThrowHelper.ThrowArgumentNullException(System.ExceptionArgument.key);
				}
				System.ThrowHelper.IfNullAndNullsAreIllegalThenThrow<TValue>(value, System.ExceptionArgument.value);
				try
				{
					TKey tkey = (TKey)((object)key);
					try
					{
						this[tkey] = (TValue)((object)value);
					}
					catch (InvalidCastException)
					{
						System.ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof(TValue));
					}
				}
				catch (InvalidCastException)
				{
					System.ThrowHelper.ThrowWrongKeyTypeArgumentException(key, typeof(TKey));
				}
			}
		}

		/// <summary>Searches for the specified key and returns the zero-based index within the entire <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.SortedList`2" />.</param>
		/// <returns>The zero-based index of <paramref name="key" /> within the entire <see cref="T:System.Collections.Generic.SortedList`2" />, if found; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x06002482 RID: 9346 RVA: 0x000AA85C File Offset: 0x000A8A5C
		[global::__DynamicallyInvokable]
		public int IndexOfKey(TKey key)
		{
			if (key == null)
			{
				System.ThrowHelper.ThrowArgumentNullException(System.ExceptionArgument.key);
			}
			int num = Array.BinarySearch<TKey>(this.keys, 0, this._size, key, this.comparer);
			if (num < 0)
			{
				return -1;
			}
			return num;
		}

		/// <summary>Searches for the specified value and returns the zero-based index of the first occurrence within the entire <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.Generic.SortedList`2" />.  The value can be <see langword="null" /> for reference types.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" /> within the entire <see cref="T:System.Collections.Generic.SortedList`2" />, if found; otherwise, -1.</returns>
		// Token: 0x06002483 RID: 9347 RVA: 0x000AA898 File Offset: 0x000A8A98
		[global::__DynamicallyInvokable]
		public int IndexOfValue(TValue value)
		{
			return Array.IndexOf<TValue>(this.values, value, 0, this._size);
		}

		// Token: 0x06002484 RID: 9348 RVA: 0x000AA8B0 File Offset: 0x000A8AB0
		private void Insert(int index, TKey key, TValue value)
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

		/// <summary>Gets the value associated with the specified key.</summary>
		/// <param name="key">The key whose value to get.</param>
		/// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Generic.SortedList`2" /> contains an element with the specified key; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x06002485 RID: 9349 RVA: 0x000AA954 File Offset: 0x000A8B54
		[global::__DynamicallyInvokable]
		public bool TryGetValue(TKey key, out TValue value)
		{
			int num = this.IndexOfKey(key);
			if (num >= 0)
			{
				value = this.values[num];
				return true;
			}
			value = default(TValue);
			return false;
		}

		/// <summary>Removes the element at the specified index of the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.Generic.SortedList`2.Count" />.</exception>
		// Token: 0x06002486 RID: 9350 RVA: 0x000AA98C File Offset: 0x000A8B8C
		[global::__DynamicallyInvokable]
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= this._size)
			{
				System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument.index, System.ExceptionResource.ArgumentOutOfRange_Index);
			}
			this._size--;
			if (index < this._size)
			{
				Array.Copy(this.keys, index + 1, this.keys, index, this._size - index);
				Array.Copy(this.values, index + 1, this.values, index, this._size - index);
			}
			this.keys[this._size] = default(TKey);
			this.values[this._size] = default(TValue);
			this.version++;
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.Generic.SortedList`2" />.</summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <returns>
		///   <see langword="true" /> if the element is successfully removed; otherwise, <see langword="false" />.  This method also returns <see langword="false" /> if <paramref name="key" /> was not found in the original <see cref="T:System.Collections.Generic.SortedList`2" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x06002487 RID: 9351 RVA: 0x000AAA44 File Offset: 0x000A8C44
		[global::__DynamicallyInvokable]
		public bool Remove(TKey key)
		{
			int num = this.IndexOfKey(key);
			if (num >= 0)
			{
				this.RemoveAt(num);
			}
			return num >= 0;
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x06002488 RID: 9352 RVA: 0x000AAA6B File Offset: 0x000A8C6B
		[global::__DynamicallyInvokable]
		void IDictionary.Remove(object key)
		{
			if (SortedList<TKey, TValue>.IsCompatibleKey(key))
			{
				this.Remove((TKey)((object)key));
			}
		}

		/// <summary>Sets the capacity to the actual number of elements in the <see cref="T:System.Collections.Generic.SortedList`2" />, if that number is less than 90 percent of current capacity.</summary>
		// Token: 0x06002489 RID: 9353 RVA: 0x000AAA84 File Offset: 0x000A8C84
		[global::__DynamicallyInvokable]
		public void TrimExcess()
		{
			int num = (int)((double)this.keys.Length * 0.9);
			if (this._size < num)
			{
				this.Capacity = this._size;
			}
		}

		// Token: 0x0600248A RID: 9354 RVA: 0x000AAABB File Offset: 0x000A8CBB
		private static bool IsCompatibleKey(object key)
		{
			if (key == null)
			{
				System.ThrowHelper.ThrowArgumentNullException(System.ExceptionArgument.key);
			}
			return key is TKey;
		}

		// Token: 0x04002003 RID: 8195
		private TKey[] keys;

		// Token: 0x04002004 RID: 8196
		private TValue[] values;

		// Token: 0x04002005 RID: 8197
		private int _size;

		// Token: 0x04002006 RID: 8198
		private int version;

		// Token: 0x04002007 RID: 8199
		private IComparer<TKey> comparer;

		// Token: 0x04002008 RID: 8200
		private SortedList<TKey, TValue>.KeyList keyList;

		// Token: 0x04002009 RID: 8201
		private SortedList<TKey, TValue>.ValueList valueList;

		// Token: 0x0400200A RID: 8202
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x0400200B RID: 8203
		private static TKey[] emptyKeys = new TKey[0];

		// Token: 0x0400200C RID: 8204
		private static TValue[] emptyValues = new TValue[0];

		// Token: 0x0400200D RID: 8205
		private const int _defaultCapacity = 4;

		// Token: 0x0400200E RID: 8206
		private const int MaxArrayLength = 2146435071;

		// Token: 0x020007F3 RID: 2035
		[Serializable]
		private struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator, IDictionaryEnumerator
		{
			// Token: 0x06004412 RID: 17426 RVA: 0x0011DCE2 File Offset: 0x0011BEE2
			internal Enumerator(SortedList<TKey, TValue> sortedList, int getEnumeratorRetType)
			{
				this._sortedList = sortedList;
				this.index = 0;
				this.version = this._sortedList.version;
				this.getEnumeratorRetType = getEnumeratorRetType;
				this.key = default(TKey);
				this.value = default(TValue);
			}

			// Token: 0x06004413 RID: 17427 RVA: 0x0011DD22 File Offset: 0x0011BF22
			public void Dispose()
			{
				this.index = 0;
				this.key = default(TKey);
				this.value = default(TValue);
			}

			// Token: 0x17000F70 RID: 3952
			// (get) Token: 0x06004414 RID: 17428 RVA: 0x0011DD43 File Offset: 0x0011BF43
			object IDictionaryEnumerator.Key
			{
				get
				{
					if (this.index == 0 || this.index == this._sortedList.Count + 1)
					{
						System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumOpCantHappen);
					}
					return this.key;
				}
			}

			// Token: 0x06004415 RID: 17429 RVA: 0x0011DD74 File Offset: 0x0011BF74
			public bool MoveNext()
			{
				if (this.version != this._sortedList.version)
				{
					System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumFailedVersion);
				}
				if (this.index < this._sortedList.Count)
				{
					this.key = this._sortedList.keys[this.index];
					this.value = this._sortedList.values[this.index];
					this.index++;
					return true;
				}
				this.index = this._sortedList.Count + 1;
				this.key = default(TKey);
				this.value = default(TValue);
				return false;
			}

			// Token: 0x17000F71 RID: 3953
			// (get) Token: 0x06004416 RID: 17430 RVA: 0x0011DE24 File Offset: 0x0011C024
			DictionaryEntry IDictionaryEnumerator.Entry
			{
				get
				{
					if (this.index == 0 || this.index == this._sortedList.Count + 1)
					{
						System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumOpCantHappen);
					}
					return new DictionaryEntry(this.key, this.value);
				}
			}

			// Token: 0x17000F72 RID: 3954
			// (get) Token: 0x06004417 RID: 17431 RVA: 0x0011DE70 File Offset: 0x0011C070
			public KeyValuePair<TKey, TValue> Current
			{
				get
				{
					return new KeyValuePair<TKey, TValue>(this.key, this.value);
				}
			}

			// Token: 0x17000F73 RID: 3955
			// (get) Token: 0x06004418 RID: 17432 RVA: 0x0011DE84 File Offset: 0x0011C084
			object IEnumerator.Current
			{
				get
				{
					if (this.index == 0 || this.index == this._sortedList.Count + 1)
					{
						System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumOpCantHappen);
					}
					if (this.getEnumeratorRetType == 2)
					{
						return new DictionaryEntry(this.key, this.value);
					}
					return new KeyValuePair<TKey, TValue>(this.key, this.value);
				}
			}

			// Token: 0x17000F74 RID: 3956
			// (get) Token: 0x06004419 RID: 17433 RVA: 0x0011DEF5 File Offset: 0x0011C0F5
			object IDictionaryEnumerator.Value
			{
				get
				{
					if (this.index == 0 || this.index == this._sortedList.Count + 1)
					{
						System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumOpCantHappen);
					}
					return this.value;
				}
			}

			// Token: 0x0600441A RID: 17434 RVA: 0x0011DF26 File Offset: 0x0011C126
			void IEnumerator.Reset()
			{
				if (this.version != this._sortedList.version)
				{
					System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumFailedVersion);
				}
				this.index = 0;
				this.key = default(TKey);
				this.value = default(TValue);
			}

			// Token: 0x04003501 RID: 13569
			private SortedList<TKey, TValue> _sortedList;

			// Token: 0x04003502 RID: 13570
			private TKey key;

			// Token: 0x04003503 RID: 13571
			private TValue value;

			// Token: 0x04003504 RID: 13572
			private int index;

			// Token: 0x04003505 RID: 13573
			private int version;

			// Token: 0x04003506 RID: 13574
			private int getEnumeratorRetType;

			// Token: 0x04003507 RID: 13575
			internal const int KeyValuePair = 1;

			// Token: 0x04003508 RID: 13576
			internal const int DictEntry = 2;
		}

		// Token: 0x020007F4 RID: 2036
		[Serializable]
		private sealed class SortedListKeyEnumerator : IEnumerator<TKey>, IDisposable, IEnumerator
		{
			// Token: 0x0600441B RID: 17435 RVA: 0x0011DF61 File Offset: 0x0011C161
			internal SortedListKeyEnumerator(SortedList<TKey, TValue> sortedList)
			{
				this._sortedList = sortedList;
				this.version = sortedList.version;
			}

			// Token: 0x0600441C RID: 17436 RVA: 0x0011DF7C File Offset: 0x0011C17C
			public void Dispose()
			{
				this.index = 0;
				this.currentKey = default(TKey);
			}

			// Token: 0x0600441D RID: 17437 RVA: 0x0011DF94 File Offset: 0x0011C194
			public bool MoveNext()
			{
				if (this.version != this._sortedList.version)
				{
					System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumFailedVersion);
				}
				if (this.index < this._sortedList.Count)
				{
					this.currentKey = this._sortedList.keys[this.index];
					this.index++;
					return true;
				}
				this.index = this._sortedList.Count + 1;
				this.currentKey = default(TKey);
				return false;
			}

			// Token: 0x17000F75 RID: 3957
			// (get) Token: 0x0600441E RID: 17438 RVA: 0x0011E01A File Offset: 0x0011C21A
			public TKey Current
			{
				get
				{
					return this.currentKey;
				}
			}

			// Token: 0x17000F76 RID: 3958
			// (get) Token: 0x0600441F RID: 17439 RVA: 0x0011E022 File Offset: 0x0011C222
			object IEnumerator.Current
			{
				get
				{
					if (this.index == 0 || this.index == this._sortedList.Count + 1)
					{
						System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumOpCantHappen);
					}
					return this.currentKey;
				}
			}

			// Token: 0x06004420 RID: 17440 RVA: 0x0011E053 File Offset: 0x0011C253
			void IEnumerator.Reset()
			{
				if (this.version != this._sortedList.version)
				{
					System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumFailedVersion);
				}
				this.index = 0;
				this.currentKey = default(TKey);
			}

			// Token: 0x04003509 RID: 13577
			private SortedList<TKey, TValue> _sortedList;

			// Token: 0x0400350A RID: 13578
			private int index;

			// Token: 0x0400350B RID: 13579
			private int version;

			// Token: 0x0400350C RID: 13580
			private TKey currentKey;
		}

		// Token: 0x020007F5 RID: 2037
		[Serializable]
		private sealed class SortedListValueEnumerator : IEnumerator<TValue>, IDisposable, IEnumerator
		{
			// Token: 0x06004421 RID: 17441 RVA: 0x0011E082 File Offset: 0x0011C282
			internal SortedListValueEnumerator(SortedList<TKey, TValue> sortedList)
			{
				this._sortedList = sortedList;
				this.version = sortedList.version;
			}

			// Token: 0x06004422 RID: 17442 RVA: 0x0011E09D File Offset: 0x0011C29D
			public void Dispose()
			{
				this.index = 0;
				this.currentValue = default(TValue);
			}

			// Token: 0x06004423 RID: 17443 RVA: 0x0011E0B4 File Offset: 0x0011C2B4
			public bool MoveNext()
			{
				if (this.version != this._sortedList.version)
				{
					System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumFailedVersion);
				}
				if (this.index < this._sortedList.Count)
				{
					this.currentValue = this._sortedList.values[this.index];
					this.index++;
					return true;
				}
				this.index = this._sortedList.Count + 1;
				this.currentValue = default(TValue);
				return false;
			}

			// Token: 0x17000F77 RID: 3959
			// (get) Token: 0x06004424 RID: 17444 RVA: 0x0011E13A File Offset: 0x0011C33A
			public TValue Current
			{
				get
				{
					return this.currentValue;
				}
			}

			// Token: 0x17000F78 RID: 3960
			// (get) Token: 0x06004425 RID: 17445 RVA: 0x0011E142 File Offset: 0x0011C342
			object IEnumerator.Current
			{
				get
				{
					if (this.index == 0 || this.index == this._sortedList.Count + 1)
					{
						System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumOpCantHappen);
					}
					return this.currentValue;
				}
			}

			// Token: 0x06004426 RID: 17446 RVA: 0x0011E173 File Offset: 0x0011C373
			void IEnumerator.Reset()
			{
				if (this.version != this._sortedList.version)
				{
					System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumFailedVersion);
				}
				this.index = 0;
				this.currentValue = default(TValue);
			}

			// Token: 0x0400350D RID: 13581
			private SortedList<TKey, TValue> _sortedList;

			// Token: 0x0400350E RID: 13582
			private int index;

			// Token: 0x0400350F RID: 13583
			private int version;

			// Token: 0x04003510 RID: 13584
			private TValue currentValue;
		}

		// Token: 0x020007F6 RID: 2038
		[DebuggerTypeProxy(typeof(System_DictionaryKeyCollectionDebugView<, >))]
		[DebuggerDisplay("Count = {Count}")]
		[Serializable]
		private sealed class KeyList : IList<TKey>, ICollection<TKey>, IEnumerable<TKey>, IEnumerable, ICollection
		{
			// Token: 0x06004427 RID: 17447 RVA: 0x0011E1A2 File Offset: 0x0011C3A2
			internal KeyList(SortedList<TKey, TValue> dictionary)
			{
				this._dict = dictionary;
			}

			// Token: 0x17000F79 RID: 3961
			// (get) Token: 0x06004428 RID: 17448 RVA: 0x0011E1B1 File Offset: 0x0011C3B1
			public int Count
			{
				get
				{
					return this._dict._size;
				}
			}

			// Token: 0x17000F7A RID: 3962
			// (get) Token: 0x06004429 RID: 17449 RVA: 0x0011E1BE File Offset: 0x0011C3BE
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000F7B RID: 3963
			// (get) Token: 0x0600442A RID: 17450 RVA: 0x0011E1C1 File Offset: 0x0011C3C1
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000F7C RID: 3964
			// (get) Token: 0x0600442B RID: 17451 RVA: 0x0011E1C4 File Offset: 0x0011C3C4
			object ICollection.SyncRoot
			{
				get
				{
					return ((ICollection)this._dict).SyncRoot;
				}
			}

			// Token: 0x0600442C RID: 17452 RVA: 0x0011E1D1 File Offset: 0x0011C3D1
			public void Add(TKey key)
			{
				System.ThrowHelper.ThrowNotSupportedException(System.ExceptionResource.NotSupported_SortedListNestedWrite);
			}

			// Token: 0x0600442D RID: 17453 RVA: 0x0011E1DA File Offset: 0x0011C3DA
			public void Clear()
			{
				System.ThrowHelper.ThrowNotSupportedException(System.ExceptionResource.NotSupported_SortedListNestedWrite);
			}

			// Token: 0x0600442E RID: 17454 RVA: 0x0011E1E3 File Offset: 0x0011C3E3
			public bool Contains(TKey key)
			{
				return this._dict.ContainsKey(key);
			}

			// Token: 0x0600442F RID: 17455 RVA: 0x0011E1F1 File Offset: 0x0011C3F1
			public void CopyTo(TKey[] array, int arrayIndex)
			{
				Array.Copy(this._dict.keys, 0, array, arrayIndex, this._dict.Count);
			}

			// Token: 0x06004430 RID: 17456 RVA: 0x0011E214 File Offset: 0x0011C414
			void ICollection.CopyTo(Array array, int arrayIndex)
			{
				if (array != null && array.Rank != 1)
				{
					System.ThrowHelper.ThrowArgumentException(System.ExceptionResource.Arg_RankMultiDimNotSupported);
				}
				try
				{
					Array.Copy(this._dict.keys, 0, array, arrayIndex, this._dict.Count);
				}
				catch (ArrayTypeMismatchException)
				{
					System.ThrowHelper.ThrowArgumentException(System.ExceptionResource.Argument_InvalidArrayType);
				}
			}

			// Token: 0x06004431 RID: 17457 RVA: 0x0011E270 File Offset: 0x0011C470
			public void Insert(int index, TKey value)
			{
				System.ThrowHelper.ThrowNotSupportedException(System.ExceptionResource.NotSupported_SortedListNestedWrite);
			}

			// Token: 0x17000F7D RID: 3965
			public TKey this[int index]
			{
				get
				{
					return this._dict.GetKey(index);
				}
				set
				{
					System.ThrowHelper.ThrowNotSupportedException(System.ExceptionResource.NotSupported_KeyCollectionSet);
				}
			}

			// Token: 0x06004434 RID: 17460 RVA: 0x0011E290 File Offset: 0x0011C490
			public IEnumerator<TKey> GetEnumerator()
			{
				return new SortedList<TKey, TValue>.SortedListKeyEnumerator(this._dict);
			}

			// Token: 0x06004435 RID: 17461 RVA: 0x0011E29D File Offset: 0x0011C49D
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new SortedList<TKey, TValue>.SortedListKeyEnumerator(this._dict);
			}

			// Token: 0x06004436 RID: 17462 RVA: 0x0011E2AC File Offset: 0x0011C4AC
			public int IndexOf(TKey key)
			{
				if (key == null)
				{
					System.ThrowHelper.ThrowArgumentNullException(System.ExceptionArgument.key);
				}
				int num = Array.BinarySearch<TKey>(this._dict.keys, 0, this._dict.Count, key, this._dict.comparer);
				if (num >= 0)
				{
					return num;
				}
				return -1;
			}

			// Token: 0x06004437 RID: 17463 RVA: 0x0011E2F7 File Offset: 0x0011C4F7
			public bool Remove(TKey key)
			{
				System.ThrowHelper.ThrowNotSupportedException(System.ExceptionResource.NotSupported_SortedListNestedWrite);
				return false;
			}

			// Token: 0x06004438 RID: 17464 RVA: 0x0011E301 File Offset: 0x0011C501
			public void RemoveAt(int index)
			{
				System.ThrowHelper.ThrowNotSupportedException(System.ExceptionResource.NotSupported_SortedListNestedWrite);
			}

			// Token: 0x04003511 RID: 13585
			private SortedList<TKey, TValue> _dict;
		}

		// Token: 0x020007F7 RID: 2039
		[DebuggerTypeProxy(typeof(System_DictionaryValueCollectionDebugView<, >))]
		[DebuggerDisplay("Count = {Count}")]
		[Serializable]
		private sealed class ValueList : IList<TValue>, ICollection<TValue>, IEnumerable<TValue>, IEnumerable, ICollection
		{
			// Token: 0x06004439 RID: 17465 RVA: 0x0011E30A File Offset: 0x0011C50A
			internal ValueList(SortedList<TKey, TValue> dictionary)
			{
				this._dict = dictionary;
			}

			// Token: 0x17000F7E RID: 3966
			// (get) Token: 0x0600443A RID: 17466 RVA: 0x0011E319 File Offset: 0x0011C519
			public int Count
			{
				get
				{
					return this._dict._size;
				}
			}

			// Token: 0x17000F7F RID: 3967
			// (get) Token: 0x0600443B RID: 17467 RVA: 0x0011E326 File Offset: 0x0011C526
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000F80 RID: 3968
			// (get) Token: 0x0600443C RID: 17468 RVA: 0x0011E329 File Offset: 0x0011C529
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000F81 RID: 3969
			// (get) Token: 0x0600443D RID: 17469 RVA: 0x0011E32C File Offset: 0x0011C52C
			object ICollection.SyncRoot
			{
				get
				{
					return ((ICollection)this._dict).SyncRoot;
				}
			}

			// Token: 0x0600443E RID: 17470 RVA: 0x0011E339 File Offset: 0x0011C539
			public void Add(TValue key)
			{
				System.ThrowHelper.ThrowNotSupportedException(System.ExceptionResource.NotSupported_SortedListNestedWrite);
			}

			// Token: 0x0600443F RID: 17471 RVA: 0x0011E342 File Offset: 0x0011C542
			public void Clear()
			{
				System.ThrowHelper.ThrowNotSupportedException(System.ExceptionResource.NotSupported_SortedListNestedWrite);
			}

			// Token: 0x06004440 RID: 17472 RVA: 0x0011E34B File Offset: 0x0011C54B
			public bool Contains(TValue value)
			{
				return this._dict.ContainsValue(value);
			}

			// Token: 0x06004441 RID: 17473 RVA: 0x0011E359 File Offset: 0x0011C559
			public void CopyTo(TValue[] array, int arrayIndex)
			{
				Array.Copy(this._dict.values, 0, array, arrayIndex, this._dict.Count);
			}

			// Token: 0x06004442 RID: 17474 RVA: 0x0011E37C File Offset: 0x0011C57C
			void ICollection.CopyTo(Array array, int arrayIndex)
			{
				if (array != null && array.Rank != 1)
				{
					System.ThrowHelper.ThrowArgumentException(System.ExceptionResource.Arg_RankMultiDimNotSupported);
				}
				try
				{
					Array.Copy(this._dict.values, 0, array, arrayIndex, this._dict.Count);
				}
				catch (ArrayTypeMismatchException)
				{
					System.ThrowHelper.ThrowArgumentException(System.ExceptionResource.Argument_InvalidArrayType);
				}
			}

			// Token: 0x06004443 RID: 17475 RVA: 0x0011E3D8 File Offset: 0x0011C5D8
			public void Insert(int index, TValue value)
			{
				System.ThrowHelper.ThrowNotSupportedException(System.ExceptionResource.NotSupported_SortedListNestedWrite);
			}

			// Token: 0x17000F82 RID: 3970
			public TValue this[int index]
			{
				get
				{
					return this._dict.GetByIndex(index);
				}
				set
				{
					System.ThrowHelper.ThrowNotSupportedException(System.ExceptionResource.NotSupported_SortedListNestedWrite);
				}
			}

			// Token: 0x06004446 RID: 17478 RVA: 0x0011E3F8 File Offset: 0x0011C5F8
			public IEnumerator<TValue> GetEnumerator()
			{
				return new SortedList<TKey, TValue>.SortedListValueEnumerator(this._dict);
			}

			// Token: 0x06004447 RID: 17479 RVA: 0x0011E405 File Offset: 0x0011C605
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new SortedList<TKey, TValue>.SortedListValueEnumerator(this._dict);
			}

			// Token: 0x06004448 RID: 17480 RVA: 0x0011E412 File Offset: 0x0011C612
			public int IndexOf(TValue value)
			{
				return Array.IndexOf<TValue>(this._dict.values, value, 0, this._dict.Count);
			}

			// Token: 0x06004449 RID: 17481 RVA: 0x0011E431 File Offset: 0x0011C631
			public bool Remove(TValue value)
			{
				System.ThrowHelper.ThrowNotSupportedException(System.ExceptionResource.NotSupported_SortedListNestedWrite);
				return false;
			}

			// Token: 0x0600444A RID: 17482 RVA: 0x0011E43B File Offset: 0x0011C63B
			public void RemoveAt(int index)
			{
				System.ThrowHelper.ThrowNotSupportedException(System.ExceptionResource.NotSupported_SortedListNestedWrite);
			}

			// Token: 0x04003512 RID: 13586
			private SortedList<TKey, TValue> _dict;
		}
	}
}
