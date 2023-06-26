using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.CellErrorTextNeeded" /> event of a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	// Token: 0x020001A7 RID: 423
	public class DataGridViewCellErrorTextNeededEventArgs : DataGridViewCellEventArgs
	{
		// Token: 0x06001E31 RID: 7729 RVA: 0x0008EE9F File Offset: 0x0008D09F
		internal DataGridViewCellErrorTextNeededEventArgs(int columnIndex, int rowIndex, string errorText)
			: base(columnIndex, rowIndex)
		{
			this.errorText = errorText;
		}

		/// <summary>Gets or sets the message that is displayed when the cell is selected.</summary>
		/// <returns>The error message.</returns>
		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001E32 RID: 7730 RVA: 0x0008EEB0 File Offset: 0x0008D0B0
		// (set) Token: 0x06001E33 RID: 7731 RVA: 0x0008EEB8 File Offset: 0x0008D0B8
		public string ErrorText
		{
			get
			{
				return this.errorText;
			}
			set
			{
				this.errorText = value;
			}
		}

		// Token: 0x04000CB5 RID: 3253
		private string errorText;
	}
}
