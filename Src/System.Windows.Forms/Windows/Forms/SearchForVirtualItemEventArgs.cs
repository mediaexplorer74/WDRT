using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.SearchForVirtualItem" /> event.</summary>
	// Token: 0x0200035F RID: 863
	public class SearchForVirtualItemEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.SearchForVirtualItemEventArgs" /> class.</summary>
		/// <param name="isTextSearch">A value indicating whether the search is a text search.</param>
		/// <param name="isPrefixSearch">A value indicating whether the search is a prefix search.</param>
		/// <param name="includeSubItemsInSearch">A value indicating whether to include subitems of list items in the search.</param>
		/// <param name="text">The text of the item to search for.</param>
		/// <param name="startingPoint">The <see cref="T:System.Drawing.Point" /> at which to start the search.</param>
		/// <param name="direction">One of the <see cref="T:System.Windows.Forms.SearchDirectionHint" /> values.</param>
		/// <param name="startIndex">The index of the <see cref="T:System.Windows.Forms.ListViewItem" /> at which to start the search.</param>
		// Token: 0x0600386E RID: 14446 RVA: 0x000FA790 File Offset: 0x000F8990
		public SearchForVirtualItemEventArgs(bool isTextSearch, bool isPrefixSearch, bool includeSubItemsInSearch, string text, Point startingPoint, SearchDirectionHint direction, int startIndex)
		{
			this.isTextSearch = isTextSearch;
			this.isPrefixSearch = isPrefixSearch;
			this.includeSubItemsInSearch = includeSubItemsInSearch;
			this.text = text;
			this.startingPoint = startingPoint;
			this.direction = direction;
			this.startIndex = startIndex;
		}

		/// <summary>Gets a value indicating whether the search is a text search.</summary>
		/// <returns>
		///   <see langword="true" /> if the search is a text search; <see langword="false" /> if the search is a location search.</returns>
		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x0600386F RID: 14447 RVA: 0x000FA7DF File Offset: 0x000F89DF
		public bool IsTextSearch
		{
			get
			{
				return this.isTextSearch;
			}
		}

		/// <summary>Gets a value indicating whether the search should include subitems of list items.</summary>
		/// <returns>
		///   <see langword="true" /> if subitems should be included in the search; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x06003870 RID: 14448 RVA: 0x000FA7E7 File Offset: 0x000F89E7
		public bool IncludeSubItemsInSearch
		{
			get
			{
				return this.includeSubItemsInSearch;
			}
		}

		/// <summary>Gets or sets the index of the <see cref="T:System.Windows.Forms.ListViewItem" /> found in the <see cref="T:System.Windows.Forms.ListView" /> .</summary>
		/// <returns>The index of the <see cref="T:System.Windows.Forms.ListViewItem" /> found in the <see cref="T:System.Windows.Forms.ListView" />.</returns>
		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x06003871 RID: 14449 RVA: 0x000FA7EF File Offset: 0x000F89EF
		// (set) Token: 0x06003872 RID: 14450 RVA: 0x000FA7F7 File Offset: 0x000F89F7
		public int Index
		{
			get
			{
				return this.index;
			}
			set
			{
				this.index = value;
			}
		}

		/// <summary>Gets a value indicating whether the search should return an item if its text starts with the search text.</summary>
		/// <returns>
		///   <see langword="true" /> if the search should match item text that starts with the search text; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x06003873 RID: 14451 RVA: 0x000FA800 File Offset: 0x000F8A00
		public bool IsPrefixSearch
		{
			get
			{
				return this.isPrefixSearch;
			}
		}

		/// <summary>Gets the text used to find an item in the <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		/// <returns>The text used to find an item in the <see cref="T:System.Windows.Forms.ListView" /> control.</returns>
		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x06003874 RID: 14452 RVA: 0x000FA808 File Offset: 0x000F8A08
		public string Text
		{
			get
			{
				return this.text;
			}
		}

		/// <summary>Gets the starting location of the search.</summary>
		/// <returns>The <see cref="T:System.Drawing.Point" /> that indicates the starting location of the search.</returns>
		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x06003875 RID: 14453 RVA: 0x000FA810 File Offset: 0x000F8A10
		public Point StartingPoint
		{
			get
			{
				return this.startingPoint;
			}
		}

		/// <summary>Gets the direction from the current item that the search should take place.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.SearchDirectionHint" /> values.</returns>
		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x06003876 RID: 14454 RVA: 0x000FA818 File Offset: 0x000F8A18
		public SearchDirectionHint Direction
		{
			get
			{
				return this.direction;
			}
		}

		/// <summary>Gets the index of the <see cref="T:System.Windows.Forms.ListViewItem" /> where the search starts.</summary>
		/// <returns>The index of the <see cref="T:System.Windows.Forms.ListViewItem" /> indicating where the search starts</returns>
		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x06003877 RID: 14455 RVA: 0x000FA820 File Offset: 0x000F8A20
		public int StartIndex
		{
			get
			{
				return this.startIndex;
			}
		}

		// Token: 0x040021B1 RID: 8625
		private bool isTextSearch;

		// Token: 0x040021B2 RID: 8626
		private bool isPrefixSearch;

		// Token: 0x040021B3 RID: 8627
		private bool includeSubItemsInSearch;

		// Token: 0x040021B4 RID: 8628
		private string text;

		// Token: 0x040021B5 RID: 8629
		private Point startingPoint;

		// Token: 0x040021B6 RID: 8630
		private SearchDirectionHint direction;

		// Token: 0x040021B7 RID: 8631
		private int startIndex;

		// Token: 0x040021B8 RID: 8632
		private int index = -1;
	}
}
