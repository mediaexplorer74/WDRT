using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the layout of multiple document interface (MDI) child windows in an MDI parent window.</summary>
	// Token: 0x020002EC RID: 748
	public enum MdiLayout
	{
		/// <summary>All MDI child windows are cascaded within the client region of the MDI parent form.</summary>
		// Token: 0x04001395 RID: 5013
		Cascade,
		/// <summary>All MDI child windows are tiled horizontally within the client region of the MDI parent form.</summary>
		// Token: 0x04001396 RID: 5014
		TileHorizontal,
		/// <summary>All MDI child windows are tiled vertically within the client region of the MDI parent form.</summary>
		// Token: 0x04001397 RID: 5015
		TileVertical,
		/// <summary>All MDI child icons are arranged within the client region of the MDI parent form.</summary>
		// Token: 0x04001398 RID: 5016
		ArrangeIcons
	}
}
