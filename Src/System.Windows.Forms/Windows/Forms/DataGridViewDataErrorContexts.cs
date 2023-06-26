using System;

namespace System.Windows.Forms
{
	/// <summary>Represents the state of a data-bound <see cref="T:System.Windows.Forms.DataGridView" /> control when a data error occurred.</summary>
	// Token: 0x020001CC RID: 460
	[Flags]
	public enum DataGridViewDataErrorContexts
	{
		/// <summary>A data error occurred when trying to format data that is either being sent to a data store, or being loaded from a data store. This value indicates that a change to a cell failed to format correctly. Either the new cell value needs to be corrected or the cell's formatting needs to change.</summary>
		// Token: 0x04000D98 RID: 3480
		Formatting = 1,
		/// <summary>A data error occurred when displaying a cell that was populated by a data source. This value indicates that the value from the data source cannot be displayed by the cell, or a mapping that translates the value from the data source to the cell is missing.</summary>
		// Token: 0x04000D99 RID: 3481
		Display = 2,
		/// <summary>A data error occurred when calculating the preferred size of a cell. This value indicates that the <see cref="T:System.Windows.Forms.DataGridView" /> failed to calculate the preferred width or height of a cell when programmatically resizing a column or row. This can occur if the cell failed to format its value.</summary>
		// Token: 0x04000D9A RID: 3482
		PreferredSize = 4,
		/// <summary>A data error occurred when deleting a row. This value indicates that the underlying data store threw an exception when a data-bound <see cref="T:System.Windows.Forms.DataGridView" /> deleted a row.</summary>
		// Token: 0x04000D9B RID: 3483
		RowDeletion = 8,
		/// <summary>A data error occurred when parsing new data. This value indicates that the <see cref="T:System.Windows.Forms.DataGridView" /> could not parse new data that was entered by the user or loaded from the underlying data store.</summary>
		// Token: 0x04000D9C RID: 3484
		Parsing = 256,
		/// <summary>A data error occurred when committing changes to the data store. This value indicates that data entered in a cell could not be committed to the underlying data store.</summary>
		// Token: 0x04000D9D RID: 3485
		Commit = 512,
		/// <summary>A data error occurred when restoring a cell to its previous value. This value indicates that a cell tried to cancel an edit and the rollback to the initial value failed. This can occur if the cell formatting changed so that it is incompatible with the initial value.</summary>
		// Token: 0x04000D9E RID: 3486
		InitialValueRestoration = 1024,
		/// <summary>A data error occurred when the <see cref="T:System.Windows.Forms.DataGridView" /> lost focus. This value indicates that the <see cref="T:System.Windows.Forms.DataGridView" /> could not commit user changes after losing focus.</summary>
		// Token: 0x04000D9F RID: 3487
		LeaveControl = 2048,
		/// <summary>A data error occurred when the selection cursor moved to another cell. This value indicates that a user selected a cell when the previously selected cell had an error condition.</summary>
		// Token: 0x04000DA0 RID: 3488
		CurrentCellChange = 4096,
		/// <summary>A data error occurred when scrolling a new region into view. This value indicates that a cell with data errors scrolled into view programmatically or with the scroll bar.</summary>
		// Token: 0x04000DA1 RID: 3489
		Scroll = 8192,
		/// <summary>A data error occurred when copying content to the Clipboard. This value indicates that the cell value could not be converted to a string.</summary>
		// Token: 0x04000DA2 RID: 3490
		ClipboardContent = 16384
	}
}
