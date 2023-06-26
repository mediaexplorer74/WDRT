using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.SortCompare" /> event.</summary>
	// Token: 0x0200021A RID: 538
	public class DataGridViewSortCompareEventArgs : HandledEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewSortCompareEventArgs" /> class.</summary>
		/// <param name="dataGridViewColumn">The column to sort.</param>
		/// <param name="cellValue1">The value of the first cell to compare.</param>
		/// <param name="cellValue2">The value of the second cell to compare.</param>
		/// <param name="rowIndex1">The index of the row containing the first cell.</param>
		/// <param name="rowIndex2">The index of the row containing the second cell.</param>
		// Token: 0x0600231D RID: 8989 RVA: 0x000A721E File Offset: 0x000A541E
		public DataGridViewSortCompareEventArgs(DataGridViewColumn dataGridViewColumn, object cellValue1, object cellValue2, int rowIndex1, int rowIndex2)
		{
			this.dataGridViewColumn = dataGridViewColumn;
			this.cellValue1 = cellValue1;
			this.cellValue2 = cellValue2;
			this.rowIndex1 = rowIndex1;
			this.rowIndex2 = rowIndex2;
		}

		/// <summary>Gets the value of the first cell to compare.</summary>
		/// <returns>The value of the first cell.</returns>
		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x0600231E RID: 8990 RVA: 0x000A724B File Offset: 0x000A544B
		public object CellValue1
		{
			get
			{
				return this.cellValue1;
			}
		}

		/// <summary>Gets the value of the second cell to compare.</summary>
		/// <returns>The value of the second cell.</returns>
		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x0600231F RID: 8991 RVA: 0x000A7253 File Offset: 0x000A5453
		public object CellValue2
		{
			get
			{
				return this.cellValue2;
			}
		}

		/// <summary>Gets the column being sorted.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> to sort.</returns>
		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06002320 RID: 8992 RVA: 0x000A725B File Offset: 0x000A545B
		public DataGridViewColumn Column
		{
			get
			{
				return this.dataGridViewColumn;
			}
		}

		/// <summary>Gets the index of the row containing the first cell to compare.</summary>
		/// <returns>The index of the row containing the second cell.</returns>
		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06002321 RID: 8993 RVA: 0x000A7263 File Offset: 0x000A5463
		public int RowIndex1
		{
			get
			{
				return this.rowIndex1;
			}
		}

		/// <summary>Gets the index of the row containing the second cell to compare.</summary>
		/// <returns>The index of the row containing the second cell.</returns>
		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06002322 RID: 8994 RVA: 0x000A726B File Offset: 0x000A546B
		public int RowIndex2
		{
			get
			{
				return this.rowIndex2;
			}
		}

		/// <summary>Gets or sets a value indicating the order in which the compared cells will be sorted.</summary>
		/// <returns>Less than zero if the first cell will be sorted before the second cell; zero if the first cell and second cell have equivalent values; greater than zero if the second cell will be sorted before the first cell.</returns>
		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06002323 RID: 8995 RVA: 0x000A7273 File Offset: 0x000A5473
		// (set) Token: 0x06002324 RID: 8996 RVA: 0x000A727B File Offset: 0x000A547B
		public int SortResult
		{
			get
			{
				return this.sortResult;
			}
			set
			{
				this.sortResult = value;
			}
		}

		// Token: 0x04000E65 RID: 3685
		private DataGridViewColumn dataGridViewColumn;

		// Token: 0x04000E66 RID: 3686
		private object cellValue1;

		// Token: 0x04000E67 RID: 3687
		private object cellValue2;

		// Token: 0x04000E68 RID: 3688
		private int sortResult;

		// Token: 0x04000E69 RID: 3689
		private int rowIndex1;

		// Token: 0x04000E6A RID: 3690
		private int rowIndex2;
	}
}
