using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	/// <summary>Represents a collection of key/value pairs that are sorted on the key.</summary>
	/// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
	/// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
	// Token: 0x020003C7 RID: 967
	[DebuggerTypeProxy(typeof(System_DictionaryDebugView<, >))]
	[DebuggerDisplay("Count = {Count}")]
	[global::__DynamicallyInvokable]
	[Serializable]
	public class SortedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary, ICollection, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> class that is empty and uses the default <see cref="T:System.Collections.Generic.IComparer`1" /> implementation for the key type.</summary>
		// Token: 0x0600249F RID: 9375 RVA: 0x000AAF78 File Offset: 0x000A9178
		[global::__DynamicallyInvokable]
		public SortedDictionary()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IDictionary`2" /> and uses the default <see cref="T:System.Collections.Generic.IComparer`1" /> implementation for the key type.</summary>
		/// <param name="dictionary">The <see cref="T:System.Collections.Generic.IDictionary`2" /> whose elements are copied to the new <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dictionary" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dictionary" /> contains one or more duplicate keys.</exception>
		// Token: 0x060024A0 RID: 9376 RVA: 0x000AAF81 File Offset: 0x000A9181
		[global::__DynamicallyInvokable]
		public SortedDictionary(IDictionary<TKey, TValue> dictionary)
			: this(dictionary, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IDictionary`2" /> and uses the specified <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to compare keys.</summary>
		/// <param name="dictionary">The <see cref="T:System.Collections.Generic.IDictionary`2" /> whose elements are copied to the new <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to use when comparing keys, or <see langword="null" /> to use the default <see cref="T:System.Collections.Generic.Comparer`1" /> for the type of the key.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dictionary" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dictionary" /> contains one or more duplicate keys.</exception>
		// Token: 0x060024A1 RID: 9377 RVA: 0x000AAF8C File Offset: 0x000A918C
		[global::__DynamicallyInvokable]
		public SortedDictionary(IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer)
		{
			if (dictionary == null)
			{
				System.ThrowHelper.ThrowArgumentNullException(System.ExceptionArgument.dictionary);
			}
			this._set = new TreeSet<KeyValuePair<TKey, TValue>>(new SortedDictionary<TKey, TValue>.KeyValuePairComparer(comparer));
			foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
			{
				this._set.Add(keyValuePair);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> class that is empty and uses the specified <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to compare keys.</summary>
		/// <param name="comparer">The <see cref="T:System.Collections.Generic.IComparer`1" /> implementation to use when comparing keys, or <see langword="null" /> to use the default <see cref="T:System.Collections.Generic.Comparer`1" /> for the type of the key.</param>
		// Token: 0x060024A2 RID: 9378 RVA: 0x000AAFFC File Offset: 0x000A91FC
		[global::__DynamicallyInvokable]
		public SortedDictionary(IComparer<TKey> comparer)
		{
			this._set = new TreeSet<KeyValuePair<TKey, TValue>>(new SortedDictionary<TKey, TValue>.KeyValuePairComparer(comparer));
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x000AB015 File Offset: 0x000A9215
		[global::__DynamicallyInvokable]
		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
		{
			this._set.Add(keyValuePair);
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x000AB024 File Offset: 0x000A9224
		[global::__DynamicallyInvokable]
		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
		{
			SortedSet<KeyValuePair<TKey, TValue>>.Node node = this._set.FindNode(keyValuePair);
			if (node == null)
			{
				return false;
			}
			if (keyValuePair.Value == null)
			{
				return node.Item.Value == null;
			}
			return EqualityComparer<TValue>.Default.Equals(node.Item.Value, keyValuePair.Value);
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x000AB084 File Offset: 0x000A9284
		[global::__DynamicallyInvokable]
		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
		{
			SortedSet<KeyValuePair<TKey, TValue>>.Node node = this._set.FindNode(keyValuePair);
			if (node == null)
			{
				return false;
			}
			if (EqualityComparer<TValue>.Default.Equals(node.Item.Value, keyValuePair.Value))
			{
				this._set.Remove(keyValuePair);
				return true;
			}
			return false;
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x060024A6 RID: 9382 RVA: 0x000AB0D1 File Offset: 0x000A92D1
		[global::__DynamicallyInvokable]
		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
		{
			[global::__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <param name="key">The key of the value to get or set.</param>
		/// <returns>The value associated with the specified key. If the specified key is not found, a get operation throws a <see cref="T:System.Collections.Generic.KeyNotFoundException" />, and a set operation creates a new element with the specified key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The property is retrieved and <paramref name="key" /> does not exist in the collection.</exception>
		// Token: 0x1700093D RID: 2365
		[global::__DynamicallyInvokable]
		public TValue this[TKey key]
		{
			[global::__DynamicallyInvokable]
			get
			{
				if (key == null)
				{
					System.ThrowHelper.ThrowArgumentNullException(System.ExceptionArgument.key);
				}
				SortedSet<KeyValuePair<TKey, TValue>>.Node node = this._set.FindNode(new KeyValuePair<TKey, TValue>(key, default(TValue)));
				if (node == null)
				{
					System.ThrowHelper.ThrowKeyNotFoundException();
				}
				return node.Item.Value;
			}
			[global::__DynamicallyInvokable]
			set
			{
				if (key == null)
				{
					System.ThrowHelper.ThrowArgumentNullException(System.ExceptionArgument.key);
				}
				SortedSet<KeyValuePair<TKey, TValue>>.Node node = this._set.FindNode(new KeyValuePair<TKey, TValue>(key, default(TValue)));
				if (node == null)
				{
					this._set.Add(new KeyValuePair<TKey, TValue>(key, value));
					return;
				}
				node.Item = new KeyValuePair<TKey, TValue>(node.Item.Key, value);
				this._set.UpdateVersion();
			}
		}

		/// <summary>Gets the number of key/value pairs contained in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
		/// <returns>The number of key/value pairs contained in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</returns>
		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x060024A9 RID: 9385 RVA: 0x000AB18F File Offset: 0x000A938F
		[global::__DynamicallyInvokable]
		public int Count
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this._set.Count;
			}
		}

		/// <summary>Gets the <see cref="T:System.Collections.Generic.IComparer`1" /> used to order the elements of the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
		/// <returns>The <see cref="T:System.Collections.Generic.IComparer`1" /> used to order the elements of the <see cref="T:System.Collections.Generic.SortedDictionary`2" /></returns>
		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x060024AA RID: 9386 RVA: 0x000AB19C File Offset: 0x000A939C
		[global::__DynamicallyInvokable]
		public IComparer<TKey> Comparer
		{
			[global::__DynamicallyInvokable]
			get
			{
				return ((SortedDictionary<TKey, TValue>.KeyValuePairComparer)this._set.Comparer).keyComparer;
			}
		}

		/// <summary>Gets a collection containing the keys in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" /> containing the keys in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</returns>
		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x060024AB RID: 9387 RVA: 0x000AB1B3 File Offset: 0x000A93B3
		[global::__DynamicallyInvokable]
		public SortedDictionary<TKey, TValue>.KeyCollection Keys
		{
			[global::__DynamicallyInvokable]
			get
			{
				if (this.keys == null)
				{
					this.keys = new SortedDictionary<TKey, TValue>.KeyCollection(this);
				}
				return this.keys;
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x060024AC RID: 9388 RVA: 0x000AB1CF File Offset: 0x000A93CF
		[global::__DynamicallyInvokable]
		ICollection<TKey> IDictionary<TKey, TValue>.Keys
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.Keys;
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x060024AD RID: 9389 RVA: 0x000AB1D7 File Offset: 0x000A93D7
		[global::__DynamicallyInvokable]
		IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.Keys;
			}
		}

		/// <summary>Gets a collection containing the values in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" /> containing the values in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</returns>
		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x060024AE RID: 9390 RVA: 0x000AB1DF File Offset: 0x000A93DF
		[global::__DynamicallyInvokable]
		public SortedDictionary<TKey, TValue>.ValueCollection Values
		{
			[global::__DynamicallyInvokable]
			get
			{
				if (this.values == null)
				{
					this.values = new SortedDictionary<TKey, TValue>.ValueCollection(this);
				}
				return this.values;
			}
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x060024AF RID: 9391 RVA: 0x000AB1FB File Offset: 0x000A93FB
		[global::__DynamicallyInvokable]
		ICollection<TValue> IDictionary<TKey, TValue>.Values
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.Values;
			}
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x060024B0 RID: 9392 RVA: 0x000AB203 File Offset: 0x000A9403
		[global::__DynamicallyInvokable]
		IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.Values;
			}
		}

		/// <summary>Adds an element with the specified key and value into the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="value">The value of the element to add. The value can be <see langword="null" /> for reference types.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</exception>
		// Token: 0x060024B1 RID: 9393 RVA: 0x000AB20B File Offset: 0x000A940B
		[global::__DynamicallyInvokable]
		public void Add(TKey key, TValue value)
		{
			if (key == null)
			{
				System.ThrowHelper.ThrowArgumentNullException(System.ExceptionArgument.key);
			}
			this._set.Add(new KeyValuePair<TKey, TValue>(key, value));
		}

		/// <summary>Removes all elements from the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
		// Token: 0x060024B2 RID: 9394 RVA: 0x000AB22E File Offset: 0x000A942E
		[global::__DynamicallyInvokable]
		public void Clear()
		{
			this._set.Clear();
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> contains an element with the specified key.</summary>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> contains an element with the specified key; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x060024B3 RID: 9395 RVA: 0x000AB23C File Offset: 0x000A943C
		[global::__DynamicallyInvokable]
		public bool ContainsKey(TKey key)
		{
			if (key == null)
			{
				System.ThrowHelper.ThrowArgumentNullException(System.ExceptionArgument.key);
			}
			return this._set.Contains(new KeyValuePair<TKey, TValue>(key, default(TValue)));
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> contains an element with the specified value.</summary>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />. The value can be <see langword="null" /> for reference types.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> contains an element with the specified value; otherwise, <see langword="false" />.</returns>
		// Token: 0x060024B4 RID: 9396 RVA: 0x000AB274 File Offset: 0x000A9474
		[global::__DynamicallyInvokable]
		public bool ContainsValue(TValue value)
		{
			bool found = false;
			if (value == null)
			{
				this._set.InOrderTreeWalk(delegate(SortedSet<KeyValuePair<TKey, TValue>>.Node node)
				{
					if (node.Item.Value == null)
					{
						found = true;
						return false;
					}
					return true;
				});
			}
			else
			{
				EqualityComparer<TValue> valueComparer = EqualityComparer<TValue>.Default;
				this._set.InOrderTreeWalk(delegate(SortedSet<KeyValuePair<TKey, TValue>>.Node node)
				{
					if (valueComparer.Equals(node.Item.Value, value))
					{
						found = true;
						return false;
					}
					return true;
				});
			}
			return found;
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> to the specified array of <see cref="T:System.Collections.Generic.KeyValuePair`2" /> structures, starting at the specified index.</summary>
		/// <param name="array">The one-dimensional array of <see cref="T:System.Collections.Generic.KeyValuePair`2" /> structures that is the destination of the elements copied from the current <see cref="T:System.Collections.Generic.SortedDictionary`2" /> The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0.</exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.SortedDictionary`2" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x060024B5 RID: 9397 RVA: 0x000AB2F2 File Offset: 0x000A94F2
		[global::__DynamicallyInvokable]
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
		{
			this._set.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
		/// <returns>A <see cref="T:System.Collections.Generic.SortedDictionary`2.Enumerator" /> for the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</returns>
		// Token: 0x060024B6 RID: 9398 RVA: 0x000AB301 File Offset: 0x000A9501
		[global::__DynamicallyInvokable]
		public SortedDictionary<TKey, TValue>.Enumerator GetEnumerator()
		{
			return new SortedDictionary<TKey, TValue>.Enumerator(this, 1);
		}

		// Token: 0x060024B7 RID: 9399 RVA: 0x000AB30A File Offset: 0x000A950A
		[global::__DynamicallyInvokable]
		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
		{
			return new SortedDictionary<TKey, TValue>.Enumerator(this, 1);
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <returns>
		///   <see langword="true" /> if the element is successfully removed; otherwise, <see langword="false" />.  This method also returns <see langword="false" /> if <paramref name="key" /> is not found in the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x060024B8 RID: 9400 RVA: 0x000AB318 File Offset: 0x000A9518
		[global::__DynamicallyInvokable]
		public bool Remove(TKey key)
		{
			if (key == null)
			{
				System.ThrowHelper.ThrowArgumentNullException(System.ExceptionArgument.key);
			}
			return this._set.Remove(new KeyValuePair<TKey, TValue>(key, default(TValue)));
		}

		/// <summary>Gets the value associated with the specified key.</summary>
		/// <param name="key">The key of the value to get.</param>
		/// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> contains an element with the specified key; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x060024B9 RID: 9401 RVA: 0x000AB350 File Offset: 0x000A9550
		[global::__DynamicallyInvokable]
		public bool TryGetValue(TKey key, out TValue value)
		{
			if (key == null)
			{
				System.ThrowHelper.ThrowArgumentNullException(System.ExceptionArgument.key);
			}
			SortedSet<KeyValuePair<TKey, TValue>>.Node node = this._set.FindNode(new KeyValuePair<TKey, TValue>(key, default(TValue)));
			if (node == null)
			{
				value = default(TValue);
				return false;
			}
			value = node.Item.Value;
			return true;
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1" /> to an array, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.ICollection`1" />. The array must have zero-based indexing.</param>
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
		// Token: 0x060024BA RID: 9402 RVA: 0x000AB3A4 File Offset: 0x000A95A4
		[global::__DynamicallyInvokable]
		void ICollection.CopyTo(Array array, int index)
		{
			((ICollection)this._set).CopyTo(array, index);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> has a fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IDictionary" /> has a fixed size; otherwise, <see langword="false" />.  In the default implementation of <see cref="T:System.Collections.Generic.SortedDictionary`2" />, this property always returns <see langword="false" />.</returns>
		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x060024BB RID: 9403 RVA: 0x000AB3B3 File Offset: 0x000A95B3
		[global::__DynamicallyInvokable]
		bool IDictionary.IsFixedSize
		{
			[global::__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IDictionary" /> is read-only; otherwise, <see langword="false" />.  In the default implementation of <see cref="T:System.Collections.Generic.SortedDictionary`2" />, this property always returns <see langword="false" />.</returns>
		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x060024BC RID: 9404 RVA: 0x000AB3B6 File Offset: 0x000A95B6
		[global::__DynamicallyInvokable]
		bool IDictionary.IsReadOnly
		{
			[global::__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x060024BD RID: 9405 RVA: 0x000AB3B9 File Offset: 0x000A95B9
		[global::__DynamicallyInvokable]
		ICollection IDictionary.Keys
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.Keys;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x060024BE RID: 9406 RVA: 0x000AB3C1 File Offset: 0x000A95C1
		[global::__DynamicallyInvokable]
		ICollection IDictionary.Values
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.Values;
			}
		}

		/// <summary>Gets or sets the element with the specified key.</summary>
		/// <param name="key">The key of the element to get.</param>
		/// <returns>The element with the specified key, or <see langword="null" /> if <paramref name="key" /> is not in the dictionary or <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">A value is being assigned, and <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.  
		///  -or-  
		///  A value is being assigned, and <paramref name="value" /> is of a type that is not assignable to the value type <paramref name="TValue" /> of the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</exception>
		// Token: 0x1700094A RID: 2378
		[global::__DynamicallyInvokable]
		object IDictionary.this[object key]
		{
			[global::__DynamicallyInvokable]
			get
			{
				TValue tvalue;
				if (SortedDictionary<TKey, TValue>.IsCompatibleKey(key) && this.TryGetValue((TKey)((object)key), out tvalue))
				{
					return tvalue;
				}
				return null;
			}
			[global::__DynamicallyInvokable]
			set
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

		/// <summary>Adds an element with the provided key and value to the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <param name="key">The object to use as the key of the element to add.</param>
		/// <param name="value">The object to use as the value of the element to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is of a type that is not assignable to the key type <paramref name="TKey" /> of the <see cref="T:System.Collections.IDictionary" />.  
		/// -or-  
		/// <paramref name="value" /> is of a type that is not assignable to the value type <paramref name="TValue" /> of the <see cref="T:System.Collections.IDictionary" />.  
		/// -or-  
		/// An element with the same key already exists in the <see cref="T:System.Collections.IDictionary" />.</exception>
		// Token: 0x060024C1 RID: 9409 RVA: 0x000AB474 File Offset: 0x000A9674
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

		/// <summary>Determines whether the <see cref="T:System.Collections.IDictionary" /> contains an element with the specified key.</summary>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.IDictionary" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IDictionary" /> contains an element with the key; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x060024C2 RID: 9410 RVA: 0x000AB4EC File Offset: 0x000A96EC
		[global::__DynamicallyInvokable]
		bool IDictionary.Contains(object key)
		{
			return SortedDictionary<TKey, TValue>.IsCompatibleKey(key) && this.ContainsKey((TKey)((object)key));
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x000AB504 File Offset: 0x000A9704
		private static bool IsCompatibleKey(object key)
		{
			if (key == null)
			{
				System.ThrowHelper.ThrowArgumentNullException(System.ExceptionArgument.key);
			}
			return key is TKey;
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x060024C4 RID: 9412 RVA: 0x000AB518 File Offset: 0x000A9718
		[global::__DynamicallyInvokable]
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new SortedDictionary<TKey, TValue>.Enumerator(this, 2);
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x060024C5 RID: 9413 RVA: 0x000AB526 File Offset: 0x000A9726
		[global::__DynamicallyInvokable]
		void IDictionary.Remove(object key)
		{
			if (SortedDictionary<TKey, TValue>.IsCompatibleKey(key))
			{
				this.Remove((TKey)((object)key));
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, <see langword="false" />.  In the default implementation of <see cref="T:System.Collections.Generic.SortedDictionary`2" />, this property always returns <see langword="false" />.</returns>
		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x060024C6 RID: 9414 RVA: 0x000AB53D File Offset: 0x000A973D
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
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x060024C7 RID: 9415 RVA: 0x000AB540 File Offset: 0x000A9740
		[global::__DynamicallyInvokable]
		object ICollection.SyncRoot
		{
			[global::__DynamicallyInvokable]
			get
			{
				return ((ICollection)this._set).SyncRoot;
			}
		}

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x060024C8 RID: 9416 RVA: 0x000AB54D File Offset: 0x000A974D
		[global::__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SortedDictionary<TKey, TValue>.Enumerator(this, 1);
		}

		// Token: 0x04002015 RID: 8213
		[NonSerialized]
		private SortedDictionary<TKey, TValue>.KeyCollection keys;

		// Token: 0x04002016 RID: 8214
		[NonSerialized]
		private SortedDictionary<TKey, TValue>.ValueCollection values;

		// Token: 0x04002017 RID: 8215
		private TreeSet<KeyValuePair<TKey, TValue>> _set;

		/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
		/// <typeparam name="TKey" />
		/// <typeparam name="TValue" />
		// Token: 0x020007F9 RID: 2041
		[global::__DynamicallyInvokable]
		public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator, IDictionaryEnumerator
		{
			// Token: 0x06004451 RID: 17489 RVA: 0x0011E5C6 File Offset: 0x0011C7C6
			internal Enumerator(SortedDictionary<TKey, TValue> dictionary, int getEnumeratorRetType)
			{
				this.treeEnum = dictionary._set.GetEnumerator();
				this.getEnumeratorRetType = getEnumeratorRetType;
			}

			/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
			/// <returns>
			///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
			// Token: 0x06004452 RID: 17490 RVA: 0x0011E5E0 File Offset: 0x0011C7E0
			[global::__DynamicallyInvokable]
			public bool MoveNext()
			{
				return this.treeEnum.MoveNext();
			}

			/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.SortedDictionary`2.Enumerator" />.</summary>
			// Token: 0x06004453 RID: 17491 RVA: 0x0011E5ED File Offset: 0x0011C7ED
			[global::__DynamicallyInvokable]
			public void Dispose()
			{
				this.treeEnum.Dispose();
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the <see cref="T:System.Collections.Generic.SortedDictionary`2" /> at the current position of the enumerator.</returns>
			// Token: 0x17000F85 RID: 3973
			// (get) Token: 0x06004454 RID: 17492 RVA: 0x0011E5FA File Offset: 0x0011C7FA
			[global::__DynamicallyInvokable]
			public KeyValuePair<TKey, TValue> Current
			{
				[global::__DynamicallyInvokable]
				get
				{
					return this.treeEnum.Current;
				}
			}

			// Token: 0x17000F86 RID: 3974
			// (get) Token: 0x06004455 RID: 17493 RVA: 0x0011E607 File Offset: 0x0011C807
			internal bool NotStartedOrEnded
			{
				get
				{
					return this.treeEnum.NotStartedOrEnded;
				}
			}

			// Token: 0x06004456 RID: 17494 RVA: 0x0011E614 File Offset: 0x0011C814
			internal void Reset()
			{
				this.treeEnum.Reset();
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
			// Token: 0x06004457 RID: 17495 RVA: 0x0011E621 File Offset: 0x0011C821
			[global::__DynamicallyInvokable]
			void IEnumerator.Reset()
			{
				this.treeEnum.Reset();
			}

			/// <summary>Gets the element at the current position of the enumerator.</summary>
			/// <returns>The element in the collection at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
			// Token: 0x17000F87 RID: 3975
			// (get) Token: 0x06004458 RID: 17496 RVA: 0x0011E630 File Offset: 0x0011C830
			[global::__DynamicallyInvokable]
			object IEnumerator.Current
			{
				[global::__DynamicallyInvokable]
				get
				{
					if (this.NotStartedOrEnded)
					{
						System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumOpCantHappen);
					}
					KeyValuePair<TKey, TValue> keyValuePair;
					if (this.getEnumeratorRetType == 2)
					{
						keyValuePair = this.Current;
						object obj = keyValuePair.Key;
						keyValuePair = this.Current;
						return new DictionaryEntry(obj, keyValuePair.Value);
					}
					keyValuePair = this.Current;
					TKey key = keyValuePair.Key;
					keyValuePair = this.Current;
					return new KeyValuePair<TKey, TValue>(key, keyValuePair.Value);
				}
			}

			/// <summary>Gets the key of the element at the current position of the enumerator.</summary>
			/// <returns>The key of the element in the collection at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
			// Token: 0x17000F88 RID: 3976
			// (get) Token: 0x06004459 RID: 17497 RVA: 0x0011E6AC File Offset: 0x0011C8AC
			[global::__DynamicallyInvokable]
			object IDictionaryEnumerator.Key
			{
				[global::__DynamicallyInvokable]
				get
				{
					if (this.NotStartedOrEnded)
					{
						System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumOpCantHappen);
					}
					KeyValuePair<TKey, TValue> keyValuePair = this.Current;
					return keyValuePair.Key;
				}
			}

			/// <summary>Gets the value of the element at the current position of the enumerator.</summary>
			/// <returns>The value of the element in the collection at the current position of the enumerator.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
			// Token: 0x17000F89 RID: 3977
			// (get) Token: 0x0600445A RID: 17498 RVA: 0x0011E6DC File Offset: 0x0011C8DC
			[global::__DynamicallyInvokable]
			object IDictionaryEnumerator.Value
			{
				[global::__DynamicallyInvokable]
				get
				{
					if (this.NotStartedOrEnded)
					{
						System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumOpCantHappen);
					}
					KeyValuePair<TKey, TValue> keyValuePair = this.Current;
					return keyValuePair.Value;
				}
			}

			/// <summary>Gets the element at the current position of the enumerator as a <see cref="T:System.Collections.DictionaryEntry" /> structure.</summary>
			/// <returns>The element in the collection at the current position of the dictionary, as a <see cref="T:System.Collections.DictionaryEntry" /> structure.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
			// Token: 0x17000F8A RID: 3978
			// (get) Token: 0x0600445B RID: 17499 RVA: 0x0011E70C File Offset: 0x0011C90C
			[global::__DynamicallyInvokable]
			DictionaryEntry IDictionaryEnumerator.Entry
			{
				[global::__DynamicallyInvokable]
				get
				{
					if (this.NotStartedOrEnded)
					{
						System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumOpCantHappen);
					}
					KeyValuePair<TKey, TValue> keyValuePair = this.Current;
					object obj = keyValuePair.Key;
					keyValuePair = this.Current;
					return new DictionaryEntry(obj, keyValuePair.Value);
				}
			}

			// Token: 0x04003517 RID: 13591
			private SortedSet<KeyValuePair<TKey, TValue>>.Enumerator treeEnum;

			// Token: 0x04003518 RID: 13592
			private int getEnumeratorRetType;

			// Token: 0x04003519 RID: 13593
			internal const int KeyValuePair = 1;

			// Token: 0x0400351A RID: 13594
			internal const int DictEntry = 2;
		}

		/// <summary>Represents the collection of keys in a <see cref="T:System.Collections.Generic.SortedDictionary`2" />. This class cannot be inherited.</summary>
		/// <typeparam name="TKey" />
		/// <typeparam name="TValue" />
		// Token: 0x020007FA RID: 2042
		[DebuggerTypeProxy(typeof(System_DictionaryKeyCollectionDebugView<, >))]
		[DebuggerDisplay("Count = {Count}")]
		[global::__DynamicallyInvokable]
		[Serializable]
		public sealed class KeyCollection : ICollection<TKey>, IEnumerable<TKey>, IEnumerable, ICollection, IReadOnlyCollection<TKey>
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" /> class that reflects the keys in the specified <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
			/// <param name="dictionary">The <see cref="T:System.Collections.Generic.SortedDictionary`2" /> whose keys are reflected in the new <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="dictionary" /> is <see langword="null" />.</exception>
			// Token: 0x0600445C RID: 17500 RVA: 0x0011E753 File Offset: 0x0011C953
			[global::__DynamicallyInvokable]
			public KeyCollection(SortedDictionary<TKey, TValue> dictionary)
			{
				if (dictionary == null)
				{
					System.ThrowHelper.ThrowArgumentNullException(System.ExceptionArgument.dictionary);
				}
				this.dictionary = dictionary;
			}

			/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />.</summary>
			/// <returns>A <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection.Enumerator" /> structure for the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />.</returns>
			// Token: 0x0600445D RID: 17501 RVA: 0x0011E76B File Offset: 0x0011C96B
			[global::__DynamicallyInvokable]
			public SortedDictionary<TKey, TValue>.KeyCollection.Enumerator GetEnumerator()
			{
				return new SortedDictionary<TKey, TValue>.KeyCollection.Enumerator(this.dictionary);
			}

			// Token: 0x0600445E RID: 17502 RVA: 0x0011E778 File Offset: 0x0011C978
			[global::__DynamicallyInvokable]
			IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator()
			{
				return new SortedDictionary<TKey, TValue>.KeyCollection.Enumerator(this.dictionary);
			}

			/// <summary>Returns an enumerator that iterates through the collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
			// Token: 0x0600445F RID: 17503 RVA: 0x0011E78A File Offset: 0x0011C98A
			[global::__DynamicallyInvokable]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new SortedDictionary<TKey, TValue>.KeyCollection.Enumerator(this.dictionary);
			}

			/// <summary>Copies the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" /> elements to an existing one-dimensional array, starting at the specified array index.</summary>
			/// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />. The array must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is <see langword="null" />.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0.</exception>
			/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
			// Token: 0x06004460 RID: 17504 RVA: 0x0011E79C File Offset: 0x0011C99C
			[global::__DynamicallyInvokable]
			public void CopyTo(TKey[] array, int index)
			{
				if (array == null)
				{
					System.ThrowHelper.ThrowArgumentNullException(System.ExceptionArgument.array);
				}
				if (index < 0)
				{
					System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument.index);
				}
				if (array.Length - index < this.Count)
				{
					System.ThrowHelper.ThrowArgumentException(System.ExceptionResource.Arg_ArrayPlusOffTooSmall);
				}
				this.dictionary._set.InOrderTreeWalk(delegate(SortedSet<KeyValuePair<TKey, TValue>>.Node node)
				{
					TKey[] array2 = array;
					int index2 = index;
					index = index2 + 1;
					array2[index2] = node.Item.Key;
					return true;
				});
			}

			/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an array, starting at a particular array index.</summary>
			/// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.ICollection" />. The array must have zero-based indexing.</param>
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
			/// The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.  
			/// -or-  
			/// The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
			// Token: 0x06004461 RID: 17505 RVA: 0x0011E818 File Offset: 0x0011CA18
			[global::__DynamicallyInvokable]
			void ICollection.CopyTo(Array array, int index)
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
				if (index < 0)
				{
					System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument.arrayIndex, System.ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
				}
				if (array.Length - index < this.dictionary.Count)
				{
					System.ThrowHelper.ThrowArgumentException(System.ExceptionResource.Arg_ArrayPlusOffTooSmall);
				}
				TKey[] array2 = array as TKey[];
				if (array2 != null)
				{
					this.CopyTo(array2, index);
					return;
				}
				object[] objects = (object[])array;
				if (objects == null)
				{
					System.ThrowHelper.ThrowArgumentException(System.ExceptionResource.Argument_InvalidArrayType);
				}
				try
				{
					this.dictionary._set.InOrderTreeWalk(delegate(SortedSet<KeyValuePair<TKey, TValue>>.Node node)
					{
						object[] objects2 = objects;
						int index2 = index;
						index = index2 + 1;
						objects2[index2] = node.Item.Key;
						return true;
					});
				}
				catch (ArrayTypeMismatchException)
				{
					System.ThrowHelper.ThrowArgumentException(System.ExceptionResource.Argument_InvalidArrayType);
				}
			}

			/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />.</summary>
			/// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />.</returns>
			// Token: 0x17000F8B RID: 3979
			// (get) Token: 0x06004462 RID: 17506 RVA: 0x0011E8F8 File Offset: 0x0011CAF8
			[global::__DynamicallyInvokable]
			public int Count
			{
				[global::__DynamicallyInvokable]
				get
				{
					return this.dictionary.Count;
				}
			}

			// Token: 0x17000F8C RID: 3980
			// (get) Token: 0x06004463 RID: 17507 RVA: 0x0011E905 File Offset: 0x0011CB05
			[global::__DynamicallyInvokable]
			bool ICollection<TKey>.IsReadOnly
			{
				[global::__DynamicallyInvokable]
				get
				{
					return true;
				}
			}

			// Token: 0x06004464 RID: 17508 RVA: 0x0011E908 File Offset: 0x0011CB08
			[global::__DynamicallyInvokable]
			void ICollection<TKey>.Add(TKey item)
			{
				System.ThrowHelper.ThrowNotSupportedException(System.ExceptionResource.NotSupported_KeyCollectionSet);
			}

			// Token: 0x06004465 RID: 17509 RVA: 0x0011E911 File Offset: 0x0011CB11
			[global::__DynamicallyInvokable]
			void ICollection<TKey>.Clear()
			{
				System.ThrowHelper.ThrowNotSupportedException(System.ExceptionResource.NotSupported_KeyCollectionSet);
			}

			// Token: 0x06004466 RID: 17510 RVA: 0x0011E91A File Offset: 0x0011CB1A
			[global::__DynamicallyInvokable]
			bool ICollection<TKey>.Contains(TKey item)
			{
				return this.dictionary.ContainsKey(item);
			}

			// Token: 0x06004467 RID: 17511 RVA: 0x0011E928 File Offset: 0x0011CB28
			[global::__DynamicallyInvokable]
			bool ICollection<TKey>.Remove(TKey item)
			{
				System.ThrowHelper.ThrowNotSupportedException(System.ExceptionResource.NotSupported_KeyCollectionSet);
				return false;
			}

			/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
			/// <returns>
			///   <see langword="true" /> if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, <see langword="false" />.  In the default implementation of <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />, this property always returns <see langword="false" />.</returns>
			// Token: 0x17000F8D RID: 3981
			// (get) Token: 0x06004468 RID: 17512 RVA: 0x0011E932 File Offset: 0x0011CB32
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
			/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  In the default implementation of <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />, this property always returns the current instance.</returns>
			// Token: 0x17000F8E RID: 3982
			// (get) Token: 0x06004469 RID: 17513 RVA: 0x0011E935 File Offset: 0x0011CB35
			[global::__DynamicallyInvokable]
			object ICollection.SyncRoot
			{
				[global::__DynamicallyInvokable]
				get
				{
					return ((ICollection)this.dictionary).SyncRoot;
				}
			}

			// Token: 0x0400351B RID: 13595
			private SortedDictionary<TKey, TValue> dictionary;

			/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />.</summary>
			/// <typeparam name="TKey" />
			/// <typeparam name="TValue" />
			// Token: 0x02000926 RID: 2342
			[global::__DynamicallyInvokable]
			public struct Enumerator : IEnumerator<TKey>, IDisposable, IEnumerator
			{
				// Token: 0x06004667 RID: 18023 RVA: 0x00125C55 File Offset: 0x00123E55
				internal Enumerator(SortedDictionary<TKey, TValue> dictionary)
				{
					this.dictEnum = dictionary.GetEnumerator();
				}

				/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection.Enumerator" />.</summary>
				// Token: 0x06004668 RID: 18024 RVA: 0x00125C63 File Offset: 0x00123E63
				[global::__DynamicallyInvokable]
				public void Dispose()
				{
					this.dictEnum.Dispose();
				}

				/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" />.</summary>
				/// <returns>
				///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
				/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
				// Token: 0x06004669 RID: 18025 RVA: 0x00125C70 File Offset: 0x00123E70
				[global::__DynamicallyInvokable]
				public bool MoveNext()
				{
					return this.dictEnum.MoveNext();
				}

				/// <summary>Gets the element at the current position of the enumerator.</summary>
				/// <returns>The element in the <see cref="T:System.Collections.Generic.SortedDictionary`2.KeyCollection" /> at the current position of the enumerator.</returns>
				// Token: 0x17000FE2 RID: 4066
				// (get) Token: 0x0600466A RID: 18026 RVA: 0x00125C80 File Offset: 0x00123E80
				[global::__DynamicallyInvokable]
				public TKey Current
				{
					[global::__DynamicallyInvokable]
					get
					{
						KeyValuePair<TKey, TValue> keyValuePair = this.dictEnum.Current;
						return keyValuePair.Key;
					}
				}

				/// <summary>Gets the element at the current position of the enumerator.</summary>
				/// <returns>The element in the collection at the current position of the enumerator.</returns>
				/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
				// Token: 0x17000FE3 RID: 4067
				// (get) Token: 0x0600466B RID: 18027 RVA: 0x00125CA0 File Offset: 0x00123EA0
				[global::__DynamicallyInvokable]
				object IEnumerator.Current
				{
					[global::__DynamicallyInvokable]
					get
					{
						if (this.dictEnum.NotStartedOrEnded)
						{
							System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumOpCantHappen);
						}
						return this.Current;
					}
				}

				/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
				/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
				// Token: 0x0600466C RID: 18028 RVA: 0x00125CC1 File Offset: 0x00123EC1
				[global::__DynamicallyInvokable]
				void IEnumerator.Reset()
				{
					this.dictEnum.Reset();
				}

				// Token: 0x04003DA7 RID: 15783
				private SortedDictionary<TKey, TValue>.Enumerator dictEnum;
			}
		}

		/// <summary>Represents the collection of values in a <see cref="T:System.Collections.Generic.SortedDictionary`2" />. This class cannot be inherited</summary>
		/// <typeparam name="TKey" />
		/// <typeparam name="TValue" />
		// Token: 0x020007FB RID: 2043
		[DebuggerTypeProxy(typeof(System_DictionaryValueCollectionDebugView<, >))]
		[DebuggerDisplay("Count = {Count}")]
		[global::__DynamicallyInvokable]
		[Serializable]
		public sealed class ValueCollection : ICollection<TValue>, IEnumerable<TValue>, IEnumerable, ICollection, IReadOnlyCollection<TValue>
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" /> class that reflects the values in the specified <see cref="T:System.Collections.Generic.SortedDictionary`2" />.</summary>
			/// <param name="dictionary">The <see cref="T:System.Collections.Generic.SortedDictionary`2" /> whose values are reflected in the new <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="dictionary" /> is <see langword="null" />.</exception>
			// Token: 0x0600446A RID: 17514 RVA: 0x0011E942 File Offset: 0x0011CB42
			[global::__DynamicallyInvokable]
			public ValueCollection(SortedDictionary<TKey, TValue> dictionary)
			{
				if (dictionary == null)
				{
					System.ThrowHelper.ThrowArgumentNullException(System.ExceptionArgument.dictionary);
				}
				this.dictionary = dictionary;
			}

			/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />.</summary>
			/// <returns>A <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection.Enumerator" /> structure for the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />.</returns>
			// Token: 0x0600446B RID: 17515 RVA: 0x0011E95A File Offset: 0x0011CB5A
			[global::__DynamicallyInvokable]
			public SortedDictionary<TKey, TValue>.ValueCollection.Enumerator GetEnumerator()
			{
				return new SortedDictionary<TKey, TValue>.ValueCollection.Enumerator(this.dictionary);
			}

			// Token: 0x0600446C RID: 17516 RVA: 0x0011E967 File Offset: 0x0011CB67
			[global::__DynamicallyInvokable]
			IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
			{
				return new SortedDictionary<TKey, TValue>.ValueCollection.Enumerator(this.dictionary);
			}

			/// <summary>Returns an enumerator that iterates through the collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
			// Token: 0x0600446D RID: 17517 RVA: 0x0011E979 File Offset: 0x0011CB79
			[global::__DynamicallyInvokable]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new SortedDictionary<TKey, TValue>.ValueCollection.Enumerator(this.dictionary);
			}

			/// <summary>Copies the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" /> elements to an existing one-dimensional array, starting at the specified array index.</summary>
			/// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />. The array must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is <see langword="null" />.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0.</exception>
			/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
			// Token: 0x0600446E RID: 17518 RVA: 0x0011E98C File Offset: 0x0011CB8C
			[global::__DynamicallyInvokable]
			public void CopyTo(TValue[] array, int index)
			{
				if (array == null)
				{
					System.ThrowHelper.ThrowArgumentNullException(System.ExceptionArgument.array);
				}
				if (index < 0)
				{
					System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument.index);
				}
				if (array.Length - index < this.Count)
				{
					System.ThrowHelper.ThrowArgumentException(System.ExceptionResource.Arg_ArrayPlusOffTooSmall);
				}
				this.dictionary._set.InOrderTreeWalk(delegate(SortedSet<KeyValuePair<TKey, TValue>>.Node node)
				{
					TValue[] array2 = array;
					int index2 = index;
					index = index2 + 1;
					array2[index2] = node.Item.Value;
					return true;
				});
			}

			/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an array, starting at a particular array index.</summary>
			/// <param name="array">The one-dimensional array that is the destination of the elements copied from the <see cref="T:System.Collections.ICollection" />. The array must have zero-based indexing.</param>
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
			/// The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.  
			/// -or-  
			/// The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
			// Token: 0x0600446F RID: 17519 RVA: 0x0011EA08 File Offset: 0x0011CC08
			[global::__DynamicallyInvokable]
			void ICollection.CopyTo(Array array, int index)
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
				if (index < 0)
				{
					System.ThrowHelper.ThrowArgumentOutOfRangeException(System.ExceptionArgument.arrayIndex, System.ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
				}
				if (array.Length - index < this.dictionary.Count)
				{
					System.ThrowHelper.ThrowArgumentException(System.ExceptionResource.Arg_ArrayPlusOffTooSmall);
				}
				TValue[] array2 = array as TValue[];
				if (array2 != null)
				{
					this.CopyTo(array2, index);
					return;
				}
				object[] objects = (object[])array;
				if (objects == null)
				{
					System.ThrowHelper.ThrowArgumentException(System.ExceptionResource.Argument_InvalidArrayType);
				}
				try
				{
					this.dictionary._set.InOrderTreeWalk(delegate(SortedSet<KeyValuePair<TKey, TValue>>.Node node)
					{
						object[] objects2 = objects;
						int index2 = index;
						index = index2 + 1;
						objects2[index2] = node.Item.Value;
						return true;
					});
				}
				catch (ArrayTypeMismatchException)
				{
					System.ThrowHelper.ThrowArgumentException(System.ExceptionResource.Argument_InvalidArrayType);
				}
			}

			/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />.</summary>
			/// <returns>The number of elements contained in the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />.</returns>
			// Token: 0x17000F8F RID: 3983
			// (get) Token: 0x06004470 RID: 17520 RVA: 0x0011EAE8 File Offset: 0x0011CCE8
			[global::__DynamicallyInvokable]
			public int Count
			{
				[global::__DynamicallyInvokable]
				get
				{
					return this.dictionary.Count;
				}
			}

			// Token: 0x17000F90 RID: 3984
			// (get) Token: 0x06004471 RID: 17521 RVA: 0x0011EAF5 File Offset: 0x0011CCF5
			[global::__DynamicallyInvokable]
			bool ICollection<TValue>.IsReadOnly
			{
				[global::__DynamicallyInvokable]
				get
				{
					return true;
				}
			}

			// Token: 0x06004472 RID: 17522 RVA: 0x0011EAF8 File Offset: 0x0011CCF8
			[global::__DynamicallyInvokable]
			void ICollection<TValue>.Add(TValue item)
			{
				System.ThrowHelper.ThrowNotSupportedException(System.ExceptionResource.NotSupported_ValueCollectionSet);
			}

			// Token: 0x06004473 RID: 17523 RVA: 0x0011EB01 File Offset: 0x0011CD01
			[global::__DynamicallyInvokable]
			void ICollection<TValue>.Clear()
			{
				System.ThrowHelper.ThrowNotSupportedException(System.ExceptionResource.NotSupported_ValueCollectionSet);
			}

			// Token: 0x06004474 RID: 17524 RVA: 0x0011EB0A File Offset: 0x0011CD0A
			[global::__DynamicallyInvokable]
			bool ICollection<TValue>.Contains(TValue item)
			{
				return this.dictionary.ContainsValue(item);
			}

			// Token: 0x06004475 RID: 17525 RVA: 0x0011EB18 File Offset: 0x0011CD18
			[global::__DynamicallyInvokable]
			bool ICollection<TValue>.Remove(TValue item)
			{
				System.ThrowHelper.ThrowNotSupportedException(System.ExceptionResource.NotSupported_ValueCollectionSet);
				return false;
			}

			/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).</summary>
			/// <returns>
			///   <see langword="true" /> if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, <see langword="false" />.  In the default implementation of <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />, this property always returns <see langword="false" />.</returns>
			// Token: 0x17000F91 RID: 3985
			// (get) Token: 0x06004476 RID: 17526 RVA: 0x0011EB22 File Offset: 0x0011CD22
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
			/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.  In the default implementation of <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />, this property always returns the current instance.</returns>
			// Token: 0x17000F92 RID: 3986
			// (get) Token: 0x06004477 RID: 17527 RVA: 0x0011EB25 File Offset: 0x0011CD25
			[global::__DynamicallyInvokable]
			object ICollection.SyncRoot
			{
				[global::__DynamicallyInvokable]
				get
				{
					return ((ICollection)this.dictionary).SyncRoot;
				}
			}

			// Token: 0x0400351C RID: 13596
			private SortedDictionary<TKey, TValue> dictionary;

			/// <summary>Enumerates the elements of a <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />.</summary>
			/// <typeparam name="TKey" />
			/// <typeparam name="TValue" />
			// Token: 0x02000929 RID: 2345
			[global::__DynamicallyInvokable]
			public struct Enumerator : IEnumerator<TValue>, IDisposable, IEnumerator
			{
				// Token: 0x06004671 RID: 18033 RVA: 0x00125D4E File Offset: 0x00123F4E
				internal Enumerator(SortedDictionary<TKey, TValue> dictionary)
				{
					this.dictEnum = dictionary.GetEnumerator();
				}

				/// <summary>Releases all resources used by the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection.Enumerator" />.</summary>
				// Token: 0x06004672 RID: 18034 RVA: 0x00125D5C File Offset: 0x00123F5C
				[global::__DynamicallyInvokable]
				public void Dispose()
				{
					this.dictEnum.Dispose();
				}

				/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" />.</summary>
				/// <returns>
				///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.</returns>
				/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
				// Token: 0x06004673 RID: 18035 RVA: 0x00125D69 File Offset: 0x00123F69
				[global::__DynamicallyInvokable]
				public bool MoveNext()
				{
					return this.dictEnum.MoveNext();
				}

				/// <summary>Gets the element at the current position of the enumerator.</summary>
				/// <returns>The element in the <see cref="T:System.Collections.Generic.SortedDictionary`2.ValueCollection" /> at the current position of the enumerator.</returns>
				// Token: 0x17000FE4 RID: 4068
				// (get) Token: 0x06004674 RID: 18036 RVA: 0x00125D78 File Offset: 0x00123F78
				[global::__DynamicallyInvokable]
				public TValue Current
				{
					[global::__DynamicallyInvokable]
					get
					{
						KeyValuePair<TKey, TValue> keyValuePair = this.dictEnum.Current;
						return keyValuePair.Value;
					}
				}

				/// <summary>Gets the element at the current position of the enumerator.</summary>
				/// <returns>The element in the collection at the current position of the enumerator.</returns>
				/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
				// Token: 0x17000FE5 RID: 4069
				// (get) Token: 0x06004675 RID: 18037 RVA: 0x00125D98 File Offset: 0x00123F98
				[global::__DynamicallyInvokable]
				object IEnumerator.Current
				{
					[global::__DynamicallyInvokable]
					get
					{
						if (this.dictEnum.NotStartedOrEnded)
						{
							System.ThrowHelper.ThrowInvalidOperationException(System.ExceptionResource.InvalidOperation_EnumOpCantHappen);
						}
						return this.Current;
					}
				}

				/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
				/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
				// Token: 0x06004676 RID: 18038 RVA: 0x00125DB9 File Offset: 0x00123FB9
				[global::__DynamicallyInvokable]
				void IEnumerator.Reset()
				{
					this.dictEnum.Reset();
				}

				// Token: 0x04003DAC RID: 15788
				private SortedDictionary<TKey, TValue>.Enumerator dictEnum;
			}
		}

		// Token: 0x020007FC RID: 2044
		[Serializable]
		internal class KeyValuePairComparer : Comparer<KeyValuePair<TKey, TValue>>
		{
			// Token: 0x06004478 RID: 17528 RVA: 0x0011EB32 File Offset: 0x0011CD32
			public KeyValuePairComparer(IComparer<TKey> keyComparer)
			{
				if (keyComparer == null)
				{
					this.keyComparer = Comparer<TKey>.Default;
					return;
				}
				this.keyComparer = keyComparer;
			}

			// Token: 0x06004479 RID: 17529 RVA: 0x0011EB50 File Offset: 0x0011CD50
			public override int Compare(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
			{
				return this.keyComparer.Compare(x.Key, y.Key);
			}

			// Token: 0x0400351D RID: 13597
			internal IComparer<TKey> keyComparer;
		}
	}
}
