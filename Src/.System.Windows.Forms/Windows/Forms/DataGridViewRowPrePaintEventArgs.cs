using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.RowPrePaint" /> event.</summary>
	// Token: 0x02000212 RID: 530
	public class DataGridViewRowPrePaintEventArgs : HandledEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRowPrePaintEventArgs" /> class.</summary>
		/// <param name="dataGridView">The <see cref="T:System.Windows.Forms.DataGridView" /> that owns the row that is being painted.</param>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the <see cref="T:System.Windows.Forms.DataGridViewRow" />.</param>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be painted.</param>
		/// <param name="rowBounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewRow" /> that is being painted.</param>
		/// <param name="rowIndex">The row index of the cell that is being painted.</param>
		/// <param name="rowState">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that specifies the state of the row.</param>
		/// <param name="errorText">An error message that is associated with the row.</param>
		/// <param name="inheritedRowStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that contains formatting and style information about the row.</param>
		/// <param name="isFirstDisplayedRow">
		///   <see langword="true" /> to indicate whether the current row is the first row currently displayed in the <see cref="T:System.Windows.Forms.DataGridView" />; otherwise, <see langword="false" />.</param>
		/// <param name="isLastVisibleRow">
		///   <see langword="true" /> to indicate whether the current row is the last row in the <see cref="T:System.Windows.Forms.DataGridView" /> that has the <see cref="P:System.Windows.Forms.DataGridViewRow.Visible" /> property set to <see langword="true" />; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridView" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="graphics" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="inheritedRowStyle" /> is <see langword="null" />.</exception>
		// Token: 0x060022B6 RID: 8886 RVA: 0x000A6A84 File Offset: 0x000A4C84
		public DataGridViewRowPrePaintEventArgs(DataGridView dataGridView, Graphics graphics, Rectangle clipBounds, Rectangle rowBounds, int rowIndex, DataGridViewElementStates rowState, string errorText, DataGridViewCellStyle inheritedRowStyle, bool isFirstDisplayedRow, bool isLastVisibleRow)
		{
			if (dataGridView == null)
			{
				throw new ArgumentNullException("dataGridView");
			}
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			if (inheritedRowStyle == null)
			{
				throw new ArgumentNullException("inheritedRowStyle");
			}
			this.dataGridView = dataGridView;
			this.graphics = graphics;
			this.clipBounds = clipBounds;
			this.rowBounds = rowBounds;
			this.rowIndex = rowIndex;
			this.rowState = rowState;
			this.errorText = errorText;
			this.inheritedRowStyle = inheritedRowStyle;
			this.isFirstDisplayedRow = isFirstDisplayedRow;
			this.isLastVisibleRow = isLastVisibleRow;
			this.paintParts = DataGridViewPaintParts.All;
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x000A6B17 File Offset: 0x000A4D17
		internal DataGridViewRowPrePaintEventArgs(DataGridView dataGridView)
		{
			this.dataGridView = dataGridView;
		}

		/// <summary>Gets or sets the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</returns>
		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x060022B8 RID: 8888 RVA: 0x000A6B26 File Offset: 0x000A4D26
		// (set) Token: 0x060022B9 RID: 8889 RVA: 0x000A6B2E File Offset: 0x000A4D2E
		public Rectangle ClipBounds
		{
			get
			{
				return this.clipBounds;
			}
			set
			{
				this.clipBounds = value;
			}
		}

		/// <summary>Gets a string that represents an error message for the current <see cref="T:System.Windows.Forms.DataGridViewRow" />.</summary>
		/// <returns>A string that represents an error message for the current <see cref="T:System.Windows.Forms.DataGridViewRow" />.</returns>
		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x060022BA RID: 8890 RVA: 0x000A6B37 File Offset: 0x000A4D37
		public string ErrorText
		{
			get
			{
				return this.errorText;
			}
		}

		/// <summary>Gets the <see cref="T:System.Drawing.Graphics" /> used to paint the current <see cref="T:System.Windows.Forms.DataGridViewRow" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Graphics" /> used to paint the current <see cref="T:System.Windows.Forms.DataGridViewRow" />.</returns>
		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x060022BB RID: 8891 RVA: 0x000A6B3F File Offset: 0x000A4D3F
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>Gets the cell style applied to the row.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that contains the cell style currently applied to the row.</returns>
		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x060022BC RID: 8892 RVA: 0x000A6B47 File Offset: 0x000A4D47
		public DataGridViewCellStyle InheritedRowStyle
		{
			get
			{
				return this.inheritedRowStyle;
			}
		}

		/// <summary>Gets a value indicating whether the current row is the first row currently displayed in the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the row being painted is currently the first row displayed in the <see cref="T:System.Windows.Forms.DataGridView" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x060022BD RID: 8893 RVA: 0x000A6B4F File Offset: 0x000A4D4F
		public bool IsFirstDisplayedRow
		{
			get
			{
				return this.isFirstDisplayedRow;
			}
		}

		/// <summary>Gets a value indicating whether the current row is the last visible row in the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the row being painted is currently the last row in the <see cref="T:System.Windows.Forms.DataGridView" /> that has the <see cref="P:System.Windows.Forms.DataGridViewRow.Visible" /> property set to <see langword="true" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x060022BE RID: 8894 RVA: 0x000A6B57 File Offset: 0x000A4D57
		public bool IsLastVisibleRow
		{
			get
			{
				return this.isLastVisibleRow;
			}
		}

		/// <summary>The cell parts that are to be painted.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values specifying the parts to be painted.</returns>
		/// <exception cref="T:System.ArgumentException">The specified value when setting this property is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values.</exception>
		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x060022BF RID: 8895 RVA: 0x000A6B5F File Offset: 0x000A4D5F
		// (set) Token: 0x060022C0 RID: 8896 RVA: 0x000A6B67 File Offset: 0x000A4D67
		public DataGridViewPaintParts PaintParts
		{
			get
			{
				return this.paintParts;
			}
			set
			{
				if ((value & ~DataGridViewPaintParts.All) != DataGridViewPaintParts.None)
				{
					throw new ArgumentException(SR.GetString("DataGridView_InvalidDataGridViewPaintPartsCombination", new object[] { "value" }));
				}
				this.paintParts = value;
			}
		}

		/// <summary>Get the bounds of the current <see cref="T:System.Windows.Forms.DataGridViewRow" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the current <see cref="T:System.Windows.Forms.DataGridViewRow" />.</returns>
		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x060022C1 RID: 8897 RVA: 0x000A6B94 File Offset: 0x000A4D94
		public Rectangle RowBounds
		{
			get
			{
				return this.rowBounds;
			}
		}

		/// <summary>Gets the index of the current <see cref="T:System.Windows.Forms.DataGridViewRow" />.</summary>
		/// <returns>The index of the current <see cref="T:System.Windows.Forms.DataGridViewRow" />.</returns>
		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x060022C2 RID: 8898 RVA: 0x000A6B9C File Offset: 0x000A4D9C
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		/// <summary>Gets the state of the current <see cref="T:System.Windows.Forms.DataGridViewRow" />.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that specifies the state of the row.</returns>
		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x060022C3 RID: 8899 RVA: 0x000A6BA4 File Offset: 0x000A4DA4
		public DataGridViewElementStates State
		{
			get
			{
				return this.rowState;
			}
		}

		/// <summary>Draws the focus rectangle around the specified bounds.</summary>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that specifies the focus area.</param>
		/// <param name="cellsPaintSelectionBackground">
		///   <see langword="true" /> to use the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.SelectionBackColor" /> property of the <see cref="P:System.Windows.Forms.DataGridViewRow.InheritedStyle" /> property to determine the color of the focus rectangle; <see langword="false" /> to use the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.BackColor" /> property of the <see cref="P:System.Windows.Forms.DataGridViewRow.InheritedStyle" />.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.DataGridViewRowPrePaintEventArgs.RowIndex" /> is less than zero or greater than the number of rows in the <see cref="T:System.Windows.Forms.DataGridView" /> control minus one.</exception>
		// Token: 0x060022C4 RID: 8900 RVA: 0x000A6BAC File Offset: 0x000A4DAC
		public void DrawFocus(Rectangle bounds, bool cellsPaintSelectionBackground)
		{
			if (this.rowIndex < 0 || this.rowIndex >= this.dataGridView.Rows.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewElementPaintingEventArgs_RowIndexOutOfRange"));
			}
			this.dataGridView.Rows.SharedRow(this.rowIndex).DrawFocus(this.graphics, this.clipBounds, bounds, this.rowIndex, this.rowState, this.inheritedRowStyle, cellsPaintSelectionBackground);
		}

		/// <summary>Paints the specified cell parts for the area in the specified bounds.</summary>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that specifies the area of the <see cref="T:System.Windows.Forms.DataGridView" /> to be painted.</param>
		/// <param name="paintParts">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values specifying the parts to paint.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.DataGridViewRowPostPaintEventArgs.RowIndex" /> is less than zero or greater than the number of rows in the <see cref="T:System.Windows.Forms.DataGridView" /> control minus one.</exception>
		// Token: 0x060022C5 RID: 8901 RVA: 0x000A6C28 File Offset: 0x000A4E28
		public void PaintCells(Rectangle clipBounds, DataGridViewPaintParts paintParts)
		{
			if (this.rowIndex < 0 || this.rowIndex >= this.dataGridView.Rows.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewElementPaintingEventArgs_RowIndexOutOfRange"));
			}
			this.dataGridView.Rows.SharedRow(this.rowIndex).PaintCells(this.graphics, clipBounds, this.rowBounds, this.rowIndex, this.rowState, this.isFirstDisplayedRow, this.isLastVisibleRow, paintParts);
		}

		/// <summary>Paints the cell backgrounds for the area in the specified bounds.</summary>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that specifies the area of the <see cref="T:System.Windows.Forms.DataGridView" /> to be painted.</param>
		/// <param name="cellsPaintSelectionBackground">
		///   <see langword="true" /> to paint the background of the specified bounds with the color of the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.SelectionBackColor" /> property of the <see cref="P:System.Windows.Forms.DataGridViewRow.InheritedStyle" />; <see langword="false" /> to paint the background of the specified bounds with the color of the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.BackColor" /> property of the <see cref="P:System.Windows.Forms.DataGridViewRow.InheritedStyle" />.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.DataGridViewRowPostPaintEventArgs.RowIndex" /> is less than zero or greater than the number of rows in the <see cref="T:System.Windows.Forms.DataGridView" /> control minus one.</exception>
		// Token: 0x060022C6 RID: 8902 RVA: 0x000A6CA8 File Offset: 0x000A4EA8
		public void PaintCellsBackground(Rectangle clipBounds, bool cellsPaintSelectionBackground)
		{
			if (this.rowIndex < 0 || this.rowIndex >= this.dataGridView.Rows.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewElementPaintingEventArgs_RowIndexOutOfRange"));
			}
			DataGridViewPaintParts dataGridViewPaintParts = DataGridViewPaintParts.Background | DataGridViewPaintParts.Border;
			if (cellsPaintSelectionBackground)
			{
				dataGridViewPaintParts |= DataGridViewPaintParts.SelectionBackground;
			}
			this.dataGridView.Rows.SharedRow(this.rowIndex).PaintCells(this.graphics, clipBounds, this.rowBounds, this.rowIndex, this.rowState, this.isFirstDisplayedRow, this.isLastVisibleRow, dataGridViewPaintParts);
		}

		/// <summary>Paints the cell contents for the area in the specified bounds.</summary>
		/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that specifies the area of the <see cref="T:System.Windows.Forms.DataGridView" /> to be painted.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.DataGridViewRowPostPaintEventArgs.RowIndex" /> is less than zero or greater than the number of rows in the <see cref="T:System.Windows.Forms.DataGridView" /> control minus one.</exception>
		// Token: 0x060022C7 RID: 8903 RVA: 0x000A6D34 File Offset: 0x000A4F34
		public void PaintCellsContent(Rectangle clipBounds)
		{
			if (this.rowIndex < 0 || this.rowIndex >= this.dataGridView.Rows.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewElementPaintingEventArgs_RowIndexOutOfRange"));
			}
			this.dataGridView.Rows.SharedRow(this.rowIndex).PaintCells(this.graphics, clipBounds, this.rowBounds, this.rowIndex, this.rowState, this.isFirstDisplayedRow, this.isLastVisibleRow, DataGridViewPaintParts.ContentBackground | DataGridViewPaintParts.ContentForeground | DataGridViewPaintParts.ErrorIcon);
		}

		/// <summary>Paints the entire row header of the current <see cref="T:System.Windows.Forms.DataGridViewRow" />.</summary>
		/// <param name="paintSelectionBackground">
		///   <see langword="true" /> to paint the row header with the color of the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.SelectionBackColor" /> property of the <see cref="P:System.Windows.Forms.DataGridViewRow.InheritedStyle" />; <see langword="false" /> to paint the row header with the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.BackColor" /> of the <see cref="P:System.Windows.Forms.DataGridView.RowHeadersDefaultCellStyle" /> property.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.DataGridViewRowPostPaintEventArgs.RowIndex" /> is less than zero or greater than the number of rows in the <see cref="T:System.Windows.Forms.DataGridView" /> control minus one.</exception>
		// Token: 0x060022C8 RID: 8904 RVA: 0x000A6DB4 File Offset: 0x000A4FB4
		public void PaintHeader(bool paintSelectionBackground)
		{
			DataGridViewPaintParts dataGridViewPaintParts = DataGridViewPaintParts.Background | DataGridViewPaintParts.Border | DataGridViewPaintParts.ContentBackground | DataGridViewPaintParts.ContentForeground | DataGridViewPaintParts.ErrorIcon;
			if (paintSelectionBackground)
			{
				dataGridViewPaintParts |= DataGridViewPaintParts.SelectionBackground;
			}
			this.PaintHeader(dataGridViewPaintParts);
		}

		/// <summary>Paints the specified parts of the row header of the current row.</summary>
		/// <param name="paintParts">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values specifying the parts to paint.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.DataGridViewRowPostPaintEventArgs.RowIndex" /> is less than zero or greater than the number of rows in the <see cref="T:System.Windows.Forms.DataGridView" /> control minus one.</exception>
		// Token: 0x060022C9 RID: 8905 RVA: 0x000A6DD4 File Offset: 0x000A4FD4
		public void PaintHeader(DataGridViewPaintParts paintParts)
		{
			if (this.rowIndex < 0 || this.rowIndex >= this.dataGridView.Rows.Count)
			{
				throw new InvalidOperationException(SR.GetString("DataGridViewElementPaintingEventArgs_RowIndexOutOfRange"));
			}
			this.dataGridView.Rows.SharedRow(this.rowIndex).PaintHeader(this.graphics, this.clipBounds, this.rowBounds, this.rowIndex, this.rowState, this.isFirstDisplayedRow, this.isLastVisibleRow, paintParts);
		}

		// Token: 0x060022CA RID: 8906 RVA: 0x000A6E58 File Offset: 0x000A5058
		internal void SetProperties(Graphics graphics, Rectangle clipBounds, Rectangle rowBounds, int rowIndex, DataGridViewElementStates rowState, string errorText, DataGridViewCellStyle inheritedRowStyle, bool isFirstDisplayedRow, bool isLastVisibleRow)
		{
			this.graphics = graphics;
			this.clipBounds = clipBounds;
			this.rowBounds = rowBounds;
			this.rowIndex = rowIndex;
			this.rowState = rowState;
			this.errorText = errorText;
			this.inheritedRowStyle = inheritedRowStyle;
			this.isFirstDisplayedRow = isFirstDisplayedRow;
			this.isLastVisibleRow = isLastVisibleRow;
			this.paintParts = DataGridViewPaintParts.All;
			base.Handled = false;
		}

		// Token: 0x04000E4B RID: 3659
		private DataGridView dataGridView;

		// Token: 0x04000E4C RID: 3660
		private Graphics graphics;

		// Token: 0x04000E4D RID: 3661
		private Rectangle clipBounds;

		// Token: 0x04000E4E RID: 3662
		private Rectangle rowBounds;

		// Token: 0x04000E4F RID: 3663
		private DataGridViewCellStyle inheritedRowStyle;

		// Token: 0x04000E50 RID: 3664
		private int rowIndex;

		// Token: 0x04000E51 RID: 3665
		private DataGridViewElementStates rowState;

		// Token: 0x04000E52 RID: 3666
		private string errorText;

		// Token: 0x04000E53 RID: 3667
		private bool isFirstDisplayedRow;

		// Token: 0x04000E54 RID: 3668
		private bool isLastVisibleRow;

		// Token: 0x04000E55 RID: 3669
		private DataGridViewPaintParts paintParts;
	}
}
