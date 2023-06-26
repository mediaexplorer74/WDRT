using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for <see cref="T:System.Windows.Forms.DataGridView" /> events related to cell and row operations.</summary>
	// Token: 0x020001A8 RID: 424
	public class DataGridViewCellEventArgs : EventArgs
	{
		// Token: 0x06001E34 RID: 7732 RVA: 0x0008EEC1 File Offset: 0x0008D0C1
		internal DataGridViewCellEventArgs(DataGridViewCell dataGridViewCell)
			: this(dataGridViewCell.ColumnIndex, dataGridViewCell.RowIndex)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> class.</summary>
		/// <param name="columnIndex">The index of the column containing the cell that the event occurs for.</param>
		/// <param name="rowIndex">The index of the row containing the cell that the event occurs for.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="columnIndex" /> is less than -1.  
		/// -or-  
		/// <paramref name="rowIndex" /> is less than -1.</exception>
		// Token: 0x06001E35 RID: 7733 RVA: 0x0008EED5 File Offset: 0x0008D0D5
		public DataGridViewCellEventArgs(int columnIndex, int rowIndex)
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

		/// <summary>Gets a value indicating the column index of the cell that the event occurs for.</summary>
		/// <returns>The index of the column containing the cell that the event occurs for.</returns>
		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001E36 RID: 7734 RVA: 0x0008EF09 File Offset: 0x0008D109
		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
		}

		/// <summary>Gets a value indicating the row index of the cell that the event occurs for.</summary>
		/// <returns>The index of the row containing the cell that the event occurs for.</returns>
		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001E37 RID: 7735 RVA: 0x0008EF11 File Offset: 0x0008D111
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x04000CB6 RID: 3254
		private int columnIndex;

		// Token: 0x04000CB7 RID: 3255
		private int rowIndex;
	}
}
