using System;

namespace System.Windows.Forms
{
	/// <summary>Defines common functionality for a cell that allows the manipulation of its value.</summary>
	// Token: 0x020001CE RID: 462
	public interface IDataGridViewEditingCell
	{
		/// <summary>Gets or sets the formatted value of the cell.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the cell's value.</returns>
		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x0600205A RID: 8282
		// (set) Token: 0x0600205B RID: 8283
		object EditingCellFormattedValue { get; set; }

		/// <summary>Gets or sets a value indicating whether the value of the cell has changed.</summary>
		/// <returns>
		///   <see langword="true" /> if the value of the cell has changed; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x0600205C RID: 8284
		// (set) Token: 0x0600205D RID: 8285
		bool EditingCellValueChanged { get; set; }

		/// <summary>Retrieves the formatted value of the cell.</summary>
		/// <param name="context">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> values that specifies the context in which the data is needed.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the formatted version of the cell contents.</returns>
		// Token: 0x0600205E RID: 8286
		object GetEditingCellFormattedValue(DataGridViewDataErrorContexts context);

		/// <summary>Prepares the currently selected cell for editing</summary>
		/// <param name="selectAll">
		///   <see langword="true" /> to select the cell contents; otherwise, <see langword="false" />.</param>
		// Token: 0x0600205F RID: 8287
		void PrepareEditingCellForEdit(bool selectAll);
	}
}
