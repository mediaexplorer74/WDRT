using System;
using System.Runtime.InteropServices;

namespace System.Collections
{
	/// <summary>Provides the <see langword="abstract" /> base class for a strongly typed collection of key/value pairs.</summary>
	// Token: 0x0200048B RID: 1163
	[ComVisible(true)]
	[Serializable]
	public abstract class DictionaryBase : IDictionary, ICollection, IEnumerable
	{
		/// <summary>Gets the list of elements contained in the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <returns>A <see cref="T:System.Collections.Hashtable" /> representing the <see cref="T:System.Collections.DictionaryBase" /> instance itself.</returns>
		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x060037AE RID: 14254 RVA: 0x000D71C5 File Offset: 0x000D53C5
		protected Hashtable InnerHashtable
		{
			get
			{
				if (this.hashtable == null)
				{
					this.hashtable = new Hashtable();
				}
				return this.hashtable;
			}
		}

		/// <summary>Gets the list of elements contained in the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> representing the <see cref="T:System.Collections.DictionaryBase" /> instance itself.</returns>
		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x060037AF RID: 14255 RVA: 0x000D71E0 File Offset: 0x000D53E0
		protected IDictionary Dictionary
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.DictionaryBase" /> instance.</returns>
		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x060037B0 RID: 14256 RVA: 0x000D71E3 File Offset: 0x000D53E3
		public int Count
		{
			get
			{
				if (this.hashtable != null)
				{
					return this.hashtable.Count;
				}
				return 0;
			}
		}

		/// <summary>Gets a value indicating whether a <see cref="T:System.Collections.DictionaryBase" /> object is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.DictionaryBase" /> object is read-only; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x060037B1 RID: 14257 RVA: 0x000D71FA File Offset: 0x000D53FA
		bool IDictionary.IsReadOnly
		{
			get
			{
				return this.InnerHashtable.IsReadOnly;
			}
		}

		/// <summary>Gets a value indicating whether a <see cref="T:System.Collections.DictionaryBase" /> object has a fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.DictionaryBase" /> object has a fixed size; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x060037B2 RID: 14258 RVA: 0x000D7207 File Offset: 0x000D5407
		bool IDictionary.IsFixedSize
		{
			get
			{
				return this.InnerHashtable.IsFixedSize;
			}
		}

