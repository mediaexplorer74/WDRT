using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for mouse events raised by a <see cref="T:System.Windows.Forms.DataGridView" /> whenever the mouse is moved within a <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
	// Token: 0x020001AD RID: 429
	public class DataGridViewCellMouseEventArgs : MouseEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> class.</summary>
		/// <param name="columnIndex">The cell's zero-based column index.</param>
		/// <param name="rowIndex">The cell's zero-based row index.</param>
		/// <param name="localX">The x-coordinate of the mouse, in pixels.</param>
		/// <param name="localY">The y-coordinate of the mouse, in pixels.</param>
		/// <param name="e">The originating <see cref="T:System.Windows.Forms.MouseEventArgs" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="columnIndex" /> is less than -1.  
		/// -or-  
		/// <paramref name="rowIndex" /> is less than -1.</exception>
		// Token: 0x06001E51 RID: 7761 RVA: 0x0008F278 File Offset: 0x0008D478
		public DataGridViewCellMouseEventArgs(int columnIndex, int rowIndex, int localX, int localY, MouseEventArgs e)
			: base(e.Button, e.Clicks, localX, localY, e.Delta)
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

		/// <summary>Gets the zero-based column index of the cell.</summary>
		/// <returns>An integer specifying the column index.</returns>
		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001E52 RID: 7762 RVA: 0x0008F2CF File Offset: 0x0008D4CF
		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
		}

		/// <summary>Gets the zero-based row index of the cell.</summary>
		/// <returns>An integer specifying the row index.</returns>
		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001E53 RID: 7763 RVA: 0x0008F2D7 File Offset: 0x0008D4D7
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x04000CC5 RID: 3269
		private int rowIndex;

		// Token: 0x04000CC6 RID: 3270
		private int columnIndex;
	}
}
