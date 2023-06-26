using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for column-related events of a <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
	// Token: 0x020001C2 RID: 450
	public class DataGridViewColumnEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewColumnEventArgs" /> class.</summary>
		/// <param name="dataGridViewColumn">The column that the event occurs for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridViewColumn" /> is <see langword="null" />.</exception>
		// Token: 0x06001FAB RID: 8107 RVA: 0x000959E1 File Offset: 0x00093BE1
		public DataGridViewColumnEventArgs(DataGridViewColumn dataGridViewColumn)
		{
			if (dataGridViewColumn == null)
			{
				throw new ArgumentNullException("dataGridViewColumn");
			}
			this.dataGridViewColumn = dataGridViewColumn;
		}

		/// <summary>Gets the column that the event occurs for.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> that the event occurs for.</returns>
		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06001FAC RID: 8108 RVA: 0x000959FE File Offset: 0x00093BFE
		public DataGridViewColumn Column
		{
			get
			{
				return this.dataGridViewColumn;
			}
		}

		// Token: 0x04000D44 RID: 3396
		private DataGridViewColumn dataGridViewColumn;
	}
}
