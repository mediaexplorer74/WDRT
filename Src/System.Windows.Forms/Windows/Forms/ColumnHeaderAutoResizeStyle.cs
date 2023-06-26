using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies how a column contained in a <see cref="T:System.Windows.Forms.ListView" /> should be resized.</summary>
	// Token: 0x02000155 RID: 341
	public enum ColumnHeaderAutoResizeStyle
	{
		/// <summary>Specifies no resizing should occur.</summary>
		// Token: 0x04000796 RID: 1942
		None,
		/// <summary>Specifies the column should be resized based on the length of the column header content.</summary>
		// Token: 0x04000797 RID: 1943
		HeaderSize,
		/// <summary>Specifies the column should be resized based on the length of the column content.</summary>
		// Token: 0x04000798 RID: 1944
		ColumnContent
	}
}
