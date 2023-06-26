using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	/// <summary>Contains a collection of strings to use for the auto-complete feature on certain Windows Forms controls.</summary>
	// Token: 0x02000128 RID: 296
	public class AutoCompleteStringCollection : IList, ICollection, IEnumerable
	{
		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <param name="index">The index at which to get or set the <see cref="T:System.String" />.</param>
		/// <returns>The <see cref="T:System.String" /> at the specified position.</returns>
		// Token: 0x17000288 RID: 648
		public string this[int index]
		{
			get
			{
				return (string)this.data[index];
			}
			set
			{
				this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, this.data[index]));
				this.data[index] = value;
				this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, value));
			}
		}

		/// <summary>Gets the number of items in the <see cref="T:System.Windows.Forms.AutoCompleteStringCollection" /> .</summary>
		/// <returns>The number of items in the <see cref="T:System.Windows.Forms.AutoCompleteStringCollection" />.</returns>
		// Token: 0x17000289 RID: 649
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x00019C9B File Offset: 0x00017E9B
		public int Count
		{
			get
			{
				return this.data.Count;
			}
		}

		/// <summary>Gets a value indicating whether the collection is read-only. For a description of this member, see <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the collection is read-only; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700028A RID: 650
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x0001180C File Offset: 0x0000FA0C
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size. For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the collection has a fixed size; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700028B RID: 651
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x0001180C File Offset: 0x0000FA0C
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Occurs when the collection changes.</summary>
		// Token: 0x14000025 RID: 37
		// (add) Token: 0x0600097E RID: 2430 RVA: 0x00019CA8 File Offset: 0x00017EA8
		// (remove) Token: 0x0600097F RID: 2431 RVA: 0x00019CC1 File Offset: 0x00017EC1
		public event CollectionChangeEventHandler CollectionChanged
		{
			add
			{
				this.onCollectionChanged = (CollectionChangeEventHandler)Delegate.Combine(this.onCollectionChanged, value);
			}
			remove
			{
				this.onCollectionChanged = (CollectionChangeEventHandler)Delegate.Remove(this.onCollectionChanged, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.AutoCompleteStringCollection.CollectionChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data.</param>
		// Token: 0x06000980 RID: 2432 RVA: 0x00019CDA File Offset: 0x00017EDA
		protected void OnCollectionChanged(CollectionChangeEventArgs e)
		{
			if (this.onCollectionChanged != null)
			{
				this.onCollectionChanged(this, e);
			}
		}

		/// <summary>Inserts a new <see cref="T:System.String" /> into the collection.</summary>
		/// <param name="value">The <see cref="T:System.String" /> to add to the collection.</param>
		/// <returns>The position in the collection where the <see cref="T:System.String" /> was added.</returns>
		// Token: 0x06000981 RID: 2433 RVA: 0x00019CF4 File Offset: 0x00017EF4
		public int Add(string value)
		{
			int num = this.data.Add(value);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, value));
			return num;
		}

		/// <summary>Adds the elements of a <see cref="T:System.String" /> collection to the end.</summary>
		/// <param name="value">The strings to add to the collection.</param>
		// Token: 0x06000982 RID: 2434 RVA: 0x00019D1C File Offset: 0x00017F1C
		public void AddRange(string[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.data.AddRange(value);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
		}

		/// <summary>Removes all strings from the collection.</summary>
		// Token: 0x06000983 RID: 2435 RVA: 0x00019D45 File Offset: 0x00017F45
		public void Clear()
		{
			this.data.Clear();
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
		}

		/// <summary>Indicates whether the <see cref="T:System.String" /> exists within the collection.</summary>
		/// <param name="value">The <see cref="T:System.String" /> for which to search.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.String" /> exists within the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000984 RID: 2436 RVA: 0x00019D5F File Offset: 0x00017F5F
		public bool Contains(string value)
		{
			return this.data.Contains(value);
		}

		/// <summary>Copies an array of <see cref="T:System.String" /> objects into the collection, starting at the specified position.</summary>
		/// <param name="array">The <see cref="T:System.String" /> objects to add to the collection.</param>
		/// <param name="index">The position within the collection at which to start the insertion.</param>
		// Token: 0x06000985 RID: 2437 RVA: 0x00019D6D File Offset: 0x00017F6D
		public void CopyTo(string[] array, int index)
		{
			this.data.CopyTo(array, index);
		}

		/// <summary>Obtains the position of the specified string within the collection.</summary>
		/// <param name="value">The <see cref="T:System.String" /> for which to search.</param>
		/// <returns>The index for the specified item.</returns>
		// Token: 0x06000986 RID: 2438 RVA: 0x00019D7C File Offset: 0x00017F7C
		public int IndexOf(string value)
		{
			return this.data.IndexOf(value);
		}

		/// <summary>Inserts the string into a specific index in the collection.</summary>
		/// <param name="index">The position at which to insert the string.</param>
		/// <param name="value">The string to insert.</param>
		// Token: 0x06000987 RID: 2439 RVA: 0x00019D8A File Offset: 0x00017F8A
		public void Insert(int index, string value)
		{
			this.data.Insert(index, value);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, value));
		}

		/// <summary>Gets a value indicating whether the contents of the collection are read-only.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x0001180C File Offset: 0x0000FA0C
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Windows.Forms.AutoCompleteStringCollection" /> is synchronized (thread safe).</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x0001180C File Offset: 0x0000FA0C
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Removes a string from the collection.</summary>
		/// <param name="value">The <see cref="T:System.String" /> to remove.</param>
		// Token: 0x0600098A RID: 2442 RVA: 0x00019DA6 File Offset: 0x00017FA6
		public void Remove(string value)
		{
			this.data.Remove(value);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, value));
		}

		/// <summary>Removes the string at the specified index.</summary>
		/// <param name="index">The zero-based index of the string to remove.</param>
		// Token: 0x0600098B RID: 2443 RVA: 0x00019DC4 File Offset: 0x00017FC4
		public void RemoveAt(int index)
		{
			string text = (string)this.data[index];
			this.data.RemoveAt(index);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, text));
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.AutoCompleteStringCollection" />.</summary>
		/// <returns>Returns this <see cref="T:System.Windows.Forms.AutoCompleteStringCollection" />.</returns>
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x00006A49 File Offset: 0x00004C49
		public object SyncRoot
		{
			[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
			get
			{
				return this;
			}
		}

		/// <summary>Gets the element at a specified index. For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
		/// <param name="index">The zero-based index of the element to get.</param>
		/// <returns>The element at the specified index.</returns>
		// Token: 0x1700028F RID: 655
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (string)value;
			}
		}

		/// <summary>Adds a string to the collection. For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
		/// <param name="value">The string to be added to the collection</param>
		/// <returns>The index at which the <paramref name="value" /> has been added.</returns>
		// Token: 0x0600098F RID: 2447 RVA: 0x00019E14 File Offset: 0x00018014
		int IList.Add(object value)
		{
			return this.Add((string)value);
		}

		/// <summary>Determines where the collection contains a specified string. For a description of this member, see <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
		/// <param name="value">The string to locate in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is found in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000990 RID: 2448 RVA: 0x00019E22 File Offset: 0x00018022
		bool IList.Contains(object value)
		{
			return this.Contains((string)value);
		}

		/// <summary>Determines the index of a specified string in the collection. For a description of this member, see <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
		/// <param name="value">The string to locate in the collection.</param>
		/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
		// Token: 0x06000991 RID: 2449 RVA: 0x00019E30 File Offset: 0x00018030
		int IList.IndexOf(object value)
		{
			return this.IndexOf((string)value);
		}

		/// <summary>Inserts an item to the collection at the specified index. For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The string to insert into the collection.</param>
		// Token: 0x06000992 RID: 2450 RVA: 0x00019E3E File Offset: 0x0001803E
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (string)value);
		}

		/// <summary>Removes the first occurrence of a specific string from the collection. For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
		/// <param name="value">The string to remove from the collection.</param>
		// Token: 0x06000993 RID: 2451 RVA: 0x00019E4D File Offset: 0x0001804D
		void IList.Remove(object value)
		{
			this.Remove((string)value);
		}

		/// <summary>Copies the strings of the collection to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index. For a description of this member, see <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the strings copied from collection. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x06000994 RID: 2452 RVA: 0x00019D6D File Offset: 0x00017F6D
		void ICollection.CopyTo(Array array, int index)
		{
			this.data.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Windows.Forms.AutoCompleteStringCollection" />.</summary>
		/// <returns>An enumerator that iterates through the collection.</returns>
		// Token: 0x06000995 RID: 2453 RVA: 0x00019E5B File Offset: 0x0001805B
		public IEnumerator GetEnumerator()
		{
			return this.data.GetEnumerator();
		}

		// Token: 0x0400060E RID: 1550
		private CollectionChangeEventHandler onCollectionChanged;

		// Token: 0x0400060F RID: 1551
		private ArrayList data = new ArrayList();
	}
}
