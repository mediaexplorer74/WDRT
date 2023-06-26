using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;

namespace System.Collections.Specialized
{
	/// <summary>Represents a collection of associated <see cref="T:System.String" /> keys and <see cref="T:System.String" /> values that can be accessed either with the key or with the index.</summary>
	// Token: 0x020003B0 RID: 944
	[global::__DynamicallyInvokable]
	[Serializable]
	public class NameValueCollection : NameObjectCollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameValueCollection" /> class that is empty, has the default initial capacity and uses the default case-insensitive hash code provider and the default case-insensitive comparer.</summary>
		// Token: 0x06002349 RID: 9033 RVA: 0x000A74E9 File Offset: 0x000A56E9
		public NameValueCollection()
		{
		}

		/// <summary>Copies the entries from the specified <see cref="T:System.Collections.Specialized.NameValueCollection" /> to a new <see cref="T:System.Collections.Specialized.NameValueCollection" /> with the same initial capacity as the number of entries copied and using the same hash code provider and the same comparer as the source collection.</summary>
		/// <param name="col">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to copy to the new <see cref="T:System.Collections.Specialized.NameValueCollection" /> instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="col" /> is <see langword="null" />.</exception>
		// Token: 0x0600234A RID: 9034 RVA: 0x000A74F1 File Offset: 0x000A56F1
		public NameValueCollection(NameValueCollection col)
			: base((col != null) ? col.Comparer : null)
		{
			this.Add(col);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameValueCollection" /> class that is empty, has the default initial capacity and uses the specified hash code provider and the specified comparer.</summary>
		/// <param name="hashProvider">The <see cref="T:System.Collections.IHashCodeProvider" /> that will supply the hash codes for all keys in the <see cref="T:System.Collections.Specialized.NameValueCollection" />.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> to use to determine whether two keys are equal.</param>
		// Token: 0x0600234B RID: 9035 RVA: 0x000A750C File Offset: 0x000A570C
		[Obsolete("Please use NameValueCollection(IEqualityComparer) instead.")]
		public NameValueCollection(IHashCodeProvider hashProvider, IComparer comparer)
			: base(hashProvider, comparer)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameValueCollection" /> class that is empty, has the specified initial capacity and uses the default case-insensitive hash code provider and the default case-insensitive comparer.</summary>
		/// <param name="capacity">The initial number of entries that the <see cref="T:System.Collections.Specialized.NameValueCollection" /> can contain.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x0600234C RID: 9036 RVA: 0x000A7516 File Offset: 0x000A5716
		public NameValueCollection(int capacity)
			: base(capacity)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameValueCollection" /> class that is empty, has the default initial capacity, and uses the specified <see cref="T:System.Collections.IEqualityComparer" /> object.</summary>
		/// <param name="equalityComparer">The <see cref="T:System.Collections.IEqualityComparer" /> object to use to determine whether two keys are equal and to generate hash codes for the keys in the collection.</param>
		// Token: 0x0600234D RID: 9037 RVA: 0x000A751F File Offset: 0x000A571F
		public NameValueCollection(IEqualityComparer equalityComparer)
			: base(equalityComparer)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameValueCollection" /> class that is empty, has the specified initial capacity, and uses the specified <see cref="T:System.Collections.IEqualityComparer" /> object.</summary>
		/// <param name="capacity">The initial number of entries that the <see cref="T:System.Collections.Specialized.NameValueCollection" /> object can contain.</param>
		/// <param name="equalityComparer">The <see cref="T:System.Collections.IEqualityComparer" /> object to use to determine whether two keys are equal and to generate hash codes for the keys in the collection.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x0600234E RID: 9038 RVA: 0x000A7528 File Offset: 0x000A5728
		public NameValueCollection(int capacity, IEqualityComparer equalityComparer)
			: base(capacity, equalityComparer)
		{
		}

		/// <summary>Copies the entries from the specified <see cref="T:System.Collections.Specialized.NameValueCollection" /> to a new <see cref="T:System.Collections.Specialized.NameValueCollection" /> with the specified initial capacity or the same initial capacity as the number of entries copied, whichever is greater, and using the default case-insensitive hash code provider and the default case-insensitive comparer.</summary>
		/// <param name="capacity">The initial number of entries that the <see cref="T:System.Collections.Specialized.NameValueCollection" /> can contain.</param>
		/// <param name="col">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to copy to the new <see cref="T:System.Collections.Specialized.NameValueCollection" /> instance.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="col" /> is <see langword="null" />.</exception>
		// Token: 0x0600234F RID: 9039 RVA: 0x000A7532 File Offset: 0x000A5732
		public NameValueCollection(int capacity, NameValueCollection col)
			: base(capacity, (col != null) ? col.Comparer : null)
		{
			if (col == null)
			{
				throw new ArgumentNullException("col");
			}
			base.Comparer = col.Comparer;
			this.Add(col);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameValueCollection" /> class that is empty, has the specified initial capacity and uses the specified hash code provider and the specified comparer.</summary>
		/// <param name="capacity">The initial number of entries that the <see cref="T:System.Collections.Specialized.NameValueCollection" /> can contain.</param>
		/// <param name="hashProvider">The <see cref="T:System.Collections.IHashCodeProvider" /> that will supply the hash codes for all keys in the <see cref="T:System.Collections.Specialized.NameValueCollection" />.</param>
		/// <param name="comparer">The <see cref="T:System.Collections.IComparer" /> to use to determine whether two keys are equal.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="capacity" /> is less than zero.</exception>
		// Token: 0x06002350 RID: 9040 RVA: 0x000A7568 File Offset: 0x000A5768
		[Obsolete("Please use NameValueCollection(Int32, IEqualityComparer) instead.")]
		public NameValueCollection(int capacity, IHashCodeProvider hashProvider, IComparer comparer)
			: base(capacity, hashProvider, comparer)
		{
		}

		// Token: 0x06002351 RID: 9041 RVA: 0x000A7573 File Offset: 0x000A5773
		internal NameValueCollection(DBNull dummy)
			: base(dummy)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Specialized.NameValueCollection" /> class that is serializable and uses the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" />.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the new <see cref="T:System.Collections.Specialized.NameValueCollection" /> instance.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source and destination of the serialized stream associated with the new <see cref="T:System.Collections.Specialized.NameValueCollection" /> instance.</param>
		// Token: 0x06002352 RID: 9042 RVA: 0x000A757C File Offset: 0x000A577C
		protected NameValueCollection(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Resets the cached arrays of the collection to <see langword="null" />.</summary>
		// Token: 0x06002353 RID: 9043 RVA: 0x000A7586 File Offset: 0x000A5786
		protected void InvalidateCachedArrays()
		{
			this._all = null;
			this._allKeys = null;
		}

		// Token: 0x06002354 RID: 9044 RVA: 0x000A7598 File Offset: 0x000A5798
		private static string GetAsOneString(ArrayList list)
		{
			int num = ((list != null) ? list.Count : 0);
			if (num == 1)
			{
				return (string)list[0];
			}
			if (num > 1)
			{
				StringBuilder stringBuilder = new StringBuilder((string)list[0]);
				for (int i = 1; i < num; i++)
				{
					stringBuilder.Append(',');
					stringBuilder.Append((string)list[i]);
				}
				return stringBuilder.ToString();
			}
			return null;
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x000A760C File Offset: 0x000A580C
		private static string[] GetAsStringArray(ArrayList list)
		{
			int num = ((list != null) ? list.Count : 0);
			if (num == 0)
			{
				return null;
			}
			string[] array = new string[num];
			list.CopyTo(0, array, 0, num);
			return array;
		}

		/// <summary>Copies the entries in the specified <see cref="T:System.Collections.Specialized.NameValueCollection" /> to the current <see cref="T:System.Collections.Specialized.NameValueCollection" />.</summary>
		/// <param name="c">The <see cref="T:System.Collections.Specialized.NameValueCollection" /> to copy to the current <see cref="T:System.Collections.Specialized.NameValueCollection" />.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="c" /> is <see langword="null" />.</exception>
		// Token: 0x06002356 RID: 9046 RVA: 0x000A7640 File Offset: 0x000A5840
		public void Add(NameValueCollection c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c");
			}
			this.InvalidateCachedArrays();
			int count = c.Count;
			for (int i = 0; i < count; i++)
			{
				string key = c.GetKey(i);
				string[] values = c.GetValues(i);
				if (values != null)
				{
					for (int j = 0; j < values.Length; j++)
					{
						this.Add(key, values[j]);
					}
				}
				else
				{
					this.Add(key, null);
				}
			}
		}

		/// <summary>Invalidates the cached arrays and removes all entries from the <see cref="T:System.Collections.Specialized.NameValueCollection" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06002357 RID: 9047 RVA: 0x000A76AE File Offset: 0x000A58AE
		public virtual void Clear()
		{
			if (base.IsReadOnly)
			{
				throw new NotSupportedException(SR.GetString("CollectionReadOnly"));
			}
			this.InvalidateCachedArrays();
			base.BaseClear();
		}

		/// <summary>Copies the entire <see cref="T:System.Collections.Specialized.NameValueCollection" /> to a compatible one-dimensional <see cref="T:System.Array" />, starting at the specified index of the target array.</summary>
		/// <param name="dest">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Specialized.NameValueCollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="dest" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dest" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dest" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the source <see cref="T:System.Collections.Specialized.NameValueCollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="dest" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The type of the source <see cref="T:System.Collections.Specialized.NameValueCollection" /> cannot be cast automatically to the type of the destination <paramref name="dest" />.</exception>
		// Token: 0x06002358 RID: 9048 RVA: 0x000A76D4 File Offset: 0x000A58D4
		public void CopyTo(Array dest, int index)
		{
			if (dest == null)
			{
				throw new ArgumentNullException("dest");
			}
			if (dest.Rank != 1)
			{
				throw new ArgumentException(SR.GetString("Arg_MultiRank"));
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("IndexOutOfRange", new object[] { index.ToString(CultureInfo.CurrentCulture) }));
			}
			if (dest.Length - index < this.Count)
			{
				throw new ArgumentException(SR.GetString("Arg_InsufficientSpace"));
			}
			int count = this.Count;
			if (this._all == null)
			{
				string[] array = new string[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = this.Get(i);
					dest.SetValue(array[i], i + index);
				}
				this._all = array;
				return;
			}
			for (int j = 0; j < count; j++)
			{
				dest.SetValue(this._all[j], j + index);
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.Specialized.NameValueCollection" /> contains keys that are not <see langword="null" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Specialized.NameValueCollection" /> contains keys that are not <see langword="null" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002359 RID: 9049 RVA: 0x000A77B3 File Offset: 0x000A59B3
		public bool HasKeys()
		{
			return this.InternalHasKeys();
		}

		// Token: 0x0600235A RID: 9050 RVA: 0x000A77BB File Offset: 0x000A59BB
		internal virtual bool InternalHasKeys()
		{
			return base.BaseHasKeys();
		}

		/// <summary>Adds an entry with the specified name and value to the <see cref="T:System.Collections.Specialized.NameValueCollection" />.</summary>
		/// <param name="name">The <see cref="T:System.String" /> key of the entry to add. The key can be <see langword="null" />.</param>
		/// <param name="value">The <see cref="T:System.String" /> value of the entry to add. The value can be <see langword="null" />.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x0600235B RID: 9051 RVA: 0x000A77C4 File Offset: 0x000A59C4
		public virtual void Add(string name, string value)
		{
			if (base.IsReadOnly)
			{
				throw new NotSupportedException(SR.GetString("CollectionReadOnly"));
			}
			this.InvalidateCachedArrays();
			ArrayList arrayList = (ArrayList)base.BaseGet(name);
			if (arrayList == null)
			{
				arrayList = new ArrayList(1);
				if (value != null)
				{
					arrayList.Add(value);
				}
				base.BaseAdd(name, arrayList);
				return;
			}
			if (value != null)
			{
				arrayList.Add(value);
			}
		}

		/// <summary>Gets the values associated with the specified key from the <see cref="T:System.Collections.Specialized.NameValueCollection" /> combined into one comma-separated list.</summary>
		/// <param name="name">The <see cref="T:System.String" /> key of the entry that contains the values to get. The key can be <see langword="null" />.</param>
		/// <returns>A <see cref="T:System.String" /> that contains a comma-separated list of the values associated with the specified key from the <see cref="T:System.Collections.Specialized.NameValueCollection" />, if found; otherwise, <see langword="null" />.</returns>
		// Token: 0x0600235C RID: 9052 RVA: 0x000A7828 File Offset: 0x000A5A28
		public virtual string Get(string name)
		{
			ArrayList arrayList = (ArrayList)base.BaseGet(name);
			return NameValueCollection.GetAsOneString(arrayList);
		}

		/// <summary>Gets the values associated with the specified key from the <see cref="T:System.Collections.Specialized.NameValueCollection" />.</summary>
		/// <param name="name">The <see cref="T:System.String" /> key of the entry that contains the values to get. The key can be <see langword="null" />.</param>
		/// <returns>A <see cref="T:System.String" /> array that contains the values associated with the specified key from the <see cref="T:System.Collections.Specialized.NameValueCollection" />, if found; otherwise, <see langword="null" />.</returns>
		// Token: 0x0600235D RID: 9053 RVA: 0x000A7848 File Offset: 0x000A5A48
		public virtual string[] GetValues(string name)
		{
			ArrayList arrayList = (ArrayList)base.BaseGet(name);
			return NameValueCollection.GetAsStringArray(arrayList);
		}

		/// <summary>Sets the value of an entry in the <see cref="T:System.Collections.Specialized.NameValueCollection" />.</summary>
		/// <param name="name">The <see cref="T:System.String" /> key of the entry to add the new value to. The key can be <see langword="null" />.</param>
		/// <param name="value">The <see cref="T:System.Object" /> that represents the new value to add to the specified entry. The value can be <see langword="null" />.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x0600235E RID: 9054 RVA: 0x000A7868 File Offset: 0x000A5A68
		public virtual void Set(string name, string value)
		{
			if (base.IsReadOnly)
			{
				throw new NotSupportedException(SR.GetString("CollectionReadOnly"));
			}
			this.InvalidateCachedArrays();
			base.BaseSet(name, new ArrayList(1) { value });
		}

		/// <summary>Removes the entries with the specified key from the <see cref="T:System.Collections.Specialized.NameObjectCollectionBase" /> instance.</summary>
		/// <param name="name">The <see cref="T:System.String" /> key of the entry to remove. The key can be <see langword="null" />.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x0600235F RID: 9055 RVA: 0x000A78AA File Offset: 0x000A5AAA
		public virtual void Remove(string name)
		{
			this.InvalidateCachedArrays();
			base.BaseRemove(name);
		}

		/// <summary>Gets or sets the entry with the specified key in the <see cref="T:System.Collections.Specialized.NameValueCollection" />.</summary>
		/// <param name="name">The <see cref="T:System.String" /> key of the entry to locate. The key can be <see langword="null" />.</param>
		/// <returns>A <see cref="T:System.String" /> that contains the comma-separated list of values associated with the specified key, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only and the operation attempts to modify the collection.</exception>
		// Token: 0x170008F1 RID: 2289
		public string this[string name]
		{
			[global::__DynamicallyInvokable]
			get
			{
				return this.Get(name);
			}
			[global::__DynamicallyInvokable]
			set
			{
				this.Set(name, value);
			}
		}

		/// <summary>Gets the values at the specified index of the <see cref="T:System.Collections.Specialized.NameValueCollection" /> combined into one comma-separated list.</summary>
		/// <param name="index">The zero-based index of the entry that contains the values to get from the collection.</param>
		/// <returns>A <see cref="T:System.String" /> that contains a comma-separated list of the values at the specified index of the <see cref="T:System.Collections.Specialized.NameValueCollection" />, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of indexes for the collection.</exception>
		// Token: 0x06002362 RID: 9058 RVA: 0x000A78CC File Offset: 0x000A5ACC
		public virtual string Get(int index)
		{
			ArrayList arrayList = (ArrayList)base.BaseGet(index);
			return NameValueCollection.GetAsOneString(arrayList);
		}

		/// <summary>Gets the values at the specified index of the <see cref="T:System.Collections.Specialized.NameValueCollection" />.</summary>
		/// <param name="index">The zero-based index of the entry that contains the values to get from the collection.</param>
		/// <returns>A <see cref="T:System.String" /> array that contains the values at the specified index of the <see cref="T:System.Collections.Specialized.NameValueCollection" />, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of indexes for the collection.</exception>
		// Token: 0x06002363 RID: 9059 RVA: 0x000A78EC File Offset: 0x000A5AEC
		public virtual string[] GetValues(int index)
		{
			ArrayList arrayList = (ArrayList)base.BaseGet(index);
			return NameValueCollection.GetAsStringArray(arrayList);
		}

		/// <summary>Gets the key at the specified index of the <see cref="T:System.Collections.Specialized.NameValueCollection" />.</summary>
		/// <param name="index">The zero-based index of the key to get from the collection.</param>
		/// <returns>A <see cref="T:System.String" /> that contains the key at the specified index of the <see cref="T:System.Collections.Specialized.NameValueCollection" />, if found; otherwise, <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of indexes for the collection.</exception>
		// Token: 0x06002364 RID: 9060 RVA: 0x000A790C File Offset: 0x000A5B0C
		public virtual string GetKey(int index)
		{
			return base.BaseGetKey(index);
		}

		/// <summary>Gets the entry at the specified index of the <see cref="T:System.Collections.Specialized.NameValueCollection" />.</summary>
		/// <param name="index">The zero-based index of the entry to locate in the collection.</param>
		/// <returns>A <see cref="T:System.String" /> that contains the comma-separated list of values at the specified index of the collection.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the valid range of indexes for the collection.</exception>
		// Token: 0x170008F2 RID: 2290
		public string this[int index]
		{
			get
			{
				return this.Get(index);
			}
		}

		/// <summary>Gets all the keys in the <see cref="T:System.Collections.Specialized.NameValueCollection" />.</summary>
		/// <returns>A <see cref="T:System.String" /> array that contains all the keys of the <see cref="T:System.Collections.Specialized.NameValueCollection" />.</returns>
		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06002366 RID: 9062 RVA: 0x000A791E File Offset: 0x000A5B1E
		public virtual string[] AllKeys
		{
			get
			{
				if (this._allKeys == null)
				{
					this._allKeys = base.BaseGetAllKeys();
				}
				return this._allKeys;
			}
		}

		// Token: 0x04001FC2 RID: 8130
		private string[] _all;

		// Token: 0x04001FC3 RID: 8131
		private string[] _allKeys;
	}
}
