using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies how an accessible object is selected or receives focus.</summary>
	// Token: 0x0200011A RID: 282
	[Flags]
	public enum AccessibleSelection
	{
		/// <summary>The selection or focus of an object is unchanged.</summary>
		// Token: 0x04000594 RID: 1428
		None = 0,
		/// <summary>Assigns focus to an object and makes it the anchor, which is the starting point for the selection. Can be combined with <see langword="TakeSelection" />, <see langword="ExtendSelection" />, <see langword="AddSelection" />, or <see langword="RemoveSelection" />.</summary>
		// Token: 0x04000595 RID: 1429
		TakeFocus = 1,
		/// <summary>Selects the object and deselects all other objects in the container.</summary>
		// Token: 0x04000596 RID: 1430
		TakeSelection = 2,
		/// <summary>Selects all objects between the anchor and the selected object.</summary>
		// Token: 0x04000597 RID: 1431
		ExtendSelection = 4,
		/// <summary>Adds the object to the selection.</summary>
		// Token: 0x04000598 RID: 1432
		AddSelection = 8,
		/// <summary>Removes the object from the selection.</summary>
		// Token: 0x04000599 RID: 1433
		RemoveSelection = 16
	}
}
