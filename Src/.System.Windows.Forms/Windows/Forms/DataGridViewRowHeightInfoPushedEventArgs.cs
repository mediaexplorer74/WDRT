using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.RowHeightInfoPushed" /> event of a <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
	// Token: 0x02000210 RID: 528
	public class DataGridViewRowHeightInfoPushedEventArgs : HandledEventArgs
	{
		// Token: 0x0600229F RID: 8863 RVA: 0x000A6661 File Offset: 0x000A4861
		internal DataGridViewRowHeightInfoPushedEventArgs(int rowIndex, int height, int minimumHeight)
			: base(false)
		{
			this.rowIndex = rowIndex;
			this.height = height;
			this.minimumHeight = minimumHeight;
		}

		/// <summary>Gets the height of the row the event occurred for.</summary>
		/// <returns>The row height, in pixels.</returns>
		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x060022A0 RID: 8864 RVA: 0x000A667F File Offset: 0x000A487F
		public int Height
		{
			get
			{
				return this.height;
			}
		}

		/// <summary>Gets the minimum height of the row the event occurred for.</summary>
		/// <returns>The minimum row height, in pixels.</returns>
		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x060022A1 RID: 8865 RVA: 0x000A6687 File Offset: 0x000A4887
		public int MinimumHeight
		{
			get
			{
				return this.minimumHeight;
			}
		}

		/// <summary>Gets the index of the row the event occurred for.</summary>
		/// <returns>The zero-based index of the row.</returns>
		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x060022A2 RID: 8866 RVA: 0x000A668F File Offset: 0x000A488F
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x04000E3E RID: 3646
		private int rowIndex;

		// Token: 0x04000E3F RID: 3647
		private int height;

		// Token: 0x04000E40 RID: 3648
		private int minimumHeight;
	}
}