		/// <summary>Gets a value indicating whether access to a <see cref="T:System.Collections.DictionaryBase" /> object is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Collections.DictionaryBase" /> object is synchronized (thread safe); otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x060037B3 RID: 14259 RVA: 0x000D7214 File Offset: 0x000D5414
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.InnerHashtable.IsSynchronized;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> object containing the keys in the <see cref="T:System.Collections.DictionaryBase" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing the keys in the <see cref="T:System.Collections.DictionaryBase" /> object.</returns>
		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x060037B4 RID: 14260 RVA: 0x000D7221 File Offset: 0x000D5421
		ICollection IDictionary.Keys
		{
			get
			{
				return this.InnerHashtable.Keys;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to a <see cref="T:System.Collections.DictionaryBase" /> object.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.DictionaryBase" /> object.</returns>
		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x060037B5 RID: 14261 RVA: 0x000D722E File Offset: 0x000D542E
		object ICollection.SyncRoot
		{
			get
			{
				return this.InnerHashtable.SyncRoot;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> object containing the values in the <see cref="T:System.Collections.DictionaryBase" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> object containing the values in the <see cref="T:System.Collections.DictionaryBase" /> object.</returns>
		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x060037B6 RID: 14262 RVA: 0x000D723B File Offset: 0x000D543B
		ICollection IDictionary.Values
		{
			get
			{
				return this.InnerHashtable.Values;
			}
		}

		/// <summary>Copies the <see cref="T:System.Collections.DictionaryBase" /> elements to a one-dimensional <see cref="T:System.Array" /> at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the <see cref="T:System.Collections.DictionaryEntry" /> objects copied from the <see cref="T:System.Collections.DictionaryBase" /> instance. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the source <see cref="T:System.Collections.DictionaryBase" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.DictionaryBase" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x060037B7 RID: 14263 RVA: 0x000D7248 File Offset: 0x000D5448
		public void CopyTo(Array array, int index)
		{
			this.InnerHashtable.CopyTo(array, index);
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <param name="key">The key whose value to get or set.</param>
		/// <returns>The value associated with the specified key. If the specified key is not found, attempting to get it returns <see langword="null" />, and attempting to set it creates a new element using the specified key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The property is set and the <see cref="T:System.Collections.DictionaryBase" /> is read-only.  
		///  -or-  
		///  The property is set, <paramref name="key" /> does not exist in the collection, and the <see cref="T:System.Collections.DictionaryBase" /> has a fixed size.</exception>
		// Token: 0x17000835 RID: 2101
		object IDictionary.this[object key]
		{
			get
			{
				object obj = this.InnerHashtable[key];
				this.OnGet(key, obj);
				return obj;
			}
			set
			{
				this.OnValidate(key, value);
				bool flag = true;
				object obj = this.InnerHashtable[key];
				if (obj == null)
				{
					flag = this.InnerHashtable.Contains(key);
				}
				this.OnSet(key, obj, value);
				this.InnerHashtable[key] = value;
				try
				{
					this.OnSetComplete(key, obj, value);
				}
				catch
				{
					if (flag)
					{
						this.InnerHashtable[key] = obj;
					}
					else
					{
						this.InnerHashtable.Remove(key);
					}
					throw;
				}
			}
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.DictionaryBase" /> contains a specific key.</summary>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.DictionaryBase" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.DictionaryBase" /> contains an element with the specified key; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x060037BA RID: 14266 RVA: 0x000D7304 File Offset: 0x000D5504
		bool IDictionary.Contains(object key)
		{
			return this.InnerHashtable.Contains(key);
		}

		/// <summary>Adds an element with the specified key and value into the <see cref="T:System.Collections.DictionaryBase" />.</summary>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="value">The value of the element to add.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.DictionaryBase" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.DictionaryBase" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.DictionaryBase" /> has a fixed size.</exception>
		// Token: 0x060037BB RID: 14267 RVA: 0x000D7314 File Offset: 0x000D5514
		void IDictionary.Add(object key, object value)
		{
			this.OnValidate(key, value);
			this.OnInsert(key, value);
			this.InnerHashtable.Add(key, value);
			try
			{
				this.OnInsertComplete(key, value);
			}
			catch
			{
				this.InnerHashtable.Remove(key);
				throw;
			}
		}

		/// <summary>Clears the contents of the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		// Token: 0x060037BC RID: 14268 RVA: 0x000D7368 File Offset: 0x000D5568
		public void Clear()
		{
			this.OnClear();
			this.InnerHashtable.Clear();
			this.OnClearComplete();
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.DictionaryBase" />.</summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.DictionaryBase" /> is read-only.  
		///  -or-  
		///  The <see cref="T:System.Collections.DictionaryBase" /> has a fixed size.</exception>
		// Token: 0x060037BD RID: 14269 RVA: 0x000D7384 File Offset: 0x000D5584
		void IDictionary.Remove(object key)
		{
			if (this.InnerHashtable.Contains(key))
			{
				object obj = this.InnerHashtable[key];
				this.OnValidate(key, obj);
				this.OnRemove(key, obj);
				this.InnerHashtable.Remove(key);
				try
				{
					this.OnRemoveComplete(key, obj);
				}
				catch
				{
					this.InnerHashtable.Add(key, obj);
					throw;
				}
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.IDictionaryEnumerator" /> that iterates through the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the <see cref="T:System.Collections.DictionaryBase" /> instance.</returns>
		// Token: 0x060037BE RID: 14270 RVA: 0x000D73F4 File Offset: 0x000D55F4
		public IDictionaryEnumerator GetEnumerator()
		{
			return this.InnerHashtable.GetEnumerator();
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that iterates through the <see cref="T:System.Collections.DictionaryBase" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.DictionaryBase" />.</returns>
		// Token: 0x060037BF RID: 14271 RVA: 0x000D7401 File Offset: 0x000D5601
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.InnerHashtable.GetEnumerator();
		}

		/// <summary>Gets the element with the specified key and value in the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <param name="key">The key of the element to get.</param>
		/// <param name="currentValue">The current value of the element associated with <paramref name="key" />.</param>
		/// <returns>An <see cref="T:System.Object" /> containing the element with the specified key and value.</returns>
		// Token: 0x060037C0 RID: 14272 RVA: 0x000D740E File Offset: 0x000D560E
		protected virtual object OnGet(object key, object currentValue)
		{
			return currentValue;
		}

		/// <summary>Performs additional custom processes before setting a value in the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <param name="key">The key of the element to locate.</param>
		/// <param name="oldValue">The old value of the element associated with <paramref name="key" />.</param>
		/// <param name="newValue">The new value of the element associated with <paramref name="key" />.</param>
		// Token: 0x060037C1 RID: 14273 RVA: 0x000D7411 File Offset: 0x000D5611
		protected virtual void OnSet(object key, object oldValue, object newValue)
		{
		}

		/// <summary>Performs additional custom processes before inserting a new element into the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <param name="key">The key of the element to insert.</param>
		/// <param name="value">The value of the element to insert.</param>
		// Token: 0x060037C2 RID: 14274 RVA: 0x000D7413 File Offset: 0x000D5613
		protected virtual void OnInsert(object key, object value)
		{
		}

		/// <summary>Performs additional custom processes before clearing the contents of the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		// Token: 0x060037C3 RID: 14275 RVA: 0x000D7415 File Offset: 0x000D5615
		protected virtual void OnClear()
		{
		}

		/// <summary>Performs additional custom processes before removing an element from the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <param name="value">The value of the element to remove.</param>
		// Token: 0x060037C4 RID: 14276 RVA: 0x000D7417 File Offset: 0x000D5617
		protected virtual void OnRemove(object key, object value)
		{
		}

		/// <summary>Performs additional custom processes when validating the element with the specified key and value.</summary>
		/// <param name="key">The key of the element to validate.</param>
		/// <param name="value">The value of the element to validate.</param>
		// Token: 0x060037C5 RID: 14277 RVA: 0x000D7419 File Offset: 0x000D5619
		protected virtual void OnValidate(object key, object value)
		{
		}

		/// <summary>Performs additional custom processes after setting a value in the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <param name="key">The key of the element to locate.</param>
		/// <param name="oldValue">The old value of the element associated with <paramref name="key" />.</param>
		/// <param name="newValue">The new value of the element associated with <paramref name="key" />.</param>
		// Token: 0x060037C6 RID: 14278 RVA: 0x000D741B File Offset: 0x000D561B
		protected virtual void OnSetComplete(object key, object oldValue, object newValue)
		{
		}

		/// <summary>Performs additional custom processes after inserting a new element into the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <param name="key">The key of the element to insert.</param>
		/// <param name="value">The value of the element to insert.</param>
		// Token: 0x060037C7 RID: 14279 RVA: 0x000D741D File Offset: 0x000D561D
		protected virtual void OnInsertComplete(object key, object value)
		{
		}

		/// <summary>Performs additional custom processes after clearing the contents of the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		// Token: 0x060037C8 RID: 14280 RVA: 0x000D741F File Offset: 0x000D561F
		protected virtual void OnClearComplete()
		{
		}

		/// <summary>Performs additional custom processes after removing an element from the <see cref="T:System.Collections.DictionaryBase" /> instance.</summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <param name="value">The value of the element to remove.</param>
		// Token: 0x060037C9 RID: 14281 RVA: 0x000D7421 File Offset: 0x000D5621
		protected virtual void OnRemoveComplete(object key, object value)
		{
		}

		// Token: 0x040018C1 RID: 6337
		private Hashtable hashtable;
	}
}
