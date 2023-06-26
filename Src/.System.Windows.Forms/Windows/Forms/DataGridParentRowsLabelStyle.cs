using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies how the parent row labels of a <see cref="T:System.Windows.Forms.DataGrid" /> control are displayed.</summary>
	// Token: 0x02000184 RID: 388
	public enum DataGridParentRowsLabelStyle
	{
		/// <summary>Display no parent row labels.</summary>
		// Token: 0x04000A63 RID: 2659
		None,
		/// <summary>Displays the parent table name.</summary>
		// Token: 0x04000A64 RID: 2660
		TableName,
		/// <summary>Displays the parent column name.</summary>
		// Token: 0x04000A65 RID: 2661
		ColumnName,
		/// <summary>Displays both the parent table and column names.</summary>
		// Token: 0x04000A66 RID: 2662
		Both
	}
}
