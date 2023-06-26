using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace System.Collections.ObjectModel
{
	/// <summary>Represents a read-only, generic collection of key/value pairs.</summary>
	/// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
	/// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
	// Token: 0x020004B5 RID: 1205
	[DebuggerTypeProxy(typeof(Mscorlib_DictionaryDebugView<, >))]
	[DebuggerDisplay("Count = {Count}")]
	[__DynamicallyInvokable]
	[Serializable]
	public class ReadOnlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" /> class that is a wrapper around the specified dictionary.</summary>
		/// <param name="dictionary">The dictionary to wrap.</param>
		// Token: 0x06003A02 RID: 14850 RVA: 0x000DEA98 File Offset: 0x000DCC98
		[__DynamicallyInvokable]
		public ReadOnlyDictionary(IDictionary<TKey, TValue> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this.m_dictionary = dictionary;
		}

		/// <summary>Gets the dictionary that is wrapped by this <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" /> object.</summary>
		/// <returns>The dictionary that is wrapped by this object.</returns>
		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06003A03 RID: 14851 RVA: 0x000DEAB5 File Offset: 0x000DCCB5
		[__DynamicallyInvokable]
		protected IDictionary<TKey, TValue> Dictionary
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_dictionary;
			}
		}

		/// <summary>Gets a key collection that contains the keys of the dictionary.</summary>
		/// <returns>A key collection that contains the keys of the dictionary.</returns>
		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06003A04 RID: 14852 RVA: 0x000DEABD File Offset: 0x000DCCBD
		[__DynamicallyInvokable]
		public ReadOnlyDictionary<TKey, TValue>.KeyCollection Keys
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_keys == null)
				{
					this.m_keys = new ReadOnlyDictionary<TKey, TValue>.KeyCollection(this.m_dictionary.Keys);
				}
				return this.m_keys;
			}
		}

		/// <summary>Gets a collection that contains the values in the dictionary.</summary>
		/// <returns>A collection that contains the values in the object that implements <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" />.</returns>
		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06003A05 RID: 14853 RVA: 0x000DEAE3 File Offset: 0x000DCCE3
		[__DynamicallyInvokable]
		public ReadOnlyDictionary<TKey, TValue>.ValueCollection Values
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_values == null)
				{
					this.m_values = new ReadOnlyDictionary<TKey, TValue>.ValueCollection(this.m_dictionary.Values);
				}
				return this.m_values;
			}
		}

		/// <summary>Determines whether the dictionary contains an element that has the specified key.</summary>
		/// <param name="key">The key to locate in the dictionary.</param>
		/// <returns>
		///   <see langword="true" /> if the dictionary contains an element that has the specified key; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003A06 RID: 14854 RVA: 0x000DEB09 File Offset: 0x000DCD09
		[__DynamicallyInvokable]
		public bool ContainsKey(TKey key)
		{
			return this.m_dictionary.ContainsKey(key);
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06003A07 RID: 14855 RVA: 0x000DEB17 File Offset: 0x000DCD17
		[__DynamicallyInvokable]
		ICollection<TKey> IDictionary<TKey, TValue>.Keys
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Keys;
			}
		}

		/// <summary>Retrieves the value that is associated with the specified key.</summary>
		/// <param name="key">The key whose value will be retrieved.</param>
		/// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if the object that implements <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" /> contains an element with the specified key; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003A08 RID: 14856 RVA: 0x000DEB1F File Offset: 0x000DCD1F
		[__DynamicallyInvokable]
		public bool TryGetValue(TKey key, out TValue value)
		{
			return this.m_dictionary.TryGetValue(key, out value);
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06003A09 RID: 14857 RVA: 0x000DEB2E File Offset: 0x000DCD2E
		[__DynamicallyInvokable]
		ICollection<TValue> IDictionary<TKey, TValue>.Values
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Values;
			}
		}

		/// <summary>Gets the element that has the specified key.</summary>
		/// <param name="key">The key of the element to get.</param>
		/// <returns>The element that has the specified key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The property is retrieved and <paramref name="key" /> is not found.</exception>
		// Token: 0x170008BE RID: 2238
		[__DynamicallyInvokable]
		public TValue this[TKey key]
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_dictionary[key];
			}
		}

		// Token: 0x06003A0B RID: 14859 RVA: 0x000DEB44 File Offset: 0x000DCD44
		[__DynamicallyInvokable]
		void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x06003A0C RID: 14860 RVA: 0x000DEB4D File Offset: 0x000DCD4D
		[__DynamicallyInvokable]
		bool IDictionary<TKey, TValue>.Remove(TKey key)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			return false;
		}

		// Token: 0x170008BF RID: 2239
		[__DynamicallyInvokable]
		TValue IDictionary<TKey, TValue>.this[TKey key]
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_dictionary[key];
			}
			[__DynamicallyInvokable]
			set
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
		}

		/// <summary>Gets the number of items in the dictionary.</summary>
		/// <returns>The number of items in the dictionary.</returns>
		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06003A0F RID: 14863 RVA: 0x000DEB6E File Offset: 0x000DCD6E
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_dictionary.Count;
			}
		}

		// Token: 0x06003A10 RID: 14864 RVA: 0x000DEB7B File Offset: 0x000DCD7B
		[__DynamicallyInvokable]
		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
		{
			return this.m_dictionary.Contains(item);
		}

		// Token: 0x06003A11 RID: 14865 RVA: 0x000DEB89 File Offset: 0x000DCD89
		[__DynamicallyInvokable]
		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			this.m_dictionary.CopyTo(array, arrayIndex);
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06003A12 RID: 14866 RVA: 0x000DEB98 File Offset: 0x000DCD98
		[__DynamicallyInvokable]
		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return true;
			}
		}

		// Token: 0x06003A13 RID: 14867 RVA: 0x000DEB9B File Offset: 0x000DCD9B
		[__DynamicallyInvokable]
		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x06003A14 RID: 14868 RVA: 0x000DEBA4 File Offset: 0x000DCDA4
		[__DynamicallyInvokable]
		void ICollection<KeyValuePair<TKey, TValue>>.Clear()
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		// Token: 0x06003A15 RID: 14869 RVA: 0x000DEBAD File Offset: 0x000DCDAD
		[__DynamicallyInvokable]
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			return false;
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" />.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x06003A16 RID: 14870 RVA: 0x000DEBB7 File Offset: 0x000DCDB7
		[__DynamicallyInvokable]
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return this.m_dictionary.GetEnumerator();
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		// Token: 0x06003A17 RID: 14871 RVA: 0x000DEBC4 File Offset: 0x000DCDC4
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.m_dictionary.GetEnumerator();
		}

		// Token: 0x06003A18 RID: 14872 RVA: 0x000DEBD1 File Offset: 0x000DCDD1
		private static bool IsCompatibleKey(object key)
		{
			if (key == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
			}
			return key is TKey;
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> exception in all cases.</summary>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="value">The value of the element to add.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06003A19 RID: 14873 RVA: 0x000DEBE5 File Offset: 0x000DCDE5
		[__DynamicallyInvokable]
		void IDictionary.Add(object key, object value)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> exception in all cases.</summary>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06003A1A RID: 14874 RVA: 0x000DEBEE File Offset: 0x000DCDEE
		[__DynamicallyInvokable]
		void IDictionary.Clear()
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		/// <summary>Determines whether the dictionary contains an element that has the specified key.</summary>
		/// <param name="key">The key to locate in the dictionary.</param>
		/// <returns>
		///   <see langword="true" /> if the dictionary contains an element that has the specified key; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x06003A1B RID: 14875 RVA: 0x000DEBF7 File Offset: 0x000DCDF7
		[__DynamicallyInvokable]
		bool IDictionary.Contains(object key)
		{
			return ReadOnlyDictionary<TKey, TValue>.IsCompatibleKey(key) && this.ContainsKey((TKey)((object)key));
		}

		/// <summary>Returns an enumerator for the dictionary.</summary>
		/// <returns>An enumerator for the dictionary.</returns>
		// Token: 0x06003A1C RID: 14876 RVA: 0x000DEC10 File Offset: 0x000DCE10
		[__DynamicallyInvokable]
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			IDictionary dictionary = this.m_dictionary as IDictionary;
			if (dictionary != null)
			{
				return dictionary.GetEnumerator();
			}
			return new ReadOnlyDictionary<TKey, TValue>.DictionaryEnumerator(this.m_dictionary);
		}

		/// <summary>Gets a value that indicates whether the dictionary has a fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> if the dictionary has a fixed size; otherwise, <see langword="false" />.</returns>
		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06003A1D RID: 14877 RVA: 0x000DEC43 File Offset: 0x000DCE43
		[__DynamicallyInvokable]
		bool IDictionary.IsFixedSize
		{
			[__DynamicallyInvokable]
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value that indicates whether the dictionary is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> in all cases.</returns>
		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06003A1E RID: 14878 RVA: 0x000DEC46 File Offset: 0x000DCE46
		[__DynamicallyInvokable]
		bool IDictionary.IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return true;
			}
		}

		/// <summary>Gets a collection that contains the keys of the dictionary.</summary>
		/// <returns>A collection that contains the keys of the dictionary.</returns>
		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06003A1F RID: 14879 RVA: 0x000DEC49 File Offset: 0x000DCE49
		[__DynamicallyInvokable]
		ICollection IDictionary.Keys
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Keys;
			}
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> exception in all cases.</summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x06003A20 RID: 14880 RVA: 0x000DEC51 File Offset: 0x000DCE51
		[__DynamicallyInvokable]
		void IDictionary.Remove(object key)
		{
			ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
		}

		/// <summary>Gets a collection that contains the values in the dictionary.</summary>
		/// <returns>A collection that contains the values in the dictionary.</returns>
		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06003A21 RID: 14881 RVA: 0x000DEC5A File Offset: 0x000DCE5A
		[__DynamicallyInvokable]
		ICollection IDictionary.Values
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Values;
			}
		}

		/// <summary>Gets the element that has the specified key.</summary>
		/// <param name="key">The key of the element to get or set.</param>
		/// <returns>The element that has the specified key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The property is set.  
		///  -or-  
		///  The property is set, <paramref name="key" /> does not exist in the collection, and the dictionary has a fixed size.</exception>
		// Token: 0x170008C6 RID: 2246
		[__DynamicallyInvokable]
		object IDictionary.this[object key]
		{
			[__DynamicallyInvokable]
			get
			{
				if (ReadOnlyDictionary<TKey, TValue>.IsCompatibleKey(key))
				{
					return this[(TKey)((object)key)];
				}
				return null;
			}
			[__DynamicallyInvokable]
			set
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}
		}

		/// <summary>Copies the elements of the dictionary to an array, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the dictionary. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the source dictionary is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.  
		/// -or-  
		/// The type of the source dictionary cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06003A24 RID: 14884 RVA: 0x000DEC88 File Offset: 0x000DCE88
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
				this.m_dictionary.CopyTo(array2, index);
				return;
			}
			DictionaryEntry[] array3 = array as DictionaryEntry[];
			if (array3 != null)
			{
				using (IEnumerator<KeyValuePair<TKey, TValue>> enumerator = this.m_dictionary.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<TKey, TValue> keyValuePair = enumerator.Current;
						array3[index++] = new DictionaryEntry(keyValuePair.Key, keyValuePair.Value);
					}
					return;
				}
			}
			object[] array4 = array as object[];
			if (array4 == null)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
			}
			try
			{
				foreach (KeyValuePair<TKey, TValue> keyValuePair2 in this.m_dictionary)
				{
					array4[index++] = new KeyValuePair<TKey, TValue>(keyValuePair2.Key, keyValuePair2.Value);
				}
			}
			catch (ArrayTypeMismatchException)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
			}
		}

		/// <summary>Gets a value that indicates whether access to the dictionary is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the dictionary is synchronized (thread safe); otherwise, <see langword="false" />.</returns>
		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06003A25 RID: 14885 RVA: 0x000DEDF4 File Offset: 0x000DCFF4
		[__DynamicallyInvokable]
		bool ICollection.IsSynchronized
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the dictionary.</summary>
		/// <returns>An object that can be used to synchronize access to the dictionary.</returns>
		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06003A26 RID: 14886 RVA: 0x000DEDF8 File Offset: 0x000DCFF8
		[__DynamicallyInvokable]
		object ICollection.SyncRoot
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_syncRoot == null)
				{
					ICollection collection = this.m_dictionary as ICollection;
					if (collection != null)
					{
						this.m_syncRoot = collection.SyncRoot;
					}
					else
					{
						Interlocked.CompareExchange<object>(ref this.m_syncRoot, new object(), null);
					}
				}
				return this.m_syncRoot;
			}
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06003A27 RID: 14887 RVA: 0x000DEE42 File Offset: 0x000DD042
		[__DynamicallyInvokable]
		IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Keys;
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06003A28 RID: 14888 RVA: 0x000DEE4A File Offset: 0x000DD04A
		[__DynamicallyInvokable]
		IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Values;
			}
		}

		// Token: 0x0400193F RID: 6463
		private readonly IDictionary<TKey, TValue> m_dictionary;

		// Token: 0x04001940 RID: 6464
		[NonSerialized]
		private object m_syncRoot;

		// Token: 0x04001941 RID: 6465
		[NonSerialized]
		private ReadOnlyDictionary<TKey, TValue>.KeyCollection m_keys;

		// Token: 0x04001942 RID: 6466
		[NonSerialized]
		private ReadOnlyDictionary<TKey, TValue>.ValueCollection m_values;

		// Token: 0x02000BD7 RID: 3031
		[Serializable]
		private struct DictionaryEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06006EF8 RID: 28408 RVA: 0x0017FA87 File Offset: 0x0017DC87
			public DictionaryEnumerator(IDictionary<TKey, TValue> dictionary)
			{
				this.m_dictionary = dictionary;
				this.m_enumerator = this.m_dictionary.GetEnumerator();
			}

			// Token: 0x170012FD RID: 4861
			// (get) Token: 0x06006EF9 RID: 28409 RVA: 0x0017FAA4 File Offset: 0x0017DCA4
			public DictionaryEntry Entry
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.m_enumerator.Current;
					object obj = keyValuePair.Key;
					keyValuePair = this.m_enumerator.Current;
					return new DictionaryEntry(obj, keyValuePair.Value);
				}
			}

			// Token: 0x170012FE RID: 4862
			// (get) Token: 0x06006EFA RID: 28410 RVA: 0x0017FAE8 File Offset: 0x0017DCE8
			public object Key
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.m_enumerator.Current;
					return keyValuePair.Key;
				}
			}

			// Token: 0x170012FF RID: 4863
			// (get) Token: 0x06006EFB RID: 28411 RVA: 0x0017FB10 File Offset: 0x0017DD10
			public object Value
			{
				get
				{
					KeyValuePair<TKey, TValue> keyValuePair = this.m_enumerator.Current;
					return keyValuePair.Value;
				}
			}

			// Token: 0x17001300 RID: 4864
			// (get) Token: 0x06006EFC RID: 28412 RVA: 0x0017FB35 File Offset: 0x0017DD35
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x06006EFD RID: 28413 RVA: 0x0017FB42 File Offset: 0x0017DD42
			public bool MoveNext()
			{
				return this.m_enumerator.MoveNext();
			}

			// Token: 0x06006EFE RID: 28414 RVA: 0x0017FB4F File Offset: 0x0017DD4F
			public void Reset()
			{
				this.m_enumerator.Reset();
			}

			// Token: 0x040035E2 RID: 13794
			private readonly IDictionary<TKey, TValue> m_dictionary;

			// Token: 0x040035E3 RID: 13795
			private IEnumerator<KeyValuePair<TKey, TValue>> m_enumerator;
		}

		/// <summary>Represents a read-only collection of the keys of a <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" /> object.</summary>
		/// <typeparam name="TKey" />
		/// <typeparam name="TValue" />
		// Token: 0x02000BD8 RID: 3032
		[DebuggerTypeProxy(typeof(Mscorlib_CollectionDebugView<>))]
		[DebuggerDisplay("Count = {Count}")]
		[__DynamicallyInvokable]
		[Serializable]
		public sealed class KeyCollection : ICollection<TKey>, IEnumerable<TKey>, IEnumerable, ICollection, IReadOnlyCollection<TKey>
		{
			// Token: 0x06006EFF RID: 28415 RVA: 0x0017FB5C File Offset: 0x0017DD5C
			internal KeyCollection(ICollection<TKey> collection)
			{
				if (collection == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
				}
				this.m_collection = collection;
			}

			// Token: 0x06006F00 RID: 28416 RVA: 0x0017FB74 File Offset: 0x0017DD74
			[__DynamicallyInvokable]
			void ICollection<TKey>.Add(TKey item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}

			// Token: 0x06006F01 RID: 28417 RVA: 0x0017FB7D File Offset: 0x0017DD7D
			[__DynamicallyInvokable]
			void ICollection<TKey>.Clear()
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}

			// Token: 0x06006F02 RID: 28418 RVA: 0x0017FB86 File Offset: 0x0017DD86
			[__DynamicallyInvokable]
			bool ICollection<TKey>.Contains(TKey item)
			{
				return this.m_collection.Contains(item);
			}

			/// <summary>Copies the elements of the collection to an array, starting at a specific array index.</summary>
			/// <param name="array">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
			/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is <see langword="null" />.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="arrayIndex" /> is less than 0.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="array" /> is multidimensional.  
			/// -or-  
			/// The number of elements in the source collection is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.  
			/// -or-  
			/// Type <paramref name="T" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
			// Token: 0x06006F03 RID: 28419 RVA: 0x0017FB94 File Offset: 0x0017DD94
			[__DynamicallyInvokable]
			public void CopyTo(TKey[] array, int arrayIndex)
			{
				this.m_collection.CopyTo(array, arrayIndex);
			}

			/// <summary>Gets the number of elements in the collection.</summary>
			/// <returns>The number of elements in the collection.</returns>
			// Token: 0x17001301 RID: 4865
			// (get) Token: 0x06006F04 RID: 28420 RVA: 0x0017FBA3 File Offset: 0x0017DDA3
			[__DynamicallyInvokable]
			public int Count
			{
				[__DynamicallyInvokable]
				get
				{
					return this.m_collection.Count;
				}
			}

			// Token: 0x17001302 RID: 4866
			// (get) Token: 0x06006F05 RID: 28421 RVA: 0x0017FBB0 File Offset: 0x0017DDB0
			[__DynamicallyInvokable]
			bool ICollection<TKey>.IsReadOnly
			{
				[__DynamicallyInvokable]
				get
				{
					return true;
				}
			}

			// Token: 0x06006F06 RID: 28422 RVA: 0x0017FBB3 File Offset: 0x0017DDB3
			[__DynamicallyInvokable]
			bool ICollection<TKey>.Remove(TKey item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
				return false;
			}

			/// <summary>Returns an enumerator that iterates through the collection.</summary>
			/// <returns>An enumerator that can be used to iterate through the collection.</returns>
			// Token: 0x06006F07 RID: 28423 RVA: 0x0017FBBD File Offset: 0x0017DDBD
			[__DynamicallyInvokable]
			public IEnumerator<TKey> GetEnumerator()
			{
				return this.m_collection.GetEnumerator();
			}

			/// <summary>Returns an enumerator that iterates through the collection.</summary>
			/// <returns>An enumerator that can be used to iterate through the collection.</returns>
			// Token: 0x06006F08 RID: 28424 RVA: 0x0017FBCA File Offset: 0x0017DDCA
			[__DynamicallyInvokable]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.m_collection.GetEnumerator();
			}

			/// <summary>Copies the elements of the collection to an array, starting at a specific array index.</summary>
			/// <param name="array">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is <see langword="null" />.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="array" /> is multidimensional.  
			/// -or-  
			/// The number of elements in the source collection is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
			// Token: 0x06006F09 RID: 28425 RVA: 0x0017FBD7 File Offset: 0x0017DDD7
			[__DynamicallyInvokable]
			void ICollection.CopyTo(Array array, int index)
			{
				ReadOnlyDictionaryHelpers.CopyToNonGenericICollectionHelper<TKey>(this.m_collection, array, index);
			}

			/// <summary>Gets a value that indicates whether access to the collection is synchronized (thread safe).</summary>
			/// <returns>
			///   <see langword="true" /> if access to the collection is synchronized (thread safe); otherwise, <see langword="false" />.</returns>
			// Token: 0x17001303 RID: 4867
			// (get) Token: 0x06006F0A RID: 28426 RVA: 0x0017FBE6 File Offset: 0x0017DDE6
			[__DynamicallyInvokable]
			bool ICollection.IsSynchronized
			{
				[__DynamicallyInvokable]
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
			/// <returns>An object that can be used to synchronize access to the collection.</returns>
			// Token: 0x17001304 RID: 4868
			// (get) Token: 0x06006F0B RID: 28427 RVA: 0x0017FBEC File Offset: 0x0017DDEC
			[__DynamicallyInvokable]
			object ICollection.SyncRoot
			{
				[__DynamicallyInvokable]
				get
				{
					if (this.m_syncRoot == null)
					{
						ICollection collection = this.m_collection as ICollection;
						if (collection != null)
						{
							this.m_syncRoot = collection.SyncRoot;
						}
						else
						{
							Interlocked.CompareExchange<object>(ref this.m_syncRoot, new object(), null);
						}
					}
					return this.m_syncRoot;
				}
			}

			// Token: 0x040035E4 RID: 13796
			private readonly ICollection<TKey> m_collection;

			// Token: 0x040035E5 RID: 13797
			[NonSerialized]
			private object m_syncRoot;
		}

		/// <summary>Represents a read-only collection of the values of a <see cref="T:System.Collections.ObjectModel.ReadOnlyDictionary`2" /> object.</summary>
		/// <typeparam name="TKey" />
		/// <typeparam name="TValue" />
		// Token: 0x02000BD9 RID: 3033
		[DebuggerTypeProxy(typeof(Mscorlib_CollectionDebugView<>))]
		[DebuggerDisplay("Count = {Count}")]
		[__DynamicallyInvokable]
		[Serializable]
		public sealed class ValueCollection : ICollection<TValue>, IEnumerable<TValue>, IEnumerable, ICollection, IReadOnlyCollection<TValue>
		{
			// Token: 0x06006F0C RID: 28428 RVA: 0x0017FC36 File Offset: 0x0017DE36
			internal ValueCollection(ICollection<TValue> collection)
			{
				if (collection == null)
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
				}
				this.m_collection = collection;
			}

			// Token: 0x06006F0D RID: 28429 RVA: 0x0017FC4E File Offset: 0x0017DE4E
			[__DynamicallyInvokable]
			void ICollection<TValue>.Add(TValue item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}

			// Token: 0x06006F0E RID: 28430 RVA: 0x0017FC57 File Offset: 0x0017DE57
			[__DynamicallyInvokable]
			void ICollection<TValue>.Clear()
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			}

			// Token: 0x06006F0F RID: 28431 RVA: 0x0017FC60 File Offset: 0x0017DE60
			[__DynamicallyInvokable]
			bool ICollection<TValue>.Contains(TValue item)
			{
				return this.m_collection.Contains(item);
			}

			/// <summary>Copies the elements of the collection to an array, starting at a specific array index.</summary>
			/// <param name="array">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
			/// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is <see langword="null" />.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="arrayIndex" /> is less than 0.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="array" /> is multidimensional.  
			/// -or-  
			/// The number of elements in the source collection is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.  
			/// -or-  
			/// Type <paramref name="T" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
			// Token: 0x06006F10 RID: 28432 RVA: 0x0017FC6E File Offset: 0x0017DE6E
			[__DynamicallyInvokable]
			public void CopyTo(TValue[] array, int arrayIndex)
			{
				this.m_collection.CopyTo(array, arrayIndex);
			}

			/// <summary>Gets the number of elements in the collection.</summary>
			/// <returns>The number of elements in the collection.</returns>
			// Token: 0x17001305 RID: 4869
			// (get) Token: 0x06006F11 RID: 28433 RVA: 0x0017FC7D File Offset: 0x0017DE7D
			[__DynamicallyInvokable]
			public int Count
			{
				[__DynamicallyInvokable]
				get
				{
					return this.m_collection.Count;
				}
			}

			// Token: 0x17001306 RID: 4870
			// (get) Token: 0x06006F12 RID: 28434 RVA: 0x0017FC8A File Offset: 0x0017DE8A
			[__DynamicallyInvokable]
			bool ICollection<TValue>.IsReadOnly
			{
				[__DynamicallyInvokable]
				get
				{
					return true;
				}
			}

			// Token: 0x06006F13 RID: 28435 RVA: 0x0017FC8D File Offset: 0x0017DE8D
			[__DynamicallyInvokable]
			bool ICollection<TValue>.Remove(TValue item)
			{
				ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
				return false;
			}

			/// <summary>Returns an enumerator that iterates through the collection.</summary>
			/// <returns>An enumerator that can be used to iterate through the collection.</returns>
			// Token: 0x06006F14 RID: 28436 RVA: 0x0017FC97 File Offset: 0x0017DE97
			[__DynamicallyInvokable]
			public IEnumerator<TValue> GetEnumerator()
			{
				return this.m_collection.GetEnumerator();
			}

			/// <summary>Returns an enumerator that iterates through the collection.</summary>
			/// <returns>An enumerator that can be used to iterate through the collection.</returns>
			// Token: 0x06006F15 RID: 28437 RVA: 0x0017FCA4 File Offset: 0x0017DEA4
			[__DynamicallyInvokable]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.m_collection.GetEnumerator();
			}

			/// <summary>Copies the elements of the collection to an array, starting at a specific array index.</summary>
			/// <param name="array">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is <see langword="null" />.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="array" /> is multidimensional.  
			/// -or-  
			/// The number of elements in the source collection is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
			// Token: 0x06006F16 RID: 28438 RVA: 0x0017FCB1 File Offset: 0x0017DEB1
			[__DynamicallyInvokable]
			void ICollection.CopyTo(Array array, int index)
			{
				ReadOnlyDictionaryHelpers.CopyToNonGenericICollectionHelper<TValue>(this.m_collection, array, index);
			}

			/// <summary>Gets a value that indicates whether access to the collection is synchronized (thread safe).</summary>
			/// <returns>
			///   <see langword="true" /> if access to the collection is synchronized (thread safe); otherwise, <see langword="false" />.</returns>
			// Token: 0x17001307 RID: 4871
			// (get) Token: 0x06006F17 RID: 28439 RVA: 0x0017FCC0 File Offset: 0x0017DEC0
			[__DynamicallyInvokable]
			bool ICollection.IsSynchronized
			{
				[__DynamicallyInvokable]
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
			/// <returns>An object that can be used to synchronize access to the collection.</returns>
			// Token: 0x17001308 RID: 4872
			// (get) Token: 0x06006F18 RID: 28440 RVA: 0x0017FCC4 File Offset: 0x0017DEC4
			[__DynamicallyInvokable]
			object ICollection.SyncRoot
			{
				[__DynamicallyInvokable]
				get
				{
					if (this.m_syncRoot == null)
					{
						ICollection collection = this.m_collection as ICollection;
						if (collection != null)
						{
							this.m_syncRoot = collection.SyncRoot;
						}
						else
						{
							Interlocked.CompareExchange<object>(ref this.m_syncRoot, new object(), null);
						}
					}
					return this.m_syncRoot;
				}
			}

			// Token: 0x040035E6 RID: 13798
			private readonly ICollection<TValue> m_collection;

			// Token: 0x040035E7 RID: 13799
			[NonSerialized]
			private object m_syncRoot;
		}
	}
}
