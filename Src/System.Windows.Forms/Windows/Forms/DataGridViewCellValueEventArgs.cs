using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.CellValueNeeded" /> and <see cref="E:System.Windows.Forms.DataGridView.CellValuePushed" /> events of the <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	// Token: 0x020001B9 RID: 441
	public class DataGridViewCellValueEventArgs : EventArgs
	{
		// Token: 0x06001EB8 RID: 7864 RVA: 0x00090928 File Offset: 0x0008EB28
		internal DataGridViewCellValueEventArgs()
		{
			this.columnIndex = (this.rowIndex = -1);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellValueEventArgs" /> class.</summary>
		/// <param name="columnIndex">The index of the column containing the cell that the event occurs for.</param>
		/// <param name="rowIndex">The index of the row containing the cell that the event occurs for.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="columnIndex" /> is less than 0.  
		/// -or-  
		/// <paramref name="rowIndex" /> is less than 0.</exception>
		// Token: 0x06001EB9 RID: 7865 RVA: 0x0009094B File Offset: 0x0008EB4B
		public DataGridViewCellValueEventArgs(int columnIndex, int rowIndex)
		{
			if (columnIndex < 0)
			{
				throw new ArgumentOutOfRangeException("columnIndex");
			}
			if (rowIndex < 0)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			this.rowIndex = rowIndex;
			this.columnIndex = columnIndex;
		}

		/// <summary>Gets a value indicating the column index of the cell that the event occurs for.</summary>
		/// <returns>The index of the column containing the cell that the event occurs for.</returns>
		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001EBA RID: 7866 RVA: 0x0009097F File Offset: 0x0008EB7F
		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
		}

		/// <summary>Gets a value indicating the row index of the cell that the event occurs for.</summary>
		/// <returns>The index of the row containing the cell that the event occurs for.</returns>
		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06001EBB RID: 7867 RVA: 0x00090987 File Offset: 0x0008EB87
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		/// <summary>Gets or sets the value of the cell that the event occurs for.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the cell's value.</returns>
		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06001EBC RID: 7868 RVA: 0x0009098F File Offset: 0x0008EB8F
		// (set) Token: 0x06001EBD RID: 7869 RVA: 0x00090997 File Offset: 0x0008EB97
		public object Value
		{
			get
			{
				return this.val;
			}
			set
			{
				this.val = value;
			}
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x000909A0 File Offset: 0x0008EBA0
		internal void SetProperties(int columnIndex, int rowIndex, object value)
		{
			this.columnIndex = columnIndex;
			this.rowIndex = rowIndex;
			this.val = value;
		}

		// Token: 0x04000D00 RID: 3328
		private int rowIndex;

		// Token: 0x04000D01 RID: 3329
		private int columnIndex;

		// Token: 0x04000D02 RID: 3330
		private object val;
	}
}
