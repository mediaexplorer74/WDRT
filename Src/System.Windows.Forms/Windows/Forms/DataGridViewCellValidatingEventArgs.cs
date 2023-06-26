using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.CellValidating" /> event of a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	// Token: 0x020001B8 RID: 440
	public class DataGridViewCellValidatingEventArgs : CancelEventArgs
	{
		// Token: 0x06001EB4 RID: 7860 RVA: 0x000908F2 File Offset: 0x0008EAF2
		internal DataGridViewCellValidatingEventArgs(int columnIndex, int rowIndex, object formattedValue)
		{
			this.rowIndex = rowIndex;
			this.columnIndex = columnIndex;
			this.formattedValue = formattedValue;
		}

		/// <summary>Gets the column index of the cell that needs to be validated.</summary>
		/// <returns>A zero-based integer that specifies the column index of the cell that needs to be validated.</returns>
		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001EB5 RID: 7861 RVA: 0x0009090F File Offset: 0x0008EB0F
		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
		}

		/// <summary>Gets the formatted contents of the cell that needs to be validated.</summary>
		/// <returns>A reference to the formatted value.</returns>
		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06001EB6 RID: 7862 RVA: 0x00090917 File Offset: 0x0008EB17
		public object FormattedValue
		{
			get
			{
				return this.formattedValue;
			}
		}

		/// <summary>Gets the row index of the cell that needs to be validated.</summary>
		/// <returns>A zero-based integer that specifies the row index of the cell that needs to be validated.</returns>
		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001EB7 RID: 7863 RVA: 0x0009091F File Offset: 0x0008EB1F
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x04000CFD RID: 3325
		private int rowIndex;

		// Token: 0x04000CFE RID: 3326
		private int columnIndex;

		// Token: 0x04000CFF RID: 3327
		private object formattedValue;
	}
}
