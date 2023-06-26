using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the <see cref="T:System.Windows.Forms.DataGridView" /> entity that owns the cell style that was changed.</summary>
	// Token: 0x020001B6 RID: 438
	[Flags]
	public enum DataGridViewCellStyleScopes
	{
		/// <summary>The owning entity is unspecified.</summary>
		// Token: 0x04000CF3 RID: 3315
		None = 0,
		/// <summary>One or more values of the object returned by the <see cref="P:System.Windows.Forms.DataGridViewCell.Style" /> property changed.</summary>
		// Token: 0x04000CF4 RID: 3316
		Cell = 1,
		/// <summary>One or more values of the object returned by the <see cref="P:System.Windows.Forms.DataGridViewColumn.DefaultCellStyle" /> property changed.</summary>
		// Token: 0x04000CF5 RID: 3317
		Column = 2,
		/// <summary>One or more values of the object returned by the <see cref="P:System.Windows.Forms.DataGridViewRow.DefaultCellStyle" /> property changed.</summary>
		// Token: 0x04000CF6 RID: 3318
		Row = 4,
		/// <summary>One or more values of the object returned by the <see cref="P:System.Windows.Forms.DataGridView.DefaultCellStyle" /> property changed.</summary>
		// Token: 0x04000CF7 RID: 3319
		DataGridView = 8,
		/// <summary>One or more values of the object returned by the <see cref="P:System.Windows.Forms.DataGridView.ColumnHeadersDefaultCellStyle" /> property changed.</summary>
		// Token: 0x04000CF8 RID: 3320
		ColumnHeaders = 16,
		/// <summary>One or more values of the object returned by the <see cref="P:System.Windows.Forms.DataGridView.RowHeadersDefaultCellStyle" /> property changed.</summary>
		// Token: 0x04000CF9 RID: 3321
		RowHeaders = 32,
		/// <summary>One or more values of the object returned by the <see cref="P:System.Windows.Forms.DataGridView.RowsDefaultCellStyle" /> property changed.</summary>
		// Token: 0x04000CFA RID: 3322
		Rows = 64,
		/// <summary>One or more values of the object returned by the <see cref="P:System.Windows.Forms.DataGridView.AlternatingRowsDefaultCellStyle" /> property changed.</summary>
		// Token: 0x04000CFB RID: 3323
		AlternatingRows = 128
	}
}
