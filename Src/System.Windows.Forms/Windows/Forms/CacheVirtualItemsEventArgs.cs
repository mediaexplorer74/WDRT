using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.CacheVirtualItems" /> event.</summary>
	// Token: 0x02000146 RID: 326
	public class CacheVirtualItemsEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.CacheVirtualItemsEventArgs" /> class with the specified starting and ending indices.</summary>
		/// <param name="startIndex">The starting index of a range of items needed by the <see cref="T:System.Windows.Forms.ListView" /> for the next <see cref="E:System.Windows.Forms.ListView.RetrieveVirtualItem" /> event that occurs.</param>
		/// <param name="endIndex">The ending index of a range of items needed by the <see cref="T:System.Windows.Forms.ListView" /> for the next <see cref="E:System.Windows.Forms.ListView.RetrieveVirtualItem" /> event that occurs.</param>
		// Token: 0x06000CD3 RID: 3283 RVA: 0x00024CF3 File Offset: 0x00022EF3
		public CacheVirtualItemsEventArgs(int startIndex, int endIndex)
		{
			this.startIndex = startIndex;
			this.endIndex = endIndex;
		}

		/// <summary>Gets the starting index for a range of values needed by a <see cref="T:System.Windows.Forms.ListView" /> control in virtual mode.</summary>
		/// <returns>The index at the start of the range of values needed by the <see cref="T:System.Windows.Forms.ListView" /> control.</returns>
		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x00024D09 File Offset: 0x00022F09
		public int StartIndex
		{
			get
			{
				return this.startIndex;
			}
		}

		/// <summary>Gets the ending index for the range of values needed by a <see cref="T:System.Windows.Forms.ListView" /> control in virtual mode.</summary>
		/// <returns>The index at the end of the range of values needed by the <see cref="T:System.Windows.Forms.ListView" /> control.</returns>
		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x00024D11 File Offset: 0x00022F11
		public int EndIndex
		{
			get
			{
				return this.endIndex;
			}
		}

		// Token: 0x0400074A RID: 1866
		private int startIndex;

		// Token: 0x0400074B RID: 1867
		private int endIndex;
	}
}
