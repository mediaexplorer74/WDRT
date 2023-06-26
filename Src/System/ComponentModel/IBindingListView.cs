using System;
using System.Collections;

namespace System.ComponentModel
{
	/// <summary>Extends the <see cref="T:System.ComponentModel.IBindingList" /> interface by providing advanced sorting and filtering capabilities.</summary>
	// Token: 0x02000558 RID: 1368
	public interface IBindingListView : IBindingList, IList, ICollection, IEnumerable
	{
		/// <summary>Sorts the data source based on the given <see cref="T:System.ComponentModel.ListSortDescriptionCollection" />.</summary>
		/// <param name="sorts">The <see cref="T:System.ComponentModel.ListSortDescriptionCollection" /> containing the sorts to apply to the data source.</param>
		// Token: 0x06003361 RID: 13153
		void ApplySort(ListSortDescriptionCollection sorts);

		/// <summary>Gets or sets the filter to be used to exclude items from the collection of items returned by the data source</summary>
		/// <returns>The string used to filter items out in the item collection returned by the data source.</returns>
		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x06003362 RID: 13154
		// (set) Token: 0x06003363 RID: 13155
		string Filter { get; set; }

		/// <summary>Gets the collection of sort descriptions currently applied to the data source.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ListSortDescriptionCollection" /> currently applied to the data source.</returns>
		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x06003364 RID: 13156
		ListSortDescriptionCollection SortDescriptions { get; }

		/// <summary>Removes the current filter applied to the data source.</summary>
		// Token: 0x06003365 RID: 13157
		void RemoveFilter();

		/// <summary>Gets a value indicating whether the data source supports advanced sorting.</summary>
		/// <returns>
		///   <see langword="true" /> if the data source supports advanced sorting; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x06003366 RID: 13158
		bool SupportsAdvancedSorting { get; }

		/// <summary>Gets a value indicating whether the data source supports filtering.</summary>
		/// <returns>
		///   <see langword="true" /> if the data source supports filtering; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x06003367 RID: 13159
		bool SupportsFiltering { get; }
	}
}
