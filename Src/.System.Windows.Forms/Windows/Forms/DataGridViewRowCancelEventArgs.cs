using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.UserDeletingRow" /> event of a <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
	// Token: 0x02000207 RID: 519
	public class DataGridViewRowCancelEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRowCancelEventArgs" /> class.</summary>
		/// <param name="dataGridViewRow">The row the user is deleting.</param>
		// Token: 0x0600220E RID: 8718 RVA: 0x000A1D06 File Offset: 0x0009FF06
		public DataGridViewRowCancelEventArgs(DataGridViewRow dataGridViewRow)
		{
			this.dataGridViewRow = dataGridViewRow;
		}

		/// <summary>Gets the row that the user is deleting.</summary>
		/// <returns>The row that the user deleted.</returns>
		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x0600220F RID: 8719 RVA: 0x000A1D15 File Offset: 0x0009FF15
		public DataGridViewRow Row
		{
			get
			{
				return this.dataGridViewRow;
			}
		}

		// Token: 0x04000E1B RID: 3611
		private DataGridViewRow dataGridViewRow;
	}
}
