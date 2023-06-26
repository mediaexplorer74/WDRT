using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies a location in a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
	// Token: 0x020001FC RID: 508
	public enum DataGridViewHitTestType
	{
		/// <summary>An empty part of the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		// Token: 0x04000DD3 RID: 3539
		None,
		/// <summary>A cell in the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		// Token: 0x04000DD4 RID: 3540
		Cell,
		/// <summary>A column header in the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		// Token: 0x04000DD5 RID: 3541
		ColumnHeader,
		/// <summary>A row header in the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		// Token: 0x04000DD6 RID: 3542
		RowHeader,
		/// <summary>The top left column header in the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		// Token: 0x04000DD7 RID: 3543
		TopLeftHeader,
		/// <summary>The horizontal scroll bar of the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		// Token: 0x04000DD8 RID: 3544
		HorizontalScrollBar,
		/// <summary>The vertical scroll bar of the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
		// Token: 0x04000DD9 RID: 3545
		VerticalScrollBar
	}
}
