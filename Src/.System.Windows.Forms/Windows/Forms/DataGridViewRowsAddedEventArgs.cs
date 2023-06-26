using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.RowsAdded" /> event.</summary>
	// Token: 0x02000213 RID: 531
	public class DataGridViewRowsAddedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRowsAddedEventArgs" /> class.</summary>
		/// <param name="rowIndex">The index of the first added row.</param>
		/// <param name="rowCount">The number of rows that have been added.</param>
		// Token: 0x060022CB RID: 8907 RVA: 0x000A6EB9 File Offset: 0x000A50B9
		public DataGridViewRowsAddedEventArgs(int rowIndex, int rowCount)
		{
			this.rowIndex = rowIndex;
			this.rowCount = rowCount;
		}

		/// <summary>Gets the index of the first added row.</summary>
		/// <returns>The index of the first added row.</returns>
		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x060022CC RID: 8908 RVA: 0x000A6ECF File Offset: 0x000A50CF
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		/// <summary>Gets the number of rows that have been added.</summary>
		/// <returns>The number of rows that have been added.</returns>
		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x060022CD RID: 8909 RVA: 0x000A6ED7 File Offset: 0x000A50D7
		public int RowCount
		{
			get
			{
				return this.rowCount;
			}
		}

		// Token: 0x04000E56 RID: 3670
		private int rowIndex;

		// Token: 0x04000E57 RID: 3671
		private int rowCount;
	}
}
