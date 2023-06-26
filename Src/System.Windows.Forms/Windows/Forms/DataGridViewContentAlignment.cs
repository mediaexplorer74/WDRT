using System;

namespace System.Windows.Forms
{
	/// <summary>Defines constants that indicate the alignment of content within a <see cref="T:System.Windows.Forms.DataGridView" /> cell.</summary>
	// Token: 0x020001CB RID: 459
	public enum DataGridViewContentAlignment
	{
		/// <summary>The alignment is not set.</summary>
		// Token: 0x04000D8D RID: 3469
		NotSet,
		/// <summary>The content is aligned vertically at the top and horizontally at the left of a cell.</summary>
		// Token: 0x04000D8E RID: 3470
		TopLeft,
		/// <summary>The content is aligned vertically at the top and horizontally at the center of a cell.</summary>
		// Token: 0x04000D8F RID: 3471
		TopCenter,
		/// <summary>The content is aligned vertically at the top and horizontally at the right of a cell.</summary>
		// Token: 0x04000D90 RID: 3472
		TopRight = 4,
		/// <summary>The content is aligned vertically at the middle and horizontally at the left of a cell.</summary>
		// Token: 0x04000D91 RID: 3473
		MiddleLeft = 16,
		/// <summary>The content is aligned at the vertical and horizontal center of a cell.</summary>
		// Token: 0x04000D92 RID: 3474
		MiddleCenter = 32,
		/// <summary>The content is aligned vertically at the middle and horizontally at the right of a cell.</summary>
		// Token: 0x04000D93 RID: 3475
		MiddleRight = 64,
		/// <summary>The content is aligned vertically at the bottom and horizontally at the left of a cell.</summary>
		// Token: 0x04000D94 RID: 3476
		BottomLeft = 256,
		/// <summary>The content is aligned vertically at the bottom and horizontally at the center of a cell.</summary>
		// Token: 0x04000D95 RID: 3477
		BottomCenter = 512,
		/// <summary>The content is aligned vertically at the bottom and horizontally at the right of a cell.</summary>
		// Token: 0x04000D96 RID: 3478
		BottomRight = 1024
	}
}
