using System;
using System.Collections;
using System.Collections.Specialized;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Represents a collection of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects.</summary>
	// Token: 0x0200059C RID: 1436
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public class PropertyDescriptorCollection : ICollection, IEnumerable, IList, IDictionary
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> class.</summary>
		/// <param name="properties">An array of type <see cref="T:System.ComponentModel.PropertyDescriptor" /> that provides the properties for this collection.</param>
		// Token: 0x06003541 RID: 13633 RVA: 0x000E79F8 File Offset: 0x000E5BF8
		public PropertyDescriptorCollection(PropertyDescriptor[] properties)
		{
			this.properties = properties;
			if (properties == null)
			{
				this.properties = new PropertyDescriptor[0];
				this.propCount = 0;
			}
			else
			{
				this.propCount = properties.Length;
			}
			this.propsOwned = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> class, which is optionally read-only.</summary>
		/// <param name="properties">An array of type <see cref="T:System.ComponentModel.PropertyDescriptor" /> that provides the properties for this collection.</param>
		/// <param name="readOnly">If <see langword="true" />, specifies that the collection cannot be modified.</param>
		// Token: 0x06003542 RID: 13634 RVA: 0x000E7A36 File Offset: 0x000E5C36
		public PropertyDescriptorCollection(PropertyDescriptor[] properties, bool readOnly)
			: this(properties)
		{
			this.readOnly = readOnly;
		}

		// Token: 0x06003543 RID: 13635 RVA: 0x000E7A48 File Offset: 0x000E5C48
		private PropertyDescriptorCollection(PropertyDescriptor[] properties, int propCount, string[] namedSort, IComparer comparer)
		{
			this.propsOwned = false;
			if (namedSort != null)
			{
				this.namedSort = (string[])namedSort.Clone();
			}
			this.comparer = comparer;
			this.properties = properties;
			this.propCount = propCount;
			this.needSort = true;
		}

		/// <summary>Gets the number of property descriptors in the collection.</summary>
		/// <returns>The number of property descriptors in the collection.</returns>
		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x06003544 RID: 13636 RVA: 0x000E7A9A File Offset: 0x000E5C9A
		public int Count
		{
			get
			{
				return this.propCount;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> at the specified index number.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to get or set.</param>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> with the specified index number.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="index" /> parameter is not a valid index for <see cref="P:System.ComponentModel.PropertyDescriptorCollection.Item(System.Int32)" />.</exception>
		// Token: 0x17000D01 RID: 3329
		public virtual PropertyDescriptor this[int index]
		{
			get
			{
				if (index >= this.propCount)
				{
					throw new IndexOutOfRangeException();
				}
				this.EnsurePropsOwned();
				return this.properties[index];
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> with the specified name.</summary>
		/// <param name="name">The name of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to get from the collection.</param>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> with the specified name, or <see langword="null" /> if the property does not exist.</returns>
		// Token: 0x17000D02 RID: 3330
		public virtual PropertyDescriptor this[string name]
		{
			get
			{
				return this.Find(name, false);
			}
		}

		/// <summary>Adds the specified <see cref="T:System.ComponentModel.PropertyDescriptor" /> to the collection.</summary>
		/// <param name="value">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to add to the collection.</param>
		/// <returns>The index of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> that was added to the collection.</returns>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06003547 RID: 13639 RVA: 0x000E7ACC File Offset: 0x000E5CCC
		public int Add(PropertyDescriptor value)
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			this.EnsureSize(this.propCount + 1);
			PropertyDescriptor[] array = this.properties;
			int num = this.propCount;
			this.propCount = num + 1;
			array[num] = value;
			return this.propCount - 1;
		}

		/// <summary>Removes all <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects from the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06003548 RID: 13640 RVA: 0x000E7B16 File Offset: 0x000E5D16
		public void Clear()
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			this.propCount = 0;
			this.cachedFoundProperties = null;
		}

		/// <summary>Returns whether the collection contains the given <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
		/// <param name="value">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to find in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the collection contains the given <see cref="T:System.ComponentModel.PropertyDescriptor" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003549 RID: 13641 RVA: 0x000E7B34 File Offset: 0x000E5D34
		public bool Contains(PropertyDescriptor value)
		{
			return this.IndexOf(value) >= 0;
		}

		/// <summary>Copies the entire collection to an array, starting at the specified index number.</summary>
		/// <param name="array">An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects to copy elements of the collection to.</param>
		/// <param name="index">The index of the <paramref name="array" /> parameter at which copying begins.</param>
		// Token: 0x0600354A RID: 13642 RVA: 0x000E7B43 File Offset: 0x000E5D43
		public void CopyTo(Array array, int index)
		{
			this.EnsurePropsOwned();
			Array.Copy(this.properties, 0, array, index, this.Count);
		}

		// Token: 0x0600354B RID: 13643 RVA: 0x000E7B60 File Offset: 0x000E5D60
		private void EnsurePropsOwned()
		{
			if (!this.propsOwned)
			{
				this.propsOwned = true;
				if (this.properties != null)
				{
					PropertyDescriptor[] array = new PropertyDescriptor[this.Count];
					Array.Copy(this.properties, 0, array, 0, this.Count);
					this.properties = array;
				}
			}
			if (this.needSort)
			{
				this.needSort = false;
				this.InternalSort(this.namedSort);
			}
		}

		// Token: 0x0600354C RID: 13644 RVA: 0x000E7BC8 File Offset: 0x000E5DC8
		private void EnsureSize(int sizeNeeded)
		{
			if (sizeNeeded <= this.properties.Length)
			{
				return;
			}
			if (this.properties == null || this.properties.Length == 0)
			{
				this.propCount = 0;
				this.properties = new PropertyDescriptor[sizeNeeded];
				return;
			}
			this.EnsurePropsOwned();
			int num = Math.Max(sizeNeeded, this.properties.Length * 2);
			PropertyDescriptor[] array = new PropertyDescriptor[num];
			Array.Copy(this.properties, 0, array, 0, this.propCount);
			this.properties = array;
		}

		/// <summary>Returns the <see cref="T:System.ComponentModel.PropertyDescriptor" /> with the specified name, using a Boolean to indicate whether to ignore case.</summary>
		/// <param name="name">The name of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to return from the collection.</param>
		/// <param name="ignoreCase">
		///   <see langword="true" /> if you want to ignore the case of the property name; otherwise, <see langword="false" />.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> with the specified name, or <see langword="null" /> if the property does not exist.</returns>
		// Token: 0x0600354D RID: 13645 RVA: 0x000E7C40 File Offset: 0x000E5E40
		public virtual PropertyDescriptor Find(string name, bool ignoreCase)
		{
			PropertyDescriptor propertyDescriptor2;
			lock (this)
			{
				PropertyDescriptor propertyDescriptor = null;
				if (this.cachedFoundProperties == null || this.cachedIgnoreCase != ignoreCase)
				{
					this.cachedIgnoreCase = ignoreCase;
					this.cachedFoundProperties = new HybridDictionary(ignoreCase);
				}
				object obj = this.cachedFoundProperties[name];
				if (obj != null)
				{
					propertyDescriptor2 = (PropertyDescriptor)obj;
				}
				else
				{
					for (int i = 0; i < this.propCount; i++)
					{
						if (ignoreCase)
						{
							if (string.Equals(this.properties[i].Name, name, StringComparison.OrdinalIgnoreCase))
							{
								this.cachedFoundProperties[name] = this.properties[i];
								propertyDescriptor = this.properties[i];
								break;
							}
						}
						else if (this.properties[i].Name.Equals(name))
						{
							this.cachedFoundProperties[name] = this.properties[i];
							propertyDescriptor = this.properties[i];
							break;
						}
					}
					propertyDescriptor2 = propertyDescriptor;
				}
			}
			return propertyDescriptor2;
		}

		/// <summary>Returns the index of the given <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
		/// <param name="value">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to return the index of.</param>
		/// <returns>The index of the given <see cref="T:System.ComponentModel.PropertyDescriptor" />.</returns>
		// Token: 0x0600354E RID: 13646 RVA: 0x000E7D48 File Offset: 0x000E5F48
		public int IndexOf(PropertyDescriptor value)
		{
			return Array.IndexOf<PropertyDescriptor>(this.properties, value, 0, this.propCount);
		}

		/// <summary>Adds the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to the collection at the specified index number.</summary>
		/// <param name="index">The index at which to add the <paramref name="value" /> parameter to the collection.</param>
		/// <param name="value">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to add to the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x0600354F RID: 13647 RVA: 0x000E7D60 File Offset: 0x000E5F60
		public void Insert(int index, PropertyDescriptor value)
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			this.EnsureSize(this.propCount + 1);
			if (index < this.propCount)
			{
				Array.Copy(this.properties, index, this.properties, index + 1, this.propCount - index);
			}
			this.properties[index] = value;
			this.propCount++;
		}

		/// <summary>Removes the specified <see cref="T:System.ComponentModel.PropertyDescriptor" /> from the collection.</summary>
		/// <param name="value">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to remove from the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06003550 RID: 13648 RVA: 0x000E7DC8 File Offset: 0x000E5FC8
		public void Remove(PropertyDescriptor value)
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			int num = this.IndexOf(value);
			if (num != -1)
			{
				this.RemoveAt(num);
			}
		}

		/// <summary>Removes the <see cref="T:System.ComponentModel.PropertyDescriptor" /> at the specified index from the collection.</summary>
		/// <param name="index">The index of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to remove from the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06003551 RID: 13649 RVA: 0x000E7DF8 File Offset: 0x000E5FF8
		public void RemoveAt(int index)
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			if (index < this.propCount - 1)
			{
				Array.Copy(this.properties, index + 1, this.properties, index, this.propCount - index - 1);
			}
			this.properties[this.propCount - 1] = null;
			this.propCount--;
		}

		/// <summary>Sorts the members of this collection, using the default sort for this collection, which is usually alphabetical.</summary>
		/// <returns>A new <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the sorted <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects.</returns>
		// Token: 0x06003552 RID: 13650 RVA: 0x000E7E5B File Offset: 0x000E605B
		public virtual PropertyDescriptorCollection Sort()
		{
			return new PropertyDescriptorCollection(this.properties, this.propCount, this.namedSort, this.comparer);
		}

		/// <summary>Sorts the members of this collection. The specified order is applied first, followed by the default sort for this collection, which is usually alphabetical.</summary>
		/// <param name="names">An array of strings describing the order in which to sort the <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects in this collection.</param>
		/// <returns>A new <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the sorted <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects.</returns>
		// Token: 0x06003553 RID: 13651 RVA: 0x000E7E7A File Offset: 0x000E607A
		public virtual PropertyDescriptorCollection Sort(string[] names)
		{
			return new PropertyDescriptorCollection(this.properties, this.propCount, names, this.comparer);
		}

		/// <summary>Sorts the members of this collection. The specified order is applied first, followed by the sort using the specified <see cref="T:System.Collections.IComparer" />.</summary>
		/// <param name="names">An array of strings describing the order in which to sort the <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects in this collection.</param>
		/// <param name="comparer">A comparer to use to sort the <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects in this collection.</param>
		/// <returns>A new <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the sorted <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects.</returns>
		// Token: 0x06003554 RID: 13652 RVA: 0x000E7E94 File Offset: 0x000E6094
		public virtual PropertyDescriptorCollection Sort(string[] names, IComparer comparer)
		{
			return new PropertyDescriptorCollection(this.properties, this.propCount, names, comparer);
		}

		/// <summary>Sorts the members of this collection, using the specified <see cref="T:System.Collections.IComparer" />.</summary>
		/// <param name="comparer">A comparer to use to sort the <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects in this collection.</param>
		/// <returns>A new <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the sorted <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects.</returns>
		// Token: 0x06003555 RID: 13653 RVA: 0x000E7EA9 File Offset: 0x000E60A9
		public virtual PropertyDescriptorCollection Sort(IComparer comparer)
		{
			return new PropertyDescriptorCollection(this.properties, this.propCount, this.namedSort, comparer);
		}

		/// <summary>Sorts the members of this collection. The specified order is applied first, followed by the default sort for this collection, which is usually alphabetical.</summary>
		/// <param name="names">An array of strings describing the order in which to sort the <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects in this collection.</param>
		// Token: 0x06003556 RID: 13654 RVA: 0x000E7EC4 File Offset: 0x000E60C4
		protected void InternalSort(string[] names)
		{
			if (this.properties == null || this.properties.Length == 0)
			{
				return;
			}
			this.InternalSort(this.comparer);
			if (names != null && names.Length != 0)
			{
				ArrayList arrayList = new ArrayList(this.properties);
				int num = 0;
				int num2 = this.properties.Length;
				for (int i = 0; i < names.Length; i++)
				{
					for (int j = 0; j < num2; j++)
					{
						PropertyDescriptor propertyDescriptor = (PropertyDescriptor)arrayList[j];
						if (propertyDescriptor != null && propertyDescriptor.Name.Equals(names[i]))
						{
							this.properties[num++] = propertyDescriptor;
							arrayList[j] = null;
							break;
						}
					}
				}
				for (int k = 0; k < num2; k++)
				{
					if (arrayList[k] != null)
					{
						this.properties[num++] = (PropertyDescriptor)arrayList[k];
					}
				}
			}
		}

		/// <summary>Sorts the members of this collection, using the specified <see cref="T:System.Collections.IComparer" />.</summary>
		/// <param name="sorter">A comparer to use to sort the <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects in this collection.</param>
		// Token: 0x06003557 RID: 13655 RVA: 0x000E7FA1 File Offset: 0x000E61A1
		protected void InternalSort(IComparer sorter)
		{
			if (sorter == null)
			{
				TypeDescriptor.SortDescriptorArray(this);
				return;
			}
			Array.Sort(this.properties, sorter);
		}

		/// <summary>Returns an enumerator for this class.</summary>
		/// <returns>An enumerator of type <see cref="T:System.Collections.IEnumerator" />.</returns>
		// Token: 0x06003558 RID: 13656 RVA: 0x000E7FBC File Offset: 0x000E61BC
		public virtual IEnumerator GetEnumerator()
		{
			this.EnsurePropsOwned();
			if (this.properties.Length != this.propCount)
			{
				PropertyDescriptor[] array = new PropertyDescriptor[this.propCount];
				Array.Copy(this.properties, 0, array, 0, this.propCount);
				return array.GetEnumerator();
			}
			return this.properties.GetEnumerator();
		}

		/// <summary>Gets the number of elements contained in the collection.</summary>
		/// <returns>The number of elements contained in the collection.</returns>
		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x06003559 RID: 13657 RVA: 0x000E8011 File Offset: 0x000E6211
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="true" /> if access to the collection is synchronized (thread safe); otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x0600355A RID: 13658 RVA: 0x000E8019 File Offset: 0x000E6219
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x0600355B RID: 13659 RVA: 0x000E801C File Offset: 0x000E621C
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		/// <summary>Adds an element with the provided key and value to the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <param name="key">The <see cref="T:System.Object" /> to use as the key of the element to add.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to use as the value of the element to add.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x0600355C RID: 13660 RVA: 0x000E8020 File Offset: 0x000E6220
		void IDictionary.Add(object key, object value)
		{
			PropertyDescriptor propertyDescriptor = value as PropertyDescriptor;
			if (propertyDescriptor == null)
			{
				throw new ArgumentException("value");
			}
			this.Add(propertyDescriptor);
		}

		/// <summary>Removes all elements from the <see cref="T:System.Collections.IDictionary" />.</summary>
		// Token: 0x0600355D RID: 13661 RVA: 0x000E804A File Offset: 0x000E624A
		void IDictionary.Clear()
		{
			this.Clear();
		}

		/// <summary>Determines whether the <see cref="T:System.Collections.IDictionary" /> contains an element with the specified key.</summary>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.IDictionary" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IDictionary" /> contains an element with the key; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600355E RID: 13662 RVA: 0x000E8052 File Offset: 0x000E6252
		bool IDictionary.Contains(object key)
		{
			return key is string && this[(string)key] != null;
		}

		/// <summary>Returns an enumerator for this class.</summary>
		/// <returns>An enumerator of type <see cref="T:System.Collections.IEnumerator" />.</returns>
		// Token: 0x0600355F RID: 13663 RVA: 0x000E806D File Offset: 0x000E626D
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new PropertyDescriptorCollection.PropertyDescriptorEnumerator(this);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> has a fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IDictionary" /> has a fixed size; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x06003560 RID: 13664 RVA: 0x000E8075 File Offset: 0x000E6275
		bool IDictionary.IsFixedSize
		{
			get
			{
				return this.readOnly;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Collections.IDictionary" /> is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Collections.IDictionary" /> is read-only; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x06003561 RID: 13665 RVA: 0x000E807D File Offset: 0x000E627D
		bool IDictionary.IsReadOnly
		{
			get
			{
				return this.readOnly;
			}
		}

		/// <summary>Gets or sets the element with the specified key.</summary>
		/// <param name="key">The key of the element to get or set.</param>
		/// <returns>The element with the specified key.</returns>
		// Token: 0x17000D08 RID: 3336
		object IDictionary.this[object key]
		{
			get
			{
				if (key is string)
				{
					return this[(string)key];
				}
				return null;
			}
			set
			{
				if (this.readOnly)
				{
					throw new NotSupportedException();
				}
				if (value != null && !(value is PropertyDescriptor))
				{
					throw new ArgumentException("value");
				}
				int num = -1;
				if (key is int)
				{
					num = (int)key;
					if (num < 0 || num >= this.propCount)
					{
						throw new IndexOutOfRangeException();
					}
				}
				else
				{
					if (!(key is string))
					{
						throw new ArgumentException("key");
					}
					for (int i = 0; i < this.propCount; i++)
					{
						if (this.properties[i].Name.Equals((string)key))
						{
							num = i;
							break;
						}
					}
				}
				if (num == -1)
				{
					this.Add((PropertyDescriptor)value);
					return;
				}
				this.EnsurePropsOwned();
				this.properties[num] = (PropertyDescriptor)value;
				if (this.cachedFoundProperties != null && key is string)
				{
					this.cachedFoundProperties[key] = value;
				}
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x06003564 RID: 13668 RVA: 0x000E817C File Offset: 0x000E637C
		ICollection IDictionary.Keys
		{
			get
			{
				string[] array = new string[this.propCount];
				for (int i = 0; i < this.propCount; i++)
				{
					array[i] = this.properties[i].Name;
				}
				return array;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x06003565 RID: 13669 RVA: 0x000E81B8 File Offset: 0x000E63B8
		ICollection IDictionary.Values
		{
			get
			{
				if (this.properties.Length != this.propCount)
				{
					PropertyDescriptor[] array = new PropertyDescriptor[this.propCount];
					Array.Copy(this.properties, 0, array, 0, this.propCount);
					return array;
				}
				return (ICollection)this.properties.Clone();
			}
		}

		/// <summary>Removes the element with the specified key from the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <param name="key">The key of the element to remove.</param>
		// Token: 0x06003566 RID: 13670 RVA: 0x000E8208 File Offset: 0x000E6408
		void IDictionary.Remove(object key)
		{
			if (key is string)
			{
				PropertyDescriptor propertyDescriptor = this[(string)key];
				if (propertyDescriptor != null)
				{
					((IList)this).Remove(propertyDescriptor);
				}
			}
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Collections.IDictionary" />.</returns>
		// Token: 0x06003567 RID: 13671 RVA: 0x000E8234 File Offset: 0x000E6434
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Adds an item to the <see cref="T:System.Collections.IList" />.</summary>
		/// <param name="value">The item to add to the collection.</param>
		/// <returns>The position into which the new element was inserted.</returns>
		// Token: 0x06003568 RID: 13672 RVA: 0x000E823C File Offset: 0x000E643C
		int IList.Add(object value)
		{
			return this.Add((PropertyDescriptor)value);
		}

		/// <summary>Removes all items from the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06003569 RID: 13673 RVA: 0x000E824A File Offset: 0x000E644A
		void IList.Clear()
		{
			this.Clear();
		}

		/// <summary>Determines whether the collection contains a specific value.</summary>
		/// <param name="value">The item to locate in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the item is found in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600356A RID: 13674 RVA: 0x000E8252 File Offset: 0x000E6452
		bool IList.Contains(object value)
		{
			return this.Contains((PropertyDescriptor)value);
		}

		/// <summary>Determines the index of a specified item in the collection.</summary>
		/// <param name="value">The item to locate in the collection.</param>
		/// <returns>The index of <paramref name="value" /> if found in the list, otherwise -1.</returns>
		// Token: 0x0600356B RID: 13675 RVA: 0x000E8260 File Offset: 0x000E6460
		int IList.IndexOf(object value)
		{
			return this.IndexOf((PropertyDescriptor)value);
		}

		/// <summary>Inserts an item into the collection at a specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The item to insert into the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x0600356C RID: 13676 RVA: 0x000E826E File Offset: 0x000E646E
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (PropertyDescriptor)value);
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the collection is read-only; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x0600356D RID: 13677 RVA: 0x000E827D File Offset: 0x000E647D
		bool IList.IsReadOnly
		{
			get
			{
				return this.readOnly;
			}
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> if the collection has a fixed size; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x0600356E RID: 13678 RVA: 0x000E8285 File Offset: 0x000E6485
		bool IList.IsFixedSize
		{
			get
			{
				return this.readOnly;
			}
		}

		/// <summary>Removes the first occurrence of a specified value from the collection.</summary>
		/// <param name="value">The item to remove from the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x0600356F RID: 13679 RVA: 0x000E828D File Offset: 0x000E648D
		void IList.Remove(object value)
		{
			this.Remove((PropertyDescriptor)value);
		}

		/// <summary>Removes the item at the specified index.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06003570 RID: 13680 RVA: 0x000E829B File Offset: 0x000E649B
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		/// <summary>Gets or sets an item from the collection at a specified index.</summary>
		/// <param name="index">The zero-based index of the item to get or set.</param>
		/// <returns>The element at the specified index.</returns>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.ComponentModel.PropertyDescriptor" />.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is less than 0.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than <see cref="P:System.ComponentModel.EventDescriptorCollection.Count" />.</exception>
		// Token: 0x17000D0D RID: 3341
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				if (this.readOnly)
				{
					throw new NotSupportedException();
				}
				if (index >= this.propCount)
				{
					throw new IndexOutOfRangeException();
				}
				if (value != null && !(value is PropertyDescriptor))
				{
					throw new ArgumentException("value");
				}
				this.EnsurePropsOwned();
				this.properties[index] = (PropertyDescriptor)value;
			}
		}

		/// <summary>Specifies an empty collection that you can use instead of creating a new one with no items. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x04002A2E RID: 10798
		public static readonly PropertyDescriptorCollection Empty = new PropertyDescriptorCollection(null, true);

		// Token: 0x04002A2F RID: 10799
		private IDictionary cachedFoundProperties;

		// Token: 0x04002A30 RID: 10800
		private bool cachedIgnoreCase;

		// Token: 0x04002A31 RID: 10801
		private PropertyDescriptor[] properties;

		// Token: 0x04002A32 RID: 10802
		private int propCount;

		// Token: 0x04002A33 RID: 10803
		private string[] namedSort;

		// Token: 0x04002A34 RID: 10804
		private IComparer comparer;

		// Token: 0x04002A35 RID: 10805
		private bool propsOwned = true;

		// Token: 0x04002A36 RID: 10806
		private bool needSort;

		// Token: 0x04002A37 RID: 10807
		private bool readOnly;

		// Token: 0x02000898 RID: 2200
		private class PropertyDescriptorEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06004583 RID: 17795 RVA: 0x001226D5 File Offset: 0x001208D5
			public PropertyDescriptorEnumerator(PropertyDescriptorCollection owner)
			{
				this.owner = owner;
			}

			// Token: 0x17000FBB RID: 4027
			// (get) Token: 0x06004584 RID: 17796 RVA: 0x001226EB File Offset: 0x001208EB
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x17000FBC RID: 4028
			// (get) Token: 0x06004585 RID: 17797 RVA: 0x001226F8 File Offset: 0x001208F8
			public DictionaryEntry Entry
			{
				get
				{
					PropertyDescriptor propertyDescriptor = this.owner[this.index];
					return new DictionaryEntry(propertyDescriptor.Name, propertyDescriptor);
				}
			}

			// Token: 0x17000FBD RID: 4029
			// (get) Token: 0x06004586 RID: 17798 RVA: 0x00122723 File Offset: 0x00120923
			public object Key
			{
				get
				{
					return this.owner[this.index].Name;
				}
			}

			// Token: 0x17000FBE RID: 4030
			// (get) Token: 0x06004587 RID: 17799 RVA: 0x0012273B File Offset: 0x0012093B
			public object Value
			{
				get
				{
					return this.owner[this.index].Name;
				}
			}

			// Token: 0x06004588 RID: 17800 RVA: 0x00122753 File Offset: 0x00120953
			public bool MoveNext()
			{
				if (this.index < this.owner.Count - 1)
				{
					this.index++;
					return true;
				}
				return false;
			}

			// Token: 0x06004589 RID: 17801 RVA: 0x0012277B File Offset: 0x0012097B
			public void Reset()
			{
				this.index = -1;
			}

			// Token: 0x040037C6 RID: 14278
			private PropertyDescriptorCollection owner;

			// Token: 0x040037C7 RID: 14279
			private int index = -1;
		}
	}
}
