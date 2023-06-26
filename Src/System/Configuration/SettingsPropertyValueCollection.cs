using System;
using System.Collections;

namespace System.Configuration
{
	/// <summary>Contains a collection of settings property values that map <see cref="T:System.Configuration.SettingsProperty" /> objects to <see cref="T:System.Configuration.SettingsPropertyValue" /> objects.</summary>
	// Token: 0x020000AE RID: 174
	public class SettingsPropertyValueCollection : IEnumerable, ICloneable, ICollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsPropertyValueCollection" /> class.</summary>
		// Token: 0x060005F7 RID: 1527 RVA: 0x00023994 File Offset: 0x00021B94
		public SettingsPropertyValueCollection()
		{
			this._Indices = new Hashtable(10, StringComparer.CurrentCultureIgnoreCase);
			this._Values = new ArrayList();
		}

		/// <summary>Adds a <see cref="T:System.Configuration.SettingsPropertyValue" /> object to the collection.</summary>
		/// <param name="property">A <see cref="T:System.Configuration.SettingsPropertyValue" /> object.</param>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to add an item to the collection, but the collection was marked as read-only.</exception>
		// Token: 0x060005F8 RID: 1528 RVA: 0x000239BC File Offset: 0x00021BBC
		public void Add(SettingsPropertyValue property)
		{
			if (this._ReadOnly)
			{
				throw new NotSupportedException();
			}
			int num = this._Values.Add(property);
			try
			{
				this._Indices.Add(property.Name, num);
			}
			catch (Exception)
			{
				this._Values.RemoveAt(num);
				throw;
			}
		}

		/// <summary>Removes a <see cref="T:System.Configuration.SettingsPropertyValue" /> object from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.SettingsPropertyValue" /> object.</param>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to remove an item from the collection, but the collection was marked as read-only.</exception>
		// Token: 0x060005F9 RID: 1529 RVA: 0x00023A1C File Offset: 0x00021C1C
		public void Remove(string name)
		{
			if (this._ReadOnly)
			{
				throw new NotSupportedException();
			}
			object obj = this._Indices[name];
			if (obj == null || !(obj is int))
			{
				return;
			}
			int num = (int)obj;
			if (num >= this._Values.Count)
			{
				return;
			}
			this._Values.RemoveAt(num);
			this._Indices.Remove(name);
			ArrayList arrayList = new ArrayList();
			foreach (object obj2 in this._Indices)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
				if ((int)dictionaryEntry.Value > num)
				{
					arrayList.Add(dictionaryEntry.Key);
				}
			}
			foreach (object obj3 in arrayList)
			{
				string text = (string)obj3;
				this._Indices[text] = (int)this._Indices[text] - 1;
			}
		}

		/// <summary>Gets an item from the collection.</summary>
		/// <param name="name">A <see cref="T:System.Configuration.SettingsPropertyValue" /> object.</param>
		/// <returns>The <see cref="T:System.Configuration.SettingsPropertyValue" /> object with the specified <paramref name="name" />.</returns>
		// Token: 0x170000F6 RID: 246
		public SettingsPropertyValue this[string name]
		{
			get
			{
				object obj = this._Indices[name];
				if (obj == null || !(obj is int))
				{
					return null;
				}
				int num = (int)obj;
				if (num >= this._Values.Count)
				{
					return null;
				}
				return (SettingsPropertyValue)this._Values[num];
			}
		}

		/// <summary>Gets the <see cref="T:System.Collections.IEnumerator" /> object as it applies to the collection.</summary>
		/// <returns>The <see cref="T:System.Collections.IEnumerator" /> object as it applies to the collection.</returns>
		// Token: 0x060005FB RID: 1531 RVA: 0x00023BA3 File Offset: 0x00021DA3
		public IEnumerator GetEnumerator()
		{
			return this._Values.GetEnumerator();
		}

		/// <summary>Creates a copy of the existing collection.</summary>
		/// <returns>A <see cref="T:System.Configuration.SettingsPropertyValueCollection" /> class.</returns>
		// Token: 0x060005FC RID: 1532 RVA: 0x00023BB0 File Offset: 0x00021DB0
		public object Clone()
		{
			return new SettingsPropertyValueCollection(this._Indices, this._Values);
		}

		/// <summary>Sets the collection to be read-only.</summary>
		// Token: 0x060005FD RID: 1533 RVA: 0x00023BC3 File Offset: 0x00021DC3
		public void SetReadOnly()
		{
			if (this._ReadOnly)
			{
				return;
			}
			this._ReadOnly = true;
			this._Values = ArrayList.ReadOnly(this._Values);
		}

		/// <summary>Removes all <see cref="T:System.Configuration.SettingsPropertyValue" /> objects from the collection.</summary>
		// Token: 0x060005FE RID: 1534 RVA: 0x00023BE6 File Offset: 0x00021DE6
		public void Clear()
		{
			this._Values.Clear();
			this._Indices.Clear();
		}

		/// <summary>Gets a value that specifies the number of <see cref="T:System.Configuration.SettingsPropertyValue" /> objects in the collection.</summary>
		/// <returns>The number of <see cref="T:System.Configuration.SettingsPropertyValue" /> objects in the collection.</returns>
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x00023BFE File Offset: 0x00021DFE
		public int Count
		{
			get
			{
				return this._Values.Count;
			}
		}

		/// <summary>Gets a value that indicates whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the <see cref="T:System.Configuration.SettingsPropertyValueCollection" /> collection is synchronized; otherwise, <see langword="false" />.</returns>
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x00023C0B File Offset: 0x00021E0B
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the object to synchronize access to the collection.</summary>
		/// <returns>The object to synchronize access to the collection.</returns>
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x00023C0E File Offset: 0x00021E0E
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Copies this <see cref="T:System.Configuration.SettingsPropertyValueCollection" /> collection to an array.</summary>
		/// <param name="array">The array to copy the collection to.</param>
		/// <param name="index">The index at which to begin copying.</param>
		// Token: 0x06000602 RID: 1538 RVA: 0x00023C11 File Offset: 0x00021E11
		public void CopyTo(Array array, int index)
		{
			this._Values.CopyTo(array, index);
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00023C20 File Offset: 0x00021E20
		private SettingsPropertyValueCollection(Hashtable indices, ArrayList values)
		{
			this._Indices = (Hashtable)indices.Clone();
			this._Values = (ArrayList)values.Clone();
		}

		// Token: 0x04000C55 RID: 3157
		private Hashtable _Indices;

		// Token: 0x04000C56 RID: 3158
		private ArrayList _Values;

		// Token: 0x04000C57 RID: 3159
		private bool _ReadOnly;
	}
}
