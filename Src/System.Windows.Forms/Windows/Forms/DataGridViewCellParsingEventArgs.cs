using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.CellParsing" /> event of a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	// Token: 0x020001AF RID: 431
	public class DataGridViewCellParsingEventArgs : ConvertEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellParsingEventArgs" /> class.</summary>
		/// <param name="rowIndex">The row index of the cell that was changed.</param>
		/// <param name="columnIndex">The column index of the cell that was changed.</param>
		/// <param name="value">The new value.</param>
		/// <param name="desiredType">The type of the new value.</param>
		/// <param name="inheritedCellStyle">The style applied to the cell that was changed.</param>
		// Token: 0x06001E66 RID: 7782 RVA: 0x0008F6D9 File Offset: 0x0008D8D9
		public DataGridViewCellParsingEventArgs(int rowIndex, int columnIndex, object value, Type desiredType, DataGridViewCellStyle inheritedCellStyle)
			: base(value, desiredType)
		{
			this.rowIndex = rowIndex;
			this.columnIndex = columnIndex;
			this.inheritedCellStyle = inheritedCellStyle;
		}

		/// <summary>Gets the row index of the cell that requires parsing.</summary>
		/// <returns>The row index of the cell that was changed.</returns>
		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001E67 RID: 7783 RVA: 0x0008F6FA File Offset: 0x0008D8FA
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		/// <summary>Gets the column index of the cell data that requires parsing.</summary>
		/// <returns>The column index of the cell that was changed.</returns>
		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001E68 RID: 7784 RVA: 0x0008F702 File Offset: 0x0008D902
		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
		}

		/// <summary>Gets or sets the style applied to the edited cell.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the current style of the cell being edited. The default value is the value of the cell <see cref="P:System.Windows.Forms.DataGridViewCell.InheritedStyle" /> property.</returns>
		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001E69 RID: 7785 RVA: 0x0008F70A File Offset: 0x0008D90A
		// (set) Token: 0x06001E6A RID: 7786 RVA: 0x0008F712 File Offset: 0x0008D912
		public DataGridViewCellStyle InheritedCellStyle
		{
			get
			{
				return this.inheritedCellStyle;
			}
			set
			{
				this.inheritedCellStyle = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether a cell's value has been successfully parsed.</summary>
		/// <returns>
		///   <see langword="true" /> if the cell's value has been successfully parsed; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001E6B RID: 7787 RVA: 0x0008F71B File Offset: 0x0008D91B
		// (set) Token: 0x06001E6C RID: 7788 RVA: 0x0008F723 File Offset: 0x0008D923
		public bool ParsingApplied
		{
			get
			{
				return this.parsingApplied;
			}
			set
			{
				this.parsingApplied = value;
			}
		}

		// Token: 0x04000CD4 RID: 3284
		private int rowIndex;

		// Token: 0x04000CD5 RID: 3285
		private int columnIndex;

		// Token: 0x04000CD6 RID: 3286
		private DataGridViewCellStyle inheritedCellStyle;

		// Token: 0x04000CD7 RID: 3287
		private bool parsingApplied;
	}
}
