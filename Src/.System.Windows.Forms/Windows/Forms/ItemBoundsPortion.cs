﻿using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies a portion of the list view item from which to retrieve the bounding rectangle.</summary>
	// Token: 0x020002A5 RID: 677
	public enum ItemBoundsPortion
	{
		/// <summary>The bounding rectangle of the entire item, including the icon, the item text, and the subitem text (if displayed), should be retrieved.</summary>
		// Token: 0x0400111B RID: 4379
		Entire,
		/// <summary>The bounding rectangle of the icon or small icon should be retrieved.</summary>
		// Token: 0x0400111C RID: 4380
		Icon,
		/// <summary>The bounding rectangle of the item text should be retrieved.</summary>
		// Token: 0x0400111D RID: 4381
		Label,
		/// <summary>The bounding rectangle of the icon or small icon and the item text should be retrieved. In all views except the details view of the <see cref="T:System.Windows.Forms.ListView" />, this value specifies the same bounding rectangle as the <see langword="Entire" /> value. In details view, this value specifies the bounding rectangle specified by the <see langword="Entire" /> value without the subitems. If the <see cref="P:System.Windows.Forms.ListView.CheckBoxes" /> property is set to <see langword="true" />, this property does not include the area of the check boxes in its bounding rectangle. To include the entire item, including the check boxes, use the <see langword="Entire" /> value when calling the <see cref="M:System.Windows.Forms.ListView.GetItemRect(System.Int32)" /> method.</summary>
		// Token: 0x0400111E RID: 4382
		ItemOnly
	}
}
