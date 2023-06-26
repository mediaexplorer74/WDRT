using System;

namespace System.Windows.Forms
{
	/// <summary>Defines constants that represent areas in a <see cref="T:System.Windows.Forms.ListView" /> or <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
	// Token: 0x020002D9 RID: 729
	[Flags]
	public enum ListViewHitTestLocations
	{
		/// <summary>A position outside the bounds of a <see cref="T:System.Windows.Forms.ListViewItem" /></summary>
		// Token: 0x04001312 RID: 4882
		None = 1,
		/// <summary>A position above the client portion of a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		// Token: 0x04001313 RID: 4883
		AboveClientArea = 256,
		/// <summary>A position below the client portion of a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		// Token: 0x04001314 RID: 4884
		BelowClientArea = 16,
		/// <summary>A position to the left of the client portion of a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		// Token: 0x04001315 RID: 4885
		LeftOfClientArea = 64,
		/// <summary>A position to the right of the client portion of a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
		// Token: 0x04001316 RID: 4886
		RightOfClientArea = 32,
		/// <summary>A position within the bounds of an image contained in a <see cref="T:System.Windows.Forms.ListView" /> or <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
		// Token: 0x04001317 RID: 4887
		Image = 2,
		/// <summary>A position within the bounds of an image associated with a <see cref="T:System.Windows.Forms.ListViewItem" /> that indicates the state of the item.</summary>
		// Token: 0x04001318 RID: 4888
		StateImage = 512,
		/// <summary>A position within the bounds of a text area contained in a <see cref="T:System.Windows.Forms.ListView" /> or <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
		// Token: 0x04001319 RID: 4889
		Label = 4
	}
}
