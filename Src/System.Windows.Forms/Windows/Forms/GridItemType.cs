using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the valid grid item types for a <see cref="T:System.Windows.Forms.PropertyGrid" />.</summary>
	// Token: 0x0200026A RID: 618
	public enum GridItemType
	{
		/// <summary>A grid entry that corresponds to a property.</summary>
		// Token: 0x0400104E RID: 4174
		Property,
		/// <summary>A grid entry that is a category name. A category is a descriptive grouping for groups of <see cref="T:System.Windows.Forms.GridItem" /> rows. Typical categories include the following Behavior, Layout, Data, and Appearance.</summary>
		// Token: 0x0400104F RID: 4175
		Category,
		/// <summary>The <see cref="T:System.Windows.Forms.GridItem" /> is an element of an array.</summary>
		// Token: 0x04001050 RID: 4176
		ArrayValue,
		/// <summary>A root item in the grid hierarchy.</summary>
		// Token: 0x04001051 RID: 4177
		Root
	}
}
