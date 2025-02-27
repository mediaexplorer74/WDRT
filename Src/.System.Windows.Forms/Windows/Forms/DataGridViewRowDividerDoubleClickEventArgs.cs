﻿using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.RowDividerDoubleClick" /> event of a <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
	// Token: 0x0200020B RID: 523
	public class DataGridViewRowDividerDoubleClickEventArgs : HandledMouseEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRowDividerDoubleClickEventArgs" /> class.</summary>
		/// <param name="rowIndex">The index of the row above the row divider that was double-clicked.</param>
		/// <param name="e">A new <see cref="T:System.Windows.Forms.HandledMouseEventArgs" /> containing the inherited event data.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="rowIndex" /> is less than -1.</exception>
		// Token: 0x06002274 RID: 8820 RVA: 0x000A4ECC File Offset: 0x000A30CC
		public DataGridViewRowDividerDoubleClickEventArgs(int rowIndex, HandledMouseEventArgs e)
			: base(e.Button, e.Clicks, e.X, e.Y, e.Delta, e.Handled)
		{
			if (rowIndex < -1)
			{
				throw new ArgumentOutOfRangeException("rowIndex");
			}
			this.rowIndex = rowIndex;
		}

		/// <summary>The index of the row above the row divider that was double-clicked.</summary>
		/// <returns>The index of the row above the divider.</returns>
		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06002275 RID: 8821 RVA: 0x000A4F19 File Offset: 0x000A3119
		public int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		// Token: 0x04000E27 RID: 3623
		private int rowIndex;
	}
}
