using System;

namespace System.Collections.Specialized
{
	/// <summary>Implements <see langword="IDictionary" /> by using a <see cref="T:System.Collections.Specialized.ListDictionary" /> while the collection is small, and then switching to a <see cref="T:System.Collections.Hashtable" /> when the collection gets large.</summary>
	// Token: 0x020003AA RID: 938
	[Serializable]
	public class HybridDictionary : IDictionary, ICollection, IEnumerable
	{
		/// <summary>Creates an empty case-sensitive <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		// Token: 0x060022EE RID: 8942 RVA: 0x000A60A8 File Offset: 0x000A42A8
		public HybridDictionary()
		{
		}

		/// <summary>Creates a case-sensitive <see cref="T:System.Collections.Specialized.HybridDictionary" /> with the specified initial size.</summary>
		/// <param name="initialSize">The approximate number of entries that the <see cref="T:System.Collections.Specialized.HybridDictionary" /> can initially contain.</param>
		// Token: 0x060022EF RID: 8943 RVA: 0x000A60B0 File Offset: 0x000A42B0
		public HybridDictionary(int initialSize)
			: this(initialSize, false)
		{
		}

		/// <summary>Creates an empty <see cref="T:System.Collections.Specialized.HybridDictionary" /> with the specified case sensitivity.</summary>
		/// <param name="caseInsensitive">A Boolean that denotes whether the <see cref="T:System.Collections.Specialized.HybridDictionary" /> is case-insensitive.</param>
		// Token: 0x060022F0 RID: 8944 RVA: 0x000A60BA File Offset: 0x000A42BA
		public HybridDictionary(bool caseInsensitive)
		{
			this.caseInsensitive = caseInsensitive;
		}

