using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.CellStyleContentChanged" /> event.</summary>
	// Token: 0x020001B3 RID: 435
	public class DataGridViewCellStyleContentChangedEventArgs : EventArgs
	{
		// Token: 0x06001EAA RID: 7850 RVA: 0x00090834 File Offset: 0x0008EA34
		internal DataGridViewCellStyleContentChangedEventArgs(DataGridViewCellStyle dataGridViewCellStyle, bool changeAffectsPreferredSize)
		{
			this.dataGridViewCellStyle = dataGridViewCellStyle;
			this.changeAffectsPreferredSize = changeAffectsPreferredSize;
		}

		/// <summary>Gets the changed <see cref="T:System.Windows.Forms.DataGridViewCellStyle" />.</summary>
		/// <returns>The changed <see cref="T:System.Windows.Forms.DataGridViewCellStyle" />.</returns>
		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06001EAB RID: 7851 RVA: 0x0009084A File Offset: 0x0008EA4A
		public DataGridViewCellStyle CellStyle
		{
			get
			{
				return this.dataGridViewCellStyle;
			}
		}

		/// <summary>Gets the scope that is affected by the changed cell style.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyleScopes" /> that indicates which <see cref="T:System.Windows.Forms.DataGridView" /> entity owns the cell style that changed.</returns>
		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001EAC RID: 7852 RVA: 0x00090852 File Offset: 0x0008EA52
		public DataGridViewCellStyleScopes CellStyleScope
		{
			get
			{
				return this.dataGridViewCellStyle.Scope;
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001EAD RID: 7853 RVA: 0x0009085F File Offset: 0x0008EA5F
		internal bool ChangeAffectsPreferredSize
		{
			get
			{
				return this.changeAffectsPreferredSize;
			}
		}

		// Token: 0x04000CEC RID: 3308
		private DataGridViewCellStyle dataGridViewCellStyle;

		// Token: 0x04000CED RID: 3309
		private bool changeAffectsPreferredSize;
	}
}
