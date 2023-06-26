using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Collections.Generic
{
	/// <summary>Represents a collection of keys and values.</summary>
	/// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
	/// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
	// Token: 0x020004BD RID: 1213
	[DebuggerTypeProxy(typeof(Mscorlib_DictionaryDebugView<, >))]
	[DebuggerDisplay("Count = {Count}")]
	[ComVisible(false)]
	[__DynamicallyInvokable]
	[Serializable]
	public class Dictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, ISerializable, IDeserializationCallback
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2" /> class that is empty, has the default initial capacity, and uses the default equality comparer for the key type.</summary>
		// Token: 0x06003A51 RID: 14929 RVA: 0x000DF69C File Offset: 0x000DD89C
		[__DynamicallyInvokable]
		public Dictionary()
			: this(0, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2" /> class that is empty, has the specified initial capacity, and uses the default equality comparer for the key type.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Generic.Dictionary`2" /> can contain.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than 0.</exception>
		// Token: 0x06003A52 RID: 14930 RVA: 0x000DF6A6 File Offset: 0x000DD8A6
		[__DynamicallyInvokable]
		public Dictionary(int capacity)
			: this(capacity, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2" /> class that is empty, has the default initial capacity, and uses the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> implementation to use when comparing keys, or <see langword="null" /> to use the default <see cref="T:System.Collections.Generic.EqualityComparer`1" /> for the type of the key.</param>
		// Token: 0x06003A53 RID: 14931 RVA: 0x000DF6B0 File Offset: 0x000DD8B0
		[__DynamicallyInvokable]
		public Dictionary(IEqualityComparer<TKey> comparer)
			: this(0, comparer)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2" /> class that is empty, has the specified initial capacity, and uses the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.</summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:System.Collections.Generic.Dictionary`2" /> can contain.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> implementation to use when comparing keys, or <see langword="null" /> to use the default <see cref="T:System.Collections.Generic.EqualityComparer`1" /> for the type of the key.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than 0.</exception>
		// Token: 0x06003A54 RID: 14932 RVA: 0x000DF6BA File Offset: 0x000DD8BA
		[__DynamicallyInvokable]
		public Dictionary(int capacity, IEqualityComparer<TKey> comparer)
		{
			if (capacity < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity);
			}
			if (capacity > 0)
			{
				this.Initialize(capacity);
			}
			this.comparer = comparer ?? EqualityComparer<TKey>.Default;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2" /> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IDictionary`2" /> and uses the default equality comparer for the key type.</summary>
		/// <param name="dictionary">The <see cref="T:System.Collections.Generic.IDictionary`2" /> whose elements are copied to the new <see cref="T:System.Collections.Generic.Dictionary`2" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dictionary" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dictionary" /> contains one or more duplicate keys.</exception>
		// Token: 0x06003A55 RID: 14933 RVA: 0x000DF6E8 File Offset: 0x000DD8E8
		[__DynamicallyInvokable]
		public Dictionary(IDictionary<TKey, TValue> dictionary)
			: this(dictionary, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2" /> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IDictionary`2" /> and uses the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.</summary>
		/// <param name="dictionary">The <see cref="T:System.Collections.Generic.IDictionary`2" /> whose elements are copied to the new <see cref="T:System.Collections.Generic.Dictionary`2" />.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> implementation to use when comparing keys, or <see langword="null" /> to use the default <see cref="T:System.Collections.Generic.EqualityComparer`1" /> for the type of the key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dictionary" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dictionary" /> contains one or more duplicate keys.</exception>
		// Token: 0x06003A56 RID: 14934 RVA: 0x000DF6F4 File Offset: 0x000DD8F4
		[__DynamicallyInvokable]
		public Dictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
			: this((dictionary != null) ? dictionary.Count : 0, comparer)
		{
			if (dictionary == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
			}
			foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
			{
				this.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2" /> class with serialized data.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object containing the information required to serialize the <see cref="T:System.Collections.Generic.Dictionary`2" />.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure containing the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.Dictionary`2" />.</param>
		// Token: 0x06003A57 RID: 14935 RVA: 0x000DF768 File Offset: 0x000DD968
		protected Dictionary(SerializationInfo info, StreamingContext context)
		{
			HashHelpers.SerializationInfoTable.Add(this, info);
		}

		/// <summary>Gets the <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> that is used to determine equality of keys for the dictionary.</summary>
		/// <returns>The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> generic interface implementation that is used to determine equality of keys for the current <see cref="T:System.Collections.Generic.Dictionary`2" /> and to provide hash values for the keys.</returns>
		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06003A58 RID: 14936 RVA: 0x000DF77C File Offset: 0x000DD97C
		[__DynamicallyInvokable]
		public IEqualityComparer<TKey> Comparer
		{
			[__DynamicallyInvokable]
			get
			{
				return this.comparer;
			}
		}

		/// <summary>Gets the number of key/value pairs contained in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
		/// <returns>The number of key/value pairs contained in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</returns>
		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06003A59 RID: 14937 RVA: 0x000DF784 File Offset: 0x000DD984
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this.count - this.freeCount;
			}
		}

		/// <summary>Gets a collection containing the keys in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" /> containing the keys in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</returns>
		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06003A5A RID: 14938 RVA: 0x000DF793 File Offset: 0x000DD993
		[__DynamicallyInvokable]
		public Dictionary<TKey, TValue>.KeyCollection Keys
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.keys == null)
				{
					this.keys = new Dictionary<TKey, TValue>.KeyCollection(this);
				}
				return this.keys;
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06003A5B RID: 14939 RVA: 0x000DF7AF File Offset: 0x000DD9AF
		[__DynamicallyInvokable]
		ICollection<TKey> IDictionary<TKey, TValue>.Keys
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.keys == null)
				{
					this.keys = new Dictionary<TKey, TValue>.KeyCollection(this);
				}
				return this.keys;
			}
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06003A5C RID: 14940 RVA: 0x000DF7CB File Offset: 0x000DD9CB
		[__DynamicallyInvokable]
		IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.keys == null)
				{
					this.keys = new Dictionary<TKey, TValue>.KeyCollection(this);
				}
				return this.keys;
			}
		}

		/// <summary>Gets a collection containing the values in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" /> containing the values in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</returns>
		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06003A5D RID: 14941 RVA: 0x000DF7E7 File Offset: 0x000DD9E7
		[__DynamicallyInvokable]
		public Dictionary<TKey, TValue>.ValueCollection Values
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.values == null)
				{
					this.values = new Dictionary<TKey, TValue>.ValueCollection(this);
				}
				return this.values;
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06003A5E RID: 14942 RVA: 0x000DF803 File Offset: 0x000DDA03
		[__DynamicallyInvokable]
		ICollection<TValue> IDictionary<TKey, TValue>.Values
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.values == null)
				{
					this.values = new Dictionary<TKey, TValue>.ValueCollection(this);
				}
				return this.values;
			}
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06003A5F RID: 14943 RVA: 0x000DF81F File Offset: 0x000DDA1F
		[__DynamicallyInvokable]
		IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.values == null)
				{
					this.values = new Dictionary<TKey, TValue>.ValueCollection(this);
				}
				return this.values;
			}
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <param name="key">The key of the value to get or set.</param>
		/// <returns>The value associated with the specified key. If the specified key is not found, a get operation throws a <see cref="T:System.Collections.Generic.KeyNotFoundException" />, and a set operation creates a new element with the specified key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The property is retrieved and <paramref name="key" /> does not exist in the collection.</exception>
		// Token: 0x170008D7 RID: 2263
		[__DynamicallyInvokable]
		public TValue this[TKey key]
		{
			[__DynamicallyInvokable]
			get
			{
				int num = this.FindEntry(key);
				if (num >= 0)
				{
					return this.entries[num].value;
				}
				ThrowHelper.ThrowKeyNotFoundException();
				return default(TValue);
			}
			[__DynamicallyInvokable]
			set
			{
				this.Insert(key, value, false);
			}
		}

		/// <summary>Adds the specified key and value to the dictionary.</summary>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="value">The value of the element to add. The value can be <see langword="null" /> for reference types.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</exception>
		// Token: 0x06003A62 RID: 14946 RVA: 0x000DF880 File Offset: 0x000DDA80
		[__DynamicallyInvokable]
		public void Add(TKey key, TValue value)
		{
			this.Insert(key, value, true);
		}

		// Token: 0x06003A63 RID: 14947 RVA: 0x000DF88B File Offset: 0x000DDA8B
		[__DynamicallyInvokable]
		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
		{
			this.Add(keyValuePair.Key, keyValuePair.Value);
		}

		// Token: 0x06003A64 RID: 14948 RVA: 0x000DF8A4 File Offset: 0x000DDAA4
		[__DynamicallyInvokable]
		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
		{
			int num = this.FindEntry(keyValuePair.Key);
			return num >= 0 && EqualityComparer<TValue>.Default.Equals(this.entries[num].value, keyValuePair.Value);
		}

		// Token: 0x06003A65 RID: 14949 RVA: 0x000DF8EC File Offset: 0x000DDAEC
		[__DynamicallyInvokable]
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
		{
			int num = this.FindEntry(keyValuePair.Key);
			if (num >= 0 && EqualityComparer<TValue>.Default.Equals(this.entries[num].value, keyValuePair.Value))
			{
				this.Remove(keyValuePair.Key);
				return true;
			}
			return false;
		}

		/// <summary>Removes all keys and values from the <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
		// Token: 0x06003A66 RID: 14950 RVA: 0x000DF940 File Offset: 0x000DDB40
		[__DynamicallyInvokable]
		public void Clear()
		{
			if (this.count > 0)
			{
				for (int i = 0; i < this.buckets.Length; i++)
				{
					this.buckets[i] = -1;
				}
				Array.Clear(this.entries, 0, this.count);
				this.freeList = -1;
				this.count = 0;
				this.freeCount = 0;
				this.version++;
			}
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Generic.Dictionary`2" /> contains the specified key.</summary>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Generic.Dictionary`2" /> contains an element with the specified key; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x06003A67 RID: 14951 RVA: 0x000DF9A7 File Offset: 0x000DDBA7
		[__DynamicallyInvokable]
		public bool ContainsKey(TKey key)
		{
			return this.FindEntry(key) >= 0;
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Generic.Dictionary`2" /> contains a specific value.</summary>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.Generic.Dictionary`2" />. The value can be <see langword="null" /> for reference types.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Generic.Dictionary`2" /> contains an element with the specified value; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003A68 RID: 14952 RVA: 0x000DF9B8 File Offset: 0x000DDBB8
		[__DynamicallyInvokable]
		public bool ContainsValue(TValue value)
		{
			if (value == null)
			{
				for (int i = 0; i < this.count; i++)
				{
					if (this.entries[i].hashCode >= 0 && this.entries[i].value == null)
					{
						return true;
					}
				}
			}
			else
			{
				EqualityComparer<TValue> @default = EqualityComparer<TValue>.Default;
				for (int j = 0; j < this.count; j++)
				{
					if (this.entries[j].hashCode >= 0 && @default.Equals(this.entries[j].value, value))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06003A69 RID: 14953 RVA: 0x000DFA58 File Offset: 0x000DDC58
		private void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (index < 0 || index > array.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (array.Length - index < this.Count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
			}
			int num = this.count;
			Dictionary<TKey, TValue>.Entry[] array2 = this.entries;
			for (int i = 0; i < num; i++)
			{
				if (array2[i].hashCode >= 0)
				{
					array[index++] = new KeyValuePair<TKey, TValue>(array2[i].key, array2[i].value);
				}
			}
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2.Enumerator" /> structure for the <see cref="T:System.Collections.Generic.Dictionary`2" />.</returns>
		// Token: 0x06003A6A RID: 14954 RVA: 0x000DFAE5 File Offset: 0x000DDCE5
		[__DynamicallyInvokable]
		public Dictionary<TKey, TValue>.Enumerator GetEnumerator()
		{
			return new Dictionary<TKey, TValue>.Enumerator(this, 2);
		}

		// Token: 0x06003A6B RID: 14955 RVA: 0x000DFAEE File Offset: 0x000DDCEE
		[__DynamicallyInvokable]
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
		{
			return new Dictionary<TKey, TValue>.Enumerator(this, 2);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and returns the data needed to serialize the <see cref="T:System.Collections.Generic.Dictionary`2" /> instance.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the <see cref="T:System.Collections.Generic.Dictionary`2" /> instance.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure that contains the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.Dictionary`2" /> instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x06003A6C RID: 14956 RVA: 0x000DFAFC File Offset: 0x000DDCFC
		[SecurityCritical]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.info);
			}
			info.AddValue("Version", this.version);
			info.AddValue("Comparer", HashHelpers.GetEqualityComparerForSerialization(this.comparer), typeof(IEqualityComparer<TKey>));
			info.AddValue("HashSize", (this.buckets == null) ? 0 : this.buckets.Length);
			if (this.buckets != null)
			{
				KeyValuePair<TKey, TValue>[] array = new KeyValuePair<TKey, TValue>[this.Count];
				this.CopyTo(array, 0);
				info.AddValue("KeyValuePairs", array, typeof(KeyValuePair<TKey, TValue>[]));
			}
		}

		// Token: 0x06003A6D RID: 14957 RVA: 0x000DFB94 File Offset: 0x000DDD94
		private int FindEntry(TKey key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			if (this.buckets != null)
			{
				int num = this.comparer.GetHashCode(key) & int.MaxValue;
				for (int i = this.buckets[num % this.buckets.Length]; i >= 0; i = this.entries[i].next)
				{
					if (this.entries[i].hashCode == num && this.comparer.Equals(this.entries[i].key, key))
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06003A6E RID: 14958 RVA: 0x000DFC2C File Offset: 0x000DDE2C
		private void Initialize(int capacity)
		{
			int prime = HashHelpers.GetPrime(capacity);
			this.buckets = new int[prime];
			for (int i = 0; i < this.buckets.Length; i++)
			{
				this.buckets[i] = -1;
			}
			this.entries = new Dictionary<TKey, TValue>.Entry[prime];
			this.freeList = -1;
		}

		// Token: 0x06003A6F RID: 14959 RVA: 0x000DFC7C File Offset: 0x000DDE7C
		private void Insert(TKey key, TValue value, bool add)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			if (this.buckets == null)
			{
				this.Initialize(0);
			}
			int num = this.comparer.GetHashCode(key) & int.MaxValue;
			int num2 = num % this.buckets.Length;
			int num3 = 0;
			for (int i = this.buckets[num2]; i >= 0; i = this.entries[i].next)
			{
				if (this.entries[i].hashCode == num && this.comparer.Equals(this.entries[i].key, key))
				{
					if (add)
					{
						ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_AddingDuplicate);
					}
					this.entries[i].value = value;
					this.version++;
					return;
				}
				num3++;
			}
			int num4;
			if (this.freeCount > 0)
			{
				num4 = this.freeList;
				this.freeList = this.entries[num4].next;
				this.freeCount--;
			}
			else
			{
				if (this.count == this.entries.Length)
				{
					this.Resize();
					num2 = num % this.buckets.Length;
				}
				num4 = this.count;
				this.count++;
			}
			this.entries[num4].hashCode = num;
			this.entries[num4].next = this.buckets[num2];
			this.entries[num4].key = key;
			this.entries[num4].value = value;
			this.buckets[num2] = num4;
			this.version++;
			if (num3 > 100 && HashHelpers.IsWellKnownEqualityComparer(this.comparer))
			{
				this.comparer = (IEqualityComparer<TKey>)HashHelpers.GetRandomizedEqualityComparer(this.comparer);
				this.Resize(this.entries.Length, true);
			}
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and raises the deserialization event when the deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object associated with the current <see cref="T:System.Collections.Generic.Dictionary`2" /> instance is invalid.</exception>
		// Token: 0x06003A70 RID: 14960 RVA: 0x000DFE5C File Offset: 0x000DE05C
		public virtual void OnDeserialization(object sender)
		{
			SerializationInfo serializationInfo;
			HashHelpers.SerializationInfoTable.TryGetValue(this, out serializationInfo);
			if (serializationInfo == null)
			{
				return;
			}
			int @int = serializationInfo.GetInt32("Version");
			int int2 = serializationInfo.GetInt32("HashSize");
			this.comparer = (IEqualityComparer<TKey>)serializationInfo.GetValue("Comparer", typeof(IEqualityComparer<TKey>));
			if (int2 != 0)
			{
				this.buckets = new int[int2];
				for (int i = 0; i < this.buckets.Length; i++)
				{
					this.buckets[i] = -1;
				}
				this.entries = new Dictionary<TKey, TValue>.Entry[int2];
				this.freeList = -1;
				KeyValuePair<TKey, TValue>[] array = (KeyValuePair<TKey, TValue>[])serializationInfo.GetValue("KeyValuePairs", typeof(KeyValuePair<TKey, TValue>[]));
				if (array == null)
				{
					ThrowHelper.ThrowSerializationException(ExceptionResource.Serialization_MissingKeys);
				}
				for (int j = 0; j < array.Length; j++)
				{
					if (array[j].Key == null)
					{
						ThrowHelper.ThrowSerializationException(ExceptionResource.Serialization_NullKey);
					}
					this.Insert(array[j].Key, array[j].Value, true);
				}
			}
			else
			{
				this.buckets = null;
			}
			this.version = @int;
			HashHelpers.SerializationInfoTable.Remove(this);
		}

		// Token: 0x06003A71 RID: 14961 RVA: 0x000DFF88 File Offset: 0x000DE188
		private void Resize()
		{
			this.Resize(HashHelpers.ExpandPrime(this.count), false);
		}

		// Token: 0x06003A72 RID: 14962 RVA: 0x000DFF9C File Offset: 0x000DE19C
		private void Resize(int newSize, bool forceNewHashCodes)
		{
			int[] array = new int[newSize];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = -1;
			}
			Dictionary<TKey, TValue>.Entry[] array2 = new Dictionary<TKey, TValue>.Entry[newSize];
			Array.Copy(this.entries, 0, array2, 0, this.count);
			if (forceNewHashCodes)
			{
				for (int j = 0; j < this.count; j++)
				{
					if (array2[j].hashCode != -1)
					{
						array2[j].hashCode = this.comparer.GetHashCode(array2[j].key) & int.MaxValue;
					}
				}
			}
			for (int k = 0; k < this.count; k++)
			{
				if (array2[k].hashCode >= 0)
				{
					int num = array2[k].hashCode % newSize;
					array2[k].next = array[num];
					array[num] = k;
				}
			}
			this.buckets = array;
			this.entries = array2;
		}

		/// <summary>Removes the value with the specified key from the <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <returns>
		///   <see langword="true" /> if the element is successfully found and removed; otherwise, <see langword="false" />.  This method returns <see langword="false" /> if <paramref name="key" /> is not found in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x06003A73 RID: 14963 RVA: 0x000E0084 File Offset: 0x000DE284
		[__DynamicallyInvokable]
		public bool Remove(TKey key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			if (this.buckets != null)
			{
				int num = this.comparer.GetHashCode(key) & int.MaxValue;
				int num2 = num % this.buckets.Length;
				int num3 = -1;
				for (int i = this.buckets[num2]; i >= 0; i = this.entries[i].next)
				{
					if (this.entries[i].hashCode == num && this.comparer.Equals(this.entries[i].key, key))
					{
						if (num3 < 0)
						{
							this.buckets[num2] = this.entries[i].next;
						}
						else
						{
							this.entries[num3].next = this.entries[i].next;
						}
						this.entries[i].hashCode = -1;
						this.entries[i].next = this.freeList;
						this.entries[i].key = default(TKey);
						this.entries[i].value = default(TValue);
						this.freeList = i;
						this.freeCount++;
						this.version++;
						return true;
					}
					num3 = i;
				}
			}
			return false;
		}

		/// <summary>Gets the value associated with the specified key.</summary>
		/// <param name="key">The key of the value to get.</param>
		/// <param name="value">When this method returns, contains the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Generic.Dictionary`2" /> contains an element with the specified key; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x06003A74 RID: 14964 RVA: 0x000E01EC File Offset: 0x000DE3EC
		[__DynamicallyInvokable]
		public bool TryGetValue(TKey key, out TValue value)
		{
			int num = this.FindEntry(key);
			if (num >= 0)
			{
				value = this.entries[num].value;
				return true;
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x06003A75 RID: 14965 RVA: 0x000E0228 File Offset: 0x000DE428
		internal TValue GetValueOrDefault(TKey key)
		{
			int num = this.FindEntry(key);
			if (num >= 0)
			{
				return this.entries[num].value;
			}
			return default(TValue);
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06003A76 RID: 14966 RVA: 0x000E025C File Offset: 0x000DE45C
		[__DynamicallyInvokable]
		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x06003A77 RID: 14967 RVA: 0x000E025F File Offset: 0x000DE45F
		[__DynamicallyInvokable]
		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
		{
			this.CopyTo(array, index);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1" /> to an array, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1" />. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// <paramref name="array" /> does not have zero-based indexing.  
		/// -or-  
		/// The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.  
		/// -or-  
		/// The type of the source <see cref="T:System.Collections.Generic.ICollection`1" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06003A78 RID: 14968 RVA: 0x000E026C File Offset: 0x000DE46C
		[__DynamicallyInvokable]
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
			}
			if (array.Rank != 1)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
			}
			if (array.GetLowerBound(0) != 0)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
			}
			if (index < 0 || index > array.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (array.Length - index < this.Count)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
			}
			KeyValuePair<TKey, TValue>[] array2 = array as KeyValuePair<TKey, TValue>[];
			if (array2 != null)
			{
				this.CopyTo(array2, index);
				return;
			}
			if (array is DictionaryEntry[])
			{
				DictionaryEntry[] array3 = array as DictionaryEntry[];
				Dictionary<TKey, TValue>.Entry[] array4 = this.entries;
				for (int i = 0; i < this.count; i++)
				{
					if (array4[i].hashCode >= 0)
					{
						array3[index++] = new DictionaryEntry(array4[i].key, array4[i].value);
					}
				}
				return;
			}
			object[] array5 = array as object[];
			if (array5 == null)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
			}
			try
			{
				int num = this.count;
				Dictionary<TKey, TValue>.Entry[] array6 = this.entries;
				for (int j = 0; j < num; j++)
				{
					if (array6[j].hashCode >= 0)
					{
						array5[index++] = new KeyValuePair<TKey, TValue>(array6[j].key, array6[j].value);
					}
				}
			}
			catch (ArrayTypeMismatchException)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
			}
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06003A79 RID: 14969 RVA: 0x000E03DC File Offset: 0x000DE5DC
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Dictionary<TKey, TValue>.Enumerator(this, 2);
		}

		/// <summary>Gets a value that indicates whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, <see langword="false" />.  In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2" />, this property always returns <see langword="false" />.</returns>
		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06003A7A RID: 14970 RVA: 0x000E03EA File Offset: 0x000DE5EA
		[__DynamicallyInvokable]
		bool ICollection.IsSynchronized
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06003A7B RID: 14971 RVA: 0x000E03ED File Offset: 0x000DE5ED
		[__DynamicallyInvokable]
		object ICollection.SyncRoot
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Collections.IDictionary" /> has a fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IDictionary" /> has a fixed size; otherwise, <see langword="false" />.  In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2" />, this property always returns <see langword="false" />.</returns>
		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06003A7C RID: 14972 RVA: 0x000E040F File Offset: 0x000DE60F
		[__DynamicallyInvokable]
		bool IDictionary.IsFixedSize
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Collections.IDictionary" /> is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IDictionary" /> is read-only; otherwise, <see langword="false" />.  In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2" />, this property always returns <see langword="false" />.</returns>
		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06003A7D RID: 14973 RVA: 0x000E0412 File Offset: 0x000DE612
		[__DynamicallyInvokable]
		bool IDictionary.IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06003A7E RID: 14974 RVA: 0x000E0415 File Offset: 0x000DE615
		[__DynamicallyInvokable]
		ICollection IDictionary.Keys
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Keys;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06003A7F RID: 14975 RVA: 0x000E041D File Offset: 0x000DE61D
		[__DynamicallyInvokable]
		ICollection IDictionary.Values
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Values;
			}
		}

		/// <summary>Gets or sets the value with the specified key.</summary>
		/// <param name="key">The key of the value to get.</param>
		/// <returns>The value associated with the specified key, or <see langword="null" /> if <paramref name="key" /> is not in the dictionary or <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.Generic.Dictionary`2" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">A value is being assigned, and <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.Generic.Dictionary`2" />.  
		///  -or-  
		///  A value is being assigned, and <paramref name="value" /> is of a type that is not assignable to the value type <paramref name="TValue" /> of the <see cref="T:System.Collections.Generic.Dictionary`2" />.</exception>
		// Token: 0x170008DF RID: 2271
		[__DynamicallyInvokable]
		object IDictionary.this[object key]
		{
			[__DynamicallyInvokable]
			get
			{
				if (Dictionary<TKey, TValue>.IsCompatibleKey(key))
				{
					int num = this.FindEntry((TKey)((object)key));
					if (num >= 0)
					{
						return this.entries[num].value;
					}
				}
				return null;
			}
			[__DynamicallyInvokable]
			set
			{
				if (key == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
				}
				ThrowHelper.IfNullAndNullsAreIllegalThenThrow<TValue>(value, ExceptionArgument.value);
				try
				{
					TKey tkey = (TKey)((object)key);
					try
					{
						this[tkey] = (TValue)((object)value);
					}
					catch (InvalidCastException)
					{
						ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof(TValue));
					}
				}
				catch (InvalidCastException)
				{
					ThrowHelper.ThrowWrongKeyTypeArgumentException(key, typeof(TKey));
				}
			}
		}

		// Token: 0x06003A82 RID: 14978 RVA: 0x000E04E0 File Offset: 0x000DE6E0
		private static bool IsCompatibleKey(object key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			return key is TKey;
		}

		/// <summary>Adds the specified key and value to the dictionary.</summary>
		/// <param name="key">The object to use as the key.</param>
		/// <param name="value">The object to use as the value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.Generic.Dictionary`2" />.  
		/// -or-  
		/// <paramref name="value" /> is of a type that is not assignable to <paramref name="TValue" />, the type of values in the <see cref="T:System.Collections.Generic.Dictionary`2" />.  
		/// -or-  
		/// A value with the same key already exists in the <see cref="T:System.Collections.Generic.Dictionary`2" />.</exception>
		// Token: 0x06003A83 RID: 14979 RVA: 0x000E04F4 File Offset: 0x000DE6F4
		[__DynamicallyInvokable]
		void IDictionary.Add(object key, object value)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			ThrowHelper.IfNullAndNullsAreIllegalThenThrow<TValue>(value, ExceptionArgument.value);
			try
			{
				TKey tkey = (TKey)((object)key);
				try
				{
					this.Add(tkey, (TValue)((object)value));
				}
				catch (InvalidCastException)
				{
					ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof(TValue));
				}
			}
			catch (InvalidCastException)
			{
				ThrowHelper.ThrowWrongKeyTypeArgumentException(key, typeof(TKey));
			}
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IDictionary" /> contains an element with the specified key.</summary>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.IDictionary" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IDictionary" /> contains an element with the specified key; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x06003A84 RID: 14980 RVA: 0x000E056C File Offset: 0x000DE76C
		[__DynamicallyInvokable]
		bool IDictionary.Contains(object key)
		{
			return Dictionary<TKey, TValue>.IsCompatibleKey(key) && this.ContainsKey((TKey)((object)key));
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x06003A85 RID: 14981 RVA: 0x000E0584 File Offset: 0x000DE784
		[__DynamicallyInvokable]
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new Dictionary<TKey, TValue>.Enumerator(this, 1);
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x06003A86 RID: 14982 RVA: 0x000E0592 File Offset: 0x000DE792
		[__DynamicallyInvokable]
		void IDictionary.Remove(object key)
		{
			if (Dictionary<TKey, TValue>.IsCompatibleKey(key))
			{
				this.Remove((TKey)((object)key));
			}
		}

		// Token: 0x0400194A RID: 6474
		private int[] buckets;

		// Token: 0x0400194B RID: 6475
		private Dictionary<TKey, TValue>.Entry[] entries;

		// Token: 0x0400194C RID: 6476
		private int count;

		// Token: 0x0400194D RID: 6477
		private int version;

		// Token: 0x0400194E RID: 6478
		private int freeList;

		// Token: 0x0400194F RID: 6479
		private int freeCount;

		// Token: 0x04001950 RID: 6480
		private IEqualityComparer<TKey> comparer;

		// Token: 0x04001951 RID: 6481
		private Dictionary<TKey, TValue>.KeyCollection keys;

		// Token: 0x04001952 RID: 6482
		private Dictionary<TKey, TValue>.ValueCollection values;

		// Token: 0x04001953 RID: 6483
		private object _syncRoot;

		// Token: 0x04001954 RID: 6484
		private const string VersionName = "Version";

		// Token: 0x04001955 RID: 6485
		private const string HashSizeName = "HashSize";

		// Token: 0x04001956 RID: 6486
		private const string KeyValuePairsName = "KeyValuePairs";

		// Token: 0x04001957 RID: 6487
		private const string ComparerName = "Comparer";

		// Token: 0x02000BDA RID: 3034
		private struct Entry
		{
			// Token: 0x040035E8 RID: 13800
			public int hashCode;

			// Token: 0x040035E9 RID: 13801
			public int next;

			// Token: 0x040035EA RID: 13802
			public TKey key;

			// Token: 0x040035EB RID: 13803
			public TValue value;
		}

		/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
		/// <typeparam name="TKey" />
		/// <typeparam name="TValue" />
		// Token: 0x02000BDB RID: 3035
		[__DynamicallyInvokable]
		[Serializable]
		public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator, IDictionaryEnumerator
		{
			// Token: 0x06006F19 RID: 28441 RVA: 0x0017FD0E File Offset: 0x0017DF0E
			internal Enumerator(Dictionary<TKey, TValue> dictionary, int getEnumeratorRetType)
			{
				this.dictionary = dictionary;
				this.version = dictionary.version;
				this.index = 0;
				this.getEnumeratorRetType = getEnumeratorRetType;
				this.current = default(KeyValuePair<TKey, TValue>);
			}

			/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
			// Token: 0x06006F1A RID: 28442 RVA: 0x0017FD40 File Offset: 0x0017DF40
			[__DynamicallyInvokable]
			public bool MoveNext()
			{
				if (this.version != this.dictionary.version)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
				}
				while (this.index < this.dictionary.count)
				{
					if (this.dictionary.entries[this.index].hashCode >= 0)
					{
						this.current = new KeyValuePair<TKey, TValue>(this.dictionary.entries[this.index].key, this.dictionary.entries[this.index].value);
						this.index++;
						return true;
					}
					this.index++;
				}
				this.index = this.dictionary.count + 1;
				this.current = default(KeyValuePair<TKey, TValue>);
				return false;
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the <see cref="T:System.Collections.Generic.Dictionary`2" /> at the current position of the enumerator.</returns>
			// Token: 0x17001309 RID: 4873
			// (get) Token: 0x06006F1B RID: 28443 RVA: 0x0017FE1F File Offset: 0x0017E01F
			[__DynamicallyInvokable]
			public KeyValuePair<TKey, TValue> Current
			{
				[__DynamicallyInvokable]
				get
				{
					return this.current;
				}
			}

			/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.Dictionary`2.Enumerator" />.</summary>
			// Token: 0x06006F1C RID: 28444 RVA: 0x0017FE27 File Offset: 0x0017E027
			[__DynamicallyInvokable]
			public void Dispose()
			{
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator, as an <see cref="T:System.Object" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
			// Token: 0x1700130A RID: 4874
			// (get) Token: 0x06006F1D RID: 28445 RVA: 0x0017FE2C File Offset: 0x0017E02C
			[__DynamicallyInvokable]
			object IEnumerator.Current
			{
				[__DynamicallyInvokable]
				get
				{
					if (this.index == 0 || this.index == this.dictionary.count + 1)
					{
						ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
					}
					if (this.getEnumeratorRetType == 1)
					{
						return new DictionaryEntry(this.current.Key, this.current.Value);
					}
					return new KeyValuePair<TKey, TValue>(this.current.Key, this.current.Value);
				}
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
			// Token: 0x06006F1E RID: 28446 RVA: 0x0017FEB1 File Offset: 0x0017E0B1
			[__DynamicallyInvokable]
			void IEnumerator.Reset()
			{
				if (this.version != this.dictionary.version)
				{
					ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
				}
				this.index = 0;
				this.current = default(KeyValuePair<TKey, TValue>);
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the dictionary at the current position of the enumerator, as a <see cref="T:System.Collections.DictionaryEntry" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
			// Token: 0x1700130B RID: 4875
			// (get) Token: 0x06006F1F RID: 28447 RVA: 0x0017FEE0 File Offset: 0x0017E0E0
			[__DynamicallyInvokable]
			DictionaryEntry IDictionaryEnumerator.Entry
			{
				[__DynamicallyInvokable]
				get
				{
					if (this.index == 0 || this.index == this.dictionary.count + 1)
					{
						ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
					}
					return new DictionaryEntry(this.current.Key, this.current.Value);
				}
			}

			/// <summary>Gets the key of the element at the current position of the enumerator.</summary>
			/// <returns>The key of the element in the dictionary at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
			// Token: 0x1700130C RID: 4876
			// (get) Token: 0x06006F20 RID: 28448 RVA: 0x0017FF36 File Offset: 0x0017E136
			[__DynamicallyInvokable]
			object IDictionaryEnumerator.Key
			{
				[__DynamicallyInvokable]
				get
				{
					if (this.index == 0 || this.index == this.dictionary.count + 1)
					{
						ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
					}
					return this.current.Key;
				}
			}

			/// <summary>Gets the value of the element at the current position of the enumerator.</summary>
			/// <returns>The value of the element in the dictionary at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
			// Token: 0x1700130D RID: 4877
			// (get) Token: 0x06006F21 RID: 28449 RVA: 0x0017FF6C File Offset: 0x0017E16C
			[__DynamicallyInvokable]
			object IDictionaryEnumerator.Value
			{
				[__DynamicallyInvokable]
				get
				{
					if (this.index == 0 || this.index == this.dictionary.count + 1)
					{
						ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
					}
					return this.current.Value;
				}
			}

			// Token: 0x040035EC RID: 13804
			private Dictionary<TKey, TValue> dictionary;

			// Token: 0x040035ED RID: 13805
			private int version;

			// Token: 0x040035EE RID: 13806
			private int index;

			// Token: 0x040035EF RID: 13807
			private KeyValuePair<TKey, TValue> current;

			// Token: 0x040035F0 RID: 13808
			private int getEnumeratorRetType;

			// Token: 0x040035F1 RID: 13809
			internal const int DictEntry = 1;

			// Token: 0x040035F2 RID: 13810
			internal const int KeyValuePair = 2;
		}

		/// <summary>Represents the collection of keys in a <see cref="T:System.Collections.Generic.Dictionary`2" />. This class cannot be inherited.</summary>
		/// <typeparam name="TKey" />
		/// <typeparam name="TValue" />
		// Token: 0x02000BDC RID: 3036
		[DebuggerTypeProxy(typeof(Mscorlib_DictionaryKeyCollectionDebugView<, >))]
		[DebuggerDisplay("Count = {Count}")]
		[__DynamicallyInvokable]
		[Serializable]
		public sealed class KeyCollection : ICollection<TKey>, IEnumerable<TKey>, IEnumerable, ICollection, IReadOnlyCollection<TKey>
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" /> class that reflects the keys in the specified <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
			/// <param name="dictionary">The <see cref="T:System.Collections.Generic.Dictionary`2" /> whose keys are reflected in the new <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="dictionary" /> is <see langword="null" />.</exception>
			// Token: 0x06006F22 RID: 28450 RVA: 0x0017FFA2 File Offset: 0x0017E1A2
			[__DynamicallyInvokable]
			public KeyCollection(Dictionary<TKey, TValue> dictionary)
			{
				if (dictionary == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
				}
				this.dictionary = dictionary;
			}

			/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />.</summary>
			/// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection.Enumerator" /> for the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />.</returns>
			// Token: 0x06006F23 RID: 28451 RVA: 0x0017FFBA File Offset: 0x0017E1BA
			[__DynamicallyInvokable]
			public Dictionary<TKey, TValue>.KeyCollection.Enumerator GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.KeyCollection.Enumerator(this.dictionary);
			}

			/// <summary>Copies the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" /> elements to an existing one-dimensional <see cref="T:System.Array" />, starting at the specified array index.</summary>
			/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is <see langword="null" />.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than zero.</exception>
			/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
			// Token: 0x06006F24 RID: 28452 RVA: 0x0017FFC8 File Offset: 0x0017E1C8
			[__DynamicallyInvokable]
			public void CopyTo(TKey[] array, int index)
			{
				if (array == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				if (index < 0 || index > array.Length)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
				}
				if (array.Length - index < this.dictionary.Count)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
				}
				int count = this.dictionary.count;
				Dictionary<TKey, TValue>.Entry[] entries = this.dictionary.entries;
				for (int i = 0; i < count; i++)
				{
					if (entries[i].hashCode >= 0)
					{
						array[index++] = entries[i].key;
					}
				}
			}

			/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />.</summary>
			/// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />.  
			///  Retrieving the value of this property is an O(1) operation.</returns>
			// Token: 0x1700130E RID: 4878
			// (get) Token: 0x06006F25 RID: 28453 RVA: 0x00180053 File Offset: 0x0017E253
			[__DynamicallyInvokable]
			public int Count
			{
				[__DynamicallyInvokable]
				get
				{
					return this.dictionary.Count;
				}
			}

			// Token: 0x1700130F RID: 4879
			// (get) Token: 0x06006F26 RID: 28454 RVA: 0x00180060 File Offset: 0x0017E260
			[__DynamicallyInvokable]
			bool ICollection<TKey>.IsReadOnly
			{
				[__DynamicallyInvokable]
				get
				{
					return true;
				}
			}

			// Token: 0x06006F27 RID: 28455 RVA: 0x00180063 File Offset: 0x0017E263
			[__DynamicallyInvokable]
			void ICollection<TKey>.Add(TKey item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
			}

			// Token: 0x06006F28 RID: 28456 RVA: 0x0018006C File Offset: 0x0017E26C
			[__DynamicallyInvokable]
			void ICollection<TKey>.Clear()
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
			}

			// Token: 0x06006F29 RID: 28457 RVA: 0x00180075 File Offset: 0x0017E275
			[__DynamicallyInvokable]
			bool ICollection<TKey>.Contains(TKey item)
			{
				return this.dictionary.ContainsKey(item);
			}

			// Token: 0x06006F2A RID: 28458 RVA: 0x00180083 File Offset: 0x0017E283
			[__DynamicallyInvokable]
			bool ICollection<TKey>.Remove(TKey item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
				return false;
			}

			// Token: 0x06006F2B RID: 28459 RVA: 0x0018008D File Offset: 0x0017E28D
			[__DynamicallyInvokable]
			IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.KeyCollection.Enumerator(this.dictionary);
			}

			/// <summary>Returns an enumerator that iterates through a collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
			// Token: 0x06006F2C RID: 28460 RVA: 0x0018009F File Offset: 0x0017E29F
			[__DynamicallyInvokable]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.KeyCollection.Enumerator(this.dictionary);
			}

			/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
			/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is <see langword="null" />.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than zero.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="array" /> is multidimensional.  
			/// -or-  
			/// <paramref name="array" /> does not have zero-based indexing.  
			/// -or-  
			/// The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.  
			/// -or-  
			/// The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
			// Token: 0x06006F2D RID: 28461 RVA: 0x001800B4 File Offset: 0x0017E2B4
			[__DynamicallyInvokable]
			void ICollection.CopyTo(Array array, int index)
			{
				if (array == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				if (array.Rank != 1)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
				}
				if (array.GetLowerBound(0) != 0)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
				}
				if (index < 0 || index > array.Length)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
				}
				if (array.Length - index < this.dictionary.Count)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
				}
				TKey[] array2 = array as TKey[];
				if (array2 != null)
				{
					this.CopyTo(array2, index);
					return;
				}
				object[] array3 = array as object[];
				if (array3 == null)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
				}
				int count = this.dictionary.count;
				Dictionary<TKey, TValue>.Entry[] entries = this.dictionary.entries;
				try
				{
					for (int i = 0; i < count; i++)
					{
						if (entries[i].hashCode >= 0)
						{
							array3[index++] = entries[i].key;
						}
					}
				}
				catch (ArrayTypeMismatchException)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
				}
			}

			/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
			/// <returns>
			///   <see langword="true" /> if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, <see langword="false" />.  In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />, this property always returns <see langword="false" />.</returns>
			// Token: 0x17001310 RID: 4880
			// (get) Token: 0x06006F2E RID: 28462 RVA: 0x001801AC File Offset: 0x0017E3AC
			[__DynamicallyInvokable]
			bool ICollection.IsSynchronized
			{
				[__DynamicallyInvokable]
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
			/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />, this property always returns the current instance.</returns>
			// Token: 0x17001311 RID: 4881
			// (get) Token: 0x06006F2F RID: 28463 RVA: 0x001801AF File Offset: 0x0017E3AF
			[__DynamicallyInvokable]
			object ICollection.SyncRoot
			{
				[__DynamicallyInvokable]
				get
				{
					return ((ICollection)this.dictionary).SyncRoot;
				}
			}

			// Token: 0x040035F3 RID: 13811
			private Dictionary<TKey, TValue> dictionary;

			/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />.</summary>
			/// <typeparam name="TKey" />
			/// <typeparam name="TValue" />
			// Token: 0x02000D06 RID: 3334
			[__DynamicallyInvokable]
			[Serializable]
			public struct Enumerator : IEnumerator<TKey>, IDisposable, IEnumerator
			{
				// Token: 0x06007228 RID: 29224 RVA: 0x0018AAD6 File Offset: 0x00188CD6
				internal Enumerator(Dictionary<TKey, TValue> dictionary)
				{
					this.dictionary = dictionary;
					this.version = dictionary.version;
					this.index = 0;
					this.currentKey = default(TKey);
				}

				/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection.Enumerator" />.</summary>
				// Token: 0x06007229 RID: 29225 RVA: 0x0018AAFE File Offset: 0x00188CFE
				[__DynamicallyInvokable]
				public void Dispose()
				{
				}

				/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" />.</summary>
				/// <returns>
				///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
				/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
				// Token: 0x0600722A RID: 29226 RVA: 0x0018AB00 File Offset: 0x00188D00
				[__DynamicallyInvokable]
				public bool MoveNext()
				{
					if (this.version != this.dictionary.version)
					{
						ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
					}
					while (this.index < this.dictionary.count)
					{
						if (this.dictionary.entries[this.index].hashCode >= 0)
						{
							this.currentKey = this.dictionary.entries[this.index].key;
							this.index++;
							return true;
						}
						this.index++;
					}
					this.index = this.dictionary.count + 1;
					this.currentKey = default(TKey);
					return false;
				}

				/// <summary>Gets the element at the current position of the enumerator.</summary>
				/// <returns>The element in the <see cref="T:System.Collections.Generic.Dictionary`2.KeyCollection" /> at the current position of the enumerator.</returns>
				// Token: 0x1700138C RID: 5004
				// (get) Token: 0x0600722B RID: 29227 RVA: 0x0018ABB9 File Offset: 0x00188DB9
				[__DynamicallyInvokable]
				public TKey Current
				{
					[__DynamicallyInvokable]
					get
					{
						return this.currentKey;
					}
				}

				/// <summary>Gets the element at the current position of the enumerator.</summary>
				/// <returns>The element in the collection at the current position of the enumerator.</returns>
				/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
				// Token: 0x1700138D RID: 5005
				// (get) Token: 0x0600722C RID: 29228 RVA: 0x0018ABC1 File Offset: 0x00188DC1
				[__DynamicallyInvokable]
				object IEnumerator.Current
				{
					[__DynamicallyInvokable]
					get
					{
						if (this.index == 0 || this.index == this.dictionary.count + 1)
						{
							ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
						}
						return this.currentKey;
					}
				}

				/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
				/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
				// Token: 0x0600722D RID: 29229 RVA: 0x0018ABF2 File Offset: 0x00188DF2
				[__DynamicallyInvokable]
				void IEnumerator.Reset()
				{
					if (this.version != this.dictionary.version)
					{
						ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
					}
					this.index = 0;
					this.currentKey = default(TKey);
				}

				// Token: 0x04003952 RID: 14674
				private Dictionary<TKey, TValue> dictionary;

				// Token: 0x04003953 RID: 14675
				private int index;

				// Token: 0x04003954 RID: 14676
				private int version;

				// Token: 0x04003955 RID: 14677
				private TKey currentKey;
			}
		}

		/// <summary>Represents the collection of values in a <see cref="T:System.Collections.Generic.Dictionary`2" />. This class cannot be inherited.</summary>
		/// <typeparam name="TKey" />
		/// <typeparam name="TValue" />
		// Token: 0x02000BDD RID: 3037
		[DebuggerTypeProxy(typeof(Mscorlib_DictionaryValueCollectionDebugView<, >))]
		[DebuggerDisplay("Count = {Count}")]
		[__DynamicallyInvokable]
		[Serializable]
		public sealed class ValueCollection : ICollection<TValue>, IEnumerable<TValue>, IEnumerable, ICollection, IReadOnlyCollection<TValue>
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" /> class that reflects the values in the specified <see cref="T:System.Collections.Generic.Dictionary`2" />.</summary>
			/// <param name="dictionary">The <see cref="T:System.Collections.Generic.Dictionary`2" /> whose values are reflected in the new <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="dictionary" /> is <see langword="null" />.</exception>
			// Token: 0x06006F30 RID: 28464 RVA: 0x001801BC File Offset: 0x0017E3BC
			[__DynamicallyInvokable]
			public ValueCollection(Dictionary<TKey, TValue> dictionary)
			{
				if (dictionary == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
				}
				this.dictionary = dictionary;
			}

			/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />.</summary>
			/// <returns>A <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection.Enumerator" /> for the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />.</returns>
			// Token: 0x06006F31 RID: 28465 RVA: 0x001801D4 File Offset: 0x0017E3D4
			[__DynamicallyInvokable]
			public Dictionary<TKey, TValue>.ValueCollection.Enumerator GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.ValueCollection.Enumerator(this.dictionary);
			}

			/// <summary>Copies the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" /> elements to an existing one-dimensional <see cref="T:System.Array" />, starting at the specified array index.</summary>
			/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is <see langword="null" />.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than zero.</exception>
			/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
			// Token: 0x06006F32 RID: 28466 RVA: 0x001801E4 File Offset: 0x0017E3E4
			[__DynamicallyInvokable]
			public void CopyTo(TValue[] array, int index)
			{
				if (array == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				if (index < 0 || index > array.Length)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
				}
				if (array.Length - index < this.dictionary.Count)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
				}
				int count = this.dictionary.count;
				Dictionary<TKey, TValue>.Entry[] entries = this.dictionary.entries;
				for (int i = 0; i < count; i++)
				{
					if (entries[i].hashCode >= 0)
					{
						array[index++] = entries[i].value;
					}
				}
			}

			/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />.</summary>
			/// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />.</returns>
			// Token: 0x17001312 RID: 4882
			// (get) Token: 0x06006F33 RID: 28467 RVA: 0x0018026F File Offset: 0x0017E46F
			[__DynamicallyInvokable]
			public int Count
			{
				[__DynamicallyInvokable]
				get
				{
					return this.dictionary.Count;
				}
			}

			// Token: 0x17001313 RID: 4883
			// (get) Token: 0x06006F34 RID: 28468 RVA: 0x0018027C File Offset: 0x0017E47C
			[__DynamicallyInvokable]
			bool ICollection<TValue>.IsReadOnly
			{
				[__DynamicallyInvokable]
				get
				{
					return true;
				}
			}

			// Token: 0x06006F35 RID: 28469 RVA: 0x0018027F File Offset: 0x0017E47F
			[__DynamicallyInvokable]
			void ICollection<TValue>.Add(TValue item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);
			}

			// Token: 0x06006F36 RID: 28470 RVA: 0x00180288 File Offset: 0x0017E488
			[__DynamicallyInvokable]
			bool ICollection<TValue>.Remove(TValue item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);
				return false;
			}

			// Token: 0x06006F37 RID: 28471 RVA: 0x00180292 File Offset: 0x0017E492
			[__DynamicallyInvokable]
			void ICollection<TValue>.Clear()
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);
			}

			// Token: 0x06006F38 RID: 28472 RVA: 0x0018029B File Offset: 0x0017E49B
			[__DynamicallyInvokable]
			bool ICollection<TValue>.Contains(TValue item)
			{
				return this.dictionary.ContainsValue(item);
			}

			// Token: 0x06006F39 RID: 28473 RVA: 0x001802A9 File Offset: 0x0017E4A9
			[__DynamicallyInvokable]
			IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.ValueCollection.Enumerator(this.dictionary);
			}

			/// <summary>Returns an enumerator that iterates through a collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
			// Token: 0x06006F3A RID: 28474 RVA: 0x001802BB File Offset: 0x0017E4BB
			[__DynamicallyInvokable]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new Dictionary<TKey, TValue>.ValueCollection.Enumerator(this.dictionary);
			}

			/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
			/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is <see langword="null" />.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than zero.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="array" /> is multidimensional.  
			/// -or-  
			/// <paramref name="array" /> does not have zero-based indexing.  
			/// -or-  
			/// The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.  
			/// -or-  
			/// The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
			// Token: 0x06006F3B RID: 28475 RVA: 0x001802D0 File Offset: 0x0017E4D0
			[__DynamicallyInvokable]
			void ICollection.CopyTo(Array array, int index)
			{
				if (array == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				if (array.Rank != 1)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
				}
				if (array.GetLowerBound(0) != 0)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
				}
				if (index < 0 || index > array.Length)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
				}
				if (array.Length - index < this.dictionary.Count)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
				}
				TValue[] array2 = array as TValue[];
				if (array2 != null)
				{
					this.CopyTo(array2, index);
					return;
				}
				object[] array3 = array as object[];
				if (array3 == null)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
				}
				int count = this.dictionary.count;
				Dictionary<TKey, TValue>.Entry[] entries = this.dictionary.entries;
				try
				{
					for (int i = 0; i < count; i++)
					{
						if (entries[i].hashCode >= 0)
						{
							array3[index++] = entries[i].value;
						}
					}
				}
				catch (ArrayTypeMismatchException)
				{
					ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
				}
			}

			/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
			/// <returns>
			///   <see langword="true" /> if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, <see langword="false" />.  In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />, this property always returns <see langword="false" />.</returns>
			// Token: 0x17001314 RID: 4884
			// (get) Token: 0x06006F3C RID: 28476 RVA: 0x001803C8 File Offset: 0x0017E5C8
			[__DynamicallyInvokable]
			bool ICollection.IsSynchronized
			{
				[__DynamicallyInvokable]
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</summary>
			/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  In the default implementation of <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />, this property always returns the current instance.</returns>
			// Token: 0x17001315 RID: 4885
			// (get) Token: 0x06006F3D RID: 28477 RVA: 0x001803CB File Offset: 0x0017E5CB
			[__DynamicallyInvokable]
			object ICollection.SyncRoot
			{
				[__DynamicallyInvokable]
				get
				{
					return ((ICollection)this.dictionary).SyncRoot;
				}
			}

			// Token: 0x040035F4 RID: 13812
			private Dictionary<TKey, TValue> dictionary;

			/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />.</summary>
			/// <typeparam name="TKey" />
			/// <typeparam name="TValue" />
			// Token: 0x02000D07 RID: 3335
			[__DynamicallyInvokable]
			[Serializable]
			public struct Enumerator : IEnumerator<TValue>, IDisposable, IEnumerator
			{
				// Token: 0x0600722E RID: 29230 RVA: 0x0018AC21 File Offset: 0x00188E21
				internal Enumerator(Dictionary<TKey, TValue> dictionary)
				{
					this.dictionary = dictionary;
					this.version = dictionary.version;
					this.index = 0;
					this.currentValue = default(TValue);
				}

				/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection.Enumerator" />.</summary>
				// Token: 0x0600722F RID: 29231 RVA: 0x0018AC49 File Offset: 0x00188E49
				[__DynamicallyInvokable]
				public void Dispose()
				{
				}

				/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" />.</summary>
				/// <returns>
				///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
				/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
				// Token: 0x06007230 RID: 29232 RVA: 0x0018AC4C File Offset: 0x00188E4C
				[__DynamicallyInvokable]
				public bool MoveNext()
				{
					if (this.version != this.dictionary.version)
					{
						ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
					}
					while (this.index < this.dictionary.count)
					{
						if (this.dictionary.entries[this.index].hashCode >= 0)
						{
							this.currentValue = this.dictionary.entries[this.index].value;
							this.index++;
							return true;
						}
						this.index++;
					}
					this.index = this.dictionary.count + 1;
					this.currentValue = default(TValue);
					return false;
				}

				/// <summary>Gets the element at the current position of the enumerator.</summary>
				/// <returns>The element in the <see cref="T:System.Collections.Generic.Dictionary`2.ValueCollection" /> at the current position of the enumerator.</returns>
				// Token: 0x1700138E RID: 5006
				// (get) Token: 0x06007231 RID: 29233 RVA: 0x0018AD05 File Offset: 0x00188F05
				[__DynamicallyInvokable]
				public TValue Current
				{
					[__DynamicallyInvokable]
					get
					{
						return this.currentValue;
					}
				}

				/// <summary>Gets the element at the current position of the enumerator.</summary>
				/// <returns>The element in the collection at the current position of the enumerator.</returns>
				/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
				// Token: 0x1700138F RID: 5007
				// (get) Token: 0x06007232 RID: 29234 RVA: 0x0018AD0D File Offset: 0x00188F0D
				[__DynamicallyInvokable]
				object IEnumerator.Current
				{
					[__DynamicallyInvokable]
					get
					{
						if (this.index == 0 || this.index == this.dictionary.count + 1)
						{
							ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
						}
						return this.currentValue;
					}
				}

				/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
				/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
				// Token: 0x06007233 RID: 29235 RVA: 0x0018AD3E File Offset: 0x00188F3E
				[__DynamicallyInvokable]
				void IEnumerator.Reset()
				{
					if (this.version != this.dictionary.version)
					{
						ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
					}
					this.index = 0;
					this.currentValue = default(TValue);
				}

				// Token: 0x04003956 RID: 14678
				private Dictionary<TKey, TValue> dictionary;

				// Token: 0x04003957 RID: 14679
				private int index;

				// Token: 0x04003958 RID: 14680
				private int version;

				// Token: 0x04003959 RID: 14681
				private TValue currentValue;
			}
		}
	}
}
