using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for row-related <see cref="T:System.Windows.Forms.DataGridView" /> events.</summary>
	// Token: 0x0200020D RID: 525
	public class DataGridViewRowEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRowEventArgs" /> class.</summary>
		/// <param name="dataGridViewRow">The <see cref="T:System.Windows.Forms.DataGridViewRow" /> that the event occurred for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="dataGridViewRow" /> is <see langword="null" />.</exception>
		// Token: 0x0600227A RID: 8826 RVA: 0x000A4F50 File Offset: 0x000A3150
		public DataGridViewRowEventArgs(DataGridViewRow dataGridViewRow)
		{
			if (dataGridViewRow == null)
			{
				throw new ArgumentNullException("dataGridViewRow");
			}
			this.dataGridViewRow = dataGridViewRow;
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridViewRow" /> associated with the event.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewRow" /> associated with the event.</returns>
		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x0600227B RID: 8827 RVA: 0x000A4F6D File Offset: 0x000A316D
		public DataGridViewRow Row
		{
			get
			{
				return this.dataGridViewRow;
			}
		}

		// Token: 0x04000E2A RID: 3626
		private DataGridViewRow dataGridViewRow;
	}
}
