using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies how items align in the <see cref="T:System.Windows.Forms.ListView" />.</summary>
	// Token: 0x020002D3 RID: 723
	public enum ListViewAlignment
	{
		/// <summary>When the user moves an item, it remains where it is dropped.</summary>
		// Token: 0x040012FD RID: 4861
		Default,
		/// <summary>Items are aligned to the top of the <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		// Token: 0x040012FE RID: 4862
		Top = 2,
		/// <summary>Items are aligned to the left of the <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		// Token: 0x040012FF RID: 4863
		Left = 1,
		/// <summary>Items are aligned to an invisible grid in the control. When the user moves an item, it moves to the closest juncture in the grid.</summary>
		// Token: 0x04001300 RID: 4864
		SnapToGrid = 5
	}
}
