using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.RowErrorTextNeeded" /> event of a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	// Token: 0x0200020C RID: 524
	public class DataGridViewRowErrorTextNeededEventArgs : EventArgs
	{
		// Token: 0x06002276 RID: 8822 RVA: 0x000A4F21 File Offset: 0x000A3121
		internal DataGridViewRowErrorTextNeededEventArgs(int rowIndex, string errorText)
		{
			this.rowIndex = rowIndex;
			this.errorText = errorText;
		}

		/// <summary>Gets or sets the error text for the row.</summary>
		/// <returns>A string that represents the error text for the row.</returns>
		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06002277 RID: 8823 RVA: 0x000A4F37 File Offset: 0x000A3137
		// (set) Token: 0x06002278 RID: 8824 RVA: 0x000A4F3F File Offset: 0x000A313F
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

		/// <summary>Gets the row that raised the <see cref="E:System.Windows.Forms.DataGridView.RowErrorTextNeeded" /> event.</summary>
		/// <returns>The zero based row index for the row.</returns>
		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06002279 RID: 8825 RVA: 0x000A4F48 File Offset: 0x000A3148
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x04000E28 RID: 3624
		private int rowIndex;

		// Token: 0x04000E29 RID: 3625
		private string errorText;
	}
}
