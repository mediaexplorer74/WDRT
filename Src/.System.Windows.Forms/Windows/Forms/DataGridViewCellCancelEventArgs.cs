using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for <see cref="E:System.Windows.Forms.DataGridView.CellBeginEdit" /> and <see cref="E:System.Windows.Forms.DataGridView.RowValidating" /> events.</summary>
	// Token: 0x020001A3 RID: 419
	public class DataGridViewCellCancelEventArgs : CancelEventArgs
	{
		// Token: 0x06001E01 RID: 7681 RVA: 0x0008E676 File Offset: 0x0008C876
		internal DataGridViewCellCancelEventArgs(DataGridViewCell dataGridViewCell)
			: this(dataGridViewCell.ColumnIndex, dataGridViewCell.RowIndex)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellCancelEventArgs" /> class.</summary>
		/// <param name="columnIndex">The index of the column containing the cell that the event occurs for.</param>
		/// <param name="rowIndex">The index of the row containing the cell that the event occurs for.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="columnIndex" /> is less than -1.  
		/// -or-  
		/// <paramref name="rowIndex" /> is less than -1.</exception>
		// Token: 0x06001E02 RID: 7682 RVA: 0x0008E68A File Offset: 0x0008C88A
		public DataGridViewCellCancelEventArgs(int columnIndex, int rowIndex)
		{
			if (columnIndex < -1)
			{
				throw new ArgumentOutOfRangeException("columnIndex");
			}
			if (rowIndex < -1)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			this.columnIndex = columnIndex;
			this.rowIndex = rowIndex;
		}

		/// <summary>Gets the column index of the cell that the event occurs for.</summary>
		/// <returns>The column index of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that the event occurs for.</returns>
		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06001E03 RID: 7683 RVA: 0x0008E6BE File Offset: 0x0008C8BE
		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
		}

		/// <summary>Gets the row index of the cell that the event occurs for.</summary>
		/// <returns>The row index of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that the event occurs for.</returns>
		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001E04 RID: 7684 RVA: 0x0008E6C6 File Offset: 0x0008C8C6
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x04000CAF RID: 3247
		private int columnIndex;

		// Token: 0x04000CB0 RID: 3248
		private int rowIndex;
	}
}
