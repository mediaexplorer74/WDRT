using System;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Identifies a cell in the grid.</summary>
	// Token: 0x0200017D RID: 381
	public struct DataGridCell
	{
		/// <summary>Gets or sets the number of a column in the <see cref="T:System.Windows.Forms.DataGrid" /> control.</summary>
		/// <returns>The number of the column.</returns>
		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x0600164C RID: 5708 RVA: 0x00050600 File Offset: 0x0004E800
		// (set) Token: 0x0600164D RID: 5709 RVA: 0x00050608 File Offset: 0x0004E808
		public int ColumnNumber
		{
			get
			{
				return this.columnNumber;
			}
			set
			{
				this.columnNumber = value;
			}
		}

		/// <summary>Gets or sets the number of a row in the <see cref="T:System.Windows.Forms.DataGrid" /> control.</summary>
		/// <returns>The number of the row.</returns>
		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x00050611 File Offset: 0x0004E811
		// (set) Token: 0x0600164F RID: 5711 RVA: 0x00050619 File Offset: 0x0004E819
		public int RowNumber
		{
			get
			{
				return this.rowNumber;
			}
			set
			{
				this.rowNumber = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridCell" /> class.</summary>
		/// <param name="r">The number of a row in the <see cref="T:System.Windows.Forms.DataGrid" />.</param>
		/// <param name="c">The number of a column in the <see cref="T:System.Windows.Forms.DataGrid" />.</param>
		// Token: 0x06001650 RID: 5712 RVA: 0x00050622 File Offset: 0x0004E822
		public DataGridCell(int r, int c)
		{
			this.rowNumber = r;
			this.columnNumber = c;
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.DataGridCell" /> is identical to a second <see cref="T:System.Windows.Forms.DataGridCell" />.</summary>
		/// <param name="o">An object you are to comparing.</param>
		/// <returns>
		///   <see langword="true" /> if the second object is identical to the first; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001651 RID: 5713 RVA: 0x00050634 File Offset: 0x0004E834
		public override bool Equals(object o)
		{
			if (o is DataGridCell)
			{
				DataGridCell dataGridCell = (DataGridCell)o;
				return dataGridCell.RowNumber == this.RowNumber && dataGridCell.ColumnNumber == this.ColumnNumber;
			}
			return false;
		}

		/// <summary>Gets a hash value that can be added to a <see cref="T:System.Collections.Hashtable" />.</summary>
		/// <returns>A number that uniquely identifies the <see cref="T:System.Windows.Forms.DataGridCell" /> in a <see cref="T:System.Collections.Hashtable" />.</returns>
		// Token: 0x06001652 RID: 5714 RVA: 0x00050672 File Offset: 0x0004E872
		public override int GetHashCode()
		{
			return ((~this.rowNumber * (this.columnNumber + 1)) & 16776960) >> 8;
		}

		/// <summary>Gets the row number and column number of the cell.</summary>
		/// <returns>A string containing the row number and column number.</returns>
		// Token: 0x06001653 RID: 5715 RVA: 0x0005068C File Offset: 0x0004E88C
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"DataGridCell {RowNumber = ",
				this.RowNumber.ToString(CultureInfo.CurrentCulture),
				", ColumnNumber = ",
				this.ColumnNumber.ToString(CultureInfo.CurrentCulture),
				"}"
			});
		}

		// Token: 0x04000A31 RID: 2609
		private int rowNumber;

		// Token: 0x04000A32 RID: 2610
		private int columnNumber;
	}
}
