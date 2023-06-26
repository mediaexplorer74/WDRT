using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.RowStateChanged" /> event of a <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
	// Token: 0x02000215 RID: 533
	public class DataGridViewRowStateChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRowStateChangedEventArgs" /> class.</summary>
		/// <param name="dataGridViewRow">The <see cref="T:System.Windows.Forms.DataGridViewRow" /> that has a changed state.</param>
		/// <param name="stateChanged">One of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values indicating the state that has changed on the row.</param>
		// Token: 0x060022D1 RID: 8913 RVA: 0x000A6F9F File Offset: 0x000A519F
		public DataGridViewRowStateChangedEventArgs(DataGridViewRow dataGridViewRow, DataGridViewElementStates stateChanged)
		{
			this.dataGridViewRow = dataGridViewRow;
			this.stateChanged = stateChanged;
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridViewRow" /> that has a changed state.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewRow" /> that has a changed state.</returns>
		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x060022D2 RID: 8914 RVA: 0x000A6FB5 File Offset: 0x000A51B5
		public DataGridViewRow Row
		{
			get
			{
				return this.dataGridViewRow;
			}
		}

		/// <summary>Gets the state that has changed on the row.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values indicating the state that has changed on the row.</returns>
		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x060022D3 RID: 8915 RVA: 0x000A6FBD File Offset: 0x000A51BD
		public DataGridViewElementStates StateChanged
		{
			get
			{
				return this.stateChanged;
			}
		}

		// Token: 0x04000E5A RID: 3674
		private DataGridViewRow dataGridViewRow;

		// Token: 0x04000E5B RID: 3675
		private DataGridViewElementStates stateChanged;
	}
}
