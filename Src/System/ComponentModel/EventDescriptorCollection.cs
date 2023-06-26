using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Represents a collection of <see cref="T:System.ComponentModel.EventDescriptor" /> objects.</summary>
	// Token: 0x0200054F RID: 1359
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public class EventDescriptorCollection : ICollection, IEnumerable, IList
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EventDescriptorCollection" /> class with the given array of <see cref="T:System.ComponentModel.EventDescriptor" /> objects.</summary>
		/// <param name="events">An array of type <see cref="T:System.ComponentModel.EventDescriptor" /> that provides the events for this collection.</param>
		// Token: 0x060032FC RID: 13052 RVA: 0x000E2C38 File Offset: 0x000E0E38
		public EventDescriptorCollection(EventDescriptor[] events)
		{
			this.events = events;
			if (events == null)
			{
				this.events = new EventDescriptor[0];
				this.eventCount = 0;
			}
			else
			{
				this.eventCount = this.events.Length;
			}
			this.eventsOwned = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EventDescriptorCollection" /> class with the given array of <see cref="T:System.ComponentModel.EventDescriptor" /> objects. The collection is optionally read-only.</summary>
		/// <param name="events">An array of type <see cref="T:System.ComponentModel.EventDescriptor" /> that provides the events for this collection.</param>
		/// <param name="readOnly">
		///   <see langword="true" /> to specify a read-only collection; otherwise, <see langword="false" />.</param>
		// Token: 0x060032FD RID: 13053 RVA: 0x000E2C86 File Offset: 0x000E0E86
		public EventDescriptorCollection(EventDescriptor[] events, bool readOnly)
			: this(events)
		{
			this.readOnly = readOnly;
		}

		// Token: 0x060032FE RID: 13054 RVA: 0x000E2C98 File Offset: 0x000E0E98
		private EventDescriptorCollection(EventDescriptor[] events, int eventCount, string[] namedSort, IComparer comparer)
		{
			this.eventsOwned = false;
			if (namedSort != null)
			{
				this.namedSort = (string[])namedSort.Clone();
			}
			this.comparer = comparer;
			this.events = events;
			this.eventCount = eventCount;
			this.needSort = true;
		}

		/// <summary>Gets the number of event descriptors in the collection.</summary>
		/// <returns>The number of event descriptors in the collection.</returns>
		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x060032FF RID: 13055 RVA: 0x000E2CEA File Offset: 0x000E0EEA
		public int Count
		{
			get
			{
				return this.eventCount;
			}
		}

		/// <summary>Gets or sets the event with the specified index number.</summary>
		/// <param name="index">The zero-based index number of the <see cref="T:System.ComponentModel.EventDescriptor" /> to get or set.</param>
		/// <returns>The <see cref="T:System.ComponentModel.EventDescriptor" /> with the specified index number.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is not a valid index for <see cref="P:System.ComponentModel.EventDescriptorCollection.Item(System.Int32)" />.</exception>
		// Token: 0x17000C77 RID: 3191
		public virtual EventDescriptor this[int index]
		{
			get
			{
				if (index >= this.eventCount)
				{
					throw new IndexOutOfRangeException();
				}
				this.EnsureEventsOwned();
				return this.events[index];
			}
		}

		/// <summary>Gets or sets the event with the specified name.</summary>
		/// <param name="name">The name of the <see cref="T:System.ComponentModel.EventDescriptor" /> to get or set.</param>
		/// <returns>The <see cref="T:System.ComponentModel.EventDescriptor" /> with the specified name, or <see langword="null" /> if the event does not exist.</returns>
		// Token: 0x17000C78 RID: 3192
		public virtual EventDescriptor this[string name]
		{
			get
			{
				return this.Find(name, false);
			}
		}

		/// <summary>Adds an <see cref="T:System.ComponentModel.EventDescriptor" /> to the end of the collection.</summary>
		/// <param name="value">An <see cref="T:System.ComponentModel.EventDescriptor" /> to add to the collection.</param>
		/// <returns>The position of the <see cref="T:System.ComponentModel.EventDescriptor" /> within the collection.</returns>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06003302 RID: 13058 RVA: 0x000E2D1C File Offset: 0x000E0F1C
		public int Add(EventDescriptor value)
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			this.EnsureSize(this.eventCount + 1);
			EventDescriptor[] array = this.events;
			int num = this.eventCount;
			this.eventCount = num + 1;
			array[num] = value;
			return this.eventCount - 1;
		}

		/// <summary>Removes all objects from the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06003303 RID: 13059 RVA: 0x000E2D66 File Offset: 0x000E0F66
		public void Clear()
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			this.eventCount = 0;
		}

		/// <summary>Returns whether the collection contains the given <see cref="T:System.ComponentModel.EventDescriptor" />.</summary>
		/// <param name="value">The <see cref="T:System.ComponentModel.EventDescriptor" /> to find within the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the collection contains the <paramref name="value" /> parameter given; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003304 RID: 13060 RVA: 0x000E2D7D File Offset: 0x000E0F7D
		public bool Contains(EventDescriptor value)
		{
			return this.IndexOf(value) >= 0;
		}

		/// <summary>Copies the elements of the collection to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from collection. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x06003305 RID: 13061 RVA: 0x000E2D8C File Offset: 0x000E0F8C
		void ICollection.CopyTo(Array array, int index)
		{
			this.EnsureEventsOwned();
			Array.Copy(this.events, 0, array, index, this.Count);
		}

		// Token: 0x06003306 RID: 13062 RVA: 0x000E2DA8 File Offset: 0x000E0FA8
		private void EnsureEventsOwned()
		{
			if (!this.eventsOwned)
			{
				this.eventsOwned = true;
				if (this.events != null)
				{
					EventDescriptor[] array = new EventDescriptor[this.Count];
					Array.Copy(this.events, 0, array, 0, this.Count);
					this.events = array;
				}
			}
			if (this.needSort)
			{
				this.needSort = false;
				this.InternalSort(this.namedSort);
			}
		}

		// Token: 0x06003307 RID: 13063 RVA: 0x000E2E10 File Offset: 0x000E1010
		private void EnsureSize(int sizeNeeded)
		{
			if (sizeNeeded <= this.events.Length)
			{
				return;
			}
			if (this.events == null || this.events.Length == 0)
			{
				this.eventCount = 0;
				this.events = new EventDescriptor[sizeNeeded];
				return;
			}
			this.EnsureEventsOwned();
			int num = Math.Max(sizeNeeded, this.events.Length * 2);
			EventDescriptor[] array = new EventDescriptor[num];
			Array.Copy(this.events, 0, array, 0, this.eventCount);
			this.events = array;
		}

		/// <summary>Gets the description of the event with the specified name in the collection.</summary>
		/// <param name="name">The name of the event to get from the collection.</param>
		/// <param name="ignoreCase">
		///   <see langword="true" /> if you want to ignore the case of the event; otherwise, <see langword="false" />.</param>
		/// <returns>The <see cref="T:System.ComponentModel.EventDescriptor" /> with the specified name, or <see langword="null" /> if the event does not exist.</returns>
		// Token: 0x06003308 RID: 13064 RVA: 0x000E2E88 File Offset: 0x000E1088
		public virtual EventDescriptor Find(string name, bool ignoreCase)
		{
			EventDescriptor eventDescriptor = null;
			if (ignoreCase)
			{
				for (int i = 0; i < this.Count; i++)
				{
					if (string.Equals(this.events[i].Name, name, StringComparison.OrdinalIgnoreCase))
					{
						eventDescriptor = this.events[i];
						break;
					}
				}
			}
			else
			{
				for (int j = 0; j < this.Count; j++)
				{
					if (string.Equals(this.events[j].Name, name, StringComparison.Ordinal))
					{
						eventDescriptor = this.events[j];
						break;
					}
				}
			}
			return eventDescriptor;
		}

		/// <summary>Returns the index of the given <see cref="T:System.ComponentModel.EventDescriptor" />.</summary>
		/// <param name="value">The <see cref="T:System.ComponentModel.EventDescriptor" /> to find within the collection.</param>
		/// <returns>The index of the given <see cref="T:System.ComponentModel.EventDescriptor" /> within the collection.</returns>
		// Token: 0x06003309 RID: 13065 RVA: 0x000E2F01 File Offset: 0x000E1101
		public int IndexOf(EventDescriptor value)
		{
			return Array.IndexOf<EventDescriptor>(this.events, value, 0, this.eventCount);
		}

		/// <summary>Inserts an <see cref="T:System.ComponentModel.EventDescriptor" /> to the collection at a specified index.</summary>
		/// <param name="index">The index within the collection in which to insert the <paramref name="value" /> parameter.</param>
		/// <param name="value">An <see cref="T:System.ComponentModel.EventDescriptor" /> to insert into the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x0600330A RID: 13066 RVA: 0x000E2F18 File Offset: 0x000E1118
		public void Insert(int index, EventDescriptor value)
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			this.EnsureSize(this.eventCount + 1);
			if (index < this.eventCount)
			{
				Array.Copy(this.events, index, this.events, index + 1, this.eventCount - index);
			}
			this.events[index] = value;
			this.eventCount++;
		}

		/// <summary>Removes the specified <see cref="T:System.ComponentModel.EventDescriptor" /> from the collection.</summary>
		/// <param name="value">The <see cref="T:System.ComponentModel.EventDescriptor" /> to remove from the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x0600330B RID: 13067 RVA: 0x000E2F80 File Offset: 0x000E1180
		public void Remove(EventDescriptor value)
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

		/// <summary>Removes the <see cref="T:System.ComponentModel.EventDescriptor" /> at the specified index from the collection.</summary>
		/// <param name="index">The index of the <see cref="T:System.ComponentModel.EventDescriptor" /> to remove.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x0600330C RID: 13068 RVA: 0x000E2FB0 File Offset: 0x000E11B0
		public void RemoveAt(int index)
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			if (index < this.eventCount - 1)
			{
				Array.Copy(this.events, index + 1, this.events, index, this.eventCount - index - 1);
			}
			this.events[this.eventCount - 1] = null;
			this.eventCount--;
		}

		/// <summary>Gets an enumerator for this <see cref="T:System.ComponentModel.EventDescriptorCollection" />.</summary>
		/// <returns>An enumerator that implements <see cref="T:System.Collections.IEnumerator" />.</returns>
		// Token: 0x0600330D RID: 13069 RVA: 0x000E3013 File Offset: 0x000E1213
		public IEnumerator GetEnumerator()
		{
			if (this.events.Length == this.eventCount)
			{
				return this.events.GetEnumerator();
			}
			return new ArraySubsetEnumerator(this.events, this.eventCount);
		}

		/// <summary>Sorts the members of this <see cref="T:System.ComponentModel.EventDescriptorCollection" />, using the default sort for this collection, which is usually alphabetical.</summary>
		/// <returns>The new <see cref="T:System.ComponentModel.EventDescriptorCollection" />.</returns>
		// Token: 0x0600330E RID: 13070 RVA: 0x000E3042 File Offset: 0x000E1242
		public virtual EventDescriptorCollection Sort()
		{
			return new EventDescriptorCollection(this.events, this.eventCount, this.namedSort, this.comparer);
		}

		/// <summary>Sorts the members of this <see cref="T:System.ComponentModel.EventDescriptorCollection" />, given a specified sort order.</summary>
		/// <param name="names">An array of strings describing the order in which to sort the <see cref="T:System.ComponentModel.EventDescriptor" /> objects in the collection.</param>
		/// <returns>The new <see cref="T:System.ComponentModel.EventDescriptorCollection" />.</returns>
		// Token: 0x0600330F RID: 13071 RVA: 0x000E3061 File Offset: 0x000E1261
		public virtual EventDescriptorCollection Sort(string[] names)
		{
			return new EventDescriptorCollection(this.events, this.eventCount, names, this.comparer);
		}

		/// <summary>Sorts the members of this <see cref="T:System.ComponentModel.EventDescriptorCollection" />, given a specified sort order and an <see cref="T:System.Collections.IComparer" />.</summary>
		/// <param name="names">An array of strings describing the order in which to sort the <see cref="T:System.ComponentModel.EventDescriptor" /> objects in the collection.</param>
		/// <param name="comparer">An <see cref="T:System.Collections.IComparer" /> to use to sort the <see cref="T:System.ComponentModel.EventDescriptor" /> objects in this collection.</param>
		/// <returns>The new <see cref="T:System.ComponentModel.EventDescriptorCollection" />.</returns>
		// Token: 0x06003310 RID: 13072 RVA: 0x000E307B File Offset: 0x000E127B
		public virtual EventDescriptorCollection Sort(string[] names, IComparer comparer)
		{
			return new EventDescriptorCollection(this.events, this.eventCount, names, comparer);
		}

		/// <summary>Sorts the members of this <see cref="T:System.ComponentModel.EventDescriptorCollection" />, using the specified <see cref="T:System.Collections.IComparer" />.</summary>
		/// <param name="comparer">An <see cref="T:System.Collections.IComparer" /> to use to sort the <see cref="T:System.ComponentModel.EventDescriptor" /> objects in this collection.</param>
		/// <returns>The new <see cref="T:System.ComponentModel.EventDescriptorCollection" />.</returns>
		// Token: 0x06003311 RID: 13073 RVA: 0x000E3090 File Offset: 0x000E1290
		public virtual EventDescriptorCollection Sort(IComparer comparer)
		{
			return new EventDescriptorCollection(this.events, this.eventCount, this.namedSort, comparer);
		}

		/// <summary>Sorts the members of this <see cref="T:System.ComponentModel.EventDescriptorCollection" />. The specified order is applied first, followed by the default sort for this collection, which is usually alphabetical.</summary>
		/// <param name="names">An array of strings describing the order in which to sort the <see cref="T:System.ComponentModel.EventDescriptor" /> objects in this collection.</param>
		// Token: 0x06003312 RID: 13074 RVA: 0x000E30AC File Offset: 0x000E12AC
		protected void InternalSort(string[] names)
		{
			if (this.events == null || this.events.Length == 0)
			{
				return;
			}
			this.InternalSort(this.comparer);
			if (names != null && names.Length != 0)
			{
				ArrayList arrayList = new ArrayList(this.events);
				int num = 0;
				int num2 = this.events.Length;
				for (int i = 0; i < names.Length; i++)
				{
					for (int j = 0; j < num2; j++)
					{
						EventDescriptor eventDescriptor = (EventDescriptor)arrayList[j];
						if (eventDescriptor != null && eventDescriptor.Name.Equals(names[i]))
						{
							this.events[num++] = eventDescriptor;
							arrayList[j] = null;
							break;
						}
					}
				}
				for (int k = 0; k < num2; k++)
				{
					if (arrayList[k] != null)
					{
						this.events[num++] = (EventDescriptor)arrayList[k];
					}
				}
			}
		}

		/// <summary>Sorts the members of this <see cref="T:System.ComponentModel.EventDescriptorCollection" />, using the specified <see cref="T:System.Collections.IComparer" />.</summary>
		/// <param name="sorter">A comparer to use to sort the <see cref="T:System.ComponentModel.EventDescriptor" /> objects in this collection.</param>
		// Token: 0x06003313 RID: 13075 RVA: 0x000E3189 File Offset: 0x000E1389
		protected void InternalSort(IComparer sorter)
		{
			if (sorter == null)
			{
				TypeDescriptor.SortDescriptorArray(this);
				return;
			}
			Array.Sort(this.events, sorter);
		}

		/// <summary>Gets the number of elements contained in the collection.</summary>
		/// <returns>The number of elements contained in the collection.</returns>
		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x06003314 RID: 13076 RVA: 0x000E31A1 File Offset: 0x000E13A1
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is synchronized.</summary>
		/// <returns>
		///   <see langword="true" /> if access to the collection is synchronized; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x06003315 RID: 13077 RVA: 0x000E31A9 File Offset: 0x000E13A9
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
		/// <returns>An object that can be used to synchronize access to the collection.</returns>
		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x06003316 RID: 13078 RVA: 0x000E31AC File Offset: 0x000E13AC
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06003317 RID: 13079 RVA: 0x000E31AF File Offset: 0x000E13AF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <returns>The element at the specified index.</returns>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="index" /> is less than 0.  
		/// -or-  
		/// <paramref name="index" /> is equal to or greater than <see cref="P:System.ComponentModel.EventDescriptorCollection.Count" />.</exception>
		// Token: 0x17000C7C RID: 3196
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
				if (index >= this.eventCount)
				{
					throw new IndexOutOfRangeException();
				}
				this.EnsureEventsOwned();
				this.events[index] = (EventDescriptor)value;
			}
		}

		/// <summary>Adds an item to the collection.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to add to the collection.</param>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x0600331A RID: 13082 RVA: 0x000E31F3 File Offset: 0x000E13F3
		int IList.Add(object value)
		{
			return this.Add((EventDescriptor)value);
		}

		/// <summary>Removes all the items from the collection.</summary>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x0600331B RID: 13083 RVA: 0x000E3201 File Offset: 0x000E1401
		void IList.Clear()
		{
			this.Clear();
		}

		/// <summary>Determines whether the collection contains a specific value.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Object" /> is found in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600331C RID: 13084 RVA: 0x000E3209 File Offset: 0x000E1409
		bool IList.Contains(object value)
		{
			return this.Contains((EventDescriptor)value);
		}

		/// <summary>Determines the index of a specific item in the collection.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection.</param>
		/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
		// Token: 0x0600331D RID: 13085 RVA: 0x000E3217 File Offset: 0x000E1417
		int IList.IndexOf(object value)
		{
			return this.IndexOf((EventDescriptor)value);
		}

		/// <summary>Inserts an item to the collection at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to insert into the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x0600331E RID: 13086 RVA: 0x000E3225 File Offset: 0x000E1425
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (EventDescriptor)value);
		}

		/// <summary>Removes the first occurrence of a specific object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to remove from the collection.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x0600331F RID: 13087 RVA: 0x000E3234 File Offset: 0x000E1434
		void IList.Remove(object value)
		{
			this.Remove((EventDescriptor)value);
		}

		/// <summary>Removes the item at the specified index.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
		// Token: 0x06003320 RID: 13088 RVA: 0x000E3242 File Offset: 0x000E1442
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the collection is read-only; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x06003321 RID: 13089 RVA: 0x000E324B File Offset: 0x000E144B
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
		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x06003322 RID: 13090 RVA: 0x000E3253 File Offset: 0x000E1453
		bool IList.IsFixedSize
		{
			get
			{
				return this.readOnly;
			}
		}

		// Token: 0x04002996 RID: 10646
		private EventDescriptor[] events;

		// Token: 0x04002997 RID: 10647
		private string[] namedSort;

		// Token: 0x04002998 RID: 10648
		private IComparer comparer;

		// Token: 0x04002999 RID: 10649
		private bool eventsOwned = true;

		// Token: 0x0400299A RID: 10650
		private bool needSort;

		// Token: 0x0400299B RID: 10651
		private int eventCount;

		// Token: 0x0400299C RID: 10652
		private bool readOnly;

		/// <summary>Specifies an empty collection to use, rather than creating a new one with no items. This <see langword="static" /> field is read-only.</summary>
		// Token: 0x0400299D RID: 10653
		public static readonly EventDescriptorCollection Empty = new EventDescriptorCollection(null, true);
	}
}
