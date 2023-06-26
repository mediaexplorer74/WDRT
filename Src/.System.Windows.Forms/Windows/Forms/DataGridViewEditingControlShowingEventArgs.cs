using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.EditingControlShowing" /> event.</summary>
	// Token: 0x020001D1 RID: 465
	public class DataGridViewEditingControlShowingEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs" /> class.</summary>
		/// <param name="control">A <see cref="T:System.Windows.Forms.Control" /> in which the user will edit the selected cell's contents.</param>
		/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> representing the style of the cell being edited.</param>
		// Token: 0x06002074 RID: 8308 RVA: 0x0009B812 File Offset: 0x00099A12
		public DataGridViewEditingControlShowingEventArgs(Control control, DataGridViewCellStyle cellStyle)
		{
			this.control = control;
			this.cellStyle = cellStyle;
		}

		/// <summary>Gets or sets the cell style of the edited cell.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> representing the style of the cell being edited.</returns>
		/// <exception cref="T:System.ArgumentNullException">The specified value when setting this property is <see langword="null" />.</exception>
		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06002075 RID: 8309 RVA: 0x0009B828 File Offset: 0x00099A28
		// (set) Token: 0x06002076 RID: 8310 RVA: 0x0009B830 File Offset: 0x00099A30
		public DataGridViewCellStyle CellStyle
		{
			get
			{
				return this.cellStyle;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.cellStyle = value;
			}
		}

		/// <summary>The control shown to the user for editing the selected cell's value.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Control" /> that displays an area for the user to enter or change the selected cell's value.</returns>
		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06002077 RID: 8311 RVA: 0x0009B847 File Offset: 0x00099A47
		public Control Control
		{
			get
			{
				return this.control;
			}
		}

		// Token: 0x04000DA6 RID: 3494
		private Control control;

		// Token: 0x04000DA7 RID: 3495
		private DataGridViewCellStyle cellStyle;
	}
}
