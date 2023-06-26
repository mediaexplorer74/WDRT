using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.AutoSizeColumnModeChanged" /> event.</summary>
	// Token: 0x02000191 RID: 401
	public class DataGridViewAutoSizeColumnModeEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnModeEventArgs" /> class.</summary>
		/// <param name="dataGridViewColumn">The column with the <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode" /> property that changed.</param>
		/// <param name="previousMode">The previous <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode" /> value of the column's <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode" /> property.</param>
		// Token: 0x06001CB5 RID: 7349 RVA: 0x00086914 File Offset: 0x00084B14
		public DataGridViewAutoSizeColumnModeEventArgs(DataGridViewColumn dataGridViewColumn, DataGridViewAutoSizeColumnMode previousMode)
		{
			this.dataGridViewColumn = dataGridViewColumn;
			this.previousMode = previousMode;
		}

		/// <summary>Gets the column with the <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode" /> property that changed.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> with the <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode" /> property that changed.</returns>
		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001CB6 RID: 7350 RVA: 0x0008692A File Offset: 0x00084B2A
		public DataGridViewColumn Column
		{
			get
			{
				return this.dataGridViewColumn;
			}
		}

		/// <summary>Gets the previous value of the <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode" /> property of the column.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode" /> value representing the previous value of the <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode" /> property of the <see cref="P:System.Windows.Forms.DataGridViewAutoSizeColumnModeEventArgs.Column" />.</returns>
		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001CB7 RID: 7351 RVA: 0x00086932 File Offset: 0x00084B32
		public DataGridViewAutoSizeColumnMode PreviousMode
		{
			get
			{
				return this.previousMode;
			}
		}

		// Token: 0x04000C2B RID: 3115
		private DataGridViewAutoSizeColumnMode previousMode;

		// Token: 0x04000C2C RID: 3116
		private DataGridViewColumn dataGridViewColumn;
	}
}
