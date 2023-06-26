using System;
using System.Collections;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Represents a collection of <see cref="T:System.ComponentModel.ListSortDescription" /> objects.</summary>
	// Token: 0x02000588 RID: 1416
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class ListSortDescriptionCollection : IList, ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListSortDescriptionCollection" /> class.</summary>
		// Token: 0x0600342E RID: 13358 RVA: 0x000E4460 File Offset: 0x000E2660
		public ListSortDescriptionCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListSortDescriptionCollection" /> class with the specified array of <see cref="T:System.ComponentModel.ListSortDescription" /> objects.</summary>
		/// <param name="sorts">The array of <see cref="T:System.ComponentModel.ListSortDescription" /> objects to be contained in the collection.</param>
		// Token: 0x0600342F RID: 13359 RVA: 0x000E4474 File Offset: 0x000E2674
		public ListSortDescriptionCollection(ListSortDescription[] sorts)
		{
			if (sorts != null)
			{
				for (int i = 0; i < sorts.Length; i++)
				{
					this.sorts.Add(sorts[i]);
				}
			}
		}

		/// <summary>Gets or sets the specified <see cref="T:System.ComponentModel.ListSortDescription" />.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.ComponentModel.ListSortDescription" /> to get or set in the collection.</param>
		/// <returns>The <see cref="T:System.ComponentModel.ListSortDescription" /> with the specified index.</returns>
		/// <exception cref="T:System.InvalidOperationException">An item is set in the <see cref="T:System.ComponentModel.ListSortDescriptionCollection" />, which is read-only.</exception>
		// Token: 0x17000CBE RID: 3262
		public ListSortDescription this[int index]
		{
			get
			{
				return (ListSortDescription)this.sorts[index];
			}
			set
			{
				throw new InvalidOperationException(SR.GetString("CantModifyListSortDescriptionCollection"));
			}
		}

		/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
		/// <returns>
		///   <see langword="true" /> in all cases.</returns>
		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x06003432 RID: 13362 RVA: 0x000E44D6 File Offset: 0x000E26D6
		bool IList.IsFixedSize
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether the collection is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> in all cases.</returns>
		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x06003433 RID: 13363 RVA: 0x000E44D9 File Offset: 0x000E26D9
		bool IList.IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the specified <see cref="T:System.ComponentModel.ListSortDescription" />.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.ComponentModel.ListSortDescription" /> to get in the collection</param>
		/// <returns>The <see cref="T:System.ComponentModel.ListSortDescription" /> with the specified index.</returns>
		// Token: 0x17000CC1 RID: 3265
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new InvalidOperationException(SR.GetString("CantModifyListSortDescriptionCollection"));
			}
		}

		/// <summary>Adds an item to the collection.</summary>
		/// <param name="value">The item to add to the collection.</param>
		/// <returns>The position into which the new element was inserted.</returns>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x06003436 RID: 13366 RVA: 0x000E44F6 File Offset: 0x000E26F6
		int IList.Add(object value)
		{
			throw new InvalidOperationException(SR.GetString("CantModifyListSortDescriptionCollection"));
		}

		/// <summary>Removes all items from the collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x06003437 RID: 13367 RVA: 0x000E4507 File Offset: 0x000E2707
		void IList.Clear()
		{
			throw new InvalidOperationException(SR.GetString("CantModifyListSortDescriptionCollection"));
		}

		/// <summary>Determines if the <see cref="T:System.ComponentModel.ListSortDescriptionCollection" /> contains a specific value.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Object" /> is found in the collection; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003438 RID: 13368 RVA: 0x000E4518 File Offset: 0x000E2718
		public bool Contains(object value)
		{
			return ((IList)this.sorts).Contains(value);
		}

		/// <summary>Returns the index of the specified item in the collection.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> to locate in the collection.</param>
		/// <returns>The index of <paramref name="value" /> if found in the list; otherwise, -1.</returns>
		// Token: 0x06003439 RID: 13369 RVA: 0x000E4526 File Offset: 0x000E2726
		public int IndexOf(object value)
		{
			return ((IList)this.sorts).IndexOf(value);
		}

		/// <summary>Inserts an item into the collection at a specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.ComponentModel.ListSortDescription" /> to get or set in the collection</param>
		/// <param name="value">The item to insert into the collection.</param>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x0600343A RID: 13370 RVA: 0x000E4534 File Offset: 0x000E2734
		void IList.Insert(int index, object value)
		{
			throw new InvalidOperationException(SR.GetString("CantModifyListSortDescriptionCollection"));
		}

		/// <summary>Removes the first occurrence of an item from the collection.</summary>
		/// <param name="value">The item to remove from the collection.</param>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x0600343B RID: 13371 RVA: 0x000E4545 File Offset: 0x000E2745
		void IList.Remove(object value)
		{
			throw new InvalidOperationException(SR.GetString("CantModifyListSortDescriptionCollection"));
		}

		/// <summary>Removes an item from the collection at a specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.ComponentModel.ListSortDescription" /> to remove from the collection</param>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x0600343C RID: 13372 RVA: 0x000E4556 File Offset: 0x000E2756
		void IList.RemoveAt(int index)
		{
			throw new InvalidOperationException(SR.GetString("CantModifyListSortDescriptionCollection"));
		}

		/// <summary>Gets the number of items in the collection.</summary>
		/// <returns>The number of items in the collection.</returns>
		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x0600343D RID: 13373 RVA: 0x000E4567 File Offset: 0x000E2767
		public int Count
		{
			get
			{
				return this.sorts.Count;
			}
		}

		/// <summary>Gets a value indicating whether access to the collection is thread safe.</summary>
		/// <returns>
		///   <see langword="true" /> in all cases.</returns>
		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x0600343E RID: 13374 RVA: 0x000E4574 File Offset: 0x000E2774
		bool ICollection.IsSynchronized
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the current instance that can be used to synchronize access to the collection.</summary>
		/// <returns>The current instance of the <see cref="T:System.ComponentModel.ListSortDescriptionCollection" />.</returns>
		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x0600343F RID: 13375 RVA: 0x000E4577 File Offset: 0x000E2777
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Copies the contents of the collection to the specified array, starting at the specified destination array index.</summary>
		/// <param name="array">The destination array for the items copied from the collection.</param>
		/// <param name="index">The index of the destination array at which copying begins.</param>
		// Token: 0x06003440 RID: 13376 RVA: 0x000E457A File Offset: 0x000E277A
		public void CopyTo(Array array, int index)
		{
			this.sorts.CopyTo(array, index);
		}

		/// <summary>Gets a <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06003441 RID: 13377 RVA: 0x000E4589 File Offset: 0x000E2789
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.sorts.GetEnumerator();
		}

		// Token: 0x040029D0 RID: 10704
		private ArrayList sorts = new ArrayList();
	}
}
