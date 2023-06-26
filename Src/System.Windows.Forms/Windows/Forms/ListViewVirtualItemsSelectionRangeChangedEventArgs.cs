using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.VirtualItemsSelectionRangeChanged" /> event.</summary>
	// Token: 0x020002E1 RID: 737
	public class ListViewVirtualItemsSelectionRangeChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewVirtualItemsSelectionRangeChangedEventArgs" /> class.</summary>
		/// <param name="startIndex">The index of the first item in the range that has changed.</param>
		/// <param name="endIndex">The index of the last item in the range that has changed.</param>
		/// <param name="isSelected">
		///   <see langword="true" /> to indicate the items are selected; <see langword="false" /> to indicate the items are deselected.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="startIndex" /> is larger than <paramref name="endIndex." /></exception>
		// Token: 0x06002EA3 RID: 11939 RVA: 0x000D315D File Offset: 0x000D135D
		public ListViewVirtualItemsSelectionRangeChangedEventArgs(int startIndex, int endIndex, bool isSelected)
		{
			if (startIndex > endIndex)
			{
				throw new ArgumentException(SR.GetString("ListViewStartIndexCannotBeLargerThanEndIndex"));
			}
			this.startIndex = startIndex;
			this.endIndex = endIndex;
			this.isSelected = isSelected;
		}

		/// <summary>Gets the index for the last item in the range of items whose selection state has changed</summary>
		/// <returns>The index of the last item in the range of items whose selection state has changed.</returns>
		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06002EA4 RID: 11940 RVA: 0x000D318E File Offset: 0x000D138E
		public int EndIndex
		{
			get
			{
				return this.endIndex;
			}
		}

		/// <summary>Gets a value indicating whether the range of items is selected.</summary>
		/// <returns>
		///   <see langword="true" /> if the range of items is selected; <see langword="false" /> if the range of items is deselected.</returns>
		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06002EA5 RID: 11941 RVA: 0x000D3196 File Offset: 0x000D1396
		public bool IsSelected
		{
			get
			{
				return this.isSelected;
			}
		}

		/// <summary>Gets the index for the first item in the range of items whose selection state has changed.</summary>
		/// <returns>The index of the first item in the range of items whose selection state has changed.</returns>
		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06002EA6 RID: 11942 RVA: 0x000D319E File Offset: 0x000D139E
		public int StartIndex
		{
			get
			{
				return this.startIndex;
			}
		}

		// Token: 0x0400133F RID: 4927
		private int startIndex;

		// Token: 0x04001340 RID: 4928
		private int endIndex;

		// Token: 0x04001341 RID: 4929
		private bool isSelected;
	}
}
