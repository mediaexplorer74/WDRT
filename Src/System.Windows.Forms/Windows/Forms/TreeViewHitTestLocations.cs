using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Defines constants that represent areas of a <see cref="T:System.Windows.Forms.TreeView" /> or <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
	// Token: 0x0200041E RID: 1054
	[Flags]
	[ComVisible(true)]
	public enum TreeViewHitTestLocations
	{
		/// <summary>A position in the client area of the <see cref="T:System.Windows.Forms.TreeView" /> control, but not on a node or a portion of a node.</summary>
		// Token: 0x04002799 RID: 10137
		None = 1,
		/// <summary>A position within the bounds of an image contained on a <see cref="T:System.Windows.Forms.TreeView" /> or <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
		// Token: 0x0400279A RID: 10138
		Image = 2,
		/// <summary>A position on the text portion of a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
		// Token: 0x0400279B RID: 10139
		Label = 4,
		/// <summary>A position in the indentation area for a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
		// Token: 0x0400279C RID: 10140
		Indent = 8,
		/// <summary>A position above the client portion of a <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
		// Token: 0x0400279D RID: 10141
		AboveClientArea = 256,
		/// <summary>A position below the client portion of a <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
		// Token: 0x0400279E RID: 10142
		BelowClientArea = 512,
		/// <summary>A position to the left of the client area of a <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
		// Token: 0x0400279F RID: 10143
		LeftOfClientArea = 2048,
		/// <summary>A position to the right of the client area of the <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
		// Token: 0x040027A0 RID: 10144
		RightOfClientArea = 1024,
		/// <summary>A position to the right of the text area of a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
		// Token: 0x040027A1 RID: 10145
		RightOfLabel = 32,
		/// <summary>A position within the bounds of a state image for a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
		// Token: 0x040027A2 RID: 10146
		StateImage = 64,
		/// <summary>A position on the plus/minus area of a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
		// Token: 0x040027A3 RID: 10147
		PlusMinus = 16
	}
}
