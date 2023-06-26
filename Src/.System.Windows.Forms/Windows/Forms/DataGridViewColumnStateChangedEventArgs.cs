using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.ColumnStateChanged" /> event.</summary>
	// Token: 0x020001C5 RID: 453
	public class DataGridViewColumnStateChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewColumnStateChangedEventArgs" /> class.</summary>
		/// <param name="dataGridViewColumn">The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> whose state has changed.</param>
		/// <param name="stateChanged">One of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
		// Token: 0x06001FC0 RID: 8128 RVA: 0x00097539 File Offset: 0x00095739
		public DataGridViewColumnStateChangedEventArgs(DataGridViewColumn dataGridViewColumn, DataGridViewElementStates stateChanged)
		{
			this.dataGridViewColumn = dataGridViewColumn;
			this.stateChanged = stateChanged;
		}

		/// <summary>Gets the column whose state changed.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> whose state changed.</returns>
		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06001FC1 RID: 8129 RVA: 0x0009754F File Offset: 0x0009574F
		public DataGridViewColumn Column
		{
			get
			{
				return this.dataGridViewColumn;
			}
		}

		/// <summary>Gets the new column state.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</returns>
		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06001FC2 RID: 8130 RVA: 0x00097557 File Offset: 0x00095757
		public DataGridViewElementStates StateChanged
		{
			get
			{
				return this.stateChanged;
			}
		}

		// Token: 0x04000D58 RID: 3416
		private DataGridViewColumn dataGridViewColumn;

		// Token: 0x04000D59 RID: 3417
		private DataGridViewElementStates stateChanged;
	}
}
