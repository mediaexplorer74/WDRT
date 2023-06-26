using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.AutoSizeColumnsModeChanged" /> event.</summary>
	// Token: 0x02000192 RID: 402
	public class DataGridViewAutoSizeColumnsModeEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnsModeEventArgs" /> class.</summary>
		/// <param name="previousModes">An array of <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode" /> values representing the previous <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode" /> property values of each column.</param>
		// Token: 0x06001CB8 RID: 7352 RVA: 0x0008693A File Offset: 0x00084B3A
		public DataGridViewAutoSizeColumnsModeEventArgs(DataGridViewAutoSizeColumnMode[] previousModes)
		{
			this.previousModes = previousModes;
		}

		/// <summary>Gets an array of the previous values of the column <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode" /> properties.</summary>
		/// <returns>An array of <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode" /> values representing the previous values of the column <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode" /> properties.</returns>
		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001CB9 RID: 7353 RVA: 0x00086949 File Offset: 0x00084B49
		public DataGridViewAutoSizeColumnMode[] PreviousModes
		{
			get
			{
				return this.previousModes;
			}
		}

		// Token: 0x04000C2D RID: 3117
		private DataGridViewAutoSizeColumnMode[] previousModes;
	}
}
