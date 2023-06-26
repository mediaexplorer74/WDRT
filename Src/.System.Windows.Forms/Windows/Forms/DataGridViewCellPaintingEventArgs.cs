using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.CellPainting" /> event.</summary>
	// Token: 0x020001AE RID: 430
	public class DataGridViewCellPaintingEventArgs : HandledEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellPaintingEventArgs" /> class.</summary>
		/// <param name="dataGridView">The <see cref="T:System.Windows.Forms.DataGridView" /> that contains the cell to be painted.</param>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</param>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</param>
		/// <param name="cellBounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
		/// <param name="rowIndex">The row index of the cell that is being painted.</param>
		/// <param name="columnIndex">The column index of the cell that is being painted.</param>
		/// <param name="cellState">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that specifies the state of the cell.</param>
		/// <param name="value">The data of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
		/// <param name="formattedValue">The formatted data of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
		/// <param name="errorText">An error message that is associated with the cell.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that contains formatting and style information about the cell.</param>
		/// <param name="advancedBorderStyle">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that contains border styles for the cell that is being painted.</param>
		/// <param name="paintParts">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values specifying the parts to paint.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridView" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="graphics" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="cellStyle" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="paintParts" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values.</exception>
		// Token: 0x06001E54 RID: 7764 RVA: 0x0008F2E0 File Offset: 0x0008D4E0
		public DataGridViewCellPaintingEventArgs(DataGridView dataGridView, Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, int columnIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			if (dataGridView == null)
			{
				throw new ArgumentNullException("dataGridView");
			}
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			if (cellStyle == null)
			{
				throw new ArgumentNullException("cellStyle");
			}
			if ((paintParts & ~DataGridViewPaintParts.All) != DataGridViewPaintParts.None)
			{
				throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewPaintPartsCombination", new object[] { "paintParts" }));
			}
			this.graphics = graphics;
			this.clipBounds = clipBounds;
			this.cellBounds = cellBounds;
			this.rowIndex = rowIndex;
			this.columnIndex = columnIndex;
			this.cellState = cellState;
			this.value = value;
			this.formattedValue = formattedValue;
			this.errorText = errorText;
			this.cellStyle = cellStyle;
			this.advancedBorderStyle = advancedBorderStyle;
			this.paintParts = paintParts;
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x0008F3A1 File Offset: 0x0008D5A1
		internal DataGridViewCellPaintingEventArgs(DataGridView dataGridView)
		{
			this.dataGridView = dataGridView;
		}

		/// <summary>Gets the border style of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that represents the border style of the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001E56 RID: 7766 RVA: 0x0008F3B0 File Offset: 0x0008D5B0
		public DataGridViewAdvancedBorderStyle AdvancedBorderStyle
		{
			get
			{
				return this.advancedBorderStyle;
			}
		}

		/// <summary>Get the bounds of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001E57 RID: 7767 RVA: 0x0008F3B8 File Offset: 0x0008D5B8
		public Rectangle CellBounds
		{
			get
			{
				return this.cellBounds;
			}
		}

		/// <summary>Gets the cell style of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that contains the cell style of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001E58 RID: 7768 RVA: 0x0008F3C0 File Offset: 0x0008D5C0
		public DataGridViewCellStyle CellStyle
		{
			get
			{
				return this.cellStyle;
			}
		}

		/// <summary>Gets the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</returns>
		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001E59 RID: 7769 RVA: 0x0008F3C8 File Offset: 0x0008D5C8
		public Rectangle ClipBounds
		{
			get
			{
				return this.clipBounds;
			}
		}

		/// <summary>Gets the column index of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
		/// <returns>The column index of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001E5A RID: 7770 RVA: 0x0008F3D0 File Offset: 0x0008D5D0
		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
		}

		/// <summary>Gets a string that represents an error message for the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
		/// <returns>A string that represents an error message for the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001E5B RID: 7771 RVA: 0x0008F3D8 File Offset: 0x0008D5D8
		public string ErrorText
		{
			get
			{
				return this.errorText;
			}
		}

		/// <summary>Gets the formatted value of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
		/// <returns>The formatted value of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001E5C RID: 7772 RVA: 0x0008F3E0 File Offset: 0x0008D5E0
		public object FormattedValue
		{
			get
			{
				return this.formattedValue;
			}
		}

		/// <summary>Gets the <see cref="T:System.Drawing.Graphics" /> used to paint the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Graphics" /> used to paint the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001E5D RID: 7773 RVA: 0x0008F3E8 File Offset: 0x0008D5E8
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>The cell parts that are to be painted.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values specifying the parts to be painted.</returns>
		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001E5E RID: 7774 RVA: 0x0008F3F0 File Offset: 0x0008D5F0
		public DataGridViewPaintParts PaintParts
		{
			get
			{
				return this.paintParts;
			}
		}

		/// <summary>Gets the row index of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
		/// <returns>The row index of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001E5F RID: 7775 RVA: 0x0008F3F8 File Offset: 0x0008D5F8
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		/// <summary>Gets the state of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that specifies the state of the cell.</returns>
		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001E60 RID: 7776 RVA: 0x0008F400 File Offset: 0x0008D600
		public DataGridViewElementStates State
		{
			get
			{
				return this.cellState;
			}
		}

		/// <summary>Gets the value of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
		/// <returns>The value of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001E61 RID: 7777 RVA: 0x0008F408 File Offset: 0x0008D608
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		/// <summary>Paints the specified parts of the cell for the area in the specified bounds.</summary>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that specifies the area of the <see cref="T:System.Windows.Forms.DataGridView" /> to be painted.</param>
		/// <param name="paintParts">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values specifying the parts to paint.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.DataGridViewCellPaintingEventArgs.RowIndex" /> is less than -1 or greater than or equal to the number of rows in the <see cref="T:System.Windows.Forms.DataGridView" /> control.  
		/// -or-  
		/// <see cref="P:System.Windows.Forms.DataGridViewCellPaintingEventArgs.ColumnIndex" /> is less than -1 or greater than or equal to the number of columns in the <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		// Token: 0x06001E62 RID: 7778 RVA: 0x0008F410 File Offset: 0x0008D610
		public void Paint(Rectangle clipBounds, DataGridViewPaintParts paintParts)
		{
			if (this.rowIndex < -1 || this.rowIndex >= this.dataGridView.Rows.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewElementPaintingEventArgs_RowIndexOutOfRange"));
			}
			if (this.columnIndex < -1 || this.columnIndex >= this.dataGridView.Columns.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewElementPaintingEventArgs_ColumnIndexOutOfRange"));
			}
			this.dataGridView.GetCellInternal(this.columnIndex, this.rowIndex).PaintInternal(this.graphics, clipBounds, this.cellBounds, this.rowIndex, this.cellState, this.value, this.formattedValue, this.errorText, this.cellStyle, this.advancedBorderStyle, paintParts);
		}

		/// <summary>Paints the cell background for the area in the specified bounds.</summary>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that specifies the area of the <see cref="T:System.Windows.Forms.DataGridView" /> to be painted.</param>
		/// <param name="cellsPaintSelectionBackground">
		///   <see langword="true" /> to paint the background of the specified bounds with the color of the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.SelectionBackColor" /> property of the <see cref="P:System.Windows.Forms.DataGridViewCell.InheritedStyle" />; <see langword="false" /> to paint the background of the specified bounds with the color of the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.BackColor" /> property of the <see cref="P:System.Windows.Forms.DataGridViewCell.InheritedStyle" />.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.DataGridViewCellPaintingEventArgs.RowIndex" /> is less than -1 or greater than or equal to the number of rows in the <see cref="T:System.Windows.Forms.DataGridView" /> control.  
		/// -or-  
		/// <see cref="P:System.Windows.Forms.DataGridViewCellPaintingEventArgs.ColumnIndex" /> is less than -1 or greater than or equal to the number of columns in the <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		// Token: 0x06001E63 RID: 7779 RVA: 0x0008F4D4 File Offset: 0x0008D6D4
		public void PaintBackground(Rectangle clipBounds, bool cellsPaintSelectionBackground)
		{
			if (this.rowIndex < -1 || this.rowIndex >= this.dataGridView.Rows.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewElementPaintingEventArgs_RowIndexOutOfRange"));
			}
			if (this.columnIndex < -1 || this.columnIndex >= this.dataGridView.Columns.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewElementPaintingEventArgs_ColumnIndexOutOfRange"));
			}
			DataGridViewPaintParts dataGridViewPaintParts = DataGridViewPaintParts.Background | DataGridViewPaintParts.Border;
			if (cellsPaintSelectionBackground)
			{
				dataGridViewPaintParts |= DataGridViewPaintParts.SelectionBackground;
			}
			this.dataGridView.GetCellInternal(this.columnIndex, this.rowIndex).PaintInternal(this.graphics, clipBounds, this.cellBounds, this.rowIndex, this.cellState, this.value, this.formattedValue, this.errorText, this.cellStyle, this.advancedBorderStyle, dataGridViewPaintParts);
		}

		/// <summary>Paints the cell content for the area in the specified bounds.</summary>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that specifies the area of the <see cref="T:System.Windows.Forms.DataGridView" /> to be painted.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.DataGridViewCellPaintingEventArgs.RowIndex" /> is less than -1 or greater than or equal to the number of rows in the <see cref="T:System.Windows.Forms.DataGridView" /> control.  
		/// -or-  
		/// <see cref="P:System.Windows.Forms.DataGridViewCellPaintingEventArgs.ColumnIndex" /> is less than -1 or greater than or equal to the number of columns in the <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
		// Token: 0x06001E64 RID: 7780 RVA: 0x0008F5A4 File Offset: 0x0008D7A4
		public void PaintContent(Rectangle clipBounds)
		{
			if (this.rowIndex < -1 || this.rowIndex >= this.dataGridView.Rows.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewElementPaintingEventArgs_RowIndexOutOfRange"));
			}
			if (this.columnIndex < -1 || this.columnIndex >= this.dataGridView.Columns.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewElementPaintingEventArgs_ColumnIndexOutOfRange"));
			}
			this.dataGridView.GetCellInternal(this.columnIndex, this.rowIndex).PaintInternal(this.graphics, clipBounds, this.cellBounds, this.rowIndex, this.cellState, this.value, this.formattedValue, this.errorText, this.cellStyle, this.advancedBorderStyle, DataGridViewPaintParts.ContentBackground | DataGridViewPaintParts.ContentForeground | DataGridViewPaintParts.ErrorIcon);
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x0008F668 File Offset: 0x0008D868
		internal void SetProperties(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, int columnIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{
			this.graphics = graphics;
			this.clipBounds = clipBounds;
			this.cellBounds = cellBounds;
			this.rowIndex = rowIndex;
			this.columnIndex = columnIndex;
			this.cellState = cellState;
			this.value = value;
			this.formattedValue = formattedValue;
			this.errorText = errorText;
			this.cellStyle = cellStyle;
			this.advancedBorderStyle = advancedBorderStyle;
			this.paintParts = paintParts;
			base.Handled = false;
		}

		// Token: 0x04000CC7 RID: 3271
		private DataGridView dataGridView;

		// Token: 0x04000CC8 RID: 3272
		private Graphics graphics;

		// Token: 0x04000CC9 RID: 3273
		private Rectangle clipBounds;

		// Token: 0x04000CCA RID: 3274
		private Rectangle cellBounds;

		// Token: 0x04000CCB RID: 3275
		private int rowIndex;

		// Token: 0x04000CCC RID: 3276
		private int columnIndex;

		// Token: 0x04000CCD RID: 3277
		private DataGridViewElementStates cellState;

		// Token: 0x04000CCE RID: 3278
		private object value;

		// Token: 0x04000CCF RID: 3279
		private object formattedValue;

		// Token: 0x04000CD0 RID: 3280
		private string errorText;

		// Token: 0x04000CD1 RID: 3281
		private DataGridViewCellStyle cellStyle;

		// Token: 0x04000CD2 RID: 3282
		private DataGridViewAdvancedBorderStyle advancedBorderStyle;

		// Token: 0x04000CD3 RID: 3283
		private DataGridViewPaintParts paintParts;
	}
}
