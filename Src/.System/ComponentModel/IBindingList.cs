using System;
using System.Collections;

namespace System.ComponentModel
{
	/// <summary>Provides the features required to support both complex and simple scenarios when binding to a data source.</summary>
	// Token: 0x02000557 RID: 1367
	public interface IBindingList : IList, ICollection, IEnumerable
	{
		/// <summary>Gets whether you can add items to the list using <see cref="M:System.ComponentModel.IBindingList.AddNew" />.</summary>
		/// <returns>
		///   <see langword="true" /> if you can add items to the list using <see cref="M:System.ComponentModel.IBindingList.AddNew" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x06003350 RID: 13136
		bool AllowNew { get; }

		/// <summary>Adds a new item to the list.</summary>
		/// <returns>The item added to the list.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.ComponentModel.IBindingList.AllowNew" /> is <see langword="false" />.</exception>
		// Token: 0x06003351 RID: 13137
		object AddNew();

		/// <summary>Gets whether you can update items in the list.</summary>
		/// <returns>
		///   <see langword="true" /> if you can update the items in the list; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x06003352 RID: 13138
		bool AllowEdit { get; }

		/// <summary>Gets whether you can remove items from the list, using <see cref="M:System.Collections.IList.Remove(System.Object)" /> or <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" />.</summary>
		/// <returns>
		///   <see langword="true" /> if you can remove items from the list; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x06003353 RID: 13139
		bool AllowRemove { get; }

		/// <summary>Gets whether a <see cref="E:System.ComponentModel.IBindingList.ListChanged" /> event is raised when the list changes or an item in the list changes.</summary>
		/// <returns>
		///   <see langword="true" /> if a <see cref="E:System.ComponentModel.IBindingList.ListChanged" /> event is raised when the list changes or when an item changes; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x06003354 RID: 13140
		bool SupportsChangeNotification { get; }

		/// <summary>Gets whether the list supports searching using the <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)" /> method.</summary>
		/// <returns>
		///   <see langword="true" /> if the list supports searching using the <see cref="M:System.ComponentModel.IBindingList.Find(System.ComponentModel.PropertyDescriptor,System.Object)" /> method; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x06003355 RID: 13141
		bool SupportsSearching { get; }

		/// <summary>Gets whether the list supports sorting.</summary>
		/// <returns>
		///   <see langword="true" /> if the list supports sorting; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x06003356 RID: 13142
		bool SupportsSorting { get; }

		/// <summary>Gets whether the items in the list are sorted.</summary>
		/// <returns>
		///   <see langword="true" /> if <see cref="M:System.ComponentModel.IBindingList.ApplySort(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)" /> has been called and <see cref="M:System.ComponentModel.IBindingList.RemoveSort" /> has not been called; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" /> is <see langword="false" />.</exception>
		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x06003357 RID: 13143
		bool IsSorted { get; }

		/// <summary>Gets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> that is being used for sorting.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> that is being used for sorting.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" /> is <see langword="false" />.</exception>
		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x06003358 RID: 13144
		PropertyDescriptor SortProperty { get; }

		/// <summary>Gets the direction of the sort.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" /> is <see langword="false" />.</exception>
		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x06003359 RID: 13145
		ListSortDirection SortDirection { get; }

		/// <summary>Occurs when the list changes or an item in the list changes.</summary>
		// Token: 0x1400004B RID: 75
		// (add) Token: 0x0600335A RID: 13146
		// (remove) Token: 0x0600335B RID: 13147
		event ListChangedEventHandler ListChanged;

		/// <summary>Adds the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to the indexes used for searching.</summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to add to the indexes used for searching.</param>
		// Token: 0x0600335C RID: 13148
		void AddIndex(PropertyDescriptor property);

		/// <summary>Sorts the list based on a <see cref="T:System.ComponentModel.PropertyDescriptor" /> and a <see cref="T:System.ComponentModel.ListSortDirection" />.</summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to sort by.</param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values.</param>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" /> is <see langword="false" />.</exception>
		// Token: 0x0600335D RID: 13149
		void ApplySort(PropertyDescriptor property, ListSortDirection direction);

		/// <summary>Returns the index of the row that has the given <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to search on.</param>
		/// <param name="key">The value of the <paramref name="property" /> parameter to search for.</param>
		/// <returns>The index of the row that has the given <see cref="T:System.ComponentModel.PropertyDescriptor" />.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.ComponentModel.IBindingList.SupportsSearching" /> is <see langword="false" />.</exception>
		// Token: 0x0600335E RID: 13150
		int Find(PropertyDescriptor property, object key);

		/// <summary>Removes the <see cref="T:System.ComponentModel.PropertyDescriptor" /> from the indexes used for searching.</summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to remove from the indexes used for searching.</param>
		// Token: 0x0600335F RID: 13151
		void RemoveIndex(PropertyDescriptor property);

		/// <summary>Removes any sort applied using <see cref="M:System.ComponentModel.IBindingList.ApplySort(System.ComponentModel.PropertyDescriptor,System.ComponentModel.ListSortDirection)" />.</summary>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="P:System.ComponentModel.IBindingList.SupportsSorting" /> is <see langword="false" />.</exception>
		// Token: 0x06003360 RID: 13152
		void RemoveSort();
	}
}
