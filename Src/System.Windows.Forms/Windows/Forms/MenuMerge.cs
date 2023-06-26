using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the behavior of a <see cref="T:System.Windows.Forms.MenuItem" /> when it is merged with items in another menu.</summary>
	// Token: 0x020002F4 RID: 756
	public enum MenuMerge
	{
		/// <summary>The <see cref="T:System.Windows.Forms.MenuItem" /> is added to the collection of existing <see cref="T:System.Windows.Forms.MenuItem" /> objects in a merged menu.</summary>
		// Token: 0x040013CE RID: 5070
		Add,
		/// <summary>The <see cref="T:System.Windows.Forms.MenuItem" /> replaces an existing <see cref="T:System.Windows.Forms.MenuItem" /> at the same position in a merged menu.</summary>
		// Token: 0x040013CF RID: 5071
		Replace,
		/// <summary>All submenu items of this <see cref="T:System.Windows.Forms.MenuItem" /> are merged with those of existing <see cref="T:System.Windows.Forms.MenuItem" /> objects at the same position in a merged menu.</summary>
		// Token: 0x040013D0 RID: 5072
		MergeItems,
		/// <summary>The <see cref="T:System.Windows.Forms.MenuItem" /> is not included in a merged menu.</summary>
		// Token: 0x040013D1 RID: 5073
		Remove
	}
}
