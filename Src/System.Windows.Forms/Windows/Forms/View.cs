using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies how list items are displayed in a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
	// Token: 0x0200042C RID: 1068
	public enum View
	{
		/// <summary>Each item appears as a full-sized icon with a label below it.</summary>
		// Token: 0x040027C4 RID: 10180
		LargeIcon,
		/// <summary>Each item appears on a separate line with further information about each item arranged in columns. The left-most column contains a small icon and label, and subsequent columns contain sub items as specified by the application. A column displays a header which can display a caption for the column. The user can resize each column at run time.</summary>
		// Token: 0x040027C5 RID: 10181
		Details,
		/// <summary>Each item appears as a small icon with a label to its right.</summary>
		// Token: 0x040027C6 RID: 10182
		SmallIcon,
		/// <summary>Each item appears as a small icon with a label to its right. Items are arranged in columns with no column headers.</summary>
		// Token: 0x040027C7 RID: 10183
		List,
		/// <summary>Each item appears as a full-sized icon with the item label and subitem information to the right of it. The subitem information that appears is specified by the application. This view is available only on Windows XP and the Windows Server 2003 family. On earlier operating systems, this value is ignored and the <see cref="T:System.Windows.Forms.ListView" /> control displays in the <see cref="F:System.Windows.Forms.View.LargeIcon" /> view.</summary>
		// Token: 0x040027C8 RID: 10184
		Tile
	}
}