		/// <summary>Creates a <see cref="T:System.Collections.Specialized.HybridDictionary" /> with the specified initial size and case sensitivity.</summary>
		/// <param name="initialSize">The approximate number of entries that the <see cref="T:System.Collections.Specialized.HybridDictionary" /> can initially contain.</param>
		/// <param name="caseInsensitive">A Boolean that denotes whether the <see cref="T:System.Collections.Specialized.HybridDictionary" /> is case-insensitive.</param>
		// Token: 0x060022F1 RID: 8945 RVA: 0x000A60C9 File Offset: 0x000A42C9
		public HybridDictionary(int initialSize, bool caseInsensitive)
		{
			this.caseInsensitive = caseInsensitive;
			if (initialSize >= 6)
			{
				if (caseInsensitive)
				{
					this.hashtable = new Hashtable(initialSize, StringComparer.OrdinalIgnoreCase);
					return;
				}
				this.hashtable = new Hashtable(initialSize);
			}
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <param name="key">The key whose value to get or set.</param>
		/// <returns>The value associated with the specified key. If the specified key is not found, attempting to get it returns <see langword="null" />, and attempting to set it creates a new entry using the specified key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x170008D5 RID: 2261
		public object this[object key]
		{
			get
			{
				ListDictionary listDictionary = this.list;
				if (this.hashtable != null)
				{
					return this.hashtable[key];
				}
				if (listDictionary != null)
				{
					return listDictionary[key];
				}
				if (key == null)
				{
					throw new ArgumentNullException("key", SR.GetString("ArgumentNull_Key"));
				}
				return null;
			}
			set
			{
				if (this.hashtable != null)
				{
					this.hashtable[key] = value;
					return;
				}
				if (this.list == null)
				{
					this.list = new ListDictionary(this.caseInsensitive ? StringComparer.OrdinalIgnoreCase : null);
					this.list[key] = value;
					return;
				}
				if (this.list.Count >= 8)
				{
					this.ChangeOver();
					this.hashtable[key] = value;
					return;
				}
				this.list[key] = value;
			}
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x060022F4 RID: 8948 RVA: 0x000A61D3 File Offset: 0x000A43D3
		private ListDictionary List
		{
			get
			{
				if (this.list == null)
				{
					this.list = new ListDictionary(this.caseInsensitive ? StringComparer.OrdinalIgnoreCase : null);
				}
				return this.list;
			}
		}

		// Token: 0x060022F5 RID: 8949 RVA: 0x000A6200 File Offset: 0x000A4400
		private void ChangeOver()
		{
			IDictionaryEnumerator enumerator = this.list.GetEnumerator();
			Hashtable hashtable;
			if (this.caseInsensitive)
			{
				hashtable = new Hashtable(13, StringComparer.OrdinalIgnoreCase);
			}
			else
			{
				hashtable = new Hashtable(13);
			}
			while (enumerator.MoveNext())
			{
				hashtable.Add(enumerator.Key, enumerator.Value);
			}
			this.hashtable = hashtable;
			this.list = null;
		}

		/// <summary>Gets the number of key/value pairs contained in the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		/// <returns>The number of key/value pairs contained in the <see cref="T:System.Collections.Specialized.HybridDictionary" />.  
		///  Retrieving the value of this property is an O(1) operation.</returns>
		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x060022F6 RID: 8950 RVA: 0x000A6264 File Offset: 0x000A4464
		public int Count
		{
			get
			{
				ListDictionary listDictionary = this.list;
				if (this.hashtable != null)
				{
					return this.hashtable.Count;
				}
				if (listDictionary != null)
				{
					return listDictionary.Count;
				}
				return 0;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the keys in the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys in the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</returns>
		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x060022F7 RID: 8951 RVA: 0x000A6297 File Offset: 0x000A4497
		public ICollection Keys
		{
			get
			{
				if (this.hashtable != null)
				{
					return this.hashtable.Keys;
				}
				return this.List.Keys;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.HybridDictionary" /> is read-only.</summary>
		/// <returns>This property always returns <see langword="false" />.</returns>
		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x060022F8 RID: 8952 RVA: 0x000A62B8 File Offset: 0x000A44B8
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.HybridDictionary" /> has a fixed size.</summary>
		/// <returns>This property always returns <see langword="false" />.</returns>
		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x060022F9 RID: 8953 RVA: 0x000A62BB File Offset: 0x000A44BB
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.HybridDictionary" /> is synchronized (thread safe).</summary>
		/// <returns>This property always returns <see langword="false" />.</returns>
		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x060022FA RID: 8954 RVA: 0x000A62BE File Offset: 0x000A44BE
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</returns>
		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x060022FB RID: 8955 RVA: 0x000A62C1 File Offset: 0x000A44C1
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</returns>
		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x060022FC RID: 8956 RVA: 0x000A62C4 File Offset: 0x000A44C4
		public ICollection Values
		{
			get
			{
				if (this.hashtable != null)
				{
					return this.hashtable.Values;
				}
				return this.List.Values;
			}
		}

		/// <summary>Adds an entry with the specified key and value into the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		/// <param name="key">The key of the entry to add.</param>
		/// <param name="value">The value of the entry to add. The value can be <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">An entry with the same key already exists in the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</exception>
		// Token: 0x060022FD RID: 8957 RVA: 0x000A62E8 File Offset: 0x000A44E8
		public void Add(object key, object value)
		{
			if (this.hashtable != null)
			{
				this.hashtable.Add(key, value);
				return;
			}
			if (this.list == null)
			{
				this.list = new ListDictionary(this.caseInsensitive ? StringComparer.OrdinalIgnoreCase : null);
				this.list.Add(key, value);
				return;
			}
			if (this.list.Count + 1 >= 9)
			{
				this.ChangeOver();
				this.hashtable.Add(key, value);
				return;
			}
			this.list.Add(key, value);
		}

		/// <summary>Removes all entries from the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		// Token: 0x060022FE RID: 8958 RVA: 0x000A6370 File Offset: 0x000A4570
		public void Clear()
		{
			if (this.hashtable != null)
			{
				Hashtable hashtable = this.hashtable;
				this.hashtable = null;
				hashtable.Clear();
			}
			if (this.list != null)
			{
				ListDictionary listDictionary = this.list;
				this.list = null;
				listDictionary.Clear();
			}
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.Specialized.HybridDictionary" /> contains a specific key.</summary>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Specialized.HybridDictionary" /> contains an entry with the specified key; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x060022FF RID: 8959 RVA: 0x000A63B8 File Offset: 0x000A45B8
		public bool Contains(object key)
		{
			ListDictionary listDictionary = this.list;
			if (this.hashtable != null)
			{
				return this.hashtable.Contains(key);
			}
			if (listDictionary != null)
			{
				return listDictionary.Contains(key);
			}
			if (key == null)
			{
				throw new ArgumentNullException("key", SR.GetString("ArgumentNull_Key"));
			}
			return false;
		}

		/// <summary>Copies the <see cref="T:System.Collections.Specialized.HybridDictionary" /> entries to a one-dimensional <see cref="T:System.Array" /> instance at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the <see cref="T:System.Collections.DictionaryEntry" /> objects copied from <see cref="T:System.Collections.Specialized.HybridDictionary" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the source <see cref="T:System.Collections.Specialized.HybridDictionary" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.Specialized.HybridDictionary" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x06002300 RID: 8960 RVA: 0x000A6405 File Offset: 0x000A4605
		public void CopyTo(Array array, int index)
		{
			if (this.hashtable != null)
			{
				this.hashtable.CopyTo(array, index);
				return;
			}
			this.List.CopyTo(array, index);
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> that iterates through the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</returns>
		// Token: 0x06002301 RID: 8961 RVA: 0x000A642C File Offset: 0x000A462C
		public IDictionaryEnumerator GetEnumerator()
		{
			if (this.hashtable != null)
			{
				return this.hashtable.GetEnumerator();
			}
			if (this.list == null)
			{
				this.list = new ListDictionary(this.caseInsensitive ? StringComparer.OrdinalIgnoreCase : null);
			}
			return this.list.GetEnumerator();
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that iterates through the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</returns>
		// Token: 0x06002302 RID: 8962 RVA: 0x000A647C File Offset: 0x000A467C
		IEnumerator IEnumerable.GetEnumerator()
		{
			if (this.hashtable != null)
			{
				return this.hashtable.GetEnumerator();
			}
			if (this.list == null)
			{
				this.list = new ListDictionary(this.caseInsensitive ? StringComparer.OrdinalIgnoreCase : null);
			}
			return this.list.GetEnumerator();
		}

		/// <summary>Removes the entry with the specified key from the <see cref="T:System.Collections.Specialized.HybridDictionary" />.</summary>
		/// <param name="key">The key of the entry to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x06002303 RID: 8963 RVA: 0x000A64CC File Offset: 0x000A46CC
		public void Remove(object key)
		{
			if (this.hashtable != null)
			{
				this.hashtable.Remove(key);
				return;
			}
			if (this.list != null)
			{
				this.list.Remove(key);
				return;
			}
			if (key == null)
			{
				throw new ArgumentNullException("key", SR.GetString("ArgumentNull_Key"));
			}
		}

		// Token: 0x04001FA1 RID: 8097
		private const int CutoverPoint = 9;

		// Token: 0x04001FA2 RID: 8098
		private const int InitialHashtableSize = 13;

		// Token: 0x04001FA3 RID: 8099
		private const int FixedSizeCutoverPoint = 6;

		// Token: 0x04001FA4 RID: 8100
		private ListDictionary list;

		// Token: 0x04001FA5 RID: 8101
		private Hashtable hashtable;

		// Token: 0x04001FA6 RID: 8102
		private bool caseInsensitive;
	}
}
