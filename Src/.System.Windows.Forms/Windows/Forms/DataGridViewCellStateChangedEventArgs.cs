using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.CellStateChanged" /> event.</summary>
	// Token: 0x020001B0 RID: 432
	public class DataGridViewCellStateChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellStateChangedEventArgs" /> class.</summary>
		/// <param name="dataGridViewCell">The <see cref="T:System.Windows.Forms.DataGridViewCell" /> that has a changed state.</param>
		/// <param name="stateChanged">One of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values indicating the state that has changed on the cell.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridViewCell" /> is <see langword="null" />.</exception>
		// Token: 0x06001E6D RID: 7789 RVA: 0x0008F72C File Offset: 0x0008D92C
		public DataGridViewCellStateChangedEventArgs(DataGridViewCell dataGridViewCell, DataGridViewElementStates stateChanged)
		{
			if (dataGridViewCell == null)
			{
				throw new ArgumentNullException("dataGridViewCell");
			}
			this.dataGridViewCell = dataGridViewCell;
			this.stateChanged = stateChanged;
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that has a changed state.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCell" /> whose state has changed.</returns>
		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06001E6E RID: 7790 RVA: 0x0008F750 File Offset: 0x0008D950
		public DataGridViewCell Cell
		{
			get
			{
				return this.dataGridViewCell;
			}
		}

		/// <summary>Gets the state that has changed on the cell.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values indicating the state that has changed on the cell.</returns>
		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001E6F RID: 7791 RVA: 0x0008F758 File Offset: 0x0008D958
		public DataGridViewElementStates StateChanged
		{
			get
			{
				return this.stateChanged;
			}
		}

		// Token: 0x04000CD8 RID: 3288
		private DataGridViewCell dataGridViewCell;

		// Token: 0x04000CD9 RID: 3289
		private DataGridViewElementStates stateChanged;
	}
}
