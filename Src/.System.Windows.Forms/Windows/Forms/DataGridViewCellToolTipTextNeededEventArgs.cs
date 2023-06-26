using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.CellToolTipTextNeeded" /> event.</summary>
	// Token: 0x020001B7 RID: 439
	public class DataGridViewCellToolTipTextNeededEventArgs : DataGridViewCellEventArgs
	{
		// Token: 0x06001EB1 RID: 7857 RVA: 0x000908D0 File Offset: 0x0008EAD0
		internal DataGridViewCellToolTipTextNeededEventArgs(int columnIndex, int rowIndex, string toolTipText)
			: base(columnIndex, rowIndex)
		{
			this.toolTipText = toolTipText;
		}

		/// <summary>Gets or sets the ToolTip text.</summary>
		/// <returns>The current ToolTip text.</returns>
		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001EB2 RID: 7858 RVA: 0x000908E1 File Offset: 0x0008EAE1
		// (set) Token: 0x06001EB3 RID: 7859 RVA: 0x000908E9 File Offset: 0x0008EAE9
		public string ToolTipText
		{
			get
			{
				return this.toolTipText;
			}
			set
			{
				this.toolTipText = value;
			}
		}

		// Token: 0x04000CFC RID: 3324
		private string toolTipText;
	}
}
