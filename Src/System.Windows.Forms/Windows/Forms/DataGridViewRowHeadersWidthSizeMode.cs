using System;

namespace System.Windows.Forms
{
	/// <summary>Defines values for specifying how the row header width is adjusted.</summary>
	// Token: 0x02000194 RID: 404
	public enum DataGridViewRowHeadersWidthSizeMode
	{
		/// <summary>Users can adjust the column header width with the mouse.</summary>
		// Token: 0x04000C33 RID: 3123
		EnableResizing,
		/// <summary>Users cannot adjust the column header width with the mouse.</summary>
		// Token: 0x04000C34 RID: 3124
		DisableResizing,
		/// <summary>The row header width adjusts to fit the contents of all the row header cells.</summary>
		// Token: 0x04000C35 RID: 3125
		AutoSizeToAllHeaders,
		/// <summary>The row header width adjusts to fit the contents of all the row headers in the currently displayed rows.</summary>
		// Token: 0x04000C36 RID: 3126
		AutoSizeToDisplayedHeaders,
		/// <summary>The row header width adjusts to fit the contents of the first row header.</summary>
		// Token: 0x04000C37 RID: 3127
		AutoSizeToFirstHeader
	}
}
