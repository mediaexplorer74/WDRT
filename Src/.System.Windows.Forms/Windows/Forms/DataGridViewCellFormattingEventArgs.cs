using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.CellFormatting" /> event of a <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
	// Token: 0x020001A9 RID: 425
	public class DataGridViewCellFormattingEventArgs : ConvertEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs" /> class.</summary>
		/// <param name="columnIndex">The column index of the cell that caused the event.</param>
		/// <param name="rowIndex">The row index of the cell that caused the event.</param>
		/// <param name="value">The cell's contents.</param>
		/// <param name="desiredType">The type to convert <paramref name="value" /> to.</param>
		/// <param name="cellStyle">The style of the cell that caused the event.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="columnIndex" /> is less than -1  
		/// -or-  
		/// <paramref name="rowIndex" /> is less than -1.</exception>
		// Token: 0x06001E38 RID: 7736 RVA: 0x0008EF19 File Offset: 0x0008D119
		public DataGridViewCellFormattingEventArgs(int columnIndex, int rowIndex, object value, Type desiredType, DataGridViewCellStyle cellStyle)
			: base(value, desiredType)
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
			this.cellStyle = cellStyle;
		}

		/// <summary>Gets or sets the style of the cell that is being formatted.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the display style of the cell being formatted. The default is the value of the cell's <see cref="P:System.Windows.Forms.DataGridViewCell.InheritedStyle" /> property.</returns>
		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001E39 RID: 7737 RVA: 0x0008EF58 File Offset: 0x0008D158
		// (set) Token: 0x06001E3A RID: 7738 RVA: 0x0008EF60 File Offset: 0x0008D160
		public DataGridViewCellStyle CellStyle
		{
			get
			{
				return this.cellStyle;
			}
			set
			{
				this.cellStyle = value;
			}
		}

		/// <summary>Gets the column index of the cell that is being formatted.</summary>
		/// <returns>The column index of the cell that is being formatted.</returns>
		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001E3B RID: 7739 RVA: 0x0008EF69 File Offset: 0x0008D169
		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
		}

		/// <summary>Gets or sets a value indicating whether the cell value has been successfully formatted.</summary>
		/// <returns>
		///   <see langword="true" /> if the formatting for the cell value has been handled; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001E3C RID: 7740 RVA: 0x0008EF71 File Offset: 0x0008D171
		// (set) Token: 0x06001E3D RID: 7741 RVA: 0x0008EF79 File Offset: 0x0008D179
		public bool FormattingApplied
		{
			get
			{
				return this.formattingApplied;
			}
			set
			{
				this.formattingApplied = value;
			}
		}

		/// <summary>Gets the row index of the cell that is being formatted.</summary>
		/// <returns>The row index of the cell that is being formatted.</returns>
		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001E3E RID: 7742 RVA: 0x0008EF82 File Offset: 0x0008D182
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x04000CB8 RID: 3256
		private int columnIndex;

		// Token: 0x04000CB9 RID: 3257
		private int rowIndex;

		// Token: 0x04000CBA RID: 3258
		private DataGridViewCellStyle cellStyle;

		// Token: 0x04000CBB RID: 3259
		private bool formattingApplied;
	}
}
