using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace System.Collections.Specialized
{
	/// <summary>Implements a hash table with the key and the value strongly typed to be strings rather than objects.</summary>
	// Token: 0x020003B7 RID: 951
	[DesignerSerializer("System.Diagnostics.Design.StringDictionaryCodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Serializable]
	public class StringDictionary : IEnumerable
	{
		/// <summary>Gets the number of key/value pairs in the <see cref="T:System.Collections.Specialized.StringDictionary" />.</summary>
		/// <returns>The number of key/value pairs in the <see cref="T:System.Collections.Specialized.StringDictionary" />.  
		///  Retrieving the value of this property is an O(1) operation.</returns>
		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x060023C2 RID: 9154 RVA: 0x000A8767 File Offset: 0x000A6967
		public virtual int Count
		{
			get
			{
				return this.contents.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.Specialized.StringDictionary" /> is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Collections.Specialized.StringDictionary" /> is synchronized (thread safe); otherwise, <see langword="false" />.</returns>
		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x060023C3 RID: 9155 RVA: 0x000A8774 File Offset: 0x000A6974
		public virtual bool IsSynchronized
		{
			get
			{
				return this.contents.IsSynchronized;
			}
		}

		/// <summary>Gets or sets the value associated with the specified key.</summary>
		/// <param name="key">The key whose value to get or set.</param>
		/// <returns>The value associated with the specified key. If the specified key is not found, Get returns <see langword="null" />, and Set creates a new entry with the specified key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x1700090F RID: 2319
		public virtual string this[string key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				return (string)this.contents[key.ToLower(CultureInfo.InvariantCulture)];
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				this.contents[key.ToLower(CultureInfo.InvariantCulture)] = value;
			}
		}

		/// <summary>Gets a collection of keys in the <see cref="T:System.Collections.Specialized.StringDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that provides the keys in the <see cref="T:System.Collections.Specialized.StringDictionary" />.</returns>
		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x060023C6 RID: 9158 RVA: 0x000A87D3 File Offset: 0x000A69D3
		public virtual ICollection Keys
		{
			get
			{
				return this.contents.Keys;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.StringDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that can be used to synchronize access to the <see cref="T:System.Collections.Specialized.StringDictionary" />.</returns>
		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x060023C7 RID: 9159 RVA: 0x000A87E0 File Offset: 0x000A69E0
		public virtual object SyncRoot
		{
			get
			{
				return this.contents.SyncRoot;
			}
		}

		/// <summary>Gets a collection of values in the <see cref="T:System.Collections.Specialized.StringDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that provides the values in the <see cref="T:System.Collections.Specialized.StringDictionary" />.</returns>
		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x060023C8 RID: 9160 RVA: 0x000A87ED File Offset: 0x000A69ED
		public virtual ICollection Values
		{
			get
			{
				return this.contents.Values;
			}
		}

		/// <summary>Adds an entry with the specified key and value into the <see cref="T:System.Collections.Specialized.StringDictionary" />.</summary>
		/// <param name="key">The key of the entry to add.</param>
		/// <param name="value">The value of the entry to add. The value can be <see langword="null" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">An entry with the same key already exists in the <see cref="T:System.Collections.Specialized.StringDictionary" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.StringDictionary" /> is read-only.</exception>
		// Token: 0x060023C9 RID: 9161 RVA: 0x000A87FA File Offset: 0x000A69FA
		public virtual void Add(string key, string value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.contents.Add(key.ToLower(CultureInfo.InvariantCulture), value);
		}

		/// <summary>Removes all entries from the <see cref="T:System.Collections.Specialized.StringDictionary" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.StringDictionary" /> is read-only.</exception>
		// Token: 0x060023CA RID: 9162 RVA: 0x000A8821 File Offset: 0x000A6A21
		public virtual void Clear()
		{
			this.contents.Clear();
		}

		/// <summary>Determines if the <see cref="T:System.Collections.Specialized.StringDictionary" /> contains a specific key.</summary>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.Specialized.StringDictionary" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Specialized.StringDictionary" /> contains an entry with the specified key; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The key is <see langword="null" />.</exception>
		// Token: 0x060023CB RID: 9163 RVA: 0x000A882E File Offset: 0x000A6A2E
		public virtual bool ContainsKey(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this.contents.ContainsKey(key.ToLower(CultureInfo.InvariantCulture));
		}

		/// <summary>Determines if the <see cref="T:System.Collections.Specialized.StringDictionary" /> contains a specific value.</summary>
		/// <param name="value">The value to locate in the <see cref="T:System.Collections.Specialized.StringDictionary" />. The value can be <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.Specialized.StringDictionary" /> contains an element with the specified value; otherwise, <see langword="false" />.</returns>
		// Token: 0x060023CC RID: 9164 RVA: 0x000A8854 File Offset: 0x000A6A54
		public virtual bool ContainsValue(string value)
		{
			return this.contents.ContainsValue(value);
		}

		/// <summary>Copies the string dictionary values to a one-dimensional <see cref="T:System.Array" /> instance at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the <see cref="T:System.Collections.Specialized.StringDictionary" />.</param>
		/// <param name="index">The index in the array where copying begins.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.  
		/// -or-  
		/// The number of elements in the <see cref="T:System.Collections.Specialized.StringDictionary" /> is greater than the available space from <paramref name="index" /> to the end of <paramref name="array" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than the lower bound of <paramref name="array" />.</exception>
		// Token: 0x060023CD RID: 9165 RVA: 0x000A8862 File Offset: 0x000A6A62
		public virtual void CopyTo(Array array, int index)
		{
			this.contents.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that iterates through the string dictionary.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that iterates through the string dictionary.</returns>
		// Token: 0x060023CE RID: 9166 RVA: 0x000A8871 File Offset: 0x000A6A71
		public virtual IEnumerator GetEnumerator()
		{
			return this.contents.GetEnumerator();
		}

		/// <summary>Removes the entry with the specified key from the string dictionary.</summary>
		/// <param name="key">The key of the entry to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">The key is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Specialized.StringDictionary" /> is read-only.</exception>
		// Token: 0x060023CF RID: 9167 RVA: 0x000A887E File Offset: 0x000A6A7E
		public virtual void Remove(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.contents.Remove(key.ToLower(CultureInfo.InvariantCulture));
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x000A88A4 File Offset: 0x000A6AA4
		internal void ReplaceHashtable(Hashtable useThisHashtableInstead)
		{
			this.contents = useThisHashtableInstead;
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x000A88AD File Offset: 0x000A6AAD
		internal IDictionary<string, string> AsGenericDictionary()
		{
			return new StringDictionary.GenericAdapter(this);
		}

		// Token: 0x04001FDD RID: 8157
		internal Hashtable contents = new Hashtable();

		// Token: 0x020007EF RID: 2031
		private class GenericAdapter : IDictionary<string, string>, ICollection<KeyValuePair<string, string>>, IEnumerable<KeyValuePair<string, string>>, IEnumerable
		{
			// Token: 0x060043ED RID: 17389 RVA: 0x0011D5B2 File Offset: 0x0011B7B2
			internal GenericAdapter(StringDictionary stringDictionary)
			{
				this.m_stringDictionary = stringDictionary;
			}

			// Token: 0x060043EE RID: 17390 RVA: 0x0011D5C1 File Offset: 0x0011B7C1
			public void Add(string key, string value)
			{
				this[key] = value;
			}

			// Token: 0x060043EF RID: 17391 RVA: 0x0011D5CB File Offset: 0x0011B7CB
			public bool ContainsKey(string key)
			{
				return this.m_stringDictionary.ContainsKey(key);
			}

			// Token: 0x060043F0 RID: 17392 RVA: 0x0011D5D9 File Offset: 0x0011B7D9
			public void Clear()
			{
				this.m_stringDictionary.Clear();
			}

			// Token: 0x17000F66 RID: 3942
			// (get) Token: 0x060043F1 RID: 17393 RVA: 0x0011D5E6 File Offset: 0x0011B7E6
			public int Count
			{
				get
				{
					return this.m_stringDictionary.Count;
				}
			}

			// Token: 0x17000F67 RID: 3943
			public string this[string key]
			{
				get
				{
					if (key == null)
					{
						throw new ArgumentNullException("key");
					}
					if (!this.m_stringDictionary.ContainsKey(key))
					{
						throw new KeyNotFoundException();
					}
					return this.m_stringDictionary[key];
				}
				set
				{
					if (key == null)
					{
						throw new ArgumentNullException("key");
					}
					this.m_stringDictionary[key] = value;
				}
			}

			// Token: 0x17000F68 RID: 3944
			// (get) Token: 0x060043F4 RID: 17396 RVA: 0x0011D640 File Offset: 0x0011B840
			public ICollection<string> Keys
			{
				get
				{
					if (this._keys == null)
					{
						this._keys = new StringDictionary.GenericAdapter.ICollectionToGenericCollectionAdapter(this.m_stringDictionary, StringDictionary.GenericAdapter.KeyOrValue.Key);
					}
					return this._keys;
				}
			}

			// Token: 0x17000F69 RID: 3945
			// (get) Token: 0x060043F5 RID: 17397 RVA: 0x0011D662 File Offset: 0x0011B862
			public ICollection<string> Values
			{
				get
				{
					if (this._values == null)
					{
						this._values = new StringDictionary.GenericAdapter.ICollectionToGenericCollectionAdapter(this.m_stringDictionary, StringDictionary.GenericAdapter.KeyOrValue.Value);
					}
					return this._values;
				}
			}

			// Token: 0x060043F6 RID: 17398 RVA: 0x0011D684 File Offset: 0x0011B884
			public bool Remove(string key)
			{
				if (!this.m_stringDictionary.ContainsKey(key))
				{
					return false;
				}
				this.m_stringDictionary.Remove(key);
				return true;
			}

			// Token: 0x060043F7 RID: 17399 RVA: 0x0011D6A3 File Offset: 0x0011B8A3
			public bool TryGetValue(string key, out string value)
			{
				if (!this.m_stringDictionary.ContainsKey(key))
				{
					value = null;
					return false;
				}
				value = this.m_stringDictionary[key];
				return true;
			}

			// Token: 0x060043F8 RID: 17400 RVA: 0x0011D6C7 File Offset: 0x0011B8C7
			void ICollection<KeyValuePair<string, string>>.Add(KeyValuePair<string, string> item)
			{
				this.m_stringDictionary.Add(item.Key, item.Value);
			}

			// Token: 0x060043F9 RID: 17401 RVA: 0x0011D6E4 File Offset: 0x0011B8E4
			bool ICollection<KeyValuePair<string, string>>.Contains(KeyValuePair<string, string> item)
			{
				string text;
				return this.TryGetValue(item.Key, out text) && text.Equals(item.Value);
			}

			// Token: 0x060043FA RID: 17402 RVA: 0x0011D714 File Offset: 0x0011B914
			void ICollection<KeyValuePair<string, string>>.CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array", SR.GetString("ArgumentNull_Array"));
				}
				if (arrayIndex < 0)
				{
					throw new ArgumentOutOfRangeException("arrayIndex", SR.GetString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				if (array.Length - arrayIndex < this.Count)
				{
					throw new ArgumentException(SR.GetString("Arg_ArrayPlusOffTooSmall"));
				}
				int num = arrayIndex;
				foreach (object obj in this.m_stringDictionary)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					array[num++] = new KeyValuePair<string, string>((string)dictionaryEntry.Key, (string)dictionaryEntry.Value);
				}
			}

			// Token: 0x17000F6A RID: 3946
			// (get) Token: 0x060043FB RID: 17403 RVA: 0x0011D7E0 File Offset: 0x0011B9E0
			bool ICollection<KeyValuePair<string, string>>.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060043FC RID: 17404 RVA: 0x0011D7E4 File Offset: 0x0011B9E4
			bool ICollection<KeyValuePair<string, string>>.Remove(KeyValuePair<string, string> item)
			{
				if (!((ICollection<KeyValuePair<string, string>>)this).Contains(item))
				{
					return false;
				}
				this.m_stringDictionary.Remove(item.Key);
				return true;
			}

			// Token: 0x060043FD RID: 17405 RVA: 0x0011D811 File Offset: 0x0011BA11
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060043FE RID: 17406 RVA: 0x0011D819 File Offset: 0x0011BA19
			public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
			{
				foreach (object obj in this.m_stringDictionary)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					yield return new KeyValuePair<string, string>((string)dictionaryEntry.Key, (string)dictionaryEntry.Value);
				}
				IEnumerator enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x040034EF RID: 13551
			private StringDictionary m_stringDictionary;

			// Token: 0x040034F0 RID: 13552
			private StringDictionary.GenericAdapter.ICollectionToGenericCollectionAdapter _values;

			// Token: 0x040034F1 RID: 13553
			private StringDictionary.GenericAdapter.ICollectionToGenericCollectionAdapter _keys;

			// Token: 0x02000923 RID: 2339
			internal enum KeyOrValue
			{
				// Token: 0x04003D9F RID: 15775
				Key,
				// Token: 0x04003DA0 RID: 15776
				Value
			}

			// Token: 0x02000924 RID: 2340
			private class ICollectionToGenericCollectionAdapter : ICollection<string>, IEnumerable<string>, IEnumerable
			{
				// Token: 0x06004654 RID: 18004 RVA: 0x00125A0B File Offset: 0x00123C0B
				public ICollectionToGenericCollectionAdapter(StringDictionary source, StringDictionary.GenericAdapter.KeyOrValue keyOrValue)
				{
					if (source == null)
					{
						throw new ArgumentNullException("source");
					}
					this._internal = source;
					this._keyOrValue = keyOrValue;
				}

				// Token: 0x06004655 RID: 18005 RVA: 0x00125A2F File Offset: 0x00123C2F
				public void Add(string item)
				{
					this.ThrowNotSupportedException();
				}

				// Token: 0x06004656 RID: 18006 RVA: 0x00125A37 File Offset: 0x00123C37
				public void Clear()
				{
					this.ThrowNotSupportedException();
				}

				// Token: 0x06004657 RID: 18007 RVA: 0x00125A3F File Offset: 0x00123C3F
				public void ThrowNotSupportedException()
				{
					if (this._keyOrValue == StringDictionary.GenericAdapter.KeyOrValue.Key)
					{
						throw new NotSupportedException(SR.GetString("NotSupported_KeyCollectionSet"));
					}
					throw new NotSupportedException(SR.GetString("NotSupported_ValueCollectionSet"));
				}

				// Token: 0x06004658 RID: 18008 RVA: 0x00125A68 File Offset: 0x00123C68
				public bool Contains(string item)
				{
					if (this._keyOrValue == StringDictionary.GenericAdapter.KeyOrValue.Key)
					{
						return this._internal.ContainsKey(item);
					}
					return this._internal.ContainsValue(item);
				}

				// Token: 0x06004659 RID: 18009 RVA: 0x00125A8C File Offset: 0x00123C8C
				public void CopyTo(string[] array, int arrayIndex)
				{
					ICollection underlyingCollection = this.GetUnderlyingCollection();
					underlyingCollection.CopyTo(array, arrayIndex);
				}

				// Token: 0x17000FDE RID: 4062
				// (get) Token: 0x0600465A RID: 18010 RVA: 0x00125AA8 File Offset: 0x00123CA8
				public int Count
				{
					get
					{
						return this._internal.Count;
					}
				}

				// Token: 0x17000FDF RID: 4063
				// (get) Token: 0x0600465B RID: 18011 RVA: 0x00125AB5 File Offset: 0x00123CB5
				public bool IsReadOnly
				{
					get
					{
						return true;
					}
				}

				// Token: 0x0600465C RID: 18012 RVA: 0x00125AB8 File Offset: 0x00123CB8
				public bool Remove(string item)
				{
					this.ThrowNotSupportedException();
					return false;
				}

				// Token: 0x0600465D RID: 18013 RVA: 0x00125AC1 File Offset: 0x00123CC1
				private ICollection GetUnderlyingCollection()
				{
					if (this._keyOrValue == StringDictionary.GenericAdapter.KeyOrValue.Key)
					{
						return this._internal.Keys;
					}
					return this._internal.Values;
				}

				// Token: 0x0600465E RID: 18014 RVA: 0x00125AE2 File Offset: 0x00123CE2
				public IEnumerator<string> GetEnumerator()
				{
					ICollection underlyingCollection = this.GetUnderlyingCollection();
					foreach (object obj in underlyingCollection)
					{
						string text = (string)obj;
						yield return text;
					}
					IEnumerator enumerator = null;
					yield break;
					yield break;
				}

				// Token: 0x0600465F RID: 18015 RVA: 0x00125AF1 File Offset: 0x00123CF1
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetUnderlyingCollection().GetEnumerator();
				}

				// Token: 0x04003DA1 RID: 15777
				private StringDictionary _internal;

				// Token: 0x04003DA2 RID: 15778
				private StringDictionary.GenericAdapter.KeyOrValue _keyOrValue;
			}
		}
	}
}
