using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Collections.Specialized
{
	/// <summary>Represents a collection of key/value pairs that are accessible by the key or index.</summary>
	// Token: 0x020003B4 RID: 948
	[Serializable]
	public class OrderedDictionary : IOrderedDictionary, IDictionary, ICollection, IEnumerable, ISerializable, IDeserializationCallback
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> class.</summary>
		// Token: 0x06002380 RID: 9088 RVA: 0x000A7F6E File Offset: 0x000A616E
		public OrderedDictionary()
			: this(0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> class using the specified initial capacity.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection can contain.</param>
		// Token: 0x06002381 RID: 9089 RVA: 0x000A7F77 File Offset: 0x000A6177
		public OrderedDictionary(int capacity)
			: this(capacity, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> class using the specified comparer.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> to use to determine whether two keys are equal.  
		///  -or-  
		///  <see langword="null" /> to use the default comparer, which is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />.</param>
		// Token: 0x06002382 RID: 9090 RVA: 0x000A7F81 File Offset: 0x000A6181
		public OrderedDictionary(IEqualityComparer comparer)
			: this(0, comparer)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> class using the specified initial capacity and comparer.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection can contain.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> to use to determine whether two keys are equal.  
		///  -or-  
		///  <see langword="null" /> to use the default comparer, which is each key's implementation of <see cref="M:System.Object.Equals(System.Object)" />.</param>
		// Token: 0x06002383 RID: 9091 RVA: 0x000A7F8B File Offset: 0x000A618B
		public OrderedDictionary(int capacity, IEqualityComparer comparer)
		{
			this._initialCapacity = capacity;
			this._comparer = comparer;
		}

		// Token: 0x06002384 RID: 9092 RVA: 0x000A7FA4 File Offset: 0x000A61A4
		private OrderedDictionary(OrderedDictionary dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this._readOnly = true;
			this._objectsArray = dictionary._objectsArray;
			this._objectsTable = dictionary._objectsTable;
			this._comparer = dictionary._comparer;
			this._initialCapacity = dictionary._initialCapacity;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> class that is serializable using the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> objects.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object containing the information required to serialize the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object containing the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Specialized.OrderedDictionary" />.</param>
		// Token: 0x06002385 RID: 9093 RVA: 0x000A7FFC File Offset: 0x000A61FC
		protected OrderedDictionary(SerializationInfo info, StreamingContext context)
		{
			this._siInfo = info;
		}

		/// <summary>Gets the number of key/values pairs contained in the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <returns>The number of key/value pairs contained in the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</returns>
		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06002386 RID: 9094 RVA: 0x000A800B File Offset: 0x000A620B
		public int Count
		{
			get
			{
				return this.objectsArray.Count;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> has a fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> has a fixed size; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06002387 RID: 9095 RVA: 0x000A8018 File Offset: 0x000A6218
		bool IDictionary.IsFixedSize
		{
			get
			{
				return this._readOnly;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06002388 RID: 9096 RVA: 0x000A8020 File Offset: 0x000A6220
		public bool IsReadOnly
		{
			get
			{
				return this._readOnly;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> object is synchronized (thread-safe).</summary>
		/// <returns>This method always returns <see langword="false" />.</returns>
		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06002389 RID: 9097 RVA: 0x000A8028 File Offset: 0x000A6228
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> object containing the keys in the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing the keys in the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</returns>
		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x0600238A RID: 9098 RVA: 0x000A802B File Offset: 0x000A622B
		public ICollection Keys
		{
			get
			{
				return new OrderedDictionary.OrderedDictionaryKeyValueCollection(this.objectsArray, true);
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x0600238B RID: 9099 RVA: 0x000A8039 File Offset: 0x000A6239
		private ArrayList objectsArray
		{
			get
			{
				if (this._objectsArray == null)
				{
					this._objectsArray = new ArrayList(this._initialCapacity);
				}
				return this._objectsArray;
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x0600238C RID: 9100 RVA: 0x000A805A File Offset: 0x000A625A
		private Hashtable objectsTable
		{
			get
			{
				if (this._objectsTable == null)
				{
					this._objectsTable = new Hashtable(this._initialCapacity, this._comparer);
				}
				return this._objectsTable;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> object.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> object.</returns>
		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x0600238D RID: 9101 RVA: 0x000A8081 File Offset: 0x000A6281
		object ICollection.SyncRoot
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

		/// <summary>Gets or sets the value at the specified index.</summary>
		/// <param name="index">The zero-based index of the value to get or set.</param>
		/// <returns>The value of the item at the specified index.</returns>
		/// <exception cref="T:System.NotSupportedException">The property is being set and the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.Specialized.OrderedDictionary.Count" />.</exception>
		// Token: 0x17000901 RID: 2305
		public object this[int index]
		{
			get
			{
				return ((DictionaryEntry)this.objectsArray[index]).Value;
			}
			set
			{
				if (this._readOnly)
				{
					throw new NotSupportedException(SR.GetString("OrderedDictionary_ReadOnly"));
				}
				if (index < 0 || index >= this.objectsArray.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				object key = ((DictionaryEntry)this.objectsArray[index]).Key;
				this.objectsArray[index] = new DictionaryEntry(key, value);
				this.objectsTable[key] = value;
			}
		}

		/// <summary>Gets or sets the value with the specified key.</summary>
		/// <param name="key">The key of the value to get or set.</param>
		/// <returns>The value associated with the specified key. If the specified key is not found, attempting to get it returns <see langword="null" />, and attempting to set it creates a new element using the specified key.</returns>
		/// <exception cref="T:System.NotSupportedException">The property is being set and the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only.</exception>
		// Token: 0x17000902 RID: 2306
		public object this[object key]
		{
			get
			{
				return this.objectsTable[key];
			}
			set
			{
				if (this._readOnly)
				{
					throw new NotSupportedException(SR.GetString("OrderedDictionary_ReadOnly"));
				}
				if (this.objectsTable.Contains(key))
				{
					this.objectsTable[key] = value;
					this.objectsArray[this.IndexOfKey(key)] = new DictionaryEntry(key, value);
					return;
				}
				this.Add(key, value);
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> object containing the values in the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing the values in the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</returns>
		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06002392 RID: 9106 RVA: 0x000A81C3 File Offset: 0x000A63C3
		public ICollection Values
		{
			get
			{
				return new OrderedDictionary.OrderedDictionaryKeyValueCollection(this.objectsArray, false);
			}
		}

		/// <summary>Adds an entry with the specified key and value into the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection with the lowest available index.</summary>
		/// <param name="key">The key of the entry to add.</param>
		/// <param name="value">The value of the entry to add. This value can be <see langword="null" />.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</exception>
		// Token: 0x06002393 RID: 9107 RVA: 0x000A81D1 File Offset: 0x000A63D1
		public void Add(object key, object value)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(SR.GetString("OrderedDictionary_ReadOnly"));
			}
			this.objectsTable.Add(key, value);
			this.objectsArray.Add(new DictionaryEntry(key, value));
		}

		/// <summary>Removes all elements from the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only.</exception>
		// Token: 0x06002394 RID: 9108 RVA: 0x000A8210 File Offset: 0x000A6410
		public void Clear()
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(SR.GetString("OrderedDictionary_ReadOnly"));
			}
			this.objectsTable.Clear();
			this.objectsArray.Clear();
		}

		/// <summary>Returns a read-only copy of the current <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <returns>A read-only copy of the current <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</returns>
		// Token: 0x06002395 RID: 9109 RVA: 0x000A8240 File Offset: 0x000A6440
		public OrderedDictionary AsReadOnly()
		{
			return new OrderedDictionary(this);
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection contains a specific key.</summary>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection contains an element with the specified key; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002396 RID: 9110 RVA: 0x000A8248 File Offset: 0x000A6448
		public bool Contains(object key)
		{
			return this.objectsTable.Contains(key);
		}

		/// <summary>Copies the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> elements to a one-dimensional <see cref="T:System.Array" /> object at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> object that is the destination of the <see cref="T:System.Collections.DictionaryEntry" /> objects copied from <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x06002397 RID: 9111 RVA: 0x000A8256 File Offset: 0x000A6456
		public void CopyTo(Array array, int index)
		{
			this.objectsTable.CopyTo(array, index);
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x000A8268 File Offset: 0x000A6468
		private int IndexOfKey(object key)
		{
			for (int i = 0; i < this.objectsArray.Count; i++)
			{
				object key2 = ((DictionaryEntry)this.objectsArray[i]).Key;
				if (this._comparer != null)
				{
					if (this._comparer.Equals(key2, key))
					{
						return i;
					}
				}
				else if (key2.Equals(key))
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Inserts a new entry into the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection with the specified key and value at the specified index.</summary>
		/// <param name="index">The zero-based index at which the element should be inserted.</param>
		/// <param name="key">The key of the entry to add.</param>
		/// <param name="value">The value of the entry to add. The value can be <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is out of range.</exception>
		/// <exception cref="T:System.NotSupportedException">This collection is read-only.</exception>
		// Token: 0x06002399 RID: 9113 RVA: 0x000A82CC File Offset: 0x000A64CC
		public void Insert(int index, object key, object value)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(SR.GetString("OrderedDictionary_ReadOnly"));
			}
			if (index > this.Count || index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this.objectsTable.Add(key, value);
			this.objectsArray.Insert(index, new DictionaryEntry(key, value));
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and is called back by the deserialization event when deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is invalid.</exception>
		// Token: 0x0600239A RID: 9114 RVA: 0x000A8330 File Offset: 0x000A6530
		protected virtual void OnDeserialization(object sender)
		{
			if (this._siInfo == null)
			{
				throw new SerializationException(SR.GetString("Serialization_InvalidOnDeser"));
			}
			this._comparer = (IEqualityComparer)this._siInfo.GetValue("KeyComparer", typeof(IEqualityComparer));
			this._readOnly = this._siInfo.GetBoolean("ReadOnly");
			this._initialCapacity = this._siInfo.GetInt32("InitialCapacity");
			object[] array = (object[])this._siInfo.GetValue("ArrayList", typeof(object[]));
			if (array != null)
			{
				foreach (object obj in array)
				{
					DictionaryEntry dictionaryEntry;
					try
					{
						dictionaryEntry = (DictionaryEntry)obj;
					}
					catch
					{
						throw new SerializationException(SR.GetString("OrderedDictionary_SerializationMismatch"));
					}
					this.objectsArray.Add(dictionaryEntry);
					this.objectsTable.Add(dictionaryEntry.Key, dictionaryEntry.Value);
				}
			}
		}

		/// <summary>Removes the entry at the specified index from the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <param name="index">The zero-based index of the entry to remove.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-
		///  <paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.Specialized.OrderedDictionary.Count" />.</exception>
		// Token: 0x0600239B RID: 9115 RVA: 0x000A8434 File Offset: 0x000A6634
		public void RemoveAt(int index)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(SR.GetString("OrderedDictionary_ReadOnly"));
			}
			if (index >= this.Count || index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			object key = ((DictionaryEntry)this.objectsArray[index]).Key;
			this.objectsArray.RemoveAt(index);
			this.objectsTable.Remove(key);
		}

		/// <summary>Removes the entry with the specified key from the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <param name="key">The key of the entry to remove.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x0600239C RID: 9116 RVA: 0x000A84A4 File Offset: 0x000A66A4
		public void Remove(object key)
		{
			if (this._readOnly)
			{
				throw new NotSupportedException(SR.GetString("OrderedDictionary_ReadOnly"));
			}
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = this.IndexOfKey(key);
			if (num < 0)
			{
				return;
			}
			this.objectsTable.Remove(key);
			this.objectsArray.RemoveAt(num);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> object that iterates through the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> object for the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</returns>
		// Token: 0x0600239D RID: 9117 RVA: 0x000A84FC File Offset: 0x000A66FC
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return new OrderedDictionary.OrderedDictionaryEnumerator(this.objectsArray, 3);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> object that iterates through the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> object for the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</returns>
		// Token: 0x0600239E RID: 9118 RVA: 0x000A850A File Offset: 0x000A670A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new OrderedDictionary.OrderedDictionaryEnumerator(this.objectsArray, 3);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data needed to serialize the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object containing the information required to serialize the <see cref="T:System.Collections.Specialized.OrderedDictionary" /> collection.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object containing the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Specialized.OrderedDictionary" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x0600239F RID: 9119 RVA: 0x000A8518 File Offset: 0x000A6718
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("KeyComparer", this._comparer, typeof(IEqualityComparer));
			info.AddValue("ReadOnly", this._readOnly);
			info.AddValue("InitialCapacity", this._initialCapacity);
			object[] array = new object[this.Count];
			this._objectsArray.CopyTo(array);
			info.AddValue("ArrayList", array);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and is called back by the deserialization event when deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		// Token: 0x060023A0 RID: 9120 RVA: 0x000A8594 File Offset: 0x000A6794
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this.OnDeserialization(sender);
		}

		// Token: 0x04001FCF RID: 8143
		private ArrayList _objectsArray;

		// Token: 0x04001FD0 RID: 8144
		private Hashtable _objectsTable;

		// Token: 0x04001FD1 RID: 8145
		private int _initialCapacity;

		// Token: 0x04001FD2 RID: 8146
		private IEqualityComparer _comparer;

		// Token: 0x04001FD3 RID: 8147
		private bool _readOnly;

		// Token: 0x04001FD4 RID: 8148
		private object _syncRoot;

		// Token: 0x04001FD5 RID: 8149
		private SerializationInfo _siInfo;

		// Token: 0x04001FD6 RID: 8150
		private const string KeyComparerName = "KeyComparer";

		// Token: 0x04001FD7 RID: 8151
		private const string ArrayListName = "ArrayList";

		// Token: 0x04001FD8 RID: 8152
		private const string ReadOnlyName = "ReadOnly";

		// Token: 0x04001FD9 RID: 8153
		private const string InitCapacityName = "InitialCapacity";

		// Token: 0x020007ED RID: 2029
		private class OrderedDictionaryEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x060043E0 RID: 17376 RVA: 0x0011D3A2 File Offset: 0x0011B5A2
			internal OrderedDictionaryEnumerator(ArrayList array, int objectReturnType)
			{
				this.arrayEnumerator = array.GetEnumerator();
				this._objectReturnType = objectReturnType;
			}

			// Token: 0x17000F5F RID: 3935
			// (get) Token: 0x060043E1 RID: 17377 RVA: 0x0011D3C0 File Offset: 0x0011B5C0
			public object Current
			{
				get
				{
					if (this._objectReturnType == 1)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)this.arrayEnumerator.Current;
						return dictionaryEntry.Key;
					}
					if (this._objectReturnType == 2)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)this.arrayEnumerator.Current;
						return dictionaryEntry.Value;
					}
					return this.Entry;
				}
			}

			// Token: 0x17000F60 RID: 3936
			// (get) Token: 0x060043E2 RID: 17378 RVA: 0x0011D41C File Offset: 0x0011B61C
			public DictionaryEntry Entry
			{
				get
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)this.arrayEnumerator.Current;
					object key = dictionaryEntry.Key;
					dictionaryEntry = (DictionaryEntry)this.arrayEnumerator.Current;
					return new DictionaryEntry(key, dictionaryEntry.Value);
				}
			}

			// Token: 0x17000F61 RID: 3937
			// (get) Token: 0x060043E3 RID: 17379 RVA: 0x0011D460 File Offset: 0x0011B660
			public object Key
			{
				get
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)this.arrayEnumerator.Current;
					return dictionaryEntry.Key;
				}
			}

			// Token: 0x17000F62 RID: 3938
			// (get) Token: 0x060043E4 RID: 17380 RVA: 0x0011D488 File Offset: 0x0011B688
			public object Value
			{
				get
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)this.arrayEnumerator.Current;
					return dictionaryEntry.Value;
				}
			}

			// Token: 0x060043E5 RID: 17381 RVA: 0x0011D4AD File Offset: 0x0011B6AD
			public bool MoveNext()
			{
				return this.arrayEnumerator.MoveNext();
			}

			// Token: 0x060043E6 RID: 17382 RVA: 0x0011D4BA File Offset: 0x0011B6BA
			public void Reset()
			{
				this.arrayEnumerator.Reset();
			}

			// Token: 0x040034E8 RID: 13544
			private int _objectReturnType;

			// Token: 0x040034E9 RID: 13545
			internal const int Keys = 1;

			// Token: 0x040034EA RID: 13546
			internal const int Values = 2;

			// Token: 0x040034EB RID: 13547
			internal const int DictionaryEntry = 3;

			// Token: 0x040034EC RID: 13548
			private IEnumerator arrayEnumerator;
		}

		// Token: 0x020007EE RID: 2030
		private class OrderedDictionaryKeyValueCollection : ICollection, IEnumerable
		{
			// Token: 0x060043E7 RID: 17383 RVA: 0x0011D4C7 File Offset: 0x0011B6C7
			public OrderedDictionaryKeyValueCollection(ArrayList array, bool isKeys)
			{
				this._objects = array;
				this.isKeys = isKeys;
			}

			// Token: 0x060043E8 RID: 17384 RVA: 0x0011D4E0 File Offset: 0x0011B6E0
			void ICollection.CopyTo(Array array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				foreach (object obj in this._objects)
				{
					array.SetValue(this.isKeys ? ((DictionaryEntry)obj).Key : ((DictionaryEntry)obj).Value, index);
					index++;
				}
			}

			// Token: 0x17000F63 RID: 3939
			// (get) Token: 0x060043E9 RID: 17385 RVA: 0x0011D57C File Offset: 0x0011B77C
			int ICollection.Count
			{
				get
				{
					return this._objects.Count;
				}
			}

			// Token: 0x17000F64 RID: 3940
			// (get) Token: 0x060043EA RID: 17386 RVA: 0x0011D589 File Offset: 0x0011B789
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000F65 RID: 3941
			// (get) Token: 0x060043EB RID: 17387 RVA: 0x0011D58C File Offset: 0x0011B78C
			object ICollection.SyncRoot
			{
				get
				{
					return this._objects.SyncRoot;
				}
			}

			// Token: 0x060043EC RID: 17388 RVA: 0x0011D599 File Offset: 0x0011B799
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new OrderedDictionary.OrderedDictionaryEnumerator(this._objects, this.isKeys ? 1 : 2);
			}

			// Token: 0x040034ED RID: 13549
			private ArrayList _objects;

			// Token: 0x040034EE RID: 13550
			private bool isKeys;
		}
	}
}
