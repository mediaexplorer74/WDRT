using System;

namespace System.Windows.Forms
{
	/// <summary>Defines values for specifying how the height of the column headers is adjusted.</summary>
	// Token: 0x02000193 RID: 403
	public enum DataGridViewColumnHeadersHeightSizeMode
	{
		/// <summary>Users can adjust the column header height with the mouse.</summary>
		// Token: 0x04000C2F RID: 3119
		EnableResizing,
		/// <summary>Users cannot adjust the column header height with the mouse.</summary>
		// Token: 0x04000C30 RID: 3120
		DisableResizing,
		/// <summary>The column header height adjusts to fit the contents of all the column header cells.</summary>
		// Token: 0x04000C31 RID: 3121
		AutoSize
	}
}
